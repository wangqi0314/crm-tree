<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit_User.aspx.cs" Inherits="manage_AddUser_edit_User" %>
<%@ Register Src="~/control/top.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/control/bottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <link rel="stylesheet" type="text/css" href="/scripts/swfupload_en/swfupload.css" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>

    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/scripts/swfupload_en/swfupload.js"></script>
    <script type="text/javascript" src="/scripts/swfupload_en/swfupload.queue.js"></script>
    <script type="text/javascript" src="/scripts/swfupload_en/fileprogress.js"></script>
    <script type="text/javascript" src="/scripts/swfupload_en/filegroupprogress.js"></script>
    <script type="text/javascript" src="/scripts/swfupload_en/swfupload.handlers_pic.js"></script>
    <script type="text/javascript" src="/scripts/manage/User/edit_User.js"></script>
        <script type="text/javascript" src="/scripts/common/setCookie.js"></script>
</head>
<body>
    
    <form id="form1" runat="server">
        <div id="container">
            <uc1:top ID="top1" runat="server" />
            <div id="content">
                <div class="nav_infor"><a class="brown" href="#"><%=Resources.CRMTREEResource.MyCarnavigation %></a>&nbsp;&gt;&nbsp;<a class="brown" href="/manage/Car/list_Car.aspx"><%=Resources.CRMTREEResource.MyCarList %></a>&nbsp;&gt;&nbsp;<%=Resources.CRMTREEResource.MyCarEdit %></div>
                <div class="cont_box">
                    <div class="cont_list" id="Information1">
                        <table width="80%" cellpadding="0" cellspacing="0" style="margin: auto;">
                            <tr>
                                <th colspan="4" class="detail" style="height: 44px">User Add</th>
                            </tr>
                            <tr>
                                <td class="table_titles" width="22%"><span class="red">*</span>UserName</td>
                                <td class="table_cont" colspan="3"><input type="text" id="txtUserName" class="input_text_w200 js_enter" /></td>
                            </tr>
                            <tr>
                                <td class="table_titles" width="22%"><span class="red">*</span>UserPassword</td>
                                <td class="table_cont">
                                    <input id="txtUserPassword" type="text" class="input_text_w200 js_enter" /></td>
                                <td class="table_titles" width="22%">Gender </td>
                                <td class="table_cont">
                                    <select id="selectGend" class="input_text_w200 js_enter">
                                        <option value="0">Male</option>
                                        <option value="1">Female</option>
                                      </select>      
                                </td>
                            </tr>
                            <tr>
                                <td class="table_titles" width="22%" >IndusTry</td>
                                <td class="table_cont">
                                    <select id="selectIndusTry"  class="input_text_w200 js_enter">
                                        <option value="0">Farmer</option>
                                        <option value="1">Government</option>
                                        <option value="2">Telecome</option>
                                        <option value="3">Computer</option>
                                     </select></td>
                                <td class="table_titles" width="22%"><span class="red">*</span>Occupation</td>
                                <td class="table_cont">
                                     <select id="selectOccupation" class="input_text_w200 js_enter">
                                        <option value="0">Self Employed</option>
                                        <option value="1">Engineer</option>
                                      </select>
                                   </td>
                            </tr>
                            <tr>
                                <td class="table_titles" width="22%" style="height: 0px">Education</td>
                                <td class="table_cont" style="height: 8px">
                                    <select id="selectEducation" class="input_text_w200 js_enter">
                                            <option value="0">None</option>
                                            <option value="1">Primary School</option>
                                            <option value="2">High School</option>
                                            <option value="3">AS , 2 Year college</option>
                                            <option value="4">BS, 4  Year College</option>
                                            <option value="5">MS, Masters program</option>
                                            <option value="6">Phd </option>
                                     </select></td>
                                <td class="table_titles" width="22%" style="height: 8px">BirthdayData</td>
                                <td class="table_cont" style="height: 8px; width: 79%;">
                                   <input type="text" id="txtBirthdayData"/ class="input_text_w200 js_enter"></td>
                            </tr>
                            <tr>
                                <td class="table_titles" width="22%" style="height: 33px">IDCode</td>
                                <td class="table_cont" style="height: 33px">
                                    <input id="txtIDCode" type="text" class="input_text_w200 js_enter" /></td>
                                <td class="table_titles" width="22%" style="height: 33px">IDType</td>
                                <td class="table_cont" style="height: 33px">
                                    <select id="selecttype" name="selecttype"  class="input_text_w200 js_enter">
                                        <option value="0" selected="selected"></option>
                                        <option  value="1">Dealer</option>
                                        <option value="2">Dealer Group</option>
                                        <option value="3">OEM</option>
                                        <option value="4">CRMTREE</option>
                                        <option value="5">Customer</option>
                                    </select></td>
                            </tr>
                             <tr>
                                <td class="table_titles" width="22%" style="height: 33px"></td>
                                <td class="table_cont" style="height: 33px">
                                    <select id="selectDealer" name="selectDealer" style="display:none" class="input_text_w200 js_enter">
                                 </select>
                                </td>
                                <td class="table_titles" width="22%" style="height: 33px" class="input_text_w200 js_enter"></td>
                                <td class="table_cont" style="height: 33px">
                                    <select id="selectGroup" name="selectGroup" class="input_text_w200 js_enter">
                                    </select></td>
                            </tr>
                            <tr>
                                <td class="table_titles" width="22%" style="height: 33px" colspan="2">  <input type="button" value="<%=Resources.CRMTREEResource.MyCarSAVE %>" class="menu_submit" id="btnSave" onclick="btnSubmit()" /></td>
                                 <td class="table_titles" width="22%" style="height: 33px"  colspan="2"><input type="button" id="btnBack" value="<%=Resources.CRMTREEResource.MyCarBacktoList %>" class="menu_submit" onclick="    javascript: history.go(-1); return false;" /></td>
                            </tr>
                        </table>
                    </div>
                    
                </div>
            </div>
            <uc2:bottom ID="bottom1" runat="server" />
        </div>
        <script type="text/javascript">
            $("#nav_My Cars").addClass("nav_select");
        </script>
    </form>
</body>
</html>