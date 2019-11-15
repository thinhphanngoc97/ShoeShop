function fnLoginSuccess(res) {
	if (res.result) {
		window.location.href = "/Admin/Invoice/Invoice";
	}
	else {
		$("#loginForm").html(res)
	}
}
