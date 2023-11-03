var populateDtt = '/PublicSpaces/PublicSpacesAutoRefTestResident/GetHistoryById/' + $('#txtResidentAutoId').val() + '; Auto Refraction Test History';


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

        GetAutoCompletePublicSpaces();
        if ($('#txtLoginId').val() != null && $('#txtLoginId').val() != "")
            console.log("Session value: ", $('#txtLoginId').val());
        if ($('#txtResidentAutoId').val() != "0") {
            GetModal($('#txtResidentAutoId').val());

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
        ReadOnlyCheckBoxes();
        $('#chkPublicSpaces').prop('checked', true);
    }

})
function ReadOnlyCheckBoxes() {

    DisabledCheckedUncheckedChecbox('chkWearGlasses');
    DisabledCheckedUncheckedChecbox('chkDistance');
    DisabledCheckedUncheckedChecbox('chkNear');
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
        $('#txtAutoRefResidentId').val('')
        $('#btnSave').text('Save');
        if ($('#txtPublicSpacesCode').val() != "" && $('#autocomplete-input-PublicSpaces').val() != "")
            GetAutoCompleteResident();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtAutoRefResidentId').val('')
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
        $('#txtAutoRefResidentId').val('')
    }
    GetAutoCompletePublicSpaces();
    GetDates();

});

$('#chkEdit').on('change', function () {
    if ($('#chkEdit').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkDisplay').prop('checked', false);
        $('#btnDelete').prop('disabled', false);
        GetAutoCompleteResident()
        $('#btnSave').text('Update');
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#txtAutoRefResidentId').val('')
    }
    GetAutoCompletePublicSpaces();
    GetDates();
});
function GetModal(Id) { 
    $.ajax({
        url: '/PublicSpaces/PublicSpacesAutoRefTestResident/GetById/' + Id,
        method: 'get',
        dataType: 'json',
        success: function (result) {
            console.log("Obj ",result);
            SetValues(result);
        }
    })
}

