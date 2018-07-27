<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Campaign_Message.aspx.cs" Inherits="manage_campaign_Campaign_Message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
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
    </style>
</head>
<body>
    <div class="easyui-panel" data-options="fit:true,footer:'#ft',border:false" style="overflow:hidden;">
        <div class="easyui-layout" data-options="fit:true">
            <div data-options="region:'north'" style="padding:5px;height:40px;overflow:hidden;">
                <input id="EX_Camp_Tags" class="easyui-combobox" data-options="editable:true" style="width:40%;"/>
                <a id="EX_Btn_Camp_Tags" class="easyui-linkbutton" data-options="plain:false,onClick:_message.insert" style="width:60px;">Insert</a>
            </div>
            <div data-options="region:'center'" style="padding:5px;overflow:hidden;">
                <input id="EX_TA_Camp_Tags" class="easyui-textbox" data-options="multiline:true,required:true,novalidate:true" style="width:100%;height:100%;"/>
            </div>
        </div>
    </div>
    <div id="ft" style="padding:5px; text-align:right;padding-right:20px;">
        <a class="easyui-linkbutton" id="btnSave" data-options="iconCls:'icon-save',onClick:_file.save" style="width:80px;"><%=Resources.CRMTREEResource.cm_buttons_save%></a>
        <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_file.close" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.cm_buttons_close%></a>
    </div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Campaign/CampaignManager.aspx';
    var _s_url_report = '/handler/Reports/Reports.aspx';
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

    var _file = {
        save: function () {
            //var iframe = $("#ctrl_iframe")[0];
            //var content = iframe.contentWindow.getContent();
            //var contentTxt = iframe.contentWindow.getContentTxt();

            //$.ajax({
            //    type: "POST", dataType: "json", url: "/handler/ajax_campaign.aspx",
            //    data: { action: "campaign_create_file", T: _t, fileName: _params.filename, fileHtml: escape(content), fileText: escape(contentTxt) },
            //    success: function (res) {
            //        if ($.checkResponse(res)) {
            //            if (res.isCreate) {
            //                var fileName = res.fileName;
            //                window.top[_params._winID]._campaign.file.change(fileName);
            //            }
            //            window.top[_params._winID].$.msgTips.save(true);
            //            _file.close();
            //        } else {
            //            $.msgTips.save(false);
            //        }
            //    }
            //});
        },
        close: function () {
            if (window._closeOwnerWindow) {
                window._closeOwnerWindow();
            } else {
                window.close();
            }
        }
    };

    _message = {
        insertAtCursor: function (txtarea, tag) {
            //IE
            if(document.selection) {
                var theSelection = document.selection.createRange().text;
                if(!theSelection) { theSelection=tag}
                txtarea.focus();
                if(theSelection.charAt(theSelection.length - 1) == " "){
                    theSelection = theSelection.substring(0, theSelection.length - 1);
                    document.selection.createRange().text = theSelection+ " ";
                } else {
                    document.selection.createRange().text = theSelection;
                }
                // Mozilla
            }else if(txtarea.selectionStart || txtarea.selectionStart == '0'){
                var startPos = txtarea.selectionStart;
                var endPos = txtarea.selectionEnd;
                var myText = (txtarea.value).substring(startPos, endPos);
                if(!myText) { myText=tag;}
                if(myText.charAt(myText.length - 1) == " "){ // exclude ending space char, if any
                    subst = myText.substring(0, (myText.length - 1))+ " "; 
                } else {
                    subst = myText; 
                }
                txtarea.value = txtarea.value.substring(0, startPos) + subst + txtarea.value.substring(endPos, txtarea.value.length);
                txtarea.focus();
                var cPos=startPos+(myText.length);
                txtarea.selectionStart=cPos;
                txtarea.selectionEnd=cPos;
                // All others
            }else{
                txtarea.value+=tag;
                txtarea.focus();
            }
            if (txtarea.createTextRange) txtarea.caretPos = document.selection.createRange().duplicate();
        
        },
        insert:function(){
            var ta = $("#EX_TA_Camp_Tags").textbox("textbox");
            var value = $("#EX_Camp_Tags").combobox('getValue');
            var text = "{{" + value + "}}";
            _message.insertAtCursor(ta, text);
        },
        Get_Camp_Tags: function () {
            $.post(_s_url, { o: JSON.stringify({ action: 'Get_Camp_Tags' }) }, function (data) {
                if (!$.checkResponse(data)) {
                    data = [];
                }
                $("#EX_Camp_Tags").initSelect({
                    data: data,
                    editable: true,
                    showNullItem: false
                });
            }, "json");
        }
    };

    $(function () {
        _message.Get_Camp_Tags();
    });
</script>