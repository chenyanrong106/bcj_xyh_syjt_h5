<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminOrder.aspx.cs" Inherits="SPACRM.WebApp.wechat.JiShi.AdminOrder" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="小时光">
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
     <link type="text/css" rel="stylesheet" href="css/index.css" />
   
    <title>小时光</title>
    <script src="js/tz.js"></script>
    <style>
        
footer.footer {
  position: fixed;
  z-index: 2;
  bottom: 0;
  left: 0;
  right: 0;
  width: 100%;
  background-color: #ffffff;
  border-top: 1px solid #eeeeee;
  overflow: visible;
}

footer.footer img.shadow {
  position: absolute;
  bottom: 100%;
}

footer.footer .inner {
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  display: -webkit-flex;
  display: -ms-flexbox;
  display: flex;
  -webkit-align-items: center;
  -ms-flex-align: center;
  align-items: center;
  height: 49px;
  min-height: 49px;
}

footer.footer nav.tabbar a {
  -webkit-flex: 1;
  -ms-flex: 1;
  flex: 1;
  display: block;
  padding: 4px 0;
  color: #aeaeae;
  text-align: center;
}

footer.footer nav.tabbar a p {
  font-size: 10px;
}

footer.footer nav.tabbar a .icon {
  width: 24px;
  height: 24px;
  margin: 0 auto 1px;
  color: #aeaeae;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}

footer.footer nav.tabbar a.active {
  color: #2c2c2c;
}

footer.footer nav.tabbar a:nth-child(1) .icon {
  background-image: url("img/tabbarIcon01@2x.png");
}

footer.footer nav.tabbar a:nth-child(1).active .icon {
  background-image: url("img/tabbarIcon01Active@2x.png");
}

footer.footer nav.tabbar a:nth-child(2) .icon {
  background-image: url("img/tabbarIcon02@2x.png");
}

footer.footer nav.tabbar a:nth-child(2).active .icon {
  background-image: url("img/tabbarIcon02Active@2x.png");
}

footer.footer nav.tabbar a:nth-child(3) .icon {
  background-image: url("img/tabbarIcon03@2x.png");
}

footer.footer nav.tabbar a:nth-child(3).active .icon {
  background-image: url("img/tabbarIcon03Active@2x.png");
}

footer.footer a.button {
  display: inline-block;
  width: 180px;
  height: 36px;
  line-height: 36px;
  margin: 0 auto;
  border-radius: 18px;
  color: #ffffff;
  font-size: 14px;
  text-align: center;
  background-color: #53c7c6;
}

footer.footer a.button:active {
  background-color: #39aead;
}

footer.footer a.button.outlineButton {
  color: #53c7c6;
  border: 1px solid #53c7c6;
  background-color: #ffffff;
}

footer.footer a.button.outlineButton:active {
  color: #ffffff;
  background-color: #53c7c6;
}

footer.footer a.button.disabled {
  color: #d7d7d7;
  border-color: #d7d7d7;
  background-color: #f5f6f7;
}

footer.footer a.button.disabled:active {
  color: #d7d7d7;
  border-color: #d7d7d7;
  background-color: #f5f6f7;
}

footer.footer.border-none {
  border-top: none;
}

footer.footer ~ main.main {
  margin-bottom: 49px;
}

    </style>

    <style>
        .footerbar{width:100%; height:40px; background:rgba(243,243,243,1); border-top:1px solid #f3f3f3; position:fixed; bottom:0; left:0; right:0; display:block; padding:4px 0; z-index:9999;max-width:75rem; margin:0 auto;}
.footerbar ul li{float:left; width:33.33%; display:block; text-align:center;}
.footerFour ul li{float:left; width:25%; display:block; text-align:center;}

.footerbar ul li span{ display:block; font-weight:normal; display:block; font-size:1.5em; margin-top:2px;color: #535353;}
.footerbar ul li.cur span,.footerbar ul li.cur bdo{ color:#ff5c67;}
.footerbar ul li bdo{ font-size:12px; color: #535353;text-align:center; display:block;}
    </style>
</head>
<body>
    <form id="form1" runat="server">

       
         <%--<footer class="footer">
            <img class="shadow" src="img/shadow@2x.png" alt="">
            <nav class="inner tabbar">
                <a href="homeadmin.aspx">
                    <div class="icon"></div>
                    <p>首页</p>
                </a>
                <a class="active" href="adminorder.aspx">
                    <div class="icon"></div>
                    <p>订单</p>
                </a>
                <a href="javascript:;">
                    <div class="icon"></div>
                    <p>我</p>
                </a>
            </nav>
        </footer>--%>
        <!--begin header-->
        <div class="meiheader">
            <div class="askboxcon">
                <a href="homeadmin.aspx"><span>&nbsp;</span></a>
                <h2>
                    <select class="dropdown-select-nav" style="width:100%;" id="storeid">
                        <%if(OpenID=="oS7pm1gdR31eq-BcyKUHWxgAvsoY"){ %>
                         <option value="31">小时光Massage（嘉里不夜城店）</option>
                        <%}else{ %>
                        <%foreach (var s in list)
                          {
                              %>
                        <option value="<%=s.ID %>" <%=Request.QueryString["sid"]==s.ID.ToString()?"selected":"" %>><%=s.NAME %></option>
                        <%
                          } }%>
                        
                       <%-- <option>2017年5月</option>
                        <option>2017年4月</option>
                        <option>2017年3月</option>
                        <option>2017年2月</option>--%>
                    </select></h2>
                <a href="tel:17621160808;"><span class="icon-tel"></span></a>
            </div>

            <ul class="lnks">
        </div>
        <!--end header-->

        <div class="order-date-item">
            <div class="swiper-container">
                <div class="swiper-wrapper" id="order-datelist">
                    <div class="swiper-slide">
                        <div class="<%=Request.QueryString["rq"]==null?"curdate":(Request.QueryString["rq"]==DateTime.Now.ToString("yyyyMMdd")?"curdate":JSOrderNum.Where(n => n.rq == DateTime.Now.ToString("yyyyMMdd")).ToList().Count>0?"dingdan-icon":"quanxiu-icon") %>"><span><em rq="<%=DateTime.Now.ToString("yyyyMMdd") %>"><%=DateTime.Now.Day %></em></span>今天<%=JSOrderNum.Where(n => n.rq == DateTime.Now.ToString("yyyyMMdd")).ToList().Sum(a=>a.num) %>单</div>
                    </div>
                    <%for (int i = 1; i < 8; i++)
                      {
                          DateTime d = DateTime.Now.AddDays(i);
                          List<SPACRM.Entity.Entities.JiShiOrderNum_EX> jsnum = JSOrderNum.Where(n => n.rq == d.ToString("yyyyMMdd")).ToList();
                          if (jsnum.Count > 0)
                          {
                    %>
                    <div class="swiper-slide">
                        <div class="<%=Request.QueryString["rq"]==d.ToString("yyyyMMdd")?"curdate":"dingdan-icon" %>"><span><em rq="<%=d.ToString("yyyyMMdd") %>"><%=d.Day %></em></span>有<%=jsnum[0].num %>单</div>
                    </div>
                    <%
                          }
                          else
                          {
                    %>
                    <div class="swiper-slide">
                        <div class="<%=Request.QueryString["rq"]==d.ToString("yyyyMMdd")?"curdate":"quanxiu-icon" %>"><span><em rq="<%=d.ToString("yyyyMMdd") %>"><%=d.Day %></em></span>无订单</div>
                    </div>
                    <%
                          }
                      } %>
                </div>
            </div>
        </div>


        <!--begin content-->
        <div class="meiWapper mei-index-bottom date-index-Top">
            <div class="order-content">




                <div class="aipet-order-items">
                    <ul>
                        <%foreach (var o in JSOrderList)
                          {
                        %>
                        <li>
                            <div class="aipet-order-ltems-title aipet-order-ltems-title-new">
                               <span><%=o.PAY_STATUS!=1?"未支付":"已支付" %></span>
                                <strong>订单号：<%=o.ORDER_NO %> </strong>
                            </div>


                            <div class="aipet-order-ltems-con">
                                <a href="javascript:;">
                                    <div class="imgTubox">
                                        <div class="imgbox">
                                            <img src="<%=string.IsNullOrEmpty(o.jsimg)?"image/jishi.jpg":WebUrl+o.jsimg %>"">
                                        </div>
                                    </div>
                                    <div class="imginfo">
                                        <p><bdo>￥<%=o.PAY_AMT %></bdo><strong><%=o.CUST_NAME %></strong></p>
                                        <span><%=o.CATEGORY_NAME %></span>
                                         <span>预约门店：<%=o.STORE_NAME %></span>
                                        <span>预约技师：<%=o.jsname %></span>
                                        <span>服务时间：<em><%=o.BEGIN_DATE.ToString("HH:mm") %>-<%=o.END_DATE.ToString("HH:mm") %></em></span>
                                        <span>下单时间：<em><%=o.CREATE_DATE.ToString("yyyy.MM.dd HH:mm:ss") %></em></span>
                                         <span>来源：<em><%=o.SOURCE=="0"?"PC端":o.SOURCE=="2"?"微信下单":o.SOURCE=="4"?"在线买单":o.SOURCE=="3"?"直接到店":"其他" %></em></span>
                                    </div>
                                </a>
                            </div>
                            <div class="aipet-order-buybtn aipet-order-buybtn-new">
                                <strong>时长：<%=o.timelengh %>分钟</strong>
                                <%if (o.RealBegDate == null)
                                  { %>
                                <div><span class="fukuan" onclick="updatezt(<%=o.ID %>,1,'上钟');">开始上钟</span></div>
                                <%}
                                  else if (o.RealEndDate == null)
                                  { %>
                                <div><span class="quanma" onclick="updatezt(<%=o.ID %>,2,'下钟');">开始下钟</span></div>
                                <%}
                                  else
                                  { %>
                                <div><span class="fukuan">已完成</span></div>
                                <%} %>
                            </div>

                        </li>
                        <%}
                          if (JSOrderList.Count == 0)
                          {
                        %>
                        <li>
                            <div class="aipet-order-ltems-title aipet-order-ltems-title-new" style="width: 100%; text-align: center;">

                                <strong><%=Request.QueryString["rq"]+":" %>无订单 </strong>
                            </div>
                        </li>
                        <%                          } %>
                    </ul>
                </div>
            </div>
            <!--end content-->
            <%if(OpenID!="oS7pm1gdR31eq-BcyKUHWxgAvsoY"){ %>
             <div class="footerbar footerFour">
    <ul>
        <li><a href="homeadmin.aspx"><span class="icon-logo-w"></span><bdo>首页</bdo></a></li>
        <li class="cur"><a href="adminorder.aspx"><span class="icon-order-f"></span><bdo>订单</bdo></a></li>
        <li><a href="yeji.aspx"><span class="icon-package"></span><bdo>业绩</bdo></a></li>
        <li><a href="javascript:;"><span class="icon-personal"></span><bdo>我</bdo></a></li>
    </ul>
</div>
            
            <%} %>
            <input type="hidden" id="sid" value="<%=Request.QueryString["sid"]%>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/function.js"></script>
<script type="text/javascript" src="js/swiper.js"></script>
<script src="js/Message2.js"></script>
<script>
    //var rilihtml = '';
    //var now = new Date();
    //var days = now.getDate();
    //var months = now.getMonth() + 1;
    //for (var i = 1; i < 14; i++) {
    //    var date = new Date(now.getTime() + i * 24 * 3600 * 1000);
    //    var month = date.getMonth() + 1;
    //    var day = date.getDate()
    //    if (day < 10) {
    //        day = '0' + day;
    //    }
    //    var week = date.getDay();
    //    var datas = [['duanxiu-icon', '短休'], ['quanxiu-icon', '全天休息'], ['quanxiu-icon', '全天休息'], ['dingdan-icon', '有订单'], ['dingdan-icon', '有订单'], ['duanxiu-icon', '短休'], ['duanxiu-icon', '短休'], ['quanxiu-icon', '全天休息'], ['quanxiu-icon', '全天休息'], ['dingdan-icon', '有订单'], ['dingdan-icon', '有订单'], ['duanxiu-icon', '短休'], ['duanxiu-icon', '短休']];
    //    rilihtml += ' <div class="swiper-slide"><div class="' + datas[i - 1][0] + '"><span><em>' + day + '</em></span>' + datas[i - 1][1] + '</div></div>';
    //}
    //if (days < 10) {
    //    days = '0' + days;
    //}
    //$('#order-datelist').html('<div class="swiper-slide"><div class="curdate"><span><em>' + days + '</em></span>今天</div></div>' + rilihtml);

    $('#order-datelist').find('.swiper-slide').on('click', function () {
        location = "adminorder.aspx?rq=" + $(this).find('em').attr("rq") + "&sid=" + $("#storeid").val();
        //alert('你选择了'+ $(this).find('em').attr("rq"));
    })

    $("#storeid").change(function () {
        location = "adminorder.aspx?rq=" + $('#order-datelist').find('.swiper-slide').find('em').attr("rq") + "&sid=" + $("#storeid").val();
    });

    var swiper = new Swiper('.swiper-container', {
        slidesPerView: 7
    });

    function updatezt(id, zt, msg) {

        $.MsgBox.Confirm("小时光", "是否确认" + msg, function () {
            $.ajax({
                url: 'adminorder.aspx?para=SZ&zt=' + zt + "&id=" + id,

                type: 'POST',

                data: {},

                dataType: 'json',

                timeout: 1000,

                error: function (e) { },

                success: function (result) {

                    if (result.st == 1) {
                        $.MsgBox.Alert("小时光", msg + "成功", function () { location = location; });
                    }
                    else {
                        $.MsgBox.Alert("小时光", msg + "失败", function () { location = location; });
                    }
                }

            });
        });
    }

    if ($("#sid").val() == "") {
        location = "adminorder.aspx?rq=" + $(this).find('em').attr("rq") + "&sid=" + $("#storeid").val();
    }
</script>
