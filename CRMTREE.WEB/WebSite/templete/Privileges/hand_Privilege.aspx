<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hand_Privilege.aspx.cs" Inherits="templete_Privileges_hand_Privilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script src="/scripts/jquery/jquery.extend.js"></script>

    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js"></script>

    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>

    <script type="text/javascript">
        var _isEn = $.isEn();
        if (!_isEn) {
            document.write('<script src="/scripts/jquery-easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></sc' + 'ript>');
        }
        var _pageParam = { a: 1 };
    </script>
</head>
<body>
    <div id="pnl" class="easyui-panel" fit="true" border="false">
        <table id="dg_2"></table>
    </div>

    <div id="tb_dg_2">
        <div id="frm_auth_process" style="padding: 10px 5px; border-bottom: 1px solid #ccc;">
            <div style="padding-right: 5px; display: inline-block">
                <span><%=Resources.CRMTREEResource.UserGroups%>：</span>
                <input id="UG_Groups" class="easyui-combobox" data-options="required:true,novalidate:true" style="width: 130px;" />
            </div>
            <%--<div style="padding-right: 5px; display: inline-block">
                <span><%=Resources.CRMTREEResource.UserGroupisMiple%>：</span>
                <input id="UG_Category" class="easyui-combobox" data-options="required:true,novalidate:true" style="width: 100px;" />
            </div>--%>
        </div>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add',onClick:Pri.hidfunction.add"><%=Resources.CRMTREEResource.em_contacts_add%></a>
    </div>
