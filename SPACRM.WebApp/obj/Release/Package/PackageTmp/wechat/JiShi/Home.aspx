<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SPACRM.WebApp.wechat.JiShi.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <input type="hidden" value="http%253a%252f%252flocalhost%253a15679%252fwechat%252fjishi%252fhome.aspx%253ffromusername%253doCjdLt5DPSQrbLQpC4USfXn46fsA%2526tousername%253doCjdLt5DPSQrbLQpC4USfXn46fsA" id="url">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, minimal-ui">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <title>业绩-服务业绩</title>
    <!-- CSS start -->
    <link rel="stylesheet" href="icofont/style.css">
    <link rel="stylesheet" href="css/style.min.css">  <script src="js/tz.js?v=1"></script>
</head>
<body>
    <%--<input type="hidden" value="http%253a%252f%252flocalhost%253a15679%252fwechat%252fspa%252fIndex.aspx%253ffromusername%253doCjdLt5DPSQrbLQpC4USfXn46fsA%2526tousername%253doCjdLt5DPSQrbLQpC4USfXn46fsA" id="url">--%>
    <%--  <form id="form1" runat="server">--%>
    <div class="container">
        <!-- header start -->
        <header class="header color-1">
            <div class="inner">
                <nav class="tabbar">
                    <a class="active" href="home.aspx">服务业绩</a>
                    <a href="home2.aspx">销售业绩</a>
                </nav>
            </div>
        </header>
        <!-- header over -->
        <!-- footer start -->
        <footer class="footer">
            <img class="shadow" src="img/shadow@2x.png" alt="">
            <nav class="inner tabbar">
                <a class="active" href="home.aspx">
                    <div class="icon"></div>
                    <p>首页</p>
                </a>
                <a href="OrderList.aspx">
                    <div class="icon"></div>
                    <p>订单</p>
                </a>
                <a href="wo.aspx">
                    <div class="icon"></div>
                    <p>我</p>
                </a>
            </nav>
        </footer>
        <!-- footer over -->
        <!-- main start -->
        <main class="main">
            <!-- slide start -->
            <section class="card-info-slide">
                <!-- card start -->
                <div class="swiper-container card-slide" id="card-slide">
                    <div class="swiper-wrapper">
                        <!-- group start -->
                        <%foreach (var y in yjlist)
                          {
                              %>
                        <div class="swiper-slide">
                            <div class="card-slide-group">
                                <h5 class="title"><%=y.ym %></h5>
                                <div class="card">
                                    <div class="main">
                                        <p class="title">本月服务业绩</p>
                                        <h1 class="number"><%=y.price %></h1>
                                    </div>
                                    <div class="bar">
                                        <p class="gray">今日业绩(元)</p>
                                        <p><%=todayyj.price %></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%
                          } %>
                        
                       
                        <!-- group over -->
                    </div>
                </div>
                <!-- card over -->
                <!-- info start -->
                <div class="swiper-container info-slide" id="info-slide">
                    <div class="swiper-wrapper">
                        <!-- group start -->
                          <%foreach (var y in yjlist)
                            {
                              %>
 <div class="swiper-slide">
                            <div class="info-slide-group">
                                <div class="column">
                                    <p class="title">面护业绩(元)</p>
                                    <h4 class="number">0</h4>
                                </div>
                                <div class="column">
                                    <p class="title">身护业绩(元)</p>
                                    <h4 class="number">0</h4>
                                </div>
                                <div class="column">
                                    <p class="title">辅项业绩(元)</p>
                                    <h4 class="number">0</h4>
                                </div>
                                <div class="column">
                                    <p class="title">打赏收益(元)</p>
                                    <h4 class="number">0</h4>
                                </div>
                            </div>
                        </div>
                        <%} %>
                       
                      
                    </div>
                </div>
                <!-- info over -->
            </section>
            <!-- slide over -->
            <!-- title start -->
            <div class="title-box link">
                <a href="DingDan.aspx">
                    <h5 class="title gray">服务记录(<%=OrderCount %>单)</h5>
                    <p class="more">查看全部<i class="icon-arrow-right"></i></p>
                </a>
            </div>
            <!-- title over -->
            <!-- list start -->
            <div class="list">
                <!-- item start -->
                <%foreach (var o in orderlist)
                  {
                      %>
<div class="item">
                    <div class="row">
                        <div class="column">
                            <div class="inner">
                                <p class="title"><%=o.CATEGORY_NAME %></p>
                                <p class="gray small"><%=o.CREATE_DATE.ToString("yyyy.MM.dd HH:mm") %></p>
                            </div>
                        </div>
                        <div class="column">
                            <p class="black big">+<%=o.PAY_AMT %></p>
                        </div>
                    </div>
                </div>
                <%
                  } %>
                
                
                <!-- item over -->
            </div>
            <!-- list over -->
        </main>
        <!-- main over -->
    </div>
    <!-- container start -->

    <input type="hidden" id="ztitle" value="技师端操作系统登录" />
    <input type="hidden" id="ftitle" value="技师端操作系统登录" />
    <%-- </form>--%>
</body>
</html>
<!-- JavaScript start -->
<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="js/swiper/swiper.min.js"></script>
<script type="text/javascript" src="js/tooltip/jquery.darktooltip.js"></script>
<script type="text/javascript" src="js/scripts.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
