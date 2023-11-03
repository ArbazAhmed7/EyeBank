
var setupDetailTable = null;
var _ddlSetupSelectedText;
var _ddlSetupSelectedValue;
var _setupDetailID;
var dropDownValue;
var _keyCode;

/* when page ready */
$(document).ready(function () {
    dropDownValue = $('#ddlSetup').val();
    initSetupDataTable(makeUrl(dropDownValue)); 
});

/* init datatable */
function initSetupDataTable(mUrl) {

    window.$.ajax({
        type: "GET",
        dataType: "json",
        url: mUrl,
        success: function (mData) {

            if (mData) { 

                setupDetailTable = window.$('#tblUser').DataTable({
                    data: mData.data,
                    select: 'multi',
                    order: [[0, 'asc']],
                    columns: [
                        {
                            data: "setupDetailID",
                            "visible": false
                        },
                        { data: "name" },
                        { data: "setupCode" },
                        {
                            data: "isActive", render: function (data, type, row) {
                                if (data)
                                    return '<input type="checkbox" checked disabled>';
                                else
                                    return '<input type="checkbox" disabled>';
                            }
                        },
                        {
                            data: null,
                            //className: "center",
                            defaultContent: '<button id="btnEditGeneralSetup" action="edit" class="editor_edit btnGeneralEdit">Edit</button> / <a href="" id="btnDeleteRow" class="editor_remove">Delete</a>',
                            //render: function (data, type, row) {
                            //    _setupDetailID = data['setupDetailID'];
                            //}
                        }
                    ],
                    processing: true,
                    scrollY: "200px",
                    scrollCollapse: true,
                    lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]]
                });
            } else {
                // hadle me 
            }
        },
        complete: function () {
             // hadle me
        },
        error: function (request, status, error) {
            // hadle me
        }
    }); 
}

/* Drop down change */
$('#ddlSetup').on('change', function (e) {
    e.preventDefault();
    getDataByDropDownValue(e.target.value); 
});

/* create URL by drop down id */
function makeUrl(value) {
    return "/Admin/Setup/GetSetup?generalSetupDetail=" + value;
}

/* download and replace data of datatable */
function getDataByDropDownValue(value) { 

    var mUrl = makeUrl(value);
    if (setupDetailTable != null) { 
        window.$.ajax({
            type: "GET",
            dataType: "json",
            url: mUrl,
            success: function (mData) {
                setupDetailTable.clear();
                setupDetailTable.rows.add(mData.data);
                setupDetailTable.draw();
            }
        });
    }
    else {
        initSetupDataTable(mUrl);
    }
}

/* Show Model Popup  */
function showMode_AddNew() {
    $('#commentForm').trigger("reset"); // to rest the model
    $("#btnModelSave").val('0');
    $("#modelHeading").html('Create');
    _ddlSetupSelectedText = $("#ddlSetup option:selected").text();
    _ddlSetupSelectedValue = $("#ddlSetup").val();
    $("#lblSetupModelMainHeading").html('Setup (' + _ddlSetupSelectedText + ')');
    $("#btnModelSave").html('Save');
    $('#exampleModal').modal('show');
}

$('#btnSaveSetupDetail').on('click', function () {
    showMode_AddNew()
})

/* Model Button OnClick Save/Update*/
$("#btnModelSave").on('click', function (e) {
    _ddlSetupSelectedValue = $("#ddlSetup").val();
    if (_ddlSetupSelectedValue > 0) {
        var _newSetupName = $("#name").val();
        var _newSetupCode = $("#code").val();
        var _newSetupIsActive = $("#active").is(':checked');
        
        var generalSetupDetail = {};
        generalSetupDetail.SetupDetailID = _setupDetailID;
        generalSetupDetail.SetupMasterID = _ddlSetupSelectedValue;
        generalSetupDetail.SetupCode = _newSetupCode;
        generalSetupDetail.Name = _newSetupName;
        generalSetupDetail.IsActive = _newSetupIsActive;

        var IsEditMode = $("#btnModelSave").val()
        var url = '';
        if (IsEditMode == '1') {
            /** InCase Of Edit*/
            url = "/Admin/Setup/UpdateSetup";

            $.confirm({
                title: 'Are you sure?',
                content: 'Do you want to Save this record?',
                buttons: {
                    confirm: {
                        text: "Yes, Save",
                        btnClass: 'btn-success',
                        keys: ['enter'],
                        action: function () {
                            var self = this;
                            return $.ajax({
                                url: url,
                                data: generalSetupDetail,
                                method: 'POST'
                            }).done(function (response) {
                                self.setContent(response.message);
                                self.setTitle(response.title);
                                getDataByDropDownValue(_ddlSetupSelectedValue);
                                clearModelFields();
                            }).fail(function () {
                                self.setContent('Something went wrong.');
                            });
                        }
                    }
                }
            })
        }
        else {
        /** InCase Of Add*/
            url = "/Admin/Setup/PostSetup";
            if (generalSetupDetail.Name != '' || generalSetupDetail.SetupCode != '') {
                $.confirm({
                    title: 'Are you sure?',
                    content: 'Do you want to add this entry?',
                    buttons: {
                        confirm: function () {
                            $.ajax({
                                url: url,
                                data: generalSetupDetail,
                                method: 'post',
                                dataType: 'JSON',
                                success: function (result) {  
                                    $.alert(result.title, result.message);
                                    getDataByDropDownValue(_ddlSetupSelectedValue);
                                    clearModelFields();
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    $.alert('Error', errorThrown);
                                }
                            });
                        },
                        cancel: function () {
                        },
                    }
                });
            }
        }
    }
    else {
    }
});

// Edit record
$('#tblUser').on('click', 'tbody tr button[action="edit"]', function () {
    //get affected row entry
    const row = setupDetailTable.row($(event.target).closest('tr'));
    $("#btnModelSave").val('1');
    $("#modelHeading").html('Edit');
    $("#btnModelSave").html('Update');
    $("#lblSetupModelMainHeading").html('Setup');
    _setupDetailID = row.data()['setupDetailID'];
    let _name = row.data()['name'];
    let _code = row.data()['setupCode'];
    let _isActive = row.data()['isActive'];
    $("#name").val(_name)
    $("#code").val(_code)
    $("#active").prop("checked", _isActive);
    $('#exampleModal').modal('show');
});

// Delete record
$('#tblUser').on('click', 'a.editor_remove', function (e) {
    e.preventDefault();
    const row = setupDetailTable.row($(event.target).closest('tr'));

    $.confirm({
        title: 'Delete setup?',
        content: 'This dialog will automatically trigger \'cancel\' in 6 seconds if you don\'t respond.',
        autoClose: 'cancelAction|8000',
        buttons: {
            confirm: function () {
                var self = this;
                var _setUpID = row.data()['setupDetailID']
                var uri = "/School/Setup/DeleteSetup?generalSetupDetailId=" + _setUpID
                return $.ajax({
                    url: uri,
                    method: 'post'
                }).done(function (response) {
                    self.setContent(response.message);
                    self.setTitle(response.title);
                    getDataByDropDownValue(_ddlSetupSelectedValue);
                    clearModelFields();
                }).fail(function () {
                    self.setContent('Something went wrong.');
                });
            },
            cancelAction: function () {
            }
        }
    });
});

// Clear Modal Popup Values
function clearModelFields() {
    $('#name').val('');
    $('#code').val('');
    $('#active').prop('checked', false);
    $('#exampleModal').modal('hide');
}