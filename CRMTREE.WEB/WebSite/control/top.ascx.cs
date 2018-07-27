using System;
using System.Text;
using System.Data;
using ShInfoTech.Common;
using System.Web.Services;
using Resources;
using CRMTree;
using CRMTree.Public;
using System.Web;
using Shinfotech.Tools;
using CRMTree.Model.User; 
public partial class control_top : System.Web.UI.UserControl
{
    private long _userid = 0;
    protected string realName = string.Empty;
    protected string currentTime = string.Empty;
    protected string lastLoginTime = string.Empty;
    protected int groupid = 0;
    protected string Languages = string.Empty;
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
        bool Interna = true;
        if (!IsPostBack)
        {
            currentTime = DateTime.Now.ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(this.Session["PublicUser"].ToString()))
            {
                realName = ((MD_UserEntity)this.Session["PublicUser"]).User.AU_Name;
            }
            CRMTree.BLL.BL_TabLinkList TabLink = new CRMTree.BLL.BL_TabLinkList();
            if (Language.GetLang2() == CRMTree.Model.Common.EM_Language.en_us)
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
   