<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendTmpMessage.aspx.cs" Inherits="SPACRM.WebApp.SendTmpMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <ul style="width:1000px;">
             <li style="width:1000px;"><h1>发送模板消息</h1></li> 
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>  openid</label></div><input style="width: 700px;height:32px;" type="text" id="txtOpenid"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"> <label>  模板id</label></div><input style="width: 700px;height:32px;" type="text" id="txtTemid"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>   first</label></div><textarea style="width: 700px;" rows="5" id="txtFirst"  ></textarea></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>keyword1</label></div><input style="width: 700px;height:32px;" type="text" id="txtkeyword1"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>keyword2</label></div><input style="width: 700px;height:32px;" type="text" id="txtkeyword2"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>keyword3</label></div><input style="width: 700px;height:32px;" type="text" id="txtkeyword3"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>keyword4</label></div><input style="width: 700px;height:32px;" type="text" id="txtkeyword4"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>keyword5</label></div><input style="width: 700px;height:32px;" type="text" id="txtkeyword5"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>keyword6</label></div><input style="width: 700px;height:32px;" type="text" id="txtkeyword6"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>keyword7</label></div><input style="width: 700px;height:32px;" type="text" id="txtkeyword7"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label> 跳转URL</label></div><input style="width: 700px;height:32px;" type="text" id="txturl"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label>颜色</label></div><input style="width: 700px;height:32px;" type="text" id="txtColor" value="#ac2102"  /></li>
             <li style="width:1000px;margin:4px 7px 5px 4px;"><div style="text-align:left;padding-right:10px;width:100px;"><label style="color:red">发送结果</label></div><input style="width: 700px;height:32px;" type="text" id="txtResult"   /></li>
        </ul>
        <div  style="width:700px;text-align:right;"><input type="button" id="btnSend"  style="width:120px;height:38px;" value="发送"/></div>
        
    </form>
</body>
</html>
<script type="text/javascript" src="wechat/spa/js/jquery.min.js"></script>
<script type="text/javascript">
    $("#btnSend").click(function () {
           $.ajax({
            url: 'SendTmpMessage.aspx?para=send',

            type: 'POST',

               data: {
                   "openid": $("#txtOpenid").val(),
                   "temid": $("#txtTemid").val(),
                   "first": $("#txtFirst").val(),
                   "keyword1": $("#keyword1").val(),
                   "keyword2": $("#keyword2").val(),
                   "keyword3": $("#keyword3").val(),
                   "keyword4": $("#keyword4").val(),
                   "keyword5": $("#keyword5").val(),
                   "keyword6": $("#keyword6").val(),
                   "keyword7": $("#keyword7").val(),
                   "url": $("#txturl").val(),
                   "color": $("#Color").val()
               },

            dataType: 'json',

            timeout: 1000,

            error: function (e) { },

            success: function (result) {
                location = location;
            }

        });
    })
</script>