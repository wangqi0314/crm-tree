<%@ Page Language="C#" AutoEventWireup="true" CodeFile="campaign_list_created .aspx.cs" Inherits="manage_campaign_list_campaign" %>

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
        <input id="<%=name_Maintain %>" type="hidden" value="<%=name_Maintain_value %>" />
        <input id="<%=name_Obtain %>" type="hidden" value="<%=name_Obtain_value %>" />
        <input id="<%=name_Branding %>" type="hidden" value="<%=name_Branding_value %>" />
        <input id="<%=name_Events %>" type="hidden" value="<%=name_Events_value %>" />
        <input id="<%=name_Surveys %>" type="hidden" value="<%=name_Surveys_value %>" />
        <input id="<%=name_Reminders %>" type="hidden" value="<%=name_Reminders_value %>" />
        <input id="<%=name_Newsletters %>" type="hidden" value="<%=name_Newsletters_value %>" />
        <input id="<%=name_Tips %>" type="hidden" value="<%=name_Tips_value %>" />

        <div id="content">
            <div class="nav_infor"><%= Resources.CRMTREESResource.CampaignList %></div>
            <div class="cont_box" style="width: 100%; height: 500px; position: relative; padding: 10px 0px 10px 0px;">
                <div class="detail_menu" style="font-size:medium ; width:100%; float:left;padding-bottom:10px;">
                    <span class="textbox combo" style="width: 150px; height: 26px;margin-right:60px;float:right;">
                        <select id="tts" class="easyui-combobox" style="width: 150px; float: right; height: 25px; margin-right: -50px;" data-options="onSelect:goToCreateCampaign">
                        <option value="<%=name_Maintain_value %>"><%=name_Maintain %>&nbsp&nbsp<%=Resources.CRMTREESResource.FormText %></option>
                        <option value="<%=name_Obtain_value %>"><%=name_Obtain %>&nbsp&nbsp<%=Resources.CRMTREESResource.FormText %></option>
                        <option value="<%=name_Branding_value %>"><%=name_Branding %>&nbsp&nbsp<%=Resources.CRMTREESResource.FormText %></option>
                        <option value="<%=name_Events_value %>"><%=name_Events %>&nbsp&nbsp<%=Resources.CRMTREESResource.FormText %></option>
                        <option value="<%=name_Surveys_value %>"><%=name_Surveys %>&nbsp&nbsp<%=Resources.CRMTREESResource.FormText %></option>
                        <option value="<%=name_Reminders_value %>"><%=name_Reminders %>&nbsp&nbsp<%=Resources.CRMTREESResource.FormText %></option>
                        <option value="<%=name_Newsletters_value %>"><%=name_Newsletters %>&nbsp&nbsp<%=Resources.CRMTREESResource.FormText %></option>
                        <option value="<%=name_Tips_value %>"><%=name_Tips %>&nbsp&nbsp<%=Resources.CRMTREESResource.FormText %></option>
                    </select>
                    </span> 
                    <a id="btn_createBlankCampaign" style="float: right;width:350px; padding: 3px 200px 3px 5px;margin-right: -188px;" class="easyui-linkbutton" onclick="goToCreateCampaign();"><%= Resources.CRMTREESResource.SelectBlank %> </a> 
                </div>
                <div class="easyui-panel" style="width: 25%; height: 90%; position: absolute; z-index: 1; margin-bottom: 5%">
                    <div style="width:92%;height: 40px; padding: 10px 0px 0px 30px;">
                        <a id="btn_useSelectedTemplate" style="padding: 3px 10px 3px 10px;" class="easyui-linkbutton" onclick="goToEditCampaign();"><%= Resources.CRMTREESResource.SelecetTemplate %></a>
                    </div>
                    <div style="font-size:16px;font-weight:bold;background-color:#FFD757;height: 23px;padding: 5 5 0 10;"><%= Resources.CRMTREESResource.TemplList %></div>
                    <div class="easyui-accordion" style="width:100%;" data-options="state:'closed',selected:null">
                        <div title="<%=name_Maintain %>" data-options="tools:[{
                            iconCls:'icon-add',
                            handler:function(){
                                add_campaign();
                            }
                        }]">
                            <ul class="easyui-tree">
                                <li data-options="state:'closed'">
                                    <span>常规活动</span>
                                <ul>
                                    <% foreach (string key in ls_name_Maintain.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Maintain[key].Split('_')[0] %>','<%=ls_name_Maintain[key].Split('_')[1] %>')"><%=key %></a></li>
                                    <%} %>
                                </ul></li>
                                <li data-options="state:'closed'" >
                                    <span>季节性活动</span>
                                <ul>
                                    <% foreach (string key in ls_name_Maintain.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Maintain[key].Split('_')[0] %>','<%=ls_name_Maintain[key].Split('_')[1] %>')"><%=key %></a></li>
                                    <%} %>
                                </ul></li>
                            </ul>
                        </div>
                        <div title="<%=name_Obtain %>" data-options="tools:[{
                            iconCls:'icon-add',
                            handler:function(){
                                add_campaign();
                            }
                        }]">
                            <ul class="easyui-tree">
                                     <% foreach (string key in ls_name_Obtain.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Obtain[key].Split('_')[0] %>','<%=ls_name_Obtain[key].Split('_')[1] %>')"><%=key %></a> </li>
                                    <%} %>
                                <ul>
                                    <% foreach (string key in ls_name_Obtain.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Obtain[key].Split('_')[0] %>','<%=ls_name_Obtain[key].Split('_')[1] %>')"><%=key %></a> </li>
                                    <%} %>
                            </ul>
                        </div>
                        <div title="<%=name_Branding %>" data-options="tools:[{
                            iconCls:'icon-add',
                            handler:function(){
                                add_campaign();
                            }
                        }]">
                             <ul class="easyui-tree">
                                <li data-options="state:'closed'">
                                    <span>季节性活动</span>
                                <ul>
                                   <% foreach (string key in ls_name_Branding.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Branding[key].Split('_')[0] %>','<%=ls_name_Branding[key].Split('_')[1] %>')"><%=key %></a> </li>
                                    <%} %>
                                </ul></li>
                            </ul>
                        </div>
                        <div title="<%=name_Events %>" data-options="tools:[{
                            iconCls:'icon-add',
                            handler:function(){
                                add_campaign();
                            }
                        }]">
                             <ul class="easyui-tree">
                                <li data-options="state:'closed'">
                                    <span>季节性活动</span>
                                <ul>
                                   <% foreach (string key in ls_name_Events.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Events[key].Split('_')[0] %>','<%=ls_name_Events[key].Split('_')[1] %>')"><%=key %></a> </li>
                                    <%} %>
                                </ul></li>
                             </ul>
                        </div>
                        <div title="<%=name_Surveys %>" data-options="tools:[{
                            iconCls:'icon-add',
                            handler:function(){
                                add_campaign();
                            }
                        }]">
                            <ul class="easyui-tree">
                                    <% foreach (string key in ls_name_Surveys.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Surveys[key].Split('_')[0] %>','<%=ls_name_Surveys[key].Split('_')[1] %>')"><%=key %></a> </li>
                                    <%} %>
                            </ul>
                        </div>
                        <div title="<%=name_Reminders %>" data-options="tools:[{
                            iconCls:'icon-add',
                            handler:function(){
                                add_campaign();
                            }
                        }]">
                             <ul class="easyui-tree">
                                   <% foreach (string key in ls_name_Reminders.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Reminders[key].Split('_')[0] %>','<%=ls_name_Reminders[key].Split('_')[1] %>')"><%=key %></a> </li>
                                    <%} %>
                            </ul>
                        </div>
                        <div title="<%=name_Newsletters %>" data-options="tools:[{
                            iconCls:'icon-add',
                            handler:function(){
                                add_campaign();
                            }
                        }]">
                            <ul class="easyui-tree">
                                    <% foreach (string key in ls_name_Newsletters.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Newsletters[key].Split('_')[0] %>','<%=ls_name_Newsletters[key].Split('_')[1] %>')"><%=key %></a> </li>
                                    <%} %>
                            </ul>
                        </div>
                        <div title="<%=name_Tips %>" data-options="tools:[{
                            iconCls:'icon-add',
                            handler:function(){
                                add_campaign();
                            }
                        }]">
                            <ul class="easyui-tree">
                                    <% foreach (string key in ls_name_Tips.Keys)
                                        { %>
                                    <li><a href="#" onclick="addPanel('<%=key %>','<%=ls_name_Tips[key].Split('_')[0] %>','<%=ls_name_Tips[key].Split('_')[1] %>')"><%=key %></a> </li>
                                    <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 25%; height: 95%; border: 0; margin-bottom: 2.5%"></div>
                <div id="tt" style="width: 75%; height: 90%; float: left;" class="easyui-tabs" data-options="tools:'#tab-tools'">
                </div>
                <div id="tab-tools">
                    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="edit_campaign();"><%= Resources.CRMTREEResource.btnEdit %></a>
                    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true" onclick="use_campaign();"><%= Resources.CRMTREEResource.btnUse %></a>
                    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="removePanel()"><%= Resources.CRMTREEResource.btnRemove %></a>

                </div>
            </div>
            <uc1:CrmTreebottom runat="server" ID="CrmTreebottom" />
        </div>
</body>
</html>
<script type="text/javascript">
    $("#nav_Campaigns").addClass("nav_select");
    $('#tt').tabs({
        onSelect: function (title) {
            JavascriptPagination(1);
        }
    });
    var tittleList = "";
    var cgCode = "";

    $('#tt').tabs({
        onClose: function (title, index) {
            tittleList = tittleList.replace(title, "");
        }
    });
    function addPanel(tittle, content, cgcode) {
        cgCode = cgcode;
        if (tittleList != "" && tittleList.indexOf(tittle) >= 0) {
            $('#tt').tabs('select', tittle);
        }
        else {
            $('#tt').tabs('add', {
                title: tittle,
                content: '<div style="padding:10"><iframe style="width:100%;Height:95%" src="/plupload/file/' + content + '\"/></div>',
                closable: true
            });
            tittleList = tittle + tittleList;
        }
    }
    function removePanel() {
        var tab = $('#tt').tabs('getSelected');
        if (tab) {
            var index = $('#tt').tabs('getTabIndex', tab);
            $('#tt').tabs('close', index);
        }
    }
    function goToEditCampaign()
    {
        var title = $('.tabs-selected').text();
        if (cgCode =="")
            alert("Please select campaign detail!")
        else
            window.location.href = "/manage/campaign/Campaign.aspx?T=1&CG_Code="+cgCode;

    }
    function goToCreateCampaign() {
        
        var temp = $('#tts').combobox('getValue');
        if (temp == "")
            alert("Please select campaign title!")
        else
            window.location.href = "/manage/campaign/Campaign.aspx?CT="+temp;

    }

    function add_campaign() {
        var temp = $('#tts').combobox('getValue');
        if (temp == "")
            alert("Please select campaign title!")
        else
            window.location.href = "/manage/campaign/Campaign.aspx?T=2&CT=" + temp;
    }

    function edit_campaign() {
        var title = $('.tabs-selected').text();
        if (cgCode == "")
            alert("Please select campaign detail!")
        else
            window.location.href = "/manage/campaign/Campaign.aspx?T=2&CG_Code=" + cgCode;
    }

    function use_campaign() {
        var title = $('.tabs-selected').text();
        if (cgCode == "")
            alert("Please select campaign detail!")
        else
            window.location.href = "/manage/campaign/Campaign.aspx?T=1&CG_Code=" + cgCode;
    }
</script>
