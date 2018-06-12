$(function () {
    function getRequestParams() {
        //返回当前 URL 的查询部分（问号 ? 之后的部分）。
        var urlParameters = location.search;
        //声明并初始化接收请求参数的对象
        var requestParameters = new Object();
        //如果该求青中有请求的参数，则获取请求的参数，否则打印提示此请求没有请求的参数
        if (urlParameters.indexOf('?') != -1) {
            //获取请求参数的字符串
            var parameters = decodeURI(urlParameters.substr(1));
            //将请求的参数以&分割中字符串数组
            parameterArray = parameters.split('&');
            //循环遍历，将请求的参数封装到请求参数的对象之中
            for (var i = 0; i < parameterArray.length; i++) {
                requestParameters[parameterArray[i].split('=')[0]] = (parameterArray[i].split('=')[1]);
            }
        }

        return requestParameters;
    }


    /** 
     * 获取指定的URL参数值 
     * URL:http://www.quwan.com/index?name=tyler 
     * 参数：paramName URL参数 
     * 调用方法:getParam("name") 
     * 返回值:tyler 
     */
    function getParam(paramName) {
        paramValue = "", isFound = !1;
        if (this.location.search.indexOf("?") == 0 && this.location.search.indexOf("=") > 1) {
            arrSource = unescape(this.location.search).substring(1, this.location.search.length).split("&"), i = 0;
            while (i < arrSource.length && !isFound) arrSource[i].indexOf("=") > 0 && arrSource[i].split("=")[0].toLowerCase() == paramName.toLowerCase() && (paramValue = arrSource[i].split("=")[1], isFound = !0), i++
        }
        return paramValue == "" && (paramValue = null), paramValue
    } 
});



