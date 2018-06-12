; (function (window, undefined, $) {
    $(document).ready(function () {
        var gridopt = {
            url: options.pointsUrl,
            colModel: [
                    { display: '编号', name: 'ID', sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '顾客编号', name: 'CUSTID', sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '订单ID', name: 'ORDERID', sortable: true, hide: true, align: 'left'},
                    { display: '交易日期', name: 'CREATEDATE', width: "10%", sortable: false, align: 'left', process: formatDate },
                    { display: '存入积分', name: 'SAVING', width: "10%", sortable: false, align: 'left' },
                    { display: '赠送积分', name: 'GIVING', width: "10%", sortable: false, align: 'left' },
                    { display: '消耗积分', name: 'CONSUMING', width: "10%", sortable: false, align: 'left' },
                    { display: '兑换产品', name: 'GOODS_NAME', width: "20%", sortable: false, align: 'left' },
                    { display: '订单编号', name: 'ORDERNO', width: "15%", sortable: false, align: 'left', process: OrderDetail },
                    { display: '备注', name: 'REMARK', width: "20%", sortable: false, align: 'left' }
            ],
            sortname: "ID",
            sortorder: "ASC",
            title: false,
            rp: 10,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function OrderDetail(value, cell) {
            var href = "";

            href = "../Order/OrderB.do?cid=" + cell[1] + "&oid=" + cell[2];

            return "<a href='#' onclick=\"javascript:window.open('" + href + "')\">" + value + "</a>";

        }
        
        function formatDate(value, cell) {
            if (value) {
                return value.split(" ")[0]
            }
            else {
                return "-"
            }
        }

    });

})(window, undefined, jQuery);
