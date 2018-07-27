
//阻止事件冒泡
var _stopPropagation = function (e) {
    if (!e) {
        return;
    }
    // If stopPropagation exists, run it on the original event
    if (e.stopPropagation) {
        e.stopPropagation();
    }

    // Support: IE
    // Set the cancelBubble property of the original event to true
    e.cancelBubble = true;
}
var MousePostion = {};
var __params = $.getParams();
var __MF_FL_FB_Code = (__params ? __params.MF_FL_FB_Code : 0);
var __download = (__params && __params.dow ? __params.dow : 0);
//--------------------------------------------------------------------------------------
//datagrid（表格）
//--------------------------------------------------------------------------------------
var _datagrid = {
    showCheckBox: true,
    showToolBar: true,
    //默认值配置
    defualts: {
        url: '/handler/Reports/Reports.aspx',
        //窗体唯一编号
        winID: "",
        //列表编号
        MF_FL_FB_Code: __MF_FL_FB_Code,
        //分页默认值
        pager: {
            action: __params.IsProc == 1 ? 'ExecProc' : 'Page',
            pageNumber: 1,
            pageSize: __params.PS ? __params.PS : 10,
            MF_FL_FB_Code: __MF_FL_FB_Code,
            sort: '',
            order: '',
            paramsData: [],
            queryParams: [],
            download: __download
        }
    },
    getWindowConfig: function (rowData, index) {
        var windowConfig = $.extend({}, _datagrid.windowConfigs[index]);
        windowConfig.title = $.formatParams(windowConfig.title, rowData);
        windowConfig.width = windowConfig.width > 0 ? windowConfig.width : 910;
        windowConfig.height = windowConfig.height > 0 ? windowConfig.height : 450;
        return windowConfig;
    },
    //获得弹出窗体参数数据
    getParamsData: function (rowData, params) {
        if (!rowData || !params) { return ""; }

        var a_params = $.trim(params).split(',');
        var o_data = { _winID: _datagrid.defualts.winID };
        $.each(a_params, function (i, n) {
            o_data[n] = rowData[n];
        });

        var params = [];
        $.each(o_data, function (n, v) {
            params.push(n + '=' + v);
        });
        return params.join('&');
    }
};

var _url_params = ['AU_Code', 'HS_Code', 'CG_Code', 'AD_Code'
    , 'DG_Code', 'AP_Code', 'OM_Code', 'EV_Code', 'RS_Code', 'PR'
    , 'P1', 'P2', 'P3', 'P4', 'P5', 'P6', 'P7', 'P8', 'P9', 'P10'

];

$.each(_url_params, function (i, n) {
    var v = __params[n];
    if ($.trim(v) != '' && $.isNumeric(v)) {
        _datagrid.defualts.pager.paramsData.push({ EX_Name: n, EX_Value: v, EX_DataType: 'int' });
    }
});

$.each(['C1', 'C2', 'C3', 'C4', 'C5', 'C6', 'C7', 'C8', 'C9', 'C10', 'AU_Name', 'Cust'], function (i, n) {
    var v = __params[n];
    if ($.trim(v) != '') {
        _datagrid.defualts.pager.paramsData.push({ EX_Name: n, EX_Value: v, EX_DataType: 'string' });
    }
});

