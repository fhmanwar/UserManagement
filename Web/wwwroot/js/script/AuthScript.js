var arr = [];
var cek1 = getCookie("log1");
var cek2 = getCookie("log2");
var cek3 = getCookie("log3");

$(document).ready(function () {
    $('#getLoad').hide();
});
function Login() {
    //debugger;
    $('#getLoad').show();
    $('#login').prop('disabled', true);
    var validate = new Object();
    validate.Email = $('#Email').val();
    validate.Password = $('#Password').val();
    $.ajax({
        type: 'POST',
        url: "/validate/",
        cache: false,
        dataType: "JSON",
        data: validate
    }).then((result) => {
        //debugger;
        $('#login').prop('disabled', false);
        $('#getLoad').hide();
        if (result.status == true) {
            if (result.msg == "VerifyCode") {
                $.notify({
                    // options
                    icon: 'flaticon-alarm-1',
                    title: 'Notification',
                    message: 'Login Successfully',
                }, {
                    // settings
                    element: 'body',
                    type: "success",
                    allow_dismiss: true,
                    placement: {
                        from: "top",
                        align: "center"
                    },
                    timer: 1500,
                    delay: 5000,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                });
                window.setTimeout(function () {
                    location.reload();
                    window.location.href = "/verify?mail=" + validate.Email;
                }, 2000);
            } else {
                $.notify({
                    // options
                    icon: 'flaticon-alarm-1',
                    title: 'Notification',
                    message: 'Login Successfully',
                }, {
                    // settings
                    element: 'body',
                    type: "success",
                    allow_dismiss: true,
                    placement: {
                        from: "top",
                        align: "center"
                    },
                    timer: 1500,
                    delay: 5000,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    icon_type: 'class',
                    template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        '<span data-notify="icon"></span> ' +
                        '<span data-notify="title">{1}</span> ' +
                        '<span data-notify="message">{2}</span>' +
                        '<div class="progress" data-notify="progressbar">' +
                        '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                        '</div>' +
                        '<a href="{3}" target="{4}" data-notify="url"></a>' +
                        '</div>'
                });
                setCookie("log1", "", 0);
                setCookie("log2", "", 0);
                setCookie("log3", "", 0);
                localStorage.setTimeLog = '';
                window.setTimeout(function () {
                    location.reload();
                    window.location.href = "/";
                }, 2000);

            }
        } else {
            $.notify({
                // options
                icon: 'flaticon-alarm-1',
                title: 'Notification',
                message: result.msg,
            }, {
                // settings
                element: 'body',
                type: "danger",
                allow_dismiss: true,
                placement: {
                    from: "top",
                    align: "center"
                },
                timer: 1000,
                delay: 5000,
                animate: {
                    enter: 'animated fadeInDown',
                    exit: 'animated fadeOutUp'
                },
                icon_type: 'class',
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> ' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
            });
            //debugger;
            var date = new Date();
            date.setTime(date.getTime() + (3 * 60 * 1000));

            if (cek1 == "") {
                setCookie("log1", "login-1", 1);

                window.setTimeout(function () {
                    location.reload();
                }, 2000);
            } else if (cek2 == "") {
                setCookie("log2", "login-2", 1);

                window.setTimeout(function () {
                    location.reload();
                }, 2000);
            } else {
                setCookie("log3", "login-3", date.toGMTString());

                //$('#login').prop('disabled', true);
                //setTimeout(function () {
                //    $('#login').prop('disabled', false);
                //}, 30000);

                //time = 120;
                //showTimer();
                //timer = setInterval(showTimer, 1000);

                var minute = 3 * 60;
                time = minute;
                showTimer();
                timer = setInterval(showTimer, 1000);

                $.notify({
                    // options
                    icon: 'flaticon-alarm-1',
                    title: 'Notification',
                    message: 'Please wait until \n' + moment(minute).format('DD MMMM LTS') + ' finished',
                }, {
                    // settings
                    element: 'body',
                    type: "danger",
                    allow_dismiss: true,
                    placement: {
                        from: "top",
                        align: "center"
                    },
                    timer: 1000,
                    delay: 5000,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    icon_type: 'class',
                    template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        '<span data-notify="icon"></span> ' +
                        '<span data-notify="title">{1}</span> ' +
                        '<span data-notify="message">{2}</span>' +
                        '<div class="progress" data-notify="progressbar">' +
                        '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                        '</div>' +
                        '<a href="{3}" target="{4}" data-notify="url"></a>' +
                        '</div>'
                });


                setTimer = (minute * 1000);
                $('#login').prop('disabled', true);
                setTimeout(function () {
                    $('#login').prop('disabled', false);
                    setCookie("log1", "", 0);
                    setCookie("log2", "", 0);
                    setCookie("log3", "", 0);
                    location.reload();
                }, setTimer);

                var cekdate = new Date();
                cekdate.setTime(date.getTime() + (minute * 1000));
                localStorage.setTimeLog = cekdate.toGMTString();
            }
        }
    });
    
}

