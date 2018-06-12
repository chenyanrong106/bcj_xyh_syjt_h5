<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sq2.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.sq2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>申请</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script src="js/oa2.js"></script>
    <meta content="telephone=no" name="format-detection" />
    <%-- <link type="text/css" rel="stylesheet" href="css/meike.css" />--%>
    <link type="text/css" href="css/main.css" rel="stylesheet" />
</head>
<body class="bodybg">
    <form id="form1" runat="server">
        <div id="jz" style="display: none;">
            &nbsp;<br />
            <img src="images/jz.jpg" /><br />
            正在上传···<br />
            &nbsp;
        </div>

        <div class="navbar">
            <div class="tabbar">
                <span><a href="javascript:;" onclick="cl(0);" class="on">发起筹粮</a></span>
                <span><a href="javascript:;" onclick="cl(1);">发起筹款</a></span>
            </div>
        </div>


        <div class="acc-login-input faqibg">
            <ul style="padding: 0; margin-bottom: 10px;">

                <li>
                    <p><strong id="titmb">目标狗粮</strong><span><input name="" type="number" id="mb" class="txt" maxlength="20" placeholder="填写目标" style="width: 90%; float: left;"><bdo class="jiage">KG</bdo></span></p>
                </li>
                <li>
                    <p><strong>截止日期</strong><span class="spanColor" id="timeday"></span></p>
                </li>

                <li>
                    <p class="huakuai">
                        <bdo id="beginid">3天</bdo><bdo class="huakuaicon">
                            <em class="kuaitiao"><em class="kuaitiaoUp" id="yellowkuai"></em></em>
                            <i class="huaBtn" id="tuobtn"></i>
                        </bdo><bdo id="endid">30天</bdo>
                    </p>
                </li>
            </ul>
        </div>


        <div class="acc-login-input baibg">
            <ul style="padding: 0; margin-bottom: 10px;">

                <li>
                    <p><strong>筹款标题</strong><span><input name="" type="text" id="bt" class="txt" maxlength="50" placeholder="填写标题"></span></p>
                </li>
                <li>
                    <p>
                        <textarea name="" cols="" id="xq" rows="" maxlength="1500" class="dropdown-area" placeholder="详细描述
