$(document).ready(function () {
    
    if (!window.location.href.includes("list")) {
        //var now = new Date();
        //var day = ("0" + now.getDate()).slice(-2);
        //var month = ("0" + (now.getMonth() + 1)).slice(-2);
        //var today = now.getFullYear() + "-" + (month) + "-" + (day);
        //$('#txtVisitDate').val(today);
        //$('#txtVisitDate').hide();
        GetDate('txtVisitDate');
        CustomDate('txtVisitDate');

        if ($('#txtLoginId').val() != null && $('#txtLoginId').val() != "")
            console.log("Session value: ", $('#txtLoginId').val());
        if ($('#txtResidentAutoId').val() != "0") {
            GetModal($('#txtResidentAutoId').val());
        }

        AllowOnlyFour('txtRightSpherical');
        GetDecimalInput('txtRightSpherical');
        AllowNumberOnly('txtRightSpherical')

        AllowOnlyFour('txtRightCyclinderical');
        GetDecimalInput('txtRightCyclinderical');
        AllowNumberOnly('txtRightCyclinderical');

        AllowOnlyFour('txtRightNearAdd');
        GetDecimalInput('txtRightNearAdd');
        AllowNumberOnly('txtRightNearAdd')

        GetDecimalInput('txtLeftSpherical');
        AllowOnlyFour('txtLeftSpherical');
        AllowNumberOnly('txtLeftSpherical')

        GetDecimalInput('txtLeftCyclinderical');
        AllowOnlyFour('txtLeftCyclinderical');
        AllowNumberOnly('txtLeftCyclinderical')

        AllowOnlyFour('txtLeftNearAdd');
        GetDecimalInput('txtLeftNearAdd');
        AllowNumberOnly('txtLeftNearAdd')

        $('#chkNew').prop('checked', true);
        $('#chkNew').change();
        $('#autocomplete-input-Locality').change();
        $('#autocomplete-input-Locality').focus();

        OnCheckBoxChecked('Right-Un-aided');
        OnCheckBoxChecked('Right-With-Glasses');
        OnCheckBoxChecked('Right-Pin-Hole');

        OnCheckBoxChecked('Left-Un-aided');
        OnCheckBoxChecked('Left-With-Glasses');
        OnCheckBoxChecked('Left-Pin-Hole');

        OnCheckBoxChecked('Right-N');
        OnCheckBoxChecked('Left-N');

        OnCheckBoxChecked('Distance-Vision');
        OnCheckBoxChecked('Near-Vision');

        OnCheckBoxChecked('RightOpthalmoscope-Red-reflex-test');
        OnCheckBoxChecked('LeftOpthalmoscope-Red-reflex-test');
        OnCheckBoxChecked('PupillaryReactions');

        OnCheckBoxChecked('RightCoverUncoverTest');
        OnCheckBoxChecked('LeftCoverUncoverTest');

        OnCheckBoxChecked('chkRightDrop');
        OnCheckBoxChecked('chkLeftDrop');

        OnCheckBoxChecked('chkTreatment');
        
        ReadOnlyCheckBoxes();
        
        $('#chkLeftNo').prop("checked", true);
        $('#chkRightNo').prop("checked", true);
        $('[name="chkRightDrop"]').change();
        $('[name="chkLeftDrop"]').change();

        SetWearGlasses(false);
        FillDropDown();
        load_Tabs()
        HideVisualFieldTest();

        $('#chkLocalities').prop('checked', true);

    }
})
function ReadOnlyCheckBoxes() { 

    DisabledCheckedUncheckedChecbox('chkWearGlasses');
    DisabledCheckedUncheckedChecbox('chkDistance');
    DisabledCheckedUncheckedChecbox('chkNear');

    //Page 3
    DisabledCheckedUncheckedChecbox('chkRightUn-aided');
    DisabledCheckedUncheckedChecbox('chkLeftUn-aided');

    DisabledCheckedUncheckedChecbox('chkRightGlasses');
    DisabledCheckedUncheckedChecbox('chkLeftGlasses');

    DisabledCheckedUncheckedChecbox('chkRightPinHole');
    DisabledCheckedUncheckedChecbox('chkLeftPinHole');

    DisabledCheckedUncheckedChecbox('chkRightAcuityDistance');
    DisabledCheckedUncheckedChecbox('chkLeftAcuityDistance');

    DisabledCheckedUncheckedChecbox('chkRightNearAdd');
    DisabledCheckedUncheckedChecbox('chkLefttNearAdd');

    DisabledCheckedUncheckedChecbox('chkRightGlasses');
    DisabledCheckedUncheckedChecbox('chkLeftGlasses');

    //Page 4
    /*DisabledCheckedUncheckedChecbox('chkNormal');*/
    DisabledCheckedUncheckedChecbox('chkfollowWearing');
    DisabledCheckedUncheckedChecbox('chkfollowUpgrade');
    DisabledCheckedUncheckedChecbox('chkfollowNew');


    DisabledCheckedUncheckedChecbox('chkfollowRightRefractiveError');
    DisabledCheckedUncheckedChecbox('chkfollowLeftRefractiveError');

    //DisabledCheckedUncheckedChecbox('chkfollowRightCycloplagic');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftCycloplagic');


    //DisabledCheckedUncheckedChecbox('chkfollowRightConjunctivitis');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftConjunctivitis');

    //DisabledCheckedUncheckedChecbox('chkfollowRightScleritis');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftScleritis'); 


    //DisabledCheckedUncheckedChecbox('chkfollowRightSquintSurgery');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftSquintSurgery');

    //DisabledCheckedUncheckedChecbox('chkfollowRightAmblyopia');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftAmblyopia');

    //DisabledCheckedUncheckedChecbox('chkfollowRightNystagmus');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftNystagmus');

    //DisabledCheckedUncheckedChecbox('chkfollowRightCorneal');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftCorneal');

    //DisabledCheckedUncheckedChecbox('chkfollowRightCataract');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftCataract');

    //DisabledCheckedUncheckedChecbox('chkfollowRightKeratoconus');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftKeratoconus');

    //DisabledCheckedUncheckedChecbox('chkfollowRightPtosis');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftPtosis');

    //DisabledCheckedUncheckedChecbox('chkfollowRightLow');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftLow');

    //DisabledCheckedUncheckedChecbox('chkfollowRightPupil');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftPupil');

    //DisabledCheckedUncheckedChecbox('chkfollowRightPterygium');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftPterygium');

    //DisabledCheckedUncheckedChecbox('chkfollowRightColorBlindness');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftColorBlindness');

    //DisabledCheckedUncheckedChecbox('chkfollowRightOthersDisorder');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftOthersDisorder');



    // Column 2

    DisabledCheckedUncheckedChecbox('chkfollowRightFurtherAssessment');
    DisabledCheckedUncheckedChecbox('chkfollowLeftFurtherAssessment');

    //DisabledCheckedUncheckedChecbox('chkfollowRightFundoscopy');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftFundoscopy');

    DisabledCheckedUncheckedChecbox('chkfollowRightSquint');
    DisabledCheckedUncheckedChecbox('chkfollowLeftSquint');

    DisabledCheckedUncheckedChecbox('chkfollowRightSquintAssessment');
    DisabledCheckedUncheckedChecbox('chkfollowLeftSquintAssessment');
    

    //DisabledCheckedUncheckedChecbox('chkfollowRightSurgery');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftSurgery');

    //DisabledCheckedUncheckedChecbox('chkfollowRightCataract');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftCataract');

    //DisabledCheckedUncheckedChecbox('chkfollowRightSquintSurgery');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftSquintSurgery');

    //DisabledCheckedUncheckedChecbox('chkfollowRightPterygium');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftPterygium');

    //DisabledCheckedUncheckedChecbox('chkfollowRightCornealDefectSurgery');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftCornealDefectSurgery');

    //DisabledCheckedUncheckedChecbox('chkfollowRightPtosisSurgery');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftPtosisSurgery');

    //DisabledCheckedUncheckedChecbox('chkfollowRightKeratoconusSurgery');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftKeratoconusSurgery');

    //DisabledCheckedUncheckedChecbox('chkfollowRightChalazion');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftChalazion');

    //DisabledCheckedUncheckedChecbox('chkfollowRightHordeolum');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftHordeolum');


    //DisabledCheckedUncheckedChecbox('chkfollowRightOthersSurgery');
    //DisabledCheckedUncheckedChecbox('chkfollowLeftOthersSurgery');
}



$('#NearSquint').change(function () {
    if ($(this).is(':checked')) {
        $('input[name="chkSquint"]').prop('disabled', false);
    }
    else {
        $('input[name="chkSquint"]').prop('disabled', true);
        $('#chkSquintRight').prop('checked', false);
        $('#chkSquintLeft').prop('checked', false);
    }
})

$('#NearAmblyopic').change(function () {
    if ($(this).is(':checked')) {
        
        $('input[name="chkAmblyopic"]').prop('disabled', false);
    }
    else {
        $('input[name="chkAmblyopic"]').prop('disabled', true);
        $('#chkAmblyopicRight').prop('checked', false);
        $('#chkAmblyopicLeft').prop('checked', false);
    }
})

