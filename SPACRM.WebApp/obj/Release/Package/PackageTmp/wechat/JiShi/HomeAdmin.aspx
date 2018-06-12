<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeAdmin.aspx.cs" Inherits="SPACRM.WebApp.wechat.JiShi.HomeAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <input type="hidden" value="http%253a%252f%252flocalhost%253a15679%252fwechat%252fjishi%252fhome.aspx%253ffromusername%253doCjdLt5DPSQrbLQpC4USfXn46fsA%2526tousername%253doCjdLt5DPSQrbLQpC4USfXn46fsA" id="url">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, minimal-ui">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <title>业绩-服务业绩</title>
    <!-- CSS start -->

    <link rel="stylesheet" href="icofont/style.css">
    <link rel="stylesheet" href="css/style.min.css?v=1">
    <link type="text/css" rel="stylesheet" href="css/index.css" />
    <script src="js/tz.js?v=1"></script>
    <style>
        .footerbar {
            width: 100%;
            height: 40px;
            background: rgba(243,243,243,1);
            border-top: 1px solid #f3f3f3;
            position: fixed;
            bottom: 0;
            left: 0;
            right: 0;
            display: block;
            padding: 4px 0;
            z-index: 9999;
            max-width: 75rem;
            margin: 0 auto;
        }

            .footerbar ul li {
                float: left;
                width: 33.33%;
                display: block;
                text-align: center;
            }

        .footerFour ul li {
            float: left;
            width: 25%;
            display: block;
            text-align: center;
        }

        .footerbar ul li span {
            display: block;
            font-weight: normal;
            display: block;
            font-size: 1.5em;
            margin-top: 2px;
            color: #535353;
        }

        .footerbar ul li.cur span, .footerbar ul li.cur bdo {
            color: #ff5c67;
        }

        .footerbar ul li bdo {
            font-size: 12px;
            color: #535353;
            text-align: center;
            display: block;
        }
    </style>
</head>
<body>
    <%--<input type="hidden" value="http%253a%252f%252flocalhost%253a15679%252fwechat%252fspa%252fIndex.aspx%253ffromusername%253doCjdLt5DPSQrbLQpC4USfXn46fsA%2526tousername%253doCjdLt5DPSQrbLQpC4USfXn46fsA" id="url">--%>
    <%--  <form id="form1" runat="server">--%>
    <div class="container">
        <!-- header start -->
        <header class="header color-1">
            <div class="inner">
                <nav class="tabbar">
                    <a class="active" href="homeadmin.aspx">服务业绩</a>
                    <a href="homeadmin.aspx">销售业绩</a>
                </nav>
            </div>
        </header>
        <!-- header over -->
        <!-- footer start -->
        <%--<footer class="footer">
            <img class="shadow" src="img/shadow@2x.png" alt="">
            <nav class="inner tabbar">
                <a class="active" href="homeadmin.aspx">
                    <div class="icon"></div>
                    <p>首页</p>
                </a>
                <a href="adminorder.aspx">
                    <div class="icon"></div>
                    <p>订单</p>
                </a>
                <a href="javascript:;">
                    <div class="icon"></div>
                    <p>我</p>
                </a>
            </nav>
        </footer>--%>
        <!-- footer over -->
        <!-- main start -->
        <main class="main">
            <!-- slide start -->
            <section class="card-info-slide">
                <!-- card start -->
                <div class="swiper-container card-slide" id="card-slide">
                    <div class="swiper-wrapper">
                        <!-- group start -->
                        <%
                            int i = 2;
                            DateTime d = DateTime.Now;
                            DateTime dt = DateTime.Now;
                            foreach (var y in yjlist)
                            {
                                dt = d.AddDays(i * -1);
                                List<SPACRM.Entity.Entities.JiShiYJ_EX> l = todayyj.Where(day => day.day == dt.ToString("yyyyMMdd")).ToList();
                              %>
                        <div class="swiper-slide">
                            <div class="card-slide-group">
                                <h5 class="title"><%=y.ym %></h5>
                                <div class="card">
                                    <div class="main">
                                        <p class="title"><%=y.ym %>月服务业绩</p>
                                        <h1 class="number"><%=y.price %></h1>
                                    </div>
                                    <%if (l.Count == 1)
                                      { %><div class="bar">
                                        <p class="gray"><%=l[0].day %>业绩(元)</p>
                                        <p><%=l[0].price %></p>
                                    </div><%} %>
                                </div>
                            </div>
                        </div>
                        <%
                                      i--;
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
                                List<SPACRM.Entity.PROD_SERVICE> list = servicelist.Where(s => s.TITLE == y.ym).ToList();
                              %>
 <div class="swiper-slide">
                            <div class="info-slide-group">
                                <%foreach (var l in list)
                                  {%>
                                      <div class="column">
                                    <p class="title"><%=l.NAME %></p>
                                    <h4 class="number"><%=l.PayCount %></h4>
                                </div>
                                 <% } %>
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
                <a href="DingDanAdmin.aspx">
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
                                <p class="title"><%=o.CATEGORY_NAME %>&nbsp;&nbsp;<%=o.empname %></p>
                                <p class="gray small"><%=o.CREATE_DATE.ToString("yyyy.MM.dd HH:mm") %>&nbsp;&nbsp;<%=o.STORE_NAME %></p>
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

    <div class="footerbar footerFour">
        <ul>
            <li class="cur"><a href="homeadmin.aspx"><span class="icon-logo-w"></span><bdo>首页</bdo></a></li>
            <li><a href="adminorder.aspx"><span class="icon-order-f"></span><bdo>订单</bdo></a></li>
            <li><a href="yeji.aspx"><span class="icon-package"></span><bdo>业绩</bdo></a></li>
            <li><a href="javascript:;"><span class="icon-personal"></span><bdo>我</bdo></a></li>
        </ul>
    </div>
</body>
</html>
<!-- JavaScript start -->
<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="js/swiper/swiper.min.js"></script>
<script type="text/javascript" src="js/tooltip/jquery.darktooltip.js"></script>
<script type="text/javascript" src="js/scripts.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
