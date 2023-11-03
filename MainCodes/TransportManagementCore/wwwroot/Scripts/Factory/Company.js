var imagePath = '~/img/noimage.png';
$(document).ready(function () {
    if (!window.location.href.includes("list")) {
        GetDate('txtEnrollementDate');
        CustomDate('txtEnrollementDate');
        //var now = new Date();
        //var day = ("0" + now.getDate()).slice(-2);
        //var month = ("0" + (now.getMonth() + 1)).slice(-2);
        //var today = now.getFullYear() + "-" + (month) + "-" + (day);
        
        //$('#txtEnrollementDate').val(today);
        //$('#txtEnrollementDate').hide();
        $('#txtCompanyName').focus();
        if ($('#txtCompanyId').val() != "0") {
            GetCompanyModal($('#txtCompanyId').val());
        }
        else {
            GetDate('txtEnrollementDate');
        }
        if ($('#txtLoginId').val() != null && $('#txtLoginId').val() != "")
            console.log("Session value: ", $('#txtLoginId').val());
        
        $('#chkNew').prop('checked', true);
        $('#chkCompanies').prop('checked', true);
        $('#chkNew').change();
        $('#autocomplete-input-company').change();
        $('#autocomplete-input-company').focus();
        CheckWebCam();
    }
})
function ResetFields() {
    $('#txtCompanyId').val('');
    $('#txtCompanyName').val('');
    $('#txtWebsite').val('');
    $('#txtAddress1').val('');
    $('#txtAddress2').val(''); 
    $('#txtAddress3').val('');
    $('#txtTown').val('');
    $('#txtDistrict').val('');
    $('#txtCity').val('');
    $('#txtWorkForce').val('');
    $('#txtOwnerName').val('');
    $('#txtOwnerCell').val('');
    $('#txtOwnerEmail').val('');
    $('#txtAdminHead').val('');
    $('#txtAdminHeadCell').val('');
    $('#txtAdminHeadEmail').val('');
    $('#txtHRHead').val('');
    $('#txtHRHeadCell').val('');
    $('#txtHRHeadEmail').val('');
    $('#txtCaptureRemarks').val('');
    $('#chkWebCam').prop('checked', false);
    $('#tblDetail').empty();

}

$('#chkNew').on('change', function () {
    if ($('#chkNew').is(':checked')) {
        $('#chkDisplay').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        //ResetWorkerValue();
        ResetFields();
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtCompanyId').val('');
        $('#btnSave').show();
        $('#btnSave').text('Save');
        $('#btnDelete').prop('disabled', true);
        $("#txtCompanyName").attr("placeholder", "");
        ResetCompanyAutoComplete();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        ResetFields();
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtCompanyId').val('')
    }
    GetAutoCompleteComapny();
    //GetDates();
});

$('#chkDisplay').on('change', function () {
    if ($('#chkDisplay').is(':checked')) {
        ResetFields();
        $('#chkNew').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        $('#DivlblVisitDate').hide();
        $('#DivinputVisitDate').hide();
        $('#btnSave').hide();
        $('#btnDelete').prop('disabled', false);
        $("#txtCompanyName").attr("placeholder", "Search factory name by pressing ↓ key");
        GetAutoCompleteComapny()
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtCompanyId').val('')
        ResetFields();
    }
    GetAutoCompleteComapny();
    //GetDates();

});

$('#chkEdit').on('change', function () {
    if ($('#chkEdit').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkDisplay').prop('checked', false);
        //GetAutoCompleteWorker()
        $('#btnSave').show();
        $('#btnSave').text('Update');
        GetAutoCompleteComapny();
        $("#txtCompanyName").attr("placeholder", "Search factory name by pressing ↓ key");
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#txtCompanyId').val('')
    }
    GetAutoCompleteComapny();
   // GetDates();
});

$('#txtEnrollementDate').change(function () {
    DateChange('txtEnrollementDate');
})

$('#txtOwnerCell').change(function () {
    maskMobileNumber('txtOwnerCell');
})
$('#txtAdminHeadCell').change(function () {
    maskMobileNumber('txtAdminHeadCell');
})