function Forgot() {
    //debugger;
    $('#getLoad').show();
    $('#login').prop('disabled', true);
    var validate = new Object();
    validate.Email = $('#Email').val();
    $.ajax({
        type: 'POST',
        url: "/changePass/",
        cache: false,
        dataType: "JSON",
        data: validate
    }).then((result) => {
        //debugger;
        $('#login').prop('disabled', false);
        $('#getLoad').hide();
        if (result.isSuccessStatusCode == true) {
            $.notify({
                // options
                icon: 'flaticon-alarm-1',
                title: 'Notification',
                message: 'Please Check Your Email',
            }, {
                // settings
                element: 'body',
                type: "success",
                allow_dismiss: true,
                placement: {
                    from: "top",
                    align: "center"
                },
                timer: 1500,
                delay: 5000,
                animate: {
                    enter: 'animated fadeInDown',
                    exit: 'animated fadeOutUp'
                },
                icon_type: 'class',
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> ' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
            });
            window.setTimeout(function () {
                location.reload();
                window.location.href = "/";
            }, 2000);
        } else {
            $.notify({
                // options
                icon: 'flaticon-alarm-1',
                title: 'Notification',
                message: result.msg,
            }, {
                // settings
                element: 'body',
                type: "warning",
                allow_dismiss: true,
                placement: {
                    from: "top",
                    align: "center"
                },
                timer: 1500,
                delay: 5000,
                animate: {
                    enter: 'animated fadeInDown',
                    exit: 'animated fadeOutUp'
                },
                icon_type: 'class',
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> ' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
            });
            window.setTimeout(function () {
                location.reload();
                window.location.href = "/";
            }, 2000);
        }
    })
}

function Reset() {
    //debugger;
    $('#getLoad').show();
    $('#login').prop('disabled', true);
    if ($('#Password').val() == $('#confirmPass').val()) {
        const urlParams = new URLSearchParams(window.location.search);
        var validate = new Object();
        validate.Token = urlParams.get('token');
        validate.Password = $('#Password').val();
        $.ajax({
            type: 'POST',
            url: "/changePass/",
            cache: false,
            dataType: "JSON",
            data: validate
        }).then((result) => {
            //debugger;
            $('#login').prop('disabled', false);
            $('#getLoad').hide();
            if (result.isSuccessStatusCode == true) {
                $.notify({
                    // options
                    icon: 'flaticon-alarm-1',
                    title: 'Notification',
                    message: 'Your Password has change <br /> Please Login With New Password',
                }, {
                    // settings
                    element: 'body',
                    type: "success",
                    allow_dismiss: true,
                    placement: {
                        from: "top",
                        align: "center"
                    },
                    timer: 1500,
                    delay: 5000,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    icon_type: 'class',
                    template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        '<span data-notify="icon"></span> ' +
                        '<span data-notify="title">{1}</span> ' +
                        '<span data-notify="message">{2}</span>' +
                        '<div class="progress" data-notify="progressbar">' +
                        '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                        '</div>' +
                        '<a href="{3}" target="{4}" data-notify="url"></a>' +
                        '</div>'
                });
                window.setTimeout(function () {
                    location.reload();
                    window.location.href = "/";
                }, 2000);
            } else {
                $.notify({
                    // options
                    icon: 'flaticon-alarm-1',
                    title: 'Notification',
                    message: result.msg,
                }, {
                    // settings
                    element: 'body',
                    type: "warning",
                    allow_dismiss: true,
                    placement: {
                        from: "top",
                        align: "center"
                    },
                    timer: 1500,
                    delay: 5000,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    icon_type: 'class',
                    template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        '<span data-notify="icon"></span> ' +
                        '<span data-notify="title">{1}</span> ' +
                        '<span data-notify="message">{2}</span>' +
                        '<div class="progress" data-notify="progressbar">' +
                        '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                        '</div>' +
                        '<a href="{3}" target="{4}" data-notify="url"></a>' +
                        '</div>'
                });
                window.setTimeout(function () {
                    location.reload();
                }, 2000);
            }
        })
    } else {
        $.notify({
            // options
            icon: 'flaticon-alarm-1',
            title: 'Notification',
            message: 'Password Not Match',
        }, {
            // settings
            element: 'body',
            type: "danger",
            allow_dismiss: true,
            placement: {
                from: "top",
                align: "center"
            },
            timer: 1500,
            delay: 5000,
            animate: {
                enter: 'animated fadeInDown',
                exit: 'animated fadeOutUp'
            },
            icon_type: 'class',
            template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                '<span data-notify="icon"></span> ' +
                '<span data-notify="title">{1}</span> ' +
                '<span data-notify="message">{2}</span>' +
                '<div class="progress" data-notify="progressbar">' +
                '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                '</div>' +
                '<a href="{3}" target="{4}" data-notify="url"></a>' +
                '</div>'
        });
        window.setTimeout(function () {
            location.reload();
        }, 2000);
    }
}


