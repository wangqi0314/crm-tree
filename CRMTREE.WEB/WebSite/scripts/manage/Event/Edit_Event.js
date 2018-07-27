//获取请求给页面的参数
var getUrlParam = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//页面的相关设置
var Page = function () { };

Page.Initialization = function () {
    if (parseInt(getUrlParam("id")) > 0) {
        //var input_hid_AU_Type = $("#input_hid_AU_Type").val();
        //var input_hid_CG_UType = $("#input_hid_CG_UType").val();
        //if (input_hid_AU_Type != input_hid_CG_UType) {
        //    $("#txt_title").attr("disabled", true);
        //    $("#txt_desc").attr("disabled", true);
        //    $("#Sales_type_1").attr("disabled", true);
        //    $("#Sales_type_2").attr("disabled", true);
        //    $("#Sales_type_3").attr("disabled", true);
        //    $("#Sales_type_4").attr("disabled", true);
        //    $("#radbtn_type_time_1").attr("disabled", true);
        //    $("#radbtn_type_time_0").attr("disabled", true);
        //    $("#txt_Start_dt").attr("disabled", true);
        //    $("#txt_End_dt").attr("disabled", true);
        //    $("#ckb_method_1").attr("disabled", true);
        //    $("#ckb_method_2").attr("disabled", true);
        //    $("#ckb_method_3").attr("disabled", true);
        //    $("#ckb_method_4").attr("disabled", true);
        //    $("#ckb_method_5").attr("disabled", true);
        //    $("#ckb_method_6").attr("disabled", true);
        //    $("#radbtn_pmc_1").attr("disabled", true);
        //    $("#radbtn_pmc_2").attr("disabled", true);
        //    $("#radbtn_pmc_3").attr("disabled", true);
        //    $("#ddl_Targeted").attr("disabled", true);
        //    $("#ddl_Succ").attr("disabled", true);
        //    $("#SWFUpload_0").hide();
        //    $("#SWFUpload_0").attr("disabled", true);
        //}
        //===================================================================================================================================
        //设置是否分享
        if ($("#input_hid_Share").val() == "True") {
            $("#input_checkbox_Share").prop("checked", true);
        }
        else {
            $("#input_checkbox_Share").prop("checked", false);
        }
        //是否积极
        if ($("#input_hid_active").val() == "1") {
            $("#input_checkbox_active").prop("checked", true);
        }
        else {
            $("#input_checkbox_active").prop("checked", false);
        }
        //====================================================================================================================================
        //事件类型
        var inttype = $.trim($("#hid_type").val());
        $("#Sales_type_" + inttype).prop("checked", "checked");
        //====================================================================================================================================
        //目标客户
        Binding_Defalut_ddl_Targeted();
        //====================================================================================================================================
        //方法
        var strmethod = $.trim($("#hid_method").val());
        var strs = new Array(); //定义一数组
        strs = strmethod.split(","); //字符分割     
        for (i = 0; i < strs.length ; i++) {
            $("#ckb_method_" + strs[i]).prop("checked", "checked");
            if (strs[i] == "1") {
                $("#sp_person").show();
                $("#radbtn_pmc_" + $.trim($("#hid_Whom").val())).prop("checked", "checked");
            }
        }
        var hid_Mess = $("#hid_Mess_Type").val();
        $("#radbtn_Mess_" + hid_Mess).prop("checked", "checked");
        //=============================================================================
        //事件风格
        $("#ddl_Genre").attr("value", $.trim($("#hid_Genre").val()));
        //=============================================================================
        //事件保留请求
        var bool_RSVP = $.trim($("#hid_RSVP").val());
        if (bool_RSVP == "True") {
            $("#RSVP_1").prop("checked", "checked");
        } else {
            $("#RSVP_0").prop("checked", "checked");
        }
        //========================================================================================================
        //相应人员
        var bool__Person = $.trim($("#hid_Person").val());
        $("#DRP_Person").combobox({
            onLoadSuccess: function () {
                $("#DRP_Person").combobox('setValues', $.trim(bool__Person).split(','));
            }
        });
        //========================================================================================================
        //推荐工具
        var bool_Tools = $.trim($("#hid_Tools").val());
        $("#DRP_Tools").combobox({
            onLoadSuccess: function () {
                $("#DRP_Tools").combobox('setValues', $.trim(bool_Tools).split(','));
            }
        });
        //$('#cc').combobox('setValues', ['001', '002']);
        //========================================================================================================
        //预算
        $("#txt_Budget").attr("value", $.trim($("#hid_Budget").val()));
        //========================================================================================================
        //$("#ddl_Targeted").attr("value", $.trim($("#hid_Targeted").val()));
        //成功竞选模型
        $("#ddl_Succ").attr("value", $.trim($("#hid_Succ").val()));
        //==========================================================================================================
        var strFileName = $.trim($("#hdFileNmae").val());
        if (1 < strFileName.length) {
            var SWFUploadHtml = "<div id=\"SWFUpload_1_1\" class=\"progressobj\"><input type=\"button\" class=\"IcoNormal\"><span class=\"ftt\"><a id=\"a_EditSWFUpload_1_1\" accesskey=\"" + strFileName + "\" class=\"js_Edit\" fullname=\"" + strFileName + "\">Edit</a></span><span class=\"ftt\"><a id=\"a_SWFUpload_1_1\" accesskey=\"" + strFileName + "\" class=\"js_delete\" fullname=\"" + strFileName + "\">Delete</a></span><span class=\"ftt\"><a id=\"a_SWFUpload_1_1\" href=\"/upload/" + strFileName + "\" class=\"js_View\" target=\"_blank\"  fullname=\"" + strFileName + "\">View</a></span></div>";
            $("#divprogresscontainer").append(SWFUploadHtml);
        }
        //SWFdisabled();
        $("#File_E").hide();
    }
    else {
        SWFupload();
    }
    var input_hid_AU_Type = $("#input_hid_AU_Type").val();
    if (input_hid_AU_Type == "1") {
        $("#input_checkbox_Share").hide();
        $(".share").hide();
    }
    var input_hid_UG_Code = $("#input_hid_UG_Code").val();
    if (input_hid_UG_Code == "41") {
        $("#input_checkbox_active").show();
        $(".active").show();
    }
    if (input_hid_UG_Code == "64") {
        $("#input_checkbox_active").show();
        $(".active").show();
    }
};
// Matrix 关于矩阵的相关操作
var Matrix = function () { };

