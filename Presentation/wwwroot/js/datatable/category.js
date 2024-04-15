let dataTable;

$(document).ready(() => {
    dataTable = $('#datatable-category').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/admin/genre/getAllGenre"
        },
        "columns": [
            { "data": "genreName", "width": "60%" },
            {
                "data": "genreId",
                "render": (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white"
                                href="/admin/genre/upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white"
                                onclick=Delete("/admin/genre/deleteGenre/${data}")>
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