<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingDan.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.DingDan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="小时光Massage">
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
    <link type="text/css" rel="stylesheet" href="css/index.css?v=1" />
    <title>小时光Massage</title>
    <script src="js/tz.js"></script>
    <style>
        .share-box {
            position: fixed;
            left: 10%;
            top: 24%;
            background: url(image/share.png) no-repeat center 0;
            width: 80%;
            height: 52%;
            background-size: 100%;
            display: block;
        }

        .share-box-del {
            width: 30px;
            height: 30px;
            background: url(image/del.png) no-repeat center center;
            background-size: 30px;
            position: absolute;
            right: 0;
            top: 0;
            z-index: 9999;
        }

        .share-box h2 {
            font-size: 18px;
            color: #fff;
            padding-top: 55%;
            line-height: 30px;
            text-align: center;
            font-weight: normal;
        }

        .share-box p {
            font-size: 10px;
            color: #ffff00;
            text-align: center;
            line-height: 20px;
            margin-bottom: 10px;
        }

        .share-box .share-btn {
            width: 58%;
            height: 40px;
            background: #ffe761;
            border-radius: 5px;
            color: #ff0000;
            font-size: 14px;
            line-height: 40px;
            text-align: center;
            margin: 0 auto;
        }
    </style>
    <style>
        .share {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: rgba(0,0,0,.8);
            display: none;
            overflow: hidden;
        }

        .sharebg {
            background: url(image/fx.png) no-repeat center 0;
            background-size: 60%;
            width: 100%;
            height: 100%;
            display: block;
            position: absolute;
            left: 0;
            top: 10%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <span></span>
                <h2>我的订单</h2>
                <a href="tel:17621160808"><span class="icon-tel"></span></a>
            </div>

            <ul class="lnks">
        </div>
        <!--end header-->

        <div class="order-tag-item order-tag-order">
            <ol><span class="curbtn" otype="0">全部</span></ol>
            <ol><span otype="1">待消费</span></ol>
            <ol><span otype="2">待评价</span></ol>
            <ol><span otype="3">已过期</span></ol>
        </div>

        <!--begin content-->
        <div class="meiWapper mei-index-bottom mei-index-Top">
            <div class="order-content">
                <div class="aipet-order-items">
                    <ul id="uid0">
                        <%foreach (var o in olist)
                          {
                        %>

                        <li>
                            <div class="aipet-order-ltems-title aipet-order-ltems-title2">
                                <span><%=o.PJID!=0?"已完成":o.RealEndDate!=null?"待评价":o.PAY_STATUS == 1 ? "待消费" : o.OverdueDate < DateTime.Now ? "已过期" : "待支付"%></span>
                                <strong><em>
                                    <img src="<%=string.IsNullOrEmpty(o.Img) ? "image/jishi.jpg" : WebUrl + o.Img%>"></em> <%=o.IS_STAFF ? o.ename : "默认技师"%></strong>
                            </div>


                            <div class="aipet-order-ltems-con aipet-order-ltems-con2">
                                <a href="dingdan_detail.aspx?oid=<%=o.ID%>">

                                    <span><bdo>预约项目：</bdo><em><%=o.sname%></em></span>
                                    <span><bdo>预约时间：</bdo><%=o.BEGIN_DATE%>-<%=o.END_DATE%></span>
                                    <span><bdo>服务地址：</bdo><%=o.ADDRESS %></span>
                                </a>
                            </div>
                            <div class="aipet-order-buybtn" style="height: auto;">
                                <strong style="font-size: 12px;">￥<%=o.TOTAL_AMT%><%=(o.TOTAL_AMT-o.PAY_AMT>0?"(储值抵扣￥"+(o.Czdk)+",优惠抵扣￥"+(o.Yhdk)+")":"") %></strong>
                                <%if (o.PAY_STATUS == 0 && o.OverdueDate > DateTime.Now)
                                  { %>
                                <div onclick="window.location='pay.aspx?oid=<%=o.ID %>'"><span class="fukuan">立即支付</span></div>
                                <%}
                                  else if (o.PAY_STATUS == 1 && o.PJID == 0 && o.RealEndDate != null)
                                  { %>
                                <div onclick="window.location='pingfen.aspx?oid=<%=o.ID %>'"><span class="quanma">评价</span></div>
                                <%} %>
                            </div>


                        </li>


                        <% } %>
                    </ul>
                    <ul id="uid1" style="display: none;">
                        <%foreach (var o in olist)
                          {
                              if (o.PAY_STATUS == 1 && o.RealBegDate == null)
                              {
                        %>

                        <li>
                            <div class="aipet-order-ltems-title aipet-order-ltems-title2">
                                <span><%=o.PAY_STATUS == 1 ? "待消费" : o.OverdueDate < DateTime.Now ? "已过期" : "待支付"%></span>
                                <strong><em>
                                    <img src="<%=string.IsNullOrEmpty(o.Img) ? "image/jishi.jpg" : WebUrl + o.Img%>"></em> <%=o.IS_STAFF ? o.ename : "默认技师"%></strong>
                            </div>


                            <div class="aipet-order-ltems-con aipet-order-ltems-con2">
                                <a href="dingdan_detail.aspx?oid=<%=o.ID%>">

                                    <span><bdo>预约项目：</bdo><em><%=o.sname%></em></span>
                                    <span><bdo>预约时间：</bdo><%=o.BEGIN_DATE%>-<%=o.END_DATE%></span>
                                    <span><bdo>服务地址：</bdo><%=o.ADDRESS %></span>
                                </a>
                            </div>
                            <div class="aipet-order-buybtn" style="height: auto;">
                                <strong style="font-size: 12px;">￥<%=o.TOTAL_AMT%><%=(o.TOTAL_AMT-o.PAY_AMT>0?"(储值抵扣￥"+(o.Czdk)+",优惠抵扣￥"+(o.Yhdk)+")":"") %></strong>
                                <%if (o.PAY_STATUS == 0 && o.OverdueDate > DateTime.Now)
                                  { %>
                                <div onclick="window.location='pay.aspx?oid=<%=o.ID%>'"><span class="fukuan">立即支付</span></div>
                                <%} %>
                            </div>


                        </li>

                        <%-- <li>
                            <div class="aipet-order-ltems-title">
                                <span><%=o.PAY_STATUS == 1 ? "待消费" : o.OverdueDate < DateTime.Now ? "已过期" : "待支付"%></span>
                                <strong><%=o.TRANS_DATE.ToString("yyyy.MM.dd HH:mm:ss")%></strong>
                            </div>


                            <div class="aipet-order-ltems-con">
                                <a href="dingdan_detail.aspx?oid=<%=o.ID%>">
                                    <div class="imgbox">
                                        <img src="<%=string.IsNullOrEmpty(o.Img) ? "image/jishi.jpg" : WebUrl + o.Img%>">
                                    </div>
                                    <p><strong><%=o.IS_STAFF ? o.ename : "默认技师"%></strong></p>
                                    <span>订单号：<%=o.ORDER_NO%></span>
                                    <span>项目名称：<%=o.sname%></span>
                                    <span>服务时间：<%=o.BEGIN_DATE%>-<%=o.END_DATE%></span>
                                </a>
                            </div>



                            <div class="aipet-order-buybtn">
                                <strong>￥<%=o.TOTAL_AMT%></strong>
                                <%if (o.PAY_STATUS == 0 && o.OverdueDate > DateTime.Now)
                                  { %>
                                <div onclick="window.location='pay.aspx?oid=<%=o.ID%>'"><span class="fukuan">立即支付</span></div>
                                <%} %>
                            </div>

                            <div class="daodian-fr daodian-icon">到店</div>
                        </li>--%>
                        <% }
                          } %>
                    </ul>
                    <ul id="uid2" style="display: none;">
                        <%foreach (var o in olist)
                          {
                              if (o.PAY_STATUS == 1 && o.PJID == 0 && o.RealEndDate != null)
                              {
                        %>

                        <li>
                            <div class="aipet-order-ltems-title aipet-order-ltems-title2">
                                <span><%=o.PJID!=0?"已完成":o.RealEndDate!=null?"待评价":o.PAY_STATUS == 1 ? "待消费" : o.OverdueDate < DateTime.Now ? "已过期" : "待支付"%></span>
                                <strong><em>
                                    <img src="<%=string.IsNullOrEmpty(o.Img) ? "image/jishi.jpg" : WebUrl + o.Img%>"></em> <%=o.IS_STAFF ? o.ename : "默认技师"%></strong>
                            </div>


                            <div class="aipet-order-ltems-con aipet-order-ltems-con2">
                                <a href="dingdan_detail.aspx?oid=<%=o.ID%>">

                                    <span><bdo>预约项目：</bdo><em><%=o.sname%></em></span>
                                    <span><bdo>预约时间：</bdo><%=o.BEGIN_DATE%>-<%=o.END_DATE%></span>
                                    <span><bdo>服务地址：</bdo><%=o.ADDRESS %></span>
                                </a>
                            </div>
                            <div class="aipet-order-buybtn" style="height: auto;">
                                <strong style="font-size: 12px;">￥<%=o.TOTAL_AMT%><%=(o.TOTAL_AMT-o.PAY_AMT>0?"(储值抵扣￥"+(o.Czdk)+",优惠抵扣￥"+(o.Yhdk)+")":"") %></strong>
                                <%if (o.PAY_STATUS == 0 && o.OverdueDate > DateTime.Now)
                                  { %>
                                <div onclick="window.location='pay.aspx?oid=<%=o.ID %>'"><span class="fukuan">立即支付</span></div>
                                <%}
                                  else if (o.PAY_STATUS == 1 && o.PJID == 0)
                                  { %>
                                <div onclick="window.location='pingfen.aspx?oid=<%=o.ID %>'"><span class="quanma">评价</span></div>
                                <%} %>
                            </div>


                        </li>
                        <% }
                          }%>
                    </ul>
                    <ul id="uid3" style="display: none;">
                        <%foreach (var o in olist)
                          {
                              if (o.PAY_STATUS != 1 && o.OverdueDate < DateTime.Now)
                              {
                        %>

                        <li>
                            <div class="aipet-order-ltems-title aipet-order-ltems-title2">
                                <span><%=o.PJID!=0?"已完成":o.RealEndDate!=null?"待评价":o.PAY_STATUS == 1 ? "待消费" : o.OverdueDate < DateTime.Now ? "已过期" : "待支付"%></span>
                                <strong><em>
                                    <img src="<%=string.IsNullOrEmpty(o.Img) ? "image/jishi.jpg" : WebUrl + o.Img%>"></em> <%=o.IS_STAFF ? o.ename : "默认技师"%></strong>
                            </div>


                            <div class="aipet-order-ltems-con aipet-order-ltems-con2">
                                <a href="dingdan_detail.aspx?oid=<%=o.ID%>">

                                    <span><bdo>预约项目：</bdo><em><%=o.sname%></em></span>
                                    <span><bdo>预约时间：</bdo><%=o.BEGIN_DATE%>-<%=o.END_DATE%></span>
                                    <span><bdo>服务地址：</bdo><%=o.ADDRESS %></span>
                                </a>
                            </div>
                            <div class="aipet-order-buybtn" style="height: auto;">
                                <strong style="font-size: 12px;">￥<%=o.TOTAL_AMT%><%=(o.TOTAL_AMT-o.PAY_AMT>0?"(储值抵扣￥"+(o.Czdk)+",优惠抵扣￥"+(o.Yhdk)+")":"") %></strong>
                                <%if (o.PAY_STATUS == 0 && o.OverdueDate > DateTime.Now)
                                  { %>
                                <div onclick="window.location='pay.aspx?oid=<%=o.ID %>'"><span class="fukuan">立即支付</span></div>
                                <%}
                                  else if (o.PAY_STATUS == 1 && o.PJID == 0)
                                  { %>
                                <div onclick="window.location='pingfen.aspx?oid=<%=o.ID %>'"><span class="quanma">评价</span></div>
                                <%} %>
                            </div>


                        </li>
                        <% }
                          }%>
                    </ul>
                </div>
            </div>
            <!--end content-->


            <div class="footerbar">
                <ul>
                    <li><a href="newindex.aspx"><span class="icon-logo-w"></span><bdo>门店</bdo></a></li>
                    <li class="cur"><a href="dingdan.aspx"><span class="icon-agreement"></span><bdo>订单</bdo></a></li>
                    <li><a href="wo.aspx"><span class="icon-personal"></span><bdo>我的</bdo></a></li>
                </ul>
            </div>

            <!--红包-->
            <%if (Request.QueryString["id"] != null)
              { %>
            <div class="maskbg" style="display: block;" id="shareId">
                <div class="share-box">
                    <div class="share-box-del"></div>
                    <h2>恭喜获得10个红包</h2>
                    <p>分享给小伙伴，大家一起来抢</p>
                    <div class="share-btn">发给好友</div>
                </div>
            </div>
            <input type="hidden" id="ztitle" value="小时光发红包啦，快来拼手气。" />
            <input type="hidden" id="ftitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
            <input type="hidden" id="fxurl" value="<%=WebUrl+"/wechat/Discount/LingHongBao.aspx?id="+Request.QueryString["id"] %>" />
            <%}
              else
              { %>
            <input type="hidden" id="ztitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
            <input type="hidden" id="ftitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
            <input type="hidden" id="fxurl" value="<%=WebUrl+"/wechat/spa/newindex.aspx" %>" />
            <%} %>

            <div class="share" id="shareBox">
                <div class="sharebg"></div>
            </div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocationdingdan.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script>
    $(".order-tag-item ol").click(function () {
        $(".order-tag-item span").removeClass("curbtn");
        $(this).find("span").addClass("curbtn");
        console.log($("#uid" + $(this).find("span").attr("otype")));
        console.log($(this).find("span").attr("otype"));
        $(".aipet-order-items ul").hide();
        $("#uid" + $(this).find("span").attr("otype")).show();
    });

    $('.share-box-del').on('click', function () {
        $('#shareId').hide();
    })
    $('.share-btn').on('click', function () {
        $('#shareId').hide();
        $("#shareBox").show();
    })

    $("#shareBox").click(function () {
        $('#shareId').show();
        $("#shareBox").hide();
    });
</script>
