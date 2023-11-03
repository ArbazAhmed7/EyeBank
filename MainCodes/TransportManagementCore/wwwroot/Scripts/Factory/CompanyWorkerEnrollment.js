
var imagePath = '~/img/noimage.png';
$(document).ready(function () {
    
    if (!window.location.href.includes("list")) {
        CustomDate('txtEnrollementDate');

        if ($('#txtWorkerAutoId').val()||0<=0)
            GetDate('txtEnrollementDate'); 
        CheckGlasses();
        GetGenderSDW();
        //GetCompany();
        if ($('#txtLoginId').val() != null && $('#txtLoginId').val() != "")
            console.log("Session value: ", $('#txtLoginId').val());
        if ($('#txtWorkerAutoId').val() != "0") {
            GetModal($('#txtWorkerAutoId').val());
        }
        $('#chkNew').prop('checked', true);
        $('#chkNew').change(); 
        $('#chkCompanies').prop('checked', true);
        $('#txtCompanyName').focus();
        
    
    }
    CheckWebCam();
    //$(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
    //    $(this).closest(".select2-container").siblings('select:enabled').select2('open');
    //});
     
})

$('#txtEnrollementDate').change(function () {
    DateChange('txtEnrollementDate');
})

$('#chkNew').on('change', function () {
    if ($('#chkNew').is(':checked')) {
        $('#chkDisplay').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        //ResetWorkerValue();
        ResetWorkerInfo();
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#btnSave').show();
        $('#btnSave').text('Save');
        GetAutoCompleteComapny();
        $('#btnDelete').prop('disabled', true);
        $("#txtCompanyName").attr("placeholder", "Search factory name by pressing ↓ key");
        $("#txtName").attr("placeholder", "");
        ResetWorkerAutoComplete();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        ResetWorkerInfo();
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
    }
    GetAutoCompleteComapny();
    //GetDates();
});

$('#chkDisplay').on('change', function () {
    if ($('#chkDisplay').is(':checked')) {
        ResetWorkerInfo();
        $('#chkNew').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
        $('#btnSave').hide();
        GetAutoCompleteComapny()
        $("#txtCompanyName").attr("placeholder", "Search factory name by pressing ↓ key");
        $("#txtName").attr("placeholder", "Search worker name by pressing ↓ key");
        $('#btnDelete').prop('disabled', false);
        if ($('#txtCompanyAutoId').val() != "" && $('#txtCompanyName').val() != "")
            GetAutoCompleteWorker();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        ResetWorkerInfo();
    }
    GetAutoCompleteComapny();
    //GetDates();

});

$('#chkEdit').on('change', function () {
    if ($('#chkEdit').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkDisplay').prop('checked', false);
        ResetWorkerInfo();
        $('#btnSave').show();
        $('#btnSave').text('Update');
        GetAutoCompleteComapny();
        $("#txtCompanyName").attr("placeholder", "Search factory name by pressing ↓ key");
        $('#btnDelete').prop('disabled', false);
        $("#txtName").attr("placeholder", "Search worker name by pressing ↓ key");
        if ($('#txtCompanyAutoId').val() != "" && $('#txtCompanyName').val() != "")
            GetAutoCompleteWorker();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
    }
    GetAutoCompleteComapny();
    // GetDates();
});


//function GetCompany() {
//    bindDropDown("ddlCompany", "/Factory/CompanyWorkerEnrollment/DropDown/Company");
//    $('#ddlCompany').append('<option value=0 selected></option>')
//}

function GetModal(Id) {
    $.ajax({
        url: '/Factory/CompanyWorkerEnrollment/GetById/' + Id,
        method: 'get',
        dataType: 'json',
        success: function (result) {
            console.log(result);
            SetValues(result)
            CreateTableForSavedImages(result.imageList);
        }
    })
}



