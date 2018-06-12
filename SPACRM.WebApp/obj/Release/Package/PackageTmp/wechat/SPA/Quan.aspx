<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quan.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Quan" %>

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
    <h2>我的优惠券</h2> 
     <a href="tel:17621160808"><span class="icon-tel"></span></a>   
    </div>
    
   
</div>
<!--end header-->


<!--begin content-->
<div class="meiWapper">  
     <div class="order-content">
     
     
       <div class="quan-fang-sou">
          <p><bdo><em class="submitbtn">兑换</em></bdo><span><input name="" type="text" class="text" placeholder="请输入您的兑换码"></span></p>
        </div>
     
    <div class="order-quan-item">
    <ul>
      <li> 
         <div class="quan-item-img">
           <strong>￥200</strong>
           <p>代金券</p>
         </div>
          <div class="quan-item-info"> 
             <p><strong>可以使用</strong></p>
             <p>限项目使用</p>
             <p>有效期至2017.12.31 23:59</p>
          </div>
          <div class="quan-item-jiao">通用</div>
          <div class="quan-item-jiantou downicon"></div>
          <div class="quan-info-txt">
              <p>适用项目：护理后应该在温暖、舒适的环境中稍作休息（1—2小时），让机体恢复正常状态，再进行其他活动</p>
          </div>
       </li>
       
       
        <li> 
         <div class="quan-item-img">
           <strong>￥100</strong>
           <p>代金券</p>
         </div>
          <div class="quan-item-info"> 
             <p><strong>可以使用</strong></p>
             <p>限项目使用</p>
             <p>有效期至2017.12.31 23:59</p>
          </div>
          <div class="quan-item-jiao">通用</div>
          <div class="quan-item-jiantou downicon"></div>
          <div class="quan-info-txt">
              <p>适用项目：护理后应该在温暖、舒适的环境中稍作休息（1—2小时），让机体恢复正常状态，再进行其他活动</p>
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
$('.order-quan-item ul li').find('.quan-item-jiantou').on('click',function(){
   
	if($(this).hasClass('downicon')){
		  $(this).removeClass('downicon').addClass('upicon');
		  $(this).next().show();
		}else{
		   $(this).removeClass('upicon').addClass('downicon');	
		    $(this).next().hide();
		} 
	
	
	
})

</script>