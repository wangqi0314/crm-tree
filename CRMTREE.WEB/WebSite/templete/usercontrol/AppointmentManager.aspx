<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppointmentManager.aspx.cs" Inherits="templete_usercontrol_AppointmentManager" %>

<!DOCTYPE html>
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
        var _winID = '<%=Guid.NewGuid()%>';
        top[_winID] = window.self;
        var _isEn = $.isEn();
        var _weekDays = [[['Sunday'], ['Monday'], ['Tuesday'], ['Wednesday'], ['Thursday'], ['Friday'], ['Saturday']],
                         [['星期日'],['星期一'], ['星期二'], ['星期三'], ['星期四'], ['星期五'], ['星期六']]];

        if (!_isEn) {
            document.write('<script src="/scripts/jquery-easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></sc' + 'ript>');
        }
    </script>
    <style type="text/css">
        html, body {
            overflow:hidden;
            font-size:12px;
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


        .textbox .l-btn {
            -moz-border-radius: 0px;
            -webkit-border-radius: 0px;
            border-radius: 0px;
        }

        .form tr td {
            padding-bottom:5px;
        }
        .advisor {
        }
        .advisor .combobox-item {
            width: 136px;
            height: 102px;
            float: left;
        }
    </style>
</head>
<body>
<table id="frm_appointment" class="form" border="0" cellpadding="3" cellspacing="2" style="margin-top:30px;margin-left:20px;">
    <tr>
        <td class="text"><%=Resources.CRMTREEResource.ap_form_customerName %></td>
        <td colspan="3">
            <input id="AP_Code" type="hidden" value="0" />
            <input id="AP_AU_Code" class="easyui-combogrid fluid" data-options="
                panelWidth:500,
                <%--panelHeight:null,
                panelMaxHeight:200,--%>
                url: '',
                hasDownArrow:false,
                idField:'AU_Code',
                textField:'AU_Name',
                <%--mode:'local',--%>
                fitColumns:true,
                required:true,
                <%--keyHandler: {
	                query: _appointment.customer.query
                },--%>
                columns:[[
                {field:'AU_Name',title:'<%=Resources.CRMTREEResource.am_customer_AU_Name %>',width:200},
                {field:'Phone',title:'<%=Resources.CRMTREEResource.am_customer_Phone %>',width:100},
                {field:'Lic',title:'<%=Resources.CRMTREEResource.main_customer_Car_Lic %>',width:100}
<%--                {field:'Car_Model',title:'<%=Resources.CRMTREEResource.am_customer_Car_Model %>',width:200}--%>
                ]]
                "/>
        </td>
        <td>
            <a class="easyui-linkbutton" id="btnAddCustomer" title="Add Customer" data-options="iconCls:'icon-add',disabled:true,plain:true,onClick:_appointment.customer.add"></a>
            <span id="Edit_Btn_1"><a class="easyui-linkbutton" id="btnEditCustomer" title="Edit Customer" data-options="iconCls:'icon-edit',disabled:true,plain:true,onClick:_appointment.customer.edit"></a></span>
        </td>
    </tr>
    <tr>
        <td class="text"><%=Resources.CRMTREEResource.ap_form_contact %></td>
        <td colspan="3">
            <input id="AP_PL_ML_Code" type="hidden" value="" />
            <input id="AP_Cont_Type" type="hidden" value="" />
            <input id="AP_PL_ML_Code_Text" class="easyui-textbox fluid" data-options="required:true,readonly:true"/>
        </td>
        <td>
            <a class="easyui-linkbutton" id="btnContactInfo" title="Select Contact" data-options="iconCls:'icon-select',disabled:true,plain:true,onClick:_appointment.contact.select"></a>
            <span id="Edit_Btn_2"><a class="easyui-linkbutton" id="btnEditContact" title="Edit Customer" data-options="iconCls:'icon-edit',disabled:true,plain:true,onClick:_appointment.contact.edit"></a></span>
        </td>
    </tr>
    <tr>
        <td class="text"><%=Resources.CRMTREEResource.ap_form_car %></td>
        <td colspan="3">
            <input id="AP_CI_Code" class="easyui-combobox fluid" data-options="required:true,
                valueField:'CI_Code',
                textField:'Cars',
                formatter: _appointment.format.car,
                onSelect:_appointment.onSelect.AP_CI_Code"/>
        </td>
        <td>
            <a class="easyui-linkbutton" id="btnCarInfo" title="Add Car" data-options="iconCls:'icon-add',disabled:true,plain:true,onClick:_appointment.car.add"></a>
            <span id="Edit_Btn_3"><a class="easyui-linkbutton" id="btnEditCar" title="Edit Customer" data-options="iconCls:'icon-edit',disabled:true,plain:true,onClick:_appointment.car.edit"></a></span>
        </td>
    </tr>
    
    <tr id="tr_ap_ad_code">
        <td class="text"><%=Resources.CRMTREEResource.AvailableDealers %></td>
        <td colspan="3">
            <input id="AP_AD_Code" class="easyui-combobox fluid" data-options="required:true,
                valueField:'AD_Code',
                textField:'AD_Name'"/>
        </td>
    </tr>
    <tr>
        <td class="text"><%=Resources.CRMTREEResource.ap_form_date %></td>
        <td colspan="3">
            <div style="float:left;"><input id="EX_AP_Date" class="easyui-datebox" data-options="required:true,onSelect:_appointment.onSelectDate,validType:'gtOrEqualDate[\'<%=DateTime.Now.ToString(Interna ? "MM/dd/yyyy":"yyyy-MM-dd") %>\']'"/></div>
            <div style="float:left;margin:0px 10px; width:100px;vertical-align:central;"><span id="AP_Weekday" style="font-weight:bold;"></span></div>
            <div style="float:left;margin-left:20px;"><%=Resources.CRMTREEResource.ap_form_time %>&nbsp;&nbsp;
                <input id="EX_AP_Time" class="easyui-timespinner" data-options="increment:10, required:true"/>
            </div>
        </td>
    </tr>
    <tr id="c_tr_advisor">
        <td class="text"><%=Resources.CRMTREEResource.ap_form_advisor %></td>
        <td colspan="3">
            <input id="AP_SA_Selected" class="easyui-combobox fluid" data-options="required:true,panelWidth:720,panelheight:220"/>
        </td>
    </tr>
    <tr>
        <td class="text"><%=Resources.CRMTREEResource.EstimatedMileage %></td>
        <td colspan="3">  
            <div style="float:left;"><input id="AP_Mileage" class="easyui-numberspinner" data-options="width:120,onChange:_appointment.onChange.AP_Mileage"/></div>
              <div style="float:left;margin-left:10px;width:200px;vertical-align:central;">
              <span id="AP_Mile_C" class="easyui-radiolist" data-options="data:[
                    { value: '1', text: '<%=Resources.CRMTREEResource.Custom %>' }, 
                    { value: '0', text: '<%=Resources.CRMTREEResource.Estimated %>',selected:true }
                ]"></span>
            </div>
            <div style="float:left;"><%=Resources.CRMTREEResource.LastMileage %><input id="EX_LastMileage" class="easyui-textbox" data-options="width:120,disabled:true"/></div>
        </td>
    </tr>
    <tr>
        <td class="text"><%=Resources.CRMTREEResource.ap_form_reason %></td>
        <td colspan="3">
            <input id="AP_SC_Code" class="easyui-combobox" data-options="required:true"/>
            <span id="spn_EX_Mileage"><input id="EX_Mileage" class="easyui-combobox" data-options="required:true"/></span>
            <span id="spn_AP_ST_Code"><input id="AP_ST_Code" class="easyui-combobox" data-options="required:false,onSelect:_appointment.onSelect.AP_ST_Code"/></span>            
        </td>
    </tr>
    <tr id="tr_SN_Note">
        <td class="text"></td>
        <td colspan="3">
            <input id="SN_Note" class="easyui-textbox fluid" data-options="multiline:true,required:true,disabled:true" style="height:40px;"/>
        </td>
    </tr>
    <tr>
        <td class="text"><%=Resources.CRMTREEResource.ap_form_transportation %></td>
        <td>
            <input id="AP_Transportation" class="easyui-combobox" data-options="required:true"/>
        </td>
         <td class="text"><%=Resources.CRMTREEResource.ap_form_notification %></td>
        <td>
            <input id="AP_Notification" type="checkbox"/>
        </td>
    </tr>
    
    <tr>
        <td class="text"><%=Resources.CRMTREEResource.GeneralNotes %></td>
        <td colspan="3">
            <input id="EX_GeneralNote" class="easyui-textbox fluid" data-options="multiline:true" style="height:40px;"/>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align:center;padding:20px;">
            <a class="easyui-linkbutton" id="btnSave" data-options="iconCls:'icon-save',disabled:true,onClick:_appointment.save" style="width:80px;">
                <%=Resources.CRMTREEResource.ap_buttons_save %>
            </a>
            <a class="easyui-linkbutton" id="btnCancel" data-options="iconCls:'icon-cancel',onClick:_appointment.cancel" style="width:80px;margin-left:20px;">
                <%=Resources.CRMTREEResource.CancelBtn %>
            </a>
        </td>
    </tr>
