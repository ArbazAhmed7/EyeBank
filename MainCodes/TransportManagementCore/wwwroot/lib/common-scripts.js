

var isDebugging = false;

function l(tag, message) {
    if (isDebugging)
        console.log(tag + ": " + message);
}

$(function () {
    $('#nav-accordion').dcAccordion({
        eventType: 'click',
        autoClose: true,
        saveState: true,
        disableLink: true,
        speed: 'slow',
        showCount: false,
        autoExpand: true,
        ookie: 'dcjq-accordion-1',
        classExpand: 'dcjq-current-parent'
    });
});

function AllowAlphabetOnly(e) {

    var key = e.keyCode;
    if (key >= 48 && key <= 57) {
        e.preventDefault();
    }
}

var Script = function () {


    //    sidebar dropdown menu auto scrolling

    jQuery('#sidebar .sub-menu > a').click(function () {
        var o = ($(this).offset());
        diff = 250 - o.top;
        if (diff > 0)
            $("#sidebar").scrollTo("-=" + Math.abs(diff), 500);
        else
            $("#sidebar").scrollTo("+=" + Math.abs(diff), 500);
    });



    //    sidebar toggle

    $(function () {
        function responsiveView() {
            var wSize = $(window).width();
            if (wSize <= 768) {
                $('#container').addClass('sidebar-close');
                $('#sidebar > ul').hide();
            }

            if (wSize > 768) {
                $('#container').removeClass('sidebar-close');
                $('#sidebar > ul').show();
            }
        }
        $(window).on('load', responsiveView);
        $(window).on('resize', responsiveView);
    });

    $('.fa-bars').click(function () {
        if ($('#sidebar > ul').is(":visible") === true) {
            $('#main-content').css({
                'margin-left': '0px'
            });
            $('#sidebar').css({
                'margin-left': '-210px'
            });
            $('#sidebar > ul').hide();
            $("#container").addClass("sidebar-closed");
        } else {
            $('#main-content').css({
                'margin-left': '210px'
            });
            $('#sidebar > ul').show();
            $('#sidebar').css({
                'margin-left': '0'
            });
            $("#container").removeClass("sidebar-closed");
        }
    });

    // custom scrollbar
    $("#sidebar").niceScroll({
        styler: "fb",
        cursorcolor: "#4ECDC4",
        cursorwidth: '3',
        cursorborderradius: '10px',
        background: '#404040',
        spacebarenabled: false,
        cursorborder: ''
    });

    //  $("html").niceScroll({styler:"fb",cursorcolor:"#4ECDC4", cursorwidth: '6', cursorborderradius: '10px', background: '#404040', spacebarenabled:false,  cursorborder: '', zindex: '1000'});

    // widget tools

    jQuery('.panel .tools .fa-chevron-down').click(function () {
        var el = jQuery(this).parents(".panel").children(".panel-body");
        if (jQuery(this).hasClass("fa-chevron-down")) {
            jQuery(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
            el.slideUp(200);
        } else {
            jQuery(this).removeClass("fa-chevron-up").addClass("fa-chevron-down");
            el.slideDown(200);
        }
    });

    jQuery('.panel .tools .fa-times').click(function () {
        jQuery(this).parents(".panel").parent().remove();
    });


    //    tool tips

    $('.tooltips').tooltip();

    //    popovers

    $('.popovers').popover();



    // custom bar chart

    if ($(".custom-bar-chart")) {
        $(".bar").each(function () {
            var i = $(this).find(".value").html();
            $(this).find(".value").html("");
            $(this).find(".value").animate({
                height: i
            }, 2000)
        })
    }

}();

$(document).ready(function (e) {
    DataTableClientSearch();
    DataTableServerSearch();
    
    var ServerSidetable = $('#data-table-server-side-search_wrapper').DataTable();


});

var mSourceUrl;
var mTableId;
var mSourceUrlClient;
var mTableIdClient;

$(window).on('shown.bs.modal', function (event) {
    var target = $(event.relatedTarget).attr('data-target');
    if (target) {
        if (target.startsWith('#lookup-modal-')) {
            var modalName = $(event.relatedTarget).attr('data-name');
            var modalUrl = $(event.relatedTarget).attr('data-url');
            initLookupScript(modalName, modalUrl);
        }
    }
});

$(window).on('hidden.bs.modal', function (event) {
    //$('#lookup-modal').modal('hide');
    var modalName = $(event.relatedTarget).attr('data-name');
    if (typeof onLookupClosed === 'function') {
        onLookupClosed(modalName);
    }
});

function initLookupScript(modalName, modalUrl) {

    if ($.fn.DataTable.isDataTable($("#table-lookup-" + modalName))) {

        var postdata = null;
        var attr = $("#lookup-button-" + modalName).attr('data-postdata');
        if (typeof attr !== typeof undefined && attr !== false) {
            postdata = JSON.parse(decodeURIComponent($("#lookup-button-" + modalName).attr("data-postdata")));
        }

        window.$.ajax({
            url: modalUrl,
            data: postdata,
            type: "POST",
            dataType: "JSON",
            success: function (mData) {
                $("#table-lookup-" + modalName).DataTable().clear();
                $("#table-lookup-" + modalName).DataTable().rows.add(mData.data);
                $("#table-lookup-" + modalName).DataTable().draw();
                $('div.dataTables_filter input', $("#table-lookup-" + modalName).DataTable().table().container()).focus();
            },
            complete: function () {
                hideLoader();
            },
            error: function (request, status, error) {
                l("Lookup", "AJAX request in is failed with error. \n\nError Details: " + error);
            }
        });

    }
    else {
        initLookupTable(modalName, modalUrl);
    }

}

function lookupItemSelected(modalName, data) {
    $('#lookup-modal-' + modalName).modal('toggle');
    if (typeof onLookupItemSelected === 'function') {
        onLookupItemSelected(modalName, data);
    }
}

function initLookupTable(modalName, modalUrl) {

    var postdata = null;
    var attr = $("#lookup-button-" + modalName).attr('data-postdata');
    if (typeof attr !== typeof undefined && attr !== false) {
        postdata = JSON.parse(decodeURIComponent($("#lookup-button-" + modalName).attr("data-postdata")));
    }

    showLoader();
    window.$.ajax({
        url: modalUrl,
        data: postdata,
        type: "POST",
        dataType: "JSON",
        success: function (mData) {
            if (mData) {
                var lookupTable = window.$('#table-lookup-' + modalName).DataTable({
                    data: mData.data,
                    columns: mData.columns,
                    scrollY: "200px",
                    select: 'multi',
                    order: [],
                    processing: true,
                    mark: true,
                    scrollCollapse: true,
                    "columnDefs": [
                        {
                            "targets": mData.columnsToHide,
                            "searchable": false,
                            "visible": false
                        },
                        {
                            "targets": 0,
                            "searchable": false,
                            'mRender': function (data, type, row) {
                                return '<div style="font-size: 16px" class="btn-group"><button class="action-button btn btn-light waves-effect lookup-action-button" title="Select" data-action="Select" type="button"><i class="zmdi zmdi-check-circle zmdi-hc-lg mdc-text-green-500"></i></button></div>';
                            }
                        }
                    ],
                    lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]]
                });
                lookupTable.on('click', '.lookup-action-button', function () {
                    var closestRow = $(this).closest('tr');
                    var data = lookupTable.row(closestRow).data();
                    lookupItemSelected(modalName, data);
                });
                $('div.dataTables_filter input', lookupTable.table().container()).focus();
            } else {
                l("Lookup", "AJAX request in sucess but data is null. Please check your data source.");
            }
        },
        complete: function () {
            hideLoader();
        },
        error: function (request, status, error) {
            l("Lookup", "AJAX request in is failed with error. \n\nError Details: " + error);
        }
    });
}

