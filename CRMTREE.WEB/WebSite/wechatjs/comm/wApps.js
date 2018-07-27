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

///////////////////////////////////////////////////////////////////////////////////////////////////////////
var init = {
    App: function () { App_List.init(); },
    App_Modify: function () { App_Modify.init(); }
};
var App_List = {
    init: function () {
        this.Loading();
    },
    Loading: function () {
        this._Con_data._bindApp_List();
    },
    _Con_data: {
        _li_1: function () {
            return "<li><b>" + arguments[0] + "</b></li>";
        },
        _li_2: function () {
            return "<li><span>预约内容："
                + arguments[0] + "</span><br /><span>服务经销商："
                + arguments[1] + "</span><br /><span>预约时间："
                + arguments[2] + "</span></li>";
        },
        _li_3: function () {
            return "<li><a href=\"/wechat/WAppointment/App_Modify.html?edit=1&AP_Code=" + arguments[0] + "\" class=\"button border-gray \"><span class=\"icon-bars margin-small-right\"></span>预约详情</a></li>";
        },
        _ul: function () {
            var _d = arguments[0];
            return "<ul class=\"list-reveal_01\">"
                + this._li_1(_d.CAR_CN)
                + this._li_2(_d.SER_CN, _d.AD_Name_CN, _d.AP_Time)
                + this._li_3(_d.AP_Code) + "</ul>";
        },
        _bindApp_List: function () {
            $.box.mask.show({ info: 'loading...' });
            var _Data = { Key: "App_01", _method: "Get_AppList", codes: Url.GetParam("code") };
            $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
                $.box.mask.hide();
                if (data.length > 0) {
                    $("#App_List").empty();
                    for (i = 0; i < data.length; i++) {
                        $("#App_List").append(App_List._Con_data._ul(data[i]));
                    }
                } else {
                    $("#App_List").empty(); $("#App_List").append("你没有需要更改的预约");
                }
            }, 'json');
        }
    }
};

