<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManager.aspx.cs" Inherits="templete_report_UserManager" %>

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

        .datagrid-header {
            border-width: 0;
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
            margin-bottom: 10px;
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

        .crm-form-flow input {
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

        .form td {
            padding-bottom: 6px;
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
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Reports/CustomerManage.aspx';
    var _s_url_employee = '/handler/Reports/EmployeeManage.aspx';
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
            remove: '<%=Resources.CRMTREEResource.em_contacts_remove%>',
            up: '<%=Resources.CRMTREEResource.em_contacts_up%>',
            down: '<%=Resources.CRMTREEResource.em_contacts_down%>',
            accept: '<%=Resources.CRMTREEResource.em_contacts_accept%>',
            ignore: '<%=Resources.CRMTREEResource.em_contacts_ignore%>'
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
    //验证
    _contacts.check = function () {
        _contacts.endEditingNoValid();
        var $dg = $("#dg_contacts");
        var rows = $dg.datagrid('getRows');
        var bValid = true;
        for (var i = 0, len = rows.length; i < len; i++) {
            var row = rows[i];
            if ($.trim(row.type) === '') {
                bValid = false;
                $("#tabs").tabs('select', 2);
                _contacts.onClickCell.call($dg, i, 'type');
                break;
            }
            if (!row.o) {
                bValid = false;
                $("#tabs").tabs('select', 2);
                _contacts.onClickCell.call($dg, i, 'info');
                break;
            }
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
                        _contacts.info.address($info);
                        break;
                    case '2':
                        _contacts.info.phone($info);
                        break;
                    case '3':
                        _contacts.info.email($info);
                        break;
                    case '4':
                        _contacts.info.messaging($info);
                        break;
                }
                $info.combo('setText', _contacts.info.format(rowData.type, rowData.o))
                .combo('showPanel').combo('textbox').focus();
            }
        }
    };
    //创建添加按钮
    _contacts.createAdd = function () {
        var $btns = $("#tb_contact div.btns");
        var a_o = [
            { icon: 'icon-add', text: _contacts.language.buttons.add, clickFun: '_contacts.add(event,this);' }
        ];
        var a_btns = setBtns(a_o);
        $btns.html(a_btns.join(''));
    };
    //创建联系信息按钮
    _contacts.createAcceptAndCancel = function () {
        var $btns = $("#tb_contactInfo div.btns");
        var a_o = [
            { icon: 'icon-ok', text: _contacts.language.buttons.accept, clickFun: '_contacts.info.accept(event,this);' },
            { icon: 'icon-cancel', text: _contacts.language.buttons.ignore, clickFun: '_contacts.info.cancel(event,this);' }
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
                                if (rowData) {
                                    delete rowData.o;
                                }
                            },
                            data: [
                                { text: _contacts.language.type.address, value: '1' },
                                { text: _contacts.language.type.phone, value: '2' },
                                { text: _contacts.language.type.eMail, value: '3' },
                                { text: _contacts.language.type.messaging, value: '4' }
                            ]
                        }
                    }
                },
                {
                    field: 'info', title: 'info', width: 400, editor: {
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
                    field: 'btnLast', align: 'center', width: 100,
                    formatter: function (value, row, index) {
                        var a_o = [
                        { icon: 'icon-remove', title: _contacts.language.buttons.remove, clickFun: '_contacts.remove(event,this);' },
                        { icon: 'icon-up', title: _contacts.language.buttons.up, clickFun: '_contacts.move(event,this,true);' },
                        { icon: 'icon-down', title: _contacts.language.buttons.down, clickFun: '_contacts.move(event,this,false);' }
                        ];
                        return setBtns(a_o, true).join('');
                    }
                }
            ]],
            onClickCell: _contacts.onClickCell
        });

        _contacts.createAdd();
        _contacts.createAcceptAndCancel();
    };
    //添加
    _contacts.add = function () {
        var $dg = $("#dg_contacts");
        _contacts.endEditingNoValid()
        var contact = { type: '1', type_Text: _contacts.language.type.address };
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
        $dg.datagrid('selectRow', rowIndex);//.datagrid('beginEdit', rowIndex);
    };
    //删除
    _contacts.remove = function (e, target) {
        var $dg = $("#dg_contacts");
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
    _contacts.move = function (e, target, isUp) {
        var $dg = $("#dg_contacts");
        var index = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
        _contacts.endEditingNoValid();
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
    _contacts.get = function () {
        var $dg = $("#dg_contacts");
        //_contacts.endEditing();
        var rows = $dg.datagrid('getRows');
        var deletedRows = $dg.datagrid('getChanges', 'deleted');
        return { changes: rows, deletes: deletedRows };
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
    };
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
    //取消
    _contacts.info.cancel = function () {
        if (_contacts.info.$info) _contacts.info.$info.combo('hidePanel');
        _contacts.cancelEdit();
    }
    //应用
    _contacts.info.accept = function () {
        if (_contacts.info.endEditing()) {
            var $dg = _contacts.info.$dg;
            var rowInfo = $dg.datagrid('getRows')[0];
            var rowContact = $("#dg_contacts").datagrid('getSelected');
            rowContact.o = rowInfo;
            if (_contacts.info.$info) {
                _contacts.info.$info.combo('hidePanel')
                .combo('setText', _contacts.info.format(rowContact.type, rowContact.o))
            }
            _contacts.endEditingNoValid();
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
            panelWidth: 670,
            data: data,
            columns: [
                {
                    field: 'AL_Type', title: language.AL_Type, width: 90, editor: {
                        type: 'combobox', options: {
                            novalidate: true,
                            required: true,
                            data: _contacts.info._words["_55"]
                        }
                    },
                    formatter: function (value, row) {
                        return row.AL_Type_Text ? row.AL_Type_Text : value;
                    }
                },
                {
                    field: 'AL_Add1', title: language.AL_Add1, width: 100, editor: {
                        type: 'textbox', options: {
                            novalidate: true,
                            required: true,
                            validType: 'maxLength[50]'
                        }
                    }
                },
                {
                    field: 'AL_Add2', title: language.AL_Add2, width: 100, editor: {
                        type: 'textbox', options: {
                            novalidate: true,
                            validType: 'maxLength[50]'
                        }
                    }
                },
                {
                    field: 'AL_District', title: language.AL_District, width: 100, editor: {
                        type: 'textbox', options: {
                            novalidate: true,
                            validType: 'maxLength[10]'
                        }
                    }
                },
                {
                    field: 'AL_City', title: language.AL_City, width: 100, editor: {
                        type: 'textbox', options: {
                            novalidate: true,
                            validType: 'maxLength[10]'
                        }
                    }
                },
                {
                    field: 'AL_State', title: language.AL_State, width: 100, editor: {
                        type: 'textbox', options: {
                            novalidate: true,
                            validType: 'maxLength[10]'
                        }
                    }
                },
                {
                    field: 'AL_Zip', title: language.AL_Zip, width: 60, editor: {
                        type: 'textbox', options: {
                            novalidate: true,
                            validType: 'maxLength[10]'
                        }
                    }
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
                field: 'PL_Number', title: language.PL_Number, width: 240, editor: {
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
            panelWidth: 400,
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
                field: 'EL_Address', title: language.EL_Address, width: 240, editor: {
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
            panelWidth: 400,
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
                field: 'ML_Handle', title: language.ML_Handle, width: 240, editor: {
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
    //personal（个人信息）
    //--------------------------------------------------------------------------------------
    var _personal = {};
    _personal.language = {
        buttons: {
            remove: '<%=Resources.CRMTREEResource.em_personal_buttons_remove%>'
        }
    };
    //验证
    _personal.validate = function () {
        return $.form.validate({
            textbox: ['AU_Name', 'AU_ID_No', 'AU_Dr_Lic', 'DE_ID']
        });
    };
    //取消验证
    _personal.disableValidation = function () {
        $.form.disableValidation({
            textbox: ['AU_Name', 'AU_ID_No', 'AU_Dr_Lic', 'DE_ID']
        });
    };
    //设置
    _personal.set = function (personal) {
        $.form.setData('#frm_personal', personal);
        $("#AU_Username").textbox('setValue', personal.AU_Username);
    };
    //获取
    _personal.get = function () {
        var data = $.form.getData("#frm_personal", ['AU_Gender', 'AU_Married']);
        return data;
    };
    //图片
    _personal.picture = {};
    //创建图片
    _personal.picture.create = function (fileName) {
        var $img = $('<div class="picItem car_img"><div class="remove" title="' + _personal.language.buttons.remove + '"></div><img src="/images/Adviser/' + fileName + '"/></div>');
        $("#plupload_container").empty().append($img);
    };
    //初始化上传
    _personal.picture.plupload = function () {
        //图片上传
        var _uploader = $.plupload({
            multi_selection: false,
            container: 'plupload_browse_button',
            browse_button: 'plupload_browse_button',
            params: { folderName: 'employee_temp' },
            init: {
                FilesAdded: function (up, files) {
                    up.refresh();
                    //创建容器
                    for (var i = 0, len = files.length; i < len; i++) {
                        var file = files[i];
                        file._$container = $('<div class="picItem car_img _add"><div _id="' + file.id + '" class="remove" title="' + _personal.language.buttons.remove + '"></div></div>');
                        file._$progress = $('<div class="progress"></div>');
                        file._$progress.appendTo(file._$container);
                        $("#plupload_container").empty().append(file._$container);
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
                    $("#DE_Picture_FN").val(fileName);
                    var imgsrc = '/plupload/' + up.settings.params.folderName + '/' + fileName;
                    file._$progress.detach();
                    file._$container.append('<img src="' + imgsrc + '"/>');
                }
            }
        });
        $("#plupload_container .car_img>img").live({
            mouseover: function () {
                $(this).fadeTo('fast', 0.6);
            },
            mouseleave: function () {
                $(this).fadeTo('fast', 1);
            },
            //预览图片
            click: function () {
                var src = $(this).attr('src');
                var title = $("#AU_Name").val() + ' - ' + '<%=Resources.CRMTREEResource.ed_personal_picture_title%>';
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
        $("#plupload_container .car_img>.remove").live({
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
                $("#DE_Picture_FN").val("");
                $(this).parent().detach();
                return false;
            }
        });
    };

    //--------------------------------------------------------------------------------------
    //account（账户信息）
    //--------------------------------------------------------------------------------------
    var _account = {};
    _account.language = {

    };
    //验证
    _account.validate = function () {
        return $.form.validate({
            textbox: ['AU_Password', 'Account_AU_Password']
        });
    };
    //取消验证
    _account.disableValidation = function () {
        $.form.disableValidation({
            textbox: ['AU_Password', 'Account_AU_Password']
        });
    };
    //设置
    _account.set = function (account) {
        $.form.setData('#frm_account', account, "#Account_AU_Password,#AU_Password");
    };
    //获取
    _account.get = function () {
        var data = $.form.getData("#frm_account", null, "#Account_AU_Password");
        return data;
    };

    //--------------------------------------------------------------------------------------
    //summary（简介信息）
    //--------------------------------------------------------------------------------------
    var _summary = {};
    _summary.language = {
        name: '<%=Resources.CRMTREEResource.ed_summary_name%>',
        active: '<%=Resources.CRMTREEResource.ed_summary_active%>',
        inActive: '<%=Resources.CRMTREEResource.ed_summary_inActive%>',
        id: '<%=Resources.CRMTREEResource.ed_summary_id%>'
    };
    //创建
    _summary.create = function (name, summary, personal) {
        if (summary && personal) {
            var imgName = $.trim(personal.DE_Picture_FN);
            var html = '<div class="crm-form-flow-item">'
            + '    <span>' + this.language.name + ' :</span>'
            + '    <label>' + $.trim(name) + '</label>'
            + '</div>'
            + '<div class="crm-form-flow-item">'
            + '    <span>' + this.language.active + ' :</span>'
            + (
            $.trim(summary.Active) == '0' ?
            '    <img src="/images/Customer/inactive.png?v=1" title="' + this.language.inActive + '" style="vertical-align:-6px;margin-left:5px;"/>'
            :
            '    <img src="/images/Customer/active.png?v=1" title="' + this.language.active + '" style="vertical-align:-3px;margin-left:5px;"/>'
            + ' (Activated on ' + personal.EX_DE_Activate_dt + ')'
            ) + '</div>'
            + '<br />'
            + '<div class="crm-form-flow-item">'
            + '    <span>' + this.language.id + ' :</span>'
            + '    <label>' + $.trim(personal.DE_ID) + '</label>'
            + '</div>'
            + '<br />'
            + '<div class="crm-form-flow-item">'
            + ' <img src="/images/Adviser/' + imgName + '" style="width:60px;height:60px;border:0;' + (imgName ? '' : 'display:none;') + '"/>'
            + '</div>'
            + '</div>';

            $("#summaryInfo").show().html(html).after('<div style="width:98%;border-bottom: 1px solid #E4E4E4;position:absolute;bottom:0;"></div>');
        }
    };
    //-----------------------------
    //contacts（联系方式）
    //-----------------------------
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
                type: '1', id: address.AL_Code, typeText: _summary.contacts.language.address,
                o: address
            });
        }

        len = contacts.phone.length;
        if (len > 0) {
            var phone = contacts.phone.sort(function (a, b) {
                return a.pref < b.pref ? 1 : -1;
            })[len - 1];
            data.push({
                type: '2', id: phone.PL_Code, typeText: _summary.contacts.language.phone,
                o: phone
            });
        }

        len = contacts.email.length;
        if (len > 0) {
            var email = contacts.email.sort(function (a, b) {
                return a.pref < b.pref ? 1 : -1;
            })[len - 1];
            data.push({
                type: '3', id: email.EL_Code, typeText: _summary.contacts.language.email,
                o: email
            });
        }

        len = contacts.messaging.length;
        if (len > 0) {
            var messaging = contacts.messaging.sort(function (a, b) {
                return a.pref < b.pref ? 1 : -1;
            })[len - 1];
            data.push({
                type: '4', id: messaging.ML_Code, typeText: _summary.contacts.language.messaging,
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
                , o.typeText
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
    //获得联系信息按钮
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
    //获得联系类型文本
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
        if (field !== 'Time' && field !== 'Flag') {
            _schedule.beginEdit(index, field);
        }
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
            border: false,
            loadMsg: '',
            columns: [[
                { field: 'Time_Text', title: '', width: 60, align: 'center' },
                { field: 'Flag_Text', title: '', width: 60, align: 'center' },
                { field: 'Mon', title: _schedule.language.mon, width: 80, editor: 'timespinner' },
                { field: 'Tue', title: _schedule.language.tue, width: 80, editor: 'timespinner' },
                { field: 'Wed', title: _schedule.language.wed, width: 80, editor: 'timespinner' },
                { field: 'Thu', title: _schedule.language.thu, width: 80, editor: 'timespinner' },
                { field: 'Fri', title: _schedule.language.fri, width: 80, editor: 'timespinner' },
                { field: 'Sat', title: _schedule.language.sat, width: 80, editor: 'timespinner' },
                { field: 'Sun', title: _schedule.language.sun, width: 80, editor: 'timespinner' }
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
        var o_schedule = { DP_UType: 1 };
        $.each(rows, function (i, o) {
            $.each(o, function (n, d) {
                if (n === 'Time' && n === 'Flag' && n.lastIndexOf('_Text') !== -1) { return true; }
                d = $.trim(d);
                if (d != "") {
                    var field = [
                        'DP',
                        'D' + _schedule.getDayOfWeek(n),
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
    //_employeeDetails（员工详情）
    //--------------------------------------------------------------------------------------
    var _employeeDetails = {};
    //关闭
    _employeeDetails.close = function () {
        if (window._closeOwnerWindow) {
            window._closeOwnerWindow();
        } else {
            window.close();
        }
    };

    //设置打开窗体
    _setWindow = function (win) {
        if (win) {
            var opts = win.window('options');
            opts.title = '<%=Resources.CRMTREEResource.em_window_title%>';
            win.window(opts);
        }
    }

    $(function () {
        //获得界面HTML
        function getHTML() {
            var language = {
                tabs: {
                    summary: '<%=Resources.CRMTREEResource.ed_tabs_summary %>',
                    personal: '<%=Resources.CRMTREEResource.ed_tabs_personal %>',
                    contacts: '<%=Resources.CRMTREEResource.ed_tabs_contacts %>',
                    schedule: '<%=Resources.CRMTREEResource.ed_tabs_schedule %>',
                    account: '<%=Resources.CRMTREEResource.ed_tabs_account %>'
                },
                personal: {
                    id: '<%=Resources.CRMTREEResource.ed_personal_id %>',
                    active: '<%=Resources.CRMTREEResource.ed_personal_active %>',
                    name: '<%=Resources.CRMTREEResource.ed_personal_name %>',
                    gender: '<%=Resources.CRMTREEResource.ed_personal_gender %>',
                    birthday: '<%=Resources.CRMTREEResource.ed_personal_birthday %>',
                    idType: '<%=Resources.CRMTREEResource.ed_personal_idType %>',
                    idNo: '<%=Resources.CRMTREEResource.ed_personal_idNo %>',
                    driverLicense: '<%=Resources.CRMTREEResource.ed_personal_driverLicense %>',
                    education: '<%=Resources.CRMTREEResource.ed_personal_education %>',
                    occupation: '<%=Resources.CRMTREEResource.ed_personal_occupation %>',
                    industry: '<%=Resources.CRMTREEResource.ed_personal_industry %>',
                    picture: '<%=Resources.CRMTREEResource.em_personal_picture %>',
                    fun: '<%=Resources.CRMTREEResource.em_personal_function %>',
                    marital: '<%=Resources.CRMTREEResource.em_personal_marital %>'
                },
                account: {
                    UserName: '<%=Resources.CRMTREEResource.ed_account_UserName %>',
                    Password: '<%=Resources.CRMTREEResource.ed_account_Password %>',
                    ConfirmPassword: '<%=Resources.CRMTREEResource.ed_account_ConfirmPassword %>'
                },
                buttons: {
                    resetAll: '<%=Resources.CRMTREEResource.em_buttons_resetAll %>',
                    save: '<%=Resources.CRMTREEResource.em_buttons_save %>',
                    undo: '<%=Resources.CRMTREEResource.em_buttons_undo %>',
                    close: '<%=Resources.CRMTREEResource.em_buttons_close %>'
                }
            };
            if (_isEn) {
                $.each(language.personal, function (n, v) {
                    language.personal[n] = v + ':';
                });
            } else {
                $.each(language.personal, function (n, v) {
                    language.personal[n] = v + '：';
                });
            }

            var html = '<div class="easyui-layout" data-options="fit:true">'
+ '        <div data-options="region:\'center\',border:false" style="padding:0px;padding-top:5px;">'
+ '            <div id="tabs" class="easyui-tabs" data-options="fit:true,selected:0,plain:true,border:false">'
+ '                <div title="' + language.tabs.summary + '" data-options="id:\'sum\',iconCls:\'icon-customer-summary\'" style="padding:10px">'
+ '                    <div class="easyui-layout" data-options="fit:true">'
+ '                        <div data-options="region:\'north\',height:\'50%\',border:false" style="padding: 10px 10px 0px 10px;overflow:hidden;">'
+ '                            <div id="summaryInfo" class="crm-form-flow" style="display:none;">'
+ '                            </div>'
+ '                        </div>'
+ '                        <div data-options="region:\'center\',border:false" style="padding: 20px 10px 10px 10px; overflow: hidden;">'
+ '                            <div class="easyui-panel" data-options="fit:true,border:false">'
+ '                                <div id="dg_summary_contacts" class="crm-table-flow">'
+ '                                </div>'
+ '                            </div>'
+ '                        </div>'
+ '                    </div>'
+ '                </div>'
+ '                <div title="' + language.tabs.personal + '" data-options="id:\'per\',iconCls:\'icon-customer-personal\'" style="padding:10px">'
+ '                    <div class="easyui-panel" data-options="fit:true,border:false" style="overflow:hidden">'
+ '                        <table id="frm_personal" class="form' + (_isEn ? '' : ' form_cn') + '" border="0" cellpadding="3" cellspacing="2">'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.id + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Code" type="hidden" value="0" />'
+ '                                    <input id="AU_Type" type="hidden" value="0" />'
+ '                                    <input id="AU_UG_Code" type="hidden" value="" />'
+ '                                    <input id="DE_Code" type="hidden" value="0" />'
+ '                                    <input id="DE_Picture_FN_Temp" type="hidden" value="" />'
+ '                                    <input id="DE_ID" class="easyui-textbox" data-options="required:true,novalidate:true,validType: \'maxLength[15]\'"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.active + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Active_tag" type="checkbox" checked="true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.name + '</td>'
+ '                                <td class="fluid">'
+ '                                    <input id="AU_Name" class="easyui-textbox" data-options="required:true,novalidate:true,validType: \'maxLength[50]\'"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.marital + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Married" class="easyui-combobox" />'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.gender + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Gender" class="easyui-combobox" />'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.birthday + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_B_date" class="easyui-datebox" />'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.idType + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_ID_Type" class="easyui-combobox" />'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.idNo + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_ID_No" class="easyui-textbox" data-options="novalidate:true,validType: \'maxLength[10]\'"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.driverLicense + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Dr_Lic" class="easyui-textbox" data-options="novalidate:true,validType: \'maxLength[20]\'"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.education + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Education" class="easyui-combobox" />'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.fun + '</td>'
+ '                                <td>'
+ '                                    <input id="DE_Type" class="easyui-combobox" />'
+ '                                </td>'
+ '                            </tr>'
//+ '                            <tr>'
//+ '                                <td class="text">' + language.personal.occupation + '</td>'
//+ '                                <td>'
//+ '                                    <input id="AU_Occupation" class="easyui-combobox" />'
//+ '                                </td>'
//+ '                                <td class="text">' + language.personal.industry + '</td>'
//+ '                                <td>'
//+ '                                    <input id="AU_Industry" class="easyui-combobox" />'
//+ '                                </td>'
//+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text"><a href="javascript:void(0);" id="plupload_browse_button" style="text-decoration: none;">' + language.personal.picture + '</a></td>'
+ '                                <td>'
+ '                                    <input id="DE_Picture_FN" type="hidden" value=""/>'
+ '                                    <div id="plupload_container" style="position:relative;width:100%;height:64px;"></div>'
+ '                                </td>'
+ '                            </tr>'
+ '                        </table>'
+ '                    </div>'
+ '                </div>'
+ '                <div title="' + language.tabs.contacts + '" data-options="id:\'con\',iconCls:\'icon-customer-contacts\'" style="padding:0px 0px 1px 0px">'
+ '                    <div class="easyui-panel" data-options="fit:true,border:false">'
+ '                        <table id="dg_contacts"></table>'
+ '                    </div>'
+ '                </div>'
+ '                <div title="' + language.tabs.schedule + '" data-options="id:\'sch\',iconCls:\'icon-customer-visits\'">'
+ '                    <table id="dg_schedule"></table>'
+ '                </div>'
+ '                <div title="' + language.tabs.account + '" data-options="id:\'acc\',iconCls:\'icon-customer-personal\'" style="padding:10px">'
+ '                    <div class="easyui-panel" data-options="fit:true,border:false" style="overflow:hidden">'
+ '                        <table id="frm_account" class="form' + (_isEn ? '' : ' form_cn') + '" border="0" cellpadding="3" cellspacing="2">'
+ '                            <tr>'
+ '                                <td class="text">' + language.account.UserName + '</td>'
+ '                                <td>'
+ '<input id="AU_Username" class="easyui-textbox" data-options="width:200,iconCls:\'icon-man\',iconWidth:38">'
+ '                                </td>'
+ '                            </tr>'
//+ '                            <tr>'
//+ '                                <td class="text">Current Password:</td>'
//+ '                                <td>'
//+ '<input class="easyui-textbox" type="password" data-options="width:200,prompt:\'Password\',iconCls:\'icon-lock\',iconWidth:38">'
//+ '                                </td>'
//+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.account.Password + '</td>'
+ '                                <td>'
+ '<input id="AU_Password" class="easyui-textbox" type="password" data-options="novalidate:true,validType:\'maxLength[32]\',width:200,prompt:\'Password\',iconCls:\'icon-lock\',iconWidth:38">'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.account.ConfirmPassword + '</td>'
+ '                                <td>'
+ '<input id="Account_AU_Password" class="easyui-textbox" type="password" data-options="novalidate:true,validType:{maxLength:[32],equals:[\'#AU_Password\']},invalidMessage:\'Confirm password and Password do not match\',width:200,prompt:\'Password\',iconCls:\'icon-lock\',iconWidth:38">'
+ '                                </td>'
+ '                            </tr>'
+ '                        </table>'
+ '                    </div>'
+ '                </div>'
+ '            </div>'
+ '        </div>'
+ '        <div data-options="region:\'south\',border:false" style="height:auto;text-align:right;padding:10px;border-top: 1px solid #B1C242;position:relative;overflow:hidden;">'
+ '            <div style="float:right;">'
+ '             <a id="btnSave" class="easyui-linkbutton" data-options="iconCls:\'icon-save\'" style="width:80px;margin-left:10px;">' + language.buttons.save + '</a>'
+ '             <a id="btnClose" class="easyui-linkbutton" data-options="iconCls:\'icon-cancel\',onClick:_employeeDetails.close" style="width: 80px; margin-left: 10px;">' + language.buttons.close + '</a>'
+ '            </div>'
+ '            <div id="scheduleButtons" style="float:right;display:none;">'
+ '             <a id="btnReset" class="easyui-linkbutton" data-options="onClick:function(){_schedule.reset(true);}" style="width:80px;">' + language.buttons.resetAll + '</a>'
+ '             <a id="btnUndo" class="easyui-linkbutton" data-options="onClick:_schedule.undo" style="width: 80px; margin-left: 10px;">' + language.buttons.undo + '</a>'
+ '            </div>'
+ '            <div style="clear:both;height:0;"></div>'
+ '        </div>'
+ '    </div>'
+ '        <div id="tb_contact" style="padding:3px;">'
+ '         <table style="width:100%;" cellpadding="0" cellspacing="0">'
+ '            <tr>'
+ '                <td style="width: 100%;"><div class="btns" style="margin-right:5px;"></div></td>'
+ '                <td style="white-space:nowrap;text-align:right">'
+ '                    '
+ '                </td>'
+ '            </tr>'
+ '         </table>'
+ '        </div>'
+ '    <div id="tb_contactInfo" style="display:none;">'
+ '        <table style="width:100%;" cellpadding="0" cellspacing="0">'
+ '            <tr>'
+ '                <td style="width: 100%;"><div class="btns" style="margin-right:5px;"></div></td>'
+ '                <td style="white-space:nowrap;text-align:right"></td>'
+ '            </tr>'
+ '        </table>'
+ '    </div>';
            return html;
        }

        //初始化
        (function Init() {
            if (_params.action !== "add" && !(_params.Empl_Code > 0)) {
                $.msgTips.info('<%=Resources.CRMTREEResource.em_msg001%>');
                return;
            }

            $(window).unload(function () {
                try {
                    $("#plupload_browse_button").empty();
                } catch (e) {

                }
            });

            var html = getHTML();
            $(window.document.body).append(html);
            $.parser.parse();

            $($("#DE_ID").textbox('textbox')).css({ color: "black" });

            initButtons();

            _personal.picture.plupload();

            _contacts.create();

            _schedule.create();

            $.form.fluid("#frm_personal");

            BindPersonalSelects();

            InitTabs();
        })();

        //初始化面板页
        function InitTabs() {
            var $tabs = $("#tabs");
            $tabs.tabs({
                onSelect: function (title, index) {
                    if (index === 1) {
                        $("#DE_ID").textbox('textbox').focus();
                    }
                    if (index === 3) {
                        $("#scheduleButtons").show();
                    } else {
                        $("#scheduleButtons").hide();
                    }

                    if (index === 4) {
                        $("#AU_Username").textbox('textbox').focus();
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
            } else {
                if (_params.action === "add") {
                    $tabs.tabs('select', 1);
                }
            }
        }

        //初始化按钮
        function initButtons() {
            //保存日程安排
            $("#btnSave").linkbutton({
                onClick: Save
            }).linkbutton('disable');
        }

        //保存数据
        function Save() {
            //验证联系信息
            if (!_contacts.check()) {
                return;
            }

            //验证个人信息
            if (!_personal.validate()) {
                $("#tabs").tabs('select', 1);
                return;
            }

            //验证账户信息
            if (!_account.validate()) {
                $("#tabs").tabs('select', 4);
                return;
            }

            var params = { action: 'Save_Employee' };
            params.contacts = _contacts.get();
            params.personal = $.extend({}, _personal.get(), _account.get());
            //params.personal.AU_Type = 0;
            params.schedule = _schedule.get();

            $.mask.show();
            $("#btnSave").linkbutton('disable');
            var s_params = JSON.stringify(params);
            $.post(_s_url_employee, { o: s_params }, function (res) {
                $.mask.hide();
                $("#btnSave").linkbutton('enable');
                if ($.checkResponse(res)) {
                    if (window._closeOwnerWindow) {
                        top[_params._winID]._datagrid.action.refresh();
                    }
                    closeWindow();
                }
            }, "json");
        }

        //关闭窗体
        function closeWindow() {
            if (window._closeOwnerWindow) {
                window._closeOwnerWindow();
            } else {
                window.close();
            }
        }

        //绑定个人下拉列表
        function BindPersonalSelects() {
            $.getWords([44, 47, 55], function (data) {
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
                    //加载下拉列表数据
                    $.bindWords({
                        wordIds: [9, 31, 23, 1036]
                        , selects: [
                            { selectId: 'AU_Gender', wordId: 9 }
                            , { selectId: 'AU_ID_Type', wordId: 31 }
                            , { selectId: 'AU_Education', wordId: 23 }
                            , { selectId: 'AU_Married', wordId: 1036 }
                            //, { selectId: 'AU_Occupation', wordId: 15 }
                            //, { selectId: 'AU_Industry', wordId: 18 }
                        ]
                        , onLoad: function () {
                            $.post(_s_url_employee, { o: JSON.stringify({ action: 'Get_Empl_Type' }) }, function (data) {
                                $("#DE_Type").initSelect({
                                    data: data,
                                    onSelect: function (record) {
                                        if (record) {
                                            $("#AU_UG_Code").val(record.PET_UG_Code);
                                        } else {
                                            $("#AU_UG_Code").val("");
                                        }
                                    }
                                });

                                BindCustomerInfo();
                            }, "json");
                        }
                    });

                }, "json");
            });
        }

        //绑定顾客信息
        function BindCustomerInfo() {
            if (_params.action == "add") {
                _schedule.reset(false);
                $("#btnSave").linkbutton('enable');
                return;
            }

            var params = { action: 'Get_EmployeeInfo', DP_UType: 1, Empl_Code: _params.Empl_Code };
            $.post(_s_url_employee, { o: JSON.stringify(params) }, function (res) {
                if (!$.checkResponse(res)) { return; }

                var name = res.personal ? res.personal.AU_Name : '';
                _summary.create(name, res.summary, res.dealerEmpl);
                _summary.contacts.create(res.contacts);

                var personal = $.extend({}, res.personal, res.dealerEmpl);
                if ($.trim(personal.DE_Picture_FN) !== '') {
                    _personal.picture.create(personal.DE_Picture_FN);
                }
                _personal.set(personal);

                var contacts = _contacts.sort(res.contacts);
                $('#dg_contacts').datagrid('loadData', contacts);

                _schedule.bindData(res.schedule);

                $("#btnSave").linkbutton('enable');
            }, "json");
        }
    });
</script>