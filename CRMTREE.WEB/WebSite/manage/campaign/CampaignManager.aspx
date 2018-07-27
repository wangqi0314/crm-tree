<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampaignManager.aspx.cs" Inherits="manage_campaign_CampaignManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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

        .red {
            color:red;
        }

        .tbl {
            width:100%;
            border-top:1px solid #ccc;
            border-left:1px solid #ccc;
        }
            .tbl tr th.th {
                font-weight:normal;
                text-align:right;
                padding-right:15px;
                font-size:14px;
                background-color:#E7EADB;
            }
            .tbl tr th.top {
                vertical-align:top;
            }
            .tbl tr .th,.tbl tr .td {
                padding:6px;
                border-right:1px solid #ccc;
                border-bottom:1px solid #ccc;
            }

        .fieldset
        {
            border:solid 1px #ccc;
            padding:5px 10px;
            float:left;
            margin-right:5px;
            display:none;
        }      

        .panel-header{ 
            background-color: #DDE2BE;
        }

        .move{
            position:absolute;
            top:-1000px;
            left:-1000px;
        }
        .hide {
            display:none;
        }
    </style>
</head>
<body>
    <div id="ex_campaign_title" class="easyui-panel" data-options="title:'<%= Resources.CRMTREEResource.CampaignEditor %>',fit:false" style="overflow:auto;padding:5px;margin-left:0px;">
    <table id="frm_campaign" class="tbl" cellpadding="0" cellspacing="0">
        <tr>
            <th class="th" style="width:150px;">
                <div style="width:150px;">
                   <span class="red">*</span><%= Resources.CRMTREESResource.CampaignTitle %>
                </div>
            </th>
            <td class="td">
                <input id="CG_Code" type="hidden" value="0" />
                <input id="CG_Title" class="easyui-textbox" data-options="required:true,novalidate:true" style="width:100%;height:32px"/>
            </td>
            <td class="td" style="width:80px;">
                <div style="width:60px;">
                    <span id="CG_Share" class="easyui-checkbox" data-options="text:'<%=Resources.CRMTREEResource.cmp_form_share %>'"></span>
                </div>
            </td>
            <th class="th" style="text-align:left;width:220px;padding:0px;">
                <div style="border-bottom:1px solid #ccc;padding-left:10px;padding-bottom:2px;">
                    <%= Resources.CRMTREEResource.cmp_Status %>
                </div>
                <div style="background-color:#FCFAB0;text-align:center;padding-top:2px;">
                    <span id="ctrl_status" style="font-size:16px;font-weight:bold;"></span>
                </div>
            </th>
        </tr>

        <tr>
            <th class="th top">
                <span class="red">*</span><%= Resources.CRMTREESResource.CampaignDescription %>
            </th>
            <td class="td">
                <input id="CG_Desc" class="easyui-textbox" data-options="multiline:true,required:true,novalidate:true" style="width:100%;height:200px"/>
            </td>
            <td class="td" style="width:300px;vertical-align:top;" colspan="2">
                <div class="easyui-layout" data-options="fit:true">
                    <div data-options="region:'center',title:'<%=Resources.CRMTREEResource.camp_Approval_Process %>'" style="padding:5px;overflow-y:scroll;">
                        <div id="ctrl_users" class="easyui-accordion" data-options="selected:null,onSelect:_approve.onSelect" ></div> 
                    </div>
                </div>
            </td>
        </tr>

        <tr>
            <th class="th">
                <span class="red">*</span><%= Resources.CRMTREESResource.CampaginCT %>
            </th>
            <td class="td" colspan="3">
                <input id="CG_RP_Code" class="easyui-combobox" data-options="required:true,novalidate:true,onChange:_RP_Code.onChange" style="width:60%;"/>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true,onClick:_RP_Code.edit"><%=Resources.CRMTREEResource.cmp_edit_parameter %></a>
            </td>
        </tr>

        <tr>
            <th class="th top">
                <span class="red">*</span><%=Resources.CRMTREESResource.EventContact %> <%= Resources.CRMTREESResource.CampaignMethod %>
            </th>
            <td class="td" colspan="3">
                <fieldset id="ex_fs_phone_calls" class="fieldset" style="min-width:220px">
                    <legend><span id="FS_Phone_Calls"></span></legend>
                    <div class="fieldset-body">
                        <span id="EX_CG_Method_Calls"></span>
                        <br />
                        <div id="EX_CG_Whom" style="display:none;"><span id="CG_Whom"></span></div>
                    </div>
                </fieldset>
                <fieldset class="fieldset">
                    <legend><span id="FS_Text_Type"></span></legend>
                    <div class="fieldset-body">
                        <span id="CG_Mess_Type"></span>
                        <br />
                        <span id="EX_CG_Method_Text"></span>
                    </div>
                </fieldset>
                <fieldset class="fieldset" style="margin-right:0px;">
                    <legend><span id="FS_Phone_Others"></span></legend>
                    <div class="fieldset-body">
                        <span id="EX_CG_Method_Others"></span>
                    </div>
                </fieldset>
            </td>
        </tr>

        <tr id="ex_tr_genre" class="">
            <th class="th">
                <span class="red">*</span><%= Resources.CRMTREESResource.EventGenre %>
            </th>
            <td class="td" colspan="3">
                <input id="CG_EG_Code" class="easyui-combobox" data-options="required:true,novalidate:true,onSelect:_campaign.select_CG_EG_Code"/>
                <span id="EX_Span_CG_EG_Code">
                    <input id="EX_CG_EG_Code" class="easyui-textbox"/>
                    <a class="easyui-linkbutton" id="btn_CG_EG_Code" data-options="plain:true,iconCls:'icon-add',onClick:_campaign.add_CG_EG_Code"><%=Resources.CRMTREEResource.cm_cars_buttons_add %></a>
                </span>
            </td>
        </tr>

        <tr id="ex_tr_rsvp" class="">
            <th class="th">
                <span class="red">*</span><%=Resources.CRMTREESResource.EventRSVP %>
            </th>
            <td class="td" colspan="3">
                <div style="float:left;padding:5px;border:1px solid #ccc;">
                    <span id="CG_RSVP" class="easyui-radiolist" data-options="data:[
                        { value: '1', text: '<%=Resources.CRMTREEResource.cmp_form_yes %>',selected:true }, 
                        { value: '0', text: '<%=Resources.CRMTREEResource.cmp_form_no %>' }
                    ]"></span>
                </div>
                <div style="float: left; margin-left: 10px;margin-top:10px;">
                    <%=Resources.CRMTREEResource.cmp_max_attendees %> <input id="CG_Max_Persons" class="easyui-numberbox"/>
                </div>
            </td>
        </tr>

        <tr id="ex_tr_response" class="">
            <th class="th">
                <span class="red">*</span><%=Resources.CRMTREESResource.EventPersonResponse %>
            </th>
            <td class="td" colspan="3">
                <input id="CG_Responsible" class="easyui-combobox" data-options="multiple:true,multiline:true,required:true,novalidate:true" style="width:60%;height:35px;"/>
            </td>
        </tr>

        <tr id="ex_tr_calendar" class="">
            <th class="th top">
                <span class="red">*</span><%=Resources.CRMTREEResource.cmp_form_calendar %>
            </th>
            <td class="td" colspan="3">
                <div>
                    <div>
                        <%=Resources.CRMTREEResource.cmp_event_start_date %> <input id="CG_Act_S_Dt" class="easyui-datebox" data-options="required:true,novalidate:true" style="width:100px;"/>
                        <%=Resources.CRMTREEResource.cmp_duration %> <input id="Ex_CG_Act_E_Dt" class="easyui-numberbox" data-options="required:true,novalidate:true" style="width:50px;"/> <%=Resources.CRMTREEResource.cmp_days %>
                    </div>
                    <div style="margin-top:10px;">
                        <%=Resources.CRMTREEResource.cmp_Start_Announcement %>  <input id="Ex_CG_Start_Dt" class="easyui-numberbox" data-options="required:true,novalidate:true" style="width:50px;"/> <%=Resources.CRMTREEResource.cmp_prior %> <input id="Ex_CG_End_Dt" class="easyui-numberbox" data-options="required:true,novalidate:true" style="width:50px;"/> <%=Resources.CRMTREEResource.cmp_days %>
                    </div>  
                </div>
            </td>
        </tr>

        <tr id="ex_tr_tools" class="">
            <th class="th">
                <%=Resources.CRMTREESResource.EventRecommendedools %>
            </th>
            <td class="td" colspan="3">
                <input id="CG_Tools" class="easyui-combobox" data-options="multiple:true,multiline:true" style="width:60%;height:35px;"/>
            </td>
        </tr>

        <tr id="ex_tr_budget" class="">
            <th class="th">
                <%=Resources.CRMTREESResource.EventBudget %>
            </th>
            <td class="td" colspan="3">
                <%=Resources.CRMTREEResource.cmp_budget_Total %> <input id="CG_Budget" class="easyui-numberbox" style="width:80px;"/><span style="width:15px; display:inline-block;"></span>
                <%=Resources.CRMTREEResource.cmp_budget_paid %> <input id="CG_OEMPay" class="easyui-numberbox" style="width:80px;"/>%<span style="width:15px; display:inline-block;"></span>
                <%=Resources.CRMTREEResource.cmp_budget_dealer_pays %> <input id="EX_Dealer_Pays" class="easyui-textbox" data-options="disabled:true" style="width:80px;"/><span style="width:15px; display:inline-block;"></span>
                <%=Resources.CRMTREEResource.cmp_budget_oem_pays %> <input id="EX_OEM_Pays" class="easyui-textbox" data-options="disabled:true" style="width:80px;"/>
            </td>
        </tr>

        <tr id="ex_tr_matrix">
            <th class="th top">
                <span class="red">*</span><%= Resources.CRMTREESResource.CampaginSM %>
            </th>
            <td class="td" colspan="3">
                <table id="CG_Succ_Matrix"></table>
            </td>
        </tr>

        <tr>
            <th class="th">
                <span class="red">*</span><%=Resources.CRMTREESResource.EventMessageFile %>
            </th>
            <td class="td" colspan="3"> 
                <input id="CG_Filename" type="hidden"/>
                <input id="CG_Filename_Temp" type="hidden"/>
                <div id="ctrol_p_upload" class="easyui-progressbar" style="width:100px;display:none;"></div> 
                <div id="ctrol_container_upload" class="move" style="float:left;margin-left:5px;">
                    Main Message
                    <div style="border:1px solid #ccc;margin-top:5px;padding:5px;">
                        <a class="easyui-linkbutton" id="ctrol_btn_upload" data-options="plain:true,iconCls:'icon-up'"><%=Resources.CRMTREEResource.btnUpload %></a>
                        <a class="easyui-linkbutton" id="ctrol_btn_create" data-options="plain:true,onClick:_campaign.file.create" style="display:none;"><%=Resources.CRMTREEResource.btnCreate %></a>
                    </div>
                </div>
                <%--<div style="float:left;margin-left:5px;">
                    Phone Message
                    <div style="border:1px solid #ccc;margin-top:5px;padding:5px;">
                        <a class="easyui-linkbutton" data-options="plain:true,onClick:_campaign.file.createPhoneMsg"><%=Resources.CRMTREEResource.btnCreate %></a>
                    </div>
                </div>--%>
                <div id="control_btn_group" class="icon-file" style="float:left;display:none;padding-left:20px;">
                    <a class="easyui-linkbutton" id="btnFileEdit" data-options="plain:true,iconCls:'icon-edit',onClick:_campaign.file.edit"><%=Resources.CRMTREEResource.btnEdit %></a>
                    <a class="easyui-linkbutton" id="btnFileView" data-options="plain:true,iconCls:'icon-search',onClick:_campaign.file.view"><%=Resources.CRMTREEResource.btnView %></a>
                    <a class="easyui-linkbutton" id="btnFileDelete" data-options="plain:true,iconCls:'icon-remove',onClick:_campaign.file.remove"><%=Resources.CRMTREEResource.btnDelete %></a>
                </div>
            </td>
        </tr>

        <tr>
            <td class="td" colspan="4" style="text-align:center;">
                <a class="easyui-linkbutton" id="btnSave" data-options="iconCls:'icon-save',onClick:_campaign.save,disabled:true" style="width:80px;"><%=Resources.CRMTREEResource.ap_buttons_save%></a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_campaign.cancel" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.CancelBtn%></a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-back',onClick:_campaign.back" style="margin-left:10px;"><%=Resources.CRMTREEResource.MyCarBacktoList%></a>
                <a class="easyui-linkbutton" id="btnRelease" data-options="onClick:_campaign.release,disabled:true" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.btnRelease %></a>
            </td>
        </tr>
    </table>
    </div>

    <div id="tb_Succ_Matrix" style="padding:0px;">
        <div class="btns" style="margin-right:5px;"></div>
    </div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Campaign/CampaignManager.aspx';
    var _s_url_report = '/handler/Reports/Reports.aspx';
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

    var _RP_Code = {
        _edit:function(rp_code){
            if (rp_code > 0) {
                $.topOpen({
                    title: '<%=Resources.CRMTREEResource.cmp_title_parameter %>'
                    , url: '/manage/campaign/ParamValueManager.aspx?PV_CG_Code=' + _params.CG_Code + '&RP_Code=' + rp_code + "&_winID=" + _campaign.guid
                    , width: 650
                    , height: 500
                });
            } else {
                $.msgTips.info("<%=Resources.CRMTREEResource.cmp_targeted_info %>");
            }
        },
        edit:function(){
            var rp_code = $("#CG_RP_Code").combobox('getValue');
            _RP_Code._edit(rp_code);
        },
        onChange: function (newValue, oldValue) {
            _campaign.paramValue = [];
            if (!(_params.CG_Code > 0 && oldValue == "") && newValue > 0) {
                _campaign.bParamValue = true;
                _RP_Code._edit(newValue);
            }
        }
    };

    //--------------------------------------------------------------------------------------
    //Approve（审批）
    //--------------------------------------------------------------------------------------
    var _approve = {
        onSelect: function (title, index) {
            if (!(_params.CG_Code > 0)) { return; }

            var panel = $("#ctrl_users").accordion('getPanel', index);
            var $content = $(">div", panel);
            var AU_Code = $content.attr("_AU_Code");
            if (!(AU_Code > 0)) { return; }

            $content.empty();
            var $dg = $("<div></div>");
            var $ta = $("<input/>");
            $content.append($dg);
            $dg.datagrid({
                width: "100%",
                height: 60,
                url: "",
                singleSelect: true,
                showHeader: false,
                columns: [[
                    { field: 'AA_Update_dt', title: 'Date', width: 130 },
                    {
                        field: 'AA_Type', title: 'Action', width: 100,
                        formatter: function (value, row, index) {
                            if (value == 0) {
                                return "<%=Resources.CRMTREEResource.btnRelease %>";
                            }else if (value == 200) {
                                return "<%=Resources.CRMTREEResource.btnReject %>";
                            }else if (value == 100 || value == 101) {
                                return "<%=Resources.CRMTREEResource.btnApprove %>";
                            } else {
                                return value;
                            }
                        }
                    }
                ]],
                onSelect: function (index, row) {
                    $ta.textbox('setValue', row.AAN_Notes);
                }
            });

            $content.append($ta);
            $ta.textbox({
                multiline: true,
                width: "100%",
                height: 50,
                readonly: true
            });

            $.post(_s_url_report, {
                o: JSON.stringify({
                    action: 'getReportData', RP_Code: 84,
                    queryParams: [
                        { EX_Name: "CG_Code", EX_Value: _params.CG_Code, EX_DataType: 'int' },
                        { EX_Name: "AU_Code", EX_Value: AU_Code, EX_DataType: 'int' }
                    ]
                })
            }, function (data) {
                if (!$.checkResponse(data)) {
                    data = [];
                }
                $dg.datagrid('loadData', data);
                if (data.length > 0) {
                    $dg.datagrid('selectRow', 0);
                }
            }, "json");
        }
    };

    //--------------------------------------------------------------------------------------
    //Campaign Success Matrix（活动成功衡量指标）
    //--------------------------------------------------------------------------------------
    var _Succ_Matrix = {
        //originalData:[],
        data: [],
        _id: '#CG_Succ_Matrix'
    };
    _Succ_Matrix.language = {
        buttons: {
            add: '<%=Resources.CRMTREEResource.cm_cars_buttons_add %>',
            remove: '<%=Resources.CRMTREEResource.cm_cars_buttons_remove %>'
        }
    };
    //取消编辑
    _Succ_Matrix.cancelEdit = function () {
        var $dg = $(_Succ_Matrix._id);
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            $dg.datagrid('cancelEdit', i);
        }
    }
    //结束编辑不验证
    _Succ_Matrix.endEditingNoValid = function () {
        var $dg = $(_Succ_Matrix._id);
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            var ed = $dg.datagrid('getEditor', { index: i, field: 'SMV_PSM_Code' });
            if (ed && ed.type === 'combobox') {
                var typeText = $(ed.target).combobox('getText');
                rows[i][ed.field + '_Text'] = typeText;
            }
            $dg.datagrid('endEdit', i);
        }
    }
    //结束编辑需要验证
    _Succ_Matrix.endEditing = function () {
        var $dg = $(_Succ_Matrix._id);
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
    _Succ_Matrix.onClickCell = function (index, field) {
        if (field !== 'btnLast') {
            _Succ_Matrix.endEditingNoValid();
            var $dg = $(this);
            var rowData = $dg.datagrid('selectRow', index).datagrid('getSelected');

            //不能修改类型
            //if (field === 'SMV_PSM_Code' && rowData.SMV_PSM_Code > 0) return;
            if (field === 'SMV_PSM_Code' && rowData.SMV_CG_Code > 0) return;

            $dg.datagrid('editCell', { index: index, field: field });

            var ed = $dg.datagrid('getEditor', { index: index, field: field });
            if (ed && ed.type === 'combobox') {
                var rows = $dg.datagrid('getRows');
                /*
                var newData = [];
                newData = $.map(_Succ_Matrix.data, function (o) {
                    var oo = o;
                    $.each(rows, function (i, r) {
                        if (r.SMV_PSM_Code == o.value) {
                            oo = null;
                            return false;
                        }
                    });
                    return oo;
                });
                */
                
                ed.target.combobox('loadData', _Succ_Matrix.data);
                ed.target.combobox('showPanel');
            }
            if (ed && ed.type === 'numberbox') {
                $c = ed.target;
                $c.numberbox('textbox').select();
            }
        }
    }
    //创建添加按钮
    _Succ_Matrix.createAdd = function () {
        var $btns = $("#tb_Succ_Matrix div.btns");
        var a_o = [
            { icon: 'icon-add', text: _Succ_Matrix.language.buttons.add, clickFun: '_Succ_Matrix.add(event,this);' }
        ];
        var a_btns = setBtns(a_o);
        $btns.html(a_btns.join(''));
    }
    //创建
    _Succ_Matrix.create = function () {
        var $dg = $(_Succ_Matrix._id);
        $dg.datagrid({
            url: null,
            title: '<%=Resources.CRMTREEResource.camp_matrix %>',
            collapsible:true,
            //collapsed:true,
            toolbar: '#tb_Succ_Matrix',
            height:160,
            rownumbers: true,
            singleSelect: true,
            //showHeader: false,
            border: true,
            nowrap: false,
            loadMsg: '',
            columns: [[
                {
                    field: 'SMV_PSM_Code', title: '<%=Resources.CRMTREEResource.cmp_SMV_PSM_Code %>', width: 300,
                    formatter: function (value, row) {
                        return row.SMV_PSM_Code_Text ? row.SMV_PSM_Code_Text : value;
                    },
                    editor: {
                        type: 'combobox', options: {
                            novalidate: true,
                            required: true,
                            data: _Succ_Matrix.data,
                            onSelect: function (record) {
                                var row = $dg.datagrid('getSelected');
                                row.PSM_Val_Type = record.PSM_Val_Type;
                                row.SMV_Days = "";
                                row.SMV_Val = "";
                                window.setTimeout(function () {
                                    _Succ_Matrix.endEditingNoValid();
                                    var rowIndex = $dg.datagrid('getRowIndex', row);
                                    _Succ_Matrix.onClickCell.call($dg, rowIndex, 'SMV_Days');
                                }, 0);
                            }
                        }
                    }
                },
                {
                    field: 'SMV_Days', title: '<%=Resources.CRMTREEResource.cmp_SMV_Days %>', width: 80, editor: {
                        type: 'numberbox'
                    }
                },
                {
                    field: 'PSM_Val_Type', title: '<%=Resources.CRMTREEResource.cmp_PSM_Val_Type %>', width: 120
                },
                {
                    field: 'SMV_Val', title: '<%=Resources.CRMTREEResource.cmp_SMV_Val %>', width: 80
                    , editor: {
                        type: 'numberbox'
                    }
                },
                {
                    field: 'btnLast', align: 'center', width: 35,
                    formatter: function (value, row, index) {
                        var a_o = [
                        { icon: 'icon-remove', title: _Succ_Matrix.language.buttons.remove, clickFun: '_Succ_Matrix.remove(event,this);' }
                        ];
                        return setBtns(a_o, true).join('');
                    }
                }
            ]],
            onClickCell: _Succ_Matrix.onClickCell
        });

        _Succ_Matrix.createAdd();
    };
    //添加
    _Succ_Matrix.add = function () {
        var $dg = $(_Succ_Matrix._id);
        _Succ_Matrix.endEditingNoValid();
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

        _Succ_Matrix.onClickCell.call($dg, rowIndex, 'SMV_PSM_Code');
    }
    //删除
    _Succ_Matrix.remove = function (e, target) {
        var $dg = $(_Succ_Matrix._id);
        var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
        var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
        if (rowData.SMV_CG_Code > 0) {
            $.confirmWindow.tempRemove(function () {
                $dg.datagrid('deleteRow', rowIndex);
            });
        } else {
            if (rowIndex >= 0) {
                $dg.datagrid('deleteRow', rowIndex);
            }
        }
        window.setTimeout(function () { _Succ_Matrix.endEditingNoValid(); }, 0);
        stopPropagation(e);
    };
    //获得
    _Succ_Matrix.get = function () {
        //_Succ_Matrix.endEditingNoValid();
        var $dg = $(_Succ_Matrix._id);
        var rows = $dg.datagrid('getRows');
        var deletedRows = $dg.datagrid('getChanges', 'deleted');
        return { changes: rows, deletes: deletedRows };
    };
    //绑定数据
    _Succ_Matrix.bindData = function (departments) {
        $(_Succ_Matrix._id).datagrid('loadData', departments ? departments : []);
    };
    //检查数据
    _Succ_Matrix.check = function () {
        _Succ_Matrix.endEditingNoValid();
        var $dg = $(_Succ_Matrix._id);
        var rows = $dg.datagrid('getRows');
        var bValid = true;
        for (var i = 0, len = rows.length; i < len; i++) {
            var row = rows[i];
            if ($.trim(row.SMV_PSM_Code) === '') {
                bValid = false;
                _Succ_Matrix.onClickCell.call($dg, i, 'SMV_PSM_Code');
                break;
            }
        }
        return bValid;
    }

    //--------------------------------------------------------------------------------------
    //Campaign（活动）
    //--------------------------------------------------------------------------------------
    var _campaign = {
        guid: '<%=Guid.NewGuid()%>',
        bParamValue:false,
        paramValue: [],
        report: "",
        select_CG_EG_Code:function(record){
            if (record) {
                if (record.value == "-1") {
                    $("#EX_Span_CG_EG_Code").show();
                } else {
                    $("#EX_Span_CG_EG_Code").hide();
                }
            }
        },
        add_CG_EG_Code: function () {
            $("#btn_CG_EG_Code").linkbutton('disable');
            $.post(_s_url, {
                o: JSON.stringify({
                    action: 'Add_CT_Event_Genre',
                    EG_Desc: $('#EX_CG_EG_Code').textbox('getValue')
                })
            }, function (res) {
                $("#btn_CG_EG_Code").linkbutton('enable');
                if ($.checkResponse(res, false)) {
                    var data = $("#CG_EG_Code").combobox('getData');
                    if (!data) { data = [];}
                    data.push(res);
                    $("#CG_EG_Code").combobox('loadData', data);
                    $('#EX_CG_EG_Code').textbox('clear');
                }
            }, "json");
        },
        updateTargeted: function () {
            if (_campaign.bParamValue && _campaign.paramValue.length > 0) {
                var text = "";
                var value = $("#CG_RP_Code").combobox("getValue");
                var data = $("#CG_RP_Code").combobox("getData");
                $.each(data, function (i, d) {
                    if (d.value == value) {
                        text = d.ex_text;
                    }
                });

                $.each(_campaign.paramValue, function (i,p) {
                    var o = {};
                    var val = "";
                    switch (p.PL_Type) {
                        case 10:
                            val = p.PV_Val_Text;
                            break;
                        case 11:
                            val = p.PV_Val_Text;
                            break;
                        case 12:
                            val = p.PV_Val_Text;
                            break;
                            //case 13:

                            //    break;
                            //case 14:

                            //    break;
                        default:
                            val = p.PV_Val;
                            break;
                    }
                    o[p.PL_Tag] = val;
                    text = $.formatParamValue(text, o);
                });
                
                if (text) {
                    $("#CG_RP_Code").combobox("setText", text);
                }
            }
        },
        ctrols: {
            textbox: ['CG_Title', 'CG_Desc'],//, 'CG_Filename'
            combobox: ['CG_RP_Code', 'CG_EG_Code', 'CG_Responsible'],
            datebox: ['CG_Act_S_Dt'],
            numberbox: ['Ex_CG_Act_E_Dt', 'Ex_CG_Start_Dt', 'Ex_CG_End_Dt']
        },
        //验证
        validate: function (o) {
            var bValid = $.form.validate(_campaign.ctrols);

            return bValid;
        },
        //取消验证
        disableValidation: function () {
            $.form.disableValidation(_campaign.ctrols);
        },
        _getCampaign:function(){
            _campaign.disableValidation();
            var o = $.form.getData("#frm_campaign", ["CG_RSVP"]);
            var bValidForm = _campaign.validate(o);
            var bValidSM = _Succ_Matrix.check();

            //特殊赋值
            o.CG_Type = _params.CT;
            var bValid = true;
            var bInPerson = false;
            o.CG_Method = [];
            if (o.EX_CG_Method_Calls) {
                $.each(o.EX_CG_Method_Calls, function (i, v) {
                    if (v == 1) {
                        bInPerson = true;
                        return false;
                    }
                });
                o.CG_Method.push(o.EX_CG_Method_Calls);
            }
            if (o.EX_CG_Method_Text) {
                o.CG_Method.push(o.EX_CG_Method_Text);
            }
            if (o.EX_CG_Method_Others) {
                o.CG_Method.push(o.EX_CG_Method_Others);
            }
            o.CG_Method = o.CG_Method.join(',');

            var msg = [];
            if (bInPerson) {
                if ($.trim(o.CG_Whom) == '') {
                    msg[msg.length] = '<%=Resources.CRMTREESResource.EventContact %> <%= Resources.CRMTREESResource.CampaignMethod %>>' + $("#FS_Phone_Calls").text() + '<%= Resources.CRMTREEResource.required %>';
                    bValid = false;
                }
            } else {
                o.CG_Whom = "";
            }

            if ($.trim(o.CG_Mess_Type) == '') {
                msg[msg.length] = '<%=Resources.CRMTREESResource.EventContact %> <%= Resources.CRMTREESResource.CampaignMethod %>>' + $("#FS_Text_Type").text() + '<%= Resources.CRMTREEResource.required %>';
                bValid = false;
            }

            //Success Matrix
            var sm_values = _Succ_Matrix.get();
            var b_sm_values = true;
            $.each(_campaign.auth.fields.CG_Succ_Matrix, function (i, ct) {
                if (_params.CT == ct) {
                    b_sm_values = false;
                    return false;
                }
            });
            if (b_sm_values && sm_values && sm_values.changes.length == 0) {
                msg[msg.length] = '<%= Resources.CRMTREESResource.CampaginSM %><%= Resources.CRMTREEResource.required %>';
                bValid = false;
            }

            if ($.trim(o.CG_Method) == '') {
                msg[msg.length] = '<%=Resources.CRMTREESResource.EventContact %> <%= Resources.CRMTREESResource.CampaignMethod %><%= Resources.CRMTREEResource.required %>';
                bValid = false;
            }

            if ($.trim(o.CG_Filename) == '') {
                msg[msg.length] = '<%= Resources.CRMTREESResource.EventMessageFile %><%= Resources.CRMTREEResource.required %>';
                bValid = false;
            }

            if (msg.length > 0) {
                $.msgTips.info(msg.join('<br/>'));
            }

            if (!bValidForm || !bValidSM || !bValid) {
                return false;
            }

            if (_params.T == 1) {
                o.CG_Code = 0;
            }

            if (_params.T >= 0) {
                o.EX_T = _params.T;
            }

            return {
                action: 'Save_Campaign',
                campaign: o,
                sm_values: sm_values,
                bParamValue: _campaign.bParamValue,
                param_value: _campaign.paramValue
            };
        },
        //保存
        save: function () {
            var o = _campaign._getCampaign();
            if (!o) {
                return;
            }

            //!(o.CG_Code > 0) && 
            if (_params.CA > 0) {
                o.CG_Cat = _params.CA;
            }

            $.mask.show();
            $("#btnSave,#btnRelease").linkbutton('disable');
            $.post(_s_url, { o: JSON.stringify(o) }, function (res) {
                $.mask.hide();
                $("#btnSave").linkbutton('enable');
                if ($.checkResponse(res)) {
                    if (_params._cmd == 'cp') {
                        $.msgTips.save(true);
                        _campaign.backToList();
                    } else {
                        _campaign.close(true);
                    }
                    
                } else {
                    $.msgTips.save(false);
                }
            }, "json");
        },
        //返回到列表
        backToList: function () {
            window.top.location = "/manage/campaign/Campaign_List.aspx?CT=" + _params.CT;
        },
        //返回
        back: function () {
            $.confirmWindow.backToList(function () {
                _campaign.backToList();
            });
        },
        //关闭
        close: function (bSave) {
            if (window._closeOwnerWindow) {
                window._closeOwnerWindow();
                if (bSave) {
                    top[_params._winID]._datagrid.action.refresh();
                }
            } else {
                window.close();
            }
        },
        //取消
        cancel: function () {
            $.confirmWindow.cancel(function () {
                window.location.reload();
            });
        },
        //发布 
        release: function () {
            var o = _campaign._getCampaign();
            if (!o) {
                return;
            }
            o.campaign.CG_Status = 5;
            $.confirmWindow.release(function () {
                $.post(_s_url, { o: JSON.stringify(o) }, function (data) {
                    if ($.checkResponse(data)) {
                        $.msgTips.release(true);
                        _campaign.backToList();
                    } else {
                        $.msgTips.release(false);
                    }
                }, "json");
            });
        },
        Get_Reports: function () {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Reports', CG_Type: _params.CT }) }, function (data) {
                if (!$.checkResponse(data)) {
                    data = [];
                }

                //if (data.length > 0) {
                //    data[0].selected = true;
                //}

                $("#CG_RP_Code").combobox('loadData', data);

                _campaign.Get_Event_Genre();
            }, "json");
        },
        Get_Event_Genre: function () {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Event_Genre' }) }, function (data) {
                if (!$.checkResponse(data)) {
                    data = [];
                }

                //if (data.length > 0) {
                //    data[0].selected = true;
                //}

                data.push({ value: '-1', text: (_isEn ? "Others" : "其它") });

                $("#CG_EG_Code").combobox('loadData', data);

                _campaign.getWordByValue();
            }, "json");
        },
        getWordByValue:function(){
            var ct = _params.CT;

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

            $.getWordByValue(pid, ct, function (o) {
                if(o){
                    $("#ex_campaign_title").panel({ title: "<%= Resources.CRMTREEResource.CampaignEditor %> " + '<span style="background-color:#FCFAB0;font-size:16px;font-weight:bold;padding:3px 20px 3px 20px;">' + $.trim(o.text) + '</span>' });
                }
                _campaign.getApprovalList();
            });
        },
        getApprovalList: function () {
            $.post(_s_url, {
                o: JSON.stringify({
                    action: 'GetApprovalList',
                    CT: _params.CT
                })
            }, function (rows) {
                if (!$.checkResponse(rows)) {
                    rows = [];
                }

                for (var i = 0, len = rows.length; i < len; i++) {
                    var dataRow = rows[i];
                    $.each(dataRow, function (n, d) {
                        if (d === null) {
                            dataRow[n] = "";
                        }
                    });

                    $('#ctrl_users').accordion('add', {
                        title: dataRow.User_Name,
                        content: '<div style="width:100%;height:110px" _AU_Code = "' + dataRow.User_Code + '"></div>',
                        selected: false
                    });
                }

                _campaign.getWordsWithParent();
            }, "json");
        },
        getWordsWithParent: function () {
        /*
        *   4056 Phone Calls
        *   4059 Text Type
        *   4064 Text Carrier
        *   4068 Others
        *   4072 Calling Options
        *   4076 Events Tools
        *   4084 Events Person
        */
            $.getWordsWithParent([4056, 4059, 4064, 4068, 4072, 4076, 4084], function (d) {
                if (d) {
                    if (d.__4056) {
                        $("#FS_Phone_Calls").text($.trim(d.__4056.text));
                    }
                    if (d.__4059) {
                        $("#FS_Text_Type").text($.trim(d.__4059.text));
                    }
                    if (d.__4068) {
                        $("#FS_Phone_Others").text($.trim(d.__4068.text));
                    }
                    if (d._4072) {
                        $("#CG_Whom").radiolist({ data: d._4072 });
                    }
                    if (d._4056) {
                        $.each(d._4056, function (i, o) {
                            if (o.id == 4057) {
                                o.valuechanged = function (checked) {
                                    if (checked) {
                                        $("#EX_CG_Whom").show('fast');
                                    } else {
                                        $("#EX_CG_Whom").hide('fast');
                                    }
                                };
                                if (o.selected) {
                                    $("#EX_CG_Whom").show();
                                } else {
                                    $("#EX_CG_Whom").hide();
                                }
                                return false;
                            }
                        });
                        $("#EX_CG_Method_Calls").checkboxlist({ data: d._4056 });
                    }
                    if (d._4059) {
                        $("#CG_Mess_Type").radiolist({ data: d._4059 });
                    }
                    if (d._4064) {
                        $("#EX_CG_Method_Text").checkboxlist({ data: d._4064 });
                    }

                    if (d._4068) {
                        $("#EX_CG_Method_Others").checkboxlist({ data: d._4068 });
                    }

                    if (d._4084) {
                        $("#CG_Responsible").combobox('loadData', d._4084);
                    }
                    if (d._4076) {
                        $("#CG_Tools").combobox('loadData', d._4076);
                    }

                    $("fieldset.fieldset").each(function () {
                        if (!$(this).hasClass('hide')) {
                            $(this).show();
                        }
                    })
                }
                _campaign.Get_Succ_Matrix();
            });
        },
        Get_Succ_Matrix: function () {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Succ_Matrix' }) }, function (data) {
                if (data) {
                    //_Succ_Matrix.originalData = data;
                    //_Succ_Matrix.data = $.map(data, function (a) { return a; });

                    _Succ_Matrix.data = data;
                }

                _Succ_Matrix.create();

                _campaign.bindDefaults();

                _campaign.bind();
            }, "json");
        },
        calc_budget: function (budget, paid) {
            var dealer_pays = '', oem_pays = '';
            if (budget > 0 && paid > 0) {
                dealer_pays = (paid / 100) * budget;
                oem_pays = ((100 - paid) / 100) * budget;
            }
            $("#EX_Dealer_Pays").textbox('setValue', dealer_pays);
            $("#EX_OEM_Pays").textbox('setValue', oem_pays);
        },
        init: function () {
            window.top[_campaign.guid] = window.self;

            $("#EX_Span_CG_EG_Code").hide();

            _campaign.auth.state();

            _campaign.file.fileUpload();

            _campaign.Get_Reports();

            $("#CG_Budget").numberbox({
                onChange: function (newValue, oldValue) {
                    var paid = $("#CG_OEMPay").numberbox('getValue');
                    _campaign.calc_budget(newValue, paid);
                }
            });

            $("#CG_OEMPay").numberbox({
                onChange: function (newValue, oldValue) {
                    var budget = $("#CG_Budget").numberbox('getValue');
                    _campaign.calc_budget(budget, newValue);
                }
            });
        },
        bind: function () {
            if (_params.CG_Code > 0) {
                var s_params = JSON.stringify({ action: 'Get_Campaign', CG_Code: _params.CG_Code });
                $.post(_s_url, { o: s_params }, function (res) {
                    if ($.checkResponse(res) && res.campaign) {
                        var campaign = res.campaign;
                        campaign.CG_Filename_Temp = campaign.CG_Filename;
                        campaign.CG_RSVP = campaign.CG_RSVP ? 1 : 0;
                        campaign.EX_CG_Method_Calls = campaign.CG_Method;
                        campaign.EX_CG_Method_Text = campaign.CG_Method;
                        campaign.EX_CG_Method_Others = campaign.CG_Method;
                        _campaign.file._fullPath = '/plupload/file/' + campaign.CG_Filename;
                        $.form.setData("#frm_campaign", campaign);
                        if (campaign.CG_Budget > 0 && campaign.CG_OEMPay > 0) {
                            _campaign.calc_budget(campaign.CG_Budget, campaign.CG_OEMPay);
                        }

                        _Succ_Matrix.bindData(res.sm_values);

                        $("#ctrl_status").text($.trim(campaign.EX_CG_Status));
                        if ($.trim(campaign.CG_Filename) == "") {
                            $("#ctrol_container_upload").removeClass("move");
                            $("#ctrol_btn_create").show();
                        } else {
                            $("#control_btn_group").show();
                        }
                        $("#btnSave,#btnRelease").linkbutton('enable');
                    }
                }, "json");
            } else {
                $.getWordByValue(4044,0, function (o) {
                    if (o) {
                        $("#ctrl_status").text($.trim(o.text));
                    }
                });

                $("#ctrol_container_upload").removeClass("move");
                $("#ctrol_btn_create").show();
                $("#btnSave").linkbutton('enable');
            }
        },
        //绑定默认值
        bindDefaults: function () {
            //Date & Time
            var date = _isEn ? '<%=DateTime.Now.AddDays(1).ToString("MM/dd/yyyy")%>' : '<%=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")%>';
            $("#CG_Act_S_Dt").datebox('setValue', date);

            //_Succ_Matrix.add();
        },
        file: {
            _progress: $("#ctrol_p_upload"),
            _fullPath: '',

            createPhoneMsg: function (params) {
                params = params ? $.trim(params) : "";
                if ($.trim(_params.T) != "") {
                    params += "&T=" + _params.T;
                }
                $.topOpen({
                    width: 700,
                    height: 500,
                    url: '/manage/campaign/Campaign_Message.aspx?_winID=' + _campaign.guid + params
                });
            },
            _create: function (params) {
                params = params ? $.trim(params) : "";
                if ($.trim(_params.T) != "") {
                    params += "&T=" + _params.T;
                }
                $.topOpen({
                    width: 900,
                    height: 650,
                    url: '/manage/campaign/CampaignFile.aspx?_winID=' + _campaign.guid + params
                });
            },
            create:function(){
                _campaign.file._create();
            },
            view: function () {
                var width = 800;
                var height = 500;
                var left = parseInt((screen.availWidth / 2) - (width / 2));
                var top = parseInt((screen.availHeight / 2) - (height / 2));
                var features = ",width=" + width + ",height=" + height + ",left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
                var rdm = Math.random();
                var myWindow = window.open(_campaign.file._fullPath+"?r="+rdm, "fileView", 'status=no,location=no,resizable,toolbar=no,scrollbars' + features, "_blank");
                if (myWindow) myWindow.focus();

            },
            edit: function () {
                var fileName = _campaign.file._fullPath;
                var params = "";
                if (fileName) {
                    params += "&filename=" + fileName;
                }
                _campaign.file._create(params);
            },
            remove: function () {
                $("#CG_Filename").val("")
                $("#control_btn_group").hide();
                $("#ctrol_container_upload").removeClass("move");
                $("#ctrol_btn_create").show();
            },
            change: function (fileName) {
                if (fileName) {
                    $("#CG_Filename").val(fileName);
                    var fullPath = '/plupload/file_temp/' + fileName;
                    _campaign.file._fullPath = fullPath;
                    _campaign.file._progress.hide();
                    $("#ctrol_container_upload").addClass("move");
                    $("#control_btn_group").show();
                }
            },
            fileUpload: function () {
                var _uploader = $.plupload({
                    filters: [
                    { title: "Html files(*.htm,*.html)", extensions: "htm,html" }
                    ],
                    multi_selection: false,
                    container: 'ctrol_btn_upload',
                    browse_button: 'ctrol_btn_upload',
                    params: { filter: "htm,html" },
                    init: {
                        FilesAdded: function (up, files) {
                            up.refresh();
                            if (files.length > 0) {
                                $("#ctrol_container_upload").addClass("move");
                                $("#ctrol_btn_create").hide();
                                _campaign.file._progress.show().progressbar({ value: 0 });
                                up.start();
                            }
                        },
                        //上传进度
                        UploadProgress: function (up, file) {
                            _campaign.file._progress.progressbar('setValue', file.percent);
                        },
                        //上传完成
                        FileUploaded: function (up, file) {
                            var fileNames = file.name.split('.');
                            var extendName = fileNames[fileNames.length - 1];
                            var fileName = file.id + '.' + extendName;
                            _campaign.file.change(fileName);
                        }
                    }
                });
            }
        },
        auth: {
            fields: {
                CG_EG_Code: [40, 90],
                CG_Responsible: [40, 90],
                CG_Act_S_Dt: [1, 2, 3, 40, 51, 52, 53, 90],
                Ex_CG_Act_E_Dt: [1, 2, 3, 40, 51, 52, 53, 90],
                Ex_CG_Start_Dt: [1, 2, 3, 40, 51, 52, 53, 90],
                Ex_CG_End_Dt: [1, 2, 3, 40, 51, 52, 53, 90],
                CG_Succ_Matrix:[35,36,85,86]
            },
            controls: {
                ex_tr_budget: [1, 2, 3, 40, 51, 52, 53, 90]
                , ex_tr_tools: [40, 90]
                , ex_tr_calendar: [1, 2, 3, 40, 51, 52, 53, 90]
                , ex_tr_rsvp: [40, 90]
                , ex_tr_genre: [40, 90]
                , ex_tr_response: [40, 90]
                , ex_fs_phone_calls: [1, 2, 3, 20, 30, 40, 51, 52, 53, 70, 80, 90]
                , ex_tr_matrix: [1, 2, 3, 20, 30, 40, 51, 52, 53, 70, 80, 90]
            },
            state: function () {
                var cg_type = _params.CT;

                $.each(_campaign.ctrols, function (i,cs) {
                    for (var j = 0; j < cs.length; j++) {
                        var c = cs[j];
                        var auths = _campaign.auth.fields[c];
                        if (auths && auths.length > 0) {
                            var b = false;
                            $.each(auths, function (k, auth) {
                                if (auth == cg_type) {
                                    b = true;
                                    return false;
                                }
                            });
                            if (!b) {
                                cs.splice(j, 1);
                                j--;
                            }
                        }
                    }
                });

                //$("#ex_tr_budget,#ex_tr_tools,#ex_tr_calendar,#ex_tr_rsvp,#ex_tr_genre,#ex_tr_response,#ex_fs_phone_calls").hide();
                
                $.each(_campaign.auth.controls, function (n, d) {
                    var b = false;
                    $.each(d, function (i, ct) {
                        if (ct == cg_type) {
                            b = true;
                            return false;
                        }
                    });
                    if (!b) {
                        $("#" + n).addClass('hide');
                    }
                });
            }
        }
    };
   
    $(function () {
        $(window).unload(function () {
            try {
                $("#ctrol_container_upload").empty();
            } catch (e) {

            }
        });

        if (_params.CG_Code > 0) {
            var s_params = JSON.stringify({ action: 'Get_CT', CG_Code: _params.CG_Code });
            $.post(_s_url, { o: s_params }, function (res) {
                if ($.checkResponse(res)) {
                    _params.CT = res.CT;
                }
                _campaign.init();
            }, "json");
        } else {
            _campaign.init();
        }
    });
</script>