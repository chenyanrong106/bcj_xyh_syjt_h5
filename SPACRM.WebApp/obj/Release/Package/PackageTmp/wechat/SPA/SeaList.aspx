<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeaList.aspx.cs" Inherits="SPACRM.WebApp.wechat.SPA.SeaList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" /> 
<meta content="yes" name="apple-mobile-web-app-capable" /> 
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta name="x5-page-mode" content="app">
<meta content="telephone=no" name="format-detection" />
<link type="text/css" rel="stylesheet" href="css/zhuanti.css"/>
<title>往期回顾</title>
</head>
<body>
    <form id="form1" runat="server">
   <div class="zhuantiList">
  <ul>
    <li>
      <a href="Seapage.aspx"><div class="zhuantiImg"><img src="images/P1.png"/></div>
      <div class="zhuantiInfo">
        <strong>香邂格蕾无花果悦颜香氛</strong>
        <p>第一期 2017.06.23  -  2017.07.06</p>
        <span><%=personcount+300 %>人参与</span>
      </div>
      </a>
    </li>
     <li>
      <a href="Seapage2.aspx"><div class="zhuantiImg"><img src="images/P2.png"/></div>
      <div class="zhuantiInfo">
        <strong>星巴克夏季特饮</strong>
        <p>第二期 2017.07.20  -  2017.08.03</p>
        <span><%=personcount2+300 %>人参与</span>
      </div>
      </a>
    </li>
    
     
  </ul>
</div>
    </form>
</body>
</html>
