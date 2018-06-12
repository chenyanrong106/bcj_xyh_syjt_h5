<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TuiJian.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.TuiJian" %>

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
    <link type="text/css" rel="stylesheet" href="css/index.css" />
    <title>小时光Massage</title>  <script src="js/tz.js"></script>
    <style>
        .share {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: rgba(0,0,0,.8);
            display: none;
            overflow: hidden;
        }

        .sharebg {
            background: url(image/fx.png) no-repeat center 0;
            background-size: 60%;
            width: 100%;
            height: 100%;
            display: block;
            position: absolute;
            left: 0;
            top: 10%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="index.aspx"><span class="icon-left"></span></a>
                <h2>推荐有奖</h2>
                <a href="javascript:void(0);"><span id="navRight" class="icon-lnk"></span></a>
            </div>

            <ul class="lnks">
                <li><a href="index.aspx"><span class="icon-logo-w"></span>我要预约</a></li>
                <li><a href="dingdan.aspx"><span class="icon-order_other"></span>我的订单</a></li>
                <li><a href="#"><span class="icon-beautician"></span>我的技师</a></li>
                <li><a href="wo.aspx"><span class="icon-personal"></span>个人中心</a></li>
                <li><a href="tel:17621160808"><span class="icon-tel"></span>呼叫客服</a></li>
            </ul>
        </div>
        <!--end header-->



        <!--begin content-->
        <div class="meiWapper">
            <div class="tuijian-content">
                <div class="mei-detail-img">
                    <img src="image/bigimg.jpg" />
                </div>
                <div class="mei-tuijian-tt">
                    <p>
                        <span>向您朋友推荐小时光Massage</span><br />
                        <strong>您将获得我们赠送的20元奖励</strong>
                        （可累计兑换成卡值进行消费抵扣）
                    </p>
                </div>
                <div class="mei-tuijian-tab">
                    <ol><a href="javascript:;" onclick="fx();"><span>去推荐</span></a></ol>
                    <ol><a href="#"><span>活动规则</span></a></ol>
                </div>
            </div>

            <div class="tuijian-content tuijian-padd">
                <h6>累计获得奖励</h6>
                <h2>￥<%=oldex.z %></h2>
                <%--<p>累计奖励￥<%=oldex.z %></p>--%>
                <div>
                    <a href="javascript:;" onclick="alert('开发中')">
                        <div class="paybtn">去兑换</div>
                    </a>
                </div>


            </div>
            <div class="pay-order-info" style="border: none;">
                <a href="mingxi.aspx" >
                    <p><strong>奖励明细</strong> <span class="fr">查看<em><i class="icon-right"></i></em></span></p>
                </a>
            </div>

        </div>
        <!--end content-->

        <div class="share" id="shareBox">
            <div class="sharebg"></div>
        </div>

        <input type="hidden" id="ztitle" value="我在小时光Massage,快来预约吧！" />
        <input type="hidden" id="ftitle" value="我在小时光Massage,快来预约吧！" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script>
    function fx() {
        $(".share").show();
    }


    $(".share").click(function () {
        $(".share").hide();
    });
</script>
