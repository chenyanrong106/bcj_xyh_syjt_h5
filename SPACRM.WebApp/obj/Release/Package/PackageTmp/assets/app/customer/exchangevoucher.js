; (function (window, undefined, $) {
    $(document).ready(function () {
        var submiting = false;
        $('#frmExchangeSave').validator({
            rules: {

            },
            fields: {
                'CardID': 'required',
                'Blance': 'required',
                'VoucherNO': 'required'
            },
            valid: function (form) {
                if (submiting) {
                    return;
                }
                submiting = true;
                FormSubmit(form, function (res) {
                    submiting = false;
                    if (res.status == 1) {
                        _showInfoMessage(res.message, 'info');
                    } else {
                        _showInfoMessage(res.message, 'error');
                    }
                });

            }

        });

        $('#btnSave').click(function () {
            $('#frmExchangeSave').submit();
        });
        $('#btnReturn').click(function () {
            location.href = options.view360Url;
        });

        $('#CardID').change(function () {
            var _val = this.value;
            if (_val == '') {
                $('#Balance').val('0');
            } else {
                var _amt = $(this).find("option:selected").attr('amt');
                $('#Balance').val(_amt);
            }

        });
        /*
        $('#btnVerifyVoucher').click(function () {
            var _vouNO = $('#VoucherNO').val();
            if (_vouNO == '') return;

            var param = {
                VOUCHER_NO: _vouNO
            };
            $.post(options.validUrl, param, function (data) {
                if (data.status > 0) {
                    var vouInfo = data.data;
                    if (vouInfo.STATUS == 0) {
                        if (vouInfo.CUST_ID && vouInfo.CUST_ID > 0) {
                            _showInfoMessage("提示：此券已被其他人兑换", 'error');
                        } else {
                            $('#spanVoucherBalance').text("面值："+vouInfo.FACE_VALUE);
                        }
                    } else {
                        var errMsg = '';
                        if (vouInfo.STATUS == 1) {
                            errMsg = "已使用";

                        } else if (vouInfo.STATUS == 2) {
                            errMsg = "已作废";

                        } else if (vouInfo.STATUS == 3) {
                            errMsg = "已过期";
                        }
                        _showInfoMessage("提示：" + errMsg, 'error');
                    }

                } else {
                    _showInfoMessage("提示：" + data.message, 'error');
                }


            },
                "json"
            );
        });
        */




    });
})(window, undefined, jQuery);