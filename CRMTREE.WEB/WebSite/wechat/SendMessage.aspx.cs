using CRMTree.BLL;
using CRMTree.BLL.Wechat;
using CRMTree.Model;
using CRMTree.Model.Wechat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wechat_SendMessage : System.Web.UI.Page
{
    protected string name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            name = Request.QueryString["n"];
            //outStr = HttpUtility.UrlDecode(name);
        }
    }
    [WebMethod]
    public static object SendMess(string data)
    {
        data o = JsonConvert.DeserializeObject<data>(data);
        int i = wechatHandle.SendCustom_text(o.OpenId, o.context);
        if (i == 0)
        {
            BL_Wechat b_o = new BL_Wechat();
            b_o.AddReplyLog(new CT_Wechat_ReplyLog() { WR_WT_Openid = o.OpenId, WR_AU_Code = 1, WR_Content = o.context, WR_Type = 1 });
        }
        return i;
    }
    [WebMethod]
    public static MD_FansList GetAllFanContent(string data)
    {
        data d = JsonConvert.DeserializeObject<data>(data);
        BL_Wechat o = new BL_Wechat();
        MD_FansList F = o.GetAllFanContent(d.OpenId);
        for (int i = 0; i < F.Fans.Count; i++)
        {
            F.Fans[i].WF_NickName_EC = HttpUtility.UrlEncode(F.Fans[i].WF_NickName);
            F.Fans[i].WT_CreateTime_S = F.Fans[i].WT_CreateTime.ToString("MM/dd/yyyy mm:ss");
           
            if (F.Fans[i].AU_Code != 0)
            {
                F.Fans[i].WF_NickName = F.Fans[i].AU_Username;
                F.Fans[i].WF_HeadImgurl = "/images/DeK.jpg";
            }

        }
        return F;
    }
    private class data
    {
        public string context { get; set; }
        public string OpenId { get; set; }
    }
}