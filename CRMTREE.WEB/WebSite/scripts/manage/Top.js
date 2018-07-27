$(document).ready(function () {
    //1
    $(".nav>ul>li").mouseenter(function () {
        $(this).children("ul").slideToggle("slow", function () {
        });
    });
    $(".nav>ul>li").mouseleave(function () {
        $(this).children("ul").toggle();
    });
    //2
    $(".nav>ul>li>ul>li").mouseenter(function () {
        $(this).children("ul").css("margin-left", $(this).width());
        $(this).children("ul").slideToggle();
    });
    $(".nav>ul>li>ul>li").mouseleave(function () {
        $(this).children("ul").toggle();
    });
    //3
    $(".nav>ul>li>ul>li>ul>li").mouseenter(function () {
        $(this).children("ul").slideToggle();
    });
    $(".nav>ul>li>ul>li>ul>li").mouseleave(function () {
        $(this).children("ul").toggle();
    });
    //5
    $(".nav>ul>li>ul>li>ul>li>ul>li").mouseenter(function () {
        $(this).children("ul").slideToggle();
    });
    $(".nav>ul>li>ul>li>ul>li>ul>li").mouseleave(function () {
        $(this).children("ul").toggle();
    });
    $("#internationalization").val($(".Languages").html());
    getFlag();
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

    $("#Help").click(function () {
        $("div[id=newnotice]").css({ "right": "0px", "bottom": "1px" });
        $("div[id=newnotice]").slideDown("slow");

        /*setTimeout(function(){$("div[id=newnotice]").slideUp("slow")},10000);*/
    }).resize(function () {
        $("div[id=newnotice]").css({ "bottom": "" });
        $("div[id=newnotice]").css({ "right": "0px", "bottom": "1px" });
    });
    $(window).scroll(function () {
        var p = $(window).scrollTop();
        var b = $(window).scrollLeft();
        $("div[id=newnotice]").css({ "bottom": "0px" });
        $("div[id=newnotice]").css({ "right": "-" + b + "px", "bottom": "-" + p + "px" });
    });

    $("label[id=tomin]").click(function () {
        $("div[id=noticecon]", "div[id=newnotice]").slideUp();
    });

    $("label[id=tomax]").click(function () {
        $("div[id=noticecon]", "div[id=newnotice]").slideDown();
    });

    $(".toclose").click(function () {
        $("div[id=newnotice]").hide();
    });

});
function getFlag() {
    if ($("#internationalization").val() == "1") {
        $("#ImageFlag").attr("src", "/images/ChinaFlag.png");
    } else if ($("#internationalization").val() == "2") {
        $("#ImageFlag").attr("src", "/images/USFlag.PNG");
    }
}