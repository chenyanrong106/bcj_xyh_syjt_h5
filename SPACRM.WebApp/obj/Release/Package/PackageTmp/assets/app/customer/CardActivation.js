; (function (window, undefined, $) {
    $(document).ready(function () {
        $('#btnGainCard').click(function () {
            var cardNo = $('#CARD_NO').val();
            var _error = $('#spanError');
            if ($.trim(cardNo) == '') {
                _error.css('visibility', 'visible').text('请输入卡号。');
                return;
            }
            _error.css('visibility', 'hidden');

            $.post(options.getCustUrl, { cardNO: cardNo },
                  function (response) {
                      if (response.status > 0) {
                          console.log(response.data);
                          var custInfo = response.data;
                          $('#spanOpenCardTime').text(custInfo.ONDATE);
                          $('#spanCardStatus').text(custInfo.IS_UNREGISTER == false ? "记名卡" : "未记名卡");
                          $('#NAME').val(custInfo.NAME);
                          $('#MOBILE').val(custInfo.MOBILE);
                          $('#ID').val(custInfo.ID);
                          _error.css('visibility', 'hidden');
                      } else {
                          _error.css('visibility', 'visible').text(response.message);
                      }
                  },
                  "json"
                  );
        });


        var submiting = false;
        $('#frmEdit').validator({
            rules: {
                mobile: [/^\d{11}$/, '请输入11位数字']
            },
            fields: {
                '#NAME': 'required',
                '#MOBILE': 'required;mobile'
            },
            valid: function (form) {
                if (submiting) {
                    return;
                }
                submiting = true;
                FormSubmit(form, function (res) {
                    if (res.status > 0) {
                        alert('激活成功。');
                        window.location.href = options.view360Url + '?cid=' + res.data;
                    } else {
                        alert('激活失败：'+res.message);
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
        $("#btnReturn").click(function (e) {
            window.location.href = options.indexUrl;
        });
    });
})(window, undefined, jQuery);