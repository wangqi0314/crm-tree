<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeDetails.aspx.cs" Inherits="templete_report_EmployeeDetails" %>

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
           padding-bottom:6px;
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
        }
    };
    //创建
    _contacts.create = function () {
        var $dg = $('#dg_contacts');
        $dg.datagrid({
            url: null,
            fit: true,
            rownumbers: true,
            singleSelect: true,
            showHeader: false,
            border: false,
            loadMsg: '',
            columns: [[
                {
                    field: 'type', title: 'type', width: "10%",
                    formatter: function (value, row) {
                        return row.type_Text;
                    }
                },
                {
                    field: 'info', title: 'info', width: "90%",
                    formatter: function (value, rowData) {
                        return _contacts.info.format(rowData.type, rowData.o);
                    }
                }
            ]],
            onClickCell: _contacts.onClickCell
        });
    };
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
            data: [data],
            loadMsg: '',
            columns: [opts.columns],
            onBeginEdit: opts.onBeginEdit
        });
        $c.datagrid('beginEdit', 0);
        _contacts.info.$dg = $c;
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
            remove: '<%=Resources.CRMTREEResource.ed_personal_buttons_remove%>'
        }
    };
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
    };
    //获取
    _personal.get = function () {
        var data = $.form.getData("#frm_personal");
        if ($.trim(data.AU_Gender) != '') {
            data.AU_Gender = (data.AU_Gender == 1);
        }
        return data;
    };
    //图片
    _personal.picture = {};
    //创建图片
    _personal.picture.create = function (fileName) {
        var $img = $('<div class="picItem car_img"><img src="/images/Adviser/' + fileName + '"/></div>');
        $("#plupload_container").empty().append($img);

        $('img', $img).bind({
            mouseover: function () {
                $(this).fadeTo('fast', 0.6);
            },
            mouseleave: function () {
                $(this).fadeTo('fast', 1);
            },
            //预览图片
            click: function () {
                var src = $(this).attr('src');
                $.topOpen({
                    title: $("#AU_Name").val() + ' - '+'<%=Resources.CRMTREEResource.ed_personal_picture_title%>',
                    showMask: false,
                    url: "/templete/report/PhotoViewer.html?src=" + src, //rd=" + Math.random() + "&
                    width: 500,
                    height: 350
                });
            }
        });
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
            ]]
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
            return;
        }
        if (!(_params.Empl_Code > 0)) {
            $.msgTips.info(_schedule.language.msg001);
            return;
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

    //设置窗体(标题)
    _setWindow = function (win) {
        if (win) {
            var opts = win.window('options');
            opts.title = '<%=Resources.CRMTREEResource.ed_window_title%>';
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
                    schedule: '<%=Resources.CRMTREEResource.ed_tabs_schedule %>'
                },
                personal: {
                    id: '<%=Resources.CRMTREEResource.ed_personal_id %>',
                    active: '<%=Resources.CRMTREEResource.ed_personal_active %>',
                    ignore: '<%=Resources.CRMTREEResource.ed_personal_ignore %>',
                    name: '<%=Resources.CRMTREEResource.ed_personal_name %>',
                    gender: '<%=Resources.CRMTREEResource.ed_personal_gender %>',
                    birthday: '<%=Resources.CRMTREEResource.ed_personal_birthday %>',
                    idType: '<%=Resources.CRMTREEResource.ed_personal_idType %>',
                    idNo: '<%=Resources.CRMTREEResource.ed_personal_idNo %>',
                    driverLicense: '<%=Resources.CRMTREEResource.ed_personal_driverLicense %>',
                    education: '<%=Resources.CRMTREEResource.ed_personal_education %>',
                    occupation: '<%=Resources.CRMTREEResource.ed_personal_occupation %>',
                    industry: '<%=Resources.CRMTREEResource.ed_personal_industry %>',
                    picture: '<%=Resources.CRMTREEResource.ed_personal_picture %>',
                    fun: '<%=Resources.CRMTREEResource.em_personal_function %>',
                    marital: '<%=Resources.CRMTREEResource.em_personal_marital %>',
                    vacation_date: '<%=Resources.CRMTREEResource.vacation_date %>'

                },
                buttons: {
                    close: '<%=Resources.CRMTREEResource.ed_buttons_close %>'
                }
            };
            if (!_isEn) {
                $.each(language.personal, function (n, v) {
                    language.personal[n] = v + '：';
                });
            } else {
                $.each(language.personal, function (n, v) {
                    language.personal[n] = v + ':';
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
+ '                    <div class="easyui-panel" data-options="fit:true,border:false">'
+ '                        <table id="frm_personal" class="form' + (_isEn ? '' : ' form_cn') + '" border="0" cellpadding="3" cellspacing="2">'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.id + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Code" type="hidden" value="0" />'
+ '                                    <input id="DE_Code" type="hidden" value="0" />'
+ '                                    <input id="DE_Picture_FN_Temp" type="hidden" value="" />'
+ '                                    <input id="DE_ID" class="easyui-textbox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.active + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Active_tag" type="checkbox" disabled="disabled"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.ignore + '</td>'
+ '                                <td>'
+ '                                    <input id="DE_Ignore" type="checkbox" disabled="disabled"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.name + '</td>'
+ '                                <td colspan="3" class="fluid">'
+ '                                    <input id="AU_Name" class="easyui-textbox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.gender + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Gender" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.birthday + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_B_date" class="easyui-datebox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.idType + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_ID_Type" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.idNo + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_ID_No" class="easyui-textbox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.driverLicense + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Dr_Lic" class="easyui-textbox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.education + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Education" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.occupation + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Occupation" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                                <td class="text">' + language.personal.industry + '</td>'
+ '                                <td>'
+ '                                    <input id="AU_Industry" class="easyui-combobox" data-options="readonly:true"/>'
+ '                                </td>'
+ '                            </tr>'
+ '                            <tr>'
+ '                                <td class="text">' + language.personal.picture + '</td>'
+ '                                <td>'
+ '                                    <input id="DE_Picture_FN" type="hidden" value=""/>'
+ '                                    <div id="plupload_container" style="position:relative;width:100%;height:64px;"></div>'
+ '                                </td>'
+ '                            </tr>'
+ '                        </table>'
+ '                    </div>'
+ '                </div>'
+ '                <div title="' + language.tabs.schedule + '" data-options="id:\'sch\',iconCls:\'icon-customer-visits\'">'
+ '                    <div class="easyui-layout" data-options="fit:true">'
+ '                        <div data-options="region:\'north\',height:\'40%\',border:false" style="padding: 0px 0px 0px 0px;overflow:hidden;">'
+ '                            <table id="dg_schedule"></table>'
+ '                            <div style="width:100%;border-bottom: 1px solid #B1C242;position:absolute;bottom:0;"></div>'
+ '                        </div>'
+ '                        <div data-options="region:\'center\',border:false" style="padding: 10px 10px 10px 10px; overflow: hidden;">'
+ '                            <div class="easyui-panel" data-options="fit:true,border:false">'
+ '                                <div id="vacation_date" class="crm-table-flow">'
+ '                                     ' + language.personal.vacation_date + ''
+ '                                     <input id="DE_Vacation_St" class="easyui-datebox" data-options="required:true,novalidate:true" style="width: 100px;" />'
+ '                                       -- '
+ '                                     <input id="DE_Vacation_En" class="easyui-datebox" data-options="required:true,novalidate:true" style="width: 100px;" />'
+ '                                </div>'
+ '                            </div>'
+ '                        </div>'
+ '                    </div>'
+ '                </div>'
+ '            </div>'
+ '        </div>'
+ '        <div data-options="region:\'south\',border:false" style="height:auto;text-align:right;padding:10px;border-top: 1px solid #B1C242;position:relative;overflow:hidden;">'
+ '            <div style="float:right;">'
+ '             <a id="btnClose" class="easyui-linkbutton" data-options="iconCls:\'icon-cancel\',onClick:_employeeDetails.close" style="width: 80px; margin-left: 10px;">' + language.buttons.close + '</a>'
+ '            </div>'
+ '            <div style="clear:both;height:0;"></div>'
+ '        </div>'
+ '    </div>';
            return html;
        }

        //初始化
        (function Init() {
            var html = getHTML();
            $(window.document.body).append(html);
            $.parser.parse();

            $($("#DE_ID").textbox('textbox')).css({ color: "black" });

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

        //关闭窗体
        function closeWindow() {
            if (window._closeOwnerWindow) { window._closeOwnerWindow(); }
        }

        //绑定个人下拉列表
        function BindPersonalSelects() {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Messaging_Carriers' }) }, function (data) {
                _contacts.info._CT_Messaging_Carriers = data ? data : [];
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
                        BindCustomerInfo();
                    }
                });

            }, "json");
        }

        //绑定顾客信息
        function BindCustomerInfo() {
            if (!(_params.Empl_Code > 0)) {
                $.msgTips.info('<%=Resources.CRMTREEResource.em_msg001%>');
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
                BindVacationDate(res.dealerEmpl.EX_DE_Vacation_St, res.dealerEmpl.EX_DE_Vacation_En);
            }, "json");
        }
        function BindVacationDate(sd, ed) {
            if (!sd || !ed) { return; }
            $("#DE_Vacation_St").datebox('setValue', sd);
            $("#DE_Vacation_En").datebox('setValue', ed);
        }
    });
</script>