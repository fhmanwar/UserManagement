var table = null 
$(document).ready(function () {

    table = $('#Presence').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/attendance/LoadData",
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
            { "data": "userId" },
            {
                "sortable": false,
                "data": "name"
            },
            {
                "data": "insAt",
                'render': function (jsonDate) {
                    var date = new Date(jsonDate);
                    if (date.getFullYear() != 0001) {
                        return moment(date).format('HH:mm');
                    }
                    return 'N/A';
                }
            },
            {
                "data": "updAt",
                'render': function (jsonDate) {
                    var date = new Date(jsonDate);
                    if (date.getFullYear() != 0001) {
                        return moment(date).format('HH:mm');
                    }
                    return 'N/A';
                }
            },
            {
                "sortable": false,
                "data": "insAt",
                'render': function (jsonDate) {
                    return moment(jsonDate).format('DD MMMM YYYY');
                }
            },
        ],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdfHtml5',
                text: '<i class="fas fa-file-pdf " data-toggle="tooltip" data-placement="top" title="PDF" ></i>',
                className: 'btn btn-danger btn-border',
                title: 'Presence Report',
                filename: 'Presence Report ' + moment(),
                exportOptions: {
                    //format: {
                    //    body: function (data, row, column, node) {
                    //        // Strip $ from salary column to make it numeric
                    //        //return column === 5 ? data.replace(/[$,]/g, '') : data;
                    //        return column === 2 ? data.replace(/[$,]/g, '') : data;
                    //    }
                    //},
                    columns: [0, 1, 2, 3, 4, 5],
                    search: 'applied',
                    order: 'applied',
                },
                customize: function (doc) {
                    //doc.content.splice(1, 0, {
                    //    margin: [0, 0, 0, 12],
                    //});
                    //debugger;
                    //doc.content[1].margin = [150, 0, 130, 0]  //left, top, right, bottom
                    var rowCount = doc.content[1].table.body.length;
                    for (i = 1; i < rowCount; i++) {
                        doc.content[1].table.body[i][2].alignment = 'center';
                    };
                    doc.content[1].table.body[0][0].text = 'No.';
                    doc.content[1].table.body[0][2].text = 'Name';
                    doc.content[1].table.body[0][5].text = 'Date';
                    doc['footer'] = (function (page, pages) {
                        return {
                            columns: [
                                'This is your left footer column',
                                {
                                    // This is the right column
                                    alignment: 'right',
                                    text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                }
                            ],
                            margin: [10, 0]
                        }
                    });
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel" data-toggle="tooltip" data-placement="Bottom" title="Excel"></i>',
                className: 'btn btn-success btn-border',
                title: 'Presence Report',
                filename: 'Presence Report ' + moment(),
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5],
                    search: 'applied',
                    order: 'applied',
                },
                customize: function (excel) {
                    debugger;
                    var sheet = excel.xl.worksheets['sheet1.xml'];
                    $('c[r=A2] t', sheet).text('No.');
                    $('c[r=C2] t', sheet).text('Name');
                    $('c[r=F2] t', sheet).text('Date');
                }
            },
            {
                extend: 'csvHtml5',
                text: '<i class="far fa-file-alt" data-toggle="tooltip" data-placement="Bottom" data-animation="true" title="CSV"></i>',
                className: 'btn btn-info btn-border',
                title: 'Presence Report',
                filename: 'Presence Report ' + moment(),
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5],
                    search: 'applied',
                    order: 'applied',
                },
            },
            {
                extend: 'print',
                text: '<i class="fas fa-print" data-toggle="tooltip" data-placement="Bottom" data-animation="false" title="Print"></i>',
                className: 'btn btn-secondary btn-border',
                title: 'Presence Report',
            }
        ],
        initComplete: function () {
            this.api().columns(2).every(function () {
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
                    select.append('<option value="' + moment(d).format('DD MMMM YYYY') + '">' + moment(d).format('DD MMMM YYYY') + '</option>')
                });
            });
        }

    });

    table.order([0, 'desc']).draw();


});