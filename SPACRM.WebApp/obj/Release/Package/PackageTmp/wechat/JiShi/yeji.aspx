<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yeji.aspx.cs" Inherits="SPACRM.WebApp.wechat.JiShi.yeji" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="月亮船">
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
    <link type="text/css" rel="stylesheet" href="css/indexnew.css?v=1" />
    <title>业绩</title>
    <style>
        html, body {
            background: #fff;
        }

        .swiper-container {
            width: 100%;
            height: 100%;
        }

        .swiper-slide {
            position: relative;
            width: 100%;
            /* Center slide text vertically */
            display: -webkit-box;
            display: -ms-flexbox;
            display: -webkit-flex;
            display: flex;
            flex-flow: column;
            -webkit-box-pack: center;
            -ms-flex-pack: center;
            -webkit-justify-content: center;
            justify-content: center;
            -webkit-box-align: center;
            -ms-flex-align: center;
            -webkit-align-items: center;
            align-items: center;
            background: #000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%if (book != null)
          { %>
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="adminorder.aspx"><span class="icon-left"></span></a>
                <h2>
                    <select class="dropdown-select-nav" style="width: 100%;" id="storeid">

                        <%foreach (var s in list)
                          {
                        %>
                        <option value="<%=s.ID %>" <%=Request.QueryString["sid"]==s.ID.ToString()?"selected":"" %>><%=s.NAME %></option>
                        <%
                          }%>

                        <%-- <option>2017年5月</option>
                        <option>2017年4月</option>
                        <option>2017年3月</option>
                        <option>2017年2月</option>--%>
                    </select></h2>
                <span></span>
            </div>
        </div>
        <!--end header-->




        <!--begin content-->
        <div class="meiWapper yeji-index-Top">
            <div class="swiper-container">
                <div class="swiper-wrapper">
                    <div class="swiper-slide">

                        <div class="yejiHeader">
                            <ul>
                                <li><span>月份</span>
                                    <strong><%=book.rq %></strong>
                                </li>
                                <li><span>总实收</span>
                                    <strong><%=book.sf %></strong>
                                </li>
                                <li><span>总微信买单</span>
                                    <strong><%=book.wx %></strong>
                                </li>

                            </ul>
                            <ul class="yejitableTr">
                                <li><bdo>日期</bdo></li>
                                <li><bdo>实收(元)</bdo></li>
                                <li><bdo>微信买单(元)</bdo></li>
                            </ul>
                        </div>
                        <div class="yeji-index-table">
                            <ul>
                                <%foreach (var b in booklist)
                                  {
                                %>
                                <li><span><%=b.rq %></span><span><%=b.sf %></span><span><%=b.wx %></span></li>
                                <%
            } %>
                            </ul>
                        </div>

                    </div>

                    <div class="swiper-slide">

                        <div class="yejiHeader">
                            <ul>
                                <li><span>总团购</span>
                                    <strong><%=book.tg %></strong>
                                </li>
                                <li><span>总储值</span>
                                    <strong><%=book.cz %></strong>
                                </li>
                                <li><span>总项目数</span>
                                    <strong><%=book.xm %></strong>
                                </li>

                            </ul>
                            <ul class="yejitableTr">
                                <li><bdo>团购</bdo></li>
                                <li><bdo>储值</bdo></li>
                                <li><bdo>项目数</bdo></li>
                            </ul>
                        </div>
                        <div class="yeji-index-table">
                            <ul>
                                <%foreach (var b in booklist)
                                  {
                                %>
                                <li><span><%=b.tg %></span><span><%=b.cz %></span><span><%=b.xm %></span></li>
                                <%
            } %>
                            </ul>
                        </div>
                    </div>

                    <div class="swiper-slide">

                        <div class="yejiHeader">
                            <ul>
                                <li><span>月份</span>
                                    <strong><%=book.rq %></strong>
                                </li>
                                <li><span>优惠金额</span>
                                    <strong><%=book.yhdk %></strong>
                                </li>
                                <li><span>抵扣金额</span>
                                    <strong><%=book.czdk %></strong>
                                </li>

                            </ul>
                            <ul class="yejitableTr">
                                <li><bdo>日期</bdo></li>
                                <li><bdo>优惠抵扣</bdo></li>
                                <li><bdo>储值抵扣</bdo></li>
                            </ul>
                        </div>
                        <div class="yeji-index-table">
                            <ul>
                                <%foreach (var b in booklist)
                                  {
                                %>
                                <li><span><%=b.rq %></span><span><%=b.yhdk %></span><span><%=b.czdk %></span></li>
                                <%
            } %>
                            </ul>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <!--end content-->
        <%} %>
        <div class="footerbar footerTwo">
            <ul>
                <li><a href="yeji.aspx?ym=<%=sym %>"><bdo>上月业绩</bdo></a></li>
                <li class="cur"><a href="yeji.aspx?ym=<%=DateTime.Now.ToString("yyyyMM") %>"><bdo>本月业绩</bdo></a></li>
            </ul>
        </div>
        <input type="hidden" id="wym" value="<%=Request.QueryString["ym"]!=null?"&ym="+Request.QueryString["ym"]:"" %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script type="text/javascript" src="js/swiper.js"></script>
<script>
    var swiper = new Swiper('.swiper-container', {
        pagination: '.swiper-pagination',
        paginationClickable: true
    });

    $("#storeid").change(function () {
        location = "yeji.aspx?sid=" + $("#storeid").val()+$("#wym").val();
    });
</script>
