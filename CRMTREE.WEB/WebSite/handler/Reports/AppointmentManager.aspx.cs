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
using CRMTree.BLL;

public partial class handler_Reports_AppointmentManager : BasePage
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
                case "Save_Appointment":
                    Save_Appointment(data);
                    break;
                case "Save_Contact":
                    Save_Contact(data);
                    break;
                case "Save_Car":
                    Save_Car(data);
                    break;
                case "Save_CustomerByName":
                    Save_CustomerByName(data);
                    break;
                case "Get_Appointment":
                    Get_Appointment(data);
                    break;
                case "Get_FirstContact":
                    Get_FirstContact(data);
                    break;
                case "Get_Customer":
                    Get_Customer(data);
                    break;
                case "Get_Contacts":
                    Get_Contacts(data);
                    break;
                case "Get_CustomerCars":
                    Get_CustomerCars(data);
                    break;
                case "Get_Selects":
                    Get_Selects();
                    break;
                case "Get_CT_Service_Types":
                    Get_CT_Service_Types(data);
                    break;
                case "Get_Maintenance_Package":
                    Get_Maintenance_Package(data);
                    break;
                case "Get_MP_Mileage":
                    Get_MP_Mileage(data);
                    break;
                case "Get_CustomerName":
                    Get_CustomerName(Convert.ToInt32(data.AU_Code));
                    break;
                case "Get_Service_Dep":
                    Get_Service_Dep(data);
                    break;
                case "Get_Dealer_Advisor_All":
                    Get_Dealer_Advisor_All();
                    break;
                case "Get_Appt_Dealers":
                    Get_Appt_Dealers(data);
                    break;
                case "Get_Appt_Mileage":
                    Get_Appt_Dealers(data);
                    break;
                case "Get_Car_LendonMsg":
                    Get_Car_LendonMsg(data);
                    break;
                case "Get_Appt_Advisor":
                    Get_Appt_Advisor(data);
                    break;
                case "Get_Appt_Trans":
                    Get_Appt_Trans(data);
                    break;
                case "Get_Appt_GetDealerTime":
                    Get_Appt_GetDealerTime(data);
                    break;
                case "Get_Appt_GetAdvisorTime":
                    Get_Appt_GetAdvisorTime(data);
                    break;
            }
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void Get_Appt_GetDealerTime(dynamic data)
    {
        var o = Get_CT_Appt_GetDealerTime(data);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private IEnumerable<dynamic> Get_CT_Appt_GetDealerTime(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var sql = ";exec [P_Appt_GetDealerTime] @AD_Code";
        return db.Query<dynamic>(sql, new
        {
            AD_Code = data.AD_Code.Value
        });
    }

    private void Get_Appt_GetAdvisorTime(dynamic data)
    {
        var o = Get_CT_Appt_GetAdvisorTime(data);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private DataSet Get_CT_Appt_GetAdvisorTime(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var sql = ";exec [P_Appt_GetAdvisorTime] @AD_Code,@DE_Code,@Sel_Date,@Sel_Time,@Lang";
        var ds = new DataSet();
        db.Fill(ds,sql, new
        {
            AD_Code = data.AD_Code.Value,
            DE_Code = data.DE_Code.Value,
            Sel_Date = data.Sel_Date.Value,
            Sel_Time = data.Sel_Time.Value,
            Lang = Interna
        });
        return ds;
    }

    private void Get_Appt_Advisor(dynamic data)
    {
        var o = Get_CT_Appt_Advisor(data);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private IEnumerable<dynamic> Get_CT_Appt_Advisor(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var sql = ";exec [P_Appt_Advisor] @AD_Code,@Sel_Date,@Sel_Time,@App_Type,@CI_Code,@Lang";
        return db.Query<dynamic>(sql, new
        {
            AD_Code = UserSession.Dealer != null ? UserSession.Dealer.AD_Code : 0,
            Sel_Date = data.Sel_Date.Value,
            Sel_Time = data.Sel_Time.Value,
            App_Type = data.App_Type.Value,
            CI_Code = data.CI_Code.Value,
            Lang = Interna
        });
    }

    private void Get_Appt_Trans(dynamic data)
    {
        var o = Get_CT_Appt_Trans(data);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private IEnumerable<dynamic> Get_CT_Appt_Trans(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var sql = ";exec [P_Appt_Trans] @AD_Code,@Sel_Date,@Sel_Time,@App_Type,@AU_Code,@CI_Code,@Lang";
        return db.Query<dynamic>(sql, new
        {
            AD_Code = UserSession.Dealer != null ? UserSession.Dealer.AD_Code : 0,
            Sel_Date = data.Sel_Date.Value,
            Sel_Time = data.Sel_Time.Value,
            App_Type = data.App_Type.Value,
            CI_Code = data.CI_Code.Value,
            AU_Code = data.AU_Code.Value,
            Lang = Interna
        });
    }

    private void Get_Car_LendonMsg(dynamic data)
    {
        dynamic o = new ExpandoObject();
        o.MP_Mileage = Get_CT_MP_Mileage(data);
        o.Appt_Dealers = Get_CT_Appt_Dealers(data);
        o.Appt_Mileage = Get_CT_Appt_Mileage(data);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Appt_Mileage(dynamic data)
    {
        var o = Get_CT_Appt_Mileage(data);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private IEnumerable<dynamic> Get_CT_Appt_Mileage(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var sql = ";exec [P_Appt_GetMileage] @CI_Code";
        return db.Query<dynamic>(sql, new
        {
            CI_Code = (int)data.CI_Code
        });
    }

    private void Get_Appt_Dealers(dynamic data)
    {
        var o = Get_CT_Appt_Dealers(data);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private IEnumerable<dynamic> Get_CT_Appt_Dealers(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var sql = ";exec [P_Appt_GetDealers] @AD_Code,@DG_Code,@OM_Code,@AU_Code, @UG_Utype,@CI_Code,@IsEn";
        return db.Query<dynamic>(sql, new
        {
            AD_Code = UserSession.Dealer != null ? UserSession.Dealer.AD_Code : 0,
            DG_Code = UserSession.DealerGroup != null ? UserSession.DealerGroup.DG_Code : 0,
            OM_Code = UserSession.OEM != null ? UserSession.OEM.OM_Code : 0,
            AU_Code = (int)data.AU_Code,
            UG_Utype = UserSession.User.UG_UType,
            CI_Code = (int)data.CI_Code,
            IsEn = Interna
        });
    }

    private void Get_CustomerName(int AU_Code)
    {
        var au = CT_All_User.SingleOrDefault(AU_Code);
        Response.Write(au.AU_Name);
    }

    private void Get_MP_Mileage(dynamic data)
    {
        var o = Get_CT_MP_Mileage(data);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private IEnumerable<dynamic> Get_CT_MP_Mileage(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        return db.Query<dynamic>(
        ";exec [CT_Get_MP_Mileage] @Dealer_Code,@CI_Code,@IsEn"
        , new { Dealer_Code = UserSession.Dealer.AD_Code, CI_Code = (int)data.CI_Code, IsEn = Interna }
        );
    }

    private void Get_Maintenance_Package(dynamic data)
    {
        var ds = new DataSet();
        int MP_Code = (int)data.MP_Code;
        if (MP_Code > 0)
        {
            var db = DBCRMTree.GetInstance();
            db.Fill(ds,
                ";exec [CT_Get_Maintenance_Package] @MP_Code,@RS_Code,@CI_Code,@IsEn"
                , new { MP_Code = MP_Code, RS_Code = (int)data.RS_Code, CI_Code = (int)data.CI_Code, IsEn = Interna }
                );
        }

        Response.Write(JsonConvert.SerializeObject(ds));
    }

    private void Save_Car(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        try
        {
            using (var tran = db.GetTransaction())
            {
                var au_code = (long)data.au_code;
                var s = JsonConvert.SerializeObject(data.o);
                CT_Car_Inventory c = JsonConvert.DeserializeObject<CT_Car_Inventory>(s);
                var ci_code = c.CI_Code;
                c.CI_AU_Code = au_code;
                c.CI_Create_dt = DateTime.Now;
                if (c.CI_Code > 0)
                {
                    c.Update(new string[]{
                                "CI_AU_Code"
                                ,"CI_Create_dt"
                                ,"CI_CS_Code"
                                ,"CI_VIN"
                                ,"CI_Mileage"
                                ,"CI_Licence"
                                ,"CI_YR_Code"
                                ,"CI_Color_I"
                                ,"CI_Color_E"
                            });
                }
                else
                {
                    ci_code = (int)c.Insert();
                }

                CT_Auto_Insurance ai = JsonConvert.DeserializeObject<CT_Auto_Insurance>(s);
                ai.AI_CI_Code = c.CI_Code;
                if (db.Exists<CT_Auto_Insurance>("AI_CI_Code=@0", c.CI_Code))
                {
                    ai.AI_Update_dt = DateTime.Now;
                    string sql = @"UPDATE CT_Auto_Insurance
                    SET AI_IC_Code = @AI_IC_Code
                    ,AI_IA_Code = @AI_IA_Code
                    ,AI_Policy = @AI_Policy
                    ,AI_End_dt = @AI_End_dt
                    ,AI_Update_dt = @AI_Update_dt
                    WHERE AI_CI_Code=@AI_CI_Code";
                    db.Execute(sql, ai);
                }
                else
                {
                    ai.Insert();
                }

                var path = System.Configuration.ConfigurationManager.AppSettings["PLUploadPath"];
                if (string.IsNullOrWhiteSpace(path))
                {
                    path = "~/plupload/";
                }
                path = Server.MapPath(path);
                var path_save = path + "customer/";
                var path_temp = path + "customer_temp/";
                //添加图片
                if (!string.IsNullOrWhiteSpace(c.CP_Picture_FN))
                {
                    string[] imgs = c.CP_Picture_FN.Split(',');
                    foreach (var img in imgs)
                    {
                        if (!string.IsNullOrWhiteSpace(img))
                        {
                            var _CT_Car_Picture = new CT_Car_Picture();
                            _CT_Car_Picture.CP_CI_Code = ci_code;
                            _CT_Car_Picture.CP_Picture_FN = img;
                            _CT_Car_Picture.CP_Update_dt = DateTime.Now;
                            _CT_Car_Picture.Insert();
                            if (!Directory.Exists(path_save))
                            {
                                Directory.CreateDirectory(path_save);
                            }
                            if (File.Exists(path_temp + img))
                            {
                                File.Move(path_temp + img, path_save + img);
                            }
                        }
                    }
                }
                tran.Complete();
                Response.Write(JsonConvert.SerializeObject(new { CI_Code = ci_code, isOK = true }));
            }
        }
        catch (Exception ex)
        {
            db.AbortTransaction();
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }
    private void Save_CustomerByName(dynamic data)
    {
        CT_All_User user = new CT_All_User();
        user.AU_Name = (string)data.AU_Name;
        user.AU_Update_dt = DateTime.Now;
        user.AU_Active_tag = 1;
        
        var au_code = (long)user.Insert();

        Response.Write(JsonConvert.SerializeObject(new { AU_Code = au_code, AU_Name = user.AU_Name }));
    }

    /// <summary>
    /// 保存预约信息
    /// </summary>
    /// <param name="data"></param>
    private void Save_Appointment(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        try
        {
            var _AP_Code = -1;
            using (var tran = db.GetTransaction())
            {
                var s = JsonConvert.SerializeObject(data.o);
                CT_Appt_Service ap = JsonConvert.DeserializeObject<CT_Appt_Service>(s);
          
                var ap_code = ap.AP_Code;

                if (ap.AP_SC_Code == -1)
                {
                    ap.AP_SC_Code = null;
                }
                if (ap.AP_ST_Code == -1)
                {
                    ap.AP_ST_Code = null;
                }

                if (UserSession.User.UG_UType == 1)
                {
                    ap.AP_AD_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
                }
                else
                {
                    ap.AP_AD_Code = ap.AP_AD_Code.HasValue && ap.AP_AD_Code.Value > 0 ? ap.AP_AD_Code : UserSession.DealerEmpl.DE_AD_OM_Code;
                }
                if (ap.AP_Code > 0)
                {
                    ap.AP_Updated_by = UserSession.User.AU_Code;
                    ap.AP_Update_Dt = DateTime.Now;
                    ap.Update(new string[]{
                        //"AP_CI_Code" ,
                        "AP_AD_Code" ,
                        //"AP_Cont_Type" ,
                        //"AP_PL_ML_Code" ,
                        //"AP_SC_Code",
                        //"AP_ST_Code" ,
                        //"AP_MP_Code" ,
                        //"AP_PAM_Code" ,
                        "AP_Notification",
                        "AP_Time" ,
                        "AP_SA_Selected" ,
                        "AP_Transportation" ,
                        "AP_Updated_by" ,
                        "AP_Update_Dt"
                    });
                }
                else
                {
                    var ug_code = UserSession.User.AU_UG_Code;
                    if (ug_code >= 10)
                    {
                        ap.AP_PAM_Code = 1;
                    }
                    else if (ug_code == 1)
                    {
                        ap.AP_PAM_Code = 2;
                    }
                    else if (ug_code == 3)
                    {
                        ap.AP_PAM_Code = 3;
                    }
                    else if (ug_code == 4)
                    {
                        ap.AP_PAM_Code = 4;
                    }
                    else if (ug_code == 5)
                    {
                        ap.AP_PAM_Code = 5;
                    }

                    if (ap.AP_SC_Code == 1)
                    {
                        ap.AP_MP_Code = ap.AP_ST_Code;
                    }

                    ap.AP_Created_by = UserSession.User.AU_Code;
                    ap.AP_Update_Dt = DateTime.Now;
                    ap_code = (int)ap.Insert();
                }

               
                if (ap_code > 0)
                {
                    string note = data.o.SN_Note;
                    CT_Service_Note n = new CT_Service_Note();
                    n.SN_AP_Code = ap_code;
                    n.SN_Note = note;
                    if (CT_Service_Note.Exists("SN_AP_Code = @0 AND ISNULL(SN_Cat,0) = 0", ap_code))
                    {
                        db.Update<CT_Service_Note>("SET [SN_Note] = @SN_Note WHERE SN_AP_Code=@SN_AP_Code AND ISNULL(SN_Cat,0) = 0", n);
                        //db.Update(@"UPDATE [CT_Service_Notes] set [SN_Note] = @SN_Note WHERE SN_AP_Code=@SN_AP_Code", n);
                    }
                    else if(!string.IsNullOrWhiteSpace(n.SN_Note))
                    {
                        n.Insert();
                    }

                    string generalNote = data.o.EX_GeneralNote;
                    CT_Service_Note gn = new CT_Service_Note();
                    gn.SN_AP_Code = ap_code;
                    gn.SN_Note = generalNote;
                    gn.SN_Cat = 1;
                    if (CT_Service_Note.Exists("SN_AP_Code = @0 AND SN_Cat = 1", ap_code))
                    {
                        db.Update<CT_Service_Note>("SET [SN_Note] = @SN_Note,SN_Cat=1 WHERE SN_AP_Code=@SN_AP_Code AND SN_Cat = 1", gn);
                    }
                    else if (!string.IsNullOrWhiteSpace(gn.SN_Note))
                    {
                        gn.Insert();
                    }
                }
                tran.Complete();
                _AP_Code = ap_code;
            }
            Response.Write(JsonConvert.SerializeObject(new { isOK = true, AP_Code = _AP_Code }));
        }
        catch (Exception ex)
        {
            db.AbortTransaction();
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void Save_Contact(dynamic data)
    {
        var au_code = (long)data.au_code;
        var s_contact = JsonConvert.SerializeObject(data.o);
        EX_CT_Contacts c = JsonConvert.DeserializeObject<EX_CT_Contacts>(s_contact);

        var s = JsonConvert.SerializeObject(c.o);

        var code = 0;
        switch (c.type)
        {
            //Phone
            case 1:
                var phone = JsonConvert.DeserializeObject<CT_Phone_List>(s);
                phone.PL_AU_AD_Code = au_code;
                phone.PL_Pref = c.pref;
                phone.PL_UType = 1;
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
                    code = (int)phone.Insert();
                }
                
                break;
            //Messaging
            case 2:
                var msg = JsonConvert.DeserializeObject<CT_Messaging_List>(s);
                msg.ML_AU_AD_Code = au_code;
                msg.ML_Pref = c.pref;
                msg.ML_UType = 1;
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
                    code = (int)msg.Insert();
                }
                break;
            //EMail
            case 3:
                var email = JsonConvert.DeserializeObject<CT_Email_List>(s);
                email.EL_AU_AD_Code = au_code;
                email.EL_UType = 1;
                email.EL_Pref = c.pref;
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
                    code = (int)email.Insert();
                }
                break;
        }
        Response.Write(JsonConvert.SerializeObject(new { code = code }));
    }

    private void Get_Contacts(dynamic data)
    {
        var AU_Code = (int)data.AU_Code;

        var db = DBCRMTree.GetInstance();

        #region contacts
        dynamic contacts = new ExpandoObject();

        var sql_text_part = Interna ? "[text_en]" : "[text_cn]";

        var sql = string.Format(@"Select PL_Code,PL_Type,PL_Number,PL_pref as [pref] 
,(select {0} from words where p_id = 47 and [value] = pl_type) as PL_Type_Text
from CT_Phone_List 
WHERE PL_AU_AD_Code=@0 and PL_Utype = 1 and isnull(PL_Active,1) = 1
                ", sql_text_part);
        contacts.phone = db.Query<dynamic>(sql, AU_Code);

        sql = string.Format(@"Select EL_Code,EL_Type,EL_Address,EL_Pref as [pref]
,(select {0} from words where p_id = 44 and [value] = el_type) as EL_Type_Text
from CT_eMail_List 
WHERE EL_AU_AD_Code=@0 and EL_Utype = 1 and isnull(EL_Active,1) = 1
                ", sql_text_part);
        contacts.email = db.Query<dynamic>(sql, AU_Code);

        sql_text_part = Interna ? "[MC_Name_EN]" : "[MC_Name_CN]";
        sql = string.Format(@"Select ML_MC_Code,ML_Code,ML_Handle,ML_Pref as [pref]
,(SELECT {0} FROM [CT_Messaging_Carriers] where MC_Code=[ML_MC_Code]) as ML_MC_Code_Text
from CT_Messaging_List 
WHERE ML_AU_AD_Code=@0 and ML_Utype = 1 and isnull(ML_Active,1) = 1
                ", sql_text_part);
        contacts.messaging = db.Query<dynamic>(sql, AU_Code);
        #endregion

        Response.Write(JsonConvert.SerializeObject(contacts));
    }

    private void Get_FirstContact(dynamic data)
    {
        var AU_Code = (int)data.AU_Code;
        var db = DBCRMTree.GetInstance();

        #region contacts
        var sql_text_part = Interna ? "[text_en]" : "[text_cn]";
        var sql_text_part_msg = Interna ? "[MC_Name_EN]" : "[MC_Name_CN]";
        var sql = string.Format(@"
    select top 1 * from (
    Select 
    1 as AP_Cont_Type,
    PL_Pref AS pref,
    PL_Code as AP_PL_ML_Code,
    isnull((select {0} from words where p_id = 47 and [value] = pl_type),'') + ' : ' + PL_Number as AP_PL_ML_Code_Text
    from CT_Phone_List 
    WHERE PL_AU_AD_Code=@0 and isNull(PL_UType,1)=1  and isnull(PL_Active,1) = 1

    union all

    Select 
    2 as AP_Cont_Type,
    ML_Pref AS pref,
    ML_Code as AP_PL_ML_Code,
    isnull((SELECT {1} FROM [CT_Messaging_Carriers] where MC_Code=[ML_MC_Code]),'') + ' : ' + ML_Handle as AP_PL_ML_Code_Text
    from CT_Messaging_List 
    WHERE ML_AU_AD_Code=@0 and isNull(ML_UType,1)=1  and isnull(ML_Active,1) = 1

    union all

    Select 
    3 AP_Cont_Type,
    EL_Pref AS pref,
    EL_Code as AP_PL_ML_Code,
    isnull((select {0} from words where p_id = 44 and [value] = el_type),'') + ' : ' +  EL_Address as AP_PL_ML_Code_Text
    from CT_eMail_List 
    WHERE EL_AU_AD_Code=@0 and isNull(EL_UType,1)=1  and isnull(EL_Active,1) = 1
)t
ORDER BY pref
                ", sql_text_part, sql_text_part_msg);
        var contact = db.Query<dynamic>(sql, AU_Code);
        #endregion

        Response.Write(JsonConvert.SerializeObject(contact));
    }

    private void Get_CustomerCars(dynamic data)
    {
        var AU_Code = (int)data.AU_Code;
        
        var db = DBCRMTree.GetInstance();
        var sql = ";exec [CT_GetCustomerCars] @User_code,@IsEn";
        var o = db.Query<dynamic>(sql, new { User_code = AU_Code, IsEn = Interna });
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Selects()
    {
        dynamic o = new ExpandoObject();
        //o.CT_Transportation = Get_CT_Transportation();
        o.CT_Serv_Category = Get_CT_Serv_Category();
        //o.Dealer_Advisor = Get_Dealer_Advisor();

        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 获得运输类型
    /// </summary>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Transportation()
    {
        var db = DBCRMTree.GetInstance();
        var text = Interna ? "[PTP_Desc_EN]" : "[PTP_Desc_CN]";
        var sql = string.Format(@"SELECT [PTP_Code] as [value],{0} as [text] FROM [CT_Transportation]", text);
        return db.Query<dynamic>(sql);
    }

    /// <summary>
    /// 获得服务类别
    /// </summary>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Serv_Category()
    {
        var db = DBCRMTree.GetInstance();
        var text = Interna ? "[SC_Desc_EN]" : "[SC_Desc_CN]";
        var sql = string.Format(@"SELECT [SC_Code] as [value],{0} as [text] FROM [CT_Serv_Category] where SC_AD_Code={1}", text, UserSession.DealerEmpl.DE_AD_OM_Code);
        return db.Query<dynamic>(sql);
    }

    /// <summary>
    /// 获得服务类型
    /// </summary>
    /// <returns></returns>
    private void Get_CT_Service_Types(dynamic data)
    {
        var ST_SC_Code = (int)data.id;
        var db = DBCRMTree.GetInstance();
        var text = Interna ? "[ST_Desc_EN]" : "[ST_Desc_CN]";
        var sql = string.Format(@"SELECT [ST_Code] as [value],{0} as [text] FROM [CT_Service_Types]", text);
        var o = db.Query<dynamic>(sql + " where ST_SC_Code=@0", ST_SC_Code);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Dealer_Advisor_All()
    {
        var db = DBCRMTree.GetInstance();
        var sql = BL_Reports.GetReportSql(131, null);
        var o = db.Query<dynamic>(sql);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 获得经销商顾问
    /// </summary>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_Dealer_Advisor()
    {
        var db = DBCRMTree.GetInstance();
        var text = Interna ? "[ST_Desc_EN]" : "[ST_Desc_CN]";
        var sql = string.Format(@"
        SELECT	DE_Code as [value],
                DE_Picture_FN ,
                AU_Name as [text]
        FROM    CT_Dealer_Empl DE
                INNER JOIN CT_All_Users AU ON DE.DE_AU_Code = AU.AU_Code
        WHERE   DE_UType = 1
                AND DE_AD_OM_Code = @0
                And AU_UG_Code in (26)
                AND isnull(DE_Activate_dt,'')!='' AND isnull(DE_DActivate_dt,'')=''
        ", text);
        return db.Query<dynamic>(sql, UserSession.DealerEmpl.DE_AD_OM_Code);
    }

    /// <summary>
    /// 获得预约信息
    /// </summary>
    /// <param name="data"></param>
    private void Get_Appointment(dynamic data)
    {
        int AP_Code = (int)data.AP_Code;

        dynamic d = new ExpandoObject();

        var o = CT_Appt_Service.SingleOrDefault(AP_Code);
        if (null != o.AP_Time && o.AP_Time.HasValue)
        {
            o.EX_AP_Date = o.AP_Time.Value.ToString(Interna ? "MM/dd/yyyy" : "yyyy-MM-dd");
            o.EX_AP_Time = o.AP_Time.Value.ToString("HH:mm");
        }

        var db = DBCRMTree.GetInstance();

        int PTP_Code = (int)o.AP_Transportation;
        int AD_Code = (int)o.AP_AD_Code;
        o.AU_Name = db.ExecuteScalar<string>(string.Format(@"
SELECT  au.AU_Name 
FROM    CT_Appt_Service [ap]
        INNER JOIN CT_All_Users [au] ON ap.AP_AU_Code = au.AU_Code
WHERE   AP_Code = @0",Interna ? "_en":"_cn"),AP_Code);

        o.SN_Note = db.ExecuteScalar<string>(@"
SELECT  SN_Note
FROM    CT_Service_Notes [sn]
WHERE   SN_AP_Code = @0 and isnull(SN_Cat,0)=0", AP_Code);

        o.AP_SA_Selected_text = db.ExecuteScalar<string>(@"
SELECT  au.AU_Name 
FROM    CT_Appt_Service [ap]
        left join CT_Dealer_Empl [de] on ap.AP_SA_Selected=de.DE_Code
        INNER JOIN CT_All_Users [au] ON de.DE_AU_Code = au.AU_Code
WHERE   AP_Code = @0",AP_Code);

        o.AP_AD_Code_text = db.ExecuteScalar<string>(string.Format(@"
SELECT  AD_Name{0}
FROM   CT_Auto_Dealers 
where AD_Code=@0", Interna ? "_en" : "_cn"), AD_Code);

        o.AP_Transportation_text = db.ExecuteScalar<string>(string.Format(@"
SELECT  PTP_Desc{0}
FROM   CT_Transportation 
where PTP_Code=@0",Interna ? "_en":"_cn"), PTP_Code);

        
        o.EX_GeneralNote = db.ExecuteScalar<string>(@"
SELECT  SN_Note
FROM    CT_Service_Notes [sn]
WHERE   SN_AP_Code = @0 and SN_Cat = 1", AP_Code);

        o.AP_PL_ML_Code_Text = db.ExecuteScalar<string>(string.Format(@"
SELECT  CASE WHEN AP_Cont_Type = 1
                  AND AP_PL_ML_Code > 0
             THEN ( SELECT  w.text{0} + ' : ' + pl.PL_Number
                    FROM    CT_Phone_List pl
                           INNER JOIN words w ON isnull(pl.PL_Type,'') = isnull(w.value,'')
                                                   AND pl.PL_Code = ap.AP_PL_ML_Code
                                                  AND w.p_id = 47
                  )
             WHEN AP_Cont_Type = 2
                  AND AP_PL_ML_Code > 0
             THEN ( SELECT  mc.MC_Name{0} + ' : ' + ml.ML_Handle
                    FROM    CT_Messaging_List ml
                           INNER JOIN CT_Messaging_Carriers mc ON isnull(mc.MC_Code,'') = isnull(ml.ML_MC_Code,'')
                                                              AND ml.ML_Code = ap.AP_PL_ML_Code
                  )
             WHEN AP_Cont_Type = 3
                  AND AP_PL_ML_Code > 0
             THEN ( SELECT  w.text{0} + ' : ' + el.EL_Address
                    FROM    CT_Email_List el
                            INNER JOIN words w ON isnull(el.EL_Type,'')= isnull(w.value,'')
                                                  AND el.EL_Code = ap.AP_PL_ML_Code
                                                  AND w.p_id = 44
                  )
        END AS AP_PL_ML_Code_Text
FROM    CT_Appt_Service [ap]
        INNER JOIN CT_All_Users [au] ON ap.AP_AU_Code = au.AU_Code
WHERE   AP_Code = @0", Interna ? "_en" : "_cn"), AP_Code);

        d.o = o;

        d.service = Get_CT_Service_Dep(data);

        Response.Write(JsonConvert.SerializeObject(d));
    }

    private void Get_Service_Dep(dynamic data)
    {
        var o = Get_CT_Service_Dep(data);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private CT_Service_Dep Get_CT_Service_Dep(dynamic data)
    {
        return CT_Service_Dep.SingleOrDefault(@"where SD_AD_Code = @0 AND SD_PDN_Code = @1",
            UserSession.DealerEmpl.DE_AD_OM_Code,
            (int)data.SD_PDN_Code);
    }

    /// <summary>
    /// 获得简要客户信息，供筛选用
    /// </summary>
    /// <param name="data"></param>
    private void Get_Customer(dynamic data)
    {
        var q = (string)data.q;
        if (string.IsNullOrWhiteSpace(q))
        {
            return;
        }
        CRMTree.Function fun = new CRMTree.Function();
        var sql = fun.getReprotQuery(44);
        var db = DBCRMTree.GetInstance();
        var items = db.Query<dynamic>(sql,q);
        Response.Write(JsonConvert.SerializeObject(items));
    }
}