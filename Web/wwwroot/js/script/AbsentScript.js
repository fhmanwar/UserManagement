var table = null;
$(document).ready(function () {
    
    table = $('#Presence').DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "autoWidth": true,
        "ordering": true,
        //"pageLength": 5,
        //"lengthChange": false,
        //"searching": false,
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
            { "data": "name" },
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
                "data": "insAt",
                'render': function (jsonDate) {
                    var date = new Date(jsonDate);
                    return moment(date).format('DD MMMM YYYY');
                }
            },
        ],
    });

    table.order([0, 'desc']).draw();
    $.fn.dataTableExt.afnFiltering.push(
        function (settings, data, dataIndex) {
            //debugger;
            //var min = $('#min-date').val()
            //var max = $('#max-date').val()
            //var createdAt = data[0] || 0; // Our date column in the table
            ////createdAt=createdAt.split(" ");
            //var startDate = moment(min, "DD/MM/YYYY");
            //var endDate = moment(max, "DD/MM/YYYY");
            //var diffDate = moment(createdAt, "DD/MM/YYYY");
            //if ( (min == "" || max == "") || (diffDate.isBetween(startDate, endDate)) ) {
            //    return true;
            //}
            var today = new Date(); 
            var todayDate = moment(today).format('DD MMMM YYYY');
            var createdAt = data[5] || 5; // Our date column in the table
            var insDate = new Date(createdAt);
            var colDate = moment(insDate).format('DD MMMM YYYY');
            //console.log(colDate);
            if (todayDate == colDate) {
                return true;
            }
            return false;
        }
    )

    //debugger;
    ////var cek = table.column(1).search('admin01').draw();
    //var cek2 = table.column(1).search("admin01");
    //var idx = table.columns(1).data().eq(0).indexOf('admin01');

    //if (idx === -1) {
    //    alert('Yes not found');
    //}
    //else {
    //    alert('Yes was found');
    //}
    //$.ajax({
    //    url: "/Attendance/LoadData/",
    //    data: { Id: 'admin01' }
    //}).then((result) => {
    //    //debugger;
    //    var today = new Date();
    //    var todayDate = moment(today).format('DD MMMM YYYY');
    //    $.each(result, function (i, val) {
    //        //debugger;
    //        var resDate = new Date(val.insAt)
    //        var getDate = moment(resDate).format('DD MMMM YYYY');
    //        if (getDate == todayDate) {
    //            console.log(todayDate);
    //        }
    //    });
    //})
});




var arg = {
    resultFunction: function (result) {
        debugger;
        var getUserId = result.code;
        $.ajax({
            url: "/Attendance/loaddata/",
        }).then((result) => {
            debugger;
            var today = new Date();
            var todayDate = moment(today).format('DD MMMM YYYY');
            //var yesterday = new Date("2020-09-30");
            //var ystDate = moment(yesterday).format('DD MMMM YYYY');

            var arruserId = [];
            var searchObj = [];
            result.forEach(function (one) {
                //debugger;
                var resDate = new Date(one.insAt)
                var getDate = moment(resDate).format('DD MMMM YYYY');
                if (todayDate == getDate && getUserId == one.userId) {
                    //searchArr[one.name] = one;
                    searchObj = one;
                    arruserId = one.userId;
                }
            })
            //console.log(searchArr[getUserId]);

            var cekUsrId = arruserId.indexOf(getUserId);
            var insertDate = new Date(searchObj.insAt);
            var insDate = moment(insertDate).format('DD MMMM YYYY');

            var fill = new Object;
            if (todayDate == insDate) {
                if (cekUsrId != -1 && searchObj.id != 0) {
                    fill.Id = searchObj.id;
                    fill.UserId = searchObj.userId;
                    fill.InsAt = searchObj.insAt;
                    fill.UpdAt = searchObj.updAt;
                    $.ajax({
                        url: "/Attendance/InsertOrUpdate",
                        cache: false,
                        dataType: "JSON",
                        data: fill,
                    }).then((result) => {
                        //debugger;
                        if (result.isSuccessStatusCode == true) {
                            Swal.fire({
                                position: 'center',
                                icon: 'success',
                                title: 'Data Updated Successfully',
                                showConfirmButton: false,
                                timer: 1500,
                            })
                            table.ajax.reload(null, false);
                        } else {
                            Swal.fire('Error', result.msg, 'error');
                        }
                    });
                } 
            } else if (todayDate) {
                if (cekUsrId == -1 && searchObj.id == null) {
                    fill.UserId = getUserId;
                    $.ajax({
                        url: "/Attendance/InsertOrUpdate",
                        cache: false,
                        dataType: "JSON",
                        data: fill,
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
                        } else {
                            Swal.fire('Error', result.msg, 'error');
                        }
                    });
                }
            } else {
                Swal.fire('Error', 'something wrong', 'error');
            }
        })

        //$('#Presence').dataTable().fnAddData([
        //    result.format,
        //    result.code
        //]);
    }
};
var decoder = $("#scanCode").WebCodeCamJQuery(arg).data().plugin_WebCodeCamJQuery;
decoder.play();
decoder.options.zoom = 1;
decoder.options.autoBrightnessValue = false;
decoder.options.contrast = 30; 




//var video = document.querySelector("#scanBarcode");
//// minta izin user
//navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia || navigator.oGetUserMedia;

//// jika user memberikan izin
//if (navigator.getUserMedia) {
//    // jalankan fungsi handleVideo, dan videoError jika izin ditolak
//    navigator.getUserMedia({ video: true }, handleVideo, videoError);
//}

//// fungsi ini akan dieksekusi jika  izin telah diberikan
//function handleVideo(stream) {
//    video.srcObject = stream;
//}

//// fungsi ini akan dieksekusi kalau user menolak izin
//function videoError(e) {
//    // do something
//    alert("Izinkan menggunakan webcam untuk demo!")
//}
