<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerManager.aspx.cs" Inherits="templete_report_CustomerManager" %>

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


        /*
        upload
        */
        img {
            border: 0;
            margin: 0;
            padding: 0;
        }


        .person td {
            padding-bottom: 10px;
        }

        /*fieldset
        {
            border:solid 1px #aaa;
            padding:0px
        }        
        .hideFieldset
        {
            border-left:0;
            border-right:0;
            border-bottom:0;
        }*/
    </style>
</head>
<body>
</body>
</html>
<script type="text/javascript">
    //ajax地址
    var _s_url = '/handler/Reports/CustomerManage.aspx';
    var _crm_url = '/handler/CRMhandle.aspx';
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
    _contacts.GetWords = [];
    _contacts.language = {
        type: {
            'address': '<%=Resources.CRMTREEResource.ed_contacts_type_address%>',
            'phone': '<%=Resources.CRMTREEResource.ed_contacts_type_phone%>',
            'eMail': '<%=Resources.CRMTREEResource.ed_contacts_type_eMail%>',
            'messaging': '<%=Resources.CRMTREEResource.ed_contacts_type_messaging%>'
        }, buttons: {
            add: '<%=Resources.CRMTREEResource.em_contacts_add%>',
            remove: '<%=Resources.CRMTREEResource.em_contacts_remove%>',
            up: '<%=Resources.CRMTREEResource.em_contacts_up%>',
            down: '<%=Resources.CRMTREEResource.em_contacts_down%>',
            accept: '<%=Resources.CRMTREEResource.em_contacts_accept%>',
            ignore: '<%=Resources.CRMTREEResource.em_contacts_ignore%>'
        }, Title: {
            GX: '<%=Resources.CRMTREEResource.CLT_GUANXI%>',
            XM: '<%=Resources.CRMTREEResource.CLT_NAME%>',
            FS: '<%=Resources.CRMTREEResource.CLT_LIANXIFANGSHI%>',
            XX: '<%=Resources.CRMTREEResource.CLT_XINXI%>',
            NU: '<%=Resources.CRMTREEResource.CLT_DoNotUse%>',
        }

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
            showHeader: true,
            border: false,
            loadMsg: '',
            columns: [[
                {
                    field: 'name_1', title: _contacts.language.Title.XM, width: 100,
                    formatter: function (value, row) {
                        return row.AU_Name;
                    },
                    editor: {
                        type: 'combobox',
                        options: {
                            onSelect: function (record) {
                                var row = $dg.datagrid('getSelected');
                                row.relation_id = record.value;
                                window.setTimeout(function () {
                                    _contacts.endEditingNoValid();
                                    var rowIndex = $dg.datagrid('getRowIndex', row);
                                    _contacts.onClickCell.call($dg, rowIndex, 'info');
                                }, 0);
                            }
                        }
                    }
                },
                {
                    field: 'relation_1',
                    title: _contacts.language.Title.GX,
                    width: 100,
                    formatter: function (value, row) {
                        return _contacts.format_data(row.relation_id);
                    },
                    //editor: {
                    //    type: 'combobox',
                    //    options: {
                    //        onSelect: function (record) {
                    //            var row = $dg.datagrid('getSelected');
                    //            window.setTimeout(function () {
                    //                _contacts.endEditingNoValid();
                    //                var rowIndex = $dg.datagrid('getRowIndex', row);
                    //                _contacts.onClickCell.call($dg, rowIndex, 'info');
                    //            }, 0);
                    //        }
                    //    }
                    //}
                },                
                {
                    field: 'contact_type',
                    title: _contacts.language.Title.FS,
                    width: 100,
                    formatter: function (value, row) {
                        return _contacts.Typeformat_data(row.relation_type);
                    },
                    editor: {
                        type: 'combobox',
                        options: {
                            novalidate: true,
                            required: true,
                            onSelect: function (record) {
                                var row = $dg.datagrid('getSelected');
                                window.setTimeout(function () {
                                    _contacts.endEditingNoValid();
                                    var rowIndex = $dg.datagrid('getRowIndex', row);
                                    _contacts.onClickCell.call($dg, rowIndex, 'info');
                                }, 0);
                            }
                        }
                    }
                },
                {
                    field: 'info',
                    title: _contacts.language.Title.XX,
                    width: 400,
                    formatter: function (value, row) {
                        //return row.info;
                        return _contacts.infoformat_data(row.relation_type, row.contact_id, row.info, row.o);
                    },
                    editor: {
                        type: 'combo',
                        options: {
                            novalidate: true,
                            required: true
                        }
                    }
                },
                {
                    field: 'donotuse',
                    title: _contacts.language.Title.NU,
                    width: 30,
                    formatter: function (value, row) {
                        return row.DonotUse;
                    },
                    editor: {
                        type: 'checkbox',
                        options: {
                            novalidate: true,
                            required: false
                        }
                    }
                },
                {
                    field: 'btnLast',
                    align: 'center',
                    width: 100,
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
    //点击单元格,开启编辑
    _contacts.onClickCell = function (index, field) {
        //**图标按钮不处理
        if (field === 'btnLast') return;

        _contacts.endEditingNoValid();
        var $dg = $(this);
        var rowData = $dg.datagrid('selectRow', index).datagrid('getSelected');
        if (field === "relation_1" && rowData.add == true) {
            $dg.datagrid('editCell', { index: index, field: field });
            var ed = $dg.datagrid('getEditor', { index: index, field: field });
            if (ed && ed.type === 'combobox') {
                ed.target.combobox('loadData', _contacts.GetWords);
                ed.target.combobox('showPanel').combobox('setValue', $dg.datagrid('getRows')[index].relation_id);
            }
        } else if (field === "name_1" && rowData.add == true) {
            $dg.datagrid('editCell', { index: index, field: field });
            var ed = $dg.datagrid('getEditor', { index: index, field: field });
            if (ed && ed.type === 'combobox') {
                ed.target.combobox({
                    valueField: 'AU_Code',
                    textField: 'AU_Name',
                }).combobox('loadData', _contacts.GetWords);
                ed.target.combobox('showPanel').combobox('setValue', $dg.datagrid('getRows')[index].AU_Code);
            }
        } else if (field === "contact_type" && rowData.add == true) {
            $dg.datagrid('editCell', { index: index, field: field });
            var ed = $dg.datagrid('getEditor', { index: index, field: field });
            if (ed && ed.type === 'combobox') {
                ed.target.combobox('loadData', [
                                    { text: _contacts.language.type.address, value: '1' },
                                    { text: _contacts.language.type.phone, value: '2' },
                                    { text: _contacts.language.type.eMail, value: '3' },
                                    { text: _contacts.language.type.messaging, value: '4' }
                ]);
                ed.target.combobox('showPanel').combobox('setValue', $dg.datagrid('getRows')[index].relation_type);
            }
        } else if (field === "info") {
            $dg.datagrid('editCell', { index: index, field: field });
            var ed = $dg.datagrid('getEditor', { index: index, field: field });
            var $info = ed.target;
            _contacts.info.$info = $info;
            switch (rowData.relation_type) {
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
        }
    }
    //取消编辑
    _contacts.cancelEdit = function () {
        var $dg = $("#dg_contacts");
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            $dg.datagrid('cancelEdit', i);
        }
    }
    //检查
    _contacts.check = function () {
        _contacts.endEditingNoValid();
        var $dg = $("#dg_contacts");
        var rows = $dg.datagrid('getRows');
        var bValid = true;
        for (var i = 0, len = rows.length; i < len; i++) {
            var row = rows[i];
            if ($.trim(row.relation_type) === '') {
                bValid = false;
                $("#tabs").tabs('select', 1);
                _contacts.onClickCell.call($dg, i, 'type');
                break;
            }
            //if (!row.o) {
            //    bValid = false;
            //    $("#tabs").tabs('select', 1);
            //    _contacts.onClickCell.call($dg, i, 'info');
            //    break;
            //}
        }
        return bValid;
    }
    //结束编辑不验证
    _contacts.endEditingNoValid = function () {
        var $dg = $("#dg_contacts");
        var rows = $dg.datagrid('getRows');
        for (var i = 0, len = rows.length; i < len; i++) {
            var ed_relation = $dg.datagrid('getEditor', { index: i, field: 'relation_1' });
            if (ed_relation != null) {
                var edValue = $(ed_relation.target).combobox('getValue');
                if (edValue != null && edValue != "") {
                    $dg.datagrid('getRows')[i].relation_id = edValue;
                }
            }
            var ed_Name = $dg.datagrid('getEditor', { index: i, field: 'name_1' });
            if (ed_Name != null) {
                var edValue = $(ed_Name.target).combobox('getValue');
                var edText = $(ed_Name.target).combobox('getText');
                if (edValue != null && edValue != "") {
                    $dg.datagrid('getRows')[i].AU_Code = edValue;
                    $dg.datagrid('getRows')[i].AU_Name = edText;
                }
            }
            var ed_type = $dg.datagrid('getEditor', { index: i, field: 'contact_type' });
            if (ed_type != null) {
                var ed3Text = $(ed_type.target).combobox('getValue');
                if (ed3Text != null && ed3Text != "") {
                    $dg.datagrid('getRows')[i].relation_type = ed3Text;
                }
            }
            var ed_info = $dg.datagrid('getEditor', { index: i, field: 'info' });
            if (ed_info != null) {
                var ed3Text = $(ed_info.target).combo('getText');
                if (ed3Text != null && ed3Text != "") {
                    $dg.datagrid('getRows')[i].info = ed3Text;
                    $dg.datagrid('getRows')[i].o = _contacts.info.$dg.datagrid('getRows')[0];
                    //_contacts.info.cancel();
                }
            }
            $dg.datagrid('endEdit', i);           
        }
        //_contacts.cancelEdit();
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
    //添加
    _contacts.add = function () {
        var $dg = $("#dg_contacts");
        _contacts.endEditingNoValid()
        var contact = { AU_Code: -1, AU_Name: "", relation_id: 0, relation_type: "1", info: "",add:true,Keys:-1 };
        var row = $dg.datagrid('getSelected');
        var rowIndex = undefined;
        if (row) {
            contact.AU_Code = row.AU_Code;
            contact.AU_Name = row.AU_Name;
            contact.relation_id = row.relation_id;
            contact.relation_type = row.relation_type;
            //contact.contact_id = row.contact_id;
            rowIndex = $dg.datagrid('getRowIndex', row) + 1;
        } else {
            rowIndex = $dg.datagrid('getRows').length;
            row = $dg.datagrid('selectRow', rowIndex - 1).datagrid('getSelected');
            contact.AU_Code = row.AU_Code;
            contact.AU_Name = row.AU_Name;
            contact.relation_id = row.relation_id;
            contact.relation_type = row.relation_type;
            //contact.contact_id = row.contact_id;
        }
        $dg.datagrid('insertRow', { index: rowIndex, row: contact });
        $dg.datagrid('selectRow', rowIndex);
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
        _contacts.endEditing();
        var rows = $dg.datagrid('getRows');
        var insertedRows = $dg.datagrid('getChanges', 'inserted');
        var updatedRows = $dg.datagrid('getChanges', 'updated');
        var deletedRows = $dg.datagrid('getChanges', 'deleted');
        return { row: rows, insert: insertedRows, update: updatedRows, deletes: deletedRows };
    }
    /* Create Wangqi Date:2015/03/24
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
    _contacts.infoformat_data = function (type, id, data, o) {
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
                var _o = _contacts.infoformat_data2(type, o);
                if (_o == null || _o == "") {
                    _o = $.trim(data);
                }
                _info = $.trim(info.text) + ":" + _o;
            }
        });
        return _info;
    }
    _contacts.infoformat_data2 = function (type, data) {
        if (!data) return "";
        var a_info = [];
        if (type == 1) {
            a_info = [
                        $.trim(data.AL_Add1),
                        $.trim(data.AL_Add2),
                        $.trim(data.AL_District),
                        $.trim(data.AL_City),
                        $.trim(data.AL_State),
                        $.trim(data.AL_Zip)
            ];
        } else if (type == 2) {
            var area_code = $.trim(data.PL_PP_Code);
            area_code = area_code != '' ? area_code + '-' : '';
            a_info = [
                area_code + $.trim(data.PL_Number)
            ];
        } else if (type == 3) {
            a_info = [
                   $.trim(data.EL_Address)
            ];
        } else if (type == 4) {
            a_info = [
                   $.trim(data.ML_Handle)
            ];
        }

        var s_info = $.trim(a_info.join(' '));
        return s_info;
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
            var info = $dg.datagrid('getRows')[0];
            _contacts.info.acceptconf(info);
            var rowContact = $("#dg_contacts").datagrid('getSelected');
            rowContact.o = info;
            if (_contacts.info.$info) {
                _contacts.info.$info.combo('hidePanel')
                .combo('setText', _contacts.info.format(rowContact.relation_type, rowContact.o))
            }
            _contacts.endEditingNoValid();
        }
    };
    //应用配置
    _contacts.info.acceptconf = function (info) {
        var rowContact = $("#dg_contacts").datagrid('getSelected');
        if (rowContact.relation_type == "1") {
            rowContact.contact_id = info.AL_Type;
            rowContact.info = $.trim(info.AL_Add1) +
                        $.trim(info.AL_Add2) +
                        $.trim(info.AL_District) +
                        $.trim(info.AL_City) +
                        $.trim(info.AL_State) +
                        $.trim(info.AL_Zip);
        } else if (rowContact.relation_type == "2") {
            rowContact.contact_id = info.PL_Type;
            rowContact.info = $.trim(info.PL_Number);
        } else if (rowContact.relation_type == "3") {
            rowContact.contact_id = info.EL_Type;
            rowContact.info = $.trim(info.EL_Address);
        } else if (rowContact.relation_type == "4") {
            rowContact.contact_id = info.ML_Type;
            rowContact.info = $.trim(info.ML_Handle);
        }
    }
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
        var data = opts.data ? opts.data : {};
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
        $info.combo('setText', $.trim(row.info)).combo('showPanel').combo('textbox').focus();
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
        var row = $("#dg_contacts").datagrid('getSelected');
        var defaultValue = _contacts.info._words_selected["_55"];
        var data = defaultValue ? { AL_Type: defaultValue } : {};
        if (row.contact_id) {
            data = row.contact_id;
        }                
        var columns = _isEn ? [
                {
                    field: 'AL_Type', title: language.AL_Type, width: 80, editor: {
                        type: 'combobox', options: {
                            novalidate: true,
                            required: true,
                            data: _contacts.info._words["_55"]
                        }
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
        ]: [
                {
                    field: 'AL_Type', title: language.AL_Type, width: 80, editor: {
                        type: 'combobox', options: {
                            novalidate: true,
                            required: true,
                            data: _contacts.info._words["_55"]
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
                    field: 'AL_City', title: language.AL_City, width: 100, editor: {
                        type: 'textbox', options: {
                            novalidate: true,
                            validType: 'maxLength[10]'
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
                    field: 'AL_Zip', title: language.AL_Zip, width: 60, editor: {
                        type: 'textbox', options: {
                            novalidate: true,
                            validType: 'maxLength[10]'
                        }
                    }
                }
        ];
        if (row.add) {
            var params = { action: 'Get_contact_value_default', contact_Type: 1, AU_Code: row.AU_Code };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (res.length > 0) {
                    _contacts.info.createDataGrid($info, { panelHeight: 100, panelWidth: 660, data: res[0], columns: columns });
                } else {
                    _contacts.info.createDataGrid($info, { panelHeight: 100, panelWidth: 660, data: data, columns: columns });
                }                
            }, "json");
        } else {
            var params = { action: 'Get_contact_value', contact_Type: 1, Keys: row.Keys };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (res.length > 0) {
                    _contacts.info.createDataGrid($info, { panelHeight: 100, panelWidth: 660, data: res[0], columns: columns });
                } else {
                    _contacts.info.createDataGrid($info, { panelHeight: 100, panelWidth: 660, data: data, columns: columns });
                }
            }, "json");
        }      
    };
    //电话
    _contacts.info.phone = function ($info) {
        var language = {
            PL_Type: '<%=Resources.CRMTREEResource.em_contacts_PL_Type%>',
            PL_Number: '<%=Resources.CRMTREEResource.em_contacts_PL_Number%>'
        };
        var row = $("#dg_contacts").datagrid('getSelected');
        var defaultValue = _contacts.info._words_selected["_47"];
        var data = defaultValue ? { PL_Type: defaultValue } : {};
        if (row.contact_id) {
            data = row.contact_id;
        }

        var columns = [
            {
                field: 'PL_Type', title: language.PL_Type, width: 130, editor: {
                    type: 'combobox', options: {
                        novalidate: true,
                        required: true,
                        data: _contacts.info._words["_47"]
                    }
                }
            },
            {
                field: 'PL_PP_Code', title: '<%=Resources.CRMTREEResource.AreaCode%>', width: 70, editor: {
                    type: 'textbox', options: {
                        novalidate: true,
                        required: false,
                        validType: 'maxLength[4]'
                    }
                }
            },
            {
                field: 'PL_Number', title: language.PL_Number, width: 170, editor: {
                    type: 'textbox', options: {
                        novalidate: true,
                        required: true,
                        validType: 'maxLength[12]'
                    }
                }
            }
        ];
        if (row.add) {
            var params = { action: 'Get_contact_value_default', contact_Type: 2, AU_Code: row.AU_Code };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (res.length > 0) { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: res[0], columns: columns }); }
                else { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: data, columns: columns }); }                
            }, "json");
        } else {
            var params = { action: 'Get_contact_value', contact_Type: 2, Keys: row.Keys };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (res.length > 0) { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: res[0], columns: columns }); }
                else { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: data, columns: columns }); }
            }, "json");
        }
    };
//邮件
_contacts.info.email = function ($info) {
    var language = {
        EL_Type: '<%=Resources.CRMTREEResource.em_contacts_EL_Type%>',
            EL_Address: '<%=Resources.CRMTREEResource.em_contacts_EL_Address%>'
    };
    var row = $("#dg_contacts").datagrid('getSelected');
    var defaultValue = _contacts.info._words_selected["_44"];
    var data = defaultValue ? { EL_Type: defaultValue } : {};
    if (row.contact_id) {
        data = row.contact_id;
    }
    
    var columns =[
            {
                field: 'EL_Type', title: language.EL_Type, width: 130, editor: {
                    type: 'combobox', options: {
                        novalidate: true,
                        required: true,
                        data: _contacts.info._words["_44"]
                    }
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
    ];
    if (row.add) {
        var params = { action: 'Get_contact_value_default', contact_Type: 3, AU_Code: row.AU_Code };
        $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
            if (res.length > 0) { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: res[0], columns: columns }); }
            else { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: data, columns: columns }); }
        }, "json");
    } else {
        var params = { action: 'Get_contact_value', contact_Type: 3, Keys: row.Keys };
        $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
            if (res.length > 0) { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: res[0], columns: columns }); }
            else { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: data, columns: columns }); }
        }, "json");
    }
    };
    //消息
    _contacts.info.messaging = function ($info) {
        var language = {
            ML_Type: '<%=Resources.CRMTREEResource.em_contacts_ML_MC_Code%>',
            ML_Handle: '<%=Resources.CRMTREEResource.em_contacts_ML_Handle%>'
        };
        var row = $("#dg_contacts").datagrid('getSelected');
        var defaultValue = _contacts.info._words_selected["_4064"];
        var data = defaultValue ? { ML_Type: defaultValue } : {};
        if (row.contact_id) {
            data = row.contact_id;
        }
        var columns = [
            {
                field: 'ML_Type',
                title: language.ML_Type,
                width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        novalidate: true,
                        required: true,
                        data: _contacts.info._words["_4064"],
                        onSelect: function (record) {
                            //$dg.datagrid('getSelected').relation_id = record.value;
                            //_contacts.endEditingNoValid();
                            //var row = $c.datagrid('getSelected');
                        }
                    }
                }
            },
            {
                field: 'ML_Handle',
                title: language.ML_Handle,
                width: 240,
                editor: {
                    type: 'textbox', options: {
                        novalidate: true,
                        required: true,
                        validType: 'maxLength[20]'
                    }
                }
            }
        ];
        if (row.add) {
            var params = { action: 'Get_contact_value_default', contact_Type: 4, AU_Code: row.AU_Code };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (res.length > 0) { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: res[0], columns: columns }); }
                else { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: data, columns: columns }); }
            }, "json");
        } else {
            var params = { action: 'Get_contact_value', contact_Type: 4, Keys: row.Keys };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (res.length > 0) { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: res[0], columns: columns }); }
                else { _contacts.info.createDataGrid($info, { panelHeight: 90, panelWidth: 400, data: data, columns: columns }); }
            }, "json");
        }
    };
    //格式化联系信息
    _contacts.info.format = function (type, row) {
        if (!row) return "";
        var a_info = [];
        switch (type) {
            case '1':
                a_info = _isEn ? [
                    $.trim(row.AL_Type_Text),
                    ':',
                    $.trim(row.AL_Add1),
                    $.trim(row.AL_Add2),
                    $.trim(row.AL_District),
                    $.trim(row.AL_City),
                    $.trim(row.AL_State),
                    $.trim(row.AL_Zip)
                ] : [
                    $.trim(row.AL_Type_Text),
                    ':',
                    $.trim(row.AL_State),
                    $.trim(row.AL_City),
                    $.trim(row.AL_District),
                    $.trim(row.AL_Add1),
                    $.trim(row.AL_Add2),
                    $.trim(row.AL_Zip)
                ];
                break;
            case '2':
                var area_code = $.trim(row.PL_PP_Code);
                area_code = area_code != '' ? area_code + '-' : '';
                a_info = [
                    $.trim(row.PL_Type_Text),
                    ':',
                    area_code + $.trim(row.PL_Number)
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
                    $.trim(row.contact_id_Text),
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
    _personal.animate = function (au_type) {
        if (au_type === '' || au_type == 0) {
            $("#frm_personal tr.tr_item").show();
        } else {
            $("#frm_personal tr.tr_item").hide();

            $("#AU_ID_No,#AU_Dr_Lic").textbox('setValue', '');
            $("#AU_B_date").textbox('setValue', '');
            $("#AU_Gender,#AU_ID_Type,#AU_Education,#AU_Occupation,#AU_Industry").combobox('setValue', '');

        }
    }
    //获取
    _personal.get = function () {
        var data = $.form.getData("#frm_personal");
        if ($.trim(data.AU_Gender) != '') {
            data.AU_Gender = (data.AU_Gender == 1);
        }

        var au_type = $.trim(data.AU_Type);
        if (!(au_type == '' || au_type == 0)) {
            $.extend(data, {
                AU_Dr_Lic: null,
                AU_Gender: null,
                AU_ID_Type: null,
                AU_ID_No: null,
                AU_Education: null,
                AU_Occupation: null,
                AU_Industry: null,
                AU_B_date: null
            });
        }

        return data;
    }

    //--------------------------------------------------------------------------------------
    //relation（所属关系信息）
    //--------------------------------------------------------------------------------------
    var _relation = {
        data: [],
        _id: '#dg_relation',
        _rowIndex: -1
    };
    _relation.language = {
        buttons: {
            add: '<%=Resources.CRMTREEResource.cm_cars_Contact_buttons_add%>'
            //,remove: 'Remove'
        }
    };
//取消编辑
_relation.cancelEdit = function () {
    var $dg = $(_relation._id);
    var rows = $dg.datagrid('getRows');
    for (var i = 0, len = rows.length; i < len; i++) {
        $dg.datagrid('cancelEdit', i);
    }
};
//检查所属关系数据
_relation.check = function () {
    _relation.endEditingNoValid();

    var $dg = $(_relation._id);
    var rows = $dg.datagrid('getRows');
    var bValid = true;
    for (var i = 0, len = rows.length; i < len; i++) {
        var row = rows[i];
        if ($.trim(row.DL_Relation) === '') {
            bValid = false;
            _relation.onClickCell.call($dg, i, 'DL_Relation');
            break;
        }
        if ($.trim(row.AU_Name) === '') {
            bValid = false;
            _relation.onClickCell.call($dg, i, 'AU_Name');
            break;
        }
    }
    return bValid;
};
//结束编辑不验证
_relation.endEditingNoValid = function () {
    var $dg = $(_relation._id);
    var rows = $dg.datagrid('getRows');
    for (var i = 0, len = rows.length; i < len; i++) {
        var ed = $dg.datagrid('getEditor', { index: i, field: 'DL_Relation' });
        if (ed && ed.type === 'combobox') {
            var typeText = $(ed.target).combobox('getText');
            rows[i][ed.field + '_Text'] = typeText;
        }
        $dg.datagrid('endEdit', i);
    }
}
//结束编辑需要验证
_relation.endEditing = function () {
    var $dg = $(_relation._id);
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
_relation.onClickCell = function (index, field) {
    if (field !== 'btnLast') {
        _relation.endEditingNoValid();
        var $dg = $(this);
        var rowData = $dg.datagrid('selectRow', index).datagrid('getSelected');

        //已保存数据不能修改类型
        if (field === 'DL_Relation' && rowData.DL_AU_Code > 0) return;

        if (field === 'AU_Name') {
            _relation._rowIndex = index;
            if (rowData.EX_User === undefined) {
                if (rowData.DL_AU_Code > 0) {
                    var params = {
                        action: 'Get_Driver',
                        DL_AU_Code: rowData.DL_AU_Code
                    };
                    //$.mask.show();
                    $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                        //$.mask.hide();
                        if (!$.checkResponse(res)) { return; }

                        var driver = {};
                        $.each(res, function (n, d) {
                            driver["Relation_" + n] = d;
                        });

                        _relation.set(driver);

                        $("#w_relation").window('open');
                    }, "json");
                } else {
                    _relation.reset();
                    $("#w_relation").window('open');
                }
            } else {
                _relation.set(rowData.EX_User);
                $("#w_relation").window('open');
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
_relation.createAdd = function () {
    var $btns = $("#tb_relation div.btns");
    var a_o = [
        { icon: 'icon-add', text: _relation.language.buttons.add, clickFun: '_relation.add(event,this);' }
    ];
    var a_btns = setBtns(a_o);
    $btns.html(a_btns.join(''));
}
//创建
_relation.create = function () {
    var $dg = $(_relation._id);
    $dg.datagrid({
        url: null,
        fit: true,
        toolbar: '#tb_relation',
        rownumbers: true,
        singleSelect: true,
        showHeader: false,
        border: false,
        nowrap: false,
        loadMsg: '',
        columns: [[
            {
                field: 'DL_Relation', title: 'Relation', width: 150,
                formatter: function (value, row) {
                    return row.DL_Relation_Text ? row.DL_Relation_Text : value;
                },
                editor: {
                    type: 'combobox', options: {
                        novalidate: true,
                        required: true,
                        data: _relation.data,
                        onSelect: function (record) {
                            var row = $dg.datagrid('getSelected');
                            row.EX_AU_Name = '';
                            row.EX_Relation_Info = null;
                            window.setTimeout(function () {
                                _relation.endEditingNoValid();
                                var rowIndex = $dg.datagrid('getRowIndex', row);
                                _relation.onClickCell.call($dg, rowIndex, 'AU_Name');
                            }, 0);
                        }
                    }
                }
            },
            {
                field: 'AU_Name', title: 'Name', width: 400
            },
        ]],
        onClickCell: _relation.onClickCell
    });

    _relation.createAdd();
};
//添加
_relation.add = function () {
    var $dg = $(_relation._id);
    _relation.endEditingNoValid();
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

    _relation.onClickCell.call($dg, rowIndex, 'DL_Relation');
}
//删除
_relation.remove = function (e, target) {
    var $dg = $(_relation._id);
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
_relation.get = function () {
    _relation.endEditingNoValid();

    var $dg = $(_relation._id);
    var rows = $dg.datagrid('getRows');
    var deletedRows = $dg.datagrid('getChanges', 'deleted');
    var changes = [];
    $.each(rows, function (i, o) {
        if (o.EX_User && o.AU_Code >= 0) {
            changes.push(o);
        }
    });
    return { changes: changes, deletes: deletedRows };
};
//绑定数据
_relation.bindData = function (relation) {
    $(_relation._id).datagrid('loadData', relation ? relation : []);
};
//应用
_relation.accept = function () {
    if (!_relation.validate()) return;

    var $dg = $("#dg_relation");
    var data = _relation.getForm();

    var newData = {};
    $.each(data, function (n, d) {
        n = n.replace(/^Relation_/g, "");
        newData[n] = d;
    });

    var rowData = $dg.datagrid('getSelected');
    var rowIndex = $dg.datagrid('getRowIndex', rowData);
    $.extend(rowData, newData);

    rowData.EX_User = data;

    $dg.datagrid('updateRow', {
        index: rowIndex,
        row: rowData
    });

    _relation.close();
};
//关闭
_relation.close = function () {
    $("#w_relation").window('close');
};
//验证
_relation.validate = function () {
    return $.form.validate({
        textbox: ['Relation_AU_Name']
        //, combobox: ['']
    });
};
//取消验证
_relation.disableValidation = function () {
    $.form.disableValidation({
        textbox: ['Relation_AU_Name']
        //,combobox: ['']
    });
};
//获取表单
_relation.getForm = function () {
    var data = $.form.getData("#frm_relation");
    if ($.trim(data.Relation_AU_Gender) != '') {
        data.Relation_AU_Gender = (data.Relation_AU_Gender == 1);
    }
    return data;
};
//设置
_relation.set = function (data) {
    $.form.setData("#frm_relation", data);
};
//重置 
_relation.reset = function () {
    _relation.disableValidation();
    $.form.reset("#frm_relation");
    $("#Relation_AU_Code").val(0);
};


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
                associates: '<%=Resources.CRMTREEResource.cm_tabs_Assc%>',
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
                industry: '<%=Resources.CRMTREEResource.cm_personal_industry%>',
                type: '<%=Resources.CRMTREEResource.cm_personal_type%>',
                relation: '<%=Resources.CRMTREEResource.cm_personal_relation%>'
            },
            cars: {
                buttonos: {
                    accept: '<%=Resources.CRMTREEResource.cm_cars_buttonos_accept%>',
                    ignore: '<%=Resources.CRMTREEResource.cm_cars_buttonos_ignore%>'
                },
            }

        };
        if (!_isEn) {
            $.each(language.personal, function (n, v) {
                language.personal[n] = v + '：';
            });
            //$.each(language.cars.form, function (n, v) {
            //    language.cars.form[n] = v + '：';
            //});
        } else {
            $.each(language.personal, function (n, v) {
                language.personal[n] = v + ':';
            });
            //$.each(language.cars.form, function (n, v) {
            //    language.cars.form[n] = v + ':';
            //});
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
+ '                                <td class="text" style="width:100px;"><span>*</span>' + language.personal.name + '</td>'
+ '                                <td colspan="3">'
+ '                                    <input id="AU_Code" type="hidden" value="0" />'
+ '                                    <input id="AU_Name" class="easyui-textbox fluid" data-options="required:true,novalidate:true,validType: \'maxLength[50]\'"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.type + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Type" class="easyui-combobox"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
+ '                                <td class="text">' + language.personal.gender + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Gender" class="easyui-combobox" />'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.birthday + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_B_date" class="easyui-datebox" />'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
+ '                                <td class="text">' + language.personal.idType + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_ID_Type" class="easyui-combobox" />'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.idNo + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_ID_No" class="easyui-textbox" data-options="novalidate:true,validType: \'maxLength[18]\'"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
+ '                                <td class="text">' + language.personal.driverLicense + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Dr_Lic" class="easyui-textbox" data-options="novalidate:true,validType: \'maxLength[20]\'"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
+ '                                <td class="text">' + language.personal.education + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Education" class="easyui-combobox" />'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr class="tr_item">'
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

+ '            <a id="btnSave" class="easyui-linkbutton" data-options="iconCls:\'icon-save\'" style="width:80px;margin-left:20px;">' + language.buttons.save + '</a>'
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
        //_cars.create();
        //buttons
        InitBtns_Save();
        $.form.fluid('#frm_personal');
        //$.form.setEmptyText('#frm_car');


        InitTabs();
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
                if (index === 4) {  //Communications
                    LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=24&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                    return;
                }
                if (index === 6) { //Cars
                    LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=123&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                    return;
                }
                if (index === 7) { //Visits
                    LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=23&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                    return;
                }
                if (index === 8) { //Appointments
                    LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=40&AU_Name=' + _params.AU_Name + '&AU_Code=' + _params.AU_Code);
                    return;
                }
                //if (index === 5) { //Favorites
                //    LoadTabIframe(index, '/templete/report/DataGrid.html?MF_FL_FB_Code=24&AU_Code=' + _params.AU_Code);
                //    return;
                //}
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
            wordIds: [1, 9, 31, 23, 15, 18, 2036]
            , selects: [
                { selectId: 'AU_Gender', wordId: 9 }
                , { selectId: 'AU_ID_Type', wordId: 31 }
                , { selectId: 'AU_Education', wordId: 23 }
                , { selectId: 'AU_Occupation', wordId: 15 }
                , { selectId: 'AU_Industry', wordId: 18 }
                , { selectId: 'AU_Type', wordId: 1 }

                //, { selectId: 'Relation_AU_Gender', wordId: 9 }
                //, { selectId: 'Relation_AU_ID_Type', wordId: 31 }
                //, { selectId: 'Relation_DL_Access', wordId: 3036 }
            ]
            , onLoad: function (data) {
                BindContactsSelects();

                //if (data._2036) {
                //    _relation.data = data._2036;
                //    _relation.create();
                //}
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
                });
            });

            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Messaging_Carriers' }) }, function (data) {
                _contacts.info._CT_Messaging_Carriers = data ? data : [];
                BindCustomerInfo();
                //BindCarSelects();
            }, "json");
        });
    }
    //汽车
    function BindCarSelects() {
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

            _contacts.GetWords = res.relation.GetWords;
            $('#dg_contacts').datagrid('loadData', res.relation.con);
            // $('#dg_cars').datagrid('loadData', res.cars);
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
        //params.cars = _cars.get();

        $.mask.show();
        $("#btnSave").linkbutton('disable');
        var s_params = JSON.stringify(params);
        $.post(_s_url, { o: s_params }, function (res) {
            $.mask.hide();
            $("#btnSave").linkbutton('enable');
            if ($.checkResponse(res)) {
                closeWindow(true);
            }
        }, "json");
    }

    //关闭窗体
    function closeWindow(bSave) {
        if (window._closeOwnerWindow) {
            window._closeOwnerWindow();
            if (bSave) {
                top[_params._winID]._datagrid.action.refresh();
            }
        }
        else {
            window.close();
        }
    }

    $("#btnCancel").click(function () {
        closeWindow();
    });

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
    $("#btnCalling").click(function () {
        var params = getICParams();
        $.windowOpen("/manage/customer/IncomingCallsManager.aspx?OT=7&CD=2&AU=" + _params.AU_Code + params, 550, 300, "InCalls");
    });
});
</script>
