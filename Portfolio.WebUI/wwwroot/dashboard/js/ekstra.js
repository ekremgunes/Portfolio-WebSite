const header = document.querySelector(".header-area")
const pageUpBtn = document.querySelector(".page-up-btn")

var lastScroll = 0
window.addEventListener("scroll", () => {

    if (lastScroll > scrollY && scrollY > 400) {
        pageUpBtn.style.visibility = "visible"
        header.style.width = "100%"
        header.style.position = "sticky"
        header.style.top = "0"
    } else {
        pageUpBtn.style.visibility = "hidden"
        header.style.width = ""
        header.style.position = ""
    }

    lastScroll = scrollY

})

pageUpBtn.addEventListener("click", () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
   
})

