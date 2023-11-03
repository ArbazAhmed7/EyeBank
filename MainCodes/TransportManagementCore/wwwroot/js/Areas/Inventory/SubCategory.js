
var selectedId = 0;

$(document).ready(function () {
    registerLookupButton("btn_main_category_modal", "modal_main_category", "/Inventory/Category/Sub/Select/Main/Json");    
    //$('#MainCat').css({ 'display': 'none' });
});
 
function save() {

    if (!validate()) {
        return;
    }

    try {

        var model = {};

        if (selectedId != 0) {
            model.id = selectedId
        }

        model.code = $('#tb_code').val();
        model.description = $('#tb_descr').val();
        model.mainCategoryId = $('#main_category_id').val(); 
        model.active = $('#isActive').prop('checked');
        model.task = selectedId == 0 ? "SAVE" : "UPDATE";

        var url = "/Inventory/Category/Sub/Save";

        $.confirm({
            title: 'Are you sure?',
            content: 'Do you want to save this record?',
            buttons: {

                confirm: {
                    text: "Yes, Save",
                    btnClass: 'btn-success',
                    keys: ['enter'],
                    action: function () {

                        $.ajax({
                            url: url,
                            data: model,
                            method: 'post',
                            dataType: 'JSON',
                            success: function (result) {
                                if (result.includes("Error")) {
                                    $.alert({ title: "<i class='fa fa-info'></i> Error ", content: result.replace("Error :", " ") });
                                    return;
                                }
                                clearForm();
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {                                
                                $.alert({ title: "<i class='fa fa-info'></i> Error ", content: errorThrown });
                            }
                        });

                    },

                },

                cancel: {
                    text: "No, Cancel",
                    keys: ['esc'],
                    action: function () {
                        // do nothing
                    }
                }

            }

        });

    }
    catch (e) {
        alert(e.error);
    }

}

function refreshDataTable() {
    DataTableClientSearch();
}

function getNextCode() {
    $('#tb_code').val('');
    $.ajax({
        url: '/Inventory/Category/Sub/Code/Json',
        method: 'get',
        success: function (result) {
            if (result == '0') {
                $("#tb_code").prop("disabled", false);
            }
            else {
                $('#tb_code').val(result);
                $("#tb_code").prop("disabled", true);
            }
        }
    });
}

function clearForm() {
    selectedId = 0;
    getNextCode();
    $('#tb_descr').val('');
    $('#main_category_id').val('');
    $('#main_category_text').val(''); 
    $('#isActive').prop('checked', true);
    $('#btnSave').html("<i class='fa fa-plus'></i>&nbsp;&nbsp;Create");
    $("#tb_descr").focus();
    refreshDataTable();
}

function onActionButtonClicked(action, data) { 
    if (action == 'edit') {
        edit(data);
    }
    else if (action == 'delete') {
        deleteRecord(data[0]);
    }
}

function edit(data) { 
     
    $('#btnSave').html("<i class='fa fa-save'></i>&nbsp;&nbsp;Save");
    selectedId = data[0];
    $('#tb_code').val(data[2]);
    $('#tb_descr').val(data[3]);

    $('#main_category_id').val(data[1]);
    $('#main_category_text').val(data[4]);
    $('#isActive').prop('checked', data[5]=='Yes'? true:false);
    $("#tb_descr").focus();

    //$('html, body').stop().animate({
    //    scrollTop: $("#editor").offset().top
    //}, 400, function () {
    //    page.off("scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove");
    //});

    //$(".effect").effect("highlight", { color: '#4ECDC4' }, 400);

}

function deleteRecord(id) {

    $.confirm({
        title: 'Are you sure?',
        content: 'Do you want to delete this record?',
        buttons: {

            confirm: {
                text: "Yes, Delete",
                btnClass: 'btn-danger',
                keys: ['enter'],
                action: function () {
                    $.ajax({
                        url: '/Inventory/Category/Sub/Delete/' + id,
                        method: 'post',
                        success: function (result) {
                            if (result.includes("Error")) {
                                $.alert({ title: "<i class='fa fa-info'></i> Error ", content: result.replace("Error :", " ") });
                                return;
                            }
                            clearForm();
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {                            
                            $.alert({ title: "<i class='fa fa-info'></i> Error ", content: errorThrown });
                        }
                    });
                }
            },

            cancel: {
                text: "No, Cancel",
                keys: ['esc'],
                action: function () {

                }
            },

        }
    });

}

$(document).keydown(function (event) {
    if (((event.ctrlKey) && event.which == 83) || ((event.ctrlKey) && event.which == 115)) {
        event.preventDefault();
        save();
        return false;
    }
}
);

function validate() {

    var returnVal = true;

    if ($('#tb_code').val() == '') {
        returnVal = false;
        AddVAlidationToControl("tb_code", "show", "Code is required", "bottom")
    }
    else {
        AddVAlidationToControl("tb_code", "hide", "", "bottom")
    }

    if ($('#tb_descr').val() == '') {
        returnVal = false;
        AddVAlidationToControl("tb_descr", "show", "Description is Required", "bottom")
    }
    else {
        AddVAlidationToControl("tb_descr", "hide", "", "bottom")
    }

    if ($('#main_category_id').val() == '') {
        returnVal = false;
        AddVAlidationToControl("main_category_text", "show", "This value is required", "bottom")
    }
    else {
        AddVAlidationToControl("main_category_text", "hide", "", "top")
    }

    return returnVal;

}

function onLookupItemSelected(modalName, data) { 
    switch (modalName) {
        case "modal_main_category": 
            setTimeout(function () { $("#tb_descr").focus(); }, 500);
            $("#main_category_id").val(data[1]);
            $("#main_category_text").val(data[2] + " (" + data[3] + ")");
            break;
    } 
}




