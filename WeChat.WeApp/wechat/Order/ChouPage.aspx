<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChouPage.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.ChouPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>爱宠筹</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <script src="js/jquery.min.js"></script>
    <script src="js/oa.js?v=2"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/GetLocation.js?v=1.0"></script>
    <script src="js/jquery.masonry.min.js"></script>
    <script src="js/lightbox-2.6.min.js"></script>
    <script type="text/javascript" src="js/lazy.js"></script>
    <script type="text/javascript" src="js/swiper.js"></script>
    <script src="js/echo.js"></script>
    <link href="css/pj.css" rel="stylesheet" />
    <link type="text/css" href="css/main.css" rel="stylesheet" />
    <link href="css/lightbox.css" rel="stylesheet" />
    <style>
        .footerbar {
            width: 100%;
            height: 40px;
            background: #fbfaf8;
            border-top: 1px solid #ddd;
            position: fixed;
            bottom: 0;
            left: 0;
            min-width: 320px;
            padding: 5px 0;
            z-index: 9999999;
        }

            .footerbar ul li {
                float: left;
                width: 20%;
                display: block;
                text-align: center;
                font-weight: normal;
            }

        .jiahao {
            width: 42px;
            height: 42px;
            background: url(img/jiahao.png) no-repeat center center #ff7200;
            background-size: 22px;
            border-radius: 42px;
            display: block;
            margin: 0px auto;
        }

        .home {
            background: url(img/home.png) no-repeat center 3px;
            padding-top: 25px;
            height: 14px;
            display: block;
            background-size: 22px;
            font-size: 12px;
            color: #ff7200;
        }

        .homehui {
            background: url(img/home2.png) no-repeat center 3px;
            padding-top: 25px;
            height: 14px;
            display: block;
            background-size: 22px;
            font-size: 12px;
            color: #999;
        }

        .aixin {
            background: url(img/aixin.png) no-repeat center 1px;
            padding-top: 25px;
            height: 14px;
            display: block;
            background-size: 24px;
            font-size: 12px;
            color: #ff7200;
        }

        .aixinhui {
            background: url(img/aixin2.png) no-repeat center 1px;
            padding-top: 25px;
            height: 14px;
            display: block;
            background-size: 24px;
            font-size: 12px;
            color: #999;
        }

        .huzhu {
            background: url(img/huzhu.png) no-repeat center 1px;
            padding-top: 25px;
            height: 14px;
            display: block;
            background-size: 24px;
            font-size: 12px;
            color: #ff7200;
        }

        .huzhuhui {
            background: url(img/huzhu2.png) no-repeat center 1px;
            padding-top: 25px;
            height: 14px;
            display: block;
            background-size: 24px;
            font-size: 12px;
            color: #999;
        }

        .admin {
            background: url(img/admin.png) no-repeat center 3px;
            padding-top: 25px;
            height: 14px;
            display: block;
            background-size: 20px;
            font-size: 12px;
            color: #ff7200;
        }

        .adminhui {
            background: url(img/admin2.png) no-repeat center 3px;
            padding-top: 25px;
            height: 14px;
            display: block;
            background-size: 20px;
            font-size: 12px;
            color: #999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!--新版-->
        <div class="acc-top-header" style="border-bottom: 1px solid #eee;">
            <ol class="width20" onclick="window.location='chou.aspx?p=<%=Request.QueryString["p"]??"1" %>';"><span class="fanhui">返回</span></ol>
            <ol class="width60"><strong>爱宠筹</strong></ol>
            <ol class="width20" onclick="window.alert('即将开放');"><span class="wodeicon">我的</span></ol>
        </div>

        <div class="acc-petchat">
            <div class="shoplist">

                <ul id="listing">
                </ul>
                <div id="loading">上拉获取更多数据...</div>
                <div id="nomoreData" style="margin-bottom:120px;">亲，已经加载完了，系统默认加载12条数据！</div>

            </div>
        </div>

        <!--新版-->
        <div class="footerbar">
            <ul>
                <%if (Request.QueryString["jztype"] == "1")
                  { %>
                <li><a href="chou.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="homehui">筹粮</bdo></a></li>
                <li><a href="choupage.aspx?p=<%=Request.QueryString["p"]??"1" %>&jztype=2"><bdo class="aixinhui">筹款</bdo></a></li>
                <%}
                  else
                  { %>
               <li><a href="chou.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="homehui">首页</bdo></a></li>
                <li><a href="choupage.aspx?p=<%=Request.QueryString["p"]??"1" %>&jztype=2"><bdo class="aixin">筹款</bdo></a></li>
                <%} %>
                <li onclick="window.location='sq1.aspx'"><bdo class="jiahao"></bdo></li>
                <li><a href="dongtai.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="huzhuhui">动态</bdo></a></li>
                <li><a href="wo.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="adminhui">我</bdo></a></li>
            </ul>
        </div>

        <input type="hidden" id="ztitle" value="帮助可爱的毛孩子们吃上饱饭" />
        <input type="hidden" id="ftitle" value="此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办" />
        <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />
        <input type="hidden" id="jztype" value="<%=Request.QueryString["jztype"]??"1" %>" />
    </form>
</body>
</html>
<script>

    echo.init({
        offset: 100,
        throttle: 250, unload: false
    });

    var tipsflag = true;
    function tips(text) {
        if (tipsflag == true) {
            var tishiDiv = document.createElement('div');
            tishiDiv.className = "motify";
            document.body.appendChild(tishiDiv);
            tipsflag = false;
        }
        $('.motify').html(text).show();
        setTimeout(function () { $('.motify').fadeOut(); }, 500);
    }

    $(function () {
        var $this = $("#wenzhang");
        var scrollTimer;
        $this.hover(function () {
            clearInterval(scrollTimer);
        }, function () {
            scrollTimer = setInterval(function () {
                scrollNews($this);
            }, 4000);
        }).trigger("mouseout");
    });

    function scrollNews(obj) {
        var $self = obj.find("ul:first");
        var lineHeight = $self.find("li:first").height();
        $self.animate({ "margin-top": -lineHeight + "px" }, 500, function () {
            $self.css({ "margin-top": "0px" }).find("li:first").appendTo($self);
        })
    }


    var swiper = new Swiper('.swiper-container', {
        slidesPerView: 'auto',
        paginationClickable: true,
        spaceBetween: 1
    });

    //数据加载
    var pageNum = 1; rowsNum = 10;
    var isNoMoreData = false, isLoading = false;
    $(window).scroll(function () {
        echo.render();
        var curTop = $(window).scrollTop();
        var juli = $(document).height() - $(window).height();
        if (curTop >= juli) {
            if (!isNoMoreData && !isLoading) {
                showPetdata(pageNum + 1);
            }
        }
    });
    showPetdata();
    function showPetdata() {
        $.ajax({
            type: "Get",
            url: "choupage.aspx?para=tj&page=" + pageNum + "&p=" + $("#urlpara").val() + "&jztype=" + $("#jztype").val(),
            dataType: 'html',
            async: false,
            beforeSend: function () {
                isLoading = true;
                $("#loading").show();
            },
            success: function (data) {
                //alert(data);
                //var data = JSON.parse(json);
                if (data !== null) {

                    if (data == "") {
                        $('#nomoreData').show();
                        $("#loading").hide();
                        isNoMoreData = true;
                    } else {


                        $('.shoplist ul').append(data);
                        $("#loading").show();
                        isLoading = false;

                        pageNum++;
                    }
                } else {
                    isNoMoreData = true;
                    $('#nomoreData').show();
                    $("#loading").hide();
                }
            },
            error: function (error) {
                console.log(error);
            }
        });

    }

    $("#rule1").click(function () {
        $("#dialog1").show();
    });

    $(".btn_close").click(function () {
        $("#dialog1").hide();
    });
</script>
