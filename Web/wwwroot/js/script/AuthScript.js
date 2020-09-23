var arr = [];
function Login() {
    debugger;
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
        debugger;
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
            window.setTimeout(function () {
                location.reload();
            }, 2000);
        }
    })
}