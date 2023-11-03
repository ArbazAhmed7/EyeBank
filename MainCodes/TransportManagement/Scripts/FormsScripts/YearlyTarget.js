function pageLoad() {    
    TargetIdAutoComplete();
}

function TargetIdAutoComplete ()
{
    $("[id$=txtTargetID]")
        .blur(function () {
            var keyEvent = $.Event("keydown");
            keyEvent.keyCode = $.ui.keyCode.ENTER;
            $(this).trigger(keyEvent);
        })
        .autocomplete({
        autoFocus: true,
        source: function (request, response) {
            $.ajax({
                url: "YearlyTarget.aspx/AutoComplete",
                data: "{'Term' :'" + request.term + "','TermType' :'TargetAutoID','Id' :'0'}",
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
            $("[id$=txtTargetID]").val(ui.item.label);
            $("[id$=hfTargetIDPKID]").val(ui.item.val);
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