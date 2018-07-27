<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerDetails.aspx.cs" Inherits="templete_report_CustomerDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>

    <script src="/scripts/common/json2.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" type="text/css"/>
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
            width:100%;
            height:100%;
            margin:0;
            padding:0;
        }
        .tt-inner {
            display: inline-block;
            line-height: 12px;
            padding-top: 5px; 
            cursor:pointer;
        }
        .tt-inner img{
            border: 0;
        }

        .datagrid-header {
            border-width: 0;
        }

        .tabs-selected .tt-inner {
            cursor: default;
        }

        .crm-form-flow {
            overflow: hidden;
            font-size: 9pt;
            font-family: Tahoma, Verdana, 宋体;
            position: relative;
        }
        .crm-form-flow-item {
            display: inline-block;
            *display: inline;
            zoom: 1;
            margin-right: 20px;
            margin-bottom:10px;
        }
            .crm-form-flow-item span {
                font-weight: bold;
                padding-left: 2px;
                line-height: 16px;
                display: inline-block;
                color: #757575;
            }
            .crm-form-flow-item label {
                padding-left: 2px;
                line-height: 16px;
                display: inline-block;
                color: #59B044;
                background-color: #FFFFFF;
            }
        .crm-form-flow input{
            overflow: hidden;
            vertical-align: top;
            +vertical-align:middle;
            width: 16px;
            height: 16px;
            margin: 0;
            padding: 0;
        }

        

        .crm-table-flow {
            overflow: hidden;
            font-size: 9pt;
            font-family: Tahoma, Verdana, 宋体;
            position: relative;
        }

        .crm-table-flow-item {
            display: inline-block;
            *display: inline;
            zoom: 1;
            margin-right: 10px;
            margin-bottom: 10px;
        }

        .summary_contacts {
            vertical-align: middle;
            cursor: pointer;
        }

        .person td {
            padding-bottom: 10px;
        }
    </style>
