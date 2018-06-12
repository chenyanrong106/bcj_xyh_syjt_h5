; (function (window, undefined, $) {
    $(document).ready(function () {
        $("#btnReturnList").click(function () {
            window.location.href = "/Customer/View360.do?cid=" + $("#hideCUST_ID").val();
        });
    })



    //var checkText = CARD_NAME;  //获取Select选择的Text
    //var checkValue = CARD_ID;  //获取Select选择的Value
    //removeoption = "<option value='" + checkValue + "'>" + checkText + "</option>";

    var indx = $("#NEW_CARD_ID").get(0).selectedIndex;
    $("#NEW_CARD_ID option[value='" + $("#OLD_CARD_ID").val() + "']").remove();



    $("#NEW_CARD_ID").change(function () {
        CardChange();
    });

    //
    $("#LCCARD_PACK").change(function () {
        CardChange();
    });

    function CardChange() {
        var id = 0;
        if ($("#cardType").val() == "1") {
            id = $("#NEW_CARD_ID").val();
        } else {
            id = $("#LCCARD_PACK").val();
        }
       

        if (id == "" || id == null) {
            _showInfoMessage('请选择会员卡级！', 'info');
            return false;
        }
        //获取新卡级信息 在判断是否有赠送项目
        $.post("/Card/CardDetailAndSendInfo.do", { id: id },
              function (ret) {
                  if (ret.status > 0) {
                      var data = eval(ret.data);
                      $("#BUY_AMT").val(data.PRICE);//销售金额
                      $("#BUY_AMT").val(data.PRICE);
                      $("#NEW_PAR_AMT").val(data.PAR_AMT);//卡内余额
                      var prodName = $("#NEW_CARD_ID").find("option:selected").text();
                      $("#hidePROD_NAME").val(prodName);
                      //卡的有效期
                      //PERIOD
                      var PERIOD = data.PERIOD;
                      if (PERIOD == "" || PERIOD == null) {
                          PERIOD = 120;//有效期默认120个月
                      }
                      var myDate = new Date();
                      var DateFrom = myDate.getFullYear() + "-" + (parseInt(myDate.getMonth()) + parseInt('1')) + "-" + myDate.getDate();     //获取当前日期
                      var DateTo = addmulMonth(DateFrom, PERIOD);

                      $("#CardBeginDate").val(DateFrom);
                      $("#CardEndDate").val(DateTo);

                      //老卡欠款
                      var amt = 0;//老卡余额
                      amt = parseFloat($("#OLD_CARD_BALANCE").val());
                      var ARREARS = parseFloat($("#OLD_CARD_ARREARS").val());
                      var OLD_CARD_SALES_AMT = parseFloat($("#OLD_CARD_SALES_AMT").val());//买老卡时付的金额
                      $("#Need_PAY_AMT").val(parseFloat(data.PRICE - OLD_CARD_SALES_AMT).toFixed(2));
                      //$("#Need_PAY_AMT").val(parseFloat(data.PRICE - amt).toFixed(2));
                      $("#spanPayed").html($("#Need_PAY_AMT").val());
                      $("#PAYED_AMT").val($("#Need_PAY_AMT").val());
                      SetHideAndShow();
                      

                      //活动
                      promotion(id);
                      SetHideAndShow();
                  }
              });

    }

    function addmulMonth(dtstr, n) {
        var s = dtstr.split("-");
        var yy = parseInt(s[0]);
        var mm = parseInt(s[1]) - 1;
        var dd = parseInt(s[2]);
        var dt = new Date(yy, mm, dd);
        dt.setMonth(dt.getMonth() + n);
        var month = parseInt(dt.getMonth()) + 1;
        return dt.getFullYear() + "-" + month + "-" + dd;
    }

    //根据卡级加载赠送活动
    function promotion(id) {
        $.post("/Order/getPresent_Prod.do", { cardId: id },
               function (ret) {
                   if (ret.status > 0) {
                       var data = eval(ret.data);
                       var html = [];
                       $("#Present_Prod option").remove();
                       $("#Present_Prod").append("<option value=''>请选择...</option>");
                       $(data).each(function () {
                           html.push("<option itemid=" + this.PROM_LIMIT_PRICE + " value=" + this.ID + ">" + this.NAME + "</option>");

                       });
                       $(html.join('')).appendTo("#Present_Prod");

                   }
               },
               "json"
              );
    }

    $("#spanPresent").click(function () {
        if ($("#divPresent").css("display") == "block") {
            $("#divPresent").hide();
            $("#spanPresent").html("赠送项目【展开】");
        }
        else {
            $("#divPresent").show();
            $("#spanPresent").html("赠送项目【收起】");
        }
    });
    $("#Present_Prod").change(function () {

        var promId = $("#Present_Prod").val();
        $("#tProdItem tbody tr").remove();
        $("#spanPLPRICE").html("0");
        if (promId == null || promId == "") {
            _showInfoMessage("请选择赠送项目！", "info");
            return false;
        }
        //根据选择赠送项目包检索项目详情
        $.post("/Order/getPresent_ProdItem.do", { promId: promId },
              function (ret) {
                  if (ret.status > 0) {
                      var data = eval(ret.data);
                      var html = [];
                      $(data).each(function () {
                          html.push("<tr>");
                          html.push("<td><input type='checkbox' name='cbxCheck' onclick='util.cbxCheck(this)' value=" + this.PROD_ID + " style='width:15px;height:15px;'></td>");
                          html.push("<td>" + this.PROD_NAME + "</td>");
                          html.push("<td>" + this.PRICE + "</td>");
                          html.push("<td><input type='text' value='1' maxlength='3' name='prodNum'  onkeypress = 'util.keyPress(this)' onkeyup = 'util.keyUpPresent(this)' onblur = 'util.onBlur(this)' style='width:60px;' />");
                          html.push("<input type='hidden' name='prodId' value=" + this.PROD_ID + " /> ");
                          html.push("<input type='hidden' name='prodName' value=" + this.PROD_NAME + " /> ");
                          html.push("<input type='hidden' name='prodPrice' value=" + this.PRICE + " /> ");
                          html.push("</td>");
                          html.push("</tr>");
                      });

                      $("#hideBeginDate").val(data[0].BEGIN_DATE);
                      $("#hideEndDate").val(data[0].END_DATE);
                      var plprice = $("#Present_Prod").children('option:selected').attr("itemid");
                      $("#spanPLPRICE").html(plprice);
                      $(html.join('')).appendTo("#tProdItem tbody");
                  }
              },
              "json"
             );

    });
    //选择赠送项目
    util.cbxCheck = function (ob) {
        var totalprice = 0;
        $("#tProdItem tbody tr").each(function () {
            var cbx = $(this).find("input[NAME$=cbxCheck]").is(':checked');
            if (cbx) {
                var price = parseFloat($(this).children("td").eq(2).html());
                var num = parseInt($(this).find("input[NAME$=prodNum]").val());
                totalprice += parseFloat(price * num);
            }
        });
        //赠送金额上限
        var plprice = parseFloat($("#spanPLPRICE").html());
        if (totalprice > plprice) {
            _showInfoMessage("赠送项目不能超过上限金额！", "info");
            $(ob).attr("checked", false);
            return false;
        }
        $("#spanTotalPrice").html(totalprice);

    };

    function SetHideAndShow() {
        var Need_PAY_AMT = parseFloat($("#Need_PAY_AMT").val());
        if (Need_PAY_AMT >= 0) {
            $("#isQuitAmt").hide();
            $("#divPayType").show();
            $("#divPayType2").hide();
        } else {
            $("#isQuitAmt").show();
            $("#divPayType").hide();
            $("#divPayType2").show();
            if ($("#QuitCash").checked) {
                QuitCashClick(1);
            } else if ($("#NoQuit").checked) {
                QuitCashClick(2);
            }
        }
    }

    function QuitCashClick(i) {
        var Need_PAY_AMT = parseFloat($("#Need_PAY_AMT").val());
        if (Need_PAY_AMT < 0) {
            $("#divPayType").hide();
            $("#divPayType2").show();
            if (i == 1)//退现金
            {
                //$("#spanPayed").html(Need_PAY_AMT);
                $("#li_cash_pay").show();
                //$("#li_card_pay").show();
                $("#cashpay").val(Need_PAY_AMT);
                //$("#cardpay").val($("#OLD_CARD_BALANCE").val());
            }
            else {
                //不退现金
                $("#spanPayed").html(0);
                $("#li_cash_pay").hide();
                $("#li_card_pay").show();
            }
        }
    }

    $('#QuitCash').click(function () {
        QuitCashClick(1);
    })

    $('#NoQuit').click(function (i) {
        QuitCashClick(2);
    })

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
    util.keyUpCash = function (ob) {
        if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
            ob.value = ob.t_value;
        else ob.t_value = ob.value;
        if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
            ob.o_value = ob.value;
        try {
            var but_amt = $("#BUY_AMT").val();
            //var vipcard = $("#NEW_CARD_ID").val();
            var vipcard = 0;
            if ($("#cardType").val() == "1") {
                vipcard = $("#NEW_CARD_ID").val();
            } else {
                vipcard = $("#LCCARD_PACK").val();
            }
           
            if (vipcard == "" || vipcard == null) {
                _showInfoMessage("请您先选择会员卡级！", "info");
                ob.value = 0;
                return false;
            }
            //销售员
            //var masseur = $("#Masseur").val();
            //if (masseur == "" || masseur == null) {
            //    _showInfoMessage("请您先选择销售员！", "info");
            //    ob.value = 0;
            //    return false;
            //}
            //var paycard_type = $("#PAYCARD_TYPE").val();
            //if (paycard_type == "" || paycard_type == null) {
            //    _showInfoMessage("请您先选择支付方式！", "info");
            //    ob.value = 0;
            //    return false;
            //}
            if (parseFloat(ob.value) > parseFloat(but_amt)) {
                _showInfoMessage("付款金额不能大于销售金额！", "info");
                ob.value = but_amt;
                $("#ARREARS").val(0);
                $("#spanPayed").html(parseFloat(ob.value).toFixed(2));
                return false;
            }
            var cash_amt = parseFloat(but_amt) - parseFloat(ob.value);
            if (!isNaN(cash_amt)) {
                //欠款
                $("#ARREARS").val(parseFloat(cash_amt).toFixed(2));
                $("#spanPayed").html(parseFloat(ob.value).toFixed(2));
            }

        }
        catch (ex) {
            _showInfoMessage("金额填写有误！" + ex.message, "error");
        }
    };

    //卡支付方式
    util.keyUpPayment = function (ob) {
        if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
            ob.value = ob.t_value;
        else ob.t_value = ob.value;
        if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
            ob.o_value = ob.value;
        try {

            //var vipcard = $("#NEW_CARD_ID").val();
            var vipcard = 0;
            if ($("#cardType").val() == "1") {
                vipcard = $("#NEW_CARD_ID").val();
            } else {
                vipcard = $("#LCCARD_PACK").val();
            }
            if (vipcard == "" || vipcard == null) {
                _showInfoMessage("请您先选择会员卡级！", "info");
                ob.value = 0;
                return false;
            }
            var payed_amt = $.trim($("#Need_PAY_AMT").val());
            if (payed_amt == "" || payed_amt == null) {
                _showInfoMessage("请您先输入付款金额！", "info");
                ob.value = 0;
                return false;
            }

            var paymentMode = $('input[name=PaymentMode]');
            var payTotal = 0;
            $(paymentMode).each(function () {
                if (this.value != "" && this.value != null) {
                    payTotal += parseFloat(this.value);
                }
            });


            //应付金额
            var payed_amt = parseFloat($("#Need_PAY_AMT").val());

            var balance = payed_amt - parseFloat(payTotal);
            if (balance == 0) {
                $("#btnSave").removeAttr("disabled");

            } else {
                $("#btnSave").attr("disabled", "disabled");
                $("#Present_Prod").attr("disabled", "disabled");

            }
            if (balance < 0) {
                _showInfoMessage("支付总额不能大于还需支付金额！", "info");
                ob.value = 0;
                return false;
            }
            $("#spanPayed").html(parseFloat(balance).toFixed(2));
        }
        catch (ex) {
            _showInfoMessage("金额填写有误！" + ex.message, "error");
        }
    };

     //购买疗程卡
        $("#btnSaveOrder").click(function () {
            //var lccard_pack = $("#NEW_CARD_ID").val();
            var lccard_pack = 0;
            if ($("#cardType").val() == "1") {
                lccard_pack = $("#NEW_CARD_ID").val();
            } else {
                lccard_pack = $("#LCCARD_PACK").val();
            }
            if (lccard_pack == "" || lccard_pack == null) {
                _showInfoMessage("请您先选择卡级！", "info");
                return false;
            }


            var paymentMode = $('input[name=PaymentMode]');
            var payTotal = 0;
            $(paymentMode).each(function () {
                if (this.value != "" && this.value != null) {
                    payTotal += parseFloat(this.value);
                }
            });
            var payed_amt = parseFloat($("#Need_PAY_AMT").val());

            var balance = payed_amt - parseFloat(payTotal);
            if (payTotal == 0 && payed_amt>0)
            {
                $("#spanPayed").html(balance);
                _showInfoMessage("没有支付完成，不能保存订单！", "info");
                return false;
            }
            if (balance < 0) {
                _showInfoMessage("支付总额不能大于还需支付金额！", "info");
                return false;
            } else if (balance>0)
            {
                $("#spanPayed").html(balance);
                _showInfoMessage("没有支付完成，不能保存订单！", "info");
                return false;
            }


            $("#formSave").submit();
        });

        var submiting = false;
        $('#formSave').validator({
            valid: function (form) {
                if (submiting) {
                    return;
                }
                submiting = true;
                FormSubmit(form, function (res) {
                    if (res.status > 0) {
                        _showInfoMessage("操作成功！", "success");
                        var left = (screen.width - 400) / 2;
                        var top = (screen.height - 500) / 2;

                        window.location.href = "/Customer/View360.do?cid=" + $("#hideCUST_ID").val();

                        //var url = "/Order/CardPrint.do?cid=" + $("#hideCUST_ID").val() + "&oid=" + res.data;
                        //PopupWindow(url, 400, 500, true, "yes");
                        window.location.href = "/Customer/View360.do?cid=" + $("#hideCUST_ID").val();
                    }
                    else {
                        _showInfoMessage("操作失败！" + res.message, "error");
                    }
                    submiting = false;
                })
            }
        });
})(window, undefined, jQuery);