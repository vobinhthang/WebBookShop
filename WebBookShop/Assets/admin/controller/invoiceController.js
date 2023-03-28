var invoice = {
    init: function () {
        invoice.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');

            $.ajax({
                url: "/Admin/invoice/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.addClass('badge-success');
                        btn.removeClass('badge-light');
                        btn.text('Đã nhập');
                    }
                }
            });
        });
    }
}
invoice.init();