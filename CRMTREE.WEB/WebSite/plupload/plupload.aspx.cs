using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plupload_plupload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var uploadFilter = System.Configuration.ConfigurationManager.AppSettings["PLUploadFilter"];
            var fName = Request.Params["folderName"] != null ? Request.Params["folderName"] : "";
            var filter = Request.Params["filter"] != null ? Request.Params["filter"] : uploadFilter;
            string folderName = string.Empty;
            if (!string.IsNullOrWhiteSpace(fName))
            {
                folderName = fName;
            }
            else
            {
                folderName = "file_temp";
            }
            //switch (fName)
            //{
            //    case "customer_temp":
            //        folderName = "customer_temp";
            //        break;
            //    case "employee_temp":
            //        folderName = "employee_temp";
            //        break;
            //    default:
            //        folderName = "file_temp";
            //        break;
            //}
            PLUpload(folderName, filter);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private void PLUpload(string folderName, string filter) 
    {
        string fileName = Request.Params["name"] != null ? Request.Params["name"] : "";
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new Exception("文件名为空！");
        }

        
        if (string.IsNullOrWhiteSpace(filter))
        {
            filter = "jpg,jpeg,gif,png,bmp";
        }

        //验证文件后缀名称
        string[] fileNames = fileName.Split('.');
        var extendName = fileNames[fileNames.Length - 1];
        extendName = extendName.ToLower();
        filter = filter.ToLower();
        string[] filterNames = filter.Split(',');
        var bValid = filterNames.Contains<string>(extendName);
        if (!bValid)
        {
            throw new Exception("非法上传！");
        }
        //验证文件大小

        var plUploadPath = System.Configuration.ConfigurationManager.AppSettings["PLUploadPath"];
        if (string.IsNullOrWhiteSpace(plUploadPath))
        {
            plUploadPath = "~/plupload/";
        }
        var savePath = Server.MapPath(plUploadPath + folderName + "/");
        if(!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        var path = savePath + fileName;
        int chunk = Request.Params["chunk"] != null ? int.Parse(Request.Params["chunk"]) : 0;
        FileStream fs = new FileStream(path, chunk == 0 ? FileMode.OpenOrCreate : FileMode.Append);

        //write our input stream to a buffer
        Byte[] buffer = null;
        if (Request.ContentType == "application/octet-stream" && Request.ContentLength > 0)
        {
            buffer = new Byte[Request.InputStream.Length];
            Request.InputStream.Read(buffer, 0, buffer.Length);
        }
        else if (Request.ContentType.Contains("multipart/form-data") 
            && Request.Files.Count > 0 
            && Request.Files[0].ContentLength > 0)
        {
            buffer = new Byte[Request.Files[0].InputStream.Length];
            Request.Files[0].InputStream.Read(buffer, 0, buffer.Length);
        }

        //write the buffer to a file.
        fs.Write(buffer, 0, buffer.Length);
        fs.Close();
    }

}