<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list_campaign.aspx.cs" Inherits="manage_campaign_list_campaign" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="uc1" TagName="CrmTreetop" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= Resources.CRMTREESResource.CampaignList %></title>
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <link href="/styles/global/main.css?t=20110530" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>
    <script type="text/javascript" src="/scripts/manage/campaign/list_campaign.js"></script>

    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>
</head>
<body>
    <div id="container">
        <uc1:CrmTreetop runat="server" ID="CrmTreetop" />

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
                        <div class="searchCont pr pt10" id="search">
                            <p class="mb10">
                                Keywords：<input class="input-text w150 SetInputStyle" type="text" id="txtKeyWords" onfocus="SetKeySearch();" />
                                PageSize：<input class="input-text SetInputStyle w30" type="text" id="txtPageSize" onfocus="SetKeySearch();" maxlength="4" value="10" />
                                <a href="javascript:;" class="btn btnSearch" id="btnSearch"><i>icon</i><span>search</span></a>
                            <p>
                        </div>
                        <div id="divGridList" class="pt10">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc1:CrmTreebottom runat="server" ID="CrmTreebottom" />
    </div>
</body>
</html>
<script type="text/javascript">
    $("#nav_Campaigns").addClass("nav_select");
</script>
