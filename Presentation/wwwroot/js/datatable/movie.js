let dataTable;

$(document).ready(() => {
    dataTable = $('#datatable-movie').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/admin/movie/getAllMovie"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "actors", "width": "20%" },
            { "data": "genres", "width": "20%" },
            {
                "data": "movieId",
                "render": (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white"
                                href="/admin/movie/upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white"
                                onclick=Delete("/admin/movie/deleteMovie/${data}")>
                                <span class="fas fa-trash-alt"></span>&nbsp;Xóa
                            </a>
                        </div>
                        `;
                },
                "width": "20%"
            }
        ]
    });
});