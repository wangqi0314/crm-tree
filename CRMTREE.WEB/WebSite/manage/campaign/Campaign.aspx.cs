using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMTree.Public;
using System.Data;
using ShInfoTech.Common;
using System.Data.SqlClient;
using CRMTree.BLL;

public partial class manage_campaign_Campaign : BasePage
{

    protected string CurrentPage = "";
    protected string MI_Name = "";
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.top.UserID = UserSession.User.AU_Code;
            string _s = this.Page.Request.UrlReferrer.PathAndQuery;
            CurrentPage = new BL_TabLinkList().GetLevelLink(UserSession.User.AU_UG_Code, _s, Interna, out MI_Name);
        }
    }
}