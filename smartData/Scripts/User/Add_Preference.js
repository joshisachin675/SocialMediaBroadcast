

function AddPreferenceSuccess(data) {
    debugger
   
    if (data.status == true) {
        $("#addPreferences").modal("hide");
        $(".modal-backdrop").remove();
        ShowMessage(BootstrapDialog.TYPE_SUCCESS, "Preference added successfully");
        RefreshGrid();
        $('#loadercontainingdiv').addClass("hidden");
    }
    else {
        $("#addPreferences").modal("hide");
        $(".modal-backdrop").remove();
        $('#loadercontainingdiv').addClass("hidden");
        ShowMessage(BootstrapDialog.TYPE_DANGER, "Error occured");
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
                $("#addPreferences").modal("hide");
                $(".modal-backdrop").remove();
            }
        }]
    });
}

function RefreshGrid() {
    $("#Preference", $('#AddPreference')).val("");
    document.getElementById("btnFilter").click();
}
$("#AddPreference").click(function () {
    $('#Preference').val("");
})