<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SPACRM.WebApp.wechat.JiShi.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no, minimal-ui">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <title>小时光登录</title>
    <!-- CSS start -->
    <link rel="stylesheet" href="icofont/style.css?v=1">
    <link rel="stylesheet" href="css/style.min.css?v=1">  <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- container start -->
        <div class="container">
            <!-- main start -->
            <main class="main">
            <!-- logo start -->
            <div class="logo-box">
                <img src="img/top.jpg" alt="">
            </div>
            <!-- logo over -->
            <!-- login start -->
            <div class="login-box">
                <div class="inner">
                    <div class="item">
                        <label class="label">+86</label>
                        <div class="input">
                            <input class="phone" type="tel" id="txtphone" placeholder="请输入手机号码">
                        </div>
                    </div>
                    <div class="item">
                        <label class="label">验证码</label>
                        <div class="input">
                            <input class="code" type="tel" id="amt" placeholder="请输入验证码">
                        </div>
                        <button class="get-code" type="button" id="timeid" disabled>获取验证码</button>
                    </div>
                </div>
                <div class="login-alert">
                    <!-- 验证提示 <p>验证失败, 请重新输入</p> -->
                </div>
                <div class="button-box">
                    <button class="button login" type="button" disabled >技师登录</button>
                </div>
            </div>
            <!-- login over -->
        </main>
            <!-- main over -->
        </div>
        <!-- container over -->

                <input type="hidden" id="ztitle" value="技师端操作系统登录" />
        <input type="hidden" id="ftitle" value="技师端操作系统登录" />
    </form>
</body>
</html>
<!-- JavaScript start -->
<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="js/swiper/swiper.min.js"></script>
<script type="text/javascript" src="js/tooltip/jquery.darktooltip.js"></script>
<script type="text/javascript" src="js/scripts.js"></script>
<script src="js/Message2.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<!-- JavaScript over -->

<!-- demo js start -->
<script>
    $(document).ready(function () {
        // 输入电话"获取验证码"样式可用
        $('.phone').bind('keyup', function () {
            $('.get-code').removeAttr('disabled');
        });
        // 输入验证码"技师登录"样式可用
        $('.code').bind('keyup', function () {
            $('.login').removeAttr('disabled');
        });
    });

    var num = 60;
    var timerrr;
    var flag = true;
    var daojishi = document.getElementById('timeid');
    function dao() {
        if (num > 0) {
            num -= 1;
            daojishi.innerHTML = num + "s";
            if (num < 10) {
                daojishi.innerHTML = "0" + num + "s";
            }
        }
        if (num == 0) {
            daojishi.innerHTML = "获取验证码";
            num = 60;
            //daojishi.className = 'yanzhengma';
            flag = true;
            clearInterval(timerrr);
        }

    }
    $("#timeid").click(function () {

        if ($("#txtphone").val().length != 11) {
            $.MsgBox.Alert("小时光", "请输入11位手机号码");
        }
        else if (flag == true) {
            $.ajax({
                type: "POST",
                url: "index.aspx?para=SendDX&phone=" + $("#txtphone").val(),
                async: false,
                timeout: 15000,
                dataType: "json",
                success: function (data) {
                    if (data.status == -1) {
                        $.MsgBox.Alert("小时光", data.message);
                    }
                    else {
                        //daojishi.className = 'yanzhenghui';
                        dao();
                        timerrr = setInterval('dao()', 1000);
                        //tips(data.message);
                        flag = false;
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.MsgBox.Alert("小时光", "发送失败");
                }
            });

        }


    })

    $(".login").click(function () {
        if ($("#txtphone").val().length != 11) {
            $.MsgBox.Alert("小时光", "请输入11位手机号码");
        }
        else if ($("#amt").val() == "") {
            $.MsgBox.Alert("小时光", "请输入验证码");
        }
        else {
            $(".login").hide();
            $.ajax({
                type: "POST",
                url: "index.aspx?para=bang&phone=" + $("#txtphone").val() + "&yzm=" + $("#amt").val(),
                async: false,
                timeout: 15000,
                dataType: "json",
                success: function (data) {
                    $(".login").show();
                    if (data.status == -1) {
                        $.MsgBox.Alert("小时光", data.message);
                    }
                    else {
                        //$.MsgBox.Alert("小时光", data.message, function () {
                        location = data.url;
                        //});
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //$.MsgBox.Alert("小时光", "发送失败");
                }
            });
        }
    });
    </script>
