define(
    "media/media_dialog.js",
    ["widget/media/media_dialog.css",
        "widget/media/richvideo.css",
        "common/wx/popup.js",
        "common/wx/Tips.js",
        "media/media_cgi.js",
        "common/wx/upload.js",
        "biz_web/ui/checkbox.js",
        "common/wx/pagebar.js",
        "common/wx/media/audio.js",
        "common/wx/media/img.js",
        "common/wx/media/video.js",
        "common/wx/media/appmsg.js",
        "common/wx/time.js",
        "tpl/media/dialog/file_layout.html.js",
        "tpl/media/dialog/appmsg_layout.html.js",
        "tpl/media/dialog/videomsg_layout.html.js",
        "tpl/media/dialog/file.html.js"],
    function (e, t, n) {
        try {
            var r = +(new Date);
            "use strict", e("widget/media/media_dialog.css"), e("widget/media/richvideo.css"), e("common/wx/popup.js");
            var i = wx.T, s = null, o = null, u = "media align_edge", a = e("common/wx/Tips.js"), f = e("media/media_cgi.js"), l = e("common/wx/upload.js"), c = e("biz_web/ui/checkbox.js"), h = e("common/wx/pagebar.js"), p = l.uploadBizFile, d = template.render, v = e("common/wx/media/audio.js"), m = e("common/wx/media/img.js"), g = e("common/wx/media/video.js"), y = e("common/wx/media/appmsg.js"), b = e("common/wx/time.js"), w = b.timeFormat, E = e("tpl/media/dialog/file_layout.html.js"), S = e("tpl/media/dialog/appmsg_layout.html.js"), x = e("tpl/media/dialog/videomsg_layout.html.js"), T = e("tpl/media/dialog/file.html.js"), N = 1, C = {}, k = {
                appmsg: S,
                videomsg: x,
                file: E
            };
            template.helper("mediaDialogtimeFormat", function (e) {
                return w(e);
            }), template.helper("mediaDialogInit", function (e) {
                return e.file_id  (C[e.file_id] = e, "") : "";
            });
            var L = {
                "2": function (e, t) {
                    return new m({
                        container: $("#" + e.attr("id")),
                        file_id: t.file_id,
                        source: "file",
                        fakeid: t.fakeid
                    });
                },
                "3": function (e, t) {
                    var n = t;
                    return n.selector = "#" + e.attr("id"), n.source = "file", new v(n);
                },
                "4": function (e, t) {
                    var n = t;
                    return n.selector = "#" + e.attr("id"), n.id = n.file_id, n.source = "file", new g(n);
                },
                "15": function (e, t) {
                    var n = t;
                    return n.selector = e, n.id = n.app_id, n.tpl = "videomsg", n.for_selection = !0, n.for_transfer = !!n.content, n.hide_transfer = !!n.content, n.video_id = n.content, new g(n);
                }
            };
            function A(e, t, n, r, i) {
                var o = e / t + 1, u = new h({
                    container: $(".pageNavigator", s.popup("get")),
                    perPage: t,
                    first: !1,
                    last: !1,
                    isSimple: !0,
                    initShowPage: o,
                    totalItemsNum: n,
                    callback: function (e) {
                        var n = e.currentPage;
                        if (n == o) return;
                        n--, i.begin = t * n, r(i);
                    }
                });
            }
            function O(e, t, n, r, f, l) {
                s && s.popup("remove"), n == 15 && (e = "videomsg");
                var c = N++;
                s = $(i(k[e], {
                    tpl: t,
                    upload_id: c,
                    type: n,
                    token: wx.data.t
                }).trim()).popup({
                    title: "选择素材",
                    className: u,
                    width: 960,
                    onOK: function () {
                        if (f && !f()) return !0;
                        this.remove(), s = null;
                    },
                    onCancel: function () {
                        this.remove(), s = null;
                    }
                }), o = s.popup("get"), p({
                    container: "#js_media_dialog_upload" + c,
                    type: n,
                    onAllComplete: function () {
                        a.suc("上传成功"), l.begin = 0, M(l);
                    }
                });
                if (!!r) {
                    e == "file" && ($(".js_media_list", o).html(i(T, {
                        list: r
                    })), $(".frm_radio[type=radio]", o).checkbox(!1), $(".media_content", o).each(function () {
                        var e = $(this), t = e.data("id"), n = e.data("type"), r = C[t];
                        if (!r) return;
                        n && L[n] && L[n](e, r);
                    }));
                    if (e == "appmsg") {
                        var h = r.length, d = $(".js_appmsg_list .inner", o);
                        for (var v = 0; v < h; ++v) {
                            var m = r[v], g = d.eq(v % 2), b = m.app_id, w = $('<div id="appmsg%s"></div>'.sprintf(b), g).appendTo(g);
                            new y({
                                container: w,
                                data: m,
                                showMask: !0
                            });
                        }
                    }
                    if (e == "videomsg") {
                        var E = o.find("#js_videomsg_list").find(".inner");
                        r.each(function (e, t) {
                            var r = E.eq(t % 2), i = $('<div id="appmsg%s"></div>'.sprintf(e.app_id), r).appendTo(r);
                            L[n] && L[n](i, e);
                        });
                    }
                    s.popup("resetPosition");
                }
            }
            function M(e) {
                var t = e.type, n = e.onSelect, r = e.count || 10, i = e.begin || 0;
                O("file", "loading"), f.getList(t, i, r, function (u) {
                    if (!s) return;
                    var a = {
                        "2": "img_cnt",
                        "3": "voice_cnt",
                        "4": "video_cnt"
                    }, f = u.file_item, l = u.file_cnt[a[t]];
                    f.length <= 0  O("file", "no-media", t, f, null, e) : (O("file", "files", t, f, function () {
                        var e = o.find('[name="media"]:checked').val(), r = $("#fileItem" + e).data("opt");
                        return r  (n(t, r), !0) : !1;
                    }, e), A(i, r, l, M, e));
                });
            }
            function _(e) {
                return e.find(".appmsg.selected,.Js_videomsg.selected");
            }
            function D(e) {
                var t = e.type, n = e.onSelect, r = e.count || 10, i = e.begin || 0;
                O("appmsg", "loading"), f.appmsg.getList(t, i, r, function (u) {
                    if (!s) return;
                    var f = {
                        "10": "app_msg_cnt",
                        "11": "commondity_msg_cnt",
                        "15": "video_msg_cnt"
                    }, l = u.item, c = u.file_cnt[f[t]];
                    l.length <= 0  O("appmsg", "no-media", t, l, null, e) : (O("appmsg", "files", t, l, function () {
                        var e = _(o).parent().data("opt");
                        return e  (n(t, e), !0) : !1;
                    }), A(i, r, c, D, e), t == 15  (o.on("click", ".Js_videomsg", function () {
                        $(this).find(".loading_tips").length  a.err("视频在转码中，不能选择") : $(this).find(".title").text().trim() != "" && (o.find(".Js_videomsg").removeClass("selected"), $(this).addClass("selected"));
                    }), o.on("mouseenter", ".Js_videomsg", function () {
                        $(this).find(".title").text().trim() == "" && $(this).addClass("no_title");
                    }), o.on("mouseleave", ".Js_videomsg", function () {
                        $(this).removeClass("no_title");
                    })) : o.on("click", ".appmsg", function () {
                        _(o).removeClass("selected"), $(this).addClass("selected");
                    }));
                });
            }
            return {
                getFile: M,
                getAppmsg: D
            };
        } catch (P) {
            wx.jslog({
                src: "media/media_dialog.js"
            }, P);
        }
    });

define(
    "common/wx/richEditor/emotionEditor.js",
    ["widget/emotion_editor.css",
        "tpl/richEditor/emotionEditor.html.js",
        "common/wx/richEditor/wysiwyg.js",
        "common/wx/richEditor/emotion.js",
        "common/wx/upload.js",
        "common/wx/Tips.js",
        "common/qq/Class.js"],
        function (e, t, n) {
            try {
                var r = +(new Date);
                "use strict";
                var i = wx.T, s = e("widget/emotion_editor.css"), o = e("tpl/richEditor/emotionEditor.html.js"), u = e("common/wx/richEditor/wysiwyg.js"), a = e("common/wx/richEditor/emotion.js"), f = e("common/wx/upload.js"), l = e("common/wx/Tips.js"), c = f.uploadCdnFile, h = e("common/qq/Class.js"), p = {
                    isHTML: !0,
                    wordlimit: 500,
                    hideUpload: !0
                }, d = 1, v = h.declare({
                    init: function (e, t) {
                        var n = this;
                        t = this.opt = $.extend(!0, {}, p, t), n.selector$ = e, t.gid = d++, n.selector$.html(i(o, t)), n.emotion = new a(e.find(".js_emotionArea")), n.wysiwyg = new u(e.find(".js_editorArea"), {
                            init: function () {
                                e.find(".js_editorTip").html("还可以输入<em>{l}</em>字".format({
                                    l: t.wordlimit
                                }));
                            },
                            textFilter: function (e) {
                                return e = n.emotion.getEmotionText(e).replace(/<br.*>/ig, "\n").replace(/<.*>/g, ""), e = e.html(!1), t.isHTML  e : e.replace(/<br.*>/ig, "\n").replace(/<.*>/g, "");
                            },
                            nodeFilter: function (e) {
                                var t = "";
                                return e.nodeName.toUpperCase() === "IMG" && (t = e), t;
                            },
                            change: function () {
                                var r = n.getContent(), i = t.wordlimit - r.length, s = e.find(".js_editorTip");
                                i < 0  s.html("已超出<em{cls}>{l}</em>字".format({
                                    l: -i,
                                    cls: ' class="warn"'
                                })) : s.html("还可以输入<em>{l}</em>字".format({
                                    l: i
                                }));
                            }
                        }), n.upload = c({
                            container: e.find(".js_upload"),
                            type: 2,
                            multi: !1,
                            onComplete: function (e, t, r, i, s) {
                                if (!i || !i.base_resp || i.base_resp.ret != 0) return;
                                var o = i.content;
                                l.suc("上传成功"), n.wysiwyg.insertHTML(o);
                            }
                        }), n._initEvent(), n.insertHTML(t.text);
                    },
                    _initEvent: function () {
                        var e = $(".js_switch", this.selector$), t = this.emotion, n = this.wysiwyg;
                        t.click(function (e) {
                            return n.insertHTML(t.getEmotionHTML(e)), !1;
                        }), t.hide(), e.click(function () {
                            t.show();
                        }), $(document).on("click", "*", function (e) {
                            var n = $(e.target), r = n.filter(".js_switch"), i = n.filter(".js_emotion_i"), s = n.filter(".emotions_item");
                            !r.length && !i.length && !s.length && t.hide();
                        });
                    },
                    setContent: function (e) {
                        var t = e || "";
                        t = this.opt.linebreak  t.replace(/\n/g, "<br>") : t, e = a.emoji(t), this.wysiwyg.setContent(e, t.html(!1));
                    },
                    insertHTML: function (e) {
                        e = e || "", this.wysiwyg.insertHTML(e);
                    },
                    getContent: function () {
                        var e = this.wysiwyg.getContent();
                        return e = typeof e == "string"  e.trim() : "", this.opt.linebreak  e.replace(/<br[^>]*>/ig, "\n") : e;
                    },
                    getHTML: function () {
                        var e = this.wysiwyg.getHTML().html(!1);
                        return typeof e == "string"  e.trim() : "";
                    }
                });
                n.exports = v;
            } catch (m) {
                wx.jslog({
                    src: "common/wx/richEditor/emotionEditor.js"
                }, m);
            }
        });

