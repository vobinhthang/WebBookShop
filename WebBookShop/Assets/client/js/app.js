

$('.product_sale--slice').owlCarousel({
    loop:true,
    margin:10,
    nav:false,
    autoplay:true,
    autoplaySpeed: 3000,
    autoplayHoverPause: true,
    responsive:{
    0:{
        items:2
    },
    600:{
        items:3
    },
    1000:{
        items:5,
    }
}
})

$('.sliders').owlCarousel({
    loop:true,
    margin:10,
    nav:false,
    dot: true,
    autoplay:true,
    autoplayTimeout: 6000,
    autoplaySpeed: 3000,
    autoplayHoverPause: true,
    responsive:{
            0:{
                items:1
            },
            600:{
                items:1
            },
            1000:{
                items:1
            }
        }
    })

