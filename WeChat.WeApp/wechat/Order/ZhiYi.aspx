<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZhiYi.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.ZhiYi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/main.css" rel="stylesheet" />
    <title>我要举报</title>
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
            <ol class="width20" onclick="fh();"><span class="fanhui">返回</span></ol>
            <ol class="width60"><strong>我要举报</strong></ol>
            <ol class="width20"></ol>
        </div>

        <div class="acc-petchat">
            <div class="acc-login-input inputbg2">
                <span style="color: red; text-align: center; margin: 0 auto; padding-left: 10px;">*官方只受理内容为基地是否存在，基地无狗，买卖狗，发起人不是基地负责人等，请务必填写真实邮箱信息，工作人员查实后，将以邮件的方式通知您。
                </span>
                <h2>负责人相关信息</h2>
                <ul style="padding: 0;">
                    <li>
                        <p><strong>真实姓名</strong><span><input name="" type="text" id="txtname" class="txt" placeholder="填写您的真实姓名"></span></p>
                    </li>
                    <li>
                        <p><strong>身份证号</strong><span><input name="" type="text" id="txtcardno" class="txt" placeholder="填写您的真实身份证号码"></span></p>
                    </li>
                    <li>
                        <p><strong>联系电话</strong><span><input name="" type="tel" id="txtphone" class="txt" placeholder="填写您的手机号码"></span></p>
                    </li>
                    <li>
                        <p><strong>联系邮箱</strong><span><input name="" type="tel" id="txtemail" class="txt" placeholder="填写您的邮箱"></span></p>
                    </li>
                    <li>
                        <p>
                            <textarea name="" cols="" rows="" class="dropdown-area" id="txtdetail" placeholder="请填写举报的理由"></textarea>
                        </p>
                    </li>
                </ul>
            </div>


            <div class="acc-login-input baibg">
                <h2>上传举报相关图片</h2>
                <div class="acc-upload-box">

                    <div style="position: relative; width: 100%; display: block; overflow: hidden; padding: 10px 0;">
                        <ul id="imglist" class="post_imglist"></ul>
                        <div class="upload_btn">
                            <input type="file" id="upload_image" name="upload_image" value="图片上传"  onchange="FileUpload_onselect()">
                        </div>
                    </div>

                    <p class="upload-info">爱宠筹建议你上传多张有效的举报相关图片，并至少上传一张手持证件的照片</p>
                </div>
            </div>


            <div class="acc-login-input">
                <ul>
                    <li class="padd10">
                        <div class="submitBtn">确认举报</div>
                    </li>
                </ul>
                <span style="color: red; text-align: center; margin: 0 auto; padding-left: 10px;">*防止恶意举报，需要实名制，我们会严格保密您的资料
                </span>
            </div>

            <input type="hidden" id="jid" value="<%=Request.QueryString["jid"]??"0" %>" />
            <input type="hidden" id="jname" value="<%=Request.QueryString["jname"]??"0" %>" />
            <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="js/oa2.js"></script>
<%--<script type="text/javascript" src="js/localResizeIMG.js"></script>
<script type="text/javascript" src="js/mobileBUGFix.mini.js"></script>--%>
<script src="../../assets/js/ajaxfileupload.js"></script>
<script src="js/Message2.js"></script>
<script type="text/javascript">
    var tipsflag = true;
    function tips(text) {
        if (tipsflag == true) {
            var tishiDiv = document.createElement('div');
            tishiDiv.className = "motify";
            document.body.appendChild(tishiDiv);
            tipsflag = false;
        }
        $('.motify').html(text).show();
        setTimeout(function () { $('.motify').fadeOut(); }, 500);
    }

    var flag = true;
    //$(".submitBtn").click(function () {

    //    if (flag == true) {
    //        tips("请填写完整信息！");
    //        //flag=false;
    //    }


    //})
    $.MsgBox.Alert("爱宠筹", "近期恶意举报频发，给项目发起人及支持用户带来不利影响。即日起，平台将对举报人进行实名认证。该实名认证仅用于追究恶意举报人民事及刑事责任使用。");
    function fh() {
        var jid = $("#jid").val();
        var p = $("#urlpara").val();
        if (jid == 0) {
            location = "chou.aspx?p=" + p;
        }
        else {
            location = "detail.aspx?id=" + jid + "&p=" + p;
        }
    }

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
                    url: 'zhiyi.aspx', //用于文件上传的服务器端请求地址
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
                            $.post("zhiyi.aspx?para=delete&id=" + $(this).parent('li').find("input").attr("id"), {
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
        if ($("#txtname").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请填写您的真实姓名");
        }
        else if (isCardID($("#txtcardno").val()) != "true") {
            $.MsgBox.Alert("爱宠筹", isCardID($("#txtcardno").val()));
        }
        else if ($("#txtphone").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请填写联系电话");
        }
        else if ($("#txtdetail").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请填写举报理由");
        }
        else if ($("#txtemail").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请填写联系邮箱");
        }
        else if (!test($("#txtemail").val())) {
            $.MsgBox.Alert("爱宠筹", "请填写正确的联系邮箱，以便后续反馈跟进进度。");
        }
        else if (pic == "") {
            $.MsgBox.Alert("爱宠筹", "请至少上传一张手持证件的照片");
        }
        else {

            $(".submitBtn").hide();
            $.post("zhiyi.aspx?para=tj", {
                name: $("#txtname").val(),
                cardno: $("#txtcardno").val(),
                phone: $("#txtphone").val(),
                detail: $("#txtdetail").val(),
                jid: $("#jid").val(),
                jname: $("#jname").val(),
                email: $("#txtemail").val(),
                pic: pic
            },
                            function (ret) {
                                $(".submitBtn").show();
                                if (ret.st == -1) {
                                    $.MsgBox.Alert("爱宠筹", ret.msg);
                                }
                                else {
                                    $.MsgBox.Alert("爱宠筹", ret.msg, function () {
                                        fh();
                                    });
                                }
                            },
                            "json"
                      );
        }

    });

    var aCity = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" }

    //验证身份证号码
    function isCardID(sId) {
        var iSum = 0;
        var info = "";
        if (!/^\d{17}(\d|x)$/i.test(sId)) return "你输入的身份证长度或格式错误";
        sId = sId.replace(/x$/i, "a");
        if (aCity[parseInt(sId.substr(0, 2))] == null) return "你的身份证地区非法";
        sBirthday = sId.substr(6, 4) + "-" + Number(sId.substr(10, 2)) + "-" + Number(sId.substr(12, 2));
        var d = new Date(sBirthday.replace(/-/g, "/"));
        if (sBirthday != (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate())) return "身份证上的出生日期非法";
        for (var i = 17; i >= 0; i--) iSum += (Math.pow(2, i) % 11) * parseInt(sId.charAt(17 - i), 11);
        if (iSum % 11 != 1) return "你输入的身份证号非法";
        //aCity[parseInt(sId.substr(0,2))]+","+sBirthday+","+(sId.substr(16,1)%2?"男":"女");//此次还可以判断出输入的身份证号的人性别
        return "true";
    }

    function test(temp) {
        //var temp = document.getElementById("text1");
        //对电子邮件的验证
        var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
        if (!myreg.test(temp)) {
            //alert('提示\n\n请输入有效的E_mail！');
            //myreg.focus();
            return false;
        }
        else {
            return true;
        }
    }

</script>
