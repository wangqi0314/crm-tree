var _DateFormat = {
    Get_datetime_local: function (value) {
        var data = new Date(value);
        var _y = data.getFullYear();
        var _m = data.getMonth() + 1;
        _m = _m <= 9 ? ("0" + _m) : _m;
        var _d = data.getDate();
        _d = _d <= 9 ? ("0" + _d) : _d;
        var _h = data.getHours();
        _h = _h <= 9 ? ("0" + _h) : _h;
        var _M = data.getMinutes();
        _M = _M <= 9 ? ("0" + _M) : _M;
        var _S = data.getSeconds();
        _S = _S <= 9 ? ("0" + _S) : _S;
        //var N_date = _y + "-" + _m + "-" + _d + "T" + _h + ":" + _M + ":" + _S;
        var N_date = _y + "-" + _m + "-" + _d + "T" + _h + ":" + _M;
        return N_date
    }, Get_date_local: function (value) {
        var data = new Date(value);
        var _y = data.getFullYear();
        var _m = data.getMonth() + 1;
        _m = _m <= 9 ? ("0" + _m) : _m;
        var _d = data.getDate();
        _d = _d <= 9 ? ("0" + _d) : _d;
        var N_date = _y + "-" + _m + "-" + _d;
        return N_date
    }, Get_time: function (value) {
        var data = new Date(value);
        var _h = data.getHours();
        _h = _h <= 9 ? ("0" + _h) : _h;
        var _M = data.getMinutes();
        _M = _M <= 9 ? ("0" + _M) : _M;
        var N_date = _h + ":" + _M;
        return N_date
    }, timeConvert: function (d) {
        var data = new Date(d);
        var _h = data.getHours();
        _h = _h <= 9 ? ("0" + _h) : _h;
        var _M = data.getMinutes();
        _M = _M <= 9 ? ("0" + _M) : _M;
        var N_date = _h + "." + _M;
        return N_date
    }, set_time: function (value) {
        var data = value.split(".");
        var _d1 = data[0] == null ? "09" : (data[0] <= 9 ? ("0" + data[0]) : data[0]);
        var _d2 = data[1] == null ? "00" : (data[1].length < 2 ? "00" : data[1]);
        return _d1 + ":" + _d2;
    }, DateCenter: function (d1, d2, d3) {
        var date = new Date(d1);
        var dateS = new Date(d2);
        var dateE = new Date(d3);
        var _isCenter = false;
        if (dateS.getTime() <= date.getTime() && date.getTime() <= dateE.getTime()) {
            _isCenter = true;
        }
        return _isCenter;
    }, GetDateDay: function (d) {
        d = d.replace('T', ' ') + ":00";
        var o = new Object(d);
        var date = new Date(0);
        var _day = date.getDay();
        if (0 <= _day && _day <= 6) {
            if (_day == 0) {
                _day = 7;
            }
            return _day;
        } else {
            return -1;
        }
    }
};
//下拉列表模板
var DropDownList = {
    select: function () {
        return "<select name=\"" + arguments[0] + "\" id=\"" + arguments[0] + "\">" + arguments[1] + "</select>";
    },
    option: function () {
        return "<option value=\"" + arguments[0] + "\">" + arguments[1] + "</option>";
    },
    optionSelect: function () {
        return "<option value=\"" + arguments[0] + "\" selected=\"selected\">" + arguments[1] + "</option>";
    }
};
var Url = {
    GetParam: function () {
        var reg = new RegExp("(^|&)" + arguments[0] + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]);
        return null;
    }
};
var CookieUtil = {
    get: function (name) {
        var cookieName = encodeURIComponent(name) + "=",
        cookieStart = document.cookie.indexOf(cookieName),
        cookieValue = null;

        if (cookieStart > -1) {
            var cookieEnd = document.cookie.indexOf(";", cookieStart)
            if (cookieEnd == -1) {
                cookieEnd = document.cookie.length;
            }
            cookieValue = decodeURIComponent(document.cookie.substring(cookieStart
                + cookieName.length, cookieEnd));
        }
        return cookieValue;
    },
    set: function (name, value, expires, path, domain, secure) {
        var cookieText = encodeURIComponent(name) + "=" + encodeURIComponent(value);
        if (expires instanceof Date) {
            cookieText += "; expires=" + expires.toGMTString();
        }
        if (path) {
            cookieText += "; path=" + path;
        }
        if (domain) {
            cookieText += "; domain=" + domain;
        }
        if (secure) {
            cookieText += "; secure=" + secure;
        }

        document.cookie = cookieText;
    },
    unset: function (name, path, domain, secure) {
        this.set(name, "", new Date(0), path, domain, secure);
    }
};
var InputChecks = {
    _mobile: /^1[3|4|5|8][0-9]\d{8}$/,
    _pwd: /^[a-zA-Z0-9]{6,16}$/,
    _qrpwd: /^[a-zA-Z0-9]{6,16}$/,
    userName: function () {
        if (arguments[0] != "") { return true; }
        else { return false; }
    },
    Mobile: function () {
        if (this._mobile.test(arguments[0])) { return true; }
        else { return false; }
    },
    pwd: function () {
        if (this._pwd.test(arguments[0])) { return true; }
        else { return false; }
    },
    qrpwd: function () {
        if (this._qrpwd.test(arguments[0]) && arguments[0] == arguments[1]) { return true; }
        else { return false; }
    },
    yzm: function () {
        if (arguments[0] != CookieUtil.get(arguments[1])) { return true; }
        else { return false; }
    },
    birthday: function () {
        var _birthdaydate = new Date(arguments[0]);
        if (_birthdaydate < new Date()) { return true; }
        else { return false; }
    },
    Open: function () {
        if (this.Mobile() && this.pwd() && this.qrpwd()) { $("#error").html(""); return true; } else { return false; }
    }
};

var HtmlBind = {
    _option: function () {
        var v = arguments[0],
            n = arguments[1] == undefined ? arguments[0] : arguments[1];
        var op = "<option value=\"" + v + "\">" + n + "</option>";
        return op;
    }
};

(function ($) {
    $.extend({
        box: {
            mask: {
                option: { info: '正在加载...', width: '110px' },
                show: function (_opt) {
                    $.extend(this.option, _opt);
                    var getid = '<div class="dialog"><div class="dialog-body">' + this.option.info + '</div></div>';
                    var detail = '<div class="dialog-win" style="position:fixed;z-index:11" >';
                    detail = detail + getid + '</div>';
                    var masklayout = $('<div class="dialog-mask"></div>');
                    var win = $(detail);
                    win.find(".dialog").addClass("open");
                    $("body").append(masklayout);
                    $("body").append(win);
                    var x = parseInt($(window).width() - win.outerWidth()) / 2;
                    var y = parseInt($(window).height() - win.outerHeight()) / 2;
                    if (y <= 10) { y = "10" }
                    win.css({ "left": x, "top": y, 'width': this.option.width });
                    masklayout.click(function () {
                        win.remove();
                        $(this).remove();
                    });
                },
                hide: function () {
                    $('.dialog-mask').fadeOut("fast", function () {
                        $(this).remove();
                    });
                    $('.dialog-win').fadeOut("fast", function () {
                        $(this).remove();
                    });
                }
            },

        }
    });
})(jQuery);