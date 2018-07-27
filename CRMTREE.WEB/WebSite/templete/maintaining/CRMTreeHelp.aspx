<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRMTreeHelp.aspx.cs" Inherits="manage_Help_CRMTreeHelp" %>

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
        h3, em {
            margin:0;
            padding:0;
        }
        em {
            font-style: normal;
            color: #C00;
        }
        .t {
            font-weight: normal;
            font-size: medium;
            color:blue;
            line-height:1.45;
        }
        .d{
            font-size:13px;
        }

        .datagrid-header, .datagrid-toolbar, .datagrid-pager, .datagrid-footer-inner{
            border:0;
        }
    </style>
</head>
<body>
     <div class="easyui-layout" data-options="fit:true">
        <%--<div data-options="region:'north'" style="height:40px;overflow:hidden;">
            <table style="width:100%;height:100%;">
                <tr>
                    <td style="text-align:right;vertical-align:middle;padding-right:5px;">
                        <input id="ex_search" class="easyui-textbox" data-options="width:200, 
                            prompt: '<%=Resources.CRMTREEResource.SearchDocumentsPrompt %>',
                            icons:[{
                                iconCls:'icon-search',
                                handler: _help.search
                            }]"
                        />
                    </td>
                </tr>
            </table>
        </div>--%>
        <%--<div data-options="region:'west',collapsible:false,title:'<%=Resources.CRMTREESResource.TemplList %>'" style="width:300px;padding:5px;overflow-y:scroll;">--%>
        <div data-options="region:'west'" style="width:170px;overflow-y:auto;">
            <div id="c_tabs" class="easyui-tabs" data-options="fit:true,tabPosition:'bottom',border:false"><%--tabWidth:100,--%>
                <div title="<%=Resources.CRMTREEResource.Directory %>" data-options="iconCls:'icon-help'" style="padding:5px;">
                    <ul id="c_tree" class="easyui-tree" data-options="fit:true, formatter:_help.formatter,onClick: _help.onClick"></ul>
                </div>
                <%--<div title="<%=Resources.CRMTREEResource.Search %>" data-options="iconCls:'icon-search'" style="padding:5px;">
                    <table id="c_dg" class="easyui-datagrid" data-options="fit:true,border:false,fitColumns:true,singleSelect:true,showHeader:false,showFooter:false">
                        <thead>
                            <tr>
                                <th field="Search"><%=Resources.CRMTREEResource.Search %></th>
                            </tr>
                        </thead>
                    </table>
                </div>--%>
                <table id="c_dg"></table>
            </div>     
        </div>
        <div data-options="region:'center'" style="padding:0px">
            <div id="c_tabs_help" class="easyui-tabs" data-options="fit:true,border:false,plain:false,tools:'#c_tabs_help_tools'">
            </div>
            <div id="c_tabs_help_tools" style="border-top:0;">
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="_help.remove();"><%= Resources.CRMTREEResource.ap_buttons_close %></a>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Reports/Reports.aspx';
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

    _help = {
        $tabs: $("#c_tabs_help"),
        $tree: $("#c_tree"),
        $dg: $("#c_dg"),
        //_key_words:'',
        remove: function () {
            _help.removeTab();
        },

        getTabIndex: function (id) {
            var tabIndex = -1;
            var tabs = _help.$tabs.tabs('tabs');
            for (var i = 0, len = tabs.length; i < len; i++) {
                var tab = tabs[i];
                if (tab[0].id == id) {
                    tabIndex = _help.$tabs.tabs('getTabIndex', tab);
                    break;
                }
            }
            return tabIndex;
        },
        addTab: function (o) {
            if (typeof o != "object") { return; }

            var index = _help.getTabIndex(o.id);
            if (index >= 0) {
                _help.$tabs.tabs('select', index);
            } else {
                _help.$tabs.tabs('add', {
                    id: o.id,
                    title: $.trim(o.title),
                    closable: true
                });

                //alert(o.fileName);
                //alert(o.id);

                if (o.fileName == '' || o.fileName == null)
                    _help.loadTabIframe(o.id, '/templete/help/masterpage.html');
                else
                    _help.loadTabIframe(o.id, '/templete/help/' + o.fileName);
                
            }
        },
        defaultPage:function()
        {
            _help.addTab({
                id: 0,
                title: '<%=Resources.CRMTREEResource.Help_General%>',
                fileName: null
            });

            //$("#c_tabs").tabs('hide', '搜索');
        },
        removeTab: function () {
            var tab = _help.$tabs.tabs('getSelected');
            if (tab) {
                var index = _help.$tabs.tabs('getTabIndex', tab);
                _help.$tabs.tabs('close', index);
            }
        },
        loadTabIframe: function (id, src) {
            var index = _help.getTabIndex(id);
            var $tab = _help.$tabs.tabs('getTab', index);
            if (null == $tab) return;
            var $tabBody = $tab.panel('body');
            var $frame = $('iframe', $tabBody);
            if ($frame.length > 0) return;

            $tabBody.css({ 'overflow': 'hidden', 'position': 'relative' });
            var $iframe = $('<iframe src="' + src + '" style="width:100%;height:100%;" frameborder="0" border="0"></iframe>');
            $iframe.appendTo($tabBody);
        },
        toTreeNode: function (data, idField, parentIdField) {
            data = data ? data : [];
            data = $.map(data, function (d, i) {
                var text = $.trim(d.TL_Text);
                return {
                    id: d[idField],
                    pid: d[parentIdField],
                    text: text,
                    attributes: {
                        title: text,
                        id: d[idField],
                        fileName: $.trim(d.HD_File_Name)
                    }
                };
            });
            return data;
        },
        getTree: function (data, idField, parentIdField) {
            data = _help.toTreeNode(data, idField, parentIdField);

            childrenField = "children";
            idField = "id";
            parentIdField = "pid";

            var tree = [];

            //建立快速索引
            var hash = {};
            for (var i = 0, l = data.length; i < l; i++)
            {
                var d = data[i];
                hash[d[idField]] = d;
            }

            //数组转树形        
            for (var i = 0, l = data.length; i < l; i++)
            {
                var d = data[i];
                var parentID = $.trim(d[parentIdField]);
                //如果没有父节点, 是第一层
                if (parentID == "" || parentID == "0")   
                {
                    tree.push(d);
                    continue;
                }

                var parent = hash[parentID];
                //如果没有父节点, 是第一层
                if (!parent)     
                {
                    tree.push(d);
                    continue;
                }

                var children = parent[childrenField];
                if (!children)
                {
                    children = [];
                    parent.state = 'closed';
                    parent[childrenField] = children;
                }
                children.push(d);
            }

            return tree;
        },

        expandTo: function () {
            _params.TL_Code = 45;
            if (_params && _params.TL_Code > 0) {
                var node = _help.$tree.tree('find', _params.TL_Code);
                if (node.target) {
                    _help.$tree.tree('expandTo', node.target).tree('select', node.target);
                    node.target.click();
                }
            }
        },
        onClick: function (node) {
            var attrs = node.attributes;
            if (attrs && $.trim(attrs.fileName) != "") {
                _help.addTab(attrs);
            }
        },
        formatter:function(node){
            var s = node.text;
            var attrs = node.attributes;
            if (attrs && $.trim(attrs.fileName) != "") {
                s = '<span style="color:blue;text-decoration:underline;">' + s + '</span>';
            }
            return s;
        },
        search:function(){
            var v = $.trim($("#ex_search").textbox('getValue'));
            if (v == "") { return; }
            _help._key_words = v;
            $("#c_tabs").tabs('select', 1);
            //_help.$dg.datagrid('loadData', []);
            $.mask.show();
            var params = { action: 'Get_Help_Documents_Search', HD_Key_Words: v };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                $.mask.hide();
                var data = [];
                if (CheckResponse(res)) {
                    data = res;
                }
                _help.$dg.datagrid('loadData', data);
            }, "json");
        },

        bind: function () {
            var params = { action: 'Get_Auth_Tab_Links' };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (CheckResponse(res)) {
                    var tree = _help.getTree(res, "TL_Code", "TL_Parent");
                    _help.$tree.tree('loadData', tree);
                    _help.expandTo();
                }
            }, "json");
        },
        _init_dg:function(){
            var cardview = $.extend({}, $.fn.datagrid.defaults.view, {
                renderRow: function (target, fields, frozen, rowIndex, rowData) {
                    var cc = [];
                    cc.push('<td colspan=' + fields.length + ' style="padding:5px;border:0;">');
                    if (!frozen) {
                        var title = $.trim(rowData.HD_Title);
                        var desc = $.trim(rowData.HD_Description);
                        var reg = new RegExp(_help._key_words, "ig");
                        //title = title.replace(reg, "<em>" + _help._key_words + "</em>");
                        //desc = desc.replace(reg, "<em>" + _help._key_words + "</em>");
                        //cc.push('<h3 class="t">' + title + '</h3>');
                        //cc.push('<div class="d">' + desc + '</div>');
                    }
                    cc.push('</td>');
                    return cc.join('');
                }
            });

            _help.$dg.datagrid({
                view: cardview,
                onClickRow: function (index, row) {
                    _help.addTab({
                        id: row.TL_Code,
                        title: row.HD_Title,
                        fileName: row.HD_File_Name
                    });
                }
            });
        },
        //_init_search: function () {
        //    var tbx = $("#ex_search").textbox('textbox');
        //    $(tbx).keyup(function (e) {
        //        if (event.keyCode == 13) {
        //            _help.search();
        //        }
        //    });
        //},
        init: function () {
            _help._init_dg();
            //_help._init_search();
           
            _help.defaultPage();
        }
    };

    $(function () {
        _help.init();
        _help.bind();
        
    });
</script>