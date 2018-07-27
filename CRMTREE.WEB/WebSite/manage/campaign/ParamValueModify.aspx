<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParamValueModify.aspx.cs" Inherits="manage_campaign_ParamValueModify" %>

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
            overflow:auto;
        }

        .form tr td {
            padding-bottom:5px;
        }

        .form tr td.text {
            width:300px;
            white-space:normal;
        }
    </style>
</head>
<body>
    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'center',border:false">
            <table id="frm_pram" class="form" border="0" cellpadding="3" cellspacing="2" style="margin-top:80px;margin-left:20px;">
            </table>
        </div>
        <div data-options="region:'south',border:false" style="height:38px;text-align:right;overflow:hidden;border-top: 1px solid #B1C242;padding-top:5px;padding-right:10px;">
            <a class="easyui-linkbutton" id="btnSave" data-options="onClick:_param_value.run" style="width:80px;"><%=Resources.CRMTREEResource.btnRun%></a>
            <a class="easyui-linkbutton" id="btnSave" data-options="iconCls:'icon-ok',onClick:_param_value.save" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.btnUpdate%></a>
            <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_param_value.close" style="width:80px;margin-left:10px;"><%=Resources.CRMTREEResource.ap_buttons_close%></a>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Campaign/CampaignManager.aspx';
    var _s_url_customer = '/handler/Reports/CustomerManage.aspx';
    var _s_url_app = '/handler/Reports/AppointmentManager.aspx';
    var _params = $.getParams();
    _params.PV_CG_Code = (_params.PV_CG_Code > 0 ? _params.PV_CG_Code : -1);

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

    var _param_value = {
        data: [],
        ctrols: {
            //textbox: [],
            //datebox: [],
            //numberbox: [],
            combobox: []
        },
        //验证
        validate: function () {
            var bValid = $.form.validate(_param_value.ctrols);
            return bValid;
        },
        //取消验证
        disableValidation: function () {
            $.form.disableValidation(_param_value.ctrols);
        },
        run: function () {
            _param_value.save(true);
        },
        save: function (bRun) {
            var bValid = _param_value.validate();
            if (!bValid) { return; }

            _param_value.get();
            if (_param_value.data && _param_value.data.length > 0) {
                $.mask.show();
                $("#btnSave").linkbutton('disable');
                $.post(_s_url, {
                    o: JSON.stringify({
                        action: 'Save_Param_Value_Report',
                        param_value: _param_value.data,
                        PV_Type: (bRun ? 10 : 0),
                        PL_RP_Code: _params.RP_Code
                    })
                }, function (res) {
                    $.mask.hide();
                    $("#btnSave").linkbutton('enable');

                    if (bRun) {
                        _param_value.close();
                        window.top[_params._winID].RunTemp(_params.RP_Code);
                        return;
                    }

                    if ($.checkResponse(res)) {
                        _param_value.close(true);
                    } else {
                        $.msgTips.save(false);
                    }
                }, "json");
            } else {
                _param_value.close();
            }
            
        },
        close: function (bSave) {
            if (window._closeOwnerWindow) {
                window._closeOwnerWindow();
                if (bSave) {
                    window.top[_params._winID].JavascriptPagination(1);
                }
            } else {
                window.close();
            }
        },
        get: function () {
            var d = $.form.getData("#frm_pram");
            var char = _isEn ? " " : "";
            $.each(_param_value.data, function (i, o) {
                /*
                10 : List of Car Makes
                11 : List of Car Make & Models
                12 : List of Car Make, Models, and Style 
                13 : List of Car Types
                14 : List of Car Types & Models
                */
                o.PV_Val_Text = "";
                switch (o.PL_Type) {
                    case 10:
                        o.PV_Val = d["_" + o.PV_PL_Code + "_1"];
                        o.PV_Val_Text = $("#_" + o.PV_PL_Code + "_1").combobox('getText');
                        break;
                    case 11:
                        o.PV_Val = d["_" + o.PV_PL_Code + "_2"];
                        o.PV_Val_Text += char + $("#_" + o.PV_PL_Code + "_1").combobox('getText');
                        o.PV_Val_Text += char + $("#_" + o.PV_PL_Code + "_2").combobox('getText');
                        break;
                    case 12:
                        o.PV_Val = d["_" + o.PV_PL_Code + "_3"];
                        o.PV_Val_Text = $("#_" + o.PV_PL_Code + "_1").combobox('getText');
                        o.PV_Val_Text += char + $("#_" + o.PV_PL_Code + "_2").combobox('getText');
                        o.PV_Val_Text += char + $("#_" + o.PV_PL_Code + "_3").combobox('getText');
                        break;
                        //case 13:

                        //    break;
                        //case 14:

                        //    break;
                    default:
                        o.PV_Val = d["_" + o.PV_PL_Code];
                        break;
                }
            });
        }
    };

    /*
    PV_Type:
        0 : Report
        1 : Campaign
        2 : Events
    */

    /*
    PL_Type:
        1 : Integer, Input field
        2 : Currentcy, Input field
        3 : Date, Calendar type (Any Date)
        4 : Date, Calendar type  Start Date (Today and After)
        5 : Date, Calendar type End Date (After Start Date)
        6 : Date, Calendar type  Start Date (Any date)
        7 : Date, Calendar type End Date (An date afte Start Date)
        10 : List of Car Makes
        11 : List of Car Make & Models
        12 : List of Car Make, Models, and Style 
        13 : List of Car Types
        14 : List of Car Types & Models
    */
    _param_value.ajax = function (data, url) {
        if (!url) { url = _s_url_customer }
        var a;
        $.ajax({
            type: 'post',
            url: url,
            data: {
                o: JSON.stringify(data)
            },
            dataType: "json",
            async: false
            , success: function (data) {
                a = data;
            }
        });
        return a;
    }
    _param_value.getComboboxData = function (data,url) {
        var a = _param_value.ajax(data,url);
        return a ? a : [];
    }

    _param_value.getDealerAdvisor = function () {
        return _param_value.getComboboxData({ action: 'Get_Dealer_Advisor_All' }, _s_url_app);
    };

    _param_value.getPeriodicals = function () {
        return _param_value.getComboboxData({ action: 'Get_Periodicals' }, _s_url_customer);
    };

    _param_value.getYears = function () {
        return _param_value.getComboboxData({ action: 'Get_Years' }, _s_url_customer);
    };

    _param_value.getMake = function(){
        return _param_value.getComboboxData({ action: 'Get_Make' }, _s_url_customer);
    };

    _param_value.getModel = function (id) {
        //if (!(id > 0)) { return []; }
        //return _param_value.getComboboxData({ action: 'Get_Car_Model', id: id, id_year: yr_code }, _s_url_customer);
        if (!(id > 0)) { return []; }
        return _param_value.getComboboxData({ action: 'Get_Car_Model_All', id: id }, _s_url_customer);
    };

    _param_value.getStyle = function (id) {
        //if (!(id > 0)) { return []; }
        //return _param_value.getComboboxData({ action: 'Get_Car_Style', id: id, id_year: yr_code }, _s_url_customer);
        if (!(id > 0)) { return []; }
        return _param_value.getComboboxData({ action: 'Get_Car_Style_All', id: id }, _s_url_customer);
    };

    _param_value.bindMake = function(d){
        var make = _param_value.getMake();
        $("#_" + d.PV_PL_Code + "_1").initSelect({
            editable: true,
            data: make
        });

        if (d.PV_Val > 0) {
            $("#_" + d.PV_PL_Code + "_1").combobox('setValue', d.PV_Val);
        }
    }
    _param_value.bindDealerAdvisor = function (d) {
        var data = _param_value.getDealerAdvisor();
        $("#_" + d.PV_PL_Code).initSelect({
            editable: true,
            data: data
        });

        if (d.PV_Val > 0) {
            $("#_" + d.PV_PL_Code).combobox('setValue', d.PV_Val);
        }
    }
    _param_value.bindPeriodicals = function (d) {
        var data = _param_value.getPeriodicals();
        $("#_" + d.PV_PL_Code).initSelect({
            editable: true,
            data: data
        });

        if (d.PV_Val > 0) {
            $("#_" + d.PV_PL_Code).combobox('setValue', d.PV_Val);
        }
    }
    _param_value.bindYears = function (d) {
        var years = _param_value.getYears();
        $("#_" + d.PV_PL_Code + "_0").initSelect({
            editable: true,
            showNullItem: false,
            data: years,
            onChange: function (nValue, oValue) {
                if (d.PL_Type == 11) {
                    var mk_code = $("#_" + d.PV_PL_Code + "_1").combobox('getValue');
                    var model = _param_value.getModel(mk_code, nValue);
                    $("#_" + d.PV_PL_Code + "_2").combobox('clear').initSelect({
                        editable: true,
                        data: model,
                        showNullItem: false
                    });
                }
                if (d.PL_Type == 12) {
                    var mk_code = $("#_" + d.PV_PL_Code + "_1").combobox('getValue');
                    var model = _param_value.getModel(mk_code, nValue);
                    $("#_" + d.PV_PL_Code + "_3").combobox('clear').combobox('loadData', []);
                    $("#_" + d.PV_PL_Code + "_2").combobox('clear').initSelect({
                        editable: true,
                        data: model,
                        showNullItem: false,
                        onChange: function (nModel, oModel) {
                            var yr_code = $("#_" + d.PV_PL_Code + "_0").combobox('getValue');
                            var style = _param_value.getStyle(nModel, yr_code);
                            $("#_" + d.PV_PL_Code + "_3").combobox('clear').initSelect({
                                editable: true,
                                showNullItem: false,
                                data: style
                            });
                        }
                    });
                }
            }
        });
    }
    _param_value.bindModel = function (d) {
        var o = {};
        if (d.PV_Val > 0) {
            o = _param_value.ajax({ action: 'Get_Recursion_By_CM_Code', id: d.PV_Val },_s_url_customer);
        }

        var bModel = true;
        var make = _param_value.getMake();
        $("#_" + d.PV_PL_Code + "_1").initSelect({
            editable: true,
            data: make,
            onChange: function (nv, ov) {
                //var yr_code = $("#_" + d.PV_PL_Code + "_0").combobox('getValue');
                var model = _param_value.getModel(nv);
                $("#_" + d.PV_PL_Code + "_2").combobox('clear').initSelect({
                    editable: true,
                    data: model
                });

                if (o && bModel && o.CM_Code > 0) {
                    $("#_" + d.PV_PL_Code + "_2").combobox('setValue', o.CM_Code);
                    bModel = false;
                }
            }
        });

        //if (o && o.YR_Code > 0) {
        //    $("#_" + d.PV_PL_Code + "_0").combobox('setValue', o.YR_Code);
        //}
        if (o && o.MK_Code > 0) {
            $("#_" + d.PV_PL_Code + "_1").combobox('setValue', o.MK_Code);
        }
    }
    _param_value.bindStyle = function (d) {
        var o = {};
        if (d.PV_Val > 0) {
            o = _param_value.ajax({ action: 'Get_Recursion_By_CS_Code', id: d.PV_Val },_s_url_customer);
        }

        var bModel = true;
        var bStyle = true;
        var make = _param_value.getMake();
        $("#_" + d.PV_PL_Code + "_1").initSelect({
            editable: true,
            data: make,
            onChange: function (nMake, oMake) {
                //var yr_code = $("#_" + d.PV_PL_Code + "_0").combobox('getValue');
                var model = _param_value.getModel(nMake);
                $("#_" + d.PV_PL_Code + "_3").combobox('clear').combobox('loadData',[]);
                $("#_" + d.PV_PL_Code + "_2").combobox('clear').initSelect({
                    editable: true,
                    data: model,
                    onChange: function (nModel, oModel) {
                        //var yr_code = $("#_" + d.PV_PL_Code + "_0").combobox('getValue');
                        var style = _param_value.getStyle(nModel);
                        $("#_" + d.PV_PL_Code + "_3").combobox('clear').initSelect({
                            editable: true,
                            data: style
                        });

                        if (o && bStyle && o.CS_Code > 0) {
                            $("#_" + d.PV_PL_Code + "_3").combobox('setValue', o.CS_Code);
                            bStyle = false;
                        }
                    }
                });

                if (o && bModel && o.CM_Code > 0) {
                    $("#_" + d.PV_PL_Code + "_2").combobox('setValue', o.CM_Code);
                    bModel = false;
                }
            }
        });

        //if (o && o.YR_Code > 0) {
        //    $("#_" + d.PV_PL_Code + "_0").combobox('setValue', o.YR_Code);
        //}
        if (o && o.MK_Code > 0) {
            $("#_" + d.PV_PL_Code + "_1").combobox('setValue', o.MK_Code);
        }
    }

    _param_value.create = function () {
        var a = [];
        var now = '<%=DateTime.Now.ToString(Interna ? "MM/dd/yyyy":"yyyy-MM-dd") %>';
        $.each(_param_value.data, function (i, d) {
            var c = '';
            var v = d.PV_Val;
            var id = '_' + d.PV_PL_Code;
            switch (d.PL_Type) {
                case 1:
                    c = '<input id="' + id + '" class="easyui-numberbox" value="' + v + '"/>';
                    break;
                case 2:
                    c = '<input id="' + id + '" class="easyui-numberbox" precision = "2" value="' + v + '"/>';
                    break;
                case 3:
                    c = '<input id="' + id + '" class="easyui-datebox" value="' + v + '"/>';
                    break;
                case 4:
                    c = '<input id="' + id + '" class="easyui-datebox" value="' + v + '"/>';
                    break;
                case 5:
                    c = '<input id="' + id + '" class="easyui-datebox" value="' + v + '"/>';
                    break;
                case 6:
                    c = '<input id="' + id + '" class="easyui-datebox" value="' + v + '"/>';// data-options="required:true,validType:\'gtOrEqualDate[\\\'' + now + '\\\']\'"
                    break;
                case 7:
                    c = '<input id="' + id + '" class="easyui-datebox" value="' + v + '"/>';
                    break;
                    /*
                        10 : List of Car Makes
                        11 : List of Car Make & Models
                        12 : List of Car Make, Models, and Style 
                        13 : List of Car Types
                        14 : Dealer Advisor --List of Car Types & Models
                    */
                case 10:
                    c = '<input id="' + id + '_1" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/>';
                    _param_value.ctrols.combobox = [id + '_1'];
                    break;
                case 11:
                    c = //'<input id="' + id + '_0" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/>'
                        //+
                        '<input id="' + id + '_1" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/>'
                        +
                        '<div style="margin-top:5px;"><input id="' + id + '_2" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/></div>'
                    _param_value.ctrols.combobox = [id + '_1', id + '_2'];
                    break;
                case 12:
                    c = //'<input id="' + id + '_0" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/>'
                        //+
                        '<input id="' + id + '_1" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/>'
                        +
                        '<div style="margin-top:5px;"><input id="' + id + '_2" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/></div>'
                        +
                        '<div style="margin-top:5px;"><input id="' + id + '_3" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/></div>'
                    _param_value.ctrols.combobox = [id + '_1', id + '_2', id + '_3'];
                    break;
                    //case 13:
                    //    c = '<input id="' + id + '" class="easyui-combobox"/>';
                    //    break;
                    case 14:
                        c = '<input id="' + id + '" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/>';
                        _param_value.ctrols.combobox = [id];
                        break;
                    case 20:
                        c = '<input id="' + id + '" class="easyui-combobox" data-options="editable: true,required:true,novalidate:true"/>';
                        _param_value.ctrols.combobox = [id];
                        break;
                default:
                    c = '';
                    break;
            }

            var style = (d.PL_Type == 11 || d.PL_Type == 12 || d.PL_Type == 14) ? 'style="vertical-align:top;padding-top:8px;"' : '';

            a.push(
            [
                '<tr>'
                    , '<td class="text" ' + style + '>'
                    , d.PL_Prompt
                    , '<td>'
                    , c
                    , '</td>'
                , '</tr>'
            ].join('')
            );
        });

        $("#frm_pram").append(a.join(''));

        $.parser.parse("#frm_pram");

        /*
            10 : List of Car Makes
            11 : List of Car Make & Models
            12 : List of Car Make, Models, and Style 
            13 : List of Car Types
            14 : List of Car Types & Models
        */
        $.each(_param_value.data, function (i, d) {
            switch (d.PL_Type) {
                case 10:
                    _param_value.bindMake(d);
                    break;
                case 11:
                    //_param_value.bindYears(d);
                    _param_value.bindModel(d);
                    break;
                case 12:
                    //_param_value.bindYears(d);
                    _param_value.bindStyle(d);
                    break;
                    //case 13:

                    //    break;
                case 14:
                    _param_value.bindDealerAdvisor(d);
                    break;
                case 20:
                    _param_value.bindPeriodicals(d);
                    break;
                default:
                    c = '';
                    break;
            }
        });

        //$("#frm_pram").form('validate');

        //$('#dd').datebox().datebox('calendar').calendar({
        //    validator: function (date) {
        //        var now = new Date();
        //        var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
        //        var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 10);
        //        return d1 <= date && date <= d2;
        //    }
        //});
    }

    $(function () {
        $.post(_s_url, {
            o: JSON.stringify({
                action: 'Get_ReportValue',
                RP_Code: _params.RP_Code,
                PV_Type: 0,
                PV_CG_Code: _params.PV_CG_Code > 0 ? _params.PV_CG_Code : -1
            })
        }, function (data) {
            _param_value.data = $.map(data, function (d) {
                var val = $.trim(d.PV_Val);
                return d.PL_Code > 0 ? {
                    PV_PL_Code: d.PL_Code
                    , PV_Type: 1
                    , PL_Tag: d.PL_Tag
                    , PL_Prompt: $.trim(_isEn ? d.PL_Prompt_En : d.PL_Prompt_Cn)
                    , PV_CG_Code: d.PV_CG_Code ? d.PV_CG_Code : 0
                    , PL_Type: d.PL_Type
                    , PV_Val: val != "" ? val : $.trim(d.PL_Default)
                } : null;
            });

            _param_value.create();
        }, "json");
    
    });
</script>