<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampaignBeneficiary.aspx.cs" Inherits="manage_campaign_CampaignBeneficiary" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="uc1" TagName="CrmTreetop" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= Resources.CRMTREESResource.CampaignList %></title>
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <link href="/styles/global/main.css?t=20110530" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>

    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>

    <script type="text/javascript" src="/scripts/manage/campaign/Beneficiary.js"></script>
</head>
<body>
    <div id="container">
        <uc1:CrmTreetop runat="server" ID="CrmTreetop" />
        <div id="content">
            <div class="nav_infor">Campaigns > Beneficiary</div>
            <div class="cont_box" style="width: 1000px; height: 770px; position: relative;">
                <div class="report_left" style="width: 992px; left: 2px; top: 2px; position: absolute; margin-bottom: 0px">
                    <div class="report_title_1"><span style="float: left; margin: 4px 4px;">BeneficiaryList</span></div>
                    <div id="container1" style="height: 400px; margin: 0px;">
                        <div class="Table_frame">
                            <%--<div class="Table_frame_header">Need to call customers</div>--%>
                            <%--   <div class="Table_frame_body" >--%>
                            <div id="Phone_List" class="Beneficiarys"></div>
                            <div class="BeneInfo Phone_Info">
                               <p>Telephone communication content</p>
                            </div>
                            <%--   </div>--%>
                        </div>

                        <div class="Table_frame">
                            <%--<div class="Table_frame_header">Need to send text messages to customers</div>--%>
                            <%--  <div class="Table_frame_body">--%>
                            <div id="Messaging_List" class="Beneficiarys"></div>
                             <div class="BeneInfo Messaging_Info">
                               <p>Short message Send content</p>
                            </div>
                            <%--</div>--%>
                        </div>
                        <div class="Table_frame">
                            <%--<div class="Table_frame_header">Need to send Email messages to customers</div>--%>
                            <%--     <div class="Table_frame_body">--%>
                            <div id="Email_List" class="Beneficiarys"></div>
                             <div class="BeneInfo Email_Infos">
                               <p>email Send content</p>
                            </div>
                            <%-- </div>--%>
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
