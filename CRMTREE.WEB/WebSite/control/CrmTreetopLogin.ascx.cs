using System;
using System.Text;
using System.Data;
using ShInfoTech.Common;
using CRMTree.Public;
using CRMTree.Model.User;
using CRMTree.Model.Common;

public partial class control_CrmTreetopLogin : System.Web.UI.UserControl
{
    private long _userid = 0;
    protected string realName = string.Empty;
    protected string currentTime = string.Empty;
    protected string lastLoginTime = string.Empty;
    protected int groupid = 0;
    protected string Languages = string.Empty;
    protected string Logo = string.Empty;
    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserID
    {
        get { return this._userid; }
        set { this._userid = value; }
    }

    protected StringBuilder strNav = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool Interna = false;
        if (!IsPostBack)
        {

            currentTime = DateTime.Now.ToString("yyyy-MM-dd");
            if (this.Session["PublicUser"]!=null&&!string.IsNullOrEmpty(this.Session["PublicUser"].ToString()))
            {
                realName = ((MD_UserEntity)this.Session["PublicUser"]).User.AU_Name;
                MD_UserEntity User = (MD_UserEntity)this.Session["PublicUser"];
                if (User.User.UG_UType == 1)
                {
                    Logo = User.Dealer.AD_logo_file_M;
                }
                else if (User.User.UG_UType == 2)
                {
                    Logo = User.DealerGroup.DG_Logo_file_M;
                }
                else if (User.User.UG_UType == 3)
                {
                    Logo = User.OEM.OM_Logo_file_M;
                }
            }
            CRMTree.BLL.BL_TabLinkList TabLink = new CRMTree.BLL.BL_TabLinkList();
            if (Language.GetLang2() == EM_Language.en_us)
            {
                Interna = true;
                Languages = "2";
            }
            else
            {
                Interna = false;
                Languages = "1";
            }
            strNav = TabLink.getTabLinkList(UserID, Interna);
        }
    }
}