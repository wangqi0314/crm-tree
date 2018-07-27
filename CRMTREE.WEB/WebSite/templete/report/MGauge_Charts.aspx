<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGauge_Charts.aspx.cs" Inherits="templete_report_Gauge_Chats" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/js/highchart-4.1.7/highcharts.js"></script>
    <script src="/js/highchart-4.1.7/highcharts-more.js"></script>
    <script src="/js/highchart-4.1.7/modules/solid-gauge.js"></script>
</head>
<body style="margin: 0px; padding: 0px;">

    <form id="form1" runat="server">
        <div style="width: <%=MaxWidth%>px; height: <%=Maxheigth%>px; margin: 0 auto"><%=_Div %></div>
    </form>
</body>

</html>
<script type="text/javascript">
    $(function () {

        var gaugeOptions = {

            chart: {
                type: 'solidgauge'
            },

            title: null,

            pane: {
                center: ['50%', '90%'],
                size: '180%',
                startAngle: -90,
                endAngle: 90,
                background: {
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || '#EEE',
                    innerRadius: '60%',
                    outerRadius: '100%',
                    shape: 'arc'
                }
            },

            tooltip: {
                enabled: false
            },

                // the value axis
            yAxis: {
                stops: [
                    [0.1, '#DF5353'], // red
                    [0.5, '#DDDF0D'], // yellow
                    [0.9, '#55BF3B'] // green
               ],
                lineWidth: 0,

                minorTickInterval: 'auto',
                minorTickWidth: 0,
                minorTickLength: 10,
                minorTickPosition: 'inside',
                minorTickColor: '#666',

                tickPixelInterval: 30,
                tickWidth: 1,
                tickPosition: 'inside',
                tickLength: 10,
                tickColor: '#666',
                labels: {
                    step: 2,
                    rotation: 'auto'
                },
                title: {
                    y: <%=T_Pos%>,
                    style: {
                        fontSize: '13px',
                        fontWeight:'bold',
                        fontFamily: 'Verdana, sans-serif'
                    }
                },
                labels: {
                    y: 16,
                    style: {
                            fontSize: '0px'
                           }
        }
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                solidgauge: {
                    dataLabels: {
                        y: 5,
                        borderWidth: 0,
                        useHTML: true
                    }
                }
            }
        };

        <%=_YAxis%>

        <%=_Life%>

    });

</script>
