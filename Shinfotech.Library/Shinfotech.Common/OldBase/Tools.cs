using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections;
using System.Xml;
using System.Xml.Xsl;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ShInfoTech.Common
{
    public partial class Tools
    {
        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConnString()
        {
            return DecryptSqlPwd("CRMTREEConnectionString");
        }

        public static string DecryptSqlPwd(string connectionStr)
        {
            string SqlStr = WebConfig.GetConnectionString(connectionStr);

            string[] str = SqlStr.Split(';');

            StringBuilder sb = new StringBuilder();

            string ss = string.Empty;

            foreach (string s in str)
            {
                if (s.Contains("User ID="))
                {
                    ss = s.Replace("User ID=", "");

                    ss = Shinfotech.Tools.Encryptions.DecryptDES(ss, "Shunovo20150701");

                    sb.Append("User ID=" + ss + ";");
                }
                else if (s.Contains("Password="))
                {
                    ss = s.Replace("Password=", "");

                    ss = Shinfotech.Tools.Encryptions.DecryptDES(ss, "Shunovo20150701");

                    sb.Append("Password=" + ss + ";");
                }
                else if (s.Contains("uid="))
                {
                    ss = s.Replace("uid=", "");

                    ss = Shinfotech.Tools.Encryptions.DecryptDES(ss, "Shunovo20150701");

                    sb.Append("uid=" + ss + ";");
                }
                else if (s.Contains("pwd="))
                {
                    ss = s.Replace("pwd=", "");

                    ss = Shinfotech.Tools.Encryptions.DecryptDES(ss, "Shunovo20150701");

                    sb.Append("pwd=" + ss + ";");
                }
                else if(string.IsNullOrEmpty(s))
                {
                    continue;
                }
                else
                {
                    sb.Append(s + ";");
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// 返回加密后的字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string ServerUrlEncode(string s)
        {
            return HttpContext.Current.Server.UrlEncode(s);
        }

        /// <summary>
        /// 输出XML
        /// </summary>
        /// <param name="strXml">XML字符串</param>
        public static void OutputXml(string strXml)
        {
            OutputXml(strXml, true);
        }
        /// <summary>
        /// 输出XML
        /// </summary>
        /// <param name="strXml">XML字符串</param>
        /// <param name="isDebug">true,false</param>
        public static void OutputXml(string strXml, bool isDebug)
        {
            if (isDebug)
            {
                if (Request.Int("debug") == 1)
                {
                    System.Web.HttpContext.Current.Response.ContentType = "text/xml";
                    System.Web.HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + strXml);
                    System.Web.HttpContext.Current.Response.End();
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.ContentType = "text/xml";
                System.Web.HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + strXml);
                System.Web.HttpContext.Current.Response.End();
            }
        }

        #region XSL/XML转换
        /// <summary>
        /// 返回转换后的HTML代码
        /// </summary>
        /// <param name="strXml">XML代码</param>
        /// <param name="skinfile">XSLT</param>
        /// <returns>返回转换后的HTML代码</returns>
        public static string Transform(string strXml, string skinfile)
        {
            return Transform(strXml, skinfile, false);
        }


        public static string Transform(string strXml, string skinfile, bool enableScript)
        {
            string re = "";
            if (Request.Int("debug") == 1)
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strXml);
                re = xml.DocumentElement.OuterXml;
            }
            else
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                if (enableScript)
                {
                    XsltSettings xsltsetting = new XsltSettings();
                    xsltsetting.EnableScript = true;
                    xslt.Load(skinfile, xsltsetting, new XmlUrlResolver());
                }
                else
                {
                    xslt.Load(skinfile);
                }

                StringBuilder sb = new StringBuilder();
                XmlWriterSettings mysetting = new XmlWriterSettings();
                mysetting.Indent = true;
                mysetting.IndentChars = "	";
                mysetting.ConformanceLevel = ConformanceLevel.Auto;
                XmlWriter writer = XmlWriter.Create(sb, mysetting);
                XsltArgumentList argsList = new XsltArgumentList();

                Tools tools = new Tools();
                argsList.AddExtensionObject("urn:Tools", tools);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strXml);
                xslt.Transform(xml, argsList, writer);
                re = sb.ToString();

                //特殊字符处理(Start)
                if (re.StartsWith("<?xml version=\"1.0\" encoding=\"utf-16\"?>"))
                {
                    re = re.Substring(39);
                }
                re = re.Replace(" xmlns:Tools=\"urn:Tools\">", ">");
                re = re.Replace(" xmlns:Tools=\"urn:Tools\" />", " />");
                if (re.StartsWith("<html xmlns=\"http://www.w3.org/1999/xhtml\">"))
                {
                    re = "<!doctype html public \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n" + re;
                }
                //特殊字符处理(End)
            }
            return re;
        }

        #endregion

        #region Struct body,result,StrCut_HTML_Tag

        [StructLayout(LayoutKind.Sequential)]
        public struct body
        {
            public DataTable dt;
            public int count;
        }

        public struct result
        {
            public bool isSucceed;
            public string msg;
            public int id;
        }
        public struct StrCut_HTML_Tag
        {
            public int status;
            public string name;
            public string isInAttribute;
            public StrCut_HTML_Tag(int _status, string _name, string _isInAttribute)
            {
                status = _status;
                name = _name;
                isInAttribute = _isInAttribute;
            }
        }

        public struct upload_result
        {
            public bool result;
            public string msg;
            public string url;
            public string smurl;
            public string time;
        }
        #endregion

        #region 插入XML节点或者XML 字符串

        /// <summary>
        /// 在指定的XML文档中，在指定的XMLPATH，插入XML元素
        /// </summary>
        /// <param name="xml">XML文档</param>
        /// <param name="xmlpath">xmlpath</param>
        /// <param name="elem">XML元素</param>
        public static void Insert(XmlDocument xml, string xmlpath, XmlElement elem)
        {
            XmlElement tmp = xml.CreateElement("Tmp");
            tmp.InnerXml = elem.OuterXml;
            XmlNode node = xml.SelectSingleNode(xmlpath);
            while (tmp.ChildNodes.Count > 0)
            {
                node.AppendChild(tmp.FirstChild);
            }
        }
        /// <summary>
        /// 在指定的XML文档中，在指定的XMLPATH，插入表示XML的字符串
        /// </summary>
        /// <param name="xml">XML文档</param>
        /// <param name="xmlpath">xmlpath</param>
        /// <param name="innerxml">XML的字符串</param>
        public static void Insert(XmlDocument xml, string xmlpath, string innerxml)
        {
            XmlElement tmp = xml.CreateElement("Tmp");
            tmp.InnerXml = innerxml;
            XmlNode node = xml.SelectSingleNode(xmlpath);
            while (tmp.ChildNodes.Count > 0)
            {
                node.AppendChild(tmp.FirstChild);
            }
        }

        public static void InsertBefore(XmlDocument xml, string xmlpath, XmlElement elem)
        {
            XmlElement tmp = xml.CreateElement("Tmp");
            tmp.InnerXml = elem.OuterXml;
            XmlNode node = xml.SelectSingleNode(xmlpath);
            while (tmp.ChildNodes.Count > 0)
            {
                node.InsertBefore(tmp.LastChild, xml.SelectSingleNode(xmlpath).FirstChild);
            }
        }
        public static void InsertBefore(XmlDocument xml, string xmlpath, string innerxml)
        {
            XmlElement tmp = xml.CreateElement("Tmp");
            tmp.InnerXml = innerxml;
            XmlNode node = xml.SelectSingleNode(xmlpath);
            while (tmp.ChildNodes.Count > 0)
            {
                node.InsertBefore(tmp.LastChild, xml.SelectSingleNode(xmlpath).FirstChild);
            }
        }

        #endregion

        #region Table转换成XML

        public static string TableToXML(DataTable dt)
        {
            return TableToXML(dt.TableName, dt);
        }
        public static string TableToXML(string DataSetName, DataTable dt)
        {
            return TableToXML(DataSetName, "item", dt);
        }
        public static string TableToXML(string DataSetName, string LoopName, DataTable dt)
        {
            dt.DataSet.DataSetName = DataSetName;
            dt.DataSet.Tables[dt.DataSet.Tables.IndexOf(dt)].TableName = LoopName;
            return dt.DataSet.GetXml();
        }

        #endregion

        /// <summary>
        /// struct body to XML
        /// </summary>
        /// <param name="re">struct body</param>
        /// <param name="page">page</param>
        /// <param name="pagesize">pagesize</param>
        /// <returns>xml</returns>
        public static string bodyToXML(body re, int page, int pagesize)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(re.dt.DataSet.GetXml());
            XmlElement p = xml.CreateElement("Page");
            p.InnerXml = page.ToString();
            xml.FirstChild.AppendChild((XmlNode)p);
            XmlElement ps = xml.CreateElement("PageSize");
            ps.InnerXml = pagesize.ToString();
            xml.FirstChild.AppendChild((XmlNode)ps);
            XmlElement c = xml.CreateElement("Count");
            c.InnerXml = re.count.ToString();
            xml.FirstChild.AppendChild((XmlNode)c);
            return xml.DocumentElement.OuterXml;
        }
        /// <summary>
        /// struct result to xml
        /// </summary>
        /// <param name="re">struct result</param>
        /// <returns>xml string</returns>
        public static string resultToXML(result re)
        {
            return "<Result>"
                        + "<isSucceed>" + re.isSucceed.ToString() + "</isSucceed>"
                        + "<Msg>" + re.msg + "</Msg>"
                        + "<ID>" + re.id.ToString() + "</ID>"
                    + "</Result>";
        }


        #region 分页函数

        /// <summary>
        /// 地址转向分页函数
        /// </summary>
        /// <param name="pageNo">当前页数</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageHrefPath">连接路径</param>
        /// <param name="pageSplit">分页分割符ex:p</param>
        /// <param name="pageHrefEnd"></param>
        /// <param name="isTarget">是否开新窗</param>
        /// <param name="strStyle">分页样式</param>
        /// <param name="strCurrentPageStyle">当前页样式</param>
        /// <param name="strInputStyle">输入框样式</param>
        /// <param name="strButtonStyle">按钮样式</param>
        /// <param name="listNum">显示数字按钮数</param>
        /// <param name="isAutoDisplay"></param>
        /// <param name="isFirstAndLast">是否显示首页跟尾页</param>
        /// <param name="isPreviousAndNext">是否显示上一页跟下一页</param>
        /// <param name="isInput">跳转输入框</param>
        /// <param name="isButton">跳转按钮</param>
        /// <param name="showInputNum">显示跳转输入框总页数大于多少</param>
        /// <returns>当前页HTML代码</returns>
        public static string Page(int pageNo, int pageSize, int recordCount, string pageHrefPath, string pageSplit,
            string pageHrefEnd, bool isTarget, string strClass, string strStyle, string strCurrentPageClass,
            string strCurrentPageStyle, string strInputClass, string strInputStyle, string strButtonClass, string strButtonStyle,
            int listNum, bool isAutoDisplay, bool isFirstAndLast, bool isPreviousAndNext, bool isShowNumericButton, bool isInput,
            bool isButton, bool isDropdown, bool isRewrite, string pageParms, int showInputNum
            )
        {
            Paging paging = new Paging();

            paging.PageHrefPath = pageHrefPath;
            paging.PageHrefEnd = pageHrefEnd;

            if ("" != pageSplit)
            {
                paging.PageSplit = pageSplit;
            }
            paging.IsTarget = isTarget;

            paging.StrClass = strClass;
            paging.StrStyle = strStyle;

            paging.StrCurrentPageClass = strCurrentPageClass;
            if ("" != strCurrentPageStyle)
            {
                paging.StrCurrentPageStyle = strCurrentPageStyle;
            }

            paging.StrInputClass = strInputClass;
            if ("" != strInputStyle)
            {
                paging.StrInputStyle = strInputStyle;
            }

            paging.StrButtonClass = strButtonClass;
            paging.StrButtonStyle = strButtonStyle;

            paging.ListNum = listNum;

            paging.IsAutoDisplay = isAutoDisplay;
            paging.IsFirstAndLast = isFirstAndLast;
            paging.IsPreviousAndNext = isPreviousAndNext;
            paging.IsInput = isInput;
            paging.IsButton = isButton;
            paging.IsDropdown = isDropdown;
            paging.IsRewrite = isRewrite;
            paging.IsShowNumericButton = isShowNumericButton;


            if (isRewrite == false)
            {
                paging.PagePath = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath;
                paging.PageParms = pageParms;
            }

            paging.ShowInputNum = showInputNum;

            if (pageNo > 0)
            {
                paging.CurrentPage = pageNo;
            }
            if (pageSize > 0)
            {
                paging.PageSize = pageSize;
            }
            if (recordCount >= 0)
            {
                paging.RecordCount = recordCount;
            }

            return paging.OutPut();



        }
        #endregion

        #region CommonSQL
        /// <summary>
        /// 执行SQL语句返回DataSet
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="conn">Conn连接对象</param>
        /// <returns>DS</returns>
        public static DataSet CommonSQL(string strSQL, SqlConnection conn)
        {
            DataSet DS = new DataSet();
            bool flag = false;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                flag = true;
            }
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(DS, "DS");
            adapter.Dispose();
            cmd.Dispose();
            if (flag)
            {
                conn.Close();
            }
            conn.Dispose();
            return DS;
        }
        /// <summary>
        /// 执行SQL语句返回DataSet
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="strConn">Conn连接字符串</param>
        /// <returns>DS</returns>
        public static DataSet CommonSQL(string strSQL, string strConn)
        {
            DataSet DS = new DataSet();
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(DS, "DS");
            conn.Close();
            adapter.Dispose();
            cmd.Dispose();
            conn.Dispose();
            return DS;
        }
        /// <summary>
        /// 执行SQL语句是否成功
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="strConn">数据库连接串</param>
        /// <returns>true,false</returns>
        public static bool ExeSQL(string strSQL, string strConn)
        {
            bool re = true;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            SqlTransaction trans = conn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
                int result = cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch 
            {
                trans.Rollback();
                re = false;
            }
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return re;
        }

        /// <summary>
        /// 执行SQL语句是否成功
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="conn">数据库连接对象</param>
        /// <returns>true,false</returns>
        public static bool ExeSQL(string strSQL, SqlConnection conn)
        {
            bool re = true;
            bool orgIsClosed = false;
            if (conn.State == ConnectionState.Closed)
            {
                orgIsClosed = true;
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            SqlTransaction trans = conn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
                int result = cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch 
            {
                trans.Rollback();
                re = false;
            }
            cmd.Dispose();
            if (orgIsClosed)
            {
                conn.Close();
            }
            return re;
        }

        /// <summary>
        /// 通过事务执行多条SQL语句，支持SqlParameter传参
        /// </summary>
        /// <param name="sqlList">List类型的SQL语句集合</param>
        /// <param name="paraList">SqlParameter[]类型的参数集合</param>
        /// <param name="connectionString">连接串</param>
        /// <returns>true or false</returns>
        public static bool ExecuteNonQueryTrans(List<string> sqlList, List<SqlParameter[]> paraList, string connectionString)
        {
            bool re = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlTransaction trans = null;
                cmd.Connection = conn;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    cmd.Transaction = trans;

                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        cmd.CommandText = sqlList[i];
                        if (paraList != null && paraList[i] != null)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(paraList[i]);
                        }
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    re = true;
                }
                catch 
                {
                    try
                    {
                        trans.Rollback();
                        re = false;
                    }
                    catch
                    {

                    }
                    //throw e;
                }

            }
            return re;
        }
        public static bool ExecuteNonQueryTrans(List<string> sqlList, string connectionString)
        {
            return ExecuteNonQueryTrans(sqlList, null, connectionString);
        }


        /// <summary>
        /// 通过事务执行多条SQL语句，支持SqlParameter传参
        /// </summary>
        /// <param name="sqlList">List类型的SQL语句集合</param>
        /// <param name="paraList">SqlParameter[]类型的参数集合</param>
        /// <param name="connectionString">连接串</param>
        /// <returns>true or false</returns>
        public static int ExecuteScalarTrans(List<string> sqlList, List<SqlParameter[]> paraList, string connectionString)
        {
            int re = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlTransaction trans = null;
                cmd.Connection = conn;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    cmd.Transaction = trans;

                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        cmd.CommandText = sqlList[i];
                        if (paraList != null && paraList[i] != null)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(paraList[i]);
                        }
                        if (i == sqlList.Count - 1)
                        {
                            re = Security.ToNum(cmd.ExecuteScalar());
                        }
                        else
                        {
                            cmd.ExecuteNonQuery();
                        }

                    }
                    trans.Commit();

                }
                catch 
                {
                    try
                    {
                        trans.Rollback();
                        re = 0;
                    }
                    catch
                    {

                    }
                    //throw e;
                }

            }
            return re;
        }
        public static int ExecuteScalarTrans(List<string> sqlList, string connectionString)
        {
            return ExecuteScalarTrans(sqlList, null, connectionString);
        }


        /// <summary>
        /// 返回当页记录集(DataTable)跟记录总数
        /// </summary>
        /// <param name="strSQL">SQL语句 或表名</param>
        /// <param name="strOrder">排序</param>
        /// <param name="page">当前页数</param>
        /// <param name="pagesize">每页显示记录数</param>
        /// <param name="conn">conn连接对象</param>
        /// <returns>DS</returns>
        public static body CommonSQL(string strSQL, string strOrder, int page, int pagesize, string field, SqlConnection conn)
        {
            if ("" == field)
            {
                field = "*";
            }
            Tools.body body = new Tools.body();
            DataSet DS = new DataSet();
            SqlCommand cmd = new SqlCommand("sp_Get_Current_Page_Record", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = cmd.Parameters.Add("@PageCurr", SqlDbType.Int);
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = page;

            SqlParameter parameter2 = cmd.Parameters.Add("@PageSize", SqlDbType.Int);
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = pagesize;

            SqlParameter parameter3 = cmd.Parameters.Add("@Field", SqlDbType.Int);
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = field;

            SqlParameter parameter4 = cmd.Parameters.Add("@Table", SqlDbType.NVarChar);
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = strSQL;

            SqlParameter parameter5 = cmd.Parameters.Add("@Condition", SqlDbType.NVarChar);
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = "";

            SqlParameter parameter6 = cmd.Parameters.Add("@Order", SqlDbType.NVarChar);
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = strOrder;

            SqlParameter parameter7 = cmd.Parameters.Add("@ReCordCount", SqlDbType.BigInt);
            parameter7.Direction = ParameterDirection.Output;

            bool flag = false;
            if (conn.State == ConnectionState.Closed)
            {
                flag = true;
                conn.Open();
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(DS);
            adapter.Dispose();
            cmd.Dispose();
            if (flag)
            {
                conn.Close();
            }
            body.dt = DS.Tables[0];
            body.count = Convert.ToInt32(parameter7.Value.ToString());
            DS.Dispose();
            return body;

        }

        /// <summary>
        /// 返回当页记录集(DataTable)跟记录总数
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="strOrder">排序</param>
        /// <param name="page">当前页数</param>
        /// <param name="pagesize">每页显示记录数</param>
        /// <param name="strConn">conn连接字符串</param>
        /// <returns>DS</returns>
        public static body CommonSQL(string strSQL, string strOrder, int page, int pagesize, string field, string strConn)
        {
            if ("" == field)
            {
                field = "*";
            }
            Tools.body body = new Tools.body();
            DataSet DS = new DataSet();
            SqlConnection conn = new SqlConnection(strConn);

            SqlCommand cmd = new SqlCommand("sp_Get_Current_Page_Record", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = cmd.Parameters.Add("@PageCurr", SqlDbType.Int);
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = page;

            SqlParameter parameter2 = cmd.Parameters.Add("@PageSize", SqlDbType.Int);
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = pagesize;

            SqlParameter parameter3 = cmd.Parameters.Add("@Field", SqlDbType.Int);
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = field;

            SqlParameter parameter4 = cmd.Parameters.Add("@Table", SqlDbType.NVarChar);
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = strSQL;

            SqlParameter parameter5 = cmd.Parameters.Add("@Condition", SqlDbType.NVarChar);
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = "";

            SqlParameter parameter6 = cmd.Parameters.Add("@Order", SqlDbType.NVarChar);
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = strOrder;

            SqlParameter parameter7 = cmd.Parameters.Add("@ReCordCount", SqlDbType.BigInt);
            parameter7.Direction = ParameterDirection.Output;

            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(DS);
            adapter.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            body.dt = DS.Tables[0];
            body.count = Convert.ToInt32(parameter7.Value.ToString());
            DS.Dispose();

            return body;

        }
        /// <summary>
        /// 返回当页记录集(DataTable)跟记录总数
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="field">字段</param>
        /// <param name="sqlOrTable">SQL语句或者表名</param>
        /// <param name="condition">条件</param>
        /// <param name="order">排序</param>
        /// <param name="connectString">连接串</param>
        /// <returns>body</returns>
        public static body GetListPager(int page, int pageSize, string field, string sqlOrTable, string condition, string order, string connectString)
        {
            Tools.body body = new Tools.body();

            if ("" == field)
            {
                field = "*";
            }

            SqlParameter[] parm = {
                new SqlParameter("@PageCurr", page),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@Field", field),
                new SqlParameter("@Table", sqlOrTable),
                new SqlParameter("@Condition", condition),
                new SqlParameter("@Order", order),
                new SqlParameter("@RecordCount", SqlDbType.Int),
            };
            parm[6].Direction = ParameterDirection.Output;

            body.dt = SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "sp_Get_Current_Page_Record", parm).Tables[0];
            body.count = (int)parm[6].Value;

            return body;
        }
        /// <summary>
        /// 返回当页记录集(DataTable)跟记录总数 记录总数先定义然后 out 方式传入
        /// </summary>
        /// <param name="page">当前页数</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="field">字段</param>
        /// <param name="sqlOrTable">SQL语句或者表名</param>
        /// <param name="condition">条件</param>
        /// <param name="order">排序</param>
        /// <param name="connectString">连接串</param>
        /// <returns>DataTable</returns>
        public static DataTable GetListPager(int page, int pageSize, string field, string sqlOrTable, string condition, string order, string connectString, out int record)
        {

            if ("" == field)
            {
                field = "*";
            }

            SqlParameter[] parm = {
                new SqlParameter("@PageCurr", page),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@Field", field),
                new SqlParameter("@Table", sqlOrTable),
                new SqlParameter("@Condition", condition),
                new SqlParameter("@Order", order),
                new SqlParameter("@RecordCount", SqlDbType.Int),
            };
            parm[6].Direction = ParameterDirection.Output;

            DataTable dt = SqlHelper.ExecuteDataset(connectString, CommandType.StoredProcedure, "sp_Get_Current_Page_Record", parm).Tables[0];
            record = (int)parm[6].Value;

            return dt;
        }

        #endregion

        #region HTTPWEB
        /// <summary>
        /// 获取指定URL的HTML代码
        /// </summary>
        /// <param name="urlString">URL</param>
        /// <returns>指定URL的HTML代码,默认UTF-8编码</returns>
        public static string GetHTML(string urlString)
        {
            return GetHTML(urlString, Encoding.UTF8);
        }
        /// <summary>
        ///  获取指定URL的HTML代码
        /// </summary>
        /// <param name="urlString">URL</param>
        /// <param name="encoding">编码</param>
        /// <returns>指定URL的HTML代码</returns>
        public static string GetHTML(string urlString, Encoding encoding)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebRespones = null;
            Stream stream = null;
            string htmlString = string.Empty;

            //请求页面
            try
            {
                httpWebRequest = WebRequest.Create(urlString) as HttpWebRequest;
            }
            //处理异常
            catch (Exception ex)
            {
                throw new Exception("建立页面请求时发生错误！", ex);
            }
            httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; Maxthon 2.0)";
            //获取服务器的返回信息
            try
            {
                httpWebRespones = (HttpWebResponse)httpWebRequest.GetResponse();
                stream = httpWebRespones.GetResponseStream();
            }
            //处理异常
            catch (Exception ex)
            {
                throw new Exception("接受服务器返回页面时发生错误！", ex);
            }
            StreamReader streamReader = new StreamReader(stream, encoding);
            //读取返回页面
            try
            {
                htmlString = streamReader.ReadToEnd();
            }
            //处理异常
            catch (Exception ex)
            {
                throw new Exception("读取页面数据时发生错误！", ex);
            }
            //释放资源返回结果
            streamReader.Close();
            stream.Close();
            return htmlString;
        }

        /// <summary>
        ///  POST数据到指定的URL，然后接收服务器返回的数据
        /// </summary>
        /// <param name="urlString">URL</param>
        /// <param name="postDataString">post Data String</param>
        /// <returns>POST数据到指定的URL，然后接收服务器返回的数据</returns>
        public static string GetHTML(string urlString, string postDataString)
        {
            return GetHTML(urlString, Encoding.UTF8, postDataString);
        }
        /// <summary>
        ///  POST数据到指定的URL，然后接收服务器返回的数据
        /// </summary>
        /// <param name="urlString">URL</param>
        /// <param name="encoding">编码</param>
        /// <param name="postDataString">post Data String</param>
        /// <returns> POST数据到指定的URL，然后接收服务器返回的数据</returns>
        public static string GetHTML(string urlString, Encoding encoding, string postDataString)
        {
            //定义局部变量
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            Stream inputStream = null;
            Stream outputStream = null;
            StreamReader streamReader = null;
            string htmlString = string.Empty;
            //转换POST数据
            byte[] postDataByte = encoding.GetBytes(postDataString);
            //建立页面请求
            try
            {
                httpWebRequest = WebRequest.Create(urlString) as HttpWebRequest;
            }
            //处理异常
            catch (Exception ex)
            {
                throw new Exception("建立页面请求时发生错误！", ex);
            }
            //指定请求处理方式
            httpWebRequest.Method = "POST";
            httpWebRequest.KeepAlive = false;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentLength = postDataByte.Length;
            //向服务器传送数据
            try
            {
                inputStream = httpWebRequest.GetRequestStream();
                inputStream.Write(postDataByte, 0, postDataByte.Length);
            }
            //处理异常
            catch (Exception ex)
            {
                throw new Exception("发送POST数据时发生错误！", ex);
            }
            finally
            {
                inputStream.Close();
            }
            //接受服务器返回信息
            try
            {
                httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                outputStream = httpWebResponse.GetResponseStream();
                streamReader = new StreamReader(outputStream, encoding);
                htmlString = streamReader.ReadToEnd();
            }
            //处理异常
            catch (Exception ex)
            {
                throw new Exception("接受服务器返回页面时发生错误！", ex);
            }
            finally
            {
                streamReader.Close();
            }
            foreach (Cookie cookie in httpWebResponse.Cookies)
            {
                cookieContainer.Add(cookie);
            }
            return htmlString;
        }
        /// <summary>
        /// 从指定的WEB地址保存图片到本地
        /// </summary>
        /// <param name="URL">URL</param>
        /// <param name="TargetPath">保存路径</param>
        /// <param name="TargetFileName">文件名</param>
        /// <returns>是否成功 true,false</returns>
        public static bool SaveImageFromWeb(string URL, string TargetPath, string TargetFileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            if (response.ContentType.ToLower().StartsWith("image/"))
            {
                byte[] arrayByte = new byte[1024];
                int imgLong = (int)response.ContentLength;
                int l = 0;
                if (!System.IO.Directory.Exists(TargetPath))
                {
                    System.IO.Directory.CreateDirectory(TargetPath);
                }
                FileStream fso = new FileStream(TargetPath + TargetFileName, FileMode.Create);
                while (l < imgLong)
                {
                    int i = stream.Read(arrayByte, 0, 1024);
                    fso.Write(arrayByte, 0, i);
                    l += i;
                }
                fso.Close();
                stream.Close();
                response.Close();
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region 从文章内容抽取图片路径
        /// <summary>
        /// 从文章内容提取图片路径
        /// </summary>
        /// <param name="newsbody">文章HTML代码</param>
        /// <param name="weburl">图片不是已HTTP开头的时候，组合路径用的URL ex:"http://demo.rewrite.com/" + imgstr </param>
        /// <returns>img路径</returns>
        public string GetPicsFromHtmlStr(string newsbody, string weburl)
        {
            string imgstr = newsbody;
            if (imgstr != "")
            {
                Match m = Regex.Match(imgstr, @"<[img|href][^>]*src\s*=\s*('|"")?([^'"">]*)\1([^>])*>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                if (!m.Success)
                {
                    imgstr = "";
                }
                else
                {
                    //<img alt=\"智能手机系统Symbian持续疲软\" src=\"http://news.jinti.com/news/upfiles/20100901/2010090102000669269.jpg\" />
                    string tem = m.Value;
                    Match m1 = Regex.Match(m.Value, @"(?<src>src=('[^']*'|""[^""]*""|[^\s>]*))", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                    if (!m1.Success)
                    {
                        imgstr = "";
                    }
                    else
                    {
                        imgstr = m1.Groups[0].ToString();
                        imgstr = imgstr.Replace("src=", "");
                        imgstr = imgstr.Replace("\"", "");
                        imgstr = imgstr.Replace("'", "");
                    }
                }
            }
            if ((imgstr != "") && !imgstr.StartsWith("http://"))
            {
                //imgstr = "http://demo.rewrite.com/" + imgstr + "";
                imgstr = weburl + imgstr;
            }
            return imgstr;
        }
        #endregion


        /// <summary>
        /// 向URL中补 ? &
        /// </summary>
        /// <param name="strUrl">URL</param>
        /// <returns>补了? & 的url</returns>
        public static string JoinChar(string strUrl)
        {
            if (strUrl.Equals("") || strUrl.Equals(null))
            {
                return "";
            }

            if (strUrl.IndexOf(Convert.ToChar("?")) < strUrl.Length)
            {
                if (strUrl.IndexOf(Convert.ToChar("?")) > 1)
                {
                    if (strUrl.LastIndexOf(Convert.ToChar("&")) < strUrl.Length)
                    {
                        return strUrl + Convert.ToChar("&");
                    }
                    else
                    {
                        return strUrl;
                    }
                }
                else
                {
                    return strUrl + Convert.ToChar("?");
                }
            }
            else
            {
                return strUrl;
            }
        }

        /// <summary>
        /// 过滤SQL中的 ' 替换成 ''
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static string ParseSQL(string strSQL)
        {
            strSQL = strSQL.Replace("'", "''");
            return strSQL;
        }

        /// <summary>
        /// 返回页数
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public static string GetPageTotal(int pageSize, int recordCount)
        {
            int pageTotal = recordCount / pageSize;

            if ((recordCount % pageSize) > 0)
            {
                pageTotal++;
            }
            return pageTotal.ToString();
        }

        /// <summary>
        /// 获取星期数
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetWeekNameByWeekDay(DayOfWeek day)
        {
            string retValue = "";
            switch (day)
            {
                case DayOfWeek.Monday:
                    retValue = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    retValue = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    retValue = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    retValue = "星期四";
                    break;
                case DayOfWeek.Friday:
                    retValue = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    retValue = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    retValue = "星期日";
                    break;
            }
            return retValue;
        }
    }
}
