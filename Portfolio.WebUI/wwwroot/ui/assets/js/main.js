(function ($) {
	'use strict';

	var intimateApp = {
		/* ---------------------------------------------
			## Content Loading
		--------------------------------------------- */

		contentLoading: function () {
			$('body').imagesLoaded(function () {
				$('.loader-wrapper').fadeOut(500);
				setTimeout(function () {
					//After 2s, the no-scroll class of the body will be removed
					$('body').removeClass('no-scroll');
					$('body').addClass('loading-done');
				}, 1000); //Here you can change preloader time
			});
		},

		/* ---------------------------------------------
			## Scroll top
		--------------------------------------------- */
		scroll_top: function () {
			$('body').append(
				"<a href='#top' id='scroll-top' class='topbutton btn-hide'><span class='fas fa-chevron-up'></span></a>"
			);
			var $scrolltop = $('#scroll-top');
			$(window).on('scroll', function () {
				if ($(this).scrollTop() > $(this).height()) {
					$scrolltop.addClass('btn-show').removeClass('btn-hide');
				} else {
					$scrolltop.addClass('btn-hide').removeClass('btn-show');
				}
			});
			$("a[href='#top']").on('click', function () {
				$('html, body').animate(
					{
						scrollTop: 0,
					},
					'normal'
				);
				return false;
			});
		},

		/* ---------------------------------------------
			## Menu Script
		--------------------------------------------- */
		menu_script: function () {
			var w = $(window).width();
			if ($('.mobile-sidebar-menu').length && w < 1200) {
				var mobileMenu = $('.site-navigation .navigation').clone().appendTo('.mobile-sidebar-menu');
			}
			if ($('.site-header.header-style-one').length || $('.site-header.header-style-three').length || $('.site-header.header-style-four').length) {
				var mobileMenu = $('.site-navigation .navigation').clone().appendTo('.mobile-sidebar-menu');
			}

			if ($('.site-navigation .mainmenu').find('li > a').siblings('.sub-menu')) {
				$('.mainmenu li > .sub-menu').siblings('a').append("<span class='menu-arrow icofont-simple-down'></span>");
			}

			// Accordion Menu
			var $AccordianMenu = $('.sidebar-menu .navigation a');
			var $mobileSubMenuOpen = $('.hamburger-menus');
			var $overlayClose = $('.overlaybg');

			$overlayClose.on('click', function (e) {
				e.preventDefault();
				$(this).parent().removeClass('sidemenu-active');
				$mobileSubMenuOpen.removeClass("click-menu");
			});

			$mobileSubMenuOpen.on('click', function () {
				$(this).toggleClass("click-menu");
				$('.mobile-sidebar-menu').toggleClass("sidemenu-active");
			});

			$AccordianMenu.on('click', function () {
				var link = $(this);
				var closestUl = link.closest("ul");
				var parallelActiveLinks = closestUl.find(".active")
				var closestLi = link.closest("li");
				var linkStatus = closestLi.hasClass("active");
				var count = 0;

				closestUl.find("ul").slideUp(function () {
					if (++count == closestUl.find("ul").length)
						parallelActiveLinks.removeClass("active");
				});

				if (!linkStatus) {
					closestLi.children("ul").slideDown();
					closestLi.addClass("active");
				}
			});
		},

		/*-------------------------------------------
			## Sticky Header
		--------------------------------------------- */
		sticky_header: function () {
			var w = $(window).width();
			if (w > 1200) {
				$(window).scroll(function() {
					if ($(this).scrollTop() > 10){  
						$('.site-header.default-header-style').addClass("sticky");
					}
					else{
						$('.site-header.default-header-style').removeClass("sticky");
					}
				});
			}
		},

		/* ---------------------------------------------
			## One Page Menu Script
		--------------------------------------------- */
		onePageMenu: function () {
			if ($('.site-header.header-style-one').length || $('.site-header.header-style-two').length || $('.site-header.header-style-three').length || $('.site-header.header-style-four').length || $('.site-header.header-style-five').length) {
				$('.mainmenu > li > a').on('click', function (e) {
					var anchor = $(this);
					$('html, body').stop().animate({
						scrollTop: $(anchor.attr('href')).offset().top - 75
					}, 1200);
					e.preventDefault();
				});
			}
		},

		/*-------------------------------------------
			## Initialize Plugin
		--------------------------------------------- */
		initialize_plugin: function () {
			// Page Animation Script
			$('[data-animate]').scrolla({
				mobile: true,
				once: true,
			});
		},

		 /*-------------------------------------------
				## Parallax Background
		--------------------------------------------- */
		bg_parallax: function () {
			var $bg_parallax = $(".bg-parallax");

			if ($bg_parallax.length) {
					$bg_parallax.each(function() {
							$bg_parallax.parallax(20 + "%", -0.25);
					});
			}
		},

		/* ---------------------------------------------
			## Promo Numbers
		 --------------------------------------------- */
		promo_numbers: function () {
			$('.fanfact-promo-numbers').each(function () {
				$(this).isInViewport(function (status) {
					if (status === 'entered') {
						for (
							var i = 0;
							i < document.querySelectorAll('.odometer').length;
							i++
						) {
							var el = document.querySelectorAll('.odometer')[i];
							el.innerHTML = el.getAttribute('data-odometer-final');
						}
					}
				});
			});
		},

		/* ---------------------------------------------
			## Isotope Activation
		--------------------------------------------- */
		isotope_activation: function () {
			var IsoGriddoload = $('.portfolio-grid');
			IsoGriddoload.isotope({
				itemSelector: '.item',
				percentPosition: true,
				layoutMode: 'packery',
			});

			var ProjMli = $('.portfolio-filter li a');
			var ProjGrid = $('.portfolio-grid');
			ProjMli.on('click', function (e) {
				e.preventDefault();
				ProjMli.removeClass('active');
				$(this).addClass('active');
				var selector = $(this).attr('data-filter');
				ProjGrid.isotope({
					filter: selector,
					animationOptions: {
						duration: 750,
						easing: 'linear',
						queue: false,
					},
				});
			});
		},

		/* ---------------------------------------------
			## Team Carousel
		--------------------------------------------- */
		team_carousel: function () {
			if ($('.team-carousel').length) {
				var items = 3;
				$('.team-carousel').owlCarousel({
					center: false,
					items: items,
					autoplay: false,
					autoplayTimeout: 5000,
					smartSpeed: 700,
					margin: 30,
					singleItem: false,
					loop: true,
					nav: false,
					dots: true,
					responsive: {
						280: {
							items: 1,
						},
						576: {
							items: 1,
						},
						768: {
							items: 2,
						},
						992: {
							items: 3,
						},
						1200: {
							items: items,
						},
					},
				});
			}
		},

		/* ---------------------------------------------
			## Testimonial Carousel
		 --------------------------------------------- */
		testimonial_carousel: function () {
			var $testimonialSlider = jQuery('.testimonial-slider');
			if ($testimonialSlider.length) {
				$testimonialSlider.owlCarousel({
					center: true,
					items: 1,
					autoplay: false,
					autoplayTimeout: 3000,
					smartSpeed: 800,
					loop: true,
					margin: 0,
					singleItem: true,
					dots: false,
					nav: true,
					navText: ["<i class='fas fa-angle-left'></i>", "<i class='fas fa-angle-right'></i>"],
				});
			}
		},

		/* ---------------------------------------------
			## Brands Carousel
		--------------------------------------------- */
		brands_carousel: function() {
			if ($('.brands-carousel').length) {
				var items = 4;
				$('.brands-carousel').owlCarousel({
					center: false,
					items: items,
					autoplay: false,
					autoplayTimeout: 5000,
					smartSpeed: 700,
					margin: 0,
					singleItem: false,
					loop: true,
					nav: false,
					dots: false,
					responsive: {
						280: {
							items: 1
						},
						400: {
							items: 2
						},
						768: {
							items: 3
						},
						992: {
							items: items
						},
					}
				});  
			}
		},

		/* ---------------------------------------------
			## Pop Up Scripts
		 --------------------------------------------- */
		popupscript: function () {
			function getScrollBarWidth() {
				var $outer = $('<div>').css({ visibility: 'hidden', width: 100, overflow: 'scroll' }).appendTo('body'),
					widthWithScroll = $('<div>').css({ width: '100%' }).appendTo($outer).outerWidth();
				$outer.remove();
				return 100 - widthWithScroll;
			}

			// Image Pop up
			var $popupImage = $(".popup-image");
			if ($popupImage.length > 0) {
				$popupImage.magnificPopup({
					type: 'image',
					fixedContentPos: false,
					gallery: { enabled: true },
					removalDelay: 300,
					mainClass: 'mfp-fade',
					callbacks: {
						// This prevenpt pushing the entire page to the right after opening Magnific popup image
						open: function () {
							$(".page-wrapper, .navbar-nav").css("margin-right", getScrollBarWidth());
						},
						close: function () {
							$(".page-wrapper, .navbar-nav").css("margin-right", 0);
						}
					}
				});
			}

			//Video Popup
			var $videoPopup = $(".video-popup");
			if ($videoPopup.length > 0) {
				$videoPopup.magnificPopup({
					type: "iframe",
					removalDelay: 300,
					mainClass: "mfp-fade",
					overflowY: "hidden",
					iframe: {
						markup: '<div class="mfp-iframe-scaler">' +
							'<div class="mfp-close"></div>' +
							'<iframe class="mfp-iframe" frameborder="0" allowfullscreen></iframe>' +
							'</div>',
						patterns: {
							youtube: {
								index: 'youtube.com/',
								id: 'v=',
								src: '//www.youtube.com/embed/%id%?autoplay=1'
							},
							vimeo: {
								index: 'vimeo.com/',
								id: '/',
								src: '//player.vimeo.com/video/%id%?autoplay=1'
							},
							gmaps: {
								index: '//maps.google.',
								src: '%id%&output=embed'
							}
						},
						srcAction: 'iframe_src'
					}
				});
			}
		},

		/* ---------------------------------------------
			## Sidebar Script
		--------------------------------------------- */
		sidebarScript: function () {
			var w = $(window).width();
			var MarginTop = w > 1199 ? 85 : 0;
			if ($('.sidebar-items').length) {
				$('.sidebar-items').theiaStickySidebar({
					containerSelector: '.blog-page-block',
					additionalMarginTop: MarginTop,
					minWidth: 992,
				});
			}
			if ($('.sidebar-services').length) {
				$('.sidebar-services').theiaStickySidebar({
					containerSelector: '.service-details-block',
					additionalMarginTop: MarginTop,
					minWidth: 992,
				});
			}
		},
		/* ---------------------------------------------
		 function initializ
		 --------------------------------------------- */
		initializ: function () {
			intimateApp.scroll_top();
			intimateApp.menu_script();
			intimateApp.sticky_header();
			intimateApp.onePageMenu();
			intimateApp.initialize_plugin();
			intimateApp.bg_parallax();
			intimateApp.promo_numbers();
			intimateApp.testimonial_carousel();
			intimateApp.brands_carousel();
			intimateApp.popupscript();
			intimateApp.sidebarScript();
		},
	};
	/* ---------------------------------------------
	 Document ready function
	 --------------------------------------------- */
	$(function () {
		intimateApp.initializ();
	});

	$(window).on('load', function () {
		intimateApp.contentLoading();
		intimateApp.isotope_activation();
	});
})(jQuery);
