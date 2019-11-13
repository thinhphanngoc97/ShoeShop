$(document).ready(function () {
    $('#upload_btn').off('click').on('click', function (e) {
        e.preventDefault();

        var thumbnail = $('#thumbnail_img').prop('files')[0];          
        var reader = new FileReader();    
        
        reader.readAsDataURL(thumbnail);
        reader.onload = function(){
            $('#base64_result').val(reader.result);
            $('#thumbnail_preview').attr('src', reader.result);
        }                
    });    
});
function deleteProduct(id) {
    $("#deleteProductModal").modal();

    $("#confirmDeleteProduct").off().on('click', function () {
        $.ajax({
            url: '/Admin/Product/DeleteProduct',
            dataType: 'json',
            type: 'POST',
            data: { id },
            success: function (res) {
                if (res.result) {
                    alert("Xóa thành công!");
                    window.location.href = "/Admin/Product/Product";
                }
                else {
                    alert("Xóa thất bại!");
                    window.location.href = "/Admin/Product/Product";
                }
            }
        });
    });
}