$('#btnSave').click(function () {
    console.log(GetModel());
    if (!validate()) return;
    //var now = new Date($('#txtEnrollementDate').val());
    //var day = ("0" + now.getDate()).slice(-2);
    //var month = ("0" + (now.getMonth() + 1)).slice(-2);
    //var FinalDate= now.getFullYear() + "-" + (month) + "-" + (day);
    

    var Model = {
        EnrollementDate: GetEnrollmentDate(),
        CompanyAutoId: $('#txtCompanyAutoId').val(),
        CompanyCode: $('#txtCompanyCode').val(),
        WorkerAutoId: $('#txtWorkerAutoId').val() || 0,
        WorkerName: $('#txtName').val(),
        WorkerCode: $('#txtWorkerCode').val(),
        RelationType: $('#ddlRelationType').val(),
        RelationName: $('#txtRelationName').val(),
        GenderAutoId: GetGender(),
        CNIC: $('#txtCNIC').val(),
        Age:$('#txtAge').val(),
        MobileNo: $('#txtCell').val(),
        WearGlasses: WearGlasses(),
        Distance: Distance(),
        Near:Near(),
        DecreasedVision: DecreasedVision(),
        Religion:GetReligon(),
        ImageList: GetModel(),
    }
    console.log("Model", Model);
    var title = "Save", content = "Do you want to save?";
    if ($('#txtWorkerAutoId').val() > 0) {
        title = "Update";
        content = "Do you want to update?";
    }
    //var response = _ConfirmAjaxRequest(title, content, "/Factory/CompanyWorkerEnrollment/SaveUpdate", Model, 'json', 'post', null, 'tblDetail');

    $.confirm({
        title: title,
        content: content,
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Factory/CompanyWorkerEnrollment/SaveUpdate",
                    data: Model,
                    method: 'post',
                    dataType: 'json',
                    success: function (result) {
                        //console.log(result);
                        if (result.toLowerCase().includes('successfully')) {
                            $("#chkWebCam").change();
                            $.confirm({
                                title: result,
                                content: "Do you want to enroll more worker's also?",
                                buttons: {
                                    Yes: function () {
                                        ResetWorkerInfo();
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
   

function GetEnrollmentDate() {
    var now = new Date($('#txtEnrollementDate').val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    return FinalDate;
}

function GetModel() {
    var ImageList = [];
    $("#tbl_CompanyInfo tr:gt(0)").each(function () {
        this_row = $(this);
        var Object = {
            WorkerAutoId: $('#txtWorkerAutoId').val() || 0,
            FileName: $.trim(this_row.find('td:eq(1)').html()),//td:eq(0) means first td of this row
            FileSize: $.trim(this_row.find('td:eq(2)').html()),
            FileType: $.trim(this_row.find('td:eq(3)').html()),
            WorkerPicture: $.trim(this_row.find('td:eq(5)').html()),
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
    $('#txtWorkerAutoId').val(object.workerAutoId)
    $('#txtWorkerCode').val(object.workerCode)
    /*$('#ddlCompany').val(object.companyCode).trigger('change');*/
    $('#txtCompanyCode').val(object.companyCode)
    $('#txtCompanyAutoId').val(object.companyAutoId);
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
    $('#txtName').val(object.workerName)
    $('#txtCNIC').val(object.cnic);
    SetDate('txtEnrollementDate', object.enrollementDate)
    
    //$('#txtEnrollementDate').val(object.enrollementDate.substr(0, 10));
    //$('#txtEnrollementDateCustom').val(object.viewDate)
}

function ResetWorkerInfo() {
    $('#chkWearGlasses').prop('checked', false);
    $('#chkDistance').prop('checked', false);
    $('#chkNear').prop('checked', false);
    $('#chkDecreasedVision').prop('checked', false);
    $('#chkNonMuslim').prop('checked', false);
    $('#chkMuslim').prop('checked', false);
    $('#txtCell').val('');
    $('#txtName').val('');
    $('#txtWorkerCode').val('');
    $('#txtWorkerAutoId').val('');
    $('#txtCNIC').val('');
    $('#ddlGender').val('1');
    $('#ddlRelationType').val('');
    $('#txtRelationName').val('');
    $('#txtAge').val(''); 
    $('#txtCaptureRemarks').val('');
    /*$('#ddlCompany').change();*/
    $('#chkWearGlasses').change();
    $('#ddlGender').change();
    $("#tblDetail").empty();
    
    if ($('#txtCompanyName').val() != "") {
    setTimeout(function () { 
        $('#txtName').focus();
    },300)  
    }
    $('#txtCompanyName').focus();
}



function onActionButtonClicked(actionName, data) {
    /*console.log(actionName);*/
    switch (actionName.toLowerCase()) {
        case 'edit':
            window.location = "/Factory/CompanyWorkerEnrollment/Add/" + data[0];
            break;
        case 'delete':
            _ConfirmAjaxRequest('Delete', 'Do you want to Delete?', "/Factory/CompanyWorkerEnrollment/Delete/" + data[0], null, 'json', 'Delete', '/Factory/CompanyWorkerEnrollment/GetCompanyList');
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


$('#ddlCompany').change(function () {
    $('#txtCompanyCode').val($('#ddlCompany').val());
    //_AjaxRequest('/Factory/CompanyWorkerEnrollment/GetCode/' + $('#ddlCompany').val(),null,'json','get')
    $.ajax({
        url:'/Factory/CompanyWorkerEnrollment/GetCode/' + $('#ddlCompany').val(),
        dataType: 'json',
        method: 'get',
        async: false,
        success: function (response) {
            console.log(response);
            $('#txtWorkerCode').val(response);

            // Handle successful response
            return response;
        },
        error: function (xhr, status, error) {
            // Handle error response
            return "Error: ", error;
        }
    });
})

$("#txtCompanyName").keydown(function (e) {
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
        GetAutoCompleteWorker();
    }
})

function GetCompanyText() {

    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        SearchText: $("#txtCompanyName").val()
    }
    return Model;
}

 

function GetAutoCompleteComapny() {
    
    companies = [];
    $.ajax({
        url: '/DropDownLookUp/Help/GetCompanies',
        data: GetCompanyText(),
        method: 'get',
        dataType: 'json',
        success: function (result) {
            companies = result;
        }
    })

    $("#txtCompanyName").autocomplete({
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
                document.getElementById("txtCompanyName").value = selectedName;
                document.getElementById("txtCompanyCode").value = code;
                document.getElementById("txtCompanyAutoId").value = selectedId;
               // GetCompanyModal($('#txtCompanyAutoId').val());


            }, 50);
            setTimeout(function () {
                $('#txtName').focus();
                GetAutoCompleteWorker();
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
function GetAutoCompleteWorker() {
    
    if ($('#chkNew').is(':checked') == true) return;
    if ($('#txtCompanyAutoId').val() > 0) {
        $.ajax({
            // url: '/DropDownLookUp/Help/GetWorkers/' + $('#txtCompanyId').val() + "/" + Type + "/" + FinalDate,
            url: '/DropDownLookUp/Help/GetWorkersForCompany',
            data: GeWorkerText(),
            method: 'get',
            dataType: 'json',
            success: function (result) {
                Workers = result;
            }
        })

        $("#txtName").autocomplete({
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
                console.log(event, ui);
                // Handle the selection of an item
                var selectedId = ui.item.id;
                var selectedName = ui.item.text;
                var code = ui.item.code;
                name = ui.item.values;
                //console.log("Selected ID: " + selectedId);
                //console.log("Selected Name: " + selectedName);

                setTimeout(function () {
                    ResetWorkerInfo();
                    document.getElementById("txtName").value = selectedName.substr(0, selectedName.length - 11);;
                    document.getElementById("txtWorkerCode").value = code;
                    document.getElementById("txtWorkerAutoId").value = selectedId;
                    GetModal($('#txtWorkerAutoId').val());


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
 
function GeWorkerText() {
    //var now = new Date($('#txtEnrollementDate').val());
    //var day = ("0" + now.getDate()).slice(-2);
    //var month = ("0" + (now.getMonth() + 1)).slice(-2);
    //var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        AutoId: $('#txtCompanyAutoId').val(),
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

    $.confirm({
        title: 'Delete',
        content: 'Are you sure',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Factory/CompanyWorkerEnrollment/DeleteByImage/' + id,
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
        if (obj[i]["workerImageAutoId"] > 0) {
            j = i + 1;
            var img1 = '<a href="' + obj[i]["workerPicture"] + '"><img style="width:110px;height:100px" src="' + obj[i]["workerPicture"] + '"/></a>';
            let
                tr = "<tr data-tranID=" + obj[i]["workerImageAutoId"] + " data-tranDetailID=" + i + " >";
            tr = tr + "<td scope=row>" + j + "</td>";
            tr = tr + "<td scope=row>" + $('#txtName').val() + "</td>";
            tr = tr + "<td scope=row>" + obj[i]["fileSize"] + "</td>";
            tr = tr + "<td scope=row>" + obj[i]["fileType"] + "</td>";
            tr = tr + "<td scope=row>" + img1 + "</td>";
            tr = tr + "<td scope=row hidden>" + $('#profileAvatar').attr('src') + "</td>";
            tr = tr + "<td hidden>" + true + "</td>";
            tr = tr + "<td >" + obj[i]["captureRemarks"] + "</td>";
            tr = tr + '<td style="margin-left:-50px;width=150px;"><button onclick="RemoveById(' + obj[i]["workerImageAutoId"] + ',this)" class="btn btn-danger"><i class="fa fa-remove"></i>&nbsp;&nbsp;Remove</button></td>';
            tr = tr + "</tr>";
            $('#tblDetail').append(tr);
        }
    }
}
//function RemoveById(id, RefreshId,obj) {
//    _ConfirmAjaxRequest('Delete', 'Do you want to Delete?', "/Factory/CompanyWorkerEnrollment/DeleteByImage/" + id, null, 'json', 'Delete', null, null, 'Factory/CompanyWorkerEnrollment/Add/' + RefreshId);
//    remove(obj);
//}

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

    if ($('#txtCompanyName').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtCompanyName", "show", "Name is Required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtCompanyName", "hide", "", "bottom")
    }

    if ($('#txtCompanyCode').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtCompanyCode", "show", "Required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtCompanyCode", "hide", "", "bottom")
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
    window.location = "/Factory/CompanyWorkerEnrollment/list";
});

$('#btnDelete').click(function () {
    if ($('#txtWorkerAutoId').val() != '' && $('#txtWorkerAutoId').val() != 'select' && $('#txtWorkerAutoId').val() != '0') {
        $.confirm({
            title: 'Delete',
            content: 'Are you sure',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: '/Factory/CompanyWorkerEnrollment/DeleteById/' + $('#txtWorkerAutoId').val(),
                        method: 'post',
                        dataType: 'json',
                        success: function (result) {
                            //console.log(result);
                            if (result.toLowerCase().includes('successfully')) {
                                $.alert(result, "Deleted");
                                $('#chkNew').prop("checked", true);
                                $('#chkNew').change();
                                ResetWorkerAutoComplete();


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

function ResetWorkerAutoComplete() {
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
