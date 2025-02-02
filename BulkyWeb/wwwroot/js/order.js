


//when document load
$(document).ready(
    
    function () {
        var url = window.location.search;

        if (url.includes("inprocess"))       loadDataTable("inprocess");
        else if (url.includes("pending")) loadDataTable("pending");
        else if (url.includes("cancelld")) loadDataTable("cancelld");
        else if (url.includes("completed"))  loadDataTable("completed");
        else if (url.includes("approved"))   loadDataTable("approved");
        else                                 loadDataTable("all");
    }

);


var dataTable;
function loadDataTable(status) {

    dataTable =
        $('#tblData').DataTable({

            "ajax": { url: '/admin/order/getall?status=' + status},
            "columns": [
                { data: 'name' , "width" : "20%"},
                { data: 'streetAddress', "width": "10%" },
                { data: 'phoneNumber', "width": "15%" },
                { data: 'orderStatus', "width": "10%" },
                { data: 'orderTotal', "width": "10%" },
                {
                    data: 'id',
                    "render": function (data) {
                        return `<div class="btn-group w-100 text-center" role="group">

                            <a href="/admin/order/detail?id=${data}" class="btn btn-primary m-1 w-auto"> <i class="bi bi-pencil-square"></i> Edit</a>

                        </div>`
                    },
                    "width": "30%"
                }
            ]
    });
}





