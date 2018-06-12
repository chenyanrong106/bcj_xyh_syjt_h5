; (function (window, undefined, $) {
    $(document).ready(function () {
        window.history.forward(1);

        var cardType = $("#hideCardType").val();
        var cardId = $("#hideCard_Id").val();
        if (cardType != null && cardType != "") {
            if (cardType == "2") {
                //疗程卡 续清
                $("#VipCard").css("display", "none");
                $("#LCCard").addClass("active");
                $("#spro").removeClass("active");
                $("#service").addClass("active");

                //疗程卡信息
                //$("#LCCARD_PACK").val(cardId);
                PayCustLCCard(cardId);
            }
            else if (cardType == "1") {
                //会员卡续清
                $("#LCCard").css("display", "none");
                $("#VipCard").addClass("active");
                $("#service").removeClass("active");
                $("#spro").addClass("active");

                PayCustCard(cardId);
            }
        }
        else {
            //是否有卡欠费
            var CardArrears = $("#hideCardArrears").val();
            if (CardArrears == "cardarrears") {
                $("#VipCard").css("display", "none");
                $("#LCCard").addClass("active");
                $("#spro").removeClass("active");
                $("#service").addClass("active");
            }
        }


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
                        if (parent && parent.xjDailog) {
                            parent.xjDailog.Close(null, true, 1);
                        }
                        else {
                            window.close();
                        }
                    }
                    else {
                        _showInfoMessage("操作失败！" + res.message, "error");
                    }
                    submiting = false;
                })
            }
        });

        $('#BEGIN_DATE').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            autoclose: true
        }).on('changeDate', function (ev) {

        });
        $('#END_DATE').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            autoclose: true
        });

        function CardChange() {
            var id = $("#CARD_ID").val();

            $("#PAYED_AMT").val("");

            $('input[name=PaymentMode]').val("");
            $("#spanPayed").html("0");
            if (id == "" || id == null) {
                _showInfoMessage('请选择会员卡级！', 'info');
                return false;
            }
            var prodName = $("#CARD_ID").find("option:selected").text();
            $("#hidePROD_NAME").val(prodName);
            //清空赠送项目项
            $("#tProdItem tbody tr").remove();
            $("#spanPLPRICE").html("0");
            $.post("/Card/Detail.do", { id: id },
                    function (ret) {
                        if (ret.status > 0) {
                            var data = eval(ret.data);
                            $("#BUY_AMT").val(data.PRICE);
                            $("#YH_AMT").val(parseFloat(data.PAR_AMT) - parseFloat(data.PRICE));
                            //$("#CASH_AMT").val(data.PRICE);
                            var mon = data.PERIOD;
                            var bdate = new Date();
                            bdate.setMonth(bdate.getMonth() + mon);
                            var edate = bdate.getFullYear() + "-" + (bdate.getMonth() + 1) + "-" + bdate.getDate();
                            $("#END_DATE").val(edate);

                            //活动
                            promotion(id);
                        }
                    },
                    "json"
                   );
        }
        $("#CARD_ID").change(function () {
            CardChange();
            //$("#ARREARS").val("");
        });
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
        //充值购卡
        $("#btnSave").click(function () {
            var vipcard = $("#CARD_ID").val();
            if (vipcard == "" || vipcard == null) {
                _showInfoMessage("请您先选择会员卡级！", "info");
                return false;
            }
            //销售员
            var masseur = $("#Masseur").val();
            if (masseur == "" || masseur == null) {
                _showInfoMessage("请您先选择销售员！", "info");
                return false;
            }
            //充值购卡
            $("#hide_Type").val("buyCard");
            $("#formSave").submit();
        });
        //购买疗程卡
        $("#btnSaveOrder").click(function () {
            var lccard_pack = $("#LCCARD_PACK").val();
            if (lccard_pack == "" || lccard_pack == null) {
                _showInfoMessage("请您先选择疗程套餐！", "info");
                return false;
            }
            //销售员
            var masseur = $("#MasseurLCC").val();
            if (masseur == "" || masseur == null) {
                _showInfoMessage("请您先选择销售员！", "info");
                return false;
            }
            //购买疗程卡
            $("#hide_Type").val("buyLCC");
            //疗程名字
            var lCCARD_NAME = $("#LCCARD_PACK").find("option:selected").text();
            $("#hideLCCARD_PACK").val(lCCARD_NAME);
            //应付
            var totalAmt = $("#spanTotalAmt").html();
            $("#hidePayAmt").val(totalAmt);

            //疗程抵用
            $("#formSave").submit();
        });
        $("#btnReturn").click(function () {
            window.location.href = "/Customer/View360.do?cid=" + $("#hideCUST_ID").val();
        });
        $("#btnReturnList").click(function () {
            window.location.href = "/Customer/View360.do?cid=" + $("#hideCUST_ID").val();
        });

        $("#LCCARD_PACK").change(function () {
            showLCCPack();
        });
        function showLCCPack() {
            var lccpackId = $("#LCCARD_PACK").val();//疗程卡套餐
            if (lccpackId != null && lccpackId != "") {
                showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 700, top: 450 });
                $.post("/Order/GetLCCPack.do", { lid: lccpackId },
                       function (ret) {
                           if (ret.status > 0) {
                               var data = eval(ret.data);
                               //疗程总价
                               $("#spanTotalAmt").html(data[0].DISCOUNT_PRICE);
                               $("#lcPayedAmt").val(data[0].DISCOUNT_PRICE);
                               $("#spanLCPayed").html(data[0].DISCOUNT_PRICE);
                               //$("#spanBalanceAmt").html(data[0].DISCOUNT_PRICE);
                               //疗程有效期 
                               $("#hidePERIOD").val(data[0].PERIOD);
                               $("#tSpeedyPro tbody").html("");
                               var proListHtml = [];
                               $(data).each(function (i) {
                                   if (i > 0) {
                                       proListHtml.push("<tr class='strip'>");
                                       //proListHtml.push("<td style=\"width: 20%;\"><div style=\"text-align:left;\">", this.ID, "</div></td>");
                                       proListHtml.push("<td style=\"width: 55%;\"><div style=\"text-align:left;\">", this.SERVICE_NAME, "</div></td>");
                                       proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", (this.AMT_PRICE / this.SERVICE_QTY).toFixed(2), "</div></td>");//this.UNIT_PRICE
                                       proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", this.SERVICE_QTY, "</div></td>");
                                       proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", this.AMT_PRICE, "</div></td>");
                                       proListHtml.push("<td style=\"width: 15%;\"><div style=\"text-align:left;\">");

                                       if (this.IS_REQUIRED) {
                                           proListHtml.push("<a title='移除' class='abtn' href='javascript:;' onclick=\"util.Remove(this,'", this.ID, "','", this.AMT_PRICE, "')\"><i class='fa fa-trash-o' ></i> 移除</a>");
                                       }
                                       else {
                                           proListHtml.push("<span title='必选疗程项目'>&nbsp;必 选</span>");
                                       }
                                       proListHtml.push("</div></td>");
                                       proListHtml.push("<td>");
                                       proListHtml.push("<input type='hidden' name='proId' value=" + this.ID + " />");//this.ID
                                       proListHtml.push("<input type='hidden' name='proName' value='" + this.SERVICE_NAME + "' />");
                                       proListHtml.push("<input type='hidden' name='proPrice' value=" + (this.AMT_PRICE / this.SERVICE_QTY).toFixed(2) + " />");//单价this.UNIT_PRICE
                                       proListHtml.push("<input type='hidden' name='proQty' value=" + this.SERVICE_QTY + " />");//数量
                                       proListHtml.push("<input type='hidden' name='payAmt' value=" + this.AMT_PRICE + " />");//应付小计金额
                                       proListHtml.push("</td>");
                                       proListHtml.push("</tr>");
                                   }
                               });
                               $(proListHtml.join('')).appendTo("#tSpeedyPro tbody");
                               hideLoadingMsg();
                           }
                       },
                       "json"
                      );
            }
            else {
                $("#tSpeedyPro tbody").html("");
                //疗程总价
                $("#spanTotalAmt").html(0);
                //$("#spanBalanceAmt").html(0);
                $("#lcPayedAmt").val(0);
                $("#spanLCPayed").html(0);

            }

        }

        //已购买的疗程卡项目 showLCCPacked
        function showLCCPacked(lccpackId) {
            //疗程卡套餐
            if (lccpackId != null && lccpackId != "") {
                showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 700, top: 450 });
                $.post("/Order/GetBuyLCCPack.do", { lid: lccpackId },
                       function (ret) {
                           if (ret.status > 0) {
                               var data = eval(ret.data);
                               //疗程总价
                               //$("#spanTotalAmt").html(data[0].DISCOUNT_PRICE);
                               //$("#lcPayedAmt").val(data[0].DISCOUNT_PRICE);
                               //$("#spanLCPayed").html(data[0].DISCOUNT_PRICE);
                               ////$("#spanBalanceAmt").html(data[0].DISCOUNT_PRICE);
                               ////疗程有效期 
                               //$("#hidePERIOD").val(data[0].PERIOD);
                               $("#tSpeedyPro tbody").html("");
                               var proListHtml = [];
                               $(data).each(function (i) {
                                   proListHtml.push("<tr class='strip'>");
                                   proListHtml.push("<td style=\"width: 55%;\"><div style=\"text-align:left;\">", this.SERVICE_NAME, "</div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", this.UNIT_PRICE, "</div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", this.BUY_QTY, "</div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", this.SERVICE_PRICE, "</div></td>");
                                   proListHtml.push("<td style=\"width: 15%;\"><div style=\"text-align:left;\">");
                                   proListHtml.push("</div></td>");

                                   proListHtml.push("<td>");
                                   proListHtml.push("<input type='hidden' name='proId' value=" + this.ID + " />");
                                   proListHtml.push("<input type='hidden' name='proName' value='" + this.SERVICE_NAME + "' />");
                                   proListHtml.push("<input type='hidden' name='proPrice' value=" + (this.AMT_PRICE / this.SERVICE_QTY).toFixed(2) + " />");//单价this.UNIT_PRICE 
                                   proListHtml.push("<input type='hidden' name='proQty' value=" + this.BUY_QTY + " />");//数量
                                   proListHtml.push("<input type='hidden' name='payAmt' value=" + this.SERVICE_PRICE + " />");//应付小计金额
                                   proListHtml.push("</td>");

                                   proListHtml.push("</tr>");
                               });
                               $(proListHtml.join('')).appendTo("#tSpeedyPro tbody");
                               hideLoadingMsg();
                           }
                       },
                       "json"
                      );
            }
            else {
                $("#tSpeedyPro tbody").html("");
                //疗程总价
                $("#spanTotalAmt").html(0);
                //$("#spanBalanceAmt").html(0);
                $("#lcPayedAmt").val(0);
                $("#spanLCPayed").html(0);

            }
        }
        util.Remove = function (obj, id, price) {
            var totalAmt = parseFloat($("#spanTotalAmt").html());
            totalAmt = totalAmt - parseFloat(price);
            $("#spanTotalAmt").html(totalAmt);
            //$("#spanBalanceAmt").html(totalAmt);
            $("#lcPayedAmt").val(totalAmt);
            $(obj).parent("div").parent("td").parent("tr").remove();
        }


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
            //alert(ob.t_value);
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {
                var but_amt = $("#BUY_AMT").val();
                var vipcard = $("#CARD_ID").val();
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
                    $("#spanPayed").html(ob.value);
                    return false;
                }
                var cash_amt = parseFloat(but_amt) - parseFloat(ob.value);
                if (!isNaN(cash_amt)) {
                    //欠款
                    $("#ARREARS").val(cash_amt);
                    $("#spanPayed").html(ob.value);
                }

            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };
        //续清
        util.keyUpPayedCash = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {
                var but_amt = $("#hideARREARS").val();
                var vipcard = $("#CARD_ID").val();
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

                if (parseFloat(ob.value) > parseFloat(but_amt)) {
                    _showInfoMessage("付款金额不能大于未付金额！", "info");
                    ob.value = but_amt;
                    $("#ARREARS").val(0);
                    $("#spanPayed").html(ob.value);
                    return false;
                }
                var cash_amt = parseFloat(but_amt) - parseFloat(ob.value);
                if (!isNaN(cash_amt)) {
                    //欠款 
                    $("#ARREARS").val(parseFloat($("#hideARREARS").val()) - ob.value);
                    $("#spanPayed").html(ob.value);
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

                var vipcard = $("#CARD_ID").val();
                if (vipcard == "" || vipcard == null) {
                    _showInfoMessage("请您先选择会员卡级！", "info");
                    ob.value = 0;
                    return false;
                }

                var payed_amt = $.trim($("#PAYED_AMT").val());
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
                var payed_amt = parseFloat($("#PAYED_AMT").val());

                var balance = payed_amt - parseFloat(payTotal);
                if (balance == 0) {
                    $("#btnSave").removeAttr("disabled");

                    ///如果有欠款
                    var arrears = $("#ARREARS").val();
                    if (arrears != "") {
                        if (parseFloat(arrears) == 0) {
                            $("#Present_Prod").removeAttr("disabled");
                        }
                        else {
                            $("#Present_Prod").attr("disabled", "disabled");
                        }
                    }

                } else {
                    $("#btnSave").attr("disabled", "disabled");
                    $("#Present_Prod").attr("disabled", "disabled");

                }
                if (balance < 0) {
                    _showInfoMessage("支付总额不能大于还需支付金额！", "info");
                    ob.value = 0;
                    return false;
                }
                $("#spanPayed").html(balance);
            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };
        //疗程卡付款金额
        util.keyUpLCPayed = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {
                //应付总额
                var spanTotalAmt = parseFloat($("#spanTotalAmt").html());
                if (ob.value > spanTotalAmt) {
                    _showInfoMessage("付款金额不能大于应付总额！", "info");
                    ob.value = $("#spanTotalAmt").html();
                    $("#spanLCPayed").html(ob.value);
                    return false;
                }
                $("#spanLCPayed").html(ob.value);
                if (parseFloat($("#spanLCPayed").html()) == 0) {
                    $("#btnSaveOrder").removeAttr("disabled", "disabled");
                }
                else {
                    $("#btnSaveOrder").attr("disabled", "disabled");
                }
            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };
        //续清
        util.keyUpXQLCPayed = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {
                if (ob.value == null || ob.value == "") {
                    ob.value = 0;
                    return false;
                }
                //应付总额
                var spanTotalAmt = parseFloat($("#spanArrearsAmt").html());
                if (ob.value > spanTotalAmt) {
                    _showInfoMessage("付款金额不能大于未付金额！", "info");
                    ob.value = $("#spanArrearsAmt").html();
                    $("#spanLCPayed").html(ob.value);
                    return false;
                }
                $("#spanLCPayed").html(ob.value);
                if (parseFloat($("#spanLCPayed").html()) == 0) {
                    $("#btnSaveOrder").removeAttr("disabled", "disabled");
                }
                else {
                    $("#btnSaveOrder").attr("disabled", "disabled");
                }
            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };
        //疗程支付方式
        util.keyUpLCPayment = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {

                var lcpaymentMode = $('input[name=LCPaymentMode]');
                var payTotal = 0;
                //支付总额
                $(lcpaymentMode).each(function () {
                    if (this.value != "" && this.value != null) {
                        payTotal += parseFloat(this.value);
                    }
                });
                ////应付金额
                var payed_amt = parseFloat($("#lcPayedAmt").val());
                //会员卡支付
                var lcVIPPaymentMode = $("#LCVIPPaymentMode").val();
                if (lcVIPPaymentMode == "" || lcVIPPaymentMode == null) {
                    lcVIPPaymentMode = 0;
                }


                //剩余还需支付
                var balance = payed_amt - parseFloat(payTotal) - parseFloat(lcVIPPaymentMode);
                //疗程抵用金额
                var isCheck = $("#cbxIsLcdy").prop("checked").toString().toUpperCase();
                if (isCheck == "TRUE") {
                    //抵用金额
                    var dyAmt = parseFloat($("#spanDYAmt").html());
                    balance = f.floatSubtract(balance, dyAmt);
                }


                if (balance.toFixed(2) < 0) {
                    _showInfoMessage("支付总额不能大于还需支付金额！", "info");
                    ob.value = 0;

                    var lcpaymentMode = $('input[name=LCPaymentMode]');
                    var payTotal = 0;
                    $(lcpaymentMode).each(function () {
                        if (this.value != "" && this.value != null) {
                            payTotal += parseFloat(this.value);
                        }
                    });
                    //付款金额
                    var lcPayedAmt = parseFloat($("#lcPayedAmt").val());
                    //疗程抵用金额
                    var isCheck = $("#cbxIsLcdy").prop("checked").toString().toUpperCase();
                    if (isCheck == "TRUE") {
                        //抵用金额
                        var dyAmt = parseFloat($("#spanDYAmt").html());
                        lcPayedAmt = lcPayedAmt - dyAmt;
                    }
                    var lcpayed = lcPayedAmt - payTotal - lcVIPPaymentMode;
                    $("#spanLCPayed").html(lcpayed.toFixed(2));

                    return false;
                }

                //疗程抵用金额
                //var isCheck = $("#cbxIsLcdy").prop("checked").toString().toUpperCase();
                //if (isCheck == "TRUE") {
                //    //抵用金额
                //    var dyAmt = parseFloat($("#spanDYAmt").html());
                //    balance = balance - dyAmt;
                //}

                if (balance.toFixed(2) == 0) {
                    $("#btnSaveOrder").removeAttr("disabled");
                }
                else {
                    $("#btnSaveOrder").attr("disabled", "disabled");
                }

                $("#spanLCPayed").html(balance.toFixed(2));
            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };
        util.keyUpVipLCPayment = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {
                var vipCARD_TYPE = $("#VIPCARD_TYPE").val();
                if (vipCARD_TYPE == "" || vipCARD_TYPE == null) {
                    _showInfoMessage("请选择会员卡！", "info");
                    ob.value = "0";
                    return false;
                }
                if (ob.value == "" || ob.value == null) {
                    ob.value = 0;
                }
                var lcpaymentMode = $('input[name=LCPaymentMode]');
                var payTotal = 0;
                $(lcpaymentMode).each(function () {
                    if (this.value != "" && this.value != null) {
                        payTotal += parseFloat(this.value);
                    }
                });
                ////应付金额
                //var payed_amt = parseFloat($("#spanTotalAmt").html());
                var payed_amt = parseFloat($("#lcPayedAmt").val());
                var balance = payed_amt - parseFloat(ob.value) - payTotal;
                //疗程抵用金额
                var isCheck = $("#cbxIsLcdy").prop("checked").toString().toUpperCase();
                if (isCheck == "TRUE") {
                    //抵用金额
                    var dyAmt = parseFloat($("#spanDYAmt").html());
                    balance = f.floatSubtract(balance, dyAmt);
                }
                //if (parseFloat(balance) == 0) {
                //    $("#btnSaveOrder").removeAttr("disabled");
                //} else {
                //    $("#btnSaveOrder").attr("disabled", "disabled");
                //}
                if (balance.toFixed(2) < 0) {
                    _showInfoMessage("支付总额不能大于还需支付金额！", "info");
                    ob.value = 0;

                    var lcpaymentMode = $('input[name=LCPaymentMode]');
                    var payTotal = 0;
                    $(lcpaymentMode).each(function () {
                        if (this.value != "" && this.value != null) {
                            payTotal += parseFloat(this.value);
                        }
                    });
                    //付款金额
                    var lcPayedAmt = parseFloat($("#lcPayedAmt").val());


                    //疗程抵用金额
                    var isCheck = $("#cbxIsLcdy").prop("checked").toString().toUpperCase();
                    if (isCheck == "TRUE") {
                        //抵用金额
                        var dyAmt = parseFloat($("#spanDYAmt").html());
                        lcPayedAmt = lcPayedAmt - dyAmt;
                    }

                    var lcpayed = f.floatSubtract(lcPayedAmt, payTotal);
                    $("#spanLCPayed").html(lcpayed.toFixed(2));

                    return false;
                }

                ////疗程抵用金额
                //var isCheck = $("#cbxIsLcdy").prop("checked").toString().toUpperCase();
                //if (isCheck == "TRUE") {
                //    //抵用金额
                //    var dyAmt = parseFloat($("#spanDYAmt").html());
                //    balance = balance - dyAmt;
                //}

                if (balance.toFixed(2) == 0) {
                    $("#btnSaveOrder").removeAttr("disabled");
                }
                else {
                    $("#btnSaveOrder").attr("disabled", "disabled");
                }
                $("#spanLCPayed").html(balance.toFixed(2));
            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };

        //抵用疗程次数
        util.keyUpDYLC = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {
                if (ob.value > 0) {
                    //剩余次数
                    var ava_Qty = $(ob).parent("td").parent("tr").children("td").eq(2).html();
                    if (parseInt(ob.value) > parseInt(ava_Qty)) {
                        //输入抵用次数大于疗程项目剩余次数
                        //_showInfoMessage("抵用次数不能大于剩余次数！", "info");
                        ob.value = 0;
                        //抵用金额-0
                        $(ob).parent("td").parent("tr").children("td").eq(4).html(0);
                        $(ob).parent("td").parent("tr").children("td").eq(5).find("input[NAME$=hideDYAmt]").val(0);
                        //计算抵用金额
                        getDYAmt();
                        return false;
                    }
                    //单价
                    var unitPrice = $(ob).parent("td").parent("tr").children("td").eq(1).html();
                    //抵用金额
                    var dyPrice = f.floatMulti(parseInt(ob.value), parseFloat(unitPrice))
                    //抵用金额
                    $(ob).parent("td").parent("tr").children("td").eq(4).html(dyPrice.toFixed(2));
                    //抵用金额
                    $(ob).parent("td").parent("tr").children("td").eq(5).find("input[NAME$=hideDYAmt]").val(dyPrice.toFixed(2));

                    //抵用金额合计
                    //var spanDYPrice = $("#spanDYPrice").html();
                    //$("#spanDYPrice").html()

                }
                else {
                    ob.value = 0;
                    $(ob).parent("td").parent("tr").children("td").eq(4).html(0);
                    $(ob).parent("td").parent("tr").children("td").eq(5).find("input[NAME$=hideDYAmt]").val(0);
                }
                //计算抵用金额
                getDYAmt();
            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };
        //抵用金额
        function getDYAmt() {
            var totalprice = 0;
            $("#tLCPro tbody tr").each(function () {
                var price = $(this).children("td").eq(4).html();
                totalprice = f.floatAdd(parseFloat(totalprice), parseFloat(price));
            });
            //抵用金额合计
            $("#spanDYPrice").html(totalprice);
        }

        var f = new Object();
        //获取参数精度，如果为整数则精度为0  
        f._getPrecision = function (arg) {
            if (arg.toString().indexOf(".") == -1) {
                return 0;
            } else {
                return arg.toString().split(".")[1].length;
            }
        }
        //获取小数的整数形式  
        f._getIntFromFloat = function (arg) {
            if (arg.toString().indexOf(".") == -1) {
                return arg;
            } else {
                return Number(arg.toString().replace(".", ""));
            }
        }
        //乘法  
        f.floatMulti = function (arg1, arg2) {
            var precision1 = this._getPrecision(arg1);
            var precision2 = this._getPrecision(arg2);
            var tempPrecision = 0;

            tempPrecision += precision1;
            tempPrecision += precision2;
            var int1 = this._getIntFromFloat(arg1);
            var int2 = this._getIntFromFloat(arg2);
            return (int1 * int2) * Math.pow(10, -tempPrecision);
        }
        //加法  
        f.floatAdd = function (arg1, arg2) {
            var precision1 = this._getPrecision(arg1);
            var precision2 = this._getPrecision(arg2);
            var temp = Math.pow(10, Math.max(precision1, precision2));
            return (this.floatMulti(arg1, temp) + this.floatMulti(arg2, temp)) / temp;
        }
        //减法  
        //arg1 被减数  
        //arg2 减数  
        f.floatSubtract = function (arg1, arg2) {
            var precision1 = this._getPrecision(arg1);
            var precision2 = this._getPrecision(arg2);
            var temp = Math.pow(10, Math.max(precision1, precision2));
            return (this.floatMulti(arg1, temp) - this.floatMulti(arg2, temp)) / temp;
        }
        ////除法  
        //arg1 被除数  
        //arg2 除数  
        f.floatDiv = function (arg1, arg2) {
            var precision1 = this._getPrecision(arg1);
            var precision2 = this._getPrecision(arg2);
            var int1 = this._getIntFromFloat(arg1);
            var int2 = this._getIntFromFloat(arg2);
            var result = (int1 / int2) * Math.pow(10, precision2 - precision1);
            return result;
        }

        //价格格式化
        util.formatPrice = function (value) {
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

        //********************//


        //*************充值开始**************//

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

            /////如果有欠款
            //var arrears = $("#ARREARS").val();
            //if (arrears != "") {
            //    var cbxCheck = $('input[name=cbxCheck]');
            //    if (parseFloat(arrears) == 0) {
            //        $(cbxCheck).each(function () {
            //            $(this).removeAttr("disabled");
            //        });
            //        //$("#Present_Prod").removeAttr("disabled");
            //    }
            //    else {
            //        //有欠款
            //        alert(1);
            //        $(cbxCheck).each(function (i) {
            //            $(this).attr("disabled", "disabled");
            //        });
            //        // $("#Present_Prod").attr("disabled", "disabled");
            //    }
            //}


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

        //卡支付方式
        util.keyUpPresent = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {

                if (ob.value == "" || ob.value == null || ob.value <= 0) {
                    ob.value = 1;
                    return false;
                }

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
                    totalprice = 0;
                    $(ob).parent("td").parent("tr").find("input[NAME$=cbxCheck]").attr("checked", false);
                    $(ob).parent("td").parent("tr").find("input[NAME$=prodNum]").val("1");

                    $("#tProdItem tbody tr").each(function () {
                        var cbx = $(this).find("input[NAME$=cbxCheck]").is(':checked');
                        if (cbx) {
                            var price = parseFloat($(this).children("td").eq(2).html());
                            var num = parseInt($(this).find("input[NAME$=prodNum]").val());
                            totalprice += parseFloat(price * num);
                        }
                    });
                    $("#spanTotalPrice").html(totalprice);
                    return false;
                }
                $("#spanTotalPrice").html(totalprice);


            }
            catch (ex) {
                _showInfoMessage("数据填写有误！" + ex.message, "error");
            }
        };
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

        //续清
        //var cardId = $("#hideCard_Id").val();
        //if (cardId != "" && cardId != null) {
        //    //PayCustCard(cardId);
        //}
        //会员卡信息
        function PayCustCard(cardId) {
            //根据卡级ID检索卡信息
            $.post(options.QueryCardByIDUrl, { cardId: cardId },
                  function (ret) {
                      if (ret.status > 0) {
                          var data = eval(ret.data);

                          $("#CARD_ID").val(data.CARD_ID);
                          $("#hideCustCardId").val(data.CARD_ID);
                          $("#CARD_ID").attr("disabled", "disabled");
                          $("#Masseur").val(data.EMP_ID);
                          $("#BUY_AMT").val(data.BUY_AMT);
                          $("#ARREARS").val(data.ARREARS);
                          $("#PAYED_AMT").val("0");
                          $("#spanPayed").html("0");

                          $("#hideARREARS").val(data.ARREARS);

                          //卡级赠送项目
                          CardChange();
                      }
                  },
                  "json"
                 );
        };

        //疗程卡信息
        function PayCustLCCard(cardId) {

            //根据卡级ID检索卡信息
            $.post(options.QueryCardByIDUrl, { cardId: cardId },
                  function (ret) {
                      if (ret.status > 0) {
                          var data = eval(ret.data);

                          $("#LCCARD_PACK").val(data.CARD_ID);
                          $("#LCCARD_PACK").attr("disabled", "disabled");
                          $("#MasseurLCC").val(data.EMP_ID);

                          //应付总额
                          $("#spanTotalAmt").html(data.BUY_AMT);
                          //欠款金额
                          $("#lcPayedAmt").val(data.ARREARS);
                          $("#spanArrearsAmt").html(data.ARREARS);
                          //已付金额
                          $("#spanPayedAmt").html(parseFloat(data.BUY_AMT) - parseFloat(data.ARREARS));
                          //还需支付
                          $("#spanLCPayed").html(data.ARREARS);
                          //$("#BUY_AMT").val(data.BUY_AMT);
                          //$("#ARREARS").val(data.ARREARS);
                          //$("#PAYED_AMT").val("0");
                          //$("#spanPayed").html("0");

                          //$("#hideARREARS").val(data.ARREARS);

                          ////卡级赠送项目
                          //CardChange();
                          //显示已购买的疗程卡
                          showLCCPacked(cardId);

                      }
                  },
                  "json"
                 );
        };
        //*************充值结束**************//
        //$("#spanDYLC").click(function () {
        //    if ($("#divDYLC").css("display") == "none") {
        //        $("#divDYLC").show();
        //        $("#spanDYLC").html("抵用疗程【收起】");
        //    }
        //    else {
        //        $("#divDYLC").hide();
        //        $("#spanDYLC").html("抵用疗程【展开】");
        //    }
        //});
        //选择抵用疗程
        $("#LCCARD_CUST").change(function () {
            var cardId = $("#LCCARD_CUST").val();
            if (cardId == "" || cardId == null) {
                _showInfoMessage("请选择抵用疗程！", "info");
                return false;
            }
            showLCCCust();
        });

        function showLCCCust() {
            var cardId = $("#LCCARD_CUST").val();//疗程卡套餐
            showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 700, top: 450 });
            $.post("/Order/GetLCCust.do", { cardId: cardId },
                   function (ret) {
                       if (ret.status > 0) {
                           var data = eval(ret.data);
                           $("#tLCItem tbody tr").remove();
                           var proListHtml = [];
                           $(data).each(function (i) {
                               proListHtml.push("<tr class='strip'>");
                               proListHtml.push("<td>");
                               proListHtml.push("<input type='checkbox' />");
                               proListHtml.push("</td>");
                               proListHtml.push("<td style=\"width: 55%;\"><div style=\"text-align:left;\">", this.SERVICE_NAME, "</div></td>");
                               proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", this.SERVICE_PRICE, "</div></td>");
                               proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", this.AVA_QTY, "</div></td>");
                               proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", parseFloat(this.SERVICE_PRICE) * parseInt(this.AVA_QTY), "</div></td>");

                               proListHtml.push("</tr>");

                           });
                           $(proListHtml.join('')).appendTo("#tLCItem tbody");
                           hideLoadingMsg();
                       }
                   },
                   "json"
                  );

            //else {
            //    //$("#tSpeedyPro tbody").html("");
            //    ////疗程总价
            //    //$("#spanTotalAmt").html(0);
            //    ////$("#spanBalanceAmt").html(0);
            //    //$("#lcPayedAmt").val(0);

            //}

        }

        $("#btnSelectLC").click(function () {
            var lcpack = $("#LCCARD_PACK").val();
            if (lcpack == "" || lcpack == null) {
                _showInfoMessage("请您先选择疗程套餐！", "info");
                return false;
            }
            $('#LCModal').modal('show');
            return false;
        });

        //选择我的疗程项目
        util.SelectCbx = function (obj) {
            //var isCheck = $(obj).prop("checked").toString().toUpperCase();
            ////小计
            //var xjPrice = $(obj).parent("td").parent("tr").children("td").eq(4).html();
            ////抵用金额
            //var spanDYPrice = $("#spanDYPrice").html();
            //if (isCheck == "TRUE") {
            //    //选中
            //    spanDYPrice = f.floatAdd(parseFloat(xjPrice), parseFloat(spanDYPrice));
            //}
            //else {
            //    //取消选中
            //    spanDYPrice = f.floatSubtract(parseFloat(spanDYPrice), parseFloat(xjPrice));
            //}
            //$("#spanDYPrice").html(spanDYPrice);
        }
        //全选
        $("#cbxLCAll").click(function () {
            var isCheck = $(this).prop("checked").toString().toUpperCase();
            if (isCheck == "TRUE") {
                $('input[name="SelectCbx"]').prop("checked", true);//全选
                var totalprice = 0;
                $("#tLCPro tbody tr").each(function () {
                    var price = $(this).children("td").eq(4).html();
                    totalprice = f.floatAdd(parseFloat(totalprice), parseFloat(price));
                });
                $("#spanDYPrice").html(totalprice);
            }
            else {
                $('input[name="SelectCbx"]').prop("checked", false);//取消全选
                $("#spanDYPrice").html(0);
            }
        });
        //保存
        $("#btnLCSave").click(function () {
            //var dyamt = parseFloat($("#spanDYAmt").html());//抵用金额
            //var lcPayed = parseFloat($("#spanLCPayed").html());//还需支付
            //if (dyamt > lcPayed) {
            //    _showInfoMessage("抵用金额不能大于疗程还需支付金额！", "info");
            //    return false;
            //}

            var dyprice = parseFloat($("#spanDYPrice").html());


            //var lcpayedAmt = lcPayed - dyprice;
            //$("#spanLCPayed").html(lcpayedAmt.toFixed(2));//还需支付

            //var dyamt = parseFloat($("#spanDYAmt").html());//抵用金额
            var lcpaymentMode = $('input[name=LCPaymentMode]');
            var payTotal = 0;
            //其他方式支付总额
            $(lcpaymentMode).each(function () {
                if (this.value != "" && this.value != null) {
                    payTotal += parseFloat(this.value);
                }
            });

            //会员卡支付
            var lcVIPPaymentMode = $("#LCVIPPaymentMode").val();
            if (lcVIPPaymentMode == "" || lcVIPPaymentMode == null) {
                lcVIPPaymentMode = 0;
            }
            //付款金额
            var lcPayedAmt = parseFloat($("#lcPayedAmt").val());

            //会员卡和其他方式已支付总额
            var payedAmts = f.floatAdd(payTotal, lcVIPPaymentMode);
            payedAmts = f.floatAdd(payedAmts, dyprice);


            //还需支付金额
            var lcpayed = f.floatSubtract(lcPayedAmt, payedAmts);
            if (parseFloat(lcpayed) == 0) {
                $("#btnSaveOrder").removeAttr("disabled");
            }
            else if (parseFloat(lcpayed) < 0) {
                $("#btnSaveOrder").attr("disabled", "disabled");
                _showInfoMessage("抵用金额不能大于还需支付金额！", "info");
                return false;
            }
            $("#spanDYAmt").html(dyprice);//抵用金额
            $("#hideLCDYAmt").val(dyprice);
            //还需支付
            $("#spanLCPayed").html(lcpayed.toFixed(2));

            $('#LCModal').modal('hide');

        });
        //抵用疗程选择
        $("#cbxDYLC").change(function () {
            var isCheck = $(this).prop("checked").toString().toUpperCase();
            var dyamt = parseFloat($("#spanDYAmt").html());//抵用金额
            var lcPayed = parseFloat($("#spanLCPayed").html());//还需支付
            if (isCheck == "TRUE") {
                //选中
                if (dyamt <= 0) {
                    _showInfoMessage("请您先选择疗程抵用！", "info");
                    return false;
                }
                if (lcPayed <= 0) {
                    _showInfoMessage("还需支付金额为0，不能够使用疗程抵用！", "info");
                    return false;
                }
                if (dyamt > lcPayed) {
                    //$("#spanLCPayed").html(0);
                    //$("#spanRetAmt").html(f.floatSubtract(dyamt,lcPayed));
                    //$("#labelRetAmt").show();
                    _showInfoMessage("抵用金额不能大于疗程还需支付金额！", "info");
                    return false;
                }

                $("#spanLCPayed").html(f.floatSubtract(lcPayed, dyamt));
            }
            else if (isCheck == "FALSE") {
                $("#spanLCPayed").html(f.floatAdd(lcPayed, dyamt));
            }

        });

        //是否使用疗程抵用
        $("#cbxIsLcdy").change(function () {
            var isCheck = $(this).prop("checked").toString().toUpperCase();
            //应付
            var totalAmt = parseFloat($("#spanTotalAmt").html());
            var dyamt = parseFloat($("#spanDYAmt").html());//抵用金额
            var lcPayed = parseFloat($("#spanLCPayed").html());//还需支付

            var lcpaymentMode = $('input[name=LCPaymentMode]');
            var payTotal = 0;
            //其他方式支付总额
            $(lcpaymentMode).each(function () {
                if (this.value != "" && this.value != null) {
                    payTotal += parseFloat(this.value);
                }
            });

            //会员卡支付
            var lcVIPPaymentMode = $("#LCVIPPaymentMode").val();
            if (lcVIPPaymentMode == "" || lcVIPPaymentMode == null) {
                lcVIPPaymentMode = 0;
            }
            var sumpayed = f.floatAdd(payTotal, lcVIPPaymentMode);

            if (isCheck == "TRUE") {
                sumpayed = f.floatAdd(dyamt, sumpayed);
                $("#spanLCPayed").html((f.floatSubtract(totalAmt, sumpayed)).toFixed(2));
                $("#divIsLcdy").show();
            }
            else {
                $("#spanLCPayed").html((f.floatSubtract(totalAmt, sumpayed)).toFixed(2));
                $("#divIsLcdy").hide();
            }

            if (lcPayed.toFixed(2) == 0) {
                $("#btnSaveOrder").removeAttr("disabled");
            }
            else {
                $("#btnSaveOrder").attr("disabled", "disabled");
            }
        });

    });

    $("#btnCancel").click(function () {
        if (parent && parent.xjDailog) {
            parent.xjDailog.Close(null, true, 1);
        }
        else {
            window.close();
        }
    });

    $("#btnReturn").click(function () {
        if (parent && parent.xjDailog) {
            parent.xjDailog.Close(null, true, 1);
        }
        else {
            window.close();
        }
    });

    $("#btnReturnList").click(function () {
        if (parent && parent.xjDailog) {
            parent.xjDailog.Close(null, true, 1);
        }
        else {
            window.close();
        }
    });

})(window, undefined, jQuery);
