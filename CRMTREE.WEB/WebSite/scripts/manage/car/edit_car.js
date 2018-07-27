$(document).ready(function () {
    if (0 < parseInt(getUrlParam("id"))) {
        init_page();
    }
    $("#radbtn_type_time_0").click(function () {
        $("#sp_Type_time").show("slow");
    });
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
            $.confirm('This campaign will start a text message campaign with using the </br>script in xxxx file from xxx to xxx for all the dealership customers.</br>  Do you want to continue saving this campaign?', 'Message Confirm ', function (action) {
                //confirm 返回三个 action 值，分别是 'ok'、'cancel' 和 'close'。
                if (action == 'ok') {
                    var $data = $("body").data("campaign");
                    if ($data == null) {
                        CheckInput();
                        var $data = $("body").data("campaign");
                    }
                    var strAction = "add_campaign";
                    var intID = getUrlParam("id");
                    if (0 < parseInt(intID)) {
                        strAction = "up_campaign";
                    }
                    $.ajax({
                        type: "POST",
                        dataType: "text",
                        url: "/handler/ajax_campaign.aspx",
                        data: {
                            action: strAction, id: intID, strTitle: escape($data.title), strDesc: escape($data.desc), intType: $data.intType, strdtSt: $data.dtSt, strdtEt: $data.dtEt, intType_Time: $data.intType_Time, strMothod: $data.Mothod, intPerson: $data.Person, intTargeted: $data.Targeted, intcodeVal: $data.intcodeVal, strFileNameAll: escape($data.FileNameAll), intSucc: $data.Succ
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
                            else {
                                $.tips('success.', 'success');
                                if (strAction == "add_campaign") {
                                    window.location.href = "/manage/campaign/list_campaign.aspx";
                                }
                                else {
                                    setTimeout(function () {
                                        $("#btnBack").click();
                                    }, 1500);
                                }
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
                    url: "/handler/car/ajax_car.aspx",
                    data: { action: "del_file", fullname: objFname },
                    success: function (data) {
                        $("#imgCar").attr("src", "/images/icon/nopic.jpg");
                    }
                });


            }
        });
        $("#asyncbox_confirm_ok").find("span").html("Delete");
        $("#asyncbox_confirm_cancel").find("span").html("Cancel");



    });

    $(".js_enter").focus(function () {
        SetKeySave();
    });

    getMyCarMake();
    var type = getUrlParam("MKID");
    if (type > 0) { GetMycarmode(type); }

    $("#txtMake").change(function () {
        GetMycarmode($(this).val());
    });
    var ss = getUrlParam("MDID");
    if (ss > 0) { GetMycarStyle(ss); }
    $("#txtMode").change(function () {
        GetMycarStyle($(this).val());
    });
    //getMyCarType();
    getMyCarYear();
    getCarColorE();
    getCarColorI();

    var VIN = getUrlParam("VIN");
    var LC = getUrlParam("LC");
    var vin = $("#txt_VIN").val(VIN);
    var mileage = $("#txt_mileage").val(LC);


});

