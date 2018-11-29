
    //function ValidateForm() {
    //    debugger;
    //    var isValid = true;
    //    var urlnew = $_BaseUrl + '/Users/api/AdminAPI/GetUserByEmailInd';
    //    var email = $("#UserName").val();
    //    var industry = $("option:selected", $("#industry")).text();
    //    $.ajax({
    //        url: urlnew,
    //        type: 'GET',
    //        data: { email: email, industry: industry },
    //        // async: false,
    //        contentType: "application/json; charset=utf-8",
    //        success: function (data) {
    //            debugger;
    //            alert(data);                
    //            if (data == "exists") {
    //                isValid = false;
    //                $messageData = $("<span>Information</span>");
    //                BootstrapDialog.show({
    //                    title: $messageData,
    //                    type: BootstrapDialog.TYPE_DANGER,
    //                    message: "You cannot add admin with same industry",
    //                    closable: true,
    //                    closeByBackdrop: false,
    //                    closeByKeyboard: false,
    //                    buttons: [{
    //                        label: 'Ok',
    //                        action: function (dialogItself) {
    //                            dialogItself.close();
    //                        }
    //                    }]
    //                });
    //                return isValid;
    //            }
    //            else {
    //                return isValid;
    //            }              
    //        },
    //       error : function (){
    //           alert("errpr")
    //        }
    //    });             
    //}


function AddUserSuccess(data) {
    debugger;
    $messageData = $("<span>Information</span>");
    if (data.status == true) {
        debugger;
        BootstrapDialog.show({
            title: $messageData,
            type: BootstrapDialog.TYPE_SUCCESS,
            message: "Your account has been registered successfully! Please login to continue.",
            closable: true,
            closeByBackdrop: false,
            closeByKeyboard: false,
            buttons: [{
                label: 'Ok',
                action: function (dialogItself) {
                    debugger;
                    dialogItself.close();
                    window.location.href = $_BaseUrl + '/Users/Login';
                }
            }]
        });
    }
    else {

    }

}

$(document).ready(function () {
    $("#industry").on('blur', function () {
        debugger;
        var isValid = true;
         var urlnew = $_BaseUrl + '/Users/api/AdminAPI/GetUserByEmailInd';
         var email = $("#UserName").val();
        var industry = $("option:selected", $("#industry")).text();
        $.ajax({
            url: urlnew,
            type: 'GET',
            data: { email: email, industry: industry },
            // async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                debugger;
                if (data == "exists") {
                    isValid = false;
                    $("#registerSubmit").prop('disabled', true);
                    $("#registerSubmit").css("background", "#69c4e2")
                    $messageData = $("<span>Information</span>");
                    BootstrapDialog.show({
                        title: $messageData,
                        type: BootstrapDialog.TYPE_DANGER,
                        message: "You cannot add user with same industry",
                        closable: true,
                        closeByBackdrop: false,
                        closeByKeyboard: false,
                        buttons: [{
                            label: 'Ok',
                            action: function (dialogItself) {
                                dialogItself.close();
                            }
                        }]
                    });
                    return isValid;
                }
                else {
                    $("#registerSubmit").prop('disabled', false);
                    $("#registerSubmit").css("background", "#13bef6");
                    return isValid;
                }
            },
            error: function () {             
            }
        });

    })


    $("#ShortName").on('blur', function () {
        debugger;
        var isValid = true;
        var urlnew = $_BaseUrl + '/Users/api/AdminAPI/CheckExistingShortName';
        var shortname = $("#ShortName").val();      
        $.ajax({
            url: urlnew,
            type: 'GET',
            data: { shortname: shortname },
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

})

