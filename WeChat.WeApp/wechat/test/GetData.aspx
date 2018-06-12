<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetData.aspx.cs" Inherits="SPACRM.WebApp.wechat.test.GetData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <iframe style="width:100%;height:1200px;" src="https://a.dper.com/shops#/shops/all?_k=ps25aq"></iframe>
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </form>
</body>
</html>
<script>
    $.ajax({
        type: "POST",
        url: "https://a.dper.com/shop/cascade/query",
        data: {category:-1,mainCategory:-1,mainRegion: -1,ownerType:0,pageIndex:1,pageSize:10,region:-1},
        async: false,
        timeout: 15000,
        dataType: "jsonp",
        success: function (data) {
           
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });
</script>