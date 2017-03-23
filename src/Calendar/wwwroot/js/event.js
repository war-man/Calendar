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
        $('#showday').removeAttr('disabled');
    } else {
        $('#showday').attr('disabled', 'disabled');
    }
}


