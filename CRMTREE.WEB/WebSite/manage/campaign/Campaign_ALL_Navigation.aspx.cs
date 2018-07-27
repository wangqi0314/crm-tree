using CRMTree.Public;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_campaign_Campaign_ALL_Navigation : BasePage
{
    protected string TemplList = string.Empty;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            int CA = Tools.Request.Int("CA", 1);
            if (CA == 1)
            {
                TemplList = Resources.CRMTREESResource.TemplList;
            }
            else if (CA == 2)
            {
                TemplList = Resources.CRMTREESResource.TemplList2;
            }
            else if (CA == 3)
            {
                TemplList = Resources.CRMTREESResource.TemplList3;
            }
        }
    }
}