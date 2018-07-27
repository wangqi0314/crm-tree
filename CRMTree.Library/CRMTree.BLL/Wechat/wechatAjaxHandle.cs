using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Model.MyCar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
namespace CRMTree.BLL.Wechat
{
    /// <summary>
    /// 微信的异步处理
    /// </summary>
    public class wechatAjaxHandle2
    {
        #region 实例化对象

        readonly BL_MyCar _b_car = new BL_MyCar();
        readonly BL_Appt_Service _b_app = new BL_Appt_Service();
        readonly BL_ServerHistory _b_serverHis = new BL_ServerHistory();
        readonly BL_Campaign _b_camp = new BL_Campaign();
        readonly B_W_Fans _b_fans = new B_W_Fans();
        readonly BL_UserEntity _user_Entity = new BL_UserEntity();
        readonly BL_MyCar _b_myCar = new BL_MyCar();
        #endregion

        #region 内部参数

        /// <summary>
        /// 表示请求的总数据
        /// </summary>
        string _JsonData = string.Empty;
        /// <summary>
        /// 表示请求http内容
        /// </summary>
        HttpContext _http = null;
        /// <summary>
        /// 微信客户端请求者的信息
        /// </summary>
        CT_Wechat_Member _User_Member = null;
        /// <summary>
        /// 用来返回的对象数据
        /// </summary>
        object _object = null;

        #endregion

        #region 构造
        /// <summary>
        /// 初始构造
        /// </summary>
        /// <param name="http"></param>
        public wechatAjaxHandle2(HttpContext http)
        {
            this._JsonData = http.Request.Form["_d"];
            this._http = http;
            initHandle();
        }
        #endregion

        #region 初始化

        /// <summary>
        /// 初始化初步请求
        /// </summary>
        private void initHandle()
        {
            var data = JsonConvert.DeserializeObject<dynamic>(_JsonData);
            if (data.Key == null)
            {
                _object = (int)_errCode.isNull;
            }
            else
            {
                _requestHandle(data);
            }
        }

        /// <summary>
        /// 相应处理
        /// </summary>
        /// <param name="data"></param>
        private void _requestHandle(dynamic data)
        {
            switch ((string)data.Key)
            {
                case "code":
                    Key_code(data);
                    break;
                case "Key_01":
                    Key_01(data);
                    break;
                case "App_01":
                    App_01(data);
                    break;
                case "Server_01":
                    Server_01(data);
                    break;
                case "Car_01":
                    Car_01(data);
                    break;
                default:
                    _object = (int)_errCode.isNull;
                    break;
            }
        }

        #endregion

        #region 处理完数据的处理

        /// <summary>
        /// 返回相应的处理
        /// </summary>
        /// <returns></returns>
        public object toResponse()
        {
            if (_object == null)
            {
                return (int)_errCode.isNull;
            }
            else
            {
                return _object;
            }
        }

        #endregion

        #region 基础信息

        /// <summary>
        /// 获取微信端用户的基础信息 code为授权字符串
        /// </summary>
        /// <param name="json"></param>
        private void Key_code(dynamic data)
        {
            string _code = string.Empty;
            if (data.codes != null)
            {
                _code = (string)data.codes;
            }
            string OpenId = _getOpenId(_code);
            if (string.IsNullOrEmpty(OpenId))
            {
                _object = _errCode.isNull;
            }
            else
            {
                _object = _b_fans.GetFans_Json(OpenId);
            }
        }
        #endregion

