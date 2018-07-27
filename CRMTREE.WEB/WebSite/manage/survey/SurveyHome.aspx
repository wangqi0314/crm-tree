<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveyHome.aspx.cs" Inherits="manage_survey_SurveyHome" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/styles/ST_001.css" rel="stylesheet" />
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>

    <script src="/scripts/common/json2.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _isEn = $.isEn();
        if (!_isEn) {
            document.write('<script src="/scripts/jquery-easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></sc' + 'ript>');
        }
    </script>
    <style>
    </style>
</head>

<body>
    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'west'" style="width: 250px;">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',border:false" style="height: 29px; overflow: hidden; border-bottom: 1px solid #B1C242; font-size: 16px; font-weight: bold; background-color: #FCFAB0; padding: 5px;">
                    活动列表
                </div>
                <div data-options="region:'center',border:false" style="padding: 5px; overflow-y: auto;">
                    <div id="_SurveyCata_s" class="easyui-accordion" data-options="selected:null">
                    </div>
                </div>
            </div>
        </div>
        <div data-options="region:'center'" style="padding: 0px">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',border:false" style="height: 29px; overflow: hidden; border-bottom: 1px solid #B1C242; font-size: 16px; font-weight: bold; background-color: #FCFAB0; padding: 5px;">
                    活动问卷列表
                </div>
                <div data-options="region:'center',border:false" style="padding: 5px; overflow-y: auto;">
                    <div id="c_tabs_camp" class="easyui-tabs" data-options="fit:true,border:true,plain:false,tools:'#c_tabs_camp_tools'">
                        <div title="Home" style="padding: 10px">
                            <%--<iframe id="ctrl_iframe" src="/templete/survey/SurveyList.aspx?CG_Code=13256" style="width: 100%; height: 100%" frameborder="0" border="0"
                        scrolling="no"></iframe>--%>
                        </div>
                    </div>
                    <div id="c_tabs_camp_tools" style="border-top: 0;">
                        <%--<a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="_campaign_all.edit();"><%= Resources.CRMTREEResource.btnEdit %></a>
                        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'" onclick="_campaign_all.use();"><%= Resources.CRMTREEResource.btnUse %></a>--%>
                        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="_SurveyCam.Remove();"><%= Resources.CRMTREEResource.ap_buttons_close %></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="isTest" style="display: none">
        <div class="_Con_Cen" _cg_code="123">
            <div class="_Con_I">
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="_SurveyCam.Remove();">添加</a>
                <hr />
            </div>
            <div class="_Con_II">
                <div class="_Con_Only_List">
                    <div class="_Con_Header">
                        <div class="_Con_Show"><span>1.wenjuan调查</span></div>
                        <div class="_Con_Edit">
                            <span>内容：</span>
                            <input type="text" value="1.wenjuan调查" />
                        </div>
                    </div>
                    <div class="_Con_Body">
                        <div class="_Con_Body_Tools">
                            <a href="javascript:void(0)" class="easyui-linkbutton _Con_Show" data-options="plain:true,iconCls:'icon-edit'" onclick="_Con_Cen.Eidt();"><%= Resources.CRMTREEResource.btnEdit %></a>
                            <a href="javascript:void(0)" class="easyui-linkbutton _Con_Edit" data-options="plain:true,iconCls:'icon-ok'" onclick="_Con_Cen.Show();">确认</a>
                            <%--<a href="javascript:void(0)" class="easyui-linkbutton _Con_Edit" data-options="plain:true,iconCls:'icon-add'" onclick="_SurveyCam.Remove();">添加</a>--%>
                        </div>
                        <div class="_Con_Body_Content">
                            <ul>
                                <li>
                                    <div class="_Con_Edit">
                                        <input type="checkbox" checked="checked" /><span>多选</span>
                                        <input type="checkbox" /><span>备注</span>
                                    </div>
                                </li>
                                <li>
                                    <div class="_Con_Show">特别满意  分数：100</div>
                                    <div class="_Con_Edit">
                                        <span>回答：</span><input type="text" value="1.wean调查" />
                                        <span>分数：</span><input type="text" value="100" />
                                        <span><a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="_SurveyCam.Remove();">删除</a></span>
                                    </div>
                                </li>
                                <li>
                                    <div class="_Con_Show">很满意  分数：100</div>
                                    <div class="_Con_Edit">
                                        <span>回答：</span><input type="text" value="1.wenjua查" />
                                        <span>分数：</span><input type="text" value="100" />
                                        <span><a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="_SurveyCam.Remove();">删除</a></span>
                                    </div>
                                </li>
                                <li>
                                    <div class="_Con_Show">满意  分数：100</div>
                                    <div class="_Con_Edit">
                                        <span>回答：</span><input type="text" value="1.wenn调查" />
                                        <span>分数：</span><input type="text" value="100" />
                                        <span><a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="_SurveyCam.Remove();">删除</a></span>
                                    </div>
                                </li>
                                <li>
                                    <div class="_Con_Show">不大满意  分数：100</div>
                                    <div class="_Con_Edit">
                                        <span>回答：</span><input type="text" value="1.wenn调查" />
                                        <span>分数：</span><input type="text" value="100" />
                                        <span><a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="_SurveyCam.Remove();">删除</a></span>
                                    </div>
                                </li>
                                <li>
                                    <div class="_Con_Show">很不满意  分数：100</div>
                                    <div class="_Con_Edit">
                                        <span>回答：</span><input type="text" value="1.wenn调查" />
                                        <span>分数：</span><input type="text" value="100" />
                                        <span><a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="_SurveyCam.Remove();">删除</a></span>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
