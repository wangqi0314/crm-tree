$(document).ready(function () {
    $("#btnSearch").click(function () {//搜索按钮点击事件
        JavascriptPagination(1);//列表加载
    });
    JavascriptPagination(1);//
    ExpendSearch();
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
            order: "AU_Update_dt",
            orderStyle: "desc"
        });
        $data = $("body").data("order");
    }
    Loading();
    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/handler/User/ajax_AddUser.aspx",
        data: { action: "list_User", page: intCurrentPage, pagesize: $.trim($("#txtPageSize").val()), keyword: escape($.trim($("#txtKeyWords").val())), dtstartDate: $.trim($("#startDate").val()), dtendDate: $.trim($("#endDate").val()), sortfield: $data.order, sortrule: $data.orderStyle },
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
//delete User
function Delete(id, file) {
    top.$.confirm('Continue Delete?', 'Tips', function (action) {
        if (action == 'ok') {
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/handler/User/ajax_AddUser.aspx",
                data: { action: "delete_User", id: id },
                success: function (data) {
                    if (0 < data) {
                        $.ajax({
                            type: "POST",
                            dataType: "text",
                            url: "/handler/ajax_AddUser.aspx",
                            data: { action: "del_file", fullname: file },
                            success: function (data) {
                            }
                        });
                        top.$.tips("operation success!", "Tips");
                        JavascriptPagination(intCurrentPage);
                    }
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
    });
    $("#asyncbox_confirm_ok").find("span").html("Continue");
    $("#asyncbox_confirm_cancel").find("span").html("Cancel");
}