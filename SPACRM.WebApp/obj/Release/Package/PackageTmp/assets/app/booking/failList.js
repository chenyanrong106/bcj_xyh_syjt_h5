; (function (window, undefined, $) {

    $(document).ready(function () {
        //搜索时间
        $('#SearchDate').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd"
        });
        var gridopt = {
            url: options.listUrl,
            colModel: [
                    { display: '编号', name: 'ID', sortable: true, hide: false, align: 'left', iskey: true },
                    { display: '姓名', name: 'CUST_NAME', sortable: false, align: 'left' },
                    //{ display: '开始时间', name: 'BEGIN_DATE', sortable: false, align: 'left' },
                    // { display: '结束时间', name: 'END_DATE', sortable: false, align: 'left' },
                    { display: '预约到店时间', name: 'BEGIN_END', sortable: false, align: 'left', process: processBE },
                    { display: '预约提交时间', name: 'CREATE_DATE', sortable: false, align: 'left' },
                    { display: '服务项目', name: 'CATEGORY_ID', sortable: false, align: 'left' },
                    //{ display: '证件号码', name: 'IDCARD', width: 160, sortable: false, align: 'left' },
                    { display: '房间', name: 'ROOM_NAME', sortable: false, align: 'left' },
                    { display: '护理师', name: 'STAFF_NAME', sortable: false, align: 'left' },
                    { display: '备注', name: 'REMARK', sortable: false, align: 'left' },
                    { display: '是否电话确认', name: 'IS_CONFIRM', hide: true, sortable: false, align: 'left' },
                    { display: '电话确认时间', name: 'CONFIRM_DATE', sortable: false, align: 'left' },
                    { display: '电话确认人', name: 'CONFIRM_STAFFNAME', sortable: false, align: 'left' },
                    { display: '操作', name: 'ID', sortable: false, align: 'center', process: processOp },
                    { display: '开始时间', name: 'BEGIN_DATE', sortable: false, hide: true, align: 'left' },              //12
                    { display: '结束时间', name: 'END_DATE', sortable: false, hide: true, align: 'left' }                 //13
            ],
            sortname: "A.Id",
            sortorder: "ASC",
            title: false,
            rp: 10,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp(value, cell) {
            var ops = [];
            var status = cell[8].toUpperCase();
            if (status == "TRUE") {
                ops.push("&nbsp;<span title='已电话确认' class='abtn' >已确认</span>");
            }
            else {
                ops.push("&nbsp;<a title='电话确认' class='abtn' href='javascript:;'  onclick=\"util.Confirm('", value, "','", cell[1], "')\"><i class='fa fa-edit' ></i>电话确认</a>");
                ops.push("&nbsp;&nbsp;<a title='修改预约信息' class='abtn' href='javascript:;'  onclick=\"util.Edit('", value, "')\"><i class='fa fa-edit' ></i>修改</a>");
            }
            ops.push("&nbsp;&nbsp;<a title='取消预约信息' class='abtn' href='javascript:;'  onclick=\"util.Delete('", value, "','", cell[1], "')\"><i class='fa fa-trash-o' ></i>取消</a>");

            return ops.join("");
        }
        function formatGender(value, cell) {
            if (value == "1") {
                return "男";
            }
            else {
                return "女";
            }
        }
        function formatDate(value, cell) {
            if (value) {
                return value.split(" ")[0]
            }
            else {
                return "-"
            }
        }
        function processBE(value, cell) {
            //var str = cell[12].substring(0, 10) + " " + cell[12].substring(10, 15) + "~" + cell[13].substring(10, 15);
            //return str;

            var day = cell[12].split(" ")[0]  //预约天
            var startTime = cell[12].split(" ")[1].substring(0, 5);
            var endTime = cell[13].split(" ")[1].substring(0, 5);
            return day + " " + startTime + "~" + endTime;
        }
        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });

        $("#btnAdd").click(function (e) {
            var url = options.editUrl;
            window.xjDailog.Open(url, {
                width: 680,
                height: 570,
                caption: '新增客户信息',
                theme: "simple", //默认主题
                onclose: function (userstate) {
                    xjgrid.Reload();

                } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
            });
        });
        $("#btnConfirm").click(function (e) {
            $('#confirmModal').modal('hide');
            var id = $("#hdCurrentId").val();
            $.post(options.setStatusUrl + "/" + id, { id: id, status: 9 },
                  function (res) {
                      if (res.status > 0) {
                          showLoadingMsg("操作成功", { right: 100, top: 10 }, true, 5000);
                          xjgrid.Reload();
                      }
                      else {
                          showErrorTip("操作失败！：" + res.message, { right: 100, top: 10 }, true, 5000);
                      }
                  },
                  "json"
            );
        });
        $("#btnConfirmOk").click(function (e) {
            $('#confirmModal').modal('hide');
            var id = $("#hdCurrentId").val();
            $.post(options.setStatusUrl + "/" + id, { id: id, status: 1 },
                  function (res) {
                      if (res.status > 0) {
                          showLoadingMsg("操作成功", { right: 100, top: 10 }, true, 5000);
                          xjgrid.Reload();
                      }
                      else {
                          showErrorTip("操作失败！：" + res.message, { right: 100, top: 10 }, true, 5000);
                      }
                  },
                  "json"
            );
        });
        util.Edit = function (id) {
            //var url = options.editUrl + "/" + id;
            var url = "/Booking/New.do/" + id;
            window.xjDailog.Open(url, {
                width: 600,
                height: 600,
                caption: '编辑预约信息',
                theme: "simple", //默认主题
                onclose: function (userstate) {
                    xjgrid.Reload();
                } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
            });
        };
        util.Delete = function (id, name) {
            $("#lbuserName").html("您确认要取消 [" + name + "] 的预约吗？");
            $("#hdCurrentId").val(id);
            $("#btnConfirmOk").hide();
            $("#btnConfirm").show();

            $('#confirmModal').modal('show');
        };

        util.View360 = function (id) {
            //TODO:
            alert("跳转到客户360页面")
        };
        $("#btnCancel").click(function (e) {
            if (parent && parent.xjDailog) {
                parent.xjDailog.Close(false, false, null);
            }
            else {
                window.close();
            }
        });

        util.Confirm = function (id, name) {
            $("#lbuserName").html("您已经电话确认[" + name + "]预约信息了吗？");
            $("#hdCurrentId").val(id);
            $("#btnConfirm").hide();
            $("#btnConfirmOk").show();
            $('#confirmModal').modal('show');
        };
        $("#btnReturn").click(function () {
            window.location.href = "/Booking/Index.do";
        });
    });

})(window, undefined, jQuery);