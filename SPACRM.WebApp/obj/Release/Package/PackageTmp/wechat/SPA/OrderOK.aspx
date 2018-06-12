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
    <link type="text/css" rel="stylesheet" href="css/index.css?v=1" />
    <title>小时光Massage</title>
    <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="window.location='orderTime.aspx?id=<%=Request.QueryString["storeid"] %>&pnum=<%=Request.QueryString["pnum"] %>&sid=<%=Request.QueryString["sid"] %>&jssex=<%=Request.QueryString["jssex"] %>'"><span class="icon-left"></span></a>
                <h2>确认订单</h2>
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
                <div class="order-list-box order-user-info">
                    <ul>
                        <li>
                            <p>
                                <i class="icon-i icon-user"></i>
                                <input name="userName" id="userName" type="text" placeholder="请填写联系人姓名" class="inputTxt" value="<%=Request.QueryString["username"]==null?(booking==null?"":booking.CUST_NAME):Request.QueryString["username"] %>">
                            </p>
                            <p>
                                <i class="icon-i icon-tel"></i>
                                <input name="iphone" id="iphone" type="tel" placeholder="请填写联系人电话" class="inputTxt" maxlength="11" value="<%=Request.QueryString["phone"]==null?(booking==null?"":booking.CUST_MOBILE):Request.QueryString["phone"] %>">
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
                            <p onclick="window.location='order.aspx?id=<%=Request.QueryString["sid"] %>&pnum=<%=Request.QueryString["pnum"] %>&jssex=<%=Request.QueryString["jssex"] %>'">
                                <span><i class="icon-right"></i></span>
                                <img src="image/tu00.jpg"><strong><%=store.NAME %></strong>地址：<%=store.ADDRESS %>
                            </p>
                            <%} %>
                            <p onclick="window.location='ordertime.aspx?id=<%=Request.QueryString["storeid"] %>&pnum=<%=Request.QueryString["pnum"] %>&jssex=<%=Request.QueryString["jssex"] %>&sid=<%=Request.QueryString["sid"] %>'"><span><bdo class="red-color"><%=Request.QueryString["time"] %></bdo> <i class="icon-right"></i></span><i class="icon-i icon-service-time"></i></p>
                            <%if (Request.QueryString["pnum"] != null && int.Parse(Request.QueryString["pnum"]) == 1)
                              { %>
                            <%if (emp != null)
                              {%>
                            <p onclick="selectjs('<%=Request.QueryString["time"] %>',<%=Request.QueryString["storeid"] %>,<%=Request.QueryString["pnum"] %>,<%=Request.QueryString["jssex"] %>,<%=Request.QueryString["sid"] %>,<%=Request.QueryString["emp"]??"0" %>,<%=Request.QueryString["dis"]??"0" %>)">
                                <span><i class="icon-right"></i></span><i class="icon-i icon-beautician"></i><bdo class="adminInfo"><%=emp.NAME %>
                                    <img src="<%=string.IsNullOrEmpty(emp.Img)?"image/jishi.jpg":WebUrl+emp.Img %>" /></bdo>
                            </p>
                            <% }
                              else
                              { %>
                            <p onclick="selectjs('<%=Request.QueryString["time"] %>',<%=Request.QueryString["storeid"] %>,<%=Request.QueryString["pnum"] %>,<%=Request.QueryString["jssex"] %>,<%=Request.QueryString["sid"] %>,<%=Request.QueryString["emp"]??"0" %>,<%=Request.QueryString["dis"]??"0" %>)">
                                <span><i class="icon-right"></i></span><i class="icon-i icon-beautician"></i><bdo class="adminInfo">默认技师
                                <img src="image/jishi.jpg" /></bdo>
                            </p>
                            <%} %>
                            <%}
                              else
                              { %>
                            <p onclick="window.alert('多人不可预约技师')">
                                <span><i class="icon-right"></i></span><i class="icon-i icon-beautician"></i><bdo class="adminInfo">默认技师
                                <img src="image/jishi.jpg" /></bdo>
                            </p>
                            <%} %>
                        </li>


                    </ul>
                </div>


                <div class="pro-box-items">
                    <%decimal price = 0;
                      int servicenum = 0;
                      int time = 0;
                      if (clist.Count > 0)
                      {
                    %>
                    <div class="min-box-item">服务项目</div>
                    <div id="order-list" class="order-list-flex" style="height: auto;">
                        <%foreach (var c in clist)
                          {
                              price += c.Num.Value * c.ServicePrice.Value;
                              servicenum += c.Num.Value;
                              time += c.Num.Value * c.TimeLeng.Value;
                        %>
                        <ul>
                            <li class="t-ellipsis"><%=c.ServiceName %></li>
                            <li><%=c.TimeLeng %>分钟</li>
                            <li>X<%=c.Num %></li>
                            <li>￥<%=c.ServicePrice %></li>

                        </ul>
                        <%} %>
                    </div>
                    <div class="min-box-item">总计<span class="total-money">￥<%=price %></span></div>
                    <%} %>
                </div>
                <div class="order-list-box min-box-top">
                    <ul>
                        <li>

                            <%-- <p><span>无可用套餐 <i class="icon-right"></i></span><i class="icon-i icon-package"></i></p>--%>
                            <%if (mydis != null)
                              {
                            %>
                            <p onclick="selectdis('<%=Request.QueryString["time"] %>',<%=Request.QueryString["storeid"] %>,<%=Request.QueryString["pnum"] %>,<%=Request.QueryString["jssex"] %>,<%=Request.QueryString["sid"] %>,<%=Request.QueryString["emp"]??"0" %>,<%=Request.QueryString["dis"]??"0" %>)"><span style="color: #ff5c67; font-weight: bold; font-size: 1.2em;">-￥<%=mydis.Money %> <i class="icon-right" style="color: black;"></i></span><i class="icon-i icon-coupon2"></i></p>
                            <%}
                              else
                              { %>
                            <p onclick="selectdis('<%=Request.QueryString["time"] %>',<%=Request.QueryString["storeid"] %>,<%=Request.QueryString["pnum"] %>,<%=Request.QueryString["jssex"] %>,<%=Request.QueryString["sid"] %>,<%=Request.QueryString["emp"]??"0" %>,<%=Request.QueryString["dis"]??"0" %>)"><span><%=UserDisList.Count==0?"无可用优惠券":UserDisList.Count+"张优惠券可用" %> <i class="icon-right"></i></span><i class="icon-i icon-coupon2"></i></p>
                            <%} %>
                        </li>
                    </ul>
                </div>



                <div class="section-box">
                    <div class="box-item remark-item">
                        <i>备注</i>
                        <div class="remark-wrap">
                            <textarea name="postscript" placeholder="有什么特殊要求可备注" id="remark" rows="3" style="resize: none"></textarea>
                        </div>
                    </div>
                </div>

                <div class="order-info-remind" style="display: none;">
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
                <li class="orderNum">实付<span class="theme-color sumMoney">￥<i><%=(price-(mydis==null?0:mydis.Money))>0?(price-(mydis==null?0:mydis.Money)):0 %></i></span></li>
                <li class="sumNum" data-num="3" data-duration="240"><span><%=servicenum %>个项目</span><span><%=time %>分钟</span></li>
                <li><a class="btn-normal bookBtn" href="javascript:;" id="bookall">立即下单</a></li>
            </ul>
        </div>
        <input type="hidden" id="storeid" value="<%=Request.QueryString["storeid"] %>" />
        <input type="hidden" id="time" value="<%=Request.QueryString["time"] %>" />
        <input type="hidden" id="pnum" value="<%=Request.QueryString["pnum"] %>" />
        <input type="hidden" id="empid" value="<%=emp==null?0:emp.ID %>" />
        <input type="hidden" id="jssex" value="<%=Request.QueryString["jssex"] %>" />
        <input type="hidden" id="emp" value="<%=Request.QueryString["emp"] %>" />
        <input type="hidden" id="dis" value="<%=Request.QueryString["dis"] %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="js/Message2.js"></script>
