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
using ShInfoTech.Common;
using CRMTree.BLL.Wechat;
using System.Configuration;
using CRMTree.BLL;

public partial class handler_Reports_CustomerManage : BasePage
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
                case "Get_contact_value_default":
                    Get_contact_value_default(data);
                    break;
                case "Get_contact_value":
                    Get_contact_value(data);
                    break;
                case "Send_Msg_Phone":
                    Send_Msg_Phone(data);
                    break;
                case "Send_Msg":
                    Send_Msg(data);
                    break;
                case "Send_Email":
                    Send_Email(data);
                    break;
                case "Save_Customer":
                    Save_Customer(data);
                    break;
                case "Save_Profile":
                    Save_Profile(data);
                    break;
                case "Get_CustomerInfo":
                    Get_CustomerInfo(data);
                    break;
                case "Get_Messaging_Carriers":
                    Get_Messaging_Carriers();
                    break;
                case "Get_Car_InventoryAndLendon":
                    Get_Car_InventoryAndLendon(Convert.ToInt32(data.id), Convert.ToInt32(data.CI_CS_Code));
                    break;
                case "Get_Car_Selects":
                    Get_Car_Selects();
                    break;
                case "Get_Years":
                    Get_Years();
                    break;
                case "Get_Make":
                    Get_Make();
                    break;
                case "Get_Car_Model_All":
                    Get_Car_Model_All(Convert.ToInt32(data.id));
                    break;
                case "Get_Car_Style_All":
                    Get_Car_Style_All(Convert.ToInt32(data.id));
                    break;
                case "Get_Car_Model":
                    Get_Car_Model(Convert.ToInt32(data.id), Convert.ToInt32(data.id_year));
                    break;
                case "Get_Car_Style":
                    Get_Car_Style(Convert.ToInt32(data.id), Convert.ToInt32(data.id_year));
                    break;
                case "Get_Insurance_Agents":
                    Get_Insurance_Agents(Convert.ToInt32(data.id));
                    break;
                case "Get_Car_Lendon":
                    Get_Car_Lendon(Convert.ToInt32(data.id), Convert.ToInt32(data.id_year));
                    break;
                case "Get_Recursion_By_CS_Code":
                    Get_Recursion_By_CS_Code(Convert.ToInt32(data.id));
                    break;
                case "Get_Recursion_By_CM_Code":
                    Get_Recursion_By_CM_Code(Convert.ToInt32(data.id));
                    break;
                case "Get_Driver":
                    Get_Driver(Convert.ToInt32(data.DL_AU_Code));
                    break;
                case "Get_PhoneList":
                    Get_PhoneList(Convert.ToInt32(data.PL_AU_AD_Code));
                    break;
                case "Get_Periodicals":
                    Get_Periodicals();
                    break;
            }
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void Send_Email(dynamic data)
    {
        try
        {
            var s_email = JsonConvert.SerializeObject(data.email);
            EX_Email email = JsonConvert.DeserializeObject<EX_Email>(s_email);

            string supportMailAccont = ConfigurationManager.AppSettings["suppotMailAccount"];
            string suppotMailPwd = ConfigurationManager.AppSettings["suppotMailPwd"];
            string mailServer = ConfigurationManager.AppSettings["mailServer"];
            string displayName = ConfigurationManager.AppSettings["displayName"];

            Mail.SendMail(supportMailAccont,
            suppotMailPwd,
            new List<string>(new string[] { email.EX_ToEmail }),
            null,
            null,
            email.EX_Subject,
            supportMailAccont,
            displayName,
            mailServer,
            email.EX_Body);
            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void Send_Msg(dynamic data)
    {
        try
        {
            MD_UserEntity _user = BL_UserEntity.GetUserInfo();
            var s_msg = JsonConvert.SerializeObject(data.msg);
            EX_Message msg = JsonConvert.DeserializeObject<EX_Message>(s_msg);

            var w = CT_Wechat_Member.SingleOrDefault("where MB_UserName=@0", msg.EX_ToMsg);
            if (null != w && !string.IsNullOrWhiteSpace(w.MB_OpenID))
            {
                wechatHandle.SendCustom_text(w.MB_OpenID, _user.User.AU_Name + "：" + msg.EX_Msg);
            }
            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void Send_Msg_Phone(dynamic data)
    {
        try
        {
            MD_UserEntity _user = BL_UserEntity.GetUserInfo();
            var s_msg = JsonConvert.SerializeObject(data.msg);
            EX_Message msg = JsonConvert.DeserializeObject<EX_Message>(s_msg);
            SendMessage.SendReminder(msg.EX_ToMsg, msg.EX_Msg);

            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void Get_PhoneList(int PL_AU_AD_Code)
    {
        var db = DBCRMTree.GetInstance();
        var o = db.Query<dynamic>(
        ";exec P_Get_PhoneList @PL_AU_AD_Code,@IsEn"
        , new { PL_AU_AD_Code = PL_AU_AD_Code, IsEn = Interna }
        );
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Periodicals()
    {
        var db = DBCRMTree.GetInstance();

        var o = db.Query<dynamic>(string.Format(@"SELECT value, text_{0} text 
        FROM words  Where p_id = @0", Interna ? "en" : "cn"), 4213);

        Response.Write(JsonConvert.SerializeObject(o));
    }
    private void Get_PeriodicalsText(int id)
    {
        var db = DBCRMTree.GetInstance();

        var o = db.Query<dynamic>(string.Format(@"SELECT , text_{0} text 
        FROM words  Where p_id = @0 and value=@1", Interna ? "en" : "cn"), 4213,id);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Recursion_By_CM_Code(int id)
    {
        var db = DBCRMTree.GetInstance();

        var o = db.SingleOrDefault<dynamic>(@"SELECT top 1
        YR_Code,
        CM_MK_Code AS MK_Code,
        CM_Code
        FROM CT_Car_Model
        LEFT JOIN CT_Car_Style
        ON CS_CM_Code = CM_Code
        LEFT JOIN CT_Years 
        ON YR_Year = CS_Year
        WHERE CM_Code=@0", id);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Recursion_By_CS_Code(int id)
    {
        var db = DBCRMTree.GetInstance();

        var o = db.SingleOrDefault<dynamic>(@"SELECT top 1
        YR_Code,
        CM_MK_Code AS MK_Code,
        CS_CM_Code AS CM_Code,
        CS_Code
        FROM CT_Car_Style
        LEFT JOIN CT_Car_Model
        ON CM_Code=CS_CM_Code
        LEFT JOIN CT_Years 
        ON YR_Year = CS_Year
        WHERE CS_Code=@0", id);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    #region 保存客户信息
    private void Save_Profile(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        try
        {
            using (var tran = db.GetTransaction())
            {
                var au_code = Save_Personal(data);

                if (au_code > 0)
                {
                    Save_Contacts(data, au_code);
                }
                else
                {
                    throw new Exception(Interna ? "Save Fail!" : "保存失败!");
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
    /// 保存客户信息
    /// </summary>
    /// <param name="data"></param>
    private void Save_Customer(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        try
        {
            using (var tran = db.GetTransaction())
            {
                var au_code = Save_Personal(data);

                if (au_code > 0)
                {
                    Save_Relation_Contacts(data.contacts, Convert.ToInt32(au_code));
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
        long au_code = personal.AU_Code;
        personal.AU_Update_dt = DateTime.Now;

        if (personal.AU_Code > 0)
        {
            var cols = new string[]{
            "AU_Update_dt",
            "AU_Name", 
            "AU_Dr_Lic", 
            "AU_Gender", 
            "AU_ID_Type", 
            "AU_ID_No",
            "AU_Education", 
            "AU_Occupation", 
            "AU_Industry", 
            "AU_B_date" ,
            "AU_Type"
            };
            var listCols = cols.ToList<string>();
            if (!string.IsNullOrWhiteSpace(personal.AU_Password))
            {
                personal.AU_Password = ShInfoTech.Common.Security.Md5(personal.AU_Password);
                listCols.Add("AU_Password");
            }
            if (!string.IsNullOrWhiteSpace(personal.AU_Username))
            {
                listCols.Add("AU_Username");
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

    private void Save_Relation_Contacts(dynamic data,int m_au_code)
    {
        var _insertCount = data.insert.Count;
        var _updateCount = data.update.Count;
        var _deletesCount = data.deletes.Count;
        var _rowCount = data.row.Count;
        if (_insertCount > 0)
        {
            Insert_Relation_Contacts(data.insert, m_au_code);
        }
        if (_updateCount > 0)
        {
            Update_Relation_Contacts(data.update);
        }
        if (_deletesCount > 0)
        {
            Delete_Relation_Contacts(data.deletes);
        }
        if (_rowCount > 0)
        {
            Sort_Relation_Contacts(data.row);
        }
      
    }
    private void Sort_Relation_Contacts(dynamic sortData)
    {
        for (int i = 0; i < sortData.Count; i++)
        {
            CT_Relation_User _u = JsonConvert.DeserializeObject<CT_Relation_User>(JsonConvert.SerializeObject(sortData[i]));
            if (_u.relation_type == "1")
            {
                CT_Address_List address = new CT_Address_List();
                address.AL_Pref =Convert.ToByte(i+1);
                address.AL_Code = Convert.ToInt32(_u.Keys);
                address.Update(new string[]{"AL_Pref"});
            }
            else if (_u.relation_type == "2") {
                CT_Phone_List phone = new CT_Phone_List();
                phone.PL_Pref = Convert.ToByte(i + 1);
                phone.PL_Code = Convert.ToInt32(_u.Keys);
                phone.Update(new string[] {"PL_Pref" });
            }
            else if (_u.relation_type == "3") {
                CT_Email_List email = new CT_Email_List();
                email.EL_Pref = Convert.ToByte(i + 1);
                email.EL_Code = Convert.ToInt32(_u.Keys);
                email.Update(new string[] { "EL_Pref" });
            }
            else if (_u.relation_type == "4") {
                CT_Messaging_List msg = new CT_Messaging_List();
                msg.ML_Pref = Convert.ToByte(i + 1);
                msg.ML_Code = Convert.ToInt32(_u.Keys);
                msg.Update(new string[] { "ML_Pref" });
            }
        }
    }
    private void Insert_Relation_Contacts(dynamic insert, int m_au_code)
    {
        for (int i = 0; i < insert.Count; i++)
        {
            byte pref = 1;
            CT_Relation_User _u = JsonConvert.DeserializeObject<CT_Relation_User>(JsonConvert.SerializeObject(insert[i]));
            if (_u.relation_type == "1")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Address_List address = new CT_Address_List();
                address.AL_AU_AD_Code = _u.AU_Code;
                address.AL_UType = 1;
                address.AL_Pref = pref++;
                address.AL_Update_dt = DateTime.Now;
                address.AL_Active = true;
                address.AL_Type = _c.AL_Type;
                address.AL_Add1 = _c.AL_Add1;
                address.AL_Add2 = _c.AL_Add2;
                address.AL_District = _c.AL_District;
                address.AL_City = _c.AL_City;
                address.AL_State = _c.AL_State;
                address.AL_Zip = _c.AL_Zip;

                var _code =address.Insert();
                if(Convert.ToInt32(_code)>0 && _u.relation_id!=0){
                    CT_Assc_Contact _ac = new CT_Assc_Contact();
                    _ac.AC_M_AU_Code = m_au_code;
                    _ac.AC_AU_Code = _u.AU_Code;
                    _ac.AC_UType = 1;
                    _ac.AC_PL_ML_Code = Convert.ToInt32(_code);
                    _ac.AC_Deactivate_dt = DateTime.Now;
                    _ac.AC_Update_dt = DateTime.Now;
                    _ac.Insert();
                }
            }
            else if (_u.relation_type == "2")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Phone_List phone = new CT_Phone_List();
                phone.PL_AU_AD_Code = _u.AU_Code;
                phone.PL_UType = 1;
                phone.PL_Pref = pref++;
                phone.PL_Update_dt = DateTime.Now;
                phone.PL_Active = true;

                phone.PL_Type = _c.PL_Type;
                phone.PL_PP_Code = _c.PL_PP_Code;
                phone.PL_Number = _c.PL_Number;

                var _code = phone.Insert();
                if (Convert.ToInt32(_code) > 0 && _u.relation_id != 0)
                {
                    CT_Assc_Contact _ac = new CT_Assc_Contact();
                    _ac.AC_M_AU_Code = m_au_code;
                    _ac.AC_AU_Code = _u.AU_Code;
                    _ac.AC_UType = 2;
                    _ac.AC_PL_ML_Code = Convert.ToInt32(_code);
                    _ac.AC_Deactivate_dt = DateTime.Now;
                    _ac.AC_Update_dt = DateTime.Now;
                    _ac.Insert();
                }
            }
            else if (_u.relation_type == "3")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Email_List email = new CT_Email_List();
                email.EL_AU_AD_Code = _u.AU_Code;
                email.EL_UType = 1;
                email.EL_Pref = pref++;
                email.EL_Update_dt = DateTime.Now;
                email.EL_Active = true;

                email.EL_Type = _c.EL_Type;
                email.EL_Address = _c.EL_Address;

                var _code = email.Insert();
                if (Convert.ToInt32(_code) > 0 && _u.relation_id != 0)
                {
                    CT_Assc_Contact _ac = new CT_Assc_Contact();
                    _ac.AC_M_AU_Code = m_au_code;
                    _ac.AC_AU_Code = _u.AU_Code;
                    _ac.AC_UType = 3;
                    _ac.AC_PL_ML_Code = Convert.ToInt32(_code);
                    _ac.AC_Deactivate_dt = DateTime.Now;
                    _ac.AC_Update_dt = DateTime.Now;
                    _ac.Insert();
                }
            }
            else if (_u.relation_type == "4")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Messaging_List msg = new CT_Messaging_List();
                msg.ML_AU_AD_Code = _u.AU_Code;
                msg.ML_UType = 1;
                msg.ML_Pref = pref++;
                msg.ML_Update_dt = DateTime.Now;
                msg.ML_Active = true;

                msg.ML_Type = _c.ML_Type;
                msg.ML_Handle = _c.ML_Handle;
                var _code = msg.Insert();
                if (Convert.ToInt32(_code) > 0 && _u.relation_id != 0)
                {
                    CT_Assc_Contact _ac = new CT_Assc_Contact();
                    _ac.AC_M_AU_Code = m_au_code;
                    _ac.AC_AU_Code = _u.AU_Code;
                    _ac.AC_UType = 4;
                    _ac.AC_PL_ML_Code = Convert.ToInt32(_code);
                    _ac.AC_Deactivate_dt = DateTime.Now;
                    _ac.AC_Update_dt = DateTime.Now;
                    _ac.Insert();
                }
            }
        }
    }
    private void Update_Relation_Contacts(dynamic update)
    {
        for (int i = 0; i < update.Count; i++)
        {
            CT_Relation_User _u = JsonConvert.DeserializeObject<CT_Relation_User>(JsonConvert.SerializeObject(update[i]));
            if (_u.relation_type == "1")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Address_List address = new CT_Address_List();
                address.AL_AU_AD_Code = _u.AU_Code;
                address.AL_Update_dt = DateTime.Now;
                address.AL_Active = true;
                address.AL_Type = _c.AL_Type;
                address.AL_Add1 = _c.AL_Add1;
                address.AL_Add2 = _c.AL_Add2;
                address.AL_District = _c.AL_District;
                address.AL_City = _c.AL_City;
                address.AL_State = _c.AL_State;
                address.AL_Zip = _c.AL_Zip;
                if (_u.Keys > 0)
                {
                    address.AL_Code = Convert.ToInt32(_u.Keys);
                    var ii = address.Update();
                }
                //else
                //{
                //    address.Insert();
                //}
            }
            else if (_u.relation_type == "2")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Phone_List phone = new CT_Phone_List();
                phone.PL_AU_AD_Code = _u.AU_Code;
                phone.PL_Update_dt = DateTime.Now;
                phone.PL_Active = true;
                phone.PL_Type = _c.PL_Type;
                phone.PL_PP_Code = _c.PL_PP_Code;
                phone.PL_Number = _c.PL_Number;
                if (_u.Keys > 0)
                {
                    phone.PL_Code = Convert.ToInt32(_u.Keys);
                    phone.Update();
                }
                //else
                //{
                //    phone.Insert();
                //}
            }
            else if (_u.relation_type == "3")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Email_List email = new CT_Email_List();
                email.EL_AU_AD_Code = _u.AU_Code;
                email.EL_Update_dt = DateTime.Now;
                email.EL_Active = true;
                email.EL_Type = _c.EL_Type;
                email.EL_Address = _c.EL_Address;
                if (_u.Keys > 0)
                {
                    email.EL_Code = Convert.ToInt32(_u.Keys);
                    email.Update();
                }
                //else
                //{
                //    email.Insert();
                //}
            }
            else if (_u.relation_type == "4")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Messaging_List msg = new CT_Messaging_List();
                msg.ML_AU_AD_Code = _u.AU_Code;
                msg.ML_Update_dt = DateTime.Now;
                msg.ML_Active = true;
                msg.ML_Type = _c.ML_Type;
                msg.ML_Handle = _c.ML_Handle;
                if (_u.Keys > 0)
                {
                    msg.ML_Code = Convert.ToInt32(_u.Keys);
                    msg.Update();
                }
                //else
                //{
                //    msg.Insert();
                //}
            }
        }
    }
    private void Delete_Relation_Contacts(dynamic Delete)
    {
        for (int i = 0; i < Delete.Count; i++)
        {
            CT_Relation_User _u = JsonConvert.DeserializeObject<CT_Relation_User>(JsonConvert.SerializeObject(Delete[i]));
            if (_u.relation_type == "1")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Address_List address = new CT_Address_List();
                if (_u.Keys > 0)
                {
                    address.AL_Code = Convert.ToInt32(_u.Keys);
                    var ii = address.Delete();
                }
            }
            else if (_u.relation_type == "2")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Phone_List phone = new CT_Phone_List();
                if (_u.Keys > 0)
                {
                    phone.PL_Code = Convert.ToInt32(_u.Keys);
                    phone.Delete();
                }
            }
            else if (_u.relation_type == "3")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Email_List email = new CT_Email_List();
                if (_u.Keys > 0)
                {
                    email.EL_Code = Convert.ToInt32(_u.Keys);
                    email.Delete();
                }
            }
            else if (_u.relation_type == "4")
            {
                CT_Relation_Con _c = JsonConvert.DeserializeObject<CT_Relation_Con>(JsonConvert.SerializeObject(_u.o));
                CT_Messaging_List msg = new CT_Messaging_List();
                if (_u.Keys > 0)
                {
                    msg.ML_Code = Convert.ToInt32(_u.Keys);
                    msg.Delete();
                } msg.Insert();
            }
        }
    }

    private int Get_User_Only_Car(DBCRMTree db, long au_code)
    {
        string sql = "select top 1 CI_Code from CT_Car_Inventory where CI_AU_Code=@0;";

        dynamic car = db.Query<dynamic>(sql, au_code);
        dynamic _car = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(car));
        string _CI = JsonConvert.SerializeObject(_car[0].CI_Code);
        return Convert.ToInt32(_CI);
    }

    private void Save_Relation(dynamic relations, int CI_Code)
    {
        var s_relations = JsonConvert.SerializeObject(relations.changes);
        List<CT_Drivers_List> drivers = JsonConvert.DeserializeObject<List<CT_Drivers_List>>(s_relations);
        List<CT_All_User> users = JsonConvert.DeserializeObject<List<CT_All_User>>(s_relations);
        var i = 0;
        foreach (var u in users)
        {
            if (u.AU_Code > 0)
            {
                u.AU_Update_dt = DateTime.Now;
                var cols = new string[]{
                "AU_Update_dt",
                "AU_Name", 
                "AU_Gender",
                "AU_B_date" ,
                "AU_ID_Type", 
                "AU_ID_No",
                "AU_Dr_Lic"
                };
                var listCols = cols.ToList<string>();
                u.Update(listCols);
            }
            else
            {
                u.AU_Code = (long)u.Insert();
            }

            var d = drivers[i];
            if (d.DL_AU_Code > 0)
            {
                d.DL_Update_dt = DateTime.Now;
                string sql = @"UPDATE CT_Drivers_List
                SET DL_Access = @DL_Access
                ,DL_Type = @DL_Type
                ,DL_Relation = @DL_Relation
                ,DL_Update_dt = @DL_Update_dt
                WHERE DL_AU_Code = @DL_AU_Code";
                var db = DBCRMTree.GetInstance();
                db.Execute(sql, d);
            }
            else
            {
                d.DL_AU_Code = u.AU_Code;
                d.DL_CI_Code = CI_Code;
                d.DL_Add_dt = DateTime.Now;
                d.Insert();
            }

            i++;
        }
    }

    /// <summary>
    /// 保存车信息
    /// </summary>
    /// <param name="data"></param>
    /// <param name="au_code"></param>
    private void Save_Cars(dynamic data, long au_code)
    {
        var s_cars = JsonConvert.SerializeObject(data.cars.changes);
        List<CT_Car_Inventory> cars = JsonConvert.DeserializeObject<List<CT_Car_Inventory>>(s_cars);
        List<CT_Auto_Insurance> insurances = JsonConvert.DeserializeObject<List<CT_Auto_Insurance>>(s_cars);
        int i = 0;
        foreach (var c in cars)
        {
            var ci_code = c.CI_Code;
            c.CI_AU_Code = au_code;
            c.CI_Create_dt = DateTime.Now;

            var ai = insurances[i];
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
                    //,"CI_Picture_FN"
                });

                var db = DBCRMTree.GetInstance();
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
            }
            else
            {
                ci_code = (int)c.Insert();
                ai.AI_CI_Code = ci_code;
                ai.Insert();
            }
            i++;

            if (null != c.Drivers)
            {
                Save_Relation(c.Drivers, ci_code);
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
            //删除图片
            if (!string.IsNullOrWhiteSpace(c.Picture_Removed))
            {
                string[] imgs_removed = c.Picture_Removed.Split(',');
                foreach (var img in imgs_removed)
                {
                    CT_Car_Picture.Delete("where [CP_Picture_FN]=@0", img);
                    var imgPath = path_save + img;
                    if (File.Exists(imgPath))
                    {
                        File.Delete(imgPath);
                    }
                }
            }
        }

        var s_delete_cars = JsonConvert.SerializeObject(data.cars.deletes);
        List<CT_Car_Inventory> delete_cars = JsonConvert.DeserializeObject<List<CT_Car_Inventory>>(s_delete_cars);
        foreach (var c in delete_cars)
        {
            c.CI_Update_dt = DateTime.Now;
            c.CI_Activate_Tag = 0;
            c.Update(new string[]{
                "CI_Update_dt",
                "CI_Activate_Tag"
            });
        }
    }
    #endregion

    /// <summary>
    /// 获得客户信息
    /// </summary>
    /// <param name="data"></param>
    private void Get_CustomerInfo(dynamic data)
    {
        dynamic o = new ExpandoObject();
        int AU_Code = (int)data.AU_Code;
        if (AU_Code > 0)
        {
            var customer = CT_All_User.SingleOrDefault(@"select 
            AU_Code, AU_Name,AU_Username, AU_Dr_Lic, AU_Gender, AU_ID_Type, AU_ID_No, AU_Education, AU_Occupation,
            AU_Industry, AU_B_date , AU_Type, AU_Update_dt from CT_All_Users where AU_Code=@0", AU_Code);

            customer.EX_AU_Activate_dt = customer.AU_Update_dt.HasValue ? customer.AU_Update_dt.Value.ToString(Interna ? "MM/dd/yyyy" : "yyyy-MM-dd") : "";
            o.personal = customer;
            #region contacts
            o.contacts = new ExpandoObject();
            var db = DBCRMTree.GetInstance();
             var sql_text_part = Interna ? "[text_en]" : "[text_cn]";
            var sql = string.Format(@"Select AL_Code,AL_Type,AL_Add1,AL_Add2,AL_District,AL_City
,AL_State,AL_Zip,AL_Pref as [pref],AL_DonotUse as [DonotUse]
,(select {0} from words where p_id = 55 and [value] = al_type) as AL_Type_Text
FROM CT_Address_List 
WHERE AL_AU_AD_Code=@0 and isnull(AL_Active,1) = 1
                ", sql_text_part);
            o.contacts.address = db.Query<dynamic>(sql, AU_Code);

            sql = string.Format(@"Select PL_Code,PL_Type,PL_Number,PL_pref as [pref],PL_DonotUse as [DonotUse]
,(select {0} from words where p_id = 47 and [value] = pl_type) as PL_Type_Text
from CT_Phone_List 
WHERE PL_AU_AD_Code=@0 and isnull(PL_Active,1) = 1
                ", sql_text_part);
            o.contacts.phone = db.Query<dynamic>(sql, AU_Code);

            sql = string.Format(@"Select EL_Code,EL_Type,EL_Address,EL_Pref as [pref],EL_DonotUse as [DonotUse]
,(select {0} from words where p_id = 44 and [value] = el_type) as EL_Type_Text
from CT_eMail_List 
WHERE EL_AU_AD_Code=@0 and isnull(EL_Active,1) = 1
                ", sql_text_part);
            o.contacts.email = db.Query<dynamic>(sql, AU_Code);

            sql_text_part = Interna ? "[MC_Name_EN]" : "[MC_Name_CN]";
            sql = string.Format(@"Select ML_MC_Code,ML_Code,ML_Handle,ML_Pref as [pref],ML_DonotUse as [DonotUse]
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
                o.summary = db.SingleOrDefault<dynamic>(
                            ";exec [CT_GetCustomerSummary] @Dealer_code,@User_code,@IsEn"
                            , new { Dealer_code = dealer_code, User_code = AU_Code, IsEn = Interna }
                            );
            }
            #endregion

            #region cars
            o.cars = db.Query<dynamic>(
                        ";exec [CT_GetCustomerCars] @User_code,@IsEn"
                        , new { User_code = AU_Code, IsEn = Interna }
                        );
            #endregion

            #region Create Wangqi  2015/03/24

            o.relation = new ExpandoObject();
            //o.relation.user = GetCustomerRelation(AU_Code.ToString());
            dynamic _re = GetCustomerRelation_Communication(AU_Code.ToString());
            o.relation.con = _re;
            o.relation.GetWords = GetWords(AU_Code);
            //string _opp = JsonConvert.SerializeObject(o);
            #endregion
        }
        Response.Write(JsonConvert.SerializeObject(o));
    }

    #region Create Wangqi  2015/03/23
    /// <summary>
    /// 获取车主的所有联系人
    /// </summary>
    /// <param name="au_code"></param>
    /// <returns></returns>
    private dynamic GetCustomerRelation(string au_code)
    {
        string sql = @"SELECT AU_Code,AU_Name,W.value AS relation_id  
                        FROM CT_All_Users INNER JOIN WORDS W ON W.p_id=2036 AND W.value=0
                        WHERE AU_Code=@0
                        UNION 
                        SELECT AU.AU_Code,AU.AU_Name,W.value AS relation_id FROM CT_Car_Inventory CI 
                        INNER JOIN CT_Drivers_List DL ON CI.CI_Code=DL.DL_CI_Code
                        INNER JOIN CT_All_Users AU ON AU.AU_Code=DL.DL_AU_Code
                        INNER JOIN WORDS W ON DL.DL_Relation=W.value AND W.p_id=2036
                        WHERE CI_AU_Code=@0";
        var db = DBCRMTree.GetInstance();
        dynamic o = new ExpandoObject();
        o = db.Query<dynamic>(sql, au_code);
        return o;
    }
    /// <summary>
    /// 获取包括车主在内的所有联系人的通讯方式
    /// </summary>
    /// <param name="au_codes"></param>
    /// <returns></returns>
    private dynamic GetCustomerRelation_Communication(string au_code)
    {
        string sql = @"SELECT * FROM(
                        --住址
                        SELECT AU.AU_Code,AU.AU_Name,0 AS relation_id,'1' as relation_type,B.* FROM CT_All_Users AU
                        INNER JOIN (
                        SELECT W.value AS contact_id ,ISNULL(AL.AL_State,'')+ISNULL(AL.AL_City,'')+ISNULL(AL.AL_District,'')+
                               ISNULL(AL.AL_Add1,'')+ISNULL(AL.AL_ADD2,'') AS info,AL.AL_AU_AD_Code AS CODE,AL.AL_Code AS Keys,
	                           AL.AL_Pref AS Orders  FROM CT_Address_List AL
                          INNER JOIN WORDS W ON p_id = 55 and value = AL.AL_Type AND ISNULL(AL_Active,1) = 1)AS B ON B.CODE=AU.AU_Code
                        WHERE AU.AU_Code=@0
                        UNION
                        SELECT AU.AU_Code,AU.AU_Name,W.value AS relation_id,'1' as relation_type,B.* FROM CT_Drivers_List DL
                        INNER JOIN WORDS W ON DL.DL_Relation=W.value AND W.p_id=2036
                        INNER JOIN CT_All_Users AU ON AU.AU_Code=DL.DL_AU_Code
                        INNER JOIN CT_Assc_Contacts AC ON DL.DL_AU_Code=AC.AC_AU_Code AND AC.AC_UType=1
                        INNER JOIN (
                        SELECT W.value AS contact_id ,ISNULL(AL.AL_State,'')+ISNULL(AL.AL_City,'')+ISNULL(AL.AL_District,'')+
                               ISNULL(AL.AL_Add1,'')+ISNULL(AL.AL_ADD2,'') AS info,AL.AL_AU_AD_Code AS CODE,AL.AL_Code AS Keys,
	                           AL.AL_Pref AS Orders  FROM CT_Address_List AL
                          INNER JOIN WORDS W ON p_id = 55 and value = AL.AL_Type AND ISNULL(AL_Active,1) = 1
                         )AS B ON B.CODE=AU.AU_Code AND B.Keys=AC.AC_PL_ML_Code
                         WHERE DL.DL_M_AU_Code=@0
                        UNION
                        --电话
                        SELECT AU.AU_Code,AU.AU_Name,0 AS relation_id,'2' as relation_type,B.* FROM CT_All_Users AU
                        INNER JOIN(
                        SELECT W.value AS contact_id ,PL.PL_Number AS info,PL.PL_AU_AD_Code AS CODE,PL.PL_Code AS Keys,pl.PL_Pref AS Orders  
                         FROM CT_Phone_List PL 
                           INNER JOIN WORDS W ON p_id = 47 and value = PL.PL_Type AND ISNULL(PL_Active,1) = 1)AS B ON B.CODE=AU.AU_Code
                        WHERE AU.AU_Code=@0
                        UNION
                        SELECT AU.AU_Code,AU.AU_Name,W.value AS relation_id,'2' as relation_type,B.* FROM CT_Drivers_List DL
                        INNER JOIN WORDS W ON DL.DL_Relation=W.value AND W.p_id=2036
                        INNER JOIN CT_All_Users AU ON AU.AU_Code=DL.DL_AU_Code
                        INNER JOIN CT_Assc_Contacts AC ON DL.DL_AU_Code=AC.AC_AU_Code AND AC.AC_UType=2
                        INNER JOIN (
                        SELECT W.value AS contact_id ,PL.PL_Number AS info,PL.PL_AU_AD_Code AS CODE,PL.PL_Code AS Keys,pl.PL_Pref AS Orders 
                         FROM CT_Phone_List PL 
                            INNER JOIN WORDS W ON p_id = 47 and value = PL.PL_Type AND ISNULL(PL_Active,1) = 1
                        )AS B ON B.CODE=AU.AU_CODE AND B.Keys=AC.AC_PL_ML_Code
                        WHERE DL.DL_M_AU_Code=@0
                        UNION
                        --邮件
                        SELECT AU.AU_Code,AU.AU_Name,0 AS relation_id,'3' as relation_type,B.* FROM CT_All_Users AU
                        INNER JOIN(
                        SELECT W.value AS contact_id ,EL.EL_Address as info ,EL.EL_AU_AD_Code as CODE,EL.EL_Code AS Keys,EL.EL_Pref AS Orders 
                         FROM CT_Email_List EL 
                            INNER JOIN WORDS W ON p_id = 44 and value = EL.EL_Type AND ISNULL(EL_Active,1) = 1)AS B ON B.CODE=AU.AU_Code
                        WHERE AU.AU_Code=@0
                        UNION
                        SELECT AU.AU_Code,AU.AU_Name,W.value AS relation_id,'3' as relation_type,B.* FROM CT_Drivers_List DL 
                        INNER JOIN WORDS W ON DL.DL_Relation=W.value AND W.p_id=2036
                        INNER JOIN CT_All_Users AU ON AU.AU_Code=DL.DL_AU_Code
                        INNER JOIN CT_Assc_Contacts AC ON DL.DL_AU_Code=AC.AC_AU_Code AND AC.AC_UType=3
                        INNER JOIN (
                         SELECT W.value AS contact_id ,EL.EL_Address as info ,EL.EL_AU_AD_Code as CODE,EL.EL_Code AS Keys,EL.EL_Pref AS Orders FROM CT_Email_List EL 
                                   INNER JOIN WORDS W ON p_id = 44 and value = EL.EL_Type AND ISNULL(EL_Active,1) = 1
                        )AS B ON B.CODE=AU.AU_CODE AND B.Keys=AC.AC_PL_ML_Code
                        WHERE DL.DL_M_AU_Code=@0
                        UNION
                        --短信
                        SELECT AU.AU_Code,AU.AU_Name,0 AS relation_id,'4' as relation_type,B.* FROM CT_All_Users AU
                        INNER JOIN(
                        SELECT MC.MC_Code AS contact_id ,ML.ML_Handle as info ,ML.ML_AU_AD_Code as CODE,ML.ML_Code AS Keys,ML.ML_Pref AS Orders 
                          FROM CT_Messaging_List ML 
                             INNER JOIN CT_Messaging_Carriers MC ON MC.MC_Code=ML.ML_Type AND ISNULL(ML_Active,1) = 1)AS B ON B.CODE=AU.AU_Code
                        WHERE AU.AU_Code=@0
                        UNION
                        SELECT AU.AU_Code,AU.AU_Name,W.value AS relation_id,'4' as relation_type,B.* FROM CT_Drivers_List DL 
                        INNER JOIN WORDS W ON DL.DL_Relation=W.value AND W.p_id=2036
                        INNER JOIN CT_All_Users AU ON AU.AU_Code=DL.DL_AU_Code
                        INNER JOIN CT_Assc_Contacts AC ON DL.DL_AU_Code=AC.AC_AU_Code AND AC.AC_UType=4
                        INNER JOIN (
                         SELECT MC.MC_Code AS contact_id ,ML.ML_Handle as info ,ML.ML_AU_AD_Code as CODE,ML.ML_Code AS Keys,ML.ML_Pref AS Orders
                           FROM CT_Messaging_List ML 
                             INNER JOIN CT_Messaging_Carriers MC ON MC.MC_Code=ML.ML_Type AND ISNULL(ML_Active,1) = 1
                        )AS B ON B.CODE=AU.AU_CODE AND B.Keys=AC.AC_PL_ML_Code
                        WHERE DL.DL_M_AU_Code=@0
                        )AS A ORDER BY Orders;";
        var db = DBCRMTree.GetInstance();
        dynamic o = new ExpandoObject();
        o = db.Query<dynamic>(sql, au_code);
        return o;
    }
    private void Get_contact_value_default(dynamic data) {
        int _AU_Code = (int)data.AU_Code;
        int _Type = (int)data.contact_Type;
        string _sql = string.Empty;
        if (_Type == 1)
        {
            _sql = @"SELECT TOP 1 W.value AS contact_id ,AL.*  FROM CT_Address_List AL
                    INNER JOIN WORDS W ON p_id = 55 and value = AL.AL_Type AND ISNULL(AL_Active,1) = 1
                    WHERE AL.AL_AU_AD_Code=@0  ORDER BY AL_Pref";
        }
        else if (_Type == 2)
        {
            _sql = @"SELECT TOP 1 W.value AS contact_id ,PL.* FROM CT_Phone_List PL 
                       INNER JOIN WORDS W ON p_id = 47 and value = PL.PL_Type AND ISNULL(PL_Active,1) = 1
                    WHERE PL.PL_AU_AD_Code=@0  ORDER BY PL_Pref";
        }
        else if (_Type == 3)
        {
            _sql = @"SELECT TOP 1 W.value AS contact_id ,EL.* FROM CT_Email_List EL 
                        INNER JOIN WORDS W ON p_id = 44 and value = EL.EL_Type AND ISNULL(EL_Active,1) = 1
                    WHERE EL.EL_AU_AD_Code=@0  ORDER BY EL_Pref";
        }
        else if (_Type == 4)
        {
            _sql = @"SELECT TOP 1 MC.MC_Code AS contact_id ,ML.* FROM CT_Messaging_List ML 
                         INNER JOIN CT_Messaging_Carriers MC ON MC.MC_Code=ML.ML_Type AND ISNULL(ML_Active,1) = 1
                    WHERE ML.ML_AU_AD_Code=@0  ORDER BY ML_Pref";
        }
        var db = DBCRMTree.GetInstance();
        dynamic o = new ExpandoObject();
        o = db.Query<dynamic>(_sql, _AU_Code);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private void Get_contact_value(dynamic data) {
        int _Code = (int)data.Keys;
        int _Type = (int)data.contact_Type;
        string _sql = string.Empty;
        if (_Type == 1) {
            _sql = @"SELECT W.value AS contact_id ,AL.*  FROM CT_Address_List AL
                    INNER JOIN WORDS W ON p_id = 55 and value = AL.AL_Type AND ISNULL(AL_Active,1) = 1
                    WHERE AL.AL_Code=@0";
        }
        else if (_Type == 2) {
            _sql = @"SELECT W.value AS contact_id ,PL.* FROM CT_Phone_List PL 
                       INNER JOIN WORDS W ON p_id = 47 and value = PL.PL_Type AND ISNULL(PL_Active,1) = 1
                    WHERE PL.PL_Code=@0";
        }
        else if (_Type == 3)
        {
            _sql = @"SELECT W.value AS contact_id ,EL.* FROM CT_Email_List EL 
                        INNER JOIN WORDS W ON p_id = 44 and value = EL.EL_Type AND ISNULL(EL_Active,1) = 1
                    WHERE EL.EL_Code=@0";
        }
        else if (_Type == 4)
        {
            _sql = @"SELECT MC.MC_Code AS contact_id ,ML.* FROM CT_Messaging_List ML 
                         INNER JOIN CT_Messaging_Carriers MC ON MC.MC_Code=ML.ML_Type AND ISNULL(ML_Active,1) = 1
                    WHERE ML.ML_Code=@0";
        }
         var db = DBCRMTree.GetInstance();
         dynamic o = new ExpandoObject();
         o = db.Query<dynamic>(_sql, _Code);
         Response.Write(JsonConvert.SerializeObject(o));
    }
    #endregion

    /// <summary>
    /// 获得聊天类型
    /// </summary>
    private void Get_Messaging_Carriers()
    {
        var o = Get_CT_Messaging_Carriers();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Driver(int DL_AU_Code)
    {
        var db = DBCRMTree.GetInstance();
        var o = db.SingleOrDefault<dynamic>(@"
        SELECT * FROM CT_Drivers_List dl INNER JOIN CT_All_Users au
        ON au.AU_Code = dl.DL_AU_Code AND DL_AU_Code = @0", DL_AU_Code);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 车信息和联动列表
    /// </summary>
    /// <param name="id"></param>
    /// <param name="CI_CS_Code"></param>
    private void Get_Car_InventoryAndLendon(int id, int CI_CS_Code)
    {
        dynamic d = new ExpandoObject();
        var db = DBCRMTree.GetInstance();

        d.CT_Car_Inventory = CT_Car_Inventory.SingleOrDefault(id);
        d.CT_Car_Inventory.CM_Code = db.ExecuteScalar<int>(@"select cs_cm_code from [CT_Car_Style]
where cs_code = @0", CI_CS_Code > 0 ? CI_CS_Code : d.CT_Car_Inventory.CI_CS_Code);
        d.CT_Car_Style = Get_CT_Car_Style(d.CT_Car_Inventory.CM_Code, d.CT_Car_Inventory.CI_YR_Code);

        d.CT_Car_Inventory.MK_Code = db.ExecuteScalar<int>(@"select cm_mk_code from [CT_Car_Model]
where cm_code = @0", d.CT_Car_Inventory.CM_Code);
        d.CT_Car_Model = Get_CT_Car_Model(d.CT_Car_Inventory.MK_Code, d.CT_Car_Inventory.CI_YR_Code);
        //d.CT_Car_Inventory.CM_Code = Get_CT_Car_Model(d.CT_Car_Inventory.MK_Code, d.CT_Car_Style.CS_Year);

        d.Drivers = db.Query<dynamic>(string.Format(@"SELECT DL_AU_Code
        ,DL_Relation
        ,text_{0} DL_Relation_Text
        ,AU_Name
        FROM [CT_Drivers_List] dl INNER JOIN dbo.CT_All_Users au
        ON au.AU_Code = dl.DL_AU_Code
        LEFT JOIN words ON value = DL_Relation AND p_id = 2036
        WHERE DL_CI_Code = @0", Interna ? "en" : "cn"), d.CT_Car_Inventory.CI_Code);

        d.CT_Auto_Insurance = CT_Auto_Insurance.FirstOrDefault("where AI_CI_Code = @0 order by AI_Update_dt desc", d.CT_Car_Inventory.CI_Code);
        //        d.CT_Auto_Insurance = CT_Auto_Insurance.SingleOrDefault(@"Select TOP 1 
        //              AI_Code,AI_CI_Code,AI_IC_Code,AI_IA_Code,AI_Policy,AI_Start_dt,AI_End_dt,AI_Update_dt
        //           from CT_Auto_Insurance where AI_CI_Code = @0 order by AI_Update_dt desc", d.CT_Car_Inventory.CI_Code);

        Response.Write(JsonConvert.SerializeObject(d));
    }

    /// <summary>
    /// 车联动列表
    /// </summary>
    /// <param name="CI_CS_Code"></param>
    private void Get_Car_Lendon(int CI_CS_Code, int CI_YR_Code)
    {
        dynamic d = new ExpandoObject();
        var db = DBCRMTree.GetInstance();

        var CM_Code = db.ExecuteScalar<int>(@"select cs_cm_code from [CT_Car_Style]
where cs_code = @0", CI_CS_Code);
        d.CT_Car_Style = Get_CT_Car_Style(CM_Code, CI_YR_Code);

        var MK_Code = db.ExecuteScalar<int>(@"select cm_mk_code from [CT_Car_Model]
where cm_code = @0", CM_Code);
        d.CT_Car_Model = Get_CT_Car_Model(MK_Code, d.CT_Car_Style.CS_Year);

        //获得图片

        Response.Write(JsonConvert.SerializeObject(d));
    }

    /// <summary>
    /// 车表单列表
    /// </summary>
    private void Get_Car_Selects()
    {
        dynamic o = new ExpandoObject();

        var ad_code = null != UserSession.Dealer ? UserSession.Dealer.AD_Code : 0;
        if (ad_code > 0)
        {
            var ad = CT_Auto_Dealer.SingleOrDefault(ad_code);
            o.AD_MK_Code = ad.AD_MK_Code.HasValue ? ad.AD_MK_Code.Value.ToString() : "";
        }

        o.CT_Make = Get_CT_Make();
        o.CT_Years = Get_CT_Years();
        o.CT_Color_List = Get_CT_Color_List();
        o.CT_Insurance_Comp = Get_CT_Insurance_Comp();

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Years()
    {
        var o = Get_CT_Years();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 车型
    /// </summary>
    /// <param name="id"></param>
    private void Get_Make()
    {
        var o = Get_CT_Make();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Car_Model_All(int id)
    {
        var o = Get_CT_Car_Model_All(id);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 车风格
    /// </summary>
    /// <param name="id"></param>
    private void Get_Car_Style_All(int id)
    {
        var o = Get_CT_Car_Style_All(id);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Car_Model(int id, int CS_Year)
    {
        var o = Get_CT_Car_Model(id, CS_Year);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 车风格
    /// </summary>
    /// <param name="id"></param>
    private void Get_Car_Style(int id, int CI_YR_Code)
    {
        var o = Get_CT_Car_Style(id, CI_YR_Code);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Insurance_Agents(int id)
    {
        var o = Get_CT_Insurance_Agents(id);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    #region selects
    private IEnumerable<dynamic> Get_CT_Messaging_Carriers()
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[MC_Name_EN]" : "[MC_Name_CN]";
        var sql = string.Format(@"SELECT {0} as [text]
                ,[MC_Code] as [value]
                ,[MC_Type]
                ,[MC_Logo_PC]
                ,[MC_Logo_MB]
                FROM [CRMTREE].[dbo].[CT_Messaging_Carriers]
                ", sql_text_part);

        return db.Query<dynamic>(sql);
    }

    /// <summary>
    /// 生产商
    /// </summary>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Make()
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[MK_Make_EN]" : "[MK_Make_CN]";
        var sql = string.Format(@"SELECT 
                isnull({0},'') as [text]
                ,[MK_Code] as value
                FROM [CT_Make]
group by {0},MK_Code
order by {0}
                ", sql_text_part);

        return db.Query<dynamic>(sql);
    }

    private IEnumerable<dynamic> Get_CT_Car_Model_All(int CM_MK_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CM_Model_EN]" : "[CM_Model_CN]";
        var sql = string.Format(@"SELECT  
                isnull({0},'') as [text]
                ,[CM_Code] as value
                FROM [CT_Car_Model]
                LEFT JOIN CT_Car_Style ON CS_CM_Code = CM_Code
                where [CM_MK_Code] = @0 
                group by {0},CM_Code
                order by {0}
                ", sql_text_part);

        return db.Query<dynamic>(sql, CM_MK_Code);
    }

    /// <summary>
    /// 样式风格
    /// </summary>
    /// <param name="CS_CM_Code"></param>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Car_Style_All(int CS_CM_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CS_Style_EN]" : "[CS_Style_CN]";
        var sql = string.Format(@"SELECT distinct 
                        isnull({0},'') as [text]
                        ,[CS_Code] as value
                        FROM [CT_Car_Style]
                        where [CS_CM_Code] = @0 
                        ", sql_text_part);
        return db.Query<dynamic>(sql, CS_CM_Code);
    }

    /// <summary>
    /// 车型
    /// </summary>
    /// <param name="CM_MK_Code"></param>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Car_Model(int CM_MK_Code, int CI_YR_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CM_Model_EN]" : "[CM_Model_CN]";
        var sql = string.Format(@"SELECT  
                isnull({0},'') as [text]
                ,[CM_Code] as value
                FROM [CT_Car_Model]
                LEFT JOIN CT_Car_Style ON CS_CM_Code = CM_Code
                where [CM_MK_Code] = @0 and CS_Year = (SELECT YR_Year FROM CT_Years WHERE YR_Code = @1)
group by {0},CM_Code
order by {0}
                ", sql_text_part);

        return db.Query<dynamic>(sql, CM_MK_Code, CI_YR_Code);
    }

    /// <summary>
    /// 样式风格
    /// </summary>
    /// <param name="CS_CM_Code"></param>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Car_Style(int CS_CM_Code, int CI_YR_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CS_Style_EN]" : "[CS_Style_CN]";
        //        var sql = string.Format(@"SELECT distinct 
        //                isnull({0},'') as [text]
        //                ,[CS_Code] as value
        //                FROM [CT_Car_Style]
        //                where [CS_CM_Code] = @0 
        //                ", sql_text_part);
        var sql = string.Format(@"SELECT  
                isnull({0},'') as [text]
                ,[CS_Code] as value
                FROM [CT_Car_Style]
                where [CS_CM_Code] = @0 and CS_Year = (SELECT YR_Year FROM CT_Years WHERE YR_Code = @1)
group by {0},CS_Code
order by {0}
                ", sql_text_part);

        return db.Query<dynamic>(sql, CS_CM_Code, CI_YR_Code);
    }

    /// <summary>
    /// 年份
    /// </summary>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Years()
    {
        var db = DBCRMTree.GetInstance();
        var sql = @"SELECT 
            [YR_Code] as [value]
            ,[YR_Year] as [text]
            FROM [CT_Years]
            order by YR_Year desc";

        return db.Query<dynamic>(sql);
    }

    /// <summary>
    /// 颜色
    /// </summary>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Color_List()
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CL_Color_EN]" : "[CL_Color_CN]";
        var sql = string.Format(@"SELECT 
                {0} as [text]
                ,[CL_Code] as [value]
                FROM [CT_Color_List]
                ", sql_text_part);

        return db.Query<dynamic>(sql);
    }

    private IEnumerable<dynamic> Get_CT_Insurance_Comp()
    {
        var db = DBCRMTree.GetInstance();
        var sql = @"SELECT 
            [IC_Code] as [value]
            ,[IC_Name] as [text]
            FROM [CT_Insurance_Comp]
            order by IC_Name";

        return db.Query<dynamic>(sql);
    }

    private IEnumerable<dynamic> Get_CT_Insurance_Agents(int IA_IC_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql = @"SELECT IA_Code AS value
        ,AU_Name AS [text]
        ,IA_AU_Code
        FROM CT_Insurance_Agents ia INNER JOIN CT_All_Users au
        ON au.AU_Code = ia.IA_AU_Code
        WHERE IA_IC_Code = @0";

        return db.Query<dynamic>(sql, IA_IC_Code);
    }
    #endregion

    #region Create WangQi Date 2015/03/25
    /// <summary>
    /// 获取相关的Word
    /// </summary>
    /// <param name="p_id"></param>
    /// <returns></returns>
    private IEnumerable<dynamic> GetWords(long AU_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "text_en" : "text_cn";
        var sql = string.Format(@"SELECT AU_Code,AU_Name,{0} as text,value FROM (
                                    SELECT AU_Code,AU_Name,W.text_cn,W.text_en,W.value   
                                    FROM CT_All_Users INNER JOIN WORDS W ON W.p_id=2036 AND W.value=0
                                    WHERE AU_Code=@0
                                    UNION 
                                    SELECT AU_Code,AU_Name,W.text_cn,W.text_en,W.value  FROM CT_Drivers_List DL
                                    INNER JOIN CT_All_Users AU ON AU.AU_Code=DL.DL_AU_Code
                                    INNER JOIN WORDS W ON DL.DL_Relation=W.value AND W.p_id=2036
                                    WHERE DL.DL_M_AU_Code=@0 ) AS A ORDER BY value;
                ", sql_text_part);

        return db.Query<dynamic>(sql, AU_Code);
    }
    #endregion
}