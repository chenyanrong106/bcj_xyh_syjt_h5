// fix:active
document.body.addEventListener('touchstart', function() {});

// card info slide
var cardSlide = new Swiper('#card-slide', {
    effect: 'coverflow',
    initialSlide: 2,
    slidesPerView: 'auto',
    centeredSlides: true,
    coverflow: {
        rotate: 0,
        stretch: -20,
        depth: 80,
        modifier: 2,
        slideShadows: false
    }
});
var infoSlide = new Swiper('#info-slide', {
    initialSlide: 2
});
cardSlide.params.control = infoSlide;
infoSlide.params.control = cardSlide;

// calendar slide

var calendarSlide = new Swiper('#calendar-slide', {
    effect: 'coverflow',
    slidesPerView: 7,
    hashnav:true,
    centeredSlides: true,
    freeMode : true,
    freeModeSticky : true,
    coverflow: {
        rotate: 0,
        depth: 0,
        slideShadows: false
    }
});

jQuery(document).ready(function($) {

    // tooltip
    $('.tooltip').darkTooltip({
        trigger: 'click',
        animation:'fadeIn',
        size: 'small'
    });

});
