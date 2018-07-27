<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampaignManagerN.aspx.cs" Inherits="manage_campaign_CampaignManagerN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />

    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>

    <script src="/scripts/plupload-1.5.7/plupload.js" type="text/javascript"></script>
    <script src="/scripts/plupload-1.5.7/plupload.flash.js" type="text/javascript"></script>
    <script src="/scripts/plupload-1.5.7/jquery.plupload.queue/jquery.plupload.queue.js" type="text/javascript"></script>

    <script src="/scripts/common/json2.js" type="text/javascript"></script>

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
            font-size: 12px;
        }

        .red {
            color: red;
        }

        .tbl {
            width: 100%;
            border-top: 1px solid #ccc;
            border-left: 1px solid #ccc;
        }

            .tbl tr th.th {
                font-weight: normal;
                text-align: right;
                padding-right: 15px;
                font-size: 14px;
                background-color: #E7EADB;
            }

            .tbl tr th.top {
                vertical-align: top;
            }

            .tbl tr .th, .tbl tr .td {
                padding: 6px;
                border-right: 1px solid #ccc;
                border-bottom: 1px solid #ccc;
            }

        .fieldset {
            border: solid 1px #ccc;
            padding: 5px 10px;
            float: left;
            margin-right: 5px;
            display: none;
        }

        .panel-header {
            background-color: #DDE2BE;
        }

        .move {
            position: absolute;
            top: -1000px;
            left: -1000px;
        }

        .hide {
            display: none;
        }

        .tbl tr th.fill {
            background-color: #FCFAB0;
            font-size: 16px;
            font-weight: bold;
            margin-right: 20px;
            padding: 3px 20px 3px 20px;
            text-align: left;
        }
    </style>
