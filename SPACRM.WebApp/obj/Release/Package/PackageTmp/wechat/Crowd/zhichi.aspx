<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zhichi.aspx.cs" Inherits="SPACRM.WebApp.wechat.Crowd.zhichi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/zhongchou.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/Message2.js"></script>
    <script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
    <title>小时光</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="zhichiList">
            <ul>
                <%foreach (var p in list)
                  {
                %>
                <li>
                    <div class="zhichiTitle">
                        <p><span><i class="icon-user"></i>认筹 <%=p.YC %> 人/剩余名额<%=p.SY %>人</span><strong>￥ <%=p.Price %> <%if (p.ISYY == 1)
                                                                                                                          { %><em style="font-size: x-small; color: green;">预约金</em><%} %></strong></p>
                    </div>
                    <div class="zhichiPro">
                        <h2 style="font-size:18px;"><%=p.Name%></h2>
                        <p><%=p.Detail %></p>
                    </div>
                    <%if (p.SY > 0)
                      { %>
                    <div class="zhichiPrice">
                        <p><span class="payBtn" onclick="pay(<%=p.ID %>)">我要支持</span><i class="icon-service-time"></i>项目结束 5 天后发送</p>
                    </div>
                    <%} %>
                </li>
                <%
        } %>
            </ul>
        </div>
    </form>
</body>
</html>
<script>
    function pay(id) {

        $.ajax({
            type: "Get",
            url: "zhichi.aspx?para=tj",
            data: { cid: id },
            dataType: 'json',
            async: false,
            beforeSend: function () {

            },
            success: function (data) {
                if (data.orderid < 0) {
                    alert("异常");
                }

                else {
                    location = "../spa/crowdpay.aspx?oid=" + data.orderid;
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

</script>
