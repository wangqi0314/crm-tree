<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/ShunovoLogin.ascx" TagPrefix="uc1" TagName="CrmTreetop" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= Resources.CRMTREESResource.LoginTitel%></title>
    <link type="text/css" href="css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
   <script type="text/javascript" src="/scripts/common/setCookie.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <uc1:CrmTreetop runat="server" ID="CrmTreetop" />
            <div id="cont_login">
                 <div class="log_title" style="font-size:16px; font-weight:bold;"><%=Resources.CRMTreeLogin.Services_Title %></div>
                <div class="lg_Servbox">
                    <div class="Body" style="width:990px; font-size:15px;color:black;padding:10px;"><%=Resources.CRMTreeLogin.Services_Cont0 %></div>
                    <br />
                    <ul>
                       <li>
                            <div class="Title" > <%=Resources.CRMTreeLogin.Services_Title1 %></div>
                            <div class="Body"><%=Resources.CRMTreeLogin.Services_Cont1 %></div>
                       </li>
                       <li>
                            <div class="Title"> <%=Resources.CRMTreeLogin.Services_Title2 %></div>
                            <div class="Body"><%=Resources.CRMTreeLogin.Services_Cont2 %></div>
                       </li>
                       <li>
                            <div class="Title"> <%=Resources.CRMTreeLogin.Services_Title3 %></div>
                            <div class="Body"><%=Resources.CRMTreeLogin.Services_Cont3 %></div>
                       </li>
                       <li>
                            <div class="Title"> <%=Resources.CRMTreeLogin.Services_Title4 %></div>
                            <div class="Body"><%=Resources.CRMTreeLogin.Services_Cont4 %></div>
                       </li>
                    </ul>
                 </div>
            </div>
                
             <div class="bottom" style="border: 1px solid #d2d2d2; background-color: #F5EDD1; height: 20px; margin-bottom: 10px; text-align: center; color: #EBEBEB; text-decoration: none;">
                 <a href="Login.aspx"><%=Resources.CRMTREESResource.LoginHome %></a> &nbsp;&nbsp;
                 <a href="About.aspx"><%=Resources.CRMTREESResource.LoginAbout %></a>&nbsp;&nbsp;
                 <a href="Products.aspx"><%=Resources.CRMTREESResource.LoginProducts %></a>&nbsp;&nbsp;
                 <a href="Services.aspx"><%=Resources.CRMTREESResource.LoginServices %></a>&nbsp;&nbsp;
<%--                 <a href="Contactus.aspx"><%=Resources.CRMTREESResource.LoginContact %></a>
                 <a href="Privacy.aspx"><%=Resources.CRMTREESResource.LoginPrivacy %></a>&nbsp;&nbsp;--%>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
