<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chou.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.chou" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <style>
        .footerbar{width:100%; height:40px; background:#fbfaf8; border-top:1px solid #ddd; position:fixed; bottom:0; left:0;min-width:320px; padding:5px 0; z-index:9999999;}
.footerbar ul li{float:left; width:20%; display:block; text-align:center; font-weight:normal;}
.jiahao{ width:42px; height:42px; background:url(img/jiahao.png) no-repeat center center #ff7200;background-size:22px; border-radius:42px; display:block; margin:0px auto;}
.home{background:url(img/home.png) no-repeat center 3px; padding-top:25px; height:14px; display:block; background-size:22px; font-size:12px; color:#ff7200;}
.homehui{background:url(img/home2.png) no-repeat center 3px; padding-top:25px; height:14px; display:block; background-size:22px; font-size:12px; color:#999;}
.aixin{background:url(img/aixin.png) no-repeat center 1px; padding-top:25px; height:14px; display:block; background-size:24px; font-size:12px; color:#ff7200;}
.aixinhui{background:url(img/aixin2.png) no-repeat center 1px; padding-top:25px; height:14px; display:block; background-size:24px; font-size:12px; color:#999;}
.huzhu{background:url(img/huzhu.png) no-repeat center 1px; padding-top:25px; height:14px; display:block; background-size:24px; font-size:12px; color:#ff7200;}
.huzhuhui{background:url(img/huzhu2.png) no-repeat center 1px; padding-top:25px; height:14px; display:block; background-size:24px; font-size:12px; color:#999;}
.admin{background:url(img/admin.png) no-repeat center 3px; padding-top:25px; height:14px; display:block; background-size:20px; font-size:12px; color:#ff7200;}
.adminhui{background:url(img/admin2.png) no-repeat center 3px; padding-top:25px; height:14px; display:block; background-size:20px; font-size:12px; color:#999;}
    </style>
    <NOSCRIPT><iframe src="*.html"></iframe></NOSCRIPT> 
</head>
<body>
    <form id="form1" runat="server">
       <%-- <div class="header_box">
            <p><span><a href="login.aspx"><bdo style="margin-top:10px;">申请筹粮</bdo></a></span>此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办</p>
        </div>--%>

        <div class="petchat">
            <%--            <div class="petliang">
                <ol>
                    <p><span>总凑粮</span><strong><%=Math.Round(orderprice/16,2) %><em>千克</em></strong></p>
                </ol>
                <ol>
                    <p><span>参与捐赠</span><strong><%=ordercount %><em>人</em></strong></p>
                </ol>
                <ol>
                    <p><span>人均凑粮</span><strong><%=Math.Round(orderprice/16/ordercount,2) %><em>千克</em></strong></p>
                </ol>
            </div>--%>

            <div class="page trans1" id="container" style="display: block; border-bottom: 10px solid #eee; display: none;">
                <div class="page2" style="opacity: 1;">
                    <div class="mod">
                        <div class="mod_title">
                            <h2>爱心配捐</h2>
                            <a href="javascript:;" class="a_match_rule" id="rule1">活动规则</a>
                        </div>
                        <div class="match_info">
                            <p class="match_p1"><span class="num"></span>今日平台总配捐池<span class="num"><%=pj.PJ %>kg</span></p>
                            <p class="match_p2">已经被使用配捐额<span class="num"><%=Math.Round(pj.YJ.Value/16,3) %>kg</span></p>
                        </div>
                    </div>
                </div>
            </div>
       
        </div>


        <!-- 规则1 -->
        <div class="dialog_wrap" id="dialog1">
            <div class="dialog">
                <div class="swiper-container ruleSlider" id="ruleSlider">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide">
                            <h2>爱心配捐规则</h2>
                            <ol>
                                <li>配捐时段：每天早上10点开始，先捐先配，配完即止。</li>
                                <li>项目范围：所有在筹项目。 </li>
                                <li>配捐额：看详细配捐池。</li>
                                <li>配捐规则：前50%配捐池进行6：1配捐，最低10元起配，后50%随机配捐，用完截止。</li>
                                <li>最终解释权归爱宠筹平台所有</li>
                            </ol>
                        </div>
                    </div>
                    <div class="swiper-pagination"></div>
                </div>
                <div class="btn_close"></div>
            </div>
        </div>



        <!--新版-->
        <div class="acc-top-header" style="border-bottom: 1px solid #eee;">
            <ol class="width20" onclick="window.location='http://mp.weixin.qq.com/s/EB8uuBPCyFxCFbffGT3JkQ';"><span class="guanzhu">关注</span></ol>
            <ol class="width60"><strong>爱宠筹</strong></ol>
            <ol class="width20">&nbsp;</ol>
        </div>

        <div class="acc-petchat">
            <div class="acc-petliang">
                <ol>
                    <p><span>参与人次</span><strong><%=ordercount %><em>次</em></strong></p>
                </ol>
                <ol>
                    <p><span>已筹宠粮</span><strong><%=Math.Round(orderprice/16/10000,2) %><em>万Kg</em></strong></p>
                </ol>
                <ol>
                    <p><span>今日配捐池</span><strong><%=Math.Round(pj.YJ.Value/16,2) %>/<%=pj.PJ %><em>kg</em></strong></p>
                </ol>
            </div>
            <div class="acc-acitve" id="wenzhang">
                <ul>
                    <li>
                        <p><a href="#">此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办</a></p>
                    </li>
                    <li>
                        <p>此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办</p>
                    </li>
                    <li>
                        <p>此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办</p>
                    </li>
                    <li>
                        <p>此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办</p>
                    </li>
                </ul>
            </div>

            <div class="acc-menu" style="display:none;">
                <ul>
                    <li><a href="choupage.aspx?p=<%=Request.QueryString["p"]??"1" %>&jztype=1" ><span class="icon01"></span>
                        <p>筹粮活动</p>
                    </a>
                    </li>
                    <li><a href="choupage.aspx?p=<%=Request.QueryString["p"]??"1" %>&jztype=2"><span class="icon02"></span>
                        <p>筹款活动</p>
                    </a>
                    </li>
                    <li><a href="javascript:;" onclick="window.alert('即将开放');"><span class="icon03"></span>
                        <p>粮款公开</p>
                    </a>
                    </li>
                    <li><a href="sq1.aspx"><span class="icon04"></span>
                        <p>发起活动</p>
                    </a>
                    </li>

                </ul>
            </div>

            <div class="acc-news-pro swiper-container">
                <div class="swiper-wrapper">
                    <%foreach (var nj in njlist)
                      {
                          if(nj.ID!=211){
                    %>
                    <div class="swiper-slide" onclick="window.location='detail.aspx?id=<%=nj.ID %>&p=<%=Request.QueryString["p"]??"1" %>'">
                        <div class="newbox">
                            <div class="imgbox">
                                <img src="<%=WebUrl + (nj.CImg==null?nj.Img1:nj.CImg) %>">
                            </div>
                            <p><strong style="font-size: 1.2rem;"><%=nj.NickName %></strong></p>
                            <div class="jindutiao">
                                <div class="wanchengtiao" style="width: <%=Math.Round(nj.je.Value / 16 / nj.Goal.Value * 100m, 2) %>%;"></div>
                            </div>
                            <p>已筹<bdo><%=nj.JZType==1?Math.Round(nj.je.Value/16,0) :nj.je%>/<%=nj.Goal %></bdo><%=nj.JZType==1?"KG" :"元"%></p>
                        </div>
                    </div>
                    <% }} %>
                </div>
            </div>




            <div class="shoplist">

                <ul id="listing">
                </ul>
                <div id="loading">上拉获取更多数据...</div>
                <div id="nomoreData" style="margin-bottom:120px;">亲，已经加载完了，系统默认加载12条数据！</div>

            </div>

        </div>

        <!--新版-->

         <div class="footerbar">
            <ul>
                <li><a href="chou.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="home">筹粮</bdo></a></li>
                <li><a href="choupage.aspx?p=<%=Request.QueryString["p"]??"1" %>&jztype=2"><bdo class="aixinhui">筹款</bdo></a></li>
                <li onclick="window.location='sq1.aspx'"><bdo class="jiahao"></bdo></li>
                <li><a href="dongtai.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="huzhuhui">动态</bdo></a></li>
                <li><a href="wo.aspx?p=<%=Request.QueryString["p"]??"1" %>"><bdo class="adminhui">我</bdo></a></li>
            </ul>
        </div>

        <input type="hidden" id="ztitle" value="帮助可爱的毛孩子们吃上饱饭" />
        <input type="hidden" id="ftitle" value="此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办" />
        <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />

    </form>
</body>
</html>
<script>
    window.onload = function () {
        document.onkeydown = function () {
            var e = window.event || arguments[0];
            if (e.keyCode == 123) {
                //alert("小样你想干嘛？");
                return false;
            } else if ((e.ctrlKey) && (e.shiftKey) && (e.keyCode == 73)) {
               // alert("还是不给你看。。");
                return false;
            }
        };
        document.oncontextmenu = function () {
           // alert("小样不给你看");
            return false;
        }
    }
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
            url: "chou.aspx?para=tj&page=" + pageNum + "&p=" + $("#urlpara").val(),
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
<%--<SCRIPT language=JavaScript> 
<!-- 
    if (window.Event)
        document.captureEvents(Event.MOUSEUP);
    function nocontextmenu() {
        event.cancelBubble = true
        event.returnValue = false;
        return false;
    }
    function norightclick(e) {
        if (window.Event) {
            if (e.which == 2 || e.which == 3)
                return false;
        }
        else
            if (event.button == 2 || event.button == 3) {
                event.cancelBubble = true
                event.returnValue = false;
                return false;
            }
    }
    document.oncontextmenu = nocontextmenu; // for IE5+ 
    document.onmousedown = norightclick; // for all others 
    //--> 
</SCRIPT>--%>