<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealershipManager.aspx.cs" Inherits="templete_usercontrol_DealershipManager" %>

<!DOCTYPE html>
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
    <script src="/scripts/jquery-easyui/datagrid-detailview.js"></script>
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
        }

        .winbody {
            border-width: 0px;
        }

        .red {
            color: red;
        }

        table.service tr td {
            padding: 5px;
            white-space: nowrap;
        }

            table.service tr td.text {
                padding-right: 10px;
            }

        .datagrid-toolbar {
            border-width: 0px;
        }

        table.tbl_service tr td {
            margin: 0;
            padding: 0;
            border: 1px solid red;
        }

        fieldset {
            display: block;
            -webkit-margin-start: 2px;
            -webkit-margin-end: 2px;
            -webkit-padding-before: 0.35em;
            -webkit-padding-start: 0.75em;
            -webkit-padding-end: 0.75em;
            -webkit-padding-after: 0.625em;
            border: 2px groove threedface;
            border-image-source: initial;
            border-image-slice: initial;
            border-image-width: initial;
            border-image-outset: initial;
            border-image-repeat: initial;
            min-width: -webkit-min-content;
        }

        .l-btn-text_search {
            margin: 0 4px 0 5px;
        }

        #Btn_Search {
            margin-left: 15px;
        }
    </style>
