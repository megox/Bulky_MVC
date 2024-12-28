﻿$(document).ready(function () {
    loadDataTable();
});

var dataTable;
function loadDataTable() {
    dataTable =  $('#tblData').DataTable({
        "ajax": { url:'/admin/product/getall'},
        "columns": [
            { data: 'title' , "width" : "20%"},
            { data: 'isbn', "width": "10%" },
            { data: 'price', "width": "10%" },
            { data: 'author', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="btn-group w-100 text-center" role="group">

                        <a href="/admin/product/upsert?id=${data}" class="btn btn-primary m-1 w-auto"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Delete('/admin/product/delete/${data}') class="btn btn btn-danger m-1 w-auto"> <i class="bi bi-trash"></i> Delete</a>

                    </div>`
                },
                "width": "30%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });


}





