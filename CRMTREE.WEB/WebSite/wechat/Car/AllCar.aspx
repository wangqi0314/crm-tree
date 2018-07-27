<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllCar.aspx.cs" Inherits="wechat_Car_AllCar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-itunes-app" content="app-id=385919493" />
    <title></title>
    <link href="/wechatcss/comm/public_W01.css" rel="stylesheet" />
    <%--<script src="http://x.autoimg.cn/m/js/v1.0.2/zepto.min.js" type="text/javascript"></script>--%>
    <%--<script src="/wechatjs/NewZepto.js"></script>--%>
    <script src="/wechatjs/zepto.js"></script>
  <%--  <link href="http://x.autoimg.cn/car/css/M/findcar/findcar2014061301.css" rel="stylesheet" />--%>
    <link href="/wechatcss/comm/findcar_W01.css" rel="stylesheet" />
</head>
<body>
    <div class="nav">
        <div class="nav-tab">
            <%--  <a href="http://m.autohome.com.cn/">首页</a>--%>
            <a class="current" href="/">找车</a>
            <%-- <a href="http://m.che168.com?pvareaid=100562">二手车</a>--%>
            <%--      <a href="http://k.m.autohome.com.cn?pvareaid=103507">口碑</a>--%>
            <%--<a href="http://club.m.autohome.com.cn">论坛</a>--%>
        </div>
    </div>
    <div class="main">
        <div id="ad_640_1" style="display: none" class="adv05 fn-mt fn-mb">
        </div>
        <div class="nav-channel nav-channel-col2" style="display: none">
            <ul>
                <li><a class="selected" href="javascript:void(0);"><span>按品牌找车<i class="arrow-top"></i></span></a></li>
                <li class="last"><a href="/price/list-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-1.html"><span>按条件找车<i class="arrow-top"></i></span></a></li>
            </ul>
        </div>
        <div class="find-siftanchor">
            <h3>请按拼音首字母选品牌</h3>
            <ul>
                <li>
                    <a href="#brandA" class="btn btn-big">A<i class="spa"></i>B<i class="spa"></i>C</a></li>
                <li>
                    <a href="#brandD" class="btn btn-big">D<i class="spa"></i>E<i class="spa"></i>F<i class="spa"></i>G</a></li>
                <li>
                    <a href="#brandH" class="btn btn-big">H<i class="spa"></i>I<i class="spa"></i>J<i class="spa"></i>K</a></li>
                <li>
                    <a href="#brandL" class="btn btn-big">L<i class="spa"></i>M<i class="spa"></i>N</a></li>
                <li>
                    <a href="#brandO" class="btn btn-big">O<i class="spa"></i>P<i class="spa"></i>Q<i class="spa"></i>R<i class="spa"></i>S</a></li>
                <li>
                    <a href="#brandT" class="btn btn-big">T<i class="spa"></i>W<i class="spa"></i>X<i class="spa"></i>Y<i class="spa"></i>Z</a></li>
                <li style="display:none">
                    <a href="javascript:void(0);" data-name="s" id="l_history" class="btn btn-big btn-disabled">我浏览的</a></li>
                <li>
                    <a href="javascript:void(0);" id="l_hotbrand" class="btn btn-big btn-selected">热门品牌</a></li>
            </ul>
        </div>
        <div class="title01 fn-mt fn-hide" id="d_history" data-name="s" data-type="history">
            <h3>我浏览的</h3>
            <div class="fold">展开<i class="arrow-bottom"></i></div>
        </div>
        <ul class="list-infor01 fn-hide" id="u_history" data-type="history">
        </ul>
        <div class="title01 fn-mt" data-type="brand">
            <h3>热门品牌</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx">大众</a><a href="CarBrand.aspx">丰田</a><a href="CarBrand.aspx">现代</a></div>
            <div class="brandbox"><a href="CarBrand.aspx">本田</a><a href="CarBrand.aspx">福特</a><a href="CarBrand.aspx">别克</a></div>
            <div class="brandbox"><a href="CarBrand.aspx">雪佛兰</a><a href="CarBrand.aspx">起亚</a><a href="CarBrand.aspx">长安</a></div>
            <div class="brandbox"><a href="CarBrand.aspx">奥迪</a><a href="CarBrand.aspx">奔驰</a><a href="CarBrand.aspx">日产</a></div>
            <div class="brandbox"><a href="CarBrand.aspx">比亚迪</a><a href="CarBrand.aspx">长城</a><a href="CarBrand.aspx">宝马</a></div>
            <div class="brandbox"><a href="CarBrand.aspx">哈弗</a><a href="CarBrand.aspx">马自达</a><a href="CarBrand.aspx">吉利汽车</a></div>
            <div class="brandbox"><a href="CarBrand.aspx">标致</a><a href="CarBrand.aspx">奇瑞</a></div>
        </div>

        <div class="title01 fn-mt" id="brandA" name="brandA">
            <h3>A</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx">阿尔法罗密欧</a><a href="CarBrand.aspx">阿斯顿·马丁</a><a href="CarBrand.aspx">安凯客车</a></div>
            <div class="brandbox"><a href="CarBrand.aspx">奥迪</a></div>
        </div>

        <div class="title01 " id="brandB" name="brandB">
            <h3>B</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx140?pvareaid=103225">巴博斯</a><a href="CarBrand.aspx120?pvareaid=103225">宝骏</a><a href="CarBrand.aspx">宝马</a></div>
            <div class="brandbox"><a href="CarBrand.aspx40?pvareaid=103225">保时捷</a><a href="CarBrand.aspx27?pvareaid=103225">北京汽车</a><a href="CarBrand.aspx203?pvareaid=103225">北汽幻速</a></div>
            <div class="brandbox"><a href="CarBrand.aspx143?pvareaid=103225">北汽威旺</a><a href="CarBrand.aspx208?pvareaid=103225">北汽新能源</a><a href="CarBrand.aspx154?pvareaid=103225">北汽制造</a></div>
            <div class="brandbox"><a href="CarBrand.aspx36?pvareaid=103225">奔驰</a><a href="CarBrand.aspx95?pvareaid=103225">奔腾</a><a href="CarBrand.aspx14?pvareaid=103225">本田</a></div>
            <div class="brandbox"><a href="CarBrand.aspx75?pvareaid=103225">比亚迪</a><a href="CarBrand.aspx13?pvareaid=103225">标致</a><a href="CarBrand.aspx38?pvareaid=103225">别克</a></div>
            <div class="brandbox"><a href="CarBrand.aspx39?pvareaid=103225">宾利</a><a href="CarBrand.aspx37?pvareaid=103225">布加迪</a></div>
        </div>

        <div class="title01 " id="brandC" name="brandC">
            <h3>C</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx79?pvareaid=103225">昌河</a><a href="CarBrand.aspx76?pvareaid=103225">长安</a><a href="CarBrand.aspx163?pvareaid=103225">长安商用</a></div>
            <div class="brandbox"><a href="CarBrand.aspx77?pvareaid=103225">长城</a><a href="CarBrand.aspx196?pvareaid=103225">成功汽车</a></div>
        </div>

        <div class="title01 " id="brandD" name="brandD">
            <h3>D</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx169?pvareaid=103225">DS</a><a href="CarBrand.aspx92?pvareaid=103225">大发</a><a href="CarBrand.aspx1?pvareaid=103225">大众</a></div>
            <div class="brandbox"><a href="CarBrand.aspx41?pvareaid=103225">道奇</a><a href="CarBrand.aspx32?pvareaid=103225">东风</a><a href="CarBrand.aspx187?pvareaid=103225">东风风度</a></div>
            <div class="brandbox"><a href="CarBrand.aspx113?pvareaid=103225">东风风神</a><a href="CarBrand.aspx165?pvareaid=103225">东风风行</a><a href="CarBrand.aspx142?pvareaid=103225">东风小康</a></div>
            <div class="brandbox"><a href="CarBrand.aspx81?pvareaid=103225">东南</a></div>
        </div>

        <div class="title01 " id="brandF" name="brandF">
            <h3>F</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx42?pvareaid=103225">法拉利</a><a href="CarBrand.aspx11?pvareaid=103225">菲亚特</a><a href="CarBrand.aspx3?pvareaid=103225">丰田</a></div>
            <div class="brandbox"><a href="CarBrand.aspx141?pvareaid=103225">福迪</a><a href="CarBrand.aspx197?pvareaid=103225">福汽启腾</a><a href="CarBrand.aspx8?pvareaid=103225">福特</a></div>
            <div class="brandbox"><a href="CarBrand.aspx96?pvareaid=103225">福田</a></div>
        </div>

        <div class="title01 " id="brandG" name="brandG">
            <h3>G</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx112?pvareaid=103225">GMC</a><a href="CarBrand.aspx152?pvareaid=103225">观致</a><a href="CarBrand.aspx116?pvareaid=103225">光冈</a></div>
            <div class="brandbox"><a href="CarBrand.aspx82?pvareaid=103225">广汽传祺</a><a href="CarBrand.aspx108?pvareaid=103225">广汽吉奥</a></div>
        </div>

        <div class="title01 " id="brandH" name="brandH">
            <h3>H</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx24?pvareaid=103225">哈飞</a><a href="CarBrand.aspx181?pvareaid=103225">哈弗</a><a href="CarBrand.aspx150?pvareaid=103225">海格</a></div>
            <div class="brandbox"><a href="CarBrand.aspx86?pvareaid=103225">海马</a><a href="CarBrand.aspx43?pvareaid=103225">悍马</a><a href="CarBrand.aspx164?pvareaid=103225">恒天</a></div>
            <div class="brandbox"><a href="CarBrand.aspx91?pvareaid=103225">红旗</a><a href="CarBrand.aspx85?pvareaid=103225">华普</a><a href="CarBrand.aspx220?pvareaid=103225">华颂</a></div>
            <div class="brandbox"><a href="CarBrand.aspx87?pvareaid=103225">华泰</a><a href="CarBrand.aspx97?pvareaid=103225">黄海</a></div>
        </div>

        <div class="title01 " id="brandJ" name="brandJ">
            <h3>J</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx46?pvareaid=103225">Jeep</a><a href="CarBrand.aspx25?pvareaid=103225">吉利汽车</a><a href="CarBrand.aspx84?pvareaid=103225">江淮</a></div>
            <div class="brandbox"><a href="CarBrand.aspx119?pvareaid=103225">江铃</a><a href="CarBrand.aspx210?pvareaid=103225">江铃集团轻汽</a><a href="CarBrand.aspx44?pvareaid=103225">捷豹</a></div>
            <div class="brandbox"><a href="CarBrand.aspx83?pvareaid=103225">金杯</a><a href="CarBrand.aspx145?pvareaid=103225">金龙</a><a href="CarBrand.aspx175?pvareaid=103225">金旅</a></div>
            <div class="brandbox"><a href="CarBrand.aspx151?pvareaid=103225">九龙</a></div>
        </div>

        <div class="title01 " id="brandK" name="brandK">
            <h3>K</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx109?pvareaid=103225">KTM</a><a href="CarBrand.aspx156?pvareaid=103225">卡尔森</a><a href="CarBrand.aspx224?pvareaid=103225">卡升</a></div>
            <div class="brandbox"><a href="CarBrand.aspx199?pvareaid=103225">卡威</a><a href="CarBrand.aspx101?pvareaid=103225">开瑞</a><a href="CarBrand.aspx47?pvareaid=103225">凯迪拉克</a></div>
            <div class="brandbox"><a href="CarBrand.aspx214?pvareaid=103225">凯翼</a><a href="CarBrand.aspx100?pvareaid=103225">科尼赛克</a><a href="CarBrand.aspx9?pvareaid=103225">克莱斯勒</a></div>
        </div>

        <div class="title01 " id="brandL" name="brandL">
            <h3>L</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx48?pvareaid=103225">兰博基尼</a><a href="CarBrand.aspx118?pvareaid=103225">劳伦士</a><a href="CarBrand.aspx54?pvareaid=103225">劳斯莱斯</a></div>
            <div class="brandbox"><a href="CarBrand.aspx215?pvareaid=103225">雷丁</a><a href="CarBrand.aspx52?pvareaid=103225">雷克萨斯</a><a href="CarBrand.aspx10?pvareaid=103225">雷诺</a></div>
            <div class="brandbox"><a href="CarBrand.aspx124?pvareaid=103225">理念</a><a href="CarBrand.aspx80?pvareaid=103225">力帆</a><a href="CarBrand.aspx89?pvareaid=103225">莲花汽车</a></div>
            <div class="brandbox"><a href="CarBrand.aspx78?pvareaid=103225">猎豹汽车</a><a href="CarBrand.aspx51?pvareaid=103225">林肯</a><a href="CarBrand.aspx53?pvareaid=103225">铃木</a></div>
            <div class="brandbox"><a href="CarBrand.aspx204?pvareaid=103225">陆地方舟</a><a href="CarBrand.aspx88?pvareaid=103225">陆风</a><a href="CarBrand.aspx49?pvareaid=103225">路虎</a></div>
            <div class="brandbox"><a href="CarBrand.aspx50?pvareaid=103225">路特斯</a></div>
        </div>

        <div class="title01 " id="brandM" name="brandM">
            <h3>M</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx20?pvareaid=103225">MG</a><a href="CarBrand.aspx56?pvareaid=103225">MINI</a><a href="CarBrand.aspx58?pvareaid=103225">马自达</a></div>
            <div class="brandbox"><a href="CarBrand.aspx57?pvareaid=103225">玛莎拉蒂</a><a href="CarBrand.aspx55?pvareaid=103225">迈巴赫</a><a href="CarBrand.aspx129?pvareaid=103225">迈凯伦</a></div>
            <div class="brandbox"><a href="CarBrand.aspx168?pvareaid=103225">摩根</a></div>
        </div>

        <div class="title01 " id="brandN" name="brandN">
            <h3>N</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx130?pvareaid=103225">纳智捷</a><a href="CarBrand.aspx213?pvareaid=103225">南京金龙</a></div>
        </div>

        <div class="title01 " id="brandO" name="brandO">
            <h3>O</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx60?pvareaid=103225">讴歌</a><a href="CarBrand.aspx59?pvareaid=103225">欧宝</a><a href="CarBrand.aspx146?pvareaid=103225">欧朗</a></div>
        </div>

        <div class="title01 " id="brandQ" name="brandQ">
            <h3>Q</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx26?pvareaid=103225">奇瑞</a><a href="CarBrand.aspx122?pvareaid=103225">启辰</a><a href="CarBrand.aspx62?pvareaid=103225">起亚</a></div>
        </div>

        <div class="title01 " id="brandR" name="brandR">
            <h3>R</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx63?pvareaid=103225">日产</a><a href="CarBrand.aspx19?pvareaid=103225">荣威</a><a href="CarBrand.aspx174?pvareaid=103225">如虎</a></div>
            <div class="brandbox"><a href="CarBrand.aspx103?pvareaid=103225">瑞麒</a></div>
        </div>

        <div class="title01 " id="brandS" name="brandS">
            <h3>S</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx45?pvareaid=103225">smart</a><a href="CarBrand.aspx64?pvareaid=103225">萨博</a><a href="CarBrand.aspx68?pvareaid=103225">三菱</a></div>
            <div class="brandbox"><a href="CarBrand.aspx149?pvareaid=103225">陕汽通家</a><a href="CarBrand.aspx155?pvareaid=103225">上汽大通</a><a href="CarBrand.aspx173?pvareaid=103225">绅宝</a></div>
            <div class="brandbox"><a href="CarBrand.aspx66?pvareaid=103225">世爵</a><a href="CarBrand.aspx90?pvareaid=103225">双环</a><a href="CarBrand.aspx69?pvareaid=103225">双龙</a></div>
            <div class="brandbox"><a href="CarBrand.aspx162?pvareaid=103225">思铭</a><a href="CarBrand.aspx65?pvareaid=103225">斯巴鲁</a><a href="CarBrand.aspx67?pvareaid=103225">斯柯达</a></div>
        </div>

        <div class="title01 " id="brandT" name="brandT">
            <h3>T</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx202?pvareaid=103225">泰卡特</a><a href="CarBrand.aspx133?pvareaid=103225">特斯拉</a><a href="CarBrand.aspx161?pvareaid=103225">腾势</a></div>
        </div>

        <div class="title01 " id="brandW" name="brandW">
            <h3>W</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx102?pvareaid=103225">威麟</a><a href="CarBrand.aspx99?pvareaid=103225">威兹曼</a><a href="CarBrand.aspx70?pvareaid=103225">沃尔沃</a></div>
            <div class="brandbox"><a href="CarBrand.aspx114?pvareaid=103225">五菱汽车</a><a href="CarBrand.aspx167?pvareaid=103225">五十铃</a></div>
        </div>

        <div class="title01 " id="brandX" name="brandX">
            <h3>X</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx98?pvareaid=103225">西雅特</a><a href="CarBrand.aspx12?pvareaid=103225">现代</a><a href="CarBrand.aspx185?pvareaid=103225">新凯</a></div>
            <div class="brandbox"><a href="CarBrand.aspx71?pvareaid=103225">雪佛兰</a><a href="CarBrand.aspx72?pvareaid=103225">雪铁龙</a></div>
        </div>

        <div class="title01 " id="brandY" name="brandY">
            <h3>Y</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx111?pvareaid=103225">野马汽车</a><a href="CarBrand.aspx110?pvareaid=103225">一汽</a><a href="CarBrand.aspx144?pvareaid=103225">依维柯</a></div>
            <div class="brandbox"><a href="CarBrand.aspx73?pvareaid=103225">英菲尼迪</a><a href="CarBrand.aspx192?pvareaid=103225">英致</a><a href="CarBrand.aspx93?pvareaid=103225">永源</a></div>
        </div>

        <div class="title01 " id="brandZ" name="brandZ">
            <h3>Z</h3>
        </div>
        <div class="find-listbrand" data-type="brand">
            <div class="brandbox"><a href="CarBrand.aspx206?pvareaid=103225">知豆</a><a href="CarBrand.aspx22?pvareaid=103225">中华</a><a href="CarBrand.aspx74?pvareaid=103225">中兴</a></div>
            <div class="brandbox"><a href="CarBrand.aspx94?pvareaid=103225">众泰</a></div>
        </div>

    </div>
    <div class="nav-sub fn-mt">
        <div class="nav-bread">
            <a href="http://m.autohome.com.cn">首页</a>
            <i class="icon-arrow arrow-right"></i>
            <span>找车</span>
        </div>
    </div>

    <style>
        .w-search-icon {
            background: url(http://x.autoimg.cn/m/news/images/bg-widget-search-v0101.png) no-repeat;
            background-size: 16px auto;
        }

        .w-search {
            color: #4e5563;
            z-index: 998;
            position: relative;
            -webkit-text-size-adjust: 100%;
        }

        .w-search-sub {
            z-index: 10;
        }

        .w-search-content {
            height: 32px;
            border: solid #d0d6e1 1px;
            background: #f5f6f9;
            z-index: 10;
            position: relative;
            border-radius: 3px;
        }

        .w-search-input {
            margin: 0 22% 0 20%;
        }

        .w-search-input-box {
            height: 32px;
            padding: 0 32px 0 6px;
            position: relative;
            overflow: hidden;
        }

        .w-search-input-text {
            width: 100%;
            height: 18px;
            margin: 0;
            padding: 7px 0;
            background: transparent;
            border: none;
            outline: none;
            font-size: 16px;
            color: #4e5563;
            line-height: 18px;
            overflow: hidden;
            box-shadow: none;
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }

        .w-search-input-clear {
            display: inline-block;
            width: 16px;
            height: 16px;
            padding: 8px;
            overflow: hidden;
            position: absolute;
            top: 0;
            right: 0;
        }

            .w-search-input-clear i {
                display: inline-block;
                width: 16px;
                height: 16px;
                background-position: 0 -33px;
                vertical-align: top;
            }

        .w-search-tab {
            width: 20%;
            position: absolute;
            top: 0;
            left: -1px;
        }

        .w-search-tab-btn {
            display: block;
            height: 18px;
            padding: 7px 7px 7px 0;
            font-size: 16px;
            color: #5b5998;
            line-height: 18px;
            text-decoration: none;
            text-align: center;
        }

            .w-search-tab-btn:visited {
                color: #3b5997;
            }

            .w-search-tab-btn:after {
                content: "";
                display: inline-block;
                width: 0;
                height: 0;
                border: solid 5px;
                border-color: #3b5998 transparent transparent transparent;
                overflow: hidden;
                position: absolute;
                top: 14px;
                right: 3px;
            }

        .w-search-tab-pop {
            width: 100%;
            padding: 6px 0;
            background: rgba(0,0,0,0.8);
            position: absolute;
            top: 33px;
            left: 0;
            border-radius: 3px;
        }

            .w-search-tab-pop a {
                display: block;
                padding: 10px 7px 10px 0;
                font-size: 16px;
                color: #fff;
                line-height: 18px;
                text-decoration: none;
                text-align: center;
            }

                .w-search-tab-pop a:visted {
                    color: #fffffe;
                }

        .w-search-btn {
            width: 22%;
            height: 18px;
            padding: 8px 0;
            background: #8FB048;
            font-size: 16px;
            color: #ffffff;
            line-height: 18px;
            text-decoration: none;
            text-align: center;
            white-space: nowrap;
            position: absolute;
            top: -1px;
            right: -1px;
            border-radius: 0 3px 3px 0;
        }

            .w-search-btn:visited {
                color: #fffffe;
            }

        .w-search-pop {
            width: 100%;
            font-size: 14px;
            position: absolute;
            top: 33px;
            left: 0;
        }

        .w-search-pop-box {
            background: #fff;
            border: solid #d0d6e1 1px;
        }

        .w-search-pop-through {
            margin: 0;
            padding: 6px 0 0;
        }

            .w-search-pop-through dt {
                margin: 0;
                padding: 5px 9px;
                color: #a2a6ae;
                line-height: 18px;
                overflow: hidden;
            }

                .w-search-pop-through dt i {
                    float: left;
                    width: 9px;
                    height: 12px;
                    margin: 3px 6px 0 0;
                    background-position: 0 0;
                    overflow: hidden;
                }

            .w-search-pop-through dd {
                margin: 0;
                padding: 0;
                border-bottom: dotted #e1e7ee 1px;
                position: relative;
            }

                .w-search-pop-through dd:last-child {
                    border: none;
                }

            .w-search-pop-through a:nth-child(1) {
                display: block;
                padding: 8px 9px;
                margin: 0 48px 0 0;
                color: #3b5998;
                line-height: 18px;
                text-decoration: none;
                white-space: nowrap;
                text-overflow: ellipsis;
                overflow: hidden;
            }

            .w-search-pop-through a:visited {
                color: #3b5997;
            }

            .w-search-pop-through span {
                width: 48px;
                height: 34px;
                text-align: center;
                overflow: hidden;
                position: absolute;
                top: 0;
                right: 0;
            }

                .w-search-pop-through span i {
                    display: inline-block;
                    width: 11px;
                    height: 11px;
                    margin: 11px 0 0;
                    background-position: 0 -52px;
                    vertical-align: top;
                }

        .w-search-pop-normal {
            margin: 0;
            padding: 6px 0 0;
        }

            .w-search-pop-normal dt {
                margin: 0;
                padding: 5px 9px;
                color: #a2a6ae;
                line-height: 18px;
                overflow: hidden;
            }

                .w-search-pop-normal dt i {
                    float: left;
                    width: 15px;
                    height: 15px;
                    margin: 1px 6px 0 0;
                    background-position: 0 -15px;
                    overflow: hidden;
                }

            .w-search-pop-normal dd {
                margin: 0;
                padding: 0;
                border-bottom: dotted #e1e7ee 1px;
                position: relative;
            }

                .w-search-pop-normal dd:last-child {
                    border: none;
                }

            .w-search-pop-normal a:nth-child(1) {
                display: block;
                padding: 8px 9px;
                margin: 0 48px 0 0;
                color: #4e5563;
                line-height: 18px;
                text-decoration: none;
                white-space: nowrap;
                text-overflow: ellipsis;
                overflow: hidden;
            }

            .w-search-pop-normal a:visited {
                color: #4e5562;
            }

            .w-search-pop-normal span {
                width: 48px;
                height: 34px;
                text-align: center;
                overflow: hidden;
                position: absolute;
                top: 0;
                right: 0;
            }

                .w-search-pop-normal span i {
                    display: inline-block;
                    width: 11px;
                    height: 11px;
                    margin: 11px 0 0;
                    background-position: 0 -52px;
                    vertical-align: top;
                }

            .w-search-pop-normal a b {
                font-weight: normal;
                color: #a2a6ae;
            }

            .w-search-pop-normal a:visited b {
                color: #a2a6ad;
            }

        .w-search-pop-bar {
            background: #f0f2f7;
            border-top: solid #d0d6e1 1px;
            font-size: 16px;
            line-height: 18px;
            overflow: hidden;
            position: relative;
        }

            .w-search-pop-bar a {
                padding: 8px 0;
                color: #3b5998;
                text-decoration: none;
                text-align: center;
            }

                .w-search-pop-bar a.w-search-pop-clear {
                    float: left;
                    width: 112px;
                    border-right: solid #d0d6e1 1px;
                }

                .w-search-pop-bar a.w-search-pop-close {
                    float: right;
                    width: 48px;
                    border-left: solid #d0d6e1 1px;
                }

                .w-search-pop-bar a:visited {
                    color: #3b5997;
                }

        .w-search-pop-divi {
            border-top: solid #e1e7ee 1px;
        }

        .w-search-notab .w-search-input {
            margin: 0 22% 0 0;
        }

        .w-search-notab .w-search-tab {
            display: none;
        }

        .w-search-hide {
            display: none;
        }
    </style>
    <div class="w-search fn-mt fn-mlr w-search-sub w-search-notab">
        <form action="http://sou.m.autohome.com.cn/zonghe" data-action="http://sou.m.autohome.com.cn/">
            <div class="w-search-content">
                <div class="w-search-tab">
                    <a class="w-search-tab-btn" href="###">综合<i></i></a>
                    <div class="w-search-tab-pop w-search-hide">
                        <a href="###">综合</a>
                        <a href="###">文章</a>
                        <a href="###">论坛</a>
                    </div>
                </div>

                <div class="w-search-input">
                    <div class="w-search-input-box">
                        <input name="q" class="w-search-input-text" type="text" required="" value="" autocomplete="off" autocorrect="off" />
                        <a class="w-search-input-clear w-search-hide" href="###"><i class="w-search-icon"></i></a>
                    </div>
                </div>
                <a class="w-search-btn" href="###">搜索</a>
            </div>
            <div class="w-search-pop">
                <div class="w-search-pop-box w-search-hide">
                    <div class="w-search-pop-bar">
                        <a href="#">关闭</a>
                    </div>
                </div>
            </div>
            <input type="hidden" name="entry" value="68" />
        </form>
    </div>
    <script src="/wechatjs/search.min.js"></script>
    <script>
        //$(document).ready(function () {
        //    $.LoadJs({ url: "http://x.autoimg.cn/car/js/m/search/v1.2/search.min.js", type: "async" });
        //});
    </script>
    <script type="text/javascript">

        /********格式化价格*********/
        function GetPriceSection(min, max) {
            var retstr = "",
                minPrice = parseFloat(min) / 10000,
                maxPrice = parseFloat(max) / 10000;
            if (maxPrice == 0) {
                return "暂无报价";
            }
            minPrice = minPrice.toFixed(2);
            maxPrice = maxPrice.toFixed(2);
            if (minPrice == maxPrice) {
                return maxPrice.toString() + "万";
            }
            return minPrice + "-" + maxPrice + "万";
        }

        /********加载用户浏览历史SATART*********/
        var hisseries = $.getCookie("historyseries", "");
        function InitHistory() {
            if (hisseries != "") {
                $("#l_history").removeClass("btn-disabled");
                $.getJSON("/ashx/car/LoadPriceBySeriesIds.ashx?seriesids=" + hisseries, function (data) {
                    var histroryHtml = "";
                    if (data) {
                        $.each(data.result, function (i, item) {
                            if (i < 3) {
                                histroryHtml += " <li>";
                                histroryHtml += "<a href=\"http://m.autohome.com.cn/" + item.id + "?pvareaid=103224\">";
                                histroryHtml += "<span class=\"caption\">" + item.name + "</span>";
                                histroryHtml += "<span class=\"pricenorm\">" + GetPriceSection(item.minprice, item.maxprice) + "</span>";
                                if (item.state == 40) {
                                    histroryHtml += "<span class=\"mark-salestop\">停售</span>";
                                }
                                histroryHtml += "<i class=\"icon-arrow arrow-right\"></i>";
                                histroryHtml += "</a>";
                                histroryHtml += "</li>";
                            }
                        });
                    }
                    $("#u_history").html(histroryHtml);
                    $("#d_history").removeClass("fn-hide");
                });
                $("#d_history").on("click", function () {
                    ToggleHistory($(this));
                });
            }
            else {
                $("#l_history").addClass("btn-disabled");
                $("#u_history").addClass("fn-hide");
                $("#d_history").addClass("fn-hide");
            }
        }

        /********选择顶层标签*********/
        function SelectMenu(obj) {
            $(".find-siftanchor a").removeClass("btn-selected");
            $(obj).addClass("btn-selected");
        }
        function ToggleHistory(obj) {
            var openname = obj.attr("data-name");
            if (openname == "s") {
                ShowHistory();
            }
            if (openname == "h") {
                HideHistory();
            }
        }
        /********隐藏浏览历史*********/
        function HideHistory() {
            $("#d_history").html("<h3>我浏览的</h3><div class=\"fold\">展开<i class=\"arrow-bottom\"></i></div>").attr("data-name", "s"),
            $("#u_history").addClass("fn-hide");
        }
        /********显示浏览历史*********/
        function ShowHistory() {
            $("#d_history").html("<h3>我浏览的</h3><div class=\"fold\">收起<i class=\"arrow-top\"></i></div>").attr("data-name", "h"),
            $("#u_history").removeClass("fn-hide");
        }
        /********加载用户浏览历史END*********/

        $(function () {
            $(".find-siftanchor").on("click", "a", function () {
                var id = $(this).attr("id");
                if (id != undefined && id == "l_history" && hisseries == "") {
                    return;
                }
                else if (id != undefined && id == "l_history" && hisseries != "") {
                    ShowHistory();
                }
                else if (id != undefined && id == "l_hotbrand") {
                    HideHistory();
                }
                SelectMenu(this);
            });

            InitHistory();
        });
    </script>
    <script language="javascript" type="text/javascript">

        var pvTrack = function () { };
        pvTrack.site = 1211002; pvTrack.category = 14; pvTrack.subcategory = 121;
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
    <script type="text/javascript" src="http://33.autohome.com.cn/njs/1072.js?v=20140821">
    </script>
    <script type="text/javascript">
        ; (function () {
            function ReplaceTemplate(D, T) { function f(d, t) { var sb = t, b = null; for (var o in d) { b = new RegExp('{' + o + '}', 'gi'); sb = sb.replace(b, '' + d[o]) } return sb } var SB = ''; if (Object.prototype.toString.call(D) == '[object Array]') { for (var i = 0; i < D.length; i++) { SB += arguments.callee(D[i], T) } } else { SB = D == '' ? '' : f(D, T) } return SB };
            var u = navigator.userAgent;
            function getHeaderHTML() {
                var headerDate = { "recommendid": 15, "rchannelId": 30, "randroidappname": "\u6C7D\u8F66\u4E4B\u5BB6", "randroidapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=824&rd=http%3A%2F%2Fapp.autohome.com.cn%2Fdownload%2Fautohome_m_head_car.apk", "randroidappimg": "http://x.autoimg.cn/m/news/logo/autohome_80X80.jpg", "riphoneappname": "\u6C7D\u8F66\u4E4B\u5BB6", "riphoneapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=811&rd=https%3a%2f%2fitunes.apple.com%2fcn%2fapp%2fqi-che-zhi-jia%2fid385919493%3fmt%3d8", "riphoneappimg": "http://x.autoimg.cn/m/news/logo/autohome_80X80.jpg", "rstatus": 1, "randroidappcomment": "\u6700\u65B0\u8F66\u8BAF\uFF0C\u4E00\u89E6\u5373\u8FBE", "riphoneappcomment": "\u6700\u65B0\u8F66\u8BAF\uFF0C\u4E00\u89E6\u5373\u8FBE" }
                var cookiename = ["bileH_2M0_e", "2"]; var snum = cookiename[0].split("_");
                var r = snum[1].match(/\d+(\.\d+)?/g), value = parseInt(r[0]) + parseInt(r[1]) * 5;
                var classname = 'diezrq14_';
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
