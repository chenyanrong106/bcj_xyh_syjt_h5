v<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Detail" %>

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
                <a href="index.aspx"><span class="icon-left"></span></a>
                <h2>项目详情</h2>
                <a href="tel:17621160808"><span class="icon-tel"></span></a>
            </div>
        </div>
        <!--end header-->



        <!--begin content-->
        <div class="meiWapper mei-index-bottom">
            <div class="mei-detail-img">
                <img src="image/tuimg.jpg" />
                <p>总销量：532737  <span><i class="icon-red icon-duration"></i>120分钟</span></p>
            </div>
            <div class="chanpin-info">
                <p class="advantage"><span>极致舒缓</span><span>改善睡眠</span><span>排毒养颜</span></p>
                <p><bdo class="shangmen">上门</bdo><bdo class="daodian">到店</bdo>通过配合呼吸，使劲力贯通足尖和手指尖，用手指一碰对方，对方即摔出表演，孙剑云老师试验的对象不是自己的徒弟，而是个练过空手道由于这种功夫对于孙剑云老师来说属于小儿科的玩艺，所以孙剑云老师从不让渲染这类事。</p>
            </div>

            <div class="mei-detail-title">
                <ol class="curbtn"><span>项目详情</span></ol>
                <ol><span>评价（21726）</span></ol>
            </div>
            <div class="mei-detail-info">
                <%if (ps != null)
                  { %>
                <%=ps.REMARK==null?"":ps.REMARK.Replace("/Assets/",PosUrl+"/Assets/") %>
                <%} %>
            </div>


        </div>

        <!--end content-->


        <div class="add-btn-buy" style="display:none;">
            <a href="javascript:;" data-id="112" data-price="399" data-discount="0" data-name="依兰诱惑-胸腹Spa" data-duration="120" data-kind="3">加入
                <br>
                购物袋</a>
        </div>

        <div class="book-box ">
            <ul class="book-items hide">
                <p class="items-title">
                    <span>到店服务</span>
                    <a href="javascript:;" class="clear-btn"><i class="icon-del2"></i>清空</a>
                </p>
                <li>
                    <p class="t-ellipsis">淋巴排毒疗法</p>
                    <p>￥298</p>
                    <p class="set-num"><a class="minus icon-minus-item" href="javascript:void(0);"></a><span class="package-num">2</span><a class="add icon-add-item theme-color" href="javascript:void(0);"></a></p>
                </li>
                <li>
                    <p class="t-ellipsis">海皙曼面部拨筋组合</p>
                    <p>￥568</p>
                    <p class="set-num"><a class="minus icon-minus-item" href="javascript:void(0);"></a><span class="package-num">1</span><a class="add icon-add-item theme-color" href="javascript:void(0);"></a></p>
                </li>
            </ul>

            <ul class="order flex">
                <%if (ps != null)
                  { %>
                <li class="orderNum"><i class="icon-i icon-trolley"></i><span class="totals-num">1</span><span class="theme-color sumMoney">￥<i><%=ps.PRICE %></i></span></li>
                <li class="sumNum" data-num="3" data-duration="240"><span>1个项目</span><span><%=ps.TIME_LEN %>分钟</span></li>
                <li><a class="btn-normal bookBtn" href="javascript:;">预约</a></li>
                <%} %>
            </ul>
        </div>

        <div class="maskbg"></div>

        <div id="confirm">
            <div class="deltip">

                <p class="info">是否清空购物车？</p>
                <a href="javascript:;" class="alert-btn">确定</a>
                <div class="delmask"><i class="icon-shut"></i></div>
            </div>
        </div>
        <input type="hidden" id="sid" value="<%=Request.QueryString["id"] %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script type="text/javascript">


    $('.icon-minus-item').on('click', function () {
        var shuzhi = parseInt($(this).next('.package-num').aspx());

        shuzhi = shuzhi - 1;
        $(this).next('.package-num').aspx(shuzhi);
        if (shuzhi <= 0) {
            $(this).parent().parent().remove();
        }
        var numli = $(".book-items li").length;
        if (numli == 0) {
            $('.book-box').hide();
            $('.maskbg').hide();
        }


    })

    $('.icon-add-item').on('click', function () {
        var shuzhi = parseInt($(this).siblings('.package-num').aspx());
        shuzhi = shuzhi + 1;
        $(this).siblings('.package-num').aspx(shuzhi);
        if (shuzhi <= 0) {
            $(this).parent().parent().remove();
        }

    })



    var loginflag = true;

    //$('.orderNum').on('click', function () {
    //    if (loginflag == true) {
    //        $('.maskbg').show();
    //        $('.book-items').show().removeClass('slideOutDown').addClass('slideInUp');
    //        loginflag = false;
    //    } else {
    //        $('.maskbg').hide();
    //        $('.book-items').removeClass('slideInUp').addClass('slideOutDown');
    //        setTimeout(function () { $('.book-items').hide() }, 100);
    //        loginflag = true;
    //    }

    //})


    $('.clear-btn').on('click', function () {
        $('#confirm').show();
    })
    $('.icon-shut').on('click', function () {

        $('#confirm').hide();
    })
    $('.alert-btn').on('click', function () {
        $('.book-items li').remove();
        $('.book-box').hide();
        $('.maskbg').hide();
        $('#confirm').hide();
    })


    $('.bookBtn').on('click', function () {
        window.location.href = 'order.aspx?id='+$("#sid").val();
    })
</script>
