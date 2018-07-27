<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CrmTreetop.ascx.cs" Inherits="control_CrmTreetop" %>

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
                $(this).children("ul").css("margin-left", $(this).width());
                $(this).children("ul").slideToggle();
            });
            $(".nav>ul>li>ul>li>ul>li").mouseleave(function () {
                $(this).children("ul").toggle();
            });
            //4
            $(".nav>ul>li>ul>li>ul>li>ul>li").mouseenter(function () {
                $(this).children("ul").css("margin-left", $(this).width());
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
                    }
                });
            });

            $("#Remind").click(function () {
                reminder();
            })

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
                    title: $(this).text(),
                    width: 700,
                    height: 500,
                    url: '/templete/maintaining/CRMTreePros.aspx?AU_Code=<%=UserID.ToString() %>'
                });
            });
            $(".MyTools").click(function () {
                $.topOpen({
                    url: '/templete/html/CRMTreeTool.html',
                    modal: false,
                    minimizable: true,
                    maximizable: true,
                    collapsible: true,
                    title: $(this).text(),
                    width: 600, height: 500
                });
            });

            $(".MyHelp").click(function () {
                $.topOpen({
                    url: '/templete/maintaining/CRMTreeHelp.aspx',
                    modal: false,
                    minimizable: true,
                    maximizable: true,
                    collapsible: true,
                    iconCls: 'icon-help',
                    title: $(this).text(),
                    width: 840, height: 540
                });
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
<div id="top">
    <input type="hidden" id="hidLogo" value="<%=Logo %>" />
    <div class="left" style="background: url(/images/Login.png) no-repeat; width: 250px; height: 80px; margin-top: 2px;">
        <a href="#">
            <div class="logo"></div>
        </a>
        <%--        <div class="tagline" style="margin:0px;height:20px; margin-top:-4px;"><%= Resources.CRMTREESResource.topLogo %></div>--%>
    </div>
   <div class="right" style="width: 750px; height: 110px">
        <div class="infor">
            <label class="loginfos">
                <span class="font_orange"><%=realName%></span> <%= Resources.CRMTREESResource.topWelcome %>
            </label>
            <label class="MyHelp" id="Help">
                <a href="#">
                    <img src="/images/helpS.png" alt="Help" title="Help" style="padding-right: 2px; margin-bottom: -4px;" /><%= Resources.CRMTREEResource.LoginHelp %></a></label>
            <label class="MyTools">
                <a href="#">
                    <img src="/images/ToolS.png" style="padding-right: 2px; margin-bottom: -4px;" /><%= Resources.CRMTREEResource.LoginTool %></a></label>
            <label class="MyRemin" id="Remind">
                <a href="#">
                    <img id="reminderIcon" src="/images/Reminder.png" style="padding-right: 2px; margin-bottom: -4px;" /><%= Resources.CRMTREEResource.LoginRemind %></a></label>
            <label class="MyPros">
                <a href="#">
                    <img src="/images/ProfS.png" style="padding-right: 2px; margin-bottom: -4px;" /><%= Resources.CRMTREEResource.LoginProfile %></a></label>
            <label class="signout">
                <a href="/LoginOut.aspx?Source=1"><%= Resources.CRMTREESResource.topSignout %></a>
            </label>
            &nbsp;
            <select id="internationalization">
                <option value="1">Chinese-汉语</option>
                <option value="2">US-English</option>
            </select>
            <div style="float:right;padding-top: 2px;">
                <img id="ImageFlag" src="/images/ChinaFlag.jpg" />
            </div>
        </div>
        <div class="Languages" style="display: none"><%=Languages %></div>
        <div class="search">
            <%--<input type="text" class="input_search" />
            <input type="button" class="menu_search" value="<%= Resources.CRMTREESResource.topSearch %>" />--%>

            <input id="cg_main_search" class="easyui-combogrid fluid" data-options="
                width:420,
                <%--height:30,--%>
                panelWidth:520,
                required:true,
                url: '',
                hasDownArrow:false,
                idField:'AU_Code',
                textField:'AU_Name',
                fitColumns:true,
                iconCls:'icon-search',
                iconAlign:'left',
                columns:[[
                {field:'AU_Name',title:'<%=Resources.CRMTREEResource.main_customer_AU_Name %>',width:100},
                {field:'Phone',title:'<%=Resources.CRMTREEResource.main_customer_Phone %>',width:100},
                {field:'eMail',title:'<%=Resources.CRMTREEResource.main_customer_eMail %>',width:100},
                {field:'Car_Lic',title:'<%=Resources.CRMTREEResource.main_customer_Car_Lic %>',width:100},
                {field:'AU_Username',title:'<%=Resources.CRMTREEResource.main_customer_AU_Username %>',width:100}
                ]]
                " />
        </div>
        <div style="width: 750px; height:65px">
            <div class="nav">
                <ul>
                    <li class="nav_left"></li>
                    <%=strNav.ToString()%>
                    <li class="nav_right"></li>
                </ul>
            </div>
        </div>
        <div class="PowBy">
            <span style="font-weight: bold; font-family: serif; padding-right: 9px; margin-top: 0px;">
                <%= Resources.CRMTREESResource.PoweredBy %> &nbsp&nbsp&nbsp<img id="ImageFlag1" src="/images/CTLogoS.png" /></span>
        </div>

    </div>
</div>
<div id="newnotice" style="width:510px;height:150px;position:absolute;z-index:999;">
    <p>
        <span class="font_orange"><%=realName%></span> <span class="Remindtitle"><%=Resources.CRMTREESResource.LoginReminder %></span>
        <span id="bts">
<%--           <label class="button" id="tomin" title="Mini">1</label>
            <label class="button" id="tomax" title="max">2 </label>
            <label class="button" id="toclose" title="Close">3</label>--%>
            <img class="toclose" src="/images/ico/close.png" title="Close" style="position:absolute;top:0px;right:0px;height:12px;margin:4px;cursor:pointer;" />
        </span>
    </p>
    <div id="noticecon">
        <iframe id="RemFrame" style="width: 98%; height: 84%; text-align: center; border: 0px;position:absolute;z-index:999;" name="RemindFrame" src="templete/report/DataGrid.html?MF_FL_FB_Code=152&AU_Code=<%=UserID.ToString() %>&IsProc=1" ></iframe>
    </div>
</div>
<script type="text/javascript">
    var _s_url = '/handler/Reports/Reports.aspx';
    var _grid_window_config = {
 <%--       url: '/templete/report/CustomerDetails.aspx?_type=1&_winID=<%=Guid.NewGuid()%>',
 --%>       url: '/templete/report/CustomerManager.aspx?_type=1&_winID=<%=Guid.NewGuid()%>',
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
    _main_search = function (newValue, oldValue) {
        if (typeof newValue === 'number') {
            //bindData
            if (newValue > 0) {
                var row = $("#cg_main_search").combogrid('grid').datagrid('getSelected');
                var au_name = row ? row.AU_Name : '';
                var windowConfig = getWindowConfig(row);
                windowConfig.url += '&AU_Name=' + au_name + '&AU_Code=' + newValue;

                $.topOpen(windowConfig);
            }
            return;
        } else {
            //clear disable
        }

        var q = $.trim(newValue);
        if (q === '') {
            $("#cg_main_search").combogrid('grid').datagrid('loadData', []);
            return;
        }

        var reg = /^\d+/;
        var type = reg.test(q) ? 1 : 2; // 1 number , 2 char

        var o = { action: 'Get_Main_Search', q: q, type: type };
        $.post(_s_url, { o: JSON.stringify(o) }, function (res) {
            if (!$.checkResponse(res)) { res = []; }
            $("#cg_main_search").combogrid('grid').datagrid('loadData', res);
        }, "json");
    };

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
            $("#cg_main_search").combogrid('textbox').focus();

            var o = { action: 'Get_Grid_Window', gw_code: 1 };
            $.post(_s_url, { o: JSON.stringify(o) }, function (res) {
                if (res) {
                    var windowConfig = res[0];
                    $.extend(_grid_window_config, windowConfig);
                    _grid_window_config.title = windowConfig.title ? windowConfig.title : "  ";
                    _grid_window_config.width = windowConfig.width ? windowConfig.width : 840;
                    _grid_window_config.height = windowConfig.title ? windowConfig.height : 450;
                }
                $("#cg_main_search").combogrid({ onChange: _main_search });
            }, "json");
        })();


    });
</script>