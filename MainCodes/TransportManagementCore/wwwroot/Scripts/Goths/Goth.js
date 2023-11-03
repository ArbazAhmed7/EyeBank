var imagePath = '~/img/noimage.png';
$(document).ready(function () {
    if (!window.location.href.includes("list")) {
        GetDate('txtEnrollementDate');
        //CustomDate('txtEnrollementDate');
        //var now = new Date();
        //var day = ("0" + now.getDate()).slice(-2);
        //var month = ("0" + (now.getMonth() + 1)).slice(-2);
        //var today = now.getFullYear() + "-" + (month) + "-" + (day);

        //$('#txtEnrollementDate').val(today);
        //$('#txtEnrollementDate').hide();
        $('#txtGothName').focus();
        if ($('#txtGothId').val() != "0" && $('#txtGothId').val() != "") {
            GetGothModal($('#txtGothId').val());
        }
        else {
            GetDate('txtEnrollementDate');
        }
        if ($('#txtLoginId').val() != null && $('#txtLoginId').val() != "")
            console.log("Session value: ", $('#txtLoginId').val());

        $('#chkNew').prop('checked', true);
        $('#chkNew').change();
        $('#chkGoth').prop('checked', true);
        $('#autocomplete-input-Goth').change();
        $('#autocomplete-input-Goth').focus();
        CheckWebCam();
    }
})
function GetGothModal(GothId) {
    /*  alert(GothId)*/
    if (GothId == '' || GothId == 0) return;
    $('#tblDetail').empty();
    $.ajax({
        url: '/Goths/GetById/' + GothId,
        method: 'get',
        dataType: 'json',
        success: function (result) {
            console.log("asdasd", result);
            SetValues(result)
            CreateTableForSavedImages(result.imageList);
        }
    })

}
function ResetFields() {
    $('#txtGothId').val('');
    $('#txtGothName').val('');
    $('#txtWebsite').val('');
    $('#txtAddress1').val('');
    $('#txtAddress2').val('');
    $('#txtAddress3').val('');
    $('#txtTown').val('');
    $('#txtDistrict').val('');
    $('#txtCity').val('');
    $('#txtWorkForce').val('');
    $('#txtPersonName').val('');
    $('#txtPersonCell').val('');
    $('#txtPersonRole').val('');
    $('#txtCaptureRemarks').val('');
    $('#chkWebCam').prop('checked', false);
    $('#tblDetail').empty();
    $("#chkWebCam").change();

}

$('#chkNew').on('change', function () {
    if ($('#chkNew').is(':checked')) {
        $('#chkDisplay').prop('checked', false);
        $('#chkEdit').prop('checked', false);
        //ResetWorkerValue();
        ResetFields();
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtGothId').val('');
        $('#btnSave').show();
        $('#btnSave').text('Save');
        $("#txtGothName").attr("placeholder", "");
        $('#btnDelete').prop('disabled', true);
        ResetGothAutoComplete();
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        ResetFields();
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtGothId').val('')
    }
    GetAutoCompleteGoth();
    //GetEnrollmentDate();
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
        $("#txtGothName").attr("placeholder", "Search Goth name by pressing ↓ key");
        GetAutoCompleteGoth()
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#DivlblVisitDate').show();
        $('#DivinputVisitDate').show();
        $('#txtGothId').val('')
        ResetFields();
    }
    GetAutoCompleteGoth();
    //GetEnrollmentDate();

});

$('#chkEdit').on('change', function () {
    if ($('#chkEdit').is(':checked')) {
        $('#chkNew').prop('checked', false);
        $('#chkDisplay').prop('checked', false);
        //GetAutoCompleteWorker()
        $('#btnSave').show();
        $('#btnSave').text('Update');
        $('#btnDelete').prop('disabled', false);
        GetAutoCompleteGoth();
        $("#txtGothName").attr("placeholder", "Search Goth name by pressing ↓ key");
    }
    if ((!$('#chkEdit').is(':checked') && !$('#chkDisplay').is(':checked') && !$('#chkNew').is(':checked'))) {
        $('#chkNew').prop('checked', true);
        $('#txtGothId').val('')
    }
    GetAutoCompleteGoth();
    // GetEnrollmentDate();
});

$('#txtEnrollementDate').change(function () {
    DateChange('txtEnrollementDate');
})

$('#txtPersonCell').change(function () {
    maskMobileNumber('txtPersonCell');
}) 