//表格动作
_datagrid.action = {
    //刷新当前列表
    refresh: function () {
        var $dg = $("#dg");
        if ($dg) {
            $dg.datagrid('getPager').pagination('select');
        }
        $.msgTips.save(true);
    },
    refresh_del: function () {
        var $dg = $("#dg");
        if ($dg) {
            $dg.datagrid('getPager').pagination('select');
        }
        $.msgTips.remove(true);
    },

    //添加
    add: function (url, params, index, target) {
        if (url) {
            var windowConfig = _datagrid.getWindowConfig(__params, index);

            var paramsData = _datagrid.getParamsData(__params, params);
            if (paramsData) paramsData = (url.indexOf("?") > 0 ? "&" : "?") + paramsData;
            windowConfig.url = url + paramsData;
            windowConfig.url += (windowConfig.url.indexOf("?") > 0 ? "&" : "?") + "action=add&_winID=" + _datagrid.defualts.winID;
            windowConfig.scroll = true;
            _rellocation(target, windowConfig);
            $.topOpen(windowConfig);
        }
    },
    //删除
    remove: function (msg, action) {
        var rowData = this._getRowAndTip();
        if (!rowData) { return; }
        var language = {
            title: { en: 'prompt', cn: '提示' }
        };
        msg = msg ? msg : " ";
        var title = language.title[_isEn ? "en" : "cn"];
        $.messager.confirm(title, msg, function (rs) {
            if (rs) {
                $.mask.show();

                var o = { action: action, o: rowData };
                $.post(_datagrid.defualts.url, { o: JSON.stringify(o) }, function (res) {
                    $.mask.hide();
                    if ($.checkResponse(res, false)) {
                        $.msgTips.cancel(true);
                        _datagrid.action.refresh_del();
                    } else {
                        $.msgTips.cancel(false);
                    }
                }, "json");
            }
        });
    },
    //复制
    copy: function () {
        var rowData = this._getRowAndTip();
        if (!rowData) { return; }
    },
    //编辑
    edit: function (url, params, index, target) {
        var rowData = this._getRowAndTip();
        if (!rowData) { return; }
        if (url) {
            var windowConfig = _datagrid.getWindowConfig(rowData, index);
            var paramsData = _datagrid.getParamsData(rowData, params);
            if (paramsData) paramsData = (url.indexOf("?") > 0 ? "&" : "?") + paramsData;
            windowConfig.url = url + paramsData + '&action=edit';
            windowConfig.scroll = true;
            _rellocation(target, windowConfig);
            $.topOpen(windowConfig);
        }
    },
    //打印
    print: function () {

    },
    //查看
    show: function (url, params, index, target) {
        var rowData = this._getRowAndTip();
        if (!rowData) { return; }
        if (url) {
            var windowConfig = _datagrid.getWindowConfig(rowData, index);
            var paramsData = _datagrid.getParamsData(rowData, params);
            if (paramsData) paramsData = (url.indexOf("?") > 0 ? "&" : "?") + paramsData;
            windowConfig.url = url + paramsData;
            windowConfig.scroll = true;
            _rellocation(target, windowConfig);
            $.topOpen(windowConfig);
        }
    },
    //激活
    activate: function (procName, params, msg) {
        var rowData = this._getRowAndTip();
        if (!rowData) { return; }
        var language = {
            title: { en: 'prompt', cn: '提示' }
        };
        msg = msg ? msg : " ";
        var title = language.title[_isEn ? "en" : "cn"];

        $.messager.confirm(title, msg, function (rs) {
            if (rs) {
            }
        });
        //}
    },
    //不激活9
    deActivate: function () {
        var rowData = this._getRowAndTip();
        if (!rowData) { return; }
    },
    //导出数据
    exportData: function () {
        var dg = $("#dg");
        var rows = dg.datagrid('getRows');
        if (!rows) {
            $.msgTips.noRecord();
            return;
        }
    },
    //自定义按钮
    custom: function (url, params, index, target) {
        var rowData = this._getRowAndTip();
        if (!rowData) { return; }
        if (url) {
            var windowConfig = _datagrid.getWindowConfig(rowData, index);
            var paramsData = _datagrid.getParamsData(rowData, params);
            if (paramsData) paramsData = (url.indexOf("?") > 0 ? "&" : "?") + paramsData;
            windowConfig.url = url + paramsData;
            windowConfig.scroll = true;
            _rellocation(target, windowConfig);
            $.topOpen(windowConfig);
        }
    },
    //选中行
    selectRow: function (e, index) {
        var dg = $("#dg");
        if (dg && dg.length > 0) {
            dg.datagrid('selectRow', index);
        }
        _stopPropagation(e);
    },
    //获得选中行并且没有选择的时候给出提示
    _getRowAndTip: function () {
        var dg = $("#dg");
        var row = dg.datagrid('getSelected');
        if (!row) {
            $.msgTips.select();
        }
        return row;
    }
};
_rellocation = function (target, windowConfig) {
    //Use the parent window location
    var left = 0;
    var top = 0;
    if (target == 1) {
        var $td = $(window.top.document).find(".window");
        $td.each(function () {
            left = $(this).offset().left;
            top = $(this).offset().top;
        });
        if (windowConfig.left == null)
            windowConfig.left = 230;
        else
            windowConfig.left += left;
        if (windowConfig.top == null)
            windowConfig.top = 100;
        else
            windowConfig.top += top;
    }
    else if (target != null && target != 10) {
        var $td;
        if (target == 2) {
            //use the #container location
            $td = $(window.top.document).find("#container");
            left = $td.offset().left;
            top = $td.offset().top;
            windowConfig.left += left;
            windowConfig.top += top;
        }
        else if (target == 3) {
            //use the #cont_box location
            $td = $(window.top.document).find(".cont_box");
            left = $td.offset().left;
            top = $td.offset().top;
            windowConfig.left += left;
            windowConfig.top += top;
        }
        else if (target == 6) {
            $td = $(window.top.document).find("#content");
            left = $td.offset().left;
            top = $td.offset().top;
            windowConfig.left += left;
            windowConfig.top += top;
        }
        else if (target == 5) {
            $td = $(window.top.document).find("iframe");
            left = $td.offset().left;
            top = $td.offset().top;
            windowConfig.left += left;
            windowConfig.top += top;
        }
        else if (target == 4) {
            $td = $(window.top.document).find("frame");
            left = $td.offset().left;
            top = $td.offset().top;
            windowConfig.left += left;
            windowConfig.top += top;
        }
    }
    else if (target == 10 && target != null) {
        left = MousePostion.x ? MousePostion.x : 100;
        top = MousePostion.y ? MousePostion.y : 100;
        windowConfig.left += parseFloat(left);
        windowConfig.top += parseFloat(top);
    }
}
//下拉列表联动事件
function _lendon(options, onLoad) {
    var o = {
        action: 'GetLenDonData',
        up_code: options.up_code,
        rp_code: options.rp_code
    };
    var $c = $("#" + options.next_param);
    $c.combobox('clear');
    if (options.other_param) {
        $(options.other_param).combobox('clear');
    }
    $.post(_datagrid.defualts.url, { o: JSON.stringify(o) }, function (res) {
        var data = [];
        var value = "";
        if ($.checkResponse(res)) {
            data = res;
            if (data.length > 0) {
                value = data[0].value;
                //data[0].selected = true;
            }
        }

        $c.combobox('loadData', data);

        if (value) {
            $c.combobox('select', value);
        }

        if (onLoad && $.isFunction(onLoad)) {
            onLoad.call();
        }
    }, "json");
};

