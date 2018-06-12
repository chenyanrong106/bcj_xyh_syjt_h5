<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZhiFu.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.ZhiFu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="小时光">
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
                <a href="newindex.aspx"><span class="icon-left"></span></a>
                <h2>在线买单</h2>
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
        <div class="meiWapper">
            <div class="order-content">
                <div class="order-address">
                    <p class="pline"><strong><i class="qian"></i>
                        <input name="" type="text" class="inputqian" placeholder="消费总额"></strong><em class="yuanzi">元</em></p>
                    <p>
                        <em><i class="icon-right"></i></em><strong><span class="icon-store"></span>
                            <select class="dropdown-select" id="storeid">
                                <option value="0">选择门店</option>
                                <%foreach (var o in list)
                                  {
                                %>
                                <option value="<%=o.ID %>" <%if (Request.QueryString["storeid"] == o.ID.ToString())
                                                             { %>selected<%} %>><%=o.NAME %></option>
                                <% } %>
                            </select></strong>
                    </p>
                    <p>
                        <em><i class="icon-right"></i></em><strong><span class="icon-beautician"></span>
                            <select class="dropdown-select" id="jsid">
                                <option value="0">选择服务人员</option>
                                <%foreach (var o in oelist)
                                  {
                                %>
                                <option value="<%=o.ID %>"><%=o.NAME %></option>
                                <% } %>
                            </select></strong>
                    </p>
                </div>
                <div class="pay-order-info">
                    <p style="display: none;">
                        <strong>优惠券：</strong> <span class="fr">
                            <select class="dropdown-select dropdown-quan" dir="rtl">
                                <option>选择优惠</option>
                                <option>10元优惠券</option>
                                <option>20元现金抵用券</option>
                                <option>50元现金抵用券</option>
                            </select><em><i class="icon-right"></i></em></span>
                    </p>
                    <p><strong>实付金额：</strong> <bdo class="fr">￥0.00</bdo></p>
                </div>


            </div>
        </div>
        <!--end content-->

        <div class="paybtnbg" style="position: static;">
            <a href="javascript:;" />
            <div class="paybtn">确认买单</div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script>
    $(".inputqian").change(function () {
        if ($(".inputqian").val() != "") {
            $(".fr").html("￥" + $(".inputqian").val());
        }
        else {
            $(".fr").html("￥0");
        }
    });

    $("#storeid").change(function () {
        selectmd();
    });

    function selectmd() {
        if ($("#storeid").val() != 0) {
            $.ajax({
                type: "Get",
                url: "zhifu.aspx?para=md",
                data: { storeid: $("#storeid").val() },
                dataType: 'html',
                async: false,
                beforeSend: function () {

                },
                success: function (data) {
                    $("#jsid").html(data);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    }

    $(".paybtn").click(function () {
        if ($(".inputqian").val() == "") {
            alert("请输入金额");
        }
        else if ($("#storeid").val() == 0) {
            alert("请选择门店");
        }
        else if ($("#jsid").val() == 0) {
            alert("请选择服务人员");
        }
        else {
            $.ajax({
                type: "Get",
                url: "zhifu.aspx?para=tj",
                data: { je: $(".inputqian").val(), js: $("#jsid").val(), storeid: $("#storeid").val() },
                dataType: 'json',
                async: false,
                beforeSend: function () {

                },
                success: function (data) {
                    if (data.orderid < 0) {
                        alert("异常");
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
    selectmd();
</script>
