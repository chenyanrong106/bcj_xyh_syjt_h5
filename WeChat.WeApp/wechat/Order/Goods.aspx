<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goods.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Goods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<title>手工小鲜粮—PETKIN宠物管家</title>
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
<meta content="yes" name="apple-mobile-web-app-capable" />
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta content="telephone=no" name="format-detection" />
<style type="text/css">
*{margin:0; padding:0;}
body{background:#fff; margin:0; padding:0;width:100%;height:100%;overflow-x:hidden;}
.main{width:100%; height:100%; max-width:640px; display:block; background:#fff; z-index:999;}
.logobox{ background:url(images/logo.png) no-repeat center center; background-size:282px 43px; display:block;  height:43px; padding:5px;}
.logotxt{ padding:5px; display:block; width:282px; margin:0 auto;}
.logotxt p{line-height:20px; font-size:14px;}
.logotxt p strong{font-size:16px; color:#ed6d00;}
.buybtn{ width:290px; padding:10px; margin:0 auto; overflow:hidden;}
.buybtn ul li{margin-bottom:5px; border:1px solid #ed6d00; height:44px; width:286px;}
.buybtn .info{float:left; width:196px; height:44px; background:#fff; text-align:center;}
.buybtn .info h2{font-size:16px; font-weight:bolder; line-height:22px; color:#ab833d;}
.buybtn .info p{font-size:12px; color:#ab833d; font-family:Arial, Helvetica, sans-serif;}
.buybtn .info p strong{color:#ff0000; font-size:16px;}
.buybtn .goumai{float:left; width:90px; height:44px; background:#ed6d00; display:block;}
.buybtn .mai{ background:url(images/tubiao.png) no-repeat 5px center; background-size:22px 19px; padding-left:34px; color:#fff; font-size:16px; font-weight:bold; display:block; line-height:44px;}
.buybtn a{display:block; text-decoration:none;}
.about{width:100%; display:block; height:30px; display:block; position:relative;}
.about h3{width:100px; border-radius:20px; height:30px; background:#eee; color:#999; font-weight:normal; position:absolute; left:50%; top:0; margin-left:-50px; font-size:16px; line-height:30px; text-align:center; z-index:999;}
.about h3 a{color:#999; text-decoration:none; font-size:14px;}
.about p{ height:1px; background:#eee; width:100%; position:absolute; top:14px; left:0; display:block; z-index:0;}
.chanimg{ padding:10px; display:block;}
.chanimg img{width:100%; display:block; max-width:280px; margin:0 auto;}
</style>
</head>
<body>
    <form id="form1" runat="server">
   <div class="main">
  <div class="logobox"></div>
  <div class="logotxt">
    <p>如果说我们能改变什么</p>
<p>我想是让每一个宠物主人知道</p>
<p>您的宝贝</p>
<p><strong>可以有更好 更健康的选择!</strong></p>
  </div>
  
  <div class="buybtn">
    <ul>
       <li>
         <div class="info">
             <h2>排除毒素套餐</h2>
             <p><strong>￥36</strong> / 6包（600g）包邮</p>
         </div>
         <div class="goumai">
           <a href="zhifu3.aspx?id=1"><bdo class="mai">买买买</bdo></a>
         </div>
       </li>
       <li>
         <div class="info">
             <h2>改善体质套餐</h2>
             <p><strong>￥108</strong> / 30包（700g）包邮</p>
         </div>
         <div class="goumai">
           <a href="zhifu3.aspx?id=2"><bdo class="mai">买买买</bdo></a>
         </div>
       </li>
     </ul>
  </div>



<div class="about">
    <h3><a href="#">关于我们</a></h3>
   <p></p>
</div>

<div class="chanimg"><img src="images/chantu.png"></div>
</div>
    </form>
</body>
</html>
