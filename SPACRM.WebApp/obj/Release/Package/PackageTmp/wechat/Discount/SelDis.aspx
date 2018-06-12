<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelDis.aspx.cs" Inherits="SPACRM.WebApp.wechat.Discount.SelDis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/youhuiquan.css" rel="stylesheet" />
    <title>优惠券</title>
</head>
<body class="bgcolor">
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="xz(0)"><span><em class="icon-fanhui"></em></span></a>
                <h2>我的优惠券</h2>
                <span class="icon-tel"></span>
            </div>
        </div>

        <div class="order-tag-item" style="margin-top: 10px;">
            <a href="javascript:;" onclick="xz(0)">
                <ol style="width:100%;"><span class="curbtn" style="    border-bottom: none;">不使用优惠券</span></ol>
            </a>
        </div>
        <!--end header-->
        <div class="quan-content-box mei-index-Top">
            <div class="quan-index-items" style="    margin-top: 15px;">
                <ul>
                    <%foreach (var d in UserDisList)
                      {
                    %>
                    <li onclick="xz(<%=d.ID %>)">
                        <div class="quan-left-waper">
                            <div class="quan-index-moneny">
                                <strong><em>￥</em><%=d.Money %></strong>
                                <p>微信下单可用</p>
                            </div>
                        </div>
                        <div class="quan-index-info">
                            <h2><%=d.DiscountName %></h2>
                            <p><%=d.Detail %></p>
                            <p>有效期：<%=d.Validity.Value.ToString("yyyy.MM.dd") %></p>
                            
                        </div>
                    </li>
                    <%} %>
                </ul>
            </div>


        </div>


          <input type="hidden" id="time" value="<%=Request.QueryString["time"]==null?Request.QueryString["time"]:Request.QueryString["time"].Split(' ')[0] %>" />
        <input type="hidden" id="time2" value="<%=Request.QueryString["time"]==null?Request.QueryString["time"]:Request.QueryString["time"].Split(' ')[1] %>" />
        <input type="hidden" id="storeid" value="<%=Request.QueryString["storeid"] %>" />
        <input type="hidden" id="pnum" value="<%=Request.QueryString["pnum"] %>" />
         <input type="hidden" id="username" value="<%=Request.QueryString["username"] %>" />
         <input type="hidden" id="phone" value="<%=Request.QueryString["phone"] %>" />
        <input type="hidden" id="jssex" value="<%=Request.QueryString["jssex"] %>"/>
            <input type="hidden" id="sid" value="<%=Request.QueryString["sid"] %>"/>
         <input type="hidden" id="emp" value="<%=Request.QueryString["emp"]??"0" %>"/>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script>
    function xz(id) {
        if (getUrlParam("url")) {
            location = "../spa/newbook.aspx?time=" + $("#time").val() + "&storeid=" + $("#storeid").val() + "&pnum=" + $("#pnum").val() + "&emp=" + $("#emp").val() + "&username=" + $("#username").val() + "&phone=" + $("#phone").val() + "&jssex=" + $("#jssex").val() + "&serviceid=" + $("#sid").val() + "&dis=" + id + "&time2=" + $("#time2").val();
        } else {
            location = "../spa/orderok.aspx?time=" + $("#time").val() + " "  +$("#time2").val() + "&storeid=" + $("#storeid").val() + "&pnum=" + $("#pnum").val() + "&emp=" + $("#emp").val() + "&username=" + $("#username").val() + "&phone=" + $("#phone").val() + "&jssex=" + $("#jssex").val() + "&sid=" + $("#sid").val() + "&dis=" + id;
        }
    }
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
</script>
