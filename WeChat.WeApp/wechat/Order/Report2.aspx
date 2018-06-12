<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report2.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Report2" %>


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
    <link href="css/lightbox.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/GetLocation.js?v=1.0"></script>
    <script src="js/jquery.masonry.min.js"></script>
    <script src="js/lightbox-2.6.min.js"></script>
    <style>
        .petchat2 {
            display: block;
            overflow: hidden;
            max-width: 75rem;
            margin: 0 auto;
            padding-top: 46px;
        }

        .petliang2 {
            border-bottom: 10px solid #eee;
            background: #fff;
            overflow: hidden;
        }

            .petliang2 ol {
                float: left;
                width: 25%;
            }

                .petliang2 ol p {
                    padding: 0px 5px 0px 10px;
                    margin: 10px 0;
                    text-align: left;
                    border-left: 1px dashed #ddd;
                    line-height: 25px;
                    font-family: Arial, Helvetica, sans-serif;
                }

                .petliang2 ol strong {
                    display: block;
                    font-size: 1.8rem;
                    color: #f60;
                }

                    .petliang2 ol strong em {
                        font-size: 12px;
                        font-style: normal;
                        font-weight: normal;
                    }

                .petliang2 ol span {
                    display: block;
                    font-size: 1.4rem;
                    color: #666;
                }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       

        <%for (int i = 1; i < 15; i++)
          {
              List<SPACRM.Entity.Entities.OrderReport> list = nvbo.GetOrderReport6(i);
              if (list.Count > 0)
              {
        %>
          参数：<%=i.ToString() %>
       <%-- <div class="petchat">--%>
            <table style="width:100%;">
                <tr><td style="width:25%;">日期</td><td style="width:25%;">总金额</td><td style="width:25%;">筹粮</td><td style="width:25%;">筹款</td></tr>
            <%--<div class="petliang2">
                <ol>
                    <p><span>日期</span><strong><em></em></strong></p>
                </ol>
                <ol>
                    <p><span>总金额</span><strong><em></em></strong></p>
                </ol>
                <ol>
                    <p><span>筹粮</span><strong><em></em></strong></p>
                </ol>
                <ol>
                    <p><span>筹款</span><strong><em></em></strong></p>
                </ol>
            </div>--%>

            <%
                  foreach (var l in list)
                  {
            %>
                <tr><td><%=l.rq.Substring(2,6) %></td><td><%=Convert.ToInt32(l.je) %></td><td><%=Convert.ToInt32(l.wx) %></td><td><%=Convert.ToInt32(l.zfb) %></td></tr>
           <%-- <div class="petliang2">
                <ol>
                    <p><span></span><strong><%=l.rq.Substring(2,6) %><em></em></strong></p>
                </ol>
                <ol>
                    <p><span></span><strong><%=Convert.ToInt32(l.je) %><em></em></strong></p>
                </ol>
                <ol>
                    <p><span></span><strong><%=Convert.ToInt32(l.wx) %><em></em></strong></p>
                </ol>
                <ol>
                    <p><span></span><strong><%=Convert.ToInt32(l.zfb) %><em></em></strong></p>
                </ol>
            </div>--%>
            <%  } %>
                </table>
       <%-- </div>--%>
        <br />
        <br />
        <%
              }
          } %>


        <br />
        <br />

       
        </div>

        <input type="hidden" id="ztitle" value="帮助可爱的毛孩子们吃上饱饭" />
        <input type="hidden" id="ftitle" value="此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办" />
    </form>
</body>
</html>
<script>

</script>
