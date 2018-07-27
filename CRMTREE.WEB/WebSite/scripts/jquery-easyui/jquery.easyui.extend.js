
//$.methods
(function ($) {
    $.bindWords = function (opts) {
        if (!opts || !$.isArray(opts.wordIds)) {
            return;
        }

        $.getWords(opts.wordIds, function (data) {
            if (!$.checkResponse(data)) { return; }
            for (var i = 0, len = opts.wordIds.length; i < len; i++) {
                var id = opts.wordIds[i];
                $.each(opts.selects, function (i, o) {
                    if (o.wordId == id) {
                        var $cbx = $("#" + o.selectId);
                        var options = $cbx.combobox('options');
                        var opt = {};
                        var a_words = $.map(data["_" + id], function (n) {
                            if (n.selected === true) {
                                opt.value = n.value
                            }
                            return n;
                        });
                        if (a_words.length > 10) {
                            opt.panelHeight = 200;
                        }
                        if (!options.required) {
                            var item = {};
                            item[options['valueField']] = ' ';
                            item[options['textField']] = '　';
                            a_words.unshift(item);
                            opt.onSelect = function (record) {
                                if (record && record[options['valueField']] === ' ') {
                                    $cbx.combobox('setText', '');
                                }
                            }
                        }
                        if (!$.isEmptyObject(opt)) {
                            $cbx.combobox(opt);
                        }
                        $cbx.combobox('loadData', a_words);
                    }
                });
            }
            if ($.isFunction(opts.onLoad)) opts.onLoad.call(this, data);
        });
    };

    $.bindWords_New = function (opts) {
        if (!opts || !$.isArray(opts.wordIds)) {
            return;
        }

        $.getWords(opts.wordIds, function (data) {
            if (!$.checkResponse(data)) { return; }
            for (var i = 0, len = opts.wordIds.length; i < len; i++) {
                var id = opts.wordIds[i];
                $.each(opts.selects, function (i, o) {
                    if (o.wordId == id) {
                        var $cbx = $("#" + o.selectId);
                        var options = $cbx.combobox('options');
                        var opt = {};
                        var a_words = $.map(data["_" + id], function (n) {
                            if (n.selected === true) {
                                opt.value = n.value
                            }
                            return n;
                        });
                        if (a_words.length > 10) {
                            opt.panelHeight = 200;
                        }
                        if (!options.required) {
                            var item = {};
                            item[options['valueField']] = '&&&';
                            item[options['textField']] = '　';
                            a_words.unshift(item);
                            opt.onSelect = function (record) {
                                if (record && record[options['valueField']] === '&&&') {
                                    $cbx.combobox('setText', '');
                                }
                            }
                        }
                        if (!$.isEmptyObject(opt)) {
                            $cbx.combobox(opt);
                        }
                        $cbx.combobox('loadData', a_words);
                    }
                });
            }
            if ($.isFunction(opts.onLoad)) opts.onLoad.call(this, data);
        });
    };

    $.form = {
        validate: function (configFields) {
            var validate = true;
            var bValid = true;
            configFields = configFields ? configFields : {};
            $.each(configFields, function (n, v) {
                if (!$.isArray(v)) { return false; }
                for (var i = 0, len = v.length; i < len; i++) {
                    var attrName = v[i];
                    switch (n) {
                        case 'textbox':
                            bValid = $("#" + attrName).textbox('enableValidation').textbox('isValid');
                            break;
                        case 'combobox':
                            var $cbx = $("#" + attrName);
                            var value = $.trim($cbx.combobox('getValue'));
                            if (value === '') {
                                $cbx.combobox('clear');
                            }
                            bValid = $cbx.combobox('enableValidation').combobox('isValid');
                            break;
                        case 'datebox':
                            bValid = $("#" + attrName).combobox('enableValidation').datebox('isValid');
                            break;
                        case 'timespinner':
                            bValid = $("#" + attrName).timespinner('enableValidation').timespinner('isValid');
                            break;
                        case 'numberbox':
                            bValid = $("#" + attrName).numberbox('enableValidation').numberbox('isValid');
                            break;
                        case 'combogrid':
                            bValid = $("#" + attrName).combogrid('enableValidation').combogrid('isValid');
                            break;
                    }
                    if (!bValid) {
                        validate = false;
                    }
                }
            });
            return validate;
        },
        disableValidation: function (configFields) {
            configFields = configFields ? configFields : {};
            $.each(configFields, function (n, v) {
                if (!$.isArray(v)) { return false; }
                for (var i = 0, len = v.length; i < len; i++) {
                    var attrName = v[i];
                    switch (n) {
                        case 'textbox':
                            $("#" + attrName).textbox('disableValidation');
                            break;
                        case 'combobox':
                            $("#" + attrName).combobox('disableValidation');
                            break;
                        case 'datebox':
                            $("#" + attrName).datebox('disableValidation');
                            break;
                        case 'timespinner':
                            $("#" + attrName).timespinner('disableValidation');
                            break;
                        case 'numberbox':
                            $("#" + attrName).numberbox('disableValidation');
                            break;
                        case 'combogrid':
                            $("#" + attrName).combogrid('disableValidation');
                            break;
                    }
                }
            });
        },
        clear: function (configFields) {
            configFields = configFields ? configFields : {};
            $.each(configFields, function (n, v) {
                if (!$.isArray(v)) { return false; }
                for (var i = 0, len = v.length; i < len; i++) {
                    var attrName = v[i];
                    switch (n) {
                        case 'textbox':
                            $("#" + attrName).textbox('clear');
                            break;
                        case 'combobox':
                            $("#" + attrName).combobox('clear');
                            break;
                        case 'datebox':
                            $("#" + attrName).datebox('clear');
                            break;
                        case 'timespinner':
                            $("#" + attrName).timespinner('clear');
                            break;
                        case 'numberbox':
                            $("#" + attrName).numberbox('clear');
                            break;
                        case 'combogrid':
                            $("#" + attrName).combogrid('clear');
                            break;
                    }
                }
            });
        },
        setEmptyText: function (frm, not) {
            var language = {
                combobox: { en: 'Please Select...', cn: '请选择...' },
                textbox: { en: 'Please input a value...', cn: '请输入值...' }
            };
            var isEn = $.isEn();
            $(":input[id].showEmptyText" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                var $c = $(this);
                if ($c.hasClass('easyui-textbox')) {
                    $c.textbox({ prompt: language.textbox[isEn ? "en" : "cn"] });
                    return true;
                }
                if ($c.hasClass('easyui-combobox')) {
                    $c.combobox({ prompt: language.combobox[isEn ? "en" : "cn"] });
                    return true;
                }
                if ($c.hasClass('easyui-datebox')) {
                    $c.datebox({ prompt: language.textbox[isEn ? "en" : "cn"] });
                    return true;
                }
                if ($c.hasClass('easyui-numberbox')) {
                    $c.numberbox({ prompt: language.textbox[isEn ? "en" : "cn"] });
                    return true;
                }
            });
        },
        reset: function (frm, not) {
            $(":input[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                var $c = $(this);
                if ($c.hasClass('easyui-textbox')) {
                    $c.textbox('reset');
                    return true;
                }
                if ($c.hasClass('easyui-combobox')) {
                    $c.combobox('reset');
                    return true;
                }
                if ($c.hasClass('easyui-combogrid')) {
                    $c.combogrid('reset');
                    return true;
                }
                if ($c.hasClass('easyui-datebox')) {
                    $c.datebox('reset');
                    return true;
                }
                if ($c.hasClass('easyui-timespinner')) {
                    $c.timespinner('reset');
                    return true;
                }
                if ($c.hasClass('easyui-numberbox')) {
                    $c.numberbox('reset');
                    return true;
                }
                if ($c.hasClass('easyui-numberspinner')) {
                    $c.numberspinner('reset');
                    return true;
                } 
                if ($c[0].type === 'hidden') {
                    $c.val('');
                    return true;
                }
                if ($c[0].type === 'checkbox') {
                    $c.attr("checked", false);
                    return true;
                }
            });
        },
        setData: function (frm, data, not) {
            if (data) {
                $(":input[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                    var $c = $(this);
                    var cid = $c.attr("id");
                    var val = data[cid];
                    if (typeof val === 'boolean') {
                        val = val ? 1 : 0;
                    }
                    val = $.trim(val);
                    if ($c.hasClass('easyui-textbox')) {
                        $c.textbox('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-combobox')) {
                        var opts = $c.combobox('options');
                        if (opts.multiple) {
                            var a_val = [];
                            if (val) {
                                a_val = val.split(",");
                            }
                            $c.combobox('setValues', a_val);
                        } else {
                            $c.combobox('select', val);
                        }
                        return true;
                    }
                    if ($c.hasClass('easyui-combogrid')) {
                        $c.combogrid('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-datebox')) {
                        $c.datebox('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-timespinner')) {
                        $c.timespinner('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-datetimespinner')) {
                        $c.datetimespinner('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-numberbox')) {
                        $c.numberbox('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-numberspinner')) {
                        $c.numberspinner('setValue', val);
                        return true;
                    } 
                    if ($c[0].type === 'hidden') {
                        $c.val(val);
                        return true;
                    }
                    if ($c[0].type === 'checkbox') {
                        var cbxValue = (val === '1');
                        $c.attr("checked", cbxValue);
                        return true;
                    }
                });

                $("span[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                    var $c = $(this);
                    var cid = $c.attr("id");
                    var val = data[cid];
                    if ($c.hasClass('uc-checkboxlist')) {
                        $c.checkboxlist('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('uc-checkbox')) {
                        $c.checkbox('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('uc-radiolist')) {
                        $c.radiolist('setValue', val);
                        return true;
                    }
                });
            }
        },
        setData_New: function (frm, data, not) {
            if (data) {
                $(":input[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                    var $c = $(this);
                    var cid = $c.attr("id");
                    var val = data[cid];
                    if (typeof val === 'boolean') {
                        val = val ? 1 : 0;
                    }
                    val = $.trim(val);
                    if ($c.hasClass('easyui-textbox')) {
                        $c.textbox('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-combobox')) {
                        var opts = $c.combobox('options');
                        if (opts.multiple) {
                            var a_val = [];
                            if (val) {
                                a_val = val.split(",");
                            }
                            $c.combobox('setValues', a_val);
                        } else {
                            if ($c.combobox('getData')[0].value == '&&&') {
                                if (val == '')
                                    $c.combobox('select', '&&&');
                                else
                                    $c.combobox('select', val);
                            } else {
                                $c.combobox('select', val);
                            }

                        }
                        return true;
                    }
                    if ($c.hasClass('easyui-combogrid')) {
                        $c.combogrid('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-datebox')) {
                        $c.datebox('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-timespinner')) {
                        $c.timespinner('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-datetimespinner')) {
                        $c.datetimespinner('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('easyui-numberbox')) {
                        $c.numberbox('setValue', val);
                        return true;
                    }
                    if ($c[0].type === 'hidden') {
                        $c.val(val);
                        return true;
                    }
                    if ($c[0].type === 'checkbox') {
                        var cbxValue = (val === '1');
                        $c.attr("checked", cbxValue);
                        return true;
                    }
                });

                $("span[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                    var $c = $(this);
                    var cid = $c.attr("id");
                    var val = data[cid];
                    if ($c.hasClass('uc-checkboxlist')) {
                        $c.checkboxlist('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('uc-checkbox')) {
                        $c.checkbox('setValue', val);
                        return true;
                    }
                    if ($c.hasClass('uc-radiolist')) {
                        $c.radiolist('setValue', val);
                        return true;
                    }
                });
            }
        },
        getData: function (frm, fields, not) {
            var data = {};
            $(":input[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                var $c = $(this);
                var cid = $c.attr("id");
                if ($c.hasClass('easyui-textbox')) {
                    data[cid] = $.trim($c.textbox('getValue'));
                    return true;
                }
                if ($c.hasClass('easyui-combobox')) {
                    var opts = $c.combobox('options');
                    if (opts.multiple) {
                        data[cid] = $c.combobox('getValues');
                        data[cid] = data[cid] ? data[cid].join(',') : data[cid];
                    } else { 
                            data[cid] = $c.combobox('getValue');
                    }
                    return true;
                }
                if ($c.hasClass('easyui-datebox')) {
                    data[cid] = $c.datebox('getValue');
                    return true;
                }
                if ($c.hasClass('easyui-numberbox')) {
                    data[cid] = $c.numberbox('getValue');
                    return true;
                }
                if ($c.hasClass('easyui-numberspinner')) {
                    data[cid] = $c.numberspinner('getValue');
                    return true;
                } 
                if ($c.hasClass('easyui-combogrid')) {
                    data[cid] = $c.combogrid('getValue');
                    return true;
                }
                if ($c.hasClass('easyui-timespinner')) {
                    data[cid] = $c.timespinner('getValue');
                    return true;
                }
                if ($c.hasClass('easyui-datetimespinner')) {
                    data[cid] = $c.datetimespinner('getValue');
                    return true;
                }
                if ($c[0].type === 'hidden') {
                    data[cid] = $c.val();
                    return true;
                }
                if ($c[0].type === 'checkbox') {
                    data[cid] = $c.attr('checked') === 'checked';
                    return true;
                }
            });

            $("span[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                var $c = $(this);
                var cid = $c.attr("id");
                if ($c.hasClass('uc-checkboxlist')) {
                    data[cid] = $c.checkboxlist('getValue');
                    return true;
                }
                if ($c.hasClass('uc-checkbox')) {
                    data[cid] = $c.checkbox('getValue');
                    return true;
                }
                if ($c.hasClass('uc-radiolist')) {
                    data[cid] = $c.radiolist('getValue');
                    return true;
                }
            });

            if (fields) {
                $.each(fields, function (i, n) {
                    if (typeof data[n] != 'boolean' && $.trim(data[n]) != '') {
                        data[n] = (data[n] == 1);
                    }
                });
            }

            return data;
        },
        getData_New: function (frm, fields, not) {
            var data = {};
            $(":input[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                var $c = $(this);
                var cid = $c.attr("id");
                if ($c.hasClass('easyui-textbox')) {
                    data[cid] = $.trim($c.textbox('getValue'));
                    return true;
                }
                if ($c.hasClass('easyui-combobox')) {
                    var opts = $c.combobox('options');
                    if (opts.multiple) {
                        data[cid] = $c.combobox('getValues');
                        data[cid] = data[cid] ? data[cid].join(',') : data[cid];
                    } else { 
                        if ($c.combobox('getValue') == '&&&')
                            data[cid] = '';
                        else
                            data[cid] = $c.combobox('getValue');
                    }
                    return true;
                }
                if ($c.hasClass('easyui-datebox')) {
                    data[cid] = $c.datebox('getValue');
                    return true;
                }
                if ($c.hasClass('easyui-numberbox')) {
                    data[cid] = $c.numberbox('getValue');
                    return true;
                }
                if ($c.hasClass('easyui-combogrid')) {
                    data[cid] = $c.combogrid('getValue');
                    return true;
                }
                if ($c.hasClass('easyui-timespinner')) {
                    data[cid] = $c.timespinner('getValue');
                    return true;
                }
                if ($c.hasClass('easyui-datetimespinner')) {
                    data[cid] = $c.datetimespinner('getValue');
                    return true;
                }
                if ($c[0].type === 'hidden') {
                    data[cid] = $c.val();
                    return true;
                }
                if ($c[0].type === 'checkbox') {
                    data[cid] = $c.attr('checked') === 'checked';
                    return true;
                }
            });

            $("span[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                var $c = $(this);
                var cid = $c.attr("id");
                if ($c.hasClass('uc-checkboxlist')) {
                    data[cid] = $c.checkboxlist('getValue');
                    return true;
                }
                if ($c.hasClass('uc-checkbox')) {
                    data[cid] = $c.checkbox('getValue');
                    return true;
                }
                if ($c.hasClass('uc-radiolist')) {
                    data[cid] = $c.radiolist('getValue');
                    return true;
                }
            });

            if (fields) {
                $.each(fields, function (i, n) {
                    if (typeof data[n] != 'boolean' && $.trim(data[n]) != '') {
                        data[n] = (data[n] == 1);
                    }
                });
            }

            return data;
        },
        _controlState: function (state, frm, not) {
            var data = {};
            $(":input[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                var $c = $(this);
                var cid = $c.attr("id");
                if ($c.hasClass('easyui-textbox')) {
                    $c.textbox(state)
                    return true;
                }
                if ($c.hasClass('easyui-combobox')) {
                    $c.combobox(state);
                    return true;
                }
                if ($c.hasClass('easyui-datebox')) {
                    $c.datebox(state)
                    return true;
                }
                if ($c.hasClass('easyui-numberbox')) {
                    $c.numberbox(state);
                    return true;
                }
                if ($c.hasClass('easyui-combogrid')) {
                    $c.combogrid(state);
                    return true;
                }
                if ($c.hasClass('easyui-timespinner')) {
                    $c.timespinner(state);
                    return true;
                }

                if ($c[0].type === 'checkbox') {
                    $c.attr('disabled', true);
                    return true;
                }
            });

            $("span[id]" + (not ? ":not(" + not + ")" : ""), frm).each(function () {
                var $c = $(this);
                var cid = $c.attr("id");
                if ($c.hasClass('uc-checkboxlist')) {
                    $c.checkboxlist('readonly');
                    return true;
                }
                if ($c.hasClass('uc-checkbox')) {
                    $c.checkbox('readonly');
                    return true;
                }
                if ($c.hasClass('uc-radiolist')) {
                    $c.radiolist('readonly');
                    return true;
                }
            });
        },
        readonly: function (frm, not) {
            this._controlState("readonly", frm, not);
        },
        disable: function (frm, not) {
            this._controlState("disable", frm, not);
        },
        getQueryParams: function (frm, bFields) {
            var data = [];
            $(":input[id]", frm).each(function () {
                var $c = $(this);
                var cid = $c.attr("id");
                var param = {
                    EX_Name: cid
                    , EX_Value: ''
                    , EX_DataType: $c.attr('FS_DataType')
                };
                if ($c.hasClass('easyui-textbox')) {
                    param.EX_Value = $.trim($c.textbox('getValue'));
                    data.push(param);
                    return true;
                }
                if ($c.hasClass('easyui-combobox')) {
                    param.EX_Value = $.trim($c.combobox('getValue'));
                    if (param.EX_DataType == 'int') {
                        param.EX_Value = param.EX_Value == '' ? 0 : param.EX_Value;
                    }
                    data.push(param);
                    return true;
                }
                if ($c.hasClass('easyui-datebox')) {
                    param.EX_Value = $c.datebox('getValue');
                    data.push(param);
                    return true;
                }
                if ($c.hasClass('easyui-numberbox')) {
                    param.EX_Value = $c.numberbox('getValue');
                    data.push(param);
                    return true;
                }
                if ($c.hasClass('easyui-combogrid')) {
                    param.EX_Value = $c.combogrid('getValue');
                    return true;
                }
                if ($c.hasClass('easyui-timespinner')) {
                    param.EX_Value = $c.timespinner('getValue');
                    data.push(param);
                    return true;
                }
                if ($c[0].type === 'hidden') {
                    param.EX_Value = $c.val();
                    data.push(param);
                    return true;
                }
                if ($c[0].type === 'checkbox') {
                    param.EX_Value = $c.attr('checked') === 'checked';
                    data.push(param);
                    return true;
                }
            });

            //if (bFields) {
            //    $.each(bFields, function (i, n) {
            //        if ($.trim(data[n]) != '') {
            //            data[n] = (data[n] == 1);
            //        }
            //    });
            //}

            return data;
        },
        setBoolean: function (data, attrNames, eValue) {
            if (!eValue) { eValue = 1; }
            if (data && attrNames) {
                $.each(attrNames, function (i, n) {
                    if ($.trim(data[n]) != '') {
                        data[n] = (data[n] == eValue);
                    }
                });
            }
        },
        fluid: function (frm) {
            $(":input[id].fluid", frm).each(function () {
                var $c = $(this);
                if ($c.hasClass('easyui-textbox')) {
                    $c.textbox('resize', "100%");
                    return true;
                }
                if ($c.hasClass('easyui-combobox')) {
                    $c.combobox('resize', "100%");
                    return true;
                }
                if ($c.hasClass('easyui-datebox')) {
                    $c.datebox('resize', "100%");
                    return true;
                }
                if ($c.hasClass('easyui-numberbox')) {
                    $c.numberbox('resize', "100%");
                    return true;
                }
                if ($c.hasClass('easyui-timespinner')) {
                    $c.timespinner('resize', "100%");
                    return true;
                }
                if ($c.hasClass('easyui-combogrid')) {
                    $c.combogrid('resize', "100%");
                    return true;
                }
            });
        }
    };
})(jQuery);

