<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SPACRM.WebApp.wechat.test.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  <!--startprint-->说明：这是开始打印的位置
    打印内容打印内容打印内容打印内容打印内容打印内容打印内容打印内容
    adfhioasdhfiohasdofihosdhfosdhfiosdhfiosdfhsdfhsdifsidfiosdfhosdhf
    <!--endprint-->
<p>所有内容</p>
<div id="div2">div2的内容</div>
<a href="javascript:doPrint()" target="_self">打印</a>

    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    function doPrint() {
        var oldstr = document.body.innerHTML;
        bdhtml = window.document.body.innerHTML;
        sprnstr = "<!--startprint-->";
        eprnstr = "<!--endprint-->";
        prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
        prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
        window.document.body.innerHTML = prnhtml;
        window.print();
        window.location.reload();
       // window.document.body.innerHTML = oldstr;
        return false;
    }
        </script>