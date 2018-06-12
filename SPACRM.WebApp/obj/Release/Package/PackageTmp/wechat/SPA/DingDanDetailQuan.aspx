<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingDanDetailQuan.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.DingDanDetailQuan" %>

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
<title>小时光Massage</title>  <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
<div class="meiheader">
  <div class="askboxcon">
    <a  href="javascript:;" onclick="history.go(-1)"><span class="icon-left"></span></a>
    <h2>我的订单</h2> 
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
<div class="meiWapper mei-index-bottom">  
     <div class="order-content">
        
       
       
       
<div class="aipet-order-items">
    <ul>
      <li>
         <div class="aipet-order-ltems-title order-detail-title">
            <span><bdo class="zhifubtn">取消订单</bdo></span>
            <strong>订单信息 <em class="daodian-icon">到店</em></strong>
         </div>
         <div class="aipet-order-ltems-con">
            <span>订单状态：<bdo class="red-color">未支付</bdo></span>
            <span>订单号：2291821626252555</span>
            <span>下单时间：2017-02-20 18：30</span>
         </div>
         
         <div class="pro-liucheng">
            <ol class="cur-status"><span></span>确认下单</ol>
            <ol><span></span>支付成功</ol>
            <ol><span></span>开始服务</ol>
            <ol><span></span>服务结束</ol>
         </div>
    </li>
    
    
    <li><h2>优惠券信息</h2>
      <div class="quanbox">
          <p><span><i class="icon-ma"></i></span><strong>可使用（1张）</strong>
          有效期至2017年3月31日</p>
          <h4>券号： <span>0982 7333 32</span></h4>
      </div>
    </li>     


<li>
<h2>项目信息</h2>
         <div class="aipet-order-ltems-con">
            <div class="imgbox"><img src="image/jishi.jpg"></div>
            <div class="pro-info-txt"> 
            <p><strong>许婷婷</strong></p>
            
            <span>项目名称：<em class="nameclass">海皙曼面部拨筋组合</em></span>
            <span>实付金额：<bdo class="nameclass red-color">188元</bdo></span>
            </div>
          
         </div>

     </li>

   <li>
<h2>服务信息</h2>
         <div class="order-fuwu-con">
            <p><bdo><a href="tel:17621160808"><i class="icon-tel"></i></a></bdo><span>联&nbsp;&nbsp;系&nbsp;&nbsp;人：</span> Mr li小结</p>
            <p><span>联系方式：</span> 4009-333-333</p>
            <p><span>服务时间</span> 2017.02.10 16:00</p>
            <p><span>门店名称：</span> 酒店提供收费停车位</p>
            <p><span>服务地址：</span> 上海虹桥元一希尔顿酒店是2015年亚洲公务航空会所旁边</p>
         </div>

     </li>    
       
       
   </ul>
    </div>
    
    
    </div>
   
</div>
<!--end content-->

<div class="paybtnbg">
  <a href="pay.aspx"><div class="paybtn">支付</div></a>
</div>

<!--begin 二维码弹窗-->
<div class="maskbg" id="erweimaId">
   <div class="maskcon">
      <img src="image/erweima_big.png"/>
   <div class="maskdel"><i class="icon-shut"></i></div>
   </div>
</div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script>
$('.icon-ma').on('click',function(){
	
	$('#erweimaId').show();
	})
$('.icon-shut').on('click',function(){
	
	$('#erweimaId').hide();
	})	
</script>