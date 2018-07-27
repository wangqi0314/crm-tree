<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Appointment.aspx.cs" Inherits="customer_Appointment" %>

<%@ Register Src="~/control/top.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/control/bottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= Resources.CRMTREEResource.Appointment %></title>
    <link href="/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" />
    <script src="/My97DatePicker/WdatePicker.js"></script>
    <link href="/css/Dashboard.css" rel="stylesheet" />
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>
    <script type="text/javascript" src="/scripts/manage/Appointment/AjaxData.js"></script>
</head>
<body>
    <form id="form1">
        <div id="container">
            <uc1:top ID="top1" runat="server" />
            <div id="content">
                <div class="nav_infor" style="background-color: #F5EDD1"><%= Resources.CRMTREEResource.Appointment %></div>
                <div class="cont_box" style="height: 500px; padding: 2px;">
                    <div class="report_left" style="width: 990px; height: 300px; padding: 0px; margin: 0px; margin-bottom: 3px;">
                        <div class="report_title">
                            <span style="float: right;"></span><%= Resources.CRMTREEResource.Appointment1 %>
                        </div>
                        <div id="My_Car" style="overflow: inherit">
                            <div class="Appointment_Contene">
                                <div class="Appointment_info_1" style="display: block">
                                    <div class="Appointment_Table">
                                        <div><span><%= Resources.CRMTREEResource.Appointment2 %></span></div>
                                    </div>
                                    <div class="Appointment_Table_2">
                                        <div>
                                            <input type="text" id="MyCarInfoId" />
                                            <input type="hidden" id="hidMyCarInfoId" />
                                        </div>
                                        <div id="NUI_msgbox" class="nui-msgbox">
                                            <asp:Repeater ID="myCarInfoList" runat="server">
                                                <ItemTemplate>
                                                    <div class="CarInfo">
                                                        <div>
                                                            <img src="/images/Car/<%# string.IsNullOrEmpty(Eval("CI_Picture_FN").ToString())?"DefaultCar.png":Eval("CI_Picture_FN") %>" />
                                                        </div>
                                                        <div class="Car_Model"><%# Interna?(Eval("CM_Model_EN")==null?"":Eval("CM_Model_EN").ToString().Trim()+" "):(Eval("CM_Model_CN")==null?"":Eval("CM_Model_CN").ToString().Trim()) %><%# Eval("CS_Style_EN")==null||Eval("CS_Style_EN").ToString().Trim()=="---"?"":Eval("CS_Style_EN").ToString().Trim()%></div>
                                                        <input type="hidden" class="hidCarCodeID" value="<%# Eval("CI_Code") %>" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="Appointment_info_1" style="background-color: #FCF8ED;">
                                    <div class="Appointment_Table">
                                        <div><span><%= Resources.CRMTREEResource.Appointment3 %></span></div>
                                    </div>
                                    <div class="Appointment_Table_2">
                                        <div>
                                            <input type="text" id="MyDealerId" />
                                            <input type="hidden" id="hidMyDealerId" />
                                            <input type="hidden" id="hidSA_Selection" />
                                            <input type="hidden" id="hidServ_Package" />
                                        </div>
                                        <div id="NUI_msgbox_MyDealerId" class="nui-msgbox">
                                        </div>
                                    </div>
                                </div>
                                <div class="Appointment_info_1">
                                    <div class="Appointment_Table">
                                        <div><span><%= Resources.CRMTREEResource.Appointment5 %></span></div>
                                    </div>
                                    <div class="Appointment_Table_2">
                                        <div>
                                            <input type="text" id="MyAdviserId" />
                                            <input type="hidden" id="hidMyAdviserId" />
                                        </div>
                                        <div id="NUI_msgbox_MyAdviserId" class="nui-msgbox" style="height: 125px; width: 235px">
                                        </div>
                                    </div>
                                </div>
                                <div class="Appointment_info_1" style="background-color: #FCF8ED;">
                                    <div class="Appointment_Table">
                                        <div><span><%= Resources.CRMTREEResource.Appointment6 %></span></div>
                                    </div>
                                    <div class="Appointment_Table_2">
                                        <div>
                                            <input type="text" id="MyTypeId" />
                                            <input type="hidden" id="hidMyTypeId" />
                                        </div>
                                        <div id="NUI_msgbox_MyTypeId" class="nui-MyType">
                                        </div>
                                    </div>
                                    <div class="Appointment_Table_4">
                                        <div>
                                            <input type="text" id="MyTypeInfoId" />
                                            <input type="hidden" id="hidMyTypeInfoId" />
                                        </div>
                                        <div id="NUI_msgbox_MyTypeInfoId" class="nui-MyType">
                                        </div>
                                    </div>
                                    <div class="Appointment_Table_5">
                                        <div>
                                            <input type="text" id="MyTypeOther" />
                                        </div>
                                    </div>
                                </div>
                                <div class="Appointment_info_1">
                                    <div class="Appointment_Table">
                                        <div><span><%= Resources.CRMTREEResource.Appointment7 %></span></div>
                                    </div>
                                    <div class="Appointment_Table_2">
                                        <div>
                                            <input type="text" id="MyTransportationid" />
                                            <input type="hidden" id="hidMyTransportationid" />
                                        </div>
                                        <div id="NUI_msgbox_MyTransportationid" class="nui-MyType">
                                            <div class="MyTransportationInfo" style="margin-left: 1px; border-bottom: 1px solid #d2d2d2; cursor: pointer; text-align: left;">
                                                <div class="selectTransportation">No Need</div>
                                            </div>
                                            <div class="MyTransportationInfo" style="margin-left: 1px; border-bottom: 1px solid #d2d2d2; cursor: pointer; text-align: left;">
                                                <div class="selectTransportation">Will wait in the waiting  room</div>
                                            </div>
                                            <div class="MyTransportationInfo" style="margin-left: 1px; border-bottom: 1px solid #d2d2d2; cursor: pointer; text-align: left;">
                                                <div class="selectTransportation">Shuttle Service</div>
                                            </div>
                                            <div class="MyTransportationInfo" style="margin-left: 1px; border-bottom: 1px solid #d2d2d2; cursor: pointer; text-align: left;">
                                                <div class="selectTransportation">Loaner car</div>
                                            </div>
                                            <div class="MyTransportationInfo" style="margin-left: 1px; border-bottom: 1px solid #d2d2d2; cursor: pointer; text-align: left;">
                                                <div class="selectTransportation">Renting a car</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="Appointment_info_1" style="background-color: #FCF8ED;">
                                    <div class="Appointment_Table">
                                        <div><span><%= Resources.CRMTREEResource.Appointment4 %></span></div>
                                    </div>
                                    <div class="Appointment_Table_2">
                                        <input runat="server" type="text" id="myDateTime" onfocus="WdatePicker({lang:'en',dateFmt:'yyyy-MM-dd',minDate:'%y-%M-{%d+1}'})" class="Wdate" style="margin-left: -50px; width: 120px; height: 20px;" />
                                    </div>
                                    <div class="Appointment_Table_3">
                                        <input runat="server" type="text" id="myTime" onfocus="WdatePicker({lang:'en',dateFmt:'HH:mm',minDate:'%H:%m}',maxDate:'{%H+10}:%m}'})" class="Wdate" style="margin-left: -100px; width: 120px; height: 20px;" />
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <input id="BtnApp" class="Btn_Car" type="button" value="<%= Resources.CRMTREEResource.Appointment1 %>" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="report_left" style="width: 990px; padding: 0px; margin: 0px; margin-bottom: 3px;">
                        <div class="report_title"><span style="float: right;"></span><%= Resources.CRMTREEResource.DashboardDealerRecommend %></div>
                        <div id="Dealers_Recommend">
                            <div class="Dealers_Recommend_1">
                                <div class="Dealers_Recommend_Info_1">
                                    <div>
                                        <img style="width: 150px; margin: 0px" src="/images/DealersLogo/DealerslogoM2.gif" />
                                    </div>
                                    <div>
                                        Dealer's latest activities<br />
                                        5000 kilometers of preferential activities
                                    </div>
                                </div>
                                <div class="Dealers_Recommend_Info_1">
                                    <div>
                                        <img style="width: 150px; margin: 0px" src="/images/DealersLogo/DealerslogoM3.gif" />
                                    </div>
                                    <div>
                                        Dealer's latest activities<br />
                                        5000 kilometers of preferential activities
                                    </div>
                                </div>
                                <div class="Dealers_Recommend_Info_1">
                                    <div>
                                        <img style="width: 200px; margin: 0px" src="/images/DealersLogo/DealerslogoM1.gif" />
                                    </div>
                                    <div>
                                        Dealer's latest activities<br />
                                        5000 kilometers of preferential activities
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <uc2:bottom ID="bottom1" runat="server" />
        </div>
    </form>
    <div id="NUI_mask" class="nui-mask"></div>
    <script type="text/javascript">
        $("#nav_Appointments").addClass("nav_select");
    </script>
</body>
</html>

