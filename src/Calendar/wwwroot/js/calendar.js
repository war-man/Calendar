// Write your Javascript code.


// jQuery
function jqUpdateSize(){
    // Get the dimensions of the viewport
    var width = $(window).width();
    var height = $(window).height();
    var top = $('#mvEventContainer').offset().top;
    /*
    $('#jqWidth').html(width);
    $('#jqHeight').html(height);
    $('#jqTop').html(top);
    */
    $('#mvEventContainer').css('height', height - top);

};

$(function () {
    $('[data-toggle="tooltip"]').tooltip();
})

/* the following function makes tooltip works inside popup */
$(function(){
    $('body').tooltip( {selector: '[data-toggle=tooltip]'} );
});
/*
$(function () {
    $('[data-toggle="popover"]').popover();
});
*/
/* keep event popover alive while the popover is being hovered */
$(function () {
    $('[data-toggle="popover"]').popover({ trigger: "manual", html: true, animation: false })
        .on("mouseenter", function () {
            var _this = this;
            $(this).popover("show");
            $(".popover").on("mouseleave", function () {
                $(_this).popover('hide');
            });
        })
        .on("mouseleave", function () {
            var _this = this;
            setTimeout(function () {
                if (!$(".popover:hover").length) {
                    $(_this).popover("hide");
                }
            }, 100);
        });
});
$(document).ready(jqUpdateSize);    // When the page first loads
$(window).resize(jqUpdateSize);     // When the browser changes size
