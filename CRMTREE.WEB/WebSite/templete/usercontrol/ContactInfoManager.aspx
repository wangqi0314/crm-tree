<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactInfoManager.aspx.cs" Inherits="templete_usercontrol_ContactInfoManager" %>

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
            overflow:hidden;
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
   <div class="easyui-panel" data-options="fit:true,border:false,footer:'#ft'">
        <table id="dg_contacts"></table>
    </div>
    <div id="tb_contact" style="padding:3px;">
       <table style="width:100%;" cellpadding="0" cellspacing="0">
           <tr>
               <td style="width: 100%;"><div class="btns" style="margin-right:5px;"></div></td>
               <td style="white-space:nowrap;text-align:right">
               </td>
           </tr>
       </table>
   </div>
    <div id="tb_contactInfo" >
        <table style="width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%;"><div class="btns" style="margin-right:5px;"></div></td>
                <td style="white-space:nowrap;text-align:right"></td>
            </tr>
        </table>
    </div>

    <div id="ft" style="padding:5px;text-align:right;">
        <a class="easyui-linkbutton" id="btnSelectFirst" data-options="iconCls:'icon-save',disabled:false,onClick:_contacts.selectFirst" style="width:80px;">
            <%=Resources.CRMTREEResource.em_contacts_select %>
        </a>
        <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_contacts.close" style="width:80px;margin-left:10px;">
            <%=Resources.CRMTREEResource.btnCancel %>
        </a>
    </div>