</table>


<div id="w_schedule" class="easyui-window" data-options="minimizable:false,closed:true,title:'  ',modal:true"
    style="width: 450px; height: 200px; padding:15px;">
    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'center',border:false" style="padding:7px;font-size:14px;">
            <span id="YesChng0"><%=Resources.CRMTREEResource.schedule_tip_top %> <br/></span><br/>
            <span id="advisor_time" style="margin-left:50px;"></span> <br/><span id="advisor_rem" style="color:red;margin-left:50px;"></span><br/>
            <span id="YesChng1"><%=Resources.CRMTREEResource.schedule_tip_bottom %></span>
        </div>
        <div id="YesChange_Time" data-options="region:'south',border:false" style="height: 38px; text-align: right; overflow: hidden; border-top: 1px solid #B1C242; padding: 10px;">
            <span id="YesChng2"><a class="easyui-linkbutton" data-options="onClick:_appointment.keep_time"><%=Resources.CRMTREEResource.schedule_btn_keep %></a>
            <a class="easyui-linkbutton" data-options="onClick:_appointment.change_time" style="margin-left: 10px;"><%=Resources.CRMTREEResource.schedule_btn_change %></a></span>
            <span id="NoChng2"><a class="easyui-linkbutton" data-options="onClick:_appointment.keep_time"><%=Resources.CRMTREEResource.cm_buttons_close %></a></span>
            <a class="easyui-linkbutton" data-options="onClick:_appointment.show_time" style="margin-left: 10px;"><%=Resources.CRMTREEResource.schedule_btn_view %></a>
        </div>
     </div>
</div>