//创建查询栏
_datagrid.createSearchBar = function (res) {
    var $querySearch = $("#querySearch");
    var html = '';
    $.each(res.search, function (i, o) {
        var title = $.trim(_isEn ? o.FS_Title_EN : o.FS_Title_CN);
        var options = $.trim(o.FS_Option);
        options = options ? ' data-options="' + o.FS_Option + '"' : '';
        if (o.FS_Class === 'linkbutton') {
            html += '<div class="container" style="margin-right:5px;"><a id="' + o.FS_Param + '" class="easyui-' + o.FS_Class + '" ' + options + '>' + title + '</a></div>';
        } else {
            title = title ? ('<span class="title">' + title + ':</span>') : '';
            html += '<div class="container">' + title + '<input id="' + o.FS_Param + '" FS_DataType="' + o.FS_DataType + '" class="easyui-' + o.FS_Class + '" ' + options + '/></div>';
        }
    });
    $querySearch.append(html);

    if (res.search.length > 0) {
        $("#toolbar").show();
    }

    $.each(res.search, function (i, o) {
        var opts = {};
        if (o.EX_Data) {
            opts.data = o.EX_Data;
        }

        var $c = $("#" + o.FS_Param);
        $c[o.FS_Class](opts);

        if (o.FS_Class == 'combobox') {
            var defaultValue = $.trim(o.FS_Default);
            window.setTimeout(function () {
                if (defaultValue) {
                    $c.combobox('select', defaultValue);
                }

                var options = $c.combobox('options');
                if (options.editable) {
                    $c.combobox('textbox').click(function () {
                        $c.combobox('showPanel');
                    });
                }
            }, 0);
        }
    });
};

