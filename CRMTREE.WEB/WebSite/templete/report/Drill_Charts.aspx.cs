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
using ShInfoTech.Common;
using CRMTree.Model.Reports;
using CRMTree.Model.Common;
using CRMTree.Model;


public partial class templete_report_C_L_P_Chats : BasePage
{
    public string MaxWidth = string.Empty;
    public string Maxheigth = string.Empty;
    public int _val1 = 10;
    public int _val2 = 10;
    public string _Div = string.Empty;
    public string _YAxis = string.Empty;
    public string _Life = "var MyTimer = setInterval(function () {";
    public string T_Pos;
    public string ChartTit = string.Empty;
    public string xAxis = string.Empty; 
    public string YDATA = string.Empty; 
    public string XTit = string.Empty; 
    public string YTit = string.Empty; 
    public string Font = string.Empty;  
    public string Format = string.Empty;  

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
        T_Pos = (int.Parse(Request.QueryString["height"]) * .50).ToString();
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
        string Lng = Interna ? "_EN" : "_CN";
        DataTable dt_Data;

        BL_HighCharts HC = new BL_HighCharts();
        DataTable dt_param = HC.getDrillParam(Code);
        if (dt_param == null || dt_param.Rows.Count == 0) { return; }
        int RP_Code = 0;
        for (int r = 0; r < dt_param.Rows.Count; r++)
        {
            int RP_Code1 = int.Parse(dt_param.Rows[r]["FD_RP_Code"].ToString());
            if (RP_Code != RP_Code1) 
            {
                RP_Code = RP_Code1;
            }
            dt_Data = HC.getDrillData(RP_Code, PR);
            if (dt_Data == null || dt_Data.Rows.Count == 0) { return; }

            string _title = HttpUtility.UrlDecode(RequestClass.GetString("Title"), Encoding.UTF8);
            if (!string.IsNullOrEmpty(_title))
            {
                ChartTit = _title;
            }
            else
            {
                ChartTit = "" + dt_param.Rows[0]["FD_TITLE" + Lng].ToString() + "";
            }
            int Level = int.Parse(dt_param.Rows[r]["FD_Level"].ToString());

            string[] Xp1 = dt_param.Rows[r]["FD_X" + Lng].ToString().Split(new string[] { "||" }, StringSplitOptions.None);
            XTit = ",xAxis: [";
            for (int i = 0; i < Xp1.Length; i++)
            {
                if (i>0) {XTit=XTit + ",";}
                string[] Xp2 = Xp1[i].Split(',');
                XTit = XTit + " {";
                XTit = XTit + " title:{text:'"+Xp2[0].ToString()+"'}";
                XTit = XTit + ", type: 'datetime', dateTimeLabelFormats: {month: '%e. %b',year: '%b' }";
                XTit = XTit + " ,labels:{" ;
                XTit = XTit + "  style: {fontSize: '" + Xp2[1].ToString() + "px' , fontFamily: 'Verdana, sans-serif'}";
                XTit = XTit + "}";
                XTit = XTit + "}]";
            }

            string[] Yp1 = dt_param.Rows[r]["FD_Y" + Lng].ToString().Split(new string[] { "||" }, StringSplitOptions.None);
            YTit = ",yAxis: [";
            for (int i = 0; i < Yp1.Length; i++)
            {
                if (i>0) {YTit=YTit + ",";}
                string[] Yp2 = Yp1[i].Split(',');
                YTit = YTit + " {";
                YTit = YTit + " title:{text:'" + Yp2[0].ToString()+"'}";
                YTit = YTit + " ,labels:{";
                YTit = YTit + "  style: {fontSize: '"+  Yp2[1].ToString() + "px' , fontFamily: 'Verdana, sans-serif'}" ;
                YTit = YTit + ",formatter: function () {return "+Yp2[2].ToString()+" + this.value;}" ;
                YTit = YTit + "}";
                if (i > 0) { YTit = YTit + ",opposite: true"; }
                YTit = YTit + "}";
            }
            YTit = YTit + "]";

            string[] Dp1 = dt_param.Rows[r]["FD_Data"].ToString().Split(new string[] { "||" }, StringSplitOptions.None);
            for (int i = 0; i < Dp1.Length; i++)
            {
                string Cur_X = "";
                if (Level == 1)
                {
                    YDATA = YDATA + " series: [";
                }
                else
                {
                    YDATA = YDATA + " drilldown: { series: [";
                }
                //       string[] Xp2 = Xp1[i].Split(',');
                string[] Yp2 = Yp1[i].Split(',');
                if (i > 0) { YDATA = YDATA + "},"; }
                string[] Dp2 = Dp1[i].Split(',');
    //            string[] Yp2 = Yp1[int.Parse(Xp2[4])].Split(',');//this needs to be fixed
                int XCol = int.Parse(Dp2[0]);
                int YCol = int.Parse(Dp2[1]);
                int IDCol = int.Parse(Dp2[2]);
                int DCol = int.Parse(Dp2[3]);
                int k = 0;
                if (Level == 1)
                {
                    YDATA = YDATA + "{type:'" + Dp2[4] + "'";
                    YDATA = YDATA + ", borderWidth: 0 ";
                    //              YDATA = YDATA + ", tooltip: {valueSuffix: "+Yp2[2]+"}";
                    YDATA = YDATA + ", title: {text:'My Title1" + Dp2[5]+"'}";
                    YDATA = YDATA + ", subtitle: {text:'My SubTitle1" + Dp2[5] + "'}";
                    YDATA = YDATA + ", xAxis:" + Dp2[5];
                    YDATA = YDATA + ", yAxis:" + Dp2[6];
                    YDATA = YDATA + ", name:'" + dt_Data.Columns[YCol].ColumnName.ToString() + "'";
                    if (Dp2[7].Trim() != "") { YDATA = YDATA + ", pointPadding:" + Dp2[7]; }
                    if (Dp2[8].Trim() != "") { YDATA = YDATA + ", pointPlacement:" + Dp2[8]; }
                    if (Dp2[9].Trim() != "") { YDATA = YDATA + ", color:'" + Dp2[9] + "'"; }
                    YDATA = YDATA + ", data: [";
                }
                for (int j = 0; j < dt_Data.Rows.Count; j++)
                {
                    if (k > 0) { YDATA = YDATA + ","; }
                    if (!string.IsNullOrEmpty(dt_Data.Rows[j][XCol].ToString()))
                    {
                        k++;
                        if ( Level > 1 && Cur_X != dt_Data.Rows[j][IDCol].ToString())
                        {
                            if (Cur_X != "") { YDATA = YDATA + "]},"; }
                            Cur_X = dt_Data.Rows[j][IDCol].ToString();
                            YDATA = YDATA + "{type:'" + Dp2[4] + "'";
                            YDATA = YDATA + ", borderWidth: 0 ";
                            YDATA = YDATA + ", title: {text:'My Title1" + Dp2[5] + "'}";
                            YDATA = YDATA + ", subtitle: {text:'My SubTitle1" + Dp2[5] + "'}";
                            //              YDATA = YDATA + ", tooltip: {valueSuffix: "+Yp2[2]+"}";
                            //YDATA = YDATA + ", xAxis:" + Dp2[5];
                            //YDATA = YDATA + ", yAxis:" + Dp2[6];
                            //YDATA = YDATA + ", name:'" + dt_Data.Columns[YCol].ColumnName.ToString() + "'";
                            if (Dp2[7].Trim() != "") { YDATA = YDATA + ", pointPadding:" + Dp2[7]; }
                            if (Dp2[8].Trim() != "") { YDATA = YDATA + ", pointPlacement:" + Dp2[8]; }
                            if (Dp2[9].Trim() != "") { YDATA = YDATA + ", color:'" + Dp2[9] + "'"; }
                            YDATA = YDATA + ", name:'" + dt_Data.Columns[YCol].ColumnName.ToString() + "'";
                            YDATA = YDATA + ", id:'" + dt_Data.Rows[j][IDCol].ToString() + "'";
                            YDATA = YDATA + ", data: [";

                        }
                       YDATA = YDATA + "{y:" + dt_Data.Rows[j][YCol].ToString() + "";
                       YDATA = YDATA + ", name:'" + dt_Data.Rows[j][XCol].ToString() + "'";
                       YDATA = YDATA + ", drilldown:'" + dt_Data.Rows[j][DCol].ToString() + "'}";
                    }
                    else { k = 0; }
                }
                YDATA = YDATA + "]";

            }
            if (Level == 1)
            {
                YDATA = YDATA + "},],";
            }
            else
            {
                YDATA = YDATA + "},] },";
            }
        }
        
    }
    private string Fill_Data(DataTable dt_Bar, int n)
    {
        if (dt_Bar == null && dt_Bar.Rows.Count == 0)
        {
            return "";
        }
        StringBuilder SB = new StringBuilder();
        SB.Append(" [");
        for (int i = 0; i < dt_Bar.Rows.Count; i++)
        {
            if (i == dt_Bar.Rows.Count - 1)
            {
                SB.Append("'" + dt_Bar.Rows[i][0] + "'");
            }
            else
            {
                SB.Append("'" + dt_Bar.Rows[i][0] + "',");
            }
        }
        SB.Append("]");
        return SB.ToString();
    }
    private string Fill_Data(DataTable dt_Bar)
    {
        if (dt_Bar == null && dt_Bar.Rows.Count == 0)
        {
            return "";
        }
        for (int i = 0; i < dt_Bar.Rows.Count; i++)
        {
            for (int j = 0; j < dt_Bar.Columns.Count; j++)
            {
                if (string.IsNullOrEmpty(dt_Bar.Rows[i][j].ToString()))
                {
                    dt_Bar.Rows[i][j] = 0;
                }
            }
        }
        StringBuilder SB = new StringBuilder();
        SB.Append("[");
        for (int i = 1; i < dt_Bar.Columns.Count; i++)
        {
            SB.Append("{");
            SB.Append("name: '" + dt_Bar.Columns[i].Caption + "',");
            SB.Append("data: [");
            for (int j = 0; j < dt_Bar.Rows.Count; j++)
            {

                if (j == dt_Bar.Rows.Count - 1)
                {
                    SB.Append("" + dt_Bar.Rows[j][i] + "");
                }
                else
                {
                    SB.Append("" + dt_Bar.Rows[j][i] + ",");
                }

            }
            SB.Append("]");
            if (i == dt_Bar.Columns.Count - 1)
            {
                SB.Append("}");
            }
            else
            {
                SB.Append("},");
            }
        }
        SB.Append("]");
        return SB.ToString();
    }
     private string ConvUTC(DateTime d1)
    {
        return d1.Year.ToString() + "," + (d1.Month - 1).ToString() + "," + d1.Day.ToString();
    }
    private string ConvS_UTC(string d1)
    {
        if (d1 != null && d1 != "")
        {
            return ConvUTC(Convert.ToDateTime(d1));
        }
        else
        {
            return "";
        }
    }
}