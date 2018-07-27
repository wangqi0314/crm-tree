using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMTree.Public;
using System.Data;
using ShInfoTech.Common;
using System.Data.SqlClient;

public partial class manage_customer_IncomingCalls : BasePage
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