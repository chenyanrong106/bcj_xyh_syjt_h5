var _maq = _maq || [];
_maq.push(['_setAccount', '平安献礼30周年，赢惊喜大礼']);

/** 
* pv统计 
*/
(function () {
    var params = {};
    //Document对象数据  
    if (document) {
        params.domain = document.domain || '';
        params.url = document.URL || '';
        params.title = document.title || '';
        params.referrer = document.referrer || '';
    }
    //Window对象数据  
    if (window && window.screen) {
        params.sh = window.screen.height || 0;
        params.sw = window.screen.width || 0;
        params.cd = window.screen.colorDepth || 0;
    }
    //navigator对象数据  
    if (navigator) {
        params.lang = navigator.language || '';
    }
    //解析_maq配置  
    if (_maq) {
        for (var i in _maq) {
            switch (_maq[i][0]) {
                case '_setAccount':
                    params.account = _maq[i][1];
                    break;
                default:
                    break;
            }
        }
    }
    // 其他参数  
    var date = new Date();
    params.ltime = date.getTime() / 1000;
    setTimeout(function () {
        //pv统计
        $.ajax({
            url: $.domainUrl + 'AddPVData',

            type: 'POST',

            data: { "params": params },

            dataType: 'html',

            timeout: 1000,

            error: function (e) { },

            success: function (result) {

            }

        });
    }, 1000);
   
})();  