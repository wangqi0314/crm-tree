using CRMTree.BLL;
using CRMTree.Model;
using CRMTree.Model.Event;
using CRMTree.Model.Reports;
using CRMTree.Public;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using CRMTree.Model.Common;

public partial class manage_campaign_Camp2_Edit : BasePage
{
    public CT_Events Event = new CT_Events();
    public string strDLL_Option = String.Empty;
    public string strDLL_Genre = String.Empty;
    public string strDLL_Succ = String.Empty;
    public static bool Inter = true;
    public static int S_RP_Code = -1;
    private static int AU_Type = -1;
    private static int AU_AD_OM_Code = -1;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            Inter = Interna;
            this.CrmTreetop.UserID = UserSession.User.AU_Code;
            Init_Page();
            strDLL_Option = getCamReprotList();
            strDLL_Genre = getGenre();
            strDLL_Succ = getSuccMatrxList();
            S_RP_Code = Event.EV_RP_Code;
            AU_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
            AU_Type = UserSession.User.UG_UType;
        }
    }
    private void Init_Page()
    {
        int EV_Code = RequestClass.GetInt("id", 0);
        if (EV_Code <= 0)
        {
            Event.EV_Start_dt = DateTime.Now;
            Event.EV_End_dt = DateTime.Now.AddDays(5);
            Event.EV_Act_S_dt = DateTime.Now;
            Event.EV_Act_E_dt = DateTime.Now.AddDays(5);
        }
        else
        {
            BL_Event Bl_event = new BL_Event();
            Event = Bl_event.getEvents(EV_Code);
            if (Event == null || string.IsNullOrEmpty(Event.EV_Title))
            {
                string str = "<script language=javascript>history.go(-1);</script>";
                Page.RegisterClientScriptBlock("key", str);
            }
        }
    }
    private string getCamReprotList()
    {
        BL_Event Event = new BL_Event();
        MD_ReportList Report = Event.getEventReprotList(Interna);
        if (Report != null && Report.CT_Reports_List.Count > 0)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < Report.CT_Reports_List.Count; i++)
            {
                str.Append("<option value='" + Report.CT_Reports_List[i].RP_Code + "'>" + Report.CT_Reports_List[i].RP_Name_EN + "</option>");
            }
            return str.ToString();
        }
        return "";
    }
    private string getGenre()
    {
        BL_Event Event = new BL_Event();
        MD_GenreList Genre = Event.getGenre(UserSession.User.UG_UType, UserSession.DealerEmpl.DE_AD_OM_Code);
        if (Genre != null && Genre.GenreList.Count > 0)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < Genre.GenreList.Count; i++)
            {
                str.Append("<option value='" + Genre.GenreList[i].EG_Code + "'>" + Genre.GenreList[i].EG_Desc + "</option>");
            }
            return str.ToString();
        }
        return "";
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
    /// <summary>
    /// Person列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static MD_EventPersonList getEventPersonList()
    {
        BL_Event Event = new BL_Event();
        return Event.getEventPersonList(Inter);
    }
    /// <summary>
    /// Recommended Tools列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static MD_EventToolsList getEventToolsList()
    {
        BL_Event Event = new BL_Event();
        return Event.getEventToolsList(Inter);
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
        CT_Succ_Matrix succ = new CT_Succ_Matrix { PSM_Code_s = Code_s, SMV_CG_Code = Code, SMV_Type = 2 };
        return Event.getSuccMatrxList(Inter, succ);
    }


    [WebMethod]
    public static CT_Reports getReplaceReport(int CG_Code)
    {
        BL_Reports Report = new BL_Reports();
        return Report.GetReplaceReport(Inter,
            new CT_Param_Value() { RP_Code = S_RP_Code, PV_Type = 2, PV_CG_Code = CG_Code, PV_UType = AU_Type, PV_AD_OM_Code = AU_AD_OM_Code });
    }
    [WebMethod]
    public static CT_Reports getReplaceReports(string RP_Code, int CG_Code, string Paramterslist)
    {
        BL_Reports Report = new BL_Reports();
        return Report.GetReplaceReport(
            Inter,
            new CT_Param_Value() { RP_Code = Convert.ToInt32(RP_Code), PV_Type = 2, PV_CG_Code = CG_Code, PV_UType = AU_Type, PV_AD_OM_Code = AU_AD_OM_Code },
            EM_ParameterMode.Page,
            Paramterslist);
    }
}
