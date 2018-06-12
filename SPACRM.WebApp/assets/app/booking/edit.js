
; (function (window, undefined, $) {
    var submiting = false;
    $('#frmEdit').validator({
        rules: {
        },
        fields: {
            '#CUST_NO': 'required',
            '#NAME': 'required',
            '#IDCARD': 'required;idcard',
            '#EMAIL': 'email',
        },
        valid: function (form) {
            if (submiting) {
                return;
            }
            submiting = true;
            FormSubmit(form, function (res) {
                if (res.status == 0) {
                    alert("操作成功！");
                    if (window.opener) {
                        window.opener.location.reload();
                        window.close();
                    }
                    else {
                        if (parent && parent.xjDailog) {
                            parent.xjDailog.Close(false, true, null);
                        }
                        else {
                            window.close();
                        }
                    }
                }
                else {
                    showErrorTip("操作失败！：" + res.message, { left: 100, top: 10 }, true, 5000);
                }
                submiting = false;
            })
        }
    });
    $('#birthday_picker').datepicker({
        language: "zh-CN",
        format: "yyyy-mm-dd"
    });
    $("#IDCARD").blur(function (e) {
        var idcard = this.value;
        //身份证 ？  可推算出性别和生日
    });
    $("#btnSave").click(function (e) {
        $("#frmEdit").submit();
    });
    $("#btnCancel").click(function (e) {
        if (parent && parent.xjDailog) {
            parent.xjDailog.Close(false, false, null);
        }
        else {
            window.close();
        }
    });

    

    //解决IE下有时输入框失去焦点
    //$("#UserCard").focus();
  
    $("#CUST_NAME").autocomplete({
        source: function (request, response) {
            $.getJSON(options.queryMemberUrl, { q: request.term, type: "name" }, function (res) {
                if (res != null && res.status > 0) {
                    var str = eval("(" + res.data + ")");
                    response(str);
                }
            });
        },
        select: function (e, ui) {
            $("#CUST_MOBILE").val(ui.item.mobile);
        }
    });

    $("#CUST_MOBILE").autocomplete({
        source: function (request, response) {
            $.getJSON(options.queryMemberUrl, { q: request.term, type: "mobile" }, function (res) {
                if (res != null && res.status > 0) {
                    var str = eval("(" + res.data + ")");
                    response(str);
                }
            });
        },
        select: function (e, ui) {
            $("#CUST_NAME").val(ui.item.name);
        }
    });             
   
    $(function () {
        $("#SERVICE_PRODUCT").select2({ width: "resolve", tags: ["面部护理", "头部护理", "全身护理", "混合护理", "洗剪吹", "面部护理1", "头部护理1", "全身护理1", "混合护理1", "洗剪吹1"] });
    });


    $(function () {

        //根据姓名/电话检索
        $("#CUST_ID").select2({
            placeholder: "输入姓名、电话查询",
            minimumInputLength: 2,
            width: 'resolve',
            ajax: {
                url: options.queryCustomerUrl,
                dataType: 'json',
                quietMillis: 100,
                data: function (term, page) {
                    return {
                        q: term, //search term
                        page_limit: 10, // page size
                        page: page // page number
                    };
                },
                results: function (data, page) {
                    var strJson = eval("(" + data.data + ")");
                    return { results: strJson, more: false };
                }
            },
            formatResult: custFormatResult,
            formatSelection: custFormatSelection,
            dropdownCssClass: "bigdrop",
            formatNoMatches: function (m) {
                return "<span style='color:#7B7B7B;font-size:12px;' >未查询到相关信息</span>";
            },
            escapeMarkup: function (m) { return m; }
        });

        function custFormatResult(obj) {
            var markup = "<table class='movie-result'><tr>";
            markup += "<td class='movie-info'><div class='movie-title'>" + obj.title + "</div>";
            if (obj.mobile !== undefined) {
                markup += "<div class='movie-synopsis'>" + obj.mobile + "</div>";
            }
            markup += "</td></tr></table>"
            return markup;
        }
        //选择顾客
        function custFormatSelection(obj) {
            //window.location.href = window.location.href + "?cid=" + obj.id;
            //return false;
            $("#CUST_MOBILE").val(obj.mobile);
            $("#CUST_NAME").val(obj.title);
            return obj.title;
        }
    });

    //选择门店
    $("#STORE_ID").change(function () {
        $("#STORE_NAME").val($(this).find("option:selected").text());
        
    });

})(window, undefined, jQuery);