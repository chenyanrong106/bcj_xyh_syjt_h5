<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiShi.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.JiShi" %>

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
    <h2>选择美容师</h2> 
    <span></span>
    </div>
    
   
</div>
<!--end header-->

<div class="order-tag-item">
    <div class="order-tag-tab">
             <ol><bdo class="oneli curtab">综合排序</bdo></ol>
               <ol><bdo>月订单最多</bdo></ol>
                 <ol><bdo>月好评最多</bdo></ol>
            </div>     
</div>

<!--begin content-->
<div class="meiWapper mei-index-Top">  
     <div class="order-content">
     
     
     <div class="aipet-order-items">
    <ul>
      <li> 
       <div class="aipet-order-ltems-con">
           <div class="jishi-checked"><i class="icon-radio"></i></div>
           <div class="imgbox"><img src="image/jishi.jpg"></div>
            <p><strong>许婷婷</strong></p>
            <span class="jishi_info">美容导师是专业美容行业的导师是专业美容导师是专业美容导师是专业美容一种职业称下级的美容机构提供技术培训和终端会议的支持。</span>
            <p class="jishi_introduce"><span><i class="icon-order_other"></i>月订单数18</span><span class="service-num"><i class="icon-favor2"></i>月评论数10</span></p>
         </div>
       </li>
       
       
        <li> 
       <div class="aipet-order-ltems-con">
       <div class="jishi-checked"><i class="icon-radio"></i></div>
           <div class="imgbox"><img src="image/jishi.jpg"></div>
            <p><strong>许婷婷</strong></p>
            <span class="jishi_info">美容导美容一种职业称下级的美容机构提供技术培训和终端会议的支持。</span>
            <p class="jishi_introduce"><span><i class="icon-order_other"></i>月订单数18</span><span class="service-num"><i class="icon-favor2"></i>月评论数10</span></p>
         </div>
       </li>
       
        <li> 
       <div class="aipet-order-ltems-con">
           <div class="jishi-checked"><i class="icon-radio"></i></div>
           <div class="imgbox"><img src="image/jishi.jpg"></div>
            <p><strong>许婷婷</strong></p>
            <span class="jishi_info">美容导美容一种职业称下级的美容机构提供技术培训和终端会议的支持。</span>
            <p class="jishi_introduce"><span><i class="icon-order_other"></i>月订单数18</span><span class="service-num"><i class="icon-favor2"></i>月评论数10</span></p>
         </div>
       </li>
       
       
        <li> 
       <div class="aipet-order-ltems-con">
       <div class="jishi-checked"><i class="icon-radio"></i></div>
           <div class="imgbox"><img src="image/jishi.jpg"></div>
            <p><strong>许婷婷</strong></p>
            <span class="jishi_info">美容导美容一种职业称下级的美容机构提供技术培训和终端会议的支持。</span>
            <p class="jishi_introduce"><span><i class="icon-order_other"></i>月订单数18</span><span class="service-num"><i class="icon-favor2"></i>月评论数10</span></p>
         </div>
       </li>
       
       
        <li> 
       <div class="aipet-order-ltems-con">
       <div class="jishi-checked"><i class="icon-radio"></i></div>
           <div class="imgbox"><img src="image/jishi.jpg"></div>
            <p><strong>许婷婷</strong></p>
            <span class="jishi_info">美容导师是专业美容行业的美容机构提供技术培训和终端会议的支持。</span>
            <p class="jishi_introduce"><span><i class="icon-order_other"></i>月订单数18</span><span class="service-num"><i class="icon-favor2"></i>月评论数10</span></p>
         </div>
       </li>
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

//备注 目前只能选一位技师，如果要允许多选 ，把数字1改成别的数字即可
$('.aipet-order-items ul li').find('.icon-radio').on('click',function(){
	var leng=$('.aipet-order-items ul li').find('.icon-radio-f').length;
    if(leng>=1 && $(this).hasClass('icon-radio-f')!=true){
		tips("亲，您只能选择一位技师");
	}
	if($(this).hasClass('icon-radio') && leng<1){
		  $(this).removeClass('icon-radio').addClass('icon-radio-f');
		}else{
		   $(this).removeClass('icon-radio-f').addClass('icon-radio');	
		} 
	
	
	
})

</script>