$(document).ready(function () {
    var Loging, Login, LoginFails, UserName, UserPass, error, we, un, pw, tv, home, About, Privacy, Products, Services, Contact,lang;
    if (Internationalization("language")) {
        error = "Please enter your user name and password";
        UserName = "Please enter your user name";
        UserPass = "Please enter your user password";
        Loging = "Loging....";
        Login = "login";
        LoginFails = "Login Fails!";
        we = "Welcome To DaeKu!";
        un = "UserName：";
        pw = "PassWord：";
        tv = "DaeKu LOGIN";
        home = "Home";
        About = "About us";
        Privacy = "Privacy & Terms";
        Products = "Products";
        Services = "Services";
        Contact = "Contact us";
        lang = 1;
    } else {
        error = "请输入用户名和密码";
        UserName = "请输入用户名";
        UserPass = "请输入密码";
        Loging = "登陆中...";
        Login = "登陆";
        LoginFails = "登陆失败";
        we = "欢迎来到DaeKu";
        un = "用户名:";
        pw = "密码:";
        tv = "DaeKu 登陆";
        home = "首页";
        About = "关于我们";
        Privacy = "隐私和条款";
        Products = "产品";
        Services = "服务";
        Contact = "联系我们";
        lang =2;
    }
    $(".title").html(we);
    $(".left_word").html(un);
    $(".left_word1").html(pw);
    $(".input_menu").val(Login);
    $("#home").html(home);
    $("#About").html(About);
    $("#Privacy").html(Privacy);
    $("#Products").html(Products);
    $("#Services").html(Services);
    $("#Contact").html(Contact);

    document.title=tv;
    $("#txt_username").focus(function () {
        SetKeyEnter();
    });
    $("#txt_password").focus(function () {
        SetKeyEnter();
    });
    $("#btnLogin").click(function () {        
        var username = $.trim($("#txt_username").val());
        var password = $.trim($("#txt_password").val());
        if ("" == username) {
            $("#txt_username").focus();
            $("#li_tips").text(UserName);
            return false;
        }
        if ("" == password) {
            $("#txt_password").focus();
            $("#li_tips").text(UserPass);
            return false;
        }
        $("#btnLogin").attr("value", Loging);
        $("#btnLogin").attr("disabled", "disabled");
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "/operation/OperationLogin.aspx/logins",
            data: "{username:'" + username + "',userpass:'" + password + "'}",
            success: function (data) {
                if (data.d != null) {
                    $("#btnLogin").attr("value", Loging);
                    window.location.href = data.d;
                }
                else {
                    $("#btnLogin").attr("value", Login);
                    $("#btnLogin").removeAttr("disabled");
                    $("#li_tips").text(LoginFails);
                }
            },
            error: function (err, err2, err3) {
                $("#btnLogin").attr("value", Login);
                $("#btnLogin").removeAttr("disabled");
                $("#li_tips").text(LoginFails);
                return false;
            }
        });
    });
    $("#internationalization").val(lang);
    $("#internationalization").change(function () {
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "/handler/International.aspx/SetLanguage",
            data: "{Key:" + $("#internationalization").val() + "}",
            success: function (data) {
                var JSONObjects = data.d;
                location.reload();
            },
            error: function (err, err2, err3) {
                alert(err3, err2);
                return false;
            }
        });
    });
    setto();
    $("#btnRegister").click(function () {
        window.location.href = '/Register.html';
    });
});


function setto() {
    if ($("#internationalization").val() == "1") {
        $("#ImageFlag").attr("src", "/images/ChinaFlag.Png");
    } else if ($("#internationalization").val() == "2") {
        $("#ImageFlag").attr("src", "/images/USFlag.png");
    }
}
 
    function SetKeyEnter() {
        document.onkeydown = function (event) {
            e = event ? event : (window.event ? window.event : null);
            if (e.keyCode == 13) {
                $("#btnLogin").click();
                return false;
            }
        }
    }
