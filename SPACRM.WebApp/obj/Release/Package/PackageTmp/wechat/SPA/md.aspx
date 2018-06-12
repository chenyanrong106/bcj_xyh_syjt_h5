﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="md.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.md" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="小时光Massage">
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
    <link type="text/css" rel="stylesheet" href="css/index.css?v=1" />
    <title>小时光Massage</title>  <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="window.location='index.aspx'"><span class="icon-left"></span></a>
                <h2>门店列表</h2>
                <a href="javascript:void(0);"><span id="navRight" class="icon-lnk"></span></a>
            </div>

            <ul class="lnks">
                <li><a href="index.aspx"><span class="icon-logo-w"></span>我要预约</a></li>
                <li><a href="dingdan.aspx"><span class="icon-order_other"></span>我的订单</a></li>
                <li><a href="wo.aspx"><span class="icon-personal"></span>个人中心</a></li>
                <li><a href="tel:17621160808"><span class="icon-tel"></span>呼叫客服</a></li>
            </ul>
        </div>
        <!--end header-->



        <!--begin content-->
        <div class="meiWapper">
            <div class="order-content">
                <div class="order-list-box">
                    <ul>
                        <%foreach (var o in list)
                          {
                        %>
                        <li>
                            <p>
                                <span onclick="location='tel:17621160808'"><i class="icon-tel icon-i-right"></i></span>
                                <img src="image/md.jpg"><strong><%=o.NAME %></strong>距离：<%=Math.Round(o.jl/1000,1) %>km
                            </p>
                            <p onclick="showmap(<%=o.Lat %>,<%=o.Lng %>,'<%=o.NAME %>','<%=o.ADDRESS %>')"><span><i class="icon-right"></i></span><i class="icon-i icon-store"></i><bdo class="address-txt"><%=o.ADDRESS %></bdo></p>
                           <%-- <%if (Request.QueryString["id"] != null)
                              { %>
                            <p><a href="javascript:;" onclick="goorder(<%=o.ID %>)" /><span>请选择服务时间 <i class="icon-right"></i></span></a><i class="icon-i icon-service-time"></i></p>
                            <%} %>--%>
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
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script>
    function showmap(a, b, c, d) {
        wx.openLocation({
            latitude: a, // 纬度，浮点数，范围为90 ~ -90
            longitude: b, // 经度，浮点数，范围为180 ~ -180。
            name: c, // 位置名
            address: d, // 地址详情说明
            scale: 28, // 地图缩放级别,整形值,范围从1~28。默认为最大
            infoUrl: '' // 在查看位置界面底部显示的超链接,可点击跳转
        });
    }
</script>