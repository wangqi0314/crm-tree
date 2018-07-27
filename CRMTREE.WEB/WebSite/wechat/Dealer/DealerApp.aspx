<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealerApp.aspx.cs" Inherits="wechat_Dealer_DealerApp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-itunes-app" content="app-id=415206413" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no" />
    <meta name="format-detection" content="email=no" />
<%--     <link href="http://x.autoimg.cn/com/co.ashx?path=|m|style|public-v12.css,|m|style|widget-v4.css,|dealer|custom|m|dealer|dealer20140819.css" rel="stylesheet" type="text/css" />--%>
    <link href="/wechatcss/comm/public_W02.css" rel="stylesheet" />
    <title>经销商询价-经销商-手机汽车之家</title>
</head>
<body>
    <div class="nav">
        <h2 class="nav-title">询价</h2>
        <a href="javascript:history.go(-1);" class="nav-back"><i class="icon-arrow arrow-left"></i><span>返回</span></a>
        <div class="nav-mini">
            <a class="nav-mini-btn" href="javascript:void(0);" id="nav">
                <img src="http://x.autoimg.cn/m/news/images/img-navmini.png" /></a><div class="nav-mini-pop pop-opacity fn-hide" id="divnav">
                    <a href="#/">首页</a><a href="/wechat/Car/AllCar.aspx"">找车</a><i class="icon-opacity-arrow arrow-top"></i>
            </div>
        </div>
    </div>
    <div class="main" id="divwrap">
        <div class="fn-mt-big fn-mlr fn-hide" id="divErrTip"><span class="tip01 tip01-warn"><i class="icon-tip icon-warn"></i>提示信息</span></div>
        <div class="dealer-enquiry-from">
            <h3 id="hSeriesName">宝马宝马5系(进口)</h3>
            <div class="item">
                <a href="javascript:void(0);" id="aSpecInfo" class="btn btn-sel">2014款 ActiveHybrid 5<i class="icon-arrow arrow-bottom"></i></a>
            </div>
            <div class="item fn-mt">
                <div class="form-text-box">
                    <input name="txtUserName" type="text" id="txtUserName" placeholder="填写您的姓名" class="form-text" maxlength="10" />
                    <a href="javascript:void(0)" class="form-text-clear fn-hide" id="clearName"><i class="icon icon-clear"></i></a>
                </div>
                <i class="mark-must">*</i>
            </div>
            <div class="item">
                <div class="form-text-box">
                    <input name="txtMobilePhone" type="text" id="txtMobilePhone" placeholder="填写您的手机号码" class="form-text" />
                    <a href="javascript:void(0)" class="form-text-clear fn-hide" id="clearPhone"><i class="icon icon-clear"></i></a>
                </div>
                <i class="mark-must">*</i>
            </div>
        </div>
        <div class="dealer-enquiry-shop" id="recommendDealer">
            <h3>请选择经销商，最多可选3家。</h3><label class="selected"><input checked="checked" name="dealerList" dealerid="77772" data-dealerorder="0" type="checkbox"><div><h4><i></i>上海宝景</h4><p>地址：闵行区莲花南路2268号</p></div></label><label class="selected"><input checked="checked" name="dealerList" dealerid="67497" data-dealerorder="1" type="checkbox"><div><h4><i></i>上海众国宝泓</h4><p>地址：上海市普陀区红柳路233号</p></div></label><label class="selected"><input checked="checked" name="dealerList" dealerid="1355" data-dealerorder="2" type="checkbox"><div><h4><i></i>上海宝诚</h4><p>地址：上海市浦东新区龙阳路2277号永达大厦1楼</p></div></label><label class="selected"><input name="dealerList" dealerid="77773" data-dealerorder="3" type="checkbox"><div><h4><i></i>宝景星诚</h4><p>地址：松江区北松公路7168号</p></div></label><label class="selected"><input name="dealerList" dealerid="1032" data-dealerorder="4" type="checkbox"><div><h4><i></i>上海凡德汽车</h4><p>地址：上海市浦东新区沪南路2525号</p></div></label><label class="selected"><input name="dealerList" dealerid="10781" data-dealerorder="5" type="checkbox"><div><h4><i></i>上海宝诚中环</h4><p>地址：宝山区长江西路999号</p></div></label>

        </div>
        <div class="dealer-enquiry-submit">
            <a class="btn btn-bar-blue" href="javascript:void(0);" id="aInquire">开始试驾预约</a>
            <a class="btn btn-bar-blue btn-load" style="display: none;" href="javascript:void(0);" id="loadmask"><i class="icon-load"></i>提交中...</a>
        </div>

        <div class="dealer-enquiry-agree">点击询价表示阅读和同意<a href="W_agreement.html">个人信息保护声明</a></div>
        <div class="tip-layer" style="display: none" id="popup_info"><span class="tip01-layer"><i class="icon-tip icon-ok"></i>最多可询价3个经销商哦！</span></div>
    </div>
    <div class="main" id="divResult" style="display: none">
        <div class="dealer-enquiry-succeed"><i class="icon-tip icon-ok"></i>提交成功，我们将及时为您报价！</div>
        <div id="divFuwuquTip" style="display: none">
        </div>
        <div class="fn-mt-big fn-mlr"><a class="btn btn-bar-blue" href="http://m.autohome.com.cn/202/">返回车系页</a></div>
    </div>
    <div id="divCity" style="display: none"></div>
    <div class="widget" id="divSpec" style="display: none">
        <div class="w-nav">
            <h2 class="w-nav-title">经销商-选车型</h2>
            <a id="spec_select_nav_back" class="w-nav-back" href="javascript:void(0)">
                <i class="w-icon-arrow w-arrow-left"></i>
                <span>返回</span>
            </a>
            <div class="w-nav-mini">
                <a id="spec_select_nav-mini-btn" class="w-nav-mini-btn" href="javascript:void(0);">
                    <img src="http://x.autoimg.cn/m/news/images/img-navmini.png">
                </a>
                <div id="spec_select_nav_banners" class="w-nav-mini-pop w-pop-opacity w-fn-hide">
                    <a href="#">首页</a>
                    <a href="#">找车</a>
                    <a href="#">经销商</a>
                    <a href="#">降价</a>
                    <a href="#">论坛</a>
                    <i class="w-icon-opacity-arrow w-arrow-top"></i>
                </div>
            </div>
        </div>
        <div id="common_specselect_list" class="w-main">
            <ul class="w-list-infor01">

                <li>
                    <a data-id="16062" data-lowprice="879000" href="javascript:void(0)">
                        <span class="caption">2014款 ActiveHybrid 5</span>
                    </a>
                </li>

                <li>
                    <a data-id="17084" data-lowprice="457000" href="javascript:void(0)">
                        <span class="caption">2014款 520i 典雅型</span>
                    </a>
                </li>

                <li>
                    <a data-id="17087" data-lowprice="689000" href="javascript:void(0)">
                        <span class="caption">2014款 528i xDrive 设计套装型</span>
                    </a>
                </li>

                <li>
                    <a data-id="17088" data-lowprice="703000" href="javascript:void(0)">
                        <span class="caption">2014款 535i 设计套装型</span>
                    </a>
                </li>

                <li>
                    <a data-id="17089" data-lowprice="809000" href="javascript:void(0)">
                        <span class="caption">2014款 535i xDrive M运动型</span>
                    </a>
                </li>

                <li>
                    <a data-id="17163" data-lowprice="489000" href="javascript:void(0)">
                        <span class="caption">2014款 520i 旅行版</span>
                    </a>
                </li>

                <li>
                    <a data-id="17164" data-lowprice="656000" href="javascript:void(0)">
                        <span class="caption">2014款 528i 旅行版</span>
                    </a>
                </li>

                <li>
                    <a data-id="17165" data-lowprice="727000" href="javascript:void(0)">
                        <span class="caption">2014款 528i xDrive M运动型 旅行版</span>
                    </a>
                </li>

            </ul>
        </div>
        <div class="w-nav-sub">
            <h2 class="w-nav-title">经销商-选车型</h2>
            <a id="spec_select_nav_back_bottom" class="w-nav-back" href="javascript:void(0);">
                <i class="w-icon-arrow w-arrow-left"></i>
                <span>返回</span>
            </a>
        </div>
    </div>
    <div class="load-layer" id="loadingImg" style="display: none;"><i class="icon-load"></i></div>

    <script src="http://x.autoimg.cn/com/co.ashx?path=|dealer|custom|m|autozepto-1.0.min.js,|dealer|custom|m|dealer|citySelect.mini.js,|dealer|custom|m|dealer|areautil.mini.js"></script>


    <script type="text/javascript">
        var _gaq = _gaq || [];
        $(function () {
            //广告JS
            //$.LoadJs({ "url": "http://m.autohome.com.cn/Ashx/public/HeaderLayerRecApp.ashx?hchannelid=40&lchannelid=19", "type": "async", "scriptCharset": "UTF-8" }, function () { });
        })
        var inquire = function () {
            var _inquire = {}, dealerarea = parseInt($.getCookie("mdealercityId", "0"));
            if (dealerarea == 0)
                dealerarea = parseInt($.getCookie("cookieCityId", "0"));
            if (dealerarea == 0)
                dealerarea = parseInt($.getCookie("area", "0"));
            if (dealerarea == 0)
                dealerarea = 110100;
            _data = {
                seriesId: "202",
                specId: "16062",
                dealerId: "0",
                cityId: auto.areautil.getCityId(dealerarea),
                countyId: auto.areautil.getCountyId(dealerarea),
                lowPrice: "879000",
                referType: "5"
            };
            _inquire.validate = function (data) {
                if (data.specId == 0) {
                    _inquire.showErr("请选择一个车型");
                    return false;
                }
                if (data.cityId == 0) {
                    _inquire.showErr("请选择城市和地区");
                    return false;
                }
                var userName = $("#txtUserName").val();
                if ($.trim(userName) == "") {
                    _inquire.showErr("请填写您的姓名");
                    return false;
                }
                var userNameLength = $.trim(userName).length;
                if (userNameLength > 10) {
                    _inquire.showErr("您输入的名字太长，请限定在10个字符以内");
                    return false;
                }
                var userPhone = $("#txtMobilePhone").val()
                if ($.trim(userPhone) == "") {
                    _inquire.showErr("请填写您的手机号码");
                    return false;
                }
                var patrn = /^(1(([3587][0-9])|(47)))\d{8}$/;
                if (!patrn.exec($.trim(userPhone))) {
                    _inquire.showErr("手机号格式错误");
                    return false;
                }
                return true;
            };
            _inquire.changeSMSTip = function () { };
            _inquire.showErr = function (msg) {
                $("#divErrTip").html('<span class="tip01 tip01-warn"><i class="icon-tip icon-warn"></i>' + msg + '</span>').show();
            };
            _inquire.postComplete = function (data) {
                if (data.success == 1) {
                    $("#divwrap,#nav,#divCity,#divSpec").hide();
                    $("#divResult").show();
                    $("#divFuwuquTip").show();
                    if (("|110100|310100|440100|120100|500100|370100|510100|430100|").indexOf("|" + _data.cityId + "|") != -1) {
                        $("#divFuwuquTip").show();
                    }
                    var _gaq = _gaq || [];
                    if (_data.referType == "2") {
                        _gaq.push(['_trackEvent', '询价', '降价排行榜-列表页', '提交成功']);
                    }
                    else if (_data.referType == "3") {
                        _gaq.push(['_trackEvent', '询价', '降价排行榜-最终页', '提交成功']);
                    }
                    else if (_data.referType == "4") {
                        _gaq.push(['_trackEvent', '询价', '文章最终页', '提交成功']);
                    }
                    else if (_data.referType == "5") {
                        _gaq.push(['_trackEvent', '询价', '车系页-公共订单', '提交成功']);
                    }
                    else if (_data.referType == "6") {
                        _gaq.push(['_trackEvent', '询价', '车型页', '提交成功']);
                    }
                    else if (_data.referType == "7") {
                        _gaq.push(['_trackEvent', '询价', '经销商页面', '提交成功']);
                    }
                    else {
                        _gaq.push(['_trackEvent', '询价', '经销商列表', '提交成功']);
                    }
                } else if (data.success == -2 || data.success == -3 || data.success == -4 || data.success == -5) {
                    _inquire.showErr(data.message);
                } else {
                    _inquire.showErr("您的订单提交失败。");
                }
            };
            _inquire.selectSpec = function (d) {
                _data.specId = d.specId;
                _data.lowPrice = parseInt(d.lowPrice, 10);

                _inquire.recomendDealers(_data.cityId, _data.specId);
                $("#aSpecInfo").html(d.specName + '<i class="icon-arrow arrow-bottom"></i>');
                $("#divwrap,.nav").show();
                $("#divSpec").hide();
                _inquire.changeSMSTip();
            };
            _inquire.init = function () {
                $("#nav").on("click", function () {
                    $("#divnav").toggle();
                });
                _inquire.btnclear("#txtUserName", "#clearName");
                _inquire.btnclear("#txtMobilePhone", "#clearPhone");
                $.ajax({
                    type: 'GET', dataType: 'json', url: '/Ashx/Dealer/GetListBySeries.ashx?AreaId=' + dealerarea + '&isShowDealer=false', success: function (data) {
                        $("#aCity").attr("data-cid", dealerarea);
                        if (!data) {
                            $('#aCity').html('北京<i class="icon-arrow arrow-bottom"></i>');
                            return;
                        }
                        $('#aCity').html(data.AreaName + '<i class="icon-arrow arrow-bottom"></i>');
                    }
                });

                //选城市
                var citySelModel = new AutoSelectCity({
                    hideEle: "#divwrap,.nav",
                    cityContainer: "#divCity",
                    loadingEle: "#loadingImg",
                    type: citySeleType.area,
                    hasWholeNation: false,
                    hasWholeProvince: false,
                    selectCallback: function (obj) {
                        _data.provinceId = auto.areautil.getProvinceId(obj.totalId || 110100);
                        _data.cityId = auto.areautil.getCityId(obj.totalId || 110100);
                        _data.countyId = auto.areautil.getCountyId(obj.totalId || 110100);
                        var tempHtml = obj.totalName || "北京";

                        _inquire.recomendDealers(_data.cityId, _data.specId);
                        $('#aCity').attr("data-cid", obj.totalId || 110100);
                        $('#aCity').html(tempHtml + '<i class="icon-arrow arrow-bottom"></i>');
                        _inquire.changeSMSTip();
                    }
                });
                $("#aCity").click(function () {
                    citySelModel.show();
                });
                //提交
                $("#aInquire").on("click", function () {
                    var url = "/ashx/dealer/inquire.ashx";
                    var userName = $.trim($("#txtUserName").val());
                    var userPhone = $.trim($("#txtMobilePhone").val());
                    if (!_inquire.validate(_data)) {
                        return false;
                    }

                    $.setCookie('orderusernamenew', userName, { "expireHours": "43200", "domain": "m.autohome.com.cn" });
                    $.setCookie('orderuserphonenew', userPhone, { "expireHours": "43200", "domain": "m.autohome.com.cn" });

                    var dealerids = [];
                    $("input[name='dealerList']").each(function () {
                        if (this.checked) {
                            dealerids.push($(this).attr("dealerId") + "-" + $(this).attr("data-dealerorder"));
                        }
                    })

                    var redirectSpecId = 0;
                    var siteId = dealerids.length == 0 ? 32 : (redirectSpecId > 0 ? 45 : 44);

                    if (dealerids.length == 0) {
                        dealerids.push(_data.dealerId);
                    }

                    var postData = {
                        dealerId: dealerids.join(","),
                        specId: _data.specId,
                        seriesId: _data.seriesId,
                        userName: userName,
                        userPhone: userPhone,
                        cityId: _data.cityId,
                        countyId: _data.countyId,
                        site: 1211004,
                        category: 72,
                        subCategory: 382,
                        url: encodeURIComponent(document.URL),
                        referurl: encodeURIComponent(document.referrer),
                        siteId: siteId,
                        typeid: 2
                    };
                    var param = '';
                    for (var i in postData) {
                        param += "&" + i + "=" + postData[i];
                    }
                    param = param.substr(1, param.length - 1);
                    var self = $(this);
                    $.ajax({
                        type: 'POST',
                        url: url + "?" + param,
                        success: function (data) {
                            var resultData = ShowCommandCarList(_data.cityId, _data.seriesId);

                            _inquire.postComplete(data);
                            $("#loadmask").hide();
                            self.show();
                        },
                        error: function (xhr, type) {
                            _inquire.showErr("您的订单提交失败。");
                            $("#loadmask").hide();
                            self.show();
                        }
                    });
                    self.hide();
                    $("#loadmask").show();
                });
                _inquire.changeSMSTip();
                _inquire.recomendDealers(_data.cityId, _data.specId);
            };
            _inquire.btnclear = function (txt, btn) {
                $(btn).on('click', function () {
                    $(txt).val("");
                    $(btn).addClass("fn-hide");
                });
                $(txt).on('input', function () {
                    if ($(txt).val() == "") {
                        $(btn).addClass("fn-hide");
                    } else {
                        $(btn).removeClass("fn-hide");
                    }
                });
                $(txt).on('focus', function () {
                    if ($(txt).val() == "") {
                        $(btn).addClass("fn-hide");
                    } else {
                        $(btn).removeClass("fn-hide");
                    }
                });
            };
            _inquire.recomendDealers = function (cityid, spid) {
                if (cityid > 0 && spid > 0)
                    $.post("/Ashx/Dealer/GetRecommendDealerList.ashx", { cid: cityid, specid: spid }, function (datas) {
                        if (datas && datas.result && datas.result.list && datas.result.list.length > 0) {
                            var templet = '<label class="selected"><input type="checkbox" #checked name="dealerList" dealerid=#DealerId data-dealerorder=#DealerOrder  /><div><h4><i></i>#CompanySimple</h4><p>地址：#Address</p></div></label>';
                            var arr = [], data = datas.result.list;
                            for (var i = 0, num = data.length > 6 ? 6 : data.length; i < num; i++) {
                                arr.push(templet.replace("#DealerId", data[i].DealerId)
                                                .replace("#CompanySimple", data[i].DealerName)
                                                .replace("#Address", data[i].DealerAddress)
                                                .replace("#checked", i > 2 ? "" : "checked='checked'")
                                                .replace("#KindId", data[i] == 1 ? "综合" : "4S")
                                                .replace("#DealerOrder", i)
                                                );
                            }
                            $('#recommendDealer').html("<h3>请选择经销商，最多可选3家。</h3>" + arr.join(''));
                            _inquire.check();
                        } else
                            $('#recommendDealer').html("");

                    }, "json");
            };
            _inquire.check = function () {
                var select = $("#recommendDealer input[name='dealerList']");
                select.live("click", function () {
                    var selector = 0;
                    select.each(function () {
                        if (this.checked) selector++;
                    });
                    if (selector > 3) {
                        var tips = $("#popup_info");
                        tips.show();
                        setTimeout(function () { tips.hide(); }, 1000);
                        this.checked = false
                    };
                });
            }
            return _inquire;
        }();
        inquire.init();
        var SelectSpecList = {
            callBack: inquire.selectSpec,
            Init: function () {
                var self = this;
                $("#spec_select_nav-mini-btn").click(function () {
                    $("#spec_select_nav_banners").toggle();
                });
                $("#spec_select_nav_back,#spec_select_nav_back_bottom").click(function () {
                    $("#divwrap,.nav").show();
                    $("#divSpec").hide();
                });
                $("#common_specselect_list a").each(function () {
                    $(this).on("click", function () {
                        var targetObj = $(this);
                        self.callBack && self.callBack({ "specId": targetObj.attr("data-id"), "specName": targetObj.text(), "lowPrice": targetObj.attr("data-lowprice") });
                    });
                });
                $("#aSpecInfo").click(function () {
                    if (parseInt('202', 10) <= 0) {
                        return;
                    }
                    $("#divwrap,.nav").hide();
                    $("#divSpec").show();
                });
            }
        }
        SelectSpecList.Init();
        var ucName = $.getCookie("orderusernamenew");
        var ucPhone = $.getCookie("orderuserphonenew");
        if (ucName) {
            $("#txtUserName").val(ucName);
        }
        if (ucPhone) {
            $("#txtMobilePhone").val(ucPhone);
        }

        function ShowCommandCarList(cityId, seriesId) {
            $.ajax({

                type: 'GET',
                url: "/Ashx/Dealer/GetCommerList.ashx?cityId=" + cityId + "&seriesId=" + seriesId,
                success: function (data) {
                    var result = GetCommandList(data, cityId);
                    $("#divFuwuquTip").html(result);
                }
            });
        }

        function GetCommandList(data, cityId) {
            if (!data) return "";
            var dataJson = data
            var count = 0;
            var result = "";
            if (dataJson.result.list < 1) return result;
            result += "<div class=\"title01\"><h3>您可能感兴趣的其他车</h3></div>";
            result += "<ul class=\"list-inforimg02\">";
            for (var i = 0; i < dataJson.result.list.length; i++) {
                if (count > 3) break;
                var SeriesName = dataJson.result.list[i].SeriesName;
                if (dataJson.result.list[i].SeriesName.length > 10) {
                    var SeriesName = dataJson.result.list[i].SeriesName.substring(0, 9) + "..";
                }
                var imagesUrl = dataJson.result.list[i].SeriesImage.replace('l_', 's_');
                var link = "http://buy.m.autohome.com.cn/" + dataJson.result.list[i].BrandId + "/" + dataJson.result.list[i].SeriesId + "/0/0/0/0-1-1-1.html";
                result += "<li>";
                result += "<a href='" + link + "'>";
                result += "<img src='" + imagesUrl + "'>";
                result += "<span>" + SeriesName + "<br><em><dfn>&darr;</dfn>" + Number((dataJson.result.list[i].MaxPriceOff / 10000).toFixed(2)) + "万</em><b>" + Number((dataJson.result.list[i].MinPrice / 10000).toFixed(2)) + "万</b></span>";
                result += "</a></li>";
                count++;
            }
            result += "</ul>";
            result += "<div class=\"fn-divi\"></div>";
            return result;
        }

    </script>
    <script type="text/javascript">
        var pvTrack = function () { };
        pvTrack.site = 1211004;;
        pvTrack.category = 72;
        pvTrack.subcategory = 382
        pvTrack.object = 0;
        pvTrack.series = 202;
        pvTrack.type = 0;
        pvTrack.typeid = 0;
        pvTrack.spec = 0;
        pvTrack.level = 0;
        pvTrack.dealer = 0;
    </script>
    <script language="javascript" type="text/javascript">
        var url_stats = "http://stats.autohome.com.cn/pv_count.php?SiteId=";
        (function () {
            if (typeof (pvTrack) !== "undefined") {
                document.write('<iframe id="PageView_stats" src="" style="display:none;"></iframe>');
                setTimeout("func_stats()", 3000);
                var doc = document, t = pvTrack;
                var pv_site, pv_category, pv_subcategory, pv_object, pv_series, pv_type, pv_typeid, pv_spec, pv_level, pv_dealer, pv_ref, pv_cur;
                pv_ref = escape(doc.referrer); pv_cur = escape(doc.URL);
                pv_site = t.site; pv_category = t.category; pv_subcategory = t.subcategory; pv_object = t.object; pv_series = t.series; pv_type = t.type; pv_typeid = t.typeid; pv_spec = t.spec; pv_level = t.level; pv_dealer = t.dealer;
                url_stats += pv_site + (pv_category != null ? "&CategoryId=" + pv_category : "") + (pv_subcategory != null ? "&SubCategoryId=" + pv_subcategory : "") + (pv_object != null ? "&objectid=" + pv_object : "") + (pv_series != null ? "&seriesid=" + pv_series : "") + (pv_type != null ? "&type=" + pv_type : "") + (pv_typeid != null ? "&typeid=" + pv_typeid : "") + (pv_spec != null ? "&specid=" + pv_spec : "") + (pv_level != null ? "&jbid=" + pv_level : "") + (pv_dealer != null ? "&dealerid=" + pv_dealer : "") + "&ref=" + pv_ref + "&cur=" + pv_cur + "&rnd=" + Math.random();
                var len_url_stats = url_stats.length;
            }
        })();

        function func_stats() { document.getElementById('PageView_stats').src = url_stats; }

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-30787185-1']);
        _gaq.push(['_addOrganic', 'm.baidu.com', 'word']);
        _gaq.push(['_setDomainName', '.autohome.com.cn']);
        _gaq.push(['_setAllowHash', false]);
        _gaq.push(['_setSampleRate', '10']);
        if (typeof (Worker) !== "undefined") {
            _gaq.push(['_setCustomVar', 1, 'User', 'Html5', 1]);
        }
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

        var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
        document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3Fc3d5d12c0100a78dd49ba1357b115ad7' type='text/javascript'%3E%3C/script%3E"));

    </script>
</body>
</html>
