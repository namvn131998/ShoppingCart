
var objNavItem = document.getElementsByClassName("btn-nav-item");
if (localStorage.getItem("active") == null)
    $(objNavItem[0]).addClass("is-active");
for (var i = 0; i < objNavItem.length; i++) {
    var objText = $(objNavItem[i]).children("span").text();
    if (objText == localStorage.getItem("active")) {
        $(objNavItem[i]).addClass("is-active");
    }

    objNavItem[i].addEventListener('click', function () {
        var current = document.getElementsByClassName("is-active");
        current[0].className = current[0].className.replace("is-active", "");
        this.className += "is-active";
        var objVal = $(this).children("span").text();
        localStorage.setItem("active", objVal)
    })
}