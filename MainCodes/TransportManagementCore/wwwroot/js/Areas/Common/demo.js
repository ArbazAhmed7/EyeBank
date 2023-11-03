

var mTable; 

function l(message) {
    if (isDebugging)
        console.log(message);
}

function initLookupScript() {
    l("Inventory: initlizing script inv.js");
    initLookupTable();
}

function initLookupTable() {


    l("initTable()");

    if (lookupTable != null) {

        window.$.ajax({
            type: "GET",
            dataType: "json",
            url: '/demo/json',
            success: function (mData) {

                l("AJAX request in sucess for data table.");
                //l(mData.data);

                if (mData) {

                    l("AJAX request in sucess and we have data.");

                    mTable = window.$('#table-lookup').DataTable({
                        data: mData.data,
                        select: 'multi',
                        order: [[0, 'asc']],
                        'columnDefs': [
                            {
                                'targets': [0],
                                'data': 'itemCode'

                            },
                            {
                                'targets': [1],
                                'data': 'qty'
                            },
                            {
                                'targets': [2],
                                'data': 'rate'
                            },
                            {
                                'targets': [3],
                                'data': 'val'
                            }
                        ],
                        processing: true,
                        "lengthMenu": [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]]
                    });


                } else {

                    l("AJAX request in sucess but data is null.");

                }

            },
            complete: function () {

                l("AJAX request in completed.");

            },
            error: function (request, status, error) {

                l("AJAX request in failed.");

            }

        });

    }




}









