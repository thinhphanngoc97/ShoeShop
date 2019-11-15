function fnUpdateInvoiceSuccess(res) {
	if (res.result) {
		$("#invoiceDetailForm").html(res.data);
		$("#updateInvoiceModal").modal();
	}
	else {
		alert("Lỗi cập nhật!");
		window.location.href = "/Admin/Invoice/Invoice";
	}
}


var invoice = {
	init: function () {
		invoice.loadInvoice(1, 3)
		invoice.filterPropertyEvent();
		invoice.filterTimeEvent();
		invoice.changeStatus();
	},
	filterPropertyEvent: function () {
		$('#propertySelect').on('change', function () {
			var idProperty = $(this).val();
			var idTime = $('#timeSelect').val();
			
			invoice.loadInvoice(idProperty, idTime);
		})		
	},
	filterTimeEvent: function () {
		$('#timeSelect').on('change', function () {
			var idTime = $(this).val();
			var idProperty = $('#propertySelect').val();

			invoice.loadInvoice(idProperty, idTime);
		})
	},
	loadInvoice: function (idProperty, idTime) {
		$.ajax({
			url: '/Invoice/LoadListInvoice',
			type: 'POST',
			data: { idProperty, idTime },
			success: function (res) {				
				if (res.result) {
					$('#bodyListInvoice').html(res.data);
				}				
			}
		})
	},
	changeStatus: function () {
		$(document).on('change', '.checkStatus', function (event) {
			var checkId = event.target.id;
			var arr = checkId.split('_');
			var id = arr[1];

			$.ajax({
				url: '/Invoice/UpdateInvoiceStatus',
				type: 'POST',
				data: { id },
				success: function (res) {
					alert("Mã đơn hàng: " + res.Id + " đã được cập nhật trạng thái");
				}
			})
		})
	}
}
invoice.init();
