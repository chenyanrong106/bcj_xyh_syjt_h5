<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home2.aspx.cs" Inherits="SPACRM.WebApp.wechat.JiShi.Home2" %>
<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, minimal-ui">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <title>业绩-销售业绩</title>
    <!-- CSS start -->
    <link rel="stylesheet" href="icofont/style.css">
    <link rel="stylesheet" href="css/style.min.css">
    <!-- CSS over -->  <script src="js/tz.js"></script>
</head>


<body>
    <!-- container start -->
    <div class="container">
        <!-- header start -->
        <header class="header color-1">
            <div class="inner">
                <nav class="tabbar">
                    <a href="home.aspx">服务业绩</a>
                    <a class="active" href="home2.aspx">销售业绩</a>
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
                        <div class="swiper-slide">
                            <div class="card-slide-group">
                                <h5 class="title"><%=DateTime.Now.ToString("yyyyMM") %></h5>
                                <div class="card">
                                    <div class="main">
                                        <p class="title">本月销售业绩</p>
                                        <h1 class="number">0</h1>
                                    </div>
                                    <div class="bar">
                                        <p class="gray">今日业绩(元)</p>
                                        <p>0</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- group over -->

                        <!-- group over -->
                    </div>
                </div>
                <!-- card over -->
                <!-- info start -->
                <div class="swiper-container info-slide" id="info-slide">
                    <div class="swiper-wrapper">
                        <!-- group start -->
                        <div class="swiper-slide">
                            <div class="info-slide-group">
                                <div class="column">
                                    <p class="title">面护套餐销售(元)</p>
                                    <h4 class="number">0</h4>
                                </div>
                                <div class="column">
                                    <p class="title">身体套餐销售(元)</p>
                                    <h4 class="number">0</h4>
                                </div>
                            </div>
                        </div>
                        <!-- group over -->
                        <!-- group start -->

                        <!-- group over -->
                    </div>
                </div>
                <!-- info over -->
            </section>
            <!-- slide over -->
            <!-- title start -->
            <div class="title-box link">
                <a href="DingDan.aspx">
                    <h5 class="title gray">服务记录(0单)</h5>
                    <p class="more">查看全部<i class="icon-arrow-right"></i></p>
                </a>
            </div>
            <!-- title over -->
            <!-- list start -->
            <div class="list">
                <!-- item start -->

                <!-- item over -->
            </div>
            <!-- list over -->
        </main>
        <!-- main over -->
    </div>
    <!-- container start -->

    <!-- JavaScript start -->
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/swiper/swiper.min.js"></script>
    <script type="text/javascript" src="js/tooltip/jquery.darktooltip.js"></script>
    <script type="text/javascript" src="js/scripts.js"></script>
    <!-- JavaScript over -->

</body>

</html>
