<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="SPACRM.WebApp.wechat.test.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="js/jquery.min.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>   <style type="text/css">
.figure-list{
  margin: 0;
  padding: 0;
}
.figure-list:after{
  content: "";
  display: block;
  clear: both;
  height: 0;
  overflow: hidden;
  visibility: hidden;
}
.figure-list li{
  list-style: none;
  float: left;
  width: 23.5%;
  margin: 0 2% 2% 0;
}
.figure-list figure{
  position: relative;
  width: 100%;
  height: 0;
  overflow: hidden;
  margin: 0;
  padding-bottom: 100%; /* 关键就在这里 */
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}
.figure-list figure a{
  display: block;
  position: absolute;
  width: 100%;
  top: 0;
  bottom: 0;
}
</style>
</head>
<body>
    <form id="form1" runat="server">

<%--<ul class="figure-list">
  <li>
    <figure style="background-image:url(http://1.su.bdimg.com/skin/19.jpg)">
      <a href="#"></a>
    </figure>
  </li>
  <li>
    <figure style="background-image:url(http://5.su.bdimg.com/skin/3.jpg)">
      <a href="#"></a>
    </figure>
  </li>
</ul>--%>
        <input type="text" id="token" placeholder="token" />
        <input type="text" id="cardid" placeholder="cardid" />
        <br /><br /><br />
        <input type="button" value="开始" id="beg" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" value="清空" id="del" />
        <br />
        <div id="jg"></div>
    </form>
</body>
</html>
<script>
    $("#beg").click(function () {
        ks();
    });
    function ks() {
        $.post("test.aspx?token=" + $("#token").val() + "&cardid="+$("#cardid").val(), {
        },
                    function (ret) {
                        //alert(ret.meg);
                        //alert(ret.meg == "ok");
                        if (ret.meg == "ok") {
                            $("#jg").prepend(ret.meg + ",id:" + ret.id + "<br>");
                            ks();
                        }
                        else {
                            $("#jg").prepend(ret.meg + ",id:" + ret.id);
                            alert(ret.meg);
                        }
                       
                    },
                    "json"
              );
    }

    $("#del").click(function () {
        $.post("test.aspx?delete=delete", {
        },
                   function (ret) {
                       $("#jg").html(ret);
                   },
                   "html"
             );
    });
</script>
