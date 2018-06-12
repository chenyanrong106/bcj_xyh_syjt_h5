; (function (window, undefined, $) {

    $(document).ready(function () {
        //初始化日期时间
        var curr = new Date();
        var begin = curr.Format("yyyy-MM-dd") + options.beginTime;
        var end = new Date(curr.valueOf() + 1 * 24 * 60 * 60 * 1000).Format("yyyy-MM-dd") + options.endTime;

        var storeID = $('#STORE_ID').val();
        var gridopt = {
            url: options.listUrl + "?ORDER_START_DATE=" + escape(begin) + "&ORDER_END_DATE=" + escape(end) + "&ORDER_STATUS=0&STORE_ID=" + storeID,
            colModel: [
                    { display: '编号', name: 'ID', width: "5%", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '顾客ID', name: 'CUST_ID', width: "9%", sortable: false, hide: true, align: 'left' },
                    { display: '订单时间', name: 'TRANS_DATE', width: "9%", sortable: false, align: 'left', process: formatDate },
                    { display: '订单号', name: 'ORDER_NO', width: "10%", sortable: false, align: 'left', process: formatOrder },
                    { display: '状态', name: 'ORDER_STATUSS', width: "8%", sortable: false, align: 'left' },
                    { display: '状态', name: 'ORDER_STATUS', width: "6%", sortable: false, hide: true, align: 'left' },
                    { display: '顾客姓名', name: 'CUST_NAME', width: "9%", sortable: false, align: 'left', process: formatCUST },
                    { display: '订单项目', name: 'PRODUCT_NAME', width: "20%", sortable: false, align: 'left' },
                    { display: '应付金额', name: 'PAY_AMT', width: "9%", sortable: false, align: 'left', process: formatRMB },
                    { display: '实付金额', name: 'PAYED_AMT', width: "9%", sortable: false, align: 'left', process: formatSFRMB },
                    { display: '付款方式', name: 'PAYMENT_TYPE', width: "10%", sortable: false, align: 'left' },
                    { display: '付款状态', name: 'PAY_STATUS', width: "10%", hide: true, sortable: false, align: 'left' },
                    { display: '操作', name: 'ID', width: "15%", sortable: false, align: 'left    ', process: processOp },
                    { display: 'type', name: 'PROD_TYPE', width: "15%", sortable: false, hide: true, align: 'left    ', process: processOp }
            ],
            sortname: "Id",
            sortorder: "DESC",
            title: false,
            rp: 10,
            usepager: true,
            showcheckbox: true
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp(value, cell) {
            var ops = [];

            if (cell[4] == "未付款") {
                ops.push("&nbsp;<a title='订单付款' class='abtn' href='javascript:;'  onclick=\"util.Pay('", value, "','", cell[1], "')\"><i class='fa fa-search' ></i> 付款</a>");
                ops.push("&nbsp;<a title='删除订单' class='abtn' href='javascript:;'  onclick=\"util.Delete('", value, "','", cell[3], "')\"><i class='fa fa-ban' ></i> 删除</a>");
            }
            else if (cell[4] == "已付款") {
                ops.push("&nbsp;<a title='业绩分配' class='abtn' href='javascript:;'  onclick=\"util.EmpPer('", value, "','", cell[1], "')\"><i class='fa fa-ticket' ></i> 业绩分配</a>");

                if (cell[13] == "0" || cell[13] == "2" || cell[13] == "3") {
                    ops.push("&nbsp;<a title='作废' class='abtn' href='javascript:;'  onclick=\"util.View('", value, "','", cell[1], "')\"><i class='fa fa-ban' ></i> 作废</a>");
                }
                //if (cell[6] == "散客") {
                //    ops.push("&nbsp;<a title='修改顾客' class='abtn' href='javascript:;'  onclick=\"util.updateCust('", value, "','", cell[1], "')\"><i class='fa fa-search' ></i> 修改顾客</a>");
                //}
            }
            else if (cell[4] == "已取消") {
                ops.push("&nbsp;<a title='删除订单' class='abtn' href='javascript:;'  onclick=\"util.Delete('", value, "','", cell[3], "')\"><i class='fa fa-ban' ></i> 删除</a>");
            }
            return ops.join("");
        }
        function formatDate(value, cell) {
            if (value) {
                //return value.split(" ")[0]
                return value
            }
            else {
                return "-"
            }
        }
        function formatCUST(value, cell) {
            var custname = "";
            if (value == "散客") {
                custname = value;
            }
            else {
                custname = "<a href='/Customer/View360.do?cid=" + cell[1] + "'>" + value + "</a>";
            }
            return custname;
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
        //金额格式化
        function formatSFRMB(value, cell) {
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
            //var arr = "";
            //if (cell[11] == 2)
            //{
            //    arr = '<span class="badge badge-danger" id="bookListCount">欠</span>';
            //}
            return (((sign) ? '' : '-') + '<b>￥</b>' + value + '.' + cents);
        }
        //订单详细
        function formatOrder(value, cell) {
            var href = "";
            if (cell[4] == "未付款") {
                //转到支付页面
                href = "/Order/OrderD.do?cid=" + cell[1] + "&oid=" + cell[0];
            }
            else if (cell[4] == "已付款") {
                //转到明细页面
                //转到明细页面 已作废
                href = "/Order/OrderB.do?cid=" + cell[1] + "&oid=" + cell[0];
            }
            else if (cell[4] == "已作废") {
                //转到明细页面 已作废
                href = "/Order/OrderB.do?cid=" + cell[1] + "&oid=" + cell[0];
            }
            var arr = "";
            if (cell[11] == 2) {
                arr = '<span class="badge badge-danger">欠</span>';
            }
            return "<a href='" + href + "'>" + value + arr + "</a>";
        }
        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });

        $("#btnAdd").click(function (e) {
            //新增订单
            window.location.href = "/Order/OrderD.do";
        });
        $("#btnCard").click(function (e) {
            //购卡
            window.location.href = "/Order/OrderCard.do";
        });

        //退款
        util.Refund = function (id) {
            var url = options.editUrl + "/" + id;
            window.xjDailog.Open(url, {
                width: 680,
                height: 570,
                caption: '编辑客户信息',
                theme: "simple", //默认主题
                onclose: function (userstate) {
                    xjgrid.Reload();
                } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
            });
        };

        util.updateCust = function (id, name) {
            $("#OrderID").val(id);
            var height = $("#QueryProdInfo_boxcontain2").height(); //页面打开时提示刷卡
            var width = $("#QueryProdInfo_boxcontain2").width();
            $("#QueryProdInfo_mask2").show();
            $("#QueryProdInfo_boxcontain2").css("top", (window.screen.height - height) / 2).css("left", (window.screen.width - width) / 2).show();
        };

        $("#QueryProdInfo_close2").click(function () {
            $("#QueryProdInfo_mask2").hide();
            $("#QueryProdInfo_boxcontain2").hide();
        });

        //根据姓名/电话检索
        $("#CUST_NAME").select2({
            placeholder: "输入姓名/电话/卡号查询",
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
            //客户信息*****************************//
            var cid = obj.id;
            $("#NewCust").html("已选会员：" + obj.title);
            $("#CustID").val(obj.id);
            //alert(cid);
        }

        //修改顾客
        $("#btnSaveCust").click(function () {
            if ($("#CustID").val() == "0") {
                _showInfoMessage("请选择会员！", "info");
                return false;
            }
            else if ($("#OrderID").val() == "0") {
                _showInfoMessage("请选择账单！", "info");
                return false;
            }
            else {
                $.post("/order/UpdateOrderCustUser.do?orderID=" + $("#OrderID").val() + "&custID=" + $("#CustID").val(), {},
                 function (data) {
                     if (data.status >= 0) {
                         //新增成功
                         window.location.href = window.location.href;
                     }
                     else {
                         _showInfoMessage(data.message, 'info');
                         return false;
                     }

                 },
          "json"
         );
            }

        });
        //作废订单
        util.Cancell = function (id, name) {
            //$("#lbOrderNo").html(name);
            //$("#hdCurrentId").val(id);
            //$('#confirmModal').modal('show');
            window.location.href = "/Order/OrderB.do?cid=" + cid + "&oid=" + oid;
        };

        //查看订单
        util.View = function (oid, cid) {
            window.location.href = "/Order/OrderB.do?cid=" + cid + "&oid=" + oid;
        };

        //付款
        util.Pay = function (oid, cid) {
            window.location.href = "/Order/orderpay.do?cid=" + cid + "&oid=" + oid;
        };

        //业绩分配
        util.EmpPer = function (oid, cid) {
            window.location.href = "/Order/EmpPer.do?cid=" + cid + "&oid=" + oid;
        };

        util.Delete = function (oid, orderNo) {
            //if (!confirm("您确认要删除该订单吗")){
            //    return false;
            //}
            //$.post(options.deleteUrl, { cid: cid,id:oid },
            //        function (data) {
            //            if (data.status > 0) {
            //                _showInfoMessage('操作成功！', 'success');
            //                xjgrid.Query(this);
            //                return false;
            //            }
            //            else {
            //                _showInfoMessage('操作失败！' + data.message, 'info');
            //                return false;
            //            }

            //        },
            //        "json"
            //       );
            $("#lbuserName").html(orderNo);
            $("#hdCurrentId").val(oid);
            $('#confirmModal').modal('show');
        }
        //确认删除订单
        $("#btnConfirm").click(function () {
            $('#confirmModal').modal('hide');
            var id = $("#hdCurrentId").val();

            $.post(options.deleteUrl, { id: id },
                    function (data) {
                        if (data.status > 0) {
                            _showInfoMessage('操作成功！', 'success');
                            xjgrid.Reload();
                            return false;
                        }
                        else {
                            _showInfoMessage('操作失败！' + data.message, 'info');
                            return false;
                        }

                    },
                    "json"
                   );
        });


        ////初始化日期时间
        //var nowDay = new Date().Format("yyyy-MM-dd ");     
        //$('#btnORDER_DATE span').html(nowDay + "至" + nowDay);

        ////时间范围快速查询
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
        //        $("#ORDER_START_DATE").val(start.format('YYYYMMDD'));
        //        $("#ORDER_END_DATE").val(end.format('YYYYMMDD'));
        //        //先清空订单号查询条件
        //        $("#ORDER_NO").val("");
        //        //提交表单查询
        //        $("#formQuery").submit();

        //    }     
        //);
        $('#ORDER_START_DATE').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            autoclose: true
        }).on('hide', function (ev) {
            var date = this.value + options.beginTime;
            $(this).val(date);
        }).val(function () {
            var date = this.value + options.beginTime;
            return date;
        });
        $('#ORDER_END_DATE').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            autoclose: true
        }).on('hide', function (ev) {
            var date = this.value + options.endTime;
            $(this).val(date);
        }).val(function () {
            var date = this.value + options.endTime;
            return date;
        });

        //订单状态选择事件
        $("#ORDER_STATUS").change(function (e) {
            var status = $("#ORDER_STATUS").val();
            if (status == "0") {
                $("#btnCombine").show();
            }
            else {
                $("#btnCombine").hide();
            }
            //先清空订单号查询条件
            $("#ORDER_NO").val("");
            //提交表单查询
            $("#formQuery").submit();
        });

        //查询按钮事件
        $("#btnSearch").click(function () {
            //先清空其他条件
            //$("#ORDER_START_DATE").val("");
            //$("#ORDER_END_DATE").val("");
            //$("#ORDER_STATUS").val(null);
            //提交表单查询
            $("#formQuery").submit();
        });

        //合并订单
        $("#btnCombine").click(function () {
            var data = xjgrid.GetCheckedRowDatas(formatPostData);
            if (data == null || data == "") {
                _showInfoMessage("请您先选择要合并的订单！", "info");
                return false;
            }
            var count = data.toString().split(',').length;
            if (count <= 1) {
                _showInfoMessage("合并的订单必须两个以上！", "info");
                return false;
            }
            window.location.href = "/Order/OrderCombine.do/" + data;
        });

        function formatPostData(cell) {
            return [cell[0]].join("_");
        }

    });
})(window, undefined, jQuery);