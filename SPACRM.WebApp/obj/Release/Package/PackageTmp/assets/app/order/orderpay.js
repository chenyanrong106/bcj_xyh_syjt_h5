; (function (window, undefined, $) {
    $(document).ready(function () {
        $("#btnBuyCar").pulsate({ color: "#FF0000" });
        window.history.forward(1);
        var submiting = false;
        $('#formPaySave').validator({
            valid: function (form) {
                if (submiting) {
                    return;
                }
                submiting = true;
                FormSubmit(form, function (res) {
                    if (res.status > 0) {
                        _showInfoMessage("操作成功！", "success");
                        var cid = $("#hide_cid").val();
                        var oid = $("#hide_oid").val();

                        var left = (screen.width - 400) / 2;
                        var top = (screen.height - 500) / 2;
                        ///window.location.href = "/Order/ConsumePrint.do?cid=" + cid + "&oid=" + oid;
                        //window.open("/Order/ConsumePrint.do?cid=" + cid + "&oid=" + oid, "_blank", "height=500,width=400,left=" + left + ",top=" + top + ",scrollbars=yes,location=no,resizable=yes");
                        ////window.location.href = "/Customer/View360.do?cid=" + $("#hideCUST_ID").val();
                        //window.location.href = "/Order/OrderB.do?cid="+cid+"&oid="+oid;
                        //alert($("#orgname").val());
                        if ($("#orgname").val() == "ESEN GROUP") {
                            var url = "/Order/OrderPrint.do?cid=" + cid + "&oid=" + oid;

                        }
                        else {
                            var url = "/Order/ConsumePrint.do?cid=" + cid + "&oid=" + oid;
                        }
                        PopupWindow(url, 400, 500, true, "yes");
                        window.location.href = "/Order/OrderB.do?cid=" + cid + "&oid=" + oid;
                    }
                    else {
                        _showInfoMessage("操作失败！" + res.message, "error");
                        $("#btnSave").removeAttr("disabled");
                    }
                    $("#btnSave").removeAttr("disabled");
                    submiting = false;
                })
            }
        });

        //****************添加付款--储值卡
        $("#btnVIPCARD").click(function () {
            //if ($("#VIPCARD_TYPE").val() == "" || $("#VIPCARD_TYPE").val() == undefined) {
            //    _showInfoMessage('请您先选择会员卡！', 'info');
            //    return false;
            //}

            //项目抵扣金额
            var serAmt = $("#payMoneyVIPCARD").val();
            if (serAmt != "" && parseFloat(serAmt) > 0) {
                var cashType = $("#VIPCARD_TYPE").val();
                var cashName = $("#VIPCARD_TYPE").find("option:selected").text();
                var cardNames = cashName.toString().split("|");

                //还可抵扣金额
                var spanSerAmt = f.floatSubtract(parseFloat($("#spanSerAmt").html()), parseFloat($("#payMoneyVIPCARD").val()));
                if (parseFloat(spanSerAmt) < 0) {
                    _showInfoMessage('项目支付金额不能大于项目抵扣金额！', 'info');
                    return false;
                }
                $("#spanSerAmt").html(spanSerAmt);

                util.addPayList("5", "会员卡支付", cashType, cardNames[0], $("#payedMoneyVIPCARD").val(), $("#payMoneyVIPCARD").val(), "2");
            }

            //产品抵扣金额
            var proAmt = $("#payMoneyProCARD").val();
            if (proAmt != "" && parseFloat(proAmt) > 0) {
                var cashType = $("#VIPCARD_TYPE").val();
                var cashName = $("#VIPCARD_TYPE").find("option:selected").text();
                var cardNames = cashName.toString().split("|");
                //还可抵扣金额
                var spanProAmt = f.floatSubtract(parseFloat($("#spanProAmt").html()), parseFloat($("#payMoneyProCARD").val()));
                if (parseFloat(spanProAmt) < 0) {
                    _showInfoMessage('产品支付金额不能大于产品抵扣金额！', 'info');
                    return false;
                }
                $("#spanProAmt").html(spanProAmt);

                util.addPayList("5", "会员卡支付", cashType, cardNames[0], $("#payedMoneyProCARD").val(), $("#payMoneyProCARD").val(), "0");//产品
            }

            $("#payedMoneyVIPCARD").val("");
            $("#payMoneyVIPCARD").val("");

            $("#payedMoneyProCARD").val("");
            $("#payMoneyProCARD").val("");

        });
        //****************现金/
        $("#btnCash").click(function () {
            var payMoney = $("#payedMoney").val();
            if (payMoney == "" || payMoney == null) {
                _showInfoMessage('请您输入付款金额！', 'info');
                return false;
            }
            util.addPayList("0", "现金", "0", "现金", $("#payedMoney").val(), $("#payedMoney").val(), "");
            $("#payedMoney").val("");
        });
        //刷卡
        $("#btnPOSTCARD").click(function () {
            if ($("#POSTCARD_TYPE").val() == "") {
                _showInfoMessage('请您先选择银联POS卡！', 'info');
                return false;
            }
            if ($.trim($("#payMoneyPOSTCARD").val()) == "") {
                _showInfoMessage('请您输入付款金额！', 'info');
                return false;
            }
            var cardType = $("#POSTCARD_TYPE").val();
            var cardName = $("#POSTCARD_TYPE").find("option:selected").text();
            util.addPayList("1", "银联POS", cardType, cardName, $("#payMoneyPOSTCARD").val(), $("#payMoneyPOSTCARD").val(), "");
            $("#payMoneyPOSTCARD").val("");
            return false;
        });

        //应收账款
        $("#btnYSZK").click(function () {
            if ($("#RECEIVABLES").val() == "") {
                _showInfoMessage('请您先选择应收账款！', 'info');
                return false;
            }
            if ($.trim($("#payYSZK").val()) == "") {
                _showInfoMessage('请您输入付款金额！', 'info');
                return false;
            }
            var cardType = $("#RECEIVABLES").val();
            var cardName = $("#RECEIVABLES").find("option:selected").text();
            util.addPayList("2", "应收账款", cardType, cardName, $("#payYSZK").val(), $("#payYSZK").val(), "");
            $("#payYSZK").val("");
            return false;
        });
        //校验优惠券信息 
        $("#btnCheckPROM").click(function () {
            var vocher_no = $("#txtVOUCHAR_NO").val();
            if ($.trim(vocher_no) == "") {
                _showInfoMessage('请您先输入优惠券编码！', 'info');
                return false;
            }
            $.post(options.checkVOCHER_NO, { VOCHER_NO: vocher_no },
                 function (ret) {
                     if (ret.status == 1) {
                         //var mes = "优惠券是由" + ret.data.STORE_NAME + "发放，面值：" + ret.data.FACE_VALUE + "元";
                         var mes = "优惠券面值：" + ret.data.values + "元";
                         $("#Pro_Voucher").html(mes);
                         $("#Pro_FaceValue").val(ret.data.values);//优惠券面值
                         $("#Pro_No").val(vocher_no);
                         $("#Pro_Id").val(ret.data.ids);
                         var xmmoney = 0;
                         $("#xmpay li [name='xmPaymentMode'").each(function () {
                             if ($(this).attr("id") != "Pro_FaceValue") {
                                 xmmoney = parseFloat($(this).val() == "" ? "0" : $(this).val()) + xmmoney;
                             }
                         });
                         $("#xmspanPayed").html(($("#xmamt").val() - xmmoney).toFixed(0));
                        // alert($("#xmspanPayed").html() + "," + document.getElementById('Pro_FaceValue').value + "," + ( parseFloat( $("#xmspanPayed").html()) <parseFloat( document.getElementById('Pro_FaceValue').value)));
                         if (parseFloat($("#xmspanPayed").html()) < parseFloat(document.getElementById('Pro_FaceValue').value)) {
                             document.getElementById('Pro_FaceValue').value = $("#xmspanPayed").html();
                         }
                         xmpaymoney(document.getElementById('Pro_FaceValue'));

                     } else if (ret.status == 0) {
                         //$("#Pro_Voucher").html("未找到该优惠券信息！请核实编码是否正确");
                         _showInfoMessage(ret.message, 'info');
                         $("#Pro_FaceValue").val(0);//优惠券面值
                     }
                     else if (ret.status == -1) {
                         $("#Pro_Voucher").html(ret.message);
                     }
                 },
                 "json"
                );

        });
        //*****************优惠券
        $("#btnPROM").click(function () {
            if ($.trim($("#txtVOUCHAR_NO").val()) == "") {
                _showInfoMessage('请您先输入优惠券编码！', 'info');
                return false;
            }
            if ($.trim($("#Pro_FaceValue").val()) == 0) {
                _showInfoMessage('请您先校验优惠券编码！', 'info');
                return false;
            }


            //如果已经添加了优惠券 
            var isadd = false;
            $("#tablePayList tbody tr").each(function () {
                var voucharNo = $(this).children("td").eq(1).html();
                if (voucharNo == $("#Pro_No").val()) {
                    isadd = true;
                }
            });
            if (isadd) {
                _showInfoMessage('您已添加了该优惠券！', 'info');
                return false;
            }

            //var cashType = $("#Pro_FaceValue").val();
            //var cashName = $("#Pro_FaceValue").val();
            var facevalue = $("#Pro_FaceValue").val();
            //如果面值大于应付金额
            if (parseFloat(facevalue) > parseFloat($("#spanResAmount").html())) {
                facevalue = $("#spanResAmount").html();
            }
            util.addPayList("3", "代金券", $("#Pro_Id").val(), $("#Pro_No").val(), facevalue, facevalue, "");
            $("#txtVOUCHAR_NO").val("");
            $("#Pro_Voucher").html("");


        });
        //储值卡选择事件
        //$("#VIPCARD_TYPE").change(function () {
        //    var id = $("#VIPCARD_TYPE").val();
        //    if (id > 0) {
        //        $.post(options.queryCardDetail, { id: id },
        //            function (data) {
        //                if (data.status == 1) {
        //                    //产品折扣
        //                    $("#hide_ProCARD_RATE").val(data.data.DISCOUNT_RATE2);
        //                    //项目折扣
        //                    $("#hide_SerCARD_RATE").val(data.data.DISCOUNT_RATE);
        //                    //欠款
        //                    $("#hide_Arrears").val(data.data.PRICE);
        //                }
        //            },
        //            "json"
        //           );
        //    }
        //});

        //支付列表 deduMoney=抵扣金额
        util.addPayList = function (payType, payTypeName, cashType, cashName, payMoney, deduMoney, prodType) {
            var price = f.floatSubtract(parseFloat($("#spanResAmount").html()), parseFloat(deduMoney));
            if (price < 0) {
                _showInfoMessage("支付总额不能大于还需支付总额！", "error");
                return false;
            }
            $("#spanResAmount").html(price);
            var html = [];
            html.push('<tr>');
            html.push('<td>' + payTypeName + '</td>');
            html.push('<td>' + cashName + '</td>');
            html.push('<td>￥' + deduMoney + '</td>');//util.formatPrice(deduMoney)
            html.push('<td>￥' + payMoney + '</td>');
            html.push('<td><a title="删除" onclick="util.delPay(this,' + deduMoney + ')" class="abtn" href="javascript:;" ><i class="fa fa-times" ></i></a><td>');

            html.push('<td><input type="hidden" name="payType" value=' + payType + ' />');
            html.push('<input type="hidden" name="cashType" value=' + cashType + ' />');
            html.push('<input type="hidden" name="deduMoney" value=' + deduMoney + ' />');//抵扣金额
            html.push('<input type="hidden" name="payMoney" value=' + payMoney + ' /></td>');//实际支付
            html.push('<input type="hidden" name="prodType" value="' + prodType + '" /></td>');

            html.push('</tr>');
            $(html.join("")).appendTo("#tablePayList tbody");

            var mess = cashName + "支付￥" + payMoney + "。";
            _showInfoMessage(mess, "success");

            if (price > 0) {
                $("#btnSubmit").hide();
                $("#btnContPay").show();
                $("#divResAmount").show();
            }
            else {
                $("#divResAmount").hide();
                $("#btnContPay").hide();
                $("#btnSubmit").show();

                $("#divPayType").hide();
                $("#divPayTypeContent").hide();
            }

            //$("#divPayType").hide();
            //$("#divPayTypeContent").hide();
            //$("#divBtnSave").show();
        };

        util.delPay = function delPay(row, price) {
            //删除支付方式 
            $(row).parent("td").parent("tr").remove();

            var newprice = f.floatAdd(parseFloat($("#spanResAmount").html()), parseFloat(price));
            $("#spanResAmount").html(newprice);

            var prodType = $(row).parent("td").parent("tr").find("input[NAME$=prodType]").val();
            if (prodType == "0") {
                //产品
                var proAmt = f.floatAdd(parseFloat($("#spanProAmt").html()), parseFloat(price));
                $("#spanProAmt").html(proAmt);
            }
            else if (prodType == "2") {
                //项目
                var serAmt = f.floatAdd(parseFloat($("#spanSerAmt").html()), parseFloat(price));
                $("#spanSerAmt").html(serAmt);
            }
            if (newprice > 0) {
                $("#btnSubmit").hide();
                $("#btnContPay").show();
                $("#divResAmount").show();


                $("#divPayType").show();
                $("#divPayTypeContent").show();
            }
            else {
                $("#divResAmount").hide();
                $("#btnContPay").hide();
                $("#btnSubmit").show();
            }
        };
        //下单出票
        $("#btnSave").click(function () {
            var payway = $("#tablePayList tbody tr").length;
            if (payway <= 0) {
                _showInfoMessage("请添加支付！", "info");
                return false;
            }
            var resAmount = parseFloat($("#spanResAmount").html());
            if (resAmount > 0) {
                _showInfoMessage("还需支付￥" + resAmount, "info");
                return false;
            }
            $("#btnSave").attr("disabled", "disabled");
            $("#formPaySave").submit();
        });

        $("#btnSave2").click(function () {
            if (parseFloat($("#spanResAmount").html()) != 0) {
                _showInfoMessage("还需支付￥" + $("#spanResAmount").html(), "info");
                return false;
            }
            //var payway = $("#tablePayList tbody tr").length;
            //if (payway <= 0) {
            //    _showInfoMessage("请添加支付！", "info");
            //    return false;
            //}
            //var resAmount = parseFloat($("#spanResAmount").html());
            //if (resAmount > 0) {
            //    _showInfoMessage("还需支付￥" + resAmount, "info");
            //    return false;
            //}
            $("#btnSave").attr("disabled", "disabled");
            $("#formPaySave").submit();
        });
        //返回
        $("#btnReturn").click(function () {
            var cid = $("#hide_cid").val();
            var oid = $("#hide_oid").val();
            window.location.href = "/Order/OrderD.do?cid=" + cid + "&oid=" + oid;;
        });

        //打印订单
        $("#btnOrderPrint").click(function () {
            var left = (screen.width - 400) / 2;
            var top = (screen.height - 500) / 2;
            var url = options.printUrl;
            if ($("#orgname").val() == "ESEN GROUP") {
                var url = options.printEnUrl;
            }

            //var url = "/Order/ConsumeOrderPrint.do?cid=" + $("#hide_cid").val() + "&oid=" + $("#hide_oid").val();
            PopupWindow(url, 400, 500, true, "yes");

        });

        util.keyPress = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
        };
        //抵扣金额
        util.keyUp = function (ob) {
            //if ($("#VIPCARD_TYPE").val() == "") {
            //    _showInfoMessage('请您先选择会员卡！', 'info');
            //    ob.value = 0;
            //    return false;
            //}
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            //输入的抵扣金额
            var vvalue = "";
            if (ob.value == "" || ob.value == null) {
                vvalue = 0;
            }
            else {
                vvalue = ob.value;
            }

            //alert(ob.value);
            //储值卡项目扣率
            var vipcardrate = $("#hide_SerCARD_RATE").val();

            //允许卡支付但不享受折扣金额
            var cardPay = $("#spanCardPay").html();
            //允许卡支付并享受折扣金额
            var cardDistPay = $("#spanCardDistPay").html();
            if (parseFloat(vvalue) > parseFloat($("#spanSerAmt").html())) {
                _showInfoMessage('项目抵扣金额不能大于' + $("#spanSerAmt").html(), 'info');
                ob.value = f.toFixed(parseFloat($("#spanSerAmt").html()));
                vvalue = f.toFixed(ob.value);
            }

            //实际应付
            var payprice = 0;
            //1.抵扣金额小于享受折扣金额
            if (parseFloat(vvalue) <= parseFloat(cardDistPay)) {
                payprice = f.floatMulti(parseFloat(vvalue), parseFloat(vipcardrate));
            }
            else if (parseFloat(vvalue) > parseFloat(cardDistPay) && parseFloat(vvalue) <= f.floatAdd(parseFloat(cardDistPay), parseFloat(cardPay))) {
                payprice = f.floatMulti(parseFloat(cardDistPay), parseFloat(vipcardrate));
                payprice = f.floatAdd(parseFloat(payprice), f.floatSubtract(parseFloat(vvalue), parseFloat(cardDistPay)));
            }
            else if (parseFloat(vvalue) > f.floatAdd(parseFloat(cardDistPay), parseFloat(cardPay))) {

            }
            //四舍五入么？
            var pointMode = $("#hide_Point_Mode").val();
            var arrears = $("#hide_Arrears").val();
            if (arrears != "") {
                if (parseFloat(arrears) > 0) {
                    //欠款 不打折
                    $("#payedMoneyVIPCARD").val(vvalue);
                }
                else {
                    //无欠款
                    if (pointMode == "1") {
                        $("#payedMoneyVIPCARD").val(f.toRound(payprice));
                    }
                    else {
                        $("#payedMoneyVIPCARD").val(f.toFixed(payprice));
                    }
                }
            }
            else {
                if (pointMode == "1") {
                    $("#payedMoneyVIPCARD").val(f.toFixed(payprice));
                }
                else {
                    $("#payedMoneyVIPCARD").val(f.toRound(payprice));
                }
            }

        };
        //抵扣金额
        util.keyUpPro = function (ob) {
            //if ($("#VIPCARD_TYPE").val() == "") {
            //    _showInfoMessage('请您先选择会员卡！', 'info');
            //    ob.value = 0;
            //    return false;
            //}
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            //输入的抵扣金额
            var vvalue = "";
            if (ob.value == "" || ob.value == null) {
                vvalue = 0;
            }
            else {
                vvalue = ob.value;
            }

            //alert(ob.value);
            //储值卡产品扣率
            var vipcardrate = $("#hide_ProCARD_RATE").val();

            //允许卡支付但不享受折扣金额
            var cardPay = $("#spanCardPay").html();
            //允许卡支付并享受折扣金额
            var cardDistPay = $("#spanCardDistPay").html();

            if (parseFloat(vvalue) > parseFloat($("#spanProAmt").html())) {
                _showInfoMessage('产品抵扣金额不能大于' + $("#spanProAmt").html(), 'info');
                ob.value = parseFloat($("#spanProAmt").html());
                vvalue = ob.value;
                //return false;
            }

            //实际应付
            var payprice = 0;
            //1.抵扣金额小于享受折扣金额
            if (parseFloat(vvalue) <= parseFloat(cardDistPay)) {
                payprice = f.floatMulti(parseFloat(vvalue), parseFloat(vipcardrate));
            }
            else if (parseFloat(vvalue) > parseFloat(cardDistPay) && parseFloat(vvalue) <= f.floatAdd(parseFloat(cardDistPay), parseFloat(cardPay))) {
                payprice = f.floatMulti(parseFloat(cardDistPay), parseFloat(vipcardrate));
                payprice = f.floatAdd(parseFloat(payprice), f.floatSubtract(parseFloat(vvalue), parseFloat(cardDistPay)));
            }
            else if (parseFloat(vvalue) > f.floatAdd(parseFloat(cardDistPay), parseFloat(cardPay))) {

            }
            //四舍五入么？
            var pointMode = $("#hide_Point_Mode").val();
            //实际应付
            var arrears = $("#hide_Arrears").val();
            if (arrears != "") {
                if (parseFloat(arrears) > 0) {
                    //欠款 不打折
                    $("#payedMoneyProCARD").val(vvalue);
                }
                else {
                    //无欠款
                    if (pointMode == "1") {
                        $("#payedMoneyProCARD").val(f.toRound(payprice));
                    }
                    else {
                        $("#payedMoneyProCARD").val(f.toFixed(payprice));
                    }
                }
            }
            else {
                if (pointMode == "1") {
                    $("#payedMoneyProCARD").val(f.toRound(payprice));
                }
                else {
                    $("#payedMoneyProCARD").val(f.toFixed(payprice));
                }
            }

            //$("#payedMoneyProCARD").val(f.toFixed(payprice));
        };
        util.keyPress = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
        }
        //实际卡付反算抵扣金额
        util.keyUpPay = function (ob) {
            //if ($("#VIPCARD_TYPE").val() == "") {
            //    _showInfoMessage('请您先选择会员卡！', 'info');
            //    ob.value = 0;
            //    return false;
            //}
            var vipcard = $('#VIPCARD_TYPE option:selected').text();
            vipcard = vipcard.split("￥")[1];
            if (parseFloat(ob.value) > parseFloat(vipcard)) {
                _showInfoMessage('卡付金额不能大于卡内余额！', 'info');
                ob.value = vipcard;
                return false;
            }

            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            //输入的抵扣金额
            var vvalue = "";
            if (ob.value == "" || ob.value == null) {
                vvalue = 0;
            }
            else {
                vvalue = ob.value;
            }
            //alert(ob.value);
            //欠款
            var arrears = $("#hide_Arrears").val();
            //项目扣率
            var vipcardrate = $("#hide_SerCARD_RATE").val();

            var serAmt = parseFloat($("#spanSerAmt").html());
            //实际应付
            var payprice = 0;
            ////1.卡付金额小于享受折扣金额
            payprice = f.floatDiv(parseFloat(vvalue), parseFloat(vipcardrate));
            if (parseFloat(payprice) > serAmt) {
                _showInfoMessage('项目抵扣金额不能大于' + serAmt, 'info');

                $("#payMoneyVIPCARD").val(serAmt);
                if (parseFloat(arrears) > 0) {
                    ob.value = serAmt;
                }
                else {
                    ob.value = f.toFixed(f.floatMulti(parseFloat(serAmt), parseFloat(vipcardrate)));
                }
                return false;
            }


            if (arrears != "") {
                if (parseFloat(arrears) > 0) {
                    //欠款 不打折
                    $("#payMoneyVIPCARD").val(vvalue);
                }
                else {
                    //无欠款
                    $("#payMoneyVIPCARD").val(f.toFixed(payprice));
                }
            }
            else {
                $("#payMoneyVIPCARD").val(f.toFixed(payprice));
            }
            //实际应付
            //$("#payMoneyVIPCARD").val(f.toFixed(payprice));
        };
        //产品实际卡付反算抵扣金额
        util.keyUpPayPro = function (ob) {
            //if ($("#VIPCARD_TYPE").val() == "") {
            //    _showInfoMessage('请您先选择会员卡！', 'info');
            //    return false;
            //}
            var vipcard = $('#VIPCARD_TYPE option:selected').text();
            vipcard = vipcard.split("￥")[1];


            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            //输入的抵扣金额
            var vvalue = "";
            if (ob.value == "" || ob.value == null) {
                vvalue = 0;
            }
            else {
                vvalue = ob.value;
            }
            //欠款
            var arrears = $("#hide_Arrears").val();
            //alert(ob.value);
            //项目扣率
            var vipcardrate = $("#hide_ProCARD_RATE").val();
            //允许卡支付并享受折扣金额
            var cardDistPay = $("#spanCardDistPay").html();
            var proAmt = parseFloat($("#spanProAmt").html());
            //实际应付
            var payprice = 0;
            ////1.卡付金额小于享受折扣金额
            payprice = f.floatDiv(parseFloat(vvalue), parseFloat(vipcardrate));
            if (parseFloat(payprice) > proAmt) {
                _showInfoMessage('产品抵扣金额不能大于' + proAmt, 'info');

                $("#payMoneyProCARD").val(proAmt);
                if (parseFloat(vipcardrate) > 0) {
                    //欠款
                    ob.value = proAmt;
                }
                else {
                    ob.value = f.toFixed(f.floatMulti(parseFloat(proAmt), parseFloat(vipcardrate)));
                }
                //  ob.value = 0;
                // vvalue = ob.value;
                return false;
            }

            if (parseFloat(ob.value) > parseFloat(vipcard)) {
                _showInfoMessage('卡付金额不能大于卡内余额！', 'info');
                ob.value = vipcard;
                return false;
            }
            //实际应付
            // $("#payMoneyProCARD").val(f.toFixed(payprice));
            if (arrears != "") {
                if (parseFloat(arrears) > 0) {
                    //欠款 不打折
                    $("#payMoneyProCARD").val(vvalue);
                }
                else {
                    //无欠款
                    $("#payMoneyProCARD").val(f.toFixed(payprice));
                }
            }
            else {
                $("#payMoneyProCARD").val(f.toFixed(payprice));
            }
        };
        //现金支付
        util.keyUpCash = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
        };

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

        //********************//
        f.toFixed = function (floatvar) {
            var f_x = parseFloat(floatvar);
            if (isNaN(f_x)) {
                alert('输入有误！');
                return false;
            }
            var f_x = Math.round(floatvar * 100) / 100;
            return f_x;
        }
        f.toRound = function (value) {
            return Math.round(value);
        }
        //$("#QueryProdInfo_mask2").hide();
        //$("#QueryProdInfo_boxcontain2").hide();
        
        $("#btnOrderSK").click(function () {  //会员卡刷卡
            var height = $("#QueryProdInfo_boxcontain2").height();
            var width = $("#QueryProdInfo_boxcontain2").width();
            $("#QueryProdInfo_mask2").show();
            $("#QueryProdInfo_boxcontain2").css("top", (window.screen.height - height) / 2).css("left", (window.screen.width - width) / 2).show();

        });
        $("#QueryProdInfo_close2").click(function () { //关闭刷卡弹窗
            $("#QueryProdInfo_mask2").hide();
            $("#QueryProdInfo_boxcontain2").hide();
        });

        $("#btnduka").click(function () {
            var cardno = $("#VIPCARD_TYPE").val();
            if ($.trim(cardno) == '') {

                _showInfoMessage("请选择会员卡！", "error"); 
            } else {
                hykxz(parseInt(cardno));
            }


        });

        //var height = $("#QueryProdInfo_boxcontain2").height(); //页面打开时提示刷卡
        //var width = $("#QueryProdInfo_boxcontain2").width();
        //$("#QueryProdInfo_mask2").show();
        //$("#QueryProdInfo_boxcontain2").css("top", (window.screen.height - height) / 2).css("left", (window.screen.width - width) / 2).show();
        //var len = 0;
        //$("#VIPCARD_TYPE option").each(function () {
        //    len++;
        //});
        //if (len == 2) {
        //    $("#VIPCARD_TYPE option:last").prop("selected", 'selected');  //如果只有一张会员卡，则直接加载这张会员卡
        //    $("#btnduka").click();
        //}
        //else if (len == 1) {  //如果没有会员卡，则关闭弹窗
        //    $("#QueryProdInfo_close2").click();
        //}
        //多会员卡需要选择
    });
})(window, undefined, jQuery);


