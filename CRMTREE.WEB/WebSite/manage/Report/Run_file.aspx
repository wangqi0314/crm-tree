<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Run_file.aspx.cs" Inherits="manage_Report_Run_file" %>

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
</head>
<body style="margin: 0px; padding: 0px">
    <div style="width: 100%; padding: 4px; margin: 0px">
        <input id="_user_list" class="easyui-combobox" style="" />
    </div>
    <hr />
    <div class="_html"></div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Campaign/CampaignManager.aspx';
    var _params = $.getParams();

    $(function () {
        var s_params = JSON.stringify({ action: 'Get_run_user_list', CG_Code: _params.CG_Code });
        $.post(_s_url, { o: s_params }, function (res) {
            if ($.checkErrCode(res)) {
                $('#_user_list').combobox({
                    valueField: 'AU_Code',
                    textField: 'AU_Name',
                    onSelect: function (record) {

                        var s_params = JSON.stringify({ action: 'Get_run_file_list', AU_Code: record.AU_Code, file_url: _params.file_url });
                        $.post(_s_url, { o: s_params }, function (res) {
                            if ($.checkErrCode(res)) {
                                var _html = unescape(res);
                                $("._html").html(_html);
                            }
                        }, "json");
                    }
                }).combobox("loadData", res);
            }
        }, "json");
        var s_params = JSON.stringify({ action: 'Get_run_file_list', file_url: _params.file_url });
        $.post(_s_url, { o: s_params }, function (res) {
            if ($.checkErrCode(res)) {
                var _html = unescape(res);
                $("._html").html(_html);
            }
        }, "json");
    });
</script>