</head>
<body>
    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'center',border:false" style="padding: 0px; padding-top: 5px;">
            <div id="tabs" class="easyui-tabs" data-options="fit:true,selected:2,plain:true,border:false">
                <div title="<%=Resources.CRMTREEResource.dm_tabs_Summary%>" data-options="iconCls:'icon-customer-summary'" style="padding: 10px">
                    <div class="easyui-panel" data-options="fit:true,border:false" style="overflow: hidden">
                    </div>
                </div>
                <div title="<%=Resources.CRMTREEResource.dm_tabs_Sales%>" data-options="iconCls:'icon-customer-personal'" style="padding: 10px">
                    <div class="easyui-panel" data-options="fit:true,border:false" style="overflow: hidden">
                    </div>
                </div>
                <div title="<%=Resources.CRMTREEResource.dm_tabs_Service%>" data-options="iconCls:'icon-customer-personal'" style="padding: 20px; overflow: hidden; position: relative;">
                    <div class="easyui-panel" data-options="border:false,height:433" style="overflow: hidden">
                        <div id="tabs_service" class="easyui-tabs" data-options="fit:true,selected:2,plain:true">
                            <div title="<%=Resources.CRMTREEResource.dm_tab_Schedule%>" style="padding: 10px;">
                                <table id="dg_schedule"></table>
                            </div>
                            <div title="<%=Resources.CRMTREEResource.dm_tab_Resources%>" style="padding: 10px;">
                                <table id="dg_resources"></table>
                            </div>
                            <div title="<%=Resources.CRMTREEResource.dm_tab_Departments%>" style="padding: 10px;">
                                <table id="dg_departments"></table>
                            </div>
                            <div title="<%=Resources.CRMTREEResource.dm_tab_Options%>" style="padding: 10px; height: 363px">
                                <table class="form" border="0" cellpadding="3" cellspacing="2">
                                    <tr>
                                        <td id="td_pos" style="width: 130px">
                                            <input type="hidden" id="SD_Code" value="0" /></td>
                                        <td style="width: 40px; text-align: center;"><%=Resources.CRMTREEResource.dm_tab_Options_Yes%></td>
                                        <td style="width: 272px; text-align: left;"><%=Resources.CRMTREEResource.dm_tab_Options_No%></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"><%=Resources.CRMTREEResource.dm_tab_Options_package%></td>
                                        <td style="text-align: center;">
                                            <input type="radio" name="SD_Serv_Package" value="1" checked="checked" /></td>
                                        <td style="text-align: left;">
                                            <input type="radio" name="SD_Serv_Package" value="0" /></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"><%=Resources.CRMTREEResource.dm_tab_Options_selection%></td>
                                        <td style="text-align: center;">
                                            <input type="radio" name="SD_SA_Selection" value="1" checked="checked" /></td>
                                        <td style="text-align: left;">
                                            <input type="radio" name="SD_SA_Selection" value="0" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <table style="margin-left: 50px; height: 255px;">
                                                <tr>
                                                    <td>
                                                        <fieldset style="border: 1px solid green; border-color: green; width: 360px; height: 260px; white-space: nowrap">
                                                            <legend>
                                                                <label><%=Resources.CRMTREEResource.setcalls%></label></legend>
                                                            <div class="btns" style="margin-right: 5px;">
                                                                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add',onClick:_Call_Per.AddHtml"><%=Resources.CRMTREEResource.cm_cars_buttons_add %></a>
                                                            </div>
                                                            <div style="border-bottom: 1px solid #E4E4E4; padding-top: 5px;"></div>
                                                            <div class="e_ui_all">
                                                            </div>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div title="<%=Resources.CRMTREEResource.CampaignApproval%>" style="padding: 10px;">
                                <div class="easyui-panel" data-options="fit:true,border:true">
                                    <table id="dg_auth_process"></table>
                                </div>
                                <div id="tb_auth_process">
                                    <div id="frm_auth_process" style="padding: 10px 5px; border-bottom: 1px solid #ccc;">
                                        <span class="red">*</span>
                                        <span><%=Resources.CRMTREEResource.CampaignCategory%></span>
                                        <input id="EX_Camp_Category" class="easyui-combobox"
                                            data-options="required:true,novalidate:true,onSelect:_camp_approval.selectATIType" style="width: 130px;" />

                                        <span class="red" style="margin-left: 10px;">*</span>
                                        <span><%=Resources.CRMTREEResource.IType%></span>
                                        <input id="AT_IType" class="easyui-combobox"
                                            data-options="required:true,novalidate:true,onSelect:_camp_approval.selectATIType" style="width: 130px;" />

                                        <div style="margin-top: 5px; display: none">
                                            <span class="red">*</span>
                                            <span><%=Resources.CRMTREEResource.CampaignType%></span>
                                            <input id="AT_SType" class="easyui-combobox" data-options="required:true,novalidate:true,multiple:true,multiline:true" style="width: 312px;" />
                                            <a id="Btn_Search" class="easyui-linkbutton l-btn-small l-btn-plain" onclick="btnsearch()" data-options="{iconCls:'icon-search',plain:true}">
                                                <span class="l-btn-text_search"><%=Resources.CRMTREEResource.Search %></span>
                                            </a>
                                        </div>
                                    </div>
                                    <a class="easyui-linkbutton"
                                        data-options="plain:true,iconCls:'icon-add',onClick:_camp_approval.add"><%=Resources.CRMTREEResource.em_contacts_add%></a>
                                </div>
                            </div>
                            <div title="<%=Resources.CRMTREEResource.KPI%>" style="padding: 10px;">
                                <div class="easyui-panel" data-options="fit:true,border:true">
                                    <table id="dg_KPI"></table>
                                </div>

                                <div id="dg_KPI_process">
                                    <a class="easyui-linkbutton"
                                        data-options="plain:true,iconCls:'icon-add',onClick:_kpi.add"><%=Resources.CRMTREEResource.em_contacts_add%></a>
                                </div>
                                <%--  <div id="dg_KPI_process_2">
                                    <a class="easyui-linkbutton"
                                        data-options="plain:true,iconCls:'icon-add',onClick:_kpi.add"><%=Resources.CRMTREEResource.em_contacts_add%></a>
                                </div>--%>
                                <div id="dg_KPI_process_3" style="display: none">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="text-align: center; height: 40px; overflow: hidden; padding-top: 5px;">
                        <a id="btnSave" data-options="iconCls:'icon-save',width:80,disabled:true" onclick="_service.save()" href="javascript:void(0)" class="easyui-linkbutton"><%=Resources.CRMTREEResource.ap_buttons_save%></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="tb_departments" style="padding: 0px;">
        <div class="btns" style="margin-right: 5px;"></div>
    </div>
    <div id="w_values" class="easyui-window" data-options="minimizable:false,closed:true,title:' ',modal:true"
        style="width: 500px; height: 250px;">
        <div class="easyui-layout" data-options="fit:true">
            <div data-options="region:'center',border:false">
                <table id="dg_values"></table>
            </div>
            <div data-options="region:'south',border:false" style="height: 38px; text-align: right; overflow: hidden; border-top: 1px solid #B1C242; padding-top: 5px; padding-right: 10px;">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',onClick:_values.accept" style="width: 80px;"><%=Resources.CRMTREEResource.cm_cars_buttonos_accept%></a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_values.close" style="width: 80px; margin-left: 10px;"><%=Resources.CRMTREEResource.cm_cars_buttonos_ignore%></a>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Reports/DealershipManager.aspx';
    var _s_url_AddCall = '/handler/Campaign/CampaignManager.aspx';
    var _url_crmhand = '/handler/CRMhandle.aspx';
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
    function SetCheckBox(a_o, index) {
        var _word = _camp_approval.$_Words, _o = [], a_btns = [];
        if (!_word) {
            return '';
        }
        if (a_o) {
            _o = a_o.split(",");
        }
        $.each(_word, function (i, w) {
            var _tr = '';
            if (_o) {
                for (var j = 0; j < _o.length; j++) {
                    if (w.value == _o[j]) {
                        _tr = 'checked="checked"';
                        break;
                    }
                }
            }
            var _input = '<input name="app_app_' + index + '" type="checkbox" ' + _tr + ' title="' + w.text + '" value="' + w.value + '" />';
            a_btns.push(_input);
        });
        return a_btns;
    }
    /*
    * KPI
    */
    var _kpi = {
        id: '#dg_KPI',
        tool: '#dg_KPI_process',
        _field_lung: {
            _pdn_name: '<%=Resources.CRMTREEResource.KPI_pdn_name%>',
            _pdn_kpi: '<%=Resources.CRMTREEResource.KPI_pdn_kpi%>',
            _pdn_Annual: '<%=Resources.CRMTREEResource.KPI_pdn_Annual%>',
            _pdn_Quarterly: '<%=Resources.CRMTREEResource.KPI_pdn_Quarterly%>',
            _pdn_Monthly: '<%=Resources.CRMTREEResource.KPI_pdn_Monthly%>',
            _pdn_Weekly: '<%=Resources.CRMTREEResource.KPI_pdn_Weekly%>',
            _pdn_Daily: '<%=Resources.CRMTREEResource.KPI_pdn_Daily%>',
        },
        $tableData: null,
        $dataSource: null,
        getChildData_2: function () {
            if (_kpi.$c_index != null) {
                var _p_data = $(_kpi.id);

                _p_data.datagrid('collapseRow', _kpi.$c_index);
            }
        },
        getChildData: function () {    // b 代表是否同一行
            if (_kpi.$c_index != null) {
                var _p_data = $(_kpi.id);

                var ddv = _p_data.datagrid('getRowDetail', _kpi.$c_index).find('table.ddv');;
                _p_data.datagrid('getRows')[_kpi.$c_index]._d = ddv.datagrid('getRows');
                _p_data.datagrid('getRowDetail', _kpi.$c_index).children().remove();
                _p_data.datagrid('getRowDetail', _kpi.$c_index).append('<table class="ddv"></table>');
                _p_data.datagrid('collapseRow', _kpi.$c_index);
                _kpi.$c_index = null;
            }
        },
        add: function () {
            _kpi.endEditingNoValidCell();
            _kpi.getChildData_2();
            _kpi.$editIndex_2 = undefined;
            var $dg = $(_kpi.id);
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
        add_2: function () {
            _kpi.endEditingNoValidCell_2();
            var $dg = $(_kpi.id).datagrid('getRowDetail', _kpi.$c_index).find('table.ddv');
            //var $dg = $(this);

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
            $(_kpi.id).datagrid('fixDetailRowHeight', _kpi.$c_index);
        },
        //删除
        remove: function (e, target) {
            var $dg = $(_kpi.id);
            var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
            var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
            if (rowData.id > 0) {
                $.confirmWindow.tempRemove(function () {
                    $dg.datagrid('deleteRow', rowIndex);
                });
            } else {
                if (rowIndex >= 0) {
                    $dg.datagrid('deleteRow', rowIndex);
                    $.post(_url_crmhand, {
                        o: JSON.stringify({
                            action: 'deleteKPI',
                            UType: 2, DE_Code: -1, PN_Code: rowData.PDN_Code, PT_Code: rowData.KPT_Code
                        })
                    }, function (data) {

                    }, "json");
                }
            }
            stopPropagation(e);
        },
        //删除
        remove_2: function (e, target) {
            var $dg = $(_kpi.id).datagrid('getRowDetail', _kpi.$c_index).find('table.ddv');
            var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
            var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
            if (rowData.id > 0) {
                $.confirmWindow.tempRemove(function () {
                    $dg.datagrid('deleteRow', rowIndex);
                });
            } else {
                if (rowIndex >= 0) {
                    $dg.datagrid('deleteRow', rowIndex);
                    var _p_data = $(_kpi.id).datagrid('getRows')[_kpi.$c_index];
                    $.post(_url_crmhand, {
                        o: JSON.stringify({
                            action: 'deleteKPI',
                            UType: 1, DE_Code: rowData.DE_Code, PN_Code: _p_data.PDN_Code, PT_Code: _p_data.KPT_Code
                        })
                    }, function (data) {
                       
                    }, "json");
                }
            }
            $(_kpi.id).datagrid('fixDetailRowHeight', _kpi.$c_index);
            stopPropagation(e);
        },
        //结构化数据库获取的数据，用于view数据显示
        getOkdata: function () {
            if (this.$dataSource == null) {
                return null;
            }
            var _d_g = this.$dataSource._d_group;
            var _d_in = this.$dataSource._d_info;
            var _new_d = [];
            $.each(_d_g, function (i, o) {
                var _cats = {};
                $.each(_d_in, function (n, info) {
                    if (o.PDN_Code == info.PDN_Code && o.KPT_Code == info.KPT_Code) {
                        if (info.KPV_Cat == 1) {
                            _cats.pdn_Annual = info.KPV_Value;
                        } else if (info.KPV_Cat == 2) {
                            _cats.pdn_Quarterly = info.KPV_Value;
                        } else if (info.KPV_Cat == 3) {
                            _cats.pdn_Monthly = info.KPV_Value;
                        } else if (info.KPV_Cat == 4) {
                            _cats.pdn_Weekly = info.KPV_Value;
                        } else if (info.KPV_Cat == 5) {
                            _cats.pdn_Daily = info.KPV_Value;
                        }
                        _cats.PDN_Code = info.PDN_Code;
                        _cats.PDN_Name = info.PDN_Name;
                        _cats.KPT_Code = info.KPT_Code;
                        _cats.KPT_Desc = info.KPT_Desc;
                    }
                });
                _cats.isOld = 1;
                _new_d.push(_cats);
            });
            return _new_d;
        },
        //过滤被选择的KPI
        filterSelectKPI: function (obj) {
            if (obj == null) {
                return [];
            }
            var _d_obj = $(_kpi.id).datagrid('getRows');
            if (_d_obj == null || _d_obj.length <= 0) {
                return [];
            }
            var _s_obj = $(_kpi.id).datagrid('getSelected');
            if (_s_obj == null) {
                return [];
            }
            var select_PDN_Code = _s_obj.PDN_Code;
            var seelct_KPT_Code = [];
            $.each(_d_obj, function (i, o) {
                if (o.PDN_Code == select_PDN_Code) {
                    seelct_KPT_Code.push(o.KPT_Code);
                }
            });
            $.each(seelct_KPT_Code, function (index, o) {
                for (var i = 0; i < obj.length; i++) {
                    if (o == obj[i].KPT_Code) {
                        obj.splice(i, 1);
                        break;
                    }
                }
            });
            return obj;
        },
        //结构化数据库获取的数据，用于view数据显示
        getUserOkdata: function (o) {
            if (o == undefined || o == null) {
                return null;
            }
            var _u_g = o._d_group;
            var _u = o._d_info
            var _new_d = [];
            $.each(_u_g, function (i, o) {
                var _cats = {};
                $.each(_u, function (n, info) {
                    if (o.DE_Code == info.KPV_DE_AD_Code) {
                        if (info.KPV_Cat == 1) {
                            _cats.pdn_Annual_user = info.KPV_Value;
                        } else if (info.KPV_Cat == 2) {
                            _cats.pdn_Quarterly_user = info.KPV_Value;
                        } else if (info.KPV_Cat == 3) {
                            _cats.pdn_Monthly_user = info.KPV_Value;
                        } else if (info.KPV_Cat == 4) {
                            _cats.pdn_Weekly_user = info.KPV_Value;
                        } else if (info.KPV_Cat == 5) {
                            _cats.pdn_Daily_user = info.KPV_Value;
                        }
                        _cats.DE_Code = info.KPV_DE_AD_Code;
                        _cats.AU_Name = info.AU_Name;
                        
                    }
                });
                _new_d.push(_cats);
            });
            return _new_d;
        },
        //结束编辑不验证
        endEditingNoValidCell: function () {
            if (_kpi.$editIndex == undefined) {
                return;
            }
            var $dg = $(_kpi.id);
            var _index = _kpi.$editIndex;

            var _ed1 = $dg.datagrid('getEditor', { index: _index, field: 'pdn_name' });
            if (_ed1 != null) {
                var edValue = $(_ed1.target).combobox('getValue');
                var edText = $(_ed1.target).combobox('getText');
                if (edValue != null && edValue != "") {
                    $dg.datagrid('getRows')[_index].PDN_Code = edValue;
                    $dg.datagrid('getRows')[_index].PDN_Name = edText;
                }
            }
            var _ed2 = $dg.datagrid('getEditor', { index: _index, field: 'pdn_kpi' });
            if (_ed2 != null) {
                var edValue = $(_ed2.target).combobox('getValue');
                var edText = $(_ed2.target).combobox('getText');
                if (edValue != null && edValue != "") {
                    $dg.datagrid('getRows')[_index].KPT_Code = edValue;
                    $dg.datagrid('getRows')[_index].KPT_Desc = edText;
                }
            }
            $dg.datagrid('endEdit', _index);
        },
        //结束编辑不验证
        endEditingNoValidCell_2: function (o) {
            if (_kpi.$editIndex_2 == undefined || _kpi.$editIndex_2 == null) {
                return;
            }
            var _index = _kpi.$editIndex_2;
            var ddv = $(_kpi.id).datagrid('getRowDetail', _kpi.$c_index).find('table.ddv');

            var _ed1 = ddv.datagrid('getEditor', { index: _index, field: 'pdn_name_user' });
            if (_ed1 != null) {
                var edValue = $(_ed1.target).combobox('getValue');
                var edText = $(_ed1.target).combobox('getText');
                if (edValue != null && edValue != "") {
                    ddv.datagrid('getRows')[_index].DE_Code = edValue;
                    ddv.datagrid('getRows')[_index].AU_Name = edText;
                }
            }
            ddv.datagrid('endEdit', _index);
        },
        checkSelectPData:function(index){
            var _o = $(_kpi.id).datagrid('getRows')[index];
            if (_o.PDN_Code != null && _o.KPT_Code != null) {
                return true;
            } else {
                var _p_data = $(_kpi.id);
                _p_data.datagrid('collapseRow', index);
                return false;
            }
        },
        //点击单元格,开启编辑
        onClickCellEvent: function (index, field) {
            _kpi.getChildData_2();
            //结束编辑，不验证
            _kpi.endEditingNoValidCell();
            var $dg = $(_kpi.id);
            if (field === "pdn_name") {
                var _edit_data = $dg.datagrid('getRows')[index];
                if (_edit_data.isOld != 1) {
                    $dg.datagrid('editCell', { index: index, field: field });
                    var ed = $dg.datagrid('getEditor', { index: index, field: field });
                    if (ed && ed.type === 'combobox') {
                        if (_kpi.$getDepartment != null) {

                            ed.target.combobox('loadData', _kpi.$getDepartment).combobox('showPanel')
                                .combobox('setValue', _edit_data.PDN_Code)
                                .combobox('setText', _edit_data.PDN_Name);
                        } else {
                            $.post(_url_crmhand, { o: JSON.stringify({ action: 'getDepartment' }) }, function (data) {
                                _kpi.$getDepartment = data ? data : [];

                                ed.target.combobox('loadData', _kpi.$getDepartment).combobox('showPanel')
                                    .combobox('setValue', _edit_data.PDN_Code)
                                    .combobox('setText', _edit_data.PDN_Name);
                            }, "json");
                        }
                    }
                }
            } else if (field === "pdn_kpi") {
                var _edit_data = $dg.datagrid('getRows')[index];
                if (_edit_data.isOld != 1) {
                    $dg.datagrid('editCell', { index: index, field: field });
                    var ed = $dg.datagrid('getEditor', { index: index, field: field });
                    if (ed && ed.type === 'combobox') {
                        $.post(_url_crmhand, { o: JSON.stringify({ action: 'getKPI' }) }, function (data) {
                            _kpi.$getKPI = data ? data : [];
                            _kpi.$getKPI = _kpi.filterSelectKPI(_kpi.$getKPI);

                            ed.target.combobox('loadData', _kpi.$getKPI).combobox('showPanel')
                                .combobox('setValue', _edit_data.KPT_Code)
                                .combobox('setText', _edit_data.KPT_Desc);
                        }, "json");
                    }
                }
            } else if (field === "pdn_Annual") {
                $dg.datagrid('editCell', { index: index, field: field });
                //$($dg.datagrid('getEditor', { index: index, field: field }).target).focus();
                //var ed = $dg.datagrid('getEditor', { index: index, field: field });
                //$(ed.target.numberbox()).focus();
            } else if (field === "pdn_Quarterly") {
                $dg.datagrid('editCell', { index: index, field: field });
            } else if (field === "pdn_Monthly") {
                $dg.datagrid('editCell', { index: index, field: field });
            } else if (field === "pdn_Weekly") {
                $dg.datagrid('editCell', { index: index, field: field });
            } else if (field === "pdn_Daily") {
                $dg.datagrid('editCell', { index: index, field: field });
            }
            _kpi.$editIndex = index;  //记录当前编辑的单元格
        },
        //点击单元格,开启编辑
        onClickCellEvent_2: function (index, field) {
            _kpi.endEditingNoValidCell();
            _kpi.endEditingNoValidCell_2();

            var $dg = $(_kpi.id).datagrid('getRowDetail', _kpi.$c_index).find('table.ddv');
            if (field === "pdn_name_user") {
                $dg.datagrid('editCell', { index: index, field: field });
                var ed = $dg.datagrid('getEditor', { index: index, field: field });
                if (ed && ed.type === 'combobox') {
                    var _P_data = $(_kpi.id).datagrid('getRows')[_kpi.$c_index];
                    var _edit_data = $dg.datagrid('getRows')[index];
                    $.post(_url_crmhand, { o: JSON.stringify({ action: 'getDepartmentUser', PN_Code: _P_data.PDN_Code }) }, function (data) {
                        data = data ? data : [];
                       
                        ed.target.combobox('loadData', data).combobox('showPanel')
                            .combobox('setValue', _edit_data.DE_Code)
                            .combobox('setText', _edit_data.AU_Name);
                    }, "json");
                }
            } else if (field === "pdn_Annual_user") {
                $dg.datagrid('editCell', { index: index, field: field });
            } else if (field === "pdn_Quarterly_user") {
                $dg.datagrid('editCell', { index: index, field: field });
            } else if (field === "pdn_Monthly_user") {
                $dg.datagrid('editCell', { index: index, field: field });
            } else if (field === "pdn_Weekly_user") {
                $dg.datagrid('editCell', { index: index, field: field });
            } else if (field === "pdn_Daily_user") {
                $dg.datagrid('editCell', { index: index, field: field });
            }
            _kpi.$editIndex_2 = index;   //记录当前编辑的单元格
        },
        create: function () {
            var $dg = $(this.id);
            $dg.datagrid({
                url: null, fit: true, rownumbers: true,
                singleSelect: true, showHeader: true,
                border: false, toolbar: '#dg_KPI_process',
                loadMsg: 'Loading....',
                remoteSort: false,
                nowrap: false,
                fitColumns: true,
                data: [],
                columns: [
                    [{
                        field: 'pdn_name', align: 'left', title: this._field_lung._pdn_name, width: 160,
                        formatter: function (value, row, index) {
                            return row.PDN_Name;
                        },
                        editor: {
                            type: 'combobox', options: {
                                novalidate: true,
                                required: true,
                                valueField: "PDN_Code",
                                textField: "PDN_Name"
                            }
                        }
                    }, {
                        field: 'pdn_kpi', align: 'left', title: this._field_lung._pdn_kpi, width: 160,
                        formatter: function (value, row) {
                            return row.KPT_Desc;
                        },
                        editor: {
                            type: 'combobox', options: {
                                novalidate: true,
                                required: true,
                                valueField: "KPT_Code",
                                textField: "KPT_Desc"
                            }
                        }
                    }, {
                        field: 'pdn_Annual', align: 'right', title: this._field_lung._pdn_Annual, width: 80,
                        formatter: function (value, row) {
                            return row.pdn_Annual;
                        },
                        editor: {
                            type: 'numberbox',
                            options: {
                                novalidate: true,
                                //required: true,
                                precision: '2'
                            }
                        }
                    }, {
                        field: 'pdn_Quarterly', align: 'right', title: this._field_lung._pdn_Quarterly, width: 80,
                        formatter: function (value, row) {
                            return row.pdn_Quarterly;
                        },
                        editor: {
                            type: 'numberbox',
                            options: {
                                novalidate: true,
                                required: true,
                                precision: '2'
                            }
                        }
                    }, {
                        field: 'pdn_Monthly', align: 'right', title: this._field_lung._pdn_Monthly, width: 80,
                        formatter: function (value, row) {
                            return row.pdn_Monthly;
                        },
                        editor: {
                            type: 'numberbox',
                            options: {
                                novalidate: true,
                                required: true,
                                precision: '2'
                            }
                        }
                    }, {
                        field: 'pdn_Weekly', align: 'right', title: this._field_lung._pdn_Weekly, width: 80,
                        formatter: function (value, row) {
                            return row.pdn_Weekly;
                        },
                        editor: {
                            type: 'numberbox',
                            options: {
                                novalidate: true,
                                required: true,
                                precision: '2'
                            }
                        }
                    }, {
                        field: 'pdn_Daily', align: 'right', title: this._field_lung._pdn_Daily, width: 80,
                        formatter: function (value, row) {
                            return row.pdn_Daily;
                        },
                        editor: {
                            type: 'numberbox',
                            options: {
                                novalidate: true,
                                required: true,
                                precision: '2'
                            }
                        }
                    }, {
                        field: 'btnLast', align: 'center', width: 60,
                        formatter: function (value, row, index) {
                            var a_o = [
                            { icon: 'icon-remove', title: '<%=Resources.CRMTREEResource.btnRemove%>', clickFun: '_kpi.remove(event,this);' }
                            ];
                            return setBtns(a_o, true).join('');
                        }
                    }]
                ],
                view: detailview,
                detailFormatter: function (rowIndex, rowData) {
                    return '<table class="ddv"></table>';
                },
                onExpandRow: function (index, row) {
                    if (!_kpi.checkSelectPData(index)) {
                        return;
                    }
                    _kpi.endEditingNoValidCell();    //结束编辑父单元格
                    _kpi.endEditingNoValidCell_2();  //结束编辑子单元格
                    _kpi.getChildData_2();
                    _kpi.$c_index = index;           //记录展开子集的父级索引
                    _kpi.$c_index_1 = index;           //记录展开子集的父级索引
                    $("#dg_KPI_process_3").append($('<div><div id="dg_KPI_process_2"><a class="easyui-linkbutton" data-options="plain:true,iconCls:\'icon-add\',onClick:_kpi.add_2"><%=Resources.CRMTREEResource.em_contacts_add%></a></div></div>').html());
                    $.parser.parse('#dg_KPI_process_3');
                    var ddv = $(_kpi.id).datagrid('getRowDetail', index).find('table.ddv');
                    ddv.datagrid({
                        fitColumns: true, toolbar: '#dg_KPI_process_2',
                        singleSelect: true, rownumbers: true,
                        loadMsg: 'loading...', height: 'auto',
                        columns: [[
                            {
                                field: 'pdn_name_user', align: 'left', title: '', width: 160,
                                formatter: function (value, row, index) {
                                    return row.AU_Name;
                                },
                                editor: {
                                    type: 'combobox', options: {
                                        novalidate: true,
                                        required: true,
                                        valueField: "DE_Code",
                                        textField: "AU_Name"
                                    }
                                }
                            }, {
                                field: 'pdn_Annual_user', align: 'right', title: _kpi._field_lung._pdn_Annual, width: 80,
                                formatter: function (value, row) {
                                    return row.pdn_Annual_user;
                                },
                                editor: {
                                    type: 'numberbox',
                                    options: {
                                        novalidate: true,
                                        required: true,
                                        precision: '2'
                                    }
                                }
                            }, {
                                field: 'pdn_Quarterly_user', align: 'right', title: _kpi._field_lung._pdn_Quarterly, width: 80,
                                formatter: function (value, row) {
                                    return row.pdn_Quarterly_user;
                                },
                                editor: {
                                    type: 'numberbox',
                                    options: {
                                        novalidate: true,
                                        required: true,
                                        precision: '2'
                                    }
                                }
                            }, {
                                field: 'pdn_Monthly_user', align: 'right', title: _kpi._field_lung._pdn_Monthly, width: 80,
                                formatter: function (value, row) {
                                    return row.pdn_Monthly_user;
                                },
                                editor: {
                                    type: 'numberbox',
                                    options: {
                                        novalidate: true,
                                        required: true,
                                        precision: '2'
                                    }
                                }
                            }, {
                                field: 'pdn_Weekly_user', align: 'right', title: _kpi._field_lung._pdn_Weekly, width: 80,
                                formatter: function (value, row) {
                                    return row.pdn_Weekly_user;
                                },
                                editor: {
                                    type: 'numberbox',
                                    options: {
                                        novalidate: true,
                                        required: true,
                                        precision: '2'
                                    }
                                }
                            }, {
                                field: 'pdn_Daily_user', align: 'right', title: _kpi._field_lung._pdn_Daily, width: 80,
                                formatter: function (value, row) {
                                    return row.pdn_Daily_user;
                                },
                                editor: {
                                    type: 'numberbox',
                                    options: {
                                        novalidate: true,
                                        required: true,
                                        precision: '2'
                                    }
                                }
                            }, {
                                field: 'btnLast', align: 'center', width: 60,
                                formatter: function (value, row, index) {
                                    var a_o = [
                                    { icon: 'icon-remove', title: '<%=Resources.CRMTREEResource.btnRemove%>', clickFun: '_kpi.remove_2(event,this);' }
                                    ];
                                    return setBtns(a_o, true).join('');
                                }
                            }
                        ]],
                        onResize: function () {
                            $(_kpi.id).datagrid('fixDetailRowHeight', index);
                        },
                        onLoadSuccess: function () {
                            setTimeout(function () {
                                $(_kpi.id).datagrid('fixDetailRowHeight', index);
                            }, 0);
                        }, onClickCell: _kpi.onClickCellEvent_2
                    });
                    if (row._d != null) {
                        $(ddv).datagrid('loadData', row._d ? row._d : []);
                        $(_kpi.id).datagrid('fixDetailRowHeight', index);
                    } else {
                        var params = { action: 'getDepartmentKPIUser', PN_Code: row.PDN_Code, PT_Code: row.KPT_Code };
                        $.post(_url_crmhand, { o: JSON.stringify(params) }, function (res) {
                            if (res) {
                                var _data = _kpi.getUserOkdata(res);
                                $(ddv).datagrid('loadData', _data ? _data : []);
                                $(_kpi.id).datagrid('fixDetailRowHeight', index);
                            }
                        }, "json");
                    }
                },
                onCollapseRow: function (index, row) {
                    _kpi.endEditingNoValidCell_2();
                    _kpi.$editIndex_2 = null;
                    _kpi.getChildData(1);
                },
                onClickCell: _kpi.onClickCellEvent
            });
        }
    };
        //--------------------------------------------------------------------------------------
        //Campaign Approval（活动审批）
        //--------------------------------------------------------------------------------------
        var _camp_approval = {
            id: '#dg_auth_process',
            data_user_groups: [],
            ctrols: {
                combobox: ['EX_Camp_Category', 'AT_SType', 'AT_IType']
            },
            LastClickCell: {},
            onClickCell: {},
            //验证
            validate: function (o) {
                var bValid = $.form.validate(_camp_approval.ctrols);
                return bValid;
            },
            //取消验证
            disableValidation: function () {
                $.form.disableValidation(_camp_approval.ctrols);
            }
        };
        /*
        *** 在活动类别或者类型选择后，触发事件加载表格；
        */
        _camp_approval.selectATIType = function () {
            var _Camp_data = $('#EX_Camp_Category').combobox('getData');
            var _Camp_value = $('#EX_Camp_Category').combobox('getValue');
            if (!_Camp_value) {
                return false;
            }
            var _AT_IType = $('#AT_IType').combobox('getValue');
            if (!_AT_IType) {
                return false;
            }
            var _w_id = 0;
            for (var i = 0; i < _Camp_data.length; i++) {
                if (_Camp_data[i].value == _Camp_value) {
                    _w_id = _Camp_data[i].id;
                    break;
                }
            }
            var params = {
                action: 'Get_approval', AT_Cat: _Camp_value, AT_IType: _AT_IType, p_id: _w_id
            };
            $("#dg_auth_process").datagrid('loadData', []);
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                //if (!$.checkResponse(res)) {
                //    return;
                //}
                _camp_approval.$_Words = res._Words;
                _camp_approval.$Width = res._Words.length * 15 + 6;
                _camp_approval.bindData(res.approval);
            }, "json");
        }
        /*
        *** 加载设置表格内的活动类别；
        */
        _camp_approval.SelectSP = function () {
            var _Camp_data = $('#EX_Camp_Category').combobox('getData');
            var _Camp_value = $('#EX_Camp_Category').combobox('getValue');
            var _w_id = 0;
            for (var i = 0; i < _Camp_data.length; i++) {
                if (_Camp_data[i].value == _Camp_value) {
                    _w_id = _Camp_data[i].id;
                    break;
                }
            }
            var _word = _camp_approval.$_Words;
            var a_o = [];
            for (var i = 0; i < _word.length; i++) {
                if (_word[i].Select) {
                    a_o.push({ icon: 'icon-selectSP', title: _word[i].text })
                } else {
                    a_o.push({ icon: 'icon-selectSPNo', title: _word[i].text })
                }
            }
            return a_o;
        };
        //取消编辑
        _camp_approval.cancelEdit = function () {
            var $dg = $(_camp_approval.id);
            var rows = $dg.datagrid('getRows');
            for (var i = 0, len = rows.length; i < len; i++) {
                $dg.datagrid('cancelEdit', i);
            }
        };
        //验证
        _camp_approval.check = function () {
            _contacts.endEditingNoValid();
            var $dg = $(_camp_approval.id);
            var rows = $dg.datagrid('getRows');
            var bValid = true;
            for (var i = 0, len = rows.length; i < len; i++) {
                var row = rows[i];
                //if ($.trim(row.type) === '') {
                //    bValid = false;
                //    $("#tabs").tabs('select', 2);
                //    _contacts.onClickCell.call($dg, i, 'type');
                //    break;
                //}
                //if (!row.o) {
                //    bValid = false;
                //    $("#tabs").tabs('select', 2);
                //    _contacts.onClickCell.call($dg, i, 'info');
                //    break;
                //}
            }
            return bValid;
        };
        //结束编辑不验证
        _camp_approval.endEditingNoValid = function () {
            var $dg = $(_camp_approval.id);
            var rows = $dg.datagrid('getRows');
            for (var i = 0, len = rows.length; i < len; i++) {
                var ed_UG_Code = $dg.datagrid('getEditor', { index: i, field: 'UG_Code' });
                if (ed_UG_Code != null) {
                    var edValue = $(ed_UG_Code.target).combobox('getValue');
                    var edText = $(ed_UG_Code.target).combobox('getText');
                    if (edValue != null && edValue != "") {
                        $dg.datagrid('getRows')[i].UG_Code = edValue;
                        $dg.datagrid('getRows')[i].UG_Name = edText;
                    }
                }
                var ed_AU_UG_Code = $dg.datagrid('getEditor', { index: i, field: 'AU_UG_Code' });
                if (ed_AU_UG_Code != null) {
                    var edValue = $(ed_AU_UG_Code.target).combobox('getValue');
                    var edText = $(ed_AU_UG_Code.target).combobox('getText');
                    if (edValue != null && edValue != "") {
                        $dg.datagrid('getRows')[i].AU_Code = edValue;
                        $dg.datagrid('getRows')[i].AU_Name = edText;
                    }
                }
                $dg.datagrid('endEdit', i);
            }
        };
        //结束编辑需要验证
        _camp_approval.endEditing = function () {
            var $dg = $(_camp_approval.id);
            var rows = $dg.datagrid('getRows');
            var bValid = true;
            for (var i = 0, len = rows.length; i < len; i++) {
                var eds = $dg.datagrid('getEditors', i);
                if (eds.length === 0) { continue; }
                for (var j = 0; j < eds.length; j++) {
                    var ed = eds[j];
                    if (!ed) {
                        continue;
                    }
                    var type = ed.type;
                    var $target = $(ed.target);
                    var isValid = false;
                    if (type === 'combobox') {
                        isValid = $target.combobox('enableValidation').combobox('isValid');
                    }
                    if (!isValid) {
                        bValid = false;
                        continue;
                    }
                    if (type === 'combobox') {
                        var typeText = $target.combobox('getText');
                        $dg.datagrid('getRows')[i][ed.field + '_Text'] = typeText;
                    }
                }
                if (bValid) {
                    $dg.datagrid('endEdit', i);
                } else {
                    $dg.datagrid('selectRow', i);
                }
            }
            return bValid;
        };
        //点击单元格,开启编辑
        _camp_approval.onClickCell = function (index, field) {
            if (field !== 'btnLast') {
                _camp_approval.endEditingNoValid();
                var $dg = $(this);
                var rowData = $dg.datagrid('selectRow', index).datagrid('getSelected');
                if (field === "app_select") {
                    var spCodesTemp = "";
                    $('input:checkbox[name=app_app_' + index + ']:checked').each(function (i) {
                        if (0 == i) {
                            spCodesTemp = $(this).val();
                        } else {
                            spCodesTemp += ("," + $(this).val());
                        }
                    });
                    rowData.AT_SType = spCodesTemp
                } else if (field === "UG_Code") {
                    $dg.datagrid('editCell', { index: index, field: field });
                    var ed = $dg.datagrid('getEditor', { index: index, field: field });
                    if (ed && ed.type === 'combobox') {
                        ed.target.combobox('showPanel')
                            .combobox('setValue', $dg.datagrid('getRows')[index].UG_Code)
                            .combobox('setText', $dg.datagrid('getRows')[index].UG_Name)
                            .combobox('setGroup', $dg.datagrid('getRows')[index].UG_Group);
                    }
                } else if (field === "AU_UG_Code") {
                    $dg.datagrid('editCell', { index: index, field: field });
                    var ed = $dg.datagrid('getEditor', { index: index, field: field });
                    if (ed && ed.type === 'combobox') {
                        $.post(_s_url, { o: JSON.stringify({ action: 'Get_All_Users', UG_Code: rowData.UG_Code }) }, function (data) {
                            data = data ? data : [];
                            ed.target.combobox('loadData', data).combobox('showPanel')
                                .combobox('setValue', $dg.datagrid('getRows')[index].AU_Code)
                                .combobox('setText', $dg.datagrid('getRows')[index].AU_Name);
                        }, "json");
                    }
                }
            }
        };
        //创建
        _camp_approval.create = function () {
            var $dg = $(_camp_approval.id);
            $dg.datagrid({
                url: null,
                fit: true,
                toolbar: '#tb_auth_process',
                rownumbers: true,
                singleSelect: true,
                showHeader: true,
                border: false,
                loadMsg: 'Loading....',
                columns: [[
                    {
                        field: 'app_select', align: 'center', title: '活动类型', width: _camp_approval.$Width,
                        formatter: function (value, row, index) {
                            var a_o = SetCheckBox(row.AT_SType, index);
                            return a_o.join('');
                        }
                    },
                    {
                        field: 'UG_Code', align: 'center', title: '职位', width: 190,
                        formatter: function (value, row) {
                            return row.UG_Name;
                        },
                        editor: {
                            type: 'combobox', options: {
                                novalidate: true,
                                required: true,
                                valueField: 'value',
                                textField: 'text',
                                groupField: 'group',
                                data: _camp_approval.data_user_groups,
                                onSelect: function (record) {
                                    var row = $dg.datagrid('getSelected');
                                    window.setTimeout(function () {
                                        _camp_approval.endEditingNoValid();
                                        var rowIndex = $dg.datagrid('getRowIndex', row);
                                        _camp_approval.onClickCell.call($dg, rowIndex, 'AU_UG_Code');
                                    }, 0);
                                }
                            }
                        }
                    },
                    {
                        field: 'AU_UG_Code', align: 'center', title: '用户', width: 190,
                        formatter: function (value, row) {
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
                        field: 'AT_Code', title: 'AT_Code', width: 0, hidden: true,
                        formatter: function (value, row) {
                            return row.AT_Code;
                        },
                        editor: {
                            type: 'textbox',
                            options: {
                                novalidate: true,
                                required: true
                            }
                        }
                    },
                    {
                        field: 'btnLast', align: 'center', width: 60,
                        formatter: function (value, row, index) {
                            var a_o = [
                            { icon: 'icon-remove', title: '<%=Resources.CRMTREEResource.btnRemove%>', clickFun: '_camp_approval.remove(event,this);' }
                            ];
                            return setBtns(a_o, true).join('');
                        }
                    }
                ]],
                onClickCell: _camp_approval.onClickCell
            });
        };
            //添加
            _camp_approval.add = function () {
                var _Camp_data = $('#EX_Camp_Category').combobox('getData');
                var _Camp_value = $('#EX_Camp_Category').combobox('getValue');
                if (!_Camp_value) {
                    return false;
                }
                var _AT_IType = $('#AT_IType').combobox('getValue');
                if (!_AT_IType) {
                    return false;
                }
                var _w_id = 0;
                for (var i = 0; i < _Camp_data.length; i++) {
                    if (_Camp_data[i].value == _Camp_value) {
                        _w_id = _Camp_data[i].id;
                        break;
                    }
                }
                var _params = { action: 'GetWords', p_id: _w_id }
                $.post(_s_url, { o: JSON.stringify(_params) }, function (data) {
                    if (data) {
                        _camp_approval.$_Words = data._Words;
                        _camp_approval.$Width = data._Words.length * 15 + 6;
                        var $dg = $(_camp_approval.id);
                        _camp_approval.endEditingNoValid()
                        var d = {};
                        var row = $dg.datagrid('getSelected');
                        var rowIndex = undefined;
                        if (row) {
                            rowIndex = $dg.datagrid('getRowIndex', row) + 1;
                        }
                        $dg.datagrid('insertRow', {
                            index: rowIndex,
                            row: d
                        });
                        rowIndex = row ? rowIndex : $dg.datagrid('getRows').length - 1;
                        $dg.datagrid('selectRow', rowIndex);//.datagrid('beginEdit', rowIndex);
                    }
                }, "json");
            };
            //删除
            _camp_approval.remove = function (e, target) {
                var $dg = $(_camp_approval.id);
                var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
                var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
                if (rowData.id > 0) {
                    $.confirmWindow.tempRemove(function () {
                        $dg.datagrid('deleteRow', rowIndex);
                    });
                } else {
                    if (rowIndex >= 0) {
                        $dg.datagrid('deleteRow', rowIndex);
                    }
                }
                stopPropagation(e);
            };
            //移动
            _camp_approval.move = function (e, target, isUp) {
                var $dg = $(_camp_approval.id);
                var index = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
                _camp_approval.endEditingNoValid();
                var rows = $dg.datagrid('getRows');
                if (index >= 0) {
                    if (isUp && index !== 0) {
                        var row = rows.splice(index, 1);
                        var upIndex = index - 1
                        var a = rows.slice(0, upIndex);
                        var b = rows.slice(upIndex);
                        var data = a.concat(row, b);
                        $dg.datagrid('loadData', data);
                        $dg.datagrid('selectRow', upIndex);
                    }
                    if (!isUp && index !== rows.length - 1) {
                        var row = rows.splice(index, 1);
                        var downIndex = index + 1;
                        var a = rows.slice(0, downIndex);
                        var b = rows.slice(downIndex);
                        var data = a.concat(row, b);
                        $dg.datagrid('loadData', data);
                        $dg.datagrid('selectRow', downIndex);
                    }
                }
                stopPropagation(e);
            };
            //获得
            _camp_approval.get = function () {
                _camp_approval.disableValidation();
                var _Camp_value = $('#EX_Camp_Category').combobox('getValue');
                var _AT_IType = $('#AT_IType').combobox('getValue');
                var $dg = $(_camp_approval.id);
                var rows = $dg.datagrid('getRows');
                return { Cam_Cat: _Camp_value, IType: _AT_IType, _data: rows };
            };
            //绑定数据
            _camp_approval.bindData = function (data) {
                $('#dg_auth_process').datagrid('loadData', data ? data : []);
            };

            //--------------------------------------------------------------------------------------
            //schedule（日程安排）
            //--------------------------------------------------------------------------------------
            var _schedule = { _id: '#dg_schedule' };
            //最后编辑项
            _schedule.lastEdit = {};
            _schedule.language = {
                am: '<%=Resources.CRMTREEResource.ed_schedule_am%>',
                pm: '<%=Resources.CRMTREEResource.ed_schedule_pm%>',
                start: '<%=Resources.CRMTREEResource.ed_schedule_start%>',
                end: '<%=Resources.CRMTREEResource.ed_schedule_end%>',
                mon: '<%=Resources.CRMTREEResource.ed_schedule_mon%>',
                tue: '<%=Resources.CRMTREEResource.ed_schedule_tue%>',
                wed: '<%=Resources.CRMTREEResource.ed_schedule_wed%>',
                thu: '<%=Resources.CRMTREEResource.ed_schedule_thu%>',
                fri: '<%=Resources.CRMTREEResource.ed_schedule_fri%>',
                sat: '<%=Resources.CRMTREEResource.ed_schedule_sat%>',
                sun: '<%=Resources.CRMTREEResource.ed_schedule_sun%>',
                msg001: "<%=Resources.CRMTREEResource.ed_schedule_msg001%>"
            };
            //合并列
            _schedule.mergeCells = function () {
                var $dg = $(this._id);
                $dg.datagrid('mergeCells', {
                    index: 0,
                    field: 'Time_Text',
                    rowspan: 2
                }).datagrid('mergeCells', {
                    index: 2,
                    field: 'Time_Text',
                    rowspan: 2
                });
            };
            //结束编辑
            _schedule.endEditing = function () {
                //检查编辑
                var bCheckEdit = _schedule.checkEdit();
                if (!bCheckEdit) {
                    return bCheckEdit;
                }

                if (_schedule.lastEdit.index >= 0) {
                    var $dg = $(this._id);
                    $dg.datagrid('endEdit', _schedule.lastEdit.index);
                    _schedule.lastEdit = {};
                    _schedule.mergeCells();
                }

                return bCheckEdit;
            };
            //检查编辑
            _schedule.checkEdit = function () {
                if (!(_schedule.lastEdit.index >= 0)) { return true; }

                var bCheckEdit = true;
                var $dg = $(this._id);
                var cIndex;
                switch (_schedule.lastEdit.index) {
                    case 0:
                        cIndex = 1;
                        break;
                    case 1:
                        cIndex = 0;
                        break;
                    case 2:
                        cIndex = 3;
                        break;
                    case 3:
                        cIndex = 2;
                        break;
                }
                var field = _schedule.lastEdit.field;
                var index = _schedule.lastEdit.index;
                var cRow = $dg.datagrid('getRows')[cIndex];
                var cValue = $.trim(cRow[field]).replace(":", ".");
                if (cValue) {
                    var ed = $dg.datagrid('getEditor', { index: index, field: field });
                    if (ed) {
                        var edValue = $(ed.target).timespinner('getValue').replace(":", ".");
                        if (edValue) {
                            bCheckEdit = cRow.Flag === 'Start' ? cValue <= edValue : cValue >= edValue;
                            if (!bCheckEdit) {
                                $.msgTips.info('<%=Resources.CRMTREEResource.em_schedule_msg001%>');
                    }
                    window.setTimeout(function () { $dg.datagrid('selectRow', index); }, 0);
                }
            }
        }
        return bCheckEdit;
    };
    //开始编辑
    _schedule.beginEdit = function (index, field) {
        //结束编辑
        var bEndEdit = _schedule.endEditing();
        if (!bEndEdit) {
            return;
        }

        //开始新编辑
        var $dg = $(this._id);
        $dg.datagrid('editCell', { index: index, field: field });
        _schedule.lastEdit = { index: index, field: field };
        var ed = $dg.datagrid('getEditor', { index: index, field: field });
        var rowData = $dg.datagrid('selectRow', index).datagrid('getSelected');
        if (ed && ed.type === 'timespinner') {
            var time = {
                AM: { min: '00:00', max: '12:00' },
                PM: { min: '12:00', max: '24:00' }
            };
            $tp = ed.target;
            $.extend($tp.timespinner('options'), time[rowData.Time]);
            $tp.timespinner('textbox').select().attr('maxLength', 5);
        }
    };
    //单击单元格
    _schedule.onClickCell = function (index, field) {
        _schedule.beginEdit(index, field);
    };
    //结束编辑
    _schedule.onEndEdit = function (rowIndex, rowData) {
        return false;
    };
    //创建日程安排
    _schedule.create = function () {
        $(this._id).datagrid({
            fit: true,
            singleSelect: true,
            border: true,
            loadMsg: '',
            columns: [[
                { field: 'Time_Text', title: '', width: '120', align: 'center', resizable: false },
                { field: 'Flag_Text', title: '', width: '120', align: 'center', resizable: false },
                { field: 'Mon', title: _schedule.language.mon, width: '90', editor: 'timespinner', resizable: false },
                { field: 'Tue', title: _schedule.language.tue, width: '90', editor: 'timespinner', resizable: false },
                { field: 'Wed', title: _schedule.language.wed, width: '90', editor: 'timespinner', resizable: false },
                { field: 'Thu', title: _schedule.language.thu, width: '90', editor: 'timespinner', resizable: false },
                { field: 'Fri', title: _schedule.language.fri, width: '90', editor: 'timespinner', resizable: false },
                { field: 'Sat', title: _schedule.language.sat, width: '90', editor: 'timespinner', resizable: false },
                { field: 'Sun', title: _schedule.language.sun, width: '90', editor: 'timespinner', resizable: false }
            ]],
            onClickCell: _schedule.onClickCell,
            onEndEdit: _schedule.onEndEdit
        });
    };
    //格式化小时
    _schedule.formatHour = function (value) {
        value = parseInt(value);
        return (value < 10 ? '0' : '') + value;
    };
    //格式化时间
    _schedule.formatTime = function (v) {
        v = $.trim(v);
        if (v === '') { return v; }

        var a_v = v.split('.');
        a_v[0] = _schedule.formatHour(a_v[0]);
        if (a_v.length > 1) {
            a_v[1] += a_v[1].length === 1 ? '0' : '';
        } else {
            a_v.push('00');
        }

        return a_v.join(':');
    };
    //绑定数据
    _schedule.bindData = function (schedule) {
        if (schedule) {
            $.each(schedule, function (n, v) {
                if (n.indexOf('DP_D') !== -1) {
                    schedule[n] = _schedule.formatTime(v);
                }
            });
            var data = [
            {
                Time: 'AM', Flag: 'Start',
                Time_Text: _schedule.language.am, Flag_Text: _schedule.language.start,
                Mon: schedule.DP_D1_AM_S,
                Tue: schedule.DP_D2_AM_S,
                Wed: schedule.DP_D3_AM_S,
                Thu: schedule.DP_D4_AM_S,
                Fri: schedule.DP_D5_AM_S,
                Sat: schedule.DP_D6_AM_S,
                Sun: schedule.DP_D7_AM_S
            },
            {
                Time: 'AM', Flag: 'End',
                Time_Text: _schedule.language.am, Flag_Text: _schedule.language.end,
                Mon: schedule.DP_D1_AM_E,
                Tue: schedule.DP_D2_AM_E,
                Wed: schedule.DP_D3_AM_E,
                Thu: schedule.DP_D4_AM_E,
                Fri: schedule.DP_D5_AM_E,
                Sat: schedule.DP_D6_AM_E,
                Sun: schedule.DP_D7_AM_E
            },
            {
                Time: 'PM', Flag: 'Start',
                Time_Text: _schedule.language.pm, Flag_Text: _schedule.language.start,
                Mon: schedule.DP_D1_PM_S,
                Tue: schedule.DP_D2_PM_S,
                Wed: schedule.DP_D3_PM_S,
                Thu: schedule.DP_D4_PM_S,
                Fri: schedule.DP_D5_PM_S,
                Sat: schedule.DP_D6_PM_S,
                Sun: schedule.DP_D7_PM_S
            },
            {
                Time: 'PM', Flag: 'End',
                Time_Text: _schedule.language.pm, Flag_Text: _schedule.language.end,
                Mon: schedule.DP_D1_PM_E,
                Tue: schedule.DP_D2_PM_E,
                Wed: schedule.DP_D3_PM_E,
                Thu: schedule.DP_D4_PM_E,
                Fri: schedule.DP_D5_PM_E,
                Sat: schedule.DP_D6_PM_E,
                Sun: schedule.DP_D7_PM_E
            }
            ];
            $(_schedule._id).datagrid('loadData', data);
            _schedule.mergeCells();
        } else {
            _schedule.reset(false);
        }
    };
    //获得日程安排
    _schedule.get = function () {
        var bEndEdit = _schedule.endEditing();
        if (!bEndEdit) {
            return {};
        }

        var rows = $(_schedule._id).datagrid('getRows');
        var o_schedule = { DP_UType: 3 };
        $.each(rows, function (i, o) {
            $.each(o, function (n, d) {
                if (n === 'Time' && n === 'Flag' && n.lastIndexOf('_Text') !== -1) { return true; }
                d = $.trim(d);
                var num = _schedule.getDayOfWeek(n);
                if (d != "" && num > 0) {
                    var field = [
                        'DP',
                        'D' + num,
                        o.Time === 'AM' ? 'AM' : 'PM',
                        o.Flag === 'Start' ? 'S' : 'E',
                    ].join('_');
                    o_schedule[field] = d.replace(":", ".");
                }
            });
        });

        return o_schedule;
    };
    //获得这周的第几天
    _schedule.getDayOfWeek = function (week) {
        var o = { "Mon": 1, "Tue": 2, "Wed": 3, "Thu": 4, "Fri": 5, "Sat": 6, "Sun": 7 };
        return o[week];
    };
    //重置
    _schedule.reset = function (bConfirm) {
        var data = [
                {
                    Time: 'AM', Flag: 'Start',
                    Time_Text: _schedule.language.am, Flag_Text: _schedule.language.start
                },
                {
                    Time: 'AM', Flag: 'End',
                    Time_Text: _schedule.language.am, Flag_Text: _schedule.language.end
                },
                {
                    Time: 'PM', Flag: 'Start',
                    Time_Text: _schedule.language.pm, Flag_Text: _schedule.language.start
                },
                {
                    Time: 'PM', Flag: 'End',
                    Time_Text: _schedule.language.pm, Flag_Text: _schedule.language.end
                }
        ];
        if (bConfirm) {
            $.confirmWindow.reset(function () {
                $(_schedule._id).datagrid('loadData', data);
                _schedule.mergeCells();
            });
        } else {
            $(_schedule._id).datagrid('loadData', data);
            _schedule.mergeCells();
        }
    };
    //撤销
    _schedule.undo = function () {
        $.confirmWindow.undo(function () {
            $(_schedule._id).datagrid('rejectChanges');
            _schedule.mergeCells();
        });
    };

    //--------------------------------------------------------------------------------------
    //resources（资源安排）
    //--------------------------------------------------------------------------------------
    var _resources = {
        _id: '#dg_resources',
        DR_UType: []
    };
    //最后编辑项
    _resources.lastEdit = {};
    _resources.language = {
        am: '<%=Resources.CRMTREEResource.ed_schedule_am%>',
        pm: '<%=Resources.CRMTREEResource.ed_schedule_pm%>',
        start: '<%=Resources.CRMTREEResource.ed_schedule_start%>',
        end: '<%=Resources.CRMTREEResource.ed_schedule_end%>',
        mon: '<%=Resources.CRMTREEResource.ed_schedule_mon%>',
        tue: '<%=Resources.CRMTREEResource.ed_schedule_tue%>',
        wed: '<%=Resources.CRMTREEResource.ed_schedule_wed%>',
        thu: '<%=Resources.CRMTREEResource.ed_schedule_thu%>',
        fri: '<%=Resources.CRMTREEResource.ed_schedule_fri%>',
        sat: '<%=Resources.CRMTREEResource.ed_schedule_sat%>',
        sun: '<%=Resources.CRMTREEResource.ed_schedule_sun%>'
    };
    //合并列
    _resources.mergeCells = function () {
        var $dg = $(this._id);
        var len = $dg.datagrid('getRows').length;
        for (var i = 0; i < len; i++) {
            if (i % 2 == 0) {
                $dg.datagrid('mergeCells', {
                    index: i,
                    field: 'DR_UType_Text',
                    rowspan: 2
                });
            }
        }
    };
    //结束编辑
    _resources.endEditing = function () {
        //检查编辑
        var bCheckEdit = _resources.checkEdit();
        if (!bCheckEdit) {
            return bCheckEdit;
        }

        if (_resources.lastEdit.index >= 0) {
            var $dg = $(this._id);
            $dg.datagrid('endEdit', _resources.lastEdit.index);
            _resources.lastEdit = {};
            _resources.mergeCells();
        }

        return bCheckEdit;
    };
    //检查编辑
    _resources.checkEdit = function () {
        if (!(_resources.lastEdit.index >= 0)) { return true; }

        var bCheckEdit = true;

        return bCheckEdit;
    };
    //开始编辑
    _resources.beginEdit = function (index, field) {
        //结束编辑
        var bEndEdit = _resources.endEditing();
        if (!bEndEdit) {
            return;
        }

        //开始新编辑
        var $dg = $(this._id);
        $dg.datagrid('editCell', { index: index, field: field });
        _resources.lastEdit = { index: index, field: field };
        var ed = $dg.datagrid('getEditor', { index: index, field: field });
        if (ed && ed.type === 'numberspinner') {
            $c = ed.target;
            $c.numberspinner('textbox').select();
        }
    };
    //单击单元格
    _resources.onClickCell = function (index, field) {
        _resources.beginEdit(index, field);
    };
    //结束编辑
    _resources.onEndEdit = function (rowIndex, rowData) {
        return false;
    };
    //创建资源
    _resources.create = function () {
        $(this._id).datagrid({
            fit: true,
            singleSelect: true,
            border: true,
            nowrap: false,
            loadMsg: '',
            columns: [[
                { field: 'DR_UType_Text', title: '', width: '120', align: 'center', resizable: false },
                { field: 'Flag_Text', title: '', width: '120', align: 'center', resizable: false },
                {
                    field: 'Mon', title: _resources.language.mon, width: '90', editor: {
                        type: 'numberspinner', options: {
                            min: 0
                        }
                    }, resizable: false
                },
                {
                    field: 'Tue', title: _resources.language.tue, width: '90', editor: {
                        type: 'numberspinner', options: {
                            min: 0
                        }
                    }, resizable: false
                },
                {
                    field: 'Wed', title: _resources.language.wed, width: '90', editor: {
                        type: 'numberspinner', options: {
                            min: 0
                        }
                    }, resizable: false
                },
                {
                    field: 'Thu', title: _resources.language.thu, width: '90', editor: {
                        type: 'numberspinner', options: {
                            min: 0
                        }
                    }, resizable: false
                },
                {
                    field: 'Fri', title: _resources.language.fri, width: '90', editor: {
                        type: 'numberspinner', options: {
                            min: 0
                        }
                    }, resizable: false
                },
                {
                    field: 'Sat', title: _resources.language.sat, width: '90', editor: {
                        type: 'numberspinner', options: {
                            min: 0
                        }
                    }, resizable: false
                },
                {
                    field: 'Sun', title: _resources.language.sun, width: '90', editor: {
                        type: 'numberspinner', options: {
                            min: 0
                        }
                    }, resizable: false
                }
            ]],
            onClickCell: _resources.onClickCell,
            onEndEdit: _resources.onEndEdit
        });

    };
    _resources.get_DR_UType_Text = function (DR_UType) {
        var DR_UType_Text = '';
        var dr_utype = _resources.DR_UType;
        for (var i = 0, len = dr_utype.length; i < len; i++) {
            if (dr_utype[i].DR_UType == DR_UType) {
                DR_UType_Text = dr_utype[i].DR_UType_Text;
                break;
            }
        }
        return DR_UType_Text ? DR_UType_Text : DR_UType;
    };
    //绑定数据
    _resources.bindData = function (resources) {
        if (resources) {
            var data = [];
            $.each(resources, function (i, o) {
                o.DR_UType_Text = _resources.get_DR_UType_Text(o.DR_UType);

                data.push({
                    DR_UType: o.DR_UType,
                    DR_UType_Text: o.DR_UType_Text,
                    DR_AU_AD_Code: o.DR_AU_AD_Code,
                    Flag: 'AM',
                    Flag_Text: 'AM',
                    Mon: o.DR_D1_AM,
                    Tue: o.DR_D2_AM,
                    Wed: o.DR_D3_AM,
                    Thu: o.DR_D4_AM,
                    Fri: o.DR_D5_AM,
                    Sat: o.DR_D6_AM,
                    Sun: o.DR_D7_AM
                });

                data.push({
                    DR_UType: o.DR_UType,
                    DR_UType_Text: o.DR_UType_Text,
                    DR_AU_AD_Code: o.DR_AU_AD_Code,
                    Flag: 'PM',
                    Flag_Text: 'PM',
                    Mon: o.DR_D1_PM,
                    Tue: o.DR_D2_PM,
                    Wed: o.DR_D3_PM,
                    Thu: o.DR_D4_PM,
                    Fri: o.DR_D5_PM,
                    Sat: o.DR_D6_PM,
                    Sun: o.DR_D7_PM
                });
            });

            $(_resources._id).datagrid('loadData', data);
            _resources.mergeCells();
        } else {
            _resources.reset(false);
        }
    };
    //获得日程安排
    _resources.get = function () {
        var bEndEdit = _resources.endEditing();
        if (!bEndEdit) {
            return {};
        }

        var rows = $(_resources._id).datagrid('getRows');
        var resources = [];
        var res = {};
        $.each(rows, function (i, o) {
            if (i % 2 == 0) {
                res = { DR_UType: o.DR_UType };
            }
            $.each(o, function (n, d) {
                if (n === 'Flag' || n.lastIndexOf('_Text') !== -1) { return true; }
                d = $.trim(d);
                var num = _resources.getDayOfWeek(n);
                if (d != "" && num > 0) {
                    var field = [
                        'DR',
                        'D' + num,
                        o.Flag === 'AM' ? 'AM' : 'PM'
                    ].join('_');
                    res[field] = d;
                }
            });
            if (i % 2 == 1) {
                resources.push(res);
            }
        });

        return resources;
    };
    //获得这周的第几天
    _resources.getDayOfWeek = function (week) {
        var o = { "Mon": 1, "Tue": 2, "Wed": 3, "Thu": 4, "Fri": 5, "Sat": 6, "Sun": 7 };
        return o[week];
    };
    //重置
    _resources.reset = function (bConfirm) {
        var data = [];
        $.each(_resources.DR_UType, function (i, o) {
            data.push($.extend({}, o, { Flag: 'AM', Flag_Text: 'AM' }));
            data.push($.extend({}, o, { Flag: 'PM', Flag_Text: 'PM' }));
        });

        if (bConfirm) {
            $.confirmWindow.reset(function () {
                $(_resources._id).datagrid('loadData', data);
                _resources.mergeCells();
            });
        } else {
            $(_resources._id).datagrid('loadData', data);
            _resources.mergeCells();
        }
    };
    //撤销
    _resources.undo = function () {
        $.confirmWindow.undo(function () {
            $(_resources._id).datagrid('rejectChanges');
            _resources.mergeCells();
        });
    };


    //--------------------------------------------------------------------------------------
    //departments（部门信息）
    //--------------------------------------------------------------------------------------
    var _departments = {
        data: [],
        _id: '#dg_departments'
    };
    _departments.language = {
        buttons: {
            add: '<%=Resources.CRMTREEResource.cm_cars_buttons_add%>',
            remove: '<%=Resources.CRMTREEResource.cm_cars_buttons_remove%>'
        }
    };
    //取消编辑
    _departments.cancelEdit = function () {
        var $dg = $(_departments._id);
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            $dg.datagrid('cancelEdit', i);
        }
    }
    //结束编辑不验证
    _departments.endEditingNoValid = function () {
        var $dg = $(_departments._id);
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            var ed = $dg.datagrid('getEditor', { index: i, field: 'PDN_PDT_Code' });
            if (ed && ed.type === 'combobox') {
                var typeText = $(ed.target).combobox('getText');
                rows[i][ed.field + '_Text'] = typeText;
            }
            $dg.datagrid('endEdit', i);
        }
    }
    //结束编辑需要验证
    _departments.endEditing = function () {
        var $dg = $(_departments._id);
        var rows = $dg.datagrid('getRows');
        var bValid = true;
        for (var i = 0, len = rows.length; i < len; i++) {
            var eds = $dg.datagrid('getEditors', i);
            if (eds.length === 0) { continue; }
            for (var j = 0; j < eds.length; j++) {
                var ed = eds[j];
                if (!ed) {
                    continue;
                }
                var type = ed.type;
                var $target = $(ed.target);
                var isValid = true;
                if (type === 'combobox') {
                    isValid = $target.combobox('enableValidation').combobox('isValid');
                }
                if (type === 'combo') {
                    isValid = $target.combo('enableValidation').combo('isValid');
                }
                if (!isValid) {
                    bValid = false;
                    continue;
                }
                if (type === 'combobox') {
                    var typeText = $target.combobox('getText');
                    rows[i][ed.field + '_Text'] = typeText;
                }
            }
            if (bValid) {
                $dg.datagrid('endEdit', i);
            } else {
                $dg.datagrid('selectRow', i);
            }
        }
        return bValid;
    }
    //点击单元格,开启编辑
    _departments.onClickCell = function (index, field) {
        if (field !== 'btnLast') {
            _departments.endEditingNoValid();
            var $dg = $(this);
            var rowData = $dg.datagrid('selectRow', index).datagrid('getSelected');

            //已保存数据不能修改类型
            if (field === 'PDN_PDT_Code' && rowData.PDN_Code > 0) return;

            if (field === 'EX_Variables' && rowData.PDT_Var_Type == 1) {
                if (rowData.EX_Values) {
                    _values.bindData(rowData.EX_Values);
                    $("#w_values").window('open');
                } else {
                    var params = {
                        action: 'Get_Dept_Variables',
                        PDV_PDT_Code: rowData.PDN_PDT_Code,
                        DV_PDN_Code: (rowData.PDN_Code ? rowData.PDN_Code : 0)
                    };
                    $.mask.show();
                    $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                        $.mask.hide();
                        if (!$.checkResponse(res)) { return; }
                        _values.bindData(res);
                        $("#w_values").window('open');
                    }, "json");
                }
                return;
            }

            $dg.datagrid('editCell', { index: index, field: field });

            var ed = $dg.datagrid('getEditor', { index: index, field: field });
            if (ed && ed.type === 'combobox') {
                ed.target.combobox('showPanel');
            }
            if (ed && ed.type === 'textbox') {
                $c = ed.target;
                $c.textbox('textbox').select();
            }
            if (ed && ed.type === 'numberspinner') {
                $c = ed.target;
                $c.numberspinner('textbox').select();
            }
        }
    }
    //创建添加按钮
    _departments.createAdd = function () {
        var $btns = $("#tb_departments div.btns");
        var a_o = [
            { icon: 'icon-add', text: _departments.language.buttons.add, clickFun: '_departments.add(event,this);' }
        ];
        var a_btns = setBtns(a_o);
        $btns.html(a_btns.join(''));
    }
    //创建
    _departments.create = function () {
        var $dg = $(_departments._id);
        $dg.datagrid({
            url: null,
            fit: true,
            toolbar: '#tb_departments',
            rownumbers: true,
            singleSelect: true,
            showHeader: false,
            border: true,
            nowrap: false,
            loadMsg: '',
            columns: [[
                {
                    field: 'PDN_PDT_Code', title: 'Dept Type', width: 150,
                    formatter: function (value, row) {
                        return row.PDN_PDT_Code_Text ? row.PDN_PDT_Code_Text : value;
                    },
                    editor: {
                        type: 'combobox', options: {
                            novalidate: true,
                            required: true,
                            data: _departments.data,
                            onSelect: function (record) {
                                var row = $dg.datagrid('getSelected');
                                row.PDT_Var_Type = record.PDT_Var_Type;
                                //row.PDN_Variable = "";
                                row.EX_Variables = "";
                                row.EX_Values = null;
                                row.PDN_Name = "";
                                window.setTimeout(function () { _departments.endEditingNoValid(); }, 0);
                            }
                        }
                    }
                },
                {
                    field: 'PDN_Name', title: 'Name', width: 200, editor: {
                        type: 'textbox',
                        options: {
                            novalidate: true,
                            required: true
                        }
                    }
                },
                //{
                //    field: 'PDN_Variable_Desc', title: 'Variable Desc',align:'center', width: 80,
                //    formatter: function (value, row) {
                //        return '# Of Bays';
                //    }
                //},
                {
                    field: 'EX_Variables', title: 'Variable', width: 400
                    //, editor: {
                    //    type: 'textbox',
                    //    options: {
                    //        novalidate: true,
                    //        required: true
                    //    }
                    //}
                },
                {
                    field: 'btnLast', align: 'center', width: 35,
                    formatter: function (value, row, index) {
                        var a_o = [
                        { icon: 'icon-remove', title: _departments.language.buttons.remove, clickFun: '_departments.remove(event,this);' }
                        ];
                        return setBtns(a_o, true).join('');
                    }
                }
            ]],
            onClickCell: _departments.onClickCell
        });

        _departments.createAdd();
    };
    //添加
    _departments.add = function () {
        var $dg = $(_departments._id);
        _departments.endEditingNoValid();
        var row = $dg.datagrid('getSelected');
        var rowIndex = undefined;
        if (row) {
            rowIndex = $dg.datagrid('getRowIndex', row) + 1;
        }
        $dg.datagrid('insertRow', {
            index: rowIndex,
            row: {}
        });
        rowIndex = row ? rowIndex : $dg.datagrid('getRows').length - 1;
        $dg.datagrid('selectRow', rowIndex);
    }
    //删除
    _departments.remove = function (e, target) {
        var $dg = $(_departments._id);
        var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
        var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
        if (rowData.PDN_AD_Code > 0) {
            $.confirmWindow.tempRemove(function () {
                $dg.datagrid('deleteRow', rowIndex);
            });
        } else {
            if (rowIndex >= 0) {
                $dg.datagrid('deleteRow', rowIndex);
            }
        }
        stopPropagation(e);
    };
    //获得
    _departments.get = function () {
        _departments.endEditingNoValid();

        var $dg = $(_departments._id);
        var rows = $dg.datagrid('getRows');
        var deletedRows = $dg.datagrid('getChanges', 'deleted');
        return { changes: rows, deletes: deletedRows };
    };
    //绑定数据
    _departments.bindData = function (departments) {
        $(_departments._id).datagrid('loadData', departments ? departments : []);
    };

    //--------------------------------------------------------------------------------------
    //Service（服务）
    //--------------------------------------------------------------------------------------
    var _service = {
        save: function () {
            var _serTab = $("#tabs_service").tabs('getSelected');
            var index = $("#tabs_service").tabs('getTabIndex', _serTab);
            if (index == 4) {
                _camp_approval.endEditingNoValid();
                var auth_process = _camp_approval.get();
                if (!auth_process) {
                    $("#tabs_service").tabs('select', 4);
                    return;
                }
                var params = { action: 'Seve_Process', saveData: auth_process };
                $.mask.show();
                $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                    $.mask.hide();
                    $("#btnSave").linkbutton('enable');
                    if ($.checkResponse(res)) {
                        $.msgTips.save(true);
                    }
                }, "json");
            } else if (index == 5) {
                _kpi.endEditingNoValidCell();
                _kpi.getChildData_2();

                var _d = $(_kpi.id).datagrid('getRows');
                var params = { action: 'Seve_KPI', _o: _d };
                $.mask.show();
                $.post(_url_crmhand, { o: JSON.stringify(params) }, function (res) {
                    $.mask.hide();
                    $("#btnSave").linkbutton('enable');
                    if ($.checkResponse(res)) {
                        $.msgTips.save(true);
                    }
                }, "json");
            }
            else{
                var params = { action: 'Save_Service' };
                params.schedule = _schedule.get();
                params.resources = _resources.get();
                params.departments = _departments.get();
                //return;
                $.each(params.departments.changes, function (i, o) {
                    var pdn_name = $.trim(o.PDN_Name);
                    if (pdn_name) {
                        //检测是否含中文字符
                        var reg = /[\u2E80-\u9FFF]+/;
                        var bCN = reg.test(pdn_name);
                        if (bCN) {
                            o.PDN_Name_CN = pdn_name;
                        } else {
                            o.PDN_Name_EN = pdn_name;
                        }
                    }
                });
                params.options = {
                    SD_PDN_Code: 2,
                    SD_SA_Selection: $('input:radio[name="SD_SA_Selection"]:checked').val() == 1,
                    SD_Serv_Package: $('input:radio[name="SD_Serv_Package"]:checked').val() == 1
                };
                var CallValiDate = _Call_Per.ValiDate(), CallDate = null;
                if (CallValiDate) {
                    for (var i = 0; i < _Call_Per.$CamCall.length; i++) {
                        if (_Call_Per.$CamCall[i].Remove) {
                            _Call_Per.$CamCall.splice(i, 1);
                            i--;
                        }
                    }
                    CallDate = _Call_Per.$CamCall;
                }
                params.callValiDate = CallValiDate;
                params.callDatd = CallDate;
                $.mask.show();
                $("#btnSave").linkbutton('disable');
                var s_params = JSON.stringify(params);
                $.post(_s_url, { o: s_params }, function (res) {
                    $.mask.hide();
                    $("#btnSave").linkbutton('enable');
                    if ($.checkResponse(res)) {
                        $.msgTips.save(true);
                    }
                }, "json");
            }
        }
    };

    //--------------------------------------------------------------------------------------
    //vlaues（参数值信息）
    //--------------------------------------------------------------------------------------
    var _values = {
        data: [],
        _id: '#dg_values'
    };
    //取消编辑
    _values.cancelEdit = function () {
        var $dg = $(_values._id);
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            $dg.datagrid('cancelEdit', i);
        }
    }
    //结束编辑不验证
    _values.endEditingNoValid = function () {
        var $dg = $(_values._id);
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            var ed = $dg.datagrid('getEditor', { index: i, field: 'PDN_PDT_Code' });
            if (ed && ed.type === 'combobox') {
                var typeText = $(ed.target).combobox('getText');
                rows[i][ed.field + '_Text'] = typeText;
            }
            $dg.datagrid('endEdit', i);
        }
    }
    //结束编辑需要验证
    _values.endEditing = function () {
        var $dg = $(_values._id);
        var rows = $dg.datagrid('getRows');
        var bValid = true;
        for (var i = 0, len = rows.length; i < len; i++) {
            var eds = $dg.datagrid('getEditors', i);
            if (eds.length === 0) { continue; }
            for (var j = 0; j < eds.length; j++) {
                var ed = eds[j];
                if (!ed) {
                    continue;
                }
                var type = ed.type;
                var $target = $(ed.target);
                var isValid = true;
                if (type === 'combobox') {
                    isValid = $target.combobox('enableValidation').combobox('isValid');
                }
                if (type === 'combo') {
                    isValid = $target.combo('enableValidation').combo('isValid');
                }
                if (!isValid) {
                    bValid = false;
                    continue;
                }
                if (type === 'combobox') {
                    var typeText = $target.combobox('getText');
                    rows[i][ed.field + '_Text'] = typeText;
                }
            }
            if (bValid) {
                $dg.datagrid('endEdit', i);
            } else {
                $dg.datagrid('selectRow', i);
            }
        }
        return bValid;
    }
    //点击单元格,开启编辑
    _values.onClickCell = function (index, field) {
        if (field === 'DV_Value') {
            _values.endEditingNoValid();
            var $dg = $(this);
            var rowData = $dg.datagrid('selectRow', index).datagrid('getSelected');

            var col = $dg.datagrid('getColumnOption', field);
            var editor = {};
            switch (rowData.PDV_Type) {
                /*
               1 : Int
               2 : Money
               3 : String
               */
                case 1:
                    editor.type = 'numberspinner';
                    editor.options = { min: 0 };
                    break;
                case 2:
                    editor.type = 'numberspinner';
                    editor.options = { min: 0, precision: 2 };
                    break;
                default:
                    editor.type = 'textbox';
                    break;
            }
            col.editor = editor;

            $dg.datagrid('editCell', { index: index, field: field });

            var ed = $dg.datagrid('getEditor', { index: index, field: field });
            if (ed && ed.type === 'textbox') {
                $c = ed.target;
                $c.textbox('textbox').select();
            }
            if (ed && ed.type === 'numberspinner') {
                $c = ed.target;
                $c.numberspinner('textbox').select();
            }
        } else {
            _values.endEditingNoValid();
        }
    }
    //创建
    _values.create = function () {
        var $dg = $(_values._id);
        $dg.datagrid({
            url: null,
            fit: true,
            //rownumbers: true,
            singleSelect: true,
            //showHeader: false,
            fitColumns: true,
            border: false,
            nowrap: false,
            loadMsg: '',
            columns: [[
                {
                    field: 'PDV_Prompt', title: '<%=Resources.CRMTREEResource.pName%>', width: 200
                },
                {
                    field: 'DV_Value', title: '<%=Resources.CRMTREEResource.value%>', width: 150
                }
            ]],
            onClickCell: _values.onClickCell
        });
    };
    //应用
    _values.accept = function () {
        _values.endEditingNoValid();

        var $dg = $(_values._id);
        var rows = $dg.datagrid('getRows');
        var values = [];
        $.each(rows, function (i, row) {
            values.push($.trim(row.PDV_Prompt) + $.trim(row.DV_Value));
        });
        var s = values.join(', ');

        var $dg_dep = $(_departments._id);
        var dr = $dg_dep.datagrid('getSelected');
        dr.EX_Values = rows;
        dr.EX_Variables = s;
        $dg_dep.datagrid('refreshRow', $dg_dep.datagrid('getRowIndex', dr));

        _values.close();
    };
    //关闭
    _values.close = function () {
        $("#w_values").window('close');
    };
    //获得
    _values.get = function () {
        _departments.endEditingNoValid();

        var $dg = $(_values._id);
        var rows = $dg.datagrid('getRows');
        var deletedRows = $dg.datagrid('getChanges', 'deleted');
        return { changes: rows, deletes: deletedRows };
    };
    //绑定数据
    _values.bindData = function (data) {
        $(_values._id).datagrid('loadData', data ? data : []);
    };
    var _options = {
        bindData: function (data) {
            if (data) {
                var SD_SA_Selection = data.SD_SA_Selection ? 1 : 0;
                var SD_Serv_Package = data.SD_Serv_Package ? 1 : 0;
                $('input:radio[name="SD_Serv_Package"][value="' + SD_Serv_Package + '"]').prop('checked', true);
                $('input:radio[name="SD_SA_Selection"][value="' + SD_SA_Selection + '"]').prop('checked', true);
            }
        }
    };
    $(function () {
        //初始化
        (function Init() {
            $.getWords([4105, 4196], function (data) {
                if (data) {
                    var DR_UType = $.map(data._4105, function (o) {
                        return { DR_UType: o.value, DR_UType_Text: o.text };
                    });
                    _resources.DR_UType = DR_UType;
                    $("#AT_IType").combobox({ data: data._4196 });
                }

                _schedule.create();
                _schedule.reset(false);

                _resources.create();
                _resources.reset(false);
                $.post(_s_url, { o: JSON.stringify({ action: 'Get_Dept_Type' }) }, function (data) {
                    if (data) {
                        _departments.data = data;
                    }

                    _departments.create();


                    $.GetWordsByIds([4093, 4170, 4184], function (data) {
                        $("#EX_Camp_Category").combobox({ data: data });

                        $.post(_s_url, { o: JSON.stringify({ action: 'Get_User_Groups' }) }, function (data) {
                            if (data) {
                                _camp_approval.data_user_groups = data;
                            }
                            _camp_approval.create();

                            BindService();
                        }, "json");
                    });

                    _values.create();
                }, "json");

                InitTabs();
            });
        })();
        function BindService() {
            var params = { action: 'Get_Service', DP_UType: 3, SD_PDN_Code: 2 };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (!$.checkResponse(res)) { return; }
                _schedule.bindData(res.schedule);

                _resources.bindData(res.resources);

                _departments.bindData(res.departments);

                _options.bindData(res.options);

                // _camp_approval.bindData(res.approval);

                $("#btnSave").linkbutton('enable');
            }, "json");


        }
        //初始化面板页
        function InitTabs() {
            $("#tabs_service").tabs('select', 3);
        }
        _Call_Per.init();
        $('#tabs_service').tabs({
            border: false,
            onSelect: function (title, index) {
                if (index == 4) {
                    var _data = $('#dg_auth_process').datagrid('getData');
                    if (_data.total == 0) {
                        _camp_approval.selectATIType();
                    }
                } else if (index == 5) {
                    _kpi.$tableData = null;
                    _kpi.$dataSource = null;
                    _kpi.$c_index = null;
                    _kpi.create();
                    var params = { action: 'getDepartmentKPI', AD_Code: 3 };
                    $.post(_url_crmhand, { o: JSON.stringify(params) }, function (res) {
                        if (res) {
                            _kpi.$dataSource = res;
                            _kpi.$tableData = _kpi.getOkdata();
                            $(_kpi.id).datagrid('loadData', _kpi.$tableData ? _kpi.$tableData : []);
                        }
                    }, "json");
                }
            }
        });
        $('#tabs').tabs({
            border: false,
            onSelect: function (title) {
            }
        });
    });
    function btnsearch() {
        var _EX_Camp_Category = $('#EX_Camp_Category').combobox('getValue');
        var _AT_IType = $('#AT_IType').combobox('getValue');
        var _AT_SType = $('#AT_SType').combobox('getValues');
        var _AT_STypes = "";
        for (var i = 0; i < _AT_SType.length; i++) {
            _AT_STypes += _AT_SType[i] + ',';
        }
        if (_EX_Camp_Category == '' || _AT_IType == '' || _AT_STypes == '')
            return;
        var params = {
            action: 'Get_approval', DP_UType: 3, SD_PDN_Code: 2, EX_Camp_Category: _EX_Camp_Category
        , AT_IType: _AT_IType, AT_SType: _AT_STypes
        };
        $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
            if (!$.checkResponse(res)) { return; }
            _camp_approval.bindData(res.approval);
        }, "json");
    }
    var _Call_Per = {};
    _Call_Per.$CamCall = [];
    _Call_Per.$TeamGroup = [];
    //添加电话比例初始化
    _Call_Per.init = function () {
        var _cg_code = _params.CG_Code;
        if (_cg_code == null || $.trim(_cg_code) == "") {
            _cg_code = 0;
        }
        $.post(_s_url_AddCall, { o: JSON.stringify({ action: 'GetCamCall', CG_Code: _cg_code }) }, function (res) {
            if ($.checkErrCode(res)) {
                _Call_Per.$TeamGroup = res.TeamGroup;
                _Call_Per.$CamCall = res.CamCall;
                _Call_Per.getHTML();
            }
        }, 'json');
    };
    //动态添加HTML
    _Call_Per.getHTML = function () {
        if (!_Call_Per.$CamCall || !_Call_Per.$CamCall.length) {
            return false;
        }
        var _setData = _Call_Per.$CamCall;
        $(".e_ui_all").empty();
        for (var i = 0; i < _setData.length; i++) {
            var _html = _Call_Per.setHTML(i, _setData[i]);
            $(".e_ui_all").append(_html);
            $.parser.parse(".e_ui_all .UI_II");
            _Call_Per.ComboboxLoading();
        }
    };
    // 添加行
    _Call_Per.AddHtml = function () {
        var _index = 0;
        if (_Call_Per.$CamCall && _Call_Per.$CamCall.length > 0) {
            _index = _Call_Per.$CamCall.length;
            _Call_Per.$CamCall.push({ CC_Percent: 0 });
            _Call_Per.$CamCall[_index].Add = true;
        }
        var _html = _Call_Per.setHTML(_index, { CC_Percent: 0 });
        $(".e_ui_all").append(_html);
        $.parser.parse(".e_ui_all .UI_II");
        _Call_Per.ComboboxLoading_index(_index);
    }
    //删除行
    _Call_Per.RemoveHtml = function () {
        var _obj = $(this);
        var _index = _obj.parent().parent().attr("_index");
        if (_Call_Per.$CamCall && _Call_Per.$CamCall.length > 0) {
            _Call_Per.$CamCall[_index].Remove = true;
        }
        $(this).parent().parent().remove();
    }
    //用户组选择事件
    _Call_Per.comboboxOnSelectTeamGroup = function (record) {
        var _index = _Call_Per.$ClickRow;
        if (!_Call_Per.$CamCall || !_Call_Per.$CamCall.length) {
            return false;
        }
        _Call_Per.$CamCall[_index].TG_Code = record.TG_Code;
        _Call_Per.$CamCall[_index].TG_UG_Code = record.TG_UG_Code;
        _Call_Per.$CamCall[_index].CC_Team = record.TG_Code;
        $.post(_s_url_AddCall, { o: JSON.stringify({ action: 'GetTeamGroupUser', UG_Code: record.TG_UG_Code }) }, function (res) {
            if ($.checkErrCode(res)) {
                $("#CamCall" + _index).combobox("loadData", res.TeamGroupUser).combobox("setValue", -1);
            }
        }, 'json');
    }
    //用户选择事件
    _Call_Per.comboboxOnSelectCamCall = function (record) {
        var _index = _Call_Per.$ClickRow;
        if (!_Call_Per.$CamCall || !_Call_Per.$CamCall.length) {
            return false;
        }
        _Call_Per.$CamCall[_index].AU_Code = record.AU_Code;
        _Call_Per.$CamCall[_index].CC_DE_Code = record.DE_Code;
    }
    //设置行点击索引
    _Call_Per.UIOnClick = function (obj) {
        _Call_Per.$ClickRow = $(obj).attr("_index");
    }
    _Call_Per.ValiDate = function () {
        if (!_Call_Per.$CamCall || !_Call_Per.$CamCall.length) {
            return false;
        }
        var _Count = 0;
        $.each(_Call_Per.$CamCall, function (i, o) {
            if (!o.Remove) {
                _Count += parseInt(o.CC_Percent);
            }
        })
        if (_Count == 100) {
            return true;
        } else {
            return false;
        }

    }
    _Call_Per.SetPercent = function (obj) {
        var _obj = $(obj);
        var _index = _obj.parent().parent().attr("_index");
        var _value = _obj.val();
        _Call_Per.$CamCall[_index].CC_Percent = _value;
    }
    _Call_Per.setHTML = function (index, data) {
        var _htm = "<div class=\"e_ui_list\" _index=\"" + index + "\" onclick=\"_Call_Per.UIOnClick(this)\">"
    + "                        <span class=\"I\">"
    + "                            <input id=\"TeamGroup" + index + "\" class=\"easyui-combobox\" style=\"width: 130px;\" /></span>"
    + "                        <span>"
    + "                            <input id=\"CamCall" + index + "\" class=\"easyui-combobox\" style=\"width: 110px;\" /></span>"
    + "                        <span class=\"I\" style=\"vertical-align: middle\">"
    + "                            <input class=\"CallRatio\" type=\"text\" style=\"width: 32px\" value=" + data.CC_Percent + " onblur=\"_Call_Per.SetPercent(this)\" />%</span>"
    + "                        <span class=\"UI_II\">"
    + "                            <a id=\"CallDelete" + index + "\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-remove',onClick:_Call_Per.RemoveHtml\"><%=Resources.CRMTREEResource.btnDelete %></a></span>"
+ "                    </div>";
        var _html = _htm;
        return _html;
    };
    _Call_Per.ComboboxLoading = function () {
        if (!_Call_Per.$CamCall || !_Call_Per.$CamCall.length) {
            return false;
        }
        var _setData = _Call_Per.$CamCall;
        for (var i = 0; i < _setData.length; i++) {
            $("#TeamGroup" + i).combobox({
                valueField: "TG_Code", textField: "UG_Name", onSelect: function (record) { _Call_Per.comboboxOnSelectTeamGroup(record); }
            }).combobox("loadData", _Call_Per.$TeamGroup).combobox("setValue", _setData[i].TG_Code);
            $("#CamCall" + i).combobox({
                valueField: "AU_Code", textField: "AU_Name", onSelect: function (record) { _Call_Per.comboboxOnSelectCamCall(record); }
            }).combobox("setValue", _setData[i].AU_Code).combobox("setText", _setData[i].AU_Name);
        }
    }
    _Call_Per.ComboboxLoading_index = function (Index) {
        $("#TeamGroup" + Index).combobox({
            valueField: "TG_Code", textField: "UG_Name", onSelect: function (record) { _Call_Per.comboboxOnSelectTeamGroup(record); }
        }).combobox("loadData", _Call_Per.$TeamGroup).combobox("setValue", 1);

        $("#CamCall" + Index).combobox({
            valueField: "AU_Code", textField: "AU_Name", onSelect: function (record) { _Call_Per.comboboxOnSelectCamCall(record); }
        }).combobox("setValue", -1).combobox("setText", "任何用户").combobox("select", -1);
        if (Index != 0) {
            _Call_Per.$CamCall[Index].TG_Code = 1;
            _Call_Per.$CamCall[Index].TG_UG_Code = 0;
            _Call_Per.$CamCall[Index].CC_Team = 1;
            _Call_Per.$CamCall[Index].AU_Code = -1;
            _Call_Per.$CamCall[Index].CC_DE_Code = 0;
        } else {
            _Call_Per.$CamCall.push({ TG_Code: 1, TG_UG_Code: 0, CC_Team: 1, AU_Code: -1, CC_DE_Code: 0 });
        }
        $.post(_s_url_AddCall, { o: JSON.stringify({ action: 'GetTeamGroupUser', UG_Code: 26 }) }, function (res) {
            if ($.checkErrCode(res)) {
                $("#CamCall" + Index).combobox("loadData", res.TeamGroupUser).combobox("setValue", -1);
            }
        }, 'json');
    }
</script>
