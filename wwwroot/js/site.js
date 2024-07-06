window.saveScrollPosition = function() {
    sessionStorage.setItem("scroll", window.scrollY.toString());
}

window.restoreScrollPosition = function () {
    const scroll = sessionStorage.getItem("scroll");
    if (scroll != null) {
        window.scrollTo({ top: +scroll });
    }
    
    sessionStorage.setItem("scroll", null);
}
