<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetopLogin.ascx" TagPrefix="uc1" TagName="CrmTreetopLogin" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ShuNovo</title>
    <link rel="icon" type="image/ico" href="images/favicon.ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Resources.CRMTREESResource.LoginTitel %></title>
    <link type="text/css" href="css/style.css" rel="stylesheet" />
    <link type="text/css" href="js/jquery.bxslider/jquery.bxslider.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="js/jquery.bxslider/jquery.bxslider.min.js"></script>
    <script type="text/javascript" src="/scripts/common/setCookie.js"></script>

    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>
    <script type="text/javascript">
        var Login, Loging, userNameEmpty, PasswordEmpy, LoginFails;
        if (Internationalization("language")) {
            Login = "Login";
            Loging = "Loading...";
            userNameEmpty = "Login Name can't be empty!";
            PasswordEmpy = "Password can't be empty!";
            LoginFails = "Login Fails!";
        } else {
            Login = "登陆";
            Loging = "登陆...";
            userNameEmpty = "用户名吧不能为空!";
            PasswordEmpy = "登陆密码不能为空!";
            LoginFails = "登陆失败!";
        }
        function login() {
            $("#error").html("<font color=\"red\">  </font>");
            var username = $("#username").val();
            var userpwd = $("#userpwd").val();
            //var userpwdE = des("shunovo2015", userpwd, 1, 0);

            //alert(userpwdE);
            if (username == "") {
                $("#error").html("<font color=\"red\">" + userNameEmpty + "</font>");
                $("#username").focus();
                return false;
            }
            if (userpwd == "") {
                $("#error").html("<font color=\"red\">" + PasswordEmpy + "</font>");
                $("#userpwd").focus();
                return false;
            }
            $("#btnLogin").attr("value", Loging);
            $("#btnLogin").attr("disabled", "disabled");
            $.ajax({
                type: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                url: "/operation/OperationLogin.aspx/logins",
                data: "{username:'" + username + "',userpass:'" + userpwd + "'}",
                success: function (data) {
                    if (data.d != null) {
                        //alert(data.d);
                        //alert("in");
                        //alert(Loging);
                        $("#btnLogin").attr("value", Loging);
                        //alert(data.d);
                        //window.location.href = "http://210.22.99.130:9005/Main.aspx?M=43";
                        window.location.href = data.d;
                    }
                    else {
                        $("#btnLogin").attr("value", Login);
                        $("#btnLogin").removeAttr("disabled");
                        $("#li_tips").text(LoginFails);
                    }
                }
            });
        }
        function SetKeyEnter() {
            document.onkeydown = function (event) {
                e = event ? event : (window.event ? window.event : null);
                if (e.keyCode == 13) {
                    document.getElementById("btnLogin").click();
                    return false;
                }
            }
        }
        function goUrl(x) {
            window.location.href = x;
        }
        $(document).ready(function () {
            $(".MyPros").hide();
            $(".signout").hide();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <uc1:CrmTreetopLogin runat="server" ID="CrmTreetopLogin" />
            <div id="cont_login">
                <div class="login_box">
                    <div class="login_title"></div>
                    <div class="login_mid">
                        <ul>
                            <li class="l_inputs">
                                <div class="o_inputs">
                                    <asp:Label ID="error" runat="server" Text=""></asp:Label>
                                </div>
                            </li>
                            <li class="l_title"><%=Resources.CRMTREESResource.LoginName %></li>
                            <li class="l_input">
                                <input class="login_input" type="text" name="username" id="username" onfocus="SetKeyEnter();" /></li>
                            <li class="l_title"><%=Resources.CRMTREESResource.LoginPassword %></li>
                            <li class="l_input">
                                <input class="login_input" type="password" name="userpwd" id="userpwd" onfocus="SetKeyEnter();" /></li>
                            <li class="l_menu">
                                <%--    <a href="javascript:;" onclick="javascript:goUrl('http://www.baidu.com');">跳转1</a>      
                            <a href="javascript:void(0);" onclick="javascript:goUrl('http://www.baidu.com');">跳转2</a>      
                            <a href="javascript:void(0);" onclick="javascript:goUrl('http://www.baidu.com');return false;">跳转3</a>      
                            <a href="#" onclick="javascript:goUrl('http://www.baidu.com');">跳转4</a>      
                            <a href="###" onclick="javascript:goUrl('http://www.baidu.com');">跳转5</a> --%>
                                <input type="button" value="<%=Resources.CRMTREESResource.LoginBtLogin %>" id="btnLogin" class="menu_login" style="cursor: pointer;" onclick="login(); return false;" /></li>
                        </ul>
                    </div>
                    <div class="login_bot"></div>
                </div>
                <div class="log_cont">
                    <div class="log_title"><%=Resources.CRMTREEResource.ScrollMess1 %></div>
                    <div class="login_img">
                        <div style="width: 372px; height: 266px;">
                            <ul class="bxslider">
                                <li>
                                    <img src="images/2.jpg" /></li>
                                <li>
                                    <img src="images/5.png" /></li>
                                <li>
                                    <img src="images/12.png" /></li>
                                <li>
                                    <img src="images/9.png" /></li>
                                <li>
                                    <img src="images/11.png" /></li>
                            </ul>
                        </div>
                        <script type="text/javascript">
                            var check = 1;
                            $('.bxslider').bxSlider({
                                mode: 'fade',
                                captions: true,
                                auto: true,
                                autoControls: true,
                                onSliderLoad: function () {
                                    // do funky JS stuff here
                                    //alert('1!');
                                },
                                onSlideAfter: function () {
                                    if (check == 1) {
                                        $(".log_title").html("<%=Resources.CRMTREEResource.ScrollMess2 %>");
                                        check = 2;
                                    } else if (check == 2) {
                                        $(".log_title").html("<%=Resources.CRMTREEResource.ScrollMess3 %>");
                                        check = 3;
                                    } else if (check == 3) {
                                        $(".log_title").html("<%=Resources.CRMTREEResource.ScrollMess4 %>");
                                        check = 4;
                                    } else if (check == 4) {
                                        $(".log_title").html("<%=Resources.CRMTREEResource.ScrollMess5 %>");
                                        check = 5;
                                    } else if (check == 5) {
                                        $(".log_title").html("<%=Resources.CRMTREEResource.ScrollMess1 %>");
                                            check = 1;
                                        }
                                }
                            });
                        </script>
                    </div>
                    <div class="cl" style="padding-top: 0px"></div>
                    <div class="log_cont">
                        <div class="log_module1">
                            <ul>
                                <li>
                                    <div class="fl">
                                        <img src="images/icon_login1.png" style="height: 40px;" />
                                    </div>
                                    <div class="l_title"><%=Resources.CRMTREESResource.LoginAssessment %></div>
                                </li>
                                <li class="l_word"><%=Resources.CRMTREESResource.LoginCon1 %></li>
                                <li><a href="Products.aspx" class="read_more"><%=Resources.CRMTREESResource.LoginReadMore %></a></li>
                            </ul>
                        </div>
                        <div class="log_module1">
                            <ul>
                                <li>
                                    <div class="fl">
                                        <img src="images/icon_login2.png" style="height: 40px;" />
                                    </div>
                                    <div class="l_title"><%=Resources.CRMTREESResource.LoginPlanning %></div>
                                </li>
                                <li class="l_word"><%=Resources.CRMTREESResource.LoginCon2 %></li>
                                <li><a href="Products.aspx" class="read_more"><%=Resources.CRMTREESResource.LoginReadMore1 %></a></li>
                            </ul>
                        </div>
                        <div class="log_module1">
                            <ul>
                                <li>
                                    <div class="fl">
                                        <img src="images/icon_login3.png" style="height: 40px;" />
                                    </div>
                                    <div class="l_title"><%=Resources.CRMTREESResource.LoginEngage %></div>
                                </li>
                                <li class="l_word"><%=Resources.CRMTREESResource.LoginCon3 %></li>
                                <li><a href="Services.aspx" class="read_more"><%=Resources.CRMTREESResource.LoginReadMore2 %></a></li>
                            </ul>
                        </div>
                        <div class="log_module2">
                            <ul>
                                <li>
                                    <div class="fl">
                                        <img src="images/icon_login4.png" style="height: 40px;" />
                                    </div>
                                    <div class="l_title"><%=Resources.CRMTREESResource.LoginEfficiencies %></div>
                                </li>
                                <li class="l_word"><%=Resources.CRMTREESResource.LoginCon4 %></li>
                                <li><a href="Services.aspx" class="read_more"><%=Resources.CRMTREESResource.LoginReadMore3 %></a></li>
                            </ul>
                        </div>
                        <div class="cl"></div>
                        <div class="log_bot">
                            <div class="log_seaos">
                                <ul>
                                    <li class="seaos_title"><%=Resources.CRMTREESResource.Loginlower1 %></li>
                                    <li>
                                        <img src="images/log_img1.jpg" /></li>
                                    <li class="seaos_orange"><%=Resources.CRMTREESResource.Loginlower7 %></li>
                                    <li class="seaos_infor"><%=Resources.CRMTREESResource.Loginlower8 %></li>
                                </ul>
                            </div>
                            <div class="log_service">
                                <%=Resources.CRMTREESResource.Loginlower2 %>
                                <ul>
                                    <li><a href="Services.aspx"><%=Resources.CRMTREESResource.Loginlower4 %></a></li>
                                    <li><a href="Services.aspx"><%=Resources.CRMTREESResource.Loginlower5 %></a></li>
                                    <li><a href="Services.aspx"><%=Resources.CRMTREESResource.Loginlower6 %></a></li>
                                </ul>
                            </div>
                            <div class="log_test">
                                <%=Resources.CRMTREESResource.Loginlower3 %>
                                <ul>
                                    <li class="log_head">
                                        <img src="images/log_head.jpg" /></li>
                                    <li class="log_word1"><%=Resources.CRMTREESResource.LoginLiuldala %><br />
                                        <span class="font_phone"><%=Resources.CRMTREESResource.LoginGlobalco %></span></li>
                                    <li class="log_word"><%=Resources.CRMTREESResource.Loginlower9 %></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bottom" style="border: 1px solid #d2d2d2; background-color: #F5EDD1; height: 20px; margin-bottom: 10px; text-align: center; color: #EBEBEB; text-decoration: none;">
                    <a href="Login.aspx"><%=Resources.CRMTREESResource.LoginHome %></a> &nbsp;&nbsp;
                 <a href="About.aspx"><%=Resources.CRMTREESResource.LoginAbout %></a>&nbsp;&nbsp;
                 <a href="Products.aspx"><%=Resources.CRMTREESResource.LoginProducts %></a>&nbsp;&nbsp;
                 <a href="Services.aspx"><%=Resources.CRMTREESResource.LoginServices %></a>&nbsp;&nbsp;
<%--                 <a href="Contactus.aspx"><%=Resources.CRMTREESResource.LoginContact %></a>
                    <a href="Privacy.aspx"><%=Resources.CRMTREESResource.LoginPrivacy %></a>&nbsp;&nbsp;--%>
                </div>
            </div>
    </form>
</body>
</html>
