
; (function (window, undefined, $) {
    $(document).ready(function () {
        $("#btnSearchOut").click(function () {
            var cardNo = $("#CardNoOut").val();
            var pwd = $("#Password").val();
            if ($.trim(cardNo) == "") {
                _showInfoMessage("请输入要转出的会员卡号！", "info");
                return;
            }

            $.post(options.getCardUrl, { cardNo: cardNo, pwd: '' },
                  function (data) {
                      if (data.status > 0) {
                          var json = data.data;
                          if (json.length > 0) {
                              $("#CardType").empty();
                              for (var j = 0; j < json.length; j++) {

                                  $("#CardType").append("<option value=" + json[j].CARD_ID + '@' + json[j].ID + '@' + json[j].MOBILE + '@' + json[j].NAME + '@' + json[j].PAR_AMT + '@' + json[j].BALANCE + ">" + json[j].CARD_NAME + "</option>");
                                  if (j == 0) {
                                      $("#outcid").val(json[j].ID);
                                      $("#CUST_Mobile_OUT").val(json[j].MOBILE);
                                      $("#CUST_NAME_OUT").val(json[j].NAME);

                                      //$("#PAR_AMT_OUT").val(formatPrice(json[j].PAR_AMT));
                                      var balance = json[j].BALANCE;
                                      //$("#TRANSFERAMT").val(json[j].BALANCE);
                                      $("#BALANCE_OUT").val(balance);

                                      $("#CUST_ID_OUT").val(json[j].CUST_ID);
                                  }
                              }
                          }
                      }
                      else {
                          $("#outcid").val("0");
                          $("#CUST_NO_OUT").val("");
                          $("#CUST_NAME_OUT").val("");
                          //$("#PAR_AMT_OUT").val("");
                          $("#BALANCE_OUT").val("");
                          //$("#TRANSFERAMT").val("");
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
                return;
            }

            $.post(options.getCardUrl, { cardNo: cardNo, pwd: "" },
                  function (data) {
                      if (data.status > 0) {
                          var json = data.data;
                          if (json.length > 0) {
                              $("#CardType_IN").empty();
                              for (var j = 0; j < json.length; j++) {
                                  $("#CardType_IN").append("<option value=" + json[j].CARD_ID + '@' + json[j].ID + '@' + json[j].MOBILE + '@' + json[j].NAME + '@' + json[j].PAR_AMT + '@' + json[j].BALANCE + ">" + json[j].CARD_NAME + "</option>");
                                  if (j == 0) {
                                      $("#incid").val(json[j].ID);
                                      $("#CUST_Mobile_IN").val(json[j].MOBILE);
                                      $("#CUST_NAME_IN").val(json[j].NAME);
                                      //$("#PAR_AMT_IN").val(formatPrice(json[j].PAR_AMT));
                                      $("#BALANCE_IN").val(json[j].BALANCE);
                                      $("#CUST_ID_IN").val(json[j].CUST_ID);
                                  }
                              }
                          }
                      }
                      else {
                          $("#incid").val("0");
                          $("#CUST_NO_IN").val("");
                          $("#CUST_NAME_IN").val("");
                          //$("#PAR_AMT_IN").val("");
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
            if (outcid == "") {
                _showInfoMessage("请您先检索转出会员卡号！", "info");
                return false;
            }
            if (incid == "") {
                _showInfoMessage("请您先检索转入会员卡号！", "info");
                return false;
            }
            $.post(options.mergeCardUrl,
                {
                    outCardID: outcid,
                    inCardID: incid,
                    outCustID: CUST_ID_OUT,
                    inCustID: CUST_ID_IN
                },
                function (data) {
                    if (data.status > 0) {
                        _showInfoMessage("操作成功！", "success");
                    }
                    else {
                        _showInfoMessage("操作失败！" + data.message, "error");
                    }
                },
                "json"
            );
        });


        $("#btnReturn").click(function () {
            window.location.href = "/Customer/Index.do";
        });



        $("#CardType").change(function () {
            var id = this.value;
            //628@86979@13003160870@叶惠德@26666.67@26666.67
            $("#outcid").val(id.split('@')[1]);
            $("#CUST_Mobile_OUT").val(id.split('@')[2]);
            $("#CUST_NAME_OUT").val(id.split('@')[3]);
            //$("#PAR_AMT_OUT").val(formatPrice(id.split('@')[4]));
            var balance = id.split('@')[5];
            //$("#TRANSFERAMT").val(balance);
            $("#BALANCE_OUT").val(balance);
        });

        $("#CardType_IN").change(function () {
            var id = this.value;

            $("#incid").val(id.split('@')[1]);
            $("#CUST_Mobile_IN").val(id.split('@')[2]);
            $("#CUST_NAME_IN").val(id.split('@')[3]);

            //$("#PAR_AMT_IN").val(formatPrice(id.split('@')[4]));
            $("#BALANCE_IN").val(id.split('@')[5]);
        });

    });
})(window, undefined, jQuery);