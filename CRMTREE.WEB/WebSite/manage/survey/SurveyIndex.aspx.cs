using CRMTree.BLL;
using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_survey_SurveyIndex : BasePage
{
    protected string CurrentPage = "";
    protected string MI_Name = "";
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.top.UserID = UserSession.User.AU_Code;

            CurrentPage = new BL_TabLinkList().GetLevelLink(UserSession.User.AU_UG_Code, Request.RawUrl, Interna, out MI_Name);

        }
    }
}