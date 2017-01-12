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

$(function () {
    $('[data-toggle="popover"]').popover();
});

function IeVersion() {
    //Set defaults
    var value = {
        IsIE: false,
        TrueVersion: 0,
        ActingVersion: 0,
        CompatibilityMode: false
    };

    //Try to find the Trident version number
    var trident = navigator.userAgent.match(/Trident\/(\d+)/);
    if (trident) {
        value.IsIE = true;
        //Convert from the Trident version number to the IE version number
        value.TrueVersion = parseInt(trident[1], 10) + 4;
    }

    //Try to find the MSIE number
    var msie = navigator.userAgent.match(/MSIE (\d+)/);
    if (msie) {
        value.IsIE = true;
        //Find the IE version number from the user agent string
        value.ActingVersion = parseInt(msie[1]);
    } else {
        //Must be IE 11 in "edge" mode
        value.ActingVersion = value.TrueVersion;
    }

    //If we have both a Trident and MSIE version number, see if they're different
    if (value.IsIE && value.TrueVersion > 0 && value.ActingVersion > 0) {
        //In compatibility mode if the trident number doesn't match up with the MSIE number
        value.CompatibilityMode = value.TrueVersion != value.ActingVersion;
    }
    return value;
}


$(document).ready(jqUpdateSize);    // When the page first loads
$(window).resize(jqUpdateSize);     // When the browser changes size
