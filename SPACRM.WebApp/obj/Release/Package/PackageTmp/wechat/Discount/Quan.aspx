<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quan.aspx.cs" Inherits="SPACRM.WebApp.wechat.Discount.Quan" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/youhuiquan.css" rel="stylesheet" />
    <title>优惠券</title>

</head>

<body>
    <div class="youhuiquan-banner">
        <img src="imageS/bigimg.jpg">
    </div>
    <div class="quan-all-waper">
        <%if (d != null)
          { %>
        <div class="quan-left-waper">
            <div class="quan-left-moneny"><em>￥</em><%=d.Money %></div>
        </div>
        <div class="quan-right-waper">
            <h2><%=d.Name %></h2>
            <h3><%=d.Detail %></h3>
            <p>有效期：<%=d.Validity.Value.ToString("yyyy.MM.dd") %></p>
        </div>
        <%} %>
    </div>

    <div class="quan-content-box">
        <div id="input-admin-box">
            <input name="phone" id="phone" type="tel" maxlength="11" class="inputText" placeholder="请输入您的手机号领取优惠券">
            <div class="submit-btn">立即领取</div>
            <div style="text-align:center;width:100%;margin-top: -15px;">
            <a href="../spa/newindex.aspx" style="color:red;    text-decoration: underline;">立即预约</a></div>
        </div>
        <div class="quan-admin-info">
            <p>优惠券领取成功</p>
            <a href="../spa/newindex.aspx">
                <div class="submit-kongbtn">立即使用</div>
            </a>
        </div>



        <div class="quan-title-box"><strong class="width80">查看适用门店</strong></div>
        <div class="pengyou-list" id="mendian">
            <ul>
                <%foreach (var s in list)
                  {
                %>
                <li>
                    <span><a href="tel:<%=s.TELEPHONE %>"><em class="dianhua"></em></a></span>
                    <strong class="mendian" style="font-weight: normal; font-size: 14px;"><%=s.NAME %></strong>
                    <p class="address"><%=s.ADDRESS %></p>
                </li>
                <%
                  } %>
            </ul>
            <div class="moreDian">点击查看更多 <bdo></bdo>家适用门店</div>
        </div>



        <div class="quan-title-box"><strong class="width70">使用说明</strong></div>
        <div class="guize-detail">
            <p><em>1、</em>本券只适用于公众号小时光SPA中的预约支付抵扣。</p>
            <p><em>2、</em>本券不得兑换现金不设找零，代金券不会再返回。</p>
            <p><em>3、</em>每个订单只能使用一张代金券，且不得与其他优惠方式同时使用。</p>
            <%if (d != null)
              { %>
            <p><em>4、</em>本券有效期截止到<%=d.Validity.Value.ToString("yyyy.MM.dd") %></p>
            <%} %>
            <p>最终解释权归小时光spa所有</p>

        </div>

        <div class="quan-title-box"><strong class="width70">商户图文</strong></div>
        <div class="detialpp">
            <p>
                <img src="imageS/1.jpg" />
            </p>
            <p>
                <img src="imageS/2.jpg" />
            </p>
            <p>
                <img src="imageS/3.jpg" />
            </p>
            <%-- <p>
                <img src="imageS/4.jpg" />
            </p>--%>
            <p>
                <img src="imageS/5.jpg" />
            </p>
            <p>
                <img src="imageS/6.jpg" />
            </p>

        </div>



    </div>

    <input type="hidden" id="disguid" value="<%=Request.QueryString["guid"] %>" />
    <%if (d != null)
      { %>
    <input type="hidden" id="ztitle" value="<%=d.Name %>" />
    <%} %>
    <input type="hidden" id="ftitle" value="身享受，心悠然。一段『小时光』，遇见更好的自己。" />
    <input type="hidden" value="<%=Server.UrlEncode(AbsoluteUri)%>" id="url" />
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script src="js/Message2.js"></script>
<script>

    //信息提示
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
    var patrn = /^0?(13[0-9]|15[0-9]|17[0-9]|18[0-9]|14[0-9])[0-9]{8}$/;


    var flag = true;
    $('.submit-btn').on('click', function () {
        if (flag == true) {
            var iphone = document.getElementById('phone').value;
            if (iphone == "") {
                tips('<p>请输入手机号码！</p>')
            } else {
                if (patrn.test(iphone)) {

                    $.ajax({
                        type: "Get",
                        url: "quan.aspx?para=lq&disguid=" + $("#disguid").val() + "&phone=" + $("#phone").val(),
                        dataType: 'json',
                        async: false,
                        beforeSend: function () {

                        },
                        success: function (data) {
                            if (data.st == -1) {
                                $.MsgBox.Alert("小时光", data.msg, function () {
                                    location = "../spa/newindex.aspx";
                                });
                            }
                            else {
                                $.MsgBox.Alert("小时光", data.msg, function () {
                                    $('#input-admin-box').hide();
                                    $('.quan-admin-info').show();
                                    flag = false;
                                });

                            }

                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });

                } else {
                    tips('<p>手机号码不对</p>');
                }
            }

        }
    })

    function xiugai() {
        document.getElementById('phone').value = '';
        $('#input-admin-box').show();
        $('.quan-admin-info').hide();
        flag = true;
    }


    $('.moreDian').find('bdo').html($('#mendian ul li').length);
    $('.moreDian').on('click', function () {
        $('#mendian ul').css({ 'height': 'auto' });
    })


</script>
