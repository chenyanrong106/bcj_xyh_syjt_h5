<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PJ.aspx.cs" Inherits="SPACRM.WebApp.wechat.JiShi.PJ" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, minimal-ui">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <title>我-我的评价</title>
    <!-- CSS start -->
    <link rel="stylesheet" href="icofont/style.css">
    <link rel="stylesheet" href="css/style.min.css">
    <!-- CSS over -->  <script src="js/tz.js"></script>
</head>


<body>
    <!-- container start -->
    <div class="container gray">
        <!-- header start -->
        <header class="header border-bottom">
            <div class="inner">
                <a class="button left" href="wo.aspx"><i class="icon-arrow-left"></i></a>
                <h4 class="title">我的评价</h4>
            </div>
        </header>
        <!-- header over -->
        <!-- main start -->
        <main class="main">
            <!-- list start -->
            <section class="list">
                <!-- item start -->
                <%foreach (var p in pjlist)
                  {
                      %>
                          <div class="item flex-column">
                    <div class="row">
                        <div class="column">
                            <p class="gray small"><%=p.CreateTime.Value.ToString("yyyy.MM.dd HH:mm") %></p>
                        </div>
                        <div class="column">
                            <div class="star-mini">
                                <%for (int i = 0; i < p.ZT; i++)
                                  {
                                      %>
                                 <img src="img/star@2x.png" alt="">
                                <%
                                  } 
                          %>
                               
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <p><%=p.Remark %></p>
                    </div>
                </div>
                          <%
                  } %>
                
                <!-- item over -->
                <!-- item over -->
            </section>

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
