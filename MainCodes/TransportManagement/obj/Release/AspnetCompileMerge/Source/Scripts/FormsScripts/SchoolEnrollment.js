function pageLoad()
{
    ImageUploadlabelUpdate();

    try {
        SchoolCodeAutoComplete();
    } catch (e) {

    }

    try {
        SchoolNameAutoComplete();
    } catch (e) {

    }
    
}

function SchoolCodeAutoComplete() {
    $("[id$=txtSchoolCode]")
        .blur(function () {
            var keyEvent = $.Event("keydown");
            keyEvent.keyCode = $.ui.keyCode.ENTER;
            $(this).trigger(keyEvent);
        })
        .autocomplete({
        autoFocus: true,
        source: function (request, response) {
            $.ajax({
                url: "SchoolEnrollment.aspx/AutoComplete",
                data: "{'Term' :'" + request.term + "','TermType' :'SchoolCode','Id' :'0'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    console.log(data);
                    response($.map(data.d, function (item) {

                        return {
                            label: item.label,
                            val: item.val,
                            json: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        focus: function (event, ui) {
            // this is for when focus of autocomplete item 
            //$("[id$=txtUseID]").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            // this is for when select autocomplete item
            $("[id$=txtSchoolCode]").val(ui.item.label);
            $("[id$=hfSchoolIDPKID]").val(ui.item.val);
            //window.location.reload(true);
            __doPostBack('UpdatePanel1', '');
            return false;
        }
    }).data("ui-autocomplete")._renderItem = function (ul, item) {
        // here return item for autocomplete text box, Here is the place 
        // where we can modify data as we want to show as autocomplete item
        return $("<li>").append(item.label).appendTo(ul);
    };
}
function SchoolNameAutoComplete()
{
    $("[id$=txtSchoolName]")
        .blur(function () {
            var keyEvent = $.Event("keydown");
            keyEvent.keyCode = $.ui.keyCode.ENTER;
            $(this).trigger(keyEvent);
        })
        .autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "SchoolEnrollment.aspx/AutoComplete",
                data: "{'Term' :'" + request.term + "','TermType' :'SchoolName','Id' :'0'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    console.log(data);
                    response($.map(data.d, function (item) {

                        return {
                            label: item.label,
                            val: item.val,
                            json: item
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        focus: function (event, ui) {
            // this is for when focus of autocomplete item 
            //$("[id$=txtUseID]").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            // this is for when select autocomplete item
            $("[id$=txtSchoolName]").val(ui.item.label);
            $("[id$=hfSchoolIDPKID]").val(ui.item.val);
            //window.location.reload(true);
            __doPostBack('UpdatePanel1', '');
            return false;
        }
    }).data("ui-autocomplete")._renderItem = function (ul, item) {
        // here return item for autocomplete text box, Here is the place 
        // where we can modify data as we want to show as autocomplete item
        return $("<li>").append(item.label).appendTo(ul);
    };
}

function ShowPopupAfterSaveConfirmation(title, body) {
    $("#PopupAfterSaveConfirmation .modal-title").html(title);
    $("#PopupAfterSaveConfirmation .modal-body").html(body);
    $("#PopupAfterSaveConfirmation").modal("show");
}

function HideBootstrapModal() {
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove(); $('#Div3').hide();
}

 
function ImageUploadlabelUpdate() {
    if ($("[id$=hfImageBytes]").val()) {
        $('[id$=lblFileUploadStudent]').text('File chosen.');
    }
    else {
        $('[id$=lblFileUploadStudent]').text('Choose file.');
    }
}

function validateInput() {

    var valRes = true;

    if ($("[id$=txtTestDate]").val().trim() == "") {
        $("[id$=txtTestDate]").attr("style", "border: red 1px solid;")
        valRes = false;
    }
    else {
        $("[id$=txtTestDate]").removeAttr("style");
    }

    if ($("[id$=txtSchoolCode]").val().trim() == "") {
        $("[id$=txtSchoolCode]").attr("style", "border: red 1px solid;")
        valRes = false;
    }
    else {
        $("[id$=txtSchoolCode]").removeAttr("style");
    }

    if ($("[id$=txtSchoolName]").val().trim() == "") {
        $("[id$=txtSchoolName]").attr("style", "border: red 1px solid;")
        valRes = false;
    }
    else {
        $("[id$=txtSchoolName]").removeAttr("style");
    }

    if ($("[id$=txtClassNo]").val().trim() == "") {
        $("[id$=txtClassNo]").attr("style", "border: red 1px solid;")
        valRes = false;
    }
    else {
        $("[id$=txtClassNo]").removeAttr("style");
    }

    if ($("[id$=txtStudentName]").val().trim() == "") {
        $("[id$=txtStudentName]").attr("style", "border: red 1px solid;")
        valRes = false;
    }
    else {
        $("[id$=txtStudentName]").removeAttr("style");
    }

    if ($("[id$=txtAge]").val().trim() == "") {
        $("[id$=txtAge]").attr("style", "border: red 1px solid;")
        valRes = false;
    }
    else {
        $("[id$=txtAge]").removeAttr("style");
    }

    if ($("[id$=txtFatherName]").val().trim() == "") {
        $("[id$=txtFatherName]").attr("style", "border: red 1px solid;")
        valRes = false;
    }
    else {
        $("[id$=txtFatherName]").removeAttr("style");
    }

    var ddlGender = document.getElementById("ddlGender");
    if (ddlGender.value == "0") {
        $("[id$=ddlGender]").attr("style", "border: red 1px solid;")
        valRes = false;
    }
    else {
        $("[id$=ddlGender]").removeAttr("style");
    }

    if (!valRes) {
        $("[id$=lbl_error]").text('* Mandatory');
    }
    else {
        $("[id$=lbl_error]").text('');
    }

    return valRes;
}