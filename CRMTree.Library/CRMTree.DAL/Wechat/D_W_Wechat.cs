using CRMTree.Model;
using CRMTree.Model.Wechat;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.DAL.Wechat
{
    /// <summary>
    /// 系统异常记录和测试日志
    /// </summary>
    public class D_W_Exception
    {
        #region Insert
        public int AddLog(string XML, string STR)
        {
            string sql = "INSERT INTO CT_Wechat_Log(WL_XML,WL_STR) VALUES('" + XML + "','" + STR + "');";
            int i = SqlHelper.ExecuteNonQuery( sql);
            return i;
        }
        public int AddExcep(string Action, string ActionCode, string Msg)
        {
            string sql = "INSERT INTO CT_Wechat_Exception VALUES('" + Action + "','" + ActionCode + "','" + Msg + "',GETDATE());";
            int i = SqlHelper.ExecuteNonQuery( sql);
            return i;
        }
        #endregion
    }
    /// <summary>
    /// 对于粉丝的处理
    /// </summary>
    public class D_W_Fans
    {
        #region Add
        public int AddFans(CT_Wechat_Fan fan)
        {
            string sql = "INSERT INTO CT_Wechat_Fans VALUES('" + fan.WF_OpenId + "','" + fan.WF_NickName + "','" + fan.WF_Sex + "','" + fan.WF_City + "','" + fan.WF_Province + "','" + fan.WF_Country + "','" + fan.WF_HeadImgurl + "',GETDATE(),'',1);";
            int i = SqlHelper.ExecuteNonQuery(sql);
            return i;
        }
        #endregion
        #region Get
        public CT_Wechat_Fan GetFans(string OpenId)
        {
            string sql = "SELECT * FROM CT_Wechat_Fans WHERE WF_OpenId='" + OpenId + "';";

            CT_Wechat_Fan o = DataHelper.ConvertToObject<CT_Wechat_Fan>(sql);
            return o;
        }
        public string GetFans_Json(string OpenId)
        {
            string sql = string.Format(@"SELECT * FROM CT_Wechat_Fans WF INNER JOIN CT_Wechat_Member MB ON WF.WF_OpenId=MB.MB_OpenID WHERE WF_OpenId='{0}';", OpenId);
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 获取的所有关注的粉丝
        /// </summary>
        /// <returns></returns>
        public MD_FansList GetAllFan()
        {
            string sql = "SELECT * FROM CT_Wechat_Fans WHERE WF_DataStatus=1;";
            DataSet ds = SqlHelper.ExecuteDataset(sql);

            MD_FansList o = new MD_FansList();
            o.Fans = DataHelper.ConvertToList<CT_Wechat_Fan>(ds);
            return o;
        }
        #endregion
        #region Update
        public int UpdateFans(string OpenId, int Status)
        {
            string sql = "UPDATE CT_Wechat_Fans SET WF_unSubscribeTime=GETDATE(),WF_DataStatus=" + Status + " WHERE WF_OpenId='" + OpenId + "';";
            int i = SqlHelper.ExecuteNonQuery( sql);
            return i;
        }
        #endregion
    }
    /// <summary>
    /// 注册后的粉丝
    /// </summary>
    public class D_W_CT_Member
    {
        #region Get
        /// <summary>
        /// 获取注册后的粉丝信息 type=1 code=OpenID； type=2 code=De_code
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public CT_Wechat_Member GetMember(string Code, int _type)
        {
            string sql = string.Empty;
            if (_type == 1)
            {
                sql = "SELECT AU_Code,MB.* FROM CT_Dealer_Empl INNER JOIN CT_Wechat_Member MB ON DE_AU_Code=MB_AU_Code INNER JOIN CT_All_Users ON AU_Code=DE_Code WHERE MB_OpenID='" + Code + "';";
            }
            else if (_type == 2)
            {
                sql = "SELECT AU_Name,MB.* FROM CT_Dealer_Empl INNER JOIN CT_Wechat_Member MB ON DE_AU_Code=MB_AU_Code INNER JOIN CT_All_Users ON AU_Code=DE_AU_Code WHERE DE_Code=" + Code + ";";
            }

            CT_Wechat_Member o = DataHelper.ConvertToObject<CT_Wechat_Member>(sql);
            return o;
        }
        #endregion
    }
    /// <summary>
    /// 记录粉丝发送的消息
    /// </summary>
    public class D_W_TrackLog
    {
        #region Add
        public int AddTrackLog(string fromOpenId, string toUserName, string Content, string date)
        {
            string sql = "INSERT INTO CT_Wechat_TrackLog VALUES('" + fromOpenId + "','" + toUserName + "',1,'" + Content + "','" + date + "');";
            int i = SqlHelper.ExecuteNonQuery(sql);
            return i;
        }
        #endregion
    }
    /// <summary>
    /// 对于用户是否在线的处理
    /// </summary>
    public class D_W_Online
    {
        #region Add
        public int AddOnline(string OpenId, string ON_date, string Key)
        {
            string sql = "INSERT INTO CT_Wechat_Online VALUES('" + OpenId + "','" + ON_date + "','" + Key + "');";
            int i = SqlHelper.ExecuteNonQuery(sql);
            return i;
        }
        #endregion
        #region Get
        public CT_Wechat_Online GetOnline(string OpenId)
        {
            string sql = "SELECT * FROM CT_Wechat_Online WHERE WO_OpenId='" + OpenId + "';";

            CT_Wechat_Online o = DataHelper.ConvertToObject<CT_Wechat_Online>(sql);
            return o;
        }
        #endregion
        #region Update
        public int UpdateOnline(string OpenId, string ON_date, string Key)
        {
            string sql = "UPDATE CT_Wechat_Online SET WO_DateTime='" + ON_date + "',WO_Key='" + Key + "' WHERE WO_OpenId='" + OpenId + "';";
            int i = SqlHelper.ExecuteNonQuery( sql);
            return i;
        }
        #endregion
    }
    /// <summary>
    /// 对客服聊天建立连接
    /// </summary>
    public class D_W_CustomerServiceConnection
    {
        #region Add
        public int AddServiceConnection(CT_Wechat_CustomerServiceConnection o)
        {
            string sql = "INSERT INTO CT_Wechat_CustomerServiceConnection([CSC_AD_OpenId],[CSC_CS_OpenId] ,[CSC_Connection_dt],[CSC_Connection_Status]) VALUES('" + o.CSC_AD_OpenId + "','" + o.CSC_CS_OpenId + "' ,'" + o.CSC_Connection_dt + "'," + o.CSC_Connection_Status + ");";
            int i = SqlHelper.ExecuteNonQuery(sql);
            return i;
        }
        #endregion
        #region Get
        public CT_Wechat_CustomerServiceConnection GetServiceConnection(string AD_OpenId, string CS_OpenId)
        {
            string sql = "SELECT * FROM CT_Wechat_CustomerServiceConnection WHERE CSC_AD_OPENID='" + AD_OpenId + "' AND CSC_CS_OpenId='" + CS_OpenId + "';";

            CT_Wechat_CustomerServiceConnection o = DataHelper.ConvertToObject<CT_Wechat_CustomerServiceConnection>(sql);
            return o;
        }
        /// <summary>
        /// 获取正则建立连接的客服信息，1代表客服，2代表客户
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public CT_Wechat_CustomerServiceConnection GetServiceConnection(string OpenId, int _type)
        {
            string sql = string.Empty;
            if (_type == 1)
            {
                sql = "SELECT TOP 1 * FROM CT_Wechat_CustomerServiceConnection WHERE CSC_AD_OpenId='" + OpenId + "' AND CSC_Connection_Status=1 ORDER BY CSC_Connection_dt DESC;";
            }
            else if (_type == 2)
            {
                sql = "SELECT TOP 1 * FROM CT_Wechat_CustomerServiceConnection WHERE CSC_CS_OpenId='" + OpenId + "' AND CSC_Connection_Status=1 ORDER BY CSC_Connection_dt DESC;";
            }

            CT_Wechat_CustomerServiceConnection o = DataHelper.ConvertToObject<CT_Wechat_CustomerServiceConnection>(sql);
            return o;
        }
        public IList<CT_Wechat_CustomerServiceConnection> GetAdviserConnection(string OpenId)
        {
            string sql = sql = "SELECT *,F.WF_NickName FROM CT_Wechat_CustomerServiceConnection C INNER JOIN CT_Wechat_Fans F ON C.CSC_CS_OpenId=F.WF_OpenId WHERE CSC_Connection_Status=1 AND CSC_AD_OpenId='" + OpenId + "' AND CSC_Connection_dt>'" + DateTime.Now.AddMinutes(-10) + "' ORDER BY CSC_Connection_dt DESC;";

            DataSet ds = SqlHelper.ExecuteDataset(sql);

            IList<CT_Wechat_CustomerServiceConnection> o = DataHelper.ConvertToList<CT_Wechat_CustomerServiceConnection>(ds);
            return o;
        }
        #endregion
        #region Update
        public int UpdateServiceConnection(CT_Wechat_CustomerServiceConnection o)
        {
            string sql = "  UPDATE CT_Wechat_CustomerServiceConnection SET CSC_Connection_dt='" + o.CSC_Connection_dt + "',  CSC_Connection_Status=" + o.CSC_Connection_Status + " WHERE CSC_AD_OPENID='" + o.CSC_AD_OpenId + "' AND CSC_CS_OpenId='" + o.CSC_CS_OpenId + "';";
            int i = SqlHelper.ExecuteNonQuery(sql);
            return i;
        }
        #endregion
    }
    /// <summary>
    /// 处理客服消息
    /// </summary>
    public class D_W_CustomSservice
    {
        #region Add
        public int AddCustomSservice(CT_Wechat_CustomSservice o)
        {
            string sql = "INSERT INTO CT_Wechat_CustomSservice([WCS_FromOpenId],[WCS_ToOpenId],[WCS_Type],[WCS_Content],[WM_CreateTime]) VALUES('" + o.WCS_FromOpenId + "','" + o.WCS_ToOpenId + "'," + o.WCS_Type + ",'" + o.WCS_Content + "','" + o.WM_CreateTime + "');";
            int i = SqlHelper.ExecuteNonQuery( sql);
            return i;
        }
        public int AddCustomSservice(string AD_OpenId, string CS_OpenId, string Notes, int DH_legacy)
        {
            SqlParameter[] parameters = { new SqlParameter("@AD_OpenId", SqlDbType.VarChar,200),
                                          new SqlParameter("@CS_OpenId", SqlDbType.VarChar,200),
                                          new SqlParameter("@Notes", SqlDbType.NVarChar,500),
                                          new SqlParameter("@DH_legacy", SqlDbType.BigInt)
                                    };
            parameters[0].Value = AD_OpenId;
            parameters[1].Value = CS_OpenId;
            parameters[2].Value = Notes;
            parameters[3].Value = DH_legacy;
            return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                                  "ADD_Dialog_Hist", parameters);
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
            string sql = "SELECT TOP 1 * FROM CT_Wechat_CustomSservice WHERE WCS_FromOpenId='" + OpenId + "' ORDER BY WM_CreateTime DESC";
            return DataHelper.ConvertToObject<CT_Wechat_CustomSservice>(sql);
        }
        #endregion
    }

}
