<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="urltest.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.urltest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery.min.js"></script>
   <%-- <script src="js/oa.js?v=2"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //alert(window.location);
    function isWeiXin() {
        var ua = window.navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == 'micromessenger') {
            return true;
        } else {
            return false;
        }
    }
    var a = window.location.toString();
    alert((a.replace("&", "*")));

    //if (!isWeiXin()) {
    //    $.ajax({
    //        type: "POST",
    //        url: "oa.aspx?oa=true",
    //        data: {},
    //        async: false,
    //        timeout: 15000,
    //        dataType: "json",
    //        success: function (data) {
    //            if (data.st == 1) {
    //                location = "oa.aspx?beforeurl=" + encodeURI(window.location);
    //            }
    //        },
    //        error: function (XMLHttpRequest, textStatus, errorThrown) {

    //        }
    //    });
    //}
    </script>