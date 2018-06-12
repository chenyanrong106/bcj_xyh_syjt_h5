; (function (window, undefined, $) {

    $(document).ready(function () {
        var gridopt = {
            url: options.listUrl,
            colModel: [
                    { display: '编号', name: 'ID', width: "0", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '名称', name: 'Name', width: "30", sortable: false, align: 'left' },
                    { display: '张数', name: 'SumNum', width: "30", sortable: false, align: 'left' },
                    { display: '已领', name: 'LQ', width: "30", sortable: false, align: 'left' },
                    { display: '面值', name: 'MinMoney', width: "30", sortable: false, align: 'left' },
                    { display: '说明', name: 'Detail', width: "45", sortable: false, align: 'left' },
                    { display: '页面', name: 'Guid', width: "45", sortable: false, align: 'left', process: processOp2 },
                    { display: '操作', name: 'ID', width: "25", sortable: false, align: 'center', process: processOp }
            ],
            sortname: "ROLE_ID",
            sortorder: "ASC",
            title: false,
            rp: 15,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp(value, cell) {
            var ops = [];
            ops.push("&nbsp;<a title='编辑角色信息' class='abtn' href='javascript:;'  onclick=\"util.Edit('", value, "')\"><i class='fa fa-edit' ></i>编辑</a>");
            //ops.push("&nbsp;&nbsp;<a title='删除角色信息' class='abtn' href='javascript:;'  onclick=\"util.Delete('", value, "','", cell[5], "')\"><i class='fa fa-trash-o' ></i>领取页面</a>");
            return ops.join("");
        }

        function processOp2(value, cell) {
            var url ="http://"+ window.location.host + "/wechat/discount/quan.aspx?guid=" + value;
            return url;
        }

        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });

        $("#btnAdd").click(function (e) {
    //        $("#ROLE_ID").val(0);
    //        $("#usertree").treeview(
    //         {
    //             url: options.getTreeList + "?orgId=" + $("#ORG_ID").val() + "&roleId=0" + $("#ROLE_ID").val(),
    //             showcheck: true,
    //             cascadecheck: true,
    //             onnodeclick: TreeNode_Click,
    //             theme: "bbit-tree-no-lines" //bbit-tree-lines ,bbit-tree-no-lines,bbit-tree-arrows
    //         }
    //);

    //        $('#frmSave')[0].reset();
            //        $('#EditModal').modal('show');
            location = "discountedit.do?id=0";
        });

        util.Edit = function (id) {
            location = "discountedit.do?id="+id;
        };

        function TreeNode_Click(data) {
            data.expand();
        }

        $("#btnSave").click(function (e) {

            var teeSd = $("#usertree").getTSVs();
            if (teeSd == "") {
                _showInfoMessage("请选择权限", 'error');
                return;
            }
            $("#AddIDS").val(teeSd);
            $("#frmSave").submit();
        });

        $('#frmSave').validator({
            rules: {
            },
            fields: {
                '#ROLE_NAME': 'required',
            },
            valid: function (form) {
                FormSubmit(form, function (res) {
                    if (res.status == 0) {
                        _showInfoMessage("操作成功！", 'success');
                        $('#EditModal').modal('hide');
                        xjgrid.Reload();
                    }
                    else {
                        _showInfoMessage("操作失败：" + res.message, 'error');
                        //showErrorTip("操作失败！：" + res.message, { left: 100, top: 10 }, true, 5000);
                    }
                })
            }
        });

        util.Delete = function (id, name) {
            //$("#lbuserName").html(name);
            //$("#hdCurrentId").val(id);
            //$('#confirmModal').modal('show');
            var url = window.location.host + "/wechat/discount/quan.aspx?guid=" + name;
            console.log(url);
            //location = url;
            open(url);
        };

        $("#btnConfirm").click(function (e) {
            $('#confirmModal').modal('hide');
            var id = $("#hdCurrentId").val();
            $.post(options.deleteUrl + "/" + id, { id: id },
                  function (res) {
                      if (res.status == 0) {
                          _showInfoMessage("操作成功！", 'success');
                          xjgrid.Reload();
                      }
                      else {
                          _showInfoMessage("操作失败：" + res.message, 'error');
                      }
                  },
                  "json"
            );
        })

    });

})(window, undefined, jQuery);