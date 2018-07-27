using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMTree.Public;
using CRMTreeDatabase;

public partial class manage_campaign_CampaignManagerN : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var Q_CG_Code = Request.QueryString["CG_Code"];
            int CG_Code = 0;
            if (null != Q_CG_Code)
            {
                try
                {
                    CG_Code = Convert.ToInt32(Q_CG_Code.ToString());
                }
                catch (Exception)
                {
                }
            }

 //        The following section was removed because the Apporval funcion was moved to CampaignManagerN.aspx program-- Fariborz
            //if (CG_Code > 0)
            //{
            //    var campaign = CT_Campaign.SingleOrDefault(CG_Code);
            //    if (null != campaign)
            //    {
            //        if (campaign.CG_Status.HasValue && campaign.CG_Status == 5)
            //        {
            //            Response.Redirect("/manage/campaign/CampaignApprove.aspx?CT=" + campaign.CG_Type + "&CG_Code=" + campaign.CG_Code);
            //        }
            //    }
            //}
        }
    }
}