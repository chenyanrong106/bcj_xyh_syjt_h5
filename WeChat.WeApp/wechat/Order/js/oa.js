function isWeiXin() {
    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == 'micromessenger') {
        return true;
    } else {
        return false;
    }
}

if (isWeiXin()) {
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
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });
}