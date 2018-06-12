<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeekReport.aspx.cs" Inherits="SPACRM.WebApp.wechat.Report.WeekReport" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>报表</title>
    <link href="../../assets/css/styles.css" rel="stylesheet" />
   <%-- <link href="../../assets/css/styles.css" rel="stylesheet" />--%>
    <link href="../../assets/plugins/charts-morrisjs/morris.css" rel="stylesheet" />
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
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="row">
                <div style="float: left; display: none;" id="divrq">
                    <input type="text" id="datetime" class="input-small form-control" style="width: 180px; display: inline-grid;" placeholder="选择查询时间" value="<%=Request.QueryString["rq"] %>" />
                </div>
                <div style="margin-left: 100px; display: none;" id="divrq2">
                    <input type="button" id="btnsearch" class="form-control" style="width: 80px; display: inline-grid;" value="查询" />
                </div>
                <div class="col-md-12 col-lg-12">
                    <div class="panel panel-primary">
                        <%-- <div class="panel-heading">
                                        <h4>月报</h4>
                                        <div class="options">
                                        </div>
                                    </div>--%>
                        <div class="panel-body">
                            <div id="bar-yue"></div>
                        </div>
                    </div>
                </div>

            </div>


        </div>

    </form>
</body>
</html>
<script src="js/jquery.min.js"></script>
<script src="../../assets/plugins/charts-morrisjs/raphael.min.js"></script>
<script src="../../assets/plugins/charts-morrisjs/morris.min.js"></script>
<script type='text/javascript' src='../../assets/plugins/easypiechart/jquery.easypiechart.min.js'></script>
<script src="../../assets/js/application.js"></script>
<script src="../../assets/js/bootstrap.min.js"></script>
<script src="js/Message2.js"></script>
<script src="../../assets/plugins/pulsate/jQuery.pulsate.min.js"></script>
<script src="../../assets/plugins/form-datepicker/js/bootstrap-datepicker.js"></script>
<script src="../../assets/plugins/form-datepicker/js/locales/bootstrap-datepicker.zh-CN.js"></script>
<%--<script src="js/demo-morrisjs.js"></script>--%>
<script>
    //if (window.location.host != "www.report.acchou.com" && window.location.host != "localhost:9732") {
    //    location = "http://petgohome.meijiewd.com/wechat/order/chou.aspx";
    //}

    $('#datetime').datepicker({
        language: "zh-CN",
        format: "yyyy-mm-dd",
        onSelect: function (dateText, inst) {
            alert(dateText);
        }
    });
    $("#btnsearch").click(function () {
        location = "order.aspx?rq=" + $('#datetime').val() + "&b=" + getQueryString("b");
    });
    function yue() {
        $.ajax({
            type: "POST",
            url: "order.aspx?para=yue",
            data: {},
            async: false,
            timeout: 15000,
            dataType: "json",
            success: function (data) {
                Morris.Bar({
                    element: 'bar-yue',
                    data: data,
                    xkey: 'rq',
                    ykeys: ['je', 'wx', 'zfb'],
                    labels: ['总金额', '筹粮', '筹款']
                });

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    }

    function year() {
        $.ajax({
            type: "POST",
            url: "order.aspx?para=year",
            data: {},
            async: false,
            timeout: 15000,
            dataType: "json",
            success: function (data) {
                Morris.Bar({
                    element: 'bar-yue',
                    data: data,
                    xkey: 'rq',
                    ykeys: ['je', 'wx', 'zfb'],
                    labels: ['总金额', '筹粮', '筹款']
                });

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    }

    function ri() {
        $.ajax({
            type: "POST",
            url: "order.aspx?para=ri",
            data: {},
            async: false,
            timeout: 15000,
            dataType: "json",
            success: function (data) {
                Morris.Bar({
                    element: 'bar-yue',
                    data: data,
                    xkey: 'rq',
                    ykeys: ['je', 'wx', 'zfb'],
                    labels: ['总金额', '筹粮', '筹款']
                });

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    }

    function z() {
        $.ajax({
            type: "POST",
            url: "order.aspx?para=z",
            data: {},
            async: false,
            timeout: 15000,
            dataType: "json",
            success: function (data) {
                Morris.Bar({
                    element: 'bar-yue',
                    data: data,
                    xkey: 'rq',
                    ykeys: ['je'],
                    labels: ['总金额']
                });

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    }

    ah();
    function ah() {
        //$("#divrq").show();
        //$("#divrq2").show();
        $.ajax({
            type: "POST",
            url: "weekreport.aspx?para=ah&rq=",
            data: {},
            async: false,
            timeout: 15000,
            dataType: "json",
            success: function (data) {
                Morris.Bar({
                    element: 'bar-yue',
                    data: data,
                    xkey: 'h',
                    ykeys: ['c'],
                    labels: ['项目数']
                });

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    }

    function mr() {
        $("#divrq").show();
        $("#divrq2").show();
        $.ajax({
            type: "POST",
            url: "order.aspx?para=mr&rq=" + $("#hirq").val(),
            data: {},
            async: false,
            timeout: 15000,
            dataType: "json",
            success: function (data) {
                Morris.Line({
                    element: 'bar-yue',
                    data: data,
                    xkey: 'h',
                    ykeys: ['p', 'c'],
                    labels: ['筹粮金额', '筹粮人次']
                });

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    }

    function u() {
        $.ajax({
            type: "POST",
            url: "order.aspx?para=u",
            data: {},
            async: false,
            timeout: 15000,
            dataType: "json",
            success: function (data) {
                Morris.Bar({
                    element: 'bar-yue',
                    data: data,
                    xkey: 'rq',
                    ykeys: ['c0', 'c1', 'c2', 'c3', 'c6', 'c7', 'c8', 'c9'],
                    labels: ['总金额', '默认参数', '宠物帮', '义工', '爱狗帮', '爱猫咪', '爱宠筹', '爱宠筹']
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    }

    function getQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return r[2]; return null;
    }

   

    //$('#newvisits,#newvisits2,#newvisits3,#newvisits4,#newvisits5,#newvisits6').easyPieChart({
    //    barColor: "#16a085",
    //    trackColor: '#edeef0',
    //    scaleColor: '#d2d3d6',
    //    scaleLength: 5,
    //    lineCap: 'square',
    //    lineWidth: 2,
    //    size: 90,
    //    onStep: function (from, to, percent) {
    //        $(this.el).find('.percent').text(Math.round(percent));
    //    }
    //});
</script>
<script>
    function isWeiXin() {
        var ua = window.navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == 'micromessenger') {
            return true;
        } else {
            return false;
        }
    }
</script>
