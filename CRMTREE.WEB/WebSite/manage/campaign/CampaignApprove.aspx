<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampaignApprove.aspx.cs" Inherits="manage_campaign_CampaignApprove" %>

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
                background-color:#FDFAEE;
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
            background-color: #FDFAEE;
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
    <div id="ex_campaign_title" class="easyui-panel" data-options="title:'<%= Resources.CRMTREEResource.CampaignEditor %>',fit:true" style="overflow:hidden;padding:5px;">
    <table id="frm_campaign" class="tbl" cellpadding="0" cellspacing="0">
        <tr>
            <th class="th" style="width:170px;">
                <div style="width:170px;">
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
                <input id="CG_RP_Code" class="easyui-combobox" data-options="required:true,novalidate:true" style="width:60%;"/>
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
                <input id="CG_EG_Code" class="easyui-combobox" data-options="required:true,novalidate:true"/>
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
                <div id="control_btn_group" class="icon-file" style="float:left;display:none;padding-left:20px;">
                    <a class="easyui-linkbutton" id="btnFileView" data-options="plain:true,iconCls:'icon-search',onClick:_campaign.file.view"><%=Resources.CRMTREEResource.btnView %></a>
                </div>
            </td>
        </tr>

        <tr>
            <td class="td" colspan="4" style="text-align:center;">
                <span id="ex_btn_approve">
                    <a class="easyui-linkbutton" id="btnApprove" data-options="iconCls:'icon-ok',onClick:_approve.approve,disabled:true" style="width:80px;"><%=Resources.CRMTREEResource.btnApprove %></a>
                    <a class="easyui-linkbutton" id="btnReject" data-options="iconCls:'icon-no',onClick:_approve.reject,disabled:true" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.btnReject %></a>
                </span>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-back',onClick:_approve.back" style="margin-left:10px;"><%=Resources.CRMTREEResource.MyCarBacktoList%></a>
            </td>
        </tr>
    </table>
    </div>

    <div id="tb_Succ_Matrix" style="padding:0px;">
        <div class="btns" style="margin-right:5px;"></div>
    </div>

    <div id="w_remark" class="easyui-window" data-options="footer:'#ft_remark',minimizable:false,closed:true,title:' ',modal:true" 
    style="width:400px;height:200px;padding:5px;">
        <div id="frm_approve" style="width:100%;height:100%;">
            <input id="EX_State" type="hidden" value=""/>
            <div id="ex_title" style="padding:5px;padding-left:0px;"></div>
            <input id="EX_Remark" class="easyui-textbox" data-options="multiline:true,required:true,novalidate:true" style="width:100%;height:90px;" />
        </div>
    </div>

    <div id="ft_remark" style="padding:5px;text-align:right;padding-right:10px;">
        <a id="btnSure" class="easyui-linkbutton" data-options="onClick:_approve.sure" style="width:80px;"><%=Resources.CRMTREEResource.btnSure%></a>
        <a class="easyui-linkbutton" data-options="onClick:_approve.close" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.btnCancel%></a>
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

            if (!(_params.CG_Code > 0)) { return;}
            
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
        _open:function(title){
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
            $content.append($dg);
            $dg.datagrid({
                width: "100%",
                height: 60,
                url: "",
                singleSelect: true,
                showHeader:false,
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
                readonly:true
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
            window.top.location = "/manage/campaign/List_Campaign.aspx?CT=" + _params.CT;
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
        data: [],
        _id: '#CG_Succ_Matrix'
    };
    _Succ_Matrix.language = {

    };
    //创建
    _Succ_Matrix.create = function () {
        var $dg = $(_Succ_Matrix._id);
        $dg.datagrid({
            url: null,
            title: '<%=Resources.CRMTREEResource.camp_matrix %>',
            collapsible: true,
            //collapsed:true,
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
                    }
                },
                {
                    field: 'SMV_Days', title: '<%=Resources.CRMTREEResource.cmp_SMV_Days %>', width: 80
                },
                {
                    field: 'PSM_Val_Type', title: '<%=Resources.CRMTREEResource.cmp_PSM_Val_Type %>', width: 120
                },
                {
                    field: 'SMV_Val', title: '<%=Resources.CRMTREEResource.cmp_SMV_Val %>', width: 80
                }
            ]]
        });
    };
    //绑定数据
    _Succ_Matrix.bindData = function (departments) {
        $(_Succ_Matrix._id).datagrid('loadData', departments ? departments : []);
    };

    //--------------------------------------------------------------------------------------
    //Campaign（活动）
    //--------------------------------------------------------------------------------------
    var _campaign = {
        guid: '<%=Guid.NewGuid()%>',
        bParamValue: false,
        paramValue: [],
        report: "",
        approvalList: [],
        EX_AT_Code:"",
        ctrols: {
            textbox: ['CG_Title', 'CG_Desc'],//, 'CG_Filename'
            combobox: ['CG_RP_Code', 'CG_EG_Code', 'CG_Responsible'],
            datebox: ['CG_Act_S_Dt'],
            numberbox: ['Ex_CG_Act_E_Dt', 'Ex_CG_Start_Dt', 'Ex_CG_End_Dt']
        },
        //返回到列表
        backToList: function () {
            window.top.location = "/manage/campaign/List_Campaign.aspx?CT=" + _params.CT;
        },
        Get_Reports: function () {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Reports', CG_Type: _params.CT }) }, function (data) {
                if (!$.checkResponse(data)) {
                    data = [];
                }

                $("#CG_RP_Code").combobox('loadData', data);

                _campaign.Get_Event_Genre();
            }, "json");
        },
        Get_Event_Genre: function () {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Event_Genre' }) }, function (data) {
                if (!$.checkResponse(data)) {
                    data = [];
                }

                $("#CG_EG_Code").combobox('loadData', data);

                _campaign.getWordByValue();
            }, "json");
        },
        getWordByValue: function () {
            var ct = _params.CT;
            $.getWordByValue(4093, ct, function (o) {
                if (o) {
                    $("#ex_campaign_title").panel({ title: "<%= Resources.CRMTREEResource.CampaignEditor %> " + $.trim(o.text) });
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

            _campaign.auth.state();

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

                        $.each(_campaign.approvalList, function (i, al) {
                            if (al.User_Code == res.AU_Code) {
                                _campaign.EX_AT_Code = al.AT_Code;
                            }
                        });

                        $("#ctrl_status").text($.trim(campaign.EX_CG_Status));
                        $("#control_btn_group").show();

                        if (!campaign.EX_Approve) {
                            $("#ex_btn_approve").hide();
                        } else {
                            $("#btnApprove,#btnReject").linkbutton('enable');
                        }
                    }
                    $.form.readonly("#frm_campaign");
                }, "json");
            }
        },
        //绑定默认值
        bindDefaults: function () {
            var date = _isEn ? '<%=DateTime.Now.AddDays(1).ToString("MM/dd/yyyy")%>' : '<%=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")%>';
            $("#CG_Act_S_Dt").datebox('setValue', date);
        },
        file: {
            _fullPath: '',
            view: function () {
                var width = 800;
                var height = 500;
                var left = parseInt((screen.availWidth / 2) - (width / 2));
                var top = parseInt((screen.availHeight / 2) - (height / 2));
                var features = ",width=" + width + ",height=" + height + ",left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
                var rdm = Math.random();
                var myWindow = window.open(_campaign.file._fullPath + "?r=" + rdm, "fileView", 'status=no,location=no,resizable,toolbar=no,scrollbars' + features, "_blank");
                if (myWindow) myWindow.focus();

            }
        },
        auth: {
            fields: {
                CG_EG_Code: [40, 90],
                CG_Responsible: [40, 90],
                CG_Act_S_Dt: [1, 2, 3, 40, 51, 52, 53, 90],
                Ex_CG_Act_E_Dt: [1, 2, 3, 40, 51, 52, 53, 90],
                Ex_CG_Start_Dt: [1, 2, 3, 40, 51, 52, 53, 90],
                Ex_CG_End_Dt: [1, 2, 3, 40, 51, 52, 53, 90]
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

                $.each(_campaign.ctrols, function (i, cs) {
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