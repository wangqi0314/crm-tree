<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HtmlFile.aspx.cs" Inherits="manage_campaign_HtmlFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <title></title>
    <script type="text/javascript">
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        $(document).ready(function () {
            var User_Code = getUrlParam("User_Code");
            var CG_Code = getUrlParam("CG_Code");
            var filename = getUrlParam("filename");
            $.ajax({
                type: "POST", dataType: 'json', contentType: "application/json; charset=utf-8",
                url: "/handler/ajax_CampaignBeneficiary.aspx/SendPhone",
                data: "{User_Code:'" + User_Code + "',CG_Code:'" + CG_Code + "',filename:'" + filename + "'}",
                success: function (data) {
                    if (data.d != null || data.d != "-1") {
                        $(".fileContent").html(data.d);
                    }
                }
            });
           
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="fileContent">
        </div>
    </form>
</body>
</html>
