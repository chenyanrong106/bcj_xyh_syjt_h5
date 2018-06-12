<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingDanAdmin.aspx.cs" Inherits="SPACRM.WebApp.wechat.JiShi.DingDanAdmin" %>


<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, minimal-ui">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <title>业绩-业绩记录-全部业绩</title>
    <!-- CSS start -->
    <link rel="stylesheet" href="icofont/style.css">
    <link rel="stylesheet" href="css/style.min.css">
    <!-- CSS over -->  <script src="js/tz.js"></script>
</head>


<body>
    <!-- container start -->
    <div class="container">
        <!-- header start -->
        <header class="header">
            <div class="inner">
                <a class="button left" href="homeadmin.aspx"><i class="icon-arrow-left"></i></a>
                <div class="title-select">
                    <select onchange="selyj(this.value)">
                        <%
                            DateTime dt = DateTime.Now;
                            for (int i = 1; i <= 5; i++)
                            {
                        %>
                        <option value="<%=dt.ToString("yyyyMM") %>" <%=Request.QueryString["ym"]==dt.ToString("yyyyMM")?"selected":"" %>><%=dt.ToString("yyyy年MM月") %></option>
                        <%
                                dt = dt.AddMonths(-1);
                          } %>
                    </select>
                    <i class="icon-arrow-down"></i>
                </div>
            </div>
        </header>
        <!-- header over -->
        <!-- main start -->
        <main class="main">
            <!-- tab start -->
            <nav class="tabbar">
                <div class="inner">
                    <a class="active" href="dingdanadmin.aspx">全部业绩</a>
                    <a href="javascript:;" onclick="alert('无');">服务业绩</a>
                    <a href="javascript:;" onclick="alert('无');">销售业绩</a>
                    <a href="javascript:;" onclick="alert('无');">打赏收益</a>
                </div>
            </nav>
            <!-- tab over -->
            <!-- title start -->
            <div class="title-box high">
                <h5 class="title gray">总业绩(<%=OrderCount %>单)</h5>
                <p class="gray">+<%=orderlist.Sum(o=>o.PAY_AMT) %></p>
            </div>
            <!-- title over -->
            <!-- list start -->
            <div class="list">
                <!-- item start -->
               <%-- <%foreach (var o in orderlist)
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
                            <p class="color-1 big">+<%=o.PAY_AMT %></p>
                        </div>
                    </div>
                </div>
                <%
                  } %>--%>
                
                <!-- item over -->
            </div>
            <a href="javascript:;" onclick="showPetdata();" class="more-load">点击加载更多</a>
            <!-- list over -->
        </main>
        <!-- main over -->
    </div>
    <input type="hidden" id="ym" value="<%=Request.QueryString["ym"] %>" />
    <!-- container start -->

    <!-- JavaScript start -->
    <%--<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>--%>
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.masonry.min.js"></script>
<%--    <script type="text/javascript" src="js/swiper/swiper.min.js"></script>
    <script type="text/javascript" src="js/tooltip/jquery.darktooltip.js"></script>
    <script type="text/javascript" src="js/scripts.js"></script>--%>
    <!-- JavaScript over -->
    <script>
        function selyj(ym) {
            window.location = "dingdanadmin.aspx?ym=" + ym;
        }
        console.log($(window));
        //数据加载
        var pageNum = 1; rowsNum = 10;
        var isNoMoreData = false, isLoading = false;
        $(window).scroll(function () {
            console.log(10);
            ////echo.render();
            //alert(1);
            //var curTop = $(window).scrollTop();
            //var juli = $(document).height() - $(window).height();
            //alert(curTop);
            //alert(juli);
            //if (curTop >= juli) {
            //    if (!isNoMoreData && !isLoading) {
            //        showPetdata(pageNum + 1);
            //    }
            //}
        });
        showPetdata();
        function showPetdata() {
            $.ajax({
                type: "Get",
                url: "dingdanadmin.aspx?para=tj&page=" + pageNum+"&ym="+$("#ym").val() ,
                dataType: 'html',
                async: false,
                beforeSend: function () {
                    isLoading = true;
                    //$("#loading").show();
                },
                success: function (data) {
                    //alert(data);
                    //var data = JSON.parse(json);
                    if (data !== null) {

                        if (data == "") {
                            //$('#nomoreData').show();
                            //$("#loading").hide();
                            isNoMoreData = true;
                        } else {


                            $('.list').append(data);
                            //$("#loading").show();
                            isLoading = false;

                            pageNum++;
                            console.log(pageNum);
                        }
                    } else {
                        isNoMoreData = true;
                        //$('#nomoreData').show();
                        //$("#loading").hide();
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });

        }
    </script>
</body>

</html>
