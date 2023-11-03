function pageLoad() {
    try {
        SchoolCodeAutoComplete();
    } catch (e) {

    }

    try {
        SchoolNameAutoComplete();
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
                url: "TeacherEnrollment.aspx/AutoComplete",
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
function TeacherNameAutoComplete()
{
    $("[id$=txtTeacherName]")
        //.blur(function () {
        //    var keyEvent = $.Event("keydown");
        //    keyEvent.keyCode = $.ui.keyCode.ENTER;
        //    $(this).trigger(keyEvent);
        //})
        .autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "TeacherEnrollment.aspx/AutoComplete",
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