//$("#isft").change(function () {
//    $("li[name='xmhyk1']").each(function () {
//        xmjets($(this).find("input[name=xmPaymentMode]"));
//    });
//    $("li[name='cphyk1']").each(function () {
//        cpjets($(this).find("input[name=cpPaymentMode]"));
//    });
//});

function xmpaymoney(obj) {  //项目 自己计算剩余未付金额

    //alert(parseFloat($(this).val() == "" ? "0" : $(this).val()));
    var xmmoney = 0;
    $("#xmpay li [name='xmPaymentMode'").each(function () {
        xmmoney = parseFloat($(this).val() == "" ? "0" : $(this).val()) + xmmoney;
    });
    $("#xmspanPayed").html(($("#xmamt").val() - xmmoney).toFixed(0));
    if ($("#cpspanPayed").length <= 0) {
        $("#spanResAmount").html((parseFloat($("#xmspanPayed").html())).toFixed(0));
    } else {
        $("#spanResAmount").html((parseFloat($("#xmspanPayed").html()) + parseFloat($("#cpspanPayed").html())).toFixed(0));
    }
    if ($("#xmspanPayed").html() < 0) { //如果金额小于0，则将文本框还原为0
        $(obj).val(0);
        //if ($(obj).attr("id") == "xmsjkf") { //如果是项目实际卡付，则将项目抵扣金额也还原为0
        //    $("#xmdkje").val(0);
        //}
        xmpaymoney(obj);
    }
    //alert($("#cpspanPayed").length <= 0);
}

