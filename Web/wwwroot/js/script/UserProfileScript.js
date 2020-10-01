var arrData = [];

$(document).ready(function () {

    $.ajax({
        url: "/GetProfile/",
        type: "GET",
        dataType: "json",
    }).then((result) => {
        //debugger;
        $('#Id').val(result.id);
        $('#NIK').val(result.nik);
        $('#Email').val(result.email);
        $('#Name').val(result.name);
        $('#Site').val(result.site);
        $('#Phone').val(result.phone);
        //console.log(result.profileImages);
        if (result.profileImages != null) {
            $("#profile").attr("src", "/upload/profiles/" + result.profileImages);
        } else {
            $("#profile").attr("src", "/images/default.png");
        }
        $('#ImageName').val(result.profileImages)
        $('#RoleName').val(result.roleName);
        $('#DeptId').val(result.departmentID);

        $('#Address').val(result.address);
        $('#Province').val(result.province);
        $('#City').val(result.city);
        $('#District').val(result.subDistrict);
        $('#Urban').val(result.village);
        $('#Zipcode').val(result.zipCode);

        $('#tag-name').text(result.name);
        $('#tag-department').text(result.departmentName + ' - ' + result.roleName);
        $('#tag-site').text(result.site);
        //debugger;
        jQuery('#generateQrCode').qrcode({
            width: 256,
            height: 256,
            text: result.id,
        });
    })

    $('#Province').on('change', function () {
        //debugger;
        var getid = $('#Province').val();
        $.ajax({
            url: "/address/LoadCity",
            type: "Get",
            data: { name: getid },
            success: function (data) {
                //debugger;
                //console.log(data);
                $('#City').empty();
                $('#City').append($('<option/>').val('0').text('-- Select --'));
                $.each(data, function (i, val) {
                    $('#City').append($('<option/>').val(val.city).text(val.city))
                });
            }
        });
        $('#City').on('change', function () {
            var getProvince = $('#City').val();
            $.ajax({
                url: "/address/LoadDistrict",
                type: "Get",
                data: { name: getProvince },
                success: function (data) {
                    //debugger;
                    $('#District').empty();
                    $('#District').append($('<option/>').val('0').text('-- Select --'));
                    $.each(data, function (i, val) {
                        $('#District').append($('<option/>').val(val.district).text(val.district))
                    });
                }
            });
            $('#District').on('change', function () {
                var getDistrict = $('#District').val();
                $.ajax({
                    url: "/address/LoadUrban",
                    type: "Get",
                    data: { name: getDistrict },
                    success: function (data) {
                        //debugger;
                        $('#Urban').empty();
                        $('#Urban').append($('<option/>').val('0').text('-- Select --'));
                        $.each(data, function (i, val) {
                            $('#Urban').append($('<option/>').val(val.urban).text(val.urban))
                        });
                    }
                });

            });
        });

    });
});

function LoadData(element) {
    //debugger;
    if (arrData.length === 0) {
        if (element[0].name === 'Province') {
            $.ajax({
                type: "Get",
                url: "/address/LoadProvince",
                success: function (data) {
                    arrData = data;
                    renderData(element);
                }
            });
        }
    }
    else {
        renderData(element);
    }
}

function renderData(element) {
    //debugger;
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('-- Select --').hide());
    if (element[0].name == 'Province') {
        $.each(arrData, function (i, val) {
            $option.append($('<option/>').val(val.provCode).text(val.provinceName))
        });
    } 
}

LoadData($('#Province'))

function GetProfile() {
    //debugger;
    $.ajax({
        url: "/GetProfile/",
        type: "GET",
        dataType: "json",
    }).then((result) => {
        //debugger;
        $('#Id').val(result.id);
        $('#NIK').val(result.nik);
        $('#Email').val(result.email);
        $('#Name').val(result.name);
        $('#Site').val(result.site);
        $('#Phone').val(result.phone);

        $('#ImageName').val(result.profileImages)
        $('#RoleName').val(result.roleName);
        $('#DeptId').val(result.departmentID);

        $('#Address').val(result.address);
        $('#Province').val(result.province);
        $('#City').val(result.city);
        $('#District').val(result.subDistrict);
        $('#Urban').val(result.village);
        $('#Zipcode').val(result.zipCode);
        $('#update').show();
        $('#myModal').modal('show');
    })
}

