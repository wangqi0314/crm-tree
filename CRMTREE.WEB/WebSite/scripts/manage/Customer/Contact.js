$(document).ready(function () {
    $(".Title").click(function () {
        $(".Title1").removeClass("Title1");
        $(this).addClass("Title1");
    });
    $(".email").click(function () {
        $(this).parent().parent().parent().find(".Contact_EAS").hide();
        $.open({
            id: 'open',
            url: '/manage/customer/SendEmail.aspx',
            title: 'SendEmail',
            width: 640,
            height: 480,
            modal: false,
            btnsbar: $.btn.OKCANCEL.concat(),
            callback: function (action, iframe) {
                if (action == 'ok') {
                    var mailTitle = $.trim($(iframe.document).find("#mailTitleInfo").val()); //邮件标题
                    var mailContent = $.trim($(iframe.document).find("#TextArea1").val()); //邮件内容
                    if ("" == mailTitle) {
                        top.$.alert('Please enter the message header！', 'Operation tips', function (action) {
                            if (action == 'ok') {
                                $(iframe.document).find("#mailTitleInfo").focus();
                            }
                        });
                        return false;
                    }
                    if ("" == mailContent) {
                        top.$.alert('Please enter the message content！', 'Operation tips', function (action) {
                            if (action == 'ok') {
                                $(iframe.document).find("#TextArea1").focus();
                            }
                        });
                        return false;
                    }
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        url: "Dashboard.aspx/SendEmail",
                        data: "{ 'Title':'" + mailTitle + "', 'Content':'" + mailContent + "'}",
                        success: function (data) {
                            $.tips(data.d, 'success');
                        },
                        error: function (err, err2, err3) {
                            $.alert(err3, err2);
                            $("#asyncbox_alert_ok").find("span").html("ok");
                            return false;
                        }
                    });
                }
            }
        });
        $("#open_ok").find("span").html("Send");
        $("#open_cancel").find("span").html("Cancel");
    });
    $(".Mobile").click(function () {
        $(this).parent().parent().parent().find(".Contact_EAS").hide();
        $.open({
            id: 'open',
            url: '/manage/customer/SendMessage.aspx',
            title: 'SendMessage',
            width: 640,
            height: 480,
            modal: false,
            btnsbar: $.btn.OKCANCEL.concat(),
            callback: function (action, iframe) {
                if (action == 'ok') {
                    var mailTitle = $.trim($(iframe.document).find("#mailTitleInfo").val()); //邮件标题
                    var mailContent = $.trim($(iframe.document).find("#TextArea1").val()); //邮件内容
                    if ("" == mailTitle) {
                        top.$.alert('Please enter the message header！', 'Operation tips', function (action) {
                            if (action == 'ok') {
                                $(iframe.document).find("#mailTitleInfo").focus();
                            }
                        });
                        return false;
                    }
                    if ("" == mailContent) {
                        top.$.alert('Please enter the message content！', 'Operation tips', function (action) {
                            if (action == 'ok') {
                                $(iframe.document).find("#TextArea1").focus();
                            }
                        });
                        return false;
                    }
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        url: "Dashboard.aspx/SendEmail",
                        data: "{ 'Title':'" + mailTitle + "', 'Content':'" + mailContent + "'}",
                        success: function (data) {
                            $.tips(data.d, 'success');
                        },
                        error: function (err, err2, err3) {
                            $.alert(err3, err2);
                            $("#asyncbox_alert_ok").find("span").html("ok");
                            return false;
                        }
                    });
                }
            }
        });
        $("#open_ok").find("span").html("Send");
        $("#open_cancel").find("span").html("Cancel");
    });
});