; (function (window, undefined, $) {
    $(document).ready(function () {
        //初始化    //服务类别  保留
        //InintSerItem();
        function InintSerItem() {
            $.post(options.querySerItemUrl, { code: "STORE_SERVICETYPE",parentCode:0 },
                     function (res) {
                         //alert(res.status);
                         if (res.status >= 0) {
                             var items = eval("(" + res.data + ")");
                             $("#SERVICE_TYPE").select2({ width: "resolve", tags: items });
                         }
                         else {
                             $("#SERVICE_TYPE").select2({ width: "resolve", tags: "" });
                         }
                     },
                     "json"
                    );
        }


        $("#ptree").treeview({
            url: options.queryLeftUrl,
            showcheck: true,
            cascadecheck: true,
            onnodeclick: TreeNode_Click,
        });

        var cuTDs = $("#ptree").getTreeData();
        if (cuTDs != undefined && cuTDs[0] != undefined) {
            //默认加载第一个菜单数据
            $("#REGION_ID1").val(cuTDs[0].id); 
            $("#h4Dict").html(cuTDs[0].text);
            $("#REGION_ID_Str").html(cuTDs[0].text);
            $("#REGION_ID").val(cuTDs[0].id);
        }

        function TreeNode_Click(data) {
            $("#h4Dict").html(data.text);
            $("#REGION_ID").val(data.id);
            $("#REGION_ID1").val(data.id);
            $("#REGION_ID_Str").html(data.text); 
            SetRightDict();
        }
        

        var gridopt = {
            url: options.listUrl,
            colModel: [
                    { display: '门店编号', name: 'STORE_NO', width: "9%", sortable: true, hide: false, align: 'left', iskey: true },
                    { display: '所属上级', name: 'ORG_NAME', width: "20%", sortable: false, align: 'left' },
                    { display: '名称', name: 'NAME', width: "14%", sortable: false, align: 'left' },
                    { display: '门店类型', name: 'TYPE', width: "9%", sortable: false, align: 'left', process: formatTYPE },
                    { display: '管理者', name: 'MANAGER', width: "8%", sortable: false, align: 'left' },
                    { display: '电话', name: 'TELEPHONE', width: "10%", sortable: false, align: 'left' },
                    { display: '状态', name: 'STATUS', width: "10%", sortable: false, align: 'left', process: formatSTATUS },
                    { display: '操作', name: 'ID', width: "20%", sortable: false, align: 'center', process: processOp }
            ],
            sortname: "Idx",
            sortorder: "ASC",
            title: false,
            rp: 15,
            usepager: true,
            showcheckbox: false,
            autoload: false
        };
        var xjgrid = new xjGrid("gridlist", gridopt);

        function processOp(value, cell) {
            var ops = [];
            ops.push("&nbsp;<a title='修改门店信息' class='abtn' href='javascript:;'  onclick=\"util.Edit('", value, "')\"><i class='fa fa-edit' ></i>修改</a>");
            ops.push("&nbsp;&nbsp;<a title='删除门店信息' class='abtn' href='javascript:;'  onclick=\"util.Delete('", value, "','", cell[0], "')\"><i class='fa fa-trash-o' ></i>删除</a>");
            //ops.push("&nbsp;&nbsp;<a title='查看门店详细信息' class='abtn' href='#'><i class='fa fa-list' ></i>查看</a>");
            return ops.join("");
        }

        function formatTYPE(value, cell) {
            if (value == "0") {
                return "加盟店";
            }
            else if (value == "1") {
                return "直营店";
            }
            else if (value == "2") {
                return "合资店";
            }
           
        }
        function formatSTATUS(value, cell) {
            if (value == "1") {
                return "正常";
            }
            else if (value == "0") {
                return "关店";
            }
        }
      
        function SetRightDict() {
           
            xjgrid.Query($("#formQuery")[0]);
        }
        SetRightDict();
        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });


        //新增 编辑
        $("#btnAdd").click(function (e) {
            $("#ID").val(0);
            $('#frmSave')[0].reset();
            $('#EditModal').modal('show');
        });
        $('#CLOSE_DATE_Str').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd",
            //autoclose: true
        })
        //    .click('hide', function (ev) {
        //   $(".datepicker").css("z-index","9999");
        //});
           
        $('#OPEN_DATE_picker').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd"
        })
        //    .click('hide', function (ev) {
        //    $(".datepicker").css("z-index", "9999");
        //});
     
        //$('#CLOSE_DATE_Str').datepicker({
        //    language: "zh-CN",
        //    format: "yyyy-mm-dd"
        //});


        //只能输入字母或者数字
        $("#STORE_NO").keypress(function (event) {
            var eventObj = event || e;
            var keyCode = eventObj.keyCode || eventObj.which;
            if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))
                return true;
            else
                return false;
        }).focus(function () {
            this.style.imeMode = 'disabled';
        }).bind("paste", function () {
            var clipboard = window.clipboardData.getData("Text");
            if (/^(\d|[a-zA-Z])+$/.test(clipboard))
                return true;
            else
                return false;
        });

        util.Edit = function (id) {
            //$.post(options.editUrl, {
            //    id: id
            //},
            //      function (ret) {
            //          if (ret && ret.status == 0) {
            //              //$('#frmSave').Reload();
            //              $("#ID").val(ret.data.ID);
            //              $("#STORE_NO").val(ret.data.STORE_NO);
            //              $("#NAME").val(ret.data.NAME);
            //              $("#BRAND").val(ret.data.BRAND);
            //              $("#PROVINCE").val(ret.data.PROVINCE);
            //              $("#CITY").val(ret.data.CITY);
            //              $("#REGION_ID1").val(ret.data.REGION_ID);
            //              $("#REGION_ID").val(ret.data.REGION_ID);
            //              $("#ADDRESS").val(ret.data.ADDRESS);
            //              $("#MANAGER").val(ret.data.MANAGER);
            //              $("#TELEPHONE").val(ret.data.TELEPHONE);
            //              $("#TYPE").val(ret.data.TYPE);
            //              //alert(ret.data.SERVICE_TYPE);
            //              $("#SERVICE_TYPE").val(ret.data.SERVICE_TYPE);
            //              $("#STATUS").val(ret.data.STATUS);
            //              $("#SERVICE_FEE_RATE").val(ret.data.SERVICE_FEE_RATE);
            //              $("#OPEN_DATE_Str").val(ret.data.OPEN_DATE_Str);
            //              $("#CLOSE_DATE_Str").val(ret.data.CLOSE_DATE_Str);
            //              $("#ORG_NAME").val(ret.data.ORG_NAME);
            //          }
            //          else {
            //              _showInfoMessage("数据库中没有此门店，请刷新重试！", 'error');
            //              $("#ORG_NAME").val(ret.data.ORG_NAME);
            //          }
            //      },
            //      "json"
            //);
            //$('#EditModal').modal('show');

            window.location.href ="/Store/Edit.do?id="+id;
        };

        $("#btnSave").click(function (e) {
            //var check = "";
          
            //$('[type="checkbox"]').each(function () {
            //    if (true == $(this).prop("checked")) {
            //        check += $(this).val() + ",";               
            //    }
            //})
            //$("#SERVICE_TYPE").val(check);

            $("#frmSave").submit();
        });
        //alert($("#FEATURE0").text())
        function GetFeature() {
            var count = $("#count").val();
            var feature = "";
            for (var i = 0; i < count; i++) {
                var cname = "#CheckBox" + i;
                var sname = "#SERVICE_TYPE" + i;

                var c = $(cname).prop("checked");
                var s = $(sname).text();
                if (c == true) {
                    feature += s;
                }
            }
            $("#SERVICE_TYPE").val(feature);
            //alert($("#FEATURE").val());
        }
        $('#frmSave').validator({
            rules: {

            },
            fields: {
                '#NAME': 'required',
                '#STORE_NO': 'required',
                '#TYPE': 'required', 
                '#OPEN_DATE_Str': 'required',
                '#REGION_ID': 'required',
                
            },
            valid: function (form) {

                FormSubmit(form, function (res) {
                    if (res.status == 0) {
                        _showInfoMessage("操作成功！", 'success');
                        $('#EditModal').modal('hide');
                        SetRightDict();
                    }
                    else {
                        _showInfoMessage("操作失败：" + res.message, 'error');
                    }
                })
            }
        });

        util.Delete = function (id, name) {
            $("#lbuserName").html(name);
            $("#hdCurrentId").val(id);
            $('#confirmModal').modal('show');
        };

        $("#btnConfirm").click(function (e) {
            $('#confirmModal').modal('hide');
            var id = $("#hdCurrentId").val();
            $.post(options.deleteUrl, { id: id },
                  function (res) {
                      if (res.status == 0) {
                          _showInfoMessage("操作成功！", 'success');
                          SetRightDict();
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