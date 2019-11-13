function deleteMessage(id) {
    $("#deleteMessageModal").modal();

    $("#confirmDeleteMessage").off().on('click', function () {
        $.ajax({
            url: '/Admin/Message/DeleteMessage',
            dataType: 'json',
            type: 'POST',
            data: { id },
            success: function (res) {
                if (res.result) {
                    alert("Xóa thành công!");
                    window.location.href = "/Admin/Message/Message";
                }
                else {
                    alert("Xóa thất bại!");
                    window.location.href = "/Admin/Message/Message";
                }
            }
        });
    });
}