function Update() {
    //debugger;
    var Data = new Object();
    if ($('#Image').val() == "") {
        if ($('#Pass').val() != "") {
            if ($('#confirmPass').val() == $('#Pass').val()) {
                Data.Id = $('#Id').val();
                Data.NIK = $('#NIK').val();
                Data.Email = $('#Email').val();
                Data.Password = $('#Pass').val();
                Data.Name = $('#Name').val();
                Data.ProfileImages = $('#ImageName').val();
                Data.Site = $('#Site').val();
                Data.Phone = $('#Phone').val();
                Data.Address = $('#Address').val
                Data.Province = $('#Province').val();
                Data.City = $('#City').val();
                Data.SubDistrict = $('#District').val();
                Data.Village = $('#Urban').val();
                Data.ZipCode = $('#Zipcode').val();
                Data.RoleName = $('#RoleName').val();
                Data.DepartmentID = $('#DeptId').val();
                $.ajax({
                    type: 'POST',
                    url: "/updProfile/",
                    cache: false,
                    dataType: "JSON",
                    data: Data
                }).then((result) => {
                    //debugger;
                    if (result.isSuccessStatusCode == true) {
                        $.notify({
                            // options
                            icon: 'flaticon-alarm-1',
                            title: 'Notification',
                            message: 'Updated Successfully',
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
                    timer: 1500,
                    delay: 5000,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                });
                window.setTimeout(function () {
                    location.reload();
                }, 2000);
            }
        } else {
            //debugger;
            Data.Id = $('#Id').val();
            Data.NIK = $('#NIK').val();
            Data.Email = $('#Email').val();
            Data.Name = $('#Name').val();
            Data.ProfileImages = $('#ImageName').val();
            Data.Site = $('#Site').val();
            Data.Phone = $('#Phone').val();
            Data.Address = $('#Address').val();
            Data.Province = $('#Province').val();
            Data.City = $('#City').val();
            Data.SubDistrict = $('#District').val();
            Data.Village = $('#Urban').val();
            Data.ZipCode = $('#Zipcode').val();
            Data.RoleName = $('#RoleName').val();
            Data.DepartmentID = $('#DeptId').val();
            $.ajax({
                type: 'POST',
                url: "/updProfile/",
                cache: false,
                dataType: "JSON",
                data: Data
            }).then((result) => {
                //debugger;
                if (result.isSuccessStatusCode == true) {
                    $.notify({
                        // options
                        icon: 'flaticon-alarm-1',
                        title: 'Notification',
                        message: 'Updated Successfully',
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
                    });
                    window.setTimeout(function () {
                        location.reload();
                    }, 2000);
                }
            })

        }
    } else {
        //debugger;
        var imgclean = $('#Image');
        var dataImg = new FormData();
        dataImg.append('file', $('#Image')[0].files[0]);
        var imgname = $('#Image').val();
        var size = $('#Image')[0].files[0].size;

        var ext = imgname.substr((imgname.lastIndexOf('.') + 1));
        if (ext == 'jpg' || ext == 'jpeg' || ext == 'png' || ext == 'gif' || ext == 'PNG' || ext == 'JPG' || ext == 'JPEG') {
            if (size <= 1000000) {
                $.ajax({
                    url: "/dashboard/UploadImage/",
                    type: "POST",
                    data: dataImg,
                    enctype: 'multipart/form-data',
                    processData: false,  // tell jQuery not to process the data
                    contentType: false   // tell jQuery not to set contentType
                }).then(function (data) {
                    if (data != 'FILE_SIZE_ERROR' || data != 'FILE_TYPE_ERROR') {
                        imgclean.replaceWith(imgclean = imgclean.clone(true));
                        if ($('#Pass').val() != "") {
                            if ($('#confirmPass').val() == $('#Pass').val()) {
                                Data.ProfileImages = $('#Image').val();
                                Data.Id = $('#Id').val();
                                Data.NIK = $('#NIK').val();
                                Data.Email = $('#Email').val();
                                Data.Password = $('#Pass').val();
                                Data.Name = $('#Name').val();
                                Data.ProfileImages = data.imgName;
                                Data.Site = $('#Site').val();
                                Data.Phone = $('#Phone').val();
                                Data.Address = $('#Address').val
                                Data.Province = $('#Province').val();
                                Data.City = $('#City').val();
                                Data.SubDistrict = $('#District').val();
                                Data.Village = $('#Urban').val();
                                Data.ZipCode = $('#Zipcode').val();
                                Data.RoleName = $('#RoleName').val();
                                Data.DepartmentID = $('#DeptId').val();
                                $.ajax({
                                    type: 'POST',
                                    url: "/updProfile/",
                                    cache: false,
                                    dataType: "JSON",
                                    data: Data
                                }).then((result) => {
                                    //debugger;
                                    if (result.isSuccessStatusCode == true) {
                                        $.notify({
                                            // options
                                            icon: 'flaticon-alarm-1',
                                            title: 'Notification',
                                            message: 'Updated Successfully',
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
                                    timer: 1500,
                                    delay: 5000,
                                    animate: {
                                        enter: 'animated fadeInDown',
                                        exit: 'animated fadeOutUp'
                                    },
                                });
                                window.setTimeout(function () {
                                    location.reload();
                                }, 2000);
                            }
                        } else {
                            //debugger;
                            Data.Id = $('#Id').val();
                            Data.NIK = $('#NIK').val();
                            Data.Email = $('#Email').val();
                            Data.Name = $('#Name').val();
                            Data.ProfileImages = data.imgName;
                            Data.Site = $('#Site').val();
                            Data.Phone = $('#Phone').val();
                            Data.Address = $('#Address').val();
                            Data.Province = $('#Province').val();
                            Data.City = $('#City').val();
                            Data.SubDistrict = $('#District').val();
                            Data.Village = $('#Urban').val();
                            Data.ZipCode = $('#Zipcode').val();
                            Data.RoleName = $('#RoleName').val();
                            Data.DepartmentID = $('#DeptId').val();
                            $.ajax({
                                type: 'POST',
                                url: "/updProfile/",
                                cache: false,
                                dataType: "JSON",
                                data: Data
                            }).then((result) => {
                                //debugger;
                                if (result.isSuccessStatusCode == true) {
                                    $.notify({
                                        // options
                                        icon: 'flaticon-alarm-1',
                                        title: 'Notification',
                                        message: 'Updated Successfully',
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
                                    });
                                    window.setTimeout(function () {
                                        location.reload();
                                    }, 2000);
                                }
                            })

                        }
                    }
                    else {
                        imgclean.replaceWith(imgclean = imgclean.clone(true));
                        $.notify({
                            // options
                            icon: 'flaticon-alarm-1',
                            title: 'Notification',
                            message: 'SORRY SIZE AND TYPE ISSUE',
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
                        });
                        window.setTimeout(function () {
                            location.reload();
                        }, 2000);
                    }

                });
            }//end size
            else {
                imgclean.replaceWith(imgclean = imgclean.clone(true));//Its for reset the value of file type
                $.notify({
                    // options
                    icon: 'flaticon-alarm-1',
                    title: 'Notification',
                    message: 'Sorry File size exceeding from 1 Mb',
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
                });
                window.setTimeout(function () {
                    location.reload();
                }, 2000);
            }
        }//end FILETYPE
        else {
            imgclean.replaceWith(imgclean = imgclean.clone(true));
            $.notify({
                // options
                icon: 'flaticon-alarm-1',
                title: 'Notification',
                message: 'Sorry Only you can uplaod JPEG|JPG|PNG|GIF file type',
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
            });
            window.setTimeout(function () {
                location.reload();
            }, 2000);
        }
    }


}
