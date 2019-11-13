function deleteDiscount(id) {
    $("#deleteDiscountModal").modal();

    $("#confirmDeleteDiscount").off().on('click', function () {
        $.ajax({
            url: '/Admin/Discount/DeleteDiscount',
            dataType: 'json',
            type: 'POST',
            data: { id },
            success: function (res) {
                if (res.result) {
                    alert("Xóa thành công!");
                    window.location.href = "/Admin/Discount/Discount";
                }
                else {
                    alert("Xóa thất bại!");
                    window.location.href = "/Admin/Discount/Discount";
                }
            }
        });
    });
}
function fnInsertDiscountSuccess(res) {
    if (res.result) {
        alert("Tạo mới thành công!");
        window.location.href = "/Admin/Discount/Discount";
    }
    else {
        $("#insertDiscountForm").html(res);
    }
}
function fnUpdateDiscountSuccess(res) {
    if (res.result) {
        $("#discountDetailForm").html(res.data);
        $("#updateDiscountModal").modal();
    }
}