</body>
</html>
<script>
    var _url_crmhand = '/handler/CRMhandle.aspx';
    $(function () {
        Pri.init();
    });

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
    //设置按钮
    function setBtns(a_o) {
        var a_btns = [];
        $.each(a_o, function (i, o) {
            var btn = $.format([
                    '<a class="easyui-linkbutton l-btn ' + (o.size ? o.size : 'l-btn-small') + ' l-btn-plain" ' + (o.title ? 'title="{title}"' : '')
                    , ' href="javascript:void(0);" ' + (o.clickFun ? 'onclick="{clickFun}"' : '') + '>'
                    , o.icon ? '<span class="l-btn-left l-btn-icon-left">' : '<span class="l-btn-left">'
                    , o.text ? '<span class="l-btn-text">{text}</span>' : '<span class="l-btn-text l-btn-empty">&nbsp;</span>'
                    , o.icon ? '<span class="l-btn-icon {icon}">&nbsp;</span>' : ''
                    , '</span>'
            ].join(''), o);
            a_btns.push(btn);
        });
        return a_btns;
    }
    var Pri = {
        base: {
            id: '#dg_2', _tool: '#tb_dg_2', s1: "#UG_Groups", s2: "#UG_Category",
            title: {
                U1: _isEn ? "User" : "用户",
                U2: _isEn ? "Function" : "方法",
                U3: _isEn ? "Type" : "类别",
                U4: _isEn ? "Run" : "执行",
                U5: _isEn ? "View" : "查看",
                U6: _isEn ? "Edit" : "编辑",
                U7: _isEn ? "Create" : "创建",
                U8: _isEn ? "Delete" : "删除",
                U9: _isEn ? "Copy" : "复制",
                U10: _isEn ? "Print" : "打印",
                P1: _isEn ? "Any User " : "所有用户"
            }
        },
        init: function () {
            Pri._select.loading1();
            Pri._select.loading2();
            Pri._d_Title.createTitle();
        },
        _select: {
            loading1: function () {
                $.post(_url_crmhand, { o: JSON.stringify({ action: 'getGroupList' }) }, function (data) {
                    var $com = $(Pri.base.s1);
                    $com.combobox({
                        valueField: 'UG_Code',
                        textField: 'UG_Name',
                        onSelect: function (record) {
                            Pri._d_Title.loadData(record.UG_Code);
                        }
                    }).combobox('loadData', data).combobox('select', data[0].UG_Code);
                }, "json");
            },
            loading2: function () {
                var $com = $(Pri.base.s2);
                $com.combobox({
                    valueField: 'id',
                    textField: 'value',
                    data: [{
                        id: '1',
                        value: 'Query'
                    }, {
                        id: '2',
                        value: 'MI'
                    }]
                }).combobox('setValue', 1);
            },
        },
        _d_Title: {
            getTitle: function () {

            },
            createTitle: function () {
                var $dg = $(Pri.base.id);
                $dg.datagrid({
                    url: null, toolbar: Pri.base._tool,
                    fit: true, rownumbers: true,
                    singleSelect: true, showHeader: true,
                    border: false,
                    loadMsg: 'Loading....',
                    remoteSort: false,
                    nowrap: false,
                    fitColumns: true,
                    columns: [[
                        {
                            field: 'AU_Code', align: 'left', title: Pri.base.title.U1, width: 100,
                            formatter: function (value, row, index) {
                                return row.AU_Name;
                            },
                            editor: {
                                type: 'combobox', options: {
                                    novalidate: true,
                                    required: true
                                }
                            }
                        },
                        {
                            field: 'FS', align: 'left', title: Pri.base.title.U2, width: 120,
                            formatter: function (value, row, index) {
                                return row.SF_Desc;
                            },
                            editor: {
                                type: 'combobox', options: {
                                    novalidate: true,
                                    required: true
                                }
                            }
                        },
                        {
                            field: 'CT', align: 'left', title: Pri.base.title.U3, width: 150,
                            formatter: function (value, row, index) {
                                return row.CT_RP;
                            },
                            editor: {
                                type: 'combobox', options: {
                                    novalidate: true,
                                    required: true,
                                    valueField: "RP_Code",
                                    textField: "CT_RP"
                                }
                            }
                        },
                        {
                            field: 'UP_Run', align: 'center', title: Pri.base.title.U4, width: 50,
                            formatter: function (value, row, index) {
                                if (value === 'True') {
                                    return "<input type=\"checkbox\" checked=\"checked\" />";
                                } else {
                                    return "<input type=\"checkbox\" />";
                                }
                            }
                        },
                        {
                            field: 'UP_View', align: 'center', title: Pri.base.title.U5, width: 50,
                            formatter: function (value, row, index) {
                                if (value === 'True') {
                                    return "<input type=\"checkbox\" checked=\"checked\" />";
                                } else {
                                    return "<input type=\"checkbox\" />";
                                }
                            }
                        },
                        {
                            field: 'UP_Edit', align: 'center', title: Pri.base.title.U6, width: 50,
                            formatter: function (value, row, index) {
                                if (value === 'True') {
                                    return "<input type=\"checkbox\" checked=\"checked\" />";
                                } else {
                                    return "<input type=\"checkbox\" />";
                                }
                            }
                        },
                        {
                            field: 'UP_Create', align: 'center', title: Pri.base.title.U7, width: 50,
                            formatter: function (value, row, index) {
                                if (value === 'True') {
                                    return "<input type=\"checkbox\" checked=\"checked\" />";
                                } else {
                                    return "<input type=\"checkbox\" />";
                                }
                            }
                        },
                        {
                            field: 'UP_Delete', align: 'center', title: Pri.base.title.U8, width: 50,
                            formatter: function (value, row, index) {
                                if (value === 'True') {
                                    return "<input type=\"checkbox\" checked=\"checked\" />";
                                } else {
                                    return "<input type=\"checkbox\" />";
                                }
                            }
                        },
                        {
                            field: 'UP_Copy', align: 'center', title: Pri.base.title.U9, width: 50,
                            formatter: function (value, row, index) {
                                if (value === 'True') {
                                    return "<input type=\"checkbox\" checked=\"checked\" />";
                                } else {
                                    return "<input type=\"checkbox\" />";
                                }
                            }
                        },
                        {
                            field: 'UP_Print', align: 'center', title: Pri.base.title.U10, width: 50,
                            formatter: function (value, row, index) {
                                if (value === 'True') {
                                    return "<input type=\"checkbox\" checked=\"checked\" />";
                                } else {
                                    return "<input type=\"checkbox\" />";
                                }
                            }
                        },
                        {
                            field: 'UP_Print1', align: 'center', title: 'Disable', width: 50,
                            formatter: function (value, row, index) {
                                if (value === 'True') {
                                    return "<input type=\"checkbox\" checked=\"checked\" />";
                                } else {
                                    return "<input type=\"checkbox\" />";
                                }
                            }
                        }, {
                            field: 'btnLast', align: 'center', width: 60,
                            formatter: function (value, row, index) {
                                var a_o = [
                                { icon: 'icon-remove', title: '<%=Resources.CRMTREEResource.btnRemove%>', clickFun: 'Pri.hidfunction.remove(event,this);' }
                                ];
                                return setBtns(a_o, true).join('');
                            }
                        }
                    ]],
                    onClickCell: Pri.hidfunction.onClickCellEvent
                });
            },
            loadData: function (code) {
                $.post(_url_crmhand, { o: JSON.stringify({ action: 'get_Pri', UG_Code: code }) }, function (data) {
                    if (data == null) {
                        data = [];
                    }
                    var $dg = $(Pri.base.id);
                    $dg.datagrid('loadData', data);

                }, "json");
            }
        },
        hidfunction: {
            //删除
            remove: function (e, target) {
                var $dg = $(Pri.base.id);
                var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
                var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
                if (rowData.id > 0) {
                    $.confirmWindow.tempRemove(function () {
                        $dg.datagrid('deleteRow', rowIndex);
                    });
                } else {
                    if (rowIndex >= 0) {
                        $dg.datagrid('deleteRow', rowIndex);
                        //$.post(_url_crmhand, {
                        //    o: JSON.stringify({
                        //        action: 'deleteKPI',
                        //        UType: 2, DE_Code: -1, PN_Code: rowData.PDN_Code, PT_Code: rowData.KPT_Code
                        //    })
                        //}, function (data) {

                        //}, "json");
                    }
                }
                stopPropagation(e);
            },
            add: function () {
                Pri.hidfunction.endEditingNoValidCell();
                var $dg = $(Pri.base.id);
                var d = {};
                var row = $dg.datagrid('getSelected');
                var rowIndex = undefined;
                if (row) {
                    rowIndex = $dg.datagrid('getRowIndex', row) + 1;
                } else {
                    rowIndex = $dg.datagrid('getRows').length;
                }
                $dg.datagrid('insertRow', { index: rowIndex, row: d });
                $dg.datagrid('selectRow', rowIndex);
            },
            //结束编辑不验证
            endEditingNoValidCell: function () {
                if (Pri.$editIndex == undefined) {
                    return;
                }
                var $dg = $(Pri.base.id);
                var _index = Pri.$editIndex;

                var _ed1 = $dg.datagrid('getEditor', { index: _index, field: 'AU_Code' });
                if (_ed1 != null) {
                    var edValue = $(_ed1.target).combobox('getValue');
                    var edText = $(_ed1.target).combobox('getText');
                    if (edValue != null && edValue != "") {
                        $dg.datagrid('getRows')[_index].UP_AU_Code = edValue;
                        $dg.datagrid('getRows')[_index].AU_Name = edText;
                    }
                }
                var _ed2 = $dg.datagrid('getEditor', { index: _index, field: 'FS' });
                if (_ed2 != null) {
                    var edValue = $(_ed2.target).combobox('getValue');
                    var edText = $(_ed2.target).combobox('getText');
                    if (edValue != null && edValue != "") {
                        $dg.datagrid('getRows')[_index].UP_SF_Code = edValue;
                        $dg.datagrid('getRows')[_index].SF_Desc = edText;
                    }
                }
                var _ed3 = $dg.datagrid('getEditor', { index: _index, field: 'CT' });
                if (_ed3 != null) {
                    var edValue = $(_ed3.target).combobox('getValue');
                    var edText = $(_ed3.target).combobox('getText');
                    if (edValue != null && edValue != "") {
                        $dg.datagrid('getRows')[_index].UP_RP_MI_Code = edValue;
                        $dg.datagrid('getRows')[_index].CT_RP = edText;
                    }
                }
                $dg.datagrid('endEdit', _index);
            },
            //点击单元格,开启编辑
            onClickCellEvent: function (index, field) {
                Pri.hidfunction.endEditingNoValidCell();
                var _au_code = $(Pri.base.s1).combobox('getValue');
                var $dg = $(Pri.base.id);
                if (field === "AU_Code") {
                    var _edit_data = $dg.datagrid('getRows')[index];
                    $dg.datagrid('editCell', { index: index, field: field });
                    var ed = $dg.datagrid('getEditor', { index: index, field: field });
                    if (ed && ed.type === 'combobox') {
                        $.post(_url_crmhand, { o: JSON.stringify({ action: 'getGroupUser', UG_Code: _au_code }) }, function (data) {
                            data = [{ AU_Code: -1, AU_Name: Pri.base.title.P1 }].concat(data ? data : []);
                            ed.target
                                .combobox({ valueField: "AU_Code", textField: "AU_Name" })
                                .combobox('loadData', data ? data : [])
                                .combobox('showPanel')
                                .combobox('setValue', _edit_data.AU_Code);
                        }, "json");
                    }
                } else if (field === "FS") {
                    var _edit_data = $dg.datagrid('getRows')[index];
                    $dg.datagrid('editCell', { index: index, field: field });
                    var ed = $dg.datagrid('getEditor', { index: index, field: field });
                    if (ed && ed.type === 'combobox') {
                        $.post(_url_crmhand, { o: JSON.stringify({ action: 'getSysFunction', UG_Code: _au_code }) }, function (data) {
                            ed.target
                                .combobox({ valueField: "SF_Code", textField: "SF_Desc" })
                                .combobox('loadData', data ? data : []).combobox('showPanel')
                                .combobox('setValue', _edit_data.UP_SF_Code);
                        }, "json");
                    }
                } else if (field === "CT") {
                    var _edit_data = $dg.datagrid('getRows')[index];
                    $dg.datagrid('editCell', { index: index, field: field });
                    var ed = $dg.datagrid('getEditor', { index: index, field: field });
                    if (ed && ed.type === 'combobox') {
                        $.post(_url_crmhand, { o: JSON.stringify({ action: 'getRpName', UG_Code: _au_code, SF_Code: _edit_data.UP_SF_Code }) }, function (data) {
                            ed.target
                                .combobox('loadData', data ? data : []).combobox('showPanel')
                                .combobox('setValue', _edit_data.UP_RP_MI_Code)
                                .combobox('setText', _edit_data.CT_RP);
                        }, "json");
                    }
                }
                Pri.$editIndex = index;
            }
        }
    };

</script>