function cppaymoney(obj) {//产品 自己计算剩余未付金额
    //alert($(this).val());
    //alert(parseFloat($(this).val() == "" ? "0" : $(this).val()));
    var cpmoney = 0;
    $("#cppay li [name='cpPaymentMode'").each(function () {
        cpmoney = parseFloat($(this).val() == "" ? "0" : $(this).val()) + cpmoney;
    });
    $("#cpspanPayed").html(($("#cpamt").val() - cpmoney).toFixed(0));
    if ($("#xmspanPayed").length <= 0) {
        $("#spanResAmount").html((parseFloat($("#cpspanPayed").html())).toFixed(0));
    }
    else {
        $("#spanResAmount").html((parseFloat($("#xmspanPayed").html()) + parseFloat($("#cpspanPayed").html())).toFixed(0));
    }

    if ($("#cpspanPayed").html() < 0) {
        $(obj).val(0);
        //if ($(obj).attr("id") == "cpsjkf") { //如果是产品实际卡付，则将产品抵扣金额也还原为0
        //    $("#cpdkje").val(0);
        //}
        cppaymoney(obj);
    }
}

//$("#hide_ProCARD_RATE").val(0.7); //默认赋值0.7测试
function cpjets(obj) {//产品 实际卡付金额推算
    var lv = $(obj).parent().find("input[name=cpzk]").val();// $("#hide_ProCARD_RATE").val();
    var id = $(obj).parent().find("input[name=cpid]").val();
    $(obj).parent().next().find("input[name=cpPaymentMode2]").val(($(obj).val() * lv).toFixed(0));
    if ($("#xmsj_"+id).length <= 0) {
        $("#spanczzh_" + id).html($(obj).parent().find("input[name=cpye]").val() - $("#cpsj_" + id).val());
    }
    else {
        $("#spanczzh_" + id).html($(obj).parent().find("input[name=cpye]").val() - $("#cpsj_" + id).val() - $("#xmsj_" + id).val());
    }

    cppaymoney(obj);//调用剩余金额计算函数
    if ($("#spanczzh_"+id).html() < 0) {
        $(obj).val(0);
        cpjets(obj);
        return;
    }
    if ($("#hide_ARREARS").val() > 0) { //如果有欠款，第一次消费不得大于充值金额的70%
        if ($("#spanczzh_" + id).html() < $(obj).parent().find("input[name=cpye]").val() * 0.3) { //剩余金额小于30%
            $(obj).val(0);
            cpjets(obj);
            return;
        }
    }
}

