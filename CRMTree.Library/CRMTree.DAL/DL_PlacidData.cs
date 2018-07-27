using CRMTree.Model;
using CRMTREE.BasePage;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.DAL
{
    /// <summary>
    /// 此类用做获取数据内的一些电话，邮件，地址等的一些联系方式和设置信息
    /// </summary>
    public class DL_PlacidData
    {
       
        /// <summary>
        /// 给CT_History_Campaign插入操作历史数据   废弃
        /// </summary>
        /// <param name="CG_His_Cam"></param>
        /// <returns></returns>
        public static int insert_CT_History_Campaigns(CT_History_Campaigns CG_His_Cam)
        {
            string insertSql = string.Empty;
            insertSql = @"insert into CT_History_Campaigns(HG_CG_Code,HG_Run_Time,HG_CG_UType,HG_CG_AD_AM_Code,HG_CG_Script,
                          HG_CG_Start_dt,HG_CG_End_dt,HG_RP_Name_EN,HG_RP_Name_CN)
                            values(@HG_CG_Code,@HG_Run_Time,@HG_CG_UType,@HG_CG_AD_AM_Code,@HG_CG_Script,@HG_CG_Start_dt,
                             @HG_CG_End_dt,@HG_RP_Name_EN,@HG_RP_Name_CN);";
            SqlParameter[] parameters = { 
                                            new SqlParameter("@HG_CG_Code", SqlDbType.Int),
                                            new SqlParameter("@HG_Run_Time", SqlDbType.DateTime),
                                            new SqlParameter("@HG_CG_UType", SqlDbType.Int),
                                            new SqlParameter("@HG_CG_AD_AM_Code", SqlDbType.Int),
                                            new SqlParameter("@HG_CG_Script", SqlDbType.NVarChar,250),
                                            new SqlParameter("@HG_CG_Start_dt", SqlDbType.NVarChar,250),
                                            new SqlParameter("@HG_CG_End_dt", SqlDbType.NVarChar,250),
                                            new SqlParameter("@HG_RP_Name_EN", SqlDbType.NVarChar,250),
                                            new SqlParameter("@HG_RP_Name_CN", SqlDbType.NVarChar,250),
                                        };
            parameters[0].Value = CG_His_Cam.HG_CG_Code;
            parameters[1].Value = CG_His_Cam.HG_Run_Time;
            parameters[2].Value = CG_His_Cam.HG_CG_UType;
            parameters[3].Value = CG_His_Cam.HG_CG_AD_AM_Code;
            parameters[4].Value = CG_His_Cam.HG_CG_Script;
            parameters[5].Value = CG_His_Cam.HG_CG_Start_dt;
            parameters[6].Value = CG_His_Cam.HG_CG_End_dt;
            parameters[7].Value = CG_His_Cam.HG_RP_Name_EN;
            parameters[8].Value = CG_His_Cam.HG_RP_Name_CN;
            return ExecuteNonQuery(insertSql, parameters);
        }
        public static int insert_CT_Report_Hist(CT_Report_Hist Report_Hist)
        {
            string insertSql = string.Empty;
            insertSql = @"insert into CT_Report_Hist(RH_RP_Code,RH_AU_Code,RH_Name_EN,RH_Name_CN,RH_Query,RH_Run_Time)
                            values(@RH_RP_Code,@RH_AU_Code,@RH_Name_EN,@RH_Name_CN,@RH_Query,@RH_Run_Time);";
            SqlParameter[] parameters = { 
                                            new SqlParameter("@RH_RP_Code", SqlDbType.Int),
                                            new SqlParameter("@RH_AU_Code", SqlDbType.Int),
                                            new SqlParameter("@RH_Name_EN", SqlDbType.NVarChar,250),
                                            new SqlParameter("@RH_Name_CN", SqlDbType.NVarChar,250),
                                            new SqlParameter("@RH_Query", SqlDbType.NVarChar),
                                            new SqlParameter("@RH_Run_Time", SqlDbType.DateTime)
                                        };
            parameters[0].Value = Report_Hist.RH_RP_Code;
            parameters[1].Value = Report_Hist.RH_AU_Code;
            parameters[2].Value = Report_Hist.RH_Name_EN;
            parameters[3].Value = Report_Hist.RH_Name_CN;
            parameters[4].Value = Report_Hist.RH_Query;
            parameters[5].Value = Report_Hist.RH_Run_Time;
            return ExecuteNonQuery(insertSql, parameters);
        }


        public static DataTable getCam_tags()
        {
            string selectSql = string.Empty;
            selectSql = @"select * from CT_Camp_Tags;";
            return ExecutionSelect(selectSql);
        }
        public static DataTable getPieChatsTitle(int FP_Code,int pr=0)
        {
            string selectSql = string.Empty;
            selectSql = string.Format(@"SELECT 
 [FP_Code]
,[FP_RP_Code]
,dbo.{0}(FP_Title_EN,FP_RP_Code) as FP_Title_EN 
,dbo.{0}(FP_Title_CN,FP_RP_Code) as FP_Title_CN
FROM CT_Fields_Pie WHERE FP_Code=" + FP_Code + "", pr == 10 ? "F_Format_Paramters_Temp" : "F_Format_Paramters");
            return ExecutionSelect(selectSql);
        }
        public static DataTable getPieChatsTitleII(int RP_Code)
        {
            string selectSql = string.Empty;
            selectSql = @"SELECT * FROM CT_FIELDS_Pie WHERE FP_CODE=" + RP_Code + ";";
            return ExecutionSelect(selectSql);
        }
        public static DataTable getBarChatsTitle(int RP_Code)
        {
            string selectSql = string.Empty;
            selectSql = @"SELECT * FROM CT_FIELDS_BAR WHERE FB_CODE=" + RP_Code + ";";
            return ExecutionSelect(selectSql);
        }
        public static DataTable getMultiChartParam(int RP_Code)
        {
            string selectSql = string.Empty;
            selectSql = @"SELECT * FROM CT_FIELDS_MultiChart WHERE FM_CODE=" + RP_Code + ";";
            return ExecutionSelect(selectSql);
        }
        public static DataTable getDGaugChartParam(int RP_Code)
        {
            string selectSql = string.Empty;
            selectSql = @"SELECT * FROM CT_FIELDS_Gauge WHERE FG_CODE=" + RP_Code + ";";
            return ExecutionSelect(selectSql);
        }
        public static DataTable getDrillChartParam(int RP_Code)
        {
            string selectSql = string.Empty;
            selectSql = @"SELECT * FROM CT_FIELDS_Drill WHERE FD_FL_CODE=" + RP_Code + " order by FD_Level Asc;";
            return ExecutionSelect(selectSql);
        }
        public static DataTable getSchChatsTitle(int RP_Code)
        {
            string selectSql = string.Empty;
            selectSql = @"SELECT * FROM CT_FIELDS_Sch WHERE FS_CODE=" + RP_Code + ";";
            return ExecutionSelect(selectSql);
        }
        /// <summary>
        /// 获取公司的顾问，返回Table格式数据
        /// </summary>
        /// <param name="DE_UType"></param>
        /// <param name="DE_AD_OM_Code"></param>
        /// <returns></returns>
        public static DataTable GetDealer_Empl(int DE_UType, int DE_AD_OM_Code)
        {
            string selectSql = @"select * from CT_Dealer_Empl where DE_Type=2 and DE_UType=" + DE_UType + " and DE_AD_OM_Code=" + DE_AD_OM_Code + ";";
            return ExecutionSelect(selectSql);
        }

        /// <summary>
        /// 获取当前经销商需要操作的活动
        /// </summary>
        /// <param name="CH_CG_Code"></param>
        /// <param name="CH_UType"></param>
        /// <param name="CH_AD_OM_Code"></param>
        /// <returns></returns>
        public static DataTable GetComm_HisList(long top, int type, int CH_CG_Code, int CH_UType, int CH_AD_OM_Code, DateTime CH_Update_dt)
        {
            string sqlCamm_His = @"SELECT top " + top + "  * FROM CT_Comm_History where CH_Event_Flg=" + type + " and CH_CG_Code=" + CH_CG_Code + " and CH_UType="
                + CH_UType + " and CH_AD_OM_Code=" + CH_AD_OM_Code + "   order by CH_Update_dt desc;";
            return ExecutionSelect(sqlCamm_His);
        }
        #region 获取通信联系信息
        /// <summary>
        /// 用 where IN的值获取 电话列表
        /// </summary>
        /// <param name="SqlIn"></param>
        /// <returns></returns>
        public static DataTable getPhoneList(string SqlIn)
        {
            string sqlPhone = "SELECT * FROM CT_Phone_List PL INNER JOIN (SELECT PL_AU_AD_CODE,MIN(PL_PREF)A FROM CT_Phone_List WHERE PL_AU_AD_Code in("
                + SqlIn + ") GROUP BY PL_AU_AD_Code) B ON PL.PL_AU_AD_Code=B.PL_AU_AD_Code AND PL.PL_Pref=B.A WHERE PL.PL_UType=1;";
            return ExecutionSelect(sqlPhone);
        }
        /// <summary>
        /// 用 where IN的值获取 消息列表
        /// </summary>
        /// <param name="SqlIn"></param>
        /// <returns></returns>
        public static DataTable getMessagingList(string SqlIn)
        {
            string sqlMessaging = "SELECT * FROM CT_Messaging_List ML INNER JOIN ( SELECT ML_AU_AD_Code,MIN(ML_PREF) A FROM CT_Messaging_List WHERE ML_AU_AD_Code in("
                + SqlIn + ") GROUP BY ML_AU_AD_Code)B ON ML.ML_AU_AD_Code=b.ML_AU_AD_Code AND ML.ML_Pref=B.A WHERE ML.ML_UType=1;";
            return ExecutionSelect(sqlMessaging);
        }
        /// <summary>
        /// 用 where IN的值获取 Email列表
        /// </summary>
        /// <param name="SqlIn"></param>
        /// <returns></returns>
        public static DataTable getEmailList(string SqlIn)
        {
            string sqlEmail = "SELECT * FROM CT_Email_List EL INNER JOIN (SELECT EL_AU_AD_Code,MIN(EL_Pref) A FROM CT_Email_List WHERE EL_AU_AD_Code in("
                + SqlIn + ") GROUP BY EL_AU_AD_Code)B ON EL.EL_AU_AD_Code=b.EL_AU_AD_Code AND EL.EL_Pref=B.A WHERE EL.EL_UType=1;";
            return ExecutionSelect(sqlEmail);
        }
        #endregion

        #region 私有方法

        #region 无干扰执行SQL
        /// <summary>
        /// 单一的执行一条selectSQL
        /// </summary>
        /// <param name="SelectSql"></param>
        /// <returns></returns>
        private static DataTable ExecutionSelect(string SelectSql)
        {
            return ExecutionSelect(SelectSql, null);
        }
        /// <summary>
        ///  单一的执行一条selectSQL,根据参数
        /// </summary>
        /// <param name="SelectSql"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private static DataTable ExecutionSelect(string SelectSql, params SqlParameter[] commandParameters)
        {
            try
            {
                DataTable dt;
                if (commandParameters != null)
                {
                    dt = SqlHelper.ExecuteDataset(CommandType.Text, SelectSql, commandParameters).Tables[0];
                }
                else
                {
                    dt = SqlHelper.ExecuteDataset(CommandType.Text, SelectSql).Tables[0];
                }
                return dt;
            }
            catch { return null; }
        }
        /// <summary>
        /// 单一的执行一条SQL
        /// </summary>
        /// <param name="SelectSql"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private static int ExecuteNonQuery(string SelectSql, params SqlParameter[] commandParameters)
        {
            try
            {
                int count = SqlHelper.ExecuteNonQuery( CommandType.Text, SelectSql, commandParameters);
                return count;
            }
            catch { return -1; }
        }
        #endregion

        #endregion
    }
}
