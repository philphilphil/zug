
$(document).ready(function () {

    function sendSwitchToggleRequest(id) {
        $.ajax({
            type: "GET",
            url: "http://192.168.0.25:5002/api/switch/toggleswitch?id=" + id,
            success: function (data) {

                if (document.getElementById(id).src.includes("_d_")) {
                    document.getElementById(id).src = "images/switches_t_" + id + ".jpg";
                }
                else {
                    document.getElementById(id).src = "images/switches_d_" + id + ".jpg";
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