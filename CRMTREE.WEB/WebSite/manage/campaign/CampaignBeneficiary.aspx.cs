using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_campaign_CampaignBeneficiary : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.CrmTreetop.UserID = UserSession.User.AU_Code;
            //AU_Code = UserSession.AU_Code;
            //AU_Type = UserSession.AU_Utype;
            //AU_AD_OM_Code = UserSession.UA_AD_OM_Code;
        }
    }
}