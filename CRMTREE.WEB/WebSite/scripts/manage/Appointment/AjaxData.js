$(document).ready(function () {
    var Car_Id = GetQueryString("Carid");  //如果是编辑时可以获取CarID
    var Ad_Code = GetQueryString("ADcode"); //如果是编辑时可以获取经销商ID
    var Ad_Name = GetQueryString("ADname");  //如果是编辑时可以获取经销商Name
    //设置预约的默认Car和默认的经销商
    if (Car_Id != null && Car_Id != "" && Ad_Code != null && Ad_Code != "") {
        setCar(Car_Id, Ad_Code, Ad_Name);
    }
    else {
        setDefault();
    }
    /*设置预约时间的默认值*/
    $("#myDateTime").val(getDate(0, 1));
    $("#myTime").val(getTime());

    //提交预约信息
    $("#BtnApp").click(function () {
        AppCheck();
    });

    //对于选择个人汽车信息的操作
    $("#MyCarInfoId").click(function (event) {
        event.stopPropagation();
        $("#NUI_msgbox").toggle();
    });
    //当选择了汽车时的操作
    $(".CarInfo").click(function () {
        $("#NUI_msgbox").hide();
        $("#MyCarInfoId").val($(this).find(".Car_Model").html());
        $("#hidMyCarInfoId").val($(this).find(".hidCarCodeID").val());
    });

    //对于选择经销商的操作
    $("#MyDealerId").click(function (event) {
        event.stopPropagation();
        getDealerList(false);
        $("#NUI_msgbox_MyDealerId").toggle();
    });
    //当选择了经销商时需要做的事情；
    $(".DealerInfo").live("click", function () {
        $("#NUI_msgbox_MyDealerId").hide(); //隐藏经销商列表
        $("#MyDealerId").val($(this).find(".Dealer_Name").html()); //经销商ID
        $("#hidMyDealerId").val($(this).find(".hidID").html()); //经销商名称
        $("#hidSA_Selection").val($(this).find(".hideSA_Selection").html()); //表示是否可以选择顾问
        $("#hidServ_Package").val($(this).find(".hideServ_Package").html()); //表示是否可以选择服务列别 Package
        //操作是否可以现在顾问提供选择
        if ($("#hidSA_Selection").val() == 'true') {
            $(".Appointment_info_1:eq(2)").show();
        } else {
            $("#MyAdviserId").val("");
            $("#hidMyAdviserId").val("");
            $("#NUI_msgbox_MyAdviserId").empty();
            $(".Appointment_info_1:eq(2)").hide();
        }
    });
    
    //预约类型选择其他时，设置交通的默认值
    $("#MyTypeOther").click(function () {
        $("#MyTransportationid").val("No Need");
        $("#hidMyTransportationid").val("1");
    });

    //当点击选择顾问时的操作
    $("#MyAdviserId").click(function (event) {
        event.stopPropagation();
        getAdviserList(false);
        $("#NUI_msgbox_MyAdviserId").toggle();
    });
    //当点击选择了顾问时的操作
    $(".AdviserInfo").live("click", function () {
        $("#NUI_msgbox_MyAdviserId").hide();
        $("#MyAdviserId").val($(this).find(".Adviser_Name").html());
        $("#hidMyAdviserId").val($(this).find(".hidAdviserID").html());
        //设置默认的Peason
        $("#MyTypeId").val("Scheduled Maintenance");
        $("#hidMyTypeId").val("1");
        $("#MyTypeInfoId").val("Best Package");
        $("#hidMyTypeInfoId").val("7");
    });

    // 预约的类别
    $("#MyTypeId").click(function (event) {
        event.stopPropagation();
        getServCateList();
        $("#NUI_msgbox_MyTypeId").toggle();
    });
    //预约类别选择后
    $(".TypeInfo").live("click", function () {
        $("#NUI_msgbox_MyTypeId").hide();
        $("#MyTypeId").val($(this).find(".Type_Model").html());
        $("#hidMyTypeId").val($(this).find(".hidTypeID").html());
        if ($("#hidMyTypeId").val() == "0") {
            $(".Appointment_Table_4").hide();
            $(".Appointment_Table_5").show();
        } else if ($("#hidMyTypeId").val() == "2") {
            $(".Appointment_Table_4").hide();
            $(".Appointment_Table_5").hide();
        } else {
            $(".Appointment_Table_4").show();
            $(".Appointment_Table_5").hide();
        }
    });
    //当选择预约类型时
    $("#MyTypeInfoId").live("click", function (event) {
        event.stopPropagation();
        if ($("#hidMyTypeId").val() == "1") {
            getMaintenance_Pack();
        }
        else {
            getServiceInfo();
        }
        $("#NUI_msgbox_MyTypeInfoId").toggle();
    });
    //当选择的是保养维护时
    $(".TypeInfoCon").live("click", function () {
        $("#NUI_msgbox_MyTypeInfoId").hide();
        $("#MyTypeInfoId").val($(this).find(".TypeInfo_Model").html());
        $("#hidMyTypeInfoId").val($(this).find(".hidTypeInfoID").html());
        if ($("#hidMyTypeInfoId").val() == "0") {
            $(".Appointment_Table_5").show();
        } else {
            $(".Appointment_Table_5").hide();
        }
    });
    //当选择的是维修类别时
    $(".TypeInfoCon").live("click", function () {
        $("#NUI_msgbox_MyTypeInfoId").hide();
        $("#MyTypeInfoId").val($(this).find(".TypeInfo_Model").html());
        $("#hidMyTypeInfoId").val($(this).find(".hidTypeInfoID").html());
        if ($("#hidMyTypeInfoId").val() == "0") {
            $(".Appointment_Table_5").show();
        }
        else {
            $(".Appointment_Table_5").hide();
        }
    });
    /* Transportation */
    $("#MyTransportationid").click(function (event) {
        event.stopPropagation();
        $("#NUI_msgbox_MyTransportationid").toggle();
    });
    $(".MyTransportationInfo").click(function () {
        $("#NUI_msgbox_MyTransportationid").hide();
        $("#MyTransportationid").val($(this).find(".selectTransportation").html());
    });
    /* Transportation */
    //控制事件的外部区域
    $(document).click(function (event) {
        var eo = $(event.target);
        if ($("#MyCarInfoId").is(":visible") && eo.attr("id") != "NUI_msgbox" && !eo.parent("#NUI_msgbox").length)
            $('#NUI_msgbox').hide();
        if ($("#MyDealerId").is(":visible") && eo.attr("id") != "NUI_msgbox_MyDealerId" && !eo.parent("#NUI_msgbox_MyDealerId").length)
            $('#NUI_msgbox_MyDealerId').hide();
        if ($("#MyAdviserId").is(":visible") && eo.attr("id") != "NUI_msgbox_MyAdviserId" && !eo.parent("#NUI_msgbox_MyAdviserId").length)
            $('#NUI_msgbox_MyAdviserId').hide();
        if ($("#MyTypeId").is(":visible") && eo.attr("id") != "NUI_msgbox_MyTypeId" && !eo.parent("#NUI_msgbox_MyTypeId").length)
            $('#NUI_msgbox_MyTypeId').hide();
        if ($("#MyTypeInfoId").is(":visible") && eo.attr("id") != "NUI_msgbox_MyTypeInfoId" && !eo.parent("#NUI_msgbox_MyTypeInfoId").length)
            $('#NUI_msgbox_MyTypeInfoId').hide();
        if ($("#MyTransportationid").is(":visible") && eo.attr("id") != "NUI_msgbox_MyTransportationid" && !eo.parent("#NUI_msgbox_MyTransportationid").length)
            $('#NUI_msgbox_MyTransportationid').hide();
    });
});
function setCar(Id, Ad_Code, Ad_Name) {
    $("#MyCarInfoId").val($("input[value='" + Id + "']").prev(".Car_Model").html());
    $("#hidMyCarInfoId").val(Id);
    if (Ad_Code != null && Ad_Code != "") {
        $("#MyDealerId").val(Ad_Name);
        $("#hidMyDealerId").val(Ad_Code);
        getDefaultRecomAdviser(Id, Ad_Code);
        $("#MyTypeId").val("Scheduled Maintenance");
        $("#hidMyTypeId").val("1");
        $("#MyTypeInfoId").val("Best Package");
        $("#hidMyTypeInfoId").val("7");
        $("#MyTransportationid").val("No Need");
        $("#hidMyTransportationid").val("1");
    }
}
function setDefault() {
    $("#MyCarInfoId").val($(".CarInfo .Car_Model").html());
    $("#hidMyCarInfoId").val($(".CarInfo .hidCarCodeID").val());
    getDealerList(true);
    $("#MyTypeId").val("Scheduled Maintenance");
    $("#hidMyTypeId").val("1");
    $("#MyTypeInfoId").val("Best Package");
    $("#hidMyTypeInfoId").val("7");
    $("#MyTransportationid").val("No Need");
    $("#hidMyTransportationid").val("1");
}

