using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class manage_campaign_Campaign_New : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.top.UserID = UserSession.User.AU_Code;
        }
    }
}