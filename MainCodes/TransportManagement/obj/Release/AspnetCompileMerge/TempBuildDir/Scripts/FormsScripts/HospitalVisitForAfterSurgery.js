function pageLoad() {

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

    $("[id$=ddlSpherical_RightEye_AutoRef]").change(function () {
        let selectedItem = $("[id$=ddlSpherical_RightEye_AutoRef] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_RightEye_AutoRef]").val('00.00');
        }
        if ($("[id$=txtSpherical_RightEye_AutoRef]").val()) {
            $("[id$=txtSpherical_RightEye_AutoRef]").val(parseFloat($("[id$=txtSpherical_RightEye_AutoRef]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }

        $("[id$=txtSpherical_RightEye_AutoRef]").focus();
        $("[id$=txtSpherical_RightEye_AutoRef]").select();
    });

    $("[id$=txtSpherical_RightEye_AutoRef]").change(function () {
        let selectedItem = $("[id$=ddlSpherical_RightEye_AutoRef] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_RightEye_AutoRef]").val('00.00');
        }
        if ($("[id$=txtSpherical_RightEye_AutoRef]").val()) {
            $("[id$=txtSpherical_RightEye_AutoRef]").val(parseFloat($("[id$=txtSpherical_RightEye_AutoRef]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=ddlCyclinderical_RightEye_AutoRef]").change(function () {
        $("[id$=txtCyclinderical_RightEye_AutoRef]").focus();
        $("[id$=txtCyclinderical_RightEye_AutoRef]").select();

        if ($("[id$=txtCyclinderical_RightEye_AutoRef]").val()) {
            $("[id$=txtCyclinderical_RightEye_AutoRef]").val(parseFloat($("[id$=txtCyclinderical_RightEye_AutoRef]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=txtAxixA_RightEye_AutoRef]").change(function () {
        //if (parseFloat($("[id$=txtAxixA_RightEye_AutoRef]").val().trim()) == 0) {
        //    $("[id$=txtAxixA_RightEye_AutoRef]").attr("style", "border: red 1px solid;")
        //    $("[id$=lbl_error]").text('* Invalid Axix');
        //}
        //else
        if (parseFloat($("[id$=txtAxixA_RightEye_AutoRef]").val().trim()) > 180) {
            $("[id$=txtAxixA_RightEye_AutoRef]").attr("style", "border: red 1px solid;")
            $("[id$=lbl_error]").text('* Invalid Axix');
        }
        else {
            $("[id$=txtAxixA_RightEye_AutoRef]").removeAttr("style");
            $("[id$=lbl_error]").text('');
        }
    });

    //$("[id$=ddlSpherical_LeftEye]").change(function () {
    //    $("[id$=txtSpherical_LeftEye]").focus();
    //});

    $("[id$=ddlSpherical_LeftEye_AutoRef]").change(function () {
        let selectedItem = $("[id$=ddlSpherical_LeftEye_AutoRef] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_LeftEye_AutoRef]").val('00.00');
        }
        if ($("[id$=txtSpherical_LeftEye_AutoRef]").val()) {
            $("[id$=txtSpherical_LeftEye_AutoRef]").val(parseFloat($("[id$=txtSpherical_LeftEye_AutoRef]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }

        $("[id$=txtSpherical_LeftEye_AutoRef]").focus();
        $("[id$=txtSpherical_LeftEye_AutoRef]").select();
    });

    $("[id$=txtSpherical_LeftEye_AutoRef]").change(function () {
        let selectedItem = $("[id$=ddlSpherical_LeftEye_AutoRef] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_LeftEye_AutoRef]").val('00.00');
        }
        if ($("[id$=txtSpherical_LeftEye_AutoRef]").val()) {
            $("[id$=txtSpherical_LeftEye_AutoRef]").val(parseFloat($("[id$=txtSpherical_LeftEye_AutoRef]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=ddlCyclinderical_LeftEye_AutoRef]").change(function () {
        $("[id$=txtCyclinderical_LeftEye_AutoRef]").focus();
        $("[id$=txtCyclinderical_LeftEye_AutoRef]").select();

        if ($("[id$=txtCyclinderical_RightEye_AutoRef]").val()) {
            $("[id$=txtCyclinderical_RightEye_AutoRef]").val(parseFloat($("[id$=txtCyclinderical_RightEye_AutoRef]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=txtAxixA_LeftEye_AutoRef]").change(function () {
        //if (parseFloat($("[id$=txtAxixA_LeftEye_AutoRef]").val().trim()) == 0) {
        //    $("[id$=txtAxixA_LeftEye_AutoRef]").attr("style", "border: red 1px solid;")
        //    $("[id$=lbl_error]").text('* Invalid Axix');
        //}
        //else
        if (parseFloat($("[id$=txtAxixA_LeftEye_AutoRef]").val().trim()) > 180) {
            $("[id$=txtAxixA_LeftEye_AutoRef]").attr("style", "border: red 1px solid;")
            $("[id$=lbl_error]").text('* Invalid Axix');
        }
        else {
            $("[id$=txtAxixA_LeftEye_AutoRef]").removeAttr("style");
            $("[id$=lbl_error]").text('');
        }
    });









    $("[id$=ddlSphericalRightEyeSR]").change(function () {
        let selectedItem = $("[id$=ddlSphericalRightEyeSR] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_RightEyeSR]").val('00.00');
        }
        if ($("[id$=txtSpherical_RightEyeSR]").val()) {
            $("[id$=txtSpherical_RightEyeSR]").val(parseFloat($("[id$=txtSpherical_RightEyeSR]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }

        $("[id$=txtSpherical_RightEyeSR]").focus();
        $("[id$=txtSpherical_RightEyeSR]").select();
    });

    $("[id$=txtSpherical_RightEyeSR]").change(function () {
        let selectedItem = $("[id$=txtSpherical_RightEyeSR] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_RightEyeSR]").val('00.00');
        }
        if ($("[id$=txtSpherical_RightEyeSR]").val()) {
            $("[id$=txtSpherical_RightEyeSR]").val(parseFloat($("[id$=txtSpherical_RightEyeSR]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=ddlCyclindericalRightEyeSR]").change(function () {
        $("[id$=txtCyclinderical_RightEyeSR]").focus();
        $("[id$=txtCyclinderical_RightEyeSR]").select();

        if ($("[id$=txtCyclinderical_RightEyeSR]").val()) {
            $("[id$=txtCyclinderical_RightEyeSR]").val(parseFloat($("[id$=txtCyclinderical_RightEyeSR]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=txtAxixA_RightEyeSR]").change(function () {
        //if (parseFloat($("[id$=txtAxixA_RightEye]").val().trim()) == 0) {
        //    $("[id$=txtAxixA_RightEye]").attr("style", "border: red 1px solid;")
        //    $("[id$=lbl_error]").text('* Invalid Axix');
        //}
        //else
        if (parseFloat($("[id$=txtAxixA_RightEyeSR]").val().trim()) > 180) {
            $("[id$=txtAxixA_RightEyeSR]").attr("style", "border: red 1px solid;")
            $("[id$=lbl_error]").text('* Invalid Axix');
        }
        else {
            $("[id$=txtAxixA_RightEyeSR]").removeAttr("style");
            $("[id$=lbl_error]").text('');
        }
    });

    //$("[id$=ddlSpherical_LeftEye]").change(function () {
    //    $("[id$=txtSpherical_LeftEye]").focus();
    //});

    $("[id$=ddlSphericalLeftEyeSR]").change(function () {
        let selectedItem = $("[id$=ddlSphericalLeftEyeSR] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_LeftEyeSR]").val('00.00');
        }
        if ($("[id$=txtSpherical_LeftEyeSR]").val()) {
            $("[id$=txtSpherical_LeftEyeSR]").val(parseFloat($("[id$=txtSpherical_LeftEyeSR]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }

        $("[id$=txtSpherical_LeftEyeSR]").focus();
        $("[id$=txtSpherical_LeftEyeSR]").select();
    });

    $("[id$=txtSpherical_LeftEyeSR]").change(function () {
        let selectedItem = $("[id$=ddlSphericalLeftEyeSR] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_LeftEyeSR]").val('00.00');
        }
        if ($("[id$=txtSpherical_LeftEyeSR]").val()) {
            $("[id$=txtSpherical_LeftEyeSR]").val(parseFloat($("[id$=txtSpherical_LeftEyeSR]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=ddlCyclindericalLeftEyeSR]").change(function () {
        $("[id$=txtCyclinderical_LeftEyeSR]").focus();
        $("[id$=txtCyclinderical_LeftEyeSR]").select();

        if ($("[id$=txtCyclinderical_LeftEyeSR]").val()) {
            $("[id$=txtCyclinderical_LeftEyeSR]").val(parseFloat($("[id$=txtCyclinderical_LeftEyeSR]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=txtAxixA_LeftEyeSR]").change(function () {
        //if (parseFloat($("[id$=txtAxixA_LeftEye]").val().trim()) == 0) {
        //    $("[id$=txtAxixA_LeftEye]").attr("style", "border: red 1px solid;")
        //    $("[id$=lbl_error]").text('* Invalid Axix');
        //}
        //else
        if (parseFloat($("[id$=txtAxixA_LeftEyeSR]").val().trim()) > 180) {
            $("[id$=txtAxixA_LeftEyeSR]").attr("style", "border: red 1px solid;")
            $("[id$=lbl_error]").text('* Invalid Axix');
        }
        else {
            $("[id$=txtAxixA_LeftEyeSR]").removeAttr("style");
            $("[id$=lbl_error]").text('');
        }
    });
    //$("[id$=ddlSpherical_RightEye]").change(function () {
    //    $("[id$=txtSpherical_RightEye]").focus(); //function () { $(this).select(); }
    //});

    //$("[id$=ddlCyclinderical_RightEye]").change(function () {
    //    $("[id$=txtCyclinderical_RightEye]").focus();
    //});

    $("[id$=ddlNear_RightEyeSR]").change(function () {
        $("[id$=txtNear_RightEyeSR]").focus();
    });

    //$("[id$=ddlSpherical_LeftEye]").change(function () {
    //    $("[id$=txtSpherical_LeftEye]").focus();
    //});

    //$("[id$=ddlCyclinderical_LeftEye]").change(function () {
    //    $("[id$=txtCyclinderical_LeftEye]").focus();
    //});

    $("[id$=ddlNear_LeftEye]").change(function () {
        $("[id$=txtNear_LeftEye]").focus();
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
                    url: "HospitalVisitForAfterSurgery.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentCode_VisitForAfterSurgery','Id' :'0'}",
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
                    url: "HospitalVisitForAfterSurgery.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentName_VisitForAfterSurgery','Id' :'0'}",
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
                    url: "HospitalVisitForAfterSurgery.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'SchoolCode_VisitForAfterSurgery','Id' :'0'}",
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
                    url: "HospitalVisitForAfterSurgery.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'SchoolName_VisitForAfterSurgery','Id' :'0'}",
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