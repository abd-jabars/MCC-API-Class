$.ajax({
    url: "https://localhost:44351/API/Employees"
}).done((result) => {
    console.log(result);
    var text = "";
    $.each(result, function (key, val) {
        text += `<tr>
                        <td>${key + 1}</td>
                        <td>${val.nik}</td>
                        <td class = "text-capitalize">${val.firstName} ${val.lastName}</td>
                        <td class = "text-capitalize">${val.birthDate}</td>
                        <td class = "text-capitalize">${val.gender}</td>
                        <td class = "text-capitalize">${val.phone}</td>
                        <td>${val.email}</td>
                        <td><button data-toggle="modal" data-target="#getPoke" class="btn btn-primary" onclick="getDetails('${val.url}')"> Details </button></td>
                    </tr>`;
    });
    $(".tabelEmployee").html(text);
}).fail((error) => {
    console.log(error);
});

function getGender(val) {
    if (val == 0) {
        var gender = `<td class = "text-capitalize">Male</td>`;
        return gender;
    } else if (val == 1) {
        var gender= `<td class = "text-capitalize">Female</td>`;
        return gender;
    }
}