$('#chkNew').on('change', function () {
    if ($('#chkNew').is(':checked')) {
        $('#chkDisplay').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        $('#btnDelete').prop('disabled', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtOptometristResidentId').val('')
        $('#txtAutoRefResidentId').val('')
        $('#txtformMode').val('New');
        $('#btnSave').text('Save');
        Clear();
        if ($('#txtLocalityCode').val() != "" && $('#autocomplete-input-Locality').val() != "")
            GetAutoCompleteResident();

    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtAutoRefResidentId').val('')
    }
    GetAutoCompleteComapny();
    GetDates();
});

$('#chkDisplay').on('change', function () {
    if ($('#chkDisplay').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        $('#btnDelete').prop('disabled', false);
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
        GetAutoCompleteResident()
        $('#btnSave').hide();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtAutoRefResidentId').val('')
        $('#txtOptometristResidentId').val('');
    }
    GetAutoCompleteComapny();
    GetDates();

});


$('#chkEdit').on('change', function () {
    if ($('#chkEdit').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkDisplay').prop('checked', false);
        $('#btnDelete').prop('disabled', false);
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
        $('.checkbox:not(#chkDisplay):not(#chkEdit):not(#chkWearGlasses)').prop('checked', false);
        $('#btnSave').text('Update');
        GetAutoCompleteResident()
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#txtAutoRefResidentId').val('')
    }
    GetAutoCompleteComapny();
    GetDates();
});

$('#txtVisitDate').change(function () {
    DateChange('txtVisitDate');
    GetAutoCompleteResident();
})
 


function FillDropDown() {
 
        $.ajax({
            url: '/DropDownLookUp/Help/GetLookUp/VisualAcuity',
            type: 'GET',
            dataType: 'Json',
            async: false,
            success: function (data) {
                //console.log(data); 
                $('#ddlLeftVisualAcuity').empty();
                $('#ddlLeftVisualAcuity').append('<option  value="-1" selected>Select</option>');

                $('#ddlRightVisualAcuity').empty();
                $('#ddlRightVisualAcuity').append('<option  value="-1" selected>Select</option>');
                $.each(data, function (index, value) {
                    $('#ddlLeftVisualAcuity').append('<option value="' + value.id + '">' + value.text + '</option>');
                    $('#ddlRightVisualAcuity').append('<option value="' + value.id + '">' + value.text + '</option>');
                });
            }
        });
   
}


function ResetLocalityValue() {
    document.getElementById("autocomplete-input-Locality").value = '';
    document.getElementById("txtLocalityCode").value = '';
    document.getElementById("txtLocalityId").value = '';
}
function GetModal(Id) {
    $.ajax({
        url: '/Localities/AutoRefTestResident/GetAutoRefByResidentId/' + Id,
        method: 'get',
        dataType: 'json',
        success: function (result) {
            console.log(result);
            SetValues(result);
        }
    })
}
 
function GetDates() {
    if (($('#chkDisplay').is(':checked')) || $('#chkEdit').is(':checked')) {
        $.ajax({
            url: '/Localities/OptometristResident/GetDatesofResident/' + $('#txtResidentAutoId').val(),
            method: 'get',
            type: 'json',
            success: function (result) {
                console.log(result);
                $('#ddlGetOptometristResidentById').empty();
                $("#ddlGetOptometristResidentById").append($("<option></option>").val('').html(' Date'));
                $('#txtOptometristResidentId').val('');
                $.each(result, function (data, value) {
                    $("#ddlGetOptometristResidentById").append($("<option></option>").val(value.code).html(value.text));
                })
                $('#divDisplay').show();
                $('#ddlGetOptometristResidentById').show()
                $('#ddlGetOptometristResidentById').focus();
            }
        })
        setTimeout(function () {
            var total_tems = $('#ddlGetOptometristResidentById').find('option').length;
            if (total_tems == 2) {
                $('#ddlGetOptometristResidentById option:nth-child(2)').attr('selected', true);
                $('#ddlGetOptometristResidentById').change()
            }

        }, 500)
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
    }
    else {
        $('#divDisplay').hide();
        $('#ddlGetOptometristResidentById').hide()
    }
    if ($('#chkDisplay').is(':checked')) {
        $('#btnSave').hide();
    }
    else
        $('#btnSave').show();

    if ($('#chkDisplay').is(':checked') || $('#chkEdit').is(':checked'))
        if ($('#autocomplete-input-Locality').val() == "" || $('#autocomplete-input-Resident').val() == "") {
            $('#divDisplay').hide();
            $('#ddlGetOptometristResidentById').hide();
        }
}


$('#ddlGetOptometristResidentById').change(function () {
    $('#txtOptometristResidentId').val($('#ddlGetOptometristResidentById').val());
    if ($('#txtOptometristResidentId').val() > 0) {
        $.ajax({
            url: '/Localities/OptometristResident/GetOptometristResidentById/' + $('#txtOptometristResidentId').val(),
            method: 'get',
            dataType: 'json',
            success: function (result) {
                //ResetFields();
                console.log("final ", result);
                SetModel(result);
                setTimeout(function () {
                    GetNewUpgradeCheckbox();
                    GetCheckDiagnosticPolicy();
                },
                    500);
                //validateNextAcuity();
                //SaveValidatFromAcuity();
                //ValidateForVisualAcuityNear();
                //btnNextFromAcuity();
                //NextFromAcuity()
            }
        })

    }
})
function Clear() {
    $('input[type="checkbox"]:not(#chkDisplay):not(#chkEdit):not(#chkWearGlasses)').prop('checked', false);
    $('#txtRightOrthopticExtraOccularMuscle').val('');
    $('#txtLeftOrthopticExtraOccularMuscle').val('');
    $('#txtRightVisitExtraOccularMuscle').val('');
    $('#txtLeftVisitExtraOccularMuscle').val('');
    $('#txtRightSpherical').val('');
    $('#txtRightCyclinderical').val('');
    $('#txtRightAxix').val('');
    $('#txtRightNearAdd').val('');
    $('#ddlRightSpherical').val('P');
    $('#ddlRightCyclinderical').val('P');
    $('#ddlRightVisualAcuity').val('-1');
    $('#txtLeftSpherical').val('');
    $('#txtLeftCyclinderical').val('');
    $('#txtLeftAxix').val('');
    $('#txtLeftNearAdd').val('');
    $('#ddlLeftSpherical').val('P');
    $('#ddlLeftCyclinderical').val('P');
    $('#ddlLeftVisualAcuity').val('-1');
}
function SetModel(SetValues) {
    console.log("Final Model Get ", SetValues);
    Clear();
    $('#txtOptometristResidentId').val(SetValues.optometristResidentId);
    $('#txtAutoRefResidentId').val(SetValues.autoRefResidentId);

    $('input[type="checkbox"][name=Right-Un-aided][value="' + SetValues.distanceVision_RightEye_Unaided + '"]').prop('checked', true);

    //Check for wearing glasses
    if ($('#chkWearGlasses').prop("checked") == true) {
        $('input[type="checkbox"][name=Right-With-Glasses][value="' + SetValues.distanceVision_RightEye_WithGlasses + '"]').prop('checked', true);
        $('input[type="checkbox"][name=Left-With-Glasses][value="' + SetValues.distanceVision_LeftEye_WithGlasses + '"]').prop('checked', true);
    }
    else {
        $('input[type="checkbox"][name=Right-With-Glasses][value="' + SetValues.distanceVision_RightEye_WithGlasses + '"]').prop('checked', false);
        $('input[type="checkbox"][name=Left-With-Glasses][value="' + SetValues.distanceVision_LeftEye_WithGlasses + '"]').prop('checked', false);
    }
    $('input[type="checkbox"][name=Right-Pin-Hole][value="' + SetValues.distanceVision_RightEye_PinHole + '"]').prop('checked', true);

    $('input[type="checkbox"][name=Left-Un-aided][value="' + SetValues.distanceVision_LeftEye_Unaided + '"]').prop('checked', true);
    $('input[type="checkbox"][name=Left-Pin-Hole][value="' + SetValues.distanceVision_LeftEye_PinHole + '"]').prop('checked', true);

    $('input[type="checkbox"][name=Right-N][value="' + SetValues.nearVision_RightEye + '"]').prop('checked', true);
    $('input[type="checkbox"][name=Left-N][value="' + SetValues.nearVision_LeftEye + '"]').prop('checked', true);

    if (SetValues.rightSquint_VA == true || SetValues.leftSquint_VA == true) {
        
        $('input[type="checkbox"][name=NearSquint]').prop('checked', true);
        $('input[type="checkbox"][id=chkSquintRight]').prop('checked', SetValues.rightSquint_VA);
        $('input[type="checkbox"][id=chkSquintLeft]').prop('checked', SetValues.leftSquint_VA);
        $('#NearSquint').change();
    }

    if (SetValues.leftAmblyopic_VA == true || SetValues.rightAmblyopic_VA == true) {
        $('input[type="checkbox"][name=NearAmblyopic]').prop('checked', true);
        $('input[type="checkbox"][id=chkAmblyopicRight]').prop('checked', SetValues.rightAmblyopic_VA);
        $('input[type="checkbox"][id=chkAmblyopicLeft]').prop('checked', SetValues.leftAmblyopic_VA);
        $('#NearAmblyopic').change(); 
    }

    $('#ddlRightSpherical').val(SetValues.right_Spherical_Status);
    $('#txtRightSpherical').val(SetValues.right_Spherical_Points);
    $('#ddlRightCyclinderical').val(SetValues.right_Cyclinderical_Status);
    $('#txtRightCyclinderical').val(SetValues.right_Cyclinderical_Points);

    if (SetValues.right_Cyclinderical_Points > 0) {
        $('#txtRightAxix').prop('readOnly', false)
        $('#txtRightAxix').val(SetValues.right_Axix_From);
    }
    $('#ddlRightVisualAcuity').val(SetValues.visualAcuity_RightEye);
    $('#ddlRightNearAdd').val(SetValues.right_Near_Status);
    $('#txtRightNearAdd').val(SetValues.right_Near_Points);
    $('#ddlLeftSpherical').val(SetValues.left_Spherical_Status);
    $('#txtLeftSpherical').val(SetValues.left_Spherical_Points);
    $('#ddlLeftCyclinderical').val(SetValues.left_Cyclinderical_Status);
    $('#txtLeftCyclinderical').val(SetValues.left_Cyclinderical_Points);
    

    if (SetValues.left_Cyclinderical_Points > 0) {
        $('#txtLeftAxix').prop('readOnly', false)
        $('#txtLeftAxix').val(SetValues.left_Axix_From);
    }
    $('#ddlLeftVisualAcuity').val(SetValues.visualAcuity_LeftEye);
    $('#ddlLeftNearAdd').val(SetValues.left_Near_Status);
    $('#txtLeftNearAdd').val(SetValues.left_Near_Points);

    $('input[type="checkbox"][name=Distance-Vision][value="' + SetValues.hirchberg_Distance + '"]').prop('checked', true);
    $('input[type="checkbox"][name=Near-Vision][value="' + SetValues.hirchberg_Near + '"]').prop('checked', true);

    $('input[type="checkbox"][name=RightOpthalmoscope-Red-reflex-test][value="' + SetValues.ophthalmoscope_RightEye + '"]').prop('checked', true);
    $('input[type="checkbox"][name=LeftOpthalmoscope-Red-reflex-test][value="' + SetValues.ophthalmoscope_LeftEye + '"]').prop('checked', true);

    $('input[type="checkbox"][name=PupillaryReactions][value="' + SetValues.pupillaryReactions_RightEye + '"]').prop('checked', true);

    $('input[type="checkbox"][name=RightCoverUncoverTest][value="' + SetValues.coverUncovertTest_RightEye + '"]').prop('checked', true);
    $('input[type="checkbox"][name=LeftCoverUncoverTest][value="' + SetValues.coverUncovertTest_LeftEye + '"]').prop('checked', true);

    $('#txtRightOrthopticExtraOccularMuscle').val(SetValues.coverUncovertTestRemarks_RightEye)
    $('#txtLeftOrthopticExtraOccularMuscle').val(SetValues.coverUncovertTestRemarks_LeftEye)

    $('#txtRightVisitExtraOccularMuscle').val(SetValues.extraOccularMuscleRemarks_RightEye)
    $('#txtLeftVisitExtraOccularMuscle').val(SetValues.extraOccularMuscleRemarks_LeftEye)

    if (SetValues.cycloplegicRefraction_RightEye == true)
        $('#chkfollowRightCycloplagic').prop('checked', true)
    else
        $('#chkfollowRightCycloplagic').prop('checked', false)

    if (SetValues.cycloplegicRefraction_LeftEye == true)
        $('#chkfollowLeftCycloplagic').prop('checked', true)
    else
        $('#chkfollowLeftCycloplagic').prop('checked', false)

    if (SetValues.conjunctivitis_RightEye == true)
        $('#chkfollowRightCycloplagic').prop('checked', true)
    else
        $('#chkfollowRightCycloplagic').prop('checked', false)

    if (SetValues.conjunctivitis_LeftEye == true)
        $('#chkfollowLeftConjunctivitis').prop('checked', true)
    else
        $('#chkfollowLeftConjunctivitis').prop('checked', false)

    if (SetValues.scleritis_RightEye == true)
        $('#chkfollowRightScleritis').prop('checked', true)
    else
        $('#chkfollowRightScleritis').prop('checked', false)

    if (SetValues.scleritis_LeftEye == true)
        $('#chkfollowLeftScleritis').prop('checked', true)
    else
        $('#chkfollowLeftScleritis').prop('checked', false)


    if (SetValues.nystagmus_RightEye == true)
        $('#chkfollowRightNystagmus').prop('checked', true)
    else
        $('#chkfollowRightNystagmus').prop('checked', false)

    if (SetValues.nystagmus_LeftEye == true)
        $('#chkfollowLeftNystagmus').prop('checked', true)
    else
        $('#chkfollowLeftNystagmus').prop('checked', false)

    if (SetValues.cornealDefect_RightEye == true)
        $('#chkfollowRightCorneal').prop('checked', true)
    else
        $('#chkfollowRightCorneal').prop('checked', false)

    if (SetValues.cornealDefect_LeftEye == true)
        $('#chkfollowLeftCorneal').prop('checked', true)
    else
        $('#chkfollowLeftCorneal').prop('checked', false)

    if (SetValues.cataract_RightEye == true)
        $('#chkfollowRightCataract').prop('checked', true)
    else
        $('#chkfollowRightCataract').prop('checked', false)

    if (SetValues.cataract_LeftEye == true)
        $('#chkfollowLeftCataract').prop('checked', true)
    else
        $('#chkfollowLeftCataract').prop('checked', false)

    if (SetValues.keratoconus_RightEye == true)
        $('#chkfollowRightKeratoconus').prop('checked', true)
    else
        $('#chkfollowRightKeratoconus').prop('checked', false)

    if (SetValues.keratoconus_LeftEye == true)
        $('#chkfollowRightKeratoconus').prop('checked', true)
    else
        $('#chkfollowRightKeratoconus').prop('checked', false)

    if (SetValues.ptosis_RightEye == true)
        $('#chkfollowRightPtosis').prop('checked', true)
    else
        $('#chkfollowRightPtosis').prop('checked', false)

    if (SetValues.ptosis_LeftEye == true)
        $('#chkfollowLeftPtosis').prop('checked', true)
    else
        $('#chkfollowLeftPtosis').prop('checked', false)


    if (SetValues.lowVision_RightEye == true)
        $('#chkfollowRightLow').prop('checked', true)
    else
        $('#chkfollowRightLow').prop('checked', false)

    if (SetValues.lowVision_LeftEye == true)
        $('#chkfollowLeftLow').prop('checked', true)
    else
        $('#chkfollowLeftLow').prop('checked', false)

    if (SetValues.rightPupilDefects == true)
        $('#chkfollowRightPupil').prop('checked', true)
    else
        $('#chkfollowRightPupil').prop('checked', false)

    if (SetValues.leftPupilDefects == true)
        $('#chkfollowLeftPupil').prop('checked', true)
    else
        $('#chkfollowLeftPupil').prop('checked', false)

    if (SetValues.pterygium_RightEye == true)
        $('#chkfollowRightPterygium').prop('checked', true)
    else
        $('#chkfollowRightPterygium').prop('checked', false)

    if (SetValues.pterygium_LeftEye == true)
        $('#chkfollowLeftPterygium').prop('checked', true)
    else
        $('#chkfollowLeftPterygium').prop('checked', false)

    if (SetValues.colorBlindness_RightEye == true)
        $('#chkfollowRightColorBlindness').prop('checked', true)
    else
        $('#chkfollowRightColorBlindness').prop('checked', false)

    if (SetValues.colorBlindness_LeftEye == true)
        $('#chkfollowLeftColorBlindness').prop('checked', true)
    else
        $('#chkfollowLeftColorBlindness').prop('checked', false)


    if (SetValues.others_RightEye == true)
        $('#chkfollowRightOthersDisorder').prop('checked', true)
    else
        $('#chkfollowRightOthersDisorder').prop('checked', false)

    if (SetValues.others_LeftEye == true)
        $('#chkfollowLeftOthersDisorder').prop('checked', true)
    else
        $('#chkfollowLeftOthersDisorder').prop('checked', false)


    if (SetValues.fundoscopy_RightEye == true)
        $('#chkfollowRightFundoscopy').prop('checked', true)
    else
        $('#chkfollowRightFundoscopy').prop('checked', false)

    if (SetValues.fundoscopy_LeftEye == true)
        $('#chkfollowLeftFundoscopy').prop('checked', true)
    else
        $('#chkfollowLeftFundoscopy').prop('checked', false)

    if (SetValues.surgery_RightEye == true)
        $('#chkfollowRightSurgery').prop('checked', true)
    else
        $('#chkfollowRightSurgery').prop('checked', false)

    if (SetValues.surgery_LeftEye == true)
        $('#chkfollowLeftSurgery').prop('checked', true)
    else
        $('#chkfollowLeftSurgery').prop('checked', false)

    if (SetValues.cataractSurgery_RightEye == true)
        $('#chkfollowRightCataract').prop('checked', true)
    else
        $('#chkfollowRightCataract').prop('checked', false)

    if (SetValues.cataractSurgery_LeftEye == true)
        $('#chkfollowLeftCataract').prop('checked', true)
    else
        $('#chkfollowLeftCataract').prop('checked', false)

    if (SetValues.surgeryPterygium_RightEye == true)
        $('#chkfollowLeftPterygium').prop('checked', true)
    else
        $('#SurgeryPterygium_RightEye').prop('checked', false)

    if (SetValues.surgeryPterygium_LeftEye== true)
        $('#chkfollowLeftPterygium ').prop('checked', true)
    else
        $('#chkfollowLeftPterygium ').prop('checked', false)

    if (SetValues.surgeryCornealDefect_RightEye == true)
        $('#chkfollowRightCornealDefectSurgery').prop('checked', true)
    else
        $('#chkfollowRightCornealDefectSurgery').prop('checked', false)

    if (SetValues.surgeryCornealDefect_LeftEye == true)
        $('#chkfollowLeftCornealDefectSurgery ').prop('checked', true)
    else
        $('#chkfollowLeftCornealDefectSurgery ').prop('checked', false)

    if (SetValues.surgeryPtosis_RightEye == true)
        $('#chkfollowRightPtosisSurgery').prop('checked', true)
    else
        $('#chkfollowRightPtosisSurgery').prop('checked', false)

    if (SetValues.surgeryPtosis_LeftEye == true)
        $('#chkfollowLeftPtosisSurgery ').prop('checked', true)
    else
        $('#chkfollowLeftPtosisSurgery ').prop('checked', false)

    if (SetValues.surgeryKeratoconus_RightEye == true)
        $('#chkfollowRightKeratoconusSurgery').prop('checked', true)
    else
        $('#chkfollowRightKeratoconusSurgery').prop('checked', false)

    if (SetValues.surgeryKeratoconus_LeftEye == true)
        $('#chkfollowLeftKeratoconusSurgery ').prop('checked', true)
    else
        $('#chkfollowLeftKeratoconusSurgery ').prop('checked', false)

    if (SetValues.chalazion_RightEye == true)
        $('#chkfollowRightChalazion').prop('checked', true)
    else
        $('#chkfollowRightChalazion').prop('checked', false)

    if (SetValues.chalazion_LeftEye == true)
        $('#chkfollowLeftChalazion ').prop('checked', true)
    else
        $('#chkfollowLeftChalazion ').prop('checked', false)


    if (SetValues.hordeolum_RightEye == true)
        $('#chkfollowRightHordeolum').prop('checked', true)
    else
        $('#chkfollowRightHordeolum').prop('checked', false)

    if (SetValues.hordeolum_LeftEye == true)
        $('#chkfollowLeftHordeolum ').prop('checked', true)
    else
        $('#chkfollowLeftHordeolum ').prop('checked', false)


    if (SetValues.surgeryOthers_RightEye == true)
        $('#chkfollowRightOthersSurgery').prop('checked', true)
    else
        $('#chkfollowRightOthersSurgery').prop('checked', false)

    if (SetValues.surgeryOthers_LeftEye == true)
        $('#chkfollowLeftOthersSurgery ').prop('checked', true)
    else
        $('#chkfollowLeftOthersSurgery ').prop('checked', false)

    if (SetValues.rightAmblyopia == true)
        $('#chkfollowRightAmblyopia').prop('checked', true)
    else
        $('#chkfollowRightAmblyopia').prop('checked', false)

    if (SetValues.leftAmblyopia == true)
        $('#chkfollowLeftAmblyopia ').prop('checked', true)
    else
        $('#chkfollowLeftAmblyopia ').prop('checked', false)

    if (SetValues.rightSquint_Surgery == true)
        $('#chkfollowRightSquintSurgery').prop('checked', true)
    else
        $('#chkfollowRightSquintSurgery').prop('checked', false)

    if (SetValues.leftSquint_Surgery == true)
        $('#chkfollowLeftSquintSurgery ').prop('checked', true)
    else
        $('#chkfollowLeftSquintSurgery ').prop('checked', false)
    
    if (SetValues.distanceVision_RightEye_Unaided > 3 || SetValues.distanceVision_LeftEye_Unaided > 3) {
        $('#borderDiv').show()
        $('#ddlRightVisualFieldTest').val(SetValues.rightVisualFieldTestId);
    }


    if (SetValues.distanceVision_RightEye_Unaided > 3 || SetValues.distanceVision_LeftEye_Unaided > 3) {
        $('#borderDiv').show()
        $('#ddlLeftVisualFieldTest').val(SetValues.leftVisualFieldTestId);
    }
    if (SetValues.distanceVision_LeftEye_Unaided <= 3 && SetValues.distanceVision_RightEye_Unaided <= 3) {
        $('#borderDiv').hide();
    }
    if (SetValues.ipd > 0)
        $('#txtIPD').val(SetValues.ipd);

    if (SetValues.rightCycloplagicdrops) {
        $('#chkRightYes').prop('checked', SetValues.rightCycloplagicdrops);
        $('#txtRightMeridian1').val(SetValues.rightMeridian1)
        $('#txtRightMeridian2').val(SetValues.rightMeridian2)
        $('#txtRightAxisRetino').val(SetValues.rightAxisOfRetino)
        $('#txtRightNoGlowVisible').val(SetValues.rightNoGlowVisibile)
        $('#ddlRightCycloSpherical').val(SetValues.right_CycloDrops_Spherical_Status)
        $('#txtRightCycloSpherical').val(SetValues.right_CycloDrops_Spherical_Points)
        $('#ddlRightCycloCyclinderical').val(SetValues.right_CycloDrops_Cyclinderical_Status)
        $('#txtRightCycloCyclinderical').val(SetValues.right_CycloDrops_Cyclinderical_Points)
        $('#txtRightCycloAxix').val(SetValues.right_CycloDrops_Axix)
        $('#txtRightFinalPrescription').val(SetValues.right_CycloDrops_FinalPrescription)
        $('[name="chkRightDrop"]').change();
    }
    else {
        $('#chkRightNo').prop('checked', true);
        $('#chkRightYes').prop('checked', false);
        $('[name="chkRightDrop"]').change();
    }

    if (SetValues.leftCycloplagicdrops) {
        $('#chkLeftYes').prop('checked', SetValues.leftCycloplagicdrops);
        $('#txtLeftMeridian1').val(SetValues.leftMeridian1)
        $('#txtLeftMeridian2').val(SetValues.leftMeridian2)
        $('#txtLeftAxisRetino').val(SetValues.leftAxisOfRetino)
        $('#txtLeftNoGlowVisible').val(SetValues.leftNoGlowVisibile)
        $('#ddlLeftCycloSpherical').val(SetValues.left_CycloDrops_Spherical_Status)
        $('#txtLeftCycloSpherical').val(SetValues.left_CycloDrops_Spherical_Points)
        $('#ddlLeftCycloCyclinderical').val(SetValues.left_CycloDrops_Cyclinderical_Status)

        $('#txtLeftCycloCyclinderical').val(SetValues.left_CycloDrops_Cyclinderical_Points)
        $('#txtLeftCycloAxix').val(SetValues.left_CycloDrops_Axix)
        $('#txtLeftFinalPrescription').val(SetValues.left_CycloDrops_FinalPrescription)
        $('[name="chkLeftDrop"]').change();
    }
    else {
        $('#chkLeftNo').prop('checked', true);
        $('#chkLeftYes').prop('checked', false);
        $('[name="chkLeftDrop"]').change();
    }
    if (SetValues.treatmentId == 1) {
        $('#chkGlassesSuggested').prop("checked", true);
        $('#chkGlassesnotSuggested').prop("checked", false);
        $('#chkGlassesnotWilling').prop("checked", false);
        $('#txtMedicine').val(SetValues.medicines)
        $('#txtPrescription').val(SetValues.prescription)
    }
    else {
        $('input[type="checkbox"][name=chkTreatment][value="' + SetValues.treatmentId + '"]').prop('checked', true);
        $('#chkGlassesSuggested').prop("checked", false);
        $('#txtMedicine').val('')
        $('#txtPrescription').val('')
        if (SetValues.treatmentId == 2)
            $('#chkGlassesnotWilling').prop("checked", false);
        else
            $('#chkGlassesnotSuggested').prop("checked", false);
    }
    /*$('input[type="checkbox"][name=chkReferToHospital][value="' + SetValues.nextVisit_ReferToHospital + '"]').prop('checked', true);*/
    $('#chkReferToHospital').prop("checked", SetValues.nextVisit_ReferToHospital)


    GetNewUpgradeCheckbox();
    //SetValues.Douchrome
    //SetValues.Achromatopsia
    //SetValues.RetinoScopy_RightEye
    //SetValues.Condition_RightEye
    //SetValues.Meridian1_RightEye
    //SetValues.Meridian2_RightEye
    //SetValues.FinalPrescription_RightEye
    //SetValues.RetinoScopy_LeftEye
    //SetValues.Condition_LeftEye
    //SetValues.Meridian1_LeftEye
    //SetValues.Meridian2_LeftEye
    //SetValues.FinalPrescription_LeftEye  
 

}
function SetValues(object) {
    SetWearGlasses(object.wearGlasses);
    //$('#chkDistance').prop("checked", object.distance);
    //$('#chkNear').prop("checked", object.near);
    $('#txtAutoRefResidentId').val(object.autoRefResidentId)
    $('#chkWearGlasses').change();
    //$('#txtResidentAutoId').val(object.ResidentAutoId)
    //$('#txtResidentCode').val(object.ResidentCode)
    //$('#ddlLocality').val(object.LocalityCode).trigger('change');
    //$('#txtLocalityCode').val(object.LocalityCode)
    //$('#txtLocalityAutoId').val(object.LocalityAutoId);
    $('#ddlGender').val(object.genderAutoId);
    $('#ddlGender').change();
    $('#txtAge').val(object.age);
    $('#ddlRelationType').val(object.relationType);
    $('#txtRelationName').val(object.relationName);
    $('#txtLocalityName').val(object.localityName);
    $('#txtResidentName').val(object.residentName);
    //SetDecreasedVision(object.decreasedVision);
    //SetDistance(object.distance);
    //SetNear(object.near);
    //SetDecreasedVision(object.decreasedVision);
    //SetReligon(object.religion);
    $('#txtCell').val(object.mobileNo)
    $('#txtName').val(object.residentName)
    $('#txtCNIC').val(object.cnic)
    /*$('#txtEnrollementDate').val(object.enrollementDate.substr(0, 10));*/
    if (object.genderAutoId == 1)
        $('#txtGender').val('Male')
    else
        $('#txtGender').val('Female')
}
function SetWearGlasses(value) {
    if (value == true) {
        $('#chkWearGlasses').prop('checked', true);
        $('input[name="Right-With-Glasses"]').prop('disabled', false);
        $('input[name="Left-With-Glasses"]').prop('disabled', false);
    }
    else {
        $('#chkWearGlasses').prop('checked', false);
        $('input[name="Right-With-Glasses"]').prop('disabled', true);
        $('input[name="Left-With-Glasses"]').prop('disabled', true);
    }
    
}



function load_Tabs() {
    var navListItems = $('div.setup-panel div a'),
        allWells = $('.setup-content'),
        allNextBtn = $('.nextBtn');

    allWells.hide();

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr('href')),
            $item = $(this);

        if (!$item.hasClass('disabled')) {
            navListItems.removeClass('btn-primary').addClass('btn-default');
            $item.addClass('btn-primary');
            allWells.hide();
            $target.show();
            //  $target.find('input:eq(0)').focus();
        }
    });

    allNextBtn.click(function () {
        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']"),
            isValid = true;

        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }
        }

        if (isValid) nextStepWizard.removeAttr('disabled').trigger('click');
    });

    $('div.setup-panel div a.btn-primary').trigger('click');
}


