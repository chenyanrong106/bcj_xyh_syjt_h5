<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sq1.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.sq1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>基地审核</title>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script src="js/oa2.js"></script>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <%-- <link type="text/css" rel="stylesheet" href="css/meike.css" />--%>
    <link type="text/css" href="css/main.css?v=1" rel="stylesheet" />
</head>
<body class="bodybg">
    <form id="form1" runat="server">
        <div id="jz" style="display: none;">
            &nbsp;<br />
            <img src="images/jz.jpg" /><br />
            正在上传···<br />
            &nbsp;
        </div>

        <div class="acc-top-header">
            <ol class="width20" onclick="window.location='chou.aspx'"><span class="fanhui">返回</span></ol>
            <ol class="width60"><strong>基地审核</strong></ol>
            <ol class="width20"></ol>
        </div>

        <div class="acc-petchat">
            <div class="acc-login-input inputbg2">
                <h2>基本信息<span style="color: red; text-align: center; margin: 0 auto; padding-left: 10px;"><br />*如发起有遇到问题，请加客服微信anitamiao0916，进行人工帮助
                </span></h2>
                <ul style="padding: 0;">
                    <li>            
                        <p><strong>真实姓名</strong><span><input name="" type="text" id="name" class="txt" placeholder="填写发起人的真实姓名"></span></p>
                    </li>
                    <li>
                        <p><strong>身份证号</strong><span><input name="" type="text" id="cardno" class="txt" placeholder="填写发起人的真实身份证号码"></span></p>
                    </li>
                    <li>
                        <p><strong>联系电话</strong><span><input name="" type="text" id="phone" class="txt" placeholder="填写发起人的手机号码"></span></p>
                    </li>
                    <li>
                        <p><strong>基地名称</strong><span><input name="" type="text" id="jdname" class="txt" placeholder="填写发起人的基地名称"></span></p>
                    </li>
                    <li>
                        <p><strong>微信号码</strong><span><input name="" type="text" id="wxno" class="txt" placeholder="填写发起人的微信号码"></span></p>
                    </li>
                </ul>
            </div>
            <div class="acc-login-input baibg">
                <h2>收款人手持身份证照片</h2>
                <div class="acc-upload-box">
                    <div style="position: relative; width: 100%; display: block; overflow: hidden; padding: 10px 0;">
                        <ul id="imglist2" class="post_imglist"></ul>
                        <div class="upload_btn">
                            <input type="file" id="upload_image2" name="upload_image2" value="图片上传"   onchange="FileUpload_onselect(2)">
                        </div>
                    </div>
                </div>
                <%-- <p class="ptxt">微爱通道发起的动物保护项目，建议多张上传动物拍照，提高项目可信度</p>--%>

                <h2>基地照片上传</h2>
                <ul>
                    <div class="acc-upload-box">
                        <div style="position: relative; width: 100%; display: block; overflow: hidden; padding: 10px 0;">
                            <ul id="imglist1" class="post_imglist"></ul>
                            <div class="upload_btn">
                                <input type="file" id="upload_image1" name="upload_image1" value="图片上传"   onchange="FileUpload_onselect(1)">
                            </div>
                        </div>
                        <p class="upload-info">上传基地照片，提升审核进度,最多5张</p>
                    </div>

                </ul>
            </div>

            <div class="acc-login-input">
                <ul>
                    <li class="padd10">
                        <div class="submitBtn">下一步</div>
                    </li>
                     <li>
                    <p class="shuoming"><i class="checkbox checked"></i>已阅读并同意<a href="wd4.aspx" class="yelllow">《项目发起协议》</a></p>
                </li>
                </ul>
            </div>
              <span style="color: red; text-align: center; margin: 0 auto; padding-left: 10px;">*如发起有遇到问题，请加客服微信anitamiao0916，进行人工帮助
                </span>
            <%--  <div class="paybtn" style="height: 40px; line-height: 40px;">确认提交</div>--%>
    </form>
</body>
</html>

