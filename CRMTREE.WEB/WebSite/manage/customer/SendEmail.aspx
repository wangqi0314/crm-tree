<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendEmail.aspx.cs" Inherits="manage_customer_SendEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/css/SendMail.css" rel="stylesheet" />
    <script src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script type="text/javascript">
        $("#TextArea1").keydown(function () {
            alert("dd");
        });
    </script>
    <title></title>
    <style type="text/css">
        body {
            margin: 0px;
            padding: 0px;
            font-family: Arial;
            font-size: 15px;
        }

        .mailTop {
            background-color: #F0F2F4;
            margin: 0px;
            padding: 2px;
            line-height: 25px;
        }
    </style>
</head>
<body ">
    <div>
        <div class="mailTop">
            <div class="mailRecipient ">
                <label style="font-weight: bold">Recipient address: </label>
                <label class="mailRecipientInfo">XXXXX_455@163.com</label>
            </div>
            <div class="mailTitle">
                <label style="font-weight: bold">Message header: </label>
                <input id="mailTitleInfo" type="text" style="width: 300px;" /></div>
        </div>
        <div class="mailContent">
            <textarea id="TextArea1" style="width:600px;height:320px;" maxlength="800" ></textarea>
        </div>
        <div class="mailBottom" style="text-align:right;color:#A9A9AA">Please input 500 words contents</div>
    </div>
</body>
</html>