        #region 个人信息处理
        private void Key_01(dynamic data)
        {
            switch ((string)data._method)
            {
                case "save_Info":
                    Key_01_Save(data);
                    break;
                case "GetAll_User_Info":
                    Key_01_GetAll_User_Info(data);
                    break;
                case "Modify_Car":
                    Key_01_Modify_Car(data);
                    break;
                case "Get_Make":
                    Key_01_Get_Make(data);
                    break;
                case "Get_Model":
                    Key_01_Get_Model(data);
                    break;
                case "Get_Style":
                    Key_01_Get_Style(data);
                    break;
            }
        }
        private void Key_01_Save(dynamic data)
        {
            CT_All_Users _user = new CT_All_Users();
            _user.OpenId = _http.Session["OpenId"].ToString();
            _user.AU_Name = data.UserName;
            _user.AU_B_date = Convert.ToDateTime(data.birthday);
            _user.AU_Gender = Convert.ToBoolean(data.sex);
            _user.Phone = data.Moblid;

            int i = _user_Entity.Modify_User_Info(_user);
            _object = i;
        }
        private void Key_01_GetAll_User_Info(dynamic data)
        {
            string _openId = _http.Session["OpenId"].ToString();
            if (string.IsNullOrEmpty(_openId))
            {
                _object = _errCode.isNull;
            }
            else
            {
                _object = _user_Entity.GetAll_User_Info_Json(_openId);
            }
        }
        private void Key_01_Modify_Car(dynamic data)
        {
            string _openId = _http.Session["OpenId"].ToString();
            CT_Car_Inventory _car_In = new CT_Car_Inventory();
            if (data.CI_AU_Code == null)
            {
                _car_In.CI_AU_Code = getWechat_Member(_openId).MB_AU_Code;
            }
            else
            {
                _car_In.CI_AU_Code = (long)data.CI_AU_Code;
            }
            _car_In.CI_CS_Code = (int)data.CI_CS_Code;
            _car_In.CI_VIN = null;
            _car_In.CI_Mileage = null;
            _car_In.CI_Licence = data.CI_Licence;

            _car_In.CI_Licence_dt = null;
            if (!string.IsNullOrEmpty((string)data.CI_Licence_dt))
            {
                _car_In.CI_Licence_dt = Convert.ToDateTime(data.CI_Licence_dt);
            }
            _car_In.CI_YR_Code = null;
            _car_In.CI_Color_I = null;
            _car_In.CI_Color_E = null;
            _car_In.CI_Picture_FN = null;
            _car_In.CI_Status = "1";
            _car_In.CI_Activate_Tag = 1;
            _car_In.CI_Frame = null;
            if (!string.IsNullOrEmpty((string)data.CI_Frame))
            {
                _car_In.CI_Frame = data.CI_Frame;
            }
            _car_In.CI_Driving = null;
            if (!string.IsNullOrEmpty((string)data.CI_Driving))
            {
                _car_In.CI_Driving = data.CI_Driving;
            }
            _car_In.CI_Driving_Type = null;
            if (!string.IsNullOrEmpty((string)data.CI_Driving_Type))
            {
                _car_In.CI_Driving_Type = data.CI_Driving_Type;
            }
            _car_In.CI_Driving_dt = null;
            if (!string.IsNullOrEmpty((string)data.CI_Driving_dt))
            {
                _car_In.CI_Driving_dt = Convert.ToDateTime(data.CI_Driving_dt);
            }
            _car_In.CI_Warr_St_dt = null;
            if (!string.IsNullOrEmpty((string)data.CI_Warr_St_dt))
            {
                _car_In.CI_Warr_St_dt = Convert.ToDateTime(data.CI_Warr_St_dt);
            }

            _car_In.CI_remarks = null;

            _object = _b_myCar.modify_Car(_car_In);
        }
        private void Key_01_Get_Make(dynamic data)
        {
            _object = _b_myCar.getMyCarMakeList();
        }
        private void Key_01_Get_Model(dynamic data)
        {
            int MK_code = (int)data.MK_code;
            _object = _b_myCar.getMyCarModeList(MK_code);
        }
        private void Key_01_Get_Style(dynamic data)
        {
            int CM_Code = (int)data.CM_Code;
            _object = _b_myCar.getMyCarStyleList(CM_Code);
        }
        #endregion