define("common/qq/jquery.plugin/tab.js",
    ["tpl/tab.html.js",
        "widget/msg_tab.css"],
        function (e, t, n) {
            try {
                var r = +(new Date);
                "use strict";
                var i = {
                    index: 0
                }, s = e("tpl/tab.html.js"), o = e("widget/msg_tab.css");
                $.fn.tab = function (e) {
                    if (!e || !e.tabs) return;
                    this.html(wx.T(s, {
                        tabs: e.tabs
                    }));
                    var t = this, n = t.find(".tab_navs"), r = n.find(".tab_nav"), o = r.length, u = null, a = null, f = function (n) {
                        var r = n.data("tab"), i = n.data("type");
                        !r || (u != n && (u && u.removeClass("selected"), a && a.hide(), u = n, a = t.find(r).parent(), a.show(), u.addClass("selected")), !!e.select && e.select(n, a, r, i));
                    }, l = function (n) {
                        var r = n.data("tab"), i = n.data("type");
                        return e.click  e.click(n, t.find(r), r, i) : !0;
                    };
                    return e = $.extend(!0, {}, i, e), r.each(function (n) {
                        var r = e.index, i = $(this).addClass("width" + o), s = i.data("tab");
                        n == r  f(i) : t.find(s).parent().hide(), n == o - 1 && i.addClass("no_extra");
                    }), n.on("click", ".tab_nav", function () {
                        var e = l($(this));
                        return e != 0 && f($(this));
                    }), {
                        getLen: function () {
                            return o;
                        },
                        getTabs: function () {
                            return r;
                        },
                        select: function (e) {
                            return f(r.eq(e));
                        }
                    };
                };
            } catch (u) {
                wx.jslog({
                    src: "common/qq/jquery.plugin/tab.js"
                }, u);
            }
        });

define("common/wx/verifycode.js",
    ["widget/verifycode.css",
        "tpl/verifycode.html.js",
        "common/qq/events.js"],
    function (e, t, n) {
        try {
            var r = +(new Date);
            "use strict", e("widget/verifycode.css");
            var i = e("tpl/verifycode.html.js"), s = "/cgi-bin/verifycoder=", o = e("common/qq/events.js"), u = o(!0);
            function a(e) {
                var t = this;
                this.$dom = $(i), this.$img = this.$dom.find("img"), this.$input = this.$dom.find("input"), this.$img.on("load", function () {
                    u.trigger("VerifyCode:load", t);
                }), this.$dom.find("a").click(function (e) {
                    t.$img.attr("src", s + +(new Date));
                }).click(), e && $(e).append(this.$dom) && (this.$container = $(e));
            }
            a.prototype.getCode = function () {
                return this.$input.val();
            }, a.prototype.focus = function () {
                this.$input.focus();
            }, a.prototype.getInput = function () {
                return this.$input;
            }, a.prototype.refresh = function () {
                this.$img.attr("src", s + +(new Date));
            }, $.fn.verifycode = function () {
                return this.each(function () {
                    $.data(this, "verifycode", new a(this));
                });
            }, n.exports = a;
        } catch (f) {
            wx.jslog({
                src: "common/wx/verifycode.js"
            }, f);
        }
    });

define("message/renderList.js",
    ["common/qq/emoji.js",
        "common/wx/simplePopup.js",
        "common/qq/Class.js",
        "common/wx/media/img.js",
        "common/wx/media/audio.js",
        "common/wx/media/video.js",
        "common/wx/media/idCard.js",
        "tpl/msgListItem.html.js",
        "common/wx/RichBuddy.js",
        "common/wx/remark.js",
        "common/wx/media/simpleAppmsg.js",
        "common/qq/events.js",
        "message/message_cgi.js",
        "common/wx/time.js",
        "common/wx/Tips.js",
        "tpl/message/video_popup.html.js",
        "common/wx/richEditor/emotionEditor.js",
        "common/wx/verifycode.js"],
        function (e, t, n) {
            try {
                var r = +(new Date);
                "use strict", e("common/qq/emoji.js");
                var i = e("common/wx/simplePopup.js"), s = e("common/qq/Class.js"), o = e("common/wx/media/img.js"), u = e("common/wx/media/audio.js"), a = e("common/wx/media/video.js"), f = e("common/wx/media/idCard.js"), l = e("tpl/msgListItem.html.js"), c = e("common/wx/RichBuddy.js"), h = e("common/wx/remark.js"), p = e("common/wx/media/simpleAppmsg.js"), d = e("common/qq/events.js"), v = d(!0), m = e("message/message_cgi.js"), g = e("common/wx/time.js"), y = e("common/wx/Tips.js"), b = g.timeFormat, w = e("tpl/message/video_popup.html.js"), E = !1, S = {
                    "1": function (e, t) {
                        return e.html(t.content.emoji());
                    },
                    "2": function (e, t) {
                        return new o({
                            container: $("#" + e.attr("id")),
                            file_id: 0,
                            msgid: t.id,
                            source: t.source,
                            fakeid: t.fakeid
                        });
                    },
                    "3": function (e, t) {
                        var n = t;
                        return n.selector = "#" + e.attr("id"), new u(n);
                    },
                    "4": function (e, t) {
                        var n = t;
                        return n.selector = "#" + e.attr("id"), n.ff_must_flash = !0, new a(n);
                    },
                    "42": function (e, t) {
                        var n = t;
                        return n.container = "#" + e.attr("id"), new f(n);
                    },
                    "10": function (e, t) {
                        var n = t;
                        return n.container = "#" + e.attr("id"), new p(n);
                    },
                    "15": function (e, t) {
                        var n = t;
                        return n.selector = e, n.tpl = "videomsg", n.id = Math.random() * 1e5 | 0, new a(n);
                    }
                };
                function x(e) {
                    var t = new c;
                    $(".avatar", e).mouseover(function () {
                        var e = $(this), n = parseInt(e.attr("data-fakeid"), 10), r = e.offset(), i = e.width();
                        if (n == wx.data.uin) return;
                        t.show({
                            fakeId: n,
                            position: {
                                left: r.left + i + 2,
                                top: r.top
                            }
                        });
                    }).mouseout(function () {
                        t.hide();
                    });
                }
                function T(e) {
                    $(".js_changeRemark", e).unbind("click").click(function (t) {
                        var n = $(this), r = n.closest("li.msgListItem"), i = n.attr("data-fakeid"), s = $(".nickname[data-fakeid=" + i + "]", e), o = $(".remark_name[data-fakeid=" + i + "]", e), u = $.trim(s.html()) == ""  "" : o.html();
                        h.show(i, u);
                    }), v.on("Remark:changed", function (t, n) {
                        var r, i, s, o;
                        r = $(".nickname[data-fakeid=" + t + "]", e), i = $(".remark_name[data-fakeid=" + t + "]", e), s = $.trim(r.html()) == ""  "" : i.html(), o = s == ""  i.html() : r.find("strong").html(), n == "" && s != ""  (r.html(""), i.html(o)) : n != "" && s == ""  (i.html(n), r.html("(<strong>{nickName}</strong>)".format({
                            nickName: o
                        }))) : n != "" && s != "" && i.html(n);
                    });
                }
                function N(e) {
                    $(e).off("click", ".js_save").on("click", ".js_save", function (e) {
                        var t = $(this), n = t.attr("idx"), r = t.attr("data-type");
                        r == 4  $(w).popup({
                            title: "保存为视频消息",
                            onOK: function () {
                                var e = this.get().find(".title").val(), t = this.get().find(".digest").val();
                                if (e.length < 1 || e.length > 64) return y.err("请输入1到64个字的标题"), !0;
                                if (t != "" && t.length > 120) return y.err("简介字数不能超过120字"), !0;
                                m.save(n, e, t, function () { });
                            },
                            onCancel: function () {
                                this.hide();
                            },
                            onHide: function () {
                                this.remove();
                            }
                        }) : new i({
                            title: "填写素材名字",
                            callback: function (e) {
                                m.save(n, e, function () { });
                            },
                            rule: function (e, t, n) {
                                var r = $.trim(e);
                                return r != "" && r.length <= 50 && r.indexOf(" ") == -1;
                            },
                            msg: "素材名必须为1到50个字符，并且素材名不能包含空格"
                        });
                    });
                }
                function C(e) {
                    e.off("click", ".js_star").on("click", ".js_star", function (e) {
                        var t = $(this), n = t.attr("idx"), r = t.attr("action"), i = t.attr("starred");
                        m.star(n, i, function (e) {
                            i == 1  (t.removeClass("star_orange").addClass("star_gray"), t.attr("starred", 0)) : (t.removeClass("star_gray").addClass("star_orange"), t.attr("starred", 1)), t.attr("title", i == 1  "收藏消息" : "取消收藏"), r == "star" && i == 1 && $("#msgListItem" + n).fadeOut();
                        });
                    });
                }
                var k = e("common/wx/richEditor/emotionEditor.js");
                function L(e) {
                    e.off("click", ".js_reply").on("click", ".js_reply", function () {
                        var t = $(this), n = t.data("id"), r = $("#msgListItem" + n).toggleClass("replying");
                        $(".replying", e).each(function () {
                            var e = $(this), t = e.data("id");
                            t != n && e.removeClass("replying");
                        }), r.data("hasClickQuickReply") || (A(r.find(".js_quick_reply_box"), r), r.data("hasClickQuickReply", !0));
                    });
                }
                function A(t, n) {
                    var r = 140, i = $(".js_editor", t), s = new k(i, {
                        wordlimit: r,
                        isHTML: !0
                    }), o = $(".js_reply_OK", t), u = $(".js_reply_pickup", t);
                    u.unbind("click").click(function () {
                        var e = $(this).data("id");
                        $("#msgListItem" + e).removeClass("replying");
                    }), t.keyup(function (e) {
                        if (wx.isHotkey(e, "enter")) return o.click(), !1;
                    });
                    var a = null, f = $(".verifyCode", t), l = e("common/wx/verifycode.js");
                    o.unbind("click").click(function () {
                        var e = $(this), t = e.data("id"), i = e.data("fakeid"), o = s.getContent();
                        if (o.length <= 0 || o.length > r) {
                            y.err("快捷回复的内容必须为1到140个字符");
                            return;
                        }
                        if (a != null && a.getCode().trim().length <= 0) {
                            y.err("请输入验证码"), a.focus();
                            return;
                        }
                        y.suc("回复中...请稍候"), e.btn(!1), m.quickReply({
                            toFakeId: i,
                            content: o,
                            quickReplyId: t,
                            imgcode: a && a.getCode().trim()
                        }, function (t) {
                            s.setContent(""), f.html("").addClass("dn"), a = null, n.addClass("replyed"), e.btn(!0);
                        }, function (t) {
                            e.btn(!0), t && t.base_resp && t.base_resp.ret == -8 && (a = f.html("").removeClass("dn").verifycode().data("verifycode"), a.focus());
                        });
                    });
                }
                var O = function () {
                    $(".avatar img").each(function () {
                        var e = $(this);
                        !e.data("src") || (e.attr("src", e.data("src")), e.removeAttr("data-src"));
                    });
                }, M = function (e) {
                    if (!e.list) return;
                    var t = e.list, n = {}, r = t.length;
                    template.helper("mediaInit", function (e) {
                        return e.id  (n[e.id] = e, "") : "";
                    }), template.helper("timeFormat", function (e) {
                        return b(e.date_time);
                    }), template.helper("id2singleURL", function (e) {
                        return wx.url("/cgi-bin/singlesendpagetofakeid=%s&t=message/send&action=index".sprintf(e.fakeid));
                    }), t.each(function (e) {
                        e.video_url && (e.type = 15), e.type == 5 && (e.type = 10), e.type == 6 && (e.type = 10), e.type == 11 && (e.type = 10), e.type == 16 && (e.type = 15);
                    });
                    var i = $(e.container), s = $(wx.T(l, {
                        token: wx.data.t,
                        list: t,
                        uin: wx.data.uin,
                        action: e.action
                    }).trim());
                    e.preAppend  s.prependTo(i) : s.appendTo(i), E  O() : (E = !0, $(window).on("load", function () {
                        O();
                    })), $(".wxMsg", s).each(function () {
                        var e = $(this), t = e.data("id"), r = n[t];
                        if (!r) return;
                        var i = r.type;
                        i && S[i] && S[i](e, r);
                    }), x(i), T(i), N(i), C(i), L(i);
                };
                return M;
            } catch (_) {
                wx.jslog({
                    src: "message/renderList.js"
                }, _);
            }
        });

