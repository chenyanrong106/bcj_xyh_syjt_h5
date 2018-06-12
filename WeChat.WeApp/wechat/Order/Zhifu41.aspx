<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhifu41.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Zhifu41" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>宠物管家手工粮招募VIP合伙人</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        body {
            background: #f2f2f2;
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            overflow-x: hidden;
            -webkit-user-select: none;
            -webkit-text-size-adjust: none;
        }

        .main {
            width: 100%;
            height: 100%;
            max-width: 640px;
            display: block;
            background: #fff;
            background-size: cover;
            position: absolute;
            left: 0;
            top: 0;
            z-index: 999;
        }

        .address {
            display: block;
            padding: 15px 20px;
            background: #fff;
            background: url(images/jiantou.png) no-repeat 95% center #fff;
            background-size: 15px;
            margin-bottom: 10px;
        }

            .address p {
                background: url(images/dingwei.png) no-repeat 0 center;
                background-size: 20px 26px;
                line-height: 22px;
                padding-left: 30px;
                color: #999;
            }

                .address p a {
                    color: #999;
                    font-size: 14px;
                    text-decoration: none;
                    display: block;
                }

        a.blue:link, a.blue:visited {
            color: #0066ff;
            text-decoration: none;
        }

        a.blue:hover {
            color: #ff0000;
            text-decoration: none;
        }

        .orderbox {
            padding: 5px 10px;
            background: #fff;
            margin-bottom: 10px;
        }

            .orderbox ul li {
                border-bottom: 1px solid #eee;
                padding: 10px;
                text-align: left;
                line-height: 30px;
                font-size: 16px;
                color: #666;
            }

                .orderbox ul li h2 {
                    font-size: 18px;
                    color: #f38a02;
                    display: block;
                    font-weight: normal;
                    font-family: Arial, Helvetica, sans-serif;
                }

                .orderbox ul li p {
                    line-height: 22px;
                    font-size: 14px;
                    color: #666;
                }

                    .orderbox ul li p strong {
                        font-family: Arial, Helvetica, sans-serif;
                        color: #ff0000;
                        font-weight: normal;
                        font-size: 16px;
                    }

                    .orderbox ul li p bdo {
                        display: block;
                        color: #999;
                        line-height: 20px;
                        font-family: Arial, Helvetica, sans-serif;
                        font-size: 12px;
                    }

                    .orderbox ul li p b {
                        font-size: 16px;
                    }

                    .orderbox ul li p i {
                        font-size: 14px;
                        color: #666;
                        font-style: normal;
                        font-family: Arial, Helvetica, sans-serif;
                    }

                .orderbox ul li span {
                    float: right;
                }

                .orderbox ul li em {
                    width: 30px;
                    height: 30px;
                    background: #ddd;
                    line-height: 30px;
                    text-align: center;
                    font-style: normal;
                    display: inline-block;
                    font-size: 30px;
                    float: left;
                }

                    .orderbox ul li em a {
                        display: block;
                        color: #333;
                        text-decoration: none;
                        -webkit-touch-callout: none;
                    }

            .orderbox .shutxt {
                width: 50px;
                height: 28px;
                border: 1px solid #ddd;
                border-radius: 0;
                background: none;
                color: #666;
                -webkit-appearance: none;
                display: inline-block;
                float: left;
                text-align: center;
                font-size: 18px;
            }

        .dropdown-select {
            width: 100%;
            margin: 0;
            padding: 0;
            height: 30px;
            line-height: 30px;
            font-size: 16px;
            color: #999;
            border: 0;
            text-align: left;
            background: none;
            border-radius: 0;
            background: transparent;
            -webkit-appearance: none;
        }

        .zhifubtn {
            background: url(images/order.png) no-repeat 30px 25px #fff;
            background-size: 60px;
            padding: 10px;
            overflow: hidden;
            display: block;
        }

            .zhifubtn ol {
                padding-left: 120px;
                height: 30px;
                color: #666;
                line-height: 30px;
                font-size: 16px;
            }

                .zhifubtn ol span {
                    font-size: 16px;
                    font-family: Arial, Helvetica, sans-serif;
                    float: right;
                    color: #ff0000;
                    padding-right: 5px;
                }

                .zhifubtn ol strong {
                    font-size: 24px;
                }

                .zhifubtn ol em {
                    font-style: normal;
                }

        .weixin {
            width: 80px;
            height: 32px;
            border: 1px solid #64cd32;
            display: inline-block;
            font-size: 14px;
            color: #41a910;
            margin-left: 10px;
            line-height: 32px;
            text-align: center;
            border-radius: 5px;
        }

        .curzhi {
            background: #64cd32;
            display: inline-block;
            color: #fff;
            font-weight: bold;
        }

        .btn {
            width: 100%;
            height: 44px;
            border-radius: 5px;
            background: #f38a02;
            font-size: 18px;
            color: #fff;
            text-align: center;
            line-height: 44px;
            font-weight: bold;
            margin: 20px 0;
        }

        .btnphone {
            width: 60px;
            height: 41px;
            background: #f90;
            color: #fff;
            border: none;
            -webkit-appearance: none;
            font-size: 16px;
            margin: 0;
            padding: 0;
            border-radius: 0 5px 5px 0;
        }

        .shurubox {
            border: 1px solid #f90;
            border-radius: 8px;
            height: 40px;
            line-height: 40px;
            display: block;
            margin: 5px;
            overflow: hidden;
        }

        .zhifubox {
            padding: 10px;
            text-align: left;
            line-height: 30px;
            font-size: 16px;
            color: #666;
        }

            .zhifubox span, .shurubox span {
                float: right;
            }

        ::-webkit-input-placeholder {
            color: #f00;
        }
    </style>
    <script src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/zepto.min.js"></script>
    <script type="text/javascript" src="js/touch.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/GetLocation.js?v=1.0"></script>
    <script src="js/Message2.js"></script>
    <script src="../../assets/plugins/pulsate/jQuery.pulsate.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="address">
            <p runat="server" id="addinfo">
                <bdo class="zuobiao"></bdo><a href="address41.aspx">您还没有默认的收货信息<br />
                    立即新增</a>
            </p>
        </div>

        <div class="orderbox">
            <ul>
                <li>
                    <p><span><em><a href="javascript:jian(1);">-</a></em><input name="" type="number" class="shutxt" size="6" value="0" id="carnum1"><em><a href="javascript:add(1);">+</a></em></span><b>1999元VIP</b><bdo runat="server" id="me1">15个名额</bdo></p>
                    <p id="danpin1"><span>小计：<strong>0元</strong></span>单价：<i>1999</i></p>
                </li>
                <li>
                    <p><span><em><a href="javascript:jian(2);">-</a></em><input name="" type="number" class="shutxt" size="6" value="0" id="carnum2"><em><a href="javascript:add(2);">+</a></em></span><b>999元VIP</b><bdo runat="server" id="me2">35个名额</bdo></p>
                    <p id="danpin2"><span>小计：<strong>0元</strong></span>单价：<i>999</i></p>
                </li>
                <li>
                    <p><span><em><a href="javascript:jian(3);">-</a></em><input name="" type="number" class="shutxt" size="6" value="0" id="carnum3"><em><a href="javascript:add(3);">+</a></em></span><b>99元VIP</b><bdo runat="server" id="me3">100个名额</bdo></p>
                    <p id="danpin3"><span>小计：<strong>0元</strong></span>单价：<i>99</i></p>
                </li>
            </ul>
        </div>
        <div class="orderbox">
            <ul>
                <%-- <li><span>
                    <input name="" type="button" class="btnphone" value="确认使用"></span><input name="" type="number" class="shutxt" id="yhq" style="width: 70%; text-align: left; font-size: 16px;" placeholder="请输入手机号码优惠凭证" onchange="change(this);" onkeydown="change(this);" onkeyup="change(this);"></li>--%>

                <div class="shurubox" style="display: none;">
                    <span>
                        <input name="" type="button" class="btnphone" value="使用"></span><input name="" type="number" class="shutxt" id="yhq" style="width: 70%; text-indent: 10px; line-height: 40px; height: 40px; text-align: left; font-size: 16px; border: none;" placeholder="请输入手机号码优惠凭证" onchange="change(this);" onkeydown="change(this);" onkeyup="change(this);">
                </div>

                <input type="hidden" id="yhqid" value="0" />
                <li style="border-bottom: none;" id="zhifu"><span><bdo class="weixin curzhi">微信支付</bdo> <bdo class="weixin">支付宝支付</bdo></span>支付方式</li>
            </ul>
        </div>
        <div class="zhifubtn" id="price">
            <ol><span>￥<bdo runat="server" id="dj">0</bdo>  <em id="sl" style="display: none;">1</em></span>VIP金额</ol>
            <ol><span>￥<bdo runat="server" id="yh">0.00</bdo></span>优惠金额</ol>
            <ol><span style="color: #999;">￥<bdo id="yf" runat="server">0.00</bdo></span>邮费</ol>
            <br>
            <ol><span><strong>￥<bdo id="sj"></bdo></strong></span>应付金额</ol>


            <div class="btn" id="tjdd" runat="server">提交订单</div>
            <div class="btn" id="Div1" runat="server" visible="false">4000份半湿粮已抢光</div>
            <div style="color: gray; font-size: small; text-align: center;">(如遇支付不了问题，请加大管家微信“a_miaom”)</div>
        </div>




        <input type="hidden" id="isadd" runat="server" value="0" />
        <input type="hidden" id="sjjg" runat="server" value="10" />
        <input type="hidden" id="cnum1" runat="server" value="0" />
        <input type="hidden" id="cnum2" runat="server" value="0" />
        <input type="hidden" id="cnum3" runat="server" value="0" />
    </form>
