using System;
using System.Collections.Generic;
using System.Web;
using ShInfoTech.Common;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CRMTree.Model.Reports;
using CRMTree.Public;
using Shinfotech.Tools;
using System.Collections.Specialized;
using CRMTree.Model.User;
using CRMTree.BLL;
using CRMTree.Model.Common;


namespace CRMTree
{
    /// <summary>
    ///Function 的摘要说明
    /// </summary>
    public class Function
    {
        public Function()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static String SQLConnection = DecryptSqlPwd("CRMTREEConnectionString");


        public static string DecryptSqlPwd(string connectionStr)
        {
            string SqlStr = WebConfig.GetConnectionString(connectionStr);

            string[] str = SqlStr.Split(';');

            StringBuilder sb = new StringBuilder();

            string ss = string.Empty; 
            
            foreach(string s in str)
            {
                if(s.Contains("User ID="))
                {
                    ss=s.Replace("User ID=", "");

                    ss=Shinfotech.Tools.Encryptions.DecryptDES(ss, "Shunovo20150701");

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
                else if (string.IsNullOrEmpty(s))
                {
                    continue;
                }
                else
                {
                    sb.Append(s+";");
                }

            }
            return sb.ToString();
        }



        /// <summary>
        /// 通过事务执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool QueryByTransaction(string sql)
        {
            bool flag = false;
            SqlConnection conn = new SqlConnection(SQLConnection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlTransaction trans = conn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
                cmd.ExecuteNonQuery();
                trans.Commit();
                flag = true;
            }
            catch 
            {
                trans.Rollback();
                flag = false;
            }
            conn.Close();
            conn.Dispose();
            cmd.Dispose();
            return flag;
        }

        /// <summary>
        /// 根据SQL语句返回查询结果
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>String</returns>
        public static string GetColumnStringBySQL(string sql)
        {
            object obj = SqlHelper.ExecuteScalar(SQLConnection, CommandType.Text, sql.ToString());
            return Security.ToStr(obj);
        }

        /// <summary>
        /// 通过SQL语句执行返回是否
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>bool</returns>
        public static bool QueryBySQL(string sql)
        {
            object obj = SqlHelper.ExecuteNonQuery(sql);
            return Security.ToNum(obj) > 0;
        }

        /// <summary>
        /// 根据SQL语句返回TABLE 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>DataTable</returns>
        public static DataTable GetColumnTableBySQL(string sql)
        {
            DataTable dt = SqlHelper.ExecuteDataset(sql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据SQL语句返回TABLE 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>DataTable</returns>
        public static DataSet GetColumnDataSetBySQL(string sql)
        {
            DataSet ds = SqlHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取一行 DataRow
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>DataRow</returns>
        public static DataRow GetOneRow(string sql)
        {
            DataRowCollection drs = SqlHelper.ExecuteDataset(sql).Tables[0].Rows;
            return drs.Count > 0 ? drs[0] : null;
        }


        /// <summary>
        /// 安全处理SQL '替换成 ''
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <returns></returns>
        public static string ParseSQL(string strSQL)
        {
            strSQL = strSQL.Replace("'", "''");
            return strSQL;
        }


        /// <summary>
        /// 判断字符串是否在数组中
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strarry"></param>
        /// <returns></returns>
        public static bool StrInArray(string str, string[] strarry)
        {
            if (str == null)
            {
                return false;
            }
            if (strarry == null || strarry.Length == 0)
            {
                return false;
            }
            for (int i = 0; i < strarry.Length; i++)
            {
                if (strarry[i] == null)
                    continue;
                if (str == strarry[i])
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取Chart颜色值
        /// </summary>
        /// <param name="i">第几根柱子 从1开始</param>
        /// <returns>颜色值 不带#</returns>
        public static string GetColorByID(int i)
        {
            string color = "";
            switch (i)
            {
                case 1:
                    color = "003366";//4F81BD
                    break;
                case 2:
                    color = "990000";//800000
                    break;
                case 3:
                    color = "928C86";//FF6600
                    break;
                case 4:
                    color = "7FABD2";//808000
                    break;
                case 5:
                    color = "DDDDDD";//008000
                    break;
                case 6:
                    color = "FFCC00";
                    break;
                case 7:
                    color = "0000FF";
                    break;
                case 8:
                    color = "666699";
                    break;
                case 9:
                    color = "808080";
                    break;
                case 10:
                    color = "000000";
                    break;
                case 11:
                    color = "993300";
                    break;
                case 12:
                    color = "333300";
                    break;
                case 13:
                    color = "003300";
                    break;
                case 14:
                    color = "003366";
                    break;
                case 15:
                    color = "000080";
                    break;
                case 16:
                    color = "333399";
                    break;
                case 17:
                    color = "333333";
                    break;
                case 18:
                    color = "FF0000";
                    break;
                case 19:
                    color = "FF9900";
                    break;
                case 20:
                    color = "99CC00";
                    break;
                case 21:
                    color = "339966";
                    break;
                case 22:
                    color = "33CCCC";
                    break;
                case 23:
                    color = "3366FF";
                    break;
                case 24:
                    color = "800080";
                    break;
                case 25:
                    color = "969696";
                    break;
                case 26:
                    color = "FF00FF";
                    break;
                case 27:
                    color = "FFCC00";
                    break;
                case 28:
                    color = "FFFF00";
                    break;
                case 29:
                    color = "00FF00";
                    break;
                case 30:
                    color = "00FFFF";
                    break;
                case 31:
                    color = "00CCFF";
                    break;
                case 32:
                    color = "993366";
                    break;
                case 33:
                    color = "C0C0C0";
                    break;
                case 34:
                    color = "FF99CC";
                    break;
                case 35:
                    color = "FFCC99";
                    break;
                case 36:
                    color = "FFFF99";
                    break;
                case 37:
                    color = "CCFFCC";
                    break;
                case 38:
                    color = "CCFFFF";
                    break;
                case 39:
                    color = "CC99FF";
                    break;
                default:
                    color = "4F81BD";
                    break;
            }
            return color;
        }


        /// <summary>
        /// 获得Repeater中选中CheckboxValue
        /// </summary>
        /// <param name="rpt">Repeater</param>
        /// <param name="controlName">Checkbox Name</param>
        /// <returns></returns>
        public static string GetCheckboxValue(Repeater rpt, string controlName)
        {
            string reportID = "";
            for (int i = 0; i < rpt.Items.Count; i++)
            {
                HtmlInputCheckBox chb = (HtmlInputCheckBox)rpt.Items[i].FindControl(controlName);
                if (chb.Checked == true)
                {
                    reportID = reportID + chb.Value + ',';
                }
            }
            if (reportID.Contains(","))
            {
                reportID = reportID.Substring(0, reportID.Length - 1);
            }
            return reportID;

        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="page">操作页面</param>
        /// <param name="fileName">导出的文件名称</param>
        /// <param name="text">要导出的内容</param>
        public static void ExportExcel(Page page, string fileName, string text)
        {
            page.EnableViewState = false;
            try
            {
                page.Response.ClearContent();
                page.Response.Buffer = true;
                page.Response.Charset = "utf-8";//设置字符集，解决中文乱码问题
                page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                page.Response.Write("<meta http-equiv=Content-Type content=\"text/html;charset=utf-8\">");//解决乱码问题
                //解决HTTP头中文乱码问题
                string strExcelText = DateTime.Now.ToString("yyyyMMddHHmmss") + "\t" + fileName;//Excel显示的内容
                string strEncode = System.Web.HttpUtility.UrlEncode(strExcelText, System.Text.Encoding.UTF8);//进行编码的格式,用gb2312出错
                page.Response.AddHeader("content-disposition", "attachment;filename=\"" + strEncode + ".xls\"");//对保存标题进行编码
                page.Response.ContentType = "application/vnd.xls";//设置输出格式
                page.Response.Write(@"<html><head><style>
                                        .list_table {border: 1px solid #000000;padding: 0;margin: 0 auto;border-width: thin;border-collapse: collapse;}
                                        td{ border: 1px solid #000000;font-size: 12px;border-width: thin;text-align: center;padding: 3px 3px 3px 8px;mso-number-format: \@;}
                                        th{border: 1px solid #000000;font-size: 12px;border-width: thin;text-align: center;padding: 3px 3px 3px 8px;mso-number-format: \@;}
                                    </style></head><body>");
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                htw.WriteLine(text);//将数据输出
                page.Response.Write(sw.ToString());
                page.Response.Write("</body></html>");
                page.Response.Flush();
                page.Response.End();
            }
            catch
            {
                return;
            }
            finally
            {
                //恢复原来控件内容
                page.EnableViewState = true;
            }
        }


        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fileid"></param>
        /// <param name="clientid"></param>
        public static void WriteLogFile(string input, int fileid, int clientid)
        {
            string path = WebConfig.GetAppSettingString("CSIFilePath");
            //指定日志文件的目录
            string fname = path + "\\UploadFiles\\UploadLogs\\" + clientid + "\\logfile-" + fileid + ".txt";
            string strSavePath = path + "\\UploadFiles\\UploadLogs\\" + clientid;
            if (!System.IO.Directory.Exists(strSavePath))
            {
                System.IO.Directory.CreateDirectory(strSavePath);
            }
            FileInfo finfo = new FileInfo(fname);
            if (finfo.Exists && finfo.Length > 2222048)
            {
                finfo.Delete();
                FileStream fs = new FileStream(fname, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs);
                writer.WriteLine("----------");
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine(input);
                writer.WriteLine("----------");
                writer.Flush();
                writer.Close();
                fs.Close();
            }
            else
            {
                using (FileStream fs = finfo.OpenWrite())
                {
                    StreamWriter w = new StreamWriter(fs);
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    w.WriteLine("----------");
                    w.WriteLine("{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    w.WriteLine(input);
                    w.WriteLine("----------");
                    w.Flush();
                    w.Close();
                }
            }
        }

        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fileid"></param>
        /// <param name="clientid"></param>
        public static void WriteLogFile(string input)
        {
            string path = WebConfig.GetAppSettingString("CSIFilePath");
            //指定日志文件的目录
            string fname = path + "\\UploadFiles\\UploadLogs\\System\\logfile-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string strSavePath = path + "\\UploadFiles\\UploadLogs\\System";
            if (!System.IO.Directory.Exists(strSavePath))
            {
                System.IO.Directory.CreateDirectory(strSavePath);
            }
            FileInfo finfo = new FileInfo(fname);
            if (finfo.Exists && finfo.Length > 222048)
            {
                finfo.Delete();
                FileStream fs = new FileStream(fname, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs);
                writer.WriteLine("----------");
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine(input);
                writer.WriteLine("----------");
                writer.Flush();
                writer.Close();
                fs.Close();
            }
            else
            {
                using (FileStream fs = finfo.OpenWrite())
                {
                    StreamWriter w = new StreamWriter(fs);
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    w.WriteLine("----------");
                    w.WriteLine("{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    w.WriteLine(input);
                    w.WriteLine("----------");
                    w.Flush();
                    w.Close();
                }
            }
        }


        /// <summary>
        /// 行转列
        /// </summary>
        /// <param name="dt">要转换的表</param>
        /// <returns>转换列后的表</returns>
        public static DataTable RowsToColumns(DataTable dt)
        {
            DataTable dtNew = new DataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataColumn cl = new DataColumn();
                dtNew.Columns.Add(cl);
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataRow row = dtNew.NewRow();
                for (int j = 0; j < dtNew.Columns.Count; j++)
                {
                    row[j] = dt.Rows[j][i].ToString();
                }
                dtNew.Rows.Add(row);
            }
            return dtNew;
        }

        /// <summary>
        /// 将两张相同表结构的表，合并成一张表
        /// </summary>
        /// <param name="dtOn">表1，合并后行在上</param>
        /// <param name="dtDown">表2，合并后行在下</param>
        /// <returns>合并后的表</returns>
        public static DataTable JoinTableByRow(DataTable dtOn, DataTable dtDown)
        {
            for (int i = 0; i < dtDown.Rows.Count; i++)
            {
                DataRow row = dtOn.NewRow();
                for (int j = 0; j < dtDown.Columns.Count; j++)
                {
                    row[j] = dtDown.Rows[i][j];
                }
                dtOn.Rows.Add(row);
            }
            return dtOn;
        }

        /// <summary>
        /// 将两张表(左表和右表)按列合并成一张表(左表)，左表是主表。
        /// </summary>
        /// <param name="dtLeft">左表</param>
        /// <param name="dtRight">右表</param>
        /// <returns></returns>
        public static DataTable JoinTableByColumn(DataTable dtLeft, DataTable dtRight)
        {
            int leftcolumnCount = dtLeft.Columns.Count;//左表原始列数
            for (int i = 0; i < dtRight.Columns.Count; i++)
            {
                string random = Guid.NewGuid().ToString();
                DataColumn column = new DataColumn("column/" + random);
                dtLeft.Columns.Add(column);
            }
            //两张表行数相同
            if (dtLeft.Rows.Count == dtRight.Rows.Count)
            {
                for (int i = 0; i < dtLeft.Rows.Count; i++)
                {
                    for (int j = 0; j < dtRight.Columns.Count; j++)
                    {
                        dtLeft.Rows[i][leftcolumnCount + j] = dtRight.Rows[i][j];
                    }
                }
            }
            //左表行数大于右表行数
            if (dtLeft.Rows.Count > dtRight.Rows.Count)
            {
                for (int i = 0; i < dtRight.Rows.Count; i++)
                {
                    for (int j = 0; j < dtRight.Columns.Count; j++)
                    {
                        dtLeft.Rows[i][leftcolumnCount + j] = dtRight.Rows[i][j];
                    }
                }
            }
            //左表行数小于右表行数
            if (dtLeft.Rows.Count < dtRight.Rows.Count)
            {
                for (int i = 0; i < dtLeft.Rows.Count; i++)
                {
                    for (int j = 0; j < dtRight.Columns.Count; j++)
                    {
                        dtLeft.Rows[i][leftcolumnCount + j] = dtRight.Rows[i][j];
                    }
                }
                int differ = dtRight.Rows.Count - dtLeft.Rows.Count;
                for (int i = 0; i < differ; i++)
                {
                    DataRow row = dtLeft.NewRow();
                    for (int j = 0; j < dtRight.Columns.Count; j++)
                    {
                        row[leftcolumnCount + j] = dtRight.Rows[i + differ][j];
                    }
                    dtLeft.Rows.Add(row);
                }
            }
            return dtLeft;
        }

        public string getReprotQuery(int RP_Code)
        {
            BL_Reports RE = new BL_Reports();
            return RE.GetReplaceReport(RP_Code).RP_Query;
        }
    }
}