function registerLookupButton(controlId, modalName, modalUrl) {
    if ($("#" + controlId).length) {
        $("#" + controlId).attr("data-name", modalName);
        $("#" + controlId).attr("data-url", modalUrl);
        $("#" + controlId).attr("data-toggle", "modal");
        $("#" + controlId).attr("data-target", "#lookup-modal-" + modalName);
        $("#" + controlId).attr("data-id", controlId);
        $('#' + controlId).prop('id', 'lookup-button-' + modalName);
    }
}

function unRegiserLookupButton(modalName) {
    if ($("#lookup-button-" + modalName).length) {
        var id = $("#lookup-button-" + modalName).attr("data-id");
        $("#lookup-button-" + modalName).removeAttr("data-name");
        $("#lookup-button-" + modalName).removeAttr("data-url");
        $("#lookup-button-" + modalName).removeAttr("data-toggle");
        $("#lookup-button-" + modalName).removeAttr("data-target");
        $('#lookup-button-' + modalName).prop('id', id);
        $("#" + id).removeAttr("data-id");
    }
}

function lookupId(lookupName, id) {
    if (id) {
        $("#lookup-" + lookupName + "-id").val(id);
    }
    else {
        return $("#lookup-" + lookupName + "-id").val();
    }
}

function lookupText(lookupName, text) {
    if (text) {
        $("#lookup-" + lookupName + "-text").val(text);
    }
    else {
        return $("#lookup-" + lookupName + "-text").val();
    }
}

function enableLookup(lookupName) {
    $("#lookup2-input-group-" + lookupName).addClass("bootstrap-timepicker");
    $("#lookup2-input-group-" + lookupName).addClass("input-group");
    $("#lookup-button-span-" + lookupName).css("visibility", 'visible');
}

function disableLookup(lookupName) {
    $("#lookup2-input-group-" + lookupName).removeClass("bootstrap-timepicker");
    $("#lookup2-input-group-" + lookupName).removeClass("input-group");
    $("#lookup-button-span-" + lookupName).css("visibility", 'hidden');
}

function showLoader() {
    $('.loader').show();
}

function hideLoader() {
    $('.loader').hide();
}

function bindDataToLookup(modalName, postData) {
    if ($("#lookup-button-" + modalName).length) {
        $("#lookup-button-" + modalName).attr("data-postdata", encodeURIComponent(JSON.stringify(postData)));
    }
}

function unBindDataFromLookup(modalName) {
    if ($("#lookup-button-" + modalName).length) {
        $("#lookup-button-" + modalName).removeAttr("data-postdata");
    }
}

function DataTableServerSearch() {
    if ($("#data-table-server-side-search").length) {
        populateDataTable($("#data-table-server-side-search").attr('data-url'));
    }
}

