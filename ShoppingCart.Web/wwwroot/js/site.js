﻿$(document).ready(function () {
    $("#sidebar").click(function () {
        if ($(".sidebar-section").hasClass("width-20")) {
            $(".sidebar-section").removeClass("width-20");
            $(".sidebar-section").addClass("width-9");
            $("#sidebar").removeClass("fa-angle-left");
            $("#sidebar").addClass("fa-angle-right");
            $(".page-content").addClass("mgl-10");
        } else {
            $(".sidebar-section").removeClass("width-9");
            $(".sidebar-section").addClass("width-20");
            $("#sidebar").removeClass("fa-angle-right");
            $("#sidebar").addClass("fa-angle-left");
            $(".page-content").removeClass("mgl-10");
        }
    })
})
function Changepassword() {
    var url = $("#btn-changepassword").attr("data-ajax-url");
    var data = $("#changepassword").serialize();
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        success: function (data) {
            if (data.result == "OK")
                alert("OK");
        }
    })
}