function CheckInput() {

    var isPass = true;
    var objFocus = "";
    var strTitle = $.trim($("#txt_title").val());
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
    var strDesc = $.trim($("#txt_desc").val());
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
    var int_type = $.trim($("#radbtn_type_0").attr("value"));
    if ($("#radbtn_type_1").prop("checked")) {
        int_type = $.trim($("#radbtn_type_1").attr("value"));
    }

    var int_type_Time = $.trim($("#radbtn_type_time_1").attr("value"));
    var strDTSt = "";
    var strDTEt = "";
    if ($("#radbtn_type_time_0").prop("checked")) {
        int_type_Time = $.trim($("#radbtn_type_time_0").attr("value"));
        strDTSt = $.trim($("#txt_Start_dt").val());
        strDTEt = $.trim($("#txt_End_dt").val());
        if ("" == strDTSt) {
            isPass = false;
            if ("" == objFocus)
                objFocus = "radbtn_type_time_0";
            $("#sp_type_time").html("Please select a start time");
            $("#sp_type_time").show();
        }
        else if ("" == strDTEt) {
            isPass = false;
            if ("" == objFocus)
                objFocus = "radbtn_type_time_0";
            $("#sp_type_time").html("Please select the expiration time");
            $("#sp_type_time").show();
        }
        else {
            $("#sp_type_time").hide();
        }
    }

    var ckb_methodCount = 0;
    var strMothod = "";
    $('input[name="ckb_method"]').each(function () {
        if ($(this).prop("checked")) {
            ckb_methodCount += 1;
            strMothod += $(this).attr("value") + ",";
        }
    });
    if (ckb_methodCount == 0) {
        isPass = false;
        if ("" == objFocus) {
            objFocus = "ckb_method_5";
        }

        $("#sp_method").show();
    }
    else {
        $("#sp_method").hide();
    }

    var intPerson = -1;
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
    var strTargeted = $.trim($("#ddl_Targeted").val());
    if ("0" == strTargeted || "Selected Customers" == strTargeted) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "ddl_Targeted";
        $("#sp_Targeted").show();
    }
    else {
        $("#sp_Targeted").hide();
    }

    var codeVal = $.trim($("#hid_targeted_val").val());

    var intSucc = $.trim($("#ddl_Succ").val());

    var strFileName = $.trim($("#hdFileNmae").val());
    if (2 >= strFileName.length) {
        isPass = false;
        if ("" == objFocus)
            objFocus = "hdFileNmae";
        //$("#sp_File").show();
    }
    else {
        $("#sp_File").hide();
    }
    if (3 < objFocus.length)
        $("#" + objFocus).focus();

    $("body").data("campaign", {
        title: strTitle,
        desc: strDesc,
        intType: int_type,
        intType_Time: int_type_Time,
        dtSt: strDTSt,
        dtEt: strDTEt,
        Mothod: strMothod,
        Person: intPerson,
        Targeted: strTargeted,
        intcodeVal: codeVal,
        Succ: intSucc,
        FileNameAll: strFileName
    });
    return isPass;
}

function init_page() {
    var inttype = $.trim($("#hid_type").val());
    $("#radbtn_type_" + inttype).prop("checked", "checked");

    var inttype_time = $.trim($("#hid_type_time").val());
    $("#radbtn_type_time_" + inttype_time).prop("checked", "checked");

    if (inttype_time == "0") {
        $("#sp_Type_time").show();
    }
    else {
        $("#sp_Type_time").hide();
    }

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
    $("#ddl_Targeted").attr("value", $.trim($("#hid_Targeted").val()));
    $("#ddl_Succ").attr("value", $.trim($("#hid_Succ").val()));

    var strFileName = $.trim($("#hdFileNmae").val());
    var strF = new Array(); //定义一数组
    strF = strFileName.split(","); //字符分割     
    for (i = 0; i < strF.length ; i++) {
        var fname = $.trim(strF[i]);
        if (1 < fname.length) {
            var SWFUploadHtml = "<div id=\"SWFUpload_1_" + i + "\" class=\"progressobj\"><input type=\"button\" class=\"IcoNormal\"><span id=\"sp_fname_SWFUpload_1_" + i + "\" class=\"fle ftt\"><a href=\"/upload/" + fname + "\" target=\"_blank\">" + fname + "</a></span><span class=\"ftt\"><a id=\"a_SWFUpload_1_" + i + "\" accesskey=\"" + fname + "\" class=\"js_delete\" fullname=\"" + fname + "\">Delete</a></span></div>";
            $("#divprogresscontainer").append(SWFUploadHtml);
        }
    }
}
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
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
var swfu;
window.onload = function () {
    var settings = {
        flash_url: "/scripts/swfupload/swfupload.swf",
        upload_url: "/handler/upload.aspx",
        file_size_limit: "3 MB",
        file_types: "*",
        file_types_description: "All Files",
        file_upload_limit: 50,//限制文件上传数量
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
            /*
                页面中不应出现以"cnt_"开头声明的元素
            */
            s_cnt_progress: "cnt_progress",
            s_cnt_span_text: "fle",
            s_cnt_progress_statebar: "cnt_progress_statebar",
            s_cnt_progress_percent: "cnt_progress_percent",
            s_cnt_progress_uploaded: "cnt_progress_uploaded",
            s_cnt_progress_size: "cnt_progress_size"
        },
        debug: false,

        // Button settings
        button_image_url: "/images/swfupload/upload_file_en.gif",
        button_width: "106",
        button_height: "35",
        button_placeholder_id: "spanButtonPlaceHolder",
        //button_text: '<span class="theFont">上传文件</span>',
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
};




