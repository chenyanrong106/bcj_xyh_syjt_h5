<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /> 
<meta content="yes" name="apple-mobile-web-app-capable" /> 
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
<meta name="apple-mobile-web-app-title" content="小时光Massage"><!-- uc强制竖屏 -->
<meta name="screen-orientation" content="portrait"><!-- UC强制全屏 -->
<meta name="full-screen" content="yes"><!-- UC应用模式 -->
<meta name="browsermode" content="application"><!-- QQ强制竖屏 -->
<meta name="x5-orientation" content="portrait"><!-- QQ强制全屏 -->
<meta name="x5-fullscreen" content="true"><!-- QQ应用模式 -->
<meta name="x5-page-mode" content="app">
<meta content="telephone=no" name="format-detection" />
<link type="text/css" rel="stylesheet" href="css/index.css"/>
<title>小时光Massage</title>  <script src="js/tz.js"></script>
</head>
<body>
    <form id="form1" runat="server">
  <!--begin header-->
<div class="meiheader">
  <div class="askboxcon">
    <a  href="javascript:;" onclick="history.go(-1)"><span class="icon-txt">客服</span></a>
    <h2>我的</h2> 
    <a href="wechat.aspx"><span class="icon-feedback"></span></a>   
    </div>
</div>
<!--end header-->



<!--begin content-->
<div class="meiWapper">
  

  
  
  
  
</div>

<!--end content-->
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="dist/lrz.bundle.js"></script>
<script type="text/javascript">
    document.querySelector('#upload_image').addEventListener('change', function () {
        // this.files[0] 是用户选择的文件
        lrz(this.files[0], { width: 70, height: 70, quality: 0.6 })
            .then(function (rst) {
                // 把处理的好的图片给用户看看呗
                var img = new Image();
                img.src = rst.base64;

                img.onload = function () {
                    $("#imglist").append('<li><span class="del"></span><img class="wh60" src="' + img.src + '"/></li>');

                    $(".del").on("click", function () {
                        $(this).parent('li').remove();

                    });
                };
                return rst;
            })
    })

    function pingjia(id) {
        var ping = ['不满意', '不满意', '一般', "满意", '满意'];
        $(function () {
            var pingCount = 0;
            $("#rank" + id + " ul li").mouseover(function () {
                $("#rank" + id + " ul li").addClass("active");//给所有的li都高亮
                $(this).nextAll("#rank" + id + " li").removeClass("active");  //当前li-->后的li高亮样式类名remove
                pingCount = $("#rank" + id + " ul").find("li.active").length; //获取高亮的li的个数
                $("#rank" + id + " bdo:eq(0)").show().aspx(ping[pingCount - 1]);
            }).click(function () {
                $("#rank" + id + " bdo:eq(0)").show().aspx(ping[pingCount - 1]);

            });
        })

    }

    pingjia(2);
    pingjia(3);
    pingjia(4);	
</script>