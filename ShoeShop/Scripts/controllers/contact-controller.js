var dialog = document.querySelector('dialog');

function fnContactSuccess(res) {
    if (res.result) {
        $("#contactForm").html(res.data);
        dialog.showModal();
    }
}

$(document).ready(function () {
    $(".closeDialog").on("click", function () {
        dialog.close();
    })
});
