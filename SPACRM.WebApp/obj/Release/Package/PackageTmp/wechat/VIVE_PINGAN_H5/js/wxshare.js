$(function () {
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
                            // 用户确认分享后执行的回调函数
                            share(3);
                        },
                        cancel: function () {
                            alert("分享好友圈取消")
                            // 用户取消分享后执行的回调函数
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
                            // 用户确认分享后执行的回调函数
                            share(4);
                        },
                        cancel: function () {
                            // 用户取消分享后执行的回调函数
                        }
                    });
                    // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
                });
                wx.error(function (res) {
                   // alert(res);
                    // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。

                });
            }
        }

    });

    //分享 3-分享朋友圈 4-分享朋友
    function share(type)
    {
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
                var res = $.parseJSON(result);
                if (res.Status == 1) {
                    //res.Data
                }
            }
        });
    }

});