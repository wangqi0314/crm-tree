<%@ Page Language="C#" AutoEventWireup="true" CodeFile="C_L_P_Chats .aspx.cs" Inherits="templete_report_C_L_P_Chats" %>

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
        <input id="xAxis" type="hidden" value="" />
        <input id="YDATA" type="hidden" value="[{type:'column', name:'Col1', data: [[Date.UTC(2013,0,1),119.1975],[Date.UTC(2013,0,2),936.2775],[Date.UTC(2013,0,3),116.0000],[Date.UTC(2013,0,4),610.3120],[Date.UTC(2013,0,5),332.4376],[Date.UTC(2013,0,6),526.3177],[Date.UTC(2013,0,7),1084.6437],[Date.UTC(2013,0,8),610.5066],[Date.UTC(2013,0,9),399.7460],[Date.UTC(2013,0,10),110.0000],[Date.UTC(2013,0,11),771.6633],[Date.UTC(2013,0,12),534.9018],[Date.UTC(2013,0,13),797.4300],[Date.UTC(2013,0,14),667.1122],[Date.UTC(2013,0,15),1156.8233],[Date.UTC(2013,0,16),763.2025],[Date.UTC(2013,0,17),378.5920],[Date.UTC(2013,0,18),1081.3366],[Date.UTC(2013,0,19),776.7120],[Date.UTC(2013,0,20),1011.5200],[Date.UTC(2013,0,21),895.9416],[Date.UTC(2013,0,22),541.9733],[Date.UTC(2013,0,23),541.3675],[Date.UTC(2013,0,24),1021.9366],[Date.UTC(2013,0,25),886.2450],[Date.UTC(2013,0,26),861.1200],[Date.UTC(2013,0,27),611.8300],[Date.UTC(2013,0,28),224.1250],[Date.UTC(2013,0,29),1071.6345],[Date.UTC(2013,0,30),608.4640],[Date.UTC(2013,0,31),487.8462],[Date.UTC(2013,1,1),717.3362],[Date.UTC(2013,1,2),516.4133],[Date.UTC(2013,1,3),671.3577],[Date.UTC(2013,1,4),68.6825],[Date.UTC(2013,1,5),502.5900],[Date.UTC(2013,1,6),614.7616],[Date.UTC(2013,1,7),360.0000],[Date.UTC(2013,1,8),782.1622],[Date.UTC(2013,1,9),927.8655],[Date.UTC(2013,1,10),435.4800],[Date.UTC(2013,1,11),2627.0280],[Date.UTC(2013,1,12),547.0412],[Date.UTC(2013,1,13),1347.6850],[Date.UTC(2013,1,14),1256.4833],[Date.UTC(2013,1,15),1140.4625],[Date.UTC(2013,1,16),392.4875],[Date.UTC(2013,1,17),3780.8137],[Date.UTC(2013,1,18),289.1250],[Date.UTC(2013,1,19),749.5066],[Date.UTC(2013,1,20),964.3760],[Date.UTC(2013,1,21),787.5666],[Date.UTC(2013,1,22),151.7128],[Date.UTC(2013,1,23),481.3040],[Date.UTC(2013,1,24),496.9062],[Date.UTC(2013,1,25),329.8055],[Date.UTC(2013,1,26),761.6860],[Date.UTC(2013,1,27),782.4250]]},{type:'spline', name:'Col1', data: [[Date.UTC(2013,0,5),504.5682],[Date.UTC(2013,0,12),562.0380],[Date.UTC(2013,0,19),860.6343],[Date.UTC(2013,0,26),770.2371],[Date.UTC(2013,1,2),700.9355],[Date.UTC(2013,1,9),634.9158],[Date.UTC(2013,1,16),1120.9193],[Date.UTC(2013,1,23),1093.8809],[Date.UTC(2013,2,2),583.4821],]},{type:'column', name:'Col1', data: [[Date.UTC(2013,0,5),504.5682],[Date.UTC(2013,0,12),562.0380],[Date.UTC(2013,0,19),860.6343],[Date.UTC(2013,0,26),770.2371],[Date.UTC(2013,1,2),700.9355],[Date.UTC(2013,1,9),634.9158],[Date.UTC(2013,1,16),1120.9193],[Date.UTC(2013,1,23),1093.8809],[Date.UTC(2013,2,2),583.4821],]},]" />
        <input id="ChartTit" type="hidden" value="[{{Period}} RO Report]" />
        <input id="XTit" type="hidden" value="[{ title:{text:'X1Label'}, type: 'datetime', dateTimeLabelFormats: {month: '%e. %b',year: '%b' } ,labels:{  style: {fontSize: '12px' , fontFamily: 'Verdana, sans-serif'}}}]" />
        <input id="YTit" type="hidden" value="[{ title:{text:'Y1Label'} ,labels:{  style: {fontSize: '12px' , fontFamily: 'Verdana, sans-serif'},formatter: function () {return '$' + this.value;}}},{ title:{text:'Y2 Label'} ,labels:{  style: {fontSize: ' 12px' , fontFamily: 'Verdana, sans-serif'},formatter: function () {return ' ' + this.value;}}}]" />
        <input id="Font" type="hidden" value="" />
        <input id="Format" type="hidden" value="" />
        <div id="container" style="margin: 0px; padding: 0px; width: 900px; height: 315px">
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(function () {
        var ChatTit = $("#ChartTit").val();
        var XTit = $("#XTit").val();
        var YTit = $("#YTit").val();
        var Font = $("#Font").val();
        var Format = $("#Format").val();
        var xAxis = $("#xAxis").val();
        var YDATA = $("#YDATA").val();

        $('#container').highcharts({
            title: {
                text: ChatTit
            },
            yAxis: eval(YTit),
            xAxis: eval(XTit),

            series: eval(YDATA),
//{
     //           type: 'column',
     //           name: 'Daily',
     //           data: [
     //               [Date.UTC(2013, 5, 27), 12233],
     //               [Date.UTC(2013, 6, 3), 17827],
     //               [Date.UTC(2013, 6, 4), 15203],
     //               [Date.UTC(2013, 6, 5), 11618],
     //               [Date.UTC(2013, 6, 6), 15287],
     //               [Date.UTC(2013, 6, 7), 15922],
     //               [Date.UTC(2013, 6, 8), 7073],
     //               [Date.UTC(2013, 6, 9), 10768],
     //               [Date.UTC(2013, 6, 10), 1693],
     //               [Date.UTC(2013, 6, 11), 2467],
     //               [Date.UTC(2013, 6, 12), 3927],
     //               [Date.UTC(2013, 6, 13), 10420],
     //               [Date.UTC(2013, 6, 14), 14134],
     //               [Date.UTC(2013, 6, 15), 12275],
     //               [Date.UTC(2013, 6, 16), 7714]
     //               ]
     //           },
     //           {
     //               type: 'spline',
     //               name: 'Weekly Average',
     //               data: [
     //               [Date.UTC(2013, 6, 2), 10236.7238],
     //               [Date.UTC(2013, 6, 8), 11176.7746],
     //               [Date.UTC(2013, 6, 14), 10214.7787]

     //               ],
     //               //marker: {
     //          //         lineWidth: 2,
     //          //         lineColor: Highcharts.getOptions().colors[0],
     //          //         fillColor: 'white'
     //          //     }
     //          }
            
            plotOptions: {
                pie: {

                }
            },
            credits: { enabled: false }
        });
    });

</script>
