; (function (window, undefined, $) {

    $(document).ready(function () {

        if ($("#ISADMIN").val() == "True") {
            $("#storeDiv").show();
        }
        else {
            $("#storeDiv").hide();
        }

        var gridopt = {
            url: options.listUrl,
            colModel: [
                    { display: '编号', name: 'ID', width: "5%", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '会员卡号', name: 'CARD_NO', width: "10%", sortable: false, align: 'left' },
                    { display: '会员姓名', name: 'CUST_NAME', width: "15%", sortable: false, align: 'left' },
                    { display: '消费积分', name: 'REAL_CONSUMING', width: "10%", sortable: false, align: 'left' },
                    { display: '兑换产品', name: 'GOODS_NAME', width: "40%", sortable: false, align: 'left' },
                    { display: '产品价格', name: 'PRICE', width: "10%", sortable: false, align: 'left' },
                    { display: '兑换日期', name: 'CREATE_TIME', width: "10%", sortable: false, align: 'left', process: formatDate}

            ],
            sortname: "Id",
            sortorder: "ASC",
            title: false,
            rp: 10,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
       
        function formatDate(value, cell) {
            if (value) {
                return value.split(" ")[0]
            }
            else {
                return "-"
            }
        }
        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });


        $("#btnSearch").click(function () {
            $.cookie("STORE", $("#STORE").val());
            var stores = "";
            $("select[name=STORE] option").each(function () {
                if ($(this).val() != "") {
                    stores += $(this).val() + ",";
                }

            });
            if ($("select[name=STORE]").val() != "") {
                stores = $("select[name=STORE]").val();
            }
            $.cookie("STORE", stores);
            $("#STORES").val(stores);
            $("#formQuery").submit();
        })


        //$('#frmSave').validator({
        //    rules: {
        //    },
        //    fields: {
        //        '#ROOM_NO': 'required',
        //        '#NAME': 'required',
        //        '#BED_NUM': 'required;digits',
        //    },
        //    valid: function (form) {
        //        FormSubmit(form, function (res) {
        //            if (res.status == 0) {
        //                _showInfoMessage("操作成功！", 'success');
        //                $('#EditModal').modal('hide');
        //                xjgrid.Reload();
        //            }
        //            else {
        //                _showInfoMessage("操作失败：" + res.message, 'error');
        //                //showErrorTip("操作失败！：" + res.message, { left: 100, top: 10 }, true, 5000);
        //            }
        //        })
        //    }
        //});

        //util.Delete = function (id, name) {
        //    $("#lbuserName").html(name);
        //    $("#hdCurrentId").val(id);
        //    $('#confirmModal').modal('show');
        //};
        //$("#btnConfirm").click(function (e) {
        //    $('#confirmModal').modal('hide');
        //    var id = $("#hdCurrentId").val();
        //    $.post(options.deleteUrl + "/" + id, { id: id },
        //          function (res) {
        //              showErrorTip(res.message, { right: 100, top: 10 }, true, 5000);
        //              if (res.status == 0) {
        //                  _showInfoMessage("删除成功！", 'success');
        //                  xjgrid.Reload();
        //              }
        //              else {
        //                  _showInfoMessage("删除失败：" + res.message, 'error');
        //              }
        //          },
        //          "json"
        //    );
        //})

        PostGetProdTypes();

        $("#REGION").change(function () {

            $("#ID").val("second");
            $.cookie("REGION", $("#REGION").val());
            //alert($.cookie("REGION"));

            PostGetProdTypes();
        })

        $("#STORE").change(function () {

            $("#ID").val("second");
            $.cookie("STORE", $("#STORE").val());
            //alert($.cookie("STORE"));

        })



        var _submiting2 = false;
        function PostGetProdTypes() {
            if (_submiting2) {
                //showErrorTip("请耐心等待服务端操作结束", { left: 400, top: 450 }, true, 5000);
                return;
            }
            _submiting2 = true;
            //showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 400, top: 450 });
            var pid = $('#REGION').val();
            $.ajax({
                url: options.proTypeUrl,
                type: "POST",
                data: { "PID": pid },
                success: function (result) {

                    hideLoadingMsg();
                    //大类
                    var cuAllPs = result.data;
                    var cuAllPsArray = new Array();
                    if (cuAllPs != "") {
                        cuAllPsArray = cuAllPs.split(",");

                    }
                    var hl = "<select class=\"form-control\" id=\"STORE\" name=\"STORE\"  ><option value=''>请选择门店名称</option>";

                    if (cuAllPs != "") {
                        for (var i = 0; i < cuAllPsArray.length; i++) {
                            var re = cuAllPsArray[i].split("_");
                            hl += "<option value=\"" + re[0] + "\">" + re[1] + "</option>";

                        }
                    }
                    hl += "<select>";
                    $("#STORE").html(hl);
                    _submiting2 = false;
                }
                ,
                error: function (result) {
                    hideLoadingMsg();
                    _submiting2 = false;
                    _showInfoMessage("页面有异常：" + result, 'error');

                }
            });
        }

    });
})(window, undefined, jQuery);