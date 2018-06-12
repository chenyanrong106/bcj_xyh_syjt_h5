; (function (window, undefined, $) {

    $(document).ready(function () {
        var gridopt = {
            url: options.listUrl,
            colModel: [
                    { display: '编号', name: 'ID', width: "0", sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '描述', name: 'Name', width: "15%", sortable: false, align: 'left' },
                    { display: '总扫描', name: 'TotalNum', width: "15%", sortable: false, align: 'left' },
                    { display: '本月扫描', name: 'MonthNum', width: "15%", sortable: false, align: 'left' },
                    { display: '上月扫描', name: 'smonnum', width: "15%", sortable: false, align: 'left' },
                   //{ display: '参数', name: 'QID', width: "15%", sortable: false, align: 'left' },
                    { display: '操作', name: 'Ticket', width: "10%", sortable: false, align: 'center', process: processOp }
            ],
            sortname: "id",
            sortorder: "ASC",
            title: false,
            rp: 15,
            usepager: true,
            showcheckbox: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp2(value, cell) {
            var ops = [];
            if (value == 1)
                ops.push("正常");
            else
                ops.push("撤柜");
            return ops.join("");
        }

        function processOp(value, cell) {
            var ops = [];
            ops.push("&nbsp;<a title='查看' class='abtn' href='javascript:;'  onclick=\"util.Edit('", value, "')\"><i class='fa fa-edit' ></i>查看二维码</a>");
            ops.push("&nbsp;<a title='编辑' class='abtn' href='javascript:;'  onclick=\"util.Edit3('", cell[0], "')\"><i class='fa fa-edit' ></i>编辑</a>");
            //ops.push("&nbsp;<a title='查看' class='abtn' href='javascript:;'  onclick=\"util.Edit2('", cell[2], "')\"><i class='fa fa-edit' ></i>查看门店</a>");
            return ops.join("");
        }

        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });

        $("#qb").click(function (e) {
            $("#replyType").val(0);
            $("#formQuery2").submit();
        });

        $("#gjz").click(function (e) {
            $("#replyType").val(1);
            $("#formQuery2").submit();
        });

        $("#bgz").click(function (e) {
            $("#replyType").val(2);
            $("#formQuery2").submit();
        });

        $("#zdhf").click(function (e) {
            $("#replyType").val(3);
            $("#formQuery2").submit();
        });


        $("#formQuery2").submit(function () {
            xjgrid.Query(this);
            return false;
        });
        $("#btnAdd").click(function (e) {
            // location = "StoreEdit.do?id=0";
            $("#ID").val(0);
            //$("#divkeys").show();
            //$("#divtype").show();
            //$("#divnews").show();
            //$("#divtext").hide();
            $('#frmSave')[0].reset();
            $('#EditModal').modal('show');


        });

        $("#btntb").click(function (e) {


            $.post(options.editUrl, {

            },
                 function (ret) {
                     xjgrid.Query(this);
                     return false;
                 },
                 "json"
           );
        });

        util.fx = function (id) {
            location = "/WXEmployee/EmployeeList.do";
        }

        util.cx = function (id) {
            location = "/wechat/Ticket.do";
        }

        util.pz = function (id) {
            location = "StoreQrcodeSet.do?id=" + id;
        }

        util.Edit2 = function (id) {
            open("storeedit.do?id=" + id);
        }

        util.Edit = function (id) {
            open("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + id);
            // $.post(options.editUrl, {
            //     id: id
            // },
            //      function (ret) {
            //          if (ret && ret.status == 0) {
            //              $("#ID").val(ret.data.ID);
            //              $("#replyType").val(ret.data.replyType);
            //              $("#KeyWords").val(ret.data.KeyWords);
            //              $("#MatchingType").val(ret.data.MatchingType);
            //              $("#MsgType").val(ret.data.MsgType);
            //              $("#Graphic_ID").val(ret.data.Graphic_ID);
            //              $("#Content").val(ret.data.Content);
            //              if (ret.data.replyType == 1) {
            //                  $("#divkeys").show();
            //                  $("#divtype").show();
            //              }
            //              else {
            //                  $("#divkeys").hide();
            //                  $("#divtype").hide();
            //              }

            //              if (ret.data.MsgType == "text") {
            //                  $("#divnews").hide();
            //                  $("#divtext").show();
            //              }
            //              else {
            //                  $("#divnews").show();
            //                  $("#divtext").hide();
            //              }
            //          }
            //          else {
            //              _showInfoMessage("数据库中没有此商户，请刷新重试！", 'error');
            //          }
            //      },
            //      "json"
            //);
            // $('#EditModal').modal('show');
        };

        util.Edit3 = function (id) {
             $.post(options.editUrl, {
                 id: id
             },
                  function (ret) {
                      console.log(ret);
                      if (ret && ret.status == 0) {
                          $("#ID").val(ret.data.ID);
                          $("#Name").val(ret.data.Name);
                      }
                      else {
                          _showInfoMessage("请刷新重试！", 'error');
                      }
                  },
                  "json"
            );
             $('#EditModal').modal('show');
        };

        function TreeNode_Click(data) {
            data.expand();
        }

        $("#btnSave").click(function (e) {
            $("#frmSave").submit();
        });

        $("#btnClose").click(function (e) {
            $('#EditModal').modal('hide');
        });



        $("#MsgType").change(function (e) {
            if ($(this).val() == "text") {
                $("#divnews").hide();
                $("#divtext").show();
            }
            else {
                $("#divnews").show();
                $("#divtext").hide();
            }
        });

        $("#replyType").change(function (e) {
            if ($(this).val() == "1") {
                $("#divkeys").show();
                $("#divtype").show();
            }
            else {
                $("#divkeys").hide();
                $("#divtype").hide();
            }
        });

        $('#frmSave').validator({
            rules: {
            },
            fields: {

            },
            valid: function (form) {
                FormSubmit(form, function (res) {
                    if (res.status == 0) {
                        _showInfoMessage("操作成功！", 'success');
                        $('#EditModal').modal('hide');
                        xjgrid.Reload();
                        //location = "Informations.do";
                    }
                    else {
                        _showInfoMessage("操作失败：" + res.message, 'error');
                        //showErrorTip("操作失败！：" + res.message, { left: 100, top: 10 }, true, 5000);
                    }
                })
            }
        });

        util.Delete = function (id) {
            $("#hdCurrentId").val(id);
            $('#confirmModal').modal('show');
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