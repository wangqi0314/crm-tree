<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenancePackageDetails.aspx.cs" Inherits="templete_usercontrol_MaintenancePackageDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="/scripts/jquery/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>

    <script src="/scripts/common/json2.js" type="text/javascript"></script>
    <%--<link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        var _isEn = $.isEn();
        //if (!_isEn) {
        //    document.write('<script src="/scripts/jquery-easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></sc' + 'ript>');
        //}
    </script>
    <style type="text/css">
        html, body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            font-size:14px;
        }
        .table{
            border-top:1px solid #ccc;
            border-left:1px solid #ccc;
            width:100%;
            margin:0 auto;
        }
        .table tr th,.table tr td{
            border-right:1px solid #ccc;
            border-bottom:1px solid #ccc;
            position:relative;
        }
        .table tr td div{
            padding:5px;
        }

        .table tr th{
            padding:5px;
        }
        .dt tr td{
            padding:5px;
        }

        .left {
            float:left;
        }
    </style>
</head>
<body>
<div style="position:relative;width:100%;height:100%;overflow:auto;">
    <table cellspacing="0" cellpadding="0" border="0" style="width:650px;margin:0 auto;margin-top:10px;">
        <tr>
            <td style="vertical-align:top;padding-top:10px;">
                <table class="table" cellspacing="0" cellpadding="0" border="0">
                    <tr style="background-color:rgb(252, 213, 62)">
                        <td>
                            <div class="left" style="width:70%;border-right:1px solid #ccc;">&nbsp;<span id="RS_Desc"></span></div>
                            <div class="left">&nbsp;<span id="CS_Style"></span></div>
                        </td>
                    </tr>
                    <tr style ="background-color:yellowgreen">
                        <td>
                            <div class="left" style="width:50%;border-right:1px solid #ccc;"><%=Resources.CRMTREEResource.ap_mp_totalvalue %> <span id="PRC_Value_Total" style="font-weight:bold;margin-left:10px;"></span></div>
                            <div class="left"><%=Resources.CRMTREEResource.ap_mp_totalprice %> <span id="PRC_Price_Total" style="margin-left:10px;font-weight:bold;font-size:24px"></span></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:20px;">
                            <table id="dt" class="table dt" cellspacing="0" cellpadding="0" border="0">
                                <thead>
                                    <tr>
                                        <th style="width:60%;"><%=Resources.CRMTREEResource.ap_mp_Item %></th>
                                        <th style="width:20%;"><%=Resources.CRMTREEResource.ap_mp_Value %></th>
                                        <th style="width:20%;"><%=Resources.CRMTREEResource.ap_mp_price %></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Reports/AppointmentManager.aspx';
    var _params = $.getParams();

    //阻止事件冒泡
    var stopPropagation = function (e) {
        if (!e) {
            return;
        }
        // If stopPropagation exists, run it on the original event
        if (e.stopPropagation) {
            e.stopPropagation();
        }

        // Support: IE
        // Set the cancelBubble property of the original event to true
        e.cancelBubble = true;
    }

    function CheckResponse (res) {
        var bCheck = true;
        if (!res || res.isOK === false) {
            bCheck = false;
        }
        return bCheck;
    }

    $(function () {
        if (!(_params.MP_Code > 0)) { return; }
        var params = {
            action: 'Get_Maintenance_Package',
            MP_Code: _params.MP_Code,
            RS_Code: _params.RS_Code ? _params.RS_Code : 0,
            CI_Code: _params.CI_Code ? _params.CI_Code : 0
        };
        $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
            if (CheckResponse(res)) {
                var mp = res.Table[0];
                var titles = res.Table1;
                var items = res.Table2;

                if (mp) {
                    $.each(mp, function (n, d) {
                        var $o = $("#" + n);
                        if ($o.length > 0) {
                            $o.append($.trim(d));
                        }
                    });
                }

                if (items) {

                    var a = [];

                    $.each(titles, function (i, title) {
                        var price = $.trim(title.PRC_Price);
                        price = price == 0 ? '<%=Resources.CRMTREEResource.ap_mp_Free %>' : price;
                        a.push([
                                    '<tr>'
                                    , '<td style="background-color:#FEF4C6;font-weight:bold;">&nbsp;' + $.trim(title.PRH_Title) + '</td>'
                                    , '<td rowspan="' + title.rowspan + '" style="text-align:center;">&nbsp;' + $.trim(title.PRC_Value) + '</td>'
                                    , '<td rowspan="' + title.rowspan + '" style="text-align:center;">&nbsp;' + price + '</td>'
                                    , '</tr>'
                        ].join(''));
                        var len = title.rowspan - 1;
                        var newItems = items.slice(0, len);
                        items.splice(0, len);
                        $.each(newItems, function (i, item) {
                            a.push([
                                    '<tr>'
                                    , '<td>&nbsp;' + $.trim(item.SC_Desc) + '</td>'
                                    , '</tr>'
                            ].join(''));
                        });
                    });
                    $("#dt>tbody").append(a.join(''));
                }
            }
        }, "json");
    });
</script>