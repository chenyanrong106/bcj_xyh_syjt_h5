<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login2.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Login2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8">
<title>申请</title>
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /> 
<meta content="yes" name="apple-mobile-web-app-capable" /> 
<meta content="black" name="apple-mobile-web-app-status-bar-style" /> 
<meta content="telephone=no" name="format-detection" />
<link type="text/css" rel="stylesheet" href="css/meike.css" />
    <script src="js/jquery.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/GetLocation.js?v=1.0"></script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="loginBox">
   <div class="logoImg"></div> 
   <img src="images/chongwu.png" class="img01 quananimate">
   <img src="images/aiwujiang.png" class="img02">
   <div class="logintxt"><p>战略合作：宠物管家、宠物帮领养中心、福贝宠粮</p></div>
   <img src="images/anniu.png" id="fq" class="img03 quananimate2">
</div>
         <input type="hidden" id="ztitle" value="帮助可爱的毛孩子们吃上饱饭-发起筹粮" />
        <input type="hidden" id="ftitle" value="此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办" />
    </form>
</body>
</html>
<script>
    $("#fq").click(function () {
        location = "sq4.aspx";
    });
</script>