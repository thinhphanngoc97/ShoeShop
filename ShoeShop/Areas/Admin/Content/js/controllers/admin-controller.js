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

function fnInsertAdminSuccess(res) {
	if (res.result) {
		alert("Tạo mới thành công!");
		window.location.href = "/Admin/Admin/Admin";
	}
	else {
		$("#insertAdminForm").html(res);
	}
}

function fnUpdateAdminSuccess(res) {
	if (res.result) {
		$("#adminDetailForm").html(res.data);
		$("#updateAdminModal").modal();
	}
	else {
		$("#adminDetailForm").html(res);
	}
}

function deleteAdmin(idAdmin) {
	$("#deleteAdminModal").modal();

	$("#confirmDeleteAdmin").off().on('click', function () {
		$.ajax({
			url: '/Admin/Admin/DeleteAdmin',
			dataType: 'json',
			type: 'POST',
			data: { idAdmin },
			success: function (res) {
				if (res.result == 1) {
					alert("Xóa thành công!");
				}
				else if (res.result == 0){
					alert("Xóa thất bại, tài khoản đang được sử dụng không đươc xóa!");
				}
				else {
					alert("Xóa thất bại!");
				}
				window.location.href = "/Admin/Admin/Admin";
			}
		});
	});
}