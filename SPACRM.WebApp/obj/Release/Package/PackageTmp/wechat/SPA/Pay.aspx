<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Pay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="小时光Massage">
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
    <title>小时光Massage</title>
    <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="dingdan.aspx"><span class="icon-left"></span></a>
                <h2>订单支付</h2>
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
                <div class="pay-order-info">
                    <%if (order != null)
                      { %>
                    <p><strong>订单号：</strong> <%=order.ORDER_NO %></p>
                    <p><strong>支付金额：</strong> <bdo>￥<%=order.PAY_AMT %></bdo></p>
                    <%} %>
                </div>

                <div class="pay-order-info pay-order-paylei">
                    <h2>选择支付方式</h2>
                    <p><span><i class="icon-radio-f" id="kaka"></i></span><strong><i class="kaka"></i>储值卡</strong> 余额：<%=oa.SurplusMoney %> <bdo class="chongzhi" onclick="window.location='chongzhi.aspx'">充值</bdo></p>

                </div>

                <div class="pay-order-info pay-order-paylei">
                    <p name="kx"><span><i class="icon-radio-f" id="weixin"></i></span><strong><i class="weixin"></i>微信支付</strong> </p>
                    <p name="kx"><span><i class="icon-radio" id="zfb"></i></span><strong><i class="zhifu"></i>支付宝支付</strong> </p>
                </div>


            </div>
            <!--end content-->
            <%if (order != null)
              { %>
            <%-- <%if(order.OverdueDate>DateTime.Now){ %>--%>
            <div class="book-box">
                <ul class="order flex payend">
                    <li class="orderNum">支付剩余时间：</li>

                    <li id="shengyuTime">0:0:0</li>

                    <li><a class="btn-normal bookBtn" href="javascript:;" id="bookall">去支付</a></li>
                </ul>
            </div>
            <%--<%} %>--%>
            <input type="hidden" id="vtime" value="<%=order.OverdueDate.Value.ToString("yyyy-MM-dd HH:mm:ss") %>" />
            <input type="hidden" id="oid" value="<%=Request.QueryString["oid"] %>" />
            <%} %>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js?v=1.2"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script src="js/Message2.js"></script>
<script>
    //支付倒计时
    function zhifuTime() {
        var interval = 1000;
        function ShowCountDown(time, divname) {
            var now = new Date();
            var arr = time.split(/[- : \/]/);
            var endDate = new Date(arr[0], arr[1] - 1, arr[2], arr[3], arr[4], arr[5]);
            //alert(endDate.getTime());
            if (endDate > now.getTime()) {
                var leftTime = endDate.getTime() - now.getTime();
                var leftsecond = parseInt(leftTime / 1000);
                //var day1=parseInt(leftsecond/(24*60*60*6)); 
                var day1 = Math.floor(leftsecond / (60 * 60 * 24));
                var hour = Math.floor((leftsecond - day1 * 24 * 60 * 60) / 3600);
                var minute = Math.floor((leftsecond - day1 * 24 * 60 * 60 - hour * 3600) / 60);
                var second = Math.floor(leftsecond - day1 * 24 * 60 * 60 - hour * 3600 - minute * 60);
                var cc = $(divname);
                cc.html(hour + ":" + minute + ":" + second);
            } else {
                window.location = "dingdan.aspx";
                var cc = $(divname);
                cc.html(0 + ":" + 0 + ":" + 0);
            }
        }
        window.setInterval(function () { ShowCountDown($("#vtime").val(), $("#shengyuTime")) }, interval);

    }
    zhifuTime();



    $('.pay-order-paylei p[name="kx"]').find('.icon-radio,.icon-radio-f').each(function (index, element) {

        $(this).on('click', function () {
            $('.pay-order-paylei p[name="kx"] span i').removeClass('icon-radio-f').addClass('icon-radio');
            if ($(this).hasClass('icon-radio')) {
                $(this).removeClass('icon-radio').addClass('icon-radio-f');
            } else {
                $(this).removeClass('icon-radio-f').addClass('icon-radio');
            }

        });

    })

    $("#bookall").click(function () {
        if ($(".pay-order-info.pay-order-paylei i.icon-radio-f").attr("id")) {
            //alert($(".pay-order-info.pay-order-paylei i.icon-radio-f").attr("id"));
            $("#bookall").hide();
            $.post("pay.aspx?para=tj", {
                zf: $(".pay-order-info.pay-order-paylei  p[name='kx'] i.icon-radio-f").attr("id"),//支付方式
                oid: $("#oid").val()
            },
                                 function (ret) {
                                     $("#bookall").show();
                                     if (ret.st == -1) {
                                         $.MsgBox.Alert("小时光", ret.msg);
                                     }
                                     else if (ret.st == 1) {
                                         $.MsgBox.Alert("小时光", ret.err_code);
                                     }
                                     else if (ret.st == 9) {
                                         $.MsgBox.Alert("小时光", ret.err_code, function () { location = "dingdan.aspx";});
                                     }
                                     else if (ret.st == 2) {
                                         location = "../order/Share.aspx?zfurl=" + ret.err_code;
                                     }
                                     else if (ret.st == 5) {
                                         window.location = "Test.aspx?iid=" + $("#iid").val() + "&txtnum=" + $("#txtnum").val() + "&remark=" + $("#remark").val() + "&p=" + $("#urlpara").val() + "&iswx=" + ((isWeiXin() ? 1 : 0));
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
                                             fail: function (ref) {
                                                 // alert(ret.err_code);
                                             },
                                             complete: function (rec) {
                                                 // alert(ret.errMsg);
                                                 if (rec.errMsg == "chooseWXPay:ok") {
                                                     $.MsgBox.Alert("小时光", "支付成功", function () {
                                                         //WeixinJSBridge.invoke('closeWindow', {}, function (res) {

                                                         //});
                                                         //window.location = "chou.aspx";
                                                         window.location = ret.returnurl;// "chou.aspx";
                                                     });
                                                 }
                                                 else if (rec.errMsg == "chooseWXPay:cancel") {
                                                     $.MsgBox.Alert("小时光", "支付已取消", function () {
                                                     });
                                                 }
                                                 else {
                                                     $.MsgBox.Alert("小时光", rec.errMsg, function () {
                                                     });
                                                     //window.location = "Test.aspx?iid=" + $("#iid").val() + "&txtnum=" + $("#txtnum").val() + "&remark=" + $("#remark").val() + "&p=" + $("#urlpara").val() + "&iswx=" + ((isWeiXin() ? 1 : 0));
                                                     //$.MsgBox.Alert("小时光", "由于微信限制不能跨号支付，方法1：请将此页面转发给自己点击链接完成操作。方法2：关注账号“小时光”参与筹粮活动");
                                                 }
                                             }
                                         });
                                     }
                                 },
                                 "json"
                           );
        }
        else {
            alert("请选择支付方式");
        }
    });

</script>
