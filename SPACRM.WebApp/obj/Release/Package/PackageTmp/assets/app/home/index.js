// -------------------------------
// Demos
// -------------------------------
$(document).ready(   
  function () {
     
      search();
   
      try {
          //Set nicescroll on notifications
          $(".scrollthis").niceScroll({ horizrailenabled: false });
          $('.dropdown').on('shown.bs.dropdown', function () {
              $(".scrollthis").getNiceScroll().resize();
              $(".scrollthis").getNiceScroll().show();
          });
          $('.dropdown').on('hide.bs.dropdown', function () {
              $(".scrollthis").getNiceScroll().hide();
          });

          $(window).scroll(function () {
              $(".scrollthis").getNiceScroll().resize();
          });
      } catch (e) { }

  });

  //初始化日期时间
    var nowDay = new Date().Format("yyyy-MM-dd ");     
    $('#btnDateRangePicker span').html(moment().startOf('month').format('YYYY-MM-DD') + "至" + moment().endOf('month').format('YYYY-MM-DD'));

    //时间范围快速查询
    $('#btnDateRangePicker').daterangepicker(
        {
            ranges: {
                '今天': [moment(), moment()],
                '昨天': [moment().subtract('days', 1), moment().subtract('days', 1)],
                '本周': [moment().subtract('days', 6), moment()],
                //'上周': [moment().subtract('days', 13), moment()],
                '本月': [moment().startOf('month'), moment().endOf('month')],
                //'上月': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')],
                '今年': [moment().startOf('year'), moment().endOf('year')],
                '去年': [moment().subtract('year', 1).startOf('year'), moment().subtract('year', 1).endOf('year')],
            },
            opens: 'left',
            startDate: moment().subtract('days', 29),
            endDate: moment()

        },
        function (start, end) {
            //选择日期事件
            $('#btnDateRangePicker span').html(start.format('YYYY-MM-DD') + ' 至 ' + end.format('YYYY-MM-DD'));
            $("#Dashboard_Start_Date").val(start.format('YYYYMMDD'));
            $("#Dashboard_End_Date").val(end.format('YYYYMMDD'));
            //alert($("#Dashboard_Start_Date").val());
            //提交表单查询
            search();
        }     
    );


    function search() {
        
        //$('#confirmModal').modal('hide');
        //var id = $("#hdCurrentId").val();
        var Dashboard_Start_Date = $("#Dashboard_Start_Date").val();
        var Dashboard_End_Date = $("#Dashboard_End_Date").val();
        if (Dashboard_Start_Date == "") {
            Dashboard_Start_Date = moment().startOf('month').format('YYYYMMDD');
        }
        if (Dashboard_End_Date == "") {
            Dashboard_End_Date = moment().endOf('month').format('YYYYMMDD');
        }
       // alert(Dashboard_Start_Date);
        $.post("IndexSearch.do", { Dashboard_Start_Date: Dashboard_Start_Date, Dashboard_End_Date: Dashboard_End_Date },
              function (res) {
                  //alert(res.Dx1.NAME);
                  $("#yye").html("<span class='text-top'>￥</span>" + res.YYSR);
                  $("#nsr").html("<span class='text-top'>￥</span>" + res.HYKSR);
                  $("#xzhy").html(res.XZHY);
                  $("#xzyy").html(res.XZYY);

                  ShowSalesAnalaysChat($("#xxfx2014").html(), res.ArrGoodsSalesAnalays, res.ArrCardSalesAnalays, res.ArrProSalesAnalays);
                  ShowMemberRecruitment($("#zmfx2014").html(), res.ArrMem, res.ArrTraveler, res.ArrTotalSum);

              },
              "json"
        );
    }

    $("#xxfx2014").click(function () {
        $("#xxfx2014").attr("class", "btn btn-default btn-sm active"); 
        $("#xxfx2013").attr("class", "btn btn-default btn-sm");
        $("#xxfx2012").attr("class", "btn btn-default btn-sm");
        $.ajax({
            url: options.salesAnalysis,
            type: "POST",
            data: { "year": $("#xxfx2014").html() },
            success: function (result) {
                if (result.Status == 1) {
                    var arr0 = result.Data[0];
                    var arr1 = result.Data[1];
                    var arr2 = result.Data[2];
                    ShowSalesAnalaysChat($("#xxfx2014").html(), arr0, arr1, arr2);
                }
            }
        });
    })

    $("#xxfx2012").click(function () {
        $("#xxfx2012").attr("class", "btn btn-default btn-sm active");
        $("#xxfx2013").attr("class", "btn btn-default btn-sm");
        $("#xxfx2014").attr("class", "btn btn-default btn-sm");
        $.ajax({
            url: options.salesAnalysis,
            type: "POST",
            data: { "year": $("#xxfx2012").html() },
            success: function (result) {
                if (result.Status == 1) {
                    var arr0 = result.Data[0];
                    var arr1 = result.Data[1];
                    var arr2 = result.Data[2];
                    ShowSalesAnalaysChat($("#xxfx2012").html(), arr0, arr1, arr2);
                }
            }
        });
    })

    $("#xxfx2013").click(function () {
        $("#xxfx2013").attr("class", "btn btn-default btn-sm active");
        $("#xxfx2014").attr("class", "btn btn-default btn-sm");
        $("#xxfx2012").attr("class", "btn btn-default btn-sm");
        $.ajax({
            url: options.salesAnalysis,
            type: "POST",
            data: { "year": $("#xxfx2013").html() },
            success: function (result) {
                if (result.Status == 1) {
                    var arr0 = result.Data[0];
                    var arr1 = result.Data[1];
                    var arr2 = result.Data[2];
                    ShowSalesAnalaysChat($("#xxfx2013").html(), arr0, arr1, arr2);
                    
                }
            }
        });
    })

    function ShowSalesAnalaysChat(year, arr0, arr1, arr2) {
        //0--实物产品 1--卡 2--项目
        
        $('#salesContainer').highcharts({
            chart: {
                renderTo: 'container',
                type: 'column',
            },
            title: {
                text: year + '年销售分析'
            },
            xAxis: {
                categories: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']
            },
            yAxis: {
                min: 0,
                title: {
                    text: '销售额'
                }
            },
            //legend: {
            //    layout: 'vertical',
            //    backgroundColor: '#FFFFFF',
            //    align: 'left',
            //    verticalAlign: 'top',
            //    x: 50,
            //    y: 20,
            //    floating: true,
            //    shadow: true
            //},
            tooltip: {
                pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> <br/>',
                shared: true
            },

            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: [{
                name: '产品收入',
                data: [ parseFloat(arr0[0]), parseFloat(arr0[1]),
                        parseFloat(arr0[2]), parseFloat(arr0[3]),
                        parseFloat(arr0[4]), parseFloat(arr0[5]),
                        parseFloat(arr0[6]), parseFloat(arr0[7]),
                        parseFloat(arr0[8]), parseFloat(arr0[9]),
                        parseFloat(arr0[10]), parseFloat(arr0[11])]
            }, {
                name: '卡收入',
                data: [parseFloat(arr1[0]), parseFloat(arr1[1]),
                       parseFloat(arr1[2]), parseFloat(arr1[3]),
                       parseFloat(arr1[4]), parseFloat(arr1[5]),
                       parseFloat(arr1[6]), parseFloat(arr1[7]),
                       parseFloat(arr1[8]), parseFloat(arr1[9]),
                       parseFloat(arr1[10]), parseFloat(arr1[11])]
            }, {
                name: '项目收入',
                data: [ parseFloat(arr2[0]), parseFloat(arr2[1]),
                        parseFloat(arr2[2]), parseFloat(arr2[3]),
                        parseFloat(arr2[4]), parseFloat(arr2[5]),
                        parseFloat(arr2[6]), parseFloat(arr2[7]),
                        parseFloat(arr2[8]), parseFloat(arr2[9]),
                        parseFloat(arr2[10]), parseFloat(arr2[11])]
            }],
            colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4']
        });

    }

   


    function ShowMemberRecruitment(year, arr0, arr1, arr2) {

        //0--实物产品 1--卡 2--项目
        $('#container1').highcharts({
            chart: {
                renderTo: 'container',
                type: 'spline'
            },
            title: {
                text: year + '年会员招募分析'
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']
            },
            yAxis: {
                min: 0,
                title: {
                    text: '人数'
                }
            },
            //legend: {
            //    layout: 'vertical',
            //    backgroundColor: '#FFFFFF',
            //    align: 'left',
            //    verticalAlign: 'top',
            //    x: 50,
            //    y: 20,
            //    floating: true,
            //    shadow: true
            //},
            tooltip: {
                //pointFormat: '<table><tbody><tr><td style="color:{series.color};padding:0">{series.name}: </td><td style="padding:0"><b>{point.y:.1f}</b></td></tr></tbody></table>',
                //shared: true,
                //useHTML: true
                pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> <br/>',
                shared: true
            },

            series: [{
                type: 'spline',
                name: '入会人数',
                data: [ parseInt(arr0[0]), parseInt(arr0[1]),
                        parseInt(arr0[2]), parseInt(arr0[3]),
                        parseInt(arr0[4]), parseInt(arr0[5]),
                        parseInt(arr0[6]), parseInt(arr0[7]),
                        parseInt(arr0[8]), parseInt(arr0[9]),
                        parseInt(arr0[10]), parseInt(arr0[11])]

            }, {
                type: 'spline',
                name: '散客消费数',
                data: [ parseInt(arr1[0]), parseInt(arr1[1]),
                        parseInt(arr1[2]), parseInt(arr1[3]),
                        parseInt(arr1[4]), parseInt(arr1[5]),
                        parseInt(arr1[6]), parseInt(arr1[7]),
                        parseInt(arr1[8]), parseInt(arr1[9]),
                        parseInt(arr1[10]), parseInt(arr1[11])]
            }, {
                type: 'spline',
                name: '总消费数',
                data: [ parseInt(arr2[0]), parseInt(arr2[1]),
                        parseInt(arr2[2]), parseInt(arr2[3]),
                        parseInt(arr2[4]), parseInt(arr2[5]),
                        parseInt(arr2[6]), parseInt(arr2[7]),
                        parseInt(arr2[8]), parseInt(arr2[9]),
                        parseInt(arr2[10]), parseInt(arr2[11])]

            }],
            colors: ['#50B432', '#ED561B', '#DDDF00', '#6AF9C4', '#24CBE5', '#64E572', '#FF9655', '#058DC7', '#FFF263', '#6AF9C4']
        });
    }

    $("#zmfx2013").click(function (e) {
        $("#zmfx2013").attr("class", "btn btn-default btn-sm active");
        $("#zmfx2014").attr("class", "btn btn-default btn-sm");
        $("#zmfx2012").attr("class", "btn btn-default btn-sm");
        $.ajax({
            url: options.memberRecruitment,
            type: "POST",
            data: { "year": $("#zmfx2013").html() },
            success: function (result) {
                if (result.Status == 1) {
                    var arr0 = result.Data[0];
                    var arr1 = result.Data[1];
                    var arr2 = result.Data[2];
                    ShowMemberRecruitment($("#zmfx2013").html(), arr0, arr1, arr2);
                }
            }
        });
    })

    $("#zmfx2014").click(function (e) {
        $("#zmfx2014").attr("class", "btn btn-default btn-sm active");
        $("#zmfx2013").attr("class", "btn btn-default btn-sm");
        $("#zmfx2012").attr("class", "btn btn-default btn-sm");
        $.ajax({
            url: options.memberRecruitment,
            type: "POST",
            data: { "year": $("#zmfx2014").html() },
            success: function (result) {
                if (result.Status == 1) {
                    var arr0 = result.Data[0];
                    var arr1 = result.Data[1];
                    var arr2 = result.Data[2];
                    ShowMemberRecruitment($("#zmfx2014").html(), arr0, arr1, arr2);
                }
            }
        });
    })

    $("#zmfx2012").click(function (e) {
        $("#zmfx2012").attr("class", "btn btn-default btn-sm active");
        $("#zmfx2013").attr("class", "btn btn-default btn-sm");
        $("#zmfx2014").attr("class", "btn btn-default btn-sm");
        $.ajax({
            url: options.memberRecruitment,
            type: "POST",
            data: { "year": $("#zmfx2012").html() },
            success: function (result) {
                if (result.Status == 1) {
                    var arr0 = result.Data[0];
                    var arr1 = result.Data[1];
                    var arr2 = result.Data[2];
                    ShowMemberRecruitment($("#zmfx2012").html(), arr0, arr1, arr2);
                }
            }
        });
    })

