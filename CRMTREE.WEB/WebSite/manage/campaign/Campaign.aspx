<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Campaign.aspx.cs" Inherits="manage_campaign_Campaign" %>

<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="CrmTreetop" TagName="top" %>
<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="CrmTreebottom" TagName="bottom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Campaign</title>
    <link href="/css/Dashboard.css" rel="stylesheet" />
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>


    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <script src="/scripts/common/json2.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>

    <style type="text/css">
        #content2 {
            margin: auto float: left;
            /*clear: both;*/
        }

        .clear {
            clear: both;
        }
    </style>
</head>
<body>
    <form id="form1">
        <div id="container">
            <CrmTreetop:top ID="top" runat="server" />
            <div id="content">
                <div class="nav_infor" style="height: 11px;"><%=CurrentPage%></div>
                <div class="cont_box" style="height: 1000px; padding: 2px;">
                    <iframe id="ctrl_iframe" src="" style="width: 100%; height: 100%;display:block" frameborder="0" border="0" scrolling="no" ></iframe>

                </div>
                <%--<div class="clear"></div>--%>
            </div>
            <CrmTreebottom:bottom ID="bottom" runat="server" />
        </div>
    </form>
    <div id="NUI_mask" class="nui-mask">fixs</div>
    <script type="text/javascript">
        $("#nav_<%=MI_Name %>").addClass("nav_select");
    </script>
    <script type="text/javascript">
     

        //$("#nav_Campaigns").addClass("nav_select");

        var _params = $.getParams();
        var params = "";
        if ($.trim(_params.CT) != "") {
            params += "&CT=" + _params.CT;
        }
        if ($.trim(_params.CG_Code) != "") {
            params += "&CG_Code=" + _params.CG_Code;
        }
        if ($.trim(_params.T) != "") {
            params += "&T=" + _params.T;
        }
        if ($.trim(_params.CA) != "") {
            params += "&CA=" + _params.CA;
        }
        $("#ctrl_iframe").attr("src", "/manage/campaign/CampaignManagerN.aspx?_cmd=cp" + params);
    </script>
</body>
</html>