        #region 预约处理
        private void App_01(dynamic data)
        {
            switch ((string)data._method)
            {
                case "Get_App":
                    Key_01_Get_App(data);
                    break;
                case "Get_AppList":
                    Key_01_AppList(data);
                    break;
                case "Get_Tran_List":
                    Key_01_Get_Tran_List(data);
                    break;
                case "Get_DefaultDealer":
                    Key_01_Get_DafaultDealer(data);
                    break;
                case "Get_Dealer_List":
                    Key_01_Get_Dealer_List(data);
                    break;
                case "GetDefaultAdviser":
                    Key_01_Get_DefaultAdviser(data);
                    break;
                case "Get_Adviser_List":
                    Key_01_Get_Adviser_List(data);
                    break;
                case "checkAdviserTime":
                    Key_01_checkAdviserTime(data);
                    break;
                case "Get_ServCategory_List":
                    Key_01_Get_ServCategory(data);
                    break;
                case "Get_ServTypes_List":
                    Key_01_Get_ServTypes(data);
                    break;
                case "Get_Maintenance_List":
                    Key_01_Get_Maintenance_Json(data);
                    break;
                case "Modify_Appt":
                    Key_01_Modify_Appt(data);
                    break;
            }
        }
        private void Key_01_Get_App(dynamic data)
        {
            int _AP_Code = (int)data.AP_Code;
            _object = _b_app.GetApp_Json(_AP_Code);
        }

        private void Key_01_AppList(dynamic data)
        {
            string code = string.Empty;
            if (data.codes != null)
            {
                code = data.codes;
            }
            string OpenId = _getOpenId(code);
            _User_Member = getWechat_Member(OpenId);
            if (_User_Member == null)
            {
                _object = -1;
            }
            else
            {
                _object = _b_app.GetAppList_Json(_User_Member.MB_AU_Code);
            }
        }

        private void Key_01_Get_DafaultDealer(dynamic data)
        {
            long _au_code = (long)data.AU_Code;
            int _ci_code = (int)data.CI_Code;
            string _default_dealer = _b_app.GetDefaultDealer(_au_code, _ci_code);
            _object = _default_dealer;
        }


        private void Key_01_Get_Dealer_List(dynamic data)
        {
            int page = 1;
            if (data.page != null)
            {
                page = data.page;
            }
            _object = _b_app.GetDealerList_Json(0, page);
        }

        private void Key_01_Get_DefaultAdviser(dynamic data)
        {
            long _au_code = (long)data.AU_Code;
            int _ci_code = (int)data.CI_Code;
            int _ad_code = (int)data.AD_Code;
            string _default_adviser = _b_app.GetDefaultAdviser(_au_code, _ci_code, _ad_code);
            _object = _default_adviser;
        }

        private void Key_01_Get_Adviser_List(dynamic data)
        {
            int page = 1;
            if (data.page != null)
            {
                page = data.page;
            }
            int _ad_code = (int)data.AD_Code;
            _object = _b_app.GetAdviserList_Json(_ad_code, page);
        }

        private void Key_01_checkAdviserTime(dynamic data)
        {
            int _ad_code = (int)data.AD_Code;
            int _de_code = (int)data.DE_Code;
            string _ap_date = (string)data.AP_Date;
            float _ap_time = (float)data.AP_Time;
            _object = _b_app.checkAdviserTime(_ad_code, _de_code, _ap_date, _ap_time, false);
        }

