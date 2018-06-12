<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingDan.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.DingDan" %>

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
     <span></span>
    <h2>我的订单</h2> 
    <a href="javascript:void(0);"><span class="icon-tel"></span></a>   
    </div>
    
    <ul class="lnks">
  
</div>
<!--end header-->

<div class="order-tag-item">
             <ol><span class="curbtn">进行中</span></ol>
               <ol><span>待评价</span></ol>
                 <ol><span>已取消</span></ol>
</div>

<!--begin content-->
<div class="meiWapper mei-index-bottom mei-index-Top">  
     <div class="order-content">
        
       
       
       
<div class="aipet-order-items">
    <ul>
      <li>
         <div class="aipet-order-ltems-title">
            <span>已支付</span>
            <strong>2016-08-02 12:23:02</strong>
         </div>
         
        
         <div class="aipet-order-ltems-con">
           <a href="dingdan_detail.aspx"><div class="imgbox"><img src="image/jishi.jpg"></div>
            <p><strong>许婷婷</strong></p>
            <span>订单号：2291821626252555</span>
            <span>项目名称：140ML</span>
            <span>服务时间：2017-02-20 18：30</span>
            </a>
         </div>
         
        
         
           <div class="aipet-order-buybtn">
                  <strong>￥159</strong>
                  <div><span class="quanma">查看券码</span></div>
           </div>
        
          <div class="daodian-fr daodian-icon">到店</div>
     </li>
     
     
     
      <li>
         <div class="aipet-order-ltems-title">
            <span>未支付</span>
            <strong>2016-08-02 12:23:02</strong>
         </div>
         
        
         <div class="aipet-order-ltems-con">
           <a href="dingdan_detail.aspx"><div class="imgbox"><img src="image/jishi.jpg"></div>
            <p><strong>许婷婷</strong></p>
            <span>订单号：2291821626252555</span>
            <span>项目名称：140ML</span>
            <span>服务时间：2017-02-20 18：30</span>
            </a>
         </div>
         
        
         
           <div class="aipet-order-buybtn">
                  <strong>￥159</strong>
                  <div><span class="fukuan">立即支付</span></div>
           </div>
        
          <div class="daodian-fr daodian-icon">到店</div>
     </li>
     
     
     
      <li>
         <div class="aipet-order-ltems-title">
            <span>已支付</span>
            <strong>2016-08-02 12:23:02</strong>
         </div>
         
        
         <div class="aipet-order-ltems-con">
           <a href="dingdan_detail.aspx"><div class="imgbox"><img src="image/jishi.jpg"></div>
            <p><strong>许婷婷</strong></p>
            <span>订单号：2291821626252555</span>
            <span>项目名称：140ML</span>
            <span>服务时间：2017-02-20 18：30</span>
            </a>
         </div>
         
        
         
           <div class="aipet-order-buybtn">
                  <strong>￥159</strong>
                  <div><span class="quanma">我要评价</span></div>
           </div>
        
          <div class="daodian-fr daodian-icon">到店</div>
     </li>
     
     
     
      <li>
         <div class="aipet-order-ltems-title">
            <span>未支付</span>
            <strong>2016-08-02 12:23:02</strong>
         </div>
         
        
         <div class="aipet-order-ltems-con">
           <a href="dingdan_detail.aspx"><div class="imgbox"><img src="image/jishi.jpg"></div>
            <p><strong>许婷婷</strong></p>
            <span>订单号：2291821626252555</span>
            <span>项目名称：140ML</span>
            <span>服务时间：2017-02-20 18：30</span>
            </a>
         </div>
         
        
         
           <div class="aipet-order-buybtn">
                  <strong>￥159</strong>
                  <div><span class="fukuan">立即支付</span></div>
           </div>
        
          <div class="daodian-fr daodian-icon">到店</div>
     </li>
       
       
       
   </ul>
    </div>
</div>
<!--end content-->


<div class="footerbar">
    <ul>
        <li><a href="index.aspx"><span class="icon-logo-w"></span><bdo>门店</bdo></a></li>
        <li class="cur"><a href="dingdan.aspx"><span class="icon-agreement"></span><bdo>订单</bdo></a></li>
        <li><a href="wo.aspx"><span class="icon-personal"></span><bdo>我的</bdo></a></li>
    </ul>
</div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>