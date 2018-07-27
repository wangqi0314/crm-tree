$(window).keydown(function (event) {
    if (event.keyCode === 8) {
        var type = event.target.type;
        if (event.target.readOnly || (type !== 'text' && type !== 'textarea' && type !== 'password')) {
            return false;
        }
    }
});

//$.methods
(function ($) {
    $.windowOpen = function (url,width,height,name) {
        var width = width > 0 ? width : 800;
        var height = height > 0 ? height : 500;

        var left = parseInt((screen.availWidth / 2) - (width / 2));
        var top = parseInt((screen.availHeight / 2) - (height / 2));
        var features = ",width=" + width + ",height=" + height + ",left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
        var rdm = Math.random();
        url = url.indexOf("?") >= 0 ? (url + "&r=" + rdm) : (url + "?r=" + rdm);
        var myWindow = window.open(url, "_" + $.trim(name), 'status=no,location=no,resizable,toolbar=no,scrollbars' + features, "_blank");
        if (myWindow) myWindow.focus();
    };

    $.format = function (result, args) {
        if (arguments.length > 1) {
            try {
                if (typeof (args) === "object") {
                    for (var key in args) {
                        var v = args[key];
                        if (v != undefined) {
                            var reg = new RegExp("({" + key + "})", "g");
                            result = result.replace(reg, v);
                        }
                    }
                }
                else {
                    for (var i = 1; i < arguments.length; i++) {
                        if (arguments[i] != undefined) {
                            var reg = new RegExp("({)" + (i - 1) + "(})", "g");
                            result = result.replace(reg, arguments[i]);
                        }
                    }
                }
            } catch (e) {
                var s = e.message;
            }
        }
        return result;
    };

    $.formatParams = function (result, args) {
        if (arguments.length > 1) {
            try {
                if (typeof (args) === "object") {
                    for (var key in args) {
                        var v = args[key];
                        if (v != undefined) {
                            var reg = new RegExp("({{" + key + "}})", "g");
                            result = result.replace(reg, v);
                        }
                    }
                }
                else {
                    for (var i = 1; i < arguments.length; i++) {
                        if (arguments[i] != undefined) {
                            var reg = new RegExp("({{)" + (i - 1) + "(}})", "g");
                            result = result.replace(reg, arguments[i]);
                        }
                    }
                }
            } catch (e) {
                var s = e.message;
            }
        }
        return result;
    };

    $.formatParamValue = function (result, args) {
        if (arguments.length > 1) {
            try {
                if (typeof (args) === "object") {
                    for (var key in args) {
                        var v = args[key];
                        if (v != undefined) {
                            var reg = new RegExp("(" + key + ")", "g");
                            result = result.replace(reg, v);
                        }
                    }
                }
                else {
                    for (var i = 1; i < arguments.length; i++) {
                        if (arguments[i] != undefined) {
                            var reg = new RegExp("()" + (i - 1) + "()", "g");
                            result = result.replace(reg, arguments[i]);
                        }
                    }
                }
            } catch (e) {
                var s = e.message;
            }
        }
        return result;
    };

    $.getCookie = function (sName) {
        var aCookie = document.cookie.split("; ");
        var lastMatch = null;
        for (var i = 0, len = aCookie.length; i < len; i++) {
            var aCrumb = aCookie[i].split("=");
            if (sName == aCrumb[0]) {
                lastMatch = aCrumb;
            }
        }
        if (lastMatch) {
            var v = lastMatch[1];
            if (v === undefined) return v;
            return unescape(v);
        }
        return null;
    };

    $.isEn = function () {
        var cookie = $.trim($.getCookie('language'));
        if (cookie == '') { return false;}
        return cookie === "en-us";
    };

    $.getParams = function (url) {
        if (!url) url = location.href;
        url = url.split("?")[1];
        var o = {};
        if (url) {
            var a = url.split("&");
            for (var i = 0, len = a.length; i < len; i++) {
                var aa = a[i].split("=");
                try {
                    o[aa[0]] = decodeURIComponent(aa[1]); //decodeURIComponent(unescape(aa[1]));
                } catch (e) { }
            }
        }
        return o;
    };

    $.getWords = function (wordIds, fun) {
        if (!$.isArray(wordIds) || !$.isFunction(fun)) {
            return;
        }

        var params = { action: 'GetWords', wordIds: wordIds.join(',') };
        $.post('/handler/Reports/Reports.aspx', { o: JSON.stringify(params) }, function (data) {
            var o = {};
            for (var i = 0, iLen = wordIds.length; i < iLen; i++) {
                var id = wordIds[i];
                var a_data = new Array();
                for (var j = 0, jLen = data.length; j < jLen; j++) {
                    var d = data[j];
                    if (id === d.p_id) {
                        a_data.push(d);
                    }
                }
                o['_' + id] = a_data;
            }
            if (fun) {
                fun.call(this, o);
            }
        }, "json");
    };
    $.GetWordsByIds = function (wordIds, fun) {
        if (!$.isArray(wordIds) || !$.isFunction(fun)) {
            return;
        }

        var params = { action: 'GetWordsByIds', wordIds: wordIds.join(',') };
        $.post('/handler/Reports/Reports.aspx', { o: JSON.stringify(params) }, function (data) {
            if (fun) {
                fun.call(this, data);
            }
        }, "json");
    };

    $.getWordsWithParent = function (wordIds, fun) {
        if (!$.isArray(wordIds) || !$.isFunction(fun)) {
            return;
        }
        var params = { action: 'GetWordsWithParent', wordIds: wordIds.join(',') };
        $.post('/handler/Reports/Reports.aspx', { o: JSON.stringify(params) }, function (data) {
            var o = {};
            for (var i = 0, iLen = wordIds.length; i < iLen; i++) {
                var id = wordIds[i];
                var a_data = new Array();
                for (var j = 0, jLen = data.length; j < jLen; j++) {
                    var d = data[j];
                    if (id === d.p_id) {
                        a_data.push(d);
                    }
                    if (id === d.id) {
                        o['__' + id] = d;
                    }
                }
                o['_' + id] = a_data;
            }
            if (fun) {
                fun.call(this, o);
            }
        }, "json");
    };

    $.getWordById = function (wordId, fun) {
        if (!(wordId > 0) || !$.isFunction(fun)) {
            return;
        }
        var params = { action: 'GetWordsByID', wordId: wordId };
        $.post('/handler/Reports/Reports.aspx', { o: JSON.stringify(params) }, function (data) {
            if (!data) {
                data = {};
            }
            if (fun) {
                fun.call(this, data);
            }
        }, "json");
    };

    $.getWordByValue = function (pid,value, fun) {
        if (!(pid > 0) || $.trim(value) == "" || !$.isFunction(fun)) {
            return;
        }
        var params = { action: 'getWordByValue', pid: pid,value:value };
        $.post('/handler/Reports/Reports.aspx', { o: JSON.stringify(params) }, function (data) {
            if (!data) {
                data = {};
            }
            if (fun) {
                fun.call(this, data);
            }
        }, "json");
    };

    $.fn.setTabLinks = function (TL_Code, fun) {
        var that = this;
        var params = { action: 'Get_Tab_Links', TL_Code: TL_Code };
        $.post('/handler/Reports/Reports.aspx', { o: JSON.stringify(params) }, function (data) {
            data = data ? data : [];
            if (data.length > 0) { $(that).empty();}
            $.each(data, function (i, d) {
                if (d.TL_Level == 1) {
                    var $span = $("<span></span>");
                    $span.text($.trim(d.TL_Text));
                    $(that).append($span);
                } else {
                    var $a = $("<a class='brown'></a>");
                    $a.attr("href", $.trim(d.TL_Link));
                    var $span = $("<span></span>");
                    $span.text($.trim(d.TL_Text));
                    $a.append($span);
                    $(that).append($a).append(" > ");
                }
            });
             
            if (fun) {
                fun.call(this, data);
            }
        }, "json");
    };
})(jQuery);


$(function () {
    (function () {
        var params = $.getParams();
        var TL_Code = params.M;
        if (TL_Code > 0) {
           // $(".nav_infor:first").setTabLinks(TL_Code);
        }
    })();
});
