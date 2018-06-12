<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Share.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Share" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<title>支付提示—小宠回家</title>
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
<meta content="yes" name="apple-mobile-web-app-capable" />
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta content="telephone=no" name="format-detection" />
<style type="text/css">
*{margin:0; padding:0;}
body{background:#f2f2f2; margin:0; padding:0;width:100%;height:100%;overflow-x:hidden;-webkit-user-select:none;-webkit-text-size-adjust:none;}
.share{width:100%; height:100%; max-width:640px; display:block; background:url(images/jianou.png) no-repeat 90% 20px #333; background-size:70px 47px; position:absolute; left:0; top:0; z-index:999;}
.sharebox{position:absolute; top:90px; left:50%; width:296px; margin-left:-150px; display:block; z-index:999; border:2px dashed #fff; height:200px; border-radius:10px;}
.sharebox p{ padding:10px; font-size:18px; color:#fff; line-height:30px; background:url(images/liulanqi.jpg) no-repeat center bottom; padding-bottom:90px; background-size:280px 82px;}
</style>
    <script src="js/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="share">
    <div class="sharebox" style="display:none;">
      <p>因为微信对支付宝的屏蔽，点击右上角的分享，请在菜单中选择浏览器打开，完成支付！</p>   
    
    </div>
  
  
</div>
        <input type="hidden" runat="server" id="url" />
    </form>
</body>
</html>
<script type="text/javascript">
    if (is_weixin()) {
        $(".sharebox").show();
    }
    else {
        location = $("#url").val();
        WeixinJSBridge.invoke('closeWindow', {}, function (res) {

        });
    }
    function is_weixin() {
        var ua = navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == "micromessenger") {
            return true;
        } else {
            return false;
        }
    }

    function getQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return r[2]; return null;
    }
</script>