<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateJiuZhuFangImage.aspx.cs" Inherits="SPACRM.WebApp.wechat.test.UpdateJiuZhuFangImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="全部项目所有图片剪切" OnClick="Button1_Click" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" Text="单个项目所有图片剪切" OnClick="Button2_Click" />
    
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="全部项目长图及缩略一图剪切" OnClick="Button3_Click" />
    
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="全部动态全部图片剪切" />
    
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="全部项目所有图片添加水印" />
        <br />
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="单个项目所有图片添加水印" />
    
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="所有项目动态添加水印" />
    
    </div>
    </form>
</body>
</html>
