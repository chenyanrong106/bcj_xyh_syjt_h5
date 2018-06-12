<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /> 
<meta content="yes" name="apple-mobile-web-app-capable" /> 
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
<meta name="apple-mobile-web-app-title" content="小时光Massage"><!-- uc强制竖屏 -->
<meta name="screen-orientation" content="portrait"><!-- UC强制全屏 -->
<meta name="full-screen" content="yes"><!-- UC应用模式 -->
<meta name="browsermode" content="application"><!-- QQ强制竖屏 -->
<meta name="x5-orientation" content="portrait"><!-- QQ强制全屏 -->
<meta name="x5-fullscreen" content="true"><!-- QQ应用模式 -->
<meta name="x5-page-mode" content="app">
<meta content="telephone=no" name="format-detection" />
<link type="text/css" rel="stylesheet" href="css/index.css"/>
<title>小时光Massage</title>
</head>
<body>
    <form id="form1" runat="server">
 <!--begin header-->
<div class="meiheader">
  <div class="askboxcon">
    <a href="wechat.aspx"><span class="icon-left"></span></a>
    <h2>预约单</h2> 
    <a href="javascript:void(0);"><span id="navRight" class="icon-lnk"></span></a>   
    </div>
    
    <ul class="lnks">
         <li><a href="#"><span class="icon-logo-w"></span>我要预约</a></li>
         <li><a href="#"><span class="icon-order_other"></span>我的订单</a></li>
         <li><a href="#"><span class="icon-beautician"></span>我的技师</a></li>
         <li><a href="#"><span class="icon-personal"></span>个人中心</a></li>
         <li><a href="tel:17621160808"><span class="icon-tel"></span>呼叫客服</a></li>
   </ul>
</div>
<!--end header-->



<!--begin content-->
<div class="meiWapper">  
     <div class="order-content">
    <div class="order-address">
        <p><em><i class="icon-right"></i></em><strong><span class="icon-location"></span>静安寺</strong></p>
    </div>
    
    <div class="order-list-box">
       <ul>
           <%foreach (var o in list)
               {
                   %>
 <li>
             <p><span><i class="icon-tel icon-i-right"></i></span><img src="image/tu01.jpg"><strong><%=o.NAME %></strong>距离：2.5km</p>
             <p><span><i class="icon-right"></i></span><i class="icon-i icon-store"></i><bdo class="address-txt"><%=o.ADDRESS %></bdo></p>
            <p> <a href="orderTime.aspx?id=<%=o.ID %>&sid=<%=Request.QueryString["id"] %>"/><span>请选择服务时间 <i class="icon-right"></i></span></a><i class="icon-i icon-service-time"></i></p>
          </li>
               <%} %>
       </ul>
    </div>
    
    </div>
   
   
</div>
<!--end content-->
        <input type="hidden" id="sid" value="<%=Request.QueryString["id"] %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
