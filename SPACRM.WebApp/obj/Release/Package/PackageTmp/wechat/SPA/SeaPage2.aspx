<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeaPage2.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.SeaPage2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="x5-page-mode" content="app">
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/zhuanti.css" />
    <title>小时光SPA</title>
    <script>(function (c, f) { var s = c.document; var b = s.documentElement; var m = s.querySelector('meta[name="viewport"]'); var n = s.querySelector('meta[name="flexible"]'); var a = 0; var r = 0; var l; var d = f.flexible || (f.flexible = {}); if (m) { var e = m.getAttribute("content").match(/initial\-scale=([\d\.]+)/); if (e) { r = parseFloat(e[1]); a = parseInt(1 / r) } } else { if (n) { var j = n.getAttribute("content"); if (j) { var q = j.match(/initial\-dpr=([\d\.]+)/); var h = j.match(/maximum\-dpr=([\d\.]+)/); if (q) { a = parseFloat(q[1]); r = parseFloat((1 / a).toFixed(2)) } if (h) { a = parseFloat(h[1]); r = parseFloat((1 / a).toFixed(2)) } } } } if (!a && !r) { var p = c.navigator.appVersion.match(/android/gi); var o = c.navigator.appVersion.match(/iphone/gi); var k = c.devicePixelRatio; if (o) { if (k >= 3 && (!a || a >= 3)) { a = 3 } else { if (k >= 2 && (!a || a >= 2)) { a = 2 } else { a = 1 } } } else { a = 1 } r = 1 / a } b.setAttribute("data-dpr", a); if (!m) { m = s.createElement("meta"); m.setAttribute("name", "viewport"); m.setAttribute("content", "initial-scale=" + r + ", maximum-scale=" + r + ", minimum-scale=" + r + ", user-scalable=no"); if (b.firstElementChild) { b.firstElementChild.appendChild(m) } else { var g = s.createElement("div"); g.appendChild(m); s.write(g.innerHTML) } } function i() { var t = b.getBoundingClientRect().width; if (t / a > 750) { t = 750 * a } var u = t / 10; b.style.fontSize = u + "px"; d.rem = c.rem = u } c.addEventListener("resize", function () { clearTimeout(l); l = setTimeout(i, 300) }, false); c.addEventListener("pageshow", function (t) { if (t.persisted) { clearTimeout(l); l = setTimeout(i, 300) } }, false); if (s.readyState === "complete") { s.body.style.fontSize = 12 * a + "px" } else { s.addEventListener("DOMContentLoaded", function (t) { s.body.style.fontSize = 12 * a + "px" }, false) } i(); d.dpr = c.dpr = a; d.refreshRem = i })(window, window["lib"] || (window["lib"] = {}));
    </script>
</head>
<body class="zhuanti">
    <form id="form1" runat="server">
        <div class="banner">
            <a href="#">
                <img src="images/banner.jpg" /></a>
            <div class="daojishi">
                <p><span>5位名额</span> | <span><%=personcount+300 %>人已参与</span></p>
                <div class="daoTime" id="daojishi">活动已结束。</div>
            </div>
            <a href="Sealist.aspx">
                <div class="wangqi">往期回顾</div>
            </a>
        </div>

        <div class="liucheng">
            <img src="images/liucheng.jpg" />
        </div>


        <div class="lipinma">
            <h2><span class="icon01">您的抽奖码</span></h2>
            <ul>
                <%foreach (var r in rands)
                  {
                %>
                <%if (r.ToString().Length == 7)
                  { %>
                <li><span>0<%=r %></span></li>
                <%}
                  else if (r.ToString().Length == 6)
                  { %>
                <li><span>00<%=r %></span></li>
                <%}
                  else
                  { %>
                <li><span><%=r %></span></li>
                <%} %>
                <%
                  } %>

                <% bool zj = false;
                   foreach (var a in rands)
                   {
                %>
                <%if (a == 11282199 || a == 13140993 || a == 13220239 || a == 13435237 || a == 13508414) { zj = true; } %>
                <%
                      
                  }
                    if (zj)
                    {
                %>
                <li style="width: 100%;"><span>恭喜您中奖了！</span></li>
                <%
                      }
                  else
                  {
                %>
                <li style="width: 100%;"><span>很遗憾，你没中奖。</span></li>
                <%   
                      }
                %>
            </ul>
        </div>

        <div class="lipinma">
            <h2><span class="icon01">开奖码</span></h2>
            <ul>
                <li><span>11282199</span></li>
                <li><span>13140993</span></li>
                <li><span>13220239</span></li>
                <li><span>13435237</span></li>
                <li><span>13508414</span></li>
            </ul>
        </div>

        <div class="lipinma">
            <%--  <h2><span class="icon02">活动须知</span></h2>--%>
            <div class="huodong">
                <strong>开奖流程</strong>
                <p>
                    1、活动结束时的上证指数为：3262.08<br />
                    2、乘以100后为：326208<br />
                    3、平方之后为：106411659264<br />
                    4、取最后八位为：11659264<br />
                    5、得出最接近此八位数的中奖码为11282199、13140993、13220239、13435237、13508414
                    
                </p>
            </div>
        </div>


        <div class="lipinma">
            <h2><span class="icon02">活动须知</span></h2>
            <div class="huodong">
                <strong>活动规则</strong>
                <p>
                    本期活动时间：2017年7月20日00:00:00 - 8月3日23:59:59 本期幵奖时间：8月4日15:00 
