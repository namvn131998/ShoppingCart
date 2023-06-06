
var objNavItem = document.getElementsByClassName("btn-nav-item");
for (var i = 0; i < objNavItem.length; i++) {
    var objText = $(objNavItem[i]).children("span").text();
    var url = window.location.href;
    if (url.indexOf(objText) > -1) {
        $(objNavItem[i]).addClass("is-active");
    }

    objNavItem[i].addEventListener('click', function () {
        var current = document.getElementsByClassName("is-active");
        current[0].className = current[0].className.replace("is-active", "");
        this.className += "is-active";
    })
}