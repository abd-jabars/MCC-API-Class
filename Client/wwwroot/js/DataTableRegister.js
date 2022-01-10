﻿$(document).ready(function () {
    $('#dataTabelEmployee').DataTable({
        'scrollX': true,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                className: 'btn btn-primary',
                text: '<i class="fa fa-files-o"> Copy </i>',
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'excel',
                className: 'btn btn-primary',
                text: '<i class="fa fa-file-excel-o"> Excel </i>',
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'pdf',
                className: 'btn btn-primary',
                text: '<i class="fa fa-file-pdf-o"> Pdf </i>',
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'print',
                className: 'btn btn-primary',
                text: '<i class="fa fa-print"> Print </i>',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                }
            }
        ],
        'ajax': {
            'url': "https://localhost:44351/API/Employees/Register",
            'dataType': 'json',
            'dataSrc': 'result'
        },
        'columns': [
            {
                'data': null,
                'bSortable': false,
                'render': (data, type, row, meta) => {
                    return (meta.row + meta.settings._iDisplayStart + 1);
                }
            },
            {
                'data': 'fullName',
                'width': '100px'
            },
            {
                'data': null,
                'render': (data, type, row) => {
                    var getDate = new Date(row['birthDate']);
                    return getDate.toLocaleDateString();
                }
            },
            {
                'data': null,
                'render': function (data, type, row) {
                    if (row['gender'] == 0) {
                        return row['gender'] = "Male"
                    }
                    else {
                        return row['gender'] = "Female"
                    }
                }
            },
            {
                'data': 'phone'
            },
            {
                'data': 'email'
            },
            {
                'data': 'universityName',
                'width': '150px',
            },
            {
                'data': 'degree'
            },
            {
                'data': 'gpa'
            },
            {
                'data': 'roleName'
            },
            {
                'data': null,
                'render': function (data, type, row) {
                    return "Rp " + row['salary']
                }
            },
            {
                'data': null,
                'render': function (data, type, row) {
                    return `<button data-toggle="modal" data-target="#getEmployeeDetail" class="d-inline btn btn-primary fa fa-search" onclick="getDetails()"></button>
                            <button data-toggle="modal" data-target="#getEmployeeDetail" class="d-inline btn btn-warning fa fa-pencil" onclick=""></button>
                            <button data-toggle="modal" data-target="#getEmployeeDetail" class="d-inline btn btn-danger fa fa-trash" onclick=""></button>`
                }
            }
        ]
    });
});

function getDetails() {
    $.ajax({
        url: "https://localhost:44351/API/Employees/Register/NIK"

    }).done((result) => {
        console.log(result);
        console.log();

}).fail((error) => {
    console.log(error);
});
}