function populateDataTable(sourceUrl) {

    if (!mSourceUrl)
        mSourceUrl = sourceUrl;

    if (!mTableId)
        mTableId = "data-table-server-side-search";

    if ($.fn.DataTable.isDataTable($("#" + mTableId))) {
        $.alert({
            content: function () {
                var self = this;
                return $.ajax({
                    type: "GET",
                    dataType: "json",
                    url: sourceUrl,
                    beforeSend: function () {
                        $("#" + mTableId).DataTable().clear();
                    }
                }).done(function (mData) {
                    $("#" + mTableId).DataTable().rows.add(mData.data);
                    $("#" + mTableId).DataTable().draw();
                }).always(function () {
                    self.close();
                });
            }
        });
    }
    else {
        initDataTable(sourceUrl);
    }

}

function initDataTable(sourceUrl) {
    $.alert({
        content: function () {
            var self = this;
            return $.ajax({
                type: "GET",
                dataType: "json",
                url: sourceUrl,
            }).done(function (mData) {

                if (mData) {
                    var mTable = window.$('#' + mTableId).DataTable({
                        data: mData.data,
                        columns: mData.columns,
                        select: 'multi',
                        searching: false,
                        font: "120px",
                        order: [],
                        processing: true,
                        scrollY: "200px",
                        scrollCollapse: true,
                        "columnDefs": [
                            {
                                "targets": '_all',
                                "defaultContent": ""
                            },
                            {
                                "targets": mData.columnsToHide,
                                "searchable": false,
                                "visible": false
                            },
                            {
                                "targets": mData.columns.length,
                                "searchable": false,
                                "title": 'Actions',
                                "visible": (mData.actionButtons ? true : false),
                                'mRender': function (data, type, row) {

                                    if (mData.actionButtons) {
                                        return '<div style="font-size: 16px; display:flex; justify-content: center;" class="btn-group">' + getActionButton(mData.actionButtons) + '</div>';
                                    }
                                    else {
                                        return '';
                                    }

                                }
                            }
                        ],
                        lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]]
                    });

                    $('#data-table-server-side-search_wrapper .row-fluid').first().append('<div class="span6"><div id="table_filter" class="dataTables_filter"><div class="input-group" style="float:right;"><input type="text" class="form-control dt-search-input" size="16"><div class="input-group-btn"><button type="button" class="btn btn-theme02 date-reset dt-search-clear"><i class="fa fa-times"></i></button><button type="button" class="btn btn-theme date-set dt-search"><i class="fa fa-search"></i></button></div></div></div></div>');
                    $(".dt-search-input").focus();
                    $(".dt-search-clear").click(function () {
                        $(".dt-search-input").val('');
                        populateDataTable(mSourceUrl);
                    });

                    $(".dt-search").click(function () {
                        var searchWord = $(".dt-search-input").val().trim();
                        populateDataTable(mSourceUrl + "/" + searchWord);
                    });

                    $(".dt-search-input").keypress(function (e) {
                        if (e.which === 13) {
                            e.preventDefault();
                            var searchWord = $(".dt-search-input").val().trim();
                            populateDataTable(mSourceUrl + "/" + searchWord);
                        }
                    });

                    $("#" + mTableId).DataTable().on('click', '.action-button', function () {
                        var actionName = $(this).attr('data-action');
                        var closestRow = $(this).closest('tr');
                        var data = $("#" + mTableId).DataTable().row(closestRow).data();
                        if (typeof onActionButtonClicked === 'function') {
                            onActionButtonClicked(actionName, data);
                        }
                    });

                } else {
                    l("populateDataTable", "AJAX request in success but data is null. Please check your data source.");
                }

            }).always(function () {
                self.close();
            });
        }
    });
}

function refreshDataTable(isClient = true) {
    if (isClient) {
        DataTableClientSearch();
    }
}

function DataTableClientSearch() {
    if ($("#data-table-client-side-search").length) {
        populateDataTableClient($("#data-table-client-side-search").attr('data-url'));
    }
}

function populateDataTableClient(sourceUrl) {

    if (!mSourceUrlClient)
        mSourceUrlClient = sourceUrl;

    if (!mTableIdClient)
        mTableIdClient = "data-table-client-side-search";

    if ($.fn.DataTable.isDataTable($("#" + mTableIdClient))) {
        var self = this;
        return $.ajax({
            type: "GET",
            dataType: "json",
            url: sourceUrl,
            beforeSend: function () {
                $("#" + mTableIdClient).DataTable().clear();
            }
        }).done(function (mData) {
            $("#" + mTableIdClient).DataTable().rows.add(mData.data);
            $("#" + mTableIdClient).DataTable().draw();
        }).always(function () {
            self.close();
        });
        //$.alert({
        //    content: function () {
        //    }
        //});
    }
    else {
        initDataTableClient(sourceUrl);
    }

}

