//信息提示
var tipsflag=true;
function tips(text){
	if(tipsflag==true){
		var tishiDiv=document.createElement('div');
		tishiDiv.className="motify"; 
		document.body.appendChild(tishiDiv);
		tipsflag=false;
	 }
	$('.motify').html(text).show();
	setTimeout(function(){$('.motify').fadeOut();},500);
}


//倒计时
var patrn=/^0?(13[0-9]|15[0-9]|17[0-9]|18[0-9]|14[0-9])[0-9]{8}$/;
var daoji=function(){
		var num=60;	
		var timerrr;
		var daojishi=document.getElementById('timeid');
		function dao(){
			if(num>0){
					num--;
					daojishi.innerHTML=num+'s';
					if(num<10){
					daojishi.innerHTML="0"+num+'s';
					}
			}
			if(num==0){
					num=0;	
					clearInterval(timerrr);
					flag=true;
					daojishi.innerHTML='重新发送';
				
			}
		}		
	    timerrr=setInterval(dao,1000);
}

//支付倒计时
function zhifuTime(){
		var interval = 1000; 
		function ShowCountDown(year,month,day,divname) 
		{ 
		var now = new Date(); 
		var endDate = new Date(year, month-1, day); 
		var leftTime=endDate.getTime()-now.getTime(); 
		var leftsecond = parseInt(leftTime/1000); 
		//var day1=parseInt(leftsecond/(24*60*60*6)); 
		var day1=Math.floor(leftsecond/(60*60*24)); 
		var hour=Math.floor((leftsecond-day1*24*60*60)/3600); 
		var minute=Math.floor((leftsecond-day1*24*60*60-hour*3600)/60); 
		var second=Math.floor(leftsecond-day1*24*60*60-hour*3600-minute*60); 
		var cc = document.getElementById(divname); 
		cc.innerHTML = hour+":"+minute+":"+second; 
		} 
		window.setInterval(function(){ShowCountDown(2017,3,11,'shengyuTime');}, interval); 
		
}

//发送验证码
var flag=true;
$('.order-user-yanzhengma').on('click',function(){
	if(flag==true){
	var iphone=document.getElementById('iphone').value;
	if(iphone==""){
		   tips('<p>请输入手机号码！</p>')
	  }else{
		  if(patrn.test(iphone)){
			   daoji();
			   flag=false;
	           tips('发送验证码成功');
		  }
	  }
	  
	}
})	



//发送验证码
var userid='';
$('#bookall').on('click',function(){
	if(userid){
		var iphone=document.getElementById('iphone').value;
		if(iphone==""){
			   tips('<p>请输入手机号码！</p>')
		  }else{
			  if(patrn.test(iphone)){
				   daoji();
				   flag=false;
				   tips('发送验证码成功');
			  }
		  }
	  
	}else{
		 tips('亲，请先登录');
		}
})	

var navFlag=true;
$('#navRight').on('click',function(){
	if(navFlag==true){
		$(this).removeClass('icon-lnk').addClass('icon-lnk-f');
		$('.lnks').show();
		navFlag=false;
	}else{
		$(this).removeClass('icon-lnk-f').addClass('icon-lnk');
		$('.lnks').hide();
		navFlag=true;
		}
	
	})