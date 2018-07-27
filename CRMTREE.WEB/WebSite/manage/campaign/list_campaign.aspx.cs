using System;
using CRMTree.Public;
using System.Web.Services;
using CRMTree.BLL;
using CRMTree.Model.User;
using System.Web;

public partial class manage_campaign_list_campaign : BasePage
{
    public static long AU_Code = -1;
    public static int AU_Type = -1;
    private static int AU_AD_OM_Code = -1;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.CrmTreetop.UserID = UserSession.User.AU_Code;
            AU_Code = UserSession.User.AU_Code;
            AU_Type = UserSession.User.UG_UType;
            AU_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        }
    }
    /// <summary>
    /// 提供执行Campaign活动的一部方法
    /// </summary>
    /// <param name="CG_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static int CampaignRun(int CG_Code)
    {
        MD_UserEntity o = checkUser();
        if (o == null) { return -1; }
        BL_Campaign Cam = new BL_Campaign();
        int err = Cam.CampaignRun(CG_Code, o.User.UG_UType, o.DealerEmpl.DE_AD_OM_Code);
        return err;
    }
    private static MD_UserEntity checkUser()
    {
        if (HttpContext.Current.Session["PublicUser"] == null)
        {
            return null;
        }
        return (MD_UserEntity)HttpContext.Current.Session["PublicUser"];
    }
}