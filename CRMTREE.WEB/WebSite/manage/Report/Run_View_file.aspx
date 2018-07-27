<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Run_View_file.aspx.cs" Inherits="manage_Report_Run_file" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/styles/ST_001.css" rel="stylesheet" />
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>

    <script src="/scripts/common/json2.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _isEn = $.isEn();
        if (!_isEn) {
            document.write('<script src="/scripts/jquery-easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></sc' + 'ript>');
        }
    </script>
     <style type="text/css">
        html, body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            font-size:12px;
        }

        .red {
            color:red;
        }

        .tbl {
            width:100%;
            border-top:1px solid #ccc;
            border-left:1px solid #ccc;
        }
            .tbl tr th.th {
                font-weight:normal;
                text-align:right;
                padding-right:15px;
                font-size:14px;
                background-color:#E7EADB;
            }
            .tbl tr th.top {
                vertical-align:top;
            }
            .tbl tr .th,.tbl tr .td {
                padding:6px;
                border-right:1px solid #ccc;
                border-bottom:1px solid #ccc;
                padding-top:10px;
                padding-bottom:10px;
            }
    </style>
</head>
<body style="margin: 0px; padding: 0px">
    <div style="width: 100%; padding: 4px; margin: 0px">    
        <table class="tbl" cellpadding="0" cellspacing="0">
            <tr>                
                <th class="th">
                   <%= Resources.CRMTREEResource.RunView_phone %>
                </th>
                <td class="td">
                    <label id="PL_Number"></label>
                </td>
            </tr>
            <tr id="c_tr_notes">
                <th class="th">
                   <%= Resources.CRMTREEResource.RunView_info %>
                </th>
                <td class="td">
                    <input id="DH_Notes" class="easyui-textbox" data-options="tipPosition:'bottom',required:false,novalidate:true,multiline:true,width:300,height:160"/>
                </td>
            </tr>
            <tr>
                <td class="td"></td>
                <td class="td" style="text-align:left;padding-left:10px;">
                    <a class="easyui-linkbutton" id="btnDone" data-options="iconCls:'icon-save',onClick:win.Send" style="width:80px;"><%= Resources.CRMTREEResource.RunView_send %></a>
                    &nbsp&nbsp 
                     <a class="easyui-linkbutton" id="btnCancel" data-options="iconCls:'icon-cancel',onClick:win.close" style="width:80px;margin-left:20px;">
                <%=Resources.CRMTREEResource.RunView_cancel %>
            </a>
                </td>
            </tr>
        </table></div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Campaign/CampaignManager.aspx';
    var _params = $.getParams();
    var win = {
        close: function () {
            window.close();
        },
        Send: function () {
            var _Test = $("#DH_Notes").textbox("getText");
            //alert(_Test)
            var s_params = JSON.stringify({ action: 'Get_Send_file', AU_Code: _params.AU_Code, AP_Code: _params.AP_Code, Content: _Test });
            $.post(_s_url, { o: s_params }, function (res) {
                if ($.checkErrCode(res)) {
                    window.close();
                }
            }, "json");
        }
    };
    $(function () {
        var s_params = JSON.stringify({ action: 'Get_view_file', AU_Code: _params.AU_Code, AP_Code: _params.AP_Code });
        $.post(_s_url, { o: s_params }, function (res) {
            if ($.checkErrCode(res)) {
                var _html = unescape(res._con);
                $("#DH_Notes").textbox("setText", _html);
                $("#PL_Number").text(res._pl);
            }
        }, "json");
    });
</script>
