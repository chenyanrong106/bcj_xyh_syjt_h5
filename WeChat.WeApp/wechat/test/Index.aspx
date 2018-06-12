<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SPACRM.WebApp.wechat.test.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="js/jquery.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="js/GetLocation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <br />
            <br />
            <br />
            <br />
            <input type="button" value="测试" id="button" style="width: 200px; height: 200px;" />

        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $("#button").click(function () {
        $.post("index.aspx?para=savedog", {
        },
         function (ret) {
             if (ret.st == 0) {
                 wx.chooseWXPay({
                     appid: ret.appId,
                     timestamp: ret.timeStamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                     nonceStr: ret.nonceStr, // 支付签名随机串，不长于 32 位
                     package: ret.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                     signType: ret.signType, // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                     paySign: ret.paySign, // 支付签名
                     success: function (res) {
                         // 支付成功后的回调函数
                         alert("ok");
                     },
                     fail: function (ret) {
                         alert(ret.err_code);
                     },
                     complete: function (ret) {
                         alert(ret.errMsg);
                     }
                 });
             }
             else {
                 alert(ret.err_code);
             }
         },
                           "json"
                          );
    });
</script>
