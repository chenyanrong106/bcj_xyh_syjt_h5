<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Index" %>

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
    <title>小时光Massage</title>  <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="alert('目前仅开放上海')"><span><em class="icon-down"></em><em>上海</em></span></a>
                <h2>首页</h2>
                <a href="tel:17621160808"><span class="icon-tel"></span></a>
            </div>
        </div>
        <!--end header-->
        <!--begin content-->
        <div class="meiWapper mei-index-bottom">


            <div class="shopImg">
                <div class="swiper-container" id="swiperbanner">
                    <div class="swiper-wrapper">

                        <div class="swiper-slide">
                            <img src="image/top.jpg" alt="团购图片2">
                        </div>
                        <div class="swiper-slide">
                            <img src="image/top.jpg" alt="开年狂欢瘦">
                        </div>
                    </div>
                    <div class="swiper-pagination"></div>
                </div>

                <div class="acc-menu">
                    <ul>
                        <li><a href="md.aspx"><span class="icon-room"></span>
                            <p>门店</p>
                        </a>
                        </li>
                        <li><a href="../html/price.html"><span class="icon-agreement"></span>
                            <p>价格</p>
                        </a>
                        </li>
                        <li><a href="javascript:;" onclick="alert('即将开放')"><span class="icon-groupon"></span>
                            <p>活动</p>
                        </a>
                        </li>
                        <li><a href="zhifu.aspx"><span class="icon-package2"></span>
                            <p>买单</p>
                        </a>
                        </li>

                    </ul>
                </div>
            </div>


            <div class="acc-title-h2"><strong class="qizhi">推荐活动</strong></div>
            <div class="mei-index-ad swiper-container" id="swiperSubimg02">
                <div class="swiper-wrapper">
                    <%if (OpenID != null)
                      { %>
                    <div class="swiper-slide" onclick="window.location='tuijian.aspx?id=<%=OpenID %>'">
                        <span>
                            <img src="image/1.png" width="" height="" /></span>
                    </div>
                    <div class="swiper-slide" onclick="window.location='../html/baozhang.html'">
                        <span>
                            <img src="image/2.png" /></span>
                    </div>
                    <%} %>
                    <%--  <div class="swiper-slide"><span><img src="image/1.png"/></span></div>
                   <div class="swiper-slide"><span><img src="image/2.png"/></span></div>--%>
                </div>
            </div>



            <div class="acc-title-h2"><strong class="zuopin">精选作品</strong></div>

            <div class="mei-index-items">
                <ul>
                    <%foreach (var s in servicelist)
                      {
                          if (IsService == 0 || s.ID != 527)
                          {
                    %>
                    <li>
                        <a>
                            <dt class="fl" onclick="window.location='detail.aspx?id=<%=s.ID %>'">
                                <img src="<%=string.IsNullOrEmpty(s.BookImg)?"https://dimg.365vmei.cn/uploads/project/big_582438f2366b2.jpg":WebUrl+ s.BookImg %>" alt="<%=s.NAME %>" onerror="javascript:this.src='http://static.emeidaojia.com/static/pro-default.png';"></dt>
                            <dd class="fl" onclick="window.location='detail.aspx?id=<%=s.ID %>'">
                                <h3><%=s.NAME %></h3>
                                <p class="advantage"><%=s.TITLE %></p>
                                <p class="introduce">
                                    <span class="duration"><i class="icon-red icon-duration"></i><%=s.TIME_LEN %>分钟</span>
                                </p>
                                <p class="introduce">
                                    <span class="new-price">￥<span><%=s.PRICE %></span></span>
                                    <span class="old-price">￥<%=s.OldPrice %></span>
                                    <span class="service-num"><i class="icon-i icon-service-num"></i><%=s.TIME_LEN+s.PayCount %>人做过</span>
                                </p>
                            </dd>
                            <div class="add-trolley" onclick='AddCar(<%=s.ID %>)'>
                                <a class="icon-add-item theme-color" data-name="海皙曼面部拨筋组合" data-price="568" data-duration="120" data-id="192" data-kind="1"></a>
                            </div>
                        </a>
                    </li>
                    <%  }
                      } %>
                </ul>
            </div>

        </div>
        <!--end content-->

        <div class="footerbar">
            <ul>
                <li class="cur"><a href="index.aspx"><span class="icon-logo"></span><bdo>门店</bdo></a></li>
                <li><a href="dingdan.aspx"><span class="icon-agreement"></span><bdo>订单</bdo></a></li>
                <li><a href="wo.aspx"><span class="icon-personal"></span><bdo>我的</bdo></a></li>
            </ul>
        </div>

        <input type="hidden" id="ztitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
        <input type="hidden" id="ftitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/lazy.js"></script>
<script type="text/javascript" src="js/swiper.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script type="text/javascript">
    echo.init({
        offset: 100,
        throttle: 250
    });
    //var mySwiper = new Swiper('#swiperbanner', { loop: true, autoplay: 4500, pagination: '.swiper-pagination' });
    //var swiper02 = new Swiper('.mei-index-ad', {
    //    slidesPerView: 'auto',
    //    paginationClickable: true,
    //});
    location = "newindex.aspx";
    wx.ready(function () {
        //alert(1);
        wx.getLocation({
            type: 'gcj02', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
            success: function (res) {
                var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                var speed = res.speed; // 速度，以米/每秒计
                var accuracy = res.accuracy; // 位置精度
                $.post("index.aspx?para=tj", {
                    lat: latitude,
                    lng: longitude
                }, function (ret) {

                });
            }
        });
    });

    function AddCar(sid) {
        $.ajax({
            type: "Get",
            url: "detail.aspx?para=add&qk=1&sid=" + sid,
            dataType: 'html',
            async: false,
            beforeSend: function () {

            },
            success: function (data) {
                window.location.href = 'XuanZe.aspx?id=' + sid + "&url=index.aspx";
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
</script>
