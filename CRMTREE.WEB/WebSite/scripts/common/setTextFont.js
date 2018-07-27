$(document).ready(function () {
    //Tip();
    //NoAccess();
    SetInputStyle();
    MonseFunction();
    $(document).ajaxComplete(function (event, request, settings) {
        ///排序开始
        $("th").has("span").not(".taxisCurrent").hover(function () {
            $(this).addClass("taxisOver");
        }, function () {
            $(this).removeClass("taxisOver");
        });
        if (!($("body").data("order") == null)) {
            if ($("body").data("order").orderStyle == "desc") {
                $(".taxisUp").attr("class", "taxisDown");
                $(".taxisDown").attr("title", "从高到低");
            } else {
                $(".taxisDown").attr("class", "taxisUp");
                $(".taxisUp").attr("title", "从低到高");
            }
        }
        $("th").has("span").not(".taxisCurrent").click(function () {
            $(".taxisCurrent").removeClass("taxisCurrent");
            $(".taxisDown").removeClass();
            $(".taxisUp").removeClass();
            $("th").children("span").addClass("taxis");
            $(this).addClass("taxisCurrent");
            if ($("body").data("order").orderStyle == "desc") {
                $(this).children("span").addClass("taxisDown");
            } else {
                $(this).children("span").addClass("taxisUp");
            }
        });
        ///排序结束
        $("div.pt10 ul:odd").not(".page ul").addClass("odd");
        $("div.pt10 ul").not(":first").not(".page ul").hover(function () {
            $(this).addClass("on");
        }, function () {
            $(this).removeClass("on");
        });
        $("div.pt10 table tr:even").not(":first").addClass("even");
        $("div.pt10 table tr").not(":first").hover(function () {
            $(this).addClass("hover");
        }, function () {
            $(this).removeClass("hover");
        });
        //点击tr变色
        $("div.pt10").find("tr").not(":first").not("#tr_noSelect").click(function () {
            $("div.pt10").find("tr").removeClass("trSelected");
            $(this).addClass("trSelected");            
        });
        //#region title
        $(".btnSearch").attr("title", "查询");
        $(".btnSaveAll").attr("title", "全部保存");
        $(".btnSave").attr("title", "保存");
        $(".btnAdd").not("[title]").attr("title", "添加");
        $(".btnDelete").not("[title]").attr("title", "删除");
        $(".btnModify").not("[title]").attr("title", "修改");
        $(".btnEnable").attr("title", "启用");
        $(".btnDisable").attr("title", "禁用");
        $(".btnCancel").not("[title]").attr("title", "取消");
        $(".btnRepeal").attr("title", "撤销");
        $(".btnChoose").attr("title", "选择");
        $(".btnCharge").attr("title", "缴费");
        $(".btnRemark").attr("title", "备注");
        $(".btnIgnore").attr("title", "忽略");
        $(".btnView").attr("title", "查看");
        $(".btnDetails").attr("title", "详细");
        $(".btnRefresh").attr("title", "刷新");
        $(".btnClose").attr("title", "关闭");
        $(".btnForward").attr("title", "前进");
        $(".btnGoBack").attr("title", "后退");
        $(".disable").not("[title]").attr("title", "权限不足");
        $(".btnFee").not("[title]").attr("title", "已缴费不能删除");
        $(".btnApproved").not("[title]").attr("title", "审核");
        $(".btnNoApproved").not("[title]").attr("title", "审核");

        //#endregion

        //#region 复选框大小
        if ($("#checkAll")[0]) {
            var obj = $("#checkAll").parents("th");
            var isie = ! -[1, ];
            var isie6 = isie && !window.xmlhttprequest;
            if (isie && isie6) {
                obj.removeClass().addClass("w50");
            };
        }
        //#endregion
        //$(":checkbox").live("change", function () {
        //    $(this).parents("tr").toggleClass("td-current", $(this).attr("checked"));
        //});

    });
});

