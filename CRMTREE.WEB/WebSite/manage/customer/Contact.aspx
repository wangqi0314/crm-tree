<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="StaticTemplate_Contact" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="uc1" TagName="CrmTreetop" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <link type="text/css" href="/css/Contact.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>
    <script type="text/javascript" src="/scripts/manage/Customer/Contact.js"></script>
</head>
<body>
    <div id="container">
        <uc1:CrmTreetop runat="server" ID="CrmTreetop" />
        <div id="content">
            <div class="nav_infor">Contact </div>
            <div class="Content_M">
                <div class="Content_top">Customer：Admin</div>
                <div class="Content_center">
                    <div class="Content_Titel_Con">
                        <div class="Content_Title">
                            <div class="Title Title1">
                                <div>Contact</div>
                            </div>
                            <div class="Title">
                                <div>Cars</div>
                            </div>
                            <div class="Title">
                                <div>Rewards</div>
                            </div>
                            <div class="Title">
                                <div>Service History</div>
                            </div>
                            <div class="Title">
                                <div>Dealers Used</div>
                            </div>
                            <div class="Title">
                                <div>Contact History</div>
                            </div>
                            <div class="Title">
                                <div>Hobbies</div>
                            </div>
                            <div class="Title">
                                <div>Current Campaigns</div>
                            </div>
                        </div>
                    </div>
                    <div class="ConText_info" style="height: 600px;">
                        <div class="Contact_Mobile">
                            <div class="MobileImage">
                                <img class="image" src="/images/Delephone.png" />
                            </div>
                            <div class="Mobile">
                                <div>Mobile:<lable class="Mob">18116351855</lable></div>
                                <div>Office:<lable class="Offi">021-8856341</lable></div>
                            </div>
                        </div>
                        <div class="Contact_Email">
                            <div class="EmailImage" style="float: left;">
                                <img style="width: 40px; padding: 0px; margin: 2px;" src="/images/mail.png" />
                            </div>
                            <div style="float: left; width: 150px; text-align: left; line-height: 1.8em;">
                                <div>Send email:</div>
                                <div class="email" style="text-decoration: underline; cursor: pointer;">XXXXX_455@163.com</div>
                            </div>
                        </div>
                        <div class="Contact_Timely">
                            <div class="TimelyImage" style="float: left;">
                                <img style="width: 40px; padding: 0px; margin: 2px;" src="/images/weixin.jpg" />
                            </div>
                            <div style="float: left; width: 150px; text-align: left; line-height: 1.8em;">
                                <div></div>
                                <div style="background: url(/images/SendMessage.jpg) no-repeat; height: 30px; cursor: pointer;">
                                    <label style="margin-top: 40px; cursor: pointer;">Send a Text Message</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc1:CrmTreebottom runat="server" ID="CrmTreebottom" />
    </div>
</body>
</html>
