using CRMTree.BLL;
using CRMTree.Model;
using CRMTree.Model.Reports;
using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_Report_Update_Targeted : BasePage
{
    private static int AU_Type = -1;
    private static int AU_AD_OM_Code = -1;
    static bool Intern = true;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            Intern = Interna;
            AU_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
            AU_Type = UserSession.User.UG_UType;
        }
    }
    /// <summary>
    /// 弹出界面 输出参数列表
    /// </summary>
    /// <param name="RP_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static Object getParamterslist(int RP_Code)
    {
        BL_Reports Report = new BL_Reports();
        return Report.GetParamterslist(Intern, new CT_Param_Value(){ RP_Code=RP_Code,PV_Type=0,PV_CG_Code=-1,PV_UType=AU_Type,PV_AD_OM_Code=AU_AD_OM_Code});
    }
    [WebMethod]
    public static int updateTags(string PL_Codes, string Paramters)
    {
        CRMTree.BLL.BL_Reports rp = new CRMTree.BLL.BL_Reports();
        return rp.insertTags(AU_Type, AU_AD_OM_Code, PL_Codes, Paramters);
    }
}