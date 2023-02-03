$(document).ready(function () {
    //$("#test1").click(function () {
    //    $("#test1").text("abcd!");
    //});
    $("#btnreload").click(function () {
        $(".reloadtext").val("");
    });

    $('#alo123').delay(3000).slideUp(500);

    $('.alo123').delay(3000).slideUp(500);

    //$("#tbSearch").keyup(function () {
    //    var roleName = $.trim($("#tbSearch").val());
    //    $.ajax({
    //        type: "POST",
    //        url: "/admin/role",
    //        data: "{keyword:'" + roleName + "'}",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (roles) {
    //            var table = $("#tableSearch");
    //            table.find("tr:not(:first)").remove();
    //            $.each(roles, function (i, role) {
    //                var table = $("#tableSearch");
    //                var row = table[0].insertRow(-1);
    //                $(row).append("<td />");
    //                $(row).find("td").eq(0).html(i+1);
    //                $(row).append("<td />");
    //                $(row).find("td").eq(1).html(role.RoleName);
    //                $(row).append("<td />");
    //                $(row).find("td").eq(2).html(role.CreatedDate);
    //                $(row).append("<td />");
    //                $(row).find("td").eq(3).html(role.UpdateDate);
    //            });
    //        }
    //    })
    //})
});
