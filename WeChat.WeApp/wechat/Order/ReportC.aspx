<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportC.aspx.cs" Inherits="SPACRM.WebApp.wechat.Order.ReportC" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>爱宠筹</title>
    <meta content="initial-scale=1.0,user-scalable=no,maximum-scale=1,width=device-width" name="viewport" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
        <link href="../../assets/css/styles.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="css/meike.css" />
    <link href="css/lightbox.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/Message2.js"></script>
        <script src="../../assets/plugins/form-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/plugins/form-datepicker/js/locales/bootstrap-datepicker.zh-CN.js"></script>
    <style>
        .petchat2 {
            display: block;
            overflow: hidden;
            max-width: 75rem;
            margin: 0 auto;
            padding-top: 46px;
        }

        .petliang2 {
            border-bottom: 10px solid #eee;
            background: #fff;
            overflow: hidden;
        }

            .petliang2 ol {
                float: left;
                width: 25%;
            }

                .petliang2 ol p {
                    padding: 0px 5px 0px 10px;
                    margin: 10px 0;
                    text-align: left;
                    border-left: 1px dashed #ddd;
                    line-height: 25px;
                    font-family: Arial, Helvetica, sans-serif;
                }

                .petliang2 ol strong {
                    display: block;
                    font-size: 1.8rem;
                    color: #f60;
                }

                    .petliang2 ol strong em {
                        font-size: 12px;
                        font-style: normal;
                        font-weight: normal;
                    }

                .petliang2 ol span {
                    display: block;
                    font-size: 1.4rem;
                    color: #666;
                }




        .btn {
            border: none;
            cursor: pointer;
            height: 50px;
            line-height: 50px;
            display: block;
            -wekit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
            width: 100%;
            margin: 10px auto 0;
            color: #fff;
            text-transform: uppercase;
            outline: none;
            background: #fa6c51;
            text-align: center;
            font-size: 18px;
            font-weight: bold;
            box-shadow: 0 5px 0 #b23622;
        }

        textarea {
            border: 1px solid #fa6c51;
            height: 50px;
            background: #fff;
            display: block;
            border-radius: 20px;
            width: 88%;
            padding: 10px;
            font-size: 16px;
        }

        .btn a:link, .btn a:visited {
            color: #fff;
            display: block;
        }

        .btn a:hover {
            color: #fff;
            display: block;
            text-decoration: none;
        }



        .phonebg {
            position: fixed;
            bottom: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: rgba(0,0,0,.9);
            display: none;
        }

        .phonebox {
            width: 280px;
            /*height: 320px;*/
            position: absolute;
            left: 50%;
            bottom: 50%;
            margin: 0 auto;
            overflow: hidden;
            z-index: 9999999;
            display: block;
            margin-left: -150px;
            margin-top: -85px;
            background: #eee;
            border-radius: 5px;
            padding: 10px;
        }

            .phonebox .txt {
                width: 99%;
                background: #fff;
                border: none;
                height: 44px;
                text-indent: 10px;
                font-size: 18px;
                line-height: 44px;
                border: 1px solid #ddd;
                -webkit-appearance: none;
            }

            .phonebox p {
                line-height: 30px;
                font-size: 16px;
                color: #666;
                text-align: center;
                background: #fff;
                padding: 8px;
                border-radius: 10px;
                height: 150px;
            }

                .phonebox p strong {
                    line-height: 50px;
                    display: block;
                    font-size: 20px;
                    text-align: center;
                    color: #ff6600;
                }

                .phonebox p bdo {
                    font-size: 14px;
                    color: #ff0000;
                    text-align: center;
                }

            .phonebox h2 {
                font-size: 14px;
                line-height: 40px;
                text-align: center;
                color: #f00;
                font-weight: normal;
            }

        .closed {
            position: absolute;
            background: url(images/cha.png) no-repeat center center;
            background-size: 20px;
            width: 20px;
            height: 20px;
            position: absolute;
            right: 15px;
            top: 15px;
            z-index: 99999999;
        }

         * {
            -webkit-box-sizing: inherit;
            -moz-box-sizing: border-box;
            /* box-sizing: border-box; */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header_box" style="height:34px;padding-top:15px;">
            <a href="Report.aspx" style="margin-left: 15px; margin-top: 15px; font-size: large; color: blue; font-weight: 300;">日报</a>
            <a href="ReportY.aspx" style="margin-left: 15px; margin-top: 15px; font-size: large; color: blue; font-weight: 300;">月报</a>
            <a href="ReportC.aspx" style="margin-left: 15px; margin-top: 15px; font-size: large; color: blue; font-weight: 300;">参数详情</a>
            <a href="ReportD.aspx" style="margin-left: 15px; margin-top: 15px; font-size: large; color: blue; font-weight: 300;">单笔</a>
            <a href="ReportL.aspx" style="margin-left: 15px; margin-top: 15px; font-size: large; color: blue; font-weight: 300;">累计</a>
            <a href="ReportDP.aspx" style="margin-left: 15px; margin-top: 15px; font-size: large; color: blue; font-weight: 300;">单篇</a>
        </div>

         <br />
        <br />
        <br />
        <br />
        <div style="float:left;">
            <input type="text" id="datetime" class="input-small form-control" style="width: 180px; display: inline-grid;" placeholder="选择查询时间" value="<%=Request.QueryString["rq"] %>" /></div>
        <div style="margin-left:100px;">
            <input type="button" id="btnsearch" class="form-control" style="width: 80px; display: inline-grid;" value="查询" /></div>

       
        <%
            if (((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null)) || Session["sfyz"] != null)
            {
                string user = "";
                string user2 = "";
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                }
                if (user == "oloJGv_apxGreADUhBSL4r7NnpVc" || user == "oloJGvzwelDQjIKMMWC8Z6ngM7gk" || user == "oloJGvyniJ-SLjTPKtIxtMRX-Oiw" || user == "oloJGvx3hLkCNadLGIpAMimz2Xwc" || user == "oloJGv4lWL-TS8yn8uo4Fu1rbVTI" || Session["sfyz"] != null)
                {
                    for (int i = 1; i < 15; i++)
                    {
                        List<SPACRM.Entity.Entities.OrderReport> list = null;
                        if (string.IsNullOrEmpty(Request.QueryString["rq"]))
                            list = nvbo.GetOrderReport5(i);
                        else
                            list = nvbo.GetOrderReport5(i, Request.QueryString["rq"]);
                        if (list.Count > 0)
                        {
        %>
          参数：<%=i.ToString()%>
        <div class="petchat" style="padding-top: 0;">
            <div class="petliang2">
                <ol>
                    <p><span>日期</span><strong><em></em></strong></p>
                </ol>
                <ol>
                    <p><span>总金额</span><strong><em></em></strong></p>
                </ol>
                <ol>
                    <p><span>筹粮</span><strong><em></em></strong></p>
                </ol>
                <ol>
                    <p><span>筹款</span><strong><em></em></strong></p>
                </ol>
            </div>

            <%
                            foreach (var l in list)
                            {
            %>
            <div class="petliang2">
                <ol>
                    <p><span></span><strong><%=l.rq.Substring(2, 6)%><em></em></strong></p>
                </ol>
                <ol>
                    <p><span></span><strong><%=Convert.ToInt32(l.je)%><em></em></strong></p>
                </ol>
                <ol>
                    <p><span></span><strong><%=Convert.ToInt32(l.wx)%><em></em></strong></p>
                </ol>
                <ol>
                    <p><span></span><strong><%=Convert.ToInt32(l.zfb)%><em></em></strong></p>
                </ol>
            </div>
            <%  } %>
        </div>
        <br />
        <br />
        <%
                        }
                    }
                }
            } %>


      

        <div class="phonebg" id="quanbox">
            <div class="closed" onclick="CloseBox();"></div>
            <div class="phonebox" id="phoneid">
                <h2 id="hname">请用微信打开或输入暗号<br />
                    （爱宠筹的主编叫什么？）</h2>
                <bdo style="margin-bottom: 20px;">
                    <input type="text" id="amt" name="iphone" class="txt" placeholder="输入暗号"></bdo>
                <div class="btn" onclick="return checked();">确认</div>
                <div class="btn" onclick="return CloseBox();">取消</div>
            </div>
        </div>

        <input type="hidden" id="ztitle" value="帮助可爱的毛孩子们吃上饱饭" />
        <input type="hidden" id="ftitle" value="此活动由宠物管家、宠物帮领养中心、福贝宠粮联合举办" />
    </form>
