var populateDtt = '/PublicSpaces/AutoRefTestResident/GetHistoryById/' + $('#txtResidentAutoId').val() + '; Auto Refraction Test History';


$(document).ready(function () {

    if (!window.location.href.includes("list")) {
 

        GetDate('txtVisitDate');
        CustomDate('txtVisitDate');

        GetAutoCompletePublicSpaces();
        if ($('#txtLoginId').val() != null && $('#txtLoginId').val() != "")
            console.log("Session value: ", $('#txtLoginId').val());
        if ($('#txtResidentAutoId').val() != "0") {
            GetModal();

        }
        $('#chkNew').prop('checked', true);
        $('#chkNew').change();
        $('#autocomplete-input-PublicSpaces').change();
        $('#autocomplete-input-PublicSpaces').focus();

        AllowOnlyFour('txtRightSpherical');
        GetDecimalInput('txtRightSpherical');
        AllowNumberOnly('txtRightSpherical')

        AllowOnlyFour('txtRightCyclinderical');
        GetDecimalInput('txtRightCyclinderical');
        AllowNumberOnly('txtRightCyclinderical')

        GetDecimalInput('txtLeftSpherical');
        AllowOnlyFour('txtLeftSpherical');
        AllowNumberOnly('txtLeftSpherical')

        GetDecimalInput('txtLeftCyclinderical');
        AllowOnlyFour('txtLeftCyclinderical');
        AllowNumberOnly('txtLeftCyclinderical')
        OneCheckBoxChecked()
        CheckboxChanged();
        DisabledCheckedUncheckedChecbox('chkWearGlasses');
        DisabledCheckedUncheckedChecbox('chkDistance');
        DisabledCheckedUncheckedChecbox('chkNear');
        $('#chkPublicSpaces').prop('checked', true);
    }

})

function OneCheckBoxChecked() {
    OnCheckBoxChecked('Right-Un-aided');
    OnCheckBoxChecked('Left-Un-aided');
    OnCheckBoxChecked('Right-N');
    OnCheckBoxChecked('Left-N');
    OnCheckBoxChecked('Comments');
    OnCheckBoxChecked('NotSatisfied');
    OnCheckBoxChecked('Treatment');
    /*OnCheckBoxChecked('NextVisit');*/
}
function GetVisitDate() {
    var now = new Date($('#txtVisitDate').val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    return FinalDate;
}

$('#txtVisitDate').change(function () {
    DateChange('txtVisitDate');
    GetAutoCompleteResident();
})


$('#chkNew').on('change', function () {
    if ($('#chkNew').is(':checked')) {
        $('#chkDisplay').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        $('#btnDelete').prop('disabled', true);
        ResetResidentValue();
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtOptometristResidentId').val('')
        $('#btnSave').text('Save');
        $('#btnDelete').prop('disabled',true)
        if ($('#txtPublicSpacesCode').val() != "" && $('#autocomplete-input-PublicSpaces').val() != "")
            GetAutoCompleteResident();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
    }
    GetAutoCompletePublicSpaces();
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
        $('#btnDelete').prop('disabled', false)
    }
    GetAutoCompletePublicSpaces();
    GetDates();

});

$('#chkEdit').on('change', function () {
    if ($('#chkEdit').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkDisplay').prop('checked', false);
        $('#btnDelete').prop('disabled', false)
        GetAutoCompleteResident()
        $('#btnSave').text('Update');
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
    }
    GetAutoCompletePublicSpaces();
    GetDates();
});


function GetModal() {
    $.ajax({
        url: '/PublicSpaces/GlassDispenseResident/GetLastOptoById',
        method: 'get',
        data: GetResidentModel(),
        dataType: 'json',
        success: function (result) {
            if (result != null) {
                console.log(result);
                SetValues(result);
            }
        }
    })
}
function GetResidentModel() {
    var Model = {
        ResidentAutoId: $('#txtResidentAutoId').val(),
        GlassDispenseResidentTransDate: GetVisitDate()
    }
    return Model;
}


