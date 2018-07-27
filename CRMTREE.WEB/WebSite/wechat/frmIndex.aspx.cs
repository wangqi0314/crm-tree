using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WechatWeb.App_Code;
using System.Web.Security;
using System.Security.Cryptography;
using System.Net;
using CRMTree.BLL.Wechat;
using System.Xml.Serialization;
using CRMTree.Model;
using CRMTree.Model.Common;
using ShInfoTech.Common;

public partial class wechat_frmIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Wechatcontrol _w = new Wechatcontrol(HttpContext.Current);
        }
        catch (Exception ex)
        {
            B_W_Exception.AddExcep("微信Page_Load", "提供的微信接口初始化失败" + ex.Message, ex.StackTrace);
        }
    }

}
/// <summary>
/// 微信控制器
/// </summary>
public class Wechatcontrol
{
    const string Token = "shinfotechdaek";
    private HttpContext _http = null;
    public Wechatcontrol(HttpContext http)
    {
        _http = http;
        Initialise();
    }
    /// <summary>
    /// 初始化接口控制器
    /// </summary>
    public void Initialise()
    {
        string type = string.Empty;
        if (_http == null)
        {
            _http = HttpContext.Current;
        }
        type = _http.Request.HttpMethod.ToLower();

        if (type == "get")
        {
            isValid();
        }
        else if (type == "post")
        {
            EventNavigation();
        }
    }
    public void EventNavigation()
    {
        xml x = GetXMLObject();
        switch (x.MsgType)
        {
            case "event":
                EventHandle(x);
                break;
            case "text":
                TextHandle(x);
                break;
            //case "location":
            //    LocationMsg(rootElement);
            //    break;
            //case "image":
            //    ImageMsg(rootElement);
            //    break;
            //case "link":
            //    LinkMsg(rootElement);
            //    break;
            //case "news":
            //    NewsMsg(rootElement);
            //    break;
            //case "MusicMsg":
            //    NewsMsg(rootElement);
            //    break;
            //case "voice":
            //    VoiceMsg(rootElement);
            //    break;
            //default:
            //    WelCome(rootElement);
            //    break;
        }
    }
    #region 事件
    /// <summary>
    /// 事件处理
    /// </summary>
    /// <param name="x"></param>
    public void EventHandle(xml x)
    {
        try
        {
            if (x.Event.ToLower().Trim() == "subscribe")
            {
                EventHandle_subscribe(x);
            }
            else if (x.Event.ToLower().Trim() == "unsubscribe")
            {
                EventHandle_unSubscribe(x);
            }
            else if (x.Event.ToLower().Trim() == "click")
            {
                EventHandle_CLICK(x);
            }
        }
        catch (Exception e)
        {
            B_W_Exception.AddExcep(x.MsgType, x.Event, e.Message);
        }
    }
    /// <summary>
    /// 关注事件处理
    /// </summary>
    /// <param name="x"></param>
    public void EventHandle_subscribe(xml x)
    {
        B_W_Fans _b_fan = new B_W_Fans();
        CT_Wechat_Fan o = wechatHandle.GetFans(x.FromUserName);
        int i = _b_fan.AddFans(o);
        wechatHandle.SendCustom_text(x.FromUserName, "亲爱的用户，欢迎使用大E库微信");
    }
    /// <summary>
    /// 取消事件处理
    /// </summary>
    /// <param name="x"></param>
    public void EventHandle_unSubscribe(xml x)
    {
        B_W_Fans _b_fan = new B_W_Fans();
        _b_fan.UpdateFans(x.FromUserName, 2);
    }
    /// <summary>
    /// 菜单点击事件
    /// </summary>
    /// <param name="x"></param>
    public void EventHandle_CLICK(xml x)
    {
        try
        {
            if (x.EventKey == "V1001_01") { EventHandle_CLICK_V1001_01(x); }
            else if (x.EventKey == "V1001_02") { EventHandle_CLICK_V1001_02(x); }
            else if (x.EventKey == "Q1001_05") { EventHandle_CLICK_Q1001_05(x); }
        }
        catch (Exception e)
        {
            B_W_Exception.AddExcep(x.MsgType, x.Event, e.Message);
        }
    }
    private void EventHandle_CLICK_V1001_01(xml x)
    {
        string text = "你好，\n推荐的活动我们会尽快发送给你";
        string senCon = wechatHandle.SendPassive_text(x.FromUserName, x.ToUserName, text);
        _http.Response.Write(senCon);
    }
    private void EventHandle_CLICK_V1001_02(xml x)
    {
        string Title = "宝马7系现金优惠18万 购车送大礼包";
        //string Title = string.Empty;
        string Description = "宝马7系现金优惠18万 购车送大礼包：";
        string PicUrl = WebConfig.GetAppSettingString("Wechat_Web_Default_Image_Url");
        //string PicUrl = string.Empty;
        string url = WebConfig.GetAppSettingString("Wechat_Web_Url") + "upload/1410101043493425.html";
        string senCon = wechatHandle.SendPassive_image_text(x.FromUserName, x.ToUserName, Title, Description, PicUrl, url);
        _http.Response.Write(senCon);
        CRMTree.BLL.BL_Wechat.SendCustom_News(x.FromUserName, 51);
    }
    private void EventHandle_CLICK_Q1001_05(xml x)
    {
        string text = B_W_TextMessage.GetMessage();
        string senCon = wechatHandle.SendPassive_text(x.FromUserName, x.ToUserName, text);
        _http.Response.Write(senCon);
    }
    #endregion
    #region 文本
    public void TextHandle(xml x)
    {
        try
        {
            WechatCommunicator _C = new WechatCommunicator(x, CommunicatorKey.Q1001_05);
        }
        catch (Exception e)
        {
            B_W_Exception.AddExcep(x.MsgType, "客服系统异样", e.Message);
        }
    }
    #endregion
    #region 对于接口请求数据转换为 xml 对象
    private xml GetXMLObject()
    {
        string postStr = GetPostString(_http);
        StringReader Reader = new StringReader(postStr);
        XmlSerializer serializer = new XmlSerializer(typeof(xml));
        xml o = serializer.Deserialize(Reader) as xml;
        B_W_Online.AddOnline(o.FromUserName, wechatHandle.GetLocalTime(o.CreateTime), null);
        B_W_Exception.AddLog(postStr, o.FromUserName);
        return o;
    }
    private string GetPostString(HttpContext http)
    {
        Stream stream = http.Request.InputStream;
        byte[] b = new byte[stream.Length];
        stream.Read(b, 0, (int)stream.Length);
        return Encoding.UTF8.GetString(b);
    }
    #endregion
    #region 验证
    private void isValid()
    {
        string echoStr = _http.Request.QueryString["echoStr"];
        if (CheckSignature())
        {
            if (!string.IsNullOrEmpty(echoStr))
            {
                _http.Response.Write(echoStr);
                _http.Response.End();
            }
        }
    }
    /// <summary>
    /// 验证微信签名
    /// </summary>
    /// * 将token、timestamp、nonce三个参数进行字典序排序
    /// * 将三个参数字符串拼接成一个字符串进行sha1加密
    /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
    /// <returns></returns>
    private bool CheckSignature()
    {
        string signature = _http.Request.QueryString["signature"];
        string timestamp = _http.Request.QueryString["timestamp"];
        string nonce = _http.Request.QueryString["nonce"];
        string[] ArrTmp = { Token, timestamp, nonce };
        Array.Sort(ArrTmp);     //字典排序
        string tmpStr = string.Join("", ArrTmp);

        tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
        //MD5 md5Hash = MD5.Create();
        //tmpStr = GetMd5Hash(md5Hash, tmpStr); 
        tmpStr = tmpStr.ToLower();
        if (tmpStr == signature)
        {
            return true;
        }
        else
        {
            return true;
        }
    }

    private string GetMd5Hash(MD5 md5Hash, string input)
    {
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("SHA1"));
        }
        return sBuilder.ToString();
    }
    #endregion
}


