$(document).ready(onReady);

function onReady() {
    $('.item').click(ItemEdit);
    $('#modal_file').change(UploadImage);
    $('#modal_save').click(ItemSave);
}

function ItemEdit() {
    let id = this.id;

    if ($(this).parent().attr('id') == "usersList") { // если ткнули на юзера
        $('#modal_itemType').val("user");
        $('#modal_itemId').val(id);
        $('#modal_award_sect').hide();
        $('#modal_user_sect').show();

        $('#modal_title').html("Пользователь");

        if (id === "") {
            $('#birthDate').val("");
            $('#modal_age').val("");
            $('#modal_user_name').val("");

        }
        else {
            $.getJSON('/Pages/getUserInfo.cshtml', { 'id': id }, function (data) {
                $('#modal_user_name').val(data['Name']);
                $('#modal_birthday').val(data['DateOfBirth']);
                $('#modal_age').val(data['Age']);
                if (data['Image'] != "")
                    $('#modal_user_image').attr('src', '~/Images/Avatars/' + data['Image']);
            });
        }
    }
    else { // если ткнули на ачивку
        $('#modal_itemType').val("award");
        $('#modal_user_sect').hide();
        $('#modal_award_sect').show();

        $('#modal_title').html("Награда");

        if (id === "") {
            $('#modal_award_title').val("");

        }
        else {
            $.getJSON('/Pages/getAwardInfo.cshtml', { 'id': id }, function (data) {
                $('#modal_award_title').val(data['Title']);
                if (data['Image'] != "")
                    $('#modal_user_image').attr('src', '~/Images/Avards/' + data['Image']);
            });
        }
    }

    $("#itemEditModal").modal('show');
}

function UploadImage() {
    if (this.files && this.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            $('#modal_user_image').attr('src', e.target.result);
        }

        reader.readAsDataURL(this.files[0]);
    }
}

function ItemSave() {
    //console.log($('#modal_itemType').val() + ' ' + $('#modal_itemId').val());
    if ($('#modal_itemType').val() == "user") {
        let id = $('#modal_itemId').val();
        let name = $('#modal_user_name').val();
        let DateOfBirth = $('#modal_birthday').val();
        let Age = $('#modal_age').val();
        let Image = $('#modal_age').val();


        $.post("demo_test_post.asp",
            {
                Id: id,
                Name: name,
                DateOfBirth: 
            },
            function (data, status) {
                alert("Data: " + data + "\nStatus: " + status);
            });
    }
}