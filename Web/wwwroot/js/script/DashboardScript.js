var table = null;
$(document).ready(function () {

    $.ajax({
        url: "/account/LoadData",
        type: "Get",
        success: function (data) {
            Circles.create({
                id: 'circles-1',
                radius: 80,
                value: data.length,
                width: 7,
                text: function (value) { return value; },
                colors: ['#f1f1f1', '#FF9E27'],
                duration: 500,
                wrpClass: 'circles-wrp',
                textClass: 'circles-text',
                styleWrapper: true,
                styleText: true
            });
        }
    });

    $.ajax({
        url: "/role/LoadData",
        type: "Get",
        success: function (data) {
            Circles.create({
                id: 'circles-2',
                radius: 80,
                value: data.length,
                width: 7,
                text: function (value) { return value; },
                colors: ['#f1f1f1', '#2BB930'],
                duration: 500,
                wrpClass: 'circles-wrp',
                textClass: 'circles-text',
                styleWrapper: true,
                styleText: true
            });
        }
    });

    $.ajax({
        url: "/department/LoadData",
        type: "Get",
        success: function (data) {
            Circles.create({
                id: 'circles-3',
                radius: 80,
                value: data.length,
                width: 7,
                text: function (value) { return value; },
                colors: ['#f1f1f1', '#F25961'],
                duration: 500,
                wrpClass: 'circles-wrp',
                textClass: 'circles-text',
                styleWrapper: true,
                styleText: true
            });
        }
    });

    table = $('#Logs').DataTable({
        //"processing": true,
        "serverSide": true,
        "responsive": true,
        "searching": false,
        "paging": false,
        "ajax": {
            url: "/Dashboard/LoadLog",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "sortable": false,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                "sortable": false,
                "data": "createDate",
                'render': function (jsonDate) {
                    var date = new Date(jsonDate);
                    return moment(date).format('DD MMMM YYYY') + '<br> Time : ' + moment(date).format('HH: mm');
                }
            },
            { "data": "email" },
            { "data": "response" },
        ],
        scrollY: 500,
        scroller: {
            loadingIndicator: true
        },
    });
    setInterval(function () {
        table.ajax.reload(null, false);
    }, 3000);

});


function getAllRole() {
    $.ajax({
        url: "/role/LoadData",
        type: "Get",
        success: function (data) {
            debugger;
            return data.length;
        }
    });
}

am4core.useTheme(am4themes_animated);

var PieRole = am4core.createFromConfig({
    "innerRadius": "50%",

    "dataSource": {
        "url": "/dashboard/LoadPieURole",
        "parser": {
            "type": "JSONParser",
        },
        "reloadFrequency": 5000,
    },

    "exporting": {
        "menu": {
            "items": [{
                "label": "...",
                "menu": [
                    {
                        "label": "Image",
                        "menu": [
                            { "type": "png", "label": "PNG" },
                            { "type": "jpg", "label": "JPG" },
                            { "type": "pdf", "label": "PDF" }
                        ]
                    }, {
                        "label": "Data",
                        "menu": [
                            { "type": "json", "label": "JSON" },
                            { "type": "csv", "label": "CSV" },
                            { "type": "xlsx", "label": "XLSX" },
                            { "type": "html", "label": "HTML" },
                            { "type": "pdfdata", "label": "PDF Data" }
                        ]
                    }, {
                        "label": "Print", "type": "print"
                    }
                ]
            }]
        },
        "filePrefix": "Pie_Chart_User_Department-" + moment().format("DD-MM-YYYY"),
    },

    // Create series
    "series": [{
        "type": "PieSeries",
        "dataFields": {
            "value": "total",
            "category": "roleName",
        },
        "slices": {
            "cornerRadius": 10,
            "innerCornerRadius": 7
        },
        "hiddenState": {
            "properties": {
                // this creates initial animation
                "opacity": 1,
                "endAngle": -90,
                "startAngle": -90
            }
        },
        "children": [{
            "type": "Label",
            "forceCreate": true,
            "text": "User/Role",
            "horizontalCenter": "middle",
            "verticalCenter": "middle",
            "fontSize": 26
        }]
    }],

    // Add legend
    "legend": {},

}, "pieURole", am4charts.PieChart);

var PieDept = am4core.createFromConfig({
    "innerRadius": "50%",

    "dataSource": {
        "url": "/dashboard/LoadPieUDiv",
        "parser": {
            "type": "JSONParser",
        },
        "reloadFrequency": 5000,
    },

    "exporting": {
        "menu": {
            "items": [{
                "label": "...",
                "menu": [
                    {
                        "label": "Image",
                        "menu": [
                            { "type": "png", "label": "PNG" },
                            { "type": "jpg", "label": "JPG" },
                            { "type": "pdf", "label": "PDF" }
                        ]
                    }, {
                        "label": "Data",
                        "menu": [
                            { "type": "json", "label": "JSON" },
                            { "type": "csv", "label": "CSV" },
                            { "type": "xlsx", "label": "XLSX" },
                            { "type": "html", "label": "HTML" },
                            { "type": "pdfdata", "label": "PDF Data" }
                        ]
                    }, {
                        "label": "Print", "type": "print"
                    }
                ]
            }]
        },
        "filePrefix": "Pie_Chart_User_Department-" + moment().format("DD-MM-YYYY"),
    },

    // Create series
    "series": [{
        "type": "PieSeries",
        "dataFields": {
            "value": "total",
            "category": "departmentName",
        },
        "slices": {
            "cornerRadius": 10,
            "innerCornerRadius": 7
        },
        "hiddenState": {
            "properties": {
                // this creates initial animation
                "opacity": 1,
                "endAngle": -90,
                "startAngle": -90
            }
        },
        "children": [{
            "type": "Label",
            "forceCreate": true,
            "text": "User/Department",
            "horizontalCenter": "middle",
            "verticalCenter": "middle",
            "fontSize": 26
        }]
    }],

    // Add legend
    "legend": {},

}, "pieUDept", am4charts.PieChart);

//function exportXLS() {
//    PieDept.exporting.export("xlsx");
//}
//function exportPDF() {
//    PieDept.exporting.export("pdf");
//}
//function exportPNG() {
//    PieDept.exporting.export("png");
//}