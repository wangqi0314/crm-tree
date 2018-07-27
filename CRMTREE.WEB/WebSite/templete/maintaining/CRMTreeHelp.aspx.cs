using CRMTree.Model.Common;
using CRMTree.Model.User;
using CRMTree.Public;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_Help_CRMTreeHelp : BasePage
{
    protected string pagePath = string.Empty;
    protected StringBuilder pageStr = new StringBuilder();
    protected string pageOnLoad = String.Empty;
    protected long au_Code = 0;
    protected string Languages = string.Empty;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(Request.UrlReferrer.PathAndQuery))
            pagePath = Request.UrlReferrer.PathAndQuery;

        bool Interna = true;
        if (!IsPostBack)
        {
            if (this.Session["PublicUser"] != null && !string.IsNullOrEmpty(this.Session["PublicUser"].ToString()))
            {
                au_Code = ((MD_UserEntity)this.Session["PublicUser"]).User.AU_Code;
                MD_UserEntity User = (MD_UserEntity)this.Session["PublicUser"];
               
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
            pageStr = TabLink.getTabLinkListForHelpSystem(au_Code, Interna, pagePath,out pageOnLoad);

        }
    }
}