(建议详细描述保护动物情况：如动物特殊情况介绍／申请动物保护理由等。)"></textarea>
                    </p>
                </li>
                <li>
                    <p class="faqi-info">项目内容和项目图片禁止透露任何<bdo>联系方式</bdo>和<bdo>银行卡</bdo>等首款信息，包括但不限于手机号码、座机号码、微信号、支付宝账号、银行卡号等。违反以上规定，项目验证和提现申请均不予通过。</p>
                </li>

            </ul>
        </div>

        <%-- <div class="choukuan-box">
            <p><strong>目标狗粮</strong><input name="" id="mb" type="number" class="txt" placeholder="请填写目标重量">千克</p>
        </div>--%>

        <%--   <div class="choukuan-box">
            <ul>
                <li>
                    <input name="" type="text" id="bt" class="txt2" placeholder="填写筹粮标题"></li>
                <li style="border-bottom: none;">
                    <textarea name="" id="xq" cols="" rows="" placeholder="这里填写当前救助基地的真实情况，筹粮的目的，大概字数在300左右效果最好"></textarea>
                    <p class="ptxt">温馨提示：凡是填写不合格的地方，将不予审核通过！</p>
                </li>
            </ul>
            <ul>
                <li><span class="uploadtxt">上传筹粮宠物照片</span>
                    <div style="position: relative; width: 100%; display: block; overflow: hidden; padding: 10px 0;">
                        <ul id="imglist" class="post_imglist"></ul>
                        <div class="upload_btn">
                            <input type="file" id="upload_image" name="upload_image" value="图片上传"  onclick="FileUpload_onclick()" onchange="FileUpload_onselect()">
                        </div>
                    </div>
                </li>
                <li>
                    <p class="ptxt">建议多张上传动物拍照，提高项目可信度</p>
                </li>
            </ul>
        </div>--%>


        <div class="acc-login-input baibg">
            <div class="acc-upload-box">

                <div style="position: relative; width: 100%; display: block; overflow: hidden; padding: 10px 0;">
                    <ul id="imglist" class="post_imglist"></ul>
                    <div class="upload_btn">
                        <input type="file" id="upload_image" name="upload_image" value="图片上传"  onclick="FileUpload_onclick()" onchange="FileUpload_onselect()">
                    </div>
                </div>

                <p class="upload-info">通道发起的动物保护项目，建议多张上传动物拍照，提高项目可信度。</p>
            </div>
        </div>

        <div class="acc-login-input baibg">
            <div class="acc-upload-box">

                <div style="position: relative; width: 100%; display: block; overflow: hidden; padding: 10px 0;">
                    <ul id="imglist2" class="post_imglist"></ul>
                    <div class="upload_btn">
                        <input type="file" id="upload_image2" name="upload_image2" value="图片上传"  onclick="FileUpload_onclick()" onchange="FileUpload_onselect2()">
                    </div>
                </div>

                <p class="upload-info">请上传一张基地的logo</p>
            </div>
        </div>


        <div class="acc-login-input">
            <ul>
                <li class="padd10">
                    <div class="submitBtn">发起项目</div>
                </li>
                <li>
                    <p class="shuoming"><i class="checkbox checked"></i>已阅读并同意<a href="wd2.aspx" class="yelllow">《爱宠筹项目发起条款》</a>及<a href="wd3.aspx" class="yelllow">《发起人承诺书》</a></p>
                </li>
            </ul>
        </div>
        <span style="color: red; text-align: center; margin: 0 auto; padding-left: 10px;">*如发起有遇到问题，请加客服微信anitamiao0916，进行人工帮助
                </span>
        <%--<div class="paybtn" style="height: 40px; line-height: 40px;">确认提交</div>--%>

        <input type="hidden" id="endtime" value="<%=DateTime.Now.AddDays(3).ToString("yyyy-MM-dd") %>" />
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

    function cl(xb) {
        $(".tabbar a").removeClass("on");
        $($(".tabbar a")[xb]).addClass("on");
        if (xb == 0) {
            $("#titmb").html("目标狗粮");
            $(".jiage").html("KG");
        }
        else {
            $("#titmb").html("目标金额");
            $(".jiage").html("元");
        }
    }

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
    //        flag = false;
    //    }


    //})


    //滑块

    var beginzhi = parseInt(String($('#beginid').html()).replace(/[^0-9]/ig, ""));
    var endzhi = parseInt(String($('#endid').html()).replace(/[^0-9]/ig, ""));
    console.log(beginzhi, endzhi);
    var i = beginzhi;
    dayzhi(i);
    var kuan = parseInt($(document).width() * 0.8);
    var kuanleft = parseInt($(document).width() * 0.2);
    var kuaitiaoUp = document.getElementById('yellowkuai');
    var odiv = document.getElementById('tuobtn');
    console.log(kuan, kuanleft);

    odiv.addEventListener('touchmove', function (event) {
        event.preventDefault();//阻止其他事件
        // 如果这个元素的位置内只有一个手指的话
        if (event.targetTouches.length == 1) {
            var touch = event.targetTouches[0];  // 把元素放在手指所在的位置

            var shubiao = touch.pageX - 90;
            if (shubiao > (kuanleft - 90) && shubiao < (kuan - 90)) {
                odiv.style.left = shubiao + 'px';
                if (shubiao > -10) {
                    kuaitiaoUp.style.width = (shubiao + 10) + 'px';
                } else {
                    kuaitiaoUp.style.width = 0 + 'px';
                }
                var zhilv = (kuan - kuanleft) / (endzhi - beginzhi);
                var dayshu = Math.ceil(parseInt(kuaitiaoUp.style.width) / zhilv);
                var dayzhi2 = dayshu + beginzhi;

                dayzhi(dayzhi2)
            }
        }
    }, false);

    function dayzhi(i) {
        var now = new Date();
        var date = new Date(now.getTime() + i * 24 * 3600 * 1000);
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var day = date.getDate();
        var weeks = date.getDay();


        document.getElementById('timeday').innerHTML = year + '-' + month + '-' + day + ' 共<em>' + i + '</em>天';
        $("#endtime").val(year + '-' + month + '-' + day);
    }

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
                    url: 'sq2.aspx', //用于文件上传的服务器端请求地址
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
            $.MsgBox.Alert("爱宠筹", "基地logo最多上传1张照片");
            //alert("最多上传5张照片");
        }
        else {
            $("#jz").show();

            $.ajaxFileUpload
            (
                {
                    url: 'sq2.aspx', //用于文件上传的服务器端请求地址
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

    $(".submitBtn").click(function () {
        //alert($(".tabbar .on").html());
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
        //else if (pic == "") {
        //    //alert("请上传至少一张照片");
        //    $.MsgBox.Alert("爱宠筹", "请上传照片");
        //} else if (pic2 == "") {
        //    //alert("请上传至少一张照片");
        //    $.MsgBox.Alert("爱宠筹", "请上传基地logo");
        //}

        else {
            $(".submitBtn").hide();
            $.post("sq2.aspx?para=tj", {
                mb: $("#mb").val(),
                bt: $("#bt").val(),
                xq: $("#xq").val(),
                pic: pic,
                pic2: pic2,
                endtime: $("#endtime").val(),
                jztype: $(".tabbar .on").html()
            },
                            function (ret) {
                                $(".submitBtn").show();
                                if (ret.st == -1) {
                                    $.MsgBox.Alert("爱宠筹", ret.msg);
                                }
                                else {
                                    $.MsgBox.Alert("爱宠筹", ret.msg, function () {
                                        WeixinJSBridge.invoke('closeWindow', {}, function (res) {

                                        });
                                    });
                                }
                            },
                            "json"
                      );
        }
    });


</script>
