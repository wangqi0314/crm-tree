using CRMTree.DAL;
using CRMTree.Model;
using CRMTree.Model.Appointmens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    /// <summary>
    /// 预约服务逻辑处理
    /// </summary>
    public class BL_Appt_Service
    {
        #region 内部

        private readonly DL_Appt_Service o = new DL_Appt_Service();

        #endregion

        #region 预约信息

        /// <summary>
        /// 获取用户的预约列表
        /// </summary>
        /// <param name="AU_code"></param>
        /// <returns></returns>
        public string GetAppList_Json(long AU_code)
        {
            return o.GetAppList_Json(AU_code);
        }

        /// <summary>
        /// 获取用户的预约信息
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        public string GetApp_Json(int AP_Code)
        {
            return o.GetApp_Json(AP_Code);
        }

        /// <summary>
        /// 预约更改
        /// </summary>
        /// <param name="App"></param>
        /// <returns></returns>
        public int Modify_Appt(CT_Appt_Service App)
        {
            return o.Modify_Appt(App);
        }

        /// <summary>
        /// 新增预约信息
        /// </summary>
        /// <param name="App"></param>
        /// <returns></returns>
        public CT_Appt_Service InsertApp(CT_Appt_Service App)
        { 
            return o.InsertApp(App);
        }

        /// <summary>
        /// 获取用户的预约信息对象
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public MD_Appointmens getAppointment(long UserCode)
        {
            return o.getAppointmensI(UserCode);
        }

        #endregion

        #region 经销商 顾问 处理

        public MD_DealerList getDealerList(long AU_Code)
        {
            return o.getDealerList(AU_Code);
        }

        public string GetDefaultDealer(long au_code, int ci_code)
        {
            return o.GetDefaultDealer(au_code, ci_code);
        }

        public string GetDealerList_Json(long AU_Code)
        {
            return GetDealerList_Json(AU_Code, 1);
        }
        public string GetDealerList_Json(long AU_Code, int page)
        {
            return o.GetDealerList_Json(AU_Code, page);
        }
        public MD_AdviserList getAdviserList(int AD_Code)
        {
            return o.getAdviserList(AD_Code);
        }

        public string GetDefaultAdviser(long AU_Code, int CI_Code, int AD_Code)
        {
            return o.GetDefaultAdviser(AU_Code, CI_Code, AD_Code);
        }

        public string GetAdviserList_Json(int AD_Code, int page)
        {
            return o.GetAdviserList_Json(AD_Code, page);
        }
        public CT_Dealer_Empl GetAdviser(string DE_Code)
        {
            return o.GetAdviser(DE_Code);
        }

        public string checkAdviserTime(int ad_code, int de_code, string ap_date, float ap_time, bool Interna)
        {
            return o.checkAdviserTime(ad_code, de_code, ap_date, ap_time, Interna);
        }

        public string GetAdviserName(string DE_Code)
        {
            CT_Dealer_Empl _de = GetAdviser(DE_Code);
            if (_de == null) { return ""; }
            return _de.AU_Name;
        }
        public string GetWordText(int pid, int id)
        {
            return o.GetWordText(pid, id);
        }
        public string Get_PeriodicalsText(string Period)
        {
            string _de = GetWordText(4213, int.Parse(Period));
            if (_de == null) { return ""; }
            return _de;
        }        /// <summary>

        #endregion

        #region 预约的相关处理

        /// <summary>
        /// 获取用户的预约车辆
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public MD_AppointmensList getMyCarStyle(int UserCode)
        {
            return o.getMyCarStyle(UserCode);
        }

        /// <summary>
        /// 获取预约服务类型列表
        /// </summary>
        /// <returns></returns>
        public MD_ServCategoryList getServList()
        {
            return o.getServList();
        }

        /// <summary>
        /// 获取服务类别
        /// </summary>
        /// <param name="ad_code"></param>
        /// <returns></returns>
        public string GetServCategory(int ad_code)
        {
            return o.GetServCategory(ad_code);
        }

        /// <summary>
        /// 获取服务类型列表
        /// </summary>
        /// <param name="ServiceType"></param>
        /// <returns></returns>
        public MD_ServiceTypesList getServiceTypesList(CT_Service_Types ServiceType)
        {
            return o.getServiceTypeList(ServiceType);
        }

        /// <summary>
        /// 获取服务类型
        /// </summary>
        /// <param name="AD_Code"></param>
        /// <param name="SC_Code"></param>
        /// <returns></returns>
        public string GetServTypes_Json(int AD_Code, int SC_Code)
        {
            return o.GetServTypes_Json(AD_Code, SC_Code);
        }

        /// <summary>
        /// 获取服务套餐
        /// </summary>
        /// <param name="MaintenancePack"></param>
        /// <returns></returns>
        public MD_MaintenancePackList getMaintenancePackList(CT_Maintenance_Pack MaintenancePack)
        {
            return o.getMaintenancePackList(MaintenancePack);
        }

        /// <summary>
        /// 获取服务套餐
        /// </summary>
        /// <param name="AD_Code"></param>
        /// <returns></returns>
        public string GetMaintenance_Json(int AD_Code)
        {
            return o.GetMaintenance_Json(AD_Code);
        }

        /// <summary>
        /// 获取预约的交通方式
        /// </summary>
        /// <returns></returns>
        public string GetTransportation_Json()
        {
            return o.GetTransportation_Json();
        }

        /// <summary>
        /// 获取预约的交通方式
        /// </summary>
        /// <returns></returns>
        public TranList GetTransportation()
        {
            return o.GetTransportation();
        }

        /// <summary>
        /// 删除一条预约信息
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        public int DeleteAppointmens(int AP_Code)
        {
            return o.DeleteAppointmens(AP_Code);
        }

        #endregion
    }
}