$('#txtHRHeadCell').change(function () {
    maskMobileNumber('txtHRHeadCell');
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

function GetCompanyText() {

    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        NewWorker: Type,
        SearchText: $("#txtCompanyName").val()
    }
    return Model;
}

function GetAutoCompleteComapny() {
    if ($('#chkNew').is(':checked') == true) return;
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
                document.getElementById("txtCompanyId").value = selectedId;
                GetCompanyModal($('#txtCompanyId').val());


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

function GetModel() {
    var now = new Date($('#txtEnrollementDate').val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    var ImageList = [];
    $("#tbl_CompanyInfo tr:gt(0)").each(function () {
        this_row = $(this);
        var Object = {
            CompanyAutoId: $('#txtCompanyId').val() || 0,
            FileName: $.trim(this_row.find('td:eq(1)').html()),//td:eq(0) means first td of this row
            FileSize: $.trim(this_row.find('td:eq(2)').html()),
            FileType: $.trim(this_row.find('td:eq(3)').html()),
            CompanyPicture: $.trim(this_row.find('td:eq(5)').html()),
            IsSaved: $.trim(this_row.find('td:eq(6)').html()),
            CaptureRemarks: $.trim(this_row.find('td:eq(7)').html()),
            CaptureDate: FinalDate

        }
        ImageList.push(Object);
    });
    return ImageList;
}



function GetCompanyModal(Id) {
    if (Id == '' || Id == 0) return;
    $('#tblDetail').empty();
    $.ajax({
        url: '/Factory/Company/GetById/' + Id,
        method: 'get',
        dataType: 'json',
        success: function (result) {
            console.log("asdasd" ,result);
            SetValues(result)
            CreateTableForSavedImages(result.imageList);
        }
    })
}

function CreateTableForSavedImages(obj) {
    console.log("Detail", obj);
    for (var i = 0; i < obj.length; i++) {
        if (obj[i]["companyImageAutoId"] > 0) {
            j = i + 1;
            var img1 = '<a href="' + obj[i]["companyPicture"] + '"><img style="width:110px;height:100px" src="' + obj[i]["companyPicture"] + '"/></a>';
            let
                tr = "<tr data-tranID=" + obj[i]["companyImageAutoId"] + " data-tranDetailID=" + i + " >";
            tr = tr + "<td scope=row>" + j + "</td>";
            tr = tr + "<td scope=row>" + $('#txtCompanyName').val() + "</td>";
            tr = tr + "<td scope=row>" + obj[i]["fileSize"] + "</td>";
            tr = tr + "<td scope=row>" + obj[i]["fileType"] + "</td>";
            tr = tr + "<td scope=row>" + img1 + "</td>";
            tr = tr + "<td scope=row hidden>" + $('#profileAvatar').attr('src') + "</td>";
            tr = tr + "<td hidden>" + true + "</td>";
            tr = tr + "<td >" + obj[i]["captureRemarks"] + "</td>";
            tr = tr + '<td style="margin-left:-50px;width=150px;"><button onclick="RemoveById(' + obj[i]["companyImageAutoId"] + ',this)" class="btn btn-danger"><i class="fa fa-remove"></i>&nbsp;&nbsp;Remove</button></td>';
            tr = tr + "</tr>";
            $('#tblDetail').append(tr);
        }
    }
}

function SetValues(object) {
    $('#txtCompanyId').val(object.companyAutoId);
    $('#txtCompanyCode').val(object.companyCode);
    $('#txtCompanyName').val(object.companyName);
    $('#txtWebsite').val(object.website);
    $('#txtAddress1').val(object.address1);
    $('#txtAddress2').val(object.address2);
    $('#txtAddress3').val(object.address3);
    $('#txtDistrict').val(object.district);
    $('#txtTown').val(object.town);
    $('#txtCity').val(object.city);
    $('#txtWorkForce').val(object.workForce);
    $('#txtOwnerName').val(object.ownerName);
    $('#txtOwnerCell').val(object.ownerMobile);
    $('#txtOwnerEmail').val(object.ownerEmail);
    $('#txtAdminHead').val(object.adminHeadName);
    $('#txtAdminHeadCell').val(object.adminHeadMobile);
    $('#txtAdminHeadEmail').val(object.adminHeadEmail);
    $('#txtHRHead').val(object.hrHeadName);
    $('#txtHRHeadCell').val(object.hrHeadMobile);
    $('#txtHRHeadEmail').val(object.hrHeadEmail);
    $('#txtEnrollementDate').val(object.enrollementDate.substr(0, 10));
    $('#txtEnrollementDate').val(object.viewDate);
    $('#txtEnrollementDateCustom').val(object.viewDate);
}

function onActionButtonClicked(actionName, data) {
    /*console.log(actionName);*/
    switch (actionName.toLowerCase()) {
        case 'edit':
            window.location = "/Factory/Company/Add/" + data[0];
            break;
        case 'delete':
            _ConfirmAjaxRequest('Delete', 'Do you want to Delete?', "/Factory/Company/Delete/" + data[0], null, 'json', 'Delete', '/Factory/Company/GetCompanyList');
            break;

    }
}

$('#btn_AddImage').click(function () {
    var divClass = ".fileupload-preview fileupload-exists thumbnail"; // Replace with the class name of your <div>
    //    console.log(imgElements);
    var imgElements = $(divClass).find("img");
    imgElements.each(function () {
        var src = $(this).attr("src");
    });
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
function CaptureSnapshot() {
    Webcam.snap(function (data) {
        console.log(data);
        img = document.getElementById('profileAvatar')
        img.src = data

        // Send image data to the controller to store locally or in database

        $('#TypeFile').val(null);
        $('#txtFileName').val($('#txtCompanyName').val());
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
        $("#txtFileName").val($('#txtCompanyName').val());
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


function remove(obj) {
    console.log(obj);
    $(obj).closest('tr').remove();
    resetRow();

}
function RemoveById(id, obj) {
    
    $.confirm({
        title: 'Delete',
        content: 'Are you sure',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Factory/Company/DeleteByImage/' + id,
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


$('#btnSave').click(function () {
    console.log(GetModel());
    var now = new Date($('#txtEnrollementDate').val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    if (!validate()) return;
    var Model = {
        EnrollementDate: FinalDate,
        CompanyAutoId: $('#txtCompanyId').val() || 0,
        CompanyCode: $('#txtCompanyCode').val() || '',
        CompanyName: $('#txtCompanyName').val(),
        Website: $('#txtWebsite').val(),
        Address1: $('#txtAddress1').val(),
        Address2: $('#txtAddress2').val(),
        Address3: $('#txtAddress3').val(),
        District: $('#txtDistrict').val(),
        Town: $('#txtTown').val(),
        City: $('#txtCity').val(),
        WorkForce: $('#txtWorkForce').val(),
        OwnerName: $('#txtOwnerName').val(),
        OwnerMobile: $('#txtOwnerCell').val(),
        OwnerEmail: $('#txtOwnerEmail').val(),
        AdminHeadName: $('#txtAdminHead').val(),
        AdminHeadMobile: $('#txtAdminHeadCell').val(),
        AdminHeadEmail: $('#txtAdminHeadEmail').val(),
        HRHeadName: $('#txtHRHead').val(),
        HRHeadMobile: $('#txtHRHeadCell').val(),
        HRHeadEmail: $('#txtHRHeadEmail').val(),
        Cellphone: $('#txtCell').val(),
        ImageList: GetModel(),
    }
    console.log("Model", Model);
    var Title = "Save", Content = "Do you want to save?";
    if ($('#txtCompanyId').val() > 0) {
        Title = "Update";
        Content = "Do you want to update?";
    }
    //var response = _ConfirmAjaxRequest(title, content, "/Factory/Company/SaveUpdate", Model, 'json', 'post', null, 'tblDetail');
    $.confirm({
        title: Title,
        content: Content,
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Factory/Company/SaveUpdate',
                    data: Model,
                    method: 'post',
                    dataType: 'json',
                    success: function (result) {
                        //console.log(result);
                        if (result.toLowerCase().includes('successfully')) {
                            __MessageBox("Saved", "Saved :" + result, "green", "Ok", function () { });
                            ResetFields();
                            setTimeout(function () {
                                CheckWebCam();
                            }, 1000);
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
    

    console.log(response);

    GetDate('txtEnrollementDate');
});

function validate() {
    var Owner = 0,HeadAdmin=0,HR=0;
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

    if ($('#txtAddress1').val() == '' && $('#txtAddress2').val() == '' && $('#txtAddress3').val() == '' ) {
        returnVal = false;
        AddVAlidationToControl("txtAddress1", "show", "Atleast one Address is required", "top")
        AddVAlidationToControl("txtAddress2", "show", "", "top")
        AddVAlidationToControl("txtAddress3", "show", "", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtAddress1", "hide", "", "top")
        AddVAlidationToControl("txtAddress2", "hide", "", "top")
        AddVAlidationToControl("txtAddress3", "hide", "", "top")
    }
    if ($('#txtCity').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtCity", "show", "City is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtCity", "hide", "", "top")
    }


    // Validation for Owner Information
    //if ( (($('#txtOwnerName').val() == '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') && 
    //    ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() == '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() == '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() == '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() == '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() == ''))
    //                                  //Single row Validateion End
    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() == ''))
    //                    //Double row validation End

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //    ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //     ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() == ''))
    //                   //Double row Dobule Column validation End

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||

    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||

    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() == ''))
    //    ||

    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() == ''))
    //    ||

    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() != '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() != '') &&
    //        ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() != ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() != ''))

    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //    ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() != ''))

    //    ||
    //        (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() == '' && $('#txtOwnerEmail').val() != '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() != '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() != '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() == '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() == '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() == '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() == ''))
    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() != '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() == '') &&
    //        ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() != ''))
    //    ||
    //    (($('#txtOwnerName').val() != '' && $('#txtOwnerCell').val() != '' && $('#txtOwnerEmail').val() != '') &&
    //        ($('#txtAdminHead').val() == '' && $('#txtAdminHeadCell').val() == '' && $('#txtAdminHeadEmail').val() != '') &&
    //        ($('#txtHRHead').val() == '' && $('#txtHRHeadCell').val() != '' && $('#txtHRHeadEmail').val() != ''))
    //)
    if ($('#txtOwnerName').val().trim() == '' || $('#txtOwnerCell').val().trim() == '' || $('#txtOwnerEmail').val().trim() == '') {
        Owner = 1;
    }
    if ($('#txtAdminHead').val().trim() == '' || $('#txtAdminHeadCell').val() == '' || $('#txtAdminHeadEmail').val().trim() == '') {
        HeadAdmin = 1;
    }
    if ($('#txtHRHead').val().trim() == '' || $('#txtHRHeadCell').val().trim() == '' || $('#txtHRHeadEmail').val().trim() == '') {
        HR = 1;
    }
    if (Owner ==1 && HR == 1 && HeadAdmin == 1)
    {
        console.log(1, 'Owner', Owner, 'HR: ', HR,"HeadAdmin: ",HeadAdmin);
        returnVal = false;
        AddVAlidationToControl("txtOwnerName", "show", "Atleast One Responsible Person complete information Required", "top")
        AddVAlidationToControl("txtOwnerCell", "show", "", "top")
        AddVAlidationToControl("txtOwnerEmail", "show", "", "top")
    }
    else {
        AddVAlidationToControl("txtOwnerName", "hide", "", "top")
        AddVAlidationToControl("txtOwnerCell", "hide", "", "top")
        AddVAlidationToControl("txtOwnerEmail", "hide", "", "top")
      
    }
    if (HeadAdmin == 1 && HR == 1 && Owner == 1) {
        console.log(2, 'Owner', Owner, 'HR: ', HR, "HeadAdmin: ", HeadAdmin);
        returnVal = false;
        AddVAlidationToControl("txtAdminHead", "show", "", "top")
        AddVAlidationToControl("txtAdminHeadCell", "show", "", "top")
        AddVAlidationToControl("txtAdminHeadEmail", "show", "", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtAdminHead", "hide", "", "top")
        AddVAlidationToControl("txtAdminHeadCell", "hide", "", "top")
        AddVAlidationToControl("txtAdminHeadEmail", "hide", "", "top")
    }
    if (HR == 1 && HeadAdmin == 1 && Owner == 1) {
        console.log(3, 'Owner', Owner, 'HR: ', HR, "HeadAdmin: ", HeadAdmin);
        returnVal = false;
        AddVAlidationToControl("txtHRHead", "show", "", "top")
        AddVAlidationToControl("txtHRHeadCell", "show", "", "top")
        AddVAlidationToControl("txtHRHeadEmail", "show", "", "top")

    }
    else {
        AddVAlidationToControl("txtHRHead", "hide", "", "top")
        AddVAlidationToControl("txtHRHeadCell", "hide", "", "top")
        AddVAlidationToControl("txtHRHeadEmail", "hide", "", "top")
    }

    if ($('#btn_AddImage').is(":visible")) {
        $.alert('Kindly click Add Image button for saveing the image', 'Alert');
        returnVal = false;
        $(window).scrollTop(0);
    }
    console.log(5, 'Owner', Owner, 'HR: ', HR, "HeadAdmin: ", HeadAdmin);
    return returnVal;

}


$('#btnAdd').click(function () {
    window.location.href = 'Add/0';
});

$('#btnRefresh').click(function () {
    window.location.reload(true);
})
$('#btnBack').click(function () {
    window.location = "/Factory/Company/list";
});

$('#btnDelete').click(function () {
    if ($('#txtCompanyId').val() != '' && $('#txtCompanyId').val() != 'select' && $('#txtCompanyId').val() != '0') {
        $.confirm({
            title: 'Delete',
            content: 'Are you sure',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: '/Factory/Company/Delete/' + $('#txtCompanyId').val(),
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
function ResetCompanyAutoComplete() {
    $("#txtCompanyName").autocomplete({ source: [] });
}

$('#chkCompanies').on('change', function () {

    if ($('#chkCompanies').is(":checked")) {
        UncheckedOther('chkCompanies');
        window.location.assign(window.location.origin + "/Factory/Company/Add/0");
    }
})
$('#chkGoth').on('change', function () {

    if ($('#chkGoth').is(":checked")) {
        UncheckedOther('chkGoth');
        window.location.assign(window.location.origin + "/Goths/Add/0");
    }
})
$('#chkLocalities').on('change', function () {

    if ($('#chkLocalities').is(":checked")) {
        UncheckedOther('chkLocalities');
        window.location.assign(window.location.origin + "/Localities/Add/0");
    }
})
$('#chkPublicSpaces').on('change', function () {

    if ($('#chkPublicSpaces').is(":checked")) {
        UncheckedOther('chkPublicSpaces');
        window.location.assign(window.location.origin + "/PublicSpaces/Add/0");
    }
})
