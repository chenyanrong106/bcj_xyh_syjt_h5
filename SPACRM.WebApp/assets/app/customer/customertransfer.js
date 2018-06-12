
; (function (window, undefined, $) {
    $(document).ready(function () {
        $("#btnSearchOut").click(function () {
            var cardNo = $("#CardNoOut").val();
            var pwd = $("#Password").val();
            if ($.trim(cardNo) == "") {
                _showInfoMessage("请输入要转出的会员卡号！", "info");
                return false;
            }
            //if ($.trim(pwd) == "") {
            //    _showInfoMessage("请输入密码！", "info");
            //    return false;
            //}
            //会员卡级



            $.post("/Customer/getCard.do", { cardNo: cardNo, pwd: pwd },
                  function (data) {
                      if (data.status > 0) {
                          var json = data.data;
                          if (json.length > 0) {
                              $("#CardType").empty();
                              for (var j = 0; j < json.length; j++) {

                                  $("#CardType").append('<option value="' + json[j].CARD_ID + '@' + json[j].ID + '@' + json[j].MOBILE + '@' + json[j].NAME + '@' + json[j].PAR_AMT + '@' + json[j].BALANCE + '">' + json[j].CARD_NAME + '</option>');
                                  if(j==0)
                                  {
                                      $("#outcid").val(json[j].ID);
                                      $("#CUST_NO_OUT").val(json[j].MOBILE);
                                      $("#CUST_NAME_OUT").val(json[j].NAME);

                                      $("#PAR_AMT_OUT").val(formatPrice(json[j].PAR_AMT));
                                      var balance = json[j].BALANCE;
                                      $("#TRANSFERAMT").val(json[j].BALANCE);
                                      $("#BALANCE_OUT").val(formatPrice(balance));

                                      $("#CUST_ID_OUT").val(json[j].CUST_ID);
                                  }
                              }
                          }
                      }
                      else {
                          $("#outcid").val("0");
                          $("#CUST_NO_OUT").val("");
                          $("#CUST_NAME_OUT").val("");
                          $("#PAR_AMT_OUT").val("");
                          $("#BALANCE_OUT").val("");
                          $("#TRANSFERAMT").val("");
                          $("#CUST_ID_OUT").val("");
                          _showInfoMessage(data.message, "info");
                      }
                  },
                  "json"
                 );

        });

        $("#btnSearchIn").click(function () {
            var cardNo = $("#CardNoIn").val();

            if ($.trim(cardNo) == "") {
                _showInfoMessage("请输入要转入的会员卡号！", "info");
                return false;
            }

            $.post("/Customer/getCard.do", { cardNo: cardNo, pwd: "" },
                  function (data) {
                      if (data.status > 0) {
                          var json = data.data;
                          if (json.length > 0) {
                              $("#CardType_IN").empty();
                              for (var j = 0; j < json.length; j++) {
                                  $("#CardType_IN").append("<option value=" + json[j].CARD_ID + '@' + json[j].ID + '@' + json[j].MOBILE + '@' + json[j].NAME + '@' + json[j].PAR_AMT + '@' + json[j].BALANCE + ">" + json[j].CARD_NAME + "</option>");
                                  if (j == 0) {
                                      $("#incid").val(json[j].ID);
                                      $("#CUST_NO_IN").val(json[j].MOBILE);
                                      $("#CUST_NAME_IN").val(json[j].NAME);
                                      $("#PAR_AMT_IN").val(formatPrice(json[j].PAR_AMT));
                                      $("#BALANCE_IN").val(formatPrice(json[j].BALANCE));
                                      $("#CUST_ID_IN").val(json[j].CUST_ID);
                                  }
                              }
                          }
                      }
                      else {
                          $("#incid").val("0");
                          $("#CUST_NO_IN").val("");
                          $("#CUST_NAME_IN").val("");
                          $("#PAR_AMT_IN").val("");
                          $("#BALANCE_IN").val("");
                          $("#CUST_ID_IN").val("");
                          _showInfoMessage(data.message, "info");
                      }
                  },
                  "json"
                 );
        });

        $("#btnSave").click(function () {
            var outcid = $("#outcid").val();
            var incid = $("#incid").val();
            var CUST_ID_OUT = $("#CUST_ID_OUT").val();
            var CUST_ID_IN = $("#CUST_ID_IN").val();
            if (outcid == "")
            {
                _showInfoMessage("请您先检索转出会员卡号！", "info");
                return false;
            }
            if (incid == "")
            {
                _showInfoMessage("请您先检索转入会员卡号！", "info");
                return false;
            }
            var BALANCE = $("#BALANCE_OUT").val().replace(',', '');
            var TransferAmt = $("#TRANSFERAMT").val().replace(',', '');
            //if (parseFloat(TransferAmt) <= 0)
            //{
            //    _showInfoMessage("转出金额必须大于0！", "info");
            //    return false;
            //}
            if (parseFloat(TransferAmt) > parseFloat(BALANCE)) {
                _showInfoMessage("转出金额必须小于等于转出卡余额！", "info");
                return false;
            }
            $.post(options.transferUrl, {
                outcid: outcid,
                incid: incid,
                TransferAmt: TransferAmt,
                CUST_ID_OUT: CUST_ID_OUT,
                CUST_ID_IN: CUST_ID_IN
            },
              function (data) {
                  if (data.status > 0) {
                      _showInfoMessage("操作成功！", "success");
                      $("#btnSearchOut").click();
                      $("#btnSearchIn").click();
                  }
                  else {
                      _showInfoMessage("操作失败！"+data.message, "error");
                  }
              },
              "json"
             );
        });
        //价格格式化
       function formatPrice(value) {
            value = value.toString().replace(/\$|\,/g, '');
            if (isNaN(value))
                value = "0";
            sign = (value == (value = Math.abs(value)));
            value = Math.floor(value * 100 + 0.50000000001);
            cents = value % 100;
            value = Math.floor(value / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((value.length - (1 + i)) / 3) ; i++)
                value = value.substring(0, value.length - (4 * i + 3)) + ',' +
                value.substring(value.length - (4 * i + 3));
            return (((sign) ? '' : '-') + '' + value);
       };

       $("#btnReturn").click(function () {
           window.location.href = "/Customer/Index.do";
       });

       util.keyPress = function (ob) {
           if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
               ob.value = ob.t_value;
           else ob.t_value = ob.value;
           if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
               ob.o_value = ob.value;
       }
       util.onBlur = function (ob) {
           if (!ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?|\.\d*?)?$/))
               ob.value = ob.o_value;
           else {
               if (ob.value.match(/^\.\d+$/))
                   ob.value = 0 + ob.value;
               if (ob.value.match(/^\.$/))
                   ob.value = 0; ob.o_value = ob.value
           };
       };

       util.keyUpLCPayment = function (ob) {
           if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
               ob.value = ob.t_value;
           else ob.t_value = ob.value;
           if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
               ob.o_value = ob.value;
           try {
              
               var balance = $('input[name=BALANCE_OUT]').val();//余额
               var payTotal = 0;
             
               ////应付金额
               var payed_amt = ob.value;
               if (parseFloat(payed_amt.replace(',', '')) > parseFloat(balance.replace(',', ''))) {
                   _showInfoMessage("转出金额不能大于会员余额！", "info");
                   ob.value = balance;
                   return false;
               }
           }
           catch (ex) {
               _showInfoMessage("金额填写有误！" + ex.message, "error");
           }
       };


       $("#CardType").change(function () {
           var id = $("#CardType").val();

           $("#outcid").val(id.split('@')[1]);
           $("#CUST_NO_OUT").val(id.split('@')[2]);
           $("#CUST_NAME_OUT").val(id.split('@')[3]);

           $("#PAR_AMT_OUT").val(formatPrice(id.split('@')[4]));
           var balance = id.split('@')[5];
           $("#TRANSFERAMT").val(balance);
           $("#BALANCE_OUT").val(formatPrice(balance));
       });

       $("#CardType_IN").change(function () {
           var id = $("#CardType_IN").val();

           $("#incid").val(id.split('@')[1]);
           $("#CUST_NO_IN").val(id.split('@')[2]);
           $("#CUST_NAME_IN").val(id.split('@')[3]);

           $("#PAR_AMT_IN").val(formatPrice(id.split('@')[4]));
           $("#BALANCE_IN").val(formatPrice(id.split('@')[5]));
       });

    });
})(window, undefined, jQuery);