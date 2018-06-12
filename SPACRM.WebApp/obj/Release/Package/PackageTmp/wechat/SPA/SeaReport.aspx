<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeaReport.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.SeaReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="月亮船">
    <!-- uc强制竖屏 -->
    <meta name="screen-orientation" content="portrait">
    <!-- UC强制全屏 -->
    <meta name="full-screen" content="yes">
    <!-- UC应用模式 -->
    <meta name="browsermode" content="application">
    <!-- QQ强制竖屏 -->
    <meta name="x5-orientation" content="portrait">
    <!-- QQ强制全屏 -->
    <meta name="x5-fullscreen" content="true">
    <!-- QQ应用模式 -->
    <meta name="x5-page-mode" content="app">
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/index.css" />
    <title>小时光Massage</title>
    <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
<%--        <div class="meiheader">
            <div class="askboxcon">
                <a href="tuijian.aspx"><span class="icon-left"></span></a>
                <h2>奖励明细</h2>
                <a href="javascript:void(0);"><span id="navRight" class="icon-lnk"></span></a>
            </div>

            <ul class="lnks">
                <li><a href="newindex.aspx"><span class="icon-logo-w"></span>我要预约</a></li>
                <li><a href="dingdan.aspx"><span class="icon-order_other"></span>我的订单</a></li>
                <li><a href="wo.aspx"><span class="icon-personal"></span>个人中心</a></li>
                <li><a href="tel:17621160808"><span class="icon-tel"></span>呼叫客服</a></li>
            </ul>
        </div>--%>
        <!--end header-->



        <!--begin content-->
        <div class="meiWapper" style="padding-top:0;">
            <div class="tuijian-content">
               <%-- <div class="mei-detail-img">
                    <img src="image/bigimg.jpg" />
                </div>--%>
                <div class="aipet-jinbi-items">
                    <ul>
                         <li style="padding:0;">
                            <p><span style="    line-height: 20px;
    font-size: 12px;">生成海报：<%=seacount %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总码数：<%=randnum %></span></p>
                        </li>
                        <%foreach (var o in sealist)
                          {%>
                        <li style="padding:0;">
                            <p><span style="    line-height: 20px;
    font-size: 12px;">码数：<%=o.c1 %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;占比：<%=o.bl %>%</span><%=o.Nickname %></p>
                        </li>
                        <%} %>
                    </ul>
                </div>


            </div>
        </div>
        <!--end content-->
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script>

</script>
