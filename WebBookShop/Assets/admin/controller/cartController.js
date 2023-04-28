var cart = {
    init: function () {
        cart.registerEvents();
    },
    registerEvents: function () {
        $('.ip__quantity').on('change', function () {
            var id = $(this).data("id");
            var quantity = $('#Quantity_' + id).val();

            $.ajax({
                url: '/cart/update',
                type: 'POST',
                data: { id: id, quantity: quantity },
                success: function (rs) {
                    if (rs.Success) {
                        $.ajax({
                            url: '/cart/index',
                            type: 'GET',
                            success: function (x) {
                                $('body').html(x);
                            }
                        })
                    }
                }
            });
        });

        $('.btn__delete').off('click').on('click', function () {
            
            var id = $(this).data("id");
            
            $.ajax({
                url: '/cart/delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $.ajax({
                            url: '/cart/index',
                            type: 'GET',
                            success: function (x) {
                                $('body').html(x);
                            }
                        })
                    }
                }
            });
        });
    }
}
cart.init();