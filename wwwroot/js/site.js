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
            document.querySelector(`#picture-${id}-column`).remove();
        },
        error: function(xhr, options, error) {
            alert(error);
        }
    });
}

function scrollToTop() {
    window.scrollTo({ 
        top: 0, 
        behavior: "smooth" 
    });
}