/////发消息
define(
    "message/message_cgi.js",
    ["common/wx/Tips.js", "common/wx/Cgi.js"],
    function (e, t, n) {
        try {
            var r = +(new Date);
            "use strict";
            var i = {
                masssend: "/cgi-bin/masssendt=ajax-response",
                massdel: "/cgi-bin/masssendpageaction=delete",
                star: "/cgi-bin/setstarmessaget=ajax-setstarmessage",
                save: "/cgi-bin/savemsgtofilet=ajax-response",
                sendMsg: "/cgi-bin/singlesendt=ajax-response&f=json",
                getNewMsg: "/cgi-bin/singlesendpagetofakeid=%s&f=json&action=sync&lastmsgfromfakeid=%s&lastmsgid=%s&createtime=%s",
                getNewMsgCount: "/cgi-bin/getnewmsgnumf=json&t=ajax-getmsgnum&lastmsgid=",
                pageNav: "/cgi-bin/messagef=json&offset=%s&day=%s&keyword=%s&action=%s&frommsgid=%s&count=%s",
                searchMsgByKeyword: "/cgi-bin/getmessaget=ajax-message&count=10&keyword="
            },
            s = e("common/wx/Tips.js"),
            o = e("common/wx/Cgi.js");
            n.exports = {
                masssend: function (e, t, n) {
                    o.post({
                        url: wx.url(i.masssend),
                        dataType: "html",
                        data: e,
                        error: function (e, t) {
                            s.err("发送失败"), n && n();
                        }
                    },
                    function (e) {
                        var r = $.parseJSON(e);
                        if (r.ret && r.ret == "0") {
                            s.suc("发送成功"), t && t(r);
                            return;
                        }
                        r.ret && r.ret == "64004"  s.err("今天的群发数量已到，无法群发") :
                        r.ret && r.ret == "67008"  s.err("消息中可能含有具备安全风险的链接，请检查") : r.ret == "-6"  s.err("请输入验证码") :
                        r.ret && r.ret == "14002"  s.err("没有“审核通过”的门店。确认有至少一个“审核通过”的门店后可进行卡券投放。") :
                        r.ret && r.ret == "14003"  s.err("投放用户缺少测试权限，请先设置白名单") :
                        s.err("发送失败"),
                        n && n(r);
                    });
                },
                massdel: function (e, t, n) {
                    o.post({
                        url: wx.url(i.massdel),
                        data: {
                            id: e
                        },
                        error: function (e, t) {
                            s.err("删除失败");
                        }
                    },
                    function (e) {
                        if (e && e.base_resp && e.base_resp.ret == 0) {
                            s.suc("删除成功"), t && t(e);
                            return;
                        }
                        s.err("删除失败"), n && n(e);
                    });
                },
                getNewMsg: function (e, t, n, r, s, u) {
                    o.get({
                        url: wx.url(i.getNewMsg.sprintf(e, t, n, r)),
                        mask: !1,
                        handlerTimeout: !0,
                        error: u
                    },
                    function (e) {
                        e && e.base_resp && e.base_resp.ret == 0 && s && s(e.page_info.msg_items.msg_item);
                    });
                },
                save: function (e, t, n, r) {
                    typeof n == "function" && (r = n, n = ""), o.post({
                        mask: !1,
                        url: wx.url(i.save),
                        dataType: "html",
                        data: {
                            msgid: e,
                            filename: t,
                            digest: n
                        },
                        error: function (e, t) {
                            s.err("保存素材失败");
                        }
                    },
                    function (e) {
                        var t = $.parseJSON(e);
                        t && t.ret == "0"  (s.suc("保存素材成功"), typeof r == "function" && r(t)) : s.err("保存素材失败");
                    });
                },
                star: function (e, t, n) {
                    o.post({
                        mask: !1,
                        url: wx.url(i.star),
                        data: {
                            msgid: e,
                            value: t == 1  0 : 1
                        },
                        dataType: "html",
                        error: function () {
                            s.err(t == 1  "取消收藏失败" : "收藏消息失败");
                        }
                    },
                    function (e) {
                        e = $.parseJSON(e),
                        e && e.ret != 0 
                        s.err(t == 1  "取消收藏失败" : "收藏消息失败") :
                        (s.suc(t == 1  "取消收藏成功" : "收藏消息成功"),
                        typeof n == "function" && n(e));
                    });
                },
                sendMsg: function (e, t, n) {
                    o.post({
                        url: wx.url(i.sendMsg),
                        data: e,
                        error: function () {
                            s.err("发送失败"),
                            n && n();
                        }
                    },
                    function (e) {
                        if (!e || !e.base_resp) {
                            s.err("发送失败");
                            return;
                        }
                        var r = e.base_resp.ret;
                        if (r == 0) {
                            s.suc("回复成功"),
                            typeof t == "function" && t(e);
                            return;
                        }
                        r == 10703  s.err("对方关闭了接收消息") :
                        r == 10700  s.err("不能发送，对方不是你的粉丝") :
                        r == 10701  s.err("该用户已被加入黑名单，无法向其发送消息") :
                        r == 62752  s.err("消息中可能含有具备安全风险的链接，请检查") :
                        r == 10704  s.err("该素材已被删除") :
                        r == 10705  s.err("该素材已被删除") :
                        r == 10706  s.err("由于该用户48小时未与你互动，你不能再主动发消息给他。直到用户下次主动发消息给你才可以对其进行回复。") :
                        r == -8  s.err("请输入验证码") :
                        s.err("发送失败"), n && n(e);
                    });
                },
                getNewMsgCount: function (e, t, n) {
                    o.post({
                        mask: !1,
                        dataType: "html",
                        handlerTimeout: !0,
                        url: wx.url(i.getNewMsgCount + e),
                        error: n
                    }, function (e) {
                        e = $.parseJSON(e), typeof t == "function" && t(e);
                    });
                },
                quickReply: function (e, t, n) {
                    this.sendMsg({
                        mask: !1,
                        tofakeid: e.toFakeId,
                        imgcode: e.imgcode,
                        type: 1,
                        content: e.content,
                        quickreplyid: e.quickReplyId
                    }, t, n);
                },
                pageNav: function (e, t, n) {
                    var r = i.pageNav.sprintf((e.page - 1) * e.count, e.day || "", e.keyword || "", e.action || "", e.frommsgid || "", e.count || "");
                    o.post({
                        dataType: "json",
                        url: wx.url(r),
                        mask: !1,
                        data: {},
                        error: n
                    },
                    function (e) {
                        e && e.base_resp && e.base_resp.ret == 0 && typeof t == "function" && t(e.msg_items.msg_item);
                    });
                },
                searchMsgByKeyword: function (e, t, n) {
                    o.post({
                        dataType: "html",
                        mask: !1,
                        url: wx.url(url.searchMsgByKeyword + e),
                        error: function () {
                            s.err("系统发生异常，请刷新页面重试"), n && n({});
                        }
                    },
                    function (e) {
                        typeof t == "function" && t($.parseJSON(e));
                    });
                }
            };
        } catch (u) {
            wx.jslog({
                src: "message/message_cgi.js"
            }, u);
        }
    });

