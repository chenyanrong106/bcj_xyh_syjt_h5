﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PJDT.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.PJDT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>配捐配置</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/meike.css" />
    <%--<link rel='stylesheet' type='text/css' href='/assets/plugins/form-select2/select2.css' />--%>
    <style>
        select {
            /*Chrome和Firefox里面的边框是不一样的，所以复写了一下*/
            border: solid 1px #000;
            /*很关键：将默认的select选择框样式清除*/
            appearance: none;
            -moz-appearance: none;
            -webkit-appearance: none;
            /*在选择框的最右侧中间显示小箭头图片*/
            /*background: url("http://ourjs.github.io/static/2015/arrow.png") no-repeat scroll right center transparent;*/
            /*为下拉小箭头留出一点位置，避免被文字覆盖*/
            padding-right: 14px;
            width: 210px;
        }

            /*清除ie的默认选择框样式清除，隐藏下拉箭头*/
            select::-ms-expand {
                display: none;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="jz" style="display: none;">
            &nbsp;<br />
            <img src="images/jz.jpg" /><br />
            正在上传···<br />
            &nbsp;
        </div>
       
        <!--更新动态begion-->
        <h2 style="text-align: center;">为项目更新动态</h2>
        <div class="choukuan-box">
            <ul>
                <li>
                    <strong>选择项目</strong><select class="form-control" id="xmid2">
                        <%foreach (var j in jzlist)
                          {
                        %>
                        <optgroup label="<%=j.NickName %>">
                            <option value="<%=j.ID %>"><%=j.Title %></option>
                        </optgroup>
                        <%} %>
                    </select></li>
                <li style="border-bottom: none;">
                    <textarea name="" id="dtxq" cols="" rows="" placeholder="填写动态内容"></textarea>
                </li>
            </ul>
            <ul>
                <li><span class="uploadtxt">上传动态照片</span>
                    <div style="position: relative; width: 100%; display: block; overflow: hidden; padding: 10px 0;">
                        <ul id="imglist" class="post_imglist"></ul>
                        <div class="upload_btn">
                            <input type="file" id="upload_image" name="upload_image" value="图片上传"  onclick="FileUpload_onclick()" onchange="FileUpload_onselect()">
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <div class="paybtn" id="btndt" style="height: 40px; line-height: 40px;">确认提交</div>
        <!--更新动态end-->
         <br />
        <br />
        <br />
        <br />
        <hr />
        <br />
        <a href="pj.aspx" style="color:blue;">添加每日配捐额度</a>
        <br /> <br />
        <a href="pjdt.aspx" style="color:blue;">项目动态管理</a>
        <br /> <br />
        <a href="pjnot.aspx" style="color:blue;">添加不参与配捐的项目</a>
        <br /> <br />
        <a href="pjxmpj.aspx" style="color:blue;">为项目添加配捐</a>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="js/Message2.js"></script>
<script src="../../assets/js/ajaxfileupload.js"></script>
<%--<script type='text/javascript' src='assets/plugins/form-select2/select2.min.js'></script>--%>
<script type="text/javascript">
    $("#jz").css({
        width: '100%', height: '100%', zIndex: '99999',
        filter: 'Alpha(opacity=60)', margin: 'auto', position: 'fixed', backgroundColor: 'gray', color: 'blue', textAlign: 'center', paddingTop: '150px', opacity: '0.6'
    });
    function FileUpload_onselect() {
        ajaxFileUpload();
    }

    function ajaxFileUpload() {
        var viewImg = $("#imglist");
        if (viewImg.find("li").length > 7) {
            $.MsgBox.Alert("爱宠筹", "照片最多上传8张");
            //alert("最多上传5张照片");
        }
        else {
            $("#jz").show();

            $.ajaxFileUpload
            (
                {
                    url: 'sq3.aspx', //用于文件上传的服务器端请求地址
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


  

    $("#btndt").click(function () {
        var pic = "";
        $("#imglist li").each(function () {
            pic += $(this).find("input").val() + "*";
        });
        if ($("#xmid2").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入项目ID");
        }
        else if ($("#dtxq").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入动态");
        }
        else if (pic == "") {
            //alert("请上传至少一张照片");
            $.MsgBox.Alert("爱宠筹", "请上传照片");
        }
        else {
            $("#btndt").hide();
            $.post("PJ.aspx?para=dt", {
                xmid: $("#xmid2").val(),
                dtxq: $("#dtxq").val(),
                pic: pic
            },
                            function (ret) {
                                $("#btndt").show();
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
