; (function (window, undefined, $) {
    $(document).ready(function () {



        var submiting = false;
        $('#formSave').validator({
            rules: {

            },
            fields: {
                '#Return_Type': {
                    rule: 'required',
                    msg: '请选择退款方式。'
                }
            },
            valid: function (form) {
                if (submiting) {
                    return;
                }

                submiting = true;
                FormSubmit(form, function (res) {
                    if (res.status > 0) {
                        _showInfoMessage("操作成功！", "success");
                        util.closeDialog();

                    } else {
                        _showInfoMessage("操作失败！" + res.message, "error");
                    }
                    submiting = false;
                })
            }
        });


        $("#btnSave").click(function () {

            $("#formSave").submit();
        });

        $('#btnReturn').click(function () {
            //util.closeDialog();
            parent.window.location.href = parent.window.location.href;
        });
        util.closeDialog = function () {
            if (window.opener) {
                window.opener.location.reload();
                window.close();
            } else {
                if (parent && parent.xjDailog) {
                    parent.xjDailog.Close(false, true, null);
                }
                else {
                    window.close();
                }
            }
        }

        util.onRefundQtyChanged = function (sender, id) {
            var maxQty = $(sender).attr('max_qty');
            var qty = sender.value;
            if (!/^[0-9]*$/.test(qty)) {
                alert("请输入数字!");
                $(sender).val(maxQty).focus();
                return;
            }
            if (parseInt(qty) > parseInt(maxQty)) {
                alert("不能大于目前可用次数!");
                $(sender).val(maxQty).focus();
                return;
            }
            var amount = 0;
            var _tr = $('#tbodyCDetails').find("tr");
            _tr.each(function () {
                var _refundQty = $(this).find('input[name=RefundQty]');
                var rQty = _refundQty.val();
                var price = $(_refundQty).attr('price');

                amount = amount + (parseInt(rQty) * parseFloat(price));
            });
            $('#CASH_AMT').text(amount.toFixed(2));
        }
    });

})(window, undefined, jQuery);