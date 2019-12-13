var dialog = document.querySelector('dialog');

function fnRegisterSuccess(res) {
    if (res.result) {
        $("#registerForm").html(res.data);

        dialog.showModal();
    }
    else {
        $("#registerForm").html(res);
    }
}

function fnLoginSuccess(res) {
    if (res.result == true) {
        window.location.href = "/";
    }
    if (res.result == false) {
        window.location.href = "/thanh-toan";
    }
}

$(document).ready(function () {
    $(".closeDialog").on("click", function () {
        dialog.close();
    })
});
