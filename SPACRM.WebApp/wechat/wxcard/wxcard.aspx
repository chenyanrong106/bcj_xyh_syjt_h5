﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxcard.aspx.cs" Inherits="SPACRM.WebApp.wechat.XYH_Coupon_H5.wxcard.wxcard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnCreateCoupon" runat="server" OnClick="btnCreateCoupon_Click" Text="创建商业集团活动卡券" />
            <br />
            <br />
            <asp:TextBox ID="txtCreateCouponLog" runat="server" Height="147px" TextMode="MultiLine" Width="767px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnCreateCoupon1" runat="server" OnClick="btnCreateCoupon1_Click" Text="创建2人成团卡券1" />
            <br />
            <br />
            <asp:TextBox ID="txtCreateCouponLog1" runat="server" Height="147px" TextMode="MultiLine" Width="767px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnCreateCoupon2" runat="server" OnClick="btnCreateCoupon2_Click" Text="创建2人成团卡券2" />
            <br />
            <br />
            <asp:TextBox ID="txtCreateCouponLog2" runat="server" Height="147px" TextMode="MultiLine" Width="767px"></asp:TextBox>
            <br />
            <br />
            <br />
            card_id:
            <asp:TextBox ID="txtCardId" runat="server" Width="388px" Text=""></asp:TextBox>
&nbsp;&nbsp;
            <asp:Button ID="btnImportCoupon" runat="server" OnClick="btnImportCoupon_Click" Text="导入卡券" />
            <br />
            <br />
            <asp:TextBox ID="txtImportCouponLog" runat="server" Height="147px" TextMode="MultiLine" Width="767px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnQueryCoupon" runat="server" OnClick="btnQueryCoupon_Click" Text="查询库存" Visible="False" />
            <br />
            <br />
            <asp:TextBox ID="txtQueryCouponLog" runat="server" Height="147px" TextMode="MultiLine" Visible="False" Width="767px"></asp:TextBox>
        </div>
    </form>
</body>
</html>