<%--<script type="text/javascript" src="js/localResizeIMG.js"></script>
<script type="text/javascript" src="js/mobileBUGFix.mini.js"></script>--%>
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
        var viewImg = $("#imglist" + id);
        if (viewImg.find("li").length > 4 && id == 1) {
            $.MsgBox.Alert("爱宠筹", "基地照片最多上传5张照片");
            //alert("最多上传5张照片");
        }
        else if (viewImg.find("li").length > 1 && id == 2) {
            $.MsgBox.Alert("爱宠筹", "身份证最多上传2张照片");
            //alert("最多上传5张照片");
        }
        else {
            $("#jz").show();

            $.ajaxFileUpload
            (
                {
                    url: 'sq1.aspx', //用于文件上传的服务器端请求地址
                    type: 'post',
                    data: { Id: '123', name: 'lunis' }, //此参数非常严谨，写错一个引号都不行
                    secureuri: false, //一般设置为false
                    fileElementId: 'upload_image' + id, //文件上传空间的id属性  <input type="file" id="file" name="file" />
                    dataType: 'text', //返回值类型 一般设置为json
                    success: function (data, status, Message)  //服务器成功响应处理函数
                    {
                        $("#jz").hide();
                        // alert(data);
                        data = JSON.parse(data);
                        viewImg.append('<li><span class="pic_time"><span class="p_img"></span><em>50%</em></span></li>');
                        viewImg.find("li:last-child").html('<span class="del"></span><img class="wh60" src="' + data.url + '"/><input type="hidden" id="'
                        + data.id
                        + '" name="fileup[]" value="'
                        + data.d + '">');

                        $(".del").on("click", function () {
                            $.post("sq1.aspx?para=delete&id=" + $(this).parent('li').find("input").attr("id"), {
                            },
                             function (ret) {
                                 //$(this).parent('li').remove();
                                 //$("#upload_image").show();
                             },
                             "html"
                       );
                            $(this).parent('li').remove();
                            $("#upload_image" + id).show();

                        });
                    },
                    error: function (data, status, e)//服务器响应失败处理函数
                    {
                        alert(e);
                    }
                }
            )
        }
        return false;
    }

    $(".submitBtn").click(function () {
        var pic1 = "";
        var pic2 = "";
        $("#imglist1 li").each(function () {
            pic1 += $(this).find("input").val() + "*";
        });
        $("#imglist2 li").each(function () {
            pic2 += $(this).find("input").val() + "*";
        });
        if ($("#name").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入姓名");
        }
        else if ($("#cardno").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入身份证");
        }
        else if ($("#phone").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入手机");
        }
        else if ($("#jdname").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入基地名称");
        }
        else if ($("#wxno").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入微信");
        }
        //else if (pic1 == "") {
        //    //alert("请上传至少一张照片");
        //    $.MsgBox.Alert("爱宠筹", "请上传身份证照片");
        //}
        //else if (pic2 == "") {
        //    //alert("请上传至少一张照片");
        //    $.MsgBox.Alert("爱宠筹", "请上传基地照片");
        //}
        else {
            $(".submitBtn").hide();
            $.post("sq1.aspx?para=tj", {
                name: $("#name").val(),
                phone: $("#phone").val(),
                cardno: $("#cardno").val(),
                jdname: $("#jdname").val(),
                wxno: $("#wxno").val(),
                pic1: pic1,
                pic2: pic2
            },
                            function (ret) {
                                $(".submitBtn").show();
                                if (ret.st == -1) {
                                    $.MsgBox.Alert("爱宠筹", ret.msg);
                                }
                                else {
                                    $.MsgBox.Alert("爱宠筹", ret.msg, function () { location = "sq2.aspx" });
                                }
                            },
                            "json"
                      );
        }
    });


</script>
<script type="text/javascript">
    //(function () {
    //    var viewImg = $("#imglist");
    //    var imgurl = '';
    //    var imgcount = 0;
    //    $('#upload_image').localResizeIMG({
    //        width: 480,
    //        quality: 0.8,
    //        success: function (result) {
    //            var status = true;
    //            if (result.height > 1600) {
    //                status = false;
    //                alert("照片最大高度不超过1600像素");
    //            }
    //            if (viewImg.find("li").length > 7) {
    //                status = false;
    //                alert("最多上传8张照片");
    //            }
    //            if (status) {
    //                viewImg.append('<li><span class="pic_time"><span class="p_img"></span><em>50%</em></span></li>');
    //                viewImg.find("li:last-child").html('<span class="del"></span><img class="wh60" src="' + result.base64 + '"/><input type="hidden" id="file'
    //                + imgcount
    //                + '" name="fileup[]" value="'
    //                + result.clearBase64 + '">');

    //                $(".del").on("click", function () {
    //                    $(this).parent('li').remove();
    //                    $("#upload_image").show();
    //                });
    //                imgcount++;
    //            }
    //        }
    //    });
    //})();


    //(function () {
    //    var viewImg = $("#imglist2");
    //    var imgurl = '';
    //    var imgcount = 0;
    //    $('#upload_image2').localResizeIMG({
    //        width: 480,
    //        quality: 0.8,
    //        success: function (result) {
    //            var status = true;
    //            if (result.height > 1600) {
    //                status = false;
    //                alert("照片最大高度不超过1600像素");
    //            }
    //            if (viewImg.find("li").length == 1) {
    //                status = false;
    //                alert("最多上传1张照片");
    //            }
    //            if (status) {
    //                viewImg.append('<li><span class="pic_time"><span class="p_img"></span><em>50%</em></span></li>');
    //                viewImg.find("li:last-child").html('<span class="del"></span><img class="wh60" src="' + result.base64 + '"/><input type="hidden" id="file'
    //                + imgcount
    //                + '" name="fileup[]" value="'
    //                + result.clearBase64 + '">');

    //                $(".del").on("click", function () {
    //                    $(this).parent('li').remove();
    //                    $("#upload_image2").show();
    //                });
    //                imgcount++;
    //            }
    //        }
    //    });
    //})();

</script>
