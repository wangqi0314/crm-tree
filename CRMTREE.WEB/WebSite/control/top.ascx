<%@ Control Language="C#" AutoEventWireup="true" CodeFile="top.ascx.cs" Inherits="control_top" %>
<script type="text/javascript" src="/scripts/manage/Top.js"></script> 
<script type="text/javascript">
 
</script>
<div id="top">
    <div class="left" style="background: url(/images/logo_daeku.png) no-repeat; width: 200px; height: 80px; margin-top: 10px;">
        <a href="#">
            <div class="logo"></div>
        </a>
    </div>
    <div class="right">
        <div class="infor">
            <label>
                <span class="font_orange"><%=realName%></span> <%=Resources.CRMTREEResource.topWelcome %>
            </label>
            <label id="Help">
                <a  href="#" >
                    <img src="/images/helpS.png" style="margin-bottom=-4px;"/><%= Resources.CRMTREEResource.LoginHelp %></a></label>
            <label>
                <a href="#" >
                    <img src="/images/ToolS.png" style="margin-bottom=-4px;"/><%= Resources.CRMTREEResource.LoginTool %></a></label>
           
            <label>
                <a href="/LoginOut.aspx?Source=2"><%= Resources.CRMTREEResource.topSignout %></a>
            </label>
            &nbsp;
            <select id="internationalization">
                <option value="1">Chinese-汉语</option>
                <option value="2">US-English</option>
            </select>
            <div style="float: right; margin-top: 2px; margin: 0px;height:20px;">
                <img id="ImageFlag" src="/images/EnglishFlag.jpg" />
            </div>
        </div>
        <div class="Languages" style="display: none"><%=Languages %></div>
        <div class="search">
            <input type="text" class="input_search" />
            <input type="button" class="menu_search" value="<%= Resources.CRMTREEResource.topSearch %>" />
        </div>
        <div class="nav">
            <ul>
                <li class="nav_left"></li>
                <%=strNav.ToString()%>
                <li class="nav_right"></li>
            </ul>
        </div>
        <div class="PowBy">
            <span style="font-weight: bold;  font-family: serif;padding-right: 9px;margin-top: 0px;">
                Powered By &nbsp&nbsp&nbsp<img id="ImageFlag" src="/images/CTLogoS.png" /></span>
        </div>

    </div>
</div>
