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
<link type="text/css" rel="stylesheet" href="css/ordertime.css?v=3"/>
<title>小时光Massage</title>  <script src="js/tz.js"></script>
    <style>
        .order-list-box {
            display: block;
            overflow: hidden;
        }

            .order-list-box ul li {
                margin-bottom: 10px;
                display: block;
                background: #fff;
                border-radius: 5px;
                border: 1px solid #f3f3f3;
            }

                .order-list-box ul li p {
                    border-bottom: 1px solid #f3f3f3;
                    padding: 15px 0 15px 10px;
                    overflow: hidden;
                    line-height: 24px;
                    color: #999;
                    font-size: 12px;
                    position: relative;
                }

                    .order-list-box ul li p img {
                        float: left;
                        margin-right: 10px;
                        width: 60px;
                        height: 60px;
                        border-radius: 10px;
                        margin-left: 3px;
                    }

                    .order-list-box ul li p span {
                        float: right;
                        color: #999;
                        font-size: 12px;
                        margin-right: 5px;
                    }

                    .order-list-box ul li p strong {
                        display: block;
                        color: #535353;
                        font-size: 1.2em;
                    }

        .address-txt {
            font-size: 14px;
            color: #535353;
        }

        .order-list-box .adminInfo {
            position: absolute;
            right: 10px;
            top: 12px;
            display: inline-block;
            color: #ff5c67;
            line-height: 30px;
        }

            .order-list-box .adminInfo img {
                float: inherit;
                width: 30px;
                height: 30px;
                border-radius: 30px;
                float: right;
                margin-left: 10px;
            }

        .mei-time-rili {
            overflow: hidden;
            background: #fff;
            display: block;
            color: #394a9d;
            margin-bottom: 10px;
        }

            .mei-time-rili ul {
                width: 100%;
                display: block;
                overflow: hidden;
            }

                .mei-time-rili ul li {
                    float: left;
                    width: 14.28%;
                    text-align: center;
                    line-height: 22px;
                    padding: 8px 0;
                    font-size: 12px;
                    font-family: Arial, Helvetica, sans-serif;
                    display: block;
                    border: none;
                    border-radius: 0;
                    margin: 0;
                }

                    .mei-time-rili ul li.curDate {
                        background: #ff5c67;
                        color: #fff;
                    }

                    .mei-time-rili ul li span {
                        width: 30px;
                        height: 30px;
                        overflow: hidden;
                        display: block;
                        margin: 0 auto;
                        text-align: center;
                    }

                        .mei-time-rili ul li span.qiandao {
                            background: #ff7200;
                            color: #fff;
                            border-radius: 30px;
                        }

        .time-rili-bg {
            color: #666;
            display: block;
            border-bottom: 2px solid #ff5c67;
            box-shadow: 0 5px 10px #ddd;
        }

            .time-rili-bg ul li {
                line-height: 20px;
                font-size: 12px;
                color: #666;
            }

                .time-rili-bg ul li bdo {
                    display: block;
                }

        .mei-box-time {
            display: block;
            overflow: hidden;
        }

            .mei-box-time span {
                float: left;
                width: 25%;
                display: block;
            }

                .mei-box-time span em {
                    margin: 5px;
                    height: 29px;
                    line-height: 29px;
                    border: 1px solid #fff;
                    color: #333;
                    font-size: 12px;
                    font-style: normal;
                    text-align: left;
                    padding-left: 20px;
                    display: block;
                    font-family: Arial, Helvetica, sans-serif;
                }

                    .mei-box-time span em.curhui {
                        color: #ccc;
                    }

                    .mei-box-time span em.curbg {
                        border: 1px solid #ff5c67;
                        border-radius: 5px;
                        color: #ff5c67;
                        position: relative;
                    }

                        .mei-box-time span em.curbg:after {
                            content: '';
                            position: absolute;
                            right: 0;
                            bottom: 0;
                            border-bottom: 5px solid #ff5c67;
                            border-right: 5px solid #ff5c67;
                            border-left: 5px solid #fff;
                            border-top: 5px solid #fff;
                            display: block;
                            z-index: 99;
                        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
  <!--begin header-->
<div class="meiheader">
  <div class="askboxcon">
    <a  href="javascript:;" onclick="window.location='order.aspx?id=<%=Request.QueryString["sid"] %>&pnum=<%=Request.QueryString["pnum"] %>&jssex=<%=Request.QueryString["jssex"] %>'"><span class="icon-left"></span></a>
    <h2>选择时间</h2> 
    <a href="javascript:void(0);;"><span id="navRight" class="icon-lnk"></span></a>   
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
    <%--<div class="order-address">
        <p><em><i class="icon-right"></i></em><strong><span class="icon-location"></span>静安寺</strong></p>
    </div>--%>
    
    <div class="order-list-box">
       <ul>
          <li>
              <%if (store != null)
                { %>
             <p><span onclick="window.location='tel:17621160808;'"><i class="icon-tel icon-i-right"></i></span><img src="image/md.jpg"><strong><%=store.NAME %></strong>距离：2.5km</p>
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
         <input type="hidden" id="pnum" value="<%=Request.QueryString["pnum"] %>" />
        <input type="hidden" id="jssex" value="<%=Request.QueryString["jssex"] %>"
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
            url: "OrderTime.aspx?para=tj&date=" + n + "&s=" + $("#storeid").val() + "&pnum=" + $("#pnum").val() + "&jssex=" + $("#jssex").val(),
            dataType: 'html',
            async: false,
            beforeSend: function () {

            },
            success: function (data) {
                //alert(data);
                //var data = JSON.parse(json);
                $('.mei-box-time').html(data);
                $('.mei-box-time span').find('.norhui').on('click', function () {
                    $('.mei-box-time span em').removeClass('curbg');
                    $(this).addClass('curbg');
                })
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

        rilihtml += '<li>周' + a[week] + '<bdo>' + month + '.' + day + '</bdo><input type="hidden" value="' + date.getFullYear() + '"></li>';
    }

    $('.time-rili-bg ul').html('<li class="curDate">今天<bdo>' + months + '.' + days + '</bdo><input type="hidden" value="' + now.getFullYear() + '"></li>' + rilihtml)

    showriqi($($('.time-rili-bg ul li')[0]).find('input').val() + "." + $($('.time-rili-bg ul li')[0]).find('bdo').html());
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
        location = "orderok.aspx?time=" + $('.time-rili-bg ul li.curDate').find("input").val() + "." + $('.time-rili-bg ul li.curDate').find("bdo").html() + " " + $(obj).html() + "&storeid=" + $("#storeid").val() + "&pnum=" + $("#pnum").val() + "&sid=" + $("#sid").val() + "&jssex=" + $("#jssex").val();
        //console.log($('.time-rili-bg ul li.curDate').find("input").val() + "." + $('.time-rili-bg ul li.curDate').find("bdo").html() + " " + $(obj).html());
        //console.log($(obj).html());
    }

    function yuyue2(msg) {
        alert(msg);
    }
</script>
