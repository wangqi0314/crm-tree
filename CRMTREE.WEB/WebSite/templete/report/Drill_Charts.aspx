<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Drill_Charts.aspx.cs" Inherits="templete_report_C_L_P_Chats" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/js/highcharts.js" type="text/javascript"></script>
    <script src="/js/highcharts-more.js" type="text/javascript"></script>
    <script src="/js/modules/exporting.js" type="text/javascript"></script>
    <script src="/js/modules/drilldown.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container" style="margin: 0px; padding: 0px; width: <%=MaxWidth%>px; height: <%=Maxheigth%>px">
        </div>
    </form>
</body>
</html>
<script type="text/javascript">

    $(function () {
        // Create the chart
        Highcharts.getOptions().colors = Highcharts.map(Highcharts.getOptions().colors, function (color) {
            return {
                radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
                stops: [
                    [0, color],
                    [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
                ]
            };
        });
        $('#container').highcharts({
            chart: {
                type: 'pie'
            }
            ,title: {
                text: '<%=ChartTit%>'
            }
            //subtitle: {
            //    text: 'Click the columns to view versions. Source: <a href="http://netmarketshare.com">netmarketshare.com</a>.'
            //}
            ,xAxis: {
                type: 'category'
            }
            //yAxis: {
            //    title: {
            //        text: 'Total percent market share'
            //    }

            //},
            <%=YTit%>
            ,plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    }
                },
                bar: {
                    borderWidth: 0,
                    dataLabels: {
                        enabled: true,
                        format: '{point.y:.0f}'
                    }
                }
            },
            tooltip: {
 //               pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },

            credits: { enabled: false },
            legend: {
                layout: 'horizontal',
                align: 'left',
                verticalAlign: 'top',
                x: 50,
                y: 10,
                floating: true,
                itemHiddenStyle: {
                    color: '#DFDFDF'
                },
                itemStyle: {
                    color: '#000000',
                    fontsize: '10px',
                    fontWeight: 'normal'
                },
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
 
    <%=YDATA%>

        //    series: [{
        //        name: "Brands",
        //        colorByPoint: true,
        //        type:'pie',
        //        data: [{
        //            name: "Microsoft Internet Explorer",
        //            y: 56.33,
        //            drilldown: "Microsoft Internet Explorer"
        //        }, {
        //            name: "Chrome",
        //            y: 24.03,
        //            drilldown: "Chrome"
        //        }, {
        //            name: "Firefox",
        //            y: 10.38,
        //            drilldown: "Firefox"
        //        }, {
        //            name: "Safari",
        //            y: 4.77,
        //            drilldown: "Safari"
        //        }, {
        //            name: "Opera",
        //            y: 0.91,
        //            drilldown: "Opera"
        //        }, {
        //            name: "Proprietary or Undetectable",
        //            y: 0.2,
        //            drilldown: null
        //        }]
        //    }],
 
        //    drilldown: {
        //        series: [{
        //            name: "Microsoft Internet Explorer",
        //            id: '1',
        //            events: {
        //                click: function (series) {
        //                    //alert('hello');
        //                    parent.location.href = "/manage/campaign/campaign.aspx?CA=1&CG_Code=13566";
        //                }
        //            },
        //            type: 'bar',
        //            data: [
        //                ["v11.0",24.13],
        //                ["v8.0", 17.2 ],
        //                ["v9.0",8.11],
        //                ["v10.0",5.33],
        //                ["v6.0",1.06],
        //                ["v7.0",0.5]
        //            ]
        //        }, {
        //            name: "Chrome",
        //            id: '5. 睡眠',
        //             type: 'bar',
        //             data: [
        //                ["v40.0",5],
        //                ["v41.0",4.32],
        //                ["v42.0",3.68],
        //                ["v39.0",2.96],
        //                ["v36.0",2.53],
        //                ["v43.0",1.45],
        //                ["v31.0",1.24],
        //                ["v35.0",0.85],
        //                ["v38.0",0.6],
        //                ["v32.0",0.55],
        //                ["v37.0",0.38],
        //                ["v33.0",0.19],
        //                ["v34.0",0.14],
        //                ["v30.0",0.14]
        //            ]
        //        },

        //        ],

        //},



    });
    });


//The following loads data dynamically
    //$(function () {

    //    // Create the chart
    //    $('#container').highcharts({
    //        chart: {
    //            type: 'column',
    //            events: {
    //                drilldown: function (e) {
    //                    if (!e.seriesOptions) {

    //                        var chart = this,
    //                            drilldowns = {
    //                                'Animals': {
    //                                    name: 'Animals',
    //                                    data: [
    //                                        ['Cows', 2],
    //                                        ['Sheep', 3]
    //                                    ]
    //                                },
    //                                'Fruits': {
    //                                    name: 'Fruits',
    //                                    data: [
    //                                        ['Apples', 5],
    //                                        ['Oranges', 7],
    //                                        ['Bananas', 2]
    //                                    ]
    //                                },
    //                                'Cars': {
    //                                    name: 'Cars',
    //                                    data: [
    //                                        ['Toyota', 1],
    //                                        ['Volkswagen', 2],
    //                                        ['Opel', 5]
    //                                    ]
    //                                }
    //                            },
    //                            series = drilldowns[e.point.name];

    //                        // Show the loading label
    //                        chart.showLoading('Simulating Ajax ...');

    //                        setTimeout(function () {
    //                            chart.hideLoading();
    //                            chart.addSeriesAsDrilldown(e.point, series);
    //                        }, 1000);
    //                    }

    //                }
    //            }
    //        },
    //        title: {
    //            text: 'Async drilldown'
    //        },
    //        xAxis: {
    //            type: 'category'
    //        },

    //        legend: {
    //            enabled: false
    //        },

    //        plotOptions: {
    //            series: {
    //                borderWidth: 0,
    //                dataLabels: {
    //                    enabled: true
    //                }
    //            }
    //        },

    //        series: [{
    //            name: 'Things',
    //            colorByPoint: true,
    //            data: [{
    //                name: 'Animals',
    //                y: 5,
    //                drilldown: true
    //            }, {
    //                name: 'Fruits',
    //                y: 2,
    //                drilldown: true
    //            }, {
    //                name: 'Cars',
    //                y: 4,
    //                drilldown: true
    //            }]
    //        }],

    //        drilldown: {
    //            series: []
    //        }
    //    });
    //});



</script>
