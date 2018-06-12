﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SPACRM.WebApp.wechat.XYH_Coupon_H5.html.Home" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="Keywords" content="" />
    <meta name="Description" content="" />
  <%--  <link rel="stylesheet" type="text/css" href="../assets/hmjweixin/css/new_style.css" />
    <link rel="stylesheet" type="text/css" href="../assets/hmjweixin/css/common.css" />
    <link rel="stylesheet" href="../assets/hmjweixin/css/flop.css">--%>
    <style>
        .login {
            margin-top: 9.142857142857143rem;
        }

        li {
            position: relative;
        }

        input {
            position: absolute;
            z-index: 999;
            top: 1.4285714285714286rem;
        }

        b {
            position: absolute;
            top: 1.4285714285714286rem;
            color: #ccc;
            height: 1.1428571428571428rem;
            line-height: 1.1428571428571428rem;
            z-index: 10;
        }

        #timeid {
            margin-top: 1rem;
        }
    </style>
    <title>华美家</title>
</head>
<body>
    <div class="wrap">
        <div class="header">
            <img src="../assets/hmjweixin/images/logo.png" alt="">
        </div>
        <div class="form">
            <ul>
                <li>
                    <label for="telenum">
                        <img src="../assets/hmjweixin/images/telicon.png" alt="" />
                    </label>
                    <b>请输入手机号</b>
                    <input type="tel" maxlength="11" id="telenum" name="telenum" onkeyup="judgeInput()"  />
                </li>
                <li><label for="telenum"><img src="../assets/hmjweixin/images/Verify.png" alt="" /></label>
                    <b>请输入验证码</b>
                <input type="tel" maxlength="6" name="verifycode"  onkeyup="judgeInput()" style="width:60%;" /> </li>
               <li> <button id="timeid" value="发送验证码">发送验证码</button></li>
            </ul>
        </div>
        <div class="btn">
            <button class="binding login" id="btnRegister">注册</button>
        </div>
        <div class="bottom">
            <img src="../assets/hmjweixin/images/bottomlogo.png" alt="" />
        </div>
        <div class="layer modaltips" id="modaltips"></div>
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
                $(document).on('focus', 'input', function () {
                    $(this).siblings('b').animate({ top: '4px' }).css('fontSize', '12px');
                    //$(this).parent('li').css("borderColor", "#333333");
                    $(this).parent('li').css("borderColor", "#333333").siblings('li').css("borderColor", "#cccccc");
                });
                $('input[name=telenum],input[name=verifycode]').blur(function () {
                    if ($(this).val().trim().length <= 0) {
                        $(this).siblings('b').animate({ top: '20px' }).css("fontSize", '16px');
                        $(this).parent('li').css("borderColor", "#cccccc")
                    }
                });

                var fansJson = '@Html.Raw(ViewBag.FansInfo)';

                localStorage.setItem("fansJson", fansJson);
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

                                    if (data.data === undefined) {
                                        tips("服务器有点忙，刷新重试", false, 0, false, true); 
                                    $(".rightbutton").click(function () {
                                        location = location;
                                            $('#modaltips').hide();
                                        });
                                        return;
                                    };

                                    tips("验证码已发送", true, 2000, true, true);
                                    console.log(data.data);
                                    sessionStorage.setItem('yzcode', data.data);
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
                $('#btnRegister').click(function () {
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
                                    window.location.href = "../assets/hmjweixin/html/agree.html?t=" + new Date().getTime();
                                } else {
                                    tips("请输入正确的验证码", true, 2000, true, true);
                                    
                                }
                            }
                        } else {
                            tips("请输入正确的手机号码", true, 2000, true, true);
                        }
                    }
                })
                var login = $('.login');
                var winHeight = $(window).height();   //获取当前页面高度
                $(window).resize(function () {
                    var thisHeight = $(this).height();
                    if (winHeight - thisHeight > 50) {
                        //当软键盘弹出，在这里面操作
                        login.css('opacity', '0')
                    } else {
                        //当软键盘收起，在此处操作
                        login.css('opacity', '1')
                    }
                });




            } catch (ex) {
                AddLog("ToZhuCe", "Exception", ex.toString());
            }

            function AddLog(title, msgType, msgContent) {

                var data = '{ "title": " ' + title + '", "msgType": "' + msgType
                    + '", "msgContent": "' + msgContent.replace(/"/g, '\'') + '"}';

                $.ajax({
                    type: "post",
                    contentType: "application/json",
                    cache: false,
                    dataType: "json",
                    url: $.domainUrl + 'User/AddLog',
                    data: data,
                    success: function (data) {

                    },
                    error: function () {
                    }
                });
            }

        })

        function judgeInput() {

            var phone = $.trim($('input[name=telenum]').val());
            var verifycode = $.trim($('input[name=verifycode]').val());
            if (phone.length == 11 && verifycode.length == 6) {
                $("#btnRegister").removeAttr("class");
                $("#btnRegister").attr("class", "register login");
            } else {
                $("#btnRegister").removeAttr("class");
                $("#btnRegister").attr("class", "binding");
            }
        }
    </script>
</body>
</html>

