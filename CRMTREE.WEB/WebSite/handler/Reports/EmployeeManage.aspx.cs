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

public partial class handler_Reports_EmployeeManage : BasePage
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
                case "Save_Employee":
                    Save_Employee(data);
                    break;
                case "Get_EmployeeInfo":
                    Get_EmployeeInfo(data);
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
        //var db = DBCRMTree.GetInstance();
        //var o = db.Query<dynamic>(";exec P_Get_User_Groups @UG,@PUG,@IsEn"
        //    , new { UG = UserSession.User.AU_UG_Code, PUG = (int)data.PUG, IsEn = Interna }
        //    );

        //Response.Write(JsonConvert.SerializeObject(o));
 
        var o = Get_CT_User_Groups();
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private IEnumerable<dynamic> Get_CT_User_Groups()
    {
        var db = DBCRMTree.GetInstance();
        var lng = Interna ? "EN" : "CN";
        var sql = string.Format(@"SELECT [UG_Code] AS value
            ,UG_Name_{0} AS [text]
            ,(Select text_{0} from [Words] where p_id=4203 and value=[UG_UType]) as [group]
        FROM [CT_User_Groups]
        WHERE UG_UType <= @0 and isnull(UG_Name_{0},'') <> '' and UG_Code <= @1 order by UG_Utype Asc,UG_Code ASC", lng);

        return db.Query<dynamic>(sql, UserSession.User.UG_UType,UserSession.User.AU_UG_Code);
    }


    #region 保存员工信息
    /// <summary>
    /// 保存日程安排
    /// </summary>
    /// <param name="data"></param>
    private void Save_Employee(dynamic data)
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

                    Save_Schedule(data, au_code);

                    Save_Dealer_Emp(data, au_code);
                }
                else
                {
                    throw new Exception(Interna ? "No employee code!" : "无员工编号！");
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
        CT_All_User personal = JsonConvert.DeserializeObject<CT_All_User>(s_personal);

        if (!string.IsNullOrWhiteSpace(personal.AU_Username))
        {
            if (CT_All_User.Exists("AU_Code != @0 and AU_Username = @1", personal.AU_Code, personal.AU_Username))
            {
                throw new Exception(Interna ? "User account already exists!" : "用户账号已存在！");
            }
        }

        long au_code = personal.AU_Code;
        personal.AU_Update_dt = DateTime.Now;

        if (!string.IsNullOrWhiteSpace(personal.AU_Password))
        {
            personal.AU_Password = ShInfoTech.Common.Security.Md5(personal.AU_Password);
        }

        if (personal.AU_Code > 0)
        {
            var cols = new string[] { 
                "AU_Update_dt",
                "AU_Name", 
                "AU_Username",
                "AU_Married",
                "AU_Dr_Lic", 
                "AU_Gender", 
                "AU_ID_Type", 
                "AU_ID_No",
                "AU_Education", 
                "AU_Type",
                "AU_UG_Code",
                "AU_Active_tag",
                //"AU_Occupation", 
                //"AU_Industry", 
                "AU_B_date" 
            };
            var listCols = cols.ToList<string>();
            if (!string.IsNullOrWhiteSpace(personal.AU_Password))
            {
                listCols.Add("AU_Password");
            }

            personal.Update(listCols);
        }
        else
        {
            au_code = (long)personal.Insert();
        }

        return au_code;
    }

    /// <summary>
    /// 保存经销商员工信息
    /// </summary>
    /// <param name="data"></param>
    /// <param name="au_code"></param>
    private void Save_Dealer_Emp(dynamic data, long au_code)
    {
        var s_personal = JsonConvert.SerializeObject(data.personal);
        if(string.IsNullOrWhiteSpace(s_personal))
        {
            return;
        }

        CT_All_User empl = JsonConvert.DeserializeObject<CT_All_User>(s_personal);
        CT_Dealer_Empl dealer_empl = JsonConvert.DeserializeObject<CT_Dealer_Empl>(s_personal);
        dealer_empl.DE_AU_Code = au_code;
        if (empl.AU_Active_tag == 1)
        {
            dealer_empl.DE_Activate_dt = DateTime.Now;
        }
        else
        {
            dealer_empl.DE_DActivate_dt = DateTime.Now;
        }
        if (!dealer_empl.DE_Ignore.Value )
        {
            dealer_empl.DE_Ignore = null;
        }


        dealer_empl.DE_Update_dt = DateTime.Now;
        if (dealer_empl.DE_Code > 0)
        {
            dealer_empl.Update(new string[]{
                //"DE_Type",
                "DE_ID"
                ,"DE_Ignore"
                ,"DE_Picture_FN"
                ,"DE_Vacation_St"
                ,"DE_Vacation_En"
                ,"DE_Activate_dt"
                ,"DE_DActivate_dt"
                ,"DE_Update_dt"
            });
        }
        else
        {
            if (!dealer_empl.DE_UType.HasValue)
            {
                dealer_empl.DE_UType = (byte)UserSession.User.UG_UType;
            }
            if (!dealer_empl.DE_AD_OM_Code.HasValue)
            {
                dealer_empl.DE_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
            }
            //switch (dealer_empl.DE_UType)
            //{
            //    case 1:
            //        dealer_empl.DE_AD_OM_Code = UserSession.Dealer.AD_Code;
            //        break;
            //    case 2:
            //        dealer_empl.DE_AD_OM_Code = UserSession.DealerGroup.DG_Code;
            //        break;
            //    case 3:
            //        dealer_empl.DE_AD_OM_Code = UserSession.OEM.OM_Code;
            //        break;
            //}
            dealer_empl.Insert();
        }

        if (dealer_empl.DE_Picture_FN != dealer_empl.DE_Picture_FN_Temp)
        {
            var path_save = Server.MapPath("~/images/Adviser/");
            //添加或更换图片
            if (!string.IsNullOrWhiteSpace(dealer_empl.DE_Picture_FN))
            {
                var path = System.Configuration.ConfigurationManager.AppSettings["PLUploadPath"];
                if (string.IsNullOrWhiteSpace(path))
                {
                    path = "~/plupload/";
                }
                path = Server.MapPath(path);
                var path_temp = path + "employee_temp/";
                if (!Directory.Exists(path_save))
                {
                    Directory.CreateDirectory(path_save);
                }
                if (File.Exists(path_temp + dealer_empl.DE_Picture_FN))
                {
                    File.Move(path_temp + dealer_empl.DE_Picture_FN, path_save + dealer_empl.DE_Picture_FN);
                }
            }

            if (!string.IsNullOrWhiteSpace(dealer_empl.DE_Picture_FN_Temp))
            {
                //删除图片
                var imgPath = path_save + dealer_empl.DE_Picture_FN_Temp;
                if (File.Exists(imgPath))
                {
                    File.Delete(imgPath);
                }
            }
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
    /// 保存日程安排
    /// </summary>
    /// <param name="data"></param>
    /// <param name="au_code"></param>
    private void Save_Schedule(dynamic data, long au_code)
    {
        var s_schedule = JsonConvert.SerializeObject(data.schedule);
        if (string.IsNullOrWhiteSpace(s_schedule)) 
        {
            return;
        }
        CT_Daily_PLanner daily = JsonConvert.DeserializeObject<CT_Daily_PLanner>(s_schedule);
        daily.DP_AU_AD_Code = au_code;
        daily.DP_Update_dt = DateTime.Now;
        if (CT_Daily_PLanner.Exists("DP_UType = @0 AND DP_AU_AD_Code = @1", daily.DP_UType, au_code))
        {
            var db = DBCRMTree.GetInstance();
            db.Update<CT_Daily_PLanner>(@"
                SET [DP_D1_AM_S] = @DP_D1_AM_S
                    ,[DP_D1_AM_E] = @DP_D1_AM_E
                    ,[DP_D1_PM_S] = @DP_D1_PM_S
                    ,[DP_D1_PM_E] = @DP_D1_PM_E
                    ,[DP_D2_AM_S] = @DP_D2_AM_S
                    ,[DP_D2_AM_E] = @DP_D2_AM_E
                    ,[DP_D2_PM_S] = @DP_D2_PM_S
                    ,[DP_D2_PM_E] = @DP_D2_PM_E
                    ,[DP_D3_AM_S] = @DP_D3_AM_S
                    ,[DP_D3_AM_E] = @DP_D3_AM_E
                    ,[DP_D3_PM_S] = @DP_D3_PM_S
                    ,[DP_D3_PM_E] = @DP_D3_PM_E
                    ,[DP_D4_AM_S] = @DP_D4_AM_S
                    ,[DP_D4_AM_E] = @DP_D4_AM_E
                    ,[DP_D4_PM_S] = @DP_D4_PM_S
                    ,[DP_D4_PM_E] = @DP_D4_PM_E
                    ,[DP_D5_AM_S] = @DP_D5_AM_S
                    ,[DP_D5_AM_E] = @DP_D5_AM_E
                    ,[DP_D5_PM_S] = @DP_D5_PM_S
                    ,[DP_D5_PM_E] = @DP_D5_PM_E
                    ,[DP_D6_AM_S] = @DP_D6_AM_S
                    ,[DP_D6_AM_E] = @DP_D6_AM_E
                    ,[DP_D6_PM_S] = @DP_D6_PM_S
                    ,[DP_D6_PM_E] = @DP_D6_PM_E
                    ,[DP_D7_AM_S] = @DP_D7_AM_S
                    ,[DP_D7_AM_E] = @DP_D7_AM_E
                    ,[DP_D7_PM_S] = @DP_D7_PM_S
                    ,[DP_D7_PM_E] = @DP_D7_PM_E
                    ,[DP_Update_dt] = @DP_Update_dt
                WHERE [DP_UType] = @DP_UType AND 
	                  [DP_AU_AD_Code] = @DP_AU_AD_Code", daily);
        }
        else
        {
            daily.Insert();
        }
    } 
    #endregion

    /// <summary>
    /// 获得日程安排
    /// </summary>
    /// <param name="DP_UType"></param>
    /// <param name="DP_AU_AD_Code"></param>
    private dynamic Get_Schedule(int DP_UType, int DP_AU_AD_Code)
    {
        var schedule = new CT_Daily_PLanner();
        if (DP_UType > 0 && DP_AU_AD_Code > 0)
        {
            schedule = CT_Daily_PLanner.SingleOrDefault(@"where [DP_UType] = @0 AND [DP_AU_AD_Code] = @1",
                DP_UType, DP_AU_AD_Code);
        }
        return schedule;
    }

    /// <summary>
    /// 获得员工信息
    /// </summary>
    /// <param name="data"></param>
    private void Get_EmployeeInfo(dynamic data)
    {
        dynamic o = new ExpandoObject();
        int Empl_Code = (int)data.Empl_Code;
        if (Empl_Code > 0)
        {
            //var empl = CT_Dealer_Empl.SingleOrDefault(Empl_Code);
            //o.dealerEmpl = new { 
            //    DE_Code = empl.DE_Code,
            //    DE_ID = empl.DE_ID,
            //    DE_Type = empl.DE_Type,
            //    DE_Picture_FN_Temp = empl.DE_Picture_FN, 
            //    DE_Picture_FN = empl.DE_Picture_FN 
            //};
            var empl = CT_Dealer_Empl.SingleOrDefault(Empl_Code);
            empl.EX_DE_Activate_dt = empl.DE_Activate_dt.HasValue ? empl.DE_Activate_dt.Value.ToString(Interna ? "MM/dd/yyyy" : "yyyy-MM-dd") : "";
            empl.EX_DE_Vacation_St = empl.DE_Vacation_St.HasValue ? empl.DE_Vacation_St.Value.ToString(Interna ? "MM/dd/yyyy" : "yyyy-MM-dd") : "";
            empl.EX_DE_Vacation_En = empl.DE_Vacation_En.HasValue ? empl.DE_Vacation_En.Value.ToString(Interna ? "MM/dd/yyyy" : "yyyy-MM-dd") : "";
            o.dealerEmpl = empl;
            var AU_Code = empl.DE_AU_Code;
            if (AU_Code > 0)
            {
                #region personal
                o.personal = CT_All_User.SingleOrDefault(@"select 
                AU_Code,
                AU_Active_tag,
                AU_Name,
                AU_Username,
                AU_Dr_Lic,
                AU_Gender,
                AU_ID_Type,
                AU_ID_No,
                AU_Education,
                AU_Married,
                AU_Type,
                AU_UG_Code,
                --AU_Occupation,
                --AU_Industry,
                AU_B_date 
                from CT_All_Users
                where AU_Code=@0", AU_Code);
                #endregion

                #region contacts
                o.contacts = new ExpandoObject();
                var db = DBCRMTree.GetInstance();
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
                sql = string.Format(@"Select ML_MC_Code,ML_Code,ML_Handle,ML_Pref as [pref]
                ,(SELECT {0} FROM [CT_Messaging_Carriers] where MC_Code=[ML_MC_Code]) as ML_MC_Code_Text
                from CT_Messaging_List 
                WHERE ML_AU_AD_Code=@0 and isnull(ML_Active,1) = 1
                ", sql_text_part);
                o.contacts.messaging = db.Query<dynamic>(sql, AU_Code);
                #endregion

                #region summary
                var dealer_code = 0;
                var user = HttpContext.Current.Session["PublicUser"] as MD_UserEntity;
                if (null != user && user.User.UG_UType == 1)
                {
                    dealer_code = user.DealerEmpl.DE_AD_OM_Code;
                }
                if (dealer_code > 0)
                {
                    db.EnableAutoSelect = false;
                    o.summary = db.SingleOrDefault<dynamic>(
                                "exec [CT_GetCustomerSummary] @Dealer_code,@User_code,@IsEn"
                                , new { Dealer_code = dealer_code, User_code = AU_Code, IsEn = Interna }
                                );
                }
                #endregion

                o.schedule = Get_Schedule((int)data.DP_UType, (int)AU_Code);
            }
        }
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