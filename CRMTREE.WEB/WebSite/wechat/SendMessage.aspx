<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="wechat_SendMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>微信公众平台</title>
    <link href="/images/Fun.ico" rel="Shortcut Icon" />
    <link rel="stylesheet" type="text/css" href="/css/Wechat/layout_head20273e.css" />
    <link rel="stylesheet" type="text/css" href="/css/Wechat/base211edb.css" />
    <link rel="stylesheet" type="text/css" href="/css/Wechat/lib1ffa7e.css" />

    <link rel="stylesheet" href="/css/Wechat/page_message1ec5f7.css" />
    <link rel="stylesheet" type="text/css" href="/css/Wechat/msg_sender1fd16f.css" />
    <link rel="stylesheet" type="text/css" href="/css/Wechat/emoji1ec663.css" />
    <link href="/css/Wechat/Msg_sendr.css" rel="stylesheet" />
    <script src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script src="/scripts/Wechat/W_SendMessage.js"></script>
    <script type="text/javascript">
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        $(document).ready(function () {
            Page();
            $("#js_submit").click(function () {
                //var area = $(".edit_area").html();
                var area = $(".edit_area").text();
                var OpenId = getUrlParam("o");
                data = { context: area, OpenId: OpenId };
                $.ajax({
                    type: "POST", dataType: "json", contentType: "application/json; charset=utf-8", url: "SendMessage.aspx/SendMess",
                    data: "{data:'" + unescape((JSON.stringify(data))) + "'}",
                    success: function (data) {
                        if (data.d == null) {
                            $('<div class="JS_TIPS page_tips error" id="wxTips"><div class="inner">Reply to fail!</div></div>').appendTo("body").fadeIn();
                            setTimeout(function () {
                                $('.JS_TIPS').fadeOut();
                            }, 3000);
                            return false;
                        } else {
                            var Targeted = data.d;
                            if (Targeted == 0) {
                                $('<div class="JS_TIPS page_tips success" id="wxTips"><div class="inner">Reply to success!</div></div>').appendTo("body").fadeIn();
                                setTimeout(function () {
                                    $('.JS_TIPS').fadeOut();
                                }, 3000);
                            } else {
                                $('<div class="JS_TIPS page_tips error" id="wxTips"><div class="inner">Reply to fail!</div></div>').appendTo("body").fadeIn();
                                setTimeout(function () {
                                    $('.JS_TIPS').fadeOut();
                                }, 3000);
                            }
                        }
                        // success error
                    }
                });
            })
        });
    </script>

