<%@ WebHandler Language="C#" Class="Wechat_Service" %>

using System;
using System.Web;
using CRMTree.BLL.Wechat;

public class Wechat_Service : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //wechatAjaxHandle _ajax = new wechatAjaxHandle(context);
        //context.Response.ContentType = "text/plain";
        //context.Response.Write(_ajax.ToResponse());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}