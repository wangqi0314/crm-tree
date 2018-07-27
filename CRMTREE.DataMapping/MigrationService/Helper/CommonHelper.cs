using MigrationService.DBConnection;
using NPOI.HSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MigrationService.Helper
{
    public class CommonHelper
    {
        public static CommonHelper instance = new CommonHelper();

        public string dealerParameter = "1";
        public string dealerActuallyName = "1";

        public DataTable ReadFromText(string filePath,char seperateKey)
        {
            var file = File.Open(filePath, FileMode.Open);
            List<string> txtLs = new List<string>();
            using (var stream = new StreamReader(file,Encoding.Default))
            {
                while (!stream.EndOfStream)
                {
                    txtLs.Add(stream.ReadLine());
                }
            }
            DataTable dtTemp = new DataTable();
            int colCount = txtLs.First().Split(seperateKey).ToList().Count()+1;
            int rowCount = txtLs.Count()+1;

            var array = new string[rowCount,colCount];
            var line = 0;
            txtLs.ForEach(t =>
            {
                var row = 0;
                t.Split(seperateKey).ToList().ForEach(p =>
                {
                    array.SetValue(p, line, row);
                    row++;
                });

                line++;
            });
            file.Close();
            dtTemp = ConvertToDataTable(array);

            return dtTemp;
        }

        public DataTable ConvertToDataTable(string[,] arr)
        {

            DataTable dataSouce = new DataTable();
            for (int i = 0; i < arr.GetLength(1)-1; i++)
            {
                DataColumn newColumn = new DataColumn(arr[0, i].ToString(), arr[0, 0].GetType());
                dataSouce.Columns.Add(newColumn);
            }
            for (int i = 1; i < arr.GetLength(0); i++)
            {
                DataRow newRow = dataSouce.NewRow();
                for (int j = 0; j < arr.GetLength(1)-1; j++)
                {
                    newRow[arr[0, j].ToString()] = arr[i, j];
                }
                dataSouce.Rows.Add(newRow);  //  .ItemArray  
            }
            return dataSouce;

        }

        public List<string> GetListFromTable(DataTable dtTemp)
        {
            List<string> lsTemp=new List<string>();
            foreach (DataColumn dc in dtTemp.Columns)
            {
                lsTemp.Add(dc.ColumnName);
            }
            return lsTemp;
        }

        public List<string> GetListFromTable(DataTable dtTemp,string specialColName)
        {
            List<string> lsTemp = new List<string>();
            foreach (DataRow dr in dtTemp.Rows)
            {
                if (dr[specialColName]!=null && dr[specialColName].ToString()!="")
                    lsTemp.Add(dr[specialColName].ToString());
            }
            return lsTemp;
        }

        public string paraReplacement(string Query,string TableName)
        {
            Query=Query.Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter);
            Query=Query.Replace("{{TABLE_NAME}}", TableName);

            return Query;
        }

        public void LogWrriten(string contents,string path)
        {
            path = "d://issues.txt";

            StreamWriter log = new StreamWriter(path , true);

            log.WriteLine(contents);

            log.Close(); 
        }

        public void ReadFromCsv(string filePath, string seperateKey)
        {
            
        }

        #region

        public DataTable ReadFromSourceFile(string filePath, int titleLineStart, int titleLineEnd, string fileName)
        {
            DataTable dt = new DataTable();
            if(filePath.EndsWith(".xlsx") || filePath.EndsWith(".xls"))
            {
                dt = ReadFromExcel(filePath, titleLineStart);
            }
            else if(filePath.EndsWith(".txt"))
            {
                dt = ReadFromTxt(filePath, titleLineStart, 1, fileName);
            }
            return dt;
        }

        
        public DataTable ReadFromExcel(string filePath, int titleLineNo)
        {
            //OleDbConnection objConn = new OleDbConnection(GetExcelConnection(filePath));

            //objConn.Open();

            //DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //if (dt == null)
            //    return null;
            //string sheetName = string.Empty;
             
            //foreach (DataRow row in dt.Rows)
            //{
            //    if(row["TABLE_NAME"]!=null && !string.IsNullOrEmpty(row["TABLE_NAME"].ToString()) && !row["TABLE_NAME"].ToString().Contains("Sheet"))
            //    {
            //        sheetName = row["TABLE_NAME"].ToString();
            //    }
                
            //}
            //string sheetQuery = string.Format("SELECT * FROM [{0}]", sheetName);

            //OleDbDataAdapter oada = new OleDbDataAdapter(sheetQuery, objConn);

            //DataTable dtNew = new DataTable();

            //dtNew.TableName = sheetName;

            //oada.Fill(dtNew);

            ExcelHelper eHelper = new ExcelHelper(filePath);

            DataTable dtNew = eHelper.ExportExcelToDataTable();

            //int i=0;
            //foreach (DataColumn dc in dtNew.Columns)
            //{
            //    dc.ColumnName = dtNew.Rows[titleLineNo - 1][i].ToString();
            //    i++;
            //}

            for (int n = 0; n < titleLineNo; n++)
            {
                dtNew.Rows.Remove(dtNew.Rows[0]);
            }           

            return dtNew;
        }

        public DataTable ReadFromTxt(string filePath, int titleLineStart,int titleLineEnd,string fileName)
        {
            MigrationService.ImplementMethod.TableMigration tm = new ImplementMethod.TableMigration();
            DataTable regardingOurTable = tm.getMyRegardingTable(fileName);

            StreamReader objReader = new StreamReader(filePath,Encoding.Default);

            System.Data.DataTable dt = new System.Data.DataTable();
            int currentLine=0;
            string sLine="";

            int rowNo=0;
            string[] cols=null;

            while (sLine != null)
            {
                sLine = objReader.ReadLine();

                if (currentLine == 0)
                {
                    cols = sLine.Split('\t');

                    foreach(string c in cols)
                    {
                        dt.Columns.Add(c.Trim(), typeof(System.String));
                    }
                    currentLine++;
                }
                else
                {
                    if (sLine != null && !sLine.Equals(""))
                    {
                        string[] rows = sLine.Split('\t');
                        DataRow dr=dt.NewRow();
                        rowNo=0;
                        foreach (string r in rows)
                        {

                            dr[cols[rowNo].Trim()] = ReplaceAndFilter(r).Trim();
                            rowNo++;
                        }
                        dt.Rows.Add(dr);
                    }
                    currentLine++;
                }
            }
            objReader.Close();

            if(dt!=null && dt.Rows.Count>0)
            {
                int rowCount = dt.Rows.Count;

                for (int i = 0; i < titleLineEnd;i++ )
                {
                    dt.Rows.RemoveAt(rowCount-1-i);
                }
                for (int n = 0; n < titleLineStart; n++)
                {
                    dt.Rows.Remove(dt.Rows[0]);
                }    
            }

            return dt;
            
        }

        public string ReplaceAndFilter(string str)
        {
            return str.Replace("--/--/--", "1900/01/01");
        }


        public bool AddDataTableToDB(DataTable source,string tableName)
        {
            SqlTransaction tran = null;//声明一个事务对象  

            StringBuilder sbSql = new StringBuilder();

            ExecuteSQL.RunSqlExecution("drop table "+tableName);

            sbSql.Append("CREATE TABLE " + tableName + "(");
            for (int i = 0; i < source.Columns.Count - 1; i++)
            {
                sbSql.Append("\r" + source.Columns[i].ColumnName.Trim() + " VARCHAR(256) NULL, ");
            }
            sbSql.Append("\r" + source.Columns[source.Columns.Count - 1].ColumnName.Trim() + " VARCHAR(256) NULL) ");
            ExecuteSQL.RunSqlExecution(sbSql.ToString());
            
            try
            {
                //string connectionStr=
                using (SqlConnection conn = ExecuteSQL.GetADOConnection())
                {
                    conn.Open();//打开链接  
                    using (tran = conn.BeginTransaction())
                    {
                        using (SqlBulkCopy copy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
                        {
                            copy.DestinationTableName = tableName;  //指定服务器上目标表的名称  
                            copy.WriteToServer(source);             //执行把DataTable中的数据写入DB  
                            tran.Commit();                          //提交事务  
                            return true;                            //返回True 执行成功！  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (null != tran)
                    tran.Rollback();
                //LogHelper.Add(ex);  
                return false;//返回False 执行失败！  
            }
        }  

        //public DataTable ReadFromExcelNpoi(string filePath, int titleLineNo)
        //{
        //    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate); //读取文件流  
        //    HSSFWorkbook workbook = new HSSFWorkbook(fs);  //根据EXCEL文件流初始化工作簿  
        //    var sheet1 = workbook.GetSheetAt(0); //获取第一个sheet  
        //    DataTable table = new DataTable();//  
        //    var row1 = sheet1.GetRow(0);//获取第一行即标头  
        //    int cellCount = row1.LastCellNum; //第一行的列数  
        //    //把第一行的数据添加到datatable的列名  
        //    for (int i = row1.FirstCellNum; i < cellCount; i++)
        //    {
        //        DataColumn column = new DataColumn(row1.GetCell(i).StringCellValue);
        //        table.Columns.Add(column);
        //    }

        //    int rowCount = sheet1.LastRowNum; //总行数  

        //    //把每行数据添加到datatable中  
        //    for (int i = (sheet1.FirstRowNum + 1); i < sheet1.LastRowNum; i++)
        //    {
        //        //HSSFRow row = sheet1.GetRow(i);
        //        DataRow dataRow = table.NewRow();

        //        for (int j = dataRow.FirstCellNum; j < cellCount; j++)
        //        {
        //            if (dataRow.GetCell(j) != null)
        //                dataRow[j] = dataRow.GetCell(j).ToString();
        //        }

        //        table.Rows.Add(dataRow);
        //    }
        //    //到这里 table 已经可以用来做数据源使用了  
        //    workbook = null; //清空工作簿--释放资源  
        //    sheet1 = null;  //清空sheet</pre><br>  

        //}
        #endregion

        public string GetExcelConnection(string strFilePath)
        {
            if (!File.Exists(strFilePath))
            {
                throw new Exception("指定的Excel文件不存在！");
            }

            if (strFilePath.EndsWith(".xlsx"))
                return "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + strFilePath + ";Extended properties='Excel 12.0;HDR=NO; IMEX=1'";
            else if (strFilePath.EndsWith(".xls"))
                return "Provider=Microsoft.Ace.OleDb.4.0;Data Source=" + strFilePath + ";Extended properties='Excel 8.0;HDR=NO; IMEX=1'";
            else
                return "";
        }


        public void ExportToExcel(string fullfilename,DataTable thisTable)
        {
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
            DataGrid excel = new DataGrid();
            excel.DataSource = thisTable.DefaultView;
            //绑定到DataGrid
            excel.DataBind();
            excel.RenderControl(htmlWriter);
            //这里指定文件的路径
            string filestr = fullfilename;

            if(File.Exists(fullfilename))
                File.Delete(fullfilename);

            int pos = filestr.LastIndexOf("\\");
            string file = filestr.Substring(0, pos);
            if (!Directory.Exists(file))
            {
                Directory.CreateDirectory(file);
            }
            System.IO.StreamWriter sw = new StreamWriter(filestr);
            sw.Write(stringWriter.ToString());

            sw.Close();
            
        }

        public DataTable SelectDistinct(DataTable SourceTable, string FieldName)
        {
            if (SourceTable == null || SourceTable.Rows.Count == 0)
                return null;

            if (string.IsNullOrEmpty(FieldName))
                return SourceTable;
            DataTable dt = new DataTable();
            dt = SourceTable.Clone();
            

            DataView dv = new DataView(SourceTable);
            DataTable dt2 = dv.ToTable(true, FieldName);

            List<string> ls =new List<string>();

            foreach(DataRow dr in dt2.AsEnumerable())
            {
                ls.Add(dr[FieldName].ToString());
            }
            //List<string> ls = dt.Columns
            

            foreach (DataRow dr in SourceTable.AsEnumerable())
            {
                if(ls.Contains(dr[FieldName].ToString()))
                {
                    ls.Remove(dr[FieldName].ToString());
                    dt.ImportRow(dr);
                }
            }

             
            ////if (ds != null)
            ////ds.Tables.Add(dt);
            return dt;
        }
  
        public bool SendMail(string AUsername, string APassword, List<string> AToList, List<string> ACcList, List<string> BCcList, string ASubject, string AFrom, string AFromDisplayName, string ASmtpServer, string AContext,string filePath)
        {
            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(AFrom, AFromDisplayName);
                if (AToList != null && AToList.Count != 0)
                {
                    foreach (string to in AToList)
                    {
                        message.To.Add(new MailAddress(to));
                    }
                }
                if (ACcList != null && ACcList.Count != 0)
                {
                    foreach (string cc in ACcList)
                    {
                        message.CC.Add(new MailAddress(cc));
                    }
                }
                if (BCcList != null && BCcList.Count != 0)
                {
                    foreach (string bcc in BCcList)
                    {
                        message.Bcc.Add(new MailAddress(bcc));
                    }
                }

                message.Subject = ASubject;
                message.Body = AContext;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.GetEncoding("UTF-8");
                message.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                message.Priority = MailPriority.Normal;

                if(!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                  message.Attachments.Add(new Attachment(filePath));
                

                SmtpClient client = new SmtpClient(ASmtpServer,25);
                 
                //client.EnableSsl = true;
                //client.ClientCertificates=
                //client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(AUsername, APassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public static void Kill(Microsoft.Office.Interop.Excel.Application excel)
        {
            IntPtr t = new IntPtr(excel.Hwnd); //得到这个句柄，具体作用是得到这块内存入口

            int k = 0;
            GetWindowThreadProcessId(t, out k); //得到本进程唯一标志k
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k); //得到对进程k的引用
            p.Kill(); //关闭进程k
        }

        public bool generateEmailToItStuffs(DataTable dtTemp,string MailMessageContent,string MailTitle)
        {

            string FileName = "E:\\TEMP\\VinFailToDecode" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".xls";

            if (dtTemp == null)
                FileName = "";
            else
                CommonHelper.instance.ExportToExcel(FileName, dtTemp);

            List<string> toList = new List<string>();
            toList.Add("Winnie.shi@shunovo.com");
            toList.Add("Nicolas.xia@shunovo.com");
            toList.Add("fariborz@shunovo.com");
            List<string> ccList = new List<string>();
            //ccList.Add("fagahdel@gmail.com");
            //ccList.Add("shihong881214@163.com");
            //ccList.Add("Winnie.shi@shunovo.com");
            ccList.Add("234163000@qq.com");
            //ccList.Add("fariborz@shunovo.com");
            List<string> bccList = new List<string>();
            bccList.Add("fagahdel@gmail.com");

            CommonHelper.instance.SendMail("information@shunovo.com", "Thinktree123", toList, ccList, bccList, MailTitle, "information@shunovo.com", "ThinkingTree", "smtp.mxhichina.com", MailMessageContent, FileName);

            return true; ;
        }
    }
}
