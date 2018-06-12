<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zx.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.zx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div>
    <h1>在线人数：<%=Application["user_sessions "] %></h1>
    </div>
    </form>
</body>
</html>
