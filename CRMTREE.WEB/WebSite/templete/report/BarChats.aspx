<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarChats.aspx.cs" Inherits="templete_report_BarChats" %>

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
            var BarTitle = $("#BarTitle").val();
            var X1 = $("#X1").val();
            var Y1 = $("#Y1").val();
            var Font = $("#Font").val();
            var Format = $("#Format").val();

            var xAxiss = $("#xAxis").val();
            var YDATA = $("#YDATA").val();
            //$.parseJSON(xAxiss);
            xAxiss = eval(xAxiss);
            YDATA = eval(YDATA);
            $('#Bar').highcharts({
                chart: {
                    type: 'column'
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: BarTitle,
                    style: { color: '#000000', font: 'bold 16px arial' }
                }, 
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                legend: {
                    <%=Legend%>
                    floating: true,
                    itemHiddenStyle: {
                        color: '#DFDFDF'
                    },
                    itemStyle: {
                        color: '#000000',
                        fontsize:'10px',
                        fontWeight: 'normal'
                    },
                    borderWidth: 1,
                    backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                    shadow: true
                 },
                xAxis: {
                    title: {
                        text: X1
                    },
                    categories: YDATA,
                    labels: {
                        rotation: -90,
                        align: 'right',
                        style: {
                            fontSize: '' + Font + 'px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: Y1
                    },
                    labels: {
                        formatter: function () {
                            return Format+this.value;
                        }
                    },
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>'+Format+'{point.y:.1f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series:  eval(xAxiss)
            });
        });
    </script>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
        <input id="xAxis" type="hidden" value="<%=xAxis %>" />
        <input id="YDATA" type="hidden" value="<%=YDATA %>" />
        <input id="BarTitle" type="hidden" value="<%=BarTitle %>" />
        <input id="X1" type="hidden" value="<%=X1 %>" />
        <input id="Y1" type="hidden" value="<%=Y1 %>" />
        <input id="Font" type="hidden" value="<%=Font %>" />
        <input id="Format" type="hidden" value="<%=Format %>" />
        <div id="Bar" style="margin: 0px; padding: 0px; width: <%=MaxWidth%>px; height: <%=Maxheigth%>px">
        </div>
    </form>
</body>
</html>
