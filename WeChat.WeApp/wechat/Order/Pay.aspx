<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.Pay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>爱宠筹</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="css/main.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="css/meike.css" />
    <style>
        .checkTime {
            padding: 5px 5px 5px;
            display: block;
            overflow: hidden;
            font-family: Arial, Helvetica, sans-serif;
            border-bottom: 10px solid #eee;
        }

            .checkTime h2 {
                font-size: 1.6rem;
                color: #eb3612;
                line-height: 35px;
                font-weight: normal;
                padding-left: 5px;
            }

            .checkTime ul li {
                float: left;
                width: 20%;
            }

                .checkTime ul li span {
                    border: 1px solid #ddd;
                    border-radius: 5px;
                    margin: 5px;
                    height: 32px;
                    text-align: center;
                    color: #464646;
                    display: block;
                    font-size: 1.4rem;
                    line-height: 32px;
                }

                    .checkTime ul li span.curspan {
                        background: #eb3612;
                        border: 1px solid #eb3612;
                        color: #fff;
                    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header_box">
            <%if (j.JZType == 1)
              { %>
            <p><%--<span><a href="login.aspx"><bdo style="margin-top:10px;">申请筹粮</bdo></a></span>--%>此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办</p>
            <%}
              else
              { %>
            <p>筹款爱心项目，平台不收取任何手续费用</p>
            <%} %>
        </div>

        <div class="petchat">

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
                <p style="border-bottom: 1px solid #eee;"><span>已有<%=j.rc %>人支持</span>将运用于事件本身及进展</p>
                <%if(j.JZType==1){ %>
                <p>注：狗粮为福贝厂家直接供应，爱宠筹官方监控，如发现质量问题，平台会发起退换货，所筹粮款按原路返回给粉丝。</p>
                <%} %>
            </div>

            <div class="helpTa">
                <%if (j.JZType == 1)
                  { %>
                <h2>狗粮兑换：0.5kg/8元，原价：<s>0.5kg/18元</s></h2>
                <%} %>
                <p>
                    <span class="shuru">
                        <input name="" type="number" id="txtnum" class="txt" placeholder="可输入其他金额"></span><bdo>支付金额：</bdo>
                </p>
            </div>
            <div class="checkTime" id="timeId1">
                <ul>
                    <li><span id="10">10元</span></li>
                    <li><span id="20">20元</span></li>
                    <li><span id="50">50元</span></li>
                    <li><span id="100">100元</span></li>
                    <li><span id="200">200元</span></li>
                </ul>
            </div>

            <div class="paylist" id="checkID">
                <ol><span><em id="微信支付" class="curxuan"></em></span><bdo class="weixin">微信支付</bdo></ol>
                <ol><span><em id="支付宝支付"></em></span><bdo class="zhifubao">支付宝支付</bdo></ol>
            </div>
            <div class="paybeizhu">
                <textarea name="" cols="" rows="" id="remark" maxlength="200" placeholder="和小伙伴们说一句话吧～默认：支持(限200字)"></textarea>
                <div class="paybtn" id="tjdd">确认支持</div>
            </div>

            <div class="acc-login-input">
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
            </div>

        </div>

        <input type="hidden" id="iid" value="<%=Request.QueryString["id"]==null?Request.QueryString["state"]:Request.QueryString["id"] %>" />

        <input type="hidden" id="ztitle" value="<%=j.Title %>" />
        <input type="hidden" id="ftitle" value="<%=j.Detail %>" />
        <input type="hidden" id="urlpara" value="<%=Request.QueryString["p"]??"1" %>" />
    </form>
</body>
</html>
<script src="js/jquery.min.js"></script>
<script src="js/oa.js?v=2"></script>
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
<script src="js/GetLocation.js?v=1.0"></script>
<script src="js/Message2.js"></script>
<script type="text/javascript">

    var olicheck = document.getElementById('checkID').getElementsByTagName('span');
    for (var i = 0; i < olicheck.length; i++) {
        olicheck[i].index = i;
        olicheck[i].onclick = function () {
            for (var j = 0; j < olicheck.length; j++) {
                olicheck[j].children[0].className = '';
            }
            olicheck[this.index].children[0].className = 'curxuan';

        }
    }

    var oli = document.getElementById("timeId" + 1).getElementsByTagName('span');
    for (var i = 0; i < oli.length; i++) {
        oli[i].index = i;
        oli[i].onclick = function () {
            for (var j = 0; j < oli.length; j++) {
                oli[j].className = '';

            }
            oli[this.index].className = 'curspan';
            $("#txtnum").val(oli[this.index].id);
        }
    }

    $("#tjdd").click(function () {
        if ($("#txtnum").val() == "") {
            $.MsgBox.Alert("爱宠筹", "请填写支持金额");
        }
        else {
            $("#tjdd").hide();
            $.post("pay.aspx?para=tj", {
                zf: $("#checkID").find($("em.curxuan")).attr("id"),//支付方式
                txtnum: $("#txtnum").val(),
                remark: $("#remark").val(),
                iid: $("#iid").val(),
                p: $("#urlpara").val(),
                iswx: (isWeiXin() ? 1 : 0)
            },
                                 function (ret) {
                                     $("#tjdd").show();
                                     if (ret.st == -1) {
                                         $.MsgBox.Alert("爱宠筹", ret.msg);
                                     }
                                     else if (ret.st == 1) {
                                         $.MsgBox.Alert("爱宠筹", ret.err_code);
                                     }
                                     else if (ret.st == 2) {
                                         location = "Share.aspx?zfurl=" + ret.err_code;
                                     }
                                     else if (ret.st == 5) {
                                         window.location = "Test.aspx?iid=" + $("#iid").val() + "&txtnum=" + $("#txtnum").val() + "&remark=" + $("#remark").val() + "&p=" + $("#urlpara").val() + "&iswx=" + ((isWeiXin() ? 1 : 0));
                                     }
                                     else {
                                         wx.chooseWXPay({
                                             appid: ret.appId,
                                             timestamp: ret.timeStamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                                             nonceStr: ret.nonceStr, // 支付签名随机串，不长于 32 位
                                             package: ret.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                                             signType: ret.signType, // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                                             paySign: ret.paySign, // 支付签名
                                             success: function (res) {
                                                 // 支付成功后的回调函数
                                                 if (res.err_msg == "get_brand_wcpay_request：ok") { $.MsgBox.Alert("爱宠筹", "支付成功"); }
                                             },
                                             fail: function (ref) {
                                                 // alert(ret.err_code);
                                             },
                                             complete: function (rec) {
                                                 // alert(ret.errMsg);
                                                 if (rec.errMsg == "chooseWXPay:ok") {
                                                     $.MsgBox.Alert("爱宠筹", "支付成功", function () {
                                                         //WeixinJSBridge.invoke('closeWindow', {}, function (res) {

                                                         //});
                                                         //window.location = "chou.aspx";
                                                         window.location = ret.returnurl;// "chou.aspx";
                                                     });
                                                 }
                                                 else if (rec.errMsg == "chooseWXPay:cancel") {
                                                     $.MsgBox.Alert("爱宠筹", "支付已取消", function () {
                                                     });
                                                 }
                                                 else {
                                                     window.location = "Test.aspx?iid=" + $("#iid").val() + "&txtnum=" + $("#txtnum").val() + "&remark=" + $("#remark").val() + "&p=" + $("#urlpara").val() + "&iswx=" + ((isWeiXin() ? 1 : 0));
                                                     //$.MsgBox.Alert("爱宠筹", "由于微信限制不能跨号支付，方法1：请将此页面转发给自己点击链接完成操作。方法2：关注账号“爱宠筹”参与筹粮活动");
                                                 }
                                             }
                                         });
                                     }
                                 },
                                 "json"
                           );
        }
    });

</script>