Matrix.SuccessMatrix = function () {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8",
        url: "Edit_Event.aspx/getSuccMatrxList_s", data: null, async: false,
        success: function (data) {
            if (data.d != null) {
                var SuccMatrix = data.d.SuccMatrixList;
                SuccMatrix = Matrix.SuccessMatrix.bind(SuccMatrix);
                $("#Succ").combobox({
                    valueField: 'PSM_Code',
                    textField: 'PSM_Desc_EN',
                    multiple: true,
                    panelHeight: '140',
                    data: SuccMatrix                    
                });
            }
        }
    });
};

Matrix.SuccessMatrix.bind = function (SuccMatrix) {
    var o;
    var os = new Array();
    var _id = getUrlParam("id");
    for (var i = 0; i < SuccMatrix.length; i++) {
        if (i == 0 && _id == null)
            o = { PSM_Code: SuccMatrix[i].PSM_Code, PSM_Desc_EN: SuccMatrix[i].PSM_Desc_EN, selected: true }
        else
            o = { PSM_Code: SuccMatrix[i].PSM_Code, PSM_Desc_EN: SuccMatrix[i].PSM_Desc_EN };
        os.push(o);
    }
    return os;
};

Matrix.SuccessMatrix.Select = function () {
    $("#Succ").combobox({
        onChange: function (newValue, oldValue) {
            $(".succ_validate").hide();
            $(".succ_Matri_param").empty();
            var _id = getUrlParam("id");
            if (_id == null) { _id = -1;}
            $.ajax({
                type: "POST", dataType: "json", contentType: "application/json; charset=utf-8",
                url: "Edit_Event.aspx/getSuccMatrx_s", data: "{Code:" + _id + ", Code_s:'" + newValue.toString() + "'}", async: false,
                success: function (data) {
                    if (data.d != null) {
                        var SuccMatrix = data.d.SuccMatrixList;
                        $(".succ_Matri_param").empty();
                        for (i = 0; i < SuccMatrix.length; i++) {
                            var text = '<li class="succ_Matri_L">';
                            text += '' + SuccMatrix[i].PSM_Desc_EN + '';
                            text += '<input id="day' + SuccMatrix[i].PSM_Code + '" style="width: 50px; height: 22px" />';
                            text += 'days >>> ' + SuccMatrix[i].PSM_Val_Type_EN + '';
                            text += '<input id="val' + SuccMatrix[i].PSM_Code + '" style="width: 100px; height: 22px" />';
                            text += '</li>';

                            $(".succ_Matri_param").append(text);
                            $('#day' + SuccMatrix[i].PSM_Code).textbox({ type: 'text', value: SuccMatrix[i].SMV_Days });
                            $('#val' + SuccMatrix[i].PSM_Code).textbox({ type: 'text', value: SuccMatrix[i].SMV_Val });
                        }
                    }
                }
            });
        }
    });
};

