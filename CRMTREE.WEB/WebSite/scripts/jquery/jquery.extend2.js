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


//$.plugin
(function ($) {
    //遮罩
    $.mask = {
        defaults: {
            loadingMsg: $.isEn() ? 'Processing...' : '处理中...'
        },
        show: function (target, options) {
            $target = target ? $(target) : $('body');
            var opts = $.extend({}, {
                loadingMsg: ''
            }, options);

            if ($.trim(opts.loadingMsg) === '') {
                opts.loadingMsg = this.defaults.loadingMsg;
            }

            $(">div.mask-container", $target).remove();

            var $containterMask = $([
	            '<div class="mask-container">',
	            '<div class="mask"></div>',
	            '<div class="mask-message' + (opts.isOpen ? ' open' : '') + '">' + opts.loadingMsg + '</div>',
	            '</div>'].join('')).appendTo($target);

            return $containterMask;
        },
        hide: function (target) {
            $target = target ? $(target) : $('body');
            $(">div.mask-container", $target).fadeOut("fast", function () {
                $(this).remove();
            });
        }
    };

    //弹出窗体
    $.topOpen = function (options) {
        var $win = window.top.$('<div></div>');
        var opts = $.extend({}, {
            url: '',
            title: ' ',
            loadingMsg: '',
            width: 600,
            height: 500,
            modal: true,
            closed: true,
            border: false,
            showMask: true,
            minimizable: false,
            maximizable: false,
            collapsible: false,
            scroll: false,
            onLoad: function () { }
        }, options, {
            isOpen: true,
            onClose: function () {
                window.setTimeout(function () {
                    $win.window('destroy', false);
                }, 0);
            }
        });
        opts.title = opts.title ? opts.title : " ";

        $win.window.style = "-webkit-box-shadow: 0px 1px 1px rgba(50, 50, 50, 0.75); -moz-box-shadow:    0px 1px 1px rgba(50, 50, 50, 0.75); box-shadow:         0px 1px 1px rgba(50, 50, 50, 0.75);  position: absolute; -moz-border-radius: 9px 9px 9px 9px; -webkit-border-radius: 9px 9px 9px 9px; border-radius: 9px 9px 9px 9px;";
        $win.appendTo(window.top.document.body);
        $win.window(opts).window('open');
        var $winBody = $win.window('body');
        $winBody.css({ overflow: opts.scroll ? 'auto' : 'hidden' });
        if (opts.showMask) {
            $.mask.show($winBody, opts);
        }

        var $iframe = $('<iframe src="' + options.url + '" style="width:100%;height:100%;" frameborder="0" border="0" scrolling="' + (opts.scroll ? '1' : '0') + '"></iframe>');
        var iframe = $iframe[0];

        if (iframe.attachEvent) {
            iframe.attachEvent("onload", function () {
                if (iframe.contentWindow) {
                    iframe.contentWindow._closeOwnerWindow = function () {
                        $win.window('close');
                    }
                }

                //if (iframe.contentWindow._setWindow) {
                //    iframe.contentWindow._setWindow($win);
                //}

                if (opts.showMask) {
                    $.mask.hide($winBody);
                }
                if ($.isFunction(opts.onLoad)) opts.onLoad.call();
            });
            //iframe.attachEvent("onunload", function () {

            //});

        } else {
            iframe.onload = function () {
                if (iframe.contentWindow) {
                    iframe.contentWindow._closeOwnerWindow = function () {
                        $win.window('close');
                    }
                }

                //if (iframe.contentWindow._setWindow) {
                //    iframe.contentWindow._setWindow($win);
                //}

                if (opts.showMask) {
                    $.mask.hide($winBody);
                }
                if ($.isFunction(opts.onLoad)) opts.onLoad.call();
            };
            //iframe.onunload = function () {

            //};
        }
        $iframe.appendTo($winBody);
        return $win;
    };

    function _tips(msg, autoHide) {
        var language = {
            title: { en: 'Prompt', cn: '提示' }
        };
        if (autoHide === undefined) {
            autoHide = true;
        }
        var isEn = $.isEn();
        $.messager.show({
            title: language.title[isEn ? "en" : "cn"],
            msg: msg,
            showType: 'fade',
            timeout: autoHide ? 1500 : 0,
            style: {
                right: '',
                bottom: ''
            }
        });
    }
    //消息提示
    $.msgTips = {
        defaults: {
            language: {
                select: { en: 'Please select one record', cn: '请选择一条记录' },
                noRecord: { en: 'No Records', cn: '没有记录' },
                save: {
                    ok: { en: 'Successfully Saved!', cn: '保存成功！' },
                    no: { en: 'Save function failed!', cn: '保存失败！' }
                },
                send: {
                    ok: { en: 'Successfully Sent!', cn: '发送成功！' },
                    no: { en: 'Send function failed!', cn: '发送失败！' }
                },
                done: {
                    ok: { en: 'Successfully Completed!', cn: '操作成功！' },
                    no: { en: 'Function failed!', cn: '操作失败！' }
                },
                add: {
                    ok: { en: 'Successfully Added !', cn: '添加成功！' },
                    no: { en: 'Add function failed!', cn: '添加失败！' }
                },
                remove: {
                    ok: { en: 'Successfully Deleted!', cn: '删除成功！' },
                    no: { en: 'Delete function failed!', cn: '删除失败！' }
                },
                release: {
                    ok: { en: 'Successfully Released!', cn: '发布成功！' },
                    no: { en: 'Release function failed!', cn: '发布失败！' }
                },
                approval: {
                    ok: { en: 'Successfully Approved!', cn: '审批成功！' },
                    no: { en: 'Approval function failed!', cn: '审批失败！' }
                },
                cancel: {
                    ok: { en: 'Successfully Canceled!', cn: '取消成功！' },
                    no: { en: 'Cancel function failed!', cn: '取消失败！' }
                },
                UpLoadVIN: {
                    ok: { en: 'File upload success!', cn: '文件上传成功！' },
                    no: { en: 'Failed to upload file!', cn: '文件上传失败！' }
                },
                UpLoadVIN_Checking: {
                    ok: { en: 'Checking file!', cn: '正在检查文件！' },
                    no: { en: 'Failed to upload file!', cn: '文件上传失败！' }
                },
                UpLoadVIN_Check: {
                    ok: { en: 'Check the file successfully!', cn: '文件检查成功！' },
                    no: { en: 'Failed to upload file!', cn: '文件上传失败！' }
                },
                UpLoadErrVin_Check: {
                    no: { en: 'The error file upload, please download the check!', cn: '上传文件中有错误，请下载检查！' }
                }
            }
        },
        //取消提示
        cancel: function (isOK) {
            var isEn = $.isEn();
            _tips(this.defaults.language.cancel[isOK ? "ok" : "no"][isEn ? "en" : "cn"]);
        },
        //信息提示
        info: function (msg, autoHide) {
            _tips(msg, autoHide);
        },
        //错误提示
        error: function (msg, autoHide) {
            _tips(msg, autoHide);
        },
        //审批
        approval: function () {
            var isEn = $.isEn();
            _tips(this.defaults.language.approval[isEn ? "en" : "cn"]);
        },
        //选择提示
        select: function () {
            var isEn = $.isEn();
            _tips(this.defaults.language.select[isEn ? "en" : "cn"]);
        },
        //无记录提示
        noRecord: function () {
            var isEn = $.isEn();
            _tips(this.defaults.language.noRecord[isEn ? "en" : "cn"]);
        },
        //保存提示
        save: function (isOK) {
            var isEn = $.isEn();
            var msg = this.defaults.language.save[isOK ? "ok" : "no"][isEn ? "en" : "cn"];
            _tips(msg, isOK);
        },
        done: function (isOK) {
            var isEn = $.isEn();
            var msg = this.defaults.language.done[isOK ? "ok" : "no"][isEn ? "en" : "cn"];
            _tips(msg, isOK);
        },
        send: function (isOK) {
            var isEn = $.isEn();
            var msg = this.defaults.language.send[isOK ? "ok" : "no"][isEn ? "en" : "cn"];
            _tips(msg, isOK);
        },
        release: function (isOK) {
            var isEn = $.isEn();
            var msg = this.defaults.language.release[isOK ? "ok" : "no"][isEn ? "en" : "cn"];
            _tips(msg, isOK);
        },
        add: function (isOK) {
            var isEn = $.isEn();
            var msg = this.defaults.language.add[isOK ? "ok" : "no"][isEn ? "en" : "cn"];
            _tips(msg, isOK);
        },
        //删除提示
        remove: function (isOK) {
            var isEn = $.isEn();
            var msg = this.defaults.language.remove[isOK ? "ok" : "no"][isEn ? "en" : "cn"];
            _tips(msg, isOK);
        },
        UpLoadVIN: function (isOK) {
            var isEn = $.isEn();
            var msg = this.defaults.language.UpLoadVIN[isOK ? "ok" : "no"][isEn ? "en" : "cn"];
            _tips(msg, isOK);
        },
        UpLoadVIN_Checking: function (isOK) {
            var isEn = $.isEn();
            var msg = this.defaults.language.UpLoadVIN_Checking[isOK ? "ok" : "no"][isEn ? "en" : "cn"];
            _tips(msg, isOK);
        },
        UpLoadVIN_Check: function (isOK) {
            var isEn = $.isEn();
            var msg = this.defaults.language.UpLoadVIN_Check[isOK ? "ok" : "no"][isEn ? "en" : "cn"];
            _tips(msg, isOK);
        },
        UploadErrVin_Check: function (con) {
            var isEn = $.isEn();
            var msg = this.defaults.language.UpLoadErrVin_Check["no"][isEn ? "en" : "cn"];
            _tips(msg, false);
        }

    };

    function _confirm(msg, fn) {
        var language = {
            title: { en: 'Prompt', cn: '提示' }
        };
        var isEn = $.isEn();
        var title = language.title[isEn ? "en" : "cn"];
        $.messager.confirm(title, msg, function (rs) {
            if (rs) {
                if ($.isFunction(fn)) {
                    fn.call();
                }
            }
        });
    };
    function _confirm2(msg, fn) {
        var language = {
            title: { en: 'Prompt', cn: '提示' }
        };
        var isEn = $.isEn();
        var title = language.title[isEn ? "en" : "cn"];
        $.messager.confirm(title, msg, function (rs) {
            if ($.isFunction(fn)) {
                fn.call(this, rs);
            }
        });
    }
    //确定提示
    $.confirmWindow = {
        //临时删除确定
        tempRemove: function (fn) {
            var language = {
                msg: {
                    en: 'This record will be DELETED when you click [Save] button?',
                    cn: '注意：本次操作将会删除该条记录，如确认，请点击下方【保存】按钮'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm(msg, fn);
        },
        //删除确定
        remove: function (fn) {
            var language = {
                msg: {
                    en: 'Are you sure you want to DELETE the selected record?',
                    cn: '你确定要删除选中记录？'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm(msg, fn);
        },
        //取消确定
        cancel: function (fn) {
            var language = {
                msg: {
                    en: 'Sure you want to CANCEL?',
                    cn: '确定要取消？'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm(msg, fn);
        },
        //忽略确定
        ignore: function (fn) {
            var language = {
                msg: {
                    en: 'Sure you want to IGNORE?',
                    cn: '确定要忽略？'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm(msg, fn);
        },
        //关闭确定
        close: function (fn) {
            var language = {
                msg: {
                    en: 'Sure you want to CLOSE?',
                    cn: '确定要关闭？'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm(msg, fn);
        },
        //撤消确定
        undo: function (fn) {
            var language = {
                msg: {
                    en: 'Sure you want to UNDO?',
                    cn: '确定要撤消？'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm(msg, fn);
        },
        //重置确定
        reset: function (fn) {
            var language = {
                msg: {
                    en: 'Sure you want to RESET all?',
                    cn: '确定要重置所有数据？'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm(msg, fn);
        },
        backToList: function (fn) {
            var language = {
                msg: {
                    en: 'Sure you want to go BACK to listing?',
                    cn: '确定要返回活动列表？'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm(msg, fn);
        },
        release: function (fn) {
            var language = {
                msg: {
                    en: 'Sure you want to RELEASE?',
                    cn: '确定要发布？'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm(msg, fn);
        },
        survey: function (count, fn) {
            var language = {
                msg: {
                    en: 'You have ' + count + ' questions unanswered,<br/>Click on Ok to return continue answer,<br/> click Cancel to save directly',
                    cn: '你有' + count + '条问题没有回答,<br/>点击"确认"返回继续作答,<br/>点击"取消"直接保存.'
                }
            };
            var isEn = $.isEn();
            var msg = language.msg[isEn ? "en" : "cn"];
            _confirm2(msg, fn);
        }
    };

    //检查返回值
    $.checkResponse = function (res, showTip) {
        var bCheck = true;
        if (!res || res.isOK === false) {
            bCheck = false;
        }
        if (showTip === undefined) {
            showTip = true;
        }
        if (showTip && !bCheck && res) {
            $.msgTips.error(res.msg, false);
        }
        return bCheck;
    }
    $.checkErrCode = function (res) {
        var bCheck = true;
        if (res === null) {
            bCheck = false;
        }
        return bCheck;
    }
    //批量上传
    $.plupload = function (opts) {
        //初始化
        var optioins = $.extend(true, {
            //container: 'container',//flash container
            //browse_button: 'pickfiles',
            runtimes: 'flash',//flash,html5,gears,silverlight,browserplus,html4
            filters: [
                { title: "Image files(*.jpg,*.jpeg,*.gif,*.png,*.bmp)", extensions: "jpg,jpeg,gif,png,bmp" }
            ],
            chunk_size: '1mb',
            max_file_size: '10mb',
            file_data_name: 'file',
            //multi_selection:false,
            //缩略图（无用）
            //resize : {width : 320, height : 240, quality : 90},
            unique_names: true,
            //上传参数
            params: {},
            init: {
                //选择并上传文件
                FilesAdded: function (up, files) {
                    /*
                    $.each(files, function (i, file) {
                        $('#filelist').append(
                            '<div id="' + file.id + '">' +
                            file.name + ' (' + plupload.formatSize(file.size) + ') <b></b>' +
                        '</div>');
                    });
                    up.refresh(); // Reposition Flash/Silverlight
                    if (files.length > 0) {
                        up.start();
                    }
                    */
                },
                //上传进度
                UploadProgress: function (up, file) {
                    //$('#' + file.id + " b").html(file.percent + "%");
                },
                //上传完成
                FileUploaded: function (up, file) {
                    //$('#' + file.id + " b").html("100%");
                },
                //上传错误
                Error: function (up, err) {
                    //"Error: " + err.code +", Message: " + err.message +(err.file ? ", File: " + err.file.name : "")
                    up.refresh(); // Reposition Flash/Silverlight
                }
            }
        }, opts, {
            _url: '/plupload/plupload.aspx',
            flash_swf_url: '/scripts/plupload-1.5.7/plupload.flash.swf',
            silverlight_xap_url: '/scripts/plupload-1.5.7/plupload.silverlight.xap',
            init: {
                //文件上传前
                BeforeUpload: function (up, file) {
                    var params = '';
                    if (up.settings.params) {
                        params = '&' + $.param(up.settings.params);
                    }
                    //解决中文文件名乱码问题
                    up.settings.url = up.settings._url + '?fileName=' + file.name + params;
                }
            }
        });
        //创建上传对象
        var uploader = new plupload.Uploader(optioins);
        //上传初始化
        uploader.init();

        return uploader;
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


