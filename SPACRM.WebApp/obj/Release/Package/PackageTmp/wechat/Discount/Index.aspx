<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SPACRM.WebApp.wechat.Discount.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <a href="../spa/wo.aspx"><span><em class="icon-fanhui"></em></span></a>
                <h2>我的优惠券</h2>
                <span class="icon-tel"></span>
            </div>
        </div>

        <div class="order-tag-item">
            <%if(mydis!=null){ %>
            <a href="index.aspx">
                <ol><span class="curbtn">未使用(<%=mydis.wsy %>)</span></ol>
            </a>
            <a href="yishiyong.aspx">
                <ol><span>已使用(<%=mydis.ysy %>)</span></ol>
            </a>
            <a href="yiguoqi.aspx">
                <ol><span>已过期(<%=mydis.ygq %>)</span></ol>
            </a>
            <%} %>
        </div>
        <!--end header-->
        <div class="quan-content-box mei-index-Top">
            <div class="quan-index-items">
                <ul>
                    <%foreach (var d in UserDisList)
                      {
                    %>
                    <li>
                        <div class="quan-left-waper">
                            <div class="quan-index-moneny">
                                <strong><em>￥</em><%=d.Money %></strong>
                                <p>微信下单可用</p>
                            </div>
                        </div>
                        <div class="quan-index-info">
                            <h2><%=d.DiscountName %></h2>
                            <p>有效期：<%=d.Validity.Value.ToString("yyyy.MM.dd") %></p>
                            <a href="../spa/index.aspx">
                                <div class="lingqubtn">立即使用</div>
                            </a>
                        </div>
                    </li>
                    <%} %>
                </ul>
            </div>


        </div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script>


</script>