function SetValues(object) {
    SetWearGlasses(object.wearGlasses);
    SetDistance(object.distance);
    SetNear(object.near);
    $('#chkWearGlasses').change();
    $('#txtAge').val(object.age);
    //$('#txtEnrollementDate').val(object.enrollementDate.substr(0, 10));
    $('#txtGender').val(object.gender)
    $('#txtLastLeftAxix').val(object.left_Axix_From);
    $('#txtLastRightAxix').val(object.right_Axix_From);
    $('#txtLastLeftCyclinderical').val(object.left_Cyclinderical_Points);
    $('#txtLastRightCyclinderical').val(object.right_Cyclinderical_Points);
    $('#txtLastLeftSpherical').val(object.left_Spherical_Points);
    $('#txtLastRightSpherical').val(object.right_Spherical_Points);
    $('#txtLastIPD').val(object.ipd);
    $('#txtOptometristResidentId').val(object.optometristPublicSpacesResidentId)
}
function SetWearGlasses(value) {
     $('#chkWearGlasses').prop('checked', value);
}
function SetDistance(value) {
    $('#chkDistance').prop('checked', value);
}

function SetNear(value) {
    $('#chkNear').prop('checked', value);
}

$("#autocomplete-input-PublicSpaces").keydown(function (e) {
    var keyCode = e.keyCode || e.which;
    //Regex for Valid Characters i.e. Alphabets and Numbers.
    var regex = /^[A-Za-z0-9]+$/;
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 39 || e.keyCode == 40) {
        return;
    }
    if (isValid) {
        GetAutoCompletePublicSpaces();
    }
})
function GetPublicSpacesText() {

    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        SearchText: $("#autocomplete-input-PublicSpaces").val()
    }
    return Model;
}
function GetAutoCompletePublicSpaces() {
    var Type = true;
    if ($('#chkEdit').is(':checked'))
        Type = false;
    PublicSpaces = [];
    $.ajax({
        url: '/DropDownLookUp/Help/GetPublicSpacesForGlassDispense',
        data: GetPublicSpacesText(),
        method: 'get',
        dataType: 'json',
        success: function (result) {
            PublicSpaces = result;
        }
    })

    $("#autocomplete-input-PublicSpaces").autocomplete({
        source: function (request, response) {
            var term = request.term.toLowerCase();
            // Filter the item list based on the search term
            var filteredList = $.grep(PublicSpaces, function (item) {
                //console.log(PublicSpaces);
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
                document.getElementById("autocomplete-input-PublicSpaces").value = selectedName;
                document.getElementById("txtPublicSpacesCode").value = code;
                document.getElementById("txtPublicSpacesAutoId").value = selectedId;
                ResetResidentValue()
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

function ResetPublicSpacesValue() {
    document.getElementById("autocomplete-input-PublicSpaces").value = '';
    document.getElementById("txtPublicSpacesCode").value = '';
    document.getElementById("txtPublicSpacesAutoId").value = '';
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

function GeResidentText() {
    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        AutoId: $('#txtPublicSpacesAutoId').val(),
        SearchText: $("#autocomplete-input-Resident").val(),
        TransDate: GetVisitDate()
    }
    return Model;
}
function GetAutoCompleteResident() {
    ResetFields();
    ResetAutoCompleteResident();
    if ($('#txtPublicSpacesAutoId').val() > 0) {
        $.ajax({
            url: '/DropDownLookUp/Help/GetPublicSpacesResidentsForGlassDispense',
            data: GeResidentText(),
            method: 'get',
            dataType: 'json',
            success: function (result) {
                Residents = result;
            }
        })

        $("#autocomplete-input-Resident").focus();
        $("#autocomplete-input-Resident").autocomplete({
            source: function (request, response) {
                var term = request.term.toLowerCase();
                // Filter the item list based on the search term
                var filteredList = $.grep(Residents, function (item) {
                    //console.log(PublicSpaces);
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

                setTimeout(function () {
                    document.getElementById("autocomplete-input-Resident").value = selectedName.substr(0, selectedName.length - 11);
                    document.getElementById("txtResidentCode").value = code;
                    document.getElementById("txtResidentAutoId").value = selectedId;
                    GetModal();
                    GetDates();

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

function ResetAutoCompleteResident() {
    $('#txtResidentCode').val('')
    $('#txtResidentAutoId').val('')
    $('#txtAge').val('')
    $('#txtGender').val('')
    $('#chkWearGlasses').prop('checked', false)
    $('#chkDistance').prop('checked', false)
    $('#chkNear').prop('checked', false)
    $('#txtLastRightSpherical').val('')
    $('#txtLastLeftSpherical').val('')
    $('#txtLastRightCyclinderical').val('')
    $('#txtLastLeftCyclinderical').val('')
    $('#txtLastRightAxix').val('')
    $('#txtLastLeftAxix').val('')
    $('#txtLastIPD').val('');
    $('#txtUnsatisfiedRemarks').val('');
    $('#ddlGetGlassDispenseId').val('');
    $('#txtPublicSpacesGlassDispenseResidentId').val('');
}
function ResetResidentValue() {
    document.getElementById("autocomplete-input-Resident").value = '';
    document.getElementById("txtResidentCode").value = '';
    document.getElementById("txtResidentAutoId").value = '';
    $('#txtLastLeftAxix').val('');
    $('#txtLastRightAxix').val('');
    $('#txtLastLeftCyclinderical').val('');
    $('#txtLastRightCyclinderical').val('');
    $('#txtLastLeftSpherical').val('');
    $('#txtLastRightSpherical').val('');
    $('#txtLastIPD').val('');
    $('#txtOptometristResidentId').val('')
    $('#txtAge').val('');
    $('#txtGender').val(''); 
    $('#chkWearGlasses').prop('checked', false);
    $('#chkDistance').prop('checked', false);
    $('#chkNear').prop('checked', false);

    if ($('#autocomplete-input-PublicSpaces').val() != '') {
        setTimeout(function () {
            $('#autocomplete-input-Resident').focus();
        }, 500);
    }
    else {
        $('#autocomplete-input-PublicSpaces').focus();
    }
    $('input[name="Right-Un-aided"]').prop('checked', false);
    $('input[name="Left-Un-aided"]').prop('checked', false);
    $('input[name="Right-N"]').prop('checked', false);
    $('input[name="Left-N"]').prop('checked', false);
    $('input[name="Comments"]').prop('checked', false);
    $('input[name="NotSatisfied"]').prop('checked', false);
    $('#txtUnsatisfiedRemarks').val('');
    
}

$('#chkNotSatisfied').change(function () {
    CheckboxDisabled($(this).is(":checked"))
})

$('#chkSatisfied').change(function () {
    CheckboxDisabled(!$(this).is(":checked"))
})
function CheckboxDisabled(value) {
    if (value)
        value = false;
    else
        value = true;
    $('input[name="NotSatisfied"]').prop('disabled', value);
}

function GetDates() {
    if (($('#chkDisplay').is(':checked')) || $('#chkEdit').is(':checked')) {
        $.ajax({
            url: '/PublicSpaces/GlassDispenseResident/GetDatesofGlassDispenseResident/' + $('#txtResidentAutoId').val(),
            method: 'get',
            type: 'json',
            async: false,
            success: function (result) {
                console.log(result);
                $('#ddlGetGlassDispenseId').empty();
                $("#ddlGetGlassDispenseId").append($("<option></option>").val('').html(' Date'));
                $('#txtPublicSpacesGlassDispenseResidentId').val('');
                $.each(result, function (data, value) {
                    $("#ddlGetGlassDispenseId").append($("<option></option>").val(value.code).html(value.text));
                })
                $('#divDisplay').show();
                $('#ddlGetGlassDispenseId').show()
                $('#ddlGetGlassDispenseId').focus();
            }
        })
        setTimeout(function () {
            var total_tems = $('#ddlGetGlassDispenseId').find('option').length;
            if (total_tems == 2) {
                $('#ddlGetGlassDispenseId option:nth-child(2)').attr('selected', true);
                $('#ddlGetGlassDispenseId').change()
            }

        }, 500)
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
    }
    else {
        $('#divDisplay').hide();
        $('#ddlGetGlassDispenseId').hide()
    }
    if ($('#chkDisplay').is(':checked')) {
        $('#btnSave').hide();
    }
    else
        $('#btnSave').show();

    if ($('#chkDisplay').is(':checked') || $('#chkEdit').is(':checked'))
        if ($('#autocomplete-input-PublicSpaces').val() == "" || $('#autocomplete-input-Resident').val() == "") {
            $('#divDisplay').hide();
            $('#ddlGetGlassDispenseId').hide();
        }

}

$('#ddlGetGlassDispenseId').change(function () {
    $('#txtPublicSpacesGlassDispenseResidentId').val($('#ddlGetGlassDispenseId').val());
    if ($('#ddlGetGlassDispenseId').val() > 0 && $('#txtResidentAutoId').val()>'0') {
        $.ajax({
            url: '/PublicSpaces/GlassDispenseResident/GetGlassDispenseById/' + $('#txtPublicSpacesGlassDispenseResidentId').val(),
            method: 'get',
            dataType: 'json',
            success: function (result) {
                ResetFields();
                console.log("final ", result);
                SetModel(result);
            }
        })

    }
})
function validate() {
    var returnVal = true;


    if ($('#txtVisitDate').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtVisitDate", "show", "Date is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtVisitDate", "hide", "", "bottom")
    }

    if ($('#txtPublicSpacesCode').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtPublicSpacesCode", "show", "Mandatory", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtPublicSpacesCode", "hide", "", "bottom")
    }
     

    if ($('#autocomplete-input-PublicSpaces').val() == '') {
        returnVal = false;
        AddVAlidationToControl("autocomplete-input-PublicSpaces", "show", "Mandatory", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("autocomplete-input-PublicSpaces", "hide", "", "bottom")
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
    var atLeastOneCheckedLeftUnaided = false;
    $('input[name="Left-Un-aided"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedLeftUnaided = true;
            //return atLeastOneCheckedRightUnaided; // exit the loop early
        }
    });
    if (atLeastOneCheckedLeftUnaided == false) {
        atLeastOneCheckedLeftUnaided = false;
        AddVAlidationToControl("Left-Un-aided1", "show", "Mandatory", "top")
        var scrollTop = window.pageYOffset;
        window.scrollTo(0, scrollTop - 100);
        returnVal = false;
    }
    else {
        AddVAlidationToControl("Left-Un-aided1", "hide", "", "top")
    }



    var atLeastOneCheckedRightN = false;
    $('input[name="Right-N"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedRightN = true;
            //return atLeastOneCheckedRightUnaided; // exit the loop early
        }
    });
    if (atLeastOneCheckedRightN == false) {
        atLeastOneCheckedRightN = false;
        AddVAlidationToControl("Right-N-6", "show", "Mandatory", "top")
        var scrollTop = window.pageYOffset;
        window.scrollTo(0, scrollTop - 100);
        returnVal = false;
    }
    else {
        AddVAlidationToControl("Right-N-6", "hide", "", "top")
    }
    var atLeastOneCheckedLeftN = false;
    $('input[name="Left-N"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedLeftN = true;
            //return atLeastOneCheckedRightUnaided; // exit the loop early
        }
    });
    if (atLeastOneCheckedLeftN == false) {
        atLeastOneCheckedLeftN = false;
        AddVAlidationToControl("Left-N-6", "show", "Mandatory", "top")
        var scrollTop = window.pageYOffset;
        window.scrollTo(0, scrollTop - 100);
        returnVal = false;
    }
    else {
        AddVAlidationToControl("Left-N-6", "hide", "", "top")
    }

    if ($('#chkNotSatisfied').is(":checked")) {
        var atLeastOneCheckedNotSatisfied = false;
        $('input[name="NotSatisfied"]').each(function () {
            if ($(this).prop('checked')) {
                atLeastOneCheckedNotSatisfied = true;
                //return atLeastOneCheckedRightUnaided; // exit the loop early
            }
        });
        if (atLeastOneCheckedNotSatisfied == false) {
            atLeastOneCheckedNotSatisfied = false;
            AddVAlidationToControl("chkBlurVision", "show", "Mandatory", "top")
            var scrollTop = window.pageYOffset;
            window.scrollTo(0, scrollTop - 100);
            returnVal = false;
        }
        else {
            AddVAlidationToControl("chkBlurVision", "hide", "", "top")
        }
    }

    if ($('#chkOthers').is(":checked") && $('#txtUnsatisfiedRemarks').val() =="")  {
        AddVAlidationToControl("txtUnsatisfiedRemarks", "show", "Mandatory", "top")
        returnVal = false;
    }
    else {
        AddVAlidationToControl("txtUnsatisfiedRemarks", "hide", "", "top")
    }
    if (!$('#chkNotSatisfied').is(":checked") && !$('#chkSatisfied').is(":checked")) {
        AddVAlidationToControl("chkSatisfied", "show", "Mandatory", "top") 
        returnVal = false;
    }
    else {
        AddVAlidationToControl("chkSatisfied", "hide", "", "top") 
    }
    return returnVal;
}

$('#btnSave').click(function () {
    if (!validate()) return;
    var Model = GetModel();
    //console.log(Model);
    var Title = 'Save';
    var Content = 'Are you sure?'
    if ($('#txtPublicSpacesGlassDispenseResidentId').val() || 0 > 0) {
        Title = 'Update';
    }
    $.confirm({
        title: Title,
        content: Content,
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/PublicSpaces/GlassDispenseResident/SaveUpdate',
                    data: Model,
                    method: 'post',
                    dataType: 'json',
                    success: function (result) {
                        //console.log(result);
                        if (result.toLowerCase().includes('successfully')) {
                            $.confirm({
                                title: result,
                                content: "Do you want to add other Resident's Glass Dispense?",
                                buttons: {
                                    Yes: function () {
                                        $('#chkNew').prop('checked', true);
                                        $('#chkNew').change();
                                        ResetResidentValue();
                                        ResetFields();
                                        GetAutoCompleteResident();

                                    },
                                    No: function () {
                                        $('#btnRefresh').click();
                                        ResetFields();
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

function GetModel() {
    var Model = {
        PublicSpacesGlassDispenseResidentId: $('#txtPublicSpacesGlassDispenseResidentId').val(),
        GlassDispenseResidentTransDate: GetVisitDate(),
        ResidentAutoId: $('#txtResidentAutoId').val(),
        VisionwithGlasses_RightEye: GetDistanceVision_RightEye_Unaided(),
        VisionwithGlasses_LeftEye: GetDistanceVision_LeftEye_Unaided(),
        NearVA_RightEye: GetNearVA_RightEye(),
        NearVA_LeftEye: GetNearVA_LeftEye(),
        ResidentSatisficaion: GetSatisfied(),
        Unsatisfied: GetUnSatisfied(),
        Unsatisfied_Reason:GetUnSatisfiedReason(),
        Unsatisfied_Remarks: GetUnSatisfiedRemarks(),
        OptometristPublicSpacesResidentId: $('#txtOptometristResidentId').val()
    }
    return Model;
}
 
function GetSatisfied() {
    if ($('#chkSatisfied').is(":checked")) {
        return 1;
    }
    else {
        return 0;
    }
}
function GetUnSatisfied() {
    if ($('#chkNotSatisfied').is(":checked")) {
        return 1;
    }
    else {
        return 0;
    }
}

function GetUnSatisfiedRemarks() {
    if ($('#chkNotSatisfied').is(":checked")) {
        return $('#txtUnsatisfiedRemarks').val();
    }
    return '';
}
function GetDistanceVision_RightEye_Unaided() {
    var right = -1;
    $("input:checkbox[name=Right-Un-aided]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

function GetDistanceVision_LeftEye_Unaided() {
    var right = -1;
    $("input:checkbox[name=Left-Un-aided]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}
function GetUnSatisfiedReason() {
    var right = -1;
    $("input:checkbox[name=NotSatisfied]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

function GetDistanceVision_RightEye_Unaided() {
    var right = -1;
    $("input:checkbox[name=Right-Un-aided]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

function GetNearVA_RightEye() {
    var right = -1;
    $("input:checkbox[name=Right-N]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

function GetNearVA_LeftEye() {
    var right = -1;
    $("input:checkbox[name=Left-N]:checked").each(function () {
        right = $(this).val();
    });
    return right;
}

//function GetTreatment() {
//    var right = -1;
//    $("input:checkbox[name=Treatment]:checked").each(function () {
//        right = $(this).val();
//    });
//    return right;
//}
//function GetMedicines() {
//    if ($('#chkSuggested').is(":checked")) {
//        return $('#txtsuggestedMedicine').val();
//    }
//    else if ($('#chknotSuggested').is(":checked")) {
//        return $('#txtnotsuggestedMedicine').val();
//    }
//    else if ($('#chknotwilling').is(":checked")) {
//        return $('#txtnotwillingMedicine').val();
//    }
//}

//function GetProvideGlass() {
//    if ($('#chkProvideGlasses').is(":checked")) {
//        return 1;
//    }
//    else {
//        return 0;
//    }
//}

//function GetReferToHospital() {
//    if ($('#chkReferToHospital').is(":checked")) {
//        return 1;
//    }
//    else {
//        return 0;
//    }
//} 
//function GetPrescription() {
//    if ($('#chkSuggested').is(":checked")) {
//        return $('#txtsuggestedPrescription').val();
//    }
//    else if ($('#chknotSuggested').is(":checked")) {
//        return $('#txtnotsuggestedPrescription').val();
//    }
//    else if ($('#chknotwilling').is(":checked")) {
//        return $('#txtnotwillingPrescription').val();
//    }
//}

function CheckboxChanged() { 
    $('input[name = "NotSatisfied"]').change(function () {
        if ($('#chkOthers').is(":checked")) {
            $('#txtUnsatisfiedRemarks').prop('readonly', false)
            $('#txtUnsatisfiedRemarks').focus();
        }
        else {
            $('#txtUnsatisfiedRemarks').prop('readonly', true)
        }
    })
    $('input[name = "Comments"]').change(function () {
        if ($('#chkSatisfied').is(":checked")) {
            $('#chkBlurVision').prop("checked", false);
            $('#chkHeadache').prop("checked", false);
            $('#chkPseudo').prop("checked", false);
            $('#chkOthers').prop("checked", false);
            CheckboxChanged();
        }
    })
    $('input[name = "Treatment"]').change(function () {
        if ($('#chkSuggested').is(":checked")) {
            $('#txtsuggestedMedicine').prop('readonly', false)
            $('#txtsuggestedPrescription').prop('readonly', false)

            $('#txtnotsuggestedMedicine').prop('readonly', true)
            $('#txtnotsuggestedPrescription').prop('readonly', true)
            $('#txtnotwillingMedicine').prop('readonly', true)
            $('#txtnotwillingPrescription').prop('readonly', true)

        }
        else if ($('#chknotSuggested').is(":checked")) {
            $('#txtnotsuggestedMedicine').prop('readonly', false)
            $('#txtnotsuggestedPrescription').prop('readonly', false)
            $('#txtsuggestedMedicine').prop('readonly', true)
            $('#txtsuggestedPrescription').prop('readonly', true)
            $('#txtnotwillingMedicine').prop('readonly', true)
            $('#txtnotwillingPrescription').prop('readonly', true)

        }
        else if ($('#chknotwilling').is(":checked")) {
            $('#txtnotsuggestedMedicine').prop('readonly', true)
            $('#txtnotsuggestedPrescription').prop('readonly', true)
            $('#txtsuggestedMedicine').prop('readonly', true)
            $('#txtsuggestedPrescription').prop('readonly', true)
            $('#txtnotwillingMedicine').prop('readonly', false)
            $('#txtnotwillingPrescription').prop('readonly', false)

        }
        else {
            $('#txtnotsuggestedMedicine').prop('readonly', true)
            $('#txtnotsuggestedPrescription').prop('readonly', true)
            $('#txtsuggestedMedicine').prop('readonly', true)
            $('#txtsuggestedPrescription').prop('readonly', true)
            $('#txtnotwillingMedicine').prop('readonly', true)
            $('#txtnotwillingPrescription').prop('readonly', true)
        }
    })
}


function ResetFields() {
    $('#txtPublicSpacesGlassDispenseResidentId').val('');
    $('#txtOptometristResidentId').val(''); 
    $("input[name='Right-Un-aided']:checkbox").prop('checked', false);
    $("input[name='Left-Un-aided']:checkbox").prop('checked', false);
    $("input[name='Right-N']:checkbox").prop('checked', false);
    $("input[name='Left-N']:checkbox").prop('checked', false);
    $("input[name='Comments']:checkbox").prop('checked', false);
    $("input[name='NotSatisfied']:checkbox").prop('checked', false);
    $("input[name='Treatment']:checkbox").prop('checked', false);
    $("input[name='NextVisit']:checkbox").prop('checked', false);
    $('#txtUnsatisfiedRemarks').val('')
    $('#chkSatisfied').change();
    CheckboxChanged();
    
}

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
function SetModel(data) {
    console.log("Final Object ", data);
    if (data["right_Spherical_Points"].toString().includes('+'))
        data["right_Spherical_Points"].toString() = data["right_Spherical_Points"].toString().replace('+', '');

    if (data["left_Spherical_Points"].toString().includes('+'))
        data["left_Spherical_Points"].toString() = data["left_Spherical_Points"].toString().replace('+', '');

    if (data["left_Axix_From"].toString().includes('+'))
        data["left_Axix_From"].toString() = data["left_Axix_From"].toString().replace('+', '');

    if (data["right_Axix_From"].toString().includes('+'))
        data["right_Axix_From"].toString() = data["right_Axix_From"].toString().replace('+', '');

    if (data["left_Cyclinderical_Points"].toString().includes('+'))
        data["left_Cyclinderical_Points"].toString() = data["left_Cyclinderical_Points"].toString().replace('+', '');

    if (data["right_Cyclinderical_Points"].toString().includes('+'))
        data["right_Cyclinderical_Points"].toString() = data["right_Cyclinderical_Points"].toString().replace('+', '');


    $('#txtRightSpherical').val(data["right_Spherical_Points"]);
    $('#txtPublicSpacesGlassDispenseResidentId').val(data["publicSpacesGlassDispenseResidentId"]);
    //$('#txtVisitDate').val($('#ddlGetAutoRefId').text());
    $('#txtResidentAutoId').val(data["residentAutoId"]);
    $('#txtLeftSpherical').val(data["left_Spherical_Points"]);
    $('#txtRightCyclinderical').val(data["right_Cyclinderical_Points"]);
    $('#txtLeftCyclinderical').val(data["left_Cyclinderical_Points"]);
    $('#txtIPD').val(data["ipd"]);
    $('#txtRightCyclinderical').change()
    $('#txtLeftCyclinderical').change()
    setTimeout(function () {
        if ($('#txtRightCyclinderical').val() != 0)
            $('#txtRightAxix').val(data["right_Axix_From"]);
        if ($('#txtLeftCyclinderical').val() != 0)
            $('#txtLeftAxix').val(data["left_Axix_From"]);
    }, 500);

    $('#ddlRightSpherical').val(data["right_Spherical_Status"]);
    $('#ddlLeftSpherical').val(data["left_Spherical_Status"]);
    $('#ddlRightCyclinderical').val(data["right_Cyclinderical_Status"]);
    $('#ddlLeftCyclinderical').val(data["left_Cyclinderical_Status"]);
    $('input[type="checkbox"][name=Right-Un-aided][value="' + data["visionwithGlasses_RightEye"] + '"]').prop('checked', true);
    $('input[type="checkbox"][name=Left-Un-aided][value="' + data["visionwithGlasses_LeftEye"] + '"]').prop('checked', true);

    $('input[type="checkbox"][name=Right-N][value="' + data["nearVA_RightEye"] + '"]').prop('checked', true);
    $('input[type="checkbox"][name=Left-N][value="' + data["nearVA_LeftEye"] + '"]').prop('checked', true);
    
    if (data["residentSatisficaion"] > 0)
        $('#chkSatisfied').prop('checked', true)
    else
        $('#chkSatisfied').prop('checked', false)
    
    if (data["unsatisfied"] > 0) {
        $('#chkNotSatisfied').prop('checked', true)
        $('#chkNotSatisfied').change()
    }
    else
        $('#chkNotSatisfied').prop('checked', false)

    if (data["unsatisfied_Reason"] > 0)
        $('input[type="checkbox"][name=NotSatisfied][value="' + data["unsatisfied_Reason"] + '"]').prop('checked', true);
    else
        $("input[name='NotSatisfied']:checkbox").prop('checked', false);

    if (data["unsatisfied_Remarks"] != "" && data["unsatisfied_Remarks"] != null) {
        $('#txtUnsatisfiedRemarks').val(data["unsatisfied_Remarks"])
        $('#txtUnsatisfiedRemarks').prop('readonly', false);
    }
    if (data["treatmentId"] > 0) {
        $('input[type="checkbox"][name=Treatment][value="' + data["treatmentId"] + '"]').prop('checked', true);
    }
    if (data["treatmentId"] == 1) {
        $('#txtsuggestedMedicine').val(data["medicines"])
        $('#txtsuggestedPrescription').val(data["prescription"])
    }
    else if (data["treatmentId"] == 2) {
        $('#txtnotsuggestedMedicine').val(data["medicines"])
        $('#txtnotsuggestedPrescription').val(data["prescription"])
    }

    else if (data["treatmentId"] == 3) {
        $('#txtnotwillingMedicine').val(data["medicines"])
        $('#txtnotwillingPrescription').val(data["prescription"])
    }
    if (data["provideGlasses"] == 1)
        $('#chkProvideGlasses').prop('checked', true)
    else
        $('#chkProvideGlasses').prop('checked', false)

    if (data["referToHospital"] == 1)
        $('#chkReferToHospital').prop('checked', true)
    else
        $('#chkReferToHospital').prop('checked', false)
    
    CheckboxChanged();
    $('#btnSave').text('Update');
}

$('#btnDelete').click(function () {
    if ($('#txtPublicSpacesGlassDispenseResidentId').val() != '' && $('#txtPublicSpacesGlassDispenseResidentId').val() != '0') {
        $.confirm({
            title: 'Delete',
            content: 'Are you sure',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: '/PublicSpaces/GlassDispenseResident/DeleteById/' + $('#txtPublicSpacesGlassDispenseResidentId').val(),
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
        window.location.assign(window.location.origin + "/Factory/GlassDispenseWorker/Add/0");
    }
})
$('#chkGoth').on('change', function () {

    if ($('#chkGoth').is(":checked")) {
        UncheckedOther('chkGoth');
        window.location.assign(window.location.origin + "/Goths/GlassDispenseResident/Add/0");
    }
})
$('#chkLocalities').on('change', function () {

    if ($('#chkLocalities').is(":checked")) {
        UncheckedOther('chkLocalities');
        window.location.assign(window.location.origin + "/Localities/GlassDispenseResident/Add/0");
    }
})
$('#chkPublicSpaces').on('change', function () {

    if ($('#chkPublicSpaces').is(":checked")) {
        UncheckedOther('chkPublicSpaces');
        window.location.assign(window.location.origin + "/PublicSpaces/GlassDispenseResident/Add/0");
    }
})