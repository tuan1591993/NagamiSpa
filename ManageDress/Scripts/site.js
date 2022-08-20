var localCache = {
    /**
     * timeout for cache in millis
     * @type {number}
     */
    timeout: 30000,
    /** 
     * @type {{_: number, data: {}}}
     **/
    data: {},
    remove: function (url) {
        delete localCache.data[url];
    },
    exist: function (url) {
        return !!localCache.data[url] && ((new Date().getTime() - localCache.data[url]._) < localCache.timeout);
    },
    get: function (url) {
        console.log('Getting in cache for url' + url);
        return localCache.data[url].data;
    },
    set: function (url, cachedData, callback) {
        localCache.remove(url);
        localCache.data[url] = {
            _: new Date().getTime(),
            data: cachedData
        };
        if ($.isFunction(callback)) callback(cachedData);
    }
};

$.ajaxPrefilter(function (options, originalOptions, jqXHR) {
    if (options.cache) {
        var complete = originalOptions.complete || $.noop,
            url = originalOptions.url;
        //remove jQuery cache as we have our own localCache
        options.cache = false;
        options.beforeSend = function () {
            if (localCache.exist(url)) {
                complete(localCache.get(url));
                return false;
            }
            return true;
        };
        options.complete = function (data, textStatus) {
            localCache.set(url, data, complete);
        };
    }
});


var home = {
    vars: {
        currentCateImage: 0
    },
    init: function () {
        contact.init();
        home.Search();
        if ($('#doctors').length > 0) {
            var slider_doctor = $('#doctors .doctor-slider-wrap').lightSlider({
                auto: true,
                loop: true,
                slideMargin: 0,
                item: 1,
                mode: 'fade',
                pause: 5000,
                controls: true,
                pager: false

            })
            $('#doctors .btn-prev').click(function () {
                slider_doctor.goToPrevSlide();
                slider_doctor_thumb.goToPrevSlide();
            });
            $('#doctors .btn-next').click(function () {
                slider_doctor.goToNextSlide();
                slider_doctor_thumb.goToNextSlide();
            });
        }


        if ($('#banner-popup').length > 0) {
            $('#banner-popup').modal('show');
        }

        if ($('.doctor-thumb-slider').length > 0) {
            var slider_doctor_thumb = $('.doctor-thumb-slider').lightSlider({
                auto: true,
                loop: true,
                slideMargin: -36,
                item: 3,
                pause: 5000,
                controls: true,
                pager: false

            });
        }

        if ($('#story').length > 0) {
            var slider_story = $('#story .light-slider').lightSlider({
                auto: true,
                pause: 10000,
                loop: true,
                slideMargin: 40,
                item: 2,
                controls: true,
                pager: false,
                responsive: [
                    {
                        breakpoint: 800,
                        settings: {
                            item: 2,
                            slideMargin: 32
                        }
                    },
                    {
                        breakpoint: 480,
                        settings: {
                            item: 1,
                            slideMargin: 24
                        }
                    }
                ]
            })
            $('#story .btn-prev').click(function () {
                slider_story.goToPrevSlide();
            });
            $('#story .btn-next').click(function () {
                slider_story.goToNextSlide();
            });
        }

        if ($('.tabxxx').length > 0) {
            $('.tabxxx').each(function () {
                var $this = $(this);
                var id = $this.attr('id').split('-')[1];
                $.get(config.UrlDomain + '/Home/_LoadImageHome',
                    { cateid: id },
                    function (data) {
                        $this.html(data);
                    });
            });
        }
        if ($('#tabcusimg').length > 0) {
            var abc = $('#tabcusimg > li > a:first').attr('rel');
            var idf = abc.split('-')[1];
            home.getImageHome(idf);

            $('#tabcusimg > li > a').hover(function () {
                $('#tabcusimg > li > a').removeClass('active');
                $(this).addClass('active');

                var id = $(this).attr('rel').split('-')[1];
                home.getImageHome(id);
            });
            $('#tabcusimg > li > a').click(function () {
                $('#tabcusimg > li > a').removeClass('active');
                $(this).addClass('active');

                var id = $(this).attr('rel').split('-')[1];
                home.getImageHome(id);
            });
        }

        $('.bn-group-txt li > a').popover({
            trigger: "hover",
            html: true,
            content: function () {
                var x = $(this).attr('rel');
                return $('.' + x).html();
            }

        });

        // ANIMATION WHEN SCROLL
        AOS.init({ disable: 'mobile' });
    },
    getImageHome: function (id) {
        if (id != home.vars.currentCateImage) {
            $.ajax({
                url: config.UrlDomain + '/Home/_LoadImageHome?cateid=' + id,
                data: {
                },
                cache: true,
                complete: function (data) {
                    $('#beforeAfter').html(data.responseText);
                }
            });
            home.vars.currentCateImage = id;
        }
    },
}