//$(function () {
//    $('#container1').highcharts({
//        chart: {
//            type: 'column'
//        },
//        title: {
//            text: '2014年会员招募分析'
//        },
//        subtitle: {
//            text: ''
//        },
//        xAxis: {
//            categories: [
//                'Jan',
//                'Feb',
//                'Mar',
//                'Apr',
//                'May',
//                'Jun',
//                'Jul',
//                'Aug',
//                'Sep',
//                'Oct',
//                'Nov',
//                'Dec'
//            ]
//        },
//        yAxis: {
//            min: 0,
//            title: {
//                text: '人数'
//            }
//        },
//        tooltip: {
//            pointFormat: '<table><tbody><tr><td style="color:{series.color};padding:0">{series.name}: </td><td style="padding:0"><b>{point.y:.1f}</b></td></tr></tbody></table>',
//            shared: true,
//            useHTML: true
//        },
////        plotOptions: {
////            column: {
////                pointPadding: 0.2,
////                borderWidth: 0
////            }
////        },
////        series: [{
////            type: 'column',
////            name: '点评团',
////            data: [49, 71, 106, 129, 144, 176, 135, 148, 216, 194, 95, 54]

////        }, {
////            type: 'column',
////            name: '朋友介绍',
////            data: [83, 78, 98, 93, 106, 84, 105, 104, 91, 83, 106, 92]

////        }, {
////            type: 'column',
////            name: '媒体',
////            data: [48, 38, 39, 41, 47, 48, 59, 59, 52, 65, 59, 51]

////        }, {
////            type: 'column',
////            name: '路过',
////            data: [42, 33, 34, 39, 52, 75, 57, 60, 47, 39, 46, 5]

////        }, {
////            type: 'spline',
////            name: 'total',
////            data: [42, 33, 34, 39, 52, 75, 57, 60, 47, 39, 46, 5]

////        }]

//        series: [{
//            type: 'column',
//            name: '顾客人数',
//            data: [49, 71, 58, 66, 39, 80, 75, 68, 74, 39, 55, 54]

//        }, {
//            type: 'spline',
//            name: 'total',
//            data: [49, 120, 178, 209, 252, 275, 287, 300, 337, 369, 376, 405]

//        }]
//    });
//});
   