$(function () {
    //debugger;
    var getDateNow = new Date();
    var getTimerLog = new Date(localStorage.setTimeLog);
    currTime = moment(getDateNow).format('DD MMMM LTS');
    getLogTime = moment(getTimerLog).format('DD MMMM LTS');

    if (cek3 == '') {
        $('#login').val('Sign In');
        $('#login').prop('disabled', false);
    } else {
        console.log(currTime);
        console.log(getLogTime);
        if (currTime > getLogTime) {
            setCookie("log1", "", 0);
            setCookie("log2", "", 0);
            setCookie("log3", "", 0);
            localStorage.setTimeLog = '';
        }
        $('#login').prop('disabled', true);
        $('#login').val('Sign In');
        $.notify({
            // options
            icon: 'flaticon-alarm-1',
            title: 'Notification',
            message: 'Please wait until <br>' + moment(getTimerLog).format('DD MMMM LTS') + ' finished',
        }, {
            // settings
            element: 'body',
            type: "danger",
            allow_dismiss: true,
            placement: {
                from: "top",
                align: "center"
            },
            timer: 1000,
            delay: 5000,
            animate: {
                enter: 'animated fadeInDown',
                exit: 'animated fadeOutUp'
            },
            icon_type: 'class',
            template: '<div data-notify="container" class="col-xs-11 col-sm-5 alert alert-{0}" role="alert">' +
                '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                '<span data-notify="icon"></span> ' +
                '<span data-notify="title">{1}</span> ' +
                '<span data-notify="message">{2}</span>' +
                '<div class="progress" data-notify="progressbar">' +
                '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                '</div>' +
                '<a href="{3}" target="{4}" data-notify="url"></a>' +
                '</div>'
        });
    }





    //debugger;
    if (localStorage.chkbx && localStorage.chkbx != '') {
        $('#rememberMe').attr('checked', 'checked');
        $('#Email').val(localStorage.mail);
        $('#Password').val(localStorage.pass);
    } else {
        $('#rememberMe').removeAttr('checked');
        $('#Email').val('');
        $('#Password').val('');
    }

    $('#rememberMe').click(function () {
        //debugger;
        if ($('#rememberMe').is(':checked')) {
            // save username and password
            localStorage.mail = $('#Email').val();
            localStorage.pass = $('#Password').val();
            localStorage.chkbx = $('#rememberMe').val();
        } else {
            localStorage.mail = '';
            localStorage.pass = '';
            localStorage.chkbx = '';
        }
    });

    //debugger;
    //var date = new Date();
    //console.log(date.getTime());

    // date.getTime() -> Get Time Now
    // (1 * 24 * 60 * 60 * 1000) -> day * Hours * minutes * second * milisecond
    //var setDate = date.setTime(date.getTime() + (1 * 24 * 60 * 60 * 1000));


});


function setCookie(cookieName, cookieValue, daysToExpire) {
    var date = new Date();
    date.setTime(date.getTime() + (daysToExpire * 24 * 60 * 60 * 1000));
    document.cookie = cookieName + "=" + cookieValue + "; expires=" + date.toGMTString();
}

function getCookie(cookieName) {
    var name = cookieName + "=";
    var allCookieArray = document.cookie.split(';');
    for (var i = 0; i < allCookieArray.length; i++) {
        var temp = allCookieArray[i].trim();
        if (temp.indexOf(name) == 0)
            return temp.substring(name.length, temp.length);
    }
    return "";
}

//function checkCookie() {
//    var user = accessCookie("testCookie");
//    if (user != "")
//        alert("Welcome Back " + user + "!!!");
//    else {
//        user = prompt("Please enter your name");
//        num = prompt("How many days you want to store your name on your computer?");
//        if (user != "" && user != null) {
//            createCookie("testCookie", user, num);
//        }
//    }
//}


function showTimer() {
    if (time < 0) {
        clearInterval(timer);
        return;
    }
    function pad(value) {
        return (value < 10 ? '0' : '') + value;
    }
    $('#login').val(Math.floor(time / 60) + ':' + pad(time % 60));
    time--;
}