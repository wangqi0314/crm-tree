<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveyList.aspx.cs" Inherits="templete_survey_SurveyList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/extendCss.css" rel="stylesheet" />

    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _isEn = $.isEn();
        if (!_isEn) {
            document.write('<script src="/scripts/jquery-easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></sc' + 'ript>');
        }
    </script>
</head>
<body style="height: 100%">
    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'center',height:'90%',border:true" style="padding: 0px; height: auto">
            <div id="all_Survey">
                <%--<div class="_Con_Only_List">
                    <div class="_Con_Header">
                        <div class="_Con_Show"><span>1.wenjuan调查</span></div>
                    </div>
                    <div class="_Con_Body">
                        <div class="_Con_Body_Tools">
                            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-down'">Next</a>
                            <a href="#network" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-up'">Prev</a>
                        </div>
                        <div class="_Con_Body_Content">
                            <div class="e_ui_all">
                                <div class="e_ui_list">
                                    <span class="I">
                                        <input type="checkbox" /></span>
                                    <span>ddddddddddddd</span>
                                    <span class="I_3">分数：<span>100</span></span>
                                </div>
                                <div class="e_ui_listII">
                                    <span class="I">备注:</span>
                                    <span class="I_3">
                                        <input class="easyui-textbox" data-options="multiline:true,height:44" style="width: 300px" /></span>
                                </div>
                            </div>
                    </div>
                 </div>
            </div>--%>
            </div>
        </div>
        <div class="_p_Lin_btn" style="display: block">
            <div data-options="region:'south',height:'10%',border:true" style="padding: 5px; text-align: center">
                <div class="Lin_btn">
                    <a class="easyui-linkbutton" id="btnSave" data-options="iconCls:'icon-save',onClick:Survey.Save" style="width: 80px;"><%=Resources.CRMTREEResource.ap_buttons_save%></a>
                </div>
            </div>
        </div>
        <div style="display: none">
            <div id="c_Nextaction" style="width: 150px; height: 24px; padding: 1px;">
                <a class="easyui-linkbutton" id="btnNext" data-options="plain:true,iconCls:'icon-down',onClick:Survey.Next"><%=Resources.CRMTREEResource.ap_buttons_Next%></a>
                <a class="easyui-linkbutton" id="btnPrev" data-options="plain:true,iconCls:'icon-up',onClick:Survey.Prev"><%=Resources.CRMTREEResource.ap_buttons_Prev%></a>
            </div>
        </div>
    </div>
    <input type="radio"  />
