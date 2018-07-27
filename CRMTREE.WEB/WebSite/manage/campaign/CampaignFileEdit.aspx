<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampaignFileEdit.aspx.cs" Inherits="manage_campaign_CampaignFileEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/ueditor/ueditor.all.js"></script>

    <script src="/scripts/common/json2.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    
</head>
<body style="margin: 0px; padding: 0px;">
        <form id="form1" runat="server">
            <div id="EditFile" style="width: 760px; height: 555px; margin: 0px; padding: 0px;"></div>
        </form>

    <script type="text/javascript">
        var ue = UE.getEditor(
        'EditFile', {
            toolbars: [['fullscreen', 'source', '|', 'undo', 'redo', '|', 'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', 'removeformat', 'formatmatch', 'autotypeset', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', 'selectall', 'cleardoc', '|', 'rowspacingtop', 'rowspacingbottom', 'lineheight', '|', 'directionalityltr', 'directionalityrtl', 'indent', '|', 'customstyle', 'paragraph', 'fontfamily', 'fontsize', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'touppercase', 'tolowercase', '|', 'link', 'unlink', '|', 'imagenone', 'imageleft', 'imageright', 'imagecenter', '|', 'simpleupload', 'insertimage', 'attachment', 'map', 'background', '|', 'horizontal', 'date', 'time', 'spechars', '|', 'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols', 'charts', '|', 'print', 'preview', 'help'
            ]], fullscreen: true, lang: "en"
        });
        ue.ready(function () {
            var filename = GetQueryString("filename");
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/handler/ajax_campaign.aspx",
                data: { action: "campaign_edit_file", fullname: filename },
                success: function (data) {
                    ue.setContent(data, false);
                }
            });
        });
        function getContent() {
            return ue.getContent();
        }
        function getContentTxt() {
            return ue.getContentTxt();
        }
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
        }
    </script>
</body>
</html>
