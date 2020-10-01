var table = null;
var arrData = [];

$(document).ready(function () {
    ClearScreen();
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
                //$('#City').append($('<option/>').val('0').text('-- Select --'));
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
                    //$('#District').append($('<option/>').val('0').text('-- Select --'));
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
                        //$('#Urban').append($('<option/>').val('0').text('-- Select --'));
                        $.each(data, function (i, val) {
                            $('#Urban').append($('<option/>').val(val.urban).text(val.urban))
                        });
                    }
                });
                //$('#Urban').on('change', function () {
                //    //var getZip = $('#Urban').val();
                //    //$.ajax({
                //    //    url: "/address/LoadZipCode",
                //    //    type: "Get",
                //    //    data: { name: getZip },
                //    //    success: function (result) {
                //    //        //debugger;
                //    //        console.log(result.zipCode);
                //    //        $('#Zipcode').val(result.zipCode);
                //    //    }
                //    //});

                //});

            });
        });

    });

    table = $('#account').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/account/LoadData",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "data": "id",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                "sortable": false,
                "data": "roleName"
            },
            {
                "render": function (data, type, row) {
                    return row.nik + '<br /> Name: ' + row.name + '<br />Email: ' + row.email
                }
            },
            {
                "sortable": false,
                "data": "site"
            },
            { "data": "phone" },
            {
                "sortable": false,
                "data": "departmentName"
            },
            { "data": "address" },
            {
                "sortable": false,
                "render": function (data, type, row, meta) {
                    //console.log(row);
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<div class="form-button-action">'
                        + '<button class="btn btn-link btn-lg btn-warning" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + meta.row + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                        + '&nbsp;'
                        + '<button class="btn btn-link btn-lg btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + meta.row + ')" ><i class="fa fa-lg fa-times"></i></button>'
                        + '</div>'
                }
            }
        ],
        initComplete: function () {
            this.api().columns(1).every(function () {
                var column = this;
                var select = $('<select class="form-control"><option value="">ALL</option></select>')
                    .appendTo($(column.header()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
            this.api().columns(3).every(function () {
                var column = this;
                var select = $('<select class="form-control"><option value="">ALL</option></select>')
                    .appendTo($(column.header()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
            this.api().columns(5).every(function () {
                var column = this;
                var select = $('<select class="form-control"><option value="">ALL</option></select>')
                    .appendTo($(column.header()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
    });


});

function ClearScreen() {
    $('#Id').val('');
    $('#NIK').val('');
    $('#Name').val('');
    $('#Email').val('');
    $('#Pass').val('');
    $('#confirmPass').val('');
    $('#Site').val('');
    $('#Phone').val('');

    $('#Address').val('');
    $('#Province').val('0');
    $('#City').val('0');
    $('#District').val('0');
    $('#Urban').val('0');
    $('#Zipcode').val('');

    $('#RoleOption').val('0');
    $('#DeptOption').val('0');

    $('#update').hide();
    $('#add').show();
}

function LoadData(element) {
    //debugger;
    if (arrData.length === 0) {
        if (element[0].name == 'RoleOption') {
            $.ajax({
                type: "Get",
                url: "/role/LoadData",
                success: function (data) {
                    //debugger;
                    arrData = data;
                    renderData(element);
                }
            });
        } else if (element[0].name == 'DeptOption') {
            $.ajax({
                type: "Get",
                url: "/department/LoadData",
                success: function (data) {
                    arrData = data;
                    renderData(element);
                }
            });
        } else if (element[0].name === 'Province') {
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
    } else if (element[0].name == 'DeptOption') {
        $.each(arrData, function (i, val) {
            $option.append($('<option/>').val(val.id).text(val.name))
        });
    } else {
        $.each(arrData, function (i, val) {
            $option.append($('<option/>').val(val.name).text(val.name))
        });
    }
}

LoadData($('#RoleOption'))
LoadData($('#DeptOption'))
LoadData($('#Province'))

//user
function GetById(number) {
    //debugger;
    var getid = table.row(number).data().id;
    $.ajax({
        url: "/account/GetById/",
        data: { Id: getid }
    }).then((result) => {
        debugger;
        $('#Id').val(result.id);
        $('#NIK').val(result.nik);
        $('#Email').val(result.email);
        $('#Name').val(result.name);
        $('#Site').val(result.site);
        $('#Phone').val(result.phone);
        $('#Address').val(result.address);
        $('#Province').val(result.province);
        $('#City').val(result.city);
        $('#District').val(result.subDistrict);
        $('#Urban').val(result.village);
        $('#Zipcode').val(result.zipCode);
        $('#RoleOption').val(result.roleName);
        $('#DeptOption').val(result.departmentID);
        $('#add').hide();
        $('#update').show();
        $('#myModal').modal('show');
    })
}

function Save() {
    if ($('#confirmPass').val() == $('#Pass').val()) {
        //debugger;
        var Data = new Object();
        Data.NIK = $('#NIK').val();
        Data.Email = $('#Email').val();
        Data.Password = $('#Pass').val();
        Data.Name = $('#Name').val();
        Data.Site = $('#Site').val();
        Data.Phone = $('#Phone').val();
        Data.Address = $('#Address').val();
        Data.Province = $('#Province').val();
        Data.City = $('#City').val();
        Data.SubDistrict = $('#District').val();
        Data.Village = $('#Urban').val();
        Data.ZipCode = $('#Zipcode').val();
        Data.RoleName = $('#RoleOption').val();
        Data.DepartmentID = $('#DeptOption').val();
        $.ajax({
            type: 'POST',
            url: "/account/InsertOrUpdate/",
            cache: false,
            dataType: "JSON",
            data: Data
        }).then((result) => {
            //debugger;
            if (result.isSuccessStatusCode == true) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Data inserted Successfully',
                    showConfirmButton: false,
                    timer: 1500,
                })
                table.ajax.reload(null, false);
                ClearScreen();
            } else {
                Swal.fire('Error', result.msg, 'error');
                ClearScreen();
            }
        })
    } else {
        Swal.fire('Error', 'Password Not Same', 'error');
    }
}

function Update() {
    //debugger;
    var Data = new Object();
    if ($('#Pass').val() != "") {
        if ($('#confirmPass').val() == $('#Pass').val()) {
            //debugger;
            Data.Id = $('#Id').val();
            Data.NIK = $('#NIK').val();
            Data.Email = $('#Email').val();
            Data.Password = $('#Pass').val();
            Data.Name = $('#Name').val();
            Data.Site = $('#Site').val();
            Data.Phone = $('#Phone').val();
            Data.Address = $('#Address').val();
            Data.Province = $('#Province').val();
            Data.City = $('#City').val();
            Data.SubDistrict = $('#District').val();
            Data.Village = $('#Urban').val();
            Data.ZipCode = $('#Zipcode').val();
            Data.RoleName = $('#RoleOption').val();
            Data.DepartmentID = $('#DeptOption').val();
            $.ajax({
                type: 'POST',
                url: "/account/InsertOrUpdate/",
                cache: false,
                dataType: "JSON",
                data: Data
            }).then((result) => {
                //debugger;
                if (result.isSuccessStatusCode == true) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Data Updated Successfully',
                        showConfirmButton: false,
                        timer: 1500,
                    });
                    ClearScreen();
                    table.ajax.reload(null, false);
                } else {
                    Swal.fire('Error', result.msg, 'error');
                    ClearScreen();
                }
            })
        } else {
            Swal.fire('Error', 'Password Not Match', 'error');
            ClearScreen();
        }
    } else {
        //debugger;
        Data.Id = $('#Id').val();
        Data.NIK = $('#NIK').val();
        Data.Email = $('#Email').val();
        Data.Name = $('#Name').val();
        Data.Site = $('#Site').val();
        Data.Phone = $('#Phone').val();
        Data.Address = $('#Address').val();
        Data.Province = $('#Province').val();
        Data.City = $('#City').val();
        Data.SubDistrict = $('#District').val();
        Data.Village = $('#Urban').val();
        Data.ZipCode = $('#Zipcode').val();
        Data.RoleName = $('#RoleOption').val();
        Data.DepartmentID = $('#DeptOption').val();
        $.ajax({
            type: 'POST',
            url: "/account/InsertOrUpdate/",
            cache: false,
            dataType: "JSON",
            data: Data
        }).then((result) => {
            //debugger;
            if (result.isSuccessStatusCode == true) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Data Updated Successfully',
                    showConfirmButton: false,
                    timer: 1500,
                });
                ClearScreen();
                table.ajax.reload(null, false);
            } else {
                Swal.fire('Error', result.msg, 'error');
                ClearScreen();
            }
        })

    }
}

function Delete(number) {
    var getId = table.row(number).data().id;
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
    }).then((resultSwal) => {
        if (resultSwal.value) {
            //debugger;
            $.ajax({
                url: "/account/Delete/",
                data: { id: getId }
            }).then((result) => {
                //debugger;
                if (result.statusCode == 200) {
                    //debugger;
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Delete Successfully',
                        showConfirmButton: false,
                        timer: 1500,
                    });
                    table.ajax.reload(null, false);
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
    });
}