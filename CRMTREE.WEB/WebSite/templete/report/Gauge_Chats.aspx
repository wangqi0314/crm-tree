<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gauge_Chats.aspx.cs" Inherits="templete_report_Gauge_Chats" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/js/highcharts.js" type="text/javascript"></script>
    <script src="/js/highcharts-more.js" type="text/javascript"></script>
    <script src="/js/modules/exporting.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {

        $('#container').highcharts({

            chart: {
                type: 'gauge',
                plotBackgroundColor: null,
                plotBackgroundImage: null,
                plotBorderWidth: 0,
                plotShadow: false
            },
                credits: { enabled: false },
            title: {
                text: 'RO Amount'
            },

            pane: {
                startAngle: -75,
                endAngle: 75,
                shape: 'arc',
                background: [{
                    backgroundColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0, '#FFF'],
                            [1, '#333']
                        ]
                    },
                    shape: 'arc',
                    borderWidth: 5,
                    outerRadius: '109%'
                }, {
                    backgroundColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0, '#333'],
                            [1, '#FFF']
                        ]
                    },
                    shape: 'arc',
                    borderWidth: 1,
                    outerRadius: '107%'
                }, {
                    // default background
                        backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || '#EEE',
                        innerRadius: '60%',
                        outerRadius: '100%',
                        shape: 'arc'
                }, {
                    shape: 'arc',
                    backgroundColor: '#DDD',
                    borderWidth: 0,
                    outerRadius: '105%',
                    innerRadius: '95%'
        }],
             },

            // the value axis
            yAxis: {
                min: 0,
                max: 200,

                minorTickInterval: 'auto',
                minorTickWidth: 1,
                minorTickLength: 10,
                minorTickPosition: 'inside',
                minorTickColor: '#666',

                tickPixelInterval: 30,
                tickWidth: 2,
                tickPosition: 'inside',
                tickLength: 10,
                tickColor: '#666',
                labels: {
                    step: 2,
                    rotation: 'auto'
                },
                title: {
                    text: 'Total RO Amount'
                },
                plotBands: [{
                    from: 0,
                    to: 120,
                    color: '#DDDF0D' // yellow
                    
                }, {
                    from: 120,
                    to: 160,
                    color: '#55BF3B' // green
                }, {
                    from: 160,
                    to: 210,
                    color: '#DF5353' // red
                }]
            },

            series: [{
                name: 'Speed',
                data: [80],
                tooltip: {
                    valueSuffix: ' km/h'
                }
            }]

        },
            // Add some life
            function (chart) {
                if (!chart.renderer.forExport) {
                    setInterval(function () {
                        var point = chart.series[0].points[0],
                            newVal,
                            inc = Math.round((Math.random() - 0.5) * 20);

                        newVal = point.y + inc;
                        if (newVal < 0 || newVal > 200) {
                            newVal = point.y - inc;
                        }

                        //point.update(newVal);
                        point.update(190);

                    }, 3000);
                }
            });
    });
</script>
