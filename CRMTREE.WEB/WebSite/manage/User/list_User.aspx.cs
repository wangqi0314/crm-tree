using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_User_list_User : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.top1.UserID = UserSession.User.AU_Code;
            int UG_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        }
    }
}