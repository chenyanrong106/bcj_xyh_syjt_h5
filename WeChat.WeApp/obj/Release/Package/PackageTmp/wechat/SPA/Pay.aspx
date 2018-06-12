<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Pay" %>

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
    <h2>订单支付</h2> 
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
    <div class="pay-order-info">
          <p><strong>订单号：</strong> 2181227266621</p>
          <p><strong>支付金额：</strong> <bdo>￥159.00</bdo></p>
    </div>
    
     <div class="pay-order-info pay-order-paylei">
      <h2>选择支付方式</h2>
          <p><span><i class="icon-radio"></i></span><strong><i class="kaka"></i>储值卡</strong> 余额：0.00 <bdo class="chongzhi">充值</bdo></p>
          <p><span><i class="icon-radio"></i></span><strong><i class="weixin"></i>微信支付</strong> </p>
          <p><span><i class="icon-radio"></i></span><strong><i class="zhifu"></i>支付宝支付</strong> </p>
    </div>
   
   
</div>
<!--end content-->

<div class="book-box">
 <ul class="order flex payend">
    <li class="orderNum">支付剩余时间：</li>
    <li id="shengyuTime">10:10:00</li>
    <li><a class="btn-normal bookBtn" href="javascript:;" id="bookall">去支付</a></li>
</ul>
</div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script>

zhifuTime();



$('.pay-order-paylei p').find('.icon-radio').each(function(index, element) {
   
  $(this).on('click',function(){
	 $('.pay-order-paylei p span i').removeClass('icon-radio-f').addClass('icon-radio');
	if($(this).hasClass('icon-radio')){
		  $(this).removeClass('icon-radio').addClass('icon-radio-f');
		}else{
		   $(this).removeClass('icon-radio-f').addClass('icon-radio');	
		} 
	
});	
	
})

</script>