var table = null;
var arrDepart = [];

$(document).ready(function () {
    table = $('#Divisions').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/division/loaddata",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { "data": "name" },
            {
                "data": "createDate",
                'render': function (jsonDate) {
                    var date = new Date(jsonDate);
                    return moment(date).format('DD MMMM YYYY') + '<br> Time : ' + moment(date).format('HH: mm');
                }
            },
            {
                "data": "updateDate",
                'render': function (jsonDate) {
                    var date = new Date(jsonDate);
                    if (date.getFullYear() != 0001) {
                        return moment(date).format('DD MMMM YYYY') + '<br> Time : ' + moment(date).format('HH:mm');
                    }
                    return "Not updated yet";
                }
            },
            {
                "sortable": false,
                "render": function (data, type, row, meta) {
                    //console.log(row);
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button class="btn btn-outline-warning btn-round" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + meta.row + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                        + '&nbsp;'
                        + '<button class="btn btn-outline-danger btn-round" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + meta.row + ')" ><i class="fa fa-lg fa-times"></i></button>'
                }
            }
        ],
        
    });
});

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#update').hide();
    $('#add').show();
    $('#myModal').modal('show');
}

//function LoadDepart(element) {
//    //debugger;
//    if (arrDepart.length === 0) {
//        $.ajax({
//            type: "Get",
//            url: "/departments/loadDepart",
//            success: function (data) {
//                arrDepart = data;
//                renderDepart(element);
//            }
//        });
//    }
//    else {
//        renderDepart(element);
//    }
//}

//function renderDepart(element) {
//    var $option = $(element);
//    $option.empty();
//    $option.append($('<option/>').val('0').text('Select Department').hide());
//    $.each(arrDepart, function (i, val) {
//        $option.append($('<option/>').val(val.id).text(val.name))
//    });
//}

//LoadDepart($('#DepartOption'))

function GetById(number) {
    //debugger;
    var getid = table.row(number).data().id;
    $.ajax({
        url: "/division/GetById/",
        data: { id: getid }
    }).then((result) => {
        //debugger;
        $('#Id').val(result.id);
        $('#Name').val(result.name);
        //$('#DepartOption').val(result.department.id)
        $('#add').hide();
        $('#update').show();
        $('#myModal').modal('show');
    })
}

function Save() {
    //debugger;
    var Div = new Object();
    Div.Id = null;
    Div.Name = $('#Name').val();
    //Div.DepartmentID = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/division/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: Div
    }).then((result) => {
        //debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data inserted Successfully',
                showConfirmButton: false,
                timer: 1500,
            })
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Update() {
    //debugger;
    var Div = new Object();
    Div.Id = $('#Id').val();
    Div.Name = $('#Name').val();
    //Div.DepartmentID = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/division/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: Div
    }).then((result) => {
        //debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data Updated Successfully',
                showConfirmButton: false,
                timer: 1500,
            });
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Delete(number) {
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
            var getid = table.row(number).data().id;
            $.ajax({
                url: "/division/Delete/",
                data: { id: getid }
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