var code; //在全局定义验证码
$(document).ready(function () {
    createCode();
    $("#txt_username").blur(function () {
        var userName = $.trim($(this).val());
        if (userName == "" || userName.length <= 5) {
            return false;
        }
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "/operation/OperationLogin.aspx/VerificationUsername",
            data: "{username:'" + userName + "'}",
            success: function (data) {
                if (data.d > 0) {
                    $(".userPrompt").css("color", "Red").html("The user name has been registered");
                }
                else {
                    $(".userPrompt").css("color", "Green").html("The user name can use");
                }
            }
        });
    })
    $("#btnRegister").click(function () {
        var username = $.trim($("#txt_username").val());
        var password = $.trim($("#txt_password").val());
        var password_1 = $.trim($("#txt_password_1").val());
        var Verification = $.trim($("#txt_Verification").val());
        var codes = $.trim($(".code lable").html());
        if (username == "" || username.length <= 5) {
            $(".userPrompt").css("color", "Red").html("Name at least 6");
            return false;
        }
        if (password == "" || password.length <= 5) {
            $(".PassPrompt").css("color", "Red").html("The password that is at least 6 bits");
            return false;
        }
        else { $(".PassPrompt").html(""); }
        if (password_1 != password) {
            $(".PassPrompt1").css("color", "Red").html("Confirm password is not correct");
            return false;
        }
        else { $(".PassPrompt1").html(""); }
        if (Verification != code) {
            $(".codes").css("color", "Red").html("Verification code input error");
            return false;
        }
        else { $(".codes").html(""); }
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "/operation/OperationLogin.aspx/Registration",
            data: "{username:'" + username + "',password:'" + password + "'}",
            success: function (data) {
                if (data.d > 0) {
                    $("#li_tips").css("color", "Green").html("Registration successful");
                    $("#txt_username").val("");
                    $("#txt_password").val("");
                    $("#txt_password_1").val("");
                    $("#txt_Verification").val("");
                    $(".userPrompt").html("");
                }
                else {
                    $("#li_tips").css("color", "Red").html("Failed to register");
                }
            }
        });
    });
});
//产生验证码
function createCode() {
    code = "";
    var codeLength = 4;//验证码的长度
    var checkCode = document.getElementById("code");
    var random = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
    'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');//随机数
    for (var i = 0; i < codeLength; i++) {//循环操作
        var index = Math.floor(Math.random() * 36);//取得随机数的索引（0~35）
        code += random[index];//根据索引取得随机数加到code上
    }
    $(".code lable").html(code);//把code值赋给验证码
}
//校验验证码
function validate() {
    var inputCode = document.getElementById("input").value.toUpperCase(); //取得输入的验证码并转化为大写
    if (inputCode.length <= 0) { //若输入的验证码长度为0
        alert("请输入验证码！"); //则弹出请输入验证码
    }
    else if (inputCode != code) { //若输入的验证码与产生的验证码不一致时
        alert("验证码输入错误！@_@"); //则弹出验证码输入错误
        createCode();//刷新验证码
        document.getElementById("input").value = "";//清空文本框
    }
    else { //输入正确时
        alert("^-^"); //弹出^-^
    }
}