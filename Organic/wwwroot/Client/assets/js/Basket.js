
$(document).ready(function () {

    let btns = document.querySelectorAll(".add-basket-btn")

    btns.forEach(x => x.addEventListener("click", function (e) {
        e.preventDefault()
        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.minicart-block').html(data);
            })
    }))

    let btns2 = document.querySelectorAll(".add-basket-kabab")
    btns2.forEach(x => x.addEventListener("click", function (e) {
        e.preventDefault()
        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.minicart-block').html(data);
            })
    }))

    $(document).on("click", ".remove-basket-btn", function (e) {
        e.preventDefault();
        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.cart-block').html(data);
            })
    })

    let btns3 = document.querySelectorAll(".add-basket-meze")
    btns3.forEach(x => x.addEventListener("click", function (e) {
        e.preventDefault()
        console.log(e.target.href)
        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.minicart-block').html(data);
            })
    }))




    let btns4 = document.querySelectorAll(".add-blogComment")
    btns4.forEach(x => x.addEventListener("click", function (e) {
        e.preventDefault()
        console.log(e.target.href)
        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.comment-list').html(data);
            })
    }))







/*    let counts = document.querySelector(".sub-total")*/

    $(document).on("click", ".remove-basket-btn", function (e) {
        e.preventDefault();
        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.minicart-block').html(data);
            })
    })

    $(document).on("click", ".plus-btn", function (e) {
        e.preventDefault();

        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.cartPageJs').html(data);

                fetch(e.target.nextElementSibling.href)
                    .then(response => response.text())
                    .then(data => {
                        $('.minicart-block').html(data);
                    })
            })
    })

    $(document).on("click", ".minus-btn", function (e) {
        e.preventDefault();

        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.cartPageJs').html(data);

                fetch(e.target.nextElementSibling.href)
                    .then(response => response.text())
                    .then(data => {
                        $('.minicart-block').html(data);
                    })
            })
    })

    $(document).on("click", '.select-catagory', function (e) {
        e.preventDefault();
        let aHref = e.target.href;
        let category = e.target.previousElementSibling
        let CategoryId = category.value;


        console.log(CategoryId)

        console.log(aHref)



        $.ajax(
            {
                type: "GET",
                url: aHref,

                data: {
                    CategoryId: CategoryId
                },

                success: function (response) {
                    console.log(response)
                    $('.filtered-area').html(response);

                },
                error: function (err) {
                    $(".modalProduct").html(err.responseText);

                }

            });

    })

    $(document).on("click", '.select-color', function (e) {
        e.preventDefault();
        let aHref = e.target.href;
        let category = e.target.previousElementSibling
        let CategoryId = category.value;


        console.log(CategoryId)

        console.log(aHref)



        $.ajax(
            {
                type: "GET",
                url: aHref,

                data: {
                    CategoryId: CategoryId
                },

                success: function (response) {
                    console.log(response)
                    $('.filtered-area').html(response);

                },
                error: function (err) {
                    $(".modalProduct").html(err.responseText);

                }

            });

    })

    $(document).on("click", '.select-tag', function (e) {
        e.preventDefault();
        let aHref = e.target.href;
        let category = e.target.previousElementSibling
        let CategoryId = category.value;


        console.log(CategoryId)

        console.log(aHref)



        $.ajax(
            {
                type: "GET",
                url: aHref,

                data: {
                    CategoryId: CategoryId
                },

                success: function (response) {
                    console.log(response)
                    $('.filtered-area').html(response);

                },
                error: function (err) {
                    $(".modalProduct").html(err.responseText);

                }

            });

    })


    $(document).on("change", ".searchproductPrice", function (e) {
        e.preventDefault();

        let minPrice = e.target.previousElementSibling.children[0].children[3].innerText.slice(1);
        let MinPrice = parseInt(minPrice);

        let maxPrice = e.target.previousElementSibling.children[0].children[4].innerText.slice(1);
        let MaxPrice = parseInt(maxPrice);
        let aHref = document.querySelector(".shoppage-url").href;

        console.log(MinPrice);
        console.log(MaxPrice);
        console.log(aHref)

        $.ajax(
            {
                url: aHref,

                data: {
                    MinPrice: MinPrice,
                    MaxPrice: MaxPrice

                },

                success: function (response) {
                    $('.filtered-area').html(response);


                },
                error: function (err) {
                    $(".modalProduct").html(err.responseText);

                }

            });


    })
})


function removePTagsFromText(text) {
    var regex = /<p[^>]*>|<\/p>/g;
    return text.replace(regex, '');
}

var text = tinyMCE.get('myTextarea').getContent();
var cleanedText = removePTagsFromText(text);