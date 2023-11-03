
$(document).ready(function () {

    if (!window.location.href.includes("list")) {
        GetDate('txtVisitDate');
        CustomDate('txtVisitDate');
        GetDate('txtPostVisitDate');
        CustomDate('txtPostVisitDate');

        GetAutoCompleteComapny();
        if ($('#txtLoginId').val() != null && $('#txtLoginId').val() != "")
            console.log("Session value: ", $('#txtLoginId').val());
        if ($('#txtWorkerAutoId').val() != "" && $('#txtWorkerAutoId').val() != "0") {
            GetModal();

        }
        $('#chkNew').prop('checked', true);
        $('#chkNew').change();
        $('#autocomplete-input-company').change();
        $('#autocomplete-input-company').focus();
 
        OneCheckBoxChecked()
        //CheckboxChanged();
        DisabledCheckedUncheckedChecbox('chkWearGlasses');
        DisabledCheckedUncheckedChecbox('chkDistance');
        DisabledCheckedUncheckedChecbox('chkNear');
    }

})

function OneCheckBoxChecked() {
    OnCheckBoxChecked('Eye');
}

$('#chkNew').on('change', function () {
    if ($('#chkNew').is(':checked')) {
        $('#chkDisplay').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        ResetWorkerValue();
        ResetFields();
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtOptometristWorkerId').val('')
        $('#btnSave').text('Save');
        $('#btnDelete').prop('disabled', true)
        if ($('#txtCompanyCode').val() != "" && $('#autocomplete-input-company').val() != "")
            GetAutoCompleteWorker();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
    }
    GetAutoCompleteComapny();
    GetDates();
});

$('#chkDisplay').on('change', function () {
    if ($('#chkDisplay').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
        $('#btnDelete').prop('disabled', false)
        GetAutoCompleteWorker()
        $('#btnSave').hide();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#btnDelete').prop('disabled', false)
    }
    GetAutoCompleteComapny();
    GetDates();

});

$('#chkEdit').on('change', function () {
    if ($('#chkEdit').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkDisplay').prop('checked', false);
        $('#btnDelete').prop('disabled', false)
        GetAutoCompleteWorker()
        $('#btnSave').text('Update');
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
    }
    GetAutoCompleteComapny();
    GetDates();
});

var formData = new FormData();
var count = 0;
$("#btnAdd").on('click', function () {
    var file = document.getElementById("ScanDocUpload"); 
    for (var i = 0; i != file.files.length; i++) {
        count = count + 1;
        formData.append("files", file.files[i]); 
        imagesPreview(file, 'div.gallery',  count);
    }
});

function imagesPreview(input, placeToInsertImagePreview,  count) {
    if (input.files) {
        var filesAmount = input.files.length;
        var files = input.files;
        for (i = 0; i < filesAmount; i++) {
            var reader = new FileReader();
            reader.onload = function (event) {
                //console.log(event);
                createModelTable(i, event.target.result, input.files[0].name, files[0], count);
            }
            reader.readAsDataURL(input.files[i]);
        }
    }
};
var imgCount = 0;
function createModelTable(id, img, filename, files, count) {
    var tableHTML = "<tr id=" + imgCount + ">" +
        "<td><label id='FileName' style='width:100px'>" + filename + "</label></td>" + 
        "<td hidden><label id='FileName' style='width:100px'>" + count + "</label></td>" +
        "<td style='margin-left:-15px'> <div style='font-size: 14px; display: inline-flex;'" +
        "</i ></button > <button id=btnRemove " +
        "data-id=" + imgCount + " class='action-button btn btn-light waves-effect' title='Delete' data-action='delete' type='button'><i class='zmdi zmdi-close-circle zmdi-hc-lg mdc-text-red-500'></i></button></div ></td > " +
        "</tr > ";
    $("#uploadFile").append(tableHTML);
    $("#ScanDocUpload").val('')
    imgCount++;
    $('#NoOfDocuments').text("No of Documents : ");
    $('#no').text($("#uploadFile tr").length);
}

