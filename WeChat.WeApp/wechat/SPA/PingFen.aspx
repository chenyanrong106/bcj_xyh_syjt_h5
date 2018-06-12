<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PingFen.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.PingFen" %>

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
<title>小时光Massage</title>
</head>
<body>
    <form id="form1" runat="server">
  <!--begin header-->
<div class="meiheader">
  <div class="askboxcon">
    <a href="wechat.aspx"><span class="icon-left"></span></a>
    <h2>评价</h2> 
    <a href="wechat.aspx"><span class="icon-lnk"></span></a>   
    </div>
</div>
<!--end header-->



<!--begin content-->
<div class="meiWapper">
   <div class="meiActive">提交并分享，有机会获得10元奖励哦~</div>
   
   
   
   <div class="acc-upload-box">
     <p><textarea name="" cols="" rows="" class="dropdown-area" placeholder="写评价（必填，10—500字）"></textarea></p>
     <div style="position:relative; width:100%; display:block; overflow:hidden; padding:10px 0;"><ul id="imglist" class="post_imglist"></ul>
      <div class="upload_btn"><input type="file" id="upload_image" value="图片上传" accept="image/jpeg,image/gif,image/png" capture="camera"></div>
      </div>
    </div>
    
    
   
  
  <div class="xiaolianbox">
  <div class="pingjiaxing xiaolian" id="rank2">
  <strong>专业技能：</strong>
  <ul>
    <li></li>
    <li></li>
    <li></li>
    <li></li>
    <li></li>
  </ul>
  <bdo id="score"></bdo>
  </div>
  
  <div class="pingjiaxing xiaolian" id="rank3">
  <strong>服务态度：</strong>
  <ul>
    <li></li>
    <li></li>
    <li></li>
    <li></li>
    <li></li>
  </ul>
  <bdo id="score"></bdo>
  </div>
  
  
   <div class="pingjiaxing xiaolian" id="rank4">
  <strong>洗发体验：</strong>
  <ul>
    <li></li>
    <li></li>
    <li></li>
    <li></li>
    <li></li>
  </ul>
  <bdo id="score"></bdo>
  </div>

   <div class="pingjiaTag">
      <span>态度一般</span><span>技术一般</span><span>服务一般</span> <a href="#"><span class="addbtn icon-add-item">添加新印象</span></a>
   </div>
  </div>
  
  
  
  
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
                    $("#imglist").append('<li><span class="del icon-shut"></span><img class="wh60" src="' + img.src + '"/></li>');

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