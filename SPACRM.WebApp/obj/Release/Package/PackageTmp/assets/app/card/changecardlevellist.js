; (function (window, undefined, $) {

    $(document).ready(function () {
        var gridopt = {
            url: options.listUrl,
            colModel: [
                    { display: '编号', name: 'ID', width: "0", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '客户ID', name: 'CUST_ID', width: "0", sortable: false, hide: true, align: 'left' },
                    { display: '门店', name: 'OP_STORE_ID', width: "5%", sortable: false, hide: true, align: 'left' },
                    { display: '客户姓名', name: 'CUST_NAME', width: "9%", sortable: false, align: 'left' },
                    { display: '会员卡号', name: 'CUST_NO', width: "9%", sortable: false, align: 'left' },
                    { display: '新卡级名称', name: 'NEW_CARD_NAME', width: "9%", sortable: false, align: 'left' },
                    { display: '卡级余额', name: 'NEW_CARD_BALANCE', width: "8%", sortable: false, align: 'left' },
                    { display: '卡级欠款', name: 'NEW_CARD_ARREARS', width: "8%", sortable: false, align: 'left' },
                    { display: '老卡级名称', name: 'OLD_CARD_NAME', width: "9%", sortable: false, align: 'left' },
                    { display: '卡级余额', name: 'OLD_CARD_BALANCE', width: "8%", sortable: false, align: 'left' },
                    { display: '卡级欠款', name: 'OLD_CARD_ARREARS', width: "8%", sortable: false, align: 'left' },
                    { display: '创建用户', name: 'CREATE_USER_NAME', width: "8%", sortable: false, align: 'left' },
                    { display: '创建日期', name: 'CREATE_DATE', width: "8%", sortable: false, align: 'left', process: formatDate }
                    //{ display: '操作', name: 'ID', width: "10%", sortable: false, align: 'center', process: processOp }
            ],
            sortname: "Id",
            sortorder: "DESC",
            title: false,
            rp: 15,
            usepager: true,
            showcheckbox: false
        };

        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp(value, cell) {
            var ops = [];
            ops.push("&nbsp;<a title='修改卡级变更' class='abtn' href='javascript:;'  onclick=\"util.Edit('", value, "')\"><i class='fa fa-edit' ></i>修改</a>");
            ops.push("&nbsp;&nbsp;<a title='删除卡级变更' class='abtn' href='javascript:;'  onclick=\"util.Delete('", value, "','", cell[1], "')\"><i class='fa fa-trash-o' ></i>删除</a>");
            return ops.join("");
        }

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

        $("#btnAdd").click(function (e) {
            window.location.href = options.editUrl;
        });

        $("#btnConfirm").click(function (e) {
            $('#confirmModal').modal('hide');
            var id = $("#hdCurrentId").val();
            $.post(options.deleteUrl, { id: id },
                  function (res) {
                      if (res.status == 0) {
                          _showInfoMessage("操作成功", 'success');
                          xjgrid.Reload();
                      }
                      else {
                          _showInfoMessage("操作失败：" + res.message, 'error');
                      }
                  },
                  "json"
            );
        })
        util.Edit = function (id) {
            window.location.href = options.editUrl + "/" + id;
        };
        util.Delete = function (id, name) {
            $("#lbuserName").html(name);
            $("#hdCurrentId").val(id);
            $('#confirmModal').modal('show');
        };




        $('#QueryChangeCardLevel').click(function () {
            $.ajax({
                url: "ChangeCardLevelList",
                type: "Get",
                success: function (result) {
                }
            });
        });
    });

})(window, undefined, jQuery);