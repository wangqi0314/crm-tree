<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DGaug_Charts.aspx.cs" Inherits="templete_report_Gauge_Chats" %>

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
        <div style="width: <%=MaxWidth%>px; height: <%=Maxheigth%>px; margin: 0 auto">
	        <div id="cont_L" style="width: 300px; height: 200px; float: left"></div>
	        <div id="cont_R" style="width: 300px; height: 200px; float: left"></div>
            </div>
        <input id="_title1" type="hidden" value="<%=_title1 %>" />
        <input id="_title2" type="hidden" value="<%=_title2 %>" />
        <input id="_label1" type="hidden" value="<%=_label1 %>" />
        <input id="_label2" type="hidden" value="<%=_label2 %>" />
        <input id="_max1" type="hidden" value="<%=_max1 %>" />
        <input id="_goal1" type="hidden" value="<%=_goal1 %>" />
        <input id="_val1" type="hidden" value="<%=_val1 %>" />
        <input id="_max2" type="hidden" value="<%=_max2 %>" />
        <input id="_goal2" type="hidden" value="<%=_goal2 %>" />
        <input id="_val2" type="hidden" value="<%=_val2 %>" />
    </form>
</body>

</html>
<script type="text/javascript">
    $(function () {
        var title1 = $("#_title1").val();
        var title2 = $("#_title2").val();
        var label1 = $("#_label1").val();
        var label2 = $("#_label2").val();
        var max1 = $("#_max1").val();
        var goal1 = $("#_goal1").val();
        var val1 = $("#_val1").val();
        var max2 = $("#_max2").val();
        var goal2 = $("#_goal2").val();
        var val2 = $("#_val2").val();

        var gaugeOptions = {

            chart: {
                type: 'solidgauge'
            },

            title: null,

            pane: {
                center: ['50%', '85%'],
                size: '140%',
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
                    y: -70,
                    style: {
                        fontSize: '14px',
                        fontWeight:'bold',
                        fontFamily: 'Verdana, sans-serif'
                    }
                },
                labels: {
                    y: 16
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

        // The First gauge
        $('#cont_L').highcharts(Highcharts.merge(gaugeOptions, {
            yAxis: {
                min: 0,
                max: max1,
                plotBands: [{
                    from: 0,
                    to: goal1,
                    color: '#DDDF0D' // yellow

                }, {
                    from: goal1,
                    to: max1,
                    color: '#55BF3B' // green
                }],
                title: {
                    text: title1
                }
            },


            series: [{
                name: title1,
                data: [eval(max1)],
                dataLabels: {
                    format: '<div style="text-align:center"><span style="font-size:25px;color:' +
                        ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black') + '">{y}</span><br/>' +
                           '<span style="font-size:12px;color:silver"> '+label1+' </span></div>'
                },
                tooltip: {
                    valueSuffix: ' '
                }
            }]

        }));

        // The Second gauge
        $('#cont_R').highcharts(Highcharts.merge(gaugeOptions, {
            yAxis: {
                min: 0,
                max: max2,
                plotBands: [{
                    from: 0,
                    to: goal2,
                    color: '#DDDF0D' // yellow

                }, {
                    from: goal2,
                    to: max2,
                    color: '#55BF3B' // green
                }],
                title: {
                    text: title2
                }
            },

            series: [{
                name: 'RPM',
                data: [eval(max2)],
                dataLabels: {
                    format: '<div style="text-align:center"><span style="font-size:25px;color:' +
                        ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black') + '">{y:.1f}</span><br/>' +
                           '<span style="font-size:12px;color:silver"> '+label2+' </span></div>'
                },
                tooltip: {
                    valueSuffix: ' '
                }
            }]

        }));

        // Bring life to the dials
        var MyTimer = setInterval(function () {
            var chart = $('#cont_L').highcharts(),
                point;
            if (chart) {
                point = chart.series[0].points[0];
                point.update(eval(val1));
            }
            chart = $('#cont_R').highcharts();
            if (chart) {
                point = chart.series[0].points[0];
                point.update(eval(val2));
            }
            clearInterval(MyTimer);
        }, 1000);




    });

</script>
