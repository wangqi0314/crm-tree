using CRMTree.BLL;
using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Public;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class templete_report_PieChats : System.Web.UI.Page
{
    public string MaxWidth = string.Empty;
    public string Maxheigth = string.Empty;
    public string PieData = string.Empty;
    public string PieTitle = string.Empty;
    public string Legend = string.Empty;
    public int RP_Code = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setQres();
            setPieData();
            setPieTitle();
        }
    }
    private void setQres()
    {
        MaxWidth = (int.Parse(Request.QueryString["width"].ToString()) - 50).ToString();
        Maxheigth = (int.Parse(Request.QueryString["height"].ToString()) - 0).ToString();
        //        te = @"[{
        //                type: 'pie',
        //                name: 'Browser share',
        //                data: [
        //                    ['a', 30], 
        //                    ['b', 40],
        //                    {   
        //                        name: 'c',
        //                        y: 10, 
        //                        sliced: true,
        //                        selected: true
        //                    },
        //                    ['d', 20]
        //                ]
        //            }]";
    }
    private void setPieData()
    {
        int MF_FL_FB_Code = int.Parse(Request.QueryString["MF_FL_FB_Code"].ToString());
        BL_HighCharts HC = new BL_HighCharts();
        DataTable dt = HC.getPieData(MF_FL_FB_Code, out RP_Code, 0);
        StringBuilder SB = new StringBuilder();
        SB.Append("[{type: 'pie',");
        SB.Append("name: '',");
        SB.Append("data: [");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == dt.Rows.Count - 1)
            {
                SB.Append("{ ");
                SB.Append("name: '" + dt.Rows[i][0] + "', ");
                SB.Append(" y:  " + dt.Rows[i][1] + ", ");
                SB.Append(" sliced: true, ");
                SB.Append(" selected: true ");
                SB.Append(" } ");
            }
            else
            {
                SB.Append("['" + dt.Rows[i][0] + "', " + dt.Rows[i][1] + "],");
            }
        }
        SB.Append("]");
        SB.Append("}]");
        PieData = SB.ToString();
    }
    private void setPieTitle()
    {
        int RP_Code = int.Parse(Request.QueryString["MF_FL_FB_Code"].ToString());
        BL_HighCharts HC = new BL_HighCharts();
        DataTable dt = HC.getPieTitleII(RP_Code);
        if (dt != null )
        {
            if (dt.Rows.Count != 0)
            {
                string _title = HttpUtility.UrlDecode(RequestClass.GetString("Title"), Encoding.UTF8);
                if (!string.IsNullOrEmpty(_title))
                {
                    PieTitle = _title;
                }
                string[] _Param = dt.Rows[0]["FP_Parameters"].ToString().Split(new string[] { "||" }, StringSplitOptions.None);
                if (_Param.Length > 0)
                {
                    if (_Param[0] == "0")  //No Legend
                    {
                        Legend = "enabled: false,";
                    }
                    else if (_Param[0] == "1") //Top Right Legend
                    {
                        Legend = "layout:'horizontal', align: 'right', verticalAlign: 'top', x: -25, y: -10,";
                    }
                    else if (_Param[0] == "2") //Left Legend
                    {
                        Legend = "layout: 'vertical', align: 'left', verticalAlign: 'top', x: 0, y: 0,";
                    }
                    else //default Parameters
                    {
                        Legend = "layout:'horizontal', align: 'right', verticalAlign: 'top', x: -25, y: -10,";
                    }
                }
                if (_Param.Length > 1)
                {
                    if (_Param[1] == "0")  //No Title
                    {
                        PieTitle = "";
                    }
                    else if (_Param[1] == "1") //Use the FP_Title field
                    {
                        PieTitle = dt.Rows[0]["FP_Title_CN"].ToString();
                    }
                }

            }
        }
    }
}