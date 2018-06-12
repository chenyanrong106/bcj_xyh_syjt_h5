<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>爱宠筹</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <%--<link type="text/css" rel="stylesheet" href="css/meike.css?v=2" />--%>
  
    <link href="css/maindetail.css" rel="stylesheet" />
    <link href="css/lightbox.css" rel="stylesheet" />  
    <link href="css/pinglun.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/oa.js?v=2"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/GetLocation.js?v=1.0"></script>
    <script src="js/jquery.masonry.min.js"></script>
    <script src="js/lightbox-2.6.min.js"></script>
    <script src="js/echo.js"></script>
    <script src="js/Message2.js"></script>
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

        .shoucang2 {
            font-weight: normal;
            font-size: 12px;
            text-align: center;
            padding-top: 9px;
            font-family: Arial, Helvetica, sans-serif;
            background: url(images/xing2.png) no-repeat center 5px;
            background-size: 15px;
            display: block;
        }

        .petliang {
            border-bottom: 10px solid #eee;
            background: #fff;
            overflow: hidden;
        }

            .petliang ol {
                float: left;
                width: 33.33%;
            }

                .petliang ol p {
                    padding: 0px 5px 0px 10px;
                    margin: 10px 0;
                    text-align: left;
                    border-left: 1px dashed #ddd;
                    line-height: 25px;
                    font-family: Arial, Helvetica, sans-serif;
                }

                .petliang ol strong {
                    display: block;
                    font-size: 1.8rem;
                    color: #f60;
                }

                    .petliang ol strong em {
                        font-size: 12px;
                        font-style: normal;
                        font-weight: normal;
                    }

                .petliang ol span {
                    display: block;
                    font-size: 1.4rem;
                    color: #666;
                }

        .shopdd {
            border-top: 1px solid #eee;
        }


        /*橙色底的头部*/
        .acc-top-header-cheng {
            background: #ff7200;
        }

            .acc-top-header-cheng ol strong {
                font-size: 1.8rem;
                line-height: 44px;
                display: block;
                color: #fff;
                font-weight: normal;
            }

            .acc-top-header-cheng .wodeicon {
                background: url(images/wode_baise.png) no-repeat 0 center;
                background-size: 25px;
                font-size: 1.4rem;
                color: #fff;
                padding-left: 30px;
                display: inline-block;
            }

            .acc-top-header-cheng .fanhui {
                background: url(images/fanhui_baise.png) no-repeat 0 center;
                background-size: 25px;
                font-size: 1.4rem;
                color: #fff;
                padding-left: 30px;
                display: inline-block;
            }

            .acc-top-header-cheng .xiangicon {
                background: url(images/xg.png) no-repeat 0 center;
                background-size: 25px;
                font-size: 1.4rem;
                color: #fff;
                padding-left: 30px;
                display: inline-block;
            }
        /*header end*/
    </style>
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
</head>
<body>
    <form id="form1" runat="server">
        <% string openid = Session["FromUserName"] == null ? "" : Session["FromUserName"].ToString(); %>
        <div class="acc-top-header acc-top-header-cheng" id="toubu" style="border-bottom: 1px solid #eee;">
            <ol class="width20" onclick="window.location='chou.aspx?p=<%=Request.QueryString["p"]??"1" %>'"><span class="fanhui">返回</span></ol>
            <ol class="width60"><strong>爱宠筹</strong></ol>
            <%if ((openid != null && j.FromUserName == openid&&!string.IsNullOrEmpty(j.FromUserName)) || openid == "oloJGv4lWL-TS8yn8uo4Fu1rbVTI" || openid == "oloJGvzwelDQjIKMMWC8Z6ngM7gk" || openid == "oloJGvx3hLkCNadLGIpAMimz2Xwc")
              { %>
            <ol class="width20"><span class="xiangicon">管理</span></ol>
            <%}
              else
              { %>
            <ol class="width20" onclick="window.location='zhiyi.aspx?jid=<%=j.ID %>&p=<%=Request.QueryString["p"]??"1" %>&jname=<%=j.NickName %>'"><span class="wodeicon">举报</span></ol>
            <%} %>
            <%--<ol class="width20"><span class="xiangicon"></span></ol>--%>
        </div>
        <%if (j.JZType == 2)
          { %>

        <div class="qsc-alert-static" style="color: #F25B4B; position: relative; background: #fff; border-left: 3px solid #F25B4B!important; font-size: 12px; display: block; overflow: hidden; max-width: 75rem; margin: 0 auto; margin-bottom: -40px; margin-top: 44px;">筹款爱心项目，平台暂时不收取任何手续费用</div>
        <%} %>
        <div class="acc-petchat">

            <%-- <%if (j.JZType == 1)
              { %>
            <div class="acc-petliang">
                <ol>
                    <p><span>参与人次</span><strong><%=j.rc %><em>次</em></strong></p>
                </ol>
                <ol>
                    <p><span>已筹宠粮</span><strong><%=Math.Round(j.je.Value/16,2) %><em>Kg</em></strong></p>
                </ol>
                <ol>
                    <p><span>目标狗粮</span><strong><%=j.Goal %><em>Kg</em></strong></p>
                </ol>
            </div>
            <%}
              else if (j.JZType == 2)
              { %>
            <div class="acc-petliang">
                <ol>
                    <p><span>参与人次</span><strong><%=j.rc %><em>次</em></strong></p>
                </ol>
                <ol>
                    <p><span>已筹金额</span><strong><%=Math.Round(j.je.Value,2) %><em>元</em></strong></p>
                </ol>
                <ol>
                    <p><span>目标金额</span><strong><%=j.Goal %><em>元</em></strong></p>
                </ol>
            </div>
            <%} %>--%>

            <div class="shoplist">
                <ul>
                    <li>
                        <div class="adminInfo">
                            <p>
                                <span>剩余<em><%=j.EndTime<DateTime.Now?0:(j.EndTime.Value-DateTime.Now).Days+1 %></em>天</span><bdo class="touxiang">
                                    <img src="<%=j.HeadImage %>"></bdo>
                                <%if(j.ID==382){ %>
                                爱宠筹官方
                                <%}else{ %>
                                <%=j.NickName %> 
                                <%} %>
                                <em></em>
                            </p>
                        </div>

                        <div class="petliang shopdd">
                            <%if (j.JZType == 1)
                              { %>
                            <ol>
                                <p><strong><%=j.Goal %><em>千克</em></strong><span>目标狗粮</span></p>
                            </ol>
                            <ol>
                                <p><strong><%=Math.Round(j.je.Value/16,2) %><em>千克</em></strong><span>已筹狗粮</span></p>
                            </ol>
                            <ol>
                                <p><strong><%=j.rc %><em>次</em></strong><span>支持次数</span></p>
                            </ol>
                            <%}
                              else if (j.JZType == 2)
                              { %>
                            <ol>
                                <p><strong><%=j.Goal %><em>元</em></strong><span>目标金额</span></p>
                            </ol>
                            <ol>
                                <p><strong><%=Math.Round(j.je.Value,2) %><em>元</em></strong><span>已筹金额</span></p>
                            </ol>
                            <ol>
                                <p><strong><%=j.rc %><em>次</em></strong><span>支持次数</span></p>
                            </ol>
                            <%} %>
                        </div>



                        <div class="shopTT">
                            <h2><%=j.Title %></h2>
                            <p><%=j.Detail %></p>
                        </div>
                        <div class="shopimgbox">
                            <%if (j.Img1 != null)
                              { %>
                            <ol>
                                <span>
                                    <%--<a href="<%=WebUrl+j.Img1 %>" data-lightbox="example">--%>
                                        <img data-echo="<%=WebUrl+(j.SImg1==null?j.Img1:j.SImg1) %>" alt="<%=WebUrl+j.Img1 %>">
                                   <%-- </a>--%>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img2 != null)
                              { %>
                            <ol>
                                <span>
                                   <%-- <a href="<%=WebUrl+j.Img2 %>" data-lightbox="example">--%>
                                        <img data-echo="<%=WebUrl+(j.SImg2==null?j.Img2:j.SImg2) %>" alt="<%=WebUrl+j.Img2 %>">
                                    <%--</a>--%>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img3 != null)
                              { %>
                            <ol>
                                <span>
                                    <%--<a href="<%=WebUrl+j.Img3 %>" data-lightbox="example">--%>
                                        <img data-echo="<%=WebUrl+(j.SImg3==null?j.Img3:j.SImg3) %>" alt="<%=WebUrl+j.Img3 %>">
                                    <%--</a>--%>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img4 != null)
                              { %>
                            <ol>
                                <span>
                                    <%--<a href="<%=WebUrl+j.Img4 %>" data-lightbox="example">--%>
                                        <img data-echo="<%=WebUrl+(j.SImg4==null?j.Img4:j.SImg4) %>" alt="<%=WebUrl+j.Img4 %>">
                                    <%--</a>--%>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img5 != null)
                              { %>
                            <ol>
                                <span>
                                   <%-- <a href="<%=WebUrl+j.Img5 %>" data-lightbox="example">--%>
                                        <img data-echo="<%=WebUrl+(j.SImg5==null?j.Img5:j.SImg5) %>" alt="<%=WebUrl+j.Img5 %>">
                                    <%--</a>--%>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img6 != null)
                              { %>
                            <ol>
                                <span>
                                    <%--<a href="<%=WebUrl+j.Img6 %>" data-lightbox="example">--%>
                                        <img data-echo="<%=WebUrl+(j.SImg6==null?j.Img6:j.SImg6) %>" alt="<%=WebUrl+j.Img6 %>">
                                    <%--</a>--%>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img7 != null)
                              { %>
                            <ol>
                                <span>
                                  <%--  <a href="<%=WebUrl+j.Img7 %>" data-lightbox="example">--%>
                                        <img data-echo="<%=WebUrl+(j.SImg7==null?j.Img7:j.SImg7) %>" alt="<%=WebUrl+j.Img7 %>">
                                    <%--</a>--%>
                                </span>
                            </ol>
                            <%} %>
                            <%if (j.Img8 != null)
                              { %>
                            <ol>
                                <span>
                                    <%--<a href="<%=WebUrl+j.Img8 %>" data-lightbox="example">--%>
                                        <img data-echo="<%=WebUrl+(j.SImg8==null?j.Img8:j.SImg8)%>" alt="<%=WebUrl+j.Img8 %>">
                                   <%-- </a>--%>
                                </span>
                            </ol>
                            <%} %>
                        </div>
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
                        <%if (j.ISJD == 1)
                          { %>
                        <li>
                            <p><strong style="color: #636363;">基地名称：<%=j.NickName %></strong></p>
                            <p><span>基地真实有效</span></p>
                        </li>
                        <%}
                          else
                          { %>
                        <li>
                            <p><strong style="color: #636363;">个人名称：<%=j.NickName %></strong></p>
                            <p><span>个人信息有效</span></p>
                        </li>
                        <%} %>
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
                    <p><a href="javascript:;" onclick="window.location='zhiyi.aspx?jid=<%=j.ID %>&p=<%=Request.QueryString["p"]??"1" %>&jname=<%=j.NickName %>'"><span class="zhiying">我要质疑</span></a> 我对该项目有质疑，希望爱宠筹介入调查！</p>
                </div>
            </div>

            
