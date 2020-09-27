var baseUrl = "http://127.0.0.1:8000/api/";
$(document).ready(function () {

    //GetData()
    //GetKota(32);
    //GetKecamatan('BEKASI');
    //GetKelurahan('BEKASI BARAT');
    GetZipCode('BINTARA JAYA');
});

function GetData() {
    debugger;
    $.ajax({
        url: "/address/LoadProvince",
        type: "GET",
        dataType: "json",
    }).then((result) => {
        debugger;
        console.log(result);
    })
}

function GetKota(Id) {
    debugger;
    $.ajax({
        url: "/address/loadcity",
        type: "GET",
        dataType: "json",
        data: { name: Id }
    }).then((result) => {
        debugger;
        console.log(result);
    })
}

function GetKecamatan(name) {
    debugger;
    $.ajax({
        url: "/address/LoadDistrict",
        type: "GET",
        dataType: "json",
        data: { name: name }
    }).then((result) => {
        debugger;
        console.log(result);
    })
}

function GetKelurahan(name) {
    debugger;
    $.ajax({
        url: "/address/LoadUrban",
        type: "GET",
        dataType: "json",
        data: { name: name }
    }).then((result) => {
        debugger;
        console.log(result);
    })
}

function GetZipCode(name) {
    debugger;
    $.ajax({
        url: "/address/LoadZipCode",
        type: "GET",
        dataType: "json",
        data: { name: name }
    }).then((result) => {
        debugger;
        console.log(result);
    })
}