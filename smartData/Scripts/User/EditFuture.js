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
            debugger;
            var date = new Date();
            date.setDate(date.getDate());
            $('.datepicker').datetimepicker(
                      {
                          minDate: date
                      });
            $('#btnContent').click(function () {
                debugger;
                var arrayKeywords = [];
                // var atleastoneischecked = false;
                //  var content = tinyMCE.get('txtMsgContent').getContent();
                var Description = $('textarea#txtMsgContent').val().trim();
                var isValid = true;
              
                if ($('#Social_Media').val() == 0) {
                    isValid = false;
                    $('#Social_Media').addClass("errorClass").attr("title", "Please Select SocialMedia")
                }
                else {
                    isValid = true;
                    $('#Social_Media').removeClass("errorClass").removeAttr("title");
                }
                if (Description.length == 0) {
                    isValid = false;
                    $('textarea').css("border", 'red 1px solid').attr("title", "Please Enter Text")
                }
                else {
                    isValid = true;
                    $('textarea').removeClass("errorClass").removeAttr("title");
                }
                //var keywords = $('#txtKeywords').val().trim();

                //$('.socialMedia').each(function () {
                //    if ($(this).is(':checked')) {
                //        atleastoneischecked = true;
                //    }
                //});

                //$('.label-info').each(function () {
                //    var text = $(this).text();
                //    arrayKeywords.push(text);
                //});

                if (Description.length == 0 && !isValid) {
                    BootstrapDialog.show({
                        message: 'Please write Description to post</br> Please enter tags',
                        buttons: [{
                            label: 'Ok',
                            cssClass: 'btn-primary',
                            action: function (dialogItself) {
                                dialogItself.close();
                            }
                        }]
                    });
                }
                else if (Description.length == 0 && !isValid) {
                    BootstrapDialog.show({
                        message: 'Please write Description to post',
                        buttons: [{
                            label: 'Ok',
                            cssClass: 'btn-primary',
                            action: function (dialogItself) {
                                dialogItself.close();
                            }
                        }]
                    });
                }
                //else if (content.length > 0 && arrayKeywords.length == 0) {
                //    BootstrapDialog.show({
                //        message: 'Please enter tags',
                //        buttons: [{
                //            label: 'Ok',
                //            cssClass: 'btn-primary',
                //            action: function (dialogItself) {
                //                dialogItself.close();
                //            }
                //        }]
                //    });
                //}

                if (Description.length > 0 && isValid == true) {
                    Dashboard.BindDataMethods.CreateJsonForPostDetails();
                }

            });

            $('#btnEditContent').click(function () {
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
        PostData: function (PostInformation) {
           
            $('.loadercontainingdiv').show();
            debugger;
            $.ajax({
                url: $_PostContentDetails,
                type: "POST",
                data: { "PostInformation": JSON.stringify(PostInformation) },
                success: function (data) {
                    // debugger;
                    if (data.status == true) {
                        $('.loadercontainingdiv').hide();
                        ShowMessage(BootstrapDialog.TYPE_SUCCESS, 'Post is added successfully.');
                        RefreshGrid();
                        $("#addContent").modal("hide");
                        $(".modal-backdrop").remove();
                    }
                    else {
                        $('.loadercontainingdiv').hide();
                        if (data.message == "Error") { ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.'); }
                        RefreshGrid();
                        $("#addContent").modal("hide");
                        $(".modal-backdrop").remove();
                    }
                },
                error: function (result) {
                    debugger;
                    $('.loadercontainingdiv').hide();
                    ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured while posting content.');
                }
            });
        },

        PostEditData: function (PostInformation) {
            $('.loadercontainingdiv').show();
            debugger;
            $.ajax({
                url: $_UpdateContentPost,
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
  
          
           
            var Description = $('#txtEditMsgContent').val();
            var Url = $('#EditUrl').val();
            var Name = $('#txtEditKeywords').val();
            var arr = [];
            var arrayIds = [];
            var ImageArray = arr;
            var JsonWorkInfo = {};
            // Get Images
            $('.dz-image-preview ').each(function () {
                var text = $(this).children().eq(1).children().eq(1).text()
                arr.push(text);
            });

            // Get social ids
            //$('.socialMedia').each(function () {
            //    if ($(this).is(':checked')) {
            //        var id = $(this).attr('data-id');
            //        arrayIds.push(id);
            //    }
            //});

            // Get keywords
            $('.label-info').each(function () {
                var text = $(this).text();
                arrayTags.push(text);
            });

            JsonWorkInfo.TextMessage = Description;
            // JsonWorkInfo.Ids = arrayIds;
            JsonWorkInfo.ImageArray = arr;
            JsonWorkInfo.Url = $('#Url').val();
            JsonWorkInfo.TagArray = arrayTags;
        
            JsonWorkInfo.PostId = $('#PostId').val();
            Dashboard.AjaxCalls.PostData(JsonWorkInfo);
        },

        CreateJsonForEditPostDetails: function () {
            debugger;
         
            // var content = tinyMCE.get('txtMsgContent').getContent();
            var Description = $('#txtEditMsgContent').val().trim();
            var Url = $('#EditUrl').val().trim();
           // var Name = $('#txtEditKeywords').val().trim();
            var PostId = $('#PostId').val();
          //  var PostDate = $('.PostDate').val();
            //var PostDate = $("#PostDate").val();
            //var category = $("#categoryList option:selected").text();
            //var category = $('#categoryList').val();
            //var socialMedia = $('#Social_Media').val();
            var arr = [];
            var ImageArray = arr;
            var arrayIds = [];
            var JsonWorkInfo = {};
            // Get Images
            $('.dz-image-preview').each(function () {
                var text = $(this).children().eq(1).children().eq(1).text()
                arr.push(text);
            });

            $('.label-info').each(function () {
                var text = $(this).text();
                editArrayTags.push(text);
            });

            //$('.EditSocialMedia').each(function () {
            //    if ($(this).is(':checked')) {
            //        var id = $(this).attr('data-id');
            //        arrayIds.push(id);
            //    }
            //});

            JsonWorkInfo.TextMessage = Description;
            // JsonWorkInfo.Ids = arrayIds;
            JsonWorkInfo.Array
            JsonWorkInfo.ImageArray = arr;
          
            JsonWorkInfo.Url = $('#EditUrl').val();
            JsonWorkInfo.Description = $('#txtEditMsgContent').val();
           
            JsonWorkInfo.Name = $('#txtEditKeywords').val();
            JsonWorkInfo.PostId = $('#PostId').val();
            var postType = '2';
            JsonWorkInfo.PostType = postType;
            // Get DateTime
            if (postType == 2) {
                var dateTime = $('.datepicker').val();
                JsonWorkInfo.PostDate = dateTime;
            }
            Dashboard.AjaxCalls.PostEditData(JsonWorkInfo);
        }
    },
    ApplyValidations: {


    },
    BindMethods: {

    },
}


//function AddContentSuccess(data) {

//    if (data.status == true) {
//        $("#addContent").modal("hide");
//        $('#loadingmessage').addClass("hidden");
//        ShowMessage(BootstrapDialog.TYPE_SUCCESS, 'Content is added successfully.');
//        RefreshGrid();
//    }
//    else {
//        $("#addContent").modal("hide");
//        $('#loadingmessage').addClass("hidden");
//        if (data.message == "Error") { ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.'); }
//        RefreshGrid();
//    }

//}


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
    //$("#socialMedia").val("");
    //$("#LastName").val("");
    //$("#UserName").val("");
    //$("#Password").val("");
    //$("#ConfirmPassword").val("");
    $('#txtMsgContent').val(" ");
    $('#removeImage').click();
    // arrayIds = [];
    $('.socialMedia').each(function () {
        if ($(this).is(':checked')) {
            $(this).attr('checked', false);
        }
    });

    //$('#txtEditMsgContent').val(" ");
    //$('#removeImage').click();
    //$('.EditSocialMedia').each(function () {
    //    if ($(this).is(':checked')) {
    //        $(this).attr('checked', false);
    //    }
    //});

    document.getElementById("btnFilter").click();
}
