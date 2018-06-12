$(document).ready(function () {
    var storeId = $("#hideStoreId").val();
    //微信列表
    $.post("/Home/BookingList.do", { storeId: storeId },
         function (data) {
             if (data.Status > 0) {
                 var json = data.Data;
                 $(json).each(function () {
                     var html = [];
                     html.push('<li><a href="#" onclick="showWXList()" class="notification-user active">');
                     html.push('<span class="time">' + this.ONDATE + '</span><i class="fa fa-user"></i>');
                     html.push('<span class="msg">' + this.NAME + ' ' + this.MOBILE + '</span>');
                     html.push('</a></li>');

                     $(html.join('')).appendTo("#DivBookingList");

                 });
             }
         },
         "json"
        );

    //未付款订单
    $.post("/Home/OrderList.do", { storeId: storeId },
         function (data) {
             if (data.Status > 0) {
                 var json = data.Data;
                 $(json).each(function () {
                     var html = [];
                     html.push('<li><a href="/Order/OrderD.do?cid=' + this.CUST_ID + '&oid=' + this.ID + '" class="active">');
                     html.push('<span class="time">' + this.ONDATE + '</span>');
                     html.push('');
                     html.push('<div><span class="name">订单号：' + this.ORDER_NO + '</span>');
                     html.push('<span class="msg">姓名：' + this.CUST_NAME + '</span></div>');
                     html.push('</a></li>');

                     $(html.join('')).appendTo("#divOrderList");

                 });
             }
         },
         "json"
        );

  
    showCount();
    function showCount()
    {
        //未约进和未付款人数
        $.post("/Home/OrderBookCount.do", { storeId: storeId },
             function (data) {
                 if (data.Status > 0) {
                     $("#bookListCount").html(data.Data[0].Data);
                     $("#orderListCount").html(data.Data[1].Data);
                     if (parseInt(data.Data[0].Data) > 0) {
                         //alert("微信上有顾客预约啦，请及时处理，谢谢！");
                     }
                 }
             },
             "json"
            );
        $.post("/Home/OrderBookCount2.do", { storeId: storeId },
            function (data) {
                if (data.Status > 0) {
                    //$("#bookListCount").html(data.Data[0].Data);
                    //$("#orderListCount").html(data.Data[1].Data);
                    if (parseInt(data.Data[0].Data) > 0) {
                        //alert("APP上有顾客预约啦，请及时处理，谢谢！");
                    }
                }
            },
            "json"
           );
        $.post("/Home/OrderAppBookCount.do", { storeId: storeId },
             function (data) {
                 if (data.Status > 0) {
                     //$("#bookListCount").html(data.Data[0].Data);
                     //$("#orderListCount").html(data.Data[1].Data);
                     if (parseInt(data.Data[0].Data) > 0) {
                         //alert("APP上有顾客预约到家啦，请及时处理，谢谢！");
                     }
                 }
             },
             "json"
            );
        setTimeout(showCount, 60000 * 10);//10分钟刷新一次
    }
    showBirthCount();
    function showBirthCount() {
        //未约进和未付款人数
        $.post("/Customer/GetCustBirthdayCount.do", {  },
             function (data) {
                 if (data.status > 0) {
                     $("#birthdaycount").text(data.data);
                 }
             },
             "json"
            );
    }

    $('#txtMemberSearch').bind('keypress', function (event) {
        if (event.keyCode == "13") {
            MemberSearch();
        }
    });


    $("#UpdatePass").click(function (e) {
        $.post("/Employee/EditPass.do", {
           
        },
                function (ret) {
                    if (ret && ret.status == 0) {
                        //alert(ret.data.USER_NO + ret.data.USER_PASS);
                        //$("#DICT_CODE").attr("readonly", "readonly");

                        $("#USER_NO").val(ret.data.USER_NO);
                        $("#USER_PASS").val(ret.data.USER_PASS);
                        $("#USER_PASS1").val(ret.data.USER_PASS);
                    }
                },
                "json"
          );
        $('#EditPASS').modal('show');
    });

    $("#btnSavePass").click(function (e) {
        var up = $("#USER_PASS").val();
        var up1 = $("#USER_PASS1").val();
        if (up != up1) {
            _showInfoMessage("确认密码不一致，请重新填写！", 'error');
            return;
        }
        $("#frmPass").submit();
    });

    $('#frmPass').validator(
        {
        rules: {
        },
        fields: {
            '#USER_PASS': 'required',
            '#USER_PASS1': 'required',
           
            //'#USER_STORES_OBJ': 'required',
        },
        valid: function (form) {
            FormSubmit(form, function (res) {
                if (res.status == 0) {
                    $('#EditPASS').modal('hide');
                    _showInfoMessage("保存成功", 'success');
                }
                else {
                    _showInfoMessage("操作失败：" + res.message, 'error');
                }
            })
        }
        });

    $("#read_card").click(function () {
        try {
            OnTest();
            MemberSearch();
        } catch (e) {

        }
    });

    //读卡器读卡
    function OnTest() {

        var st;
        var lSnr;
        var BZ = true;
        st = rd.dc_init(100, 115200);

        if (st <= 0 && BZ) {
            //  alert("读卡机连接不成功!");
            _showInfoMessage('读卡机连接不成功！', 'info');
            BZ = false;
            return BZ;
        }

        st = rd.dc_card(0, lSnr);
        if (st != 0 && BZ) {
            // alert("没有检测非接触式IC卡!");
            _showInfoMessage('没有检测非接触式IC卡！', 'info');
            rd.dc_exit();
            BZ = false;
            return BZ;
        }
        //alert(rd.get_bstrRBuffer_asc);
        //document.myform.tcfdCardId.value = rd.get_bstrRBuffer_asc;
        document.getElementById("txtMemberSearch").value = rd.get_bstrRBuffer_asc;
        st = rd.dc_beep(50);
        if (st != 0 && BZ) //返回值小于0表示失败
        {
            _showInfoMessage('读卡机连接不成功！', 'info');
            rd.dc_exit();
            BZ = false;
            return BZ;
        }

        //关闭端口
        st = rd.dc_exit();
        if (st != 0 && BZ) //返回值小于0表示失败
        {
            _showInfoMessage('读卡机端口关闭不成功！', 'info');
            //alert("读卡机端口关闭不成功!");
            BZ = false;
            return BZ;
        }

        //if (BZ == true) //返回值小于0表示失败
        //{
        //    alert("读卡成功!");
        //}
    }
    /*
    var szdata = "18616387289";
    $.post("/Home/SearchMemberByPhone.do", { phone: szdata },
               function (data) {
                   if (data.Status > 0) {
                       $("#txtalink").html(szdata + "来电" + "[" + data.Data.NAME + "]"); //顶部提示
                       $("#txtMemberSearch").val(szdata);
                       $("#txtMemberSearch").focus();
                       shake($("#txtalink"), "red", 300);

                       if (data.Status == 2) {
                           $("#winpop").css("width", "450px");
                       }
                       else {
                           $("#winpop").css("width", "300px");
                       }
                       $("#txtalinkcon").html(data.Message);
                       document.getElementById('winpop').style.height = '0px';//右下角弹窗
                       setTimeout("tips_pop()", 800);

                       if (data.Data.ID == 0) {
                           alert(szdata + "来电" + "[" + data.Data.NAME + "]");
                       }
                       else {
                           if (confirm("会员[" + data.Data.NAME + "]来电，是否跳转到顾客详情界面？")) {
                               //location = "/Customer/View360.do?cid=" + data.Data.ID;
                               window.open("/Customer/View360.do?cid=" + data.Data.ID);
                           }
                       }

                       //var audio = document.createElement("audio");  //来电语音
                       //var index = 0;
                       //audio.src = "../../assets/music/合成语音.wav";
                       //var arr = new Array();
                       //var str = szdata;
                       //arr = str.split('');;
                       //audio.addEventListener('ended', function () {
                       //    setTimeout(function ()
                       //    { if (index < arr.length) { audio.src = '../../assets/music/' + arr[index].toString() + '.wav'; audio.play(); index++ } }, 200);
                       //}, false);
                       //audio.play();
                   }
               },
    "json"
    );
    */

   
});
function MemberSearch() {
    //查会员信息信息
    var condition = $("#txtMemberSearch").val();
    if (condition == "" || condition == null) {
        return false;
    }
    $.post("/Home/SearchMemberInfo.do", { q: condition },
          function (data) {
              if (data.Status > 0) {
                  var cid = eval(data.Data);
                  window.location.href = "/Customer/View360.do?cid=" + cid;
              }
              else {
                  _showInfoMessage(data.Message, "error");
              }
          },
          "json"
         );
}

