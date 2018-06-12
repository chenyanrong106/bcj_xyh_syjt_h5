<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="building.aspx.cs" Inherits="SPACRM.WebApp.wechat.VIVE_PINAN_H5.html.building" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="Keywords" content="" />
    <meta name="Description" content="" />
    <link rel="stylesheet" type="text/css" href="../css/building.css?v=1.0" />
    <title>平安献礼30年</title>
    <style>
        .btn-audio { position: absolute; top: 0; left: 0; z-index: 99999; }
    </style>
</head>
<body>
    <div class="page_building">
        <div class="building_box">
            <div class="building_img">
                <div class="box_b">
                </div>
                <div class="box_y">
                </div>
            </div>
            <div class="buliding_cnt">
                <!-- 排行榜 -->
                <div class="phb">
                    <img src="../images/logo1.png" alt="" class="logo">
                    <img src="../images/phb.png" alt="" class="phb_btn">
                </div>
                <!-- 优惠and平安 -->
                <div class="youhui">
                    <a href="https://elis-ecocdn.pingan.com.cn/elis_smp_als_dmz/activity/items/index.html?activityId=20180425353310">
                        <img src="../images/youhuibtn.png" alt="">
                    </a>
                </div>
                <div class="pingan">
                    <img src="../images/pinganbtn.png" alt="">
                </div>
                <!-- 点亮层数 -->
                <div class="number_box" style="display: block;">
                    <p>
                        点<br>
                        亮<br>
                        平<br>
                        安<br>
                        即<br>
                        可<br>
                        抽<br>
                        奖<br>
                    </p>
                </div>
                <!-- 倒计时 -->
                <div class="time_box">
                    <div class="time">
                        <div class="line"></div>
                    </div>
                    <span><em class="time_number">10</em>秒</span>
                </div>
            </div>
            <div class="btn-audio">
                <audio id="mp3Btn" preload="auto" src="https://daoket.github.io/img/load.mp3">
                </audio>
                <%-- <audio media-player="audioPlayer" autoplay controls="controls" preload="auto" id="mp3Btn"
                crossOrigin="anonymous" src="https://daoket.github.io/img/load.mp3"></audio>--%>
            </div>
        </div>
        <!-- layout -->
        <div class="layout">
            <!-- 摇一摇 -->
            <div class="yaoyiyao">
                <div class="box">
                    <img src="../images/yaoyiyao.png" alt="" class="">
                    <h2>摇一摇拼手速</h2>
                    <p>
                        在规定时间内摇动频率越快<br>
                        楼层点亮越多
                    </p>
                </div>
            </div>
            <!-- 点亮层数 -->
            <div class="result_box">
                <div class="box">
                    <h2>点亮平安大楼层数</h2>
                    <em class="number">88</em>
                    <a href="./building.html">
                        <img src="../images/again.png" alt="">
                    </a>
                    <a href="./share.html">
                        <img src="../images/other.png" alt="">
                    </a>
                    <p>历史最高楼层数：<em class="history">76</em></p>
                </div>
            </div>
            <!-- 分享延时 -->
            <div class="share_box">
                <div class="box_s">
                    <div class="icon">
                        <img src="../images/share.png" alt="">
                    </div>
                    <div class="text">
                        &nbsp;&nbsp;速速传播出去，<br>
                        &nbsp;&nbsp;可「<em>延长5秒</em>」<br>
                        &nbsp;&nbsp;点亮大楼时间！
                    </div>
                </div>
            </div>
            <!-- 排行榜 -->
            <div class="paihb_box">
                <div class="box">
                    <dl class="title clearfix">
                        <dt>排行</dt>
                        <dt>头像</dt>
                        <dt>昵称</dt>
                        <dt>最高点亮楼层</dt>
                    </dl>
                    <div class="list_box" id="rankData">
                    </div>
                    <div class="back">
                        <img src="../images/back.png" alt="">
                    </div>
                </div>
            </div>
        </div>
        <!-- success -->
        <div class="success">
            <div class="box">
                <h2>&nbsp;&nbsp;成功点亮平安大楼！</h2>
                <a href="javascript:void(0)">
                    <img src="../images/zlyc.png" alt="">
                </a>
            </div>
        </div>
            <div class="pdName"></div>
    </div>
    <input type="hidden" id="MaxFloorNum" value="0" />
    <input type="hidden" id="CurFloorNum" value="0" />
    <script src="../js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
    <script>
        //自动播放
        window.onload = function () {

            // 排行榜 显示or隐藏
            var layout = $('.layout');
            $('.phb_btn').click(function () {
                layout.fadeIn();
                layout.children('.paihb_box').fadeIn();
            });
            $('.back').click(function () {
                layout.children('.paihb_box').fadeOut();
                layout.fadeOut();
            });

            //单独设置分享
            $.ajax({
                url: '../../Business.ashx?para=jsapi&apiurl=' + encodeURIComponent(location.href),
                type: 'POST',
                data: {},
                dataType: 'html',
                timeout: 1000,
                error: function (e) { },
                success: function (result) {
                    result = JSON.parse(result);
                    console.log(result);
                    if (result.state == 0) {
                        wx.config({
                            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                            appId: result.appId, // 必填，公众号的唯一标识
                            timestamp: result.timestamp, // 必填，生成签名的时间戳
                            nonceStr: result.nonceStr, // 必填，生成签名的随机串
                            signature: result.signature,// 必填，签名，见附录1
                            jsApiList: ["onMenuShareTimeline", "onMenuShareAppMessage", "onMenuShareQQ", "onMenuShareWeibo"] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
                        });
                        wx.ready(function () {
                            // alert("ready");
                            //wx.hideMenuItems({
                            //    menuList: ["share:qq", "weiboApp", "share:facebook", "share:QZone", "copyUrl", "originPage", "openWithQQBrowser", "openWithSafari", "email"] // 要隐藏的菜单项，只能隐藏“传播类”和“保护类”按钮，所有menu项见附录3
                            //});
                            wx.onMenuShareTimeline({ //分享朋友圈
                                title: "平安献礼30年，缤纷礼品超出想象！", // 分享标题
                                desc: "金条、银条……超多大礼等你拿！",
                                link: $.ActivityUrl + 'html/load.aspx', // 分享链接
                                imgUrl: result.imgUrl, // 分享图标
                                success: function () {
                                    share(3);
                                    //跳转到指定页面
                                    if ($("#CurFloorNum").val()>0) {
                                        window.location.href = 'building-share.html?cur_floor_num=' + $("#CurFloorNum").val() + "&max_floor_num=" + $("#MaxFloorNum").val();
                                    }
                                    //window.location.href = $.ActivityUrl + 'html/building-share.html?cur_floor_num=90&max_floor_num=110';
                                },
                                cancel: function () {
                                }
                            });
                            wx.onMenuShareAppMessage({ //分享给朋友
                                title: "平安献礼30年，缤纷礼品超出想象！", // 分享标题
                                desc: "金条、银条……超多大礼等你拿！",
                                link: $.ActivityUrl + 'html/load.aspx', // 分享链接
                                imgUrl: result.imgUrl, // 分享图标
                                type: '', // 分享类型,music、video或link，不填默认为link
                                dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                                success: function () {
                                    share(4);
                                    //跳转到指定页面
                                    if ($("#CurFloorNum").val()>0) {
                                        window.location.href = 'building-share.html?cur_floor_num=' + $("#CurFloorNum").val() + "&max_floor_num=" + $("#MaxFloorNum").val();
                                    }
                                    //window.location.href = $.ActivityUrl + 'html/building-share.html?cur_floor_num=90&max_floor_num=110';
                                },
                                cancel: function () {
                                }
                            });
                            // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
                        });
                        wx.error(function (res) {
                            //alert(res);
                            // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。

                        });
                    }
                }

            });


            setTimeout(function () {
                //获得抽奖资格
                $.ajax({
                    url: $.domainUrl + "GetLotteryOpportunity",
                    type: 'get',
                    data: {},
                    dataType: 'html',
                    timeout: 1000,
                    error: function (e) { },
                    success: function (result) {
                        var res = $.parseJSON(result);
                        if (res.Status == 1) {
                            //res.Data
                        } else if (res.Status == -5) {
                            window.location.href = "index.aspx";
                        }
                    }
                });
            }, 3000);

            //排行榜 数据
            function QueryRank() {
                $.ajax({
                    url: $.domainUrl + "QueryRank",
                    type: 'get',
                    data: {},
                    dataType: 'html',
                    timeout: 1000,
                    error: function (e) { },
                    success: function (result) {
                        var res = $.parseJSON(result);
                        if (res.Status == 1) {
                            var list = res.Data;
                            $("#rankData").html("");
                            $.each(list, function (i, e) {
                                //debugger
                                var html = '<dl class="clearfix">\
                                                        <dt><em class="id">'+ e.rowNumber + '</em></dt>\
                                                            <dt><img src="'+ e.headimgurl + '" alt=""></dt>\
                                                            <dt><em class="name">'+ e.nickName + '</em></dt>\
                                                            <dt><em class="number">'+ e.floorNum + '</em></dt>\
                                                    </dl>';
                                $("#rankData").append(html);
                            });
                        }
                    }
                });
            }
            setTimeout(function () {
                QueryRank();
            }, 2000);



            setTimeout(function () {
                //查询历史最高
                $.ajax({
                    url: $.domainUrl + "GetMaxFloorNum",
                    type: 'get',
                    data: {},
                    dataType: 'html',
                    timeout: 1000,
                    error: function (e) { },
                    success: function (result) {
                        var res = $.parseJSON(result);
                        if (res.Status == 1) {
                            var MaxFloorNum = res.Data;
                            $("#MaxFloorNum").val(MaxFloorNum);
                        } else if (res.Status == -5) {
                            window.location.href = "index.aspx";
                        }
                    }
                });
            }, 5000);

            //toast
            var toast = function () {
                $('.pdName').css({
                    'display': 'block'
                })
                timer = setTimeout(function () {
                    $('.pdName').css({
                        'display': 'none'
                    })
                }, 5000);
                return false;
            };
            
            var isProcessing = false;
            //抽奖后页面跳转
            $(".success a").click(function () {
                if (isProcessing) {
                    return;
                }
                else {
                    $(".success").hide();
                    $('.pdName').html("奖品发放中，请稍候...");
                    toast();
                }
                //今日抽奖次数已用完，明天再来试试吧！
                var url = $.domainUrl + "Lottery";
                $.get(url, function (result) {
                    isProcessing = false;
                    var json = $.parseJSON(result);
                    //成功
                    if (json.Status == "1") {
                        var data = json.Data;

                        //实物
                        if (data.type == "1") {
                            location.href = "prize.html?prizename=" + data.prize_name + "&id=" + data.id + "&prizecode=" + data.prize_code;
                        }
                        //券
                        else if (data.type == "2") {
                            location.href = "yhq.html?prizename=" + data.prize_name + "&id=" + data.id + "&prizecode=" + data.prize_code;
                        }
                        //卡片
                        else if (data.type == "3") {
                            location.href = "card.html?prizename=" + data.prize_name + "&id=" + data.id + "&prizecode=" + data.prize_code + "&imgsrc=" + data.img_src;
                        }
                    } else if (json.Status == -3) {
                        //alert("今日抽奖次数已用完，明天再来试试吧！");
                        $('.pdName').html("今日抽奖次数已用完，<br/>明天再来试试吧！");
                        toast();
                    }
                });
            });



            function audioAutoPlay(id) {
                var audio = document.getElementById(id);
                audio.play();
                document.addEventListener("WeixinJSBridgeReady", function () {
                    audio.play();
                }, false);
            }
            // audioAutoPlay('mp3Btn')



            var StartGame = false;

            //点亮平安
            $('.pingan').click(function () {
               
                 $('.number_box p').html('');
                $('.yaoyiyao img').addClass('hand_move');
                // 判断是否支持摇一摇
                if (window.DeviceMotionEvent) {
                    //判断倒计时
                    var time = parseInt($('.time_number').html());
                    if (time > 1) {
                        var audio = document.getElementById('mp3Btn');
                        audioAutoPlay('mp3Btn');
                        audio.setAttribute('src', '../audio.mp3');
                        audio.load();
                        layout.fadeIn();
                        layout.children('.yaoyiyao').fadeIn();
                        $('.time_box').fadeIn();
                        //监听摇一摇运动事件(可以类比click事件，每摇一次触发一次事件，调用回掉函数);
                        window.addEventListener('devicemotion', deviceMotionHandler, false);
                    } else {
                        layout.fadeIn();
                        layout.children('.share_box').fadeIn();
                    }
                } else {
                    layout.children('.yaoyiyao .box').html('您的手机不支持摇一摇功能！');
                }
                // 摇一摇
                var SHAKE_THRESHOLD = 3000;
                var last_update = 0;
                var x = y = z = last_x = last_y = last_z = 0;
                var num = 0;
                var num2 = 0;//点亮的楼层数;
                var first = true;
                var isprint = false;
                var isstart = true;
                function deviceMotionHandler(eventData) {
                    var a = parseInt($('.time_number').html());//10s倒计时
                    var b = a;
                    var timer;
                    //捕捉重力加速度
                    var acceleration = eventData.accelerationIncludingGravity;
                    var curTime = new Date().getTime();
                    if ((curTime - last_update) > 100) {
                        var diffTime = curTime - last_update;
                        last_update = curTime;
                        x = acceleration.x;
                        y = acceleration.y;
                        z = acceleration.z;
                        var speed = Math.abs(x + y + z - last_x - last_y - last_z) / diffTime * 10000;
                        //获取加速度差值；
                        var x1 = Math.abs(x - last_x);
                        var y1 = Math.abs(y - last_y);
                        var z1 = Math.abs(z - last_z);
                        var max = 0;
                        if (x1 > y1) {
                            if (x1 > z1) {
                                max = x1;
                            } else {
                                max = z1;
                            }
                        } else {
                            if (y1 > z1) {
                                max = y1;
                            } else {
                                max = z1;
                            }
                        }
                        //晃动幅度大于40
                        if (max > 40) {
                            if (isstart) {
                                var audio = document.getElementById('mp3Btn');
                                audioAutoPlay('mp3Btn');
                                audio.setAttribute('src', '../audio.mp3');
                                audio.load();
                                isstart = false;
                            }
                            StartGame = true;

                            isprint = true;
                            num += 2;
                            num2 = num;
                            if (first) {
                                //begin摇一摇时
                                layout.children('.yaoyiyao').fadeOut();
                                layout.fadeOut();
                                $('.youhui').fadeOut();
                                $('.pingan').fadeOut();
                                timer = setInterval(function () {
                                    a = a - 1;//倒计时的值
                                    var w = a / b * 100 + '%';//b=10;
                                    $('.time .line').css('width', w);
                                    if (a == 0) {
                                        $('.btn-audio').remove();
                                        clearInterval(timer);
                                        $('.time_box span').html('<em class="time_number">结束</em>');
                                        $('.youhui').fadeIn();
                                        // 计算高度
                                        if (num2 >= 118) {
                                            num2 = 118;
                                            $('.success').delay(2000).fadeIn();
                                            $('.number_box').html('<p>点<br>亮<br>平<br>安<br>全<br>部 <em class="number">' + num2 + '</em>层</p>')
                                        } else {
                                            $('.number_box').html('<p>点<br>亮<br>平<br>安 <em class="number breathzi">' + num2 + '</em>层</p>')
                                            $('.pingan').fadeIn();
                                            $('.result_box .number').html(num2);
                                        }
                                        $("#CurFloorNum").val(num2);
                                        $.ajax({
                                            url: $.domainUrl + "SaveLightFloorRecord",
                                            type: 'post',
                                            data: {
                                                "floor_num": num2,
                                                "spend_time": 10
                                            },
                                            dataType: 'html',
                                            timeout: 1000,
                                            error: function (e) { },
                                            success: function (result) {

                                            }
                                        });

                                        //判断 如果num2>maxFloor 则重新刷新排行榜
                                        if (num2 > parseInt($("#MaxFloorNum").val())) {
                                            QueryRank();
                                            $("#MaxFloorNum").val(num2);
                                        }
                                        $('.number_box').fadeIn();
                                        var he = num2 / 118 * 100 + '%';
                                        $('.building_box .box_y').css('height', he);
                                        $('.pingan img').attr('src', '../images/jixubtn.png').addClass('breathpic');
                                        isprint = false;
                                        num = 0;
                                    } else {
                                        $('.time_number').html(a);
                                        audioAutoPlay('mp3Btn');
                                    }
                                }, 1000);
                                first = false;
                            }
                        }
                        last_x = x;
                        last_y = y;
                        last_z = z;
                    }
                }
            });
            //点击空白隐藏
            layout.click(function (e) {
                if (e.target.className == 'yaoyiyao' || e.target.className == 'result_box' || e.target.className == 'box_s') {
                    layout.children('.yaoyiyao').fadeOut();
                    layout.children('.result_box').fadeOut();
                    layout.children('.share_box').fadeOut();
                    layout.fadeOut()
                }
            })



            //分享 3-分享朋友圈 4-分享朋友
            function share(type) {
                $.ajax({
                    url: $.domainUrl + "WXShare",
                    type: 'get',
                    data: {
                        "type": type
                    },
                    dataType: 'html',
                    timeout: 1000,
                    error: function (e) { },
                    success: function (result) {
                    }
                });
            }
        };


    </script>
</body>
</html>