function getActionButton(actionButtonsIntArray) {

    var actionButtons = '';

    /* View */
    if (actionButtonsIntArray.includes(1))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="View" data-action="view" type="button"><i class="zmdi zmdi-eye zmdi-hc-lg mdc-text-green-500"></i></button>';

    /* Edit */
    if (actionButtonsIntArray.includes(2))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Edit" data-action="edit" type="button"><i class="zmdi zmdi-edit zmdi-hc-lg mdc-text-amber-800"></i></button>';

    /* Delete */
    if (actionButtonsIntArray.includes(3))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Delete" data-action="delete" type="button"><i class="zmdi zmdi-close-circle zmdi-hc-lg mdc-text-red-500"></i></button>';

    /* MarkForModification */
    if (actionButtonsIntArray.includes(4))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Mark For Modification" data-action="MarkForModification" type="button"><i class="zmdi zmdi-comment-alert zmdi-hc-lg mdc-text-amber-800"></i></button>';

    /* Approve */
    if (actionButtonsIntArray.includes(5))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Approve" data-action="Approve" type="button"><i class="zmdi zmdi-check-circle zmdi-hc-lg mdc-text-green-500"></i></button>';

    /* Reject */
    if (actionButtonsIntArray.includes(6))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Reject" data-action="Reject" type="button"><i class="zmdi zmdi-close-circle zmdi-hc-lg mdc-text-red-500"></i></button>';

    /* CreatePurchaseOrder */
    if (actionButtonsIntArray.includes(7))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Create Purchase Order" data-action="CreatePurchaseOrder" type="button"><i class="zmdi zmdi-shopping-cart zmdi-hc-lg mdc-text-light-blue"></i></button>';
    /* Confirmation (Open Confirm popup) */
    if (actionButtonsIntArray.includes(12))

        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Confirm" data-action="Confirm" type="button"><i style="color:#10B610;" class="fa fa-check-square" aria-hidden="true"></i></button>';
    /* Confirmation (Open Confirm popup) */
    /* Print */
    if (actionButtonsIntArray.includes(8))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Print" data-action="Print" type="button"><i class="zmdi zmdi-print zmdi-hc-lg mdc-text-grey"></i></button>';

    /* Add */
    if (actionButtonsIntArray.includes(9))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Add" data-action="Add" type="button"><i class="zmdi zmdi-plus-circle zmdi-hc-lg mdc-text-amber-800"></i></button>';

    /* Reminder/Set Reminder */
    if (actionButtonsIntArray.includes(10))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Set Reminder" data-action="Reminder" type="button"><i style="color:#D2691E;" class="zmdi zmdi-alarm zmdi-hc-lg"></i></button>';

    /* Approval (Open approval popup) */
    if (actionButtonsIntArray.includes(11))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Approval" data-action="Approval" type="button"><i style="color:#2aa0d8;" class="zmdi zmdi-shield-check zmdi-hc-lg"></i></button>';


    if (actionButtonsIntArray.includes(13))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Temporary Donation" data-action="Confirm" type="button"><i style="color:#10B610;" class="zmdi zmdi-bike zmdi-hc-lg" aria-hidden="true"></i></button>';

    /* Patient Discharge */
    if (actionButtonsIntArray.includes(14))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Discharge" data-action="Discharge" type="button"><i style="color:#6666ff		; font-size: 15px;" class="fa fa-user-times" aria-hidden="true"></i></button>';

    /* Surgery PostPoned*/
    if (actionButtonsIntArray.includes(15))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="PostPoned" data-action="PostPoned" type="button"><i style="color:#6666ff		; font-size: 15px;" class="fa fa-clock-o" aria-hidden="true"></i></button>';

    /* LAMA */
    if (actionButtonsIntArray.includes(16))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="LAMA" data-action="LAMA" type="button"><i style="color:#6666ff; font-size: 15px;" class="fa fa-chain-broken" aria-hidden="true"></i></button>';

    if (actionButtonsIntArray.includes(17))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Direct FollowUp" data-action="FollowUp" type="button"><i style="color:#6666ff		; font-size: 15px;" class="fa fa-clock-o" aria-hidden="true"></i></button>';

    if (actionButtonsIntArray.includes(18))
        actionButtons += '<button class="action-button btn btn-light waves-effect" title="Print FollowUp" data-action="Print" type="button"><i class="zmdi zmdi-print zmdi-hc-lg mdc-text-grey"></i></button>';

    return actionButtons;
}

function initDataTableClient(sourceUrl) {
    var self = this;
    return $.ajax({
        type: "GET",
        dataType: "json",
        url: sourceUrl,
    }).done(function (mData) {

        if (mData) {
            console.log(mData);
            var mTable = window.$('#' + mTableIdClient).DataTable({
                data: mData.data,
                columns: mData.columns,
                select: 'multi',
                order: [],
                processing: true,
                scrollY: "200px",
                mark: true,
                scrollCollapse: false,
                sScrollX: "100%",
                sScrollXInner: "100%",
                "columnDefs": [
                    {
                        "targets": '_all',
                        "defaultContent": ""
                    },
                    {
                        "targets": mData.columnsToHide,
                        "searchable": false,
                        "visible": false
                    },
                    {
                        "targets": mData.columns.length,
                        "searchable": false,
                        "title": 'Actions',
                        "visible": (mData.actionButtons ? true : false),
                        'mRender': function (data, type, row) {

                            if (mData.actionButtons) {
                                return '<div style="font-size: 16px; display:flex; justify-content: center;" class="btn-group">' + getActionButton(mData.actionButtons) + '</div>';
                            }
                            else {
                                return '';
                            }

                        }
                    }
                ],
                lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]]
            });

            $("#" + mTableIdClient).DataTable().on('click', '.action-button', function () {
                var actionName = $(this).attr('data-action');
                var closestRow = $(this).closest('tr');
                var data = $("#" + mTableIdClient).DataTable().row(closestRow).data();
                if (typeof onActionButtonClicked === 'function') {
                    onActionButtonClicked(actionName, data);
                }
            });

        } else {
            l("populateDataTable", "AJAX request in sucess but data is null. Please check your data source.");
        }

    }).always(function () {
        //self.close();
    });
}

