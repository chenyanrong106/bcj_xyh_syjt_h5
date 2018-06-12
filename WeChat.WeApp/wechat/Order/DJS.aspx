<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DJS.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.DJS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>手工半湿粮——宠物管家</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="telephone=no" name="format-detection" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        html {
            height: 100%;
            will-change: transform, opacity;
        }

        body {
            font-family: Microsoft Yahei, \5FAE\8F6F\96C5\9ED1, \5b8b\4f53, Arial, Lucida, Verdana, Helvetica, sans-serif;
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: 100%;
            background: #000;
            overflow-x: hidden;
            -webkit-user-select: none;
            -webkit-text-size-adjust: none;
        }

        .endTimebg {
            width: 100%;
            background: url(images/bg3.jpg) no-repeat center center #000;
            background-size: 100% 313px;
            height: 100%;
            position: absolute;
            left: 0;
            top: 0;
            display: block;
            z-index: 0;
        }

        .endTimebox {
            width: 300px;
            height: 540px;
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 99;
            margin-left: -150px;
            margin-top: -220px;
            display: block;
            overflow: hidden;
        }

        .endTimebg .petlogo {
            background: url(images/logo3.png) no-repeat center 0;
            background-size: 160px 71px;
            width: 220px;
            height: 20px;
            padding-top: 90px;
            margin: 20px auto;
            display: block;
            z-index: 2;
        }

            .endTimebg .petlogo p {
                line-height: 20px;
                font-size: 16px;
                text-align: center;
                color: #ed6d00;
            }

        .endTimebg .daojishi {
            width: 300px;
            height: 100px;
            display: block;
            z-index: 99;
            margin: 40px auto 80px;
        }

            .endTimebg .daojishi h2 {
                line-height: 30px;
                font-size: 18px;
                color: #fff;
                text-align: center;
            }

            .endTimebg .daojishi p {
                width: 300px;
                height: 55px;
                display: block;
            }

                .endTimebg .daojishi p span {
                    float: left;
                    width: 55px;
                    margin: 10px;
                    display: inline;
                    text-align: center;
                    background-color: #b333a8;
                    font-size: 12px;
                    font-family: Arial, Helvetica, sans-serif;
                    height: 55px;
                    border-radius: 55px;
                    color: #fff;
                    line-height: 30px;
                }

                .endTimebg .daojishi p em {
                    display: block;
                    font-size: 30px;
                    color: #fff;
                    font-style: normal;
                    line-height: 55px;
                }

        .endTimebg .btn {
            width: 100%;
            height: 44px;
            line-height: 44px;
            background: #ed6d00;
            border-radius: 5px;
            text-align: center;
            color: #fff;
            font-size: 18px;
            font-weight: bold;
            margin: 10px auto;
        }

        .phonebg {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: rgba(0,0,0,.9);
            display: none;
        }

        .phonebox {
            width: 280px;
            height: 170px;
            position: absolute;
            left: 50%;
            top: 50%;
            margin: 0 auto;
            overflow: hidden;
            z-index: 9999999;
            display: block;
            margin-left: -150px;
            margin-top: -85px;
            background: #eee;
            border-radius: 5px;
            padding: 10px;
        }

            .phonebox .txt {
                width: 99%;
                background: #fff;
                border: none;
                height: 44px;
                text-indent: 10px;
                font-size: 18px;
                line-height: 44px;
                border: 1px solid #ddd;
                -webkit-appearance: none;
            }

            .phonebox p {
                line-height: 30px;
                font-size: 16px;
                color: #666;
                text-align: center;
                background: #fff;
                padding: 8px;
                border-radius: 10px;
                height: 150px;
            }

                .phonebox p strong {
                    line-height: 50px;
                    display: block;
                    font-size: 20px;
                    text-align: center;
                    color: #ff6600;
                }

                .phonebox p bdo {
                    font-size: 14px;
                    color: #ff0000;
                    text-align: center;
                }

            .phonebox h2 {
                font-size: 14px;
                line-height: 40px;
                text-align: center;
                color: #f00;
                font-weight: normal;
            }

        .closed {
            position: absolute;
            background: url(images/cha.png) no-repeat center center;
            background-size: 20px;
            width: 20px;
            height: 20px;
            position: absolute;
            right: 15px;
            top: 15px;
            z-index: 99999999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="endTimebg">
            <div class="endTimebox">
                <div class="petlogo">
                    <p>2015.10.8 12:00准时开抢</p>
                </div>
                <div class="daojishi">
                    <h2>新鲜上市倒计时</h2>
                    <p id="timeid">
                        <span><em>00</em>天</span>
                        <span><em>00</em>小时</span>
                        <span><em>00</em>分</span>
                        <span><em>00</em>秒</span>
                    </p>
                </div>
                <div class="btn" onclick="return showbox();" id="lq" runat="server">领取优惠券</div>
                <div class="btn" id="lw" runat="server" visible="false">本次优惠券已领完</div>
                <div style="color: gray; font-size: smaller; text-align: center;" id="yhqsl" runat="server"></div>
                <br />
                <div style="color: gray; font-size: smaller; text-align: center;" id="Div1"><a style="color: blue;" href="http://mp.weixin.qq.com/s?__biz=MzI0MzAzMjAyOA==&mid=210326296&idx=1&sn=269139b531d111f188bd67ea497bd03e&scene=0#rd">点击可直接关注PETKIN手工湿粮</a></div>
            </div>


            <div class="phonebg" id="quanbox">
                <div class="closed" onclick="closed();"></div>
                <div class="phonebox" id="phoneid">
                    <h2>需要手机号码作为唯一识别码来领取优惠</h2>
                    <bdo style="margin-bottom: 20px;">
                        <input type="tel" id="iphone" name="iphone" class="txt" placeholder="输入您的手机号"></bdo>
                    <div class="btn" onclick="return checked();">确认</div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/zepto.min.js"></script>
<script type="text/javascript" src="js/touch.js"></script>
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
<script src="js/djs.js?v=1.0"></script>
<script src="js/Message2.js"></script>
<script type="text/javascript">
    document.addEventListener('touchmove', function (event) {
        event.preventDefault();
    }, false);
    var oEm = document.getElementById('timeid').getElementsByTagName('em');
    var tianshu = oEm[0];
    var xiaoshi = oEm[1];
    var fenz = oEm[2];
    var miaoz = oEm[3];
    function ShowCountDown(year, month, day) {
        var now = new Date();
        var endDate2 = new Date(year, month - 1, day);
        var endDate = endDate2.getTime();
        var leftTime = endDate - now.getTime();
        leftTime = leftTime + 12 * 3600 * 1000;
        var leftsecond = parseInt(leftTime / 1000);
        var day1 = Math.floor(leftsecond / (60 * 60 * 24));
        var hour = Math.floor((leftsecond - day1 * 24 * 60 * 60) / 3600);
        var minute = Math.floor((leftsecond - day1 * 24 * 60 * 60 - hour * 3600) / 60);
        var second = Math.floor(leftsecond - day1 * 24 * 60 * 60 - hour * 3600 - minute * 60);
        if (day1 >= 10) {
            tianshu.innerHTML = day1;
        } else {
            tianshu.innerHTML = "0" + day1;
        }
        if (hour >= 10) {
            xiaoshi.innerHTML = hour;
        } else {
            xiaoshi.innerHTML = "0" + hour;
        }
        if (minute >= 10) {
            fenz.innerHTML = minute;
        } else {
            fenz.innerHTML = "0" + minute;
        }
        if (second >= 10) {
            miaoz.innerHTML = second;
        } else {
            miaoz.innerHTML = "0" + second;
        }


        if (leftsecond <= 0) {
            clearInterval(timer);
            // alert("时间到了");
            //$.MsgBox.Alert("宠物管家", "时间到了");
            tianshu.innerHTML = "0";
            xiaoshi.innerHTML = "0";
            fenz.innerHTML = "0";
            miaoz.innerHTML = "0";
        }

    }
    var timer = setInterval(function () { ShowCountDown(2015, 10, 8); }, 1000);



    function showbox() {
        document.getElementById('quanbox').style.display = "block";
    }
    function closed() {
        document.getElementById('quanbox').style.display = "none";
    }

    function checked() {
        var phone = document.all.iphone.value;
        var patrn = /^0?(13[0-9]|15[0-9]|17[0-9]|18[0-9]|14[0-9])[0-9]{8}$/;

        if (patrn.test(phone)) {
            //alert("恭喜获得一张8折抵扣券，刚填写的手机号码为唯一识别码，产品上市当天可直接抵扣");  
            $.post("djs.aspx?para=tj", {
                phone: $("#iphone").val()
            },
                               function (ret) {
                                   if (ret.st == 1) {
                                       document.getElementById('phoneid').innerHTML = "<p><strong>恭喜您,获得一张8折抵扣券</strong> 刚填写的手机号码为唯一识别码，产品上市当天可直接抵扣。 <br/> <bdo>2015.10.8 中午12:00 我们不见不散！</bdo></p>";
                                   }
                                   else {
                                       $.MsgBox.Alert("宠物管家", ret.msg);
                                   }
                               },
                               "json"
                         );

        } else {
            //alert("手机号码不正确！");
            $.MsgBox.Alert("宠物管家", "手机号码不正确");
        }
    }
</script>
