<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shenhe1.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.shenhe1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>爱宠筹</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <script src="js/jquery.min.js"></script>
    <script src="js/oa.js?v=2"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/GetLocation.js?v=1.0"></script>
    <script src="js/jquery.masonry.min.js"></script>
    <script src="js/lightbox-2.6.min.js"></script>
    <script type="text/javascript" src="js/lazy.js"></script>
    <script type="text/javascript" src="js/swiper.js"></script>
    <script src="js/echo.js"></script>
    <link href="css/pj.css" rel="stylesheet" />
    <%--<link type="text/css" rel="stylesheet" href="css/meike.css" />--%>
    <link type="text/css" href="css/main.css" rel="stylesheet" />
    <link href="css/lightbox.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <%-- <div class="header_box">
            <p><span><a href="login.aspx"><bdo style="margin-top:10px;">申请筹粮</bdo></a></span>此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办</p>
        </div>--%>

        






        <div class="acc-petchat">
           


            <%--<div class="acc-news-pro swiper-container">
                <div class="swiper-wrapper">
                    <%foreach (var nj in njlist)
                      {
                    %>
                    <div class="swiper-slide" onclick="window.location='detail.aspx?id=<%=nj.ID %>&p=<%=Request.QueryString["p"]??"1" %>'">
                        <div class="newbox">
                            <div class="imgbox">
                                <img src="<%=WebUrl + (nj.CImg==null?nj.Img1:nj.CImg) %>">
                            </div>
                            <p><strong><%=nj.Title %></strong></p>
                            <div class="jindutiao">
                                <div class="wanchengtiao" style="width: <%=Math.Round(nj.je.Value / 16 / nj.Goal.Value * 100m, 2) %>%;"></div>
                            </div>
                            <p>已筹<bdo><%=nj.JZType==1?Math.Round(nj.je.Value/16,0) :nj.je%>/<%=nj.Goal %></bdo><%=nj.JZType==1?"KG" :"元"%></p>
                        </div>
                    </div>
                    <% } %>
                </div>
            </div>--%>


             <div class="acc-news-pro swiper-container">
                <div class="swiper-wrapper">
                    <%foreach (var nj in njlist)
                      {
                    %>
                    <div class="swiper-slide" onclick="window.location='detail.aspx?id=<%=nj.ID %>&p=<%=Request.QueryString["p"]??"1" %>'">
                        <div class="newbox">
                            <div class="imgbox">
                                <img src="<%=WebUrl + (nj.CImg==null?nj.Img1:nj.CImg) %>">
                            </div>
                            <p><strong><%=nj.Title %></strong></p>
                            <div class="jindutiao">
                                <div class="wanchengtiao" style="width: <%=Math.Round(nj.je.Value / 16 / nj.Goal.Value * 100m, 2) %>%;"></div>
                            </div>
                            <p>已筹<bdo><%=nj.JZType==1?Math.Round(nj.je.Value/16,0) :nj.je%>/<%=nj.Goal %></bdo><%=nj.JZType==1?"KG" :"元"%></p>
                        </div>
                    </div>
                    <% } %>
                </div>
            </div>

            <div class="shoplist">

                <ul id="listing">
                </ul>
                <div id="loading">上拉获取更多数据...</div>
                <div id="nomoreData">亲，已经加载完了！</div>

            </div>

        </div>

        <!--新版-->

        <input type="hidden" id="ztitle" value="帮助可爱的毛孩子们吃上饱饭" />
        <input type="hidden" id="ftitle" value="此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办" />
        <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />
    </form>
</body>
</html>
<script>
    //var flag = true;
    //var $container = $('#listing');
    ////按需加载
    //$(window).scroll(function () {
    //    // 当滚动到最底部以上100像素时， 加载新内容
    //    if ($(document).height() - $(this).scrollTop() - $(this).height() < 50) {
    //        if (flag) {
    //            flag = false;
    //            getList2();

    //        }
    //    }
    //});
    //var num = 1;  //初始默认为1,前面已经加载过一次
    //function getList2() {
    //    var $boxes = $(getList());
    //    $container.append($boxes);
    //}
    //function getList() {
    //    var boxes = [];
    //    $.ajax({
    //        type: "get",
    //        url: "chou.aspx?para=tj&page=" + num + "&p=" + $("#urlpara").val(),
    //        beforeSend: function (XMLHttpRequest) {
    //            // $("#div1").html('<div class="loading">正在加载</div>');
    //        },
    //        success: function (data, textStatus) {
    //            if (isNaN(data)) {
    //                num++;
    //                flag = true;
    //                boxes.push(data);
    //                var $boxes = $(boxes.join(""));
    //                $container.append($boxes);
    //            } else {
    //                return false;
    //            }
    //        },
    //        complete: function (XMLHttpRequest, textStatus) {
    //            // alert("加载完成！");
    //        },
    //        error: function () {
    //            //alert("加载出错！");
    //        }
    //    });
    //    //把数组转成字符串
    //    return boxes.join("");
    //};

    //getList2();



    echo.init({
        offset: 100,
        throttle: 250, unload: false
    });

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


    var swiper = new Swiper('.swiper-container', {
        slidesPerView: 'auto',
        paginationClickable: true,
        spaceBetween: 1
    });

    //数据加载
    var pageNum = 1; rowsNum = 10;
    var isNoMoreData = false, isLoading = false;
    $(window).scroll(function () {
        echo.render();
        var curTop = $(window).scrollTop();
        var juli = $(document).height() - $(window).height();
        if (curTop >= juli) {
            if (!isNoMoreData && !isLoading) {
                showPetdata(pageNum + 1);
            }
        }
    });
    showPetdata();
    function showPetdata() {
        $.ajax({
            type: "Get",
            url: "shenhe1.aspx?para=tj&page=" + pageNum + "&p=" + $("#urlpara").val(),
            dataType: 'html',
            async: false,
            beforeSend: function () {
                isLoading = true;
                $("#loading").show();
            },
            success: function (data) {
                //alert(data);
                //var data = JSON.parse(json);
                if (data !== null) {

                    if (data == "") {
                        $('#nomoreData').show();
                        $("#loading").hide();
                        isNoMoreData = true;
                    } else {


                        $('.shoplist ul').append(data);
                        $("#loading").show();
                        isLoading = false;

                        pageNum++;
                    }
                } else {
                    isNoMoreData = true;
                    $('#nomoreData').show();
                    $("#loading").hide();
                }
            },
            error: function (error) {
                console.log(error);
            }
        });

    }

    $("#rule1").click(function () {
        $("#dialog1").show();
    });

    $(".btn_close").click(function () {
        $("#dialog1").hide();
    });
</script>
