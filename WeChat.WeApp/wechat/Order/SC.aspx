<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SC.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.SC" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>手工半湿粮优先试吃权—PETKIN宠物管家</title>
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
<meta content="yes" name="apple-mobile-web-app-capable" />
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta content="telephone=no" name="format-detection" />
<style type="text/css">
*{margin:0; padding:0;}
.main{width:100%; height:100%; max-width:640px; display:block; background:url(images/bg2.jpg) no-repeat center center; background-size:cover; position:absolute; left:0; top:0; z-index:999;}
.txtcon{width:300px; position:absolute; left:50%; margin-left:-150px; bottom:10px; height:175px;}
.txtcon p{text-align:center; color:#fff; font-size:16px; line-height:22px;}
.txtcon p strong{color:#ff8920; font-size:18px;}
.txtcon p span{width:65%; float:left; margin-right:10px;}
.txtcon p bdo{width:100%;float:left;text-align:center;}
.txtcon h2{font-size:20px; line-height:40px; color:#ff8920; text-align:center; padding:10px 0;}
.text{width:100%; border:none; background:#fff; border-radius:5px; height:40px; font-size:18px; text-indent:5px;}
.btn{width:93px; height:40px; background:url(images/btn.png) no-repeat 0 0; background-size:93px 40px; display:block; font-size:18px; color:#fff; font-weight:bold;margin:auto; } 
.shurubg{background:rgba(0, 0, 0, 0.7) none repeat scroll 0 0 !important; filter:Alpha(opacity=70); background:#000;position:fixed; left:0; top:0; width:100%;display:none; height:100%; z-index:999999; }
.shuruma{width:300px; background:#e94948; border-radius:5px; position:fixed; z-index:999999; left:50%; top:50%; margin-left:-150px; margin-top:-210px;}
.shuruma h2{font-size:18px; color:#fff; line-height:25px; display:block; text-align:center; padding:10px;}
.shuruma p img{text-align:center; margin:0 auto;}
.shuruma p{ padding:10px; color:#fff;}
.shuruma .shanchu{ background:url(images/shanchu.png) no-repeat center center; width:30px; height:30px; display:block; position:absolute; z-index:99999; right:-10px; top:-10px; background-size:30px;}
</style>
    <script src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/zepto.min.js"></script>
<script type="text/javascript" src="js/touch.js"></script>
        <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
        <script src="js/GetLocation.js?v=1"></script>
    <script src="js/Message2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
  <div class="txtcon">
    <p>PETKIN手工半湿粮是一个互联网产品，<br>
因为管家一直希望宝贝狗狗可以有<br/>
<strong>更好更健康的选择！</strong></p>
    <h2>外加1000包鲜粮试吃开抢啦&nbsp;<bdo  id="btnid"><input name="" type="button" class="btn" value="开抢" id="btn"></bdo></h2>
  </div>

</div>
<div class="shurubg" id="shuruma">
    <div class="shuruma">
      <div class="shanchu" id="delbtn"></div>
      <h2 id="tx">恭喜您，获得试吃包邮权
<br/>
            试吃活动19号正式上线</h2>
     <div align="center"><img src="images/erweima2.png" width="200"/></div>
     <p>注：试吃最新活动，更多营养互动<br/>
请关注我们管家鲜粮账号：petkin手工半湿粮</p>
    </div>
</div>
    </form>
</body>
</html>
<script type="text/javascript">
    $("#btnid").click(function () {
        location = "zhifu.aspx";
    });
    $('#btnid').on('tap', function () {
        //checked();
        location = "zhifu.aspx";
    })

    $('#delbtn').on('tap', function () {
        document.getElementById('shuruma').style.display = "none";
    })

</script>