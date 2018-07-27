//页面的相关设置
var Page = function () {
    Page.Initialization();
    //var a=i.a;
};
Page.Initialization = function () {
    
     Message();
    
};

var Message = function () {
    Message.list();
};
Message.list = function () {
   
    var OpenId = getUrlParam("o");
    data = { context: "", OpenId: OpenId };
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8",
        url: "SendMessage.aspx/GetAllFanContent", data: "{data:'" + unescape((JSON.stringify(data))) + "'}", async: true,
        success: function (data) {
            if (data.d != null) {
                var o = data.d.Fans;
                for (i = 0; i < o.length; i++) {
                    var li = Message.list.item(o[i]);
                    $(".message_list").append(li);
                }
            }
        }
    });
};
Message.list.item = function (o) {
    var date = (new Date).getTime();
    var opr = Message.list.item.message_opr(date);
    var info = Message.list.item.message_info(date,o);
    var content = Message.list.item.message_content(date, o.WT_LogContent);
    var Box = Message.list.item.quickReplyBox(date);
    var li = " <li class=\"message_item \" id=\"msgListItem" + date + "\" data-id=\"" + date + "\">" + opr + info + content + Box + "</li>";
    return li;
    
};
Message.list.item.message_opr = function (date) {
    var opr = "<div class=\"message_opr\"> <a href=\"javascript:;\" class=\"js_star icon18_common star_gray\" action=\"\" idx=\"" + date + "\" starred=\"\" title=\"收藏消息\">取消收藏</a> <a href=\"javascript:;\" data-id=\"" + date + "\" data-tofakeid=\"1472492616\" class=\"icon18_common reply_gray js_reply\" title=\"快捷回复\">快捷回复</a> </div>";
    return opr;
};
Message.list.item.message_info = function (date,o) {
    var info = "<div class=\"message_info\"><div class=\"message_status\"><em class=\"tips\">已回复</em></div> <div class=\"message_time\">" + o.WT_CreateTime_S + "</div>\ <div class=\"user_info\"> <a href=\"SendMessage.aspx?n=" + o.WF_NickName_EC + "&o=" + o.WF_OpenId + "\" target=\"_blank\" data-fakeid=\"1472492616\" class=\"remark_name\">" + unescape(o.WF_NickName) + "</a><span class=\"nickname\" data-fakeid=\"1472492616\"></span> <a href=\"javascript:;\" class=\"icon14_common edit_gray js_changeRemark\" data-fakeid=\"1472492616\" title=\"修改备注\" style=\"display: none;\"></a> <a target=\"_blank\" href=\"SendMessage.aspx?n=" + o.WF_NickName_EC + "&o=" + o.WF_OpenId + "\" class=\"avatar\" data-fakeid=\"1472492616\"><img src=\"" + o.WF_HeadImgurl + "\" data-fakeid=\"1472492616\"> </a> </div> </div>";
    return info;
};
Message.list.item.message_content = function (date, content) {
    var content = "<div class=\"message_content text\"> <div id=\"wxMsg" + date + "\" data-id=\"" + date + "\" class=\"wxMsg\">" + content + "</div> </div>";
    return content;
};
Message.list.item.quickReplyBox = function (date) {
    var quick = "<div id=\"quickReplyBox" + date + "\" class=\"js_quick_reply_box quick_reply_box\"> <label for=\"\" class=\"frm_label\">快速回复:</label> <div class=\"emoion_editor_wrp js_editor\"></div> <div class=\"verifyCode\"></div> <p class=\"quick_reply_box_tool_bar\"> <span class=\"btn btn_primary btn_input\" data-id=\"" + date + "\"> <button class=\"js_reply_OK\" data-id=\"" + date + "\" data-fakeid=\"1472492616\">发送</button> </span><a class=\"js_reply_pickup btn btn_default pickup\" data-id=\"" + date + "\" href=\"javascript:;\">收起</a> </p> </div>";
    return quick;
};