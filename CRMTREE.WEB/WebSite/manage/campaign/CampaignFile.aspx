<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampaignFile.aspx.cs" Inherits="manage_campaign_CampaignFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="/scripts/jquery/jquery-1.8.2.min.js"></script>
    <script src="/scripts/jquery/jquery.extend.js" type="text/javascript"></script>
    <script src="/scripts/common/json2.js" type="text/javascript"></script>
    <link href="/scripts/jquery-easyui/themes/metro-green/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery-easyui/themes/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-easyui/jquery.easyui.extend.js" type="text/javascript"></script>
</head>
<body style="margin: 0px; padding: 0px;">
    <div class="easyui-panel" data-options="fit:true,footer:'#ft',border:false" style="overflow:hidden;">
        <iframe id="ctrl_iframe" src="" style="width:100%;height:100%;" frameborder="0" border="0" scrolling="no">

        </iframe>
    </div>
    <div id="ft" style="padding:5px; text-align:right;padding-right:20px;">
        <a class="easyui-linkbutton" id="btnSave" data-options="iconCls:'icon-save',onClick:_file.save" style="width:80px;"><%=Resources.CRMTREEResource.cm_buttons_save%></a>
        <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_file.close" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.cm_buttons_close%></a>
    </div>
    
    <script type="text/javascript">
        var _params = $.getParams();
        _params.filename = _params.filename ? $.trim(_params.filename) : "";
        var _t = _params.T ? _params.T : 0;
        var _file = {
            save: function () {
                var iframe = $("#ctrl_iframe")[0];
                var content = iframe.contentWindow.getContent();
                var contentTxt = iframe.contentWindow.getContentTxt();
                
                $.ajax({
                    type: "POST", dataType: "json", url: "/handler/ajax_campaign.aspx",
                    data: { action: "campaign_create_file", T: _t, fileName: _params.filename, fileHtml: escape(content), fileText: escape(contentTxt) },
                    success: function (res) {
                        if ($.checkResponse(res)) {
                            if (res.isCreate) {
                                var fileName = res.fileName;
                                window.top[_params._winID]._Camp_Methods.file.change(fileName);
                            }
                            window.top[_params._winID].$.msgTips.save(true);
                            _file.close();
                        } else {
                            $.msgTips.save(false);
                        }
                    }
                });
            },
            close: function () {
                if (window._closeOwnerWindow) {
                    window._closeOwnerWindow();
                }else {
                    window.close();
                }
            }
        };
        var params = "?action=edit";
        if (_params.filename) {
            params += "&filename=" + _params.filename;
        }
        if (_params.T) {
            params += "&T=" + _params.T;
        }
        $("#ctrl_iframe").attr("src", "/manage/campaign/CampaignFileEdit.aspx" + params);
    </script>
</body>
</html>
