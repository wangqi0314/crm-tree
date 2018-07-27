$(document).ready(function () {
    $(document).ajaxComplete(function (event, request, settings) {
        $(".Beneficiarys table tr:even").not(":first").addClass("even");
        $(".Beneficiarys table tr").not(":first").hover(function () {
            $(this).addClass("hover");
        }, function () {
            $(this).removeClass("hover");
        });
        //点击tr变色
        $(".Beneficiarys").find("tr").not(":first").not("#tr_noSelect").click(function () {
            $(".Beneficiarys").find("tr").removeClass("trSelected");
            $(this).addClass("trSelected");
        });
    });
    //加载电话列表
    getPhone_List();
    Messaging_List();
    Email_List();
});
function getPhone_List() {
    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/handler/ajax_CampaignBeneficiary.aspx",
        data: { action: "phone_list" },
        success: function (data) {
            if (10 < data.length)
                $("#Phone_List").html(data);
            else if (-1 == data) {
                top.$.alert('None of the event!', 'Tips');
                return false;
            }
        }
    });
}
function Messaging_List() {
    Loading();
    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/handler/ajax_CampaignBeneficiary.aspx",
        data: { action: "messaging_list" },
        success: function (data) {
            if (10 < data.length)
                $("#Messaging_List").html(data);
            else if (-1 == data) {
                top.$.alert('None of the event!', 'Tips');
                return false;
            }
        }
    });
}
function Email_List() {
    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/handler/ajax_CampaignBeneficiary.aspx",
        data: { action: "email_list" },
        success: function (data) {
            if (10 < data.length)
                $("#Email_List").html(data);
            else if (-1 == data) {
                top.$.alert('None of the event!', 'Tips');
                return false;
            }
        }
    });
}
function SendPhone(User_Code, CG_Code, filename) {
    $.ajax({
        type: "POST", dataType: 'json', contentType: "application/json; charset=utf-8",
        url: "/handler/ajax_CampaignBeneficiary.aspx/SendPhone",
        data: "{User_Code:'" + User_Code + "',CG_Code:'" + CG_Code + "',filename:'" + filename + "'}",
        success: function (data) {
            if (data.d != null||data.d!="-1") {
                $(".Phone_Info").empty();
                $(".Phone_Info").html(data.d);
                $.open({
                    id: 'open',
                    url: '/manage/campaign/HtmlFile.aspx?User_Code=' + User_Code + '&CG_Code=' + CG_Code + '&filename=' + filename + '',
                    title: 'Html', width: 600, height: 550,
                    modal: false,
                    btnsbar: $.btn.OKCANCEL.concat(),
                    callback: function (action, iframe) {
                        if (action == 'ok') {
                            
                          
                        }
                    }
                });
                $("#open_ok").find("span").html("ok");
                $("#open_cancel").find("span").html("Cancel");
            }
        }
    });
}
function SendMesageing(User_Code, CG_Code, filename, mobile) {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "/handler/ajax_CampaignBeneficiary.aspx/SendMesageing",
        data: "{User_Code:'" + User_Code + "',CG_Code:'" + CG_Code + "',filename:'" + filename + "',mobile:'" + mobile + "'}",
        success: function (data) {
            if (data.d != null || data.d != "-1") {
                //alert(data.d);
                $(".Messaging_Info").empty();
                $(".Messaging_Info").html(data.d);
            }
        }
    });
}
function EmailView(User_Code, CG_Code, filename) {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "/handler/ajax_CampaignBeneficiary.aspx/EmailView",
        data: "{User_Code:'" + User_Code + "',CG_Code:'" + CG_Code + "',filename:'" + filename + "'}",
        success: function (data) {
            if (data.d != null || data.d != "-1") {
                $(".Email_Infos").empty();
                $(".Email_Infos").html(data.d);
            }
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
function SendEmail(email, User_Code, CG_Code, filename) {
    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "/handler/ajax_CampaignBeneficiary.aspx/sendEmail",
        data: "{email:'" + email + "',User_Code:'" + User_Code + "',CG_Code:'" + CG_Code + "',filename:'" + filename + "'}",
        success: function (data) {
            if (data.d != null) {
                if (data.d = "0") {
                    $.alert("Mail success!", "Confirmation");
                }
                else {
                    $.alert("Send mail failed!", "Confirmation");
                }
            }
        },
        error: function (err, err2, err3) {
            $.alert(err3, err2);
            $("#asyncbox_alert_ok").find("span").html("ok");
            return false;
        }
    });
}
