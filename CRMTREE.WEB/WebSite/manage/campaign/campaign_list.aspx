<%@ Page Language="C#" AutoEventWireup="true" CodeFile="campaign_list.aspx.cs" Inherits="manage_campaign_list_campaign" %>

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
            <%--<div class="nav_infor"><%= Resources.CRMTREESResource.CampaignList %></div>--%>
            <div class="nav_infor" style="height: 11px;"><%=CurrentPage%></div>
             <div class="cont_box" style="width:97%;height:500px; position:relative; padding: 10px;" >
                <div id="tt" class="easyui-tabs" data-options="fit:true,selected:0,plain:true,border:true,tabPosition:'left',headerWidth:160,tabWidth:160" style="border-bottom: 1px solid #B1C242;">
                </div>
            </div>
        </div>
        <uc1:CrmTreebottom runat="server" ID="CrmTreebottom" />
    </div>
     <script type="text/javascript">
         $("#nav_<%=MI_Name %>").addClass("nav_select");
        </script>
</body>
</html>
<script type="text/javascript">
    //$("#nav_Campaigns").addClass("nav_select");

    var _params = $.getParams();
    var _camp_list = {
        $tabs: $("#tt"),

        getTabCode: function () {
            var code = 0;
            var tab = _camp_list.$tabs.tabs('getSelected');
            if (tab) {
                code = tab[0].id;
            }
            return code;
        },

        getTabIndex: function (id) {
            var tabIndex = -1;
            var tabs = _camp_list.$tabs.tabs('tabs');
            for (var i = 0, len = tabs.length; i < len; i++) {
                var tab = tabs[i];
                if (tab[0].id == id) {
                    tabIndex = _camp_list.$tabs.tabs('getTabIndex', tab);
                    break;
                }
            }
            return tabIndex;
        },
        addTab: function (o) {
            if (typeof o != "object") { return; }

            var index = _camp_list.getTabIndex(o.value);
            if (index >= 0) {
                _camp_list.$tabs.tabs('select', index);
            } else {
                _camp_list.$tabs.tabs('add', {
                    id: o.value,
                    title: $.trim(o.text),
                    content:['<div class="cont_box" style="position: relative;">',
	                    '<div class="box clearfix">',
	                    '<div class="box-bg" style="margin-top:-5px;">',
		                    '<div class="pt10" style="width: 100%;padding:1px;margin-left: -26px;">',
	                    '</div>',
                    '</div>'].join('')
                });
            }
        },
        init: function () {
            var pid = 4093;
            var cat = _params.CA;
            if (cat > 0) {
                if (cat == 1) {
                    pid = 4093;
                } else if (cat == 2) {
                    pid = 4170;
                } else if (cat == 3) {
                    pid = 4184;
                }
            }
            if (cat == 2) {
                _camp_list.$tabs.tabs({
                    width:165,
                    headerWidth: 164
                });
            }
            $.getWords([pid], function (data) {
                if (data) {
                    $.each(data["_" + pid], function (i, o) {
                        _camp_list.addTab(o);
                    });
                    var iLoad=1
                     var index = 1;
                    if (_params.CT > 0) {
                        index = _camp_list.getTabIndex(_params.CT);
                        index = index >= 0 ? index : 0;
                    }
                    if (index == 0) iLoad = 0; 

                    _camp_list.$tabs.tabs({
                        onSelect: function (title,index) {
                            JavascriptPagination(1,iLoad);
                            ExpendSearch();
                        }
                    });
                    iLoad = 0;
                    if (index > 0) _camp_list.$tabs.tabs('select', index);
               }
            });
        }
    };

    _camp_list.init();
</script>
