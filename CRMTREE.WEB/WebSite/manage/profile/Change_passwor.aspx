<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Change_passwor.aspx.cs" Inherits="manage_profile_List_Profile" %>

<%@ Register Src="~/control/top.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/control/bottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <%--    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <link href="/styles/global/main.css?t=20110530" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>--%>
    <title><%= Resources.CRMTREEResource.MyProfile %></title>
    <script type="text/javascript">
        function login() {
            var OPW = $.trim($("#TextOPW").val());
            var NPW = $.trim($("#TextNPW").val());
            var CPW = $.trim($("#TextCPW").val());
            if (OPW == "") {
                $(".TextOPW").html("The old password can not be empty!");
                $(".TextNPW").html("");
                $(".TextCPW").html("");
                $("#TextOPW").focus();
                return false;
            }
            if (NPW == "") {
                $(".TextOPW").html("");
                $(".TextNPW").html("The new password cannot be blank!");
                $(".TextCPW").html("");
                $("#TextNPW").focus();
                return false;
            }
            if (CPW == "") {
                $(".TextOPW").html("");
                $(".TextNPW").html("");
                $(".TextCPW").html("Repeat password can not be empty!");
                $("#TextCPW").focus();
                return false;
            }
            if (NPW != CPW) {
                $(".TextOPW").html("");
                $(".TextNPW").html("");
                $(".TextCPW").html("The two password is not correct!");
                return false;
            }
            $(".TextOPW").html("");
            $(".TextNPW").html("");
            $(".TextCPW").html("");
            $.ajax({
                type: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                url: "Change_passwor.aspx/PasswordChang",
                data: "{ OPW:'" + OPW + "', NPW: '" + NPW + "' }",
                success: function (data) {
                    if (data.d == 1) {
                        $(".password_suresPrompt lable").html("Password updated successfully");
                        $(".password_suresPrompt").show();
                    }
                    else if (data.d == 2) {
                        $(".TextOPW").html("The password input error!");
                        $(".TextNPW").html("");
                        $(".TextCPW").html("");
                    }
                    else {
                        $(".TextOPW").html("Password update failed!");
                        $(".TextNPW").html("");
                        $(".TextCPW").html("");
                    }
                }
            });
        }
    </script>
    <style type="text/css">
        .password_suresPrompt{ position: relative;
            left: 200px;height:45px; width: 600px;display:none}
        .Cont_Cont {
            position: relative;
            left: 200px;
            border: 1px solid #d2d2d2;
            background-color: aliceblue;
            width: 600px;
            height: 170px;
            border-radius: 2px 2px 2px 2px;
            line-height: 2.5;
            margin: 0px;
            padding: 0px;
        }

        .Cont_Titel {
            text-align: center;
            font: bold;
        }

        .Cont_Info {
            border-bottom: 1px solid #d2d2d2;
            height: 30px;
        }

        .inputs {
            margin-top: 5px;
            height: 24px;
        }

        .Cont_lsft {
            width: 150px;
            margin: 0px;
            padding: 0px;
            float: left;
            text-align: right;
        }

        .Cont_right {
            width: 400px;
            float: left;
            height: 29px;
        }

        .Btns {
            background: url(/images/menu_submit.png) no-repeat;
            background-size: 96px 25px;
            width: 96px;
            height: 25px;
            color: #fff;
            border: none;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <div id="container">
        <uc1:top ID="top1" runat="server" />
        <div id="content">
            <div class="nav_infor">
                <a class="brown" href="#"><%= Resources.CRMTREEResource.MyProfile %></a>&nbsp;&gt;&nbsp;<%= Resources.CRMTREEResource.MyProfile1 %>
            </div>
            <div class="cont_box" style="padding:0px;">
                <div style="height:45px;">
                <div class="password_suresPrompt"><img src="/images/icon/succeed.png" /><lable style="position:relative;top:-15px;"></lable></div></div>
                <div class="Cont_Cont">
                    <div class="Cont_Titel"><b><%= Resources.CRMTREEResource.MyProfile1 %></b></div>
                    <div class="Cont_Info">
                        <div class="Cont_lsft">
                            <span class="red">*</span><span><%= Resources.CRMTREEResource.MyProfile2 %></span>
                        </div>
                        <div style="">
                            <input class="input_text inputs" id="TextOPW" runat="server" type="password" />
                            <span class="red TextOPW"></span>
                        </div>
                    </div>
                    <div class="Cont_Info">
                        <div class="Cont_lsft">
                            <span class="red">*</span><%= Resources.CRMTREEResource.MyProfile3 %>
                        </div>
                        <div class="Cont_right">
                            <input class="input_text inputs" id="TextNPW" runat="server" type="password" />
                            <span class="red TextNPW"></span>
                        </div>
                    </div>
                    <div class="Cont_Info">
                        <div class="Cont_lsft">
                            <span class="red">*</span><%= Resources.CRMTREEResource.MyProfile4 %>
                        </div>
                        <div class="Cont_right">
                            <input class="input_text inputs" id="TextCPW" runat="server" type="password" />
                            <span class="red TextCPW"></span>
                        </div>
                    </div>
                    <div class="Cont_Titel">
                        <div style="margin-top: 10px;">
                            <input class="Btns" type="button" value="<%= Resources.CRMTREEResource.MyProfile5 %>" id="btnLogin" onclick="login()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc2:bottom ID="bottom1" runat="server" />
    </div>
    <script type="text/javascript">
        $("#nav_MyProfile").addClass("nav_select");
    </script>
</body>
</html>
