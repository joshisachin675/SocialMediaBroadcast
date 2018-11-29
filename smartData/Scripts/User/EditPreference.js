$(document).ready(function () {
    Dashboard.Init();

});


var Dashboard = {
    Init: function () {
        Dashboard.PreInit();

    },
    PreInit: function () {
        Dashboard.Events.ClickEvents();

    },
    PageLoad: {

    },
    Events: {
        ClickEvents: function () {
        
          
            $('#btnEditPreference').click(function () {
                debugger;
                var arrayKeywords = [];
                // var atleastoneischecked = false;
                var editcontent = $('textarea#txtEditMsgContent').val().trim();




                //$('.label-info').each(function () {
                //    var text = $(this).text();
                //    arrayKeywords.push(text);
                //});

                if (editcontent.length == 0) {
                    BootstrapDialog.show({
                        message: 'Please write content to post</br> Please enter tags',
                        buttons: [{
                            label: 'Ok',
                            cssClass: 'btn-primary',
                            action: function (dialogItself) {
                                dialogItself.close();
                            }
                        }]
                    });
                }

                if (editcontent.length > 0) {
                    Dashboard.BindDataMethods.CreateJsonForEditPostDetails();
                }

            });

        },
        keyDown: function () {

        },
        keypress: function () {

        },
    },
    AjaxCalls: {
     

        PostEditData: function (PostInformation) {
            $('.loadercontainingdiv').show();
            debugger;
            $.ajax({
                url: $_UpdatePreference,
                type: "POST",
                data: { "PostInformation": JSON.stringify(PostInformation) },
                success: function (data) {
                    // debugger;
                    if (data.status == true) {
                        $('.loadercontainingdiv').hide();
                        ShowMessage(BootstrapDialog.TYPE_SUCCESS, 'Preference updated successfully.');
                        RefreshGrid();
                        $("#EditPreference").modal("hide");
                        $(".modal-backdrop").remove();
                    }
                    else {
                        $('.loadercontainingdiv').hide();
                        if (data.message == "Error") { ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.'); }
                        RefreshGrid();
                        $("#EditPreference").modal("hide");
                        $(".modal-backdrop").remove();
                    }
                },
                error: function (result) {
                    debugger;
                    $('.loadercontainingdiv').hide();
                    ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured while updating content.');
                }
            });
        },
    },
    BindDataMethods: {
        CreateJsonForPostDetails: function () {
            debugger;
            // var content = tinyMCE.get('txtMsgContent').getContent();

            var JsonWorkInfo = {};

            var Preference = $('#txtEditMsgContent').val();
      

            JsonWorkInfo.Preference = Preference;
            JsonWorkInfo.PreferenceId = $('#PreferenceId').val();
            Dashboard.AjaxCalls.PostData(JsonWorkInfo);
        },

        CreateJsonForEditPostDetails: function () {
            debugger;
            var JsonWorkInfo = {};
            // var content = tinyMCE.get('txtMsgContent').getContent();
           var Preference = $('#txtEditMsgContent').val().trim();
           var PreferenceId = $('#PreferenceId').val();
    
            JsonWorkInfo.Preference = Preference;
      
            JsonWorkInfo.Description = $('#Preference').val();
            JsonWorkInfo.PreferenceId = $('#PreferenceId').val();
           
            Dashboard.AjaxCalls.PostEditData(JsonWorkInfo);
        }
    },
    ApplyValidations: {


    },
    BindMethods: {

    },
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
                $("#addContent").modal('hide');
                $("#editContent").modal('hide');
            }
        }]
    });
}




function RefreshGrid() {

    $('#txtMsgContent').val(" ");
    $('#removeImage').click();
    // arrayIds = [];
    $('.socialMedia').each(function () {
        if ($(this).is(':checked')) {
            $(this).attr('checked', false);
        }
    });


    document.getElementById("btnFilter").click();
}
