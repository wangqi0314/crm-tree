define("tpl/uploader.html.js", [], function(e, t, n) {
return '<li id="uploadItem{id}" data-status="{className}" class="upload_file">\n    <strong class="upload_file_name">{fileName}</strong>\n    <span class="upload_file_size">({size})</span>\n    {if showError}\n    	{if error}\n    <em class="upload_file_status {className}">{status}</em>\n    	{else}\n    <div class="progress_bar"><div class="progress_bar_thumb" style="width:0%"></div></div>\n    <a href="javascript:;" class="upload_file_cancel js_cancel">取消</a>\n    	{/if}\n    {else}\n    <div class="progress_bar"><div class="progress_bar_thumb" style="width:0%"></div></div>\n    {/if}\n</li>\n';
});define("biz_web/lib/uploadify.js", [ "biz_web/lib/swfobject.js" ], function(e, t, n) {
try {
var r = +(new Date), i = e("biz_web/lib/swfobject.js");
$.extend($.fn, {
uploadify: function(e) {
$(this).each(function() {
var t = $(this), n = t.attr("id"), r = location.pathname, s = $.extend({
id: n,
uploader: "uploadify.swf",
script: "uploadify.php",
expressInstall: null,
folder: "",
height: null,
width: null,
cancelImg: "cancel.png",
wmode: "opaque",
scriptAccess: "always",
fileDataName: "Filedata",
method: "POST",
queueSizeLimit: 5,
simUploadLimit: 1,
queueID: !1,
displayData: "percentage",
buttonImg: null,
buttonText: "",
rollover: !1,
hideButton: !0,
onInit: function() {},
onSelect: function() {},
onQueueFull: function() {},
onCheck: function() {},
onCancel: function() {},
onError: function() {},
onProgress: function() {},
onComplete: function() {},
onAllComplete: function() {}
}, e);
r = r.split("/"), r.pop(), r = r.join("/") + "/";
var o = {};
o.uploadifyID = s.id, o.pagepath = r, s.buttonImg && (o.buttonImg = escape(s.buttonImg)), s.buttonText && (o.buttonText = escape(s.buttonText)), s.rollover && (o.rollover = !0), o.script = encodeURIComponent(s.script), o.folder = escape(s.folder);
if (s.scriptData) {
var u = "";
for (var a in s.scriptData) u += "&" + a + "=" + s.scriptData[a];
o.scriptData = escape(u.substr(1));
}
o.width = s.width || t.outerWidth(), o.height = s.height || t.outerHeight(), o.wmode = s.wmode, o.method = s.method, o.queueSizeLimit = s.queueSizeLimit, o.simUploadLimit = s.simUploadLimit, s.hideButton && (o.hideButton = !0), s.fileDesc && (o.fileDesc = s.fileDesc), s.fileExt && (o.fileExt = s.fileExt), s.multi && (o.multi = !0), s.auto && (o.auto = !0), s.sizeLimit && (o.sizeLimit = s.sizeLimit), s.checkScript && (o.checkScript = s.checkScript), s.fileDataName && (o.fileDataName = s.fileDataName), s.queueID && (o.queueID = s.queueID);
if (s.onInit() !== !1) {
var f = t.offset();
t.parent().append($('<div id="' + n + 'Uploader"></div>')).parent().addClass("upload_box"), i.switchOffAutoHideShow(), i.registerObject("flashAntelope", "9.0.0"), i.embedSWF(s.uploader, s.id + "Uploader", "100%", "100%", "9.0.24", s.expressInstall, o, {
quality: "high",
wmode: s.wmode,
allowScriptAccess: s.scriptAccess
}, undefined, function() {
$("#" + s.id + "Uploader").css("zoom", 1).css("zoom", 0);
}), s.queueID == 0 && $("#" + n + "Uploader").after('<div id="' + n + 'Queue" class="uploadifyQueue"></div>');
}
typeof s.onOpen == "function" && t.bind("uploadifyOpen", s.onOpen), t.bind("uploadifySelect", {
action: s.onSelect,
queueID: s.queueID
}, function(e, n, r) {
e.data.action(e, n, r) === !1 && document.getElementById(t.attr("id") + "Uploader").cancelFileUpload(n, !0, !1);
}), typeof s.onSelectOnce == "function" && t.bind("uploadifySelectOnce", s.onSelectOnce), t.bind("uploadifyQueueFull", {
action: s.onQueueFull
}, function(e, t) {
e.data.action(e, t) !== !1 && alert("The queue is full.  The max size is " + t + ".");
}), t.bind("uploadifyCancel", {
action: s.onCancel
}, function(e, t, r, i, s) {
if (e.data.action(e, t, r, i, s) !== !1) {
var o = s == 1 ? 0 : 250;
$("#" + n + t).fadeOut(o, function() {
$(this).remove();
});
}
}), typeof s.onClearQueue == "function" && t.bind("uploadifyClearQueue", s.onClearQueue);
var l = [];
t.bind("uploadifyError", {
action: s.onError
}, function(e, t, r, i) {
if (e.data.action(e, t, r, i) !== !1) {
var s = new Array(t, r, i);
l.push(s), $("#" + n + t + " .percentage").text(" - " + i.type + " Error"), $("#" + n + t).addClass("uploadifyError");
}
}), t.bind("uploadifyProgress", {
action: s.onProgress,
toDisplay: s.displayData
}, function(e, t, r, i) {
e.data.action(e, t, r, i) !== !1 && ($("#" + n + t + "ProgressBar").css("width", i.percentage + "%"), e.data.toDisplay == "percentage" && (displayData = " - " + i.percentage + "%"), e.data.toDisplay == "speed" && (displayData = " - " + i.speed + "KB/s"), e.data.toDisplay == null && (displayData = " "), $("#" + n + t + " .percentage").text(displayData));
}), t.bind("uploadifyComplete", {
action: s.onComplete
}, function(e, t, n, r, i) {
e.data.action(e, t, n, unescape(r), i) !== !1;
}), typeof s.onAllComplete == "function" && t.bind("uploadifyAllComplete", {
action: s.onAllComplete
}, function(e, t) {
e.data.action(e, t) !== !1 && (l = []);
});
});
},
uploadifySettings: function(e, t, n) {
var r = !1;
this$.each(function() {
if (e == "scriptData" && t != null) {
if (n) var i = t; else var i = $.extend(settings.scriptData, t);
var s = "";
for (var o in i) s += "&" + o + "=" + escape(i[o]);
t = s.substr(1);
}
r = document.getElementById(id + "Uploader").updateSettings(e, t);
});
if (t == null) {
if (e == "scriptData") {
var i = unescape(r).split("&"), s = new Object;
for (var o = 0; o < i.length; o++) {
var u = i[o].split("=");
s[u[0]] = u[1];
}
r = s;
}
return r;
}
},
uploadifyUpload: function(e) {
$(this).each(function() {
document.getElementById($(this).attr("id") + "Uploader").startFileUpload(e, !1);
});
},
uploadifyCancel: function(e) {
$(this).each(function() {
document.getElementById($(this).attr("id") + "Uploader").cancelFileUpload(e, !0, !1);
});
},
uploadifyClearQueue: function() {
$(this).each(function() {
document.getElementById($(this).attr("id") + "Uploader").clearFileUploadQueue(!1);
});
}
});
} catch (s) {
wx.jslog({
src: "biz_web/lib/uploadify.js"
}, s);
}
});define("tpl/richEditor/emotion.html.js", [], function(e, t, n) {
return '<ul class="emotions" onselectstart="return false;"> \n    {each edata as e index}\n        <li class="emotions_item">\n            <i\n                class="js_emotion_i" \n                data-gifurl=\'{e.gifurl}\' \n                data-title=\'{e.title}\' \n                style=\'{e.bp}\'>\n            </i>\n        </li>\n    {/each}\n</ul>\n<span class="emotions_preview js_emotionPreviewArea"></span>\n';
});define("common/wx/richEditor/editorRange.js", [], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = function() {
return document.selection ? document.selection : (window.getSelection || document.getSelection)();
}, s = function(e, t, n) {
if (!n && e === t) return !1;
if (e.compareDocumentPosition) {
var r = e.compareDocumentPosition(t);
if (r == 20 || r == 0) return !0;
} else if (e.contains(t)) return !0;
return !1;
}, o = function(e, t) {
var n = t.commonAncestorContainer || t.parentElement && t.parentElement() || null;
return n ? s(e, n, !0) : !1;
}, u = function(e) {
var t = i();
if (!t) return null;
var n = t.getRangeAt ? t.rangeCount ? t.getRangeAt(0) : null : t.createRange();
return n ? e ? o(e, n) ? n : null : n : null;
}, a = function(e) {
return e.parentElement ? e.parentElement() : e.commonAncestorContainer;
};
n.exports = {
getSelection: i,
getRange: u,
containsRange: o,
parentContainer: a
};
} catch (f) {
wx.jslog({
src: "common/wx/richEditor/editorRange.js"
}, f);
}
});define("tpl/media/simple_appmsg.html.js", [], function(e, t, n) {
return '<div class="appmsgImgArea">\n    <img src="{appmsg_cover}" />\n</div>\n<div class="appmsgContentArea">\n    <div class="appmsgTitle">\n        {if content_url}\n        <a href="{content_url}" target="_blank">[{type_msg}]{title}</a>\n        {else}\n        <span>[{type_msg}]{title}</span>\n        {/if}\n    </div>\n    {if content_url}\n    <a href="{content_url}" target="_blank" class="appmsgDesc">{desc}</a>\n    {else}\n    <span class="appmsgDesc">{desc}</span>\n    {/if}\n</div>\n<div class="cl"></div>\n{if (show_type == 0 && !!app_name) }\n<div class="resource appmsgFrom">来自{app_name}</div>\n{/if}\n';
});define("biz_web/ui/dropdown.js", [ "biz_web/widget/dropdown.css", "tpl/biz_web/ui/dropdown.html.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict", e("biz_web/widget/dropdown.css");
var i = e("tpl/biz_web/ui/dropdown.html.js"), s = {
label: "请选择",
data: [],
callback: $.noop,
render: $.noop,
delay: 500,
disabled: !1,
search: !1
}, o = "dropdown_menu";
function u(e) {
e.render && typeof e.render && (e.renderHtml = "", $.each(e.data, function(t, n) {
e.renderHtml += e.render(n);
})), e = $.extend(!0, {}, s, e);
var t = this;
t.container = $(e.container), t.container.addClass(e.search ? o + " search" : o), this.isDisabled = e.disabled, e.disabled && t.container.addClass("disabled"), t.opt = e, t.container.html(template.compile(i)(e)).find(".jsDropdownList").hide(), t.bt = t.container.find(".jsDropdownBt"), t.dropdown = t.container.find(".jsDropdownList"), $.each(e.data, function(e, n) {
$.data(t.dropdown.find(".jsDropdownItem")[e], "value", n.value), $.data(t.dropdown.find(".jsDropdownItem")[e], "name", n.name), $.data(t.dropdown.find(".jsDropdownItem")[e], "item", n);
}), typeof e.index != "undefined" && e.data.length !== 0 && (t.bt.find(".jsBtLabel").html(e.data[e.index].name || e.label), t.value = e.data[e.index].value), t.bt.on("click", function() {
return a(), e.disabled || (t.dropdown.show(), t.container.addClass("open")), !1;
}), e.search && t.bt.find(".jsBtLabel").on("keyup", function(e) {
if (t.disabled) return;
var n = $(this);
if (e.keyCode == 13) t.value ? (n.html(n.data("name")).removeClass("error"), t.dropdown.hide()) : n.find("div").remove(); else {
var r = n.html().trim(), i = [];
t.value = null, t.dropdown.show().find(".jsDropdownItem").each(function() {
var e = $(this);
e.hasClass("js_empty") || (e.data("name").indexOf(r) > -1 ? (e.parent().show(), i.push({
name: e.data("name"),
value: e.data("value")
})) : e.parent().hide());
}), i.length == 0 ? t.dropdown.find(".js_empty").length == 0 && t.dropdown.append('<li class="jsDropdownItem js_empty empty">未找到"' + r + '"</li>') : (t.dropdown.find(".js_empty").remove(), i.length == 1 && (i[0].name == r ? n.removeClass("error") : n.data("name", i[0].name), t.value = i[0].value));
}
}).on("blur", function() {
if (t.disabled) return;
var n = $(this);
t.value ? $(this).html() != $(this).data("name") && (n.addClass("error"), t.value = null) : n.html() != "" ? n.addClass("error") : (n.html(e.label).removeClass("error"), t.value = null);
}).on("focus", function() {
if (t.disabled) return;
var n = $(this), r = $(this).html().trim();
r == e.label && n.html("").removeClass("error"), r == "" && n.removeClass("error"), t.dropdown.show(), t.container.addClass("open");
}), $(document).on("click", a), t.dropdown.on("click", ".jsDropdownItem", function(n) {
var r = $(this).data("value"), i = $(this).data("name"), s = $(this).data("index");
if (!t.value || t.value && t.value != r) {
t.value = r, t.name = i;
if (e.callback && typeof e.callback == "function") {
var o = e.callback(r, i, s, $(this).data("item")) || i;
e.search ? t.bt.find(".jsBtLabel").html(o).data("name", o).removeClass("error") : t.bt.find(".jsBtLabel").html(o);
}
}
t.dropdown.hide();
});
}
function a() {
$(".jsDropdownList").hide(), $(".dropdown_menu").each(function() {
!$(this).hasClass("dropdown_checkbox") && $(this).removeClass("open");
});
}
return u.prototype = {
selected: function(e, t) {
var n = this;
if (typeof e == "number") {
if (this.opt.data && this.opt.data[e]) {
var r = this.opt.data[e].name, i = this.opt.data[e].value;
t != 0 && this.dropdown.find(".jsDropdownItem:eq(" + e + ")").trigger("click", i), this.bt.find(".jsBtLabel").html(r);
}
} else $.each(this.opt.data, function(r, s) {
if (e == s.value || e == s.name) return t != 0 && n.dropdown.find(".jsDropdownItem:eq(" + r + ")").trigger("click", i), n.bt.find(".jsBtLabel").html(s.name), !1;
});
return this;
},
reset: function() {
return this.bt.find(".jsBtLabel").html(this.opt.label), this.value = null, this;
},
hidegreater: function(e) {
var t = this;
return typeof e == "number" && t.opt.data && t.opt.data[e] && (t.dropdown.find(".jsDropdownItem").show(), t.dropdown.find(".jsDropdownItem:gt(" + e + ")").hide()), this;
},
destroy: function() {
return this.isDisabled && this.container.removeClass("disabled"), this.container.children().remove(), this.container.off(), this;
},
enable: function() {
return this.opt.disabled = !1, this.container.removeClass("disabled"), this.opt.search && this.bt.find(".jsBtLabel").attr("contenteditable", !0), this;
},
disable: function() {
return this.opt.disabled = !0, this.container.addClass("disabled"), this.opt.search && this.bt.find(".jsBtLabel").attr("contenteditable", !1), this;
}
}, u;
} catch (f) {
wx.jslog({
src: "biz_web/ui/dropdown.js"
}, f);
}
});define("user/user_cgi.js", [ "common/wx/Tips.js", "common/wx/Cgi.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = {
changeRemark: "/cgi-bin/modifycontacts?t=ajax-response&action=setremark",
changeGroup: "/cgi-bin/modifycontacts?action=modifycontacts&t=ajax-putinto-group",
getBuddy: "/cgi-bin/getcontactinfo?t=ajax-getcontactinfo&lang=zh_CN&fakeid="
}, s = e("common/wx/Tips.js"), o = e("common/wx/Cgi.js");
n.exports = {
changeRemark: function(e, t, n) {
o.post({
mask: !1,
url: i.changeRemark,
data: {
remark: t,
tofakeuin: e
}
}, function(e) {
if (e.ret == "0") s.suc("修改成功"), typeof n == "function" && n(e); else switch (e.ret) {
case "61900":
s.err("修改失败");
break;
case "61901":
s.err("系统繁忙");
break;
case "61910":
s.err("修改失败");
break;
case "61911":
s.err("修改失败，对方不是你的粉丝");
break;
case "61912":
s.err("修改失败，备注不能超过30个字");
break;
default:
s.err("修改失败");
}
});
},
changeGroup: function(e, t, n) {
var r = $.isArray(e) ? e.join("|") : e;
o.post({
url: i.changeGroup,
data: {
contacttype: t,
tofakeidlist: r
},
mask: !1
}, function(e) {
e && e.ret == "0" ? (typeof n == "function" && n(e), s.suc("添加成功")) : s.err("添加失败");
});
},
getBuddyInfo: function(e, t) {
o.post({
mask: !1,
url: i.getBuddy + e
}, function(e) {
typeof t == "function" && t(e);
});
}
};
} catch (u) {
wx.jslog({
src: "user/user_cgi.js"
}, u);
}
});define("tpl/RichBuddy/RichBuddyContent.html.js", [], function(e, t, n) {
return '<div class="frm_control_group nickName">\n    <label class="frm_label" title="{nick_name}">{nick_name}</label>\n    <div class="frm_controls frm_vertical_pt">\n        <span class="icon18_common {if gender==1}man_blue{else if gender==2}woman_orange{/if}"></span>\n	</div>\n</div>\n<div class="frm_control_group nickName">\n    <label class="frm_label">备注名</label>\n    <div class="frm_controls frm_vertical_pt">\n        <span class=\'js_remarkName remark_name\'>{remark_name}</span>\n		<a title="修改备注" class="icon14_common edit_gray js_changeRemark" href="javascript:;">修改备注</a> \n	</div>\n</div>\n<div class="frm_control_group nickName">\n    <label class="frm_label">地区</label>\n    <div class="frm_controls frm_vertical_pt">\n        {country} {province} {city}\n	</div>\n</div>\n<div class="frm_control_group nickName">\n    <label class="frm_label">签名</label>\n    <div class="frm_controls frm_vertical_pt">\n        {signature}\n	</div>\n</div>\n<div class="frm_control_group nickName js_group_container">\n<label class="frm_label">分组</label>\n    <div class="frm_controls frm_vertical_pt">\n        <div class="dropdown_wrp js_group"></div>\n    </div>\n</div>\n';
});define("tpl/RichBuddy/RichBuddyLayout.html.js", [], function(e, t, n) {
return '<div class="rich_buddy popover arrow_left" style="display:none;">\n    <div class="popover_inner">\n        <div class="popover_content">\n            <div class="rich_buddy_hd">详细资料</div>\n\n            <div class="loadingArea rich_buddy_loading"><span class="vm_box"></span><i class="icon_loading_small dark"></i></div>\n            <div class="rich_buddy_bd buddyRichContent">\n            </div>\n        </div>\n    </div>\n    <i class="popover_arrow popover_arrow_out"></i>\n    <i class="popover_arrow popover_arrow_in"></i>\n</div>\n';
});define("tpl/media/id_card.html.js", [], function(e, t, n) {
return '<div class="mediaBox bcardBox">\n    <div class="mediaContent">\n        <div class="bCard">\n            <div class="bCardHeader">名片</div>\n            <div class="bCardContent">\n                <img class="bCardAvatar" src="{avatar}" alt="{username}" title="{username}"/>\n                <div class="info">\n                    <p class="nickname">{nickname}</p>\n                    <p class="username">{username}</p>\n                </div>\n            </div>\n        </div>\n    </div>\n    <span class="iconArrow"></span>\n</div>\n';
});define("biz_common/jquery.validate.js", [], function(e, t, n) {
try {
var r = +(new Date);
(function(e) {
e.extend(e.fn, {
validate: function(t) {
if (!this.length) {
t && t.debug && window.console && console.warn("Nothing selected, can't validate, returning nothing.");
return;
}
var n = e.data(this[0], "validator");
return n ? n : (this.attr("novalidate", "novalidate"), n = new e.validator(t, this[0]), e.data(this[0], "validator", n), n.settings.onsubmit && (this.validateDelegate(":submit", "click", function(t) {
n.settings.submitHandler && (n.submitButton = t.target), e(t.target).hasClass("cancel") && (n.cancelSubmit = !0), e(t.target).attr("formnovalidate") !== undefined && (n.cancelSubmit = !0);
}), this.submit(function(t) {
function r() {
var r;
return n.settings.submitHandler ? (n.submitButton && (r = e("<input type='hidden'/>").attr("name", n.submitButton.name).val(e(n.submitButton).val()).appendTo(n.currentForm)), n.settings.submitHandler.call(n, n.currentForm, t), n.submitButton && r.remove(), !1) : !0;
}
return n.settings.debug && t.preventDefault(), n.cancelSubmit ? (n.cancelSubmit = !1, r()) : n.form() ? n.pendingRequest ? (n.formSubmitted = !0, !1) : r() : (n.focusInvalid(), !1);
})), n);
},
valid: function() {
if (e(this[0]).is("form")) return this.validate().form();
var t = !0, n = e(this[0].form).validate();
return this.each(function() {
t = t && n.element(this);
}), t;
},
removeAttrs: function(t) {
var n = {}, r = this;
return e.each(t.split(/\s/), function(e, t) {
n[t] = r.attr(t), r.removeAttr(t);
}), n;
},
rules: function(t, n) {
var r = this[0];
if (t) {
var i = e.data(r.form, "validator").settings, s = i.rules, o = e.validator.staticRules(r);
switch (t) {
case "add":
e.extend(o, e.validator.normalizeRule(n)), delete o.messages, s[r.name] = o, n.messages && (i.messages[r.name] = e.extend(i.messages[r.name], n.messages));
break;
case "remove":
if (!n) return delete s[r.name], o;
var u = {};
return e.each(n.split(/\s/), function(e, t) {
u[t] = o[t], delete o[t];
}), u;
}
}
var a = e.validator.normalizeRules(e.extend({}, e.validator.classRules(r), e.validator.attributeRules(r), e.validator.dataRules(r), e.validator.staticRules(r)), r);
if (a.required) {
var f = a.required;
delete a.required, a = e.extend({
required: f
}, a);
}
return a;
}
}), e.extend(e.expr[":"], {
blank: function(t) {
return !e.trim("" + e(t).val());
},
filled: function(t) {
return !!e.trim("" + e(t).val());
},
unchecked: function(t) {
return !e(t).prop("checked");
}
}), e.validator = function(t, n) {
this.settings = e.extend(!0, {}, e.validator.defaults, t), this.currentForm = n, this.init();
}, e.validator.format = function(t, n) {
return arguments.length === 1 ? function() {
var n = e.makeArray(arguments);
return n.unshift(t), e.validator.format.apply(this, n);
} : (arguments.length > 2 && n.constructor !== Array && (n = e.makeArray(arguments).slice(1)), n.constructor !== Array && (n = [ n ]), e.each(n, function(e, n) {
t = t.replace(new RegExp("\\{" + e + "\\}", "g"), function() {
return n;
});
}), t);
}, e.extend(e.validator, {
defaults: {
messages: {},
groups: {},
rules: {},
errorClass: "error",
validClass: "valid",
errorElement: "label",
focusInvalid: !0,
errorContainer: e([]),
errorLabelContainer: e([]),
onsubmit: !0,
ignore: ":hidden",
ignoreTitle: !1,
onfocusin: function(e, t) {
this.lastActive = e, this.settings.focusCleanup && !this.blockFocusCleanup && (this.settings.unhighlight && this.settings.unhighlight.call(this, e, this.settings.errorClass, this.settings.validClass), this.addWrapper(this.errorsFor(e)).hide());
},
onfocusout: function(e, t) {
this.checkable(e) || this.element(e);
},
onkeyup: function(e, t) {
if (t.which === 9 && this.elementValue(e) === "") return;
(e.name in this.submitted || e === this.lastElement) && this.element(e);
},
onclick: function(e, t) {
e.name in this.submitted ? this.element(e) : e.parentNode.name in this.submitted && this.element(e.parentNode);
},
highlight: function(t, n, r) {
t.type === "radio" ? this.findByName(t.name).addClass(n).removeClass(r) : e(t).addClass(n).removeClass(r);
},
unhighlight: function(t, n, r) {
t.type === "radio" ? this.findByName(t.name).removeClass(n).addClass(r) : e(t).removeClass(n).addClass(r);
}
},
setDefaults: function(t) {
e.extend(e.validator.defaults, t);
},
messages: {
required: "This field is required.",
remote: "Please fix this field.",
email: "Please enter a valid email address.",
url: "Please enter a valid URL.",
date: "Please enter a valid date.",
dateISO: "Please enter a valid date (ISO).",
number: "Please enter a valid number.",
digits: "Please enter only digits.",
creditcard: "Please enter a valid credit card number.",
equalTo: "Please enter the same value again.",
maxlength: e.validator.format("Please enter no more than {0} characters."),
minlength: e.validator.format("Please enter at least {0} characters."),
rangelength: e.validator.format("Please enter a value between {0} and {1} characters long."),
range: e.validator.format("Please enter a value between {0} and {1}."),
max: e.validator.format("Please enter a value less than or equal to {0}."),
min: e.validator.format("Please enter a value greater than or equal to {0}.")
},
autoCreateRanges: !1,
prototype: {
init: function() {
function t(t) {
var n = e.data(this[0].form, "validator"), r = "on" + t.type.replace(/^validate/, "");
n.settings[r] && n.settings[r].call(n, this[0], t);
}
this.labelContainer = e(this.settings.errorLabelContainer), this.errorContext = this.labelContainer.length && this.labelContainer || e(this.currentForm), this.containers = e(this.settings.errorContainer).add(this.settings.errorLabelContainer), this.submitted = {}, this.valueCache = {}, this.pendingRequest = 0, this.pending = {}, this.invalid = {}, this.reset();
var n = this.groups = {};
e.each(this.settings.groups, function(t, r) {
typeof r == "string" && (r = r.split(/\s/)), e.each(r, function(e, r) {
n[r] = t;
});
});
var r = this.settings.rules;
e.each(r, function(t, n) {
r[t] = e.validator.normalizeRule(n);
}), e(this.currentForm).validateDelegate(":text, [type='password'], [type='file'], select, textarea, [type='number'], [type='search'] ,[type='tel'], [type='url'], [type='email'], [type='datetime'], [type='date'], [type='month'], [type='week'], [type='time'], [type='datetime-local'], [type='range'], [type='color'] ", "focusin focusout keyup", t).validateDelegate("[type='radio'], [type='checkbox'], select, option", "click", t), this.settings.invalidHandler && e(this.currentForm).bind("invalid-form.validate", this.settings.invalidHandler);
},
form: function() {
return this.checkForm(), e.extend(this.submitted, this.errorMap), this.invalid = e.extend({}, this.errorMap), this.valid() || e(this.currentForm).triggerHandler("invalid-form", [ this ]), this.showErrors(), this.valid();
},
checkForm: function() {
this.prepareForm();
for (var e = 0, t = this.currentElements = this.elements(); t[e]; e++) this.check(t[e]);
return this.valid();
},
element: function(t) {
t = this.validationTargetFor(this.clean(t)), this.lastElement = t, this.prepareElement(t), this.currentElements = e(t);
var n = this.check(t) !== !1;
return n ? delete this.invalid[t.name] : this.invalid[t.name] = !0, this.numberOfInvalids() || (this.toHide = this.toHide.add(this.containers)), this.showErrors(), n;
},
showErrors: function(t) {
if (t) {
e.extend(this.errorMap, t), this.errorList = [];
for (var n in t) this.errorList.push({
message: t[n],
element: this.findByName(n)[0]
});
this.successList = e.grep(this.successList, function(e) {
return !(e.name in t);
});
}
this.settings.showErrors ? this.settings.showErrors.call(this, this.errorMap, this.errorList) : this.defaultShowErrors();
},
resetForm: function() {
e.fn.resetForm && e(this.currentForm).resetForm(), this.submitted = {}, this.lastElement = null, this.prepareForm(), this.hideErrors(), this.elements().removeClass(this.settings.errorClass).removeData("previousValue");
},
numberOfInvalids: function() {
return this.objectLength(this.invalid);
},
objectLength: function(e) {
var t = 0;
for (var n in e) t++;
return t;
},
hideErrors: function() {
this.addWrapper(this.toHide).hide();
},
valid: function() {
return this.size() === 0;
},
size: function() {
return this.errorList.length;
},
focusInvalid: function() {
if (this.settings.focusInvalid) try {
e(this.findLastActive() || this.errorList.length && this.errorList[0].element || []).filter(":visible").focus().trigger("focusin");
} catch (t) {}
},
findLastActive: function() {
var t = this.lastActive;
return t && e.grep(this.errorList, function(e) {
return e.element.name === t.name;
}).length === 1 && t;
},
elements: function() {
var t = this, n = {};
return e(this.currentForm).find("input, select, textarea").not(":submit, :reset, :image, [disabled]").not(this.settings.ignore).filter(function() {
return !this.name && t.settings.debug && window.console && console.error("%o has no name assigned", this), this.name in n || !t.objectLength(e(this).rules()) ? !1 : (n[this.name] = !0, !0);
});
},
clean: function(t) {
return e(t)[0];
},
errors: function() {
var t = this.settings.errorClass.replace(" ", ".");
return e(this.settings.errorElement + "." + t, this.errorContext);
},
reset: function() {
this.successList = [], this.errorList = [], this.errorMap = {}, this.toShow = e([]), this.toHide = e([]), this.currentElements = e([]);
},
prepareForm: function() {
this.reset(), this.toHide = this.errors().add(this.containers);
},
prepareElement: function(e) {
this.reset(), this.toHide = this.errorsFor(e);
},
elementValue: function(t) {
var n = e(t).attr("type"), r = e(t).val();
return n === "radio" || n === "checkbox" ? e("input[name='" + e(t).attr("name") + "']:checked").val() : typeof r == "string" ? r.replace(/\r/g, "") : r;
},
check: function(t) {
t = this.validationTargetFor(this.clean(t));
var n = e(t).rules(), r = !1, i = this.elementValue(t), s;
for (var o in n) {
var u = {
method: o,
parameters: n[o]
};
try {
s = e.validator.methods[o].call(this, i, t, u.parameters);
if (s === "dependency-mismatch") {
r = !0;
continue;
}
r = !1;
if (s === "pending") {
this.toHide = this.toHide.not(this.errorsFor(t));
return;
}
if (!s) return this.formatAndAdd(t, u), !1;
} catch (a) {
throw this.settings.debug && window.console && console.log("Exception occurred when checking element " + t.id + ", check the '" + u.method + "' method.", a), a;
}
}
if (r) return;
return this.objectLength(n) && this.successList.push(t), !0;
},
customDataMessage: function(t, n) {
return e(t).data("msg-" + n.toLowerCase()) || t.attributes && e(t).attr("data-msg-" + n.toLowerCase());
},
customMessage: function(e, t) {
var n = this.settings.messages[e];
return n && (n.constructor === String ? n : n[t]);
},
findDefined: function() {
for (var e = 0; e < arguments.length; e++) if (arguments[e] !== undefined) return arguments[e];
return undefined;
},
defaultMessage: function(t, n) {
return this.findDefined(this.customMessage(t.name, n), this.customDataMessage(t, n), !this.settings.ignoreTitle && t.title || undefined, e.validator.messages[n], "<strong>Warning: No message defined for " + t.name + "</strong>");
},
formatAndAdd: function(t, n) {
var r = this.defaultMessage(t, n.method), i = /\$?\{(\d+)\}/g;
typeof r == "function" ? r = r.call(this, n.parameters, t) : i.test(r) && (r = e.validator.format(r.replace(i, "{$1}"), n.parameters)), this.errorList.push({
message: r,
element: t
}), this.errorMap[t.name] = r, this.submitted[t.name] = r;
},
addWrapper: function(e) {
return this.settings.wrapper && (e = e.add(e.parent(this.settings.wrapper))), e;
},
defaultShowErrors: function() {
var e, t;
for (e = 0; this.errorList[e]; e++) {
var n = this.errorList[e];
this.settings.highlight && this.settings.highlight.call(this, n.element, this.settings.errorClass, this.settings.validClass), this.showLabel(n.element, n.message);
}
this.errorList.length && (this.toShow = this.toShow.add(this.containers));
if (this.settings.success) for (e = 0; this.successList[e]; e++) this.showLabel(this.successList[e]);
if (this.settings.unhighlight) for (e = 0, t = this.validElements(); t[e]; e++) this.settings.unhighlight.call(this, t[e], this.settings.errorClass, this.settings.validClass);
this.toHide = this.toHide.not(this.toShow), this.hideErrors(), this.addWrapper(this.toShow).show();
},
validElements: function() {
return this.currentElements.not(this.invalidElements());
},
invalidElements: function() {
return e(this.errorList).map(function() {
return this.element;
});
},
showLabel: function(t, n) {
var r = this.errorsFor(t);
r.length ? (r.removeClass(this.settings.validClass).addClass(this.settings.errorClass), r.html(n)) : (r = e("<" + this.settings.errorElement + ">").attr("for", this.idOrName(t)).addClass(this.settings.errorClass).html(n || ""), this.settings.wrapper && (r = r.hide().show().wrap("<" + this.settings.wrapper + " class='frm_msg fail'/>").parent()), this.labelContainer.append(r).length || (this.settings.errorPlacement ? this.settings.errorPlacement(r, e(t)) : r.insertAfter(t))), !n && this.settings.success && (r.text(""), typeof this.settings.success == "string" ? r.addClass(this.settings.success) : this.settings.success(r, t)), this.toShow = this.toShow.add(r);
},
errorsFor: function(t) {
var n = this.idOrName(t);
return this.errors().filter(function() {
return e(this).attr("for") === n;
});
},
idOrName: function(e) {
return this.groups[e.name] || (this.checkable(e) ? e.name : e.id || e.name);
},
validationTargetFor: function(e) {
return this.checkable(e) && (e = this.findByName(e.name).not(this.settings.ignore)[0]), e;
},
checkable: function(e) {
return /radio|checkbox/i.test(e.type);
},
findByName: function(t) {
return e(this.currentForm).find("[name='" + t + "']");
},
getLength: function(t, n) {
switch (n.nodeName.toLowerCase()) {
case "select":
return e("option:selected", n).length;
case "input":
if (this.checkable(n)) return this.findByName(n.name).filter(":checked").length;
}
return t.length;
},
depend: function(e, t) {
return this.dependTypes[typeof e] ? this.dependTypes[typeof e](e, t) : !0;
},
dependTypes: {
"boolean": function(e, t) {
return e;
},
string: function(t, n) {
return !!e(t, n.form).length;
},
"function": function(e, t) {
return e(t);
}
},
optional: function(t) {
var n = this.elementValue(t);
return !e.validator.methods.required.call(this, n, t) && "dependency-mismatch";
},
startRequest: function(e) {
this.pending[e.name] || (this.pendingRequest++, this.pending[e.name] = !0);
},
stopRequest: function(t, n) {
this.pendingRequest--, this.pendingRequest < 0 && (this.pendingRequest = 0), delete this.pending[t.name], n && this.pendingRequest === 0 && this.formSubmitted && this.form() ? (e(this.currentForm).submit(), this.formSubmitted = !1) : !n && this.pendingRequest === 0 && this.formSubmitted && (e(this.currentForm).triggerHandler("invalid-form", [ this ]), this.formSubmitted = !1);
},
previousValue: function(t) {
return e.data(t, "previousValue") || e.data(t, "previousValue", {
old: null,
valid: !0,
message: this.defaultMessage(t, "remote")
});
}
},
classRuleSettings: {
required: {
required: !0
},
email: {
email: !0
},
url: {
url: !0
},
date: {
date: !0
},
dateISO: {
dateISO: !0
},
number: {
number: !0
},
digits: {
digits: !0
},
creditcard: {
creditcard: !0
}
},
addClassRules: function(t, n) {
t.constructor === String ? this.classRuleSettings[t] = n : e.extend(this.classRuleSettings, t);
},
classRules: function(t) {
var n = {}, r = e(t).attr("class");
return r && e.each(r.split(" "), function() {
this in e.validator.classRuleSettings && e.extend(n, e.validator.classRuleSettings[this]);
}), n;
},
attributeRules: function(t) {
var n = {}, r = e(t), i = r[0].getAttribute("type");
for (var s in e.validator.methods) {
var o;
s === "required" ? (o = r.get(0).getAttribute(s), o === "" && (o = !0), o = !!o) : o = r.attr(s), /min|max/.test(s) && (i === null || /number|range|text/.test(i)) && (o = Number(o)), o ? n[s] = o : i === s && i !== "range" && (n[s] = !0);
}
return n.maxlength && /-1|2147483647|524288/.test(n.maxlength) && delete n.maxlength, n;
},
dataRules: function(t) {
var n, r, i = {}, s = e(t);
for (n in e.validator.methods) r = s.data("rule-" + n.toLowerCase()), r !== undefined && (i[n] = r);
return i;
},
staticRules: function(t) {
var n = {}, r = e.data(t.form, "validator");
return r.settings.rules && (n = e.validator.normalizeRule(r.settings.rules[t.name]) || {}), n;
},
normalizeRules: function(t, n) {
return e.each(t, function(r, i) {
if (i === !1) {
delete t[r];
return;
}
if (i.param || i.depends) {
var s = !0;
switch (typeof i.depends) {
case "string":
s = !!e(i.depends, n.form).length;
break;
case "function":
s = i.depends.call(n, n);
}
s ? typeof i != "string" && (t[r] = i.param !== undefined ? i.param : !0) : delete t[r];
}
}), e.each(t, function(r, i) {
t[r] = e.isFunction(i) ? i(n) : i;
}), e.each([ "minlength", "maxlength" ], function() {
t[this] && (t[this] = Number(t[this]));
}), e.each([ "rangelength", "range" ], function() {
var n;
t[this] && (e.isArray(t[this]) ? t[this] = [ Number(t[this][0]), Number(t[this][1]) ] : typeof t[this] == "string" && (n = t[this].split(/[\s,]+/), t[this] = [ Number(n[0]), Number(n[1]) ]));
}), e.validator.autoCreateRanges && (t.min && t.max && (t.range = [ t.min, t.max ], delete t.min, delete t.max), t.minlength && t.maxlength && (t.rangelength = [ t.minlength, t.maxlength ], delete t.minlength, delete t.maxlength)), t;
},
normalizeRule: function(t) {
if (typeof t == "string") {
var n = {};
e.each(t.split(/\s/), function() {
n[this] = !0;
}), t = n;
}
return t;
},
addMethod: function(t, n, r) {
e.validator.methods[t] = n, e.validator.messages[t] = r !== undefined ? r : e.validator.messages[t], n.length < 3 && e.validator.addClassRules(t, e.validator.normalizeRule(t));
},
methods: {
required: function(t, n, r) {
if (!this.depend(r, n)) return "dependency-mismatch";
if (n.nodeName.toLowerCase() === "select") {
var i = e(n).val();
return i && i.length > 0;
}
return this.checkable(n) ? this.getLength(t, n) > 0 : e.trim(t).length > 0;
},
email: function(e, t) {
return this.optional(t) || /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i.test(e);
},
url: function(e, t) {
return this.optional(t) || /^(https?|s?ftp|weixin):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(e);
},
date: function(e, t) {
return this.optional(t) || !/Invalid|NaN/.test((new Date(e)).toString());
},
dateISO: function(e, t) {
return this.optional(t) || /^\d{4}[\/\-]\d{1,2}[\/\-]\d{1,2}$/.test(e);
},
number: function(e, t) {
return this.optional(t) || /^-?(?:\d+|\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$/.test(e);
},
digits: function(e, t) {
return this.optional(t) || /^\d+$/.test(e);
},
creditcard: function(e, t) {
if (this.optional(t)) return "dependency-mismatch";
if (/[^0-9 \-]+/.test(e)) return !1;
var n = 0, r = 0, i = !1;
e = e.replace(/\D/g, "");
for (var s = e.length - 1; s >= 0; s--) {
var o = e.charAt(s);
r = parseInt(o, 10), i && (r *= 2) > 9 && (r -= 9), n += r, i = !i;
}
return n % 10 === 0;
},
minlength: function(t, n, r) {
var i = e.isArray(t) ? t.length : this.getLength(e.trim(t), n);
return this.optional(n) || i >= r;
},
maxlength: function(t, n, r) {
var i = e.isArray(t) ? t.length : this.getLength(e.trim(t), n);
return this.optional(n) || i <= r;
},
rangelength: function(t, n, r) {
var i = e.isArray(t) ? t.length : this.getLength(e.trim(t), n);
return this.optional(n) || i >= r[0] && i <= r[1];
},
min: function(e, t, n) {
return this.optional(t) || e >= n;
},
max: function(e, t, n) {
return this.optional(t) || e <= n;
},
range: function(e, t, n) {
return this.optional(t) || e >= n[0] && e <= n[1];
},
equalTo: function(t, n, r) {
var i = e(r);
return this.settings.onfocusout && i.unbind(".validate-equalTo").bind("blur.validate-equalTo", function() {
e(n).valid();
}), t === i.val();
},
remote: function(t, n, r) {
if (this.optional(n)) return "dependency-mismatch";
var i = this.previousValue(n);
this.settings.messages[n.name] || (this.settings.messages[n.name] = {}), i.originalMessage = this.settings.messages[n.name].remote, this.settings.messages[n.name].remote = i.message, r = typeof r == "string" && {
url: r
} || r;
if (i.old === t) return i.valid;
i.old = t;
var s = this;
this.startRequest(n);
var o = {};
return o[n.name] = t, e.ajax(e.extend(!0, {
url: r,
mode: "abort",
port: "validate" + n.name,
dataType: "json",
data: o,
success: function(r) {
s.settings.messages[n.name].remote = i.originalMessage;
var o = r === !0 || r === "true";
if (o) {
var u = s.formSubmitted;
s.prepareElement(n), s.formSubmitted = u, s.successList.push(n), delete s.invalid[n.name], s.showErrors();
} else {
var a = {}, f = r || s.defaultMessage(n, "remote");
a[n.name] = i.message = e.isFunction(f) ? f(t) : f, s.invalid[n.name] = !0, s.showErrors(a);
}
i.valid = o, s.stopRequest(n, o);
}
}, r)), "pending";
}
}
}), e.format = e.validator.format;
})(jQuery), function(e) {
var t = {};
if (e.ajaxPrefilter) e.ajaxPrefilter(function(e, n, r) {
var i = e.port;
e.mode === "abort" && (t[i] && t[i].abort(), t[i] = r);
}); else {
var n = e.ajax;
e.ajax = function(r) {
var i = ("mode" in r ? r : e.ajaxSettings).mode, s = ("port" in r ? r : e.ajaxSettings).port;
return i === "abort" ? (t[s] && t[s].abort(), t[s] = n.apply(this, arguments), t[s]) : n.apply(this, arguments);
};
}
}(jQuery), function(e) {
e.extend(e.fn, {
validateDelegate: function(t, n, r) {
return this.bind(n, function(n) {
var i = e(n.target);
if (i.is(t)) return r.apply(i, arguments);
});
}
});
}(jQuery), function(e) {
e.validator.defaults.errorClass = "frm_msg_content", e.validator.defaults.errorElement = "span", e.validator.defaults.errorPlacement = function(e, t) {
t.parent().after(e);
}, e.validator.defaults.wrapper = "p", e.validator.messages = {
required: "必选字段",
remote: "请修正该字段",
email: "请输入正确格式的电子邮件",
url: "请输入合法的网址",
date: "请输入合法的日期",
dateISO: "请输入合法的日期 (ISO).",
number: "请输入合法的数字",
digits: "只能输入整数",
creditcard: "请输入合法的信用卡号",
equalTo: "请再次输入相同的值",
accept: "请输入拥有合法后缀名的字符串",
maxlength: e.validator.format("请输入一个长度最多是 {0} 的字符串"),
minlength: e.validator.format("请输入一个长度最少是 {0} 的字符串"),
rangelength: e.validator.format("请输入一个长度介于 {0} 和 {1} 之间的字符串"),
range: e.validator.format("请输入一个介于 {0} 和 {1} 之间的值"),
max: e.validator.format("请输入一个最大为 {0} 的值"),
min: e.validator.format("请输入一个最小为 {0} 的值")
}, function() {
function t(e) {
var t = 0;
e[17].toLowerCase() == "x" && (e[17] = 10);
for (var n = 0; n < 17; n++) t += s[n] * e[n];
return valCodePosition = t % 11, e[17] == o[valCodePosition] ? !0 : !1;
}
function n(e) {
var t = e.substring(6, 10), n = e.substring(10, 12), r = e.substring(12, 14), i = new Date(t, parseFloat(n) - 1, parseFloat(r));
return (new Date).getFullYear() - parseInt(t) < 18 ? !1 : i.getFullYear() != parseFloat(t) || i.getMonth() != parseFloat(n) - 1 || i.getDate() != parseFloat(r) ? !1 : !0;
}
function r(e) {
var t = e.substring(6, 8), n = e.substring(8, 10), r = e.substring(10, 12), i = new Date(t, parseFloat(n) - 1, parseFloat(r));
return i.getYear() != parseFloat(t) || i.getMonth() != parseFloat(n) - 1 || i.getDate() != parseFloat(r) ? !1 : !0;
}
function i(r) {
r = e.trim(r.replace(/ /g, ""));
if (r.length == 15) return !1;
if (r.length == 18) {
var i = r.split("");
return n(r) && t(i) ? !0 : !1;
}
return !1;
}
var s = [ 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 ], o = [ 1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2 ];
e.validator.addMethod("idcard", function(e, t, n) {
return i(e);
}, "身份证格式不正确，或者年龄未满18周岁，请重新填写"), e.validator.addMethod("mobile", function(t, n, r) {
return t = e.trim(t), /^1\d{10}$/.test(t);
}, "请输入正确的手机号码"), e.validator.addMethod("telephone", function(t, n) {
return t = e.trim(t), /^\d{1,4}(-\d{1,12})+$/.test(t);
}, "请输入正确的座机号码，如020-12345678"), e.validator.addMethod("verifycode", function(t, n) {
return t = e.trim(t), /^\d{6}$/.test(t);
}, "验证码应为6位数字"), e.validator.addMethod("byteRangeLength", function(e, t, n) {
return this.optional(t) || e.len() <= n[1] && e.len() >= n[0];
}, "_(必须为{0}到{1}个字节之间)");
}();
}(jQuery);
var i = {
optional: function(e) {
return !1;
},
getLength: function(e) {
return e ? e.length : 0;
}
};
function s(e, t, n) {
return $.validator.methods[e].call(i, t, null, n);
}
var o = $.validator;
return o.rules = {}, $.each(o.methods, function(e, t) {
o.rules[e] = function(e, n) {
return t.call(i, e, null, n);
};
}), o;
} catch (u) {
wx.jslog({
src: "biz_common/jquery.validate.js"
}, u);
}
});define("tpl/simplePopup.html.js", [], function(e, t, n) {
return '<div class="simple_dialog_content">\n    <form id="popupForm_{id}"  method="post"  class="form" onSubmit="return false;">\n         <div class="frm_control_group">\n            {if label}<label class="frm_label">{label}</label>{/if}\n            <span class="frm_input_box">\n                <input type="text" class="frm_input" name="popInput" value="{value}"/>\n                <input style="display:none"/>\n            </span>\n            {if tips}<p class="frm_tips">{tips}</p>{/if}\n        </div>       \n        <div class="js_verifycode"></div>\n    </form>\n</div>\n';
});define("common/lib/MockJax.js", [], function(e, t, n) {
try {
var r = +(new Date);
(function(e) {
function t(t) {
window.DOMParser == undefined && window.ActiveXObject && (DOMParser = function() {}, DOMParser.prototype.parseFromString = function(e) {
var t = new ActiveXObject("Microsoft.XMLDOM");
return t.async = "false", t.loadXML(e), t;
});
try {
var n = (new DOMParser).parseFromString(t, "text/xml");
if (!e.isXMLDoc(n)) throw "Unable to parse XML";
var r = e("parsererror", n);
if (r.length == 1) throw "Error: " + e(n).text();
return n;
} catch (i) {
var s = i.name == undefined ? i : i.name + ": " + i.message;
return e(document).trigger("xmlParseError", [ s ]), undefined;
}
}
function n(t, n, r) {
(t.context ? e(t.context) : e.event).trigger(n, r);
}
function r(t, n) {
var i = !0;
return typeof n == "string" ? e.isFunction(t.test) ? t.test(n) : t == n : (e.each(t, function(s) {
if (n[s] === undefined) return i = !1, i;
typeof n[s] == "object" ? i = i && r(t[s], n[s]) : e.isFunction(t[s].test) ? i = i && t[s].test(n[s]) : i = i && t[s] == n[s];
}), i);
}
function i(t, n) {
if (e.isFunction(t)) return t(n);
if (e.isFunction(t.url.test)) {
if (!t.url.test(n.url)) return null;
} else {
var i = t.url.indexOf("*");
if (t.url !== n.url && i === -1 || !(new RegExp(t.url.replace(/[-[\]{}()+?.,\\^$|#\s]/g, "\\$&").replace(/\*/g, ".+"))).test(n.url)) return null;
}
return t.data && n.data && !r(t.data, n.data) ? null : t && t.type && t.type.toLowerCase() != n.type.toLowerCase() ? null : t;
}
function s(n, r, i) {
var s = function(s) {
return function() {
return function() {
var s;
this.status = n.status, this.statusText = n.statusText, this.readyState = 4, e.isFunction(n.response) && n.response(i), r.dataType == "json" && typeof n.responseText == "object" ? this.responseText = JSON.stringify(n.responseText) : r.dataType == "xml" ? typeof n.responseXML == "string" ? (this.responseXML = t(n.responseXML), this.responseText = n.responseXML) : this.responseXML = n.responseXML : this.responseText = n.responseText;
if (typeof n.status == "number" || typeof n.status == "string") this.status = n.status;
typeof n.statusText == "string" && (this.statusText = n.statusText), s = this.onreadystatechange || this.onload, e.isFunction(s) ? (n.isTimeout && (this.status = -1), s.call(this, n.isTimeout ? "timeout" : undefined)) : n.isTimeout && (this.status = -1);
}.apply(s);
};
}(this);
n.proxy ? v({
global: !1,
url: n.proxy,
type: n.proxyType,
data: n.data,
dataType: r.dataType === "script" ? "text/plain" : r.dataType,
complete: function(e) {
n.responseXML = e.responseXML, n.responseText = e.responseText, n.status = e.status, n.statusText = e.statusText, this.responseTimer = setTimeout(s, n.responseTime || 0);
}
}) : r.async === !1 ? s() : this.responseTimer = setTimeout(s, n.responseTime || 50);
}
function o(t, n, r, i) {
return t = e.extend(!0, {}, e.mockjaxSettings, t), typeof t.headers == "undefined" && (t.headers = {}), t.contentType && (t.headers["content-type"] = t.contentType), {
status: t.status,
statusText: t.statusText,
readyState: 1,
open: function() {},
send: function() {
i.fired = !0, s.call(this, t, n, r);
},
abort: function() {
clearTimeout(this.responseTimer);
},
setRequestHeader: function(e, n) {
t.headers[e] = n;
},
getResponseHeader: function(e) {
if (t.headers && t.headers[e]) return t.headers[e];
if (e.toLowerCase() == "last-modified") return t.lastModified || (new Date).toString();
if (e.toLowerCase() == "etag") return t.etag || "";
if (e.toLowerCase() == "content-type") return t.contentType || "text/plain";
},
getAllResponseHeaders: function() {
var n = "";
return e.each(t.headers, function(e, t) {
n += e + ": " + t + "\n";
}), n;
}
};
}
function u(e, t, n) {
a(e), e.dataType = "json";
if (e.data && y.test(e.data) || y.test(e.url)) {
l(e, t, n);
var r = /^(\w+:)?\/\/([^\/?#]+)/, i = r.exec(e.url), s = i && (i[1] && i[1] !== location.protocol || i[2] !== location.host);
e.dataType = "script";
if (e.type.toUpperCase() === "GET" && s) {
var o = f(e, t, n);
return o ? o : !0;
}
}
return null;
}
function a(e) {
if (e.type.toUpperCase() === "GET") y.test(e.url) || (e.url += (/\?/.test(e.url) ? "&" : "?") + (e.jsonp || "callback") + "=?"); else if (!e.data || !y.test(e.data)) e.data = (e.data ? e.data + "&" : "") + (e.jsonp || "callback") + "=?";
}
function f(t, n, r) {
var i = r && r.context || t, s = null;
return n.response && e.isFunction(n.response) ? n.response(r) : typeof n.responseText == "object" ? e.globalEval("(" + JSON.stringify(n.responseText) + ")") : e.globalEval("(" + n.responseText + ")"), c(t, i, n), h(t, i, n), e.Deferred && (s = new e.Deferred, typeof n.responseText == "object" ? s.resolveWith(i, [ n.responseText ]) : s.resolveWith(i, [ e.parseJSON(n.responseText) ])), s;
}
function l(e, t, n) {
var r = n && n.context || e, i = e.jsonpCallback || "jsonp" + b++;
e.data && (e.data = (e.data + "").replace(y, "=" + i + "$1")), e.url = e.url.replace(y, "=" + i + "$1"), window[i] = window[i] || function(n) {
data = n, c(e, r, t), h(e, r, t), window[i] = undefined;
try {
delete window[i];
} catch (s) {}
head && head.removeChild(script);
};
}
function c(e, t, r) {
e.success && e.success.call(t, r.responseText || "", status, {}), e.global && n(e, "ajaxSuccess", [ {}, e ]);
}
function h(t, r) {
t.complete && t.complete.call(r, {}, status), t.global && n("ajaxComplete", [ {}, t ]), t.global && !--e.active && e.event.trigger("ajaxStop");
}
function p(t, n) {
var r, s, a;
typeof t == "object" ? (n = t, t = undefined) : n.url = t, s = e.extend(!0, {}, e.ajaxSettings, n);
for (var f = 0; f < m.length; f++) {
if (!m[f]) continue;
a = i(m[f], s);
if (!a) continue;
g.push(s), e.mockjaxSettings.log(a, s);
if (s.dataType === "jsonp") if (r = u(s, a, n)) return r;
return a.cache = s.cache, a.timeout = s.timeout, a.global = s.global, d(a, n), function(t, n, i, s) {
r = v.call(e, e.extend(!0, {}, i, {
xhr: function() {
return o(t, n, i, s);
}
}));
}(a, s, n, m[f]), r;
}
return v.apply(e, [ n ]);
}
function d(e, t) {
if (!(e.url instanceof RegExp)) return;
if (!e.hasOwnProperty("urlParams")) return;
var n = e.url.exec(t.url);
if (n.length === 1) return;
n.shift();
var r = 0, i = n.length, s = e.urlParams.length, o = Math.min(i, s), u = {};
for (r; r < o; r++) {
var a = e.urlParams[r];
u[a] = n[r];
}
t.urlParams = u;
}
var v = e.ajax, m = [], g = [], y = /=\?(&|$)/, b = (new Date).getTime();
e.extend({
ajax: p
}), e.mockjaxSettings = {
log: function(t, n) {
if (t.logging === !1 || typeof t.logging == "undefined" && e.mockjaxSettings.logging === !1) return;
if (window.console && console.log) {
var r = "MOCK " + n.type.toUpperCase() + ": " + n.url, i = e.extend({}, n);
if (typeof console.log == "function") console.log(r, i); else try {
console.log(r + " " + JSON.stringify(i));
} catch (s) {
console.log(r);
}
}
},
logging: !0,
status: 200,
statusText: "OK",
responseTime: 500,
isTimeout: !1,
contentType: "text/plain",
response: "",
responseText: "",
responseXML: "",
proxy: "",
proxyType: "GET",
lastModified: null,
etag: "",
headers: {
etag: "IJF@H#@923uf8023hFO@I#H#",
"content-type": "text/plain"
}
}, e.mockjax = function(e) {
var t = m.length;
return m[t] = e, t;
}, e.mockjaxClear = function(e) {
arguments.length == 1 ? m[e] = null : m = [], g = [];
}, e.mockjax.handler = function(e) {
if (arguments.length == 1) return m[e];
}, e.mockjax.mockedAjaxCalls = function() {
return g;
};
})(jQuery);
} catch (i) {
wx.jslog({
src: "common/lib/MockJax.js"
}, i);
}
});define("common/wx/cgiReport.js", [ "common/wx/Tips.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = e("common/wx/Tips.js");
t.error = function(e, t) {
var n = 11;
switch (e) {
case "timeout":
n = 7;
break;
case "error":
n = 8;
break;
case "notmodified":
n = 9;
break;
case "parsererror":
n = 10;
}
t.data.lang && delete t.data.lang, t.data.random && delete t.data.random, t.data.f && delete t.data.f, t.data.ajax && delete t.data.ajax, t.data.token && delete t.data.token, $.ajax({
url: "/misc/jslog?1=1",
data: {
content: "[fakeid={uin}] [xhr] [url={url}] [param={param}] [info={info}] [useragent={userAgent}] [page={page}]".format({
uin: wx.data.uin,
useragent: window.navigator.userAgent,
page: location.href,
url: t.url,
param: $.param(t.data).substr(0, 50),
info: e
}),
id: n,
level: "error"
},
type: "POST"
}), $.ajax({
url: "/misc/jslog?1=1",
data: {
content: "[fakeid={uin}] [xhr] [url={url}] [param={param}] [info={info}] [useragent={userAgent}] [page={page}]".format({
uin: wx.data.uin,
useragent: window.navigator.userAgent,
page: location.href,
url: t.url,
param: $.param(t.data).substr(0, 50),
info: e
}),
id: 6,
level: "error"
},
type: "POST"
}), e == "timeout" && i.err("你的网络环境较差，请稍后重试");
};
} catch (s) {
wx.jslog({
src: "common/wx/cgiReport.js"
}, s);
}
});