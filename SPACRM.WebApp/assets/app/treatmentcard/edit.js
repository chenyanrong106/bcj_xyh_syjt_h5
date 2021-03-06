﻿; (function (window, undefined, $) {
    var totalAmt = 0;//售价
    var totalOriAmt = 0;//原价
    $(document).ready(function () {
        $('#BEGIN_DATE_picker').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd"
        });
        $('#END_DATE_picker').datepicker({
            language: "zh-CN",
            format: "yyyy-mm-dd"
        });
        
        //保存验证
        $('#frmSave').validator({
            rules: {
            },
            fields: {
                '#NAME': 'required',
                '#PERIOD': 'required;;integer[+0]'

            },
            valid: function (form) {
                FormSubmit(form, function (res) {
                    if (res.status == 0) {
                        if ($("#ID").val() == "0") {
                            $("#flag").val(1);
                            $("#ID").val(res.data);
                        }
                        _showInfoMessage('操作成功！', 'success');
                        $("#foundli").removeClass("active");
                        $("#storeli").addClass("active");
                        $("#BASE_INFO").removeClass("active");
                        $("#STORE").addClass("active");
                        var id = $("#ID").val();
                        InitStoGrid(id);
                    }
                    else {                     
                        _showInfoMessage("操作失败：" + res.message, 'error');                      
                    }
                })
            }
        });

        //保存
        $('#btnSaveFrm').click(function () {
    
            $('#frmSave').submit();
        });




        var sto_grid = null;
        $('#tab_store').on('show.bs.tab', function (e) {
            var id = $("#ID").val();
            if (id == "" || id == "0") {
                alert("请先保存卡片信息！");
                e.preventDefault();
                return false;
            }
        });

            $('#tab_store').on('shown.bs.tab', function (e) {
                var id = $("#ID").val();
                if (id != "" && id != "0") {
                    if (sto_grid == null) {
                        InitStoGrid(id);
                    }

                }
            });
      

        //保存适用门店
        $("#btnStoreSave").click(function () {

            //$("select[@name=STORE_INFO_discounts]").each(function (i) {
            //    alert(i);
            //});
            var id = $("#ID").val();
            //alert(id);
            var url = options.Sto_SaveUrl + "/" + id;
            var data = sto_grid.GetCheckedRowDatas(formatPostStoreData);
            var postData = data.join(",");
            PostSave(postData, url);
        });

        var _submiting = false;
        function PostSave(postData, url) {
            if (_submiting) {
                showErrorTip("请耐心等待服务端操作结束", { left: 400, top: 450 }, true, 5000);
                return;
            }
            _submiting = true;
            showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 400, top: 450 });
            $.ajax({
                url: url,
                type: "POST",
                data: { "formdata": postData },
                success: function (result) {
                    hideLoadingMsg();
                    if (result.status == 0) {
                        //alert("操作成功！");
                        _showInfoMessage("操作成功", 'success');
                        window.location.href = "../../Treatmentcard/Index.do";
                    }
                    else {
                        showErrorTip("操作失败！：" + res.message, { left: 400, top: 450 }, true, 5000);
                    }
                    _submiting = false;
                },
                error: function (result) {
                    hideLoadingMsg();
                    _submiting = false;
                    alert("提交表单失败：" + result);
                }
            });
        }

        function InitStoGrid(id) {
            var gridopt = {
                url: options.Sto_QueryUrl + "/" + id,
                colModel: [
                        { display: '编号', name: 'ID', hide: true, iskey: true },
                        { display: '门店编号', name: 'STORE_NO', align: 'left' },
                        { display: '门店名称', name: 'NAME', align: 'left' },
                        { display: '允许销售', name: 'IS_SALES', align: 'left', process: formatDis3 },
                        { display: '销售最大数', name: 'SALE_MAX', align: 'left', width: "10%", process: formatDis },
                        { display: '允许使用', name: 'IS_USERD', align: 'left', process: formatDis4 },
                        { display: '是否选择', name: 'IsChecked', hide: true }
                ],
                title: false,
                rp: 10,
                localpage: true,
                rowhanlder: rowStoreBind,
                usepager: true,
                showcheckbox: true
            };
            sto_grid = new xjGrid("STORE_INFO", gridopt);
        }


        function rowStoreBind(rowid, cell) {
            var disinput = $(this).find("select");
            var disinput2 = $(this).find("input.discounts_input");
            if (disinput.length > 0) {
                disinput.blur(UpdateBind)
            }
            if (disinput2.length > 0) {
                disinput2.blur(UpdateBind)
            }

            //var disinput = $(this).find("select.form-control option").val();
            //alert(disinput);
        }
        function UpdateBind() {
            var inid = this.id;
            var tar = inid.split("_");
            var rowid = parseInt(tar[tar.length - 1]);
            if (tar[0] == "SERVICE") {//服务列表
                s_grid.UpdateBind(rowid, 2, this.value); //更新到内部
            }
            else if (tar[0] == "PRODUCT") {//产品列表
                p_grid.UpdateBind(rowid, 2, this.value); //更新到内部
            }
            else if (tar[0] == "SALES") {
                //alert(this.value);
                sto_grid.UpdateBind(rowid, 3, this.value); //更新到内部
            }
            else if (tar[0] == "USED") {
                sto_grid.UpdateBind(rowid, 5, this.value); //更新到内部
            }
            else if (tar[0] == "SALEMAX") {
                sto_grid.UpdateBind(rowid, 4, this.value); //更新到内部
            }

        }

        function formatPostStoreData(cell) {
            return [cell[0], cell[3], cell[4], cell[5]].join("_");
            // return [cell[0]];
        }
        function formatDis(value, cell, rowid) {
            if ($("#flag").val() == 1) {
                if (value == 0) {
                    value = 1000;
                }
            }
            sto_grid.UpdateBind(rowid, 4, value);
            return "<input type='text' class='discounts_input' id='SALEMAX_INFO_discounts_" + rowid + "' value='" + value + "'/>";
        }
        function formatDis3(value, cell, rowid) {
            if ($("#flag").val() == 1) {
                value = 1;
                sto_grid.UpdateBind(rowid, 3, value);
                return "<select type='text' class='form-control'  id='SALES_INFO_discounts_" + rowid + "'  ><option  value='1' selected='true'>是</option><option  value='0' >否</option> </select>";

            }
            else if (value == "False") {
                return "<select type='text' class='form-control' id='SALES_INFO_discounts_" + rowid + "'><option  value='1'>是</option><option  value='0' selected='true'>否</option> </select>";

            }
            else

                return "<select type='text' class='form-control'  id='SALES_INFO_discounts_" + rowid + "'  ><option  value='1' selected='true'>是</option><option  value='0' >否</option> </select>";
        }

        function formatDis4(value, cell, rowid) {
            if ($("#flag").val() == 1) {
                value = 1;
                sto_grid.UpdateBind(rowid, 5, value);
                return "<select type='text' class='form-control'  id='USED_INFO_discounts_" + rowid + "' ><option  value='1' selected='true'>是</option><option  value='0' >否</option> </select>";
            }
            else {
                if (value == "False") {
                    return "<select type='text' class='form-control' id='USED_INFO_discounts_" + rowid + "'><option  value='1'>是</option><option  value='0' selected='true'>否</option> </select>";

                }
                else

                    return "<select type='text' class='form-control'  id='USED_INFO_discounts_" + rowid + "' ><option  value='1' selected='true'>是</option><option  value='0' >否</option> </select>";
            }
        }

        //取消
        $('#btnCloseFrm').click(function () {
            window.location.href = options.indexUrl;
        });

        //获取小分类下拉选框列表
        //var _submiting2 = false;
        //function PostGetProdTypes() {
        //    if (_submiting2) {
        //        //showErrorTip("请耐心等待服务端操作结束", { left: 400, top: 450 }, true, 5000);
        //        return;
        //    }
        //    _submiting2 = true;
        //    var type ="2";
        //    //showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 400, top: 450 });
        //    $.ajax({
        //        url: options.proTypeUrl,
        //        type: "POST",
        //        data: { "type": type },
        //        success: function (result) {
        //            hideLoadingMsg();
        //            //类别
        //            var cuAllPs = result.data;
        //            var cuAllPsArray = new Array();
        //            if (cuAllPs != "") {
        //                cuAllPsArray = cuAllPs.split(",");
        //            }

        //            var hl = "<select class=\"form-control\" id=\"PRO_TYPE\" name=\"PRO_TYPE\" onchange=\"util.ProTypeChange()\">";

        //            if (cuAllPs != "") {
        //                for (var i = 0; i < cuAllPsArray.length; i++) {
        //                    var re = cuAllPsArray[i].split("_");
        //                    hl += "<option value=\"" + re[0] + "\">" + re[1] + "</option>";
        //                }
        //            }
        //            hl += "<select>";
        //            $("#pro_type_div").html(hl);

        //            PostGetProItems();

        //            _submiting2 = false;
        //        },
        //        error: function (result) {
        //            hideLoadingMsg();
        //            _submiting2 = false;
        //            alert("页面有异常：" + result);
        //        }
        //    });
        //}


        //项目类别选择改变事件
        $("#TYPE").change(function () {
            PostGetProdTypes2();
        });

        var _submiting3 = false;
        function PostGetProdTypes2() {
            //if (_submiting3) {
            //    showErrorTip("请耐心等待服务端操作结束", { left: 400, top: 450 }, true, 5000);
            //    return;
            //}
            //_submiting3 = true;
            //showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 400, top: 450 });
            var pid = $('#TYPE').val();

            $.ajax({
                url: options.proSerURL,
                type: "POST",
                data: { "PID": pid },
                success: function (result) {

                    //hideLoadingMsg();

                    var cuAllPs = result.data;
                    if (result.data != null && result.data != "undefined" && result.data.length > 0) {

                        var cuAllPsArray_type = new Array();
                        if (cuAllPs != "") {
                            cuAllPsArray_type = cuAllPs.split("@@")[0].split(",");

                        }
                        var cuAllPsArray = new Array();
                        if (cuAllPs != "") {
                            cuAllPsArray = cuAllPs.split("@@")[1].split(",");

                        }
                        //加载小类
                        
                        if (cuAllPsArray_type != "") {
                            var hl = "<select class=\"form-control\" id=\"TYPE2\" name=\"TYPE2\" onchange=\"util.TYPE2ONCHANGE()\">";
                            hl += "<option value=\"\">请选择</option>";
                            for (var i = 0; i < cuAllPsArray_type.length; i++) {
                                var re = cuAllPsArray_type[i].split("_");

                                if (re[0] != "undefined" && re[0] != "") {
                                    hl += "<option value=\"" + re[0] + "\">" + re[1] + "</option>";
                                }
                            }
                            hl += "<select>";
                            $("#pro_type2_div").html(hl);
                        }
                      
                        //加载项目
                        hl = "<select class=\"form-control\" id=\"PRO_ITEMS\" name=\"PRO_ITEMS\"  >";
                        
                        if (cuAllPs != "") {
                            for (var i = 0; i < cuAllPsArray.length; i++) {
                                var re = cuAllPsArray[i].split("_");

                                //if (selectV == re[1])
                                //    hl += "<option value=\"" + re[0] + "\" selected=\"selected\">" + re[1] + "</option>";
                                //else
                                if (re[0] != "undefined" && re[0] != "") {
                                    hl += "<option value=\"" + re[0] + "@" + re[2] + "\">" + re[1] + "</option>";
                                }

                            }
                        }
                        hl += "<select>";
                        $("#COUNT").val("1");
                        $("#PRO_ITEMS").html(hl);
                        if (cuAllPsArray!="") {
                            $("#btnAddOnePro").css("display", "block");
                        } else { $("#btnAddOnePro").css("display", "none"); }
                        
                    } else {
                        $("#btnAddOnePro").css("display", "none");
                        $("#PRO_ITEMS").html("");
                        var hl = "<select class=\"form-control\" id=\"TYPE2\" name=\"TYPE2\" onchange=\"util.TYPE2ONCHANGE()\">";
                        hl += "<option value=\"\">请选择</option>";
                        hl += "<select>";
                        $("#pro_type2_div").html(hl);

                    }
                    //_submiting3 = false;
                }
                ,
                error: function (result) {
                    //hideLoadingMsg();
                    //_submiting3 = false;
                    _showInfoMessage("页面有异常：" + result, 'error');

                }
            });
        }


        util.TYPE2ONCHANGE = function () {
            var pid = $('#TYPE').val();
            var pid2 = $('#TYPE2').val();

            $.ajax({
                url: options.proSerURL2,
                type: "POST",
                data: { "PID": pid, "PID2": pid2 },
                success: function (result) {
                    var cuAllPs = result.data;
                    if (result.data.length > 0) {
                        var cuAllPsArray = new Array();
                        if (cuAllPs != "") {
                            cuAllPsArray = cuAllPs.split(",");

                        }
                        //加载项目
                        var hl = "<select class=\"form-control\" id=\"PRO_ITEMS\" name=\"PRO_ITEMS\"  >";

                        if (cuAllPs != "") {
                            for (var i = 0; i < cuAllPsArray.length; i++) {
                                var re = cuAllPsArray[i].split("_");
                                if (re[0] != "undefined" && re[0] != "") {
                                    hl += "<option value=\"" + re[0] + "@" + re[2] + "\">" + re[1] + "</option>";
                                }

                            }
                        }
                        hl += "<select>";
                        $("#COUNT").val("1");
                        $("#PRO_ITEMS").html(hl);
                        $("#btnAddOnePro").css("display", "block");
                    } else {
                        $("#btnAddOnePro").css("display", "none");
                        $("#PRO_ITEMS").html("");
                    }
                }
                ,
                error: function (result) {
                    _showInfoMessage("页面有异常：" + result, 'error');

                }
            });
        }



        //根据大小类下拉产品类型查询所有产品
        //var _submiting = false;
        //function PostGetProItems() {
        //    if (_submiting) {
        //        showErrorTip("请耐心等待服务端操作结束", { left: 400, top: 450 }, true, 5000);
        //        return;
        //    }
        //    _submiting = true;
        //    showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 400, top: 450 });

        //    var prodtype ="2";
        //    var objtype = $('#PRO_TYPE').val();

        //    $.ajax({
        //        url: options.proItemsUrl,
        //        type: "POST",
        //        data: { "prodtype": prodtype, "objtype": objtype },
        //        success: function (result) {
        //            hideLoadingMsg();
        //            $("#cuAllProInfos").val(result.data);//大类_编号_名称
        //            loadProDdl();//显示产品小类下拉列表                   
        //            _submiting = false;
        //        },
        //        error: function (result) {
        //            hideLoadingMsg();
        //            _submiting = false;
        //            alert("页面有异常：" + result);
        //        }
        //    });
        //}

        ////显示产品小类下拉列表
        //function loadProDdl() {
        //    //当前类别的所有产品:大类_编号_名称
        //    var cuAllPs = $("#cuAllProInfos").val();
        //    var cuAllPsArray = new Array();
        //    if (cuAllPs != "") {
        //        cuAllPsArray = cuAllPs.split(",");
        //    }

        //    //当前选中的产品:产品大类_产品编号_产品名称
        //    var ids = $("#PROD_INFO").val();
        //    var idArray = new Array();
        //    if (ids != "") {
        //        idArray = ids.split(",");
        //    }

        //    var hl = "<select class=\"form-control\" id=\"PRO_ITEMS\" name=\"PRO_ITEMS\">";

        //    if (cuAllPs != "") {
        //        for (var i = 0; i < cuAllPsArray.length; i++) {
        //            if (cuAllPsArray[i] != undefined) {
        //                var re = cuAllPsArray[i].split("_");
        //                if ($.inArray(cuAllPsArray[i], idArray) == -1)
        //                    hl += "<option value=\"" + re[2] + "\">" + re[3] + "</option>";
        //            }
        //        }
        //    }
        //    hl += "<select>";
        //    $("#pro_items_div").html(hl);
        //}

        //产品小类选择改变
        //util.ProTypeChange = function () {
        //    PostGetProItems();
        //}

        //大类转换
        function formatCP(value) {
            if (value == 0) {
                return "实物产品";
            }
            else if (value == 1) {
                return "卡";
            }
            else if (value == 2) {
                return "项目";
            }
        }

        //展示选中产品列表
        var prodInfos;
        function showSelectProdList() {
            totalAmt = 0;
            totalOriAmt = 0;
            var prodInfoStr = $("#PROD_INFO").val();//对应的产品：产品大类_产品小类_产品编号_产品名称_数量_折后单价_是否必选_折扣_金额

            if (prodInfoStr != "") {
                prodInfos = prodInfoStr.split(",");
                var proListHtml = [];
                for (var i = 0; i < prodInfos.length; i++) {
                    var cuObj = prodInfos[i].split("_");
                    if (i % 2 == 0)
                        proListHtml.push("<tr>");
                    else
                        proListHtml.push("<tr class='strip'>");
                    var amt = cuObj[9];
                    var ischeck = true;
                   
                    if (cuObj[6] == 'True' || cuObj[6] == '1')
                    {
                        proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='checkbox' name='Isselect' checked='checked'  onclick=\"util.onCheckChange(this," + amt + ",'", prodInfos[i], "',this.checked)\"></div></td>");//是否必选
                    } else {
                        proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='hidden' name='proc_id' value='" + cuObj[2] + "'><input type='checkbox' name='Isselect'  onclick=\"util.onCheckChange(this," + amt + ",'", prodInfos[i], "',this.checked)\"></div></td>");//是否必选
                    }
                    proListHtml.push("<td style=\"width: 5%;\"><div style=\"text-align:left;\">", formatCP(cuObj[0]), "</div></td>");//类型
                    proListHtml.push("<td style=\"width: 20%;\"><div style=\"text-align:left;\">", cuObj[3], "</div></td>");//产品名称
                    proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\">", amt, "</div></td>");//标准价
                    proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='text' name='num' style='width:50px;'  value='" + cuObj[4] + "'  onchange = \"util.onChangeNum(this," + amt + ",'", prodInfos[i], "','", i, "')\"  ></div></td>");//数量this.id,"+i+"
                    proListHtml.push("<td style=\"width: 15%;\"><div style=\"text-align:left;\"><input type='text' name='discount' style='width:50px;' value='" + cuObj[7] + "'  onchange = \"util.onChangeDiscount(this," + amt + ",'", prodInfos[i], "','", i, "')\"  ></div></td>");//折扣
                    proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='text' name='price' style='width:80px;' value='" + cuObj[5] + "' onchange = \"util.onChangePrice(this," + amt + ",'", prodInfos[i], "','", i, "')\"  ></div></td>");//折后价
                    proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><input type='text' name='amt' style='width:80px;' value='" + cuObj[8] + "'  onchange = \"util.onChangeAmt(this," + amt + ",'", prodInfos[i], "','", i, "')\"      ></div></td>");//金额//onkeypress=\"util.keyPress(this)\"  onblur=\"util.onBlur(this)\"
                    proListHtml.push("<td style=\"width: 10%;\"><div style=\"text-align:left;\"><a title='移除' class='abtn' href='javascript:;' onclick=\"util.Remove('", prodInfos[i], "')\"><i class='fa fa-trash-o' ></i> 移除</a></div></td>");
                    proListHtml.push("</tr>");
                    totalAmt = parseFloat(parseFloat(totalAmt) + parseFloat(cuObj[8]));

                    totalOriAmt = parseFloat(parseFloat(totalOriAmt) + parseFloat(cuObj[9] * cuObj[4]));
                }
                
                $("#tableProds tbody").html("");
                $("#tableProds tbody").html(proListHtml.join(""));
                
                totalAmt = Math.round(parseFloat(totalAmt));
                $("#PRICE").val(totalAmt);

                totalOriAmt = Math.round(parseFloat(totalOriAmt));
                $("#PAR_AMT").val(totalOriAmt);

                $("#tableProds tr").each(function (i) {
                    $(this).hover(function (e) { $(this).addClass("mhover"); }, function (e) { $(this).removeClass("mhover"); });
                });

            }
            else {
                totalAmt = Math.round(parseFloat(totalAmt));
                $("#PRICE").val(totalAmt);

                totalOriAmt = Math.round(parseFloat(totalOriAmt));
                $("#PAR_AMT").val(totalOriAmt);
                $("#tableProds tbody").html("");
            }
        }
        showSelectProdList();


        //添加产品
        var _addsubmiting = false;
        $("#btnAddOnePro").click(
         function () {
             var Pro_value = $("#PRO_ITEMS").val();
             var sId = Pro_value.split('@')[0];//下拉列表选中值
             var sText = $("#PRO_ITEMS").find("option:selected").text();
             var sAmt = Pro_value.split('@')[1];
             var sCount = $("#COUNT").val();
             if (sCount == null || sCount == "") {
                 sCount = 0
             }


             var sIds = $("#PROD_INFO").val();//对应的产品：产品大类_产品小类_产品编号_产品名称_数量_折后单价_是否必选_折扣_金额
             //判断是否已经有产品
             var IsHave=false;//是否有相同的疗程
             if (sIds != "") {
                 var str;
                 var prodInfos = sIds.split(",");
                 var proListHtml = [];
                 for (var i = 0; i < prodInfos.length; i++) {
                     var cuObj = prodInfos[i].split("_");
                     if (sId == cuObj[2])//找到相同的编号
                     {
                         str = "2_" + $("#PRO_TYPE").val() + "_" + sId + "_" + sText + "_" + (parseInt(sCount) + parseInt(cuObj[4])) + "_" + parseFloat(sAmt).toFixed(2) + "_1" + "_" + parseFloat(cuObj[7]).toFixed(2) + "_" + parseFloat(cuObj[7] * sAmt * (parseInt(sCount) + parseInt(cuObj[4]))).toFixed(2) + "_" + sAmt;
                      

                         prodInfos.splice(i, 1, str);
                         IsHave = true;


                         var sIds = prodInfos.join(",");

                         $("#PROD_INFO").val(sIds);
                         showSelectProdList();
                         break;
                     }
                 }
             }

             if (!IsHave) {
                 if (sId != undefined) {
                     if (sIds == "")
                         sIds = "2_" + $("#PRO_TYPE").val() + "_" + sId + "_" + sText + "_" + sCount + "_" + parseFloat(sAmt).toFixed(2) + "_1" + "_1.00" + "_" + parseFloat(sAmt * sCount).toFixed(2) + "_" + sAmt;
                     else
                         sIds += ",2_" + $("#PRO_TYPE").val() + "_" + sId + "_" + sText + "_" + sCount + "_" + parseFloat(sAmt).toFixed(2) + "_1" + "_1.00" + "_" + parseFloat(sAmt * sCount).toFixed(2) + "_" + sAmt;

                     if (_addsubmiting) {
                         //showErrorTip("请耐心等待服务端操作结束", { left: 400, top: 450 }, true, 5000);
                         return;
                     }
                     _addsubmiting = true;
                     //showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 400, top: 450 });

                    
                     //loadProDdl();
                     _addsubmiting = false;
                     $("#PROD_INFO").val(sIds);
                     showSelectProdList();
                 }
                 else {
                     _showInfoMessage("请先选择产品！", 'success');

                 }
             }

            
           
         }
         );

        //根据"产品大类_产品编号_产品名称 "去掉产品
        var _removesubmiting = false;
        util.Remove = function (pro) {

            var sIds = $("#PROD_INFO").val();//当前选中的:产品大类_产品编号_产品名称             
            var sPros = sIds.split(",");//需要去掉的"产品大类_产品编号_产品名称 "

            var removeIndex = $.inArray(pro, sPros);
            sPros.splice(removeIndex, 1);
        
            var reSIds = sPros.join(",");
            if (_removesubmiting) {
                //showErrorTip("请耐心等待服务端操作结束", { left: 400, top: 450 }, true, 5000);
                return;
            }
            _removesubmiting = true;
            //showLoadingMsg("正在努力地请求服务端,请稍后...", { left: 400, top: 450 });
            $("#PROD_INFO").val(reSIds);
            showSelectProdList();
            //loadProDdl();
            _removesubmiting = false;
        }


        $("#SelectOrNot").change(function () {
            var v = $("#SelectOrNot").val();
            if (v == "1") {
                $("#show").show();

            }
            else {
                $("#show").hide();
            }
        })

      
        util.onChangeNum = function (objectid, p_amt, pro, i) {
            var num = $(objectid).val();//数量
            if (!/^[0-9]*$/.test(num)) {
                alert("请输入整数!");
                $(objectid).val("");
                $(objectid).focus();
                return;
            }

            var discount = $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=discount]").val();//折扣
            $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=price]").val((p_amt * discount));
            $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=amt]").val((num * p_amt * discount));
            //对应的产品：产品大类_产品小类_产品编号_产品名称_数量_折后单价_是否必选_折扣_金额_标准价
            var str = "";
            var arrStr = pro.split("_");

            str = arrStr[0] + "_" + arrStr[1] + "_" + arrStr[2] + "_" + arrStr[3] + "_" + num + "_" + parseFloat(p_amt * discount).toFixed(2) + "_" + (arrStr[6] == 'True' ? 1 : 0) + "_" + parseFloat(discount).toFixed(2) + "_" + parseFloat(num * p_amt * discount).toFixed(2) + "_" + p_amt;

            var sIds = $("#PROD_INFO").val();//当前选中的:产品大类_产品编号_产品名称             
            var sPros = sIds.split(",");//需要去掉的"产品大类_产品编号_产品名称 "

            var removeIndex = $.inArray(pro, sPros);

            sPros.splice(removeIndex, 1, str);
            var reSIds = sPros.join(",");
            $("#btnSaveFrm").focus();
            $("#PROD_INFO").val(reSIds);
            showSelectProdList();

        }


        util.onChangeDiscount = function (objectid, p_amt, pro, i) {
            var discount = $(objectid).val();//折扣

            //验证有两位小数的正实数：^[0-9]+(.[0-9]{2})?$ 
            //^[0-9]+([.]{1}[0-9]+){0,1}$
            var reg = new RegExp("^[0](.[0-9]{0,2})?$");
            if (!reg.test(discount) && discount!=1) {
                alert("输入小于等于1的小数并且最多保留两位小数的数!");
                $(objectid).val("");
                $(objectid).focus();
                return;
            }
            if (discount>1)
            {
                alert("输入小于等于1的小数并且最多保留两位小数的数!");
                $(objectid).val("");
                $(objectid).focus();
                return;
            }
            var num = $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=num]").val();//数量

            $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=price]").val(p_amt * discount);
            $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=amt]").val(num * p_amt * discount);

      
            //对应的产品：产品大类_产品小类_产品编号_产品名称_数量_折后单价_是否必选_折扣_金额_标准价

            var str = "";
            var arrStr = pro.split("_");

            str = arrStr[0] + "_" + arrStr[1] + "_" + arrStr[2] + "_" + arrStr[3] + "_" + num + "_" + parseFloat(p_amt * discount).toFixed(2) + "_" + (arrStr[6] == 'True' ? 1 : 0) + "_" + parseFloat(discount).toFixed(2) + "_" + parseFloat(num * p_amt * discount).toFixed(2) + "_" + p_amt;

            var sIds = $("#PROD_INFO").val();//当前选中的:产品大类_产品编号_产品名称             
            var sPros = sIds.split(",");//需要去掉的"产品大类_产品编号_产品名称 "

            var removeIndex = $.inArray(pro, sPros);
            sPros.splice(removeIndex, 1, str);
            var reSIds = sPros.join(",");
            $("#btnSaveFrm").focus();
            $("#PROD_INFO").val(reSIds);
            showSelectProdList();
        }

        util.onChangePrice = function (objectid,p_amt, pro, i) {

            var Price = $(objectid).val();//折后价
            var reg = new RegExp("^[0-9]+([.]{1}[0-9]+){0,1}$");
            if (!reg.test(Price)) {
                alert("输入最多保留两位小数的数!");
                $(objectid).val("");
                $(objectid).focus();
                return;
            }//
            var num = $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=num]").val();//数量
            var discount = $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=discount]").val();
            $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=amt]").val(num * Price);

   
            //对应的产品：产品大类_产品小类_产品编号_产品名称_数量_折后单价_是否必选_折扣_金额_标准价
   
            var str = "";
            var arrStr = pro.split("_");

            str = arrStr[0] + "_" + arrStr[1] + "_" + arrStr[2] + "_" + arrStr[3] + "_" + (num) + "_" + parseFloat(Price).toFixed(2) + "_" + (arrStr[6] == 'True' ? 1 : 0) + "_" + parseFloat(discount).toFixed(2) + "_" + parseFloat(num * Price).toFixed(2) + "_" + p_amt;


            var sIds = $("#PROD_INFO").val();//当前选中的:产品大类_产品编号_产品名称             
            var sPros = sIds.split(",");//需要去掉的"产品大类_产品编号_产品名称 "

            var removeIndex = $.inArray(pro, sPros);

            sPros.splice(removeIndex, 1, str);
            var reSIds = sPros.join(",");
            $("#btnSaveFrm").focus();
            $("#PROD_INFO").val(reSIds);
            showSelectProdList();
            
        }

        util.ontextchange = function ()
        {
            showSelectProdList();
        }

        util.onChangeAmt = function (objectid, p_amt, pro, i) {

            if (!objectid.value.match(/^[\+\-]?\d*?\.?\d*?$/))
                objectid.value = objectid.t_value;
            else objectid.t_value = objectid.value;
            if (objectid.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
                objectid.o_value = objectid.value;
            try {
                if (objectid.value == null || objectid.value == "") {
                    objectid.value = 0;
                    return false;
                }
                
            }
            catch (ex) {
             //   _showInfoMessage("金额填写有误！" + ex.message, "error");
            }


            var amt = $(objectid).val();//金额
            var reg = new RegExp("^[0-9]+([.]{1}[0-9]+){0,1}$");
            if (!reg.test(amt)) {
                alert("输入最多保留两位小数的数!");
                $(objectid).val("");
                $(objectid).focus();
                return;
            }
            var Price = $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=price]").val();
            var num = $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=num]").val();//数量
            var discount = $(objectid).parent("div").parent("td").parent("tr").find("input[NAME$=discount]").val();
            //对应的产品：产品大类_产品小类_产品编号_产品名称_数量_折后单价_是否必选_折扣_金额_标准价
            var str = "";
            var arrStr = pro.split("_");
            str = arrStr[0] + "_" + arrStr[1] + "_" + arrStr[2] + "_" + arrStr[3] + "_" + (num) + "_" + parseFloat(Price).toFixed(2) + "_" + (arrStr[6] == 'True' ? 1 : 0) + "_" + parseFloat(discount).toFixed(2) + "_" + parseFloat(amt).toFixed(2) + "_" + p_amt;
            var sIds = $("#PROD_INFO").val();//当前选中的:产品大类_产品编号_产品名称             
            var sPros = sIds.split(",");//需要去掉的"产品大类_产品编号_产品名称 "
            var removeIndex = $.inArray(pro, sPros);

            sPros.splice(removeIndex, 1, str);

            var reSIds = sPros.join(",");
            $("#btnSaveFrm").focus();
            $("#PROD_INFO").val(reSIds);
            showSelectProdList();

            //var cuObj = pro.split("_");
            //totalAmt = parseFloat(parseFloat(totalAmt) - parseFloat(cuObj[8]));
            //totalAmt = parseFloat(parseFloat(totalAmt) + parseFloat(amt));
            //totalAmt = Math.round(parseFloat(totalAmt));
            //$("#PRICE").val(totalAmt);
        }

        util.onCheckChange = function (object, p_amt, pro,check)
        {

           

            var num = $(object).parent("div").parent("td").parent("tr").find("input[NAME$=num]").val();//数量
            var discount = $(object).parent("div").parent("td").parent("tr").find("input[NAME$=discount]").val();
            var Price = $(object).parent("div").parent("td").parent("tr").find("input[NAME$=price]").val();
            var amt = $(object).parent("div").parent("td").parent("tr").find("input[NAME$=amt]").val();
            //var Price = discount * p_amt;//折后价
            var isselect = 0;
            //对应的产品：产品大类_产品小类_产品编号_产品名称_数量_折后单价_是否必选_折扣_金额
            if (check) {
                isselect = 1;
            } else {
                isselect = 0;
            }
            var str = "";
            var arrStr = pro.split("_");

            str = arrStr[0] + "_" + arrStr[1] + "_" + arrStr[2] + "_" + arrStr[3] + "_" + (num) + "_" + parseFloat(Price).toFixed(2) + "_" + isselect + "_" + parseFloat(discount).toFixed(2) + "_" + parseFloat(amt).toFixed(2) + "_" + p_amt;
            var sIds = $("#PROD_INFO").val();//当前选中的:产品大类_产品编号_产品名称             
            var sPros = sIds.split(",");//需要去掉的"产品大类_产品编号_产品名称 "
            var removeIndex = $.inArray(pro, sPros);

            sPros.splice(removeIndex, 1, str);
            var reSIds = sPros.join(",");

            $("#PROD_INFO").val(reSIds);
            showSelectProdList();
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


        ////项目类别选择改变事件
        //$("#TYPE").click(function () {

        //    if ($("#confirmModal").css("display") == "none") {
        //        //$("#showSelect").html("关闭");
        //        $('#confirmModal').show();
        //        $('#confirmModal').css("width", $('#TYPE').css("width"));
        //    }
        //    else {
        //        //$("#showSelect").html("选择");
        //        $('#confirmModal').hide();
        //    }
        //});



        //$("#ptree").treeview({
        //    url: options.queryLeftUrl,
        //    showcheck: true,
        //    cascadecheck: true,
        //    onnodeclick: TreeNode_Click
        //});

        //function TreeNode_Click(data) {
           
        //    $("#h4Dict").html(data.text);
        //    $("#PARENT_ID").val(data.id);
        //    $("#TYPE").val(data.text);
        //    $("#PROD_TYPE").val(data.pid);
        //    $('#confirmModal').hide();
        //    PostGetProdTypes2();
        //}
    });
})(window, undefined, jQuery);