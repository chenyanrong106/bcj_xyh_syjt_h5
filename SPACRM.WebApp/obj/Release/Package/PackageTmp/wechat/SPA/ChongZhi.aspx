<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChongZhi.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.ChongZhi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="小时光">
    <!-- uc强制竖屏 -->
    <meta name="screen-orientation" content="portrait">
    <!-- UC强制全屏 -->
    <meta name="full-screen" content="yes">
    <!-- UC应用模式 -->
    <meta name="browsermode" content="application">
    <!-- QQ强制竖屏 -->
    <meta name="x5-orientation" content="portrait">
    <!-- QQ强制全屏 -->
    <meta name="x5-fullscreen" content="true">
    <!-- QQ应用模式 -->
    <meta name="x5-page-mode" content="app">
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/index.css" />
    <title>小时光</title>
    <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="wo.aspx"><span class="icon-left"></span></a>
                <h2>我要充值</h2>
                <a href="javascript:void(0);"><span id="navRight" class="icon-lnk"></span></a>
            </div>

            <ul class="lnks">
                <li><a href="index.aspx"><span class="icon-logo-w"></span>我要预约</a></li>
                <li><a href="dingdan.aspx"><span class="icon-order_other"></span>我的订单</a></li>
                <li><a href="wo.aspx"><span class="icon-personal"></span>个人中心</a></li>
                <li><a href="tel:17621160808"><span class="icon-tel"></span>呼叫客服</a></li>
            </ul>
        </div>
        <!--end header-->



        <!--begin content-->
        <div class="meiWapper">
            <div class="order-content">
                <div class="chongzhi-total">
                    <%if (oa != null)
                      { %>
                    <div class="chongzhi-admin-yue">
                        <h2><span>余额：</span>￥<%=oa.SurplusMoney??0 %></h2>
                        <p>
                            <span>
                                <img src="<%=oa.headimgurl %>"></span><%=oa.Nickname %><br />
                            <%=string.IsNullOrEmpty(oa.Phone)?"&nbsp;":oa.Phone %>
                        </p>
                    </div>
                    <%} %>
                    <div class="mei-jinbi-guize chongzhi-jilu"><a href="czdetail.aspx" class="gold-guize">充值记录</a></div>

                </div>

            </div>
            <div class="chongzhi-list">
                <p>选择充值金额</p>
                <ul>
                    <li><span><strong>1000</strong>送100元</span></li>
                    <li><span><strong>2000</strong>送200元</span></li>
                    <li><span><strong>3000</strong>送500元</span></li>
                    <%--  <li><span><strong>5000</strong>送100元</span></li> 
               <li><span><strong>10000</strong>送100元</span></li>--%>
                    <%-- <li><span><strong>1</strong>送0.1元</span></li>--%>
                    <%--   <li><span><strong>0.1</strong>送0元</span></li>--%>
                </ul>
            </div>


            <div class="book-box">
                <ul class="order flex">
                    <li style="width: 100%; padding-left: 0;"><a class="btn-normal bookBtn" href="javascript:;" id="bookall">确定</a></li>
                </ul>
            </div>
        </div>
        <!--end content-->
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script src="js/Message2.js"></script>
<script>
    $('.chongzhi-list ul li').on('click', function () {
        $(this).addClass('xuanzhong').siblings().removeClass('xuanzhong');
    })

    $("#bookall").click(function () {
        if ($(".chongzhi-list li.xuanzhong strong").html()) {
            $("#bookall").hide();
            $.post("chongzhi.aspx?para=tj", {
                zf: "微信支付",//支付方式
                je: $(".chongzhi-list li.xuanzhong strong").html()//充值金额
            },
                               function (ret) {
                                   if (ret.st == -1) {
                                       $.MsgBox.Alert("小时光", ret.msg);
                                   }
                                   else if (ret.st == 1) {
                                       $.MsgBox.Alert("小时光", ret.err_code);
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
                                               if (res.err_msg == "get_brand_wcpay_request：ok") { $.MsgBox.Alert("小时光", "支付成功"); }
                                           },
                                           fail: function (ret) {
                                               // alert(ret.err_code);
                                           },
                                           complete: function (ret) {
                                               // alert(ret.errMsg);
                                               if (ret.errMsg == "chooseWXPay:ok") {
                                                   $.MsgBox.Alert("小时光", "支付成功", function () {
                                                       WeixinJSBridge.invoke('closeWindow', {}, function (res) {

                                                       });
                                                   });
                                               }
                                               else if (ret.errMsg == "chooseWXPay:cancel") {
                                                   $.MsgBox.Alert("小时光", "支付已取消", function () {
                                                       //  $.post("zhifu3.aspx?para=qxyq", {
                                                       //      yhqid: $("#yhqid").val()
                                                       //  },
                                                       //      function (ret) {

                                                       //      },
                                                       //      "json"
                                                       //);
                                                   });
                                               }
                                               else { $.MsgBox.Alert("小时光", "网络异常，请刷新后重试"); }
                                           }
                                       });
                                   }
                                   $("#bookall").show();
                               },
                               "json"
                         );
        }
        else {
            alert("请选择需要充值的金额");
        }
    });
</script>