专属海报每周更新一次，请记得回到本活动页更新海报喔！
                </p>
                <br />
                <strong>参与方式</strong>
                <p>参与者通过分享自己的邀请海报让朋友扫码关注小时光SPA微信公众号。每成功邀请一位新用户，即可随时获得一个8位数的抽奖码，可无限累计，抽奖码越多中奖几率越大。抽奖码仅当期活动有效。超出活动时间获得的邀请码可自动参与下一期活动。</p>
                <br />
                <strong>抽奖规则</strong>
                <p>小时光郑重承诺，活动抽奖规则公平公幵。取该期活动结朿日期收盘时候的上证指数（如果当天休市则取休市时候的指数），该指数乘以100，得到一个6位整数，然后该6位整数平方后，取最后8位数。与此8位数最接近的数字就是中奖号码（如果有两个数字一样接近，则取更大的数字为中奖号码）</p>

                <br />
                <strong>中奖号码举例</strong>
                <p>活动结束时间8月3日23:59:59,取8月4日收盘时候上证指数3273.83,该指数乘以100为327383,取平方数为107179628689,取后8位数为79628689,与该数字最接近的即为中奖号码。</p>

                <br />
                <strong>获奖通知</strong>
                <p>请留意抽奖时间，幵奖后24小时内我们将联系绑定的微信，获奖人需根据获奖通知提供手机号码，同时小时光SPA公告、推文会显示中奖ID。获奖商品保留30天，若因获奖用户取消关注、长期不留意小时光SPA推送等原因无法取得联系，将自动取消领奖，请一定保持可联络状态。谢谢配合。</p>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    window.onload = function () {
        test();

        function test() {
            // 当前时间
            var nowTime = new Date().getTime();

            // 结束时间
            var endTime = new Date('2017/08/4 00:00:00');
            // 相差的时间	
            var t = endTime.getTime() - nowTime;

            if (t <= 0) {
                document.getElementById('daojishi').innerHTML('活动已经结束');
                return false;
            }

            var d = Math.floor(t / 1000 / 60 / 60 / 24);
            var h = Math.floor(t / 1000 / 60 / 60 % 24);
            var i = Math.floor(t / 1000 / 60 % 60);
            var s = Math.floor(t / 1000 % 60);
            if (d < 10) { d = '0' + d }
            if (h < 10) { h = '0' + h }
            if (i < 10) { i = '0' + i }
            if (s < 10) { s = '0' + s }
            document.getElementById('daojishi').innerHTML = d + '天' + h + '小时' + i + '分钟' + s + '秒';
            setTimeout(test, 1000);
        }
    }
</script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script>
    $.ajax({
        url: '../Business.ashx?para=jsapi&apiurl=' + $("#url").val(),

        type: 'POST',

        data: {},

        dataType: 'html',

        timeout: 1000,

        error: function (e) { },

        success: function (result) {

            result = JSON.parse(result);
            console.log(result);
            if (result.state == 0) {
                wx.config({
                    debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                    appId: result.appId, // 必填，公众号的唯一标识
                    timestamp: result.timestamp, // 必填，生成签名的时间戳
                    nonceStr: result.nonceStr, // 必填，生成签名的随机串
                    signature: result.signature,// 必填，签名，见附录1
                    jsApiList: ["getLocation", "onMenuShareTimeline", "onMenuShareAppMessage", "onMenuShareQQ", "onMenuShareWeibo", "scanQRCode", "openLocation", "chooseWXPay"] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
                });
                wx.ready(function () {
                    //alert("ready");

                    wx.onMenuShareTimeline({ //分享朋友圈
                        title: "【免费赢星巴克夏季特饮】冰爽一夏，等你来领！—小时光SPA", // 分享标题
                        link: result.link, // 分享链接
                        imgUrl: "http://www.xsgapp.com/wechat/spa/images/p2.jpg", // 分享图标
                        success: function () {
                            // 用户确认分享后执行的回调函数
                            zf();
                        },
                        cancel: function () {
                            // 用户取消分享后执行的回调函数
                        }
                    });
                    wx.onMenuShareAppMessage({ //分享给朋友
                        title: "【免费赢星巴克夏季特饮】冰爽一夏，等你来领！—小时光SPA", // 分享标题
                        desc: "小时光SPA心愿好礼第二期", // 分享描述
                        link: result.link, // 分享链接
                        imgUrl: "http://www.xsgapp.com/wechat/spa/images/p2.jpg", // 分享图标
                        type: '', // 分享类型,music、video或link，不填默认为link
                        dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                        success: function () {
                            // 用户确认分享后执行的回调函数
                            zf();
                        },
                        cancel: function () {
                            // 用户取消分享后执行的回调函数
                        }
                    });
                    // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
                });
                wx.error(function (res) {
                    // alert("error");
                    // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。

                });
            }
        }

    });

    function zf() {
        $.ajax({
            url: 'seapage2.aspx?para=zf',

            type: 'POST',

            data: {},

            dataType: 'json',

            timeout: 1000,

            error: function (e) { },

            success: function (result) {
                location = location;
            }

        });
    }
</script>
