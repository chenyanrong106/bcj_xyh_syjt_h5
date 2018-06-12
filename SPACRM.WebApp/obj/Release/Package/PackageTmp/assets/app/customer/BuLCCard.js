; (function (window, undefined, $) {

    $(document).ready(function () {


        $("#CARD_ID").change(function () {
            util.showLCCPack(this.value);
        });

        $('#btnSave').click(function () {

            $('#formSave').submit();
        });

        $('#btnReturn').click(function () {

            window.location.href = options.view360Url;
        });

        //保存验证
        $('#formSave').validator({
            rules: {
            },
            fields: {
                'CARD_ID': {
                    rule: 'required',
                    msg: '请选择一个疗程套餐'
                }

            },
            valid: function (form) {
                FormSubmit(form, function (res) {
                    if (res.status > 0) {
                        window.location.href = options.view360Url;

                    } else {
                        _showInfoMessage("操作失败：" + res.message, 'error');
                    }
                })
            }
        });
    });

    util.onPriceChange = function (id, obj) {
        var price = $(obj).val();
        if (!/^[0-9]*$/.test(price)) {
            alert("请输入数字!");
            $(obj).val("0").focus();
            return;
        }
        var _qty = $(obj).parents("tr").find("input[name=proBuyQty]");
        var _amt = $(obj).parents("tr").find("input[name=proAmt]");
        _amt.val(parseFloat(price) * parseInt(_qty.val()));
    }
    util.onBuyQtyChange = function (id, obj) {
        var buyQty = $(obj).val();
        if (!/^[0-9]*$/.test(buyQty)) {
            alert("请输入数字!");
            $(obj).val("1").focus();
            return;
        }
        var _price = $(obj).parents("tr").find("input[name=proPrice]");
        var _amt = $(obj).parents("tr").find("input[name=proAmt]");
        _amt.val(parseInt(buyQty) * parseFloat(_price.val()));
    }
    util.onUsedQtyChange = function (id, obj) {
        var usedQty = $(obj).val();
        if (!/^[0-9]*$/.test(usedQty)) {
            alert("请输入数字!");
            $(obj).val("0").focus();
            return;
        }
        var _buyQty = $(obj).parents("tr").find("input[name=proBuyQty]");
        if (parseInt(_buyQty.val()) < parseInt(usedQty)) {
            alert("已使用数量不能大于购买数量!");
            $(obj).val("0").focus();
            return;
        }


        var _leftQty = $(obj).parents("tr").find("input[name=proLeftQty]");

        _leftQty.val(parseInt(_buyQty.val()) - parseInt(usedQty));
    }

    util.showLCCPack = function (lccpackId) {
        if (lccpackId != null && lccpackId != "") {
            showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 700, top: 450 });
            $.post("/Order/GetLCCPack.do", { lid: lccpackId },
                   function (ret) {
                       if (ret.status > 0) {
                           var data = eval(ret.data);

                           $("#tSpeedyPro tbody").html("");
                           var proListHtml = [];
                           $(data).each(function (i) {
                               if (i > 0) {
                                   proListHtml.push("<tr class='strip' id='" + this.ID + "'>");
                                   proListHtml.push("<td style=\"width: 40%;\"><div style=\"text-align:left;\">", this.SERVICE_NAME, "</div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='text' name='proPrice' style='width:50px;' onchange='util.onPriceChange(" + this.ID + ", this)' value='", this.UNIT_PRICE, "' /></div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='text' name='proBuyQty' style='width:50px;' onchange='util.onBuyQtyChange(" + this.ID + ", this)' value='", this.SERVICE_QTY, "' /></div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='text' name='proUsedQty' style='width:50px;'onchange='util.onUsedQtyChange(" + this.ID + ", this)' value='0' /></div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='text' name='proLeftQty' style='width:50px;' value='0' /></div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='text' name='proPresentQty' style='width:50px;' value='0' /></div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='text' name='proAmt' style='width:50px;' readonly value='", this.AMT_PRICE, "' /></div></td>");
                                   proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">");

                                   proListHtml.push("<input type='hidden' name='proId' value=" + this.ID + " />");//this.ID
                                   proListHtml.push("<input type='hidden' name='proName' value='" + this.SERVICE_NAME + "' />");

                                   proListHtml.push("<a title='移除' class='abtn' href='javascript:;' onclick=\"util.Remove(this,'", this.ID, "','", this.AMT_PRICE, "')\"><i class='fa fa-trash-o' ></i> 移除</a>");

                                   proListHtml.push("</div></td>");
                                   //proListHtml.push("<td>");
                                   //proListHtml.push("<input type='hidden' name='proId' value=" + this.ID + " />");//this.ID
                                   //proListHtml.push("<input type='hidden' name='proName' value='" + this.SERVICE_NAME + "' />");
                                   //proListHtml.push("<input type='hidden' name='proPrice' value=" + (this.AMT_PRICE / this.SERVICE_QTY).toFixed(2) + " />");//单价this.UNIT_PRICE
                                   //proListHtml.push("<input type='hidden' name='proQty' value=" + this.SERVICE_QTY + " />");//数量
                                   //proListHtml.push("<input type='hidden' name='payAmt' value=" + this.AMT_PRICE + " />");//应付小计金额
                                   //proListHtml.push("</td>");
                                   proListHtml.push("</tr>");
                               }
                           });
                           $(proListHtml.join('')).appendTo("#tSpeedyPro tbody");
                           hideLoadingMsg();
                       }
                   },
                   "json"
                  );
        } else {
            $("#tSpeedyPro tbody").html("");

        }

    }

    util.Remove = function (obj, id, price) {
        //var totalAmt = parseFloat($("#spanTotalAmt").html());
        //totalAmt = totalAmt - parseFloat(price);
        //$("#spanTotalAmt").html(totalAmt);
        //$("#lcPayedAmt").val(totalAmt);
        $(obj).parent("div").parent("td").parent("tr").remove();
    }
})(window, undefined, jQuery);