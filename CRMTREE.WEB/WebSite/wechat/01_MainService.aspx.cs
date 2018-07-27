using CRMTree.BLL.Wechat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wechat_01_MainService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string _JsonData = Request.Form["_d"];
        //wechatAjaxHandle _ajax = new wechatAjaxHandle(_JsonData, HttpContext.Current);
        //Response.Write(_ajax.ToResponse());
    }
    /// <summary>
    /// 客户端请求的处理
    /// </summary>
    /// <param name="jsonData"></param>
    /// <returns></returns>
    [WebMethod]
    public static object AjaxJsonRequert(string jsonData)
    {
        //wechatAjaxHandle _ajax = new wechatAjaxHandle(jsonData, HttpContext.Current);
        //return _ajax.ToResponse();
        return null;

    }
}