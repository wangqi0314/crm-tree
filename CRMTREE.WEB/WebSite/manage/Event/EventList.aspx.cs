using CRMTree.BLL;
using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_Event_EventList : BasePage
{
    public static long AU_Code = -1;
    public static int AU_Type = -1;
    private static int AU_AD_OM_Code = -1;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.CrmTreetop.UserID = UserSession.User.AU_Code;
            AU_Code = UserSession.User.AU_Code;
            AU_Type = UserSession.User.UG_UType;
            AU_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        }
    }
    /// <summary>
    /// 提供执行Campaign活动的一部方法
    /// </summary>
    /// <param name="CG_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static int EventRun(int CG_Code)
    {
        BL_Event Ev = new BL_Event();
        int err = Ev.EventRun(CG_Code, AU_Type, AU_AD_OM_Code);
        return err;
    }
}