using System;
using CRMTree.Public;
using System.Web.Services;
using CRMTree.BLL;

public partial class manage_campaign_list_campaign : BasePage
{
    public static bool _Interna;
    public static long AU_Code = -1;
    public static int AU_Type = -1;
    private static int AU_AD_OM_Code = -1;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.CrmTreetop.UserID = UserSession.User.AU_Code;
            _Interna = Interna;
            AU_Code = UserSession.User.AU_Code;
            AU_Type = UserSession.User.UG_UType;
            AU_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        }
    }
    [WebMethod]
    public static int CampaignRun(int CG_Code)
    {
        BL_Campaign Cam = new BL_Campaign();
        int err = Cam.CampaignRun(CG_Code, AU_Type, AU_AD_OM_Code);
        return err;
    }

    [WebMethod]
    public static string getFields_List_Code(int RP_Code)
    {
        BL_Reports RP = new BL_Reports();
        string err = RP.GetFields_List_Code(RP_Code);
        return err;
    }
    [WebMethod]
    public static string Expore_Ex(int Pl_Code,int PR)
    {
        string fileName = string.Empty;
        BL_Export EP = new BL_Export(Pl_Code, PR);
        EP.Expore_Ex(Pl_Code, PR, out fileName);
        return fileName;
    }
}