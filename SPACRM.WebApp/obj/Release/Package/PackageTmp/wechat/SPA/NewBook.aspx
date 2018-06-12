<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewBook.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.NewBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="美道家">
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

    <link type="text/css" rel="stylesheet" href="css/newindex.css" />
    <title>小时光Massage</title>
    <style>
        .proBookTitle em {
            float:right;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="newdetail.aspx?id=<%=Request.QueryString["storeid"] %>"><span class="icon-left"></span></a>
                <h2>在线预订</h2>
                <a href="tel:17621160808"><span class="icon-tel"></span></a>
            </div>
        </div>
        <!--end header-->
        <!--begin content-->
        <div class="meiWapper">

            <div class="indexProDetail">

                <%if (service != null)
                  { %>
                <div class="proBookPin">
                    <p>
                        <span><i class="icon-right"></i></span>
                        <img src="<%=WebUrl+service.BookImg %>"><strong><%=service.NAME %></strong><%=service.TIME_LEN %>分钟
                    </p>
                </div>
                <%} %>
                <div class="proBookTitle">
                    <h2>选择预订人数</h2>
                    <ul>
                        <li class="shuzi"><span><bdo class="jiahao22"><a href="javascript:add(3);"><em class="jiahao"></em></a></bdo><bdo>
                            <input type="tel" class="shutxt" value="<%=Request.QueryString["pnum"]??"1" %>" id="carnum3" /></bdo><bdo><a href="javascript:jian(3);"><em class="jianhao"></em></a></bdo></span>人数</li>
                    </ul>
                </div>

                <div class="proBookTitle">
                    <h2>选择到店时间</h2>
                </div>
                <div class="proBookDate">
                    <ul class="progaodu" id="dateBook">
                    </ul>
                    <p align="center"><a href="javascript:void(0)"><span class="moreFuwu">更多到店时间</span></a></p>
                </div>
                <div class="proBookTitle">
                    <ul>
                        <li><a href="javascript:;" onclick="selectjs();"><span class="touxiang">
                            <img src="<%=emp==null?"image/jishi.jpg":WebUrl+emp.Img %>"></span><span style="padding-right: 10px;"><%=emp==null?"默认技师":emp.NAME %></span></a>预订时间：<bdo class="weicheck"><%=Request.QueryString["time2"]==null?"未选择时间":Request.QueryString["time"]+" "+Request.QueryString["time2"] %></bdo></li>
                    </ul>
                </div>


                <div class="proBookTitle proquan">
                    <ul>
                        <%if (mydis != null)
                          {
                        %>
                        <li onclick="selectdis();"><span><i class="icon-right"></i></span>-￥<%=mydis.Money %></li>
                        <%--<p onclick="selectdis('<%=Request.QueryString["time"] %>',<%=Request.QueryString["storeid"] %>,<%=Request.QueryString["pnum"] %>,<%=Request.QueryString["jssex"] %>,<%=Request.QueryString["sid"] %>,<%=Request.QueryString["emp"]??"0" %>,<%=Request.QueryString["dis"]??"0" %>)"><span style="color: #ff5c67; font-weight: bold; font-size: 1.2em;">-￥<%=mydis.Money %> <i class="icon-right" style="color: black;"></i></span><i class="icon-i icon-coupon2"></i></p>--%>
                        <%}
                          else
                          { %>
                        <li onclick="selectdis();"><span><i class="icon-right"></i></span><%=UserDisList.Count==0?"无可用优惠券":UserDisList.Count+"张优惠券可用" %></li>
                        <%--<p onclick="selectdis('<%=Request.QueryString["time"] %>',<%=Request.QueryString["storeid"] %>,<%=Request.QueryString["pnum"] %>,<%=Request.QueryString["jssex"] %>,<%=Request.QueryString["sid"] %>,<%=Request.QueryString["emp"]??"0" %>,<%=Request.QueryString["dis"]??"0" %>)"><span><%=UserDisList.Count==0?"无可用优惠券":UserDisList.Count+"张优惠券可用" %> <i class="icon-right"></i></span><i class="icon-i icon-coupon2"></i></p>--%>
                        <%} %>
                        <%--<li><span><i class="icon-right"></i></span>现金券/抵用券/优惠码</li>--%>
                    </ul>
                </div>

                <div class="proBookTitle">
                    <h2>确认手机号码</h2>
                    <%--<ul>
                        <li>手机号&nbsp;&nbsp;<input name="name" type="text" class="inputTxtPro" maxlength="11" value="<%=Request.QueryString["phone"]==null?(booking==null?"":booking.CUST_MOBILE):Request.QueryString["phone"] %>" style="width: 80%;"></li>
                        <li>称呼&nbsp;&nbsp;
                            <input name="name" type="text"class="inputTxtPro" value="<%=Request.QueryString["username"]==null?(booking==null?"":booking.CUST_NAME):Request.QueryString["username"] %>" style="width: 80%;"></li>
                    </ul>--%>
                    <ul>
                        <li><span style="width:85%;">
                            <input name="name" type="tel" class="inputTxtPro" style="width: 100%; " maxlength="11" id="iphone" value="<%=Request.QueryString["phone"]==null?(booking==null?"":booking.CUST_MOBILE):Request.QueryString["phone"] %>"></span>手机号</li>
                        <li><span class="checkSex" style="width:85%;"><input name="name" type="text" class="inputTxtPro" style="width: 70%;" maxlength="11"  id="userName"  value="<%=Request.QueryString["username"]==null?(booking==null?"":booking.CUST_NAME):Request.QueryString["username"] %>"><%if(booking==null||string.IsNullOrEmpty(booking.CUST_SEX)||booking.CUST_SEX=="先生"){ %> <em>女士</em><em class="cur">先生</em><%}else{ %> <em class="cur">女士</em><em>先生</em><%} %></span>称呼 </li>
                    </ul>
                </div>

                <div class="proBookTitle proquan">
                    <ul>
                        <li>备注(选填)
                            <input name="remark" type="text" class="inputTxtPro" style="width: 75%" id="remark" maxlength="20" placeholder="不超过20个字"></li>
                    </ul>
                </div>



            </div>
            <div class="proListinfo">
                <p>* 提前<span>2小时</span>可退款，订单将保留到开场后<span>15分钟</span></p>
            </div>
        </div>
        <!--end content-->

        <div class="paybtnbg">
            <a href="javascript:;">
                <div class="paybtn">168元（1人预订）去支付</div>
            </a>
        </div>
        <input type="hidden" id="storeid" value="<%=Request.QueryString["storeid"] %>" />
        <input type="hidden" id="time" value="<%=Request.QueryString["time"] %>" />
        <input type="hidden" id="serviceid" value="<%=Request.QueryString["serviceid"] %>" />
        <input type="hidden" id="empid" value="<%=emp==null?0:emp.ID %>" />
        <input type="hidden" id="dis" value="<%=Request.QueryString["dis"] %>" />
        <input type="hidden" id="IsPostBack" value="0" />
        <input type="hidden" id="dismoney" value="<%=mydis==null?0:mydis.Money %>" />
        <%if (service != null)
          { %>
        <input type="hidden" id="timeleng" value="<%=service.TIME_LEN %>" />
        <input type="hidden" id="sprice" value="<%=service.PRICE %>" />
        <%} %>
    </form>
</body>
</html>

<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="js/Message2.js"></script>
<script type="text/javascript">
    function jian(id) {
        num = parseInt(document.getElementById("carnum" + id).value);
        if (num > 1) {
            num = num - 1;
            document.getElementById("carnum" + id).value = num;
        } else {
            document.getElementById("carnum" + id).value = 1;
        }
        SelectTime();
    }
    function add(id) {
        num = parseInt(document.getElementById("carnum" + id).value);
        num = num + 1;
        document.getElementById("carnum" + id).value = num;
        SelectTime();
    }

    $('.checkSex em').on('click', function () {
        $('.checkSex em').removeClass('cur');
        $(this).addClass('cur');
    })




    var moreFlag = true;
    $('.moreFuwu').on('click', function () {
        if (moreFlag == true) {
            $(this).html('收起');
            $(this).addClass('moreFuwuUp');
            $('.proBookDate ul').removeClass('progaodu');
            moreFlag = false;
        } else {
            $(this).html('更多到店时间');
            $(this).removeClass('moreFuwuUp');
            $('.proBookDate ul').addClass('progaodu');
            moreFlag = true;
        }
    })

    $(".paybtn").click(function () {
        if ($("#userName").val() == "") {
            alert("请输入姓名");
        }
        else if ($("#iphone").val() == "" || $("#iphone").val().length != 11) {
            alert("请输入正确的手机号码");
        }
        else if ($(".weicheck").html() == "未选择时间") {
            alert("请选择时间");
        }
        else {
            $.ajax({
                type: "Get",
                url: "newbook.aspx?para=tj",
                data: { name: $("#userName").val(), phone: $("#iphone").val(), storeid: $("#storeid").val(), time: $(".weicheck").html(), pnum: $("#carnum3").val(), empid: $("#empid").val(), remark: $("#remark").val(), jssex: $("#jssex").val(), dis: $("#dis").val(), serviceid: $("#serviceid").val(), sex: $(".checkSex em.cur").html() },
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

    //
    function SelectTime() {
        var price = $("#sprice").val() * $("#carnum3").val();
        if ($("#dismoney").val() > price) {
            price = 0;
        }
        else {
            price = price - $("#dismoney").val();
        }
        $(".paybtn").html(price + "元（" + $("#carnum3").val() + "人预订）去支付");
        $.ajax({
            type: "Get",
            url: "NewBook.aspx?para=time&date=" + $("#time").val() + "&s=" + $("#storeid").val() + "&pnum=" + $("#carnum3").val() + "&timeleng=" + $("#timeleng").val() + "&price=" + $("#sprice").val(),
            dataType: 'html',
            async: false,
            beforeSend: function () {

            },
            success: function (data) {
                //alert(data);
                //var data = JSON.parse(json);
                $("#dateBook").html(data);
                $('#dateBook li div span.price').parent('div').on('click', function () {
                    $('#dateBook li div').removeClass('cur');
                    $(this).addClass('cur');
                    $('.weicheck').html($("#time").val() + " " + $(this).find('span').eq(0).html());
                })
                //$('.mei-box-time').html(data);
                //$('.mei-box-time span').find('.norhui').on('click', function () {
                //    $('.mei-box-time span em').removeClass('curbg');
                //    $(this).addClass('curbg');
                //})
            },
            error: function (error) {
                console.log(error);
            }
        });
        $("#IsPostBack").val(parseInt($("#IsPostBack").val()) + 1);
        if ($("#IsPostBack").val() > 1 || getUrlParam("time2") == "0:00") {
            $(".weicheck").html("未选择时间");

        } else {
            if (getUrlParam("time2")) {
                $('#dateBook li div span').each(function () {
                    if ($(this).html() == getUrlParam("time2")) {
                        $(this).parent().addClass("cur");
                        return;
                    }
                });

            }
        }

    }

    function selectjs() {
        if ($("#carnum3").val() > 1) {
            alert("多人不可指定技师");
        }
        else if ($(".weicheck").html() == "未选择时间") {
            alert("请选择时间");
        } else {

            window.location = "jishi.aspx?time=" + $(".weicheck").html() + "&storeid=" + $("#storeid").val() + "&pnum=" + $("#carnum3").val() + "&username=" + $("#userName").val() + "&phone=" + $("#iphone").val() + "&jssex=-1&sid=" + $("#serviceid").val() + "&emp=" + $("#empid").val() + "&dis=" + $("#dis").val() + "&url=newbook";
        }
    }

    function selectdis() {
        //var time = "";
        if ($(".weicheck").html() != "未选择时间") {
            window.location = "../discount/seldis.aspx?time=" + $(".weicheck").html() + "&storeid=" + $("#storeid").val() + "&pnum=" + $("#carnum3").val() + "&username=" + $("#userName").val() + "&phone=" + $("#iphone").val() + "&jssex=-1&sid=" + $("#serviceid").val() + "&emp=" + $("#empid").val() + "&dis=" + $("#dis").val() + "&url=newbook";
        } else {
            window.location = "../discount/seldis.aspx?time=" + $("#time").val() + " 0:00" + "&storeid=" + $("#storeid").val() + "&pnum=" + $("#carnum3").val() + "&username=" + $("#userName").val() + "&phone=" + $("#iphone").val() + "&jssex=-1&sid=" + $("#serviceid").val() + "&emp=" + $("#empid").val() + "&dis=" + $("#dis").val() + "&url=newbook";
        }
    }

    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
    SelectTime();
</script>