//创建表格
_datagrid.create = function (res) {
    _datagrid.defualts.winID = res._winID;
    top[_datagrid.defualts.winID] = window.self;

    var btns = _datagrid.getBtns(res.buttons);

    //左边列
    var left_cols = _datagrid.getLeftCols(btns);

    //右边列
    var right_cols = _datagrid.getRightCols(btns);

    //列字体大小控制
    _datagrid.setColsFontSize(res.columns);

    //合并所有列
    res.columns = left_cols.concat(res.columns, right_cols);

    //设置标题
    //$("#tb .panel-title").html(res.title);

    //工具栏按钮
    if (btns.top.length > 0) {
        $("#toolbar").show();
        var toolbarBtn = $("#buttons");
        toolbarBtn.html(btns.top.join(''));
    }

    if (btns.top.length == 0 && res.search.length == 0) {
        _datagrid.showToolBar = false;
        $("#toolbar").hide();
    }

    //表格配置
    _datagrid.config(res, btns.rowClick);

    //执行分页
    var sortField = $.trim(res.options.FL_Sort);
    if (sortField != '') {
        _datagrid.defualts.pager.sort = sortField;
    }
    _datagrid.page();

    //分页设置
    _datagrid.pager(res.pageSize);
};
//格式化按钮
_datagrid.formatBtns = function (btns, index) {
    var a_btns = [];
    if ($.isArray(btns)) {
        for (var i = 0, len = btns.length; i < len; i++) {
            a_btns.push(btns[i] ? $.format(btns[i], index) : btns[i]);
        }
    }
    return a_btns;
};

