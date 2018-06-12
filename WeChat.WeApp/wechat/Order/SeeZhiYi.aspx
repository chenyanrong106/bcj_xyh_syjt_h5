<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeZhiYi.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.SeeZhiYi" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>爱宠筹</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link href="css/detail.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
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
            width:100%;
        }
        .shoucang2{ font-weight:normal; font-size:12px; text-align:center; padding-top:9px; font-family:Arial, Helvetica, sans-serif; background:url(images/xing2.png) no-repeat center 5px; background-size:15px; display:block;}
        .petliang{ border-bottom:10px solid #eee; background:#fff; overflow:hidden;}
.petliang ol{ float:left; width:33.33%;}
.petliang ol p{ padding:0px 5px 0px 10px; margin:10px 0; text-align:left;border-left:1px dashed #ddd; line-height:25px; font-family:Arial, Helvetica, sans-serif;}
.petliang ol strong{ display:block; font-size:1.8rem; color:#f60;}
.petliang ol strong em{ font-size:12px; font-style:normal; font-weight:normal;}
.petliang ol span{ display:block; font-size:1.4rem; color:#666;}
.shopdd{ border-top:1px solid #eee;}


/*橙色底的头部*/
.acc-top-header-cheng{background:#ff7200;}
.acc-top-header-cheng ol strong{ font-size:1.8rem; line-height:44px; display:block; color:#fff; font-weight:normal;}
.acc-top-header-cheng .wodeicon{ background:url(images/wode_baise.png) no-repeat 0 center; background-size:25px; font-size:1.4rem; color:#fff; padding-left:30px; display:inline-block;}
.acc-top-header-cheng .fanhui{ background:url(images/fanhui_baise.png) no-repeat 0 center; background-size:25px; font-size:1.4rem; color:#fff; padding-left:30px; display:inline-block;}
/*header end*/
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="acc-petchat">
            <%if(j!=null){ %>
            <div class="shoplist">
               <%-- <ul>
                    <li>--%>
                        <div class="shopTT">
                            <h2>姓名：<%=j.Name %></h2>
                             <h2>手机：<%=j.Phone %></h2>
                             <h2>身份证：<%=j.CardNo %></h2>
                             <h2>邮箱：<%=j.EMail %></h2>
                            <h2>项目编号：<%=j.JID %></h2>
                            <h2>基地名称：<%=j.JNickName %></h2>
                             <h2>时间：<%=j.CreateTime.Value.ToString("yyyy.MM.dd HH:mm:ss") %></h2>
                            <p>理由：<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=j.Detail.Replace("\r","<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("。","。<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("？","？<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("！","！<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace(".",".<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("?","?<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("!","!<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") %></p>
                        </div>
                        <div class="shopimgbox">
                            <%if (j.Img1 != null)
                              { %>
                            <%--<ol>
                                <span>--%>
                                    <a href="<%=WebUrl+j.Img1 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.Img1==null?j.Img1:j.Img1) %>">
                                    </a>
                               <%-- </span>
                            </ol>--%>
                            <%} %>
                            <%if (j.Img2 != null)
                              { %>
                            <%--<ol>
                                <span>--%>
                                    <a href="<%=WebUrl+j.Img2 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.Img2==null?j.Img2:j.Img2) %>">
                                    </a>
                              <%--  </span>
                            </ol>--%>
                            <%} %>
                            <%if (j.Img3 != null)
                              { %>
                           <%-- <ol>
                                <span>--%>
                                    <a href="<%=WebUrl+j.Img3 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.Img3==null?j.Img3:j.Img3) %>">
                                    </a>
                               <%-- </span>
                            </ol>--%>
                            <%} %>
                            <%if (j.Img4 != null)
                              { %>
                           <%-- <ol>
                                <span>--%>
                                    <a href="<%=WebUrl+j.Img4 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.Img4==null?j.Img4:j.Img4) %>">
                                    </a>
                              <%--  </span>
                            </ol>--%>
                            <%} %>
                            <%if (j.Img5 != null)
                              { %>
                            <%--<ol>
                                <span>--%>
                                    <a href="<%=WebUrl+j.Img5 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.Img5==null?j.Img5:j.Img5) %>">
                                    </a>
                               <%-- </span>
                            </ol>--%>
                            <%} %>
                            <%if (j.Img6 != null)
                              { %>
                           <%-- <ol>
                                <span>--%>
                                    <a href="<%=WebUrl+j.Img6 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.Img6==null?j.Img6:j.Img6) %>">
                                    </a>
                               <%-- </span>
                            </ol>--%>
                            <%} %>
                            <%if (j.Img7 != null)
                              { %>
                           <%-- <ol>
                                <span>--%>
                                    <a href="<%=WebUrl+j.Img7 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.Img7==null?j.Img7:j.Img7) %>">
                                    </a>
                               <%-- </span>
                            </ol>--%>
                            <%} %>
                            <%if (j.Img8 != null)
                              { %>
                           <%-- <ol>
                                <span>--%>
                                    <a href="<%=WebUrl+j.Img8 %>" data-lightbox="example">
                                        <img src="<%=WebUrl+(j.Img8==null?j.Img8:j.Img8)%>">
                                    </a>
                              <%--  </span>
                            </ol>--%>
                            <%} %>
                        </div>
                  <%--  </li>
                </ul>--%>
            </div>
            <%} %>
        </div>

        <div class="share" id="shareBox">
            <div class="sharebg"></div>
        </div>
    </form>
</body>
</html>

