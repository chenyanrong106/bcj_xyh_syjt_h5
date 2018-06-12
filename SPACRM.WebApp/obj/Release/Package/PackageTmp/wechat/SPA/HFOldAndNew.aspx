<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HFOldAndNew.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.HFOldAndNew" %>


<!DOCTYPE html>
<html>
<head lang="en">
    <meta charset="utf-8" />
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /><meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <title>汉方SPA</title>
    <link rel="stylesheet" href="css/public.css"/>
    <script>
        (function (c, f) { var s = c.document; var b = s.documentElement; var m = s.querySelector('meta[name="viewport"]'); var n = s.querySelector('meta[name="flexible"]'); var a = 0; var r = 0; var l; var d = f.flexible || (f.flexible = {}); if (m) { var e = m.getAttribute("content").match(/initial\-scale=([\d\.]+)/); if (e) { r = parseFloat(e[1]); a = parseInt(1 / r) } } else { if (n) { var j = n.getAttribute("content"); if (j) { var q = j.match(/initial\-dpr=([\d\.]+)/); var h = j.match(/maximum\-dpr=([\d\.]+)/); if (q) { a = parseFloat(q[1]); r = parseFloat((1 / a).toFixed(2)) } if (h) { a = parseFloat(h[1]); r = parseFloat((1 / a).toFixed(2)) } } } } if (!a && !r) { var p = c.navigator.appVersion.match(/android/gi); var o = c.navigator.appVersion.match(/iphone/gi); var k = c.devicePixelRatio; if (o) { if (k >= 3 && (!a || a >= 3)) { a = 3 } else { if (k >= 2 && (!a || a >= 2)) { a = 2 } else { a = 1 } } } else { a = 1 } r = 1 / a } b.setAttribute("data-dpr", a); if (!m) { m = s.createElement("meta"); m.setAttribute("name", "viewport"); m.setAttribute("content", "initial-scale=" + r + ", maximum-scale=" + r + ", minimum-scale=" + r + ", user-scalable=no"); if (b.firstElementChild) { b.firstElementChild.appendChild(m) } else { var g = s.createElement("div"); g.appendChild(m); s.write(g.innerHTML) } } function i() { var t = b.getBoundingClientRect().width; if (t / a > 750) { t = 750 * a } var u = t / 10; b.style.fontSize = u + "px"; d.rem = c.rem = u } c.addEventListener("resize", function () { clearTimeout(l); l = setTimeout(i, 300) }, false); c.addEventListener("pageshow", function (t) { if (t.persisted) { clearTimeout(l); l = setTimeout(i, 300) } }, false); if (s.readyState === "complete") { s.body.style.fontSize = 12 * a + "px" } else { s.addEventListener("DOMContentLoaded", function (t) { s.body.style.fontSize = 12 * a + "px" }, false) } i(); d.dpr = c.dpr = a; d.refreshRem = i })(window, window["lib"] || (window["lib"] = {}));
    </script>
    <style>

    </style>
