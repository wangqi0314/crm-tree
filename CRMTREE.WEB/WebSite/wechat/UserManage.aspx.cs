using CRMTree.BLL;
using CRMTree.Model.Wechat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wechat_UserManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static MD_FansList GetAllFan()
    {
        BL_Wechat o = new BL_Wechat();
        MD_FansList F = new MD_FansList();
        F = o.GetAllFan();
        for (int i = 0; i < F.Fans.Count; i++)
        {
            F.Fans[i].WF_NickName_EC = HttpUtility.UrlEncode(F.Fans[i].WF_NickName);
        }
        return F;
    }
}