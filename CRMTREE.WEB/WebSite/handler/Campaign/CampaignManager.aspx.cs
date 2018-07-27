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
using CRMTree.Model.Common;
using CRM_M = CRMTree.Model;
using ShInfoTech.Common;
using CRMTree.Model.User;
using CRMTree.BLL;
using System.Configuration;

public partial class handler_Campaign_CampaignManager : BasePage
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
                case "deleteVINFile":
                    deleteVINFile(data);
                    break;
                case "checkVINFile":
                    checkVINFile(data);
                    break;
                case "checkErrVINFile":
                    checkErrVINFile(data);
                    break;
                case "Get_Campaign_All":
                    Get_Campaign_All(data);
                    break;
                case "Get_All_Campaign":
                    Get_All_Campaign();
                    break;
                case "Get_Reports":
                    Get_Reports(data);
                    break;
                case "Get_Report_info":
                    Get_Report_info(data);
                    break;
                case "GetReport_Param":
                    GetReport_Param(data);
                    break;
                case "Get_Reports_Method":
                    Get_Reports_Method(data);
                    break;
                case "GetApprovalList":
                    GetApprovalList(data);
                    break;
                case "GetApproveUser":
                    GetApproveUser(data);
                    break;
                case "Get_Event_Genre":
                    Get_Event_Genre();
                    break;
                case "Get_Succ_Matrix":
                    Get_Succ_Matrix();
                    break;
                case "Get_ReportValue":
                    Get_ReportValue(data);
                    break;
                case "Get_Campaign":
                    Get_Campaign(data);
                    break;
                case "Get_CT":
                    Get_CT(data);
                    break;
                case "Get_Camp_Tags":
                    Get_Camp_Tags();
                    break;
                case "Set_Status":
                    Set_Status(data);
                    break;
                case "Save_Campaign":
                    Save_Campaign(data);
                    break;
                case "Save_Activities":
                    Save_Activities(data);
                    break;
                case "Add_CT_Event_Genre":
                    Add_CT_Event_Genre(data);
                    break;
                case "Save_Param_Value_Report":
                    Save_Param_Value_Report(data);
                    break;
                case "GetCamCall":
                    GetCamCall(data);
                    break;
                case "GetTeamGroupUser":
                    GetTeamGroupUser(data);
                    break;
                case "Get_run_user_list":
                    Get_run_user_list(data);
                    break;
                case "Get_run_file_list":
                    Get_run_file_list(data);
                    break;
                case "Get_view_file":
                    Get_view_file(data);
                    break;
                case "Get_Send_file":
                    Get_Send_file(data);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }
    private void Get_run_user_list(dynamic data)
    {
        int _CG_Code = (int)data.CG_Code;
        BL_Campaign _bl_cam = new BL_Campaign();
        DataTable _dt = _bl_cam.GetRunUser(_CG_Code, UserSession.DealerEmpl.DE_UType, UserSession.DealerEmpl.DE_AD_OM_Code);
        string _d = JsonConvert.SerializeObject(_dt);
        Response.Write(_d);
    }
    private void Get_run_file_list(dynamic data)
    {
        string _file_url = data.file_url;
        _file_url = Microsoft.JScript.GlobalObject.unescape(_file_url);
        string _fileCount = Files.FileContext(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + _file_url).Replace("\"", "'");
        if (data.AU_Code != null && ((string)data.AU_Code) != "")
        {
            List<CRMTreeDatabase.EX_Param> exParams = new List<CRMTreeDatabase.EX_Param>();
            exParams.Add(new CRMTreeDatabase.EX_Param() { EX_DataType = "int", EX_Name = "AU", EX_Value = (string)data.AU_Code });
            _fileCount = BL_Reports.GetRunFileContent(_fileCount, exParams,2);
        }

        _fileCount = Microsoft.JScript.GlobalObject.escape(_fileCount);
        string _d = JsonConvert.SerializeObject(_fileCount);
        Response.Write(_d);
    }
    private void Get_view_file(dynamic data)
    {
        int _au_code = (int)data.AU_Code;
        int _ap_code = (int)data.AP_Code;
        BL_Campaign _bl_cam = new BL_Campaign();
        CRMTree.Model.CT_Campaigns _o = _bl_cam.GetTXCam(UserSession.DealerEmpl.DE_UType, UserSession.DealerEmpl.DE_AD_OM_Code);

        string _fileCount = string.Empty;
        if (data.AU_Code != null && ((string)data.AU_Code) != "" && !string.IsNullOrEmpty(_o.CM_Filename) )
        {
            List<CRMTreeDatabase.EX_Param> exParams = new List<CRMTreeDatabase.EX_Param>();
            exParams.Add(new CRMTreeDatabase.EX_Param() { EX_DataType = "int", EX_Name = "AU", EX_Value = (string)data.AU_Code });
            exParams.Add(new CRMTreeDatabase.EX_Param() { EX_DataType = "int", EX_Name = "AD", EX_Value = UserSession.DealerEmpl.DE_AD_OM_Code.ToString() });
            exParams.Add(new CRMTreeDatabase.EX_Param() { EX_DataType = "int", EX_Name = "AP", EX_Value = (string)data.AP_Code });
            _fileCount = BL_Reports.GetFileContent(_o.CM_Filename, exParams,2);
        }
        _fileCount = Microsoft.JScript.GlobalObject.escape(_fileCount);
        CRMTree.Model.CT_All_Users _oo = _bl_cam.GetTXCam_Number(_au_code, _ap_code);
        string _pl_number ="";
        if(_oo != null && !string.IsNullOrEmpty(_oo.PL_Number)){
            _pl_number = _oo.PL_Number;
        }
        Response.Write(JsonConvert.SerializeObject(new { _con = _fileCount, _pl = _pl_number }));
    }
    private void Get_Send_file(dynamic data)
    {
        int _au_code = (int)data.AU_Code;
        int _ap_code = (int)data.AP_Code;
        string _au_Content = (string)data.Content;
        BL_Campaign _bl_cam = new BL_Campaign();
        CRMTree.Model.CT_All_Users _o = _bl_cam.GetTXCam_Number(_au_code, _ap_code);
        if (_o != null && !string.IsNullOrEmpty(_o.PL_Number))
        {
            SendMessage.SendAppconfirm(Convert.ToString(_o.PL_Number), _au_Content);
            //SendMessage.SendAppconfirm(Convert.ToString("18516147361"), _au_Content);
        }
        Response.Write(1);
    }
    /// <summary>
    /// 获取已经设置好的电话分配情况
    /// </summary>
    /// <param name="data"></param>
    private void GetCamCall(dynamic data)
    {
        int _CG_Code = (int)data.CG_Code;
        int type = 0;
        if (_CG_Code == 0)
        {
            _CG_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
            type = 1;
        }
        dynamic o = new ExpandoObject();
        BL_Campaign _b_cam = new BL_Campaign();
        o.TeamGroup = _b_cam.GetTeamGroup(Interna);
        o.CamCall = _b_cam.GetCamCall(_CG_Code, Interna, type);
        string _d_o = JsonConvert.SerializeObject(o);
        Response.Write(_d_o);
    }
    /// <summary>
    /// 获取组内用户
    /// </summary>
    /// <param name="data"></param>
    private void GetTeamGroupUser(dynamic data)
    {
        int _UG_Code = (int)data.UG_Code;
        dynamic o = new ExpandoObject();
        BL_Campaign _b_cam = new BL_Campaign();
        o.TeamGroupUser = _b_cam.GetTeamGroupUser(UserSession.DealerEmpl.DE_AD_OM_Code, _UG_Code, Interna);
        string _d_o = JsonConvert.SerializeObject(o);
        Response.Write(_d_o);
    }
    private void Add_CT_Event_Genre(dynamic data)
    {
        CT_Event_Genre g = new CT_Event_Genre();
        if (Interna)
        {
            g.EG_Desc = (string)data.EG_Desc;
        }
        else
        {
            g.EG_Desc_CN = (string)data.EG_Desc;
        }
        g.EG_UType = (byte)UserSession.User.UG_UType;
        g.EG_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        g.EG_Code = (short)g.Insert();
        Response.Write(JsonConvert.SerializeObject(new { selected = true, value = g.EG_Code, text = (string)data.EG_Desc }));
    }

    private void Get_Campaign_All(dynamic data)
    {
        var ds = new DataSet();
        var db = DBCRMTree.GetInstance();
        db.Fill(ds,
            ";exec P_Get_Campaign_All2 @P_ID,@IsEn,@CA,@Dlr,@Utype"
            , new { P_ID = (int)data.P_ID, IsEn = Interna, CA = (int)data.CA, Dlr = (int)UserSession.DealerEmpl.DE_AD_OM_Code, Utype = (int)UserSession.DealerEmpl.DE_UType }
            );

        Response.Write(JsonConvert.SerializeObject(ds));
    }

    private void Get_All_Campaign()
    {
        var ds = new DataSet();
        var db = DBCRMTree.GetInstance();
        db.Fill(ds,
            ";exec [P_GetALLCampaign] @IsEn"
            , new { IsEn = Interna }
            );

        Response.Write(JsonConvert.SerializeObject(ds));
    }

    private string getUrl()
    {
        string url = "";
        url += Request.Url.Scheme + "://" + Request.Url.Authority;
        return url;
    }

    private void Send_Email_Release(int cg_code)
    {
        var url = getUrl();
        var campaign = CT_Campaign.SingleOrDefault(cg_code);
        var approveUser = Get_ApproveUser((int)campaign.CG_Code, (int)campaign.CG_Type, (int)campaign.CG_Cat, 1);
        var send = Get_Send((int)campaign.CG_Created_By, approveUser.User_Code);

        string title = Resources.CRMTREEResource.cmp_app_request_title;
        string content = string.Format(Resources.CRMTREEResource.cmp_app_request_content,
         approveUser.User_Name,
         send.From_Name,
         campaign.CG_Code,
         url
         );
        //System.Environment.NewLine

        string supportMailAccont = ConfigurationManager.AppSettings["suppotMailAccount"];
        string suppotMailPwd = ConfigurationManager.AppSettings["suppotMailPwd"];
        string mailServer = ConfigurationManager.AppSettings["mailServer"];
        string displayName = ConfigurationManager.AppSettings["displayName"];


        Mail.SendMail(supportMailAccont,
            suppotMailPwd,
            new List<string>(new string[] { send.EL_To }),
            new List<string>(new string[] { send.EL_From }),
            null,
            title,
            supportMailAccont,
            displayName,
            mailServer,
            content);
    }

    private void Send_Email_Approve(int cg_code)
    {
        var url = getUrl();
        var campaign = CT_Campaign.SingleOrDefault(cg_code);
        var approveUser = Get_ApproveUser((int)campaign.CG_Code, (int)campaign.CG_Type, (int)campaign.CG_Cat, 1);
        string title = string.Empty;
        string title2 = string.Empty;
        string content = string.Empty;
        string supportMailAccont = ConfigurationManager.AppSettings["suppotMailAccount"];
        string suppotMailPwd = ConfigurationManager.AppSettings["suppotMailPwd"];
        string mailServer = ConfigurationManager.AppSettings["mailServer"];
        string displayName = ConfigurationManager.AppSettings["displayName"];
        EX_Approve send = new EX_Approve();
        title = Resources.CRMTREEResource.cmp_app_approved_title;
        var next_user = 0;
        if (null != approveUser)
        {
            next_user = approveUser.User_Code;
        }
        if (next_user == 0)
        {
            send = Get_Send((int)campaign.CG_Created_By, next_user);
            title = Resources.CRMTREEResource.cmp_app_appr_Ready_title;
            CT_Campaign cmp = new CT_Campaign();
            cmp.CG_Code = cg_code;
            cmp.CG_Status = 10;
            cmp.CG_Update_dt = DateTime.Now;
            cmp.CG_Updated_By = UserSession.User.AU_Code;
            var cols = new string[]{
                    "CG_Status"
                    };
            cmp.Update(cols);

        }
        else
        {
            send = Get_Send((int)campaign.CG_Created_By, next_user);
            title2 = Resources.CRMTREEResource.cmp_app_request_title;
            content = string.Format(Resources.CRMTREEResource.cmp_app_request_content,
             approveUser.User_Name,
             send.From_Name,
             campaign.CG_Code,
             url
             );
            //System.Environment.NewLine



            Mail.SendMail(supportMailAccont,
                suppotMailPwd,
                new List<string>(new string[] { send.EL_To }),
                new List<string>(new string[] { send.EL_From }),
                null,
                title2,
                supportMailAccont,
                displayName,
                mailServer,
                content);

        }
        content = string.Format(Resources.CRMTREEResource.cmp_app_approved_content,
        send.From_Name,
        UserSession.User.AU_Name,  //approveUser.User_Name,
        campaign.CG_Code,
        url
        );
        //System.Environment.NewLine

        Mail.SendMail(supportMailAccont,
           suppotMailPwd,
            new List<string>(new string[] { send.EL_From }),
            null,
            null,
            title,
            supportMailAccont,
            displayName,
            mailServer,
            content);

    }

    private void Send_Email_Reject(int cg_code)
    {
        var url = getUrl();
        var campaign = CT_Campaign.SingleOrDefault(cg_code);
        var approveUser = Get_ApproveUser((int)campaign.CG_Code, (int)campaign.CG_Type, (int)campaign.CG_Cat, 1);
        var next_user = 0;
        if (null != approveUser)
        {
            next_user = approveUser.User_Code;
        }
        var send = Get_Send((int)campaign.CG_Created_By, next_user);

        string title = Resources.CRMTREEResource.cmp_app_rejected_title;
        string content = string.Format(Resources.CRMTREEResource.cmp_app_rejected_content,
         send.From_Name,
         UserSession.User.AU_Name,  //approveUser.User_Name,
         campaign.CG_Code,
         url
         );
        //System.Environment.NewLine
        campaign.CG_Status = 0;
        var cols = new string[]{
                    "CG_Status"
                    };
        campaign.Update(cols);

        string supportMailAccont = ConfigurationManager.AppSettings["suppotMailAccount"];
        string suppotMailPwd = ConfigurationManager.AppSettings["suppotMailPwd"];
        string mailServer = ConfigurationManager.AppSettings["mailServer"];
        string displayName = ConfigurationManager.AppSettings["displayName"];

        Mail.SendMail(supportMailAccont,
            suppotMailPwd,
            new List<string>(new string[] { send.EL_From }),
            null,
            null,
            title,
            supportMailAccont,
            displayName,
            mailServer,
            content);
    }

    private void Save_Activities(dynamic data)
    {
        //var ug_code = this.UserSession.AU_UG_Code;
        //if (ug_code != 28 && ug_code != 40)
        //{
        //    throw new Exception(Interna ? "Without the permission!" : "无此权限！");
        //}

        var db = DBCRMTree.GetInstance();
        try
        {
            using (var tran = db.GetTransaction())
            {
                var s_activity = JsonConvert.SerializeObject(data.activity);
                EX_CT_Auth_Activities activity = JsonConvert.DeserializeObject<EX_CT_Auth_Activities>(s_activity);

                CT_Auth_Activity aa = new CT_Auth_Activity();
                aa.AA_AT_Code = activity.EX_AT_Code;
                aa.AA_CG_Code = activity.EX_CG_Code;
                aa.AA_AU_Code = UserSession.User.AU_Code;
                aa.AA_Update_dt = DateTime.Now;
                if (activity.EX_State == 0)
                {
                    aa.AA_Type = 200;
                }
                if (activity.EX_State == 1)
                {
                    if (string.IsNullOrWhiteSpace(activity.EX_Remark))
                    {
                        aa.AA_Type = 100;
                    }
                    else
                    {
                        aa.AA_Type = 101;
                    }
                }
                var id = (int)aa.Insert();

                if (!string.IsNullOrWhiteSpace(activity.EX_Remark))
                {
                    CT_Auth_Note an = new CT_Auth_Note();
                    an.AAN_AA_Code = id;
                    an.AAN_Notes = activity.EX_Remark;
                    an.Insert();
                }

                if (activity.EX_State == 0)
                {
                    CT_Campaign campaign = new CT_Campaign();
                    campaign.CG_Code = activity.EX_CG_Code;
                    campaign.CG_Status = 0;
                    campaign.CG_Update_dt = DateTime.Now;
                    campaign.CG_Updated_By = UserSession.User.AU_Code;
                    var cols = new string[]{
                    "CG_Status",
                    "CG_Update_dt",
                    "CG_Updated_By"
                    };
                    campaign.Update(cols);
                }

                if (activity.EX_State == 0)
                {
                    Send_Email_Reject(activity.EX_CG_Code);
                }
                else
                {
                    Send_Email_Approve(activity.EX_CG_Code);
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

    private void Set_Status(dynamic data)
    {
        var bRelease = false;
        var s_campaign = JsonConvert.SerializeObject(data.campaign);
        CT_Campaign campaign = JsonConvert.DeserializeObject<CT_Campaign>(s_campaign);
        if (campaign.CG_Code > 0)
        {
            campaign.CG_Update_dt = DateTime.Now;
            campaign.CG_Updated_By = UserSession.User.AU_Code;
            var cols = new string[]{
            "CG_Status",
            "CG_Update_dt",
            "CG_Updated_By"
            };
            campaign.Update(cols);
            bRelease = true;
        }

        Response.Write(JsonConvert.SerializeObject(new { isOK = bRelease }));
    }

    private void Get_CT(dynamic data)
    {
        dynamic o = new ExpandoObject();
        int CG_Code = (int)data.CG_Code;
        int CT = -1;
        if (CG_Code > 0)
        {
            var db = DBCRMTree.GetInstance();
            CT = db.ExecuteScalar<int>("select CG_Type from CT_Campaigns where CG_Code=@0", CG_Code);
        }
        Response.Write(JsonConvert.SerializeObject(new { CT = CT }));
    }

    /// <summary>
    /// 获得客户信息
    /// </summary>
    /// <param name="data"></param>
    private void Get_Campaign(dynamic data)
    {
        dynamic o = new ExpandoObject();
        o.AU_Code = UserSession.User.AU_Code;
        int CG_Code = (int)data.CG_Code;
        if (CG_Code > 0)
        {
            var campaign = CT_Campaign.SingleOrDefault(CG_Code);
            if (null == campaign)
            {
                throw new Exception(Interna ? "Campaign does not exist!" : "活动不存在！");
            }
            if (campaign.CG_Act_S_Dt.HasValue)
            {
                long D = 864000000000;
                try
                {
                    long Ex_CG_Act_E_Dt = campaign.CG_Act_E_Dt.Value.ToFileTime() - campaign.CG_Act_S_Dt.Value.ToFileTime();
                    campaign.Ex_CG_Act_E_Dt = (int)(Ex_CG_Act_E_Dt / D);


                    long Ex_CG_Start_Dt = campaign.CG_Act_S_Dt.Value.ToFileTime() - campaign.CG_Start_Dt.Value.ToFileTime();
                    campaign.Ex_CG_Start_Dt = (int)(Ex_CG_Start_Dt / D);

                    long Ex_CG_End_Dt = campaign.CG_End_Dt.Value.ToFileTime() - campaign.CG_Act_S_Dt.Value.ToFileTime();
                    campaign.Ex_CG_End_Dt = (int)(Ex_CG_End_Dt / D) + campaign.Ex_CG_Start_Dt;
                }
                catch (Exception)
                {
                }
            }
            o.campaign = campaign;

            var db = DBCRMTree.GetInstance();
            o.sm_values = db.Query<dynamic>(string.Format(@"SELECT [SMV_PSM_Code]
                ,[SMV_Type]
                ,[SMV_CG_Code]
                ,[SMV_Days]
                ,[SMV_Val]
	            ,{0} as SMV_PSM_Code_Text
	            ,{1} as PSM_Val_Type
            FROM [CT_SM_Values] INNER JOIN CT_Succ_Matrix 
            ON PSM_Code = SMV_PSM_Code AND SMV_CG_Code = @0",
            Interna ? "[PSM_Desc_EN]" : "[PSM_Desc_CN]",
            Interna ? "[PSM_Val_Type_EN]" : "[PSM_Val_Type_CN]"
            ), CG_Code);

            o.methods = db.Query<dynamic>(string.Format(@"
            SELECT  CM_CG_Code ,
            CM_Contact_Index ,
            CM_Method ,
            LEFT(CM_Method_Text,LEN(CM_Method_Text)-1) CM_Method_Text,
            CM_RP_Code ,
            CM_RP_Code_Text ,
            CM_Filename,
            CM_Filename as CM_Filename_Temp
            FROM (
            SELECT CM_CG_Code
            ,CM_Contact_Index
            ,CM_Method
            ,(SELECT {0}+',' FROM dbo.words 
            WHERE p_id IN(4059, 4064, 4068) 
            AND value IN(SELECT * from dbo.f_split(CM_Method,','))
            FOR XML PATH('')
            )
            AS CM_Method_Text
            ,CM_RP_Code
            ,dbo.F_Format_Paramters_Method(
                CM_RP_Code
                ,CM_CG_Code
                ,CM_Contact_Index
                ,@1
                ,@2
                ,@3
            ) AS CM_RP_Code_Text
            ,CM_Filename
            FROM CT_Camp_Methods 
            WHERE CM_CG_Code=@0 )t
            order by CM_RP_Code,CM_Contact_Index",
            Interna ? "text_en" : "text_cn",
            Interna ? "RP_Name_EN" : "RP_Name_CN"
            ), CG_Code,
            UserSession.User.UG_UType,
            UserSession.DealerEmpl.DE_AD_OM_Code,
            Interna);

            if (!campaign.CG_Status.HasValue)
            {
                campaign.CG_Status = 0;
            }
            campaign.EX_CG_Status = db.ExecuteScalar<string>(string.Format(
                "select {0} from words where p_id=4044 and value=@0"
                , Interna ? "[text_en]" : "[text_cn]"
                ), campaign.CG_Status);

            if (campaign.CG_Status.HasValue && campaign.CG_Status.Value >= 5)
            {
                campaign.EX_Approve = 2;
                var approve = Get_ApproveUser(campaign.CG_Code, (int)campaign.CG_Type, (int)campaign.CG_Cat, 1);
                if (null != approve && approve.User_Code == UserSession.User.AU_Code)
                {
                    campaign.EX_Approve = 1;
                }
            }
        }

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Save_Campaign(dynamic data)
    {
        //var ug_code = this.UserSession.AU_UG_Code;
        //if (ug_code != 28 && ug_code != 40)
        //{
        //    throw new Exception(Interna ? "Without the permission!" : "无此权限！");
        //}
        int cg_code = 0;
        var db = DBCRMTree.GetInstance();
        try
        {
            using (var tran = db.GetTransaction())
            {
                cg_code = Save_CampaignInfo(data);

                if ((bool)data.bParamValue)
                {
                    Save_Param_Value(data, cg_code, db);
                }

                Save_Camp_Methods(data, cg_code, db);

                Save_SM_Values(data, cg_code, db);

                tran.Complete();
            }
            Save_Camp_Call(data, cg_code);
            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
        catch (Exception ex)
        {
            db.AbortTransaction();
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }
    private int Save_Camp_Call(dynamic data, int CG_Code)
    {
        var _v = (bool)data.callValiDate;
        if (_v)
        {
            string _o = JsonConvert.SerializeObject(data.callDatd);
            IList<CRM_M.Campaigns.CT_Camp_Calls> _Camp_Calls = JsonConvert.DeserializeObject<IList<CRM_M.Campaigns.CT_Camp_Calls>>(_o);
            BL_Campaign bll = new BL_Campaign();
            return bll.Save_Camp_Call(_Camp_Calls, CG_Code);
        }
        else
        {
            return (int)_errCode.isNull;
        }
    }
    private int Save_CampaignInfo(dynamic data)
    {
        var s_campaign = JsonConvert.SerializeObject(data.campaign);
        CT_Campaign campaign = JsonConvert.DeserializeObject<CT_Campaign>(s_campaign);
        int cg_code = campaign.CG_Code;

        if (campaign.CG_Act_S_Dt.HasValue)
        {
            try
            {
                campaign.CG_Act_E_Dt = campaign.CG_Act_S_Dt.Value.AddDays(campaign.Ex_CG_Act_E_Dt.Value);
                campaign.CG_Start_Dt = campaign.CG_Act_S_Dt.Value.AddDays(-campaign.Ex_CG_Start_Dt.Value);
                campaign.CG_End_Dt = campaign.CG_Act_S_Dt.Value.AddDays(campaign.Ex_CG_End_Dt.Value - campaign.Ex_CG_Start_Dt.Value);
            }
            catch (Exception)
            {
            }
        }


        if (campaign.CG_Code > 0)
        {
            campaign.CG_Update_dt = DateTime.Now;
            campaign.CG_Updated_By = UserSession.User.AU_Code;

            if (!campaign.CG_Status.HasValue)
            {
                campaign.CG_Status = 0;
            }

            var cols = new string[]{
            "CG_Title",
            "CG_Share",
            "CG_Desc",
            "CG_RP_Code",
            "CG_Method",
            "CG_Whom",
            "CG_Mess_Type",
            "CG_EG_Code",
            "CG_RSVP",
            "CG_Max_Persons",
            "CG_Responsible",
            "CG_Tools",
            "CG_Budget",
            "CG_Start_Dt",
            "CG_End_Dt",
            "CG_Act_S_Dt",
            "CG_Act_E_Dt",
            //"CG_Filename",
            "CG_Update_dt",
            "CG_Updated_By",
            "CG_Status",
            "CG_OEMPay"
            //,"CG_Type"
            };
            campaign.Update(cols);
        }
        else
        {
            campaign.CG_UType = (byte)UserSession.User.UG_UType;
            campaign.CG_AD_OM_Code = (int)UserSession.DealerEmpl.DE_AD_OM_Code;
            campaign.CG_Update_dt = DateTime.Now;
            campaign.CG_Created_By = UserSession.User.AU_Code;
            campaign.CG_Active_Tag = 1;
            campaign.CG_Status = 0;

            if (campaign.CG_Cat == null || campaign.CG_Cat == 0)
                campaign.CG_Cat = 1;

            if (campaign.EX_T.HasValue)
            {
                if (campaign.EX_T.Value == 1)
                {
                    campaign.CG_Status = 0;
                }

                if (campaign.EX_T.Value == 2)
                {
                    campaign.CG_Template = true;
                }
            }

            cg_code = (int)campaign.Insert();
        }

        if (campaign.CG_Status == 5)
        {
            CT_Auth_Activity aa = new CT_Auth_Activity();
            aa.AA_CG_Code = cg_code;
            aa.AA_AU_Code = UserSession.User.AU_Code;
            aa.AA_Update_dt = DateTime.Now;
            aa.AA_Type = 0;
            aa.Insert();

            Send_Email_Release(cg_code);
        }

        //BL_CRMhandle blCRM=new BL_CRMhandle();
        //blCRM.LoadCustomerizeVinToDB(campaign,cg_code);

        return cg_code;
    }

    private void Save_Param_Value_Report(dynamic data)
    {
        var s_param_value = JsonConvert.SerializeObject(data.param_value);
        List<CT_Param_Value> pvs = JsonConvert.DeserializeObject<List<CT_Param_Value>>(s_param_value);

        var pv_type = (byte)data.PV_Type;

        var o = new
        {
            PL_RP_Code = (int)data.PL_RP_Code,
            PV_Type = pv_type,
            PV_UType = (byte)UserSession.User.UG_UType,
            PV_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code
        };

        var db = DBCRMTree.GetInstance();
        db.Execute(@"DELETE FROM CT_Param_Value 
        WHERE (PV_Type=10 or PV_Type=@PV_Type) AND PV_CG_Code=-1 
        AND exists(select 1 from CT_Paramters_list where PV_PL_Code = PL_Code and PL_RP_Code=@PL_RP_Code)
        AND PV_UType=@PV_UType AND PV_AD_OM_Code = @PV_AD_OM_Code",
        o);

        foreach (var pv in pvs)
        {
            pv.PV_CG_Code = -1;
            pv.PV_Type = o.PV_Type;
            pv.PV_UType = o.PV_UType;
            pv.PV_AD_OM_Code = o.PV_AD_OM_Code;
            pv.Insert();
        }

        Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
    }

    private void Save_Param_Value(dynamic data, int cg_code, DBCRMTree db)
    {
        var s_param_value = JsonConvert.SerializeObject(data.param_value);
        List<CT_Param_Value> pvs = JsonConvert.DeserializeObject<List<CT_Param_Value>>(s_param_value);

        var o = new
        {
            PV_CG_Code = cg_code,
            PV_UType = (byte)UserSession.User.UG_UType,
            PV_AD_OM_Code = (int)UserSession.DealerEmpl.DE_AD_OM_Code
        };

        db.Execute(@"DELETE FROM CT_Param_Value WHERE 
        PV_CG_Code = @PV_CG_Code
        and PV_UType = @PV_UType
        and PV_AD_OM_Code = @PV_AD_OM_Code
        and PV_Type=1"
        , o);

        foreach (var pv in pvs)
        {
            pv.PV_CG_Code = o.PV_CG_Code;
            pv.PV_UType = o.PV_UType;
            pv.PV_AD_OM_Code = o.PV_AD_OM_Code;
            pv.Insert();
        }
    }

    private void Save_Camp_Files(string fileName, string fileName_Temp, string file_guid, int? EX_T)
    {
        if (!string.IsNullOrWhiteSpace(fileName))
        {
            var path_save = Server.MapPath("~/plupload/file/");
            var path = System.Configuration.ConfigurationManager.AppSettings["PLUploadPath"];
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "~/plupload/";
            }
            path = Server.MapPath(path);
            var path_temp = path + "file_temp/";
            if (!Directory.Exists(path_save))
            {
                Directory.CreateDirectory(path_save);
            }

            var fNames = fileName.Split('.').ToList();
            var extendName = "." + fNames[fNames.Count - 1];
            fNames.RemoveAt(fNames.Count - 1);
            var fName = string.Join("", fNames.ToArray());

            if (EX_T.HasValue && EX_T.Value == 1)
            {
                if (fileName != fileName_Temp)
                {
                    //移动
                    if (File.Exists(path_temp + fileName))
                    {
                        File.Move(path_temp + fileName, path_save + fileName);
                    }
                    if (File.Exists(path_temp + fName + ".txt"))
                    {
                        File.Move(path_temp + fName + ".txt", path_save + fName + ".txt");
                    }
                }
                else
                {
                    //复制
                    if (File.Exists(path_save + fileName))
                    {
                        File.Copy(path_save + fileName, path_save + file_guid + extendName);
                        if (File.Exists(path_save + fName + ".txt"))
                        {
                            File.Copy(path_save + fName + ".txt", path_save + file_guid + ".txt");
                        }
                    }
                }
            }
            else
            {
                if (fileName != fileName_Temp)
                {
                    //移动
                    if (File.Exists(path_temp + fileName))
                    {
                        File.Move(path_temp + fileName, path_save + fileName);
                    }
                    if (File.Exists(path_temp + fName + ".txt"))
                    {
                        File.Move(path_temp + fName + ".txt", path_save + fName + ".txt");
                    }

                    if (!string.IsNullOrWhiteSpace(fileName_Temp))
                    {
                        //删除
                        var oldFile = path_save + fileName_Temp;
                        if (File.Exists(oldFile))
                        {
                            File.Delete(oldFile);
                        }
                        fNames = fileName_Temp.Split('.').ToList();
                        fNames.RemoveAt(fNames.Count - 1);
                        fName = string.Join("", fNames.ToArray());
                        if (File.Exists(path_save + fName + ".txt"))
                        {
                            File.Delete(path_save + fName + ".txt");
                        }
                    }
                }
            }
        }
    }

    private void Save_Camp_Methods(dynamic data, int cg_code, DBCRMTree db)
    {
        var s_campaign = JsonConvert.SerializeObject(data.campaign);
        CT_Campaign campaign = JsonConvert.DeserializeObject<CT_Campaign>(s_campaign);

        var s_smvs = JsonConvert.SerializeObject(data.camp_methods.changes);
        List<CT_Camp_Method> cms = JsonConvert.DeserializeObject<List<CT_Camp_Method>>(s_smvs);

        var file_guid = Guid.NewGuid().ToString();
        byte index = 1;
        db.Execute("DELETE FROM CT_Camp_Methods WHERE CM_CG_Code = @0", cg_code);
        foreach (var cm in cms)
        {
            var CM_Filename = cm.CM_Filename;
            if (campaign.EX_T.HasValue && campaign.EX_T.Value == 1)
            {
                if (cm.CM_CG_Code > 0 && CM_Filename != cm.CM_Filename_Temp)
                {
                    var fNames = CM_Filename.Split('.').ToList();
                    var extendName = "." + fNames[fNames.Count - 1];
                    cm.CM_Filename = file_guid + extendName;
                }
            }

            cm.CM_Contact_Index = index;
            cm.CM_CG_Code = cg_code;
            cm.Insert();

            if (cm.EX_IsParamValue.HasValue && cm.EX_IsParamValue.Value && !string.IsNullOrWhiteSpace(cm.EX_ParamValue))
            {
                Save_Param_Value_Method(cm.EX_ParamValue, cg_code, index, db);
            }

            Save_Camp_Files(CM_Filename, cm.CM_Filename_Temp, file_guid, campaign.EX_T);

            index++;
        }
    }

    private void Save_Param_Value_Method(string s_param_value, int cg_code, byte index, DBCRMTree db)
    {
        List<CT_Param_Value> pvs = JsonConvert.DeserializeObject<List<CT_Param_Value>>(s_param_value);

        var o = new
        {
            PV_CG_Code = cg_code,
            PV_UType = (byte)UserSession.User.UG_UType,
            PV_AD_OM_Code = (int)UserSession.DealerEmpl.DE_AD_OM_Code,
            PV_Contact_Index = index
        };

        db.Execute(@"DELETE FROM CT_Param_Value WHERE 
        PV_CG_Code = @PV_CG_Code
        and PV_Contact_Index=@PV_Contact_Index
        and PV_UType = @PV_UType
        and PV_AD_OM_Code = @PV_AD_OM_Code
        and PV_Type=2"
        , o);

        foreach (var pv in pvs)
        {
            pv.PV_Contact_Index = o.PV_Contact_Index;
            pv.PV_CG_Code = o.PV_CG_Code;
            pv.PV_UType = o.PV_UType;
            pv.PV_AD_OM_Code = o.PV_AD_OM_Code;
            pv.Insert();
        }
    }

    private void Save_SM_Values(dynamic data, int cg_code, DBCRMTree db)
    {
        var s_smvs = JsonConvert.SerializeObject(data.sm_values.changes);
        List<CT_SM_Value> smvs = JsonConvert.DeserializeObject<List<CT_SM_Value>>(s_smvs);

        db.Execute("DELETE FROM CT_SM_Values WHERE SMV_CG_Code = @0", cg_code);

        foreach (var smv in smvs)
        {
            smv.SMV_CG_Code = cg_code;
            smv.SMV_Type = 1;//1 campaign,2 events
            smv.Insert();
        }
    }

    private EX_Approve Get_ApproveUser(int CG_Code, int CT, int CA, int Changetype)
    {
        var db = DBCRMTree.GetInstance();
        var sql = @";exec P_GetApproveUser @CG_Code,@CT,@CA,@AT_UType,@AD_OM_Code,@tt";
        var o = db.SingleOrDefault<EX_Approve>(sql, new
        {
            CG_Code = CG_Code,
            CT = CT,
            CA = CA,
            AT_UType = UserSession.User.UG_UType,
            AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code,
            tt = Changetype
        });
        return o;
    }

    private EX_Approve Get_Send(int creator, int nextApprover)
    {
        var db = DBCRMTree.GetInstance();
        var sql = @";exec P_GetSendEmail @creator,@nextApprover";
        var o = db.SingleOrDefault<EX_Approve>(sql, new
        {
            creator = creator,
            nextApprover = nextApprover
        });
        return o;
    }

    private void GetApproveUser(dynamic data)
    {
        var o = Get_ApproveUser((int)data.CG_Code, (int)data.CT, (int)data.CA, 1);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void GetApprovalList(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var sql = @";exec P_GetApprovalList @CT,@AT_UType,@AD_OM_Code,@CA,@tt";
        var o = db.Query<dynamic>(sql, new
        {
            CT = (int)data.CT,
            CA = (int)data.CA,
            AT_UType = UserSession.User.UG_UType,
            AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code,
            tt = 1
        });

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_ReportValue(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var sql = @";exec RP_GetReportValue @RP_Code,
                    @PV_Type,
                    @PV_CG_Code,
                    @PV_UType,
                    @AD_Code,
                    @DG_Code,
                    @OM_Code";
        var o = db.Query<dynamic>(sql, new
        {
            RP_Code = (int)data.RP_Code,
            PV_Type = (int)data.PV_Type,
            PV_CG_Code = (int)data.PV_CG_Code,
            PV_UType = UserSession.User.UG_UType,
            AD_Code = UserSession.Dealer != null ? UserSession.Dealer.AD_Code : 0,
            DG_Code = UserSession.DealerGroup != null ? UserSession.DealerGroup.DG_Code : 0,
            OM_Code = UserSession.OEM != null ? UserSession.OEM.OM_Code : 0
        });

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Succ_Matrix()
    {
        var o = Get_CT_Succ_Matrix();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private IEnumerable<dynamic> Get_CT_Succ_Matrix()
    {
        var db = DBCRMTree.GetInstance();
        var sql = string.Format(@"SELECT  PSM_Code AS [value],
            {0} AS [text],
            PSM_Category ,
            {1} AS [PSM_Val_Type]
            FROM    CT_Succ_Matrix;",
            Interna ? "[PSM_Desc_EN]" : "[PSM_Desc_CN]",
            Interna ? "[PSM_Val_Type_EN]" : "[PSM_Val_Type_CN]");
        return db.Query<dynamic>(sql);
    }

    private void Get_Event_Genre()
    {
        var o = Get_CT_Event_Genre();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private IEnumerable<dynamic> Get_CT_Event_Genre()
    {
        var db = DBCRMTree.GetInstance();
        var sql = string.Format(@"SELECT  EG_Code AS [value] ,
            {0} AS [text]
            FROM    CT_Event_Genre
            WHERE   EG_UType = @0
            AND EG_AD_OM_Code = @1",
            Interna ? "[EG_Desc]" : "[EG_Desc_CN]");
        return db.Query<dynamic>(sql, UserSession.User.UG_UType, UserSession.DealerEmpl.DE_AD_OM_Code);
    }

    private void Get_Reports(dynamic data)
    {
        int CG_Type = (int)data.CG_Type;
        int CA = (int)data.CA;
        var o = Get_CT_Reports(CG_Type, CA);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private void Get_Report_info(dynamic data)
    {
        int RP_Code = (int)data.RP_Code;
        int CG_Code = (int)data.CG_Code;
        var o = Get_CT_Reports_Info(RP_Code, CG_Code);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private void GetReport_Param(dynamic data)
    {
        int RP_Code = (int)data.RP_Code;
        var o = Get_Report_Param(RP_Code);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Reports_Method(dynamic data)
    {
        int CG_Type = (int)data.CG_Type;
        int CA = (int)data.CA;
        var o = Get_CT_Reports_Method(CG_Type, CA);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private IEnumerable<dynamic> Get_CT_Reports(int CG_Type, int CA)
    {
        MD_UserEntity _user = BL_UserEntity.GetUserInfo();
        if (_user == null)
        {
            return null;
        }
        var db = DBCRMTree.GetInstance();

        var sql = string.Format(@"SELECT  RP_Code AS [value] ,
            dbo.F_Format_Paramters({0}, RP_Code,{1},{2}) AS [text],
            {0} as ex_text
            FROM CT_Reports
            WHERE RP_Type IN ( 1,5,6,7,8,9 ) 
            AND @0 IN ( SELECT * FROM dbo.f_split(RP_Camp_Type, ',') ) AND(RP_Cat4=@1 OR RP_Cat4 IS NULL)
            order by RP_Sort
            ",
            Interna ? "[RP_Name_EN]" : "[RP_Name_CN]", _user.User.UG_UType, _user.DealerEmpl.DE_AD_OM_Code);
        return db.Query<dynamic>(sql, CG_Type, CA);
    }
    private IEnumerable<string> Get_CT_Reports_Info(int RP_Code, int CG_Code)
    {
        MD_UserEntity _user = BL_UserEntity.GetUserInfo();
        if (_user == null)
        {
            return null;
        }
        int _In = 0;
        if (Language.GetLang2() == EM_Language.en_us)
        {
            _In = 1;
        }
        var db = DBCRMTree.GetInstance();

        var sql = string.Format(@" SELECT dbo.F_Format_Paramters_Target({0},{1},{2},{3},{4})",
            RP_Code, CG_Code, _user.User.UG_UType, _user.DealerEmpl.DE_AD_OM_Code, _In);
        return db.Query<string>(sql);
    }
    private IEnumerable<int> Get_Report_Param(int RP_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql = string.Format(@"SELECT PL_CODE FROM CT_REPORTS INNER JOIN CT_PARAMTERS_LIST ON RP_CODE=PL_RP_CODE 
                                   WHERE RP_CODE={0}", RP_Code);
        return db.Query<int>(sql);
    }

    private IEnumerable<dynamic> Get_CT_Reports_Method(int CG_Type, int CA)
    {
        MD_UserEntity _user = BL_UserEntity.GetUserInfo();
        if (_user == null)
        {
            return null;
        }
        var db = DBCRMTree.GetInstance();
        var sql = string.Format(@"SELECT  RP_Code AS [value] ,
            dbo.F_Format_Paramters({0}, RP_Code,{1},{2}) AS [text],
            {0} as ex_text
            FROM CT_Reports
            WHERE RP_Type IN ( 2, 5, 7, 9, 10, 11 ) 
            AND @0 IN ( SELECT * FROM dbo.f_split(RP_Camp_Type, ',') ) AND(RP_Cat4=@1 OR RP_Cat4 IS NULL)
            order by RP_Sort
            ",
            Interna ? "[RP_Name_EN]" : "[RP_Name_CN]", _user.User.UG_UType, _user.DealerEmpl.DE_AD_OM_Code);
        return db.Query<dynamic>(sql, CG_Type, CA);
    }

    private void Get_Camp_Tags()
    {
        var o = Get_CT_Camp_Tags();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private IEnumerable<dynamic> Get_CT_Camp_Tags()
    {
        var db = DBCRMTree.GetInstance();
        var sql = string.Format(@"SELECT  CT_Code AS [value],
            {0} AS [text]
            FROM CT_Camp_Tags;",
            Interna ? "CT_Desc_EN" : "CT_Desc_CN");
        return db.Query<dynamic>(sql);
    }
    private void checkVINFile(dynamic data)
    {
        string fileName = (string)data.vinFile;
        string _path = this.Request.PhysicalApplicationPath + "plupload\\VINFile\\" + fileName;
        BL_CRMhandle _bl_crm = new BL_CRMhandle();
        _bl_crm.check_VINS(_path);
        Response.Write(JsonConvert.SerializeObject(_path));
    }
    private void checkErrVINFile(dynamic data)
    {
        string fileName = (string)data.vinFile;
        string _path = this.Request.PhysicalApplicationPath + "plupload\\VINFile\\" + fileName;
        BL_CRMhandle _bl_crm = new BL_CRMhandle();
        Response.Write(_bl_crm.check_errVINs(_path));
    }
    private void deleteVINFile(dynamic data)
    {
        string fileName = (string)data.vinFile;
        string _path = this.Request.PhysicalApplicationPath + "plupload\\VINFile\\" + fileName;
        int err = 0;
        if (File.Exists(_path))
        {
            try
            {
                File.Delete(_path);
                err = 1;
            }
            catch
            {
                err = 0;
            }
        }
        Response.Write(JsonConvert.SerializeObject(err));
    }
}
