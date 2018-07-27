<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTMLOpener.aspx.cs" Inherits="manage_Generic_HTMLOpener" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <script src="/scripts/common/json2.js" type="text/javascript"></script>

    <%--<META http-equiv="refresh" content="0;url=/plupload/file/<%=Request.QueryString["FName"] %>">--%>

    <style type="text/css">
        html, body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            font-size:12px;
        }
    </style>
</head>
<body>
    <div id="c_campaign_file" style=" width:690px;"></div>
</body>
</html>

<script type="text/javascript">
    var _s_url = '/handler/Customer/CustomerService.aspx';
    var _params = $.getParams();

    var cg = _params.CG_Code > 0 ? _params.CG_Code : (_params.CG > 0 ? _params.CG : 0);

    var au = _params.AU_Code > 0 ? _params.AU_Code : (_params.AU > 0 ? _params.AU : 0);

    var fName = $.trim(_params.FName);

    $(function () {
        var params = {
            action: fName != '' ? 'GetFileContentByFileName' : 'GetFileContent',
            FileName: fName,
            CG_Code: cg,
            AU_Code: au
        };
        $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
            if (res) {
                $("#c_campaign_file").html($.trim(res));
            }
        }, "html");
    });
</script>
