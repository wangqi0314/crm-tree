using CRMTree.Model;
using CRMTree.Model.Campaigns;
using CRMTree.Model.Common;
using CRMTree.Model.MyCar;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
namespace CRMTree.BLL.Wechat
{
    #region 微信端客服系统处理
    /// <summary>
    /// 微信通信控制器
    /// </summary>
    public class WechatCommunicator
    {
        //各类通信类型的Key
        private CommunicatorKey _CommunicatorKey;
        //接受到的信息
        private xml _x;
        private string _AD_OpenId = string.Empty;
        private string _CS_OpenId = string.Empty;

        /// <summary>
        /// 初始化通讯器
        /// </summary>
        /// <param name="x"></param>
        /// <param name="Key"></param>
        public WechatCommunicator(xml x, CommunicatorKey Key)
        {
            this._CommunicatorKey = Key;
            this._x = x;
            this.Initialise();
        }
        private void Initialise()
        {
            IsCommunicatorType();
        }
        /// <summary>
        /// 通讯类型，顾问和客户
        /// </summary>
        private void IsCommunicatorType()
        {
            CT_Wechat_Member _M = B_W_CT_Member.GetMember(_x.FromUserName, 1);
            if (_M == null)
            {
                new CustomerControl(_x);
            }
            else
            {
                new AdviserControl(_x);
            }

        }
        /// <summary>
        /// 通讯连接控制
        /// </summary>
        public class ConnectControl
        {
            /// <summary>
            /// 判断在指定时间内是否选择了顾问，进行聊天
            /// </summary>
            /// <param name="second"></param>
            /// <returns></returns>
            public static bool isSelectExpire(string OpenId, CommunicatorKey Key, int second)
            {
                CT_Wechat_Online on = B_W_Online.GetOnline(OpenId);
                if (on == null)
                {
                    return false;
                }
                if (on.WO_Key != Key.ToString())
                {
                    return false;
                }
                int datetime = wechatHandle.ConvertDateTimeInt(DateTime.Now);
                if (datetime - Convert.ToInt32(on.WO_DateTime) > second)
                {
                    return false;
                }
                return true;
            }
            public static bool IsCustomConnectExpire(string CS_OpenId, int minute, out string OpenId)
            {
                CT_Wechat_CustomerServiceConnection _C = B_W_CustomerServiceConnection.GetServiceConnection(CS_OpenId, 2);
                if (_C == null)
                {
                    OpenId = null;
                    return false;
                }
                if (_C.CSC_Connection_dt.AddMinutes(minute) < DateTime.Now)
                {
                    OpenId = null;
                    return false;
                }
                OpenId = _C.CSC_AD_OpenId;
                return true;
            }
            public static bool IsServiceConnectExpire(string CS_OpenId, int minute, out string OpenId)
            {
                CT_Wechat_CustomerServiceConnection _C = B_W_CustomerServiceConnection.GetServiceConnection(CS_OpenId, 1);
                if (_C == null)
                {
                    OpenId = null;
                    return false;
                }
                if (_C.CSC_Connection_dt.AddMinutes(minute) < DateTime.Now)
                {
                    OpenId = null;
                    return false;
                }
                OpenId = _C.CSC_CS_OpenId;
                return true;
            }

        }
        /// <summary>
        ///  客户的通讯控制
        /// </summary>
        public class CustomerControl
        {
            /// <summary>
            /// 待处理的实体数据
            /// </summary>
            private xml _x;
            private string _AD_OpenId = string.Empty;
            /// <summary>
            /// 实例化，同时获取处理的数据
            /// </summary>
            /// <param name="x"></param>
            public CustomerControl(xml x)
            {
                this._x = x;
                CustomerCommunicate();
            }
            /// <summary>
            /// 判断是否建立客服连接
            /// </summary>
            /// <returns></returns>
            private bool IsCustomConnectExpire()
            {
                if (!ConnectControl.IsCustomConnectExpire(_x.FromUserName, 10, out _AD_OpenId))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            /// <summary>
            /// 客户通讯
            /// </summary>
            private void CustomerCommunicate()
            {
                if (IsCustomConnectExpire())
                {
                    B_W_CustomerServiceConnection.AddServiceConnection(new CT_Wechat_CustomerServiceConnection()
                    {
                        CSC_AD_OpenId = _AD_OpenId,
                        CSC_CS_OpenId = _x.FromUserName,
                        CSC_Connection_dt = DateTime.Now,
                        CSC_Connection_Status = 1
                    });
                    B_W_Fans _b_fan = new B_W_Fans();
                    CT_Wechat_Fan _fan = _b_fan.GetFans(_x.FromUserName);
                    wechatHandle.SendCustom_text(_AD_OpenId, "@" + _fan.WF_NickName + "：" + _x.Content);
                    B_W_CustomSservice _b_custom = new B_W_CustomSservice();
                    _b_custom.AddCustomSservice(new CT_Wechat_CustomSservice() { WCS_FromOpenId = _x.FromUserName, WCS_ToOpenId = _AD_OpenId, WCS_Content = _x.Content, WM_CreateTime = wechatHandle.GetLocalTime(Convert.ToInt64(_x.CreateTime)) });
                    _b_custom.AddCustomSservice(_AD_OpenId, _x.FromUserName, _x.Content, 0);
                }
                else
                {
                    try
                    {
                        Convert.ToInt32(_x.Content);
                        CT_Wechat_Member _M_s = B_W_CT_Member.GetMember(_x.Content, 2);
                        if (_M_s == null)
                        {
                            wechatHandle.SendCustom_text(_x.FromUserName, "请你先选择推荐的顾问，再进行通话。");
                            wechatHandle.SendCustom_text(_x.FromUserName, B_W_TextMessage.GetMessage());
                        }
                        else
                        {
                            B_W_CustomerServiceConnection.AddServiceConnection(new CT_Wechat_CustomerServiceConnection()
                            {
                                CSC_AD_OpenId = _M_s.MB_OpenID,
                                CSC_CS_OpenId = _x.FromUserName,
                                CSC_Connection_dt = DateTime.Now,
                                CSC_Connection_Status = 1
                            });
                            wechatHandle.SendCustom_text(_x.FromUserName, _M_s.AU_Name + ":正在等待你的问题");
                        }
                    }
                    catch
                    {
                        wechatHandle.SendCustom_text(_x.FromUserName, "请你先选择顾问，再进行通话。否则，你的消息我们仅作留言处理，不能及时回复你。");
                        wechatHandle.SendCustom_text(_x.FromUserName, B_W_TextMessage.GetMessage());
                        AddTrack();
                    }
                }
            }
            /// <summary>
            /// 添加客服发送的多余数据，做留言处理
            /// </summary>
            public void AddTrack()
            {
                B_W_TrackLog _o = new B_W_TrackLog();
                _o.AddTrackLog(_x.FromUserName, _x.ToUserName, _x.Content, wechatHandle.GetLocalTime(_x.CreateTime));
            }
        }
        /// <summary>
        /// 顾问的通讯控制
        /// </summary>
        private class AdviserControl
        {
            /// <summary>
            /// 待处理的实体数据
            /// </summary>
            private xml _x;
            private string _CS_OpenId = string.Empty;
            /// <summary>
            /// 实例化，同时获取处理的数据
            /// </summary>
            /// <param name="x"></param>
            public AdviserControl(xml x)
            {
                this._x = x;
                AdviserCommunicate();
            }
            /// <summary>
            /// 判断服务连接是否过期
            /// </summary>
            /// <returns></returns>
            private bool IsServiceConnectExpire()
            {
                if (!ConnectControl.IsServiceConnectExpire(_x.FromUserName, 10, out _CS_OpenId))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            /// <summary>
            /// 顾问通讯
            /// </summary>
            private void AdviserCommunicate()
            {
                if (IsServiceConnectExpire())
                {
                    if (IsSelectSend())
                    {
                        _CS_OpenId = SelectCutomerOpenId();
                    }
                    B_W_CustomerServiceConnection.AddServiceConnection(new CT_Wechat_CustomerServiceConnection()
                    {
                        CSC_AD_OpenId = _x.FromUserName,
                        CSC_CS_OpenId = _CS_OpenId,
                        CSC_Connection_dt = DateTime.Now,
                        CSC_Connection_Status = 1
                    });
                    B_W_Fans _b_fan = new B_W_Fans();
                    CT_Wechat_Fan _fan = _b_fan.GetFans(_x.FromUserName);
                    wechatHandle.SendCustom_text(_CS_OpenId, "@" + _fan.WF_NickName + "：" + _x.Content);
                    B_W_CustomSservice _b_custom = new B_W_CustomSservice();
                    _b_custom.AddCustomSservice(new CT_Wechat_CustomSservice() { WCS_FromOpenId = _x.FromUserName, WCS_ToOpenId = _CS_OpenId, WCS_Content = _x.Content, WM_CreateTime = wechatHandle.GetLocalTime(Convert.ToInt64(_x.CreateTime)) });
                    _b_custom.AddCustomSservice(_x.FromUserName, _CS_OpenId, _x.Content, 1);
                }
                else
                {
                    wechatHandle.SendCustom_text(_x.FromUserName, "没有客户跟你进行客服连接");

                }
            }

            private bool IsSelectSend()
            {
                if (_x.Content.Substring(0, 1) == "@")
                {
                    return true;
                }
                else { return false; }
            }
            private string SelectCutomerOpenId()
            {
                IList<CT_Wechat_CustomerServiceConnection> _c_s = new B_W_CustomerServiceConnection().GetAdviserConnection(_x.FromUserName);
                if (_c_s == null) { return "-1"; }
                string cs_OpenId = "-1";
                for (int i = 0; i < _c_s.Count; i++)
                {
                    if (_x.Content.Contains(_c_s[i].WF_NickName))
                    {
                        cs_OpenId = _c_s[i].CSC_CS_OpenId;
                        break;
                    }
                }
                return cs_OpenId;
            }
        }
    }
    #endregion    
}