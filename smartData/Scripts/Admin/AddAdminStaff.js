$(document).ready(function () {

});

function Validate() {
    debugger;
    var isValid = true;
    var email = $("#UserName").val();
    var userType = $("#hdnuserType").val();
    var IndustryId = $("#hdnindustryId").val();
    var industry = "";
    if (userType == 2) {
        industry = IndustryId;
    }
    else {
        industry = $("option:selected", $("#industry")).val();
    }     
    $.ajax({
        url: $_UrlGetUserByEmailandIndustryId,
        type: "Get",
        data: { email: email, industry: industry },
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;                  
            if (data == "exists") {
                isValid = false;
                $messageData = $("<span>Information</span>");
                BootstrapDialog.show({
                    title: $messageData,
                    type: BootstrapDialog.TYPE_DANGER,
                    message: "Already exists!",
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
            }
            else {
                isValid = true;
            }
            return isValid;
        },
       
    });
    return isValid;
    if (isValid == false) {
        
    }
}

function AddAdminSuccess(data) {

    if (data.status == true) {
        debugger
        $("#addStaff").modal("hide");
        $('#loadingmessage').addClass("hidden");
        ShowMessage(BootstrapDialog.TYPE_SUCCESS, 'Registration is successful. An email has been sent to registered email containing password.');
        RefreshGrid();
    }
    else {
        debugger
        $("#addStaff").modal("hide");
        $('#loadingmessage').addClass("hidden");
        if (data.message == "PlanExpired") { ShowMessage(BootstrapDialog.TYPE_DANGER, 'Your plan has been expired, Please update your plan to use the service.'); }
        else if (data.message == "UserCountExceeded") { ShowMessage(BootstrapDialog.TYPE_DANGER, 'Your have reached to add user count, upgrade your plan to add more users.'); }
        else if (data.message == "Error") { ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.'); }
        RefreshGrid();
    }

}


function ShowMessage(type, message) {
    $messageData = $("<span>Information</span>");
    BootstrapDialog.show({
        title: $messageData,
        type: type,
        message: message,
        closable: true,
        closeByBackdrop: false,
        closeByKeyboard: false,
        buttons: [{
            label: 'Ok',
            action: function (dialogItself) {
                dialogItself.close();
                RefreshGrid();
                $("#addStaff").modal('hide');
                $(".modal-backdrop").remove();
            }
        }]
    });
}

function RefreshGrid() {
    $("#FirstName").val("");
    $("#LastName").val("");
    $("#UserName").val("");
    $('#industry').val("");
    $("#Password").val("");
    $("#ConfirmPassword").val("");
    document.getElementById("btnFilter").click();
}
