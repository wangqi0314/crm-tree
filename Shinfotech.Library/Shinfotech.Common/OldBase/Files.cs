using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;
namespace ShInfoTech.Common
{
    /// <summary>
    /// �ļ�������
    /// </summary>
    public class Files
    {
        /// <summary>
        /// �ļ����
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
        /// <returns>true:�Ƿ���ڲ��ҿ�д,false:�����ڻ��߲���д</returns>
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
        /// ���Ŀ¼�Ƿ����
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
        /// <returns>true:����,false:������</returns>
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
        /// �����ļ�
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
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
        /// ����Ŀ¼
        /// </summary>
        /// <param name="Path">Ŀ¼·��</param>
        /// <returns>true:�����ɹ�,false:����ʧ��</returns>
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
        /// ɾ���ļ�
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
        /// <returns>true�ļ�ɾ��,falseû��ɾ��</returns>
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
        /// ����ʱ������
        /// </summary>
        /// <returns></returns>
        public static string GenerateFileName()
        {
            return (DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Millisecond.ToString());
        }

        /// <summary>
        /// ��ȡ�ļ��Ĵ�С
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
        /// <returns>�ļ���С</returns>
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
        /// ��ȡ�ļ�����
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
        /// <param name="strEncoding">�ļ�����</param>
        /// <returns>����</returns>
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
        /// д���ļ�����
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
        /// <param name="Content">����</param>
        /// <param name="strEncoding">�ļ�����</param>
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
        ///  ����dataSet�����ļ�
        /// </summary>
        /// <param name="ds">dataSet����</param>
        ///      <param name="fileType">������ʽ(saveΪ���浽���أ�openΪ���ߴ�)</param>
        ///     <param name="strType">�������ļ�����(excel,word,html)</param>
        ///    <param name="modelFile">ģ��xml�ļ�������table��ʽ��</param>
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
        /// ��ȡ�ļ������ݣ�HTML��TXT
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Filename"></param>
        /// <returns></returns>
        public static string FileContext(string Path ,string Filename)
        {
            string fileContext = string.Empty;
            try
            {
                //�ж��ļ��ǲ��Ǵ���
                if (File.Exists(Path + Filename))
                {
                    FileStream fs = new FileStream(Path + Filename, FileMode.Open);
                    StreamReader m_streamReader = new StreamReader(fs);
                    // ���������ж�ȡÿһ�У�ֱ���ļ������һ��
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
                //�ж��ļ��ǲ��Ǵ���
                if (File.Exists(PathFilename))
                {
                    FileStream fs = new FileStream(PathFilename, FileMode.Open);
                    StreamReader m_streamReader = new StreamReader(fs);
                    // ���������ж�ȡÿһ�У�ֱ���ļ������һ��
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