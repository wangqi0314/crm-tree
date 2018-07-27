<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarStyle.aspx.cs" Inherits="wechat_Car_CarStyle" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="format-detection" content="email=no" />
    <meta name="apple-itunes-app" content="app-id=385919493" />
    <title></title>
    <%--<link href="/wechatcss/comm/public_W01.css" rel="stylesheet" />--%>
    <%--<script src="/wechatjs/NewZepto.js"></script>--%>
    <script src="/wechatjs/zepto.js"></script>
    <%--<link href="/wechatcss/comm/findcar_W01.css" rel="stylesheet" />--%>
    <%--<link href="http://x.autoimg.cn/com/com.ashx?path=[/m/style/v2.7.6/series.css,widget.css]" rel="stylesheet" />--%>
    <link href="/wechatcss/comm/series_W01.css" rel="stylesheet" />
</head>
<body>
    <section class="wrapper">
        <nav class="nav-bread">
            <a href="#">找车</a>
            <i class="iconfont icon-arrow-right"></i>
            <a href="#">车系</a>
            <i class="iconfont icon-arrow-right"></i>
            <span class="end">综述</span>
        </nav>
        <section>
            <h1 class="car-name">宝马5系(进口)<i class="iconfont  icon-favor" id="Collect"></i></h1>
            <ul class="menu menu-column4">
                <li><a href="#" class="activate">综述</a></li>
                <li><a href="#">配置</a></li>
                <li><a href="#">图片</a></li>
                <li class="last"><a id="nav_dealerPrice" href="#">经销商</a></li>
                <li><a id="nav_jiangjia" href="#">降价</a></li>
            </ul>
            <script type="text/javascript">
                var Config = Config || [];
                Config["SeriesId"] = Config["SeriesId"] || 202;
                Config["BrandId"] = Config["BrandId"] || 15;
                Config["PVAreaIds"] = { "param": "#pvareaid=102185", "koubei": "#pvareaid=102417", "bbs": "#pvareaid=102510", "article": "#pvareaid=103310", "pic": "#pvareaid=103373", "price": "#pvareaid=103374", "dealer": "#pvareaid=103375", "video": "#pvareaid=104033", "2sc": "", "dai1": "#pvareaid=103377", "dai2": "#pvareaid=103378", "che168": "#pvareaid=100681" };
            </script>
        </section>
        <section class="car-info model-norm fn-mt fn-mlr">
            <header class="fn-mlr">
                <a href="http://car.m.autohome.com.cn/pic/series/202.html#pvareaid=103141" id="TitlePicLink" class="img">
                    <img src="http://car0.m.autoimg.cn/upload/2013/9/30/va_wp_201309301824175074122.jpg" /></a>
                <h1>宝马5系(进口)</h1>
                <p class="price">45.70-87.90<em>万</em></p>
                <p id="XunjiaBotton" class="handle"><a href="/wechat/Dealer/DealerApp.aspx" class="btn btn-full btn-highlight">试驾预约</a></p>
            </header>
            <section class="fn-mlr fn-mb">
                <dl>
                    <dt>品牌：</dt>
                    <dd>宝马</dd>
                </dl>
                <dl>
                    <dt>排量：</dt>
                    <dd><span>2.0T</span><span>3.0T</span></dd>
                </dl>
                <dl>
                    <dt>级别：</dt>
                    <dd>中大型车</dd>
                </dl>
                <dl>
                    <dt>变速箱：</dt>
                    <dd><span><span>自动</span></span></dd>
                </dl>
                <dl>
                    <dt>颜色：</dt>
                    <dd><i class="colorlump" style="background: #FAFAFA"></i><i class="colorlump" style="background: #000000"></i><i class="colorlump" style="background: #242224"></i><i class="colorlump" style="background: #B0B2A7"></i><i class="colorlump" style="background: #8B989C"></i><i class="colorlump" style="background: #4F473F"></i><i class="colorlump" style="background: #273042"></i><i class="colorlump" style="background: #E7E7E7"></i><i class="colorlump" style="background: #B1C8D6"></i><i class="colorlump" style="background: #554006"></i><i class="colorlump" style="background: #6C6F6F"></i><i class="colorlump" style="background: #949A9C"></i><i class="colorlump" style="background: #442424"></i><i class="colorlump" style="background: #3C3C3C"></i></dd>
                </dl>
            </section>
            <a href="#" id="OptionLink_W" class="btn btn-full fn-mt fn-mb fn-mlr">详细参数配置<i class="iconfont icon-arrow-right"></i></a>
        </section>
        <div id="mc8j9c" class="adv03-1 fn-mt fn-mlr" style="display: none"></div>
        <div id="k5g4kk" class="adv01 fn-mt fn-mlr" style="display: none"></div>
        <section class="model-norm fn-mt fn-mlr fn-hide" id="mall"></section>
        <section class="model-norm fn-mt fn-mlr fn-hide" id="supergroup"></section>
        <section id="specBanner" class="model-cartype model-norm fn-mt fn-mlr" data-role="tabSwitch">
            <h1 class="tab" data-role="tab">
                <span id="series_btn_Saling1">在售车型</span>
                <span id="series_btn_Saling2" class="fn-hide">即将销售</span>
                <span id="series_btn_Saling3">停售车型</span>
            </h1>
            <div data-role="content">
                <div id="Series_SpecList1">

                    <h2 class="title-sub">
                        <span>2.0升  184马力</span>
                    </h2>
                    <ul class="list-cartype fn-mb fn-mlr">

                        <li data-modelid="17084">
                            <h3><a href="/spec/17084/#pvareaid=103382">2014款 520i 典雅型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>45.70万</strong></span>
                                <span class="lowest">全国最低价：<strong>37.94</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/17084/#pvareaid=103383" data-id="17084">口碑</a>
                                <span class="btn btn-small" data-comid="17084" data-name="2014款 520i 典雅型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="17084">试驾预约</a>
                            </p>
                        </li>

                        <li data-modelid="17163">
                            <h3><a href="/spec/17163/#pvareaid=103382">2014款 520i 旅行版<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>48.90万</strong></span>
                                <span class="lowest">全国最低价：<strong>42.30</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/17163/#pvareaid=103383" data-id="17163">口碑</a>
                                <span class="btn btn-small" data-comid="17163" data-name="2014款 520i 旅行版">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="17163">试驾预约</a>
                            </p>
                        </li>

                    </ul>

                    <h2 class="title-sub">
                        <span>2.0升  245马力</span>
                    </h2>
                    <ul class="list-cartype fn-mb fn-mlr">

                        <li data-modelid="17164">
                            <h3><a href="/spec/17164/#pvareaid=103382">2014款 528i 旅行版<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>65.60万</strong></span>
                                <span class="lowest">全国最低价：<strong>56.40</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small btn-disabled">口碑</a>
                                <span class="btn btn-small" data-comid="17164" data-name="2014款 528i 旅行版">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="17164">试驾预约</a>
                            </p>
                        </li>

                        <li data-modelid="17087">
                            <h3><a href="/spec/17087/#pvareaid=103382">2014款 528i xDrive 设计套装型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>68.90万</strong></span>
                                <span class="lowest">全国最低价：<strong>56.50</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small btn-disabled">口碑</a>
                                <span class="btn btn-small" data-comid="17087" data-name="2014款 528i xDrive 设计套装型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="17087">试驾预约</a>
                            </p>
                        </li>

                        <li data-modelid="17165">
                            <h3><a href="/spec/17165/#pvareaid=103382">2014款 528i xDrive M运动型 旅行版<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>72.70万</strong></span>
                                <span class="lowest">全国最低价：<strong>62.55</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/17165/#pvareaid=103383" data-id="17165">口碑</a>
                                <span class="btn btn-small" data-comid="17165" data-name="2014款 528i xDrive M运动型 旅行版">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="17165">试驾预约</a>
                            </p>
                        </li>

                    </ul>

                    <h2 class="title-sub">
                        <span>3.0升  306马力</span>
                    </h2>
                    <ul class="list-cartype fn-mb fn-mlr">

                        <li data-modelid="17088">
                            <h3><a href="/spec/17088/#pvareaid=103382">2014款 535i 设计套装型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>70.30万</strong></span>
                                <span class="lowest">全国最低价：<strong>56.94</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/17088/#pvareaid=103383" data-id="17088">口碑</a>
                                <span class="btn btn-small" data-comid="17088" data-name="2014款 535i 设计套装型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="17088">试驾预约</a>
                            </p>
                        </li>

                        <li data-modelid="17089">
                            <h3><a href="/spec/17089/#pvareaid=103382">2014款 535i xDrive M运动型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>80.90万</strong></span>
                                <span class="lowest">全国最低价：<strong>67.15</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/17089/#pvareaid=103383" data-id="17089">口碑</a>
                                <span class="btn btn-small" data-comid="17089" data-name="2014款 535i xDrive M运动型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="17089">试驾预约</a>
                            </p>
                        </li>

                        <li data-modelid="16062">
                            <h3><a href="/spec/16062/#pvareaid=103382">2014款 ActiveHybrid 5<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>87.90万</strong></span>
                                <span class="lowest">全国最低价：<strong>72.96</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small btn-disabled">口碑</a>
                                <span class="btn btn-small" data-comid="16062" data-name="2014款 ActiveHybrid 5">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="16062">试驾预约</a>
                            </p>
                        </li>

                    </ul>

                    <h2 class="title-sub">
                        <span>3.0升  306马力</span>
                    </h2>
                    <ul class="list-cartype fn-mb fn-mlr">

                        <li data-modelid="17088">
                            <h3><a href="/spec/17088/#pvareaid=103382">2014款 535i 设计套装型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>70.30万</strong></span>
                                <span class="lowest">全国最低价：<strong>56.94</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/17088/#pvareaid=103383" data-id="17088">口碑</a>
                                <span class="btn btn-small" data-comid="17088" data-name="2014款 535i 设计套装型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="17088">试驾预约</a>
                            </p>
                        </li>

                        <li data-modelid="17089">
                            <h3><a href="/spec/17089/#pvareaid=103382">2014款 535i xDrive M运动型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>80.90万</strong></span>
                                <span class="lowest">全国最低价：<strong>67.15</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/17089/#pvareaid=103383" data-id="17089">口碑</a>
                                <span class="btn btn-small" data-comid="17089" data-name="2014款 535i xDrive M运动型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="17089">试驾预约</a>
                            </p>
                        </li>

                        <li data-modelid="16062">
                            <h3><a href="/spec/16062/#pvareaid=103382">2014款 ActiveHybrid 5<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>87.90万</strong></span>
                                <span class="lowest">全国最低价：<strong>72.96</strong><em>万</em></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small btn-disabled">口碑</a>
                                <span class="btn btn-small" data-comid="16062" data-name="2014款 ActiveHybrid 5">+&nbsp;对比</span>
                                <a class="btn btn-small btn-highlight" href="#" data-id="16062">试驾预约</a>
                            </p>
                        </li>

                    </ul>

                    <span class="btn btn-big btn-full btn-more fn-mt fn-mb fn-mlr" id="LoadMoreSpec1">查看更多在售车型<br />
                        <i class="iconfont icon-arrow-bottom"></i></span>
                </div>
                <div id="Series_SpecList2" class="fn-hide">
                    <ul class="list-cartype fn-mb fn-mlr">
                    </ul>
                    <span class="btn btn-big btn-full btn-more fn-mt fn-mb fn-mlr" id="LoadMoreSpec2">查看更多即将销售车型<br />
                        <i class="iconfont icon-arrow-bottom"></i></span>
                    <span class="btn btn-big btn-full btn-loading fn-mt fn-mb fn-mlr"><i class="loading"></i>加载中...</span>
                </div>
                <div id="Series_SpecList3">
                    <div class="car-year">
                        <span class="btn btn-small" id="year_current" data-year="2013">2013款<i class="iconfont icon-arrow-bottom"></i></span>
                        <div id="year_current_list" class="filter filter-column2 fn-hide">
                            <ul>
                                <li><span data-year="2013" class="activate">2013款</span></li>
                                <li><span data-year="2012">2012款</span></li>
                                <li><span data-year="2011">2011款</span></li>
                                <li><span data-year="2010">2010款</span></li>
                                <li><span data-year="2006">2006款</span></li>
                                <li><span data-year="2004">2004款</span></li>
                                <li><span data-year="2001">2001款</span></li>
                                <li><span data-year="1994">1994款</span></li>
                            </ul>
                        </div>
                    </div>

                    <h2 class="title-sub">
                        <span>2.0升 184马力</span>
                    </h2>
                    <ul class="list-cartype fn-mb fn-mlr">

                        <li>
                            <h3><a href="/spec/14643/">2013款 520i 典雅型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>45.50万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/14643/#pvareaid=103383" data-id="14643">口碑</a>
                                <span class="btn btn-small" data-comid="14643" data-name="2013款 520i 典雅型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                    </ul>

                    <h2 class="title-sub">
                        <span>2.0升 245马力</span>
                    </h2>
                    <ul class="list-cartype fn-mb fn-mlr">

                        <li>
                            <h3><a href="/spec/14647/">2013款 528i xDrive豪华型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>70.30万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/14647/#pvareaid=103383" data-id="14647">口碑</a>
                                <span class="btn btn-small" data-comid="14647" data-name="2013款 528i xDrive豪华型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                    </ul>

                    <h2 class="title-sub">
                        <span>3.0升 258马力</span>
                    </h2>
                    <ul class="list-cartype fn-mb fn-mlr">

                        <li>
                            <h3><a href="/spec/14642/">2013款 530i 领先型 旅行版<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>67.40万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/14642/#pvareaid=103383" data-id="14642">口碑</a>
                                <span class="btn btn-small" data-comid="14642" data-name="2013款 530i 领先型 旅行版">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                    </ul>

                    <h2 class="title-sub">
                        <span>3.0升 306马力</span>
                    </h2>
                    <ul class="list-cartype fn-mb fn-mlr">

                        <li>
                            <h3><a href="/spec/14644/">2013款 535i 领先运动型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>68.80万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/14644/#pvareaid=103383" data-id="14644">口碑</a>
                                <span class="btn btn-small" data-comid="14644" data-name="2013款 535i 领先运动型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                        <li>
                            <h3><a href="/spec/14645/">2013款 535i 豪华运动型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>76.20万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/14645/#pvareaid=103383" data-id="14645">口碑</a>
                                <span class="btn btn-small" data-comid="14645" data-name="2013款 535i 豪华运动型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                        <li>
                            <h3><a href="/spec/14646/">2013款 535i xDrive豪华型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>78.40万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/14646/#pvareaid=103383" data-id="14646">口碑</a>
                                <span class="btn btn-small" data-comid="14646" data-name="2013款 535i xDrive豪华型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                        <li>
                            <h3><a href="/spec/11036/">2013款 ActiveHybrid 5<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>87.80万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small btn-disabled">口碑</a>
                                <span class="btn btn-small" data-comid="11036" data-name="2013款 ActiveHybrid 5">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                    </ul>

                    <h2 class="title-sub">
                        <span>3.0升 306马力</span>
                    </h2>
                    <ul class="list-cartype fn-mb fn-mlr">

                        <li>
                            <h3><a href="/spec/14644/">2013款 535i 领先运动型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>68.80万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/14644/#pvareaid=103383" data-id="14644">口碑</a>
                                <span class="btn btn-small" data-comid="14644" data-name="2013款 535i 领先运动型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                        <li>
                            <h3><a href="/spec/14645/">2013款 535i 豪华运动型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>76.20万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/14645/#pvareaid=103383" data-id="14645">口碑</a>
                                <span class="btn btn-small" data-comid="14645" data-name="2013款 535i 豪华运动型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                        <li>
                            <h3><a href="/spec/14646/">2013款 535i xDrive豪华型<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>78.40万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small" href="http://k.m.autohome.com.cn/spec/14646/#pvareaid=103383" data-id="14646">口碑</a>
                                <span class="btn btn-small" data-comid="14646" data-name="2013款 535i xDrive豪华型">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                        <li>
                            <h3><a href="/spec/11036/">2013款 ActiveHybrid 5<i class="iconfont icon-arrow-right"></i></a></h3>
                            <p class="info">
                                <span class="guide">指导价：<strong>87.80万</strong></span>
                            </p>
                            <p class="handle">
                                <a class="btn btn-small btn-disabled">口碑</a>
                                <span class="btn btn-small" data-comid="11036" data-name="2013款 ActiveHybrid 5">+&nbsp;对比</span>
                                <a class="btn btn-small btn-disabled">试驾预约</a>
                            </p>
                        </li>

                    </ul>

                    <span class="btn btn-big btn-full btn-more fn-mt fn-mb fn-mlr" id="LoadMoreSpec3">查看更多停售车型<br />
                        <i class="iconfont icon-arrow-bottom"></i></span>
                    <span class="btn btn-big btn-full btn-loading fn-mt fn-mb fn-mlr"><i class="loading"></i>加载中...</span>
                </div>
            </div>
        </section>
        <section id="SeriesClassPicBody" class="model-norm fn-mt fn-mlr" style="display: none">
            <h1 class="title">
                <span>实拍图片</span>
            </h1>
            <ul class="list-thumb">

                <li>
                    <a href="http://car.m.autohome.com.cn/pic/series/202-1.html#pvareaid=103386">
                        <img data-src="http://car0.m.autoimg.cn/upload/2014/9/18/280x210_0_q30_2014091809212042826411.jpg">
                        <h2 class="single">外观<span class="amount">869&nbsp;张</span></h2>
                    </a>
                </li>

                <li>
                    <a href="http://car.m.autohome.com.cn/pic/series/202-10.html#pvareaid=103386">
                        <img data-src="http://car0.m.autoimg.cn/upload/2014/9/18/280x210_0_q30_2014091809205631626411.jpg">
                        <h2 class="single">中控<span class="amount">1229&nbsp;张</span></h2>
                    </a>
                </li>

                <li>
                    <a href="http://car.m.autohome.com.cn/pic/series/202-3.html#pvareaid=103386">
                        <img data-src="http://car0.m.autoimg.cn/upload/2014/9/18/280x210_0_q30_2014091809205968126410.jpg">
                        <h2 class="single">座椅<span class="amount">1374&nbsp;张</span></h2>
                    </a>
                </li>

                <li>
                    <a href="http://car.m.autohome.com.cn/pic/series/202-12.html#pvareaid=103386">
                        <img data-src="http://car0.m.autoimg.cn/upload/2014/9/18/280x210_0_q30_2014091809205259826410.jpg">
                        <h2 class="single">细节<span class="amount">1557&nbsp;张</span></h2>
                    </a>
                </li>

            </ul>
            <a class="btn btn-full fn-mt fn-mb fn-mlr" href="http://car.m.autohome.com.cn/pic/series/202.html#pvareaid=103387">查看全部实拍图片<i class="iconfont icon-arrow-right"></i></a>
        </section>
        <section class="model-norm fn-mt fn-mlr" data-role="tabSwitch" id="DealerBody1s">
            <h1 class="title">
                <span>授权经销商</span>
                <a class="btn btn-small tarea" href="#">
                    <abbr id="DealerCityName_W">上海</abbr><i class="iconfont icon-arrow-right"></i></a>
            </h1>
            <div class="content-null fn-hide" id="NullDealerList">该地区暂无经销商记录</div>
            <ul class="list-dealer fn-mlr" id="DealerList_1">
                <li>
                    <a class="info" href="http://dealer.m.autohome.com.cn/1356/b_202.html#pvareaid=103499">
                    <h2>上海德宝<i class="iconfont icon-arrow-right"></i></h2>
                    <p class="price"><dfn>¥</dfn>45.50-48.90<em>万</em></p>
                </a>
                    <a href="http://dealer.m.autohome.com.cn/map/1356" class="site"><i class="iconfont icon-anchor"></i>上海市闵行区华翔路245号/吴中路52号一层</a><p class="handle"><a href="tel:4009314249" class="btn btn-tel" onclick="_hmt.push(['_trackEvent','拨打电话','点击','新拨打电话']);"><i class="iconfont icon-tel-24h"></i>拨打电话</a>
                    <a href="/wechat/Dealer/DealerApp.aspx" class="btn btn-highlight"><i class="iconfont icon-mobile"></i>试驾预约</a></p>
                    <p class="sale">
                        <a href="http://dealer.m.autohome.com.cn/news_19805311.html#pvareaid=103501"><i class="mark-sale">促</i>进口宝马5系优惠5万 少量现车</a></p>
                </li>
                <li><a class="info" href="http://dealer.m.autohome.com.cn/67497/b_202.html#pvareaid=103499">
                    <h2>上海众国宝泓<i class="iconfont icon-arrow-right"></i></h2>
                    <p class="price"><dfn>¥</dfn>42.20-87.90<em>万</em></p>
                </a><a href="http://dealer.m.autohome.com.cn/map/67497" class="site"><i class="iconfont icon-anchor"></i>上海市普陀区红柳路233号</a><p class="handle"><a href="tel:4009314297" class="btn btn-tel" onclick="_hmt.push(['_trackEvent','拨打电话','点击','新拨打电话']);"><i class="iconfont icon-tel-24h"></i>拨打电话</a><a href="/wechat/Dealer/DealerApp.aspx" class="btn btn-highlight"><i class="iconfont icon-mobile"></i>试驾预约</a></p>
                    <p class="sale"><a href="http://dealer.m.autohome.com.cn/news_18809710.html#pvareaid=103501"><i class="mark-sale">促</i>进口宝马5系最低45.5万起 欢迎试乘试驾</a></p>
                </li>
                <li><a class="info" href="http://dealer.m.autohome.com.cn/77772/b_202.html#pvareaid=103499">
                    <h2>上海宝景<i class="iconfont icon-arrow-right"></i></h2>
                    <p class="price"><dfn>¥</dfn>42.50-87.90<em>万</em></p>
                </a><a href="http://dealer.m.autohome.com.cn/map/77772" class="site"><i class="iconfont icon-anchor"></i>闵行区莲花南路2268号</a><p class="handle"><a href="tel:4009317258" class="btn btn-tel" onclick="_hmt.push(['_trackEvent','拨打电话','点击','新拨打电话']);"><i class="iconfont icon-tel-24h"></i>拨打电话</a><a href="/wechat/Dealer/DealerApp.aspx" class="btn btn-highlight"><i class="iconfont icon-mobile"></i>试驾预约</a></p>
                    <p class="sale"><a href="http://dealer.m.autohome.com.cn/news_19427910.html#pvareaid=103501"><i class="mark-sale">促</i>购进口宝马5系享7.20万优惠 可试乘试驾</a></p>
                </li>
                <li><a class="info" href="http://dealer.m.autohome.com.cn/101047/b_202.html#pvareaid=103499">
                    <h2>上海绿地宝仕<i class="iconfont icon-arrow-right"></i></h2>
                    <p class="price"><dfn>¥</dfn>44.50-44.70<em>万</em></p>
                </a><a href="http://dealer.m.autohome.com.cn/map/101047" class="site"><i class="iconfont icon-anchor"></i>上海市杨浦区周家嘴路3388号近隆昌路</a><p class="handle"><a href="tel:4009314216" class="btn btn-tel" onclick="_hmt.push(['_trackEvent','拨打电话','点击','新拨打电话']);"><i class="iconfont icon-tel-24h"></i>拨打电话</a><a href="/wechat/Dealer/DealerApp.aspx" class="btn btn-highlight"><i class="iconfont icon-mobile"></i>试驾预约</a></p>
                    <p class="sale"><a href="http://dealer.m.autohome.com.cn/news_18167650.html#pvareaid=103501"><i class="mark-sale">促</i>购进口宝马5系享1万优惠 可试乘试驾</a></p>
                </li>
                <li><a class="info" href="http://dealer.m.autohome.com.cn/1355/b_202.html#pvareaid=103499">
                    <h2>上海宝诚<i class="iconfont icon-arrow-right"></i></h2>
                    <p class="price"><dfn>¥</dfn>45.50-87.90<em>万</em></p>
                </a><a href="http://dealer.m.autohome.com.cn/map/1355" class="site"><i class="iconfont icon-anchor"></i>上海市浦东新区龙阳路2277号永达大厦1楼</a><p class="handle"><a href="tel:4009314978" class="btn btn-tel" onclick="_hmt.push(['_trackEvent','拨打电话','点击','新拨打电话']);"><i class="iconfont icon-tel"></i>拨打电话</a><a href="/wechat/Dealer/DealerApp.aspx" class="btn btn-highlight"><i class="iconfont icon-mobile"></i>试驾预约</a></p>
                    <p class="sale"><a href="http://dealer.m.autohome.com.cn/news_19973377.html#pvareaid=103501"><i class="mark-sale">促</i>购进口宝马5系最高降4万 送大礼包</a></p>
                </li>
            </ul>
            <a class="btn btn-full fn-mt fn-mb fn-mlr" href="#" id="DealerList_1_more_W">查看更多经销商<i class="iconfont icon-arrow-right"></i></a>
        </section>
        <script type="text/javascript">
            var Config = Config || {};
            Config["bottonAppJson"] = [{
                "recommendid": 2,
                "rchannelId": 10,
                "randroidappname": "\u8FDD\u7AE0\u67E5\u8BE2\u52A9\u624B",
                "randroidapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=550&rd=http%3a%2f%2fapp.autohome.com.cn%2fdownload%2fwz_m_b.apk",
                "randroidappimg": "http://x.autoimg.cn/m/news/logo/wzh_80X80.jpg",
                "riphoneappname": "\u8FDD\u7AE0\u67E5\u8BE2\u52A9\u624B",
                "riphoneapphref": "http://club.m.autohome.com.cn/Ashx/RD/rd.ashx?id=339&rd=http%3a%2f%2fitunes.apple.com%2fcn%2fapp%2fid708985992%3fmt%3d8",
                "riphoneappimg": "http://x.autoimg.cn/m/news/logo/wzh_80X80.jpg",
                "rstatus": 1, "randroidappcomment": "", "riphoneappcomment": ""
            }, {
                "recommendid": 3,
                "rchannelId": 10, "randroidappname": "\u4E8C\u624B\u8F66", "randroidapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=630&rd=http%3a%2f%2fapp.autohome.com.cn%2fdownload%2fusedcar_m_tuijian.apk", "randroidappimg": "http://x.autoimg.cn/m/news/logo/2sc_80X80.jpg", "riphoneappname": "\u6C7D\u8F66\u62A5\u4EF7", "riphoneapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=786&rd=http%3a%2f%2fitunes.apple.com%2fcn%2fapp%2fid415206413%3fmt%3d8", "riphoneappimg": "http://x.autoimg.cn/m/news/logo/bj_80X80.jpg", "rstatus": 1, "randroidappcomment": "", "riphoneappcomment": ""
            }, { "recommendid": 4, "rchannelId": 10, "randroidappname": "\u6C7D\u8F66\u62A5\u4EF7", "randroidapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=787&rd=http%3a%2f%2fapp.autohome.com.cn%2fdownload%2fahprice-summary.apk", "randroidappimg": "http://x.autoimg.cn/m/news/logo/bj_80X80.jpg", "riphoneappname": "\u4E8C\u624B\u8F66", "riphoneapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=631&rd=https%3a%2f%2fitunes.apple.com%2fcn%2fapp%2fer-shou-che-qi-che-zhi-jia%2fid455177952%3fmt%3d8", "riphoneappimg": "http://x.autoimg.cn/m/news/logo/2sc_80X80.jpg", "rstatus": 1, "randroidappcomment": "", "riphoneappcomment": "" }, { "recommendid": 1, "rchannelId": 10, "randroidappname": " \u6C7D\u8F66\u4E4B\u5BB6", "randroidapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=764&rd=http%3a%2f%2fapp.autohome.com.cn%2fdownload%2fautohome_m_article.apk", "randroidappimg": "http://x.autoimg.cn/m/news/logo/autohome_80X80.jpg", "riphoneappname": "\u6C7D\u8F66\u4E4B\u5BB6", "riphoneapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=765&rd=https%3a%2f%2fitunes.apple.com%2fcn%2fapp%2fqi-che-zhi-jia%2fid385919493%3fmt%3d8", "riphoneappimg": "http://x.autoimg.cn/m/news/logo/autohome_80X80.jpg", "rstatus": 1, "randroidappcomment": "", "riphoneappcomment": "" }, { "recommendid": 8, "rchannelId": 10, "randroidappname": "\u7528\u8F66\u4E4B\u5BB6", "randroidapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=781&rd=http%3a%2f%2fy.m.autohome.com.cn%2fapp%2fdownload%2fusehome.apk", "randroidappimg": "http://img.autohome.com.cn/newspic/2014/7/30/2014073014295090856.jpg", "riphoneappname": "\u7528\u8F66\u4E4B\u5BB6", "riphoneapphref": "http://app.autohome.com.cn/app/Ashx/RD/rd.ashx?id=782&rd=http%3a%2f%2fitunes.apple.com%2fcn%2fapp%2fqi-che-zhi-jia%2fid898126444%3fmt%3d8", "riphoneappimg": "http://img.autohome.com.cn/newspic/2014/7/30/2014073014295090856.jpg", "rstatus": 1, "randroidappcomment": "\u7528\u8F66\u517B\u8F66\u5FC5\u5907\u5DE5\u5177", "riphoneappcomment": "\u7528\u8F66\u517B\u8F66\u5FC5\u5907\u5DE5\u5177" }];
        </script>
        <div class="vs-tag fn-hide" id="compare_mark">
            对比<br />
            (0)
        </div>
        <div class="vs-cont-new fn-hide" id="compare_body">
            <h1>车型对比（<span id="compare_no">0</span>/4）
                <span class="delete" id="compare_clear"><i class="iconfont icon-delete"></i>清空</span>
            </h1>
            <ul class="info" id="compare_ul">
                <li index="0"></li>
                <li index="1"></li>
                <li index="2"></li>
                <li index="3"></li>
            </ul>
            <div class="btn btn-highlight btn-full fn-mr fn-mlr" id="compare_go">开始对比</div>
            <i class="iconfont icon-arrow-left"></i>
        </div>
    </section>
    <div id="loadPart"></div>
    <i class="loading loading-overall" id="loadImg" style="display: none"></i>
    <div id="loadSpec"></div>
    <script type="text/javascript" src="http://x.autoimg.cn/com/com.ashx?path=[/m/js/v1.6.2/zepto-1.2.min.js,area/areautil.js,as/mass.min.js,User/UserHelper.min.js,User/userlogin.min.js,index/notice.min.js,public/bottomApp.min.js]"></script>
    <script>
        var Auto = Auto || {};
        Auto["ReplaceTemplate"] = function (D, T) { function f(d, t) { var sb = t, b = null; for (var o in d) { b = new RegExp("{" + o + "}", "gi"); sb = sb.replace(b, "" + d[o]) } return sb } var SB = ""; if (Object.prototype.toString.call(D) == '[object Array]') { for (var i = 0; i < D.length; i++) { SB += arguments.callee(D[i], T) } } else { SB = D == "" ? "" : f(D, T) } return SB };
        $.LoadJs = function (e, t) { if (e) { var a, i, n = null, o = document.head || document.getElementsByTagName("head")[0] || document.documentElement; a = document.createElement("script"), a.async = e.type.toLowerCase(), e.scriptCharset && (a.charset = e.scriptCharset), a.src = e.url, e.timeout > 0 && (i = setTimeout(function () { n(), t && t(408, "failure") }, e.timeout)), a.onload = a.onreadystatechange = function (e, n) { (n || !a.readyState || /loaded|complete/.test(a.readyState)) && (a.onload = a.onreadystatechange = null, o && a.parentNode && o.removeChild(a), a = void 0, n || (clearTimeout(i), t && t(200, "success"))) }, a.onerror = function () { n(), t && t(500, "failure") }, o.insertBefore(a, o.firstChild), n = function () { a && a.onload(0, 1) } } }, $.getDomain = function (e) { var t = document.domain.replace(/dealer.|club./, ""); return "m" === e ? t : "club" === e ? "club." + t : "dealer" === e ? "dealer." + t : void 0 };
        $.getDomain = function (a) { var b = document.domain.replace(/dealer.|club.|buy./, ""); return "m" === a ? b : "club" === a ? "club." + b : "dealer" === a ? "dealer." + b : void 0 };
        var Config = Config || {};
        Config["BrandId"] = 15, Config["SeriesId"] = 202, Config["CityId"] = Auto.areautil.getCityId(), Config["FlagState"] = true, Config["list"] = List = { "specKCountList": [{ "SpecId": 6893, "Counts": 1 }, { "SpecId": 8239, "Counts": 8 }, { "SpecId": 8240, "Counts": 22 }, { "SpecId": 10459, "Counts": 8 }, { "SpecId": 10460, "Counts": 1 }, { "SpecId": 11406, "Counts": 14 }, { "SpecId": 12882, "Counts": 10 }, { "SpecId": 13051, "Counts": 7 }, { "SpecId": 14642, "Counts": 8 }, { "SpecId": 14643, "Counts": 18 }, { "SpecId": 14644, "Counts": 13 }, { "SpecId": 14645, "Counts": 4 }, { "SpecId": 14646, "Counts": 2 }, { "SpecId": 14647, "Counts": 2 }, { "SpecId": 17084, "Counts": 24 }, { "SpecId": 17088, "Counts": 5 }, { "SpecId": 17089, "Counts": 2 }, { "SpecId": 17163, "Counts": 4 }, { "SpecId": 17165, "Counts": 1 }], "specGroupByYearList": [{ "Year": 2013, "SpecGroupList": [{ "Displacement": 2, "Enginepower": 184, "ISClassic": 0, "FlowmodeId": 2, "FlowmodeName": "\u6DA1\u8F6E\u589E\u538B", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 14643, "name": "2013\u6B3E 520i \u5178\u96C5\u578B", "logo": "http://car0.autoimg.cn/upload/2013/3/20/201303201850376404435.jpg", "year": 2013, "minprice": 455000, "maxprice": 455000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 2, "enginepower": 184, "ispreferential": 0, "istaxrelief": 0, "order": 91192, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 2, "Enginepower": 245, "ISClassic": 0, "FlowmodeId": 2, "FlowmodeName": "\u6DA1\u8F6E\u589E\u538B", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 14647, "name": "2013\u6B3E 528i xDrive\u8C6A\u534E\u578B", "logo": "http://car0.autoimg.cn/upload/2013/6/20/201306201901361114122.jpg", "year": 2013, "minprice": 703000, "maxprice": 703000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u56DB\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 2, "enginepower": 245, "ispreferential": 0, "istaxrelief": 0, "order": 91210, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 3, "Enginepower": 258, "ISClassic": 0, "FlowmodeId": 1, "FlowmodeName": "\u81EA\u7136\u5438\u6C14", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 14642, "name": "2013\u6B3E 530i \u9886\u5148\u578B \u65C5\u884C\u7248", "logo": "http://car0.autoimg.cn/upload/2013/6/20/201306201920109854122.jpg", "year": 2013, "minprice": 674000, "maxprice": 674000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 3, "enginepower": 258, "ispreferential": 0, "istaxrelief": 0, "order": 91216, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 3, "Enginepower": 306, "ISClassic": 0, "FlowmodeId": 2, "FlowmodeName": "\u6DA1\u8F6E\u589E\u538B", "ParamIsShow": 0, "FuelTypeId": 3, "SpecList": [{ "id": 14644, "name": "2013\u6B3E 535i \u9886\u5148\u8FD0\u52A8\u578B", "logo": "http://car0.autoimg.cn/upload/2012/12/11/20121211200623223264.jpg", "year": 2013, "minprice": 688000, "maxprice": 688000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 91222, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }, { "id": 14645, "name": "2013\u6B3E 535i \u8C6A\u534E\u8FD0\u52A8\u578B", "logo": "http://car0.autoimg.cn/upload/2012/12/11/20121211200705489264.jpg", "year": 2013, "minprice": 762000, "maxprice": 762000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 91666, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }, { "id": 14646, "name": "2013\u6B3E 535i xDrive\u8C6A\u534E\u578B", "logo": "http://car0.autoimg.cn/upload/2012/12/11/20121211200752476264.jpg", "year": 2013, "minprice": 784000, "maxprice": 784000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u56DB\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 91672, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }, { "id": 11036, "name": "2013\u6B3E ActiveHybrid 5", "logo": "http://car0.autoimg.cn/upload/2014/8/8/20140808191311041510411.jpg", "year": 2013, "minprice": 878000, "maxprice": 878000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 91678, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 3, "Enginepower": 306, "ISClassic": 0, "FlowmodeId": 2, "FlowmodeName": "\u6DA1\u8F6E\u589E\u538B", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 14644, "name": "2013\u6B3E 535i \u9886\u5148\u8FD0\u52A8\u578B", "logo": "http://car0.autoimg.cn/upload/2012/12/11/20121211200623223264.jpg", "year": 2013, "minprice": 688000, "maxprice": 688000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 91222, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }, { "id": 14645, "name": "2013\u6B3E 535i \u8C6A\u534E\u8FD0\u52A8\u578B", "logo": "http://car0.autoimg.cn/upload/2012/12/11/20121211200705489264.jpg", "year": 2013, "minprice": 762000, "maxprice": 762000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 91666, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }, { "id": 14646, "name": "2013\u6B3E 535i xDrive\u8C6A\u534E\u578B", "logo": "http://car0.autoimg.cn/upload/2012/12/11/20121211200752476264.jpg", "year": 2013, "minprice": 784000, "maxprice": 784000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u56DB\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 91672, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }, { "id": 11036, "name": "2013\u6B3E ActiveHybrid 5", "logo": "http://car0.autoimg.cn/upload/2014/8/8/20140808191311041510411.jpg", "year": 2013, "minprice": 878000, "maxprice": 878000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 91678, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }] }, { "Year": 2012, "SpecGroupList": [{ "Displacement": 2, "Enginepower": 184, "ISClassic": 0, "FlowmodeId": 2, "FlowmodeName": "\u6DA1\u8F6E\u589E\u538B", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 12882, "name": "2012\u6B3E 520i \u5178\u96C5\u578B", "logo": "http://car0.autoimg.cn/upload/spec/12882/201206141657240623655.jpg", "year": 2012, "minprice": 453000, "maxprice": 453000, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 2, "enginepower": 184, "ispreferential": 0, "istaxrelief": 0, "order": 82984, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 2, "Enginepower": 245, "ISClassic": 0, "FlowmodeId": 2, "FlowmodeName": "\u6DA1\u8F6E\u589E\u538B", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 13051, "name": "2012\u6B3E 528i xDrive\u8C6A\u534E\u578B", "logo": "http://car0.autoimg.cn/upload/spec/13051/201206141716089493655.jpg", "year": 2012, "minprice": 692600, "maxprice": 692600, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u56DB\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 2, "enginepower": 245, "ispreferential": 0, "istaxrelief": 0, "order": 84856, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 3, "Enginepower": 258, "ISClassic": 0, "FlowmodeId": 1, "FlowmodeName": "\u81EA\u7136\u5438\u6C14", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 11406, "name": "2012\u6B3E 530i \u9886\u5148\u578B \u65C5\u884C\u7248", "logo": "http://car0.autoimg.cn/upload/spec/11406/20111222190542034272.jpg", "year": 2012, "minprice": 666600, "maxprice": 666600, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 3, "enginepower": 258, "ispreferential": 0, "istaxrelief": 0, "order": 85096, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }] }, { "Year": 2011, "SpecGroupList": [{ "Displacement": 0, "Enginepower": 0, "ISClassic": 0, "FlowmodeId": 1, "FlowmodeName": "\u81EA\u7136\u5438\u6C14", "ParamIsShow": 0, "FuelTypeId": 0, "SpecList": [{ "id": 7085, "name": "2011\u6B3E \u57FA\u672C\u578B", "logo": "http://car0.autoimg.cn/upload/spec/6559/6559808415632.jpg", "year": 2011, "minprice": 0, "maxprice": 0, "transmission": "", "state": 40, "drivingmodename": null, "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 0, "enginepower": 0, "ispreferential": 0, "istaxrelief": 0, "order": 76624, "specisimage": 1, "paramisshow": 1, "isclassic": 0 }, { "id": 7317, "name": "2011\u6B3E \u65C5\u884C\u7248", "logo": "http://car0.autoimg.cn/upload/spec/7317/20100314131116065264.jpg", "year": 2011, "minprice": 0, "maxprice": 0, "transmission": "", "state": 40, "drivingmodename": null, "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 0, "enginepower": 0, "ispreferential": 0, "istaxrelief": 0, "order": 76630, "specisimage": 1, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 2.5, "Enginepower": 204, "ISClassic": 0, "FlowmodeId": 1, "FlowmodeName": "\u81EA\u7136\u5438\u6C14", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 10459, "name": "2011\u6B3E 523i \u9886\u5148\u578B", "logo": "http://car0.autoimg.cn/upload/spec/10459/20110712180258598264.jpg", "year": 2011, "minprice": 492600, "maxprice": 492600, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 2.5, "enginepower": 204, "ispreferential": 0, "istaxrelief": 0, "order": 74968, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 3, "Enginepower": 306, "ISClassic": 0, "FlowmodeId": 2, "FlowmodeName": "\u6DA1\u8F6E\u589E\u538B", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 8240, "name": "2011\u6B3E 535i \u9886\u5148\u8FD0\u52A8\u578B", "logo": "http://car0.autoimg.cn/upload/spec/8240/20100921171829873240.jpg", "year": 2011, "minprice": 677600, "maxprice": 677600, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 76606, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }, { "id": 8239, "name": "2011\u6B3E 535i \u8C6A\u534E\u8FD0\u52A8\u578B", "logo": "http://car0.autoimg.cn/upload/spec/8239/20100922002118029272.jpg", "year": 2011, "minprice": 751600, "maxprice": 751600, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 76612, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }, { "id": 10460, "name": "2011\u6B3E 535i xDrive\u8C6A\u534E\u578B", "logo": "http://car0.autoimg.cn/upload/spec/10460/20110712180314989264.jpg", "year": 2011, "minprice": 773600, "maxprice": 773600, "transmission": "8\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u56DB\u9A71", "flowmodeid": 2, "flowmodename": "\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 76618, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 3, "Enginepower": 306, "ISClassic": 0, "FlowmodeId": 5, "FlowmodeName": "\u53CC\u6DA1\u8F6E\u589E\u538B", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 6559, "name": "2011\u6B3E 535i", "logo": "http://car0.autoimg.cn/upload/spec/6559/20100412084659104264.jpg", "year": 2011, "minprice": 0, "maxprice": 0, "transmission": "", "state": 40, "drivingmodename": null, "flowmodeid": 5, "flowmodename": "\u53CC\u6DA1\u8F6E\u589E\u538B", "displacement": 3, "enginepower": 306, "ispreferential": 0, "istaxrelief": 0, "order": 76600, "specisimage": 1, "paramisshow": 1, "isclassic": 0 }] }, { "Displacement": 4.4, "Enginepower": 407, "ISClassic": 0, "FlowmodeId": 5, "FlowmodeName": "\u53CC\u6DA1\u8F6E\u589E\u538B", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 8621, "name": "2011\u6B3E 550i", "logo": "http://car0.autoimg.cn/upload/spec/8621/20100925090242982264.jpg", "year": 2011, "minprice": 0, "maxprice": 0, "transmission": "", "state": 40, "drivingmodename": null, "flowmodeid": 5, "flowmodename": "\u53CC\u6DA1\u8F6E\u589E\u538B", "displacement": 4.4, "enginepower": 407, "ispreferential": 0, "istaxrelief": 0, "order": 80146, "specisimage": 1, "paramisshow": 1, "isclassic": 0 }] }] }, { "Year": 2010, "SpecGroupList": [{ "Displacement": 0, "Enginepower": 0, "ISClassic": 0, "FlowmodeId": 1, "FlowmodeName": "\u81EA\u7136\u5438\u6C14", "ParamIsShow": 0, "FuelTypeId": 0, "SpecList": [{ "id": 7152, "name": "2010\u6B3E ActiveHybrid", "logo": "http://car0.autoimg.cn/upload/spec/7152/20100227093607575264.jpg", "year": 2010, "minprice": 0, "maxprice": 0, "transmission": "", "state": 40, "drivingmodename": null, "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 0, "enginepower": 0, "ispreferential": 0, "istaxrelief": 0, "order": 30634, "specisimage": 1, "paramisshow": 1, "isclassic": 0 }] }] }, { "Year": 2006, "SpecGroupList": [{ "Displacement": 4.8, "Enginepower": 367, "ISClassic": 0, "FlowmodeId": 1, "FlowmodeName": "\u81EA\u7136\u5438\u6C14", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 2076, "name": "2006\u6B3E 550i", "logo": "http://car0.autoimg.cn/upload/spec/2076/2076234515043.jpg", "year": 2006, "minprice": 1280000, "maxprice": 1280000, "transmission": "6\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 4.8, "enginepower": 367, "ispreferential": 0, "istaxrelief": 0, "order": 28276, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }] }, { "Year": 2004, "SpecGroupList": [{ "Displacement": 4.4, "Enginepower": 333, "ISClassic": 0, "FlowmodeId": 1, "FlowmodeName": "\u81EA\u7136\u5438\u6C14", "ParamIsShow": 0, "FuelTypeId": 1, "SpecList": [{ "id": 543, "name": "2004\u6B3E 545i", "logo": "http://car0.autoimg.cn/upload/spec/543/543125894329.jpg", "year": 2004, "minprice": 890000, "maxprice": 890000, "transmission": "6\u6321\u624B\u81EA\u4E00\u4F53", "state": 40, "drivingmodename": "\u524D\u7F6E\u540E\u9A71", "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 4.4, "enginepower": 333, "ispreferential": 0, "istaxrelief": 0, "order": 2788, "specisimage": 0, "paramisshow": 1, "isclassic": 0 }] }] }, { "Year": 2001, "SpecGroupList": [{ "Displacement": 0, "Enginepower": 0, "ISClassic": 0, "FlowmodeId": 1, "FlowmodeName": "\u81EA\u7136\u5438\u6C14", "ParamIsShow": 0, "FuelTypeId": 0, "SpecList": [{ "id": 6893, "name": "2001\u6B3E \u57FA\u672C\u578B", "logo": "http://car0.autoimg.cn/upload/spec/6893/20091225135353048264.jpg", "year": 2001, "minprice": 0, "maxprice": 0, "transmission": "", "state": 40, "drivingmodename": null, "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 0, "enginepower": 0, "ispreferential": 0, "istaxrelief": 0, "order": 28, "specisimage": 1, "paramisshow": 1, "isclassic": 0 }] }] }, { "Year": 1994, "SpecGroupList": [{ "Displacement": 0, "Enginepower": 0, "ISClassic": 0, "FlowmodeId": 1, "FlowmodeName": "\u81EA\u7136\u5438\u6C14", "ParamIsShow": 0, "FuelTypeId": 0, "SpecList": [{ "id": 16687, "name": "\u4EE5\u5F80\u7ECF\u5178\u7248", "logo": "http://car0.autoimg.cn/upload/2013/8/17/2013081715464195974.jpg", "year": 1994, "minprice": 0, "maxprice": 0, "transmission": "", "state": 40, "drivingmodename": null, "flowmodeid": 1, "flowmodename": "\u81EA\u7136\u5438\u6C14", "displacement": 0, "enginepower": 0, "ispreferential": 0, "istaxrelief": 0, "order": 85126, "specisimage": 1, "paramisshow": 1, "isclassic": 0 }] }] }] };
        Config["TabIndex"] = 1, Config["SeriesName"] = "宝马5系(进口)";
        var CollectConfig = { brandid: Config.BrandId, seriesid: Config.SeriesId, specid: 0, action: 'select' };
    </script>
    <script type="text/javascript" src="http://x.autoimg.cn/com/com.ashx?path=[/m/js/v1.6.2/,area/citySelect.min.js,Car/collect_new.min.js,Car/Series/seriesTab.min.js,Car/Series/seriesInfo_new.min.js]"></script>
    <script type="text/javascript">
        $("#JingXuanBody li img").error(function () {
            this.src = "http://x.autoimg.cn/m/images/280x210_mrtp.png";
        });
        if ((navigator.userAgent.indexOf('JUC') < 0 && navigator.userAgent.indexOf('UCBrowser') < 0) || navigator.userAgent.indexOf('iPh') > -1) {
            $.getScript("/Ashx/public/HeaderLayerRecApp.ashx?hchannelid=34&lchannelid=13");
        }
    </script>
    <script type="text/javascript"  charset="utf-8" src="http://33.autohome.com.cn/njs/1074.js?v=xx"></script>
    <script type="text/javascript" src="http://33.autohome.com.cn/ashow/m-all.js"></script>
    <script type="text/javascript">
        Zepto(function ($) {
            var _wz = ["i3x1me", "hgc86q"];
            $.each(_wz, function (key, value) {
                var ad = displyDiv = $("#" + value), b = value.indexOf("div_") > -1;
                if (b) ad = $("#" + value.replace("div_", ""));
                if (ad.text() && ad.text().trim() != '') { displyDiv.show(); }
            });
        })
    </script>
    <script type="text/javascript">
        var ab_utmx = ''; function abtest() { } (function () { var k = '1300', d = document, c = d.cookie; function f(n) { if (c) { var i = c.indexOf(n + '='); if (i > -1) { var j = c.indexOf(';', i); var x = escape(c.substring(i + n.length + 1, j < 0 ? c.length : j)); var o = x.indexOf(k); if (o > -1) { var l = x.indexOf('.', o); var y = x.substring(o, l < 0 ? x.length : l); return y } } return '' } } ab_utmx = f('ab_mseries') })();
        var pvTrack = function () { };
        pvTrack.site = 1211001; pvTrack.category = 48; pvTrack.subcategory = 33; pvTrack.series = Config.SeriesId;
        var url_stats = "http://stats.autohome.com.cn/pv_count.php?SiteId=";
        (function () {
            if (typeof (pvTrack) !== "undefined") {
                setTimeout("func_stats()", 3000);
                var doc = document, t = pvTrack;
                var pv_site, pv_category, pv_subcategory, pv_object, pv_series, pv_type, pv_typeid, pv_spec, pv_level, pv_dealer, pv_ref, pv_cur;
                pv_ref = escape(doc.referrer); pv_cur = escape(doc.URL);
                pv_site = t.site; pv_category = t.category; pv_subcategory = t.subcategory; pv_object = t.object; pv_series = t.series; pv_type = t.type; pv_typeid = t.typeid; pv_spec = t.spec; pv_level = t.level; pv_dealer = t.dealer;
                url_stats += pv_site + (pv_category != null ? "&CategoryId=" + pv_category : "") + (pv_subcategory != null ? "&SubCategoryId=" + pv_subcategory : "") + (pv_object != null ? "&objectid=" + pv_object : "") + (pv_series != null ? "&seriesid=" + pv_series : "") + (pv_type != null ? "&type=" + pv_type : "") + (pv_typeid != null ? "&typeid=" + pv_typeid : "") + (pv_spec != null ? "&specid=" + pv_spec : "") + (pv_level != null ? "&jbid=" + pv_level : "") + (pv_dealer != null ? "&dealerid=" + pv_dealer : "") + "&ref=" + pv_ref + "&cur=" + pv_cur + "&rnd=" + Math.random() + "&abtest=" + ab_utmx;//增加abtest参数;
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

        var _hmt = _hmt || [];
        (function () {
            var hm = document.createElement('script'); hm.type = 'text/javascript'; hm.async = true;
            hm.src = '//hm.baidu.com/h.js?c3d5d12c0100a78dd49ba1357b115ad7';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(hm, s);
        })();

    </script>
</body>
</html>
