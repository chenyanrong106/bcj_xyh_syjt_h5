<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sq4.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.sq4" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>申请</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/meike.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="jz" style="display: none;">
            &nbsp;<br />
            <img src="images/jz.jpg" /><br />
            正在上传···<br />
            &nbsp;
        </div>
        <div class="choukuan-box">
            <p><strong>目标</strong><input name="" id="mb" type="number" class="txt" style="width: 200px;" placeholder="请填写目标">千克/元</p>
        </div>

        <div class="choukuan-box">
            <ul>
                <li>
                    <input name="" type="text" id="bt" maxlength="50" class="txt2" placeholder="填写筹粮标题"></li>
                <li>
                    <input name="" type="text" id="jd" class="txt2" placeholder="填写基地名称"></li>
                <li>
                    <input name="" type="text" id="Text1" class="txt2" placeholder="填写联系人姓名"></li>
                <li>
                    <input name="" type="text" id="Text2" class="txt2" placeholder="填写联系人手机"></li>
                <li>
                    <input name="" type="text" id="Text3" class="txt2" placeholder="填写联系人微信"></li>
                <li style="border-bottom: none;">
                    <textarea name="" id="xq" cols="" rows="" maxlength="1500" placeholder="这里填写当前救助基地的真实情况，筹粮的目的，大概字数在300左右效果最好"></textarea>
                    <p class="ptxt">温馨提示：凡是填写不合格的地方，将不予审核通过！</p>
                </li>
                <li style="display: none;">请选择众筹方式：&nbsp;&nbsp;<input type="radio" name="jztype" value="1" checked="checked" />捐粮&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="jztype" value="2" />捐钱</li>
            </ul>
            <ul>
                <li><span class="uploadtxt">上传筹粮宠物照片</span>
                    <div style="position: relative; width: 100%; display: block; overflow: hidden; padding: 10px 0;">
                        <ul id="imglist" class="post_imglist"></ul>
                        <div class="upload_btn">
                            <input type="file" id="upload_image" name="upload_image" value="图片上传"  onchange="FileUpload_onselect()">
                        </div>
                    </div>
                </li>
                <li><span class="uploadtxt">上传基地Logo</span>
                    <div style="position: relative; width: 100%; display: block; overflow: hidden; padding: 10px 0;">
                        <ul id="imglist2" class="post_imglist"></ul>
                        <div class="upload_btn">
                            <input type="file" id="upload_image2" name="upload_image2" value="图片上传"   onchange="FileUpload_onselect2()">
                        </div>
                    </div>
                </li>
                <li>
                    <p class="ptxt">建议多张上传动物拍照，提高项目可信度</p>
                </li>
            </ul>
        </div>
        <div class="paybtn" style="height: 40px; line-height: 40px;">确认提交</div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<%--<script type="text/javascript" src="js/localResizeIMG.js"></script>
<script type="text/javascript" src="js/mobileBUGFix.mini.js"></script>--%>
<script src="../../assets/js/ajaxfileupload.js"></script>
<script src="js/Message2.js"></script>
<script type="text/javascript">
    $("#jz").css({
        width: '100%', height: '100%', zIndex: '99999',
        filter: 'Alpha(opacity=60)', margin: 'auto', position: 'fixed', backgroundColor: 'gray', color: 'blue', textAlign: 'center', paddingTop: '150px', opacity: '0.6'
    });
    function FileUpload_onselect() {
        ajaxFileUpload();
    }
    //function FileUpload_onclick() {
    //    ajaxFileUpload();
    //}

    function ajaxFileUpload() {
        var viewImg = $("#imglist");
        if (viewImg.find("li").length > 7) {
            $.MsgBox.Alert("爱宠筹", "基地照片最多上传8张照片");
            //alert("最多上传5张照片");
        }
        else {
            $("#jz").show();

            $.ajaxFileUpload
            (
                {
                    url: 'sq4.aspx', //用于文件上传的服务器端请求地址
                    type: 'post',
                    data: { Id: '123', name: 'lunis' }, //此参数非常严谨，写错一个引号都不行
                    secureuri: false, //一般设置为false
                    fileElementId: 'upload_image', //文件上传空间的id属性  <input type="file" id="file" name="file" />
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
                            $.post("sq2.aspx?para=delete&id=" + $(this).parent('li').find("input").attr("id"), {
                            },
                             function (ret) {
                                 //$(this).parent('li').remove();
                                 //$("#upload_image").show();
                             },
                             "html"
                       );
                            $(this).parent('li').remove();
                            $("#upload_image").show();

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


    function FileUpload_onselect2() {
        ajaxFileUpload2();
    }
    //function FileUpload_onclick2() {
    //    ajaxFileUpload2();
    //}

    function ajaxFileUpload2() {
        var viewImg = $("#imglist2");
        if (viewImg.find("li").length > 0) {
            $.MsgBox.Alert("爱宠筹", "基地照片最多上传1张照片");
            //alert("最多上传5张照片");
        }
        else {
            $("#jz").show();

            $.ajaxFileUpload
            (
                {
                    url: 'sq4.aspx', //用于文件上传的服务器端请求地址
                    type: 'post',
                    data: { Id: '123', name: 'lunis' }, //此参数非常严谨，写错一个引号都不行
                    secureuri: false, //一般设置为false
                    fileElementId: 'upload_image2', //文件上传空间的id属性  <input type="file" id="file" name="file" />
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
                            $.post("sq3.aspx?para=delete&id=" + $(this).parent('li').find("input").attr("id"), {
                            },
                             function (ret) {
                                 //$(this).parent('li').remove();
                                 //$("#upload_image").show();
                             },
                             "html"
                       );
                            $(this).parent('li').remove();
                            $("#upload_image2").show();

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

    $(".paybtn").click(function () {
        //alert($("input[name='jztype']:checked").val());
        var pic = "";
        $("#imglist li").each(function () {
            pic += $(this).find("input").val() + "*";
        });

        var pic2 = "";
        $("#imglist2 li").each(function () {
            pic2 += $(this).find("input").val() + "*";
        });

        if ($("#mb").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入目标");
        }
        else if ($("#bt").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入标题");
        }
        else if ($("#xq").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入详情");
        }
        else if ($("#jd").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入基地名称");
        }
        //else if (pic == "") {
        //    //alert("请上传至少一张照片");
        //    $.MsgBox.Alert("爱宠筹", "请上传照片");
        //}
        //else if (pic2 == "") {
        //    //alert("请上传至少一张照片");
        //    $.MsgBox.Alert("爱宠筹", "请上传基地logo");
        //}
        else if ($("#Text1").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请填写联系人姓名");
        }
        else if ($("#Text2").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请填写联系人手机");
        }
        else if ($("#Text3").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请填写联系人微信");
        }
        else
        {
            $(".paybtn").hide();
            $.post("sq4.aspx?para=tj", {
                mb: $("#mb").val(),
                bt: $("#bt").val(),
                xq: $("#xq").val(),
                jd: $("#jd").val(),
                jztype: $("input[name='jztype']:checked").val(),
                pic: pic,
                pic2: pic2,
                xm: $("#Text1").val(),
                sj: $("#Text2").val(),
                wx: $("#Text3").val()
            },
                            function (ret) {
                                $(".paybtn").show();
                                if (ret.st == -1) {
                                    $.MsgBox.Alert("爱宠筹", ret.msg);
                                }
                                else {
                                    $.MsgBox.Alert("爱宠筹", ret.msg, function () {
                                        window.location = window.location;
                                    });
                                }
                            },
                            "json"
                      );
        }
    });


</script>
