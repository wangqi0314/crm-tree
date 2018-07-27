<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiChart.aspx.cs" Inherits="templete_report_C_L_P_Chats" %>

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
        <input id="xAxis" type="hidden" value="<%=xAxis %>" />
        <input id="YDATA" type="hidden" value="<%=YDATA %>" />
        <input id="ChartTit" type="hidden" value="<%=ChartTit %>" />
        <input id="XTit" type="hidden" value="<%=XTit %>" />
        <input id="YTit" type="hidden" value="<%=YTit %>" />
        <input id="Font" type="hidden" value="<%=Font %>" />
        <input id="Format" type="hidden" value="<%=Format %>" />
        <div id="container" style="margin: 0px; padding: 0px; width: <%=MaxWidth%>px; height: <%=Maxheigth%>px">
        </div>
    </form>
</body>
</html>
<script type="text/javascript">

    function MtooltipFormatter (tooltip) {
        var ret;
        // Create the header with reference to the time interval
        //ret = '<small>' + Highcharts.dateFormat('%A, %b %e, %H:%M', tooltip.x) + '-' +
        //       Highcharts.dateFormat('%H:%M', tooltip.points[0].point.to) + '</small><br>';
        var index = tooltip.points[0].point.index,
            ret = '<small>' + Highcharts.dateFormat('%A, %b %e ', tooltip.x) + '</small><br>';

        // Symbol text
        //ret += '<b>' + this.symbolNames[index] + '</b>';
        ret += '<table>';

        // Add all series
        Highcharts.each(tooltip.points, function (point) {
            var series = point.series;
            ret += '<tr><td><span style="color:' + series.color + '">\u25CF</span> ' + series.name +
                ': </td><td style="white-space:nowrap">'  +'<b>'+
                series.options.tooltip.valueSuffix + '</b> '+ Highcharts.pick(point.point.value, point.y)+'</td></tr>';
        });

        // Add wind
        //ret += '<tr><td style="vertical-align: top">\u25CF Wind</td><td style="white-space:nowrap">' + this.windDirectionNames[index] +
        //    '<br>' + this.windSpeedNames[index] + ' (' +
        //    Highcharts.numberFormat(this.windSpeeds[index], 1) + ' m/s)</td></tr>';

        ret += '</table>';


        return ret;
    };

    $(function () {
        var ChatTit = $("#ChartTit").val();
        var XTit = $("#XTit").val();
        var YTit = $("#YTit").val();
        var Font = $("#Font").val();
        var Format = $("#Format").val();
        var xAxis = $("#xAxis").val();
        var YDATA = $("#YDATA").val();

        $('#container').highcharts({
            chart: {
                marginBottom: 25,
                marginRight: 60,
                marginLeft: 60,
                marginTop: 20,
                plotBorderWidth: 0,
            },
            title: {
                text: ChatTit
            },
            tooltip: {
                shared: true,
                useHTML: true,
                formatter: function () {
                    return MtooltipFormatter(this);
                }
            },
            legend: {
                layout: 'horizontal',
                align: 'right',
                verticalAlign: 'top',
                x: -50,
                y: -10,
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
            yAxis: eval(YTit),
            xAxis: eval(XTit),

            series: eval(YDATA),
            plotOptions: {
                pie: {

                }
            },
            credits: { enabled: false }
        });
    });

</script>