</body>
</html>
<script type="text/javascript">
    var _s_url = "/handler/Han_Survey.aspx";
    var _params = $.getParams();
    var CurIndx = 0;
    var toolT = 0;
    function isArray(obj) {
        return Object.prototype.toString.call(obj) === '[object Array]';
    }
    function S_Goto(moveI) {
        CurIndx = CurIndx + moveI;
        if (CurIndx < 0) CurIndx = 0;
        toolT.tooltip('hide');
        if (moveI == 1) {
            $("#btnNext").attr("href", '#network' + CurIndx);
        } else {
            $("#btnPrev").attr("href", '#network' + CurIndx);
        }
    }

    var Survey = {};
    Survey.Next = function () {
        S_Goto(1);
    }
    Survey.Prev = function () {
        S_Goto(-1);
    }
    Survey.returns = function (_notAnswer) {
        var _index = _notAnswer[0];
        window.location.hash = "#network" + _index;
        return false;
    },
    Survey.Check2 = function () {
        if ($.trim(_params.CG_Code) == "") {
            return false;
        }
        if ($.trim(_params.DE_Code) == "") {
            return false;
        }
        if ($.trim(_params.AU_Code) == "") {
            return false;
        }
        if (!Survey.$Data.survey) {
            return false;
        }
        var _s_data = Survey.$Data.survey;
        var _notAnswer = [];
       
        for (var i = 0; i < _s_data.length; i++) {
            if (!_s_data[i].SR_Answers && i != 2) {
                _notAnswer.push(i);
            }
        }
        return _notAnswer;
    };
    Survey.Check = function () {
        if ($.trim(_params.CG_Code) == "") {
            return false;
        }
        if ($.trim(_params.DE_Code) == "") {
            return false;
        }
        if ($.trim(_params.AU_Code) == "") {
            return false;
        }
        var _notAnswer = [];
        var _s_data = Survey.$Data.survey;
        for (var i = 0; i < _s_data.length; i++) {
            if (!_s_data[i].SR_Answers && i != 2) {
                _notAnswer.push(i);
            }
        }
        if (_notAnswer.length == 0) {
            return true;
        } else {
            $.confirmWindow.survey(_notAnswer.length, function (isTrue) {
                if (isTrue) {
                    var _index = _notAnswer[0];
                    _notAnswer = [];
                    window.location.hash = "#network" + _index;
                    return false;
                } else {
                    return true;
                }
            });
        }
    }
    Survey.Save = function () {
        if ($.trim(_params.CG_Code) == "") {
            return false;
        }
        if ($.trim(_params.DE_Code) == "") {
            return false;
        }
        if ($.trim(_params.AU_Code) == "") {
            return false;
        }
        var _notAnswer = [];
        var _s_data = Survey.$Data.survey;
        var _Answers = "", _Notes = "";
        for (var i = 0; i < _s_data.length; i++) {
            _Answers += (_s_data[i].SR_Answers) ? _s_data[i].SR_Answers + "||" : " ||";
            _Notes += (_s_data[i].SR_Notes) ? _s_data[i].SR_Notes + "||" : " ||";
            if (!_s_data[i].SR_Answers && i!=2) {
                _notAnswer.push(i);
            }
        }
        //if (_notAnswer.length == 0) {
        if (true) {
            var _save_d = {
                action: 'Save_Survey',
                CG_Code: _params.CG_Code,
                SR_AU_Code: _params.AU_Code,
                SR_Advisor: _params.DE_Code,
                SR_Answers: _Answers.substring(0, _Answers.length - 2),
                SR_Notes: _Notes.substring(0, _Notes.length - 2)
            };
            $.post(_s_url, { o: JSON.stringify(_save_d) }, function (data) {
                if ($.checkErrCode(data)) {
                    if (data == 0) {
                        // $.msgTips.save(true);
                    } else {
                        // $.msgTips.save(false);
                    }
                }
            }, "json");
        } else {
            $.confirmWindow.survey(_notAnswer.length, function (isTrue) {
                if (isTrue) {
                    var _index = _notAnswer[0];
                    _notAnswer = [];
                    window.location.hash = "#network" + _index;
                } else {
                    var _save_d = {
                        action: 'Save_Survey',
                        CG_Code: _params.CG_Code,
                        SR_AU_Code: _params.AU_Code,
                        SR_Advisor: _params.DE_Code,
                        SR_Answers: _Answers.substring(0, _Answers.length - 2),
                        SR_Notes: _Notes.substring(0, _Notes.length - 2)
                    };
                    $.post(_s_url, { o: JSON.stringify(_save_d) }, function (data) {
                        if ($.checkErrCode(data)) {
                            if (data == 0) {
                                // $.msgTips.save(true);
                            } else {
                                // $.msgTips.save(false);
                            }
                        }
                    }, "json");
                }
            });
        }
    }
    Survey.ChangeVal = function (obj) {
        var _o = $(obj), _d_v_t = Survey.$Data.survey;
        var _o_value = _o.val();
        var _o_index = _o.attr("_index");
        var _o_sType = _o.attr("_sType");
        if (_o_sType == 1) {
            Survey.$Data.survey[_o_index].SR_Answers = _o_value;
        } else if (_o_sType == 3) {
            Survey.$Data.survey[_o_index].SR_Notes = _o_value;
        }
        CurIndx = parseInt(_o_index);
        $(obj).tooltip({
            content: function () {
                return $('#c_Nextaction');
            },
            position: 'bottom',
            showEvent: 'click',
            onShow: function () {  //NEXT PAGE
                toolT = $(obj);
                toolT.tooltip('tip').unbind().bind('mouseenter', function () {
                    toolT.tooltip('show');
                }).bind('mouseleave', function () {
                    toolT.tooltip('hide');
                });
            }
        }).tooltip('show');



    }
    Survey._HTML = {};
    Survey._HTML.setFromInput = function (index, data) {
        try {
            var _d_Type = data.SF_Types.split("||");
            var _d_Text = data.SF_Text.split("||");
            var _d_ValType = data.SF_ValType.split("||");
            var _d_Value = data.SF_Value.split("||");
            var _w_Answer = [];
            var _w_Note = [];
            if (Survey.$Data.answer && Survey.$Data.answer.length >0) {
                _w_Answer = Survey.$Data.answer[0].SR_Answers.split("||");
                _w_Note = Survey.$Data.answer[0].SR_Notes.split("||");
            }
        }
        catch (e) {
            return "";
        }
        if (_d_Type.length != _d_Text.length) {
            return "没有默认回答模板！";
        }
        var _html = "";
        for (var i = 0; i < _d_Type.length; i++) {
            var _t = "", _text = "", _valT = "", _temHtml ="";
            if (_d_Type[i] == 1 || _d_Type[i] == 2) {
                if (_d_Type[i] == 1) {
                    
                    if (_w_Answer[index] == _d_Value[i]) {
                        _t = "<input name=\"" + data.SQ_Code + "\" type=\"radio\" checked = \"checked\" _sType=\"1\" _index=\"" + index + "\" value=\"" + _d_Value[i] + "\" onchange=\"Survey.ChangeVal(this)\" />";
                        Survey.$Data.survey[index].SR_Answers = _d_Value[i];
                        
                    } else {
                        _t = "<input name=\"" + data.SQ_Code + "\" type=\"radio\" _sType=\"1\" _index=\"" + index + "\" value=\"" + _d_Value[i] + "\" onchange=\"Survey.ChangeVal(this)\" />";
                    }
                } else if (_d_Type[i] == 2) {
                    _t = "<input name=\"" + data.SQ_Code + "\" type=\"checkbox\" _sType=\"2\" _index=\"" + index + "\" value=\"" + _d_Value[i] + "\" onchange=\"Survey.ChangeVal(this)\" />";
                }
                _text = _d_Text[i];
                _valT = "分数： " + _d_Value[i];
                _temHtml = "   <div class=\"e_ui_list\">"
+ "                                   <span class=\"I\">" + _t + "</span>"
+ "                                   <span>" + _text + "</span>"
+ "                                   <span class=\"I_3\">" + _valT + "</span>"
+ "                                </div> ";
            } else if (_d_Type[i] == 3) {
                var _note = _w_Note[index] ? _w_Note[index] : "";
                Survey.$Data.survey[index].SR_Notes = _note;
                _t = "<textarea name=\"" + data.SQ_Code + "\" style=\"width:200px;height:40px;\" _sType=\"3\" _index=\"" + index + "\" onblur=\"Survey.ChangeVal(this)\">" + _note + "</textarea>";
                _text = _d_Text[i];
                _temHtml = "   <div class=\"e_ui_listII\" >"
+ "                                   <span class=\"I\">" + _text + ":</span>"
+ "                                   <span class=\"I_3\">" + _t + "</span>"
+ "                                </div> ";
            }
            _html += _temHtml;
        }
        return _html;
    };
    Survey._HTML.setHTML = function (index, data) {
        var _html = "<div class=\"_Con_Only_List\">"
                   + "       <div id=\"network" + index + "\" class=\"_Con_Header\">"
                   + "          <div class=\"_Con_Show\"><span>" + data.SQ_Question + "</span></div>"
                   + "       </div>"
                   + "       <div class=\"_Con_Body\">"
                   + "                        <div class=\"_Con_Body_Tools\">"
                   + "         <a href=\"#network" + (index + 1) + "\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-down'\">下一题</a>"
                   + "         <a href=\"#network" + (index - 1) + "\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-up'\">上一题</a>"
                   + "     </div>"
                   + "           <div class=\"_Con_Body_Content\">"
                   + "             <div class=\"e_ui_all\" _index=\"" + index + "\">"
+ Survey._HTML.setFromInput(index, data);
        + "             </div>"
        + "           </div>"
        + "       </div>";
        + "       </div>";
        return _html;
    };
    Survey._HTML.getHTML = function () {
        if (!Survey.$Data || !Survey.$Data.survey.length) {
            $("#all_Survey").append("没有问卷数据！");
            $(".Lin_btn").hide();
            return false;
        }
        var _setData = Survey.$Data.survey;
        $("#all_Survey").empty();
        //var st = new Date().getTime();
        for (var i = 0; i < _setData.length; i++) {
            var _html = Survey._HTML.setHTML(i, _setData[i]);
            $("#all_Survey").append(_html);
            $.parser.parse("#all_Survey");
        }
        //var st2 = new Date().getTime() - st;
        //var _s = st2;
    };
    $(function () {


        function initData(CG_Code,AU_Code,DE_Advosor) {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Survey', CG_Code: CG_Code, AU_Code: AU_Code, DE_Advosor: DE_Advosor }) }, function (data) {
                if (data) {
                    Survey.$Data = data;
                    Survey._HTML.getHTML();
                } else {
                    $("#all_Survey").append("没有问卷数据！");
                    $(".Lin_btn").hide();
                }
            }, "json");
        }

        (function Init() {
            if ($.trim(_params._cmd) != "SurveyIndex") {
                $("._p_Lin_btn").hide(); 
            }
            if ($.trim(_params.CG_Code) != ""&& $.trim(_params.AU_Code) != ""&& $.trim(_params.DE_Code) != "") {
                initData(_params.CG_Code, _params.AU_Code, _params.DE_Code);
            } else {
                $("#all_Survey").append("没有问卷数据！");
                $(".Lin_btn").hide();
            }
        })();

    });
</script>