</body>
</html>
<script type="text/javascript">
    var _s_url_reports = '/handler/Reports/Reports.aspx';
    var _s_url_appointment = '/handler/Reports/AppointmentManager.aspx';
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

    var ug_utype = '<%=UserSession.User.UG_UType%>';
    if(ug_utype == 1){
        $("#tr_ap_ad_code").hide();
    }else{
        $("#tr_ap_ad_code").show();
    }

    var Run_View = {
        au_code: -1,
        ap_code:-1,
        View_file: function () {
            var width = 400;
            var height = 300;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var features = ",width=" + width + ",height=" + height + ",left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
            var rdm = Math.random();
            var fileName = "/manage/Report/Run_View_file.aspx?AU_Code=" + Run_View.au_code+"&AP_Code="+Run_View.ap_code;
            var myWindow = window.open(fileName + "&r=" + rdm, "fileView", 'status=no,location=no,resizable,toolbar=no,scrollbars' + features, "_blank");
            if (myWindow) myWindow.focus();
        }
    };


    //--------------------------------------------------------------------------------------
    //appointment（预约信息）
    //--------------------------------------------------------------------------------------
    var _appointment = {
        _service: { SD_SA_Selection: true, SD_Serv_Package: true },
        //验证
        validate: function (o) {
            if (_appointment._service.SD_SA_Selection) {
                var bValid = $.form.validate({
                    combogrid: ['AP_AU_Code'],
                    textbox: ['AP_PL_ML_Code_Text'],
                    combobox: ['AP_CI_Code', 'AP_SC_Code', 'AP_Transportation', 'AP_SA_Selected'],
                    datebox: ['EX_AP_Date'],
                    numberspinner: ['AP_Mileage'],
                    timespinner: ['EX_AP_Time']
                });
            } else {
                var bValid = $.form.validate({
                    combogrid: ['AP_AU_Code'],
                    textbox: ['AP_PL_ML_Code_Text'],
                    combobox: ['AP_CI_Code','AP_SC_Code', 'AP_Transportation'],
                    datebox: ['EX_AP_Date'],
                    numberspinner: ['AP_Mileage'],
                    timespinner: ['EX_AP_Time']
                });
            }

            var bValidReason = true;
            if (o.AP_SC_Code == -1 || o.AP_ST_Code == -1) {
                bValidReason = $("#SN_Note").textbox('enableValidation').textbox('isValid');
            } else {
                if (_appointment._service.SD_Serv_Package) {
                    bValidReason = $("#AP_ST_Code").combobox('enableValidation').combobox('isValid');
                }
            }
            return bValid && bValidReason;
        },
        //取消验证
        disableValidation: function () {
            $.form.disableValidation({
                textbox: ['SN_Note', 'AP_PL_ML_Code_Text'],
                combobox: ['AP_SC_Code', 'AP_ST_Code', 'AP_CI_Code', 'AP_SA_Selected','AP_Transportation'],
                combogrid: [],
                datebox: ['EX_AP_Date'],
                numberspinner: ['AP_Mileage'],
                timespinner: ['EX_AP_Time']
            });
        },
        bindData:function(){
            if (_params.AP_Code > 0) {
                //$.mask.show();
                var s_params = JSON.stringify({ action: 'Get_Appointment', AP_Code: _params.AP_Code, SD_PDN_Code: 2 });
                $.post(_s_url_appointment, { o: s_params }, function (res) {
                    //$.mask.hide();
                    if ($.checkResponse(res)) {
                        _appointment.set(res);
                    }
                }, "json");
            } else {
                $.msgTips.info(_isEn ? 'No Appointment Code!' : '无预约编号！');
                return;
            }
        },
        //绑定默认值
        bindDefaults: function () {
            //Date & Time
            var date = _isEn ? '<%=DateTime.Now.AddDays(1).ToString("MM/dd/yyyy")%>' : '<%=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")%>';
            var time = '<%=DateTime.Now.ToString("HH:mm")%>';
            
            $('#EX_AP_Date').datebox().datebox('calendar').calendar({
                validator: function (date) {
                    var y= parseInt('<%=DateTime.Now.Year%>') ,m=parseInt('<%=DateTime.Now.Month%>') - 1,d=parseInt('<%=DateTime.Now.Day%>');
                    var d1 = new Date(y, m, d);
                    return d1 <= date;
                }
            });
            $("#EX_AP_Date").datebox('setValue', date);
            $("#EX_AP_Time").timespinner('setValue', '00:00');
            var _ndate = new Date($("#EX_AP_Date").datebox('getValue'));
            var _dyofweek = _ndate.getDay();
            $('#AP_Weekday').text(_isEn ? _weekDays[0][_dyofweek] : _weekDays[1][_dyofweek]);

            $("#AP_Code").val(0);
        },
        //设置
        set: function (res) {
            var app = res.o;
            if (app) {
                app.AP_SC_Code = app.AP_SC_Code > 0 ? app.AP_SC_Code : -1;
                app.AP_ST_Code = app.AP_ST_Code > 0 ? app.AP_ST_Code : -1;
                $.form.setData('#frm_appointment', app, "#AP_AU_Code,#AP_CI_Code,#AP_SC_Code,#AP_ST_Code");
                $("#AP_AU_Code")
                    .combogrid("setValue", app.AP_AU_Code)
                    .combogrid('setText', app.AU_Name);
                $("#AP_SA_Selected").combobox("setText", app.AP_SA_Selected_text);
                $("#AP_Transportation").combobox("setText", app.AP_Transportation_text);
                $("#AP_AD_Code").combobox("setText", app.AP_AD_Code_text);


               _appointment.car.bindData(app.AP_AU_Code, app.AP_CI_Code, function () {
                   _appointment.onSelect.AP_SC_Code({ value: app.AP_SC_Code }, function () {
                        if (app.AP_SC_Code > 0) {
                            $("#AP_ST_Code").combobox('select', app.AP_ST_Code);
                        }else{
                            _appointment.onSelect.animate(app.AP_SC_Code);
                        }

                        $("#AP_SC_Code").combobox({
                            onSelect: _appointment.onSelect.AP_SC_Code
                        }).combobox('setValue', app.AP_SC_Code);


                        if (app.SN_Note) {
                            $("#SN_Note").textbox('setValue', app.SN_Note);
                        }

                        if (app.SN_Note) {
                            $("#EX_GeneralNote").textbox('setValue', app.SN_Note);
                        }

                        if (_params.action != 'view') {
                            _appointment.buttons.enable();
                        } else {
                            $("#EX_AP_Date").datebox("readonly", true);
                            $("#EX_AP_Date").spinner("readonly", true);
                            $("#AP_SA_Selected,#AP_Transportation").combobox("readonly", true);
                        }
                   });
                });
            }
        },
        //获取
        get: function () {
            var data = $.form.getData("#frm_appointment");
            //$.form.setBoolean(data,[]);
            return data;
        },
        clear:function(){
            _appointment.car.clear();
            _appointment.contact.clear();
            $("#AP_AD_Code").combobox('loadData', []).combobox('clear');
            $("#AP_Mile_C").radiolist('setValue', 0);
        },
        //车辆信息
        car: {
            bindData: function (au_code, ci_code, fn) {
                var o = { action: 'Get_CustomerCars', AU_Code: au_code };
                $.post(_s_url_appointment, { o: JSON.stringify(o) }, function (res) {
                    if (res && res.length > 0) {
                        if (ci_code > 0) {
                                    _appointment.buttons.enable();
                                    _appointment.buttons.show();
                            $.each(res, function (i, o) {
                                if (o && o.CI_Code == ci_code) {
                                    o.selected = true;
                                }
                            });
                        } else {
                            res[0].selected = true;
                        }

                        _appointment.onSelect.AP_CI_Code(res[0]);
                    }

                    $("#AP_CI_Code").combobox('loadData', res);

                    if (fn && $.isFunction(fn)) fn.call();
                }, "json");
            },
            add: function () {
                var au_code = $("#AP_AU_Code").combogrid('getValue');
                if (!(au_code > 0)) { return; }

                var guid = '<%=Guid.NewGuid()%>';
                window.top[guid] = window.self;
                $.topOpen({
                    title: '<%=Resources.CRMTREEResource.ap_car_window_title%>',
                    url: "/templete/usercontrol/CarInfoManager.aspx?AC=AP&AU=" + au_code + "&_winID=" + guid,
                    width: 860, height: 420
                });
            },
            edit: function () {
                var au_code = $("#AP_AU_Code").combogrid('getValue');
                if (au_code > 0) {
                    $.windowOpen("/templete/report/CustomerManager.aspx?TI=Car&action=edit&AU_Code=" + au_code, 890, 500);
                }
            },
            clear: function () {
                $("#AP_CI_Code").combobox('loadData', []).combobox('clear');
            }
        },
        //联系信息
        contact: {
            language:{
                'phone': '<%=Resources.CRMTREEResource.ed_contacts_type_phone%>',
                'eMail': '<%=Resources.CRMTREEResource.ed_contacts_type_eMail%>',
                'messaging': '<%=Resources.CRMTREEResource.ed_contacts_type_messaging%>'
            },
            getContactText: function (type) {
                var typeText = "";
                switch (type) {
                    case 1:
                        typeText = _appointment.contact.language.phone
                        break;
                    case 2:
                        typeText = _appointment.contact.language.messaging
                        break;
                    case 3:
                        typeText = _appointment.contact.language.eMail
                        break;
                }
                return typeText;
            },
            bindData: function (au_code, fn) {
  //              _appointment.buttons.show();
                var o = { action: 'Get_FirstContact', AU_Code: au_code };
                $.post(_s_url_appointment, { o: JSON.stringify(o) }, function (res) {
                    if (res && res.length > 0) {
                        var contact = res[0];
                        if (contact) {
                            contact.AP_PL_ML_Code_Text = _appointment.contact.getContactText(contact.AP_Cont_Type) + ' : ' + contact.AP_PL_ML_Code_Text;
                            _appointment.contact.set(contact);
                        }
                    }
                    if (fn && $.isFunction(fn)) fn.call();
                }, "json");
            },
            edit: function () {
                var au_code = $("#AP_AU_Code").combogrid('getValue');
                if (au_code > 0) {
                    $.windowOpen("/templete/report/CustomerManager.aspx?TI=Con&action=edit&AU_Code=" + au_code, 890, 500);
                }
            },
            set:function(o){
                if (o) {
                    $("#AP_PL_ML_Code").val(o.AP_PL_ML_Code);
                    $("#AP_Cont_Type").val(o.AP_Cont_Type);
                    $("#AP_PL_ML_Code_Text").textbox('setValue',o.AP_PL_ML_Code_Text);
                }
            },
            select: function () {
                var au_code = $("#AP_AU_Code").combogrid('getValue');
                if (!(au_code > 0)) { return; }

                var guid = '<%=Guid.NewGuid()%>';
                window.top[guid] = window.self;
                $.topOpen({
                    title: '<%=Resources.CRMTREEResource.ap_contact_window_title%>',
                    url: "/templete/usercontrol/ContactInfoManager.aspx?AU_Code=" + au_code + "&_winID=" + guid,
                    width: 600, height: 250
                });
            },
            clear: function () {
                $("#AP_Cont_Type ,#AP_PL_ML_Code").val('');
                $("#AP_PL_ML_Code_Text").textbox('clear');
            }
        },
        //保存
        save: function () {
            _appointment.disableValidation();
            var o = $.form.getData("#frm_appointment");
            var bValid = _appointment.validate(o);
            if (bValid && o.AP_AU_Code > 0) {
                $.mask.show();
                $("#btnSave").linkbutton('disable');

                o.AP_Mile_C = o.AP_Mile_C == 1;

                o.AP_Time = o.EX_AP_Date + ' ' + o.EX_AP_Time;
                Run_View.au_code = o.AP_AU_Code;
                var s_params = JSON.stringify({ action: 'Save_Appointment', o: o });
                $.post(_s_url_appointment, { o: s_params }, function (res) {
                    $.mask.hide();
                    $("#btnSave").linkbutton('enable');
                    if ($.checkResponse(res)) {
                        if (_params._cmd == 'cp') {
                            //$.msgTips.save(true);
                            Run_View.ap_code = res.AP_Code;
                            Run_View.View_file();
                        }
                        _appointment.cancel(true);
                    } else {
                        $.msgTips.save(false);
                    }
                }, "json");
            }
        },
        //取消
        cancel: function (bSave) {
            if (_params._cmd == 'cp') {
                $.form.reset("#frm_appointment");
                _appointment.bindDefaults();
                $("#spn_EX_Mileage").hide();
                _appointment.onSelect.animate();
                _appointment.disableValidation();
                return;
            }

            if (window._closeOwnerWindow) {
                window._closeOwnerWindow();
                if(bSave)
                    top[_params._winID]._datagrid.action.refresh();
            }
            else {
                window.close();
            }
        },
        //顾客
        customer: {
            //筛选
            filter: function (newValue, oldValue) {
                if (typeof newValue === 'number') {
                    _appointment.customer.bindData(newValue);
                    return;
                } else {
                    _appointment.clear();
                    _appointment.buttons.disable();
                }

                var q = $.trim(newValue);
                if (q === '') {
                    $("#AP_AU_Code").combogrid('grid').datagrid('loadData', []);
                    return;
                } else {
                    $("#btnAddCustomer").linkbutton('enable');
                }

                var o = { action: 'Get_Customer', q: q };
                $.post(_s_url_appointment, { o: JSON.stringify(o) }, function (res) {
                    if (!$.checkResponse(res)) { res = []; }
                    $("#AP_AU_Code").combogrid('grid').datagrid('loadData', res);
                }, "json");
            },
            //添加
            add: function () {
                var cName = $.trim($("#AP_AU_Code").combogrid('getText'));
                if (cName === '') {
                    $.msgTips.info(_isEn ? "Please input the customer name!" : "请输入客户名称！");
                    return;
                }

                $.mask.show();
                var s_params = JSON.stringify({ action: 'Save_CustomerByName', AU_Name: cName });
                $.post(_s_url_appointment, { o: s_params }, function (res) {
                    $.mask.hide();
                    $("#btnAddCustomer").linkbutton('enable');
                    if ($.checkResponse(res)) {
                        $.msgTips.add(true);

                        if (res.AU_Code > 0) {
                            $("#AP_AU_Code").combogrid('setValue', res.AU_Code + 0).combogrid('setText', res.AU_Name);
                            $("#btnAddCustomer").linkbutton('disable');
                            _appointment.buttons.enable();
                        }
                    } else {
                        $.msgTips.add(false);
                    }
                }, "json");
            },
            edit: function () {
                var au_code = $("#AP_AU_Code").combogrid('getValue');
                if (au_code > 0) {
                    $.windowOpen("/templete/report/CustomerManager.aspx?TI=Per&action=edit&AU_Code=" + au_code, 890, 500);
                }
            },
            //绑定数据
            bindData: function (au_code) {
                _appointment.clear();
                $("#btnAddCustomer").linkbutton('disable');
                _appointment.buttons.disable();
                _appointment.contact.bindData(au_code, function () {
                    _appointment.car.bindData(au_code, null, function () {
                            _appointment.buttons.enable();
                        
                    });
                });
            }
        },
        onChange:{
            AP_Mileage: function (nv, ov) {
                if ($.trim(ov) != "") {
                    $("#AP_Mile_C").radiolist('setValue', 1);
                }
            }
        },
        //联动
        onSelect: {
            //原因动态显示和隐藏
            animate: function (value) {
                if (value == -1) {
                    $("#SN_Note").textbox('enable');
                    $("#tr_SN_Note").show();
                } else {
                    $("#SN_Note").textbox('disable').textbox('clear');
                    $("#tr_SN_Note").hide();
                }
            },
            //服务类别
            AP_SC_Code: function (record, fn) {
                $("#spn_EX_Mileage").hide();
                $("#AP_ST_Code").combobox('clear').initSelect();

                _appointment.onSelect.animate(record.value);

                var value = record.value;
                if (value > 0) {
                    if (value == 1) {
                        if (_appointment._service.SD_Serv_Package) {
                            $("#spn_AP_ST_Code").show();
                        } else {
                            $("#spn_AP_ST_Code").hide();
                        }

                        $("#spn_EX_Mileage").show();
                        $.post(_s_url_reports, { o: JSON.stringify({ action: 'GetLenDonData', rp_code: 4, up_code: value }) },
                        function (res) {
                            if (res) {
                                res.push({ value: '-1', text: 'Other' });
                                var opts =
                                {
                                    data: res
                                }

                                if (value > 0) {
                                    opts.iconWidth = 22;
                                    opts.icons = [{
                                        iconCls: 'icon-help',
                                        handler: function (e) {
                                            var text = $(e.data.target).textbox('getText');
                                            var MP_Code = $(e.data.target).textbox('getValue');
                                            var RS_Code = $("#EX_Mileage").combobox('getValue');
                                            var CI_Code = $("#AP_CI_Code").combobox('getValue');
                                            if (MP_Code > 0) {
                                                $.topOpen({
                                                    url: '/templete/usercontrol/MaintenancePackageDetails.aspx?CI_Code=' + CI_Code + '&RS_Code=' + RS_Code + '&MP_Code=' + MP_Code
                                                    , title: ' ' + text
                                                    , width: 700
                                                    , height: 450
                                                    , iconCls: 'icon-help'
                                                });
                                            }
                                        }
                                    }];
                                }

                                $("#AP_ST_Code").initSelect(opts);
                            }
                            if (fn && $.isFunction(fn)) fn.call();
                        }, "json");
                    } else {
                        $("#spn_AP_ST_Code").show();

                        $.post(_s_url_appointment, { o: JSON.stringify({ action: 'Get_CT_Service_Types', id: value }) },
                        function (res) {
                            if (res) {
                                res.push({ value: '-1', text: 'Other' });
                                $("#AP_ST_Code").initSelect({ data: res, icons: [] });
                            }
                            if (fn && $.isFunction(fn)) fn.call();
                        }, "json");
                    }
                } else {
                    if (fn && $.isFunction(fn)) fn.call();
                }
            },
            //服务类型
            AP_ST_Code: function (record) {
                if (record) {
                    _appointment.onSelect.animate(record.value);
                }
            },
            //选择车辆，加载数据
            AP_CI_Code: function (record) {
                $("#AP_AD_Code").combobox('clear').initSelect();
                $("#AP_Mileage").numberspinner('clear');
                $("#AP_Mile_C").radiolist('setValue', 0);

                $("#EX_Mileage").combobox('clear').initSelect();

                $("#EX_LastMileage").textbox('clear').textbox('setValue', record.CI_Mileage);

                if (record && record.CI_Code > 0) {
                    var ci_code = record.CI_Code ? record.CI_Code : 0;
                    var au_code = $("#AP_AU_Code").combogrid('getValue');
                    au_code = au_code ? au_code : 0;

                    $.post(_s_url_appointment, {
                        o: JSON.stringify({
                            action: 'Get_Car_LendonMsg',
                            CI_Code: ci_code,
                            AU_Code: au_code
                        })
                    }, function (res) {
                        if (res) {
                            var mp_mileage = res.MP_Mileage;
                            $("#EX_Mileage").initSelect({ data: mp_mileage });
                            if (mp_mileage.length > 0) {
                                $("#EX_Mileage").combobox('setValue', mp_mileage[0].value);
                            }

                            var appt_dealers = res.Appt_Dealers;
                            $("#AP_AD_Code").initSelect({
                            onChange: function (nv, ov) {
                                    if (!nv > 0) { return; }
                                    $.post(_s_url_appointment, {
                                        o: JSON.stringify({
                                            action: 'Get_Appt_GetDealerTime',
                                            AD_Code: nv
                                        })
                                    }, function (res) {
                                        if (res && res.length > 0) {
                                            var data = res[0];
                                            var range = {};
                                            if (data.AP_Time_Min > 0) {
                                                range.min = _appointment.formatTime(data.AP_Time_Min);
                                            }
                                            if (data.AP_Time_Max > 0) {
                                                range.max = _appointment.formatTime(data.AP_Time_Max);
                                            }
                                            if (!$.isEmptyObject(range)) {
                                                var tDate = $("#EX_AP_Date").datebox('getValue');
                                                var tTime = $("#EX_AP_Time").timespinner('getValue');
                                                if (tTime=='00:00') {
                                                    tTime = range.min;
                                                }
                                                $("#EX_AP_Time").timespinner(range);
                                                $("#EX_AP_Time").timespinner({
                                                    highlight:1,
                                                    increment: data.AP_Time_Inc
                                                });
                                                $("#EX_AP_Time").timespinner('setValue', tTime);

                                            }
                                            if (data.SD_Appt_Days <= 0) {
                                                $('#EX_AP_Date').datebox().datebox('calendar').calendar({
                                                    validator: function (date) {
                                                        var y = parseInt('<%=DateTime.Now.Year%>'), m = parseInt('<%=DateTime.Now.Month%>') - 1, d = parseInt('<%=DateTime.Now.Day%>');
                                                        var d1 = new Date(y, m, d);
                                                        var d2 = new Date(y, m, d + parseInt(data.SD_Appt_Days));
                                                        return d1 <= date && date <= d2;
                                                    }
                                                });
                                                var _ndate = new Date($("#EX_AP_Date").datebox('getValue'));
                                                var _dyofweek = _ndate.getDay();
                                                $('#AP_Weekday').text(_isEn ? _weekDays[0][_dyofweek] : _weekDays[1][_dyofweek]);

                                            }
                                        }
                                    }, "json");
                                }, data: appt_dealers
                            });
                            if (appt_dealers.length > 0) {
                                var AD_Code = appt_dealers[0].AD_Code;
                                for (var i = 0, len = appt_dealers.length; i < len; i++) {
                                    if (appt_dealers[i]["Selected_Flg"] == 1) {
                                        AD_Code = appt_dealers[i]["AD_Code"];
                                        break;
                                    }
                                }
                                $("#AP_AD_Code").combobox('setValue', AD_Code);
                            }

                            var appt_mileage = res.Appt_Mileage;
                            if (appt_mileage && appt_mileage.length > 0) {
                                $("#AP_Mileage").numberspinner("setValue", appt_mileage[0].E_Mileage);
                            }
                        }
                    }, "json");
                }
            }
        },
        //格式化小时
        formatHour : function (value) {
            value = parseInt(value);
            return (value < 10 ? '0' : '') + value;
        },
        //格式化时间
        formatTime: function (v) {
            v = $.trim(v);
            if (v === '') { return v; }
            var a_v = v.split('.');
            a_v[0] = _appointment.formatHour(a_v[0]);
            if (a_v.length > 1) {
                a_v[1] += a_v[1].length === 1 ? '0' : '';
            } else {
                a_v.push('00');
            }
            return a_v.join(':');
        },
        schedule: function (e,Empl_Code) {
            if (Empl_Code > 0) {
                $.windowOpen("/templete/report/EmployeeDetails.ASPX?TI=Sch&Empl_Code=" + Empl_Code, 890, 500);
            }
            stopPropagation(e);
        },
        keep_time: function () {
            $("#w_schedule").window("close");
        },
        change_time: function () {
            $("#w_schedule").window("close");
            $("#EX_AP_Date").datebox('setValue',_ap_time.AP_Date);
            $("#EX_AP_Time").timespinner('setValue', _ap_time.AP_Time);
            var _ndate = new Date($("#EX_AP_Date").datebox('getValue'));
            var _dyofweek = _ndate.getDay();
            $('#AP_Weekday').text(_isEn ? _weekDays[0][_dyofweek] : _weekDays[1][_dyofweek]);


        },
        show_time: function () {
            $("#w_schedule").window("close");
            var Empl_Code = $("#AP_SA_Selected").combobox("getValue");
            $.windowOpen("/templete/report/EmployeeDetails.ASPX?TI=Sch&Empl_Code=" + Empl_Code, 890, 500);
        },
        onSelectDate:   function (date) {
            var _dyofweek = date.getDay();
            $('#AP_Weekday').text(_isEn ? _weekDays[0][_dyofweek] : _weekDays[1][_dyofweek]);
        },
    //格式化项
        format: {
            //顾问
            //advisor: function (row) {
            //    var text = $.trim(row.text);
            //    if (text) {
            //        var imgName = $.trim(row.DE_Picture_FN) ? row.DE_Picture_FN : 'defaultAvatar.gif';
            //        var s = '<div style="padding:5px;">'
            //            + '<div style="font-weight:bold;text-align:left;">' + text + '</div>'
            //            + '<img src="/images/Adviser/' + imgName + '" style="width:70px;height:70px;border:0;"/>'
            //            + '</div>'
            //        ;
            //        return s;
            //    }
            //    return row.text;
            //},
            advisor: function (row) {
                var bgColor = '#BEFFA5';
                var status = '<%=Resources.CRMTREEResource.Available %> ';
                if (row.DE_Avl == 1 && row.DE_Busy == 1) {
                    status = '<%=Resources.CRMTREEResource.Busy %>';
                    bgColor = '#F9F7A2'
                }
                if (row.DE_Avl == 0) {
                    status = '<%=Resources.CRMTREEResource.Unavailable %>';
                    bgColor = '#C7C7C7'
                }
                var imgName = $.trim(row.DE_Picture) ? row.DE_Picture : 'defaultAvatar.gif';
                var s = '<div style="margin:0px;font-size:10px;">'
                    + '<div style="margin:1px;padding:2px;background-color:' + bgColor + ';height:97px">'
                    + '<div style="valign:center"><img src="/images/Adviser/' + imgName + '" style="width:50px;height:50px;border:0;"/>'
                    + '<div style="height:20px;float:right"><a href="javascript:;" onclick="_appointment.schedule(event,\'' + row.DE_Code + '\');"><%=Resources.CRMTREEResource.Schedule %></a>'
                    + '</div>'
                    + '<div style="  width: 50px; margin: -15px 2px 0px 55px;"><b><%--=Resources.CRMTREEResource.Status --%></b>' + status
                    + '</div>'
                    + '</div>'
                    + '<div style="  margin: 7px 5px 0px 3px; text-align:left;line-height:15px;">'
                    + '<b><%=Resources.CRMTREEResource.ap_name %> <span style="FONT-SIZE: 12PX;">' + $.trim(row.DE_Name)+'</span></b>'
                    + '</br>'
<%--                    + '<b><%=Resources.CRMTREEResource.Comment %> </b>'--%>
                    + $.trim(row.DE_Commnt)
                    + '</div>'
                    +' </div>'
                    +' </div>'
                ;
                return s;
            },
            //汽车
            car: function (row) {
                var text = $.trim(row.Cars);
                if (text) {
                    var imgs = [];
                    var imgNames = $.trim(row._img_srcs) ? row._img_srcs.split(',') : [];
                    for (var i = 0, len = imgNames.length; i < len; i++) {
                        var imgName = $.trim(imgNames[i]);
                        if (imgName) {
                            imgs.push('<img src="/plupload/customer/' + imgName + '" style="width:100px;height:100px;border:0;margin-right:5px;"/>');
                        }
                    }

                    var s = '<div style="padding:5px;">'
                        + '<div style="font-weight:bold;text-align:left;">' + text + '</div>'
                        + imgs.join('')
                        + '</div>'
                    ;
                    return s;
                }
                return row.Cars;
            }
        },
        //按钮
        buttons: {
            //启用
            enable: function () {
                var btns = _params.action === 'edit' ? '' : ',#btnContactInfo,#btnCarInfo,#btnEditCustomer,#btnEditContact,#btnEditCar';
                $("#btnSave" + btns).linkbutton('enable');
                if (_params.action != 'view') {
                    //$("#Edit_Btn_1").show();
                    //$("#Edit_Btn_2").show();
                    //$("#Edit_Btn_3").show();
                }
            },
            show: function () {
                $("#Edit_Btn_1").show();
                $("#Edit_Btn_2").show();
                $("#Edit_Btn_3").show();
            },
           //禁用
            disable: function () {
                $("#btnSave,#btnContactInfo,#btnCarInfo,#btnAddCustomer,#btnEditCustomer,#btnEditContact,#btnEditCar").linkbutton('disable');
                $("#Edit_Btn_1").hide();
                $("#Edit_Btn_2").hide();
                $("#Edit_Btn_3").hide();
            }
        }
    };

    //设置打开窗体
    _setWindow = function (win) {
        if (win) {
            var opts = win.window('options');
            opts.title = _params.action === 'edit' ? '<%=Resources.CRMTREEResource.ap_window_editTitle%>' : '<%=Resources.CRMTREEResource.ap_window_title%>';
            win.window(opts);
        }
    }

    _ap_time = { AP_Date: '', AP_Time: '' };
    _set_ap_time = function (DE_Code) {
        var Sel_Date = $("#EX_AP_Date").datebox('getValue');
        var Sel_Time = $("#EX_AP_Time").timespinner('getValue').replace(":", ".");
        var AD_Code = $("#AP_AD_Code").combobox('getValue');
        $.post(_s_url_appointment, {
            o: JSON.stringify({
                action: 'Get_Appt_GetAdvisorTime',
                AD_Code: AD_Code,
                DE_Code: DE_Code,
                Sel_Date: Sel_Date,
                Sel_Time: Sel_Time
            })
        }, function (res) {
            if (res && res.Table) {
                var data = res.Table[0];
                _ap_time.AP_Date = $.trim(data.AP_Date);
                _ap_time.AP_Time = _appointment.formatTime(data.AP_Time);
                var _ndate = new Date(_ap_time.AP_Date);
                var _dyofweek = _ndate.getDay();
                var _Dday = _isEn ? _weekDays[0][_dyofweek] : _weekDays[1][_dyofweek];

                $("#advisor_time").text('['+_Dday+'] '+_ap_time.AP_Date + ' ' + _ap_time.AP_Time);
                $("#advisor_rem").text($.trim(data.AP_Time_Remark));
            }
            if (data.Found == 1) {
                $('#YesChng0').show();
                $('#YesChng1').show();
                $('#YesChng2').show();
                $('#advisor_time').show();
                $('#NoChng2').hide();

            } else {
                $('#YesChng0').hide();
                $('#YesChng1').hide();
                $('#YesChng2').hide();
                $('#advisor_time').hide();
                $('#NoChng2').show();
            }

            $("#w_schedule").window('open');
        }, "json");
    };


    $(function () {
        $("#AP_SA_Selected").combobox('panel').addClass('advisor');

        //初始化
        (function Init() {
            if (_params._cmd == 'cp') {
                $("#btnCancel").linkbutton({ text: '<%=Resources.CRMTREEResource.CancelBtn%>' });
            }

            $.form.fluid("#frm_appointment");
            $("#tr_SN_Note").hide();

            _appointment.buttons.disable();

            _appointment.bindDefaults();
            _appointment.disableValidation();

            $.post(_s_url_appointment, { o: JSON.stringify({ action: 'Get_Service_Dep', SD_PDN_Code: 2 }) }, function (res) {
                if ($.checkResponse(res, false)) {
                    _appointment._service.SD_SA_Selection = res.SD_SA_Selection === false ? false : true;
                    _appointment._service.SD_Serv_Package = res.SD_Serv_Package === false ? false : true;
                }
                if (_appointment._service.SD_SA_Selection) {
                    $("#c_tr_advisor").show();
                } else {
                    $("#c_tr_advisor").hide();
                }

                BindSelects();
            }, "json");
            

            if (_params.action === 'edit' || _params.action === 'view') {
                $("#AP_AU_Code").combogrid("readonly", true);
                $("#AP_SC_Code,#AP_ST_Code,#AP_CI_Code,#EX_Mileage").combobox("readonly", true);
            } else {
                var $c = $("#AP_AU_Code");
                $c.combogrid({ onChange: _appointment.customer.filter })
                    .combogrid('textbox')
                    .click(function () { $c.combogrid('showPanel'); })

                //customer
                if (_params.AU_Code > 0) {
                    if (!_params.AU_Name || _params.AU_Name == 'undefined') {
                        var params = { action: 'Get_CustomerName', AU_Code: _params.AU_Code };
                        _params.AU_Name = $.ajax({
                            async: true,
                            url: _s_url_appointment,
                            data: { o: JSON.stringify(params) },
                            dataType: "json",
                            timeout:60000
                        }).responseText;
                    }
                    $c.combogrid('setValue', parseInt(_params.AU_Code)).combogrid('setText', _params.AU_Name);
                }

                $c.combogrid('resize', "100%");
            }

            InitCustomerName();

            $("#spn_EX_Mileage").hide();
        })();

        function InitCustomerName() {
            $("#AP_AU_Code").combogrid('textbox').focus();
        }

        //绑定下拉列表
        function BindSelects() {
            _appointment.onSelect.AP_CI_Code({ CI_Code: 0 });

            $.post(_s_url_appointment, { o: JSON.stringify({ action: 'Get_Selects' }) }, function (res) {
                if (res) {
                    $("#AP_Transportation").initSelect({
                        valueField: 'XP_Code',
                        textField: 'XP_Desc',
                        data: [],//res.CT_Transportation
                        onShowPanel: function () {
                            var AU_Code = $("#AP_AU_Code").combogrid('getValue');
                            if (!AU_Code > 0) { return; }
                            var Sel_Date = $("#EX_AP_Date").datebox('getValue');
                            var Sel_Time = $("#EX_AP_Time").timespinner('getValue').replace(":", ".");
                            var App_Type = $("#AP_SC_Code").combobox('getValue');
                            var CI_Code = $("#AP_CI_Code").combobox('getValue');
                            $.post(_s_url_appointment, {
                                o: JSON.stringify({
                                    action: 'Get_Appt_Trans',
                                    Sel_Date: Sel_Date,
                                    Sel_Time: Sel_Time,
                                    App_Type: App_Type,
                                    AU_Code:AU_Code,
                                    CI_Code: CI_Code
                                })
                            }, function (res) {
                                if (res) {
                                    var v = $("#AP_Transportation").combobox('getValue');
                                    $("#AP_Transportation").initSelect({
                                        data: res
                                    //    ,onSelect: function (record) {
                                    //        if (record.XP_Avl == 0) {
                                    //            var ad_code = $("#AP_AD_Code").combobox('getValue');
                                    //            if (ad_code > 0) {
                                    //                _set_ap_time(ad_code);
                                    //            }
                                    //        }
                                    //    }
                                    });
                                    if (!(v > 0) && res.length > 0) {
                                        v = res[0].XP_Code;
                                        $.each(res, function (i, o) {
                                            if (o.Selected_Flg == 1) {
                                                v = o.XP_Code;
                                            }
                                        });
                                    }
                                    if (v > 0) {
                                        $("#AP_Transportation").combobox('setValue', v);
                                    }
                                }
                            }, "json");
                        }
                    });

                    if (res.CT_Serv_Category) {
                        res.CT_Serv_Category.push({ value: '-1', text: 'Other' });
                    }

                    $("#AP_SC_Code").initSelect({
                        data: res.CT_Serv_Category,
                        onSelect: (_params.action === 'edit' ||_params.action === 'view') ? function(){} : _appointment.onSelect.AP_SC_Code,
                        onChange: function (newValue, oldValue) {
                            var that = this;
                            window.setTimeout(function () {
                                var v = $(that).combobox('getValue');
                                if (!(v > 0)) {
                                    $("#AP_ST_Code").combobox('clear').initSelect({icons:[]});
                                }
                            }, 0);
                        }
                    });

                    $("#AP_SA_Selected").initSelect({
                        //panelWidth：650，
                        valueField: 'DE_Code',
                        textField: 'DE_Name',
                        editable: true,
                        formatter: _appointment.format.advisor,
                        data: [],//res.Dealer_Advisor
                        onShowPanel: function () {
                            var AU_Code = $("#AP_AU_Code").combogrid('getValue');
                            if (!AU_Code > 0) { return; }
                            var Sel_Date = $("#EX_AP_Date").datebox('getValue');
                            var Sel_Time = $("#EX_AP_Time").timespinner('getValue').replace(":", ".");
                            var App_Type = $("#AP_SC_Code").combobox('getValue');
                            var CI_Code = $("#AP_CI_Code").combobox('getValue');
                            $.post(_s_url_appointment, {
                                o: JSON.stringify({
                                    action: 'Get_Appt_Advisor',
                                    Sel_Date: Sel_Date,
                                    Sel_Time: Sel_Time,
                                    App_Type: App_Type,
                                    CI_Code: CI_Code
                                })
                            }, function (res) {
                                if (res) {
                                    var v = $("#AP_SA_Selected").combobox('getValue');
                                    $("#AP_SA_Selected").initSelect({ editable: true, data: res });
                                    if (!(v > 0) && res.length > 0) {
                                        v = res[0].DE_Code;
                                        $.each(res, function (i, o) {
                                            if (o.Selected_Flg == 1) {
                                                v = o.DE_Code;
                                            }
                                        });
                                    }
                                    if (v > 0) {
                                        $("#AP_SA_Selected").combobox('setValue', v);
                                    }
                                }
                            }, "json");
                        },
                        onSelect: function (record) {
                            if (record && (record.DE_Avl == 0 || record.DE_Busy == 1)) {
                                _set_ap_time(record.DE_Code);
                            }
                        }
                    });

                    if (_params.action === 'edit' || _params.action === 'view') {
                        _appointment.bindData();
                    }
                }
            }, "json");
        }
    });
</script>
