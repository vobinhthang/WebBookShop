var prd = {
    init: function () {
        prd.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');

            $.ajax({
                url: "/Admin/Product/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.addClass('badge-success');
                        btn.removeClass('badge-light');
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.addClass('badge-light');
                        btn.removeClass('badge-success');
                        btn.text('Ẩn');
                    }
                }
            });
        });
        $('.btn-active-hot').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');

            $.ajax({
                url: "/Admin/Product/ChangeHot",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.addClass('badge-danger');
                        btn.removeClass('badge-light');
                        btn.text('Hot');
                    }
                    else {
                        btn.addClass('badge-light');
                        btn.removeClass('badge-danger');
                        btn.text('Không');
                    }
                }
            });
        });
       
    }
}
prd.init();