</head>
<body style="width: 100%">
    <div class="easyui-panel" data-options="fit:false" style="overflow: hidden; padding: 0px; margin-left: 0px;">
        <table id="frm_campaign" class="tbl" cellpadding="0" cellspacing="0">
            <tr>
                <th class="th" style="width: 100px;">
                    <div style="width: 100px;">
                        <%= Resources.CRMTREEResource.CampaignEditor %>
                    </div>
                </th>
                <th class="th fill" style="width: 150px;">
                    <div style="width: 150px;">
                        <span id="c_campaign_editor"></span>
                    </div>
                </th>
                <th class="th" style="width: 100px;">
                    <div style="width: 100px;">
                        <%= Resources.CRMTREEResource.cmp_Status %>
                    </div>
                </th>
                <th class="th fill" style="width: 100%;">
                    <span id="ctrl_status"></span>
                </th>
                <td class="td" style="width: 120px;">
                    <div style="width: 120px;">
                        <span id="CG_Share" class="easyui-checkbox" data-options="text:'<%=Resources.CRMTREEResource.cmp_form_share %>'"></span>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="padding: 5px; width: 100%; height: 100%;">
                    <table class="tbl" cellpadding="0" cellspacing="0">
                        <tr>
                            <th class="th" style="width: 150px;">
                                <div style="width: 150px;">
                                    <span class="red">*</span><%= Resources.CRMTREESResource.CampaignTitle %>
                                </div>
                            </th>
                            <td class="td">
                                <input id="CG_Code" type="hidden" value="0" />
                                <input id="CG_Title" class="easyui-textbox" data-options="required:true,novalidate:true" style="width: 100%; height: 32px" />
                            </td>
                        </tr>
                        <tr>
                            <th class="th top">
                                <span class="red">*</span><%= Resources.CRMTREESResource.CampaignDescription %>
                            </th>
                            <td class="td" colspan="3">
                                <input id="CG_Desc" class="easyui-textbox" data-options="multiline:true,required:true,novalidate:true" style="width: 100%; height: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <th class="th">
                                <span class="red">*</span><%= Resources.CRMTREESResource.CampaginCT %>
                            </th>
                            <td class="td" colspan="3">
                                <div style="display: inline;">
                                    <input id="CG_RP_Code"
                                        style="width: 420px"
                                        class="easyui-combobox"
                                        data-options="required:true,novalidate:true,onChange:_RP_Code.onChange" />
                                    <a id="editRP_Code" class="easyui-linkbutton"
                                        data-options="iconCls:'icon-edit',plain:true,onClick:_RP_Code.edit">
                                        <%=Resources.CRMTREEResource.cmp_edit_parameter %></a>
                                </div>
                                <div id="VIN_file_Up" style="display: inline; width: 500px">
                                    <input id="VIN_File_hid" type="hidden" />
                                    <a class="easyui-linkbutton" id="VIN_btn_upload"
                                        data-options="plain:true,iconCls:'icon-up'">
                                        <%=Resources.CRMTREEResource.VINUpload %></a>
                                    <a class="easyui-linkbutton" id="VIN_download_file"
                                        data-options="plain:true,iconCls:'icon-down',
                                           onClick:_campaign.file.Check_VIN_download">
                                        <%=Resources.CRMTREEResource.VINDownload %></a>
                                    <a class="easyui-linkbutton" id="VIN_Delete_file"
                                        data-options="plain:true,iconCls:'icon-remove',
                                        onClick:_campaign.file.Delete_VIN">
                                        <%=Resources.CRMTREEResource.btnRemove %></a>
                                </div>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <th class="th top">
                                <span class="red">*</span><%=Resources.CRMTREEResource.ContactMethods %>
                            </th>
                            <td class="td" colspan="3">
                                <fieldset id="ex_fs_phone_calls" class="fieldset" style="min-width: 220px">
                                    <legend><span id="FS_Phone_Calls"></span></legend>
                                    <div class="fieldset-body">
                                        <span id="EX_CG_Method_Calls"></span>
                                        <br />
                                        <div id="EX_CG_Whom" style="display: none;"><span id="CG_Whom"></span></div>
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
                                <fieldset class="fieldset" style="margin-right: 0px;">
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
                                <input id="CG_EG_Code" class="easyui-combobox" data-options="required:true,novalidate:true,onSelect:_campaign.select_CG_EG_Code" />
                                <span id="EX_Span_CG_EG_Code">
                                    <input id="EX_CG_EG_Code" class="easyui-textbox" />
                                    <a class="easyui-linkbutton" id="btn_CG_EG_Code" data-options="plain:true,iconCls:'icon-add',onClick:_campaign.add_CG_EG_Code"><%=Resources.CRMTREEResource.cm_cars_buttons_add %></a>
                                </span>
                            </td>
                        </tr>
                        <tr id="ex_tr_rsvp" class="">
                            <th class="th">
                                <span class="red">*</span><%=Resources.CRMTREESResource.EventRSVP %>
                            </th>
                            <td class="td" colspan="3">
                                <div style="float: left; padding: 5px; border: 1px solid #ccc;">
                                    <span id="CG_RSVP" class="easyui-radiolist" data-options="data:[
                                    { value: '1', text: '<%=Resources.CRMTREEResource.cmp_form_yes %>',selected:true }, 
                                    { value: '0', text: '<%=Resources.CRMTREEResource.cmp_form_no %>' }
                                ]"></span>
                                </div>
                                <div style="float: left; margin-left: 10px; margin-top: 10px;">
                                    <%=Resources.CRMTREEResource.cmp_max_attendees %>
                                    <input id="CG_Max_Persons" class="easyui-numberbox" />
                                </div>
                            </td>
                        </tr>
                        <tr id="ex_tr_response" class="">
                            <th class="th">
                                <span class="red">*</span><%=Resources.CRMTREESResource.EventPersonResponse %>
                            </th>
                            <td class="td" colspan="3">
                                <input id="CG_Responsible" class="easyui-combobox" data-options="multiple:true,multiline:true,required:true,novalidate:true" style="width: 60%; height: 35px;" />
                            </td>
                        </tr>
                        <tr id="ex_tr_calendar" class="">
                            <th class="th top">
                                <span class="red">*</span><%=Resources.CRMTREEResource.cmp_form_calendar %>
                            </th>
                            <td class="td" colspan="3">
                                <div>
                                    <%=Resources.CRMTREEResource.cmp_event_start_date %>
                                    <input id="CG_Act_S_Dt" class="easyui-datebox" data-options="required:true,novalidate:true" style="width: 100px;" />
                                    <%=Resources.CRMTREEResource.cmp_duration %>
                                    <input id="Ex_CG_Act_E_Dt" class="easyui-numberbox" data-options="required:true,novalidate:true" style="width: 50px;" />
                                    <%=Resources.CRMTREEResource.cmp_days %>
                                    <%=Resources.CRMTREEResource.cmp_Start_Announcement %>
                                    <input id="Ex_CG_Start_Dt" class="easyui-numberbox" data-options="required:true,novalidate:true" style="width: 50px;" />
                                    <%=Resources.CRMTREEResource.cmp_prior %>
                                    <input id="Ex_CG_End_Dt" class="easyui-numberbox" data-options="required:true,novalidate:true" style="width: 50px;" />
                                    <%=Resources.CRMTREEResource.cmp_days %>
                                </div>
                            </td>
                        </tr>
                        <tr id="ex_tr_tools" class="">
                            <th class="th">
                                <%=Resources.CRMTREESResource.EventRecommendedools %>
                            </th>
                            <td class="td" colspan="3">
                                <input id="CG_Tools" class="easyui-combobox" data-options="multiple:true,multiline:true" style="width: 60%; height: 35px;" />
                            </td>
                        </tr>
                        <tr id="ex_tr_budget" class="">
                            <th class="th">
                                <%=Resources.CRMTREESResource.EventBudget %>
                            </th>
                            <td class="td" colspan="3">
                                <%=Resources.CRMTREEResource.cmp_budget_Total %>
                                <input id="CG_Budget" class="easyui-numberbox" style="width: 80px;" /><span style="width: 15px; display: inline-block;"></span>
                                <%=Resources.CRMTREEResource.cmp_budget_paid %>
                                <input id="CG_OEMPay" class="easyui-numberbox" style="width: 80px;" />%<span style="width: 15px; display: inline-block;"></span>
                                <%=Resources.CRMTREEResource.cmp_budget_dealer_pays %>
                                <input id="EX_Dealer_Pays" class="easyui-textbox" data-options="disabled:true" style="width: 80px;" /><span style="width: 15px; display: inline-block;"></span>
                                <%=Resources.CRMTREEResource.cmp_budget_oem_pays %>
                                <input id="EX_OEM_Pays" class="easyui-textbox" data-options="disabled:true" style="width: 80px;" />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <th class="th">
                                <span class="red">*</span><%=Resources.CRMTREESResource.EventMessageFile %>
                            </th>
                            <td class="td" colspan="3">
                                <input id="CG_Filename" type="hidden" />
                                <input id="CG_Filename_Temp" type="hidden" />
                                <div id="ctrol_p_upload" class="easyui-progressbar" style="width: 100px; display: none;"></div>
                                <div id="ctrol_container_upload" class="move" style="float: left; margin-left: 5px;">
                                    <a class="easyui-linkbutton" id="ctrol_btn_upload" data-options="plain:true,iconCls:'icon-up'"><%=Resources.CRMTREEResource.btnUpload %></a>
                                    <a class="easyui-linkbutton" id="ctrol_btn_create" data-options="plain:true,onClick:_campaign.file.create" style="display: none;"><%=Resources.CRMTREEResource.btnCreate %></a>
                                </div>
                                <div id="control_btn_group" class="icon-file" style="float: left; display: none; padding-left: 20px;">
                                    <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit',onClick:_campaign.file.edit"><%=Resources.CRMTREEResource.btnEdit %></a>
                                    <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search',onClick:_campaign.file.view"><%=Resources.CRMTREEResource.btnView %></a>
                                    <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove',onClick:_campaign.file.remove"><%=Resources.CRMTREEResource.btnDelete %></a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td" colspan="3">
                                <table id="EX_Camp_Methods"></table>
                            </td>
                        </tr>
                        <tr id="ex_tr_matrix">
                            <td class="td" colspan="3">
                                <table id="CG_Succ_Matrix"></table>
                            </td>
                        </tr>
                        <tr>
                            <td class="td" colspan="4" style="vertical-align: top;">
                                <div class="easyui-panel" data-options="collapsible:true,collapsed:true,title:'<%=Resources.CRMTREEResource.camp_Approval_Process %>'" style="height: 200px; width: 100%; padding: 5px; overflow-y: scroll;">
                                    <div id="ctrl_users" class="easyui-accordion" data-options="selected:null" style="width: 70%; padding: 5px;"></div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td" colspan="4" style="text-align: center;">
                                <span id="ex_btn_Edit">
                                    <a class="easyui-linkbutton" id="btnSave" data-options="iconCls:'icon-save',onClick:_campaign.save,disabled:true" style="width: 80px;"><%=Resources.CRMTREEResource.ap_buttons_save%></a>
                                    <a class="easyui-linkbutton" id="btnCancelAll" data-options="iconCls:'icon-cancel',onClick:_campaign.cancel,disabled:true" style="width: 80px; margin-left: 10px;"><%=Resources.CRMTREEResource.CancelBtn%></a>
                                    <a class="easyui-linkbutton" id="btnRelease" data-options="onClick:_campaign.release,disabled:true" style="width: 80px; margin-left: 10px;"><%=Resources.CRMTREEResource.btnRelease %></a>
                                </span>
                                <span id="ex_btn_approve">
                                    <a class="easyui-linkbutton" id="btnApprove" data-options="iconCls:'icon-ok',onClick:_approve.approve,disabled:true" style="width: 80px;"><%=Resources.CRMTREEResource.btnApprove %></a>
                                    <a class="easyui-linkbutton" id="btnReject" data-options="iconCls:'icon-no',onClick:_approve.reject,disabled:true" style="width: 80px; margin-left: 10px;"><%=Resources.CRMTREEResource.btnReject %></a>
                                </span>
                                <a class="easyui-linkbutton" data-options="iconCls:'icon-back',onClick:_campaign.back" style="margin-left: 10px;"><%=Resources.CRMTREEResource.MyCarBacktoList%></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="tb_Succ_Matrix" style="padding: 0px;">
        <div class="btns" style="margin-right: 5px;"></div>
    </div>
    <div id="tb_Camp_Methods" style="padding: 0px;">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="white-space: nowrap;">
                    <div class="btns" style="margin-right: 5px;"></div>
                </td>
                <td style="white-space: nowrap; width: 100%; text-align: right;">
                    <div style="float: right; text-align: right;">
                        <div id="c_p_upload" class="easyui-progressbar" style="width: 100px; display: none;"></div>
                        <div id="c_upload" class="move" style="float: left; margin-left: 5px;">
                            <a class="easyui-linkbutton" id="c_btn_calls" data-options="plain:true,iconCls:'icon-search'"><%=Resources.CRMTREEResource.calls %></a>
                            <a class="easyui-linkbutton" id="c_btn_upload" data-options="plain:true,iconCls:'icon-up'"><%=Resources.CRMTREEResource.btnUpload %></a>
                            <a class="easyui-linkbutton" id="c_btn_create" data-options="plain:true,onClick:_Camp_Methods.file.create"><%=Resources.CRMTREEResource.btnCreate %></a>
                        </div>
                        <div id="c_btn_group" class="icon-file" style="float: left; padding-left: 20px; display: none;">
                            <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit',onClick:_Camp_Methods.file.edit"><%=Resources.CRMTREEResource.btnEdit %></a>
                            <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search',onClick:_Camp_Methods.file.view"><%=Resources.CRMTREEResource.btnView %></a>
                            <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove',onClick:_Camp_Methods.file.remove"><%=Resources.CRMTREEResource.btnDelete %></a>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none">
        <div id="AddCall_1" style="position: absolute; top: 2px; right: 2px;">
            <fieldset style="border: 1px solid green; border-color: green; width: 360px; height: 300px; white-space: nowrap">
                <legend>电话拨打比例</legend>
                <div class="btns" style="margin-right: 5px;">
                    <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add',onClick:_Call_Per.AddHtml"><%=Resources.CRMTREEResource.cm_cars_buttons_add %></a>
                    <%--<a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit',onClick:_Call_Per.ValiDate">验证</a>--%>
                </div>
                <div style="border-bottom: 1px solid #E4E4E4; padding-top: 5px;"></div>
                <div class="e_ui_all">
                    <%-- <div class="e_ui_list">
                        <span class="I">
                            <input class="easyui-combobox" style="width: 130px;" /></span>
                        <span>
                            <input class="easyui-combobox" style="width: 110px;" /></span>
                        <span class="I" style="vertical-align: middle">
                            <input type="text" style="width: 32px"  />%</span>
                        <span class="I">
                            <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'"><%=Resources.CRMTREEResource.btnDelete %></a></span>
                    </div>--%>
                </div>
            </fieldset>
        </div>
    </div>
    <div id="w_remark" class="easyui-window" data-options="footer:'#ft_remark',minimizable:false,closed:true,title:' ',modal:true"
        style="width: 400px; height: 200px; padding: 5px;">
        <div id="frm_approve" style="width: 100%; height: 100%;">
            <input id="EX_State" type="hidden" value="" />
            <div id="ex_title" style="padding: 5px; padding-left: 0px;"></div>
            <input id="EX_Remark" class="easyui-textbox" data-options="multiline:true,required:true,novalidate:true" style="width: 100%; height: 90px;" />
        </div>
    </div>
    <div id="ft_remark" style="padding: 5px; text-align: right; padding-right: 10px;">
        <a id="btnSure" class="easyui-linkbutton" data-options="onClick:_approve.sure" style="width: 80px;"><%=Resources.CRMTREEResource.btnSure%></a>
        <a class="easyui-linkbutton" data-options="onClick:_approve.close" style="width: 80px; margin-left: 10px;"><%=Resources.CRMTREEResource.btnCancel%></a>
    </div>
