function fnInsertCategorySuccess(res) {
	if (res.result) {
		alert("Tạo mới thành công!");
		window.location.href = "/Admin/Category/Category";
	}
	else {
		alert("Lỗi tạo mới!");
		window.location.href = "/Admin/Category/Category";
	}
}

function fnUpdateCategorySuccess(res) {
	if (res.result) {
		$("#categoryDetailForm").html(res.data);
		$("#updateCategoryModal").modal();
	}
	else {
		alert("Lỗi cập nhật!");
		window.location.href = "/Admin/Category/Category";
	}
}

function deleteCategory(idCategory) {
	$("#deleteCategoryModal").modal();
	
	$("#confirmDeleteCategory").off().on('click', function () {
		$.ajax({
			url: '/Admin/Category/DeleteCategory',
			dataType: 'json',
			type: 'POST',
			data: { idCategory },
			success: function (res) {
				if (res.result) {
					alert("Xóa thành công!");
				}
				else {
					alert("Xóa thất bại!");
				}
				window.location.href = "/Admin/Category/Category";
			}
		});
	});
}
