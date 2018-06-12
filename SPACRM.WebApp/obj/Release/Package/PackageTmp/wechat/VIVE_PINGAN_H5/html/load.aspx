<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="load.aspx.cs" Inherits="SPACRM.WebApp.wechat.VIVE_PINAN_H5.html.load" %>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="Keywords" content="" />
    <meta name="Description" content="" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <title>平安献礼30年</title>
</head>
<body>
    <div class="wrap load-page">
        <div class="load">
            <div class="loadimg" id="loading"></div>
            <p id="progress">0%</p>
            <div class="zhezhao">
                <div class="zhezhaoimg" id="mask"></div>
            </div>
        </div>
    </div>
    <input type="hidden" value="<%=Server.UrlEncode(AbsoluteUri)%>" id="url" />
    <script src="../js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
    <script src="../js/wxshare.js"></script>
    <script src="../js/requestparams.js"></script>
    <script src="../js/pv.record.js"></script>
    
    <script type="text/javascript">
        $(function () {

            //授权
          


            //记录客人来源
            function InitActivitySource() {
                 var source = 3;//参与活动来源 1-通过菜单访问   2-通过原文访问   3-通过单独活动链接访问 4-扫二维码
                var requestParameters = $.getQueryString("source");
                try {
                   
                    if (requestParameters != null && requestParameters != undefined && requestParameters == 'menu')
                        source = 1;
                    else if (requestParameters != null && requestParameters != undefined && requestParameters == 'text')
                        source = 2;
                    else if (requestParameters != null && requestParameters != undefined && requestParameters == '4')
                         source = 4;
                } catch (err) { }
                
                $.ajax({
                    url: $.domainUrl + "AddCustSource",
                    type: 'post',
                    data: {
                        "source": source
                    },
                    async: false,
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
            }

                 setTimeout(function () {
                     InitActivitySource();
            }, 100);
            



            var oBar = $(".zhezhao");
            var oSpan = $("#progress");
            var total;
            var count = 0;
            var images = new Array();
            function preload() {
                for (var i = 0; i < preload.arguments.length; i++) {
                    total = preload.arguments.length;
                    console.log(total);
                    images[i] = new Image();
                    images[i].onload = function () {
                        count++;
                        var scale = count / total;
                        oBar.css({ "height": (1.3 - scale) * 100 + "%" });
                        oSpan.html(parseInt(scale * 100) + "%");
                        if (count == total) {
                            oSpan.html("100%");
                            setTimeout(function () {
                                
                                window.location.href="index.aspx";
                            }, 500);
                        }
                    };
                    images[i].src = preload.arguments[i]
                }
            }
            preload('../images/xs-bg.jpg', '../images/gz_bg.jpg', '../images/jp-bg.jpg', '../images/jp-bg.jpg', '../images/card/smds.png', '../images/card/gjtd.png', '../images/card/pxxy.png', '../images/card/sbhjj.png', '../images/card/szds.png', '../images/card/poyi.png', '../images/card/xgss.png', '../images/card/paf.png')
        });
    </script>


</body>
</html>