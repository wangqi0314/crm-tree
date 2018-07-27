<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTest.aspx.cs" Inherits="wechat_frmTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="/wechatjs/jquery-1.10.2.min.js"></script>
    <script src="/wechatjs/comm/wechatBase.js"></script>
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    
    <script src="/scripts/jquery/jquery.extend.js"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js"></script>
    <style>
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button3" runat="server" Text="微信测试按钮" OnClick="Button2_Click" />
            <br />
            <br />
        </div>
        <div>
            <asp:Button ID="btn_GetOAuthUrl" runat="server" Text="获取认证链接" OnClick="btn_GetOAuthUrl_Click" />
            <asp:TextBox ID="txt_url" runat="server" Width="470px"></asp:TextBox>
        </div>
        <div id="_d"></div>
    </form>
    <div id="pp" style="background:#efefef;border:1px solid #ccc;"></div>
</body>
</html>
    <script>
        $(function () {
            $('#pp').pagination({
                total: 2000,
                pageSize: 40
            });
        });
    </script>
