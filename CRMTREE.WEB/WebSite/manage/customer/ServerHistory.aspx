<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServerHistory.aspx.cs" Inherits="manage_customer_ServerHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ServerHistory</title>
    <link type="text/css" href="/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" />
    <link type="text/css" href="/css/ServerHistory.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/scripts/manage/Customer/ServerHistory.js"></script>
</head>
<body>
    <div>

        <div class="SelectField">
            <b style="float: left; margin-top: 5px;">Service History:</b>&nbsp;<div class="CarInfo" style="float: left; margin-left: 5px; margin-top: 5px; font-size: 15px; font-family: Arial">2009 BMW SpiderVeloce</div>
        </div>
        <div class="ServiceInfo_list">
            <div class="ServiceTite">
                <b style="margin-right: 18px;">Service Date:</b>
                <input type="text" id="BeginDateTime" onfocus="WdatePicker({lang:'en',dateFmt:'MM/dd/yyyy',maxDate:'%y-%M-{%d-1}'})" class="Wdate" />
                <input type="text" id="EndDateTime" onfocus="WdatePicker({lang:'en',dateFmt:'MM/dd/yyyy',minDate:'#F{$dp.$D(\'BeginDateTime\')}',maxDate:'%y-%M-%d'})" class="Wdate" />
                <input type="button" class="Btn_ServerHistory" value="Show Service History" style="height: 25px; width: 150px;" />
            </div>
            <div class="Service">
                <div class="ServiceInfo_single ServiceInfo">
                    <div><b>Date:</b><span>06/05/0214</span> &nbsp; &nbsp;<b>Dealer:</b><span>06/05/0214</span> <span style="background-color:AppWorkspace; margin-left:20px;border-radius: 2px 2px 2px 2px;padding:2px;">Details</span></div>
                </div>
                <div class="ServiceInfoCon">
                    <div><b>Service Adviser:</b><span>06/05/0214</span> &nbsp; &nbsp;<b>Repair Orders:</b><span>06/05/0214</span></div>
                    <div><b>Total Amount:</b><span>06/05/0214</span> &nbsp; <b>paid:</b><span>06/05/0214</span>&nbsp; <b>Points Used:</b><span>06/05/0214</span></div>
                    <div style="border-bottom: 1px solid #d2d2d2; margin: 5px 20px;">fff</div>
                </div>
                <div class="ServiceInfo_double ServiceInfo">
                    <div><b>Date:</b><span>06/05/0214</span> &nbsp; &nbsp;<b>Dealer:</b><span>06/05/0214</span></div>
                </div>
                <div class="ServiceInfoCon">
                    <div><b>Service Adviser:</b><span>06/05/0214</span> &nbsp; &nbsp;<b>Repair Orders:</b><span>06/05/0214</span></div>
                    <div><b>Total Amount:</b><span>06/05/0214</span> &nbsp; <b>paid:</b><span>06/05/0214</span>&nbsp; <b>Points Used:</b><span>06/05/0214</span></div>
                    <div style="border-bottom: 1px solid #d2d2d2; margin: 5px 20px;">fff</div>
                </div>
                <div class="ServiceInfo_single ServiceInfo"></div>
                <div class="ServiceInfo_double ServiceInfo"></div>
                <div class="ServiceInfo_single ServiceInfo"></div>
                <div class="ServiceInfo_double ServiceInfo"></div>
            </div>
        </div>
        <div></div>
    </div>
</body>
</html>
