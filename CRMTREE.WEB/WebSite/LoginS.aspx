<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginS.aspx.cs" Inherits="Login" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetopLogin.ascx" TagPrefix="uc1" TagName="CrmTreetopLogin" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="icon" type="image/ico" href="images/favicon.ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Resources.CRMTREESResource.LoginTitel %></title>
    <link type="text/css" href="css/style.css" rel="stylesheet" />
    <link type="text/css" href="js/jquery.bxslider/jquery.bxslider.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="js/jquery.bxslider/jquery.bxslider.min.js"></script>
    <script type="text/javascript" src="/scripts/common/setCookie.js"></script>

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
                        $("#btnLogin").attr("value", Loging);
                        if (window.opener != null && !window.opener.closed) {
                            window.opener.location.reload();
                            window.opener.focus();
                        }
                        window.close();

                        //if (window.opener != null && !window.opener.closed) {
                        //    window.opener.location.reload();
                        //    window.opener.focus();
                        //}
                        //window.close();
                       // var _p = ;
                       // $(parent.document.getElementById('_FG2')).remove();
                       // parent.coloseDF();
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
    <form id="form1" runat="server" style="align-content: center;">
        <div id="container" style="left: 0px; width: 311px; position: absolute;">
            <div class="login_title" style="width=311px;">
            </div>
            <div class="login_mid" style="padding-top: 12px;">
                <div style="color: red; font-weight: bold; width: 250px; text-align: center; padding: 0px 18px 10px 18px;">
                    <%=Resources.CRMTREESResource.LoginExpired %>
                </div>
                <ul>
                    <li class="l_inputs">
                        <div class="o_inputs" style="color: red; font-size: large; font-weight: bold">
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
                        <input type="button" value="<%=Resources.CRMTREESResource.LoginBtLogin %>" id="btnLogin" class="menu_login" style="cursor: pointer;" onclick="login(); return false;" /></li>
                </ul>
            </div>
            <div class="login_bot"></div>
        </div>
    </form>
</body>
</html>
