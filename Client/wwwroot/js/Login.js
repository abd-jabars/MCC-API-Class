console.log("TES 123");

//function Login() {
//    var email = $('#inputEmail').val();
//    var password = $('#inputPassword').val();
    
//    var login = Object();
//    login.email = email;
//    login.password = password;

//    $.ajax({
//        url: "https://localhost:44388/Login/Auth",
//        type: "POST",
//        data: login
//    }).done((result) => {
//        console.log(result);

//        var swalIcon;
//        if (result.status == 200) {
//            swalIcon = 'success';
//            swalTitle = 'Success';
//            if (result.status === 200) {
//                setTimeout(function () {
//                    location.href = "https://localhost:44388/SbAdmin";
//                }, 3000);
//            }
//        } else {
//            swalIcon = 'error'
//            swalTitle = 'Oops!'
//        }
//        Swal.fire({
//            icon: swalIcon,
//            title: swalTitle,
//            text: result.message,
//        });
//    }).fail((error) => {
//        console.log(error);
//        Swal.fire({
//            icon: 'error',
//            title: 'Something went wrong',
//            text: "Hmmm....",
//        });
//    });
//}

//$('#loginForm').submit(function (e) {
//    e.preventDefault();
//    Login();
//});