</body>
</html>
<script>
    $('#datetime').datepicker({
        language: "zh-CN",
        format: "yyyy-mm-dd",
        onSelect: function (dateText, inst) {
            alert(dateText);
        }
    });
    $("#btnsearch").click(function () {
        location = "reportc.aspx?rq=" + $('#datetime').val();
    });
    function isWeiXin() {
        var ua = window.navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == 'micromessenger') {
            return true;
        } else {
            return false;
        }
    }

    if (isWeiXin()) {
        $.ajax({
            type: "POST",
            url: "oa.aspx?oa=true",
            data: {},
            async: false,
            timeout: 15000,
            dataType: "json",
            success: function (data) {
                if (data.st == 1) {
                    var a = window.location.toString();
                    location = "oa.aspx?beforeurl=" + encodeURI(a.replace("&", "*"));
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "report.aspx?para=oa",
            data: {},
            async: false,
            timeout: 15000,
            dataType: "html",
            success: function (data) {
                if (data != "ok") {
                    $("#quanbox").show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });

    }
</script>
<script>
    function CloseBox() {
        $("#quanbox").hide();
    }
    function checked() {

        if ($("#amt").val() == "") {
            $.MsgBox.Alert("系统提示", "请输入暗号");
        }
        else {
            //alert("恭喜获得一张8折抵扣券，刚填写的手机号码为唯一识别码，产品上市当天可直接抵扣");  
            $.ajax({
                type: "POST",
                url: "report.aspx?para=yz",
                data: { mm: $("#amt").val() },
                async: false,
                timeout: 15000,
                dataType: "html",
                success: function (data) {
                    if (data == "ok") {
                        location = location;
                    }
                    else {
                        $.MsgBox.Alert("系统提示", "暗号错误");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            });


        }
    }

</script>
