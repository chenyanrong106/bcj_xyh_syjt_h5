<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Address.aspx.cs" Inherits="SPACRM.WebApp.wechat.NewVer.Order.Address" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<title>新建地址</title>
<meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
<meta content="yes" name="apple-mobile-web-app-capable" />
<meta content="black" name="apple-mobile-web-app-status-bar-style" />
<meta content="telephone=no" name="format-detection" />
    <link type="text/css" href="../newver/css/index.css" rel="stylesheet" />
<style type="text/css">
*{margin:0; padding:0;}
body{background:#f2f2f2; margin:0; padding:0;width:100%;height:100%;overflow-x:hidden;-webkit-user-select:none;-webkit-text-size-adjust:none;}
.main{width:100%; height:100%; max-width:640px; display:block; background:#fff; background-size:cover; position:absolute; left:0; top:0; z-index:999;}
.dizhi{  display:block; padding:10px; background:#fff; margin-bottom:10px;}
.dizhi ul li{ border-bottom:1px solid #eee; line-height:44px; color:#999; padding:5px 0;}
.dizhi ul li strong{color:#666; font-size:16px; text-decoration:none; width:80px; display:inline-block;}
.dizhi ul li input{width:70%; border:none; -webkit-appearance: none; height:44px; font-size:16px;}
.btn{ width:100%; height:44px; border-radius:5px; background:#f38a02; font-size:18px; color:#fff; text-align:center; line-height:44px; font-weight:bold; margin:20px 0;}
</style>
    
     <script src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/zepto.min.js"></script>
<script type="text/javascript" src="js/touch.js"></script>
        <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
        <script src="js/GetLocation.js"></script>
    <script src="js/Message2.js"></script>
    <script src="../NewVer/js/area.js"></script>
    <script type="text/javascript">_init_area();</script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="main">
  
<div class="dizhi">
  <ul>
      <li><p><strong>收货人</strong><input name="" runat="server" id="xm" type="text" placeholder="您的姓名"></p></li>
      <li><p><strong>手机号码</strong><input name="" runat="server" id="sj" type="text" placeholder="输入手机号码"></p></li>
      <li><p><strong>省市信息</strong><select id="s_province" name="s_province" class="dropdown-select"></select>  
    <select id="s_city" name="s_city" class="dropdown-select"></select>  
    <select id="s_county" name="s_county" class="dropdown-select"></select></p></li>
      <li><p><strong>详细地址</strong><input name="" runat="server" id="address" type="text" placeholder="街道信息"></p></li>
  </ul>
    <div style="font-size:smaller;color:gray;margin-top:5px;"><font style="color:red;">*</font>无法选择县级或区域信息的，请直接写在详细地址中。</div>
  <div class="btn">保存</div>


</div>

<div
    </form>
</body>
</html>
<script type="text/javascript">
    $(".btn").click(function () {
        if ($("#xm").val() == "") {
            $.MsgBox.Alert("宠物管家", "请输入收货人姓名");
        }
        else if ($("#sj").val().length!=11) {
            $.MsgBox.Alert("宠物管家", "请输入11位手机号码");
        }
        else if ($("#s_province").val() == "省份") {
            $.MsgBox.Alert("宠物管家", "请选择省份");
        }
        else if ($("#s_city").val() == "地级市") {
            $.MsgBox.Alert("宠物管家", "请选择地级市");
        }
        //else if ($("#s_county").val() == "市、县级市") {
        //    $.MsgBox.Alert("宠物管家", "请选择市、县级市");
        //}
        else if ($("#address").val() == "") {
            $.MsgBox.Alert("宠物管家", "请输入详细地址");
        } else {
            $.post("Address.aspx?para=tj", {
                xm: $("#xm").val(),
                sj: $("#sj").val(),
                s: $("#s_province").val(),
                c: $("#s_city").val(),
                x: $("#s_county").val(),
                address: $("#address").val()
            },
                                function (ret) {
                                    if (ret == "ok") {
                                        $.MsgBox.Alert("宠物管家", ret, function () { location = "zhifu2.aspx";});
                                    }
                                    else {
                                        $.MsgBox.Alert("宠物管家", ret);
                                    }
                                },
                                "html"
                          );
        }
    });
    _init_area();
    var Gid = document.getElementById;
    var showArea = function () {
        Gid('show').innerHTML = "<h3>省" + Gid('s_province').value + " - 市" +
        Gid('s_city').value + " - 县/区" +
        Gid('s_county').value + "</h3>"
    }
    Gid('s_county').setAttribute('onchange', 'showArea()');
    $("#s_province").html().append("<option value=\"上海\" selected>上海</option>");
</script>