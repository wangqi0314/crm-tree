<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list_car.aspx.cs" Inherits="manage_car_list_car" %>

<%@ Register Src="~/control/top.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/control/bottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Resources.CRMTREEResource.MyCarList %></title>
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <link href="/styles/global/main.css?t=20110530" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>
    <script type="text/javascript" src="/scripts/manage/car/list_car.js"></script>
</head>
<body>
    <div id="container">
        <uc1:top ID="top1" runat="server" />

        <div id="content">
            <div class="nav_infor"><a class="brown" href="#"><%=Resources.CRMTREEResource.MyCarnavigation %></a>&nbsp;&gt;&nbsp;<%=Resources.CRMTREEResource.MyCarList %></div>
            <div class="cont_box">
               <%-- <div class="cont_title">
                    <ul>
                        <li class="t_left"></li>
                        <li class="t_cont">car List</li>
                        <li class="t_right"></li>
                        <li class="t_add"><a href="/manage/car/edit_car.aspx" >
                            <img src="/images/icon_add.png" /></a></li>
                        <li class="t_arrow" onclick="ExpendSearch()">
                            <a href="javascript:;">
                                <img src="/images/arrow_down.png" /></a>
                        </li>
                    </ul>
                </div>--%>
               <div class="report_title" >
                    <span style="float:right;margin-top:-10px;">
                        <img style="cursor:pointer;" src="/images/arrow_down.png" />
                        <img class="Add_car" style="cursor:pointer;" src="/images/icon_add.png" /></span>
                   <%=Resources.CRMTREEResource.MyCarList %>

                </div>
                <div class="box clearfix">
                    <div class="box-bg">
                        <div class="searchCont pr pt10" id="search">
                            <p class="mb10">
                                Keywords： 
                    <input class="input-text w150 SetInputStyle" type="text" id="txtKeyWords" onfocus="SetKeySearch();"/>
                                PageSize：<input class="input-text SetInputStyle w30" type="text" id="txtPageSize" onfocus="SetKeySearch();"  maxlength="4" value="10" />
                                <a href="javascript:;" class="btn btnSearch" id="btnSearch"><i>icon</i><span>search</span></a>
                            <p>
                        </div>
                        <div id="divGridList" class="pt10">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc2:bottom ID="bottom1" runat="server" />
    </div>
    <script type="text/javascript">
        $("#nav_MyCars").addClass("nav_select");
</script>
</body>
</html>

