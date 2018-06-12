<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wo.aspx.cs" Inherits="SPACRM.WebApp.wechat.JiShi.wo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, minimal-ui">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <title>我</title>
    <!-- CSS start -->
    <link rel="stylesheet" href="icofont/style.css">
    <link rel="stylesheet" href="css/style.min.css">  <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <!-- container start -->
    <div class="container gray">
        <!-- footer start -->
        <footer class="footer">
            <img class="shadow" src="img/shadow@2x.png" alt="">
            <nav class="inner tabbar">
                <a href="home.aspx">
                    <div class="icon"></div>
                    <p>首页</p>
                </a>
                <a href="orderlist.aspx">
                    <div class="icon"></div>
                    <p>订单</p>
                </a>
                <a class="active" href="wo.aspx">
                    <div class="icon"></div>
                    <p>我</p>
                </a>
            </nav>
        </footer>
        <!-- footer over -->
        <!-- main start -->
        <main class="main">
            <!-- panel start -->
            <section class="panel">
                <div class="avatar">
                    <img src="img/avatar@2x.png" alt="">
                </div>
                <%if(emp!=null){ %>
                <h4 class="name"><%=emp.NAME %></h4>
                <div class="info">
                    <div class="column">
                        <p class="number"><%=OrderCount %></p>
                        <p class="title">累计单量</p>
                    </div>
                    <div class="column">
                        <p class="number"><%=pjlist.Count==0?0:pjlist.Average(p=>p.ZT) %>分</p>
                        <p class="title">综合评分</p>
                    </div>
                </div>
                <%} %>
            </section>
            <!-- panel over -->
            <div class="spacing"></div>
            <!-- list start -->
            <section class="list">
                <!-- item start -->
                <a class="item" href="pj.aspx">
                    <div class="row icon-arrow-right">
                        <div class="column">
                            <div class="icon">
                                <img src="img/icon01@2x.png" alt="">
                            </div>
                            <p class="black big">我的评价</p>
                        </div>
                    </div>
                </a>
                <!-- item over -->
                <!-- item start -->
               <%-- <a class="item" href="3-user-time.html">
                    <div class="row icon-arrow-right">
                        <div class="column">
                            <div class="icon">
                                <img src="img/icon02@2x.png" alt="">
                            </div>
                            <p class="black big">时间管理</p>
                        </div>
                    </div>
                </a>--%>
                 <a class="item" href="2-order-wait.html">
                    <div class="row icon-arrow-right">
                        <div class="column">
                            <div class="icon">
                                <img src="img/icon02@2x.png" alt="">
                            </div>
                            <p class="black big">订单管理</p>
                        </div>
                    </div>
                </a>
                <!-- item over -->
                <!-- item start -->
               <%-- <a class="item" href="3-user-teaching.html">
                    <div class="row icon-arrow-right">
                        <div class="column">
                            <div class="icon">
                                <img src="img/icon03@2x.png" alt="">
                            </div>
                            <p class="black big">护理教程</p>
                        </div>
                    </div>
                </a>--%>
                <!-- item over -->
            </section>
            <!-- list over -->
            <div class="spacing"></div>
            <!-- list start -->
            <section class="list">
                <!-- item start -->
                <div class="item">
                    <div class="row">
                        <div class="column">
                            <div class="icon">
                                <img src="img/icon04@2x.png" alt="">
                            </div>
                            <p class="black big">累计服务时长</p>
                        </div>
                        <div class="column">
                            <p class="color-1"><%=ServiceTime==0?0:Math.Round(ServiceTime/60.0,1) %>小时</p>
                        </div>
                    </div>
                </div>
                <!-- item over -->
            </section>
            <!-- list over -->
            <div class="spacing"></div>
            <!-- list start -->
            <section class="list">
                <!-- item start -->
                <a class="item" href="tel:17621160808">
                    <div class="row">
                        <div class="column">
                            <div class="icon">
                                <img src="img/icon05@2x.png" alt="">
                            </div>
                            <p class="black big">联系客服</p>
                        </div>
                        <div class="column">
                            <p class="color-1">17621160808</p>
                        </div>
                    </div>
                </a>
                <!-- item over -->
            </section>
            <!-- list over -->
            <div class="spacing"></div>
            <!-- list start -->
           
            <!-- list over -->
            <div class="spacing"></div>
        </main>
        <!-- main over -->
    </div>
    </form>
</body>
</html>
 <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/swiper/swiper.min.js"></script>
    <script type="text/javascript" src="js/tooltip/jquery.darktooltip.js"></script>
    <script type="text/javascript" src="js/scripts.js"></script>