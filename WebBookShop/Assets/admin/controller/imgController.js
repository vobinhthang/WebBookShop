var img = {
    init: function () {
        img.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('change', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');

            $.ajax({
                url: "/Admin/Product/ChangeThumbnail",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    

                }
            });
        });
    }
}
img.init();