function cpjeft(obj) {//产品 实际卡付金额反推
    var lv = $(obj).parent().prev().find("input[name=cpzk]").val();// $("#hide_ProCARD_RATE").val();
    var id = $(obj).parent().prev().find("input[name=cpid]").val();
    if ($("#isft").prop("checked") == true) {
        $(obj).parent().prev().find("input[name=cpPaymentMode]").val(($(obj).val() / lv).toFixed(0));
    }
    else {
        $(obj).parent().prev().find("input[name=cpPaymentMode]").val($(obj).val());
    }
    if ($("#xmsj_"+id).length <= 0) {
        $("#spanczzh_" + id).html($(obj).parent().prev().find("input[name=cpye]").val() - $("#cpsj_" + id).val());
    }
    else {
        $("#spanczzh_" + id).html($(obj).parent().prev().find("input[name=cpye]").val() - $("#cpsj_" + id).val() - $("#xmsj_" + id).val());
    }

 
    if ($("#spanczzh_"+id).html() < 0) {
        $(obj).val(0);
        cpjeft(obj);
        return;
    }
    if ($("#hide_ARREARS").val() > 0) { //如果有欠款，第一次消费不得大于充值金额的70%

        if ($("#spanczzh_" + id).html() < $(obj).parent().prev().find("input[name=cpye]").val() * 0.3) { //剩余金额小于30%

            $(obj).val(0);
            cpjeft(obj);
            return;
        }
    }
    cppaymoney(obj);//调用剩余金额计算函数
}

