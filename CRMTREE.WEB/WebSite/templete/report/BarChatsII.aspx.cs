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

public partial class templete_report_BarChats : System.Web.UI.Page
{
    public string MaxWidth = string.Empty;
    public string Maxheigth = string.Empty;
    public string xAxis = string.Empty; //Bar数据
    public string YDATA = string.Empty; //Bar标题
    public string BarTitle = string.Empty; //主标题
    public string X1 = string.Empty; //X标题
    public string Y1 = string.Empty; //Y标题
    public string Font = string.Empty;  //字体大小
    public string Format = string.Empty;  //字体大小
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setQres();
            setPieData();
            setBarTitle();
        }
    }
    private void setQres()
    {
        MaxWidth = Request.QueryString["width"].ToString();
        Maxheigth = Request.QueryString["height"].ToString();
    }
    private void setPieData()
    {
        string Code = Request.QueryString["MF_FL_FB_Code"].ToString();

        int PR = 0;
        if (null != Request.QueryString["PR"])
        {
            PR = int.Parse(Request.QueryString["PR"].ToString());
        }

        if (!string.IsNullOrEmpty(Code))
        {
            int RP_Code = int.Parse(Code);
            BL_HighCharts Hichart = new BL_HighCharts();
            DataTable dt_Bar = Hichart.getColumnData(RP_Code,PR);
            if (dt_Bar != null && dt_Bar.Rows.Count != 0)
            {
                YDATA = Fill_Data(dt_Bar, 0);
                xAxis = Fill_Data(dt_Bar);
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
    private void setBarTitle()
    {
        int RP_Code = int.Parse(Request.QueryString["MF_FL_FB_Code"].ToString());
        BL_HighCharts HC = new BL_HighCharts();
        DataTable dt = HC.getBarTitle(RP_Code);
        if (dt != null || dt.Rows.Count != 0)
        {
            BarTitle = dt.Rows[0]["FB_TITLE"].ToString();
            X1 = dt.Rows[0]["FB_X1_LABEL"].ToString();
            Y1 = dt.Rows[0]["FB_Y1_LABEL"].ToString();
            Font = dt.Rows[0]["FB_X1_Font"].ToString();
            if (string.IsNullOrEmpty(dt.Rows[0]["FB_X1_Font"].ToString()))
            {
                Font = "8";
            }
            if (string.IsNullOrEmpty(dt.Rows[0]["FB_X1_Format"].ToString()))
            {
                Format = "￥";
            }
            else if (dt.Rows[0]["FB_X1_Format"].ToString() == "1")
            {
                Format = "$";
            }
            else 
            {
                Format = "";
            }
        }
        string _title = HttpUtility.UrlDecode(RequestClass.GetString("Title"), Encoding.UTF8);
        if (!string.IsNullOrEmpty(_title))
        {
            BarTitle = _title;
        }
    }
}