        private void Key_01_Get_Tran_List(dynamic data)
        {
            _object = _b_app.GetTransportation_Json();
        }
        private void Key_01_Get_ServCategory(dynamic data)
        {
            int _ad_code = (int)data.AD_Code;
            _object = _b_app.GetServCategory(_ad_code);
        }
        private void Key_01_Get_ServTypes(dynamic data)
        {
            int _ad_code = (int)data.AD_Code;
            int _sc_code = (int)data.SC_Code;
            _object = _b_app.GetServTypes_Json(_ad_code, _sc_code);
        }
        private void Key_01_Get_Maintenance_Json(dynamic data)
        {
            int _ad_code = (int)data.AD_Code;
            _object = _b_app.GetMaintenance_Json(_ad_code);
        }
        private void Key_01_Modify_Appt(dynamic data)
        {
            CT_Appt_Service _app_m = new CT_Appt_Service();
            _app_m.AP_Code = -1;
            if (data.AP_Code != null)
            {
                _app_m.AP_Code = (int)_app_m.AP_Code;
            }
            _app_m.AP_AU_Code = (long)data.AU_Code;
            _app_m.AP_CI_Code = (int)data.CI_Code;
            _app_m.AP_AD_Code = (int)data.AD_Code;
            _app_m.AP_Time = Convert.ToDateTime(data.AP_Time);
            _app_m.AP_SA_Selected = (int)data.SA_Selected;
            _app_m.AP_SC_Code = (int)data.SC_Code;
            _app_m.AP_ST_Code = (int)data.ST_Code;
            _app_m.AP_MP_Code = -1;
            if (data.MP_Code != null)
            {
                _app_m.AP_MP_Code = (int)data.MP_Code;
            }
            _app_m.AP_PAM_Code = 3;
            _app_m.AP_Notification = true;
            _app_m.AP_Transportation = (byte)data.AP_Tran;
            _app_m.AP_Notes = null;
            if (data.AP_Note != null)
            {
                _app_m.AP_Notes = data.AP_Note;
            }
            _object = _b_app.Modify_Appt(_app_m);
            SendWechatInfo_01(data);
        }
        private void SendWechatInfo_01(dynamic data)
        {
            string OpenId = _http.Session["OpenId"].ToString();
            string send_info = "尊敬的车主，您好！\n"
                + (string.IsNullOrEmpty((string)data.AP_Code) ? "你的预约信息成功处理\n如下是你的预约信息：\n" : "你的预约信息成功修改\n如下是你更改后的预约信息：\n")
                + "我的汽车：" + data.CAR_CN + "\n"
                + "经 销 商：" + data.AD_Name_CN + "\n"
                + "选择顾问：" + data.AU_Name + "\n"
                + "预约类型：" + data.SER_CN + "\n"
                + "交通方式：" + data.AP_Tran_Name + "\n"
                + "预约时间：" + Convert.ToDateTime(data.AP_Time) + "\n";
            wechatHandle.SendCustom_text(OpenId, send_info);
            CT_Campaigns _com;
            try
            {
                _com = _b_camp.GetCampaign(Convert.ToInt32(data.AD_Code), true);
            }
            catch
            {
                _com = _b_camp.GetCampaign(3, true);
            }
            if (_com != null)
            {
                List<CRMTreeDatabase.EX_Param> exParams = new List<CRMTreeDatabase.EX_Param>();
                exParams.Add(new CRMTreeDatabase.EX_Param()
                {
                    EX_DataType = "int",
                    EX_Name = "AU",
                    EX_Value = data.AU_Code
                });
                exParams.Add(new CRMTreeDatabase.EX_Param()
                {
                    EX_DataType = "int",
                    EX_Name = "CI",
                    EX_Value = data.CI_Code
                });
                exParams.Add(new CRMTreeDatabase.EX_Param()
                {
                    EX_DataType = "int",
                    EX_Name = "AP",
                    EX_Value = data.AP_Code
                });
                exParams.Add(new CRMTreeDatabase.EX_Param()
                {
                    EX_DataType = "int",
                    EX_Name = "AD",
                    EX_Value = data.AD_Code
                });
                exParams.Add(new CRMTreeDatabase.EX_Param()
                {
                    EX_DataType = "int",
                    EX_Name = "DE",
                    EX_Value = data.SA_Selected
                });
                exParams.Add(new CRMTreeDatabase.EX_Param()
                {
                    EX_DataType = "int",
                    EX_Name = "ST",
                    EX_Value = data.ST_Code
                });
                exParams.Add(new CRMTreeDatabase.EX_Param()
                {
                    EX_DataType = "int",
                    EX_Name = "SC",
                    EX_Value = data.SC_Code
                });
                exParams.Add(new CRMTreeDatabase.EX_Param()
                {
                    EX_DataType = "int",
                    EX_Name = "MP",
                    EX_Value = data.MP_Code
                });
                wechatHandle.SendCustom_text(OpenId, BL_Reports.GetFileContent(_com.CG_Filename,
                    exParams, 2));
                _b_camp.Modify_Comm_History(1, OpenId, _com.CG_Code);
            }
        }
        #endregion