define(
    "common/wx/richEditor/msgSender.js",
    ["common/wx/popup.js",
        "widget/msg_sender.css",
        "common/qq/jquery.plugin/tab.js",
        "common/wx/richEditor/emotionEditor.js",
        "media/media_dialog.js",
        "common/wx/media/factory.js",
        "common/qq/Class.js",
        "common/wx/Tips.js",
        "common/wx/media/audio.js",
        "common/wx/media/img.js",
        "common/wx/media/video.js",
        "common/wx/media/cardmsg.js",
        "common/wx/tooltip.js",
        "common/wx/media/appmsg.js"],
    function (e, t, n) {
        try {
            var r = +(new Date);
            "use strict",
            e("common/wx/popup.js"),
            e("widget/msg_sender.css");
            var i = e("common/qq/jquery.plugin/tab.js"),
                s = e("common/wx/richEditor/emotionEditor.js"),
                o = e("media/media_dialog.js"),
                u = e("common/wx/media/factory.js"),
                a = e("common/qq/Class.js"),
                f = e("common/wx/Tips.js"),
                l = e("common/wx/media/audio.js"),
                c = e("common/wx/media/img.js"),
                h = e("common/wx/media/video.js"),
                p = e("common/wx/media/cardmsg.js"),
                d = e("common/wx/tooltip.js"),
                v = e("common/wx/media/appmsg.js"),
                m = 1,
                g = {},
                y = [{
                    text: "文字",
                    acl: "can_text_msg",
                    className: "tab_text",
                    selector: "js_textArea",
                    innerClassName: "no_extra",
                    type: 1
                }, {
                    text: "图片",
                    acl: "can_image_msg",
                    className: "tab_img",
                    selector: "js_imgArea",
                    type: 2
                }, {
                    text: "语音",
                    acl: "can_voice_msg",
                    className: "tab_audio",
                    selector: "js_audioArea",
                    type: 3
                }, {
                    text: "视频",
                    acl: "can_video_msg",
                    className: "tab_video",
                    selector: "js_videoArea",
                    type: 15
                }, {
                    text: "图文消息",
                    acl: "can_app_msg",
                    className: "tab_appmsg",
                    selector: "js_appmsgArea",
                    type: 10
                }, {
                    text: "商品消息",
                    acl: "can_commodity_app_msg",
                    className: "tab_commondity_appmsg",
                    selector: "js_commondityAppmsgArea",
                    type: 11
                }, {
                    text: "卡券",
                    acl: "can_card_msg",
                    className: "tab_cardmsg",
                    selector: "js_cardmsgArea",
                    type: 16
                }];
            function b(e, t) {
                var n = [];
                for (var r = 0; r < e.length; ++r) {
                    var i = e[r];
                    !!t && !!t[i.acl] && n.push(i);
                }
                return n;
            }
            function w(e) {
                var t = {}, n = e.slice();
                n.push({
                    acl: "can_video_msg",
                    className: "tab_video",
                    selector: "js_videoArea",
                    text: "视频",
                    type: 4,
                    index: 3
                });
                for (var r = 0; r < n.length; ++r) {
                    var i = n[r];
                    i.index = i.index || r, t[i.type] = i;
                }
                return t;
            }
            var E = u.itemRender,
                S = a.declare({
                    select: function () {
                        this.msgSender.type = this.type;
                    },
                    fillData: function (e) { },
                    getData: function () { },
                    click: function () {
                        this.msgSender.type = this.type;
                    }
                }),
            x = S.Inherit({
                init: function (e) {
                    this.msgSender = e, this.type = 1, this.info = e.infos[this.type], this.wordlimit = e.opt.wordlimit, this.index = this.info && this.info.index;
                },
                fillData: function (e) {
                    var t = this.msgSender;
                    t.type = this.type, t.select(this.index), t.emotionEditor.setContent(e.content);
                },
                getData: function () {
                    var e = this.msgSender.emotionEditor.getContent();
                    return {
                        type: this.type,
                        content: e
                    };
                },
                clear: function () {
                    return this.fillData({
                        content: ""
                    });
                },
                isValidate: function (e) {
                    var t = e && e.type == 1 && e.content != "" && e.content.length <= this.wordlimit;
                    return t || f.err("文字必须为1到%s个字".sprintf(this.wordlimit)), t;
                }
            }),
            T = S.Inherit({
                init: function (e, t) {
                    this.type = t, this.msgSender = e, this.info = e.infos[t], this.index = this.info && this.info.index;
                },
                click: function () {
                    var e = null, t = this;
                    return t.type == 10 || t.type == 11 || t.type == 15  e = o.getAppmsg : e = o.getFile, e({
                        type: t.type,
                        begin: 0,
                        count: 10,
                        onSelect: function (e, n) {
                            var r = t.msgSender;
                            t.msgSender.type = e, r.select(t.index);
                            var i = "msgSender_media_%s_%s".sprintf(r.gid, e);
                            $("." + t.info.selector).html('<div id="%s"></div>'.sprintf(i)), E[e] && E[e]("#" + i, n);
                        }
                    }), !1;
                },
                fillData: function (e) {
                    var t = this.msgSender, n = this.type, r = "msgSender_media_%s_%s".sprintf(t.gid, n);
                    $("." + this.info.selector).html('<div id="%s"></div>'.sprintf(r)), t.select(this.index), this.msgSender.type = n, E[n] && E[n]("#" + r, e);
                },
                clear: function () {
                    var e = this.type;
                    return $("." + this.info.selector).html("");
                },
                getData: function (e) {
                    var t = this.type, n = "msgSender_media_%s_%s".sprintf(this.msgSender.gid, t), r = $("#" + n).data("opt");
                    if (!r) return !1;
                    if (!e) return t == 10 || t == 11  {
                        type: t,
                        app_id: r.data.app_id
                    } : t == 15  {
                        type: t,
                        app_id: r.app_id
                    } : {
                        type: t,
                        file_id: r.file_id
                    };
                    r.type = t;
                    var i = r.data || {};
                    return $.extend(r, i);
                },
                isValidate: function (e) {
                    var t = !!e;
                    if (!!e) {
                        var n = e.type;
                        n == 10 || n == 11 || n == 15  t = !!e.type && !!e.app_id : t = !!e.type && !!e.file_id;
                    }
                    return t || f.err("请选择素材"), t;
                }
            }),
            N = {
                wordlimit: 600
            },
            C = a.declare({
                init: function (e, t) {
                    var n = this, r = 0;
                    e = $(e).show(), n.gid = m++, n.opt = $.extend(!0, {}, N, t);
                    var i = y, s = t && t.acl || {};
                    i = b(i, s), n.infos = w(i), n.op = {
                        "1": new x(n),
                        "2": new T(n, 2),
                        "3": new T(n, 3),
                        "4": new T(n, 4),
                        "7": new T(n, 15),
                        "10": new T(n, 10),
                        "11": new T(n, 11),
                        "15": new T(n, 15),
                        "16": new p(n)
                    }, n.tab = e.tab({
                        index: r,
                        tabs: i,
                        select: function (e, t, n, r) { },
                        click: function (e, t, r, i) {
                            return n.op[i] && n.op[i].click();
                        }
                    }), n._init(e);
                    var o = t.data;
                    o  n.setData(o) : n.type = 1;
                },
                setData: function (e) {
                    e = e || {
                        type: 1
                    };
                    var t = this, n = null, r = e.type;
                    t.type = r || 1, !(n = t.op[r]) || n.fillData(e);
                },
                _init: function (e) {
                    this.dom = e, this.emotionEditor = new s($(".js_textArea", e), {
                        wordlimit: this.opt.wordlimit,
                        linebreak: !0
                    }), new d({
                        dom: this.dom.find(".tab_nav"),
                        position: {
                            x: -2,
                            y: 1
                        }
                    });
                },
                getData: function (e) {
                    if (this.type) {
                        var t = this.op[this.type].getData(e);
                        return {
                            error: !this.isValidate(),
                            data: t
                        };
                    }
                    return {
                        error: !0
                    };
                },
                getArea: function (e) {
                    return this.dom.find("." + this.infos[e].selector);
                },
                getTabs: function () {
                    return this.tab.getTabs();
                },
                isValidate: function () {
                    var e = this.type && this.op[this.type].getData();
                    return this.type && this.op[this.type].isValidate(e);
                },
                clear: function () {
                    return this.type && this.op[this.type].clear();
                },
                select: function (e) {
                    return this.tab.select(e);
                }
            });
            return C;
        } catch (k) {
            wx.jslog({
                src: "common/wx/richEditor/msgSender.js"
            }, k);
        }
    });

