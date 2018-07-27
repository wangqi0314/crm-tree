/// <reference path="../../../handler/ajax_report.aspx" />
/// <reference path="../../../handler/ajax_report.aspx" />
$(document).ready(function () {
    JavascriptPagination(1);
    ExpendSearch();

    $(".Cam_add").click(function () {
        window.location.href = "/manage/campaign/edit_campaign.aspx";
    });
    $(".Beneficiary").click(function () {
        window.location.href = "/manage/campaign/CampaignBeneficiary.aspx";
    });
});

var _run_pr = 0;
function RunTemp(RP_Code) {
    _run_pr = 10;
    //$("tr.trSelected:first a>i.btnRun:first", "div.pt10").click();
    var _url = $("tr.trSelected:first a>i.btnRun:first", "div.pt10").attr("_hidurl");
    $.post("/handler/ajax_report.aspx", { action: "get_report", RP_Code: RP_Code,PR:10 }, function (data) {
        Runs(RP_Code, data.RP_Name_EN, data.RP_Name_EN, _url);
    }, "json");    
}

function Runs(RP_Code, Name_EN, Name_CN, Url) {
    var urlBefore = '/templete/report/DataGrid.html?MF_FL_FB_Code=';
    var urlAfter = '&width=992&height=315&M=43&NB=1';

    var title = $('.tabs-selected').text();
    var C = $("#" + title).val();
    var pr = _run_pr;
    if (C == 1)
    {
        urlBefore = '/templete/report/BarChats.aspx?MF_FL_FB_Code=';
        urlAfter = '&width=900&height=315';
    }
    else
    {
        urlBefore = '/templete/report/DataGrid.html?PR=' + _run_pr + '&MF_FL_FB_Code=';
        urlAfter = '&width=992&height=315&M=43&NB=1&IsProc=0&PS=50';
    }
    var pr_export = _run_pr;
    _run_pr = 0;

    $.ajax({
        type: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        url: "/manage/Report/list_report.aspx/getFields_List_Code",
        data: "{RP_Code:'" + RP_Code + " '}",
        success: function (data) {
            if ($.trim(data.d) != "") {
                var para;
                if (C == 1) {
                    para = Url + "&PR=" + pr;
                }
                else
                    para = urlBefore + data.d + urlAfter;
                $.open({
                    id: 'open',
                    url: para,
                    title: Name_EN,
                    width: 950,
                    height: 400,
                    modal: false,
                        btnsbar: [{
                        text: 'Export',                  //按钮文本
                        action: 'Export_1'             //按钮 action 值，用于按钮事件触发 唯一
                    }, {
                        text: 'Cancel',                  //按钮文本
                        action: 'Cancel_2'             //按钮 action 值，用于按钮事件触发 唯一
                }],
                        callback: function (action, iframe) {
                        if (action == "Export_1") {
                            $.ajax({
                                type: "POST",
                                dataType: 'json',
                                    contentType: "application/json; charset=utf-8",
                                    url: "/manage/Report/list_report.aspx/Expore_Ex",
                                    data: "{Pl_Code:'" + data.d + "',PR:" + pr_export + "}",
                                    success: function (data) {
                                        var _fileName = data.d;
                                        window.location.href = "/handler/Downloads.aspx?T=1&fileName=" + _fileName;
                            }
                        });
                    }
                }
                });
                }
                else
                    {
                        $.alert('No data！', 'Successfully saved');
            }
        }
    });
    $("#open_ok").find("span").html("Update");
    $("#open_cancel").find("span").html("Cancel");
}

function EditTags(rp_code) {
    $.topOpen({
        title: _lng.campaign
        , url: '/manage/campaign/ParamValueModify.aspx?RP_Code=' + rp_code + "&_winID=" + _guid
        , width: 650
        , height: 500
    });
}
function EditTags2(Rp_Code) {
    $.open({
        id: 'open',
        url: '/manage/Report/Update_Targeted.aspx?RP_Code=' + Rp_Code + '',
        title: 'Update Report Parameter',
        width: 600,
        height: 300,
        modal: false,
        btnsbar: [{
            text: 'Run',
            action: 'Run_1'
        }, {
            text: 'Update',
            action: 'Update_2'
        }, {
            text: 'Cancel',
            action: 'Cancel_3'
        }]
,
        callback: function (action, iframe) {
            var t = this;
            if (action == "Run_1") {
                //$.close(t.id);
                Runs(Rp_Code);
            } else if (action == "Update_2") {
                var PL_Codelist = $(iframe.document).find(".Paramterslist").find(".PL_Code");
                var Paramterslist = $(iframe.document).find(".Paramterslist").find(".PL_Val");
                var PL_Codes = "";
                if (Paramterslist.length <= 0) {
                    return false;
                }
                PL_Codes = strConnect(PL_Codelist); //用逗号连接字符串
                Paramters = strConnect(Paramterslist); //用逗号连接字符串

                $.ajax({
                    type: "POST",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    url: "/manage/Report/Update_Targeted.aspx/updateTags",
                    data: "{PL_Codes:'" + PL_Codes + " ',Paramters:'" + Paramters + "'}",
                    success: function (data) {
                        if (data.d != null) {
                            location.reload();
                        }
                    }
                });
            }

        }
    });
    $("#open_ok").find("span").html("Update");
    $("#open_cancel").find("span").html("Cancel");
}
function strConnect(str) {
    var Paramters = "";
    for (var i = 0; i < str.length; i++) {
        if (i == str.length - 1) {
            Paramters += str[i].value;
        } else {
            Paramters += str[i].value + ",";
        }
    }
    return Paramters;
}

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
    var C1 = getUrlParam("C1");
    var C2 = getUrlParam("C2");

    var title = $('.tabs-selected').text();
    var C = $("#"+title).val();

    //alert(C);

    var $data = $("body").data("order");
    if ($data == null) {
        $("body").data("order", {
            order: "RP_Sort",
            orderStyle: "asc"
        });
        $data = $("body").data("order");
    }
    Loading();
    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/handler/ajax_report.aspx",
        data: { action: "list_report", page: intCurrentPage, pagesize: $.trim($("#txtPageSize").val()), keyword: escape($.trim($("#txtKeyWords").val())), dtstartDate: $.trim($("#startDate").val()), dtendDate: $.trim($("#endDate").val()), sortfield: $data.order, sortrule: $data.orderStyle ,C1:C1,C2:C2,C:C},
        success: function (data) {
            if (10 < data.length) {
                $("#divGridList").html(data);
                $("#divGridList1").html(data);
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

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}