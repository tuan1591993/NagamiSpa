var config = {
    UrlDomain: '/',
    UrlStatic: '/Content/',
    PhotoService: '/',
    FacebookAppId: 0,
    FacebookId: '',
    FacebookLiked: 0,
    FacebookAccessToken: '',
    FacebookName: '',
    FacebookScop: 'email,public_profile',
    DownloadList: [],
    FacebookFanpageId: '108124972874259',
    FacebookFanpageUrl: 'https://www.facebook.com/truykich.vn',
    FacebookUrlShare: 'https://www.facebook.com/truykich.vn',
}
// CSRF (XSRF) security
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};
var utility = {
    LoadMCE: function (elm) {
        tinyMCE.init({
            // General options
            mode: "exact",
            elements: elm,
            width: "100%",
            height: 250,
            plugins: "paste",
            toolbar: "paste",
            theme: "advanced",
            theme_advanced_buttons3_add: "pastetext,pasteword,selectall",
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,fontselect,fontsizeselect",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "top",
            theme_advanced_resizing: true
        });
    },
    Download: function (osid, type) {
        $.get(config.UrlDomain + 'Common/Download', {
            osid: osid,
            type: type
        }, function (data) {
            if (data.data == '#' || data.data == '') {
                swal("", "Bộ cài đang được tải lên mời bạn vui lòng vào lại sau.");
            } else {
                //window.open(data.data);
                location.href = data.data;
            }
        });
    },
    PopupDownload: function (x) {
        if (x == 'window') {
            utility.Download(3, 1);
        } else {
            $('.pop_down').slideUp();
            var popup = '#popup_' + x;
            $(popup).slideDown();
            utility.AutoScroll(popup, 150);
            $('#divLoadingProcess').show();
            $('#divLoadingProcess').click(function () {
                $('.pop_down').slideUp();
                $(popup).hide();
            });
        }
    },
    PopupLogin: function (req) {
        $.fancybox({
            href: req,
            beforeShow: function () {
                this.skin.css({ /*border: "8px solid #0B8FD3",*/
                    background: "none repeat scroll 0 0 #EDEDED"
                });
            },
            afterShow: function () {
                this.skin.css({ /*border: "8px solid #0B8FD3",*/
                    background: "none repeat scroll 0 0 #EDEDED"
                });
            },
            'onClosed': function () {
                console.log('close popup');
            },
            type: 'iframe',
            padding: 0,
            width: 372,
            height: 550
        });
    },
    ShareAndGift: function (btn, callback) {
        var id = $(btn).attr('rel');
        $.get(config.UrlDomain + 'Common/GetEventInfo', {
            eventid: id
        }, function (data) {
            if (data.Code > 0) {
                var info = data.data;
                var title = $(btn).attr('fb-share-title', info.GiftName).attr('fb-share-title');
                var desc = $(btn).attr('fb-share-desc', info.Description).attr('fb-share-desc');
                var image = $(btn).attr('fb-share-image', info.ImagesShare).attr('fb-share-image');
                var link = $(btn).attr('fb-share-link', info.LinkUrl).attr('fb-share-link');
                var imgshare = image;
                var x = image.split(';');
                var cx = x.length;
                if (x.length > 1) {
                    var ex = Math.floor(Math.random() * cx);
                    if (ex == 0 || ex == cx) ex = 1;
                    imgshare = x[ex];
                }

                if (config.FacebookId == '') {
                    FB.login(function (response) {
                        console.log(response);
                        if (response.authResponse) {
                            config.FacebookId = response.authResponse.userID;
                            config.FacebookAccessToken = response.authResponse.accessToken;
                            utility.Share(link, title, desc, imgshare, function () {
                                callback(id);
                            });
                        }
                    }, { scope: config.FacebookScop, return_scopes: true });
                } else {
                    utility.Share(link, title, desc, imgshare, function () {
                        callback(id);
                    });
                }
            } else {
                jAlert(data.ResponseMessage, "Thông báo");
            }
        });
    },
    ShareAndAction: function (btn, callback) {
        var title = $(btn).attr('fb-share-title');
        var desc = $(btn).attr('fb-share-desc');
        var image = $(btn).attr('fb-share-image');
        var link = $(btn).attr('fb-share-link');
        var imgshare = image;

        if (config.FacebookId == '') {
            FB.login(function (response) {
                console.log(response);
                if (response.authResponse) {
                    config.FacebookId = response.authResponse.userID;
                    config.FacebookAccessToken = response.authResponse.accessToken;
                    utility.Share(link, title, desc, imgshare, function () {
                        callback();
                    });
                }
            }, { scope: config.FacebookScop, return_scopes: true });
        } else {
            utility.Share(link, title, desc, imgshare, function () {
                callback();
            });
        }
    },
    CheckLiked: function (callback) {
        FB.api('/me/likes?fields=id', function (res) {
            console.log(res);
            var likes_count = res.data.length;
            for (i = 0; i < likes_count; i++) {
                if (res.data[i].id === config.FacebookFanpageId) {
                    config.FacebookLiked = 1;
                    break;
                }
            }
            if (callback != undefined)
                callback();
        });
    },
    Share: function (link, name, desc, image, callback) {
        var img = image;
        FB.ui({
            method: 'feed',
            name: name,
            link: link,
            picture: img,
            description: desc
        }, function (response) {
            console.log(response);
            if (response && response.post_id) {
                callback();
            }
        });
        return true;
    },
    CheckMobile: function () {
        var check = false;
        (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true })(navigator.userAgent || navigator.vendor || window.opera);
        return check;
    },
    AutoScroll: function (anchor, top) {
        var $target = $(anchor);
        $target = $target.length && $target || $('[name=' + anchor.slice(1) + ']');
        if ($target.length) {
            var targetOffset = $target.offset().top - top;
            $('html,body').animate({ scrollTop: targetOffset }, 1000);

            return false;
        }
        return true;
    },
    getMobileOperatingSystem: function () {
        var userAgent = navigator.userAgent || navigator.vendor || window.opera;
        if (userAgent.match(/Windows Phone/i)) {
            return 3; //windowphone
        }
        else if (userAgent.match(/iPad/i) || userAgent.match(/iPhone/i) || userAgent.match(/iPod/i)) {
            return 1;//ios
        }
        else if (userAgent.match(/Android/i)) {
            return 2;//android
        }
        else {
            return 4;//'unknown'
        }
    },
    get_url_param: function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null) return "";
        else return results[1];
    },
    getparam: function (name, url) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(url);
        if (results == null) return "";
        else return results[1];
    },
    GetImageyoutubeFromUrl: function (link) {
        var imgurl = "https://i.ytimg.com/vi/{0}/mqdefault.jpg";
        //var x = link.split('=');
        var ret = "qqNSb_gK01U";
        //if (x.length > 1)
        //    a = x[1];

        if (link.indexOf("youtube.com") > 0) {
            var ix = link.split('?');
            try {
                if (ix.length > 1) {
                    ret = ix[1];
                    var c = ret.split('&');
                    ret = c[0];
                    var d = ret.split('=');
                    ret = d[1];
                }
            }
            catch (err) {
                ret = "qqNSb_gK01U";
            }
        }
        else {
            var x = link.split('/');
            ret = x[x.length - 1];
        }

        return String.format(imgurl, ret);
    },
    GetVideoFromYouTue: function (link, auto) {
        var imgurl = "http://www.youtube.com/embed/{0}?autoplay=1";
        if (auto == false)
            imgurl = "http://www.youtube.com/embed/{0}";
        var ret = "qqNSb_gK01U";
        if (link.indexOf("youtube.com") > 0) {
            var ix = link.split('?');
            try {
                if (ix.length > 1) {
                    ret = ix[1];
                    var c = ret.split('&');
                    ret = c[0];
                    var d = ret.split('=');
                    ret = d[1];
                }
            }
            catch (err) {
                ret = "qqNSb_gK01U";
            }
        }
        else {
            var x = link.split('/');
            ret = x[x.length - 1];
        }

        return String.format(imgurl, ret);
    },
    GetMediaUrl: function (s) {
        return config.UrlStatic + s;
    },
    IsIntger: function (data) {
        return data % 1 === 0;
    },
    validateEmail: function (email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    },
    validatePhone: function (phone) {
        if (phone.length < 9 || phone.length > 13) {
            return false;
        }
        if (utility.IsIntger(phone))
            return true;
        return false;
    }
}

