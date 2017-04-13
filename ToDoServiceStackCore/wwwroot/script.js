var uri = 'http://localhost:58378';
$(function() {

    var getUsers = $.ajax({
        url: uri + "/users?format=json",
        method: "GET",
        dataType: "json"
    });
    getUsers.done(function(resp) {
        $.each(resp.users, function(i, user) {
            $("#users").append("<tr><td>" + user.firstName + "</td><td>" + user.lastName + "</td><td><a href='task.html?userId=" + user.id + "'>1</a></td></tr>");
        });
    });    
    getUsers.fail(function(jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });

    $("#addUser").click(function() {
        $("#addUserModal").modal();
    });

    $("#addUserButtonOK").click(function() {
        var postUser = $.ajax({
            url: uri + "/user",
            method: "POST",
            dataType: "json",
            data: {
                firstName: $("#firstName").val(),
                lastName: $("#lastName").val()
            }
        });
        postUser.done(function(resp) {
            alert("Request!: " + resp);
        });
        postUser.fail(function(jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    });
});
