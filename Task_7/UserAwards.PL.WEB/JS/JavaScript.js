$(document).ready(onReady);

function onReady() {
    $('.item').click(ItemEdit);
}

function ItemEdit() {
    let id = this.id;
    if (id === "") {
        $('#birthDate').val("");
    }
    else {
        $.getJSON('/Pages/getUserInfo.cshtml', { 'id': id }, function (data) {
            $('#birthDate').val(data['DateOfBirth']);
        });
    }
    $("#itemEditModal").modal('show');
}