function xmjets(obj) {//项目 实际卡付金额推算
    var lv = $(obj).parent().find("input[name=xmzk]").val();// $("#hide_SerCARD_RATE").val();
    var id = $(obj).parent().find("input[name=xmid]").val();
    //alert($(obj).parent().find("input").eq(4).val());
    $(obj).parent().next().find("input[name=xmPaymentMode2]").val(($(obj).val() * lv).toFixed(0));
    if ($("#cpsj_"+id).length <= 0) {
        $("#spanczzh_" + id).html($(obj).parent().find("input[name=xmye]").val() - $(obj).parent().next().find("input[name=xmPaymentMode2]").val());
    }
    else {
        $("#spanczzh_" + id).html($(obj).parent().find("input[name=xmye]").val() - $(obj).parent().next().find("input[name=xmPaymentMode2]").val() - $("#cpsj_" + id).val());
    }
    //alert(parseFloat( $(obj).parent().next().find("input[name=xmPaymentMode2]").val()));
    xmpaymoney(obj);//调用剩余金额计算函数
    if ($("#spanczzh_"+id).html() < 0) {
        $(obj).val(0);
        xmjets(obj);
        return;
    }
    if ($("#hide_ARREARS").val() > 0) { //如果有欠款，第一次消费不得大于充值金额的70%
        if ($("#spanczzh_" + id).html() < $(obj).parent().find("input[name=xmye]").val() * 0.3) { //剩余金额小于30%
            $(obj).val(0);
            xmjets(obj);
            return;
        }
    }
}

