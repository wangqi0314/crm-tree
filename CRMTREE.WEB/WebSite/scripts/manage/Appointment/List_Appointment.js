$(document).ready(function () {
    $("#btnSearch").click(function () {//搜索按钮点击事件
        JavascriptPagination(1);//列表加载
    });
    JavascriptPagination(1);//
    ExpendSearch();
    $(".App_Add").click(function () {
        window.location.href = "/manage/Appointment/Appointment.aspx";
    });

});
//折叠/展开搜索
var tag = "up";
function ExpendSearch() {
    if ("down" == tag) {
        $(".t_arrow").html(" <a href=\"javascript:;\"><img src=\"/images/arrow_up.png\" /></a>");
        $("#search").show();
        $("#divGridList").removeClass("pt10");
        tag = "up";
    }
    else {
        $(".t_arrow").html(" <a href=\"javascript:;\"><img src=\"/images/arrow_down.png\" title=\"expend to search\"/></a>");
        $("#search").hide();
        $("#divGridList").addClass("pt10");
        tag = "down";
    }
}
//得到数据查询以及分页码
//CurrentPage当前页
var intCurrentPage = 1;
function JavascriptPagination(CurrentPage) {
    intCurrentPage = CurrentPage;
    var $data = $("body").data("order");
    if ($data == null) {
        $("body").data("order", {
            order: "AP_Time",
            orderStyle: "desc"
        });
        $data = $("body").data("order");
    }
    Loading();
    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/handler/Appointment/Ajax_Appointment.aspx",
        data: { action: "list_car", page: intCurrentPage, pagesize: $.trim($("#txtPageSize").val()), keyword: escape($.trim($("#txtKeyWords").val())), dtstartDate: $.trim($("#startDate").val()), dtendDate: $.trim($("#endDate").val()), sortfield: $data.order, sortrule: $data.orderStyle },
        success: function (data) {
            if (10 < data.length) $("#divGridList").html(data);
            else if (-1 == data) {
                top.$.alert('None of the event!', 'Tips');
                return false;
            }
            else if (-2 == data) {
                top.$.alert('arguments are incorrec！', 'Tips');
                return false;
            }
            else if (-3 == data) {
                top.$.alert('operation failed!', 'Tips');
                return false;
            }
        }
    });
}

function Delete(id) {
    var ds="{ AP_Code:'"+ id +"'}";
    $.confirm('Countinue Delete?', 'Message Confirm ', function (action) {
        //confirm 返回三个 action 值，分别是 'ok'、'cancel' 和 'close'。
        if (action == 'ok') {
            $.ajax({
                type: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                url: "/handler/Appointment/Ajax_Appointment.aspx/DeleteApp",
                data: ds,
                success: function (data) {
                    if (data.d > 0) {
                        $.tips("Delete success!", "Tips");
                        JavascriptPagination(intCurrentPage);
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
}
function uncompile(code) {
    code = unescape(code);
    var c = String.fromCharCode(code.charCodeAt(0) - code.length);
    for (var i = 1; i < code.length; i++)
        return c;
}