function bindDropDown(dropDownId, sourceUrl, keepOpen, isMultiple, modalId, onSuccess) {

    if ($("#" + dropDownId).length) {

        $.ajax({
            type: "GET",
            dataType: "json",
            url: sourceUrl,
            async: false,
            beforeSend: function () {
                $("#" + dropDownId).empty();
            }
        }).done(function (data) {

            if (data) {

                $.each(data, function (key, dropDownOption) {
                    if (dropDownOption) {
                        var dataAttribute = "";
                        $.each(dropDownOption.extras, function (key, extra) {
                            dataAttribute += ' data-' + extra.key + '="' + extra.value + '" ';
                        });
                        var option = '<option value="' + dropDownOption.code + '"' + dataAttribute + '>' + dropDownOption.text + '</option>';
                        $("#" + dropDownId).append(option);
                    }
                });
            }

            if (keepOpen) {
                var list = $("#" + dropDownId).select2({
                    id: dropDownId,
                    dropdownPosition: 'below',
                    closeOnSelect: false
                }).on("select2:closing", function (e) {
                    e.preventDefault();
                }).on("select2:closed", function (e) {
                    list.select2("open");
                });
                $("#" + dropDownId).select2("open");
            }
            else {
                $("#" + dropDownId).select2({ id: dropDownId, multiple: isMultiple });
            }

            if (modalId) {
                $("#" + dropDownId).select2({ id: dropDownId, dropdownParent: $('#' + modalId) });
            }

            if (onSuccess && typeof onSuccess === 'function') {
                onSuccess(dropDownId, data);
            }

        }).always(function () {
            // self.close();
        });

        //$.alert({
        //    content: function () {
        //        var self = this;
        //        return


        //    }
        //});

    } else {
        alert('Drop down control with id="' + dropDownId + '" not found.');
    }


}

var maskMobileNumber = (e) => {
    var _mobileNo = $("#" + e + "").val();
    if (_mobileNo === '') {
        return;
    }
    var newVal = _mobileNo.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '-').replace(/[a-zA-Z]/, '');
    var isNumber = Number.isInteger(parseInt(newVal));
    if (isNumber) {
        if (newVal.length === 11) {
            var seprator = "-";
            var first = newVal.slice(0, 4);
            var second = newVal.slice(4, 11);
            var mobileNo = first + seprator + second;
            $("#" + e + "").val(mobileNo);
            AddVAlidationToControl(e, "hide", "", "")
            return true;
        }
        else {
            AddVAlidationToControl(e, "show", "Invalid mobile number", "bottom")
            return false;
        }
    }
}