$(document).on("click", "#btnRemove", function (e, i) {
    var c = 1;
    var remaing = $('#no').text();
    remaing = remaing - 1;
    $('#no').text(remaing); 
    var files = formData.getAll("files"); 
    var Count = count - 1
    files.splice($("[type='file']").index(Count), 1);
    formData.delete("files");
    $.each(files, function (i, v) {

        formData.append("files", v);
    });
    $(this).closest('tr').remove();
});


$("#autocomplete-input-company").keydown(function (e) {
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
function GetCompanyText() {

    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        SearchText: $("#autocomplete-input-company").val()
    }
    return Model;
}
function GetAutoCompleteComapny() {
    var Type = true;
    if ($('#chkEdit').is(':checked'))
        Type = false;
    companies = [];
    $.ajax({
        url: '/DropDownLookUp/Help/GetCompaniesForVisitForSurgeryWorker',
        data: GetCompanyText(),
        method: 'get',
        dataType: 'json',
        success: function (result) {
            companies = result;
        }
    })

    $("#autocomplete-input-company").autocomplete({
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
            //console.log(event, ui);
            // Handle the selection of an item
            var selectedId = ui.item.id;
            var selectedName = ui.item.text;
            var code = ui.item.code;
            name = ui.item.values;
            //.log("Selected ID: " + selectedId);
            //console.log("Selected Name: " + selectedName);

            setTimeout(function () {
                document.getElementById("autocomplete-input-company").value = selectedName;
                document.getElementById("txtCompanyCode").value = code;
                document.getElementById("txtCompanyAutoId").value = selectedId;
                ResetWorkerValue()
                GetAutoCompleteWorker();


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

function GeWorkerText() {
    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        AutoId: $('#txtCompanyAutoId').val(),
        SearchText: $("#autocomplete-input-worker").val(),
        TransDate: GetVisitDate()
    }
    return Model;
}

function GetVisitDate() {
    var now = new Date($('#txtVisitDate').val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    return FinalDate;
}
function GetAutoCompleteWorker() {
    if ($('#txtCompanyAutoId').val() > 0) {
        $.ajax({
            url: '/DropDownLookUp/Help/GetWorkersForVisitForSurgeryWorker',
            data: GeWorkerText(),
            method: 'get',
            dataType: 'json',
            success: function (result) {
                Workers = result;
            }
        })

        $("#autocomplete-input-worker").focus();
        $("#autocomplete-input-worker").autocomplete({
            source: function (request, response) {
                var term = request.term.toLowerCase();
                // Filter the item list based on the search term
                var filteredList = $.grep(Workers, function (item) {
                    //console.log(companies);
                    return item.text.toLowerCase().indexOf(term) !== -1;

                });

                response(filteredList); // Pass the filtered list to the response function
            },
            minLength: 0, // Set the minimum length of input before triggering autocomplete
            select: function (event, ui) {
                //console.log(event, ui);
                // Handle the selection of an item
                var selectedId = ui.item.id;
                var selectedName = ui.item.text;
                var code = ui.item.code;
                name = ui.item.values;

                setTimeout(function () {
                    document.getElementById("autocomplete-input-worker").value = selectedName.substr(0, selectedName.length - 11);
                    document.getElementById("txtWorkerCode").value = code;
                    document.getElementById("txtWorkerAutoId").value = selectedId;
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


function GetModal() {
    $.ajax({
        url: '/Factory/VisitForSurgeryWorker/GetLastOptoForSurgery',
        method: 'get',
        data: GetWorkerModel(),
        dataType: 'json',
        success: function (result) {
            //console.log(result);
            SetValues(result);
        }
    })
}
function GetWorkerModel() {
    var Model = {
        WorkerAutoId: $('#txtWorkerAutoId').val(),
        VisitDate: GetVisitDate()
    }
    return Model;
}

function SetValues(object) {
    SetWearGlasses(object.wearGlasses);
    SetDistance(object.distance);
    SetNear(object.near);
    $('#chkWearGlasses').change();
    $('#txtAge').val(object.age);
    $('#txtMobile').val(object.mobileNo);
    $('#txtGender').val(object.gender)
    $('#txtOptometristWorkerId').val(object.optometristWorkerId)
  
}

function CreateTableForSavedImages(obj) {
    //console.log("Detail", obj);
    for (var i = 0; i < obj.length; i++) {
        if (obj[i]["surgeryWorkerDocumentsId"] > 0) {
            j = i + 1;
            let
                tr = "<tr data-tranID=" + obj[i]["surgeryWorkerDocumentsId"] + " data-tranDetailID=" + i + " >";
            tr = tr + "<td scope=row>" + j + "</td>";
            tr = tr + "<td scope=row>" + obj[i]["fileName"] + "</td>"
            tr = tr + "<td scope=row>" + obj[i]["fileType"] + "</td>"
            tr = tr + "<td hidden>" + true + "</td>";
            tr = tr + '<td style="margin-left:-50px;width=150px;"><button onclick="DownloadById(' + obj[i]["surgeryWorkerDocumentsId"] + ',this)" class="btn btn-primary"><i class="fa fa-save"></i>&nbsp;&nbsp;Download</button></td>';
            tr = tr + '<td style="margin-left:-50px;width=150px;"><button onclick="RemoveById(' + obj[i]["surgeryWorkerDocumentsId"] + ',this)" class="btn btn-danger"><i class="fa fa-remove"></i>&nbsp;&nbsp;Remove</button></td>';
            tr = tr + "</tr>";
            $('#DocumentBody').append(tr);
            $('#tblDocuments').show();
        }
    }
}

function DownloadById(id, obj) {
    window.open(window.location.origin+"/Factory/VisitForSurgeryWorker/GetDocumentById/"+id, '_blank');

}


function RemoveById(id, obj) {

    $.confirm({
        title: 'Delete',
        content: 'Are you sure',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Factory/VisitForSurgeryWorker/DeleteById/' + id,
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

function remove(obj) {
    //console.log(obj);
    $(obj).closest('tr').remove();
    resetRow();

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

    if ($('#autocomplete-input-company').val() == '') {
        returnVal = false;
        AddVAlidationToControl("autocomplete-input-company", "show", "Name is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("autocomplete-input-company", "hide", "", "bottom")
    }

    if ($('#txtCompanyCode').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtCompanyCode", "show", "Code is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtCompanyCode", "hide", "", "bottom")
    }

    if ($('#autocomplete-input-worker').val() == '') {
        returnVal = false;
        AddVAlidationToControl("autocomplete-input-worker", "show", "Name is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("autocomplete-input-worker", "hide", "", "bottom")
    }

    if ($('#txtWorkerCode').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtWorkerCode", "show", "Code is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtWorkerCode", "hide", "", "bottom")
    }

    if ($('#txtHospital').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtHospital", "show", "Hospital is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtHospital", "hide", "", "bottom")
    }

    if ($('#txtOptometrist').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtOptometrist", "show", "Optometrist is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtOptometrist", "hide", "", "bottom")
    }

    if ($('#txtOphthalmologist').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtOphthalmologist", "show", "Ophthalmologist is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtOphthalmologist", "hide", "", "bottom")
    }

    if ($('#txtOphthalmologist').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtOphthalmologist", "show", "Ophthalmologist is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtOphthalmologist", "hide", "", "bottom")
    }

    if ($('#txtSurgeon').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtSurgeon", "show", "Surgeon is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtSurgeon", "hide", "", "bottom")
    }

    if ($('#txtSurgeryName').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtSurgeryName", "show", "Surgery Name is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtSurgeryName", "hide", "", "bottom")
    }

    if ($('#txtCommentsSurgeon').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtCommentsSurgeon", "show", "Comments are required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtCommentsSurgeon", "hide", "", "bottom")
    }

    if ($('#txtPostVisitDate').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtPostVisitDate", "show", "Next Follow up date is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtPostVisitDate", "hide", "", "bottom")
    }

    var atLeastOneCheckedEye= false;
    $('input[name="Eye"]').each(function () {
        if ($(this).prop('checked')) {
            atLeastOneCheckedEye = true;
            //return atLeastOneCheckedRightUnaided; // exit the loop early
        }
    });
    if (atLeastOneCheckedEye == false) {
        atLeastOneCheckedEye = false;
        AddVAlidationToControl("chkRightEye", "show", "*", "top")
        AddVAlidationToControl("chkLeftEye", "show", "*", "top")
        returnVal = false;
    }
    else {
        AddVAlidationToControl("chkRightEye", "hide", "", "top")
        AddVAlidationToControl("chkLeftEye", "hide", "", "top")
    }
    if ($("#uploadFile tr").length <= 0 && $("#DocumentBody tr").length <= 0) {
        AddVAlidationToControl("no", "show", "Upload atleast one document/picture", "top")
        returnVal = false;
    }
    else {
        AddVAlidationToControl("no", "hide", "", "top")
    }
    return returnVal;
}
function GetEye() {
    if ($('#chkRightEye').prop('checked'))
        return 'Right';
    else if ($('#chkLeftEye').prop('checked'))
        return 'Left';
}

$('#txtVisitDate').change(function () {
    DateChange('txtVisitDate');
    GetAutoCompleteWorker();
})

$('#txtPostVisitDate').change(function () {
    DateChange('txtPostVisitDate');
    //GetAutoCompleteWorker();
})

 
//$('#btn_upload').click(function () {
//    $('#exampleModal').show();
//})
$('#btnSave').click(function () {
    if (!validate()) { return; }
    formData.append("VisitSurgeryWorkerId", $('#txtVisitSurgeryWorkerId').val());
    formData.append("OptometristWorkerId", $('#txtOptometristWorkerId').val());
    formData.append("WorkerAutoId", $('#txtWorkerAutoId').val());
    formData.append("CompanyAutoId", $('#txtCompanyAutoId').val());
    formData.append("VisitDate", $('#txtVisitDate').val());
    formData.append("Hospital", $('#txtHospital').val());
    formData.append("Optometrist", $('#txtOptometrist').val());
    formData.append("Ophthalmologist", $('#txtOphthalmologist').val());
    formData.append("Surgeon", $('#txtSurgeon').val());
    formData.append("NameOfSurgery", $('#txtSurgeryName').val());
    formData.append("Eye", GetEye());
    formData.append("PostSurgeryVisitDate", $('#txtPostVisitDate').val());
    formData.append("CommentOfSurgeonAfterSurgery", $('#txtCommentsSurgeon').val());
    var Title = 'Save';
    var Content = 'Are you sure?'
    if ($('#txtVisitSurgeryWorkerId').val() || 0 > 0) {
        Title = 'Update';
    }
    $.confirm({
        title: Title,
        content: Content,
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Factory/VisitForSurgeryWorker/SaveUpdate',
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST",
                    success: function (result) {
                        //console.log(result);
                        if (result.toLowerCase().includes('successfully')) {
                            $.confirm({
                                title: result,
                                content: "Do you want to add other worker's Visit for surgery?",
                                buttons: {
                                    Yes: function () {
                                        $('#chkNew').prop('checked', true);
                                        $('#chkNew').change();
                                        ResetWorkerValue();
                                        ResetFields();
                                        GetAutoCompleteWorker();

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


$('#ddlGetHospitalVisitId').change(function () {
    $('#txtVisitSurgeryWorkerId').val($('#ddlGetHospitalVisitId').val());
    if ($('#ddlGetHospitalVisitId').val() > 0) {
        $.ajax({
            url: '/Factory/VisitForSurgeryWorker/GetSurgeryWorkerById/' + $('#txtVisitSurgeryWorkerId').val(),
            method: 'get',
            dataType: 'json',
            success: function (result) {
                ResetFields();
                //console.log("final ", result);
                SetModel(result);
            }
        })

    }
})

function GetDates() {
    if (($('#chkDisplay').is(':checked')) || $('#chkEdit').is(':checked')) {
        $.ajax({
            url: '/Factory/VisitForSurgeryWorker/GetDatesofSurgeryWorker/' + $('#txtWorkerAutoId').val(),
            method: 'get',
            type: 'json',
            async: false,
            success: function (result) {
                //console.log(result);
                $('#ddlGetHospitalVisitId').empty();
                $("#ddlGetHospitalVisitId").append($("<option></option>").val('').html(' Date'));
                $('#txtVisitSurgeryWorkerId').val('');
                $.each(result, function (data, value) {
                    $("#ddlGetHospitalVisitId").append($("<option></option>").val(value.code).html(value.text));
                })
                $('#divDisplay').show();
                $('#ddlGetHospitalVisitId').show()
                $('#ddlGetHospitalVisitId').focus();
            }
        })
        setTimeout(function () {
            var total_tems = $('#ddlGetHospitalVisitId').find('option').length;
            if (total_tems == 2) {
                $('#ddlGetHospitalVisitId option:nth-child(2)').attr('selected', true);
                $('#ddlGetHospitalVisitId').change()
            }

        }, 500)
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
    }
    else {
        $('#divDisplay').hide();
        $('#ddlGetHospitalVisitId').hide()
    }
    if ($('#chkDisplay').is(':checked')) {
        $('#btnSave').hide();
    }
    else
        $('#btnSave').show();

    if ($('#chkDisplay').is(':checked') || $('#chkEdit').is(':checked'))
        if ($('#autocomplete-input-company').val() == "" || $('#autocomplete-input-worker').val() == "") {
            $('#divDisplay').hide();
            $('#ddlGetHospitalVisitId').hide();
        }

}

function SetModel(result) {
    $('#txtVisitSurgeryWorkerId').val(result.visitSurgeryWorkerId);
    $('#txtHospital').val(result.hospital);
    $('#txtOptometrist').val(result.optometrist);
    $('#txtOphthalmologist').val(result.ophthalmologist);
    $('#txtSurgeon').val(result.surgeon);
    $('#txtSurgeryName').val(result.nameOfSurgery);
    $('#txtCommentsSurgeon').val(result.commentOfSurgeonAfterSurgery);
    $('#txtPostVisitDate').val(result.displayPostDate);
    SetEye(result.eye)
    CreateTableForSavedImages(result.modelfiles)
}
function SetEye(Eye) {
    if (Eye.toLowerCase() == "left") {
        $('#chkRightEye').prop('checked', false);
        $('#chkLeftEye').prop('checked', true);}
    else if (Eye.toLowerCase() == "right") {
        $('#chkRightEye').prop('checked', true);
        $('#chkLeftEye').prop('checked', false);
    }
}

$('#btnDelete').click(function () {
    $.confirm({
        title: 'Delete',
        content: 'Are you sure',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Factory/VisitForSurgeryWorker/DeleteSurgeryById/' + $('#txtVisitSurgeryWorkerId').val(),
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
})
function ResetCompanyValue() {
    document.getElementById("autocomplete-input-company").value = '';
    document.getElementById("txtCompanyCode").value = '';
    document.getElementById("txtCompanyAutoId").value = '';
}


function ResetWorkerValue() {
    document.getElementById("autocomplete-input-worker").value = '';
    document.getElementById("txtWorkerCode").value = '';
    document.getElementById("txtWorkerAutoId").value = '';
    $('#txtOptometristWorkerId').val('')
    $('#txtAge').val('');
    $('#txtGender').val(''); 
    $('#txtMobile').val('');
    $('#chkWearGlasses').prop('checked', false);
    $('#chkDistance').prop('checked', false);
    $('#chkNear').prop('checked', false);

    if ($('#autocomplete-input-company').val() != '') {
        setTimeout(function () {
            $('#autocomplete-input-worker').focus();
        }, 500);
    }
    else {
        $('#autocomplete-input-company').focus();
    }
    $('input[name="Eye"]').prop('checked', false); 

}

function ResetFields() {
    $('#txtVisitSurgeryWorkerId').val('');
    $('#txtOptometristWorkerId').val('');
    $('#txtHospital').val(''); 
    $('#txtOptometrist').val('');
    $('#txtOphthalmologist').val('');
    $('#txtSurgeon').val('');
    $('#chkRightEye').prop('checked', false);
    $('#chkLeftEye').prop('checked', false);
    $('#txtCommentsSurgeon').val(''); 
    formData = new FormData();
    $('#ScanDocUpload').val(null);
    $('#DocumentBody').empty();
    $('#tblDocuments').hide();
    $('#NoOfDocuments').text("");
    $('#uploadFile tr').empty()
    $('#no').text("");
}