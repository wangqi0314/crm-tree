<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="customer_Dashboard" %>

<%@ Register Src="~/control/top.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/control/bottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Resources.CRMTREEResource.DashboardPage %></title>

    <link href="/css/Dashboard.css" rel="stylesheet" />
    <link type="text/css" href="/css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" href="/scripts/asyncbox/skins/tree/asyncbox.css" />
    <script type="text/javascript" src="/scripts/asyncbox/AsyncBox.v1.4.js"></script>
    <script type="text/javascript" src="/scripts/common/setTextFont.js"></script>
    <script type="text/javascript" src="/scripts/manage/Customer/JS_Dashboard.js"></script>
    <%--<script src="http://kefu.qycn.com/vclient/state.php?webid=86007" language="javascript" type="text/javascript"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <uc1:top ID="top1" runat="server" />
            <div id="content">
                <div class="nav_infor" style="height: 10px; background-color: #F5EDD1"><%=Resources.CRMTREEResource.DashboardPage %> </div>
                <div class="cont_box" style="height: 760px; padding: 2px;">
                    <div class="report_left" style="width: 300px; padding: 0px; margin-right: 4px; margin-bottom: 3px;">
                        <div class="report_title"><span style="float: right;"></span><%=Resources.CRMTREEResource.DashboardEvent %></div>
                        <div id="My_Appointments">
                            <div class="My_Appointments_1">
                                <div class="My_Appointments_List">
                                    <ul>
                                        <li><%= Resources.CRMTREEResource.DashboardEvent1 %></li>
                                        <li>
                                            <ul>
                                                <li><%=AppointmenEn %></li>
                                            </ul>
                                        </li>
                                    </ul>
                                    <ul style="background-color: #FCF8ED">
                                        <li><%= Resources.CRMTREEResource.DashboardEvent2 %></li>
                                        <li>
                                            <ul>
                                                <li>Current specials</li>
                                            </ul>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li><%= Resources.CRMTREEResource.DashboardEvent3 %></li>
                                        <li>
                                            <ul>
                                                <%=Surveys %>
                                            </ul>
                                        </li>
                                    </ul>
                                    <ul style="background-color: #FCF8ED">
                                        <li><%= Resources.CRMTREEResource.DashboardEvent4 %></li>
                                        <li>
                                            <ul>
                                                <li>- Points expiring within</li>
                                                <li>-Points expiring within</li>
                                            </ul>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li><%= Resources.CRMTREEResource.DashboardEvent5 %></li>
                                        <li>
                                            <ul>
                                                <li>Pending Surveys</li>
                                            </ul>
                                        </li>
                                    </ul>
                                    <ul style="background-color: #FCF8ED">
                                        <li><%= Resources.CRMTREEResource.DashboardEvent6 %></li>
                                        <li>
                                            <ul>
                                                <li>Pending Reviews</li>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="report_left" style="width: 684px; padding: 0px; margin: 0px; margin-bottom: 3px; z-index: -1">
                        <div class="report_title">
                            <span style="float: right;">
                                <input style="margin: 0px; border: 1px solid dashed;" type="button" value="<%= Resources.CRMTREEResource.DashboardMyCar1 %>" />
                                <input style="margin: 0px; border: 1px solid dashed" type="button" value="<%= Resources.CRMTREEResource.DashboardMyCar2 %>" onclick="AddCar()" /></span><%= Resources.CRMTREEResource.DashboardMyCar %>
                        </div>
                        <div id="My_Car">
                            <asp:Repeater ID="myCarInfoList" runat="server">
                                <ItemTemplate>
                                    <div class="Car_info_1">
                                        <div class="Car_Image">
                                            <img style="position: relative; top: 2px; width: 91px; padding: 0px; margin: 0px" src="/images/Car/<%# 
                                            string.IsNullOrEmpty(Eval("CI_Picture_FN").ToString())?"DefaultCar.png":Eval("CI_Picture_FN") %>" />
                                        </div>
                                        <div class="Car_Desc">
                                            <div class="Car_Description_Info">
                                                <ul>
                                                    <li><%=Resources.CRMTREEResource.DashboardMyCar3 %>
                                                        <%# Eval("YR_Year").ToString().Trim() %></li>
                                                    <li><%= Resources.CRMTREEResource.DashboardMyCar4 %>
                                                        <%# Interna?Eval("MK_Make_EN").ToString().Trim():Eval("MK_Make_CN").ToString().Trim() %></li>
                                                    <li><%= Resources.CRMTREEResource.DashboardMyCar5 %>
                                                        <%# Interna?(Eval("CM_Model_EN")==null?"":Eval("CM_Model_EN").ToString().Trim()):(Eval("CM_Model_CN")==null?"":Eval("CM_Model_CN").ToString().Trim()) %>
                                                        <%# Eval("CS_Style_EN")==null||Eval("CS_Style_EN").ToString().Trim()=="---"?"":Eval("CS_Style_EN")%>

                                                    </li>
                                                </ul>
                                            </div>
                                            <%--<div class="hidCarInfo" style="display: none;">
                                                <%# Eval("YR_Year")==null?"":Eval("YR_Year").ToString().Trim()+" "+Eval("CM_Model_EN")==null?"":Eval("CM_Model_EN").ToString().Trim()+" "+ Eval("CS_Style_EN")==null?"":Eval("CS_Style_EN").ToString().Trim() %>
                                            </div>--%>
                                        </div>
                                        <div class="Car_Recommend">
                                            <div class="Car_Description_Info">
                                                <ul>
                                                    <li style="font-size: 12px; color: #252525"><%=Resources.CRMTREEResource.DashboardMyCar10 %>
                                                        <ul style="margin-left: 8px; font-size: 9px; color: #888888">
                                                            <li><%#Interna?Eval("RS_Desc_EN"):Eval("RS_Desc_CN") %></li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="Car_Btn">
                                            <div class="Car_Description_Info">
                                                <div class="Btn_list">
                                                    <input type="button" class="Btn_Car" name="<%# Eval("CI_Code") %>" onclick="Comfirm(this)" value="<%=Resources.CRMTREEResource.DashboardMyCar6 %>" />
                                                </div>
                                                <div class="Btn_list">
                                                    <input type="button" class="Btn_Car" name="<%# Eval("CI_Code") %>" onclick="MakeAppointments(this)" value="<%=Resources.CRMTREEResource.DashboardMyCar7 %>" />
                                                </div>
                                                <div class="Btn_list">
                                                    <input type="button" class="Btn_Car" onclick="Tips()" value="<%=Resources.CRMTREEResource.DashboardMyCar8 %>" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="hidCar_Consultant">
                                            <div>Is recommended consultant for you......</div>
                                        </div>
                                        <div class="Car_Consultant CarRecommendAdviser">
                                            <div class="Car_CI_Code" style="display: none"><%# Eval("CI_Code").ToString().Trim() %></div>
                                            <div class="AD_Code" style="display: none"></div>
                                            <div class="AD_Name" style="display: none"></div>
                                            <div class="Car_AD_1 car_ad">
                                                <div class="AdviserID" style="display: none"></div>
                                                <img class="img1" style="position: relative; top: 2px; width: 45px; height: 45px; padding: 0px; margin: 0px" src="" />
                                                &nbsp
                                                <span class="AD_name"></span>
                                            </div>
                                            <div class="Car_AD_2 car_ad">
                                                <div class="AdviserID" style="display: none"></div>
                                                <img class="img1" style="position: relative; top: 2px; width: 45px; height: 45px; padding: 0px; margin: 0px" src="" />
                                                &nbsp
                                                <span class="AD_name"></span>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <div class="Car_info_1" style="background-color: #FCF8ED">
                                        <div class="Car_Image">
                                            <img style="position: relative; top: 2px; width: 91px; padding: 0px; margin: 0px" src="/images/Car/<%# string.IsNullOrEmpty(Eval("CI_Picture_FN").ToString())?"DefaultCar.png":Eval("CI_Picture_FN") %>" />
                                        </div>
                                        <div class="Car_Desc">
                                            <div class="Car_Description_Info">
                                                <ul>
                                                    <li><%=Resources.CRMTREEResource.DashboardMyCar3 %>
                                                        <%# 
                                                        Eval("YR_Year").ToString().Trim()
                                                        %>

                                                    </li>
                                                    <li><%= Resources.CRMTREEResource.DashboardMyCar4 %>
                                                        <%# Interna?(Eval("MK_Make_EN")==null?"":Eval("MK_Make_EN").ToString().Trim()):(Eval("MK_Make_CN")==null?"":Eval("MK_Make_CN").ToString().Trim()) %>

                                                    </li>
                                                    <li><%= Resources.CRMTREEResource.DashboardMyCar5 %>
                                                        <%# Interna?(Eval("CM_Model_EN")==null?"":Eval("CM_Model_EN").ToString().Trim()):(Eval("CM_Model_CN")==null?"":Eval("CM_Model_CN").ToString().Trim()) %>
                                                        <%#(Eval("CS_Style_EN")==null|| Eval("CS_Style_EN").ToString().Trim()=="---")?"":Eval("CS_Style_EN")%>

                                                    </li>
                                                </ul>
                                            </div>
                                           <%-- <div class="hidCarInfo" style="display: none;">
                                                <%# Eval("YR_Year").ToString().Trim()+" "+Eval("CM_Model_EN")==null?"":Eval("CM_Model_EN").ToString().Trim()+" "+ (Eval("CS_Style_EN")==null||Eval("CS_Style_EN").ToString().Trim()=="---"?"":Eval("CS_Style_EN")) %>
                                            </div>--%>
                                        </div>
                                        <div class="Car_Recommend">
                                            <div class="Car_Description_Info">
                                                <ul>
                                                    <li style="font-size: 12px; color: #252525"><%=Resources.CRMTREEResource.DashboardMyCar10 %>
                                                        <ul style="margin-left: 8px; font-size: 9px; color: #888888">
                                                            <li><%# Interna?Eval("RS_Desc_EN"):Eval("RS_Desc_CN") %></li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="Car_Btn">
                                            <div class="Car_Description_Info">
                                                <div class="Btn_list">
                                                    <input type="button" class="Btn_Car" name="<%# Eval("CI_Code") %>" onclick="Comfirm(this)" value="<%=Resources.CRMTREEResource.DashboardMyCar6 %>" />
                                                </div>
                                                <div class="Btn_list">
                                                    <input type="button" class="Btn_Car" name="<%# Eval("CI_Code") %>" onclick="MakeAppointments(this)" value="<%=Resources.CRMTREEResource.DashboardMyCar7 %>" />
                                                </div>
                                                <div class="Btn_list">
                                                    <input type="button" class="Btn_Car" onclick="Tips()" value="<%=Resources.CRMTREEResource.DashboardMyCar8 %>" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="hidCar_Consultant">
                                            <div>Is recommended consultant for you......</div>
                                        </div>
                                        <div class="Car_Consultant CarRecommendAdviser">
                                            <div class="Car_CI_Code" style="display: none"><%# Eval("CI_Code").ToString().Trim() %></div>
                                            <div class="AD_Code" style="display: none"></div>
                                            <div class="AD_Name" style="display: none"></div>
                                            <div class="Car_AD_1 car_ad">
                                                <div class="AdviserID" style="display: none"></div>
                                                <img class="img1" style="position: relative; top: 2px; width: 45px; height: 45px; padding: 0px; margin: 0px" src="" />
                                                &nbsp<span class="AD_name"></span>
                                            </div>
                                            <div class="Car_AD_2 car_ad">
                                                <div class="AdviserID" style="display: none"></div>
                                                <img class="img1" style="position: relative; top: 2px; width: 45px; height: 45px; padding: 0px; margin: 0px" src="" />
                                                &nbsp<span class="AD_name"></span>
                                            </div>
                                        </div>
                                    </div>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="report_left" style="width: 300px; padding: 0px; margin-right: 4px; margin-bottom: 3px;">
                        <div class="report_title"><span style="float: right;"></span><%= Resources.CRMTREEResource.DashboardMyReward %></div>
                        <div id="My_Rewards">
                            <ul>
                                <li><%= Resources.CRMTREEResource.DashboardMyReward1 %></li>
                                <li>
                                    <ul>
                                        <li>-Current Points: 15,356</li>
                                        <li>-2 more Oil changes left</li>
                                    </ul>
                                </li>
                            </ul>
                            <ul style="background-color: #FCF8ED">
                                <li><%= Resources.CRMTREEResource.DashboardMyReward2 %></li>
                                <li>
                                    <ul>
                                        <li>-Current Points: 115,356</li>
                                        <li>-Points expiring:10,000</li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="report_left" style="width: 684px; padding: 0px; margin: 0px; margin-bottom: 3px;">
                        <div class="report_title"><span style="float: right;"></span><%= Resources.CRMTREEResource.DashboardMyDealers %></div>
                        <div id="My_Dealers">
                            <div class="My_Dealers_1">
                                <div class="Dealers_Info">
                                    <div>
                                        <img style="width: 200px; margin: 0px" src="/images/DealersLogo/DealerslogoM1.gif" />
                                    </div>
                                    <div>
                                        Dealer's latest activities<br />
                                        5000 kilometers of preferential activities
                                    </div>
                                </div>
                                <div class="Dealers_Review">
                                    <div class="Car_Description_Info">
                                        <div class="Btn_list" style="">
                                            <input type="button" class="Btn_Car" style="background-color: greenyellow" value="<%= Resources.CRMTREEResource.DashboardMyDealers1 %>" />
                                        </div>
                                        <div class="Btn_list">
                                            <input type="button" class="Btn_Car" value="<%= Resources.CRMTREEResource.DashboardMyDealers2 %>" />
                                        </div>
                                    </div>
                                </div>
                                <div class="Car_Consultant" style="width: 175px;">
                                    <div>
                                        <img style="position: relative; top: 2px; width: 45px; height: 45px; padding: 0px; margin: 0px" src="/images/Adviser/22.png" />
                                        &nbsp<a href="#"><%=Resources.CRMTREEResource.DashboardMyCar9 %></a>
                                    </div>
                                    <div>
                                        <img style="position: relative; top: 2px; width: 45px; height: 45px; padding: 0px; margin: 0px" src="/images/Adviser/11.png" />
                                        &nbsp<a href="#"><%=Resources.CRMTREEResource.DashboardMyCar9 %></a>
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
        <script type="text/javascript">
            function Tips() {
                $.tips("suceess！");
            }
            function AddCar() {
                location.href = "/manage/car/edit_car.aspx";
            }
            function Comfirm(id) {
                var CarId = $(id).attr("name");
                var AD_Code = $(id).parent().parent().parent().parent().find(".AD_Code").html();
                var AD_Name = $(id).parent().parent().parent().parent().find(".AD_Name").html();

                location.href = "/manage/Appointment/Appointment.aspx?Carid=" + CarId + "&ADcode=" + AD_Code + "&ADname=" + AD_Name + "";
            }
            function MakeAppointments(id) {
                var CarId = $(id).attr("name");
                var CarInfo = $(id).parent().parent().parent().parent().find(".hidCarInfo").html();
                $.open({
                    id: 'open',
                    url: '/manage/customer/ServerHistory.aspx?id=' + CarId + '&CarInfo=' + CarInfo + '',
                    title: 'ServerHistory',
                    width: 640,
                    height: 480,
                    modal: true,
                    btnsbar: $.btn.CANCEL.concat()
                });
                $("#open_cancel").find("span").html("OK");
            }
            $("#nav_Dashboard").addClass("nav_select");
        </script>
    </form>
    <div class="SendEmail_Windows"></div>
    <div class="Contact_EAS">
        <div class="adviser_Info">
            <div class="adviser_Info_Image">
                <img class="img1" style="width: 70px; margin-top: 0px;" src="" />
            </div>
            <div class="adviser_Info_Con">
                <div>Name:<label class="AD_name"></label></div>
                <div>DealerShip:<label class="DSName"></label></div>
                <div>Address of service:<label class="Address" style="word-break: break-all; word-wrap: break-word;"></label></div>
                <img class="Contact_EAS_Close" src="/images/ico/close.png" title="Close" style="position: absolute; top: 0px; right: 0px; height: 12px; margin: 4px; cursor: pointer;" />
            </div>
        </div>
        <div class="adviser_contact">
            <div class="adviser_Mobile">
                <div class="DeleImage" style="float: left">
                    <img class="image" src="/images/Delephone.png" />
                </div>
                <div style="float: left; text-align: left; line-height: 1.8em;">
                    <div>Mobile:<lable class="Mob"></lable></div>
                    <div>Office:<lable class="Offi"></lable></div>
                    <div>DealerShip:<lable class="DS"></lable></div>
                </div>
            </div>
            <div class="adviser_Email" style="height: 60px; border-bottom: 1px solid #d2d2d2;">
                <div class="EmailImage" style="float: left;">
                    <img style="width: 40px; padding: 0px; margin: 2px;" src="/images/mail.png" />
                </div>
                <div style="float: left; width: 150px; text-align: left; line-height: 1.8em;">
                    <div>Send email:</div>
                    <div class="email" style="text-decoration: underline; cursor: pointer;">ss</div>
                </div>
            </div>
            <div class="adviser_Timely" style="height: 60px; border-bottom: 1px solid #d2d2d2;">
                <div class="TimelyImage" style="float: left;">
                    <img style="width: 40px; padding: 0px; margin: 2px;" src="/images/Mesage.png" />
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
</body>
</html>