<script>
    var _s_url = "/handler/Han_Survey.aspx";
    var _params = $.getParams();
    var _Con_Cen = {};
    //问卷问题回答选项编辑
    _Con_Cen.Eidt = function (obj, index) {
        if (!_SurveyCam.$Data || _SurveyCam.$Data.length <= 0) {
            return;
        }
        //当前索引数据
        var _o = _SurveyCam.$Data[index];

        var _htm = "", _ul = "";
        var _HeaderDatd = _html._edit.getParentHtml(_o.SQ_Question);
        var _HeaderHtml = _html._header.getHeader(_HeaderDatd);
        if ($.trim(_o.SF_Text) != "") {
            var _s_text = _o.SF_Text.split("||");
            var _s_value = _o.SF_Value.split("||");
            var _s_Type = _o.SF_Types.split("||");
            var is_Check = true;
            var is_Note = true;
            if (_s_Type[0] == 1) {
                is_Check = false;
            }
            if (_s_Type[_s_Type.length - 1] != 3) {
                is_Note = false;
            }
            _ul += _html._body.getCheckboxLi(is_Check, is_Note);
            var _s_text_length = _s_text.length;
            if (is_Note) {
                _s_text_length--;
            }
            for (var j = 0; j < _s_text_length; j++) {
                var _bodyData = _html._edit.getChildHtml(_s_text[j], _s_value[j]);
                var _bodyHtml = _html._body.getLi(_bodyData);
                _ul += _bodyHtml;
            }
        } else {
            //表示此问题不需要回答
            _ul += _html._body.getCheckboxLi(false, false);
        }
        //所有UL 标签组合
        _ul = _html._body.getUL(_ul);
        _ul = _html._body.getBoay_Content(_ul);
        // 问卷回答组合 加 问卷Tool
        _ul = _html._body.getBody(_html._body.getBody_ToolsII(index) + _ul);
        // 整个问题组合
        _htm = _HeaderHtml + _ul;
        $(obj).parent().parent().parent().html(_htm);
    }
    _Con_Cen.Show = function (obj, index) {
        if (!_SurveyCam.$Data || _SurveyCam.$Data.length <= 0) {
            _SurveyCam.$Data = [{}];
        }
        if (index == _SurveyCam.$Data.length) {
            _SurveyCam.$Data.push({});
        }
        var _obj = $(obj).parent().parent().parent();

        var _question = _obj.find("._Con_Header ._Con_Edit input[type=text]").val();
        _SurveyCam.$Data[index].SQ_Question = _question;

        var _question_check = _obj.find("._Con_Body ._Con_Body_Content ul li input[type=checkbox]:nth-child(1)").attr("checked");
        var _question_Node = _obj.find("._Con_Body ._Con_Body_Content ul li input[type=checkbox]:nth-child(3)").attr("checked");
        var _question_answer = _obj.find("._Con_Body ._Con_Body_Content ul li input[type=text]:nth-child(2)");
        var _question_answer_value = _obj.find("._Con_Body ._Con_Body_Content ul li input[type=text]:nth-child(4)");

        var _answer = "", _value = "",_types = "";
        if (_question_check === "checked") {
            _question_check = "2";
        } else {
            _question_check = "1";
        }
        $.each(_question_answer, function (i, o) {
            _answer += $(o).val() + "||";
            _types += _question_check + "||";
        });        
        $.each(_question_answer_value, function (i, o) {
            _value += $(o).val() + "||";
        });

        if (_question_Node === "checked") {
            _types += "3";
        } else {
            _answer = _answer.substring(0, _answer.length - 2);
            _value = _value.substring(0, _value.length - 2);
            _types = _types.substring(0, _types.length - 2);
        }
        
        _SurveyCam.$Data[index].SF_Text = _answer;
        _SurveyCam.$Data[index].SF_Value = _value;
        _SurveyCam.$Data[index].SF_Types = _types; 
        _SurveyCam.$Data[index].SF_ValType = _types;


        var _o = _SurveyCam.$Data[index];
        if ($.trim(_o) == "") {
            return;
        }
        var _htm = "", _ul = "";
        var _HeaderDatd = _html._show.getParentHtml(_o.SQ_Question);
        var _HeaderHtml = _html._header.getHeader(_HeaderDatd);
        if ($.trim(_o.SF_Text) == "") {
            _ul = _html._body.getBody(_html._body.getBody_ToolsI(index));
            _htm += _html.getCon_List(_HeaderHtml + $(_ul).css("min-height", "30px").prop("outerHTML"));
        } else {
            var _s_text = _o.SF_Text.split("||");
            var _s_value = _o.SF_Value.split("||");

            for (var j = 0; j < _s_text.length; j++) {
                var _bodyData = _html._show.getChildHtml(_s_text[j] + "&nbsp;&nbsp;&nbsp;" + _s_value[j]);
                var _bodyHtml = _html._body.getLi(_bodyData);
                _ul += _bodyHtml;
            }
            _ul = _html._body.getUL(_ul);
            _ul = _html._body.getBoay_Content(_ul);
            _ul = _html._body.getBody(_html._body.getBody_ToolsI(index) + _ul);
            _htm = _HeaderHtml + _ul;
        }
        $(obj).parent().parent().parent().html(_htm);
    }
    _Con_Cen.AddLi = function (obj, index) {
        var _bodyData = _html._edit.getChildHtml("", "");
        var _bodyHtml = _html._body.getLi(_bodyData);
        $(obj).parent().parent().find("._Con_Body_Content ul").append(_bodyHtml);
    }
    _Con_Cen.Add = function (cg_code) {
        var _index = 0;
        if (_SurveyCam.$Data && _SurveyCam.$Data.length > 0) {
            _index = _SurveyCam.$Data.length;
        }
        if (_index == 0) {
            _SurveyCam.$Data = [];
            _SurveyCam.$Data.push({});
        }

        var _htm = "", _ul = "";
        var _HeaderDatd = _html._edit.getParentHtml("");
        var _HeaderHtml = _html._header.getHeader(_HeaderDatd);
        var _bodyData = _html._edit.getChildHtml("", "");
        var _bodyHtml = _html._body.getLi(_bodyData);
        _ul += _html._body.getCheckboxLi(false, false);
        _ul += _bodyHtml;
        _ul = _html._body.getUL(_ul);
        _ul = _html._body.getBoay_Content(_ul);
        _ul = _html._body.getBody(_html._body.getBody_ToolsII(_index) + _ul);
        _htm = _html.getCon_List(_HeaderHtml + _ul);
        $("._Con_II").prepend(_htm);
    }
    _Con_Cen.DeleteLi = function (obj) {
        $(obj).parent().parent().remove();
    }
    _Con_Cen.Delete = function (obj, index) {
        if (!_SurveyCam.$Data || _SurveyCam.$Data.length <= 0) {
            return;
        }
        _SurveyCam.$Data[index].Delete = true;
        $.post(_s_url, { o: JSON.stringify({ action: 'Delete_Survey', SQ_Code: _SurveyCam.$Data[index].SQ_Code, SF_Code: _SurveyCam.$Data[index].SF_Code }) }, function (data) {
            if (data >= 0) {
                $(obj).parent().parent().parent().remove();
            }
        }, "json");
          
        

    }
    _Con_Cen.Save = function () {
        if (!_SurveyCam.$Data && !_SurveyCam.$Data.length > 0) {
            return;
        }
        var tab = _SurveyCam.$tabs.tabs('getSelected');
        var _CG_Code = $(tab).find("._Con_Cen").attr("_cg_code");
        $.post(_s_url, { o: JSON.stringify({ action: 'Save_Survey_data', CG_Code: _CG_Code,Save_Data:_SurveyCam.$Data }) }, function (data) {
            if (data >=0) {
                var index = _SurveyCam.getTabIndex(_CG_Code);
                _SurveyCam.$tabs.tabs('close', index);
            } 
        }, "json");
    };

    var _html = {
        _show: {
            getParentHtml: function (o1, o2) {
                var _div = $("<div></div>").addClass("_Con_Show");
                $("<span></span>").text(o1).appendTo(_div);
                return _div.prop("outerHTML");
            },
            getChildHtml: function (o1, o2) {
                var _div = $("<div></div>").addClass("_Con_Show").html(o1);
                return _div.prop("outerHTML");
            }
        },
        _edit: {
            getParentHtml: function (o1, o2) {
                var _div = $("<div></div>").addClass("_Con_Edit");
                $("<span></span>").text("内容：").appendTo(_div);
                $("<input />").attr("type", "text").attr("value", o1).appendTo(_div);
                return _div.prop("outerHTML");
            },
            getChildHtml: function (o1, o2) {
                var _h = "<span>回答：</span><input type=\"text\" value=\"" + o1 + "\" />"
                          + " <span>分数：</span><input type=\"text\" value=\"" + o2 + "\" />"
                          + " <span><a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-remove'\" onclick=\"_Con_Cen.DeleteLi(this);\">删除</a></span>";
                var _div = $("<div></div>").addClass("_Con_Edit");
                $(_h).appendTo(_div);
                $.parser.parse(_div);
                return _div.prop("outerHTML");
            }
        },
        _body: {
            getLi: function (o1) {
                return $("<li></li>").html(o1).prop("outerHTML");
            },
            getCheckboxLi: function (o1, o2) {
                var _div = $("<div></div>").addClass("_Con_Edit");
                if (o1) {
                    $("<input />").attr("type", "checkbox").attr("checked", "checked").appendTo(_div);
                } else {
                    $("<input />").attr("type", "checkbox").removeAttr("checked").appendTo(_div);
                }
                $("<span>多选</span>").appendTo(_div);
                if (o2) {
                    $("<input />").attr("type", "checkbox").attr("checked", "checked").appendTo(_div);
                } else {
                    $("<input />").attr("type", "checkbox").removeAttr("checked").appendTo(_div);
                }
                $("<span>备注</span>").appendTo(_div);
                return $("<li></li>").html(_div).prop("outerHTML");
            },
            getUL: function (o1) {
                return $("<ul></ul>").html(o1).prop("outerHTML");
            },
            getBoay_Content: function (o1) {
                return $("<div></div>").addClass("_Con_Body_Content").html(o1).prop("outerHTML");
            },
            getBody_Tools: function () {
                var _o = "<a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-add'\" onclick=\"_Con_Cen.Add();\">添加</a><a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-ok'\" onclick=\"_Con_Cen.Save();\">保存</a><hr />";
                var _div = $("<div></div>").addClass("_Con_I");
                $(_o).appendTo(_div);
                $.parser.parse(_div);
                return _div.prop("outerHTML");
            },
            getBody_ToolsI: function (index) {
                var _o = "<a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-edit'\" onclick=\"_Con_Cen.Eidt(this," + index + ");\">编辑</a><a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-remove'\" onclick=\"_Con_Cen.Delete(this," + index + ");\">删除</a>";
                var _div = $("<div></div>").addClass("_Con_Body_Tools");
                $(_o).appendTo(_div);
                $.parser.parse(_div);
                return _div.prop("outerHTML");
            },
            getBody_ToolsII: function (index) {
                var _o = "<a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-ok'\" onclick=\"_Con_Cen.Show(this," + index + ");\">确认</a><a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-add'\" onclick=\"_Con_Cen.AddLi(this,0);\">添加</a>";
                var _div = $("<div></div>").addClass("_Con_Body_Tools");
                $(_o).appendTo(_div);
                $.parser.parse(_div);
                return _div.prop("outerHTML");
            },
            getBody: function (o1) {
                return $("<div></div>").addClass("_Con_Body").html(o1).prop("outerHTML");
            }
        },
        _header: {
            getHeader: function (o1) {
                return $("<div></div>").addClass("_Con_Header").html(o1).prop("outerHTML");
            }
        },
        getCon_List: function (o1) {
            return $("<div></div>").addClass("_Con_Only_List").html(o1).prop("outerHTML");
        }
    };

    var _SurveyCategory = {};
    _SurveyCategory.init = function () {
        var params = { action: 'GetSurveyCamCategory' };
        $.mask.show();
        $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
            $.mask.hide();
            if ($.checkErrCode(res)) {
                _SurveyCategory.init.$SurveyCata = res.SurveyCata
                _SurveyCategory.initBind();
            }
        }, "json");
    }
    _SurveyCategory.initBind = function () {
        if (!_SurveyCategory.init.$SurveyCata || _SurveyCategory.init.$SurveyCata.length <= 0) {
            return false;
        }
        $.each(_SurveyCategory.init.$SurveyCata, function (i, o) {
            var $panel = $("<div class='_SurveyCata" + i + "' _index=" + o.value + "><ul id='_SurveyCataCam" + i + "'></ul></div>");
            $('#_SurveyCata_s').accordion('add', {
                title: o.text,
                content: $panel,
                selected: false
            });
        });
        $('#_SurveyCata_s').accordion({
            onSelect: function (title, index) {
                _SurveyCategory.onSelect(title, index);
            }
        });
    }
    _SurveyCategory.onSelect = function (title, index) {
        var CG_Type = $("._SurveyCata" + index).attr("_index");
        $("#_SurveyCataCam" + index).tree({
            formatter: function (node) {
                return node.CG_Title;
            },
            onClick: function (node) {
                _SurveyCam.RemoveAll();
                _SurveyCam.AddTab(node);
            }
        });
        var params = { action: 'GetSurveyCategoryCam', CG_Type: CG_Type };
        $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
            if ($.checkErrCode(res)) {
                _SurveyCategory.init.SurveyCataCam = res.SurveyCataCam;
                $("#_SurveyCataCam" + index).tree("loadData", res.SurveyCataCam);
            }
        }, "json");

    }

    var _SurveyCam = { $tabs: $("#c_tabs_camp") };
    _SurveyCam.loadTabData = function (node) {
        var index = _SurveyCam.getTabIndex(node.CG_Code);
        var $tab = _SurveyCam.$tabs.tabs('getTab', index);
        if (null == $tab) return;
        var $tabBody = $tab.panel('body');
        $.post(_s_url, { o: JSON.stringify({ action: 'Get_Survey', CG_Code: node.CG_Code }) }, function (data) {
            if (data.survey.length > 0) {
                _SurveyCam.$Data = null;
                _SurveyCam.$Data = data.survey;
                var _html_tool = _html._body.getBody_Tools();
                var _html_body = _SurveyCam.SetData();
                _html_body = $("<div></div>").addClass("_Con_II").html(_html_body).prop("outerHTML");
                $("<div></div>").addClass("_Con_Cen").attr("_CG_Code", node.CG_Code).html(_html_tool + _html_body).appendTo($tabBody);
            } else {
                //$tabBody.append($("#isTest").html());
                //$tabBody.append("没有问卷数据！");
                var _html_tool = _html._body.getBody_Tools();
                var _html_body = $("<div></div>").addClass("_Con_II").prop("outerHTML");
                $("<div></div>").addClass("_Con_Cen").attr("_CG_Code", node.CG_Code).html(_html_tool + _html_body).appendTo($tabBody);
            }
        }, "json");
    };
    _SurveyCam.SetData = function () {
        if (!_SurveyCam.$Data || _SurveyCam.$Data.length <= 0) {
            return "";
        }
        var _survey = _SurveyCam.$Data;
        var _htm = "";
        for (var i = 0; i < _survey.length; i++) {
            var _o = _survey[i];
            var _ul = "";
            var _HeaderDatd = _html._show.getParentHtml(_o.SQ_Question);
            var _HeaderHtml = _html._header.getHeader(_HeaderDatd);
            if ($.trim(_o.SF_Text) == "") {
                _ul = _html._body.getBoay_Content("<ul></ul>");
                _ul = _html._body.getBody(_html._body.getBody_ToolsI(i) + _ul);
                _htm += _html.getCon_List(_HeaderHtml + _ul);
                continue;
            }
            var _s_text = _o.SF_Text.split("||");
            var _s_value = _o.SF_Value.split("||");
            var _s_Types = _o.SF_Types.split("||");
            var _s_Cout = _s_Types.length;
            if (_s_Types[_s_Types.length - 1] == 3 || _s_Types[_s_Types.length - 1] == 4) {
                _s_Cout = _s_Types.length - 1;
            }

            for (var j = 0; j < _s_Cout; j++) {
                var _bodyData = _html._show.getChildHtml(_s_text[j] + "&nbsp;&nbsp;&nbsp;" + _s_value[j]);
                var _bodyHtml = _html._body.getLi(_bodyData);
                _ul += _bodyHtml;
            }
            _ul = _html._body.getUL(_ul);
            _ul = _html._body.getBoay_Content(_ul);
            _ul = _html._body.getBody(_html._body.getBody_ToolsI(i) + _ul);
            _htm += _html.getCon_List(_HeaderHtml + _ul);
        }
        return _htm;
    }
    _SurveyCam.AddTab = function (node) {
        var index = _SurveyCam.getTabIndex(node.CG_Code);
        if (index >= 0) {
            _SurveyCam.$tabs.tabs('select', index);
        } else {
            _SurveyCam.$tabs.tabs('add', {
                id: node.CG_Code,
                title: $.trim(node.CG_Title),
                closable: true
            });
            _SurveyCam.loadTabData(node);
        }
    };
    _SurveyCam.Remove = function () {
        var tab = _SurveyCam.$tabs.tabs('getSelected');
        if (tab) {
            var index = _SurveyCam.$tabs.tabs('getTabIndex', tab);
            if (index != 0) {
                _SurveyCam.$tabs.tabs('close', index);
            }
        }
    },
    _SurveyCam.RemoveAll = function () {
        _SurveyCam.$Data = null;
        var tabs = _SurveyCam.$tabs.tabs('tabs');
        if (tabs) {
            $.each(tabs, function (i, tab) {
                var index = _SurveyCam.$tabs.tabs('getTabIndex', tab);
                if (index >= 0) {
                    _SurveyCam.$tabs.tabs('close', index);
                }
            });           
        }
    }
    _SurveyCam.getTabIndex = function (id) {
        var tabIndex = -1;
        var tabs = _SurveyCam.$tabs.tabs('tabs');
        for (var i = 0, len = tabs.length; i < len; i++) {
            var tab = tabs[i];
            if (tab[0].id == id) {
                tabIndex = _SurveyCam.$tabs.tabs('getTabIndex', tab);
                break;
            }
        }
        return tabIndex;
    },
    $(function () {
        _SurveyCategory.init();

    });
</script>
