
; (function (window, undefined, $) {
    $(document).ready(function () {
        var day = " 00:00";
        var day2 = " 00:00";
        $.ajax({
            url: "/report/GetBegAndEndTime.do",
            type: "POST",
            data: { "PID": 0 },
            async: false,
            timeout: 15000,
            success: function (result) {
                day = result.data.HOURS_BEGIN;
                day2 = result.data.HOURS_END;
            }
                ,
            error: function (result) {
                alert(result.error);
            }
        });
        $("#btnSearch").click(function () {
            Inint();
        });


        $("#btnSave").click(function () {
            var outcid = $("#outcid").val();
            var incid = $("#incid").val();
            $.post("/Customer/DailyCheckSave.do", { outcid: outcid, incid: incid },
              function (data) {
                  if (data.status > 0) {
                      _showInfoMessage("操作成功！", "success");
                  }
                  else {
                      _showInfoMessage(data.message, "error");
                  }
              },
              "json"
             );
        });

        $("#btnPrint").click(function () {
            var left = (screen.width - 400) / 2;
            var top = (screen.height - 500) / 2;
            if ($("#STORE_Id").val() == "") {
                _showInfoMessage("请选择门店！", "info");
                return false;
            }

            else {
                var url = "/Customer/DailyPrint2.do?startDate=" + $("#startDate").val() + "&endDate=" + $("#endDate").val() + "&StoreName=" + $("#STORE_Id  option:selected").text() + "&storeId=" + $("#STORE_Id").val();
                PopupWindow(url, 400, 500, true, "yes");
                //window.open("/Customer/DailyPrint.do?startDate=" + $("#startDate").val() + "&endDate=" + $("#endDate").val() + "&StoreName=" + $("#STORE_Id  option:selected").text() + "&storeId=" + $("#STORE_Id").val(), "_blank", "height=500,width=400,left=" + left + ",top=" + top + ",scrollbars=yes,location=no,resizable=yes");

            }
        });

        //$("#STORE_Id").change(function ()
        //{
        //    alert($('#STORE_Id option:selected').text());
        //})

        $("#btnReturn").click(function () {
            window.location.href = "/Customer/Index.do";
        });

        $('#startDate').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            autoclose: true
        }).on('hide', function (ev) {
            var date = $('#startDate').val() + day;
            $('#startDate').val(date)
        });
        $('#endDate').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            autoclose: true
        }).on('hide', function (ev) {
            var date = $('#endDate').val() + day2;
            $('#endDate').val(date)
        });

        $('#startDate').val(new Date().Format("yyyy-MM-dd") + day)
        $('#endDate').val(GetDateStr(1).Format("yyyy-MM-dd") + day2)
        // Inint();

        function GetDateStr(AddDayCount) {
            var dd = new Date();
            dd.setDate(dd.getDate() + AddDayCount);//获取AddDayCount天后的日期   
            return dd;

            //var y = dd.getYear();
            //var m = dd.getMonth() + 1;//获取当前月份的日期   
            //var d = dd.getDate();
            //return y + "-" + m + "-" + d;
        }
        function Inint() {
            var storeId = $("#STORE_Id").val();
            var startDate = $("#startDate").val();
            var endDate = $("#endDate").val();
            if (storeId == "") {
                _showInfoMessage("请选择门店！", "info");
                return false;
            }

            //判断是否有未付款订单
            $.post("/Customer/DailyIncome2.do", { storeId: storeId, sDate: startDate, eDate: endDate },
                     function (data) {
                         if (data.status > 0) {
                             var json = eval(data.data);
                             $("#divTable").html("");
                             var html = [];
                             html.push("<table>");
                             $(json).each(function () {
                                 if (this.PAY_AMT == "" || this.PAY_AMT == null) {
                                     html.push('<tr style="background-color:#e6e5e5">');
                                     html.push('<td colspan="2"><h4>' + this.PAYMENT_TYPE + '</h4></td>');
                                     html.push('</tr>');

                                 }
                                 else {
                                     html.push('<tr>');
                                     html.push('<td style="width:50%">' + this.PAYMENT_TYPE + '</td>');
                                     html.push('<td>' + this.PAY_AMT + '</td>');
                                     html.push('</tr>');
                                 }
                             });
                             html.push("</table>");
                             $(html.join('')).appendTo("#divTable");
                         }
                     },
                     "json"
                    );
        }

    });
})(window, undefined, jQuery);