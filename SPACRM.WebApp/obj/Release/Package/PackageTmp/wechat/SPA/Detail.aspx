<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Detail" %>

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
    <title>小时光Massage</title>  <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="window.location='index.aspx'"><span class="icon-left"></span></a>
                <h2>项目详情</h2>
                <a href="tel:17621160808"><span class="icon-tel"></span></a>
            </div>
        </div>
        <!--end header-->



        <!--begin content-->
        <div class="meiWapper mei-index-bottom">
            <%if (ps != null)
              { %>
            <div class="mei-detail-img">

                <img src="<%=string.IsNullOrEmpty(ps.TopImg)?"image/tuimg.jpg":WebUrl+ps.TopImg %>" />
                <p>总销量：<%=ps.TIME_LEN+ps.PayCount %>  <span><i class="icon-red icon-duration"></i><%=ps.TIME_LEN %>分钟</span></p>

            </div>
            <div class="chanpin-info">
                <p class="advantage"><%=GetBQ(ps.BQ) %></p>
                <p><bdo class="daodian">到店</bdo><%=ps.DDSM %></p>
            </div>

            <div class="mei-detail-title">
                <ol class="curbtn"><span>项目详情</span></ol>
                <ol><span>评价（<%=ps.PJCount %>）</span></ol>
            </div>
            <%} %>
            <div id="mei-datail-content">
                <ol>
                    <div class="mei-detail-info">
                        <%if (ps != null)
                          { %>
                        <%-- <%=ps.REMARK %>--%>
                        <%=ps.REMARK==null?"":ps.REMARK.Replace("/Assets/",PosUrl+"/Assets/") %>
                        <%} %>
                    </div>
                </ol>
                <ol style="display: none;">
                    <div class="mei-detail-pingjia">
                        <ul>
                            <%foreach (var pj in pjlist)
                              {
                            %>
                            <li>
                                <div class="touxiang-box">
                                    <span><%=GetStar(pj.ZT) %></span><strong>
                                        <img src="<%=(pj.DownPic == null ? pj.headimgurl : pj.DownPic.Replace("~", "")) %>"><%=pj.Nickname %></strong>
                                </div>
                                <p><%=pj.Remark %></p>
                                <p><span><%=pj.NAME %><img src="<%=string.IsNullOrEmpty(pj.Img)?"image/jishi.jpg":WebUrl+pj.Img %>"></span><em><%=pj.CreateTime.Value.ToString("yyyy.MM.dd HH:mm:ss") %></em></p>
                            </li>
                            <%} %>
                        </ul>
                    </div>
                </ol>
            </div>

        </div>

        <!--end content-->


        <div class="add-btn-buy" style="display: none;">
            <a href="javascript:;" data-id="112" data-price="399" data-discount="0" data-duration="120" onclick="AddCar(<%=Request.QueryString["id"] %>)" data-kind="3">加入
                <br>
                购物袋</a>
        </div>

        <div class="book-box">

            <ul class="order flex">
                <%if (ps.PRICE == 9.9m)
                  { %>
                <li style="width: 100%; padding-left: 0;"><a class="btn-normal bookBtn" href="javascript:;" onclick="yuyue()">试营业尊享预约</a></li>
                <%}
                  else
                  { %>
                <li style="width: 100%; padding-left: 0;"><a class="btn-normal bookBtn" href="javascript:;" onclick="yuyue()">预约</a></li>
                <%} %>
            </ul>
        </div>

        <%-- <div class="book-box" style="border-top: none;">

            <%= GetHtml(clist) %>
        </div>--%>

        <div class="maskbg"></div>

        <div id="confirm">
            <div class="deltip">

                <p class="info">是否清空购物车？</p>
                <a href="javascript:;" class="alert-btn">确定</a>
                <div class="delmask"><i class="icon-shut"></i></div>
            </div>
        </div>
        <input type="hidden" id="sid" value="<%=Request.QueryString["id"] %>" />
        <%if (ps != null)
          { %>
        <input type="hidden" id="ztitle" value="<%=ps.NAME %>" />
        <input type="hidden" id="ftitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
        <%} %>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script type="text/javascript">

    $('.mei-detail-title ol').on('click', function () {
        $(this).addClass('curbtn').siblings().removeClass('curbtn');
        $('#mei-datail-content ol').hide();
        $('#mei-datail-content ol').eq($(this).index()).show();
    })

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

    $('.orderNum').on('click', function () {


    })

    function openline() {
        if (loginflag == true) {
            $('.maskbg').show();
            $('.book-items').show().removeClass('slideOutDown').addClass('slideInUp');
            loginflag = false;
        } else {
            $('.maskbg').hide();
            $('.book-items').removeClass('slideInUp').addClass('slideOutDown');
            setTimeout(function () { $('.book-items').hide() }, 100);
            loginflag = true;
        }
    }


    $('.clear-btn').on('click', function () {
        $('#confirm').show();
    })
    $('.icon-shut').on('click', function () {

        $('#confirm').hide();
    })
    $('.alert-btn').on('click', function () {
        $.ajax({
            type: "Get",
            url: "detail.aspx?para=qk",
            dataType: 'html',
            async: false,
            beforeSend: function () {

            },
            success: function (data) {
                $('.book-box').html(data);
                if (loginflag == false) {
                    $('.maskbg').show();
                    $('.book-items').show().removeClass('slideOutDown').addClass('slideInUp');
                }
                //$('.book-box').hide();
                //$('.maskbg').hide();
                $('#confirm').hide();
            },
            error: function (error) {
                console.log(error);
            }
        });

    })


    //$('.bookBtn').on('click', function () {
    //    window.location.href = 'XuanZe.aspx?id=' + $("#sid").val();
    //})

    function AddCar(sid) {
        $.ajax({
            type: "Get",
            url: "detail.aspx?para=add&sid=" + sid,
            dataType: 'html',
            async: false,
            beforeSend: function () {

            },
            success: function (data) {
                //alert(data);
                //var data = JSON.parse(json);
                $('.book-box').html(data);
                if (loginflag == false) {
                    $('.maskbg').show();
                    $('.book-items').show().removeClass('slideOutDown').addClass('slideInUp');
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    function DelCar(sid) {
        $.ajax({
            type: "Get",
            url: "detail.aspx?para=del&sid=" + sid,
            dataType: 'html',
            async: false,
            beforeSend: function () {

            },
            success: function (data) {
                //alert(data);
                //var data = JSON.parse(json);
                $('.book-box').html(data);
                if (loginflag == false) {
                    $('.maskbg').show();
                    $('.book-items').show().removeClass('slideOutDown').addClass('slideInUp');
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function DelCars() {
        $('#confirm').show();
    }

    function yuyue() {
        $.ajax({
            type: "Get",
            url: "detail.aspx?para=add&qk=1&sid=" + $("#sid").val(),
            dataType: 'html',
            async: false,
            beforeSend: function () {

            },
            success: function (data) {
                window.location.href = 'XuanZe.aspx?id=' + $("#sid").val();
            },
            error: function (error) {
                console.log(error);
            }
        });

    }
</script>
