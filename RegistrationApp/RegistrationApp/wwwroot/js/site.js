// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function CheckAvailability() {
    var data = $("#FullName").val();
    $.ajax({
        type: "POST",
        url: "/Home/CheckFullName",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (response) {
            var message = $("#message");
            if (response) {

                console.log(response);
                message.css("color", "red");
                message.html("Full name is already exist");
                $('#submit').attr('disabled', 'disabled');
            }
            else {
                message.css("color", "green");
                message.html("Full name is available");
                $('#submit').removeAttr('disabled');
            }
        }
    });
};
function ClearMessage() {
    $("#message").html("");
};