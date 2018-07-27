<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarInfoManager.aspx.cs" Inherits="templete_usercontrol_CarInfoManager" %>

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
            overflow: hidden;
        }

        .tt-inner {
            display: inline-block;
            line-height: 12px;
            padding-top: 5px; 
            cursor: pointer;
        }

            .tt-inner img {
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

        .picItem {
            margin: 0;
            padding: 0;
            float: left;
            position: relative;
            width: 100px;
            height: 80px;
            margin-right: 5px;
            margin-bottom: 5px;
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
    var _s_url = '/handler/Reports/CarInfoManage.aspx';
    var _s_url_appointment = '/handler/Reports/AppointmentManager.aspx';
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
                    _cars._edit(CI_Code, AU_Code);
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
        if (_cars.AD_MK_Code > 0) {
            $("#MK_Code").combobox('select', _cars.AD_MK_Code);
        }
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

            $("#MK_Code").combobox('setValue', res.carInfo[0].MK_Code);

            $("#CM_Code").initSelect({ data: res.CT_Car_Model, onSelect: _cars.onSelect.CM_Code })
                .combobox('setValue', res.carInfo[0].CM_Code);

            var data_year = $("#CI_YR_Code").combobox('getData');
            $("#CI_YR_Code").initSelect({ editable: true, data: data_year, onSelect: _cars.onSelect.CI_YR_Code })
            .combobox('setValue', carInfo.CI_YR_Code);

            $("#CI_CS_Code").initSelect({ data: res.CT_Car_Style }).combobox('setValue', res.carInfo[0].CS_Code);
        }
    };

    //内部编辑
    _cars._edit = function (CI_Code, AU_Code) {
        _cars.picture.removed = [];
        if (CI_Code > 0) {
            $.mask.show();
            var params = {
                action: 'Get_Car_InventoryAndLendon',
                id: CI_Code
            };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (res) {
                    var carInfo = $.extend({}, res.CT_Car_Inventory, res.CT_Auto_Insurance);
                    _cars._lendon(carInfo, res);
                    _cars.set(carInfo);
                    _cars.picture.set(res.CT_Car_Inventory.CP_Picture_FN, false);
                    $('#win_cars').window('open').window('center');
                } else {

                }
                $.mask.hide();
            }, 'json'); 
        } else {
            if (AU_Code > 0) {
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
    //保存
    _cars.save = function () {
        if (!_cars.validate()) return;
        var AU_Code = _params.AU;
        if (!(AU_Code > 0)) { AU_Code = _params.AU_Code; }

        var $dg = $("#dg_cars");
        var row = _cars.getForm();
        //      row = _cars.get();
        row.CP_Picture_FN = _cars.picture.get();
        row._img_srcs = _cars.picture.getSrcs();
        row.Cars = _cars.format(row);
        if (_cars.picture.removed.length > 0) {
            if (!row.Picture_Removed) row.Picture_Removed = '';
            row.Picture_Removed += (row.Picture_Removed ? ',' : '') + _cars.picture.removed.join(',');
        }

            $.mask.show();
           // $("#btnSave").linkbutton('disable');
         var s_params = JSON.stringify({ action: 'Save_Car', au_code: AU_Code, o: row });
         $.post(_s_url, { o: s_params }, function (res) {
                $.mask.hide();
                $("#btnSave").linkbutton('enable');
                if ($.checkResponse(res)) {
                    $.msgTips.save(true);
                if (_params.AC === 'AP') {//Appointment Add      
                     window.top[_params._winID]._appointment.car.bindData(AU_Code, res.CI_Code);
                } else if (_params.AC === 'A' || _params.AC === 'V') {// Add or Edit
                     top[_params._winID]._datagrid.action.refresh();
                }
                    _cars.close();
                } else {
                    $.msgTips.save(false);
                }
             
            }, "json");



    };
    //关闭
    _cars.close = function () {
        if (window._closeOwnerWindow) { window._closeOwnerWindow(); }
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
        $.form.setData("#frm_car", data, "#MK_Code,#CM_Code,#CI_CS_Code,#CI_YR_Code");

    };
    //联动事件
    _cars.onSelect = {};
    //生产商联动
    _cars.onSelect.MK_Code = function (record) {
        $("#CM_Code,#CI_CS_Code").combobox('clear').initSelect();
        if (record && record.value > 0) {
            var yr_code = $("#CI_YR_Code").combobox('getValue');
            if (yr_code > 0) {
                $.post(_s_url, { o: JSON.stringify({ action: 'Get_Car_Model', id: record.value, id_year: yr_code }) }, function (res) {
                    $("#CM_Code").initSelect({ data: res, onSelect: _cars.onSelect.CM_Code });
                }, "json");
            }
        }
    };
    //车型联动
    _cars.onSelect.CM_Code = function (record) {
        $("#CI_CS_Code").combobox('clear').initSelect();
        if (record && record.value > 0) {
            var yr_code = $("#CI_YR_Code").combobox('getValue');
            if (yr_code > 0) {
                $.post(_s_url, { o: JSON.stringify({ action: 'Get_Car_Style', id: record.value, id_year: yr_code }) }, function (res) {
                    $("#CI_CS_Code").initSelect({ data: res });
                }, "json");
            }
        }
    };
    _cars.onSelect.CI_YR_Code = function (record) {
        $("#CI_CS_Code").combobox('clear').initSelect();
        $("#CM_Code").combobox('clear').initSelect();
        if (record && record.value > 0) {
            var mk_code = $("#MK_Code").combobox('getValue');
            if (mk_code > 0) {
                $.post(_s_url, { o: JSON.stringify({ action: 'Get_Car_Model', id: mk_code, id_year: record.value }) }, function (res) {
                    $("#CM_Code").initSelect({ data: res, onSelect: _cars.onSelect.CM_Code });
                }, "json");
            }
            //$.post(_s_url, { o: JSON.stringify({ action: 'Get_Car_Style', id: cm_code, id_year: record.value }) }, function (res) {
            //    $("#CI_CS_Code").initSelect({ data: res });
            //}, "json");
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

    //设置打开窗体
    _setWindow = function (win) {
        if (win) {
            var opts = win.window('options');
            opts.title = '<%=Resources.CRMTREEResource.ap_car_window_title%>';
            win.window(opts);
        }
    }

    $(function () {
        //获得界面HTML
        function getHTML() {
            var language = {
                buttons: {
                    save: '<%=Resources.CRMTREEResource.cm_buttons_save%>',
                    close: '<%=Resources.CRMTREEResource.cm_buttons_close%>'
                },
                cars: {
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
                $.each(language.cars.form, function (n, v) {
                    language.cars.form[n] = v + '：';
                });
            } else {
                $.each(language.cars.form, function (n, v) {
                    language.cars.form[n] = v + ':';
                });
            }

            var html = '<div class="easyui-layout" data-options="fit:true">'
+ '    <div data-options="region:\'north\',border:false,height:180" style="padding: 0px; padding-top: 5px; border-bottom: 1px solid #DEDEDE;position:relative;">'
+ '     <table id="frm_car" class="form" border="0" cellpadding="3" cellspacing="2">'
+ '      <tr>'
+ '          <td class="text">' + language.cars.form.Years + '</td>'
+ '          <td>'
+ '              <input id="CI_YR_Code" class="easyui-combobox showEmptyText" data-options="editable:true,required:true,novalidate:true" />'
+ '          </td>'
+ '         <td></td><td></td>'
+ '         <td rowspan="5" style="padding:0px;vertical-align:bottom"><fieldset class="hideFieldset" style="width:80%;"><legend><label><%=Resources.CRMTREEResource.Insurance%></label></legend>'
+ '     <table id="frm_car" class="form" border="0" cellpadding="3" cellspacing="2">'
+ '      <tr>'
+ '          <td class="text"><%=Resources.CRMTREEResource.Company%></td>'
+ '          <td>'
+ '              <input id="AI_IC_Code" class="easyui-combobox" />'
+ '          </td>'
+ '      </tr>'
+ '      <tr>'
+ '          <td class="text"><%=Resources.CRMTREEResource.ExpDate%></td>'
+ '          <td>'
+ '              <input id="AI_End_dt" class="easyui-datebox" />'
+ '          </td>'
+ '      </tr>'
+ '      <tr>'
+ '          <td class="text"><%=Resources.CRMTREEResource.Policy%></td>'
+ '          <td>'
+ '              <input id="AI_Policy" class="easyui-textbox" />'
+ '          </td>'
+ '      </tr>'
+ '     </table>'
+'</fieldset></td>'
+ '      </tr>'
+ '      <tr>'
+ '          <td class="text">' + language.cars.form.Make + '</td>'
+ '          <td>'
+ '              <input id="CI_Code" type="hidden" value="0" />'
+ '              <input id="MK_Code" class="easyui-combobox showEmptyText" />'
+ '          </td>'

+ '          <td class="text">' + language.cars.form.VIN + '</td>'
+ '          <td>'
+ '              <input id="CI_VIN" class="easyui-textbox" data-options="novalidate:true,validType: \'maxLength[20]\'"/>'
+ '          </td>'
+ '      </tr>'
+ '      <tr>'
+ '          <td class="text">' + language.cars.form.Model + '</td>'
+ '          <td>'
+ '              <input id="CM_Code" class="easyui-combobox showEmptyText" />'
+ '          </td>'

+ '          <td class="text">' + language.cars.form.Mileage + '</td>'
+ '          <td>'
+ '              <input id="CI_Mileage" class="easyui-numberbox" />'
+ '          </td>'
+ '      </tr>'
+ '      <tr>'
+ '          <td class="text">' + language.cars.form.Style + '</td>'
+ '          <td>'
+ '              <input id="CI_CS_Code" class="easyui-combobox showEmptyText" data-options="required:true,novalidate:true" />'
+ '          </td>'
+ '          <td class="text">' + language.cars.form.Licence + '</td>'
+ '          <td>'
+ '              <input id="CI_Licence" class="easyui-textbox" data-options="novalidate:true,validType: \'maxLength[15]\'"/>'
+ '          </td>'
+ '      </tr>'
+ '      <tr>'
+ '          <td class="text">' + language.cars.form.ColorExternal + '</td>'
+ '          <td>'
+ '              <input id="CI_Color_E" class="easyui-combobox" />'
+ '          </td>'
+ '          <td class="text">' + language.cars.form.ColorInternal + '</td>'
+ '          <td>'
+ '              <input id="CI_Color_I" class="easyui-combobox" />'
+ '          </td>'
+ '      </tr>'
+ '     </table>'
+ '    </div>'
+ '    <div data-options="region:\'center\',border:false" style="position: relative; border-bottom: 1px solid #B1C242; overflow-x: hidden;">'
+ '        <div id="ci_container_car" style="position:relative;margin:5px;">'
+ '            <div id="ci_upload_car" class="picItem" style="position: relative; width: 100px;height: 80px;overflow:hidden;background-color: transparent;">'
+ '                <img src="/scripts/jquery-easyui/themes/icons/add_car.png" style="position:absolute;" />'
+ '            </div>'
+ '        </div>'
+ '    </div>'
+ '    <div data-options="region:\'south\',border:false" style="height:auto;text-align:right;padding:10px;">'
+ '        <a class="easyui-linkbutton" id="btnSave" data-options="iconCls:\'icon-save\',onClick:_cars.save" style="width:80px;">' + language.buttons.save + '</a>'
+ '        <a class="easyui-linkbutton" data-options="iconCls:\'icon-cancel\',onClick:_cars.close" style="width:80px;margin-left:10px;">' + language.buttons.close + '</a>'
+ '    </div>'
+ '</div>';;
            return html;
        }

        //初始化
        (function Init() {
            var AU_Code = _params.AU;
            if (!(AU_Code > 0)) { AU_Code = _params.AU_Code; }
            if (!(AU_Code > 0)) {
                $.msgTips.info('<%=Resources.CRMTREEResource.cm_msg001%>');
                return;
            }

            var html = getHTML();
            $(window.document.body).append(html);
            $.parser.parse();
            BindCarSelects();
            if (_params.AC === 'A' || _params.AC === 'AP') {//Add
                _cars.createAdd();

            } else if (_params.AC === 'E' || _params.AC === 'V') {//Edit
            if (_params.CI > 0) {
                    _cars._edit(_params.CI, AU_Code);
            } else {
                _cars.createAdd();
            }
            }
            $("#MK_Code").combobox('textbox').focus();


            $.form.setEmptyText('#frm_car');

            _cars.picture.plupload();

            $(window).unload(function () {
                try {
                    $("#ci_upload_car").empty();
                } catch (e) {

                }
            });
        })();


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
 //                   $("#CM_Code").initSelect({ onSelect: _cars.onSelect.CM_Code });
                    if (res.CT_Years && res.CT_Years.length > 0) {
                        _cars._year = res.CT_Years[0].value;
                    }
                    $("#CI_YR_Code").initSelect({ editable: true, data: res.CT_Years, onSelect: _cars.onSelect.CI_YR_Code });
                    if (_cars._year > 0) {
                        $("#CI_YR_Code").combobox('setValue', _cars._year);
                    }
                    $("#CI_Color_E,#CI_Color_I").initSelect({ editable: true, data: res.CT_Color_List });

                    _cars.AD_MK_Code = res.AD_MK_Code;

                    //if (_cars.AD_MK_Code > 0) {
                    //    $("#MK_Code").combobox('select', _cars.AD_MK_Code);
                    //}
                }

            }, "json");
        }
    });
</script>