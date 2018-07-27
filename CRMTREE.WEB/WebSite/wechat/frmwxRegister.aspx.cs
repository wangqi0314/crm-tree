using CRMTree.BLL;
using CRMTree.BLL.Wechat;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wechat_frmwxRegister : System.Web.UI.Page
{
    public string openId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            openId = wechatHandle.GetOpenId(HttpContext.Current,"");
        }
    }
    [WebMethod]
    public static string SendMesageing(string mobile, string ran)
    {
        SendMessage.SendMobileVerification(mobile, ran,"90");
        return "1";
    }
    [WebMethod]
    public static int VerificationUsername(string mobile)
    {
        string openId = wechatHandle.GetOpenId(HttpContext.Current, "");
        if (string.IsNullOrEmpty(openId))
        {
            //openId = "ogw2MjhK4qPJbDSmtCeXWLezqAOM1";
        }
        BL_UserEntity u = new BL_UserEntity();
        int i = u.VerificationUsername(mobile, openId);
        return i;
    }
    [WebMethod]
    public static string WechatRegister(string mobile, string Pwd)
    {
        string openId = wechatHandle.GetOpenId(HttpContext.Current, "");
        if (string.IsNullOrEmpty(openId))
        {
            //openId = "ogw2MjhK4qPJbDSmtCeXWLezqAOM1";
        }
        BL_UserEntity u = new BL_UserEntity();
        string i = u.WechatRegister(mobile,  ShInfoTech.Common.Security.Md5(Pwd), openId);
        return i;
    }
}