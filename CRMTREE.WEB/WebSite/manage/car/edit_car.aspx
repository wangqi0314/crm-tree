<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit_car.aspx.cs" Inherits="manage_car_edit_car" %>

<%@ Register Src="~/control/top.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/control/bottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
    <script type="text/javascript" src="/scripts/manage/Car/edit_Car.js"></script>
    <script type="text/javascript" src="/scripts/common/setCookie.js"></script>
    <title><%=Resources.CRMTREEResource.MyCarEdit %></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <uc1:top ID="top1" runat="server" />
            <div id="content">
                <div class="nav_infor">
                    <a class="brown" href="#"><%=Resources.CRMTREEResource.MyCarnavigation %></a>&nbsp;&gt;&nbsp;
                    <a class="brown" href="/manage/Car/list_Car.aspx"><%=Resources.CRMTREEResource.MyCarList %></a>&nbsp;&gt;&nbsp;
                    <%=Resources.CRMTREEResource.MyCarEdit %>
                </div>
                <div class="cont_box">
                    <div class="cont_list" id="Information1">
                        <div class="report_title"><%=Resources.CRMTREEResource.MyCarBasicInformation %></div>
                        <table width="100%" cellpadding="0" cellspacing="0" style="margin: auto;">
                            <tr>
                                <td class="table_titles" width="22%"><span class="red">*</span><%=Resources.CRMTREEResource.MyCarMAKE %></td>
                                <td class="table_cont" style="width: 458px">
                                    <select class="input_text_w200 js_enter" id="txtMake" name="txtMake"></select>
                                </td>
                            </tr>
                            <tr>
                                <td class="table_titles" width="22%"><span class="red">*</span><%=Resources.CRMTREEResource.MyCarMODEL %></td>
                                <td class="table_cont" style="width: 458px">
                                    <select class="input_text_w200 js_enter" id="txtMode" name="txtMode">
                                    </select></td>
                                <td class="table_titles" width="22%" style="height: 0px"><%=Resources.CRMTREEResource.MyCarVIN %></td>
                                <td class="table_cont" style="height: 8px">
                                    <input class="input_text_w200 js_enter" id="txt_VIN" name="txt_VIN" maxlength="50" value="" /></td>

                            </tr>
                            <tr>
                                <td class="table_titles" width="22%"><span class="red">*</span><%=Resources.CRMTREEResource.MyCarSTYLE %></td>
                                <td class="table_cont" style="width: 458px">
                                    <select class="input_text_w200 js_enter" id="txtStyle" name="txtStyle">
                                    </select></td>
                                <td class="table_titles" width="22%" style="height: 8px"><%=Resources.CRMTREEResource.MyCarMILEAGE %></td>
                                <td class="table_cont" style="height: 8px; width: 79%;">
                                    <input class="input_text_w200 js_enter" id="txt_mileage" name="txt_mileage" maxlength="50" value="" /></td>
                            </tr>
                            <tr>
                                <td class="table_titles" width="22%"><span class="red">*</span><%=Resources.CRMTREEResource.MyCarYEARS %></td>
                                <td class="table_cont" style="width: 458px">
                                    <select class="input_text_w200 js_enter" id="txtYears" name="txtYears">
                                    </select></td>
                                <td class="table_titles" width="22%" style="height: 0px"><%=Resources.CRMTREEResource.MyCarLic %></td>
                                <td class="table_cont" style="height: 8px">
                                    <input class="input_text_w200 js_enter" id="txt_Lic" name="txt_Lic" maxlength="50" value="" /></td>
                            </tr>
                            <tr>
                                <td class="table_titles" width="22%" style="height: 33px"><%=Resources.CRMTREEResource.MyCarCOLOREXTERNAL %></td>
                                <td class="table_cont" style="height: 33px">
                                    <select class="input_text_w200 js_enter" id="txtCOLOR_E" name="txtCOLOR_E">
                                    </select></td>
                                <td class="table_titles" width="22%" style="height: 33px"><%=Resources.CRMTREEResource.MyCarCOLORINTERNAL %></td>
                                <td class="table_cont" style="height: 33px">
                                    <select class="input_text_w200 js_enter" id="txtCOLOR_I" name="txtCOLOR_I">
                                    </select></td>
                            </tr>
                            <tr>
                                <td class="table_titles"><%=Resources.CRMTREEResource.MyCarPICTURE %></td>
                                <td class="table_cont" colspan="3" style="width: 458px">
                                    <span id="spanButtonPlaceHolder"></span>
                                    <br />
                                    <div id="divprogresscontainer"></div>
                                    <input type="hidden" id="hdFileNmae" name="hdFileNmae" value="<%=strFilename %>" />
                                    <div id="divprogressGroup"></div>
                                    <br />
                                    <span id="sp_File" class="red" style="display: none;"><%=Resources.CRMTREEResource.MyCarCon2 %></span>
                                    <br />
                                    <img id="imgCar" src="/images/icon/nopic.jpg" style="height: 200px; width: 267px;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="detail_menu">
                        <input type="button" value="<%=Resources.CRMTREEResource.MyCarSAVE %>" class="menu_submit_med" id="btnSave" onclick="btnSubmit()" />&nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="btnBack" value="<%=Resources.CRMTREEResource.CancelBtn %>" class="menu_submit_med" onclick="    javascript: history.go(-1); return false;" />
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
