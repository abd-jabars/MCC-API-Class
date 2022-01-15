$(document).ready(function () {
    $('#dataTabelEmployee').DataTable({
        'scrollX': true,
        'ajax': {
            //'url': "https://localhost:44351/API/Employees/Register",
            'url': "https://localhost:44388/Employees/GetRegistered",
            'dataType': 'json',
            //'dataSrc': 'result'
            'dataSrc': ''
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
                'data': 'nik'
            },
            {
                'data': null,
                'width': '100px',
                'render': function (data, type, row) {
                    return row['firstName'] + ' ' + row['lastName']
                }
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
                    return `<button data-toggle="modal" data-target="#detailEmployee" class="d-inline btn btn-primary fa fa-info" onclick="Detail(${row["nik"]})"></button>
                            <button data-toggle="modal" data-target="#registerNewEmployee" class="d-inline btn btn-warning fa fa-pencil" onclick="Update(${row["nik"]})"></button>
                            <button data-toggle="modal" data-target="#getEmployeeDetail" class="d-inline btn btn-danger fa fa-trash" onclick="Delete(${row["nik"]})"></button>`;
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
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'excel',
                className: 'btn btn-primary',
                text: '<i class="fa fa-file-excel-o"> Excel </i>',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'pdf',
                className: 'btn btn-primary',
                text: '<i class="fa fa-file-pdf-o"> Pdf </i>',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'print',
                className: 'btn btn-primary',
                text: '<i class="fa fa-print"> Print </i>',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            }
        ]
    });
});

function Insert() {
    $.ajax({
        //url: "https://localhost:44351/API/Universities",
        url: "https://localhost:44388/Universities/GetAll",
    }).done((result) => {
        var univOpt = "";

        $.each(result, function (key, val) {
            univOpt += `<option value="${val.universityId}">${val.universityName}</option>`
        });
        $("#university").html(univOpt);

        $('#password').attr("readonly", false);

        $('#form1').trigger("reset");
        //submitForm();

        var insertTitle = "";
        insertTitle += `<h3 class="mx-auto my-1"> Insert new data </h3>`;
        $("#registerNewEmployee .modal-header").html(insertTitle);

        $("#submitButton").html("Insert");

    }).fail((error) => {
        console.log(error);
    });
}

function GetUniversities() {
    $.ajax({
        //url: "https://localhost:44351/API/Universities"
        url: "https://localhost:44388/Universities/GetAll"

    }).done((result) => {
        //console.log(result);

        var univOpt = "";

        $.each(result, function (key, val) {
            univOpt += `<option value="${val.universityId}">${val.universityName}</option>`
            /*$("#university").html(univOpt);*/
        });
        $("#university").html(univOpt);

    }).fail((error) => {
        console.log(error);
    });
}

function setFormValue(data) {
    var updateTitle = "";
    updateTitle += `<h3 class="mx-auto my-1"> Update data: ${data.nik} - ${data.firstName} ${data.lastName} </h3>`;
    $("#registerNewEmployee .modal-header").html(updateTitle);

    const splitBirthDate = data.birthDate.split("T");

    let nik = data.nik;
    let firstName = data.firstName;
    let lastName = data.lastName;
    //let birthDate = data.birthDate;
    let birthDate = splitBirthDate[0];
    let email = data.email;
    let phone = data.phone;
    let gender = data.gender;
    let university = data.universityId;
    let degree = data.degree;
    //let gpa = data.gpa;
    let gpa = parseFloat(data.gpa);
    let salary = data.salary;

    $("#nik").val(nik);
    $("#firstName").val(firstName);
    $("#lastName").val(lastName);
    $("#university").val(university);
    $("#birthDate").val(birthDate);
    $("#email").val(email);
    $('#password').attr("readonly", true);
    $("#phone").val(phone);
    $("#gender").val(gender);
    $("#degree").val(degree);
    //$("#gpa").val(parseFloat(gpa));
    $("#gpa").val(gpa);
    $("#salary").val(salary);
    $("#submitButton").html("Update");

    //var updateButton = "";
    //updateButton += `<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
    //                <button type="submit" class="btn btn-primary" id="updateButton" onclick="SubmitUpdateForm()">Update</button>`;
    //$(".modal-footer").html(updateButton);
}

