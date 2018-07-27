<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bottom.ascx.cs" Inherits="control_bottom" %>
<div class="bottom" style="border: 1px solid #d2d2d2; background-color: #dae0bc; height: 20px; margin-bottom: 10px; text-align: center;color:#4c5d00; text-decoration:none;">
                <a href="#"><%=Resources.CRMTREEResource.LoginFoot1 %></a> &nbsp;&nbsp;
                <a href="#"><%=Resources.CRMTREEResource.LoginFoot2 %></a>&nbsp;&nbsp;
                <a href="#"><%=Resources.CRMTREEResource.LoginFoot3 %></a>&nbsp;&nbsp;
                <a href="#"><%=Resources.CRMTREEResource.LoginFoot5 %></a>&nbsp;&nbsp;
                <a href="#"><%=Resources.CRMTREEResource.LoginFoot4 %></a>
            </div>
<div id="newnotice">
    <p>
        <span class="Helptitle">Help information</span>
        <span id="bts">
      <%--      <label class="button" id="tomin" title="Mini">1</label>
            <label class="button" id="tomax" title="max">2 </label>--%>
            <%--<label class="button" id="toclose" title="Close">3</label>--%>
            <img class="toclose" src="/images/ico/close.png" title="Close" style="position:absolute;top:0px;right:0px;height:12px;margin:4px;cursor:pointer;" />
        </span>
    </p>
    <div id="noticecon">
        <iframe id="mainFrame" style="width: 100%; height: 100%; text-align: center; border: 0px;" name="HelpFrame" src="/manage/Help/CRMTreeHelp.aspx"></iframe>
    </div>
</div>
