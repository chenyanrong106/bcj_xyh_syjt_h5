; (function (window, undefined, $) {

    $(document).ready(function () {

        var submiting = false;
        $('#frmSave').validator({
            rules: {

            },
            messages: {
                required: "必选项"
            },
            fields: {
                "#CARD_ID": "required"
            },
            valid: function (form) {
                if (submiting) {
                    return;
                }
                submiting = true;
                FormSubmit(form, function (res) {
                    if (res.status > 0) {
                        _showInfoMessage("操作成功！", "success");
                        window.location.href = options.view360Url;
                    } else {
                        _showInfoMessage("操作失败，" + res.message, "error");
                    }
                    submiting = false;
                })
            }
        });

        $("#btnSave").click(function () {

            $('#frmSave').submit();
        });
        $("#btnReturn").click(function () {
            window.location.href = options.view360Url;
        });

    });

})(window, undefined, jQuery);