<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubDataGrid.aspx.cs" Inherits="templete_report_SubDataGrid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script src="/scripts/jquery/jquery.extend2.js"></script>

    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js"></script>
    <script src="/scripts/jquery-easyui/datagrid-detailview.js"></script>

    <script src="/scripts/manage/Report/SubDataGrid.js"></script>
    <script type="text/javascript">
        var _isEn = $.isEn();
        if (!_isEn) {
            document.write('<script src="/scripts/jquery-easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></sc' + 'ript>');
        }
        var _pageParam = { a: 1 };
    </script>
</head>
<body>
    <div id="pnl" class="easyui-panel" fit="true" border="false">
        <table id="dg_2"></table>
    </div>
</body>
</html>