<%--<script type="text/javascript" src="js/function.js"></script>--%>
<script>
    $("#bookall").click(function () {
        if ($("#userName").val() == "") {
            alert("请输入姓名");
        }
        else if ($("#iphone").val() == "" || $("#iphone").val().length != 11) {
            alert("请输入正确的手机号码");
        }
        else {
            $.ajax({
                type: "Get",
                url: "orderok.aspx?para=tj",
                data: { name: $("#userName").val(), phone: $("#iphone").val(), storeid: $("#storeid").val(), time: $("#time").val(), pnum: $("#pnum").val(), empid: $("#empid").val(), remark: $("#remark").val(), jssex: $("#jssex").val(), dis: $("#dis").val() },
                dataType: 'json',
                async: false,
                beforeSend: function () {

                },
                success: function (data) {
                    if (data.orderid == 0) {
                        location = "dingdan.aspx";
                    }
                    else if (data.orderid == 1) {
                        $.MsgBox.Alert("小时光", "下单成功", function () {
                            location = "dingdan.aspx";
                        });
                    }
                    else if (data.orderid == -2) {
                        alert("技师不足");
                    }
                    else if (data.orderid == -3) {
                        alert("指定的技师被占用");
                    }
                    else if (data.orderid == -4) {
                        alert("优惠券不存在");
                    }
                    else if (data.orderid == -5) {
                        alert("优惠券已使用或已过期");
                    }
                    else if (data.orderid == -6) {
                        alert("非法优惠券");
                    }
                    else {
                        location = "pay.aspx?oid=" + data.orderid;
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    });

    function selectjs(time, storeid, num, jssex, sid, emp, dis) {
        window.location = "jishi.aspx?time=" + time + "&storeid=" + storeid + "&pnum=" + num + "&username=" + $("#userName").val() + "&phone=" + $("#iphone").val() + "&jssex=" + jssex + "&sid=" + sid + "&emp=" + emp + "&dis=" + dis;
    }

    function selectdis(time, storeid, num, jssex, sid, emp, dis) {
        window.location = "../discount/seldis.aspx?time=" + time + "&storeid=" + storeid + "&pnum=" + num + "&username=" + $("#userName").val() + "&phone=" + $("#iphone").val() + "&jssex=" + jssex + "&sid=" + sid + "&emp=" + emp + "&dis=" + dis;
    }
</script>