        #region 车辆处理信息
        private void Car_01(dynamic data)
        {
            switch ((string)data._method)
            {
                case "Get_DefaultCar":
                    Car_Get_DefaultCar(data);
                    break;
                case "Get_CarList":
                    Car_01_CarList(data);
                    break;
                case "Get_CarInfo":
                    Car_01_CarInfo(data);
                    break;
                case "Get_BindCarList":
                    Get_BindCarList(data);
                    break;
                case "BindCar_Adviser":
                    CarBind_Adviser(data);
                    break;
            }
        }

        /// <summary>
        /// 获取默认的汽车信息
        /// </summary>
        /// <param name="data"></param>
        private void Car_Get_DefaultCar(dynamic data)
        {
            long au_code = (long)data.AU_Code;
            string _default_car = _b_car.GetDafaultCarInfo(au_code);
            _object = _default_car;
        }
        private void Car_01_CarList(dynamic data)
        {
            if (data.AU_Code == null)
            {
                string _openId = _http.Session["OpenId"].ToString();
                data.AU_Code = getWechat_Member(_openId).MB_AU_Code;
            }
            long au_code = (long)data.AU_Code;
            _object = _b_car.GetCarInfo_List(au_code);
        }
        private void Car_01_CarInfo(dynamic data)
        {
            int _ci_code = (int)data.CI_Code;
            _object = _b_car.GetCarInfo_Json(_ci_code);
        }
        private void Get_BindCarList(dynamic data)
        {
            string _openId = _http.Session["OpenId"].ToString();
            _object = _b_myCar.GetBindCarList_Json(getWechat_Member(_openId).MB_AU_Code);
        }
        private void CarBind_Adviser(dynamic data)
        {
            CT_Car_Inventory _car_o = new CT_Car_Inventory()
            {
                CI_AU_Code = (long)data.AU_Code,
                CI_Code = (int)data.CI_Code,
                DE_Code = (int)data.DE_Code,
                IS_Bind = (bool)data.IS_Bind
            };
            _object = _b_myCar.CarBind_Adviser(_car_o);
        }
        #endregion

        #region 历史服务信息处理
        private void Server_01(dynamic data)
        {
            switch ((string)data._method)
            {
                case "Get_Service_List":
                    Server_01_Get_Service_List(data);
                    break;
                case "Get_Service_Info":
                    Server_01_Get_Service_Info(data);
                    break;
            }
        }
        private void Server_01_Get_Service_List(dynamic data)
        {
            string code = string.Empty;
            if (data.codes != null)
            {
                code = data.codes;
            }
            string OpenId = _getOpenId(code);
            _User_Member = getWechat_Member(OpenId);
            if (_User_Member == null)
            {
                _object = -1;
            }
            else
            {
                _object = _b_serverHis.GetHistory_Service(_User_Member.MB_AU_Code, null);
            }
        }
        private void Server_01_Get_Service_Info(dynamic data)
        {
            int _hs_code = (int)data.HS_Code;
            _object = _b_serverHis.GetService_Info(_hs_code);
        }
        #endregion

        #region 相关逻辑处理
        /// <summary>
        /// 获取注册过的用户信息
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        private CT_Wechat_Member getWechat_Member(string OpenId)
        {
            return new BL_UserEntity().GetWechat_Member(OpenId);
        }
        /// <summary>
        /// 根据code，获取OpenID
        /// </summary>
        /// <param name="codes"></param>
        private string _getOpenId(string code)
        {
            string OpenId = string.Empty, Key = "OpenId";
            if (_http.Session != null && _http.Session[Key] != null)
            {
                OpenId = _http.Session[Key].ToString();
                return OpenId;
            }
            if (string.IsNullOrEmpty(code))
            {
                OpenId = "ogw2MjhK4qPJbDSmtCeXWLezqAOM";
                _http.Session[Key] = OpenId;
                B_W_Exception.AddExcep("AjaxJson >", "_getOpenId", "获取固定值:" + OpenId);
            }
            else
            {
                OpenId = wechatHandle.GetOpenId(_http, code);
                B_W_Exception.AddExcep("AjaxJson >", "_getOpenId", "获取动态值:" + OpenId);
            }
            return OpenId;
        }
        #endregion
    }
}