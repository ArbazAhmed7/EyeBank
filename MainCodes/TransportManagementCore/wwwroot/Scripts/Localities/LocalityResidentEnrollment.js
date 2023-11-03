
var imagePath = '~/img/noimage.png';
$(document).ready(function () {

    if (!window.location.href.includes("list")) {
        CustomDate('txtEnrollementDate');

        if ($('#txtResidentAutoId').val() || 0 <= 0)
            GetDate('txtEnrollementDate');
        CheckGlasses();
        GetGenderSDW();
        //GetLocality();
        if ($('#txtLoginId').val() != null && $('#txtLoginId').val() != "")
            console.log("Session value: ", $('#txtLoginId').val());
        if ($('#txtResidentAutoId').val() != "0") {
            //GetModal($('#txtResidentAutoId').val());
        }
        $('#chkNew').prop('checked', true);
        $('#chkNew').change(); 
        $('#chkLocalities').prop('checked', true);
        $('#txtLocalityName').focus();


    }
    CheckWebCam();
    //$(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
    //    $(this).closest(".select2-container").siblings('select:enabled').select2('open');
    //});

})
 

$('#chkNew').on('change', function () {
    if ($('#chkNew').is(':checked')) {
        $('#chkDisplay').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        $('#btnDelete').prop('disabled', true);
        //ResetResidentValue();
        ResetResidentInfo();
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#btnSave').show();
        $('#btnSave').text('Save');
        GetAutoCompleteLocality();
        $("#txtLocalityName").attr("placeholder", "Search Locality name by pressing ↓ key");
        $("#txtName").attr("placeholder", "");
        ResetResidentAutoComplete();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        ResetResidentInfo();
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
    }
    GetAutoCompleteLocality();
    //GetDates();
});

$('#chkDisplay').on('change', function () {
    if ($('#chkDisplay').is(':checked')) {
        ResetResidentInfo();
        $('#chkNew').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        $('#btnDelete').prop('disabled', false);
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
        $('#btnSave').hide();
        GetAutoCompleteLocality()
        $("#txtLocalityName").attr("placeholder", "Search Locality name by pressing ↓ key");
        $("#txtName").attr("placeholder", "Search Resident name by pressing ↓ key");
        if ($('#txtLocalityAutoId').val() != "" && $('#txtLocalityName').val() != "")
            GetAutoCompleteResident();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        ResetResidentInfo();
    }
    GetAutoCompleteLocality();
    //GetDates();

});

$('#chkEdit').on('change', function () {
    if ($('#chkEdit').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkDisplay').prop('checked', false);
        $('#btnDelete').prop('disabled', false);
        ResetResidentInfo();
        $('#btnSave').show();
        $('#btnSave').text('Update');
        GetAutoCompleteLocality();
        $("#txtLocalityName").attr("placeholder", "Search Locality name by pressing ↓ key");
        $("#txtName").attr("placeholder", "Search Resident name by pressing ↓ key");
        if ($('#txtLocalityAutoId').val() != "" && $('#txtLocalityName').val() != "")
            GetAutoCompleteResident();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
    }
    GetAutoCompleteLocality();
    // GetDates();
});




