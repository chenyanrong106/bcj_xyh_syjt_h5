<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sueccss.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.sueccss" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/main.css" rel="stylesheet" />
    <title>成功捐赠</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="acc-login-box acc-wo-touxing acc-suc-touxing">
            <%if (o != null)
              { %>
            <div class="acc-login-touxiang">
                <img src="<%=o.headimgurl %>">
            </div>
            <div class="acc-login-name"><%=o.Nickname %></div>
            <%} %>
            <%if (jz != null)
              { %>
            <div class="acc-login-info">
                <p>
                    感谢您支持 <%=jz.NickName %><br />
                    本次共捐助 <%if (jz.JZType == 2)
                            { %><%=order.TotalPrice %>元<%}
                            else
                            { %><%=Math.Round(order.TotalPrice.Value/16,3) %>Kg<%} %>
                </p>
            </div>
            <%} %>
        </div>
        <%if (order != null)
          { %>
        <div class="acc-login-input acc-suc-result">

            <%if (order.PJ > 0)
              { %>
            <div class="acc-suc-padding">
                <h2>您的爱心已被放大</h2>
                <p>宠物管家 为您的爱心配捐<bdo><%=Math.Round(order.PJ.Value/16,3) %>Kg</bdo></p>
            </div>
            <%} %>

            <ul>
                <li>
                    <p class="liuyanzishu"><span id="zishu"></span><strong>留言支持</strong></p>
                    <p>
                        <textarea id="weibo" name="" cols="" rows="" class="dropdown-area dropdown-area2" maxlength="200" placeholder="支持！"><%=order.CourierRemark %></textarea>
                    </p>
                </li>
                <li class="padd10" <%if(!string.IsNullOrEmpty(order.CourierRemark)){ %>style="display:none;"<%} %>>
                    <div class="submitBtn">提交留言</div>
                </li>

            </ul>

            <p class="acc-suc-bp"><a href="chou.aspx?p=<%=Request.QueryString["p"]??"1" %>"><span>返回首页</span></a> | <a href="detail.aspx?id=<%=order.Source %>&p=<%=Request.QueryString["p"]??"1" %>"><span>返回该项目</span></a></p>
        </div>
        <%} %>
        <% if (Request.QueryString["oid"] != null)
           {
               string[] oids = Request.QueryString["oid"].Split(',');
               if (oids.Length > 2)
               {%>
        <input type="hidden" id="oid" value="<%=oids[0] %>" />
        <input type="hidden" id="orderno" value="<%=oids[2]%>" />
        <input type="hidden" id="urlpara" value="<%=oids[1] %>" />
        <%}
           }
           else
           { %>
         <input type="hidden" id="oid" value="0" />
        <input type="hidden" id="orderno" value="1" />
        <input type="hidden" id="urlpara" value="1" />
        <%} %>
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

    $(function () {
        var zongNum = 200;
        $("#zishu").text('你可以输入' + zongNum + '个字');
        $("#weibo").keyup(function () {
            var len = $(this).val().length;
            if (len <= zongNum) {
                var num = zongNum - len;
                $("#zishu").text(num + '/' + zongNum);
            } else {
                tips('亲，您输入的字数超过了');
                $("#zishu").text(0 + '/' + zongNum);
                $(this).val($(this).val().substring(0, zongNum));
            }
        });

        $('.submitBtn').on('click', function () {
            //var zonglen = $("#weibo").val().length;
            //if (zonglen > zongNum) {
            //    tips('<p>亲，您输入的字数超过了' + zongNum + '字</p>');
            //}
            $(".submitBtn").hide();
            $.post("sueccss.aspx?para=tj", {
                remark: $("#weibo").val(),
                oid: $("#oid").val(),
                orderno: $("#orderno").val()
            },
                            function (ret) {
                                $(".submitBtn").show();
                                if (ret.st == 1) {
                                    $.MsgBox.Alert("爱宠筹", ret.msg);
                                }
                                else {
                                    $.MsgBox.Alert("爱宠筹", ret.msg, function () {
                                        window.location = "chou.aspx?p="+$("#urlpara").val();
                                    });
                                }
                            },
                            "json"
                      );
        })
    });

    function onBridgeReady() {
        WeixinJSBridge.call('hideOptionMenu');
    }

    if (typeof WeixinJSBridge == "undefined") {
        if (document.addEventListener) {
            document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
        } else if (document.attachEvent) {
            document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
            document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
        }
    } else {
        onBridgeReady();
    }
</script>
