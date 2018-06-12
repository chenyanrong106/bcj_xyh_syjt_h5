<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewDetail.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.NewDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="美道家">
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
    <link type="text/css" rel="stylesheet" href="css/newindex.css?v=1" />
    <title>小时光Massage</title>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="newindex.aspx"><span class="icon-left"></span></a>
                <h2>小时光 Massage</h2>
                <a href="tel:17621160808"><span class="icon-tel"></span></a>
            </div>
        </div>
        <!--end header-->
        <!--begin content-->
        <div class="meiWapper">

            <div class="indexProDetail">


                <div class="shopImg">
                    <div class="swiper-container" id="swiperbanner">
                        <div class="swiper-wrapper">

                              <div class="swiper-slide">
                                <img src="image/2.jpg" alt="团购图片2">
                            </div>
                            <div class="swiper-slide">
                                <img src="image/1.jpg" alt="团购图片2">
                            </div>
                            <div class="swiper-slide">
                                <img src="image/3.jpg" alt="团购图片2">
                            </div>
                            <div class="swiper-slide">
                                <img src="image/5.jpg" alt="团购图片2">
                            </div>
                        </div>
                        <div class="swiper-pagination"></div>
                    </div>

                    <%if (store != null)
                      { %>
                    <div class="proBookbox">
                        <div class="bookBtn" onclick="window.location='zhifu.aspx?storeid=<%=store.ID %>'">买单</div>
                        <p><strong><%=store.NAME %> </strong></p>
                        <div class="proPrice"><span><i class="star"></i><i class="star"></i><i class="star"></i><i class="star"></i><i class="star"></i>&nbsp;<%=StorePJ %>条</span> ￥150/人</div>
                    </div>

                    <div class="bookAddress">
                        <div class="addressIocn">
                            <p onclick="showmap(<%=store.Lat %>,<%=store.Lng %>,'<%=store.NAME %>','<%=store.ADDRESS %>')"><span class="icon-location"></span><%=store.ADDRESS %></p>
                        </div>
                        <div class="addTel" onclick="window.location='tel:<%=store.TELEPHONE %>'">
                            <span class="icon-tel"></span>
                        </div>
                    </div>
                    <%} %>
                    <div class="anxinxuan">
                        <h2></h2>
                        <ol><span>杜绝推销</span></ol>
                        <ol><span>快速维权</span></ol>
                        <ol><span>价格透明</span></ol>
                        <ol><span>无隐形消费</span></ol>
                    </div>

                    <div class="freeBook">
                        <div class="bookTT"><span><%=servicelist.Sum(s=>s.PayCount)+300 %>人已消费</span>
                          <%--  <bdo class="ding">订</bdo>--%> 
                            <strong>优惠预订（<%=servicelist.Count %>）</strong></div>
                        <div class="mei-time-rili time-rili-bg time-rili-line">
                            <ul>
                                <li>周一<bdo>2.21</bdo></li>
                                <li>周二<bdo>2.22</bdo></li>
                                <li>周三<bdo>2.23</bdo></li>
                                <li>周四<bdo>2.24</bdo></li>
                                <li>周五<bdo>2.25</bdo></li>
                                <li>周六<bdo>2.26</bdo></li>
                            </ul>
                        </div>
                        <div class="bookProlist">
                            <ul class="progaodu">
                                <%foreach (var s in servicelist)
                                  {
                                %>
                                <li>
                                    <div class="bookMenu">
                                        <p><strong><%=s.NAME %></strong></p>
                                        <p class="info"><%=s.TIME_LEN %>分钟  <%=s.BQ %></p>
                                    </div>
                                    <div class="bookPrice">
                                        <%if(s.PRICE!=null&&s.OldPrice!=null){ %>
                                        <p>￥<%=s.PRICE.ToString("0.##") %> <span class="yuanjia">￥<%=s.OldPrice.Value.ToString("0.##") %></span></p>
                                        <%} %>
                                        <%-- <p><span class="lijian">最高立减30元</span></p>--%>
                                    </div>
                                    <a href="javascript:;">
                                        <div class="bookDing" onclick="yuyue(<%=s.ID %>)">预订</div>
                                    </a>
                                </li>
                                <%
                                  } %>
                            </ul>
                            <p align="center"><a href="javascript:void(0)"><span class="moreFuwu">查看全部4个服务</span></a></p>
                        </div>
                    </div>




                    <div class="bookTT online">
                       <%-- <bdo class="ding">约</bdo> --%>
                        <strong>技师（16）</strong></div>


                    <div class="jishi-list-roll swiper-container" id="swiperSubimg02">
                        <div class="swiper-wrapper">
                            <%foreach (var emp in oelist)
                              {
                            %>
                            <div class="swiper-slide">
                                <div class="jishiWoo">
                                    <img src="<%=string.IsNullOrEmpty(emp.Img)?"image/jishi.jpg":WebUrl+emp.Img %>">
                                    <p class="jishiName"><%=emp.NAME %></p>
                                    <p><bdo>高级技师</bdo></p>
                                    <p>月订单数<%=emp.MDD %></p>
                                </div>
                            </div>
                            <%
                              } %>
                        </div>
                    </div>


                    <div class="bookTT online"><span><i class="icon-right"></i></span>
                       <%-- <bdo class="ding">享</bdo> --%>
                        <strong>商户信息</strong></div>
                    <div class="Proshanghu">
                        <ol><span><em class="iocn01 iocn02"></em>WIFI</span></ol>
                        <ol><span><em class="iocn01 iocn04"></em>包厢</span></ol>
                        <ol><span><em class="iocn01"></em>吸烟区</span></ol>
                        <ol><span><em class="iocn01 iocn03"></em>无烟区</span></ol>
                        <p>营业时间：10：00~1：00 周一至周日</p>
                    </div>
                    <div class="proBookTitle" style="border-top: 1px solid #f3f3f3; border-bottom: 10px solid #f3f3f3;">
                        <ul>
                            <a href="newindex.aspx">
                                <li style="border-bottom: none;"><span><i class="icon-right"></i></span>查看全部<%=list.Count %>家门店</li>
                            </a>
                        </ul>
                    </div>
                </div>


            </div>
            <!--end content-->
            <input type="hidden" id="storeid" value="<%=Request.QueryString["id"] %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script type="text/javascript" src="js/swiper.js"></script>