</body>
</html>
<script type="text/javascript">
    function SetWinHeight(obj) {
        var win = obj;
        if (document.getElementById) {
            if (win && !window.opera) {
                if (win.contentDocument && win.contentDocument.body.offsetHeight)
                    win.height = win.contentDocument.body.offsetHeight;
                else if (win.Document && win.Document.body.scrollHeight)
                    win.height = win.Document.body.scrollHeight;
            }
        }
    }
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
        _Showedit: function (rp_code, PV_Type) {
            PV_Type = PV_Type ? PV_Type : 1;
            $.topOpen({
                title: '<%=Resources.CRMTREEResource.cmp_title_parameter %>',
                url: '/manage/campaign/ParamValueManager.aspx?PV_Type=' + PV_Type + '&PV_CG_Code=' + _params.CG_Code + '&RP_Code=' + rp_code + "&_winID=" + _campaign.guid,
                width: 650,
                height: 500
            });
        },
        _edit: function (rp_code, PV_Type) {
            if (rp_code > 0) {
                $.post(_s_url, { o: JSON.stringify({ action: 'GetReport_Param', RP_Code: rp_code }) }, function (data) {
                    if (!$.checkResponse(data)) {
                        data = [];
                    }
                    if (data.length > 0) {
                        _RP_Code._Showedit(rp_code, PV_Type);
                    }
                }, "json");
            } else {
                $.msgTips.info("<%=Resources.CRMTREEResource.cmp_targeted_info %>");
            }
        },
        edit: function () {
            var rp_code = $("#CG_RP_Code").combobox('getValue');
            _RP_Code._edit(rp_code, 1);
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
        wid: "#w_remark",
        ctrols: {
            textbox: ['EX_Remark']
        },
        //确定
        sure: function () {
            var approve = $.form.getData("#frm_approve");
            var bValid = true;
            if (approve.EX_State == 0) {
                bValid = $.form.validate(_approve.ctrols);
            }

            if (!bValid) {
                $("#EX_Remark").textbox("textbox").focus();
                return;
            }

            if (!(_params.CG_Code > 0)) { return; }

            $.mask.show();
            $("#btnSure").linkbutton('disable');

            approve.EX_CG_Code = _params.CG_Code;
            approve.EX_AT_Code = _campaign.EX_AT_Code;
            var s_params = JSON.stringify({
                action: 'Save_Activities', activity: approve
            });
            $.post(_s_url, { o: s_params }, function (res) {
                $.mask.hide();
                $("#btnSure").linkbutton('enable');
                _approve.close();

                if ($.checkResponse(res)) {
                    $.msgTips.approval(true);
                    _campaign.backToList();
                } else {
                    $.msgTips.approval(false);
                }
            }, "json");
        },
        //关闭
        close: function () {
            $(_approve.wid).window('close');
        },
        _open: function (title) {
            $(_approve.wid)
            .window({
                title: title,
                onOpen: function () {
                    window.setTimeout(function () {
                        $("#EX_Remark").textbox("textbox").focus();
                    }, 0);
                }
            })
            .window('open');
        },
        //同意
        approve: function () {
            $.form.disableValidation(_approve.ctrols);
            $.form.setData("#frm_approve", { EX_State: 1, EX_Remark: "" });
            $("#ex_title").text("<%=Resources.CRMTREEResource.cmp_app_approve %>");
            $("#btnSure").linkbutton({ text: "<%=Resources.CRMTREEResource.btnApprove %>" });
            _approve._open("<%=Resources.CRMTREEResource.btnApprove %>");
        },
        //不同意
        reject: function () {
            $.form.disableValidation(_approve.ctrols);
            $.form.setData("#frm_approve", { EX_State: 0, EX_Remark: "" });
            $("#ex_title").text("<%=Resources.CRMTREEResource.cmp_app_reject %>");
            $("#btnSure").linkbutton({ text: "<%=Resources.CRMTREEResource.btnReject %>" });
            _approve._open("<%=Resources.CRMTREEResource.btnReject %>");
        },
        onSelect: function (title, index) {
            if (!(_params.CG_Code > 0)) { return; }
            var panel = $("#ctrl_users").accordion('getPanel', index);
            var $content = $(">div", panel);
            var AU_Code = $content.attr("_AU_Code");
            if (!(AU_Code > 0)) { return; }
            $content.empty();
            var $dg = $("<div></div>");
            var $ta = $("<input/>");
            var $cc = $('<div style="width:100%;height:100%;">'
            + '<div data-options="region:\'west\',border:false" style="width:100%;"></div>'
            //+ '<div data-options="region:\'center\',border:false"></div>'
            + '</div>');
            $content.append($cc);
            $cc.layout();

            //$content.append($dg);
            $cc.layout('panel', 'west').append($dg);
            $dg.datagrid({
                width: "100%",
                height: "100%",
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
                            } else if (value == 200) {
                                return "<%=Resources.CRMTREEResource.btnReject %>";
                            } else if (value == 100 || value == 101) {
                                return "<%=Resources.CRMTREEResource.btnApprove %>";
                            } else {
                                return value;
                            }
                        }
                    },
                    { field: 'AAN_Notes', title: 'Notes', width: 400 }
                ]]
            });

            //$content.append($ta);
            $cc.layout('panel', 'center').append($ta);
            $ta.textbox({
                multiline: true,
                width: "100%",
                height: "100%",
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
        },
        //返回到列表
        backToList: function () {
            window.top.location = "/manage/campaign/Campaign_List.aspx?CT=" + _params.CT;
        },
        //返回
        back: function () {
            $.confirmWindow.backToList(function () {
                _approve.backToList();
            });
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
        var eds = $dg.datagrid('getEditors', i);
        for (var j = 0; j < eds.length; j++) {
            var ed = eds[j];
            if (!ed) {
                continue;
            }
            var type = ed.type;
            var $target = $(ed.target);
            if (type === 'combobox') {
                var typeText = $target.combobox('getText');
                rows[i][ed.field + '_Text'] = typeText;
            }
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
        if (field === 'SMV_PSM_Code' && rowData.SMV_CG_Code > 0) return;

        $dg.datagrid('editCell', { index: index, field: field });

        var ed = $dg.datagrid('getEditor', { index: index, field: field });
        if (ed && ed.type === 'combobox') {
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
        title: '<span class="red">*</span><%= Resources.CRMTREESResource.CampaginSM %>',
        collapsible: true,
        collapsed: true,
        toolbar: '#tb_Succ_Matrix',
        height: 160,
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
            $dg.datagrid('getPanel').panel('expand');
            _Succ_Matrix.onClickCell.call($dg, i, 'SMV_PSM_Code');
            break;
        }
    }
    return bValid;
}

//--------------------------------------------------------------------------------------
//Campaign Method（活动方法）
//--------------------------------------------------------------------------------------
var _Camp_Methods = {
    data: {
        CM_RP_Code: [],
        CM_Method: []
    },
    _id: '#EX_Camp_Methods',

    file: {
        _opts: {
            c_upload: 'c_upload',
            c_btn_upload: 'c_btn_upload',
            c_btn_group: 'c_btn_group'
        },
        _progress: $("#c_p_upload"),
        _create: function (params) {
            var row = $(_Camp_Methods._id).datagrid('getSelected');
            if (row) {
                params = params ? $.trim(params) : "";
                if ($.trim(_params.T) != "") {
                    params += "&T=" + _params.T;
                }
                $.topOpen({
                    width: 900,
                    height: 650,
                    url: '/manage/campaign/CampaignFile.aspx?_winID=' + _campaign.guid + params
                });
            }
        },
        create: function () {
            _Camp_Methods.endEditingNoValid();
            _Camp_Methods.file._create();
        },
        view: function () {
            var row = $(_Camp_Methods._id).datagrid('getSelected');
            if (row) {
                var fileName = row.EX_Full_Path ? row.EX_Full_Path : (row.CM_Filename ? '/plupload/file/' + row.CM_Filename : '');
                if (fileName) {
                    var width = 800;
                    var height = 500;
                    var left = parseInt((screen.availWidth / 2) - (width / 2));
                    var top = parseInt((screen.availHeight / 2) - (height / 2));
                    var features = ",width=" + width + ",height=" + height + ",left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
                    var rdm = Math.random();
                    fileName = "/manage/Report/Run_file.aspx?CG_Code=" + row.CM_CG_Code + "&file_url=" + escape(fileName);
                    var myWindow = window.open(fileName + "&r=" + rdm, "fileView", 'status=no,location=no,resizable,toolbar=no,scrollbars' + features, "_blank");
                    if (myWindow) myWindow.focus();
                }
            }
        },
        edit: function () {
            var row = $(_Camp_Methods._id).datagrid('getSelected');
            if (row) {
                var fileName = row.EX_Full_Path ? row.EX_Full_Path : (row.CM_Filename ? '/plupload/file/' + row.CM_Filename : '');
                var params = "";
                if (fileName) {
                    params += "&filename=" + fileName;
                }
                _Camp_Methods.file._create(params);
            }
        },
        remove: function () {
            var $dg = $(_Camp_Methods._id);
            var row = $dg.datagrid('getSelected');
            if (row) {
                row.CM_Filename = "";
                row.EX_Full_Path = "";

                var rowIndex = $dg.datagrid('getRowIndex', row);
                $dg.datagrid('refreshRow', rowIndex);

                $("#" + _Camp_Methods.file._opts.c_btn_group).hide();
                $("#" + _Camp_Methods.file._opts.c_upload).removeClass("move");
            }
        },
        change: function (fileName) {
            if (fileName) {
                var $dg = $(_Camp_Methods._id);
                var row = $dg.datagrid('getSelected');
                if (row) {
                    row.CM_Filename = fileName;
                    row.EX_Full_Path = '/plupload/file_temp/' + fileName;

                    var rowIndex = $dg.datagrid('getRowIndex', row);
                    $dg.datagrid('refreshRow', rowIndex);

                    _Camp_Methods.file._progress.hide();
                    $("#" + _Camp_Methods.file._opts.c_upload).addClass("move");
                    $("#" + _Camp_Methods.file._opts.c_btn_group).show();
                }
            }
        },
        fileUpload: function () {
            $.plupload({
                filters: [
                { title: "Html files(*.htm,*.html)", extensions: "htm,html" }
                ],
                multi_selection: false,
                container: _Camp_Methods.file._opts.c_btn_upload,
                browse_button: _Camp_Methods.file._opts.c_btn_upload,
                params: { filter: "htm,html" },
                init: {
                    FilesAdded: function (up, files) {
                        up.refresh();
                        if (files.length > 0) {
                            _Camp_Methods.endEditingNoValid();
                            $("#" + _Camp_Methods.file._opts.c_upload).addClass("move");
                            _Camp_Methods.file._progress.show().progressbar({ value: 0 });
                            up.start();
                        }
                    },
                    //上传进度
                    UploadProgress: function (up, file) {
                        _Camp_Methods.file._progress.progressbar('setValue', file.percent);
                    },
                    //上传完成
                    FileUploaded: function (up, file) {
                        var fileNames = file.name.split('.');
                        var extendName = fileNames[fileNames.length - 1];
                        var fileName = file.id + '.' + extendName;
                        _Camp_Methods.file.change(fileName);
                    }
                }
            });
        }
    },
    getParamValue: function () {
        var row = $(_Camp_Methods._id).datagrid('getSelected');
        return row.EX_ParamValue ? JSON.parse(row.EX_ParamValue) : [];
    },
    updateTargeted: function (bParamValue, paramValue) {
        if (bParamValue && paramValue && paramValue.length > 0) {
            var $dg = $(_Camp_Methods._id);
            var row = $dg.datagrid('getSelected');
            if (row) {
                row.EX_IsParamValue = bParamValue;
                row.EX_ParamValue = JSON.stringify(paramValue);

                var rowIndex = $dg.datagrid('getRowIndex', row);
                var ed = $dg.datagrid('getEditor', { index: rowIndex, field: 'CM_RP_Code' });
                if (null == ed) { return; }
                var $ed = $(ed.target);

                var text = "";
                var value = $ed.combobox("getValue");
                var data = $ed.combobox("getData");
                $.each(data, function (i, d) {
                    if (d.value == value) {
                        text = d.ex_text;
                    }
                });

                $.each(paramValue, function (i, p) {
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
                    $ed.combobox("setText", text);
                }
            }
        }
    }
};
_Camp_Methods.language = {
    buttons: {
        add: '<%=Resources.CRMTREEResource.cm_cars_buttons_add %>',
        remove: '<%=Resources.CRMTREEResource.cm_cars_buttons_remove %>'
    }
};
//取消编辑
_Camp_Methods.cancelEdit = function () {
    var $dg = $(_Camp_Methods._id);
    var rows = $dg.datagrid('getRows');
    for (var i = 0, len = rows.length; i < len; i++) {
        $dg.datagrid('cancelEdit', i);
    }
}
//结束编辑不验证
_Camp_Methods.endEditingNoValid = function () {
    var $dg = $(_Camp_Methods._id);
    var rows = $dg.datagrid('getRows');
    for (var i = 0, len = rows.length; i < len; i++) {
        var eds = $dg.datagrid('getEditors', i);
        for (var j = 0; j < eds.length; j++) {
            var ed = eds[j];
            if (!ed) {
                continue;
            }
            var type = ed.type;
            var $target = $(ed.target);
            if (type === 'combobox') {
                var typeText = $target.combobox('getText');
                rows[i][ed.field + '_Text'] = typeText;
            }
        }
        $dg.datagrid('endEdit', i);
    }
}
//结束编辑需要验证
_Camp_Methods.endEditing = function () {
    var $dg = $(_Camp_Methods._id);
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
_Camp_Methods.onClickCell = function (index, field) {
    if (field !== 'btnLast') {
        _Camp_Methods.endEditingNoValid();
        var $dg = $(this);
        var rowData = $dg.datagrid('selectRow', index).datagrid('getSelected');

        $dg.datagrid('editCell', { index: index, field: field });

        var ed = $dg.datagrid('getEditor', { index: index, field: field });
        if (ed && ed.type === 'combobox') {
            if (field == 'CM_RP_Code') {
                ed.target.combobox('setText', rowData.CM_RP_Code_Text);
            }
            ed.target.combobox('showPanel');
        }
        if (ed && ed.type === 'numberbox') {
            $c = ed.target;
            $c.numberbox('textbox').select();
        }
    }
}
//创建添加按钮
_Camp_Methods.createAdd = function () {
    var $btns = $("#tb_Camp_Methods div.btns");
    var a_o = [
        { icon: 'icon-add', text: _Camp_Methods.language.buttons.add, clickFun: '_Camp_Methods.add(event,this);' }
    ];
    var a_btns = setBtns(a_o);
    $btns.html(a_btns.join(''));
}
//创建
_Camp_Methods.create = function () {
    var $dg = $(_Camp_Methods._id);
    $dg.datagrid({
        url: null,
        title: '<span class="red">*</span><%=Resources.CRMTREEResource.ContactMethods %>',
        collapsible: true,
        collapsed: true,
        toolbar: '#tb_Camp_Methods',
        height: 410,
        rownumbers: true,
        singleSelect: true,
        //showHeader: false,
        border: true,
        nowrap: false,
        loadMsg: '',
        columns: [[
            {
                field: 'CM_RP_Code', title: '<%=Resources.CRMTREEResource.Condition %>', width: 250,
                formatter: function (value, row) {
                    if (value == null || value == "") {
                        value = "Start the Campaign by :";
                    }
                    return row.CM_RP_Code_Text ? row.CM_RP_Code_Text : "";
                },
                editor: {
                    type: 'combobox', options: {
                        novalidate: true,
                        required: true,
                        height: 40,
                        data: _Camp_Methods.data.CM_RP_Code,
                        icons: [{
                            iconCls: 'icon-edit',
                            handler: function (e) {
                                var $target = $(e.data.target);
                                var rp_code = $target.combobox('getValue');
                                if (rp_code > 0) {
                                    $target.combobox('hidePanel');
                                    _RP_Code._edit(rp_code, 2);
                                }
                            }
                        }],
                        onSelect: function (record) {
                            var row = $dg.datagrid('getSelected');
                            row.EX_ParamValue = "";
                            var rp_code = record.value;
                            if (!(_params.CG_Code > 0 && rp_code == "") && rp_code > 0) {
                                row.EX_IsParamValue = true;
                                _RP_Code._edit(rp_code, 2);
                            }
                        }
                    }
                }
            },
                {
                    field: 'CM_Method', title: '<%=Resources.CRMTREEResource.Method %>', width: 200,
                    formatter: function (value, row) {
                        return row.CM_Method_Text ? row.CM_Method_Text : value;
                    },
                    editor: {  //方法 多选
                        type: 'combobox',
                        options: {
                            multiple: true,
                            multiline: true,
                            novalidate: true,
                            required: true,
                            height: 40,
                            data: _Camp_Methods.data.CM_Method,
                            onSelect: function (_phoneid) {
                                var phoneid = _phoneid.value;
                                var selecteds = $(this).combobox('getText');
                                var selectValues = $(this).combobox('getValues');
                                if (phoneid > 0 && phoneid != "" && selectValues.toString().indexOf(3) >= 0) {
                                    _Camp_Methods.AddCall();
                                }

                            }, onUnselect: function (_phoneid) {
                                var phoneid = _phoneid.value;
                                var selecteds = $(this).combobox('getText');
                            }
                        }
                    }
                },
                {
                    field: 'CM_Filename', title: '<%=Resources.CRMTREEResource.Message %>', width: 60,
                    formatter: function (value, row, index) {
                        var fileName = ($.trim(row.CM_Filename) != '' ?
                        '<div class="icon-file" style="padding-left:20px;width:100%;height:100%;">&nbsp;</div>' : '');
                        return fileName;
                    }
                },
                {
                    field: 'btnLast', align: 'center', width: 35,
                    formatter: function (value, row, index) {
                        var a_o = [
                        { icon: 'icon-remove', title: _Camp_Methods.language.buttons.remove, clickFun: '_Camp_Methods.remove(event,this);' }];
                        return '<div style="height:35px;padding-top:5px;">' + setBtns(a_o, true).join('') + '</div>';
                    }
                }
        ]],
        onClickCell: _Camp_Methods.onClickCell,
        onSelect: function (index, row) {
            var fileName = $.trim(row.CM_Filename);
            if (fileName != '') {
                $("#" + _Camp_Methods.file._opts.c_upload).addClass("move");
                $("#" + _Camp_Methods.file._opts.c_btn_group).show();
            } else {
                $("#" + _Camp_Methods.file._opts.c_upload).removeClass("move");
                $("#" + _Camp_Methods.file._opts.c_btn_group).hide();
            }

            if (typeof (row.CM_Method) != "undefined") {
                var row = $dg.datagrid('getSelected');
                if (row.CM_Method.indexOf(3) >= 0) {
                    _Camp_Methods.AddCall();
                }
            }
        }
    });

    _Camp_Methods.createAdd();

    _Camp_Methods.file.fileUpload();
};
_Camp_Methods.AddCall = function () {
    var _h_p = $("#tb_Camp_Methods").parent().find(".datagrid-view2 .datagrid-body");
    $(_h_p).append($("#AddCall_1"));
    _Call_Per.init();
}

//添加
_Camp_Methods.add = function () {
    var $dg = $(_Camp_Methods._id);
    _Camp_Methods.endEditingNoValid();
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

    _Camp_Methods.onClickCell.call($dg, rowIndex, 'CM_RP_Code');
}
//删除
_Camp_Methods.remove = function (e, target) {
    var $dg = $(_Camp_Methods._id);
    var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
    var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
    if (rowData.CM_CG_Code > 0) {
        $.confirmWindow.tempRemove(function () {
            $dg.datagrid('deleteRow', rowIndex);
            $("#" + _Camp_Methods.file._opts.c_upload).addClass("move");
            $("#" + _Camp_Methods.file._opts.c_btn_group).hide();
        });
    } else {
        if (rowIndex >= 0) {
            $dg.datagrid('deleteRow', rowIndex);
            $("#" + _Camp_Methods.file._opts.c_upload).addClass("move");
            $("#" + _Camp_Methods.file._opts.c_btn_group).hide();
        }
    }
    window.setTimeout(function () {
        _Camp_Methods.endEditingNoValid();
    }, 0);
    stopPropagation(e);
};
//获得
_Camp_Methods.get = function () {
    //_Camp_Methods.endEditingNoValid();
    var $dg = $(_Camp_Methods._id);
    var rows = $dg.datagrid('getRows');
    var deletedRows = $dg.datagrid('getChanges', 'deleted');
    return { changes: rows, deletes: deletedRows };
};
//绑定数据
_Camp_Methods.bindData = function (departments) {
    $(_Camp_Methods._id).datagrid('loadData', departments ? departments : []);
};
//检查数据
_Camp_Methods.check = function () {
    _Camp_Methods.endEditingNoValid();
    var $dg = $(_Camp_Methods._id);
    var rows = $dg.datagrid('getRows');
    var bValid = true;
    for (var i = 0, len = rows.length; i < len; i++) {
        var row = rows[i];
        if ($.trim(row.CM_RP_Code) === '') {
            bValid = false;
            $dg.datagrid('getPanel').panel('expand');
            _Camp_Methods.onClickCell.call($dg, i, 'CM_RP_Code');
            break;
        }
        if ($.trim(row.CM_Method) === '') {
            bValid = false;
            $dg.datagrid('getPanel').panel('expand');
            _Camp_Methods.onClickCell.call($dg, i, 'CM_Method');
            break;
        }
        if ($.trim(row.CM_Filename) === '') {
            bValid = false;
            $dg.datagrid('getPanel').panel('expand');
            _Camp_Methods.onClickCell.call($dg, i, 'CM_Filename');
            break;
        }
    }
    if (bValid) {
        $.each(rows, function (i, o) {
            var _m = o.CM_Method.split(",");
            $.each(_m, function (j, _o) {
                if (_o == 3) {
                    _Camp_Methods.$AddCall = true;
                }
            });
        });
    }
    return bValid;
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
    $.post(_s_url, { o: JSON.stringify({ action: 'GetCamCall', CG_Code: _cg_code }) }, function (res) {
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
    $("#AddCall_1 .e_ui_all").empty();
    for (var i = 0; i < _setData.length; i++) {
        var _html = _Call_Per.setHTML(i, _setData[i]);
        $("#AddCall_1 .e_ui_all").append(_html);
        $.parser.parse("#AddCall_1 .UI_II");
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
    $("#AddCall_1 .e_ui_all").append(_html);
    $.parser.parse("#AddCall_1 .UI_II");
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
    $.post(_s_url, { o: JSON.stringify({ action: 'GetTeamGroupUser', UG_Code: record.TG_UG_Code }) }, function (res) {
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
    $.post(_s_url, { o: JSON.stringify({ action: 'GetTeamGroupUser', UG_Code: 26 }) }, function (res) {
        if ($.checkErrCode(res)) {
            $("#CamCall" + Index).combobox("loadData", res.TeamGroupUser).combobox("setValue", -1);
        }
    }, 'json');
}
//--------------------------------------------------------------------------------------
//Campaign（活动）
//--------------------------------------------------------------------------------------
var _campaign = {
    guid: '<%=Guid.NewGuid()%>',
    bParamValue: false,
    paramValue: [],
    report: "",
    approvalList: [],
    EX_AT_Code: "",
    select_CG_EG_Code: function (record) {
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
                if (!data) { data = []; }
                data.push(res);
                $("#CG_EG_Code").combobox('loadData', data);
                $('#EX_CG_EG_Code').textbox('clear');
            }
        }, "json");
    },
    updateTargeted: function (bParamValue, paramValue) {
        _campaign.bParamValue = bParamValue;
        _campaign.paramValue = paramValue;
        if (_campaign.bParamValue && _campaign.paramValue.length > 0) {
            var text = "";
            var value = $("#CG_RP_Code").combobox("getValue");
            var data = $("#CG_RP_Code").combobox("getData");
            $.each(data, function (i, d) {
                if (d.value == value) {
                    text = d.ex_text;
                }
            });

            $.each(_campaign.paramValue, function (i, p) {
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
        textbox: ['CG_Title', 'CG_Desc'],
        combobox: ['CG_EG_Code', 'CG_RP_Code', 'CG_Responsible'],
        datebox: ['CG_Act_S_Dt']
        //numberbox: ['Ex_CG_Act_E_Dt', 'Ex_CG_Start_Dt', 'Ex_CG_End_Dt']
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
    _getCampaign: function () {
        _campaign.disableValidation();
        var o = $.form.getData("#frm_campaign", ["CG_RSVP"]);
        var bValidForm = _campaign.validate(o);
        var bValidSM = _Succ_Matrix.check();
        //if (!Check.IsSucc_Matrix()) {
        //    bValidSM = true;
        //}

        //特殊赋值
        o.CG_Type = _params.CT;
        var bValid = true;
        var msg = [];

        var bCampMethods = _Camp_Methods.check();
        var camp_methods = _Camp_Methods.get();
        if (!bCampMethods || camp_methods.changes.length == 0) {
            msg[msg.length] = '<%=Resources.CRMTREEResource.ContactMethods %><%= Resources.CRMTREEResource.required %>';
            bValid = false;
        }

        //Success Matrix
        var sm_values = _Succ_Matrix.get();
        var b_sm_values = true;
        $.each(_campaign.auth.fields.CG_Succ_Matrix, function (i, ct) {
            if (_params.CA == ct[0] && _params.CT == ct[1]) {
                b_sm_values = false;
                return false;
            }
        });
        if (b_sm_values && sm_values && sm_values.changes.length == 0) {
            msg[msg.length] = '<%= Resources.CRMTREESResource.CampaginSM %><%= Resources.CRMTREEResource.required %>';
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
        if (_params.CA > 0) {
            o.CG_Cat = _params.CA;
        }
        if (_params.CT == 95 && _params.T != 2) {
            var _VINfile = $("#VIN_File_hid").val();
            if (!_VINfile) { return false; } else {
                o.CG_Filename = $("#VIN_File_hid").val();
            }
        }
        var CallValiDate = _Call_Per.ValiDate(), CallDate = null;
        if (CallValiDate) {
            for (var i = 0; i < _Call_Per.$CamCall.length; i++) {
                if (_Call_Per.$CamCall[i].Remove) {
                    _Call_Per.$CamCall.splice(i, 1);
                    i--;
                }
            }
            CallDate = _Call_Per.$CamCall;
        } else {
            if (_Call_Per.$CamCall.length > 0) {
                $.msgTips.info("电话拨打比例之和不符合规则");
                return false;
            }
        }
        return {
            action: 'Save_Campaign',
            campaign: o,
            sm_values: sm_values,
            bParamValue: _campaign.bParamValue,
            param_value: _campaign.paramValue,
            camp_methods: camp_methods,
            callValiDate: CallValiDate,
            callDatd: CallDate
        };
    },
    //保存
    save: function () {
        var o = _campaign._getCampaign();
        if (!o) {
            return;
        }
        if (_params.CA > 0) {
            o.CG_Cat = _params.CA;
        }

        $.mask.show();
        $("#btnSave,#btnRelease,#btnCancelAll").linkbutton('disable');
        $.post(_s_url, { o: JSON.stringify(o) }, function (res) {
            $.mask.hide();
            $("#ex_btn_Edit").show();
            $("#btnSave").linkbutton('enable');
            $("#btnCancelAll").linkbutton('enable');
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
        window.top.location = "/manage/campaign/Campaign_List.aspx?CA=" + _params.CA + "&CT=" + _params.CT;
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
                if ($.checkResponse(data, false)) {
                    $.msgTips.release(true);
                    _campaign.backToList();
                } else {
                    $.msgTips.release(false);
                }
            }, "json");
        });
    },
    Get_Reports: function () {
        $.post(_s_url, { o: JSON.stringify({ action: 'Get_Reports', CG_Type: _params.CT, CA: _params.CA }) }, function (data) {
            if (!$.checkResponse(data)) {
                data = [];
            }

            $("#CG_RP_Code").combobox('loadData', data);
            if (_params.CT == 95) {
                $("#CG_RP_Code").combobox('setValue', 153);
                $("#editRP_Code").hide();
            }
            _campaign.Get_Reports_Method();

        }, "json");
    },
    Set_Reports_info: function (r, c) {
        $.post(_s_url, { o: JSON.stringify({ action: 'Get_Report_info', RP_Code: r, CG_Code: c }) }, function (data) {
            if (!$.checkResponse(data)) {
                data = [];
            }
            $("#CG_RP_Code").combobox("setText", data);
        }, "json");
    },
    Get_Report_Param: function (r) {
        $.post(_s_url, { o: JSON.stringify({ action: 'GetReport_Param', RP_Code: r }) }, function (data) {
            if (!$.checkResponse(data)) {
                data = [];
            }
        }, "json");
    },
    Get_Reports_Method: function () {
        $.post(_s_url, {
            o: JSON.stringify({
                action: 'Get_Reports_Method', CG_Type: _params.CT, CA: _params.CA
            })
        }, function (data) {
            if (!$.checkResponse(data)) {
                data = [];
            }
            _Camp_Methods.data.CM_RP_Code = data;

            _campaign.Get_Event_Genre();
        }, "json");
    },
    Get_Event_Genre: function () {
        $.post(_s_url, { o: JSON.stringify({ action: 'Get_Event_Genre' }) }, function (data) {
            if (!$.checkResponse(data)) {
                data = [];
            }
            data.push({ value: '-1', text: (_isEn ? "Others" : "其它") });

            $("#CG_EG_Code").combobox('loadData', data);

            _campaign.getWordByValue();
        }, "json");
    },
    getWordByValue: function () {
        var ct = _params.CT;

        var pid = 4093;
        var cat = _params.CA;
        //if cat is null tgen cat=cg_cat
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
            if (o) {
                $("#c_campaign_editor").text($.trim(o.text));
            }
            _campaign.getApprovalList();
        });
    },
    getApprovalList: function () {
        $.post(_s_url, {
            o: JSON.stringify({
                action: 'GetApprovalList',
                CT: _params.CT, CA: _params.CA
            })
        }, function (rows) {
            if (!$.checkResponse(rows)) {
                rows = [];
            }
            _campaign.approvalList = rows;
            for (var i = 0, len = rows.length; i < len; i++) {
                var dataRow = rows[i];
                $.each(dataRow, function (n, d) {
                    if (d === null) {
                        dataRow[n] = "";
                    }
                });

                $('#ctrl_users').accordion('add', {
                    title: dataRow.User_Name,
                    content: '<div style="width:100%;height:80px" _AU_Code = "' + dataRow.User_Code + '"></div>',
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
                    _Camp_Methods.data.CM_Method = _Camp_Methods.data.CM_Method.concat(d._4059);
                    $("#CG_Mess_Type").radiolist({ data: d._4059 });
                }
                if (d._4064) {
                    _Camp_Methods.data.CM_Method = _Camp_Methods.data.CM_Method.concat(d._4064);
                    $("#EX_CG_Method_Text").checkboxlist({ data: d._4064 });
                }

                if (d._4068) {
                    _Camp_Methods.data.CM_Method = _Camp_Methods.data.CM_Method.concat(d._4068);
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
                _Succ_Matrix.data = data;
            }
            _Camp_Methods.create();

            _Succ_Matrix.create();
            _campaign.bindDefaults();
            _campaign.bind();

            _campaign.auth.state();
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

        _campaign.file.fileUploadVIN();

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
        if (_params.CT == 95) {
            $("#VIN_file_Up").css({ display: "inline" });
            $("#VIN_btn_upload").show();
            $("#VIN_again_upload").hide();
            $("#VIN_download_file").hide();
            $("#VIN_Delete_file").hide();
        } else {
            $("#VIN_file_Up").css({ display: "none" });
        }
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

                    $.each(_campaign.approvalList, function (i, al) {
                        if (al.User_Code == res.AU_Code) {
                            _campaign.EX_AT_Code = al.AT_Code;
                        }
                    });

                    _Camp_Methods.bindData(res.methods);
                    setTimeout(_campaign.Set_Reports_info(campaign.CG_RP_Code, _params.CG_Code), 1000);
                    $("#ctrl_status").text($.trim(campaign.EX_CG_Status));
                    if ($.trim(campaign.CG_Filename) == "") {
                        $("#VIN_btn_upload .l-btn-left .l-btn-text").text("<%=Resources.CRMTREEResource.Again_Upload %>");
                        // $("#VIN_btn_upload").show();
                        $("#VIN_again_upload").hide();
                        $("#VIN_download_file").hide();
                        $("#VIN_Delete_file").hide();
                    } else {
                        $("#VIN_btn_upload .l-btn-left .l-btn-text").text("<%=Resources.CRMTREEResource.Again_Upload %>");
                        //$("#VIN_btn_upload").hide();
                        $("#VIN_again_upload").show();
                        $("#VIN_download_file").show();
                        $("#VIN_Delete_file").show();
                        $("#VIN_File_hid").val(campaign.CG_Filename);
                    }
                    $("#ex_btn_Edit").show();
                    $("#btnSave,#btnRelease,#btnCancelAll").linkbutton('enable');
                    $("#ex_btn_approve").hide();
                    if (campaign.EX_Approve > 0) {
                        $("#btnSave,#btnRelease,#btnCancelAll").linkbutton('disable');
                        $("#ex_btn_Edit").hide();
                    }
                    if (campaign.EX_Approve == 1) {
                        $("#btnApprove,#btnReject").linkbutton('enable');
                        $("#ex_btn_approve").show();
                    }
                }
            }, "json");
        } else {
            $.getWordByValue(4044, 0, function (o) {
                if (o) {
                    $("#ctrl_status").text($.trim(o.text));
                }
            });
            $("#ctrol_container_upload").removeClass("move");
            $("#ctrol_btn_create").show();
            $("#btnSave").linkbutton('enable');
        }
    },
    bindDefaults: function () {//绑定默认值
        //Date & Time
        var date = _isEn ? '<%=DateTime.Now.AddDays(1).ToString("MM/dd/yyyy")%>' : '<%=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")%>';
        $("#CG_Act_S_Dt").datebox('setValue', date);
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
        create: function () {
            _campaign.file._create();
        },
        view: function () {
            var width = 800;
            var height = 500;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var features = ",width=" + width + ",height=" + height + ",left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
            var rdm = Math.random();
            var myWindow = window.open(_campaign.file._fullPath + "?r=" + rdm, "fileView", 'status=no,location=no,resizable,toolbar=no,scrollbars' + features, "_blank");
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
        },
        fileUploadVIN: function () {
            var optioins = {
                runtimes: 'flash',
                filters: [
                    { title: "Excel files(*.xls,*.xlsx)", extensions: "xls,xlsx" }
                ],
                chunk_size: '1mb',
                max_file_size: '10mb',
                file_data_name: 'file',
                unique_names: true,
                //上传参数
                params: {},
                _url: '/handler/VINUpload.aspx',
                flash_swf_url: '/scripts/plupload-1.5.7/plupload.flash.swf',
                silverlight_xap_url: '/scripts/plupload-1.5.7/plupload.silverlight.xap',
                multi_selection: false,
                browse_button: 'VIN_btn_upload',
                container: 'VIN_btn_upload',
                init: {
                    FilesAdded: function (up, files) {
                        up.refresh();
                        if (files.length > 0) {
                            up.start();
                        }
                    },
                    //上传进度
                    UploadProgress: function (up, file) {
                    },
                    //上传完成
                    FileUploaded: function (up, file) {
                        $("#VIN_File_hid").val(file.target_name);
                        $("#VIN_btn_upload .l-btn-left .l-btn-text").text("<%=Resources.CRMTREEResource.Again_Upload %>");
                        $("#VIN_again_upload").show();
                        $("#VIN_download_file").show();
                        $("#VIN_Delete_file").show();
                        $.msgTips.UpLoadVIN(true);
                        setTimeout(function () {
                            $.msgTips.UpLoadVIN_Checking(true);
                            _campaign.file.Check_VIN();
                        }, 2000);

                    },
                    BeforeUpload: function (up, file) {
                        var params = '';
                        if (up.settings.params) {
                            params = '&' + $.param(up.settings.params);
                        }
                        //解决中文文件名乱码问题
                        up.settings.url = up.settings._url + '?fileName=' + file.name + params;
                    },
                    Error: function (up, err) {
                        up.refresh();
                        // $.msgTips.UpLoadVIN(false);
                    }
                }
            };
            //创建上传对象
            var uploader = new plupload.Uploader(optioins);
            //上传初始化
            uploader.init();
        },
        fileAgainUploadVIN: function () {
            var optioins = {
                runtimes: 'flash',
                filters: [
                    { title: "Excel files(*.xls,*.xlsx)", extensions: "xls,xlsx" }
                ],
                chunk_size: '1mb',
                max_file_size: '10mb',
                file_data_name: 'file',
                unique_names: true,
                //上传参数
                params: {},
                _url: '/handler/VINUpload.aspx',
                flash_swf_url: '/scripts/plupload-1.5.7/plupload.flash.swf',
                silverlight_xap_url: '/scripts/plupload-1.5.7/plupload.silverlight.xap',
                multi_selection: false,
                browse_button: 'VIN_again_upload',
                container: 'VIN_again_upload',
                init: {
                    FilesAdded: function (up, files) {
                        up.refresh();
                        if (files.length > 0) {
                            up.start();
                        }
                    },
                    //上传进度
                    UploadProgress: function (up, file) {
                    },
                    //上传完成
                    FileUploaded: function (up, file) {
                        $("#VIN_File_hid").val(file.target_name);
                    },
                    BeforeUpload: function (up, file) {
                        var params = '';
                        if (up.settings.params) {
                            params = '&' + $.param(up.settings.params);
                        }
                        //解决中文文件名乱码问题
                        up.settings.url = up.settings._url + '?fileName=' + file.name + params;
                    },
                    Error: function (up, err) {
                        up.refresh();
                    }
                }
            };
            //创建上传对象
            var uploader = new plupload.Uploader(optioins);
            //上传初始化
            uploader.init();
        },
        Check_VIN: function () {
            var s_params = JSON.stringify({ action: 'checkErrVINFile', vinFile: $("#VIN_File_hid").val() });
            $.post(_s_url, { o: s_params }, function (res) {
                if ($.checkErrCode(res)) {
                    if (res == 0) {
                        $.msgTips.UpLoadVIN_Check(true);
                    } else {
                        $.msgTips.UploadErrVin_Check(res);
                    }
                }
            }, "json");
        },
        Check_VIN_download: function () {
            var s_params = JSON.stringify({ action: 'checkVINFile', vinFile: $("#VIN_File_hid").val() });
            $.post(_s_url, { o: s_params }, function (res) {
                if ($.checkResponse(res)) {
                    var _file_hid = $("#VIN_File_hid").val();
                    if (_file_hid) {
                        window.location.href = "/handler/Downloads.aspx?T=2&fileName=" + $("#VIN_File_hid").val();
                    }
                }
            }, "json");
        },
        Delete_VIN: function () {
            var s_params = JSON.stringify({ action: 'deleteVINFile', vinFile: $("#VIN_File_hid").val() });
            $.post(_s_url, { o: s_params }, function (res) {
                if ($.checkResponse(res)) {
                    if (res == 1) {
                        $("#VIN_File_hid").val("");
                        $("#VIN_btn_upload").show();
                        $("#VIN_again_upload").hide();
                        $("#VIN_download_file").hide();
                        $("#VIN_Delete_file").hide();
                        $.msgTips.remove(true);
                    } else {
                        $.msgTips.remove(false);
                    }
                } else {
                    $.msgTips.remove(false);
                }
            }, "json");
        }

    },
    auth: {
        fields: {
            CG_EG_Code: [[1, 40], [1, 90]],
            CG_Responsible: [[1, 40], [1, 90]],
            CG_Act_S_Dt: [[1, 1], [1, 2], [1, 3], [1, 40], [1, 85], [1, 86], [1, 51], [1, 52], [1, 53], [1, 90], [1, 85], [1, 86], [1, 95]],
            Ex_CG_Act_E_Dt: [[1, 1], [1, 2], [1, 3], [1, 40], [1, 85], [1, 86], [1, 51], [1, 52], [1, 53], [1, 90], [1, 85], [1, 86], [1, 95]],
            Ex_CG_Start_Dt: [[1, 1], [1, 2], [1, 3], [1, 40], [1, 85], [1, 86], [1, 51], [1, 52], [1, 53], [1, 90], [1, 85], [1, 86], [1, 95]],
            Ex_CG_End_Dt: [[1, 1], [1, 2], [1, 3], [1, 40], [1, 85], [1, 86], [1, 51], [1, 52], [1, 53], [1, 90], [1, 85], [1, 86], [1, 95]],
            CG_Succ_Matrix: [[1, 35], [1, 36], [1, 85], [1, 86], [2, 81], [2, 71], [2, 92], [2, 61], [2, 66], [2, 50], [3, 71], [3, 72], [3, 73], [3, 74]]
        },
        controls: {
            ex_tr_budget: [[1, 1], [1, 2], [1, 3], [1, 40], [1, 51], [1, 52], [1, 53], [1, 90]]
            , ex_tr_tools: [[1, 40], [1, 90]]
            , ex_tr_calendar: [[1, 1], [1, 2], [1, 3], [1, 40], [1, 51], [1, 52], [1, 53], [1, 90], [1, 95]]
            , ex_tr_rsvp: [[1, 40], [1, 90]]
            , ex_tr_genre: [[1, 40], [1, 90]]
            , ex_tr_response: [[1, 40], [1, 90]]
            , ex_fs_phone_calls: [[1, 1], [1, 2], [1, 3], [1, 20], [1, 30], [1, 40], [1, 51], [1, 52], [1, 53], [1, 90], [1, 70], [1, 80]]
            , ex_tr_matrix: [[1, 1], [1, 2], [1, 3], [1, 20], [1, 30], [1, 40], [1, 51], [1, 52], [1, 53], [1, 90], [1, 95], [2, 80], [2, 70], [2, 90], [2, 91], [2, 60], [2, 65], [3, 70]]
            , VIN_file_Up: [[1, 95]]
        },
        state: function () {
            var cg_type = _params.CT;
            var cg_ca = _params.CA;
            $.each(_campaign.ctrols, function (i, cs) {
                for (var j = 0; j < cs.length; j++) {
                    var c = cs[j];
                    var auths = _campaign.auth.fields[c];
                    if (auths && auths.length > 0) {
                        var b = false;
                        $.each(auths, function (k, auth) {
                            if (auth.length == 2) {
                                if (auth[0] == cg_ca && auth[1] == cg_type) {
                                    b = true;
                                    return false;
                                }
                            }
                        });
                        if (!b) {
                            cs.splice(j, 1);
                            j--;
                        }
                    }
                }
            });
            $.each(_campaign.auth.controls, function (n, d) {
                var b = false;
                $.each(d, function (i, ct) {
                    if (ct.length == 2) {
                        if (ct[0] == cg_ca && ct[1] == cg_type) {
                            b = true;
                            return false;
                        }
                    }
                });
                if (!b) {
                    $("#" + n).addClass('hide');
                }
            });
        }
    }
};
var Check = {};
Check.IsSucc_Matrix = function () {
    var Is_Matrix = false;
    var cg_type = _params.CT;
    var _ma_s = _campaign.auth.controls.ex_tr_matrix;
    for (var i = 0; i < _ma_s.length ; i++) {
        if (_ma_s[i][1] == cg_type) {
            Is_Matrix = true;
            break;
        }
    }
    return Is_Matrix;
}
$(function () {
    $(window).unload(function () {
        try {
            $("object").detach();
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
var isShow = false;
$(function () {
    //(function () {
    //    var doc = document,
    //      //获取父页面的容器
    //       par = window.parent.document.getElementById('ctrl_iframe'),
    //       body = doc.body,
    //     div, height;
    //    setTimeout(function () {
    //        div = doc.createElement('br');
    //        body.appendChild(div);
    //        //当前页面的高度
    //        height =  body.scrollHeight;
            
    //        //设置容器高度      
    //        par.style.height = height + 'px';
    //        //定时来执行
    //        //setTimeout(arguments.callee, 300);
    //    }, 100);
    //})();
    $('#c_btn_calls').click(function () {
        isShow = !isShow;
        if (isShow)
            _Camp_Methods.AddCall();
    });
});
</script>
