; (function (window, undefined, $) {
    $(document).ready(function () {

        var submiting = false;
        $('#formSave').validator({
            valid: function (form) {
                if (submiting) {
                    return;
                }
                if ($("#Total_AMT").val() == undefined || $("#Total_AMT").val() == "") {
                    alert("请至少选择一个卡级！");
                    return;
                }
                submiting = true;
                FormSubmit(form, function (res) {
                    if (res.status > 0) {
                        _showInfoMessage("操作成功！", "success");
                        window.location.href = "/Customer/View360.do?cid=" + $("#hideCUST_ID").val();

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


        $("#PAYCARD_TYPE").change(function () {

            var prodName = $("#PAYCARD_TYPE").find("option:selected").text();

            $("#hidePROD_NAME").val(prodName);
        });

        $("#btnSave").click(function () {

            $("#formSave").submit();
        });
        $("#btnReturn").click(function () {
            window.location.href = "/Customer/View360.do?cid=" + $("#hideCUST_ID").val();
        });
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
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {
                var but_amt = $("#spanBalance").text();
                //var vipcard = $("#CARD_ID").val();
                //if (vipcard == "" || vipcard == null) {
                //    _showInfoMessage("请您先选择会员卡级！", "info");
                //    ob.value = 0;
                //    return false;
                //}
                var paycard_type = $("#PAYCARD_TYPE").val();
                if (paycard_type == "" || paycard_type == null) {
                    _showInfoMessage("请您先选择退卡方式！", "info");
                    ob.value = 0;
                    $("#CASH_AMT").val(but_amt);
                    return false;
                }
                if (parseFloat(ob.value) > parseFloat(but_amt)) {
                    _showInfoMessage("退卡金额不能大于卡内余额！", "info");
                    ob.value = but_amt;
                    $("#CASH_AMT").val(0);
                    $("#CARD_CASH_AMT").val(but_amt);
                    return false;
                }
                var cash_amt = parseFloat(but_amt) - parseFloat(ob.value);
                if (!isNaN(cash_amt)) {
                    $("#CASH_AMT").val(cash_amt);
                }

            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };


        util.isAllSelectOnClick = function (object) {
            var allCheckBoxs = $(object).parent("div").parent("td").parent("tr").parent("thead").parent("table").find("input[NAME$=isSelect]");
            var totalamt = 0;
            var ID = "";
            var _prodCardID = '';
            var CARD_AMT = "";
            for (var i = 0; i < allCheckBoxs.length ; i++) {
                allCheckBoxs[i].checked = object.checked;
                totalamt = parseFloat(totalamt) + (parseFloat(allCheckBoxs[i].value.split('|')[1]));// - parseFloat(allCheckBoxs[i].value.split('|')[2])
                ID += allCheckBoxs[i].value.split('|')[0] + ',';
                _prodCardID += allCheckBoxs[i].value.split('|')[3] + ',';
                CARD_AMT += allCheckBoxs[i].value.split('|')[1] + ',';
            }
            ID = ID.substr(0, ID.length - 1);
            _prodCardID = _prodCardID.substr(0, _prodCardID.length - 1);
            CARD_AMT = CARD_AMT.substr(0, CARD_AMT.length - 1);
            if (object.checked) {
                $("#CASH_AMT").html(parseFloat(totalamt).toFixed(2));
                $("#HCARD_ID").val(ID);
                $("#ProdCardID").val(_prodCardID);
                $("#HCARD_AMT").val(CARD_AMT);
                $("#Total_AMT").val(parseFloat(totalamt).toFixed(2));
                $("#Return_AMT").val(parseFloat(totalamt).toFixed(2));
            } else {
                $("#CASH_AMT").html(0);
                $("#HCARD_ID").val("");
                $("#ProdCardID").val("");
                $("#HCARD_AMT").val("");
                $("#Total_AMT").val(0);
                $("#Return_AMT").val(0);
            }
        }


        util.isSelectOnClick = function (object) {
            //if (!object.checked)
            //{
            //    $("#isAllSelect").checked = object.checked;
            //}
            var allCheckBoxs = $(object).parent("td").parent("tr").parent("tbody").find("input[NAME$=isSelect]");
            var totalamt = 0;
            var ID = "";
            var _prodCardID = '';
            var CARD_AMT = "";
            for (var i = 0; i < allCheckBoxs.length ; i++) {
                if (allCheckBoxs[i].checked) {
                    totalamt = parseFloat(totalamt) + (parseFloat(allCheckBoxs[i].value.split('|')[1]));// - parseFloat(allCheckBoxs[i].value.split('|')[2])
                    ID += allCheckBoxs[i].value.split('|')[0] + ',';
                    _prodCardID += allCheckBoxs[i].value.split('|')[3] + ',';
                    CARD_AMT += allCheckBoxs[i].value.split('|')[1] + ',';
                }
            }
            ID = ID.substr(0, ID.length - 1);
            _prodCardID = _prodCardID.substr(0, _prodCardID.length - 1);
            CARD_AMT = CARD_AMT.substr(0, CARD_AMT.length - 1);

            $("#HCARD_ID").val(ID);
            $("#ProdCardID").val(_prodCardID);
            $("#HCARD_AMT").val(CARD_AMT);
            $("#CASH_AMT").html(parseFloat(totalamt).toFixed(2));
            $("#Total_AMT").val(parseFloat(totalamt).toFixed(2));
            $("#Return_AMT").val(parseFloat(totalamt).toFixed(2));
        }
        util.openDialog = function (sender, id) {
            var url = options.absentCardEdit + "/" + id;
            window.xjDailog.Open(url, {
                width: 740,
                height: 500,
                caption: '退卡操作',
                theme: "simple", //默认主题
                onclose: function (userstate) {
                    //xjgrid.Reload();
                    util.xjcal.reload();
                } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
            });
        }

    });
})(window, undefined, jQuery);
