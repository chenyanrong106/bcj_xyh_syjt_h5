<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DongTai.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.DongTai" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/dongtai.css" rel="stylesheet" />
    <link href="css/lightbox.css" rel="stylesheet" />
    <title>动态</title>
    <style>
        .aipet-dongtai-imgbox img {
            -webkit-border-radius: 20px;
            -moz-border-radius: 20px;
            border-radius: 5px;
            -webkit-box-shadow: inset 0 1px 5px rgba(0,0,0,.5);
            -moz-box-shadow: inset 0 1px 5px rgba(0,0,0,.5);
            box-shadow: inset 0 1px 5px rgba(0,0,0,.5);
        }

        .acc-top-header {
            position: fixed;
            left: 0;
            top: 0;
            right: 0;
            height: 44px;
            z-index: 99999;
            display: block;
            width: 100%;
            line-height: 44px;
            background: #fff;
            max-width: 75rem;
            margin: 0 auto;
        }

            .acc-top-header ol {
                float: left;
                height: 44px;
                display: block;
                text-align: center;
            }

        .width20 {
            width: 20%;
        }

        .width60 {
            width: 60%;
        }

        .acc-top-header ol strong {
            font-size: 1.8rem;
            line-height: 44px;
            display: block;
            color: #ff7200;
            font-weight: normal;
        }

        .acc-top-header .guanzhu {
            background: url(../images/weixin.png) no-repeat 0 center;
            background-size: 25px;
            font-size: 1.4rem;
            color: #666;
            padding-left: 30px;
            display: inline-block;
        }

        .acc-top-header .wodeicon {
            background: url(../images/wode.png) no-repeat 0 center;
            background-size: 25px;
            font-size: 1.4rem;
            color: #666;
            padding-left: 30px;
            display: inline-block;
        }

        .acc-top-header .fanhui {
            background: url(../images/fanhui.png) no-repeat 0 center;
            background-size: 25px;
            font-size: 1.4rem;
            color: #666;
            padding-left: 30px;
            display: inline-block;
        }

        .acc-top-header .xiangicon {
            font-size: 12px;
            color: #666;
            display: inline-block;
            text-align: center;
        }

        /*橙色底的头部*/
        .acc-top-header-cheng {
            background: #ff7200;
        }

            .acc-top-header-cheng ol strong {
                font-size: 1.8rem;
                line-height: 44px;
                display: block;
                color: #fff;
                font-weight: normal;
            }

            .acc-top-header-cheng .wodeicon {
                background: url(images/wode_baise.png) no-repeat 0 center;
                background-size: 25px;
                font-size: 1.4rem;
                color: #fff;
                padding-left: 30px;
                display: inline-block;
            }

            .acc-top-header-cheng .fanhui {
                background: url(images/fanhui_baise.png) no-repeat 0 center;
                background-size: 25px;
                font-size: 1.4rem;
                color: #fff;
                padding-left: 30px;
                display: inline-block;
            }

            .acc-top-header-cheng .xiangicon {
                background: url(images/xg.png) no-repeat 0 center;
                background-size: 25px;
                font-size: 1.4rem;
                color: #fff;
                padding-left: 30px;
                display: inline-block;
            }
        /*header end*/
    </style>
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
        <div class="acc-top-header acc-top-header-cheng" style="border-bottom: 1px solid #eee;">
            <ol class="width20" onclick="window.location='chou.aspx?p=<%=Request.QueryString["p"]??"1" %>'"><span class="fanhui">返回</span></ol>
            <ol class="width60"><strong>爱宠筹</strong></ol>
            <ol class="width20" onclick="window.location='wo.aspx?p=<%=Request.QueryString["p"]??"1" %>'"><span class="wodeicon">我的</span></ol>
        </div>
        <div class="aipet-dongtai" style="padding-top: 46px;">
            <ul>
            </ul>
        </div>

        <div class="footerbar">
            <ul>
                <li><a href="chou.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="homehui">筹粮</bdo></a></li>
                <li><a href="choupage.aspx?p=<%=Request.QueryString["p"]??"1" %>&jztype=2"><bdo class="aixinhui">筹款</bdo></a></li>
                <li onclick="window.location='sq1.aspx'"><bdo class="jiahao"></bdo></li>
                <li><a href="dongtai.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="huzhu">动态</bdo></a></li>
                <li><a href="wo.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="adminhui">我</bdo></a></li>
            </ul>
        </div>

        <input type="hidden" id="ztitle" value="快来看啊，又有新动态更新啦！" />
        <input type="hidden" id="ftitle" value="此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办" />
        <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="js/oa2.js"></script>
<script type="text/javascript" src="js/lazy.js"></script>
<script type="text/javascript" src="js/swiper.js"></script>
<script src="js/lightbox-2.6.min.js"></script>
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script type="text/javascript">
    echo.init({
        offset: 100,
        throttle: 250
    });
    var swiper02 = new Swiper('.acc-news-pro', {
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
            url: "dongtai.aspx?para=tj&page=" + pageNum,
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


                        $('.aipet-dongtai ul').append(data);
                        //$("#loading").show();
                        isLoading = false;

                        pageNum++;
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
