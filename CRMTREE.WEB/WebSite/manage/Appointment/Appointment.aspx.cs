using CRMTree.Model.MyCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using CRMTree.Public;
using CRMTree;

public partial class customer_Appointment : BasePage
{
    static long userCode = -1;
    static bool Intern = true;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            Intern = Interna;
            userCode = UserSession.User.AU_Code;
            this.top1.UserID = UserSession.User.AU_Code;
            int UG_Code = UserSession.User.UG_UType;
            getMyCar();
        }
    }
    private void getMyCar()
    {
        CRMTree.BLL.BL_MyCar myCar = new CRMTree.BLL.BL_MyCar();
        MD_CarList myCarList = myCar.GetCarList(UserSession.User.AU_Code, true);
        this.myCarInfoList.DataSource = myCarList.Car_Inventory_List;
        this.myCarInfoList.DataBind();
    }
    [WebMethod]
    public static Object AddApp(string Data, string type)
    {
        CRMTree.BLL.BL_Appt_Service myAppointmens = new CRMTree.BLL.BL_Appt_Service();
        CRMTree.Model.CT_Appt_Service App = new CRMTree.Model.CT_Appt_Service();
        string[] Datas = Data.Split(',');
        App.AP_AU_Code = userCode;
        App.AP_CI_Code = int.Parse(Datas[0].ToString());
        App.AP_AD_Code = int.Parse(Datas[1].ToString());
        App.AP_Time = Convert.ToDateTime(Datas[2].ToString() + " " + Datas[3].ToString());
        if (string.IsNullOrEmpty(Datas[4].ToString()))
        {
            App.AP_SA_Selected = null;
        }
        else
        {
            App.AP_SA_Selected = int.Parse(Datas[4].ToString());
        }
        if (int.Parse(type) == 1)
        {
            App.AP_MP_Code = int.Parse(Datas[5].ToString());
        }
        else
        {
            if (string.IsNullOrEmpty(Datas[5].ToString()))
            {
                App.AP_ST_Code = null;
            }
            else
            {
                App.AP_ST_Code = int.Parse(Datas[5].ToString());
            }
        }
        App.AP_Transportation = null;
        CRMTree.Model.CT_Appt_Service Apps = myAppointmens.InsertApp(App);
        return Apps;
    }
    [WebMethod]
    public static Object MyCarStyke(string CarId)
    {
        CRMTree.BLL.BL_Appt_Service myAppointmens = new CRMTree.BLL.BL_Appt_Service();
        CRMTree.Model.Appointmens.MD_AppointmensList myCarStyle = myAppointmens.getMyCarStyle(6);
        return myCarStyle;
    }
    /// <summary>
    /// 根据用户获取推荐的经销商
    /// </summary>
    /// <param name="AU_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static Object MyDealerList(string AU_Code)
    {
        CRMTree.BLL.BL_Appt_Service myAppointmens = new CRMTree.BLL.BL_Appt_Service();

        CRMTree.Model.Appointmens.MD_DealerList myDealerList = myAppointmens.getDealerList(Convert.ToInt64(userCode));
        if (myDealerList != null && !Intern)
        {
            for (int i = 0; i < myDealerList.Dealers_List.Count; i++)
            {
                myDealerList.Dealers_List[i].AD_Name_EN = myDealerList.Dealers_List[i].AD_Name_CN;
            }
        }
        return myDealerList;
    }
    /// <summary>
    /// 根据顾问ID获取顾问
    /// </summary>
    /// <param name="AD_code"></param>
    /// <returns></returns>
    [WebMethod]
    public static Object MyAdviserList(int AD_code)
    {
        CRMTree.BLL.BL_Appt_Service myAppointmens = new CRMTree.BLL.BL_Appt_Service();
        CRMTree.Model.Appointmens.MD_AdviserList myAdviserList = myAppointmens.getAdviserList(AD_code);
        return myAdviserList;
    }
    /// <summary>
    /// 根据CarId 经销商ID获取默认推荐的顾问
    /// </summary>
    /// <param name="CI_Code"></param>
    /// <param name="AD_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static Object getDefaultRecomAdviser(string CI_Code, string AD_Code)
    {
        if (CI_Code != "null" && AD_Code != "null" && !string.IsNullOrEmpty(CI_Code) && !string.IsNullOrEmpty(AD_Code))
        {
            CRMTree.BLL.BL_Advisers myAdvisers = new CRMTree.BLL.BL_Advisers();
            CRMTree.Model.CT_Dealer_Empl myServiceAdviser = myAdvisers.getDefaultRecomAdviser(int.Parse(CI_Code), int.Parse(AD_Code));
            return myServiceAdviser;
        }
        else
            return null;
    }
    /// <summary>
    /// 获取预约的类别
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static Object getServCateList()
    {
        CRMTree.BLL.BL_Appt_Service myAppointmens = new CRMTree.BLL.BL_Appt_Service();
        CRMTree.Model.Appointmens.MD_ServCategoryList myServCateList = myAppointmens.getServList();
        if (myServCateList != null && !Intern)
        {
            for (int i = 0; i < myServCateList.Serv_Category_List.Count; i++)
            {
                myServCateList.Serv_Category_List[i].SC_Desc_EN = myServCateList.Serv_Category_List[i].SC_Desc_CN;
            }
        }
        return myServCateList;
    }
    /// <summary>
    /// 获取服务的类型
    /// </summary>
    /// <param name="SC_Code"></param>
    /// <param name="AD_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static Object getServiceType(string SC_Code, string AD_Code)
    {
        CRMTree.Model.CT_Service_Types ServiceType = new CRMTree.Model.CT_Service_Types();
        ServiceType.ST_SC_Code = int.Parse(SC_Code);
        ServiceType.ST_AD_Code = int.Parse(AD_Code);
        CRMTree.BLL.BL_Appt_Service myAppointmens = new CRMTree.BLL.BL_Appt_Service();
        CRMTree.Model.Appointmens.MD_ServiceTypesList myServiceTypesList = myAppointmens.getServiceTypesList(ServiceType);
        if (myServiceTypesList != null && !Intern)
        {
            for (int i = 0; i < myServiceTypesList.Service_Types_List.Count; i++)
            {
                myServiceTypesList.Service_Types_List[i].ST_Desc_EN = myServiceTypesList.Service_Types_List[i].ST_Desc_CN;
            }
        }
        return myServiceTypesList;
    }
    /// <summary>
    /// 获取保养得类型
    /// </summary>
    /// <param name="AD_Code"></param>
    /// <returns></returns>
    [WebMethod]
    public static Object getMaintenancePack(string AD_Code)
    {
        CRMTree.Model.CT_Maintenance_Pack MaintenancePack = new CRMTree.Model.CT_Maintenance_Pack();
        MaintenancePack.MP_AD_Code = int.Parse(AD_Code);
        CRMTree.BLL.BL_Appt_Service myAppointmens = new CRMTree.BLL.BL_Appt_Service();
        CRMTree.Model.Appointmens.MD_MaintenancePackList myMaintenancePackList = myAppointmens.getMaintenancePackList(MaintenancePack);
        if (myMaintenancePackList != null && !Intern)
        {
            for (int i = 0; i < myMaintenancePackList.Maintenance_Pack_List.Count; i++)
            {
                myMaintenancePackList.Maintenance_Pack_List[i].MP_Desc_EN = myMaintenancePackList.Maintenance_Pack_List[i].MP_Desc_CN;
            }
        }
        return myMaintenancePackList;
    }
}