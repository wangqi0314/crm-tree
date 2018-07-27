using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_VINUpload : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Response.CacheControl = "no-cache";
        if (this.Page.Request.Files.Count > 0)
        {
            try
            {
                for (int j = 0; j < this.Page.Request.Files.Count; j++)
                {
                    HttpPostedFile uploadFile = this.Page.Request.Files[j];
                    var uplength = uploadFile.ContentLength;
                    if (uplength > 0)
                    {
                        string extname = Path.GetExtension(uploadFile.FileName);
                        string strFilename = uploadFile.FileName;
                        //string fullname = SetFileName() + extname;//文件名含后缀
                        string _name = Request.Params["name"] != null ? Request.Params["name"] : "";
                        var sr = uploadFile.InputStream;
                        //定义byte型数组   
                        var b = new byte[uplength];
                        sr.Read(b, 0, uplength);
                        uploadFile.SaveAs(Server.MapPath("~/plupload/VINFile/") + _name);
                        Response.Write("{\"fileName\":\"" + strFilename + "\",\"fullname\":\"" + _name + "\"}");
                    }
                    else Response.Write(-1);
                }
            }
            catch
            {
                Response.Write(-2);
            }
        }
    }
    private string SetFileName()
    {
        var user = UserSession.User;
        return "VIN_" + user.AU_Code.ToString() + "_" + DateTime.Now.ToString("yyMMddHHmmssffff");
    }
}