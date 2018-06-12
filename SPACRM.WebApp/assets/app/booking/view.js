
; (function (window, undefined, $) {   
    var op_width = 350;
    var op_heigth = 166;

    initCalOps();
    getColModels(initView);

    $("#slCol").change(function (event) {
        var cols = $(this).val().split("|");
        if (cols[1] != "") {
            $("#filter").hide();
        } else {
            $("#filter").show();
        }
        refreshView();
    });
    $("#STORE_Id option[value='']").remove();
    $("#STORE_Id").change(function (event) {
        var storeId = $(this).val()
        if (storeId != "") {
            $("#slCol").val("1|"); //重置
            getColModels(resetView); //重置视图
        }
    });

    $(window).resize(resizeView);
    /**
     * POST获取日历视图列定义信息，并交由回调函数处理后续
     * @param {Function} cb [回调函数]
     */
    function getColModels(cb) {
        var slColValue = $("#slCol").val();
        var showdate = $("#hdshowday").val();
        var storeid = $("#STORE_Id").val();
        $.post(
            options.getModelsUrl,
            { "slcol": slColValue, "storeid": storeid, "showdate": showdate },
            function (ret) {
                if (ret && ret.status == 0) {
                    util.colModels = ret.data;
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
     * 初始化整个视图
     * @param  {Array} models [列的定义内容，包含可预约的时间段等信息]
     */
    function initView(models) {
        var width = document.documentElement.clientWidth - op_width;//old -10
        var height = document.documentElement.clientHeight - op_heigth;//-210
        var iWidth = parseInt(getCookie("itemWidth"));
        var itemWidth = 160;
        if (!!iWidth && iWidth > 0) {
            itemWidth = iWidth;
        }
        buildMList(models);
        initSlider(width, itemWidth, models);
        initFilter(models);
        // 初始化 日历视图
        initxjCal(width, itemWidth, height, models);
    }
    function resetView(models) {
        var width = document.documentElement.clientWidth - op_width;//old -10
        var height = document.documentElement.clientHeight - op_heigth;//-210
        var iWidth = parseInt(getCookie("itemWidth"));
        var itemWidth = 160;
        if (!!iWidth && iWidth > 0) {
            itemWidth = iWidth;
        }
        buildMList(models);
        resetSlider(width, itemWidth, models);
        buildFilterHtml(models);
        reloadView(models);
    }
    /**
     * 修改视图大小     *
    */
    function resizeView() {
        var width = document.documentElement.clientWidth - op_width;//old -10
        var height = document.documentElement.clientHeight - op_heigth;//-210
        var iWidth = parseInt(getCookie("itemWidth"));
        var itemWidth = 160;
        if (!!iWidth && iWidth > 0) {
            itemWidth = iWidth;
        }
        resetSlider(width, itemWidth, util.xjcal.getoption().colModels);

        util.xjcal.resize(width, height);
        //console.log("w=", width + ",h=" + height);
    }
    /**
     * [初始化日期操作区域]
     */
    function initCalOps() {
        // 日期选择相关
        var dp = new xjMiniCal("dp-inner", {
            onchange: function (date) { $(document).click(); onDateChanged(date) }
        });
        $("#choose-cal").click(function () {
            releasePop();
            //初始化时间
            var date = StrToDate($("#hdshowday").val());
            if (!date) {
                date = new Date();
            }
            dp.goto(date);
            //显示
            var cof = $(this).offset();
            $("#dp-wrapper").css({ 'left': cof.left - 10, 'top': cof.top + 25 }).fadeIn('fast');
            $(document).one('click', function () {
                $("#dp-wrapper").fadeOut('fast');
            });
            return false;
        });
        $("#prev-cal").click(function () {
            var date = StrToDate($("#hdshowday").val());
            if (!date) {
                date = new Date();
            }
            else {
                var cols = $("#slCol").val();
                var arrCols = cols.split("|");
                if (arrCols[1] == "") { //所有技师
                    date = DateAdd('d', -1, date);
                }
                else {
                    date = DateAdd('d', -7, date);
                }
            }
            onDateChanged(date);
        });
        $("#next-cal").click(function () {
            var date = StrToDate($("#hdshowday").val());
            if (!date) {
                date = new Date();
            }
            else {
                var cols = $("#slCol").val();
                var arrCols = cols.split("|");
                if (arrCols[1] == "") { //所有技师
                    date = DateAdd('d', 1, date);
                }
                else {
                    date = DateAdd('d', 7, date);
                }
            }
            onDateChanged(date);
        });
        //阻止容器内事件冒泡
        $("#dp-wrapper").click(function () {
            return false;
        });

        $("#dp-btn-cancel").click(function () {
            releasePop();
        });
        $("#dp-btn-today").click(function () {
            releasePop();
            onDateChanged(new Date());
        });
    }
    function resetSlider(viewwidth, itemWidth, models) {

    }
    /**
     * [初始化缩放区域]
     * @param  {Int} viewwidth [日历视图的宽度，用于计算滑动块的最大最小单位]
     * @param  {Array} models [列的定义内容，包含可预约的时间段等信息]
    */
    function initSlider(viewwidth, itemWidth, models) {

        $("#slider-wrapper").click(function (event) {
            event.stopPropagation();
        });

        var l = models.length;

        var min = parseInt(viewwidth / l) - 1;
        if(min > 200){
            min = 200;
        }
        var max = viewwidth - 17;
        //列宽滑动条
        $("#col_slider").slider({
            'min': min,
            'max': max,
            'value': itemWidth,
            'tooltip': 'hide',
            'handle': 'square'
        }).on('slide', function (event) {
            //console.log(event);
            resizeItemWidth(event.value);
        });
        $("#time_slider").slider({
            'min': 15,
            'max': 30,
            'value': 15,
            'step': 15,
            'tooltip': 'hide',
            'handle': 'square'
        }).on('slide', function (event) {
            //console.log(event);
            resizeTimeHeight(event.value);
        });
        $("#changesize").click(function () {
            releasePop();
            var cof = $(this).offset();
            $(this).addClass("droptricker");
            $("#slider-wrapper").css({ 'left': cof.left - 320, 'top': cof.top + 26 }).fadeIn('fast');
            $(document).one('click', function () {
                $("#slider-wrapper").fadeOut('fast');
                $("#changesize").removeClass("droptricker");
            });
            return false;
        });
    }

    function buildFilterHtml(models) {
        var cbht = [];
        for (var i = 0, l = models.length; i < l; i++) {
            cbht.push("<label><input type='checkbox' name='filtercb' checked='checked' value='", models[i].id, "'/>", models[i].display, "</label>");
        }
        $("#filter-content").html(cbht.join(""));
    }
    /**
     * [初始化列选择区域]
     * @param  {Array} models [列的定义内容，包含可预约的时间段等信息]
     */
    function initFilter(models) {
        // 初始化 列选择视图
        buildFilterHtml(models);
        $("#filter-wrapper").click(function (event) {
            event.stopPropagation();
            //return false; //阻止冒泡
        });
        $("#filter").click(function () {
            releasePop();
            var cof = $(this).offset();
            $(this).addClass("droptricker");
            $("#filter-wrapper").css({ 'left': cof.left - 560, 'top': cof.top + 25 }).fadeIn('fast');
            $(document).one('click', function () {
                $("#filter-wrapper").fadeOut('fast');
                $("#filter").removeClass("droptricker");
            });
            return false;
        });

        // 全选
        $("#filter-cball").click(function () {
            $("#filter-content input[type='checkbox']").each(function (i) {
                this.checked = true;
                // body...
            });
        });
        // 全不选
        $("#filter-cball-not").click(function () {
            $("#filter-content input[type='checkbox']").each(function (i) {
                this.checked = false;
                // body...
            });
        });
        // 取消
        $("#filter-cancel").click(releasePop);
        // 确认
        $("#filter-ok").click(function () {
            var chs = {};
            var length = 0;
            $("#filter-content input[type='checkbox']").each(function (i) {
                if (this.checked) {
                    chs[this.value] = true;
                    length++;
                }
            });
            if (length == 0) {
                alert("至少选择一项");
                return;
            }
            releasePop();
            filterColModels(chs);
        })
    }
    function buildMList(models) {
        var dl = [];
        dl.push("<option value='1|'>所有技师</option>");
        dl.push("<optgroup label='指定技师'>");
        for (var i = 0, l = models.length; i < l; i++) {
            dl.push("<option value='1|", models[i].id, "'>", models[i].display, "</option>");
        }
        dl.push("</optgroup>");
        $("#slCol").html(dl.join(""));
    }
    /**
     * [初始化日历视图，并将实例赋予util对象上]
     * @param  {Array} models [列的定义内容，包含可预约的时间段等信息]
     */
    function initxjCal(width, itemWidth, height, models) {
        util.xjcal = new xjCalendar("calendar", {
            url: options.calReqHandlerUrl, //请求数据的Url
            width: width,
            height: height,
            eventItems: [],
            colModels: models,//横向栏目
            rowRange: { start: options.startHour, end: options.endHour },// 纵向时间范围 默认为上午11点到晚上22点
            onDragAdd: onDragEventAdd,
            onBindEvent: bindEvent,
            onItemShow: false,
            itemWidth: itemWidth, //单元列宽度
            theme: 14,
            //onChipToolClick: onChipToolClickHandler,
            //onFilterData: FilterData,
            extParam: [{ name: "subid", value: $("#STORE_Id").val() }],
            enableDrag: false,
            autoLoad: true//自动加载，如果eventItems参数没有配置，可启用该参数，默认第一次展现时
        });
    }
    /**
     * 隐藏弹出层 释放事件
     */
    function releasePop() {
        $(document).click();
    }
    /**
     * 时间变化的事件处理函数
     * @param  {Date} date [目标日期]
    */
    function onDateChanged(date) {
        $("#hdshowday").val(date.Format("yyyy-MM-dd"));
        $("#dateshow").html(date.Format("周W , yyyy年MM月dd日"));
        refreshView();
    }
    function buildCalReqParams() {
        var cols = $("#slCol").val();

        var arrCols = cols.split("|");
        var type = arrCols[1] == "" ? 1 : 2;

        var p = { "view": type };

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

        if (type == 2) { //按技师多个选择
            // 1 判断当前技师是否为空,门店是否为空
            extp.push({ name: "mid", value: arrCols[1] });
        }

        p.extParam = extp;
        return p;
    }
    function reloadView(models) {
        var p = buildCalReqParams();
        p.colModels = models;
        util.xjcal.reload(p); // 重新加载列和数据
    }
    /**
     * 刷新日历视图，重新加载数据
     */
    function refreshView() {
        var p = buildCalReqParams();
        getColModels(function (models) {
            p.colModels = models;
            util.xjcal.reload(p); // 重新加载列和数据
        });
    }
    function resizeItemWidth(width) {
        util.xjcal.setoption({ 'itemWidth': width });
        util.xjcal.resizeItemWidth(width);
        setitemWidthCookie(width);
    }
    var setCookeJob;
    function setitemWidthCookie(width) {
        if (setCookeJob) {
            window.clearTimeout(setCookeJob);
        }
        setCookeJob = window.setTimeout(function () {
            setCookie("itemWidth", width);
        }, 2000);
    }

    function renderView(newop) {
        util.xjcal.setoption(newop);
        util.xjcal.render();
    }
    /**
     * 过滤日历视图列
     * @param  {Object} chs [已经选中的列]
     */
    function filterColModels(chs) {
        var newCols = [];
        for (var i = 0, l = util.colModels.length; i < l; i++) {
            if (chs[util.colModels[i].id]) { //选中
                newCols.push(util.colModels[i]);
            }
        }
        if (newCols.length == 0) {
            alert("请至少选中一列");
            return false;
        }
        var p = {};
        p.colModels = newCols;
        util.xjcal.reRender(p); // 重新加载列和数据
    }
    function resizeTimeHeight(unit) {
        util.xjcal.resizeTimeHeight(unit); // 重新加载列和数据
    }

    // 操作cookie相关
    function getExpDate(days, hours, minutes) {
        var expDate = new Date();
        if (typeof (days) == "number" && typeof (hours) == "number" && typeof (hours) == "number") {
            expDate.setDate(expDate.getDate() + parseInt(days));
            expDate.setHours(expDate.getHours() + parseInt(hours));
            expDate.setMinutes(expDate.getMinutes() + parseInt(minutes));
            return expDate.toGMTString();
        }
    }
    //utility function called by getCookie()
    function getCookieVal(offset) {
        var endstr = document.cookie.indexOf(";", offset);
        if (endstr == -1) {
            endstr = document.cookie.length;
        }
        return unescape(document.cookie.substring(offset, endstr));
    }

    // primary function to retrieve cookie by name
    function getCookie(name) {
        var arg = name + "=";
        var alen = arg.length;
        var clen = document.cookie.length;
        var i = 0;
        while (i < clen) {
            var j = i + alen;
            if (document.cookie.substring(i, j) == arg) {
                return getCookieVal(j);
            }
            i = document.cookie.indexOf(" ", i) + 1;
            if (i == 0) break;
        }
        return;
    }
    // store cookie value with optional details as needed
    function setCookie(name, value, expires, path, domain, secure) {
        document.cookie = name + "=" + escape(value) +
            ((expires) ? "; expires=" + expires : "") +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            ((secure) ? "; secure" : "");
    }
    // remove the cookie by setting ancient expiration date
    function deleteCookie(name, path, domain) {
        if (getCookie(name)) {
            document.cookie = name + "=" +
                ((path) ? "; path=" + path : "") +
                ((domain) ? "; domain=" + domain : "") +
                "; expires=Thu, 01-Jan-70 00:00:01 GMT";
        }
    }

 


 //----------------------------------------------------视图扩展和外置操作,这行以上的代码请不要随便改------------------------------------------------------------
    /**
     * 当拖拽时快速新增的方法
     * @param  {Object} item 拖拽时所产生的数据包括开始时间 结束时间等
    */
    function onDragEventAdd(item) {
        var mid, date, subid, subName;
        subid = $("#STORE_Id").val();//门店
        subName = $("#STORE_Id").find("option:selected").text();

        var cols = $("#slCol").val();
        var arrCols = cols.split("|");
        var type = arrCols[1] == "" ? 1 : 2;     
        if (type == 1) {
            mid = item.ColId;//技师ID 
            date = item.OrderDate;//预约日期
        }
        else {
            //未指定技师
            mid = arrCols[1];
            date = item.ColId;//预约日期
        }
        //date = item.OrderDate;//预约日期
        var starttime = StrToDate(date + " " + item.OST + ":00");
  
        if (starttime < new Date()) {
            //showErrorTip("预约开始时间小于当前时间哦~", { right: 100, top: 5 }, true, 5000);
            //return false;
            if (!window.confirm("预约开始时间小于当前时间，是否确定预约？")) {
                return false;
            }
        }   
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
    /**
    * 快捷按钮点击事件
    */
    function onChipToolClickHandler(type, item) {      
        return false;       
    }
     
    /**
     * 绑定Item的时间
    */
    function bindEvent(item) {
        var content = [];
        content.push("<div><b>", item.UserFullName, "</b></div>");
        content.push("<div>电 话：", item.MobilePhone, "</div>");
        //content.push("<div>卡 号：", item.UserCard, "</div>");
        content.push("<div>时 间：", item.OrderDate, " ", item.OST, "-", item.OET, "</div>");
        content.push("<div>项 目：", item.ItemName, "</div>");
       
        //content.push("<div>房 间：", item.BedName, "</div>");
        if (item.Remark) {
            content.push("<div>备 注：", item.Remark, "</div>");
        }
        if (item.CusReqMasseur == "true") {
            content.push("<div>指定技师</div>");
        }
        content.push("<div>金 额：", item.TOTAL_AMT + "(应付:<span style='color:yellow;'>" + item.PAY_AMT + "</span>)", "</div>");
        if (item.PAY_STATUS == 1) {
            content.push("<div>状 态：", item.paystatus, "</div>");
        } else {
            content.push("<div style='color:yellow;'>状 态：", item.paystatus, "</div>");
        }
        $(this).tipTip({
            defaultPosition: "left",
            content: content.join("") // HTML or String to fill TipTIp with
        });
        //other event such as click
        $(this).click(function (event) {
            //记录临时变量
            $("#hdbookingid").val(item.Idx);
            $("#hdcustid").val(item.CustId);
            $("#hdstate").val(item.Status);
            $("#hdoid").val(item.OID);
            $("#hdpaystate").val(item.PAY_STATUS);

            $("#itemop-wrapper").css({ 'left': event.pageX, 'top': event.pageY }).fadeIn('fast');
            $(document).one('click', function () {
                $("#itemop-wrapper").fadeOut('fast');
            });
            return false;
        });
    }
    //开单
    $("#op_btn_0").click(function () {
        var id = $("#hdbookingid").val();
        var cid = $("#hdcustid").val();
        //window.location.href = "/Order/OrderD.do?cid=" + cid + "&bid=" + id;
        if ($("#hdpaystate").val() == 1) {
            alert("该预约已支付");
        } else {
            window.location.href = "/Order/OrderD.do?cid=" + cid + "&oid=" + $("#hdoid").val();
            releasePop();
        }
        return false;
    });
    //编辑
    $("#op_btn_1").click(function () {
        var id = $("#hdbookingid").val();
        var url = StrFormatNoEncode("{0}?id={1}", ["/Booking/New.do", id]);
        window.xjDailog.Open(url, {
            width: 600,
            height: 600,
            caption: '预约登记',
            theme: "simple", //默认主题
            onclose: function (userstate) {
                util.xjcal.reload();
            } //窗口关闭时执行的回调函数  只有窗口通过函数主动关闭时才会触发。
        });
        releasePop();
        return false;
    });
    //确认
    $("#op_btn_2").click(function () {
        var id = $("#hdbookingid").val();
        var state = $("#hdstate").val();
        //校验 OR NOT
        var status = 1;
        updateStatus(id, status);
        releasePop();
        return false;
    });
    //取消
    $("#op_btn_3").click(function () {
        if (confirm("确定要取消此人预约吗？")) {
            var id = $("#hdbookingid").val();
            var state = $("#hdstate").val();
            //校验 OR NOT
            var status = 9;
            updateStatus(id, status);
            releasePop();
            return false;
        }
    });

    //查看会员信息
    $("#op_btn_4").click(function () {
        var cid = $("#hdcustid").val();
        window.location.href = "/Customer/View360.do?cid=" + cid ;
        releasePop();
        return false;
    });
    function updateStatus(id, status) {
        $.post(options.setStatusUrl,
          { ID: id, STATUS: status },
          function (ret) {
              if (ret.status > 0) {
                  _showInfoMessage('操作成功！', 'success');
                  util.xjcal.reload();
              }
              else {
                  alert("操作失败，请稍后重试");
              }
          },
          "json"
       );
    }

    $("#btnShowFailList").click(function (e) {
        var url = "/Booking/FailList.do";
        PopupWindow(url, 1100, 600, true, "yes");
    });

    $("#btnShowWxList").click(function (e) {
        var url = "/Booking/WxList.do";
        PopupWindow(url, 1100, 600, true, "yes");
    });
    function FilterData(data) {
        var showdata = [];  
        for (var i = 0, l = data.length; i < l; i++) {
            if (data[i].Status == 8) // 预约不进
            {
            }
            else if (data[i].Status == 9) //已取消
            {
            }
            else if (data[i].Status == 7) //爽约
            {
            }
            else {
                showdata.push(data[i]);
            }
        }    
        return showdata;
    }   

})(window, undefined, jQuery);







