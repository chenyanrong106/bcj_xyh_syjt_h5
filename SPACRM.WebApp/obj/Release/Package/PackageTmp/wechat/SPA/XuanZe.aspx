<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XuanZe.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.XuanZe" %>

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
    <title>小时光Massage</title>  <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="window.location='<%=Request.QueryString["url"]==null?"detail.aspx?id="+Request.QueryString["id"]:Request.QueryString["url"] %>'"><span class="icon-left"></span></a>
                <h2>选择项目</h2>
                <a href="tel:17621160808"><span class="icon-tel"></span></a>
            </div>
        </div>
        <!--end header-->


        <!--begin content-->
        <div class="meiWapper mei-index-bottom">
            <div class="mei-index-items xuanze-title">
                <h2>已选项目</h2>
                <ul>
                    <%foreach (var c in clist)
                      {%>
                    <li id="li<%=c.ServiceID %>">
                        <a href="detail.aspx?id=<%=c.ServiceID %>">
                            <dt class="fl">
                                <img src="<%=string.IsNullOrEmpty(c.BookImg)?"https://dimg.365vmei.cn/uploads/project/big_582438f2366b2.jpg":WebUrl+ c.BookImg %>" alt="<%=c.ServiceName %>" onerror="javascript:this.src='http://static.emeidaojia.com/static/pro-default.png';"></dt>
                        </a>
                        <dd class="fl">
                            <h3><%=c.ServiceName %></h3>

                            <p class="introduce">
                                <span class="duration"><i class="icon-red icon-duration"></i><%=c.TimeLeng %>分钟</span>
                            </p>
                            <p class="introduce">
                                <span class="new-price">￥<span><%=c.ServicePrice %></span></span>
                                <span class="old-price">￥<%=c.ServiceOldPrice %></span>

                            </p>
                        </dd>

                        <div class="xuanze-addbtn">
                            <p><a class="icon-jian-item" id="mins100" href="javascript:;" onclick='DelCar(<%=c.ServiceID %>)'></a><span class="package-num num-input" id="snum<%=c.ServiceID %>"><%=c.Num %></span><a class="icon-jia-item" id="add100" href="javascript::;" onclick='AddCar(<%=c.ServiceID %>)'></a></p>
                        </div>
                    </li>
                    <%  } %>
                </ul>
            </div>



            <div class="mei-index-items xuanze-title">
                <h2>选择下面一个套餐一起体验</h2>
                <ul>
                    <% foreach (var s in servicelist)
                       {%>
                    <li>
                        <a href="detail.aspx?id=<%=s.ID %>">
                            <dt class="fl">
                                <img src="<%=string.IsNullOrEmpty(s.BookImg)?"https://dimg.365vmei.cn/uploads/project/big_582438f2366b2.jpg":WebUrl+ s.BookImg %>" alt="<%=s.NAME %>" onerror="javascript:this.src='http://static.emeidaojia.com/static/pro-default.png';"></dt>
                        </a>
                        <dd class="fl">
                            <h3><%=s.NAME %></h3>

                            <p class="introduce">
                                <span class="duration"><i class="icon-red icon-duration"></i><%=s.TIME_LEN %>分钟</span>
                            </p>
                            <p class="introduce">
                                <span class="new-price">￥<span><%=s.PRICE %></span></span>
                                <span class="old-price">￥<%=s.PRICE %></span>

                            </p>
                        </dd>

                        <div class="add-trolley">
                            <a href="javascript:;" class="icon-add-item theme-color" data-name="小时光" data-price="218" data-duration="120" data-id="195" data-kind="1" onclick="AddCar(<%=s.ID %>)"></a>
                        </div>
                    </li>
                    <% } %>
                </ul>
            </div>

        </div>
        <!--end content-->


        <div class="book-box ">
            <%--  <%if (clist.Count > 0)
              {
            %>--%>
            <%= GetHtml(clist) %>
            <%-- <%} %>--%>
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


    var loginflag = true;

    //$('.orderNum').on('click', function () {
    //    if (loginflag == true) {
    //        $('.maskbg').show();
    //        $('.book-items').show().removeClass('slideOutDown').addClass('slideInUp');
    //        // addnum();
    //        loginflag = false;
    //    } else {
    //        $('.maskbg').hide();
    //        $('.book-items').removeClass('slideInUp').addClass('slideOutDown');
    //        setTimeout(function () { $('.book-items').hide() }, 100);
    //        loginflag = true;
    //    }

    //})

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


    //$('.clear-btn').on('click', function () {
    //    $('#confirm').show();
    //})
    //$('.icon-shut').on('click', function () {

    //    $('#confirm').hide();
    //})
    //$('.alert-btn').on('click', function () {
    //    $('.book-items li').remove();
    //    $('.book-box').hide();
    //    $('.maskbg').hide();
    //    $('#confirm').hide();
    //})


    //$('.bookBtn').on('click', function () {
    //    window.location.href = 'order.aspx?id=' + $("#sid").val();
    //})



    //$('.add-trolley a').on('click', function () {

    //    var anum = parseInt($(this).attr('data-kind'));
    //    var pro_name = $(this).attr('data-name');
    //    var pro_price = $(this).attr('data-price');
    //    var pro_id = $(this).attr('data-id');
    //    var pro_time = $(this).attr('data-duration');
    //    var arrid = [];

    //    $('.book-items li').find('.minusNum').each(function (index, element) {
    //        arrid.push($(this).attr('id'));
    //    });



    //    if ($.inArray("jian" + pro_id, arrid) == -1) {
    //        var prolist = '<li data-price="' + pro_price + '" data-duration="' + pro_time + '">\
    //			<p class="t-ellipsis">'+ pro_name + '</p><p>￥' + pro_price + '</p>\
    //			<p class="set-num"><a class="minusNum icon-minus-item" id="jian'+ pro_id + '" href="javascript:minsnum(' + pro_id + ');"></a><span class="package-num">' + anum + '</span><a class="addNum icon-add-item theme-color" id="jia' + pro_id + '" href="javascript:addnum(' + pro_id + ');"></a></p>\
    //			</li>';
    //        $('.book-items').append(prolist);
    //    } else {
    //        anum = parseInt($('#jian' + pro_id).parent().find('.package-num').html());
    //        anum++;
    //        $('#jian' + pro_id).parent().find('.package-num').html(anum);
    //    }


    //    if ($('.book-items li').length > 0) {
    //        $('.book-box').show();
    //        $('.book-items').hide();
    //        $('.book-items').removeClass('slideInUp');
    //        loginflag = true;
    //    }
    //    qiuhe();




    //})

    //初始化
    //qiuhe();

    function qiuhe() {
        var sumnum = totalPrice = danjia = totime = totalTime = 0;
        $('.book-items li').each(function (index, element) {
            sumnum += parseInt($(this).find('.package-num').html());

            totalPrice += parseInt($(this).attr('data-price')) * parseInt($(this).find('.package-num').html());
            totalTime += parseInt($(this).attr('data-duration')) * parseInt($(this).find('.package-num').html());

            //console.log(sumnum,totalPrice,totalTime);

        });
        $('.sumNum').attr('data-num', sumnum);
        $('.sumMoney').find('i').html(totalPrice);

        $('.sumNum').find('span').eq(0).html(sumnum + '个项目');
        $('.sumNum').find('span').eq(1).html(totalTime + '分钟');
        $('.totals-num').html(sumnum);

    }

    function minsnumTop(id) {
        var shuzhi = parseInt($('#mins' + id).parent().find('.package-num').html());
        shuzhi = shuzhi - 1;
        if (shuzhi <= 0) {
            shuzhi = 1;
        }
        $('#mins' + id).parent().find('.package-num').html(shuzhi);
        $('#jian' + id).parent().find('.package-num').html(shuzhi);
        qiuhe();
    }

    function addnumTop(id) {

        var shuzhi2 = parseInt($('#add' + id).parent().find('.package-num').html());
        shuzhi2 = shuzhi2 + 1;
        $('#add' + id).parent().find('.package-num').html(shuzhi2);
        $('#jia' + id).parent().find('.package-num').html(shuzhi2);
        qiuhe();
    }


    function minsnum(id) {
        var shuzhi = parseInt($('#jian' + id).parent().find('.package-num').html());
        shuzhi = shuzhi - 1;

        $('#jian' + id).parent().find('.package-num').html(shuzhi);
        $('#mins' + id).parent().find('.package-num').html(shuzhi);
        if (shuzhi <= 0) {
            $('#jian' + id).parent().parent().remove();
        }
        var numli = $(".book-items li").length;
        if (numli == 0) {
            $('.book-box').hide();
            $('.maskbg').hide();
        }
        qiuhe();
    }

    function addnum(id) {

        var shuzhi2 = parseInt($('#jia' + id).parent().find('.package-num').html());
        if (shuzhi2 <= 0) {
            $('#jia' + id).parent().parent().remove();
        }
        shuzhi2 = shuzhi2 + 1;
        $('#jia' + id).parent().find('.package-num').html(shuzhi2);
        $('#add' + id).parent().find('.package-num').html(shuzhi2);
        qiuhe();
    }



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
                $("#snum" + sid).html(parseInt($("#snum" + sid).html()) + 1);
                window.location = window.location;
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
                if (parseInt($("#snum" + sid).html()) > 0) {
                    $("#snum" + sid).html(parseInt($("#snum" + sid).html()) - 1);
                }
                if ($("#snum" + sid).html() == 0) {
                    $("#li" + sid).remove();
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
        if ($(".totals-num").html() == "0") {
            alert("请至少添加一个大项目");
        }
        else {
            window.location.href = 'order.aspx?id=' + $("#sid").val();
        }
    }
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

</script>

