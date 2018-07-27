<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_Event.aspx.cs" Inherits="manage_Event_Edit_Event" %>

<%@ Register Src="~/control/CrmTreebottom.ascx" TagPrefix="uc1" TagName="CrmTreebottom" %>
<%@ Register Src="~/control/CrmTreetop.ascx" TagPrefix="uc1" TagName="CrmTreetop" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script type="text/javascript" src="/scripts/swfupload_en/swfupload.handlers.js"></script>
    <script type="text/javascript" src="/scripts/manage/Event/Edit_Event.js"></script>

    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>
    <title>Event</title>
    <style type="text/css">
        .boder_height {
            height: 25px;
            border: 1px solid #ddd;
        }

        .boders {
            border: 1px solid #ddd;
        }

        .Meth_s {
            float: left;
            margin-right: 10px;
            border-radius: 4px;
            border: 1px solid #d2d2d2;
            width: 230px;
        }

        .succ_Matri_L {
            border-bottom: 1px dashed #b1c242;
            padding-bottom: 2px;
            padding-top: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <uc1:CrmTreetop runat="server" ID="CrmTreetop" />
            <div id="content">
                <div class="nav_infor">
                    <a class="brown" href="#">List of Event</a>> Event
                </div>
                <div class="cont_box">
                    <div class="cont_list" id="Information1">
                        <div class="report_title">Event Editor</div>
                        <input type="hidden" id="input_hid_CG_UType" value="" />
                        <input type="hidden" id="input_hid_AU_Type" value="" />
                        <input type="hidden" id="input_hid_UG_Code" value="" />
                        <table cellpadding="0" cellspacing="0" style="width: 100%; margin: auto;">
                            <%-- 事件标题--%>
                            <tr>
                                <td class="table_title" colspan="2" style="width: 22%">
                                    <span class="red">*</span><%= Resources.CRMTREESResource.CampaignTitle %>
                                </td>
                                <td class="table_cont" style="width: 78%">
                                    <input class="input_text_w400 js_enter" id="txt_title" name="txt_title" maxlength="50" value="<%=Event.EV_Title %>" />
                                    <span id="sp_title" class="red" style="display: none;"><%= Resources.CRMTREESResource.CampaginCon8 %></span>
                                    <input id="input_checkbox_Share" type="checkbox" /><lable class="share"><%=Resources.CRMTREESResource.CampaignShare %></lable>
                                    <input id="input_checkbox_active" type="checkbox" /><lable class="active"><%=Resources.CRMTREESResource.CampaignActive %></lable>
                                    <input type="hidden" id="input_hid_Share" value="<%=Event.EV_Share %>" />
                                    <input type="hidden" id="input_hid_active" value="<%=Event.EV_Active_Tag %>" />
                                </td>
                            </tr>
                            <%-- 事件描述--%>
                            <tr>
                                <td class="table_title" colspan="2" style="vertical-align: top; width: 22%">
                                    <span class="red">*</span><%= Resources.CRMTREESResource.CampaignDescription %>
                                </td>
                                <td class="table_cont" style="width: 78%">
                                    <textarea class="input_textarea_wAuto98 js_enter boders" id="txt_desc"
                                        name="txt_desc" cols="48" rows="5"><%=Event.EV_Desc %></textarea>
                                    <span id="sp_desc" class="red" style="display: none;"><%= Resources.CRMTREESResource.CampaginCon9 %> </span>
                                </td>
                            </tr>
                            <%-- 事件类型--%>
                            <tr>
                                <td class="table_title" colspan="2"><span class="red">*</span><%= Resources.CRMTREESResource.CampaignType %></td>
                                <td class="table_cont">
                                    <input type="hidden" id="hid_type" value="<%=Event.EV_Type %>" />
                                    <div style="float: left; margin-right: 10px; border-radius: 4px; border: 1px solid #d2d2d2; width: 230px;">
                                        <label>
                                            <input type="radio" name="Sales_type" checked="checked"
                                                value="1" id="Sales_type_1" /><%=Resources.CRMTREESResource.EventSales %>
                                        </label>
                                        <label>
                                            <input type="radio" name="Sales_type" value="2" id="Sales_type_2" /><%=Resources.CRMTREESResource.EventService %>
                                        </label>
                                        <label>
                                            <input type="radio" name="Sales_type" value="3" id="Sales_type_3" /><%=Resources.CRMTREESResource.EventMarketing %>
                                        </label>
                                    </div>
                                </td>
                            </tr>
                            <%-- 事件目标--%>
                            <tr>
                                <td class="table_title" colspan="2"><span class="red">*</span><%= Resources.CRMTREESResource.CampaginCT %></td>
                                <td class="table_cont">
                                    <input type="hidden" id="hid_Targeted" />
                                    <select id="ddl_Targeted" class="js_enter boder_height" style="width: 500px;">
                                        <%=strDLL_Option %>
                                    </select>&nbsp;&nbsp;
                                    <input type="button" value="<%= Resources.CRMTREESResource.CampaginUpdate %>" class="menu_submit_small"
                                        id="btnTargeted" style="height: 20px; width: 60px;" />
                                    <br />
                                    <span id="sp_Targeted" class="red" style="display: none;"><%=Resources.CRMTREESResource.EventTarSp %> </span>
                                    <input type="hidden" id="hid_targeted_val" value="" />
                                    <input type="hidden" id="hid_targeted_val_old" value="" />
                                </td>
                            </tr>
                            <%-- 事件持续时间 --%>
                            <tr>
                                <td class="table_title" style="width: 10%; vertical-align: middle;" rowspan="2"><%=Resources.CRMTREESResource.EventContact %></td>
                                <td class="table_title">
                                    <span class="red">*</span><%=Resources.CRMTREESResource.EventDuration %>
                                </td>
                                <td class="table_cont"><%= Resources.CRMTREESResource.Campaigntime %>
                                    <span>
                                        <input id="txt_Start_dt" class="input_text Wdate js_enter" type="text"
                                            onfocus="WdatePicker({lang: 'en', dateFmt: 'MM/dd/yyyy', minDate: '%y-%M-%d'})"
                                            value="<%=Event.EV_Start_dt.ToString("MM/dd/yyyy") %>" />
                                        -
                                        <input id="txt_End_dt" class="input_text Wdate js_enter" type="text"
                                            onfocus="WdatePicker({lang: 'en', dateFmt: 'MM/dd/yyyy', minDate:'#F{$dp.$D(\'txt_Start_dt\')}'})"
                                            value="<%=Event.EV_End_dt.ToString("MM/dd/yyyy") %>" />
                                        <span id="sp_type_time" class="red" style="display: none;">Please enter Date</span>
                                    </span>
                                </td>
                            </tr>
                            <%-- 事件方法--%>
                            <tr>
                                <td class="table_title"><span class="red">*</span><%= Resources.CRMTREESResource.CampaignMethod %>></td>
                                <td class="table_cont">
                                    <div class="Meth_s">
                                        <input type="hidden" id="hid_method" value="<%=Event.EV_Method %>" />
                                        <p class="ps"><%= Resources.CRMTREESResource.CampaignPhoneCalls %></p>
                                        <label>
                                            <input type="checkbox" name="ckb_method" value="1" id="ckb_method_1" class="js_enter" />
                                            <%= Resources.CRMTREESResource.CampaignInperson %>
                                        </label>
                                        <label>
                                            <input type="checkbox" name="ckb_method" value="2" id="ckb_method_2" class="js_enter" />
                                            <%=Resources.CRMTREESResource.CampaignAutomated %>
                                        </label>
                                        <br />
                                        <span id="sp_person" style="display: none;">
                                            <input type="hidden" id="hid_Whom" value="<%=Event.EV_Whom %>" />
                                            <label>
                                                <input type="radio" name="radbtn_pmc" checked="checked" value="1" id="radbtn_pmc_1" class="js_enter" />
                                                <%=Resources.CRMTREESResource.CampaignDealer %>
                                            </label>
                                            <label>
                                                <input type="radio" name="radbtn_pmc" value="2" id="radbtn_pmc_2" class="js_enter" />
                                                <%= Resources.CRMTREESResource.CampaignCRMTree %>
                                            </label>
                                            <label>
                                                <input type="radio" name="radbtn_pmc" value="3" id="radbtn_pmc_3" class="js_enter" />
                                                <%= Resources.CRMTREESResource.CampaignBothof %>
                                            </label>
                                            <span id="sp_rad_person" class="red" style="display: none;">
                                                <%= Resources.CRMTREESResource.CampaignCon1 %>
                                            </span>
                                        </span>
                                    </div>
                                    <div class="Meth_s" style="width: 300px;">
                                        <p class="ps"><%= Resources.CRMTREESResource.CampaignText %></p>
                                        <label>
                                            <input type="hidden" id="hid_Mess_Type" value="<%=Event.EV_Mess_Type %>" />
                                            <input type="radio" name="radbtn_Mess" value="1" checked="checked" id="radbtn_Mess_1" class="js_enter" />
                                            <%=Resources.CRMTREESResource.CampaignTextSun %>
                                            <input type="radio" name="radbtn_Mess" value="10" id="radbtn_Mess_10" class="js_enter" />
                                            <%=Resources.CRMTREESResource.CampaignTextScript %>
                                            <input type="radio" name="radbtn_Mess" value="3" id="radbtn_Mess_3" class="js_enter" />
                                            <%=Resources.CRMTREESResource.CampaignTextVoice %>
                                            <input type="radio" name="radbtn_Mess" value="2" id="radbtn_Mess_2" class="js_enter" />
                                            <%=Resources.CRMTREESResource.CampaignTextHtml %>
                                        </label>
                                        <br />
                                        <label>
                                            <input type="checkbox" name="ckb_method" value="3" id="ckb_method_3" class="js_enter" />
                                            <%= Resources.CRMTREESResource.CampaignScriptedmessage %>
                                            <input type="checkbox" name="ckb_method" value="4" id="ckb_method_4" class="js_enter" />
                                            <%=Resources.CRMTREESResource.CampaignScriptedWeibo %>
                                            <input type="checkbox" name="ckb_method" value="5" id="ckb_method_5" class="js_enter" />
                                            <%=Resources.CRMTREESResource.CampaignScriptStd %>
                                        </label>
                                    </div>
                                    <div class="Meth_s" style="width: 130px;">
                                        <p class="ps"><%= Resources.CRMTREESResource.CampaignText %></p>
                                        <label>
                                            <input type="checkbox" name="ckb_method" value="6" id="ckb_method_6" class="js_enter" />
                                            <%= Resources.CRMTREESResource.CampaginEMail %>
                                            <input type="checkbox" name="ckb_method" value="7" id="ckb_method_7" class="js_enter" />
                                            <%=Resources.CRMTREESResource.CampaginMailers %>
                                        </label>
                                        <br />
                                        <label>
                                            <input type="checkbox" name="ckb_method" value="8" id="ckb_method_8" class="js_enter" /><%= Resources.CRMTREESResource.CampaginDekuApp %>
                                        </label>
                                    </div>
                                    <span id="sp_method" class="red" style="display: none;">Please method</span>
                                </td>
                            </tr>
                            <%-- 事件风格--%>
                            <tr>
                                <td class="table_title" colspan="2">
                                    <span class="red">*</span><%= Resources.CRMTREESResource.EventGenre %></td>
                                <td class="table_cont">
                                    <input type="hidden" id="hid_Genre" value="<%=Event.EV_EG_Code %>" />
                                    <select id="ddl_Genre" class="js_enter boder_height" style="width: 500px;">
                                        <%=strDLL_Genre %>
                                    </select>
                                    <br />
                                    <span id="sp_Genre" class="red" style="display: none;"><%= Resources.CRMTREESResource.CampaginCon5 %></span>
                                </td>
                            </tr>
                            <%-- 事件保留请求--%>
                            <tr>
                                <td class="table_title" colspan="2">
                                    <span class="red">*</span><%=Resources.CRMTREESResource.EventRSVP %></td>
                                <td class="table_cont">
                                    <input type="hidden" id="hid_RSVP" value="<%=Event.EV_RSVP %>" />
                                    <div style="float: left; margin-right: 10px; border-radius: 4px; border: 1px solid #d2d2d2; width: 130px;">
                                        <label>
                                            <input type="radio" name="RSVP" value="1" id="RSVP_1" checked="checked" />
                                            <%=Resources.CRMTREESResource.EventYes %>
                                        </label>
                                        <label>
                                            <input type="radio" name="RSVP" value="0" id="RSVP_0" />
                                            <%=Resources.CRMTREESResource.EventNo %>
                                        </label>
                                    </div>
                                </td>
                            </tr>
                            <%-- 事件活动时间--%>
                            <tr>
                                <td class="table_title" colspan="2">
                                    <span class="red">*</span><%=Resources.CRMTREESResource.EventDate %>
                                </td>
                                <td class="table_cont">
                                    <span>
                                        <input id="EV_Act_S_dt" class="input_text Wdate js_enter" type="text" onfocus="WdatePicker({lang: 'en', dateFmt: 'MM/dd/yyyy', minDate: '%y-%M-%d'})" value="<%=Event.EV_Act_S_dt.ToString("MM/dd/yyyy") %>" />
                                        -
                                        <input id="EV_Act_E_dt" class="input_text Wdate js_enter" type="text" onfocus="WdatePicker({lang: 'en', dateFmt: 'MM/dd/yyyy', minDate:'#F{$dp.$D(\'EV_Act_S_dt\')}'})" value="<%=Event.EV_Act_E_dt.ToString("MM/dd/yyyy") %>" />
                                        <span id="sp_Act_times" class="red" style="display: none;"></span>
                                    </span>
                                </td>
                            </tr>
                            <%-- 相应人员--%>
                            <tr>
                                <td class="table_title" colspan="2">
                                    <span class="red">*</span><%=Resources.CRMTREESResource.EventPersonResponse %></td>
                                <td class="table_cont">
                                    <input type="hidden" id="hid_Person" value="<%=Event.EV_Respnsible %>" />
                                    <input id="DRP_Person" class="easyui-combobox" style="width: 350px;" name="language" />
                                    <br />
                                    <span id="sp_Person" class="red" style="display: none;">This option cannot be empty!</span>
                                </td>
                            </tr>
                            <%-- 推荐工具--%>
                            <tr>
                                <td class="table_title" colspan="2">
                                    <%=Resources.CRMTREESResource.EventRecommendedools %>
                                </td>
                                <td class="table_cont">
                                    <input type="hidden" id="hid_Tools" value="<%=Event.EV_Tools %>" />
                                    <input id="DRP_Tools" class="easyui-combobox" style="width: 350px;" name="language" />
                                    <br />
                                    <span id="sp_Tools" class="red" style="display: none;"><%= Resources.CRMTREESResource.CampaginCon5 %></span>
                                </td>
                            </tr>
                            <%-- 预算--%>
                            <tr>
                                <td class="table_title" colspan="2" style="width: 22%"><%=Resources.CRMTREESResource.EventBudget %>
                                </td>
                                <td class="table_cont" style="width: 78%">
                                    <input type="hidden" id="hid_Budget" value="<%=Event.EV_Budget %>" />
                                    <input type="text" class="input_text_w400 js_enter" id="txt_Budget" name="txt_title" maxlength="10" style="width: 200px;" value="" />
                                    <span id="sp_Budget" class="red" style="display: none;"><%= Resources.CRMTREESResource.CampaginCon8 %></span>
                                </td>
                            </tr>
                            <%--  成功竞选模型--%>
                            <tr>
                                <td class="table_title" colspan="2">
                                    <span class="red">*</span><%= Resources.CRMTREESResource.CampaginSM %></td>
                                <td class="table_cont">
                                    <input type="hidden" id="hid_Succ" value="<%=Event.EV_Succ_Matrix %>" />
                                  <%--  <select id="ddl_Succ" class="js_enter boder_height" style="width: 500px;">
                                        <%=strDLL_Succ %>
                                    </select>--%>
                                    <%--<br />--%>
                                    <span id="sp_Success" class="red" style="display: none;"><%= Resources.CRMTREESResource.CampaginCon5 %></span>
                                    <div id="p" class="easyui-panel" title="Please click on the select Matrix" style="width: 600px; height: 200px; padding: 5px;"
                                        data-options="collapsible:true,collapsed:false">
                                        <p class="succ_validate" style="font-size: 13px; text-align: right; color: red; display: none">
                                            Please set the selection of parameter matrix!
                                        </p>
                                        
                                        <input id="Succ" class="easyui-combobox" style="width: 350px;" name="language" />
                                        <p style="font-size: 14px">Please set the selection of parameter matrix:</p>
                                        <ul class="succ_Matri_param" style="padding: 5px">
                                            <li class="succ_Matri_L">Please setPlease 
                                                <input class="easyui-textbox" style="width: 50px; height: 22px" />
                                                >>>Please set 
                                                <input class="easyui-textbox" style="width: 100px; height: 22px" /></li>
                                            <li class="succ_Matri_L">Please setPlease 
                                                <input class="easyui-textbox" style="width: 50px; height: 22px" />
                                                >>>Please set 
                                                <input class="easyui-textbox" style="width: 100px; height: 22px" /></li>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                            <%-- Campagin消息文件名--%>
                            <tr>
                                <td class="table_title" colspan="2"><span class="red">*</span><%=Resources.CRMTREESResource.EventMessageFile %></td>
                                <td class="table_cont">
                                    <div id="File_E" style="height: 35px; width: 170px;">
                                        <span id="spanButtonPlaceHolder"></span>
                                        <div style="float: right">
                                            <input type="button" id="Create_file" value="Create" />
                                        </div>
                                    </div>
                                    <br />
                                    <div id="divprogresscontainer"></div>
                                    <input type="hidden" id="hdFileNmae" name="hdFileNmae" value="<%=Event.EV_Filename %>" />
                                    <div id="divprogressGroup"></div>
                                    <br />
                                    <span id="sp_File" class="red" style="display: none;">Please upload or create a Event file! </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="detail_menu StdBtn1">
                        <input type="button" value="<%= Resources.CRMTREESResource.CampaginSAVE %>" class="menu_submit_med" id="btnSave" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <a class="btnBack" href="javascript: history.go(-1);"><%= Resources.CRMTREESResource.CampagonBL %></a>
                    </div>
                </div>
            </div>
            <uc1:CrmTreebottom runat="server" ID="CrmTreebottom" />
        </div>
        <script type="text/javascript">
            $("#nav_Events").addClass("nav_select");
        </script>
    </form>
    <input id="hidUpdateTargeted_RP_code" type="hidden" value="0" />
    <input id="hidUpdateTargeted_Count" type="hidden" value="0" />
    <div class="hidUpdateTargeted" style="display: none;"></div>
</body>
</html>
