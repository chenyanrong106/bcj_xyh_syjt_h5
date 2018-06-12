; (function (window, undefined, $) {
    $(document).ready(function () {
        var gridopt = {
            url: options.linkUrl,
            colModel: [
                    { display: 'ID', name: 'ID', width: "1%", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: 'VOUCHER_NO', name: 'VOUCHER_NO', width: "1%", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '类别', name: 'TYPE_NAME', width: "30%", sortable: false, align: 'left' },
                    { display: '发放门店', name: 'STORE_NAME', width: "20%", sortable: false, align: 'left', process: formatDate },
                    { display: '状态', name: 'STATUS_NAME', width: "10%", sortable: false, align: 'left' },
                    { display: '过期时间', name: 'INVALID_DATE', width: "30%", sortable: false, align: 'left' },
                    { display: '状态', name: 'STATUS', width: "1%", sortable: false,hide: true, align: 'left' },
                    { display: '操作', name: 'ID', width: "10%", sortable: false, align: 'left', process: processOp }
            ],
            sortname: "ID",
            sortorder: "ASC",
            title: false,
            rp: 10,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp(value, cell) {
            var ops = [];
            ops.push("&nbsp;<a title='使用' class='abtn' href='javascript:;'  onclick=\"util.View('", cell[1], "')\"><i class='fa fa-search' ></i> 查看</a>");
            //if (cell[4] == "1") {
            //    ops.push("&nbsp;<a title='退款' class='abtn' href='javascript:;'  onclick=\"util.Refund('", value, "')\"><i class='fa fa-shopping-cart' ></i> 退款</a>");
            //    ops.push("&nbsp;<a title='作废' class='abtn' href='javascript:;'  onclick=\"util.Cancell('", value, "')\"><i class='fa fa-ban' ></i> 作废</a>");
            //}
            return ops.join("");
        }
        function formatTRANS_TYPE(value, cell) {
            var status = "未知";
            switch (value) {
                case "0":
                    status = "测试交易";
                    break;
                case "1":
                    status = "正常交易";
                    break;
                case "8":
                    status = "已退货";
                    break;
                case "9":
                    status = "已取消";
                    break;
            }
            return status;
        }

        function formatDate(value, cell) {
            if (value) {
                return value.split(" ")[0]
            }
            else {
                return "-"
            }
        }

        //金额格式化
        function formatRMB(value, cell) {
            value = value.toString().replace(/\$|\,/g, '');
            if (isNaN(value))
                value = "0";
            sign = (value == (value = Math.abs(value)));
            value = Math.floor(value * 100 + 0.50000000001);
            cents = value % 100;
            value = Math.floor(value / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((value.length - (1 + i)) / 3) ; i++)
                value = value.substring(0, value.length - (4 * i + 3)) + ',' +
                value.substring(value.length - (4 * i + 3));
            return (((sign) ? '' : '-') + '<b>￥</b>' + value + '.' + cents);
        }

        util.View = function (VOUCHER_NO)
        {
            $.ajax({
                url: options.ViewDetailInfo,
                type: "POST",
                data: { VOUCHER_NO: VOUCHER_NO },
                success: function (result) {// $('#confirmModal').modal('show');
                    var Data = result.data;
                    if (result.status==1)
                    {
                        $('#confirmModal').modal('show');
                        $("#STORE_NAME").val(Data.STORE_NAME);
                        $("#VOUCHER_NO").val(Data.VOUCHER_NO);
                        $("#INVALID_DATE").val(ChangeDateFormat(Data.INVALID_DATE));
                        $("#USER_STORES").val(Data.USER_STORES);
                        $("#USED_STORE_ID").val(Data.USED_STORE_ID);
                        $("#USED_TIME").val(ChangeDateFormat(Data.USED_TIME));
                        $("#REMARK").val(Data.REMARK);
                    }
                }
            });
        }

        function ChangeDateFormat(time) {
            if (time != null) {
                var date = new Date(parseInt(time.replace("/Date(", "").replace(")/", ""), 10));
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                return date.getFullYear() + "-" + month + "-" + currentDate;
            }
            return "";
        }


    });

})(window, undefined, jQuery);