//鼠标挪到文本框上的样式
function SetInputStyle() {
    $(".SetInputStyle").mouseover(function () {
        $(this).css("border", "#73CEF7 1px solid");
    });
    $(".SetInputStyle").click(function () {
        $(this).css("border", "#FF8C00 1px solid");
    });
    $(".SetInputStyle").focus(function () {
        $(this).css("border", "#FF8C00 1px solid");
    });
    $(".SetInputStyle").blur(function () {
        $(this).css("border", "#738C9C 1px solid");
    });
}
//鼠标挪到文本框上的样式
function MonseFunction() {
    $(".txtmouse").click(function () {
        $(this).css("border", "#FF8C00 1px solid");
    });
    $(".txtmouse").hover(function () {
        //鼠标在元素上移动
        $(this).css("border", "#73CEF7 1px solid");
    }, function () {
        //鼠标从元素上离开
        var objNull = $(this).attr('pip');
        if (objNull == "1") {
            if ($.trim($(this).val()) == "")
                $(this).css("border", "red 1px solid");
            else $(this).css("border", "#abc 1px solid");
        }
        else $(this).css("border", "#abc 1px solid");
    }).blur(function () {
        var objNull = $(this).attr('pip');
        if (objNull == "1") {
            if ($.trim($(this).val()) == "")
                $(this).css("border", "red 1px solid");
            else
                $(this).css("border", "#abc 1px solid");
        }
        else
            $(this).css("border", "#abc 1px solid");
    });
}
function Tip() {
    var xOffset = -10; // x distance from mouse
    var yOffset = 10; // y distance from mouse  
    $("[tip]").hover(
		function (e) {
		    if ($(this).attr('tip') != undefined) {
		        var top = (e.pageY + yOffset);
		        var left = (e.pageX + xOffset);
		        $('body').append('<p id="vtip"><img id="vtipArrow" src="/scripts/coustom/css/vtip_arrow.png"/>' + $(this).attr('tip') + '</p>');
		        $('p#vtip').css("top", top + "px").css("left", left + "px").css("font-size", "12px");
		        $('p#vtip').bgiframe();
		    }
		},
		function () {

		    if ($(this).attr('tip') != undefined) {
		        $("p#vtip").remove();
		    }
		    if ($(this).attr('tip') == "") {
		        $("p#vtip").remove();
		    }
		}
	).mousemove(
		function (e) {

		    if ($(this).attr('tip') != undefined) {
		        var top = (e.pageY + yOffset);
		        var left = (e.pageX + xOffset);
		        $("p#vtip").css("top", top + "px").css("left", left + "px").css("font-size", "12px");
		    }
		    if ($(this).attr('tip') == "") {
		        $("p#vtip").remove();
		    }
		}
	);
}
//添加一行和保存全部快捷键
jQuery(document).keypress(function (e) {
    if (e.ctrlKey && e.which == 13 || e.which == 10) {
        AddRows();
    } else if (e.shiftKey && e.which == 13 || e.which == 10) {
        SaveAll();
    }
    //    else if (e.shiftKey && e.which == 76) {
    //        SignOn('离开锁屏');
    //        return false;
    //    } else if (e.shiftKey && e.which == 79) window.location = "/signout.aspx";
})
//给文本框添加回车事件
//rows:行号 action:0编辑,1添加
function SetKeyCode(rows, action) {
    var btnID = "";
    if (1 == action) btnID = "btn_add_" + rows;
    if (0 == action) btnID = "btn_edit_" + rows;
    document.onkeydown = function (event) {
        e = event ? event : (window.event ? window.event : null);
        if (e.keyCode == 13) {
            $("#" + btnID + "").click();
            return false;
        }
    }
}
//给查询文本框添加回车事件
function SetKeySearch() {
    document.onkeydown = function (event) {
        e = event ? event : (window.event ? window.event : null);
        if (e.keyCode == 13) {
            $("#btnSearch").click();
            return false;
        }
    }
}
//给文分页文本框添加回车事件
function SetKeyGoToPage() {
    document.onkeydown = function (event) {
        e = event ? event : (window.event ? window.event : null);
        if (e.keyCode == 13) {
            document.getElementById("btnGoTo").click();
            return false;
        }
    }
}
//输入框分页跳转
function GoToPage(max,a) {
    var page = $.trim($(a).parent().find("#txtToPage").val());
    if ("" != page) {
        if (parseInt(page) > max) page = max;
        JavascriptPagination(page);
    }
}
//显示加载样式
function Loading() {
    var loader = jQuery('<div id="loader"><img src="/images/loading/1.gif"/></div>').css({ position: "absolute", top: "50%", left: "40%", display: "none" }).appendTo("body").hide(); jQuery("#loader").ajaxStart(function () { loader.show(); }).ajaxStop(function () { loader.remove(); }).ajaxError(function (a, b, e) { e; });
}
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
//无操作权限
function NoAccess() {
    $(".noaccess").live("click", function () {
        
        top.$.alert('权限不足', '操作提示');
    });
}
//ajax请求退出登录
function SignOut() {
    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/handler/login.aspx?action=signout",
        data: "",
        success: function (data) {
        }
    });
}
//登录超时本页重新登录
function SignOn(message) {
    if (null == message || "" == message) message = "超时登录";
    else SignOut();
    top.$.open({
        id: 'signon',
        url: '/signon.html',
        title: message + '-南菱二手车管理系统-易联信息技术',
        modal: true,
        drag: false,
        btnsbar: $.btn.OK, //按钮栏配置请参考 “辅助函数” 中的 $.btn。
        callback: function (action, iframe) {
            var t = this;
            if (action == 'ok') {
                var strPassword = $.trim($(iframe.document).find("#txtPassword").val());
                if ("" == strPassword) {
                    $(iframe.document).find("#txtPassword").focus();
                    $(iframe.document).find("#txtPassword").css("border", "red 1px solid");
                    return false;
                }
                var strUrl = "/handler/login.aspx?action=signon&username=" + escape($.cookie('manager')) + "&password=" + escape(strPassword) + "&validatecode=lock";
                $.ajax({
                    type: "POST",
                    dataType: "text",
                    url: strUrl,
                    data: "",
                    success: function (data) {
                        var arry = new Array();
                        arry = data.split(',');
                        if (-1 == arry[0]) {
                            var times = $(window.frames["mainFrame"].document).find("#spTimes").html();
                            $(window.frames["mainFrame"].document).find("#spTimes").html((parseInt(times) + 1));
                            top.$.close(t.id);
                        }
                        else {
                            $(iframe.document).find("#tdError").html("" + arry[1].replace("用户名或", "") + "");
                            return false;
                        }
                    }
                });
                return false;
            }
        }
    });
    var isie = ! -[1, ];
    var isie6 = isie && !window.xmlhttprequest;
    if (isie && isie6) document.frames("signon_content").location.reload(true);
}
//业务提醒统计
function warningShow(id, type) {
    //var year = parseInt($("#" + id, window.parent.menuFrame.document).find("em").html()) - 1;
    var obj = $("#" + id, window.top.document);
    var num = 0;
    if (type == "+") {
        num = parseInt(obj.find("em").html()) + 1;
    } else {
        num = parseInt(obj.find("em").html()) - 1;
    }
    if (num == 0) {
        obj.hide();
    } else {
        obj.show();
        obj.find("em").html(num);
    }
}
//业务提醒未办事项统计更改
function warningChange() {
    $.getJSON("/handler/year_expire.aspx", { action: "statsList", t: Math.random() }, function (json) {
        if (json == null) {
            $("#ul_warning .warning", window.top.document).hide();
        } else {
            $("#ul_warning .warning", window.top.document).each(function () {
                var key = $(this).attr("id").replace("_warning", "");
                if (json[0][key] > 0) {
                    $(this).find("em").html(json[0][key]);
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
        }
    });
}
//过期业务数量统计
function warningChange1() {
    $.getJSON("/handler/year_expire.aspx", { action: "statsList", t: Math.random() }, function (json) {
        if (json == null) {
            $("#ul_warning .warning", window.top.document).hide();
        } else {
            $("#ul_warning .warning", window.top.document).each(function () {
                var key = $(this).attr("id").replace("_warning", "");
                if (json[0][key] > 0) {
                    $(this).find("em").html(json[0][key]);
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
        }
    });
    $.getJSON("/handler/overdue_season_expire.aspx", { action: "lostCount", t: Math.random() }, function (json) {
        if (json == null) {
            $("#mainFrame", window.top.document).contents().find("#showWarning .warning").hide();
            //$("#showWarning .warning", window.parent.document).hide();
        } else {
            $("#mainFrame", window.top.document).contents().find("#showWarning .warning").each(function () {
                var key = $(this).attr("id").replace("_warning", "");
                if (json[0][key] > 0) {
                    $(this).find("em").html(json[0][key]);
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
        }
    });
}
//启用/禁用保存按钮
//enabled: ture禁用 false：启用
function DisabledSaveBtn(enabled) {
    if (enabled) {
        $('#open_ok', window.top.document).attr("disabled", "disabled");
        $('#open_ok', window.top.document).find("span").attr("disabled", "disabled");
        $('#asyncbox_prompt_ok', window.top.document).attr("disabled", "disabled");
        $('#asyncbox_prompt_ok', window.top.document).find("span").attr("disabled", "disabled");
    }
    else {
        $('#open_ok', window.top.document).removeAttr("disabled", "disabled");
        $('#open_ok', window.top.document).find("span").removeAttr("disabled", "disabled");
        $('#asyncbox_prompt_ok', window.top.document).removeAttr("disabled", "disabled");
        $('#asyncbox_prompt_ok', window.top.document).find("span").removeAttr("disabled", "disabled");
    }
}

//列表排序
function changeOrder(obj, order) {
    var $data = $("body").data("order");
    if ($data == null) {
        $("body").data("order", {
            order: order,
            orderStyle: "asc"
        });
    } else {
        if ($("body").data("order").order == order) {
            if ($data.orderStyle == "desc") {
                $("body").data("order").orderStyle = "asc";
            } else {
                $("body").data("order").orderStyle = "desc";
            }
        }
        else {
            $("body").data("order").order = order;
        }
    }
    JavascriptPagination(1);
}
