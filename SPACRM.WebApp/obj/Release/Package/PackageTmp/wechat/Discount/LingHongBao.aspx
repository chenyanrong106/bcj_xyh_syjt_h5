<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LingHongBao.aspx.cs" Inherits="SPACRM.WebApp.wechat.Discount.LingHongBao" %>


<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/youhuiquan.css" rel="stylesheet" />
    <title>小时光SPA</title>

</head>

<body>
    <div class="youhuiquan-banner">
        <img src="images/bigimg.jpg">
    </div>
    <%if (hb != null)
      { %>
    <div class="quan-all-waper<%=hb.mnum>=1?" quan-huise":hb.znum>=10?" quan-huise":"" %>">

        <div class="quan-left-waper">
            <div class="quan-left-moneny"><em>￥</em>1~3</div>
        </div>
        <div class="quan-right-waper">
            <h2>小时光红包</h2>
            <h3>下单可直接抵扣，可累计使用。</h3>
            <p>有效期：永久</p>
        </div>
    </div>
    <%} %>

    <div class="quan-content-box">
        <div id="input-admin-box">
            <%-- <input name="phone" id="phone" type="tel" maxlength="11" class="inputText" placeholder="请输入您的手机号领取优惠券">--%>
            <%if (hb != null && hb.znum < 10 && hb.mnum < 1)
              { %>
            <div class="submit-btn">立即领取</div>
            <%} %>
            <%if(hb!=null&&hb.znum>=10&&hb.mnum<1){ %>
            <div style="text-align: center; width: 100%; margin-top: -15px;">
                <a href="../spa/newindex.aspx" style="color: red; text-decoration: underline;font-size:16px;">立即预约</a>
            </div>
            <%} %>
        </div>
        <%if(hb!=null&&hb.mnum>=1){ %>
        <div class="quan-admin-info" style="display:block;">
            <p>红包已放入账户，可关注小时光并在账户中查看。</p>
            <a href="../spa/newindex.aspx">
                <div class="submit-kongbtn">立即使用</div>
            </a>
        </div>
        <%} else if(hb!=null&&hb.znum>=10&&hb.mnum<1){ %>
        <div class="quan-admin-info" style="display:block;">
            <p>红包已抢完，下次早点来。</p>
            <a href="../spa/newindex.aspx">
                <div class="submit-kongbtn">立即预约</div>
            </a>
        </div>
        <%} %>

        <div class="quan-title-box"><strong>看看朋友们的手气如何</strong></div>
        <div class="pengyou-list">
            <ul>
                <%foreach (var c in CardList)
                  {
                      %>
                <li>
                    <span>￥<%=c.AMT %></span><img src="<%=c.orderno %>"><strong><%=c.Remark %><i><%=c.CreateTime.Value.ToString("MM.dd HH:mm") %></i></strong>拼人品的时候到了！
                </li>
                <%
                  } %>
               
            </ul>
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
            <p><em>1、</em>红包只适用于公众号小时光SPA中的预约支付抵扣。</p>
            <p><em>2、</em>红包不得兑换现金不设找零，代金券不会再返回。</p>
            <p>最终解释权归小时光spa所有</p>

        </div>

        <div class="quan-title-box"><strong class="width70">商户图文</strong></div>
        <div class="detialpp">
            <p>
                <img src="images/1.jpg" />
            </p>
            <p>
                <img src="images/2.jpg" />
            </p>
            <p>
                <img src="images/3.jpg" />
            </p>
            <%-- <p>
                <img src="images/4.jpg" />
            </p>--%>
            <p>
                <img src="images/5.jpg" />
            </p>
            <p>
                <img src="images/6.jpg" />
            </p>

        </div>



    </div>

    <input type="hidden" id="disguid" value="<%=Request.QueryString["id"] %>" />

    <input type="hidden" id="ztitle" value="快来领红包" />

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
            //var iphone = document.getElementById('phone').value;
            //if (iphone == "") {
            //    tips('<p>请输入手机号码！</p>')
            //} else {
            //    if (patrn.test(iphone)) {

            $.ajax({
                type: "Get",
                url: "linghongbao.aspx?para=lq&disguid=" + $("#disguid").val(),
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
                            //$('#input-admin-box').hide();
                            //$('.quan-admin-info').show();
                            //flag = false;
                            location = location;
                        });

                    }

                },
                error: function (error) {
                    console.log(error);
                }
            });

            //} else {
            //    tips('<p>手机号码不对</p>');
            //}
            //}

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
