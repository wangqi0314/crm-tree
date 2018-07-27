using CRMTree.BLL.Wechat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// 
/// </summary>
public partial class wechat_02_MainService : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        wechatAjaxHandle2 _ajax = new wechatAjaxHandle2(HttpContext.Current);
        var _dataInfo = _ajax.toResponse();
        Response.Write(_dataInfo);
    }
}