String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}
COMMON = {
    Alert: function (message) {
        swal("", message)
    },
    Confirm: function (message, btnconfirmText, showcancel = true, callback) {
        swal({
            title: "Thông báo",
            text: message,
            showCancelButton: showcancel,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: btnconfirmText,
            cancelButtonText: "Đóng lại",
            closeOnConfirm: false
        },
            function (b) {
                if (b) {
                    callback();
                }
            });
    },
    readURL: function (input, imgwrap) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $(imgwrap).attr('src', e.target.result);
            };
            reader.readAsDataURL(input.files[0]);
        }
    },
    // Hàm convert chuỗi json Datetime sang Date
    jSonToDate: function (value) {
        try {
            var parsedDate = new Date(parseInt(value.substr(6)));
            return new Date(parsedDate);
        } catch (e) {
            return new Date();
        }
    }, autoScroll: function (anchor, top) {
        var $target = $(anchor);
        $target = $target.length && $target || $('[name=' + anchor.slice(1) + ']');
        if ($target.length) {
            var targetOffset = $target.offset().top - top;
            $('html,body').animate({ scrollTop: targetOffset }, 1000);
            return false;
        }
        return true;
    },

    // Hàm Datetime sang chuối ngày tháng
    // expDate: Ngày tháng
    // option:
    //      0: dd/MM/yyyy hh:mm:ss
    //      1: dd/MM/yyyy
    //      2: hh:mm:ss dd/MM/yyyy
    //      3: hh dd/MM/yyyy
    //      4: hh:mm (AM/PM) - dd/MM/yyyy
    //      5: dd/MM/yyy hh:mm
    //      6: dd/MM/yyy hh:mm (PM/AM)
    //      7: dd/MM

    dateToString: function (expDate, option) {
        var _day = expDate.getDate();
        var _month = expDate.getMonth() + 1;
        var _year = expDate.getFullYear();
        var _hour = expDate.getHours();
        var _minute = expDate.getMinutes();
        var _second = expDate.getSeconds();
        var _ap = "AM";
        if (_hour > 11) _ap = "PM";
        if (_day < 10) _day = "0" + _day;
        if (_month < 10) _month = "0" + _month;
        if (_hour < 10) _hour = "0" + _hour;
        if (_minute < 10) _minute = "0" + _minute;
        if (_second < 10) _second = "0" + _second;
        switch (option) {
            case 0:
                return _day + '/' + _month + '/' + _year + ' ' + _hour + ':' + _minute + ':' + _second;
            case 1:
                return _day + '/' + _month + '/' + _year;
            case 2:
                return _hour + ':' + _minute + ':' + _second + ' ' + _day + '/' + _month + '/' + _year;
            case 3:
                return _hour + 'h - ' + _day + '/' + _month + '/' + _year;
            case 4:
                return _hour + ':' + _minute + ' ' + _ap + ' - ' + _day + '/' + _month + '/' + _year;
            //Thang them vao
            case 5:
                return _hour + ':' + _minute + ':' + _second;
            case 6:
                return _hour + ':' + _minute + ' - ' + _day + '/' + _month + '/' + _year;
            case 7:
                return _day + '/' + _month;
            case 8:
                return _month + '/' + _day + '/' + _year + ' ' + _hour + ':' + _minute + ':' + _second;
            default:
                return expDate.toString();
        }
    },

    // Hàm convert chuỗi json Datetime sang chuối ngày tháng
    // value: chuỗi jSon datetime
    jSonDateToString: function (value, option) {
        if ((typeof (value) == 'undefined') || (value == null)) {
            return value;
        }
        if ((typeof (option) == 'undefined') || (option == null)) {
            option = 0;
        }
        var expDate = COMMON.jSonToDate(value);
        return COMMON.dateToString(expDate, option);
    },

    processAjaxPopup: function (idWrapper) {
        try {
            var top = ($(window).height() - $(idWrapper).outerHeight()) / 2;
            var left = (($(window).width() / 2) - ($(idWrapper).outerWidth() / 2));
            if (top < 0) top = 0;
            if (left < 0) left = 0;

            $(idWrapper).css({
                top: top + 'px',
                left: left + 'px'
            });
            $(idWrapper).draggable({
                handle: $(idWrapper + " .widget-header"),
                start: function (event, ui) { }
            });
            $(idWrapper + " .widget-header").css({ cursor: 'move' });
            $(idWrapper + " .widget-header .icon-remove").click(function () {
                $(idWrapper).parent().html('');
            });

            $(idWrapper + " .widget-toolbox #btnCancel").click(function () {
                $(idWrapper).parent().html('');
            });
        } catch (e) { }
    },
    processAjaxPaging: function () {
        $('div[data-paging="true"]').each(function (parameters) {
            var idPaging = '#' + $(this).attr("id");
            $(idPaging + ' li').off('click');
            $(idPaging + ' li').click(function () {
                if (!$(this).hasClass('disabled') && !$(this).hasClass('active')) {
                    $(idPaging + ' #CurrentPage').val($('a', this).attr('rel'));
                    $(this).closest('form').submit();
                }
            });
        });
    },
    ajaxValidateForm: function (idWrapper) {
        $.validator.unobtrusive.parse(idWrapper);
    },

    processAjaxAction: function (formMethod, url, data, callback, callbackError) {
        $.ajax({
            type: formMethod,
            url: url,
            data: data, // serializes the form's elements.
            cache: false,
            success: function (responseData) {
                callback(responseData);
            },
            error: function () {
                if ((callbackError != undefined) && (callbackError != null))
                    callbackError();
            }
        });
    },

    processAjaxFormButton: function () {
        COMMON.processAjaxPaging();
        $('[ajax-method]').each(function () {
            $(this).off('click');
            $(this).click(function () {
                var idForm = '#' + $(this).closest('form').attr('id');
                var actionMethod = $(this).attr('ajax-method');
                var actionUrl = $(this).attr('ajax-action');
                var hasConfirm = $(this).attr('ajax-confirm');
                var confirmMessage = $(this).attr("ajax-confirm-message");
                var updateTargetId = $(this).attr("ajax-updateTargetId");
                var resubmit = $(this).attr("ajax-resubmit");
                var ajaxCallback = $(this).attr("ajax-callback");
                if ((confirmMessage != undefined) && (confirmMessage != null)) {
                    jConfirm(confirmMessage, 'Thông báo...', function (isConfirm) {
                        if (isConfirm) {
                            COMMON._processAjaxFormButtonPost(actionMethod, actionUrl, idForm, updateTargetId, resubmit, ajaxCallback);
                        }
                    });
                } else {
                    COMMON._processAjaxFormButtonPost(actionMethod, actionUrl, idForm, updateTargetId, resubmit, ajaxCallback);
                }
            });
        });
    },
    _processAjaxFormButtonPost: function (actionMethod, actionUrl, idForm, updateTargetId, resubmit, ajaxCallback) {
        $.ajax({
            type: actionMethod,
            url: actionUrl,
            data: null,
            success: function (responseData) {
                if ((ajaxCallback != undefined) && (ajaxCallback != null)) {
                    eval(ajaxCallback + '(responseData)');
                    return;
                }
                if ((updateTargetId != undefined) && (updateTargetId != null)) {
                    if (updateTargetId == '') {
                        if ($('#divRootPopupWrapper').length == 0) {
                            var sHtml = '<div id="divRootPopupWrapper" style="position: absolute; z-index: 1000;"></div>';
                            $('body').append(sHtml);
                        }
                        $('#divRootPopupWrapper').append(responseData);
                    } else {
                        $(updateTargetId).html(responseData);
                    }
                } else {
                    if (responseData.Code <= 0) {
                        jAlert(responseData.ResponseMessage);
                    } else {
                        if ((resubmit != undefined) && (resubmit != null)) {
                            if (((resubmit) != '') && !isNaN(resubmit)) {
                                $("#CurrentPage", idForm).val(resubmit);
                            }
                            $(idForm).submit();
                        }
                    }
                }
            },
            error: function () {
                jAlert('Có lỗi xảy ra trong quá trình xử lý!');
            }
        });
    },

    setTemplatePopup: function (divPopup) {
        try {
            //alert('window height:' + $(window).height() + ' - popup height:' + $(divPopup).outerHeight());
            var top = (($(window).height() / 2) - ($(divPopup).outerHeight() / 2));
            var left = (($(window).width() / 2) - ($(divPopup).outerWidth() / 2));
            if (top < 0) top = 0;
            if (left < 0) left = 0;

            $(divPopup).css({
                top: top + 'px',
                left: left + 'px'
            });

            $(divPopup).draggable({
                handle: $(divPopup + " .barpopup"),
                start: function (event, ui) { }
            });
            $(divPopup + " .barpopup").css({ cursor: 'move' });
            $(".popupclosebutton").hover(function () {
                $(this).addClass('closebutton_hover');
            },
                function () {
                    $(this).removeClass('closebutton_hover');
                });
        } catch (e) { }
    },
    subString: function (value, length, extend) {
        if ((typeof (value) == 'undefined') || (value == null)) {
            return '';
        }
        if (value.length <= length) {
            return value;
        }
        return value.substr(0, length) + extend;
    },
    subStringMeans: function (value, length, extend) {
        //alert("độ dài:"+value.length +" - cắt:"+length+" - thành:"+extend);
        if ((typeof (value) == 'undefined') || (value == null)) {
            return '';
        }
        if (value.length <= length) {
            return value;
        }
        var _tValue = value.substr(0, length);
        if (value[length] == '')
            _tValue = value.substr(0, length + 1);
        else
            _tValue = _tValue.substr(0, _tValue.lastIndexOf(" "));
        return _tValue + extend;
    },
    // Hàm lấy url từ chuỗi Unicode
    // mục đích: phục vụ cho SEO
    getUrlText: function (plainText) {
        var _URL_CHARS_UNICODE = "AÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴBCDĐEÉÈẸẺẼÊẾỀỆỂỄFGHIÍÌỊỈĨJKLMNOÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠPQRSTUÚÙỤỦŨƯỨỪỰỬỮVWXYÝỲỴỶỸZaáàạảãâấầậẩẫăắằặẳẵbcdđeéèẹẻẽêếềệểễfghiíìịỉĩjklmnoóòọỏõôốồộổỗơớờợởỡpqrstuúùụủũưứừựửữvwxyýỳỵỷỹz0123456789_";
        var _URL_CHARS_ANSI = "AAAAAAAAAAAAAAAAAABCDDEEEEEEEEEEEEFGHIIIIIIJKLMNOOOOOOOOOOOOOOOOOOPQRSTUUUUUUUUUUUUVWXYYYYYYZaaaaaaaaaaaaaaaaaabcddeeeeeeeeeeeefghiiiiiijklmnoooooooooooooooooopqrstuuuuuuuuuuuuvwxyyyyyyz0123456789_";
        var _URL_CHARS_BASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";

        var _strTemp = "";
        var _iLength = plainText.length;

        var _iIndex = 0;

        // Loại bỏ các ký tự có dấu
        for (var i = 0; i < _iLength; i++) {
            iIndex = _URL_CHARS_UNICODE.indexOf(plainText.charAt(i));
            if (iIndex == -1)
                _strTemp += plainText.charAt(i);
            else
                _strTemp += _URL_CHARS_ANSI.charAt(iIndex);
        }
        var _strReturn = "";

        // Loại bỏ các ký tự lạ
        for (var i = 0; i < _iLength; i++) {
            if (_URL_CHARS_BASE.indexOf(_strTemp.charAt(i)) == -1) {
                _strReturn += '-';
            }
            else {
                _strReturn += _strTemp.charAt(i);
            }
        }

        while (_strReturn.indexOf("--") != -1) {
            _strReturn = _strReturn.replace('--', '-');
        }

        if ((_strReturn.length > 0) && (_strReturn.charAt(0) == '-')) {
            _strReturn = _strReturn.substr(1);
        }

        if ((_strReturn.length > 0) && (_strReturn.charAt(_strReturn.length - 1) == '-')) {
            _strReturn = _strReturn.substr(0, _strReturn.length - 1);
        }
        if (_strReturn.length > 60) {
            _iIndex = _strReturn.indexOf('-', 59);
            if (_iIndex != -1) {
                _strReturn = _strReturn.substring(0, _iIndex);
            }
        }
        return _strReturn.toLowerCase();
    },
    // Hàm lấy xâu định dạng theo kiểu tiền tệ: 1234123 --> 1,234,123
    formatMoney: function (argValue) {
        var _comma = (1 / 2 + '').charAt(1);
        var _digit = '.';
        if (_comma == '.') {
            _digit = ',';
        }

        var _sSign = "";
        if (argValue < 0) {
            _sSign = "-";
            argValue = -argValue;
        }

        var _sTemp = "" + argValue;
        var _index = _sTemp.indexOf(_comma);
        var _digitExt = "";
        if (_index != -1) {
            _digitExt = _sTemp.substring(_index + 1);
            _sTemp = _sTemp.substring(0, _index);
        }

        var _sReturn = "";
        while (_sTemp.length > 3) {
            _sReturn = _digit + _sTemp.substring(_sTemp.length - 3) + _sReturn;
            _sTemp = _sTemp.substring(0, _sTemp.length - 3);
        }
        _sReturn = _sSign + _sTemp + _sReturn;
        if (_digitExt.length > 0) {
            _sReturn += _comma + _digitExt;
        }
        return _sReturn;
    },
    countWords: function (string) {
        string = string.trim();
        if (string.length == 0) return 0;
        while (string.indexOf('  ') != -1) {
            string = string.replace('  ', ' ');
        }
        return string.split(' ').length;
    },

    getAvatar: function (goId, width, height) {
        var acc1 = parseInt(goId / 1000000);
        var acc2 = parseInt(goId / 1000);
        return "http://avatar.go.vn/avatar/store/account/" + acc1 + "/" + acc2 + "/" + goId + "/" + goId + ".png." + width + "." + height + ".cache";
    }
};
// thêm hàm indexOf cho IE
if (!Array.indexOf) {
    Array.prototype.indexOf = function (obj, start) {
        start = (start == null) ? 0 : start;
        for (var i = start; i < this.length; i++)
            if (this[i] == obj) {
                return i;
            }
        return -1;
    };
};

