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
        
        $('#check_payment_1').off('click').on('click', function () {
            $('#ip__checkout-1').prop('checked', true)
            $('#ip__checkout-2').removeProp('checked')
        });
        $('#check_payment_2').off('click').on('click', function () {
            $('#ip__checkout-2').prop('checked', true)
            $('#ip__checkout-1').removeProp('checked')
        });

        const divAccount = document.querySelector('.box__account');
        $('#txtcheck_1').off('click').on('click', function () {
            $('#ip__checkout-1').prop('checked', true)
            $('#ip__checkout-2').removeProp('checked')
                divAccount.style.display = 'none';
                $('.txtlogin').removeAttr('required');   
        });
        $('#txtcheck_2').off('click').on('click', function () {
            $('#ip__checkout-2').prop('checked', true)
            $('#ip__checkout-1').removeProp('checked')
                divAccount.style.display = 'block';
                $('.txtlogin').prop('required', true);
                console.log("change input")
        });
        //$('#ip__checkout-2').on('change', function () {
        //    console.log("change")
        //    $('.txtlogin').prop('required', true);
        //});

    }
}
cart.init();