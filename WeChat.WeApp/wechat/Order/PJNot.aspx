<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PJNot.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.PJNot" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>配捐配置</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link type="text/css" rel="stylesheet" href="css/meike.css" />
    <%--<link rel='stylesheet' type='text/css' href='/assets/plugins/form-select2/select2.css' />--%>
    <style>
        select {
            /*Chrome和Firefox里面的边框是不一样的，所以复写了一下*/
            border: solid 1px #000;
            /*很关键：将默认的select选择框样式清除*/
            appearance: none;
            -moz-appearance: none;
            -webkit-appearance: none;
            /*在选择框的最右侧中间显示小箭头图片*/
            /*background: url("http://ourjs.github.io/static/2015/arrow.png") no-repeat scroll right center transparent;*/
            /*为下拉小箭头留出一点位置，避免被文字覆盖*/
            padding-right: 14px;
            width: 210px;
        }

            /*清除ie的默认选择框样式清除，隐藏下拉箭头*/
            select::-ms-expand {
                display: none;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="jz" style="display: none;">
            &nbsp;<br />
            <img src="images/jz.jpg" /><br />
            正在上传···<br />
            &nbsp;
        </div>
       
       
        <!--配置不参与配捐的基地begion-->
        <h2 style="text-align: center;">配置不参与配捐的基地</h2>
        <div class="choukuan-box">
            <p>
                <strong>选择项目</strong><select class="form-control" id="xmid3">
                    <%foreach (var j in jzlist)
                      {
                    %>
                    <optgroup label="<%=j.NickName %>">
                        <option value="<%=j.ID %>"><%=j.Title %></option>
                    </optgroup>
                    <%} %>
                </select>

            </p>
            <p><strong>时间</strong><input name="" id="Text1" type="text" class="txt" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" placeholder="请填写不参与配捐日期"></p>
        </div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 25%;">日期</td>
                <td style="width: 75%;">基地</td>
            </tr>
            <%foreach (var j in ntlist)
              {
            %>
            <tr>
                <td style="width: 25%;"><%=j.Time.Value.ToString("yyyy-MM-dd") %></td>
                <td style="width: 75%;"><%=j.JNickName %></td>
            </tr>
            <%} %>
            <tr></tr>
        </table>
        <br />
        <div class="paybtn" id="Div1" style="height: 40px; line-height: 40px;">确认提交</div>
        <!--配置不参与配捐的基地end-->
        <br />
        <br />
        <br />
        <br />
        <hr />
        <br />
        <a href="pj.aspx" style="color:blue;">添加每日配捐额度</a>
        <br /> <br />
        <a href="pjdt.aspx" style="color:blue;">项目动态管理</a>
        <br /> <br />
        <a href="pjnot.aspx" style="color:blue;">添加不参与配捐的项目</a>
        <br /> <br />
        <a href="pjxmpj.aspx" style="color:blue;">为项目添加配捐</a>
    </form>
</body>
</html>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="js/Message2.js"></script>
<script src="../../assets/js/ajaxfileupload.js"></script>
<%--<script type='text/javascript' src='assets/plugins/form-select2/select2.min.js'></script>--%>
<script type="text/javascript">
   

    $("#Div1").click(function () {
        if ($("#xmid3").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入项目ID");
        }
        else if ($("#Text1").val() == "") {
            //alert("请输入完整信息");
            $.MsgBox.Alert("爱宠筹", "请输入不参与配捐的时间");
        }
        else {
            $("#Div1").hide();
            $.post("PJ.aspx?para=ntpj", {
                xmid: $("#xmid3").val(),
                time: $("#Text1").val(),
                jd: $("#xmid3").find("option:selected").text()
            },
                            function (ret) {
                                $("#Div1").show();
                                if (ret.st == -1) {
                                    $.MsgBox.Alert("爱宠筹", ret.msg);
                                }
                                else {
                                    $.MsgBox.Alert("爱宠筹", ret.msg, function () {
                                        window.location = window.location;
                                    });
                                }
                            },
                            "json"
                      );
        }
    });

</script>