function showWXList()
{
    var url = "/Booking/WxList.do";
    PopupWindow(url, 800, 600, true, "yes");
}

function shake(ele, cls, times) {
    var i = 0, t = false, o = "white ", c = "", times = times || 2;
    if (t) return;
    t = setInterval(function () {
        i++;
        c = i % 2 ? cls : o;
        ele.css("color", c);
        if (i == 2 * times) {
            clearInterval(t);
            ele.removeClass(cls);
        }
    }, 200);
};


function tips_pop() {
    var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
    var popH = parseInt(MsgPop.style.height);//用parseInt将对象的高度转化为数字,以方便下面比较
    if (popH == 0) {         //如果窗口的高度是0
        MsgPop.style.display = "block";//那么将隐藏的窗口显示出来
        show = setInterval("changeH('up')", 2);//开始以每0.002秒调用函数changeH("up"),即每0.002秒向上移动一次
    }
    else {         //否则
        hide = setInterval("changeH('down')", 2);//开始以每0.002秒调用函数changeH("down"),即每0.002秒向下移动一次
    }
}
function changeH(str) {
    var MsgPop = document.getElementById("winpop");
    var popH = parseInt(MsgPop.style.height);
    if (str == "up") {     //如果这个参数是UP
        if (popH <= 200) {    //如果转化为数值的高度小于等于100
            MsgPop.style.height = (popH + 1).toString() + "px";//高度增加4个象素
        }
        else {
            clearInterval(show);//否则就取消这个函数调用,意思就是如果高度超过100象度了,就不再增长了
            if ($("#CallCustID").val() == 0) {
                alert($("#CallCustPhone").val() + "来电" + "[" + $("#CallCustName").val() + "]");
            }
            else {
                if (confirm("会员[" + $("#CallCustName").val() + "]来电，是否跳转到顾客详情界面？")) {
                    //location = "/Customer/View360.do?cid=" + data.Data.ID;
                    window.open("/Customer/View360.do?cid=" + $("#CallCustID").val());
                }
            }
        }
    }
    if (str == "down") {
        if (popH >= 4) {       //如果这个参数是down
            MsgPop.style.height = (popH - 1).toString() + "px";//那么窗口的高度减少4个象素
        }
        else {        //否则
            clearInterval(hide);    //否则就取消这个函数调用,意思就是如果高度小于4个象度的时候,就不再减了
            MsgPop.style.display = "none";  //因为窗口有边框,所以还是可以看见1~2象素没缩进去,这时候就把DIV隐藏掉
        }
    }
}



