﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LingQu.aspx.cs" Inherits="SPACRM.WebApp.wechat.Discount.LingQu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /> 
<meta content="yes" name="apple-mobile-web-app-capable" /> 
<meta content="black" name="apple-mobile-web-app-status-bar-style" /> 
<meta content="telephone=no" name="format-detection" />
<link type="text/css" href="css/youhuiquan.css" rel="stylesheet" />
<title>优惠券</title>
</head>
<body class="bgcolor">
    <form id="form1" runat="server">
   
<!--begin header-->
<div class="meiheader">
  <div class="askboxcon">
    <a href="quan.html"><span><em class="icon-fanhui"></em></span></a>
    <h2>优惠券领取成功</h2> 
     <span class="icon-tel"></span>
    </div>
</div>
<!--end header-->
<div class="quan-content-box quan-content">
  
   <div class="lingqu-icon"><p>您的优惠券领取成功</p></div>
    <div class="lingqu">
   <div class="quan-all-waper">
      <div class="quan-left-waper">
         <div class="quan-left-moneny"><em>￥</em>50</div>
      </div>
      <div class="quan-right-waper">
          <h2>星巴克优惠券</h2>
          <h3>满 60 元即可使用</h3>
          <p>有效期：2017/2/20 - 2017/3/31</p>
      </div>
</div>

  <p class="quanma-show">券码：<strong>3837 8493 2323</strong></p>
  <p>您可以通过的方式查找你的优惠券</p>
  <p>1、必须使用1000元才能使用100元代金券一张</p>
  <p>2、必须使用1000元才能使用100元代金券一张</p>
  <br/>
 </div>
 
  

 <div class="quanshuru lingqutop"> 
      <a href="index.html"/><div class="submit-btn">查看优惠券</div></div>
  </div>
</div>   
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script>

    //信息提示
    var tipsflag = true;
    function tips(text) {
        if (tipsflag == true) {
            var tishiDiv = document.createElement('div');
            tishiDiv.className = "motify";
            document.body.appendChild(tishiDiv);
            tipsflag = false;
        }
        $('.motify').html(text).show();
        setTimeout(function () { $('.motify').fadeOut(); }, 500);
    }
    var patrn = /^0?(13[0-9]|15[0-9]|17[0-9]|18[0-9]|14[0-9])[0-9]{8}$/;


    var flag = true;
    $('.submit-btn').on('click', function () {
        if (flag == true) {
            var iphone = document.getElementById('phone').value;
            if (iphone == "") {
                tips('<p>请输入优惠券券码！</p>')
            } else {

                tips('<p>恭喜您，验证成功</p>');

            }

        }
    })

</script>
 