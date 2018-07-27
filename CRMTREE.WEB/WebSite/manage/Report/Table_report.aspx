<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Table_report.aspx.cs" Inherits="manage_campaign_list_campaign" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="uc1" TagName="CrmTreetop" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= Resources.CRMTREESResource.ReportsTab %></title>
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <link href="/styles/global/main.css?t=20110530" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>
    <script type="text/javascript" src="/scripts/manage/Report/list_report.js"></script>

    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _guid = '<%=Guid.NewGuid()%>';
        window.top[_guid] = window.self;
        var _lng = { campaign: '<%=Resources.CRMTREEResource.cmp_title_parameter %>' };
    </script>
</head>
<body>
    <div id="container">
        <uc1:CrmTreetop runat="server" ID="CrmTreetop" />
        <input id="<%=name_Table %>" type="hidden" value="<%=name_Table_value %>" />
        <input id="<%=name_Chart %>" type="hidden" value="<%=name_Chart_value %>" />
        <div id="content">
            <%--<div class="nav_infor"><%= Resources.CRMTREESResource.ListOfReports %></div>--%>
            <div class="nav_infor" style="height: 11px;"><%=CurrentPage%></div>
            <div class="cont_box" style="width: 95%; height: 500px; position: relative; padding: 20px;">
                <div id="tt" class="easyui-tabs" data-options="fit:true,selected:0,plain:true,border:true" style="border-bottom: 1px solid #B1C242;">
                    <div title="<%= Resources.CRMTREESResource.TablesTab %>" data-options="iconCls:'icon-contacts-table'" style="overflow: auto; padding: 3px;">
                        <div class="report_title_1">
                            <span style="float: left; margin: 4px 4px;"><%= Resources.CRMTREESResource.ListOfReports %></span>
                            <%--<div style="float: right; margin-right:5px;margin-top:-5px;">
                                <img style="cursor: pointer;" src="/images/arrow_down.png" />
                                <img class="Cam_add" style="cursor: pointer;" src="/images/icon_add.png" />
                            </div>
                            <div style="float: right; margin-top: 4px; margin-right: 5px;">
                                <img class="Beneficiary" style="cursor: pointer;" title="Activities of the beneficiary" src="/images/Customer/Personal_24.png" />
                            </div>--%>
                        </div>
                        <div class="box clearfix">
                            <div class="box-bg">
                                <div id="divGridList1" class="pt10" style="width: 940px; padding: 1px; margin-left: -11px;">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div title="<%= Resources.CRMTREESResource.ChartsTab %>" data-options="iconCls:'icon-large-chart'" style="overflow: auto; padding: 3px;">
                        <div class="report_title_1">
                            <span style="float: left; margin: 4px 4px;"><%= Resources.CRMTREESResource.ListOfReports %></span>
                            <%--<div style="float: right; margin-right:5px;margin-top:-5px;">
                                <img style="cursor: pointer;" src="/images/arrow_down.png" />
                                <img class="Cam_add" style="cursor: pointer;" src="/images/icon_add.png" />
                            </div>
                            <div style="float: right; margin-top: 4px; margin-right: 5px;">
                                <img class="Beneficiary" style="cursor: pointer;" title="Activities of the beneficiary" src="/images/Customer/Personal_24.png" />
                            </div>--%>
                        </div>
                        <div title="<%=name_Chart %>" data-options="iconCls:'icon-contacts-chart'" style="overflow: auto; padding: 3px;">
                            <div class="box clearfix">
                                <div class="box-bg">
                                    <div id="divGridList" class="pt10" style="width: 940px; padding: 1px; margin-left: -11px;">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <uc1:CrmTreebottom runat="server" ID="CrmTreebottom" />
        </div>
    </div>
     <script type="text/javascript">
         $("#nav_<%=MI_Name %>").addClass("nav_select");
        </script>
</body>
</html>
<script type="text/javascript">
    $('#tt').tabs({
        //selected:1,
        onSelect: function (title, index) {
            JavascriptPagination(1);
        }
    });
</script>
