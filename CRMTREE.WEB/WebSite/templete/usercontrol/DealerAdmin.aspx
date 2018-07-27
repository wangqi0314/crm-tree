<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealerAdmin.aspx.cs" Inherits="templete_usercontrol_DealerAdmin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>

    <script src="/scripts/plupload-1.5.7/plupload.js" type="text/javascript"></script>
    <script src="/scripts/plupload-1.5.7/plupload.flash.js" type="text/javascript"></script>
    <script src="/scripts/plupload-1.5.7/jquery.plupload.queue/jquery.plupload.queue.js" type="text/javascript"></script>

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
            document.write('<script src="/scripts/plupload-1.5.7/i18n/zh_CN.js" type="text/javascript"></sc' + 'ript>');
        }
    </script>
    <style type="text/css">
        html, body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
        }

        table.service tr td {
            padding:5px;
            white-space:nowrap;
        }
        table.service tr td.text {
            padding-right:10px;
        }

        .datagrid-toolbar{
            border-width:0px;
        }

        table.tbl_service tr td{
            margin:0;padding:0;
            border:1px solid red;
        }

        /*
        upload
        */
        img {
            border: 0;
            margin: 0;
            padding: 0;
        }

        .picItem {
            margin: 0;
            padding: 0;
            float: left;
            position: relative;
            width: 60px;
            height: 60px;
            margin-right: 5px;
            margin-bottom: 5px;
            background: #F0F0F0;
        }

            .picItem img {
                width: 60px;
                height: 60px;
                cursor: pointer;
            }

            .picItem div.btn {
                cursor: pointer;
                text-align: center;
                color: #404040;
                line-height: 30px;
            }

            .picItem div.btnbg {
                background: #E0F892;
            }

            .picItem div.progress {
                position: absolute;
                top: 50%;
                margin-top: -10px;
            }

            .picItem div.remove {
                position: absolute;
                top: 0;
                right: 0;
                z-index: 1;
                width: 18px;
                height: 18px;
                background: url(/scripts/jquery-easyui/themes/icons/remove.png) no-repeat center center;
                text-align: center;
                cursor: pointer;
            }
    </style>
