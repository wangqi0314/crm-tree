<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmwxRegister.aspx.cs" Inherits="wechat_frmwxRegister" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="/css/Wechat/page_login213560.css" rel="stylesheet" />
    <script src="/wechatjs/jquery-1.10.2.min.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        //获取6位随机验证码
        function random() {
            var num = "";
            for (i = 0; i < 6; i++) {
                num = num + Math.floor(Math.random() * 10);
            }
            return num;
        }
        //验证码有效期倒计时
        function RemainTime(iTime) {
            var iSecond;
            var sSecond = "", sTime = "";
            if (iTime >= 0) {
                iSecond = parseInt(iTime % 300);
                if (iSecond >= 0) {
                    sSecond = "有效时间" + iTime + "秒";
                }
                sTime = "<span style='color:darkorange;font-size:13px;'>" + sSecond + "</span>";
                if (iTime == 0) {
                    clearTimeout(Account);
                    sTime = "<span style='color:red;font-size:12px;'>验证码已过期</span>";
                }
                else {
                    Account = setTimeout(function () { RemainTime(iTime) }, 1000);
                }
                iTime = iTime - 1;
            }
            $("#endtime").html(sTime);
        }
        var CookieUtil = {
            get: function (name) {
                var cookieName = encodeURIComponent(name) + "=",
                cookieStart = document.cookie.indexOf(cookieName),
                cookieValue = null;

                if (cookieStart > -1) {
                    var cookieEnd = document.cookie.indexOf(";", cookieStart)
                    if (cookieEnd == -1) {
                        cookieEnd = document.cookie.length;
                    }
                    cookieValue = decodeURIComponent(document.cookie.substring(cookieStart
                        + cookieName.length, cookieEnd));
                }
                return cookieValue;
            },
            set: function (name, value, expires, path, domain, secure) {
                var cookieText = encodeURIComponent(name) + "=" + encodeURIComponent(value);
                if (expires instanceof Date) {
                    cookieText += "; expires=" + expires.toGMTString();
                }
                if (path) {
                    cookieText += "; path=" + path;
                }
                if (domain) {
                    cookieText += "; domain=" + domain;
                }
                if (secure) {
                    cookieText += "; secure=" + secure;
                }

                document.cookie = cookieText;
            },
            unset: function (name, path, domain, secure) {
                this.set(name, "", new Date(0), path, domain, secure);
            }
        };
        var Checks = {
            Mobile: function () {
                if (!(/^1[3|4|5|8][0-9]\d{8}$/.test($("#account").val()))) {
                    $("#error").html("<font>不是完整的手机号</font>");
                    $("#account").focus();
                    return false;
                } else { return true; }
            },
            pwd: function () {
                if (!(/^[a-zA-Z0-9]{6,16}$/.test($("#pwd").val()))) {
                    $("#error").html("<font>密码至少6为最多16位</font>");
                    $("#pwd").focus();
                    return false;
                } else { return true; }
            },
            qrpwd: function () {
                if (!(/^[a-zA-Z0-9]{6,16}$/.test($("#qrpwd").val()))) {
                    $("#error").html("<font>密码确认错误</font>");
                    $("#pwd").focus();
                    return false;
                } else if ($("#pwd").val() != $("#qrpwd").val()) {
                    $("#error").html("<font>密码确认错误</font>");
                    $("#pwd").focus();
                    return false;
                } else { return true; }
            },
            yzm: function () {
                var yzm = $("#yzm").val();
                if (yzm != CookieUtil.get("va")) {
                    $("#error").html("<font>验证码错误</font>");
                    $("#yzm").focus();
                    return false;
                } else { return true; }
            },
            Open: function () {
                if (this.Mobile() && this.pwd() && this.qrpwd()) { $("#error").html(""); return true; } else { return false; }
            }
        };
        $(document).ready(function () {
            $("#account").blur(function () {
                if (Checks.Mobile()) {
                    $.ajax({
                        type: "POST", dataType: 'json', contentType: "application/json; charset=utf-8",
                        url: "frmwxRegister.aspx/VerificationUsername",
                        data: "{mobile:'" + $("#account").val() + "'}",
                        success: function (data) {
                            if (data.d != 0) {
                                $("#error").html("<font>该手机号已经注册！</font>");
                                $("#account").focus();
                            }
                            else { $("#error").html("<font>该手机号通过验证！</font>"); }
                        }
                    });
                }
            })
            $("#validate").click(function () {
                if (Checks.Open()) {
                    var ran = random();
                    $.ajax({
                        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8",
                        url: "frmwxRegister.aspx/SendMesageing", data: "{mobile:" + $("#account").val() + ", ran:'" + ran + "'}", async: false,
                        success: function (data) {
                            if (data.d != null) {
                                var err = data.d;
                                if (err == "1") {
                                    RemainTime(90);
                                    CookieUtil.set("va", ran);
                                    setTimeout(function () { CookieUtil.unset("va"); }, 90000);
                                }
                            }
                        }
                    });
                }
            });
            $("#RegisterUrl").click(function () {
                if (Checks.Open() && Checks.yzm()) {
                    $.ajax({
                        type: "POST", dataType: 'json', contentType: "application/json; charset=utf-8",
                        url: "frmwxRegister.aspx/WechatRegister",
                        data: "{mobile:'" + $("#account").val() + "',Pwd:'" + $("#pwd").val() + "'}",
                        success: function (data) {
                            if (data.d =="S0") {
                                $("#error").html("<font>该手机号已经注册成功！</font>");
                            }
                            else { $("#error").html("<font>该手机号注册失败！</font>"); }
                        }
                    });
                }
            });
        });
    </script>
</head>
<body>
    <div style="position: relative; height: 40px; background-color: #85B716; width: 100%; padding-top: 0px;">
        <div style="padding: 5px 10px;">注册</div>
    </div>
    <div class="login_frame">
        <div class="login_err_panel" id="error"></div>
        <form class="login_form" id="loginForm" runat="server">
            <div class="login_input_panel" id="js_mainContent">
                <div class="login_input">
                    <i class="icon_login un"></i>
                    <input type="text" placeholder="你的手机号码" id="account" name="account" />
                </div>
                <div class="login_input">
                    <i class="icon_login pwd"></i>
                    <input type="password" placeholder="密码" id="pwd" name="password" />
                </div>
                <div class="login_input">
                    <i class="icon_login pwd"></i>
                    <input type="password" placeholder="确认密码" id="qrpwd" name="password" />
                </div>
                <div class="login_input">
                    <i class="icon_login pwd"></i>
                    <input type="text" placeholder="验证码" id="yzm" name="yzm" />
                </div>
                <div class="login_validate">
                    <a class="btn_register" title="点击获取验证码" href="javascript:" id="validate" style="padding-left: 0px">获取验证码</a>
                    <span id="endtime" style="padding: 10px 0px;"></span>
                </div>
            </div>
            <div class="login_btn_panel" style="text-align: right;">
                <a class="btn_register" title="没有账户点击注册" href="javascript:" id="RegisterUrl">注册</a>

            </div>
        </form>
    </div>
</body>
</html>
