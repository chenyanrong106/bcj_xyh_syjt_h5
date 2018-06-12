<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BangPhone.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.BangPhone" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /> 
<meta content="yes" name="apple-mobile-web-app-capable" /> 
<meta content="black" name="apple-mobile-web-app-status-bar-style" /> 
<meta content="telephone=no" name="format-detection" />
<link type="text/css" href="css/main.css" rel="stylesheet" />
<title>登录爱宠筹</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="acc-login-box">
    <div class="acc-login-touxiang"><img src="<%if(oa!=null){ %><%=oa.headimgurl %><%}else{ %><%= "../../assets/images/logo.jpg"%><%} %>" /></div>
    <div class="acc-login-name"><%if(oa!=null){ %><%=oa.Nickname %><%}else{ %>爱宠筹<%} %></div>
    <div class="acc-login-info"><p>太棒了！只需验证手机号就完成注册了</p></div>
</div>

<div class="acc-login-input">
    <ul>
        <li><p><strong>手机号</strong><span><input name="" type="tel" id="txtphone" placeholder="输入你的手机号" maxlength="11" class="txt"></span></p></li>
        <li><p><strong>验证码</strong><span><input name="" type="tel" id="amt" placeholder="填写短信验证码" maxlength="6" class="txt" style="width:60%; float:left;"><bdo class="yanzhengma" id="timeid">获取验证码</bdo></span></p></li>
    </ul>
    <ul>
       <li class="padd10"><div class="submitBtn">确定</div></li>
       <li><p class="shuoming"><i class="checkbox checked"></i><i>已阅读并同意<a href="wd.aspx" class="yelllow">《爱宠筹用户注册协议》</a></i></p></li>
    </ul>
</div>


   <div class="weixinlogin" style="display:none;">
      <span class="weixinIcon"></span>
      <span>微信登录</span>
   </div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
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
            $.MsgBox.Alert("爱宠筹", "请输入11位手机号码");
        }
        else if (flag == true) {
            $.ajax({
                type: "POST",
                url: "bangphone.aspx?para=SendDX&phone=" + $("#txtphone").val(),
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
                    $.MsgBox.Alert("宠物管家", "发送失败");
                }
            });
          
        }


    })

    $(".submitBtn").click(function () {
        if ($("#txtphone").val().length != 11) {
            $.MsgBox.Alert("爱宠筹", "请输入11位手机号码");
        }
        else if ($("#amt").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请输入验证码");
        }
        else {
            $.ajax({
                type: "POST",
                url: "bangphone.aspx?para=bang&phone=" + $("#txtphone").val() + "&yzm=" + $("#amt").val(),
                async: false,
                timeout: 15000,
                dataType: "json",
                success: function (data) {
                    if (data.status == -1) {
                        $.MsgBox.Alert("爱宠筹", data.message);
                    }
                    else {
                        //$.MsgBox.Alert("爱宠筹", data.message, function () {
                            location = data.url;
                        //});
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //$.MsgBox.Alert("爱宠筹", "发送失败");
                }
            });
        }
    });
</script>
