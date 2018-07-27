<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarBrand.aspx.cs" Inherits="wechat_Car_CarBrand" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-itunes-app" content="app-id=385919493" />
    <link href="/wechatcss/comm/public_W01.css" rel="stylesheet" />
    <%--<script src="/wechatjs/NewZepto.js"></script>--%>
    <script src="/wechatjs/zepto.js"></script>
    <link href="/wechatcss/comm/findcar_W01.css" rel="stylesheet" />
    <title></title>
</head>

<body>
    <style>
        .w-header {
            height: 44px;
            padding: 0 10px;
            font-family: STHeiti,Arial;
        }

        .w-header-logo {
            float: left;
            margin: 8px 0 0;
        }

            .w-header-logo img {
                width: auto;
                height: 28px;
            }

        .w-fn-xab {
            float: right;
            margin: 8px 0 0 15px;
            background: #5f7dbe;
            padding: 6px 8px;
            font-size: 14px;
            color: #fff;
            line-height: 16px;
            text-decoration: none;
            border-radius: 3px;
        }

            .w-fn-xab:visited {
                color: #fffffe;
            }

        .w-header-login {
            float: right;
            margin: 18px 0 0;
            font-size: 14px;
            color: #3b5998;
            line-height: 16px;
            text-decoration: none;
        }

            .w-header-login:visited {
                color: #3b5997;
            }

        .w-header-user {
            float: right;
            width: 28px;
            height: 28px;
            margin: 8px 0 0;
            color: #fff;
            background: #5f7dbe url("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACYAAAAoCAMAAACl6XjsAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOkNDRTYxM0E3MzMwMTExRTNCM0M1ODZFRDA1RTA1N0JGIiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOkNDRTYxM0E4MzMwMTExRTNCM0M1ODZFRDA1RTA1N0JGIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6Q0NFNjEzQTUzMzAxMTFFM0IzQzU4NkVEMDVFMDU3QkYiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6Q0NFNjEzQTYzMzAxMTFFM0IzQzU4NkVEMDVFMDU3QkYiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz5mW81GAAAA81BMVEVffb7///+AmMv+/v5gfr9visW6x+PJ0+m1w+GKoM9jgMBlgsHY3+/AzObx9PmNo9HM1erH0ejw8/mRptJif7+WqtTEz+ft8fj8/f7j6fRng8G8yeTo7faUqNOktdr7/P1yjMbc4vHW3e6esNd5ksmDm827yOOardaVqdSCmsybrtbN1uuQpdKcr9eSp9P6+/17k8nF0OjCzeZohML5+vxuicRtiMSBmcytvN72+PuPpNHZ4PDI0umHns709vrl6vT3+fy0wuC2xOGIn89kgcCMotDq7vaouNxxi8WXq9Vsh8O/y+WxwN/m6/Xr7/fy9frv8vhphI9NAAABG0lEQVR42ozUZ3OCQBCA4VsOlABiL0k0aqoxvfeY3tv//zUxo5dZdvYW3s/PDNyxi1KJgqK/EUXlrVBJDZsw6dMTVA7+0/0Zm3oFnO7wyn1KMPieY9k6kOosW6Asx7I7yvJp55xUYtktZfvZ3u2UZY+UnbPMI0of8/fbTLJZXm3Th16xzKfsmWVLlFVZdnSTVDH/6dVJBav2rm3gLjGbt07vADPr9KrgC92aa1+GojaqdS+tVt6whriBDcMKItvLxkqGlSVVaxs2Eg5ae0drZXUrfXy9Fw5nHuoRnZDFTkBQz4+B6ecD/5m8QgssdVffjKpeg1BlOgNrGsS6m39qeQdSOnvBX9veeELDw3QWu8qBDPXUgZOh8FeAAQAyIhIIZaFxtwAAAABJRU5ErkJggg==") no-repeat center;
            background-size: 18px auto;
            position: relative;
            border-radius: 3px;
        }

            .w-header-user:visited {
                color: #fffffe;
            }

            .w-header-user i {
                display: block;
                width: 18px;
                height: 18px;
                background: #f84949;
                font-size: 10px;
                font-style: normal;
                line-height: 18px;
                text-align: center;
                position: absolute;
                top: -6px;
                right: -6px;
                border-radius: 10px;
            }

        .w-header-switch {
            z-index: 1000;
            float: left;
            margin: 17px 0 0 10px;
            position: relative;
        }

        .w-header-switch-btn {
            display: inline-block;
            font-size: 14px;
            color: #777d8b;
            line-height: 16px;
            text-decoration: none;
        }

            .w-header-switch-btn:visited {
                color: #777d8a;
            }

            .w-header-switch-btn i {
                display: inline-block;
                width: 0;
                height: 0;
                margin: 0 0 0 2px;
                border: solid 5px;
                border-color: rgba(119,125,139,1) transparent transparent;
                vertical-align: -4px;
            }

        .w-header-switch-pop {
            width: 90px;
            padding: 6px 0;
            position: absolute;
            top: 35px;
            left: -25px;
            background: rgba(0,0,0,0.8);
            border-radius: 3px;
        }

            .w-header-switch-pop a {
                display: block;
                margin: 0;
                padding: 10px 0;
                font-size: 16px;
                color: #fff;
                line-height: 18px;
                text-decoration: none;
                text-align: center;
            }

                .w-header-switch-pop a:visited {
                    color: #fffffe;
                }

            .w-header-switch-pop i {
                display: block;
                width: 0;
                height: 0;
                border: solid 9px;
                border-color: transparent transparent rgba(0,0,0,0.8);
                position: absolute;
                top: -18px;
                left: 50%;
                margin-left: -9px;
            }
    </style>
    <script type="text/javascript" language="javascript">
        (function (win, doc, header_app) {
            var hApp = doc.getElementById("loginuserapp").previousSibling; hApp.className = header_app; var Valite = { GetCookie: function (name) { var acookie = doc.cookie.split("; "), i = 0, len = acookie.length, arr; for (; i < len;) { arr = acookie[i++].split("="); if (name == arr[0] && arr.length > 1) return arr } return null } };
            var userId = 0;
            (function () {
                var clubUserShow = Valite.GetCookie("clubUserShow"), loginuser = doc.getElementById('loginuser'), loginuserapp = doc.getElementById('loginuserapp');
                var reg = /[1-9]{1,}\d{0,}?$/;
                if (clubUserShow == null || clubUserShow.length < 1) { loginuser.style.display = "block"; loginuser.href = "http://account.m.autohome.com.cn/?backurl=" + location.href; return; } else { var regUserId = reg.exec(clubUserShow[1].split('|')[0]); if (regUserId.length == 0) { return; } else { userId = regUserId[0]; loginuserapp.style.display = "block"; GetHeadImg(); } }
            })();

            function loadScript(b, c) { var a = doc.createElement('script'); a.type = 'text/javascript'; if (a.readyState) { a.onreadystatechange = function () { if (a.readyState == 'loaded' || a.readyState == 'complete') { a.onreadystatechange = null; c() } } } else { a.onload = function () { c() } } a.src = b; doc.getElementsByTagName('head')[0].appendChild(a) };

            win.getUserImgCallback = function (a) { if (a == undefined || a.returncode != 0 || a.headimageurl30.indexOf('head_30X30') >= 0) { doc.getElementById('loginuserapp').setAttribute('class', 'w-header-user w-header-user-null'); doc.getElementById('headImg').style.display = 'none'; return } else { doc.getElementById('loginuserapp').setAttribute('class', 'w-header-user'); doc.getElementById('headImg').style.display = 'block'; doc.getElementById('headImg').src = a.headimageurl30 } msgcallback(a); setInterval(GetUnreadMessage, 30000) };

            function GetHeadImg() { if (userId <= 0) { return; } loadScript('http://i.api.autohome.com.cn/apiajax/AjaxNewsMsg/GetMessageByHeadImage?_appid=i&userid=' + userId + '&callback=getUserImgCallback&timestamp=' + (new Date()).getTime(), function (json) { }); }
            if (/iPhone|iPad/ig.test(navigator.userAgent)) { hApp.href = 'http://itunes.apple.com/cn/app/id708985992?mt=8'; hApp.innerHTML = '违章App'; } else if (/Windows Phone/ig.test(navigator.userAgent)) { hApp.style.display = 'none'; };
            win.ChangeTypeOnClick = function () { if (doc.getElementById('vtype').style.display == 'none') { doc.getElementById('vtype').style.display = ''; } else { doc.getElementById('vtype').style.display = 'none'; } };
            win.msgcallback = function (validate) { var messageCount = doc.getElementById('messageCount'); if (validate == undefined) { return } var count = 0; count = validate.total; if (count > 0) { messageCount.style.display = 'block'; messageCount.innerHTML = (count > 99 ? '99+' : count) } else { messageCount.style.display = 'none' } }; win.GetUnreadMessage = function () { if (userId <= 0) { return } loadScript('http://msg.autoimg.cn/clubalert?callback=msgcallback&uid=' + userId + '&timestamp=' + (new Date()).getTime(), function (json) { }) }
        })(window, document, 'w-fn-xab')
    </script>
    <div class="nav">
        <h2 class="nav-title">宝马</h2>
        <a class="nav-back" href="/"><i class="icon-arrow arrow-left"></i><span>返回</span></a>
    </div>
    <div class="main">
        <div class="title01">
            <h3>华晨宝马</h3>
        </div>
        <ul class="list-infor01">
            <li class="js-mall66"><a href="CarStyle.aspx"><span class="caption">宝马3系</span><span class="pricenorm">28.30-60.78万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall65"><a href="CarStyle.aspx"><span class="caption">宝马5系</span><span class="pricenorm">43.56-77.86万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2561"><a href="CarStyle.aspx"><span class="caption">宝马X1</span><span class="pricenorm">25.90-43.60万</span><i class="icon-arrow arrow-right"></i></a></li>
        </ul>
        <div class="title01">
            <h3>宝马(进口)</h3>
        </div>
        <ul class="list-infor01">
            <li class="js-mall2388"><a href="CarStyle.aspx"><span class="caption">宝马i3</span><span class="pricenorm">44.98-51.68万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall373"><a href="CarStyle.aspx"><span class="caption">宝马1系</span><span class="pricenorm">24.20-50.50万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall317"><a href="CarStyle.aspx"><span class="caption">宝马3系(进口)</span><span class="pricenorm">39.96-67.00万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2963"><a href="CarStyle.aspx"><span class="caption">宝马3系GT</span><span class="pricenorm">44.50-67.30万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2968"><a href="CarStyle.aspx"><span class="caption">宝马4系</span><span class="pricenorm">48.00-80.80万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall202"><a href="CarStyle.aspx"><span class="caption">宝马5系(进口)</span><span class="pricenorm">45.70-87.90万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2847"><a href="CarStyle.aspx"><span class="caption">宝马5系GT</span><span class="pricenorm">73.80-170.00万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall270"><a href="CarStyle.aspx"><span class="caption">宝马6系</span><span class="pricenorm">113.90-201.80万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall153"><a href="CarStyle.aspx"><span class="caption">宝马7系</span><span class="pricenorm">93.35-270.35万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall271"><a href="CarStyle.aspx"><span class="caption">宝马X3</span><span class="pricenorm">47.90-75.00万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall3053"><a href="CarStyle.aspx"><span class="caption">宝马X4</span><span class="pricenorm">55.20-77.40万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall159"><a href="CarStyle.aspx"><span class="caption">宝马X5</span><span class="pricenorm">85.28-177.30万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall587"><a href="CarStyle.aspx"><span class="caption">宝马X6</span><span class="pricenorm">98.36-216.80万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall3230"><a href="CarStyle.aspx"><span class="caption">宝马2系</span><span class="pricenorm">32.00-51.70万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2387"><a href="CarStyle.aspx"><span class="caption">宝马i8</span><span class="pricenorm">198.80万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall161"><a href="CarStyle.aspx"><span class="caption">宝马Z4</span><span class="pricenorm">58.30-90.90万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall675"><a href="CarStyle.aspx"><span class="caption">宝马X1(进口)</span><span class="pricenorm">暂无报价</span><i class="icon-arrow arrow-right"></i></a></li>
        </ul>
        <div class="title01">
            <h3>宝马M</h3>
        </div>
        <ul class="list-infor01">
            <li class="js-mall2196"><a href="CarStyle.aspx"><span class="caption">宝马M3</span><span class="pricenorm">99.80-129.70万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall3189"><a href="CarStyle.aspx"><span class="caption">宝马M4</span><span class="pricenorm">97.60-113.00万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2726"><a href="CarStyle.aspx"><span class="caption">宝马M5</span><span class="pricenorm">178.80-197.80万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2727"><a href="CarStyle.aspx"><span class="caption">宝马M6</span><span class="pricenorm">230.60-276.80万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2728"><a href="CarStyle.aspx"><span class="caption">宝马X5 M</span><span class="pricenorm">210.70万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2729"><a href="CarStyle.aspx"><span class="caption">宝马X6 M</span><span class="pricenorm">223.80万</span><i class="icon-arrow arrow-right"></i></a></li>
            <li class="js-mall2725"><a href="CarStyle.aspx"><span class="caption">宝马1系M</span><span class="mark-salestop">停售</span><span class="pricenorm">暂无报价</span><i class="icon-arrow arrow-right"></i></a></li>
        </ul>
    </div>
    <div class="nav-sub">
        <h2 class="nav-title">宝马</h2>
        <a class="nav-back" href="/"><i class="icon-arrow arrow-left"></i><span>返回</span></a>
    </div>

    <script language="javascript" type="text/javascript">

        var pvTrack = function () { };
        pvTrack.site = 1211002; pvTrack.category = 14; pvTrack.subcategory = 122;
        var url_stats = "http://stats.autohome.com.cn/pv_count.php?SiteId=";
        (function () {
            if (typeof (pvTrack) !== "undefined") {
                setTimeout("func_stats()", 3000);
                var doc = document, t = pvTrack;
                var pv_site, pv_category, pv_subcategory, pv_object, pv_series, pv_type, pv_typeid, pv_spec, pv_level, pv_dealer, pv_ref, pv_cur;
                pv_ref = escape(doc.referrer); pv_cur = escape(doc.URL);
                pv_site = t.site; pv_category = t.category; pv_subcategory = t.subcategory; pv_object = t.object; pv_series = t.series; pv_type = t.type; pv_typeid = t.typeid; pv_spec = t.spec; pv_level = t.level; pv_dealer = t.dealer;
                url_stats += pv_site + (pv_category != null ? "&CategoryId=" + pv_category : "") + (pv_subcategory != null ? "&SubCategoryId=" + pv_subcategory : "") + (pv_object != null ? "&objectid=" + pv_object : "") + (pv_series != null ? "&seriesid=" + pv_series : "") + (pv_type != null ? "&type=" + pv_type : "") + (pv_typeid != null ? "&typeid=" + pv_typeid : "") + (pv_spec != null ? "&specid=" + pv_spec : "") + (pv_level != null ? "&jbid=" + pv_level : "") + (pv_dealer != null ? "&dealerid=" + pv_dealer : "") + "&ref=" + pv_ref + "&cur=" + pv_cur + "&rnd=" + Math.random();
                var len_url_stats = url_stats.length;
            }
        })();
        document.write('<iframe id="PageView_stats" src="" style="display:none;"></iframe>');
        function func_stats() { document.getElementById('PageView_stats').src = url_stats; }

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-30787185-1']);
        _gaq.push(['_addOrganic', 'm.baidu.com', 'word']);
        _gaq.push(['_setDomainName', '.autohome.com.cn']);
        _gaq.push(['_setAllowHash', false]);
        _gaq.push(['_setSampleRate', '10']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

        var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
        document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3Fc3d5d12c0100a78dd49ba1357b115ad7' type='text/javascript'%3E%3C/script%3E"));

    </script>
    <script type="text/javascript">
        (function () {
            $("#nav").on("click", function () {
                $("#divnav").toggle();
            });
        })();

        $(document).ready(function () {
            var cookieCityId = parseInt($.getCookie("cookieCityId", "0"));
            if (cookieCityId == 0) {
                cookieCityId = parseInt($.getCookie("area", "0"));
            }
            if (cookieCityId == 0) {
                cookieCityId = 110100;
            }
            cookieCityId = parseInt(cookieCityId / 100) * 100;
            var brandId = 15;

            $.ajaxJSONP({
                url: "http://api.mall.autohome.com.cn/gethomead/" + cookieCityId + "?_appid=car&callback=?",
                jsonpCallback: "loadMalljsonp",
                success: function (data) {
                    if (!(data == null || data.returncode != 0 || data.result == null || data.result.list == null || data.result.list.length == 0)) {
                        $.each(data.result.list, function (i, item) {
                            $(".js-mall" + item.seriesId).addClass("find-mallport").append("<a class=\"btn btn-small-orange\" href=\"" + item.url + "\">特价购买</a><span class=\"mallport-info\"><b>限时特价</b>，点击进入车商城特价活动</span>");
                        });
                    }
                    cookieCityId > 0 && getSuperBuy();
                }
            });

            function getSuperBuy() {
                $.ajaxJSONP({
                    url: "http://car.interface.autohome.com.cn/Mweb/GetSuperBuySeriesByBrand.ashx?_callback=?",
                    data: {
                        cityid: cookieCityId,
                        brandid: brandId
                    },
                    jsonpCallback: "loadSuperBuyjsonp",
                    success: function (data) {
                        if (data == null || data.returncode != 0 || data.result == null || data.result.length == 0) {
                            return;
                        }
                        $.each(data.result, function (i, item) {
                            $(".js-mall" + item.seriesId).hasClass('find-mallport') === false
                            && $(".js-mall" + item.seriesId).addClass("find-mallport")
                                                           .append("<a class=\"btn btn-small-orange\" href=\"" + item.MUrl + "#pvareaid=103719\">参与团购</a><span class=\"mallport-info\">团购最高优惠 <b>" + item.MaxAmount + "</b></span>");
                        });
                    }
                });
            }
        });
    </script>

    <script type="text/javascript">
        ; (function () {
            function ReplaceTemplate(D, T) { function f(d, t) { var sb = t, b = null; for (var o in d) { b = new RegExp('{' + o + '}', 'gi'); sb = sb.replace(b, '' + d[o]) } return sb } var SB = ''; if (Object.prototype.toString.call(D) == '[object Array]') { for (var i = 0; i < D.length; i++) { SB += arguments.callee(D[i], T) } } else { SB = D == '' ? '' : f(D, T) } return SB };
            var u = navigator.userAgent;
            function getHeaderHTML() {
                var headerDate = { "recommendid": 15, "rchannelId": 30, "randroidappname": "\u6C7D\u8F66\u4E4B\u5BB6", "randroidapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=824&rd=http%3A%2F%2Fapp.autohome.com.cn%2Fdownload%2Fautohome_m_head_car.apk", "randroidappimg": "http://x.autoimg.cn/m/news/logo/autohome_80X80.jpg", "riphoneappname": "\u6C7D\u8F66\u4E4B\u5BB6", "riphoneapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=811&rd=https%3a%2f%2fitunes.apple.com%2fcn%2fapp%2fqi-che-zhi-jia%2fid385919493%3fmt%3d8", "riphoneappimg": "http://x.autoimg.cn/m/news/logo/autohome_80X80.jpg", "rstatus": 1, "randroidappcomment": "\u6700\u65B0\u8F66\u8BAF\uFF0C\u4E00\u89E6\u5373\u8FBE", "riphoneappcomment": "\u6700\u65B0\u8F66\u8BAF\uFF0C\u4E00\u89E6\u5373\u8FBE" }
                var cookiename = ["bileH_2M0_e", "2"]; var snum = cookiename[0].split("_");
                var r = snum[1].match(/\d+(\.\d+)?/g), value = parseInt(r[0]) + parseInt(r[1]) * 5;
                var classname = 'ivi9yg14_';
                var iscookie = $.getCookie(cookiename[0], '0');
                if ((iscookie == cookiename[1] && value == cookiename[1]) || document.URL.toLowerCase().indexOf("from=qq") > 0 || document.URL.toLowerCase().indexOf("from=360") > 0) {
                    return;
                }
                var css = '.' + classname + '{height:50px;background-color:#f9f9f9;position:relative;}.' + classname + ' a{display:block;height:36px;padding:7px 0 7px 12px;margin-right:40px;color:#666;line-height:1.0;overflow:hidden;position:relative;}.' + classname + ' a:visited{color:#666665;}.' + classname + ' img{float:left;width:36px;height:36px;margin-right:9px;}.' + classname + ' h1{margin-top:2px;font-size:16px;font-weight:bold;}.' + classname + ' p{margin-top:4px;font-size:12px;color:#939393;}.' + classname + ' span{width:62px;height:20px;border:solid #2f75bb 1px;border-radius:2px;font-size:12px;color:#2f75bb;line-height:20px;text-align:center;position:absolute;top:14px;right:8px;}.' + classname + ' i{padding:17px 12px;position:absolute;top:0;right:0;}.' + classname + ' i::before{content:"";display:inline-block;width:16px;height:16px;background:url(http://s.autoimg.cn/mass/v1/img/icon-app-close.png) no-repeat;background-size:16px auto;vertical-align:top;}'
                var html = "";
                if (headerDate == null || headerDate == "") { return; }
                if ((u.indexOf('Mac') > -1 && u.indexOf('iPhone') < 0) || u.indexOf('iPad') > -1) {
                    return;
                }
                if (u.indexOf('iPhone') > -1) {
                    if (u.indexOf('Safari') > -1 && (u.indexOf('Chrome') < 0 && u.indexOf('CriOS') < 0 && u.indexOf('MQQBrowser') < 0)) {
                        return;
                    }
                    html += '<section class="' + classname + '"><a href="{riphoneapphref}"><img src="{riphoneappimg}" /><h1>{riphoneappname}</h1><p>{riphoneappcomment}</p><span>免费下载</span></a><i id="headerClose"></i></section>';
                } else {
                    if (u.indexOf('JUC') > -1 || u.indexOf('UCBrowser') > -1) {
                        return;
                    }
                    html += '<section class="' + classname + '"><a href="{randroidapphref}"><img src="{randroidappimg}" /><h1>{randroidappname}</h1><p>{randroidappcomment}</p><span>免费下载</span></a><i id="headerClose"></i></section>';
                }
                if (html == '') { return; }
                var lstyle = document.createElement("style");
                lstyle.type = "text/css";
                lstyle.appendChild(document.createTextNode(css));
                $($('body').children()[0]).before(ReplaceTemplate(headerDate, html));
                document.getElementsByTagName("head")[0].appendChild(lstyle);
                $('#headerClose').on('click', function () {
                    $('.' + classname).remove();
                    $.setCookie(cookiename[0], cookiename[1], { domain: 'm.autohome.com.cn', expireHours: 24 * 7 });
                });
            }
            getHeaderHTML();
        })();
    </script>
</body>
</html>
