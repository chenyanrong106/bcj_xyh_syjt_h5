<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Buy8.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Buy8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8">
<title>手工半湿粮—PETKIN宠物管家</title>
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
<meta content="yes" name="apple-mobile-web-app-capable" />
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta content="telephone=no" name="format-detection" />
<style type="text/css">
*{margin:0; padding:0;}
body{background:#fff; margin:0; padding:0;width:100%;height:100%;overflow-x:hidden;-webkit-user-select:none;-webkit-text-size-adjust:none;}
a:link,a:visited{ color:#666; text-decoration:none;} a:hover{text-decoration:none;}
.main{ position:relative; width:100%; height:580px; display:block; z-index:1;}
.logo{ background:url(images/logo5.jpg) no-repeat center center; height:120px; width:100%; display:block; background-size:300px 54px;}
 .chanpin{ width:100%; text-align:center; min-width:320px; display:block; overflow:hidden;height:330px;}
  .chanpin ul li{ float:left; width:50%;  display:block;}
  .chanpin ul li div{ padding:10px 5px; display:block; height:300px;}
  .chanpin ul li img{width:90%; display:block; margin:0 auto;}
  .chanpin ul li p{ line-height:22px; font-size:16px; color:#333; padding-top:10px; text-align:center;}
  .chanpin ul li span{color:#ed6c00; display:block;font-size:14px; padding-top:10px; margin-bottom:10px;}
  .chanpin ul li strong{ color:#ed6c00; font-size:20px; font-family:Arial, Helvetica, sans-serif; line-height:30px;}
  .buybtn{ width:130px; height:35px; background:#2dcb73; border-radius:35px; display:block; color:#fff; font-size:18px; line-height:35px; text-align:center; margin:5px auto;}
  .divbox{ background:#ddd; width:100%; height:1px; display:block; margin-top:40px;}
  .weizhi{ width:150px; height:36px; background:#fff; display:block; position:absolute; bottom:-15px; left:50%; margin-left:-75px; border-radius:36px; line-height:36px; text-align:center; border:1px solid #ddd; color:#666;}
  .weizhi a{ display:block;}
</style>

</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
 <div class="logo"></div>
 <div class="chanpin">
                <ul>
                  <li>
                 <div><a href="#"><img src="images/chan001.jpg"/></a>
                 <p>牛肉或三文鱼配方<br>1.5kg（100g*15袋）</p>
                 <span>尝鲜价:<strong>￥90</strong> 包邮</span>
                 <a href="zhifu8.aspx"><bdo class="buybtn">立即购买</bdo></a>
                 </div>
                 </li>
                  <li>
                  <div><a href="#"><img src="images/chan002.jpg" /></a>
                 <p>牛肉或三文鱼配方<br>5.0kg（250g*20袋）</p>
                 <span>尝鲜价:<strong>￥240</strong> 包邮</span>
                  <a href="zhifu8.aspx"><bdo class="buybtn">立即购买</bdo></a>
                 </div>
                 </li>
                </ul>
             </div>
            
        
            <div class="divbox">
               <p class="weizhi">
                <a href="http://mp.weixin.qq.com/s?__biz=MzI0MzAzMjAyOA==&mid=210326296&idx=1&sn=269139b531d111f188bd67ea497bd03e&scene=0#wechat_redirect">联系客服了解鲜粮</a>
              </p>
            </div>  
 
        <div style="font-size:10pt;color:gray;padding-left:10px;padding-right:10px;">  PS：宠物帮领养中心粉丝在消费后，销售额中的10%会捐献给宠物帮救助基金，并会在每月初公布基金明细。此活动由宠物帮领养中心与PETKIN手工湿粮联合发起。</div>

<div>

    </form>
</body>
</html>
 <script src="js/jquery.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/GetLocation.js?v=1.0"></script>