$('#btnNext').click(function () {
    var navListItems = $('div.setup-panel div a'),
        allWells = $('.setup-content'),
        allNextBtn = $('.nextBtn');

    allWells.hide();

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr('href')),
            $item = $(this);

        if (!$item.hasClass('disabled')) {
            navListItems.removeClass('btn-primary').addClass('btn-default');
            $item.addClass('btn-primary');
            allWells.hide();
            $target.show();
            //  $target.find('input:eq(0)').focus();
        }
    });

    allNextBtn.click(function () {
        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']"),
            isValid = true;

        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }
        }

        if (isValid) nextStepWizard.removeAttr('disabled').trigger('click');
    });

    $('div.setup-panel div a.btn-primary').trigger('click');
})


$("#autocomplete-input-Locality").keydown(function (e) {
    var keyCode = e.keyCode || e.which; 
    //Regex for Valid Characters i.e. Alphabets and Numbers.
    var regex = /^[A-Za-z0-9]+$/; 
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 39 || e.keyCode == 40) {
        return;
    }
    if (isValid) {
        GetAutoCompleteComapny();
    }
})
function GetLocalityText() {
    
    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        SearchText: $("#autocomplete-input-Locality").val()
    }
    return Model;
}


$("#autocomplete-input-Resident").keydown(function (e) {
    var keyCode = e.keyCode || e.which;
    //Regex for Valid Characters i.e. Alphabets and Numbers.
    var regex = /^[A-Za-z0-9]+$/;
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 39 || e.keyCode == 40) {
        return;
    }
    if (isValid) {
        GetAutoCompleteResident();
    }
})
function GetAutoCompleteComapny() {
   
    Localities = [];
    
    $.ajax({
        //url: '/DropDownLookUp/Help/GetLocalitiesForOptometristResident/' + Type+'/'+$("#autocomplete-input-Locality").val(),
        url: '/DropDownLookUp/Help/GetLocalitiesForOptometristResident',
        data: GetLocalityText(),
        method: 'get',
        dataType: 'json',
        success: function (result) {
            Localities = result;
        }
    })

    $("#autocomplete-input-Locality").autocomplete({
        source: function (request, response) {
            var term = request.term.toLowerCase();
            // Filter the item list based on the search term
            var filteredList = $.grep(Localities, function (item) {
                //console.log(Localities);
                return item.text.toLowerCase().indexOf(term) !== -1;

            });

            response(filteredList); // Pass the filtered list to the response function
        },
        minLength: 0, // Set the minimum length of input before triggering autocomplete
        select: function (event, ui) {
            console.log(event, ui);
            // Handle the selection of an item
            var selectedId = ui.item.id;
            var selectedName = ui.item.text;
            var code = ui.item.code;
            name = ui.item.values;
            //.log("Selected ID: " + selectedId);
            //console.log("Selected Name: " + selectedName);

            setTimeout(function () {
                document.getElementById("autocomplete-input-Locality").value = selectedName;
                document.getElementById("txtLocalityCode").value = code;
                document.getElementById("txtLocalityId").value = selectedId;
                GetAutoCompleteResident();


            }, 50);
        }

    }).data("ui-autocomplete")._renderItem = function (ul, item) {
        item.value = item.text;
        item.label = item.text;
        // Customize the rendering of each autocomplete item
        return $("<li>")
            .append("<div>" + item.text + "</div>")
            .appendTo(ul);
    };

}


function GetVisitDate() {
    var now = new Date($('#txtVisitDate').val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    return FinalDate;
}
function GeResidentText() {
    //var now = new Date($('#txtVisitDate').val());
    //var day = ("0" + now.getDate()).slice(-2);
    //var month = ("0" + (now.getMonth() + 1)).slice(-2);
    //var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        AutoId: $('#txtLocalityId').val(),
        SearchText: $("#autocomplete-input-Resident").val(),
        TransDate: GetVisitDate()
    }
    return Model;
}
function GetAutoCompleteResident() {
    //ResetResidentValue();
    if ($('#txtVisitDate').val() == null || $('#txtVisitDate').val() == "") return;
    var Residents = [];
    if ($('#txtLocalityId').val() > 0) {
        $('#autocomplete-input-Resident').focus();
        $.ajax({
            //url: '/DropDownLookUp/Help/GetResidentsForOptometrist/' + $('#txtLocalityId').val() + "/" + Type + "/" + FinalDate,
            url: '/DropDownLookUp/Help/GetResidentsForOptometrist',
            data: GeResidentText(),
            method: 'get',
            dataType: 'json',
            success: function (result) {
                Residents = result;
            }
        })

        $("#autocomplete-input-Resident").autocomplete({
            source: function (request, response) {
                var term = request.term.toLowerCase();
                // Filter the item list based on the search term
                var filteredList = $.grep(Residents, function (item) {
                    //console.log(Localities);
                    return item.text.toLowerCase().indexOf(term) !== -1;

                });

                response(filteredList); // Pass the filtered list to the response function
            },
            minLength: 0, // Set the minimum length of input before triggering autocomplete
            select: function (event, ui) {
                console.log(event, ui);
                // Handle the selection of an item
                var selectedId = ui.item.id;
                var selectedName = ui.item.text;
                var code = ui.item.code;
                name = ui.item.values;
                //console.log("Selected ID: " + selectedId);
                //console.log("Selected Name: " + selectedName);

                setTimeout(function () {
                    document.getElementById("autocomplete-input-Resident").value = selectedName.substr(0, selectedName.length-11);
                    document.getElementById("txtResidentCode").value = code;
                    document.getElementById("txtResidentAutoId").value = selectedId;
                    GetModal($('#txtResidentAutoId').val());
                    GetResidentLastHistory();
                    GetDates();
                    GetResidentInfo($("#txtResidentAutoId").val())

                }, 50);
            }

        }).data("ui-autocomplete")._renderItem = function (ul, item) {
            item.value = item.text;
            item.label = item.text;
            // Customize the rendering of each autocomplete item
            return $("<li>")
                .append("<div>" + item.text + "</div>")
                .appendTo(ul);
        };
    }
}

function GetResidentLastHistory() {
    if ($('#txtResidentAutoId').val() || 0 > 0) {
        $.ajax({
            url: '/Localities/AutoRefTestResident/GetLastHistoryById/' + $('#txtResidentAutoId').val(),
            method: 'get',
            dataType: 'json',
            async: false,
            success: function (result) {
                console.log("result  ", result);
                setTimeout(function () {
                    SetLastVisitHistory(result);
                }, 500)

            }
        })
    }
}

function SetLastVisitHistory(data) {
    ClearAutoRefractionTestHistory();
    if (data != null) {
        $('#txtlastVisitDate').val(data["lastVisitDate"]);
        $('#txtLastLeftAxix').val(data["left_Axix_From"]);
        $('#txtLastRightAxix').val(data["right_Axix_From"]);
        $('#txtLastLeftCyclinderical').val(data["left_Cyclinderical_Points"]);
        $('#txtLastRightCyclinderical').val(data["right_Cyclinderical_Points"]);
        $('#txtLastLeftSpherical').val(data["left_Spherical_Points"]);
        $('#txtLastRightSpherical').val(data["right_Spherical_Points"]);
        $('#txtLastIPD').val(data["ipd"]);
        //console.log(data["right_Spherical_Points"]);
    }
}

function ClearAutoRefractionTestHistory() {
    $('#txtlastVisitDate').val('');
    $('#txtLastLeftAxix').val('');
    $('#txtLastRightAxix').val('');
    $('#txtLastLeftCyclinderical').val('');
    $('#txtLastRightCyclinderical').val('');
    $('#txtLastLeftSpherical').val('');
    $('#txtLastRightSpherical').val('');
    $('#txtLastIPD').val('');
}
$('[name="chkRightDrop"]').change(function () {
    if (!$('#chkRightYes').prop('checked')) {
        $('#txtRightMeridian1').prop('disabled', true);
        $('#txtRightMeridian2').prop('disabled', true);
        $('#txtRightAxisRetino').prop('disabled', true);
        $('#txtRightNoGlowVisible').prop('disabled', true);
        $('#ddlRightCycloSpherical').prop('disabled', true);
        $('#txtRightCycloSpherical').prop('disabled', true);
        $('#ddlRightCycloCyclinderical').prop('disabled', true);
        $('#txtRightCycloCyclinderical').prop('disabled', true);
        $('#txtRightCycloAxix').prop('disabled', true);
        $('#txtRightFinalPrescription').prop('disabled', true);
    }
    else {
        $('#txtRightMeridian1').prop('disabled', false);
        $('#txtRightMeridian2').prop('disabled', false);
        $('#txtRightAxisRetino').prop('disabled', false);
        $('#txtRightNoGlowVisible').prop('disabled', false);
        $('#ddlRightCycloSpherical').prop('disabled', false);
        $('#txtRightCycloSpherical').prop('disabled', false);
        $('#ddlRightCycloCyclinderical').prop('disabled', false);
        $('#txtRightCycloCyclinderical').prop('disabled', false);
        $('#txtRightCycloAxix').prop('disabled', false);
        $('#txtRightCycloAxix').prop('readonly', false);
        $('#txtRightFinalPrescription').prop('disabled', false);
        $('#txtRightMeridian1').focus();

    }
})

