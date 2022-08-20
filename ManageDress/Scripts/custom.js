
$(function () {
	"use strict";
	// ============================================================== 
	//This is for preloader
	// ============================================================== 

	$('.icon-menu').on('click', function () {
		$(this).toggleClass('active');
		$('body').toggleClass('open');
	});
	$('.floating-btn .register').on('click', function () {
		$('.floating-btn').toggleClass('active');
	});
	$('.floating-btn .close').on('click', function () {
		$('.floating-btn').removeClass('active');
	});
	$(window).scroll(function () {
		if ($(window).scrollTop() >= 40) {
			$('body').addClass('scroll');
		}
	});
	$(window).scroll(function () {
		if ($(window).scrollTop() < 8) {
			$('body').removeClass('scroll');
		}
		var scrollHeight, totalHeight;
		scrollHeight = document.body.scrollHeight;
		totalHeight = window.scrollY + window.innerHeight + 80;

		if (totalHeight >= scrollHeight) {
			$('body').addClass('endpage');
		}
		else {
			$('body').removeClass('endpage');
		}
	});
	$('.ontop a').on('click', function (e) {
		e.preventDefault();
		$('html,body').animate({
			scrollTop: 0
		}, 700);
	});

});