function getPage(mUrl, callback, mMethod = "GET", payload, mDataType = "json") {

    console.log(mMethod + ': ' + mUrl);

    $.alert({
        content: function () {
            var self = this;
            return $.ajax({
                type: mMethod,
                dataType: mDataType,
                url: mUrl,
                data: payload
            }).done(function (mData) {
                try {
                    callback(JSON.parse(mData));
                    //console.log('Json response has been parsed successfully.');
                } catch (e) {
                    //console.log('Error while parsing json response.' + e);
                    callback(mData);
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                console.log(mMethod + ': ' + mUrl + ', ERROR: ' + errorThrown);
            }).always(function () {
                self.close();
            });
        }
    });
}


// #region General Approval Dialog

/* Constructor */
function approvalDialog(formName, transactionId, trandtlid) {
    $('#approval-dialog-formname').val(formName);
    $('#approval-dialog-transactionid').val(transactionId);
    $('#approval-dialog-trandtlid').val(trandtlid);
    $('#approval-dialog').modal('show');
}

/* model events OPEN */
$("#approval-dialog").on('shown.bs.modal', function () {
    var formName = $('#approval-dialog-formname').val();
    var transactionId = $('#approval-dialog-transactionid').val();
    var trandtlid = $('#approval-dialog-trandtlid').val();
    approvalDialogHandleOpen(formName, transactionId, trandtlid)
});

/* model events CLOSE */
$("#approval-dialog").on('hidden.bs.modal', function () {
    var formName = $('#approval-dialog-formname').val();
    var transactionId = $('#approval-dialog-transactionid').val();
    var trandtlid = $('#approval-dialog-trandtlid').val();
    approvalDialogHandleClose(formName, transactionId, trandtlid);
});

/* dialog button events */
function approvalDialogAction(actionName) {

    var formName = $('#approval-dialog-formname').val();
    var transactionId = $('#approval-dialog-transactionid').val();
    var trandtlid = $('#approval-dialog-trandtlid').val();
    var remarks = $('#approval-dialog-remarks').val();
    var attachmentId = approvalDialogAttachment();

    switch (actionName.toLowerCase()) {
        case 'approve':
            approvalDialogHandleApprove(formName, transactionId, trandtlid, remarks, attachmentId);
            break;
        case 'reject':
            approvalDialogHandleReject(formName, transactionId, trandtlid, remarks, attachmentId);
            break;
        case 'hold':
            approvalDialogHandleHold(formName, transactionId, trandtlid, remarks);
            break;
        case 'forward':
            approvalDialogHandleForward(formName, transactionId, trandtlid, remarks);
            break;
        case 'cancel':
            approvalDialogHandleCancel(formName, transactionId, trandtlid);
            break;
        case 'attachment':

            var options = {
                id: attachmentId
            }
            approvalDialogHandleAttachment(options);
            break;

        default:
            $.alert('Unrecognized approval action, Please contact IT support for help.', 'Unknown Approval Action');
    }
}
 
// #endregion

// #region General Attachment Dialog

(function (window) {

    function attachmentDialog() {

        var _attachmentDialog = {};

        var _attachmentDialogFiler;
        var _attachmentDialogFilerDefaultOptions;
        var _attachmentDialogFilerKit;

        var _options;
        var _onSave;
        var _onCancel;
        var _hasSubmited = false;

        _attachmentDialog.show = function (options, onSave, onCancel) {

            _options = options;

            if (!options.limit)
                _options.limit = 10;

            if (!options.maxSize)
                _options.maxSize = 100;

            if (!options.extensions)
                _options.extensions = ['jpg', 'pdf'];

            if (!options.mode)
                _options.mode = '';

            _onSave = onSave;
            _onCancel = onCancel;

            $('#attachment-dialog').modal('show');

        }

        /* private model events OPEN */
        $("#attachment-dialog").on('shown.bs.modal', function () {

            var deleteButton;
            var isDisabled;
            if (_options.mode === 'readonly') {
                deleteButton = '';
                isDisabled = true;
                $('#attachment-dialog-button-submit').css('display', 'none');
            } else {
                deleteButton = '<li><a class=\"icon-jfi-trash jFiler-item-trash-action\"></a></li>';
                isDisabled = false;
                $('#attachment-dialog-button-submit').css('display', 'inline-block');
            }

            _attachmentDialogFilerDefaultOptions = {
                showThumbs: true,
                allowDuplicates: false,
                addMore: true,
                disabled: isDisabled,
                templates: {
                    removeConfirmation: false,
                    item: '<li class=\"jFiler-item\" style=\"\"><div class=\"jFiler-item-container\"><div class=\"jFiler-item-inner\"><div class=\"jFiler-item-icon pull-left\">{{fi-icon}}</div><div class=\"jFiler-item-info pull-left\"><div class=\"jFiler-item-title\">{{fi-name}}</div><div class=\"jFiler-item-others\"><span>Size: {{fi-size2}}</span><span>Type: {{fi-type}}/{{fi-extension}}</span><span class=\"jFiler-item-status\"></span></div><div class=\"jFiler-item-assets\"><ul class=\"list-inline\">' + deleteButton + '</ul></div></div></div></div></li>',
                    itemAppend: '<li class=\"jFiler-item\" style=\"\"><div class=\"jFiler-item-container\"><div class=\"jFiler-item-inner\"><div class=\"jFiler-item-icon pull-left\">{{fi-icon}}</div><div class=\"jFiler-item-info pull-left\"><a target=\"_blank\" href=\"{{fi-url}}/view\"><div class=\"jFiler-item-title\">{{fi-name}}</div></a><div class=\"jFiler-item-others\"><span>Size: {{fi-size2}}</span><span>Type: {{fi-type}}/{{fi-extension}}</span><span class=\"jFiler-item-status\"></span></div><div class=\"jFiler-item-assets\"><ul class=\"list-inline\"><li><a href=\"{{fi-url}}/download\" download=\"{{fi-name}}\" class=\"jfi-download-button icon-jfi-download-o\"></a></li>' + deleteButton + '</ul></div></div></div></div></li>',
                }
            };

            if (_options.limit)
                _attachmentDialogFilerDefaultOptions.limit = _options.limit;

            if (_options.maxSize)
                _attachmentDialogFilerDefaultOptions.fileMaxSize = _options.maxSize;

            //if (_options.extensions)
            //    _attachmentDialogFilerDefaultOptions.extensions = _options.extensions;

            /* init filer */
            _attachmentDialogFilerKit = $("#attachment_dialog_filer").prop("jFiler");
            if (_attachmentDialogFilerKit) {
                _attachmentDialogFilerKit.reset();
            }
            else {
                _attachmentDialogFiler = $('#attachment_dialog_filer').filer(_attachmentDialogFilerDefaultOptions);
                _attachmentDialogFilerKit = $("#attachment_dialog_filer").prop("jFiler");
                $('.jFiler-input').css('width', '100%');
            }

            /* set readonly mode if given */
            if (_options.mode === 'readonly') {
                $('.jFiler-input, .icon-jfi-trash').css('display', 'none');
            } else {
                $('.jFiler-input').css('display', 'block');
            }

            /* get data if id is given */
            if (_options.id) {
                getPage('/AttachmentDialog/Get/' + _options.id, function (data) {
                    if (data && data.length > 0) {

                        for (var i = 0; i < data.length; i++) {
                            _attachmentDialogFilerKit.append(data[i]);
                        }

                        $('.jfi-download-button').each(function () {
                            $(this).attr('href', $(this).data('url'));
                        });

                    }
                });
            }


        });

        $("#attachment-dialog-button-submit").click(function () {


            var id = _options.id;

            var formData = new FormData();
            var filerKit = $("#attachment_dialog_filer").prop("jFiler");
            if (filerKit.files_list && filerKit.files_list.length > 0) {

                var filesToKeep = [];
                for (var i = 0; i < filerKit.files_list.length; i++) {
                    var file = filerKit.files_list[i].file;
                    if (file._choosed) {
                        formData.append('NewFiles', file);
                    } else {
                        filesToKeep.push(file.url.split('/').pop());
                    }

                }

                if (id) {
                    formData.append('id', id);
                    formData.append('filesToKeep', JSON.stringify(filesToKeep));
                }

                var url = '/AttachmentDialog/' + (id ? 'Update' : 'Create')
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        $('#attachment-dialog-button-submit').find('i').removeClass('fa-spin');
                    },
                    success: function (result) {
                        if (result) {
                            if (result.isSuccess) {
                                if (_onSave) {
                                    _onSave(result.id, _options);
                                }
                                _hasSubmited = true;
                                $('#attachment-dialog').modal('toggle');
                            }
                            else {
                                $.alert(result.message, result.title);
                            }
                        } else {
                            $.alert('Unknown response from server.', 'Unknown Response');
                        }
                    },
                    error: function () {
                        $.alert('Unable to upload your attachments. Please try again or contact MIS support team.', 'ERROR');
                    }
                });

            }
            else {

                if (id) {

                    $.confirm({
                        title: 'Delete All',
                        content: 'Do you want to remove all attachments?',
                        buttons: {

                            confirm: {
                                text: "No, Cancel",
                                btnClass: 'btn-success',
                                keys: ['enter'],
                                action: function () {
                                    // do nothing
                                },
                            },

                            cancel: {
                                text: "Yes, Delete All",
                                btnClass: 'btn-danger',
                                keys: ['esc'],
                                action: function () {
                                    //alert('impliment delete here');
                                }
                            }

                        }

                    });

                } else {

                    $.alert('There is no file to upload. Please add some files.', 'No Files Found');

                }

            }


        });

        $("#attachment-dialog").on('hide.bs.modal', function () {
            if (!_hasSubmited) {
                if (_onCancel) {
                    _onCancel(_options);
                }
            }
            _options = null;
            _onSave = null;
            _onCancel = null;
            _hasSubmited = false;
            _attachmentDialogFilerDefaultOptions = null;
            _attachmentDialogFiler = null;
            _attachmentDialogFilerKit = null;
        });



        return _attachmentDialog;
    }

    if (typeof (window.attachmentDialog) === 'undefined') {
        window.attachmentDialog = attachmentDialog();
    }

})(window);


