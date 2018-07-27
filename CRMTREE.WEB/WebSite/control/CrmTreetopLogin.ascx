<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CrmTreetopLogin.ascx.cs" Inherits="control_CrmTreetopLogin" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ShuNovo</title>
    <link rel="icon" type="image/ico" href="images/favicon.ico" />
    <script type="text/javascript">
        $(document).ready(function () {
            //1
            $(".nav>ul>li").mouseenter(function () {
                $(this).children("ul").slideToggle("fast", function () {
                });
            });
            $(".nav>ul>li").mouseleave(function () {
                $(this).children("ul").toggle();
            });
            //2
            $(".nav>ul>li>ul>li").mouseenter(function () {
                $(this).children("ul").css("margin-left", $(this).width());
                $(this).children("ul").slideToggle();
            });
            $(".nav>ul>li>ul>li").mouseleave(function () {
                $(this).children("ul").toggle();
            });
            //3
            $(".nav>ul>li>ul>li>ul>li").mouseenter(function () {
                $(this).children("ul").slideToggle();
            });
            $(".nav>ul>li>ul>li>ul>li").mouseleave(function () {
                $(this).children("ul").toggle();
            });
            //5
            $(".nav>ul>li>ul>li>ul>li>ul>li").mouseenter(function () {
                $(this).children("ul").slideToggle();
            });
            $(".nav>ul>li>ul>li>ul>li>ul>li").mouseleave(function () {
                $(this).children("ul").toggle();
            });
            $("#internationalization").val($(".Languages").html());
            getFlag();
            $("#internationalization").change(function () {
                $.ajax({
                    type: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    url: "/handler/International.aspx/SetLanguage",
                    data: "{Key:" + $("#internationalization").val() + "}",
                    success: function (data) {
                        var JSONObjects = data.d;
                        location.reload();
                    },
                    error: function (err, err2, err3) {
                        alert(err3, err2);
                        return false;
                    }
                });
            });

            $(window).scroll(function () {
                var p = $(window).scrollTop();
                var b = $(window).scrollLeft();
                $("div[id=newnotice]").css({ "bottom": "0px" });
                $("div[id=newnotice]").css({ "right": "-" + b + "px", "bottom": "-" + p + "px" });
            });

            $("label[id=tomin]").click(function () {
                $("div[id=noticecon]", "div[id=newnotice]").slideUp();
            });

            $("label[id=tomax]").click(function () {
                $("div[id=noticecon]", "div[id=newnotice]").slideDown();
            });

            $(".toclose").click(function () {
                $("div[id=newnotice]").hide();
            });

            $(".MyPros").click(function () {
                $.topOpen({
                    url: '/templete/maintaining/CRMTreePros.aspx?AU_Code=<%=UserID.ToString() %>', width: 600, height: 500
                });
            });
            $(".MyTools").click(function () {
                $.topOpen({ url: '/templete/html/CRMTreeTool.html', modal: false, minimizable: true, maximizable: true, collapsible: true, title: 'Tool', width: 600, height: 500 });
            });

            $(".MyHelp").click(function () {
                $.topOpen({ url: '/templete/maintaining/CRMTreeHelp.aspx', modal: false, minimizable: true, maximizable: true, collapsible: true, title: 'Help' });
            });
            var logos = $("#hidLogo").val();
            if (logos == "" || logos == null) {
                logos = "Login.png";
            }
            $(".left").css({ background: "url(/images/" + logos + ") no-repeat" });
        });
        function getFlag() {
            if ($("#internationalization").val() == "1") {
                $("#ImageFlag").attr("src", "/images/ChinaFlag.png");
            } else if ($("#internationalization").val() == "2") {
                $("#ImageFlag").attr("src", "/images/USFlag.png");
            }
        }
    </script>
</head>
<body></body>
</html>
<div id="top" style="height: 75px;">
    <input type="hidden" id="hidLogo" value="<%=Logo %>" />
    <div class="left" style="background: url(/images/Login.png) no-repeat; width: 300px; height: 65px; margin-top: 10px;">
        <a href="#">
            <div class="logo"></div>
        </a>
        <%--        <div class="tagline" style="margin:0px;height:20px; margin-top:-4px;"><%= Resources.CRMTREESResource.topLogo %></div>--%>
    </div>
    <div class="right" style="width: 700px; height: 70px">
        <div class="infor">
            <label class="loginfos">
            </label>
            &nbsp;
           <select id="internationalization">
               <option value="1">Chinese-汉语</option>
               <option value="2">US-English</option>
           </select>
            <div style="float: right;">
                <img id="ImageFlag" src="/images/EnglishFlag.jpg" />
            </div>
        </div>
        <div class="Languages" style="display: none"><%=Languages %></div>
        <div class="nav" style="margin-top: 25px;">
            <ul>
                <li class="nav_left"></li>
                <%=strNav.ToString()%>
                <li class="nav_right"></li>
            </ul>
        </div>
        <div class="PowBy">
            <%-- <span style="font-weight: bold;  font-family: serif;padding-right: 9px;margin-top: 0px;">
                <%= Resources.CRMTREESResource.PoweredBy %> &nbsp&nbsp&nbsp<img id="ImageFlag" src="/images/CTLogoS.png" /></span>--%>
        </div>

    </div>
</div>
<script type="text/javascript">
    var _s_url = '/handler/Reports/Reports.aspx';
    var _grid_window_config = {
<%--        url: '/templete/report/CustomerDetails.aspx?_type=1&_winID=<%=Guid.NewGuid()%>',--%>
        url: '/templete/report/CustomerManager.aspx?_type=1&_winID=<%=Guid.NewGuid()%>',
        width: 840,
        height: 450
    };
    function getWindowConfig(rowData) {
        var windowConfig = $.extend({}, _grid_window_config);
        windowConfig.title = $.formatParams(windowConfig.title, rowData);
        windowConfig.width = windowConfig.width > 0 ? windowConfig.width : 840;
        windowConfig.height = windowConfig.height > 0 ? windowConfig.height : 450;
        return windowConfig;
    }

    $(function () {
        //初始化
        (function Init() {
            /*
            var $c = $("#cg_main_search");
            var textbox = $c.combogrid('textbox');
            $(textbox).click(function () {
                $c.combogrid('showPanel');
            }).focus();
            */

            //$("#cg_main_search").combogrid('textbox').focus();

            var o = { action: 'Get_Grid_Window', gw_code: 1 };
            $.post(_s_url, { o: JSON.stringify(o) }, function (res) {
                if (res) {
                    var windowConfig = res[0];
                    $.extend(_grid_window_config, windowConfig);
                    _grid_window_config.title = windowConfig.title ? windowConfig.title : "  ";
                    _grid_window_config.width = windowConfig.width ? windowConfig.width : 840;
                    _grid_window_config.height = windowConfig.title ? windowConfig.height : 450;
                }
                //$("#cg_main_search").combogrid({ onChange: _main_search });
            }, "json");
        })();


    });
</script>
