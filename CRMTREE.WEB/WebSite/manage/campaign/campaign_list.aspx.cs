using System;
using CRMTree.Public;
using System.Web.Services;
using CRMTree.BLL;

public partial class manage_campaign_list_campaign : BasePage
{
    public static long AU_Code = -1;
    public static int AU_Type = -1;
    private static int AU_AD_OM_Code = -1;

    public string name_Maintain = string.Empty;
    public string name_Obtain = string.Empty;
    public string name_Branding = string.Empty;
    public string name_Events = string.Empty;
    public string name_Surveys = string.Empty;
    public string name_Reminders = string.Empty;
    public string name_Newsletters = string.Empty;
    public string name_Tips = string.Empty;

    public string name_Maintain_value = string.Empty;
    public string name_Obtain_value = string.Empty;
    public string name_Branding_value = string.Empty;
    public string name_Events_value = string.Empty;
    public string name_Surveys_value = string.Empty;
    public string name_Reminders_value = string.Empty;
    public string name_Newsletters_value = string.Empty;
    public string name_Tips_value = string.Empty;

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

        //name_Maintain_value = BL_Reports.GetValues("4094", "value");
        //name_Obtain_value = BL_Reports.GetValues("4095", "value");
        //name_Branding_value = BL_Reports.GetValues("4096", "value");
        //name_Events_value = BL_Reports.GetValues("4097", "value");
        //name_Surveys_value = BL_Reports.GetValues("4098", "value");
        //name_Reminders_value = BL_Reports.GetValues("4099", "value");
        //name_Newsletters_value = BL_Reports.GetValues("4100", "value");
        //name_Tips_value = BL_Reports.GetValues("4101", "value");

        //if (Interna)
        //{
        //    name_Maintain = BL_Reports.GetValues("4094", "text_en");
        //    name_Obtain = BL_Reports.GetValues("4095", "text_en");
        //    name_Branding = BL_Reports.GetValues("4096", "text_en");
        //    name_Events = BL_Reports.GetValues("4097", "text_en");
        //    name_Surveys = BL_Reports.GetValues("4098", "text_en");
        //    name_Reminders = BL_Reports.GetValues("4099", "text_en");
        //    name_Newsletters = BL_Reports.GetValues("4100", "text_en");
        //    name_Tips = BL_Reports.GetValues("4101", "text_en");
        //}
        //else
        //{
        //    name_Maintain = BL_Reports.GetValues("4094", "text_cn");
        //    name_Obtain = BL_Reports.GetValues("4095", "text_cn");
        //    name_Branding = BL_Reports.GetValues("4096", "text_cn");
        //    name_Events = BL_Reports.GetValues("4097", "text_cn");
        //    name_Surveys = BL_Reports.GetValues("4098", "text_cn");
        //    name_Reminders = BL_Reports.GetValues("4099", "text_cn");
        //    name_Newsletters = BL_Reports.GetValues("4100", "text_cn");
        //    name_Tips = BL_Reports.GetValues("4101", "text_cn");
        //}

    }
    /// <summary>
    /// 提供执行Campaign活动的一部方法
    /// </summary>
    /// <param name="CG_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static int CampaignRun(int CG_Code)
    {
        BL_Campaign Cam = new BL_Campaign();
        int err=Cam.CampaignRun(CG_Code,AU_Type, AU_AD_OM_Code);
        return err;
    }
}