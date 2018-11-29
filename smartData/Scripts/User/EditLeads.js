$(document).ready(function () {
    $("#EmailAddress").on('blur', function () {
        debugger;
        var status = ValidateEmail("#EmailAddress", "Please enter valid email id.");

    });


   


});


function ValidateFormLeads() {
    debugger;
    var isValid = true;
    var isEmailValid = true;
    var isPasswordValid = true;
    var isShortName = true;
    $("#ConfirmPassword").css("border")
    var regExp = /^(?=.*\d)(?=.*[A-Z]).{8,12}$/;
    if (!ValidateEmail("#EmailAddress", "Please enter valid email id.")) {
        return false;
    }
    if (CheckRequired("#FirstName", "First Name is required") != true)
        isValid = false;
    if (CheckRequired("#LastName", "Last Name is required") != true)
        isValid = false;

    if (CheckRequired("#Address", " Address is required") != true)
        isValid = false;


    

    if (CheckRequired("#StreetAddress", "Street Address is required") != true)
        isValid = false;


    if (CheckRequired("#City", "City is required") != true)
        isValid = false;

    if (CheckRequired("#EmailAddress", "Email is required") != true) {
        isValid = false;
        isEmailValid = false;
    }

    if (CheckRequired("#Shortname", "Short Name is required") != true) {
        isValid = false;
        isShortName = false;
    }


    if (isEmailValid) {
        debugger;
        if (ValidateEmail("#EmailAddress", "Email id is not valid") != true)
            isValid = false;
    }

   

    if (isValid == true)
        return true;
    else
        return false;
}


function AfterUpdate(result) {
    debugger;
    if (result.Response == "success") {
        var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
        BootstrapDialog.show({ message: 'The record has been updated successfully.' });
        var $table = $("#tblmanageLeads"); $table.bootstrapTable('refresh');
        $("#mdlEditLeads").modal("hide");
        // BootstrapDialog.alert('The record has been updated successfully.');
    }
    else if (result.Response == "newPassword") {
        // BootstrapDialog.alert('The record has been updated successfully.');
        var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
        BootstrapDialog.show({ message: 'The record has been updated successfully.' });
        var $table = $("#tblmanageLeads"); $table.bootstrapTable('refresh');
        $("#mdlEditLeads").modal("hide");
    }
    else {

        ShowMessage(BootstrapDialog.TYPE_DANGER, "Some error occerred.");

    }
}
function ShowMessage(type, message) {
    $messageData = $("<span>Error</span>");
    var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
    BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
    BootstrapDialog.show({
        title: $messageData,
        type: type,
        message: message,
    });

}
function ValidateEmail(sEmail) {
 
    var reg = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    if (!reg.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}