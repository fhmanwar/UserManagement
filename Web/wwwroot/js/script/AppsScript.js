$(document).ready(function () {
    $('#asset').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/apps/getasset",
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
            { "data": "address" },
            { "data": "departmentName" },
        ],
    });

    $('#exam').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/apps/getexam",
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
            { "data": "address" },
        ],
    });

    $('#interview').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/apps/getinterview",
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
            { "data": "address" },
        ],
    });

    $('#reimburse').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/apps/getreimbursement",
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
            { "data": "address" },
        ],
    });
});