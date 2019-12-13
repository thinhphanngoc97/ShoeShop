var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#add_to_cart').off('click').on('click', function (e) {
            e.preventDefault();
            var result = document.getElementById('qty');
            var quantity = result.value;
            var productId = $(result).data('id');

            window.location.href = "/them-gio-hang?productId=" + productId + "&quantity=" + quantity;
        });

        $('.items-count').off('click').on('click', function () {
            var productList = $('.quantity_text');
            var cartList = [];

            $.each(productList, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        Id: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Cart/UpdateCart',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });

        $('.delete_item').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { productId: $(this).data('id') },
                url: '/Cart/DeleteItem',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            });
        });

        $('#delete_discount_code').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/Cart/DeleteDiscountCode";
        });
    }
}
cart.init();

var dialog = document.querySelector('dialog');

function fnCheckoutSuccess(res) {
    if (res.result) {
        //$("#checkoutForm").html(res.data);
        dialog.showModal();
    }
}

$(document).ready(function () {
    $(".closeDialog").on("click", function () {
        dialog.close();
    })
});