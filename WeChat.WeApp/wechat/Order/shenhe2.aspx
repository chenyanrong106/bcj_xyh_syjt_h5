<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shenhe2.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.shenhe2" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>爱宠筹</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/meike.css?v=2" />
    <link href="css/lightbox.css" rel="stylesheet" />
    <style>
        .share {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: rgba(0,0,0,.8);
            display: none;
            overflow: hidden;
        }

        .sharebg {
            background: url(images/fx.png) no-repeat center 0;
            background-size: 60%;
            width: 100%;
            height: 100%;
            display: block;
            position: absolute;
            left: 0;
            top: 10%;
        }

        .shopimgbox img {
            -webkit-border-radius: 20px;
            -moz-border-radius: 20px;
            border-radius: 5px;
            -webkit-box-shadow: inset 0 1px 5px rgba(0,0,0,.5);
            -moz-box-shadow: inset 0 1px 5px rgba(0,0,0,.5);
            box-shadow: inset 0 1px 5px rgba(0,0,0,.5);
        }

        /*新增css*/
        .helpTa h2 i {
            float: right;
            color: #999;
            font-size: 12px;
            font-style: normal;
        }

        .faguiBox {
            text-align: left;
        }

            .faguiBox ul li {
                border-bottom: 1px solid #eee;
                line-height: 22px;
                padding: 5px 0;
                margin-bottom: 5px;
            }

                .faguiBox ul li strong {
                    font-size: 1.4rem;
                    color: #333;
                }

                .faguiBox ul li p {
                    display: block;
                    line-height: 22px;
                }

                    .faguiBox ul li p span {
                        display: inline-block;
                        margin-right: 10px;
                        font-size: 12px;
                        color: #1dae65;
                        float: none;
                        background: url(images/gouxuan2.png) no-repeat 0 center;
                        background-size: 16px;
                        padding-left: 20px;
                    }

        .zhiying {
            border: 1px solid #eee;
            width: 60px;
            height: 30px;
            text-align: center;
            background: #eee;
            line-height: 30px;
            border-radius: 5px;
            color: #999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header_box">
            <p>此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办</p>
        </div>

        <div class="petchat" style="padding-bottom: 44px;">

            <div class="shoplist">
                <ul>
                    <li>
                        <%if (j != null)
                          { %>
                        <div class="adminInfo">
                            <p>
                                <span>剩余<em><%=j.EndTime<DateTime.Now?0:(j.EndTime.Value-DateTime.Now).Days+1 %></em>天</span><bdo class="touxiang">
                                    <img src="<%=j.HeadImage %>"></bdo><%=j.NickName %> <em></em>
                            </p>
                        </div>
                        <%} %>

                        <div class="petliang shopdd">
                            <%if (j.JZType == 1)
                              { %>
                            <ol>
                                <p><strong><%=j.Goal %><em>千克</em></strong><span>目标狗粮</span></p>
                            </ol>
                            <%}
                              else if (j.JZType == 2)
                              { %>
                            <ol>
                                <p><strong><%=j.Goal %><em>元</em></strong><span>目标金额</span></p>
                            </ol>
                            <%} %> <%if (s != null)
                                     { %>
                            <ol>
                                <p><strong><%=s.Name %><em></em></strong><span>姓名</span></p>
                            </ol>
                            <ol>
                                <p><strong><%=s.Phone %><em></em></strong><span>手机</span></p>
                            </ol>
                            <%} %>
                        </div>
                        <%if (s != null)
                          { %>
                        <div class="petliang shopdd">
                            <ol>
                                <p><strong><%=s.CardNo %><em></em></strong><span>身份证</span></p>
                            </ol>
                            <ol>
                                <p><strong><%=s.JDName %><em></em></strong><span>基地名称</span></p>
                            </ol>
                            <ol>
                                <p><strong><%=s.WXNo %><em></em></strong><span>微信号</span></p>
                            </ol>
                        </div>
                        <%} %>
                        项目上线时间：
                        <input type="text" class="helpTa txt" placeholder="上线时间" id="txtbegtime" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" />
                        项目结束时间：
                        <input type="text" class="helpTa txt" placeholder="下线时间" id="txtendtime" value="<%=j.EndTime.Value.ToString("yyyy-MM-dd") %>" />
                        <div class="shopTT">
                            <h2><%=j.Title %></h2>
                            <p><%=j.Detail %></p>
                        </div>
                        宠物图片：
                        <div class="shopimgbox">
                            <%if (j.Img1 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+j.Img1 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.SImg1==null?j.Img1:j.SImg1) %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img2 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+j.Img2 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.SImg2==null?j.Img2:j.SImg2) %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img3 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+j.Img3 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.SImg3==null?j.Img3:j.SImg3) %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img4 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+j.Img4 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.SImg4==null?j.Img4:j.SImg4) %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img5 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+j.Img5 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.SImg5==null?j.Img5:j.SImg5) %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img6 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+j.Img6 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.SImg6==null?j.Img6:j.SImg6) %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img7 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+j.Img7 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.SImg7==null?j.Img7:j.SImg7) %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img8 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+j.Img8 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.SImg8==null?j.Img8:j.SImg8)%>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                        </div>

                        <%if (s != null)
                          { %>
                        身份证照片及基地图片：
                        <div class="shopimgbox">
                            <%if (s.CardNoImg1 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+s.CardNoImg1 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+s.CardNoImg1 %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (s.CardNoImg2 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+s.CardNoImg2 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+s.CardNoImg2 %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (s.JDImg1 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+s.JDImg1 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+s.JDImg1 %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (s.JDImg2 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+s.JDImg2 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+s.JDImg2 %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (s.JDImg3 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+s.JDImg3 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+s.JDImg3 %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (s.JDImg4 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+s.JDImg4 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+s.JDImg4 %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                            <%if (s.JDImg5 != null)
                              { %>
                            <ol>
                                <span>
                                    <a href="<%=WebUrl+s.JDImg5 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+s.JDImg5 %>">
                                    </a>
                                </span>
                            </ol>
                            <%} %>
                        </div>
                        <%} %>
                    </li>
                </ul>
            </div>


            <div class="helpTa">
                <h2>帮他实现</h2>
                <%if (j.JZType == 1)
                  { %>
                <p><strong><%=j.Goal %></strong>千克</p>
                <%}
                  else if (j.JZType == 2)
                  { %>
                <p><strong><%=j.Goal %></strong>元</p>
                <%} %>
                <p><span>已有<%=j.rc %>人支持</span>将运用于事件本身及进展</p>
            </div>

            <!--新增内容 begin-->

            <div class="helpTa">
                <h2><i>爱宠筹提示您了解项目之后再帮助TA</i>已提交资料</h2>
                <div class="faguiBox">
                    <ul>
                        <li>
                            <p><strong style="color: #636363;">基地名称：<%=j.NickName %></strong></p>
                            <p><span>基地真实有效</span></p>
                        </li>
                        <%if (j.fqr != null && j.fqr != "")
                          { %>
                        <li>
                            <p><strong style="color: #636363;">项目发起人：<%=j.fqr %></strong></p>
                            <p><span>身份证已提交</span><span>关系证明已提交</span></p>
                        </li>
                        <%} %>
                        <%if (j.jsr != null && j.jsr != "")
                          { %>
                        <li>
                            <p><strong style="color: #636363;">粮（款）接收人：<%=j.jsr %></strong></p>
                            <p><span>身份证已提交</span><span>关系证明已提交</span></p>
                        </li>
                        <%} %>
                    </ul>
                    <p><a href="javascript:;" onclick="window.alert('即将开放');"><span class="zhiying">我要质疑</span></a> 我对该项目由质疑，希望爱宠筹介入调查！</p>
                </div>
            </div>

            <!--新增内容 end-->


            <%if (nlist.Count > 0)
              { %>
            <div class="helpNews">
                <h2>最新动态 <em><%=nlist.Count %>人</em></h2>
                <ul>
                    <%foreach (var n in nlist)
                      {%>
                    <li>
                        <div class="chouTT">
                            <p>
                                <img src="<%=n.HeadImage %>">
                                <strong><%=n.NickName %> <em>发布了进度更新</em></strong>
                                <span><%=n.CreateTime.Value.ToString("MM-dd HH:mm:dd") %></span>
                            </p>
                        </div>
                        <div class="chouText">
                            <p><%=n.Detail %></p>

                            <div class="shopimgbox2">
                                <%if (n.Img1 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img1 %>" data-lightbox="example">
                                            <img src="<%=WebUrl+n.Img1 %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img2 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img2 %>" data-lightbox="example">
                                            <img src="<%=WebUrl+n.Img2 %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img3 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img3 %>" data-lightbox="example">
                                            <img src="<%=WebUrl+n.Img3 %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img4 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img4 %>" data-lightbox="example">
                                            <img src="<%=WebUrl+n.Img4 %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img5 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img5 %>" data-lightbox="example">
                                            <img src="<%=WebUrl+n.Img5 %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img6 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img6 %>" data-lightbox="example">
                                            <img src="<%=WebUrl+n.Img6 %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img7 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img7 %>" data-lightbox="example">
                                            <img src="<%=WebUrl+n.Img7 %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img8 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img8 %>" data-lightbox="example">
                                            <img src="<%=WebUrl+n.Img8 %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                            </div>
                        </div>
                    </li>
                    <% } %>
                </ul>
            </div>
            <%} %>


            <div class="helpTa">
                <h2>他的支持者 <em><%=j.rc %>人</em></h2>
                <div class="comcon">
                    <ul id="listing">
                    </ul>
                </div>
            </div>



        </div>

        <div class="juanbtn">
            <span><a href="chou.aspx"><strong class="shoucang">首页<em></em></strong></a></span>

            <bdo><a href="javascript:;"><strong class="paybtn">审核</strong></a></bdo>

            <span><a href="javascript:;"><strong class="sharetu">删除<em></em></strong></a></span>
        </div>

        <div class="share" id="shareBox">
            <div class="sharebg"></div>
        </div>

        <input type="hidden" id="iid" value="<%=Request.QueryString["id"]==null?Request.QueryString["state"]:Request.QueryString["id"] %>" />
        <input type="hidden" id="ztitle" value="<%=j.Title %>" />
        <input type="hidden" id="ftitle" value="<%=j.Detail %>" />
    </form>
</body>
</html>
<script src="js/jquery.min.js"></script>
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script src="js/jquery.masonry.min.js"></script>
<script src="js/lightbox-2.6.min.js"></script>
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
    //        url: "detail.aspx?para=tj&page=" + num + "&iid=" + $("#iid").val(),
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

    $(".sharetu").click(function () {
        $.ajax({
            type: "POST",
            url: "shenhe2.aspx?para=tj&iid=" + $("#iid").val() + "&zt=2",
            data: {},
            async: false,
            timeout: 15000,
            dataType: "html",
            success: function (data) {

                location = "shenhe1.aspx";

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    });

    //$(".share").click(function () {
    //    $(".share").hide();
    //});

    $(".paybtn").click(function () {
        $.ajax({
            type: "POST",
            url: "shenhe2.aspx?para=tj&iid=" + $("#iid").val() + "&zt=1",
            data: { begtime: $("#txtbegtime").val(), endtime: $("#txtendtime").val() },
            async: false,
            timeout: 15000,
            dataType: "html",
            success: function (data) {

                location = "shenhe1.aspx";

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });

    });
</script>
