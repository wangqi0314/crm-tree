<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerServiceOperator.aspx.cs" Inherits="manage_customer_CustomerServiceOperator" %>

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
            font-size: 12px;
        }

        .red {
            color: red;
        }

        .tbl {
            width: 100%;
            border-top: 1px solid #ccc;
            border-left: 1px solid #ccc;
        }

            .tbl tr th.th {
                font-weight: normal;
                text-align: right;
                padding-right: 15px;
                font-size: 14px;
                background-color: #E7EADB;
            }

            .tbl tr th.top {
                vertical-align: top;
            }

            .tbl tr .th, .tbl tr .td {
                padding: 6px;
                border-right: 1px solid #ccc;
                border-bottom: 1px solid #ccc;
            }

        .fieldset {
            border: solid 1px #ccc;
            padding: 5px 10px;
            float: left;
            margin-right: 5px;
            display: none;
        }

        .panel-header {
            background-color: #DDE2BE;
        }

        .move {
            position: absolute;
            top: -1000px;
            left: -1000px;
        }

        .hide {
            display: none;
        }
    </style>
</head>
<body>
    <div id="frm_customer_service">
        <table class="tbl" cellpadding="0" cellspacing="0">
            <tr>
                <td class="td" style="width: 240px;">
                    <span id="AU_Name" style="font-weight: bold; font-size: 14px;"></span>
                    <input id="AU_Code" type="hidden" />
                    <input id="HD_Code" type="hidden" />
                    <input id="HD_AU_Code" type="hidden" />
                </td>
                <td class="td" style="width: 150px;">
                    <span id="PL_Number" style="font-weight: bold; font-size: 14px;"></span>
                </td>
                <td class="td">
                    <a class="easyui-linkbutton" id="btnCall" data-options="onClick:_customer_service.call,disabled:true,plain:false" style="width: 80px;"><%= Resources.CRMTREEResource.btnCall %></a>
                    <a class="easyui-linkbutton" id="btnAUInfo" data-options="" style="width: 80px;"><%= Resources.CRMTREEResource.btnAUinfo %></a>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <%= Resources.CRMTREEResource.CustomerResponse %>
                </td>
                <td class="td" colspan="2">
                    <%= Resources.CRMTREEResource.Message %>
                </td>
            </tr>
            <tr>
                <td class="td" style="vertical-align: top;">
                    <span id="HD_Action" style="display: inline-block; width: 190px;"></span>
                </td>
                <td class="td" colspan="2" style="vertical-align: top; padding: 0;">
                    <div id="c_campaign_file" style="height: 370px; width: 740px; overflow: auto; margin: 0; padding-left: 10px;"></div>
                </td>
            </tr>
            <tr>
                <td class="td" style="text-align: left; padding-left: 10px;">
                    <a class="easyui-linkbutton" id="btnAddNotes" data-options="iconCls:'icon-add',onClick:_customer_service.addNotes,disabled:true"><%= Resources.CRMTREEResource.btnAddNotes %></a>
                    <a class="easyui-linkbutton" id="btnAddCallBk" data-options="iconCls:'icon-add',onClick:_customer_service.addCallBK2,disabled:true"><%= Resources.CRMTREEResource.btnCallBk %></a>
                </td>
                <td class="td" colspan="2" style="text-align: left; padding-left: 10px;">
                    <a class="easyui-linkbutton" id="btnDone" data-options="iconCls:'icon-save',onClick:_customer_service.done,disabled:true" style="width: 80px;"><%= Resources.CRMTREEResource.btnDone %></a>
                    <a class="easyui-linkbutton" id="btnSkip" data-options="iconCls:'icon-cancel',onClick:_customer_service.skip,disabled:true" style="width: 80px; margin-left: 10px;"><%= Resources.CRMTREEResource.btnSkip %></a>
                    <a class="easyui-linkbutton" id="btnAddAppointment" data-options="iconCls:'icon-add',onClick:_customer_service.addAppointment,disabled:true" style="margin-left: 10px;"><%=Resources.CRMTREEResource.cd_buttons_addAppointment %></a>
                </td>
            </tr>
        </table>
    </div>
    <div id="c_tips" style="display: none">
        <div id="c_notes">
            <div style="text-align: left; padding-bottom: 5px; font-weight: bold;">
                <span id="c_notes_title"></span>
            </div>
            <input id="DH_Notes" class="easyui-textbox" data-options="multiline:true,width:400,height:80" />
            <div style="text-align: right;">
                <a class="easyui-linkbutton" id="c_btn_done_notes" style="width: 60px; margin-top: 5px;"><%= Resources.CRMTREEResource.btnDone %></a>
                <a class="easyui-linkbutton" id="c_btn_close_notes" style="width: 60px; margin-left: 5px; margin-top: 5px;"><%=Resources.CRMTREEResource.CancelBtn %></a>
            </div>
        </div>

        <div id="c_callback" class="easyui-window" data-options="title:'<%= Resources.CRMTREEResource.btnCallBk %>',minimizable:false,closed:true,modal:true"
            style="width: 230px; height: 130px;">
            <div style="padding: 10px;">
                <div style="text-align: left; padding-bottom: 5px; font-weight: bold;">
                    <span id="c_callback_title"></span>
                </div>
                <input id="RT_Time" class="easyui-datetimebox" showseconds="false" value="" style="width: 150px;" />
                <div style="text-align: right; padding-top: 10px;">
                    <a class="easyui-linkbutton" id="c_btn_done_callback" style="width: 60px; margin-top: 5px;"><%= Resources.CRMTREEResource.btnDone %></a>
                    <a class="easyui-linkbutton" id="c_btn_close_callback" style="width: 60px; margin-left: 5px; margin-top: 5px;"><%=Resources.CRMTREEResource.CancelBtn %></a>
                </div>
            </div>
        </div>
    </div>

    <div id="w_notes" class="easyui-window" data-options="title:'<%= Resources.CRMTREEResource.Notes %>',minimizable:false,closed:true,modal:true"
        style="width: 400px; height: 180px;">
        <div class="easyui-layout" data-options="fit:true">
            <div data-options="region:'center',border:false">
                <input id="EX_DH_Notes" class="easyui-textbox" data-options="multiline:true,width:'100%',height:'100%'" />
            </div>
            <div data-options="region:'south',border:false" style="height: 38px; text-align: right; overflow: hidden; border-top: 1px solid #B1C242; padding-top: 5px; padding-right: 10px;">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',onClick:_customer_service.close_notes" style="width: 80px;"><%=Resources.CRMTREEResource.btnDone%></a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',onClick:_customer_service.ignore_notes" style="width: 80px; margin-left: 10px;"><%=Resources.CRMTREEResource.cm_cars_buttonos_ignore%></a>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    var _s_url = '/handler/Customer/CustomerService.aspx';
    var _s_url_report = '/handler/Reports/Reports.aspx';
    var _params = $.getParams();
    var _Notes = '';

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

    //--------------------------------------------------------------------------------------
    //customer_service（活动）
    //--------------------------------------------------------------------------------------
    var _customer_service = {
        guid: '<%=Guid.NewGuid()%>',
        startTime: null,
        CG_Cat: [],
        PL_Code: [],
        CI_Code: [],
        hd_codes: [],
        queryParams: [],
        check: function () {
            var bCheck = true;
            var data = $.form.getData("#frm_customer_service");

            if (!(data.HD_Action > 0)) {
                $.msgTips.info(_isEn ? 'Please select customer response!' : '请选择客户反馈！');
                bCheck = false;
            }

            if (data.HD_Action == 99 || data.HD_Action == 38 || data.HD_Action == 100 || data.HD_Action == 150) {
                //var notes = $.trim($('#DH_Notes').textbox('getValue'));
                if (_Notes == '') {
                    $("#HD_Action").radiolist('setValue', data.HD_Action);
                    bCheck = false;
                }
            }


            return bCheck;
        },
        get: function () {
            var data = $.form.getData("#frm_customer_service");

            var endTime = new Date();
            var seconds = Math.floor((endTime - _customer_service.startTime) / 1000);

            var params = { action: 'Save_CustomerService' };
            params.data = $.extend({
                HD_CH_Code: _params.CH_Code,
                PL_Code: _customer_service.PL_Code,
                CI_Code: _customer_service.CI_Code,
                RT_Time: seconds,

                //CH_Status: 1,
                DH_legacy: 1, //1:customer service to customer, 0:customer to customer service
                DH_Duration: seconds//skip:-1, done: calc seconds
            }, data);

            if (_Notes != '') {
                params.data.DH_Notes = _Notes;
            }

            params.data.RT_Time = $.trim($('#RT_Time').textbox('getValue'));

            return params;
        },
        //呼叫
        call: function () {
            $("#btnCall").linkbutton('disable');
            $("#btnDone,#btnAddNotes,#btnAddCallBk").linkbutton('enable');
            $("#HD_Action").radiolist('readonly', false);
            _Notes = '';
            $.trim($('#EX_DH_Notes').textbox('setValue', _Notes));
            $.trim($('#DH_Notes').textbox('setValue', _Notes));
            _customer_service.startTime = new Date();
        },
        //完成
        done: function () {
            if (!_customer_service.check()) {
                return;
            }
            var data = $.form.getData("#frm_customer_service");
            if (_customer_service.CG_Cat === 3 && data.HD_Action == 20) {
                var _bCheck = SurveyiFrame.window.Survey.Check2();
                if (_bCheck.length > 0) {
                    $.confirmWindow.survey(_bCheck, function (isTrue) {
                    if (isTrue) {
                        SurveyiFrame.window.Survey.returns(_bCheck);
                    } else {
                        SurveyiFrame.window.Survey.Save();
                        _customer_service.done2();
                    }
                });               
                } else {
                    SurveyiFrame.window.Survey.Save();
                }
            } else {
                _customer_service.done2();
            }
           
        },

        done2: function () {
            var o = _customer_service.get();
            if (!o) {
                return;
            }

            $.mask.show();
            $("#btnDone").linkbutton('disable');
            $.post(_s_url, { o: JSON.stringify(o) }, function (res) {
                $.mask.hide();
                $("#btnDone").linkbutton('enable');
                if ($.checkResponse(res, false)) {
                    $.msgTips.done(true);
                    if (_params._cmd == 'cp') {
                        _customer_service.bindCustomerInfo();
                    } else {
                        if (_params.HD > 0) {
                            _customer_service.close(true);
                        } else {
                            _customer_service.bindCustomerInfo();
                        }
                    }
                } else {
                    $.msgTips.save(false);
                }
            }, "json");
        },
        //跳过
        skip: function () {
            _customer_service.bindDefaults();
            _customer_service.bindCustomerInfo();
        },
        //添加预约
        addAppointment: function () {
            //var au_code = $("#HD_AU_Code").val();
            var au_Name = $("#AU_Name").text();
            var au_code = $("#AU_Code").val();
            window.top.$.topOpen({
                title: '<%= Resources.CRMTREEResource.ap_window_title %>',
                url: "/templete/usercontrol/AppointmentManager.aspx?AU_Name=" + $("#AU_Name").text() + "&AU_Code=" + au_code + "&_winID=<%=Guid.NewGuid()%>",
                width: 720,
                height: 460
            });
        },
        ignore_notes: function () {
            $("#EX_DH_Notes").textbox('clear');
            _Notes = '';
            $("#w_notes").window('close');
        },
        close_notes: function () {
            _Notes = $.trim($('#EX_DH_Notes').textbox('getValue'));
            $.trim($('#DH_Notes').textbox('setValue', _Notes));
            $("#w_notes").window('close');
        },
        //添加备注
        addNotes: function () {
            $.trim($('#EX_DH_Notes').textbox('setValue', _Notes));
            $("#w_notes").window('open');
        },
        addCallBK: function () {
            $("#c_callback").window('open');
        },
        addCallBK2: function () {
            $("#btnAddCallBk").tooltip({
                content: function () {
                    var now = new Date() + 15 * 60000; //add 15 minutes to now!
                    //var nTime = now.getFullYear + '-' + now.getMonth + '-' + now.getDate + ' ' + now.getHours() + ':' + now.getMinutes();
                    //$("#RT_Time").val(nTime);
                    return $('#c_callback');
                },
                position: 'top',
                showEvent: 'click',
                hideEvent: 'none',
                onShow: function () {
                    $tooltip = $("#btnAddCallBk");
                    tipsShow = true;
                    $("#c_btn_done_callback").unbind("click").bind("click", function () {
                        $("#c_tips").append($('#c_callback'));
                        $tooltip.tooltip('destroy');
                        tipsShow = false;
                    });

                    $("#c_btn_close_callback").unbind("click").bind("click", function () {
                        $("#c_tips").append($('#c_callback'));
                        $tooltip.tooltip('destroy');
                        tipsShow = false;
                    });
                },
                onHide: function () {
                    tipsShow = false;
                }
            }).tooltip('show');

        },
        //关闭
        close: function (bSave) {
            if (window._closeOwnerWindow) {
                window._closeOwnerWindow();
                if (bSave) {
                    top[_params._winID]._datagrid.action.refresh();
                }
            } else {
                window.close();
            }
        },
        init: function () {
            window.top[_customer_service.guid] = window.self;
            _customer_service.bindWords();
        },
        get_HD_Code: function () {
            if (_params.HD > 0) {
                _customer_service.hd_codes.push({ HD_Code: _params.HD });
            }

            var hd_code = 0;
            if (_customer_service.hd_codes.length > 0) {
                hd_code = _customer_service.hd_codes[0].HD_Code;
                _customer_service.hd_codes.splice(0, 1);
            } else {
                if (!(_params.RP > 0)) { return 0; }

                var params = { action: 'getReportData', RP_Code: _params.RP, queryParams: _customer_service.queryParams };
                var res = $.ajax({
                    async: false,
                    url: _s_url_report,
                    data: { o: JSON.stringify(params) },
                    dataType: "json"
                }).responseText;
                res = JSON.parse(res);

                _customer_service.hd_codes = $.checkResponse(res, false) ? res : [];
                if (_customer_service.hd_codes.length > 0) {
                    hd_code = _customer_service.hd_codes[0].HD_Code;
                    _customer_service.hd_codes.splice(0, 1);
                }
            }

            return hd_code;
        },
        //_customer_service.bindCustomerInfo();
        bindCustomerInfo: function () {
            $("#btnDone,#btnSkip,#btnAddAppointment,#btnCall,#btnAddNotes,#btnAddCallBk").linkbutton('disable');
            $("#HD_Action").radiolist('readonly').radiolist('clear');

            var hd_code = _customer_service.get_HD_Code();
            if (!(hd_code > 0)) { return; }

            var params = { action: 'Get_CustomerInfo', HD_Code: hd_code };
            $.post(_s_url, { o: JSON.stringify(params) }, function (res) {
                if (!$.checkResponse(res)) { return; }
                var service = res.service;

                if (service) {
                    $.form.setData("#frm_customer_service", service, "#HD_Action");
                    $("#AU_Name").text($.trim(service.AU_Name));
                    $("#AU_Code").val(service.CH_AU_Code);
                    $("#PL_Number").text($.trim(service.PL_Number));
                    _customer_service.CG_Cat = service.CG_Cat;
                    _customer_service.PL_Code = service.PL_Code;
                    _customer_service.CI_Code = service.CI_Code;
                }
                if (_customer_service.CG_Cat === 3) {
                    //                  alert(service.CM_FileName);
                    //$("#c_campaign_file").html('<object data="' + service.CM_FileName + '&AU_Code=' + service.CH_AU_Code + '&DE_Code=4' + '" width="690px" height="370px"/>');
                    $("#c_campaign_file").html('<iframe name="SurveyiFrame" frameborder="0" border="0"  scrolling="no" src="' + service.CM_FileName + '&AU_Code=' + service.CH_AU_Code + '&DE_Code=' + service.HD_AU_Code + '" width="690px" height="370px"/>');
                    //                   alert(service.CM_FileName_+'AU_Code='+service.HD_AU_Code);
                    //                   $("#c_campaign_file").html('<object data="' +  service.CM_FileName +'&AU_Code='+service.CH_AU_Code+'&DE_Code='+service.HD_AU_Code+'" width="690px" height="370px"/>');
                } else {
                    //                   alert('>>>>'+service.CM_FileName);
                    $("#c_campaign_file").html($.trim(res.fileContent));
                }
                $("#btnSkip,#btnAddAppointment,#btnCall").linkbutton('enable');
            }, "json");
        },
        bindWords: function () {
            /*
            * 4122 Call Actions
            */
            $.getWords([4122], function (d) {
                if (d) {
                    if (d._4122) {
                        var tipsShow = false;
                        var $tooltip = null;
                        $("#HD_Action").radiolist({
                            data: d._4122,
                            repeatItems: 1,
                            valuechanged: function (value) {
                                if (tipsShow && $tooltip) {
                                    $("#c_tips").append($('#c_notes,#c_callback'));
                                    $tooltip.tooltip('destroy');
                                    tipsShow = false;
                                }

                                if (value == 49 || value == 38 || value == 99 || value == 100 || value == 150) {
                                    _customer_service._setTipsTitle(d._4122, value);
                                    $(this).tooltip({
                                        content: function () {
                                            return $('#c_notes');
                                        },
                                        position: 'right',
                                        showEvent: 'click',
                                        hideEvent: 'none',
                                        onShow: function () {
                                            $tooltip = $(this);
                                            tipsShow = true;
                                            $("#c_btn_done_notes").unbind("click").bind("click", function () {
                                                $("#c_tips").append($('#c_notes'));
                                                _Notes = $.trim($('#DH_Notes').textbox('getValue'));
                                                $.trim($('#EX_DH_Notes').textbox('setValue', _Notes));
                                                $tooltip.tooltip('destroy');
                                                tipsShow = false;
                                            });

                                            $("#c_btn_close_notes").unbind("click").bind("click", function () {
                                                $("#c_tips").append($('#c_notes'));
                                                $tooltip.tooltip('destroy');
                                                $("#DH_Notes").textbox('clear');
                                                _Notes = '';
                                                tipsShow = false;
                                            });
                                        },
                                        onHide: function () {
                                            tipsShow = false;
                                        }
                                    }).tooltip('show');
                                }

                                if (value == 31 || value == 40) {
                                    _customer_service._setTipsTitle(d._4122, value);
                                    $(this).tooltip({
                                        content: function () {
                                            return $('#c_callback');
                                        },
                                        position: 'right',
                                        showEvent: 'click',
                                        hideEvent: 'none',
                                        onShow: function () {
                                            $tooltip = $(this);
                                            tipsShow = true;
                                            $("#c_btn_done_callback").unbind("click").bind("click", function () {
                                                $("#c_tips").append($('#c_callback'));
                                                $tooltip.tooltip('destroy');
                                                tipsShow = false;
                                            });

                                            $("#c_btn_close_callback").unbind("click").bind("click", function () {
                                                $("#c_tips").append($('#c_callback'));
                                                $tooltip.tooltip('destroy');
                                                tipsShow = false;
                                                $("#RT_Time").datetimebox('clear');
                                            });
                                        },
                                        onHide: function () {
                                            tipsShow = false;
                                        }
                                    }).tooltip('show');
                                }
                            }
                        });
                    }
                }
                _customer_service.bindCustomerInfo();
            });
        },
        //绑定默认值
        bindDefaults: function () {
            $("#EX_DH_Notes,#DH_Notes").textbox('clear');
        },
        _setTipsTitle: function (d, v) {
            var text = '';
            $.each(d, function (i, w) {
                if (w.value == v) {
                    text = w.text;
                    return false;
                }
            });
            switch (v) {
                case '49', '99', '100', '150', '38':
                    $("#c_notes_title").text(text + ":");
                    break;
                case '31':
                case '40':
                    $("#c_callback_title").text(text + ":");
                    break;
            }
        }
    };

    $(function () {
        var ctValue = $.trim(_params.CT);
        var ct = {
            EX_Name: 'CT'
                , EX_Value: ctValue
                , EX_DataType: ctValue > 0 ? 'int' : 'string'
        };
        _customer_service.queryParams.push(ct);

        for (var i = 1; i <= 10; i++) {
            var pName = 'P' + i;
            var v = $.trim(_params[pName]);
            var p = {
                EX_Name: pName
                    , EX_Value: v
                    , EX_DataType: v > 0 ? 'int' : 'string'
            };
            _customer_service.queryParams.push(p);
        }
        if (_params.HD > 0) {
            $("#btnSkip").hide();
        }

        _customer_service.init();
        $("#btnAUInfo").click(function () {
            var au_Name = $("#AU_Name").text();
            var au_code = $("#AU_Code").val();
            window.top.$.topOpen({
                title: '',
<%--                url: "/templete/report/CustomerDetails.aspx?TI=Con&AU_Name=" + au_Name + "&AU_Code=" + au_code + "&_winID=<%=Guid.NewGuid()%>",--%>
                url: "/templete/report/CustomerManager.aspx?TI=Con&AU_Name=" + au_Name + "&AU_Code=" + au_code + "&_winID=<%=Guid.NewGuid()%>",
                width: 720,
                height: 460
            });
        });
    });
</script>
