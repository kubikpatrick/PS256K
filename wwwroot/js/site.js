function deleteAlbum(id) {
    $.ajax({
        url: "/albums/delete/@Model.Id",
        type: "DELETE",
        success: function(response) {
            window.location = "/albums";
        },
        error: function(xhr, options, error) {
            alert(error);
        }
    });
}

function deletePicture(id) {
    $.ajax({
        url: `/pictures/delete/${id}`,
        type: "DELETE",
        success: function(response) {
            window.location.reload();

        },
        error: function(xhr, options, error) {
            alert(error);
        }
    });
}