$("#txtGothName").keydown(function (e) {
    var keyCode = e.keyCode || e.which;
    //Regex for Valid Characters i.e. Alphabets and Numbers.
    var regex = /^[A-Za-z0-9]+$/;
    //Validate TextBox value against the Regex.
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 39 || e.keyCode == 40) {
        return;
    }
    if (isValid) {
        GetAutoCompleteGoth();
    }
})

function GetGothText() {

    var Type = true;
    if ($('#chkEdit').is(':checked') == true || $('#chkDisplay').is(':checked') == true)
        Type = false;
    var Model = {
        New: Type,
        SearchText: $("#txtGothName").val()
    }
    return Model;
}

function GetAutoCompleteGoth() {
    if ($('#chkNew').is(':checked') == true) return;
    Goth = [];
    $.ajax({
        url: '/DropDownLookUp/Help/GetGoths',
        data: GetGothText(),
        method: 'get',
        dataType: 'json',
        success: function (result) {
            Goth = result;
        }
    })

    $("#txtGothName").autocomplete({
        source: function (request, response) {
            var term = request.term.toLowerCase();
            // Filter the item list based on the search term
            var filteredList = $.grep(Goth, function (item) {
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
            
            setTimeout(function () {
                document.getElementById("txtGothName").value = selectedName;
                document.getElementById("txtGothCode").value = code;
                document.getElementById("txtGothId").value = selectedId;
                GetGothModal($('#txtGothId').val());


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
 
    var ImageList = [];
    $("#tbl_GothInfo tr:gt(0)").each(function () {
        this_row = $(this);
        var Object = {
            GothAutoId: $('#txtGothId').val() || 0,
            FileName: $.trim(this_row.find('td:eq(1)').html()),//td:eq(0) means first td of this row
            FileSize: $.trim(this_row.find('td:eq(2)').html()),
            FileType: $.trim(this_row.find('td:eq(3)').html()),
            GothPicture: $.trim(this_row.find('td:eq(5)').html()),
            IsSaved: $.trim(this_row.find('td:eq(6)').html()),
            CaptureRemarks: $.trim(this_row.find('td:eq(7)').html()),
            CaptureDate: GetEnrollmentDate()

        }
        ImageList.push(Object);
    });
    return ImageList;
}



function GetGothModal(Id) {
    if (Id == '' || Id == 0) return;
    $('#tblDetail').empty();
    $.ajax({
        url: '/Goths/GetById/' + Id,
        method: 'get',
        dataType: 'json',
        success: function (result) {
            console.log("asdasd", result);
            SetValues(result)
            CreateTableForSavedImages(result.imageList);
        }
    })
}

function CreateTableForSavedImages(obj) {
    console.log("Detail", obj);
    for (var i = 0; i < obj.length; i++) {
        if (obj[i]["gothImageAutoId"] > 0) {
            j = i + 1;
            var img1 = '<a href="' + obj[i]["GothPicture"] + '"><img style="width:110px;height:100px" src="' + obj[i]["gothPicture"] + '"/></a>';
            let
                tr = "<tr data-tranID=" + obj[i]["gothImageAutoId"] + " data-tranDetailID=" + i + " >";
            tr = tr + "<td scope=row>" + j + "</td>";
            tr = tr + "<td scope=row>" + $('#txtGothName').val() + "</td>";
            tr = tr + "<td scope=row>" + obj[i]["fileSize"] + "</td>";
            tr = tr + "<td scope=row>" + obj[i]["fileType"] + "</td>";
            tr = tr + "<td scope=row>" + img1 + "</td>";
            tr = tr + "<td scope=row hidden>" + $('#profileAvatar').attr('src') + "</td>";
            tr = tr + "<td hidden>" + true + "</td>";
            tr = tr + "<td >" + obj[i]["captureRemarks"] + "</td>";
            tr = tr + '<td style="margin-left:-50px;width=150px;"><button onclick="RemoveById(' + obj[i]["gothImageAutoId"] + ',this)" class="btn btn-danger"><i class="fa fa-remove"></i>&nbsp;&nbsp;Remove</button></td>';
            tr = tr + "</tr>";
            $('#tblDetail').append(tr);
        }
    }
}

function SetValues(object) {
    $('#txtGothId').val(object.gothAutoId);
    $('#txtGothCode').val(object.gothCode);
    $('#txtGothName').val(object.gothName);
    $('#txtWebsite').val(object.website);
    $('#txtAddress1').val(object.address1);
    $('#txtAddress2').val(object.address2);
    $('#txtAddress3').val(object.address3);
    $('#txtDistrict').val(object.district);
    $('#txtTown').val(object.town);
    $('#txtCity').val(object.city);
    $('#txtPersonName').val(object.nameofPerson);
    $('#txtPersonCell').val(object.personMobile);
    $('#txtPersonRole').val(object.personRole); 
    $('#txtEnrollementDate').val(object.enrollementDate.substr(0, 10));
    $('#txtEnrollementDate').val(object.viewDate);
    $('#txtEnrollementDateCustom').val(object.viewDate);
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
        $('#txtFileName').val($('#txtGothName').val());
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
        $("#txtFileName").val($('#txtGothName').val());
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
    
    //_ConfirmAjaxRequest('Delete', 'Do you want to Delete?', "/Goth/DeleteByImage/" + id, null, 'json', 'Delete', null, null, '/Goth/Add/' + RefreshId);
    
    $.confirm({
        title: 'Delete',
        content: 'Are you sure',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Goths/DeleteByImage/' + id,
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
    //var now = new Date($('#txtEnrollementDate').val());
    //var day = ("0" + now.getDate()).slice(-2);
    //var month = ("0" + (now.getMonth() + 1)).slice(-2);
    //var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    if (!validate()) return;
    var Model = {
        EnrollementDate: GetEnrollmentDate(),
        GothAutoId: $('#txtGothId').val() || 0,
        GothCode: $('#txtGothCode').val() || '',
        GothName: $('#txtGothName').val(),
        Website: $('#txtWebsite').val(),
        Address1: $('#txtAddress1').val(),
        Address2: $('#txtAddress2').val(),
        Address3: $('#txtAddress3').val(),
        District: $('#txtDistrict').val(),
        Town: $('#txtTown').val(),
        City: $('#txtCity').val(),
        NameofPerson: $('#txtPersonName').val(),
        PersonMobile: $('#txtPersonCell').val(),
        PersonRole: $('#txtPersonRole').val(),
        ImageList: GetModel(),
    }
    console.log("Model", Model);
    var Title = "Save", Content = "Do you want to save?";
    if ($('#txtGothId').val() > 0) {
        Title = "Update";
        Content = "Do you want to update?";
    }
    //var response = _ConfirmAjaxRequest(title, content, "/Factory/Goth/SaveUpdate", Model, 'json', 'post', null, 'tblDetail');
    $.confirm({
        title: Title,
        content: Content,
        buttons: {
            confirm: function () {
                $.ajax({
                    url: '/Goths/SaveUpdate',
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

function GetEnrollmentDate() {
    var now = new Date($('#txtEnrollementDate').val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var FinalDate = now.getFullYear() + "-" + (month) + "-" + (day);
    return FinalDate;
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

    if ($('#txtGothName').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtGothName", "show", "Name is Required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtGothName", "hide", "", "bottom")
    }

    if ($('#txtAddress1').val() == '' && $('#txtAddress2').val() == '' && $('#txtAddress3').val() == '') {
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
    if ($('#txtPersonName').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtPersonName", "show", "City is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtPersonName", "hide", "", "top")
    }
    if ($('#txtPersonCell').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtPersonCell", "show", "City is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtPersonCell", "hide", "", "top")
    }
    if ($('#txtPersonRole').val() == '') {
        returnVal = false;
        AddVAlidationToControl("txtPersonRole", "show", "City is required", "top")
        $(window).scrollTop(0);
    }
    else {
        AddVAlidationToControl("txtPersonRole", "hide", "", "top")
    }
    if ($('#btn_AddImage').is(":visible")) {
        $.alert('Kindly click Add Image button for saveing the image', 'Alert');
        returnVal = false;
        $(window).scrollTop(0);
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
    window.location = "/Goths/list";
});

$('#btnDelete').click(function () {
    if ($('#txtGothId').val() != '' && $('#txtGothId').val() != 'select' && $('#txtGothId').val() != '0') {
        $.confirm({
            title: 'Delete',
            content: 'Are you sure',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: '/Goths/Delete/' + $('#txtGothId').val(),
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
function ResetGothAutoComplete() {
    $("#txtGothName").autocomplete({ source: [] });
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
