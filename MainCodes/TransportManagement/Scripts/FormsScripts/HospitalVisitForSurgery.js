function pageLoad() {
    /*EventsRegiter*/
    chkListSurgeryRight_Click();
    chkListSurgeryLeft_Click();
 

    /*MethodsRegister*/
    ShowHidetxtOtherSurgeryRight();
    ShowHidetxtOtherSurgeryLeft();

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
function chkListSurgeryRight_Click() {
    $("[id$=chkListSurgeryRight] input").on("click", function () {
        ShowHidetxtOtherSurgeryRight();
    });
}

function chkListSurgeryLeft_Click() {
    $("[id$=chkListSurgeryLeft] input").on("click", function () {
        ShowHidetxtOtherSurgeryLeft();
    });
}
 

/*Methods*/
function ShowHidetxtOtherSurgeryRight() {
    $("[id$=chkListSurgeryRight] input").each(function () {
        if ($(this).is(":checked") && $(this).val() == 8) {
            $("[id$=DivtxtOtherSurgeryRight]").show();
            $("[id$=txtOtherSurgeryRight]").focus();
        }
        else if ($(this).is(":checked") == false && $(this).val() == 8) { // 8 is other
            $("[id$=DivtxtOtherSurgeryRight]").hide();
        }
    });
}

function ShowHidetxtOtherSurgeryLeft() {
    $("[id$=chkListSurgeryLeft] input").each(function () {
        if ($(this).is(":checked") && $(this).val() == 8) {
            $("[id$=DivtxtOtherSurgeryLeft]").show();
            $("[id$=txtOtherSurgeryLeft]").focus();
        }
        else if ($(this).is(":checked") == false && $(this).val() == 8) {
            $("[id$=DivtxtOtherSurgeryLeft]").hide();
        }
    });
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
                    url: "HospitalVisitForSurgery.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentCode_VisitForSurgery','Id' :'0'}",
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
                    url: "HospitalVisitForSurgery.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentName_VisitForSurgery','Id' :'0'}",
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
                    url: "HospitalVisitForSurgery.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'SchoolCode_VisitForSurgery','Id' :'0'}",
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
                    url: "HospitalVisitForSurgery.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'SchoolName_VisitForSurgery','Id' :'0'}",
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
 