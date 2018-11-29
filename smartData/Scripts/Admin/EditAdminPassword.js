
function EditAdminPasswordSuccess(data) {

    if (data.status == true) {
        debugger
        $("#editPassword").hide();
       // $('#loadingmessage').addClass("hidden");
        ShowMessage(BootstrapDialog.TYPE_SUCCESS, 'Record updated successfully.');
        RefreshGrid();
    }
    else {
        debugger
        $("#editPassword").hide();
       // $('#loadingmessage').addClass("hidden");
        ShowMessage(BootstrapDialog.TYPE_ERROR, 'There was some error occured while updating record.');
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
                $("#editPassword").hide();
                $(".modal-backdrop").remove();
            }
        }]
    });
}

function RefreshGrid() {
    $("#Password").val("");
    $("#ConfirmPassword").val("");
    document.getElementById("btnFilter").click();
}


$(document).ready(function () {

})