//$o.methods
(function ($) {
    $.fn.extend({
        initSelect: function (opts) {
            return this.each(function () {
                var $cbx = $(this);
                var options = $cbx.combobox('options');
                var options_part = {

                };
                if (options) {
                    options_part = {
                        required: options.required,
                        valueField: options.valueField,
                        textField: options.textField,
                        showNullItem: options.showNullItem,
                        nullItemText: options.nullItemText
                    };
                }
                var opt = $.extend(true, {
                    data: [],
                    valueField: 'text',
                    textField: 'value',
                    showNullItem: true,
                    required: false,
                    nullItemText: '　'
                }, options_part, opts);

                if (!opt.data) {
                    opt.data = [];
                }

                if (!opt.required && opt.showNullItem) {
                    var item = {};
                    item[opt.valueField] = '';
                    item[opt.textField] = opt.nullItemText;
                    opt.data.unshift(item);
                    opt.onSelect = function (record) {
                        if (record && record[opt.valueField] === '') {
                            $cbx.combobox('setText', '');
                        }
                        if (opts.onSelect) {
                            opts.onSelect.call(this, record);
                        }
                    }
                }

                $cbx.combobox(opt);

                $cbx.combobox('setText', '');
                if (opt.editable) {
                    $cbx.combobox('textbox').click(function () {
                        $cbx.combobox('showPanel');
                    });
                }
            });
        }
    });
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
            border:false,
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
        UploadErrVin_Check: function (con)
        {
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
                    fn.call(this,rs);
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
        survey: function (count,fn) {
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

//extend
if ($.fn.datagrid) {
    $.extend($.fn.datagrid.methods, {
        editCell: function (jq, param) {
            return jq.each(function () {
                var opts = $(this).datagrid('options');
                var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
                for (var i = 0; i < fields.length; i++) {
                    var col = $(this).datagrid('getColumnOption', fields[i]);
                    col.editor1 = col.editor;
                    if (fields[i] != param.field) {
                        col.editor = null;
                    }
                }
                $(this).datagrid('beginEdit', param.index);
                for (var i = 0; i < fields.length; i++) {
                    var col = $(this).datagrid('getColumnOption', fields[i]);
                    col.editor = col.editor1;
                }
            });
        }
    });
}

//defaults
if ($.fn.combobox) {
    $.extend($.fn.combobox.defaults, {
        panelHeight: 'auto',
        panelMaxHeight: 200,
        editable: false,
        showNullItem: true,
        novalidate: true,
        nullItemText: '　',
        filter: function (q, row) {
            var opts = $(this).combobox("options");
            var text = row[opts.textField];
            return text ? text.toLowerCase().indexOf(q.toLowerCase()) >= 0 : false;
        }
    });
}
if ($.fn.combogrid) {
    $.fn.combogrid.defaults.novalidate = true;
}
if ($.fn.textbox) {
    $.fn.textbox.defaults.novalidate = true;
}
if ($.fn.datebox) {
    $.fn.datebox.defaults.novalidate = true;
}
if ($.fn.timespinner) {
    $.fn.timespinner.defaults.novalidate = true;
}
if ($.fn.window) {
    $.fn.window.defaults.collapsible = false;
    $.fn.window.defaults.maximizable = false;
    $.fn.window.defaults.minimizable = false;
}

//editors
if ($.fn.datagrid) {
    $.extend($.fn.datagrid.defaults.editors, {
        combo: {
            init: function (container, options) {
                var input = $('<input type="text" class="datagrid-editable-input"/>').appendTo(container);
                options = options ? options : {};
                $.extend(options, { editable: false });
                input.combo(options);
                return input;
            },
            destroy: function (target) {
                $(target).remove();
            },
            getValue: function (target) {
                return $(target).val();
            },
            setValue: function (target, value) {
                $(target).val(value);
            },
            resize: function (target, width) {
                //$(target).combo('resize', width);
                $(target)._outerWidth(width);
            }
        }
    });
}
//rules
if ($.fn.validatebox) {
    $.extend($.fn.validatebox.defaults.rules, {
        maxLength: {
            validator: function (value, param) {
                return value.length <= param[0];
            },
            message: ($.isEn() ? 'No more than {0} characters.' : '不能超过 {0} 个字符')
        },
        gtOrEqualDate: {
            validator: function (value, param) {
                var d1 = $.fn.datebox.defaults.parser(param[0]);
                var d2 = $.fn.datebox.defaults.parser(value);
                return d2 >= d1;
            },
            message: ($.isEn() ? 'The date must be greater than or equal to {0}.' : '日期必须大于或等于{0}。')
        },
        equals: {
            validator: function (value, param) {
                return value == $(param[0]).val();
            },
            message: ($.isEn() ? 'Fields do not match.' : '字段不一致。')
        }
    });
}

/*
* desc checkboxList
*/
(function ($) {
    var STYLE = {
        checkbox: {
            cursor: "pointer",
            background: "transparent url('data:image/gif;base64,R0lGODlhDwAmAKIAAPr6+v///+vr68rKyvT09Pj4+ICAgAAAACH5BAAAAAAALAAAAAAPACYAAANuGLrc/mvISWcYJOutBS5gKIIeUQBoqgLlua7tC3+yGtfojes1L/sv4MyEywUEyKQyCWk6n1BoZSq5cK6Z1mgrtNFkhtx3ZQizxqkyIHAmqtTsdkENgKdiZfv9w9bviXFxXm4KP2g/R0uKAlGNDAkAOw==') no-repeat center top",
            verticalAlign: "middle",
            height: "19px",
            width: "18px",
            display: "block"
        },
        span: {
            "float": "left",
            display: "block",
            margin: "0px 0px",
            marginTop: "5px"
        },
        label: {
            marginTop: "8px",
            marginRight: "8px",
            display: "block",
            "float": "left",
            fontSize: "12px",
            cursor: "pointer",
            paddingLeft: "3px"
        }
    };

    function rander(target) {
        var jqObj = $(target);
        jqObj.addClass("uc-checkboxlist");
        //var id = jqObj.attr('id');
        var opts = $.data(target, 'checkbox').options;
        var data = opts.data ? opts.data : [];

        jqObj.css('display', 'inline-block');

        $.each(opts.data, function (i, d) {
            var jqCheckbox = $('<input type="checkbox" value="' + $.trim(d.value) + '" ' + (d.selected ? 'checked="checked"' : '') + ' label="' + $.trim(d.text) + '"/>').appendTo(jqObj);
            var jqWrap = $('<span/>').css(STYLE.span);
            var jqLabel = $('<label/>').css(STYLE.label);
            var jqCheckboxA = $('<a/>').data('lable', jqLabel).css(STYLE.checkbox).text(' ');
            var labelText = jqCheckbox.data('lable', jqLabel).attr('label');
            jqCheckbox.hide();
            jqCheckbox.after(jqLabel.text(labelText));
            jqCheckbox.wrap(jqWrap);
            jqCheckbox.before(jqCheckboxA);
            if (jqCheckbox.prop('checked')) {
                jqCheckboxA.css('background-position', 'center bottom');
            }

            var fun = d.valuechanged;
            var isFun = $.isFunction(fun);
            jqLabel.click(function () {
                (function (ck, cka) {
                    var checked = !ck.prop('checked');
                    ck.prop('checked', checked);
                    var y = 'top';
                    if (ck.prop('checked')) {
                        y = 'bottom';
                    }
                    cka.css('background-position', 'center ' + y);

                    if (isFun) {
                        fun.call(this, checked);
                    }
                })(jqCheckbox, jqCheckboxA);
            });

            jqCheckboxA.click(function () {
                $(this).data('lable').click();
            });
        });
    }

    $.fn.checkboxlist = function (options, param) {
        if (typeof options == 'string') {
            return $.fn.checkboxlist.methods[options](this, param);
        }

        options = options || {};
        return this.each(function () {
            if (!$.data(this, 'checkbox')) {
                $.data(this, 'checkbox', {
                    options: $.extend({}, $.fn.checkboxlist.defaults, $.fn.tabs.parseOptions(this), options)
                });
                rander(this);
            } else {
                var opt = $.data(this, 'checkbox').options;
                $.data(this, 'checkbox', {
                    options: $.extend({}, opt, options)
                });
            }
        });
    };

    function check(jq, val, check) {
        var ipt = jq.find('input[value=' + val + ']');
        if (ipt.length) {
            ipt.prop('checked', check).each(function () {
                $(this).data('lable').click();
            });
        }
    }

    $.fn.checkboxlist.methods = {
        readonly: function (jq) {
            jq.find('input').each(function () {
                $($(this).data('lable')).unbind("click");
            });
        },
        getValue: function (jq) {
            var checked = jq.find('input:checked');
            var vals = [];
            checked.each(function () {
                vals.push(this.value);
            });
            return vals.join(',');
        },
        setValue: function (jq, vals) {
            this.unCheckAll(jq);
            var a_vals = [];
            if (typeof vals == 'string' && $.trim(vals) != '') {
                a_vals = vals.split(',');
            }
            if (a_vals.length > 0) {
                $.each(a_vals, function () {
                    check(jq, this);
                });
            }
        },
        unCheck: function (jq, vals) {
            if (vals && typeof vals != 'object') {
                check(jq, vals, true);
            } else if (vals.sort) {
                $.each(vals, function () {
                    check(jq, this, true);
                });
            }
        },
        checkAll: function (jq) {
            jq.find('input').prop('checked', false).each(function () {
                $(this).data('lable').click();
            });
        },
        unCheckAll: function (jq) {
            jq.find('input').prop('checked', true).each(function () {
                $(this).data('lable').click();
            });
        }
    };

    $.fn.checkboxlist.defaults = {
        style: STYLE
    };

    if ($.parser && $.parser.plugins) {
        $.parser.plugins.push('checkboxlist');
    }
})(jQuery);

/*
* desc checkbox
*/
(function ($) {
    var STYLE = {
        checkbox: {
            cursor: "pointer",
            background: "transparent url('data:image/gif;base64,R0lGODlhDwAmAKIAAPr6+v///+vr68rKyvT09Pj4+ICAgAAAACH5BAAAAAAALAAAAAAPACYAAANuGLrc/mvISWcYJOutBS5gKIIeUQBoqgLlua7tC3+yGtfojes1L/sv4MyEywUEyKQyCWk6n1BoZSq5cK6Z1mgrtNFkhtx3ZQizxqkyIHAmqtTsdkENgKdiZfv9w9bviXFxXm4KP2g/R0uKAlGNDAkAOw==') no-repeat center top",
            verticalAlign: "middle",
            height: "19px",
            width: "18px",
            display: "block"
        },
        span: {
            "float": "left",
            display: "block",
            margin: "0px 0px",
            marginTop: "5px"
        },
        label: {
            marginTop: "8px",
            marginRight: "8px",
            display: "block",
            "float": "left",
            fontSize: "12px",
            cursor: "pointer",
            paddingLeft: "3px"
        }
    };

    function rander(target) {
        var jqObj = $(target);
        jqObj.addClass("uc-checkbox");
        var opts = $.data(target, 'checkbox').options;
        jqObj.css('display', 'inline-block');

        var jqCheckbox = $('<input type="checkbox" value="' + $.trim(opts.value) + '" ' + (opts.selected ? 'checked="checked"' : '') + ' label="' + $.trim(opts.text) + '"/>').appendTo(jqObj);
        var jqWrap = $('<span/>').css(STYLE.span);
        var jqLabel = $('<label/>').css(STYLE.label);
        var jqCheckboxA = $('<a/>').data('lable', jqLabel).css(STYLE.checkbox).text(' ');
        var labelText = jqCheckbox.data('lable', jqLabel).attr('label');
        jqCheckbox.hide();
        jqCheckbox.after(jqLabel.text(labelText));
        jqCheckbox.wrap(jqWrap);
        jqCheckbox.before(jqCheckboxA);
        if (jqCheckbox.prop('checked')) {
            jqCheckboxA.css('background-position', 'center bottom');
        }

        jqLabel.click(function () {
            (function (ck, cka) {
                ck.prop('checked', !ck.prop('checked'));
                var y = 'top';
                if (ck.prop('checked')) {
                    y = 'bottom';
                }
                cka.css('background-position', 'center ' + y);
            })(jqCheckbox, jqCheckboxA);
        });

        jqCheckboxA.click(function () {
            $(this).data('lable').click();
        });
    }

    $.fn.checkbox = function (options, param) {
        if (typeof options == 'string') {
            return $.fn.checkbox.methods[options](this, param);
        }

        options = options || {};
        return this.each(function () {
            if (!$.data(this, 'checkbox')) {
                $.data(this, 'checkbox', {
                    options: $.extend({}, $.fn.checkbox.defaults, $.fn.tabs.parseOptions(this), options)
                });
                rander(this);
            } else {
                var opt = $.data(this, 'checkbox').options;
                $.data(this, 'checkbox', {
                    options: $.extend({}, opt, options)
                });
            }
        });
    };

    function check(jq, check) {
        var ipt = jq.find('input');
        if (ipt.length) {
            ipt.prop('checked', !check).each(function () {
                $(this).data('lable').click();
            });
        }
    }

    $.fn.checkbox.methods = {
        readonly: function (jq) {
            jq.find('input').each(function () {
                $($(this).data('lable')).unbind("click");
            });
        },
        getValue: function (jq) {
            var checked = jq.find('input:checked:first');
            return !!checked.length;
        },
        setValue: function (jq, val) {
            check(jq, !!val);
        },
        check: function (jq) {
            check(jq, true);
        },
        unCheck: function (jq) {
            check(jq, false);
        },
        checkAll: function (jq) {
            jq.find('input').prop('checked', false).each(function () {
                $(this).data('lable').click();
            });
        },
        unCheckAll: function (jq) {
            jq.find('input').prop('checked', true).each(function () {
                $(this).data('lable').click();
            });
        }
    };

    $.fn.checkbox.defaults = {
        style: STYLE
    };

    if ($.parser && $.parser.plugins) {
        $.parser.plugins.push('checkbox');
    }
})(jQuery);

/*
* desc radioList
*/
(function ($) {
    var STYLE = {
        radio: {
            cursor: "pointer",
            background: "transparent url('data:image/gif;base64,R0lGODlhDwAmANUAAP////z8/Pj4+Ovr6/v7++7u7uPj493d3ff39/Ly8vT09ICAgPX19a+vr+Li4urq6vn5+fr6+v39/dXV1efn5+bm5uTk5ODg4N7e3v7+/vHx8fDw8O3t7e/v74eHh+Hh4c3NzdfX1+np6eXl5cDAwNra2t/f38/Pz/Pz8/b29sbGxsHBwc7OzsLCwujo6NHR0by8vL29vcXFxb+/v7m5udPT09jY2MPDw7u7u7i4uNLS0uzs7AAAAAAAAAAAAAAAACH5BAAAAAAALAAAAAAPACYAAAb/QIBwSCwaj0VGSIbLzUAXQfFDap1KjktJVysMHTHQ4WKgPAqaymQDYJBYh4/FNSgkGIjBgRC6YRwjIjsddwIRAQ4cKi8GFSIcGygphgEBBRYwB2ZoCggQBJUBCBg0FHV3nqChBAcrFYQMlKGVAgYnJpKyswEJDwYTqbuhAwoQEw+qwgoDEgAbNhzCAQoiUkIaGAYdCAQCCQMD1kMEBSMmBxbEzUjs7e7v8EJKTE5Q4kJUVlhaXF5CYGLIbEqzps2bOHNO4dHDxw+gAgIyREBAKdGiRgUMNPDQwICqS5nMQGiwoGSDDJVGlaqTwUPJBR4AVGLlihABkiZRBqh1S1IELY0cDUio1OtXKgkZAGQYWomYMWTSpjFzBk0aNXHYtHHzBu4eAHLm0KmLR5ZIEAA7') no-repeat center top",
            verticalAlign: "middle",
            height: "19px",
            width: "18px",
            display: "block"
        },
        span: {
            "float": "left",
            display: "block",
            margin: "0px 0px",
            marginTop: "5px"
        },
        label: {
            marginTop: "8px",
            marginRight: "8px",
            display: "block",
            "float": "left",
            fontSize: "12px",
            cursor: "pointer",
            paddingLeft: "3px"
        }
    };

    function rander(target) {
        var jqObj = $(target);
        jqObj.addClass("uc-radiolist");
        var id = jqObj.attr('id');
        var opts = $.data(target, 'radio').options;
        jqObj.css('display', 'inline-block');
        var checked;

        var fun = opts.valuechanged;
        var isFun = $.isFunction(fun);

        $.each(opts.data, function (i, d) {
            var jqRadio = $('<input name="' + id + '" type="radio" value="' + $.trim(d.value) + '" ' + (d.selected ? 'checked="checked"' : '') + ' label="' + $.trim(d.text) + '"/>').appendTo(jqObj);
            var jqWrap = $('<span/>').css(STYLE.span);
            var jqLabel = $('<label/>').css(STYLE.label);
            var jqRadioA = $('<a/>').data('lable', jqLabel).addClass("RadioA").css(STYLE.radio).text(' ');
            var labelText = jqRadio.data('lable', jqLabel).attr('label');
            jqRadio.hide();

            var $repeat = $('<div style="display:inline-block;*display:inline;zoom:1;"></div>');
            jqRadio.wrap($repeat);
            if (opts.repeatItems > 0 && (i + 1) % opts.repeatItems == 0) {
                jqRadio.parent().after('</br>');
            }

            jqRadio.after(jqLabel.text(labelText));
            jqRadio.wrap(jqWrap);
            jqRadio.before(jqRadioA);

            if (jqRadio.prop('checked')) {
                checked = jqRadioA;
            }

            var rdoClick = function () {
                (function (rdo) {
                    rdo.prop('checked', true);
                    $('input[type=radio]', jqObj).each(function () {
                        var rd = $(this);
                        var y = 'top';
                        if (rd.prop('checked')) {
                            y = 'bottom';
                        }
                        rd.prev().css('background-position', 'center ' + y);
                    });
                    if (isFun) {
                        fun.call(rdo.parent().parent(), d.value);
                    }
                })(jqRadio);
            };
            jqLabel.data("_click", rdoClick);
            jqLabel.click(rdoClick);

            jqRadioA.click(function () {
                $(this).data('lable').click();
            });
        });

        if (checked) {
            checked.css('background-position', 'center bottom');
        }
    }

    $.fn.radiolist = function (options, param) {
        if (typeof options == 'string') {
            return $.fn.radiolist.methods[options](this, param);
        }

        options = options || {};
        return this.each(function () {
            if (!$.data(this, 'radio')) {
                $.data(this, 'radio', {
                    options: $.extend({}, $.fn.radiolist.defaults, $.fn.tabs.parseOptions(this), options)
                });
                rander(this);
            } else {
                var opt = $.data(this, 'radio').options;
                $.data(this, 'radio', {
                    options: $.extend({}, opt, options)
                });
            }
        });
    };

    $.fn.radiolist.methods = {
        readonly: function (jq, b) {
            jq.find('input').each(function () {
                var $label = $($(this).data('lable'));
                if (b || typeof b == "undefined") {
                    $label.unbind("click");
                } else {
                    $label.bind("click", $label.data("_click"));
                }
            });
            return jq;
        },
        getValue: function (jq) {
            var checked = jq.find('input:checked:first');
            var value = '';
            if (checked.length) {
                value = checked[0].value;
            }
            return value;
        },
        setValue: function (jq, val) {
            if (typeof val != 'object' && $.trim(val) != '') {
                var ipt = jq.find('input[value=' + val + ']');
                if (ipt.length > 0) {
                    ipt.prop('checked', false).data('lable').click();
                }
            }
        },
        clear: function (jq) {
            var checked = jq.find('input:checked:first');
            if (checked.length) {
                checked.prop('checked', false);
                checked.prev().css('background-position', 'center top');
            }
            return jq;
        }
    };

    $.fn.radiolist.defaults = {
        style: STYLE
    };

    if ($.parser && $.parser.plugins) {
        $.parser.plugins.push('radiolist');
    }
})(jQuery);

if ($.fn.datebox) {
    $.fn.datebox.defaults.parser = function (s) {
        if (!s) return new Date();
        var ss = s.split('/');
        var y = parseInt(ss[2], 10);
        var m = parseInt(ss[0], 10);
        var d = parseInt(ss[1], 10);
        if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
            return new Date(y, m - 1, d);
        } else {
            return new Date();
        }
    };
}