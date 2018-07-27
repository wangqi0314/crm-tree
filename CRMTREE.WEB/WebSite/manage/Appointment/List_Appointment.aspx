<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List_Appointment.aspx.cs" Inherits="manage_Appointment_List_Appointment" %>

<%@ Register Src="~/control/top.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/control/bottom.ascx" TagName="bottom" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= Resources.CRMTREEResource.Appointment_List %></title>
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <link href="/styles/global/main.css?t=20110530" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>
    <script src="/scripts/manage/Appointment/List_Appointment.js"></script>
</head>
<body>
    <div id="container">
        <uc1:top ID="top1" runat="server" />
        <div id="content">
            <div class="nav_infor">
                <a class="brown" href="#"><%= Resources.CRMTREEResource.Appointment %></a>
                &nbsp;&gt;&nbsp;
                <%= Resources.CRMTREEResource.Appointment_List %>
            </div>
            <div class="cont_box">
                <div class="report_title">
                    <span style="float: right; margin-top: -10px;">
                        <img style="cursor: pointer;" src="/images/arrow_down.png" />
                        <img class="App_Add" style="cursor: pointer;" src="/images/icon_add.png" />
                    </span><%= Resources.CRMTREEResource.Appointment_List %>
                </div>
                <div class="box clearfix">
                    <div class="box-bg">
                        <div class="searchCont pr pt10" id="search">
                            <p class="mb10">
                                Keywords：<input class="input-text w150 SetInputStyle" type="text" id="txtKeyWords" onfocus="SetKeySearch();" />
                                PageSize：<input class="input-text SetInputStyle w30" type="text" id="txtPageSize" onfocus="SetKeySearch();" maxlength="4" value="5" />
                                <a href="javascript:;" class="btn btnSearch" id="btnSearch"><i>icon</i><span>search</span></a>
                            </p>
                        </div>
                        <div id="divGridList" class="pt10">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc2:bottom ID="bottom1" runat="server" />
    </div>
</body>
<script type="text/javascript">
    $("#nav_Appointments").addClass("nav_select");
</script>
</html>
