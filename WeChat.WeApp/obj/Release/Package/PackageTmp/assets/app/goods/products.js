﻿; (function (window, undefined, $) {
    var submiting = false;
    $(document).ready(function () {
        var gridopt = {
            url: options.listUrl,
            colModel: [
                    { display: '编号', name: 'ID', width: "0", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '产品编号', name: 'SKU', width: "10%", sortable: false, align: 'left' },
                    { display: '名称', name: 'NAME', width: "20%", sortable: false, align: 'left' },
                    { display: '类型', name: 'TYPE', width: "10%", sortable: false, align: 'left', process: formatTRANSFER },
                    { display: '价格', name: 'PRICE', width: "10%", sortable: false, align: 'left' },
                    { display: '成本', name: 'COST', width: "10%", sortable: false, align: 'left' },
                    { display: '状态', name: 'STATUS', width: "10%", sortable: false, align: 'left', process: formatSTATUS },
                    { display: '操作', name: 'ID', width: "15%", sortable: false, align: 'center', process: processOp }
            ],
            sortname: "Id",
            sortorder: "DESC",
            title: false,
            rp: 10,
            usepager: true,
            showcheckbox: false,


        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp(value, cell) {
            var ops = [];
            ops.push("&nbsp;<a title='编辑级别信息' class='abtn' href='javascript:;'  onclick=\"util.Edit('", value, "')\"><i class='fa fa-edit' ></i>编辑</a>");
            ops.push("&nbsp;&nbsp;<a title='删除级别信息' class='abtn' href='javascript:;'  onclick=\"util.Delete('", value, "','", cell[2], "')\"><i class='fa fa-trash-o' ></i>删除</a>");

            return ops.join("");
        }
        function formatGender(value, cell) {
            if (value == "1") {
                return "男";
            }
            else {
                return "女";
            }
        }
        function formatDate(value, cell) {
            if (value) {
                return value.split(" ")[0]
            }
            else {
                return "-"
            }
        }

        //是否
        function formatTRANSFER(value, cell) {
            if (value == "1") {
                return "正装产品";
            }
            else if (value == "2") {
                return "赠品";
            }
            else if (value == "3") { return "耗材"; }
        }

        //是否
        function formatSTATUS(value, cell) {
            if (value == "1") {
                return "上架";
            }
          
            else { return "下架"; }
        }


        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });

        $("#btnAdd").click(function (e) {
            //$("#ID").val(0);
            //$('#frmSave')[0].reset();
            //$('#EditModal').modal('show');
            var url = options.editUrl;
            location.href = url;
        });

        $("#btnConfirm").click(function (e) {
            $('#confirmModal').modal('hide');
            var id = $("#hdCurrentId").val();
            $.post(options.deleteUrl + "/" + id, { id: id },
                  function (res) {
                      if (res.status == 0) {
                          _showInfoMessage("操作成功！", 'success');
                          xjgrid.Reload();
                      }
                      else {
                          _showInfoMessage("操作失败：" + res.message, 'error');
                      }
                  },
                  "json"
            );
        });

        util.Edit = function (id) {
            //$.getJSON(options.editUrl, { id: id }, function (res) {
            //    if (res != null && res.status == 0) {
            //        var data = eval(res);
            //        $("#ID").val(data.data.ID);                
            //        $('#EditModal').modal('show');
            //    }
            //});
            var url = options.editUrl + "/" + id;
            location.href = url;
        };

        util.Delete = function (id, name) {
            $("#lbuserName").html(name);
            $("#hdCurrentId").val(id);
            $('#confirmModal').modal('show');
        };

        //验证
        $('#frmSave').validator({
            rules: {
            },
            fields: {
                '#PAY_NAME': 'required',
                '#PAY_TYPE': 'required',
                '#COMMISSIONRATE': 'required'
            },
            valid: function (form) {
                FormSubmit(form, function (res) {
                    if (res.status == 0) {
                        _showInfoMessage("操作成功！", 'success');
                        $('#EditModal').modal('hide');
                        xjgrid.Reload();
                    }
                    else {
                        _showInfoMessage("操作失败：" + res.message, 'error');
                    }
                    submiting = false;
                })
            }
        });
        $("#btnSave").click(function (e) {
            $("#frmSave").submit();
        });
    });


})(window, undefined, jQuery);