//Make下拉框数据
function getMyCarMake() {
    var Car = $("#txtMake").val();
    if (Car != null)
    { return false; }
    $("#txtMode").empty();
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "edit_car.aspx/getMyCarMakeList",
        data: null,
        success: function (data) {
            var JSONObjects = data.d;
            InsertCarMake(JSONObjects.Car_Make_List)
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });

}
function InsertCarMake(data) {
    $("#txtMake").append("<option value='0' style='color:#A8A8A8'>Please select</option>");
    var type = getUrlParam("MKID");
    var html;
    for (var i = 0; i < data.length; i++) {
        html = "<option value='" + data[i].MK_Code + "'";
        if (data[i].MK_Code == type) {
            html = html + " selected='selected'";
        }
        html = html + ">" + data[i].MK_Make_EN + "</option>";
        $("#txtMake").append(html);

    }
}
//MODEL 下拉列表数据

function GetMycarmode(dd) {
    var type = getUrlParam("MKID");
    if (dd == type) {
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "edit_car.aspx/getMyCatModeList",
            data: "{MK_code:'" + type + "'}",
            success: function (data) {
                var JSONObjects = data.d;
                InsertCarModel(JSONObjects.Car_Model_List)
            },
            error: function (err, err2, err3) {
                $.alert(err3, err2);
                $("#asyncbox_alert_ok").find("span").html("ok");
                return false;
            }
        });
    } else {
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "edit_car.aspx/getMyCatModeList",
            data: "{MK_code:'" + dd + "'}",
            success: function (data) {
                var JSONObjects = data.d;
                InsertCarModel(JSONObjects.Car_Model_List)
            },
            error: function (err, err2, err3) {
                $.alert(err3, err2);
                $("#asyncbox_alert_ok").find("span").html("ok");
                return false;
            }
        });
    }




}
function InsertCarModel(data) {
    $("#txtMode").empty();
    $("#txtMode").append("<option value='0' style='color:#A8A8A8'> --</option>");
    var type = getUrlParam("MDID");
    var html;
    for (var i = 0; i < data.length; i++) {
        html = "<option value='" + data[i].CM_Code + "'";
        if (data[i].CM_Code == type) {
            html = html + " selected='selected'";
        }
        html = html + ">" + data[i].CM_Model_En + "</option>";
        $("#txtMode").append(html);

    }
}
//Style下拉表
function GetMycarStyle(ss) {
    var type = getUrlParam("MDID");
    if (ss == type) {
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "edit_car.aspx/getMyCatStyleList",
            data: "{CM_code:'" + type + "'}",
            success: function (data) {
                var JSONObjects = data.d;
                InsertCarStyle(JSONObjects.Car_Style_List)
            },
            error: function (err, err2, err3) {
                $.alert(err3, err2);
                $("#asyncbox_alert_ok").find("span").html("ok");
                return false;
            }
        });
    } else {
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "edit_car.aspx/getMyCatStyleList",
            data: "{CM_code:'" + $("#txtMode").val() + "'}",
            success: function (data) {
                var JSONObjects = data.d;
                InsertCarStyle(JSONObjects.Car_Style_List)
            },
            error: function (err, err2, err3) {
                $.alert(err3, err2);
                $("#asyncbox_alert_ok").find("span").html("ok");
                return false;
            }
        });
    }
}
function InsertCarStyle(data) {
    $("#txtStyle").empty();
    $("#txtStyle").append("<option value='0' style='color:#A8A8A8'> --</option>");
    var type = getUrlParam("CSID");
    var html;
    for (var i = 0; i < data.length; i++) {
        html = "<option value='" + data[i].CS_Code + "'";
        if (data[i].CS_Code == type) {
            html = html + " selected='selected'";
        }
        html = html + ">" + data[i].CS_Style_EN + "</option>";
        $("#txtStyle").append(html);

    }
}
//汽车年龄下来框
function getMyCarYear() {
    var Car = $("#txtYears").val();
    if (Car != null)
    { return false; }
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "edit_car.aspx/getMyCatYearsList",
        data: null,
        success: function (data) {
            var JSONObjects = data.d;
            InsertCarYear(JSONObjects.Car_Years)
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertCarYear(data) {
    jQuery("#txtYears").append("<option value='0' style='color:#A8A8A8'>Please select</option>");
    var type = getUrlParam("YEID");
    var html;
    for (var i = 0; i < data.length; i++) {
        html = "<option value='" + data[i].YR_Code + "'";
        if (data[i].YR_Code == type) {
            html = html + " selected='selected'";
        }
        html = html + ">" + data[i].YR_Year + "</option>";
        $("#txtYears").append(html);
    }
}
////汽车颜色E
function getCarColorE() {
    var Car = $("#colorE").val();
    if (Car != null)
    { return false; }
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "edit_car.aspx/getMyCatColorsList",
        data: null,
        success: function (data) {
            var JSONObjects = data.d;
            InsertCarColorE(JSONObjects.Car_Color)
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertCarColorE(data) {
    jQuery("#txtCOLOR_E").append("<option value='0' style='color:#A8A8A8'>Please select</option>");
    var type = getUrlParam("ColorE");
    var html;
    for (var i = 0; i < data.length; i++) {
        html = "<option value='" + data[i].CL_Code + "'";
        if (data[i].CL_Code == type) {
            html = html + " selected='selected'";
        }
        html = html + ">" + data[i].CL_Color_EN + "</option>";
        $("#txtCOLOR_E").append(html);
    }
}
//汽车颜色I
function getCarColorI() {
    var Car = $("#txtCOLOR_I").val();
    if (Car != null)
    { return false; }
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "edit_car.aspx/getMyCatColorsList",
        data: null,
        success: function (data) {
            var JSONObjects = data.d;
            InsertCarColorI(JSONObjects.Car_Color)
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertCarColorI(data) {
    jQuery("#txtCOLOR_I").append("<option value='0' style='color:#A8A8A8'>Please select</option>");
    var type = getUrlParam("ColorI");
    var html;
    for (var i = 0; i < data.length; i++) {
        html = "<option value='" + data[i].CL_Code + "'";
        if (data[i].CL_Code == type) {
            html = html + " selected='selected'";
        }
        html = html + ">" + data[i].CL_Color_EN + "</option>";
        $("#txtCOLOR_I").append(html);
    }
}
//获取地址值
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function btnSubmit() {
    var tid = getUrlParam("id");
    var hdFileNmae = $("#hdFileNmae").val();
    var Makeid = $("#txtMake").val();
    var Modeid = $("#txtMode").val();
    var Styleid = $("#txtStyle").val();
    var typeid = $("#txttype").val();
    var Yearsid = $("#txtYears").val();
    var COLOR_Eid = $("#txtCOLOR_E").val();
    var COLOR_Iid = $("#txtCOLOR_I").val();
    var vin = $("#txt_VIN").val();
    var Lic = $("#txt_Lic").val();
    var mileage = $("#txt_mileage").val();
    var strAction = "";
    if (0 < tid) {
        strAction = "up_car";
        $.ajax({
            type: "get",
            url: "/handler/car/ajax_car.aspx",
            //var Lic = $("#txt_Lic").val();
            data: { action: strAction, tid: tid, Makeid: Makeid, Modeid: Modeid, Styleid: Styleid, typeid: typeid, Yearsid: Yearsid, COLOR_Eid: COLOR_Eid, COLOR_Iid: COLOR_Iid, vin: escape(vin), Lic: escape(Lic), mileage: mileage },
            success: function (data) {
                if (0 < data) {
                    $.tips("Tips", "success", 5);
                    if (strAction == "up_car") {
                        window.location.href = "/manage/car/list_car.aspx";
                    }
                }
                else if (-1 == data) {
                    top.$.alert('Input is wrong', 'Tips');
                    return false;
                }
                else if (-2 == data) {
                    top.$.alert('Failed', 'Tips');
                    return false;
                } else if (-3 == data) {
                    top.$.alert('Perform error', 'Tips');
                    return false;
                }
            }
        });

    } else {
        strAction = "add_car";
        $.ajax({
            type: "get",
            url: "/handler/car/ajax_car.aspx",
            data: { action: strAction, Makeid: Makeid, Modeid: Modeid, Styleid: Styleid, typeid: typeid, Yearsid: Yearsid, COLOR_Eid: COLOR_Eid, COLOR_Iid: COLOR_Iid, vin: escape(vin), Lic: escape(Lic), mileage: mileage, hdFileNmae: escape(hdFileNmae) },
            success: function (data) {
                if (0 < data) {
                    $.tips("Tips", "success", 3000);
                    if (strAction == "add_car") {
                        window.location.href = "/manage/car/list_car.aspx";
                    }
                    else {
 //                       alert("2");
                        setTimeout(function () {
                            $("#btnBack").click();
                        }, 1500);
                    }
                }
                else if (-1 == data) {
 //                   alert("3");
                    top.$.alert('Input is wrong', 'Tips');
                    return false;
                }
                else if (-2 == data) {
                    top.$.alert('Failed', 'Tips');
                    return false;

                }
            }

        });
    }


}