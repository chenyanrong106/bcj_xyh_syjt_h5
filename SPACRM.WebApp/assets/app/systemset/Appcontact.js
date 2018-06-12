; (function (window, undefined, $) {

    $(document).ready(function () {
        var _storeID = $('#STORE_ID').val();
        var gridopt = {
            url: options.listUrl,
            colModel: [
                    { display: '编号', name: 'ID', width: "10%", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '编号', name: 'CUST_ID', width: "10%", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '顾客姓名', name: 'NAME', width: "10%", sortable: false, align: 'left', process: processOp },
                    { display: '顾客手机', name: 'MOBILE', width: "10%", sortable: true, hide: false, align: 'left' },
                    { display: '顾客信息', name: 'CONTACT_INFO', width: "10%", sortable: false, align: 'left' },
                    { display: '反馈类型', name: 'CONTACT_TYPE', width: "10%", sortable: false, align: 'left' },
                    { display: '反馈内容', name: 'CONTENT', width: "60%", sortable: false, align: 'left' }
            ],
            sortname: "ID",
            sortorder: "desc",
            title: false,
            rp: 15,
            usepager: true,
            showcheckbox: false,
            extparams: [{ name: "STORE_ID", value: _storeID }]
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp(value, cell) {
            var ops = [];
            var url = "/Customer/View360.do?cid="+cell[1];
            ops.push("&nbsp;<a title='查看会员信息' class='abtn'  href='" + url + "'>" + value + "</a>");
            return ops.join("");
        }
        function formatColor(value, cell) {
            return "<a title=" + value + " style='float:left;width:60px;height:25px;;background-color:" + value + "'></a>&nbsp;[" + value + "]";
        }
        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });

        $('#btnSearch').click(function () {
            $("#formQuery").submit();
        });

    });

})(window, undefined, jQuery);