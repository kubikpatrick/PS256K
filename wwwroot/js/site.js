function deleteProject(id) {
    $.ajax({
        url: `/projects/delete/${id}`,
        type: "DELETE",
        success: function(response) {
            window.location = "/projects";
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

function deleteCustomer(id) {
    $.ajax({
        url: `/customers/delete/${id}`,
        type: "DELETE",
        success: function(response) {
            document.querySelector(`#customer-${id}-column`).remove();
        },
        error: function(xhr, options, error) {
            alert(error);
        }
    });
}

function createProject(customerId) {
    $.ajax({
        url: `/projects/create/${customerId}`,
        type: "POST",
        data: {
            "Name": document.querySelector("#Name").value,
        },
        success: function(response) {
            window.location = `/projects/edit/${response.id}`;
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