<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendNews.aspx.cs" Inherits="SPACRM.WebApp.wechat.test.SendNews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="发送" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
