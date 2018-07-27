<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRMTreeTool.aspx.cs" Inherits="templete_report_Tool" %>

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
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" type="text/css"/>
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
            width:100%;
            height:100%;
            margin:0;
            padding:0;
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
            width: 100px;
            height: 80px;
            margin-right: 5px;
            margin-bottom:5px;
            background: #F0F0F0;
        }
            .picItem img {
                width: 100px;
                height: 80px;
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

        .person td {
            padding-bottom: 10px;
        }
    </style>
</head>
<body> 
    
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
    }
    _contacts.check = function () {
        _contacts.endEditingNoValid();

        var $dg = $("#dg_contacts");
        var rows = $dg.datagrid('getRows');
        var bValid = true;
        for (var i = 0, len = rows.length; i < len; i++) {
            var row = rows[i];
            if ($.trim(row.type) === '') {
                bValid = false;
                $("#tabs").tabs('select', 1);
                _contacts.onClickCell.call($dg, i, 'type');
                //$dg.datagrid('editCell', { index: i, field: 'type' });
                break;
            }
            if (!row.o) {
                bValid = false;
                $("#tabs").tabs('select', 1);
                _contacts.onClickCell.call($dg, i, 'info');
                //$dg.datagrid('editCell', { index: i, field: 'info' });
                break;
            }
        }
        return bValid;
    }
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
    }
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
    }
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
    }
    //创建添加按钮
    _contacts.createAdd = function () {
        var $btns = $("#tb_contact div.btns");
        var a_o = [
            { icon: 'icon-add', text: _contacts.language.buttons.add, clickFun: '_contacts.add(event,this);' }
        ];
        var a_btns = setBtns(a_o);
        $btns.html(a_btns.join(''));
    }
    //创建联系信息按钮
    _contacts.createAcceptAndCancel = function () {
        var $btns = $("#tb_contactInfo div.btns");
        var a_o = [
            { icon: 'icon-ok', text: _contacts.language.buttons.accept, clickFun: '_contacts.info.accept(event,this);' },
            { icon: 'icon-cancel', text: _contacts.language.buttons.ignore, clickFun: '_contacts.info.cancel(event,this);' }
        ];
        var a_btns = setBtns(a_o);
        $btns.html(a_btns.join(''));
    }
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
    }
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
    }
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
    }
    //获得
    _contacts.get = function () {
        var $dg = $("#dg_contacts");
        //_contacts.endEditing();
        var rows = $dg.datagrid('getRows');
        var deletedRows = $dg.datagrid('getChanges', 'deleted');
        return { changes: rows, deletes: deletedRows };
    }
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
            panelWidth: 660,
            data: data,
            columns: [
                {
                    field: 'AL_Type', title: language.AL_Type, width: 80, editor: {
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
    //cars（车信息）
    //--------------------------------------------------------------------------------------
    var _cars = {};
    //内部行索引
    _cars._rowIndex = -1;
    _cars.language = {
        buttons: {
            add: '<%=Resources.CRMTREEResource.cm_cars_buttons_add%>',
            remove: '<%=Resources.CRMTREEResource.cm_cars_buttons_remove%>',
            edit: '<%=Resources.CRMTREEResource.cm_cars_buttons_edit%>'
        },
        datagrid: {
            Cars: '<%=Resources.CRMTREEResource.cm_cars_datagrid_Cars%>',
            Last_Service: '<%=Resources.CRMTREEResource.cm_cars_datagrid_Last_Service%>',
            Next_Service: '<%=Resources.CRMTREEResource.cm_cars_datagrid_Next_Service%>'
        }
    };
    //添加
    _cars.createAdd = function () {
        var $btns = $("#tb_cars div.btns");
        var a_o = [
            { icon: 'icon-add', text: _cars.language.buttons.add, clickFun: '_cars.add(event,this);' }
        ];
        var a_btns = setBtns(a_o);
        $btns.html(a_btns.join(''));
    };
    //创建
    _cars.create = function () {
        $('#dg_cars').datagrid({
            url: null,
            fit: true,
            toolbar: '#tb_cars',
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
                },
                {
                    field: 'btnLast', align: 'center', width: 170,
                    formatter: function (value, row, index) {
                        var a_o = [
                        { icon: 'icon-remove', title: _cars.language.buttons.remove, clickFun: '_cars.remove(event,this);' },
                        { icon: 'icon-edit', title: _cars.language.buttons.edit, clickFun: '_cars.edit(event,this);' }
                        ];
                        return setBtns(a_o, true).join('');
                    }
                }
            ]]
                , onDblClickRow: function (rowIndex, rowData) {
                    _cars._edit(rowIndex, rowData);
                }
        });
        _cars.createAdd();
    };
    //添加
    _cars.add = function () {
        _cars._rowIndex = -1;
        _cars.picture.removed = [];
        $("#MK_Code").combobox('select', '');
        $("#ci_container_car .car_img").detach();
        _cars.set({});
        $('#win_cars').window('open').window('center');
        $("#MK_Code").combobox('textbox').focus();
    };
    //删除
    _cars.remove = function (e, target) {
        var $dg = $("#dg_cars");
        var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
        var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
        if (rowData.CI_Code > 0) {
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
    //内部编辑
    _cars._edit = function (rowIndex, rowData) {
        _cars._rowIndex = rowIndex;
        _cars.picture.removed = [];
        if (rowData.CI_Code > 0) {
            $.mask.show();
            var params = {
                action: 'Get_Car_InventoryAndLendon',
                id: rowData.CI_Code,
                CI_CS_Code: (rowData.CI_CS_Code > 0 ? rowData.CI_CS_Code : 0)
            };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (res) {
                    var carInfo = $.extend({}, res.CT_Car_Inventory, rowData);

                    _cars._lendon(carInfo, res);
                    _cars.set(carInfo);
                    _cars.picture.set(rowData._img_srcs, false);

                    $('#win_cars').window('open').window('center');
                } else {

                }
                $.mask.hide();
            }, 'json');
        } else {
            if (rowData.CI_CS_Code > 0) {
                $.mask.show();
                var params = { action: 'Get_Car_Lendon', id: rowData.CI_CS_Code };
                $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                    if (res) {
                        _cars._lendon(rowData, res);
                        _cars.set(rowData);
                        _cars.picture.set(rowData._img_srcs, true);

                        $('#win_cars').window('open').window('center');
                    } else {

                    }
                    $.mask.hide();
                }, 'json');
            } else {
                $("#MK_Code").combobox('select', '');
                _cars.set(rowData);
                $('#win_cars').window('open').window('center');
            }
        }
    };
    //编辑
    _cars.edit = function (e, target) {
        var $dg = $("#dg_cars");
        var rowIndex = parseInt($(target).closest('tr.datagrid-row').attr('datagrid-row-index'));
        var rowData = $dg.datagrid('selectRow', rowIndex).datagrid('getSelected');
        _cars._edit(rowIndex, rowData);
        stopPropagation(e);
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
    //确定
    _cars.sure = function () {
        if (!_cars.validate()) return;

        var $dg = $("#dg_cars");
        var row = _cars.getForm();
        row.CP_Picture_FN = _cars.picture.get();
        row._img_srcs = _cars.picture.getSrcs();
        row.Cars = _cars.format(row);
        $('#win_cars').window('close');
        if (_cars._rowIndex >= 0) {
            var rowData = $dg.datagrid('getSelected');

            if (_cars.picture.removed.length > 0) {
                if (!rowData.Picture_Removed) rowData.Picture_Removed = '';
                rowData.Picture_Removed += (rowData.Picture_Removed ? ',' : '') + _cars.picture.removed.join(',');
            }

            if (rowData.CI_Code > 0) {
                rowData.__type = 'updated';
            }
            $.extend(rowData, row);
            $dg.datagrid('updateRow', {
                index: _cars._rowIndex,
                row: rowData
            });
        } else {
            var rowIndex = $dg.datagrid('getRows').length;
            row.__type = 'inserted';
            row.CI_Code = 0;
            $dg.datagrid('appendRow', row).datagrid('selectRow', rowIndex);
        }
    };
    //取消
    _cars.cancel = function () {
        $("#win_cars").window('close');
    };
    //验证
    _cars.validate = function () {
        return $.form.validate({
            textbox: ['CI_VIN', 'CI_Licence'],
            combobox: ['CI_CS_Code', 'CI_YR_Code']
        });
    };
    //取消验证
    _cars.disableValidation = function () {
        $.form.disableValidation({
            textbox: ['CI_VIN', 'CI_Licence'],
            combobox: ['CI_CS_Code', 'CI_YR_Code']
        });
    };
    //获取表单
    _cars.getForm = function () {
        var data = $.form.getData("#frm_car");
        return data;
    }
    //获取列表数据
    _cars.get = function () {
        var $dg = $("#dg_cars");
        var rows = $dg.datagrid('getRows');
        var changes = $.map(rows, function (o, i) {
            return (o.__type === 'inserted' || o.__type === 'updated') ? o : null;
        });
        var deletedRows = $dg.datagrid('getChanges', 'deleted');
        return { changes: changes, deletes: deletedRows };
    };
    //设置
    _cars.set = function (data) {
        $.form.setData("#frm_car", data, "#MK_Code,#CM_Code,#CI_CS_Code");
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
    //图片删除存储
    _cars.picture.removed = [];
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
    //初始化上传
    _cars.picture.plupload = function () {
        //图片上传
        var _uploader = $.plupload({
            container: 'ci_upload_car',
            browse_button: 'ci_browse_button',
            params: { folderName: 'customer_temp' },
            init: {
                FilesAdded: function (up, files) {
                    up.refresh();
                    //创建容器
                    for (var i = 0, len = files.length; i < len; i++) {
                        var file = files[i];
                        file._$container = $('<div class="picItem car_img _add"><div _id="' + file.id + '" class="remove"></div></div>');
                        file._$progress = $('<div class="progress"></div>');
                        file._$progress.appendTo(file._$container);
                        file._$container.appendTo("#ci_container_car");
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
                    var imgsrc = '/plupload/' + up.settings.params.folderName + '/' + file.id + '.' + extendName;
                    file._$progress.detach();
                    file._$container.append('<img src="' + imgsrc + '"/>');
                }
            }
        });

        $("#ci_container_car .car_img>img").live({
            mouseover: function () {
                $(this).fadeTo('fast', 0.6);
            },
            mouseleave: function () {
                $(this).fadeTo('fast', 1);
            },
            click: function () {
                var src = $(this).attr('src');
                var title = '<%=Resources.CRMTREEResource.ed_personal_picture_title%>';
                $.topOpen({
                    title: title,
                    showMask: false,
                    url: "/templete/report/PhotoViewer.html?src=" + src,
                    width: 900,
                    height: 550
                });
            }
        });

        $("#ci_container_car .car_img>.remove").live({
            click: function () {
                var fileName = $.trim($(this).attr("_fileName"));
                var fileId = $.trim($(this).attr("_id"));
                $(this).parent().detach();
                if (fileId && fileId.indexOf('.') === -1) {
                    for (var i in _uploader.files) {
                        if (_uploader.files[i].id === fileId) {
                            _uploader.files.splice(i, 1);
                            break;
                        }
                    }
                }
                if (fileName) {
                    _cars.picture.removed.push(fileName);
                }
                return false;
            }
        });
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

    //设置打开窗体
    _setWindow = function (win) {
        if (win) {
            var opts = win.window('options');
            opts.title = '<%=Resources.CRMTREEResource.cm_window_title%>';
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
                    communications: '<%=Resources.CRMTREEResource.cm_tabs_communications%>',
                    preferences: '<%=Resources.CRMTREEResource.cm_tabs_preferences%>',
                    cars: '<%=Resources.CRMTREEResource.cm_tabs_cars%>',
                    visits: '<%=Resources.CRMTREEResource.cm_tabs_visits%>',
                    appointMent: '<%=Resources.CRMTREEResource.cm_tabs_appointment%>'
                },
                buttons: {
                    save: '<%=Resources.CRMTREEResource.cm_buttons_save%>',
                    close: '<%=Resources.CRMTREEResource.cm_buttons_close%>'
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
                    industry: '<%=Resources.CRMTREEResource.cm_personal_industry%>'
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
+ '            <div id="tabs" class="easyui-tabs" data-options="fit:true,plain:true,border:false" style="border-bottom: 1px solid #B1C242;">'
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
+ '                                <td class="text"><span>*</span>' + language.personal.name + '</td>'
+ '                                <td colspan="3">'
+ '                                    <input id="AU_Code" type="hidden" value="0" />'
+ '                                    <input id="AU_Name" class="easyui-textbox fluid" data-options="required:true,novalidate:true,validType: \'maxLength[50]\'"/>'
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
+ '                                <td class="text">' + language.personal.occupation + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Occupation" class="easyui-combobox" />'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.industry + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Industry" class="easyui-combobox" />'
+ '                                </td>'
+ '                            </tr>'
+ '                        </table>'
+ '                    </div>'
+ '                </div>'
+ '                <div title="' + language.tabs.communications + '" data-options="id:\'com\',iconCls:\'icon-customer-communications\'">'
+ '                </div>'
+ '                <div title="' + language.tabs.preferences + '" data-options="id:\'pre\',iconCls:\'icon-customer-preferences\'" style="padding:5px">'
+ '                </div>'
+ '                <div title="' + language.tabs.cars + '" data-options="id:\'car\',iconCls:\'icon-customer-cars\'" style="padding:0px 0px 1px 0px">'
+ '                    <div class="easyui-panel" data-options="fit:true,border:false">'
+ '                        <table id="dg_cars"></table>'
+ '                    </div>'
+ '                </div>'
+ '                <div title="' + language.tabs.visits + '" data-options="id:\'vis\',iconCls:\'icon-customer-visits\'">'
+ '                </div>'
+ '                <div title="' + language.tabs.appointMent + '" data-options="id:\'app\',iconCls:\'icon-customer-visits\'">'
+ '                </div>'
+ '            </div>'
+ '        </div> '
+ '        <div data-options="region:\'south\',border:false" style="height:auto;text-align:right;padding:10px;">'
+ '            <a id="btnSave" class="easyui-linkbutton" data-options="iconCls:\'icon-save\'" style="width:80px;">' + language.buttons.save + '</a>'
+ '            <a id="btnCancel" class="easyui-linkbutton" data-options="iconCls:\'icon-cancel\'" style="width:80px;margin-left:10px;">' + language.buttons.close + '</a>'
+ '        </div>'
+ '    </div>'
+ '    <div id="tb_contact" style="padding:3px;">'
+ '        <table style="width:100%;" cellpadding="0" cellspacing="0">'
+ '            <tr>'
+ '                <td style="width: 100%;"><div class="btns" style="margin-right:5px;"></div></td>'
+ '                <td style="white-space:nowrap;text-align:right">'
+ '                    '
+ '                </td>'
+ '            </tr>'
+ '        </table>'
+ '    </div>'
+ '    <div id="tb_contactInfo" >'
+ '        <table style="width:100%;" cellpadding="0" cellspacing="0">'
+ '            <tr>'
+ '                <td style="width: 100%;"><div class="btns" style="margin-right:5px;"></div></td>'
+ '                <td style="white-space:nowrap;text-align:right"></td>'
+ '            </tr>'
+ '        </table>'
+ '    </div>'
+ '    <div id="tb_cars" style="padding:3px;">'
+ '        <table style="width:100%;" cellpadding="0" cellspacing="0">'
+ '            <tr>'
+ '                <td style="width: 100%;"><div class="btns" style="margin-right:5px;"></div></td>'
+ '                <td style="white-space:nowrap;text-align:right;"></td>'
+ '            </tr>'
+ '        </table>'
+ '    </div>'
+ '    <div id="win_cars" class="easyui-window" title="' + language.cars.title + '" data-options="minimizable:false,closed:true,modal:true,height:335,width:600">'
+ '        <div class="easyui-layout" data-options="fit:true">'
+ '            <div data-options="region:\'north\',border:false,height:160" style="padding: 0px; padding-top: 5px; border-bottom: 1px solid #DEDEDE;position:relative;">'
+ '                <table id="frm_car" class="form" border="0" cellpadding="3" cellspacing="2">'
+ '                    <tr>'
+ '                        <td class="text">' + language.cars.form.Make + '</td>'
+ '                        <td colspan="3">'
+ '                            <input id="CI_Code" type="hidden" value="0" />'
+ '                            <input id="MK_Code" class="easyui-combobox showEmptyText" />'
+ '                        </td>'
+ '                    </tr>'
+ '                    <tr>'
+ '                        <td class="text">' + language.cars.form.Model + '</td>'
+ '                        <td>'
+ '                            <input id="CM_Code" class="easyui-combobox showEmptyText" />'
+ '                        </td>'
+ '                        <td class="text">' + language.cars.form.VIN + '</td>'
+ '                        <td>'
+ '                            <input id="CI_VIN" class="easyui-textbox" data-options="novalidate:true,validType: \'maxLength[20]\'"/>'
+ '                        </td>'
+ '                    </tr>'
+ '                    <tr>'
+ '                        <td class="text">' + language.cars.form.Style + '</td>'
+ '                        <td>'
+ '                            <input id="CI_CS_Code" class="easyui-combobox showEmptyText" data-options="required:true,novalidate:true" />'
+ '                        </td>'
+ '                        <td class="text">' + language.cars.form.Mileage + '</td>'
+ '                        <td>'
+ '                            <input id="CI_Mileage" class="easyui-numberbox" />'
+ '                        </td>'
+ '                    </tr>'
+ '                    <tr>'
+ '                        <td class="text">' + language.cars.form.Years + '</td>'
+ '                        <td>'
+ '                            <input id="CI_YR_Code" class="easyui-combobox showEmptyText" data-options="editable:true,required:true,novalidate:true" />'
+ '                        </td>'
+ '                        <td class="text">' + language.cars.form.Licence + '</td>'
+ '                        <td>'
+ '                            <input id="CI_Licence" class="easyui-textbox" data-options="novalidate:true,validType: \'maxLength[15]\'"/>'
+ '                        </td>'
+ '                    </tr>'
+ '                    <tr>'
+ '                        <td class="text">' + language.cars.form.ColorExternal + '</td>'
+ '                        <td>'
+ '                            <input id="CI_Color_E" class="easyui-combobox" />'
+ '                        </td>'
+ '                        <td class="text">' + language.cars.form.ColorInternal + '</td>'
+ '                        <td>'
+ '                            <input id="CI_Color_I" class="easyui-combobox" />'
+ '                        </td>'
+ '                    </tr>'
+ '                </table>'
+ '            </div>'
+ '            <div data-options="region:\'center\',border:false" style="position: relative; border-bottom: 1px solid #B1C242; overflow-x: hidden;">'
+ '                <div id="ci_container_car" style="position:relative;margin:5px;">'
+ '                    <div id="ci_upload_car" class="picItem" style="position: relative; width: 100px;height: 80px;overflow:hidden;background-color: transparent;">'
+ '                        <img src="/scripts/jquery-easyui/themes/icons/add_car.png" style="position:absolute;" />'
+ '                    </div>'
+ '                </div>'
+ '            </div>'
+ '            <div data-options="region:\'south\',border:false" style="height:auto;text-align:right;padding:10px;">'
+ '                <a class="easyui-linkbutton" data-options="iconCls:\'icon-ok\',onClick:_cars.sure" style="width:80px;">' + language.cars.buttonos.accept + '</a>'
+ '                <a class="easyui-linkbutton" data-options="iconCls:\'icon-cancel\',onClick:_cars.cancel" style="width:80px;margin-left:10px;">' + language.cars.buttonos.ignore + '</a>'
+ '            </div>'
+ '        </div>'
+ '    </div>';
            return html;
        }

        //初始化
        (function Init() {
            if (_params.action !== "add" && !(_params.AU_Code > 0)) {
                $.msgTips.info('<%=Resources.CRMTREEResource.cm_msg001%>');
                return;
            }

            var html = getHTML();
            $(window.document.body).append(html);
            $.parser.parse();

            BindData();
            //list
            _contacts.create();
            _cars.create();
            //buttons
            InitBtns_Save();

            $.form.fluid('#frm_personal');
            $.form.setEmptyText('#frm_car');

            $("#win_cars").window({
                onBeforeOpen: function () {
                    _cars.disableValidation();
                }
            });

            _cars.picture.plupload();

            $(window).unload(function () {
                try {
                    $("#ci_upload_car").empty();
                } catch (e) {

                }
            });

            InitTabs();
        })();

        //初始化面板页
        function InitTabs() {
            var $tabs = $("#tabs");
            //_params._tab_id = 'vis';
            $tabs.tabs({
                onSelect: function (title, index) {
                    if (index === 2) {
                        $("#AU_Name").textbox('textbox').focus();
                        return;
                    }
                    if (index === 3) {
                        LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=24&AU_Code=' + _params.AU_Code);
                        return;
                    }
                    if (index === 6) {
                        LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=23&AU_Code=' + _params.AU_Code);
                        return;
                    }
                    if (index === 7) {
                        LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=40&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                        return;
                    }
                }
            });

            var tabId = $.trim(_params._tab_id);
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
            //加载下拉列表数据
            $.bindWords({
                wordIds: [9, 31, 23, 15, 18]
                , selects: [
                    { selectId: 'AU_Gender', wordId: 9 }
                    , { selectId: 'AU_ID_Type', wordId: 31 }
                    , { selectId: 'AU_Education', wordId: 23 }
                    , { selectId: 'AU_Occupation', wordId: 15 }
                    , { selectId: 'AU_Industry', wordId: 18 }
                ]
                , onLoad: function () {
                    BindContactsSelects();
                }
            });
        }
        //联系方式
        function BindContactsSelects() {
            $.getWords([44, 47, 55], function (data) {
                _contacts.info._words = data ? data : {};

                $.each(_contacts.info._words, function (n, w) {
                    $.each(w, function (i, o) {
                        if (o.selected) {
                            _contacts.info._words_selected[n] = o.value;
                            return false;
                        }
                    });
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
                    })
                    $("#CM_Code").initSelect({ onSelect: _cars.onSelect.CM_Code });
                    $("#CI_YR_Code").initSelect({ editable: true, data: res.CT_Years });

                    $("#CI_Color_E,#CI_Color_I").initSelect({ editable: true, data: res.CT_Color_List });
                }

                BindCustomerInfo();
            }, "json");
        }
        //顾客信息
        function BindCustomerInfo() {
            if (_params.action == "add") {
                $("#btnSave").linkbutton('enable');
                return;
            }

            var params = { action: 'Get_CustomerInfo', AU_Code: _params.AU_Code };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (!$.checkResponse(res)) { return; }

                var name = res.personal ? res.personal.AU_Name : '';
                res.summary.EX_AU_Activate_dt = res.personal.EX_AU_Activate_dt;
                _summary.create(name, res.summary);
                _summary.contacts.create(res.contacts);

                _personal.set(res.personal);

                var contacts = _contacts.sort(res.contacts);
                $('#dg_contacts').datagrid('loadData', contacts);

                $('#dg_cars').datagrid('loadData', res.cars);

                $("#btnSave").linkbutton('enable');
            }, "json");
        }

        //格式化按钮
        function formatBtns(btns, index) {
            var a_btns = [];
            if ($.isArray(btns)) {
                for (var i = 0, len = btns.length; i < len; i++) {
                    a_btns.push(btns[i] ? $.format(btns[i], index) : btns[i]);
                }
            }
            return a_btns;
        }
        //保存
        function InitBtns_Save() {
            $("#btnSave").linkbutton({ onClick: Save }).linkbutton('disable');
        }
        //保存数据
        function Save() {
            //验证联系信息
            if (!_contacts.check()) {
                return;
            }

            //验证个人信息
            if (!_personal.validate()) {
                $("#tabs").tabs('select', 2);
                return;
            }

            var params = { action: 'Save_Customer' };
            params.contacts = _contacts.get();
            params.personal = _personal.get();
            params.cars = _cars.get();
            //return;

            $.mask.show();
            $("#btnSave").linkbutton('disable');
            var s_params = JSON.stringify(params);
            $.post(_s_url, { o: s_params }, function (res) {
                $.mask.hide();
                $("#btnSave").linkbutton('enable');
                if ($.checkResponse(res)) {
                    closeWindow();
                    top[_params._winID]._datagrid.action.refresh();
                }
            }, "json");
        }

        //关闭窗体
        function closeWindow() {
            if (window._closeOwnerWindow) { window._closeOwnerWindow(); }
        }

        $("#btnCancel").click(function () {
            closeWindow();
        });
    });
</script>