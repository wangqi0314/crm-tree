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
    public string xAxis = string.Empty; //Bar数据
    public string YDATA = string.Empty; //Bar标题
    public string ChartTit = string.Empty; //主标题
    public string XTit = string.Empty; //X标题
    public string YTit = string.Empty; //Y标题
    public string Font = string.Empty;  //字体大小
    public string Format = string.Empty;  //字体大小
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
        MaxWidth = (int.Parse(Request.QueryString["width"]) * .97).ToString();
        Maxheigth = (int.Parse(Request.QueryString["height"]) * .92).ToString();
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
        DataTable dt_param = HC.getMultiParam(Code);
        if (dt_param == null || dt_param.Rows.Count == 0) { return; }

        string Lng = Interna ? "_EN" : "_CN";
        
        int RP_Code = int.Parse(dt_param.Rows[0]["FM_RP_Code"].ToString());
        DataTable dt_Data = HC.getMultiData(RP_Code, PR);
        if (dt_Data == null || dt_Data.Rows.Count == 0) { return; }

        string _title = HttpUtility.UrlDecode(RequestClass.GetString("Title"), Encoding.UTF8);
        if (!string.IsNullOrEmpty(_title))
        {
            ChartTit = _title;
        }
        else
        {
            ChartTit = ""+dt_param.Rows[0]["FM_TITLE"+Lng].ToString()+"";
        }

        string[] Xp1 = dt_param.Rows[0]["FM_X" + Lng].ToString().Split(new string[] { "||" }, StringSplitOptions.None);
        XTit="[";
        for (int i = 0; i < Xp1.Length; i++)
        {
            if (i>0) {XTit=XTit + ",";}
            string[] Xp2 = Xp1[i].Split(',');
            XTit = XTit + "{";
            XTit = XTit + " title:{text:'"+Xp2[0].ToString()+"'}";
            XTit = XTit + ", type: 'datetime', dateTimeLabelFormats: {month: '%e. %b',year: '%b' }";
            XTit = XTit + " ,labels:{" ;
            XTit = XTit + "  style: {fontSize: '" + Xp2[1].ToString() + "px' , fontFamily: 'Verdana, sans-serif'}";
            XTit = XTit + "}";
            XTit = XTit + "}]";
        }

        string[] Yp1 = dt_param.Rows[0]["FM_Y" + Lng].ToString().Split(new string[] { "||" }, StringSplitOptions.None);
        YTit="[";
        for (int i = 0; i < Yp1.Length; i++)
        {
            if (i>0) {YTit=YTit + ",";}
            string[] Yp2 = Yp1[i].Split(',');
            YTit = YTit + "{";
            YTit = YTit + " title:{text:'" + Yp2[0].ToString()+"'}";
            YTit = YTit + " ,labels:{";
            YTit = YTit + "  style: {fontSize: '"+  Yp2[1].ToString() + "px' , fontFamily: 'Verdana, sans-serif'}" ;
            YTit = YTit + ",formatter: function () {return "+Yp2[2].ToString()+" + this.value;}" ;
            YTit = YTit + "}";
            if (i > 0) { YTit = YTit + ",opposite: true"; }
            YTit = YTit + "}";
        }
        YTit = YTit + "]";

        string[] Dp1 = dt_param.Rows[0]["FM_Data"].ToString().Split(new string[] { "||" }, StringSplitOptions.None);
        YDATA = "[";
        for (int i = 0; i < Dp1.Length; i++)
        {
            if (i > 0) { YDATA = YDATA + "},"; }
            string[] Xp2 = Dp1[i].Split(',');
            string[] Yp2 = Yp1[int.Parse(Xp2[2])].Split(',');
            int Col = int.Parse(Xp2[0]);
            int k = 0;
            YDATA = YDATA + "{type:'" + Xp2[1] + "'";
            YDATA = YDATA + ", borderWidth: 0 ";
            YDATA = YDATA + ", tooltip: {valueSuffix: "+Yp2[2]+"}";
            YDATA = YDATA + ", yAxis:" + Xp2[2];
            YDATA = YDATA + ", name:'" + dt_Data.Columns[Col].ColumnName.ToString() + "'";
            if (Xp2[3].Trim() != "") { YDATA = YDATA + ", pointPadding:" + Xp2[3]; }
            if (Xp2[4].Trim() != "") { YDATA = YDATA + ", pointPlacement:" + Xp2[4]; }
            if (Xp2[5].Trim() != "") { YDATA = YDATA + ", color:'" + Xp2[5] +"'"; }
            YDATA = YDATA + ", data: [";
            for (int j = 0; j < dt_Data.Rows.Count; j++)
            {
                if (k > 0) { YDATA = YDATA + ","; }
                if (!string.IsNullOrEmpty(dt_Data.Rows[j][Col].ToString()))
                {
                    k++;
                    YDATA = YDATA + "[Date.UTC(" + ConvS_UTC(dt_Data.Rows[j][0].ToString()) + ")," + dt_Data.Rows[j][Col].ToString() + "]";
                }
                else { k = 0; }
            }
            YDATA = YDATA + "]";

        }
        YDATA = YDATA + "},]";

        
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