function xmjeft(obj) {//项目 实际卡付金额反推
    var lv = $(obj).parent().prev().find("input[name=xmzk]").val();// $("#hide_SerCARD_RATE").val();
    var id = $(obj).parent().prev().find("input[name=xmid]").val();
    if ($("#isft").prop("checked") == true) {
        $(obj).parent().prev().find("input[name=xmPaymentMode]").val(($(obj).val() / lv).toFixed(0));
    }
    else {
        $(obj).parent().prev().find("input[name=xmPaymentMode]").val($(obj).val());
    }
    if ($("#cpsj_"+id).length <= 0) {
        $("#spanczzh_" + id).html($(obj).parent().prev().find("input[name=xmye]").val() - $(obj).val());
    }
    else {
        $("#spanczzh_" + id).html($(obj).parent().prev().find("input[name=xmye]").val() - $(obj).val() - $("#cpsj_" + id).val());
    }
    if ($("#spanczzh_"+id).html() < 0 ) {
        $(obj).val(0);
        $(obj).parent().prev().find("input[name=xmPaymentMode]").val(0);
        xmjeft(obj);
        return;
    }
    if ($("#hide_ARREARS").val() > 0) { //如果有欠款，第一次消费不得大于充值金额的70%
        if ($("#spanczzh_" + id).html() < $(obj).parent().prev().find("input[name=xmye]").val() * 0.3) { //剩余金额小于30%
            $(obj).val(0);
            xmjeft(obj);
            return;
        }
    }
    xmpaymoney(obj);//调用剩余金额计算函数
}