<%--         <div class="acc-login-input">
                <ul>

                    <li>
                        <%if (j.JZType == 1)
                          { %>
                        <p class="shuoming"><i class="checkbox checked"></i>已阅读并同意<a href="wd5.aspx" class="yelllow">《宠物粮款捐赠服务协议》</a></p>
                        <%} 
                          else{%>
                          <p class="shuoming"><i class="checkbox checked"></i>已阅读并同意<a href="wd6.aspx" class="yelllow">《友宠爱心捐赠服务协议》</a></p>
                        <%} %>
                    </li>
                </ul>
            </div>--%>

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

                            <div class="shopimgbox">
                                <%if (n.Img1 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img1 %>" data-lightbox="example">
                                            <img data-echo="<%=WebUrl+(n.SImg1??n.Img1) %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img2 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img2 %>" data-lightbox="example">
                                            <img data-echo="<%=WebUrl+(n.SImg2??n.Img2) %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img3 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img3 %>" data-lightbox="example">
                                            <img data-echo="<%=WebUrl+(n.SImg3??n.Img3) %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                           <%-- </div>
                            <div class="shopimgbox2">--%>
                                <%if (n.Img4 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img4 %>" data-lightbox="example">
                                            <img data-echo="<%=WebUrl+(n.SImg4??n.Img4) %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img5 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img5 %>" data-lightbox="example">
                                            <img data-echo="<%=WebUrl+(n.SImg5??n.Img5) %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img6 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img6 %>" data-lightbox="example">
                                            <img data-echo="<%=WebUrl+(n.SImg6??n.Img6) %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img7 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img7 %>" data-lightbox="example">
                                            <img data-echo="<%=WebUrl+(n.SImg7??n.Img7) %>">
                                        </a>
                                    </span>
                                </ol>
                                <%} %>
                                <%if (n.Img8 != null)
                                  { %>
                                <ol>
                                    <span>
                                        <a href="<%=WebUrl+n.Img8 %>" data-lightbox="example">
                                            <img data-echo="<%=WebUrl+(n.SImg8??n.Img8) %>">
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
                        <%if (j.pj != 0)
                          { %>
                        <li>
                            <p>
                                <img src="http://SPACRM.meijiewd.com/assets/images/logo.jpg">
                                爱宠筹官方赞助配捐了<strong><%=Math.Round(j.pj.Value/16,2) %>千克</strong>
                            </p>
                            <p><bdo style="width: 100%;"><%=DateTime.Now.ToString("yyyy-MM-dd") %></bdo></p>
                            <p>
                                <bdo class="txtinfo" style="width: 100%;">支持！</bdo>
                            </p>
                        </li>
                        <%} %>
                    </ul>
                </div>
            </div>



        </div>

        <div class="juanbtn">
            <span><a href="javascript:;" onclick="star(this,<%=j.ID %>);"><%if (isstar == 0)
                                                                            { %><strong class="shoucang">关注<em></em></strong><%}
                                                                            else
                                                                            { %><strong class="shoucang2">已关注<em></em></strong><%} %></a></span>
            <%if ((j.EndTime.Value - DateTime.Now).Days + 1 > 0&&DateTime.Now>j.BegTime)
              { %>
            <bdo><a href="pay.aspx?id=<%=j.ID %>&p=<%=Request.QueryString["p"]??"1" %>"><strong class="paybtn">我要支持</strong></a></bdo>
            <%}
              else
              { %>
            <bdo><a href="javascript:;"><strong class="paybtn" style="background-color: gray;">已结束</strong></a></bdo>
            <%} %>
            <span><a href="javascript:;"><strong class="sharetu">分享<em></em></strong></a></span>
        </div>


        <div class="share" id="shareBox">
            <div class="sharebg"></div>
        </div>

        <div class="acc-xiangmu">
            <div class="acc-xiangmu-con">
                <h2>项目管理</h2>
                <div class="acc-xiangbox">
                    <%if (openid == "oloJGv4lWL-TS8yn8uo4Fu1rbVTI" || openid == "oloJGvzwelDQjIKMMWC8Z6ngM7gk" || openid == "oloJGvx3hLkCNadLGIpAMimz2Xwc")
                      { %>
                    <ol onclick="window.location='editchou.aspx?id=<%=j.ID %>'">
                        <span class="editIcon"></span>
                        <p>编辑项目</p>
                    </ol>
                    <%} %>
                    <ol onclick="window.location='AddNews.aspx?id=<%=j.ID %>'">
                        <span class="updateIcon"></span>
                        <p>更新动态</p>
                    </ol>
                </div>
                <div class="submitBtn">取消</div>
            </div>
        </div>

        <div id="tanceng" class="tanceng">
            <div class="maskbg" onClick="javascript:closedLogin();" style="z-index:999980;"></div>
    <div class="login-lrlz-box">
        <h2>发表评论</h2>
        <div class="login-lrlz-input">
		  <div class="login-pinglunceng"><textarea name="" cols="" rows="" id="txtpl" maxlength="200" placeholder="请输入回复内容，不超过100个字"></textarea></div>
        </div>
		<div class="login-lrlz-btn"><ol><span class="quxiaobtn" onClick="javascript:closedLogin();">取消</span></ol><ol><span id="subbtn" onclick="pl();" class="subbtn">写好了</span></ol></div>
</div>
        </div>

        <input type="hidden" id="iid" value="<%=Request.QueryString["id"]==null?Request.QueryString["state"]:Request.QueryString["id"] %>" />
        <input type="hidden" id="ztitle" value="<%=j.Title %>" />
        <input type="hidden" id="ftitle" value="<%=j.Detail %>" />
        <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />
        <input type="hidden" id="jztype" value="<%=j.JZType %>" />
        <input type="hidden" id="oid" value="0" />
    </form>
</body>
</html>

<script>
    echo.init({
        offset: 100,
        throttle: 250, unload: false
    });

    var flag = true;
    var $container = $('#listing');
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
        var $boxes = $(getList());
        $container.append($boxes);
    }
    function getList() {
        var boxes = [];
        $.ajax({
            type: "get",
            url: "detail.aspx?para=tj&page=" + num + "&iid=" + $("#iid").val() + "&jztype=" + $("#jztype").val(),
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

    $(".sharetu").click(function () {
        $("#toubu").hide();
        $(".share").show();
    });

    $(".share").click(function () {
        $("#toubu").show();
        $(".share").hide();
    });

    function star(obj, jid) {
        $.ajax({
            type: "get",
            url: "detail.aspx?para=star&jid=" + jid,
            dataType: "json",
            success: function (data, textStatus) {
                if (data.st == 3) {
                    var a = window.location.toString();
                    location = "bangphone.aspx?beforeurl2=" + encodeURI(a.replace("&", "*"));
                }
                else if (data.st == 1) {  //关注成功
                    $(obj).find("strong").removeClass("shoucang").addClass("shoucang2");
                    $(obj).find("strong").html("已关注<em></em>");
                }
                else if (data.st == 2) { //取消关注成功
                    $(obj).find("strong").removeClass("shoucang2").addClass("shoucang");
                    $(obj).find("strong").html("关注<em></em>");
                }
            }
        });

    }

    $('.xiangicon').on('click', function () {
        $('.acc-xiangmu').animate({ "height": "200px" }, 300);
    })
    $('.submitBtn').on('click', function () {
        $('.acc-xiangmu').animate({ "height": "0px" }, 300);
    })


    function getImg(obj) {
        var m = [];
        for (var i = 0; i < obj.length; i++) {
            m[i] = obj[i].alt;
        }

        return m;
    }

    //imgClick($("#ppppc img"));
    imgClick($(".shopimgbox img"));

    function imgClick(obj) {
        $(obj).click(function () {
            var src = getImg(obj);
            var curent = $(this).index();
            preview(src[curent], src);
        });
        function preview(current, urls) {
            console.log(urls);
            wx.previewImage({
                current: current, // 当前显示图片的http链接
                urls: urls// 需要预览的图片http链接列表
            });
        }


    }

    function layerpl(id) {
        $("#oid").val(id);
        $("#tanceng").show();
    }

    function closedLogin() {
        $("#tanceng").hide();
    }

    function pl() {
        if ($("#txtpl").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请输入评论内容");
        }
        else {
            $.ajax({
                type: "get",
                url: "detail.aspx?para=pl&oid=" + $("#oid").val() + "&jid=" + $("#iid").val(),
                data: { detail: $("#txtpl").val() },
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.st == 3) {
                        var a = window.location.toString();
                        location = "bangphone.aspx?beforeurl2=" + encodeURI(a.replace("&", "*"));
                    }
                    else if (data.st == 1) {  //关注成功
                        $('#aipet-pinglun-box' + $("#oid").val()).append(data.msg);
                        $("#txtpl").val("");
                        closedLogin();
                    }
                    else if (data.st == 2) { //取消关注成功
                        $.MsgBox.Alert("爱宠筹", data.msg, function () { $("#tanceng").hide(); });
                    }
                }
            });
        }
    }
</script>
