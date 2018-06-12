<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNews.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.AddNews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /> 
<meta content="yes" name="apple-mobile-web-app-capable" /> 
<meta content="black" name="apple-mobile-web-app-status-bar-style" /> 
<meta content="telephone=no" name="format-detection" />
<link type="text/css" href="css/main.css" rel="stylesheet" />
<title>更新动态</title>
</head>
<body class="bodybg">
    <form id="form1" runat="server">
        <%if(jz!=null){ %>
        <h1 style="text-align:center;"><%=jz.NickName %></h1>
        <%} %>
   <div class="acc-login-input baibg">
    <ul style=" padding:0;margin-bottom:10px;">
          <li><p><textarea name="" id="dtxq"  cols="" rows="" class="dropdown-area" placeholder="填写您要更新的内容"></textarea></p></li>
          <li><p class="faqi-info">温馨提示：不超过120个字</p></li>
       
    </ul>
</div>


<div class="acc-login-input baibg">
    <div class="acc-upload-box">
     <div style="position:relative; width:100%; display:block; overflow:hidden; padding:10px 0;">
        <ul id="imglist" class="post_imglist"></ul>
                        <div class="upload_btn">
                            <input type="file" id="upload_image" name="upload_image" value="图片上传"  onclick="FileUpload_onclick()" onchange="FileUpload_onselect()">
                        </div>
      </div>
    
    <p class="upload-info">请勿在更新内容中上传支付二维码或引导用户发微信、支付宝红包、私人账号汇款等信息，一经发现，将严肃处理。</p>
    </div>
</div>


<div class="acc-login-input">
    <ul>
       <li class="padd10"><div class="submitBtn">更新</div></li>
    </ul>
</div>
        <input type="hidden" id="oid" value="<%=Request.QueryString["id"] %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="js/oa2.js"></script>
<script type="text/javascript" src="js/localResizeIMG.js"></script>
<script type="text/javascript" src="js/mobileBUGFix.mini.js"></script>
<script src="js/Message2.js"></script>
<script src="../../assets/js/ajaxfileupload.js"></script>
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



    $(".submitBtn").click(function () {
        var pic = "";
        $("#imglist li").each(function () {
            pic += $(this).find("input").val() + "*";
        });
        if ($("#oid").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "无项目ID");
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
            $(".submitBtn").hide();
            $.post("PJ.aspx?para=dt", {
                xmid: $("#oid").val(),
                dtxq: $("#dtxq").val(),
                pic: pic
            },
                            function (ret) {
                                $(".submitBtn").show();
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

    //var opts = $("#source").html(), opts2 = "<option></option>" + opts;
    //$("select.populate").each(function () { var e = $(this); e.html(e.hasClass("placeholder") ? opts2 : opts); });

</script>