// #endregion

//#region Window Dialog For Reporting

var fn_windowDialog = {};
var userSessionId; var _hfSessionBranchId; var _hfUserId; var _hfSessionLoginId;
$(function () {
    fn_windowDialog.reportDialog = function (url, title, w, h) {
        title = title == "" ? "Title" : title; w = w == "" ? "900" : w; h = h == "" ? "500" : h;

        // Fixes dual-screen position                             Most browsers      Firefox
        const dualScreenLeft = window.screenLeft !== undefined ? window.screenLeft : window.screenX;
        const dualScreenTop = window.screenTop !== undefined ? window.screenTop : window.screenY;

        const width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
        const height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

        const systemZoom = width / window.screen.availWidth;
        const left = (width - w) / 2 / systemZoom + dualScreenLeft
        const top = (height - h) / 2 / systemZoom + dualScreenTop
        const newWindow = window.open(url + '&S_Id=' + userSessionId + '&S_BranchId=' + _hfSessionBranchId.trim() + '&S_UserId=' + _hfUserId.trim() + '&S_LoginId=' + _hfSessionLoginId.trim(), title,
            `
            scrollbars=yes,
            width=${w / systemZoom}, 
            height=${h / systemZoom}, 
            top=${top}, 
            left=${left}
            `
        )
        //$.ajax({
        //    url: url,
        //    //data: {
        //    //    some_var: "Something",
        //    //    another_var: "Something else"
        //    //},
        //    // remember, this request is just returning the URL we need.
        //    success: function (data) {
        //        newWindow.location = data;
        //    }
        //});
        return false;
        //if (window.focus) newWindow.focus();
    }
})

//#endregion

 
 
//#region NIC
var isValid_NIC = (id) => {
    var _cnic = $("#" + id + "").val();
    if (_cnic === '') {
        return false;
    }
    var cnicNumber = _cnic.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '-').replace(/[a-zA-Z]/, '');

    if (!$.isNumeric(cnicNumber)) {
        $("#span_cnic").text('alphabet/space not allowed')
        $("#span_cnic").css('display', 'block');
        return false;
    }
    else {
        if (cnicNumber.length > 13) { cnicNumber = cnicNumber.substring(0, 13); }
        else cnicNumber = cnicNumber.padEnd(13, 0);

        var seprator = "-";
        var first = cnicNumber.slice(0, 5);
        var second = cnicNumber.slice(5, 12);
        var thrid = cnicNumber.slice(12);
        var _cnicNumber = first + seprator + second + seprator + thrid;
        $("#" + id + "").val(_cnicNumber);
        $("#span_cnic").css('display', 'none');
        return true;
    }
}
//#endregion