</head>
<body>
    <div class="easyui-layout" data-options="fit:true">
    <div data-options="region:'center',border:false" style="padding:0px;padding-top:5px;">
        <div id="tabs" class="easyui-tabs" data-options="fit:true,selected:3,plain:true,border:false">
            <div title="<%=Resources.CRMTREEResource.dm_tabs_Summary%>" data-options="iconCls:'icon-customer-summary'" style="padding:10px">
                <div class="easyui-panel" data-options="fit:true,border:false" style="overflow:hidden">
                        
                </div>
            </div>
            <div title="<%=Resources.CRMTREEResource.dm_tabs_Sales%>" data-options="iconCls:'icon-customer-personal'" style="padding:10px">
                <div class="easyui-panel" data-options="fit:true,border:false" style="overflow:hidden">
                        
                </div>
            </div>
           
            <div title="<%=Resources.CRMTREEResource.dm_tabs_Service%>" data-options="iconCls:'icon-customer-personal'" style="padding:20px;overflow:hidden;position:relative;">
                <div class="easyui-panel" data-options="border:false,height:410" style="overflow:hidden">
                        <div id="tabs_service" class="easyui-tabs" data-options="fit:true,selected:0,plain:true">
                            <div title="<%=Resources.CRMTREEResource.dm_tab_Schedule%>" style="padding:20px;">
                                <table id="dg_schedule"></table>
                            </div>
                            <div title="<%=Resources.CRMTREEResource.dm_tab_Resources%>" style="padding:20px;">
                                <table id="dg_resources"></table>
                            </div>
                            <div title="<%=Resources.CRMTREEResource.dm_tab_Departments%>" style="padding:20px;">
                                <table id="dg_departments"></table>
                            </div>
                            <div title="<%=Resources.CRMTREEResource.dm_tab_Options%>" style="padding:20px;">
                                <table class="form" border="0" cellpadding="3" cellspacing="2">
                                    <tr>
                                        <td><input type="hidden" id="SD_Code" value="0"/></td>
                                        <td style="width:40px;text-align:center;"><%=Resources.CRMTREEResource.dm_tab_Options_Yes%></td>
                                        <td style="width:20px;text-align:center;"><%=Resources.CRMTREEResource.dm_tab_Options_No%></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right"><%=Resources.CRMTREEResource.dm_tab_Options_package%></td>
                                        <td style="text-align:center;"><input type="radio" name="SD_Serv_Package" value="1" checked="checked"/></td>
                                        <td style="text-align:center;"><input type="radio" name="SD_Serv_Package" value="0"/></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right"><%=Resources.CRMTREEResource.dm_tab_Options_selection%></td>
                                        <td style="text-align:center;"><input type="radio" name="SD_SA_Selection" value="1" checked="checked"/></td>
                                        <td style="text-align:center;"><input type="radio" name="SD_SA_Selection" value="0"/></td>
                                    </tr>
                                </table>
                            </div>
		                </div>
                </div>
                <div style="text-align:center;height:40px;overflow:hidden;padding-top:5px;">
                    <a id="btnSave" data-options="iconCls:'icon-save',width:80,disabled:true" onclick ="_service.save()" href="javascript:void(0)" class="easyui-linkbutton"><%=Resources.CRMTREEResource.ap_buttons_save%></a>
                </div>
            </div>

            <div title="<%=Resources.CRMTREEResource.contact%>" style="padding:20px;">
                <table id="frm_personal" class="form" border="0" cellpadding="3" cellspacing="2">
                    <tr>
                        <td class="text"><%=Resources.CRMTREEResource.name_cn%></td>
                        <td colspan="5">
                            <input id="AD_Name_EN" class="easyui-textbox" data-options="required:true,novalidate:true"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="text"><%=Resources.CRMTREEResource.name_en%></td>
                        <td colspan="5">
                            <input id="AD_Name_CN" class="easyui-textbox" data-options="required:true,novalidate:true"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="text"><%=Resources.CRMTREEResource.phone%></td>
                        <td colspan="5">
                            <input class="easyui-textbox" data-options="required:true,novalidate:true"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="text"><%=Resources.CRMTREEResource.complaints%></td>
                        <td colspan="5">
                            <input class="easyui-textbox" data-options="required:true,novalidate:true"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="text"><%=Resources.CRMTREEResource.fax%></td>
                        <td colspan="5">
                            <input class="easyui-textbox" data-options="required:true,novalidate:true"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="text"><%=Resources.CRMTREEResource.adress%></td>
                        <td colspan="5">
                            <input class="easyui-textbox" data-options="width:400,required:true,novalidate:true"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="text"><a href="javascript:void(0);" id="plupload_browse_button_s" style="text-decoration: none;"><%=Resources.CRMTREEResource.uploadLogo_s%></a></td>
                        <td>
                            <input id="AD_logo_file_S" type="hidden" value=""/>
                            <div id="plupload_container_s" style="position:relative;width:100%;height:64px;"></div>
                        </td>

                        <td class="text"><a href="javascript:void(0);" id="plupload_browse_button_m" style="text-decoration: none;"><%=Resources.CRMTREEResource.uploadLogo_m%></a></td>
                        <td>
                            <input id="AD_logo_file_M" type="hidden" value=""/>
                            <div id="plupload_container_m" style="position:relative;width:100%;height:64px;"></div>
                        </td>

                        <td class="text"><a href="javascript:void(0);" id="plupload_browse_button_l" style="text-decoration: none;"><%=Resources.CRMTREEResource.uploadLogo_l%></a></td>
                        <td>
                            <input id="AD_logo_file_L" type="hidden" value=""/>
                            <div id="plupload_container_l" style="position:relative;width:100%;height:64px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <a id="btnSave_Contact" data-options="iconCls:'icon-save',width:80,disabled:true" onclick ="_contact.save()" href="javascript:void(0)" class="easyui-linkbutton"><%=Resources.CRMTREEResource.ap_buttons_save%></a>
                        </td>
                    </tr>
                </table>
            </div>
		</div>
	</div>
    </div>

    <div id="tb_departments" style="padding:0px;">
        <div class="btns" style="margin-right:5px;"></div>
    </div>

    <div id="w_values" class="easyui-window" data-options="minimizable:false,closed:true,title:' ',modal:true" 
    style="width:500px;height:250px;">
        <div class="easyui-layout" data-options="fit:true">
            <div data-options="region:'center',border:false">
                <table id="dg_values"></table>
            </div>
            <div data-options="region:'south',border:false" style="height:38px;text-align:right;overflow:hidden;border-top: 1px solid #B1C242;padding-top:5px;padding-right:10px;">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',onClick:_values.accept" style="width:80px;"><%=Resources.CRMTREEResource.cm_cars_buttonos_accept%></a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_values.close" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.cm_cars_buttonos_ignore%></a>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Reports/DealershipManager.aspx';
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
                    , '</span></a>'
            ].join(''), o);
            a_btns.push(btn);
        });
        return a_btns;
    }

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

            var params = { action: 'Save_Service' };
            params.schedule = _schedule.get();
            params.resources = _resources.get();
            params.departments = _departments.get();

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
                SD_SA_Selection: $('input:radio[name="SD_SA_Selection"]:checked').val() == 1,
                SD_Serv_Package: $('input:radio[name="SD_Serv_Package"]:checked').val() == 1
            };

            //return;

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
                    field: 'PDV_Name', title: 'Name', width: 200
                },
                {
                    field: 'DV_Value', title: 'Value', width: 150
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


    //--------------------------------------------------------------------------------------
    //contact（联系信息）
    //--------------------------------------------------------------------------------------
    var _contact = {
        save: function () {

        }
    };
    //图片
    _contact.picture = {};
    //创建图片
    _contact.picture.create = function (target,fileName) {
        var $img = $('<div class="picItem car_img"><div class="remove" title="' + _contact.language.buttons.remove + '"></div><img src="/images/DealersLogo/' + fileName + '"/></div>');
        $(target).empty().append($img);
    };
    //初始化上传
    _contact.picture.plupload = function (options) {
        //图片上传
        var _uploader = $.plupload({
            multi_selection: false,
            container: options.container,
            browse_button: options.browse_button,
            params: { folderName: 'file_temp' },
            init: {
                FilesAdded: function (up, files) {
                    up.refresh();
                    //创建容器
                    for (var i = 0, len = files.length; i < len; i++) {
                        var file = files[i];
                        file._$container = $('<div class="picItem car_img _add"><div _id="' + file.id + '" class="remove" title="<%=Resources.CRMTREEResource.em_personal_buttons_remove%>"></div></div>');
                        file._$progress = $('<div class="progress"></div>');
                        file._$progress.appendTo(file._$container);
                        $(options.plupload_container).empty().append(file._$container);
                        file._$progress.progressbar({ width: '100%' });
                    }
                    if (files.length > 0) {
                        up.start();
                    }
                },
                //上传进度
                UploadProgress: function (up, file) {
                    file._$progress.progressbar('setValue', file.percent);
                },
                //上传完成
                FileUploaded: function (up, file) {
                    var fileNames = file.name.split('.');
                    var extendName = fileNames[fileNames.length - 1];
                    var fileName = file.id + '.' + extendName;
                    $(options.target).val(fileName);
                    var imgsrc = '/plupload/' + up.settings.params.folderName + '/' + fileName;
                    file._$progress.detach();
                    file._$container.append('<img src="' + imgsrc + '"/>');
                }
            }
        });

        $(".car_img>img", options.plupload_container).live({
            mouseover: function () {
                $(this).fadeTo('fast', 0.6);
            },
            mouseleave: function () {
                $(this).fadeTo('fast', 1);
            },
            //预览图片
            click: function () {
                var src = $(this).attr('src');
                var pre = $("#AD_Name_CN").textbox('getValue') + '(' + $("#AD_Name_EN").textbox('getValue') + ') - '
                var title = pre = '<%=Resources.CRMTREEResource.ed_personal_picture_title%>';
                $.topOpen({
                    title: title,
                    showMask: false,
                    url: "/templete/report/PhotoViewer.html?src=" + src,
                    width: 500,
                    height: 350
                });
            }
        });
        //删除图片
        $(".car_img>.remove", options.plupload_container).live({
            click: function () {
                var fileId = $.trim($(this).attr("_id"));
                if (fileId) {
                    if (fileId.indexOf('.') === -1) {
                        for (var i in _uploader.files) {
                            if (_uploader.files[i].id === fileId) {
                                _uploader.files.splice(i, 1);
                                break;
                            }
                        }
                    }
                }
                $(options.target).val("");
                $(this).parent().detach();
                return false;
            }
        });
    };

    $(function () {
        //初始化
        (function Init() {
            $(window).unload(function () {
                try {
                    $("#plupload_browse_button").empty();
                } catch (e) {

                }
            });
            
            _contact.picture.plupload({
                container: 'plupload_browse_button_s',
                browse_button: 'plupload_browse_button_s',
                target:'#AD_logo_file_S',
                plupload_container:'#plupload_container_s'
            });

            _contact.picture.plupload({
                container: 'plupload_browse_button_m',
                browse_button: 'plupload_browse_button_m',
                target:'#AD_logo_file_M',
                plupload_container:'#plupload_container_m'
            });

            _contact.picture.plupload({
                container: 'plupload_browse_button_l',
                browse_button: 'plupload_browse_button_l',
                target: '#AD_logo_file_L',
                plupload_container: '#plupload_container_l'
            });

            $.getWords([4105], function (data) {
                if (data) {
                    var DR_UType = $.map(data._4105, function (o) {
                        return { DR_UType: o.value, DR_UType_Text: o.text };
                    });
                    _resources.DR_UType = DR_UType;
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

                    BindService();

                    _values.create();
                }, "json");

                InitTabs();
            });
        })();

        //初始化面板页
        function InitTabs() {

        }

        function BindService() {
            var params = { action: 'Get_Service', DP_UType: 3 };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (!$.checkResponse(res)) { return; }

                _schedule.bindData(res.schedule);

                _resources.bindData(res.resources);

                _departments.bindData(res.departments);

                $("#btnSave").linkbutton('enable');
            }, "json");
        }
    });
</script>