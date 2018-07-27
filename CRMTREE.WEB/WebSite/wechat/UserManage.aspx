<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManage.aspx.cs" Inherits="wechat_UserManage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>微信公众平台</title>
    <link href="/images/Fun.ico" rel="Shortcut Icon" />
    <link rel="stylesheet" type="text/css" href="/css/Wechat/layout_head20273e.css" />
    <link rel="stylesheet" type="text/css" href="/css/Wechat/base211edb.css" />
    <link rel="stylesheet" type="text/css" href="/css/Wechat/lib1ffa7e.css" />
    <link rel="stylesheet" href="/css/Wechat/page_user1ec5f7.css" />
    <link rel="stylesheet" type="text/css" href="/css/Wechat/pagination1ec5f7.css" />
    <script src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script src="/scripts/Wechat/W_UserManage.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { Page();});
    </script>
</head>
<body class="zh_CN">
    <div class="head" id="header">
        <div class="head_box">
            <div class="inner wrp">
                <h1 class="logo"><a title="微信公众平台" href="/"></a></h1>
                <div class="account">
                    <div class="account_meta account_info account_meta_primary">
                        <a class="nickname" href="/cgi-bin/settingpage?t=setting/index&amp;action=index&amp;token=795126424&amp;lang=zh_CN">思树-大E库</a>                                <span class="type_wrp"><a class="type icon_service_label" href="/cgi-bin/settingpage?t=setting/index&amp;action=index&amp;token=795126424&amp;lang=zh_CN">服务号</a>                                                            <a class="type icon_verify_label success" href="/merchant/store?action=detail&amp;t=wxverify/detail&amp;info=verify&amp;lang=zh_CN&amp;token=795126424">已认证</a>                                    </span><a href="/cgi-bin/settingpage?t=setting/index&amp;action=index&amp;token=795126424&amp;lang=zh_CN">
                            <img class="avatar" src="/misc/getheadimg?fakeid=2391543171&amp;token=795126424&amp;lang=zh_CN"></a>
                    </div>
                    <div class="account_meta account_inbox account_meta_primary" id="accountArea"><a class="account_inbox_switch" href="/cgi-bin/frame?t=notification/index_frame&amp;lang=zh_CN&amp;token=795126424"><i class="icon_inbox">通知</i>&nbsp;                </a></div>
                    <div class="account_meta account_logout account_meta_primary"><a id="logout" href="/cgi-bin/logout?t=wxm-logout&amp;lang=zh_CN&amp;token=795126424">退出</a></div>
                </div>
            </div>
        </div>
    </div>
    <div class="body page_user" id="body">
        <div class="container_box cell_layout side_l" id="js_container_box">
            <div class="col_side">
            </div>
            <div class="col_main">
                <div class="main_bd">
                    <div class="mod_hd">
                        <div class="mod_info">分组名称<span class="group_name" id="js_groupName">全部用户</span></div>
                        <div class="mod_opr">
                            <a class="btn btn_primary" id="js_groupAdd" href="javascript:;" data-y="3">
                                <i class="icon14_common add_white"></i>新建分组</a>
                        </div>
                    </div>
                    <div class="inner_container_box side_r cell_layout">
                        <div class="inner_main">
                            <div class="bd">
                                <div class="table_wrp user_list">
                                    <table class="table" cellspacing="0">
                                        <thead class="thead">
                                            <tr>
                                                <th class="table_cell user no_extra" colspan="3">
                                                    <div class="group_select">
                                                        <label class="group_select_label frm_checkbox_label" for="selectAll">
                                                            <i class="icon_checkbox"></i>
                                                            <input class="frm_checkbox" id="selectAll" type="checkbox" />
                                                            全选
                                                        </label>
                                                        &nbsp;
                                                        <div class="dropdown_wrp dropdown_menu disabled" id="allGroup">
                                                            <a class="btn dropdown_switch jsDropdownBt" href="javascript:;">
                                                                <label class="jsBtLabel">添加到</label><i class="arrow"></i></a>
                                                            <div class="dropdown_data_container jsDropdownList" style="display: none;">
                                                                <ul class="dropdown_data_list">
                                                                    <li class="dropdown_data_item ">
                                                                        <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="未分组" data-index="0" data-value="0">未分组</a>
                                                                    </li>
                                                                    <li class="dropdown_data_item ">
                                                                        <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="黑名单" data-index="1" data-value="1">黑名单</a>
                                                                    </li>
                                                                    <li class="dropdown_data_item ">
                                                                        <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="星标组" data-index="2" data-value="2">星标组</a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody class="tbody" id="userGroups">
                                         <%--   <tr>
                                                <td class="table_cell user">
                                                    <div class="user_info">
                                                        <a class="remark_name" href="SendMessage.aspx?n=<%=HttpUtility.UrlEncode("王琪") %>&o=ogw2MjuNlKXBNeWXtKfrL6lYFH48" target="_blank" data-fakeid="1472492616">王琪</a>
                                                        <span class="nick_name" data-fakeid="1472492616"></span>
                                                        <a class="avatar" href="SendMessage.aspx" target="_blank">
                                                            <img class="js_msgSenderAvatar"
                                                                src="http://wx.qlogo.cn/mmopen/A2ia16n3R4EQ4SyR35B8tJ0zlQPBa3CtOmga0yZzZic8Oibc4IhIMJlZqq2iaOpHib9dYGX4pFhOuHrrS75FfJEwiaAgc4fIgibXG7f/0"
                                                                data-fakeid="1472492616" />
                                                        </a>
                                                        <label class="frm_checkbox_label" for="check1472492616">
                                                            <i class="icon_checkbox"></i>
                                                            <input class="frm_checkbox js_select" id="check1472492616" type="checkbox" value="1472492616"></label>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_category">
                                                    <div class="js_selectArea dropdown_menu" id="selectArea1472492616" data-fid="1472492616" data-gid="0">
                                                        <a class="btn dropdown_switch jsDropdownBt" href="javascript:;">
                                                            <label class="jsBtLabel">未分组</label><i class="arrow"></i></a>
                                                        <div class="dropdown_data_container jsDropdownList" style="display: none;">
                                                            <ul class="dropdown_data_list">
                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="未分组" data-index="0" data-value="0">未分组</a>
                                                                </li>
                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="黑名单" data-index="1" data-value="1">黑名单</a>
                                                                </li>
                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="星标组" data-index="2" data-value="2">星标组</a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_opr">
                                                    <a class="btn remark js_msgSenderRemark" data-fakeid="1472492616">修改备注</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_cell user">
                                                    <div class="user_info">
                                                        <a class="remark_name" href="SendMessage.aspx?n=<%=HttpUtility.UrlEncode("贱小样") %>&o=ogw2MjrJ-FUz6IU-zc5Hchg62wDg" target="_blank" data-fakeid="1991173660">贱小样</a>
                                                        <span class="nick_name" data-fakeid="1991173660"></span>

                                                        <a class="avatar" href="SendMessage.aspx" target="_blank">
                                                            <img class="js_msgSenderAvatar" src="http://wx.qlogo.cn/mmopen/K6CEv0Hv9DcUy9qrOwkOsBbtibuse1p0oGoetnbkOzdYwWFjyzKWP5OVulaEKPYeDroZUT3oOicWCfw7SSZicrEuHceiaCFcUGl2/0" data-fakeid="1991173660">
                                                        </a>
                                                        <label class="frm_checkbox_label" for="check1991173660">
                                                            <i class="icon_checkbox"></i>
                                                            <input class="frm_checkbox js_select" id="check1991173660" type="checkbox" value="1991173660"></label>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_category">
                                                    <div class="js_selectArea dropdown_menu" id="selectArea1991173660" data-fid="1991173660" data-gid="0">
                                                        <a class="btn dropdown_switch jsDropdownBt" href="javascript:;">
                                                            <label class="jsBtLabel">未分组</label><i class="arrow"></i></a>
                                                        <div class="dropdown_data_container jsDropdownList" style="display: none;">
                                                            <ul class="dropdown_data_list">


                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="未分组" data-index="0" data-value="0">未分组</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="黑名单" data-index="1" data-value="1">黑名单</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="星标组" data-index="2" data-value="2">星标组</a>
                                                                </li>


                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_opr">
                                                    <a class="btn remark js_msgSenderRemark" data-fakeid="1991173660">修改备注</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_cell user">
                                                    <div class="user_info">

                                                        <a class="remark_name" href="SendMessage.aspx?n=<%=HttpUtility.UrlEncode("拼命追") %>&o=ogw2MjuNlKXBNeWXtKfrL6lYFH48" target="_blank" data-fakeid="2555071522">拼命追</a>
                                                        <span class="nick_name" data-fakeid="2555071522"></span>

                                                        <a class="avatar" href="SendMessage.aspx" target="_blank">
                                                            <img class="js_msgSenderAvatar" src="http://wx.qlogo.cn/mmopen/aibCTFHaM4kVy8aH9lLNbicbtleCjOBclibWeInJDglo2WW6kPTd7govoNL4Onmy7KWETtlKyicKzyuXWaxhr7eMriaeCkamIaDDz/0" data-fakeid="2555071522">
                                                        </a>
                                                        <label class="frm_checkbox_label" for="check2555071522">
                                                            <i class="icon_checkbox"></i>
                                                            <input class="frm_checkbox js_select" id="check2555071522" type="checkbox" value="2555071522"></label>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_category">
                                                    <div class="js_selectArea dropdown_menu" id="selectArea2555071522" data-fid="2555071522" data-gid="0">
                                                        <a class="btn dropdown_switch jsDropdownBt" href="javascript:;">
                                                            <label class="jsBtLabel">未分组</label><i class="arrow"></i></a>
                                                        <div class="dropdown_data_container jsDropdownList" style="display: none;">
                                                            <ul class="dropdown_data_list">


                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="未分组" data-index="0" data-value="0">未分组</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="黑名单" data-index="1" data-value="1">黑名单</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="星标组" data-index="2" data-value="2">星标组</a>
                                                                </li>


                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_opr">
                                                    <a class="btn remark js_msgSenderRemark" data-fakeid="2555071522">修改备注</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_cell user">
                                                    <div class="user_info">

                                                        <a class="remark_name" href="SendMessage.aspx?n=<%=HttpUtility.UrlEncode("Winnie") %>&o=ogw2MjuNlKXBNeWXtKfrL6lYFH48" target="_blank" data-fakeid="195744175">Winnie</a>
                                                        <span class="nick_name" data-fakeid="195744175"></span>

                                                        <a class="avatar" href="SendMessage.aspx" target="_blank">
                                                            <img class="js_msgSenderAvatar" src="http://wx.qlogo.cn/mmopen/PiajxSqBRaEItI5P4loviaYtOhxaMGMLiaq00SZTdl33yjmeNYEKlT3oK0qg1t9B5vRKlFdY8NRZtSiav42TJphFuQ/0" data-fakeid="195744175">
                                                        </a>
                                                        <label class="frm_checkbox_label" for="check195744175">
                                                            <i class="icon_checkbox"></i>
                                                            <input class="frm_checkbox js_select" id="check195744175" type="checkbox" value="195744175"></label>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_category">
                                                    <div class="js_selectArea dropdown_menu" id="selectArea195744175" data-fid="195744175" data-gid="0">
                                                        <a class="btn dropdown_switch jsDropdownBt" href="javascript:;">
                                                            <label class="jsBtLabel">未分组</label><i class="arrow"></i></a>
                                                        <div class="dropdown_data_container jsDropdownList" style="display: none;">
                                                            <ul class="dropdown_data_list">


                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="未分组" data-index="0" data-value="0">未分组</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="黑名单" data-index="1" data-value="1">黑名单</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="星标组" data-index="2" data-value="2">星标组</a>
                                                                </li>


                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_opr">
                                                    <a class="btn remark js_msgSenderRemark" data-fakeid="195744175">修改备注</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_cell user">
                                                    <div class="user_info">

                                                        <a class="remark_name" href="SendMessage.aspx?n=<%=HttpUtility.UrlEncode("yuxin") %>&o=ogw2MjuNlKXBNeWXtKfrL6lYFH48" target="_blank" data-fakeid="2471196600">yuxin</a>
                                                        <span class="nick_name" data-fakeid="2471196600"></span>

                                                        <a class="avatar" href="SendMessage.aspx" target="_blank">
                                                            <img class="js_msgSenderAvatar" src="http://wx.qlogo.cn/mmopen/4gS6TfSFHxNwrPlbEhwkRna78MBoKR7z1arqXyoLqavQVriatE3DJyh9PIsP5ErVBKterNXxfVdB97DvFhnYvJcGlS8rXsLuB/0" data-fakeid="2471196600">
                                                        </a>
                                                        <label class="frm_checkbox_label" for="check2471196600">
                                                            <i class="icon_checkbox"></i>
                                                            <input class="frm_checkbox js_select" id="check2471196600" type="checkbox" value="2471196600"></label>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_category">
                                                    <div class="js_selectArea dropdown_menu" id="selectArea2471196600" data-fid="2471196600" data-gid="0">
                                                        <a class="btn dropdown_switch jsDropdownBt" href="javascript:;">
                                                            <label class="jsBtLabel">未分组</label><i class="arrow"></i></a>
                                                        <div class="dropdown_data_container jsDropdownList" style="display: none;">
                                                            <ul class="dropdown_data_list">


                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="未分组" data-index="0" data-value="0">未分组</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="黑名单" data-index="1" data-value="1">黑名单</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="星标组" data-index="2" data-value="2">星标组</a>
                                                                </li>


                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_opr">
                                                    <a class="btn remark js_msgSenderRemark" data-fakeid="2471196600">修改备注</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_cell user">
                                                    <div class="user_info">

                                                        <a class="remark_name" href="SendMessage.aspx?n=<%=HttpUtility.UrlEncode("Fariborz in Shanghai") %>&o=ogw2MjuNlKXBNeWXtKfrL6lYFH48" target="_blank" data-fakeid="1154259143">Fariborz in Shanghai</a>
                                                        <span class="nick_name" data-fakeid="1154259143"></span>

                                                        <a class="avatar" href="SendMessage.aspx" target="_blank">
                                                            <img class="js_msgSenderAvatar" src="" data-fakeid="1154259143">
                                                        </a>
                                                        <label class="frm_checkbox_label" for="check1154259143">
                                                            <i class="icon_checkbox"></i>
                                                            <input class="frm_checkbox js_select" id="check1154259143" type="checkbox" value="1154259143"></label>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_category">
                                                    <div class="js_selectArea dropdown_menu" id="selectArea1154259143" data-fid="1154259143" data-gid="0">
                                                        <a class="btn dropdown_switch jsDropdownBt" href="javascript:;">
                                                            <label class="jsBtLabel">未分组</label><i class="arrow"></i></a>
                                                        <div class="dropdown_data_container jsDropdownList" style="display: none;">
                                                            <ul class="dropdown_data_list">


                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="未分组" data-index="0" data-value="0">未分组</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="黑名单" data-index="1" data-value="1">黑名单</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="星标组" data-index="2" data-value="2">星标组</a>
                                                                </li>


                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_opr">
                                                    <a class="btn remark js_msgSenderRemark" data-fakeid="1154259143">修改备注</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_cell user">
                                                    <div class="user_info">

                                                        <a class="remark_name" href="SendMessage.aspx?n=<%=HttpUtility.UrlEncode("王琪") %>&o=ogw2MjhK4qPJbDSmtCeXWLezqAOM" target="_blank" data-fakeid="1186264660">王琪</a>
                                                        <span class="nick_name" data-fakeid="1186264660"></span>

                                                        <a class="avatar" href="SendMessage.aspx" target="_blank">
                                                            <img class="js_msgSenderAvatar" src="http://wx.qlogo.cn/mmopen/3wkgibn3bn9Bbdu75xUQGGhHMghhz8kT0Lq20073LfmqLvjC7FFiaq1ib3H2iaNhXmdKgmt8VfXQMs3pU1FDpPdrYQJaop4SB7qs/0" data-fakeid="1186264660">
                                                        </a>
                                                        <label class="frm_checkbox_label" for="check1186264660">
                                                            <i class="icon_checkbox"></i>
                                                            <input class="frm_checkbox js_select" id="check1186264660" type="checkbox" value="1186264660"></label>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_category">
                                                    <div class="js_selectArea dropdown_menu" id="selectArea1186264660" data-fid="1186264660" data-gid="0">
                                                        <a class="btn dropdown_switch jsDropdownBt" href="javascript:;">
                                                            <label class="jsBtLabel">未分组</label><i class="arrow"></i></a>
                                                        <div class="dropdown_data_container jsDropdownList" style="display: none;">
                                                            <ul class="dropdown_data_list">


                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="未分组" data-index="0" data-value="0">未分组</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="黑名单" data-index="1" data-value="1">黑名单</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="星标组" data-index="2" data-value="2">星标组</a>
                                                                </li>


                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_opr">
                                                    <a class="btn remark js_msgSenderRemark" data-fakeid="1186264660">修改备注</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="table_cell user">
                                                    <div class="user_info">

                                                        <a class="remark_name" href="SendMessage.aspx" target="_blank" data-fakeid="430926335">王鹏程</a>
                                                        <span class="nick_name" data-fakeid="430926335"></span>

                                                        <a class="avatar" href="SendMessage.aspx" target="_blank">
                                                            <img class="js_msgSenderAvatar" src="http://wx.qlogo.cn/mmopen/aibCTFHaM4kU1oBJLgialRicRjsQjRBLyvpPibx9m8k4K1aOWMImrbcpGqGHgIR5ELQEh8p8PQhHak9nEjankZ1zWOjZnyqYvua5/0" data-fakeid="430926335">
                                                        </a>
                                                        <label class="frm_checkbox_label" for="check430926335">
                                                            <i class="icon_checkbox"></i>
                                                            <input class="frm_checkbox js_select" id="check430926335" type="checkbox" value="430926335"></label>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_category">
                                                    <div class="js_selectArea dropdown_menu" id="selectArea430926335" data-fid="430926335" data-gid="0">
                                                        <a class="btn dropdown_switch jsDropdownBt" href="javascript:;">
                                                            <label class="jsBtLabel">未分组</label><i class="arrow"></i></a>
                                                        <div class="dropdown_data_container jsDropdownList" style="display: none;">
                                                            <ul class="dropdown_data_list">


                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="未分组" data-index="0" data-value="0">未分组</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="黑名单" data-index="1" data-value="1">黑名单</a>
                                                                </li>

                                                                <li class="dropdown_data_item ">
                                                                    <a class="jsDropdownItem" onclick="return false;" href="javascript:;" data-name="星标组" data-index="2" data-value="2">星标组</a>
                                                                </li>


                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="table_cell user_opr">
                                                    <a class="btn remark js_msgSenderRemark" data-fakeid="430926335">修改备注</a>
                                                </td>
                                            </tr>--%>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="tool_area">
                                    <div class="pagination_wrp js_pageNavigator"></div>
                                </div>
                            </div>
                        </div>
                        <div class="inner_side">
                            <div class="bd">
                                <div class="group_list">
                                    <div class="inner_menu_box" id="groupsList">
                                        <dl class="inner_menu">
                                            <dt class="inner_menu_item">
                                                <a title="全部用户" class="inner_menu_link" href="/cgi-bin/contactmanage?t=user/index&amp;pagesize=10&amp;pageidx=0&amp;type=0&amp;token=795126424&amp;lang=zh_CN">
                                                    <strong>全部用户</strong><em class="num">(8)</em>
                                                </a>
                                            </dt>
                                            <dd class="inner_menu_item ">
                                                <a title="未分组" class="inner_menu_link" href="/cgi-bin/contactmanage?t=user/index&amp;pagesize=10&amp;pageidx=0&amp;type=0&amp;groupid=0&amp;token=795126424&amp;lang=zh_CN">
                                                    <strong>未分组</strong><em class="num">(8)</em>
                                                </a>
                                            </dd>
                                            <dd class="inner_menu_item " id="group2">
                                                <a title="加入该分组中的用户仅作为更重要的用户归类标识" class="inner_menu_link" href="/cgi-bin/contactmanage?t=user/index&amp;pagesize=10&amp;pageidx=0&amp;type=0&amp;groupid=2&amp;token=795126424&amp;lang=zh_CN">
                                                    <strong>星标组</strong><em class="num">(0)</em>
                                                </a>
                                            </dd>
                                        </dl>
                                        <dl class="inner_menu no_extra">
                                            <dt class="inner_menu_item selected">
                                                <a title="黑名单" class="inner_menu_link" href="/cgi-bin/contactmanage?t=user/index&amp;pagesize=10&amp;pageidx=0&amp;type=0&amp;groupid=1&amp;token=795126424&amp;lang=zh_CN">
                                                    <strong>黑名单</strong><em class="num">(0)</em>
                                                </a>
                                            </dt>
                                        </dl>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
