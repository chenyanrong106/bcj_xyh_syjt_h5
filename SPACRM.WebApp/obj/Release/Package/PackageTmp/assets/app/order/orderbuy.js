﻿; (function (window, undefined, $) {
    $(document).ready(function () {

        //服务 
        var gridoptser = {
            url: options.servicelistUrl,
            colModel: [
                    { display: '编号', name: 'ID', sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '名称', name: 'NAME', sortable: false, align: 'left', title: true, process: formatName },
                    { display: '操作', name: 'ID', width: 0, sortable: false, hide: false, process: formatOprate },
                    { display: '价格', name: 'PRICE', sortable: false, align: 'right', process: formatRMB },
                    { display: '是否卡付', name: 'IS_CARDPAY', sortable: false, hide: true },
                    { display: '是否受折扣', name: 'IS_DISCOUNT', sortable: false, hide: true }

            ],
            sortname: "Id",
            sortorder: "ASC",
            title: false,
            rp: 10,
            usepager: true,
            showcheckbox: false,
            striped: false
        };
        var xjgridser = new xjGrid("serGridlist", gridoptser);
        //产品
        var gridoptgoods = {
            url: options.goodslistUrl,
            colModel: [
                    { display: '编号', name: 'ID', sortable: true, hide: true, align: 'left', iskey: true },
                    { display: '名称', name: 'NAME', sortable: false, align: 'left', title: true, process: formatName },
                    { display: '操作', name: 'ID', width: 0, sortable: false, hide: false, process: formatOprate },
                    { display: '价格', name: 'PRICE', sortable: false, align: 'right', process: formatRMB },
                    { display: '是否卡付', name: 'IS_CARDPAY', sortable: false, hide: true },
                    { display: '是否受折扣', name: 'IS_DISCOUNT', sortable: false, hide: true }
            ],
            sortname: "Id",
            sortorder: "ASC",
            title: false,
            rp: 10,
            usepager: true,
            showcheckbox: false,
            striped: false
        };
        var xjgridgoods = new xjGrid("goodsGridlist", gridoptgoods);
        //金额格式化xxxx,xxxx,.00
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
            return (((sign) ? '' : '-') + '<span class="badge"><b>￥</b>' + value + '</span>');//value + '.' + cents + '</span>');
        }
        //产品名称格式化
        function formatName(value, cell) {
            //if (value.length > 20) {
            //    value = value.substring(0, 14) + ".."

            //}

            return value;
        }
        //产品名称格式化
        function formatOprate(value, cell) {
            //价格 是否允许卡付 是否享受折扣
            var html = "<input type='hidden' name='is_cpay_dist' value='" + cell[3] + "," + cell[4] + "," + cell[5] + "' />";
            return html;
        }

        $('#chkFWF').prop('checked', (options.hasServiceFee == "1"))
            .click(function () {
                calcPrice(0);
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
                return "<span style='color:#7B7B7B;font-size:12px;' >未查询到相关信息</span>&nbsp;&nbsp;<a href='javascript:;' id='newCust' onclick='return showAddCustName();'><i class='fa fa-plus'></i> 新增</a>";
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
            if (cid != null && cid != "") {
                getCustomerInfo(cid, "");
            }

            //个人疗程卡信息
            //getTreatement(cid)

            return false;
        }
        //顾客信息
        function getCustomerInfo(cid, cname) {
            $("#spanCustName").html("");
            $("#spanCustMobile").html("");
            $("#spanCustCard").html("");
            $("#spanCustCardLevel").html("");
            $("#spanCustCardBalance").html("");
            //查顾客信息
            $.post(options.customerDetailUrl, { id: cid, name: cname },
                  function (data) {
                      if (data.status == 1) {
                          if (cid <= 0) {
                              $("#hide_Cid").val(data.data.ID);
                          }
                          else {
                              $("#hide_Cid").val(cid);
                          }
                          $("#spanCustName").html(data.data.NAME);
                          $("#spanCustMobile").html(data.data.MOBILE);
                          $("#spanCustCard").html(data.data.CARD_NO);
                          $("#spanCustCardLevel").html(data.data.CARD_NAME);
                          $("#spanCustCardBalance").html(data.data.BALANCE);
                          $("#divNewCustomer").hide();
                          $("#divCustomer").show();

                          getTreatement(cid);

                          //选择顾客 检索该顾客会员卡
                          //getCusCardInfo(cid);
                      }
                      else {
                          $("#divCustomer").hide();
                          $("#divNewCustomer").show();
                      }
                  },
                  "json"
                 );

        }

        //得到个人疗程卡信息
        function getTreatement(cid) {
            //查顾客疗程卡信息/Order/getTreamentCard.do 
            $.post(options.queryTreamentCard, { cid: cid },
                     function (data) {
                         $("#tTreatmentPro tbody tr").remove();
                         if (data.status > 0) {
                             var json = eval(data.data);
                             var html = [];

                             $(json).each(function (i) {
                                 if (this.FLAG == 0) {
                                     for (var j = 0; j < json.length; j++) {
                                         if (j == 0) {
                                             html.push('<tr id=' + this.ID + '><td><b>-' + this.SERVICE_NAME + '</b></td><td></td><td class="tdPice"></td><td></td></tr>');
                                         } else {
                                             if (json[j].CUST_CARD_ID == this.ID) {
                                                 html.push('<tr id=' + json[j].ID + '><td>&nbsp;&nbsp;　├' + json[j].SERVICE_NAME + '</td><td>' + json[j].UNIT_PRICE + '</td><td class="tdPice"><span class="badge">' + json[j].COUNT + ' 次</span></td><td> <input type="hidden" id="prod_get_type" name="prod_get_type" value=' + json[j].TYPE + ' /><input type="hidden" id="prod_service_id" name="prod_service_id" value="' + json[j].SERVICE_ID + '"  /></td></tr>');
                                             }
                                         }
                                     }
                                 }
                             });
                             $(html.join('')).appendTo("#tTreatmentPro tbody");
                             $("ul#ultab li:eq(3)").css("display", "block");
                             $("ul#ultab li:eq(0)").removeClass("active");
                             $("ul#ultab li:eq(3)").attr("class", "active");
                             $("#spro").removeClass("tab-pane active");
                             $("#spro").addClass("tab-pane");

                             //
                             $("#treatment").removeClass("tab-pane");
                             $("#treatment").addClass("tab-pane active");

                         }
                         else {
                             var html = "<tr><td>暂无疗程卡信息</td></tr>";
                             $(html).appendTo("#tTreatmentPro tbody");
                         }
                     },
                     "json"
                    );


        }
        //检索顾客会员卡，疗程卡信息
        function getCusCardInfo(cid) {
            //VIPCARD_TYPE
            //LCCARD_TYPE
            //POSTCARD_TYPE
            //储值卡
            //$.post("/Common/QueryVipCard.do", { cid: cid },
            //         function (data) {
            //             if (data.status == 1) {
            //                 $(data.data).each(function () {
            //                     $("#VIPCARD_TYPE").append("<option value=" + this.ID + ">" + this.CARD_NAME + "</option>");
            //                 });
            //             }

            //         },
            //         "json"
            //        );
            ////折扣卡
            //$.post("/Common/QueryTreatCard.do", { cid: cid },
            //        function (data) {
            //            if (data.status == 1) {
            //                $(data.data).each(function () {
            //                    $("#LCCARD_TYPE").append("<option value=" + this.ID + ">" + this.CARD_NAME + "</option>");
            //                });
            //            }

            //        },
            //        "json"
            //       );

        }
        //散客开单
        $("#btnOrderBill").click(function () {
            //$("#spanCustName").html("散客");
            //$("#divNewCustomer").hide();
            //$("#divCustomer").show();
            getCustomerInfo(0, "散客");//散客
        });

        //根据项目名称、价格检索
        $("#PROD_NAME_PRICE").select2({
            placeholder: "输入项目名、价格查询",
            minimumInputLength: 2,
            width: 'resolve',
            ajax: {
                url: options.queryGodSerCardUrl,
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
            formatResult: goodsFormatResult,
            formatSelection: goodsFormatSelection,
            dropdownCssClass: "bigdrop", //
            escapeMarkup: function (m) { return m; } // 
        });

        function goodsFormatResult(obj) {
            var markup = "<table class='movie-result'><tr>";
            markup += "<td class='movie-info'><div class='movie-title'>" + obj.title + "</div>";
            if (obj.price !== undefined) {
                markup += "<div class='movie-synopsis'>￥：" + obj.price + "</div>";
            }
            markup += "</td></tr></table>"
            return markup;
        }

        $("#filelist").click(function (e) {
            var id = $("#hide_Cid").val();
            var url = "/Customer/filesshow.do?cid=" + id;
            window.open(url, 'example04', 'height=500, width=1200, top=100, left=100, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
            //window.xjDailog.Open(url, {
            //    width: 520,
            //    height: 350,
            //    caption: '发卡',
            //    theme: "simple", //默认主题
            //    onclose: function (userstate) {
            //       // window.location.href = "/Customer/view360.do?cid=" + id;

            //    } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
            //});
        });

        $("#buycard").click(function (e) {
            var id = $("#hide_Cid").val();
            var url = "BuyCards2.do?id=" + id;
            // window.open(url, 'example04', 'height=500, width=1200, top=100, left=100, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
            window.xjDailog.Open(url, {
                width: 1550,
                height: 850,
                caption: '日结',
                theme: "simple", //默认主题
                onclose: function (userstate) {
                    //alert(userstate);
                    location.href = location.href;

                } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
            });
        });

        //选择项目添加到购物车
        function goodsFormatSelection(obj) {
            //添加项目
            var name = obj.title;
            var objx = obj.price + "," + obj.is_cardpay + ',' + obj.is_discount;
            getProAjax(obj.id, obj.table, name, objx, "");
            return obj.title;
        }

        //添加产品
        function _addProduct(tid, table) {
            $('#' + tid + '' + '_detail').delegate('tr', 'click', function () {
                var name = $(this).children("td").eq(0).children("div").attr("title");
                var obj = $(this).find("input[NAME$=is_cpay_dist]").val();
                getProAjax($(this).attr("id"), table, name, obj, "");
            });
        }
        //添加常用产品
        $("#tSpeedyPro tr").bind("click", function () {
            var name = $(this).children("td").eq(0).html();
            var obj = $(this).find("input[NAME$=is_cpay_dist]").val();
            getProAjax($(this).attr("id"), $(this).attr("itemid"), name, obj, "");
        });
        //添加疗程卡
        $("#tTreatmentPro").delegate("tr", "click", function () {
            var count = $(this).children("td").eq(2).text().split(' ')[0];
            if (count == "" || count == null) return false;
            if (count < 1) {
                _showInfoMessage('该疗程项目已用完！', 'error');
                return false;
            }
            var price = $(this).children("td").eq(1).text().split(' ')[0];
            var name = $(this).children("td").html().split('├')[1];
            var prod_get_type = $(this).children("td").eq(3).find("input[NAME$=prod_get_type]").val();
            var objx = price + "," + prod_get_type;
            var prod_service_id = $(this).children("td").eq(3).find("input[NAME$=prod_service_id]").val();
            getProAjax(prod_service_id, "2", name, objx, $(this).attr("id"));
            count = parseInt(count) - 1;
            var mess = '<span class="badge">' + count + ' 次</span>';
            $(this).children("td").eq(2).html(mess);


        });
        //添加服务项目
        _addProduct("serGridlist", "2");
        //添加产品项目
        _addProduct("goodsGridlist", "0");

        //客户信息*****************************//
        var cidx = $("#hide_Cid").val();
        var oidx = $("#hide_Oid").val();
        var bidx = $("#hide_Bid").val();
        if (cidx != "" && oidx != "") {
            //订单来
            //查顾客信息
            $.post(options.queryOrder, { cid: cidx, oid: oidx },
                  function (data) {
                      if (data.status > 0) {
                          $("#spanCustName").html(data.data.CUST_NAME);
                          $("#spanCustMobile").html(data.data.MOBILE);
                          $("#spanCustCard").html(data.data.CUST_NO);
                          $("#spanOrder_No").html(data.data.ORDER_NO);
                          $("#ROOM_ID").val(data.data.ROOM_ID);

                          $("#spanCustCard").html(data.data.CARD_NO);
                          $("#spanCustCardLevel").html(data.data.CARD_NAME);
                          $("#spanCustCardBalance").html(data.data.BALANCE);

                          $("#divNewCustomer").hide();
                          $("#divCustomer").show();

                          //个人疗程卡信息
                          //getTreatement(cidx);
                          //订单项目
                          getOrderDetail(oidx);
                      }
                      else {
                          $("#divCustomer").hide();
                          $("#divNewCustomer").show();
                      }
                  },
                  "json"
                 );

        } else if (cidx != "" && bidx != "") {
            //预约来
            getCustomerInfo(cidx, "");
            getBookingItem(cidx, bidx);

        } else if (cidx != "") {
            getCustomerInfo(cidx, "");
        }
        //预约项目带过来
        function getBookingItem(cidx, bid) {
            $.post(options.queryBookingService, { bid: bid },
                 function (data) {//oidx
                     if (data.status > 0) {
                         var strJson = eval(data.data);

                         //员工
                         //var employees = $("#hide_span").html();

                         $("#divProds").show();
                         $("#divPrice").show();
                         $("#divCart").hide();
                         $("#btnSave").show();
                         $("#btnCheckIn").show();
                         var mspriceSum = 0;
                         var zjpriceSum = 0;
                         for (var i = 0; i < strJson.length; i++) {
                             var PROD_ID = strJson[i].ID;
                             var PROD_TYPE = strJson[i].PROD_TYPE;
                             var PROD_NAME = strJson[i].NAME;
                             var PROD_PRICE = strJson[i].PRICE;
                             var PROD_AMT = strJson[i].PRICE;
                             var IS_CARDPAY = strJson[i].IS_CARDPAY;
                             var IS_DISCOUNT = strJson[i].IS_DISCOUNT;
                             var EMP_ID = strJson[i].EMP_ID;
                             if (EMP_ID == undefined || EMP_ID == null) { EMP_ID = ""; }
                             var EMP_NAME = strJson[i].EMPLOYEE_NAME;

                             var PROMOTION_ID = strJson[i].PROMOTION_ID;
                             var prod_get_type = strJson[i].PROD_GET_TYPE;
                             var cust_cdetail_id = "";
                             if (prod_get_type == undefined || prod_get_type == null) {
                                 prod_get_type = "";
                             }
                             var html = [];

                             html.push('<tr id=' + PROD_ID + ' class="odd gradeX" style="height:30px;">');
                             html.push('<td></td>');
                             html.push('<td>' + PROD_NAME + '</td>');
                             html.push('<td>');
                             //employees = $("#hide_span").html();
                             //html.push('<select id="selectEmployee_' + i + '" name="Employee" style="width:80px;">');
                             //html.push(employees);
                             //html.push('</select>');
                             var empdivid = "bookEmployee_" + i;
                             html.push('<input id="' + empdivid + '" onclick="openwin(this.id)" readonly="readonly" value="' + EMP_NAME + '"  style="width:80px;"/>');

                             html.push('&nbsp;<input type="checkbox" name="cbxIS_DK" onchange="cbxIS_DKChange(this)" />点钟&nbsp;');
                             html.push('</td>');
                             html.push('<td style="text-align:left;">' + formatPrice(PROD_PRICE) + '</td>');//价格
                             html.push('<td style="text-align:left;">' + '<select style="width:120px;" id="selectPrivilege_' + PROD_ID + '_' + cidx + '_' + PROD_TYPE + '_' + i + '" name="selectPrivilege" onchange="privilege(this)"></select>' + '</td>');
                             html.push('<td style="text-align:center;">' + formatPrice(PROD_AMT) + '</td>');
                             html.push('<td class="center"> ');
                             html.push('<input type="hidden" name="proId" value="' + PROD_ID + '" />');         //项目ID
                             html.push('<input type="hidden" name="proType" value="' + PROD_TYPE + '" />');       //项目类型
                             html.push('<input type="hidden" name="proName" value="' + PROD_NAME + '" />');         //项目名称
                             html.push('<input type="hidden" name="proPrice" value="' + PROD_PRICE + '" />');//项目价格
                             html.push('<input type="hidden" name="proPrivilege" value="' + f.floatSubtract(parseFloat(PROD_PRICE), parseFloat(PROD_AMT)) + '" />');//优惠
                             html.push('<input type="hidden" name="payAmt" value="' + PROD_AMT + '" />');//应付金额
                             html.push('<input type="hidden" name="cardPay" value="' + IS_CARDPAY + '" />');//是否可卡付
                             html.push('<input type="hidden" name="discount" value="' + IS_DISCOUNT + '" />');//是否打折
                             html.push('<input type="hidden" name="PRODGETTYPE" value="' + prod_get_type + '" />');//疗程类型 1购买的疗程 2赠送的疗程 
                             //点钟
                             html.push('<input type="hidden" name="IS_DK" value="false" />');
                             html.push('<input type="hidden" name="Employee" value="' + EMP_ID + '" /> ');
                             html.push('<input type="hidden" name="BeginTime" value="" />');
                             html.push('<input type="hidden" name="EndTime" value="" />');
                             html.push('<input type="hidden" name="ADJUST_AMT" value="0" />');//调整金额
                             html.push('<input type="hidden" name="ADJUST_REASON" value="" />');//调整原因
                             if (cust_cdetail_id != undefined
                                 && cust_cdetail_id != null
                                 && cust_cdetail_id != "") {
                                 html.push('<input type="hidden" name="CUST_CDETAIL_ID" value="' + cust_cdetail_id + '" />');
                                 html.push('&nbsp;&nbsp;<a title="删除" onclick="delItem(this,0)" class="abtn" href="javascript:;" ><i class="fa fa-times" ></i></a>');
                             } else {
                                 html.push('<input type="hidden" name="CUST_CDETAIL_ID" value="" />');
                                 html.push('&nbsp;&nbsp;<a title="删除" onclick="delItem(this,' + PROD_PRICE + ')" class="abtn" href="javascript:;" ><i class="fa fa-times" ></i></a>');
                             }
                             html.push('</td>');
                             html.push('</tr>');

                             $(html.join("")).appendTo("#tableProds");

                             getPROMOTION(PROD_ID, cidx, PROD_TYPE, i, PROMOTION_ID, PROD_PRICE);

                             //累计预约的价格
                             mspriceSum = f.floatAdd(parseFloat(mspriceSum), parseFloat(PROD_PRICE));
                             zjpriceSum = f.floatAdd(parseFloat(zjpriceSum), parseFloat(PROD_AMT));
                         }

                         //计算预约的总价格
                         //msprice = f.floatAdd(parseFloat(msprice), parseFloat(PROD_PRICE));
                         //zjprice = f.floatAdd(parseFloat(zjprice), parseFloat(PROD_AMT));
                         $("#spanMSZJ").html(mspriceSum);
                         $("#spanYH").html(f.floatSubtract(parseFloat(mspriceSum), parseFloat(zjpriceSum)));
                         //$("#spanZJ").html(zjpriceSum);
                         $("#ZJ2").val(zjpriceSum);
                         calcPrice(0);

                         //计算 卡付 现金 
                         var cardPayPrice = 0, cardDistPayPrice = 0, cashPay = 0;
                         $("#tableProds tbody tr").each(function () {
                             var cardPay = $(this).children("td").eq(6).find("input[NAME$=cardPay]").val().toUpperCase();
                             var discount = $(this).children("td").eq(6).find("input[NAME$=discount]").val().toUpperCase();
                             var price = $(this).find("input[NAME$=payAmt]").val();//瀹為檯鏀ˉ浠樹环鏍?

                             //卡付 不允许打折
                             if (cardPay == "TRUE" && discount == "FALSE") {
                                 cardPayPrice = f.floatAdd(parseFloat(cardPayPrice), parseFloat(price));
                             }
                                 //卡付允许打折
                             else if (cardPay == "TRUE" && discount == "TRUE") {
                                 cardDistPayPrice = f.floatAdd(parseFloat(cardDistPayPrice), parseFloat(price));
                             }
                             else {
                                 //现金支付
                                 cashPay = f.floatAdd(parseFloat(cashPay), parseFloat(price));
                                 // alert(cashPay);
                             }

                         });

                         //允许卡付
                         $("#spanCardPay").html(cardPayPrice);
                         //允许卡付打折
                         $("#spanCardDistPay").html(cardDistPayPrice);
                         //现金
                         //cashPay = f.floatSubtract(parseFloat($("#spanMSZJ").html()), parseFloat(f.floatAdd(cardPayPrice, cardDistPayPrice)));
                         $("#spanCashPay").html(cashPay);

                     }
                 });
        }
        //订单来信息订单项目ID***************************//
        function getOrderDetail(oidx) {
            //显示会员疗程
            //对应疗程删除数据加回
            var cidx = $("#hide_Cid").val();
            if (cidx == "" || cidx == null) {
                cidx = 0;
            } else {
                getTreatement(cidx);
            }

            $.post(options.queryGodSer, { oid: oidx },
                  function (data) {//oidx订单id
                      if (data.status > 0) {
                          var strJson = eval(data.data);
                          //添加项目详细信息
                          //var employees = $("#hide_span").html();
                          //项目/产品ID,表,产品名称,obj{价格/是否卡付/是否打折}

                          $("#divProds").show();
                          $("#divPrice").show();
                          $("#divCart").hide();
                          $("#btnSave").show();
                          $("#btnCheckIn").show();
                          var mspriceSum = 0;
                          var zjpriceSum = 0;
                          for (var i = 0; i < strJson.length; i++) {
                              //取得该顾客和产品享有优惠信息
                              var PROD_ID = strJson[i].PROD_ID;
                              var PROD_TYPE = strJson[i].PROD_TYPE;
                              var PROD_NAME = strJson[i].PROD_NAME;
                              var PROD_PRICE = strJson[i].PROD_PRICE;
                              var PROD_AMT = strJson[i].PROD_AMT;
                              var IS_CARDPAY = strJson[i].IS_CARDPAY;
                              var IS_DISCOUNT = strJson[i].IS_DISCOUNT;
                              var cust_cdetail_id = strJson[i].CDETAIL_ID;
                              var EMP_ID = strJson[i].EMP_ID;
                              if (EMP_ID == undefined || EMP_ID == null) {
                                  EMP_ID = "";
                              }
                              var EMP_NAME = strJson[i].EMPLOYEE_NAME;
                              var PROMOTION_ID = strJson[i].PROMOTION_ID;
                              var prod_get_type = strJson[i].PROD_GET_TYPE;
                              if (prod_get_type == undefined || prod_get_type == null) {
                                  prod_get_type = "";
                              }
                              var is_dk = strJson[i].IS_DK;
                              var checked = "";
                              var ckvalue = false;
                              if (is_dk.toString().toUpperCase() == "TRUE") {
                                  checked = " checked='checked' ";
                                  ckvalue = true;
                              }

                              var html = [];

                              html.push('<tr id=' + PROD_ID + ' class="odd gradeX" style="height:30px;">');
                              html.push('<td></td>');
                              html.push('<td>' + PROD_NAME + '</td>');
                              html.push('<td>');

                              var empdivid = "orderEmployee_" + i;
                              html.push('<input id="' + empdivid + '" onclick="openwin(this.id)" readonly="readonly" value="' + EMP_NAME + '"  style="width:80px;"/>');
                              html.push('<span style="display:;">&nbsp;<input type="checkbox" name="cbxIS_DK" "' + checked + '" onchange="cbxIS_DKChange(this)" />点钟&nbsp;</span>');
                              html.push('</td>');


                              html.push('<td style="text-align:left;">' + formatPrice(PROD_PRICE) + '</td>');//价格
                              html.push('<td style="text-align:left;">' + '<select style="width:120px;" id="selectPrivilege_' + PROD_ID + '_' + cidx + '_' + PROD_TYPE + '_' + i + '" name="selectPrivilege" onchange="privilege(this)"></select>' + '</td>');
                              html.push('<td style="text-align:center;">' + formatPrice(PROD_AMT) + '</td>');
                              html.push('<td class="center"> ');
                              html.push('<input type="hidden" name="proId" value="' + PROD_ID + '" />');         //项目ID
                              html.push('<input type="hidden" name="proType" value="' + PROD_TYPE + '" />');       //项目来源
                              html.push('<input type="hidden" name="proName" value="' + PROD_NAME + '" />');         //项目名称
                              html.push('<input type="hidden" name="proPrice" value="' + PROD_PRICE + '" />');//项目单价
                              html.push('<input type="hidden" name="proPrivilege" value="' + f.floatSubtract(parseFloat(PROD_PRICE), parseFloat(PROD_AMT)) + '" />');//优惠金额
                              html.push('<input type="hidden" name="payAmt" value="' + PROD_AMT + '" />');//应付金额
                              html.push('<input type="hidden" name="cardPay" value="' + IS_CARDPAY + '" />');//是否可卡付 
                              html.push('<input type="hidden" name="discount" value="' + IS_DISCOUNT + '" />');//是否享折扣 
                              html.push('<input type="hidden" name="PRODGETTYPE" value="' + prod_get_type + '"/>');//疗程类型 1购买的疗程 2赠送的疗程 
                              //点钟
                              html.push('<input type="hidden" name="IS_DK" value="' + ckvalue + '" />');
                              html.push('<input type="hidden" name="Employee" value="' + EMP_ID + '" /> ');
                              html.push('<input type="hidden" name="BeginTime" value="" />');
                              html.push('<input type="hidden" name="EndTime" value="" />');
                              html.push('<input type="hidden" name="ADJUST_AMT" value="0" />');//调整金额
                              html.push('<input type="hidden" name="ADJUST_REASON" value="" />');//调整原因
                              if (cust_cdetail_id != undefined && cust_cdetail_id != null && cust_cdetail_id != "") {
                                  html.push('<input type="hidden" name="CUST_CDETAIL_ID" value="' + cust_cdetail_id + '" />');
                              } else { html.push('<input type="hidden" name="CUST_CDETAIL_ID" value="" />'); }
                              html.push('&nbsp;&nbsp;<a title="删除" onclick="delItem(this,' + PROD_PRICE + ')" class="abtn" href="javascript:;" ><i class="fa fa-times" ></i></a>');

                              html.push('</td>');
                              html.push('</tr>');

                              $(html.join("")).appendTo("#tableProds");

                              getPROMOTION(PROD_ID, cidx, PROD_TYPE, i, PROMOTION_ID, PROD_PRICE);

                              //累计价格
                              mspriceSum = f.floatAdd(parseFloat(mspriceSum), parseFloat(PROD_PRICE));
                              zjpriceSum = f.floatAdd(parseFloat(zjpriceSum), parseFloat(PROD_AMT));
                          }
                          // 计算价格
                          //var msprice = parseFloat($("#spanMSZJ").html());
                          //var yhprice = parseFloat($("#spanYH").html());
                          //var zjprice = parseFloat($("#spanZJ").html());
                          //var zjprice = parseFloat($("#ZJ2").val());
                          ///msprice = f.floatAdd(parseFloat(msprice), parseFloat(PROD_PRICE));
                          //zjprice = f.floatAdd(parseFloat(zjprice), parseFloat(PROD_AMT));
                          $("#spanMSZJ").html(mspriceSum);
                          $("#spanYH").html(f.floatSubtract(parseFloat(mspriceSum), parseFloat(zjpriceSum)));
                          //$("#spanZJ").html(zjprice);
                          $("#ZJ2").val(zjpriceSum);
                          calcPrice(0);

                          //计算组合价格
                          //允许卡支付但不享受折扣金额  允许卡支付并享受折扣金额 现金支付
                          var cardPayPrice = 0, cardDistPayPrice = 0, cashPay = 0;
                          $("#tableProds tbody tr").each(function () {
                              var cardPay = $(this).children("td").eq(6).find("input[NAME$=cardPay]").val().toUpperCase();
                              var discount = $(this).children("td").eq(6).find("input[NAME$=discount]").val().toUpperCase();
                              var price = $(this).find("input[NAME$=payAmt]").val();//实际支付价格

                              //允许卡支付但不享受折扣金额：
                              if (cardPay == "TRUE" && discount == "FALSE") {
                                  cardPayPrice = f.floatAdd(parseFloat(cardPayPrice), parseFloat(price));
                              }
                                  //允许卡支付并享受折扣金额
                              else if (cardPay == "TRUE" && discount == "TRUE") {
                                  cardDistPayPrice = f.floatAdd(parseFloat(cardDistPayPrice), parseFloat(price));
                              }
                              else {
                                  //现金支付
                                  cashPay = f.floatAdd(parseFloat(cashPay), parseFloat(price));
                                  // alert(cashPay);
                              }

                          });

                          //允许卡支付但不享受折扣金额
                          $("#spanCardPay").html(cardPayPrice);
                          //允许卡支付并享受折扣金额
                          $("#spanCardDistPay").html(cardDistPayPrice);
                          //剩下现金支付
                          //现金支付 
                          //cashPay = f.floatSubtract(parseFloat($("#spanMSZJ").html()), parseFloat(f.floatAdd(cardPayPrice, cardDistPayPrice)));
                          $("#spanCashPay").html(cashPay);

                      }
                  });

        }
        //预约来项目
        //优惠活动int CUST_ID, int PROD_ID, decimal PROD_PRICE, int PROD_TYPE
        function getPROMOTION(PROD_ID, cidx, PROD_TYPE, proi, PROMOTION_ID, PROD_PRICE) {

            $.getJSON(options.queryPromotionUrl + "?t=" + new Date().getMilliseconds(), {
                CUST_ID: cidx,
                PROD_ID: PROD_ID,
                PROD_PRICE: PROD_PRICE,
                PROD_TYPE: PROD_TYPE
            },
            function (res) {
                if (res.status > 0) {
                    var json = eval(res.data);
                    $("#selectPrivilege_" + PROD_ID + "_" + cidx + "_" + PROD_TYPE + "_" + proi).append("<option value='0'>请选择优惠/活动</option>");
                    $(json).each(function () {
                        $("#selectPrivilege_" + PROD_ID + "_" + cidx + "_" + PROD_TYPE + "_" + proi).append("<option value='" + this.ID + "'>" + this.NAME + " ￥" + this.PROM_PRICE + "</option>");
                    });
                    $("#selectPrivilege_" + PROD_ID + "_" + cidx + "_" + PROD_TYPE + "_" + proi).val(PROMOTION_ID);

                }
                else {
                    //无优惠
                    $("#selectPrivilege_" + PROD_ID + "_" + cidx + "_" + PROD_TYPE + "_" + proi).append("<option value='0'>暂无优惠/活动</option>");
                }
            });
        }

        //选择项目类别
        util.checkSerMenu = function (obj) {

            var unSelect = $(obj).parent("li").parent("ul").children("li");
            $(unSelect).each(function () {
                $(this).removeClass("serSelected");
            });
            $(obj).parent("li").addClass("serSelected");

            var gridoptser = {
                url: options.servicelistUrl + "?TYPE=" + $(obj).attr("itemid"),
                colModel: [
                        { display: '编号', name: 'ID', sortable: true, hide: true, align: 'left', iskey: true },
                        { display: '名称', name: 'NAME', sortable: false, align: 'left', title: true, process: formatName },
                        { display: '操作', name: 'ID', width: 0, sortable: false, hide: false, process: formatOprate },
                        { display: '价格', name: 'PRICE', sortable: false, align: 'right', process: formatRMB },
                        { display: '是否卡付', name: 'IS_CARDPAY', sortable: false, hide: true },
                        { display: '是否受折扣', name: 'IS_DISCOUNT', sortable: false, hide: true }

                ],
                sortname: "Id",
                sortorder: "ASC",
                title: false,
                rp: 10,
                usepager: true,
                showcheckbox: false,
                striped: false
            };
            var xjgridser = new xjGrid("serGridlist", gridoptser);
            //添加服务项目
            _addProduct("serGridlist", "2");
        }

        //选择产品类别
        util.checkProMenu = function (obj) {

            var unSelect = $(obj).parent("li").parent("ul").children("li");
            $(unSelect).each(function () {
                $(this).removeClass("serSelected");
            });
            $(obj).parent("li").addClass("serSelected");

            //产品
            var gridoptgoods = {
                url: options.goodslistUrl + "?TYPE=" + $(obj).attr("itemid"),
                colModel: [
                        { display: '编号', name: 'ID', sortable: true, hide: true, align: 'left', iskey: true },
                        { display: '名称', name: 'NAME', sortable: false, align: 'left', title: true, process: formatName },
                        { display: '操作', name: 'ID', width: 0, sortable: false, hide: false, process: formatOprate },
                        { display: '价格', name: 'PRICE', sortable: false, align: 'right', process: formatRMB },
                        { display: '是否卡付', name: 'IS_CARDPAY', sortable: false, hide: true },
                        { display: '是否受折扣', name: 'IS_DISCOUNT', sortable: false, hide: true }
                ],
                sortname: "Id",
                sortorder: "ASC",
                title: false,
                rp: 10,
                usepager: true,
                showcheckbox: false,
                striped: false
            };
            var xjgridgoods = new xjGrid("goodsGridlist", gridoptgoods);
            //添加产品
            _addProduct("goodsGridlist", "0");
        }

        //修改姓名
        util.updateCust = function () {
            $("#divCustomer").hide();
            $("#divNewCustomer").show();
        }
    });

    //添加项目详细信息
    var proi = 1;
    var employees = [];
    function getProAjax(pid, table, name, obj, cust_cdetail_id) {
        //console.log(pid + table + name + obj + cust_cdetail_id);
        //项目/产品ID,表,产品名称,obj{价格/是否卡付/是否打折}
        var cid = $("#hide_Cid").val();

        if (cid == "" || cid == null) {
            $("#btnOrderBill").click();
            cid = $("#hide_Cid").val();
            return false;
        }
        $("#divProds").show();
        $("#divPrice").show();
        $("#divCart").hide();
        $("#btnSave").show();
        $("#btnCheckIn").show();
        //var employees = $("#hide_span").html();
        var html = [];
        html.push('<tr id=' + pid + ' class="odd gradeX" style="height:30px;">');
        html.push('<td></td>');
        html.push('<td>' + name + '</td>');
        html.push('<td>');
        //html.push('<select name="Employee" style="width:80px;">');
        //html.push(employees);
        //html.push('</select>');
        var empdivid = "Employee_" + proi;
        html.push('<input id="' + empdivid + '" onclick="openwin(this.id)" readonly="readonly"  style="width:80px;">');
        html.push('&nbsp;<input type="checkbox" name="cbxIS_DK" onchange="cbxIS_DKChange(this)" />点钟&nbsp;');
        html.push('</td>');
        if (cust_cdetail_id != undefined
            && cust_cdetail_id != null
            && cust_cdetail_id != "") {
            //疗程卡
            var objcell = obj.split(',');
            html.push('<td style="text-align:left;">' + formatPrice(objcell[0]) + '</td>');//价格
            html.push('<td style="text-align:left;">' + '<select style="width:120px;" id="selectPrivilege_' + pid + '_' + cid + '_' + table + '_' + proi + '"  name="selectPrivilege" class="selectPrivilege"  onchange="privilege(this)"></select>' + '</td>');
            html.push('<td style="text-align:center;">' + formatPrice(objcell[0]) + '</td>');
            html.push('<td class="center"> ');
            html.push('<input type="hidden" name="proId" value=' + pid + ' />');         //项目ID
            html.push('<input type="hidden" name="proType" value=' + table + ' />');       //项目来源
            html.push('<input type="hidden" name="proName" value="' + name + '" />');         //项目名称
            html.push('<input type="hidden" name="proPrice" value=' + objcell[0] + '  />');//项目单价
            html.push('<input type="hidden" name="proPrivilege" value=0 />');//优惠金额
            html.push('<input type="hidden" name="payAmt" value=' + objcell[0] + ' />');//应付金额
            html.push('<input type="hidden" name="cardPay" />');//是否可卡付 
            html.push('<input type="hidden" name="discount" />');//是否享折扣 
            html.push('<input type="hidden" name="PRODGETTYPE" value=' + objcell[1] + '/>');//疗程类型 1购买的疗程 2赠送的疗程 
            //点钟
            html.push('<input type="hidden" name="IS_DK" value="false" />');
            html.push('<input type="hidden" name="Employee" /> ');
            html.push('<input type="hidden" name="ADJUST_AMT" value="0" />');//调整金额
            html.push('<input type="hidden" name="ADJUST_REASON" value="" />');//调整原因
            html.push('<input type="hidden" name="BeginTime" value="" />');
            html.push('<input type="hidden" name="EndTime" value="" />');
            html.push('<input type="hidden" name="CUST_CDETAIL_ID" value="' + cust_cdetail_id + '" />');
            html.push('&nbsp;&nbsp;<a title="删除" onclick="delItem(this,' + objcell[0] + ')" class="abtn" href="javascript:;" ><i class="fa fa-times" ></i></a>');
            html.push('</td>');
        }
        else {

            var objcell = obj.split(',');
            html.push('<td style="text-align:left;">' + formatPrice(objcell[0]) + '</td>');//价格
            html.push('<td style="text-align:left;">');

            html.push('<select style="width:120px;" id="selectPrivilege_' + pid + '_' + cid + '_' + table + '_' + proi + '" name="selectPrivilege" onchange="privilege(this)"></select>');
            //+ '<select style="width:120px;" id="selectPrivilege_' + pid + '_' + cid + '_' + table + '_' + proi + '" name="selectPrivilege" onchange="privilege(this)"></select>'

            html.push('</td>');
            html.push('<td style="text-align:center;">' + formatPrice(objcell[0]) + '</td>');
            html.push('<td class="center"> ');
            html.push('<input type="hidden" name="proId" value="' + pid + '" />');         //项目ID
            html.push('<input type="hidden" name="proType" value="' + table + '" />');       //项目来源
            html.push('<input type="hidden" name="proName" value="' + name + '" />');         //项目名称
            html.push('<input type="hidden" name="proPrice" value="' + objcell[0] + '" />');//项目单价
            html.push('<input type="hidden" name="proPrivilege" value=0 />');//优惠金额
            html.push('<input type="hidden" name="payAmt" value="' + objcell[0] + '" />');//应付金额
            html.push('<input type="hidden" name="cardPay" value="' + objcell[1] + '" />');//是否可卡付 
            html.push('<input type="hidden" name="discount" value="' + objcell[2] + '" />');//是否享折扣 
            html.push('<input type="hidden" name="PRODGETTYPE" value="' + objcell[1] + '" />');//疗程类型 1购买的疗程 2赠送的疗程 
            html.push('<input type="hidden" name="Employee" /> ');
            html.push('<input type="hidden" name="ADJUST_AMT" value="0" />');//调整金额
            html.push('<input type="hidden" name="ADJUST_REASON" value="" />');//调整原因
            html.push('<input type="hidden" name="BeginTime" value="" />');
            html.push('<input type="hidden" name="EndTime" value="" />');
            //点钟
            html.push('<input type="hidden" name="IS_DK" value="false" />');
            html.push('<input type="hidden" name="CUST_CDETAIL_ID" value="" />');
            html.push('&nbsp;&nbsp;<a title="删除" onclick="delItem(this,' + objcell[0] + ')" class="abtn" href="javascript:;" ><i class="fa fa-times" ></i></a>');
            html.push('</td>');
        }

        var img = $("#progressImgage");
        var mask = $("#maskOfProgressImage");
        var url = options.queryPromotionUrl + "?t=" + new Date().getMilliseconds();

        $.ajax({
            url: url,
            type: "post",
            dataType: "json",
            data: {
                CUST_ID: cid,
                PROD_ID: pid,
                PROD_PRICE: objcell[0],
                PROD_TYPE: table
            },
            beforeSend: function (xhr) {
                img.show().css({
                    //"position": "fixed",
                    //"top": "50%",
                    //"left": "50%",
                    //"margin-top": function () { return -1 * img.height() / 2; },
                    //"margin-left": function () { return -1 * img.width() / 2; }
                });
                mask.show().css("opacity", "0.1");
                $("#divloading").css("opacity", "0.3");
            },
            success: function (rets) {
                if (rets.status > 0) {
                    var json = eval(rets.data);
                    $("#selectPrivilege_" + pid + "_" + cid + "_" + table + "_" + proi).append("<option value='0'>请选择优惠/活动</option>");
                    $(json).each(function () {
                        $("#selectPrivilege_" + pid + "_" + cid + "_" + table + "_" + proi).append("<option value='" + this.ID + "'>" + this.NAME + " ￥" + this.PROM_PRICE + "</option>");
                    });
                }
                else {
                    //无优惠
                    $("#selectPrivilege_" + pid + "_" + cid + "_" + table + "_" + proi).append("<option value='0'>暂无优惠/活动</option>");
                }
                proi++;


                //计算价格
                var msprice = parseFloat($("#spanMSZJ").html());
                var yhprice = parseFloat($("#spanYH").html());
                //var zjprice = parseFloat($("#spanZJ").html());
                var zjprice = parseFloat($("#ZJ2").val());
                msprice = f.floatAdd(parseFloat(msprice), parseFloat(objcell[0]));
                zjprice = f.floatAdd(parseFloat(zjprice), parseFloat(objcell[0]));
                $("#spanMSZJ").html(msprice);
                $("#spanYH").html(yhprice);
                //$("#spanZJ").html(zjprice);
                $("#ZJ2").val(zjprice);
                calcPrice(zjprice);
                //剩余金额=应付金额-已付金额
                var payprice = 0;
                $("#tablePayList tbody tr").each(function () {
                    payprice = f.floatAdd(parseFloat(payprice), parseFloat($(this).find("input[NAME$=payMoney]").val()));
                });
                //应付总价
                //$("#spanResAmount").html(f.floatSubtract(parseFloat($("#spanZJ").html()), parseFloat(payprice)));


                //计算组合价格
                //允许卡支付但不享受折扣金额  允许卡支付并享受折扣金额 现金支付
                var cardPayPrice = 0, cardDistPayPrice = 0, cashPay = 0;
                $("#tableProds tbody tr").each(function () {
                    var cardPay = $(this).children("td").eq(6).find("input[NAME$=cardPay]").val().toUpperCase();
                    var discount = $(this).children("td").eq(6).find("input[NAME$=discount]").val().toUpperCase();
                    var price = $(this).find("input[NAME$=payAmt]").val();//实际支付价格

                    //允许卡支付但不享受折扣金额：
                    if (cardPay == "TRUE" && discount == "FALSE") {
                        cardPayPrice = f.floatAdd(parseFloat(cardPayPrice), parseFloat(price));
                    }
                        //允许卡支付并享受折扣金额
                    else if (cardPay == "TRUE" && discount == "TRUE") {

                        cardDistPayPrice = f.floatAdd(parseFloat(cardDistPayPrice), parseFloat(price));
                    }
                    else {
                        //现金支付
                        cashPay = f.floatAdd(parseFloat(cashPay), parseFloat(price));
                    }
                });

                //允许卡支付但不享受折扣金额
                $("#spanCardPay").html(cardPayPrice);
                //允许卡支付并享受折扣金额
                $("#spanCardDistPay").html(cardDistPayPrice);
                //剩下现金支付
                $("#spanCashPay").html(cashPay);
                //_showInfoMessage('项目[' + name + ']添加成功！', 'success');

                _showInfoMessage('项目[' + name + ']添加成功！', 'success');

            },
            complete: function (xhr) {
                img.hide();
                mask.hide();
                $("#divloading").css("opacity", "1");

            },
            error: function () {
                //无优惠
                $("#selectPrivilege_" + pid + "_" + cid + "_" + table + "_" + proi).append("<option value='0'>暂无优惠/活动</option>");
                return false;
            }
        });

        html.push('</tr>');
        $(html.join("")).appendTo("#tableProds");
        $("#selectPrivilege_" + pid + "_" + cid + "_" + table + "_" + proi).val();


    }
    //弹出层关闭按钮
    $("#closewin").click(function () {
        $("#windo").hide();
    });
    //模糊检索员工
    $("#EMP_NAME").autocomplete({
        source: function (request, response) {
            $.getJSON(options.queryEmployeeBy, { q: request.term }, function (res) {
                if (res != null && res.status > 0) {
                    var str = eval("(" + res.data + ")");
                    response(str);
                }
            });
        },
        select: function (e, ui) {
            if (ui.item.employee_no != "") {
                $("#divcontent").hide();
                $("#divcontentone").show();
                //$("#ulcontentone").html("");
                //var li = '<li onclick="addEmp(this)" id="' + ui.item.value + '" itemid="' + ui.item.id + '"> <div class="contentemp"><div>' + ui.item.employee_no + '</div><div>' + ui.item.value + '</div></div></li>'
                var mydate = new Date();
                var hours = mydate.getHours();
                var minutes = mydate.getMinutes();
                var times = hours + ":" + minutes;
                var li = '<li id="' + ui.item.value + '" itemid="' + ui.item.id + '">' +
                                '<div class="emp_name">' +
                                ui.item.value + '(' + $.trim(ui.item.employee_no) + ')' +
                                '</div>' +
                                '<div class="emp_time">' +
                                    '<input class="text" id="BEGINTIME' + ui.item.id + '" name="BEGINTIME' + ui.item.id + '" type="text" class="text" value="' + times + '" onclick="selectTime(this);" >' +
                                    '<span class="input-group-addon">到</span>' +
                                    '<input class="text" id="ENDTIME' + ui.item.id + '" name="ENDTIME' + ui.item.id + '" type="text" class="text" value="' + times + '" onclick="selectTime(this);">' +
                                '</div>' +
                                '<div class="emp_sure">' +
                                    '<input type="button" class="btn btn-primary button_sure" onclick="addEmp(this, ' + ui.item.id + ')" value="选择">' +
                                '</div>' +
                            '</li>';
                $("#ulcontentone").html(li);
            }
        }
    });

    //新增顾客//
    $('#firstname').editable({
        validate: function (value) {
            var _mobile = $.trim($("#mobile").val());
            if ($.trim($("#custname").val()) == '') {
                return '姓名不能为空';
            }
            else if (_mobile == '') {
                return '电话不能为空';
            }
            else if (_mobile.length != 11) {
                return '电话必须为11位';
            }
            else {

                $.post("/Customer/Save.do", { NAME: $.trim($("#custname").val()), MOBILE: $.trim($("#mobile").val()), CUST_NO: 0 },
                   function (data) {
                       if (data.status == 0) {
                           //新增成功
                           window.location.href = window.location.href + "?cid=" + data.data;
                       }
                       else {
                           _showInfoMessage('操作失败！', 'info');
                           return false;
                       }

                   },
                   "json"
                  );
            }
        }
    });


    //跳转列表页面
    function returnList() {
        window.location.href = options.returnListUrl;
    }

    var submiting = false;
    $('#formSave').validator({
        valid: function (form) {
            if (submiting) {
                return;
            }
            submiting = true;
            FormSubmit(form, function (res) {
                if (res.status == 0) {
                    _showInfoMessage("操作成功！", "success");
                    var submitType = $("#hide_SubmitType").val();
                    if (submitType == "check") {
                        //保存订单返回列表 2秒跳转
                        //setInterval(returnList, 2000);
                        var coid = res.data.toString().split('_');
                        var tip = $("#spanCashPay").html() + "/" + $("#spanCardPay").html() + "/" + $("#spanCardDistPay").html();
                        window.location.href = "/Order/orderpay.do?cid=" + coid[0] + "&oid=" + coid[1] + "&tip=" + tip;
                        return false;
                    }
                    else if (submitType == "save") {
                        window.location.href = "/Order/OrderList.do";
                        return false;
                    }
                }
                else {
                    _showInfoMessage("操作失败！" + res.message, "error");
                }
                $("#hide_SubmitType").val("");
                submiting = false;
            })
        }
    });
    //提交订单
    function submit(mess) {
        if ($("#spanCustName").html() == "") {
            _showInfoMessage("请您先选择顾客！", "info");
            return false;
        }
        //先选择房间
        //if ($("#ROOM_ID").val() == "") {
        //    _showInfoMessage("请您先选择房间！", "info");
        //    return false;
        //}
        //先选择项产品/服务
        //行数
        var rowcount = $("#tableProds tbody").children("tr").length;
        if (rowcount < 1) {
            _showInfoMessage("请您先添加产品/服务！", "info");
            return false;
        }
        //if (!confirm(mess)) {
        //    return false;
        //}

        //门市总价
        $("#hide_MSZJ").val($("#spanMSZJ").text());
        //应付总价
        $("#hide_ZJ").val($("#spanZJ").text());
        //支付金额
        //var payedAmt = parseFloat($("#spanZJ").text()) - parseFloat($("#spanResAmount").text());
        //alert(payedAmt);
        //$("#hidePAYED_AMT").val(payedAmt);
        $("#formSave").submit();
    }
    //结账
    $("#btnCheckIn").click(function () {
        $("#hide_SubmitType").val("check");
        submit("您确认要结账吗？");
    });
    //保存订单
    $("#btnSave").click(function () {
        $("#hide_SubmitType").val("save");
        submit("您确认要保存订单吗？");
    });
    //跳转列表页面
    function returnList() {
        window.location.href = options.returnListUrl;
    }


})(window, undefined, jQuery);

//********************//
var f = new Object();
//获取参数精度，如果为整数则精度为0  
f._getPrecision = function (arg) {
    if (arg.toString().indexOf(".") == -1) {
        return 0;
    } else {
        return arg.toString().split(".")[1].length;
    }
}
//获取小数的整数形式  
f._getIntFromFloat = function (arg) {
    if (arg.toString().indexOf(".") == -1) {
        return arg;
    } else {
        return Number(arg.toString().replace(".", ""));
    }
}
//乘法  
f.floatMulti = function (arg1, arg2) {
    var precision1 = this._getPrecision(arg1);
    var precision2 = this._getPrecision(arg2);
    var tempPrecision = 0;

    tempPrecision += precision1;
    tempPrecision += precision2;
    var int1 = this._getIntFromFloat(arg1);
    var int2 = this._getIntFromFloat(arg2);
    return (int1 * int2) * Math.pow(10, -tempPrecision);
}
//加法  
f.floatAdd = function (arg1, arg2) {
    var precision1 = this._getPrecision(arg1);
    var precision2 = this._getPrecision(arg2);
    var temp = Math.pow(10, Math.max(precision1, precision2));
    return (this.floatMulti(arg1, temp) + this.floatMulti(arg2, temp)) / temp;
}
//减法  
//arg1 被减数  
//arg2 减数  
f.floatSubtract = function (arg1, arg2) {
    var precision1 = this._getPrecision(arg1);
    var precision2 = this._getPrecision(arg2);
    var temp = Math.pow(10, Math.max(precision1, precision2));
    return (this.floatMulti(arg1, temp) - this.floatMulti(arg2, temp)) / temp;
}
////除法  
//arg1 被除数  
//arg2 除数  
f.floatDiv = function (arg1, arg2) {
    var precision1 = this._getPrecision(arg1);
    var precision2 = this._getPrecision(arg2);
    var int1 = this._getIntFromFloat(arg1);
    var int2 = this._getIntFromFloat(arg2);
    var result = (int1 / int2) * Math.pow(10, precision2 - precision1);
    return result;
}

//********************//
//是否点钟 选择事件
function cbxIS_DKChange(obj) {//cbxIS_DK
    $(obj).parent("td").parent("tr").children("td").eq(6).find("input[NAME$=IS_DK]").val($(obj).prop("checked"));
}

//护理师选择事件
//function selChange(obj)
//{
//   $(obj).parent("td").children("span").css("display","block");
//}
//新增顾客
function showAddCustName() {
    //$("#firstname").click();
    //$("#mobile").val($("#CUST_NAME").val());
    //alert($("#CUST_NAME").val());
    $(".select2-input").each(function () {
        if ($(this).val() != "") {
            $("#mobile").val($(this).val());
        }
    });
    // alert($(".select2-input").val());
    var height = $("#QueryProdInfo_boxcontain2").height(); //页面打开时提示刷卡
    var width = $("#QueryProdInfo_boxcontain2").width();
    $("#QueryProdInfo_mask2").show();
    $("#QueryProdInfo_boxcontain2").css("top", (window.screen.height - height) / 2).css("left", (window.screen.width - width) / 2).show();
    //$("#custname").focus();;
    //$("#btnSaveCust").click();
    //  alert($("#QueryProdInfo_boxcontain2").css("top"));
}

$("#QueryProdInfo_close2").click(function () {
    $("#QueryProdInfo_mask2").hide();
    $("#QueryProdInfo_boxcontain2").hide();
});


//新增顾客
$("#btnSaveCust").click(function () {
    var GENDER = "男";
    if ($("#woman").prop("checked") == true) {
        GENDER = "女";
    }
    //alert($("#khly").val());
    if ($.trim($("#custname").val()) == '') {
        _showInfoMessage('姓名不能为空！', 'info');
        return;
    }
    else if ($.trim($("#mobile").val()) == '') {
        _showInfoMessage('电话不能为空！', 'info');
        return;
    }
    else {

        $.post("/Customer/Save.do", { NAME: $.trim($("#custname").val()), MOBILE: $.trim($("#mobile").val()), CUST_NO: 0, GENDER: GENDER, SOURCE: $("#khly").val() },
           function (data) {
               if (data.status == 0) {
                   //新增成功
                   window.location.href = window.location.href + "?cid=" + data.data.ID;
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

//删除项目
function delItem(row, price) {
    var mszj = $("#spanMSZJ").html();
    var yh = $("#spanYH").html();
    //项目优惠金额
    var proPrivilege = $(row).parent('td').parent("tr").children("td").find("input[NAME$=proPrivilege]").val();
    var privilege = f.floatSubtract(parseFloat(yh), parseFloat(proPrivilege));

    //yh = yh - 20;
    mszj = f.floatSubtract(parseFloat(mszj), parseFloat(price));
    $(row).parent("td").parent("tr").remove();

    //行数
    var rowcount = $("#tableProds tbody").children("tr").length;
    if (rowcount < 1) {
        //付款 保存
        //$("#divPayMoney").hide();
        //$("#divBtnSave").hide();
        $("#btnSave").hide();
        $("#btnCheckIn").hide();
    }

    $("#spanMSZJ").html(mszj);
    $("#spanYH").html(privilege);
    var zjprice = f.floatSubtract(parseFloat(mszj), parseFloat(privilege));
    //$("#spanZJ").html(zjprice);
    $("#ZJ2").val(zjprice);
    calcPrice(zjprice);

    //剩余金额=应付金额-已付金额
    var payprice = 0;
    $("#tablePayList tbody tr").each(function () {
        payprice = f.floatAdd(parseFloat(payprice), parseFloat($(this).find("input[NAME$=payMoney]").val()));
    });
    //剩余未支付=应付-已付
    //$("#spanResAmount").html(f.floatSubtract(parseFloat($("#spanZJ").html()), parseFloat(payprice)));
    //储值卡支付金额
    //$("#payMoneyVIPCARD").val($("#spanResAmount").html());
    $("#payMoneyVIPCARD").val(f.floatAdd(parseFloat($("#spanCardPay").html()), parseFloat($("#spanCardDistPay").html())));
    //现金支付金额
    //$("#payMoneyPOSTCARD").val($("#spanResAmount").html());
    $("#payMoneyPOSTCARD").val($("#spanCashPay").html());

    //支付提醒begin
    //允许卡支付但不享受折扣金额
    var cardPayPrice = $("#spanCardPay").html();
    //允许卡支付享受折扣金额
    var cardDistPayPrice = $("#spanCardDistPay").html();
    //现金
    var cashPay = $("#spanCashPay").html();
    var cardPay = $(row).parent("td").find("input[NAME$=cardPay]").val().toUpperCase();
    var discount = $(row).parent("td").find("input[NAME$=discount]").val().toUpperCase();
    var payAmt = $(row).parent("td").find("input[NAME$=payAmt]").val();//实际支付价格

    //允许卡支付但不享受折扣金额：
    if (cardPay == "TRUE" && discount == "FALSE") {
        cardPayPrice = f.floatSubtract(parseFloat(cardPayPrice), parseFloat(payAmt));
    }
        //允许卡支付并享受折扣金额
    else if (cardPay == "TRUE" && discount == "TRUE") {
        cardDistPayPrice = f.floatSubtract(parseFloat(cardDistPayPrice), parseFloat(payAmt));
    }
    else {
        cashPay = f.floatSubtract(parseFloat(cashPay), parseFloat(payAmt));
    }
    $("#spanCardPay").html(cardPayPrice);
    $("#spanCardDistPay").html(cardDistPayPrice);
    //剩下现金支付
    $("#spanCashPay").html(cashPay);
    //end

    //如果删除的是疗程卡  则还原其次数
    var proId = $(row).parent("td").find("input[NAME$=proId]").val();
    var proType = $(row).parent("td").find("input[NAME$=proType]").val();
    var cust_cdetail_id = $(row).parent("td").find("input[NAME$=CUST_CDETAIL_ID]").val();
    if (cust_cdetail_id != undefined && cust_cdetail_id != null && cust_cdetail_id != "") {
        var count = $("#tTreatmentPro #" + cust_cdetail_id).children("td").eq(2).text().split(' ')[0];
        if (count == "") {
            count = 0;
        }
        count = parseInt(count) + 1;
        var mess = '<span class="badge">' + count + ' 次</span>';
        $("#tTreatmentPro #" + cust_cdetail_id).children("td").eq(2).html(mess);;
    }
}

//价格格式化
function formatPrice(value) {
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
    return (((sign) ? '' : '-') + '' + value);
}
//金额转数字
function fromatMoneyNum(value) {
    var num = value.trim();
    var ss = value.toString();
    if (ss.length == 0) {
        return "0";
    }
    return ss.replace(/,/g, "");

}
//优惠/活动选择
function privilege(obj) {
    var id = $(obj).children('option:selected').val();

    var yhprice = $("#spanYH").text();
    var proprivilege = $(obj).parent('td').parent("tr").children("td").find("input[NAME$=proPrivilege]").val();
    if (id > 0) {
        //if (id == "3") {
        //    $('#confirmModal').modal('show');
        //    $(obj).children('option:selected').val(null);
        //    return false;
        //}


        //获取产品单价
        var proprice = $(obj).parent('td').parent("tr").children("td").find("input[NAME$=proPrice]").val();
        //优惠金额
        var promprice = $(obj).children('option:selected').text().split('￥')[1];
        //最终金额=产品单价-优惠金额
        var totalprice = f.floatSubtract(parseFloat(proprice), parseFloat(promprice));
        //显示产品需要支付价格
        $(obj).parent('td').parent("tr").children("td").eq(5).html(formatPrice(totalprice));
        //实际需要支付价格
        $(obj).parent('td').parent("tr").children("td").find("input[NAME$=payAmt]").val(totalprice);
        //计算优惠和合计
        //优惠=门市价-优惠后价格
        //var yhprice = $("#spanYH").text();
        //门市价
        var msprice = $(obj).parent('td').parent("tr").children("td").eq(4).html();
        //优惠
        var yh = f.floatSubtract(parseFloat(yhprice), parseFloat(proprivilege));
        $("#spanYH").text(f.floatAdd(parseFloat(yh), parseFloat(promprice)));
        //合计
        var zjprice = f.floatSubtract(parseFloat($("#spanMSZJ").html()), parseFloat($("#spanYH").html()));
        //$("#spanZJ").html(zjprice);
        $("#ZJ2").val(zjprice);
        calcPrice(zjprice);
        //支付提醒begin
        //允许卡支付但不享受折扣金额
        var cardPayPrice = $("#spanCardPay").html();
        //允许卡支付享受折扣金额
        var cardDistPayPrice = $("#spanCardDistPay").html();
        //现金支付
        var cashPayprice = $("#spanCashPay").html();

        var cardPay = $(obj).parent("td").parent("tr").find("input[NAME$=cardPay]").val().toUpperCase();
        var discount = $(obj).parent("td").parent("tr").find("input[NAME$=discount]").val().toUpperCase();
        var proPrivilege = $(obj).parent("td").parent("tr").find("input[NAME$=proPrivilege]").val();//上次选择优惠金额

        //允许卡支付但不享受折扣金额：
        if (cardPay == "TRUE" && discount == "FALSE") {
            //减去上一次优惠 加上本次选择
            // cardPayPrice = f.floatSubtract(f.floatAdd(parseFloat(cardPayPrice), parseFloat(proPrivilege)), parseFloat(promprice));
            cardPayPrice = f.floatAdd(parseFloat(cardPayPrice), parseFloat(proPrivilege)) - parseFloat(promprice);

        }
            //允许卡支付并享受折扣金额
        else if (cardPay == "TRUE" && discount == "TRUE") {
            cardDistPayPrice = f.floatSubtract(f.floatAdd(parseFloat(cardDistPayPrice), parseFloat(proPrivilege)), parseFloat(promprice));
        }
        else {
            cashPayprice = f.floatSubtract(f.floatAdd(parseFloat(cashPayprice), parseFloat(proPrivilege)), parseFloat(promprice));
        }

        $("#spanCardPay").html(cardPayPrice);
        $("#spanCardDistPay").html(cardDistPayPrice);
        //剩下现金支付
        $("#spanCashPay").html(cashPayprice);
        //end

        //优惠金额
        $(obj).parent('td').parent("tr").children("td").find("input[NAME$=proPrivilege]").val(promprice);



        //添加赠送
    }
    else {

        //未选择优惠 或者选择优惠活动有误
        //还原最终价格 ==门市价
        var msj = $(obj).parent('td').parent("tr").children("td").eq(3).html();

        $(obj).parent('td').parent("tr").children("td").eq(5).html(msj);
        //实际需要支付价格
        $(obj).parent('td').parent("tr").children("td").find("input[NAME$=payAmt]").val(msj);


        //支付提醒begin
        //允许卡支付但不享受折扣金额
        var cardPayPrice = $("#spanCardPay").html();
        //允许卡支付享受折扣金额
        var cardDistPayPrice = $("#spanCardDistPay").html();
        //现金支付
        var cashPayprice = $("#spanCashPay").html();

        var cardPay = $(obj).parent("td").parent("tr").find("input[NAME$=cardPay]").val().toUpperCase();
        var discount = $(obj).parent("td").parent("tr").find("input[NAME$=discount]").val().toUpperCase();
        var proPrivilege = $(obj).parent("td").parent("tr").find("input[NAME$=proPrivilege]").val();//上次选择优惠金额

        //允许卡支付但不享受折扣金额：
        if (cardPay == "TRUE" && discount == "FALSE") {
            //还原上次优惠
            cardPayPrice = f.floatAdd(parseFloat(cardPayPrice), parseFloat(proPrivilege));
        }
            //允许卡支付并享受折扣金额
        else if (cardPay == "TRUE" && discount == "TRUE") {
            cardDistPayPrice = f.floatAdd(parseFloat(cardDistPayPrice), parseFloat(proPrivilege));
        }
        else {
            cashPayprice = f.floatAdd(parseFloat(cashPayprice), parseFloat(proPrivilege));
        }

        $("#spanCardPay").html(cardPayPrice);
        $("#spanCardDistPay").html(cardDistPayPrice);
        //剩下现金支付
        $("#spanCashPay").html(cashPayprice);
        //end

        var yh = f.floatSubtract(parseFloat(yhprice), parseFloat(proprivilege));
        //优惠金额
        $(obj).parent('td').parent("tr").children("td").find("input[NAME$=proPrivilege]").val(0);
        //应付

        $("#spanYH").text(yh);
        var zjprice = f.floatSubtract(parseFloat($("#spanMSZJ").html()), parseFloat($("#spanYH").html()));
        //$("#spanZJ").html(zjprice);
        $("#ZJ2").val(zjprice);
        calcPrice(zjprice);
    }

    //剩余金额=应付金额-已付金额
    var payprice = 0;
    $("#tablePayList tbody tr").each(function () {
        payprice = f.floatAdd(parseFloat(payprice), parseFloat($(this).find("input[NAME$=payMoney]").val()));
    });
    //剩余未支付
    //$("#spanResAmount").html(f.floatSubtract(parseFloat($("#spanZJ").html()), parseFloat(payprice)));

    //储值卡支付金额
    //$("#payMoneyVIPCARD").val($("#spanResAmount").html());
    $("#payMoneyVIPCARD").val(f.floatAdd(parseFloat($("#spanCardPay").html()), parseFloat($("#spanCardDistPay").html())));
    //现金支付金额
    $("#payMoneyPOSTCARD").val($("#spanCashPay").html());
}
//弹出层
function openwin(id) {
    $("#hide_Employee").val(id);
    $("#EMP_NAME").val("");
    $("#divcontentone").hide();
    $("#divcontent").show();
    id = "#" + id;
    var left = $(id).offset().left;
    var top = $(id).offset().top;
    $("#windo").css({ position: "absolute", "top": top, "left": left, "margin-top": 32 });
    $("#windo").show();
}
//选择员工
function addEmp(obj, empID) {
    var id = "#" + $("#hide_Employee").val();
    var _li = $(obj).parents('li');
    $(id).val(_li.attr("id"));//当前激活员工文本框

    var bTime = $('#BEGINTIME' + empID).val(),
        eTime = $('#ENDTIME' + empID).val();

    var _tr = $(id).parent("td").parent("tr");
    _tr.find("input[NAME$=Employee]").val(empID);
    _tr.find("input[NAME$=BeginTime]").val(bTime);
    _tr.find("input[NAME$=EndTime]").val(eTime);

    $("#closewin").click();
}
//选择时间
function selectTime(sender) {
    $(sender).timepicker({
        language: "zh-CN"
    });
    $(sender).timepicker("show");
}

String.prototype.replaceAll = function (s1, s2) {
    return this.replace(new RegExp(s1, "gm"), s2);
}



//计算服务费
function calcPrice(zj) {
    var zongjia = $('#ZJ2').val();
    zongjia = parseFloat(zongjia);
    var servicePrice = '0.00';
    var _isChecked = $('#chkFWF').prop('checked');
    if (_isChecked == true) {
        var mszj = $("#spanMSZJ").html();
        mszj = parseFloat(mszj);
        //console.log(mszj);
        var _sRate = $('#hideFWF_Rate').val();
        servicePrice = f.floatMulti(mszj, parseFloat(_sRate));
        servicePrice = servicePrice.toFixed(0);
        //console.log(servicePrice);
        zongjia = f.floatAdd(zongjia, servicePrice);
        //console.log(zongjia);
    }
    $('#spanFWF').text(servicePrice);
    $("#spanZJ").html(zongjia);
}

