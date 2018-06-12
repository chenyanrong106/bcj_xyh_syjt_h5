; (function (window, undefined, $) {
    $(document).ready(function () {

        $('#ORDER_START_DATE').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            autoclose: true
        });
        $('#ORDER_END_DATE').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            autoclose: true
        });
        //初始化日期时间
        var day = new Date().Format("yyyy-MM-dd");
        var storeID = $('#STORE_ID').val();
        var gridopt = {
            url: options.listUrl + "?ORDER_START_DATE=" + day + "&ORDER_END_DATE=" + day + "&STORE_ID=" + storeID,
            colModel: [
                //{ display: 'N', name: 'N', width: "50px", sortable: false, align: 'left' },
{ display: '日期', name: 'date', width: "100px", sortable: false, align: 'left', iskey: true },
{ display: '门店', name: 'STORE_NAME', width: "50px", sortable: false, align: 'left' },
{ display: '客户编号', name: 'CUST_NO', width: "50px",hide: true, sortable: false, align: 'left' },
{ display: '客户卡号', name: 'card_no', width: "50px", sortable: false, align: 'left' },
{ display: '订单号', name: 'ORDER_NO', width: "50px", sortable: false, align: 'left' },
{ display: '总额', name: 'total_amt', width: "50px", sortable: false, align: 'left' },
{ display: '应付', name: 'tt_amp', width: "50px", sortable: false, align: 'left' },
{ display: '现金', name: 'XJ', width: "50px", sortable: false, align: 'left' },
{ display: '银联卡', name: 'YLK', width: "50px", sortable: false, align: 'left' },
{ display: '外卡', name: 'WK', width: "50px", sortable: false, align: 'left' },
{ display: '微信支付', name: 'WXZF', width: "50px", sortable: false, align: 'left' },
{ display: '支付宝', name: 'ZFB', width: "50px", sortable: false, align: 'left' },
{ display: '大众点评团购', name: 'DZDP', width: "50px", sortable: false, align: 'left' },
{ display: '大众点评闪惠', name: 'DZDPSH', width: "50px", sortable: false, align: 'left' },
{ display: '美团', name: 'MT', width: "50px", sortable: false, align: 'left' },
{ display: '糯米', name: 'NM', width: "50px", sortable: false, align: 'left' },
{ display: '转账', name: 'ZZ', width: "50px", sortable: false, align: 'left' },
{ display: '现金流合计', name: 'XJSUM', width: "50px", sortable: false, align: 'left' },
{ display: '现金券', name: 'XJQ', width: "50px", sortable: false, align: 'left' },
{ display: '消费券', name: 'xfq', width: "50px", sortable: false, align: 'left' },
{ display: '会员卡扣款', name: 'HYKKK', width: "50px", sortable: false, align: 'left' },
{ display: '会员卡实际扣款', name: 'HYKSJKK', width: "50px", sortable: false, align: 'left' },
{ display: '新客充值', name: 'XKCZ', width: "50px", sortable: false, align: 'left' },
{ display: '老客续充', name: 'LKXC', width: "50px", sortable: false, align: 'left' },
{ display: '退卡', name: 'TK', width: "50px", sortable: false, align: 'left' },
{ display: '赠送奖励', name: 'ZSJL', width: "50px", sortable: false, align: 'left' },
{ display: '疗程卡购买', name: 'LCKGM', width: "50px", sortable: false, align: 'left' },
{ display: '疗程卡消耗', name: 'LCKXX', width: "50px", sortable: false, align: 'left' },
{ display: '会员标志', name: 'HY', width: "50px", sortable: false, align: 'left' },
{ display: '新客标志', name: 'XK', width: "50px", sortable: false, align: 'left' },
{ display: '老客标志', name: 'LK', width: "50px", sortable: false, align: 'left' },
{ display: '散客标志', name: 'SK', width: "50px", sortable: false, align: 'left' },
{ display: '总计', name: 'ZJ', width: "50px", sortable: false, align: 'left' },
{ display: '实物产品数量', name: 'swcpsl', width: "50px", sortable: false, align: 'left' },
{ display: '实物产品金额', name: 'swcpje', width: "50px", sortable: false, align: 'left' },
{ display: '服务实际营业额', name: 'fwsjyye', width: "50px", sortable: false, align: 'left' }
            ],
            sortname: "[date]",
            sortorder: "ASC",
            title: false,
            rp: 10,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt); 


        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });

        //查询按钮事件
        $("#btnSearch").click(function () {
            //提交表单查询
            $("#formQuery").submit();
        });

    });

})(window, undefined, jQuery);