var contact = {
    init: function () {
        $('#btsavecs').click(function () {
            var fullname = $('#ctfullname').val();
            var mobile = $('#ctmobile').val();
            var message = $('#ctdv').val();

            if (fullname.length == 0 || mobile.length == 0 || message.length == 0) {
                swal("", "* Bạn cần nhập đầy đủ thông tin để thực hiện");
                return false;
            }

            if (!utility.validatePhone(mobile)) {
                swal("", "* Số điện thoại không đúng định dạng");
                return false;
            }
            $('#btsavecs').hide();
            $.post(config.UrlDomain + '/home/savecontact',
                {
                    ldid: 1,
                    fullname: fullname,
                    mobile: mobile,
                    notes: message
                },
                function (data) {
                    $('#btsavecs').show();
                    if (data > 0 || data == -2) {
                        //swal("",
                        //    "Chúc mừng bạn đã đăng ký trải nghiệm thành công! Bích Nguyệt Group sẽ sớm liên hệ lại với bạn.");
                        //$('.closePop').click();
                        location.href = 'https://bichnguyet.vn/uu-dai/trinamkhocobichnguyet/cam-on/';
                    } else if (data == -7) {
                        swal("", "Chương trình đã kết thúc!");
                    }
                    else if (data == -8) {
                        swal("", "Bạn cần nhập đầy đủ thông tin để thực hiện");
                    }
                    else {
                        swal("", "Có lỗi xảy ra trên hệ thống, bạn vui lòng thử lại");
                    }
                });
        });

        $('#btnsendcontact').click(function () {
            var fullname = $('#fullname').val();
            var mobile = $('#phonenumber').val();
            var message = $('#dvqt').val();

            if (fullname.length == 0 || mobile.length == 0 || message.length == 0) {
                swal("", "* Bạn cần nhập đầy đủ thông tin để thực hiện");
                return false;
            }

            if (!utility.validatePhone(mobile)) {
                swal("", "* Số điện thoại không đúng định dạng");
                return false;
            }

            $('#btnsendcontact').hide();
            $.post(config.UrlDomain + '/home/savecontact',
                {
                    ldid: 1,
                    fullname: fullname,
                    mobile: mobile,
                    notes: message
                },
                function (data) {
                    $('#btnsendcontact').show();
                    if (data > 0 || data == -2) {
                        //swal("",
                        //    "Chúc mừng bạn đã đăng ký trải nghiệm thành công! Bích Nguyệt Group sẽ sớm liên hệ lại với bạn.");
                        //$('.closePop').click();
                        location.href = 'https://bichnguyet.vn/uu-dai/trinamkhocobichnguyet/cam-on/';
                    } else if (data == -7) {
                        swal("", "Chương trình đã kết thúc!");
                    }
                    else if (data == -8) {
                        swal("", "Bạn cần nhập đầy đủ thông tin để thực hiện");
                    }
                    else {
                        swal("", "Có lỗi xảy ra trên hệ thống, bạn vui lòng thử lại");
                    }
                });
        });
    }
}