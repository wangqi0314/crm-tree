using CRMTree.Model;
using CRMTree.Model.Wechat;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.DAL
{
    /// <summary>
    /// 对于微信提供相关的数据交互类
    /// </summary>
    public class DL_Wechat
    {

        #region 粉丝的消息记录跟客户的回复消息
        /// <summary>
        /// 获取粉丝的消息记录
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public MD_FansList GetAllFanContent(string OpenId)
        {
            string sql = "select a.*,U.AU_Username from CT_All_Users U right join (select T.WT_LogContent,F.WF_OpenId,F.WF_NickName,F.WF_HeadImgurl,T.WT_CreateTime,'' AU_Code from CT_Wechat_Fans F inner join CT_Wechat_TrackLog T on WF_OpenId=WT_FromOpenId where WT_LogType=1 and F.WF_OpenId='" + OpenId + "' union select WR_Content,WR_WT_Openid,'','',WR_CreateTime,WR_AU_Code AU_Code from CT_Wechat_ReplyLog where WR_WT_Openid='" + OpenId + "')a  on U.AU_Code=a.AU_Code order by a.WT_CreateTime desc ;";
            
            MD_FansList o = new MD_FansList();
            o.Fans = DataHelper.ConvertToList<CT_Wechat_Fan>(sql);
            return o;
        }
        public int AddReplyLog(CT_Wechat_ReplyLog o)
        {
            string sql = " insert into CT_Wechat_ReplyLog(WR_WT_Openid,WR_AU_Code,WR_Type,WR_Content)values('" + o.WR_WT_Openid + "'," + o.WR_AU_Code + "," + o.WR_Type + ",'" + o.WR_Content + "');";
            int i = SqlHelper.ExecuteNonQuery(sql);
            return i;
        }
        #endregion

        #region 微信上传的媒体文件
        /// <summary>
        ///  获取活动宣传相关的多媒体文件，缩列图，图文 CG_EV_Type=1 Campaign  WM_Tpe=4,缩列图  WM_Tpe=5,图文模板
        /// </summary>
        /// <param name="CG_EV_Code"></param>
        /// <param name="CG_EV_Type"></param>
        /// <param name="WM_Tpe"></param>
        /// <returns></returns>
        public CT_Wechat_Multimedium GETMultimedium(int CG_EV_Code, int CG_EV_Type, int WM_Tpe,string FileName)
        {
            string sql = "SELECT * FROM CT_Wechat_Multimedia WHERE WM_CG_EV_CODE=" + CG_EV_Code + " AND WM_CG_EV_TYPE=" + CG_EV_Type + " AND WM_Tpe=" + WM_Tpe + " AND WM_fileName='"+FileName+"';";

            CT_Wechat_Multimedium o = DataHelper.ConvertToObject<CT_Wechat_Multimedium>(sql);
            return o;
        }
        /// <summary>
        /// 对活动宣传的多媒体文件延长有效期
        /// </summary>
        /// <param name="CG_EV_Code"></param>
        /// <param name="CG_EV_Type"></param>
        /// <param name="WM_INVALIDATION"></param>
        /// <returns></returns>
        public int UpdateMultimedium(int CG_EV_Code, int CG_EV_Type, int WM_Tpe,string fileName, string Media_Id, DateTime WM_INVALIDATION)
        {
            string sql = "UPDATE CT_Wechat_Multimedia SET WM_Media_Id='" + Media_Id + "',WM_INVALIDATION='" + WM_INVALIDATION + "' WHERE WM_CG_EV_CODE=" + CG_EV_Code + " AND WM_CG_EV_TYPE=" + CG_EV_Type + " AND WM_Tpe=" + WM_Tpe + " AND WM_fileName='" + fileName + "';";
            int i = SqlHelper.ExecuteNonQuery(sql);
            return i;
        }
        /// <summary>
        /// 对于活动的宣传，添加新的多媒体文件
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int AddMultimedium(CT_Wechat_Multimedium o)
        {
            string sql = "INSERT INTO CT_Wechat_Multimedia([WM_CG_EV_Code] ,[WM_CG_EV_Type] ,[WM_Tpe] ,[WM_Media],[WM_Media_Id] ,[WM_Create_Dt],[WM_Invalidation] ,[WM_fileName]) VALUES(" + o.WM_CG_EV_Code + "," + o.WM_CG_EV_Type + "," + o.WM_Tpe + ",'" + o.WM_Media + "','" + o.WM_Media_Id + "','" + o.WM_Create_Dt + "','" + o.WM_Invalidation + "','"+o.WM_fileName+"');";
            int i = SqlHelper.ExecuteNonQuery(sql);
            return i;
        }
        #endregion

        /// <summary>
        /// 获取绑定了微信的用户列表
        /// </summary>
        /// <param name="Users"></param>
        /// <returns></returns>
        public IList<CT_All_Users> GetWM_User(string Users)
        {
            string sql=@"SELECT MB_OpenID,AU.* FROM CT_Wechat_Member WM INNER JOIN CT_All_Users AU ON AU_Code=WM.MB_AU_Code
                        WHERE AU_Code IN (" + Users + ");";
            
            return DataHelper.ConvertToList<CT_All_Users>(sql);
        }
    }
}
