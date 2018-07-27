$(document).ready(function () {
    $(".Contact_EAS_Close").click(function () {
        $(".Contact_EAS").hide();
    });
    $(".Car_Consultant .car_ad").click(function (event) {
        event.stopPropagation();
        var d = $(this).offset();
        var Pwidth = d.left, Pheight = d.top;
        $(".Contact_EAS").find(".DSName").html($(this).parent().find(".AD_Name").html());//经销商的名字
        var ad_code = $(this).find(".AdviserID").html();
        $(".Contact_EAS").find(".AD_name").html($(this).find(".AD_name").html()); //顾问的名字
        $(".Contact_EAS").find(".img1").attr("src", $(this).find(".img1").attr("src"));
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "Dashboard.aspx/getAdviserMessage",
            data: "{ AU_AD_Code:'" + ad_code + "'}",
            success: function (data) {
                var Ad = data.d;
                if (Ad != null || Ad != "") {
                    $(".Contact_EAS").find(".DS").html(Ad.DealerShip);
                    $(".Contact_EAS").find(".Address").html(Ad.Address);
                    $(".Contact_EAS").find(".Mob").html(Ad.Mobil);
                    $(".Contact_EAS").find(".Offi").html(Ad.Office);
                    $(".Contact_EAS").find(".email").html(Ad.Email);
                }
            },
            error: function (err, err2, err3) {
                $.alert(err3, err2);
                $("#asyncbox_alert_ok").find("span").html("ok");
                return false;
            }
        });
        $(".Contact_EAS").css({ position: "absolute", left: Pwidth - 120, top: Pheight + 50 }).show();
    });
    $(document).click(function (event) {
        var eo = $(event.target);
        if ($(".Car_Consultant .car_ad").is(":visible") && eo.attr("class") != "Contact_EAS" && !eo.parent(".Contact_EAS").length)
            $('.Contact_EAS').hide();
    });
    $(".adviser_Email").click(function () {
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
    $(".CarRecommendAdviser").each(function () {
        var Car_Code = $(this).find(".Car_CI_Code").html();
        if (Car_Code != null || Car_Code != "") {
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: "Dashboard.aspx/getCarRecommendAdviser",
                data: "{ Car_CI_Code:'" + Car_Code + "'}",
                success: function (data) {
                    var Adver = data.d;
                    if (Adver != null && Adver != "") {
                        $(".CarRecommendAdviser").each(function () {
                            if ($(this).find(".Car_CI_Code").html() == Car_Code) {
                                if (Adver.Adviser_List[0].DE_Picture_FN !== "") {
                                    $(this).find(".Car_AD_1 .img1").attr("src", "/images/Adviser/" + Adver.Adviser_List[0].DE_Picture_FN);
                                } else {
                                    $(this).find(".Car_AD_1 .img1").attr("src", "/images/Adviser/defaultAvatar.gif");
                                }
                                $(this).find(".Car_AD_1 .AdviserID").append(Adver.Adviser_List[0].AU_Code);
                                $(this).find(".Car_AD_1 .AD_name").text(Adver.Adviser_List[0].AU_Name);

                                if (Adver.Adviser_List[1].DE_Picture_FN != "") {
                                    $(this).find(".Car_AD_2 .img1").attr("src", "/images/Adviser/" + Adver.Adviser_List[1].DE_Picture_FN);
                                } else {
                                    $(this).find(".Car_AD_2 .img1").attr("src", "/images/Adviser/defaultAvatar.gif");
                                }
                                $(this).find(".Car_AD_2 .AdviserID").append(Adver.Adviser_List[1].AU_Code);
                                $(this).find(".Car_AD_2 .AD_name").text(Adver.Adviser_List[1].AU_Name);

                                $(this).find(".AD_Code").html(Adver.Adviser_List[0].AD_Code);
                                $(this).find(".AD_Name").html(Adver.Adviser_List[0].AD_Name_EN);
                            }
                            $(".CarRecommendAdviser").show();
                            $(".hidCar_Consultant").hide();
                        });
                    }
                },
                error: function (err, err2, err3) {
                    $.alert(err3, err2);
                    $("#asyncbox_alert_ok").find("span").html("ok");
                    return false;
                }
            });
        }
    });
});