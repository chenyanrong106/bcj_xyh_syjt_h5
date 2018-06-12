<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderOK.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.OrderOK" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="wechat.aspx"><span class="icon-left"></span></a>
                <h2>确认订单</h2>
                <a href="javascript:void(0);"><span id="navRight" class="icon-lnk"></span></a>
            </div>

            <ul class="lnks">
                <li><a href="#"><span class="icon-logo-w"></span>我要预约</a></li>
                <li><a href="#"><span class="icon-order_other"></span>我的订单</a></li>
                <li><a href="#"><span class="icon-beautician"></span>我的技师</a></li>
                <li><a href="#"><span class="icon-personal"></span>个人中心</a></li>
                <li><a href="tel:17621160808"><span class="icon-tel"></span>呼叫客服</a></li>
            </ul>
        </div>
        <!--end header-->



        <!--begin content-->
        <div class="meiWapper mei-index-bottom">
            <div class="order-content">
                <div class="order-list-box order-user-info">
                    <ul>
                        <li>
                            <p><i class="icon-i icon-user"></i>
                                <input name="userName" id="userName" type="text" placeholder="请填写联系人姓名" class="inputTxt"></p>
                            <p>
                                <i class="icon-i icon-tel"></i>
                                <input name="iphone" id="iphone" type="tel" placeholder="请填写联系人电话" class="inputTxt"  maxlength="11">
                                <%--<bdo class="order-user-yanzhengma" id="timeid">发送验证码</bdo>--%>
                            </p>
                          <%--  <p><i class="icon-i icon-lock"></i>
                                <input name="codema" type="text" placeholder="请输入短信收到的验证码" class="inputTxt"></p>--%>
                        </li>
                    </ul>
                </div>

                <div class="order-list-box">
                    <ul>
                        <li>
                            <%if (store != null)
                              { %>
                            <p><span><i class="icon-right"></i></span>
                                <img src="image/tu00.jpg"><strong><%=store.NAME %></strong>地址：<%=store.ADDRESS %></p>
                            <%} %>
                            <p><span><bdo class="red-color"><%=Request.QueryString["time"] %></bdo> <i class="icon-right"></i></span><i class="icon-i icon-service-time"></i></p>
                            <p><span><i class="icon-right"></i></span><i class="icon-i icon-beautician"></i><bdo class="adminInfo">许婷婷
                                <img src="image/tou.jpg" /></bdo></p>
                        </li>


                    </ul>
                </div>


                <div class="pro-box-items">
                    <%if (ps != null)
                      { %>
                    <div class="min-box-item">服务项目</div>
                    <div id="order-list" class="order-list-flex">
                        <ul>

                            <li class="t-ellipsis"><%=ps.NAME %></li>
                            <li><%=ps.TIME_LEN %>分钟</li>
                            <li>X1</li>
                            <li>￥<%=ps.PRICE %></li>

                        </ul>
                    </div>
                    <div class="min-box-item">总计<span class="total-money">￥<%=ps.PRICE %></span></div>
                    <%} %>
                </div>
                <div class="order-list-box min-box-top">
                    <ul>
                        <li>

                            <p><span>无可用套餐 <i class="icon-right"></i></span><i class="icon-i icon-package"></i></p>
                            <p><span>无可用优惠券 <i class="icon-right"></i></span><i class="icon-i icon-coupon2"></i></p>
                        </li>
                    </ul>
                </div>



                <div class="section-box">
                    <div class="box-item remark-item">
                        <i>备注</i>
                        <div class="remark-wrap">
                            <textarea name="postscript" placeholder="有什么特殊要求可备注" rows="3" style="resize: none"></textarea></div>
                    </div>
                </div>

                <div class="order-info-remind">
                    <p>1、服务开始前／美容师出发前，您可致电客服修改订单，限一次</p>
                    <p>2、微信支付对于不同的银行卡限额不同，请确认限额后购买。若超过限定额度，可以转入微信钱包支付。</p>
                    <p>3、目前小时光Massage所有服务仅针对女性用户</p>
                    <p>4、若技师没有在规定时间内提供服务，请在24小时内联系我们进行申诉，逾期将可能影响申诉进度。为保障您的权益，请及时反馈问题。</p>
                </div>

            </div>
        </div>
        <!--end content-->

        <div class="book-box">
            <ul class="order flex">
                <li class="orderNum">实付<span class="theme-color sumMoney">￥<i>399</i></span></li>
                <li class="sumNum" data-num="3" data-duration="240"><span>3个项目</span><span>240分钟</span></li>
                <li><a class="btn-normal bookBtn" href="javascript:;" id="bookall">立即下单</a></li>
            </ul>
        </div>
        <input type="hidden" id="storeid" value="<%=Request.QueryString["storeid"] %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
