$(document).ready(function () {
    tick();
});
//获取当前日期
function GetDate() {
    var objD = new Date();
    var str;
    var yy = objD.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = objD.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = objD.getDate();
    if (dd < 10) dd = '0' + dd;
    var hh = objD.getHours();
    if (hh < 10) hh = '0' + hh;
    var mm = objD.getMinutes();
    if (mm < 10) mm = '0' + mm;
    var ss = objD.getSeconds();
    if (ss < 10) ss = '0' + ss;
    str = yy + "-" + MM + "-" + dd + " " + hh + ":" + mm + ":" + ss;
    return (str);
}
//时间差
function dateDiff(interval, date1, date2) {
    var objInterval = { 'D': 1000 * 60 * 60 * 24, 'H': 1000 * 60 * 60, 'M': 1000 * 60, 'S': 1000, 'T': 1 };
    interval = interval.toUpperCase();
    var dt1 = Date.parse(date1.replace(/-/g, '/'));
    var dt2 = Date.parse(date2.replace(/-/g, '/'));
    try {
        return Math.round((dt2 - dt1) / eval('(objInterval.' + interval + ')'));
    }
    catch (e) {
        return e.message;
    }
}
//时间和
function DateAdd(interval,number,date){
	switch(interval.toLowerCase()){
		case "y": return new Date(date.setFullYear(date.getFullYear()+number));
		case "m": return new Date(date.setMonth(date.getMonth()+number));
		case "d": return new Date(date.setDate(date.getDate()+number));
		case "w": return new Date(date.setDate(date.getDate()+7*number));
		case "h": return new Date(date.setHours(date.getHours()+number));
		case "n": return new Date(date.setMinutes(date.getMinutes()+number));
		case "s": return new Date(date.setSeconds(date.getSeconds()+number));
		case "l": return new Date(date.setMilliseconds(date.getMilliseconds()+number));
	}
}
//是否是日期格式
function IsDate(dateval){
	var arr = new Array();
	
	if(dateval.indexOf("-") != -1){
		arr = dateval.toString().split("-");
	}else if(dateval.indexOf("/") != -1){
		arr = dateval.toString().split("/");
	}else{
		return false;
	}	
	//yyyy-mm-dd || yyyy/mm/dd
	if(arr[0].length==4){
		var date = new Date(arr[0],arr[1]-1,arr[2]);
		if(date.getFullYear()==arr[0] && date.getMonth()==arr[1]-1 && date.getDate()==arr[2]){
			return true;
		}
	}
	//dd-mm-yyyy || dd/mm/yyyy
	if(arr[2].length==4){
		var date = new Date(arr[2],arr[1]-1,arr[0]);
		if(date.getFullYear()==arr[2] && date.getMonth()==arr[1]-1 && date.getDate()==arr[0]){
			return true;
		}
	}
	//mm-dd-yyyy || mm/dd/yyyy
	if(arr[2].length==4){
		var date = new Date(arr[2],arr[0]-1,arr[1]);
		if(date.getFullYear()==arr[2] && date.getMonth()==arr[0]-1 && date.getDate()==arr[1]){
			return true;
		}
	}	
	return false;
}
//动态显示当前时间
function showLocale(objD) {//
    var str, colorhead, colorfoot;
    var yy = objD.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = objD.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = objD.getDate();
    if (dd < 10) dd = '0' + dd;
    var hh = objD.getHours();
    if (hh < 10) hh = '0' + hh;
    var mm = objD.getMinutes();
    if (mm < 10) mm = '0' + mm;
    var ss = objD.getSeconds();
    if (ss < 10) ss = '0' + ss;
    var ww = objD.getDay();
    if (ww == 0) colorhead = "<font color=\"#FFFFFF\">";
    if (ww > 0 && ww < 6) colorhead = "<font color=\"#FFFFFF\">";
    if (ww == 6) colorhead = "<font color=\"#FFFFFF\">";
    if (ww == 0) ww = "星期日";
    if (ww == 1) ww = "星期一";
    if (ww == 2) ww = "星期二";
    if (ww == 3) ww = "星期三";
    if (ww == 4) ww = "星期四";
    if (ww == 5) ww = "星期五";
    if (ww == 6) ww = "星期六";
    colorfoot = "</font>"
    str = colorhead + yy + "-" + MM + "-" + dd + " " + hh + ":" + mm + ":" + ss + "  " + ww + colorfoot;
    return (str);
}
function tick() {
    var today;
    today = new Date();
    $("#liTimeNow").html(showLocale(today));
    window.setTimeout("tick()", 1000);
}
/**
* 时间对象的格式化;
*/
Date.prototype.format = function (format) {
    /*
    * eg:format="YYYY-MM-dd hh:mm:ss";
    */
    var o = {
        "M+": this.getMonth() + 1,  //month
        "d+": this.getDate(),     //day
        "h+": this.getHours(),    //hour
        "m+": this.getMinutes(),  //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
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

function NewDate(str) {
    str = str.split('-');
    var date = new Date();
    date.setUTCFullYear(str[0], str[1] - 1, str[2]);
    date.setUTCHours(0, 0, 0, 0);
    return date;
}
function TimeCom(dateValue) {
    var newCom;

    if (dateValue == "") {
        newCom = new Date();
    } else {
        newCom = NewDate(dateValue);
    }
    this.year = newCom.getYear();
    this.month = newCom.getMonth() + 1;
    this.day = newCom.getDate();
    this.hour = newCom.getHours();
    this.minute = newCom.getMinutes();
    this.second = newCom.getSeconds();
    this.msecond = newCom.getMilliseconds();
    this.week = newCom.getDay();
}
function MyDateDiff(interval, date1, date2) {
    var TimeCom1 = new TimeCom(date1);
    var TimeCom2 = new TimeCom(date2);
    var result;
    switch (String(interval).toLowerCase()) {
        case "y":
        case "year":
            result = TimeCom1.year - TimeCom2.year;
            break;
        case "m":
        case "month":
            result = (TimeCom1.year - TimeCom2.year) * 12 + (TimeCom1.month - TimeCom2.month);
            break;
        case "d":
        case "day":
            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day)) / (1000 * 60 * 60 * 24));
            break;
        case "h":
        case "hour":
            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day, TimeCom1.hour) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day, TimeCom2.hour)) / (1000 * 60 * 60));
            break;
        case "min":
        case "minute":
            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day, TimeCom1.hour, TimeCom1.minute) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day, TimeCom2.hour, TimeCom2.minute)) / (1000 * 60));
            break;
        case "s":
        case "second":
            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day, TimeCom1.hour, TimeCom1.minute, TimeCom1.second) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day, TimeCom2.hour, TimeCom2.minute, TimeCom2.second)) / 1000);
            break;
        case "ms":
        case "msecond":
            result = Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day, TimeCom1.hour, TimeCom1.minute, TimeCom1.second, TimeCom1.msecond) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day, TimeCom2.hour, TimeCom2.minute, TimeCom2.second, TimeCom1.msecond);
            break;
        case "w":
        case "week":
            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day)) / (1000 * 60 * 60 * 24)) % 7;
            break;
        default:
            result = "invalid";
    }
    return (result);
}
////使用方法:

//var testDate = new Date();

//var testStr = testDate.format("YYYY年MM月dd日hh小时mm分ss秒");

//alert(testStr);
