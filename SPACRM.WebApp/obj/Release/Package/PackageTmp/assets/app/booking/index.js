
; (function (window, undefined, $) {
    $("#STORE_Id option[value='']").remove();
    var models = GetColModels(initxjcal);
    var width = document.documentElement.clientWidth - 380;//old -10  
    var height = document.documentElement.clientHeight-210;//-210
    //$("#STORE_Id").append("<option value=''>请选择门店</option>");
    function initxjcal(models) {
        util.xjcal = new xjCalendar("calendar", {
            height: false, //视图的高度，如果不设置则默认获取所在页面的高度
            url: options.calReqHandlerUrl, //请求数据的Url  
            width: width,
            height: height,
            eventItems: [],
            colModels: models,//横向栏目
            rowRange: { start: options.startHour, end: options.endHour },// 纵向时间范围 默认为上午11点到晚上22点  
            onDragAdd: onDragEventAdd,
            onBindEvent: BindTip,
            onItemShow: false,
            theme:14,
            onChipToolClick: onChipToolClickHandler,
            onFilterData: FilterData,
            extParam: [{ name: "subid", value: $("#STORE_Id").val() }],
            autoLoad: true//自动加载，如果eventItems参数没有配置，可启用该参数，默认第一次展现时        
        });
    }
    function GetColModels(cb) {
        var type = $("#slColType").val();
        var mid = $("#MasseurId").val();
        var showdate = $("#hdshowday").val();
        var storeid = $("#STORE_Id").val();

        if (storeid == null || storeid == "") {
            storeid = $("#hideSTORE_ID").val();
        }

        $.post(options.getModelsUrl, { "type": type, "mid": mid, "storeid": storeid, "showdate": showdate },
                  function (ret) {
                      if (ret && ret.status == 0) {
                          cb(ret.data);
                      }
                      else {
                          alert("获取横向坐标出错!");
                      }
                  },
                  "json"
            );
    }
    /**
     * 当拖拽时快速新增的方法
     * @param  {Object} item 拖拽时所产生的数据包括开始时间 结束时间等
     */
    function onDragEventAdd(item) {
        var mid, date, subid,subName;
        subid = $("#STORE_Id").val();//门店
        subName = $("#STORE_Id").find("option:selected").text();
        var type = $("#slColType").val();//是否指定技师
        if (type == "1") {
            mid = item.ColId;//技师ID 
        }
        else {
            //未指定技师
            mid = $("#MasseurId").val();
            //date = item.ColId;
        }
        date = item.OrderDate;//预约日期
        var starttime = StrToDate(date + " " + item.OST + ":00");
        if (starttime < new Date()) {
            //showErrorTip("预约开始时间小于当前时间哦0~", { right: 100, top: 5 }, true, 5000);
            //return false;
            if(!window.confirm("预约开始时间小于当前时间，是否确定预约？")){
                return false;
            }
        }
        //string mid,string subid, string date, string starthm, string endhm

        //var url = StrFormatNoEncode("{6}?mid={0}&subid={1}&date={2}&starthm={3}&endhm={4}&subName={5}", [mid, subid, date, item.OST, item.OET,subName,"/Booking/Edits.do"]);
        //console.log(url);
       // var url = "/Booking/Edits.do/4?_=1398673493981";
        ////window.xjDailog.Open(url, {
        ////    width: 650,
        ////    height: 570,
        ////    caption: '预约登记',
        ////    theme: "simple", //默认主题
        ////    onclose: function (userstate) {
        ////        //xjgrid.Reload();
        ////        util.xjcal.reload();
        ////    } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
        ////});
        //新预约
        //$('#EditModal').modal('show');
        var url = StrFormatNoEncode("{6}?mid={0}&subid={1}&date={2}&starthm={3}&endhm={4}&subName={5}", [mid, subid, date, item.OST, item.OET, subName, "/Booking/New.do"]);
        window.xjDailog.Open(url, {
            width: 600,
            height: 600,
            caption: '预约登记',
            theme: "simple", //默认主题
            onclose: function (userstate) {
                //xjgrid.Reload();
                util.xjcal.reload();
            } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
        });
    }
    function onChipToolClickHandler(type,item) {
        //alert(item.Idx+"mao");
        return false;
        switch (type) {
            case "1":// 跳转到客户360
                window.location.href = options.view360Url + "?cid=" + item.Idx;
                break;
            case "2":// 开单
                UserCheckIn(item);
                break;
            case "3"://查看业务单
                location.href = options.viewBillUrl + "?bid=" + item.Idx;
                break;
            case "4"://电话确认
                ConfirmBooking(item);
                break;
            case "5"://修改预约                
                EditBooking(item.Idx);
                break;
            case "6"://取消预约
                CancelBooking(item);
                break;
        }
    }
    //关闭Tip
    util.closeTip=function () {
        $(this).tipTip({
            activation: "close"
        });
    }
    function BindTip(item) {
       
        //console.log(item);
        var content = [];
        content.push("<div id='tiptip_content2'>");
        content.push("<div id='tiptipTitle'>");
        content.push("<div><button id='btnClose' style='padding:5px;' onclick='util.closeTip()'  type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;&nbsp;</button></div>");
        content.push("<div style='background-color:#DDF4FA;width:100%;height:35px;'><div style='padding:5px;'>&nbsp;<i class='fa fa-user'></i> <a href='/Customer/View360.do?cid=" + item.CustId + "'>" + item.UserFullName + "</a> </div></div>");//<i class='fa fa-pencil-square-o'></i>
        content.push("</div>");

        content.push("<div id='tiptip_contentTip'>");
        //content.push("<div><b>", item.UserFullName, "</b></div>");
        content.push("<div>电 话：", item.MobilePhone, "</div>");
        content.push("<div>卡 号：", item.UserCard, "</div>");
        content.push("<div>时 间：", item.OrderDate, " ", item.OST, "-", item.OET, "</div>");
        content.push("<div>类 别：", item.ItemName, "</div>");
        content.push("<div>房 间：", item.BedName, "</div>");
        content.push("<div>备 注：", item.Remark, "</div>");
        content.push("</div>");

        

        var array = new Array(item.Idx,item.CustId,item.OrderDate,item.OST,item.OET);
       
        content.push("<div id='tiptipFoot'>");
        content.push("<div id='tiptipFootContent'>");

        if (item.Is_Confirm=="true") {
            //content.push("<div style='float:left;margin-left:0px;'><button type='button' disabled='disabled' style='cursor:not-allowed;' class='btn-default btn-sm btn1'><i class='fa fa-phone'></i> 已确认</button></div>");
        }
        else if (item.Is_Confirm == "false") {
            content.push("<div style='float:left;margin-left:0px;'><button type='button' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "',1)\"  id='btnPhone' class='btn-info btn-sm btn1'><i class='fa fa-phone'></i> 电话确认</button></div>");
        }
        //var style="  class='btn-info btn-sm btn2' ";
        //if (item.Status == "1") {
        //    //已开单
        //    content.push("<div style='float:left;margin-left:0px;'><button type='button' class='btn-info btn-sm btn2' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "', 4)\" ><i class='fa fa-search-plus'></i> 查看订单</button></div>");
        //}
        //else {
        //    content.push("<div style='float:left;margin-left:0px;'><button type='button' class='btn-info btn-sm btn2' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "', 2)\" ><i class='fa fa-legal'></i> 开单</button></div>");
        //    content.push("<div style='float:left;margin-left:0px;'><button type='button' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "',9)\" class='btn-info btn-sm btn4'> <i class='fa fa-mail-reply-all'></i> 取消预约</button></div>");
        //}
 
        if (item.Status == "0")
        {
            //新预约
            content.push("<div style='float:left;margin-left:0px;'><button type='button' class='btn-info btn-sm btn2' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "', 2)\" ><i class='fa fa-legal'></i> 开单</button></div>");
            content.push("<div style='float:left;margin-left:0px;'><button type='button' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "',3)\" class='btn-info btn-sm btn4'> <i class='fa fa-pencil-square-o'></i> 编辑</button></div>");
            content.push("<div style='float:left;margin-left:0px;'><button type='button' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "',9)\" class='btn-info btn-sm btn4'> <i class='fa fa-mail-reply-all'></i> 取消预约</button></div>");

        }
        else if (item.status=="1")
        {
            //已开单
            content.push("<div style='float:left;margin-left:0px;'><button type='button' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "',3)\" class='btn-info btn-sm btn4'> <i class='fa fa-pencil-square-o'></i> 编辑</button></div>");
            content.push("<div style='float:left;margin-left:0px;'><button type='button' class='btn-info btn-sm btn2' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "', 5)\" ><i class='fa fa-search-plus'></i> 查看订单</button></div>");
        }
        else if (item.Status == "2")
        {
            content.push("<div style='float:left;margin-left:0px;'><button type='button' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "',3)\" class='btn-info btn-sm btn4'> <i class='fa fa-pencil-square-o'></i> 编辑</button></div>");
            //已结账
            content.push("<div style='float:left;margin-left:0px;'><button type='button' class='btn-info btn-sm btn2' onclick=\"util.bookStatus(" + item.Idx + "," + item.CustId + ",'" + item.MasseurId + "','" + item.OrderDate + "','" + item.OST + "','" + item.OET + "', 5)\" ><i class='fa fa-search-plus'></i> 查看订单</button></div>");
        }

        else if (item.Status == "8") {
            //已爽约
        }
        else if (item.Status == "7") {
            //未约进
        }

        

        

        content.push("</div>");

        content.push("</div>");

        content.push("</div>");
        //if (item.Remark) {
        //    content.push("<div>备 注：", item.Remark, "</div>");
        //}
        if (item.CusReqMasseur == "true") {
            content.push("<div>指定技师</div>");
        }
        $(this).tipTip({
            defaultPosition: "right",
            activation: "click",
            content: content.join("") // HTML or String to fill TipTIp with           
        });
    }

    util.bookStatus = function (id,cid,colid,date,OST,OET,status) {
       
        var mid,subid, subName;
        subid = $("#STORE_Id").val();//门店
        subName = $("#STORE_Id").find("option:selected").text();
        var type = $("#slColType").val();//是否指定技师
        if (type == "1") {
            mid = colid;//技师ID 
        }
        else {
            //未指定技师
            mid = $("#MasseurId").val();
        }
       
 
        if (status == "2") {
        
            //开单
            window.location.href = "/Order/OrderD.do?cid=" + cid + "&bid=" + id;
            return false;
        }
        else if (status == "3")
        {
            //编辑
            util.closeTip();
            var url = StrFormatNoEncode("{6}?id={7}&mid={0}&subid={1}&date={2}&starthm={3}&endhm={4}&subName={5}", [mid, subid, date, OST, OET, subName, "/Booking/New.do",id]);
            window.xjDailog.Open(url, {
                width: 600,
                height: 600,
                caption: '预约登记',
                theme: "simple", //默认主题
                onclose: function (userstate) {
                    //xjgrid.Reload();
                    util.xjcal.reload();
                } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
            });
            return false;
        }
        else if (status == "5")
        {
            //查看订单
            //开单
            window.location.href = "/Order/OrderD.do?cid=" + cid + "&oid=" + id;
            return false;
        }
        else if (status == "1")
        {

        }

        //if (!confirm("您确认执行该操作吗？")) {
        //    return false;
        //}
        $.post(options.setStatusUrl, { ID: id, STATUS: status },
                   function (ret) {
                       if (ret.status > 0) {
                           //alert("操作成功！");
                           //util.closeTip();
                           //util.xjcal.reload();
                           _showInfoMessage('操作成功！', 'success');
                           setInterval(closeWindow, 1000);
                       }
                   },
                   "json"
                  );
        //关闭
        function closeWindow()
        {
            util.closeTip();
            util.xjcal.reload();
        }
        //closeTip();
        //新预约
        //$('#EditModal').modal('show');
        //var url = StrFormatNoEncode("{6}?mid={0}&subid={1}&date={2}&starthm={3}&endhm={4}&subName={5}", [mid, subid, date, OST, OET, subName, "/Booking/New.do"]);
        //window.xjDailog.Open(url, {
        //    width: 600,
        //    height: 600,
        //    caption: '预约登记',
        //    theme: "simple", //默认主题
        //    onclose: function (userstate) {
        //        //xjgrid.Reload();
        //        util.xjcal.reload();
        //    } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
        //});

    }

    function FilterData(data) {
        var showdata = [];
        //var forcedata = [];
        //var canceldata = [];
        //var nocomedata = [];
        for (var i = 0, l = data.length; i < l; i++) {
            if (data[i].Status == 8) // 预约不进
            {
                //forcedata.push(data[i]);
            }
            else if (data[i].Status == 9) //已取消
            {
                // canceldata.push(data[i]);
            }
            else if (data[i].Status == 7) //爽约
            {
                //nocomedata.push(data[i]);
            }
            else {
                showdata.push(data[i]);
            }
        }
        // 已取消左侧
        //setTimeout(function (e) {
        //    RenderLeftList(forcedata, canceldata, nocomedata);
        //}, 500);
        return showdata;
    }

    $('#pdateshow').datepicker({
        language: "zh-CN",
        format: "yyyy-mm-dd",
        todayBtn: true,
        pickerPosition: "bottom-left",
        minView: 2,
        autoclose: true
    }).on('changeDate', function (ev) {
        RefreshView();
    });
   
    $("#btnShowFailList").click(function (e) {
        var url = "/Booking/FailList.do";
        PopupWindow(url, 1300, 600, true, "yes");
    });

    $("#btnShowWxList").click(function (e) {
        var url = "/Booking/WxList.do";
        PopupWindow(url, 1300, 600, true, "yes");
    });

    $("#slColType").change(RefreshView);

    $("#MasseurId").change(function (e) {
        var type = $("#slColType").val();
        if (type == 2) {
            RefreshView();
        }
    });
    var subelement = $("#SubBranchId");
    if (subelement.length == 1 && subelement[0].tagName.toLowerCase() == "select") {
        subelement.change(function (e) {
            var v = $(this).val();
            $.post(
               options.getResourceUrl,
               { code: "Masseur", parentCode: v, alloption: 1 },
               function (res) {
                   if (res != null) {
                       var oplist = [];
                       for (var i = 0, l = res.length; i < l; i++) {
                           oplist.push("<option value='", res[i].Value, "'>", res[i].Name, "</option>");
                       }
                       $("#MasseurId").html(oplist.join(""));
                   }
                   var type = $("#slColType").val();
                   if (type == 1) {
                       RefreshView();
                   }
               },
               "json"
           );
        });
    }
    function RefreshView() {
        var type = $("#slColType").val();
        var p = { "viewtype": type };
        var extp = [];
        var subid = $("#STORE_Id").val();
        if (typeof (subid) == "undefined" || subid == "") {
            showErrorTip("门店为空或不存在", { right: 100, top: 5 }, true, 5000);
            return false;
        }
        extp.push({ name: "subid", value: subid });
        var showdate = $("#hdshowday").val();
        if (showdate == "") {
            showErrorTip("当前日期获取错误", { right: 100, top: 5 }, true, 5000);
            return false;
        }
        p.showDay = StrToDate(showdate);

        if (type == 1) { //按技师多个选择
            // 1 判断技师列表是否为空，门店是否为空                      
            if ($("#MasseurId option").length == 0) {
                showErrorTip("护理师列表为空", { right: 100, top: 5 }, true, 5000);
                return false;
            }
        }
        else {
            // 1 判断当前技师是否为空,门店是否为空                
            var mid = $("#MasseurId").val();
            if (mid == "") {
                showErrorTip("请选择护理师", { right: 100, top: 5 }, true, 5000);
                return false;
            }
            extp.push({ name: "mid", value: mid });
        }
        GetColModels(function (models) {
            p.colModels = models;

            p.extParam = extp;
            util.xjcal.reload(p);
        });

    }
    function oncalendarchange(date) {
        var strdate = date.Format("yyyy年M月d日");
        var type = $("#slColType").val();
        if (type == 1) {
            $("#pdateshow").html(strdate);
        }
        else {
            var enddate = DateAdd("d", 7, date);
            var format;
            if (enddate.getFullYear() != date.getFullYear()) {
                format = "yyyy年M月d日";
            }
            else {
                if (enddate.getMonth() != date.getMonth()) {
                    format = "M月d日";
                }
                else {
                    format = "d日";
                }
            }
            $("#pdateshow").html(strdate + " 至 " + enddate.Format(format));
        }
        $("#hdshowday").val(date.Format("yyyy-MM-dd"));
        RefreshView();
    }
    function UserCheckIn(item) {
        var cardtype = item.UserCardType;
        if (cardtype == 1) { // 会员的话，登录
            location.href = options.checkInUrl + "?memberid=" + item.MemberId + "&bid=" + item.Idx + "&cardno=" + item.UserCard + "&roomid=" + item.BedId;
            /* 会员登录逻辑去除
            var url = options.loginUrl + "?usercard=" + item.UserCard + "&memberid=" + item.MemberId + "&bid=" + item.Idx + "&roomid=" + item.BedId;
            window.xjDailog.Open(url, {
                width: 500,
                height: 280,
                caption: '会员登录',
                theme: "simple" //默认主题
            });
            */
        }
        else { //非会员的话 填写一些信息
            location.href = options.tempLoginUrl + StrFormat("?fullname={0}&idcard={1}&phone={2}&memberid={3}&bid={4}&cardno={5}&roomid={6}",
                [item.UserFullName, item.IdCard, item.MobilePhone, item.MemberId, item.Idx, item.UserCard, item.BedId]);
        }
    }
    util.EditBooking = EditBooking;
    function EditBooking(idx) {
        var url = options.editUrl + "/" + idx;
        window.xjDailog.Open(url, {
            width: 680,
            height: 600,
            caption: '修改预约',
            theme: "simple", //默认主题
            onclose: function (userstate) {
                util.xjcal.reload();
            } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
        });
    }
    function CancelBooking(item) {
        if (confirm("确认要取消这条预约记录吗？")) {
            $.post(options.setStatusUrl, { idx: item.Idx, type: 1 },
                function (res) {
                    if (res.status == 0) {
                        util.xjcal.reload();
                    }
                    else {
                        showErrorTip("操作失败！：" + res.message, { right: 100, top: 10 }, true, 5000);
                    }
                },
                "json"
             );
        }
    }
    function ConfirmBooking(item) {
        if (confirm("顾客[" + item.UserFullName + "]的这条预约，已电话确认？")) {
            $.post(options.setStatusUrl, { idx: item.Idx, type: 2 },
                function (res) {
                    if (res.status == 0) {
                        util.xjcal.reload();
                    }
                    else {
                        showErrorTip("操作失败！：" + res.message, { right: 100, top: 10 }, true, 5000);
                    }
                },
                "json"
             );
        }
    }
    var newlist = [];
    var index = 0;
    function LoadNewList() {
        var subid = $("#SubBranchId").val();
        $.post(options.newListUrl + "?subid=" + subid, { subid: subid },
               function (res) {
                   if (res.status == 0) {

                       if (res.data && res.data.length > 0) {
                           newlist = res.data;
                           index = 0;
                       }
                       else {
                           $("#annouce").html("");
                       }

                   }
               },
               "json"
         );
        setTimeout(LoadNewList, 5000 * 60);
    }
    function animate() {
        var html = [];
        if (newlist[index]) {
            html.push("<div style='width:560px;margin-left:470px;'>")
            html.push("<span style='margin-right:10px;'>", index + 1, ". 客户 [<strong>", newlist[index].UserFullName, "(", newlist[index].MobilePhone, ")</strong>] 预约了 [<strong>", newlist[index].OrderTimeShow, "</strong>]的服务，请确认！</span>");
            html.push("</div>")
            index++;
            if (index >= newlist.length) {
                index = 0;
            }
            $("#annouce").html(html.join(""));
            $("#annouce>div").animate({ "margin-left": -480 }, 16000, function () {
                setTimeout(animate, 1000);
            }).hover(
            function () { $(this).stop(); },
            function () {
                $(this).animate({ "margin-left": -480 },
                    6000, function () {
                        setTimeout(animate, 1000);
                    });
            });
        }
        else {
            setTimeout(animate, 2000);
        }

    }
    //animate();
    //setTimeout(LoadNewList, 5000);    
    $("#STORE_Id").change(function () {
        RefreshView();
    });


    $("#CUST_NAME").autocomplete({
        source: function (request, response) {
            $.getJSON(options.queryMemberUrl, { q: request.term, type: "name" }, function (res) {
                if (res != null && res.status > 0) {
                    var str = eval("(" + res.data + ")");
                    response(str);
                }
            });
        },
        select: function (e, ui) {
            $("#CUST_MOBILE").val(ui.item.mobile);
        }
    });


    $("#CUST_MOBILE").autocomplete({
        source: function (request, response) {
   
            $.getJSON(options.queryMemberUrl, { q: request.term, type: "mobile" }, function (res) {
                if (res != null && res.status > 0) {
                    var str = eval("(" + res.data + ")");
                    response(str);
                }
            });
        },
        select: function (e, ui) {
            $("#CUST_NAME").val(ui.item.name);
        }
    });

    $(function () {
    //根据姓名/电话检索
    $("#CUST_ID").select2({
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
            return "<span style='color:#7B7B7B;font-size:12px;' >未查询到相关信息</span>";
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
        //return false;
        $("#CUST_MOBILE").val(obj.mobile);
        $("#CUST_NAME").val(obj.title);
        return obj.title;
    }

    $("#slColType").change(function () {
        if ($("#slColType").val() == "2") {
            $("#MasseurId").show();
        }
        else {
            $("#MasseurId").hide();
        }
    });


    });

})(window, undefined, jQuery);






