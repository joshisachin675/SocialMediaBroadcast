$(function () {
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
            debugger;
            $('#btnEditContent').click(function () {
                debugger;
                var atleastoneischecked = false;
                var editcontent = $('#txtEditMsgContent').val().trim();

                $('.EditSocialMedia').each(function () {
                    if ($(this).is(':checked')) {
                        atleastoneischecked = true;
                    }
                });

                if (atleastoneischecked == false) {
                    BootstrapDialog.show({
                        message: 'Please select atleast one account for posting.',
                        buttons: [{
                            label: 'Ok',
                            cssClass: 'btn-primary',
                            action: function (dialogItself) {
                                dialogItself.close();
                            }
                        }]
                    });
                }

                if (editcontent.length == 0) {
                    BootstrapDialog.show({
                        message: 'Please write content to post.',
                        buttons: [{
                            label: 'Ok',
                            cssClass: 'btn-primary',
                            action: function (dialogItself) {
                                dialogItself.close();
                            }
                        }]
                    });
                }

                if (atleastoneischecked == true && editcontent.length > 0) {
                    Dashboard.BindDataMethods.CreateJsonForPostDetails();
                }

            });


        },
        keyDown: function () {

        },
        keypress: function () {

        },
    },
    AjaxCalls: {
        PostData: function (PostInformation) {
            $('.loadercontainingdiv').show();
            debugger;
            $.ajax({
                url: $_UpdateContentDetails,
                type: "POST",
                data: { "PostInformation": JSON.stringify(PostInformation) },
                success: function (data) {
                    // debugger;
                    if (data.status == true) {
                        $('.loadercontainingdiv').hide();
                        ShowMessage(BootstrapDialog.TYPE_SUCCESS, 'Content updated successfully.');
                        RefreshGrid();
                        $("#editContent").modal("hide");
                        $(".modal-backdrop").remove();
                    }
                    else {
                        $('.loadercontainingdiv').hide();
                        if (data.message == "Error") { ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.'); }
                        RefreshGrid();
                        $("#editContent").modal("hide");
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
            var content = $('#txtEditMsgContent').val().trim();
            var arr = [];
            var arrayIds = [];
            var JsonWorkInfo = {};
            // Get Images
            $('.dz-image-preview').each(function () {
                var text = $(this).children().eq(1).children().eq(1).text()
                arr.push(text);
            });

            $('.EditSocialMedia').each(function () {
                if ($(this).is(':checked')) {
                    var id = $(this).attr('data-id');
                    arrayIds.push(id);
                }
            });

            JsonWorkInfo.TextMessage = content;
            JsonWorkInfo.Ids = arrayIds;
            JsonWorkInfo.ImageArray = arr;
            JsonWorkInfo.Url = $('#EditUrl').val();
            JsonWorkInfo.ContentId = $('#contentId').val();
            Dashboard.AjaxCalls.PostData(JsonWorkInfo);
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
                $("#editContent").modal('hide');
            }
        }]
    });
}

function RefreshGrid() {
    $('#txtEditMsgContent').val(" ");
    $('#removeImage').click();
    $('.EditSocialMedia').each(function () {
        if ($(this).is(':checked')) {
            $(this).attr('checked', false);
        }
    });
    document.getElementById("btnFilter").click();
}