define(
    "common/qq/emoji.js",
    ["widget/emoji.css"],
    function (e, t, n) {
        try {
            var r = +(new Date);
            e("widget/emoji.css");
            var i = {
                "☀": "2600",
                "☁": "2601",
                "☔": "2614",
                "⛄": "26c4",
                "⚡": "26a1",
                "🌀": "1f300",
                "🌁": "1f301",
                "🌂": "1f302",
                "🌃": "1f303",
                "🌄": "1f304",
                "🌅": "1f305",
                "🌆": "1f306",
                "🌇": "1f307",
                "🌈": "1f308",
                "❄": "2744",
                "⛅": "26c5",
                "🌉": "1f309",
                "🌊": "1f30a",
                "🌋": "1f30b",
                "🌌": "1f30c",
                "🌏": "1f30f",
                "🌑": "1f311",
                "🌔": "1f314",
                "🌓": "1f313",
                "🌙": "1f319",
                "🌕": "1f315",
                "🌛": "1f31b",
                "🌟": "1f31f",
                "🌠": "1f320",
                "🕐": "1f550",
                "🕑": "1f551",
                "🕒": "1f552",
                "🕓": "1f553",
                "🕔": "1f554",
                "🕕": "1f555",
                "🕖": "1f556",
                "🕗": "1f557",
                "🕘": "1f558",
                "🕙": "1f559",
                "🕚": "1f55a",
                "🕛": "1f55b",
                "⌚": "231a",
                "⌛": "231b",
                "⏰": "23f0",
                "⏳": "23f3",
                "♈": "2648",
                "♉": "2649",
                "♊": "264a",
                "♋": "264b",
                "♌": "264c",
                "♍": "264d",
                "♎": "264e",
                "♏": "264f",
                "♐": "2650",
                "♑": "2651",
                "♒": "2652",
                "♓": "2653",
                "⛎": "26ce",
                "🍀": "1f340",
                "🌷": "1f337",
                "🌱": "1f331",
                "🍁": "1f341",
                "🌸": "1f338",
                "🌹": "1f339",
                "🍂": "1f342",
                "🍃": "1f343",
                "🌺": "1f33a",
                "🌻": "1f33b",
                "🌴": "1f334",
                "🌵": "1f335",
                "🌾": "1f33e",
                "🌽": "1f33d",
                "🍄": "1f344",
                "🌰": "1f330",
                "🌼": "1f33c",
                "🌿": "1f33f",
                "🍒": "1f352",
                "🍌": "1f34c",
                "🍎": "1f34e",
                "🍊": "1f34a",
                "🍓": "1f353",
                "🍉": "1f349",
                "🍅": "1f345",
                "🍆": "1f346",
                "🍈": "1f348",
                "🍍": "1f34d",
                "🍇": "1f347",
                "🍑": "1f351",
                "🍏": "1f34f",
                "👀": "1f440",
                "👂": "1f442",
                "👃": "1f443",
                "👄": "1f444",
                "👅": "1f445",
                "💄": "1f484",
                "💅": "1f485",
                "💆": "1f486",
                "💇": "1f487",
                "💈": "1f488",
                "👤": "1f464",
                "👦": "1f466",
                "👧": "1f467",
                "👨": "1f468",
                "👩": "1f469",
                "👪": "1f46a",
                "👫": "1f46b",
                "👮": "1f46e",
                "👯": "1f46f",
                "👰": "1f470",
                "👱": "1f471",
                "👲": "1f472",
                "👳": "1f473",
                "👴": "1f474",
                "👵": "1f475",
                "👶": "1f476",
                "👷": "1f477",
                "👸": "1f478",
                "👹": "1f479",
                "👺": "1f47a",
                "👻": "1f47b",
                "👼": "1f47c",
                "👽": "1f47d",
                "👾": "1f47e",
                "👿": "1f47f",
                "💀": "1f480",
                "💁": "1f481",
                "💂": "1f482",
                "💃": "1f483",
                "🐌": "1f40c",
                "🐍": "1f40d",
                "🐎": "1f40e",
                "🐔": "1f414",
                "🐗": "1f417",
                "🐫": "1f42b",
                "🐘": "1f418",
                "🐨": "1f428",
                "🐒": "1f412",
                "🐑": "1f411",
                "🐙": "1f419",
                "🐚": "1f41a",
                "🐛": "1f41b",
                "🐜": "1f41c",
                "🐝": "1f41d",
                "🐞": "1f41e",
                "🐠": "1f420",
                "🐡": "1f421",
                "🐢": "1f422",
                "🐤": "1f424",
                "🐥": "1f425",
                "🐦": "1f426",
                "🐣": "1f423",
                "🐧": "1f427",
                "🐩": "1f429",
                "🐟": "1f41f",
                "🐬": "1f42c",
                "🐭": "1f42d",
                "🐯": "1f42f",
                "🐱": "1f431",
                "🐳": "1f433",
                "🐴": "1f434",
                "🐵": "1f435",
                "🐶": "1f436",
                "🐷": "1f437",
                "🐻": "1f43b",
                "🐹": "1f439",
                "🐺": "1f43a",
                "🐮": "1f42e",
                "🐰": "1f430",
                "🐸": "1f438",
                "🐾": "1f43e",
                "🐲": "1f432",
                "🐼": "1f43c",
                "🐽": "1f43d",
                "😠": "1f620",
                "😩": "1f629",
                "😲": "1f632",
                "😞": "1f61e",
                "😵": "1f635",
                "😰": "1f630",
                "😒": "1f612",
                "😍": "1f60d",
                "😤": "1f624",
                "😜": "1f61c",
                "😝": "1f61d",
                "😋": "1f60b",
                "😘": "1f618",
                "😚": "1f61a",
                "😷": "1f637",
                "😳": "1f633",
                "😃": "1f603",
                "😅": "1f605",
                "😆": "1f606",
                "😁": "1f601",
                "😂": "1f602",
                "😊": "1f60a",
                "☺": "263a",
                "😄": "1f604",
                "😢": "1f622",
                "😭": "1f62d",
                "😨": "1f628",
                "😣": "1f623",
                "😡": "1f621",
                "😌": "1f60c",
                "😖": "1f616",
                "😔": "1f614",
                "😱": "1f631",
                "😪": "1f62a",
                "😏": "1f60f",
                "😓": "1f613",
                "😥": "1f625",
                "😫": "1f62b",
                "😉": "1f609",
                "😺": "1f63a",
                "😸": "1f638",
                "😹": "1f639",
                "😽": "1f63d",
                "😻": "1f63b",
                "😿": "1f63f",
                "😾": "1f63e",
                "😼": "1f63c",
                "🙀": "1f640",
                "🙅": "1f645",
                "🙆": "1f646",
                "🙇": "1f647",
                "🙈": "1f648",
                "🙊": "1f64a",
                "🙉": "1f649",
                "🙋": "1f64b",
                "🙌": "1f64c",
                "🙍": "1f64d",
                "🙎": "1f64e",
                "🙏": "1f64f",
                "🏠": "1f3e0",
                "🏡": "1f3e1",
                "🏢": "1f3e2",
                "🏣": "1f3e3",
                "🏥": "1f3e5",
                "🏦": "1f3e6",
                "🏧": "1f3e7",
                "🏨": "1f3e8",
                "🏩": "1f3e9",
                "🏪": "1f3ea",
                "🏫": "1f3eb",
                "⛪": "26ea",
                "⛲": "26f2",
                "🏬": "1f3ec",
                "🏯": "1f3ef",
                "🏰": "1f3f0",
                "🏭": "1f3ed",
                "⚓": "2693",
                "🏮": "1f3ee",
                "🗻": "1f5fb",
                "🗼": "1f5fc",
                "🗽": "1f5fd",
                "🗾": "1f5fe",
                "🗿": "1f5ff",
                "👞": "1f45e",
                "👟": "1f45f",
                "👠": "1f460",
                "👡": "1f461",
                "👢": "1f462",
                "👣": "1f463",
                "👓": "1f453",
                "👕": "1f455",
                "👖": "1f456",
                "👑": "1f451",
                "👔": "1f454",
                "👒": "1f452",
                "👗": "1f457",
                "👘": "1f458",
                "👙": "1f459",
                "👚": "1f45a",
                "👛": "1f45b",
                "👜": "1f45c",
                "👝": "1f45d",
                "💰": "1f4b0",
                "💱": "1f4b1",
                "💹": "1f4b9",
                "💲": "1f4b2",
                "💳": "1f4b3",
                "💴": "1f4b4",
                "💵": "1f4b5",
                "💸": "1f4b8",
                "🇨🇳": "1f1e81f1f3",
                "🇩🇪": "1f1e91f1ea",
                "🇪🇸": "1f1ea1f1f8",
                "🇫🇷": "1f1eb1f1f7",
                "🇬🇧": "1f1ec1f1e7",
                "🇮🇹": "1f1ee1f1f9",
                "🇯🇵": "1f1ef1f1f5",
                "🇰🇷": "1f1f01f1f7",
                "🇷🇺": "1f1f71f1fa",
                "🇺🇸": "1f1fa1f1f8",
                "🔥": "1f525",
                "🔦": "1f526",
                "🔧": "1f527",
                "🔨": "1f528",
                "🔩": "1f529",
                "🔪": "1f52a",
                "🔫": "1f52b",
                "🔮": "1f52e",
                "🔯": "1f52f",
                "🔰": "1f530",
                "🔱": "1f531",
                "💉": "1f489",
                "💊": "1f48a",
                "🅰": "1f170",
                "🅱": "1f171",
                "🆎": "1f18e",
                "🅾": "1f17e",
                "🎀": "1f380",
                "🎁": "1f381",
                "🎂": "1f382",
                "🎄": "1f384",
                "🎅": "1f385",
                "🎌": "1f38c",
                "🎆": "1f386",
                "🎈": "1f388",
                "🎉": "1f389",
                "🎍": "1f38d",
                "🎎": "1f38e",
                "🎓": "1f393",
                "🎒": "1f392",
                "🎏": "1f38f",
                "🎇": "1f387",
                "🎐": "1f390",
                "🎃": "1f383",
                "🎊": "1f38a",
                "🎋": "1f38b",
                "🎑": "1f391",
                "📟": "1f4df",
                "☎": "260e",
                "📞": "1f4de",
                "📱": "1f4f1",
                "📲": "1f4f2",
                "📝": "1f4dd",
                "📠": "1f4e0",
                "✉": "2709",
                "📨": "1f4e8",
                "📩": "1f4e9",
                "📪": "1f4ea",
                "📫": "1f4eb",
                "📮": "1f4ee",
                "📰": "1f4f0",
                "📢": "1f4e2",
                "📣": "1f4e3",
                "📡": "1f4e1",
                "📤": "1f4e4",
                "📥": "1f4e5",
                "📦": "1f4e6",
                "📧": "1f4e7",
                "🔠": "1f520",
                "🔡": "1f521",
                "🔢": "1f522",
                "🔣": "1f523",
                "🔤": "1f524",
                "✒": "2712",
                "💺": "1f4ba",
                "💻": "1f4bb",
                "✏": "270f",
                "📎": "1f4ce",
                "💼": "1f4bc",
                "💽": "1f4bd",
                "💾": "1f4be",
                "💿": "1f4bf",
                "📀": "1f4c0",
                "✂": "2702",
                "📍": "1f4cd",
                "📃": "1f4c3",
                "📄": "1f4c4",
                "📅": "1f4c5",
                "📁": "1f4c1",
                "📂": "1f4c2",
                "📓": "1f4d3",
                "📖": "1f4d6",
                "📔": "1f4d4",
                "📕": "1f4d5",
                "📗": "1f4d7",
                "📘": "1f4d8",
                "📙": "1f4d9",
                "📚": "1f4da",
                "📛": "1f4db",
                "📜": "1f4dc",
                "📋": "1f4cb",
                "📆": "1f4c6",
                "📊": "1f4ca",
                "📈": "1f4c8",
                "📉": "1f4c9",
                "📇": "1f4c7",
                "📌": "1f4cc",
                "📒": "1f4d2",
                "📏": "1f4cf",
                "📐": "1f4d0",
                "📑": "1f4d1",
                "🎽": "1f3bd",
                "⚾": "26be",
                "⛳": "26f3",
                "🎾": "1f3be",
                "⚽": "26bd",
                "🎿": "1f3bf",
                "🏀": "1f3c0",
                "🏁": "1f3c1",
                "🏂": "1f3c2",
                "🏃": "1f3c3",
                "🏄": "1f3c4",
                "🏆": "1f3c6",
                "🏈": "1f3c8",
                "🏊": "1f3ca",
                "🚃": "1f683",
                "🚇": "1f687",
                "Ⓜ": "24c2",
                "🚄": "1f684",
                "🚅": "1f685",
                "🚗": "1f697",
                "🚙": "1f699",
                "🚌": "1f68c",
                "🚏": "1f68f",
                "🚢": "1f6a2",
                "✈": "2708",
                "⛵": "26f5",
                "🚉": "1f689",
                "🚀": "1f680",
                "🚤": "1f6a4",
                "🚕": "1f695",
                "🚚": "1f69a",
                "🚒": "1f692",
                "🚑": "1f691",
                "🚓": "1f693",
                "⛽": "26fd",
                "🅿": "1f17f",
                "🚥": "1f6a5",
                "🚧": "1f6a7",
                "🚨": "1f6a8",
                "♨": "2668",
                "⛺": "26fa",
                "🎠": "1f3a0",
                "🎡": "1f3a1",
                "🎢": "1f3a2",
                "🎣": "1f3a3",
                "🎤": "1f3a4",
                "🎥": "1f3a5",
                "🎦": "1f3a6",
                "🎧": "1f3a7",
                "🎨": "1f3a8",
                "🎩": "1f3a9",
                "🎪": "1f3aa",
                "🎫": "1f3ab",
                "🎬": "1f3ac",
                "🎭": "1f3ad",
                "🎮": "1f3ae",
                "🀄": "1f004",
                "🎯": "1f3af",
                "🎰": "1f3b0",
                "🎱": "1f3b1",
                "🎲": "1f3b2",
                "🎳": "1f3b3",
                "🎴": "1f3b4",
                "🃏": "1f0cf",
                "🎵": "1f3b5",
                "🎶": "1f3b6",
                "🎷": "1f3b7",
                "🎸": "1f3b8",
                "🎹": "1f3b9",
                "🎺": "1f3ba",
                "🎻": "1f3bb",
                "🎼": "1f3bc",
                "〽": "303d",
                "📷": "1f4f7",
                "📹": "1f4f9",
                "📺": "1f4fa",
                "📻": "1f4fb",
                "📼": "1f4fc",
                "💋": "1f48b",
                "💌": "1f48c",
                "💍": "1f48d",
                "💎": "1f48e",
                "💏": "1f48f",
                "💐": "1f490",
                "💑": "1f491",
                "💒": "1f492",
                "🔞": "1f51e",
                "©": "a9",
                "®": "ae",
                "™": "2122",
                "ℹ": "2139",
                "#⃣": "2320e3",
                "1⃣": "3120e3",
                "2⃣": "3220e3",
                "3⃣": "3320e3",
                "4⃣": "3420e3",
                "5⃣": "3520e3",
                "6⃣": "3620e3",
                "7⃣": "3720e3",
                "8⃣": "3820e3",
                "9⃣": "3920e3",
                "0⃣": "3020e3",
                "🔟": "1f51f",
                "📶": "1f4f6",
                "📳": "1f4f3",
                "📴": "1f4f4",
                "🍔": "1f354",
                "🍙": "1f359",
                "🍰": "1f370",
                "🍜": "1f35c",
                "🍞": "1f35e",
                "🍳": "1f373",
                "🍦": "1f366",
                "🍟": "1f35f",
                "🍡": "1f361",
                "🍘": "1f358",
                "🍚": "1f35a",
                "🍝": "1f35d",
                "🍛": "1f35b",
                "🍢": "1f362",
                "🍣": "1f363",
                "🍱": "1f371",
                "🍲": "1f372",
                "🍧": "1f367",
                "🍖": "1f356",
                "🍥": "1f365",
                "🍠": "1f360",
                "🍕": "1f355",
                "🍗": "1f357",
                "🍨": "1f368",
                "🍩": "1f369",
                "🍪": "1f36a",
                "🍫": "1f36b",
                "🍬": "1f36c",
                "🍭": "1f36d",
                "🍮": "1f36e",
                "🍯": "1f36f",
                "🍤": "1f364",
                "🍴": "1f374",
                "☕": "2615",
                "🍸": "1f378",
                "🍺": "1f37a",
                "🍵": "1f375",
                "🍶": "1f376",
                "🍷": "1f377",
                "🍻": "1f37b",
                "🍹": "1f379",
                "↗": "2197",
                "↘": "2198",
                "↖": "2196",
                "↙": "2199",
                "⤴": "2934",
                "⤵": "2935",
                "↔": "2194",
                "↕": "2195",
                "⬆": "2b06",
                "⬇": "2b07",
                "➡": "27a1",
                "⬅": "2b05",
                "▶": "25b6",
                "◀": "25c0",
                "⏩": "23e9",
                "⏪": "23ea",
                "⏫": "23eb",
                "⏬": "23ec",
                "🔺": "1f53a",
                "🔻": "1f53b",
                "🔼": "1f53c",
                "🔽": "1f53d",
                "⭕": "2b55",
                "❌": "274c",
                "❎": "274e",
                "❗": "2757",
                "⁉": "2049",
                "‼": "203c",
                "❓": "2753",
                "❔": "2754",
                "❕": "2755",
                "〰": "3030",
                "➰": "27b0",
                "➿": "27bf",
                "❤": "2764",
                "💓": "1f493",
                "💔": "1f494",
                "💕": "1f495",
                "💖": "1f496",
                "💗": "1f497",
                "💘": "1f498",
                "💙": "1f499",
                "💚": "1f49a",
                "💛": "1f49b",
                "💜": "1f49c",
                "💝": "1f49d",
                "💞": "1f49e",
                "💟": "1f49f",
                "♥": "2665",
                "♠": "2660",
                "♦": "2666",
                "♣": "2663",
                "🚬": "1f6ac",
                "🚭": "1f6ad",
                "♿": "267f",
                "🚩": "1f6a9",
                "⚠": "26a0",
                "⛔": "26d4",
                "♻": "267b",
                "🚲": "1f6b2",
                "🚶": "1f6b6",
                "🚹": "1f6b9",
                "🚺": "1f6ba",
                "🛀": "1f6c0",
                "🚻": "1f6bb",
                "🚽": "1f6bd",
                "🚾": "1f6be",
                "🚼": "1f6bc",
                "🚪": "1f6aa",
                "🚫": "1f6ab",
                "✔": "2714",
                "🆑": "1f191",
                "🆒": "1f192",
                "🆓": "1f193",
                "🆔": "1f194",
                "🆕": "1f195",
                "🆖": "1f196",
                "🆗": "1f197",
                "🆘": "1f198",
                "🆙": "1f199",
                "🆚": "1f19a",
                "🈁": "1f201",
                "🈂": "1f202",
                "🈲": "1f232",
                "🈳": "1f233",
                "🈴": "1f234",
                "🈵": "1f235",
                "🈶": "1f236",
                "🈚": "1f21a",
                "🈷": "1f237",
                "🈸": "1f238",
                "🈹": "1f239",
                "🈯": "1f22f",
                "🈺": "1f23a",
                "㊙": "3299",
                "㊗": "3297",
                "🉐": "1f250",
                "🉑": "1f251",
                "➕": "2795",
                "➖": "2796",
                "✖": "2716",
                "➗": "2797",
                "💠": "1f4a0",
                "💡": "1f4a1",
                "💢": "1f4a2",
                "💣": "1f4a3",
                "💤": "1f4a4",
                "💥": "1f4a5",
                "💦": "1f4a6",
                "💧": "1f4a7",
                "💨": "1f4a8",
                "💩": "1f4a9",
                "💪": "1f4aa",
                "💫": "1f4ab",
                "💬": "1f4ac",
                "✨": "2728",
                "✴": "2734",
                "✳": "2733",
                "⚪": "26aa",
                "⚫": "26ab",
                "🔴": "1f534",
                "🔵": "1f535",
                "🔲": "1f532",
                "🔳": "1f533",
                "⭐": "2b50",
                "⬜": "2b1c",
                "⬛": "2b1b",
                "▫": "25ab",
                "▪": "25aa",
                "◽": "25fd",
                "◾": "25fe",
                "◻": "25fb",
                "◼": "25fc",
                "🔶": "1f536",
                "🔷": "1f537",
                "🔸": "1f538",
                "🔹": "1f539",
                "❇": "2747",
                "💮": "1f4ae",
                "💯": "1f4af",
                "↩": "21a9",
                "↪": "21aa",
                "🔃": "1f503",
                "🔊": "1f50a",
                "🔋": "1f50b",
                "🔌": "1f50c",
                "🔍": "1f50d",
                "🔎": "1f50e",
                "🔒": "1f512",
                "🔓": "1f513",
                "🔏": "1f50f",
                "🔐": "1f510",
                "🔑": "1f511",
                "🔔": "1f514",
                "☑": "2611",
                "🔘": "1f518",
                "🔖": "1f516",
                "🔗": "1f517",
                "🔙": "1f519",
                "🔚": "1f51a",
                "🔛": "1f51b",
                "🔜": "1f51c",
                "🔝": "1f51d",
                " ": "2003",
                " ": "2002",
                " ": "2005",
                "✅": "2705",
                "✊": "270a",
                "✋": "270b",
                "✌": "270c",
                "👊": "1f44a",
                "👍": "1f44d",
                "☝": "261d",
                "👆": "1f446",
                "👇": "1f447",
                "👈": "1f448",
                "👉": "1f449",
                "👋": "1f44b",
                "👏": "1f44f",
                "👌": "1f44c",
                "👎": "1f44e",
                "👐": "1f450",
                "": "2600",
                "": "2601",
                "": "2614",
                "": "26c4",
                "": "26a1",
                "": "1f300",
                "[霧]": "1f301",
                "": "1f302",
                "": "1f30c",
                "": "1f304",
                "": "1f305",
                "": "1f306",
                "": "1f307",
                "": "1f308",
                "[雪結晶]": "2744",
                "": "26c5",
                "": "1f30a",
                "[火山]": "1f30b",
                "[地球]": "1f30f",
                "●": "1f311",
                "": "1f31b",
                "○": "1f315",
                "": "1f31f",
                "☆彡": "1f320",
                "": "1f550",
                "": "1f551",
                "": "1f552",
                "": "1f553",
                "": "1f554",
                "": "1f555",
                "": "1f556",
                "": "1f557",
                "": "1f558",
                "": "23f0",
                "": "1f55a",
                "": "1f55b",
                "[腕時計]": "231a",
                "[砂時計]": "23f3",
                "": "2648",
                "": "2649",
                "": "264a",
                "": "264b",
                "": "264c",
                "": "264d",
                "": "264e",
                "": "264f",
                "": "2650",
                "": "2651",
                "": "2652",
                "": "2653",
                "": "26ce",
                "": "1f33f",
                "": "1f337",
                "": "1f341",
                "": "1f338",
                "": "1f339",
                "": "1f342",
                "": "1f343",
                "": "1f33a",
                "": "1f33c",
                "": "1f334",
                "": "1f335",
                "": "1f33e",
                "[とうもろこし]": "1f33d",
                "[キノコ]": "1f344",
                "[栗]": "1f330",
                "[さくらんぼ]": "1f352",
                "[バナナ]": "1f34c",
                "": "1f34f",
                "": "1f34a",
                "": "1f353",
                "": "1f349",
                "": "1f345",
                "": "1f346",
                "[メロン]": "1f348",
                "[パイナップル]": "1f34d",
                "[ブドウ]": "1f347",
                "[モモ]": "1f351",
                "": "1f440",
                "": "1f442",
                "": "1f443",
                "": "1f444",
                "": "1f61d",
                "": "1f484",
                "": "1f485",
                "": "1f486",
                "": "1f487",
                "": "1f488",
                "〓": "2005",
                "": "1f466",
                "": "1f467",
                "": "1f468",
                "": "1f469",
                "[家族]": "1f46a",
                "": "1f46b",
                "": "1f46e",
                "": "1f46f",
                "[花嫁]": "1f470",
                "": "1f471",
                "": "1f472",
                "": "1f473",
                "": "1f474",
                "": "1f475",
                "": "1f476",
                "": "1f477",
                "": "1f478",
                "[なまはげ]": "1f479",
                "[天狗]": "1f47a",
                "": "1f47b",
                "": "1f47c",
                "": "1f47d",
                "": "1f47e",
                "": "1f47f",
                "": "1f480",
                "": "1f481",
                "": "1f482",
                "": "1f483",
                "[カタツムリ]": "1f40c",
                "": "1f40d",
                "": "1f40e",
                "": "1f414",
                "": "1f417",
                "": "1f42b",
                "": "1f418",
                "": "1f428",
                "": "1f412",
                "": "1f411",
                "": "1f419",
                "": "1f41a",
                "": "1f41b",
                "[アリ]": "1f41c",
                "[ミツバチ]": "1f41d",
                "[てんとう虫]": "1f41e",
                "": "1f420",
                "": "1f3a3",
                "[カメ]": "1f422",
                "": "1f423",
                "": "1f426",
                "": "1f427",
                "": "1f436",
                "": "1f42c",
                "": "1f42d",
                "": "1f42f",
                "": "1f431",
                "": "1f433",
                "": "1f434",
                "": "1f435",
                "": "1f43d",
                "": "1f43b",
                "": "1f439",
                "": "1f43a",
                "": "1f42e",
                "": "1f430",
                "": "1f438",
                "": "1f463",
                "[辰]": "1f432",
                "[パンダ]": "1f43c",
                "": "1f620",
                "": "1f64d",
                "": "1f632",
                "": "1f61e",
                "": "1f62b",
                "": "1f630",
                "": "1f612",
                "": "1f63b",
                "": "1f63c",
                "": "1f61c",
                "": "1f60a",
                "": "1f63d",
                "": "1f61a",
                "": "1f637",
                "": "1f633",
                "": "1f63a",
                "": "1f605",
                "": "1f60c",
                "": "1f639",
                "": "263a",
                "": "1f604",
                "": "1f63f",
                "": "1f62d",
                "": "1f628",
                "": "1f64e",
                "": "1f4ab",
                "": "1f631",
                "": "1f62a",
                "": "1f60f",
                "": "1f613",
                "": "1f625",
                "": "1f609",
                "": "1f645",
                "": "1f646",
                "": "1f647",
                "(/_＼)": "1f648",
                "(・×・)": "1f64a",
                "|(・×・)|": "1f649",
                "": "270b",
                "": "1f64c",
                "": "1f64f",
                "": "1f3e1",
                "": "1f3e2",
                "": "1f3e3",
                "": "1f3e5",
                "": "1f3e6",
                "": "1f3e7",
                "": "1f3e8",
                "": "1f3e9",
                "": "1f3ea",
                "": "1f3eb",
                "": "26ea",
                "": "26f2",
                "": "1f3ec",
                "": "1f3ef",
                "": "1f3f0",
                "": "1f3ed",
                "": "1f6a2",
                "": "1f376",
                "": "1f5fb",
                "": "1f5fc",
                "": "1f5fd",
                "[日本地図]": "1f5fe",
                "[モアイ]": "1f5ff",
                "": "1f45f",
                "": "1f460",
                "": "1f461",
                "": "1f462",
                "[メガネ]": "1f453",
                "": "1f45a",
                "[ジーンズ]": "1f456",
                "": "1f451",
                "": "1f454",
                "": "1f452",
                "": "1f457",
                "": "1f458",
                "": "1f459",
                "[財布]": "1f45b",
                "": "1f45c",
                "[ふくろ]": "1f45d",
                "": "1f4b5",
                "": "1f4b1",
                "": "1f4c8",
                "[カード]": "1f4b3",
                "￥": "1f4b4",
                "[飛んでいくお金]": "1f4b8",
                "": "1f1e81f1f3",
                "": "1f1e91f1ea",
                "": "1f1ea1f1f8",
                "": "1f1eb1f1f7",
                "": "1f1ec1f1e7",
                "": "1f1ee1f1f9",
                "": "1f1ef1f1f5",
                "": "1f1f01f1f7",
                "": "1f1f71f1fa",
                "": "1f1fa1f1f8",
                "": "1f525",
                "[懐中電灯]": "1f526",
                "[レンチ]": "1f527",
                "": "1f528",
                "[ネジ]": "1f529",
                "[包丁]": "1f52a",
                "": "1f52b",
                "": "1f52f",
                "": "1f530",
                "": "1f531",
                "": "1f489",
                "": "1f48a",
                "": "1f170",
                "": "1f171",
                "": "1f18e",
                "": "1f17e",
                "": "1f380",
                "": "1f4e6",
                "": "1f382",
                "": "1f384",
                "": "1f385",
                "": "1f38c",
                "": "1f386",
                "": "1f388",
                "": "1f389",
                "": "1f38d",
                "": "1f38e",
                "": "1f393",
                "": "1f392",
                "": "1f38f",
                "": "1f387",
                "": "1f390",
                "": "1f383",
                "[オメデトウ]": "1f38a",
                "[七夕]": "1f38b",
                "": "1f391",
                "[ポケベル]": "1f4df",
                "": "1f4de",
                "": "1f4f1",
                "": "1f4f2",
                "": "1f4d1",
                "": "1f4e0",
                "": "1f4e7",
                "": "1f4eb",
                "": "1f4ee",
                "[新聞]": "1f4f0",
                "": "1f4e2",
                "": "1f4e3",
                "": "1f4e1",
                "[送信BOX]": "1f4e4",
                "[受信BOX]": "1f4e5",
                "[ABCD]": "1f520",
                "[abcd]": "1f521",
                "[1234]": "1f522",
                "[記号]": "1f523",
                "[ABC]": "1f524",
                "[ペン]": "2712",
                "": "1f4ba",
                "": "1f4bb",
                "[クリップ]": "1f4ce",
                "": "1f4bc",
                "": "1f4be",
                "": "1f4bf",
                "": "1f4c0",
                "": "2702",
                "[画びょう]": "1f4cc",
                "[カレンダー]": "1f4c6",
                "[フォルダ]": "1f4c2",
                "": "1f4d2",
                "[名札]": "1f4db",
                "[スクロール]": "1f4dc",
                "[グラフ]": "1f4c9",
                "[定規]": "1f4cf",
                "[三角定規]": "1f4d0",
                "": "26be",
                "": "26f3",
                "": "1f3be",
                "": "26bd",
                "": "1f3bf",
                "": "1f3c0",
                "": "1f3c1",
                "[スノボ]": "1f3c2",
                "": "1f3c3",
                "": "1f3c4",
                "": "1f3c6",
                "": "1f3c8",
                "": "1f3ca",
                "": "1f683",
                "": "24c2",
                "": "1f684",
                "": "1f685",
                "": "1f697",
                "": "1f699",
                "": "1f68c",
                "": "1f68f",
                "": "2708",
                "": "26f5",
                "": "1f689",
                "": "1f680",
                "": "1f6a4",
                "": "1f695",
                "": "1f69a",
                "": "1f692",
                "": "1f691",
                "": "1f6a8",
                "": "26fd",
                "": "1f17f",
                "": "1f6a5",
                "": "26d4",
                "": "2668",
                "": "26fa",
                "": "1f3a1",
                "": "1f3a2",
                "": "1f3a4",
                "": "1f4f9",
                "": "1f3a6",
                "": "1f3a7",
                "": "1f3a8",
                "": "1f3ad",
                "[イベント]": "1f3aa",
                "": "1f3ab",
                "": "1f3ac",
                "[ゲーム]": "1f3ae",
                "": "1f004",
                "": "1f3af",
                "": "1f3b0",
                "": "1f3b1",
                "[サイコロ]": "1f3b2",
                "[ボーリング]": "1f3b3",
                "[花札]": "1f3b4",
                "[ジョーカー]": "1f0cf",
                "": "1f3b5",
                "": "1f3bc",
                "": "1f3b7",
                "": "1f3b8",
                "[ピアノ]": "1f3b9",
                "": "1f3ba",
                "[バイオリン]": "1f3bb",
                "": "303d",
                "": "1f4f7",
                "": "1f4fa",
                "": "1f4fb",
                "": "1f4fc",
                "": "1f48b",
                "": "1f48c",
                "": "1f48d",
                "": "1f48e",
                "": "1f48f",
                "": "1f490",
                "": "1f491",
                "": "1f492",
                "": "1f51e",
                "": "a9",
                "": "ae",
                "": "2122",
                "[ｉ]": "2139",
                "": "2320e3",
                "": "3120e3",
                "": "3220e3",
                "": "3320e3",
                "": "3420e3",
                "": "3520e3",
                "": "3620e3",
                "": "3720e3",
                "": "3820e3",
                "": "3920e3",
                "": "3020e3",
                "[10]": "1f51f",
                "": "1f4f6",
                "": "1f4f3",
                "": "1f4f4",
                "": "1f354",
                "": "1f359",
                "": "1f370",
                "": "1f35c",
                "": "1f35e",
                "": "1f373",
                "": "1f366",
                "": "1f35f",
                "": "1f361",
                "": "1f358",
                "": "1f35a",
                "": "1f35d",
                "": "1f35b",
                "": "1f362",
                "": "1f363",
                "": "1f371",
                "": "1f372",
                "": "1f367",
                "[肉]": "1f356",
                "[なると]": "1f365",
                "[やきいも]": "1f360",
                "[ピザ]": "1f355",
                "[チキン]": "1f357",
                "[アイスクリーム]": "1f368",
                "[ドーナツ]": "1f369",
                "[クッキー]": "1f36a",
                "[チョコ]": "1f36b",
                "[キャンディ]": "1f36d",
                "[プリン]": "1f36e",
                "[ハチミツ]": "1f36f",
                "[エビフライ]": "1f364",
                "": "1f374",
                "": "2615",
                "": "1f379",
                "": "1f37a",
                "": "1f375",
                "": "1f37b",
                "": "2934",
                "": "2935",
                "": "2196",
                "": "2199",
                "⇔": "2194",
                "↑↓": "1f503",
                "": "2b06",
                "": "2b07",
                "": "27a1",
                "": "1f519",
                "": "25b6",
                "": "25c0",
                "": "23e9",
                "": "23ea",
                "▲": "1f53c",
                "▼": "1f53d",
                "": "2b55",
                "": "2716",
                "": "2757",
                "！？": "2049",
                "！！": "203c",
                "": "2753",
                "": "2754",
                "": "2755",
                "～": "27b0",
                "": "27bf",
                "": "2764",
                "": "1f49e",
                "": "1f494",
                "": "1f497",
                "": "1f498",
                "": "1f499",
                "": "1f49a",
                "": "1f49b",
                "": "1f49c",
                "": "1f49d",
                "": "1f49f",
                "": "2665",
                "": "2660",
                "": "2666",
                "": "2663",
                "": "1f6ac",
                "": "1f6ad",
                "": "267f",
                "[旗]": "1f6a9",
                "": "26a0",
                "": "1f6b2",
                "": "1f6b6",
                "": "1f6b9",
                "": "1f6ba",
                "": "1f6c0",
                "": "1f6bb",
                "": "1f6bd",
                "": "1f6be",
                "": "1f6bc",
                "[ドア]": "1f6aa",
                "[禁止]": "1f6ab",
                "[チェックマーク]": "2705",
                "[CL]": "1f191",
                "": "1f192",
                "[FREE]": "1f193",
                "": "1f194",
                "": "1f195",
                "[NG]": "1f196",
                "": "1f197",
                "[SOS]": "1f198",
                "": "1f199",
                "": "1f19a",
                "": "1f201",
                "": "1f202",
                "[禁]": "1f232",
                "": "1f233",
                "[合]": "1f234",
                "": "1f235",
                "": "1f236",
                "": "1f21a",
                "": "1f237",
                "": "1f238",
                "": "1f239",
                "": "1f22f",
                "": "1f23a",
                "": "3299",
                "": "3297",
                "": "1f250",
                "[可]": "1f251",
                "[＋]": "2795",
                "[－]": "2796",
                "[÷]": "2797",
                "": "1f4a1",
                "": "1f4a2",
                "": "1f4a3",
                "": "1f4a4",
                "[ドンッ]": "1f4a5",
                "": "1f4a7",
                "": "1f4a8",
                "": "1f4a9",
                "": "1f4aa",
                "[フキダシ]": "1f4ac",
                "": "2747",
                "": "2734",
                "": "2733",
                "": "1f534",
                "": "25fc",
                "": "1f539",
                "": "2b50",
                "[花丸]": "1f4ae",
                "[100点]": "1f4af",
                "←┘": "21a9",
                "└→": "21aa",
                "": "1f50a",
                "[電池]": "1f50b",
                "[コンセント]": "1f50c",
                "": "1f50e",
                "": "1f510",
                "": "1f513",
                "": "1f511",
                "": "1f514",
                "[ラジオボタン]": "1f518",
                "[ブックマーク]": "1f516",
                "[リンク]": "1f517",
                "[end]": "1f51a",
                "[ON]": "1f51b",
                "[SOON]": "1f51c",
                "": "1f51d",
                "": "270a",
                "": "270c",
                "": "1f44a",
                "": "1f44d",
                "": "261d",
                "": "1f446",
                "": "1f447",
                "": "1f448",
                "": "1f449",
                "": "1f44b",
                "": "1f44f",
                "": "1f44c",
                "": "1f44e",
                "": "1f450"
            }, s = {
                "/微笑": "0",
                "/撇嘴": "1",
                "/色": "2",
                "/发呆": "3",
                "/得意": "4",
                "/流泪": "5",
                "/害羞": "6",
                "/闭嘴": "7",
                "/睡": "8",
                "/大哭": "9",
                "/尴尬": "10",
                "/发怒": "11",
                "/调皮": "12",
                "/呲牙": "13",
                "/惊讶": "14",
                "/难过": "15",
                "/酷": "16",
                "/冷汗": "17",
                "/抓狂": "18",
                "/吐": "19",
                "/偷笑": "20",
                "/可爱": "21",
                "/白眼": "22",
                "/傲慢": "23",
                "/饥饿": "24",
                "/困": "25",
                "/惊恐": "26",
                "/流汗": "27",
                "/憨笑": "28",
                "/大兵": "29",
                "/奋斗": "30",
                "/咒骂": "31",
                "/疑问": "32",
                "/嘘": "33",
                "/晕": "34",
                "/折磨": "35",
                "/衰": "36",
                "/骷髅": "37",
                "/敲打": "38",
                "/再见": "39",
                "/擦汗": "40",
                "/抠鼻": "41",
                "/鼓掌": "42",
                "/糗大了": "43",
                "/坏笑": "44",
                "/左哼哼": "45",
                "/右哼哼": "46",
                "/哈欠": "47",
                "/鄙视": "48",
                "/委屈": "49",
                "/快哭了": "50",
                "/阴险": "51",
                "/亲亲": "52",
                "/吓": "53",
                "/可怜": "54",
                "/菜刀": "55",
                "/西瓜": "56",
                "/啤酒": "57",
                "/篮球": "58",
                "/乒乓": "59",
                "/咖啡": "60",
                "/饭": "61",
                "/猪头": "62",
                "/玫瑰": "63",
                "/凋谢": "64",
                "/示爱": "65",
                "/爱心": "66",
                "/心碎": "67",
                "/蛋糕": "68",
                "/闪电": "69",
                "/炸弹": "70",
                "/刀": "71",
                "/足球": "72",
                "/瓢虫": "73",
                "/便便": "74",
                "/月亮": "75",
                "/太阳": "76",
                "/礼物": "77",
                "/拥抱": "78",
                "/强": "79",
                "/弱": "80",
                "/握手": "81",
                "/胜利": "82",
                "/抱拳": "83",
                "/勾引": "84",
                "/拳头": "85",
                "/差劲": "86",
                "/爱你": "87",
                "/NO": "88",
                "/OK": "89",
                "/爱情": "90",
                "/飞吻": "91",
                "/跳跳": "92",
                "/发抖": "93",
                "/怄火": "94",
                "/转圈": "95",
                "/磕头": "96",
                "/回头": "97",
                "/跳绳": "98",
                "/挥手": "99",
                "/激动": "100",
                "/街舞": "101",
                "/献吻": "102",
                "/左太极": "103",
                "/右太极": "104",
                "/::)": "0",
                "/::~": "1",
                "/::B": "2",
                "/::|": "3",
                "/:8-)": "4",
                "/::<": "5",
                "/::$": "6",
                "/::X": "7",
                "/::Z": "8",
                "/::(": "9",
                "/::'(": "9",
                "/::-|": "10",
                "/::@": "11",
                "/::P": "12",
                "/::D": "13",
                "/::O": "14",
                "/::(": "15",
                "/::+": "16",
                "/:--b": "17",
                "/::Q": "18",
                "/::T": "19",
                "/:,@P": "20",
                "/:,@-D": "21",
                "/::d": "22",
                "/:,@o": "23",
                "/::g": "24",
                "/:|-)": "25",
                "/::!": "26",
                "/::L": "27",
                "/::>": "28",
                "/::,@": "29",
                "/:,@f": "30",
                "/::-S": "31",
                "/:": "32",
                "/:,@x": "33",
                "/:,@@": "34",
                "/::8": "35",
                "/:,@!": "36",
                "/:!!!": "37",
                "/:xx": "38",
                "/:bye": "39",
                "/:wipe": "40",
                "/:dig": "41",
                "/:handclap": "42",
                "/:&-(": "43",
                "/:B-)": "44",
                "/:<@": "45",
                "/:@>": "46",
                "/::-O": "47",
                "/:>-|": "48",
                "/:P-(": "49",
                "/::'|": "50",
                "/:X-)": "51",
                "/::*": "52",
                "/:@x": "53",
                "/:8*": "54",
                "/:pd": "55",
                "/:<W>": "56",
                "/:beer": "57",
                "/:basketb": "58",
                "/:oo": "59",
                "/:coffee": "60",
                "/:eat": "61",
                "/:pig": "62",
                "/:rose": "63",
                "/:fade": "64",
                "/:showlove": "65",
                "/:heart": "66",
                "/:break": "67",
                "/:cake": "68",
                "/:li": "69",
                "/:bome": "70",
                "/:kn": "71",
                "/:footb": "72",
                "/:ladybug": "73",
                "/:shit": "74",
                "/:moon": "75",
                "/:sun": "76",
                "/:gift": "77",
                "/:hug": "78",
                "/:strong": "79",
                "/:weak": "80",
                "/:share": "81",
                "/:v": "82",
                "/:@)": "83",
                "/:jj": "84",
                "/:@@": "85",
                "/:bad": "86",
                "/:lvu": "87",
                "/:no": "88",
                "/:ok": "89",
                "/:love": "90",
                "/:<L>": "91",
                "/:jump": "92",
                "/:shake": "93",
                "/:<O>": "94",
                "/:circle": "95",
                "/:kotow": "96",
                "/:turn": "97",
                "/:skip": "98",
                "/:oY": "99",
                "/:#-0": "100",
                "/:hiphot": "101",
                "/:kiss": "102",
                "/:<&": "103",
                "/:&>": "104"
            }, o = '<span class="emoji emoji%s"></span>', u = wx.resPath + "/mpres/htmledition/images/icon/emotion/", a = '<img src="' + u + '%s.gif" width="24" height="24">';
            String.prototype.emoji = function () {
                var e = this.toString();
                for (var t in i) while (-1 != e.indexOf(t)) e = e.replace(t, o.sprintf(i[t]));
                for (var t in s) while (-1 != e.indexOf(t)) e = e.replace(t, a.sprintf(s[t]));
                return e;
            };
        } catch (f) {
            wx.jslog({
                src: "common/qq/emoji.js"
            }, f);
        }
    });

