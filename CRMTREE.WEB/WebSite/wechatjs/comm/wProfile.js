var _url = "/wechat/02_MainService.aspx";
/* HTML页面初始化*/
var init = {
    profileMain: function () { profile.init(); },
    personalInit: function () { personalInfo.init(); },
    prosonalCarfile: function () { CarInfo.init(); },
    prosonalCarList: function () { personal_Car_List.init(); },
}
/* 个人信息主页面处理信息*/
var profile = {
    init: function () {
        var _Data = { Key: "code", codes: Url.GetParam("code") };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                $("#user").text("欢迎光临" + data[0].WF_NickName);
            }
        }, 'json');
    }
};
/* 个人信息页面处理信息*/
var personalInfo = {
    init: function () {
        this.EventClick();
        this._GetUserInfo();
    },
    EventClick: function () {
        $("#_save").click(this._save);
    },
    _GetUserInfo: function () {
        var _Data = { Key: "Key_01", _method: "GetAll_User_Info" };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                $("#userName").val(data[0].AU_Name);
                $("#userPhone").val(data[0].PL_Number);
                $("#birthday").val(_DateFormat.Get_date_local(data[0].AU_B_date));
                if (data[0].AU_Gender == "False") {
                    $("._se_01").addClass("active");
                    $("._se_02").removeClass("active");
                } else {
                    $("._se_01").removeClass("active");
                    $("._se_02").addClass("active");
                }
            } else {
                window.location.href = "/wechat/frmwxRegister.aspx";
            }
        }, 'json');
    },
    _save: function () {
        var _getData = {
            Key: "Key_01", _method: "save_Info",
            UserName: $.trim($("#userName").val()), Moblid: $.trim($("#userPhone").val()),
            birthday: $.trim($("#birthday").val()), sex: $(".radio .active").find("input").val()
        };
        var _dataCheck = {
            userName: InputChecks.userName(_getData.UserName), Mobile: InputChecks.Mobile(_getData.Moblid),
            birthday: InputChecks.birthday(_getData.birthday), sex: _getData.sex,
        }
        var save_check = function () {
            var _c = false;
            if (!_dataCheck.userName) { $(".ts").text("用户名不能为空"); _c = false; return _c }
            if (!_dataCheck.Mobile) { $(".ts").text("手机号码有误"); _c = false; return _c }
            if (!_dataCheck.birthday) { $(".ts").text("出生日期有误"); _c = false; return _c }
            _c = true; return _c
        }
        if (save_check()) {
            $.post(_url, { _d: JSON.stringify(_getData) }, function (data) {
                if (data > 0) {
                    $(".ts").text("信息保存成功")
                }
            }, 'json');
        }

    }
};
/* 个人汽车信息列表页面处理*/
var personal_Car_List = {
    init: function () { this.Loading(); },
    Loading: function () {
        this._Con_data._bindService_List();
    },
    _Con_data: {
        _li_1: function () {
            return "<li><b>" + arguments[0] + "</b></li>";
        },
        _li_2: function () {
            return "<li><span>牌照：" + arguments[0] + "</span></li>";
        },
        _li_3: function () {
            return "<li><a href=\"/wechat/profile/personalCarInfo.html?edit=1&CI_Code=" + arguments[0] + "\" class=\"button border-gray\"><span class=\"icon-bars margin-small-right\"></span>汽车详情</a></li>";
        },
        _ul: function () {
            var _d = arguments[0];
            return "<ul class=\"list-reveal_01\">"
                + this._li_1(_d.CAR_CN)
                + this._li_2(_d.CI_Licence)
                + this._li_3(_d.CI_Code) + "</ul>";
        },
        _bindService_List: function () {
            var _Data = { Key: "Car_01", _method: "Get_CarList" };
            $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
                if (data.length > 0) {
                    $("#Service_List").empty();
                    for (i = 0; i < data.length; i++) {
                        $("#Service_List").append(personal_Car_List._Con_data._ul(data[i]));
                    }
                } else { $("#Service_List").empty(); $("#Service_List").append("你没有添加汽车信息"); }
            }, 'json');
        }
    }
};
/* 个人汽车详细信息页面处理*/
var CarInfo = {
    init: function () {
        this.EventClick();
        this.Loading();
    },
    EventClick: function () {
        /* 保存事件*/
        $("#_save").click(this._save);
        /* change事件*/
        $("#CI_Make").change(this.bindModel);
        $("#CI_Model").change(this.bindStyle);
        /* 提示页返回事件*/
        $(".BACK_01").click(this._Event.Click_BACK_01);
        /* 选择汽车信息后的事件处理*/
        $("#CI_CS_Code").click(this._Event.Click_Car);
        $("#CarConfirm").click(this._Event.Click_Car_SECTCT);

    },
    Loading: function () {
        this.bindMake();
        this._IsEdit_Add();
    },
    _IsEdit_Add: function () {
        var edit = Url.GetParam("edit");
        var add = Url.GetParam("add");
        if (edit == null) {
            this._Add(add);
        }
        else {
            this._Edit(edit);
        }
    },
    _Edit: function () {
        var _getData = { Key: "Car_01", _method: "Get_CarInfo", CI_Code: Url.GetParam("CI_Code") }
        $.post(_url, { _d: JSON.stringify(_getData) }, function (data) {
            if (data.length > 0) {
                $("#CI_Code").val(data[0].CI_Code);
                $("#AU_Code").val(data[0].CI_AU_Code);
                $("#CI_CS_Code").val(data[0].CAR_CN);
                $("#hid_CI_Style").val(data[0].CI_CS_Code);
                $("#CI_Licence").val(data[0].CI_Licence);
                $("#CI_Frame").val(data[0].CI_Frame);
                $("#CI_Licence_dt").val(_DateFormat.Get_date_local(data[0].CI_Licence_dt));
                $("#CI_Driving").val(data[0].CI_Driving);
                $("#CI_Driving_Type").val(data[0].CI_Driving_Type);
                $("#CI_Driving_dt").val(_DateFormat.Get_date_local(data[0].CI_Driving_dt));
                $("#CI_Warr_St_dt").val(_DateFormat.Get_date_local(data[0].CI_Warr_St_dt));
            }
        }, 'json');
    },
    _Add: function () {
    },
    bindMake: function () {
        var _Data = { Key: "Key_01", _method: "Get_Make" };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                $("#CI_Make").empty();
                for (i = 0; i < data.length; i++) {
                    $("#CI_Make").append(HtmlBind._option(data[i].MK_Code, data[i].MK_Make_CN));
                }
                CarInfo.bindModel();
            }
        }, 'jaon');
    },
    bindModel: function () {
        var _Data = { Key: "Key_01", _method: "Get_Model", MK_code: $("#CI_Make").val() };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                $("#CI_Model").empty();
                for (i = 0; i < data.length; i++) {
                    $("#CI_Model").append(HtmlBind._option(data[i].CM_Code, data[i].CM_Model_CN));
                }
                CarInfo.bindStyle();
            }
        }, 'json');
    },
    bindStyle: function () {
        var _Data = { Key: "Key_01", _method: "Get_Style", CM_Code: $("#CI_Model").val() };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                $("#CI_Style").empty();
                for (i = 0; i < data.length; i++) {
                    $("#CI_Style").append(HtmlBind._option(data[i].CS_Code, data[i].CS_Style_CN));
                }
            }
        }, 'json');
    },
    _Event: {
        Click_BACK_01: function () {
            $("#page2").removeClass("slidefade").addClass("slidefade_h").hide();
            $("#page1").show();
        },
        Click_Car: function () {
            CarInfo.bindMake();
            $("#page2").removeClass("slidefade_h").addClass("slidefade").show();
            $("#page1").hide();
        },
        Click_Car_SECTCT: function () {
            if (CarInfo._CarConfirm()) {
                CarInfo._CarSelect();
                $("#page2").removeClass("slidefade").addClass("slidefade_h").hide();
                $("#page1").show();
            }
        },
    },
    _CarConfirm: function () {
        var _make = $("#CI_Make").val();
        var _model = $("#CI_Model").val();
        var _style = $("#CI_Style").val();
        if (_make == null || _make == "") {
            Tips_01.Show_f("汽车品牌选择错误");
            return false;
        } else if (_model == null || _model == "") {
            Tips_01.Show_f("车款选择错误");
            return false;
        } else if (_style == null || _style == "") {
            Tips_01.Show_f("型号选择错误");
            return false;
        } else {
            Tips_01.Show_f("");
            return true;
        }
    },
    _CarSelect: function () {
        var car_Info = $("#CI_Make").find("option:selected").text()
              + "-"
              + $("#CI_Model").find("option:selected").text()
              + "-"
              + $("#CI_Style").find("option:selected").text();
        $("#CI_CS_Code").val(car_Info);
        $("#hid_CI_Style").val($("#CI_Style").val());

    },
    _save: function () {
        var _getData = {
            Key: "Key_01", _method: "Modify_Car", CI_AU_Code: $.trim($("#AU_Code").val()),
            CI_CS_Code: $.trim($("#hid_CI_Style").val()), CI_VIN: $.trim($("#CI_VIN").val()),
            CI_Mileage: $.trim($("#CI_Mileage").val()), CI_Licence: $.trim($("#CI_Licence").val()),
            CI_Licence_dt: $.trim($("#CI_Licence_dt").val()), CI_YR_Code: $.trim($("#CI_YR_Code").val()),
            CI_Frame: $.trim($("#CI_Frame").val()), CI_Driving: $.trim($("#CI_Driving").val()),
            CI_Driving_Type: $.trim($("#CI_Driving_Type").val()), CI_Driving_dt: $.trim($("#CI_Driving_dt").val()),
            CI_Warr_St_dt: $.trim($("#CI_Warr_St_dt").val())
        };
        var _dataCheck = {
            userName: InputChecks.userName(_getData.UserName), Mobile: InputChecks.Mobile(_getData.Moblid),
            birthday: InputChecks.birthday(_getData.birthday), sex: _getData.sex,
        }
        var save_check = function () {
            var _c = false;
            if (!_dataCheck.userName) { $(".ts").text("用户名不能为空"); _c = false; return _c }
            if (!_dataCheck.Mobile) { $(".ts").text("手机号码有误"); _c = false; return _c }
            if (!_dataCheck.birthday) { $(".ts").text("出生日期有误"); _c = false; return _c }
            _c = true; return _c
        }
        //if (save_check()) {
        $.post(_url, { _d: JSON.stringify(_getData) }, function (data) {
            if (data >= 0) {
                $(".ts").text("信息保存成功");
                $("#_save").hide();
            }
        }, 'json');
        //}

    }
};
/* 提示信息处理*/
var Tips_01 = {
    Show: function () { $(".ts").text(arguments[0]); },
    Hidden: function () { $(".ts").text(""); },
    Show_f: function () { $(".ts_f").text(arguments[0]); },
    Hidden_f: function () { $(".ts_f").text(""); }
};