</head>
</head>
<body>
<form method="post" action="Seapage.aspx?fromusername=oXUdmwk-Yn-d1tG2GYxHdoCCmZn8&amp;tousername=oXUdmwk-Yn-d1tG2GYxHdoCCmZn8" id="form1">
    <div class="aspNetHidden">
        <input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwULLTE4NDIzNjI4NDhkZHAFbCEwACSqNUuVqcAyOb9t96Kab3AawcJDsWj0o7gK" />
    </div>

    <div class="banner">
        <a href="#">
            <img src="HFOldAndNewImage/header.jpg" />

        </a>
        <div class="daojishi">
            <div class="daoTime" id="daojishi">06天05小时09分结束</div>
        </div>
    </div>
  <%--  <div class="liucheng"></div>--%>
   <div class="save">
        <img src="HFOldAndNewImage/save.gif" alt=""/>
    </div>
    <%if (oa != null && !string.IsNullOrEmpty(oa.SeaImg))
          { %>
    <div class="quan">
        <a href="#"><%--href="javascript:;"--%>
                <img src="<%=WebUrl+"/seaimg/"+oa.SeaImg+".jpg" %>" /></a>
    </div>
    <%} %>


    <div class="rank">
          <div class="rank-wrapper">
              <div class='_header'>
                  <img src="HFOldAndNewImage/icon.png" alt=""/>
                  <span>排行榜</span>
              </div>
              <section>
                  <ul class="tabUL">
                      <li>排名</li>
                      <li>小主</li>
                      <li>邀请数</li>
                  </ul>
                  <div class="person-box">
                      <ul class="person-box-ul"> <%----%>
                          <% if (ListWXFansRank != null && ListWXFansRank.Count > 0)
                              {
                                  for (int i = 0; i < 100; i++)
                                  {
                                      if (ListWXFansRank.Count>i)
                                      {
                                  %>
                              <li>
                              <span class="num"><% =ListWXFansRank[i].rowNumber %></span>
                              <span>
                                  <img src="<%=ListWXFansRank[i].headimgurl %>" />
                                  <em><% =ListWXFansRank[i].Nickname %></em>
                              </span>
                              <span class="order"><% =ListWXFansRank[i].InviteCnt %></span>
                          </li>
                          <% 
                                      }
                                  }
                              }%>
                      </ul>
                  </div>
              </section>
              <div class="footer">
                  <img src="HFOldAndNewImage/click.png" alt="" />
                  <p>我的排名:&nbsp;&nbsp;<em><%=rank %></em></p>
              </div>
          </div>
    </div>
    <div class="lipinma">
        <div class="_header">
             <img src="HFOldAndNewImage/page.png" alt="">
             <span>活动须知</span>
        </div>
        <div class="huodong">
            <strong>活动时间</strong>
            <p class="mb10">2018年4月27日18:00:00 - 5月3日23:59:59</p>

            <strong>开奖时间</strong>
            <p class="mb10">2018年5月4日</p>
            <strong>活动规则</strong>
            <p>1、参与者通过微信推文阅读原文链接或点击微信菜单“汉方礼遇”-“邀请有礼”进入活动界面，生成专属美丽海报</p>
            <p>2、将海报分享给好友或分享至朋友圈</p>
            <p>3、好友通过扫描海报上的二维码成功关注“佰草集汉方SPA”微信公众号，该参与者排行榜上的邀请数即增加一位</p>
            <p>4、活动于2018年5月3日24点截止，以届时排行榜上排名为准，邀请成功数最多的五位参与者将分别获得惊喜大礼一份，获奖名单将在下次推送时公布</p>
            <p class="mb10">5、排行榜单实时更新，如好友在活动期间取消关注“佰草集汉方SPA”微信公众号，参与者的邀请数将被对应扣减</p>
            <strong>获奖通知</strong>
            <p>
                请留意开奖时间，开奖后24小时内我们将联系获奖者的微信，请保持微信畅通，若中奖者在7天内未回复信息，将视作自动放弃领奖名额
            </p>
        </div>
    </div>

    <input type="hidden" value="<%=Server.UrlEncode(AbsoluteUri)%>" id="url" /></form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script type="text/javascript">
  //数据请求到了之后对列表从大到小进行排序；
 
  
    window.onload=function(){
        test();
        function test(){
            // 当前时间
            var nowTime = new Date().getTime();
            // 结束时间
            var endTime = new Date('2018/05/03 23:59:59');
            // 相差的时间
            var t = endTime.getTime() - nowTime;
            if (t <= 0) {
                $('#daojishi').html("活动已经结束");
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
            var time=d + '天' + h + '小时' + i + '分钟' + s + '秒结束';
            $("#daojishi").html(time);
            setTimeout(test, 1000);
        }
    }
</script>

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
                        title: "免费赢佰草集惊喜大礼！迎接初夏！", // 分享标题
                        link: result.link, // 分享链接
                        imgUrl: "http://bcj2.puman.cn/assets/img/song.jpg", // 分享图标
                        success: function () {
                            // 用户确认分享后执行的回调函数
                            zf(3);
                        },
                        cancel: function () {
                            // 用户取消分享后执行的回调函数
                        }
                    });
                    wx.onMenuShareAppMessage({ //分享给朋友
                        title: "免费赢佰草集惊喜大礼！迎接初夏！", // 分享标题
                        desc: "汉方养美，等你来哦～", // 分享描述
                        link: result.link, // 分享链接
                        imgUrl: "http://bcj2.puman.cn/assets/img/song.jpg", // 分享图标
                        type: '', // 分享类型,music、video或link，不填默认为link
                        dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                        success: function () {
                            // 用户确认分享后执行的回调函数
                            zf(4);
                        },
                        cancel: function () {
                            // 用户取消分享后执行的回调函数
                        }
                    });
                    // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
                });
                wx.error(function (res) {
                    //alert(res);
                    // alert("error");
                    // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。

                });
            }
        }

    });

    function zf(source) {
        $.ajax({
            url: 'HFOldAndNew.aspx?para=zf&source='+source,

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

        function zf4() {
        $.ajax({
            url: 'HFOldAndNew.aspx?para=zf4',

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
<script type="text/javascript">
var _maq = _maq || [];
_maq.push(['_setAccount', '佰草集汉方Spa代言人活动']);
 

</script>
<script>
    /** 
 * pv统计 
 */  
(function () {  
    var params = {};  
    //Document对象数据  
    if(document) {  
        params.domain = document.domain || '';   
        params.url = document.URL || '';   
        params.title = document.title || '';   
        params.referrer = document.referrer || '';   
    }     
    //Window对象数据  
    if(window && window.screen) {  
        params.sh = window.screen.height || 0;  
        params.sw = window.screen.width || 0;  
        params.cd = window.screen.colorDepth || 0;  
    }     
    //navigator对象数据  
    if(navigator) {  
        params.lang = navigator.language || '';   
    }     
    //解析_maq配置  
    if(_maq) {  
        for(var i in _maq) {  
            switch(_maq[i][0]) {  
                case '_setAccount':  
                    params.account = _maq[i][1];  
                    break;  
                default:  
                    break;  
            }     
        }     
    }  
    // 其他参数  
    var date = new Date();  
    params.ltime = date.getTime() / 1000;  

    //pv统计
      $.ajax({
        url: '../Business.ashx?para=pv',

        type: 'POST',

        data: {"params":params},

        dataType: 'html',

        timeout: 1000,

        error: function (e) { },

        success: function (result) {

        }

    });
})();  
</script>

