using CRMTree.BLL;
using CRMTree.Model;
using CRMTree.Model.MyCar;
using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_campaign_UpdateTargeted : BasePage
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
    /// 获取配置的参数列表集合
    /// </summary>
    /// <param name="RP_Code"></param>
    /// <param name="CG_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static Object getParamterslist(int RP_Code, int CG_Code)
    {
        BL_Reports Report = new BL_Reports();
        return Report.GetParamterslist(Intern, 
            new CT_Param_Value() { RP_Code = RP_Code, PV_Type = 2, PV_CG_Code = CG_Code, PV_UType = AU_Type, PV_AD_OM_Code = AU_AD_OM_Code });
    }
    /// <summary>
    /// 获取Make,i 表示，针对Param_List参数的type 11 和 12 准备的
    /// </summary>
    /// <param name="i">i=1,表示type=11；i=2，表示type=12</param>
    /// <param name="MK_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static Object getCarMake(int i, int CM_Code)
    {
        BL_MyCar Car = new BL_MyCar();
        CT_Make CarMake = Car.getCarMake(i,CM_Code);
        if (CarMake != null && !Intern)
        {
            CarMake.MK_Make_EN = CarMake.MK_Make_CN;
        }
        return CarMake;
    }
    /// <summary>
    /// 获取CarMode
    /// </summary>
    /// <param name="MK_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static Object getMyCarMode(int i,int CM_Code)
    {
        BL_MyCar myCar = new BL_MyCar();
        CT_Car_Model Model = myCar.getCarModel(i, CM_Code);
        if (Model != null && !Intern)
        {
            Model.CM_Model_EN = Model.CM_Model_CN;
        }
        return Model;
    }
    [WebMethod]
    public static Object getMyCarStyle(int CS_Code)
    {
        BL_MyCar myCar = new BL_MyCar();
        CT_Car_Style Style = myCar.getMyStyle(CS_Code);
        if (Style != null && !Intern)
        {
            Style.CS_Style_EN = Style.CS_Style_CN;
        }
        return Style;
    }
}