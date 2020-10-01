var table = null;

$(document).ready(function () {
    table = $('#role').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "http://wilayahindo.herokuapp.com/api/subprovinces",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                render: function (data, type, row, meta) {
                    //console.log(row);
                    return meta.row + meta.settings._iDisplayStart + 1;
                    //return meta.row + 1;
                }
            },
            { "data": "provinceName" },
            { "data": "city" },
            { "data": "district" },
            { "data": "urban" },
            { "data": "zipCode" },
        ],
    });

    //GetData()
    //GetKota(32);
    //GetKecamatan('BEKASI');
    //GetKelurahan('BEKASI BARAT');
    GetZipCode('BINTARA JAYA');
});

function GetData() {
    //debugger;
    $.ajax({
        url: "/address/LoadProvince",
        type: "GET",
        dataType: "json",
    }).then((result) => {
        //debugger;
        console.log(result);
    })
}

function GetKota(Id) {
    //debugger;
    $.ajax({
        url: "/address/loadcity",
        type: "GET",
        dataType: "json",
        data: { name: Id }
    }).then((result) => {
        //debugger;
        console.log(result);
    })
}

function GetKecamatan(name) {
    //debugger;
    $.ajax({
        url: "/address/LoadDistrict",
        type: "GET",
        dataType: "json",
        data: { name: name }
    }).then((result) => {
        //debugger;
        console.log(result);
    })
}

function GetKelurahan(name) {
    //debugger;
    $.ajax({
        url: "/address/LoadUrban",
        type: "GET",
        dataType: "json",
        data: { name: name }
    }).then((result) => {
        //debugger;
        console.log(result);
    })
}

function GetZipCode(name) {
    //debugger;
    $.ajax({
        url: "/address/LoadZipCode",
        type: "GET",
        dataType: "json",
        data: { name: name }
    }).then((result) => {
        //debugger;
        console.log(result);
    })
}