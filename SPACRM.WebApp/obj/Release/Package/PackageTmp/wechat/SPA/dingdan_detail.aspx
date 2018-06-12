<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dingdan_detail.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.dingdan_detail" %>

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
    <link type="text/css" rel="stylesheet" href="css/index.css?v=4.0" />
    <title>小时光Massage</title>  <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <%if (order != null)
          {%>
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="history.go(-1)"><span class="icon-left"></span></a>
                <h2>我的订单</h2>
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
        <div class="meiWapper mei-index-bottom">
            <div class="order-content">




                <div class="aipet-order-items">
                    <ul>
                        <li>
                            <div class="aipet-order-ltems-title order-detail-title">
                                <span style="display:none;"><bdo class="zhifubtn">取消订单</bdo></span>
                                <strong>订单信息 <em class="daodian-icon">到店</em></strong>
                            </div>
                            <div class="aipet-order-ltems-con">
                                <span>订单状态：<bdo class="red-color"><%=order.PAY_STATUS==1?"已支付":order.OverdueDate<DateTime.Now?"已过期":"待支付" %></bdo></span>
                                <span>订单号：<%=order.ORDER_NO %></span>
                                <span>下单时间：<%=order.TRANS_DATE.ToString("yyyy.MM.dd HH:mm:ss") %></span>
                            </div>

                            <div class="pro-liucheng">
                                <ol class="cur-status"><span></span>确认下单</ol>
                                <ol <%if (order.PAY_STATUS == 1)
                                      { %>class="cur-status" <%} %>><span></span>支付成功</ol>
                                <ol <%if (order.RealBegDate!=null)
                                      { %>class="cur-status" <%} %>><span></span>开始服务</ol>
                                <ol <%if (order.RealEndDate!=null)
                                      { %>class="cur-status" <%} %>><span></span>服务结束</ol>
                            </div>
                        </li>

                        <li>
                            <h2>项目信息</h2>
                            <div class="aipet-order-ltems-con">
                                <div class="imgbox">
                                    <img src="<%=string.IsNullOrEmpty(order.Img)?"image/jishi.jpg":WebUrl+order.Img %>">
                                </div>
                                <div class="pro-info-txt">
                                    <p><bdo><a href="tel:<%=order.TELEPHONE %>"><i class="icon-tel"></i></a></bdo><strong><%=order.IS_STAFF?order.ename:"默认技师" %></strong></p>

                                    <span>项目名称：<em class="nameclass"><%=order.sname %></em></span>
                                    <span>实付金额：<bdo class="nameclass red-color"><%=order.TOTAL_AMT %>元<%=(order.TOTAL_AMT-order.PAY_AMT>0?"(储值抵扣￥"+(order.Czdk)+",优惠抵扣￥"+(order.Yhdk)+")":"") %></bdo></span>
                                </div>

                            </div>

                        </li>

                        <li>
                            <h2>预约信息</h2>
                            <div class="order-fuwu-con">
                                <p><span>预&nbsp;&nbsp;约&nbsp;&nbsp;人：</span> <%=order.CUST_NAME %></p>
                                <p><span>联系方式：</span> <%=order.CUST_MOBILE %></p>
                                <p><span>服务时间</span> <%=order.BEGIN_DATE %>-<%=order.END_DATE %></p>
                                <p><span>门店名称：</span> <%=order.STORE_NAME %></p>
                                <p><span>服务地址：</span> <%=order.ADDRESS %></p>
                            </div>

                        </li>


                    </ul>
                </div>


            </div>

        </div>
        <!--end content-->
        <%if (order.PAY_STATUS == 0 && order.OverdueDate > DateTime.Now)
          { %>
        <div class="paybtnbg">
            <a href="pay.aspx?oid=<%=order.ID %>">
                <div class="paybtn">支付</div>
            </a>
        </div>
        <%}
          else if (order.PAY_STATUS == 1 && order.PJID == 0&&order.RealEndDate!=null)
          {%>
        <div class="paybtnbg">
            <a href="pingfen.aspx?oid=<%=order.ID %>">
                <div class="paybtn">评价</div>
            </a>
        </div>
        <% }
          }%>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