<script type="text/javascript">
    var mySwiper = new Swiper('#swiperbanner', { loop: true, autoplay: 4500, pagination: '.swiper-pagination' });
    var swiper02 = new Swiper('.jishi-list-roll', {
        slidesPerView: 'auto',
        paginationClickable: true,
    });


    var rilihtml = '';
    var now = new Date();
    var days = now.getDate();
    var months = now.getMonth() + 1;
    for (var i = 1; i < 7; i++) {
        var date = new Date(now.getTime() + i * 24 * 3600 * 1000);
        var month = date.getMonth() + 1;
        var day = date.getDate();
        var week = date.getDay();
        var a = new Array("日", "一", "二", "三", "四", "五", "六");

        rilihtml += '<li>周' + a[week] + '<bdo name="' + date.getFullYear() + '">' + month + '.' + day + '</bdo></li>';
    }

    $('.time-rili-bg ul').html('<li class="curDate">今天<bdo name="' + now.getFullYear() + '">' + months + '.' + days + '</bdo></li>' + rilihtml)

    $('.time-rili-bg ul li').on('click', function () {
        $(this).addClass('curDate').siblings().removeClass('curDate');
    })


    var moreFlag = true;
    $('.moreFuwu').on('click', function () {
        if (moreFlag == true) {
            $(this).html('收起');
            $(this).addClass('moreFuwuUp');
            $('.bookProlist ul').removeClass('progaodu');
            moreFlag = false;
        } else {
            $(this).html('更多到店时间');
            $(this).removeClass('moreFuwuUp');
            $('.bookProlist ul').addClass('progaodu');
            moreFlag = true;
        }
    })
    $('.moreFuwu').click();
    function yuyue(serviceid) {
        console.log(serviceid);
        console.log($(".mei-time-rili ul li.curDate bdo").attr("name") + "." + $(".mei-time-rili ul li.curDate bdo").html());
        console.log($("#storeid").val());
        location = "newbook.aspx?storeid=" + $("#storeid").val() + "&time=" + $(".mei-time-rili ul li.curDate bdo").attr("name") + "." + $(".mei-time-rili ul li.curDate bdo").html() + "&serviceid=" + serviceid;
    }

    function showmap(a, b, c, d) {
        wx.openLocation({
            latitude: a, // 纬度，浮点数，范围为90 ~ -90
            longitude: b, // 经度，浮点数，范围为180 ~ -180。
            name: c, // 位置名
            address: d, // 地址详情说明
            scale: 28, // 地图缩放级别,整形值,范围从1~28。默认为最大
            infoUrl: '' // 在查看位置界面底部显示的超链接,可点击跳转
        });
    }
</script>
