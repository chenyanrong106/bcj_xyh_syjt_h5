<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiShi.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.JiShi" %>

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
    <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="history.go(-1)"><span class="icon-left"></span></a>
                <h2>选择技师</h2>
                <span></span>
            </div>


        </div>
        <!--end header-->

        <div class="order-tag-item" style="display: none;">
            <div class="order-tag-tab">
                <ol><bdo class="oneli curtab">综合排序</bdo></ol>
                <ol><bdo>月订单最多</bdo></ol>
                <ol><bdo>月好评最多</bdo></ol>
            </div>
        </div>

        <!--begin content-->
        <div class="meiWapper ">
            <div class="order-content">


                <div class="aipet-order-items">
                    <ul>
                        <%foreach (var emp in oelist)
                          {%>
                        <%if (emp.STATUS == 1)
                          { %>
                        <li onclick="xz(<%=emp.ID %>)">
                            <div class="aipet-order-ltems-con">
                                <%--  <div class="jishi-checked">
                                    <i class="icon-radio" eid="<%=emp.ID %>"></i>
                                </div>--%>
                                <div class="imgbox">
                                    <img src="<%=string.IsNullOrEmpty(emp.Img)?"image/jishi.jpg":WebUrl+emp.Img %>">
                                </div>
                                <p><strong><%=emp.NAME %></strong></p>
                                <span class="jishi_info"><%=string.IsNullOrEmpty(emp.JianJie)?"暂无描述":emp.JianJie %></span>
                                <p class="jishi_introduce">
                                    <span><i class="icon-order_other"></i>月订单数<%=emp.MDD %></span><span class="service-num"><i class="icon-favor2"></i>月评论数<%=emp.MPL %></span>
                                    <%--                            <span class="service-num"><%if (emp.STATUS == 1)
                                                                                                                                                                                     { %>可预约&nbsp;&nbsp;<%}
                                                                                                                                                                                     else if (emp.STATUS == 2)
                                                                                                                                                                                     { %>约满&nbsp;&nbsp;<%}
                                                                                                                                                                                     else
                                                                                                                                                                                     { %>休息&nbsp;&nbsp;<%} %></span>--%>
                                </p>
                            </div>
                        </li>
                        <%}%>
                        <%} %>
                    </ul>
                </div>



            </div>

        </div>
        <input type="hidden" id="time" value="<%=Request.QueryString["time"]==null?Request.QueryString["time"]:Request.QueryString["time"].Split(' ')[0] %>" />
        <input type="hidden" id="time2" value="<%=Request.QueryString["time"]==null?Request.QueryString["time"]:Request.QueryString["time"].Split(' ')[1] %>" />
        <input type="hidden" id="storeid" value="<%=Request.QueryString["storeid"] %>" />
        <input type="hidden" id="pnum" value="<%=Request.QueryString["pnum"] %>" />
        <input type="hidden" id="username" value="<%=Request.QueryString["username"] %>" />
        <input type="hidden" id="phone" value="<%=Request.QueryString["phone"] %>" />
        <input type="hidden" id="jssex" value="<%=Request.QueryString["jssex"] %>" />
        <input type="hidden" id="sid" value="<%=Request.QueryString["sid"] %>" />
        <input type="hidden" id="dis" value="<%=Request.QueryString["dis"]??"0" %>" />
        <!--end content-->
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script>

    //备注 目前只能选一位技师，如果要允许多选 ，把数字1改成别的数字即可
    $('.aipet-order-items ul li').find('.icon-radio').on('click', function () {
        var leng = $('.aipet-order-items ul li').find('.icon-radio-f').length;
        if (leng >= 1 && $(this).hasClass('icon-radio-f') != true) {
            tips("亲，您只能选择一位技师");
        }
        if ($(this).hasClass('icon-radio') && leng < 1) {
            $(this).removeClass('icon-radio').addClass('icon-radio-f');
            location = "orderok.aspx?time=" + $("#time").val() + "&storeid=" + $("#storeid").val() + "&pnum=" + $("#pnum").val() + "&emp=" + $(this).attr("eid");
        } else {
            $(this).removeClass('icon-radio-f').addClass('icon-radio');
        }



    })

    function xz(id) {
        if (getUrlParam("url")) {
            location = "newbook.aspx?time=" + $("#time").val() + "&storeid=" + $("#storeid").val() + "&pnum=" + $("#pnum").val() + "&emp=" + id + "&username=" + $("#username").val() + "&phone=" + $("#phone").val() + "&jssex=" + $("#jssex").val() + "&serviceid=" + $("#sid").val() + "&dis=" + $("#dis").val() + "&time2=" + $("#time2").val();
        } else {
            location = "orderok.aspx?time=" + $("#time").val() + " "+$("#time2").val() + "&storeid=" + $("#storeid").val() + "&pnum=" + $("#pnum").val() + "&emp=" + id + "&username=" + $("#username").val() + "&phone=" + $("#phone").val() + "&jssex=" + $("#jssex").val() + "&sid=" + $("#sid").val() + "&dis=" + $("#dis").val();
        }
        //

    }

    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
</script>
