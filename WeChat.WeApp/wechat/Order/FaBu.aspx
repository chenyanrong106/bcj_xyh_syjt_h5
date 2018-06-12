<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaBu.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.FaBu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/main.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script src="js/oa2.js"></script>
    <title>我的发布</title>
</head>
<body class="bodybg">
    <form id="form1" runat="server">
        <div class="acc-top-header">
            <ol class="width20" onclick="window.location='chou.aspx?p=<%=Request.QueryString["p"]??"1" %>'"><span class="fanhui">首页</span></ol>
            <ol class="width60"><strong>爱宠筹</strong></ol>
            <ol class="width20" onclick="window.location='wo.aspx?p=<%=Request.QueryString["p"]??"1" %>'"><span class="wodeicon">我的</span></ol>
        </div>

        <div class="acc-petchat">
            <div class="acc-petliang acc-order" id="accorder">
                <ol onclick="ck(0);"><span class="curbtn">进行中</span></ol>
                <ol onclick="ck(1);"><span>已成功</span></ol>
                <ol onclick="ck(2);"><span>已失败</span></ol>
            </div>


            <div class="acc-jili-items">
                <ul id="ulist">

                </ul>
            </div>

        </div>
         <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />
    </form>
</body>
</html>

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

    $(function () {
        var $this = $("#wenzhang");
        var scrollTimer;
        $this.hover(function () {
            clearInterval(scrollTimer);
        }, function () {
            scrollTimer = setInterval(function () {
                scrollNews($this);
            }, 4000);
        }).trigger("mouseout");
    });

    function scrollNews(obj) {
        var $self = obj.find("ul:first");
        var lineHeight = $self.find("li:first").height();
        $self.animate({ "margin-top": -lineHeight + "px" }, 500, function () {
            $self.css({ "margin-top": "0px" }).find("li:first").appendTo($self);
        })
    }

    function ck(id) {
        $("#accorder ol span").removeClass("curbtn");
        $($("#accorder ol span")[id]).addClass("curbtn");
        $container.html("");
        type = id + 1;
        num = 1;
        if (id < 2) {
            getList2();
        }
    }

    var type = 1;
    var flag = true;
    var $container = $('#ulist');
    //按需加载
    $(window).scroll(function () {
        // 当滚动到最底部以上100像素时， 加载新内容
        if ($(document).height() - $(this).scrollTop() - $(this).height() < 50) {
            if (flag) {
                flag = false;
                getList2();
            }
        }
    });
    var num = 1;  //初始默认为1,前面已经加载过一次
    function getList2() {
        getList(num);
    }
    function getList(n) {
        var boxes = [];
        $.ajax({
            type: "get",
            url: "fabu.aspx?para=tj&page=" + n + "&type=" + type + "&p=" + $("#urlpara").val(),
            beforeSend: function (XMLHttpRequest) {
                // $("#div1").html('<div class="loading">正在加载</div>');
            },
            success: function (data, textStatus) {
                if (isNaN(data)) {
                    num++;
                    flag = true;
                    boxes.push(data);
                    var $boxes = $(boxes.join(""));
                    $container.append($boxes);
                } else {
                    return false;
                }
            },
            complete: function (XMLHttpRequest, textStatus) {
                // alert("加载完成！");
            },
            error: function () {
                //alert("加载出错！");
            }
        });
        //把数组转成字符串
        return boxes.join("");
    };

    getList2();
</script>
