/* This js include all the functions/javascripts used in the Event CRUD .cshtml */

/* Initialize all tooltip object in Event CRUD forms */
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
})

/* RiskLevelMatrix Defined in the StaticListOfValuesService Service Class */
function UpdateRiskLevel(RiskLevelMatrix) {
        if ($('#Likelihood').val() == "" || $('#Impact').val() == "") {
            $('#RiskLevelDisplay').removeClass('cal-risklevel-' + $('#RiskLevel').val());
            $('#RiskLevelDisplay').val("");
            $('#RiskLevel').val("");
        }
        else {
            $('#RiskLevelDisplay').removeClass('cal-risklevel-' + $('#RiskLevel').val());
            $('#RiskLevel').val(RiskLevelMatrix[$('#Likelihood').val() - 1][$('#Impact').val() - 1]);
            $('#RiskLevelDisplay').text($('#RiskLevel option:selected').text());
            $('#RiskLevelDisplay').addClass('cal-risklevel-' + $('#RiskLevel').val());
        }
}

/* This Wrapper function is necessary */
function UpdateRiskLevelWrapper() {
    UpdateRiskLevel(riskmatrix);
}


function doIA(iaurl, mode) {
    var url = "";
    var param = "";

    if (mode == "hosts") {
        param = $('#AffectedHosts').val();
        url = iaurl + "?broken=true&server=" + encodeURIComponent(param);
    } else {
        param = $('#AffectedProjects').val();
        url = iaurl + "?system=" + encodeURIComponent(param);
    }

    window.open(url, '_blank');
};

function UpdateSearchDateFilter() {
    if ($('#searchdatefrom').val() == "" && $('#searchdateto').val() == "") {
        $('#searchrange').removeAttr('disabled');
    } else {
        $('#searchrange').attr('disabled', 'disabled');
        $("#searchrange").val("");
    }
}


function ClearEventSearchForm() {
    $("[id|=fp]").val("");
    $("#searchday").val("ND");
    $('#searchdatefrom').val("");
    $('#searchdateto').val("");
    $("#searchrange").val("");
    $('#searchrange').removeAttr('disabled');
}

// Numeric only control handler
jQuery.fn.ForceNumericOnly =
function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.charCode || e.keyCode || 0;

            if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
                // numbers   
                key >= 48 && key <= 57 ||
                // Numeric keypad
                key >= 96 && key <= 105 ||
                // comma, period and minus, . on keypad
               key == 190 || key == 188 || key == 109 || key == 110 ||
                // Backspace and Tab and Enter
               key == 8 || key == 9 || key == 13 ||
                // Home and End
               key == 35 || key == 36 ||
                // left and right arrows
               key == 37 || key == 39 ||
                // Del and Ins
               key == 46 || key == 45)
                return true;

            return false;
        });
    });
};
