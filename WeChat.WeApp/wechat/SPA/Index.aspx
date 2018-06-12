<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Index" %>

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
    <a href="wechat.aspx"><span><em class="icon-down"></em><em>上海</em></span></a>
    <h2>首页</h2> 
    <a href="wechat.aspx"><span class="icon-tel"></span></a>   
    </div>
</div>
<!--end header-->
<!--begin content-->
<div class="meiWapper mei-index-bottom">
  
  
  <div class="shopImg">
<div class="swiper-container" id="swiperbanner">
        <div class="swiper-wrapper">
            
            <div class="swiper-slide"><img src="https://static.emeidaojia.com/static/02.jpg" alt="团购图片2"></div>
            <div class="swiper-slide"><img src="https://static.emeidaojia.com/static/1adsasacac.jpg" alt="开年狂欢瘦"></div>
            <div class="swiper-slide"><img src="https://static.emeidaojia.com/static/%E4%BA%86%E8%A7%A3%E6%88%91%E4%BB%ACbannerb%20(1).jpg" alt="这是一个有「态度」的美容平台"></div>
            <div class="swiper-slide"><img src="https://static.emeidaojia.com/static/tousuBN.jpg" alt="欢迎投诉"></div>
        </div>
        <div class="swiper-pagination"></div>
</div>

 <div class="acc-menu">
        <ul>
           <li><a href="order.aspx"><span class="icon-room"></span>
           <p>门店</p></a>
           </li>
           <li><a href="order.aspx"><span class="icon-agreement"></span>
           <p>订单</p></a>
           </li>
           <li><a href="#"><span class="icon-groupon"></span>
           <p>活动</p></a>
           </li>
           <li><a href="pay.aspx"><span class="icon-package2"></span>
           <p>支付</p></a>
           </li>
           
        </ul>
    </div>
</div>


<div class="acc-title-h2"><strong class="qizhi">推荐活动</strong></div>
<div class="mei-index-ad swiper-container" id="swiperSubimg02">
               <div class="swiper-wrapper">
                    <div class="swiper-slide"><span><img src="image/tu.jpg" width="" height=""/></span></div>
                   <div class="swiper-slide"><span><img src="image/tu.jpg"/></span></div>
                   <div class="swiper-slide"><span><img src="image/tu.jpg"/></span></div>
                   <div class="swiper-slide"><span><img src="image/tu.jpg"/></span></div>
                </div>
</div>  
  


<div class="acc-title-h2"><strong class="zuopin">精选作品</strong></div>

<div class="mei-index-items">
  <ul>  
      <%foreach (var s in servicelist)
        {
            %>
<li>
      <a href="detail.aspx?id=<%=s.ID %>"><dt class="fl"><img src="https://dimg.365vmei.cn/uploads/project/big_582438f2366b2.jpg" alt="海皙曼面部拨筋组合" onerror="javascript:this.src='http://static.emeidaojia.com/static/pro-default.png';"></dt>
      <dd class="fl">
         <h3><%=s.NAME %></h3>
         <p class="advantage"><%=s.TITLE %></p>
         <p class="introduce">
             <span class="duration"><i class="icon-red icon-duration"></i><%=s.TIME_LEN %>分钟</span>
         </p>
           <p class="introduce">
              <span class="new-price">￥<span><%=s.PRICE %></span></span>
              <span class="old-price">￥<%=s.PRICE %></span>
              <span class="service-num"><i class="icon-i icon-service-num"></i>576人做过</span>
          </p>  
         </dd>
         <div class="add-trolley">
           <a href="javascript:;" class="icon-add-item theme-color" data-name="海皙曼面部拨筋组合" data-price="568" data-duration="120" data-id="192" data-kind="1"></a>
         </div></a>
     </li>
      <%  } %>
     
     
     
    
  </ul> 
</div>

</div>
<!--end content-->

<div class="footerbar">
    <ul>
        <li class="cur"><a href="index.aspx"><span class="icon-logo"></span><bdo>门店</bdo></a></li>
        <li><a href="dingdan.aspx"><span class="icon-agreement"></span><bdo>订单</bdo></a></li>
        <li><a href="wo.aspx"><span class="icon-personal"></span><bdo>我的</bdo></a></li>
    </ul>
</div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/lazy.js"></script>
<script type="text/javascript" src="js/swiper.js"></script>
<script type="text/javascript">
    echo.init({
        offset: 100,
        throttle: 250
    });
    var mySwiper = new Swiper('#swiperbanner', { loop: true, autoplay: 4500, pagination: '.swiper-pagination' });
    var swiper02 = new Swiper('.mei-index-ad', {
        slidesPerView: 'auto',
        paginationClickable: true,
    });
</script>