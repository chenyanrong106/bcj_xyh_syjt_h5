; (function (window, undefined, $) {

    $(document).ready(function () {
        $('.end_date').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            autoclose: true
        });


        util.modify = function (sender, id) {

            $('#spanEndDate' + id).hide();
            $('#spanEndDate2' + id).show();
        }
        util.revokeCard = function (sender, id) {
            if (!confirm("确定要作废此券吗？")) {
                return;
            }
            var param = {
                id: id
            };
            $.post(options.revokeCardUrl, param, function (data) {
                if (data.status > 0) {
                    //$(sender).remove();
                    window.location.href = window.location.href;
                }
            },
                "json"
            );
        }
        util.editcardid = function (sender, id, type) {
            if (type == 5 || type == 6) {   //电子券不能修改卡级
                return;
            }
            $('#card' + id).hide();
            $('#card2' + id).show();
        }

        util.editcard = function (obj, id) {
            //alert($(obj).val() + "," + id);
            var param = {
                CCID: id,
                CID: $(obj).val()
            };
            $.post(options.modifyCardID, param, function (data) {
                history.go(0);
            },
               "json"
           );
        }

        util.confirmSave = function (sender, id) {
            var endDate = $(sender).prev().val();
            var param = {
                id: id,
                endDate: endDate
            };
            $.post(options.modifyEndDateUrl, param, function (data) {
                if (data.status > 0) {
                    $('#endDateValue' + id).text(endDate);
                }

                $('#spanEndDate' + id).show();
                $('#spanEndDate2' + id).hide();

            },
                "json"
            );
        }
    });

})(window, undefined, jQuery);