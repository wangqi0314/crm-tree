using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using CRMTree.Public;
using Shinfotech.Tools;

public partial class manage_car_edit_car : BasePage
{
    static long userCode = -1;
    static bool Intern = true;
    public string strFilename = String.Empty;
    protected override void OnLoad(EventArgs e)
    {

        base.OnLoad(e);
        if (!IsPostBack)
        {
            Intern = Interna;
            userCode = UserSession.User.AU_Code;
            this.top1.UserID = UserSession.User.AU_Code;
            int UG_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        }
       
       
    }
    
    #region Customers Targeted 数据加载
    [WebMethod]
    public static Object getMyCarTypeList()
    {

        CRMTree.BLL.BL_MyCar myCar = new CRMTree.BLL.BL_MyCar();
        CRMTree.Model.MyCar.MD_CarTypeList myCarStyle = myCar.getMyCarTypeList();
        if (myCarStyle != null && !Intern)
        {
            for (int i = 0; i < myCarStyle.Car_Type_List.Count; i++)
            {
                myCarStyle.Car_Type_List[i].CT_Type_EN = myCarStyle.Car_Type_List[i].CT_Type_CN;
            }
        }
        return myCarStyle;
    }
    [WebMethod]
    public static Object getMyCatYearsList()
    {
        CRMTree.BLL.BL_MyCar myCar = new CRMTree.BLL.BL_MyCar();
        CRMTree.Model.MyCar.MD_CarYears myCarYear = myCar.getMyCarYearsList();
        return myCarYear;
    }
    [WebMethod]
    public static Object getMyCatColorsList()
    {
        CRMTree.BLL.BL_MyCar myCar = new CRMTree.BLL.BL_MyCar();
        CRMTree.Model.MyCar.MD_CarColorsList myCarColorE = myCar.getMyCatColorsList();
        if (myCarColorE != null && !Intern)
        {
            for (int i = 0; i < myCarColorE.Car_Color.Count; i++)
            {
                myCarColorE.Car_Color[i].CL_Color_EN = myCarColorE.Car_Color[i].CL_Color_CN;
            }
        }
        return myCarColorE;
    }
     [WebMethod]
    public static Object getMyCatColorsListI()
    {
        CRMTree.BLL.BL_MyCar myCar = new CRMTree.BLL.BL_MyCar();
        CRMTree.Model.MyCar.MD_CarColorsList myCarColorI = myCar.getMyCatColorsListI();
        if (myCarColorI != null && !Intern)
        {
            for (int i = 0; i < myCarColorI.Car_Color_I.Count; i++)
            {
                myCarColorI.Car_Color_I[i].CL_Color_EN = myCarColorI.Car_Color_I[i].CL_Color_CN;
            }
        }
        return myCarColorI;
    }

    #endregion
}