using CRMTree.Public;
using CRM=CRMTree.Model;
using CRMTree.BLL;
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
using CRM_M = CRMTree.Model;
using CRMTree.Model.Common;
public partial class handler_Reports_DealershipManager : BasePage
{
    protected override void OnLoad(EventArgs e)
    {

        try
        {
            UserBuss buss = new UserBuss();
            bool CheckLogin = buss.CheckLogin("PublicUser", "");
            if (CheckLogin)
            {
                base.OnLoad(e);
                var o = Request.Params["o"];
                var data = JsonConvert.DeserializeObject<dynamic>(o);
                string acion = data.action;
                switch (acion)
                {
                    case "Get_Dept_Type":
                        Get_Dept_Type();
                        break;
                    case "Get_Service":
                        Get_Service(data);
                        break;
                    case "Get_Dept_Variables":
                        Get_Dept_Variables(data);
                        break;
                    case "Get_All_Users":
                        Get_All_Users((int)data.UG_Code);
                        break;
                    case "Get_User_Groups":
                        Get_User_Groups();
                        break;
                    case "Save_Service":
                        Save_Service(data);
                        break;
                    case "Seve_Process":
                        Seve_Process(data);
                        break;
                    case "Get_approval":
                        Get_approval(data);
                        break;
                    case "GetWords":
                        GetWords(data);
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }
    private void GetWords(dynamic data)
    {
        int p_id = (int)data.p_id;
        BL_CRMhandle _b_crm = new BL_CRMhandle();
        string _Words = _b_crm.GetWords(Interna, p_id);
        string _o = "{\"_Words\":" + _Words + "}";
        Response.Write(_o);
    }
    private void Get_approval(dynamic data)
    {
        int UType = UserSession.DealerEmpl.DE_UType;
        int AD_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        int AT_CG_Cat = (int)data.AT_Cat;
        int AT_IType = (int)data.AT_IType;
        int p_id = (int)data.p_id;

        BL_Campaign _b_Cam = new BL_Campaign();
        string _Process = _b_Cam.GetProcess(Interna, UType, AD_Code, AT_CG_Cat, AT_IType);
        BL_CRMhandle _b_crm = new BL_CRMhandle();
        string _Words = _b_crm.GetWords(Interna, p_id);
        string _o = "{\"approval\":" + _Process + ",\"_Words\":" + _Words + "}";
        Response.Write(_o); 
    }

    private void Get_Dept_Variables(dynamic data)
    {
        var o = Get_CT_Dept_Variables(data);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private IEnumerable<dynamic> Get_CT_Dept_Variables(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var lng = Interna ? "EN" : "CN";
        var sql = string.Format(@"SELECT  PDV_Type ,
                {0} AS PDV_Name ,
                {1} as PDV_Prompt,
		        PDV_Code AS DV_PDV_Code,
		        DV_PDN_Code,
                DV_Value
        FROM    CT_Dept_Variables pdv
                LEFT JOIN CT_Dept_Values dv ON dv.DV_PDV_Code = pdv.PDV_Code
                                               AND DV_PDN_Code = @DV_PDN_Code
        WHERE   PDV_PDT_Code = @PDV_PDT_Code", "PDV_Name_" + lng, "PDV_Prompt_" + lng);

        return db.Query<dynamic>(sql, new
        {
            PDV_PDT_Code = (int)data.PDV_PDT_Code,
            DV_PDN_Code = (int)data.DV_PDN_Code
        });
    }

    private IEnumerable<dynamic> Get_CT_User_Groups()
    {
        var db = DBCRMTree.GetInstance();
        var lng = Interna ? "EN" : "CN";
        var sql = string.Format(@"SELECT [UG_Code] AS value
            ,UG_Name_{0} AS [text]
            ,(Select text_{0} from [Words] where p_id=4203 and value=[UG_UType]) as [group]
        FROM [CT_User_Groups]
        WHERE UG_Appr_Auth = 1 and isnull(UG_Name_{0},'') <> '' order by UG_Utype Asc,UG_Code ASC", lng);

        return db.Query<dynamic>(sql, UserSession.User.UG_UType);
    }

    private IEnumerable<dynamic> Get_CT_All_Users(int UG_Code)
    {
        var db = DBCRMTree.GetInstance();
        string AU_Name = Interna ? "Person In Charge" : "负责人";
        //AU_Type
        var sql = string.Format(@"
         select 0 as value,'{0}' text,'true' as  selected
         union 
         SELECT [AU_Code] AS value
            ,AU_Name AS [text],'null' as  selected
        FROM CT_Dealer_Empl INNER JOIN [dbo].[CT_All_Users]
        ON AU_Code = DE_AU_Code
        WHERE AU_UG_Code=@0 AND ISNULL(AU_Active_tag,1) = 1
        AND DE_UType = @1 AND DE_AD_OM_Code=@2", AU_Name);

        return db.Query<dynamic>(sql, UG_Code, UserSession.DealerEmpl.DE_UType, UserSession.DealerEmpl.DE_AD_OM_Code);
    }

    private void Get_Service(dynamic data)
    {
        dynamic o = new ExpandoObject();
        var AU_AD_Code = UserSession.DealerEmpl.DE_AD_OM_Code;

        o.schedule = Get_Schedule((int)data.DP_UType, AU_AD_Code);

        o.resources = Get_Resources(AU_AD_Code);

        o.departments = Get_Departments(AU_AD_Code);

        o.options = Get_Options(AU_AD_Code, (int)data.SD_PDN_Code);

        o.approval = Get_Approval(AU_AD_Code);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private dynamic Get_Approval(int AU_AD_Code)
    {
        CRMTree.BLL.BL_Campaign bll = new CRMTree.BLL.BL_Campaign();
        DataTable dt = bll.GetApprovalInfo(UserSession.DealerEmpl.DE_UType, AU_AD_Code, Interna, this.UserSession.User.UG_UType);
        //IEnumerable<CT_Auth_Process> approval = null; 
        //approval = CT_Auth_Process.Query(@"where AT_AD_OM_Code = @0", AU_AD_Code); 
        return dt;
    }
    private dynamic Get_Options(int AU_AD_Code, int SD_PDN_Code)
    {
        var servDep = new CT_Service_Dep();

        servDep = CT_Service_Dep.SingleOrDefault(@"where SD_AD_Code = @0 AND SD_PDN_Code = @1", AU_AD_Code, SD_PDN_Code);

        return servDep;
    }

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

    private dynamic Get_Resources(int DR_AU_AD_Code)
    {
        IEnumerable<CT_Daily_Resource> resources = null;
        if (DR_AU_AD_Code > 0)
        {
            resources = CT_Daily_Resource.Query(@"where [DR_AU_AD_Code] = @0", DR_AU_AD_Code);
        }
        return resources;
    }

    private dynamic Get_Departments(int PDN_AD_Code)
    {
        IEnumerable<dynamic> resources = null;
        if (PDN_AD_Code > 0)
        {
            var lng = Interna ? "EN" : "CN";

            var db = DBCRMTree.GetInstance();
            resources = db.Query<dynamic>(string.Format(@"
            SELECT
                PDN_Code,PDN_AD_Code,PDN_PDT_Code,PDN_Name,PDN_Variable,PDT_Var_Type,PDN_PDT_Code_Text,
                LEFT(EX_Variables, LEN(EX_Variables)-1) AS EX_Variables
            FROM(
            SELECT 
            [PDN_Code]
            ,[PDN_AD_Code]
            ,[PDN_PDT_Code]
            ,ISNULL([PDN_Name_EN],[PDN_Name_CN]) AS PDN_Name
            ,[PDN_Variable]
	        ,pdt.PDT_Var_Type
	        ,{0} AS PDN_PDT_Code_Text
            ,CAST(( SELECT   ISNULL({1}, '') + ISNULL(DV_Value, '')
                        + ', '
               FROM     CT_Dept_Variables pdv
                        LEFT JOIN CT_Dept_Values dv ON dv.DV_PDV_Code = pdv.PDV_Code
                                                       AND DV_PDN_Code = PDN_Code
               WHERE    PDV_PDT_Code = PDN_PDT_Code
             FOR XML PATH('')
             ) AS NVARCHAR(MAX)) AS EX_Variables
            FROM CT_Dept_Names pdn
            LEFT JOIN CT_Dept_Type pdt
            ON pdt.PDT_Code = pdn.PDN_PDT_Code 
            WHERE PDN_AD_Code = @0
            )t", "PDT_Name_" + lng, "PDV_Prompt_" + lng), PDN_AD_Code);
        }
        return resources;
    }
    private void Seve_Process(dynamic data)
    {
        string _sProList = JsonConvert.SerializeObject(data.saveData._data);
        List<CRM.Campaigns.CT_Auth_Process> _ProList = JsonConvert.DeserializeObject<List<CRM.Campaigns.CT_Auth_Process>>(_sProList);
        BL_Process _Pre = new BL_Process();
        int i = _Pre.Save_Provess(_ProList, (int)data.saveData.Cam_Cat, (int)data.saveData.IType, UserSession.DealerEmpl.DE_AU_Code, UserSession.DealerEmpl.DE_UType, UserSession.DealerEmpl.DE_AD_OM_Code);
        Response.Write(i);
    }
    private void Save_Service(dynamic data)
    {  
        CRMTree.BLL.BL_Campaign bll = new CRMTree.BLL.BL_Campaign();        

        var db = DBCRMTree.GetInstance();
        try
        {
            using (var tran = db.GetTransaction())
            {
                Save_Schedule(data, db);

                Save_Resources(data, db);

                Save_Options(data);

                Save_Departments(data, db);

                tran.Complete();
            }
            Save_Camp_Call(data);
            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
        catch (Exception ex)
        {
            db.AbortTransaction();
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }
    private int Save_Camp_Call(dynamic data)
    {
        var _v = (bool)data.callValiDate;
        if (_v)
        {
            string _o = JsonConvert.SerializeObject(data.callDatd);
            IList<CRM_M.Campaigns.CT_Camp_Calls> _Camp_Calls = JsonConvert.DeserializeObject<IList<CRM_M.Campaigns.CT_Camp_Calls>>(_o);
            BL_Campaign bll = new BL_Campaign();
            return bll.Save_Camp_Call(_Camp_Calls, UserSession.DealerEmpl.DE_AD_OM_Code,1);
        }
        else
        {
            return (int)_errCode.isNull;
        }
    }

    private void Save_Auth_Process(dynamic data, DBCRMTree db)
    {
        var s_eaps = JsonConvert.SerializeObject(data.auth_process.changes);
        List<EX_Auth_Process> eaps = JsonConvert.DeserializeObject<List<EX_Auth_Process>>(s_eaps);

        int i = 1;
        foreach (var eap in eaps)
        {
            CT_Auth_Process ap = new CT_Auth_Process();
            ap.AT_Code = eap.AT_Code;
            ap.AT_UType = (byte)UserSession.User.UG_UType;
            ap.AT_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
            ap.AT_Update_dt = DateTime.Now;

            ap.AT_SType = eap.AT_SType;
            ap.AT_IType = (byte?)eap.AT_IType;
            if (eap.AU_Code.HasValue && eap.AU_Code.Value > 0)
            {
                ap.AT_AType = 1;
                ap.AT_AU_UG_Code = eap.AU_Code;
            }
            if (eap.UG_Code.HasValue && eap.UG_Code.Value > 0)
            {
                ap.AT_AType = 2;
                ap.AT_AU_UG_Code = eap.UG_Code;
            }
            ap.AT_Level = (byte)i;
            i++;

            if (ap.AT_Code > 0)
            {

            }
            else
            {
                ap.AT_Created_By = UserSession.User.AU_Code;
                ap.Insert();
            }
        }

        var s_deletes = JsonConvert.SerializeObject(data.auth_process.deletes);
        List<CT_Auth_Process> deletes = JsonConvert.DeserializeObject<List<CT_Auth_Process>>(s_deletes);
        foreach (var d in deletes)
        {
            d.Delete();
        }
    }

    private void Save_Options(dynamic data)
    {
        var s_options = JsonConvert.SerializeObject(data.options);
        if (string.IsNullOrWhiteSpace(s_options))
        {
            return;
        }

        CT_Service_Dep servDep = JsonConvert.DeserializeObject<CT_Service_Dep>(s_options);
        servDep.SD_AD_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        servDep.SD_Update_dt = DateTime.Now;

        var db = DBCRMTree.GetInstance();
        if (CT_Service_Dep.Exists("SD_AD_Code = @SD_AD_Code AND SD_PDN_Code = @SD_PDN_Code", servDep))
        {
            db.Update<CT_Service_Dep>(@"SET 
            SD_SA_Selection=@SD_SA_Selection,
            SD_Serv_Package=@SD_Serv_Package,
            SD_Update_dt=@SD_Update_dt
            where SD_AD_Code = @SD_AD_Code AND SD_PDN_Code = @SD_PDN_Code", servDep);
        }
        else
        {
            servDep.Insert();
        }

    }

    private void Save_Departments(dynamic data, DBCRMTree db)
    {
        var s_departments = JsonConvert.SerializeObject(data.departments.changes);
        List<CT_Dept_Name> departments = JsonConvert.DeserializeObject<List<CT_Dept_Name>>(s_departments);

        foreach (var pdn in departments)
        {
            pdn.PDN_AD_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
            pdn.PDN_Update_dt = DateTime.Now;
            if (pdn.PDN_Code > 0)
            {
                pdn.Update(new string[]{
                    "PDN_Name_EN",
                    "PDN_Name_CN",
                    "PDN_Update_dt"
                });
            }
            else
            {
                pdn.PDN_Code = (short)pdn.Insert();
            }

            if (null != pdn.EX_Values)
            {
                foreach (var v in pdn.EX_Values)
                {
                    v.DV_PDN_Code = pdn.PDN_Code;
                    v.DV_Update_dt = DateTime.Now;

                    if (CT_Dept_Value.Exists("DV_PDN_Code = @DV_PDN_Code AND DV_PDV_Code = @DV_PDV_Code", v))
                    {
                        db.Update<CT_Dept_Value>(@"
                        SET DV_Value = @DV_Value,
                            DV_Update_dt = @DV_Update_dt
                        WHERE DV_PDN_Code = @DV_PDN_Code AND DV_PDV_Code = @DV_PDV_Code", v);
                    }
                    else
                    {
                        v.Insert();
                    }
                }
            }
        }

        var s_deletes = JsonConvert.SerializeObject(data.departments.deletes);
        List<CT_Dept_Name> deletes = JsonConvert.DeserializeObject<List<CT_Dept_Name>>(s_deletes);
        foreach (var d in deletes)
        {
            db.Execute("DELETE FROM CT_Dept_Values WHERE DV_PDN_Code = @0", d.PDN_Code);
            d.Delete();
        }
    }

    private void Save_Schedule(dynamic data, DBCRMTree db)
    {
        var s_schedule = JsonConvert.SerializeObject(data.schedule);
        if (string.IsNullOrWhiteSpace(s_schedule))
        {
            return;
        }
        CT_Daily_PLanner daily = JsonConvert.DeserializeObject<CT_Daily_PLanner>(s_schedule);
        daily.DP_AU_AD_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        daily.DP_Update_dt = DateTime.Now;
        if (CT_Daily_PLanner.Exists("DP_UType = @0 AND DP_AU_AD_Code = @1", daily.DP_UType, daily.DP_AU_AD_Code))
        {
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

    private void Save_Resources(dynamic data, DBCRMTree db)
    {
        var s_resources = JsonConvert.SerializeObject(data.resources);
        if (string.IsNullOrWhiteSpace(s_resources))
        {
            return;
        }
        List<CT_Daily_Resource> resources = JsonConvert.DeserializeObject<List<CT_Daily_Resource>>(s_resources);
        foreach (var resource in resources)
        {
            resource.DR_AU_AD_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
            resource.DR_Update_dt = DateTime.Now;
            if (CT_Daily_Resource.Exists("DR_UType = @0 AND DR_AU_AD_Code = @1", resource.DR_UType, resource.DR_AU_AD_Code))
            {
                db.Update<CT_Daily_Resource>(@"
                SET [DR_D1_AM] = @DR_D1_AM,
                    [DR_D1_PM] = @DR_D1_PM,
                    [DR_D2_AM] = @DR_D2_AM,
                    [DR_D2_PM] = @DR_D2_PM,
                    [DR_D3_AM] = @DR_D3_AM,
                    [DR_D3_PM] = @DR_D3_PM,
                    [DR_D4_AM] = @DR_D4_AM,
                    [DR_D4_PM] = @DR_D4_PM,
                    [DR_D5_AM] = @DR_D5_AM,
                    [DR_D5_PM] = @DR_D5_PM,
                    [DR_D6_AM] = @DR_D6_AM,
                    [DR_D6_PM] = @DR_D6_PM,
                    [DR_D7_AM] = @DR_D7_AM,
                    [DR_D7_PM] = @DR_D7_PM,
                    [DR_Update_dt] = @DR_Update_dt
                WHERE [DR_UType] = @DR_UType AND 
	                  [DR_AU_AD_Code] = @DR_AU_AD_Code", resource);
            }
            else
            {
                resource.Insert();
            }
        }
    }

    private void Save_Auto_Dealers(dynamic data)
    {
        var s_dealer = JsonConvert.SerializeObject(data.dealer);
        if (string.IsNullOrWhiteSpace(s_dealer))
        {
            return;
        }

        CT_Auto_Dealer dealer = JsonConvert.DeserializeObject<CT_Auto_Dealer>(s_dealer);

        dealer.AD_Update_dt = DateTime.Now;

        if (dealer.AD_Code > 0)
        {
            var cols = new string[] { 
                "AD_Name_EN",
                "AD_Name_CN",
                "AD_logo_file_S",
                "AD_logo_file_M",
                "AD_logo_file_L",
                "AD_Update_dt"
            };
            var listCols = cols.ToList<string>();
            dealer.Update(listCols);
        }
        else
        {
            dealer.Insert();
        }

        var path = "~/images/DealersLogo/";
        Save_File(path, dealer.AD_logo_file_S, dealer.AD_logo_file_S_Temp);
        Save_File(path, dealer.AD_logo_file_M, dealer.AD_logo_file_M_Temp);
        Save_File(path, dealer.AD_logo_file_L, dealer.AD_logo_file_L_Temp);
    }

    private void Save_File(string filePath, string file, string file_temp)
    {
        if (file != file_temp)
        {
            var path_save = Server.MapPath(filePath);
            //添加或更换图片
            if (!string.IsNullOrWhiteSpace(file))
            {
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
                if (File.Exists(path_temp + file))
                {
                    File.Move(path_temp + file, path_save + file);
                }
            }

            if (!string.IsNullOrWhiteSpace(file_temp))
            {
                //删除图片
                var imgPath = path_save + file_temp;
                if (File.Exists(imgPath))
                {
                    File.Delete(imgPath);
                }
            }
        }
    }

    private void Get_User_Groups()
    {
        var o = Get_CT_User_Groups();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_All_Users(int UG_Code)
    {
        var o = Get_CT_All_Users(UG_Code);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Dept_Type()
    {
        var o = Get_CT_Dept_Type();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private IEnumerable<dynamic> Get_CT_Dept_Type()
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[PDT_Name_EN]" : "[PDT_Name_CN]";
        var sql = string.Format(@"
            SELECT {0} as [text],PDT_Code as [value],[PDT_Var_Type] FROM CT_Dept_Type",
            sql_text_part);

        return db.Query<dynamic>(sql);
    }
}