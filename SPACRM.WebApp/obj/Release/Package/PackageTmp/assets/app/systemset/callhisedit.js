; (function (window, undefined, $) {

    var submiting = false;
    $('#frmEdit').validator({
        rules: {

        },
        fields: {
            '#NAME': 'required'
        },
        valid: function (form) {
            if (submiting) {
                return;
            }
            submiting = true;
            FormSubmit(form, function (res) {
                if (res.status == 0) {
                    window.location.href = options.indexUrl;

                } else {
                    _showInfoMessage("操作失败：" + res.message, 'error');
                }
                submiting = false;
            })
        }
    });
    function submitForm() {
        $("#frmEdit").submit();
    }
    $("#btnSave").click(function (e) {
        submitForm();
    });
    $("#btnCancel").click(function () {
        window.location.href = options.indexUrl;
    });

})(window, undefined, jQuery);