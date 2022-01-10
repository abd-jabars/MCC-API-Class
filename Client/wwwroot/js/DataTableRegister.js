$(document).ready(function () {
    $('#dataTabelEmployee').DataTable({
        'scrollX': true,
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
                'data': 'birthDate',
                'width': '100px'
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
                'data': 'email'
            },
            {
                'data': 'roleName'
            },
            {
                'data': 'salary',
                'width': '110px',
                'render': $.fn.dataTable.render.number('.', ',', 0, 'Rp ')
            },
            {
                'data': null,
                'width': '150px',
                'render': function (data, type, row) {
                    return `<button data-toggle="modal" data-target="#getEmployeeDetail" class="d-inline btn btn-primary fa fa-info" onclick=""></button>
                            <button data-toggle="modal" data-target="#getEmployeeDetail" class="d-inline btn btn-warning fa fa-pencil" onclick=""></button>
                            <button data-toggle="modal" data-target="#getEmployeeDetail" class="d-inline btn btn-danger fa fa-trash" onclick=""></button>`
                }
            }
        ],
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
        ]
    });
});

function Insert() {
    $.ajax({
        url: "https://localhost:44351/API/Universities"

    }).done((result) => {
        var univOpt = "";

        $.each(result, function (key, val) {
            univOpt += `<option value="${val.universityId}">${val.universityName}</option>`
        });
        $("#university").html(univOpt);

    }).fail((error) => {
        console.log(error);
    });
}

function submitForm() {
    var firstName = $('#firstName').val();
    var lastName = $('#lastName').val();
    var email = $('#email').val();
    var password = $('#password').val();
    var phone = $('#phone').val();
    var birthDate = $('#birthDate').val();
    var gender = $('#gender').val();
    var university = $('#university').val();
    var degree = $('#degree').val();
    var gpa = $('#gpa').val();
    var salary = $('#salary').val();

    var registeredData = Object();
    registeredData.FirstName = firstName;
    registeredData.LastName = lastName;
    if (gender == "Male") {
        registeredData.Gender = 1;
    } else {
        registeredData.Gender = 2;
    }
    registeredData.BirthDate = birthDate;
    registeredData.Phone = phone;
    registeredData.Email = email;
    registeredData.Password = password;
    registeredData.Degree = degree;
    registeredData.GPA = gpa;
    //registeredData.UniversityId = parseInt(university+1);
    registeredData.UniversityId = parseInt(university);
    registeredData.Salary = parseInt(salary);

    console.log(registeredData);

    var myTable = $('#dataTabelEmployee').DataTable();
    $.ajax({
        url: "https://localhost:44351/API/Employees/Register",
        contentType: "application/json;charset=utf-8",
        type: "POST",
        data: JSON.stringify(registeredData)
    }).done((result) => {
        console.log(result);
        console.log(result.message);
        //alert(result.message);
        myTable.ajax.reload();
        Swal.fire({
            icon: 'success',
            title: 'Success',
            text: result.message,
            type: 'success'
        });
    }).fail((error) => {
        console.log(error);
        console.log(result.message);
        //alert(result.message);
        Swal.fire({
            icon: 'error',
            title: 'Something went wrong',
            text: result.message,
            type: 'error'
        });
    });
}

$('#form1').submit(function (e) {
    e.preventDefault();

    submitForm();
    $('#submitButton').modal('toggle');
});

//$('#form1').validate({
//    rules: {
//        errorClass: "error",
//        'email': {
//            required: true,
//            email: true
//        },
//        'password': {
//            required: true,
//            minlength: 8
//        }
//    },
//    messages: {
//        'password': {
//            required: "This field is required",
//            minlength: "Your password must be at least 8 character long"
//        }
//    }
//});

//function getDetails() {
//    $.ajax({
//        url: "https://localhost:44351/API/Employees/Register/NIK"

//    }).done((result) => {
//        console.log(result);
//        console.log();

//}).fail((error) => {
//    console.log(error);
//});
//}