//获得操作按钮
_datagrid.getBtn = function (btn) {
    if (!btn) { return ''; }

    var bf_type = btn.BF_Type;
    var bf_func = btn.BF_Func;
    var o = {
        selectFun: ''
        , clickFun: ''
        , title: ''
        , tip: $.trim(btn.tip)
        , text: (btn.BF_ShowTxt ? $.trim(btn.text) : '')
        , icon: $.trim(btn.BF_Icon)
    };
    if (!btn.BF_ShowTxt) o.title = $.trim(btn.text);

    switch (bf_func) {
        case 0: //Custom
            o.clickFun = "_datagrid.action.custom('" + $.trim(btn.BF_Link) + "','" + $.trim(btn.BF_Param) + "'," + btn._index + "," + btn.BF_Target + ");";
            o.title = o.title ? o.title : 'Custom';
            break;
        case 1: //Add
            _datagrid.showCheckBox = false;
            o.icon = o.icon ? o.icon : 'icon-add';
            if (btn.BF_Param = 'AU,Name,MAU')
                btn.BF_Param = 'AU_Code,AU_Name';
            o.clickFun = "_datagrid.action.add('" + $.trim(btn.BF_Link) + "','" + $.trim(btn.BF_Param) + "'," + btn._index + "," + btn.BF_Target + ");";
            o.title = o.title ? o.title : 'Add';
            break;
        case 2: //Delete
            o.icon = o.icon ? o.icon : 'icon-remove';
            o.clickFun = '_datagrid.action.remove(\'{tip}\',\'' + $.trim(btn.BF_Link) + '\');';
            o.title = o.title ? o.title : 'Delete';
            break;
        case 3: //Copy
            o.icon = o.icon ? o.icon : 'icon-copy';
            o.clickFun = '_datagrid.action.copy();';
            o.title = o.title ? o.title : 'Copy';
            break;
        case 4: //Edit
            _datagrid.showCheckBox = false;
            o.icon = o.icon ? o.icon : 'icon-edit';
            o.clickFun = "_datagrid.action.edit('" + $.trim(btn.BF_Link) + "','" + $.trim(btn.BF_Param) + "'," + btn._index + "," + btn.BF_Target + ");";
            o.title = o.title ? o.title : 'Edit';
            break;
        case 5: //Print
            _datagrid.showCheckBox = false;
            o.icon = o.icon ? o.icon : 'icon-print';
            o.clickFun = '_datagrid.action.print();';
            o.title = o.title ? o.title : 'Print';
            break;
        case 6: //Show Details
            o.icon = o.icon ? o.icon : 'icon-search';
            o.clickFun = "_datagrid.action.show('" + $.trim(btn.BF_Link) + "','" + $.trim(btn.BF_Param) + "'," + btn._index + "," + btn.BF_Target + ");";
            o.title = o.title ? o.title : 'Show Details';
            break;
        case 7: //Activate/Deactivate
            o.icon = o.icon ? o.icon : 'icon-ok';
            o.clickFun = "_datagrid.action.activate('" + $.trim(btn.BF_Link) + "','" + $.trim(btn.BF_Param) + "','{tip}');";
            o.title = o.title ? o.title : 'Activate';
            break;
        case 8: //Export
            _datagrid.showCheckBox = false;
            o.icon = o.icon ? o.icon : 'icon-export';
            o.clickFun = '_datagrid.action.export();';
            o.title = o.title ? o.title : 'Export';
            break;
    }
    if (btn.BF_ShowTxt) o.title = '';

    o.selectFun = (bf_type === 2 || bf_type === 3) ? '_datagrid.action.selectRow(event,{0});' : '';
    var a_btn = [
        '<a class="easyui-linkbutton l-btn l-btn-small l-btn-plain" title="{title}"'
        , ' href="javascript:void(0);" onclick="{selectFun}{clickFun}">'
        , o.icon ? '<span class="l-btn-left l-btn-icon-left">' : '<span class="l-btn-left">'
        , o.text ? '<span class="l-btn-text">{text}</span>' : '<span class="l-btn-text l-btn-empty">&nbsp;</span>'
        , o.icon ? '<span class="l-btn-icon {icon}">&nbsp;</span>' : ''
        , '</span></a>'
    ];
    var btn = $.format(a_btn.join(''), o);
    return btn;
};
var _BF_Targets = null;
_datagrid.windowConfigs = [];
//获得操作按钮组
_datagrid.getBtns = function (buttons) {
    var btns = {
        left: [],
        right: [],
        top: [],
        rowClick: {}
    };

    if (buttons && buttons.length > 0) {
        $.each(buttons, function (i, btn) {
            var o_gw = {
                title: btn.GW_Title,
                width: btn.GW_Width,
                height: btn.GW_Height,
                top: btn.GW_X,
                left: btn.GW_Y
            };

            btn._index = i;
            if (btn.BF_Target != null)
                _BF_Targets = btn.BF_Target;
            _datagrid.windowConfigs.push(o_gw);
            if (btn.b_visible) {
                var bf_type = btn.BF_Type;
                switch (bf_type) {
                    case 1:
                        btns.rowClick = btn;
                        break;
                    case 2:
                        btns.left.push(_datagrid.getBtn(btn));
                        break;
                    case 3:
                        btns.right.push(_datagrid.getBtn(btn));
                        break;
                    case 4:
                        btns.top.push(_datagrid.getBtn(btn));
                        break;
                }
            }
        });
    }

    return btns;
};
//左边列
_datagrid.getLeftCols = function (btns) {
    var left_cols = [];
    //增加复选框列
    if (_datagrid.showCheckBox && btns.top.length > 0) {
        left_cols.push({ field: 'ck', checkbox: true });
    }
    //增加按钮列
    if (btns && btns.right && btns.left.length > 0) {
        left_cols.push({
            field: 'btnFirst', align: 'center', width: btns.left.length * 30
            , formatter: function (value, row, index) {
                return _datagrid.formatBtns(btns.left, index).join('');
            }
        });
    }
    return left_cols;
};
//右边列
_datagrid.getRightCols = function (btns) {
    var right_cols = [];
    //增加按钮列
    if (btns && btns.right && btns.right.length > 0) {
        right_cols.push({
            field: 'btnLast', align: 'center', width: btns.right.length * 30
            , formatter: function (value, row, index) {
                return _datagrid.formatBtns(btns.right, index).join('');
            }
        });
    }
    return right_cols;
};
//列字体大小控制
_datagrid.setColsFontSize = function (columns) {
    $.each(columns, function (i, c) {
        var fontSize = c.FN_Font > 0 ? c.FN_Font : 12;
        if (fontSize !== 12) {
            c.formatter = function (value, row, index) {
                return '<div style="font-size:' + fontSize + 'px;margin:0;padding:0;">' + value + '</div>';;
            }
        }
        if (c.FN_Format === 30) {
            c.formatter = function (value, row, index) {
                return value ? '<div style="height:100%;width:100%;background:url(' + value + ') no-repeat center center;">&nbsp;</div>' : '';
            }
        }
    });
};
//配置表格
_datagrid.config = function (res, rowClick) {
    var optioins = res.options;

    var $dg = $("#dg");
    //设置标题和列头
    var dg_config = {
        url: null,
        fit: true,
        border: false,
        toolbar: _datagrid.showToolBar ? '#toolbar' : '',
        pagination: __params.IsProc == 1 ? false : true,
        rownumbers: true,
        singleSelect: true,
        nowrap: (optioins.FL_Type == 1 || optioins.FL_Type == 11) ? false : true,
        loadMsg: '',
        columns: [res.columns],
        //排序事件
        onBeforeSortColumn: function (sort, order) {
            _datagrid.defualts.pager.sort = sort;
            _datagrid.defualts.pager.order = order;
            _datagrid.page();
        }
    };

    //行点击事件
    if (!$.isEmptyObject(rowClick)) {
        dg_config.onClickRow = function (rowIndex, rowData) {
            var url = $.trim(rowClick.BF_Link);
            var windowConfig = _datagrid.getWindowConfig(rowData, rowClick._index);
            var paramsData = _datagrid.getParamsData(rowData, rowClick.BF_Param);
            if (paramsData) paramsData = (url.indexOf("?") > 0 ? "&" : "?") + paramsData;
            windowConfig.url = url + paramsData;
            _rellocation(rowClick.BF_Target, windowConfig);
            $.topOpen(windowConfig);
        }
    }
    $dg.datagrid(dg_config);
    return $dg;
};
//分页
_datagrid.page = function () {
    var $dg = $("#dg");
    $.mask.show();

    var params = $.form.getQueryParams("#querySearch");
    _datagrid.defualts.pager.queryParams = params.concat(_datagrid.defualts.pager.paramsData);
    //CT
    var isCT = false;
    $.each(_datagrid.defualts.pager.queryParams, function (i, qp) {
        if (qp.EX_Name == 'CT') {
            isCT = true;
            if ((qp.EX_Value == 0 || $.trim(qp.EX_Value) == '') && __params.CT > 0) {
                qp.EX_Value = __params.CT;
            }
            return false;
        }
    });
    if (!isCT && __params.CT > 0) {
        var ct = {
            EX_Name: 'CT'
            , EX_Value: __params.CT
            , EX_DataType: 'int'
        };
        _datagrid.defualts.pager.queryParams.push(ct);
    }

    $.post(_datagrid.defualts.url, { o: JSON.stringify(_datagrid.defualts.pager) }, function (res) {
        $.mask.hide();
        if (!$.checkResponse(res)) {
            $dg.datagrid('loadData', []);
            $.msgTips.info(_isEn ? 'No record!' : '没有记录！');
            return;
        }

        var row = $dg.datagrid('getSelected');
        var rowIndex = -1;
        if (row) {
            rowIndex = $dg.datagrid('getRowIndex', row);
        }

        for (var i = 0, len = res.rows.length; i < len; i++) {
            var dataRow = res.rows[i];
            $.each(dataRow, function (n, d) {
                if (d === null) {
                    dataRow[n] = "";
                }
            });
        }
        
        $dg.datagrid('loadData', res);

        if (rowIndex >= 0) {
            $dg.datagrid('selectRow', rowIndex).datagrid('scrollTo', rowIndex);
        }

        if (res.rows.length == 0) {
            $.msgTips.info(_isEn ? 'No record!' : '没有记录！');
        }
    }, "json");
};
//分页条设置
_datagrid.pager = function (pgSize) {
    var $dg = $("#dg");
    var pager = $dg.datagrid("getPager");
    var opts = $dg.datagrid('options');
    
    pager.pagination({
        pageSize: __params.PS ? __params.PS : (pgSize ? pgSize : 10),
        
        //分页事件
        onSelectPage: function (pageNumber, pageSize) {
            if (pageNumber !== opts.pageNumber) {
                $dg.datagrid('unselectAll');
            }
            //表格行号设置
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            opts.pageNumber = pageNumber;
            opts.pageSize = pageSize;

            //分页参数设置
            _datagrid.defualts.pager.pageNumber = pageNumber;
            _datagrid.defualts.pager.pageSize = pageSize;

            //执行分页
            _datagrid.page();
        }
    });
}
//初始化
$(function () {
    $(top.window).mousemove(function (event) {
        MousePostion.x = 0;
        MousePostion.y = 0;

        var e = event || window.event;
        var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft;
        var scrollY = document.documentElement.scrollTop || document.body.scrollTop;
        MousePostion.x = e.pageX || e.clientX + scrollX;
        MousePostion.y = e.pageY || e.clientY + scrollY;
    });
    var MF_FL_FB_Code = _datagrid.defualts.MF_FL_FB_Code;
    if (!(MF_FL_FB_Code > 0)) {
        $.msgTips.info(_isEn ? "No list of Code!" : "无列表代码！");
        return;
    }
    //表格按钮NB=1不查询按钮
    //var nb = $.trim(__params.NB) == '' ? 0 : __params.NB;
    var params = { action: 'DataGrid', MF_FL_FB_Code: MF_FL_FB_Code, NB: __params.NB };
    $.post(_datagrid.defualts.url, { o: JSON.stringify(params) }, function (res) {
        if (!$.checkResponse(res)) {
            return;
        }
        var kkk = -1;
        $.each(res.columns, function (i, o) {
            kkk++;
            if (o.FN_Type == 50) {
                var T_title = res.columns[kkk].title;
                var T_field = res.columns[kkk].field;
                var T_sortable = res.columns[kkk].sortable;
                var T_width = res.columns[kkk].width;
                var T_FN_Format = res.columns[kkk].FN_Format;
                var T_FN_Font = res.columns[kkk].FN_Font;
                var T_halign = res.columns[kkk].halign;
                var T_align = res.columns[kkk].align;
                res.columns[kkk] = { title: T_title + 1, field: T_field + 1, sortable: 0, width: T_width, FN_Format: T_FN_Format, FN_Font: T_FN_Font, halign: T_halign, align: T_align, FN_Type: 50 };
                for (var j = 2; j < res.FN_Type50[0].Max_Col + 2; j++) {
                    kkk++;
                    console.log(res.columns.join());
                    res.columns.splice(kkk, 0, { title: T_title + j, field: T_field + j, sortable: 0, width: T_width, FN_Format: T_FN_Format, FN_Font: T_FN_Font, halign: T_halign, align: T_align, FN_Type: 50 });
                    console.log(res.columns.join());
                }
            }
        });
        
        //创建查询栏
        _datagrid.createSearchBar(res);

        //创建表格
        _datagrid.create(res);
    }, "json");
});