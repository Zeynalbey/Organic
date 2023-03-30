$(document).ready(function () {

    let btns = document.querySelectorAll(".blogelike")

    btns.forEach(x => x.addEventListener("click", function (e) {
        e.preventDefault()
        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.blog-like').html(data);
            })
    }))

})