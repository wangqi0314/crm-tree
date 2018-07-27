var _url = "/wechat/02_MainService.aspx";


//下拉列表模板
var DropDownList = {
    select: function (id, v, t) {
        var op = this.option(v, t);
        var se = "<select name=\"" + id + "\" id=\"" + id + "\">" + op + "</select>";
        return se;
    },
    option: function (v, t) {
        var op = "<option value=\"" + v + "\">" + t + "</option>";
        return op;
    },
    optionSelect: function (v, t) {
        var op = "<option value=\"" + v + "\" selected=\"selected\">" + t + "</option>";
        return op;
    }
};

//预加载显示
var loader = {
    Show: function (c) {
        var load = "<div class=\"loader\">正在加载中..</div>";
        $(c).append(load);
    },
    empy: function (c) {
        $(c).empty();
    }
};

////////////////////////////////////////////////////////////////////////////////////////////////////
var init = {
    Service: function () { Service_List.init(); },
    Service_Info: function () { Service_Info.init(); }
};
var Service_List = {
    init: function () { this.Loading(); },
    Loading: function () {
        this._Con_data._bindService_List();
    },
    _Con_data: {
        _li_1: function () {
            return "<li><b>" + arguments[0] + "</b></li>";
        },
        _li_2: function () {
            return "<li><span>服务公司："
                + arguments[0] + "</span><br /><span>服务顾问："
                + arguments[1] + "</span><br /><span>服务时间："
                + arguments[2] + "</span></li>";
        },
        _li_3: function () {
            return "<li><a href=\"/wechat/Other/ServiceHis_Info.html?HS_Code=" + arguments[0] + "\" class=\"button border-gray\"><span class=\"icon-bars margin-small-right\"></span>服务详情</a></li>";
        },
        _ul: function () {
            var _d = arguments[0];
            return "<ul class=\"list-reveal_01\">"
                + this._li_1(_d.CAR_CN)
                + this._li_2(_d.AD_Name_CN, _d.AU_Name_AE, _DateFormat.Get_date_local(_d.HS_RO_Close))
                + this._li_3(_d.HS_Code) + "</ul>";
        },
        _bindService_List: function () {
            var _Data = { Key: "Server_01", _method: "Get_Service_List", codes: Url.GetParam("code") };
            $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
                if (data.length > 0) {
                    $("#Service_List").empty();
                    for (i = 0; i < data.length; i++) {
                        $("#Service_List").append(Service_List._Con_data._ul(data[i]));
                    }
                } else {
                    $("#Service_List").empty(); $("#Service_List").append("没有数据");
                }
            }, 'json');
        }
    }
};
var Service_Info = {
    init: function () { this.Loading(); },
    Loading: function () {
        this._Con_data._bindService_List();
    },
    _Con_data: {
        _li_1: function () {
            return "<li><b>" + arguments[0] + "</b></li>";
        },
        _li_2: function () {
            return "<li><span>服务公司："
                + arguments[0] + "</span><br /><span>服务顾问："
                + arguments[1] + "</span><br /><span>服务时间："
                + arguments[2] + "</span></li>";
        },
        _li_3: function () {
            return "<li><a href=\"/wechat/Other/ServiceHis_Info.html?HS_Code=" + arguments[0] + "\" class=\"button border-gray\"><span class=\"icon-bars margin-small-right\"></span>服务详情</a></li>";
        },
        _ul: function () {
            var _d = arguments[0];
            return "<ul class=\"list-reveal_01\">"
                + this._li_1(_d.CAR_CN)
                + this._li_2(_d.AD_Name_CN, _d.AU_Name_AE, _DateFormat.Get_date_local(_d.HS_RO_Close))
                + this._li_3(_d.HS_Code) + "</ul>";
        },
        _bindService_List: function () {
            var _Data = { Key: "Server_01", _method: "Get_Service_Info", HS_Code: Url.GetParam("HS_Code") };
            $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
                if (data.length > 0) {
                    $("#Service_Info").empty();
                    for (i = 0; i < data.length; i++) {
                        $("#Service_Info").append(data[i]);
                    }
                } else {
                    $("#Service_Info").empty(); $("#Service_Info").append("没有详情数据");
                }
            }, 'json');
        }
    }
};

var Tips_01 = {
    Show: function () { $(".ts").text(arguments[0]); },
    Hidden: function () { $(".ts").text(""); },
    Show_f: function () { $(".ts_f").text(arguments[0]); },
    Hidden_f: function () { $(".ts_f").text(""); }
};