using CRMTree.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class templete_report_SchChats : System.Web.UI.Page
{
    public string MaxWidth = string.Empty;
    public string Maxheigth = string.Empty;
    public string xAxis = string.Empty; //Bar数据
    public string YBand = string.Empty; //Bar标题
    public string xNow = string.Empty; //Bar标题
    public string SchTitle = string.Empty; //主标题
    public string X1 = string.Empty; //X标题
    public string Y1 = string.Empty; //Y标题
    public string Font = string.Empty;  //字体大小
    public string Format = string.Empty;  //字体大小
    public string id_arr = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setQres();
            setSchData();
            setSchTitle();
        }
    }
    private void setQres()
    {
        MaxWidth = Request.QueryString["width"].ToString();
        Maxheigth = Request.QueryString["height"].ToString();
    }
    private void setSchData()
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
            DataTable dt_Sch = Hichart.getSchData(RP_Code,PR);
            if (dt_Sch != null && dt_Sch.Rows.Count != 0)
            {
                YBand = Fill_Data(dt_Sch, 0);
                xAxis = Fill_Data(dt_Sch);
                xNow = "[{ from: Date.UTC(" + ConvUTC(DateTime.Today.AddDays(-1)) + "), to:Date.UTC( " + ConvUTC(DateTime.Today) + "), color: 'rgba(185,8,37,0.5)', label: { text: '" + Resources.CRMTREEResource.SchedNow + "', style: { color: 'black' }}}]";
            }
        }
    }
    private string Fill_Data(DataTable dt_Sch, int n)
    {
        if (dt_Sch == null && dt_Sch.Rows.Count == 0)
        {
            return "";
        }
        StringBuilder SB = new StringBuilder();
        SB.Append(" [");
        for (int i = 0; i < dt_Sch.Rows.Count; i++)
        {
            SB.Append(" { from:");
            SB.Append((i -.2).ToString() + ", to:" + (i + 0.8).ToString());
            if(i%2==0){
               SB.Append(", color :'rgba(68, 170, 213, 0.1)',");
            }
            else{
                SB.Append(", color :'rgba(0, 0, 0, 0.1)',");
            }
            SB.Append("label:{text:'<a>" + dt_Sch.Rows[i][1] + "</A>', style:{color:'#606060',fontWeight:'bold'}}}");
           if (i != dt_Sch.Rows.Count - 1)
            {
                SB.Append(",");
            }
        }
        SB.Append("]");
        return SB.ToString();
    }
    private string Fill_Data(DataTable dt_Sch)
    {
        if (dt_Sch == null && dt_Sch.Rows.Count == 0)
        {
            return "";
        }

        StringBuilder SB = new StringBuilder();
        id_arr = "";
        SB.Append("[");
        for (int j = 0; j < dt_Sch.Rows.Count; j++)
        {
            id_arr=id_arr+ dt_Sch.Rows[j][0].ToString()+"," ;
            int i=3;
            if (!string.IsNullOrEmpty(dt_Sch.Rows[j][i].ToString()))
            {
                SB.Append("{id:'" + dt_Sch.Rows[j][0] + "',name:'" + dt_Sch.Rows[j][1] + Resources.CRMTREEResource.SchedAnnounce+ " ',  color :'rgba(243, 83, 23, 0.5)', data: [");
                SB.Append("[Date.UTC(" + ConvS_UTC(dt_Sch.Rows[j][i].ToString()) + ")," + (j).ToString() + "],");
                SB.Append("[Date.UTC(" + ConvS_UTC(dt_Sch.Rows[j][i + 1].ToString()) + ")," + (j).ToString() + "]]},");
            };
            i=5;
            if (!string.IsNullOrEmpty(dt_Sch.Rows[j][i].ToString()))
            {
                SB.Append("{id:'" + dt_Sch.Rows[j][0] + "',name:'" + dt_Sch.Rows[j][1] +  Resources.CRMTREEResource.SchedEvent +" ',  color :'rgba(104,132,0,0.5)', data: [");
                SB.Append("[Date.UTC(" + ConvS_UTC(dt_Sch.Rows[j][i].ToString()) + ")," + (j).ToString() + "],");
                SB.Append("[Date.UTC(" + ConvS_UTC(dt_Sch.Rows[j][i + 1].ToString()) + ")," + (j).ToString() + "]]}");
            };
            if (j != dt_Sch.Rows.Count - 1)
            {
                SB.Append(",");
            }
        }
        SB.Append("]");
        return SB.ToString();
    }
    private void setSchTitle()
    {
        int RP_Code = int.Parse(Request.QueryString["MF_FL_FB_Code"].ToString());
        BL_HighCharts HC = new BL_HighCharts();
        DataTable dt = HC.getSchTitle(RP_Code);
        if (dt != null || dt.Rows.Count != 0)
        {
            SchTitle = dt.Rows[0]["FS_TITLE"].ToString();
            X1 = dt.Rows[0]["FS_X1_LABEL"].ToString();
            Y1 = dt.Rows[0]["FS_Y1_LABEL"].ToString();
            Font = dt.Rows[0]["FS_X1_Font"].ToString();
            if (string.IsNullOrEmpty(dt.Rows[0]["FS_X1_Font"].ToString()))
            {
                Font = "8";
            }
        }
    }
    private string ConvUTC(DateTime d1)
    {
        return d1.Year.ToString() + "," + (d1.Month - 1).ToString() + "," + d1.Day.ToString();
    }
    private string ConvS_UTC(string d1)
    {
        if (d1 !=null && d1!="" ) {
            return ConvUTC(Convert.ToDateTime(d1));
        } else{
            return "";
        }
    }
}