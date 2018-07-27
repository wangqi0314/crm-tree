<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveyAnswerIndex.aspx.cs" Inherits="manage_survey_SurveyAnswerIndex" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/styles/ST_001.css" rel="stylesheet" />
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <link href="/styles/ST_001.css" rel="stylesheet" />
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
        <div data-options="region:'center'" style="padding: 0px">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'center',border:false" style="padding: 5px; overflow-y: auto;">
                    <div class="st_table" style="display: none">
                        <table>
                            <tr>
                                <th></th>
                                <th>1</th>
                                <th>1</th>
                                <th>1</th>
                                <th>1</th>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td><span title="1">2</span>2</td>
                                <td>2</td>
                                <td>2</td>
                                <td>2</td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>2</td>
                                <td>2</td>
                                <td>2</td>
                                <td>2</td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>2</td>
                                <td>2</td>
                                <td>2</td>
                                <td>2</td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>2</td>
                                <td>2</td>
                                <td>2</td>
                                <td>2</td>
                            </tr>
                        </table>
                    </div>
                    <hr />
                    <div id="st_table" class="st_table"></div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
<script>
    var _s_url = "/handler/Han_Survey.aspx";
    var _params = $.getParams();
    var myTbale = {
        _table: $("<table></table>"),
        Title: ["题号", "题目", "回答", "备注"],
        Data: [["2", "2", "2", "2"], ["2", "2", "2", "2"], ["2", "2", "2", "2"], ["2", "2", "2", ""]],
        CreateTitle: function () {
            var _tr = $("<tr></tr>");
            _tr.append("<th></th>");
            $.each(this.Title, function (i, o) {
                _tr.append("<th>" + o + "</th>");
            });
            this._table.append(_tr);
        },
        CreateData: function () {
            var _td_Count = this.Title.length;
            if (this.Data.length == 0) {
                return $("<tr></tr>").html("<td>1</td><td colspan=\"" + this.Title.length + "\">没有找到数据！</td>").appendTo(myTbale._table);;
            }
            $.each(this.Data, function (i, o) {
                var _td = "<td>" + (i + 1) + "</td>";
                for (var i = 0; i < _td_Count; i++) {
                    _td += "<td><span title=\"" + o[i] + "\">" + o[i] + "</span></td>";
                }
                $("<tr></tr>").html(_td).appendTo(myTbale._table);
            });
        },
        Create: function () {
            this.CreateTitle();
            this.CreateData();
            return this._table.prop("outerHTML");
        }
    };
    var _survey = {
        dataHander: function () {
            if (_survey.$data == null) {
                return [];
            }
            if (_survey.$data.t1.length == 0) {
                return [];
            }
            var _answer = [];
            var _note = [];
            if (_survey.$data.t2 != 0) {
                _answer = _survey.$data.t2[0].SR_Answers.split("||");
                _note = _survey.$data.t2[0].SR_Notes.split("||");
            }
            var _data = new Array();
            $.each(_survey.$data.t1, function (i, o) {
                var _o = new Array();
                _o.push("Q" + (i + 1));
                _o.push(o.SQ_Question);
                _o.push(_survey.dataForm(o, _answer[i]));
                _o.push((_note[i])?_note[i] : "");
                _data.push(_o);
            });
            return _data;
        },
        dataForm: function (o, value) {
            if ($.trim(o.SF_Text) == "") {
                return "";
            }
            if ($.trim(o.SF_Value) == "") {
                return "";
            }
            var _SF_Text = o.SF_Text.split("||");
            var _SF_Value = o.SF_Value.split("||");
            var _index = -1;
            for (var i = 0; i < _SF_Value.length; i++) {
                if (value == _SF_Value[i]) {
                    _index = i;
                }
            }
            if (_index == -1) {
                return "";
            } else {
                return _SF_Value[_index];
            }
        }
    };
    $(function () {
        $.post(_s_url, { o: JSON.stringify({ action: 'GetSurveyAnswer', HD_Code: _params.HD }) }, function (data) {
            if ($.checkErrCode(data)) {
                _survey.$data = data;
                myTbale.Data = _survey.dataHander();
                $("#st_table").append(myTbale.Create());

            }
        }, "json");
    });
</script>
