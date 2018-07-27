using CRMTree.DAL.Wechat;
using CRMTree.Model;
using CRMTree.Model.Wechat;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL.Wechat
{
    /// <summary>
    /// 系统异常记录和测试日志
    /// </summary>
    public class B_W_Exception
    {
        /// <summary>
        /// 对于微信接口操作异常的记录
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="ActionCode"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public static int AddExcep(string Action, string ActionCode, string Msg)
        {
            return new D_W_Exception().AddExcep(Action, ActionCode, Msg);
        }
        /// <summary>
        /// 对于微信相关的所以接口操作的XML日志记录
        /// </summary>
        /// <param name="XML"></param>
        /// <param name="STR"></param>
        /// <returns></returns>
        public static int AddLog(string XML, string STR)
        {
            return new D_W_Exception().AddLog(XML, STR);
        }
    }
    /// <summary>
    /// 对于粉丝的处理
    /// </summary>
    public class B_W_Fans
    {
        private D_W_Fans _d_fan = new D_W_Fans();
        #region Add
        public int AddFans(CT_Wechat_Fan fan)
        {
            CT_Wechat_Fan o = _d_fan.GetFans(fan.WF_OpenId);
            if (o != null)
            {
                int i = _d_fan.UpdateFans(fan.WF_OpenId, 1);
                return i;
            }
            return _d_fan.AddFans(fan);
        }
        #endregion

        #region Get
        public CT_Wechat_Fan GetFans(string OpenId)
        {
            return _d_fan.GetFans(OpenId);
        }
        public string GetFans_Json(string OpenId)
        {
            return _d_fan.GetFans_Json(OpenId);
        }
        /// <summary>
        /// 获取的所有关注的粉丝
        /// </summary>
        /// <returns></returns>
        public MD_FansList GetAllFan()
        {
            return _d_fan.GetAllFan();
        }
        #endregion
        #region Update
        public int UpdateFans(string OpenId, int Status)
        {
            return _d_fan.UpdateFans(OpenId, Status);
        }
        #endregion
    }
    /// <summary>
    /// 注册后的粉丝
    /// </summary>
    public class B_W_CT_Member
    {
        #region Get
        /// <summary>
        /// 获取注册后的粉丝信息 type=1 code=OpenID； type=2 code=De_code
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static CT_Wechat_Member GetMember(string Code, int type)
        {
            return new D_W_CT_Member().GetMember(Code, type);
        }
        #endregion
    }
    /// <summary>
    /// 记录粉丝发送的消息
    /// </summary>
    public class B_W_TrackLog
    {
        D_W_TrackLog _track = new D_W_TrackLog();
        public int AddTrackLog(string fromOpenId, string toUserName, string Content, string date)
        {
            return _track.AddTrackLog(fromOpenId, toUserName, Content, date);
        }
    }
    /// <summary>
    /// 处理是否在线
    /// </summary>
    public class B_W_Online
    {
        public static void AddOnline(string OpenId, string ON_date, string Key)
        {
            D_W_Online _d_on_s = new D_W_Online();
            CT_Wechat_Online o = _d_on_s.GetOnline(OpenId);
            if (o == null)
            {
                _d_on_s.AddOnline(OpenId, ON_date, Key);
            }
            else
            {
                _d_on_s.UpdateOnline(OpenId, ON_date, Key);
            }
        }
        public static CT_Wechat_Online GetOnline(string OpenId)
        {
            return new D_W_Online().GetOnline(OpenId);
        }
    }
    /// <summary>
    /// 对客服聊天建立连接
    /// </summary>
    public class B_W_CustomerServiceConnection
    {
        D_W_CustomerServiceConnection _d_Conn = new D_W_CustomerServiceConnection();
        #region Add
        public static void AddServiceConnection(CT_Wechat_CustomerServiceConnection _o)
        {
            D_W_CustomerServiceConnection _d_Conn_s = new D_W_CustomerServiceConnection();
            CT_Wechat_CustomerServiceConnection o = _d_Conn_s.GetServiceConnection(_o.CSC_AD_OpenId, _o.CSC_CS_OpenId);
            if (o == null)
            {
                _d_Conn_s.AddServiceConnection(_o);

            }
            else { _d_Conn_s.UpdateServiceConnection(_o); }
        }
        #endregion
        #region Get
        public static CT_Wechat_CustomerServiceConnection GetServiceConnection(string AD_OpenId, string CS_OpenId)
        {
            return new D_W_CustomerServiceConnection().GetServiceConnection(AD_OpenId, CS_OpenId);
        }
        /// <summary>
        /// 获取正则建立连接的客服信息，1代表客服，2代表客户
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static CT_Wechat_CustomerServiceConnection GetServiceConnection(string CS_OpenId, int _type)
        {
            return new D_W_CustomerServiceConnection().GetServiceConnection(CS_OpenId, _type);
        }
        public IList<CT_Wechat_CustomerServiceConnection> GetAdviserConnection(string OpenId)
        {
            return new D_W_CustomerServiceConnection().GetAdviserConnection(OpenId);
        }
        #endregion
        #region Update
        public static int UpdateOnline(CT_Wechat_CustomerServiceConnection o)
        {
            int i = new D_W_CustomerServiceConnection().UpdateServiceConnection(o);
            return i;
        }
        #endregion
    }
    /// <summary>
    /// 处理客服消息
    /// </summary>
    public class B_W_CustomSservice
    {
        D_W_CustomSservice _d_custom = new D_W_CustomSservice();
        #region Add
        public int AddCustomSservice(CT_Wechat_CustomSservice o)
        {
            return _d_custom.AddCustomSservice(o);
        }
        public int AddCustomSservice(string AD_OpenId, string CS_OpenId, string Notes, int DH_legacy)
        {
            return _d_custom.AddCustomSservice(AD_OpenId, CS_OpenId, Notes, DH_legacy);
        }
        #endregion
        #region Get
        /// <summary>
        /// 获取客户最后一次发送的消息
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public CT_Wechat_CustomSservice GetCustomerLastInfo(string OpenId)
        {
            return _d_custom.GetCustomerLastInfo(OpenId); ;
        }
        #endregion
    }
}
