$(document).ready(function () {
    
    $(".ServiceInfo").click(function () {
        $(this).next(".ServiceInfoCon").slideToggle();
    });
    $(".ServiceInfoCon").click(function () {
        $(this).slideToggle();
    });
    $("#BeginDateTime").val(getDate(6,0));
    $("#EndDateTime").val(getDate(0, 0));
    getServiceInfoSearch();
    $(".Btn_ServerHistory").click(function () {
        getServiceInfoSearch();
    });
});
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
}
Date.prototype.format = function (format) {
    var o =
    {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
        "S": this.getMilliseconds() //millisecond 
    }
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
function formatJsonDateTime(val, formatType) {
    var re = /-?\d+/;
    var m = re.exec(val);
    var d = new Date(parseInt(m[0]));
    // 按【2012-02-13 09:09:09】的格式返回日期
    return d.format(formatType);
}
function getServiceInfo()
{
    var Id = GetQueryString("id");
    //var Id = "227";
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "ServerHistory.aspx/getMyServiceHis",
        data: "{CI_Code:'" + Id + "'}",
        success: function (data) {
            var JSONObjects = data.d;
            if (JSONObjects != null) {
                InsertServiceInfoList(JSONObjects.History_Service);
            }else{         
                $(".Service").empty();
                $(".Service").append("There is no historical data!");
            }
        },
        error: function (err, err2, err3) {
            alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function getServiceInfoSearch() {
    var Id = GetQueryString("id");
    var CarInfo = GetQueryString("CarInfo");
    $(".CarInfo").empty();
    $(".CarInfo").html(CarInfo);

    var BeginDate = $("#BeginDateTime").val();
    var endDate = $("#EndDateTime").val();
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "ServerHistory.aspx/getMyServiceHisSearch",
        data: "{CI_Code:'" + Id + "',BeginDate:'" + BeginDate + "',EndDate:'" + endDate + "'}",
        success: function (data) {
            var JSONObjects = data.d;
            if (JSONObjects != null) {
                InsertServiceInfoList(JSONObjects.History_Service);
            }
            else {
                $(".Service").empty();
                $(".Service").append("There is no Historyed data form " + BeginDate + " to " + endDate + "!");
            }
        },
        error: function (err, err2, err3) {
            alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function InsertServiceInfoList(data) {
    $(".Service").empty();
    for (var i = 1; i <= data.length; i++) {
        if(i%2 == 0 ){
            $(".Service").append(myService_D(data[i - 1]));
        }else{
            $(".Service").append(myService_S(data[i - 1]));
        }
    }
    $(".ServiceInfo").click(function () {
        $(this).next(".ServiceInfoCon").slideToggle();
    });
    $(".ServiceInfoCon").click(function () {
        $(this).slideToggle();
    });
}
function myService_S(Date)
{
    return " <div class='ServiceInfo_single ServiceInfo'><div><b>Date:</b><span>" + formatJsonDateTime(Date.HS_RO_Close, "MM/dd/yyyy") + "</span> &nbsp; &nbsp;<b>Dealer:</b><span>" + Date.AD_Name_EN + "</span><span style='background-color:AppWorkspace; margin-left:20px;border-radius: 2px 2px 2px 2px;padding:2px;'>Details</span></div></div><div class='ServiceInfoCon'><div><b>Service Adviser:</b><span>" + Date.AU_Name + "</span> &nbsp; &nbsp;<b>Repair Orders:</b><span>" + Date.HS_RO_No + "</span></div><div><b>Total Amount:</b><span>" + Date.HS_RO_Amount + "</span> &nbsp; <b>paid:</b><span>" + Date.HS_CustPay + "</span>&nbsp; <b>Points Used:</b><span>" + Date.HS_PointsUsed + "</span></div><div style='border-bottom: 1px solid #d2d2d2; margin: 5px 20px;'>" + Date.SC_Desc_CN + "</div></div>";
}
function myService_D(Date)
{
    return " <div class='ServiceInfo_double ServiceInfo'><div><b>Date:</b><span>" + formatJsonDateTime(Date.HS_RO_Close, "MM/dd/yyyy") + "</span> &nbsp; &nbsp;<b>Dealer:</b><span>" + Date.AD_Name_EN + "</span><span style='background-color:AppWorkspace; margin-left:20px;border-radius: 2px 2px 2px 2px;padding:2px;'>Details</span></div></div><div class='ServiceInfoCon'><div><b>Service Adviser:</b><span>" + Date.AU_Name + "</span> &nbsp; &nbsp;<b>Repair Orders:</b><span>" + Date.HS_RO_No + "</span></div><div><b>Total Amount:</b><span>" + Date.HS_RO_Amount + "</span> &nbsp; <b>paid:</b><span>" + Date.HS_CustPay + "</span>&nbsp; <b>Points Used:</b><span>" + Date.HS_PointsUsed + "</span></div><div style='border-bottom: 1px solid #d2d2d2; margin: 5px 20px;'>" + Date.SC_Desc_CN + "</div></div>";
}

function getDate(month,day){
    var d = new Date();
    var vYear = d.getFullYear();
    var vMon = d.getMonth() + 1 - month;
    var vDay = d.getDate() - day;
    var h = d.getHours();
    var m = d.getMinutes();
    var se = d.getSeconds();
    if (vMon == 0) {
        vMon = 12;
        vYear = vYear - 1;
    }
    s = (vMon < 10 ? "0" + vMon : vMon) +"/"+ (vDay < 10 ? "0" + vDay : vDay)+"/"+vYear;
    return s;
}