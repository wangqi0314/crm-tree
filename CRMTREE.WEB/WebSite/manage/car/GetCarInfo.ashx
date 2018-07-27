<%@ WebHandler Language="C#" Class="GetCarInfo" %>

using System;
using System.Web;
using System.Web.SessionState;
using CRMTree.Model.User;
using CRMTree.BLL;
using ShInfoTech.Common;
public class GetCarInfo : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string str = "";
        if (context.Request.Params["_auid"] != null && context.Request.Params["_auid"] != "undefined" && context.Request.Params["_auid"] != "null" && context.Request.Params["_auid"] != "")
        {
            string auid = context.Request.Params["_auid"].ToString();
            str = Newtonsoft.Json.JsonConvert.SerializeObject(new BL_MyCar().GetCarInfoByAU_Code(Convert.ToInt32(auid)));
            context.Response.Write(str);
        }
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}