Matrix.SuccessMatrix.Default = function () {
    $("#Succ").combobox({
        onLoadSuccess: function () {
            var _id = getUrlParam("id");
            if (_id != null) {
                $("#Succ").combobox('setValues', $.trim($("#hid_Succ").val()).split(','));
                //$("#Succ").combobox('setValues', $.trim($("#hid_Succ").val()).split(','));
            }
        }
    });
};

Matrix.SuccessMatrix.getParamvalue = function () {
    var o_s = $("#Succ").combobox('getValues');
    if (o_s == null || o_s == "") { return null; }
    var _o;
    var o = new Array();
    for (i = 0; i < o_s.length; i++) {
        _o = { PSM_Code: o_s[i], SMV_Days: $('#day' + o_s[i]).textbox('getText'), SMV_Val: $('#val' + o_s[i]).textbox('getText') };
        o.push(_o);
    }
    return JSON.stringify(o);
}

Matrix.SuccessMatrix.checkValue = function () {
    var o_s = $("#Succ").combobox('getValues');
    if (o_s == null || o_s == "") { return false; }
    var _o;
    for (i = 0; i < o_s.length; i++) {
        _o = $('#day' + o_s[i]).textbox('getText');
        if (_o == null || $.trim(_o) == "") { return false;}
        _o = $('#val' + o_s[i]).textbox('getText');
        if (_o == null || $.trim(_o) == "") { return false; }
    }
    return true;
}