$('[name="chkLeftDrop"]').change(function () {
    if (!$('#chkLeftYes').prop('checked')) {
        $('#txtLeftMeridian1').prop('disabled', true);
        $('#txtLeftMeridian2').prop('disabled', true);
        $('#txtLeftAxisRetino').prop('disabled', true);
        $('#txtLeftNoGlowVisible').prop('disabled', true);
        $('#ddlLeftCycloSpherical').prop('disabled', true);
        $('#txtLeftCycloSpherical').prop('disabled', true);
        $('#ddlLeftCycloCyclinderical').prop('disabled', true);
        $('#txtLeftCycloCyclinderical').prop('disabled', true);
        $('#txtLeftCycloAxix').prop('disabled', true);
        $('#txtLeftFinalPrescription').prop('disabled', true);
    }
    else {
        $('#txtLeftMeridian1').prop('disabled', false);
        $('#txtLeftMeridian2').prop('disabled', false);
        $('#txtLeftAxisRetino').prop('disabled', false);
        $('#txtLeftNoGlowVisible').prop('disabled', false);
        $('#ddlLeftCycloSpherical').prop('disabled', false);
        $('#txtLeftCycloSpherical').prop('disabled', false);
        $('#ddlLeftCycloCyclinderical').prop('disabled', false);
        $('#txtLeftCycloCyclinderical').prop('disabled', false);
        $('#txtLeftCycloAxix').prop('disabled', false);
        $('#txtLeftCycloAxix').prop('readonly', false);
        $('#txtLeftFinalPrescription').prop('disabled', false);
        $('#txtLeftMeridian1').focus();

    }
})
$('#txtRightCyclinderical').change(function () {
    if ($('#txtRightCyclinderical').val().trim() != "")
        $('#txtRightAxix').prop("readonly", false);
    else {
        $('#txtRightAxix').val('');
        $('#txtRightAxix').prop("readonly", true);
    }
})
$('#txtLeftCyclinderical').change(function () {
    if ($('#txtLeftCyclinderical').val().trim() != "")
        $('#txtLeftAxix').prop("readonly", false);
    else {
        $('#txtLeftAxix').val('');
        $('#txtLeftAxix').prop("readonly", true);
    }
})

$('#btnNextFromAcuity').click(function () {

    if (!validateNextAcuity()) return;
    NextFromAcuity();
    //if (!ValidateForVisualAcuityNear()) return;
    //NextbtnFromAcuity();
    GetNewUpgradeCheckbox();
    setTimeout(function () {
        GetNewUpgradeCheckbox();
        GetCheckDiagnosticPolicy();
    },
        500);
    
    //$('#step2').click();
})
function NextFromAcuity() {
    var retVal = true;

    // 1. If VA 6/6 and N6 Resident go to Diagnostic Page.
    if (
        (GetDistanceVision_RightEye_Unaided() == 0 && GetDistanceVision_RightEye_PinHole() == 0 && GetDistanceVision_LeftEye_Unaided() == 0 && GetDistanceVision_LeftEye_PinHole() == 0
            && GetNearVision_LeftEye() == 0 && GetNearVision_RightEye() == 0) &&
        ($('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false) &&
        ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
    ) {
        $('#step4').click(); // Go to Diagnosis  Treatment & Follow Up Page 
    }

    //  Full filliing 3 conditions
    //  1. IF VA is less than 6/6 Resident, it will move to Subjective Refraction and then on Diagnosis
    //  2. If VA less then 6/6 and N6 ,go to Subjective then to Diagnostic,
    //  3. If VA 6/6 and less than N6, go to subjective , then to diagnostic
    else if (
        (
            (GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0)
            || (GetNearVision_LeftEye() > 0 || GetNearVision_RightEye() > 0))
        && ($('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false) &&
        ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
    ) {
        if (!ValidateToFillSubjectiveRefraction()) return;
        $('#step4').click(); // Go to Diagnosis  Treatment & Follow Up Page 
    }

    // 1. If VA 6/6 + N6 + Squint it will check the history squint for particular eye and go to diagnostic page
    else if (
        (GetDistanceVision_RightEye_Unaided() == 0 && GetDistanceVision_RightEye_PinHole() == 0 && GetDistanceVision_LeftEye_Unaided() == 0 && GetDistanceVision_LeftEye_PinHole() == 0
            && GetNearVision_LeftEye() == 0 && GetNearVision_RightEye() == 0)
        && ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true) &&
        ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
    ) {
        if ($('#chkSquintRight').prop('checked') == true) {
            $('#chkfollowRightSquint').prop('checked', true);
        }
        else {
            $('#chkfollowRightSquint').prop('checked', false);
        }
        if ($('#chkSquintLeft').prop('checked') == true) {
            $('#chkfollowLeftSquint').prop('checked', true);
        }
        else {
            $('#chkfollowLeftSquint').prop('checked', false);
        }
        $('#step4').click();
    }
    // If VA less than 6/6 + Squint it will go to Orthoptic Assesment and when go to Diagnostic page, system select
    // refer to hospital and for Squint Assessment
    else if (
        (GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0)
        //&& (GetNearVision_LeftEye() == 0 && GetNearVision_RightEye() == 0)
        && ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true)
        && ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
    ) {
        if ($('#chkSquintRight').prop('checked') == true) {
            $('#chkfollowRightSquintAssessment').prop('checked', true);
        }
        else {
            $('#chkfollowRightSquintAssessment').prop('checked', false);
        }
        if ($('#chkSquintLeft').prop('checked') == true) {
            $('#chkfollowLeftSquintAssessment').prop('checked', true);
        }
        else {
            $('#chkfollowLeftSquintAssessment').prop('checked', false);
        }
        $('#step2').click();
    }

    else if (
        (GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0
        || GetNearVision_LeftEye() > 0 || GetNearVision_RightEye() > 0)
        && ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true)
        && ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
    ) {
        if ($('#chkSquintRight').prop('checked') == true) {
            $('#chkfollowRightSquintAssessment').prop('checked', true);
        }
        else {
            $('#chkfollowRightSquintAssessment').prop('checked', false);
        }
        if ($('#chkSquintLeft').prop('checked') == true) {
            $('#chkfollowLeftSquintAssessment').prop('checked', true);
        }
        else {
            $('#chkfollowLeftSquintAssessment').prop('checked', false);
        }
        $('#step2').click();
    }
    else if (
        ($('#chkAmblyopicRight').prop('checked') == true || $('#chkAmblyopicLeft').prop('checked') == true)
        ) 
    {
        if ($('#chkAmblyopicRight').prop('checked') == true) {
            $('#chkfollowRightFurtherAssessment').prop('checked', true);
        }
        else {
            $('#chkfollowRightFurtherAssessment').prop('checked', false);
        }
        if ($('#chkAmblyopicLeft').prop('checked') == true) {
            $('#chkfollowLeftFurtherAssessment').prop('checked', true);
        }
        else {
            $('#chkfollowLeftFurtherAssessment').prop('checked', false);
        }
        $('#step4').click();
    }
    

}

function hideValidateFillSubjectiveRefraction() {
    AddVAlidationToControl("txtRightSpherical", "hide", "", "bottom")
    AddVAlidationToControl("txtLeftSpherical", "hide", "", "top")
    AddVAlidationToControl("txtRightCyclinderical", "hide", "", "bottom")
    AddVAlidationToControl("txtLeftCyclinderical", "hide", "", "bottom")
    AddVAlidationToControl("txtRightAxix", "hide", "", "bottom")    
    AddVAlidationToControl("txtLeftAxix", "hide", "", "bottom")
    AddVAlidationToControl("txtIPD", "hide", "", "bottom")
}

function validateNextAcuity() {
    var returnVal = true;

    if ($('#autocomplete-input-Locality').val() == '') {
        returnVal = false;
        AddVAlidationToControl("autocomplete-input-Locality", "show", "Mandatory", "top")
        $(window).scrollTop(0);
        return returnVal
    }
    else {
        AddVAlidationToControl("autocomplete-input-Locality", "hide", "", "bottom")
    }

    if ($('#txtLocalityCode').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtLocalityCode", "show", "Mandatory", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtLocalityCode", "hide", "", "bottom")
    }

    if ($('#autocomplete-input-Resident').val() == '') {
        returnVal = false;
        AddVAlidationToControl("autocomplete-input-Resident", "show", "Mandatory", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("autocomplete-input-Resident", "hide", "", "bottom")
    }

    if ($('#txtResidentCode').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtResidentCode", "show", "Mandatory", "top")
        $(window).scrollTop(0);
        return returnVal
    }
    else {
        AddVAlidationToControl("txtResidentCode", "hide", "", "bottom")
    }

    // Visual Acuity Right Un-aided Checked
    var atLeastOneCheckedRightUnaided = false;
    $('input[name="Right-Un-aided"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedRightUnaided = true;
            //return atLeastOneCheckedRightUnaided; // exit the loop early
        }
    });
    if (atLeastOneCheckedRightUnaided == false) {
        atLeastOneCheckedRightUnaided = false;
        AddVAlidationToControl("Right-Un-aided1", "show", "Mandatory", "top")
        var scrollTop = window.pageYOffset;
        window.scrollTo(0, scrollTop - 100);
        returnVal = false;
    }
    else {
        AddVAlidationToControl("Right-Un-aided1", "hide", "", "top")
    }


    // Visual Acuity Right With Glasses Checked
    if ($('#chkWearGlasses').is(':checked') == true) {
        var atLeastOneCheckedRightWithGlasses = false;
        $('input[name="Right-With-Glasses"]').each(function () {
            if ($(this).prop('checked')) {
                atLeastOneCheckedRightWithGlasses = true;
                // exit the loop early
            }
        });
        if (atLeastOneCheckedRightWithGlasses == false) {
            atLeastOneCheckedRightWithGlasses = false;
            AddVAlidationToControl("Right-With-Glasses1", "show", "Mandatory", "top")
            var scrollTop = window.pageYOffset;
            window.scrollTo(0, scrollTop - 100);
            returnVal = false;
        }
        else {
            AddVAlidationToControl("Right-With-Glasses1", "hide", "", "top")
        }
    }
    //Right Pin Hole
    var atLeastOneCheckedRightPinHole = false;
    $('input[name="Right-Pin-Hole"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedRightPinHole = true;
            // exit the loop early
        }
    });
    if (atLeastOneCheckedRightPinHole == false) {
        atLeastOneCheckedRightPinHole = false;
        AddVAlidationToControl("Right-Pin-Hole1", "show", "Mandatory", "top")
        var scrollTop = window.pageYOffset;
        window.scrollTo(0, scrollTop - 100);
        returnVal = false;
    }
    else {
        AddVAlidationToControl("Right-Pin-Hole1", "hide", "", "top")
    }



    // Visual Acuity Left Un-aided Checked
    var atLeastOneCheckedLefttUnaided = false;
    $('input[name="Left-Un-aided"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedLefttUnaided = true;
             // exit the loop early
        }
    });
    if (atLeastOneCheckedLefttUnaided == false) {
        atLeastOneCheckedLefttUnaided = false;
        AddVAlidationToControl("Left-Un-aided1", "show", "Mandatory", "top")
        var scrollTop = window.pageYOffset;
        window.scrollTo(0, scrollTop - 100);
        returnVal = false;
    }
    else {
        AddVAlidationToControl("Left-Un-aided1", "hide", "", "top")
    }


    // Visual Acuity Right With Glasses Checked
    if ($('#chkWearGlasses').is(':checked') == true) {
        var atLeastOneCheckedLeftWithGlasses = false;
        $('input[name="Left-With-Glasses"]').each(function () {
            if ($(this).prop('checked')) {
                atLeastOneCheckedLeftWithGlasses = true;
                // exit the loop early
            }
        });
        if (atLeastOneCheckedLeftWithGlasses == false) {
            atLeastOneCheckedLeftWithGlasses = false;
            AddVAlidationToControl("Left-With-Glasses1", "show", "Mandatory", "top")
            var scrollTop = window.pageYOffset;
            window.scrollTo(0, scrollTop - 100);
            returnVal = false;
        }
        else {
            AddVAlidationToControl("Left-With-Glasses1", "hide", "", "top")
        }
    }
    //Left Pin Hole
    var atLeastOneCheckedLeftPinHole = false;
    $('input[name="Left-Pin-Hole"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedLeftPinHole = true;
            // exit the loop early
        }
    });
    if (atLeastOneCheckedLeftPinHole == false) {
        atLeastOneCheckedLeftPinHole = false;
        AddVAlidationToControl("Left-Pin-Hole1", "show", "Mandatory", "top")
        var scrollTop = window.pageYOffset;
        window.scrollTo(0, scrollTop - 100);
        returnVal = false;
    }
    else {
        AddVAlidationToControl("Left-Pin-Hole1", "hide", "", "top")
    }

    // Visual Acuity Near Left VA Checked
    var atLeastOneCheckedRightVA= false;
    $('input[name="Right-N"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedRightVA = true;
            return atLeastOneCheckedRightVA; // exit the loop early
        }
    });
    if (atLeastOneCheckedRightVA == false) {
        atLeastOneCheckedRightVA = false;
        AddVAlidationToControl("Right-N-6", "show", "Mandatory", "top")
        var scrollTop = window.pageYOffset;
        window.scrollTo(0, scrollTop - 100);
        returnVal = false;
    }
    else {
        AddVAlidationToControl("Right-N-6", "hide", "", "top")
    }

    var atLeastOneCheckedLefttVA = false;
    $('input[name="Left-N"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedLefttVA = true;
            return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });
    if (atLeastOneCheckedLefttVA == false) {
        atLeastOneCheckedLefttVA = false;
        AddVAlidationToControl("Left-N-6", "show", "Mandatory", "top")
        var scrollTop = window.pageYOffset;
        window.scrollTo(0, scrollTop - 100);
        returnVal = false;
    }
    else {
        AddVAlidationToControl("Left-N-6", "hide", "", "top")
    }
     

    if ($('#NearSquint').prop('checked')) {
        if (!$('#chkSquintRight').prop('checked') && !$('#chkSquintLeft').prop('checked')) {
            AddVAlidationToControl("chkSquintLeft", "show", "Mandatory", "top")
            AddVAlidationToControl("chkSquintRight", "show", "Mandatory", "top")
            returnVal = false;
        }
        else {
            AddVAlidationToControl("chkSquintLeft", "hide", "", "top")
            AddVAlidationToControl("chkSquintRight", "hide", "", "top")
        }
    }

    if ($('#NearAmblyopic').prop('checked')) {
        if (!$('#chkAmblyopicRight').prop('checked') && !$('#chkAmblyopicLeft').prop('checked')) {
            AddVAlidationToControl("chkAmblyopicRight", "show", "Mandatory", "top")
            AddVAlidationToControl("chkAmblyopicLeft", "show", "Mandatory", "top")
            returnVal = false;
        }
        else {
            AddVAlidationToControl("chkAmblyopicRight", "hide", "", "top")
            AddVAlidationToControl("chkAmblyopicLeft", "hide", "", "top")
  
        }
    }

    return returnVal;

}

