<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetopLogin.ascx" TagPrefix="uc1" TagName="CrmTreetop" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Resources.CRMTREESResource.LoginTitel %></title>
    <link type="text/css" href="css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
   <script type="text/javascript" src="/scripts/common/setCookie.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <uc1:CrmTreetop runat="server" ID="CrmTreetop" />
            <div id="cont_login">
                 <div class="log_title" style="font-size:16px; font-weight:bold;"> Contact Us</div>
                    CRM Tree is a comprehensive, web-based CRM solution for automotive OEMs and dealerships. We provide easy-to-use, intuitive technologies and strategic services to create meaningful customer relationships that grow your business. Our team brings decades of expertise from California’s Silicon Valley and a deeply-rooted understanding of the Chinese auto market.
Our products and services
CRM Tree offers a comprehensive set of branches to help you grow.

Sales Branch
We provide insights about customer visits to your showrooms, customer interests, and customer ratings of your staff, products, and overall experience. 

Does your sales staff spend a good chunk of their day tracking customer forms from multiple touch points? Let CRM Tree manage it. The Sales Branch gives you full control to automate the sales CRM. Our tools make managing follow-up calls easy. An online appointment system manages sales department resources so you don’t have to.

Service Branch
From service reminders to appointment scheduling and service follow-ups, this branch of CRM Tree offers a full suite of automated support.

Campaign Branch
Design, execute, and manage campaigns based on customer preferences with our Campaign Branch. Choose from a list of proven campaigns, or create a brand new one with a few clicks. Monitor campaigns and outings in real time for instant feedback so you can adjust on the fly.  

Reward Branch
Our automated system delivers fast, reliable, and comprehensive reward activity management that makes the customer experience feel truly rewarding. Booking concierge services and redeeming points for service and activities are effortless. 

Report Branch
KPI status tracking, ROI analysis, operational effectiveness evaluations, consumer behavior investigation, and earnings simulators are just a few features of our Report Branch. If you don’t find what you are looking for in the list of the existing reports, quickly create the report you want.  

Consulting Branch
Our team of experts is regarded as some of the best automotive retail consultants in the world. We have delivered market analyses for professional business publications within China and internationally. Our Consulting Branch connects academic disciplines with market specific frameworks to help solve your unique challenges. 

We will help with:
•	Design, implementation, and monitoring campaign strategies.
•	Creation of new reports tailored to your needs.
•	Design and analysis of customer surveys.
•	Methods to improve sales and service operations.
•	Analysis of customer behaviors.

                
             <div class="bottom" style="border: 1px solid #d2d2d2; background-color: #F5EDD1; height: 20px; margin-bottom: 10px; text-align: center; color: #EBEBEB; text-decoration: none;">
                 <a href="Login.aspx"><%=Resources.CRMTREESResource.LoginHome %></a> &nbsp;&nbsp;
                 <a href="About.aspx"><%=Resources.CRMTREESResource.LoginAbout %></a>&nbsp;&nbsp;
                 <a href="Products.aspx"><%=Resources.CRMTREESResource.LoginProducts %></a>&nbsp;&nbsp;
                 <a href="Services.aspx"><%=Resources.CRMTREESResource.LoginServices %></a>&nbsp;&nbsp;
                 <a href="Contactus.aspx"><%=Resources.CRMTREESResource.LoginContact %></a>
                 <a href="Privacy.aspx"><%=Resources.CRMTREESResource.LoginPrivacy %></a>&nbsp;&nbsp;
                </div>
            </div>
        </div>
    </form>
</body>
</html>
