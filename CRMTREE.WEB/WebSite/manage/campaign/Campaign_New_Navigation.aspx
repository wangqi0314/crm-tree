<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Campaign_New_Navigation.aspx.cs" Inherits="manage_campaign_Campaign_New_Navigation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>

    <script src="/scripts/common/json2.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>

    <script type="text/javascript">
        var _isEn = $.isEn();
        if (!_isEn) {
            document.write('<script src="/scripts/jquery-easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></sc' + 'ript>');
        }
    </script>

    <style type="text/css">
        html, body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            font-size:12px;
        }
    </style>
</head>
<body>
     <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'north'" style="height:40px;overflow:hidden;">
            <table style="width:100%;height:100%;">
                <tr>
                    <td style="text-align:right;vertical-align:middle;padding-right:5px;">
                        <input id="ex_campaign" class="easyui-combobox" data-options="width:200,prompt:'<%= Resources.CRMTREESResource.SelectBlank %>'"/>
                    </td>
                </tr>
            </table>
        </div>
        <%--<div data-options="region:'west',collapsible:false,title:'<%=Resources.CRMTREESResource.TemplList %>'" style="width:300px;padding:5px;overflow-y:scroll;">--%>
         <div data-options="region:'west'" style="width:300px;">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',border:false" style="height:29px;overflow:hidden;border-bottom:1px solid #B1C242;font-size:16px;font-weight:bold;background-color:#FCFAB0;padding:5px;">
                    <%=Resources.CRMTREESResource.TemplList %>
                </div>
                <div data-options="region:'center',border:false" style="padding:5px;overflow-y:auto;">
                    <div id="c_acc_camp" class="easyui-accordion" data-options="selected:null" ></div>
                </div>
            </div>

            <%--<div id="c_acc_camp" class="easyui-accordion" data-options="selected:null" ></div>--%>
        </div>
        <div data-options="region:'center'" style="padding:0px">
            <div id="c_tabs_camp" class="easyui-tabs" data-options="fit:true,border:false,plain:false,tools:'#c_tabs_camp_tools'">
            </div>
            <div id="c_tabs_camp_tools" style="border-top:0;">
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="_campaign_all.edit();"><%= Resources.CRMTREEResource.btnEdit %></a>
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'" onclick="_campaign_all.use();"><%= Resources.CRMTREEResource.btnUse %></a>
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="_campaign_all.remove();"><%= Resources.CRMTREEResource.ap_buttons_close %></a>
            </div>
        </div>
    </div>

    <div id="c_tree_camp_mm" class="easyui-menu" style="width:120px;">
        <div onclick="_campaign_all.edit_camp();" data-options="iconCls:'icon-edit'"><%= Resources.CRMTREEResource.btnEdit %></div>
        <div onclick="_campaign_all.use_camp();" data-options="iconCls:'icon-ok'"><%= Resources.CRMTREEResource.btnUse %></div>
    </div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Campaign/CampaignManager.aspx';
    var _params = $.getParams();

    //阻止事件冒泡
    var stopPropagation = function (e) {
        if (!e) {
            return;
        }
        // If stopPropagation exists, run it on the original event
        if (e.stopPropagation) {
            e.stopPropagation();
        }

        // Support: IE
        // Set the cancelBubble property of the original event to true
        e.cancelBubble = true;
    }

    function CheckResponse(res) {
        var bCheck = true;
        if (!res || res.isOK === false) {
            bCheck = false;
        }
        return bCheck;
    }

    _campaign_all = {
        $tabs: $("#c_tabs_camp"),
        _cg_code: 0,
        get_cg_code: function () {
            var cg_code = 0;
            var tab = _campaign_all.$tabs.tabs('getSelected');
            if (tab) {
                cg_code = tab[0].id;
            } else {
                var msg = _isEn ? "Please select campaign tab!" : "请选择活动标签页！";
                $.msgTips.info(msg);
            }
            return cg_code;
        },
        addNoT: function (ct) {
            if (ct > 0) {
                window.top.location.href = "/manage/campaign/Campaign.aspx?CT=" + ct;
            }
        },
        add: function (ct) {
            if (ct > 0) {
                window.top.location.href = "/manage/campaign/Campaign.aspx?T=2&CT=" + ct;
            }
        },
        edit: function () {
            var cg_code = _campaign_all.get_cg_code();
            if (cg_code > 0) {
                window.top.location.href = "/manage/campaign/Campaign.aspx?T=2&CG_Code=" + cg_code;
            }
        },
        use: function () {
            var cg_code = _campaign_all.get_cg_code();
            if (cg_code > 0) {
                window.top.location.href = "/manage/campaign/Campaign.aspx?T=1&CG_Code=" + cg_code;
            }
        },
        remove: function () {
            _campaign_all.removeTab();
        },
        edit_camp: function () {
            var cg_code = _campaign_all._cg_code;
            if (cg_code > 0) {
                window.top.location.href = "/manage/campaign/Campaign.aspx?T=2&CG_Code=" + cg_code;
            }
        },
        use_camp: function () {
            var cg_code = _campaign_all._cg_code;
            if (cg_code > 0) {
                window.top.location.href = "/manage/campaign/Campaign.aspx?T=1&CG_Code=" + cg_code;
            }
        },


        onClick: function (node) {
            var attrs = node.attributes;
            if (attrs && $.trim(attrs.CG_Filename) != "") {
                _campaign_all.addTab(attrs);
            }
        },

        getTabIndex: function (id) {
            var tabIndex = -1;
            var tabs = _campaign_all.$tabs.tabs('tabs');
            for (var i = 0, len = tabs.length; i < len; i++) {
                var tab = tabs[i];
                if (tab[0].id == id) {
                    tabIndex = _campaign_all.$tabs.tabs('getTabIndex', tab);
                    break;
                }
            }
            return tabIndex;
        },
        addTab: function (o) {
            if (typeof o != "object") { return; }

            var index = _campaign_all.getTabIndex(o.CG_Code);
            if (index >= 0) {
                _campaign_all.$tabs.tabs('select', index);
            } else {
                _campaign_all.$tabs.tabs('add', {
                    id: o.CG_Code,
                    title: $.trim(o.CG_Title),
                    closable: true
                });
                _campaign_all.loadTabIframe(o.CG_Code, '/plupload/file/' + o.CG_Filename);
            }
        },
        removeTab: function () {
            var tab = _campaign_all.$tabs.tabs('getSelected');
            if (tab) {
                var index = _campaign_all.$tabs.tabs('getTabIndex', tab);
                _campaign_all.$tabs.tabs('close', index);
            }
        },
        loadTabIframe: function (id, src) {
            var index = _campaign_all.getTabIndex(id);
            var $tab = _campaign_all.$tabs.tabs('getTab', index);
            if (null == $tab) return;
            var $tabBody = $tab.panel('body');
            var $frame = $('iframe', $tabBody);
            if ($frame.length > 0) return;

            $tabBody.css({ 'overflow': 'hidden', 'position': 'relative' });
            var $iframe = $('<iframe src="' + src + '" style="width:100%;height:100%;" frameborder="0" border="0"></iframe>');
            $iframe.appendTo($tabBody);
        },

        bind: function () {
            var params = { action: 'Get_Campaign_All', P_ID: 4093 };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (CheckResponse(res)) {
                    var accs = res.Table;
                    var cats = res.Table1;
                    var cams = res.Table2;

                    accs = accs ? accs : [];
                    cats = cats ? cats : [];
                    cams = cams ? cams : [];

                    $("#ex_campaign").initSelect({
                        editable: true,
                        showNullItem: false,
                        data: accs,
                        onSelect: function (record) {
                            _campaign_all.addNoT(record.value);
                        }
                    });

                    //accs start
                    $.each(accs, function (i, acc) {
                        var $panel = $("<div></div>");
                        $('#c_acc_camp').accordion('add', {
                            title: acc.text,
                            content: $panel,
                            selected: false,
                            tools: [{
                                iconCls: 'icon-add',
                                handler: function () {
                                    _campaign_all.add(acc.value);
                                }
                            }]
                        });
                        $panel.panel({
                            border: false,
                            //minHeight:100,
                            maxHeight: 200
                        });
                        var $body = $panel.panel('body');
                        $body.css({ padding: 5 });

                        var $cat = $('<ul></ul>');
                        $body.append($cat);

                        var a_cat = [];
                        //cats start
                        $.each(cats, function (k, cat) {
                            if (acc.value == cat.CCC_Camp_Type) {
                                var a_camp = [];
                                //cams start
                                a_camp = $.map(cams, function (cam, j) {
                                    return acc.value == cam.CG_Type && cat.CCC_Code == cam.CG_CCC_Code ? {
                                        id: cam.CG_Code,
                                        text: cam.CG_Title,
                                        attributes: cam
                                    } : null;
                                });
                                //cams end
                                if (a_camp.length > 0) {
                                    a_cat.push({
                                        id: cat.CCC_Code,
                                        text: cat.CCC_Desc,
                                        state: "closed",
                                        children: a_camp
                                    });
                                }
                            }
                        });
                        //cats end

                        var a_camp_o = [];
                        //cams start
                        a_camp_o = $.map(cams, function (cam, l) {
                            return acc.value == cam.CG_Type && $.trim(cam.CG_CCC_Code) == "" ? {
                                id: cam.CG_Code,
                                text: cam.CG_Title,
                                attributes: cam
                            } : null;
                        });
                        //cams end

                        if (a_camp_o.length > 0) {
                            a_cat = a_cat.concat(a_camp_o);
                        }

                        $cat.tree({
                            data: a_cat,
                            onClick: _campaign_all.onClick,
                            onContextMenu: function (e, node) {
                                e.preventDefault();

                                var attrs = node.attributes;
                                if (attrs && attrs.CG_Code > 0) {
                                    _campaign_all._cg_code = attrs.CG_Code;
                                    $(this).tree('select', node.target);
                                    $('#c_tree_camp_mm').menu('show', {
                                        left: e.pageX,
                                        top: e.pageY
                                    });
                                }
                            }
                        });
                    });
                    //accs end
                }
            }, "json");
        }
    };

    $(function () {
        _campaign_all.bind();
    });
</script>