$(document).ready(function () {
    var swfu;
    $("#radbtn_type_time_1").click(function () {
        $("#sp_Type_time").hide("slow");
    });
    $("#ckb_method_1").click(function () {
        if ($(this).prop("checked") == true) {
            $("#sp_person").show("slow");
        }
        else {
            $("#sp_person").hide("slow");
        }
    });
    $("#btnSave").click(function () {
        if (CheckInput()) {
            $.confirm('Do you want to continue saving this event?', 'Message Confirm ', function (action) {
                //confirm 返回三个 action 值，分别是 'ok'、'cancel' 和 'close'。
                if (action == 'ok') {
                    var $data = $("body").data("event");
                    if ($data == null) {
                        CheckInput();
                        var $data = $("body").data("event");
                    }
                    var strAction = "add_event";
                    var EV_Code = getUrlParam("id");
                    if (0 < parseInt(EV_Code)) {
                        strAction = "event_modify";
                    }
                    var o_succ=Matrix.SuccessMatrix.getParamvalue();
                    ///*2014-07-10 WangQi*/
                    var RP_Code = $("#ddl_Targeted").val();
                    Paramterslist = $(".hidUpdateTargeted").find(".PL_Val");
                    var Paramters = strConnect(Paramterslist); //用逗号连接字符串
                    ParamtersCode_list = $(".hidUpdateTargeted").find(".PL_Code");
                    var ParamtersCode = strConnect(ParamtersCode_list); //用逗号连接字符串
                    var df = ParamtersCode;
                    $.ajax({
                        type: "POST",
                        dataType: "text",
                        url: "/handler/ajax_Event.aspx",
                        data: {
                            action: strAction, EV_Title: escape($data.EV_Title), EV_Share: $data.EV_Share, EV_Active_Tag: $data.EV_Active_Tag,
                            EV_Desc: escape($data.EV_Desc), EV_Type: $data.EV_Type, EV_RP_Code: $data.EV_RP_Code, EV_Start_dt: $data.EV_Start_dt,
                            EV_End_dt: $data.EV_End_dt, EV_Method: $data.EV_Method, EV_Whom: $data.EV_Whom, EV_Mess_Type: $data.EV_Mess_Type,
                            EV_EG_Code: $data.EV_EG_Code, EV_RSVP: $data.EV_RSVP, EV_Act_S_dt: $data.EV_Act_S_dt, EV_Act_E_dt: $data.EV_Act_E_dt,
                            EV_Respnsible: escape($data.EV_Respnsible), EV_Tools: escape($data.EV_Tools), EV_Budget: escape($data.EV_Budget),
                            EV_Succ_Matrix: escape($data.EV_Succ_Matrix), EV_Filename: escape($data.EV_Filename), EV_Code: EV_Code, PL_Code_List: ParamtersCode,
                            PL_Val_List: escape(Paramters), o_succ: escape(o_succ)
                        },
                        success: function (data) {
                            if (0 == data) {
                                $.tips('Login Timeoutr.', 'error', 3000);
                            }
                            else if (-1 == data) {
                                $.tips('Without Permission.', 'error', 3000);
                            }
                            else if (-2 == data) {
                                $.tips('Incorrect parameter.', 'error', 3000);
                            }
                            else if (-3 == data) {
                                $.tips('The operation failed.', 'error', 3000);
                            }
                            else if (-4 == data) {
                                $.tips('Title already exists.', 'error', 3000);
                            }
                            else if (0 < data) {

                                $.tips('success.', 'success');
                                if (strAction == "add_event") {
                                    window.location.href = "/manage/Event/EventList.aspx";
                                }
                                else {
                                    setTimeout(function () {
                                        window.location.href = "/manage/Event/EventList.aspx";
                                    }, 2000);
                                }
                            }
                            else {
                                $.tips('The operation failed.', 'error', 3000);
                            }

                        }, error: function (er) {
                            $.tips('The operation failed.', 'error', 3000);
                        }
                    });
                }
            });
            $("#asyncbox_confirm_ok").find("span").html("Save");
            $("#asyncbox_confirm_cancel").find("span").html("Back to Edit");
        }
    });
    $("#btnTargeted").click(function () {
        $.open({
            id: 'open', url: '/manage/Event/UpdateTargeted.aspx?RP_Code=' + $("#ddl_Targeted").val() + '&CG_Code=' + getUrlParam("id"),
            title: 'Update Event Targeted Customer Parameter', width: 600, height: 300, modal: false,
            btnsbar: $.btn.OKCANCEL.concat(),
            callback: function (action, iframe) {
                if (action == 'ok') {
                    $(".hidUpdateTargeted").empty();
                    $(".hidUpdateTargeted").append($(iframe.document).find(".Paramterslist").html());
                    $("#hidUpdateTargeted_RP_code").val($("#ddl_Targeted").val());
                    $("#hidUpdateTargeted_Count").val("1");
                    var Paramterslist = $(iframe.document).find(".Paramterslist").find(".PL_Val");
                    if (Paramterslist.length <= 0) {
                        return false;
                    }
                    var Paramters = strConnect(Paramterslist); //用逗号连接字符串
                    var CG_Code = getUrlParam("id");
                    if (CG_Code == null) { CG_Code = 0; }
                    $.ajax({
                        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8", url: "Edit_Event.aspx/getReplaceReports",
                        data: "{RP_Code:'" + $("#ddl_Targeted").val() + "',CG_Code:" + CG_Code + ",Paramterslist:'" + Paramters + "'}", async: false,
                        success: function (data) {
                            if (data.d == null) {
                                return false;
                            }
                            var Targeted = data.d;

                            $("#hid_Targeted").val(Targeted.RP_Code);
                            var option = "<option value='" + Targeted.RP_Code + "'>" + Targeted.RP_Name_EN + "</option>";
                            $("#ddl_Targeted option[value='" + Targeted.RP_Code + "']").remove();

                            $("#ddl_Targeted").append(option);
                            $("#ddl_Targeted").prop("value", Targeted.RP_Code);
                        }
                    });
                }
            }
        });
        $("#open_ok").find("span").html("Update");
        $("#open_cancel").find("span").html("Cancel");
    });
    $("#ddl_Targeted").bind("change", function () {
        $("#hidUpdateTargeted_Count").val("0");
        $.open({
            id: 'open',
            url: '/manage/Event/UpdateTargeted.aspx?RP_Code=' + $("#ddl_Targeted").val() + '&CG_Code=' + getUrlParam("id"),
            title: 'Update Event Targeted Customer Parameter', width: 600, height: 300, modal: false,
            btnsbar: $.btn.OKCANCEL.concat(),
            callback: function (action, iframe) {
                if (action == 'ok') {
                    var UpdateTargeted = $(iframe.document).find(".Paramterslist").html();
                    $(".hidUpdateTargeted").empty();
                    $(".hidUpdateTargeted").append(UpdateTargeted);
                    $("#hidUpdateTargeted_RP_code").val($("#ddl_Targeted").val());
                    $("#hidUpdateTargeted_Count").val("1");
                    var Paramterslist = $(iframe.document).find(".Paramterslist").find(".PL_Val");
                    if (Paramterslist.length <= 0) {
                        return false;
                    }
                    var Paramters = strConnect(Paramterslist); //用逗号连接字符串
                    var CG_Code = getUrlParam("id");
                    if (CG_Code == null) { CG_Code = 0; }
                    $.ajax({
                        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8", url: "Edit_Event.aspx/getReplaceReports",
                        data: "{RP_Code:'" + $("#ddl_Targeted").val() + "',CG_Code:" + CG_Code + ",Paramterslist:'" + Paramters + "'}", async: false,
                        success: function (data) {
                            if (data.d == null) {
                                return false;
                            }
                            var Targeted = data.d;
                            $("#hid_Targeted").val(Targeted.RP_Code);
                            var option = "<option value='" + Targeted.RP_Code + "'>" + Targeted.RP_Name_EN + "</option>";
                            $("#ddl_Targeted option[value='" + Targeted.RP_Code + "']").remove();
                            $("#ddl_Targeted").append(option);
                            $("#ddl_Targeted").prop("value", Targeted.RP_Code);
                        }
                    });
                }
            }
        });
        $("#open_ok").find("span").html("Update");
        $("#open_cancel").find("span").html("Cancel");
    });
    $(".js_delete").live("click", function () {
        var obj = $(this);
        var objDIV_ID = obj.attr("id").substring(2);
        var objFname = obj.attr("fullname");
        $.confirm('Countinue Delete?', 'Message Confirm ', function (action) {
            //confirm 返回三个 action 值，分别是 'ok'、'cancel' 和 'close'。
            if (action == 'ok') {
                $("#" + objDIV_ID).remove();
                var strFileName = $("#hdFileNmae").val();
                $("#hdFileNmae").val(strFileName.replace("," + objFname + ",", ",")); //从文本框中删除objFname
                $.ajax({
                    type: "POST",
                    dataType: "text",
                    url: "/handler/ajax_campaign.aspx",
                    data: { action: "del_file", fullname: objFname },
                    success: function (data) {
                    }
                });
            }
        });
        $("#asyncbox_confirm_ok").find("span").html("Delete");
        $("#asyncbox_confirm_cancel").find("span").html("Cancel");
    });
    $("#Create_file").live("click", function () {
        $.open({
            id: 'open', url: '/manage/campaign/FileEdit.aspx', title: 'EditHtml', width: 800, height: 550, modal: false,
            btnsbar: $.btn.OKCANCEL.concat(),
            callback: function (action, iframe) {
                if (action == 'ok') {
                    var getContent = iframe.window.getContent();
                    var getContentTxt = iframe.window.getContentTxt();
                    $.ajax({
                        type: "POST", dataType: "text", url: "/handler/ajax_campaign.aspx",
                        data: { action: "create_html", fileHtml: escape(getContent), fileText: escape(getContentTxt) },
                        success: function (data) {
                            if (data != -1) {
                                var fileName = data;
                                $("#File_E").hide();
                                var SWFUploadHtml = "<div id=\"SWFUpload_1_1\" class=\"progressobj\"><input type=\"button\" class=\"IcoNormal\"><span class=\"ftt\"><a id=\"a_EditSWFUpload_1_1\" accesskey=\"" + fileName + "\" class=\"js_Edit\" fullname=\"" + fileName + "\">Edit</a></span><span class=\"ftt\"><a id=\"a_SWFUpload_1_1\" accesskey=\"" + fileName + "\" class=\"js_delete\" fullname=\"" + fileName + "\">Delete</a></span><span class=\"ftt\"><a id=\"a_SWFUpload_1_1\" href=\"/upload/" + fileName + "\" class=\"js_View\" target=\"_blank\" fullname=\"" + fileName + "\">View</a></span></div>";
                                $("#divprogresscontainer").append(SWFUploadHtml);
                                $("#hdFileNmae").val(fileName);
                            } else {
                                $.alert('Save failed！', 'Save failed');
                                $("#asyncbox_alert_ok").find("span").html("ok");
                            }
                        }
                    });
                }
            }
        });
        $("#open_ok").find("span").html("Save");
        $("#open_cancel").find("span").html("Cancel");
    });
    $(".js_Edit").live("click", function () {
        var obj = $(this).attr("fullname");
        $.open({
            id: 'open', url: '/manage/campaign/FileEdit.aspx?filename=' + obj + '', title: 'EditHtml', width: 800, height: 550, modal: false,
            btnsbar: $.btn.OKCANCEL.concat(),
            callback: function (action, iframe) {
                if (action == 'ok') {
                    var getContent = iframe.window.getContent();
                    var getContentTxt = iframe.window.getContentTxt();
                    //var mailContent = $(iframe.document).find("#ueditor_0").contents().find("body").html();
                    $.ajax({
                        type: "POST", dataType: "text", url: "/handler/ajax_campaign.aspx",
                        data: { action: "add_html", fileName: obj, fileHtml: escape(getContent), fileText: escape(getContentTxt) },
                        success: function (data) {
                            if (data == 1) {
                                $.alert('Successfully saved！', 'Successfully saved');
                                $("#asyncbox_alert_ok").find("span").html("ok");
                            } else if (data == -1) {
                                $.alert('Save failed！', 'Save failed');
                                $("#asyncbox_alert_ok").find("span").html("ok");
                            }
                        }
                    });
                }
            }
        });
        $("#open_ok").find("span").html("Save");
        $("#open_cancel").find("span").html("Cancel");
    });
    $(".js_enter").focus(function () {
        SetKeySave();
    });

    Binding_EventPerson();
    Binding_EventTools();

    // Matrix 关于矩阵的相关操作
    Matrix.SuccessMatrix();
    Matrix.SuccessMatrix.Select();
    Matrix.SuccessMatrix.Default();

    Page.Initialization();
});
function CheckInput() {
    var isPass = true;
    var objFocus = "";
    //=========================================================================================================
    //事件标题
    var strTitle = $.trim($("#txt_title").val());   // 1
    if (0 == strTitle.length) {
        isPass = false;
        objFocus = "txt_title";
        $("#sp_title").show();
    }
    else if (50 < strTitle.length) {
        isPass = false;
        objFocus = "txt_title";
        $("#sp_title").html("Can input 50 words");
        $("#sp_title").show();
    }
    else {
        $("#sp_title").hide();
    }
    //是否分享
    var Share = 0;                                  // 2
    if ($("#input_checkbox_Share").prop("checked")) {
        Share = 1;
    }
    //是否积极
    var Active = 0;                                // 3
    if ($("#input_checkbox_active").prop("checked")) {
        Active = 1;
    }
    //===========================================================================================================
    //事件描述
    var strDesc = $.trim($("#txt_desc").val());   //4
    if (0 == strDesc.length) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "txt_desc";
        $("#sp_desc").show();
    }
    else if (250 < strDesc.length) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "txt_desc";
        $("#sp_desc").html("Can input 250 words");
        $("#sp_desc").show();
    }
    else {
        $("#sp_desc").hide();
    }
    //===========================================================================================================
    //事件类型                                     // 5
    var int_type = $.trim($("#Sales_type_1").attr("value"));
    if ($("#Sales_type_2").prop("checked")) {
        int_type = $.trim($("#Sales_type_2").attr("value"));
    } else if ($("#Sales_type_3").prop("checked")) {
        int_type = $.trim($("#Sales_type_3").attr("value"));
    }
    //===========================================================================================================
    //客户目标
    var strTargeted = $.trim($("#ddl_Targeted").val()); //6
    if ("0" == strTargeted || "Please Select" == strTargeted) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "ddl_Targeted";
        $("#sp_Targeted").show();
    }
    else {
        $("#sp_Targeted").hide();
    }
    //var codeVal = $.trim($("#hid_targeted_val").val());
    //===========================================================================================================
    //活动频率
    var strDTSt = "", strDTEt = "";                 // 7 8 
    strDTSt = $.trim($("#txt_Start_dt").val());
    strDTEt = $.trim($("#txt_End_dt").val());
    if ("" == strDTSt) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "txt_Start_dt";
        $("#sp_type_time").html("Please select a start time");
        $("#sp_type_time").show();
    }
    else if ("" == strDTEt) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "txt_End_dt";
        $("#sp_type_time").html("Please select the expiration time");
        $("#sp_type_time").show();
    }
    else {
        $("#sp_type_time").hide();
    }
    //==========================================================================================================
    //活动方法
    var ckb_methodCount = 0;
    var strMothod = "";                         //9
    $('input[name="ckb_method"]').each(function () {
        if ($(this).prop("checked")) {
            ckb_methodCount += 1;
            strMothod += $(this).attr("value") + ",";
        }
    });
    if (ckb_methodCount == 0) {
        isPass = false;
        if ("" == objFocus) {
            objFocus = "ckb_method_1";
        }
        $("#sp_method").show();
    }
    else {
        $("#sp_method").hide();
    }
    //私人电话
    var intPerson = -1;                       // 10
    $('input[name="radbtn_pmc"]').each(function () {
        if ($(this).prop("checked")) {
            intPerson = $(this).attr("value");
        }
    });
    if ($("#ckb_method_1").prop("checked") && intPerson == -1) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "ckb_method_1";
        $("#sp_rad_person").show();
    }
    else {
        $("#sp_rad_person").hide();
    }
    //宣传方式
    var textMent = -1;                      // 11
    $('input[name="radbtn_Mess"]').each(function () {
        if ($(this).prop("checked")) {
            textMent = $(this).attr("value");
        }
    });
    //========================================================================================================
    //事件题材
    var intdGenre = $.trim($("#ddl_Genre").val());  // 12
    //=======================================================================================================
    //事件保留请求                                     // 13
    var int_RSVP = $.trim($("#RSVP_1").attr("value"));
    if ($("#RSVP_0").prop("checked")) {
        int_RSVP = $.trim($("#RSVP_0").attr("value"));
    }
    //=========================================================================================================
    //事件活动时间
    var strACTSt = "", strACTEt = "";                 // 14 15 
    strACTSt = $.trim($("#EV_Act_S_dt").val());
    strACTEt = $.trim($("#EV_Act_E_dt").val());
    if ("" == strACTSt) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "EV_Act_S_dt";
        $("#sp_Act_times").html("Please select a start time");
        $("#sp_Act_times").show();
    }
    else if ("" == strACTEt) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "EV_Act_E_dt";
        $("#sp_Act_times").html("Please select the expiration time");
        $("#sp_Act_times").show();
    }
    else {
        $("#sp_Act_times").hide();
    }
    //===========================================================================================================
    //相应人员
    var DRP_Person = $("#DRP_Person").combobox('getValues');  // 16
    if ("" == DRP_Person || 0 == DRP_Person.length) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "DRP_Person";
        $("#sp_Person").show();
    }
    //===========================================================================================================
    //推荐工具
    var DRP_Tools = $("#DRP_Tools").combobox('getValues');   // 17
    //===========================================================================================================
    //预算
    var txt_Budget = $.trim($("#txt_Budget").val());  // 18
    //============================================================================================================
    //成功竞选模型
    //var intSucc = $.trim($("#ddl_Succ").val());         // 19
    var intSucc = $("#Succ").combobox('getValues');
    isPass = Matrix.SuccessMatrix.checkValue();
    if (!isPass) { $(".succ_validate").show(); }
    //===========================================================================================================
    //文件名
    var strFileName = $.trim($("#hdFileNmae").val());   //20
    if (2 >= strFileName.length) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "hdFileNmae";
        $("#sp_File").show();
    }
    else {
        $("#sp_File").hide();
    }
    if (3 < objFocus.length)
        $("#" + objFocus).focus();

    $("body").data("event", {
        EV_Title: strTitle,
        EV_Share: Share,
        EV_Active_Tag: Active,
        EV_Desc: strDesc,
        EV_Type: int_type,
        EV_RP_Code: strTargeted,
        EV_Start_dt: strDTSt,
        EV_End_dt: strDTEt,
        EV_Method: strMothod,
        EV_Whom: intPerson,
        EV_Mess_Type: textMent,
        EV_EG_Code: intdGenre,
        EV_RSVP: int_RSVP,
        EV_Act_S_dt: strACTSt,
        EV_Act_E_dt: strACTEt,
        EV_Respnsible: DRP_Person,
        EV_Tools: DRP_Tools,
        EV_Budget: txt_Budget,
        EV_Succ_Matrix: intSucc,
        EV_Filename: strFileName,
    });
    return isPass;
}

