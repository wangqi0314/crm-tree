<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncomingCallsManager.aspx.cs" Inherits="manage_customer_IncomingCallsManager" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%= Request.QueryString["CD"] != null ? (Request.QueryString["CD"].ToString() == "1" ? Resources.CRMTREEResource.Incoming : Resources.CRMTREEResource.Calling) : ""  %></title>
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
                background-color:#E7EADB;
            }
            .tbl tr th.top {
                vertical-align:top;
            }
            .tbl tr .th,.tbl tr .td {
                padding:6px;
                border-right:1px solid #ccc;
                border-bottom:1px solid #ccc;
                padding-top:10px;
                padding-bottom:10px;
            }
    </style>
</head>
<body>
    <div id="frm_incoming_calls">
        <table class="tbl" cellpadding="0" cellspacing="0">
            <tr>
                <th class="th">
                   <%= Resources.CRMTREEResource.phone %>
                </th>
                <td class="td">
                   <input id="PL_Code" class="easyui-combobox" data-options="width:200"/>
                   <a class="easyui-linkbutton" id="btnPhoneList" title="Add" data-options="iconCls:'icon-reload',disabled:true,plain:true,onClick:_incoming_calls.PhoneList"></a>
                </td>
            </tr>
            <tr>
                <th class="th" style="width:80px;">
                    <div style="width:80px;">
                    <%= Resources.CRMTREEResource.Person %><span class="red">*</span>
                    </div>
                </th>
                <td class="td">
                    <input id="CH_AU_Code" class="easyui-combogrid fluid" data-options="
                    iconCls:'icon-search',iconAlign:'right',
                    width:300,
                    panelWidth:500,
                    required:true,
                    url: '',
                    hasDownArrow:false,
                    idField:'AU_Code',
                    textField:'AU_Name',
                    fitColumns:true,
                    columns:[[
                    {field:'AU_Name',title:'<%=Resources.CRMTREEResource.am_customer_AU_Name %>',width:150},
                    {field:'Phone',title:'<%=Resources.CRMTREEResource.am_customer_Phone %>',width:100},
                    {field:'Lic',title:'<%=Resources.CRMTREEResource.main_customer_Car_Lic %>',width:100},
                    {field:'Car_Model',title:'<%=Resources.CRMTREEResource.am_customer_Car_Model %>',width:200}
                    ]]
                    "/>

                    <a class="easyui-linkbutton" id="btnAddCustomer" title="Add Customer" data-options="iconCls:'icon-add',disabled:true,plain:true,onClick:_incoming_calls.customer.add"></a>
                    <a class="easyui-linkbutton" id="btnEditCustomer" title="Edit Customer" data-options="iconCls:'icon-edit',disabled:true,plain:true,onClick:_incoming_calls.customer.edit"></a>
                </td>
            </tr>
            <tr>
                <th class="th">
                   <%= Resources.CRMTREEResource.Reason %><span class="red">*</span>
                </th>
                <td class="td">
                   <input id="CH_PTY_Code" class="easyui-combobox" data-options="width:200,panelHeight:140,required:true,novalidate:true,onSelect:_incoming_calls.onSelect.CH_PTY_Code"/>
                   
                   <a class="easyui-linkbutton" id="btnAddAction" title="Add" data-options="iconCls:'icon-reload',disabled:true,plain:true,onClick:_incoming_calls.action"></a>
                </td>
            </tr>
            <tr id="c_tr_notes">
                <th class="th">
                   <%= Resources.CRMTREEResource.Notes %>
                </th>
                <td class="td">
                    <input id="DH_Notes" class="easyui-textbox" data-options="tipPosition:'bottom',required:false,novalidate:true,multiline:true,width:300,height:60"/>
                </td>
            </tr>
            <tr id="c_tr_action" style="display:none">
                <th class="th">
                   <%= Resources.CRMTREEResource.Action %>
                </th>
                <td class="td">
                   <input id="CH_Status" class="easyui-combobox" data-options="width:200,required:false,novalidate:true,                       icons: [{	                   iconCls:'icon-clear',	                   handler: function(e){		                   $(e.data.target).combobox('clear');	                   }                    }]"/>
                </td>
            </tr>
            <tr>
                <td class="td"></td>
                <td class="td" style="text-align:left;padding-left:10px;">
                    <a class="easyui-linkbutton" id="btnDone" data-options="iconCls:'icon-save',onClick:function(){_incoming_calls.done(0)},disabled:true" style="width:80px;"><%= Resources.CRMTREEResource.btnDone %></a>
                    &nbsp&nbsp <a class="easyui-linkbutton" id="btnMore" data-options="iconCls:'icon-save',onClick:function(){_incoming_calls.done(1)},disabled:true" style="width:180px;"><%= Resources.CRMTREEResource.btnMore %></a>
                </td>
            </tr>
        </table>
    </div>
    
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Customer/IncomingCalls.aspx';
    //var _s_url_report = '/handler/Reports/Reports.aspx';
    var _s_url_appointment = '/handler/Reports/AppointmentManager.aspx';
    var _s_url_customer = '/handler/Reports/CustomerManage.aspx';

    var _params = $.getParams();

    //--------------------------------------------------------------------------------------
    //incoming_calls（来电）
    //--------------------------------------------------------------------------------------
    var _incoming_calls = {
        guid: '<%=Guid.NewGuid()%>',
        startTime:null,
        controls:{
            combogrid: ['CH_AU_Code'],
            //textbox: ['DH_Notes'],
            combobox: ['CH_PTY_Code']//'CH_Status', 
        },
        //验证
        validate: function (o) {
            var bValid = $.form.validate(_incoming_calls.controls);
            return bValid;
        },
        //取消验证
        disableValidation: function () {
            $.form.disableValidation(_incoming_calls.controls);
        },
        //清除
        clear:function(){
            $.form.clear(_incoming_calls.controls);
        },
        //完成
        done: function(np) { 
            var o = $.form.getData("#frm_incoming_calls");
 //           alert('np=' + np);
            _incoming_calls.disableValidation();
            if (!_incoming_calls.validate(o)) {
                return;
            }

            var endTime = new Date();
            o.DH_Duration = Math.floor((endTime - _incoming_calls.startTime) / 1000);

            if (_params.CD == 1) {
                var pty_type = _incoming_calls._get_PTY_Type();
                var ch_type = '';
                var ch_st = '';
                var ch_res = '';
  //              alert('PTY=' + pty_type);
                switch (pty_type) {
                    case 1:
                        ch_type = 1;
                        ch_st = 10;
                        ch_res = 1
                        break;
                    case 2:
                        ch_type = 1;
                        ch_st = 10;
                        ch_res = 1
                        break;
                    case 3:
                        ch_type = 2;
                        ch_type = 1;
                        ch_st = 10;
                        ch_res = 1
                        break;
                    case 4:
                        ch_type = 1;
                        ch_st = 10;
                        ch_res = 1
                        break;
                    case 5:
                        ch_type = 1;
                        ch_st = 10;
                        ch_res=1
                        break;
                }
                o.CH_Type = ch_type;
            }
            if (_params.CD == 2) {
                if (_params.OT > 0) {
                    o.CH_Type = _params.OT;
                }
            }

            //DH_legacy 1:customer service to customer, 0:customer to customer service
            //CD 1:incoming , 2:calling
            o.DH_legacy = _params.CD == 2;

            $.mask.show();
            $("#btnDone,#btnMore").linkbutton('disable');
            $.post(_s_url, { o: JSON.stringify({ action: 'Save_Incoming_Calls', data: o }) }, function (res) {
                $.mask.hide();
                if ($.checkResponse(res, false)) {
                    if (_params._cmd == 'cp') {
                        _incoming_calls.clear();
                        _incoming_calls.disableValidation();
                        $.msgTips.done(true);
                    } else {
                        _incoming_calls.close(true);
                    }
                } else {
                    $.msgTips.done(false);
                    $("#btnDone,#btnMore").linkbutton('enable');
                }
            }, "json");
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
        _openWindow: function (PTY_Type) {
            var req = !((PTY_Type >= 1 && PTY_Type <= 5) || PTY_Type == 20);
            //$("#DH_Notes").textbox({ required: req });
            //$("#CH_Status").combobox({ required: req });
            //if (req) {
            //    $("#c_tr_notes,#c_tr_action").show();
            //} else {
            //    $("#c_tr_notes,#c_tr_action").hide();
            //}

            var au_code = $("#CH_AU_Code").combogrid('getValue');
            if (!req && !(au_code > 0)) {
                $.msgTips.info(_isEn ? "Please select or add the customer!" : "请选择或添加客户！");
                return;
            }
            var au_name = $.trim($(_incoming_calls.customer.id).combogrid('getText'));
            switch(PTY_Type){
                case 1:
                    $.windowOpen("/templete/usercontrol/AppointmentManager.aspx?action=Add&AU_Code=" + au_code + "&AU_Name=" + au_name, 720, 450, "AppointmentManager");
                    break;
                case 2:
                    $.windowOpen("/templete/report/CustomerManager.aspx?TI=App&AU_Code=" + au_code + "&AU_Name=" + au_name, 800, 450, "CustomerManager");
                    break;
                case 3:
                    $.windowOpen("/templete/report/CustomerManager.aspx?TI=Com&AU_Code=" + au_code + "&AU_Name=" + au_name, 800, 450, "CustomerManager");
                    break;
                case 4:
                    $.windowOpen("/templete/report/CustomerManager.aspx?TI=Vis&AU_Code=" + au_code + "&AU_Name=" + au_name, 800, 450, "CustomerManager");
                    break;
                case 5:
                    $.windowOpen("/templete/report/CustomerManager.aspx?TI=Car&AU_Code=" + au_code + "&AU_Name=" + au_name, 800, 450, "CustomerManager");
                    break;
                case 20:
                    break;
            }
        },
        _get_PTY_Type: function () {
            var pty_type = '';
            var value = $("#CH_PTY_Code").combobox("getValue");
            if (value > 0) {
                var data = $("#CH_PTY_Code").combobox("getData");
                $.each(data, function (i, d) {
                    if (d.value == value) {
                        pty_type = d.PTY_Type
                        return false;
                    }
                });
            }
            return pty_type;
        },
        action: function () {
            var pty_type = _incoming_calls._get_PTY_Type();
            _incoming_calls._openWindow(pty_type);
        },
        PhoneList: function () {
            $.post(_s_url_customer, {
                    o: JSON.stringify({
                        action: 'Get_PhoneList', PL_AU_AD_Code: $("#CH_AU_Code").combogrid('getValue')
                    })
                }, function (res) {
                    if (!$.checkResponse(res)) { res = []; }
                    var pl = '';
                    if (_params.PL > 0) {
                        pl = _params.PL;
                    } else {
                        if (res.length > 0) {
                            pl = res[0].value;
                        }
                    }

                    $("#PL_Code").combobox('loadData', res);
                    if (res.length > 0) {
                        $("#PL_Code").combobox('setValue', pl);
                    }
                }, "json");
        },
        onSelect: {
            CH_PTY_Code: function (record) {
                _incoming_calls._openWindow(record.PTY_Type);
            }
        },
        _init_person: function () {
            var $c = $("#CH_AU_Code");
            $c.combogrid({ onChange: _incoming_calls.customer.filter })
            .combogrid('textbox')
            .click(function () { $c.combogrid('showPanel'); });
        },
        init: function () {
            window.top[_incoming_calls.guid] = window.self;

            _incoming_calls._init_person();

            //customer
            if (_params.AU > 0) {
                if (!_params.AU_Name || _params.AU_Name == 'undefined') {
                    var params = { action: 'Get_CustomerName', AU_Code: _params.AU };
                    _params.AU_Name = $.ajax({
                        async: false,
                        url: _s_url_appointment,
                        data: { o: JSON.stringify(params) },
                        dataType: "json"
                    }).responseText;
                }
                $("#btnAddCustomer").hide();
                $("#CH_AU_Code").combogrid('setValue', parseInt(_params.AU))
                .combogrid('setText', _params.AU_Name).combogrid('readonly');
                _incoming_calls.buttons.enable();
            }
            _incoming_calls.bindWords();
            
        },
        bindWords: function () {
            /*
            * 4122 Call Actions
            */
            //$.getWords([4138], function (d) {
            //    if (d) {
            //        if (d._4138) {
            //            $("#CH_Status").combobox('loadData',d._4138);
            //        }
            //    }
            //});

            var dir = _params.CD == 2 ? 1 : 0;
            var o = { action: 'Get_Action', Dir: dir, Dept: 1 };
            $.post(_s_url, { o: JSON.stringify(o) }, function (res) {
                if (!$.checkResponse(res)) { res = []; }
                $("#CH_PTY_Code").combobox('loadData', res);
                $.post(_s_url_customer, {
                    o: JSON.stringify({
                        action: 'Get_PhoneList', PL_AU_AD_Code: _params.AU
                    })
                }, function (res) {
                    if (!$.checkResponse(res)) { res = []; }
                    var pl = '';
                    if (_params.PL > 0) {
                        pl = _params.PL;
                    } else {
                        if (res.length > 0) {
                            pl = res[0].value;
                        }
                    }
                   
                    $("#PL_Code").combobox('loadData', res);
                    if (res.length > 0) {
                        $("#PL_Code").combobox('setValue', pl);
                    }
                    

                    _incoming_calls.startTime = new Date();
                    _incoming_calls.bind();
                }, "json");
            }, "json");
        },
        //绑定默认值
        bindDefaults: function () {

        },
        bind: function () {
            if (!(_params.HD > 0)) { return; }
            $("#btnDone,#btnMore,#btnEditCustomer").linkbutton('disable');
            var params = { action: 'Get_IncomingCalls', HD_Code: _params.HD };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (!$.checkResponse(res)) { return; }

                $("#btnDone,#btnMore,#btnEditCustomer").linkbutton('enable');
            }, "json");
        },
        //按钮
        buttons: {
            //启用
            enable: function () {
                $("#btnDone,#btnMore,#btnEditCustomer,#btnAddAction").linkbutton('enable');
            },
            //禁用
            disable: function () {
                $("#btnDone,#btnMore,#btnAddCustomer,#btnEditCustomer,#btnAddAction").linkbutton('disable');
            }
        },
        //顾客
        customer: {
            id: '#CH_AU_Code',
            //筛选
            filter: function (newValue, oldValue) {
                if (typeof newValue === 'number') {
                    $("#btnAddCustomer").linkbutton('disable');
                    _incoming_calls.buttons.enable();
                    _incoming_calls.PhoneList();
                    return;
                } else {
                    _incoming_calls.buttons.disable();
                }

                var q = $.trim(newValue);
                if (q === '') {
                    $(_incoming_calls.customer.id).combogrid('grid').datagrid('loadData', []);
                    return;
                } else {
                    $("#btnAddCustomer").linkbutton('enable');
                }

                var o = { action: 'Get_Customer', q: q };
                $.post(_s_url_appointment, { o: JSON.stringify(o) }, function (res) {
                    if (!$.checkResponse(res)) { res = []; }
                    $(_incoming_calls.customer.id).combogrid('grid').datagrid('loadData', res);
                   
                }, "json");
            },
            //编辑
            edit:function(){
                var au_code = $("#CH_AU_Code").combogrid('getValue');
                if (au_code > 0) {
                    $.windowOpen("/templete/report/CustomerManager.aspx?action=edit&AU_Code=" + au_code, 890, 500);
                }
                $("#btnPhoneList").linkbutton('enable');
            },
            //添加
            add: function () {
                var cName = $.trim($(_incoming_calls.customer.id).combogrid('getText'));
                if (cName === '') {
                    $.msgTips.info(_isEn ? "Please input the customer name!" : "请输入客户名称！");
                    return;
                }

                $.mask.show();
                $("#btnAddCustomer").linkbutton('disable');
                var s_params = JSON.stringify({ action: 'Save_CustomerByName', AU_Name: cName });
                $.post(_s_url_appointment, { o: s_params }, function (res) {
                    $.mask.hide();
                    if ($.checkResponse(res)) {
                        $.msgTips.add(true);
                        if (res.AU_Code > 0) {
                            $(_incoming_calls.customer.id).combogrid('setValue', res.AU_Code + 0)
                            .combogrid('setText', res.AU_Name);
                        }
                    } else {
                        $.msgTips.add(false);
                        $("#btnAddCustomer").linkbutton('enable');
                    }
                }, "json");
            }
        }
    };

    $(function () {
        _incoming_calls.init();
    });
</script>