function sleep(d) {
    for (var t = Date.now() ; Date.now() - t <= d;);
}

var audioElement2 = document.createElement('audio');
audioElement2.setAttribute('src', '../../assets/music/合成语音.wav');
audioElement2.controls = true;
audioElement2.loop = true;
audioElement2.load();



function T_GetEvent(uID, uEventType, uHandle, uResult, szdata) {
    if (uEventType == BriEvent_GetCallID) {
        $.post("/Home/SearchMemberByPhone.do", { phone: szdata },
            function (data) {
                if (data.Status > 0) {
                    $("#txtalink").html(szdata + "来电" + "[" + data.Data.NAME + "]"); //顶部提示
                    $("#txtMemberSearch").val(szdata);
                    $("#txtMemberSearch").focus();
                    shake($("#txtalink"), "red", 300);

                    if (data.Status == 2) {
                        $("#winpop").css("width", "450px");
                    }
                    else {
                        $("#winpop").css("width", "300px");
                    }
                    $("#txtalinkcon").html(data.Message);
                    document.getElementById('winpop').style.height = '0px';//右下角弹窗
                    setTimeout("tips_pop()", 800);

                    $("#CallCustID").val(data.Data.ID);
                    $("#CallCustName").val(data.Data.NAME);
                    $("#CallCustPhone").val(szdata);

//                    var audio = document.createElement("audio");  //来电语音
//                    var index = 0;
//                    audio.src = "../../assets/music/合成语音.wav";
//                    var arr = new Array();
//                    var str = szdata;
//                    arr = str.split('');;
//                    audio.addEventListener('ended', function () {
//                        setTimeout(function ()
//                        { if (index < arr.length) { audio.src = '../../assets/music/' + arr[index].toString() + '.wav'; audio.play(); index++ } }, 200);
//                    }, false);
//                    audio.play();
                }
            },
"json"
);


    }
    else if (uEventType == BriEvent_PhoneHang || uEventType == BriEvent_StopCallIn || uEventType == BriEvent_RemoteHang || uEventType == BriEvent_RingBack) {
        $("#txtalink").html("");
        $("#txtMemberSearch").val("");
        audioElement2.pause();
        shake($("#txtalink"), "red", 0);
        tips_pop();
    }
}


