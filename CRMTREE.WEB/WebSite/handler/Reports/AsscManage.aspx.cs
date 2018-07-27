using CRMTree.Public;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using CRMTreeDatabase;
using System.Dynamic;
using PetaPoco;
using System.IO;
using CRMTree.Model.User;

public partial class handler_Reports_AsscManage : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        try
        {
            base.OnLoad(e);

            var o = Request.Params["o"];
            var data = JsonConvert.DeserializeObject<dynamic>(o);
            string acion = data.action;
            switch (acion)
            {
                case "Save_Assc":
                    Save_Assc(data);
                    break;
                case "Get_AsscInfo":
                    Get_AsscInfo(data);
                    break;
                case "Get_Empl_Type":
                    Get_Empl_Type();
                    break;
                case "Get_User_Groups":
                    Get_User_Groups(data);
                    break;
            }
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void Get_User_Groups(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var o = db.Query<dynamic>(";exec P_Get_User_Groups @UG,@PUG,@IsEn"
            , new { UG = UserSession.User.AU_UG_Code, PUG = (int)data.PUG, IsEn = Interna }
            );

        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 保存日程安排
    /// </summary>
    /// <param name="data"></param>
    private void Save_Assc(dynamic data)
    {
        //var ug_code = this.UserSession.User.AU_UG_Code;
        //if (ug_code != 28 && ug_code != 40)
        //{
        //    throw new Exception(Interna ? "Without the permission!" : "无此权限！");
        //}

        var db = DBCRMTree.GetInstance();
        try
        {
            using (var tran = db.GetTransaction())
            {
                var au_code = Save_Personal(data);

                if (au_code > 0)
                {
                    Save_Contacts(data, au_code);

                    Save_Contacts_List(data, au_code);

                    //        Save_Dealer_Emp(data, au_code);
                }
                else
                {
                    throw new Exception(Interna ? "No Associates!" : "无合伙人！");
                }
                tran.Complete();
            }
            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
        catch (Exception ex)
        {
            db.AbortTransaction();
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    /// <summary>
    /// 保存个人信息
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private long Save_Personal(dynamic data)
    {
        var s_personal = JsonConvert.SerializeObject(data.personal);

        CRMTree.Model.CT_All_Users personal = JsonConvert.DeserializeObject<CRMTree.Model.CT_All_Users>(s_personal);

        CRMTree.Model.CT_Drivers_List_New drivermodel = JsonConvert.DeserializeObject<CRMTree.Model.CT_Drivers_List_New>(s_personal);
        if (!string.IsNullOrWhiteSpace(personal.AU_Username))
        {
            if (CT_All_User.Exists("AU_Code != @0 and AU_Username = @1", personal.AU_Code, personal.AU_Username))
            {
                throw new Exception(Interna ? "User account already exists!" : "用户账号已存在！");
            }
        }
        if (string.IsNullOrWhiteSpace(personal.AU_Name))
        {
            throw new Exception(Interna ? "NOT NULL!" : "姓名不能为空！");
        }
        long? au_code = data.AU_Code;
        personal.AU_Update_dt = DateTime.Now;

        //更新个人资料
        CRMTree.BLL.BL_UserEntity bll = new CRMTree.BLL.BL_UserEntity();
        if (!string.IsNullOrWhiteSpace(personal.AU_Password))
        {
            if (!bll.ExistsPwd(personal.AU_Password, personal.AU_Username))
                personal.AU_Password = ShInfoTech.Common.Security.Md5(personal.AU_Password);
        } 

        int isSuccess = 0;


        if (au_code != null) //添加
        {
            bll.Update_CT_All_Users(personal);
            drivermodel.MAU_Code = data.MAU_Code;
        }

        isSuccess = bll.add_personal(drivermodel);
        if (isSuccess > 0)
        {
            if (au_code == null)
            {
                au_code = bll.getMaxAU_Code();
            }
            return (long)au_code;
        }

        else
            return isSuccess;
    }

    /// <summary>
    /// 保存经销商员工信息
    /// </summary>
    /// <param name="data"></param>
    /// <param name="au_code"></param>
    private void Save_Contacts_List(dynamic data, long au_code)
    {
        var s_personal = JsonConvert.SerializeObject(data.personal);
        //        int Empl_Code = (int)data.AU_Code;
        //        if (Empl_Code > 0)
        //        {
        //            var empl = CT_Dealer_Empl.SingleOrDefault(Empl_Code);
        //            empl.EX_DE_Activate_dt = empl.DE_Activate_dt.HasValue ? empl.DE_Activate_dt.Value.ToString(Interna ? "MM/dd/yyyy" : "yyyy-MM-dd") : "";
        //            o.dealerEmpl = empl;

        //        CT_All_User empl = JsonConvert.DeserializeObject<CT_All_User>(s_personal);
        CT_Drivers_List Contacts_List = JsonConvert.DeserializeObject<CT_Drivers_List>(s_personal);
        Contacts_List.DL_AU_Code = au_code;
        Contacts_List.DL_M_AU_Code = data.MAU_Code;
        Contacts_List.DL_Access = data.personal.DL_Acc == "" ? null : data.personal.DL_Acc;
        Contacts_List.DL_Relation = data.personal.DL_Rel == "" ? null : data.personal.DL_Rel;
        Contacts_List.DL_Update_dt = DateTime.Now;

        if (Contacts_List.DL_AU_Code > 0)
        {
            var db = DBCRMTree.GetInstance();
            db.Update<CT_Drivers_List>(@" Set 
                 [DL_Access] = @DL_Access
                ,[DL_Relation] = @DL_Relation
                ,[DL_Update_dt] = @DL_Update_dt
                Where DL_M_AU_Code=@DL_M_AU_Code and DL_AU_Code=@DL_AU_Code ", Contacts_List);
        }
        else
        {
            Contacts_List.Insert();
        }

    }

    /// <summary>
    /// 保存联系信息
    /// </summary>
    /// <param name="data"></param>
    /// <param name="au_code"></param>
    private void Save_Contacts(dynamic data, long au_code)
    {
        var s_contacts = JsonConvert.SerializeObject(data.contacts.changes);
        List<EX_CT_Contacts> contacts = JsonConvert.DeserializeObject<List<EX_CT_Contacts>>(s_contacts);
        byte pref = 1;
        foreach (var c in contacts)
        {
            var s = JsonConvert.SerializeObject(c.o);
            switch (c.type)
            {
                //Address
                case 1:
                    var address = JsonConvert.DeserializeObject<CT_Address_List>(s);
                    address.AL_AU_AD_Code = au_code;
                    address.AL_UType = 1;
                    address.AL_Pref = pref++;
                    address.AL_Update_dt = DateTime.Now;
                    address.AL_Active = true;
                    if (address.AL_Code > 0)
                    {
                        address.Update(new string[]{
                                    "AL_AU_AD_Code",
                                    "AL_UType",
                                    "AL_Active",
                                    "AL_Pref",
                                    "AL_Update_dt",
                                    "AL_Type",
                                    "AL_Add1",
                                    "AL_Add2",
                                    "AL_District",
                                    "AL_City",
                                    "AL_State",
                                    "AL_Zip"
                                    });
                    }
                    else
                    {
                        address.Insert();
                    }
                    break;
                //Phone
                case 2:
                    var phone = JsonConvert.DeserializeObject<CT_Phone_List>(s);
                    phone.PL_AU_AD_Code = au_code;
                    phone.PL_UType = 1;
                    phone.PL_Pref = pref++;
                    phone.PL_Update_dt = DateTime.Now;
                    phone.PL_Active = true;
                    if (phone.PL_Code > 0)
                    {
                        phone.Update(new string[]{
                                    "PL_AU_AD_Code",
                                    "PL_UType",
                                    "PL_Active",
                                    "PL_Pref",
                                    "PL_Update_dt",
                                    "PL_Type",
                                    "PL_Number"
                                    });
                    }
                    else
                    {
                        phone.Insert();
                    }
                    break;
                //EMail
                case 3:
                    var email = JsonConvert.DeserializeObject<CT_Email_List>(s);
                    email.EL_AU_AD_Code = au_code;
                    email.EL_UType = 1;
                    email.EL_Pref = pref++;
                    email.EL_Update_dt = DateTime.Now;
                    email.EL_Active = true;
                    if (email.EL_Code > 0)
                    {
                        email.Update(new string[]{
                                    "EL_AU_AD_Code",
                                    "EL_UType",
                                    "EL_Active",
                                    "EL_Pref",
                                    "EL_Update_dt",
                                    "EL_Type",
                                    "EL_Address"
                                    });
                    }
                    else
                    {
                        email.Insert();
                    }
                    break;
                //Messaging
                case 4:
                    var msg = JsonConvert.DeserializeObject<CT_Messaging_List>(s);
                    msg.ML_AU_AD_Code = au_code;
                    msg.ML_UType = 1;
                    msg.ML_Pref = pref++;
                    msg.ML_Update_dt = DateTime.Now;
                    msg.ML_Active = true;
                    if (msg.ML_Code > 0)
                    {
                        msg.Update(new string[]{
                                    "ML_AU_AD_Code",
                                    "ML_UType",
                                    "ML_Active",
                                    "ML_Pref",
                                    "ML_Update_dt",
                                    "ML_MC_Code",
                                    "ML_Handle"
                                    });
                    }
                    else
                    {
                        msg.Insert();
                    }
                    break;
            }
        }

        var s_deletes = JsonConvert.SerializeObject(data.contacts.deletes);
        List<EX_CT_Contacts> deletes = JsonConvert.DeserializeObject<List<EX_CT_Contacts>>(s_deletes);
        foreach (var d in deletes)
        {
            var s = JsonConvert.SerializeObject(d.o);
            switch (d.type)
            {
                //Address
                case 1:
                    var address = JsonConvert.DeserializeObject<CT_Address_List>(s);
                    address.AL_Update_dt = DateTime.Now;
                    address.AL_Active = false;
                    address.Update(new string[]{
                                "AL_Update_dt",
                                "AL_Active"
                                });
                    break;
                //Phone
                case 2:
                    var phone = JsonConvert.DeserializeObject<CT_Phone_List>(s);
                    phone.PL_Update_dt = DateTime.Now;
                    phone.PL_Active = false;
                    phone.Update(new string[]{
                                "PL_Update_dt",
                                "PL_Active"
                                });
                    break;
                //EMail
                case 3:
                    var email = JsonConvert.DeserializeObject<CT_Email_List>(s);
                    email.EL_Update_dt = DateTime.Now;
                    email.EL_Active = false;
                    email.Update(new string[]{
                                "EL_Update_dt",
                                "EL_Active"
                                });
                    break;
                //Messaging
                case 4:
                    var msg = JsonConvert.DeserializeObject<CT_Messaging_List>(s);
                    msg.ML_Update_dt = DateTime.Now;
                    msg.ML_Active = false;
                    msg.Update(new string[]{
                                "ML_Update_dt",
                                "ML_Active"
                                });
                    break;
            }
        }
    }


    /// <summary>
    /// 获得员工信息
    /// </summary>
    /// <param name="data"></param>
    private void Get_AsscInfo(dynamic data)
    {
        dynamic o = new ExpandoObject();
        //        int Empl_Code = (int)data.AU_Code;
        //        if (Empl_Code > 0)
        //        {
        //            var empl = CT_Dealer_Empl.SingleOrDefault(Empl_Code);
        //            empl.EX_DE_Activate_dt = empl.DE_Activate_dt.HasValue ? empl.DE_Activate_dt.Value.ToString(Interna ? "MM/dd/yyyy" : "yyyy-MM-dd") : "";
        //            o.dealerEmpl = empl;
        var AU_Code = (int)data.AU_Code;
        var MAU_Code = (int)data.MAU_Code;
        if (AU_Code > 0)
        {

            #region personal
            o.personal = CT_All_User.SingleOrDefault(@"select 
                AU_Code,
                AU_Active_tag,
                AU_Name,
                AU_Username,
                AU_Password,
                AU_Dr_Lic,
                AU_Gender,
                AU_ID_Type,
                AU_ID_No,
                AU_Education,
                AU_Married,
                AU_Type,
                AU_UG_Code,
                AU_Occupation,
                AU_Industry,
                AU_B_date
                from CT_All_Users
                where AU_Code=@0", AU_Code);
            #endregion

            #region Categories
            o.Categories = CT_Drivers_ListNew.SingleOrDefault(@"select distinct DL_Access as DL_Acc,DL_Relation as DL_Rel,DL_type,[dbo].[f_GetDL_CI_CodesByMAUCodeANDAU_Code](@1,@0) AS DL_CI_Codes from CT_Drivers_List 
                where DL_AU_Code=@0 and DL_M_AU_Code=@1", AU_Code, MAU_Code);
            //            o.Categories = new ExpandoObject();
            var db = DBCRMTree.GetInstance();
            //            var sql = string.Format(@"select DL_Access,DL_Relation,DL_type, DL_CI_Code from CT_Drivers_List 
            //                where DL_AU_Code=@0 and DL_M_AU_Code=@1");
            //            o.Categories = db.Query<dynamic>(sql, AU_Code, MAU_Code);

            #endregion

            #region contacts
            o.contacts = new ExpandoObject();
            var sql_text_part = Interna ? "[text_en]" : "[text_cn]";
            var sql = string.Format(@"Select AL_Code,AL_Type,AL_Add1,AL_Add2,AL_District,AL_City
                ,AL_State,AL_Zip,AL_Pref as [pref]
                ,(select {0} from words where p_id = 55 and [value] = al_type) as AL_Type_Text
                FROM CT_Address_List 
                WHERE AL_AU_AD_Code=@0 and isnull(AL_Active,1) = 1
                ", sql_text_part);
            o.contacts.address = db.Query<dynamic>(sql, AU_Code);

            sql = string.Format(@"Select PL_Code,PL_Type,PL_Number,PL_pref as [pref] 
                ,(select {0} from words where p_id = 47 and [value] = pl_type) as PL_Type_Text
                from CT_Phone_List 
                WHERE PL_AU_AD_Code=@0 and isnull(PL_Active,1) = 1
                ", sql_text_part);
            o.contacts.phone = db.Query<dynamic>(sql, AU_Code);

            sql = string.Format(@"Select EL_Code,EL_Type,EL_Address,EL_Pref as [pref]
                ,(select {0} from words where p_id = 44 and [value] = el_type) as EL_Type_Text
                from CT_eMail_List 
                WHERE EL_AU_AD_Code=@0 and isnull(EL_Active,1) = 1
                ", sql_text_part);
            o.contacts.email = db.Query<dynamic>(sql, AU_Code);

            sql_text_part = Interna ? "[MC_Name_EN]" : "[MC_Name_CN]";
            sql = string.Format(@"Select ML_Type as ML_MC_Code,ML_Code,ML_Handle,ML_Pref as [pref]
                ,ISNULL((SELECT {0} FROM [CT_Messaging_Carriers] where MC_Code=[ML_Type]),'') as ML_MC_Code_Text
                from CT_Messaging_List 
                WHERE ML_AU_AD_Code=@0 and isnull(ML_Active,1) = 1
                ", sql_text_part);
            o.contacts.messaging = db.Query<dynamic>(sql, AU_Code);
            #endregion

        }
        //        }
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Empl_Type()
    {
        var o = Get_CT_Empl_Type();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private IEnumerable<dynamic> Get_CT_Empl_Type()
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[PET_Type_EN]" : "[PET_Type_CN]";
        var sql = string.Format(@"
            SELECT 
            {0} as [text],
            PET_Code as [value],
            PET_UG_Code
            FROM CT_Empl_Type
            WHERE PET_Cat = 1",
            sql_text_part);

        return db.Query<dynamic>(sql);
    }
}