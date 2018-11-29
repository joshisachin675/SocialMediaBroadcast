
function EditCategorySuccess(data) {
    debugger
   
    if (data.result == true) {
        $("#editCategory").modal("hide");

        var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
        BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_SUCCESS, message: 'Industry updated successfully.' });
       // ShowMessage(BootstrapDialog.TYPE_SUCCESS, "Industry updated successfully");
        RefreshGrid();
        $('#loadercontainingdiv').addClass("hidden");
    }
    else {
        $("#editCategory").modal("hide");
        $('#loadercontainingdiv').addClass("hidden");

        var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
        BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_DANGER, message: 'Error occured.' });

    //    ShowMessage(BootstrapDialog.TYPE_DANGER, "Error occured");
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
                $("#editCategory").modal("hide");
            }
        }]
    });
}

function RefreshGrid() {
    $("#IndustryName", $('#editCatgeory')).val("");
    document.getElementById("btnFilter").click();
}