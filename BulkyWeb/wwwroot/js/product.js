var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "description", "width": "20%" },
            { "data": "author", "width": "10%" },
            { "data": "isbn", "width": "10%" },
            { "data": "listPrice", "width": "10%" },
            {
                "data": "price",
                "width": "10%"
            },
            {
                "data": "price50",
                "width": "5%"
            },
            {
                "data": "price100",
                "width": "5%"
            },
            {
                "data": "imageUrl",
                "render": function (data) {
                    if (data) {
                        return `<img src="${data}" style="width:50px; height:50px;" />`;
                    } else {
                        return "No Image";
                    }
                },
                "width": "5%"
            },
            {
                "data": "category.name",  
                "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-success btn-sm mx-1">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-danger btn-sm mx-1">
                                <i class="bi bi-trash-fill"></i>
                            </a>
                        </div>
                    `;
                },
                "width": "10%"
            }
        ]
    });
}
