<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PieChats.aspx.cs" Inherits="templete_report_PieChats" %>

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
            var PieTitle = $("#PieTitle").val();
            var PieData = $("#PieData").val();
            $(window.parent.document).find(".asyncbox_title_tips").text(PieTitle);
            Highcharts.getOptions().colors = Highcharts.map(Highcharts.getOptions().colors, function (color) {
                return {
                    radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
                    stops: [
                        [0, color],
                        [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
                    ]
                };
            });

            PieData = eval(PieData);           
            $('#PieCh').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: true,
                    type: 'pie'
        },
                credits: {
                    enabled: false
                },
                title: {
                    text: PieTitle,
                    style: { color: '#000000', font: 'bold 16px arial' }
                },
                legend: {
                    <%=Legend%>
                    floating: false,
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

                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.5f}%</b>'
                },
                plotOptions: {
                     pie: {
                         allowPointSelect: true,
                         cursor: 'pointer',
                         borderWidth: 0,
                         dataLabels: {
                             enabled: false
                         },
                         showInLegend: true,
                         size: '120%'
                     }
                },
                series: PieData
            });
        });
       
        //根据参数获取Id的value
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="PieData" type="hidden" value="<%=PieData %>" />
        <input id="PieTitle" type="hidden" value="<%=PieTitle %>" />
        <div id="PieCh" style="margin:0px;padding:0px;width:<%=MaxWidth%>px;height:<%=Maxheigth%>px">
        </div>
    </form>
</body>
</html>
