using System;
using System.Text;
using CRMTree.Public;
using Shinfotech.Tools;
using System.Data;
using System.Web.Services;
using CRMTree.BLL;
using CRMTree.Model.Reports;
using CRMTree.Model.Event;
using CRMTree.Model;
using CRMTree.Model.Common;
public partial class manage_campaign_edit_campaign : BasePage
{
    public string strDLL_Option = String.Empty;
    public string strDLL_Succ = String.Empty;
    public static bool Inter = true;
    public static int S_RP_Code = -1;

    public CRMTree.Model.CT_Campaigns Campaign = new CRMTree.Model.CT_Campaigns();

    public static int AU_Type = -1;
    private static int AU_AD_OM_Code = -1;
    public static int UG_Code = -1;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            Inter = Interna;
            this.CrmTreetop.UserID = UserSession.User.AU_Code;
            AU_Type = UserSession.User.UG_UType;
            AU_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
            UG_Code = UserSession.User.AU_UG_Code;            
            Init_Page();
            strDLL_Option = getCamReprotList();
            strDLL_Succ = getSuccMatrxList();
            S_RP_Code = Campaign.CG_RP_Code;
        }
    }

    private string getSuccMatrxList()
    {
        BL_Event Event = new BL_Event();
        MD_SuccMatrixList SuccMatrix = Event.getSuccMatrxList(Interna);
        if (SuccMatrix != null && SuccMatrix.SuccMatrixList.Count > 0)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < SuccMatrix.SuccMatrixList.Count; i++)
            {
                str.Append("<option value='" + SuccMatrix.SuccMatrixList[i].PSM_Code + "'>" + SuccMatrix.SuccMatrixList[i].PSM_Desc_EN + "</option>");
            }
            return str.ToString();
        }
        return "";
    }
    private void Init_Page()
    {
        int intCG_Code = RequestClass.GetInt("id", 0);
        if (intCG_Code <= 0)
        {
            Campaign.CG_Start_Dt = DateTime.Now;
            Campaign.CG_End_Dt = DateTime.Now.AddDays(5);
        }
        else
        {
            CRMTree.BLL.BL_Campaign Cam = new CRMTree.BLL.BL_Campaign();
            Campaign = Cam.GetCampaign(intCG_Code);
            if (Campaign == null || string.IsNullOrEmpty(Campaign.CG_Title))
            {
                string str = "<script language=javascript>history.go(-1);</script>";
                //Page.RegisterClientScriptBlock("key", str);
            }
        }
    }
    private string getCamReprotList()
    {
        BL_Campaign Cam = new BL_Campaign();
        MD_ReportList reportP = Cam.getCampaignReprotList(Interna);
        if (reportP != null && reportP.CT_Reports_List.Count > 0)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < reportP.CT_Reports_List.Count; i++)
            {
                str.Append("<option value='" + reportP.CT_Reports_List[i].RP_Code + "'>" + reportP.CT_Reports_List[i].RP_Name_EN + "</option>");
            }
            return str.ToString();
        }
        return "";

    }

    /// <summary>
    /// Matrx 列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static MD_SuccMatrixList getSuccMatrxList_s()
    {
        BL_Event Event = new BL_Event();
        return Event.getSuccMatrxList(Inter);
    }
    [WebMethod]
    public static MD_SuccMatrixList getSuccMatrx_s(int Code, string Code_s)
    {
        BL_Event Event = new BL_Event();
        CT_Succ_Matrix succ = new CT_Succ_Matrix { PSM_Code_s = Code_s, SMV_CG_Code = Code, SMV_Type = 1 };
        return Event.getSuccMatrxList(Inter, succ);
    }


    [WebMethod]
    public static CT_Reports getReplaceReports(string RP_Code, int CG_Code, string Paramterslist)
    {
        BL_Reports Report = new BL_Reports();
        return Report.GetReplaceReport(
            Inter,
            new CT_Param_Value() { RP_Code = Convert.ToInt32(RP_Code), PV_Type = 1, PV_CG_Code = CG_Code, PV_UType = AU_Type, PV_AD_OM_Code = AU_AD_OM_Code },
            EM_ParameterMode.Page,
            Paramterslist);
    }
    [WebMethod]
    public static CT_Reports getReplaceReport(int CG_Code)
    {
        BL_Reports Report = new BL_Reports();
        return Report.GetReplaceReport(Inter,
            new CT_Param_Value() { RP_Code = S_RP_Code, PV_Type = 1, PV_CG_Code = CG_Code, PV_UType = AU_Type, PV_AD_OM_Code = AU_AD_OM_Code });
    }
}