function xmzsts(obj) {//项目 赠送金额结余推算
    if ($("#cpzskf").length <= 0) {
        $("#spanzszh").html($("#hide_zengsongzhanghu").val() - $("#xmzskf").val());
    }
    else {
        $("#spanzszh").html($("#hide_zengsongzhanghu").val() - $("#xmzskf").val() - $("#cpzskf").val());
    }
    if ($("#spanzszh").html() < 0) {
        $(obj).val(0);
        xmzsts(obj);
    }
    xmpaymoney(obj);//调用剩余金额计算函数
}

function cpzsts(obj) {//产品 赠送金额结余推算
    if ($("#xmzskf").length <= 0) {
        $("#spanzszh").html($("#hide_zengsongzhanghu").val() - $("#cpzskf").val());
    }
    else {
        $("#spanzszh").html($("#hide_zengsongzhanghu").val() - $("#xmzskf").val() - $("#cpzskf").val());
    }
    if ($("#spanzszh").html() < 0) {
        $(obj).val(0);
        cpzsts(obj);
    }
    cppaymoney(obj);//调用剩余金额计算函数
}


function hykxz(id) {  //选择会员卡
    $("#QueryProdInfo_mask2").hide();
    $("#QueryProdInfo_boxcontain2").hide();
    //$("#xmzskf").attr("placeholder","剩余金额："+id)

    $.ajax({
        type: 'Post',
        async: true, //同步执行，不然会有问题
        dataType: "json",
        url: "GetCardInfoByID.do?id=" + id,   //提交的页面/方法名
        data: { 'id': id },              //参数（如果没有参数：null）
        //contentType: "application/json; charset=utf-8",
        error: function (msg) {//请求失败处理函数
            //alert("数据加载失败");
        },
        success: function (data) { //请求成功后处理函数。
            //console.log(data); return;
            var BALANCE = 0;
            if (data.status > 0) {
                $("#hide_SerCARD_RATE").val(data.data.DISCOUNT_RATE);  //项目储值账户折率
                $("#hide_ProCARD_RATE").val(data.data.DISCOUNT_RATE2);  //产品储值账户折率
                //$("#hide_SerCARD_RATE2").val(data.data[0].DISCOUNT_RATE3);  //项目赠送账户折率
                //$("#hide_ProCARD_RATE2").val(data.data[0].DISCOUNT_RATE4);  //产品赠送账户折率

                //if (data.data.BALANCE2 > 0) {
                //    $("#divhykye").html("储值账户：￥<span id='spanczzh' style='color: red;'>" + data.data[0].BALANCE + "</span><br />赠送账户：￥<span id='spanzszh' style='color: red;'>" + data.data[0].BALANCE2 + "</span>");
                //    $("#xmhyk3").show();
                //    $("#cphyk3").show();
                //}
                //else {
                $("#divhykye").html("<div class='col-md-3'><b>前次消费卡金余额：￥<span id='spanqcczzh' style='color: red;'>" + data.data.BALANCE + "</span></b></div>");
                $("#divhykye").append("<div class='col-md-3'><b>本次消费卡金余额：￥<span id='spanczzh' style='color: red;'>" + data.data.BALANCE + "</span></b></div>");
                //}
                $("#hide_chuzhizhanghu").val(data.data.BALANCE); //储值账户金额
                //$("#hide_zengsongzhanghu").val(data.data.BALANCE2);  //赠送账户金额
                $("#CUST_CARD_ID").val(data.data.ID); //储值账户id
                //$("#CUST_CARD_ID2").val(data.data.ID2);//赠送账户id
                $("#xmhyk1").show();
                $("#xmhyk2").show();

                $("#cphyk1").show();
                $("#cphyk2").show();

                $("#lbhyk2").show();
                $("#lbhyk1").show();

                if (data.data.ARREARS > 0) {  //有欠款
                    $("#hide_paycount").val(data.data.paycount);//支付次数
                    $("#hide_ARREARS").val(data.data.ARREARS);//欠费金额
                    $("#btnBuyCar").show();
                    $("#btnBuyCar").attr("href", "/Card/BuyCards.do?ccid=" + data.data.CUST_ID + "&id=" + data.data.ID + "&type=1"); //续清页面
                    if (data.data[0].paycount > 0) {  //如果不是第一次消费，则不能使用储值卡
                        $("#xmhyk1").hide();
                        $("#xmhyk2").hide();
                        $("#xmhyk3").hide();
                        $("#cphyk1").hide();
                        $("#cphyk2").hide();
                        $("#cphyk3").hide();
                        alert("有欠款未付清，不可使用储值卡。");
                    }

                }

            }
            else {

            }
        }
    });


}

