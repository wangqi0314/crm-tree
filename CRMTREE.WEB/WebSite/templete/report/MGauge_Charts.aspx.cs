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
    public int _val1 = 10;
    public int _val2 = 10;
    public string _Div = string.Empty;
    public string _YAxis = string.Empty;
    public string _Life = "var MyTimer = setInterval(function () {";
    public string T_Pos;



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
        T_Pos=(int.Parse(Request.QueryString["height"])*.50).ToString();
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

        int RP_Code = int.Parse(dt_param.Rows[0]["FG_RP_Code"].ToString());
        DataTable dt_Data = HC.getGaugeData(RP_Code, PR);
        if (dt_Data == null || dt_Data.Rows.Count == 0) { return; }


        string Lng = Interna ? "_EN" : "_CN";

        string[] _titles = dt_param.Rows[0]["FG_TITLE" + Lng].ToString().Split(new string[] { "||" }, StringSplitOptions.None);

        string[] _labels = dt_param.Rows[0]["FG_Text" + Lng].ToString().Split(new string[] { "||" }, StringSplitOptions.None);

        string[] _Data = dt_param.Rows[0]["FG_Attributes"].ToString().Split(new string[] { "||" }, StringSplitOptions.None);

        string _goal = "";
        string _max = "";
        string _val = "";
        int Row = 0;
        int _Width = (Convert.ToInt32(MaxWidth) / _Data.Length);
        int _Height = Convert.ToInt32(Maxheigth);// Convert.ToInt32((2.25 * _Width) / 3);
        for (int i = 0; i < _Data.Length; i++)
        {
            _Div = _Div + "<div id=\"cont_" + i + "\" style=\"width: " + _Width.ToString() + "px; height: " + _Height.ToString() + "px; float: left\"></div>";

            //Get the KPI Data
            _goal = "10";
            _val = "12";
            if (_Data[i] != null && _Data[i] != "")
            {
                Row = Convert.ToInt32(_Data[i]);
            }
            if (!string.IsNullOrEmpty(dt_Data.Rows[Row][0].ToString()))
            {
                _goal = dt_Data.Rows[Row][0].ToString();
            }
            if (!string.IsNullOrEmpty(dt_Data.Rows[Row][1].ToString()))
            {
                _val = dt_Data.Rows[Row][1].ToString();
            }
            else { _val = "0"; }

            _max = (float.Parse(_goal) * 1.5).ToString();

            _YAxis = _YAxis +
                        "$('#cont_" + i.ToString() + "').highcharts(Highcharts.merge(gaugeOptions, {" +
                        " yAxis: {" +
                            " min: 0," +
                            " max: " + _max + "," +
                            " plotBands: [{" +
                                " from: 0," +
                                " to: " + _goal + "," +
                                " color: '#DDDF0D'" + // yellow

                            " }, {" +
                                " from: " + _goal + "," +
                                " to: " + _max + "," +
                                " color: '#55BF3B'" + // green
                            " }]," +
                            " title: {" +
                //                               " text: '" + _titles[i] +
                               " text: '[" + dt_Data.Rows[Row][4] + "] " + dt_Data.Rows[Row][5] +
                            "' }" +
                        " }," +


                        " series: [{" +
                //                            " name: '"+_titles[i]+"',"+
                            " name: '" + dt_Data.Rows[Row][4] + " " + dt_Data.Rows[Row][5] + "'," +
                            " data: [" + _max + "]," +
                            " dataLabels: {"+
                                " format: '<div style=\"text-align:center\"><span style=\"font-size:25px;color:' +"+
                                    " ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black') + '\">{y}</span><br/>' +"+
                                       " '<span style=\"font-size:12px;color:silver\"> " + _labels[i] + " </span></div>'" +
                            " }," +
                            " tooltip: {"+
                                " valueSuffix: ' '"+
                            " }"+
                        " }]"+

                    " }));";

            _Life=_Life +"  $('#cont_"+i.ToString()+"').highcharts().series[0].points[0].update(eval("+_val+"));";
                
        }
        _Life = _Life + " clearInterval(MyTimer);}, 1000);";
        

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