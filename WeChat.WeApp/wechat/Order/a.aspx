<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.a" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>爱宠筹</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/meike.css" />
    <script src="js/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pays-info">
            <h2>祝福</h2>
            <h6>写给谈三丽同学的新婚祝福</h6>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;又是一年开学季，距离初三那年开学已经过去了整整十年，转眼间就是一个十年，时间过得好快啊，不禁又想起了那一年李老师安排在曦萌前面两排及后面两排的众女生了（此处省略了8000余字十年回忆）<sup>①②③</sup><br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;时间飞快而又无比漫长，这些年里你一个小姑娘为了工作独自在异乡闯荡，走遍了半个中国，无依无靠，令人心酸。<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;好在一切都过去了，你终于找到了可以依靠的人，并将一起步入婚姻的殿堂，真是替你们感到无比的高兴,在此奉上最真挚的祝福，祝福你们新婚愉快，幸福美满，激情永在，白头偕老！

            <br />
            <br />
            <br />
            <div style="text-align:right;">2016.09.01&nbsp;&nbsp;上海&nbsp;&nbsp;</div>
            <br />
            <br />
            <br />
            注：<br />
            ①因每年都可以回忆成一篇800多字的作文，所以这里的8000字出处是科学的。<br />
            ②回忆太多伤神，故此处省略。<br />
            ③略。
            <div class="shoukuanfang">
                <p>
                    <img src="images/hb2.jpg">同心同德美满夫妻 
                    <br />
                    克勤克俭幸福鸳鸯
                </p>
            </div>

            <div class="weimaBox">
                <p>
                    <img src="images/hb4.png">
                </p>

            </div>
    </form>
</body>
</html>
<script type="text/javascript">
    document.getElementById('showbtn').onclick = function () {
        document.getElementById('showbox').style.display = 'block';
    }

    document.getElementById('zhidaoBtn').onclick = function () {
        document.getElementById('showbox').style.display = 'none';
    }

    function jc() {
        $.post("test.aspx?para=tj&orderno=" + $("#orderno").val(), {

        },
                               function (ret) {
                                   if (ret.st > 0) {
                                       window.location = "chou.aspx";
                                   }
                                   else {
                                       setTimeout("jc()", 1000);
                                   }

                               },
                               "json"
                         );
    }

    jc();
</script>
