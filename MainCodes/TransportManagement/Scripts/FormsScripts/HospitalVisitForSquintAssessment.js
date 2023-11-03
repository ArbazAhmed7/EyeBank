function pageLoad() {
    /*EventsRegiter*/
    chkListSquintDiagRight_Click();
    chkListSquintDiagLeft_Click();
    chkRoutineCheckup_Click();
    chkFundoscopy_Click();
    chkSquintAssessment_Click();
    chkFurtherAssessment_Click();

    /*MethodsRegister*/
    ShowHidetxtOtherSquintDiagRight();
    ShowHidetxtOtherSquintDiagLeft();
    ShowHidetxtRoutineCheckupDate();
    ShowHidetxtFundoscopyDate();
    ShowHidetxtFurtherAssessmentDate();

    try {
        StudentCodeAutoComplete();
    } catch (e) {

    }

    try {
        StudentNameAutoComplete();
    } catch (e) {

    }

    try {
        SchoolCodeAutoComplete();
    } catch (e) {

    }

    try {
        SchoolNameAutoComplete();
    } catch (e) {

    }
}

/*Events*/
function chkListSquintDiagRight_Click() {
    $("[id$=chkListSquintDiagRight] input").on("click", function () {
        ShowHidetxtOtherSquintDiagRight();
    });
}

function chkListSquintDiagLeft_Click() {
    $("[id$=chkListSquintDiagLeft] input").on("click", function () {
        ShowHidetxtOtherSquintDiagLeft();
    });
}

function chkRoutineCheckup_Click() {
    $("[id$=chkRoutineCheckup]").on("click", function () {
        ShowHidetxtRoutineCheckupDate();
    });
}

function chkFundoscopy_Click() {
    $("[id$=chkFundoscopy]").on("click", function () {
        ShowHidetxtFundoscopyDate();
    });
}

function chkSquintAssessment_Click() {
    $("[id$=chkSquintAssessment]").on("click", function () {
        ShowHidetxtSquintAssessmentDate();
    });
}

function chkFurtherAssessment_Click() {
    $("[id$=chkFurtherAssessment]").on("click", function () {
        ShowHidetxtFurtherAssessmentDate();
    });
}


/*Methods*/
function ShowHidetxtOtherSquintDiagRight() {
    $("[id$=chkListSquintDiagRight] input").each(function () {
        if ($(this).is(":checked") && $(this).val() == 4) {
            $("[id$=DivtxtOtherSquintDiagRight]").show();
            $("[id$=txtOtherSquintDiagRight]").focus();
        }
        else if ($(this).is(":checked") == false && $(this).val() == 4) { // 4 is other
            $("[id$=DivtxtOtherSquintDiagRight]").hide();
        }
    });
}

function ShowHidetxtOtherSquintDiagLeft() {
    $("[id$=chkListSquintDiagLeft] input").each(function () {
        if ($(this).is(":checked") && $(this).val() == 4) {
            $("[id$=DivtxtOtherSquintDiagLeft]").show();
            $("[id$=txtOtherSquintDiagLeft]").focus();
        }
        else if ($(this).is(":checked") == false && $(this).val() == 4) {
            $("[id$=DivtxtOtherSquintDiagLeft]").hide();
        }
    });
}

function ShowHidetxtRoutineCheckupDate() {
    if ($("[id$=chkRoutineCheckup]").is(":checked")) {
        $("[id$=divtxtRoutineCheckupDate]").show();
        $("[id$=txtRoutineCheckupDate]").focus();
    }
    else {
        $("[id$=divtxtRoutineCheckupDate]").hide();
    }
}

function ShowHidetxtFundoscopyDate() {
    if ($("[id$=chkFundoscopy]").is(":checked")) {
        $("[id$=divtxtFundoscopyDate]").show();
        $("[id$=txtFundoscopyDate]").focus();
    }
    else {
        $("[id$=divtxtFundoscopyDate]").hide();
    }
}

function ShowHidetxtSquintAssessmentDate() {
    if ($("[id$=chkSquintAssessment]").is(":checked")) {
        $("[id$=divtxtSquintAssessmentDate]").show();
        $("[id$=txtSquintAssessmentDate]").focus();
    }
    else {
        $("[id$=divtxtSquintAssessmentDate]").hide();
    }
}

function ShowHidetxtFurtherAssessmentDate() {
    if ($("[id$=chkFurtherAssessment]").is(":checked")) {
        $("[id$=divtxtFurtherAssessmentDate]").show();
        $("[id$=txtFurtherAssessmentDate]").focus();
    }
    else {
        $("[id$=divtxtFurtherAssessmentDate]").hide();
    }
}

function StudentCodeAutoComplete() {
    $("[id$=txtStudentCode]")
        .blur(function () {
            var keyEvent = $.Event("keydown");
            keyEvent.keyCode = $.ui.keyCode.ENTER;
            $(this).trigger(keyEvent);
        })
        .autocomplete({
            autoFocus: true,
            source: function (request, response) {
                $.ajax({
                    url: "HospitalVisitForSquintAssessment.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentCode_SquintAssessment','Id' :'0'}",
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
                $("[id$=txtStudentCode]").val(ui.item.label);
                $("[id$=hfStudentIDPKID]").val(ui.item.val);
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
function StudentNameAutoComplete() {
    $("[id$=txtStudentName]")
        .blur(function () {
            var keyEvent = $.Event("keydown");
            keyEvent.keyCode = $.ui.keyCode.ENTER;
            $(this).trigger(keyEvent);
        })
        .autocomplete({
            autoFocus: true,
            source: function (request, response) {
                $.ajax({
                    url: "HospitalVisitForSquintAssessment.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentName_SquintAssessment','Id' :'0'}",
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
                $("[id$=txtStudentName]").val(ui.item.label);
                $("[id$=hfStudentIDPKID]").val(ui.item.val);
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
                    url: "HospitalVisitForSquintAssessment.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'SchoolCode_SquintAssessment','Id' :'0'}",
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
function SchoolNameAutoComplete() {
    $("[id$=txtSchoolName]")
        .blur(function () {
            var keyEvent = $.Event("keydown");
            keyEvent.keyCode = $.ui.keyCode.ENTER;
            $(this).trigger(keyEvent);
        })
        .autocomplete({
            autoFocus: true,
            source: function (request, response) {
                $.ajax({
                    url: "HospitalVisitForSquintAssessment.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'SchoolName_SquintAssessment','Id' :'0'}",
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