function SubAppDate() {
    var MyCarInfoId = $("#hidMyCarInfoId").val();
    var MyDealerId = $("#hidMyDealerId").val();
    var myDateTime = $("#myDateTime").val();
    var myTime = $("#myTime").val();
    var MyAdviserId = $("#hidMyAdviserId").val();
    var MyTypeId = $("#hidMyTypeInfoId").val();
    var MyTransportationid = $("#hidMyTransportationid").val();
    var Datas = "{'Data':'"
          + MyCarInfoId + ","
          + MyDealerId + ","
          + myDateTime + ","
          + myTime + ","
          + MyAdviserId + ","
          + MyTypeId + ","
          + MyTransportationid + "','type':'"
          + $("#hidMyTypeId").val()
          + "'}";
    //$.alert("biaoti", Datas);
    //$("#asyncbox_alert_ok").find("span").html("ok");
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "Appointment.aspx/AddApp",
        data: Datas,
        success: function (data) {
            var JSONObjects = data.d;
            $.alert("Success !", "Prompt");
            $("#asyncbox_alert_ok").find("span").html("ok");
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function AppCheck() {
    var MyCarInfoId = $("#hidMyCarInfoId").val();
    var MyDealerId = $("#hidMyDealerId").val();
    var myDateTime = $("#myDateTime").val();
    var myTime = $("#myTime").val();
    var MyAdviserId = $("#hidMyAdviserId").val();
    var MyTypeId = $("#hidMyTypeInfoId").val();
    var MyTransportationid = $("#hidMyTransportationid").val();
    if (MyCarInfoId == null || MyCarInfoId == "") {
        $.alert("Please Select Car !", "Prompt");
        return;
    }
    if (MyDealerId == null || MyDealerId == "") {
        $.alert("Please Select Dealer !", "Prompt");
        return;
    }
    if (myDateTime == null || myDateTime == "") {
        $.alert("Please Select Dater !", "Prompt");
        return;
    }
    if (myTime == null || myTime == "") {
        $.alert("Please Select Time !", "Prompt");
        return;
    }
    if ($("#hidSA_Selection").val() == "true") {
        if (MyAdviserId == null || MyAdviserId == "") {
            $.alert("Please Select Adviser !", "Prompt");
            return;
        }
    }
    SubAppDate();
}

function getCarStyle() {
    var Car = $("#MyCarInfoId").val();
    if (Car != null)
    { return false; }
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "Appointment.aspx/MyCarStyke",
        data: "{CarId:'dd'}",
        success: function (data) {
            if (data.d != null) {
                InsertCar(JSONObjects.Appointmens_List);
            }
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertCar(data) {
    jQuery("#MyCarInfoId").append("<option value='0'></option>");
    for (var i = 0; i < data.length; i++) {
        jQuery("#MyCarInfoId").append("<option value='" + data[i].CI_Code + "'>" + data[i].CS_Style_EN + "</option>");
    }
}

function getDealerList(defaults) {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "Appointment.aspx/MyDealerList",
        data: "{AU_Code:'dd'}",
        success: function (data) {
            if (data.d != null) {
                if (defaults) {
                    $("#MyDealerId").val(data.d.Dealers_List[0].AD_Name_EN);
                    $("#hidMyDealerId").val(data.d.Dealers_List[0].AD_Code);
                    $("#hidSA_Selection").val(data.d.Dealers_List[0].SD_SA_Selection);
                    $("#hidServ_Package").val(data.d.Dealers_List[0].SD_Serv_Package);
                    getAdviserList(true); //设置默认的顾问
                } else {
                    InsertDealerList(data.d.Dealers_List);
                    getAdviserList(true); //设置默认的顾问
                }
            }
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertDealerList(data) {
    $("#NUI_msgbox_MyDealerId").empty();
    for (var i = 0; i < data.length; i++) {
        $("#NUI_msgbox_MyDealerId").append(myDeas(data[i].AD_logo_file_S, data[i].AD_Name_EN, data[i].AD_Code, data[i].SD_SA_Selection, data[i].SD_Serv_Package));
    }
}
function myDeas(img, name, id, SA, Serv) {
    return "<div class='DealerInfo' style='float: left; margin-left:10px;margin-top:2px; width: 93px;border: 1px solid #d2d2d2; cursor: pointer;'>  <div> <img style='position: relative; top: 2px; width: 91px;padding: 0px; margin: 0px' src='/images/DealersLogo/" + img + "' /> </div> <div class='Dealer_Name'>" + name + "</div><div class='hidID' style='display:none'>" + id + "</div><div class='hideSA_Selection' style='display:none'>" + SA + "</div><div class='hideServ_Package' style='display:none'>" + Serv + "</div> </div>";
}


function getAdviserList(defaults) {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "Appointment.aspx/MyAdviserList",
        data: "{AD_code:'" + $("#hidMyDealerId").val() + "'}",
        success: function (data) {
            if (data.d != null) {
                if (defaults) {
                    $("#MyAdviserId").val(data.d.Adviser_List[0].AU_Name);
                    $("#hidMyAdviserId").val(data.d.Adviser_List[0].DE_Code);
                } else {
                    InsertAdviserList(data.d.Adviser_List);
                }
            }
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertAdviserList(data) {
    $("#NUI_msgbox_MyAdviserId").empty();
    for (var i = 0; i < data.length; i++) {
        if (data[i].DE_Picture_FN == null || data[i].DE_Picture_FN == "") {
            data[i].DE_Picture_FN = "defaultAvatar.gif";
        }
        $("#NUI_msgbox_MyAdviserId").append(myAdviserlist(data[i].DE_Picture_FN, data[i].AU_Name, data[i].DE_Code));
    }
}
function myAdviserlist(img, name, id) {
    return "<div class='AdviserInfo' style='float: left; margin-left:10px;width:93px;border:1px solid #d2d2d2; cursor: pointer;'><div><img style='position:relative;top:2px;width:91px;padding: 0px; margin: 0px' src='/images/Adviser/" + img + "' /></div> <div class='Adviser_Name'>" + name + "</div><div class='hidAdviserID' style='display:none'>" + id + "</div></div>";
}

//对于预约类别的操作
function getServCateList() {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "Appointment.aspx/getServCateList",
        data: null,
        success: function (data) {
            if (data.d != null) {
                InsertServCateList(data.d.Serv_Category_List);
            }
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertServCateList(data) {
    $("#NUI_msgbox_MyTypeId").empty();
    for (var i = 0; i < data.length; i++) {
        $("#NUI_msgbox_MyTypeId").append(myServCatelist(data[i].SC_Desc_EN, data[i].SC_Code));
    }
    $("#NUI_msgbox_MyTypeId").append(myServCatelist("Others...", 0));
}
function myServCatelist(name, id) {
    return "<div class='TypeInfo' style='margin-left:1px;border-bottom:1px solid #d2d2d2;cursor:pointer;text-align:left;'><div class='Type_Model'>" + name + "</div><div class='hidTypeID' style='display:none'>" + id + "</div></div>";
}

function getServiceInfo() {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "Appointment.aspx/getServiceType",
        data: "{'SC_Code':'" + $("#hidMyTypeId").val() + "','AD_Code':'" + $("#hidMyDealerId").val() + "'}",
        success: function (data) {
            if (data.d != null) {
                InsertServiceInfoList(data.d.Service_Types_List);
            }
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertServiceInfoList(data) {
    $("#MyTypeInfoId").val("");
    $("#NUI_msgbox_MyTypeInfoId").empty();
    for (var i = 0; i < data.length; i++) {
        $("#NUI_msgbox_MyTypeInfoId").append(myServiceInlist(data[i].ST_Desc_EN, data[i].ST_Code));
    }
    $("#NUI_msgbox_MyTypeInfoId").append(myServiceInlist("Others...", 0));
}
function myServiceInlist(name, id) {
    return "<div class='TypeInfoCon' style='margin-left:4px;border-bottom:1px solid #d2d2d2;cursor: pointer;text-align:left;'><div class='TypeInfo_Model'>" + name + "</div><div class='hidTypeInfoID' style='display:none'>" + id + "</div></div>";
}

function getMaintenance_Pack() {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "Appointment.aspx/getMaintenancePack",
        data: "{'AD_Code':'" + $("#hidMyDealerId").val() + "'}",
        success: function (data) {
            if (data.d != null) {
                InsertMaintenancenfoList(data.d.Maintenance_Pack_List);
            }
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertMaintenancenfoList(data) {
    $("#MyTypeInfoId").val("");
    $("#NUI_msgbox_MyTypeInfoId").empty();
    for (var i = 0; i < data.length; i++) {
        $("#NUI_msgbox_MyTypeInfoId").append(myServiceInlist(data[i].MP_Desc_EN, data[i].MP_Code));
    }
   
}
//根据参数获取Id的value
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
}
//获取日期
function getDate(month, day) {
    var d = new Date();
    var vYear = d.getFullYear();
    var vMon = d.getMonth() + 1 - month;
    var vDay = d.getDate() + day;
    var h = d.getHours();
    var m = d.getMinutes();
    var se = d.getSeconds();
    if (vMon == 0) {
        vMon = 12;
        vYear = vYear - 1;
    }
    s = vYear + "-" + (vMon < 10 ? "0" + vMon : vMon) + "-" + (vDay < 10 ? "0" + vDay : vDay);
    return s;
}
//获取时间
function getTime() {
    var d = new Date();
    var h = d.getHours();
    var m = d.getMinutes();
    if (m % 15 != 0) {
        var Ml = m % 15;
        m = m - Ml + 15
        if (m = 60) {
            m = "00";
            h = h + 1;
        }
    }
    s = h + ":" + m;
    return s;
}
//获取默认推荐的顾问
function getDefaultRecomAdviser(Car_Id, Ad_Code) {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "Appointment.aspx/getDefaultRecomAdviser",
        data: "{CI_Code:'" + Car_Id + "',AD_Code:'" + Ad_Code + "'}",
        success: function (data) {
            if (data.d != null) {
                $("#MyAdviserId").val(data.d.AU_Name);
                $("#hidMyAdviserId").val(data.d.DE_Code);
            }
        },
        error: function (err, err2, err3) {
            alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}