const no_award_image = "/Images/no_award_image.png";
const no_avatar = "/Images/no_avatar.png";

$(document).ready(onReady);

function onReady() {
    $('.item').click(ItemEdit);
    $('.deleteItem').click(DeleteItem);
    $('#modal_file').change(UploadImage);
    $('#modal_save').click(ItemSave);
    $('.modal_list_plus').click(AddLink);
}

function ItemEdit() {
    let id = this.id;

    $('#modal_itemId').val(id);
    if ($(this).parent().parent().attr('id') == "usersList") { // если ткнули на юзера
        $('#modal_itemType').val("user");
        $('#modal_award_sect').hide();
        $('#modal_user_sect').show();
        $('#modal_user_image').attr('src', no_avatar);

        $('#modal_title').html("Пользователь");

        if (id === "") {
            $('#birthDate').val("");
            $('#modal_age').val("");
            $('#modal_user_name').val("");
            $('#modal_birthday').val("");
        }
        else {
            $.getJSON('/Pages/getUserInfo.cshtml', { 'id': id }, function (data) {
                $('#modal_user_name').val(data['Name']);
                $('#modal_birthday').val(data['DateOfBirth']);
                $('#modal_age').val(data['Age']);
                if (data['Image'] != null)
                    $('#modal_user_image').attr('src', data['Image']);
            });
        }

        $.post("/Pages/avardsListModal.cshtml", // Получаем ачивки юзера
            { Id: id },
            function (data) {
                $('#modal_user_awards').prepend(data);
            });
    }
    else { // если ткнули на ачивку
        $('#modal_itemType').val("award");
        $('#modal_user_sect').hide();
        $('#modal_award_sect').show();
        $('#modal_award_image').attr('src', no_award_image);

        $('#modal_title').html("Награда");

        if (id == "") {
            $('#modal_award_title').val("");
        }
        else {
            $.getJSON('/Pages/getAwardInfo.cshtml', { 'id': id }, function (data) {
                $('#modal_award_title').val(data['Title']);
                if (data['Image'] != null)
                    $('#modal_award_image').attr('src', data['Image']);
            });
        }
    }

    $("#itemEditModal").modal('show');
}

function UploadImage() {
    if (this.files && this.files[0]) {
        let imgParentDiv;
        if ($('#modal_itemType').val() == 'user')
            imgParentDiv = $('#modal_user_image').parent();
        else
            imgParentDiv = $('#modal_award_image').parent();

        let max_width = $(imgParentDiv)[0].offsetWidth;
        let max_height = $(imgParentDiv)[0].offsetHeight;

        let reader = new FileReader();

        reader.readAsDataURL(this.files[0]);
        reader.onload = function (e) {
            let img = new Image(); // Масштабируем

            img.src = event.target.result;
            img.onload = () => {
                // Масштабируем под новые размеры, сохраняя пропорции
                let scaleFactor = img.height / img.width;
                let new_width = max_width;
                let new_height = new_width * scaleFactor;
                if (new_height > max_height) {
                    new_height = max_height;
                    new_width = new_height / scaleFactor;
                }
                /////////////////////////////////////////////////////
                let elem = document.createElement('canvas');
                elem.width = new_width;
                elem.height = new_height;
                let ctx = elem.getContext('2d');

                ctx.drawImage(img, 0, 0, new_width, new_height);

                if ($('#modal_itemType').val() == 'user')
                    $('#modal_user_image').attr('src', elem.toDataURL());
                else
                    $('#modal_award_image').attr('src', elem.toDataURL());
            }
        }
    }
}

function DeleteItem() {
    event.stopPropagation();
    let id = $(this).parent().attr('id');
    let name = $(this).siblings('.itemName').html();

    if ($(this).parent().parent().parent().attr('id') == "usersList") { // если ткнули на юзера
        type = "user";
    }
    else {
        type = "award";
    }

    if (type == "user")
        $('#confirm_body').html("Вы действительно хотите удалить пользователя " + name + "?");
    else
        $('#confirm_body').html("Вы действительно хотите удалить эту награду?<br>Награда \"" + name + "\" удалится у всех пользователей.");

    $('#confirm_delete_but').click(function () {
        $.post("/Pages/itemDelete.cshtml",
            {
                Type: type,
                Id: id
            },
            function (data) {
                if (data == "")
                    $('#toast_body').html("Успешно удалено");
                else
                    $('#toast_body').html(data);
                $('#confirm_delete').modal('hide');
                $('.toast').toast('show');
                if (type == 'user')
                    UpdateItemsList('user');
                else
                    UpdateItemsList('award');

            });
    })
    $('#confirm_delete').modal('show');
}

function ItemSave() {
    let type = $('#modal_itemType').val();
    let id = $('#modal_itemId').val();
    let name;
    let image;

    let data = {};

    if (type == "user") {
        let birthday = $('#modal_birthday').val();
        let age = $('#modal_age').val();
        image = $('#modal_user_image').attr('src');
        if (image == no_avatar)
            image = null;
        name = $('#modal_user_name').val();

        data.DateOfBirth = birthday;
        data.Age = age;
        data.Image = image;
    }
    else {
        image = $('#modal_award_image').attr('src');
        if (image == no_award_image)
            image = null;
        name = $('#modal_award_title').val();
    }

    data.Type = type;
    data.Id = id;
    data.Name = name;
    data.Image = image;

    $.post("/Pages/itemSave.cshtml",
        data,
        function (data) {
            if (data == "") {
                $("#itemEditModal").modal('hide');
                $('#toast_body').html("Успешно сохранено");
                if (type == 'user')
                    UpdateItemsList('user');
                else
                    UpdateItemsList('award');
            }
            else
                $('#toast_body').html(data);
            $('.toast').toast('show');
        });
}

function UpdateItemsList(type) {
    if (type == 'user') {
        $.get("/Pages/usersListPartial.cshtml",
            null,
            function (data) {
                $('#users').html(data);
                $('.item').click(ItemEdit);
                $('.deleteItem').click(DeleteItem);
            });
    }
    else {
        $.get("/Pages/awardsListPartial.cshtml",
            null,
            function (data) {
                $('#awards').html(data);
                $('.item').click(ItemEdit);
                $('.deleteItem').click(DeleteItem);
            });
    }
}

function AddLink() {
    
}