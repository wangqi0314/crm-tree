using MigrationService.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MigrationService.DBConnection
{
    public class ExecuteSQL
    {
        private static string getConnectionString = string.Empty;

        #region protected static SqlConnection GetADOConnection()
        public static SqlConnection GetADOConnection()
        {
            getConnectionString = @"Data Source = 210.22.99.130,9010\SQL2008R2;Initial Catalog = CRMTREE;User Id = sa;Password = aA1";
            //getConnectionString = @"Data Source = 10.254.200.66,1433\SQL2008R2;Initial Catalog = CRMTREE;User Id = sa;Password = baoxin@123.com";
            //getConnectionString = @"Data Source=crmtree123.sqlserver.rds.aliyuncs.com,3433 ; User ID=crmtree_dbo; Password=crmtree_dbo_prod; Initial Catalog=CRMTREE;Timeout=60000";
            SqlConnection conn = new SqlConnection(getConnectionString);
            return conn;
        }
        #endregion

        #region public static SqlDataReader RunSqlDataReader(string strSql)
        /// <summary>
        /// </summary>
        /// <param name="strSql">strSql</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunSqlDataReader(string strSql)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            objConn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        #endregion

        #region public static SqlDataReader RunSqlDataReader(string strSql, ArrayList parametersList)
        /// <summary>
        /// exec sql with parameter
        /// </summary>
        /// <param name="strSql">strSql</param>
        /// <param name="parameters">parameters</param>
        /// <returns> DataReader</returns>
        public static SqlDataReader RunSqlDataReader(string strSql, ArrayList parametersList)
        {
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            objConn.Open();
            if (parametersList != null && parametersList.Count > 0)
            {
                FillParameters(cmd.Parameters, parametersList);
            }
            dr = cmd.ExecuteReader();
            return dr;
        }

        #endregion

        #region public static int RunSql(string strSql,ArrayList parametersList)
        /// <summary>
        /// </summary>
        /// <param name="strSql">Sql</param>
        /// <param name="parameters">pararmeters []</param>
        /// <returns>int</returns>
        public static int RunSql(string strSql, ArrayList parametersList)
        {
            int n = -1;
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            objConn.Open();
            try
            {
                if (parametersList != null && parametersList.Count > 0)
                {
                    FillParameters(cmd.Parameters, parametersList);
                }
                n = (int)cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                objConn.Close();
            }
            return n;
        }
        #endregion

        #region public static string GetRegardingField(string strSql,DataRow dr,string parameter)
        /// <summary>
        /// ExcuteScalar
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>int</returns>
        public static string GetRegardingField(string strSql, DataRow dr, string parameter)
        {
            try
            {
                ArrayList altemp = new ArrayList();

                int paraNo = Regex.Matches(strSql, @"\?").Count;

                for (int i = 0; i < paraNo; i++)
                    altemp.Add(dr[parameter.Split('|')[i]].ToString());

                if (ExecuteQuery(strSql, altemp) == null)
                    return "";
                return ExecuteQuery(strSql, altemp).AsEnumerable().FirstOrDefault()[0].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region public static int RunSqlExcuteScalar(string strSql)
        /// <summary>
        /// ExcuteScalar
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>int</returns>
        public static int RunSqlExcuteScalar(string strSql)
        {
            int n = -1;
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            objConn.Open();
            try
            {
                Object m = cmd.ExecuteScalar();
                if (m != null)
                    n = Convert.ToInt32(m);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                objConn.Close();
            }
            return n;
        }
        #endregion

        #region public static int RunSqlExcuteScalar(string strSql)
        /// <summary>
        /// ExcuteScalar
        /// </summary>
        /// <param name="strSql">string</param>
        /// <returns>int</returns>
        public static bool RunSqlExecution(string strSql)
        {
            bool flag = true;
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            objConn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                flag = false;
            }
            finally
            {
                objConn.Close();
            }
            return flag;
        }
        #endregion

        #region public static int RunSqlExecuteScalar(string strSql, ArrayList parametersList)
        public static int RunSqlExecuteScalar(string strSql, ArrayList parametersList)
        {
            int n = -1;
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            objConn.Open();
            try
            {
                if (parametersList != null && parametersList.Count > 0)
                {
                    FillParameters(cmd.Parameters, parametersList);
                }
                n = (int)cmd.ExecuteScalar();
            }
            catch { }
            finally
            {
                objConn.Close();
            }
            return n;
        }
        #endregion

        #region public static string SaveReturnIndentityKey(string strSql)
        public static string SaveReturnIndentityKey(string strSql)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;

            SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
            sqlAd.Fill(ds);
            dt = ds.Tables[0];

            if (dt == null || dt.Rows.Count == 0)
                return "";
            objConn.Close();
            sqlAd.Dispose();
            return dt.Rows[0][0].ToString();

        }

        #endregion

        #region public static string SaveReturnIndentityKey(string strSql,string tableName)
        public static string SaveReturnIndentityKey(string strSql, string tableName)
        {
            if (RunSqlExecution(strSql))
            {
                string strSqlDefault = "Select IDENT_CURRENT(?)";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                SqlConnection objConn = GetADOConnection();
                cmd.Connection = objConn;
                cmd.CommandText = GetParameterizeSql(strSqlDefault);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 900;
                ArrayList parametersList = new ArrayList(); parametersList.Add(tableName);
                if (parametersList != null && parametersList.Count > 0)
                {
                    FillParameters(cmd.Parameters, parametersList);
                }
                SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
                sqlAd.Fill(ds);
                dt = ds.Tables[0];
                objConn.Close();
                sqlAd.Dispose();
                return dt.Rows[0][0].ToString();
            }
            else
                return string.Empty;
        }

        #endregion

        #region public static string SaveReturnIndentityKey(string strSql,string tableName)
        public static string SaveReturnIndentityKey(string strSql, ArrayList parametersList)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;

            if (parametersList != null && parametersList.Count > 0)
            {
                FillParameters(cmd.Parameters, parametersList);
            }
            SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
            sqlAd.Fill(ds);
            dt = ds.Tables[0];
            objConn.Close();
            sqlAd.Dispose();
            return dt.Rows[0][0].ToString();

        }

        #endregion


        public static string SaveReturnIndentityKey(string strSql, ArrayList parametersList,bool procedureOrNot)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql.Split(';')[1]);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 900;
           
            ArrayList alList=new ArrayList();
            ArrayList alParaNew = new ArrayList();

            string strParaList = strSql.Split(';')[2].ToString();

            if(!string.IsNullOrEmpty(strParaList))
            {
                string [] paraList=strParaList.Split(',');

                int calc=0;

                foreach(string s in paraList)
                {
                    alList.Add(s.Split('|')[0]);

                    if (!string.IsNullOrEmpty(s.Split('|')[1]) && s.Split('|')[1].Contains('?'))
                    {
                        alParaNew.Add(parametersList[calc]);
                        calc++;
                    }
                    else
                    {
                        alParaNew.Add(s.Split('|')[1]);
                    }
                }

            }


            if (alParaNew != null && alParaNew.Count > 0)
            {
                FillParameters(cmd.Parameters, alParaNew, alList);
            }
            SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
            sqlAd.Fill(ds);
            dt = ds.Tables[0];
            objConn.Close();
            sqlAd.Dispose();
            return dt.Rows[0][0].ToString();

        }


        public static void TestingGetProcedure()
        {
            //string procedure = "";


             
            //conn.Open();
            SqlCommand cmd = new SqlCommand("Add_Dealer_Empl_Process", GetADOConnection());

            cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Input;
            //param.ParameterName = GetParameterName(i);

            cmd.Parameters.Add("@AD_Code", SqlDbType.Int).Value = 125;
            cmd.Parameters.Add("@UA_DMS_Code", SqlDbType.VarChar).Value = "AutoGenerated";
            cmd.Parameters.Add("@AU_Name", SqlDbType.VarChar).Value = "卢顺杨";
            cmd.Parameters.Add("@AU_B_date", SqlDbType.DateTime).Value = "2000/01/01";
            cmd.Parameters.Add("@AU_UG_Code", SqlDbType.Int).Value = 21;
            cmd.Parameters.Add("@DE_ID", SqlDbType.VarChar).Value = "AutoGenerated";

            //cmd.Parameters["@isinsert"].Direction = ParameterDirection.Output;    //设置参数的输出类型，和存储过程中参数匹配
            //cmd.Parameters["@isupdate"].Direction = ParameterDirection.Output;
            //cmd.Parameters["@datetime"].Direction = ParameterDirection.Output;
            //cmd.Parameters["return_number"].Direction = ParameterDirection.ReturnValue; //有return返回值向参数列表添加

            //cmd.ExecuteNonQuery();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            sda.Fill(ds);

            // dataGridView1.DataSource = ds.Tables[0];

        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // 预处理
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            //PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // 创建SqlDataAdapter和DataSet.
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // 填充DataSet.
                da.Fill(ds);

                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();
                return ds;
            }
        }

        #region public static string SaveReturnIndentityKey(string MR_Fields_Insert_Query, string MR_Fields_Insert_Parameter, string MR_Fields_Select_Query, string MR_Fields_Select_Parameter)
        public static string SaveReturnIndentityKey(string MR_Fields_Insert_Query, string MR_Fields_Insert_Parameter, string MR_Fields_Select_Query, string MR_Fields_Select_Parameter,DataRow dr)
        {
            if (string.IsNullOrEmpty(MR_Fields_Insert_Query) && string.IsNullOrEmpty(MR_Fields_Insert_Parameter))
            {
                ArrayList parametersList_Select = new ArrayList(MR_Fields_Select_Parameter.Split('|'));
                ArrayList paras = new ArrayList();
                foreach (string para in parametersList_Select)
                {
                    paras.Add(dr[para].ToString());
                }

                if (MR_Fields_Select_Query.Contains("Exec"))
                    return SaveReturnIndentityKey(MR_Fields_Select_Query, paras,true);
                else
                    return SaveReturnIndentityKey(MR_Fields_Select_Query, paras);
            }
            else
            {
                ArrayList parametersList_Insert = new ArrayList(MR_Fields_Insert_Parameter.Split('|'));

                ArrayList parasIn = new ArrayList();

                foreach (string para in parametersList_Insert)
                {
                    parasIn.Add(dr[para].ToString());
                }

                ArrayList parametersList_Select = new ArrayList(MR_Fields_Select_Parameter.Split('|'));

                ArrayList parasSelect = new ArrayList();

                foreach (string para in parametersList_Select)
                {
                    parasSelect.Add(dr[para].ToString());
                }

                if (RunSql(MR_Fields_Insert_Query, parasIn) > 0)
                {
                    if (MR_Fields_Select_Query.ToUpper().Contains("EXEC"))
                        return SaveReturnIndentityKey(MR_Fields_Select_Query, parasSelect, true);
                    else
                        return SaveReturnIndentityKey(MR_Fields_Select_Query, parasSelect);
                }
                else
                    return string.Empty;
            }
        }

        #endregion

        #region public static string SaveReturnIndentityKey(string strSql,string strSqlTemplate,string tableName)
        public static string SaveReturnIndentityKey(string strSql, string strSqlTemplate, string tableName)
        {
            try
            {
                if (RunSqlExecution(strSql))
                {
                    //string strSqlDefault = "Select IDENT_CURRENT(?)";
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand();
                    SqlConnection objConn = GetADOConnection();
                    cmd.Connection = objConn;
                    cmd.CommandText = GetParameterizeSql(strSqlTemplate);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 900;
                    //ArrayList parametersList = new ArrayList(); parametersList.Add(tableName);
                    //if (parametersList != null && parametersList.Count > 0)
                    //{
                    //    FillParameters(cmd.Parameters, parametersList);
                    //}
                    SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
                    sqlAd.Fill(ds);
                    dt = ds.Tables[0];
                    objConn.Close();
                    sqlAd.Dispose();
                    return dt.Rows[0][0].ToString();
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }

        #endregion

        #region public static List<string> ExecuteTranQuery(string strSql,string tableName,string colName)
        public static int ExecuteQuery(List<string> queryList)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            SqlConnection conn = GetADOConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            int calc = 0;
            try
            {
                foreach (string query in queryList)
                {
                    if (string.IsNullOrEmpty(query))
                        continue;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    calc++;
                }
                tran.Commit();
                return calc;
            }
            catch
            {

                tran.Rollback();
                return calc;
            }

        }
        #endregion

        #region public static List<string> ExecuteQuery(string strSql,string tableName,string colName)
        public static List<string> ExecuteQuery(string strSql, string tableName, string colName)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;

            ArrayList parametersList = new ArrayList();
            parametersList.Add(tableName);
            if (parametersList != null && parametersList.Count > 0)
            {
                FillParameters(cmd.Parameters, parametersList);
            }
            SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
            sqlAd.Fill(ds);
            dt = ds.Tables[0];
            objConn.Close();

            List<string> lsTemp = new List<string>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[colName] != null)
                    lsTemp.Add(dr[colName].ToString());
            }

            return lsTemp;
        }
        #endregion

        public static bool ExecuteQuery(DataTable dt, string tableName)
        {
            StringBuilder sbTemp = new StringBuilder();
            StringBuilder sbTempInsertQuery = new StringBuilder();
            string endIcon = string.Empty;
            sbTempInsertQuery.Append("insert into " + tableName + "(");
            bool flag = true;

            List<string> ls = CommonHelper.instance.GetListFromTable(dt);

            int calc = 0;
            foreach (string temp in ls)
            {
                calc++;
                endIcon = (calc == ls.Count ? "" : ",");
                sbTempInsertQuery.Append(temp + endIcon);
            }
            calc = 0;

            int dataSum = 0;
            foreach (DataRow dr in dt.AsEnumerable())
            {
                dataSum++;
                if (dataSum >= 5000)
                {
                    dataSum = 0;
                    flag = RunSqlExecution(sbTemp.Append(")").ToString());

                    if (!flag)
                        return false;
                    sbTemp = new StringBuilder();
                    sbTemp.Append(sbTempInsertQuery.ToString() + ") values (");
                    foreach (string temp in ls)
                    {
                        calc++;
                        endIcon = (calc == ls.Count ? "" : ",");
                        sbTemp.Append("\'" + dr[temp] + "\'" + endIcon);
                    }
                    sbTemp.Append(")");
                    calc = 0;
                }
                else
                {
                    sbTemp.Append(sbTempInsertQuery.ToString() + ") values (");
                    foreach (string temp in ls)
                    {
                        calc++;
                        endIcon = (calc == ls.Count ? "" : ",");
                        sbTemp.Append("\'" + dr[temp] + "\'" + endIcon);
                    }
                    sbTemp.Append(")");
                    calc = 0;
                }

            }
            return RunSqlExecution(sbTemp.ToString());

        }

        #region public static DataTable ExecuteQuery(string strSql)
        public static DataTable ExecuteQuery(string strSql)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
            sqlAd.Fill(ds);
            dt = ds.Tables[0];
            objConn.Close();
            return dt;
        }
        #endregion

        public static DataTable getFieldAttribute(string tableName)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from " + tableName + " where 1=0 ", GetADOConnection());
            DataSet ds = new DataSet();
            da.FillSchema(ds, SchemaType.Source);

            DataTable dt;
            dt = ds.Tables[0].Clone();

            return dt;
        }

        #region public static DataTable ExecuteQuery(string strSql, ArrayList parametersList)
        public static DataTable ExecuteQuery(string strSql, ArrayList parametersList)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            if (parametersList != null && parametersList.Count > 0)
            {
                FillParameters(cmd.Parameters, parametersList);
            }
            SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
            sqlAd.Fill(ds);
            dt = ds.Tables[0];
            objConn.Close();
            sqlAd.Dispose();
            return dt;
        }
        #endregion

        #region public static DataSet RunSql(string strSql, string tableName)
        /// <summary>
        /// </summary>
        /// <param name="strSql">string</param>
        /// <param name="tableName">string</param>
        /// <returns>DataSet</returns>
        public static DataSet RunSql(string strSql, string tableName)
        {
            SqlConnection objConn = GetADOConnection();
            DataSet ds = new DataSet();
            objConn.Open();
            SqlCommand cmd = new SqlCommand(strSql, objConn);
            SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
            sqlAd.Fill(ds, tableName);
            objConn.Close();
            return ds;
        }
        #endregion

        #region public static DataSet RunSql(string strSql, string tableName,  ArrayList parametersList)
        public static DataSet RunSql(string strSql, string tableName, ArrayList parametersList)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            if (parametersList != null && parametersList.Count > 0)
            {
                FillParameters(cmd.Parameters, parametersList);
            }
            SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
            sqlAd.Fill(ds, tableName);
            objConn.Close();
            sqlAd.Dispose();
            return ds;
        }
        #endregion

        #region  public static DataSet RunSql(string strSql, string tableName, int pageSize, int currentPageIndex)
        /// <summary>
        /// Select page
        /// </summary>
        /// <param name="strSql">string</param>
        /// <param name="tableName">string</param>
        /// <param name="pageSize">int</param>
        /// <param name="currentPageIndex">int</param>
        /// <returns>DataSet</returns>
        public static DataSet RunSql(string strSql, string tableName, int pageSize, int currentPageIndex)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            objConn.Open();
            SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
            sqlAD.Fill(ds, pageSize * (currentPageIndex - 1), pageSize, tableName);
            objConn.Close();
            return ds;
        }
        #endregion

        #region  public static DataSet RunSql(string strSql, string tableName,  ArrayList parametersList, int pageSize, int currentPageIndex)
        public static DataSet RunSql(string strSql, string tableName, ArrayList parametersList, int pageSize, int currentPageIndex)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlConnection objConn = GetADOConnection();
            cmd.Connection = objConn;
            cmd.CommandText = GetParameterizeSql(strSql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 900;
            if (parametersList != null && parametersList.Count > 0)
            {
                FillParameters(cmd.Parameters, parametersList);
            }
            SqlDataAdapter sqlAd = new SqlDataAdapter(cmd);
            sqlAd.Fill(ds, pageSize * (currentPageIndex - 1), pageSize, tableName);
            objConn.Close();
            sqlAd.Dispose();
            return ds;
        }
        #endregion

        #region   private static void FillParameters(IDataParameterCollection collection, ArrayList valueList)
        /// <summary>
        /// fill parameters to query
        /// </summary>
        /// <param name="collection">parameters list</param>
        /// <param name="sourceParameters">parameters value list</param>
        private static void FillParameters(IDataParameterCollection collection, ArrayList valueList)
        {
            if (null != valueList && 0 != valueList.Count)
            {
                for (int i = 0; i < valueList.Count; i++)
                {
                    SqlParameter param = new SqlParameter();
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = GetParameterName(i);
                    param.Value = valueList[i];
                    collection.Add(param);
                }
            }
        }
        #endregion

        #region   private static void FillParameters(IDataParameterCollection collection, ArrayList valueList,ArrayList ParameterList)
        /// <summary>
        /// fill parameters to query
        /// </summary>
        /// <param name="collection">parameters list</param>
        /// <param name="sourceParameters">parameters value list</param>
        private static void FillParameters(IDataParameterCollection collection, ArrayList valueList,ArrayList parameterList)
        {
            if (null != valueList && 0 != valueList.Count)
            {
                for (int i = 0; i < valueList.Count; i++)
                {
                    SqlParameter param = new SqlParameter();
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = parameterList[i].ToString();
                    param.Value = valueList[i];
                    collection.Add(param);
                }
            }
        }
        #endregion

        #region private static string GetParameterizeSql(string sourceSql)
        /// <summary>
        /// get parameterize sql
        /// </summary>
        /// <param name="sourceSql">no have parameterize sql</param>
        /// <returns>parameterize sql</returns>
        private static string GetParameterizeSql(string sourceSql)
        {
            int pos = sourceSql.IndexOf('?', 0);
            int index = 0;
            while (pos > -1)
            {
                sourceSql = sourceSql.Substring(0, pos) + GetParameterName(index) + sourceSql.Substring(pos + 1);
                pos = sourceSql.IndexOf('?', 0);
                index++;
            }

            return sourceSql;
        }
        #endregion

        #region private static string GetParameterName(int index)
        /// <summary>
        /// get parameter name
        /// </summary>
        /// <param name="index">parameter index </param>
        /// <returns>the parameter name of support sql server database</returns>
        private static string GetParameterName(int index)
        {
            return "@p" + index.ToString();
        }
        #endregion

    }
}
