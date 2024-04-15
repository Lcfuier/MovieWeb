let dataTable;

$(document).ready(() => {
    dataTable = $('#datatable-category').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/admin/actor/getAllActor"
        },
        "columns": [
            { "data": "actorName", "width": "60%" },
            {
                "data": "actorId",
                "render": (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white"
                                href="/admin/actor/upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white"
                                onclick=Delete("/admin/actor/deleteActor/${data}")>
                                <span class="fas fa-trash-alt"></span>&nbsp;Xóa
                            </a>
                        </div>
                        `;
                },
                "width": "40%"
            }
        ]
    });
});