function __MessageBox(title, content, typeColor, btnText, btnColor, callbackData) {
    $.alert({
        title: title,
        content: content,
        type: typeColor,
        typeAnimated: true,
        buttons: {
            confirm: {
                text: btnText,
                btnClass: 'btn-' + btnColor,
                action: function () {
                }
            }
        }
    });
} 
 
function _AjaxRequest(URL, Model = null, DataType, Method) {
    var Result ;
        if (Model != null) {
            $.ajax({
                url: URL,
                data: Model,
                dataType: DataType,
                method: Method,
                async:true,
                success: function (response) {
                    Result = response;
                    // Handle successful response
                    return response;
                    console.log(response);
                },
                error: function (xhr, status, error) {
                    // Handle error response
                    return "Error: ", error;
                }
            });
        }
        else {
            $.ajax({
                url: URL,
                dataType: DataType,
                method: Method,
                async: false,
                success: function (response) {
                    console.log(response);
                    // Handle successful response
                    return response;
                },
                error: function (xhr, status, error) {
                    // Handle error response
                    return "Error: ", error;
                }
            });
        }
}
var response = "";
function _ConfirmAjaxRequest(Title, Content, URL, Model = null, DataType, Method, populateDt = null, clearTableRows = null,reloadPage=null) {
    response = "";
$.confirm({
    title: Title,
    content: Content,
    buttons: {
        confirm: function () {
            $.ajax({
                url: URL,
                data: Model,
                method: Method,
                dataType: DataType,
                success: function (result) {
                    console.log(result);
                    GetResponse(result)
                    if (result.toLowerCase().includes('successfully')) {
                        __MessageBox("Saved", result, "green", "Ok", function () { });
                        clearFields();
                        if (populateDt != null) {
                            populateDataTable(populateDt);
                        }
                        if (clearTableRows != null && clearTableRows != '') {
                            $('#' + clearTableRows + '').empty();
                        }
                        if (reloadPage != null) {
                            location.reload(true);
                        }
                    }
                    else {
                        __MessageBox("Error", "Error :" + result, "red", "Ok", function () { });
                    }
                },
                error: function (xhr, status, error) {
                    // Handle error response
                    __MessageBox("Error", "Error :" + error, "red", "Ok", function () { });
                }
            });
        },
        cancel: function () {
        },
    }

})
}
function GetResponse(get) {
    response = get;
    console.log("get",response);
}

function clearFields() {
    $("input[type=text]").each(function () {
        $('#' + this.id + '').val("");
    });
    $("input[type=number]").each(function () {
        $('#' + this.id + '').val("");
    });
    $("input[type=hidden]").each(function () {
        $('#' + this.id + '').val("");
    });
    $('input').prop('checked', false);
    $("select").each(function () {
        $('#' + this.id + '').val("");
    })
    
}

function OnCheckBoxChecked(name) {
    // Attach a change event listener to the checkboxes with the specific name
    $('input[type="checkbox"][name="'+name+'"]').change(function () {
        // Check if the checkbox is checked
        if ($(this).is(':checked')) {
            // Uncheck all checkboxes with the same name except the current one
            $('input[type="checkbox"][name="'+name+'"]').not(this).prop('checked', false);
        }
    });
}

function DisabledCheckedUncheckedChecbox(CheckBoxId) {
    $('#' + CheckBoxId + '').on('click', function (e) {
        e.preventDefault(); // Prevent the default checkbox behavior
        return false; // Return false to prevent checkbox state change
    });
}

function CheckedAtleastOneCheckboxCheckedByName(checkBoxName) {

    $('input[name="' + checkBoxName +'"]').on('click', function () {
        var atLeastOneChecked = false;
        $('input[name="' + checkBoxName +'"]').each(function () {
            if ($(this).prop('checked')) {
                atLeastOneChecked = true;
                console.log(atLeastOneChecked);
                return atLeastOneChecked; // exit the loop early
            }
        });
        if (atLeastOneChecked == true) {
            console.log('At least one checkbox is checked.');
            return true;
        } else {
            console.log('No checkboxes are checked.');
            atLeastOneChecked = false;
            return atLeastOneChecked;
        }
    });

}

function UncheckedAllCheckedBoxes() {
    $('input[type="checkbox"]').prop('checked', false);
}

function UncheckedOther(id) {
    if (id == "chkGoth") {
        $('#chkPublicSpaces').prop("checked", false);
        $('#chkLocalities').prop("checked", false);
        $('#chkCompanies').prop("checked", false);
    }
    else if (id == "chkLocalities") {
        $('#chkPublicSpaces').prop("checked", false);
        $('#chkCompanies').prop("checked", false);
        $('#chkGoth').prop("checked", false);
    }
    else if (id == "chkPublicSpaces") {
        $('#chkLocalities').prop("checked", false);
        $('#chkCompanies').prop("checked", false);
        $('#chkGoth').prop("checked", false);
    }
    else if (id == "chkCompanies") {
        $('#chkPublicSpaces').prop("checked", false);
        $('#chkLocalities').prop("checked", false);
        $('#chkGoth').prop("checked", false);
    }
}
