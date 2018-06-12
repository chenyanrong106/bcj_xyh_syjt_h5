
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
            $(this).val($(this).val()+day);
          
        });
        $('#END_DATE').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd"
        }).on('hide', function (ev) {
            $(this).val($(this).val()+day2);
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
                    { display: '业务单编号', name: 'ORDERID', sortable: true, hide: true, align: 'left' },
                    { display: '顾客编号', name: 'CUSTID', sortable: true, hide: true, align: 'left' },
                    { display: '会员姓名', name: 'CustName', sortable: false, align: 'left', process: CustDetail },
                    { display: '会员卡号', name: 'CustNo', sortable: false, align: 'left' },
                    //{ display: '会员卡级', name: 'CustName', sortable: false, align: 'left' },
                    { display: '时间', name: 'Dates', sortable: false, align: 'left' },
                    { display: '分店名称', name: 'StoreName', sortable: false, align: 'left' },
                    { display: '操作员', name: 'CREATE_USER', sortable: false, align: 'left' },
                    //{ display: '原价金额', name: 'OldAmt', sortable: false, align: 'left', sum: true },
                    //{ display: '折扣', name: 'BankPOS', sortable: false, align: 'left' },
                    //{ display: '实际金额', name: 'RealAmt', sortable: false, align: 'left', sum: true },
                    { display: '存款额', name: 'SAVING', sortable: false, align: 'left', sum: true },
                    //{ display: '存款优惠', name: 'DisAmt', sortable: false, align: 'left', sum: true },
                    { display: '卡上余额', name: 'BALANCE', sortable: false, align: 'left', sum: true },
                    { display: '邮箱', name: 'EMAIL', sortable: false, align: 'left' }
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
            $.cookie("STORE", $("#STORE").val());
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
})(window, undefined, jQuery);