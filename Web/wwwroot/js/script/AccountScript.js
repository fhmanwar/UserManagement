var table = null;
var tRole = null;
var arrData = [];
var arrProvince = [];

$(document).ready(function () {
    //ClearScreen();
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

    tRole = $('#role').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/role/LoadData",
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
            { "data": "name" },
            {
                "data": "insAt",
                'render': function (jsonDate) {
                    var date = new Date(jsonDate);
                    if (date.getFullYear() != 0001) {
                        return moment(date).format('DD MMMM YYYY') + '<br> Time : ' + moment(date).format('HH: mm');
                    }
                    return 'Date Not Define';
                }
            },
            {
                "data": "updAt",
                'render': function (jsonDate) {
                    var date = new Date(jsonDate);
                    if (date.getFullYear() != 0001) {
                        return moment(date).format('DD MMMM YYYY') + '<br> Time : ' + moment(date).format('HH:mm');
                    }
                    return "Not updated yet";
                }
            },
        ],
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