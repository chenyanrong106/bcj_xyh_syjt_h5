<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderTime.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.OrderTime" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /> 
<meta content="yes" name="apple-mobile-web-app-capable" /> 
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
<meta name="apple-mobile-web-app-title" content="小时光Massage"><!-- uc强制竖屏 -->
<meta name="screen-orientation" content="portrait"><!-- UC强制全屏 -->
<meta name="full-screen" content="yes"><!-- UC应用模式 -->
<meta name="browsermode" content="application"><!-- QQ强制竖屏 -->
<meta name="x5-orientation" content="portrait"><!-- QQ强制全屏 -->
<meta name="x5-fullscreen" content="true"><!-- QQ应用模式 -->
<meta name="x5-page-mode" content="app">
<meta content="telephone=no" name="format-detection" />
<link type="text/css" rel="stylesheet" href="css/index.css"/>
<title>小时光Massage</title>
</head>
<body>
    <form id="form1" runat="server">
  <!--begin header-->
<div class="meiheader">
  <div class="askboxcon">
    <a href="order.aspx"><span class="icon-left"></span></a>
    <h2>预约单</h2> 
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
<div class="meiWapper">  
     <div class="order-content">
    <div class="order-address">
        <p><em><i class="icon-right"></i></em><strong><span class="icon-location"></span>静安寺</strong></p>
    </div>
    
    <div class="order-list-box">
       <ul>
          <li>
              <%if(store!=null){ %>
             <p><span><i class="icon-tel icon-i-right"></i></span><img src="image/tu00.jpg"><strong><%=store.NAME %></strong>距离：2.5km</p>
             <p><span><i class="icon-right"></i></span><i class="icon-i icon-store"></i><bdo class="address-txt"><%=store.ADDRESS %></bdo></p>
              <%} %>
             <p><span class="xuanzeTime">请选择服务时间 <i class="icon-down"></i></span><i class="icon-i icon-service-time"></i></p>
            <div class="riqi-check">
            <div class="mei-time-rili time-rili-bg">
                <ul>
                  <!-- <li class="curDate">今天<bdo>2.20</bdo></li>
                   <li>周一<bdo>2.21</bdo></li>
                   <li>周二<bdo>2.22</bdo></li>
                   <li>周三<bdo>2.23</bdo></li>
                   <li>周四<bdo>2.24</bdo></li>
                   <li>周五<bdo>2.25</bdo></li>
                   <li>周六<bdo>2.26</bdo></li>-->
                </ul>
            </div>  
            
            <div class="mei-box-time">
                <!-- <span><em class="curhui">9:30(满)</em></span>
                 <span><em class="curhui">10:00(满)</em></span>
                 <span><em class="curhui">10:30(满)</em></span>
                 <span><em class="curhui">11:00(满)</em></span>
                <span><em>11:30</em></span>
                <span><em>12:00</em></span>
              -->
            </div>
            </div>
          </li>
          
           
       </ul>
    </div>
    
    </div>
   
   
</div>
<!--end content-->
        <input type="hidden" id="storeid" value="<%=Request.QueryString["id"] %>" />
        <input type="hidden" id="sid" value="<%=Request.QueryString["sid"] %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script type="text/javascript">

    function showriqi(n) {
        //console.log(n);
        //var paiqi = paiqi1 = paiqi2 = '';
        //var Arrman = [[1, 0], [0, 1], [0, 0], [0, 0], [1, 1], [1, 0], [0, 0], [1, 0], [1, 0], [0, 1], [0, 0], [0, 0]];
        //for (var i = 0; i < n; i++) {
        //    if (Arrman[i][0] == 1) {
        //        paiqi1 = '<em class="curhui">' + (i + 9) + ':00(满)</em>';
        //    } else {
        //        paiqi1 = '<em>' + (i + 9) + ':00</em>';
        //    }

        //    if (Arrman[i][1] == 1) {
        //        paiqi2 = '<em class="curhui">' + (i + 9) + ':30(满)</em>';
        //    } else {
        //        paiqi2 = '<em>' + (i + 9) + ':30</em>';
        //    }
        //    paiqi += '<span>' + paiqi1 + '</span><span>' + paiqi2 + '</span>';
        //}

        //$('.mei-box-time').html(paiqi);

        $.ajax({
            type: "Get",
            url: "OrderTime.aspx?para=tj&date=" + n + "&s=" + $("#storeid").val(),
            dataType: 'html',
            async: false,
            beforeSend: function () {
          
            },
            success: function (data) {
                //alert(data);
                //var data = JSON.parse(json);
                $('.mei-box-time').html(data);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
  

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

        rilihtml += '<li>周' + a[week] + '<bdo>' + month + '.' + day + '</bdo><input type="hidden" value="'+date.getFullYear()+'"></li>';
    }

    $('.time-rili-bg ul').html('<li class="curDate">今天<bdo>' + months + '.' + days + '</bdo><input type="hidden" value="' + now.getFullYear() + '"></li>' + rilihtml)

    showriqi($($('.time-rili-bg ul li')[0]).find('input').val()+"."+$($('.time-rili-bg ul li')[0]).find('bdo').html()); 
    $('.time-rili-bg ul li').on('click', function () {
        $(this).addClass('curDate').siblings().removeClass('curDate');
        //console.log($(this).find('bdo').html());
        //console.log($(this).find('input').val());
        //console.log($(this).index());
        showriqi($(this).find('input').val() + "." + $(this).find('bdo').html());
    })


    $('.xuanzeTime i').on('click', function () {
        if ($(this).hasClass('icon-down')) {
            $(this).removeClass('icon-down').addClass('icon-right');
            $(this).parent().parent().next().hide();
        } else {
            $(this).removeClass('icon-right').addClass('icon-down');
            $(this).parent().parent().next().show();
        }
    })

    function yuyue(obj) {
        location = "orderok.aspx?time="+$('.time-rili-bg ul li.curDate').find("input").val() + "." + $('.time-rili-bg ul li.curDate').find("bdo").html() + " " + $(obj).html()+"&storeid="+$("#storeid").val()+"&sid="+$("#sid").val();
        //console.log($('.time-rili-bg ul li.curDate').find("input").val() + "." + $('.time-rili-bg ul li.curDate').find("bdo").html() + " " + $(obj).html());
        //console.log($(obj).html());
    }
</script>
