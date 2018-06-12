<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bang.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.Bang" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8">
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /> 
<meta content="yes" name="apple-mobile-web-app-capable" /> 
<meta content="black" name="apple-mobile-web-app-status-bar-style" /> 
<meta content="telephone=no" name="format-detection" />
<link type="text/css" href="css/main.css?v=1" rel="stylesheet" />
<title>注册</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="acc-login-box">
    <div class="acc-login-touxiang"><img src="<%if(oa!=null){ %><%=oa.headimgurl %><%}else{ %><%= "image/logo.jpg"%><%} %>" /></div>
    <div class="acc-login-name"><%if(oa!=null){ %><%=oa.Nickname %><%}else{ %>小时光SPA<%} %></div>
    <div class="acc-login-info"><p>太棒了！只需验证手机号就完成注册了</p></div>
</div>

<div class="acc-login-input">
    <ul>
        <li><p><strong>手机号</strong><span><input name="" type="tel" id="txtphone" placeholder="输入你的手机号" maxlength="11" class="txt"></span></p></li>
        <li><p><strong>验证码</strong><span><input name="" type="tel" id="amt" placeholder="填写短信验证码" maxlength="6" class="txt" style="width:60%; float:left;"><bdo class="yanzhengma" id="timeid">获取验证码</bdo></span></p></li>
    </ul>
    <ul>
       <li class="padd10"><div class="submitBtn">确定</div></li>
     <%--  <li><p class="shuoming"><i class="checkbox checked"></i><i>已阅读并同意<a href="wd.aspx" class="yelllow">《小时光SPA用户注册协议》</a></i></p></li>--%>
    </ul>
</div>

                <input type="hidden" id="ztitle" value="注册小时光，送你代金券。" />
        <input type="hidden" id="ftitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
   <div class="weixinlogin" style="display:none;">
      <span class="weixinIcon"></span>
      <span>微信登录</span>
   </div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
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

    var num = 60;
    var timerrr;
    var flag = true;
    var daojishi = document.getElementById('timeid');
    function dao() {
        if (num > 0) {
            num -= 1;
            daojishi.innerHTML = num;
            if (num < 10) {
                daojishi.innerHTML = "0" + num;
            }
        }
        if (num == 0) {
            daojishi.innerHTML = "获取验证码";
            num = 60;
            daojishi.className = 'yanzhengma';
            flag = true;
            clearInterval(timerrr);
        }

    }
    $("#timeid").click(function () {

        if ($("#txtphone").val().length != 11) {
            $.MsgBox.Alert("小时光SPA", "请输入11位手机号码");
        }
        else if (flag == true) {
            $.ajax({
                type: "POST",
                url: "bang.aspx?para=SendDX&phone=" + $("#txtphone").val(),
                async: false,
                timeout: 15000,
                dataType: "json",
                success: function (data) {

                    daojishi.className = 'yanzhenghui';
                    dao();
                    timerrr = setInterval('dao()', 1000);
                    tips(data.message);
                    flag = false;

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.MsgBox.Alert("小时光SPA", "发送失败");
                }
            });

        }


    })

    $(".submitBtn").click(function () {
        if ($("#txtphone").val().length != 11) {
            $.MsgBox.Alert("小时光SPA", "请输入11位手机号码");
        }
        else if ($("#amt").val() == "") {
            $.MsgBox.Alert("小时光SPA", "请输入验证码");
        }
        else {
            $.ajax({
                type: "POST",
                url: "bang.aspx?para=bang&phone=" + $("#txtphone").val() + "&yzm=" + $("#amt").val(),
                async: false,
                timeout: 15000,
                dataType: "json",
                success: function (data) {
                    if (data.status == -1) {
                        $.MsgBox.Alert("小时光SPA", data.message);
                    }
                    else {
                        $.MsgBox.Alert("小时光SPA", data.message, function () {
                        location = data.url;
                        });
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //$.MsgBox.Alert("小时光SPA", "发送失败");
                }
            });
        }
    });
</script>
