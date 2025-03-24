function deleteAlbum(id) {
    
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
    })
}