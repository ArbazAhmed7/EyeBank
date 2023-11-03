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
        TeacherCodeAutoComplete();
    } catch (e) {

    }

    try {
        TeacherNameAutoComplete();
    } catch (e) {

    }

    $("[id$=ddlSpherical_RightEye]").change(function () {
        let selectedItem = $("[id$=ddlSpherical_RightEye] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_RightEye]").val('00.00');
        }
        if ($("[id$=txtSpherical_RightEye]").val()) {
            $("[id$=txtSpherical_RightEye]").val(parseFloat($("[id$=txtSpherical_RightEye]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }

        $("[id$=txtSpherical_RightEye]").focus();
        $("[id$=txtSpherical_RightEye]").select();
    });

    $("[id$=txtSpherical_RightEye]").change(function () {
        let selectedItem = $("[id$=ddlSpherical_RightEye] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_RightEye]").val('00.00');
        }
        if ($("[id$=txtSpherical_RightEye]").val()) {
            $("[id$=txtSpherical_RightEye]").val(parseFloat($("[id$=txtSpherical_RightEye]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=ddlCyclinderical_RightEye]").change(function () {
        $("[id$=txtCyclinderical_RightEye]").focus();
        $("[id$=txtCyclinderical_RightEye]").select();

        if ($("[id$=txtCyclinderical_RightEye]").val()) {
            $("[id$=txtCyclinderical_RightEye]").val(parseFloat($("[id$=txtCyclinderical_RightEye]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=txtAxixA_RightEye]").change(function () {
        //if (parseFloat($("[id$=txtAxixA_RightEye]").val().trim()) == 0) {
        //    $("[id$=txtAxixA_RightEye]").attr("style", "border: red 1px solid;")
        //    $("[id$=lbl_error]").text('* Invalid Axix');
        //}
        //else
        if (parseFloat($("[id$=txtAxixA_RightEye]").val().trim()) > 180) {
            $("[id$=txtAxixA_RightEye]").attr("style", "border: red 1px solid;")
            $("[id$=lbl_error]").text('* Invalid Axix');
        }
        else {
            $("[id$=txtAxixA_RightEye]").removeAttr("style");
            $("[id$=lbl_error]").text('');
        }
    });

    //$("[id$=ddlSpherical_LeftEye]").change(function () {
    //    $("[id$=txtSpherical_LeftEye]").focus();
    //});

    $("[id$=ddlSpherical_LeftEye]").change(function () {
        let selectedItem = $("[id$=ddlSpherical_LeftEye] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_LeftEye]").val('00.00');
        }
        if ($("[id$=txtSpherical_LeftEye]").val()) {
            $("[id$=txtSpherical_LeftEye]").val(parseFloat($("[id$=txtSpherical_LeftEye]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }

        $("[id$=txtSpherical_LeftEye]").focus();
        $("[id$=txtSpherical_LeftEye]").select();
    });

    $("[id$=txtSpherical_LeftEye]").change(function () {
        let selectedItem = $("[id$=ddlSpherical_LeftEye] :selected").text();

        if (selectedItem.toLocaleLowerCase() == "plano") {
            $("[id$=txtSpherical_LeftEye]").val('00.00');
        }
        if ($("[id$=txtSpherical_LeftEye]").val()) {
            $("[id$=txtSpherical_LeftEye]").val(parseFloat($("[id$=txtSpherical_LeftEye]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=ddlCyclinderical_LeftEye]").change(function () {
        $("[id$=txtCyclinderical_LeftEye]").focus();
        $("[id$=txtCyclinderical_LeftEye]").select();

        if ($("[id$=txtCyclinderical_RightEye]").val()) {
            $("[id$=txtCyclinderical_RightEye]").val(parseFloat($("[id$=txtCyclinderical_RightEye]").val()).toFixed(2).replace(/^\d\./, '0$&'));
        }
    });

    $("[id$=txtAxixA_LeftEye]").change(function () {
        //if (parseFloat($("[id$=txtAxixA_LeftEye]").val().trim()) == 0) {
        //    $("[id$=txtAxixA_LeftEye]").attr("style", "border: red 1px solid;")
        //    $("[id$=lbl_error]").text('* Invalid Axix');
        //}
        //else
        if (parseFloat($("[id$=txtAxixA_LeftEye]").val().trim()) > 180) {
            $("[id$=txtAxixA_LeftEye]").attr("style", "border: red 1px solid;")
            $("[id$=lbl_error]").text('* Invalid Axix');
        }
        else {
            $("[id$=txtAxixA_LeftEye]").removeAttr("style");
            $("[id$=lbl_error]").text('');
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
                    url: "AutoRefractionistInspection.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentCode','Id' :'0'}",
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
                    url: "AutoRefractionistInspection.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'StudentName','Id' :'0'}",
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

function TeacherCodeAutoComplete() {
    $("[id$=txtTeacherCode]")
        .blur(function () {
            var keyEvent = $.Event("keydown");
            keyEvent.keyCode = $.ui.keyCode.ENTER;
            $(this).trigger(keyEvent);
        })
        .autocomplete({
            autoFocus: true,
            source: function (request, response) {
                $.ajax({
                    url: "AutoRefractionistInspection.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'TeacherCode','Id' :'0'}",
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
                $("[id$=txtTeacherCode]").val(ui.item.label);
                $("[id$=hfTeacherIDPKID]").val(ui.item.val);
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
function TeacherNameAutoComplete() {
    $("[id$=txtTeacherName]")
        .blur(function () {
            var keyEvent = $.Event("keydown");
            keyEvent.keyCode = $.ui.keyCode.ENTER;
            $(this).trigger(keyEvent);
        })
        .autocomplete({
            autoFocus: true,
            source: function (request, response) {
                $.ajax({
                    url: "AutoRefractionistInspection.aspx/AutoComplete",
                    data: "{'Term' :'" + request.term + "','TermType' :'TeacherName','Id' :'0'}",
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
                $("[id$=txtTeacherName]").val(ui.item.label);
                $("[id$=hfTeacherIDPKID]").val(ui.item.val);
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