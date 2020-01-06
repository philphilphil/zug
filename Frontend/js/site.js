
$(document).ready(function () {

    function sendSwitchToggleRequest(id) {
        $.ajax({
            type: "GET",
            url: "http://192.168.0.25:5000/Switch/Toggle/" + id,
            success: function (data) {

                if (document.getElementById(id).src.includes("_d_")) {
                    document.getElementById(id).src = "static/images/switches_t_" + id + ".jpg";
                }
                else {
                    document.getElementById(id).src = "static/images/switches_d_" + id + ".jpg";
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