
function EditSubIndustrySuccess(data) {
    debugger;

    if (data.result == true) {
        $("#editSubIndustry").modal("hide");

        var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
        BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_SUCCESS, message: 'SubIndustry updated successfully.' });

       // ShowMessage(BootstrapDialog.TYPE_SUCCESS, "SubIndustry updated successfully");
        RefreshGrid();
        $('#loadercontainingdiv').addClass("hidden");
    }
    else {
        $("#editSubIndustry").modal("hide");
        $('#loadercontainingdiv').addClass("hidden");

        var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
        BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_SUCCESS, message: 'Error occured.' });

       // ShowMessage(BootstrapDialog.TYPE_DANGER, "Error occured");
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
                $("#editSubIndustry").modal("hide");
            }
        }]
    });
}

function RefreshGrid() {
    $("#SubIndustryName", $('#editSubIndustry')).val("");
    document.getElementById("btnFilter").click();
}