function Update(nik) {
    GetUniversities();
    $.ajax({
        //url: "https://localhost:44351/API/Employees/Register/" + nik,
        url: "https://localhost:44388/Employees/GetRegisteredById/" + nik,
        'dataType': 'json',
        'dataSrc': ''
    }).done((result) => {

        console.log(result);

        var data = result;

        setFormValue(data);

    }).fail((error) => {
        console.log(error);
    });
}

function UpdateData() {
    var firstName = $('#firstName').val();
    var lastName = $('#lastName').val();
    var email = $('#email').val();
    var password = $('#password').attr("readonly", true);
    var phone = $('#phone').val();
    var birthDate = $('#birthDate').val();
    var gender = $('#gender').val();
    var university = $('#university').val();
    //var degree = $('#degree').val();
    var degree = $('#degree').val();
    //var gpa = Number($('#gpa').val());
    //var gpa = $('#gpa').val();
    var gpa = parseFloat($('#gpa').val());
    //var gpa = parseFloat($('#gpa').val().replace('.', ','));
    var salary = $('#salary').val();

    var registeredData = Object();
    registeredData.NIK = $("#nik").val();
    registeredData.FirstName = firstName;
    registeredData.LastName = lastName;
    registeredData.Gender = gender;
    registeredData.BirthDate = birthDate;
    registeredData.Phone = phone;
    registeredData.Email = email;
    //registeredData.Password = password;
    registeredData.Degree = degree;
    //registeredData.GPA = parseFloat(gpa);
    registeredData.GPA = gpa;
    registeredData.UniversityId = parseInt(university);
    registeredData.Salary = parseInt(salary);

    console.log(registeredData);
    console.log(gpa);

    //var myJson = JSON.stringify(registeredData)

    //console.log("My Json 1", myJson)

    var myTable = $('#dataTabelEmployee').DataTable();
    $.ajax({
        //url: "https://localhost:44351/API/Employees/Register",
        url: "https://localhost:44388/Employees/Register",
        //contentType: "application/json;charset=utf-8",
        type: "PUT",
        //data: JSON.stringify(registeredData)
        data: registeredData
    }).done((result) => {
        console.log(result);
        //console.log(result.message);
        myTable.ajax.reload();
        var swalIcon;
        if (result.status == 200) {
            swalIcon = 'success';
            swalTitle = 'Success';
        } else {
            swalIcon = 'error'
            swalTitle = 'Oops!'
        }
        Swal.fire({
            icon: swalIcon,
            title: swalTitle,
            text: result.message,
        });
    }).fail((error) => {
        console.log(error);
        //console.log(result.message);
        //alert(result.message);
        Swal.fire({
            icon: 'error',
            title: 'Something went wrong',
            text: error.message,
        });
    });
}

function Delete(nik) {

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        
        if (result.isConfirmed) {
            var myTable = $('#dataTabelEmployee').DataTable();

            $.ajax({
                //url: "https://localhost:44351/API/DeleteRegisteredData/" + nik,
                url: "https://localhost:44388/Employees/DeleteRegisteredData/" + nik,
                type: "DELETE"
            }).done((result) => {

                console.log(result);

                if (result === 200) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                } else {
                    Swal.fire(
                        'Something went wrong',
                        'Hmmmm',
                        'error'
                    )
                }

                myTable.ajax.reload();

            }).fail((error) => {
                console.log(error);
            });
            
        }
    })
}

