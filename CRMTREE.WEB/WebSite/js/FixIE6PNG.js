﻿// JavaScript Document
var DD_belatedPNG = {
        ns: "DD_belatedPNG",
        imgSize: {},
        delay: 10,
        nodesFixed: 0,
        createVmlNameSpace: function() {
                if (document.namespaces && !document.namespaces[this.ns]) {
                        document.namespaces.add(this.ns, "urn:schemas-microsoft-com:vml");
                }
        },
        createVmlStyleSheet: function() {
                var _1, _2;
                _1 = document.createElement("style");
                _1.setAttribute("media", "screen");
                document.documentElement.firstChild.insertBefore(_1, document.documentElement.firstChild.firstChild);
                if (_1.styleSheet) {
                        _1 = _1.styleSheet;
                        _1.addRule(this.ns + "\\:*", "{behavior:url(#default#VML)}");
                        _1.addRule(this.ns + "\\:shape", "position:absolute;");
                        _1.addRule("img." + this.ns + "_sizeFinder", "behavior:none; border:none; position:absolute; z-index:-1; top:-10000px; visibility:hidden;");
                        this.screenStyleSheet = _1;
                        _2 = document.createElement("style");
                        _2.setAttribute("media", "print");
                        document.documentElement.firstChild.insertBefore(_2, document.documentElement.firstChild.firstChild);
                        _2 = _2.styleSheet;
                        _2.addRule(this.ns + "\\:*", "{display: none !important;}");
                        _2.addRule("img." + this.ns + "_sizeFinder", "{display: none !important;}");
                }
        },
        readPropertyChange: function() {
                var el, _4, v;
                el = event.srcElement;
                if (!el.vmlInitiated) {
                        return;
                }
                if (event.propertyName.search("background") != -1 || event.propertyName.search("border") != -1) {
                        DD_belatedPNG.applyVML(el);
                }
                if (event.propertyName == "style.display") {
                        _4 = (el.currentStyle.display == "none") ? "none": "block";
                        for (v in el.vml) {
                                if (el.vml.hasOwnProperty(v)) {
                                        el.vml[v].shape.style.display = _4;
                                }
                        }
                }
                if (event.propertyName.search("filter") != -1) {
                        DD_belatedPNG.vmlOpacity(el);
                }
        },
        vmlOpacity: function(el) {
                if (el.currentStyle.filter.search("lpha") != -1) {
                        var _7 = el.currentStyle.filter;
                        _7 = parseInt(_7.substring(_7.lastIndexOf("=") + 1, _7.lastIndexOf(")")), 10) / 100;
                        el.vml.color.shape.style.filter = el.currentStyle.filter;
                        el.vml.image.fill.opacity = _7;
                }
        },
        handlePseudoHover: function(el) {
                setTimeout(function() {
                        DD_belatedPNG.applyVML(el);
                },
                1);
        },
        fix: function(_9) {
                if (this.screenStyleSheet) {
                        var _a, i;
                        _a = _9.split(",");
                        for (i = 0; i < _a.length; i++) {
                                this.screenStyleSheet.addRule(_a[i], "behavior:expression(DD_belatedPNG.fixPng(this))");
                        }
                }
        },
        applyVML: function(el) {
                el.runtimeStyle.cssText = "";
                this.vmlFill(el);
                this.vmlOffsets(el);
                this.vmlOpacity(el);
                if (el.isImg) {
                        this.copyImageBorders(el);
                }
        },
        attachHandlers: function(el) {
                var _e, _f, _10, _11, a, h;
                _e = this;
                _f = {
                        resize: "vmlOffsets",
                        move: "vmlOffsets"
                };
                if (el.nodeName == "A") {
                        _11 = {
                                mouseleave: "handlePseudoHover",
                                mouseenter: "handlePseudoHover",
                                focus: "handlePseudoHover",
                                blur: "handlePseudoHover"
                        };
                        for (a in _11) {
                                if (_11.hasOwnProperty(a)) {
                                        _f[a] = _11[a];
                                }
                        }
                }
                for (h in _f) {
                        if (_f.hasOwnProperty(h)) {
                                _10 = function() {
                                        _e[_f[h]](el);
                                };
                                el.attachEvent("on" + h, _10);
                        }
                }
                el.attachEvent("onpropertychange", this.readPropertyChange);
        },
        giveLayout: function(el) {
                el.style.zoom = 1;
                if (el.currentStyle.position == "static") {
                        el.style.position = "relative";
                }
        },
        copyImageBorders: function(el) {
                var _16, s;
                _16 = {
                        "borderStyle": true,
                        "borderWidth": true,
                        "borderColor": true
                };
                for (s in _16) {
                        if (_16.hasOwnProperty(s)) {
                                el.vml.color.shape.style[s] = el.currentStyle[s];
                        }
                }
        },
        vmlFill: function(el) {
                if (!el.currentStyle) {
                        return;
                } else {
                        var _19, _1a, lib, v, img, _1e;
                        _19 = el.currentStyle;
                }
                for (v in el.vml) {
                        if (el.vml.hasOwnProperty(v)) {
                                el.vml[v].shape.style.zIndex = _19.zIndex;
                        }
                }
                el.runtimeStyle.backgroundColor = "";
                el.runtimeStyle.backgroundImage = "";
                _1a = true;
                if (_19.backgroundImage != "none" || el.isImg) {
                        if (!el.isImg) {
                                el.vmlBg = _19.backgroundImage;
                                el.vmlBg = el.vmlBg.substr(5, el.vmlBg.lastIndexOf("\")") - 5);
                        } else {
                                el.vmlBg = el.src;
                        }
                        lib = this;
                        if (!lib.imgSize[el.vmlBg]) {
                                img = document.createElement("img");
                                lib.imgSize[el.vmlBg] = img;
                                img.className = lib.ns + "_sizeFinder";
                                img.runtimeStyle.cssText = "behavior:none; position:absolute; left:-10000px; top:-10000px; border:none; margin:0; padding:0;";
                                _1e = function() {
                                        this.width = this.offsetWidth;
                                        this.height = this.offsetHeight;
                                        lib.vmlOffsets(el);
                                };
                                img.attachEvent("onload", _1e);
                                img.src = el.vmlBg;
                                img.removeAttribute("width");
                                img.removeAttribute("height");
                                document.body.insertBefore(img, document.body.firstChild);
                        }
                        el.vml.image.fill.src = el.vmlBg;
                        _1a = false;
                }
                el.vml.image.fill.on = !_1a;
                el.vml.image.fill.color = "none";
                el.vml.color.shape.style.backgroundColor = _19.backgroundColor;
                el.runtimeStyle.backgroundImage = "none";
                el.runtimeStyle.backgroundColor = "transparent";
        },
        vmlOffsets: function(el) {
                var _20, _21, _22, _23, bg, bgR, dC, _27, b, c, v;
                _20 = el.currentStyle;
                _21 = {
                        "W": el.clientWidth + 1,
                        "H": el.clientHeight + 1,
                        "w": this.imgSize[el.vmlBg].width,
                        "h": this.imgSize[el.vmlBg].height,
                        "L": el.offsetLeft,
                        "T": el.offsetTop,
                        "bLW": el.clientLeft,
                        "bTW": el.clientTop
                };
                _22 = (_21.L + _21.bLW == 1) ? 1 : 0;
                _23 = function(vml, l, t, w, h, o) {
                        vml.coordsize = w + "," + h;
                        vml.coordorigin = o + "," + o;
                        vml.path = "m0,0l" + w + ",0l" + w + "," + h + "l0," + h + " xe";
                        vml.style.width = w + "px";
                        vml.style.height = h + "px";
                        vml.style.left = l + "px";
                        vml.style.top = t + "px";
                };
                _23(el.vml.color.shape, (_21.L + (el.isImg ? 0 : _21.bLW)), (_21.T + (el.isImg ? 0 : _21.bTW)), (_21.W - 1), (_21.H - 1), 0);
                _23(el.vml.image.shape, (_21.L + _21.bLW), (_21.T + _21.bTW), (_21.W), (_21.H), 1);
                bg = {
                        "X": 0,
                        "Y": 0
                };
                if (el.isImg) {
                        bg.X = parseInt(_20.paddingLeft, 10) + 1;
                        bg.Y = parseInt(_20.paddingTop, 10) + 1;
                } else {
                        for (b in bg) {
                                if (bg.hasOwnProperty(b)) {
                                        this.figurePercentage(bg, _21, b, _20["backgroundPosition" + b]);
                                }
                        }
                }
                el.vml.image.fill.position = (bg.X / _21.W) + "," + (bg.Y / _21.H);
                bgR = _20.backgroundRepeat;
                dC = {
                        "T": 1,
                        "R": _21.W + _22,
                        "B": _21.H,
                        "L": 1 + _22
                };
                _27 = {
                        "X": {
                                "b1": "L",
                                "b2": "R",
                                "d": "W"
                        },
                        "Y": {
                                "b1": "T",
                                "b2": "B",
                                "d": "H"
                        }
                };
                if (bgR != "repeat" || el.isImg) {
                        c = {
                                "T": (bg.Y),
                                "R": (bg.X + _21.w),
                                "B": (bg.Y + _21.h),
                                "L": (bg.X)
                        };
                        if (bgR.search("repeat-") != -1) {
                                v = bgR.split("repeat-")[1].toUpperCase();
                                c[_27[v].b1] = 1;
                                c[_27[v].b2] = _21[_27[v].d];
                        }
                        if (c.B > _21.H) {
                                c.B = _21.H;
                        }
                        el.vml.image.shape.style.clip = "rect(" + c.T + "px " + (c.R + _22) + "px " + c.B + "px " + (c.L + _22) + "px)";
                } else {
                        el.vml.image.shape.style.clip = "rect(" + dC.T + "px " + dC.R + "px " + dC.B + "px " + dC.L + "px)";
                }
        },
        figurePercentage: function(bg, _32, _33, _34) {
                var _35, _36;
                _36 = true;
                _35 = (_33 == "X");
                switch (_34) {
                case "left":
                case "top":
                        bg[_33] = 0;
                        break;
                case "center":
                        bg[_33] = 0.5;
                        break;
                case "right":
                case "bottom":
                        bg[_33] = 1;
                        break;
                default:
                        if (_34.search("%") != -1) {
                                bg[_33] = parseInt(_34, 10) / 100;
                        } else {
                                _36 = false;
                        }
                }
                bg[_33] = Math.ceil(_36 ? ((_32[_35 ? "W": "H"] * bg[_33]) - (_32[_35 ? "w": "h"] * bg[_33])) : parseInt(_34, 10));
                if (bg[_33] % 2 === 0) {
                        bg[_33]++;
                }
                return bg[_33];
        },
        fixPng: function(el) {
                el.style.behavior = "none";
                var lib, els, _3a, v, e;
                if (el.nodeName == "BODY" || el.nodeName == "TD" || el.nodeName == "TR") {
                        return;
                }
                el.isImg = false;
                if (el.nodeName == "IMG") {
                        if (el.src.toLowerCase().search(/\.png$/) != -1) {
                                el.isImg = true;
                                el.style.visibility = "hidden";
                        } else {
                                return;
                        }
                } else {
                        if (el.currentStyle.backgroundImage.toLowerCase().search(".png") == -1) {
                                return;
                        }
                }
                lib = DD_belatedPNG;
                el.vml = {
                        color: {},
                        image: {}
                };
                els = {
                        shape: {},
                        fill: {}
                };
                for (v in el.vml) {
                        if (el.vml.hasOwnProperty(v)) {
                                for (e in els) {
                                        if (els.hasOwnProperty(e)) {
                                                _3a = lib.ns + ":" + e;
                                                el.vml[v][e] = document.createElement(_3a);
                                        }
                                }
                                el.vml[v].shape.stroked = false;
                                el.vml[v].shape.appendChild(el.vml[v].fill);
                                el.parentNode.insertBefore(el.vml[v].shape, el);
                        }
                }
                el.vml.image.shape.fillcolor = "none";
                el.vml.image.fill.type = "tile";
                el.vml.color.fill.on = false;
                lib.attachHandlers(el);
                lib.giveLayout(el);
                lib.giveLayout(el.offsetParent);
                el.vmlInitiated = true;
                lib.applyVML(el);
        }
};
try {
        document.execCommand("BackgroundImageCache", false, true);
} catch(r) {}
DD_belatedPNG.createVmlNameSpace();
DD_belatedPNG.createVmlStyleSheet();
function pngFix(params) {
    var len = params.length;
    if (len == 0) { return}
    for (var i = 0; i < len; i++) {
        DD_belatedPNG.fix(params[i]); 
    }
}