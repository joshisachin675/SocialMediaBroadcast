
var IsExists = false;

$(document).ready(function () {
    $("#Email").on('blur', function () {
        debugger;
        var status = ValidateEmail("#Email", "Please enter valid email id.");
        if (status == true) {
            $.ajax({
                url: $_UrlGetUserByEmail,
                type: "Get",
                data: { email: $("#Email").val(), id: $('#currentUserId').val() },
                success: function (result) {
                    debugger;
                    if (result == "exists") {
                        BootstrapDialog.alert('Email already exists');
                        IsExists = true;
                        $('#Email').css('border', '1px red solid').attr('title', 'Email already exists');
                    }
                    else {
                        IsExists = false;
                        $('#Email').css('border', '1px solid green').removeAttr('title');
                    }
                },
            });
        }
    });


    $("#Shortname").on('blur', function () {
        debugger;
        var isValid = true;
        var urlnew = $_BaseUrl + '/Users/api/AdminApi/CheckExistingShortName';
        var shortname = $("#Shortname").val();
        $.ajax({
            url: urlnew,
            type: 'GET',
            data: { shortname: shortname, UserId: parseInt($('#currentUserId').val()) },
            // async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                debugger;
                if (data == "exists") {
                    isValid = false;
                    $("#registerSubmit").prop('disabled', true);
                    $("#registerSubmit").css("background", "#69c4e2")
                    $("#shortnameerror").html("Short Name already exists! Please choose other one!");
                    return isValid;
                }
                else {
                    $("#registerSubmit").prop('disabled', false);
                    $("#registerSubmit").css("background", "#13bef6");
                    $("#shortnameerror").html("");
                    return isValid;
                }
            },
            error: function () {

            }
        });

    })


});


function ValidateForm() {
    debugger;
    var isValid = true;
    var isEmailValid = true;
    var isPasswordValid = true;
    var isShortName = true;
    $("#ConfirmPassword").css("border")
    var regExp = /^(?=.*\d)(?=.*[A-Z]).{8,12}$/;
    if (!ValidateEmail("#Email", "Please enter valid email id.")) {
        return false;
    }
    if (CheckRequired("#FirstName", "Name is required") != true)
        isValid = false;
    if (CheckRequired("#LastName", "Name is required") != true)
        isValid = false;

    if (CheckRequired("#Email", "Email is required") != true) {
        isValid = false;
        isEmailValid = false;
    }

    if (CheckRequired("#Shortname", "Short Name is required") != true) {
        isValid = false;
        isShortName = false;
    }

    if ($("#Password").val() != "") {
        if ($("#Password").val() == $("#ConfirmPassword").val()) {
            isValid = true;
            isPasswordValid = true;
            $("#ConfirmPassword").removeClass("errorClass");
            $("#ConfirmPassword").attr("title", "");
        }
        else {
            isValid = false;
            isPasswordValid = false;
            ValidateRepassword("#ConfirmPassword", "#Password", "Password and confirm password don't match")
            $("#Password").removeClass("errorClass");
        }
    }

    if (isEmailValid) {
        debugger;
        if (ValidateEmail("#Email", "Email id is not valid") != true)
            isValid = false;
    }

    if (IsExists == true || isShortName == false) {
        isValid = false;
    }
    else {
        isValid = true;
    }

    if (isValid == true)
        return true;
    else
        return false;
}


function AfterUpdate(result) {
    debugger;
    if (result.Response == "success") {
        var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
        BootstrapDialog.configDefaultOptions({cssClass: 'bstDialg'});
        BootstrapDialog.show({ message: 'The record has been updated successfully.'   });
        // BootstrapDialog.alert('The record has been updated successfully.');
    }
    else if (result.Response == "newPassword") {
        // BootstrapDialog.alert('The record has been updated successfully.');
        var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
        BootstrapDialog.show({ message: 'The record has been updated successfully.' });
    }
    else if (result.Response == "AlreadyExists") {
        ShowStaus('Email Id already in use', 'errorMsg');
        setTimeout(function () {
        }, 1000)
    }
    else if (result.Response == "NoUpdate") {
        ShowStaus('No Record Updated', 'errorMsg');
        setTimeout(function () {
        }, 1000)
    }
    else {

        ShowMessage(BootstrapDialog.TYPE_DANGER, "There seems to be an error on this page, Sorry for the inconvenience. Our tech team was notified and we'll do our best to get this sorted out asap.<br><br>Thank you for your patience and understanding.<br><br>CRWork Team");

    }
}
function ShowMessage(type, message) {
    $messageData = $("<span>Error</span>");
    var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
    BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
    BootstrapDialog.show({
      title: $messageData,
      type: type,
      message: message,
    });



    //BootstrapDialog.show({
    //    title: $messageData,
    //    type: type,
    //    message: message,
    //    closable: true,
    //    closeByBackdrop: false,
    //    closeByKeyboard: false,
    //    buttons: [{
    //        label: 'Ok',
    //        action: function (dialogItself) {
    //            dialogItself.close();
    //        }
    //    }]
    //});
}