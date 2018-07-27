using System;
using CRMTree.Public;
using System.Web.Services;
using CRMTree.BLL;
using System.Data;

public partial class manage_campaign_list_campaign : BasePage
{
    public static long AU_Code = -1;
    public static int AU_Type = -1;
    private static int AU_AD_OM_Code = -1;

    public string name_Table = string.Empty;
    public string name_Chart = string.Empty;
    public string name_Table_value = string.Empty;
    public string name_Chart_value = string.Empty;

    protected string CurrentPage = "";
    protected string MI_Name = "";
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!IsPostBack)
        {
            this.CrmTreetop.UserID = UserSession.User.AU_Code;
            AU_Code = UserSession.User.AU_Code;
            AU_Type = UserSession.User.UG_UType;
            AU_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;

            CurrentPage = new BL_TabLinkList().GetLevelLink(UserSession.User.AU_UG_Code, Request.RawUrl, Interna, out MI_Name);
        }
        
        if (Interna)
        {
            name_Table = BL_Reports.GetValues("4103", "text_en");
            name_Table_value = BL_Reports.GetValues("4103", "value");
            name_Chart = BL_Reports.GetValues("4104", "text_en");
            name_Chart_value = BL_Reports.GetValues("4104", "value");
        }
        else
        {
            name_Table = BL_Reports.GetValues("4103", "text_cn");
            name_Table_value = BL_Reports.GetValues("4103", "value");
            name_Chart = BL_Reports.GetValues("4104", "text_cn");
            name_Chart_value = BL_Reports.GetValues("4104", "value");
        }
    }
    [WebMethod]
    public static int CampaignRun(int CG_Code)
    {
        BL_Campaign Cam = new BL_Campaign();
        int err = Cam.CampaignRun(CG_Code, AU_Type, AU_AD_OM_Code);
        return err;
    }
}