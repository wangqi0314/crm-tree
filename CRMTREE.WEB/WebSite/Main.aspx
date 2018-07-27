<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="CRMTree.Main" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="uc1" TagName="CrmTreetop" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ShuNovo</title>
    <link rel="icon" type="image/ico" href="images/favicon.ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="css/style.css" rel="stylesheet" />
    <link href="/styles/global/main.css?t=20110530" rel="stylesheet" />

    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="js/highcharts.js" type="text/javascript"></script>
    <script src="js/highcharts-more.js" type="text/javascript"></script>
    <script src="js/modules/exporting.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common/setCookie.js"></script>

    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <script src="/scripts/common/json2.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>

    <script src="/scripts/common/DateTime.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            CheckReminders();
            $(".report_title .BL_Link").live("click", function () {
                var href = $(this).attr("href");
                var MF_Code = $(this).attr("Ma");
                $(this).parent().parent().parent().addClass("mb");

                if (href == "#1") {
                    var $iframe = $(this).closest("div.report_left").find("iframe:first");
                    if ($iframe.length > 0) {
                        $iframe.attr('src', $iframe.attr('src'));
                    }
                } else if (href == "#11") {
                    $.ajax({
                        type: "POST",
                        dataType: 'json',
                        contentType: "application/json; charset=utf-8",
                        url: "/Main.aspx/getReports",
                        data: "{MF_Code:'" + MF_Code + "'}",
                        success: function (data) {
                            if (data.d != null) {
                                var Report = data.d;
                                $(".mb").replaceWith(Report);
                            }
                        }
                    });
                }
            });

        });
        function coloseDF() {
            //$("#FG2").remove();
            location.reload();
        }
        function WindowOpen(link, name, size, target, title) {

            var myWindow = window.open(link, name, size, target);
            if (myWindow) myWindow.focus();

        }
        function WindowOpen2(link, name, size, target, title) {
            $.topOpen({ url: link, title: title, width: 650, height: 400, target: target, name: name });
            //$.topOpen({
            //    id: 'open',
            //    url: link,
            //    title: 'ServerHistory',
            //width: 640,
            //height: 480,
            //modal: true,
            //    btnsbar: $.btn.CANCEL.concat()
            //     });
        }
        function CheckReminders() {
            $.ajax({
                type: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                url: "/Main.aspx/getNextTime",
                data: null,
                success: function (data) {
                    $("#reminderIcon").attr("src", "/images/Reminder.png");
                    if (data.d != null) {
                        //Change the reminder picture
                        $("#reminderIcon").attr("src", "/images/Reminder2.png");
                        //Set the timer
                        var _l = data.d.replace("/Date(", "").replace(")/", "");
                        var _date_s = new Date(parseInt( _l));
                        var n_alarm = _date_s.getTime();
                        var _date_ss = new Date($.now())
                        var _d = _date_ss.getTime();
                        
                        var tdiff = n_alarm - _d;

                        if (tdiff <= 0) {
                            reminder();
                        } else {
                            setTimeout(reminder, tdiff);
                        }
                    }
                }
            });
            setTimeout(CheckReminders, 1000 * 300);
        }
        function reminder() {
            $("div[id=newnotice]").css({ "left": "5px", "top": "0px" });
            $("div[id=newnotice]").slideDown("fast");
            $('#RemFrame').attr('src', $('#RemFrame').attr('src'));
            setTimeout(function () { $("div[id=newnotice]").slideUp("slow") }, 10000);
        }

        function Downloadfile(obj, url) {
            var _obj = $(obj);
            var _p_s = _obj.parent().parent().parent().parent().find("#mainFrame")[0].contentWindow.datagrid_page_param();
            var _p = "";
            $.each(_p_s, function (i, o) {
                _p += o.EX_Name + "=" + o.EX_Value + "&";
            });
            if ($.trim(_p) != "") {
                _p = _p.substring(0, _p.length - 1);
                url = url + "&" + _p;
            }
            window.location.href = url;
        }
    </script>
    <style type="text/css">
               .nui-mask {
            -webkit-animation: nui-fadeIn .3s;
            animation: nui-fadeIn .3s;
            background: #000;
            opacity: .1;
            filter: alpha(opacity=10);
            width: 100%;
            height: 100%;
            z-index: 998;
            position: absolute;
            left: 0;
            top: 0;
            line-height: 0;
            font-size: 0;
            overflow: hidden;
        }

        .nui-msgbox {
            /*margin:auto*/
            top: 238px;
            left: 441px;
            zoom: 1;
            border-radius: 4px;
            overflow: hidden;
            outline: 0;
            /*border: 1px solid #888;*/
            width: 311px;
            position: fixed;
            -position: absolute;
            z-index: 999;
            font-size: 12px;
            line-height: 1.500;
            box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
            background: #FAFAFA;
            -webkit-animation: nui-fadeIn .3s;
            animation: nui-fadeIn .3s;
            /*overflow:no-display*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <uc1:CrmTreetop runat="server" ID="CrmTreetop" />
            <div id="content" style='min-height: <%= MaxHeight %>px'>
                <div class="nav_infor" style="height: 11px;"><%=CurrentPage%></div>
                <div class="cont_box" style="width: 1000px; position: relative;">
                    <%=reportHtml %>
                </div>
            </div>
            <uc1:CrmTreebottom runat="server" ID="CrmTreebottom" />
        </div>
    </form>
</body>
<script type="text/javascript">
    $("#nav_<%=MI_Name %>").addClass("nav_select");
</script>
</html>
