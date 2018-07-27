using CRMTree.BLL.Wechat;
using CRMTree.DAL;
using CRMTree.DAL.Wechat;
using CRMTree.Model;
using CRMTree.Model.Campaigns;
using CRMTree.Model.Wechat;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    /// <summary>
    /// 对外提供的微信相关操作的使用类
    /// </summary>
    public class BL_Wechat
    {
        #region  实例化的微信相关的数据交互类
        /// <summary>
        /// 实例化的微信相关的数据交互类
        /// </summary>
        DL_Wechat wechat = new DL_Wechat();
        #endregion

        #region 微信关注的粉丝信息
        public MD_FansList GetAllFan()
        {
            D_W_Fans fan = new D_W_Fans();
            return fan.GetAllFan();
        }
        #endregion

        #region 粉丝的消息记录跟客户的回复消息
        public MD_FansList GetAllFanContent(string OpenId)
        {
            return wechat.GetAllFanContent(OpenId);
        }
        public int AddReplyLog(CT_Wechat_ReplyLog o)
        {
            return wechat.AddReplyLog(o);
        }
        #endregion

        #region 微信上传的媒体文件

        public int SendWechat_news(int CG_code, string fileName, string[] Users)
        {
            if (Users == null || Users.Length <= 0)
            {
                return 10000;
            }
            IList<string> _users_List = ReportWechat.GroupData(Users, 900);
            IList<string> _OpenIds = new List<string>();
            foreach (string users in _users_List)
            {
                IList<CT_All_Users> user_list = GetWM_User(users);
                if (user_list != null && user_list.Count > 0)
                {
                     foreach (CT_All_Users user in user_list)
                     {
                         _OpenIds.Add(user.MB_OpenID);
                     } 
                }
            }
            if (_OpenIds != null && _OpenIds.Count <= 0)
            {
                return 10000;
            }
            _OpenIds = ReportWechat.GroupData(_OpenIds, 10000);
            if (_OpenIds == null || _OpenIds.Count <= 0)
            {
                return 10000; 
            }
            BL_CamRun _camRun = new BL_CamRun();
            string _MaId = _camRun.GetMaterialId(CG_code, fileName);
            if (string.IsNullOrEmpty(_MaId))
            {
                return 10000;
            }
            int _err = 10000;
            foreach (string openids in _OpenIds)
            {
               _err = wechatHandle.SendCustom_news_ArrayString(_MaId, openids);
            }
            return _err;
        }
        #endregion

        /// <summary>
        /// 获取绑定了微信的用户列表
        /// </summary>
        /// <param name="Users"></param>
        /// <returns></returns>
        public IList<CT_All_Users> GetWM_User(string Users)
        {
            return new DL_Wechat().GetWM_User(Users);
        }

        /// <summary>
        /// 普通接口发送图文消息
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int SendCustom_News(string touser, int type)
        {
            MD_CampaignList _c = BL_Campaign.GetRecommend_Trpe(type);
            if (_c == null || _c.CampaignList == null)
            {
                wechatHandle.SendCustom_text(touser, "暂时没有相关的活动推荐给你");
                return 10000;
            }
            IList<customSend> _customSend = new List<customSend>();
            foreach (CT_Campaigns cam in _c.CampaignList)
            {
                _customSend.Add(new customSend()
                {
                    title = cam.CG_Desc,
                    description = cam.CG_Desc,
                    picurl = "http://www.daeku.com/images/1.jpg",
                    url = System.Configuration.ConfigurationManager.AppSettings["Wechat_Web_Url"] + "plupload/file/" + cam.CG_Filename,
                });
            }
            string _news = wechatHandle.custom_News(touser, _customSend);
            return wechatHandle.SendCustom_news(_news);
        }
    }
}
