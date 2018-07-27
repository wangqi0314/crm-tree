var _url_1 = "/handler/CRM_Gridhandle.aspx";

$(function () {
    _pageParam.urlParam = $.getParams();
    _SubGrid._pInit();
});

_SubGrid = {
    //开始的初始化
    _pInit: function () {
        var _code = _pageParam.urlParam.MF_FL_FB_Code;
        if (!(_code > 0)) {
            $.msgTips.info(_isEn ? "No list of Code!" : "无列表代码！");
            return;
        }
        var params = { action: 'DataGrid', _code: _code };
        $.post(_url_1, { o: JSON.stringify(params) }, function (d) {
            _pageParam.List_data = d.List_data;
            var _d = _SubGrid._pTitle.getTitle(d);
            _SubGrid._pTitle.setTitle(_d);
            _SubGrid._pData._pData(_d);
            _pageParam.winID = d._winID;
            top[_pageParam.winID] = window.self;
        }, 'json');
    },
    //父级表格的表头处理；
    _pTitle: {
        getTitle: function (d) {
            return d;
        },
        setTitle: function (d) {
            var $dg = $("#dg_2");
            var dg_config = {
                fit: true, rownumbers: true, pagination: true,
                singleSelect: true, showHeader: true,
                border: false, toolbar: '', loadMsg: 'Loading....',
                remoteSort: false, nowrap: false, fitColumns: true,
                view: detailview, columns: [d.column],
                detailFormatter: function (rowIndex, rowData) {
                    return '<table class="ddv"></table>';
                },
                onExpandRow: function (index, row) {
                    _SubGrid._function.onCollapseRow(index);
                    _SubGrid._cTitle.setTitle(index, row);
                },
                onCollapseRow: function (index, row) {
                }
            };

            $dg.datagrid(dg_config);
            return $dg;
        }
    },
    //父级表格的数据处理； 
    _pData: {
        _pData: function () {
            var $dg = $("#dg_2");
            var params = {
                action: 'getData', isPage: true, _code: _pageParam.urlParam.MF_FL_FB_Code,
                _param: JSON.stringify(_pageParam.urlParam)
            };
            $.post(_url_1, { o: JSON.stringify(params) }, function (d) {
                $dg.datagrid('loadData', d.dataJson);
                var pager = $dg.datagrid("getPager");
                pager.pagination({
                    total: d.SumRow, onSelectPage: function (pageNumber, pageSize) {
                        params = {
                            action: 'getData', isPage: true, _code: _pageParam.urlParam.MF_FL_FB_Code,
                            page: pageNumber, _param: JSON.stringify(_pageParam.urlParam)
                        };
                        $.post(_url_1, { o: JSON.stringify(params) }, function (d2) {
                            $dg.datagrid('loadData', d2.dataJson);
                            var pager = $dg.datagrid("getPager");
                            pager.pagination({
                                total: d2.SumRow,
                                pageNumber: pageNumber
                            });

                        }, 'json');
                    }
                });

            }, 'json');
        }
    },

    //子级表格的表头处理；
    _cTitle: {
        setTitle: function (index, row) {
            if (_pageParam.List_data[0].FL_FL_Code == null) {
                return;
            }
            var $dg = $("#dg_2");
            var params = { action: 'DataGrid', _code: _pageParam.List_data[0].FL_FL_Code };
            $.post(_url_1, { o: JSON.stringify(params) }, function (d) {
                var ddv = $dg.datagrid('getRowDetail', index).find('table.ddv');
                ddv.datagrid({
                    showHeader: true, remoteSort: false, nowrap: false, pagination: false,
                    fitColumns: true, toolbar: '', singleSelect: true, rownumbers: true,
                    loadMsg: 'loading...', height: 'auto', columns: [d.column],
                    onResize: function () {
                        $dg.datagrid('fixDetailRowHeight', index);
                    },
                    onLoadSuccess: function () {
                        setTimeout(function () {
                            $dg.datagrid('fixDetailRowHeight', index);
                        }, 0);
                    },
                    onClickRow: function (index, row) {
                    }
                });

                _SubGrid._cData.setData(index, row);
            }, 'json');
        }
    },

    //子级表格的数据处理
    _cData: {
        setData: function (index, row) {
            var $dg = $("#dg_2");
            var ddv = $dg.datagrid('getRowDetail', index).find('table.ddv');
            var params = {
                action: 'getData', isPage: false,
                _code: _pageParam.List_data[0].FL_FL_Code, _param: JSON.stringify(row)
            };
            $.post(_url_1, { o: JSON.stringify(params) }, function (d) {
                ddv.datagrid('loadData', d);
            }, 'json');
        }
    },

    //各种类型的方法
    _function: {
        onCollapseRow: function (index) {
            var $dg = $("#dg_2");
            var _data = $dg.datagrid("getRows");
            for (var i = 0; i < _data.length; i++) {
                if (i != index) {
                    $dg.datagrid('collapseRow', i);
                }
            }
        }
    }
};
