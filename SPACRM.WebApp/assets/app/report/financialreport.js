﻿
; (function (window, undefined, $) {

    $(document).ready(function () {
        var day = " 00:00";
        var day2 = " 00:00";
        $.ajax({
            url: "GetBegAndEndTime.do",
            type: "POST",
            data: { "PID": 0 },
            async: false,
            timeout: 15000,
            success: function (result) {
                day = result.data.HOURS_BEGIN;
                day2 = result.data.HOURS_END;
            }
                ,
            error: function (result) {
                alert(result.error);
            }
        });
        $('#BEGIN_DATE').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd"
        }).on('hide', function (ev) {
            $(this).val($(this).val() + day);
        });
        $('#END_DATE').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd"
        }).on('hide', function (ev) {
            $(this).val($(this).val() + day2);
        });

        $("#BEGIN_DATE").val(new Date().Format("yyyy-MM-dd") + day);
        $("#END_DATE").val(new Date().Format("yyyy-MM-dd") + day2);
        $.cookie("BEGIN_DATE", $("#BEGIN_DATE").val());
        $.cookie("END_DATE", $("#END_DATE").val());
        //初始化日期时间
        var nowDay = new Date().Format("yyyy-MM-dd ");
        $('#btnORDER_DATE span').html(nowDay + "至" + nowDay);

        //时间范围快速查询
        //$('#btnORDER_DATE').daterangepicker(
        //    {
        //        ranges: {
        //            '今天': [moment(), moment()],
        //            '昨天': [moment().subtract('days', 1), moment().subtract('days', 1)],
        //            '本周': [moment().subtract('days', 6), moment()],
        //            //'上周': [moment().subtract('days', 13), moment()],
        //            '本月': [moment().startOf('month'), moment().endOf('month')],
        //            //'上月': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')],
        //            '今年': [moment().startOf('year'), moment().endOf('year')],
        //            '去年': [moment().subtract('year', 1).startOf('year'), moment().subtract('year', 1).endOf('year')],
        //        },
        //        opens: 'left',
        //        startDate: moment().subtract('days', 29),
        //        endDate: moment()
        //    },
        //    function (start, end) {
        //        //选择日期事件
        //        $('#btnORDER_DATE span').html(start.format('YYYY-MM-DD') + ' 至 ' + end.format('YYYY-MM-DD'));
        //        $("#BEGIN_DATE").val(start.format('YYYY-MM-DD'));
        //        $("#END_DATE").val(end.format('YYYY-MM-DD'));
        //        $.cookie("BEGIN_DATE", null);
        //        $.cookie("END_DATE", null);
        //        $.cookie("BEGIN_DATE", $("#BEGIN_DATE").val());
        //        $.cookie("END_DATE", $("#END_DATE").val());
        //        //先清空订单号查询条件
        //        $("#ORDER_NO").val("");
        //        //提交表单查询
        //        $("#formQuery").submit();

        //    }
        //);


        var gridopt = {
            url: options.listUrl + "?BEGIN_DATE=" + day + "&END_DATE=" + day,
            colModel: [
                    { display: '编号', name: 'ID', sortable: true, hide: true, align: 'left', iskey: true },
                     { display: '业务单编号', name: 'ORDERID', sortable: true, hide: true, align: 'left' },
                        { display: '顾客编号', name: 'CUSTID', sortable: true, hide: true, align: 'left' },
                    { display: '日期', name: 'Dates', sortable: false, align: 'left' },
                    { display: '分店号', name: 'StoreName', sortable: false, align: 'left' },
                    { display: '业务单号', name: 'ORDERNO', sortable: false, align: 'left', process: OrderDetail },
                    { display: '顾客姓名', name: 'CustName', sortable: false, align: 'left', process: CustDetail },
                   { display: '项目/产品收入', name: 'ServiceIncome', sortable: false, align: 'left', sum: true },
                   //{ display: '产品收入', name: 'ProdIncome', sortable: false, align: 'left' },
                   { display: '会员卡收入', name: 'CardIncome', sortable: false, align: 'left', sum: true },
                   { display: '现金', name: 'Cash', sortable: false, align: 'left', sum: true },
                  { display: '信用卡', name: 'BankPOS', sortable: false, align: 'left', sum: true },
                  { display: '房帐', name: 'ZK', sortable: false, align: 'left', sum: true },
                  { display: '代币券', name: 'DBQ', sortable: false, align: 'left', sum: true },
                  { display: '会员卡扣款', name: 'CardDeduct', sortable: false, align: 'left', sum: true },
                  { display: '会员卡实际扣款', name: 'CardRealDeduct', sortable: false, align: 'left', sum: true }
            ],
            sortname: "Id",
            sortorder: "ASC",
            title: false,
            rp: 20,
            localpage: true,
            usepager: true,
            sum:true
        };
        var xjgrid = new xjGrid("gridlist", gridopt);

        function CustDetail(value, cell) {
            var href = "";

            href = "../Customer/View360.do" + "?cid=" + cell[2];

            return "<a href='#' onclick=\"javascript:window.open('" + href + "')\">" + value + "</a>";

        }

        function OrderDetail(value, cell) {
            var href = "";

            href = "../Order/OrderB.do?cid=" + cell[2] + "&oid=" + cell[1];

            return "<a href='#' onclick=\"javascript:window.open('" + href + "')\">" + value + "</a>";

        }

        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });

        $("#btnExcel").click(function () {
            $.cookie("BEGIN_DATE", $("#BEGIN_DATE").val());
            $.cookie("END_DATE", $("#END_DATE").val());
            //$("#formQuery").submit();
            var stores = "";
            $("select[name=STORE] option").each(function () {
                if ($(this).val() != "") {
                    stores += $(this).val() + ",";
                }

            });
            var dq = $("#REGION").find("option:selected").text();
            var md = $("#STORE").find("option:selected").text();
            open("/assets/app/report/financialrep.aspx?type=2&beg=" + $("#BEGIN_DATE").val() + "&end=" + $("#END_DATE").val() + "&REGION=" + $("#REGION").val() + "&STORE=" + $("#STORE").val() + "&stores=" + stores + "&md=" + md + "&dq=" + dq);
        })

        $("#btnSearch").click(function () {
            $.cookie("BEGIN_DATE", $("#BEGIN_DATE").val());
            $.cookie("END_DATE", $("#END_DATE").val());
            var stores = "";
            $("select[name=STORE] option").each(function () {
                if ($(this).val() != "") {
                    stores += $(this).val() + ",";
                }
               
            });
            var dq = $("#REGION").find("option:selected").text();
            var md= $("#STORE").find("option:selected").text();
            //$("#formQuery").submit();
            open("/assets/app/report/financialrep.aspx?type=1&beg=" + $("#BEGIN_DATE").val() + "&end=" + $("#END_DATE").val() + "&REGION=" + $("#REGION").val() + "&STORE=" + $("#STORE").val() + "&stores=" + stores+"&md="+md+"&dq="+dq);
        })


        PostGetProdTypes();

        $("#REGION").change(function () {

            $("#ID").val("second");
            $.cookie("REGION", $("#REGION").val());
            PostGetProdTypes();
        })

        $("#STORE").change(function () {

            $("#ID").val("second");
            $.cookie("STORE", $("#STORE").val());


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