; (function (window, undefined, $) {
    function xjMiniCal(id,option) {
        this.__option = $.extend({
            weekStart: 0,
            weekName: ["日","一","二","三","四","五","六"], //星期的格式
            monthName: ["一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二"], //月份的格式
            monthp: "月",
            dateValueFormat: "yyyy-MM-dd",
            currentDate: new Date(),
            today: new Date(),
            onchange: false
        }, option);

        this.__id = id;
        this.__parent = $("#" + id);
        this.init();
    }
    xjMiniCal.prototype = {
        init: function () {
            this.__parent.addClass("xjminical");
            this.render();
            this.filldate();
            this.__initevent();
        },
        __initevent: function () {
            // body...
            var me = this;
            var aprev = this.__parent.find("a.cal-prev");
            var anext = this.__parent.find("a.cal-next");
            var monthsl = this.__parent.find("select.monthsl");
            var yearsl = this.__parent.find("select.yearsl");
            aprev.click(function () {
                me.__option.currentDate = DateAdd("m", -1, me.__option.currentDate);
                me.filldate();
            });
            anext.click(function () {
                me.__option.currentDate = DateAdd("m", 1, me.__option.currentDate);
                me.filldate();
            });
            monthsl.change(YMChange);
            yearsl.change(YMChange);
            function YMChange(){
                var month = parseInt(monthsl.val());
                var year = parseInt(yearsl.val());
                me.__option.currentDate = new Date(year,month-1,1);
                me.filldate();
            }
        },
        render: function (argument) {
            var html = [];

            html.push("<div class='xjminical-p1'>");
            html.push("<div class='xjminical-p1-head  xjminical-head-m1'><a class='cal-prev' href='javascript:void(0);' hidefocus='on'></a>");
            html.push("<span class='ymslp'><select class='yearsl'></select> 年 <select class='monthsl'>");
            for (var i = 1; i <=12; i++) {
                html.push("<option value='",i,"'>",i,"</option>");
            }
            html.push("</select>月</span></div>");
            //周
            html.push("<table class='xjminical-body xjminical-body-m1' cellspacing='0'><thead><tr>");
            //生成周
            for (var i = this.__option.weekStart, j = 0; j < 7; j++) {
                html.push("<th><span>", this.__option.weekName[i], "</span></th>");
                if (i == 6) { i = 0; } else { i++; }
            }
            html.push("</tr></thead>");
            //生成tBody,需要重新生成的
            html.push("<tbody></tbody></table>");
            html.push("</div>");


            // 生成第二个月
            html.push("<div class='xjminical-p1'>");
            html.push("<div class='xjminical-p1-head  xjminical-head-m2'><span class='ymbtn'></span><a class='cal-next' href='javascript:void(0);' hidefocus='on'></a></div>");
            //周
            html.push("<table class='xjminical-body xjminical-body-m2' cellspacing='0'><thead><tr>");
            //生成周
            for (var i = this.__option.weekStart, j = 0; j < 7; j++) {
                html.push("<th><span>", this.__option.weekName[i], "</span></th>");
                if (i == 6) { i = 0; } else { i++; }
            }
            html.push("</tr></thead>");
            //生成tBody,需要重新生成的
            html.push("<tbody></tbody></table>");
            html.push("</div>");

            this.__parent.html(html.join(""));
        },
        filldate:function(){
            this.__fillsdate("m1", this.__option.currentDate);
            //TODO:隐藏第二个日历
            this.__fillsdate("m2", DateAdd("m", 1, this.__option.currentDate));
        },
        __fillsdate: function (m,date) {
            var me = this;
            var tb = this.__parent.find("table.xjminical-body-"+m+" tbody");
            var year = date.getFullYear();
            var monthName = date.getMonth(); //this.__option.monthName[date.getMonth()];
            //monthName + this.__option.monthp + " " + year

            if(m =="m2")
            {
                $("div.xjminical-head-"+m+" span.ymbtn", this.__parent).html([year,' 年 ',monthName+1,' ',this.__option.monthp].join(""));
            }
            else
            {
                var yselect =  $("select.yearsl", this.__parent);

                var yh = [];
                for(var i = year-10;i<year+2;i++){
                    yh.push("<option value='",i,"'",i==year?" selected":'',">",i,"</option>");
                }
                yselect.html(yh.join(""));
                //生成下拉列表

                var mselect =  $("select.monthsl", this.__parent);
                mselect[0].selectedIndex = monthName;
            }


            var year = date.getFullYear();
            var month = date.getMonth();
            var firstdate = new Date(year, month, 1);

            var diffday = this.__option.weekStart - firstdate.getDay();
            if (diffday > 0) {
                diffday -= 7;
            }
            var startdate = DateAdd("d", diffday, firstdate);
            var enddate = DateAdd("d", 42, startdate);
            var tds = this.__option.today.Format(this.__option.dateValueFormat);
            var ins = this.__option.currentDate.Format(this.__option.dateValueFormat);
            var bhm = [];
            for (var i = 1; i <= 42; i++) {
                if (i % 7 == 1) {
                    bhm.push("<tr>");
                }
                var ndate = DateAdd("d", i - 1, startdate);
                var tdc = [];
                /*
                if (ndate.getMonth() < month) {
                    tdc.push("xjminical-prevday");
                }
                else if (ndate.getMonth() > month) {
                    tdc.push("xjminical-nextday");
                }
                */
                if(ndate.getMonth() < month || ndate.getMonth() > month){
                    bhm.push("<td class='outdate' title=''><a><em><span>&nbsp;</span></em></a></td>");
                }
                else
                {
                    tdc.push("xjminical-day");
                    var s = ndate.Format(this.__option.dateValueFormat);
                    if (s == tds) {
                        tdc.push("xjminical-today");
                    }
                    if (s == ins) {
                        //console.log(s + "=" + ins);
                        tdc.push("xjminical-current");
                    }
                    bhm.push("<td class='", tdc.join(" "), "' title='", ndate.Format(this.__option.dateValueFormat), "' xdate='", ndate.Format("yyyy-M-d"), "'><a href='javascript:void(0);' hidefocus='on'><em><span>", ndate.getDate(), "</span></em></a></td>");
                }

                if (i % 7 == 0) {
                    bhm.push("</tr>");
                }
            }
            tb.html(bhm.join(""));
            tb.find("td:not(.outdate)").each(function (i) {
                $(this).click(tbclick);
            });
            function tbclick() {
                me.__parent.find("td.xjminical-current").each(function () {
                    $(this).removeClass("xjminical-current");
                });
                $(this).addClass("xjminical-current").blur();
                var idate = $(this).attr("xdate");
                var date = idate.split(/\D/);
                me.__option.currentDate = new Date(date[0], parseInt(date[1], 10) - 1, date[2]);
                if (me.__option.onchange) {
                    me.__option.onchange.call(me, me.__option.currentDate);
                }
            }
        },
        goto: function (date) {
            // body...
            this.__option.currentDate = date;
            this.filldate();
        }
    };
    window.xjMiniCal = xjMiniCal;

})(window, undefined, jQuery);