function SetValues(object) {
    SetWearGlasses(object.wearGlasses);
    SetNear(object.near);
    SetDistance(object.distance);
    $('#chkWearGlasses').change();
    $('#txtResidentAutoId').val(object.residentAutoId)
    $('#txtResidentCode').val(object.residentCode)
    $('#ddlPublicSpaces').val(object.publicSpacesCode).trigger('change');
    $('#txtPublicSpacesCode').val(object.publicSpacesCode)
    $('#txtPublicSpacesAutoId').val(object.publicSpacesAutoId);
    $('#ddlGender').val(object.genderAutoId);
    $('#ddlGender').change();
    $('#txtAge').val(object.age);
    $('#ddlRelationType').val(object.relationType);
    $('#txtRelationName').val(object.relationName);
    $('#txtPublicSpacesName').val(object.publicSpacesName);
    $('#txtResidentName').val(object.residentName);
    $('#txtCell').val(object.mobileNo)
    $('#autocomplete-input-Resident').val(object.residentName)
    $('#txtCNIC').val(object.cnic)
    SetDate('txtlastVisitDate', object.enrollementDate)
    //$('#txtEnrollementDate').val(object.enrollementDate.substr(0, 10));
    if (object.genderAutoId == 1)
        $('#txtGender').val('Male')
    else
        $('#txtGender').val('Female')
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

$('#btnSave').click(function () {
    if (!validate()) return;
    var Model = GetModel();
    //console.log(Model);
    var Title = 'Save';
    var Content = 'Are you sure?'
    if ($('#txtAutoRefResidentId').val() || 0 > 0) {
        Title = 'Update';
    }
    $.confirm({
        title: Title,
        content: Content,
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/PublicSpaces/PublicSpacesAutoRefTestResident/SaveUpdate',
                    data: Model,
                    method: 'post',
                    dataType: 'json',
                    success: function (result) {
                        //console.log(result);
                        if (result.toLowerCase().includes('successfully')) {
                            $.confirm({
                                title: result,
                                content: "Do you want to add other Resident's Refraction Test also?",
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
                                        //$('#divDisplay').hide();
                                        //$('#ddlGetAutoRefId').hide()
                                        //$('#DivlblVisitDate').show();
                                        //$('#DivinputVisitDate').show();

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

function GetModel() {
    //var now = new Date($('#txtVisitDate').val());
    //var day = ("0" + now.getDate()).slice(-2);
    //var month = ("0" + (now.getMonth() + 1)).slice(-2);
    //var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    var Model = {
        AutoRefResidentId: $('#txtAutoRefResidentId').val(),
        AutoRefResidentTransId: '',
        //AutoRefResidentTransDate: $('#txtVisitDate').val(),
        AutoRefResidentTransDate: GetVisitDate(),
        ResidentAutoId: $('#txtResidentAutoId').val(),
        Right_Spherical_Points: $('#txtRightSpherical').val(),
        Right_Cyclinderical_Points: $('#txtRightCyclinderical').val(),
        Right_Axix_From: $('#txtRightAxix').val(),
        Left_Spherical_Points: $('#txtLeftSpherical').val(),
        Left_Cyclinderical_Points: $('#txtLeftCyclinderical').val(),
        Left_Axix_From: $('#txtLeftAxix').val(),
        IPD: $('#txtIPD').val(),
        Right_Spherical_Status: GetRight_Spherical_Status(),
        Right_Cyclinderical_Status: GetRight_Cyclinderical_Status(),
        Left_Spherical_Status: GetLeft_Spherical_Status(),
        Left_Cyclinderical_Status: GetLeft_Cyclinderical_Status()

    }
    return Model;
}
function GetRight_Spherical_Status() {
    return $('#ddlRightSpherical').val();
    //if ($('#txtRightSpherical').val() < 0)
    //    return "N";
    //else
    //    return "P";
}

function GetRight_Cyclinderical_Status() {
    return $('#ddlRightCyclinderical').val();
    //if ($('#txtRightCyclinderical').val() < 0)
    //    return "N";
    //else
    //    return "P";
}

function GetLeft_Spherical_Status() {
    return $('#ddlLeftSpherical').val();
    //if ($('#txtLeftSpherical').val() < 0)
    //    return "N";
    //else
    //    return "P";
}

function GetLeft_Cyclinderical_Status() {
    return $('#ddlLeftCyclinderical').val();
    //if ($('#txtLeftCyclinderical').val() < 0)
    //    return "N";
    //else
    //    return "P";
}
function validate() {
    var returnVal = true;

    if ($('#txtIPD').val() == "") {
        returnVal = false;
        AddVAlidationToControl("txtIPD", "show", "Mandatory", "top")
    }
    else {
        AddVAlidationToControl("txtIPD", "hide", "", "bottom")
    }

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

    if ($('#txtRightSpherical').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtRightSpherical", "show", "Mandatory", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtRightSpherical", "hide", "", "bottom")
    }

    if ($('#txtLeftSpherical').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtLeftSpherical", "show", "Mandatory", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtLeftSpherical", "hide", "", "bottom")
    }
    if (($('#txtRightCyclinderical').val().trim() != '' && $('#txtRightCyclinderical').val().trim() != '0') && $('#txtRightAxix').val().trim() == "") {
        returnVal = false;
        AddVAlidationToControl("txtRightAxix", "show", "Mandatory", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtRightAxix", "hide", "", "bottom")
    }

    if (($('#txtLeftCyclinderical').val().trim() != '' && $('#txtLeftCyclinderical').val().trim() != '0') && $('#txtLeftAxix').val().trim() == "") {
        returnVal = false;
        AddVAlidationToControl("txtLeftAxix", "show", "Mandatory", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtLeftAxix", "hide", "", "bottom")
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

    if ($('#txtResidentAutoId').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtResidentAutoId", "show", "Mandatory", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtResidentAutoId", "hide", "", "bottom")
    }
 
    return returnVal;

}

function onActionButtonClicked(actionName, data) {
    console.log(actionName);
    console.log(data);
    switch (actionName.toLowerCase()) {
        case 'edit':
            $(window).scrollTop(0);
            SetModel(data);
            break;
    }
}
function SetModel(data) {
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
    $('#txtAutoRefResidentId').val(data["autoRefResidentId"]);
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
    $('#btnSave').text('Update');
}
$('#btnBack').click(function () {
    window.location = "/PublicSpaces/PublicSpacesAutoRefTestResident/list";
})

$('#btnRefresh').click(function () {
    window.location.reload(true);
})
function onLookupItemSelected(lookupName, data) {
    console.log(lookupName);
    //console.log(data);
    let id = data[2];
    let title = data[3];
    console.log(title);
    switch (lookupName) {
        case 'PublicSpaces':
            $('#txtPublicSpacesCode').val(id);
            $('#lookup-PublicSpaces-text').val(title);
            break;
        case 'helpDepartment':
            lookupId('helpDepartment', id);
            lookupText('helpDepartment', title);
            break;
        case 'helpLocation':
            lookupId('helpLocation', id);
            lookupText('helpLocation', title);
            break
        case 'helpBatch':
            lookupId('helpBatch', id);
            lookupText('helpBatch', title);
            $('#lookup-button-helpMfgDate').attr('data-url', "/LookupHelp/Help/MfgDate/" + $('#txthidBatchNo').val() + '/' + selectedItemID + '/' + lookupId('helpLocation') + '/' + lookupId('helpDepartment'));
            $('#txtQuantity').val('1');
            /*console.log(data);*/
            $('#txtStock').val(data[6]); //Stock
            $('#txtRate').val(parseFloat(data[7]));
            $('#txtMfgDate').val(_convertDate(data[4]));
            $('#txtExpDate').val(_convertDate(data[5]));
            $('#txtAmount').val($('#txtRate').val() * $('#txtStock').val());
            break;
        case 'helpMrNo':
            lookupId('helpMrNo', data[1]);
            lookupText('helpMrNo', data[2]);
            $('#PatientID').val(data[3]);
            $('#PatientTitle').val(data[4]);
            break;
        case 'helpPatient':
            $('#txtMrNo').val(data[2]);
            $('#txtMrNo').val(data[2]);
            $('#PatientTitle').val(data[4]);
            $('#PatientID').val(data[1]);

            break;
        case 'GetSurgeryCategoryRate':
            $('#txtMainSurgeryID').val(data[1]);
            $('#lookup-GetSurgeryCategoryRate-text').val(data[2]);
            $('#txtItemConsumable').val(data[3]);
            $('#txtEffectiveDate').val(data[4].substr(0, 10));

            var GetEFDate = { mainSurgery: data[1], SubSurgeryID: 0 };
            $('#txtMainSurgeryID').change();
            $('#txtSubSurgeryCategoryTitle').val('');
            $('#txtSubSurgeryID').val('');
            $('#txtSurgeryRate').val('');
            $('#txtAmountSurgeryRate').val('');
            break;
        case 'GetSubSurgeryCategoryRates':
            /*console.log($('#txtMainSurgeryID').val());*/
            $('#txtSubSurgeryCategoryTitle').val(data[2]);
            $('#txtSubSurgeryID').val(data[1]);
            GetRate();
            $('#ddlItem').val(0).trigger('change');
            //var GetEFDate = { mainSurgery: $('#txtMainSurgeryID').val(), SubSurgeryID: $('#txtSubSurgeryID').val() };
            //bindDataToLookup("GetEFDateRate", { i: GetEFDate });
            break;
        //case 'GetEFDateRate':
        //    $('#txtEffectiveDate').val(data[1]);
        //    GetRate();
        //    //console.log(data);
        //    break;
    }
    //registerLookupButton("btnSubSurgery", "GetSubSurgeryCategoryRates", "/LookupHelp/Help/GetSubSurgeryCategoryRates/GetSubSurgeryCategoryRates");
    //registerLookupButton("btnEFDate", "GetEFDateRate", "/LookupHelp/Help/GetEFDateRate/GetEFDateRate");
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
        url: '/DropDownLookUp/Help/GetPublicSpacesForAutoRef',
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
                //console.log(companies);
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
                document.getElementById("txtPublicSpacesId").value = selectedId;
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
    document.getElementById("txtPublicSpacesId").value = '';
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
    //var now = new Date($('#txtVisitDate').val());
    //var day = ("0" + now.getDate()).slice(-2);
    //var month = ("0" + (now.getMonth() + 1)).slice(-2);
    //var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        AutoId: $('#txtPublicSpacesId').val(),
        SearchText: $("#autocomplete-input-Resident").val(),
        TransDate: GetVisitDate()
    }
    return Model;
}
function GetAutoCompleteResident() {
    /*ResetResidentValue();*/
    if ($('#txtPublicSpacesId').val() > 0) {
        Residents=[]
        $.ajax({
            // url: '/DropDownLookUp/Help/GetResidents/' + $('#txtPublicSpacesId').val() + "/" + Type + "/" + FinalDate,
            url: '/DropDownLookUp/Help/GetResidentForAutoRefPublicSpaces',
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
                    //console.log(companies);
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
                console.log("Selected ID: " + selectedId);
                console.log("Selected Name: " + selectedName);

                setTimeout(function () {
                    document.getElementById("autocomplete-input-Resident").value = selectedName.substr(0, selectedName.length - 11);
                    document.getElementById("txtResidentCode").value = code;
                    document.getElementById("txtResidentAutoId").value = selectedId;
                    GetModal($('#txtResidentAutoId').val());
                    GetResidentLastHistory();
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
function ResetResidentValue() {
    document.getElementById("autocomplete-input-Resident").value = '';
    document.getElementById("txtResidentAutoId").value = '';
    document.getElementById("txtResidentCode").value = '';

    $('#txtlastVisitDate').val('dd | MMM | yyyy');
    $('#txtLastLeftAxix').val('');
    $('#txtLastRightAxix').val('');
    $('#txtLastLeftCyclinderical').val('');
    $('#txtLastRightCyclinderical').val('');
    $('#txtLastLeftSpherical').val('');
    $('#txtLastRightSpherical').val('');
    $('#txtLastIPD').val('');
    $('#txtRightSpherical').val('');
    $('#txtAutoRefResidentId').val('');
    $('#txtResidentAutoId').val('');
    $('#txtLeftSpherical').val('');
    $('#txtRightCyclinderical').val('');
    $('#txtLeftCyclinderical').val('');
    $('#txtIPD').val('');
    $('#txtRightAxix').val('');
    $('#txtLeftAxix').val('');
    $('#txtAge').val('');
    $('#txtGender').val('');
    $('#chkWearGlasses').prop('checked', false);
    $('#chkDistance').prop('checked', false);
    $('#chkNear').prop('checked', false);
    $('#ddlRightSpherical').val('P');
    $('#ddlLeftSpherical').val('P');
    $('#ddlRightCyclinderical').val('P');
    $('#ddlLeftCyclinderical').val('P');

    $('#txtLeftCyclinderical').change();
    $('#txtRightCyclinderical').change();
    if ($('#autocomplete-input-PublicSpaces').val() != '') {
        setTimeout(function () {
            $('#autocomplete-input-Resident').focus();
        }, 500);
    }
    else {
        $('#autocomplete-input-PublicSpaces').focus();
    }

}
function GetResidentLastHistory() {
    if ($('#txtResidentAutoId').val() || 0 > 0) {
        $.ajax({
            url: '/PublicSpaces/PublicSpacesAutoRefTestResident/GetLastHistoryById/' + $('#txtResidentAutoId').val(),
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
    if (data != null) {
        $('#txtlastVisitDate').val(data["lastVisitDate"]);
        $('#txtLastLeftAxix').val(data["left_Axix_From"]);
        $('#txtLastRightAxix').val(data["right_Axix_From"]);
        $('#txtLastLeftCyclinderical').val(data["left_Cyclinderical_Points"]);
        $('#txtLastRightCyclinderical').val(data["right_Cyclinderical_Points"]);
        $('#txtLastLeftSpherical').val(data["left_Spherical_Points"]);
        $('#txtLastRightSpherical').val(data["right_Spherical_Points"]);
        $('#txtLastIPD').val(data["ipd"]);
        console.log(data["right_Spherical_Points"]);
    }
}


function GetDates() {
    if (($('#chkDisplay').is(':checked')) || $('#chkEdit').is(':checked')) {
        $.ajax({
            url: '/PublicSpaces/PublicSpacesAutoRefTestResident/GetDatesofResident/' + $('#txtResidentAutoId').val(),
            method: 'get',
            type: 'json',
            async: false,
            success: function (result) {
                console.log(result);
                $('#ddlGetAutoRefId').empty();
                $("#ddlGetAutoRefId").append($("<option></option>").val('').html(' Date'));
                $('#txtAutoRefResidentId').val('');
                $.each(result, function (data, value) {
                    $("#ddlGetAutoRefId").append($("<option></option>").val(value.code).html(value.text));
                })
                $('#divDisplay').show();
                $('#ddlGetAutoRefId').show()
                $('#ddlGetAutoRefId').focus();
            }
        })
        setTimeout(function () {
            var total_tems = $('#ddlGetAutoRefId').find('option').length;
            if (total_tems == 2) {
                $('#ddlGetAutoRefId option:nth-child(2)').attr('selected', true);
                $('#ddlGetAutoRefId').change()
            }

        }, 500)
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
    }
    else {
        $('#divDisplay').hide();
        $('#ddlGetAutoRefId').hide()
    }
    if ($('#chkDisplay').is(':checked')) {
        $('#btnSave').hide();
    }
    else
        $('#btnSave').show();

    if ($('#chkDisplay').is(':checked') || $('#chkEdit').is(':checked'))
        if ($('#autocomplete-input-PublicSpaces').val() == "" || $('#autocomplete-input-Resident').val() == "") {
            $('#divDisplay').hide();
            $('#ddlGetAutoRefId').hide();
        }

}

$('#ddlGetAutoRefId').change(function () {
    $('#txtAutoRefResidentId').val($('#ddlGetAutoRefId').val());
    if ($('#txtAutoRefResidentId').val() > 0) {
        $.ajax({
            url: '/PublicSpaces/PublicSpacesAutoRefTestResident/GetAutoRefById/' + $('#txtAutoRefResidentId').val(),
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

function ResetFields() {
    $('#ddlRightSpherical').val('P');
    $('#ddlLeftSpherical').val('P');
    $('#ddlRightCyclinderical').val('P');
    $('#ddlLeftCyclinderical').val('P');
    $('#txtAutoRefResidentId').val('');
}

$('#btnDelete').click(function () {
    if ($('#ddlGetAutoRefId').val() != '' && $('#ddlGetAutoRefId').val() != 'select') {
        $.confirm({
            title: 'Delete',
            content: 'Are you sure',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: '/PublicSpaces/PublicSpacesAutoRefTestResident/DeleteById/' + $('#ddlGetAutoRefId').val(),
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
        window.location.assign(window.location.origin + "/Factory/AutoRefTestWorker/Add/0");
    }
})
$('#chkGoth').on('change', function () {

    if ($('#chkGoth').is(":checked")) {
        UncheckedOther('chkGoth');
        window.location.assign(window.location.origin + "/Goths/GothAutoRefTestResident/Add/0");
    }
})
$('#chkLocalities').on('change', function () {

    if ($('#chkLocalities').is(":checked")) {
        UncheckedOther('chkLocalities');
        window.location.assign(window.location.origin + "/Localities/AutoRefTestResident/Add/0");
    }
})
$('#chkPublicSpaces').on('change', function () {

    if ($('#chkPublicSpaces').is(":checked")) {
        UncheckedOther('chkPublicSpaces');
        window.location.assign(window.location.origin + "/PublicSpaces/PublicSpacesAutoRefTestResident/Add/0");
    }
})