using CRMTree.Public;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_Report_Run_file : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            //var _CG_Code = Request.QueryString["CG_Code"];
            //var _file_url = Request.QueryString["file_url"];
            //_file_url = Microsoft.JScript.GlobalObject.unescape(_file_url);
            //string _fileCount = Files.FileContext(HttpContext.Current.Request.PhysicalApplicationPath+"\\"+_file_url);
            //Response.Write(_fileCount);
        }
    }
}