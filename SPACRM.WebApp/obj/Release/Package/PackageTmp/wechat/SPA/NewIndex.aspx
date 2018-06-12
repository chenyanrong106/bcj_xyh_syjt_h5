<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewIndex.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.NewIndex" %>

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
    <link type="text/css" rel="stylesheet" href="css/newindex.css?v=2" />
    <title>小时光Massage</title>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="alert('目前仅开放上海')"><span><em class="icon-down"></em><em>上海</em></span></a>
                <h2>门店</h2>
                <a href="tel:17621160808"><span class="icon-tel"></span></a>
            </div>
        </div>
        <!--end header-->
        <!--begin content-->
        <div class="meiWapper mei-index-bottom">

            <div class="indexProList">
                <ul>
                    <%foreach (var s in list)
                      {
                          if (s.Star == null)
                              s.Star = 5;
                    %>
                    <li>
                        <a href="NewDetail.aspx?id=<%=s.ID %>">
                            <div class="proListImg">
                                <div class="morenImg">
                                    <img src="image/md.jpg">
                                </div>
                            </div>
                            <div class="proListTxt">
                                <strong><%=s.NAME %></strong>
                                <div class="proPrice">
                                    <span>
                                        <%for (int i = 1; i <= 5; i++)
                                          {
                                              if (s.Star >= i)
                                              {
                                        %><i class="star"></i><%
                                          }
                                          else
                                          {
                                        %><i class="star1"></i><%   
                                          }
                                      } %>
                                    </span>￥150/人
                                </div>
                                <p><%=s.QY %></p>
                                <div class="proTag">
                                    <% foreach (var m in s.BQ.Split(';'))
                                       {
                                    %>
                                    <span><%=m %></span>
                                    <%
                                       } %>
                                </div>
                            </div>
                            <div class="proJuli">
                                <p><span class="ding">订</span></p>
                                <p><%=Math.Round(s.jl/1000,1) %>km</p>
                            </div>
                        </a>
                    </li>
                    <%
                      } %>
                </ul>
            </div>


        </div>
        <!--end content-->

        <div class="footerbar" style="border-top: 1px solid #ddd;">
            <ul>
                <li class="cur"><a href="newindex.aspx"><span class="icon-logo"></span><bdo>门店</bdo></a></li>
                <li><a href="dingdan.aspx"><span class="icon-agreement"></span><bdo>订单</bdo></a></li>
                <li><a href="wo.aspx"><span class="icon-personal"></span><bdo>我的</bdo></a></li>
            </ul>
        </div>
        <input type="hidden" id="ztitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
        <input type="hidden" id="ftitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script>
    wx.ready(function () {
        //alert(1);
        wx.getLocation({
            type: 'gcj02', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
            success: function (res) {
                var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                var speed = res.speed; // 速度，以米/每秒计
                var accuracy = res.accuracy; // 位置精度
                $.post("index.aspx?para=tj", {
                    lat: latitude,
                    lng: longitude
                }, function (ret) {

                });
            }
        });
    });</script>