function SetKeySave() {
    document.onkeydown = function (event) {
        e = event ? event : (window.event ? window.event : null);
        if (e.keyCode == 13) {
            $("#btnSave").click();
            return false;
        }
    }
}
function Binding_Defalut_ddl_Targeted() {
    var CG_Code = getUrlParam("id");
    $.ajax({
        type: "POST", dataType: 'json', contentType: "application/json; charset=utf-8", url: "Edit_Event.aspx/getReplaceReport",
        data: "{CG_Code:'" + CG_Code + "'}",
        success: function (data) {
            if (data.d != null) {
                var Report = data.d;
                $("#hid_Targeted").val(Report.RP_Code);
                var option = "<option value='" + Report.RP_Code + "'>" + Report.RP_Name_EN + "</option>";
                $("#ddl_Targeted option[value='" + Report.RP_Code + "']").remove();
                $("#ddl_Targeted").append(option);
                $("#ddl_Targeted").prop("value", Report.RP_Code);
            }
        }
    });
}
function Binding_EventPerson() {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8",
        url: "Edit_Event.aspx/getEventPersonList", data: null, async: false,
        success: function (data) {
            if (data.d != null) {
                var EventPerson = data.d.EventPersonList;
                EventPerson = bindPerson(EventPerson);
                $("#DRP_Person").combobox({
                    valueField: 'PEP_Code',
                    textField: 'PEP_Desc_EN',
                    multiple: true,
                    panelHeight: '140',
                    data: EventPerson
                });
            }
        }
    });
}
function bindPerson(EventPerson) {
    var data = "[";
    for (var i = 0; i < EventPerson.length; i++) {
        data += "{'PEP_Code': " + EventPerson[i].PEP_Code + ",'PEP_Desc_EN': '" + EventPerson[i].PEP_Desc_EN + "'},";
    }
    data += "]";
    return eval(data);
}
function Binding_EventTools() {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8",
        url: "Edit_Event.aspx/getEventToolsList", data: null, async: false,
        success: function (data) {
            if (data.d != null) {
                var EventTools = data.d.EventToolsLIst;
                EventTools = bindTools(EventTools);
                $("#DRP_Tools").combobox({
                    valueField: 'PET_Code',
                    textField: 'PET_Desc_EN',
                    multiple: true,
                    panelHeight: '140',
                    data: EventTools
                });
            }
        }
    });
}
function bindTools(EventTools) {
    var data = "[";
    for (var i = 0; i < EventTools.length; i++) {
        data += "{'PET_Code': " + EventTools[i].PET_Code + ",'PET_Desc_EN': '" + EventTools[i].PET_Desc_EN + "'},";
    }
    data += "]";
    return eval(data);
}







