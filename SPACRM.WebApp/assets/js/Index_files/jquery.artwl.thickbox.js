

; (function ($) {
    $.extend({
        artwl_bind: function (options) {
            options=$.extend({
                showbtnid:"",
                title:"",
                content:""
                },options);
            var mask = '<div id="artwl_mask"></div>';
            var boxcontain = '<div id="artwl_boxcontain">\
                                  <a id="artwl_close" href="javascript:void(0);" title="Close"></a>\
                                  <div id="artwl_showbox">\
                                      <div id="artwl_title">\
                                          <h2>\
                                              Title</h2>\
                                      </div>\
                                      <div id="artwl_message">\
                                          Content<br />\
                                      </div>\
                                  </div>\
                              </div>';
            var cssCode = 'html, body, h1, h2, h3, h4, h5{margin: 0px;padding: 0px;}\
                            #artwl_mask{background-color: #000;position: absolute;top: 0px;left: 0px;width: 100%;height: 100%;opacity: 0.5;filter: alpha(opacity=50);display: none;}\
                            #artwl_boxcontain{margin: 0 auto;position: absolute;z-index: 2;line-height: 28px;display: none;}\
                            #artwl_showbox{padding: 10px;background: #FFF;border-radius: 5px;margin: 20px;min-width:300px;min-height:200px;}\
                            #artwl_title{position: relative;height: 27px;border-bottom: 1px solid #999;}\
                            #artwl_close{position: absolute;cursor: pointer;outline: none;top: 0;right: 0;z-index: 4;width: 42px;height: 42px;overflow: hidden;background-image: url(../../assets/js/Index_files/feedback-close.png);_background: none;}\
                            #artwl_message{padding: 10px 0px;overflow: hidden;line-height: 19px;}';
            if ($("#artwl_mask").length == 0) {
                $("body").append(mask + boxcontain);
                $("head").append("<style type='text/css'>" + cssCode + "</style>");
                if(options.title!=""){
                    $("#artwl_title").html(options.title);
                }
                if(options.content!=""){
                    $("#artwl_message").html(options.content);
                }
            }
            $("#"+options.showbtnid).click(function () {
                var height = $("#artwl_boxcontain").height();
                var width = $("#artwl_boxcontain").width();
                $("#artwl_mask").show();
                $("#artwl_boxcontain").css("top", ($(window).height() - height) / 2).css("left", ($(window).width() - width) / 2).show();
                //if ($.browser.msie && $.browser.version.substr(0, 1) < 7) {
                //    width = $(window).width() > 600 ? 600 : $(window).width() - 40;
                //    $("#artwl_boxcontain").css("width", width + "px").css("top", ($(window).height() - height) / 2).css("left", ($(window).width() - width) / 2).show();
                //    $("#artwl_mask").css("width", $(window).width() + "px").css("height", $(window).height() + "px").css("background", "#888");
                //    $("#artwl_close").css("top", "30px").css("right", "30px").css("font-size", "20px").text("关闭");
                //}
            });
            $("#artwl_close").click(function () {
                $("#artwl_mask").hide();
                $("#artwl_boxcontain").hide();
            });
        },
        artwl_close:function(options){
            options=$.extend({
                callback:null
                },options);
            $("#artwl_mask").hide();
            $("#artwl_boxcontain").hide();
            if(options.callback!=null){
                options.callback();
            }
        }
    });
})(jQuery);

; (function ($) {
    $.extend({
        artw2_bind: function (options) {
            options = $.extend({
                showbtnid: "",
                title: "",
                content: ""
            }, options);
            var mask = '<div id="artw2_mask"></div>';
            var boxcontain = '<div id="artw2_boxcontain">\
                                  <a id="artw2_close" href="javascript:void(0);" title="Close"></a>\
                                  <div id="artw2_showbox">\
                                      <div id="artw2_title">\
                                          <h2>\
                                              Title</h2>\
                                      </div>\
                                      <div id="artw2_message">\
                                          Content<br />\
                                      </div>\
                                  </div>\
                              </div>';
            var cssCode = 'html, body, h1, h2, h3, h4, h5{margin: 0px;padding: 0px;}\
                            #artw2_mask{background-color: #000;position: absolute;top: 0px;left: 0px;width: 100%;height: 100%;opacity: 0.5;filter: alpha(opacity=50);display: none;}\
                            #artw2_boxcontain{margin: 0 auto;position: absolute;z-index: 2;line-height: 28px;display: none;}\
                            #artw2_showbox{padding: 10px;background: #FFF;border-radius: 5px;margin: 20px;min-width:300px;min-height:200px;}\
                            #artw2_title{position: relative;height: 27px;border-bottom: 1px solid #999;}\
                            #artw2_close{position: absolute;cursor: pointer;outline: none;top: 0;right: 0;z-index: 4;width: 42px;height: 42px;overflow: hidden;background-image: url(../../assets/js/Index_files/feedback-close.png);_background: none;}\
                            #artw2_message{padding: 10px 0px;overflow: hidden;line-height: 19px;}';
            if ($("#artw2_mask").length == 0) {
                $("body").append(mask + boxcontain);
                $("head").append("<style type='text/css'>" + cssCode + "</style>");
                if (options.title != "") {
                    $("#artw2_title").html(options.title);
                }
                if (options.content != "") {
                    $("#artw2_message").html(options.content);
                }
            }
            $("#" + options.showbtnid).click(function () {
                var height = $("#artw2_boxcontain").height();
                var width = $("#artw2_boxcontain").width();
                $("#artw2_mask").html("");
                $("#artw2_mask").show();
                $("#artw2_boxcontain").css("top", ($(window).height() - height) / 2).css("left", ($(window).width() - width) / 2).show();
                //if ($.browser.msie && $.browser.version.substr(0, 1) < 7) {
                //    width = $(window).width() > 600 ? 600 : $(window).width() - 40;
                //    $("#artw2_boxcontain").css("width", width + "px").css("top", ($(window).height() - height) / 2).css("left", ($(window).width() - width) / 2).show();
                //    $("#artw2_mask").css("width", $(window).width() + "px").css("height", $(window).height() + "px").css("background", "#888");
                //    $("#artw2_close").css("top", "30px").css("right", "30px").css("font-size", "20px").text("关闭");
                //}
            });
            $("#artw2_close").click(function () {
                $("#artw2_mask").hide();
                $("#artw2_boxcontain").hide();
            });
        },
        artw2_close: function (options) {
            options = $.extend({
                callback: null
            }, options);
            $("#artw2_mask").hide();
            $("#artw2_boxcontain").hide();
            if (options.callback != null) {
                options.callback();
            }
        }
    });
})(jQuery);
