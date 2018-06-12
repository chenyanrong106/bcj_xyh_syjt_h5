; (function (window, undefined, $) {

    $(document).ready(function () {

        var gridopt = {
            url: options.listUrl,
            colModel: [
                    { display: '内容', name: 'CONTENT', width: "60%", sortable: true, hide: false, align: 'left', iskey: true },
                    { display: '时间', name: 'SEND_TIME', width: "20%", sortable: false, align: 'left' },
                    { display: '状态', name: 'STATUS', width: "10%", sortable: false, align: 'left',process:formatStatus },
                    { display: '创建人', name: 'CREATE_USER', width: "10%", sortable: false, align: 'left' }
            ],
            sortname: "ID",
            sortorder: "DESC",
            title: false,
            rp: 15,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function formatStatus(value, cell) {
            var ops = [];
            ops.push(value==1?"已发送":"未发送");
            return ops.join("");
        } 
        //颜色选择器
        $('.cpicker').colorpicker();
        $('#BEGINTIME').timepicker();
        $('#ENDTIME').timepicker();

        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });
        $("#btnSave").click(function () {
            alert(1);
            var id = $("#SCHID").val();
            var name = $("#NAME").val();
            var color = $("#COLOR").val();
            var btime = $("#BEGINTIME").val();
            var etime = $("#ENDTIME").val();
            $.post(options.editUrl, { ID: id, NAME: name, COLOR: color, BEGINTIME: btime, ENDTIME: etime },
                function (ret) {
                    if (ret.status > 0) {
                        _showInfoMessage('操作成功！', 'success');
                        $('#EditModal').modal('hide');
                        xjgrid.Reload();
                    }
                    else {
                        _showInfoMessage('操作失败！' + ret.message, 'error');
                        return false;
                    }
                },
                "json"
               );
        });
        $("#btnAdd").click(function (e) {
            $('#frmSave')[0].reset();
            $("#SCHID").val(0);
            $('#EditModal').modal('show');
        });
        $("#btnConfirm").click(function (e) {
            $('#confirmModal').modal('hide');
            var id = $("#SCHID").val();

            $.post(options.deleteUrl + "/" + id, { id: id },
                  function (res) {
                      if (res.status == 0) {
                          _showInfoMessage('操作成功！', 'success');
                          xjgrid.Reload();
                      }
                      else {
                          _showInfoMessage('操作失败！' + ret.message, 'error');
                          return false;
                      }
                  },
                  "json"
            );
        })
        util.Edit = function (id) {

            $.post(options.detailUrl, { id: id },
                function (ret) {
                    if (ret.status > 0) {
                        var data = eval(ret.data);
                        $("#SCHID").val(id);
                        $("#NAME").val(data.NAME);
                        $("#COLOR").val(data.COLOR);
                        $("#BEGINTIME").val(data.BEGINTIME);
                        $("#ENDTIME").val(data.ENDTIME);
                        //$("#divColor").attr("data-color", data.COLOR);
                    }
                },
                "json"
               );
            $('#EditModal').modal('show');

        };
        util.Delete = function (id, name) {
            $("#lbuserName").html(name);
            $("#SCHID").val(id);
            $('#confirmModal').modal('show');
        };
    });

})(window, undefined, jQuery);