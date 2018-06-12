<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>PETKIN手工湿粮</title>
	<meta charset="utf-8">
	<meta http-equiv="Pragma" content="no-cache" />
	<meta http-equiv="Cache-Control" content="no-cache"/>
	<meta name="viewport" content="user-scalable=no, minimal-ui, target-densitydpi=device-dpi" />
	<meta content="yes" name="apple-mobile-web-app-capable">
	<meta content="black" name="apple-mobile-web-app-status-bar-style">
	<meta content="telephone=no" name="format-detection">
    <link href="css/style.css" rel="stylesheet" type="text/css"/>
	<link href="css/swiper.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div id="loading"></div>
<div class="icon-arrow-up" data-type="arrow">
     <i class="ani-up2"></i>
</div>
<div id="wrapper">
<div class="swiper-container swiper-container-v">
     <div class="swiper-wrapper">
     
      <div class="swiper-slide" id="ss0">
          <div class="swiper-container" id="swiper-container-h-1">
            <div class="swiper-wrapper">
                <div class="innerbox" style="background:#000;">
                <div class="bg01 jianbian"></div>
                <div class="guang"><div class="demo p4-list1"><img src="images/guang.png"><img src="images/guang.png"></div></div>
                <div class="xiaoxian show01"><img src="images/xiaoxian.png"></div>
                <div class="hengkong suoda"><img src="images/hengkong.png"></div>
                
              </div>
            </div>
            <!-- Add Pagination -->
            <div class="swiper-pagination" id="swiper-pagination-1"></div>
          </div>
         </div>
     
     
        <div class="swiper-slide" id="ss1">
          <div class="swiper-container" id="swiper-container-h-2">
            <div class="swiper-wrapper">
                <div class="innerbox" style="background-image:url(images/1.jpg)">
                  <div id="widget_1" class="widget" style="z-index:1;left: 8px; right: 0; margin: 0 auto; top:357px; width:274px; height:74px;">
                    <div style="width:100%; height:100%;" class="gouliang">互联网狗粮1.0</div>
                  </div>
                  
                   <div class="widget" style="z-index:1; left:98px; bottom:225px;width:105px;height:85px;">
                   
                    <div class="per"><span class="num" data-per="0">0</span>小时</div>
                  </div>
                  <div class="widget" style="z-index:1; left:272px; bottom:225px;width:105px; height:85px;">
                    
                    <div class="per"><span class="num" data-per="24">0</span>小时</div>
                  </div>
                    <div class="widget" style="z-index:1; left:442px; bottom:225px;width:105px; height:85px;">
                    
                    <div class="per"><span class="num" data-per="96">0</span>小时</div>
                  </div>
                  
              </div>
            </div>
            <!-- Add Pagination -->
            <div class="swiper-pagination" id="swiper-pagination-2"></div>
          </div>
         </div>
      <div class="swiper-slide" id="ss2">
        <div class="swiper-container" id="swiper-container-h-3">
            <div class="swiper-wrapper">
             
                <div class="innerbox" style="background-image:url(images/2.jpg)">
                  <div class="widget" style="z-index:1; left:98px; top:325px;width:105px;height:85px;">
                    <div class="pic"><img width="85px" height="100%" src="images/xx.png"></div>
                    <div class="per">+ <span class="num" data-per="86.9">0</span>.8 %</div>
                  </div>
                  <div class="widget" style="z-index:1; left:222px; top:325px;width:105px; height:85px;">
                    <div class="pic"><img width="85px" height="100%" src="images/yy.png"></div>
                    <div class="per">+ <span class="num" data-per="22.6">0</span>.6 %</div>
                  </div>
                  <div class="widget" style="z-index:1; left:345px; top:325px;width:105px; height:85px;">
                    <div class="pic"><img width="85px" height="100%" src="images/kg.png"></div>
                    <div class="per">+ <span class="num" data-per="76.1">0</span>.1 %</div>
                  </div>
                  <div class="widget" style="z-index:1; left:476px; top:325px;width:105px; height:85px;">
                    <div class="pic"><img width="85px" height="100%" src="images/aq.png"></div>
                    <div class="per">+ <span class="num" data-per="95.5">0</span>.5 %</div>
                  </div>
               
              </div>

            </div>
            <!-- Add Pagination -->
            <div class="swiper-pagination" id="swiper-pagination-3"></div>
          </div>
        	
		</div>
		
		<div class="swiper-slide" id="ss3">
       <div class="swiper-container" id="swiper-container-h-4">
        <div class="swiper-wrapper">
        
          	<div class="innerbox" style="background-image:url(images/4.jpg)">
          		<div class="widget"  style="z-index:1; left:200px; top:400px;width:105px; ">
    			      <div class="pic"><img width="85px" height="100%" src="images/yy.png"></div>
    			      <div class="per">+ <span class="num" data-per="79">0</span>.0 %</div>
    			    </div>
    			    <div class="widget"  style="z-index:1; left:365px; top:400px;width:105px; height:85px;">
    			      <div class="pic"><img width="85px" height="100%" src="images/aq.png"></div>
    			      <div class="per">+ <span class="num" data-per="17.4">0</span>.4 %</div>
    			    </div>
          	</div>
         
        </div>
        <!-- Add Pagination -->
        <div class="swiper-pagination" id="swiper-pagination-4"></div>
      </div>
		</div>
		<div class="swiper-slide" id="ss4">
      <div class="swiper-container" id="swiper-container-h-5">
        <div class="swiper-wrapper">
         
            <div class="innerbox" style="background-image:url(images/5.jpg)">
              <div class="widget"  style="z-index:1; left:190px; top:305px;width:105px; ">
                <div class="pic"><img width="85px" eight="100%" src="images/yy.png"></div>
                <div class="per">+ <span class="num" data-per="23.6">0</span>.6 %</div>
              </div>
              <div class="widget"  style="z-index:1; left:365px; top:305px;width:105px; height:85px;">
                <div class="pic"><img width="85px" height="100%" src="images/kg.png"></div>
                <div class="per">+ <span class="num" data-per="19.2">0</span>.2 %</div>
              </div>
           
          </div>

         

        </div>
        <!-- Add Pagination -->
        <div class="swiper-pagination" id="swiper-pagination-5"></div>
      </div>
		</div>
		
		<div class="swiper-slide" id="ss5">
      <div class="swiper-container" id="swiper-container-h-6">
        <div class="swiper-wrapper">
       
            <div class="innerbox" style="background-image:url(images/7.jpg)">
              <div class="widget"  style="z-index:1; left:190px; top:285px;width:105px; ">
                <div class="pic"><img width="85px" height="100%" src="images/xx.png"></div>
                <div class="per">+ <span class="num" data-per="47.8">0</span>.8 %</div>
              </div>
              <div class="widget"  style="z-index:1; left:365px; top:285px;width:105px; height:85px;">
                <div class="pic"><img width="85px" height="100%" src="images/aq.png"></div>
                <div class="per">+ <span class="num" data-per="79.9">0</span>.9 %</div>
              </div>
           
          </div>

        </div>
        <!-- Add Pagination -->
        <div class="swiper-pagination" id="swiper-pagination-6"></div>
      </div>
   </div>
	
		
       
        
        
        <div class="swiper-slide" id="ss6">
        	<div class="innerbox" style="background-image:url(images/9.jpg)">
            
             <div class="chanpin">
                <ul>
                  <li>
                 <div><a href="#"><img src="images/chan001.jpg"/></a>
                 <p>牛肉或三文鱼配方<br>1.5kg（100g*15袋）</p>
                 <span>尝鲜价:<strong>￥90</strong> 包邮</span>
                 <a href="zhifu3.aspx"><bdo class="buybtn">立即购买</bdo></a>
                 </div>
                 </li>
                  <li>
                  <div><a href="#"><img src="images/chan002.jpg" /></a>
                 <p>牛肉或三文鱼配方<br>5.0kg（250g*20袋）</p>
                 <span>尝鲜价:<strong>￥240</strong> 包邮</span>
                  <a href="zhifu3.aspx"><bdo class="buybtn">立即购买</bdo></a>
                 </div>
                 </li>
                </ul>
             </div>
            
            <div style="position:absolute;bottom:120px;width:640px;text-align:center" class="show02">
            
               <p>
                <a class="button button-balanced button-rounded" href="http://mp.weixin.qq.com/s?__biz=MzI0MzAzMjAyOA==&mid=210326296&idx=1&sn=269139b531d111f188bd67ea497bd03e&scene=0#wechat_redirect">点击关注我们</a>
              </p>
            </div>
          </div>
		</div>
        
    </div>
</div>
</div>

<script src="js/jquery.min.js"></script>
<script src="js/swiper.min.js"></script>
<script src="js/velocity.min.js"></script>
<script src="js/velocity.ui.min.js"></script>
<script src="js/jquery.animateNumber.min.js"></script>
<script src="js/app.js"></script>
         <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/GetLocation.js?v=1.0"></script>
    </form>
</body>
</html>