var App_Modify = {
    init: function () {
        this.Loading();
    },
    Loading: function () {
        $(".BACK_01").click(this._Event.Click_BACK_01);
        $(".BACK_02").click(this._Event.Click_BACK_02);
        $(".BACK_03").click(this._Event.Click_BACK_03);
        $(".BACK_04").click(this._Event.Click_BACK_04);
        //===========
        $("#AP_Car").click(this._Event.Click_Car);
        $("#page2").on("click", ".Car_Select", App_Modify._Event.Click_Car_SECTCT);
        //============
        $("#AP_Dealers").click(this._Event.Click_Dealers);
        $("#page3").on("click", ".Dealers_SECTCT", App_Modify._Event.Click_Dealers_SECTCT);
        //============
        $("#AP_Adviser").click(this._Event.Click_Adviser);
        $("#page4").on("click", ".Adviser_Select", App_Modify._Event.Click_Adviser_SECTCT);
        //=============
        $("#AP_Type").click(this._Event.Click_Type);

        //===============UPDATE
        $("#SC_Code_S").bind("change", function () {
            App_Modify._GetServTypes();
        });
        $("#ST_Code_S").bind("change", function () {
            if ($("#SC_Code_S").val() == 1) {
                App_Modify._GetMaintenance();
            } else { $("#MP_Code_S").addClass("hidden").removeClass("hiddenshow"); }
        });
        $("#ServiceConfirm").bind("click", function () {
            App_Modify._Event.Click_Type_SECTCT();
        });
        $("#New_save").bind("click", function () {
            App_Modify._DataCollection();
        });
        this._Tran();
        this.IsEdit_Add();

        $(window).scroll(function () {
            var scrollTop = $(this).scrollTop();
            var scrollHeight = $(document).height();
            var windowHeight = $(this).height();
            if (scrollTop + windowHeight == scrollHeight) {
                var SumPage = $("#Adviser_SumPage").val();
                var page = $("#Adviser_Page").val();
                if (parseInt(page) + 1 <= parseInt(SumPage)) {
                    $("#Adviser_Page").val(parseInt(page) + 1);
                    App_Modify._GetAdviser(parseInt(page) + 1);
                }
            }
        });
    },
    IsEdit_Add: function () {
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
        var _Data = {
            Key: "App_01", _method: "Get_App",
            AP_Code: Url.GetParam("AP_Code")
        };
        $.box.mask.show({ info: 'loading...' });
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            $.box.mask.hide();
            if (data.length > 0) {
                $("#AP_Code").val(data[0].AP_Code);
                $("#AU_Code").val(data[0].AP_AU_Code);
                $("#AP_Car").val(data[0].CAR_CN);
                $("#CI_Code").val(data[0].AP_CI_Code);
                $("#AP_Dealers").val(data[0].AD_Name_CN);
                $("#AD_Code").val(data[0].AP_AD_Code);
                $("#AP_Adviser").val(data[0].AU_Name);
                $("#SA_Selected").val(data[0].AP_SA_Selected);
                $("#AP_Type").val(data[0].SER_CN);
                $("#SC_Code").val(data[0].AP_SC_Code);
                $("#ST_Code").val(data[0].AP_ST_Code);
                $("#MP_Code").val(data[0].AP_MP_Code);
                $("#AP_Tran").val(data[0].AP_Transportation);
                $("#AP_Time").val(_DateFormat.Get_datetime_local(data[0].AP_Time));
                if (data[0].SN_Note != null && data[0].SN_Note != "") {
                    $("#AP_Note").addClass("show").removeClass("hidden").val(data[0].SN_Note);
                }
            }
        }, 'json');
    },
    /*
    页面初始化，获取用户的基础信息
    */
    _Add: function () {        
        var _Data = { Key: "code", codes: Url.GetParam("code") };
        $.box.mask.show({ info: 'loading...' });
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            $.box.mask.hide();
            if (data.length > 0) {
                $("#AU_Code").val(data[0].MB_AU_Code);
                App_Modify._GetDafaultCar();
            }
        }, 'json');
    },
    /*
    添加预约时提供的默认汽车
    */
    _GetDafaultCar: function () {
        var _au_code = $("#AU_Code").val();
        var _Data = { Key: "Car_01", _method: "Get_DefaultCar", AU_Code: _au_code };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                $("#AP_Car").val(data[0].CAR_CN);
                $("#CI_Code").val(data[0].CI_Code);
                App_Modify._GetDafaultDealers();
            }
        }, 'json')
    },
    _GetCar: function () {
        var _getData = { Key: "Car_01", _method: "Get_CarList", AU_Code: $("#AU_Code").val() };
        $.post(_url, { _d: JSON.stringify(_getData) }, function (data) {
            if (data.length > 0) {
                $("#Car_Select").empty();
                for (i = 0; i < data.length; i++) {
                    if (parseInt($("#CI_Code").val()) == data[i].CI_Code) {
                        $("#Car_Select").append(Car.HTML.ItemBindSelect(data[i]));
                    } else {
                        $("#Car_Select").append(Car.HTML.ItemBind(data[i]));
                    }
                }
            } else {
                $("#Car_Select").empty();
                $("#Car_Select").append("你没有添加汽车");
            }
        }, 'json');
    },
    /*
    添加预约时提供的默认汽车
    */
    _GetDafaultDealers: function () {
        var _au_code = $("#AU_Code").val();
        var _ci_code = $("#CI_Code").val();
        var _Data = { Key: "App_01", _method: "Get_DefaultDealer", AU_Code: _au_code, CI_Code: _ci_code };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                $("#AP_Dealers").val(data[0].AD_Name_CN);
                $("#AD_Code").val(data[0].AD_Code);
                App_Modify._GetDefaultDateTime();
            }
        }, 'json');
    },
    _GetDealers: function () {
        var _Data = { Key: "App_01", _method: "Get_Dealer_List", AU_Code: $("#AU_Code").val() };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.dataCount > 0) {
                $("#Dealers_Select").empty();
                for (i = 0; i < data.dataJson.length; i++) {
                    if (parseInt($("#AD_Code").val()) == data.dataJson[i].AD_Code) {
                        $("#Dealers_Select").append(Dealers.HTML.ItemBindSelect(data.dataJson[i]));
                    } else {
                        $("#Dealers_Select").append(Dealers.HTML.ItemBind(data.dataJson[i]));
                    }
                }
            } else {
                $("#Dealers_Select").empty();
                $("#Dealers_Select").append("没有符合你选择汽车的经销商");
            }
        }, 'json');
    },
    /*
    设置默认的预约时间
    */
    _GetDefaultDateTime: function () {
        var _datetime = new Date();
        var _hours = _datetime.getHours();
        _datetime.setHours(_hours + 3);
        var _datetime = _DateFormat.Get_datetime_local(_datetime);
        $("#AP_Time").val(_datetime);
        App_Modify._GetDefaultAdviser();
    },
    /*
    设置默认的预约顾问
    */
    _GetDefaultAdviser: function () {
        var _au_code = $("#AU_Code").val();
        var _ci_code = $("#CI_Code").val();
        var _ad_code = $("#AD_Code").val();
        var _Data = { Key: "App_01", _method: "GetDefaultAdviser", AU_Code: _au_code, CI_Code: _ci_code, AD_Code: _ad_code };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                $("#AP_Adviser").val(data[0].AU_Name);
                $("#SA_Selected").val(data[0].DE_Code);
            }
        }, 'json');
    },
    _GetAdviser: function (page) {
        var thisPage = 1;
        if (page != undefined) {
            thisPage = page;
        }
        var _data = { Key: "App_01", _method: "Get_Adviser_List", AD_Code: $("#AD_Code").val(), page: thisPage };
        $.post(_url, { _d: JSON.stringify(_data) }, function (data) {
            if (data.dataCount > 0) {
                var _page = $("#Adviser_Page").val();
                if (_page == 1) {
                    $("#Adviser_Select").empty();
                }
                $("#Adviser_SumPage").val(data.SumPage);
                for (i = 0; i < data.dataJson.length; i++) {
                    if (parseInt($("#SA_Selected").val()) == data.dataJson[i].DE_Code) {
                        $("#Adviser_Select").append(Adviser.HTML.ItemBindSelect(data.dataJson[i]));
                    } else {
                        $("#Adviser_Select").append(Adviser.HTML.ItemBind(data.dataJson[i]));
                    }
                }
            } else {
                $("#Adviser_Select").empty();
                $("#Adviser_Select").append("该经销商没有符合你的顾问");
            }
        }, 'json');
    },
    checkAdviserTime: function () {
        var _data = {
            Key: "App_01", _method: "checkAdviserTime",
            AD_Code: $("#AD_Code").val(),
            DE_Code: $("#SA_Selected").val(),
            AP_Date: _DateFormat.Get_date_local($("#AP_Time").val()),
            AP_Time: _DateFormat.timeConvert($("#AP_Time").val())
        };
        $.post(_url, { _d: JSON.stringify(_data) }, function (data) {
            if (data.length > 0) {
                if (data[0].Found == 0) {
                    $("#show_info").html(data[0].AP_Time_Remark);
                    $("#check_adviser_time").click();
                }
            }
        }, 'json');
    },
    _GetServCategory: function () {
        var _data = { Key: "App_01", _method: "Get_ServCategory_List", AD_Code: $("#AD_Code").val() };
        $.post(_url, { _d: JSON.stringify(_data) }, function (data) {
            if (data.length > 0) {
                $("#SC_Code_S").empty();
                for (i = 0; i < data.length; i++) {
                    $("#SC_Code_S").append(DropDownList.option(data[i].SC_Code, data[i].SC_Desc_CN));
                }
                App_Modify._GetServTypes();
            }
        }, 'json');
    },
    _GetServTypes: function () {
        var _Data = {
            Key: "App_01", _method: "Get_ServTypes_List",
            AD_Code: $("#AD_Code").val(),
            SC_Code: $("#SC_Code_S").val()
        };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                $("#ST_Code_S").empty();
                for (i = 0; i < data.length; i++) {
                    $("#ST_Code_S").append(DropDownList.option(data[i].ST_Code, data[i].ST_Desc_CN));
                }
                if ($("#SC_Code_S").val() == 1) {
                    App_Modify._GetMaintenance();
                } else {
                    $("#MP_Code_S").addClass("hidden").removeClass("hiddenshow");
                }
            } else {
                $("#ST_Code_S").empty();
                $("#ST_Code_S").append(DropDownList.option(-1, "你可以留言,没有合适的类型"));
                $("#MP_Code_S").addClass("hidden").removeClass("hiddenshow");
            }
        }, 'json');
    },
    _GetMaintenance: function () {
        var _Data = {
            Key: "App_01", _method: "Get_Maintenance_List",
            AD_Code: $("#AD_Code").val()
        };
        $.post(_url, { _d: JSON.stringify(data) }, function (data) {
            if (data.length > 0) {
                $("#MP_Code_S").addClass("show").removeClass("hidden");
                $("#MP_Item").empty();
                for (i = 0; i < data.length; i++) {
                    $("#MP_Item").append(Maintenance._Select_Loading.Ul_List_01.li_01(data[i]));
                }
            } else {
                $("#MP_Item").empty();
                $("#MP_Item").append(Maintenance._Select_Loading.Ul_List_01.li_02("该经销商没有提供保养套餐"));
            }
        }, 'json');
    },
    _ServiceConfirm: function () {
        //alert($("#SC_Code_S").find("option:selected").text());
        //alert($("#ST_Code_S").val());
        //alert($('input[name="MP_I"]:checked').parent().text());
        var SC_Code_S = $("#SC_Code_S").val();
        var ST_Code_S = $("#ST_Code_S").val();
        if (SC_Code_S == 1) {
            if (ST_Code_S == -1 && $("#AP_Note_S").val() == "") {
                Tips_01.Show_f("该经销商没有相关的服务，你可以留言");
                return false;
            } else if ($('input[name="MP_I"]:checked').val() == undefined) {
                Tips_01.Show_f("你需要选择合适的套餐");
                return false;
            } else {
                Tips_01.Show_f("");
                return true;
            }
        } else {
            Tips_01.Show_f("");
            return true;
        }
    },
    _ServiceSelect: function () {
        var AP_Type = $("#SC_Code_S").find("option:selected").text()
                + "："
                + $("#ST_Code_S").find("option:selected").text();
        $("#SC_Code").val($("#SC_Code_S").val());
        $("#ST_Code").val($("#ST_Code_S").val());
        $("#MP_Code").val("");
        if ($('input[name="MP_I"]:checked').val() != undefined) {
            $("#MP_Code").val($('input[name="MP_I"]:checked').val());
            AP_Type = $("#SC_Code_S").find("option:selected").text()
                + "："
                + $('input[name="MP_I"]:checked').parent().text();
        }
        if ($("#AP_Note_S").val() == "") {
            $("#AP_Note").val("");
            $("#AP_Note").addClass("hidden").removeClass("show");
        } else {
            $("#AP_Note").val($("#AP_Note_S").val());
            $("#AP_Note").addClass("show").removeClass("hidden");
        }
        $("#AP_Type").val(AP_Type);

    },
    _Event: {
        Click_BACK_01: function () {
            $("#page2").removeClass("slidefade").addClass("slidefade_h").hide();
            $("#page1").show();
        },
        Click_Car: function () {
            App_Modify._GetCar();
            $("#page2").removeClass("slidefade_h").addClass("slidefade").show();
            $("#page1").hide();
        },
        Click_Car_SECTCT: function () {
            var _car_d = $(this).find(".Car_Info_01").text();
            _car_d = eval("(" + _car_d + ")");
            $("#AP_Car").val(_car_d.CAR_CN);
            $("#CI_Code").val(_car_d.CI_Code);
            $("#page2").removeClass("slidefade").addClass("slidefade_h").hide();
            $("#page1").show();
        },

        Click_BACK_02: function () {
            $("#page3").removeClass("slidefade").addClass("slidefade_h").hide();
            $("#page1").show();
        },
        Click_Dealers: function () {
            App_Modify._GetDealers();
            $("#page3").removeClass("slidefade_h").addClass("slidefade").show();
            $("#page1").hide();
        },
        Click_Dealers_SECTCT: function () {
            var _Dealers_d = $(this).find(".Dealers_Info_01").text();
            _Dealers_d = eval("(" + _Dealers_d + ")");
            $("#AP_Dealers").val(_Dealers_d.AD_Name_CN);
            $("#AD_Code").val(_Dealers_d.AD_Code);
            $("#page3").removeClass("slidefade").addClass("slidefade_h").hide();
            $("#page1").show();
        },

        Click_BACK_03: function () {
            $("#page4").removeClass("slidefade").addClass("slidefade_h").hide();
            $("#page1").show();
        },
        Click_Adviser: function () {
            $("#Adviser_Page").val(1);
            App_Modify._GetAdviser();
            $("#page4").removeClass("slidefade_h").addClass("slidefade").show();
            $("#page1").hide();
        },
        Click_Adviser_SECTCT: function () {
            $("body").on("click", "#confirmAll", App_Modify._Event.Click_Adviser_Select_01);
            $("#show_info_dialog").html($(this).prop("outerHTML"));
            $("#select_adviser_dialog").click();

            //var _Adviser_d = $(this).find(".Adviser_Info_01").text();
            //_Adviser_d = eval("(" + _Adviser_d + ")");
            //$("#AP_Adviser").val(_Adviser_d.AU_Name);
            //$("#SA_Selected").val(_Adviser_d.DE_Code);
            //$("#page4").removeClass("slidefade").addClass("slidefade_h").hide();
            //$("#page1").show();
        },
        Click_Adviser_Select_01: function () {
            $(".dialog-mask").remove();
            $(".dialog-win").remove();
            var _Adviser_d = $(this).parent().parent().find(".Adviser_Info_01").text();
            _Adviser_d = eval("(" + _Adviser_d + ")");
            $("#AP_Adviser").val(_Adviser_d.AU_Name);
            $("#SA_Selected").val(_Adviser_d.DE_Code);
            $("#page4").removeClass("slidefade").addClass("slidefade_h").hide();
            $("#page1").show();
            App_Modify.checkAdviserTime();
        },

        Click_BACK_04: function () {
            $("#page5").removeClass("slidefade").addClass("slidefade_h").hide();
            $("#page1").show();
        },
        Click_Type: function () {
            App_Modify._GetServCategory();
            $("#page5").removeClass("slidefade_h").addClass("slidefade").show();
            $("#page1").hide();
        },
        Click_Type_SECTCT: function () {
            if (App_Modify._ServiceConfirm()) {
                App_Modify._ServiceSelect();
                $("#page5").removeClass("slidefade").addClass("slidefade_h").hide();
                $("#page1").show();
            }
        },
    },
    _DataCollection: function () {
        var App_data = {
            Key: "App_01", _method: "Modify_Appt",
            AP_Code: $.trim($("#AP_Code").val()),
            AU_Code: $.trim($("#AU_Code").val()),
            CI_Code: $.trim($("#CI_Code").val()),
            CAR_CN: $.trim($("#AP_Car").val()),
            AD_Code: $.trim($("#AD_Code").val()),
            AD_Name_CN: $.trim($("#AP_Dealers").val()),
            SA_Selected: $.trim($("#SA_Selected").val()),
            AU_Name: $.trim($("#AP_Adviser").val()),
            SC_Code: $.trim($("#SC_Code").val()),
            ST_Code: $.trim($("#ST_Code").val()),
            MP_Code: $.trim($("#MP_Code").val()),
            SER_CN: $.trim($("#AP_Type").val()),
            AP_Tran: $.trim($("#AP_Tran").val()),
            AP_Tran_Name: $.trim($("#AP_Tran").find("option:selected").text()),
            AP_Time: $.trim($("#AP_Time").val()),
            AP_Note: $.trim($("#AP_Note").val()),
        };
        var data_check = function () {
            if (App_data.CI_Code == null || App_data.CI_Code == "") {
                Tips_01.Show("你没有选择需要预约的汽车");
                return false;
            } else if (App_data.AD_Code == null || App_data.AD_Code == "") {
                Tips_01.Show("你没有选择需要预约的经销商");
                return false;
            } else if (App_data.SA_Selected == null || App_data.SA_Selected == "") {
                Tips_01.Show("你没有选择需要预约的顾问");
                return false;
            } else if (App_data.SC_Code == null || App_data.SC_Code == "") {
                Tips_01.Show("你没有选择需要预约的服务类型");
                return false;
            } else if (App_data.ST_Code == null || App_data.ST_Code == "") {
                Tips_01.Show("你没有选择需要预约的服务类型");
                return false;
            } else if ((App_data.SC_Code == 1) && (App_data.MP_Code == null || App_data.MP_Code == "")) {
                Tips_01.Show("你没有选择需要预约的服务类型");
                return false;
            } else if (App_data.AP_Tran == null || App_data.AP_Tran == "") {
                Tips_01.Show("你没有选择需要预约的交通方式");
                return false;
            } else if (App_data.AP_Time == null || App_data.AP_Time == "") {
                Tips_01.Show("你没有选择需要预约的时间");
                return false;
            } else if (new Date(App_data.AP_Time) < new Date()) {
                Tips_01.Show("你选择需要预约的时间有误");
                return false;
            } else { return true; }

        };

        if (data_check()) {
            $.post(_url, { _d: JSON.stringify(App_data) }, function (data) {
                if (data >= 0) {
                    Tips_01.Show("预约信息处理成功");
                    $("#New_save").hide();
                }
            }, 'json');
        }
    },
    _Tran: function () {
        var _Data = { Key: "App_01", _method: "Get_Tran_List" };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            if (data.length > 0) {
                for (i = 0; i < data.length; i++) {
                    $("#AP_Tran").append(DropDownList.option(data[i].PTP_Code, data[i].PTP_Desc_CN));
                }
            }
        }, 'json');
    },
};
var Car = {
    HTML: {
        ItemBindSelect: function (d) {
            return this.ItemBind(d, true);
        }, ItemBind: function (d, isSelect) {
            var _css = "doc-icons-a Car_Select ";
            if (isSelect) {
                _css += " select";
            }
            var _html = "";
            if (d != null) {
                _html += "<ul class=\"" + _css + "\">" + this.item1(d.CI_Picture_FN) + this.item2(d.CAR_CN) + this.itemHidden(d) + "</ul>";
            }
            return _html;
        }, item1: function (img) {
            var _img = "DefaultCar.png";
            if ($.trim(img) != "") {
                _img = img;
            }
            return " <li><img class=\"radius-circle\" src=\"/images/Car/" + _img + "\" /></li>";
        }, item2: function (CAR_CN) {
            var _html = "";
            if ($.trim(CAR_CN) != "") {
                _html += "<b>" + CAR_CN + "</b>";
            }
            return "<li>" + _html + "</li>";
        }, itemHidden: function (o) {
            return "<li class =\"Car_Info_01 hidden\">" + JSON.stringify(o) + "</li>";
        }
    }
};
var Dealers = {
    HTML: {
        ItemBindSelect: function (d) {
            return this.ItemBind(d, true);
        }, ItemBind: function (d, isSelect) {
            var _css = "doc-icons-a Dealers_SECTCT ";
            if (isSelect) {
                _css += " select";
            }
            var _html = "";
            if (d != null) {
                _html += "<ul class=\"" + _css + "\">" + this.item1(d.AD_logo_file_S) + this.item2(d.AD_Name_CN) + this.itemHidden(d) + "</ul>";
            }
            return _html;
        }, item1: function (img) {
            var _img = "BuickOEM.png";
            if ($.trim(img) != "") {
                _img = img;
            }
            return " <li><img class=\"radius-circle\" src=\"/images/DealersLogo/" + _img + "\" /></li>";
        }, item2: function (AD_Name_CN) {
            var _html = "";
            if ($.trim(AD_Name_CN) != "") {
                _html += "<b>" + AD_Name_CN + "</b>";
            }
            return "<li>" + _html + "</li>";
        }, itemHidden: function (o) {
            return "<li class =\"Dealers_Info_01 hidden\">" + JSON.stringify(o) + "</li>";
        }
    }
};
var Adviser = {
    HTML: {
        ItemBindSelect: function (d) {
            return this.ItemBind(d, true);
        }, ItemBind: function (d, isSelect) {
            var _css = "doc-icons-a Adviser_Select ";
            if (isSelect) {
                _css += " select";
            }
            var _html = "";
            if (d != null) {
                _html += "<ul class=\"" + _css + "\">"
                        + this.item1(d.DE_Picture_FN)
                        + this.item2(d)
                        + this.item3(d.AL_District)
                        + this.item4(d)
                        + this.itemHidden(d) + "</ul>";
            }
            return _html;
        }, item1: function (img) {
            var _img = "1578.png";
            if ($.trim(img) != "") {
                _img = img;
            }
            return " <li><img class=\"radius-circle\" src=\"/images/Adviser/" + _img + "\" /></li>";
        }, item2: function (o) {
            var _html = "";
            if ($.trim(o.AU_Name) != "") {
                _html += "<div><b>姓名：</b>" + o.AU_Name + "</div>";
            } if ($.trim(o.UG_Name_CN) != "") {
                _html += "<div><b>职位：</b>" + o.UG_Name_CN + "</div>";
            } if ($.trim(o.PL_Number) != "") {
                _html += "<div><b>电话：</b>" + o.PL_Number + "</div>";
            }
            return "<li>" + _html + "</li>";
        }, item3: function (address) {
            var _html = "";
            if ($.trim(address) != "") {
                _html += "<li><b>地址：</b>" + address + "</li>";
            }
            return _html;
        }, item4: function (o) {
            var _app_time = $("#AP_Time").val();
            var _isVacation = _DateFormat.DateCenter(_app_time, o.DE_Vacation_St, o.DE_Vacation_En);
            var _html = "";
            if (_isVacation) {
                //_html += "<li class=\"NoSelect\"><div><b>假期时间：</b>"
                //      + _DateFormat.Get_date_local(o.DE_Vacation_St) + " -- "
                //      + _DateFormat.Get_date_local(o.DE_Vacation_En) + "</div><div>假期时间暂时不能为你服务</div></li>";
                _html += "<li class=\"NoSelect\"><div>暂时不能为你服务</div></li>";
            } else {
                var _day = _DateFormat.GetDateDay(_app_time);
                var d1, d2, d3, d4;
                if (_day == 1) {
                    d1 = o.DP_D1_AM_S, d2 = o.DP_D1_AM_E, d3 = o.DP_D1_PM_S, d4 = o.DP_D1_PM_E;
                } else if (_day == 2) {
                    d1 = o.DP_D2_AM_S, d2 = o.DP_D2_AM_E, d3 = o.DP_D2_PM_S, d4 = o.DP_D2_PM_E;
                } else if (_day == 3) {
                    d1 = o.DP_D3_AM_S, d2 = o.DP_D3_AM_E, d3 = o.DP_D3_PM_S, d4 = o.DP_D3_PM_E;
                } else if (_day == 4) {
                    d1 = o.DP_D4_AM_S, d2 = o.DP_D4_AM_E, d3 = o.DP_D4_PM_S, d4 = o.DP_D4_PM_E;
                } else if (_day == 5) {
                    d1 = o.DP_D5_AM_S, d2 = o.DP_D5_AM_E, d3 = o.DP_D5_PM_S, d4 = o.DP_D5_PM_E;
                } else if (_day == 6) {
                    d1 = o.DP_D6_AM_S, d2 = o.DP_D6_AM_E, d3 = o.DP_D6_PM_S, d4 = o.DP_D6_PM_E;
                } else if (_day == 7) {
                    d1 = o.DP_D7_AM_S, d2 = o.DP_D7_AM_E, d3 = o.DP_D7_PM_S, d4 = o.DP_D7_PM_E;
                }
                if ($.trim(d1) == "") {
                    d1 = "9.00";
                } if ($.trim(d2) == "") {
                    d2 = "12.00";
                } if ($.trim(d3) == "") {
                    d3 = "13.00";
                } if ($.trim(d4) == "") {
                    d4 = "18.00";
                }
                _html += "<li class=\"font_color\"><b>上班时间</b>"
                      + "<div ><b>上午：</b>" + _DateFormat.set_time(d1) + " - " + _DateFormat.set_time(d2) + "</div>"
                      + "<div ><b>下午：</b>" + _DateFormat.set_time(d3) + " - " + _DateFormat.set_time(d4) + "</div>"
                      + "</li>";
            }
            return _html;
        }, itemHidden: function (o) {
            return "<li class =\"Adviser_Info_01 hidden\">" + JSON.stringify(o) + "</li>";
        }
    }
};
var Maintenance = {
    _Select_Loading: {
        Ul_List_01: {
            li_01: function () {
                var _d = arguments[0];
                return "<li class=\"radius-big\"><label><input type=\"radio\" name=\"MP_I\" value=\"" + _d.MP_Code + "\" />" + _d.MP_Desc_CN + "</label></li>";
            },
            li_02: function () {
                return "<li class=\"radius-big\"><label>" + arguments[0] + "</label></li>";
            },
            ul_01: function () {
                return "<ul class=\"doc-l_01\">" + this.li_01(arguments[0]) + "</li>";
            }
        },
    },
};

var Tips_01 = {
    Show: function () { $(".ts").text(arguments[0]); },
    Hidden: function () { $(".ts").text(""); },
    Show_f: function () { $(".ts_f").text(arguments[0]); },
    Hidden_f: function () { $(".ts_f").text(""); }
};