///验证券号
function CheckV(obj) {
    if ($(obj).prev().val() == "") {
        _showInfoMessage("请输入券号！", "info");
    }
    else if ($(obj).parent("li").find("input[NAME$=xmqh]").val() != $(obj).prev().val()) {
        _showInfoMessage("券号不正确！", "info");
    }
    else {
        var mes = "优惠券面值：" + $(obj).parent("li").find("input[NAME$=xmye]").val() + "元";
        $(obj).parent("li").find("span").html(mes);
        $(obj).parent("li").find("input[NAME$=xmPaymentMode]").val($(obj).parent("li").find("input[NAME$=xmye]").val());//优惠券面值
        $(obj).parent("li").find("input[NAME$=xmVNO]").val($(obj).prev().val());
        $(obj).parent("li").find("input[NAME$=xmVID]").val($(obj).parent("li").find("input[NAME$=qhid]").val());
        var xmmoney = 0;//alert(1);
        $("#xmpay li [name='xmPaymentMode'").each(function () {
            if ($(this).attr("id") != $(obj).parent("li").find("input[NAME$=xmPaymentMode]").attr("id")) {
                xmmoney = parseFloat($(this).val() == "" ? "0" : $(this).val()) + xmmoney;
            }
        });
        
        $("#xmspanPayed").html(($("#xmamt").val() - xmmoney).toFixed(0));
        if (parseFloat($("#xmspanPayed").html()) < parseFloat($(obj).parent("li").find("input[NAME$=xmPaymentMode]").val())) {
            $(obj).parent("li").find("input[NAME$=xmPaymentMode]").val($("#xmspanPayed").html());
        }
        xmpaymoney($(obj).parent("li").find("input[NAME$=xmPaymentMode]"));
    }
}