function submitForm() {
    var firstName = $('#firstName').val();
    var lastName = $('#lastName').val();
    var email = $('#email').val();
    var password = $('#password').val();
    var phone = $('#phone').val();
    var birthDate = $('#birthDate').val();
    //var birthDate = new Date(parseInt($('#birthDate').val().substr(6)));
    //var birthDate = new Date($('#birthDate').val() + "Z").toISOString().substring(0, 10);
    //var gender = $('#gender').val();
    var gender = parseInt($('#gender').val());
    var university = $('#university').val();
    var degree = $('#degree').val();
    //var gpa = $('#gpa').val();
    var gpa = parseFloat($('#gpa').val());
    var salary = $('#salary').val();

    var registeredData = Object();
    registeredData.FirstName = firstName;
    registeredData.LastName = lastName;
    registeredData.Gender = gender;
    registeredData.BirthDate = birthDate;
    registeredData.Phone = phone;
    registeredData.Email = email;
    registeredData.Password = password;
    registeredData.Degree = degree;
    //registeredData.GPA = parseFloat(gpa);
    registeredData.GPA = gpa;
    registeredData.UniversityId = parseInt(university);
    registeredData.Salary = parseInt(salary);

    console.log(registeredData);
    console.log(gpa);
    
    var myTable = $('#dataTabelEmployee').DataTable();
    $.ajax({
        //url: "https://localhost:44351/API/Employees/Register",
        url: "https://localhost:44388/Employees/Register",
        //contentType: "application/json;charset=utf-8",
        type: "POST",
        //data: JSON.stringify(registeredData)
        data: registeredData
    }).done((result) => {
        console.log(result);
        
        myTable.ajax.reload();
        var swalIcon;
        if (result.status == 200) {
            swalIcon = 'success';
            swalTitle = 'Success';
        } else {
            swalIcon = 'error'
            swalTitle = 'Oops!'
        }
        Swal.fire({
            icon: swalIcon,
            title: swalTitle,
            text: result.message,
        });
    }).fail((error) => {
        console.log(error);
        //console.log(result.message);
        //alert(result.message);
        Swal.fire({
            icon: 'error',
            title: 'Something went wrong',
            text: "Hmmm....",
        });
    });
}

$('#form1').submit(function (e) {
    e.preventDefault();
    if ($("#submitButton").html() == "Insert") {
        submitForm();
        $('#form1').trigger("reset");
        $('#registerNewEmployee').modal('hide');
    } else {
        UpdateData();
        $('#form1').trigger("reset");
        $('#registerNewEmployee').modal('hide');
    }
});

function Detail(nik) {
    $.ajax({
        //url: "https://localhost:44351/API/Employees/Register/" + nik,
        url: "https://localhost:44388/Employees/GetRegisteredById/" + nik,
    }).done((result) => {

        console.log(result);

        var data = result.result[0];

        SetDetailValue(data);

    }).fail((error) => {
        console.log(error);
    });
}

function SetDetailValue(data) {

    let detailButton = "";
    detailButton += `<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>`;
    $("#detailEmployee .modal-footer").html(detailButton);

    let nik = "";
    nik += `<h6 class="ml-auto my-1 d-inline-block"> NIK: </h6> <h6 class="mr-auto my-1 d-inline-block"> ${data.nik} </h6>`;
    $(".detailRegisteredNIK").html(nik);

    let fullName = "";
    fullName += `<h6 class="mx-auto my-1"> Name: ${data.firstName} ${data.lastName} </h6>`;
    $(".detailRegisteredName").html(fullName);

    let email = "";
    email += `<h6 class="mx-auto my-1"> Email: ${data.email} </h6>`;
    $(".detailRegisteredEmail").html(email);

    let phone = "";
    phone += `<h6 class="mx-auto my-1"> Phone: ${data.phone} </h6>`;
    $(".detailRegisteredPhone").html(phone);

    let birthDate = "";
    birthDate += `<h6 class="mx-auto my-1"> Birth date: ${data.birthDate} </h6>`;
    $(".detailRegisteredBirthDate").html(birthDate);

    let getGender = data.gender;
    if (getGender === 0) {
        let gender = "";
        gender += `<h6 class="mx-auto my-1"> Gender: Male </h6>`;
        $(".detailRegisteredGender").html(gender);
    } else {
        let gender = "";
        gender += `<h6 class="mx-auto my-1"> Gender: Female </h6>`;
        $(".detailRegisteredGender").html(gender);
    }

    let universityName = "";
    universityName += `<h6 class="mx-auto my-1"> University: ${data.universityName} </h6>`;
    $(".detailRegisteredUniversityname").html(universityName);

    let degree = "";
    degree += `<h6 class="mx-auto my-1"> Degree: ${data.degree} </h6>`;
    $(".detailRegisteredDegree").html(degree);

    let gpa = "";
    gpa += `<h6 class="mx-auto my-1"> GPA: ${data.gpa} </h6>`;
    $(".detailRegisteredGPA").html(gpa);

    let roleName = "";
    roleName += `<h6 class="mx-auto my-1"> Role: ${data.roleName} </h6>`;
    $(".detailRegisteredRole").html(roleName);

    let salary = "";
    salary += `<h6 class="mx-auto my-1"> Salary: ${data.salary} </h6>`;
    $(".detailRegisteredSalary").html(salary);

}
