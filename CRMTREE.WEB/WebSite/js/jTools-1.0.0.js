/*
 * jTools Javascript Library v1.0.0
 */
(function (window, undefined) {

    var version = "1.0.0 Build 201106171039", // 版本号
	globalName = "jTools"; 	// 全局标识符

    // 防止重复加载
    if (window[globalName] && window[globalName].version >= version) { return; }

    var _$ = window.$; 	// 记录当前框架，以便恢复
    var document = window.document;

    /// @overload 根据CSS选择器和上下文匹配出HTML元素
    ///		@param {String} CSS选择器
    ///		@param {HTMLElement,Array,HTMLCollection} 上下文
    ///		@return {HTMLElement,Array} 匹配到的经扩展的HTML元素
    /// @overload 扩展HTML元素
    ///		@param {HTMLElement,Array,HTMLCollection} 要扩展的Html元素
    ///		@return {HTMLElement,Array} 经扩展的HTML元素
    var jTools = window[globalName] = window.$ = function (selector, context) {
        if (!selector) { return selector; }

        "string" == typeof selector && (selector = selectorQuery.exec(selector, context || document));

        return extendElems(selector);
    };

    /// 根据上下文及CSS选择器获取结果集中的第一个元素
    /// @param {String} CSS选择器
    /// @param {HTMLElement,Array,HTMLCollection} 上下文
    ///	@return {HTMLElement} 匹配到的经扩展的HTML元素
    jTools.one = function (selector, context) {
        var result = jTools(selector, context);
        return result && !result.nodeType ? extendElems(result[0]) : result;
    };

    /// 根据上下文及CSS选择器获取所有元素
    /// @param {String} CSS选择器
    /// @param {HTMLElement,Array,HTMLCollection} 上下文
    ///	@return {Array} 匹配到的经扩展的HTML元素数组
    jTools.all = function (selector, context) {
        var result = jTools(null == selector ? [] : selector) || [];
        return result.nodeType ? extendElems([result]) : result;
    };

    // 扩展HTML元素(数组)
    // @param {HTMLElement,Array,HTMLCollection} 元素(数组)
    // @return {HtmlElement,Array} 扩展后的元素
    function extendElems(elems) {
        if (elems && !elems[globalName]) {
            if (elems.nodeType) {	// 扩展Html元素和非IE下的XML元素
                if ("unknown" !== typeof elems.getAttribute) {
                    for (var p in jTools.element) {
                        // 不覆盖原有的属性和方法
                        undefined === elems[p] && (elems[p] = jTools.element[p]);
                    }
                }
            } else {	// HTMLCollection Or Array
                elems = jTools.util.extend(jTools.util.toArray(elems), jTools.element);
            }
        }
        return elems;
    };

    /// 标识当前版本
    jTools.version = version;

    /// 恢复本类库对$和jTools全局变量的占用
    /// @return {Object} jTools对象
    jTools.resume = function () {
        _$ = window.$;
        window.$ = window[globalName] = jTools;
        return jTools;
    };

    /// 恢复最近一次本类库加载前或jTools.resume方法调用前的$变量
    /// @return {Mixed} 原$变量
    jTools.retire = function () {
        window.$ = _$;
        return _$;
    };

    /// 添加插件
    /// @param {String} 名字空间
    /// @param {Mixed} 插件
    /// @param {String} 插件版本号
    /// @ex:jTools.addPlugin("ui", {}, "1.0.0 Build 201106171316")
    jTools.addPlugin = function (namespace, plugin, version) {
        if (jTools[namespace] && jTools[namespace].version >= version) { return; }

        plugin.version = version;
        jTools[namespace] = plugin;

        return plugin;
    };

    var isNaN = window.isNaN,
	Math = window.Math,
	testElem = document.createElement("div"); // 用于特性检查的元素

    testElem.innerHTML = "<p class='TEST'></p>";


    // selectorQuery选择器解析引擎
    var selectorQuery = {

        SPACE: /\s*([\s>~+,])\s*/g, // 用于去空格
        ISSIMPLE: /^#?[\w\u00c0-\uFFFF_-]+$/, 	// 判断是否简单选择器(只有id或tagname，不包括*)
        IMPLIEDALL: /([>\s~\+,]|^)([#\.\[:])/g, 	// 用于补全选择器
        ATTRVALUES: /=(["'])([^'"]*)\1]/g, 		// 用于替换引号括起来的属性值
        ATTR: /\[\s*([\w\u00c0-\uFFFF_-]+)\s*(?:(\S?\=)\s*(.*?))?\s*\]/g, // 用于替换属性选择器
        PSEUDOSEQ: /\(([^\(\)]*)\)$/g, 	// 用于匹配伪类选择器最后的序号
        BEGINIDAPART: /^(?:\*#([\w\u00c0-\uFFFF_-]+))/, 	// 用于分离开头的id选择器
        STANDARD: /^[>\s~\+:]/, 	// 判断是否标准选择器(以空格、>、~或+开头)
        STREAM: /[#\.>\s\[\]:~\+]+|[^#\.>\s\[\]:~\+]+/g, // 用于把选择器表达式分离成操作符/操作数 数组
        ISINT: /^\d+$/, // 判断是否整数

        // 判断是否使用浏览器的querySelectorAll
        enableQuerySelector: testElem.querySelectorAll && testElem.querySelectorAll(".TEST").length > 0,

        tempAttrValues: [], // 临时记录引号/双引号间的属性值
        tempAttrs: [], 	// 临时记录属性表达式

        idName: globalName + "UniqueId",
        id: 0,

        // 解析CSS选择器获取元素
        // @param {String} 选择器
        // @param {HTMLElement,Array,HTMLCollection} 上下文
        // @return {HTMLElement,Array,HTMLCollection} 匹配到的元素
        exec: function (selector, context) {

            var result, 	// 最后结果
			selectors, 	// selector数组
			selCount, 	// selector数组长度
			i, j, 		// 循环变量
			temp, 		// 临时搜索结果
			matchs, 	// 操作符/操作数 数组
			streamLen, 	// 操作符/操作数 数组长度
			token, 		// 操作符
			filter, 	// 操作数
			t = this;

            // 清除多余的空白
            selector = selector.trim();

            if ("" === selector) { return; }

            // 对简单选择符的优化操作
            if (t.ISSIMPLE.test(selector)) {
                if (0 === selector.indexOf("#") && typeof context.getElementById !== "undefined") {
                    //alert("simple id: " + selector);	// @debug
                    return t.getElemById(context, selector.substr(1));
                } else if (typeof context.getElementsByTagName !== "undefined") {
                    //alert("simple tagname: " + selector);	// @debug
                    return jTools.util.toArray(context.getElementsByTagName(selector));
                }
            }

            // 使用querySelectorAll
            if (t.enableQuerySelector && context.nodeType) {
                try {
                    return jTools.util.toArray(context.querySelectorAll(selector));
                } catch (e) {

                }
            }

            // 转换成数组，统一处理
            context = context.nodeType ? [context] : jTools.util.toArray(context);

            selectors = selector.replace(t.SPACE, "$1")		// 去空白
						.replace(t.ATTRVALUES, t.analyzeAttrValues)	// 替换属性值
						.replace(t.ATTR, t.analyzeAttrs)	// 替换属性选择符
						.replace(t.IMPLIEDALL, "$1*$2")		// 添加必要的"*"(例如.class1 => *.class1)
						.split(","); // 分离多个选择器
            selCount = selectors.length;

            i = -1; result = [];

            while (++i < selCount) {
                // 重置上下文
                temp = context;

                selector = selectors[i];

                if (t.BEGINIDAPART.test(selector)) {	// 优化以id选择器开头且上下文是document的情况
                    if (typeof context[0].getElementById !== "undefined") {
                        //alert("begin with id selector: " + RegExp.$1);	// @debug
                        temp = [t.getElemById(context[0], RegExp.$1)];
                        //alert("result: " + temp); // @debug
                        if (!temp[0]) {
                            continue;
                        }
                        selector = RegExp.rightContext;
                    } else {	// 上下文不是document, 恢复常规查找
                        selector = selectors[i];
                    }
                }

                // 处理后续的部分
                if (selector !== "") {
                    if (!t.STANDARD.test(selector)) {
                        selector = " " + selector;
                    }

                    // 分离换成字符串数组，从0开始双数是操作符，单数是操作数(例如 " *.class1" => [" ", "*", ".", "class1"])
                    matchs = selector.match(t.STREAM) || []; streamLen = matchs.length; j = 0;
                    //alert("stream: " + matchs);	// @debug
                    while (j < streamLen) {
                        token = matchs[j++]; filter = matchs[j++];
                        //alert(token + (this.operators[token] ? " is " : " is not ") + "exist"); 	// @debug
                        //alert("filter: " + filter);	// @debug
                        //alert("context: " + temp);	// @debug
                        temp = t.operators[token] ? t.operators[token](temp, filter) : [];
                        if (0 === temp.length) {
                            break;
                        }
                    }
                }

                jTools.util.merge(result, temp);
            }

            // 清空临时数组
            t.tempAttrValues.length = t.tempAttrs.length = 0;

            return result.length > 1 ? t.unique(result) : result;
        },

        // 属性替换处理函数
        analyzeAttrs: function ($1, $2, $3, $4) {
            return "[]" + (selectorQuery.tempAttrs.push([$2, $3, $4]) - 1);
        },

        // 属性值替换处理函数
        analyzeAttrValues: function ($1, $2, $3) {
            return "=" + (selectorQuery.tempAttrValues.push($3) - 1) + "]";
        },

        // 获取不重复的元素id
        // @param {HTMLElement} 元素
        // @return {Number} id
        generateId: function (elem) {
            var idName = this.idName, id;
            try {
                id = elem[idName] = elem[idName] || new Number(++this.id);
            } catch (e) {
                id = elem.getAttribute(idName);
                if (!id) {
                    id = new Number(++this.id);
                    elem.setAttribute(idName, id);
                }
            }
            return id.valueOf();
        },

        // 去除数组中的重复元素
        // @param {Array} 元素数组
        // @return {Array} 已去重复的元素数组
        unique: function (elems) {
            var result = [], i = 0, flags = {}, elem, id;
            while (elem = elems[i++]) {
                if (1 === elem.nodeType) {
                    id = this.generateId(elem);
                    if (!flags[id]) {
                        flags[id] = true;
                        result.push(elem);
                    }
                }
            }
            return result;
        },

        // 属性名映射
        attrMap: {
            "class": "className",
            "for": "htmlFor"
        },

        // 获取元素属性
        // @param {HTMLElement} 元素
        // @param {String} 属性名
        // @return {String} 属性值
        getAttribute: function (elem, attrName) {
            var trueName = this.attrMap[attrName] || attrName, attrValue = elem[trueName];
            if ("string" !== typeof attrValue) {
                if ("undefined" !== typeof elem.getAttributeNode) {
                    attrValue = elem.getAttributeNode(attrName);
                    attrValue = undefined == attrValue ? attrValue : attrValue.value;
                } else if (elem.attributes) {		// for IE5.5
                    attrValue = String(elem.attributes[attrName]);
                }
            }
            return null == attrValue ? "" : attrValue;
        },

        // 通过id获取元素
        // @param {HTMLElement} 上下文，一般是document
        // @param {String} id
        // @return {HTMLElement} 元素
        getElemById: function (context, id) {
            var result = context.getElementById(id);
            if (result && result.id !== id && context.all) {	// 修复IE下的id/name bug
                result = context.all[id];
                if (result) {
                    result.nodeType && (result = [result]);
                    for (var i = 0; i < result.length; i++) {
                        if (this.getAttribute(result[i], "id") === id) {
                            return result[i];
                        }
                    }
                }
            } else {
                return result;
            }
        },

        // 搜索指定位置的某标签名元素
        // @param {Array} 上下文
        // @param {String} 第一个元素相对位置
        // @param {String} 下一个元素相对位置
        // @param {String} 标签名
        // @param {Number} 最多进行多少次查找
        // @return {Array} 搜索结果
        getElemsByTagName: function (context, first, next, tagName, limit) {
            var result = [], i = -1, len = context.length, elem, counter, tagNameUpper;
            tagName !== "*" && (tagNameUpper = tagName.toUpperCase());

            while (++i < len) {
                elem = context[i][first]; counter = 0;
                while (elem && (!limit || counter < limit)) {
                    if (1 === elem.nodeType) {
                        (elem.nodeName.toUpperCase() === tagNameUpper || !tagNameUpper) && result.push(elem);
                        counter++;
                    }
                    elem = elem[next];
                }
            }

            return result;
        },

        // 根据指定顺序检查上下文父元素的第n个子元素是否该上下文元素
        // @param {Array} 上下文
        // @param {Number} 序号
        // @param {String} 第一个元素相对位置
        // @param {String} 下一个元素相对位置
        // @return {Array} 搜索结果
        checkElemPosition: function (context, seq, first, next) {
            var result = [];
            if (!isNaN(seq)) {
                var len = context.length, i = -1,
				cache = {}, 	// 节点缓存
				parent, id, current, child;

                while (++i < len) {
                    parent = context[i].parentNode; 	// 找到父节点
                    id = this.generateId(parent); 	// 为父节点生成一个id作为缓存键值

                    if (undefined === cache[id]) {	// 如果缓存中没有，则重新寻找父元素的第N个子元素
                        current = 0; 		// 重置当前序号
                        child = parent[first]; // 第一个元素
                        while (child) {
                            1 === child.nodeType && current++; // 序号加1
                            if (current < seq) {
                                child = child[next]; // 还没到指定序号，继续找
                            } else {
                                break; // 已经到指定序号，中断循环
                            }
                        }
                        cache[id] = child || 0; 	// 记下本次搜索结果
                    } else {
                        child = cache[id];
                    }
                    context[i] === child && result.push(context[i]); // 搜索结果与节点相符
                }
            }
            return result;
        },

        // 获取特定位置的元素
        // @param {Array} 上下文
        // @param {Number} 第一个位置
        // @param {Number} 下一个位置递增量
        // @return {Array} 过滤结果
        getElemsByPosition: function (context, first, next) {
            var i = first, len = context.length, result = [];
            while (i >= 0 && i < len) {
                result.push(context[i]);
                i += next;
            }
            return result;
        },

        // 根据属性值过滤元素
        // @param {Array} 上下文
        // @param {Array} 属性数组
        // @return {Array} 过滤结果
        getElemsByAttribute: function (context, filter) {
            var result = [], elem, i = 0,
			check = this.attrOperators[filter[1] || ""],
			attrValue = "~=" === filter[1] ? " " + filter[2] + " " : filter[2];
            if (check) {
                while (elem = context[i++]) {
                    check(this.getAttribute(elem, filter[0]), attrValue) && result.push(elem);
                }
            }
            return result;
        },

        // 操作符
        operators: {

            // id选择符
            "#": function (context, id) {
                return selectorQuery.getElemsByAttribute(context, ["id", "=", id]);
            },

            // 后代选择符
            " ": function (context, tagName) {
                var len = context.length;
                if (1 === len) {
                    return context[0].getElementsByTagName(tagName);
                } else {
                    var result = [], i = -1;
                    while (++i < len) {
                        jTools.util.merge(result, context[i].getElementsByTagName(tagName));
                    }
                    return result;
                }
            },

            // 类名选择器
            ".": function (context, className) {
                return selectorQuery.getElemsByAttribute(context, ["class", "~=", className]);
            },

            // 子元素选择符
            ">": function (context, tagName) {
                return selectorQuery.getElemsByTagName(context, "firstChild", "nextSibling", tagName);
            },

            // 同级元素选择符
            "+": function (context, tagName) {
                return selectorQuery.getElemsByTagName(context, "nextSibling", "nextSibling", tagName, 1);
            },

            // 同级元素选择符
            "~": function (context, tagName) {
                return selectorQuery.getElemsByTagName(context, "nextSibling", "nextSibling", tagName);
            },

            // 属性选择符
            "[]": function (context, filter) {
                filter = selectorQuery.tempAttrs[filter];
                if (filter) {
                    if (selectorQuery.ISINT.test(filter[2])) {
                        filter[2] = selectorQuery.tempAttrValues[filter[2]];
                    }
                    return selectorQuery.getElemsByAttribute(context, filter);
                } else {
                    return context;
                }
            },

            // 伪类选择符
            ":": function (context, filter) {
                var seq;
                if (selectorQuery.PSEUDOSEQ.test(filter)) {
                    seq = parseInt(RegExp.$1);
                    filter = RegExp.leftContext;
                }
                return selectorQuery.pseOperators[filter] ? selectorQuery.pseOperators[filter](context, seq) : [];
            }
        },

        // 属性操作符
        attrOperators: {

            // 是否包含指定属性值
            "": function (value) { return value !== ""; },

            // 是否与指定属性值相等
            "=": function (value, input) { return input === value; },

            // 是否包含指定属性值
            "~=": function (value, input) { return (" " + value + " ").indexOf(input) >= 0; },

            // 是否与指定属性值不等
            "!=": function (value, input) { return input !== value; },

            // 属性值是否以某段字符串开头
            "^=": function (value, input) { return value.indexOf(input) === 0; },

            // 属性值是否以某段字符串结尾
            "$=": function (value, input) { return value.substr(value.length - input.length) === input; },

            // 属性值是否包含某段子字符串
            "*=": function (value, input) { return value.indexOf(input) >= 0; }
        },

        // 伪类选择符
        pseOperators: {

            // 获取第一个子元素
            "first-child": function (context) {
                return selectorQuery.checkElemPosition(context, 1, "firstChild", "nextSibling");
            },

            // 获取第n个子元素
            "nth-child": function (context, seq) {
                return selectorQuery.checkElemPosition(context, seq, "firstChild", "nextSibling");
            },

            // 获取最后一个子元素
            "last-child": function (context) {
                return selectorQuery.checkElemPosition(context, 1, "lastChild", "previousSibling");
            },

            // 获取倒数第n个子元素
            "nth-last-child": function (context, seq) {
                return selectorQuery.checkElemPosition(context, seq, "lastChild", "previousSibling");
            },

            // 获取第奇数个元素
            "odd": function (context) {
                return selectorQuery.getElemsByPosition(context, 0, 2);
            },

            // 获取第偶数个元素
            "even": function (context) {
                return selectorQuery.getElemsByPosition(context, 1, 2);
            },

            // 获取第N个元素前的元素
            "lt": function (context, seq) {
                return selectorQuery.getElemsByPosition(context, seq - 1, -1);
            },

            // 获取第N个元素后的元素
            "gt": function (context, seq) {
                return selectorQuery.getElemsByPosition(context, seq + 1, 1);
            }
        }
    }; //end selectorQuery选择器解析引擎


    // HTML元素扩展操作，用于继承
    jTools.element = {

        /// 获取指定序号的元素
        /// @param {Number} 序号
        /// @return {HTMLElement} 元素
        get: function (i) { return jTools.dom.single(this, i); },

        /// @overload 获取指定序号并经过扩展的元素
        /// 	@param {Number} 序号索引
        ///		@return {HTMLElement} 指定序号并经过扩展的元素
        /// @overload 以当前元素为上下文通过CSS选择器获取元素
        ///		@param {String} CSS选择器
        ///		@return {HTMLElement,Array} 匹配到的经扩展的HTML元素
        $: function (selector) {
            return jTools("number" === typeof selector ? this.get(selector) : selector, this);
        },

        /*/// 获取当前第一个元素的父元素
        /// @return {HTMLElement} 当前元素的父元素
        parent : function() {
        var node = this.get(0);
        if (node) {
        node = node.parentNode;
        if (node) { return jTools(node); }
        }
        },*/

        /// 检查当前元素是否包含某些样式类
        /// @param {String} 样式类名
        /// @return {Boolean} 元素是否包含某个样式类
        hasClass: function (className) { return jTools.style.hasClass(this, className); },

        /// 添加样式
        /// @param {String,Object} 类名或样式，多个类名用空格隔开
        /// @return {HTMLElement,Array} 当前元素
        addCss: function (css) { return jTools.style.addCss(this, css); },

        /// 移除样式
        /// @param {String,Object} 类名或样式，多个类名用空格隔开
        /// @return {HTMLElement,Array} 当前元素
        removeCss: function (css) { return jTools.style.removeCss(this, css); },

        /// @overload 获取当前第一个元素的样式值
        ///		@param {String} 样式名
        ///		@return {String} 样式值
        /// @overload 设置当前元素的样式值
        ///		@param {String} 样式名
        ///		@param {String} 样式值
        ///		@return {HTMLElement,Array} 当前元素
        css: function (name, val) {
            var temp;
            if (arguments.length > 1) {
                temp = {};
                temp[name] = val;
                return this.addCss(temp);
            } else {
                temp = this.get(0);
                return temp ? jTools.style.getCurrentStyle(temp, name) : undefined;
            }
        },

        /// 添加事件委托函数
        /// @param {String} 事件名，多个事件用逗号隔开
        /// @param {Function} 事件委托函数
        /// @param {Mixed} 额外数据
        /// @return {HTMLElement,Array} 当前元素
        addEvent: function (eventName, handler, data) {
            return jTools.event.addEvent(this, eventName, handler, data);
        },

        /// 移除事件委托函数
        /// @param {String} 事件名，多个事件用逗号隔开
        /// @param {Function} 事件委托函数
        /// @return {HTMLElement,Array} 当前元素
        removeEvent: function (eventName, handler) {
            return jTools.event.removeEvent(this, eventName, handler);
        },

        /// @overload 获取当前元素的属性值
        ///		@param {String} 属性名
        ///		@return {Mixed} 属性值
        /// @overload 设置当前元素的属性值
        ///		@param {String} 属性名
        ///		@param {String,Function} 属性值或用于计算属性值的函数
        ///		@return {HTMLElement,Array} 当前元素
        attr: function (name, value) {
            var t = this;
            name = selectorQuery.attrMap[name] || name;
            if (value !== undefined) {
                return jTools.dom.eachNode(t, function (name, value) {
                    this[name] = jTools.util.isFunction(value) ? value.call(this) : value;
                }, arguments);
            } else {
                var elem = this.get(0);
                return elem ? elem[name] : undefined;
            }
        },

        /// 对每个节点执行特定操作
        /// @param {Function} 要执行的操作
        /// @return {HTMLElement, Array} 当前元素
        each: function (callback) { return jTools.dom.eachNode(this, callback); }
    };

    jTools.element[globalName] = jTools.element.$;

    // window对象、document对象的添加、移除事件方法
    window.addEvent = document.addEvent = jTools.element.addEvent;
    window.removeEvent = document.removeEvent = jTools.element.removeEvent;

    var tplCache = {}, 	// 模板缓存
	slice = Array.prototype.slice,
	toString = Object.prototype.toString;


    /// 工具函数
    jTools.util = {

        /// 把值集合转换成GET字符串
        /// @param {Object} 值集合
        /// @return {String} GET字符串
        toQueryString: function (values) {
            if (values) {
                var str = [];
                for (var n in values) {
                    values[n] != null && str.push(n + "=" + window.encodeURIComponent(values[n]));
                }
                return str.join("&").replace(/%20/g, "+");
            }
        },

        /// 检查变量是否Array类型
        /// @param {Mixed} 待测变量
        /// @return {Boolean} 待测变量是否Array类型
        isArray: function (value) { return toString.call(value) === "[object Array]"; },

        /// 检查变量是否函数类型
        /// @param {Mixed} 待测变量
        /// @return {Boolean} 待测变量是否Function类型
        isFunction: function (value) { return toString.call(value) === "[object Function]"; },

        /// 检查变量是否Object
        /// @param {Mixed} 待测变量
        /// @return {Boolean} 待测变量是否Object
        isObject: function (value) { return toString.call(value) === "[object Object]"; },

        /// 检查对象是否空对象
        /// @Param {Object} 待测对象
        /// @param {Boolean} 待测对象是否空对象
        isEmptyObject: function (value) {
            for (var i in value) {
                return false;
            }
            return true;
        },

        /// 把集合转换为数组
        /// @param {Array,Collection} 数组或集合
        /// @return {Array} 数组
        toArray: function (col) {
            if (jTools.util.isArray(col)) { return col; }

            var arr;
            try {
                arr = slice.call(col);
            } catch (e) {
                arr = [];
                var i = col.length;
                while (i) {
                    arr[--i] = col[i];
                }
            }
            return arr;
        },

        /// 合并数组
        /// @param {Array} 目标数组
        /// @param {Array,Collection} 源数组
        /// @return {Array} 混合后的目标数组
        merge: function (first, second) {
            var i = second.length, pos = first.length;
            while (--i >= 0) {
                first[pos + i] = second[i];
            }
            return first;
        },

        /// 模板转换
        /// @param {String} 模板代码
        /// @param {Object} 值集合
        /// @param {Boolean} 是否缓存模板，默认为是
        /// @return {String} 转换后的代码
        parseTpl: function (tpl, values, isCached) {
            if (null == tpl) { return; }
            if (null == values) { return tpl; }

            var fn = tplCache[tpl];
            if (!fn) {
                fn = new Function("obj", "var _=[];with(obj){_.push('" +
					tpl.replace(/[\r\t\n]/g, " ")
					.replace(/'(?=[^#]*#>)/g, "\t")
					.split("'").join("\\'")
					.split("\t").join("'")
					.replace(/<#=(.+?)#>/g, "',$1,'")
					.split("<#").join("');")
					.split("#>").join("_.push('")
					+ "');}return _.join('');");
                isCached !== false && (tplCache[tpl] = fn);
            }

            return fn(values);
        },

        /// 把源对象的属性和方法扩展到目标对象上
        /// @param {Mixed} 目标对象，如果目标对象为null，则新建一个对象
        /// @param {Mixed} 源对象
        /// @return {Mixed} 已扩展的目标对象
        extend: function (des, src) {
            if (src != null) {
                null == des && (des = {});
                for (var i in src) {
                    des[i] = src[i];
                }
            }

            return des;
        },

        /// 复制JSON
        /// @param {Object} 源JSON，对于JSON不支持的形式（Function、Regex等），直接赋值，不进行复制
        /// @return {Object} JSON副本
        cloneJson: function (json) {
            var result = {}, p;
            for (p in json) {
                if (jTools.util.isObject(json[p])) {
                    result[p] = arguments.callee(json[p]);
                } else if (jTools.util.isArray(json[p])) {
                    result[p] = json[p].slice();
                } else {
                    result[p] = json[p];
                }
            }

            return result;
        },

        /// 遍历数组或对象，对每个成员执行某个方法
        /// @param {Mixed} 数组或对象
        /// @param {Function} 回调函数
        /// @param {Array} 额外参数
        /// @return {Mixed} 原数组或对象
        each: function (obj, callback, args) {
            if (obj != null) {
                var i = -1, len = obj.length,
				isObj = len === undefined || jTools.util.isFunction(obj);

                if (args) {
                    if (isObj) {
                        for (i in obj) {
                            if (false === callback.apply(obj[i], args)) {
                                break;
                            }
                        }
                    } else {
                        while (++i < len) {
                            if (false === callback.apply(obj[i], args)) {
                                break;
                            }
                        }
                    }
                } else {
                    if (isObj) {
                        for (i in obj) {
                            if (false === callback.call(obj[i], i, obj[i])) {
                                break;
                            }
                        }
                    } else {
                        while (++i < len) {
                            if (false === callback.call(obj[i], i, obj[i])) {
                                break;
                            }
                        }
                    }
                }
            }

            return obj;
        },

        /// 获取随机数
        /// @param {Number} 上限
        /// @param {Number} 下限
        /// @return {Number} 随机数
        random: function (up, down) {
            if (up && down) {
                return parseInt(Math.random() * (up - down + 1) + down);
            }
            else if (up) {
                return parseInt(Math.random() * parseInt(up) + 1);
            }
            else {
                return Math.random();
            }
        },

        /// 缩小图片显示
        /// @param {Object} 图片对象
        /// @param {Number} 图片宽
        /// @return {Number} 图片高
        MaxImg: function (obj, width, height) {
            var max_width = width;
            if (obj.offsetWidth > max_width) {
                obj.resized = true;
                obj.height = obj.height * max_width / obj.width;
                obj.width = max_width;
            }
            if (height) {
                var max_height = height;
                if (obj.offsetHeight > max_height) {
                    obj.resized = true;
                    obj.width = obj.width * max_height / obj.height;
                    obj.height = max_height;
                }
            }
        },
        /// 设为首页
        /// @param {String} URL地址
        SetHome: function (url) {
            try {
                //obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(url);
                document.body.style.behavior = 'url(#default#homepage)'; document.body.setHomePage(url);
            }
            catch (e) {
                if (window.netscape) {
                    try {
                        netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                    }
                    catch (e) {
                        alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将[signed.applets.codebase_principal_support]设置为'true'");
                    }
                    var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
                    prefs.setCharPref('browser.startup.homepage', vrl);
                }
            }
        },

        /// 添加到收藏夹
        /// @param {String} 地址
        /// @param {String} 标题
        AddFavorite: function (sURL, sTitle) {
            try {
                window.external.addFavorite(sURL, sTitle);
            }
            catch (e) {
                try {
                    window.sidebar.addPanel(sTitle, sURL, "");
                }
                catch (e) {
                    alert("加入收藏失败，请使用Ctrl+D进行添加");
                }
            }
        },
        /// 复制内容到剪贴板去
        /// @param {String} 要复制的内容
        CopyToClipBoard: function (txt) {
            if (window.clipboardData) {
                window.clipboardData.clearData();
                window.clipboardData.setData("Text", txt);
                alert("地址已复制成功，\n\n您可以粘贴并通过QQ/MSN/Email邀请朋友浏览该频道内容。");
            } else if (navigator.userAgent.indexOf("Opera") != -1) {
                window.location = txt;
            } else if (window.netscape) {
                try {
                    netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                } catch (e) {
                    alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true'");
                }
                var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
                if (!clip)
                    return;
                var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
                if (!trans)
                    return;
                trans.addDataFlavor('text/unicode');
                var str = new Object();
                var len = new Object();
                var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
                var copytext = txt;
                str.data = copytext;
                trans.setTransferData("text/unicode", str, copytext.length * 2);
                var clipid = Components.interfaces.nsIClipboard;
                if (!clip)
                    return false;
                clip.setData(trans, null, clipid.kGlobalClipboard);
                alert("地址已复制成功，\n\n您可以粘贴并通过QQ/MSN/Email邀请朋友浏览该频道内容。");
            }
        },
        GetChkOrRadioValues: function (frmname, name) {
            var form = document.forms[frmname];
            var eles = form[name];
            var values = new Array();
            var index = -1;
            for (var i = 0; i < eles.length; i++) {

                if (eles[i].type == "checkbox" || eles[i].type == "radio") {
                    if (eles[i].checked) {
                        index++;
                        values[index] = eles[i].value;
                    }
                }
                else {
                    index++;
                    values[index] = eles[i].value;
                }
            }
            return values;
        }

    };
    // 快速访问
    jTools.parseTpl = jTools.util.parseTpl;
    jTools.each = jTools.util.each;


    /// 面向对象辅助函数
    jTools.oo = {

        /// 创建类
        /// @param {Function} 构造函数
        /// @param {Object} 方法
        /// @param {Object} 父类
        /// @param {Function} 指定子类如何调用父类的构造函数。默认情况下使用子类的构造函数参数直接调用
        /// @return {Function} 类
        create: function (body, methods, parentClass, parentConstruct) {
            methods && jTools.util.extend(body.prototype, methods);
            return parentClass ? jTools.oo._inherit(body, parentClass, parentConstruct) : body;
        },

        /// 类继承
        /// @param {Function} 子类
        /// @param {Function} 父类
        /// @param {Function} 指定子类如何调用父类的构造函数。默认情况下使用子类的构造函数参数直接调用。
        /// @return {Function} 继承后的子类
        _inherit: function (subClass, parentClass, parentConstruct) {
            var trueClass = parentConstruct ? function () {
                parentConstruct.call(this, parentClass, arguments);
                subClass.apply(this, arguments);
            } : function () {
                parentClass.apply(this, arguments);
                subClass.apply(this, arguments);
            };

            jTools.util.extend(trueClass.prototype, parentClass.prototype);
            jTools.util.extend(trueClass.prototype, subClass.prototype);

            return trueClass;
        }
    };


    var readyList = [], 	// DOM Ready函数队列
	isReadyBound, 	// 是否已绑定DOM Ready事件
	onDomReady;

    if (document.addEventListener) {
        onDomReady = function () {
            document.removeEventListener("DOMContentLoaded", onDomReady, false);
            domReadyNow();
        };
    } else if (document.attachEvent) {	// For IE Only
        onDomReady = function () {
            if ("complete" === document.readyState) {
                document.detachEvent("onreadystatechange", onDomReady);
                domReadyNow();
            }
        };
    }

    // DOM Ready检查 For IE
    function doScrollCheck() {
        if (jTools.dom.isReady) { return; }

        try {
            document.documentElement.doScroll("left");
        } catch (e) {
            setTimeout(doScrollCheck, 1);
            return;
        }

        domReadyNow();
    }
    // DOM已就绪
    function domReadyNow() {
        if (!jTools.dom.isReady) {
            if (!document.body) { return setTimeout(domReadyNow, 13); }

            jTools.dom.isReady = true;

            if (readyList) {
                var i = -1, len = readyList.length;
                while (++i < len) {
                    readyList[i].call(document, jTools);
                }
                readyList = null;
            }
        }
    }
    // 绑定DOMReady事件
    function bindReady() {
        if (isReadyBound) { return; }

        if ("complete" === document.readyState) { return domReadyNow(); }

        if (document.addEventListener) {
            document.addEventListener("DOMContentLoaded", domReadyNow, false);
            window.addEventListener("load", domReadyNow, false);
        } else if (document.attachEvent) {
            document.attachEvent("onreadystatechange", domReadyNow);
            window.attachEvent("onload", domReadyNow);
            var isTopLevel;
            try {
                isTopLevel = window.frameElement == null;
            } catch (e) { }

            document.documentElement.doScroll && isTopLevel && doScrollCheck();
        }

        isReadyBound = true;
    }

    /// DOM操作
    jTools.dom = {

        /// 获取节点集合中的第n个节点
        /// @param {HTMLElement,HTMLCollection,Array} 节点或节点集合
        /// @param {Number} 节点序号
        /// @param {HTMLElement} 第n个节点
        single: function (nodes, n) {
            null == n && (n = 0);
            return nodes.nodeType || nodes.setInterval ? (0 == n ? nodes : undefined) : nodes[n];
        },

        /// 把节点放到数组容器中
        /// @param {HTMLElement,HTMLCollection,Array} 节点或节点集合
        /// @return {Array} 节点数组
        wrapByArray: function (nodes) {
            if (!nodes) {
                return [];
            } else if (nodes.nodeType || nodes.setInterval) {
                return [nodes];
            } else {
                return jTools.util.toArray(nodes);
            }
        },

        /// 对节点执行指定操作
        /// @param {HTMLElement,Array,HTMLCollection} 节点
        /// @param {Function} 回调函数
        /// @param {Array} 额外的参数
        /// @return {HTMLElement,Array,HTMLCollection} 指定节点
        eachNode: function (nodes, callback, args) {
            jTools.each(jTools.dom.wrapByArray(nodes), callback, args);
            return nodes;
        },

        /// 在DOM就绪时执行指定函数
        /// @param {Function} 指定函数
        /// @param {Object} 当前对象
        ready: function (fn) {
            // 绑定事件
            bindReady();

            if (jTools.dom.isReady) {
                fn.call(document, jTools);
            } else {
                readyList.push(fn);
            }

            return this;
        },

        /// 替换节点
        /// @param {HTMLElement} 新节点
        /// @param {HTMLElement} 原节点
        /// @return {HTMLElement} 新节点
        replaceNode: function (newNode, oldNode) {
            if (oldNode.replaceNode) {
                oldNode.replaceNode(newNode);
            } else if (oldNode.parentNode.replaceChild) {
                oldNode.parentNode.replaceChild(newNode, oldNode);
            }
            return newNode;
        }
    };
    // 快速访问
    jTools.ready = jTools.dom.ready;


    // 对CSS样式字符串进行解释的正则表达式
    var CSSSPACE = /\s*([:;])\s*/g,
	STYLENAME = /[^:;]+?(?=:)/g,
	STYLESPLITER = /[^:;]+/g,
	CLASSSPLITER = /[^\s]+/g,
	FIXCSSNAME = /-([a-z])/gi,
	FLOATNAME = testElem.style.styleFloat !== undefined ? "styleFloat" : "cssFloat",
	STYLENOTINPXS = /^(?:zIndex|fontWeight|opacity|zoom)$/,
	ISFLOAT = /^float$/i,
	OPACITYINFILTER = /opacity=([^,)]+)/, // 匹配IE下的不透明度
	ALPHAFILTER = /alpha\([^\)]*\)/, 	// 匹配滤镜中的不透明度设置
	setSpecialStyle = {
	    opacity: function (elem, val) {
	        var filter = jTools.style.getCurrentStyle(elem, "filter") || "";
	        if (val !== "") {
	            var alpha = "alpha(opacity=" + val * 100 + ")";
	            elem.style.filter = ALPHAFILTER.test(filter) ? filter.replace(ALPHAFILTER, alpha) : alpha;
	        } else {
	            elem.style.filter = filter.replace(ALPHAFILTER, "");
	        }
	    }
	},
	getSpecialStyle = {
	    opacity: function (elem, val) {
	        return OPACITYINFILTER.test(jTools.style.getCurrentStyle(elem, "filter") || "") ?
				parseFloat(RegExp.$1) / 100 : "";
	    }
	};

    // 添加样式类
    function addClasses(classes, len, str) {
        if (this.className) {
            var className = " " + this.className + " ", i = -1;
            while (++i < len) {
-1 === className.indexOf(" " + classes[i] + " ") && (className += (classes[i] + " "));
            }
            this.className = className.trim();
        } else {
            this.className = str;
        }
    }
    // 删除样式类
    function removeClasses(classes, len, str) {
        switch (this.className) {
            case str:
                this.className = "";
                break;

            case "":
                return;
                break;

            default:
                var className = " " + this.className + " ", i = -1;
                while (++i < len) {
                    className = className.replace(" " + classes[i] + " ", " ");
                }
                this.className = className.trim();
                break;
        }
    }
    // 添加样式
    function addStyles(styles) {
        for (var s in styles) {
            if (this.style[s] !== undefined) {
                this.style[s] =
				STYLENOTINPXS.test(s) || "" == styles[s] || isNaN(styles[s]) ? styles[s] : styles[s] + "px";
            } else if (setSpecialStyle[s]) {
                setSpecialStyle[s](this, styles[s]);
            }
        }
    }
    // 移除样式
    function removeStyles(styles) {
        for (var s in styles) {
            if (setSpecialStyle[s]) {
                setSpecialStyle[s](this, "");
            } else {
                this.style[s] !== undefined && (this.style[s] = "");
            }
        }
    }

    /// 样式控制
    jTools.style = {

        /// 把样式名转换成样式属性名
        /// @param {String} 样式名
        /// @return {String} 样式属性名
        fixStyleName: function (name) {
            return ISFLOAT.test(name) ? FLOATNAME : name.replace(FIXCSSNAME, function ($0, $1) {
                return $1.toUpperCase(); // 转换为js标准的样式名
            });
        },

        /// 检查指定元素是否包含某些样式类
        /// @param {HTMLElement,HTMLCollection,Array} 指定元素
        /// @param {String} 样式类名
        /// @return {Boolean} 元素是否包含某个样式类
        hasClass: function (elems, className) {
            elems = jTools.dom.wrapByArray(elems);
            var i = elems.length;
            if (i > 0) {
                className = " " + className + " ";
                while (--i >= 0) {
                    if ((" " + elems[i].className + " ").indexOf(className) >= 0) {
                        return true;
                    }
                }
            }
            return false;
        },

        // 识别CSS样式
        // @param {String,Object} 样式
        // @return {Object,Array} 样式字典或样式类数组
        parse: function (css) {
            var temp, result, i;
            if ("string" === typeof css) {
                temp = css.indexOf(":") >= 0;
                if (css.indexOf(";") >= 0 || temp) {
                    result = {};
                    css = css.trim()
					.replace(CSSSPACE, "$1")
					.replace(temp ? STYLENAME : STYLESPLITER, jTools.style.fixStyleName)
					.match(STYLESPLITER);
                    var len = css.length;
                    i = 0;
                    if (temp) {
                        if (len % 2 !== 0) {
                            throw "invalid inline style";
                        }
                        while (i < len) {
                            result[css[i++]] = css[i++];
                        }
                    } else {
                        while (i < len) {
                            result[css[i++]] = "";
                        }
                    }
                } else {
                    result = css.match(CLASSSPLITER) || [];
                }
            } else {
                result = css;

                if ("object" === typeof result) {
                    for (i in result) {
                        temp = jTools.style.fixStyleName(i);
                        if (temp !== i) {
                            result[temp] = result[i];
                            delete result[i];
                        }
                    }
                }
            }

            return result;
        },

        /// 为指定HTML元素添加样式
        /// @param {HTMLElement,Array,HTMLCollection} 指定元素
        /// @param {String,Object} 样式
        /// @return {HTMLElement,Array,HTMLCollection} 指定元素
        addCss: function (elems, css) {
            if (css) {
                var result = jTools.style.parse(css);
                if (jTools.util.isArray(result)) {
                    jTools.dom.eachNode(elems, addClasses, [result, result.length, css]);
                } else {
                    jTools.dom.eachNode(elems, addStyles, [result, css]);
                }
            }
            return elems;
        },

        /// 为指定HTML元素移除样式
        /// @param {HTMLElement,Array,HTMLCollection} 指定元素
        /// @param {String,Object} 样式
        /// @return {HTMLElement,Array,HTMLCollection} 指定元素
        removeCss: function (elems, css) {
            if (css) {
                var result = jTools.style.parse(css);
                if (jTools.util.isArray(result)) {
                    jTools.dom.eachNode(elems, removeClasses, [result, result.length, css]);
                } else {
                    jTools.dom.eachNode(elems, removeStyles, [result]);
                }
            }
            return elems;
        },

        /// 获取指定元素的当前样式
        /// @param {HTMLElement,Array,HTMLCollection} 指定元素
        /// @param {String} 样式名
        /// @param {Object} 元素所在的页面的window对象，默认为当前window对象
        /// @return {String} 样式值
        getCurrentStyle: function (node, styleName, w) {
            if (!node) { return undefined; }

            !node.nodeType && (node = node[0]);
            styleName = jTools.style.fixStyleName(styleName);

            if (node.style[styleName] !== undefined) {
                return node.style[styleName] ||
				((node.currentStyle || (w || window).getComputedStyle(node, null))[styleName]);
            } else if (getSpecialStyle[styleName]) {
                return getSpecialStyle[styleName](node);
            }
        },
        /// 获取指定元素的位置
        /// @param {HTMLElement,Array,HTMLCollection} 指定元素
        /// @param {Object} 元素
        /// @return {Collection} 集合
        GetPositionByElement: function (obj) {
            var pLeft = obj.offsetLeft;
            var pTop = obj.offsetTop;
            while (obj.tagName.toUpperCase() != "BODY" && obj.tagName.toUpperCase() != "HTML") {
                obj = obj.offsetParent;
                pLeft += obj.offsetLeft;
                pTop += obj.offsetTop;
            }
            return { top: pTop, left: pLeft };
        }
    };

    //避免每次都进行兼容性判断
    var _addEvent, _removeEvent;
    if (window.addEventListener) {
        _addEvent = function (e, n, h) { e.addEventListener(n, h, false); };
        _removeEvent = function (e, n, h) { e.removeEventListener(n, h, false); };
    } else if (window.attachEvent) {
        _addEvent = function (e, n, h) { e.attachEvent("on" + n, h); };
        _removeEvent = function (e, n, h) { e.detachEvent("on" + n, h); };
    }

    // 添加事件
    function newEvent(eventName, handler, data) {
        var t = this;
        handler = jTools.event.delegate(t, eventName, handler, data);
        _addEvent(t, eventName, handler);
    }
    // 移除事件
    function disposeEvent(eventName, handler) {
        var t = this, trueHandler = jTools.event.getDelegate(t, eventName, handler);
        if (handler) {
            _removeEvent(t, eventName, trueHandler);
        } else if (eventName) {
            jTools.each(trueHandler, function (i, event) {
                event && disposeEvent.call(t, eventName, event);
            });
        } else {
            jTools.each(trueHandler, function (i) {
                disposeEvent.call(t, i);
            });
        }
    }

    var EVENTSPLITER = /\s*,\s*/, // 事件名分隔符
	eventId = 0; // 事件编号基值

    /// 事件处理
    jTools.event = {

        /// 事件Id属性名
        idName: globalName + "EventId",

        /// 事件容器名
        eventSpace: globalName + "Events",

        /// 为指定HTML元素添加事件委托函数
        /// @param {HTMLElement,Array,HTMLCollection} 指定元素
        /// @param {String} 事件名，多个事件名用逗号隔开
        /// @param {Function} 事件委托函数
        /// @param {Object} 额外传入的数据
        /// @return {HTMLElement,Array,HTMLCollection} 指定元素
        addEvent: function (elems, eventNames, handler, data) {
            eventNames = eventNames.split(EVENTSPLITER);
            var i = eventNames.length;
            while (--i >= 0) {
                jTools.dom.eachNode(elems, newEvent, [eventNames[i], handler, data]);
            }
            return elems;
        },

        /// 为指定HTML元素移除事件委托函数
        /// @param {HTMLElement,Array,HTMLCollection} 指定元素
        /// @param {String} 事件名，多个事件名用逗号隔开
        /// @param {Function} 事件处理函数
        /// @return {HTMLElement,Array,HTMLCollection} 指定元素
        removeEvent: function (elems, eventNames, handler) {
            if (eventNames) {
                eventNames = eventNames.split(EVENTSPLITER);
                var i = eventNames.length;
                while (--i >= 0) {
                    jTools.dom.eachNode(elems, disposeEvent, [eventNames[i], handler]);
                }
            } else {
                jTools.dom.eachNode(elems, disposeEvent, []);
            }

            return elems;
        },

        /// 生成事件代理
        /// @param {HTMLElement} 元素
        /// @param {String} 事件名
        /// @param {Function} 事件处理函数
        /// @param {Object} 额外传入的数据
        /// @return {Function} 事件代理
        delegate: function (elem, eventName, handler, data) {
            var t = jTools.event, events = elem[t.eventSpace] = elem[t.eventSpace] || {}, 	// 取得事件Hash表引用
			id = handler[t.idName] = handler[t.idName] || ++eventId; 	// 获取不重复的事件编号
            // 生成特定事件Hash表
            events[eventName] = events[eventName] || {};

            var trueHandler = events[eventName][id];
            if (!trueHandler) {
                trueHandler = function (e) {
                    e = t.fix(e);
                    e.data = data;
                    var result = handler.call(elem, e/*, data*/);
                    false === result && e.preventDefault();
                    true === e.cancelBubble && e.stopPropagation();
                    return result;
                };
                events[eventName][id] = trueHandler;
            }

            return trueHandler;
        },

        /// 获取事件代理
        /// @param {HTMLElement} 元素
        /// @param {String} 事件名
        /// @param {Function} 事件处理函数
        /// @return {Function} 事件代理
        getDelegate: function (elem, eventName, handler) {
            var t = jTools.event, eventSpace = elem[t.eventSpace];
            if (eventSpace) {
                try {
                    if (handler) {
                        var eventId = handler[t.idName];
                        if (eventId) { return eventSpace[eventName][eventId]; }
                    } else if (eventName) {
                        return eventSpace[eventName];
                    } else {
                        return eventSpace;
                    }
                } catch (e) {

                }
            }
            return handler;
        },

        /// 修复不同浏览器的事件兼容性
        /// @param {Event} 事件对象
        /// @return {Event} 修复后的事件对象
        fix: function (e) { return new jTools.event.EventArg(e); }
    };


    /// 事件参数类
    /// @param {Object} 源事件参数
    jTools.event.EventArg = jTools.oo.create(function (src) {
        var t = this;
        t._src = src;

        // 把原有的属性复制过来
        jTools.each(t._props, function (i, val) {
            if (src[val] != null) {
                t[val] = src[val];
            }
        });

        // 事件发生的时间
        t.timeStamp = Date.now();

        t.target = t.target || t.srcElement || document;
        3 == t.target.nodeType && (t.target = t.target.parentNode);

        if (null == t.pageX && t.clientX != null) {
            var doc = document.documentElement, body = document.body;
            t.pageX = t.clientX +
			(doc && doc.scrollLeft || body && body.scrollLeft || 0) -
			(doc && doc.clientLeft || body && body.clientLeft || 0);
            t.pageY = t.clientY +
			(doc && doc.scrollTop || body && body.scrollTop || 0) -
			(doc && doc.clientTop || body && body.clientTop || 0);
        }

        if (null == t.which) {
            if (t.charCode != null ? t.charCode : t.keyCode) {
                // 键盘按键事件
                t.which = t.charCode || t.keyCode;
            } else if (t.button) {
                // 鼠标单击事件：1 == 左键; 2 == 中键; 3 == 右键
                t.which = (t.button & 1 ? 1 : (t.button & 2 ? 3 : (t.button & 4 ? 2 : 0)));
            }
        }
    }, {
        /// 源事件对象拥有的属性
        _props: "altKey attrChange attrName bubbles button cancelable charCode clientX clientY ctrlKey currentTarget data detail eventPhase fromElement handler keyCode layerX layerY metaKey newValue offsetX offsetY originalTarget pageX pageY prevValue relatedNode relatedTarget screenX screenY shiftKey srcElement target toElement view wheelDelta which".split(" "),

        /// 禁止冒泡
        stopPropagation: function () {
            if (this._src.stopPropagation) {
                this._src.stopPropagation();
            } else {
                this._src.cancelBubble = true;
            }
        },

        /// 禁止默认事件执行
        preventDefault: function () {
            if (this._src.preventDefault) {
                this._src.preventDefault();
            } else {
                this._src.returnValue = false;
            }
        }
    });

    ///注释掉了的 如果不用UI
    /// 事件驱动抽象类
    //jTools.event.EventDriven = jTools.oo.create(function () {
    //        this._events = {};
    //    }, {
    //        /// 添加事件
    //        /// @param {String} 事件名
    //        /// @param {Function} 事件处理函数
    //        addEvent: function (eventName, handler) {
    //            var events = this._events;
    //            !events[eventName] && (events[eventName] = []);
    //            events[eventName].indexOf(handler) === -1 && events[eventName].push(handler);
    //        },

    //        /// 移除事件
    //        /// @param {String} 事件名
    //        /// @param {Function} 事件处理函数
    //        removeEvent: function (eventName, handler) {
    //            var theEvents = this._events[eventName];
    //            for (var i = theEvents.length - 1; i >= 0; i--) {
    //                if (theEvents[i] === handler) {
    //                    theEvents[i] = undefined;
    //                    return;
    //                }
    //            }
    //        },

    //        /// 触发事件
    //        /// @param {String} 事件名
    //        /// @param {Array} 事件参数
    //        /// @return {Boolean} 如果事件处理函数返回了false，则返回false表示取消当前操作；其他情况均返回undefined
    //        triggerEvent: function (eventName, args) {
    //            var theEvents = this._events[eventName];
    //            if (theEvents) {
    //                for (var i = 0; i < theEvents.length; i++) {
    //                    if (theEvents[i]) {
    //                        if (false === theEvents[i].apply(this, args || [])) {
    //                            return false;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    });


    // 浏览器检测
    var ua = window.navigator.userAgent.toLowerCase(), browserMatch =
	/(webkit)[ \/]([\w.]+)/.exec(ua) ||
	/(opera)(?:.*version)?[ \/]([\w.]+)/.exec(ua) ||
	/(msie) ([\w.]+)/.exec(ua) ||
	!/compatible/.test(ua) && /(mozilla)(?:.*? rv:([\w.]+))?/.exec(window.navigator.userAgent.toLowerCase());
    jTools.browser = {};
    if (browserMatch) {
        jTools.browser[browserMatch[1] || ""] = true;
        jTools.browser.version = browserMatch[2] || "0";
    }

    /// Ajax操作
    jTools.ajax = {

        /// 创建XmlHttpRequest对象
        /// @return {XMLHttpRequest} XmlHttpRequest对象
        createXhr: function () {
            var xhr;
            try {
                xhr = window.ActiveXObject ? new ActiveXObject("Microsoft.XMLHTTP") : new XMLHttpRequest();
            } catch (e) { }
            if (!xhr) {
                throw "failed to create XMLHttpRequest object";
            }
            return xhr;
        },

        /// 发送Ajax请求
        /// @param {String} 请求地址
        /// @param {String} 发送方式，"GET"或"POST"，默认为GET
        /// @param {Object} 发送的数据
        /// @param {Object} 其他可选参数
        /// @param {XMLHttpRequest} 用于发送请求的XmlHttpRequest对象，如不指定则自动创建
        /// @return {XMLHttpRequest} XmlHttpRequest对象
        /// @ex: $.ajax.send("test.aspx","POST",{ username : "zhangshan", password : "123456" },
        ///        {
        ///            headers : { "Content-Type" : "text/html;charset=GBK"; "Content-Type" ; "text/xml;charset=GBK" }, // 指定为GBK 编码   请求头
        ///            timeout : 30000, // 超时时间30 秒
        ///            onSuccess : function(xhr) {
        ///　　            alert(xhr.responseText); // 请求成功后输出返回的文本   请求成功后的操作；
        ///            },
        ///            onTimeout : function() {
        ///　　            alert("timeout"); //请求超时时的操作。
        ///            },
        ///            onError : function(xhr) {
        ///　　            alert("error");//请求失败时的操作；
        ///            }
        ///        }
        ///  );jTools.ajax.send(url,"POST",data,{onSuccess:CallBack_Operation,onError :onError});
        send: function (url, method, data, options, xhr) {
            // 创建XMLHttpRequest对象
            xhr = xhr || jTools.ajax.createXhr();
            var hasCompleted;

            // 修正参数
            "string" == typeof method && (method = method.toUpperCase());
            method = method != "GET" && method != "POST" ? "GET" : method; 	// 默认为GET

            options = "function" == typeof options ? {
                onSuccess: options
            } : options || {};
            options.async = null == options.async ? true : Boolean(options.async);

            // 连接参数键值对
            var postData;
            if (data) {
                postData = jTools.util.toQueryString(data);
                if ("GET" === method && postData) {
                    url += ("?" + postData);
                    postData = undefined;
                }
            }

            var stateChange = function () {
                if (4 == xhr.readyState) {
                    hasCompleted = true;
                    var eventName = 200 == xhr.status ? "onSuccess" : "onError";
                    options[eventName] && options[eventName](xhr);
                }
            };

            if (options.async) {
                options.timeout > 0 && setTimeout(function () {
                    if (!hasCompleted) {
                        xhr.abort();
                        options.onTimeout && options.onTimeout(xhr);
                    }
                }, options.timeout);

                xhr.onreadystatechange = stateChange;
            }

            // 打开连接并发送数据
            xhr.open(method, url, options.async, options.username, options.password);

            // 设置header
            var contentType = [];
            "POST" === method && contentType.push("application/x-www-form-urlencoded");
            xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            if (options.headers) {
                for (var h in options.headers) {
                    if ("content-type" === h.toLowerCase()) {
                        contentType.push(options.headers[h]);
                    } else {
                        xhr.setRequestHeader(h, options.headers[h]);
                    }
                }
            }
            contentType.length &&
			xhr.setRequestHeader("Content-Type", contentType.join(";").replace(/;+/g, ";").replace(/;$/, ""));

            xhr.send(postData);

            !options.async && stateChange();

            return xhr;
        },

        /// 以GET方式发送Ajax请求
        /// @param {String} 请求地址
        /// @param {Object} 发送的数据
        /// @param {Object} 其他可选参数
        /// @param {XMLHttpRequest} 用于发送请求的XmlHttpRequest对象，如不指定则自动创建
        /// @return {XMLHttpRequest} XmlHttpRequest对象
        get: function (url, data, options, xhr) {
            return jTools.ajax.send(url, "GET", data, options, xhr);
        },

        /// 以POST方式发送Ajax请求
        /// @param {String} 请求地址
        /// @param {Object} 发送的数据
        /// @param {Object} 其他可选参数
        /// @param {XMLHttpRequest} 用于发送请求的XmlHttpRequest对象，如不指定则自动创建
        /// @return {XMLHttpRequest} XmlHttpRequest对象
        post: function (url, data, options, xhr) {
            return jTools.ajax.send(url, "POST", data, options, xhr);
        },

        //通用结果返回值 自己用而已
        CommonResult: function (responseText) {
            var ID = 0;
            var Msg = "";
            var isSucceed = "False";
            if ($.browser.msie) {
                var objXML = new ActiveXObject("Microsoft.XmlDom");
                objXML.loadXML(responseText);
                if (objXML.parseError != 0) {
                    Msg = "Request XML Error !";
                }
                else {
                    var xml = objXML.documentElement;
                    ID = xml.selectSingleNode("/Result/ID").text;
                    Msg = xml.selectSingleNode("/Result/Msg").text;
                    isSucceed = xml.selectSingleNode("/Result/isSucceed").text;
                }
            }
            else {
                var objXML = (new DOMParser()).parseFromString(responseText, "text/xml");
                if (objXML.documentElement.tagName == "parsererror") {
                    Msg = "Request XML Error !";
                }
                else {
                    var xml = objXML.documentElement;
                    for (var i = 0; i < xml.childNodes.length; i++) {
                        switch (xml.childNodes[i].nodeName) {
                            case ("ID"):
                                if (xml.childNodes[i].firstChild) {
                                    ID = xml.childNodes[i].firstChild.nodeValue;
                                }
                                break;
                            case ("Msg"):
                                if (xml.childNodes[i].firstChild) {
                                    Msg = xml.childNodes[i].firstChild.nodeValue;
                                }
                                break;
                            case ("isSucceed"):
                                if (xml.childNodes[i].firstChild) {
                                    isSucceed = xml.childNodes[i].firstChild.nodeValue;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return { isSucceed: isSucceed, Msg: Msg, ID: ID };
        },


        // 动态读取类型
        fileTypes: {
            // js脚本
            "js": {
                tagName: "script",
                urlAttr: "src",
                attrs: { type: "text/javascript" },
                onLoad: function (node) { node.parentNode.removeChild(node); }
            },

            // css样式
            "css": {
                tagName: "link",
                urlAttr: "href",
                attrs: { rel: "stylesheet", type: "text/css" }
            }
        },

        //importJs : function(url, onComplete, charset, doc)
        /// 动态加载资源
        /// @param {String} 资源地址
        /// @param {Object} GET参数
        /// @param {Object,Function} 其他选项或加载完成后执行的函数
        load: function (url, data, options) {
            // 修正选项参数
            if (null == options) {
                options = {};
            } else if ("function" == typeof options) {
                options = { onLoad: options };
            }

            // 识别文件类型
            var type;
            if (options.type) {
                type = options.type;
            } else if (/\.(\w+)(?:\?.*)?$/.test(url)) {
                type = RegExp.$1
            }
            type = jTools.ajax.fileTypes[type];
            if (!type) { throw "unknown file type"; }

            // 创建节点并设置属性
            var doc = options.targetDoc || document, node = doc.createElement(type.tagName), i;
            if (type.attrs) {
                for (i in type.attrs) {
                    node[i] = type.attrs[i];
                }
            }
            for (i in options) {
                "string" == typeof options[i] && (node[i] = options[i]);
            }

            // 读取完成后执行的操作
            node.onload = node.onreadystatechange = function () {
                if (!this.readyState || "loaded" == this.readyState || "complete" == this.readyState) {
                    this.onload = this.onreadystatechange = null;
                    type.onLoad && type.onLoad(this);
                    options && options.onLoad && options.onLoad();
                }
            };
            // IE不支持onerror

            // 设置Url
            node[type.urlAttr] = data != null ?
			url + "?" + jTools.util.toQueryString(data) : url;

            jTools.one("head", doc).appendChild(node);
        }
    };

    //弹出窗口类
    jTools.win = {

        compatMode: document.compatMode,
        documentMode: document.documentMode, //http://msdn.microsoft.com/zh-cn/library/cc817574.aspx
        isIE: navigator.appName == "Microsoft Internet Explorer" ? true : false,
        isFF: navigator.appName == "Netscape" ? true : false,
        IEVersion: parseInt(navigator.appVersion.replace(/.*?MSIE\s([\d\.]+).*/ig, "$1")),
        isFixed: (/MSIE\s([\d\.]+?);/ig.test(navigator.appVersion)) ? ((parseInt(navigator.appVersion.match(/MSIE\s([\d\.]+?);/ig)[0].replace("MSIE ", "").replace(";", "")) > 6) ? true : false) : true,
        scrollWidth: function () { return document.documentElement.scrollWidth > document.documentElement.clientWidth ? document.documentElement.scrollWidth : document.documentElement.clientWidth },
        scrollHeight: function () { return document.documentElement.scrollHeight > document.documentElement.clientHeight ? document.documentElement.scrollHeight : document.documentElement.clientHeight },
        clientWidth: function () { return document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth : document.body.clientWidth },
        clientHeight: function () { return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight : document.body.clientHeight },

        //遮盖层
        covering: {
            zindex: 2011,
            obj: null,
            objCount: 0,
            hiddenObj: null,
            open: function (opacity, backgroundColor) {
                var coveringDiv;
                if (jTools.win.covering.obj == null) {
                    coveringDiv = document.createElement("div");
                }
                else {
                    coveringDiv = jTools.win.covering.obj;
                }
                coveringDiv.id = "jTools_win_covering_div";
                coveringDiv.style.position = "absolute";
                coveringDiv.style.zIndex = jTools.win.covering.zindex;
                coveringDiv.style.left = "0px";
                coveringDiv.style.top = "0px";
                coveringDiv.style.width = jTools.win.scrollWidth() + "px";
                coveringDiv.style.height = jTools.win.scrollHeight() + "px";
                if (backgroundColor) {
                    coveringDiv.style.backgroundColor = backgroundColor;
                }
                else {
                    coveringDiv.style.backgroundColor = "#000000";
                }
                if (opacity) {
                    if (opacity > 1 && opacity <= 100) {
                        coveringDiv.style.filter = "alpha(opacity=" + opacity.toString() + ")";
                        coveringDiv.style.opacity = (opacity * 0.01).toString();
                    }
                    else if (opacity >= 0 && opacity <= 1) {
                        coveringDiv.style.filter = "alpha(opacity=" + (opacity * 100).toString() + ")";
                        coveringDiv.style.opacity = opacity.toString();
                    }
                }
                else {
                    coveringDiv.style.filter = "alpha(opacity=50)";
                    coveringDiv.style.opacity = "0.5";
                }
                if (jTools.win.covering.obj == null) {

                    jTools.win.covering.obj = coveringDiv;
                    jTools.win.covering.hiddenObj = new Array();
                    if (jTools.win.isIE) {
                        if (jTools.win.IEVersion <= 6) {
                            this.hidden(document.getElementsByTagName("select"));
                        }
                    }
                    /*this.hidden(document.getElementsByTagName("object"));*/
                    //添加遮盖层
                    document.body.appendChild(jTools.win.covering.obj);

                    if (window.addEventListener) {
                        window.addEventListener('resize', jTools.win.covering.onresize, false);
                    } else if (window.attachEvent) {
                        window.attachEvent('onresize', jTools.win.covering.onresize);
                    }
                }
                jTools.win.covering.objCount++;
            },
            _interval: function () {
                if (jTools.win.covering.obj != null) {
                    if (jTools.win.covering.clientSize.width != jTools.win.clientWidth()) {
                        jTools.win.covering.obj.style.width = jTools.win.scrollWidth() + "px";
                    }
                    if (jTools.win.covering.clientSize.height != jTools.win.clientHeight()) {
                        jTools.win.covering.obj.style.height = jTools.win.scrollHeight() + "px";
                    }
                }
            },
            onresize: function () {
                if (jTools.win.covering.obj != null) {
                    jTools.win.covering.obj.style.width = jTools.win.clientWidth() + "px";
                    setTimeout('jTools.win.covering.obj.style.width = jTools.win.scrollWidth() + "px";', 10);
                    jTools.win.covering.obj.style.height = jTools.win.clientHeight() + "px";
                    setTimeout('jTools.win.covering.obj.style.height = jTools.win.scrollHeight() + "px";', 10);
                }
            },
            hidden: function (hiddenElement) {
                for (var i = 0; i < hiddenElement.length; i++) {
                    jTools.win.covering.hiddenObj.push({ obj: hiddenElement[i], value: hiddenElement[i].style.display });
                    hiddenElement[i].style.display = "none";
                }
            },
            close: function () {
                if (jTools.win.covering.objCount > 1) {
                    jTools.win.covering.objCount--;
                }
                else {
                    if (window.removeEventListener) {
                        window.removeEventListener('resize', jTools.win.covering.onresize, false);
                    } else if (window.detachEvent) {
                        window.detachEvent('onresize', jTools.win.covering.onresize);
                    }
                    if (jTools.win.covering.obj != null) {
                        jTools.win.covering.obj.parentNode.removeChild(jTools.win.covering.obj);
                        jTools.win.covering.obj = null;
                    }
                    if (jTools.win.covering.hiddenObj != null) {
                        for (var i = 0; i < jTools.win.covering.hiddenObj.length; i++) {
                            jTools.win.covering.hiddenObj[i].obj.style.display = jTools.win.covering.hiddenObj[i].value;
                        }
                        jTools.win.covering.hiddenObj = null;
                    }
                    jTools.win.covering.objCount--;
                }
            }
        },
        //end covering

        /// 窗口弹出函数
        /// @param {Object} 对象
        /// @param {String} 标题
        /// @param {String} 内容
        /// @param {Collection} 参数集合
        Popup: function (obj, title, content, parameters) {
            this.width = "250px";
            this.height;
            this.top;
            this.left;

            this.isDrag = true; //是否可以拖动
            this.isCovering = false; //是否需要遮罩
            this.isScroll = false; //是否需要随滚动条滚动
            this.isTitleDblclick = true; //双击标题栏是否会最大化

            this.isMaxBtn = true; //是否需要最大化按钮
            this.isCloseBtn = true; //是否需要关闭按钮
            this.isTitleBox = true;

            this.isAlign = true; //内容是否需要居中
            this.isVAlign = true; //内容是否需要垂直居中

            this.picPath = "http://club.jinti.com/JS/"; //图片路径
            this.TitleColor = "black"; //"#1F62A4"
            this.TitleBackgroundColor = "#E2EAF8";
            this.border = "solid 1px #92B0DD"; //边框样式 solid 1px #93B2CE";
            this.borderBottomStyle = "solid 1px #92B0DD"; //标题底部边框样式

            this.BackgroundColor = ""; //"#F4F7F9";
            this.mainTagPadding = "";

            if (parameters) {
                if (parameters.width) this.width = parseInt(parameters.width) + "px";
                if (parameters.height) this.height = parseInt(parameters.height) + "px";
                if (parameters.top) this.top = parseInt(parameters.top) + "px";
                if (parameters.left) this.left = parseInt(parameters.left) + "px";
                if (parameters.isDrag) if (parameters.isDrag == false) this.isDrag = parameters.isDrag;
                if (parameters.isCovering) if (parameters.isCovering == true) this.isCovering = parameters.isCovering;
                if (parameters.isScroll) if (parameters.isScroll == true) this.isScroll = parameters.isScroll;
                if (parameters.isTitleDblclick) if (parameters.isTitleDblclick == false) this.isTitleDblclick = parameters.isTitleDblclick;
                if (parameters.isMaxBtn) if (parameters.isMaxBtn == false) this.isMaxBtn = parameters.isMaxBtn;
                if (parameters.isCloseBtn) if (parameters.isCloseBtn == false) this.isCloseBtn = parameters.isCloseBtn;
                if (parameters.isTitleBox) if (parameters.isTitleBox == false) this.isTitleBox = parameters.isTitleBox;
                if (parameters.isAlign) if (parameters.isAlign == false) this.isAlign = parameters.isAlign;
                if (parameters.isVAlign) if (parameters.isVAlign == false) this.isVAlign = parameters.isVAlign;
                if (parameters.picPath) this.picPath = parameters.picPath;
                if (parameters.TitleColor) this.TitleColor = parameters.TitleColor;
                if (parameters.TitleBackgroundColor) this.TitleBackgroundColor = parameters.TitleBackgroundColor;
                if (parameters.border) this.border = parameters.border;
                if (parameters.BackgroundColor) this.BackgroundColor = parameters.BackgroundColor;
            };

            this.title = "JsWin"; //标题
            this.content = ""; //内容

            if (title != undefined && title != null) { this.title = title; }
            if (content != undefined && content != null) { this.content = content }

            this.mainTag = document.createElement("div");
            this.drag = { draging: false, e: null, x: 0, y: 0, top: 0, left: 0, dashedWin: null, overWin: null };
            this.maxSize = { isMax: false, btn: null, width: null, height: null, top: null, left: null };

            this.titleBtnWidth = 60;
            this.onmousemove;
            this.onmouseup;
            this.onselectstart;
            this.onscroll = { x: null, y: null, Timer: null };

            this.obj = false;

            this.open = function () {
                //打开遮盖层
                if (this.isCovering) { jTools.win.covering.open(30); }
                //主框
                this.mainTag.style.zIndex = jTools.win.covering.zindex + 1;
                //this.mainTag.style.border = this.border;

                //this.BackgroundColor = "#f0f0f0";
                this.mainTag.style.background = this.BackgroundColor; //this.BackgroundColor; 主框DIV背景色

                this.mainTag.style.width = this.width;

                this.mainTag.style.padding = this.mainTagPadding;

                if (this.isScroll && jTools.win.isFixed) {
                    this.mainTag.style.position = "fixed";
                    this.isScroll = false;
                }
                else {
                    this.mainTag.style.position = "absolute";
                }
                //标题栏
                var titleTitleDblclick = this.isTitleDblclick ? 'ondblclick=' + obj + '.max()' : '';
                var maxBtn = this.isMaxBtn ? '<img src="' + this.picPath + 'winmaxbtn.gif" style="cursor:pointer; margin:0px 8px 0px 8px;" onclick="' + obj + '.max()" />' : '';

                //var closeBtn = this.isCloseBtn ? '<img src="' + this.picPath + 'winclosebtn.gif" style="cursor:pointer;" onclick="' + obj + '.close()" />' : '';
                var closeBtn = this.isCloseBtn ? '<a class="closeBtn" href="#"　style="cursor:pointer;" onclick="' + obj + '.close()"></a>' : '';

                var dragClick = this.isDrag ? 'onmousedown="' + obj + '.dragDown(event)"' : '';
                var TitleBoxDisplay = this.isTitleBox ? "" : "display:none;";
                var TitleBGColor = this.TitleBackgroundColor == "" ? "" : "background-color:" + this.TitleBackgroundColor + ";";

                //var borderBottom = this.content != "" ? "border-bottom:" + this.border : "";
                var borderBottom = this.title != "" ? "border-bottom:" + this.borderBottomStyle : "";

                var titleHTML = '<div style="' + TitleBoxDisplay + 'position:absolute;width:' + (((maxBtn + closeBtn) == "") ? parseInt(this.width) : (parseInt(parseInt(this.width) - this.titleBtnWidth))) + 'px;height:23px;background-images:url(' + this.picPath + 'transparency.PNG);cursor:move;" ' + titleTitleDblclick + ' ' + dragClick + '>'
							    + '</div>'
							    + '<table border="0" cellspacing="0" cellpadding="0" style="' + TitleBoxDisplay + 'width:100%;height:28px;' + TitleBGColor + borderBottom + '">'
								    + '<tr style="width:100%;">'
									    + '<td style="text-align:left;font-size:14px;color:' + this.TitleColor + ';">'
										    + '&#160;&#160;' + this.title
									    + '</td>'
									    + '<td style="width:' + this.titleBtnWidth + 'px;text-align:right;">'
										    + maxBtn
										    + closeBtn
										    + '&#160;'
									    + '</td>'
								    + '</tr>'
							    + '</table>';

                //                var titleHTML = '<div style="' + TitleBoxDisplay + 'position:absolute;width:' + (((maxBtn + closeBtn) == "") ? parseInt(this.width) : (parseInt(parseInt(this.width) - this.titleBtnWidth))) + 'px;height:23px;background-images:url(' + this.picPath + 'transparency.PNG);cursor:move;" ' + titleTitleDblclick + ' ' + dragClick + '>'
                //							    + '</div>'
                //							    + '<table border="0" cellspacing="0" cellpadding="0" style="' + TitleBoxDisplay + 'width:100%;height:24px;background-image:url(' + this.picPath + 'titleBG.jpg);' + TitleBGColor + borderBottom + '">'
                //								    + '<tr style="width:100%;">'
                //									    + '<td style="text-align:left;font-size:14px;color:' + this.TitleColor + ';">'
                //										    + '&#160;&#160;' + this.title + "sdfsdf"
                //									    + '</td>'
                //									    + '<td style="width:' + this.titleBtnWidth + 'px;text-align:right;">'
                //										    + maxBtn
                //										    + closeBtn
                //										    + '&#160;'
                //									    + '</td>'
                //								    + '</tr>'
                //							    + '</table>';



                //内容区域
                var contentHTML = '';
                if (this.content != '') {
                    var isAlign = this.isAlign ? ' align="center"' : '';
                    var isVAlign = this.isVAlign ? ' valign="middle"' : '';
                    contentHTML = '<table style="width:100%; " border="0" cellspacing="0" cellpadding="0">'
								    + '<tr>'
									    + '<td' + isAlign + isVAlign + ' style="word-break:break-all;word-wrap:break-word;">'
										    + this.content
									    + '</td>'
								    + '</tr>'
							    + '</table>';
                }

                this.mainTag.innerHTML = titleHTML + contentHTML; //把标题跟内容赋值给弹出层DIV

                if (jTools.win.isIE) {
                    this.maxSize.btn = this.mainTag.childNodes(1).firstChild.firstChild.childNodes(1).firstChild;
                } else {
                    this.maxSize.btn = this.mainTag.childNodes[1].firstChild.firstChild.childNodes[1].firstChild;
                }

                document.body.appendChild(this.mainTag); //把弹出层DIV附加到BODY中去

                //if (parseInt(this.mainTag.offsetHeight) < 48) { this.mainTag.style.height = "48px"; }

                if (this.top == undefined) {
                    if (this.mainTag.style.position == "fixed") {
                        this.top = (parseInt(jTools.win.clientHeight()) - parseInt(this.mainTag.offsetHeight)) / 2;
                    }
                    else {
                        this.top = parseInt(document.documentElement.scrollTop) + (parseInt(jTools.win.clientHeight()) - parseInt(this.mainTag.offsetHeight)) / 2;
                    }
                }
                if (this.left == undefined) {
                    this.left = (parseInt(jTools.win.clientWidth()) - parseInt(this.mainTag.clientWidth)) / 2;
                }

                this.mainTag.style.top = this.top + "px";
                this.mainTag.style.left = this.left + "px";

                if (this.isScroll) {
                    this.onscroll.x = this.left - parseInt(document.documentElement.scrollLeft);
                    this.onscroll.y = this.top - parseInt(document.documentElement.scrollTop);
                    //this.onscroll.Timer = setInterval(obj + ".Scroll()", 100);
                    if (window.addEventListener) {
                        window.addEventListener('scroll', this.onscroll, false);
                    } else if (window.attachEvent) {
                        window.attachEvent('onscroll', this.onscroll);
                    }
                }
                if (window.addEventListener) {
                    window.addEventListener('resize', this.onresize, false);
                } else if (window.attachEvent) {
                    window.attachEvent('onresize', this.onresize);
                }
                this.obj = true;
            };

            //鼠标按下是执行
            this.dragDown = function (e) {
                if (!this.maxSize.isMax) {
                    this.drag.e = e;
                    this.drag.x = e.clientX;
                    this.drag.y = e.clientY;
                    this.drag.top = parseInt(this.mainTag.style.top);
                    this.drag.left = parseInt(this.mainTag.style.left);
                    this.drag.draging = true;
                    //拖动时的虚线框
                    this.drag.dashedWin = document.createElement("div");
                    this.drag.dashedWin.style.zIndex = jTools.win.covering.zindex + 2;
                    this.drag.dashedWin.style.border = "dashed 1px black";
                    this.drag.dashedWin.style.background = "url(" + this.picPath + "ransparency.PNG)";
                    this.drag.dashedWin.style.width = parseInt(this.mainTag.offsetWidth) + "px";
                    this.drag.dashedWin.style.height = parseInt(this.mainTag.offsetHeight) + "px";
                    this.drag.dashedWin.style.position = "absolute";

                    //
                    if (this.mainTag.style.position == "fixed") {
                        this.drag.dashedWin.style.top = parseInt(this.mainTag.style.top) + document.documentElement.scrollTop + "px";
                        this.drag.dashedWin.style.left = parseInt(this.mainTag.style.left) + document.documentElement.scrollLeft + "px";
                    }
                    else {
                        this.drag.dashedWin.style.top = parseInt(this.mainTag.style.top) + "px";
                        this.drag.dashedWin.style.left = parseInt(this.mainTag.style.left) + "px";
                    }
                    this.drag.dashedWin.style.cursor = "move";

                    //this.drag.dashedWin.innerHTML='<div style="width:' + parseInt(this.mainTag.offsetWidth) + 'px;height:' + parseInt(this.mainTag.offsetHeight) + 'px;" onmousemove="' + obj + '.dragMove(event)" onmouseup="' + obj + '.dragUp()" onmouseout="' + obj + '.dragUp()">&#160;</div>';
                    this.drag.dashedWin.innerHTML = '<div style="width:' + parseInt(this.mainTag.offsetWidth) + 'px;height:' + parseInt(this.mainTag.offsetHeight) + 'px;"></div>';

                    if (jTools.win.isIE) {
                        this.onselectstart = document.body.onselectstart;
                        document.body.onselectstart = function () { return false; }
                    }
                    else {
                        e.preventDefault();
                    }

                    this.onmousemove = document.onmousemove;
                    this.onmouseup = document.onmouseup;
                    if (jTools.win.isIE) {
                        document.onmousemove = this.dragMove;
                    }
                    else {
                        document.onmousemove = new Function(window.event, obj.toString() + ".dragMove(arguments[0])");
                    }
                    document.onmouseup = this.dragUp;
                    document.body.appendChild(this.drag.dashedWin);
                }
            };

            //鼠标拖动
            this.dragMove = function (e) {
                if (eval(obj).drag.draging) {
                    var tmp_y;
                    var tmp_x;
                    if (jTools.win.isIE) {
                        tmp_y = eval(obj).drag.top + event.clientY - eval(obj).drag.y;
                        tmp_x = eval(obj).drag.left + event.clientX - eval(obj).drag.x;
                    }
                    else {
                        tmp_y = eval(obj).drag.top + e.clientY - eval(obj).drag.y;
                        tmp_x = eval(obj).drag.left + e.clientX - eval(obj).drag.x;
                    }
                    eval(obj).movedashedWin(tmp_x, tmp_y);
                }
            };

            //鼠标移开
            this.dragUp = function (e) {
                if (eval(obj).drag.draging) {
                    eval(obj).drag.draging = false;
                    if (eval(obj).mainTag.style.position == "fixed") {
                        eval(obj).move(parseInt(eval(obj).drag.dashedWin.style.left) - document.documentElement.scrollLeft, parseInt(eval(obj).drag.dashedWin.style.top) - document.documentElement.scrollTop);
                    }
                    else {
                        eval(obj).move(parseInt(eval(obj).drag.dashedWin.style.left), parseInt(eval(obj).drag.dashedWin.style.top));
                    }
                    eval(obj).drag.dashedWin.parentNode.removeChild(eval(obj).drag.dashedWin);
                    eval(obj).drag.dashedWin = null;
                    document.onmousemove = eval(obj).onmousemove;
                    document.onmouseup = eval(obj).onmouseup;
                    if (jTools.win.isIE) {
                        document.body.onselectstart = eval(obj).onselectstart;
                        eval(obj).onselectstart = null;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            };

            //虚线框移动
            this.movedashedWin = function (x, y) {
                if (x > 0 && (x + parseInt(this.mainTag.offsetWidth) < (parseInt(jTools.win.scrollWidth()) - 1))) {
                    if (eval(obj).mainTag.style.position == "fixed") this.drag.dashedWin.style.left = x + document.documentElement.scrollLeft + "px";
                    else this.drag.dashedWin.style.left = x + "px";
                }
                if (y > 0 && (y + parseInt(this.mainTag.offsetHeight) < (parseInt(jTools.win.scrollHeight()) - 1))) {
                    if (eval(obj).mainTag.style.position == "fixed") this.drag.dashedWin.style.top = y + document.documentElement.scrollTop + "px";
                    else this.drag.dashedWin.style.top = y + "px";
                }
            };

            //主框移动
            this.move = function (x, y) {
                if (x > 0 && (x + parseInt(this.mainTag.offsetWidth) < (parseInt(jTools.win.scrollWidth()) - 1))) {
                    this.mainTag.style.left = x + "px";
                    if (this.isScroll != null) { this.onscroll.x = x - parseInt(document.documentElement.scrollLeft); }
                }
                if (y > 0 && (y + parseInt(this.mainTag.offsetHeight) < (parseInt(jTools.win.scrollHeight()) - 1))) {
                    this.mainTag.style.top = y + "px";
                    if (this.isScroll != null) { this.onscroll.y = y - parseInt(document.documentElement.scrollTop); }
                }
            };

            //关闭
            this.close = function () {
                this.mainTag.parentNode.removeChild(this.mainTag);
                if (this.drag.overWin != null) {
                    this.drag.overWin.parentNode.removeChild(this.drag.overWin);
                    var select = document.getElementsByTagName("select");
                    for (var i = 0; i < selectObj.length; i++) {
                        select[i].style.display = selectObj[i];
                    }
                }
                if (window.removeEventListener) {
                    window.removeEventListener('resize', this.onresize, false);
                } else if (window.detachEvent) {
                    window.detachEvent('onresize', this.onresize);
                }
                if (this.isScroll) {
                    if (window.removeEventListener) {
                        window.removeEventListener('scroll', this.onscroll, false);
                    } else if (window.detachEvent) {
                        window.detachEvent('onscroll', this.onscroll);
                    }
                }
                if (this.isCovering) { jTools.win.covering.close(); }
                this.obj = false;
                obj = null;
            };

            //窗体最大化
            this.max = function () {
                if (this.maxSize.isMax) {
                    this.mainTag.style.top = parseInt(this.maxSize.top) + parseInt(document.documentElement.scrollTop) + "px";
                    this.mainTag.style.left = parseInt(this.maxSize.left) + parseInt(document.documentElement.scrollLeft) + "px";
                    this.mainTag.style.border = this.border;
                    this.mainTag.style.width = parseInt(this.maxSize.width) + "px";
                    this.mainTag.style.height = parseInt(this.maxSize.height) + "px";
                    this.mainTag.firstChild.style.width = parseInt(parseInt(this.mainTag.style.width) - this.titleBtnWidth) + "px";
                    if (this.isMaxBtn) this.maxSize.btn.src = this.picPath + 'winmaxbtn.gif';
                    this.maxSize.isMax = false;
                }
                else {
                    this.maxSize.width = parseInt(this.mainTag.offsetWidth);
                    this.maxSize.height = parseInt(this.mainTag.offsetHeight);
                    this.maxSize.top = parseInt(this.mainTag.style.top) - parseInt(document.documentElement.scrollTop);
                    this.maxSize.left = parseInt(this.mainTag.style.left) - parseInt(document.documentElement.scrollLeft);
                    this.mainTag.style.top = "0px";
                    this.mainTag.style.left = "0px";
                    this.mainTag.style.border = "0px";
                    this.mainTag.style.width = parseInt(document.documentElement.scrollWidth) + "px";
                    if (parseInt(document.documentElement.clientHeight) > parseInt(document.body.clientHeight)) {
                        this.mainTag.style.height = parseInt(document.documentElement.clientHeight) + "px";
                    }
                    else {
                        this.mainTag.style.height = parseInt(document.body.clientHeight) + "px";
                    }
                    this.mainTag.firstChild.style.width = parseInt(parseInt(this.mainTag.style.width) - this.titleBtnWidth) + "px";
                    if (this.isMaxBtn) this.maxSize.btn.src = this.picPath + 'wincomebackbtn.gif';
                    document.documentElement.scrollTop = 0;
                    document.documentElement.scrollLeft = 0;
                    this.maxSize.isMax = true;
                }
            };

            //移动滚动条
            this.onscroll = function () {
                var pt = eval(obj);
                pt.mainTag.style.left = pt.onscroll.x + parseInt(document.documentElement.scrollLeft) + "px";
                pt.mainTag.style.top = pt.onscroll.y + parseInt(document.documentElement.scrollTop) + "px";
            };
            //重置窗口大小
            this.onresize = function () {
                var pt = eval(obj);
                if (pt.mainTag.style.position == "fixed") {
                    pt.top = (parseInt(jTools.win.clientHeight()) - parseInt(pt.mainTag.offsetHeight)) / 2;
                }
                else {
                    pt.top = parseInt(document.documentElement.scrollTop) + (parseInt(jTools.win.clientHeight()) - parseInt(pt.mainTag.offsetHeight)) / 2;
                }
                pt.left = (parseInt(jTools.win.clientWidth()) - parseInt(pt.mainTag.clientWidth)) / 2;
                pt.mainTag.style.top = pt.top + "px"; pt.mainTag.style.left = pt.left + "px";
                pt.onscroll.x = pt.left - parseInt(document.documentElement.scrollLeft);
                pt.onscroll.y = pt.top - parseInt(document.documentElement.scrollTop);
            };

            //更新标题    
            this.refreshTitle = function (titleHTML) {
                var objTitle;
                if (jTools.win.isIE) { //标题标签
                    objTitle = this.mainTag.childNodes(1).firstChild.firstChild.firstChild;
                }
                else {
                    objTitle = this.mainTag.childNodes[1].firstChild.firstChild.firstChild;
                }
                if (titleHTML) {
                    this.title = "&#160;&#160;" + titleHTML;
                }
                objTitle.innerHTML = this.title;
            };
            //更新内容
            this.refreshContent = function (contentHTML) {
                var objContent;
                if (jTools.win.isIE) { //内容标签
                    objContent = this.mainTag.childNodes(2).firstChild.firstChild.firstChild;
                }
                else {
                    objContent = this.mainTag.childNodes[2].firstChild.firstChild.firstChild;
                }
                if (contentHTML) {
                    this.content = contentHTML;
                }
                objContent.innerHTML = this.content;
            };
        }, //end Popup

        //调用示例1
        Tip: {
            obj: null,
            open: function (content, width, borderStyle) {
                var isOpen = false;
                if (jTools.win.TipWin.obj != null) { //jTools.TipWin.obj 初始化对象
                    if (jTools.win.TipWin.obj.obj) {
                        isOpen = true;
                    }
                    //jTools.TipWin.obj.close();
                }
                content = '<table style=" font-size:13px;font-family:SimSun;" width="100%" height="100"><tr><td align="center" valign="middle">' + content + '</td></tr></table>';
                if (!isOpen) {
                    // Popup: function (obj, title, content, parameters) {
                    jTools.win.TipWin.obj = new jTools.win.Popup("jTools.win.TipWin.obj", '', content);
                    if (width) {
                        jTools.win.TipWin.obj.width = parseInt(width) + "px";
                    }
                    else {
                        jTools.win.TipWin.obj.width = "300px";
                    }
                    jTools.win.TipWin.obj.isTitleBox = false;
                    jTools.win.TipWin.obj.isScroll = true;
                    jTools.win.TipWin.obj.isCovering = true;
                    jTools.win.TipWin.obj.border = borderStyle; // "solid 3px #999999"; //93B2CE
                    jTools.win.TipWin.obj.open();
                }
                else {
                    jTools.win.TipWin.obj.refreshTitle(" ");
                    jTools.win.TipWin.obj.refreshContent(content);
                }
            },
            close: function () {
                if (jTools.win.TipWin.obj != null) {
                    jTools.win.TipWin.obj.close();
                }
            }
        },

        //调用示例2
        TipWin: {
            obj: null,
            open: function (title, content, width, borderStyle, isOpenAgain) {
                var isOpen = false;
                if (jTools.win.TipWin.obj != null) {
                    if (jTools.win.TipWin.obj.obj) {
                        isOpen = true;
                    }
                    //jTools.win.TipWin.obj.close();
                }
                content = '<table style="background:white;font-size:13px;font-family:SimSun;" width="100%" height="100"><tr><td align="center" valign="middle">' + content + '</td></tr></table>';
                if (!isOpen || isOpenAgain == 1) {
                    if (isOpen) {
                        jTools.win.TipWin.obj.close();
                    }
                    jTools.win.TipWin.obj = new jTools.win.Popup("jTools.win.TipWin.obj", title, content);
                    if (width) {
                        jTools.win.TipWin.obj.width = parseInt(width) + "px";
                    }
                    else {
                        jTools.win.TipWin.obj.width = "300px";
                    }
                    jTools.win.TipWin.obj.isMaxBtn = false;
                    jTools.win.TipWin.obj.isCloseBtn = true;
                    jTools.win.TipWin.obj.isTitleDblclick = false;
                    jTools.win.TipWin.obj.isScroll = true;
                    jTools.win.TipWin.obj.isCovering = true;
                    jTools.win.TipWin.obj.TitleBackgroundColor = "#FFFFFF";
                    jTools.win.TipWin.obj.BackgroundColor = "#f0f0f0"; //主框DIV背景色 64ACE8 3890D1 03A7F8 52A9E9 59B2EE
                    jTools.win.TipWin.obj.mainTagPadding = "5px";

                    if (borderStyle == "") {
                        jTools.win.TipWin.obj.border = "solid 3px #999999"; //solid 1px gray
                        jTools.win.TipWin.obj.borderBottomStyle = "solid 3px #999999";
                    }
                    else {
                        jTools.win.TipWin.obj.border = borderStyle;
                        jTools.win.TipWin.obj.borderBottomStyle = borderStyle;
                    }
                    jTools.win.TipWin.obj.open();
                }
                else {
                    if (title == "") title = " ";
                    jTools.win.TipWin.obj.refreshTitle(title);
                    jTools.win.TipWin.obj.refreshContent(content);
                }
            },
            close: function () {
                if (jTools.win.TipWin.obj != null) {
                    jTools.win.TipWin.obj.close();
                }
            }
        }

    };
    //end Popup

    // Cookie过期时间格式
    var EXPIRESWITHUNIT = /[smhdMy]$/,
	    TIMEUNITS = {
	        s: 1,
	        m: 60,
	        h: 60 * 60,
	        d: 24 * 60 * 60,
	        M: 30 * 24 * 60 * 60,
	        y: 365 * 24 * 60 * 60
	    };

    /// Cookie操作
    jTools.cookie = {

        /// 编码函数
        encoder: window.encodeURIComponent,

        /// 解码函数
        decoder: window.decodeURIComponent,

        /// 获取Cookie值
        /// @param {String} Cookie名
        /// @return {String} Cookie值
        get: function (name) {
            var t = jTools.cookie;
            name = t.encoder(name) + "=";
            var cookie = document.cookie, beginPos = cookie.indexOf(name), endPos;
            if (-1 === beginPos) {
                return undefined;
            }
            beginPos += name.length; endPos = cookie.indexOf(";", beginPos);
            if (endPos === -1) {
                endPos = cookie.length;
            }
            return t.decoder(cookie.substring(beginPos, endPos));
        },

        /// 写入Cookie值
        /// @param {String} Cookie名
        /// @param {Mixed} Cookie值
        /// @param {Number,Date,String} 过期时间
        /// @param {String} 域，默认为当前页
        /// @param {String} 路径，默认为当前路径
        /// @param {Boolean} 是否仅把Cookie发送给受保护的服务器连接(https)，默认为否
        set: function (name, value, expires, domain, path, secure) {
            var t = jTools.cookie, cookieStr = [t.encoder(name) + "=" + t.encoder(value)];
            if (expires) {
                var date, unit;
                if ("[object Date]" === toString.call(expires)) {
                    date = expires;
                } else {
                    if ("string" === typeof expires && EXPIRESWITHUNIT.test(expires)) {
                        expires = expires.substring(0, expires.length - 1);
                        unit = RegExp.lastMatch;
                    }
                    if (!isNaN(expires)) {
                        date = new Date();
                        date.setTime(date.getTime() + expires * TIMEUNITS[unit || "m"] * 1000);
                    }
                }
                date && cookieStr.push("expires=" + date.toUTCString());
            }
            path && cookieStr.push("path=" + path);
            domain && cookieStr.push("domain=" + domain);
            secure && cookieStr.push("secure");
            document.cookie = cookieStr.join(";");
        },

        /// 删除Cookie
        /// @param {String} Cookie名
        /// @param {String} 域
        /// @param {String} 路径
        del: function (name, domain, path) {
            document.cookie = jTools.cookie.encoder(name) + "=" +
			    (path ? ";path=" + path : "") + (domain ? ";domain=" + domain : "") + ";expires=Thu, 01-Jan-1970 00:00:01 GMT";
        }
    };


    var whiteSpaces = /^\s+|\s+$/g;
    /// 去掉当前字符串两端的某段字符串
    /// @param {String} 要去掉的字符串，默认为空白
    /// @return {String} 修整后的字符串
    !String.prototype.trim && (String.prototype.trim = function () { return this.replace(whiteSpaces, ""); });

    /// 从左边开始截取一定长度的子字符串
    /// @param {Number} 长度
    /// @return {String} 子字符串
    String.prototype.left = function (n) { return this.substr(0, n); };

    /// 从右边开始截取一定长度的子字符串
    /// @param {Number} 长度
    /// @return {String} 子字符串
    String.prototype.right = function (n) { return this.slice(-n); };

    /// 格式化字符串
    /// @param {String} 要格式化的字符串
    /// @param {String} 参数
    /// @return {String} 格式化后的字符串
    String.format = function (str) {
        var args = arguments, re = new RegExp("%([1-" + args.length + "])", "g");
        return String(str).replace(re, function ($0, $1) {
            return args[$1];
        });
    };

    /// 为函数绑定this和参数
    /// @param {Mixed} 需绑定为this的对象
    /// @param {Mixed} 参数
    /// @return {Function} 绑定this和参数的函数
    Function.prototype.bind = function () {
        if (!arguments.length) { return this; }
        var method = this, args = slice.call(arguments), object = args.shift();
        return function () {
            //arrayObject.concat(arrayX,arrayX,......,arrayX)
            //返回一个新的数组。该数组是通过把所有 arrayX 参数添加到 arrayObject 中生成的。如果要进行 concat() 操作的参数是数组，那么添加的是数组中的元素，而不是数组。
            return method.apply(object, args.concat(slice.call(arguments)));
        };
    };


    var arrayProto = Array.prototype;

    /// 在当前数组中顺序检索指定元素
    /// @param {Mixed} 指定元素
    /// @param {Number} 开始搜索的位置，默认为0
    /// @return {Number} 指定元素在数组中第一个匹配项的索引；如果该元素不存在于数组中，返回-1
    !arrayProto.indexOf && (arrayProto.indexOf = function (elt, from) {
        var len = this.length;
        from = Number(from) || 0;
        from = from < 0 ? Math.ceil(from) : Math.floor(from);
        from < 0 && (from += len);

        while (from < len) {
            if (this[from] === elt) { return from; }
            from++;
        }

        return -1;
    });

    /// 在当前数组中反序检索指定元素
    /// @param {Mixed} 指定元素
    /// @param {Number} 开始搜索的位置，默认为数组长度减一
    /// @return {Number} 指定元素在数组中第一个匹配项的索引；如果该元素不存在于数组中，返回-1
    !arrayProto.lastIndexOf && (arrayProto.lastIndexOf = function (elt, from) {
        var len = this.length;
        from = Number(from);

        if (isNaN(from)) {
            from = len - 1;
        } else {
            from = from < 0 ? Math.ceil(from) : Math.floor(from);
        }
        if (from < 0) {
            from += len;
        } else if (from >= len) {
            from = len - 1;
        }

        while (from > -1) {
            if (this[from] === elt) { return from; }
            from--;
        }

        return -1;
    });

    /// 对数组的每个元素执行一次指定函数
    /// @param {Function} 指定函数，第一个参数是元素，第二个参数是元素序号，第三个参数是数组
    /// @param {Mixed} 指定函数内的this
    !arrayProto.forEach && (arrayProto.forEach = function (fn, thisp) {
        var i = -1, len = this.length;
        while (++i < len) {
            fn.call(thisp, this[i], i, this);
        }
    });

    /// 检查是否所有元素都符合指定条件
    /// @param {Function} 用于测试每个元素的函数
    /// @param {Object} 测试函数内的this
    /// @return {Boolean} 是否所有元素都符合指定条件
    !arrayProto.every && (arrayProto.every = function (fn, thisp) {
        var result = true;
        this.forEach(function () {
            if (true === result && !fn.apply(this, arguments)) {
                result = false;
            }
        }, thisp);

        return result;
    });

    /// 检查是否有元素符合指定条件
    /// @param {Function} 用于测试元素的函数
    /// @param {Object} 测试函数内的this
    /// @return {Boolean} 是否有元素符合指定条件
    !arrayProto.some && (arrayProto.some = function (fn, thisp) {
        var result = false;
        this.forEach(function () {
            if (false === result && fn.apply(this, arguments)) {
                result = true;
            }
        }, thisp);

        return result;
    });

    /// 从当前数组创建一个由符合指定条件元素组成的新数组
    /// @param {Function} 用于测试每个元素的函数
    /// @param {Object} 测试函数内的this
    /// @return {Array} 由符合指定条件元素组成的新数组
    !arrayProto.filter && (arrayProto.filter = function (fn, thisp) {
        var result = [];

        this.forEach(function (val) {
            fn.apply(this, arguments) && result.push(val);
        }, thisp);

        return result;
    });

    /// 对当前数组的每个元素进行指定处理并把结果创建为新数组
    /// @param {Function} 处理每个元素的函数
    /// @param {Object} 指定函数内的this
    /// @param {return} 结果数组
    !arrayProto.map && (arrayProto.map = function (fn, thisp) {
        var result = [];

        this.forEach(function (val, i) {
            result[i] = fn.apply(this, arguments);
        }, thisp);

        return result;
    });

    /// 删除当前数组指定位置的元素
    /// @param {Number} 指定位置
    /// @return {Array} 当前数组
    arrayProto.remove = function (n) {
        n >= 0 && this.splice(n, 1);
        return this;
    };


    // 把数字转换成两位数的字符串
    function toTwoDigit(num) { return num < 10 ? "0" + num : num; }

    // 临时记录正在转换的日期
    var tempYear, tempMonth, tempDate, tempHour, tempMinute, tempSecond;

    // 格式替换函数
    function getDatePart(part) {
        switch (part) {
            case "yyyy": return tempYear;
            case "yy": return tempYear.toString().slice(-2);
            case "MM": return toTwoDigit(tempMonth);
            case "M": return tempMonth;
            case "dd": return toTwoDigit(tempDate);
            case "d": return tempDate;
            case "HH": return toTwoDigit(tempHour);
            case "H": return tempHour;
            case "hh": return toTwoDigit(tempHour > 12 ? tempHour - 12 : tempHour);
            case "h": return tempHour > 12 ? tempHour - 12 : tempHour;
            case "mm": return toTwoDigit(tempMinute);
            case "m": return tempMinute;
            case "ss": return toTwoDigit(tempSecond);
            case "s": return tempSecond;
            default: return part;
        }
    }

    /// 返回当前日期的毫秒表示
    /// @param {Number} 当前日期的毫秒表示
    !Date.now && (Date.now = function () { return +new Date; });

    /// 返回指定格式的日期字符串
    /// @param {String} 格式
    /// @return {String} 指定格式的日期字符串
    Date.prototype.format = function (formation) {
        tempYear = this.getFullYear();
        tempMonth = this.getMonth() + 1;
        tempDate = this.getDate();
        tempHour = this.getHours();
        tempMinute = this.getMinutes();
        tempSecond = this.getSeconds();

        return formation.replace(/y+|m+|d+|h+|s+|H+|M+/g, getDatePart);
    };


    // 回收资源
    testElem = null;


})(window);