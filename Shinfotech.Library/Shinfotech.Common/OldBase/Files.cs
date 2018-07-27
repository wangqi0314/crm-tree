using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;
namespace ShInfoTech.Common
{
    /// <summary>
    /// 文件处理类
    /// </summary>
    public class Files
    {
        /// <summary>
        /// 文件检测
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns>true:是否存在并且可写,false:不存在或者不可写</returns>
        public static bool CheckFile(string Path)
        {
            if (!File.Exists(Path))
            {
                return false;
            }
            FileInfo info = new FileInfo(Path);
            if (info.Attributes.ToString().IndexOf("ReadOnly") != -1)
            {
                try
                {
                    info.Attributes = FileAttributes.Normal;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检测目录是否存在
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns>true:存在,false:不存在</returns>
        public static bool CheckFolder(string Path)
        {
            bool flag = false;
            if (Directory.Exists(Path))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        public static void CreateFile(string Path)
        {
            try
            {
                File.CreateText(Path);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 建立目录
        /// </summary>
        /// <param name="Path">目录路径</param>
        /// <returns>true:建立成功,false:建立失败</returns>
        public static bool CreateFolder(string Path)
        {
            try
            {
                if (!CheckFolder(Path))
                {
                    Directory.CreateDirectory(Path);
                    return true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns>true文件删除,false没有删除</returns>
        public static bool DeleteFile(string Path)
        {
            bool flag = false;
            if (!File.Exists(Path))
            {
                return flag;
            }
            try
            {
                File.Delete(Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 返回时间种子
        /// </summary>
        /// <returns></returns>
        public static string GenerateFileName()
        {
            return (DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Millisecond.ToString());
        }

        /// <summary>
        /// 获取文件的大小
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns>文件大小</returns>
        public static long GetFileSize(string Path)
        {
            long length = 0L;
            if (CheckFile(Path))
            {
                FileInfo info = new FileInfo(Path);
                length = info.Length;
            }
            return length;
        }

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="strEncoding">文件编码</param>
        /// <returns>内容</returns>
        public static string ReadFile(string Path, string strEncoding)
        {
            string str;
            try
            {
                using (StreamReader reader = new StreamReader(Path, Encoding.GetEncoding(strEncoding)))
                {
                    str = reader.ReadToEnd();
                }
            }
            catch
            {
                str = null;
            }
            return str;
        }

        /// <summary>
        /// 写入文件内容
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="Content">内容</param>
        /// <param name="strEncoding">文件编码</param>
        public static void WriteFile(string Path, string Content, string strEncoding)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Path, false, Encoding.GetEncoding(strEncoding)))
                {
                    writer.Write(Content);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        ///  根据dataSet导出文件
        /// </summary>
        /// <param name="ds">dataSet对象</param>
        ///      <param name="fileType">导出方式(save为保存到本地，open为在线打开)</param>
        ///     <param name="strType">导出的文件类型(excel,word,html)</param>
        ///    <param name="modelFile">模板xml文件（是以table形式）</param>
        /// <returns></returns>
        public static bool FileExport(DataSet ds, string fileType, string strType, string modelFile)
        {
            string exportType = "";
            string saveType = "";
            string strfiletype = fileType;
            if (strfiletype != null)
            {
                if (!(strfiletype == "save"))
                {
                    if (strfiletype == "open")
                    {
                        strfiletype = strType;
                        if (strfiletype != null)
                        {
                            if (!(strfiletype == "excel"))
                            {
                                if (strfiletype == "word")
                                {
                                    exportType = "online;filename=out.doc";
                                    saveType = "application/ms-word";
                                }
                                else if (strfiletype == "html")
                                {
                                    exportType = "online;filename=out.html";
                                    saveType = "application/ms-html";
                                }
                            }
                            else
                            {
                                exportType = "online;filename=out.xls";
                                saveType = "application/ms-excel";
                            }
                        }
                    }
                }
                else
                {
                    strfiletype = strType;
                    if (strfiletype != null)
                    {
                        if (!(strfiletype == "excel"))
                        {
                            if (strfiletype == "word")
                            {
                                exportType = "attachment;filename=out.doc";
                                saveType = "application/ms-word";
                            }
                            else if (strfiletype == "html")
                            {
                                exportType = "attachment;filename=out.html";
                                saveType = "application/ms-html";
                            }
                        }
                        else
                        {
                            exportType = "attachment;filename=out.xls";
                            saveType = "application/ms-excel;IMEX=1";
                        }
                    }
                }
            }
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(HttpContext.Current.Server.MapPath(modelFile));
                    XmlNode modeNode = doc.SelectSingleNode("table").ChildNodes[1];
                    HttpResponse resp = HttpContext.Current.Response;
                    resp.ContentEncoding = Encoding.GetEncoding("GB2312");
                    resp.Charset = "GB2312";
                    resp.AppendHeader("Content-Disposition", exportType);
                    resp.ContentType = saveType;
                    DataRow[] myRow = ds.Tables[0].Select();
                    int cl = modeNode.ChildNodes.Count;
                    foreach (DataRow row in myRow)
                    {
                        XmlNode node = modeNode.CloneNode(true);
                        for (int i = 0; i < cl; i++)
                        {
                            node.ChildNodes[i].InnerText = row[i].ToString();
                        }
                        doc.SelectSingleNode("table").AppendChild(node);
                    }
                    doc.SelectSingleNode("table").RemoveChild(modeNode);
                    string retStr = doc.SelectSingleNode("table").OuterXml;
                    resp.Write(retStr);
                    resp.End();
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return false;
        }
        /// <summary>
        /// 读取文件的内容，HTML，TXT
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Filename"></param>
        /// <returns></returns>
        public static string FileContext(string Path ,string Filename)
        {
            string fileContext = string.Empty;
            try
            {
                //判断文件是不是存在
                if (File.Exists(Path + Filename))
                {
                    FileStream fs = new FileStream(Path + Filename, FileMode.Open);
                    StreamReader m_streamReader = new StreamReader(fs);
                    // 从数据流中读取每一行，直到文件的最后一行
                    m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    fileContext = m_streamReader.ReadToEnd();
                    m_streamReader.Dispose();
                    m_streamReader.Close();
                }
            }
            catch 
            {
                return fileContext;
            }
            return fileContext;
        }
        public static string FileContext(string PathFilename)
        {
            string fileContext = string.Empty;
            try
            {
                //判断文件是不是存在
                if (File.Exists(PathFilename))
                {
                    FileStream fs = new FileStream(PathFilename, FileMode.Open);
                    StreamReader m_streamReader = new StreamReader(fs);
                    // 从数据流中读取每一行，直到文件的最后一行
                    m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    fileContext = m_streamReader.ReadToEnd();
                    m_streamReader.Dispose();
                    m_streamReader.Close();
                }
            }
            catch
            {
                return fileContext;
            }
            return fileContext;
        }
    }
}