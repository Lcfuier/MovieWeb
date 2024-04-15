$('#movie-upsert-form').submit(function (event) {
    // Check if at least one checkbox is selected
    const isActorChecked = $('input[name="movieDto.ActorIds"]:checked').length > 0;
    const isGenreChecked = $('input[name="movieDto.GenreIds"]:checked').length > 0;

    // If no checkbox is selected, display an error message and prevent form submission
    if (!isActorChecked ) {
        event.preventDefault();
        $('#actor-error').text("Vui lòng chọn ít nhất 1 diễn viên.");
    }
    if (!isGenreChecked) {
        event.preventDefault();
        $('#category-error').text("Vui lòng chọn ít nhất 1 thể loại.");
    }
});