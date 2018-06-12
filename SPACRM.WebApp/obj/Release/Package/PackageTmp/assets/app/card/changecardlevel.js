; (function (window, undefined, $) {
    $(document).ready(function () {
        $("#divPayType").hide();
        $("#divPayType2").hide();
        $("#divCustomer").hide();


        getCustomerInfo( $("#hide_Cid").val());
        var submiting = false;
        $('#frmSave').validator({
            valid: function (form) {
                if (submiting) {
                    return;
                }
                submiting = true;
                FormSubmit(form, function (res) {
                    if (res.status > 0) {
                        _showInfoMessage("操作成功！", "success");
                       // window.location.reload();

                        window.location.href = "/Card/ChangeCardLevelList.do";

                    }
                    else {
                        _showInfoMessage("操作失败！" + res.message, "error");
                    }
                    submiting = false;
                })
            }
        });

        //新增顾客
        function showAddCustName() {
            $("#firstname").click();
        }

        //新增顾客//
        $('#firstname').editable({
            validate: function (value) {
                if ($.trim($("#custname").val()) == '') {
                    return '姓名不能为空';
                }
                else if ($.trim($("#mobile").val()) == '') {
                    return '电话不能为空';
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


        //根据姓名/电话检索
        $("#CUST_NAME").select2({
            placeholder: "输入姓名、电话查询",
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
                getCustomerInfo(cid);
            }
            return false;
        }


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
                          //$("#spanOrder_No").html(data.data.id);
                          //$("#ROOM_ID").val(data.data.ROOM_ID);
                          $("#divNewCustomer").hide();
                          $("#divCustomer").show();

                          getOrderDetail(oidx);
                      }
                      else {
                          $("#divCustomer").hide();
                          $("#divNewCustomer").show();
                      }
                  },
                  "json"
                 );
        }


        //顾客信息
        function getCustomerInfo(cid) {
            //查顾客信息
            $.post("/Customer/Detail.do", { id: cid },
                  function (data) {
                      if (data.status == 1) {
                          $("#hide_Cid").val(cid);
                          $("#spanCustName").html(data.data.NAME);
                          $("#spanCustMobile").html(data.data.MOBILE);
                          $("#spanCustCard").html(data.data.CARD_NO);
                          $("#hideCUST_ID").val(data.data.ID);
                          //$("#spanCustCardLevel").html(data.data.CARD_NAME);
                          //$("#spanCustCardBalance").html(data.data.BALANCE);
                          $("#divNewCustomer").hide();
                          $("#divCustomer").show();

                          //查询卡级信息tSpeedyPro
                          getCusCardInfo(cid);
                      }
                      else {
                          $("#divCustomer").hide();
                          $("#divNewCustomer").show();
                      }
                  },
                  "json"
                 );
        }


        //检索顾客会员卡，疗程卡信息
        function getCusCardInfo(cid) {
            $.get("/Card/GetChangeCardLevelByCust_ID.do", { cid: cid },
                     function (data) {
                         if (data.status == 1) {
                             var cardLevelHtml = [];
                             var json = data.data;
                             if (json.length > 0) {
                                 //$(json).each(function () {
                                     for (var j = 0; j < json.length; j++) {
                                         cardLevelHtml.push("<tr class='strip'>");
                                         if ($("#OLD_CARD_ID").val() == json[j].CARD_ID) {
                                             cardLevelHtml.push("<td><input type=\"radio\" name=\"OldCardId\" checked=\"checked\" id=\"isSelect\"  onclick = \"util.onClick(this," + json[j].ID + ", " + json[j].PAR_AMT + ", " + json[j].BALANCE + ",  " + json[j].ARREARS + "," + json[j].CARD_ID + ",'"+json[j].CARD_NAME+"')\"  value=\"" + json[j].ID + "\"/><input type=\"hidden\" name=\"ID\" value=\"" + json[j].ID + "\" /><input type=\"hidden\" name=\"STORE_ID\" value=\"" + json[j].STORE_ID + "\" /></td>");
                                         } else {
                                             cardLevelHtml.push("<td><input type=\"radio\" name=\"OldCardId\" id=\"isSelect\"  onclick = \"util.onClick(this," + json[j].ID + ", " + json[j].PAR_AMT + ", " + json[j].BALANCE + ",  " + json[j].ARREARS + "," + json[j].CARD_ID + ",'"+json[j].CARD_NAME+"')\"  value=\"" + json[j].ID + "\"/><input type=\"hidden\" name=\"ID\" value=\"" + json[j].ID + "\" /><input type=\"hidden\" name=\"STORE_ID\" value=\"" + json[j].STORE_ID + "\" /></td>");
                                         }
                                         
                                         
                                         cardLevelHtml.push("<td>" + json[j].STORE_NAME + "</td>")
                                         cardLevelHtml.push("<td>" + json[j].CARD_NAME + "</td>")
                                         cardLevelHtml.push("<td>" + json[j].PAR_AMT + "</td>")
                                         cardLevelHtml.push("<td>" + json[j].BALANCE + "</td>")
                                         cardLevelHtml.push("<td>" + json[j].ARREARS + "</td>");
                                         cardLevelHtml.push("<\tr>");
                                     }
                                 //});
                                 $("#divCustomer").show();
                                 $("#divNewCustomer").hide();
                                 $("#tCardLavel tbody").html(cardLevelHtml.join(""));
                             } else {
                                 alert("该客户没有卡级信息！");
                                 $("#spanCustName").html("");
                                 $("#divCustomer").hide();
                                 $("#divNewCustomer").show();
                                 $("#tCardLavel tbody").html("");

                             }
                         }
                     },
                     "json"
                    );
        }

        var removeoption;
        util.onClick = function (object, CUST_CARD_ID, OLD_CARD_TAMT, OLD_CARD_BALANCE, OLD_CARD_ARREARS, CARD_ID,CARD_NAME) {
            if (object.checked) {
                $("#CUST_CARD_ID").val(CUST_CARD_ID);
                $("#OLD_CARD_ID").val(CARD_ID);//卡级id
                $("#OLD_CARD_TAMT").val(OLD_CARD_TAMT);
                $("#OLD_CARD_BALANCE").val(OLD_CARD_BALANCE);
                $("#OLD_CARD_ARREARS").val(OLD_CARD_ARREARS);
                $("#OLD_CARD_USED_AMT").val(OLD_CARD_TAMT - OLD_CARD_BALANCE);

                $("#IsSelectOldCardLevel").val(1);
                if (!($("#NEW_CARD_ID").val() == "" || $("#NEW_CARD_ID").val() == null)) {
                    $("#NEW_CARD_SALE_AMT").val();//销售金额
                    //老卡欠款
                    var amt = 0;//;
                    amt = $("#OLD_CARD_BALANCE").val();//老卡余额

                    //2014-10-10$("#Need_PAY_AMT").val(parseFloat($("#NEW_CARD_SALE_AMT").val()) - amt + parseFloat(OLD_CARD_ARREARS));
                    //$("#Need_PAY_AMT").val(parseFloat($("#NEW_CARD_SALE_AMT").val() - parseFloat(amt)).toFixed(2));//需支付金额  大于0 isQuitAmt不可见 小于0 可见
                    $("#Need_PAY_AMT").val(parseFloat($("#NEW_CARD_SALE_AMT").val() - parseFloat(OLD_CARD_TAMT)).toFixed(2));//需支付金额  大于0 isQuitAmt不可见 小于0 可见
                    $("#spanPayed").html(parseFloat($("#Need_PAY_AMT").val()).toFixed(2));
                    $("#PAYED_AMT").val(parseFloat($("#Need_PAY_AMT").val()).toFixed(2));
                    SetHideAndShow();
                }


                //   $.post("/Card/CardDetailAndSendInfo.do", { id: id },
                //function (ret) {
                //    if (ret.status > 0) {
                //        var data = eval(ret.data);


                //去掉当前选中的卡级NEW_CARD_ID
                //$("#NEW_CARD_ID").empty();
                if (removeoption != "") {
                    $("#NEW_CARD_ID").append(removeoption);
                }
                var checkText = CARD_NAME;  //获取Select选择的Text
                var checkValue = CARD_ID;  //获取Select选择的Value
                removeoption = "<option value='" + checkValue + "'>" + checkText + "</option>";

                var indx = $("#NEW_CARD_ID").get(0).selectedIndex;
                $("#NEW_CARD_ID option[value='" + CARD_ID + "']").remove();
                


                //var sel = document.getElementById("NEW_CARD_ID");
                ////document.all.sel.options.remove(document.all.sel.selectedindex);
                ////document.all.sel.selectindex = 0;
                //var opt = new Option("A", "11");
                //sel.options.add(opt);
                //var opt1 = new Option("B", "11");
                //sel.options.add(opt1);
                //opt1.selected = true;
                //var opt2 = new Option("C", "11");
                //sel.options.add(opt2);
                
                //    }
                //});
            }
        }

        function SetHideAndShow() {
            var Need_PAY_AMT = parseFloat($("#Need_PAY_AMT").val());
            if (Need_PAY_AMT >= 0) {
                $("#isQuitAmt").hide();
                $("#divPayType").show();
                $("#divPayType2").hide();
            } else {
                $("#isQuitAmt").show();
                $("#divPayType").hide();
                $("#divPayType2").show();
                if ($("#QuitCash").checked) {
                    QuitCashClick(1);
                } else if ($("#NoQuit").checked) {
                    QuitCashClick(2);
                }
            }
        }


        $("#NEW_CARD_ID").change(function () {
            var id = $("#NEW_CARD_ID").val();
            if (id == "" || id == null) {
                _showInfoMessage('请选择会员卡级！', 'info');
                return false;
            }
            //获取新卡级信息 在判断是否有赠送项目
            $.post("/Card/CardDetailAndSendInfo.do", { id: id },
                  function (ret) {
                      if (ret.status > 0) {
                          var data = eval(ret.data);
                          $("#NEW_CARD_SALE_AMT").val(data.PRICE);//销售金额
                          $("#NEW_CARD_BALANCE").val(data.PAR_AMT);//卡内余额
                          $("#NEW_PAR_AMT").val(data.PAR_AMT);//卡内余额
                          var prodName = $("#NEW_CARD_ID").find("option:selected").text();
                          $("#hidePROD_NAME").val(prodName);
                          //卡的有效期
                          //PERIOD
                          var PERIOD = data.PERIOD;
                          if (PERIOD == "" || PERIOD==null)
                          {
                              PERIOD = 120;//有效期默认120个月
                          }
                          var myDate = new Date();
                          var DateFrom = myDate.getFullYear() + "-" + (parseInt(myDate.getMonth()) + parseInt('1')) + "-" + myDate.getDate();     //获取当前日期
                          var DateTo = addmulMonth(DateFrom, PERIOD);
                         
                          $("#CardBeginDate").val(DateFrom);
                          $("#CardEndDate").val(DateTo);

                          var OLD_CARD_TAMT = $("#OLD_CARD_TAMT").val();//老卡销售已付的金额
                          //老卡欠款
                          var amt = 0;//老卡余额
                          amt = $("#OLD_CARD_BALANCE").val();
                          var ARREARS=parseFloat($("#OLD_CARD_ARREARS").val());
                          if ($("#IsSelectOldCardLevel").val() == 1) {
                              //2014-10-10$("#Need_PAY_AMT").val(parseFloat(data.PRICE) - amt + parseFloat(ARREARS));
                              //$("#Need_PAY_AMT").val(parseFloat(data.PRICE - amt).toFixed(2));
                              $("#Need_PAY_AMT").val(parseFloat(data.PRICE - OLD_CARD_TAMT).toFixed(2));
                              $("#spanPayed").html(parseFloat($("#Need_PAY_AMT").val()).toFixed(2));
                              $("#PAYED_AMT").val(parseFloat($("#Need_PAY_AMT").val()).toFixed(2));
                              SetHideAndShow();
                          }

                          //活动
                          promotion(id);
                      }
                  });






            //var prodName = $("#CARD_ID").find("option:selected").text();
            ////$("#hidePROD_NAME").val(prodName);
            ////清空赠送项目项
            ////$("#tProdItem tbody tr").remove();
            //$("#spanPLPRICE").html("0");
            //$.post("/Card/Detail.do", { id: id },
            //        function (ret) {
            //            if (ret.status > 0) {
            //                var data = eval(ret.data);
            //                $("#BUY_AMT").val(data.PRICE);
            //                $("#YH_AMT").val(parseFloat(data.PAR_AMT) - parseFloat(data.PRICE));
            //                //$("#CASH_AMT").val(data.PRICE);
            //                var mon = data.PERIOD;
            //                var bdate = new Date();
            //                bdate.setMonth(bdate.getMonth() + mon);
            //                var edate = bdate.getFullYear() + "-" + (bdate.getMonth()) + "-" + bdate.getDate();
            //                $("#END_DATE").val(edate);

            //                //活动
            //                promotion(id);
            //            }
            //        },
            //        "json"
            //       );
        });


        function addmulMonth(dtstr, n) {
            var s = dtstr.split("-");
            var yy = parseInt(s[0]);
            var mm = parseInt(s[1]) - 1;
            var dd = parseInt(s[2]);
            var dt = new Date(yy, mm, dd);
            dt.setMonth(dt.getMonth() + n);
            var month = parseInt(dt.getMonth()) + 1;
            return dt.getFullYear() + "-" + month + "-" + dd;
        }

        function getNewDay(dateTemp, months) {
            var dateTemp = dateTemp.split("-");
            var nDate = new Date(dateTemp[1] + '-' + dateTemp[2] + '-' + dateTemp[0]); //转换为MM-DD-YYYY格式    
            var millSeconds = Math.abs(nDate);  //（加上days天 ：var millSeconds = Math.abs(nDate) - (days * 24 * 60 * 60 * 1000);）
            var rDate = new Date(millSeconds);
            var year = rDate.getFullYear();
            var month = rDate.getMonth() + months;
            if (month < 10) month = "0" + month;
            var date = rDate.getDate();
            if (date < 10) date = "0" + date;
            return (year + "-" + month + "-" + date);
        }

        util.keyPress = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
        }
        util.onBlur = function (ob) {
            if (!ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?|\.\d*?)?$/))
                ob.value = ob.o_value;
            else {
                if (ob.value.match(/^\.\d+$/))
                    ob.value = 0 + ob.value;
                if (ob.value.match(/^\.$/))
                    ob.value = 0; ob.o_value = ob.value
            };
        };
        util.keyUpCash = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {
                var but_amt = $("#Need_PAY_AMT").val();
                var vipcard = $("#NEW_CARD_ID").val();
                if (vipcard == "" || vipcard == null) {
                    _showInfoMessage("请您先选择会员卡级！", "info");
                    ob.value = 0;
                    return false;
                }
                //销售员
                //var masseur = $("#Masseur").val();
                //if (masseur == "" || masseur == null) {
                //    _showInfoMessage("请您先选择销售员！", "info");
                //    ob.value = 0;
                //    return false;
                //}
                //var paycard_type = $("#PAYCARD_TYPE").val();
                //if (paycard_type == "" || paycard_type == null) {
                //    _showInfoMessage("请您先选择支付方式！", "info");
                //    ob.value = 0;
                //    return false;
                //}
                if (parseFloat(ob.value) > parseFloat(but_amt)) {
                    _showInfoMessage("付款金额不能大于销售金额！", "info");
                    ob.value = but_amt;
                    $("#ARREARS").val(0);
                    $("#spanPayed").html(parseFloat(ob.value).toFixed(2));
                    return false;
                }
                var cash_amt = parseFloat(but_amt) - parseFloat(ob.value);
                if (!isNaN(cash_amt)) {
                    //欠款
                    $("#ARREARS").val(parseFloat(cash_amt).toFixed(2));
                    $("#spanPayed").html(parseFloat(ob.value).toFixed(2));
                }

            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };
        //卡支付方式
        util.keyUpPayment = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {

                //if(ob.value==""||ob.value==null)
                //{
                //    ob.value = 0;
                //    return false;
                //}
                var vipcard = $("#NEW_CARD_ID").val();
                if (vipcard == "" || vipcard == null) {
                    _showInfoMessage("请您先选择会员卡级！", "info");
                    ob.value = 0;
                    return false;
                }
                //销售员
                //var masseur = $("#Masseur").val();
                //if (masseur == "" || masseur == null) {
                //    _showInfoMessage("请您先选择销售员！", "info");
                //    ob.value = 0;
                //    return false;
                //}
                var payed_amt = $.trim($("#PAYED_AMT").val());
                if (payed_amt == "" || payed_amt == null) {
                    _showInfoMessage("请您先输入付款金额！", "info");
                    ob.value = 0;
                    return false;
                }

                var paymentMode = $('input[name=PaymentMode]');
                var payTotal = 0;
                $(paymentMode).each(function () {
                    if (this.value != "" && this.value != null) {
                        payTotal += parseFloat(this.value);
                    }
                });
                //应付金额
                var payed_amt = parseFloat($("#Need_PAY_AMT").val());

                var balance = payed_amt - parseFloat(payTotal);
                if (balance == 0) {
                    $("#btnSave").removeAttr("disabled");
                } else {
                    $("#btnSave").attr("disabled", "disabled");
                }
                if (balance < 0) {
                    _showInfoMessage("支付总额不能大于还需支付金额！", "info");
                    ob.value = 0;
                    return false;
                }
                $("#spanPayed").html(parseFloat(balance).toFixed(2));
            }
            catch (ex) {
                _showInfoMessage("金额填写有误！" + ex.message, "error");
            }
        };

        function promotion(id) {
            var cust_id = $("#hide_Cid").val(); 
            var cust_card_id = $("#CUST_CARD_ID").val();
            $.post("/Card/getPresent_Prod.do", { cust_id: cust_id, cardId: id, cust_card_id: cust_card_id },
                   function (ret) {
                       if (ret.status > 0) {
                           if (ret.data.length > 0) {
                               $("#isSendProc").show();
                               var data = eval(ret.data);
                               var html = [];
                               $("#Present_Prod option").remove();
                               $("#Present_Prod").append("<option value=''>请选择...</option>");
                               $(data).each(function () {
                                   html.push("<option itemid=" + this.PROM_LIMIT_PRICE + " value=" + this.ID + ">" + this.NAME + "</option>");

                               });
                               $(html.join('')).appendTo("#Present_Prod");
                           } else {
                               $("#isSendProc").hide();
                           }
                       }
                   },
                   "json"
                  );
        }

        $("#spanPresent").click(function () {
            if ($("#divPresent").css("display") == "block") {
                $("#divPresent").hide();
                $("#spanPresent").html("赠送项目【展开】");
            }
            else {
                $("#divPresent").show();
                $("#spanPresent").html("赠送项目【收起】");
            }
        });

        $("#Present_Prod").change(function () {

            var promId = $("#Present_Prod").val();
            $("#tProdItem tbody tr").remove();
            $("#spanPLPRICE").html("0");
            if (promId == null || promId == "") {
                _showInfoMessage("请选择赠送项目！", "info");
                return false;
            }
            //根据选择赠送项目包检索项目详情
            $.post("/Order/getPresent_ProdItem.do", { promId: promId },
                  function (ret) {
                      if (ret.status > 0) {
                          var data = eval(ret.data);
                          var html = [];
                          $(data).each(function () {
                              html.push("<tr>");
                              html.push("<td><input type='checkbox' name='cbxCheck' onclick='util.cbxCheck(this)' value=" + this.PROD_ID + " style='width:15px;height:15px;'></td>");
                              html.push("<td>" + this.PROD_NAME + "</td>");
                              html.push("<td>" + this.PRICE + "</td>");
                              html.push("<td><input type='text' value='1' maxlength='3' name='prodNum'  onkeypress = 'util.keyPress(this)' onkeyup = 'util.keyUpPresent(this)' onblur = 'util.onBlur(this)' style='width:60px;' />");
                              html.push("<input type='hidden' name='prodId' value=" + this.PROD_ID + " /> ");
                              html.push("<input type='hidden' name='prodName' value=" + this.PROD_NAME + " /> ");
                              html.push("<input type='hidden' name='prodPrice' value=" + this.PRICE + " /> ");
                              html.push("</td>");
                              html.push("</tr>");
                          });
                          
                          $("#hideBeginDate").val(data[0].BEGIN_DATE);
                          $("#hideEndDate").val(data[0].END_DATE);
                          var plprice = $("#Present_Prod").children('option:selected').attr("itemid");
                          $("#spanPLPRICE").html(plprice);
                          $(html.join('')).appendTo("#tProdItem tbody");
                      }
                  },
                  "json"
                 );

        });


        //卡支付方式
        util.keyUpPresent = function (ob) {
            if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                ob.value = ob.t_value;
            else ob.t_value = ob.value;
            if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                ob.o_value = ob.value;
            try {

                if (ob.value == "" || ob.value == null || ob.value <= 0) {
                    ob.value = 1;
                    return false;
                }

                var totalprice = 0;
                $("#tProdItem tbody tr").each(function () {
                    var cbx = $(this).find("input[NAME$=cbxCheck]").is(':checked');
                    if (cbx) {
                        var price = parseFloat($(this).children("td").eq(2).html());
                        var num = parseInt($(this).find("input[NAME$=prodNum]").val());
                        totalprice += parseFloat(price * num);
                    }
                });

                //赠送金额上限
                var plprice = parseFloat($("#spanPLPRICE").html());
                if (totalprice > plprice) {
                    _showInfoMessage("赠送项目不能超过上限金额！", "info");
                    totalprice = 0;
                    $(ob).parent("td").parent("tr").find("input[NAME$=cbxCheck]").attr("checked", false);
                    $(ob).parent("td").parent("tr").find("input[NAME$=prodNum]").val("1");

                    $("#tProdItem tbody tr").each(function () {
                        var cbx = $(this).find("input[NAME$=cbxCheck]").is(':checked');
                        if (cbx) {
                            var price = parseFloat($(this).children("td").eq(2).html());
                            var num = parseInt($(this).find("input[NAME$=prodNum]").val());
                            totalprice += parseFloat(price * num);
                        }
                    });
                    $("#spanTotalPrice").html(totalprice);
                    return false;
                }
                $("#spanTotalPrice").html(totalprice);


            }
            catch (ex) {
                _showInfoMessage("数据填写有误！" + ex.message, "error");
            }
        };

        //选择赠送项目
        util.cbxCheck = function (ob) {
            var totalprice = 0;
            $("#tProdItem tbody tr").each(function () {
                var cbx = $(this).find("input[NAME$=cbxCheck]").is(':checked');
                if (cbx) {
                    var price = parseFloat($(this).children("td").eq(2).html());
                    var num = parseInt($(this).find("input[NAME$=prodNum]").val());
                    totalprice += parseFloat(price * num);
                }
            });
            //赠送金额上限
            var plprice = parseFloat($("#spanPLPRICE").html());
            if (totalprice > plprice) {
                _showInfoMessage("赠送项目不能超过上限金额！", "info");
                $(ob).attr("checked", false);
                return false;
            }
            $("#spanTotalPrice").html(totalprice);

        };
        $('#QuitCash').click(function () {
            QuitCashClick(1);
        })

        $('#NoQuit').click(function (i) {
            QuitCashClick(2);
        })
        function QuitCashClick(i) {
            var Need_PAY_AMT = parseFloat($("#Need_PAY_AMT").val());
            if (Need_PAY_AMT < 0) {
                $("#divPayType").hide();
                $("#divPayType2").show();
                if (i == 1)//退现金
                {
                    //$("#spanPayed").html(Need_PAY_AMT);
                    $("#li_cash_pay").show();
                    //$("#li_card_pay").show();
                    $("#cashpay").val(Need_PAY_AMT);
                    //$("#cardpay").val($("#OLD_CARD_BALANCE").val());
                }
                else {
                    //不退现金
                    $("#spanPayed").html(0);
                    $("#li_cash_pay").hide();
                    $("#li_card_pay").show();
                }
            }
        }

        //保存
        $('#btnSaveOrder').click(function () {
            //销售员
            var CustName = $("#spanCustName").html();
            if (CustName == "" || CustName == null) {
                _showInfoMessage("请您先选择需要变更卡级的客户！", "info");
                return false;
            }

            var OLD_CARD_ID = $("#OLD_CARD_ID").val();
            if (OLD_CARD_ID == "" || OLD_CARD_ID == null) {
                _showInfoMessage("请您先选择需要变更的卡级！", "info");
                return false;
            }

            //销售员
            var NEW_CARD_ID = $("#NEW_CARD_ID").val();
            if (NEW_CARD_ID == "" || NEW_CARD_ID == null) {
                _showInfoMessage("请您先选择需要变更到哪个卡级！", "info");
                return false;
            }
            var Need_PAY_AMT =parseFloat( $("#Need_PAY_AMT").val());
            if (Need_PAY_AMT > 0) {
                var spanPayed = $("#spanPayed").html();
                if (spanPayed != parseFloat("0").toFixed(2)) {
                    _showInfoMessage("还有" + spanPayed + "没有付清,请全部付清！", "info");
                    return false;
                }
            }



            $("#frmSave").submit();
        });


        //util.click = function (object) {
        //    var amt1 = parseFloat($("#NEW_CARD_SALE_AMT").val());//销售金额
        //    var amt2 = parseFloat($("#OLD_CARD_BALANCE").val());//老卡余额
        //    var amt3 = parseFloat($("#OLD_CARD_ARREARS").val());//老卡欠款
        //    if (object.checked) {
        //        if (amt2 > amt1 + amt3) {
        //            $("#OfUseAmt").val(amt1 + amt3);
        //            $("#Need_PAY_AMT").val(0);
        //            $("#spanPayed").html(0);
        //            //卡付amt1 + amt3
        //        } else {
        //            $("#OfUseAmt").val(amt2);
        //            $("#Need_PAY_AMT").val(amt1 + amt3 - amt2);
        //            $("#spanPayed").html(amt1 + amt3 - amt2);
        //            //卡付amt2
        //        }
        //        $("#IsOfUseValue").val(1);
        //        $("#OfUseAmt").attr("disabled", false);
        //    } else {
        //        $("#IsOfUseValue").val(0);
        //        $("#OfUseAmt").attr("disabled", true);
        //        $("#OfUseAmt").val(0);
        //        $("#Need_PAY_AMT").val(amt1 + amt3);
        //        $("#spanPayed").html(amt1 + amt3);
        //    }


        //}


        
        //util.onChangeAmt = function (objectid) {
        //    if ( $("#IsOfUseValue").val()==1) {
        //        var amt = $(objectid).val();//金额
        //        var pattern = /^([0-9.]+)$/;
        //        if (!pattern.test(amt)) {
                   
        //            $(objectid).val("");
        //            $(objectid).focus();
        //            return;
        //        }
        //        var amt2 = parseFloat($("#OLD_CARD_BALANCE").val());//老卡余额
        //        var amt1 = parseFloat($("#NEW_CARD_SALE_AMT").val());//销售金额
        //        var amt2 = parseFloat($("#OLD_CARD_BALANCE").val());//老卡余额
        //        var amt3 = parseFloat($("#OLD_CARD_ARREARS").val());//老卡欠款
        //        if (amt2 > amt1 + amt3) {
        //            if (amt > amt1 + amt3) {
        //                alert("最多只能抵扣" + (amt1 + amt3));
        //                $(objectid).val((amt1 + amt3));
        //                $("#Need_PAY_AMT").val(0);
        //                $("#spanPayed").html(0);
        //            } else {
        //                $("#Need_PAY_AMT").val((amt1 + amt3) - amt);
        //                $("#spanPayed").html((amt1 + amt3) - amt);
        //            }
        //        } else {
        //            if (amt > amt2) {
        //                alert("最多只能抵扣" + amt2);
        //                $(objectid).val(amt2);
        //                $("#Need_PAY_AMT").val((amt1 + amt3) - amt2);
        //                $("#spanPayed").html((amt1 + amt3) - amt2);
        //            } else {
        //                $("#Need_PAY_AMT").val((amt1 + amt3) - amt);
        //                $("#spanPayed").html((amt1 + amt3) - amt);
        //            }
        //        }

        //    }
        //}
            

        $('#btnReturnList').click(function () { window.location.href = "/Card/ChangeCardLevelList.do"; })
        $('#btnReLoad').click(function () { window.location.reload(); })


    })
})(window, undefined, jQuery);