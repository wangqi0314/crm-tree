var _url = "/wechat/02_MainService.aspx";

/* 所以页面信息的初始化处理
*/
var init = {
    Car_Adviser: function () { CarBindAdviser.init(); },
};
/* Car页面总对象
*/
var CarBindAdviser = {
    /* 页面初始化，总的方法调用
    */
    init: function () {
        this.initLoading();
        this.initEvent();
    },
    /*最终绑定顾问
    */
    BindCar_Adviser: function () {
        var _para = {
            _d: JSON.stringify({
                Key: "Car_01", _method: "BindCar_Adviser", AU_Code: CarBindAdviser.$AU_Code,
                CI_Code: CarBindAdviser.$CI_Code,
                DE_Code: CarBindAdviser.$DE_Code,
                IS_Bind:1
            })
        };
        $.post(_url, _para, function (data) {
            if (data) {
                location.reload();
            }
        },'json');
    },
    /*最终解除绑定顾问
    */
    NoBindCar_Adviser: function () {
        var _para = {
            _d: JSON.stringify({
                Key: "Car_01", _method: "BindCar_Adviser", AU_Code: CarBindAdviser.$AU_Code,
                CI_Code: CarBindAdviser.$CI_Code,
                DE_Code: CarBindAdviser.$DE_Code,
                IS_Bind: 0
            })
        };
        $.post(_url, _para, function (data) {
            if (data) {
                location.reload();
            }
        },'json');
    },
    /* 页面初始化时的事件处理
    */
    initEvent: function () {
        $("#ClickBindAllCar").on("click", function () {            
            CarBindAdviser.ClickAllBind()
        });
        $("#backPage2").on("click", function () {
            CarBindAdviser.backPage2()
        });
        $("#backPage3").on("click", function () {
            CarBindAdviser.backPage3()
        });
        $("body").on("click", ".BindAdviser", function () {
            CarBindAdviser.BindAdviser(this)
        });
        $("body").on("click", "#confirmAll", function (e) {
            CarBindAdviser.BindCar_Adviser();
        });
        $("#page1").on("click", ".NoBindAdviser", function (e) {
            CarBindAdviser.$CI_Code = $(e.target).parent().find("#hidCI_Code").val();
            CarBindAdviser.$DE_Code = $(e.target).parent().find("#hidDE_Code").val();
            $("#NObind_adviser_dialog").click();
        });
        $("body").on("click", "#NoBindconfirm", function (e) {
            CarBindAdviser.NoBindCar_Adviser();
        });
    },
    /* 页面初始化，初始数据的加载
   */
    initLoading: function () {
        var _para = { _d: JSON.stringify({ Key: "Car_01", _method: "Get_BindCarList" }) };
        $.post(_url, _para, function (data) {
            CarBindAdviser.initLoading_data(data);
        },'json');
    },
    /* 页面初始化，初始数据的加载,数据处理
    */
    initLoading_data: function (data) {
        if (data.length > 0) {
            var NoBindCar = 0;
            for (i = 0; i < data.length; i++) {
                if (data[i].IsBind == 1) {
                    var _b_car = CarBindAdviser.Car_Bind.li_01(data[i].CAR_CN)
                                + CarBindAdviser.Car_Bind.li_02("服务顾问")
                                + CarBindAdviser.Car_Bind.li_03(data[i].AU_Name)
                                + CarBindAdviser.Car_Bind.li_05(data[i].CI_Code, data[i].DE_Code)
                    _b_car = CarBindAdviser.Car_Bind.ul_01(_b_car);
                    $("#CarBindList").append(_b_car);
                }
                else {
                    var _car = CarBindAdviser.Car_Bind.li_01(data[i].CAR_CN)
                                + CarBindAdviser.Car_Bind.li_02("没有绑定")
                                + CarBindAdviser.Car_Bind.li_04(data[i].CI_Code)
                    _car = CarBindAdviser.Car_Bind.ul_01(_car);
                    $("#CarBindList").append(_car);
                    NoBindCar += 1;
                }
            }
            if (NoBindCar == 0) {
                $("#NoBindCar").removeClass("show").addClass("hidden");
            } else {
                $("#NoBindCar").removeClass("hidden").addClass("show")
                $(".NoBindCar").text("你还有 " + NoBindCar + " 辆汽车没有绑定服务顾问");
            }
            CarBindAdviser.$AU_Code = data[0].CI_AU_Code                                    //===== AU_Code
            //初始加载的绑定汽车信息隐藏保存
            $("#hidCarBindInfo").val(JSON.stringify(data));
        }
    },
    /* 关于用户所有未绑定顾得的车辆，进行处理
    */
    ClickAllBind: function () {
        $("#page1").addClass("hidden").removeClass("show");
        $("#page2").addClass("show").removeClass("hidden");
        var data = $("#hidCarBindInfo").val();
        data = eval("(" + data + ")");
        if (data.length > 0) {
            $("#notBindCar").empty();
            var NoBindCar = 0;
            for (i = 0; i < data.length; i++) {
                if (data[i].IsBind == 0) {
                    var _b_car = CarBindAdviser.Car_Bind.li_01(data[i].CAR_CN)
                                + CarBindAdviser.Car_Bind.li_02("==>>")
                                + CarBindAdviser.Car_Bind.li_04(data[i].CI_Code)
                    _b_car = CarBindAdviser.Car_Bind.ul_01(_b_car);
                    $("#notBindCar").append(_b_car);
                }
            }            
        }
    },
    /* 显示顾问绑定页面Page3，加载数据
    */
    BindAdviser: function (o) {
        $("#page1").addClass("hidden").removeClass("show");
        $("#page2").addClass("hidden").removeClass("show");
        $("#page3").addClass("show").removeClass("hidden");
        CarBindAdviser.$CI_Code = $(o).parent().parent().find("#hidCI_Code").val();                  //======CI_Code
        CarBindAdviser._GetDealers(1)
    },
    /* 获取服务器经销商信息，进行处理
    */
    _GetDealers: function (page) {
        var thisPage = 1;
        if (page != undefined) {
            thisPage = page;
        }
        var _Data = { Key: "App_01", _method: "Get_Dealer_List", AU_Code: CarBindAdviser.$AU_Code, page: thisPage };
        $.post(_url, { _d: JSON.stringify(_Data) }, function (data) {
            CarBindAdviser._GetDealers_data(data);
        }, 'json');
    },
    /* 获取服务器经销商信息，进行处理,数据处理
    */
    _GetDealers_data: function (data) {
        if (data.dataCount > 0) {
            $("#d_a_info_s").empty();
            //$("#Adviser_SumRow").val(data.SumPage);
            for (i = 0; i < data.dataJson.length; i++) {
                $("#d_a_info_s").append(CarBindAdviser.Dealers_Bind.bindDiv(data.dataJson[i]));
            }
            $(".list-reveal_01").on("click", function () {
                CarBindAdviser._GetAdviser(this, 1)
            });
        } else {
            $("#d_a_info_s").empty();
            $("#d_a_info_s").append("没有符合你的顾问");
        }
    },
    /* 获取服务器顾问信息，进行处理
    */
    _GetAdviser: function (o, page) {
        var thisPage = 1;
        if (page != undefined) {
            thisPage = page;
        }
        var thisDealer = $(o).find(".hidDealers_Info").html();
        thisDealer = eval("(" + thisDealer + ")");
        CarBindAdviser.$bind = { AD_Code: thisDealer.AD_Code };
        var _data = { Key: "App_01", _method: "Get_Adviser_List", AD_Code: thisDealer.AD_Code, page: thisPage };
        $.post(_url, { _d: JSON.stringify(_data) }, function (data) {
            CarBindAdviser._GetAdviser_data(o, data);
        }, 'json');
    },
    /* 获取服务器顾问信息，进行处理,数据处理
    */
    _GetAdviser_data: function (o,data) {
        if (data.dataCount > 0) {
            //$("#d_a_info_s").empty();
            //$("#Adviser_SumRow").val(data.SumPage);
            $(o).parent().find(".doc-icons-a").remove();
            for (i = 0; i < data.dataCount; i++) {
                $(o).after(CarBindAdviser.Adviser_Bind.bingUL(data.dataJson[i]));
            }
            CarBindAdviser.SelectDealer(o);
            $(".doc-icons-a").on("click", function () { CarBindAdviser.SelectAdviser(this) });
        } else {
            $("#d_a_info_s").empty();
            $("#d_a_info_s").append("没有符合你的顾问");
        }
    },
    /* 被选择的经销商的HTML显示处理
    */
    SelectDealer: function (d) {
        var _dealer = $(d);
        if (_dealer.attr("isSelect") == "1") {
            _dealer.removeClass("reveal_back");
            _dealer.parent().removeClass("border").removeClass("border-main");
            _dealer.find(".icon-check-square-o").addClass("icon-square-o").removeClass("icon-check-square-o");
            _dealer.parent().find(".doc-icons-a").addClass("hidden").removeClass("show");
            _dealer.attr("isSelect", "0");
        } else {
            _dealer.addClass("reveal_back");
            _dealer.parent().addClass("border").addClass("border-main");
            _dealer.find(".icon-square-o").addClass("icon-check-square-o").removeClass("icon-square-o");
            this.NotSelectDealer();
            _dealer.parent().find(".doc-icons-a").addClass("show").removeClass("hidden");
            _dealer.attr("isSelect", "1");
        }
    },
    /* 没有被选择的经销商的HTML显示处理
    */
    NotSelectDealer: function () {
        $(".list-reveal_01").each(function () {
            var _dealer = $(this);
            if (_dealer.attr("isSelect") == "1") {
                _dealer.parent().removeClass("border").removeClass("border-main");
                _dealer.find(".icon-check-square-o").addClass("icon-square-o").removeClass("icon-check-square-o");
                _dealer.parent().find(".doc-icons-a").addClass("hidden").removeClass("show");
                _dealer.attr("isSelect", "0");
            }
        });
    },
    /*当选择具体的顾问后的操作
    */
    SelectAdviser: function (o) {
        CarBindAdviser.$DE_Code = $(o).find("#hidDE_Code").val();
        $("#show_info_dialog").html($(o).prop("outerHTML"));
        $("#bind_adviser_dialog").click();
    },
    /* Page2返回时需要处理的信息
    */
    backPage2: function () {
        $("#page1").addClass("show").removeClass("hidden");
        $("#page2").addClass("hidden").removeClass("show");
    },
    /* Page3返回时需要处理的信息
    */
    backPage3: function () {
        $("#page2").addClass("show").removeClass("hidden");
        $("#page3").addClass("hidden").removeClass("show");
    },
};
/* 绑定汽车信息的HTML结构
*/
CarBindAdviser.Car_Bind = {
    li_01: function () {
        return "<li><div><b>" + arguments[0] + "</b></div></li>";
    },
    li_02: function () {
        return "<li>" + arguments[0] + "</li>";
    },
    li_03: function () {
        return "<li><div><b>" + arguments[0] + "</b></div></li>";
    },
    li_04: function () {
        return "<li><div><button class=\"BindAdviser button button-little bg-dot\"><span class=\"icon-bars margin-small-right\"></span>绑定顾问</button><input type=\"hidden\" id=\"hidCI_Code\" value='" + arguments[0] + "' /></div></li>";
    },
    li_05: function () {
        return "<li><div><button class=\"NoBindAdviser button button-little button_newLi\"><span class=\"icon-bars margin-small-right\"></span>解除绑定</button><input type=\"hidden\" id=\"hidCI_Code\" value='" + arguments[0] + "' /><input type=\"hidden\" id=\"hidDE_Code\" value='" + arguments[1] + "' /></div></li>";
    },
    ul_01: function () {
        return "<ul class=\"list-reveal_01\">" + arguments[0] + "</ul>";
    }
}
/* 绑定经销商信息的HTML结构
*/
CarBindAdviser.Dealers_Bind = {
    bindDiv: function (d) {
        var _div = "<div class=\"padding-right padding-top padding-left\">"
                    + this.thisUL(d)
                    + "</div>";
        return _div;
    },
    thisUL: function (d) {
        var ul = "<ul class=\"list-reveal_01\" isselect=\"0\">"
                    + "<li><span><b>" + d.AD_Name_CN + "</b></span></li>"
                    + "<li><span><b>地址：" + d.AL_District + "</b></span></li>"
                    + "<li class=\"float-right\"><span id=\"selectDealer\" class=\"icon-square-o text-large\"></span></li>"
                    + "<li class=\"hidDealers_Info hidden\">" + JSON.stringify(d) + "</li>"
                + "</ul>";
        return ul;
    }
}
/* 绑定顾问信息的HTML结构
*/
CarBindAdviser.Adviser_Bind = {
    bingUL: function (d) {
        var _ul = "<ul class=\"doc-icons-a\">"
                   + " <li>"
                   + "<img class=\"radius-circle\" src=\"/images/Adviser/1578.png\" />"
                   + "</li>"
                   + "<li>"
                   + "   <div><b>姓名：" + d.AU_Name + "</b></div>"
                   + "   <div><b>职位：</b>" + d.UG_Name_CN + "</div>"
                   + "   <div><b>电话：</b>" + d.PL_Number + "</div>"
                   + " </li>"
                   + " <li><b>地址：</b>" + d.AL_District + "</li>"
                   + "<li><input type=\"hidden\" id=\"hidDE_Code\" value='" + d.DE_Code + "' /></li>"
                   + " </ul>";
        return _ul;
    },
}
/*================================================================================================================*/
/* 提示信息
*/
var Tips_01 = {
    Show: function () { $(".ts").text(arguments[0]); },
    Hidden: function () { $(".ts").text(""); },
    Show_f: function () { $(".ts_f").text(arguments[0]); },
    Hidden_f: function () { $(".ts_f").text(""); }
};
/* 预加载显示
*/
var loader = {
    Show: function (c) {
        var load = "<div class=\"loader\">正在加载中..</div>";
        $(c).append(load);
    },
    empy: function (c) {
        $(c).empty();
    }
};