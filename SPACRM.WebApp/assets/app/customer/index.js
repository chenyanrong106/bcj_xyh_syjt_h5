; (function (window, undefined, $) {

    $(document).ready(function () {
        var storeID = $('#STORE').val(),
            type = $('#TYPE').val();

        var gridopt = {
            url: options.listUrl + "?searchid=" + (getQueryString("searchid") || -1),
            colModel: [
                    { display: '编号', name: 'ID', width: "5%", sortable: true, hide: false, align: 'left', iskey: true },
                    { display: '姓名', name: 'NAME', width: "8%", sortable: false, align: 'left', process: Detail },
                    { display: '手机号', name: 'MOBILE', width: "10%", sortable: false, align: 'left' },
                    { display: '性别', name: 'GENDER', width: "6%", sortable: false, align: 'left' },
                    { display: '会籍店', name: 'STORE_NAME', width: "15%", sortable: false, align: 'left' },
                    { display: '顾客类型', name: 'TYPE', width: "10%", sortable: false, align: 'left', process: formatType },
                    { display: '会员卡号', name: 'CARD_NO', width: "10%", sortable: false, align: 'left' },
                    //{ display: '密码', name: 'PASSWORD', width: "10%", sortable: false, align: 'left', hide: true },
                    { display: '卡级（余额）', name: 'CARD_NAME', width: "28%", sortable: false, align: 'left', process: processCard },
                    //{ display: '余额', name: 'BALANCE', width: "8%", sortable: false, align: 'right' },
                    { display: '操作', name: 'ID', width: "17%", sortable: false, align: 'center', process: processOp }
            ],
            sortname: "Id",
            sortorder: "ASC",
            title: false,
            rp: 15,
            usepager: true,
            showcheckbox: false,
            extparams: [{ name: "STORE", value: storeID }, { name: "TYPE", value: type }]
        };
        var xjgrid = new xjGrid("gridlist", gridopt);
        function processOp(value, cell) {
            var ops = [];
            if (options.hasRight_ModifyCardExpire == "True") {
                ops.push("&nbsp;<a title='修改用户信息' href='javascript:;'  onclick=\"util.Edit('", value, "')\"><i class='fa fa-edit' ></i>修改</a>");


            } return ops.join("");
        }
        function processCard(value, cell) {
            var ops = [];
            var json = eval('(' + value + ')');
            $(json).each(function (index, data) {
                ops.push("<span style='display:inline-block;'>" + this.CardName + "（￥<b>" + this.Balance + "</b>）</span>");
            });
            return ops.join("");
        }


        function Detail(value, cell) {
            var href = "";

            href = "View360.do" + "?cid=" + cell[0];

            return "<a href='" + href + "'>" + value + "</a>";

        }

        function formatGender(value, cell) {
            if (value == "1") {
                return "男";
            }
            else {
                return "女";
            }
        }
        function formatType(value, cell) {
            if (value == "1") {
                return "门店会员";
            }
            else if (value == "0") {
                return "体验顾客";
            }
            //else if (value == "2") {
            //    return "合作商户";
            //}
        }
        function formatDate(value, cell) {
            if (value) {
                return value.split(" ")[0]
            }
            else {
                return "-"
            }
        }
        $("#formQuery").submit(function () {
            xjgrid.Query(this);
            return false;
        });

        $("#btnAdd").click(function (e) {
            var url = options.editUrl + "?flag=1";
            location.href = url;
        });
        $("#btnConfirm").click(function (e) {
            $('#confirmModal').modal('hide');
            var id = $("#hdCurrentId").val();
            $.post(options.deleteUrl + "/" + id, { id: id },
                  function (res) {
                      if (res.status == 0) {
                          showLoadingMsg("操作成功", { right: 100, top: 10 }, true, 5000);
                          xjgrid.Reload();
                      }
                      else {
                          showErrorTip("操作失败！：" + res.message, { right: 100, top: 10 }, true, 5000);
                      }
                  },
                  "json"
            );
        })

        util.Edit = function (id) {
            var url = options.editUrl + "?id=" + id + "&flag=1";
            location.href = url;
        };
        util.Delete = function (id, name) {
            $("#lbuserName").html(name);
            $("#hdCurrentId").val(id);
            $('#confirmModal').modal('show');
        };

        util.View360 = function (id) {
            //TODO:
            //alert("跳转到客户360页面");
            location.href = "View360.do" + "?cid=" + id;
        };

        //根据姓名/电话检索
        //$("#CUST_NAME").select2({
        //    placeholder: "输入姓名/电话/卡号查询",
        //    minimumInputLength: 2,
        //    width: 'resolve',
        //    ajax: {
        //        url: options.queryCustomerUrl,
        //        dataType: 'json',
        //        quietMillis: 100,
        //        data: function (term, page) {
        //            return {
        //                q: term, //search term
        //                page_limit: 10, // page size
        //                page: page // page number
        //            };
        //        },
        //        results: function (data, page) {
        //            //var more = (page * 10) < data.total;
        //            var strJson = eval("(" + data.data + ")");
        //            return { results: strJson, more: false };
        //        }
        //    },
        //    formatResult: custFormatResult,
        //    formatSelection: custFormatSelection,
        //    dropdownCssClass: "bigdrop",
        //    formatNoMatches: function (m) {
        //        return "<span style='color:#7B7B7B;font-size:12px;' >未查询到相关信息</span>&nbsp;&nbsp;<a href='javascript:;' itemid=" + m + " id='newCust' onclick='javascript:util.showAddCust(this)' ><i class='fa fa-plus'></i> 新增</a>";

        //    },
        //    escapeMarkup: function (m) { return m; }
        //});

        util.showAddCust = function (obj) {
            var m = $(obj).attr("itemid");
            var url = options.editUrl + "?custname=" + m + "&flag=1";
            window.location.href = url;
        }

        function custFormatResult(obj) {
            var markup = "<table class='movie-result'><tr>";
            markup += "<td class='movie-info'><div class='movie-title'>" + obj.title + "</div>";
            if (obj.mobile !== undefined) {
                markup += "<div class='movie-synopsis'>" + obj.mobile + "</div>";
            }

            if (obj.card_no !== undefined) {
                markup += "<div class='movie-synopsis'>" + obj.card_no + "</div>";
            }
            markup += "</td></tr></table>"
            return markup;
        }


        //选择顾客
        function custFormatSelection(obj) {

            $("#CUST_NAME").val(encodeURI(obj.title));
            $("#CUST_PHONE").val(obj.mobile);
            $("#CARD_NO").val(obj.card_no);

            $("#formQuery").submit();

            return obj.title;
        }

        var cid = $("#hide_Cid").val();
        if (cid != null && cid != "") {
            $("#divNewCustomer").hide();
            $("#divCustomer").show();

            //查顾客信息
            $.post("/Customer/Edit.do", { id: cid },
                  function (res) {
                      if (res.status == 0) {
                          $("#newcustname").html($("#hideCUST_NAME").val());
                          $("#newcustmobile").html($("#hide_MOBILE").val());
                          $("#divNewCustomer").hide();
                          $("#divCustomer").show();
                      }
                      else {
                          return '操作失败';
                      }

                  },
                  "json"
                 );
        }
        else {
            $("#divCustomer").hide();
            $("#divNewCustomer").show();
        }


        $("#btnFaCard").click(function () {
            $("#CUST_ID").val("");
            $("#CARD_NO").val("");
            $("#PASSWORD").val("");
            var data = xjgrid.GetCheckedRowDatas(formatPostData);
            var count = data.toString().split(',').length;
            if (data == null || data == "") {
                _showInfoMessage("请您选择一个顾客进行发卡！", "info");
                return false;
            }
            if (count > 1) {
                _showInfoMessage("您只能选择一个顾客进行发卡！", "info");
                return false;
            }

            var dataArray = data.toString().split('_');
            var cardNo = dataArray[2];
            if (cardNo != "" && cardNo != null) {
                _showInfoMessage("该顾客已发过会员卡片，不能重复发放！", "info");
                return;
            }
            $("#CUST_ID").val(dataArray[0]);
            $("#spanCUST_NAME").html(dataArray[1]);
            $("#spanPHONE").html(dataArray[3]);
            $("#CARD_NO").val(dataArray[2]);
            $("#PASSWORD").val(dataArray[4]);

            $('#FaCardModal').modal('show');
        });

        $("#btnFaCardSave").click(function () {
            $('#frmFaCardSave').submit();
        });


        function formatPostData(cell) {
            return [cell[0], cell[1], cell[5], cell[6], cell[7]].join("_");
        }
        var submiting = false;
        $('#frmFaCardSave').validator({
            rules: {
            },
            fields: {
                '#CARD_NO': 'required'
            },
            valid: function (form) {
                if (submiting) {
                    return;
                }
                submiting = true;
                FormSubmit(form, function (res) {
                    if (res.status > 0) {
                        _showInfoMessage("操作成功！", "success");
                        xjgrid.Reload();
                        $('#FaCardModal').modal('hide');
                    }
                    else {
                        _showInfoMessage("操作失败！", "error");
                    }
                    submiting = false;
                })
            }
        });


        $("#STORE").change(function () {
            //var s = $("#STORE").val();
            //$("#STORE_ID").val(s);
            $("#formQuery").submit();
        })

        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return r[2]; return null;
        }


    });

})(window, undefined, jQuery);