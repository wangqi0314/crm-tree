

$(document).ready(function () {
    ////实例化编辑器
    //var um = UM.getEditor('myEditor');
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
    $("#selecttype").change(function () {
        var aa = $(this).val();
        var b = document.getElementById("selectDealer");
        if (aa == 1 || aa == 2 || aa == 3) {
                b.style.display = "";
            } else {
                b.style.display = "none";
            }
        try {
            $.ajax({
                type: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                url: "edit_User.aspx/getGroupsList",
                data: "{UG_UType:'" + aa + "'}",
                success: function (data) {
                    var JSONObjects = data.d;
                    InsertGroups(JSONObjects.User_Groups_List)
                },
                error: function (err, err2, err3) {
                    $.alert(err3, err2);
                    $("#asyncbox_alert_ok").find("span").html("ok");
                    return false;
                }
            });
        } catch (e) {

        }
    });
    function InsertGroups(data) {
        $("#selectGroup").empty();
        for (var i = 0; i < data.length; i++) {
            jQuery("#selectGroup").append("<option value='" + data[i].UG_Code + "'>" + data[i].UG_Name_EN + "</option>");
        }
    }

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
                            action: strAction, id: intID, strTitle: escape($data.title), strDesc: escape($data.desc), intType: $data.intType, strdtSt: $data.dtSt, strdtEt: $data.dtEt, intType_Time: $data.intType_Time, strMothod: $data.Mothod, intPerson: $data.Person, intTargeted: $data.Targeted, intcodeVal:$data.intcodeVal,strFileNameAll: escape($data.FileNameAll), intSucc: $data.Succ
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

    $("#btnTargeted").click(function () {
        var code = parseInt($.trim($("#ddl_Targeted").val()));
        var codeVal = $.trim($("#ddl_Targeted  option:selected").text());
        if (0 < code) {
            $("#sp_Targeted").hide();
            top.$.prompt('Selected Customers', '', '', 'text', function (action, val) {
                if (action == 'ok') {
                    var reg = new RegExp("^[0-9]*$");
                    if ("" == val) {
                        if ($("#sp_text_tips").length == 0) {
                            $("#asyncbox_prompt_Text").after("<span id=\"sp_text_tips\" style=\"float:left;color:red; padding-left:5px;\">Please enter  Customers Targeted</span>");
                        }
                        else {
                            $("#sp_text_tips").html("Please enter  Customers Targeted");
                        }
                        return false;
                    }
                    else if (!reg.test(val)) {
                        if ($("#sp_text_tips").length == 0) {
                            $("#asyncbox_prompt_Text").after("<span id=\"sp_text_tips\" style=\"float:left;color:red; padding-left:5px;\">Allows only enter numbers</span>");
                        }
                        else {
                            $("#sp_text_tips").html("Allows only enter numbers");
                        }
                        return false;
                    }
                    else if (5 < val.length) {
                        if ($("#sp_text_tips").length == 0) {
                            $("#asyncbox_prompt_Text").after("<span id=\"sp_text_tips\" style=\"float:left;color:red; padding-left:5px;\">A maximum of 5 digits</span>");
                        } else {
                            $("#sp_text_tips").html("A maximum of 5 digits");
                        }
                        return false;
                    }
                    else {
                        var strs = new Array(); //定义一数组
                        strs = codeVal.split(" "); //字符分割  
                        $("#hid_targeted_val").val(parseInt(val));
                        var option = "<option value='" + code + "'>" + strs[0] + " " + parseInt(val) + "</option>";
                        $("#ddl_Targeted option[value='" + code + "']").remove();

                        $("#ddl_Targeted").append(option);
                        $("#ddl_Targeted").prop("value", code);

                        //$.ajax({
                        //    type: "POST",
                        //    dataType: "text",
                        //    url: "/handler/ajax_campaign.aspx",
                        //    data: { action: "add_targeted", targeted: val, intCoede: code },
                        //    success: function (data) {
                        //        var option = "<option value='" + code + "'>" + data + "</option>";
                        //        $("#ddl_Targeted option[value='" + code + "']").remove();

                        //            $("#ddl_Targeted").append(option);
                        //            $("#ddl_Targeted").prop("value", code);
                        //    }, error: function (er) {

                        //    }
                        //});
                    }
                }

            });
            $("#asyncbox_prompt").css("width", "198px");
            $("#asyncbox_prompt_Text").addClass("input_text");
            $("#asyncbox_prompt_ok").find("span").html("Save");
            $("#asyncbox_prompt_cancel").find("span").html("Cancel");
        }
        else {
            $("#sp_Targeted").show();
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
    Dealers();
    bb();

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
        $("#sp_File").show();
    }
    else {
        $("#sp_File").hide();
    }
    if (3 < objFocus.length)
        $("#" + objFocus).focus();

    $("body").data("campaign", {
        title: strTitle,
        desc: strDesc,
        intType:int_type,
        intType_Time: int_type_Time,
        dtSt: strDTSt,
        dtEt: strDTEt,
        Mothod: strMothod,
        Person:intPerson,
        Targeted: strTargeted,
        intcodeVal:codeVal,
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
        if (strs[i] == "1")
        {
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

//汽车年龄下来框
function Dealers() {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "edit_User.aspx/DealerList",
        data: null,
        success: function (data) {
            var JSONObjects = data.d;
            InsertDealer(JSONObjects.User_Dealers_List)
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertDealer(data) {
    jQuery("#selectDealer").append("<option value='0' style='color:#A8A8A8'>Please select</option>");
    var type = getUrlParam("type");
    var html;
    for (var i = 0; i < data.length; i++) {
        html = "<option value='" + data[i].AD_Code + "'";
        if (data[i].AD_Code == type) {
            html = html + " selected='selected'";
        }
        html = html + ">" + data[i].AD_Name_EN + "</option>";
        $("#selectDealer").append(html);
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
    var UserName = $("#txtUserName").val();
    var Password = $("#txtUserPassword").val();
    var Gend = $("#selectGend").val();
    var IndusTry = $("#selectIndusTry").val();
    var Occupation = $("#selectOccupation").val();
    var Education = $("#selectEducation").val();
    var BirthdayData = $("#txtBirthdayData").val();
    var IDCode = $("#txtIDCode").val();
    var IDType = $("#selecttype").val();
    var Deraler = $("#selectDealer").val();
    var Group = $("#selectGroup").val();
    var strAction = "";
 
    if (0 < tid) {
        strAction = "up_User";
        $.ajax({
            type: "get",
            url: "/handler/User/ajax_AddUser.aspx",
            data: { action: strAction, tid: tid, Gend: Gend, IndusTry: IndusTry, Occupation: Occupation, Education: Education, UserName: escape(UserName), BirthdayData: escape(BirthdayData), IDType: escape(IDType), Password: escape(Password), Deraler: Deraler, Group: Group },
            success: function (data) {
                if (0 < data){
                     $.tips("Tips", "success", 5);
                    
                } 
                else if (-1 == data) {
                    top.$.alert('Input is wrong', 'Tips');
                    return false;
                }
                else if (-2 == data) {
                    top.$.alert('Failed', 'Tips');
                    return false;
                }
            }
        });

    } else
    {
        strAction = "add_User";
        $.ajax({
            type: "get",
            url: "/handler/User/ajax_AddUser.aspx",
            data: { action: strAction, tid: tid, Gend: Gend, IndusTry: IndusTry, Occupation: Occupation, Education: Education, UserName: escape(UserName), BirthdayData: escape(BirthdayData), IDType: escape(IDType), Password: escape(Password), Deraler: Deraler, Group: Group },
            success: function (data) {
                if (0 < data) {
                    $.tips("Tips", "success", 3000);
                    if (strAction == "add_User") {
                        window.location.href = "/manage/User/list_User.aspx";
                    }
                    else {
                        setTimeout(function () {
                            $("#btnBack").click();
                        }, 1500);
                    }
                }
                else if (-1 == data) {
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