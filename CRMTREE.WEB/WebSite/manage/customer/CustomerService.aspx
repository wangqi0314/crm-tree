﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerService.aspx.cs" Inherits="manage_customer_CustomerService" %>

<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="CrmTreetop" TagName="top"%>
<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="CrmTreebottom" TagName="bottom"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= Resources.CRMTREEResource.CustomerService %></title>
    <link href="/css/Dashboard.css" rel="stylesheet" />
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
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
    </style>
</head>
<body>
    <form id="form1">
        <div id="container">
            <CrmTreetop:top ID="top" runat="server" />
            <div id="content">
                <div class="nav_infor" ><%= Resources.CRMTREEResource.CustomerService %></div>
                <div class="cont_box" style="height:500px; padding: 2px;">
                    <iframe id="ctrl_iframe" src="" style="width:100%;height:100%;" frameborder="0" border="0" scrolling="no"></iframe>
                </div>
            </div>
            <CrmTreebottom:bottom ID="bottom" runat="server" />
        </div>
    </form>
    <div id="NUI_mask" class="nui-mask">fixs</div>
    <script type="text/javascript">
        //$("#nav_Campaigns").addClass("nav_select");

        var _params = $.getParams();
        var params = "";
        if (_params.HD > 0) {
            params += "&HD=" + _params.HD;
        }
        if ($.trim(_params.RP) != "") {
            params += "&RP=" + _params.RP;
        }
        if (_params.CT > 0) {
            params += "&CT=" + _params.CT;
        }
        for (var i = 1; i <= 10; i++) {
            var pName = 'P' + i;
            var v = $.trim(_params[pName]);
            if (v) {
                params += "&" + pName + "=" + v;
            }
        }
        $("#ctrl_iframe").attr("src", "/manage/customer/CustomerServiceOperator.aspx?_cmd=cp" + params);
    </script>
</body>
</html>
 