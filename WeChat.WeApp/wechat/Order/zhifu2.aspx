<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zhifu2.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.zhifu2" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8">
<title>手工湿粮优先试吃权—PETKIN宠物管家</title>
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
<meta content="yes" name="apple-mobile-web-app-capable" />
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta content="telephone=no" name="format-detection" />
<style type="text/css">
*{margin:0; padding:0;}
body{background:#f2f2f2; margin:0; padding:0;width:100%;height:100%;overflow-x:hidden;-webkit-user-select:none;-webkit-text-size-adjust:none;}
.main{width:100%; height:100%; max-width:640px; display:block; background:#fff; background-size:cover; position:absolute; left:0; top:0; z-index:999;}
.address{  display:block; padding:15px 20px; background:#fff;background:url(images/jiantou.png) no-repeat 95% center #fff; background-size:15px; margin-bottom:10px;}
.address p{background:url(images/dingwei.png) no-repeat 0 center; background-size:20px 26px;line-height:22px; padding-left:30px; color:#999;}
.address p a{color:#999; font-size:14px; text-decoration:none; display:block;}
a.blue:link,a.blue:visited{color:#0066ff; text-decoration:none;} a.blue:hover{color:#ff0000; text-decoration:none;}
.orderbox{padding:5px 10px; background:#fff; margin-bottom:10px;}
.orderbox ul li{border-bottom:1px solid #eee; padding:10px; text-align:left; line-height:30px; font-size:16px; color:#666;}
.orderbox ul li h2{font-size:18px; color:#f38a02; display:block; font-weight:normal; font-family:Arial, Helvetica, sans-serif;}
.orderbox ul li span{float:right;}
.orderbox ul li em{width:30px; height:30px; background:#ddd; line-height:30px; text-align:center; font-style:normal; display:inline-block; font-size:30px;float:left;}
.orderbox ul li em a{display:block; color:#333; text-decoration:none;-webkit-touch-callout: none;}
.orderbox .shutxt{width:50px; height:28px; border:1px solid #ddd; border-radius:0; background:none; color:#666; -webkit-appearance:none; display:inline-block; float:left; text-align:center; font-size:18px;}
.dropdown-select {width: 100%;margin: 0;padding:0;height:30px;line-height:30px;font-size: 16px;color: #999;border: 0; text-align:left;background:none;border-radius: 0;background:transparent;
  -webkit-appearance: none;}
.zhifubtn{ background:url(images/order.png) no-repeat 30px 25px #fff; background-size:60px; padding:10px; overflow:hidden; display:block;}
.zhifubtn ol{ padding-left:120px; height:30px; color:#666; line-height:30px; font-size:16px;}  
.zhifubtn ol span{font-size:16px; font-family:Arial, Helvetica, sans-serif; float:right; color:#ff0000; padding-right:5px;}
.zhifubtn ol strong{font-size:24px;}
.zhifubtn ol em{font-style:normal;}
.weixin{ width:80px; height:32px; border:1px solid #64cd32; display:inline-block;font-size:14px; color:#41a910; margin-left:10px; line-height:32px; text-align:center; border-radius:5px;}
.curzhi{background:#64cd32; display:inline-block; color:#fff; font-weight:bold;}
.btn{ width:100%; height:44px; border-radius:5px; background:#f38a02; font-size:18px; color:#fff; text-align:center; line-height:44px; font-weight:bold; margin:20px 0;}
</style>
     <script src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/zepto.min.js"></script>
<script type="text/javascript" src="js/touch.js"></script>
        <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
        <script src="js/GetLocation.js?v=1.0"></script>
    <script src="js/Message2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="address">
   <p runat="server" id="addinfo"><bdo class="zuobiao"></bdo><a href="address.aspx">您还没有默认的收货信息<br/>
    立即新增</a></p>
</div>

<div class="orderbox">
  <ul>
     <li><h2>PETKIN 手工湿粮（100g）*3包</h2></li>
     <li style="display:none;"><span><em><a href="javascript:jian();">-</a></em><input name="" type="number" class="shutxt" size="6" value="1" id="carnum"><em><a href="javascript:add();">+</a></em></span>数量</li>
     <li style="display:none;"><span><select name="" class="dropdown-select"><option>请选择优惠券</option></select></span>优惠</li>
     <li style="border-bottom:none;" id="zhifu"><span><bdo class="weixin curzhi">微信支付</bdo> <bdo class="weixin">支付宝支付</bdo></span>支付方式</li>
  </ul>
</div>

<div class="zhifubtn" id="price">
  <ol><span>￥<bdo>0</bdo> × <em>1</em></span>湿粮金额</ol>
  <ol><span>￥<bdo runat="server" id="yh">0.00</bdo></span>优惠金额</ol>
  <ol><span style="color:#999;">￥<bdo>8.00</bdo></span>邮费</ol>
  <br>
  <ol><span><strong>￥<bdo></bdo></strong></span>应付金额</ol>
    

  <div class="btn" id="tjdd" runat="server">提交订单</div>
    <div class="btn" id="Div1" runat="server" visible="false">湿粮已抢光</div>
    <div style="color:red;font-size:small;text-align:center;"> (此链接仅限品牌代理推荐试吃通道，他人付款无效，每人仅限购买一份。)</div>
</div>




    <input type="hidden" id="isadd" runat="server" value="0" />
    <input type="hidden" id="sjjg" runat="server" value="10" />
    </form>
</body>
</html>
<script type="text/javascript">
    var num = 1;
    function jian() {
        num = parseInt(document.getElementById("carnum").value);
        if (num > 1) {
            num = num - 1;
            document.getElementById("carnum").value = num;
        } else {
            document.getElementById("carnum").value = 1;
        }
        chengji();
    }
    function add() {
        num = parseInt(document.getElementById("carnum").value);
        num = num + 1;
        document.getElementById("carnum").value = num;
        chengji();
    }

    var zhibdo = document.getElementById('zhifu').getElementsByTagName('bdo');
    zhibdo[0].onclick = function () {
        zhibdo[0].className = 'weixin curzhi';
        zhibdo[1].className = 'weixin';
    }
    zhibdo[1].onclick = function () {
        zhibdo[1].className = 'weixin curzhi';
        zhibdo[0].className = 'weixin';
    }

    var socre = 0;
    window.onload = function () {
        chengji();
    }
    function chengji() {
        var jiage = document.getElementById('price').getElementsByTagName('bdo');
        document.getElementById('price').getElementsByTagName('em')[0].innerHTML = num;
        socre = parseFloat(jiage[0].innerHTML) * parseInt(num) - parseFloat(jiage[1].innerHTML) + parseFloat(jiage[2].innerHTML);
        jiage[3].innerHTML = socre.toFixed(2);
    }

    $("#tjdd").click(function () {
        if ($("#isadd").val() == "0") {
            $.MsgBox.Alert("宠物管家", "请填写收货地址");
        }
        else {
            $.post("zhifu2.aspx?para=tj", {
                zf: $("#zhifu").find($("bdo.curzhi")).html()
            },
                                function (ret) {
                                    if (ret.st == -1) {
                                        $.MsgBox.Alert("宠物管家", ret.msg);
                                    }
                                    else if (ret.st == 1) {
                                        $.MsgBox.Alert("宠物管家", ret.err_code);
                                    }
                                    else if (ret.st == 2) {
                                        location = "Share.aspx?zfurl=" + ret.err_code;
                                    }
                                    else {
                                        wx.chooseWXPay({
                                            appid: ret.appId,
                                            timestamp: ret.timeStamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                                            nonceStr: ret.nonceStr, // 支付签名随机串，不长于 32 位
                                            package: ret.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                                            signType: ret.signType, // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                                            paySign: ret.paySign, // 支付签名
                                            success: function (res) {
                                                // 支付成功后的回调函数
                                                if (res.err_msg == "get_brand_wcpay_request：ok") { $.MsgBox.Alert("宠物管家", "支付成功"); }
                                            },
                                            fail: function (ret) {
                                                // alert(ret.err_code);
                                            },
                                            complete: function (ret) {
                                                // alert(ret.errMsg);
                                                if (ret.errMsg == "chooseWXPay:ok") {
                                                    $.MsgBox.Alert("宠物管家", "支付成功", function () {
                                                        WeixinJSBridge.invoke('closeWindow', {}, function (res) {

                                                        });
                                                    });
                                                }
                                                else if (ret.errMsg == "chooseWXPay:cancel") {
                                                    $.MsgBox.Alert("宠物管家", "支付已取消");
                                                }
                                                else { $.MsgBox.Alert("宠物管家", ret.errMsg); }
                                            }
                                        });
                                    }
                                },
                                "json"
                          );
        }
    });
</script>