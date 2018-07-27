using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMTree.Public;

public partial class templete_report_DealerAdmin : BasePage
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