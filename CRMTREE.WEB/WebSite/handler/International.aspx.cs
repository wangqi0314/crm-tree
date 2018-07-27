using CRMTree.Model.Common;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_International : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 

    }
    /// <summary>
    /// 设置当前使用的语言
    /// </summary>
    /// <param name="Key"></param>
    /// <returns></returns>
    [WebMethod]
    public static int SetLanguage(int Key)
    {
        if (Key == 1)
        {
            /* 中文*/
            Language.SetLang_Cookies(EM_Language.zh_cn);
        }
        else if (Key == 2)
        {
            /* 英文*/
            Language.SetLang_Cookies(EM_Language.en_us);
        }
        return 0;
    }
}