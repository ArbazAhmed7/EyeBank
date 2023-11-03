
var selectedId = 0;

function save() {

    if (!validate()) {
        return;
    }

    try { 

        var mainCategory = {};

        if (selectedId != 0) {
            mainCategory.MainCategoryID = selectedId
        }

        mainCategory.MainCatCode = $('#tb_code').val();
        mainCategory.Description = $('#tb_descr').val();
        mainCategory.CoverMin = $('#tb_convermin').val();
        mainCategory.CoverMax = $('#tb_convermax').val(); 
        mainCategory.IsActive = $('#isActive').prop('checked'); 
        mainCategory.Operation = selectedId == 0 ? "SAVE" : "UPDATE"; 

        var url = "/Inventory/Category/Main/Save";

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
                            data: mainCategory,
                            method: 'post',
                            dataType: 'JSON',
                            success: function (result) {
                            if (result.includes("Error")) {
                                $.alert({ title: "<i class='fa fa-info'></i> Error ", content: result.replace("Error :", " ") });
                            }
                                clearForm();
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                $.alert('Error', errorThrown);
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
        url: '/Inventory/Category/Main/Code/Json', 
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
    $('#tb_convermin').val(0);
    $('#tb_convermax').val(0);
    $('#isActive').prop('checked', true);
    $('#btnSave').html("<i class='fa fa-plus'></i>&nbsp;&nbsp;Create");
    $("#tb_descr").focus();
    refreshDataTable();  
}

function onActionButtonClicked(action, data) { 
     
    if (action == 'edit') {
        edit(data);
    }
    else if (action == 'delete'){
        deleteCategory(data[0]);
    }
}

function edit(data) { 
    
    $('#btnSave').html("<i class='fa fa-save'></i>&nbsp;&nbsp;Save");
    selectedId = data[0];
    $('#tb_code').val(data[1]);
    $('#tb_descr').val(data[2]);
    $('#tb_convermin').val(data[3]);
    $('#tb_convermax').val(data[4]);  
    $('#isActive').prop('checked', data[5] == 'Yes' ? true : false); 
    $("#tb_descr").focus();

    //$('html, body').stop().animate({
    //    scrollTop: $("#editor").offset().top
    //}, 400, function () {
    //    page.off("scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove");
    //});  
     

    $(".effect").effect("highlight", { color: '#4ECDC4' }, 400);

}

function deleteCategory(id){

    $.confirm({ 
        title: 'Are you sure?',
        content: 'Do you want to delete this record?', 
        buttons: {

            confirm: {
                text: "Yes, Delete",
                btnClass: 'btn-danger',
                keys: ['enter'],
                action: function() { 
                    $.ajax({
                    url: '/Inventory/Category/Main/Delete/' + id,
                    method: 'post',
                        success: function (result) {
                            if (result.includes("Error")) {
                                $.alert({ title: "<i class='fa fa-info'></i>Error ", content: result.replace("Error :", " ") });
                            }
                        clearForm();
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.alert('Error', errorThrown);
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
        AddVAlidationToControl('tb_code', "show", 'Mandatory', 'top');
        $('#tb_code').focus();
    }
    else {
        AddVAlidationToControl("tb_code", "hide", "", "bottom");
    }

    if ($('#tb_descr').val() == '') {
        returnVal = false;
        AddVAlidationToControl("tb_descr", "show", 'Mandatory', 'top');
    }
    else {
        AddVAlidationToControl("tb_descr", "hide", "", "bottom")
    }

    if ($('#tb_convermin').val() == '' || parseInt($('#tb_convermin').val()) ==0) {
        returnVal = false;
        AddVAlidationToControl("tb_convermin", "show", 'Mandatory', 'top')
    }
    else {
        AddVAlidationToControl("tb_convermin", "hide", "", "bottom")
    }

    if ($('#tb_convermax').val() == '' || parseInt($('#tb_convermax').val()) == 0) {
        returnVal = false;
        AddVAlidationToControl("tb_convermax", "show", 'Mandatory', 'top')
    }
    else {
        AddVAlidationToControl("tb_convermax", "hide", "", "bottom")
    }

    return returnVal;

}





