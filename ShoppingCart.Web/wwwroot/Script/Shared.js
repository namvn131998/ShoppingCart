var objNavItem = document.getElementsByClassName("btn-nav-item");
for (var i = 0; i < objNavItem.length; i++) {
    objNavItem[i].addEventListener('click', function () {
        var current = document.getElementsByClassName("is-active");
        current[0].className = current[0].className.replace("is-active", "");
        this.className += "is-active";
        alert($(this).children("span.nav-item"));
    })
}