</body>
</html>
<script type="text/javascript">

    var num = 0;
    function jian(id) {
        num = parseInt($("#carnum" + id).val());
        if (num > 1) {
            num = num - 1;
            $("#carnum" + id).val(num);
        } else {
            num = 0;
            $("#carnum" + id).val(0);
        }
        var danpinzong = document.getElementById('danpin' + id).getElementsByTagName('strong')[0];
        if (id == 1) {
            danpinzong.innerHTML = 1999 * num + '元';
        }
        else if (id == 2) {
            danpinzong.innerHTML = 999 * num + '元';
        }
        else {
            danpinzong.innerHTML = 99 * num + '元';
        }
        chengji();
    }
    function add(id) {
        $("#carnum" + 1).val(0);
        $("#carnum" + 2).val(0);
        $("#carnum" + 3).val(0);

        document.getElementById('danpin' + 1).getElementsByTagName('strong')[0].innerHTML = '0元';
        document.getElementById('danpin' + 2).getElementsByTagName('strong')[0].innerHTML = '0元';
        document.getElementById('danpin' + 3).getElementsByTagName('strong')[0].innerHTML = '0元';
        if ($("#cnum" + id).val() == "0") {
            $.MsgBox.Alert("宠物半湿粮", "该名额已抢完");
        } else {
            num = parseInt($("#carnum" + id).val());
            if (num < 1)
                num = num + 1;
            else
                num = 1;
            $("#carnum" + id).val(num);
            var danpinzong = document.getElementById('danpin' + id).getElementsByTagName('strong')[0];
            if (id == 1) {
                danpinzong.innerHTML = 1999 * num + '元';
            }
            else if (id == 2) {
                danpinzong.innerHTML = 999 * num + '元';
            }
            else {
                danpinzong.innerHTML = 99 * num + '元';
            }

        }
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
        $("#dj").html($("#carnum1").val() * 1999 + $("#carnum2").val() * 999 + $("#carnum3").val() * 99);
        var jiage = document.getElementById('price').getElementsByTagName('bdo');
        document.getElementById('price').getElementsByTagName('em')[0].innerHTML = parseInt($("#carnum1").val()) + parseInt($("#carnum2").val()) + parseInt($("#carnum3").val());
        socre = parseFloat(jiage[0].innerHTML) * parseInt(1) + parseFloat(jiage[2].innerHTML);

        if ($("#yhqid").val() == "0") {
            $("#yh").html("0.00");
            jiage[3].innerHTML = socre.toFixed(2);
        }
        else {
            $("#yh").html((socre * 0.2).toFixed(2));
            jiage[3].innerHTML = (socre - parseFloat(jiage[1].innerHTML)).toFixed(2);
        }


    }

    $("#tjdd").click(function () {
        if ($("#isadd").val() == "0") {
            $.MsgBox.Alert("宠物半湿粮", "请填写收货地址");
        }
        else {
            $.post("zhifu41.aspx?para=tj", {
                zf: $("#zhifu").find($("bdo.curzhi")).html(),//支付方式
                dj: $("#dj").html(),                          //单价
                sl: $("#sl").html(),                          //数量
                yh: $("#yh").html(),                           //优惠
                yf: $("#yf").html(),                            //邮费
                sj: $("#sj").html(),                             //实付
                carnum1: $("#carnum1").val(),                   //第一种产品数量
                carnum2: $("#carnum2").val(),
                carnum3: $("#carnum3").val(),
                yhqid: $("#yhqid").val()
            },
                                 function (ret) {
                                     if (ret.st == -1) {
                                         $.MsgBox.Alert("宠物半湿粮", ret.msg);
                                     }
                                     else if (ret.st == 1) {
                                         $.MsgBox.Alert("宠物半湿粮", ret.err_code);
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
                                                 if (res.err_msg == "get_brand_wcpay_request：ok") { $.MsgBox.Alert("宠物半湿粮", "支付成功"); }
                                             },
                                             fail: function (ret) {
                                                 // alert(ret.err_code);
                                             },
                                             complete: function (ret) {
                                                 // alert(ret.errMsg);
                                                 if (ret.errMsg == "chooseWXPay:ok") {
                                                     $.MsgBox.Alert("宠物半湿粮", "支付成功", function () {
                                                         WeixinJSBridge.invoke('closeWindow', {}, function (res) {

                                                         });
                                                     });
                                                 }
                                                 else if (ret.errMsg == "chooseWXPay:cancel") {
                                                     $.MsgBox.Alert("宠物半湿粮", "支付已取消", function () {
                                                         //  $.post("zhifu3.aspx?para=qxyq", {
                                                         //      yhqid: $("#yhqid").val()
                                                         //  },
                                                         //      function (ret) {

                                                         //      },
                                                         //      "json"
                                                         //);
                                                     });
                                                 }
                                                 else { $.MsgBox.Alert("宠物半湿粮", ret.errMsg); }
                                             }
                                         });
                                     }
                                 },
                                 "json"
                           );
        }
    });

    $(".btnphone").click(function () {
        if ($("#yhq").val().length != 11) {
            $.MsgBox.Alert("宠物半湿粮", "请输入优惠券");
        }
        else {
            $.post("zhifu3.aspx?para=yq", {
                qh: $("#yhq").val()
            },
                                 function (ret) {
                                     if (ret.st == 0) {
                                         $.MsgBox.Alert("宠物半湿粮", "恭喜您，此优惠券可以使用。", function () {
                                             $("#yhqid").val(ret.err_code);
                                             chengji();
                                         });
                                     }
                                     else if (ret.st == 1) {
                                         $.MsgBox.Alert("宠物半湿粮", ret.err_code, function () {
                                             $("#yhqid").val(0);
                                             chengji();
                                         });
                                     }
                                     $(".btnphone").css("background", "#f90");
                                 },
                                 "json"
                           );
        }
    });

    function change(obj) {
        if ($(obj).val().length == 11) {
            $(".btnphone").css("background", "red").pulsate({ color: "red" });
        }
        else {
            $(".btnphone").css("background", "#f90").pulsate({ onHover: true });
        }
    }
</script>
