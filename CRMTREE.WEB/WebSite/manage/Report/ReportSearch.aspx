<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportSearch.aspx.cs" Inherits="manage_Report_ReportSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title></title>
    <link href="/wechatcss/pintuer.css" rel="stylesheet" />
    <link href="/styles/ST_002.css" rel="stylesheet" />
    <script src="/wechatjs/jquery-1.10.2.min.js"></script>
    <script src="/wechatjs/pintuer.js"></script>
    <script src="/scripts/manage/Report/ReportSearch.js"></script>
</head>
<body>
    <div class="container-layout margin-little padding-little">
        <div class="panel margin-small-bottom head1">
            <div class="panel-body  padding SearchCategory">
                <%--      <label class="float-left margin-big-right" style="display:inline-block;vertical-align:middle">搜索名称 >></label>
                <ul class="selected-inline">
                    <li value="1">车主信息</li>
                    <li>车辆信息</li>
                    <li>维修历史</li>
                    <li>销售信息</li>
                    <li class="selected border-sub">预约信息</li>
                    <li>其他</li>
                </ul>
                 <hr class="" />
                <div class="ca-info">
                    <label>基本信息：</label>
                    <label>
                        <input type="checkbox" />
                        拼图框架</label>
                    <label>
                        <input type="checkbox" />
                        拼图框架</label>
                </div>--%>
            </div>
        </div>

        <div class="panel margin-small-bottom searchQuery">
            <div class="panel-body padding">
                <div class=" margin-little-bottom padding-little">
                    <span class="text-big margin-big-right" style="display:inline-block;vertical-align:middle">搜索条件 >></span>
                    <div class="button-group radio">
                        <label class="button button-small active">
                            <input name="is-Query" value="yes" checked="checked" type="radio" /><span class="icon icon-check"></span> 单选</label>
                        <label class="button button-small">
                            <input name="is-Query" value="no" type="radio" /><span class="icon icon-check"></span> 多选</label>
                    </div>
                </div>



                <div class="panel">
                    <ul class="list-group">
                        <li>
                            <div class="ca-info">
                                <label>
                                    <input type="radio" />
                                    在 2015-01-01 和 2015-06030 期间消费超过 1000 金额的车主</label>
                            </div>
                        </li>
                        <li>
                            <div class="ca-info">
                                <label>
                                    <input type="radio" />
                                    上次访问在7天前</label>
                            </div>
                        </li>
                        <li>
                            <div class="ca-info">
                                <label>
                                    <input type="radio" />
                                    质保过期前15 天</label>
                            </div>
                        </li>
                        <li>
                            <div class="ca-info">
                                <label>
                                    <input type="radio" />
                                    5个月不<input type="text" class="input" placeholder="用户名/邮箱/手机" />到店的客户</label>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
