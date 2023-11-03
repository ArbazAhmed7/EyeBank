function pageLoad() {
    
    chkListDiagRight_Click(); 
    chkListDiagLeft_Click();

    chkListDiag2Right_Click();
    chkListDiag2Left_Click();

    chkRoutineCheckup_Click();
    chkFurtherAssessment_Click();
    chkSurgeryFollowup_Click();

    ShowHidetxtRoutineCheckupDate();
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

 

function chkListDiagRight_Click() {      
    $("[id$=chkListDiagRight] input").on("click", function () {
        $("[id$=chkListDiagRight] input").each(function () {
            if ($(this).is(":checked") && $(this).val() == 13) {
                $("[id$=DivtxtOtherDiagRight]").show();
                $("[id$=txtOtherDiagRight]").focus();
            }
            else if ($(this).is(":checked") == false && $(this).val() == 13) { // 12 is other
                $("[id$=DivtxtOtherDiagRight]").hide();
            }
        });
    });  
}

function chkListDiagLeft_Click() {
    $("[id$=chkListDiagLeft] input").on("click", function () {
        $("[id$=chkListDiagLeft] input").each(function () {
            if ($(this).is(":checked") && $(this).val() == 13) {
                $("[id$=DivtxtOtherDiagLeft]").show();
                $("[id$=txtOtherDiagLeft]").focus();
            }
            else if ($(this).is(":checked") == false && $(this).val() == 13) {
                $("[id$=DivtxtOtherDiagLeft]").hide();
            }
        });
    });
}

function chkListDiag2Right_Click() {
    $("[id$=chkListDiag2Right] input").on("click", function () {
        $("[id$=chkListDiag2Right] input").each(function () {
            if ($(this).is(":checked") && $(this).val() == 8) {
                $("[id$=DivtxtOtherDiag2Right]").show();
                $("[id$=txtOtherDiag2Right]").focus();
            }
            else if ($(this).is(":checked") == false && $(this).val() == 8) { //8 is other
                $("[id$=DivtxtOtherDiag2Right]").hide();
            }
        });
    });
}

function chkListDiag2Left_Click() {
    $("[id$=chkListDiag2Left] input").on("click", function () {
        $("[id$=chkListDiag2Left] input").each(function () {
            if ($(this).is(":checked") && $(this).val() == 8) {
                $("[id$=DivtxtOtherDiag2Left]").show();
                $("[id$=txtOtherDiag2Left]").focus();
            }
            else if ($(this).is(":checked") == false && $(this).val() == 8) {
                $("[id$=DivtxtOtherDiag2Left]").hide();
            }
        });
    });
}

function chkRoutineCheckup_Click() {    
    $("[id$=chkRoutineCheckup]").on("click", function () {
        ShowHidetxtRoutineCheckupDate();
    });
}

function chkFurtherAssessment_Click() {
    $("[id$=chkFurtherAssessment]").on("click", function () {
        ShowHidetxtFurtherAssessmentDate();
    });
}

function chkSurgeryFollowup_Click() {
    $("[id$=chkSurgery]").on("click", function () {
        ShowHidetxtSurgeryDate();
    });
}

function ShowHidetxtRoutineCheckupDate()
{
    if ($("[id$=chkRoutineCheckup]").is(":checked")) {
        $("[id$=divtxtRoutineCheckupDate]").show();
        $("[id$=txtRoutineCheckupDate]").focus();
    }
    else {
        $("[id$=divtxtRoutineCheckupDate]").hide();
    }
}

function ShowHidetxtFurtherAssessmentDate()
{
    if ($("[id$=chkFurtherAssessment]").is(":checked")) {
        $("[id$=divtxtFurtherAssessmentDate]").show();
        $("[id$=txtFurtherAssessmentDate]").focus();
    }
    else {
        $("[id$=divtxtFurtherAssessmentDate]").hide();
    }
}

function ShowHidetxtSurgeryDate() {
    if ($("[id$=chkSurgery]").is(":checked")) {
        $("[id$=divtxtSurgeryDate]").show();
        $("[id$=txtSurgeryDate]").focus();
    }
    else {
        $("[id$=divtxtSurgeryDate]").hide();
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
                    url: "HospitalVisitForFundoscopy.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentCode_Fundoscopy','Id' :'0'}",
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
                    url: "HospitalVisitForFundoscopy.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentName_Fundoscopy','Id' :'0'}",
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
                    url: "HospitalVisitForFundoscopy.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'SchoolCode_Fundoscopy','Id' :'0'}",
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
                    url: "HospitalVisitForFundoscopy.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'SchoolName_Fundoscopy','Id' :'0'}",
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