</head>
<body> 
    <div id="w_email" class="easyui-window" data-options="title:'<%= Resources.CRMTREEResource.ed_contacts_type_eMail %>',minimizable:false,closed:true,modal:false" 
    style="width:500px;height:250px;">
        <div id="frm_email" class="easyui-layout" data-options="fit:true">
            <div data-options="region:'north',border:false" style="height:80px;padding:5px;">
                <input id="EX_ToEmail" class="easyui-textbox" data-options="width:'100%',readonly:true"/><br /><br />
                <input id="EX_Subject" class="easyui-textbox" data-options="required: true,width:'100%',prompt:'<%= Resources.CRMTREEResource.subject %>'"/>
            </div>
            <div data-options="region:'center',border:false" style="padding:0px 5px 5px 5px;">
                <input id="EX_Body" class="easyui-textbox" data-options="required: true,multiline:true,width:'100%',height:'100%',prompt:'<%= Resources.CRMTREEResource.body %>'"/>
            </div>
            <div data-options="region:'south',border:false" style="height:38px;text-align:right;overflow:hidden;border-top: 1px solid #B1C242;padding-top:5px;padding-right:10px;">
                <a id="btnSendEmail" class="easyui-linkbutton" data-options="onClick:_contacts.email" style="width:80px;"><%=Resources.CRMTREEResource.btnSend%></a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_contacts.close_email" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.cm_cars_buttonos_ignore%></a>
            </div>
        </div>
    </div>

    <div id="w_msg" class="easyui-window" data-options="title:'<%= Resources.CRMTREEResource.ed_contacts_type_messaging %>',minimizable:false,closed:true,modal:false" 
    style="width:500px;height:250px;">
        <div id="frm_msg" class="easyui-layout" data-options="fit:true">
            <div data-options="region:'north',border:false" style="height:45px;padding:5px;">
                <input id="ML_MC_Code" type="hidden" value="" />
                <input id="EX_ToMsg" class="easyui-textbox" data-options="width:'100%',readonly:true"/><br /><br />
            </div>
            <div data-options="region:'center',border:false" style="padding:0px 5px 5px 5px;">
                <input id="EX_Msg" class="easyui-textbox" data-options="required: true,multiline:true,width:'100%',height:'100%'"/>
            </div>
            <div data-options="region:'south',border:false" style="height:38px;text-align:right;overflow:hidden;border-top: 1px solid #B1C242;padding-top:5px;padding-right:10px;">
                <a id="btnSendMsg" class="easyui-linkbutton" data-options="onClick:_contacts.msg" style="width:80px;"><%=Resources.CRMTREEResource.btnSend%></a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_contacts.close_msg" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.cm_cars_buttonos_ignore%></a>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    //ajax地址
    var _s_url = '/handler/Reports/CustomerManage.aspx';
    //界面地址参数
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
            if (o) {
                var style = o.style ? ' style="' + o.style + '" ' : '';
                var btn = $.format([
                        '<a class="easyui-linkbutton l-btn ' + (o.size ? o.size : 'l-btn-small') + ' l-btn-plain" ' + (o.title ? 'title="{title}"' : '')
                        , ' href="javascript:void(0);" ' + (o.clickFun ? 'onclick="{clickFun}"' : '') + '>'
                        , o.icon ? '<span class="l-btn-left l-btn-icon-left">' : '<span class="l-btn-left">'
                        , o.text ? '<span class="l-btn-text" ' + style + '>{text}</span>' : '<span class="l-btn-text l-btn-empty">&nbsp;</span>'
                        , o.icon ? '<span class="l-btn-icon {icon}">&nbsp;</span>' : ''
                        , '</span></a>'
                ].join(''), o);
                a_btns.push(btn);
            }
        });
        return a_btns;
    }

    //--------------------------------------------------------------------------------------
    //contacts（联系方式）
    //--------------------------------------------------------------------------------------
    var _contacts = {};
    _contacts.language = {
        type: {
            'address': '<%=Resources.CRMTREEResource.ed_contacts_type_address%>',
            'phone': '<%=Resources.CRMTREEResource.ed_contacts_type_phone%>',
            'eMail': '<%=Resources.CRMTREEResource.ed_contacts_type_eMail%>',
            'messaging': '<%=Resources.CRMTREEResource.ed_contacts_type_messaging%>'
        }, Title: {
            GX: '<%=Resources.CRMTREEResource.CLT_GUANXI%>',
            XM: '<%=Resources.CRMTREEResource.CLT_NAME%>',
            FS: '<%=Resources.CRMTREEResource.CLT_LIANXIFANGSHI%>',
            XX: '<%=Resources.CRMTREEResource.CLT_XINXI%>',
        }
    };
    //创建联系方式表格
    _contacts.create = function () {
        var $dg = $('#dg_contacts');
        $dg.datagrid({
            url: null,
            fit: true,
            toolbar: '#tb_contact',
            rownumbers: true,
            singleSelect: true,
            showHeader: true,
            border: false,
            loadMsg: '',
            columns: [[
                {
                    field: 'name_1', title: _contacts.language.Title.XM, width: 100,
                    formatter: function (value, row) {
                        return row.AU_Name;
                    }
                },
                {
                    field: 'relation_1',
                    title: _contacts.language.Title.GX, width: 100,
                    formatter: function (value, row) {
                        return _contacts.format_data(row.relation_id);
                    }
                }, {
                    field: 'contact_type',
                    title: _contacts.language.Title.FS,width: 100,
                    formatter: function (value, row) {
                        return _contacts.Typeformat_data(row.relation_type);
                    }
                },
                {
                    field: 'info',
                    title: _contacts.language.Title.XX,width: 400,
                    formatter: function (value, row) {
                        return _contacts.infoformat_data(row.relation_type, row.contact_id, row.info);
                    }
                },
                {
                    field: 'btnLast', align: 'center', width: 60,
                    formatter: function (value, row, index) {
                        var a_o = [];
                        //邮件
                        if (row.relation_type == 2) {
                            a_o = [
                            {
                                text: '<%=Resources.CRMTREEResource.Calling%>',
                                clickFun: '_contacts.call(event,this);',
                                style: 'color:blue;text-decoration:underline;'
                            }
                            ];
                        }
                        //短信
                        if (row.relation_type == 3 || row.relation_type == 4) {
                            a_o = [
                                {
                                    text: '<%=Resources.CRMTREEResource.contact%>',
                                    clickFun: '_contacts.contact(event,this);',
                                    style: 'color:blue;text-decoration:underline;'
                                }
                            ];
                        } 
                        return setBtns(a_o, true).join('');
                    }
                }
            ]]
        });
    };
    _contacts.emailControls = {
        textbox: ['EX_ToEmail', 'EX_Subject', 'EX_Body']
    };
    _contacts.msgControls = {
        textbox: ['EX_ToMsg', 'EX_Msg']
    };
    _contacts.email = function (e, target) {
        var bValid = $.form.validate(_contacts.emailControls);
        if (bValid) {
            var o = $.form.getData("#frm_email");
             
            //$("#btnSendEmail").linkbutton('disable');
            var s_params = JSON.stringify({ action: 'Send_Email', email: o });
            $.post(_s_url, { o: s_params }, function (res) {
                //$("#btnSendEmail").linkbutton('enable');
                if ($.checkResponse(res)) {
                    $.msgTips.send(true);
                } else {
                    $.msgTips.send(false);
                }
            }, "json");
            $("#w_email").window('close');
        }
    };
    _contacts.close_email = function (e, target) {
        $("#w_email").window('close');
    };
    _contacts.msg = function (e, target) {
        var bValid = $.form.validate(_contacts.msgControls);
        if (bValid) {
            var o = $.form.getData("#frm_msg");

            var action = '';
            //微信
            if (o.ML_MC_Code == 2) {
                action = 'Send_Msg';
            }

            //手机短信
            if (o.ML_MC_Code == 1) {
                action = 'Send_Msg_Phone';
            }
            
            if (action == '') { return;}

            //$("#btnSendMsg").linkbutton('disable');
            var s_params = JSON.stringify({ action: action, msg: o });
            $.post(_s_url, { o: s_params }, function (res) {
                //$("#btnSendMsg").linkbutton('enable');
                if ($.checkResponse(res)) {
                    $.msgTips.send(true);
                } else {
                    $.msgTips.send(false);
                }
            }, "json");
            $("#w_msg").window('close');
        }
    };
    _contacts.close_msg = function (e, target) {
        $("#w_msg").window('close');
    };
    _contacts.contact = function (e, target) {
        var $dg = $("#dg_contacts");
        var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
        var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
        if (rowData) {
            if (rowData.relation_type == 3) {
                $.form.disableValidation(_contacts.emailControls);
                $.form.clear(_contacts.emailControls);
                $("#EX_ToEmail").textbox('setValue', $.trim(rowData.info));
                $("#w_email").window({ title: rowData.info }).window('open');
            }else if (rowData.relation_type == 4) {
                $.form.disableValidation(_contacts.msgControls);
                $.form.clear(_contacts.msgControls);
                $("#ML_MC_Code").val(rowData.Keys);
                $("#EX_ToMsg").textbox('setValue', $.trim(rowData.info));
                $("#w_msg").window({ title: rowData.info }).window('open');
            }
        }
        stopPropagation(e);
    };
    _contacts.call = function (e, target) {
        var $dg = $("#dg_contacts");
        var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
        var rowData = $dg.datagrid('selectRow', rowIndex);
//        $("#btnIncoming").click();
        $("#btnCalling").click();

        stopPropagation(e);
    };
    //排序
    _contacts.sort = function (contacts) {
        var address = [], phone = [], email = [], messaging = [];
        $.each(contacts.address, function (i, info) {
            address.push({ type: '1', id: info.AL_Code, type_Text: _contacts.language.type.address, o: info });
        });
        $.each(contacts.phone, function (i, info) {
            phone.push({ type: '2', id: info.PL_Code, type_Text: _contacts.language.type.phone, o: info });
        });
        $.each(contacts.email, function (i, info) {
            email.push({ type: '3', id: info.EL_Code, type_Text: _contacts.language.type.eMail, o: info });
        });
        $.each(contacts.messaging, function (i, info) {
            messaging.push({ type: '4', id: info.ML_Code, type_Text: _contacts.language.type.messaging, o: info });
        });

        var data = address.concat(phone, email, messaging);
        data = data.sort(function (a, b) {
            return a.o.pref > b.o.pref ? 1 : -1;
        });

        return data;
    }
    /* Create Wangqi Date:2015/04/24
    */
    _contacts.format_data = function (data) {
        var _info;
        $.each(_contacts.GetWords, function (i, info) {
            if (info.value == data) {
                _info = info.text;
            }
        });
        return _info;
    }
    _contacts.Typeformat_data = function (data) {
        var _text;
        if (data == "1") {
            _text = _contacts.language.type.address
        } else if (data == "2") {
            _text = _contacts.language.type.phone
        } else if (data == "3") {
            _text = _contacts.language.type.eMail
        } else if (data == "4") {
            _text = _contacts.language.type.messaging
        }
        return _text;
    }
    _contacts.infoformat_data = function (type, id, data) {
        if (type === undefined) return;
        if (id === undefined) return;
        //if (data === undefined) return;
        //if (data === undefined) return;
        var _w;
        var _info;
        if (type == 1) {
            _w = _contacts.info._words["_55"];
        } else if (type == 2) {
            _w = _contacts.info._words["_47"];
        } else if (type == 3) {
            _w = _contacts.info._words["_44"];
        } else if (type == 4) {
            _w = _contacts.info._words["_4064"];
        }
        $.each(_w, function (i, info) {
            if (info.value == id) {
                _info = $.trim(info.text) + ":" + $.trim(data);
            }
        });
        return _info;
    }
    //---------------------------
    //联系信息
    //---------------------------
    _contacts.info = {};
    //联系方式分类数据
    _contacts.info._words = {};
    //默认值
    _contacts.info._words_selected = {};
    //通讯分类数据
    _contacts.info._CT_Messaging_Carriers = [];
    //创建联系信息表格
    _contacts.info.createDataGrid = function ($info, opts) {
        opts = opts ? opts : {};
        $info.combo({ panelHeight: opts.panelHeight, panelWidth: opts.panelWidth });
        var $c = $('<table></table>').appendTo($info.combo('panel'));
        var row = $("#dg_contacts").datagrid('getSelected');
        var data = row.o ? row.o : (opts.data ? opts.data : {});
        $.each(data, function (n, v) {
            if (typeof v === 'string')
                data[n] = $.trim(v);
        });
        $c.datagrid({
            url: null,
            fit: true,
            singleSelect: true,
            border: false,
            data: [data],
            loadMsg: '',
            columns: [opts.columns],
            onBeginEdit: opts.onBeginEdit
        });
        _contacts.info.$dg = $c;
    };
    //地址
    _contacts.info.address = function ($info) {
        var language = {
            AL_Type: '<%=Resources.CRMTREEResource.em_contacts_AL_Type%>',
            AL_Add1: '<%=Resources.CRMTREEResource.em_contacts_AL_Add1%>',
            AL_Add2: '<%=Resources.CRMTREEResource.em_contacts_AL_Add2%>',
            AL_District: '<%=Resources.CRMTREEResource.em_contacts_AL_District%>',
            AL_City: '<%=Resources.CRMTREEResource.em_contacts_AL_City%>',
            AL_State: '<%=Resources.CRMTREEResource.em_contacts_AL_State%>',
            AL_Zip: '<%=Resources.CRMTREEResource.em_contacts_AL_Zip%>'
        };
        var defaultValue = _contacts.info._words_selected["_55"];
        var data = defaultValue ? { AL_Type: defaultValue } : {};
        _contacts.info.createDataGrid($info, {
            panelHeight: 100,
            panelWidth: 660,
            data: data,
            columns: [
                {
                    field: 'AL_Type', title: language.AL_Type, width: 80,
                    formatter: function (value, row) {
                        return row.AL_Type_Text ? row.AL_Type_Text : value;
                    }
                },
                {
                    field: 'AL_Add1', title: language.AL_Add1, width: 100
                },
                {
                    field: 'AL_Add2', title: language.AL_Add2, width: 100
                },
                {
                    field: 'AL_District', title: language.AL_District, width: 100
                },
                {
                    field: 'AL_City', title: language.AL_City, width: 100
                },
                {
                    field: 'AL_State', title: language.AL_State, width: 100
                },
                {
                    field: 'AL_Zip', title: language.AL_Zip, width: 60
                }
            ]
        });
    };
    //电话
    _contacts.info.phone = function ($info) {
        var language = {
            PL_Type: '<%=Resources.CRMTREEResource.em_contacts_PL_Type%>',
            PL_Number: '<%=Resources.CRMTREEResource.em_contacts_PL_Number%>'
        };
        var defaultValue = _contacts.info._words_selected["_47"];
        var data = defaultValue ? { PL_Type: defaultValue } : {};
        _contacts.info.createDataGrid($info, {
            panelHeight: 90,
            panelWidth: 400,
            data: data,
            columns: [
            {
                field: 'PL_Type', title: language.PL_Type, width: 130,
                formatter: function (value, row) {
                    return row.PL_Type_Text ? row.PL_Type_Text : value;
                }
            },
            {
                field: 'PL_Number', title: language.PL_Number, width: 240
            }
            ]
        });
    };
    //邮件
    _contacts.info.email = function ($info) {
        var language = {
            EL_Type: '<%=Resources.CRMTREEResource.em_contacts_EL_Type%>',
            EL_Address: '<%=Resources.CRMTREEResource.em_contacts_EL_Address%>'
        };
        var defaultValue = _contacts.info._words_selected["_44"];
        var data = defaultValue ? { EL_Type: defaultValue } : {};
        _contacts.info.createDataGrid($info, {
            panelHeight: 90,
            panelWidth: 400,
            data: data,
            columns: [
            {
                field: 'EL_Type', title: language.EL_Type, width: 130, 
                formatter: function (value, row) {
                    return row.EL_Type_Text ? row.EL_Type_Text : value;
                }
            },
            {
                field: 'EL_Address', title: language.EL_Address, width: 240
            }
            ]
        });
    };
    //消息
    _contacts.info.messaging = function ($info) {
        var language = {
            ML_MC_Code: '<%=Resources.CRMTREEResource.em_contacts_ML_MC_Code%>',
            ML_Handle: '<%=Resources.CRMTREEResource.em_contacts_ML_Handle%>'
        };
        var defaultValue = _contacts.info._CT_Messaging_Carriers.length > 0 ? _contacts.info._CT_Messaging_Carriers[0].value : '';
        var data = defaultValue ? { ML_MC_Code: defaultValue } : {};
        _contacts.info.createDataGrid($info, {
            panelHeight: 90,
            panelWidth: 400,
            data: data,
            columns: [
            {
                field: 'ML_MC_Code', title: language.ML_MC_Code, width: 130, 
                formatter: function (value, row) {
                    return row.ML_MC_Code_Text ? row.ML_MC_Code_Text : value;
                }
            },
            {
                field: 'ML_Handle', title: language.ML_Handle, width: 240
            }
            ]
        });
    };
    //格式化联系信息
    _contacts.info.format = function (type, row) {
        if (!row) return "";
        var a_info = [];
        switch (type) {
            case '1':
                a_info = [
                    $.trim(row.AL_Type_Text),
                    ':',
                    $.trim(row.AL_Add1),
                    $.trim(row.AL_Add2),
                    $.trim(row.AL_District),
                    $.trim(row.AL_City),
                    $.trim(row.AL_State),
                    $.trim(row.AL_Zip)
                ];
                break;
            case '2':
                a_info = [
                    $.trim(row.PL_Type_Text),
                    ':',
                    $.trim(row.PL_Number)
                ];
                break;
            case '3':
                a_info = [
                    $.trim(row.EL_Type_Text),
                    ':',
                    $.trim(row.EL_Address)
                ];
                break;
            case '4':
                a_info = [
                    $.trim(row.ML_MC_Code_Text),
                    ':',
                    $.trim(row.ML_Handle)
                ];
                break;
        }
        var s_info = $.trim(a_info.join(' '));
        return s_info == ':' ? '' : s_info;
    };
    //格式化简介联系信息
    _contacts.info.formatSummary = function (type, row) {
        if (!row) return "";
        var a_info = [];
        switch (type) {
            case '1':
                a_info = [
                    $.trim(row.AL_Add1),
                    $.trim(row.AL_Add2),
                    $.trim(row.AL_District),
                    $.trim(row.AL_City),
                    $.trim(row.AL_State),
                    $.trim(row.AL_Zip)
                ];
                break;
            case '2':
                a_info = [
                    $.trim(row.PL_Number)
                ];
                break;
            case '3':
                a_info = [
                    $.trim(row.EL_Address)
                ];
                break;
            case '4':
                a_info = [
                    $.trim(row.ML_Handle)
                ];
                break;
        }
        return s_info = $.trim(a_info.join(' '));
    };

    //--------------------------------------------------------------------------------------
    //cars（车信息）
    //--------------------------------------------------------------------------------------
    var _cars = {};
    //内部行索引
    _cars._rowIndex = -1;
    _cars.language = {
        datagrid: {
            Cars: '<%=Resources.CRMTREEResource.cm_cars_datagrid_Cars%>',
            Last_Service: '<%=Resources.CRMTREEResource.cm_cars_datagrid_Last_Service%>',
            Next_Service: '<%=Resources.CRMTREEResource.cm_cars_datagrid_Next_Service%>'
        }
    };
    //创建
    _cars.create = function () {
        $('#dg_cars').datagrid({
            url: null,
            fit: true,
            rownumbers: true,
            singleSelect: true,
            border: false,
            loadMsg: '',
            columns: [[
                {
                    field: 'Cars', title: _cars.language.datagrid.Cars, width: 300
                },
                {
                    field: 'Last_Service', title: _cars.language.datagrid.Last_Service, width: 80
                },
                {
                    field: 'Next_Service', title: _cars.language.datagrid.Next_Service, width: 200
                }
            ]]
        });
    };
    //内部联动
    _cars._lendon = function (carInfo, res) {
        if (carInfo && res) {
            $("#MK_Code").combobox('select', '');

            $("#MK_Code").combobox('setValue', carInfo.MK_Code);

            $("#CM_Code").initSelect({
                data: res.CT_Car_Model
                , onSelect: _cars.onSelect.CM_Code
            }).combobox('setValue', carInfo.CM_Code);

            $("#CI_CS_Code").initSelect({ data: res.CT_Car_Style }).combobox('setValue', carInfo.CI_CS_Code);
        }
    };
    //格式化消息
    _cars.format = function (row) {
        return [
            $.trim($("#CI_YR_Code").combobox('getText'))
            , $.trim($("#MK_Code").combobox('getText'))
            , $.trim($("#CM_Code").combobox('getText'))
            , $.trim($("#CI_CS_Code").combobox('getText')) + '[' + $.trim($("#CI_Color_E").combobox('getText')) + ']' + '[' + $.trim(row['CI_Licence']) + ']'
        ].join(', ');
    };
    //联动事件
    _cars.onSelect = {};
    //生产商联动
    _cars.onSelect.MK_Code = function (record) {
        $("#CM_Code,#CI_CS_Code").combobox('clear').initSelect();
        if (record.value > 0) {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Car_Model', id: record.value }) }, function (res) {
                $("#CM_Code").initSelect({ data: res, onSelect: _cars.onSelect.CM_Code });
            }, "json");
        }
    };
    //车型联动
    _cars.onSelect.CM_Code = function (record) {
        $("#CI_CS_Code").combobox('clear').initSelect();
        if (record.value > 0) {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Car_Style', id: record.value }) }, function (res) {
                $("#CI_CS_Code").initSelect({ data: res });
            }, "json");
        }
    };
    //图片
    _cars.picture = {};
    //创建图片
    _cars.picture.create = function (src) {
        if (src.indexOf('/') === -1) {
            $('<div class="picItem car_img"><div _fileName="' + src + '" class="remove" title="' + _cars.language.buttons.remove + '"></div><img src="/plupload/customer/' + src + '"/></div>')
            .appendTo("#ci_container_car");
        } else {
            $('<div class="picItem car_img' + (src.indexOf('/customer_temp/') !== -1 ? ' _add' : '') + '"><div class="remove"></div><img src="' + src + '"/></div>').appendTo("#ci_container_car");
        }
    };
    //获得图片路径
    _cars.picture.getSrcs = function () {
        return $("#ci_container_car .car_img>img").map(function () {
            return $(this).attr('src');
        }).get().join(',');
    };
    //获得图片名称
    _cars.picture.get = function () {
        return $("#ci_container_car ._add>img").map(function () {
            var src = $(this).attr('src');
            var srcs = src.split('/');
            return srcs[srcs.length - 1];
        }).get().join(',');
    };
    //设置图片
    _cars.picture.set = function (imgSrcs, bAdd) {
        $("#ci_container_car .car_img").detach();
        if (imgSrcs) {
            var a_img_srcs = imgSrcs.split(',');
            $.each(a_img_srcs, function (i, src) {
                if ($.trim(src)) {
                    _cars.picture.create(src, bAdd);
                }
            });
        }
    };

    //--------------------------------------------------------------------------------------
    //summary（简介信息）
    //--------------------------------------------------------------------------------------
    var _summary = {};
    _summary.language = {
        name: '<%=Resources.CRMTREEResource.cm_summary_name%>',
        active: '<%=Resources.CRMTREEResource.cm_summary_active%>',
        inActive: '<%=Resources.CRMTREEResource.cm_summary_inActive%>',
        nextAppointment: '<%=Resources.CRMTREEResource.cm_summary_nextAppointment%>',
        appointmentForCar: '<%=Resources.CRMTREEResource.cm_summary_appointmentForCar%>',
        numberCars: '<%=Resources.CRMTREEResource.cm_summary_numberCars%>',
        totalNumberVisits: '<%=Resources.CRMTREEResource.cm_summary_totalNumberVisits%>',
        lastVisit: '<%=Resources.CRMTREEResource.cm_summary_lastVisit%>',
        visitForCar: '<%=Resources.CRMTREEResource.cm_summary_visitForCar%>'
    };
    //创建
    _summary.create = function (name, summary) {
        if (summary) {
            var html = '<div class="crm-form-flow-item">'
            + '    <span>' + _summary.language.name + ' :</span>'
            + '    <label>' + $.trim(name) + '</label>'
            + '</div>'
            + '<div class="crm-form-flow-item">'
            + '    <span>' + _summary.language.active + ' :</span>'
            + (
            $.trim(summary.Active) == '1' ?
            ('    <img src="/images/Customer/active.png?v=1" title="' + _summary.language.active + '" style="vertical-align:-3px;"/>'
            + ' (Activated on ' + summary.EX_AU_Activate_dt + ')')
            :
            '    <img src="/images/Customer/inactive.png?v=1" title="' + _summary.language.inActive + '" style="vertical-align:-6px;"/>'
            ) + '</div>';

            var shopInfo = $.trim(summary.ShopInfo);
            html += shopInfo ?
            '<div class="crm-form-flow-item">'
            + '    <img src="/images/Customer/repair_car.png" title="' + shopInfo + '" style="vertical-align:-3px;"/>'
            + '</div>' : '';

            html += '<br />'
            + '<div class="crm-form-flow-item">'
            + '    <span>' + _summary.language.nextAppointment + ' :</span>'
            + '    <label>' + $.trim(summary.Next_Appointment) + '</label>'
            + '</div>'
            + '<div class="crm-form-flow-item">'
            + '    <span>' + _summary.language.appointmentForCar + ' :</span>'
            + '    <label>' + $.trim(summary.Next_Appointment_Car) + '</label>'
            + '</div>'
            + '<br />'
            + '<div class="crm-form-flow-item">'
            + '    <span>' + _summary.language.numberCars + ' :</span>'
            + '    <label>' + $.trim(summary.CarNo) + '</label>'
            + '</div>'
            + '<br />'
            + '<div class="crm-form-flow-item">'
            + '    <span>' + _summary.language.totalNumberVisits + ' :</span>'
            + '    <label>' + $.trim(summary.VisitsNo) + '</label>'
            + '</div>'
            + '<div class="crm-form-flow-item">'
            + '    <span>' + _summary.language.lastVisit + ' :</span>'
            + '    <label>' + $.trim(summary.Last_Visit) + '</label>'
            + '</div>'
            + '<div class="crm-form-flow-item">'
            + '    <span>' + _summary.language.visitForCar + ' :</span>'
            + '    <label>' + $.trim(summary.Last_Visit_Car) + '</label>'
            + '</div>';
            $("#summaryInfo").show().html(html).after('<div style="border-bottom: 1px solid #E4E4E4;padding-top:15px;"></div>');
        }
    };
    //----------------------------------
    //_summary.contacts（简介联系方式）
    //----------------------------------
    _summary.contacts = {};
    _summary.contacts.language = {
        address: '<%=Resources.CRMTREEResource.ed_contacts_type_address%>',
        phone: '<%=Resources.CRMTREEResource.ed_contacts_type_phone%>',
        email: '<%=Resources.CRMTREEResource.ed_contacts_type_eMail%>',
        messaging: '<%=Resources.CRMTREEResource.ed_contacts_type_messaging%>'
    };
    //排序
    _summary.contacts.sort = function (contacts) {
        var data = [];

        var len = contacts.address.length;
        if (len > 0) {
            var address = contacts.address.sort(function (a, b) {
                return a.pref < b.pref ? 1 : -1;
            })[len - 1];
            data.push({
                type: '1', id: address.AL_Code, type_Text: _summary.contacts.language.address,
                o: address
            });
        }

        len = contacts.phone.length;
        if (len > 0) {
            var phone = contacts.phone.sort(function (a, b) {
                return a.pref < b.pref ? 1 : -1;
            })[len - 1];
            data.push({
                type: '2', id: phone.PL_Code, type_Text: _summary.contacts.language.phone,
                o: phone
            });
        }

        len = contacts.email.length;
        if (len > 0) {
            var email = contacts.email.sort(function (a, b) {
                return a.pref < b.pref ? 1 : -1;
            })[len - 1];
            data.push({
                type: '3', id: email.EL_Code, type_Text: _summary.contacts.language.email,
                o: email
            });
        }

        len = contacts.messaging.length;
        if (len > 0) {
            var messaging = contacts.messaging.sort(function (a, b) {
                return a.pref < b.pref ? 1 : -1;
            })[len - 1];
            data.push({
                type: '4', id: messaging.ML_Code, type_Text: _summary.contacts.language.messaging,
                o: messaging
            });
        }

        data = data.sort(function (a, b) {
            return a.o.pref > b.o.pref ? 1 : -1;
        });

        return data;
    };
    //创建
    _summary.contacts.create = function (a_contacts) {
        if (a_contacts) {
            var contacts = _summary.contacts.sort(a_contacts);
            var $dg = $("#dg_summary_contacts");
            var s_html = '';
            $.each(contacts, function (i, o) {
                s_html +=
                [
                '<div class="crm-table-flow-item" style="width:80px;font-weight:bold;">'
                , o.type_Text
                , '</div>'
                , '<div class="crm-table-flow-item" style="width:40px;">'
               , _summary.contacts.getContactsButton(o.type)
                , '</div>'
                , '<div class="crm-table-flow-item">'
                , _summary.contacts.getContactTypeText(o.type, o.o)
                , '</div>'
                , '<div class="crm-table-flow-item" style="color: #59B044;background-color: #FFFFFF;">'
                , _contacts.info.formatSummary(o.type, o.o)
                , '</div>'
                , '<br />'
                ].join('');
            });
            $dg.html(s_html);
        }
    };
    //联系按钮
    _summary.contacts.getContactsButton = function (type) {
        var btn = '';
        switch (type) {
            case '1':
                btn = '<img src="/images/Customer/address.png" title="' + _summary.contacts.language.address + '" class="summary_contacts"/>';
                break;
            case '2':
                btn = '<img src="/images/Customer/phone.png" title="' + _summary.contacts.language.phone + '" class="summary_contacts"/>';
                break;
            case '3':
                btn = '<img src="/images/Customer/email.png" title="' + _summary.contacts.language.email + '" class="summary_contacts"/>';
                break;
            case '4':
                btn = '<img src="/images/Customer/message.png" title="' + _summary.contacts.language.messaging + '" class="summary_contacts"/>';
                break;
        }
        return btn;
    };
    //联系文本
    _summary.contacts.getContactTypeText = function (type, row) {
        var text = '';
        switch (type) {
            case '1':
                text = $.trim(row.AL_Type_Text);

                break;
            case '2':
                text = $.trim(row.PL_Type_Text);
                break;
            case '3':
                text = $.trim(row.EL_Type_Text);
                break;
            case '4':
                text = $.trim(row.ML_MC_Code_Text);
                break;
        }
        return text ? text + ' : ' : '';
    };

    //--------------------------------------------------------------------------------------
    //personal（个人信息）
    //--------------------------------------------------------------------------------------
    var _personal = {};
    //验证
    _personal.validate = function () {
        return $.form.validate({
            textbox: ['AU_Name', 'AU_ID_No', 'AU_Dr_Lic']
        });
    };
    //取消验证
    _personal.disableValidation = function () {
        $.form.disableValidation({
            textbox: ['AU_Name', 'AU_ID_No', 'AU_Dr_Lic']
        });
    };
    //设置
    _personal.set = function (personal) {
        $.form.setData('#frm_personal', personal);
    }
    //获取
    _personal.get = function () {
        var data = $.form.getData("#frm_personal");
        if ($.trim(data.AU_Gender) != '') {
            data.AU_Gender = (data.AU_Gender == 1);
        }
        return data;
    }
    _personal.animate = function (au_type) {
        if (au_type === '' || au_type == 0) {
            $("#frm_personal tr.tr_item").show();
        } else {
            $("#frm_personal tr.tr_item").hide();
        }
    }

    var _employeeDetails = {
        //关闭窗体
        close: function () {
            if (window._closeOwnerWindow) { window._closeOwnerWindow(); }
        },
        addAppointment: function () {
            window.top.$.topOpen({
                url: "/templete/usercontrol/AppointmentManager.aspx?Action=Add&AU_Name=" + _params.AU_Name + "&AU_Code=" + _params.AU_Code + "&_winID=<%=Guid.NewGuid()%>",
                width: 700,
                height: 400
            });
            _employeeDetails.close();
        }
    };



    //设置打开窗体
    _setWindow = function (win) {
        if (win) {
            var opts = win.window('options');
            opts.title = '<%=Resources.CRMTREEResource.cd_window_title%>';
            win.window(opts);
        }
    }

    $(function () {
        //获得界面HTML
        function getHTML() {
            var language = {
                tabs: {
                    summary: '<%=Resources.CRMTREEResource.cm_tabs_summary%>',
                    contacts: '<%=Resources.CRMTREEResource.cm_tabs_contacts%>',
                    personal: '<%=Resources.CRMTREEResource.cm_tabs_personal%>',
                    associates: '<%=Resources.CRMTREEResource.cm_tabs_Assc%>',
                    communications: '<%=Resources.CRMTREEResource.cm_tabs_communications%>',
                    preferences: '<%=Resources.CRMTREEResource.cm_tabs_preferences%>',
                    cars: '<%=Resources.CRMTREEResource.cm_tabs_cars%>',
                    visits: '<%=Resources.CRMTREEResource.cm_tabs_visits%>',
                    appointMent: '<%=Resources.CRMTREEResource.cm_tabs_appointment%>'
                },
                buttons: {
                    close: '<%=Resources.CRMTREEResource.cm_buttons_close%>',
                    addAppointment: '<%=Resources.CRMTREEResource.cd_buttons_addAppointment %>'
                },
                personal: {
                    id: '<%=Resources.CRMTREEResource.cm_personal_id%>',
                    name: '<%=Resources.CRMTREEResource.cm_personal_name%>',
                    gender: '<%=Resources.CRMTREEResource.cm_personal_gender%>',
                    birthday: '<%=Resources.CRMTREEResource.cm_personal_birthday%>',
                    idType: '<%=Resources.CRMTREEResource.cm_personal_idType%>',
                    idNo: '<%=Resources.CRMTREEResource.cm_personal_idNo%>',
                    driverLicense: '<%=Resources.CRMTREEResource.cm_personal_driverLicense%>',
                    education: '<%=Resources.CRMTREEResource.cm_personal_education%>',
                    occupation: '<%=Resources.CRMTREEResource.cm_personal_occupation%>',
                    industry: '<%=Resources.CRMTREEResource.cm_personal_industry%>',
                    type: '<%=Resources.CRMTREEResource.cm_personal_type%>'
                },
                cars: {
                    buttonos: {
                        accept: '<%=Resources.CRMTREEResource.cm_cars_buttonos_accept%>',
                        ignore: '<%=Resources.CRMTREEResource.cm_cars_buttonos_ignore%>'
                    },
                    title: '<%=Resources.CRMTREEResource.cm_cars_title%>',
                    form: {
                        Make: '<%=Resources.CRMTREEResource.cm_cars_form_Make%>',
                        Model: '<%=Resources.CRMTREEResource.cm_cars_form_Model%>',
                        VIN: '<%=Resources.CRMTREEResource.cm_cars_form_VIN%>',
                        Style: '<%=Resources.CRMTREEResource.cm_cars_form_Style%>',
                        Mileage: '<%=Resources.CRMTREEResource.cm_cars_form_Mileage%>',
                        Years: '<%=Resources.CRMTREEResource.cm_cars_form_Years%>',
                        Licence: '<%=Resources.CRMTREEResource.cm_cars_form_Licence%>',
                        ColorExternal: '<%=Resources.CRMTREEResource.cm_cars_form_ColorExternal%>',
                        ColorInternal: '<%=Resources.CRMTREEResource.cm_cars_form_ColorInternal%>'
                    }
                }

            };
            if (!_isEn) {
                $.each(language.personal, function (n, v) {
                    language.personal[n] = v + '：';
                });
                $.each(language.cars.form, function (n, v) {
                    language.cars.form[n] = v + '：';
                });
            } else {
                $.each(language.personal, function (n, v) {
                    language.personal[n] = v + ':';
                });
                $.each(language.cars.form, function (n, v) {
                    language.cars.form[n] = v + ':';
                });
            }

            var html = '<div class="easyui-layout" data-options="fit:true">'
+ '        <div data-options="region:\'center\',border:false" style="padding:0px;padding-top:5px;">'
+ '            <div id="tabs" class="easyui-tabs" data-options="fit:true,selected:0,plain:true,border:false" style="border-bottom: 1px solid #B1C242;">'
+ '                <div title="' + language.tabs.summary + '" data-options="id:\'sum\',iconCls:\'icon-customer-summary\'" style="padding:10px">'
+ '                    <div class="easyui-layout" data-options="fit:true">'
+ '                        <div data-options="region:\'north\',height:150,border:false" style="padding: 10px;">'
+ '                            <div id="summaryInfo" class="crm-form-flow" style="display:none;">'
+ '                            </div>'
+ '                        </div>'
+ '                        <div data-options="region:\'center\',border:false" style="padding:10px;">'
+ '                            <div class="easyui-panel" data-options="fit:true,border:false">'
+ '                                <div id="dg_summary_contacts" class="crm-table-flow">'
+ '                                </div>'
+ '                            </div>'
+ '                        </div>'
+ '                    </div>'
+ '                </div>'
+ '                <div title="' + language.tabs.contacts + '" data-options="id:\'con\',iconCls:\'icon-customer-contacts\'" style="padding:0px 0px 1px 0px">'
+ '                    <div class="easyui-panel" data-options="fit:true,border:false">'
+ '                        <table id="dg_contacts"></table>'
+ '                    </div>'
+ '                </div>'
+ '                <div title="' + language.tabs.personal + '" data-options="id:\'per\',iconCls:\'icon-customer-personal\'" style="padding:10px">'
+ '                    <div class="easyui-panel" data-options="fit:true,border:false" style="position:relative;">'
+ '                        <table id="frm_personal" class="form person" border="0" cellpadding="3" cellspacing="2">'
+ '                            <tr>'
+ '                                <td class="text" style="width:100px;">' + language.personal.name + '</td>'
+ '                                <td colspan="3">'
+ '                                    <input id="AU_Code" type="hidden" value="0" />'
+ '                                    <input id="AU_Name" class="easyui-textbox fluid" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.type + '</td>'
+ '                                <td colspan="3">'
+ '                                    <input id="AU_Type" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
+ '                                <td class="text">' + language.personal.gender + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Gender" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.birthday + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_B_date" class="easyui-datebox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
+ '                                <td class="text">' + language.personal.idType + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_ID_Type" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.idNo + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_ID_No" class="easyui-textbox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
+ '                                <td class="text">' + language.personal.driverLicense + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Dr_Lic" class="easyui-textbox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
+ '                                <td class="text">' + language.personal.education + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Education" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
+ '                                <td class="text">' + language.personal.occupation + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Occupation" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.industry + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Industry" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                        </table>'
+ '                    </div>'
+ '                </div>'
+ '                <div title="' + language.tabs.associates + '" data-options="id:\'ass\',iconCls:\'icon-customer-associates\'">'
+ '                </div>'
+ '                <div title="' + language.tabs.communications + '" data-options="id:\'com\',iconCls:\'icon-customer-communications\'">'
+ '                </div>'
+ '                <div title="' + language.tabs.preferences + '" data-options="id:\'pre\',iconCls:\'icon-customer-preferences\'" style="padding:5px">'
+ '                </div>'
+ '                <div title="' + language.tabs.cars + '" data-options="id:\'car\',iconCls:\'icon-customer-cars\'">'
+ '                </div>'
//+ '                <div title="' + language.tabs.cars + '" data-options="id:\'car\',iconCls:\'icon-customer-cars\'" style="padding:0px 0px 1px 0px">'
//+ '                    <div class="easyui-panel" data-options="fit:true,border:false">'
//+ '                        <table id="dg_cars"></table>'
//+ '                    </div>'
//+ '                </div>'
+ '                <div title="' + language.tabs.visits + '" data-options="id:\'vis\',iconCls:\'icon-customer-visits\'">'
+ '                </div>'
+ '                <div title="' + language.tabs.appointMent + '" data-options="id:\'app\',iconCls:\'icon-customer-visits\'">'
+ '                </div>'
+ '            </div>'
+ '        </div> '
+ '        <div data-options="region:\'south\',border:false" style="height:auto;text-align:right;padding:10px;">'
+ '            <a id="btnIncoming" class="easyui-linkbutton" data-options="iconCls:\'icon-incoming\'" style="width:80px;"><%=Resources.CRMTREEResource.Incoming%></a>'
+ '            <a id="btnCalling" class="easyui-linkbutton" data-options="iconCls:\'icon-calling\'" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.Calling%></a>'

+ (_params._type == 1 ? '<a id="btnAddAppointment" class="easyui-linkbutton" data-options="iconCls:\'icon-add\',onClick:_employeeDetails.addAppointment" style="width: 130px;margin-left:10px;">' + language.buttons.addAppointment + '</a>' : '')
+ '            <a id="btnCancel" class="easyui-linkbutton" data-options="iconCls:\'icon-cancel\',onClick:_employeeDetails.close" style="width:80px;margin-left:10px;">' + language.buttons.close + '</a>'
+ '        </div>'
+ '    </div>';
            return html;
        }

        //初始化
        (function Init() {
            var html = getHTML();
            $(window.document.body).append(html);
            $.parser.parse();

            BindData();
            //list
            _contacts.create();
            _cars.create();

            $.form.fluid('#frm_personal');
            $.form.setEmptyText('#frm_car');

            $("#win_cars").window({
                onBeforeOpen: function () {
                    _cars.disableValidation();
                }
            });

            $(window).unload(function () {
                try {
                    $("#ci_upload_car").empty();
                } catch (e) {

                }
            });

            InitTabs();

            function getICParams() {
                var row = $("#dg_contacts").datagrid('getSelected');
                var params = "";
                if (row && row.type == 2) {
                    params += "&PL=" + row.id;
                }
                return params;
            }

            $("#btnIncoming").click(function () {
                var params = getICParams();
                $.windowOpen("/manage/customer/IncomingCallsManager.aspx?CD=1&AU=" + _params.AU_Code + params, 550, 300, "InCalls");
            });
            //$.windowOpen("/manage/customer/CustomerServiceOperator.aspx?RP=", 800, 430, "OutCalls");
            $("#btnCalling").click(function () {
                var params = getICParams();
                $.windowOpen("/manage/customer/IncomingCallsManager.aspx?OT=7&CD=2&AU=" + _params.AU_Code + params, 550, 300, "OutCalls");
            });
        })();

        //初始化面板页
        function InitTabs() {
            var $tabs = $("#tabs");
            $tabs.tabs({
                onSelect: function (title, index) {
                    if (index === 2) {
                        $("#AU_Name").textbox('textbox').focus();
                        return;
                    }
                    if (index === 3) { //Associates
                        LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=122&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                        return;
                    }
                    if (index === 4) {
                        LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=24&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                        return;
                    }
                    if (index === 6) {
                        LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=123&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                        return;
                    }
                    if (index === 7) {
                        //LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=40&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                        LoadTabIframe(index, '/templete/report/SubDataGrid.aspx?MF_FL_FB_Code=23&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                        return;
                    }
                    if (index === 8) { //Appointments
                        LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=40&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                        return;
                    }
                }
            });

            var tabId = $.trim(_params.TI).toLowerCase();
            if (tabId) {
                var tabIndex = 0;
                var tabs = $tabs.tabs('tabs');
                for (var i = 0, len = tabs.length; i < len; i++) {
                    var tab = tabs[i];
                    if (tab[0].id == tabId) {
                        tabIndex = $tabs.tabs('getTabIndex', tab);
                        break;
                    }
                }
                if (tabIndex > 0) {
                    $tabs.tabs('select', tabIndex);
                }
            }
        }

        //加载TabIframe
        function LoadTabIframe(index, src) {
            var $tab = $("#tabs").tabs('getTab', index);
            if ($tab == null) return;
            var $tabBody = $tab.panel('body');
            var $frame = $('iframe', $tabBody);
            if ($frame.length === 0) {
                $tabBody.css({ 'overflow': 'hidden', 'position': 'relative' });
                var $iframe = $('<iframe src="' + src + '" style="width:100%;height:100%;" frameborder="0" border="0" scrolling="no"></iframe>');
                $iframe.appendTo($tabBody);
            }
        }

        //绑定数据
        function BindData() {
            BindPersonalSelects();
        }
        //个人信息
        function BindPersonalSelects() {
            $("#AU_Type").combobox({
                onChange: function (nV, oV) {
                    _personal.animate(nV);
                }
            });

            //加载下拉列表数据
            $.bindWords({
                wordIds: [1, 9, 31, 23, 15, 18]
                , selects: [
                    { selectId: 'AU_Gender', wordId: 9 }
                    , { selectId: 'AU_ID_Type', wordId: 31 }
                    , { selectId: 'AU_Education', wordId: 23 }
                    , { selectId: 'AU_Occupation', wordId: 15 }
                    , { selectId: 'AU_Industry', wordId: 18 }
                    , { selectId: 'AU_Type', wordId: 1 }
                ]
                , onLoad: function () {
                    BindContactsSelects();
                }
            });
        }
        //联系方式
        function BindContactsSelects() {
            $.getWords([44, 47, 55, 4064], function (data) {
                _contacts.info._words = data ? data : {};

                $.each(_contacts.info._words, function (n, w) {
                    $.each(w, function (i, o) {
                        if (o.selected) {
                            _contacts.info._words_selected[n] = o.value;
                            return false;
                        }
                    })
                });

                $.post(_s_url, { o: JSON.stringify({ action: 'Get_Messaging_Carriers' }) }, function (data) {
                    _contacts.info._CT_Messaging_Carriers = data ? data : [];
                    BindCarSelects();
                }, "json");
            });
        }
        //汽车
        function BindCarSelects() {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Car_Selects' }) }, function (res) {
                if (res) {
                    $("#MK_Code").initSelect({
                        editable: true,
                        data: res.CT_Make,
                        onSelect: _cars.onSelect.MK_Code,
                        onChange: function (newValue, oldValue) {
                            var that = this;
                            window.setTimeout(function () {
                                var v = $(that).combobox('getValue');
                                if (!(v > 0)) {
                                    $("#CM_Code,#CI_CS_Code").combobox('clear').initSelect();
                                }
                            }, 0);
                        }
                    });
                    $("#CM_Code").initSelect({ onSelect: _cars.onSelect.CM_Code });
                    $("#CI_YR_Code").initSelect({ editable: true, data: res.CT_Years });

                    $("#CI_Color_E,#CI_Color_I").initSelect({ editable: true, data: res.CT_Color_List });
                }

                if (_params.AU_Code > 0) {
                    BindCustomerInfo();
                }
            }, "json");
        }
        //顾客信息
        function BindCustomerInfo() {
            var params = { action: 'Get_CustomerInfo', AU_Code: _params.AU_Code };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (!$.checkResponse(res)) { return; }

                var name = res.personal ? res.personal.AU_Name : '';
                res.summary.EX_AU_Activate_dt = res.personal.EX_AU_Activate_dt;
                _summary.create(name, res.summary);
                _summary.contacts.create(res.contacts);

                _personal.set(res.personal);

                //var contacts = _contacts.sort(res.contacts);
                //$('#dg_contacts').datagrid('loadData', contacts);
                _contacts.GetWords = res.relation.GetWords;
                $('#dg_contacts').datagrid('loadData', res.relation.con)
                $('#dg_cars').datagrid('loadData', res.cars);
            }, "json");
        }
    });
</script>