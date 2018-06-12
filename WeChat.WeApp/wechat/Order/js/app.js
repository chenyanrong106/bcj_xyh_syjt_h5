view();
document.onreadystatechange = begin;//当页面加载状态改变的时候执行这个方法.
function begin(){
    if(document.readyState == "complete") {
       showBody();
    }
}
//如果3s内加装还未完成，显示内容
setTimeout(function(){
    showBody();
},3000);
function showBody(){
    $("#loading").fadeOut();
    setTimeout(function(){$("#widget_1").velocity(effect);},500);
}
$(function(){
	var effect="transition.bounceIn";
	var swiper = new Swiper('.swiper-container-v', {
    direction: 'vertical',
    updateOmImagesReady:true,
    onImagesReady: function(swiper){
      $("#wrapper").css({"visibility":"visible"});
    },
    onInit: function(swiper){
    },
    onSlideChangeEnd: function(swiper){
      var time=0;
      $(".widget").hide();
      var index=swiper.activeIndex;
      var w=$("#ss"+index);
      w.find(".widget").each(function(index){ 
        var _this=$(this);
        var n=_this.find(".num");
        var per=n.attr("data-per");
        setTimeout(function(){_this.velocity(effect);n.animateNumber({ number: per },1000);},time);
        time=time+250;
      });
	  	if(index==6){
			$('.icon-arrow-up').hide();
			}else{
			$('.icon-arrow-up').show();	
	  }
	  if(index==0){
	    $(body).css('background','#000');
	  }
      
    }
  });
  for(var i=1;i<8;i++){
    new Swiper('#swiper-container-h-'+i,{
        pagination: '#swiper-pagination-'+i,
        paginationClickable: true,
        loop:true
    });
  }
});
function view(){
  var viewport = document.querySelector("meta[name=viewport]");
  var w=window.screen.width;
  var scale=w/640;
  viewport.setAttribute('content', 'width=640, user-scalable=no,initial-scale='+scale+', maximum-scale='+scale+',minimal-ui, target-densitydpi=device-dpi');
}