<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SPACRM.WebApp.wechat.Crowd.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/zhongchou.css" rel="stylesheet" />
    <title>小时光</title>
    <style>
        .detailContent img {
            width:100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%if (crowd != null)
          { %>
        <div class="videoBanner">
            <%--<video class="video-real" id="video1" controls="controls" preload="auto" playsinline="" poster="http://images.kaistart.com/o_1bgo2oneb11o51smu7vf7tl1qje14.jpg?imageView2/1/w/900/h/506" src="http://video.kaistart.com/iPgzfOjmgRFhpep7yrTYJsdxg9s=/ljdhJCMraQ3Doar2bZqIncHBEKdW">
                <p>您的浏览器不支持 video 标签。</p>
            </video>--%>
            <img src="images/topimg.jpg" style="width:100%;" />
        </div>

        <div class="zhongchouzhe">
            <div class="zhongchouInfo">
                <p>
                    <strong>
                        <img src="images/12.jpg" /></strong><bdo><%=crowd.NiceName %></bdo>
                    <%=crowd.CreateTime.Value.ToString("yyyy年MM月dd日") %>  <%=crowd.City %>
                </p>
            </div>
            <div class="zhongchouText">
                <h2><%=crowd.Title %></h2>
                <p><%=crowd.Detail %></p>
            </div>
            <div class="zhongchouLine">
                <p><span><%=Convert.ToInt32( crowd.YCJE*100/crowd.MB) %>%</span> 已认筹<strong><%=crowd.YCJE %></strong></p>
                <div class="quanline">
                    <div style="width: <%=Convert.ToInt32( crowd.YCJE*100/crowd.MB) %>%" class="curline"></div>
                </div>
                <p class="moren"><span>进行中</span><bdo>目标金额(￥)<%=crowd.MB %></bdo>认筹人数 <%=crowd.YCRS %></p>
            </div>
        </div>


        <div class="zhongchouDetail">
            <div class="zhongchouTab" id="tabmenu">
                <ol><a href="#intro"><span class="curtab">我的故事</span></a></ol>
                <ol><a href="#pro"><span>项目信息</span></ol>
                <ol><a href="#jindu"><span>我的回报</span></ol>
                <ol><a href="#fenxian"><span>团队介绍</span></ol>
            </div>

            <div class="detailContent" id="intro">
                <%=crowd.TW1 %>

            </div>

            <div class="detailContent" id="pro">
                <%=crowd.TW2 %>
            </div>

            <div class="detailContent" id="jindu">
                <%=crowd.TW3 %>
            </div>

            <div class="detailContent" id="fenxian">
                <%=crowd.TW4 %>
            </div>

        </div>
        <br /><br />
        <div class="footer">
            <div class="fanhui">
                <div class="fanhuiIcon"><span class="icon-back"></span></div>
            </div>
            <div class="pinglunBox" >
                <div class="pinglunIcon">&nbsp;</div>
            </div>
            <a href="zhichi.aspx?id=<%=crowd.ID %>">
                <div class="zhichiBtn">
                    我要支持
                </div>
            </a>
        </div>
        <%} %>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js" ></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script type="text/javascript">
    var oTop = $("#tabmenu").offset().top;
    var sTop = 0;
    $(window).scroll(function () {
        sTop = $(this).scrollTop();
        if (sTop >= oTop) {
            $("#tabmenu").css({ "position": "fixed", "top": "0" });
        } else {
            $("#tabmenu").css({ "position": "static" });
        }
    });

    var $root = $('html, body');
    $('#tabmenu a').click(function () {
        $('#tabmenu ol span').removeClass('curtab');
        $(this).find('span').addClass('curtab');
        $root.animate({
            scrollTop: $($.attr(this, 'href')).offset().top - 50
        }, 500);
        return false;
    });
</script>
