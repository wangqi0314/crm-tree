$(function () {
    //页面初始加载事件
    search.event.loading();

    //页面头部加载事件
    search.event.searchHead();

    //设置筛选方式
    search.event.searchQuery();
});
search = {
    base: {
        _url: '/handler/CRM_ReportSearch.aspx',
    },
    event: {
        loading: function () {
            var params = { action: 'SearchCategory', _code: 1 };
            $.post(search.base._url, { o: JSON.stringify(params) }, function (data) {
                if (data != null) {
                    var _html = search.HTML.head.category.gethtml(data);
                    $('.SearchCategory').prepend(_html);
                }
            }, 'json');

            //var paramss = { action: 'getReportQuery', RPS_Code: 1 };
            //$.post(search.base._url, { o: JSON.stringify(paramss) }, function (data) {
            //    if (data != null) {
            //    }
            //}, 'json');
        },
        searchHead: function () {
            $(".SearchCategory").on("click", ".selected-inline li", function (e) {
                $(".SearchCategory .selected-inline li").removeClass("selected border-sub");
                var _o = $(e.target);
                _o.addClass("selected border-sub");
                if (_o.attr("valueIf") != -1) {
                    search.event.loadingSearch(_o.val(), _o.text());
                    $(e.target).attr({ valueIf: -1 });
                }
            });
        },
        loadingSearch: function (id, text) {
            var params = { action: 'CategoryInfo', RF_Code: id };
            $.post(search.base._url, { o: JSON.stringify(params) }, function (data) {
                if (data != null) {
                    var _html = search.HTML.head.categoryInfo.gethtml(text, data);
                    $('.SearchCategory').append(_html);
                }
            }, 'json');
        },
        searchQuery: function () {
            $(".searchQuery").on("click", "input[name=is-Query]", function (e) {
                var _o = $(e.target);
                if (_o.val() == "yes") {
                    $(".searchQuery input[type=checkbox]").attr({ type: "radio" });
                } else {
                    $(".searchQuery input[type=radio]").attr({ type: "checkbox" });
                }
            });
        }
    },
    HTML: {
        head: {
            category: {
                sethtml: function (o) {
                    if (o == null) {
                        return '';
                    }
                    return '<li value=' + o.value + '>' + o.text + '</li>';
                },
                gethtml: function (o) {
                    var _html = '<label class=" text-big" style="display:inline-block;vertical-align:middle">搜索名称 >></label>';
                    if (o == null || o.length <= 0) {
                        return _html;
                    }
                    _html += '<ul class="selected-inline" style="display:inline-block;vertical-align:middle">';
                    for (var i = 0; i < o.length; i++) {
                        _html += this.sethtml(o[i]);
                    }
                    _html += '</ul>';
                    return _html;
                }
            },
            categoryInfo: {
                sethtml: function (o) {
                    if (o == null) {
                        return '';
                    }
                    return '<label><input type="checkbox" /> ' + o.RF_Name + ' </label>';
                },
                gethtml: function (c, o) {
                    var _html = '<hr class="" /> <div class="ca-info">';
                    _html += ' <label><strong>' + c + '：</strong></label>';

                    if (o == null || o.length <= 0) {
                        return _html;
                    }
                    for (var i = 0; i < o.length; i++) {
                        _html += this.sethtml(o[i]);
                    }
                    _html += '</div>';
                    return _html;
                }
            },
        }
    }
};