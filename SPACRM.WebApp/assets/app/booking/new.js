
; (function (window, undefined, $) {
    var submiting = false;
    $('#frmEdit').validator({
        rules: {
            mobile: [/^\d{11}$/, '请输入11位有效手机号码']
        },
        fields: {
            '#CUST_MOBILE': 'required;mobile',
            //'#CUST_NAME': 'required',
            //'#SER_TYPE': 'required',
            //'#STAFF_ID': 'required',
        },
        valid: function (form) {
            if (submiting) {
                return;
            }
            submiting = true;
            FormSubmit(form, function (res) {
                if (res.status == 0) {
                    _showInfoMessage('操作成功！', 'success');
                    setInterval(returnList, 2000);
                } else {
                    _showInfoMessage('操作失败！' + res.message, 'error');
                    // showErrorTip("操作失败！：" + res.message, { left: 100, top: 10 }, true, 5000);
                }
                submiting = false;
            })
        }
    });
    function returnList() {
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
    $('#birthday_picker').datepicker({
        language: "zh-CN",
        format: "yyyy-mm-dd"
    });

    $("#btnSave").click(function (e) {
        //验证顾客身份信息
        var mobile = $.trim($("#CUST_MOBILE").val());
        if (mobile == "") {
            _showInfoMessage("顾客电话不能为空！", 'info');
            return false;
        }
        var _id = $('#ID'),
            _date = $('#CREATE_DATE'),
            _bTime = $('#BEGINTIME'),
            _eTime = $('#ENDTIME'),
            _staffID = $('#STAFF_ID');

        submitForm(mobile);
        //$.post(options.validStaffConfict,
        //    {
        //        bID: _id.val(),
        //        date: _date.val(),
        //        beginTime: _bTime.val(),
        //        endTime: _eTime.val(),
        //        staffID: _staffID.val()
        //    },
        //    function (response) {
        //        if (response.status == -1) {
        //            var flag = confirm(response.message);
        //            if (flag == true) {
        //                submitForm(mobile);
        //            }
        //        } else {
        //            submitForm(mobile);
        //        }
        //    },
        //    'json');

    });
    function submitForm(mobile) {
        $.post(options.checkMobile, { MOBILE: mobile },
                function (res) {
                    if (res.status >= 0) {
                        if (!confirm("未检测到该顾客身份信息，保存将新增该顾客信息！")) {
                            return false;
                        }
                    }
                    submitBook();
                },
                "json"
               );
    }
    function submitBook() {
        var serType = $("#SER_TYPE").find("option:selected").text();
        $("#hideSERVICE_TYPE").val(serType);
        $("#frmEdit").submit();
    }
    $("#btnCancel").click(function (e) {
        if (parent && parent.xjDailog) {
            parent.xjDailog.Close(false, false, null);
        }
        else {
            window.close();
        }
    });

    //时间
    $('#BEGINTIME').timepicker();
    $('#ENDTIME').timepicker();


    //初始化    //服务项目  保留
    InintSerItem($("#SER_TYPE").val());
    function InintSerItem(cateId) {
        $.post(options.querySerItemUrl, { category_id: cateId },
                 function (res) {
                     if (res.status >= 0) {
                         var items = eval("(" + res.data + ")");
                         $("#SERVICE_PRODUCT").select2({ width: "resolve", tags: items });
                     }
                     else {
                         $("#SERVICE_PRODUCT").select2({ width: "resolve", tags: "" });
                     }
                 },
                 "json"
                );
    }
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
            $("#CUST_ID").val(ui.item.id);
        }
    });

    $("#CUST_MOBILE").autocomplete({
        source: function (request, response) {
            $.getJSON(options.queryMemberUrl, { q: request.term, type: "mobile" }, function (res) {
                if (res != null && res.status >= 0) {
                    var str = eval("(" + res.data + ")");
                    response(str);
                }
            });
        },
        select: function (e, ui) {
            $("#CUST_NAME").val(ui.item.name);
            $("#CUST_ID").val(ui.item.id);

        }
    });

    //类别选择
    $("#SER_TYPE").change(function () {
        InintSerItem($(this).val());
    });
    //房间选择
    $("#BED_ID").change(function () {

    });

    //var staffnum = 1;
    //$("#addStaff").click(function () {
    //    var content = [];
    //    //<div class="row">
    //    //      <label class="col-xs-2 control-label" for="MANAGER">技  师</label>
    //    //      <div class="col-xs-9">
    //    //          @Html.ResourceDropDownList("STAFF_ID", "Masseur",Request.QueryString["mid"], new { @class = "form-control" },null, true)<a id="addStaff">+新增</a>
    //    //      </div>
    //    //  </div>

    //    content.push('<div class="row" id="row_' + staffnum + '">');
    //    content.push('<label class="col-xs-2 control-label" for="MANAGER"></label>');
    //    content.push('<div class="col-xs-4">');
    //    content.push('<select class="form-control" style="margin-top:5px;"><option>张三四</option></select> ');
    //    content.push('</div>');
    //    content.push(" <label class='col-xs-2 control-label'><span onclick=\"util.delStaf('", "row_"+staffnum+"", "')\">-删除</span></label>");
    //    content.push('</div>');

    //    $(content.join('')).appendTo("#divStaff");
    //    staffnum++;
    //});

    //util.delStaf = function (row) {
    //    $("#" + row).remove();
    //};

    //服务项目
    function _addSerItem() {
        $.post(options.querySerCateItemUrl, null,
            function (res) {
                if (res.status >= 0) {
                    var items = eval("(" + res.data + ")");
                    $("#SERVICE_PRODUCT").select2({ width: "resolve", tags: items });
                }
            },
            "json"
           );

        $.post(options.querySerItemUrl, { category_id: cateId },
            function (res) {
                if (res.status >= 0) {
                    var items = eval("(" + res.data + ")");

                    $("#SERVICE_PRODUCT").select2({ width: "resolve", tags: items });
                }
            },
            "json"
           );
    }
    $('#CREATE_DATE').datepicker({
        language: "zh-CN",
        format: "yyyy-mm-dd",
        autoclose: true
    });
})(window, undefined, jQuery);