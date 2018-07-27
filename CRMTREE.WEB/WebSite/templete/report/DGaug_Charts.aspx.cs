using CRMTree.Public;
using CRMTree.BLL;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.IO;
using System.Text;
using Shinfotech.Tools;
using ShInfoTech.Common;
using CRMTree.Model.Reports;
using CRMTree.Model.Common;
using CRMTree.Model;
public partial class templete_report_Gauge_Chats : BasePage
{
    public string MaxWidth = string.Empty;
    public string Maxheigth = string.Empty;
    public string _title1 = string.Empty;
    public string _title2 = string.Empty;
    public string _label1 = string.Empty;
    public string _label2 = string.Empty; 
    public int _max1 = 50;
    public int _goal1 = 30;
    public int _val1 = 10;
    public int _max2 = 30;
    public int _goal2 = 20;
    public int _val2 = 10;


    public string ChartTit = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setParam();
            GetData();
        }
    }
    private void setParam()
    {
        MaxWidth = Request.QueryString["width"].ToString();
        Maxheigth = Request.QueryString["height"].ToString();
    }
    private void GetData()
    {
        if (string.IsNullOrEmpty(Request.QueryString["MF_FL_FB_Code"].ToString())) { return; }
        int Code = int.Parse(Request.QueryString["MF_FL_FB_Code"]);

        int PR = 0;
        if (null != Request.QueryString["PR"])
        {
            PR = int.Parse(Request.QueryString["PR"].ToString());
        }

        BL_HighCharts HC = new BL_HighCharts();
        DataTable dt_param = HC.getDGaugParam(Code);
        if (dt_param == null || dt_param.Rows.Count == 0) { return; }

        string Lng = Interna ? "_EN" : "_CN";

        string[] Xp1 = dt_param.Rows[0]["FG_TITLE" + Lng].ToString().Split(new string[] { "||" }, StringSplitOptions.None);
        _title1 = Xp1[0];
        _title2 = Xp1[1];

        Xp1 = dt_param.Rows[0]["FG_Text" + Lng].ToString().Split(new string[] { "||" }, StringSplitOptions.None);
        _label1 = Xp1[0];
        _label2 = Xp1[1];

        //int RP_Code = int.Parse(dt_param.Rows[0]["FG_RP_Code"].ToString());
        //DataTable dt_Data = HC.getMultiData(RP_Code, PR);
        //if (dt_Data == null || dt_Data.Rows.Count == 0) { return; }

        //for (int j = 0; j < dt_Data.Rows.Count; j++)
        //{
        //    if (k > 0) { YDATA = YDATA + ","; }
        //    if (!string.IsNullOrEmpty(dt_Data.Rows[j][Col].ToString()))
        //    {
        //        k++;
        //        YDATA = YDATA + "[Date.UTC(" + ConvS_UTC(dt_Data.Rows[j][0].ToString()) + ")," + dt_Data.Rows[j][Col].ToString() + "]";
        //    }
        //    else { k = 0; }
        //}


    }
 }