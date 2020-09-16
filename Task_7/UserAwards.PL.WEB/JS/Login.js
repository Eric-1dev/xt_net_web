$(document).ready(onReady);

function onReady() {
    $('#loginButton').click(Login);
    $('#registrationButton').click(Registration);
}

function Login() {
    $.post("/Pages/loginLogic.cshtml",
        {
            Login: $('#login').val(),
            Password: $.md5($('#password').val())
        }, function (data) {
            if (data == "")
                window.location.href = "/";
            else
                $('#result').html(data);
        });
}

function Registration() {
    $.post("/Pages/registerLogic.cshtml",
        {
            Login: $('#login').val(),
            Password: $.md5($('#password').val())
        }, function (data) {
            if (data == "")
                $('#result').html("Успешно зарегистрирован. Войдите под своим паролем.")
            else
                $('#result').html(data);
        });
}