; (function (window, undefined, $) {

    $(document).ready(function () {
        var day = " 00:00";
        var day2 = " 00:00";
        $.ajax({
            url: "/Report/GetBegAndEndTime.do",
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
            url: options.listUrl,
            colModel: [
                { display: '编 号', name: 'ID', sortable: true, hide: true, align: 'left', iskey: true },
                { display: '会籍店', name: 'STORE_NAME', width: "20%", sortable: false, align: 'left' },
                { display: '表面号', name: 'CARD_NO', width: "10%", sortable: false, align: 'left' },
                { display: '姓 名', name: 'NAME', width: "8%", sortable: false, align: 'left', process: processDetail },
                { display: '手机号', name: 'MOBILE', width: "10%", sortable: false, align: 'left' },
                { display: '性 别', name: 'GENDER', width: "6%", sortable: false, align: 'left' },
                { display: '生 日', name: 'BIRTHDAY', width: "6%", sortable: false, align: 'left' },
                { display: '地址', name: 'ADDRESS', width: "20%", sortable: false, align: 'left' },
                { display: '邮箱', name: 'EMAIL', width: "7%", sortable: false, align: 'left'}
                //{ display: '会员卡号', name: 'CARD_NO', width: "10%", sortable: false, align: 'left' },
                //{ display: '密码', name: 'PASSWORD', width: "10%", sortable: false, align: 'left', hide: true },
                //{ display: '卡级（余额）', name: 'CARD_NAME', width: "28%", sortable: false, align: 'left', process: processCard },
                //{ display: '余额', name: 'BALANCE', width: "8%", sortable: false, align: 'right' },
                //{ display: '操作', name: 'ID', width: "17%", sortable: false, align: 'center' }
            ],
            sortname: "ID",
            sortorder: "ASC",
            title: false,
            rp: 15,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);


        function processDetail(value, cell) {
            var url = options.view360Url + "?cid=" + cell[0];
            return "<a href='" + url + "'>" + value + "</a>";
        }

        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });
    });

})(window, undefined, jQuery);