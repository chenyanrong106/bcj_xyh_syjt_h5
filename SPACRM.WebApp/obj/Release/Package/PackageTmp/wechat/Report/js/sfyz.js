if (window.location.host != "www.report.acchou.com" && window.location.host != "localhost:9732") {
    location = "http://petgohome.meijiewd.com/wechat/order/chou.aspx";
}

$(document).ready(function () {
    //if (isWeiXin()) {
    //    $.ajax({
    //        type: "POST",
    //        url: "../order/oa.aspx?oa=true",
    //        data: {},
    //        async: false,
    //        timeout: 15000,
    //        dataType: "json",
    //        success: function (data) {
    //            if (data.st == 1) {
    //                var a = window.location.toString();
    //                location = "../order/oa.aspx?beforeurl=" + encodeURI(a.replace("&", "*"));
    //            }
    //        },
    //        error: function (XMLHttpRequest, textStatus, errorThrown) {

    //        }
    //    });
    //}
    //else {
    $.ajax({
        type: "POST",
        url: "cwb.aspx?para=oa",
        data: {},
        async: false,
        timeout: 15000,
        dataType: "html",
        success: function (data) {
            //alert(data);
            console.log(data != "ok");
            if (data != "ok") {
                console.log($("#quanbox"));
                $($("#quanbox")[0]).show();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(1);
        }
    });

    //}

});