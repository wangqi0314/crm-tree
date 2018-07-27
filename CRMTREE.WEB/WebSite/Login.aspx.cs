using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShInfoTech.Common;
using CRMTree.Public;
using System.Web.Services;
using System.Threading;
using System.Globalization;
public partial class Login : System.Web.UI.Page
{
    protected string currentTime = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        currentTime = DateTime.Now.ToString("yyyy-MM-dd");
        if (!IsPostBack)
        {
            Language.SetLang();
        }
    }

}