function SWFupload() {
    var settings = {
        flash_url: "/scripts/swfupload_en/swfupload.swf",
        upload_url: "/handler/upload.aspx",  //接收上传的服务端url
        file_size_limit: "10 MB", //用户可以选择的文件大小，有效的单位有B、KB、MB、GB，若无单位默认为KB
        file_types: "*.html;*.htm",
        file_types_description: "",
        file_upload_limit: 1,//限制文件上传数量
        file_queue_limit: 0,
        custom_settings: {
            progressTarget: "divprogresscontainer",
            progressGroupTarget: "divprogressGroup",
            //progress object
            container_css: "progressobj",
            icoNormal_css: "IcoNormal",
            icoWaiting_css: "IcoWaiting",
            icoUpload_css: "IcoUpload",
            fname_css: "fle ftt",
            state_div_css: "statebarSmallDiv",
            state_bar_css: "statebar",
            percent_css: "ftt",
            href_delete_css: "ftt",
            //sum object
            s_cnt_progress: "cnt_progress",
            s_cnt_span_text: "fle",
            s_cnt_progress_statebar: "cnt_progress_statebar",
            s_cnt_progress_percent: "cnt_progress_percent",
            s_cnt_progress_uploaded: "cnt_progress_uploaded",
            s_cnt_progress_size: "cnt_progress_size"
        },
        debug: false,
        // Button settings
        button_image_url: "/images/swfupload/upload_file_en.gif", //按钮背景图片路径
        button_width: "106", //按钮宽度
        button_height: "35", //按钮高度
        button_placeholder_id: "spanButtonPlaceHolder",
        //button_disabled: true,
        //button_text: '<span class="theFont">上传文件</span>',////按钮文字
        //button_text_style: ".theFont { font-size: 12;color:#0068B7; }",
        //button_text_left_padding: 12,
        //button_text_top_padding: 3,

        // The event handler functions are defined in handlers.js
        file_queued_handler: fileQueued,
        file_queue_error_handler: fileQueueError,
        upload_start_handler: uploadStart,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: uploadSuccess,
        upload_complete_handler: uploadComplete,
        file_dialog_complete_handler: fileDialogComplete
    };
    swfu = new SWFUpload(settings);
}
function SWFdisabled() {
    var settings = {
        flash_url: "/scripts/swfupload_en/swfupload.swf",
        upload_url: "/handler/upload.aspx",  //接收上传的服务端url
        file_size_limit: "10 MB", //用户可以选择的文件大小，有效的单位有B、KB、MB、GB，若无单位默认为KB
        file_types: "*.html;*.htm",
        file_types_description: "",
        file_upload_limit: 1,//限制文件上传数量
        file_queue_limit: 0,
        custom_settings: {
            progressTarget: "divprogresscontainer",
            progressGroupTarget: "divprogressGroup",
            //progress object
            container_css: "progressobj",
            icoNormal_css: "IcoNormal",
            icoWaiting_css: "IcoWaiting",
            icoUpload_css: "IcoUpload",
            fname_css: "fle ftt",
            state_div_css: "statebarSmallDiv",
            state_bar_css: "statebar",
            percent_css: "ftt",
            href_delete_css: "ftt",
            //sum object
            s_cnt_progress: "cnt_progress",
            s_cnt_span_text: "fle",
            s_cnt_progress_statebar: "cnt_progress_statebar",
            s_cnt_progress_percent: "cnt_progress_percent",
            s_cnt_progress_uploaded: "cnt_progress_uploaded",
            s_cnt_progress_size: "cnt_progress_size"
        },
        debug: false,
        // Button settings
        button_image_url: "/images/swfupload/upload_file_en.gif", //按钮背景图片路径
        button_width: "106", //按钮宽度
        button_height: "35", //按钮高度
        button_placeholder_id: "spanButtonPlaceHolder",
        button_disabled: true,
        //button_text: '<span class="theFont">上传文件</span>',////按钮文字
        //button_text_style: ".theFont { font-size: 12;color:#0068B7; }",
        //button_text_left_padding: 12,
        //button_text_top_padding: 3,
        // The event handler functions are defined in handlers.js
        file_queued_handler: fileQueued,
        file_queue_error_handler: fileQueueError,
        upload_start_handler: uploadStart,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: uploadSuccess,
        upload_complete_handler: uploadComplete,
        file_dialog_complete_handler: fileDialogComplete
    };
    swfu = new SWFUpload(settings);
}
function strConnect(str) {
    var Paramters = "";
    for (var i = 0; i < str.length; i++) {
        if (i == str.length - 1) {
            Paramters += str[i].value;
        } else {
            Paramters += str[i].value + ",";
        }
    }
    return Paramters;
}
