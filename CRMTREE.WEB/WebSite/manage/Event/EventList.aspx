<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventList.aspx.cs" Inherits="manage_Event_EventList" %>
<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="uc1" TagName="CrmTreetop" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <link href="/styles/global/main.css?t=20110530" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>
    <script type="text/javascript" src="/scripts/manage/Event/EventList.js"></script>

    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>
</head>
<body>
    <div id="container">
        <uc1:crmtreetop runat="server" id="CrmTreetop" />

        <div id="content">
            <div class="nav_infor"><%= Resources.CRMTREESResource.CampaignList %></div>
            <div class="cont_box" style="position: relative;">
                <div class="report_title_1">
                    <span style="float: left; margin: 4px 4px;"><%= Resources.CRMTREESResource.CampaignList %></span>
                    <div style="float: right; margin-right: 5px; margin-top: -5px;">
                        <%--<img style="cursor: pointer;" src="/images/arrow_down.png" />--%>
                        <img class="Cam_add" style="cursor: pointer;" src="/images/icon_add.png" />
                    </div>
                    <div style="float: right; margin-top: 4px; margin-right: 5px;">
                        <img class="Beneficiary" style="cursor: pointer;" title="Activities of the beneficiary" src="/images/Customer/Personal_24.png" />
                    </div>
                </div>
                <div class="box clearfix">
                    <div class="box-bg">
                        <div id="divGridList" class="pt10">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc1:crmtreebottom runat="server" id="CrmTreebottom" />
    </div>
    <script type="text/javascript">
        $("#nav_Events").addClass("nav_select");
    </script>
</body>
</html>
