
$(document).ready(function () {

    function sendSwitchToggleRequest(id) {
        $.ajax({
            type: "GET",
            url: "http://192.168.0.25:5000/Switch/Toggle/" + id,
            success: function (data) {

                if (document.getElementById(id).src.includes("_d_")) {
                    document.getElementById(id).src = document.getElementById(id).src.replace("_d_", "_t_");
                }
                else {
                    document.getElementById(id).src = document.getElementById(id).src.replace("_t_", "_d_");
                }

            },
            error: function () {
                alert("Error");
            }
        });
    }

    $(function () {
        $('#btnSubmit').click(function (event) {

        });
    });

    $(".switch").on("click", function () {
        id = $(this).attr("id");
        console.log(id);
        event.preventDefault();
        sendSwitchToggleRequest(id);
    });
});