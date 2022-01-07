////$.ajax({
////    url: "https://localhost:44351/API/Employees"
////}).done((result) => {
////    console.log(result);
////    var text = "";
////    $.each(result, function (key, val) {
////        text += `<tr>
////                        <td>${key + 1}</td>
////                        <td>${val.nik}</td>
////                        <td class = "text-capitalize">${val.firstName} ${val.lastName}</td>
////                        <td class = "text-capitalize">${val.birthDate}</td>
////                        <td class = "text-capitalize">${val.gender}</td>
////                        <td class = "text-capitalize">${val.phone}</td>
////                        <td>${val.email}</td>
////                        <td>${val.salary}</td>
////                    </tr>`;
////    });
////    $(".tabelEmployee").html(text);
////}).fail((error) => {
////    console.log(error);
////});

$(document).ready(function () {
    $('#dataTabelEmployee').DataTable({
        'ajax': {
            'url': "https://localhost:44351/API/Employees",
            'dataType': 'json',
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
                'render': function (data, type, row) {
                    return row['firstName'] + ' ' + row['lastName']
                }
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
                'data': null,
                'render': function (data, type, row) {
                    return "Rp " + row['salary']
                }
            }
        ]
    });
});