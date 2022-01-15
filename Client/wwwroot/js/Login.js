console.log("TES 123");

$('#forgotPasswordForm').submit(function (e) {
    e.preventDefault();
    ForgotPassword()
});

function ForgotPassword() {
    var email = $('#inputEmail').val();
    
    var forgotPassword = Object();
    forgotPassword.email = email;

    $.ajax({
        url: "https://localhost:44388/Login/ForgotPassword",
        type: "PUT",
        data: forgotPassword
    }).done((result) => {
        console.log(result);

        var swalIcon;
        if (result.status == 200) {
            swalIcon = 'success';
            swalTitle = 'Success';
            //if (result.status === 200) {
            //    setTimeout(function () {
            //        location.href = "https://localhost:44388/SbAdmin";
            //    }, 3000);
            //}
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
        Swal.fire({
            icon: 'error',
            title: 'Something went wrong',
            text: "Hmmm....",
        });
    });
}