</head>
<body class="zh_CN">
    <div class="head" id="header">
        <div class="head_box">
            <div class="inner wrp">
                <h1 class="logo"><a href="/" title="微信公众平台"></a></h1>
                <div class="account">
                    <div class="account_meta account_info account_meta_primary">
                        <a href="/cgi-bin/settingpage?t=setting/index&amp;action=index&amp;token=795126424&amp;lang=zh_CN" class="nickname">思树-大E库</a>
                        <span class="type_wrp">
                            <a href="/cgi-bin/settingpage?t=setting/index&amp;action=index&amp;token=795126424&amp;lang=zh_CN" class="type icon_service_label">服务号</a>
                            <a href="/merchant/store?action=detail&amp;t=wxverify/detail&amp;info=verify&amp;lang=zh_CN&amp;token=795126424" class="type icon_verify_label success">已认证</a>
                        </span>
                        <a href="/cgi-bin/settingpage?t=setting/index&amp;action=index&amp;token=795126424&amp;lang=zh_CN">
                            <img src="/misc/getheadimg?fakeid=2391543171&amp;token=795126424&amp;lang=zh_CN" class="avatar"></a>
                    </div>
                    <div id="accountArea" class="account_meta account_inbox account_meta_primary">
                        <a href="/cgi-bin/frame?t=notification/index_frame&amp;lang=zh_CN&amp;token=795126424" class="account_inbox_switch">
                            <i class="icon_inbox">通知</i>&nbsp;                </a>
                    </div>
                    <div class="account_meta account_logout account_meta_primary"><a id="logout" href="/cgi-bin/logout?t=wxm-logout&amp;lang=zh_CN&amp;token=795126424">退出</a></div>
                </div>
            </div>
        </div>
    </div>
    <div id="body" class="body page_message_send">
        <div id="js_container_box" class="container_box cell_layout side_l">
            <div class="col_side"></div>
            <div class="col_main">
                <div class="main_hd">
                    <h2>与&nbsp;<span id="js_nick_name"><%=name %></span>&nbsp;的聊天</h2>
                </div>
                <div class="main_bd">
                    <div id="msgSender">
                        <div class="msg_sender" id="js_msgSender">
                            <div class="msg_tab">
                                <ul class="tab_navs">
                                    <li class="tab_nav tab_text width5 selected" data-type="1" data-tab=".js_textArea" data-tooltip="文字">
                                        <a href="javascript:void(0);">&nbsp;<i class="icon_msg_sender"></i></a>
                                    </li>
                                    <li class="tab_nav tab_img width5" data-type="2" data-tab=".js_imgArea" data-tooltip="图片">
                                        <a href="javascript:void(0);">&nbsp;<i class="icon_msg_sender"></i></a>
                                    </li>
                                    <li class="tab_nav tab_audio width5" data-type="3" data-tab=".js_audioArea" data-tooltip="语音">
                                        <a href="javascript:void(0);">&nbsp;<i class="icon_msg_sender"></i></a>
                                    </li>
                                    <li class="tab_nav tab_video width5" data-type="15" data-tab=".js_videoArea" data-tooltip="视频">
                                        <a href="javascript:void(0);">&nbsp;<i class="icon_msg_sender"></i></a>
                                    </li>
                                    <li class="tab_nav tab_appmsg width5 no_extra" data-type="10" data-tab=".js_appmsgArea" data-tooltip="图文消息">
                                        <a href="javascript:void(0);">&nbsp;<i class="icon_msg_sender"></i></a>
                                    </li>
                                </ul>
                                <div class="tab_panel">
                                    <div class="tab_content" style="display: block;">
                                        <div class="js_textArea inner no_extra">
                                            <div class="emotion_editor">
                                                <div class="edit_area js_editorArea" contenteditable="true" style="overflow-y: auto; overflow-x: hidden;"></div>
                                                <div class="editor_toolbar">
                                                    <a href="javascript:void(0);" class="icon_emotion emotion_switch js_switch">表情</a>
                                                    <p class="editor_tip js_editorTip">还可以输入<em>600</em>字</p>
                                                    <div class="emotion_wrp js_emotionArea">
                                                        <ul class="emotions" onselectstart="return false;">
                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/0.gif" data-title="微笑" style="background-position: 0px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/1.gif" data-title="撇嘴" style="background-position: -24px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/2.gif" data-title="色" style="background-position: -48px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/3.gif" data-title="发呆" style="background-position: -72px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/4.gif" data-title="得意" style="background-position: -96px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/5.gif" data-title="流泪" style="background-position: -120px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/6.gif" data-title="害羞" style="background-position: -144px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/7.gif" data-title="闭嘴" style="background-position: -168px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/8.gif" data-title="睡" style="background-position: -192px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/9.gif" data-title="大哭" style="background-position: -216px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/10.gif" data-title="尴尬" style="background-position: -240px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/11.gif" data-title="发怒" style="background-position: -264px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/12.gif" data-title="调皮" style="background-position: -288px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/13.gif" data-title="呲牙" style="background-position: -312px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/14.gif" data-title="惊讶" style="background-position: -336px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/15.gif" data-title="难过" style="background-position: -360px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/16.gif" data-title="酷" style="background-position: -384px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/17.gif" data-title="冷汗" style="background-position: -408px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/18.gif" data-title="抓狂" style="background-position: -432px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/19.gif" data-title="吐" style="background-position: -456px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/20.gif" data-title="偷笑" style="background-position: -480px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/21.gif" data-title="可爱" style="background-position: -504px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/22.gif" data-title="白眼" style="background-position: -528px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/23.gif" data-title="傲慢" style="background-position: -552px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/24.gif" data-title="饥饿" style="background-position: -576px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/25.gif" data-title="困" style="background-position: -600px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/26.gif" data-title="惊恐" style="background-position: -624px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/27.gif" data-title="流汗" style="background-position: -648px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/28.gif" data-title="憨笑" style="background-position: -672px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/29.gif" data-title="大兵" style="background-position: -696px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/30.gif" data-title="奋斗" style="background-position: -720px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/31.gif" data-title="咒骂" style="background-position: -744px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/32.gif" data-title="疑问" style="background-position: -768px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/33.gif" data-title="嘘" style="background-position: -792px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/34.gif" data-title="晕" style="background-position: -816px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/35.gif" data-title="折磨" style="background-position: -840px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/36.gif" data-title="衰" style="background-position: -864px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/37.gif" data-title="骷髅" style="background-position: -888px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/38.gif" data-title="敲打" style="background-position: -912px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/39.gif" data-title="再见" style="background-position: -936px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/40.gif" data-title="擦汗" style="background-position: -960px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/41.gif" data-title="抠鼻" style="background-position: -984px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/42.gif" data-title="鼓掌" style="background-position: -1008px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/43.gif" data-title="糗大了" style="background-position: -1032px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/44.gif" data-title="坏笑" style="background-position: -1056px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/45.gif" data-title="左哼哼" style="background-position: -1080px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/46.gif" data-title="右哼哼" style="background-position: -1104px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/47.gif" data-title="哈欠" style="background-position: -1128px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/48.gif" data-title="鄙视" style="background-position: -1152px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/49.gif" data-title="委屈" style="background-position: -1176px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/50.gif" data-title="快哭了" style="background-position: -1200px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/51.gif" data-title="阴险" style="background-position: -1224px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/52.gif" data-title="亲亲" style="background-position: -1248px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/53.gif" data-title="吓" style="background-position: -1272px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/54.gif" data-title="可怜" style="background-position: -1296px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/55.gif" data-title="菜刀" style="background-position: -1320px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/56.gif" data-title="西瓜" style="background-position: -1344px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/57.gif" data-title="啤酒" style="background-position: -1368px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/58.gif" data-title="篮球" style="background-position: -1392px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/59.gif" data-title="乒乓" style="background-position: -1416px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/60.gif" data-title="咖啡" style="background-position: -1440px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/61.gif" data-title="饭" style="background-position: -1464px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/62.gif" data-title="猪头" style="background-position: -1488px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/63.gif" data-title="玫瑰" style="background-position: -1512px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/64.gif" data-title="凋谢" style="background-position: -1536px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/65.gif" data-title="示爱" style="background-position: -1560px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/66.gif" data-title="爱心" style="background-position: -1584px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/67.gif" data-title="心碎" style="background-position: -1608px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/68.gif" data-title="蛋糕" style="background-position: -1632px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/69.gif" data-title="闪电" style="background-position: -1656px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/70.gif" data-title="炸弹" style="background-position: -1680px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/71.gif" data-title="刀" style="background-position: -1704px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/72.gif" data-title="足球" style="background-position: -1728px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/73.gif" data-title="瓢虫" style="background-position: -1752px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/74.gif" data-title="便便" style="background-position: -1776px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/75.gif" data-title="月亮" style="background-position: -1800px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/76.gif" data-title="太阳" style="background-position: -1824px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/77.gif" data-title="礼物" style="background-position: -1848px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/78.gif" data-title="拥抱" style="background-position: -1872px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/79.gif" data-title="强" style="background-position: -1896px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/80.gif" data-title="弱" style="background-position: -1920px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/81.gif" data-title="握手" style="background-position: -1944px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/82.gif" data-title="胜利" style="background-position: -1968px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/83.gif" data-title="抱拳" style="background-position: -1992px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/84.gif" data-title="勾引" style="background-position: -2016px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/85.gif" data-title="拳头" style="background-position: -2040px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/86.gif" data-title="差劲" style="background-position: -2064px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/87.gif" data-title="爱你" style="background-position: -2088px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/88.gif" data-title="NO" style="background-position: -2112px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/89.gif" data-title="OK" style="background-position: -2136px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/90.gif" data-title="爱情" style="background-position: -2160px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/91.gif" data-title="飞吻" style="background-position: -2184px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/92.gif" data-title="跳跳" style="background-position: -2208px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/93.gif" data-title="发抖" style="background-position: -2232px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/94.gif" data-title="怄火" style="background-position: -2256px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/95.gif" data-title="转圈" style="background-position: -2280px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/96.gif" data-title="磕头" style="background-position: -2304px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/97.gif" data-title="回头" style="background-position: -2328px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/98.gif" data-title="跳绳" style="background-position: -2352px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/99.gif" data-title="挥手" style="background-position: -2376px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/100.gif" data-title="激动" style="background-position: -2400px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/101.gif" data-title="街舞" style="background-position: -2424px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/102.gif" data-title="献吻" style="background-position: -2448px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/103.gif" data-title="左太极" style="background-position: -2472px 0;"></i>
                                                            </li>

                                                            <li class="emotions_item">
                                                                <i class="js_emotion_i" data-gifurl="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/104.gif" data-title="右太极" style="background-position: -2496px 0;"></i>
                                                            </li>

                                                        </ul>
                                                        <span class="emotions_preview js_emotionPreviewArea"></span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab_content" style="display: none;">
                                        <div class="js_imgArea inner ">
                                        </div>
                                    </div>
                                    <div class="tab_content" style="display: none;">
                                        <div class="js_audioArea inner ">
                                        </div>
                                    </div>
                                    <div class="tab_content" style="display: none;">
                                        <div class="js_videoArea inner "></div>
                                    </div>
                                    <div class="tab_content" style="display: none;">
                                        <div class="js_appmsgArea inner "></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tool_area">
                            <div id="verifycode"></div>
                            <span class="btn btn_primary btn_input" id="js_submit">
                                <button>发送</button></span>
                        </div>
                    </div>
                    <div class="message_area">
                        <h4>最近聊天记录</h4>
                        <ul class="message_list" id="listContainer">
                         <%--   <li class="message_item replyed replying " id="msgListItem200866606" data-id="200866606">
                                <div class="message_opr">
                                    <a href="javascript:;" class="js_star icon18_common star_gray" action="" idx="200866606" starred="" title="收藏消息">取消收藏</a>
                                    <a href="javascript:;" data-id="200866606" data-tofakeid="1472492616" class="icon18_common reply_gray js_reply" title="快捷回复">快捷回复</a>
                                </div>
                                <div class="message_info">
                                    <div class="message_status"><em class="tips">已回复</em></div>
                                    <div class="message_time">16:17</div>
                                    <div class="user_info">
                                        <a href="/cgi-bin/singlesendpage?tofakeid=1472492616&amp;t=message/send&amp;action=index&amp;token=795126424&amp;lang=zh_CN" target="_blank" data-fakeid="1472492616" class="remark_name">王琪</a>
                                        <span class="nickname" data-fakeid="1472492616"></span>
                                        <a href="javascript:;" class="icon14_common edit_gray js_changeRemark" data-fakeid="1472492616" title="修改备注" style="display: none;"></a>
                                        <a target="_blank" href="/cgi-bin/singlesendpage?tofakeid=1472492616&amp;t=message/send&amp;action=index&amp;token=795126424&amp;lang=zh_CN" class="avatar" data-fakeid="1472492616">
                                            <img src="/misc/getheadimg?token=795126424&amp;fakeid=1472492616" data-fakeid="1472492616">
                                        </a>
                                    </div>
                                </div>
                                <div class="message_content text">
                                    <div id="wxMsg200866606" data-id="200866606" class="wxMsg">就</div>
                                </div>
                                <div id="quickReplyBox200866606" class="js_quick_reply_box quick_reply_box">
                                    <label for="" class="frm_label">快速回复:</label>
                                    <div class="emoion_editor_wrp js_editor">
                                        <div class="emotion_editor">
                                            <div class="edit_area js_editorArea" contenteditable="true" style="overflow-y: auto; overflow-x: hidden;"></div>
                                            <div class="editor_toolbar">
                                                <p class="editor_tip js_editorTip">还可以输入<em>140</em>字</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="verifyCode"></div>
                                    <p class="quick_reply_box_tool_bar">
                                        <span class="btn btn_primary btn_input" data-id="200866606">
                                            <button class="js_reply_OK" data-id="200866606" data-fakeid="1472492616">发送</button>
                                        </span>
                                        <a class="js_reply_pickup btn btn_default pickup" data-id="200866606" href="javascript:;">收起</a>
                                    </p>
                                </div>
                            </li>
                            <li class="message_item replyed" id="msgListItem200866605" data-id="200866605">
                                <div class="message_info">
                                    <div class="message_status"><em class="tips">已回复</em></div>
                                    <div class="message_time">16:16</div>
                                    <div class="user_info">
                                        <span data-fakeid="2391543171" class="remark_name">思树-大E库</span>
                                        <span class="nickname" data-fakeid="2391543171"></span>
                                        <span class="avatar" data-fakeid="2391543171">
                                            <img src="/misc/getheadimg?token=795126424&amp;fakeid=2391543171" data-fakeid="2391543171">
                                        </span>
                                    </div>
                                </div>
                                <div class="message_content text">
                                    <div id="wxMsg200866605" data-id="200866605" class="wxMsg">
                                        <img src="https://res.wx.qq.com/mpres/htmledition/images/icon/emotion/1.gif" width="24" height="24">
                                    </div>
                                </div>
                            </li>
                            <li class="message_item " id="msgListItem200861563" data-id="200861563">
                                <div class="message_opr">
                                    <a href="javascript:;" class="js_star icon18_common star_gray" action="" idx="200861563" starred="" title="收藏消息">取消收藏</a>
                                    <a href="javascript:;" data-id="200861563" data-tofakeid="1472492616" class="icon18_common reply_gray js_reply" title="快捷回复">快捷回复</a>
                                </div>
                                <div class="message_info">
                                    <div class="message_status"><em class="tips">已回复</em></div>
                                    <div class="message_time">昨天 18:18</div>
                                    <div class="user_info">
                                        <a href="/cgi-bin/singlesendpage?tofakeid=1472492616&amp;t=message/send&amp;action=index&amp;token=795126424&amp;lang=zh_CN" target="_blank" data-fakeid="1472492616" class="remark_name">王琪</a>
                                        <span class="nickname" data-fakeid="1472492616"></span>
                                        <a href="javascript:;" class="icon14_common edit_gray js_changeRemark" data-fakeid="1472492616" title="修改备注" style="display: none;"></a>
                                        <a target="_blank" href="/cgi-bin/singlesendpage?tofakeid=1472492616&amp;t=message/send&amp;action=index&amp;token=795126424&amp;lang=zh_CN" class="avatar" data-fakeid="1472492616">
                                            <img src="/misc/getheadimg?token=795126424&amp;fakeid=1472492616" data-fakeid="1472492616">
                                        </a>
                                    </div>
                                </div>
                                <div class="message_content text">
                                    <div id="wxMsg200861563" data-id="200861563" class="wxMsg">你好</div>
                                </div>
                                <div id="quickReplyBox200861563" class="js_quick_reply_box quick_reply_box">
                                    <label for="" class="frm_label">快速回复:</label>
                                    <div class="emoion_editor_wrp js_editor"></div>
                                    <div class="verifyCode"></div>
                                    <p class="quick_reply_box_tool_bar">
                                        <span class="btn btn_primary btn_input" data-id="200861563">
                                            <button class="js_reply_OK" data-id="200861563" data-fakeid="1472492616">发送</button>
                                        </span><a class="js_reply_pickup btn btn_default pickup" data-id="200861563" href="javascript:;">收起</a>
                                    </p>
                                </div>
                            </li>--%>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="tooltip" style="top: 237px; left: 315px; display: none;">
        <div class="tooltip_inner">文字</div>
        <i class="tooltip_arrow"></i>
    </div>
    <div class="tooltip" style="display: none;">
        <div class="tooltip_inner">图片</div>
        <i class="tooltip_arrow"></i>
    </div>
    <div class="tooltip" style="display: none;">
        <div class="tooltip_inner">语音</div>
        <i class="tooltip_arrow"></i>
    </div>
    <div class="tooltip" style="top: 237px; left: 516.96875px; display: none;">
        <div class="tooltip_inner">视频</div>
        <i class="tooltip_arrow"></i>
    </div>
    <div class="tooltip" style="top: 237px; left: 571.625px; display: none;">
        <div class="tooltip_inner">图文消息</div>
        <i class="tooltip_arrow"></i>
    </div>
</body>
</html>
