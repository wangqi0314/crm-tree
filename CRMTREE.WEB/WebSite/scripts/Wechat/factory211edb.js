define("common/wx/time.js", [], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = function(e) {
return e += "", e.length >= 2 ? e : "0" + e;
}, s = [ "日", "一", "二", "三", "四", "五", "六" ];
function o(e) {
var t = new Date(e * 1e3), n = new Date, r = t.getTime(), o = n.getTime(), u = 864e5;
return o - r < u && n.getDate() == t.getDate() ? "%s:%s".sprintf(i(t.getHours()), i(t.getMinutes())) : o - r < 2 * u && (new Date(t * 1 + u)).getDate() == n.getDate() ? "昨天 %s:%s".sprintf(i(t.getHours()), i(t.getMinutes())) : o - r <= 6 * u ? "星期%s %s:%s".sprintf(s[t.getDay()], i(t.getHours()), i(t.getMinutes())) : t.getFullYear() == n.getFullYear() ? "%s月%s日".sprintf(i(t.getMonth() + 1), i(t.getDate())) : "%s年%s月%s日".sprintf(t.getFullYear(), i(t.getMonth() + 1), i(t.getDate()));
}
function u(e) {
var t = new Date(e * 1e3);
return "%s-%s-%s %s:%s:%s".sprintf(t.getFullYear(), i(t.getMonth() + 1), i(t.getDate()), i(t.getHours()), i(t.getMinutes()), i(t.getSeconds()));
}
return {
timeFormat: o,
getFullTime: u
};
} catch (a) {
wx.jslog({
src: "common/wx/time.js"
}, a);
}
});define("common/qq/events.js", [], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
function i(e) {
e === !0 ? this.data = window.wx.events || {} : this.data = {};
}
i.prototype = {
on: function(e, t) {
return this.data[e] = this.data[e] || [], this.data[e].push(t), this;
},
off: function(e, t) {
this.data[e] && this.data[e].length > 0 && (t && typeof t == "function" ? $.each(this.data[e], function(n, r) {
r === t && this.data[e].splice(n, 1);
}) : this.data[e] = []);
},
trigger: function(e) {
var t = arguments;
this.data[e] && this.data[e].length > 0 && $.each(this.data[e], function(e, n) {
var r = n.apply(this, Array.prototype.slice.call(t, 1));
if (r === !1) return !1;
});
}
}, n.exports = function(e) {
return new i(e);
};
} catch (s) {
wx.jslog({
src: "common/qq/events.js"
}, s);
}
});define("common/wx/media/simpleAppmsg.js", [ "tpl/media/simple_appmsg.html.js", "widget/media.css", "common/qq/Class.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = wx.T, s = e("tpl/media/simple_appmsg.html.js"), o = e("widget/media.css"), u = e("common/qq/Class.js"), a = wx.url("/cgi-bin/getimgdata?"), f = {
"9": {
"1": "图文消息",
"2": "图文消息",
"3": "图文消息",
"4": "图文消息"
},
"10": {
"1": "图文消息",
"2": "图文消息",
"3": "图文消息",
"4": "图文消息"
},
"11": {
"1": "活动消息",
"2": "第三方应用消息",
"3": "商品消息",
"4": "单条商品消息"
}
}, l = function(e, t) {
var n = f[e];
return (n ? n[t] : "") || "未知类型";
}, c = u.declare({
init: function(e) {
if (!e || !e.container) return;
e.appmsg_cover = a + "&mode=small&source=%s&msgid=%s&fileId=%s".sprintf(e.source, e.id, e.file_id), e.type_msg = l(e.type, e.app_sub_type), $(e.container).html(wx.T(s, e)).data(e), this.opt = e;
},
getData: function() {
return this.opt;
}
});
return c;
} catch (h) {
wx.jslog({
src: "common/wx/media/simpleAppmsg.js"
}, h);
}
});define("common/wx/remark.js", [ "common/wx/Tips.js", "common/qq/events.js", "user/user_cgi.js", "common/wx/simplePopup.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = e("common/wx/Tips.js"), s = e("common/qq/events.js"), o = s(!0), u = e("user/user_cgi.js"), a = e("common/wx/simplePopup.js"), f = function() {
this.id = null, this.remarkName = null, this._init();
};
f.prototype = {
_init: function() {
var e = this;
o.on("Remark:change", function(t, n) {
e.show(t, n);
});
},
show: function(e, t) {
this.id = e, this.remarkName = t;
var n = this;
new a({
title: "添加备注",
callback: function(e) {
u.changeRemark(n.id, e, function(t) {
i.suc("修改成功"), o.trigger("Remark:changed", n.id, (e + "").html(!0));
});
},
rule: function(e, t, n) {
return e.length <= 30;
},
value: (t + "").html(!1),
msg: "备注不能超过30个字"
});
},
hide: function() {}
}, n.exports = new f;
} catch (l) {
wx.jslog({
src: "common/wx/remark.js"
}, l);
}
});define("common/wx/RichBuddy.js", [ "common/qq/emoji.js", "tpl/RichBuddy/RichBuddyLayout.html.js", "tpl/RichBuddy/RichBuddyContent.html.js", "widget/rich_buddy.css", "common/wx/Tips.js", "common/qq/Class.js", "common/wx/remark.js", "user/user_cgi.js", "common/qq/events.js", "biz_web/ui/dropdown.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict", e("common/qq/emoji.js");
var i = e("tpl/RichBuddy/RichBuddyLayout.html.js"), s = e("tpl/RichBuddy/RichBuddyContent.html.js"), o = template.compile(s), u = e("widget/rich_buddy.css"), a = e("common/wx/Tips.js"), f = e("common/qq/Class.js"), l = e("common/wx/remark.js"), c = e("common/qq/emoji.js"), h = e("user/user_cgi.js"), p = e("common/qq/events.js"), d = e("biz_web/ui/dropdown.js"), v = p(!0), m = {}, g = {
position: {
left: 0,
top: 0
},
fakeId: ""
}, y = f.declare({
init: function(e) {
e && (this._init_opts = $.extend(!0, this._init_opts, e));
},
$element: null,
$content: null,
hideTimer: null,
namespace: ".RichBuddy",
options: {},
_init_opts: {
hideGroup: !1,
data: undefined
},
_init: function() {
var e = this.options, t = this, n, r = e.fakeId, s = e.position;
this._unbindEvent(), this.$element || (this.$element = $(i).appendTo(document.body)), this.$content = this.$element.find(".buddyRichContent"), this.$loading = this.$element.find(".loadingArea"), typeof this._init_opts.data == "object" && this._init_opts.data !== null && (m = this._init_opts.data), m[r] ? (n = m[r], this.$content.html(o(n)), this._hideLoading(), this._bindEvent()) : (this._showLoading(), h.getBuddyInfo(r, function(n) {
if (!n || !n.base_resp) {
a.err("系统出错，请稍后重试！");
return;
}
if (n.base_resp.ret == 0) {
var i = n.contact_info;
i.groups = n.groups.groups, n = i;
for (var s in n) typeof n[s] == "string" && (n[s] = n[s].replace(/&nbsp;/ig, " "));
n.nick_name = n.nick_name.emoji(), n.fake_id && (m[r] = n), n.fake_id == e.fakeId && (t._hideLoading(), t.$content.html(o(n)), t._bindEvent());
} else switch (+n.base_resp.ret) {
case 1:
a.err("该用户已经对您取消关注");
break;
case 2:
break;
case -3:
a.err("会话过期，请重新登录");
break;
default:
a.err("系统出错，请稍后重试！");
}
}));
},
_showLoading: function() {
this.$loading.show(), this.$content.hide();
},
_hideLoading: function() {
this.$loading.hide(), this.$content.show();
},
_bindEvent: function() {
var e = this, t = this.options, n = m[t.fakeId];
if (!n) return;
this.$element.bind("mouseover" + this.namespace, function(t) {
clearTimeout(e.hideTimer), e.hideTimer = null, e.$element.show();
}).bind("mouseout" + this.namespace, function(t) {
e._mouseout();
}), this.$element.find(".js_changeRemark").bind("click" + this.namespace, function() {
v.trigger("Remark:change", t.fakeId, n.remark_name);
}), v.on("Remark:changed", function(t, n) {
m[t] && (m[t].remark_name = n), e.$element.find(".js_remarkName").html(n);
}), n.groups || (n.groups = []);
var r = [], i;
for (var s = 0; s < n.groups.length; s++) r.push({
name: n.groups[s].name,
value: n.groups[s].id
}), n.group_id == n.groups[s].id && (i = n.groups[s].name);
new d({
container: this.$element.find(".js_group"),
label: i || "请选择",
data: r,
callback: function(e, r) {
if (n.group_id != e) {
var i = t.fakeId;
h.changeGroup(i, e, function(t) {
m[i].GroupID = e;
});
}
},
search: !1
}), this._init_opts.hideGroup && this.$element.find(".js_group_container").hide();
},
_unbindEvent: function() {
if (this.$element) {
var e = this.namespace;
this.$element.find(".js_changeRemark").unbind(e), this.$element.unbind(e), this.$element.stop(), this.$element.css("opacity", 1), this.$element.show();
}
},
_mouseout: function() {
var e = this;
e.hideTimer || (e.hideTimer = setTimeout(function() {
!!e.$element && e.$element.fadeOut(), e.hideTimer = null;
}, 1e3));
},
show: function(e) {
var t = this.options.fakeId;
e && (this.options = e), clearTimeout(this.hideTimer), this.hideTimer = null, e.fakeId !== t && this._init(), this.$element.css(e.position), this.$element.show();
},
hide: function() {
this._mouseout();
}
});
n.exports = y;
} catch (b) {
wx.jslog({
src: "common/wx/RichBuddy.js"
}, b);
}
});define("tpl/msgListItem.html.js", [], function(e, t, n) {
return '{if list.length <= 0}\n    <p class="empty_tips">暂无消息</p>\n{else}\n    {each list as item as index}\n    <li class="message_item {if item.has_reply}replyed{/if}" id="msgListItem{item.id}" data-id="{item.id}">\n        {if (item.fakeid != uin)}\n        <div class="message_opr">\n            {if (item.type != 10 || item.app_sub_type != 2) }\n            <a href="javascript:;" class="js_star icon18_common {if (item.is_starred_msg != 1)}star_gray{else}star_orange{/if}" action="{action}" idx="{item.id}" starred="{item.is_starred_msg}" title="{if (item.is_starred_msg != 1) }收藏消息{else}取消收藏{/if}">取消收藏</a>\n            {/if}\n            {if (item.type!= 1 && item.type != 10 && item.type != 4) }\n            <a href="javascript:;" class="js_save icon18_common save_gray" idx="{item.id}" data-type="{item.type}" title="保存为素材">保存为素材</a>\n            <a href="/cgi-bin/downloadfile?msgid={item.id}&source={item.source}&token={token}" class="icon18_common download_gray" target="_blank" idx="{item.id}" title="下载">下载</a>\n            {/if}\n            {if (item.type == 4) }\n            <a href="/cgi-bin/downloadfile?msgid={item.id}&source={item.source}&token={token}" class="icon18_common download_gray" target="_blank" idx="{item.id}" title="下载">下载</a>\n            {/if}\n            <a href="javascript:;" data-id="{item.id}" data-tofakeid="{item.fakeid}" class="icon18_common reply_gray js_reply" title="快捷回复">快捷回复</a>\n        </div>\n        {/if}\n        <div class="message_info">\n            <div class="message_status"><em class="tips">已回复</em></div>\n            <div class="message_time">{timeFormat item}</div>\n            <div class="user_info">\n                {if (item.fakeid != uin)}\n                <a href="{id2singleURL item}" target="_blank" data-fakeid="{item.fakeid}" class="remark_name">{if item.remark_name}{=item.remark_name}{else}{=item.nick_name.emoji()}{/if}</a>\n                {else}\n                <span data-fakeid="{item.fakeid}" class="remark_name">{if item.remark_name}{=item.remark_name}{else}{=item.nick_name.emoji()}{/if}</span>\n                {/if}\n                \n                <span class="nickname" data-fakeid="{item.fakeid}">{if item.remark_name}(<strong>{=item.nick_name.emoji()}</strong>){/if}</span>\n                \n                {if (item.fakeid != uin)}\n                <a href="javascript:;" class="icon14_common edit_gray js_changeRemark" data-fakeid="{item.fakeid}" title="修改备注" style="display:none;"></a>\n                {/if}\n                {if (item.fakeid != uin)}\n                <a target="_blank" href="{id2singleURL item}" class="avatar" data-fakeid="{item.fakeid}">\n                    <img src="https://res.wx.qq.com/mpres/htmledition/images/icon/page-setting/avatar/icon80_avatar_default.png" data-src="/misc/getheadimg?token={token}&fakeid={item.fakeid}" data-fakeid="{item.fakeid}">\n                </a>\n                {else}\n                <span class="avatar" data-fakeid="{item.fakeid}">\n                    <img src="https://res.wx.qq.com/mpres/htmledition/images/icon/page-setting/avatar/icon80_avatar_default.png" data-src="/misc/getheadimg?token={token}&fakeid={item.fakeid}" data-fakeid="{item.fakeid}">\n                </span>\n                {/if}\n            </div>\n        </div>\n\n        <div class="message_content {if item.type == 1}text{/if}">\n            <div id="wxMsg{item.id}" data-id="{item.id}" class="wxMsg">\n                {mediaInit item}\n            </div>\n        </div>\n\n        {if (item.fakeid != uin)}\n        <div id="quickReplyBox{item.id}" class="js_quick_reply_box quick_reply_box">\n            <label for="" class="frm_label">快速回复:</label>\n            <div class="emoion_editor_wrp js_editor"></div>\n            <div class="verifyCode"></div>\n            <p class="quick_reply_box_tool_bar">\n                <span class="btn btn_primary btn_input" data-id="{item.id}">\n                    <button class="js_reply_OK" data-id="{item.id}" data-fakeid="{item.fakeid}">发送</button>\n                </span><a class="js_reply_pickup btn btn_default pickup" data-id="{item.id}" href="javascript:;">收起</a>\n            </p>\n        </div>\n        {/if}\n    </li>\n    {/each}\n{/if}\n';
});define("common/wx/media/idCard.js", [ "tpl/media/id_card.html.js", "widget/media.css", "common/qq/Class.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = wx.T, s = e("tpl/media/id_card.html.js"), o = e("widget/media.css"), u = e("common/qq/Class.js"), a = wx.url("/misc/getheadimg?1=1"), f = u.declare({
init: function(e) {
if (!e || !e.container) return;
e.avatar = a + "&fakeid=" + e.fakeid, $(e.container).html(wx.T(s, e)).data(e), this.opt = e;
},
getData: function() {
return this.opt;
}
});
n.exports = f;
} catch (l) {
wx.jslog({
src: "common/wx/media/idCard.js"
}, l);
}
});define("common/wx/simplePopup.js", [ "tpl/simplePopup.html.js", "common/wx/popup.js", "biz_common/jquery.validate.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = e("tpl/simplePopup.html.js");
e("common/wx/popup.js"), e("biz_common/jquery.validate.js");
function s(e) {
var t = $.Deferred(), n = this;
n.$dom = $(template.compile(i)(e)).popup({
title: e.title || "输入提示框",
buttons: [ {
text: "确认",
click: function() {
var i = this;
if (r.form()) {
var s = n.$dom.find("input").val().trim();
if (e.callback) {
var o = e.callback.call(i, s);
o !== !1 && this.remove();
} else this.remove();
t.resolve(s);
}
},
type: "primary"
}, {
text: "取消",
click: function() {
this.remove();
},
type: "default"
} ],
className: "simple label_block"
}), n.$dom.find("input").val(e.value).focus(), $.validator.addMethod("_popupMethod", function(t, n, r) {
return e && e.rule && e.rule(t.trim(), n, r);
}, e.msg);
var r = n.$dom.find("form").validate({
rules: {
popInput: {
required: !0,
_popupMethod: !0
}
},
messages: {
popInput: {
required: "输入框内容不能为空"
}
},
onfocusout: !1
});
return t.callback = t.done, t;
}
n.exports = s;
} catch (o) {
wx.jslog({
src: "common/wx/simplePopup.js"
}, o);
}
});define("common/wx/media/appmsg.js", [ "widget/media.css", "common/wx/time.js", "tpl/media/appmsg.html.js", "common/qq/Class.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict", e("widget/media.css");
var i = wx.T, s = e("common/wx/time.js"), o = e("tpl/media/appmsg.html.js"), u = e("common/qq/Class.js"), a = u.declare({
init: function(e) {
if (!e || !e.container) return;
e.data = e.data || $.extend({}, e);
var t = e.data, n = t.multi_item || [], r = n.length, i = null, u = [];
if (r <= 0) return;
i = n[0];
for (var a = 1; a < r; ++a) u.push(n[a]);
var f = {
id: t.app_id,
type: e.type,
file_id: t.file_id,
time: t.create_time ? s.timeFormat(t.create_time) : "",
isMul: r > 1,
first: i,
list: u,
token: wx.data.t,
showEdit: e.showEdit || !1,
showMask: e.showMask || !1
};
$(e.container).html(wx.T(o, f)).data("opt", e), this.renderData = f;
},
getData: function() {
return this.renderData;
}
});
return a;
} catch (f) {
wx.jslog({
src: "common/wx/media/appmsg.js"
}, f);
}
});define("common/wx/tooltip.js", [ "tpl/tooltip.html.js", "widget/tooltip.css" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = e("tpl/tooltip.html.js");
e("widget/tooltip.css");
var s = {
dom: "",
content: "",
position: {
x: 0,
y: 0
}
}, o = function(e) {
this.options = e = $.extend(!0, {}, s, e), this.$dom = $(this.options.dom), this.init();
};
o.prototype = {
constructor: o,
init: function() {
var e = this;
e.pops = [], e.$dom.each(function() {
var t = $(this), n = t.data("tooltip"), r = $(template.compile(i)(n ? $.extend(!0, {}, e.options, {
content: n
}) : e.options));
e.pops.push(r), $("body").append(r), r.css("display", "none"), t.on("mouseenter", function() {
var n = t.offset();
r.css({
top: n.top + t.height() + (e.options.position.y || 0),
left: n.left + t.width() / 2 - r.width() / 2 + (e.options.position.x || 0)
}), r.show();
}).on("mouseleave", function() {
r.hide();
}), t.data("tooltip_pop", r);
});
},
show: function() {
var e = this, t = 0, n = e.pops.length;
for (var t = 0; t < n; t++) e.pops[t].show();
},
hide: function() {
var e = this, t = 0, n = e.pops.length;
for (var t = 0; t < n; t++) e.pops[t].hide();
}
}, n.exports = o;
} catch (u) {
wx.jslog({
src: "common/wx/tooltip.js"
}, u);
}
});define("common/wx/media/cardmsg.js", [ "widget/media.css", "common/wx/time.js", "tpl/media/cardmsg.html.js", "common/qq/Class.js", "cardticket/send_card.js", "common/wx/Tips.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict", e("widget/media.css");
var i = wx.T, s = e("common/wx/time.js"), o = e("tpl/media/cardmsg.html.js"), u = e("common/qq/Class.js"), a = e("cardticket/send_card.js"), f = e("common/wx/Tips.js"), l = u.declare({
type: 16,
init: function(e) {
this.opt = e.opt, this.info = e.infos[this.type], this.index = this.info && this.info.index, this.data = this.opt.data, this.msgSender = e;
},
getData: function() {
return {
type: this.type,
cardid: this.data.id,
cardnum: this.data.cardnum
};
},
click: function() {
var e = this;
return this.send_card = new a({
multi: !1,
selectComplete: function(t, n) {
if (!t) {
f.err("请选择卡券");
return;
}
t.cardnum = n, e.fillData(t), e.msgSender.select(e.index);
},
source: "直接群发卡券"
}), this.send_card.show(), !1;
},
fillData: function(e) {
this.data = e, this.msgSender.type = this.type;
var t = i(o, e);
$("." + this.info.selector).html(t), this.msgSender.select(this.index);
},
isValidate: function() {
return this.data.id ? !0 : (f.err("请选择卡券"), !1);
}
});
return l;
} catch (c) {
wx.jslog({
src: "common/wx/media/cardmsg.js"
}, c);
}
});define("common/wx/media/video.js", [ "widget/media/richvideo.css", "widget/media.css", "biz_web/lib/video.js", "common/wx/Cgi.js", "common/wx/time.js", "common/qq/Class.js", "biz_web/lib/swfobject.js", "tpl/media/video.html.js", "tpl/media/videomsg.html.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict", e("widget/media/richvideo.css"), e("widget/media.css");
var i = e("biz_web/lib/video.js"), s = e("common/wx/Cgi.js"), o = e("common/wx/time.js"), u = e("common/qq/Class.js"), a = e("biz_web/lib/swfobject.js"), f = e("tpl/media/video.html.js"), l = e("tpl/media/videomsg.html.js"), c = wx.T, h = wx.data.t, p = document, d = !!a.ua.pv[0], v = p.createElement("video"), m = "#wxVideoBoxFold", g = "#wxVideoPlayer", y = "wxVideo", b = {}, w = navigator.userAgent.toLowerCase(), E = /msie/.test(w), S = /firefox/.test(w);
i.options.flash.swf = wx.path.video;
var x = {
id: "",
source: "",
type: "",
file_id: ""
}, T = 5e3, N = function(e) {
if (e.video_url) {
var t = "tmp" + (Math.random() * 1e5 | 0), n = $('<video id="%s"></video>'.sprintf(t)).appendTo("body");
i("#" + t).ready(function() {
$("#" + t).hide();
var n = this;
this.on("error", function(t) {
n.dispose(), e.dom.find(".loading_tips").show(), e.video_url = "", setTimeout(function() {
N(e);
}, T);
}), this.on("loadedmetadata", function() {
n.dispose(), $(e.selector).children().remove(), e.for_transfer = !1, e.digest = e.digest ? e.digest.html(!1) : "", new C(e);
});
var r = e.video_url;
v.canPlayType ? n.src(r) : n.src([ {
type: "video/x-flv",
src: r + "&trans=1"
} ]), n.play();
});
} else s.get({
url: wx.url("/cgi-bin/appmsg?action=get_video_url&videoid=%s".sprintf(e.video_id)),
error: function() {
setTimeout(function() {
N(e);
}, T);
}
}, function(t) {
e.video_url = t.video_url || "", e.video_download_url = t.video_download_url || "", setTimeout(function() {
N(e);
}, T);
});
}, C = u.declare({
init: function(e) {
var t = this;
$(e.selector).data("opt", e), e = $.extend(!0, {}, x, e), t.id = e.id, t.source = e.source, t.file_id = e.file_id, t.type = e.type, t.video_url = e.video_url, t.tpl = e.tpl, t.ff_must_flash = e.ff_must_flash, e.src = t.getVideoURL(), e.token = h || wx.data.t, e.time = e.create_time ? o.timeFormat(e.create_time) : "", e.digest = e.digest ? e.digest.replace(/<br.*>/g, "\n").html() : "", e.for_network = typeof e.video_url == "string" ? !e.video_url : !e.content;
var n = e.tpl == "videomsg" ? $(c(l, e)) : $(c(f, e));
t.dom = e.dom = $(e.selector).append(n), e.tpl == "videomsg" && e.for_transfer && N(e, t.dom), t.dom.find(".video_desc").length && t.dom.find(".video_desc").html(t.dom.find(".video_desc").attr("data-digest").replace(/\n/g, "<br>")), t.dom.find(".wxVideoScreenshot").on("click", function() {
t.dom.find(".mediaContent").css({
height: "auto"
}), t.play(e.play);
}), t.dom.find(".wxNetworkVideo").on("click", function() {
window.open($(this).attr("data-contenturl"));
}), t.dom.find(".video_switch").click(function() {
t.dom.find(".mediaContent").css({
height: "104px"
}), t.pause(e.pause);
});
},
getVideoURL: function() {
var e = this.source, t = this.id, n = this.file_id;
return !e || (e = "&source=" + e), this.video_url || "/cgi-bin/getvideodata?msgid={msgid}&fileid={fileid}&token={token}{source}".format({
msgid: t,
fileid: n,
source: e,
token: wx.data.t
});
},
canPlayType: function() {
var e = this.type;
return !v.canPlayType && !d;
},
play: function(e) {
var t = this;
if (t.canPlayType()) {
alert("您当前浏览器无法播放视频，请安装Flash插件/更换Chrome浏览器");
return;
}
var n = this.id, r = this.player;
if (!!r) return $("#wxVideoBox" + n).addClass("wxVideoPlaying").find(".wxVideoPlayContent").show(), r.play(), e && e(this);
var s = t.getVideoURL();
$("#wxVideoBox" + n).addClass("wxVideoPlaying").find(".wxVideoPlayContent").show();
var o = t.tpl == "videomsg" ? {
width: "100%",
height: "100%"
} : {};
i("#wxVideo" + n, o).ready(function() {
r = this;
var i = 0;
return r.on("fullscreenchange", function() {
i ? ($("#wxVideoPlayer" + n).css({
overflow: "hidden",
zoom: "1"
}), $("#wxVideoBox" + n).css({
"z-index": "0"
})) : ($("#wxVideoPlayer" + n).css({
overflow: "visible",
zoom: "normal"
}), $("#wxVideoBox" + n).css({
"z-index": "1"
})), i = ~i;
}), r.on("ended", function() {
this.currentTime(0);
}), E || !v.canPlayType || t.ff_must_flash && S ? r.src([ {
type: "video/x-flv",
src: s + "&trans=1"
} ]) : r.src(s), r.play(), t.player = r, e && e(this);
});
},
pause: function(e) {
var t = this.player;
t && t.pause(), $("#wxVideoBox" + this.id).removeClass("wxVideoPlaying").find(".wxVideoPlayContent").hide(), e && e(this);
}
});
return C;
} catch (k) {
wx.jslog({
src: "common/wx/media/video.js"
}, k);
}
});define("common/wx/media/img.js", [ "widget/media.css", "tpl/media/img.html.js", "common/qq/Class.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = wx.T, s = e("widget/media.css"), o = e("tpl/media/img.html.js"), u = e("common/qq/Class.js"), a = u.declare({
init: function(e) {
if (!e || !e.container) return;
var t = e;
$(e.container).html(o.format({
token: wx.data.t,
id: e.file_id,
msgid: e.msgid || "",
source: e.source,
ow: ~e.fakeid
})).data("opt", e), this.data = t;
},
getData: function() {
return this.data;
}
});
return a;
} catch (f) {
wx.jslog({
src: "common/wx/media/img.js"
}, f);
}
});define("common/wx/media/audio.js", [ "biz_web/lib/soundmanager2.js", "tpl/media/audio.html.js", "widget/media.css", "common/qq/Class.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = wx.T, s = e("biz_web/lib/soundmanager2.js"), o = e("tpl/media/audio.html.js"), u = e("widget/media.css"), a = e("common/qq/Class.js"), f = null, l = null, c = null, h = "wxAudioPlaying", p = function() {
c = s, c.setup({
url: "/mpres/zh_CN/htmledition/plprecorder/biz_web/",
preferFlash: !1,
debugMode: !1
});
};
$(window).load(function() {
p();
});
var d = {
id: "",
source: "",
file_id: ""
}, v = a.declare({
init: function(e) {
var t = this;
$.extend(!0, t, d, e), this.soundId = this.id || this.file_id, t.play_length = Math.ceil(t.play_length * 1 / 1e3);
var n = $(i(o, t));
t.dom = $(e.selector).append(n).data("opt", e), n.click(function() {
t.toggle();
});
},
getAudioURL: function() {
var e = this.source, t = this.id, n = this.file_id;
return !e || (e = "&source=" + e), wx.url("/cgi-bin/getvoicedata?msgid={id}&fileid={fileid}{source}".format({
id: t,
fileid: n,
source: e
}));
},
isPlaying: function() {
return f != null && this == f;
},
toggle: function() {
this.isPlaying() ? this.stop() : (f && f.stop(), this.play());
},
play: function(e) {
var t = this;
if (this.isPlaying()) return;
this.dom.addClass(h), !!f && f.dom.removeClass(h), f = this, this.sound || (!c && p(), this.sound = c.createSound({
id: t.soundId,
url: t.getAudioURL(),
onfinish: function() {
f && (f.dom.removeClass(h), f = null);
}
})), c.play(this.soundId), e && e(this);
},
stop: function(e) {
if (!this.isPlaying()) return;
f = null, this.dom.removeClass(h), c.stop(this.soundId), e && e(this);
}
});
n.exports = v;
} catch (m) {
wx.jslog({
src: "common/wx/media/audio.js"
}, m);
}
});define("common/wx/media/factory.js", [ "common/wx/media/img.js", "common/wx/media/audio.js", "common/wx/media/video.js", "common/wx/media/appmsg.js", "common/qq/emoji.js" ], function(e, t, n) {
try {
var r = +(new Date);
"use strict";
var i = e("common/wx/media/img.js"), s = e("common/wx/media/audio.js"), o = e("common/wx/media/video.js"), u = e("common/wx/media/appmsg.js");
e("common/qq/emoji.js");
var a = {
"1": function(e, t) {
return $(e).html(t.content.emoji());
},
"2": function(e, t) {
return t.container = $(e), new i(t);
},
"3": function(e, t) {
return t.selector = $(e), t.source = "file", new s(t);
},
"4": function(e, t) {
return t.selector = $(e), t.id = t.file_id, t.source = "file", new o(t);
},
"10": function(e, t) {
return t.container = $(e), t.showMask = !1, new u(t);
},
"11": function(e, t) {
return t.container = $(e), t.showMask = !1, new u(t);
},
"15": function(e, t) {
return t.multi_item && t.multi_item[0] && (t.title = t.multi_item[0].title, t.digest = t.multi_item[0].digest), t.selector = $(e), t.id = Math.random() * 1e6 | 0, t.tpl = "videomsg", t.for_selection = !1, t.for_operation = !1, new o(t);
}
}, f = {
render: function(e, t) {
a[t.type] && $(e).length > 0 && a[t.type]($(e).html(""), t);
},
itemRender: a
};
n.exports = f;
} catch (l) {
wx.jslog({
src: "common/wx/media/factory.js"
}, l);
}
});