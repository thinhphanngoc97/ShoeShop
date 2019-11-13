$(document).ready(function () {
	$('#btnUploadInsertAvatar').off('click').on('click', function (e) {
		e.preventDefault();

		var avatar = $('#insertavatar_img').prop('files')[0];
		var reader = new FileReader();

		reader.readAsDataURL(avatar);
		reader.onload = function () {
			$('#insertavatar_base64').val(reader.result);
			$('#insertavatar_preview').attr('src', reader.result);
		}
	});

	$('#btnUploadUpdateAvatar').off('click').on('click', function (e) {
		e.preventDefault();

		var avatar = $('#updateavatar_img').prop('files')[0];
		var reader = new FileReader();

		reader.readAsDataURL(avatar);
		reader.onload = function () {
			$('#updateavatar_base64').val(reader.result);
			$('#updateavatar_preview').attr('src', reader.result);
		}
	});
});

function fnInsertCustomerSuccess(res) {
	if (res.result) {
		alert("Tạo mới thành công!");
		window.location.href = "/Admin/Customer/Customer";
	}
	else {
		$("#insertCustomerForm").html(res);
	}
}

function fnUpdateCustomerSuccess(res) {
	if (res.result) {
		$("#customerDetailForm").html(res.data);
		$("#updateCustomerModal").modal();
	}
	else {
		$("#customerDetailForm").html(res);
	}
}

function deleteCustomer(idCustomer) {
	$("#deleteCustomerModal").modal();

	$("#confirmDeleteCustomer").off().on('click', function () {
		$.ajax({
			url: '/Admin/Customer/DeleteCustomer',
			dataType: 'json',
			type: 'POST',
			data: { idCustomer },
			success: function (res) {
				if (res.result) {
					alert("Xóa thành công!");
				}
				else {
					alert("Xóa thất bại!");
				}
				window.location.href = "/Admin/Customer/Customer";
			}
		});
	});
}