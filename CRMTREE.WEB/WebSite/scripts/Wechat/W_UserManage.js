//页面的相关设置
var Page = function () {
    Page.Initialization();
    //var a=i.a;
};
Page.Initialization = function () {
    User();
};
Page.Ajax = function (url, data, async) {
    var o;
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8",
        url: url, data: data, async: async,
        success: function (data) {
            if (data.d != null) { o = data.d; }
            else { return false; }
        }
        //error: function (err, err2, err3) {
        //    alert(err + "," + err2 + " ," + err3);
        //    return false;
        //}
    });
    return o;
}
var User = function () {
    User.userGroups();

};
User.userGroups = function () {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8",
        url: "UserManage.aspx/GetAllFan", data: null, async: true,
        success: function (data) {
            if (data.d != null) {
                var o = data.d.Fans;
                for (i = 0; i < o.length; i++) {
                    var tr_userRow = User.userGroups.userRow(o[i]);
                    $("#userGroups").append(tr_userRow);
                }
            }
        }
    });
};
User.userGroups.userRow = function (user) {
    var td_user = User.userGroups.userRow.user(user);
    var td_user_category = User.userGroups.userRow.user_category();
    var td_btn_Remark = User.userGroups.userRow.user_opr();
    var tr = "<tr>" + td_user + td_user_category + td_btn_Remark + "</tr>";
    return tr;
};

User.userGroups.userRow.user = function (user) {
    var o = User.userGroups.userRow.user.user_info(user);
    var td = "<td class=\"table_cell user\">" + o + "</td>";
    return td;
};
User.userGroups.userRow.user.user_info = function (user) {
    var test = "href=\"SendMessage.aspx?n=" + user.WF_NickName_EC + "&o=" + user.WF_OpenId + "\"";
    var user = "<div class=\"user_info\"> <a class=\"remark_name\" href=\"SendMessage.aspx?n=" + user.WF_NickName_EC + "&o=" + user.WF_OpenId + "\" target=\"_blank\" data-fakeid=\"1472492616\">" + unescape(user.WF_NickName) + "</a> <span class=\"nick_name\" data-fakeid=\"1472492616\"></span> <a class=\"avatar\" href=\"SendMessage.aspx?n=" + user.WF_NickName_EC + "&o=" + user.WF_OpenId + "\" target=\"_blank\"> <img class=\"js_msgSenderAvatar\" src=\"" + user.WF_HeadImgurl + "\" data-fakeid=\"1472492616\" /> </a> <label class=\"frm_checkbox_label\" for=\"check1472492616\"> <i class=\"icon_checkbox\"></i> <input class=\"frm_checkbox js_select\" id=\"check1472492616\" type=\"checkbox\" value=\"1472492616\"></label> </div>";
    return user;

}

User.userGroups.userRow.user_category = function () {
    var o = User.userGroups.userRow.user_category.dropdown_menu();
    var td = "<td class=\"table_cell user_category\">" + o + "</td>";
    return td;
};
User.userGroups.userRow.user_category.dropdown_menu = function () {
    var menu = "<div class=\"js_selectArea dropdown_menu\" id=\"selectArea1472492616\" data-fid=\"1472492616\" data-gid=\"0\"> <a class=\"btn dropdown_switch jsDropdownBt\" href=\"javascript:;\"> <label class=\"jsBtLabel\">未分组</label><i class=\"arrow\"></i></a> <div class=\"dropdown_data_container jsDropdownList\" style=\"display: none;\"> <ul class=\"dropdown_data_list\"> <li class=\"dropdown_data_item \"> <a class=\"jsDropdownItem\" onclick=\"return false;\" href=\"javascript:;\" data-name=\"未分组\" data-index=\"0\" data-value=\"0\">未分组</a> </li> <li class=\"dropdown_data_item \"> <a class=\"jsDropdownItem\" onclick=\"return false;\" href=\"javascript:;\" data-name=\"黑名单\" data-index=\"1\" data-value=\"1\">黑名单</a> </li> <li class=\"dropdown_data_item \"> <a class=\"jsDropdownItem\" onclick=\"return false;\" href=\"javascript:;\" data-name=\"星标组\" data-index=\"2\" data-value=\"2\">星标组</a> </li> </ul> </div> </div>";
    return menu;
};

User.userGroups.userRow.user_opr = function () {
    var o = User.userGroups.userRow.user_opr.btn_remark();
    var td = "<td class=\"table_cell user_opr\">" + o + "</td>";
    return td;
};
User.userGroups.userRow.user_opr.btn_remark = function () {
    var remark = "<a class=\"btn remark js_msgSenderRemark\" data-fakeid=\"1472492616\">修改备注</a>";
    return remark;
};

//(function (e) {
//    if (0)
//        alert(0);
//    if (1)
//        alert(1);
//})(this);;
