using CRMTree.DAL.Wechat;
using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Model.Reports;
using CRMTree.Model.Wechat;
using CRMTREE.BasePage;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRMTree.DAL
{
    /// <summary>
    /// 此类适用于操作表CT_Reports,以及相关的报表使用的各种方式
    /// </summary>
    public class DL_Reports
    {
        #region GET_Reports

        #region GetList
        /// <summary>
        /// 获取Report表的 Name 跟 Desc 有分页
        /// </summary>
        /// <param name="AU_TYPE">用户类型 1 Dealer 2 OEM 3 DealerGroup</param>
        /// <param name="CG_AD_OM_Code">用户公司id</param>
        /// <param name="RP_CODE_LIST">报表RP_code，形式 1,2,3,4,5,</param>
        /// <param name="primarykey">列表主键</param>
        /// <param name="fields">列表显示字段</param>
        /// <param name="ordefiled">排序字段</param>
        /// <param name="orderway">排序方式</param>
        /// <param name="currentpage">当前页</param>
        /// <param name="pagesize">每页数量</param>
        /// <param name="pagecount">总页数</param>
        /// <param name="rowscount">总行数</param>
        /// <returns></returns>
        public IList<CT_Reports> GetReportList(string RP_CODE_LIST, string ordefiled, string orderway, int currentpage, int pagesize, string RP_Cat, out int pagecount, out int rowscount, bool ChartOrNot)
        {
            #region 参数
            SqlParameter[] _p = { 
                                          new SqlParameter("@primarykey", SqlDbType.VarChar,500),
                                          new SqlParameter("@fields", SqlDbType.VarChar,500),
                                          new SqlParameter("@ordefiled", SqlDbType.VarChar,500),
                                          new SqlParameter("@orderway", SqlDbType.VarChar,500),
                                          new SqlParameter("@currentpage", SqlDbType.Int,4),
                                          new SqlParameter("@pagesize", SqlDbType.Int,4),
                                          new SqlParameter("@pagecount", SqlDbType.Int,4),
                                          new SqlParameter("@rowscount", SqlDbType.Int,4),
                                          new SqlParameter("@RP_CODE_LIST", SqlDbType.NVarChar,1000),
                                          new SqlParameter("@where", SqlDbType.NVarChar,1000),
                                    };
            _p[0].Value = "RP_Code";
            _p[1].Value = "*";
            _p[2].Value = ordefiled;
            _p[3].Value = orderway;
            _p[4].Value = currentpage;
            _p[5].Value = pagesize;
            _p[6].Direction = ParameterDirection.Output;
            _p[7].Direction = ParameterDirection.Output;
            _p[8].Value = RP_CODE_LIST;
            _p[9].Value = RP_Cat;

            #endregion
            string procedureName = string.Empty;

            if (ChartOrNot)
                procedureName = "RP_getChartsList";
            else
                procedureName = "RP_getReportList";

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, procedureName, _p);
            if (ds == null || ds.Tables.Count == 0)
            {
                pagecount = 0;
                rowscount = 0;
                return null;
            }
            pagecount = Convert.ToInt32(_p[6].Value.ToString());
            rowscount = Convert.ToInt32(_p[7].Value.ToString());
            IList<CT_Reports> RPList = DataHelper.ConvertToList<CT_Reports>(ds);
            return RPList;
        }

        #endregion

        /// <summary>
        /// 获取报表信息，没有附带参数值
        /// </summary>
        /// <param name="RP_Code"></param>
        /// <returns></returns>
        public CT_Reports GetReprot(int RP_Code)
        {
            string sql = string.Format(@"SELECT * FROM CT_Reports WHERE RP_Code={0};", RP_Code);
            CT_Reports o = DataHelper.ConvertToObject<CT_Reports>(sql);
            return o;
        }
        /// <summary>
        /// 获取报表,以及报表相关的参数值，单一的参数列表获取,只有默认值参数
        /// </summary>
        /// <param name="RP_Code"></param>
        /// <returns></returns>
        public MD_ReportList GetReportValue(int RP_Code)
        {
            string Sql = @"SELECT RP_Code,RP_Name_EN,RP_Name_CN,RP_Desc_EN,RP_Desc_CN,RP_Query,PL_Code,PL_Tag,PL_Default FROM CT_Reports RP
                             INNER JOIN CT_Paramters_list PL on PL.PL_RP_Code=RP.RP_Code
                             WHERE RP_Code=" + RP_Code + "";

            MD_ReportList o = new MD_ReportList();
            o.CT_Reports_List = DataHelper.ConvertToList<CT_Reports>(Sql);
            return o;
        }
        /// <summary>
        /// 获取报表,以及报表相关的参数值=》名称、描述、query等；
        /// </summary>
        /// <param name="Param_value"></param>
        /// <returns></returns>
        public MD_ReportList GetReportValue(CT_Param_Value Param_value)
        {
            #region 参数
            SqlParameter[] _p = { new SqlParameter("@RP_Code", SqlDbType.Int,4),
                                          new SqlParameter("@PV_Type", SqlDbType.Int,4),
                                          new SqlParameter("@PV_CG_Code", SqlDbType.Int,4),
                                          new SqlParameter("@PV_UType", SqlDbType.Int,4),
                                          new SqlParameter("@AD_Code", SqlDbType.Int,4),
                                          new SqlParameter("@DG_Code", SqlDbType.Int,4),
                                          new SqlParameter("@OM_Code", SqlDbType.Int,4),
                                    };
            _p[0].Value = Param_value.RP_Code;
            _p[1].Value = Param_value.PV_Type;
            _p[2].Value = Param_value.PV_CG_Code;
            _p[3].Value = Param_value.PV_UType;
            if (Param_value.PV_UType == 1)
            {
                _p[4].Value = Param_value.PV_AD_OM_Code;
                _p[5].Value = null;
                _p[6].Value = null;
            }
            else if (Param_value.PV_UType == 2)
            {
                _p[4].Value = null;
                _p[5].Value = Param_value.PV_AD_OM_Code;
                _p[6].Value = null;
            }
            else if (Param_value.PV_UType == 3)
            {
                _p[4].Value = null;
                _p[5].Value = null;
                _p[6].Value = Param_value.PV_AD_OM_Code;
            }
            #endregion
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "RP_GetReportValue", _p);

            MD_ReportList o = new MD_ReportList();
            o.CT_Reports_List = DataHelper.ConvertToList<CT_Reports>(ds);
            return o;
        }
        /// <summary>
        /// 根据不同的ChatType类型获取相关的报表
        /// </summary>
        /// <param name="chartType"></param>
        /// <param name="FL_FB_Code"></param>
        /// <returns></returns>
        public CT_Reports GetReprot_Chat(ChartType chartType, int FL_FB_Code)
        {
            string SQL = string.Empty;
            if (chartType == ChartType.Pie)
            {
                SQL = @"SELECT R.* FROM CT_Reports R INNER JOIN CT_Fields_Pie ON RP_Code=FP_RP_Code WHERE FP_Code="
                    + FL_FB_Code + ";";
            }
            else if (chartType == ChartType.Bar)
            {
                SQL = @"SELECT R.* FROM CT_Reports R INNER JOIN CT_Fields_Bar ON RP_Code=FB_RP_Code  WHERE FB_Code="
                    + FL_FB_Code + ";";
            }
            else if (chartType == ChartType.Sch)
            {
                SQL = @"SELECT R.* FROM CT_Reports R INNER JOIN CT_Fields_Sch ON RP_Code=FS_RP_Code  WHERE FS_Code="
                       + FL_FB_Code + ";";
            }
            else if (chartType == ChartType.Multi)
            {
                SQL = @"SELECT R.* FROM CT_Reports R INNER JOIN CT_Fields_MultiChart ON RP_Code=FM_RP_Code   WHERE RP_Code="
                       + FL_FB_Code + ";";
            }
            else if (chartType == ChartType.Gauge)
            {
                SQL = @"SELECT R.* FROM CT_Reports R INNER JOIN CT_Fields_Gauge ON RP_Code=FG_RP_Code   WHERE RP_Code="
                       + FL_FB_Code + ";";
            }
            else if (chartType == ChartType.Drill)
            {
                SQL = @"SELECT R.* FROM CT_Reports R INNER JOIN CT_Fields_Drill ON RP_Code=FD_RP_Code   WHERE RP_Code="
                       + FL_FB_Code + ";";
            }
            else if (chartType == ChartType.List)
            {
                SQL = @"SELECT R.* FROM CT_Reports R INNER JOIN CT_Fields_lists ON RP_Code=FL_RP_Code  WHERE FL_Code="
                    + FL_FB_Code + ";";
            }

            CT_Reports o = DataHelper.ConvertToObject<CT_Reports>(SQL);
            return o;
        }
        /// <summary>
        /// CT_Fields_list
        /// </summary>
        /// <param name="RP_Code"></param>
        /// <returns></returns>
        public Fields_list GetFields_list(int RP_Code)
        {
            string sql = " SELECT FL.* FROM CT_Fields_lists FL INNER JOIN CT_Reports RP ON FL.FL_RP_Code=RP.RP_Code WHERE RP.RP_Code=" + RP_Code + ";";
            DataSet ds = SqlHelper.ExecuteDataset(sql);

            Fields_list o = new Fields_list();
            o.List = DataHelper.ConvertToList<CT_Fields_list>(ds);
            return o;
        }

        /// <summary>
        /// 获取显示列表的标题
        /// </summary>
        /// <param name="PL_Code"></param>
        /// <returns></returns>
        public DataTable GetExport_Title(int PL_Code)
        {
            string sql = "select FN_Code,FN_FieldName,FN_Desc_EN,FN_Desc_CN from CT_Fields_Names FN inner join CT_Fields_lists FL on FN.FN_FL_FB_Code=FL.FL_Code where FL.FL_Code="
                + PL_Code + " order by FN_Order;";
            return SqlHelper.ExecuteDataset(sql).Tables[0];
        }
        public DataTable GetExport_Type50(int PL_Code, int Type)
        {
            string sql = @"SELECT * FROM CT_Fields_Names WHERE FN_FL_FB_Code = " + PL_Code + " AND FN_TYPE=" + Type + ";";
            DataSet ds = SqlHelper.ExecuteDataset(sql);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            return ds.Tables[0];
        }
        #endregion

        #region Insert
        public int insertTags(int AU_TYPE, int CG_AD_OM_Code, string PL_Code_List, string Pl_Val_List)
        {
            SqlParameter[] _p = { new SqlParameter("@AU_Type", SqlDbType.Int,4),
                                          new SqlParameter("@AU_AD_OM_Code", SqlDbType.Int,4),
                                          new SqlParameter("@PL_Code_List", SqlDbType.VarChar,500),
                                          new SqlParameter("@Pl_Val_List", SqlDbType.VarChar,500),
                                          new SqlParameter("@PV_Type", SqlDbType.Int,4),
                                          new SqlParameter("@PV_CG_Code", SqlDbType.Int,4),
                                         
                                    };
            _p[0].Value = AU_TYPE;
            _p[1].Value = CG_AD_OM_Code;
            _p[2].Value = PL_Code_List;
            _p[3].Value = Pl_Val_List;
            _p[4].Value = 0;
            _p[5].Value = -1;
            int i = SqlHelper.ExecuteNonQuery( CommandType.StoredProcedure, "RP_UPDate_Tags", _p);
            return i;
        }
        public int insertReport_Hist(long AU_Code, int RP_Code, string RP_Query, string Name_EN, string Name_CN)
        {
            CT_Report_Hist Hist = new CT_Report_Hist();
            Hist.RH_RP_Code = RP_Code;
            Hist.RH_Query = RP_Query;
            Hist.RH_AU_Code = AU_Code;
            Hist.RH_Name_EN = Name_EN;
            Hist.RH_Name_CN = Name_CN;
            Hist.RH_Run_Time = DateTime.Now;
            return DL_PlacidData.insert_CT_Report_Hist(Hist);
        }
        #endregion

        #region Run
        public int ReportRun<T>(CT_Reports R, T t, int UType, int AD_OM_Code)
        {
            string Class = t.GetType().Name;
            int n = -1;
            switch (Class)
            {
                case "CT_Campaigns":
                    n = CampaignRun(t as CT_Campaigns, R, UType, AD_OM_Code);
                    break;
                case "CT_Events":
                    n = EventRun(t as CT_Events, R, UType, AD_OM_Code);
                    break;
            }
            return n;
        }
        private int CampaignRun(CT_Campaigns o, CT_Reports R, int UType, int AD_OM_Code)
        {
            DateTime runStarTime = DateTime.Now;
            //string s = GetWechatHistory(1, o);
            int runCount = 0;
            string sql = GetCommSql(UType, AD_OM_Code, R.RP_Query, o.CG_Code, o.CG_Method, runStarTime, 0, out runCount);
            if (!String.IsNullOrEmpty(sql))
                sql += GetHistorySql_C(o, R, sql.Replace("'", "''"), runStarTime.ToString(), UType, AD_OM_Code);
            else
                return -5;
            try
            {
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    string Hsql = GetHanderSql(runCount, 0, o.CG_Code, UType, AD_OM_Code, runStarTime);
                    i = SqlHelper.ExecuteNonQuery(Hsql);
                }
                DateTime runEndTime = DateTime.Now;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private int EventRun(CT_Events o, CT_Reports R, int UType, int AD_OM_Code)
        {
            DateTime runStarTime = DateTime.Now;
            int runCount = 0;
            string sql = GetCommSql(UType, AD_OM_Code, R.RP_Query, o.EV_Code, o.EV_Method, runStarTime, 1, out runCount);
            sql += GetHistorySql_E(o, R, sql.Replace("'", "''"), runStarTime.ToString(), UType, AD_OM_Code);
            try
            {
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    string Hsql = GetHanderSql(runCount, 1, o.EV_Code, UType, AD_OM_Code, runStarTime);
                    i = SqlHelper.ExecuteNonQuery( Hsql);
                }
                DateTime runEndTime = DateTime.Now;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 获取插入表CT_Comm_History的SQL列表
        /// </summary>
        /// <param name="UType"></param>
        /// <param name="AD_OM_Code"></param>
        /// <param name="Query"></param>
        /// <param name="CG_EV_Code"></param>
        /// <param name="_m"></param>
        /// <param name="time"></param>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private string GetCommSql(int UType, int AD_OM_Code, string Query, int CG_EV_Code, string _m, DateTime time, int type, out int count)
        {
            string[] Users = GetUserCode(Query, "AU_Code").Distinct().ToArray();
            string[] Method = GetMethod(_m);
            string sql = string.Empty;
            int _count = 0;
            for (int u = 0; u < Users.Length; u++)
            {
                for (int m = 0; m < Method.Length; m++)
                {
                    if (CheckHistoryRecords(Method[m], Users[u], CG_EV_Code.ToString()) > 0)
                        continue;
                    string[] _c = GetUserCommunication(Users[u]);
                    for (int c = 0; c < _c.Length; c++)
                    {
                        if (_c[c].Trim() != "-1")
                        {
                            sql += "INSERT INTO CT_Comm_History VALUES(" + UType + "," + AD_OM_Code + "," + Method[m] + ","
                                + _c[c] + "," + Users[u] + "," + type + "," + CG_EV_Code + "," + DataConfiguration.GetPTY(type) + ",0,'" + time + "');";
                            _count++;
                        }
                    }
                }
            }
            count = _count;
            return sql;
        }

        /// <summary>
        /// Check CheckHistoryRecords
        /// </summary>
        /// <param name="CH_Type"></param>
        /// <param name="Au_Code"></param>
        /// <param name="CH_CG_CODE"></param>
        /// <returns></returns>
        public int CheckHistoryRecords(string CH_Type, string Au_Code, string CH_CG_CODE)
        {
            int UTime = 0;

            string query = "select * from CT_Comm_History where CH_Type=" + CH_Type + " and CH_AU_Code=\'" + Au_Code + "\' and CH_CG_Code=" + CH_CG_CODE;

            DataTable dtTemp = SqlHelper.ExecuteDataset( query).Tables[0];

            if (dtTemp != null && dtTemp.Rows.Count > 0)
                UTime = dtTemp.Rows.Count;

            return UTime;

        }


        /// <summary>
        /// 获取插入表CT_History_Campaigns的SQL列表
        /// </summary>
        /// <param name="_c"></param>
        /// <param name="_r"></param>
        /// <param name="_sql"></param>
        /// <param name="runTime"></param>
        /// <param name="UType"></param>
        /// <param name="PV_AD_OM_Code"></param>
        /// <returns></returns>
        private string GetHistorySql_E(CT_Events _e, CT_Reports _r, string _sql, string runTime, int UType, int PV_AD_OM_Code)
        {
            string sql = "INSERT INTO CT_History_Events VALUES(" + _e.EV_Code + ", '"
                                               + runTime + "' ," + UType + "," + PV_AD_OM_Code + " ,'" + _e.EV_Start_dt + "' ,'" + _e.EV_End_dt + "' ,'"
                                               + _e.EV_Act_S_dt + "' ,'" + _e.EV_Act_E_dt + "' ,'" + _sql + "' ,'" + _r.RP_Name_EN + "' ,'" + _r.RP_Name_CN
                                               + "','" + _e.EV_Budget + "' ,'" + _e.EV_Respnsible + "' ,'" + _e.EV_Tools + "');";
            return sql;
        }
        /// <summary>
        /// 获取插入表CT_History_Campaigns的SQL列表
        /// </summary>
        /// <param name="_c"></param>
        /// <param name="_r"></param>
        /// <param name="_sql"></param>
        /// <param name="runTime"></param>
        /// <param name="UType"></param>
        /// <param name="PV_AD_OM_Code"></param>
        /// <returns></returns>
        private string GetHistorySql_C(CT_Campaigns _c, CT_Reports _r, string _sql, string runTime, int UType, int PV_AD_OM_Code)
        {

            string sql = "INSERT INTO CT_History_Campaigns VALUES(" + _c.CG_Code + ",'" + runTime + "'," + UType + ","
                + PV_AD_OM_Code + ",'" + _c.CG_Start_Dt + "','" + _c.CG_End_Dt + "','" + _sql + "','" + _r.RP_Name_EN + "','" + _r.RP_Name_CN + "');";

            return sql;
        }
        /// <summary>
        /// 获取插入表CT_Handler的SQL列表
        /// </summary>
        /// <param name="top"></param>
        /// <param name="type"></param>
        /// <param name="CH_CG_Code"></param>
        /// <param name="CH_UType"></param>
        /// <param name="CH_AD_OM_Code"></param>
        /// <param name="CH_Update_dt"></param>
        /// <returns></returns>
        private string GetHanderSql(long top, int type, int CH_CG_Code, int CH_UType, int CH_AD_OM_Code, DateTime CH_Update_dt)
        {
            string[] _d = GetDealer_Empl(CH_UType, CH_AD_OM_Code);
            string[] _h = GetComm_HisList(top, type, CH_CG_Code, CH_UType, CH_AD_OM_Code, CH_Update_dt);
            string sql = string.Empty;
            for (int i = 0; i < _d.Length; i++)
            {
                for (int j = 0; j < _h.Length; j++)
                {
                    sql += "INSERT INTO CT_Handler VALUES(" + _h[j] + "," + _d[i] + ",0);";
                }
            }
            return sql;
        }
        /// <summary>
        /// 执行报表的SQL，返回某一列的数组形式 AU_Code
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public string[] GetUserCode(string Query, string Code)
        {
            DataTable U = QueryExecution(Query);
            string Codes = DataConfiguration.GetString_Code(U, Code);
            return Codes.Split(',');
        }
        private string[] GetMethod(string m)
        {
            return DataConfiguration.Get_CH_UType(m);
        }
        private string[] GetUserCommunication(string UserCode)
        {
            DataTable dt_Phone = DL_PlacidData.getPhoneList(UserCode);
            DataTable dt_Messaging = DL_PlacidData.getMessagingList(UserCode);
            DataTable dt_Emal = DL_PlacidData.getEmailList(UserCode);
            string _p_c = "-1", _m_c = "-1", _e_c = "-1";
            if (dt_Phone != null && dt_Phone.Rows.Count > 0)
                _p_c = dt_Phone.Rows[0]["PL_Code"].ToString();
            if (dt_Messaging != null && dt_Messaging.Rows.Count > 0)
                _m_c = dt_Messaging.Rows[0]["ML_Code"].ToString();
            if (dt_Emal != null && dt_Emal.Rows.Count > 0)
                _e_c = dt_Emal.Rows[0]["EL_Code"].ToString();
            return new string[] { _p_c, _m_c, _e_c };
        }
        private string[] GetDealer_Empl(int DE_UType, int DE_AD_OM_Code)
        {
            DataTable d = DL_PlacidData.GetDealer_Empl(DE_UType, DE_AD_OM_Code);
            string _u = DataConfiguration.GetString_Code(d, "DE_AU_Code");
            return _u.Split(',');
        }
        private string[] GetComm_HisList(long top, int type, int CH_CG_Code, int CH_UType, int CH_AD_OM_Code, DateTime CH_Update_dt)
        {
            DataTable d = DL_PlacidData.GetComm_HisList(top, type, CH_CG_Code, CH_UType, CH_AD_OM_Code, CH_Update_dt);
            string _c = DataConfiguration.GetString_Code(d, "CH_Code");
            return _c.Split(',');
        }
        #endregion

        #region 执行报表的Sql
        /// <summary>
        /// 根据报表的RP_Query来执行SQl，返回表格形式
        /// </summary>
        /// <param name="RP_Query"></param>
        /// <returns></returns>
        public static DataTable QueryExecution(string RP_Query)
        {
            try
            {
                DataTable dt = SqlHelper.ExecuteDataset( RP_Query).Tables[0];
                return dt;
            }
            catch { return null; }
        }
        public static DataTable QueryExecution(string RP_Query, bool d)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@AU_Name", ""),
                                          new SqlParameter("@Phone_No", "")
                                    };
                DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, RP_Query, parameters).Tables[0];
                return dt;
            }
            catch { return null; }
        }
        #endregion
    }
}
