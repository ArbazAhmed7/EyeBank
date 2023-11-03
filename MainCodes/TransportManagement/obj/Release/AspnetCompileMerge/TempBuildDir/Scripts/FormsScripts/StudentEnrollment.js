function pageLoad() {

    try {
        SchoolCodeAutoComplete();
    } catch (e) { }

    try {
        SchoolNameAutoComplete();
    } catch (e) { }

    try {
        ClassCodeAutoComplete();
    } catch (e) { }

    try {
        StudentCodeAutoComplete();
    } catch (e) { }

    try {
        StudentNameAutoComplete();
    } catch (e) { }     
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
                url: "StudentEnrollment.aspx/AutoComplete",
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
function StudentNameAutoComplete()
{
    $("[id$=txtStudentName]")
        //.blur(function () {
        //    var keyEvent = $.Event("keydown");
        //    keyEvent.keyCode = $.ui.keyCode.ENTER;
        //    $(this).trigger(keyEvent);
        //})
        .autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "StudentEnrollment.aspx/AutoComplete",
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

function ClassCodeAutoComplete() {
    $("[id$=txtClassNo]")
        .blur(function () {
            var keyEvent = $.Event("keydown");
            keyEvent.keyCode = $.ui.keyCode.ENTER;
            $(this).trigger(keyEvent);
        })
        .autocomplete({
        autoFocus: true,
        source: function (request, response) {
            $.ajax({
                url: "ClassSection.aspx/AutoComplete",
                data: "{'Term' :'" + request.term + "','TermType' :'ClassNo','Id' :'0'}",
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
            $("[id$=txtClassNo]").val(ui.item.label);
            $("[id$=hfClassIDPKID]").val(ui.item.val);
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