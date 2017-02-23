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

$(function () {
    $('[data-toggle="popover"]').popover();
});

$(document).ready(jqUpdateSize);    // When the page first loads
$(window).resize(jqUpdateSize);     // When the browser changes size
