
; (function (window, undefined, $) {

    $(document).ready(function () {

        //$('a.panel-collapse').click();

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
        function GetDateStr(AddDayCount) {
            var dd = new Date();
            dd.setDate(dd.getDate() + AddDayCount);//获取AddDayCount天后的日期   
            var y = dd.getFullYear();
            var m = dd.getMonth() + 1;//获取当前月份的日期   
            var d = dd.getDate();
            return y + "-" + m + "-" + d;
        }

        $("#BEGIN_DATE").val(new Date().Format("yyyy-MM-dd") + day);
        $("#END_DATE").val(GetDateStr(1) + day2);
        $.cookie("BEGIN_DATE", $("#BEGIN_DATE").val());
        $.cookie("END_DATE", $("#END_DATE").val());
        //初始化日期时间
        var nowDay = new Date().Format("yyyy-MM-dd ");
        $('#btnORDER_DATE span').html(nowDay + "至" + nowDay);



        var gridopt = {
            url: options.listUrl + "?BEGIN_DATE=" + day + "&END_DATE=" + day,
            colModel: [
                { display: '编号', name: 'ID', sortable: true, hide: true, align: 'left', iskey: true },
                { display: '日期', name: 'TRANS_DATE', sortable: true,  align: 'left' },
                { display: '会员号', name: 'CARD_NO', sortable: true,  align: 'left' },
                { display: '姓名', name: 'NAME', sortable: false, align: 'left', process: CustDetail },
                { display: '手机', name: 'MOBILE', sortable: false, align: 'left' },
                { display: '订单号', name: 'ORDER_NO', sortable: false, align: 'left' },
                { display: '活动名称', name: 'PROMOTION_NAME', sortable: false, align: 'left' },
                { display: '服务大项', name: 'CATE_NAME', sortable: false, align: 'left' },
                { display: '服务小项', name: 'PROD_NAME', sortable: false, align: 'left' },
                { display: '项目应付', name: 'PROD_PRICE', sortable: false, align: 'left', sum: true },
                { display: '项目实付', name: 'PAY_AMT', sortable: false, align: 'left', sum: true },
                { display: '优惠金额', name: 'promotion_amt', sortable: false, align: 'left', sum: true },
                { display: '订单实付', name: 'PAYED_AMT', sortable: false, align: 'left', sum: true },
                { display: '老客数', name: 'lk', sortable: false, align: 'left', sum: true,xsw:0 },
                { display: '新客数', name: 'xk', sortable: false, align: 'left', sum: true, xsw: 0 },
                { display: '会员数', name: 'hy', sortable: false, align: 'left', sum: true, xsw: 0 }
                //{ display: '订单类型', name: 'DisAmt', sortable: false, align: 'left' },
                //{ display: '充值回顾', name: 'BALANCE', sortable: false, align: 'left' }
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

        $("#btnSearch").click(function () {
            $.cookie("BEGIN_DATE", $("#BEGIN_DATE").val());
            $.cookie("END_DATE", $("#END_DATE").val());
            var stores = "";
            $("select[name=STORE] option").each(function () {
                if ($(this).val() != "") {
                    stores += $(this).val() + ",";
                }

            });
            $("#STORES").val(stores);
            $("#formQuery").submit();
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

    $("#lx").change(function () {
        $.ajax({
            type: 'Post',
            //async: false, //同步执行，不然会有问题
            dataType: "json",
            url: "getPROMOTION.do?lx=" + $(this).val(),   //提交的页面/方法名
            data: "{'userName':'" + $("#ItemName0").val() + "'}",              //参数（如果没有参数：null）
            contentType: "application/json; charset=utf-8",
            error: function (msg) {//请求失败处理函数
                alert("数据加载失败");
            },
            success: function (data) { //请求成功后处理函数。
                if (data.status > 0) {
                    $("#cxmc").html('<option value="-1">全部</option>');
                    for (var i = 0; i < data.data.length; i++) {
                        $("#cxmc").append('<option value="'+data.data[i].ID+'">'+data.data[i].NAME+'</option>');
                    }

                }
                else {
                    _showInfoMessage(data.message, 'error');
                    $("#cxmc").html('<option value="-1">全部</option>');
                    //alert(data.message);
                    return false;
                }
            }
        });
    });
})(window, undefined, jQuery);