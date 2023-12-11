
$(document).ready(function () {
    $('.banner-slider').slick({
        autoplay: true,
        autoplaySpeed: 3000,
        infinite: true,
        dots: true,
        arrows: false
    });
    $('.category-slider').slick({
        dots: false,
        arrows: true,
        slidesToShow: 6,
        slidesToScroll: 1,
        prevArrow: '<span class="slick-prev slick-arrow" style=""><i class="fa-solid fa-chevron-left"></i></span>',
        nextArrow: '<span class="slick-next slick-arrow" style=""><i class="fa-solid fa-chevron-right"></i></span>',
    })
});
