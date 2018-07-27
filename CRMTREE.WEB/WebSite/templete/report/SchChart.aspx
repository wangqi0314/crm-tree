<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchChart.aspx.cs" Inherits="templete_report_SchChats" %>
<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="CrmTreetop" TagName="top"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/js/highcharts.js" type="text/javascript"></script>
    <script src="/js/highcharts-more.js" type="text/javascript"></script>
    <script src="/js/modules/exporting.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var SchTitle = $("#SchTitle").val();
            var X1 = $("#X1").val();
            var Y1 = $("#Y1").val();
            var Font = $("#Font").val();
           
            var xAxiss = $("#xAxis").val();
            var YBand = $("#YBand").val();
            var xNow = $("#xNow").val();
            var id_arr = $("#id_arr").val();
            xAxiss = eval(xAxiss);
            YBand = eval(YBand);
            xNow = eval(xNow);
            var ids = new Array();
            ids = id_arr.split(',');
            $('#Bar').highcharts({
                chart: {
                    type: 'line',
                    //borderColor: 'rgba(0, 0, 0, 0.3)',
                    //borderWidth: 2,
                    spacing : [10,20,0,20],
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: SchTitle,
                    style: { color: '#000000', font: 'bold 16px arial' }
                }, 
                legend: {
                    enabled:false
                },
                yAxis: {
                    title: {
                        text: X1
                    },
                    labels: {
                        enabled:false,
                        align: 'right'
                    },
                    gridLineWidth: 0,
                    plotBands : YBand
                },
                xAxis: {
                    title: {
                        text: Y1
                    },
                    type: 'datetime',
                    minRange:30*24*3600, //1 month
                    dateTimeLabelFormats: { // don't display the dummy year
                        month: '%e. %b',
                        year: '%b'
                    },
                    gridLineWidth: 1,
                    plotBands: xNow

                 },
                plotOptions: {
                    line: {
                        lineWidth : 20,
                        borderWidth: 0
                    },
                    series: {
                        type:'datetime',
                        cursor: 'pointer',
                        events: {
                            click: function (series) {
                                //alert('You just clicked the graph: id=' +ids[((this.index-this.index % 2)/2).toString()] );
                                parent.location.href = "/manage/campaign/campaign.aspx?CA=1&CG_Code="+ids[((this.index-this.index % 2)/2).toString()];
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{series.x}</span>',
                        pointFormat: '<span style="color:{series.color};padding:0">{series.name} </span>',
                        shared: true,
                        snap :5,
                        useHTML: true
                    },
                },
                series: eval(xAxiss),

                });
        });
    </script>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
        <input id="xAxis" type="hidden" value="<%=xAxis %>" />
        <input id="YBand" type="hidden" value="<%=YBand %>" />
        <input id="xNow" type="hidden" value="<%=xNow %>" />
        <input id="SchTitle" type="hidden" value="<%=SchTitle %>" />
        <input id="X1" type="hidden" value="<%=X1 %>" />
        <input id="Y1" type="hidden" value="<%=Y1 %>" />
        <input id="Font" type="hidden" value="<%=Font %>" />
        <input id="id_arr" type="hidden" value="<%=id_arr %>" />
        <div id="Bar" style="margin: 0px; padding: 0px; width: <%=MaxWidth%>px; height: <%=Maxheigth%>px">
        </div>
    </form>
</body>
</html>