define(
    "message/send.js",
    ["common/qq/emoji.js",
        "common/wx/richEditor/msgSender.js",
        "message/message_cgi.js",
        "message/renderList.js",
        "common/wx/verifycode.js"],
    function (e, t, n) {
        try {
            var r = +(new Date);
            "use strict",
            e("common/qq/emoji.js");
            var i = wx.cgiData,
                s = i.tofakeid,
                o = i.to_nick_name,
                u = i.msg_items.msg_item,
                a = e("common/wx/richEditor/msgSender.js"),
                f = e("message/message_cgi.js");
            $("#js_nick_name").html(i.to_nick_name.emoji());
            var l = 1e3,
                c = l * 5;
            (function (t) {
                function n(e) {
                    if (e.length <= 0) return;
                    var t = e[0];
                    i = t.fakeid, o = t.id, h = t.date_time, p({
                        container: "#listContainer",
                        preAppend: !0,
                        list: e
                    });
                }
                function r(e) {
                    return e < l * 20 && (e += l * 5), e;
                }
                var i,
                    o,
                    h,
                    p = e("message/renderList.js"),
                    d = function () {
                        f.getNewMsg(s, i, o, h,
                            function (e) {
                        n(e), e.length <= 0  c = r(c) : c = 5 * l, setTimeout(d, c);
                    }, function () {
                        c = r(c), setTimeout(d, c);
                    });
                };
                n(u),
                setTimeout(d, c);
                var v = new a($("#js_msgSender"),
                    {data: { type: 1 },
                    acl: wx.acl.msg_acl
                    }),
                m = null,
                g = $("#verifycode"),
                y = e("common/wx/verifycode.js");
                $("#js_submit").click(function () {
                    var e = v.getData();
                    if (e.error) return;
                    var t = e.data;
                    t.tofakeid = s,
                    t.fileid = t.file_id,
                    t.appmsgid = t.app_id;
                    if (m != null && m.getCode().trim().length <= 0) {
                        Tips.err("请输入验证码"), m.focus();
                        return;
                    }
                    t.imgcode = m && m.getCode().trim();
                    var n = $(this).btn(!1);
                    f.sendMsg(t,
                        function (e) {
                            n.btn(!0),
                                g.html("").hide(),
                            m = null,
                            v.clear(),
                            c = 3 * l,
                            setTimeout(d, c);
                        },
                    function (e) {
                        g.html("").hide(),
                        n.btn(!0),
                        e.base_resp && e.base_resp.ret == -8 && (m = g.html("").show().verifycode().data("verifycode"),
                        m.focus());
                    });
                });
            })(i);
        }
        catch (h) {
            wx.jslog({src: "message/send.js"}, h);
        }
    });