<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendTempleMessage.aspx.cs" Inherits="SPACRM.WebApp.wechat.test.SendTempleMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>快递状态模板消息发送</h1>
      <h3> 微信号：</h3><asp:TextBox ID="TextBox1" runat="server" Height="53px" Width="100%"></asp:TextBox>
        <h3>状态（不填为正在备货）：</h3> <asp:TextBox ID="TextBox2" runat="server" Height="53px" Width="100%"></asp:TextBox>
       <h3> 备注（不填为亲，您好您抢拍的PETKIN手工小鲜粮，收货地址缺少省份和地区，请直接回复详细地址谢谢）：</h3><asp:TextBox ID="TextBox3" runat="server" Height="53px" Width="100%"></asp:TextBox>
       <h3> 跳转页面：</h3>
        <asp:RadioButton ID="RadioButton1" GroupName="s" Checked="true" runat="server" Text="通知填写省份城市页面" /><asp:RadioButton ID="RadioButton2" GroupName="s" runat="server" Text="感谢页面" />
        <asp:Button ID="Button1" runat="server" Height="53px" Text="发送" Width="100%" OnClick="Button1_Click" />
    
    </div>
    </form>
</body>
</html>
