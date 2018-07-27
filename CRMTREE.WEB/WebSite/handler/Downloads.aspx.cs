using Shinfotech.Tools;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_Downloads : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        down_load_file();
    }
    void ExportToExcel(string filename, string content)
    {
        filename = HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8);
        Response.Clear();
        Response.Buffer = true;
        content = "<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=UTF-8\"/>" + content;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
        Response.Write(content);
        Response.Flush();
        Response.End();
    }

    /// <summary>  
    /// HttpResponse.AddHeader 方法--将一个 HTTP 头添加到输出流。  
    /// public void AddHeader( string name, string value )  
    /// name                              要添加 value 的 HTTP 头名称。   
    /// value                             要添加到头中的字符串。  
    /// Response.Clear() 方法：           清除缓冲区流中的所有内容输出  
    /// HttpRequest.ContentType    属性： 获取或设置传入请求的 MIME 内容类型  
    /// HttpResponse.BinaryWrite() 方法： 将一个二进制字符串写入 HTTP 输出流。  
    /// HttpResponse.Flush()       方法： 向客户端发送当前所有缓冲的输出  
    /// HttpResponse.WriteFile()   方法： 将指定的文件直接写入 HTTP 响应输出流。  
    /// HttpResponse.End()         方法： 将当前所有缓冲的输出发送到客户端，停止该页的执行，  
    ///                                   并引发 EndRequest 事件。  
    /// </summary>  
    /// <param name="fileName"></param> 客户端保存的文件名  
    /// <param name="filePath"></param> 服务端文件路径  
    private void down_load_file()
    {
        string _fileName = RequestClass.GetString("fileName");
        int _type = RequestClass.GetInt("T",0);
        string _Doc = string.Empty;
        if (_type == 1) {
            _Doc = WebConfig.GetAppSettingString("DownFile");
        }
        else if (_type == 2) {
            _Doc = "plupload\\VINFile\\";
        }
        string _url = HttpContext.Current.Request.PhysicalApplicationPath
                 + "\\" + _Doc
                 + "\\" + _fileName;

        //以字符流的形式下载文件   File.OpenWrite("d:\\excel.csv")
        //FileStream fs = new FileStream(filePath, FileMode.Open);
        FileStream fs = File.OpenRead(_url);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        Response.ContentType = "application/-excel";
        //通知浏览器下载文件而不是打开  
        Response.AddHeader("Content-Disposition", "attachment;filename="
            + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
    private void load_file()
    {
        //II、  
        string filePath = @"d:\\excel.csv";
        System.IO.FileInfo file = new System.IO.FileInfo(filePath);
        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(file.FullName, true);
            Response.End();
        }
    }
    /// <summary>
    /// 使用OutputStream.Write分块下载文件，参数为文件绝对路径
    /// </summary>
    /// <param name="FileName"></param>
    public static void DownLoadFile(string filePath)
    {
        //string filePath = MapPathFile(FileName);
        //指定块大小
        long chunkSize = 204800;
        //建立一个200K的缓冲区
        byte[] buffer = new byte[chunkSize];
        //已读的字节数
        long dataToRead = 0;
        FileStream stream = null;
        try
        {
            //打开文件
            stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            dataToRead = stream.Length;
            //添加Http头
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath)));
            HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());
            while (dataToRead > 0)
            {
                if (HttpContext.Current.Response.IsClientConnected)
                {
                    int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                    HttpContext.Current.Response.Flush();
                    //HttpContext.Current.Response.Clear();
                    buffer = new Byte[chunkSize];
                    dataToRead = dataToRead - length;
                }
                else
                {
                    //防止client失去连接
                    dataToRead = -1;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //HttpContext.Current.Response.Write("Error:" + ex.Message);
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
            HttpContext.Current.Response.Close();
        }
    }
    /// <summary>
    /// 使用OutputStream.Write分块下载文件，参数为文件虚拟路径
    /// </summary>
    /// <param name="FileName"></param>
    public static void DownLoad(string FileName)
    {
        //string filePath = MapPathFile(FileName);
        string filePath = "d:\\excel.csv";
        //指定块大小
        long chunkSize = 204800;
        //建立一个200K的缓冲区
        byte[] buffer = new byte[chunkSize];
        //已读的字节数
        long dataToRead = 0;
        FileStream stream = null;
        try
        {
            //打开文件
            stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            dataToRead = stream.Length;
            //添加Http头
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath)));
            HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());
            while (dataToRead > 0)
            {
                if (HttpContext.Current.Response.IsClientConnected)
                {
                    int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.Clear();
                    dataToRead -= length;
                }
                else
                {
                    //防止client失去连接
                    dataToRead = -1;
                }
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write("Error:" + ex.Message);
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
            HttpContext.Current.Response.Close();
        }
    }
}