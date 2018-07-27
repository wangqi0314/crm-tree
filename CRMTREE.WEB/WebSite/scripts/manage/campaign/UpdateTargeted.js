$(document).ready(function () {
    InitializationPage();    
    $("#txt_Date").live("focus", function () {
        WdatePicker({ lang: 'en', dateFmt: 'MM/dd/yyyy' });
    });
    $("#txt_Start_dt").live("focus", function () {
        WdatePicker({ lang: 'en', dateFmt: 'MM/dd/yyyy', minDate: '%y-%M-%d' });
    });
    $("#txt_End_dt").live("focus", function () {
        WdatePicker({ lang: 'en', dateFmt: 'MM/dd/yyyy', minDate: '#F{$dp.$D(\'txt_Start_dt\')}' });
    });
    $("#txt_any_Start_dt").live("focus", function () {
        WdatePicker({ lang: 'en', dateFmt: 'MM/dd/yyyy' });
    });
    $("#txt_any_End_dt").live("focus", function () {
        WdatePicker({ lang: 'en', dateFmt: 'MM/dd/yyyy', minDate: '#F{$dp.$D(\'txt_any_Start_dt\')}' });
    });
    $("input").live("blur", function () {
        $(this).attr("value", $(this).val());
    });
    $("#Makes_List").live("change", function () {
        //alert($("#Makes_List").find("option:selected").text());
        if ($(".hidMakes").val() != null) {
            //在下拉列表的值变化时，首先为Make的隐藏域赋值； 第一步
            $(".hidMakes").val($("#Makes_List").val());
        }
        else if ($(".hidModes").val() != null) {
            //在第一步完成时，加载Mode下拉类表；             第二步
            ModeList(1, $("#Makes_List").val(), null);
            $(".hidModes").val($("#Modes_List").val());
        } else if ($(".hidStyle").val() != null) {
            //在第一步完成时，加载Mode下拉类表；             第二步
            ModeList(2, $("#Makes_List").val(), null);
            //StyleList($("#Modes_List").val(), null);
            $(".hidStyle").val($("#Style_List").val());
        }  
    });
    $("#Modes_List").live("change", function () {
        if ($(".hidModes").val() != null) {
            $(".hidModes").val($("#Modes_List").val());  //获取Make选择的值
        } else if ($(".hidStyle").val() != null) {
            //在第一步完成时，加载Mode下拉类表；             第二步
            StyleList($("#Modes_List").val(), null);
            $(".hidStyle").val($("#Style_List").val());
        }
    });
    $("#Style_List").live("change", function () {
        $(".hidStyle").val($("#Style_List").val());
    });
});
function InitializationPage() {
    var Count = top.$("#hidUpdateTargeted_Count").val();
    var rp_Code = GetQueryString("RP_Code");
    var CG_Code = GetQueryString("CG_Code");
    if (CG_Code == "null" || CG_Code == "") {
        CG_Code = 0;
    }
    if (rp_Code != null && rp_Code != "" && Count == "0") {
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "UpdateTargeted.aspx/getParamterslist",
            data: "{RP_Code:'" + rp_Code + " ',CG_Code:'" + CG_Code + "'}",
            success: function (data) {
                if (data.d != null) {
                    var Report = data.d.CT_Paramters_list
                    for (var i = 0; i < Report.length; i++) {
                        if (Report[i].PL_Type == 1) {
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /> <div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><input type='text' class='Integer PL_Val' value='" + Report[i].PL_Default + "' /></div></div>");
                        }
                        else if (Report[i].PL_Type == 2) {
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /><div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><input type='text' class='text PL_Val' value='" + Report[i].PL_Default + "' /></div></div>");
                        }
                        else if (Report[i].PL_Type == 3) {
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /><div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><input type='text' id='txt_Date' value='" + Report[i].PL_Default + "'   class='Wdate Date PL_Val' /></div></div>");
                        }
                        else if (Report[i].PL_Type == 4) {
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /><div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><input type='text' id='txt_Start_dt' value='" + Report[i].PL_Default + "'   class='Wdate Date PL_Val' /></div></div>");
                        }
                        else if (Report[i].PL_Type == 5) {
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /><div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><input type='text' id='txt_End_dt' value='" + Report[i].PL_Default + "'   class='Wdate Date PL_Val' /></div></div>");
                        }
                        else if (Report[i].PL_Type == 6) {
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /><div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><input type='text' id='txt_any_Start_dt' value='" + Report[i].PL_Default + "'   class='Wdate Date PL_Val' /></div></div>");
                        }
                        else if (Report[i].PL_Type == 7) {
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /><div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><input type='text' id='txt_any_End_dt' value='" + Report[i].PL_Default + "'   class='Wdate Date PL_Val' /></div></div>");
                        }
                        else if (Report[i].PL_Type == 10) {
                            //PL_Type=10;是根据需要绑定下拉列表Make处理的，每一次的赋值变化和初始化的值都需要存储在隐藏域hidMakes中
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /><input type='hidden' class='hidMakes PL_Val' value='" + Report[i].PL_Default + "' /><div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><select id='Makes_List' class='selects'></select></div></div>");
                            MakeList(0, Report[i].PL_Default);
                        }
                        else if (Report[i].PL_Type == 11) {
                            top.$("#open").width(730);
                            //PL_Type=11;是根据需要绑定下拉列表Model处理的，每一次的赋值变化和初始化的值都需要存储在隐藏域hidModes中
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /><input type='hidden' class='hidModes PL_Val' value='" + Report[i].PL_Default + "' /><div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><select id='Makes_List' class='selects'></select><select id='Modes_List' class='selects' ></select></div></div>");
                            MakeList(1, Report[i].PL_Default);
                        } else if (Report[i].PL_Type == 12) {
                            top.$("#open").width(900);
                            //PL_Type=12;是根据需要绑定下拉列表Style处理的，每一次的赋值变化和初始化的值都需要存储在隐藏域hidStyle中
                            $(".Paramterslist").append("<div class='Items'><input type='hidden' class='PL_Code' value='" + Report[i].PL_Code + "' /><input type='hidden' class='hidStyle PL_Val' value='" + Report[i].PL_Default + "' /><div class='Items_Title'><span>" + Report[i].PL_Prompt_En + "</span></div><div class='InfoRight'><select id='Makes_List' class='selects'></select><select id='Modes_List' class='selects' ></select><select id='Style_List' class='selects' ></select></div></div>");
                            MakeList(2, Report[i].PL_Default);
                        }
                    }

                }
            }
        });
    }
    else {
        $(".Paramterslist").append(top.$(".hidUpdateTargeted").html());
    }
}
//获取MakeList，用来初始化下拉列表
function MakeList(i,value) {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8", url: "UpdateTargeted.aspx/getMyCarMakeList",
        data: null, async: false,
        success: function (data) {
            if (data.d != null) {
                var MakeList = MakeList_Binding(data.d);
                //页面加载时，首先为Make下拉列表赋值
                $("#Makes_List").append(MakeList);
                ModeList($("#Makes_List").val());
                if (i == 0) {
                    //i=0 代表者页面上只有一个Make列表，加载完列表后，有两种能发生；
                    //一：页面处于创建状态，这个没有做选择，但是对应的隐藏域中存储的是默认的Make值；
                    //二：页面被选择，获取对应隐藏域的值，加载Make的选择值；
                    CT_Makes(0, value);
                    $(".hidMakes").val($("#Makes_List").val()); //默认值赋值完毕，为隐藏域再赋值
                } else if (i == 1) {
                    //i=1 代表页面上有两个下拉列表Make和Model；
                    //这里需要做的事情；
                    //加载Mode列表
                    CT_Makes(1, value);
                } else if (i == 2) {
                    CT_Makes(2, value);
                }
                $(".hidModes").val($("#Modes_List").val());  //获取Make选择的值
            }
        }
    });
}
//MakeList的子方法，用来组合数据
function MakeList_Binding(data) {
    if (data == null || data.Car_Make_List == null) return;
    var MakeList = data.Car_Make_List;
    var Makes = "";
    for (var i = 0; i < MakeList.length; i++) {
        Makes += "<option value='" + MakeList[i].MK_Code + "'>" + MakeList[i].MK_Make_EN + "</option>";
    }
    return Makes;
}
//获取Make数据库存储的默认值
function CT_Makes(i, CM_Code) {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8", url: "UpdateTargeted.aspx/getCarMake",
        data: "{i:" + i + ",CM_Code:" + CM_Code + "}", async: false,
        success: function (data) {
            if (data.d != null) {
                var Make = data.d;
                $(".hidMakes").val(Make.MK_Code);
                var option = "<option value='" + Make.MK_Code + "'>" + Make.MK_Make_EN + "</option>";
                $("#Makes_List option[value='" + Make.MK_Code + "']").remove();
                $("#Makes_List").append(option);
                $("#Makes_List").prop("value", Make.MK_Code);
                if (i != 0) {
                    ModeList(i, $("#Makes_List").val(), CM_Code);
                }

            }
        }
    });
}
//获取ModeList，用来初始化下拉列表
function ModeList(i, MK_Code, value) {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8", url: "UpdateTargeted.aspx/getMyCarModeList",
        data: "{MK_Code:" + MK_Code + "}", async: false,
        success: function (data) {
            if (data.d != null) {
                var ModeList = ModeList_Binding(data.d);
                $("#Modes_List").empty()
                $("#Modes_List").append(ModeList);
                if (i == 1) {
                    CT_Mode(0, value);
                } else if (i == 2) {
                    if (value == null) { StyleList($("#Modes_List").val(), null) }
                    else {
                        CT_Mode(1, value);
                    }
                }
            }
        }
    });
}
//ModeList子方法，用来组合数据
function ModeList_Binding(data) {
    if (data == null || data.Car_Model_List == null) return;
    var ModeList = data.Car_Model_List;
    var Modes = "";
    for (var i = 0; i < ModeList.length; i++) {
        Modes += "<option value='" + ModeList[i].CM_Code + "'>" + ModeList[i].CM_Model_EN + "</option>";
    }
    return Modes;
}
//获取Mode数据库存储的默认值
function CT_Mode(i, CM_Code) {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8", url: "UpdateTargeted.aspx/getMyCarMode",
        data: "{i:" + i + ",CM_Code:" + CM_Code + "}", async: false,
        success: function (data) {
            if (data.d != null) {
                var Mode = data.d;
                $(".hidModes").val(Mode.CM_Code);
                var option = "<option value='" + Mode.CM_Code + "'>" + Mode.CM_Model_EN + "</option>";
                $("#Modes_List option[value='" + Mode.CM_Code + "']").remove();
                $("#Modes_List").append(option);
                $("#Modes_List").prop("value", Mode.CM_Code);
                if (i == 0 && CM_Code == null) {
                    $(".hidModes").val($("#Modes_List").val());
                }
                if (i == 1) {
                    StyleList($("#Modes_List").val(), CM_Code);
                }
            }
        }
    });
}
//获取StyleList，用来初始化下拉列表
function StyleList(CM_Code, value) {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8", url: "UpdateTargeted.aspx/getMyCarStyleList",
        data: "{CM_Code:" + CM_Code + "}",
        success: function (data) {
            if (data.d != null) {
                var StyleList = StyleList_Binding(data.d);
                $("#Style_List").empty()
                $("#Style_List").append(StyleList);
                CT_Style(value);
                if (value == null) {
                    $(".hidStyle").val($("#Style_List").val());
                }
            }
        }
    });
}
//StyleList 的子方法，用来组合数据
function StyleList_Binding(data) {
    if (data == null || data.Car_Style_List == null) return;
    var StyleList = data.Car_Style_List;
    var Styles = "";
    for (var i = 0; i < StyleList.length; i++) {
        Styles += "<option value='" + StyleList[i].CS_Code + "'>" + StyleList[i].CS_Style_EN + "</option>";
    }
    return Styles;
}
function CT_Style(CS_Code) {
    $.ajax({
        type: "POST", dataType: "json", contentType: "application/json; charset=utf-8", url: "UpdateTargeted.aspx/getMyCarStyle",
        data: "{CS_Code:" + CS_Code + "}", async: false,
        success: function (data) {
            if (data.d != null) {
                var Style = data.d;
                $(".hidStyle").val(Style.CS_Code);
                var option = "<option value='" + Style.CS_Code + "'>" + Style.CS_Style_EN + "</option>";
                $("#Style_List option[value='" + Style.CS_Code + "']").remove();
                $("#Style_List").append(option);
                $("#Style_List").prop("value", Style.CS_Code);
            }
        }
    });
}

//获取URL后面的参数值
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
}