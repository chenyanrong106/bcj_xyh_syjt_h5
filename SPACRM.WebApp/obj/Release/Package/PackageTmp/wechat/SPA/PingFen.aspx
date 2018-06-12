﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PingFen.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.PingFen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="apple-mobile-web-app-title" content="小时光Massage">
    <!-- uc强制竖屏 -->
    <meta name="screen-orientation" content="portrait">
    <!-- UC强制全屏 -->
    <meta name="full-screen" content="yes">
    <!-- UC应用模式 -->
    <meta name="browsermode" content="application">
    <!-- QQ强制竖屏 -->
    <meta name="x5-orientation" content="portrait">
    <!-- QQ强制全屏 -->
    <meta name="x5-fullscreen" content="true">
    <!-- QQ应用模式 -->
    <meta name="x5-page-mode" content="app">
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/index.css" />
    <title>小时光Massage</title>  <script src="js/tz.js"></script>
    <style>
        a.pj {
            display: inline-block;
            margin-top: 27px;
            height: 24px;
            line-height: 24px;
            width: 100%;
            background: #ff5c67;
            border-radius: 5px;
            -webkit-border-radius: 5px;
            color: #FFFFFF;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!--begin header-->
        <div id="jz" style="display: none;">
            &nbsp;<br />
            <img src="image/jz.jpg" /><br />
            正在上传···<br />
            &nbsp;
        </div>
        <div class="meiheader">
            <div class="askboxcon">
                <a href="javascript:;" onclick="history.go(-1)"><span class="icon-left"></span></a>
                <h2>评价</h2>
                <a ><span >&nbsp;</span></a>
            </div>
        </div>
        <!--end header-->



        <!--begin content-->
        <div class="meiWapper">
            <div class="meiActive" style="display: none;">提交并分享，有机会获得10元奖励哦~</div>



            <div class="acc-upload-box">
                <p>
                    <textarea name="" cols="" rows="" class="dropdown-area" id="remark" placeholder="写评价（必填，10—500字）"></textarea>
                </p>
                <div style="position: relative; width: 100%; display: none; overflow: hidden; padding: 10px 0;">
                    <ul id="imglist1" class="post_imglist"></ul>
                    <div class="upload_btn">
                        <input type="file" id="upload_image1" name="upload_image1" value="图片上传" onchange="FileUpload_onselect(1)">
                    </div>
                </div>
            </div>




            <div class="xiaolianbox">

                <div class="pingjiaxing xiaolian" id="rank1">
                    <strong>总体：</strong>
                    <ul>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                    </ul>
                    <bdo id="score"></bdo>
                </div>

                <div class="pingjiaxing xiaolian" id="rank2">
                    <strong>专业：</strong>
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
                    <strong>服务：</strong>
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
                    <strong>环境：</strong>
                    <ul>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                        <li></li>
                    </ul>
                    <bdo id="score"></bdo>
                </div>
                <%if (order != null && order.PJID.IsNullOrZero() == 0)
                  { %>
                <div class="pingjiaTag">
                    <a class="pj" href="javascript:;" id="bookall">提交</a>
                </div>
                <%} %>
            </div>




        </div>
        <%if (order != null)
          {%>
        <input type="hidden" id="oid" value="<%=order.ID %>" />
        <%} %>
        <!--end content-->
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="dist/lrz.bundle.js"></script>
<script src="../../assets/js/ajaxfileupload.js"></script>
<script src="js/Message2.js"></script>
<script type="text/javascript">
    $("#jz").css({
        width: '100%', height: '100%', zIndex: '99999',
        filter: 'Alpha(opacity=60)', margin: 'auto', position: 'fixed', backgroundColor: 'gray', color: 'blue', textAlign: 'center', paddingTop: '150px', opacity: '0.6'
    });
    function FileUpload_onselect(id) {
        ajaxFileUpload(id);
    }
    //function FileUpload_onclick() {
    //    ajaxFileUpload();
    //}

    function ajaxFileUpload(id) {
        var viewImg = $("#imglist"+id);
        if (viewImg.find("li").length > 0) {
            $.MsgBox.Alert("系统提示", "最多上传1张照片");
            //alert("最多上传5张照片");
        }
        else {
            $("#jz").show();
            lrz(document.querySelector('#upload_image'+id).files[0], { quality: 1 }).then(function (rst) {
                rst.formData.append('fileLen', rst.fileLen);

                $.ajax({
                    url: '../Business.ashx?para=upimg', // 这个地址做了跨域处理，可以用于实际调试
                    data: rst.formData,
                    processData: false,
                    contentType: false,
                    type: 'POST',
                    success: function (data) {
                        $("#jz").hide();
                        // alert(data);
                        data = JSON.parse(data);
                        viewImg.append('<li><span class="pic_time"><span class="p_img"></span><em>50%</em></span></li>');
                        viewImg.find("li:last-child").html('<span class="del"></span><img class="wh60" src="' + data.url + '"/><input type="hidden" id="'
                        + data.id
                        + '" name="fileup[]" value="'
                        + data.d + '">');

                        $(".del").on("click", function () {
                            $.post("../Business.ashx?para=delete&id=" + $(this).parent('li').find("input").attr("id"), {
                            },
                             function (ret) {
                                 //$(this).parent('li').remove();
                                 //$("#upload_image").show();
                             },
                             "html"
                       );
                            $(this).parent('li').remove();
                            $("#upload_image"+id).show();

                        });
                    }
                });
            });

        }
        return false;
    }
</script>
<script type="text/javascript">
    //document.querySelector('#upload_image').addEventListener('change', function () {
    //    // this.files[0] 是用户选择的文件
    //    lrz(this.files[0], { width: 70, height: 70, quality: 0.6 })
    //        .then(function (rst) {
    //            // 把处理的好的图片给用户看看呗
    //            var img = new Image();
    //            img.src = rst.base64;

    //            img.onload = function () {
    //                $("#imglist").append('<li><span class="del icon-shut"></span><img class="wh60" src="' + img.src + '"/></li>');

    //                $(".del").on("click", function () {
    //                    $(this).parent('li').remove();

    //                });
    //            };
    //            return rst;
    //        })
    //})

    function pingjia(id) {
        var ping = ['不满意', '不满意', '一般', "满意", '满意'];
        $(function () {
            var pingCount = 0;
            $("#rank" + id + " ul li").mouseover(function () {
                $("#rank" + id + " ul li").addClass("active");//给所有的li都高亮
                $(this).nextAll("#rank" + id + " li").removeClass("active");  //当前li-->后的li高亮样式类名remove
                pingCount = $("#rank" + id + " ul").find("li.active").length; //获取高亮的li的个数
                $("#rank" + id + " bdo:eq(0)").show().html(ping[pingCount - 1]);
            }).click(function () {
                $("#rank" + id + " bdo:eq(0)").show().html(ping[pingCount - 1]);

            });
        })

    }
    pingjia(1);
    pingjia(2);
    pingjia(3);
    pingjia(4);

    $("#bookall").click(function () {
        if ($("#remark").val() == "") {
            alert("请留下评价吧！");
        }
        else if ($("#rank1 ul li.active").length == 0) {
            alert("请为‘总体’打个分吧！");
        } else if ($("#rank2 ul li.active").length == 0) {
            alert("请为‘专业’打个分吧！");
        } else if ($("#rank3 ul li.active").length == 0) {
            alert("请为‘服务’打个分吧！");
        } else if ($("#rank4 ul li.active").length == 0) {
            alert("请为‘环境’打个分吧！");
        }
        else {
            var pic1 = "";
            $("#imglist1 li").each(function () {
                pic1 += $(this).find("input").val() + "*";
            });
            $("#bookall").hide();
            $.post("pingfen.aspx?para=tj", {
                remark: $("#remark").val(),
                rank1: $("#rank1 ul li.active").length,
                rank2: $("#rank2 ul li.active").length,
                rank3: $("#rank3 ul li.active").length,
                rank4: $("#rank4 ul li.active").length,
                pic: pic1,
                oid: $("#oid").val()
            },
                            function (ret) {
                                $("#bookall").show();
                                if (ret.st == 2) {
                                    $.MsgBox.Alert("小时光", ret.msg);
                                }
                                else {
                                    $.MsgBox.Alert("小时光", ret.msg, function () { location = "wo.aspx" });
                                }
                            },
                            "json"
                      );
        }
    });
</script>