Loading = new function () {
    this.m_idLoading = "__divLoading";
    this.m_idLoadingProcess = 'divLoadingProcess';
    this.show = function () {
        if ($("#" + this.m_idLoadingProcess).length == 0) {
            $("body").append('<div id="' + this.m_idLoadingProcess + '"></div>');
            var html = '<div style="position: fixed; z-index: 99; top: 0px; left: 0px; width: 100%; height: 1970px; background: #000; filter: alpha(opacity=40); opacity: 0.4;"></div>';
            $("#" + this.m_idLoadingProcess).html(html);
        }
        else {
            $("#" + this.m_idLoadingProcess).show();
        }
    };

    this.showex = function () {
        if ($("#" + this.m_idLoadingProcess).length == 0) {
            $("body").append('<div id="' + this.m_idLoadingProcess + '"></div>');
            var html = '<div style="position: fixed; z-index: 9999; top: 0px; left: 0px; width: 100%; height: 1970px; background: #000; filter: alpha(opacity=40); opacity: 0.40; cursor:pointer;"></div>';
            html += '<div style="position:fixed; z-index:99999; top:200px; margin:0 auto; width:100%; text-align:center; font-weight:bold"><div style="background:#FFF;padding:10px; margin:0 auto; width:100px;">Đang xử lý...</div></div>';
            $("#" + this.m_idLoadingProcess).html(html);
        }
        else {
            $("#" + this.m_idLoadingProcess).show();
        }
    };

    this.showy = function () {
        if ($("#" + this.m_idLoadingProcess).length == 0) {
            $("body").append('<div id="' + this.m_idLoadingProcess + '"></div>');
            var html = '<div style="position: fixed; z-index: 100; top: 0px; left: 0px; width: 100%; height: 1970px; background: #000; filter: alpha(opacity=50); opacity: 0.5; cursor:pointer;"></div>';
            //+ '<div style="background:#FFF; padding:10px; float:right; position:fixed; z-index:99999; top:0; right:0; font-weight:bold"><img src="/Assets/images/loading.gif" style="width:16px; margin-right:5px; float:left" />Đang xử lý...</div>';
            $("#" + this.m_idLoadingProcess).html(html);
        }
        else {
            $("#" + this.m_idLoadingProcess).show();
        }
    };

    this.close = function () {
        if ($("#" + this.m_idLoadingProcess).length != 0) {
            $("#" + this.m_idLoadingProcess).hide();
        }
    };

};

/*-------------------------------Manu Cookie JS-------------------------------------*/
//fllow minute

function get_cookie(cookie_name) {
    var cookieValue = document.cookie;
    var cookieRexp = new RegExp("\\b" + cookie_name + "=([^;]*)");
    cookieValue = cookieRexp.exec(cookieValue);
    if (cookieValue != null) {
        cookieValue = cookieValue[1];
    }
    else {
        cookieValue = 0;
    }
    return cookieValue;
}

function set_cookie(cookie_name, value, expire) {
    var expire_date = new Date();

    var futdate = new Date();
    var expdate = futdate.getTime();
    expdate += expire * 60 * 1000;
    futdate.setTime(expdate);

    document.cookie = (cookie_name + "=" + escape(value) + ((expire == null) ? "" : ";expires=" + futdate.toGMTString()));
    return true;
}
/*-------------------------------End Manu Cookie JS-----------------------------------*/