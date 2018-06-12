
; (function (window, undefined, $) {
    var submiting = false;
    $('#frmEdit').validator({
        rules: {
            // 自定义规则，如果通过返回true，否则返回错误消息
            cusidcard: function (element, param, field) {
                var idtype = $("#IDTYPE").val();
                if (idtype == param[0]) {
                    return /^.+$/.test($(element).val()) || "必填项";
                }
                return true;
            },
            mobile: [/^\d{11}$/, '请输入11位有效手机号码']
        },
        fields: {
            '#CUST_NO': 'required',
            '#NAME': 'required',
            //'#IDCARD': 'idcard',
            '#MOBILE': 'mobile',
            '#EMAIL': 'email'
            //'CARD_NO': {
            //    rule: "required",
            //    timely: false
            //}
        },
        valid: function (form) {
            if (submiting) {
                return;
            }
            submiting = true;
            FormSubmit(form, function (res) {
                if (res.status == 0) {
                    util.customer = res.data;
                    var _id = $('#ID').val();
                    if (_id == '0') {
                        $('#confirmModal').modal('show');

                    } else {
                        _showInfoMessage("操作成功！", 'success');
                        var flag = $("#flag").val();
                        if (flag == "1") {
                            window.location.href = "/Customer/Index.do";
                        }
                    }

                } else {
                    _showInfoMessage("操作失败：" + res.message, 'error');
                }
                submiting = false;
            })
        }
    });
    function submitForm(value) {
        //$('#frmEdit').validator("setField", "CARD_NO", value);
        $("#frmEdit").submit();
    }
    $("#btnSave").click(function (e) {
        submitForm(null);
    });
    $("#btnClose").click(function () {
        window.location.href = options.indexUrl;
    });
    $("#btnYes").click(function (e) {
        var data = util.customer;
        var mobile = '';
        if (typeof(data.MOBILE) != 'undefined') {
            mobile = data.MOBILE;
        }
        var url = options.faCardUrl + "?cid=" + data.ID + "&name=" + data.NAME + "&phone=" + mobile;
        window.xjDailog.Open(url, {
            width: 520,
            height: 350,
            caption: '发卡',
            theme: "simple", //默认主题
            onclose: function (userstate) {

            } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
        });

        $('#confirmModal').modal('hide');
    });
    $("#btnNo").click(function (e) {
        window.location.href = options.indexUrl;
    });
    $('#birthday_picker').datepicker({
        language: "zh-CN",
        format: "yyyy-mm-dd"
    });
    $("#IDCARD").blur(function (e) {
        //身份证？可推算出性别和生日
        var v = this.value;
        if (/^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$/.test(v)) {
            var lastchar = v.substr(v.length - 1);
            if (lastchar == "X" || lastchar == "x") {
                $("#GENDER")[0].checked = true;
            }
            else {
                var lcInt = parseInt(lastchar);
                if ((lcInt % 2) == 0) {
                    $("#GENDER")[0].checked = true;
                }
                else {
                    $("#GENDER")[1].checked = true;
                }
            }
        }
    });
    $("#btnCancel").click(function (e) {
        //if (parent && parent.xjDailog) {
        //    parent.xjDailog.Close(false, false, null);
        //}
        //else {
        //    window.close();
        //}
    });

    if ($("#SOURCE").val() == "1") {
        $("#show").show();
    }
    else {
        $("#show").hide();
    }

    $("#SOURCE").change(function () {
        var v = $("#SOURCE").val();
        if (v == "1") {
            $("#show").show();

        }
        else {
            $("#show").hide();
        }
    })
})(window, undefined, jQuery);