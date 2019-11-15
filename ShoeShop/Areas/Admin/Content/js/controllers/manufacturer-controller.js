function deleteManufacturer(id) {
    $("#deleteManufacturerModal").modal();

    $("#confirmDeleteManufacturer").off().on('click', function () {
        $.ajax({
            url: '/Admin/Manufacturer/DeleteManufacturer',
            dataType: 'json',
            type: 'POST',
            data: { id },
            success: function (res) {
                if (res.result) {
                    alert("Xóa thành công!");
                    window.location.href = "/Admin/Manufacturer/Manufacturer";
                }
                else {
                    alert("Xóa thất bại!");
                    window.location.href = "/Admin/Manufacturer/Manufacturer";
                }
            }
        });
    });
}

function fnInsertManufacturerSuccess(res) {
    if (res.result) {
        alert("Tạo mới thành công!");
        window.location.href = "/Admin/Manufacturer/Manufacturer";
    }
    else {
        $("#insertManufacturerForm").html(res);
    }
}
function fnUpdateManufacturerSuccess(res) {
    debugger
    if (res.result) {
        $("#manufacturerDetailForm").html(res.data);
        $("#updateManufacturerModal").modal();
    }
}