<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wo.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.wo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/main.css" rel="stylesheet" />
    <title>个人中心</title>
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
            background: url(images/weixin.png) no-repeat 0 center;
            background-size: 25px;
            font-size: 1.4rem;
            color: #666;
            padding-left: 30px;
            display: inline-block;
        }

        .acc-top-header .wodeicon {
            background: url(images/wode.png) no-repeat 0 center;
            background-size: 25px;
            font-size: 1.4rem;
            color: #666;
            padding-left: 30px;
            display: inline-block;
        }

        .acc-top-header .fanhui {
            background: url(images/fanhui.png) no-repeat 0 center;
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
        <div class="acc-top-header acc-top-header-cheng">
            <ol class="width20" onclick="window.location='chou.aspx?p=<%=Request.QueryString["p"]??"1" %>';"><span class="fanhui">返回</span></ol>
            <ol class="width60"><strong>爱宠筹</strong></ol>
            <ol class="width20" onclick="window.alert('即将开放');"><span class="wodeicon">我的</span></ol>
        </div>

        <div class="acc-login-box acc-wo-touxing" style="padding-top:46px;">
            <div class="acc-login-touxiang">
                <img src="<%if (oa != null)
                            { %><%=oa.headimgurl %><%}
                            else
                            { %><%= "../../assets/images/logo.jpg"%><%} %>" />
            </div>
            <div class="acc-login-name">
                <%if (oa != null)
                  { %><%=oa.Nickname %><%}
                  else
                  { %>爱宠筹<%} %>
            </div>
            <div class="acc-nav-item" style="border-top: 1px solid #eee;">
                <ol onclick="window.location='fabu.aspx?p=<%=Request.QueryString["p"]??"1" %>'">发起<span><%=mypost %></span></ol>
                <ol>支持<span><%=myzc %></span></ol>
                <ol onclick="window.location='guanzhu.aspx?p=<%=Request.QueryString["p"]??"1" %>'">关注<span><%=mystar %></span></ol>
            </div>
        </div>


        <div class="paylist paylistbottom">
            <ol>
                <a href="javascript:;">
                    <p><span><%=Math.Round(mycl/16,2) %>kg</span><bdo class="juanicon01">我的捐粮</bdo></p>
                </a>
            </ol>
            <ol>
                <a href="javascript:;">
                    <p><span><%=myck %>元</span><bdo class="juanicon02">我的捐款</bdo></p>
                </a>
            </ol>
        </div>

        <div class="acc-jilu-juanTT">
            <span id="jilujuanId">
                <em class="curbtn">按捐粮排列</em>
                <em>按捐款排列</em>
            </span>
            <strong>捐粮记录</strong>
        </div>
        <div class="acc-jili-items">
            <ul class="dis" id="jilu-items01">
            </ul>
            <ul class="undis" id="jilu-items02">
            </ul>
        </div>
        <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="js/oa2.js"></script>
<script>
    var jztype = 1;
    $('#jilujuanId em').on('click', function () {
        $(this).addClass('curbtn').siblings().removeClass('curbtn');
        $('#jilu-items0' + ($(this).index() + 1)).show().siblings().hide();
        if ($(this).index() == 0) {
            jztype = 1;
            $('#jilujuanId').next('strong').html('捐粮记录');
        }
        if ($(this).index() == 1) {
            jztype = 2;
            $('#jilujuanId').next('strong').html('捐款记录');
        }
        $container.html("");
        $container2.html("");
        num = 1;
        num2 = 1;
        getList2();
    })

    var flag = true;
    var $container = $('#jilu-items01');
    var $container2 = $('#jilu-items02');
    //按需加载
    $(window).scroll(function () {
        // 当滚动到最底部以上100像素时， 加载新内容
        if ($(document).height() - $(this).scrollTop() - $(this).height() < 50) {
            if (flag) {
                flag = false;
                getList2();
            }
        }
    });
    var num = 1;  //初始默认为1,前面已经加载过一次
    var num2 = 1;
    function getList2() {
        if (jztype == 1) {
            getList(num);
        }
        else {
            getList(num2);
        }
        //$container.append($boxes);
    }
    function getList(n) {
        var boxes = [];
        $.ajax({
            type: "get",
            url: "wo.aspx?para=tj&page=" + n + "&jztype=" + jztype + "&p=" + $("#urlpara").val(),
            beforeSend: function (XMLHttpRequest) {
                // $("#div1").html('<div class="loading">正在加载</div>');
            },
            success: function (data, textStatus) {
                if (isNaN(data)) {
                    if (jztype == 1) {
                        num++;
                        flag = true;
                        boxes.push(data);
                        var $boxes = $(boxes.join(""));
                        $container.append($boxes);
                    }
                    else {
                        num2++;
                        flag = true;
                        boxes.push(data);
                        var $boxes = $(boxes.join(""));
                        $container2.append($boxes);
                    }
                } else {
                    return false;
                }
            },
            complete: function (XMLHttpRequest, textStatus) {
                // alert("加载完成！");
            },
            error: function () {
                //alert("加载出错！");
            }
        });
        //把数组转成字符串
        return boxes.join("");
    };

    getList2();
</script>
