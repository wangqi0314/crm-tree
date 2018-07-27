$(document).ready(function () {
    $("#btnSearch").click(function () {//搜索按钮点击事件
        JavascriptPagination(1);//列表加载
    });
    JavascriptPagination(1);
    ExpendSearch();

    $(".Cam_add").click(function () {
        var CT = getUrlParam("CT");
        var params = "";
        if (CT) {
            params += "&CT=" + CT;
        }
        var url = "/manage/campaign/Campaign.aspx?_cmd=cp" + params;
        window.location.href = url;
    });
    $(".Beneficiary").click(function () {
        window.location.href = "/manage/campaign/CampaignBeneficiary.aspx";
    });
        //点击tr变色
    $("#divGridList").find("table tr td").live("click", function () {
        var CG_Code = $(this).parent().attr("value");
        //CG_Code = "/manage/campaign/edit_campaign.aspx?id=" + CG_Code;
        var CT = getUrlParam("CT");
        var params = "";
        if (CT) {
            params += "&CT=" + CT;
        }
        if (CG_Code) {
            params += "&CG_Code=" + CG_Code;
        }
        var url = "/manage/campaign/Campaign.aspx?_cmd=cp" + params;
        window.document.location.href = url;
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
function JavascriptPagination(CurrentPage,ict) {
    intCurrentPage = CurrentPage;
    var title = $('.tabs-selected').text();
    var CT;
    try{
        CT = _camp_list.getTabCode();
     
    }
    catch (e){
        CT = $("#" + title).val();
    }
    if (CT == 0) return false;
    if (CT == null && CT == undefined)
        CT = getUrlParam("CT");
   
    if (CT == null) return;

    var CA = getUrlParam("CA");

    var $data = $("body").data("order");
    if ($data == null) {
        $("body").data("order", {
            order: "CG_Update_dt",
            orderStyle: "desc"
        });
        $data = $("body").data("order");
    }
    if (ict != 1) { //ict indicates if this is the initial load cycle... Fariborz
        Loading();
//        alert('CT=' + CT + ' CA=' + CA);
        $.ajax({
            type: "POST",
            dataType: "text",
            url: "/handler/ajax_campaign.aspx",
            data: { action: "list_campaign", page: intCurrentPage, pagesize: $.trim($("#txtPageSize").val()), keyword: escape($.trim($("#txtKeyWords").val())), dtstartDate: $.trim($("#startDate").val()), dtendDate: $.trim($("#endDate").val()), sortfield: $data.order, sortrule: $data.orderStyle, CT: CT, CA: CA },
            success: function (data) {
                if (10 < data.length) {
                    $("div.pt10").html(data);
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
}
//delete campaign
function Delete(event,id, file) {
    event.stopPropagation();
    top.$.confirm('Continue Delete?', 'Tips', function (action) {
        if (action == 'ok') {
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/handler/ajax_campaign.aspx",
                data: { action: "delete_campaign", id: id },
                success: function (data) {
                    if (0 < data) {
                        $.ajax({
                            type: "POST",
                            dataType: "text",
                            url: "/handler/ajax_campaign.aspx",
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
function Run(event,id) {
    event.stopPropagation();
    $.confirm(
        'Are you sure you want to generate the report?',
        'Tips',
        function (action) {
            if (action == 'ok') {
                $.ajax({
                    type: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    url: "/manage/campaign/list_campaign.aspx/CampaignRun",
                    data: "{CG_Code:" + id + "}",
                    success: function (data) {
                        var err = data.d;
                        if (err == null) {
                            $.alert("The report did not perform!", "Tips");
                        }else if (err == "-1") {
                            $.alert("The report did not execute the statement!", "Tips");
                            $("#asyncbox_alert_ok").find("span").html("OK");                         
                        }else if (err == "-2") {
                            $.alert("The report did not perform error!", "Tips");
                            $("#asyncbox_alert_ok").find("span").html("OK");
                        }else if (err == "-3") {
                            $.alert("The report parameter error!", "Tips");
                            $("#asyncbox_alert_ok").find("span").html("OK");
                        } else if (err == "-4") {
                            $.alert("Report SQL error!", "Tips");
                            $("#asyncbox_alert_ok").find("span").html("OK");
                        } else if (err == "0") {
                            $.tips("Report generation success!", "Tips");
                        } else if(err=="-5"){
                            $.alert("Sorry, You can't generate the same compaign twice!", "Tips");
                            $("#asyncbox_alert_ok").find("span").html("OK");
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
    $("#asyncbox_confirm_ok").find("span").html("Continue");
    $("#asyncbox_confirm_cancel").find("span").html("Cancel");
}

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}