</body>
</html>
<script type="text/javascript">
    //ajax地址
    var _s_url = '/handler/Reports/AppointmentManager.aspx';
    var _s_url_customer = '/handler/Reports/CustomerManage.aspx';
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
    //contacts（联系信息）
    //--------------------------------------------------------------------------------------
    var _contacts = {};
    _contacts.language = {
        type: {
            'address': '<%=Resources.CRMTREEResource.ed_contacts_type_address%>',
            'phone': '<%=Resources.CRMTREEResource.ed_contacts_type_phone%>',
            'eMail': '<%=Resources.CRMTREEResource.ed_contacts_type_eMail%>',
            'messaging': '<%=Resources.CRMTREEResource.ed_contacts_type_messaging%>'
        },
        buttons: {
            add: '<%=Resources.CRMTREEResource.em_contacts_add%>',
            save: '<%=Resources.CRMTREEResource.em_contacts_save%>',
            ignore: '<%=Resources.CRMTREEResource.em_contacts_ignore%>',
            select: '<%=Resources.CRMTREEResource.em_contacts_select%>'
        }
    };
    //取消编辑
    _contacts.cancelEdit = function () {
        var $dg = $("#dg_contacts");
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            $dg.datagrid('cancelEdit', i);
        }
    };
    //检查
    _contacts.check = function (rowIndex) {
        if (!(rowIndex >= 0)) {
            return false;
        }

        _contacts.endEditingNoValid();
        var bValid = true;
        var $dg = $("#dg_contacts");
        var row = $dg.datagrid('getRows')[rowIndex];

        if ($.trim(row.type) === '') {
            bValid = false;
            _contacts.onClickCell.call($dg, rowIndex, 'type');
        }

        if (!row.o) {
            bValid = false;
            _contacts.onClickCell.call($dg, rowIndex, 'info');
        }

        return bValid;
    };
    //结束编辑不验证
    _contacts.endEditingNoValid = function () {
        var $dg = $("#dg_contacts");
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            var ed = $dg.datagrid('getEditor', { index: i, field: 'type' });
            if (ed && ed.type === 'combobox') {
                var typeText = $(ed.target).combobox('getText');
                $dg.datagrid('getRows')[i]['type_Text'] = typeText;
            }
            $dg.datagrid('endEdit', i);
        }
    };
    //结束编辑需要验证
    _contacts.endEditing = function () {
        var $dg = $("#dg_contacts");
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
                if (type === 'combo') {
                    isValid = $target.combo('enableValidation').combo('isValid');
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
    _contacts.onClickCell = function (index, field) {
        if (field !== 'btnLast') {
            _contacts.endEditingNoValid();
            var $dg = $(this);
            var rowData = $dg.datagrid('selectRow', index).datagrid('getSelected');

            //已保存数据不能修改类型
            if (field === 'type' && rowData.id > 0) return;

            $dg.datagrid('editCell', { index: index, field: field });

            var ed = $dg.datagrid('getEditor', { index: index, field: field });
            if (ed && ed.type === 'combobox') {
                ed.target.combobox('showPanel');
            }

            if (field === 'info') {
                var $info = ed.target;
                _contacts.info.$info = $info;
                switch (rowData.type) {
                    case '1':
                        _contacts.info.phone($info);
                        break;
                    case '2':
                        _contacts.info.messaging($info);
                        break;
                    case '3':
                        _contacts.info.email($info);
                        break;
                }
                $info.combo('setText', _contacts.info.format(rowData.type, rowData.o))
                .combo('showPanel').combo('textbox').focus();
            }
        }
    };
    //创建添加按钮
    _contacts.createToolBarButton = function () {
        var $btns = $("#tb_contact div.btns");
        var a_o = [
            { icon: 'icon-add', text: _contacts.language.buttons.add, clickFun: '_contacts.add(event,this);' }
        ];
        var a_btns = setBtns(a_o);
        $btns.html(a_btns.join(''));
    };
    //创建
    _contacts.create = function () {
        var $dg = $('#dg_contacts');
        $dg.datagrid({
            url: null,
            fit: true,
            toolbar: '#tb_contact',
            rownumbers: true,
            singleSelect: true,
            showHeader: false,
            border: false,
            loadMsg: '',
            columns: [[
                {
                    field: 'type', title: 'type', width: 100,
                    formatter: function (value, row) {
                        return row.type_Text;
                    },
                    editor: {
                        type: 'combobox', options: {
                            novalidate: true,
                            required: true,
                            onSelect: function (record) {
                                var rowData = $dg.datagrid('getSelected');
                                var rowIndex = $dg.datagrid('getRowIndex', rowData);
                                delete rowData.o;
                            },
                            data: [
                                { text: _contacts.language.type.phone, value: '1' },
                                { text: _contacts.language.type.messaging, value: '2' },
                                { text: _contacts.language.type.eMail, value: '3' }
                            ]
                        }
                    }
                },
                {
                    field: 'info', title: 'info', width: 360, editor: {
                        type: 'combo',
                        options: {
                            novalidate: true,
                            required: true
                        }
                    },
                    formatter: function (value, rowData) {
                        return _contacts.info.format(rowData.type, rowData.o);
                    }
                },
                {
                    field: 'btnLast', align: 'center', width: 60,
                    formatter: function (value, row, index) {
                        var a_o = [
                        {
                            text: _contacts.language.buttons.select,
                            clickFun: '_contacts.select(event,this);',
                            style: 'color:blue;text-decoration:underline;'
                        },
                        ];
                        return setBtns(a_o, true).join('');
                    }
                }
            ]],
            onClickCell: _contacts.onClickCell
        });

        _contacts.createToolBarButton();
        _contacts.info.createToolBarButton();
    };
    //添加
    _contacts.add = function () {
        var $dg = $("#dg_contacts");
        _contacts.endEditingNoValid()
        var contact = { type: '1', type_Text: _contacts.language.type.phone };
        var row = $dg.datagrid('getSelected');
        var rowIndex = undefined;
        if (row) {
            contact.type = row.type;
            contact.type_Text = row.type_Text;
            rowIndex = $dg.datagrid('getRowIndex', row) + 1;
        }
        $dg.datagrid('insertRow', {
            index: rowIndex,
            row: contact
        });
        rowIndex = row ? rowIndex : $dg.datagrid('getRows').length - 1;
        $dg.datagrid('selectRow', rowIndex);
    };
    //选择第一个
    _contacts.selectFirst = function () {
        var $dg = $("#dg_contacts");
        var rows = $dg.datagrid('getRows');
        if (rows.length > 0) {
            var rowData = rows[0];
            var o = {
                AP_PL_ML_Code: rowData.id,
                AP_Cont_Type: rowData.type,
                AP_PL_ML_Code_Text: $.trim(rowData.type_Text) + ' : ' + _contacts.info.format(rowData.type, rowData.o)
            };
            window.top[_params._winID]._appointment.contact.set(o);
            _contacts.close();
        }
    };
    //选择
    _contacts.select = function (e, target) {
        var $dg = $("#dg_contacts");
        var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
        if (_contacts.check(rowIndex)) {
            var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');

            var o = {
                AP_PL_ML_Code: rowData.id,
                AP_Cont_Type: rowData.type,
                AP_PL_ML_Code_Text: $.trim(rowData.type_Text) + ' : ' + _contacts.info.format(rowData.type, rowData.o)
            };
            window.top[_params._winID]._appointment.contact.set(o);
            _contacts.close();
        }

        stopPropagation(e);
    };
    _contacts.close = function () {
        if (window._closeOwnerWindow) {
            window._closeOwnerWindow();
        } else {
            window.close();
        }
    }
    //获得
    _contacts.get = function () {
        var $dg = $("#dg_contacts");
        var rows = $dg.datagrid('getRows');
        var deletedRows = $dg.datagrid('getChanges', 'deleted');
        return { changes: rows, deletes: deletedRows };
    }
    //排序
    _contacts.sort = function (contacts) {
        var address = [], phone = [], email = [], messaging = [];
        $.each(contacts.phone, function (i, info) {
            phone.push({ type: '1', id: info.PL_Code, type_Text: _contacts.language.type.phone, o: info });
        });
        $.each(contacts.messaging, function (i, info) {
            messaging.push({ type: '2', id: info.ML_Code, type_Text: _contacts.language.type.messaging, o: info });
        });
        $.each(contacts.email, function (i, info) {
            email.push({ type: '3', id: info.EL_Code, type_Text: _contacts.language.type.eMail, o: info });
        });

        var data = address.concat(phone, email, messaging);
        data = data.sort(function (a, b) {
            return a.o.pref > b.o.pref ? -1 : 1;
        });

        return data;
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
    //创建联系信息按钮
    _contacts.info.createToolBarButton = function () {
        var $btns = $("#tb_contactInfo div.btns");
        var a_o = [
            { icon: 'icon-save', text: _contacts.language.buttons.save, clickFun: '_contacts.info.save(event,this);' },
            { icon: 'icon-cancel', text: _contacts.language.buttons.ignore, clickFun: '_contacts.info.cancel(event,this);' }
        ];
        var a_btns = setBtns(a_o);
        $btns.html(a_btns.join(''));
    }
    //取消
    _contacts.info.cancel = function () {
        if (_contacts.info.$info) _contacts.info.$info.combo('hidePanel');
        _contacts.cancelEdit();
    };
    //保存
    _contacts.info.save = function () {
        if (_contacts.info.endEditing()) {
            var $dg = _contacts.info.$dg;
            var rowInfo = $dg.datagrid('getRows')[0];
            var rowContact = $("#dg_contacts").datagrid('getSelected');
            var rowIndex = $("#dg_contacts").datagrid('getRowIndex', rowContact);
            rowContact.pref = rowIndex + 1;
            rowContact.o = rowInfo;
            if (_contacts.info.$info) {
                _contacts.info.$info.combo('hidePanel')
                .combo('setText', _contacts.info.format(rowContact.type, rowContact.o))
            }

            $.mask.show();
            $("#btnSave").linkbutton('disable');
            var s_params = JSON.stringify({ action: 'Save_Contact', au_code: _params.AU_Code, o: rowContact });
            $.post(_s_url, { o: s_params }, function (res) {
                $.mask.hide();
                $("#btnSave").linkbutton('enable');
                if ($.checkResponse(res)) {
                    if (res.code > 0) {
                        rowContact.id = res.code;
                        switch (rowContact.type) {
                            case 1:
                                rowContact.o.PL_Code = res.code;
                                break;
                            case 2:
                                rowContact.o.ML_Code = res.code;
                                break;
                            case 3:
                                rowContact.o.EL_Code = res.code;
                                break;
                        }
                    }
                    _contacts.endEditingNoValid();
                }
            }, "json");
        }
    };
    //结束编辑无需验证
    _contacts.info.endEditingNoValid = function () {
        var $dg = _contacts.info.$dg;
        if ($dg) {
            var rows = $dg.datagrid('getRows');
            for (var i = 0, len = rows.length; i < len; i++) {
                var eds = $dg.datagrid('getEditors', i);
                for (var j = 0; j < eds.length; j++) {
                    var ed = eds[j];
                    if (ed && ed.type === 'combobox') {
                        var typeText = $(ed.target).combobox('getText');
                        $dg.datagrid('getRows')[i][ed.field + '_Text'] = typeText;
                    }
                }
                $dg.datagrid('endEdit', i);
            }
        }
    };

    //结束编辑需验证
    _contacts.info.endEditing = function () {
        var $dg = _contacts.info.$dg;
        var bValid = true;
        if ($dg) {
            var rows = $dg.datagrid('getRows');
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
                    if (type === 'textbox') {
                        isValid = $target.textbox('enableValidation').textbox('isValid');
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
                }
            }
        }
        return bValid;
    };
    //创建编辑表格
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
            toolbar: '#tb_contactInfo',
            data: [data],
            loadMsg: '',
            columns: [opts.columns],
            onBeginEdit: opts.onBeginEdit
        });
        $c.datagrid('beginEdit', 0);
        _contacts.info.$dg = $c;
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
            panelWidth: 360,
            data: data,
            columns: [
            {
                field: 'PL_Type', title: language.PL_Type, width: 130, editor: {
                    type: 'combobox', options: {
                        novalidate: true,
                        required: true,
                        data: _contacts.info._words["_47"]
                    }
                },
                formatter: function (value, row) {
                    return row.PL_Type_Text ? row.PL_Type_Text : value;
                }
            },
            {
                field: 'PL_Number', title: language.PL_Number, width: 200, editor: {
                    type: 'textbox', options: {
                        novalidate: true,
                        required: true,
                        validType: 'maxLength[12]'
                    }
                }
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
            panelWidth: 360,
            data: data,
            columns: [
            {
                field: 'EL_Type', title: language.EL_Type, width: 130, editor: {
                    type: 'combobox', options: {
                        novalidate: true,
                        required: true,
                        data: _contacts.info._words["_44"]
                    }
                },
                formatter: function (value, row) {
                    return row.EL_Type_Text ? row.EL_Type_Text : value;
                }
            },
            {
                field: 'EL_Address', title: language.EL_Address, width: 200, editor: {
                    type: 'textbox', options: {
                        novalidate: true,
                        required: true,
                        validType: ['email', 'maxLength[25]']
                    }
                }
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
            panelWidth: 360,
            data: data,
            columns: [
            {
                field: 'ML_MC_Code', title: language.ML_MC_Code, width: 130, editor: {
                    type: 'combobox', options: {
                        novalidate: true,
                        required: true,
                        data: _contacts.info._CT_Messaging_Carriers
                    }
                },
                formatter: function (value, row) {
                    return row.ML_MC_Code_Text ? row.ML_MC_Code_Text : value;
                }
            },
            {
                field: 'ML_Handle', title: language.ML_Handle, width: 200, editor: {
                    type: 'textbox', options: {
                        novalidate: true,
                        required: true,
                        validType: 'maxLength[20]'
                    }
                }
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
                    $.trim(row.PL_Type_Text),
                    ':',
                    $.trim(row.PL_Number)
                ];
                break;
            case '2':
                a_info = [
                    $.trim(row.ML_MC_Code_Text),
                    ':',
                    $.trim(row.ML_Handle)
                ];
                break;
            case '3':
                a_info = [
                    $.trim(row.EL_Type_Text),
                    ':',
                    $.trim(row.EL_Address)
                ];
                break;
        }
        var s_info = $.trim(a_info.join(' '));
        return s_info == ':' ? '' : s_info;
    };

    //设置打开窗体
    _setWindow = function (win) {
        if (win) {
            var opts = win.window('options');
            opts.title = '<%=Resources.CRMTREEResource.ap_contact_window_title%>';
            win.window(opts);
        }
    }

    $(function () {
        //初始化
        (function Init() {
            if (!(_params.AU_Code > 0)) {
                $.msgTips.info('<%=Resources.CRMTREEResource.cm_msg001%>');
                return;
            }

            _contacts.create();

            BindContactsSelects();
        })();

        //联系方式
        function BindContactsSelects() {
            $.getWords([44, 47], function (data) {
                _contacts.info._words = data ? data : {};
                $.each(_contacts.info._words, function (n, w) {
                    $.each(w, function (i, o) {
                        if (o.selected) {
                            _contacts.info._words_selected[n] = o.value;
                            return false;
                        }
                    })
                });
                $.post(_s_url_customer, { o: JSON.stringify({ action: 'Get_Messaging_Carriers' }) }, function (data) {
                    _contacts.info._CT_Messaging_Carriers = data ? data : [];

                    BindContacts();
                }, "json");
            });
        }
        function BindContacts() {
            var params = { action: 'Get_Contacts', AU_Code: _params.AU_Code };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (!$.checkResponse(res)) { return; }

                var contacts = _contacts.sort(res);
                $('#dg_contacts').datagrid('loadData', contacts);

            }, "json");
        }
        //关闭窗体
        function closeWindow() {
            if (window._closeOwnerWindow) { window._closeOwnerWindow(); }
        }
    });
</script>