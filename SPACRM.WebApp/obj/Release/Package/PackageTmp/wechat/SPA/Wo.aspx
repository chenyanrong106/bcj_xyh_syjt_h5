<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wo.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Wo" %>

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
    <link type="text/css" rel="stylesheet" href="css/index.css?v=1" />
    <title>小时光Massage</title>
    <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;"><span class="icon-txt">&nbsp;</span></a>
                <h2>我的</h2>
                <a href="tel:17621160808"><span class="icon-tel"></span></a>
            </div>
        </div>
        <!--end header-->




        <!--begin content-->
        <div class="meiWapper mei-index-bottom">

            <div class="admin-items">
                <%if (oa != null)
                  { %>
                <p>
                    <em class="icon-right"></em>
                    <img src="<%=oa.headimgurl %>"><strong><%=oa.Nickname %></strong>
                    <span id="phone"></span>
                </p>
                <%} %>
            </div>

            <div class="admin-tab">
                <%--<ol onclick="window.location='quan.aspx'"><span class="icon-coupon2"></span>
      <p><strong>卡券</strong></p>
      <p>1张优惠券</p>
      </ol>--%>
                <ol onclick="window.location='../discount/index.aspx'">
                    <span class="icon-coupon2"></span>
                    <p><strong>卡券</strong></p>
                    <p><%=UserDisList.Count %>张优惠券</p>
                </ol>
                <ol onclick="window.location='dingdan.aspx'">
                    <span class="icon-order_other"></span>
                    <p><strong>我的订单</strong></p>
                    <p>订单管理</p>
                </ol>

                <%-- <ol onclick="window.location='chongzhi.aspx'"><span class="icon-package2"></span>
      <p><strong>我的余额</strong></p>
      <p>￥0.00</p>
      </ol>--%>
                <%if (oa != null)
                  { %>
                <ol onclick="window.location='chongzhi.aspx'">
                    <span class="icon-package2"></span>
                    <p><strong>我的余额</strong></p>
                    <p>￥<%=oa.SurplusMoney??0 %></p>
                </ol>
                <%} %>
            </div>


            <div class="order-list-box">
                <ul>
                    <%-- <%if(oa!=null){ %>
          <li>
             <p onclick="window.location='tuijian.aspx?id=<%=oa.FromUserName %>'"><span><bdo class="red-color">10元奖励累计</bdo> <i class="icon-right"></i></span><i class="icon-i icon-recommend"></i><bdo class="address-txt">推荐有奖</bdo></p>
             <p><span><i class="icon-right"></i></span><i class="icon-i icon-invite"></i><bdo class="address-txt">我的成长</bdo></p>
          </li>
          <%} %>
           <li>
             <p><span><i class="icon-right"></i></span><i class="icon-i icon-trolley"></i><bdo class="address-txt">礼品卡兑换</bdo></p>
             <p><span><i class="icon-right"></i></span><i class="icon-i icon-user"></i><bdo class="address-txt">用户档案</bdo></p>
          </li>
           <li>
             <p><span><i class="icon-right"></i></span><i class="icon-i icon-feedback"></i><bdo class="address-txt">意见反馈</bdo></p>
             <p><span><i class="icon-right"></i></span><i class="icon-i icon-beautician"></i><bdo class="address-txt">招聘技师</bdo></p>
          </li>--%>
                    <%if (oa != null)
                      { %>
                    <li>
                        <p onclick="alert('敬请期待')"><span><bdo class="red-color"></bdo> <i class="icon-right"></i></span><i class="icon-i icon-recommend"></i><bdo class="address-txt">推荐有奖</bdo></p>
                        <p onclick="window.alert('开发中')"><span><i class="icon-right"></i></span><i class="icon-i icon-invite"></i><bdo class="address-txt">我的成长</bdo></p>
                    </li>
                    <%} %>
                    <li>
                        <p onclick="window.alert('开发中')"><span><i class="icon-right"></i></span><i class="icon-i icon-trolley"></i><bdo class="address-txt">礼品卡兑换</bdo></p>
                        <p onclick="window.alert('开发中')"><span><i class="icon-right"></i></span><i class="icon-i icon-user"></i><bdo class="address-txt">用户档案</bdo></p>
                    </li>
                    <li>
                        <p onclick="window.alert('开发中')"><span><i class="icon-right"></i></span><i class="icon-i icon-feedback"></i><bdo class="address-txt">意见反馈</bdo></p>
                        <p onclick="window.alert('开发中')"><span><i class="icon-right"></i></span><i class="icon-i icon-beautician"></i><bdo class="address-txt">招聘技师</bdo></p>
                    </li>

                </ul>
            </div>

        </div>
        <!--end content-->

        <div class="footerbar">
            <ul>
                <li><a href="newindex.aspx"><span class="icon-logo"></span><bdo>门店</bdo></a></li>
                <li><a href="dingdan.aspx"><span class="icon-agreement"></span><bdo>订单</bdo></a></li>
                <li class="cur"><a href="wo.aspx"><span class="icon-personal"></span><bdo>我的</bdo></a></li>
            </ul>
        </div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/lazy.js"></script>
<script type="text/javascript" src="js/swiper.js"></script>
<script type="text/javascript">

</script>