function ValidateForVisualAcuityNear() {
    var val = true;
    if ((GetNearVision_RightEye() > 0 || GetNearVision_LeftEye() > 0) && ($('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false)
        && ($('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false)) {
        if (!ValidateToFillSubjectiveRefraction()) val = false;

    }
    return val;
}
function ValidateToFillSubjectiveRefraction() {
    var returnVal = true;
    if (!$('#Right-NoImprovement').is(":checked") && GetRightVisualFieldTestValue() == 0) {
        if ($('#txtRightSpherical').val() == '') {
            returnVal = false;
            AddVAlidationToControl("txtRightSpherical", "show", "Mandatory", "top")
        }
        else {
            AddVAlidationToControl("txtRightSpherical", "hide", "", "bottom")
        }

        if ($('#txtRightCyclinderical').val().trim() == '') {
            returnVal = false;
            AddVAlidationToControl("txtRightCyclinderical", "show", "Mandatory", "top")
        }
        else {
            AddVAlidationToControl("txtRightCyclinderical", "hide", "", "bottom")
        }

        if (($('#txtRightCyclinderical').val().trim() != '' && $('#txtRightCyclinderical').val().trim() != '0') && $('#txtRightAxix').val().trim() == "") {
            returnVal = false;
            AddVAlidationToControl("txtRightAxix", "show", "Mandatory", "top")
        }
        else {
            AddVAlidationToControl("txtRightAxix", "hide", "", "bottom")
        }
    }
    else {
        AddVAlidationToControl("txtRightSpherical", "hide", "", "bottom")
        AddVAlidationToControl("txtRightCyclinderical", "hide", "", "bottom")
        AddVAlidationToControl("txtRightAxix", "hide", "", "bottom")
    }
    if (!$('#Left-NoImprovement').is(":checked") && GetLeftVisualFieldTestValue() == 0 ) {
        if ($('#txtLeftSpherical').val() == '') {
            returnVal = false;
            AddVAlidationToControl("txtLeftSpherical", "show", "Mandatory", "top")

        }
        else {
            AddVAlidationToControl("txtLeftSpherical", "hide", "", "bottom")
        }
        if ($('#txtLeftCyclinderical').val().trim() == '') {
            returnVal = false;
            AddVAlidationToControl("txtLeftCyclinderical", "show", "Mandatory", "top")
        }
        else {
            AddVAlidationToControl("txtLeftCyclinderical", "hide", "", "bottom")
        }

        if (($('#txtLeftCyclinderical').val().trim() != '' && $('#txtLeftCyclinderical').val().trim() != '0') && $('#txtLeftAxix').val().trim() == "") {
            returnVal = false;
            AddVAlidationToControl("txtLeftAxix", "show", "Mandatory", "top")
        }
        else {
            AddVAlidationToControl("txtLeftAxix", "hide", "", "bottom")
        }
        if ($('#txtIPD').val() == "") {
            returnVal = false;
            $('#step1').click();
            AddVAlidationToControl("txtIPD", "show", "Mandatory", "top")
        }
        else {
            AddVAlidationToControl("txtIPD", "hide", "", "bottom")
        }
    }
    return returnVal;
}
function NextbtnFromAcuity() {
    //var Right_un_aided = GetDistanceVision_RightEye_Unaided();
    //console.log("Rf ",Right_un_aided );
   // if ($('#Right-Un-aided1').prop('checked') && $('#Left-Un-aided1').prop('checked') && $('#Right-N-6').prop('checked') && $('#Left-N-6').prop('checked')) {
    if (GetDistanceVision_RightEye_Unaided() == 0 && GetDistanceVision_LeftEye_Unaided() == 0 && GetDistanceVision_RightEye_PinHole() == 0 && GetDistanceVision_LeftEye_PinHole() == 0 && GetNearVision_RightEye() == 0 && GetNearVision_LeftEye() == 0 && $('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false
        && !$('#chkSquintRight').prop('checked') && !$('#chkSquintLeft').prop('checked')) {
        $('#step4').click(); // Go to Diagnosis  Treatment & Follow Up Page 
        //return "step4";
    }

    else if (((GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_LeftEye_Unaided() > 0) || (GetNearVision_RightEye() == 0 && GetNearVision_LeftEye() == 0) || (GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_LeftEye_PinHole() > 0))
        && ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false) && ($('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false)
    ) {

        if (!ValidateToFillSubjectiveRefraction()) return;
        $('#step4').click(); // Go to Diagnosis  Treatment & Follow Up Page 
        // return "step4";
    }

    else if ((GetDistanceVision_RightEye_Unaided() == 0 && GetDistanceVision_LeftEye_Unaided() == 0) && (GetNearVision_RightEye() > 0 || GetNearVision_LeftEye() > 0)
        && $('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false && $('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false
    ) {

        if (!ValidateToFillSubjectiveRefraction()) return;
        $('#step4').click(); // Go to Diagnosis  Treatment & Follow Up Page 
        // return "step4";
    }

    else if (GetDistanceVision_RightEye_Unaided() == 0 && GetDistanceVision_LeftEye_Unaided() == 0 && GetDistanceVision_RightEye_PinHole() == 0 && GetDistanceVision_LeftEye_PinHole() == 0 && GetNearVision_RightEye() == 0 && GetNearVision_LeftEye() == 0 && $('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false
        && ($('#chkSquintRight').prop('checked') || $('#chkSquintLeft').prop('checked'))
    ) {

        $('#chkNormal').prop('checked', true);
        if ($('#chkSquintRight').prop('checked'))
            $('#chkfollowRightSquint').prop('checked', true);
        else
            $('#chkfollowRightSquint').prop('checked', false);

        if ($('#chkSquintLeft').prop('checked'))
            $('#chkfollowLeftSquint').prop('checked', true);
        else
            $('#chkfollowLeftSquint').prop('checked', false);


        $('#step4').click(); // Go to Diagnosis  Treatment & Follow Up Page 
        //return "step4";
    }

    else if ((GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_LeftEye_Unaided() > 0) && $('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false
        && ($('#chkSquintRight').prop('checked') || $('#chkSquintLeft').prop('checked'))
    ) {

        if ($('#chkSquintRight').prop('checked'))
            $('#chkfollowRightSquintAssessment').prop('checked', true);
        else
            $('#chkfollowRightSquintAssessment').prop('checked', false);

        if ($('#chkSquintLeft').prop('checked'))
            $('#chkfollowLeftSquintAssessment').prop('checked', true);

        else
            $('#chkfollowLeftSquintAssessment').prop('checked', false);


        $('#step2').click(); // Go to Orthoptic Assessment Page
        //return "step2";
    }
    else if (($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true)
        && (GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_LeftEye_PinHole() > 0)
        && (GetNearVision_RightEye() == 0 || GetNearVision_LeftEye() == 0)
    )
    {

        $('#chkNormal').prop('checked', true);
        if ($('#chkSquintRight').prop('checked'))
            $('#chkfollowRightSquintAssessment').prop('checked', true);
        else
            $('#chkfollowRightSquintAssessment').prop('checked', false);

        if ($('#chkSquintLeft').prop('checked'))
            $('#chkfollowLeftSquintAssessment').prop('checked', true);
        else
            $('#chkfollowLeftSquintAssessment').prop('checked', false);
        $('#step2').click();
    }
    

    else if ($('#chkAmblyopicRight').prop('checked') || $('#chkAmblyopicLeft').prop('checked')) {
        $('#chkfollowRightFurtherAssessment').prop('checked', true);
        $('#chkfollowLeftFurtherAssessment').prop('checked', true);
        $('#step4').click();
        //return "step4";
    }

    else if (($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true) && ($('#chkAmblyopicRight').prop('checked') == true || $('#chkAmblyopicLeft').prop('checked') == true)) {
        
        if ($('#chkAmblyopicRight').prop('checked') )
            $('#chkfollowRightFurtherAssessment').prop('checked', true);
        else
            $('#chkfollowRightFurtherAssessment').prop('checked', false);

        if ($('#chkAmblyopicLeft').prop('checked'))
            $('#chkfollowLeftFurtherAssessment').prop('checked', true);
        else
            $('#chkfollowLeftFurtherAssessment').prop('checked', false);
        
    }
    if ($('#txtRightSpherical').val() > 0 || $('#txtLeftSpherical').val() > 0) {

        if ($('#txtRightSpherical').val() > 0) {
            $('#chkfollowRightRefractiveError').prop('checked', true);

            if ($('#ddlRightVisualAcuity').val()>0) {
                $('#chkRightAcuityDistance').prop('checked', true);
            }
            else
                $('#chkRightAcuityDistance').prop('checked', false);


            if ($('#txtRightNearAdd').val() > 0) {
                $('#chkRightNearAdd').prop('checked', true);
            }
            else
                $('#chkRightNearAdd').prop('checked', false);
        }
        else
            $('#chkfollowRightRefractiveError').prop('checked', false);

        if ($('#txtLeftSpherical').val() > 0) {
            $('#chkfollowLeftRefractiveError').prop('checked', true);

            if ($('#ddlLeftVisualAcuity').val() > 0) {
                $('#chkLeftAcuityDistance').prop('checked', true);
            }
            else
                $('#chkLeftAcuityDistance').prop('checked', false);


            if ($('#txtLeftNearAdd').val() > 0) {
                $('#chkLeftNearAdd').prop('checked', true);
            }
            else
                $('#chkLeftNearAdd').prop('checked', false);
        }

        else
            $('#chkfollowLeftRefractiveError').prop('checked', false);
    }

    if ((!$('#chkWearGlasses').prop('checked')) && ($('#txtRightSpherical').val() > 0 || $('#txtLeftSpherical').val() > 0))
        $('#chkfollowNew').prop('checked', true);
    else
        $('#chkfollowNew').prop('checked', false);

    if ($('#chkWearGlasses').prop('checked') && ($('#txtRightSpherical').val() > 0 || $('#txtLeftSpherical').val() > 0))
        $('#chkfollowUpgrade').prop('checked', true);
    else
        $('#chkfollowUpgrade').prop('checked', false);



    // Un aided Checked for Visit Summary
    var atLeastOneCheckedPinUnaidedRight =  false;
    var atLeastOneCheckedPinUnaidedLeft = false;

    $('input[name="Right-Un-aided"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedPinUnaidedRight = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });

    $('input[name="Left-Un-aided"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedPinUnaidedLeft = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });

    if (GetDistanceVision_RightEye_Unaided() > 0 )
        $('#chkRightUn-aided').prop('checked', true);
    else
        $('#chkRightUn-aided').prop('checked', false);

    if (GetDistanceVision_LeftEye_Unaided() > 0)
        $('#chkLeftUn-aided').prop('checked', true);
    else
        $('#chkLeftUn-aided').prop('checked', false);



    // Glasses Checked for Visit Summary
    var atLeastOneCheckedGlassesRight = false;
    var atLeastOneCheckedGlassesLeft = false;

    $('input[name="Right-With-Glasses"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedGlassesRight = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });

    $('input[name="Left-With-Glasses"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedGlassesLeft = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });

    //if (atLeastOneCheckedGlassesRight == true)
    if (GetDistanceVision_RightWithGlasses()>0)
        $('#chkRightGlasses').prop('checked', true);
    else
        $('#chkRightGlasses').prop('checked', false);

    //if (atLeastOneCheckedGlassesLeft == true)
    if (GetDistanceVision_LeftWithGlasses() > 0)
        $('#chkLeftGlasses').prop('checked', true);
    else
        $('#chkLeftGlasses').prop('checked', false);





    // Pin Hole Checked for visit SUmmary
    var atLeastOneCheckedPinHoleRight = false;
    var atLeastOneCheckedPinHoleLeft = false;

    $('input[name="Right-Pin-Hole"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedPinHoleRight = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });
    $('input[name="Left-Pin-Hole"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedPinHoleLeft = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });
    //if (atLeastOneCheckedPinHoleRight == true)
    if (GetDistanceVision_RightEye_Unaided()>0)
        $('#chkRightPinHole').prop('checked', true);
    else
        $('#chkRightPinHole').prop('checked', false);

    if (GetDistanceVision_LeftEye_Unaided() > 0)
        $('#chkLeftPinHole').prop('checked', true);
    else
        $('#chkLeftPinHole').prop('checked', false);
        
        

}


function NextbtnFromOrthoptic() {
        if (!ValidateForHirchbergTest()) return;
    
    $('#step3').click();
}

function ValidateForHirchbergTest() {
    var returnVal = true;
    //if ((!$('#Right-Un-aided1').prop('checked') || !$('#Left-Un-aided1').prop('checked')) && (!$('#Right-N-6').prop('checked') || !$('#Left-N-6').prop('checked'))
    //  && ($('#chkSquintRight').prop('checked') || $('#chkSquintLeft').prop('checked'))
    //     )
    //if ((GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0 || GetDistanceVision_RightEye_PinHole() > 0)
    //    && ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') ==true)
    //)
    if (
        ((GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0)
            //&& (GetNearVision_LeftEye() == 0 && GetNearVision_RightEye() == 0)
            && ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true)
            && ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false))
        ||
        ((GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0
            || GetNearVision_LeftEye() > 0 || GetNearVision_RightEye() > 0)
            && ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true)
            && ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
        )
    ) {
        var atLeastOneCheckedDistanceVision = false;

        $('input[name="Distance-Vision"]').each(function () {
            if ($(this).prop('checked')) {
                atLeastOneCheckedDistanceVision = true;
                //return atLeastOneCheckedLefttVA; // exit the loop early
            }
        });
        var atLeastOneCheckedNearVision = false;
        $('input[name="Near-Vision"]').each(function () {
            if ($(this).prop('checked')) {
                atLeastOneCheckedNearVision = true;
                //return atLeastOneCheckedLefttVA; // exit the loop early
            }
        });
        if (atLeastOneCheckedDistanceVision == false) {
            AddVAlidationToControl("Distance-Vision1", "show", "Mandatory", "top")
            console.log("Distance-Vision1");
            returnVal = false;
        }
        else {
            AddVAlidationToControl("Distance-Vision1", "hide", "", "top")
            console.log("Distance-Vision1 OK");
        }

        if (atLeastOneCheckedNearVision == false) {

            AddVAlidationToControl("Near-Vision1", "show", "Mandatory", "top")
            returnVal = false;
            console.log("Near-Vision1");
        }
        else {
            AddVAlidationToControl("Near-Vision1", "hide", "", "top")
            console.log("Near-Vision1 OK");
        }

        if ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true) {
            var atLeastOneCheckedRightOpthalmoscopeRedReflexTest = false;

            $('input[name="RightOpthalmoscope-Red-reflex-test"]').each(function () {
                if ($(this).prop('checked')) {
                    atLeastOneCheckedRightOpthalmoscopeRedReflexTest = true;
                    //return atLeastOneCheckedLefttVA; // exit the loop early
                }
            });
            if (atLeastOneCheckedRightOpthalmoscopeRedReflexTest == false) {
                AddVAlidationToControl("chkRightRed-Glow-Seens", "show", "Mandatory", "top");
                returnVal = false;
                console.log("chkRightRed-Vision1");
            }
            else {
                AddVAlidationToControl("chkRightRed-Glow-Seens", "hide", "", "top");
                console.log("chkRightRed-Vision1");
            }


            var RightCoverUncoverTest = false;

            $('input[name="RightCoverUncoverTest"]').each(function () {
                if ($(this).prop('checked')) {
                    RightCoverUncoverTest = true;
                    //return atLeastOneCheckedLefttVA; // exit the loop early
                }
            });
            if (RightCoverUncoverTest == false) {
                AddVAlidationToControl("chkRightOrthophoria", "show", "Mandatory", "top");
                console.log("chkRightOrthophoria");
                returnVal = false;
            }
            else {
                AddVAlidationToControl("chkRightOrthophoria", "hide", "", "top");
                console.log("chkRightOrthophoria OK");
            }

            if ($('#txtRightOrthopticExtraOccularMuscle').val().trim() == "") {
                AddVAlidationToControl("txtRightOrthopticExtraOccularMuscle", "show", "Mandatory", "top");
                console.log("txtRightOrthopticExtraOccularMuscle");
                returnVal = false;
            }
            else {
                AddVAlidationToControl("txtRightOrthopticExtraOccularMuscle", "hide", "", "top");
                console.log("txtRightOrthopticExtraOccularMuscle OK");
            }
        }

        if ($('#chkSquintLeft').prop('checked') == true || $('#chkSquintRight').prop('checked') == true) {
            var atLeastOneCheckedLeftOpthalmoscopeRedReflexTest = false;

            $('input[name="LeftOpthalmoscope-Red-reflex-test"]').each(function () {
                if ($(this).prop('checked')) {
                    atLeastOneCheckedLeftOpthalmoscopeRedReflexTest = true;
                    //return atLeastOneCheckedLefttVA; // exit the loop early
                }
            });
            if (atLeastOneCheckedLeftOpthalmoscopeRedReflexTest == false) {
                AddVAlidationToControl("chkLeftRed-Glow-Seens", "show", "Mandatory", "top");
                console.log("chkLeftRed-Glow-Seens");
                returnVal = false;
            }
            else {
                AddVAlidationToControl("chkLeftRed-Glow-Seens", "hide", "", "top");
                console.log("chkLeftRed-Glow-Seens OK");
            }



            var LeftCoverUncoverTest = false;

            $('input[name="LeftCoverUncoverTest"]').each(function () {
                if ($(this).prop('checked')) {
                    LeftCoverUncoverTest = true;
                    //return atLeastOneCheckedLefttVA; // exit the loop early
                }
            });
            if (LeftCoverUncoverTest == false) {
                AddVAlidationToControl("chkLeftOrthophoria", "show", "Mandatory", "top");
                console.log("chkLeftOrthophoria");
                returnVal = false;
            }
            else {
                AddVAlidationToControl("chkLeftOrthophoria", "hide", "", "top");
                console.log("chkLeftOrthophoria OK");
            }

            if ($('#txtLeftOrthopticExtraOccularMuscle').val().trim() == "") {
                AddVAlidationToControl("txtLeftOrthopticExtraOccularMuscle", "show", "Mandatory", "top");
                console.log("txtLeftOrthopticExtraOccularMuscle");
                returnVal = false;
            }
            else {
                AddVAlidationToControl("txtLeftOrthopticExtraOccularMuscle", "hide", "", "top");
                console.log("txtLeftOrthopticExtraOccularMuscle OK");
            }
        }
        if ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true) {
            var PupillaryReactions = false;

            $('input[name="PupillaryReactions"]').each(function () {
                if ($(this).prop('checked')) {
                    PupillaryReactions = true;
                    //return atLeastOneCheckedLefttVA; // exit the loop early
                }
            });
            if (PupillaryReactions == false) {
                AddVAlidationToControl("chkRRR", "show", "Mandatory", "top");
                console.log("chkRRR");
                returnVal = false;
            }
            else {
                AddVAlidationToControl("chkRRR", "hide", "", "top");
                console.log("chkRRR OK");
            }
        }

        //if ($('#chkSquintRight').prop('checked')) {
        //    AddVAlidationToControl("txtRightOrthopticExtraOccularMuscle", "show", "Mandatory", "top")
        //    returnVal = false;
        //}
        //else {
        //    AddVAlidationToControl("txtRightOrthopticExtraOccularMuscle", "hide", "", "bottom") 
        //}

        //if ($('#chkSquintLeft').prop('checked')) {
        //    AddVAlidationToControl("txtLeftOrthopticExtraOccularMuscle", "show", "Mandatory", "top")
        //    returnVal = false;
        //}
        //else {
        //    AddVAlidationToControl("txtLeftOrthopticExtraOccularMuscle", "hide", "", "bottom")
        //}


    }
    return returnVal;
}

$('#btnPreviousFromOrthoptic').click(function () {
    //NextbtnFromAcuity();
    $('#step1').click();
})

$('#btnNextFromOrthoptic').click(function () {
    NextbtnFromOrthoptic();
    GetCheckDiagnosticPolicy();
    GetNewUpgradeCheckbox();
    
})

$('#btnPreviousFromVisit').click(function () {
    $('#step2').click();
})


$('#btnNextFromVisit').click(function () {
    $('#step4').click();
})

function GetModel() {
    //var now = new Date($('#txtVisitDate').val());
    //var day = ("0" + now.getDate()).slice(-2);
    //var month = ("0" + (now.getMonth() + 1)).slice(-2);
    //var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);

    var Model = {
        OptometristResidentId: $('#txtOptometristResidentId').val(),
        AutoRefResidentId: $('#txtAutoRefResidentId').val(),
        OptometristResidentTransDate: GetVisitDate(),
        ResidentAutoId: $('#txtResidentAutoId').val(),
        LocalityAutoId: $('#txtLocalityAutoId').val(),
        HasChiefComplain:0,
        ChiefComplainRemarks:null,
        HasOccularHistory:0,
        OccularHistoryRemarks:null,
        HasMedicalHistory:0,
        MedicalHistoryRemarks:null,
        DistanceVision_RightEye_Unaided: GetDistanceVision_RightEye_Unaided(),
        DistanceVision_RightEye_WithGlasses: GetDistanceVision_RightWithGlasses(),
        DistanceVision_RightEye_PinHole: GetDistanceVision_RightEye_PinHole(),
        DistanceVision_LeftEye_Unaided: GetDistanceVision_LeftEye_Unaided(),
        DistanceVision_LeftEye_WithGlasses: GetDistanceVision_LeftWithGlasses(),
        DistanceVision_LeftEye_PinHole: GetDistanceVision_LeftEye_PinHole(),
        NearVision_RightEye: GetNearVision_RightEye(),
        NearVision_LeftEye: GetNearVision_LeftEye(),
        Right_Spherical_Status: GetRight_Spherical_Status(),
        Right_Spherical_Points: GetRight_Spherical_Points(),
        Right_Cyclinderical_Status: GetRight_Cyclinderical_Status(),
        Right_Cyclinderical_Points: GetRight_Cyclinderical_Points(),
        Right_Axix_From: GetRight_txtRightAxix(),
        Right_Near_Status: GetRightNearAdd(),
        Right_Near_Points: GetRightNearAddPoint(),
        RightSquint_VA: $('#chkSquintRight').prop('checked'),
        RightAmblyopic_VA: $('#chkAmblyopicRight').prop('checked'),
        VisualAcuity_RightEye: GetRightVisualAcuity(),
        Left_Spherical_Status: GetLeft_Spherical_Status(),
        Left_Spherical_Points: GetLeft_Spherical_Points(),
        Left_Cyclinderical_Status: GetLeft_Cyclinderical_Status(),
        Left_Cyclinderical_Points: GetLeft_Cyclinderical_Points(),
        Left_Axix_From: GetRight_txtLeftAxix(),
        Left_Near_Status: GetLeftNearAdd(),
        Left_Near_Points: GetLeftNearAddPoint(),
        VisualAcuity_LeftEye: GetLeftVisualAcuity(),
        LeftSquint_VA: $('#chkSquintLeft').prop('checked'),
        LeftAmblyopic_VA: $('#chkAmblyopicLeft').prop('checked'),


        Hirchberg_Distance: GetHirchberg_Distance(),
        Hirchberg_Near: GetHirchberg_Near(),
        Ophthalmoscope_RightEye: GetOphthalmoscope_RightEye(),
        PupillaryReactions_RightEye: GetPupillaryReactions_RightEye(),
        CoverUncovertTest_RightEye: GetCoverUncovertTest_RightEye(),
        CoverUncovertTestRemarks_RightEye: $('#txtRightOrthopticExtraOccularMuscle').val(),
        ExtraOccularMuscleRemarks_RightEye: $('#txtRightVisitExtraOccularMuscle').val().trim(),
        Ophthalmoscope_LeftEye: GetOphthalmoscope_LeftEye(),
        PupillaryReactions_LeftEye: GetPupillaryReactions_LeftEye(),
        CoverUncovertTest_LeftEye: GetCoverUncovertTest_LeftEye(),
        CoverUncovertTestRemarks_LeftEye: $('#txtLeftOrthopticExtraOccularMuscle').val(),
        ExtraOccularMuscleRemarks_LeftEye: $('#txtLeftVisitExtraOccularMuscle').val(),
        CycloplegicRefraction_RightEye: $('#chkfollowRightCycloplagic').prop('checked'),
        CycloplegicRefraction_LeftEye: $('#chkfollowLeftCycloplagic').prop('checked'),
        CycloplegicRefraction_RightEye: $('#chkfollowRightCycloplagic').prop('checked'),
        CycloplegicRefraction_LeftEye: $('#chkfollowLeftCycloplagic').prop('checked'),
        Conjunctivitis_RightEye: $('#chkfollowRightConjunctivitis').prop('checked'),
        Conjunctivitis_LeftEye: $('#chkfollowLeftConjunctivitis').prop('checked'),
        Scleritis_RightEye: $('#chkfollowRightScleritis').prop('checked'),
        Scleritis_LeftEye: $('#chkfollowLeftScleritis').prop('checked'),
        Nystagmus_LeftEye: $('#chkfollowRightNystagmus').prop('checked'),
        Nystagmus_RightEye: $('#chkfollowLeftNystagmus').prop('checked'),
        CornealDefect_RightEye: $('#chkfollowRightCorneal').prop('checked'),
        CornealDefect_LeftEye: $('#chkfollowLeftCorneal').prop('checked'),
        Cataract_RightEye: $('#chkfollowRightCataract').prop('checked'),
        Cataract_LeftEye: $('#chkfollowLeftCataract').prop('checked'),
        Keratoconus_RightEye: $('#chkfollowRightKeratoconus').prop('checked'),
        Keratoconus_LeftEye: $('#chkfollowLeftKeratoconus').prop('checked'),
        Ptosis_RightEye: $('#chkfollowRightPtosis').prop('checked'),
        Ptosis_LeftEye: $('#chkfollowLeftPtosis').prop('checked'),
        LowVision_RightEye: $('#chkfollowRightLow').prop('checked'),
        LowVision_LeftEye: $('#chkfollowLeftLow').prop('checked'),
        Pterygium_RightEye: $('#chkfollowRightPterygium').prop('checked'),
        Pterygium_LeftEye: $('#chkfollowLeftPterygium').prop('checked'),
        ColorBlindness_RightEye: $('#chkfollowRightColorBlindness').prop('checked'),
        ColorBlindness_LeftEye: $('#chkfollowLeftColorBlindness').prop('checked'),
        Others_RightEye: $('#chkfollowRightOthersDisorder').prop('checked'),
        Others_LeftEye: $('#chkfollowLeftOthersDisorder').prop('checked'),
        Fundoscopy_RightEye: $('#chkfollowRightFundoscopy').prop('checked'),
        Fundoscopy_LeftEye: $('#chkfollowLeftFundoscopy').prop('checked'),
        Surgery_RightEye: $('#chkfollowRightSurgery').prop('checked'),
        Surgery_LeftEye: $('#chkfollowLeftSurgery').prop('checked'),
        CataractSurgery_RightEye: $('#chkfollowRightCataract').prop('checked'),
        CataractSurgery_LeftEye: $('#chkfollowLeftCataract').prop('checked'),
        SurgeryPterygium_RightEye: $('#chkfollowRightPterygium').prop('checked'),
        SurgeryPterygium_RightEye: $('#chkfollowLeftPterygium').prop('checked'),
        SurgeryCornealDefect_RightEye: $('#chkfollowRightCornealDefectSurgery').prop('checked'),
        SurgeryCornealDefect_LeftEye: $('#chkfollowLeftCornealDefectSurgery').prop('checked'),
        SurgeryPtosis_RightEye: $('#chkfollowRightPtosisSurgery').prop('checked'),
        SurgeryPtosis_LeftEye: $('#chkfollowLeftPtosisSurgery').prop('checked'), 
        SurgeryKeratoconus_RightEye: $('#chkfollowRightKeratoconusSurgery').prop('checked'),
        SurgeryKeratoconus_LeftEye: $('#chkfollowLeftKeratoconusSurgery').prop('checked'),
        Chalazion_RightEye: $('#chkfollowRightChalazion').prop('checked'),
        Chalazion_LeftEye: $('#chkfollowLeftChalazion').prop('checked'),
        Hordeolum_RightEye: $('#chkfollowRightHordeolum').prop('checked'),
        Hordeolum_LeftEye: $('#chkfollowLeftHordeolum').prop('checked'),
        SurgeryOthers_RightEye: $('#chkfollowRightOthersSurgery').prop('checked'),
        SurgeryOthers_LeftEye: $('#chkfollowLeftOthersSurgery').prop('checked'),
        RightPupilDefects: $('#chkfollowRightPupil').prop('checked'),
        LeftPupilDefects: $('#chkfollowLeftPupil').prop('checked'),
        RightAmblyopia: $('#chkfollowRightAmblyopia').prop('checked'),
        LeftAmblyopia: $('#chkfollowLeftAmblyopia').prop('checked'),
        RightSquint_Surgery: $('#chkfollowRightSquintSurgery').prop('checked'),
        LeftSquint_Surgery: $('#chkfollowLeftSquintSurgery').prop('checked'),
        IPD: $('#txtIPD').val(),
        RightVisualFieldTestId: GetRightVisualFieldTest(),
        LeftVisualFieldTestId: GetLeftVisualFieldTest(),

        RightCycloplagicdrops: GetRightCycloplagicdrops(),
        RightMeridian1: GetRightMeridian1(),
        RightMeridian2: GetRightMeridian2(),
        RightAxisOfRetino: GetRightAxisOfRetino(),
        RightNoGlowVisibile: GetRightNoGlowVisibile(),
        Right_CycloDrops_Spherical_Status: GetRight_CycloDrops_Spherical_Status(),
        Right_CycloDrops_Spherical_Points: GetRight_CycloDrops_Spherical_Points(),
        Right_CycloDrops_Cyclinderical_Status: GetRight_CycloDrops_Cyclinderical_Status(),
        Right_CycloDrops_Cyclinderical_Points: GetRight_CycloDrops_Cyclinderical_Points(),
        Right_CycloDrops_Axix: GetRight_CycloDrops_Axix(),
        Right_CycloDrops_FinalPrescription: GetRight_CycloDrops_FinalPrescription(),
        LeftCycloplagicdrops: GetLeftCycloplagicdrops(),
        LeftMeridian1: GetLeftMeridian1(),
        LeftMeridian2: GetLeftMeridian2(),
        LeftAxisOfRetino: GetLeftAxisOfRetino(),
        LeftNoGlowVisibile: GetLeftNoGlowVisibile(),
        Left_CycloDrops_Spherical_Status: GetLeft_CycloDrops_Spherical_Status(),
        Left_CycloDrops_Spherical_Points: GetLeft_CycloDrops_Spherical_Points(),
        Left_CycloDrops_Cyclinderical_Status: GetLeft_CycloDrops_Cyclinderical_Status(),
        Left_CycloDrops_Cyclinderical_Points: GetLeft_CycloDrops_Cyclinderical_Points(),
        Left_CycloDrops_Axix: GetLeft_CycloDrops_Axix(),
        Left_CycloDrops_FinalPrescription: GetLeft_CycloDrops_FinalPrescription(),
        TreatmentId: GetTreatment(),
        Medicines: GetMedicines(),
        Prescription: GetPrescription(),
        NextVisit_ReferToHospital: $('#chkReferToHospital').prop("checked")
    }
    return Model;
}

function GetTreatment() {
    if (!$('#chkGlassesSuggested').prop("checked") && !$('#chkGlassesnotSuggested').prop("checked") && !$('#chkGlassesnotWilling').prop("checked")) {
        return 0
    }
    else {
        return $("input[name='chkTreatment']:checked").val()
    }
}

function GetMedicines() {
    if ($('#chkGlassesSuggested').prop("checked")) {
        return $('#txtMedicine').val()
    }
    else
        return null;
}
function GetPrescription() {
    if ($('#chkGlassesSuggested').prop("checked")) {
        return $('#txtPrescription').val()
    }
    else
        return null;
}

function GetRightCycloplagicdrops() {
    if ($('#chkRightYes').prop("checked"))
        return true;
    else
        false;
}

function GetRightMeridian1() {
    if ($('#chkRightYes').prop("checked"))
        return $('#txtRightMeridian1').val();
    else
        return null;
}

function GetRightMeridian2() {
    if ($('#chkRightYes').prop("checked"))
        return $('#txtRightMeridian2').val();
    else
        return null;
}

function GetRightAxisOfRetino() {
    if ($('#chkRightYes').prop("checked"))
        return $('#txtRightAxisRetino').val();
    else
        return null;
}

function GetRightNoGlowVisibile() {
    if ($('#chkRightYes').prop("checked"))
        return $('#txtRightNoGlowVisible').val();
    else
        return null;
}


function GetRight_CycloDrops_Spherical_Status() {
    if ($('#txtRightCycloSpherical').val() > 0)
        return $('#ddlRightCycloSpherical').val()
    else
        return "P";
}

function GetRight_CycloDrops_Spherical_Points() {
    if ($('#txtRightCycloSpherical').val() > 0)
        return $('#txtRightCycloSpherical').val();
    else
        return 0;
}

function GetRight_CycloDrops_Cyclinderical_Status() {
    if ($('#txtRightCycloCyclinderical').val() > 0)
        return $('#ddlRightCycloCyclinderical').val();
    else
        return "P";
}

function GetRight_CycloDrops_Cyclinderical_Points() {
    if ($('#txtRightCycloCyclinderical').val() > 0)
        return $('#txtRightCycloCyclinderical').val();
    else
        return 0;
}

function GetRight_CycloDrops_Axix() {
    if ($('#txtRightCycloAxix').val() > 0)
        return $('#txtRightCycloAxix').val();
    else
        return 0;
}

function GetRight_CycloDrops_FinalPrescription() {
    return $('#txtRightFinalPrescription').val();
}

function GetRightCycloplagicdrops() {
    if ($('#chkRightYes').prop("checked"))
        return true;
    else
        false;
}
function GetLeftCycloplagicdrops() {
    if ($('#chkLeftYes').prop("checked"))
        return true;
    else
        false;
}

function GetLeftMeridian1() {
    if ($('#chkLeftYes').prop("checked"))
        return $('#txtLeftMeridian1').val();
    else
        return null;
}

function GetLeftMeridian2() {
    if ($('#chkLeftYes').prop("checked"))
        return $('#txtLeftMeridian2').val();
    else
        return null;
}

function GetLeftAxisOfRetino() {
    if ($('#chkLeftYes').prop("checked"))
        return $('#txtLeftAxisRetino').val();
    else
        return null;
}

function GetLeftNoGlowVisibile() {
    if ($('#chkLeftYes').prop("checked"))
        return $('#txtLeftNoGlowVisible').val();
    else
        return null;
}

function GetLeft_CycloDrops_Spherical_Status() {
    if ($('#txtLeftCycloSpherical').val() > 0)
        return $('#ddlLeftCycloSpherical').val()
    else
        return "P";
}

function GetLeft_CycloDrops_Spherical_Points() {
    if ($('#txtLeftCycloSpherical').val() > 0)
        return $('#txtLeftCycloSpherical').val();
    else
        return 0;
}

function GetLeft_CycloDrops_Cyclinderical_Status() {
    if ($('#txtLeftCycloCyclinderical').val() > 0)
        return $('#ddlLeftCycloCyclinderical').val();
    else
        return "P";
}

function GetLeft_CycloDrops_Cyclinderical_Points() {
    if ($('#txtLeftCycloCyclinderical').val() > 0)
        return $('#txtLeftCycloCyclinderical').val();
    else
        return 0;
}

function GetLeft_CycloDrops_Axix() {
    if ($('#txtLeftCycloAxix').val() > 0)
        return $('#txtLeftCycloAxix').val();
    else
        return 0;
}

function GetLeft_CycloDrops_FinalPrescription() {
    return $('#txtLeftFinalPrescription').val();
}

function GetRightVisualFieldTest() {
    if ($("input:checkbox[name=Right-Un-aided]:checked").val() > 3 || $("input:checkbox[name=Left-Un-aided]:checked").val() > 3) {
        return $('#ddlRightVisualFieldTest').val();
    }
    else
        return -1;
}

function GetLeftVisualFieldTest() {
    if ($("input:checkbox[name=Right-Un-aided]:checked").val() > 3 || $("input:checkbox[name=Left-Un-aided]:checked").val() > 3) {
        return $('#ddlLeftVisualFieldTest').val();
    }
    else
        return -1;
}

function GetHirchberg_Distance() {
    var id = -1;
    $("input:checkbox[name=Distance-Vision]:checked").each(function () {
        id= ($(this).val());
    });
    return id;
}

function GetHirchberg_Near() {
    var id = -1;
    $("input:checkbox[name=Near-Vision]:checked").each(function () {
        id = ($(this).val());
    });
    return id;
}


function GetOphthalmoscope_RightEye() {
    var id = 0;
    $("input:checkbox[name=RightOpthalmoscope-Red-reflex-test]:checked").each(function () {
        id = ($(this).val());
    });
    return id;
}



function GetPupillaryReactions_RightEye() {
    var id = 0;
    $("input:checkbox[name=PupillaryReactions]:checked").each(function () {
        id = ($(this).val());
    });
    return id;
}


function GetCoverUncovertTest_RightEye() {
    var id = 0;
    $("input:checkbox[name=RightCoverUncoverTest]:checked").each(function () {
        id = ($(this).val());
    });
    return id;
}


function GetOphthalmoscope_LeftEye() {
    var id = 0;
    $("input:checkbox[name=LeftOpthalmoscope-Red-reflex-test]:checked").each(function () {
        id = ($(this).val());
    });
    return id;
}

 


function GetCoverUncovertTest_LeftEye() {
    var id = 0;
    $("input:checkbox[name=LeftCoverUncoverTest]:checked").each(function () {
        id = ($(this).val());
    });
    return id;
}

function GetPupillaryReactions_LeftEye() {
    var id = 0;
    $("input:checkbox[name=PupillaryReactions]:checked").each(function () {
        id = ($(this).val());
    });
    return id;
}


$('#btnMoveFromtreatment').click(function () {
    $('#step3').click();
})

$("input:checkbox[name=Right-Un-aided]").change(function () {
    if (GetDistanceVision_RightEye_Unaided() > 3 || GetDistanceVision_LeftEye_Unaided() > 3)
        ShowVisualFieldTest();
    else
        HideVisualFieldTest();
});

$("input:checkbox[name=Left-Un-aided]").change(function () {
    if (GetDistanceVision_RightEye_Unaided() > 3 || GetDistanceVision_LeftEye_Unaided() > 3)
        ShowVisualFieldTest();
    else
        HideVisualFieldTest();
});
function GetDistanceVision_RightEye_Unaided() {
    var right = 0;
    $("input:checkbox[name=Right-Un-aided]:checked").each(function () {
        right =$(this).val();
    });
    return right ;
}
function GetDistanceVision_RightWithGlasses() {
    var right = 0;
    $("input:checkbox[name=Right-With-Glasses]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

function GetDistanceVision_RightEye_PinHole() {
    var right = 0;
    $("input:checkbox[name=Right-Pin-Hole]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}


function GetDistanceVision_LeftEye_Unaided() {
    var right = 0;
    $("input:checkbox[name=Left-Un-aided]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}
function GetDistanceVision_LeftWithGlasses() {
    var right = 0;
    $("input:checkbox[name=Left-With-Glasses]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

function GetDistanceVision_LeftEye_PinHole() {
    var right = 0;
    $("input:checkbox[name=Left-Pin-Hole]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

function GetNearVision_RightEye() {
    var right = 0;
    $("input:checkbox[name=Right-N]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

function GetNearVision_LeftEye() {
    var right = 0;
    $("input:checkbox[name=Left-N]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

function GetRight_Spherical_Status() {
    if ($('#txtRightSpherical').val() > 0)
        return $('#ddlRightSpherical').val()
    else
        return "P";
}
function GetRight_Spherical_Points() {
    if ($('#txtRightSpherical').val() > 0)
        return $('#txtRightSpherical').val();
    else
        return 0;
}

function GetRight_Cyclinderical_Status() {
    if ($('#txtRightCyclinderical').val() > 0)
        return $('#ddlRightCyclinderical').val()
    else
        return "P";
}

function GetRight_Cyclinderical_Points() {
    if ($('#txtRightCyclinderical').val() > 0)
        return $('#txtRightCyclinderical').val();
    else
        return 0;
}

function GetRight_txtRightAxix() {
    if ($('#txtRightCyclinderical').val() > 0)
        return $('#txtRightAxix').val();
    else
        return 0;
}

function GetRightVisualAcuity() {
    return $('#ddlRightVisualAcuity').val()
}

function GetRightNearAdd() {
    return $('#ddlRightNearAdd').val()
}

function GetRightNearAddPoint() {
    return $('#txtRightNearAdd').val()
}




function GetLeft_Spherical_Status() {
    if ($('#txtLefSpherical').val() > 0)
        return $('#ddlLeftSpherical').val()
    else
        return "P";
}
function GetLeft_Spherical_Points() {
    if ($('#txtLeftSpherical').val() > 0)
        return $('#txtLeftSpherical').val();
    else
        return 0;
}

function GetLeft_Cyclinderical_Status() {
    if ($('#txtLeftCyclinderical').val() > 0)
        return $('#ddlLeftCyclinderical').val()
    else
        return "P";
}

function GetLeft_Cyclinderical_Points() {
    if ($('#txtLeftCyclinderical').val() > 0)
        return $('#txtLeftCyclinderical').val();
    else
        return 0;
}

function GetRight_txtLeftAxix() {
    if ($('#txtLeftCyclinderical').val() > 0)
        return $('#txtLeftAxix').val();
    else
        return 0;
}

function GetLeftVisualAcuity() {
    return $('#ddlLeftVisualAcuity').val()
}

function GetLeftNearAdd() {
    return $('#ddlLeftNearAdd').val()
}

function GetLeftNearAddPoint() {
    return $('#txtLeftNearAdd').val()
}



function SaveValidatFromAcuity() {
    var validate = true;
  

  if ((GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_LeftEye_Unaided() > 0) && (GetNearVision_RightEye() == 0 && GetNearVision_LeftEye() == 0)
        && $('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false && $('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false
    ) {
      if (!ValidateToFillSubjectiveRefraction())
          validate = false;
        // return "step4";
    }

    else if ((GetDistanceVision_RightEye_Unaided() == 0 && GetDistanceVision_LeftEye_Unaided() == 0) && (GetNearVision_RightEye() > 0 || GetNearVision_LeftEye() > 0)
        && $('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false && $('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false
    ) {
        if (!ValidateToFillSubjectiveRefraction())   validate = false; 
    }

     if (GetDistanceVision_RightEye_Unaided() == 0 && GetDistanceVision_LeftEye_Unaided() == 0 && GetNearVision_RightEye() == 0 && GetNearVision_LeftEye() == 0 && $('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false
        && ($('#chkSquintRight').prop('checked') || $('#chkSquintLeft').prop('checked'))
    ) {

        $('#chkNormal').prop('checked', true);
        if ($('#chkSquintRight').prop('checked'))
            $('#chkfollowRightSquint').prop('checked', true);
        else
            $('#chkfollowRightSquint').prop('checked', false);

        if ($('#chkSquintLeft').prop('checked'))
            $('#chkfollowLeftSquint').prop('checked', true);
        else
            $('#chkfollowLeftSquint').prop('checked', false);


        //$('#step4').click(); // Go to Diagnosis  Treatment & Follow Up Page 
        //return "step4";
    }

    if ((GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_LeftEye_Unaided() > 0) && $('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false
        && ($('#chkSquintRight').prop('checked') || $('#chkSquintLeft').prop('checked'))
    ) {

        if ($('#chkSquintRight').prop('checked'))
            $('#chkfollowRightSquintAssessment').prop('checked', true);
        else
            $('#chkfollowRightSquintAssessment').prop('checked', false);

        if ($('#chkSquintLeft').prop('checked'))
            $('#chkfollowLeftSquintAssessment').prop('checked', true);

        else
            $('#chkfollowLeftSquintAssessment').prop('checked', false);


        //$('#step2').click(); // Go to Orthoptic Assessment Page
        //return "step2";
    }

    else if ($('#chkAmblyopicRight').prop('checked') || $('#chkAmblyopicRight').prop('checked')) {
        $('#chkfollowRightFurtherAssessment').prop('checked', true);
        $('#chkfollowLeftFurtherAssessment').prop('checked', true);
        //$('#step4').click();
        //return "step4";
    }

    else if (($('#chkSquintRight').prop('checked') || $('#chkSquintLeft').prop('checked')) && ($('#chkAmblyopicRight').prop('checked') || $('#chkAmblyopicLeft').prop('checked'))) {

        if ($('#chkAmblyopicRight').prop('checked'))
            $('#chkfollowRightFurtherAssessment').prop('checked', true);
        else
            $('#chkfollowRightFurtherAssessment').prop('checked', false);

        if ($('#chkAmblyopicLeft').prop('checked'))
            $('#chkfollowLeftFurtherAssessment').prop('checked', true);
        else
            $('#chkfollowLeftFurtherAssessment').prop('checked', false);

    }
    if ($('#txtRightSpherical').val() > 0 || $('#txtLeftSpherical').val() > 0) {

        if ($('#txtRightSpherical').val() > 0) {
            $('#chkfollowRightRefractiveError').prop('checked', true);

            if ($('#ddlRightVisualAcuity').val() > 0) {
                $('#chkRightAcuityDistance').prop('checked', true);
            }
            else
                $('#chkRightAcuityDistance').prop('checked', false);


            if ($('#txtRightNearAdd').val() > 0) {
                $('#chkRightNearAdd').prop('checked', true);
            }
            else
                $('#chkRightNearAdd').prop('checked', false);
        }
        else
            $('#chkfollowRightRefractiveError').prop('checked', false);

        if ($('#txtLeftSpherical').val() > 0) {
            $('#chkfollowLeftRefractiveError').prop('checked', true);

            if ($('#ddlLeftVisualAcuity').val() > 0) {
                $('#chkLeftAcuityDistance').prop('checked', true);
            }
            else
                $('#chkLeftAcuityDistance').prop('checked', false);


            if ($('#txtLeftNearAdd').val() > 0) {
                $('#chkLeftNearAdd').prop('checked', true);
            }
            else
                $('#chkLeftNearAdd').prop('checked', false);
        }

        else
            $('#chkfollowLeftRefractiveError').prop('checked', false);
    }

    if ((!$('#chkWearGlasses').prop('checked')) && ($('#txtRightSpherical').val() > 0 || $('#txtLeftSpherical').val() > 0))
        $('#chkfollowNew').prop('checked', true);
    else
        $('#chkfollowNew').prop('checked', false);

    if ($('#chkWearGlasses').prop('checked') && ($('#txtRightSpherical').val() > 0 || $('#txtLeftSpherical').val() > 0))
        $('#chkfollowUpgrade').prop('checked', true);
    else
        $('#chkfollowUpgrade').prop('checked', false);



    // Un aided Checked for Visit Summary
    var atLeastOneCheckedPinUnaidedRight = false;
    var atLeastOneCheckedPinUnaidedLeft = false;

    $('input[name="Right-Un-aided"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedPinUnaidedRight = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });

    $('input[name="Left-Un-aided"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedPinUnaidedLeft = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });

    if (GetDistanceVision_RightEye_Unaided() > 0)
        $('#chkRightUn-aided').prop('checked', true);
    else
        $('#chkRightUn-aided').prop('checked', false);

    if (GetDistanceVision_LeftEye_Unaided() > 0)
        $('#chkLeftUn-aided').prop('checked', true);
    else
        $('#chkLeftUn-aided').prop('checked', false);



    // Glasses Checked for Visit Summary
    var atLeastOneCheckedGlassesRight = false;
    var atLeastOneCheckedGlassesLeft = false;

    $('input[name="Right-With-Glasses"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedGlassesRight = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });

    $('input[name="Left-With-Glasses"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedGlassesLeft = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });

    //if (atLeastOneCheckedGlassesRight == true)
    if (GetDistanceVision_RightWithGlasses() > 0)
        $('#chkRightGlasses').prop('checked', true);
    else
        $('#chkRightGlasses').prop('checked', false);

    //if (atLeastOneCheckedGlassesLeft == true)
    if (GetDistanceVision_LeftWithGlasses() > 0)
        $('#chkLeftGlasses').prop('checked', true);
    else
        $('#chkLeftGlasses').prop('checked', false);





    // Pin Hole Checked for visit SUmmary
    var atLeastOneCheckedPinHoleRight = false;
    var atLeastOneCheckedPinHoleLeft = false;

    $('input[name="Right-Pin-Hole"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedPinHoleRight = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });
    $('input[name="Left-Pin-Hole"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedPinHoleLeft = true;
            //return atLeastOneCheckedLefttVA; // exit the loop early
        }
    });
    //if (atLeastOneCheckedPinHoleRight == true)
    if (GetDistanceVision_RightEye_Unaided() > 0)
        $('#chkRightPinHole').prop('checked', true);
    else
        $('#chkRightPinHole').prop('checked', false);

    if (GetDistanceVision_LeftEye_Unaided() > 0)
        $('#chkLeftPinHole').prop('checked', true);
    else
        $('#chkLeftPinHole').prop('checked', false);

    return validate;

}
function GetCheckDiagnosticPolicy() {
    if (((GetDistanceVision_LeftEye_Unaided() == 0 || GetDistanceVision_LeftEye_PinHole() == 0) ||
        (GetDistanceVision_RightEye_Unaided() == 0 || GetDistanceVision_RightEye_PinHole() == 0)) &&
        ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true) &&
        ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
    ) {
        if ($('#chkSquintRight').prop('checked'))
            $('#chkfollowRightSquint').prop('checked', true);
        else
            $('#chkfollowRightSquint').prop('checked', false);

        if ($('#chkSquintLeft').prop('checked'))
            $('#chkfollowLeftSquint').prop('checked', true);

        else
            $('#chkfollowLeftSquint').prop('checked', false);

    }

    if (((GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0 || GetDistanceVision_LeftWithGlasses() > 0) ||
        (GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_RightWithGlasses() > 0)) &&
        ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true) &&
        ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
    ) {
        if ($('#chkSquintRight').prop('checked'))
            $('#chkfollowRightSquintAssessment').prop('checked', true);
        else
            $('#chkfollowRightSquintAssessment').prop('checked', false);

        if ($('#chkSquintLeft').prop('checked'))
            $('#chkfollowLeftSquintAssessment').prop('checked', true);

        else
            $('#chkfollowLeftSquintAssessment').prop('checked', false);

    }
    if (($('#chkAmblyopicRight').prop('checked') == true || $('#chkAmblyopicLeft').prop('checked') == true)
        && ($('#chkSquintRight').prop('checked') == false || $('#chkSquintLeft').prop('checked') == false)
    ) {
        if ($('#chkAmblyopicRight').prop('checked') == true)
            $('#chkfollowRightFurtherAssessment').prop('checked', true);
        else
            $('#chkfollowRightFurtherAssessment').prop('checked', false);

        if ($('#chkAmblyopicLeft').prop('checked') == true)
            $('#chkfollowLeftFurtherAssessment').prop('checked', true);
        else
            $('#chkfollowLeftFurtherAssessment').prop('checked', false);


    }
    if (($('#chkAmblyopicRight').prop('checked') == true || $('#chkAmblyopicLeft').prop('checked') == true)
        && ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true)
    ) {
        if ($('#chkAmblyopicRight').prop('checked') == true)
            $('#chkfollowRightFurtherAssessment').prop('checked', true);
        else
            $('#chkfollowRightFurtherAssessment').prop('checked', false);

        if ($('#chkAmblyopicLeft').prop('checked') == true)
            $('#chkfollowLeftFurtherAssessment').prop('checked', true);
        else
            $('#chkfollowLeftFurtherAssessment').prop('checked', false);
    }
    if (GetDistanceVision_LeftEye_Unaided() > 0) {
        $('#chkLeftUn-aided').prop('checked', true);
    }
    else {
        $('#chkLeftUn-aided').prop('checked', false);
    }
    if (GetDistanceVision_RightEye_Unaided() > 0) {
        $('#chkRightUn-aided').prop('checked', true);
    }
    else {
        $('#chkRightUn-aided').prop('checked', false);
    }
    if (GetDistanceVision_LeftEye_PinHole() > 0) {
        $('#chkLeftPinHole').prop('checked', true);
    }
    else {
        $('#chkLeftPinHole').prop('checked', false);
    }

    if (GetDistanceVision_RightEye_PinHole() > 0) {
        $('#chkRightPinHole').prop('checked', true);
    }
    else {
        $('#chkRightPinHole').prop('checked', false);
    }

    if (GetDistanceVision_LeftWithGlasses() > 0) {
        $('#chkLeftGlasses').prop('checked', true);
    }
    else {
        $('#chkLeftGlasses').prop('checked', false);
    }

    if (GetDistanceVision_RightWithGlasses() > 0) {
        $('#chkRightGlasses').prop('checked', true);
    }
    else {
        $('#chkRightGlasses').prop('checked', false);
    }

    if ($('#ddlRightVisualAcuity').val() > 0) {
        $('#chkRightAcuityDistance').prop('checked', true);
    }
    else {
        $('#chkRightAcuityDistance').prop('checked', false);
    }

    if ($('#ddlLeftVisualAcuity').val() > 0) {
        $('#chkLeftAcuityDistance').prop('checked', true);
    }
    else {
        $('#chkLeftAcuityDistance').prop('checked', false);
    }

    if ($('#txtRightNearAdd').val() || 0 > 0) {
        $('#chkRightNearAdd').prop('checked', true);
    }
    else {
        $('#chkRightNearAdd').prop('checked', false);
    }

    if ($('#txtLeftNearAdd').val() || 0 > 0) {
        $('#chkLeftNearAdd').prop('checked', true);
    }
    else {
        $('#chkLeftNearAdd').prop('checked', false);
    }
}

function GetNewUpgradeCheckbox() {
    if (((GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0 || GetDistanceVision_LeftWithGlasses() > 0) ||
        (GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_RightWithGlasses() > 0)) &&
        $('#chkWearGlasses').prop('checked') == true
    ) {
        $('#chkfollowUpgrade').prop('checked', true); 
        $('#chkfollowNew').prop('checked', false);
        $('#chkfollowWearing').prop('checked', false);
        
    }
    else {
        $('#chkfollowUpgrade').prop('checked', false);
    }
    if (($('#chkWearGlasses').prop('checked') == false) && ((GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0 || GetDistanceVision_LeftWithGlasses() > 0) ||
        (GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_RightWithGlasses() > 0))
    ) {
        $('#chkfollowNew').prop('checked', true);
        $('#chkfollowWearing').prop('checked', true);
        $('#chkfollowUpgrade').prop('checked', false);
    }
    else {
        $('#chkfollowNew').prop('checked', false);
        $('#chkfollowWearing').prop('checked', false);
    }
    if ($('#txtRightSpherical').val() || 0 > 0) {
        $('#chkfollowRightRefractiveError').prop('checked', true);
    }
    else {
        $('#chkfollowRightRefractiveError').prop('checked', false);
    }

    if ($('#txtLeftSpherical').val() || 0 > 0) {
        $('#chkfollowLeftRefractiveError').prop('checked', true);
    }
    else {
        $('#chkfollowLeftRefractiveError').prop('checked', false);
    }
}


$('#btnRefresh').click(function () {

    location.reload(true);
})

$('#btnFinishFromtreatment').click(function () {
    $('#btnSave').click();
})

$('#btnSave').click(function () {
    if (!validateNextAcuity()) {  $('#step1').click(); return; }
    if (!SaveValidatFromAcuity()) { $('#step1').click(); return; }
    if (!ValidateForVisualAcuityNear()) return;
    if ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true) {
        if (!ValidateForHirchbergTest()) { $('#step2').click(); return; }
    }
    hideValidateFillSubjectiveRefraction();

    //if (((GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0) || ((GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0)))
    //    && ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true)
    //    && ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)    ) {
    //    if (!ValidateForHirchbergTest()) { $('#step2').click(); return; }
    //}

    //else if (
    //    (GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0
    //        || GetNearVision_LeftEye() > 0 || GetNearVision_RightEye() > 0)
    //    && ($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true)
    //    && ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
    //) {
    //    if ($('#chkSquintRight').prop('checked') == true) {
    //        $('#chkfollowRightSquintAssessment').prop('checked', true);
    //    }
    //    else {
    //        $('#chkfollowRightSquintAssessment').prop('checked', false);
    //    }
    //    if ($('#chkSquintLeft').prop('checked') == true) {
    //        $('#chkfollowLeftSquintAssessment').prop('checked', true);
    //    }
    //    else {
    //        $('#chkfollowLeftSquintAssessment').prop('checked', false);
    //    }
    //    if (!ValidateForHirchbergTest()) { $('#step2').click(); return; }
    //}

    if (((GetDistanceVision_LeftEye_Unaided() > 0 || GetDistanceVision_LeftEye_PinHole() > 0) || (GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_RightEye_PinHole() > 0))
        && ($('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false) &&
        ($('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false)
        ) {
        if (!ValidateToFillSubjectiveRefraction()) { return; } else { $('#step4').click(); }
    }
    if (($('#chkSquintRight').prop('checked') == true || $('#chkSquintLeft').prop('checked') == true) &&
        ($('#chkAmblyopicRight').prop('checked') == true && $('#chkAmblyopicLeft').prop('checked') == true)) {

    }
    //else if ((GetDistanceVision_RightEye_Unaided() > 0 || GetDistanceVision_LeftEye_Unaided() > 0) || (GetNearVision_RightEye() == 0 && GetNearVision_LeftEye() == 0)
    //    || (GetDistanceVision_RightEye_PinHole() > 0 || GetDistanceVision_LeftEye_PinHole() > 0)
    //    || $('#chkAmblyopicRight').prop('checked') == false && $('#chkAmblyopicLeft').prop('checked') == false && $('#chkSquintRight').prop('checked') == false && $('#chkSquintLeft').prop('checked') == false
    //) {
    //    if (!ValidateToFillSubjectiveRefraction()) return;
    //    $('#step4').click(); // Go to Diagnosis  Treatment & Follow Up Page 
    //    // return "step4";
    //}
    hideValidateFillSubjectiveRefraction();
    GetNewUpgradeCheckbox();
    var data = GetModel()

    console.log("Final result",data);
    var Title = 'Save';
    var Content = 'Are you sure?'
    if ($('#txtOptometristResidentId').val() || 0 > 0) {
        Title = 'Update';
    }
    $.confirm({
        title: Title,
        content: Content,
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Localities/OptometristResident/SaveUpdate',
                    data: data,
                    method: 'post',
                    dataType: 'json',
                    success: function (result) {
                        //console.log(result);
                        if (result.toLowerCase().includes('successfully')) {
                            $.confirm({
                                title: result,
                                content: "Do you want to add other Resident's Optometrist Test also?",
                                buttons: {
                                    Yes: function () {
                                        ResetResidentValue();
                                        //ResetFields();
                                        UncheckedAllCheckedBoxes();
                                        GetAutoCompleteResident();
                                        $('#chkNew').prop('checked', true);
                                        $('#chkNew').change();
                                        $('#step1').click();
                                        $('#autocomplete-input-Resident').focus();

                                    },
                                    No: function () {
                                        $('#btnRefresh').click();
                                        $('#chkNew').change();
                                    }
                                }

                            })


                        }
                        else {
                            __MessageBox("Error", "Error :" + result, "red", "Ok", function () { });
                        }
                    },
                    error: function (xhr, status, error) {
                        // Handle error response
                        __MessageBox("Error", "Error :" + error, "red", "Ok", function () { });
                    }
                });
            },
            cancel: function () {
            },
        }

    })
})

function ResetResidentValue() {
    document.getElementById("autocomplete-input-Resident").value = '';
    document.getElementById("txtResidentCode").value = '';
    document.getElementById("txtResidentAutoId").value = '';

    $('#txtlastVisitDate').val('dd | MMM | yyyy');
    $('#txtLastLeftAxix').val('');
    $('#txtLastRightAxix').val('');
    $('#txtLastLeftCyclinderical').val('');
    $('#txtLastRightCyclinderical').val('');
    $('#txtLastLeftSpherical').val('');
    $('#txtLastRightSpherical').val('');
    $('#txtRightSpherical').val('');
    $('#txtAutoRefResidentId').val('');
    $('#txtResidentAutoId').val('');
    $('#txtLeftSpherical').val('');
    $('#txtLastIPD').val('');
    $('#txtRightCyclinderical').val('');
    $('#txtLeftCyclinderical').val('');
    $('#txtRightAxix').val('');
    $('#txtLeftAxix').val('');
    $('#txtAge').val('');
    $('#txtGender').val('');

    $('#ddlRightSpherical').val('P');
    $('#txtRightSpherical').val('');
    $('#ddlRightCyclinderical').val('P');
    $('#txtRightCyclinderical').val('');
    $('#txtRightAxix').val('');
    $('#ddlRightVisualAcuity').val('-1');
    $('#ddlRightNearAdd').val('P');
    $('#txtRightNearAdd').val('');
    $('#ddlLeftSpherical').val('P');
    $('#txtLeftSpherical').val('');
    $('#ddlLeftCyclinderical').val('P');
    $('#txtLeftCyclinderical').val('');
    $('#txtLeftAxix').val('');
    $('#ddlLeftVisualAcuity').val('-1');
    $('#ddlLeftNearAdd').val('P');
    $('#txtLeftNearAdd').val('');
    $('#txtAge').val('');
    $('#txtGender').val('');
    
    $('#chkWearGlasses').prop('checked', false);
    $('#chkDistance').prop('checked', false);
    $('#chkNear').prop('checked', false);

    $('#txtLeftCyclinderical').change();
    $('#txtRightCyclinderical').change();
    if ($('#autocomplete-input-Locality').val() != '') {
        setTimeout(function () {
            $('#autocomplete-input-Resident').focus();
        }, 500);
    }
    else {
        $('#autocomplete-input-Locality').focus();
    }
    $('#ddlRightVisualFieldTest').val(-1)
    $('#ddlLeftVisualFieldTest').val(-1)
    $('#txtIPD').val('')
    $('#chkRightNo').prop('checked', true);
    $('#chkRightYes').prop('checked', false);
    $('#chkLeftNo').prop('checked', true);
    $('#chkLeftYes').prop('checked', false);
    $('#txtRightMeridian1').val('')
    $('#txtRightMeridian2').val('')
    $('#txtRightAxisRetino').val('')
    $('#txtRightNoGlowVisible').val('')
    $('#ddlRightCycloSpherical').val('P')
    $('#txtRightCycloSpherical').val('')
    $('#ddlRightCycloCyclinderical').val('P')
    $('#txtRightCycloCyclinderical').val('')
    $('#txtRightCycloAxix').val('')
    $('#txtRightFinalPrescription').val('')
    $('#txtLeftMeridian1').val('')
    $('#txtLeftMeridian2').val('')
    $('#txtLeftAxisRetino').val('')
    $('#txtLeftNoGlowVisible').val('')
    $('#ddlLeftCycloSpherical').val('P')
    $('#txtLeftCycloSpherical').val('')
    $('#ddlLeftCycloCyclinderical').val('P')
    $('#txtLeftCycloCyclinderical').val('')
    $('#txtLeftCycloAxix').val('')
    $('#txtLeftFinalPrescription').val('')
    $('#txtMedicine').val('')
    $('#txtPrescription').val('')
    $('#chkGlassesSuggested').prop("checked", false)
    $('#chkGlassesnotSuggested').prop("checked", false)
    $('#chkGlassesnotWilling').prop("checked", false)
    $('#chkReferToHospital').prop("checked", false)
    $('[name="chkRightDrop"]').change();
    $('[name="chkLeftDrop"]').change();

}

function GetResidentInfo(Id) {
    $.ajax({
        url: '/Localities/LocalityResidentEnrollment/GetById/' + Id,
        method: 'get',
        dataType: 'json',
        success: function (result) {
            //console.log(result);
            SetValuesResident(result);
        }
    })
}


function SetValuesResident(object) {
    $('#txtAge').val(object.age);
    //$('#txtEnrollementDate').val(object.enrollementDate.substr(0, 10));
    setTimeout(function () {
        $('#chkWearGlasses').prop("checked", object.wearGlasses);
        $('#chkDistance').prop("checked", object.distance);
        $('#chkNear').prop("checked", object.near);
    }, 500)
    if (object.genderAutoId == 1)
        $('#txtGender').val('Male')
    else
        $('#txtGender').val('Female')
}

function HideVisualFieldTest() {
    $('#MainVisualFieldTest').hide();
    $('#borderDiv').hide();
}

function ShowVisualFieldTest() {
    $('#MainVisualFieldTest').show();
    $('#borderDiv').show();
}

function GetRightVisualFieldTestValue() {
    return $('#ddlRightVisualFieldTest').val();
}

function GetLeftVisualFieldTestValue() {
    return $('#ddlLeftVisualFieldTest').val();
}
$('#btnDelete').click(function () {
    if ($('#ddlGetOptometristResidentById').val() != '' && $('#ddlGetOptometristResidentById').val() != 'select') {
        $.confirm({
            title: 'Delete',
            content: 'Are you sure',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: '/Localities/OptometristResident/DeleteById/' + $('#ddlGetOptometristResidentById').val(),
                        method: 'post',
                        dataType: 'json',
                        success: function (result) {
                            //console.log(result);
                            if (result.toLowerCase().includes('successfully')) {
                                $.alert(result, "Deleted");
                                $('#chkNew').prop("checked", true);
                                $('#chkNew').change();


                            }
                            else {
                                __MessageBox("Error", "Error :" + result, "red", "Ok", function () { });
                            }
                        },
                        error: function (xhr, status, error) {
                            // Handle error response
                            __MessageBox("Error", "Error :" + error, "red", "Ok", function () { });
                        }
                    });
                },
                cancel: function () {
                },
            }

        })
    }
    else {
        $.alert("No Data to delete.", "Alert");
    }
})


$('#chkCompanies').on('change', function () {

    if ($('#chkCompanies').is(":checked")) {
        UncheckedOther('chkCompanies');
        window.location.assign(window.location.origin + "/Factory/OptometristWokrer/Add/0");
    }
})
$('#chkGoth').on('change', function () {

    if ($('#chkGoth').is(":checked")) {
        UncheckedOther('chkGoth');
        window.location.assign(window.location.origin + "/Goths/GothOptometristResident/Add/0");
    }
})
$('#chkLocalities').on('change', function () {

    if ($('#chkLocalities').is(":checked")) {
        UncheckedOther('chkLocalities');
        window.location.assign(window.location.origin + "/Localities/OptometristResident/Add/0");
    }
})
$('#chkPublicSpaces').on('change', function () {

    if ($('#chkPublicSpaces').is(":checked")) {
        UncheckedOther('chkPublicSpaces');
        window.location.assign(window.location.origin + "/PublicSpaces/OptometristResident/Add/0");
    }
})