function GetModal(Id) {
    $.ajax({
        url: '/Localities/LocalityResidentEnrollment/GetById/' + Id,
        method: 'get',
        dataType: 'json',
        success: function (result) {
            console.log(result);
            SetValues(result)
            CreateTableForSavedImages(result.imageList);
        }
    })
}
function GetEnrollmentDate() {
    var now = new Date($('#txtEnrollementDate').val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    return FinalDate;
}


$('#btnSave').click(function () {
    console.log(GetModelImage());
    if (!validate()) return;

    var Model = {
        EnrollementDate: GetEnrollmentDate(),
        LocalityAutoId: $('#txtLocalityAutoId').val(),
        LocalityCode: $('#txtLocalityCode').val(),
        ResidentAutoId: $('#txtResidentAutoId').val() || 0,
        ResidentName: $('#txtName').val(),
        ResidentCode: $('#txtNameCode').val(),
        RelationType: $('#ddlRelationType').val(),
        RelationName: $('#txtRelationName').val(),
        GenderAutoId: GetGender(),
        CNIC: $('#txtCNIC').val(),
        Age: $('#txtAge').val(),
        MobileNo: $('#txtCell').val(),
        WearGlasses: WearGlasses(),
        Distance: Distance(),
        Near: Near(),
        DecreasedVision: DecreasedVision(),
        Religion: GetReligon(),
        ImageList: GetModelImage(),
    }
    console.log("Model", Model);
    var title = "Save", content = "Do you want to save?";
    if ($('#txtResidentAutoId').val() > 0) {
        title = "Update";
        content = "Do you want to update?";
    }
    //var response = _ConfirmAjaxRequest(title, content, "/Localities/LocalityResidentEnrollment/SaveUpdate", Model, 'json', 'post', null, 'tblDetail');

    $.confirm({
        title: title,
        content: content,
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Localities/LocalityResidentEnrollment/SaveUpdate",
                    data: Model,
                    method: 'post',
                    dataType: 'json',
                    success: function (result) {
                        console.log(result);
                        if (result.toLowerCase().includes('successfully')) {
                            $.confirm({
                                title: result,
                                content: "Do you want to enroll more Resident's also?",
                                buttons: {
                                    Yes: function () {
                                        ResetResidentInfo();
                                        $('#chkNew').prop('checked', true);
                                        $('#chkNew').change();
                                        $('#tblDetail').empty();
                                        setTimeout(function () {
                                            CheckWebCam();
                                        }, 1000);
                                        console.log(response);
                                    },
                                    No: function () {
                                        setTimeout(function () {
                                            CheckWebCam();
                                        }, 1000);
                                        console.log(response);
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




function GetModelImage() {
    var ImageList = [];
    $("#tbl_LocalityInfo tr:gt(0)").each(function () {
        this_row = $(this);
        var Object = {
            ResidentAutoId: $('#txtResidentAutoId').val() || 0,
            FileName: $.trim(this_row.find('td:eq(1)').html()),//td:eq(0) means first td of this row
            FileSize: $.trim(this_row.find('td:eq(2)').html()),
            FileType: $.trim(this_row.find('td:eq(3)').html()),
            ResidentPicture: $.trim(this_row.find('td:eq(5)').html()),
            IsSaved: $.trim(this_row.find('td:eq(6)').html()),
            CaptureRemarks: $.trim(this_row.find('td:eq(7)').html()),
            CaptureDate: GetEnrollmentDate()

        }
        ImageList.push(Object);
    });
    return ImageList;
}

function SetValues(object) {
    SetWearGlasses(object.wearGlasses);
    $('#chkWearGlasses').change();
    $('#txtResidentAutoId').val(object.residentAutoId)
    $('#txtNameCode').val(object.residentCode)
    /*$('#ddlLocality').val(object.LocalityCode).trigger('change');*/
    $('#txtLocalityCode').val(object.localityCode)
    $('#txtLocalityAutoId').val(object.localityAutoId);
    $('#ddlGender').val(object.genderAutoId);
    $('#ddlGender').change();
    $('#txtAge').val(object.age);
    $('#ddlRelationType').val(object.relationType);
    $('#txtRelationName').val(object.relationName);
    SetDecreasedVision(object.decreasedVision);
    SetDistance(object.distance);
    SetNear(object.near);
    SetDecreasedVision(object.decreasedVision);
    SetReligon(object.religion);
    $('#txtCell').val(object.mobileNo)
    $('#txtName').val(object.residentName)
    $('#txtCNIC').val(object.cnic);
    SetDate('txtEnrollementDate', object.enrollementDate)

    //$('#txtEnrollementDate').val(object.enrollementDate.substr(0, 10));
    //$('#txtEnrollementDateCustom').val(object.viewDate)
}
$('#txtEnrollementDate').change(function () {
    DateChange('txtEnrollementDate');
})
function ResetResidentInfo() {
    $('#chkWearGlasses').prop('checked', false);
    $('#chkDistance').prop('checked', false);
    $('#chkNear').prop('checked', false);
    $('#chkDecreasedVision').prop('checked', false);
    $('#chkNonMuslim').prop('checked', false);
    $('#chkMuslim').prop('checked', false);
    $('#txtCell').val('');
    $('#txtName').val('');
    $('#txtNameCode').val('');
    $('#txtResidentAutoId').val('')
    $('#txtResidentCode').val('')
    $('#txtCNIC').val('');
    $('#ddlGender').val('1');
    $('#ddlRelationType').val('');
    $('#txtRelationName').val('');
    $('#txtAge').val('');
    $('#txtCaptureRemarks').val('');
    /*$('#ddlLocality').change();*/
    $('#chkWearGlasses').change();
    $('#ddlGender').change();
    $("#tblDetail").empty();

    if ($('#txtLocalityName').val() != "") {
        setTimeout(function () {
            $('#txtName').focus();
        }, 300)
    }
    $('#txtLocalityName').focus();
}



function onActionButtonClicked(actionName, data) {
    /*console.log(actionName);*/
    switch (actionName.toLowerCase()) {
        case 'edit':
            window.location = "/Localities/LocalityResidentEnrollment/Add/" + data[0];
            break;
        case 'delete':
            _ConfirmAjaxRequest('Delete', 'Do you want to Delete?', "/Localities/LocalityResidentEnrollment/Delete/" + data[0], null, 'json', 'Delete', '/Localities/LocalityResidentEnrollment/GetLocalityList');
            break;

    }
}

function GetGender() {
    return $('#ddlGender').val();
}

function WearGlasses() {
    return $('#chkWearGlasses').is(':checked');
}
function Distance() {
    return $('#chkDistance').is(':checked');
}
function Near() {
    return $('#chkNear').is(':checked');
}
function DecreasedVision() {
    return $('#chkDecreasedVision').is(':checked');
}

function GetReligon() {
    if ($('#chkMuslim').is(':checked'))
        return true;
    else
        return false;
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
function SetDecreasedVision(value) {
        $('#chkDecreasedVision').prop('checked', value);
}

function SetReligon(value) {
    if (value == true)
        $('#chkMuslim').prop('checked', true);
    else {
        $('#chkNonMuslim').prop('checked', true);
        $('#chkMuslim').prop('checked', false);
    }

}




$("#txtLocalityName").keydown(function (e) {
    var keyCode = e.keyCode || e.which;
    //Regex for Valid Characters i.e. Alphabets and Numbers.
    var regex = /^[A-Za-z0-9]+$/;
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 39 || e.keyCode == 40) {
        return;
    }
    if (isValid) {
        GetAutoCompleteLocality();
    }
})


$("#txtName").keydown(function (w) {
    var keyCode = w.keyCode || w.which;
    //Regex for Valid Characters i.e. Alphabets and Numbers.
    var regex = /^[A-Za-z0-9]+$/;
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (w.keyCode == 37 || w.keyCode == 38 || w.keyCode == 39 || w.keyCode == 40) {
        return;
    }
    if (isValid && !$('#chkNew').is(':checked')) {
        GetAutoCompleteResident();
    }
})

function GetLocalityText() {

    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        SearchText: $("#txtLocalityName").val()
    }
    return Model;
}



function GetAutoCompleteLocality() {

    companies = [];
    $.ajax({
        url: '/DropDownLookUp/Help/GetLocalities',
        data: GetLocalityText(),
        method: 'get',
        dataType: 'json',
        success: function (result) {
            companies = result;
        }
    })

    $("#txtLocalityName").autocomplete({
        source: function (request, response) {
            var term = request.term.toLowerCase();
            // Filter the item list based on the search term
            var filteredList = $.grep(companies, function (item) {
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
                document.getElementById("txtLocalityName").value = selectedName;
                document.getElementById("txtLocalityCode").value = code;
                document.getElementById("txtLocalityAutoId").value = selectedId;
                // GetLocalityModal($('#txtLocalityAutoId').val());


            }, 50);
            setTimeout(function () {
                $('#txtName').focus();
                GetAutoCompleteResident();
            }, 500);
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
function GetAutoCompleteResident() {

    if ($('#chkNew').is(':checked') == true) return;
    if ($('#txtLocalityAutoId').val() > 0) {
        $.ajax({
            // url: '/DropDownLookUp/Help/GetResidents/' + $('#txtLocalityId').val() + "/" + Type + "/" + FinalDate,
            url: '/DropDownLookUp/Help/GetResidentsForLocality',
            data: GeResidentText(),
            method: 'get',
            dataType: 'json',
            success: function (result) {
                Residents = result;
            }
        })

        $("#txtName").autocomplete({
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
                //console.log("Selected ID: " + selectedId);
                //console.log("Selected Name: " + selectedName);

                setTimeout(function () {
                    ResetResidentInfo();
                    document.getElementById("txtName").value = selectedName.substr(0, selectedName.length - 11);;
                    document.getElementById("txtNameCode").value = code;
                    document.getElementById("txtResidentAutoId").value = selectedId;
                    GetModal($('#txtResidentAutoId').val());


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

function GeResidentText() {
    //var now = new Date($('#txtEnrollementDate').val());
    //var day = ("0" + now.getDate()).slice(-2);
    //var month = ("0" + (now.getMonth() + 1)).slice(-2);
    //var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        AutoId: $('#txtLocalityAutoId').val(),
        SearchText: $("#txtName").val(),
        TransDate: GetEnrollmentDate()
    }
    return Model;
}
$('#chkWearGlasses').on('change', function () {
    CheckGlasses();
    //CheckReligon();
})
$('#chkDecreasedVision').on('change', function () {
    if ($('#chkWearGlasses').is(':checked')) {
        $('#chkDecreasedVision').prop('checked', true);
    }
    //CheckGlasses();
    //CheckReligon();
})

$('#chkMuslim').change(function () {
    $('#chkMuslim').prop('checked', true);
    $('#chkNonMuslim').prop('checked', false);
    //CheckReligon()
})
$('#chkNonMuslim').change(function () {
    $('#chkNonMuslim').prop('checked', true);
    $('#chkMuslim').prop('checked', false);
})
$('#ddlGender').change(function () {
    GetGenderSDW();
})
$("#txtAge").keypress(function (e) {
    if ($("#txtAge").val().length > 1) {
        e.preventDefault();
    }
});
//$("#txtName").keypress(function (e) {
//    AllowAlphabetOnly(e);
//});
$("#txtRelationName").keypress(function (e) {
    AllowAlphabetOnly(e);
});

$('#txtCNIC').blur(function () {
    ValidateCNIC();
})

$('#txtCell').change(function () {
    maskMobileNumber('txtCell');
})
function ValidateCNIC() {
    $('#txtCNIC').val($('#txtCNIC').val().substr(0, 5) + '-' + $('#txtCNIC').val().substr(5, 7) + '-' + $('#txtCNIC').val().substr(12, 1));
    isValid_NIC('txtCNIC');

}
function GetGenderSDW() {
    if ($('#ddlGender').val() == "1") {
        $('#ddlRelationType').empty();
        $('#ddlRelationType').append('<option selected value="S">Son</option>');
    }
    else {
        $('#ddlRelationType').empty();
        $('#ddlRelationType').append('<option selected value="D">Daughter</option>');
        $('#ddlRelationType').append('<option value="W">Wife</option>');
    }
}

function CheckGlasses() {

    if (!$('#chkWearGlasses').is(':checked')) {
        $('#chkDistance').prop("disabled", true);
        $('#chkNear').prop("disabled", true);
        /*$('#chkDecreasedVision').prop("disabled", true);*/
        $('#chkDistance').prop("checked", false);
        $('#chkNear').prop("checked", false);
        /*$('#chkDecreasedVision').prop('checked', false);*/
        $('#chkDistance').prop("disabled", true);
        $('#chkNear').prop("disabled", true);
    }
    else {
        $('#chkDistance').prop("disabled", false);
        $('#chkNear').prop("disabled", false);
        $('#chkDecreasedVision').prop("disabled", false);
        $('#chkDecreasedVision').prop('checked', true);
    }
}


$('#btn_AddImage').click(function () {
    var divClass = ".fileupload-preview fileupload-exists thumbnail"; // Replace with the class name of your <div>
    //    console.log(imgElements);
    var imgElements = $(divClass).find("img");
    imgElements.each(function () {
        var src = $(this).attr("src");
    });
    $('#chkWebCam').prop("checked", false);
    $('#chkWebCam').change();
    CheckWebCam();
})
$("#chkWebCam").change(function () {
    if (this.checked) {
        $('#btnCapture').show();
        $('#LiveCamera').css({ 'display': '', 'width': 200, 'height': 150 });
        Webcam.set({
            width: 200,
            height: 150,
            image_format: 'png',
            jpeg_quality: 100
        });

        Webcam.attach('#LiveCamera');
    }
    else {
        $('#btnCapture').hide();
        $('#LiveCamera').css({ 'display': 'none', 'width': 180, 'height': 150 });
    }


});

function RemoveById(id, obj) {
    //_ConfirmAjaxRequest('Delete', 'Do you want to Delete?', "/Localities/LocalityResidentEnrollment/DeleteByImage/" + id, null, 'json', 'Delete', null, null, 'Localities/LocalityResidentEnrollment/Add/' + RefreshId);
    $.confirm({
        title: 'Delete',
        content: 'Are you sure',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Localities/LocalityResidentEnrollment/DeleteByImage/' + id,
                    method: 'post',
                    dataType: 'json',
                    success: function (result) {
                        //console.log(result);
                        if (result.toLowerCase().includes('successfully')) {
                            $.alert(result, "Deleted");
                            remove(obj);

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

function CaptureSnapshot() {
    Webcam.snap(function (data) {
        console.log(data);
        img = document.getElementById('profileAvatar')
        img.src = data

        // Send image data to the controller to store locally or in database

        $('#TypeFile').val(null);
        $('#txtFileName').val($('#txtName').val());
        $('#txtFileSize').val('60544');
        $('#txtFileType').val('png');
        $('#btn_AddImage').show();
        $('#btn_Remove').show();
    });
}
function previewImage(event) {
    var reader = new FileReader(); // Create a FileReader object

    reader.onload = function () {
        var img = document.getElementById('profileAvatar');
        img.src = reader.result; // Set the src of the <img> tag to the image data URL
    };

    reader.readAsDataURL(event.target.files[0]); // Read the selected file as a data URL
    $('#profileAvatar').css({ 'display': '', 'width': 190, 'height': 150 });
    $('#btn_AddImage').show();
    $('#btn_Remove').show();

}
$('#TypeFile').change(function () {
    var file = this.files[0];
    console.log(file);
    var fileName = file.name;
    var fileType = file.type;
    var fileSize = file.size;
    $('#txtFileName').val(fileName);
    $('#txtFileSize').val(fileSize);
    $('#txtFileType').val(fileType);
});


$('#btn_Remove').click(function () {
    $('#profileAvatar').attr('src', imagePath);
    $('#profileAvatar').css("background-color", "lightgray");
    $('#txtFileName').val('');
    $('#txtFileSize').val('');
    $('#txtFileType').val('');
})

$('#btn_AddImage').click(function () {
    if ($('#TypeFile').val() == "")
        $("#txtFileName").val($('#txtName').val());
    var img1 = '<a href="' + $('#profileAvatar').attr('src') + '"><img style="width:110px;height:100px" src="' + $('#profileAvatar').attr('src') + '"/></a>';
    var i = $("#tblDetail tr").length + 1;
    let
        tr = "<tr data-tranID=" + i + " data-tranDetailID=" + i + " >";
    tr = tr + "<td  width=100px scope=row>" + i + "</td>";
    tr = tr + "<td  width=200px scope=row>" + $("#txtFileName").val() + "</td>";
    tr = tr + "<td  width=150px scope=row>" + $('#txtFileSize').val() + "</td>";
    tr = tr + "<td  width=150px scope=row>" + $('#txtFileType').val() + "</td>";
    tr = tr + "<td  width=200px scope=row>" + img1 + "</td>";
    tr = tr + "<td  scope=row hidden>" + $('#profileAvatar').attr('src') + "</td>";
    tr = tr + "<td   hidden>" + false + "</td>";
    tr = tr + "<td  width=200px scope=row>" + $('#txtCaptureRemarks').val() + "</td>";
    tr = tr + '<td style="margin-left:-50px;width=150px;"><button onclick="remove(this)" class="btn btn-danger"><i class="fa fa-remove"></i>&nbsp;&nbsp;Remove</button></td>';
    tr = tr + "</tr>";
    $('#tblDetail').append(tr);
    i = i = 1;
    $('#ImgUpdate').val(true);
    $('#btn_Remove').hide();
    $('#btn_AddImage').hide();
    CheckWebCam();
    $('#profileAvatar').attr('src', imagePath);
    $('#profileAvatar').css("background-color", "lightgray");
    $('#btn_Remove').click();
})

function CreateTableForSavedImages(obj) {
    $('#tblDetail').empty();
    console.log("Detail", obj);
    for (var i = 0; i < obj.length; i++) {
        if (obj[i]["residentImageAutoId"] > 0) {
            j = i + 1;
            var img1 = '<a href="' + obj[i]["residentPicture"] + '"><img style="width:110px;height:100px" src="' + obj[i]["residentPicture"] + '"/></a>';
            let
                tr = "<tr data-tranID=" + obj[i]["residentImageAutoId"] + " data-tranDetailID=" + i + " >";
            tr = tr + "<td scope=row>" + j + "</td>";
            tr = tr + "<td scope=row>" + $('#txtName').val() + "</td>";
            tr = tr + "<td scope=row>" + obj[i]["fileSize"] + "</td>";
            tr = tr + "<td scope=row>" + obj[i]["fileType"] + "</td>";
            tr = tr + "<td scope=row>" + img1 + "</td>";
            tr = tr + "<td scope=row hidden>" + $('#profileAvatar').attr('src') + "</td>";
            tr = tr + "<td hidden>" + true + "</td>";
            tr = tr + "<td >" + obj[i]["captureRemarks"] + "</td>";
            tr = tr + '<td style="margin-left:-50px;width=150px;"><button onclick="RemoveById(' + obj[i]["residentImageAutoId"] + ',this)" class="btn btn-danger"><i class="fa fa-remove"></i>&nbsp;&nbsp;Remove</button></td>';
            tr = tr + "</tr>";
            $('#tblDetail').append(tr);
        }
    }
}
function RemoveById(id, RefreshId, obj) {
    _ConfirmAjaxRequest('Delete', 'Do you want to Delete?', "/Localities/LocalityResidentEnrollment/DeleteByImage/" + id, null, 'json', 'Delete', null, null, 'Localities/LocalityResidentEnrollment/Add/' + RefreshId);
    remove(obj);
}

function remove(obj) {
    console.log(obj);
    $(obj).closest('tr').remove();
    resetRow();

}

function resetRow() {
    let i = 1;
    $('#tblDetail > tr').each(function (index) {
        var $td = $(this).find('td');
        $td.eq(0).html(i);
        ++i;
    });
}

function CheckWebCam() {
    if (!$('#chkWebCam').is(':checked')) {
        $('#btn_Remove').hide();
        $('#btn_AddImage').hide();
        $("#btnCapture").hide();
    }
    else {
        $("#btnCapture").show();
    }
}



function validate() {

    var returnVal = true;

    if ($('#txtEnrollementDate').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtEnrollementDate", "show", "Date is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtEnrollementDate", "hide", "", "bottom")
    }

    if ($('#txtLocalityName').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtLocalityName", "show", "Name is Required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtLocalityName", "hide", "", "bottom")
    }

    if ($('#txtLocalityCode').val() == '') {
        returnVal = false; 
        AddVAlidationToControl("txtLocalityCode", "show", "Required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtLocalityCode", "hide", "", "bottom")
    }

    if ($('#txtName').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtName", "show", "Name is Required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtName", "hide", "", "top")
    }
    if ($('#txtAge').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtAge", "show", "Age is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtAge", "hide", "", "top")
    }

    if ($('#txtRelationName').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtRelationName", "show", "Name is Required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtRelationName", "hide", "", "top")
    }
    if ($('#txtCNIC').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtCNIC", "show", "CNIC is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtCNIC", "hide", "", "top")
    }

    if ($('#btn_AddImage').is(":visible")) {
        $.alert('Kindly click Add Image button for saveing the image', 'Alert');
        returnVal = false;
        $(window).scrollTop(0);
    }

    if (!$('#chkMuslim').prop("checked") && !$('#chkNonMuslim').prop("checked")) {
        returnVal = false;
        AddVAlidationToControl("chkNonMuslim", "show", "*", "top")
        AddVAlidationToControl("chkMuslim", "show", "*", "top")
        $(window).scrollTop(10);
    }
    else {
        AddVAlidationToControl("chkNonMuslim", "hide", "", "top")
        AddVAlidationToControl("chkMuslim", "hide", "", "top")
    }
    return returnVal;

}


$('#btnAdd').click(function () {
    window.location.href = 'Add/0';
});

$('#btnRefresh').click(function () {
    window.location.reload(true);
})
$('#btnBack').click(function () {
    window.location = "/Localities/LocalityResidentEnrollment/list";
});

$('#btnDelete').click(function () {
    if ($('#txtResidentAutoId').val() != '' && $('#txtResidentAutoId').val() != 'select' && $('#txtResidentAutoId').val() != '0') {
        $.confirm({
            title: 'Delete',
            content: 'Are you sure',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: '/Localities/LocalityResidentEnrollment/DeleteById/' + $('#txtResidentAutoId').val(),
                        method: 'post',
                        dataType: 'json',
                        success: function (result) {
                            //console.log(result);
                            if (result.toLowerCase().includes('successfully')) {
                                $.alert(result, "Deleted");
                                $('#chkNew').prop("checked", true);
                                $('#chkNew').change();
                                ResetResidentAutoComplete();


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

function ResetResidentAutoComplete() {
    $("#txtName").autocomplete({ source: [] });
}
$('#chkCompanies').on('change', function () {

    if ($('#chkCompanies').is(":checked")) {
        UncheckedOther('chkCompanies');
        window.location.assign(window.location.origin + "/Factory/CompanyWorkerEnrollment/Add/0");
    }
})
$('#chkGoth').on('change', function () {

    if ($('#chkGoth').is(":checked")) {
        UncheckedOther('chkGoth');
        window.location.assign(window.location.origin + "/Goths/GothsResidentEnrollment/Add/0");
    }
})
$('#chkLocalities').on('change', function () {

    if ($('#chkLocalities').is(":checked")) {
        UncheckedOther('chkLocalities');
        window.location.assign(window.location.origin + "/Localities/LocalityResidentEnrollment/Add/0");
    }
})
$('#chkPublicSpaces').on('change', function () {

    if ($('#chkPublicSpaces').is(":checked")) {
        UncheckedOther('chkPublicSpaces');
        window.location.assign(window.location.origin + "/PublicSpaces/PublicSpacesResidentEnrollment/Add/0");
    }
})