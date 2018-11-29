

function AddCategorySuccess(data) {
    debugger

    if (data.status == true) {
        $("#addCategory").modal("hide");
        $(".modal-backdrop").remove();

        var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
        BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_SUCCESS, message: 'Category added successfully.' });


       /// ShowMessage(BootstrapDialog.TYPE_SUCCESS, "Category added successfully");
        RefreshGrid();
        $('#loadercontainingdiv').addClass("hidden");
    }
    else {
        $("#addCategory").modal("hide");
        $(".modal-backdrop").remove();
        $('#loadercontainingdiv').addClass("hidden");
        ///ShowMessage(BootstrapDialog.TYPE_DANGER, "Error occured");

        var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
        BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_DANGER, message: 'Error occured.' });
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
                $("#addCategory").modal("hide");
                $(".modal-backdrop").remove();
            }
        }]
    });
}

function RefreshGrid() {
    $("#IndustryName", $('#AddCategory')).val("");
    document.getElementById("btnFilter").click();
}

$('#btnaddcat').click(function () {
    debugger;
    $("#IndustryName", $('#AddCategory')).val("")
    $("#IndustryShortName", $('#AddCategory')).val("")
})


