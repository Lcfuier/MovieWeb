let dataTable;

$(document).ready(() => {
    dataTable = $('#datatable-episode').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/admin/movieEpisode/GetAllEpisode"
        },
        "columns": [
            { "data": "movieName", "width": "30%" },
            { "data": "movieDetailName", "width": "30%" },
            {
                "data": "actorId",
                "render": (data) => {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white"
                                href="/admin/movieEpisode/upsert/${data}">
                                <span class="fas fa-edit"></span>&nbsp;Sửa
                            </a>
                            <a class="btn btn-danger text-white"
                                onclick=Delete("/admin/movieEpisode/deleteEpisode/${data}")>
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