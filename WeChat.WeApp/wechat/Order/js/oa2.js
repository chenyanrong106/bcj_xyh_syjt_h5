function isWeiXin() {
    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == 'micromessenger') {
        return true;
    } else {
        return false;
    }
}
//alert(window.location.toString().replace(/&/g, "*")); //正则表达式，替换所有&
if (isWeiXin()) {//微信打开则跳转到授权页面
    $.ajax({
        type: "POST",
        url: "oa.aspx?oa=true",
        data: {},
        async: false,
        timeout: 15000,
        dataType: "json",
        success: function (data) {
            if (data.st == 1) {
                var a = window.location.toString();
                location = "oa.aspx?beforeurl=" + encodeURI(a.replace("&", "*"));
            } else {
                $.ajax({
                    type: "POST",
                    url: "oa.aspx?ph=true",
                    data: {},
                    async: false,
                    timeout: 15000,
                    dataType: "json",
                    success: function (data) {
                        if (data.st == 1) {
                            var a = window.location.toString();
                            location = "bangphone.aspx?beforeurl2=" + encodeURI(a.replace(/&/g, "*"));
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                    }
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });
}
else { //普通浏览器打开则跳转到登录页面
    $.ajax({
        type: "POST",
        url: "oa.aspx?ph=true",
        data: {},
        async: false,
        timeout: 15000,
        dataType: "json",
        success: function (data) {
            if (data.st == 1) {
                var a = window.location.toString();
                location = "bangphone.aspx?beforeurl2=" + encodeURI(a.replace(/&/g, "*"));
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });
}