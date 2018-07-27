using System;
using System.IO;
using System.Web;

public partial class handler_upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (this.Page.Request.Files.Count > 0)
        {
            try
            {
                for (int i = 0; i < this.Page.Request.Files.Count; i++)
                {
                    string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
                    HttpPostedFile uploadFile = this.Page.Request.Files[i];
                    var uplength = uploadFile.ContentLength;
                    if (uplength > 0)
                    {
                        string extname = Path.GetExtension(uploadFile.FileName);
                        string strFilename = uploadFile.FileName;
                        string fullname = DateTime.Now.ToString("yyMMddHHmmssffff") + extname;//文件名含后缀
                        var sr = uploadFile.InputStream;
                        //定义byte型数组   
                        var b = new byte[uplength];
                        sr.Read(b, 0, uplength);

                        //string UploadFilePath = Server.MapPath("~/upload/");
                        uploadFile.SaveAs(strPath + fullname);
                        Response.Write("{\"fileName\":\"" + strFilename + "\",\"fullname\":\"" + fullname + "\"}");
                    }
                    else Response.Write(-2);
                }
            }
            catch 
            {
                Response.Write(-3);
            }
        }
    }
}