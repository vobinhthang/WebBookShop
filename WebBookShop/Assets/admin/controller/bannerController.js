﻿var banner = {
    init: function () {
        banner.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');

            $.ajax({
                url: "/Admin/Banner/ChangeStatus",
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
    }
}
banner.init();