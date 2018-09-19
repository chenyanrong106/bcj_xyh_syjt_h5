<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SPACRM.WebApp.wechat.XYH_Coupon_H5.html.Home" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="Keywords" content="" />
    <meta name="Description" content="" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/new_style.css" />

    <title>佰草集璀璨礼</title>
</head>
<body>
    <div class="wrap index-page">

        <div class="container">
            <img class="home" src="../images/home.png" alt="" width="88%" onclick="return false">

            <div class="form">
                <ul>
                    <li>
                        <input type="tel" placeholder="填写您的手机号" id="tel" name="telenum" maxlength="11" /></li>
                    <li>
                        <input type="tel" id="code" name="verifycode" maxlength="6" /><em id="timeid">获取验证码</em></li>
                    <li id="submit"><a href="javascript:void(0);" class="login">提&nbsp;交</a></li>
                </ul>
            </div>

        </div>
    </div>
    <div class="layer modaltips" id="modaltips"></div>

    <!-- layOut -->
    <div class="layout">
        <div class="share">
            <img src="../images/share.png" alt="">
        </div>
        <div class="text">
            <p>感谢您的参与，欢迎邀请</p>
            <p>非佰草集/华美家的闺蜜参与哦！</p>
        </div>
    </div>

    <script src="../js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="../js/jquery.cookie.js" type="text/javascript" charset="utf-8"></script>
    <script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
    <script src="../js/wxshare.js"></script>
    <script src="../js/requestparams.js"></script>
    <script src="../js/pv.record.js"></script>
    <script src="../js/validate.js" type="text/javascript" charset="utf-8"></script>
    <script>
        $(function () {
            try {

                //var millisecond = 0;
                //var isFinished = false;
                //var int = setInterval(timer, 50);
                ////超过一秒，页面还未出现的话，就弹出提示
                //function timer() {
                //    millisecond += 50;
                //    if (millisecond >= 50 && !isFinished) {
                //       // tips("数据加载中，请稍候！", true, 60000, true);
                //        window.clearInterval(int);
                //    }
                //}



                $('#timeid').click(function () {
                    var phone = $('input[name=telenum]').val();
                    if (phone == '') {
                        tips("请输入手机号码", true, 2000, true, true);
                    } else if (isPhone(phone)) {
                        if (flag == true) {
                            dao();
                            timerrr = setInterval('dao()', 1000);
                            flag = false;
                            //调短信验证码接口,返回的数据为验证码数据；
                            $.ajax({
                                type: "get",
                                url: $.domainUrl + 'SendMsg?mobile=' + phone,
                                dataType: "json",
                                success: function (data) {
                                    var res = data;// $.parseJSON(data);
                                    if (res.Status == 1) {
                                        tips("验证码已发送", true, 2000, true, true);
                                        console.log(res.Data);
                                        sessionStorage.setItem('yzcode', res.Data);
                                    }
                                    else if (res.Status == -6) {
                                        tips("活动已结束", true, 5000, true, true);
                                    }
                                    else {
                                        tips("服务器有点忙，刷新重试", false, 0, false, true);
                                        $(".rightbutton").click(function () {
                                            location = location;
                                            $('#modaltips').hide();
                                        });
                                        return;
                                    }



                                },
                                error: function () {

                                    tips("服务器有点忙，刷新重试", false, 0, false, true);
                                    $(".rightbutton").click(function () {
                                        location = location;
                                        $('#modaltips').hide();
                                    });

                                }
                            })
                        }
                    } else {
                        //tips("请输入正确的手机号码");
                        tips("请输入正确的手机号码", true, 2000, true, true);
                    }
                });

                var isProcessing = false;
                $('#submit').click(function () {
                    if (isProcessing) {
                        return;
                    }
                    else {
                        tips("正在加载，请耐心等待", true, 10000, true, true);
                    }
                    var phone = $('input[name=telenum]').val();
                    var verifycode = $('input[name=verifycode]').val();
                    if (phone == '') {
                        tips("请输入手机号码", true, 2000, true, true);
                    } else {
                        if (isPhone(phone)) {
                            localStorage.setItem('tel', phone);
                            if (verifycode == '') {
                                tips("请输入验证码", true, 2000, true, true);
                            } else {
                                //判断验证码是否正确
                                var yzcode = sessionStorage.getItem('yzcode');
                                if (yzcode === null) { tips("验证码获取失败，请刷新重试", true, 2000, true, true); return; }
                                if (verifycode == yzcode) {
                                    //验证手机
                                    $.ajax({
                                        type: "get",
                                        url: $.domainUrl + 'VerificaMobile?mobile=' + phone,
                                        dataType: "json",
                                        success: function (data) {
                                            isProcessing = false;
                                            var res = data;// $.parseJSON(data);
                                            if (res.Status == 1) {
                                                //判断是否是新客
                                                $.ajax({
                                                    type: "get",
                                                    url: $.domainUrl + 'IsMember?mobile=' + phone,
                                                    dataType: "json",
                                                    success: function (data) {
                                                        var result = data;// $.parseJSON(data);
                                                        if (result.Status == 1) {
                                                            window.location.href = "list.html?mobile=" + phone;
                                                        } else if (result.Status == 0) {
                                                            $('.layout').fadeIn();
                                                        }
                                                        else if (result.Status == 2) {
                                                            tips("已经过参加活动", true, 5000, true, true);
                                                        }
                                                        else if (result.Status == -1) {
                                                            tips(result.Message, true, 5000, true, true);
                                                        }
                                                    }
                                                });

                                            } else {
                                                tips(res.Message, true, 2000, true, true);
                                                if (res.Status == 0) {
                                                    $('.layout').fadeIn(); //不满足条件
                                                }
                                            }

                                        },
                                        error: function () {
                                            isProcessing = false;
                                        }
                                    });
                                } else {
                                    isProcessing = false;
                                    tips("请输入正确的验证码", true, 2000, true, true);

                                }
                            }
                        } else {
                            tips("请输入正确的手机号码", true, 2000, true, true);
                        }
                    }
                })


                $('.layout').click(function (e) {
                    var even = e.target
                    if (even.classList.contains('layout')) {
                        $('.layout').fadeOut();
                    }
                })




                var total;
                var count = 0;
                var images = new Array();
                function preload() {
                    var len = preload.arguments.length;
                    for (var i = 0; i < len; i++) {
                        if (images[i] == "") { len = len - 1; return; };//容错处理
                        len = preload.arguments.length;
                        console.log(total);
                        images[i] = new Image();
                        images[i].onload = function () {
                            count++;
                            var scale = count / len;

                            if (count == len - 1) {
                                //$("#content").show();
                               // $('#modaltips').hide();
                                isFinished = true;
                            }
                        };
                        images[i].src = preload.arguments[i]
                    }
                }
                preload(
                    '../images/30.png',
                    '../images/bg.jpg',
                    '../images/bg1.jpg',
                    '../images/flower.png',
                    '../images/home.png',
                    '../images/list-bg.jpg',
                    '../images/list.png',
                    '../images/list1.png',
                    '../images/list2.png',
                    '../images/list3.png',
                    '../images/logo.png',
                    '../images/logo.jpg',
                    '../images/pic1.png',
                    '../images/share.png',
                    '../images/sign-check-icon.png'
                )



            } catch (ex) {
            }


        })
    </script>
</body>
</html>

