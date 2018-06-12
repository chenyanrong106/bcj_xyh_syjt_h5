<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>爱宠筹</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/meike.css" />
    <script src="js/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pays-info">
            <h2>支付未完成</h2>
            <h6>长按识别下面的二维码，继续微信支付</h6>

            <div class="shoukuanfang">
                <p>
                    <img src="http://SPACRM.meijiewd.com/assets/images/logo.jpg">待支付金额：<strong><%=je %></strong><br />
                    收款方：爱宠筹 - 为爱筹
                </p>
            </div>

            <div class="weimaBox">
                <p>
                    <img src="MakeQRCode.aspx?data=<%=url %>">
                </p>
                <p style="width: 166px; margin: 0 auto; height: 30px; padding-left: 40px;" id="showbtn"><bdo>遇到跨号支付问题</bdo><bdo class="gantan"></bdo></p>
            </div>

            <div class="payinfoss">
                <p>由于微信限制不能跨号支付，尝试其他方法</p>
                <p>方法1：请将活动链接转发给自己点击链接完成操作</p>
                <p>方法2：关注账号“爱宠筹”参与筹粮活动</p>
            </div>

            <div class="maskbg" id="showbox">
                <div class="shuomingBox">
                    <h3>跨号支付问题</h3>
                    <p>
                        因为我们主要推广订阅号，但订阅号无法开通微信支付，于是我们用服务号开通了微信支付，并在wap商城里接入了微信支付。结果通过订阅号访问wap商城通过微信支付时，就提示如下图的 不允许跨号支付！！
要是通过服务号就没有问题！
                    </p>
                    <div style="padding: 10px;">
                        <div class="paybtn" id="zhidaoBtn">我知道了</div>
                    </div>
                </div>
            </div>
            <input type="hidden" id="orderno" value="<%=orderno %>" />
            <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />
            <input type="hidden" id="iswx" runat="server" value="1" />
            <input type="hidden" id="cid"  value="<%=Request.QueryString["cid"]??"0" %>" />
    </form>
</body>
</html>
<script type="text/javascript">
    document.getElementById('showbtn').onclick = function () {
        document.getElementById('showbox').style.display = 'block';
    }

    document.getElementById('zhidaoBtn').onclick = function () {
        document.getElementById('showbox').style.display = 'none';
    }

    function jc() {
        $.post("test.aspx?para=tj&orderno=" + $("#orderno").val(), {

        },
                               function (ret) {
                                   if (ret.st > 0) {
                                       if ($("#cid").val() == 0) {
                                           window.location = "chou.aspx";
                                       }
                                       else {
                                           window.location = "../cat/qindex.aspx?id=" + $("#cid").val();
                                       }
                                   }
                                   else {
                                       setTimeout("jc()", 1000);
                                   }

                               },
                               "json"
                         );
    }

    jc();


  
</script>
