
$(function () {
    Dashboard.Init();

});

var Dashboard = {
    Init: function () {
        Dashboard.PreInit();
        var arrayIds = [];
        // Dashboard.PageLoad.GridLoad();
    },
    PreInit: function () {
        Dashboard.Events.ClickEvents();
        Dashboard.Events.OnChange();
    },
    PageLoad: {

    },
    Events: {
        ClickEvents: function () {
            //$('#imagfb').click(function () {
            //    var accountId = 1;
            //    var JsonInfo = {};
            //    if ($(this).hasClass("added")) {
            //        $('.fbImg').remove();
            //        $(this).removeClass("added");
            //        JsonInfo.Account = "Facebook";
            //        JsonInfo.Status = 0;
            //    }
            //    else {
            //        var url = $('#imagfb img').attr('src');
            //        $('#socialmediapics').append('<img class="fbImg" src=' + url + ' height="25" width="25" />');
            //        $(this).addClass("added");
            //        JsonInfo.Account = "Facebook";
            //        JsonInfo.Status = 1;
            //    }
            //    // debugger;


            //    $.ajax({
            //        url: $_UpdateSocialMediaStatus,
            //        type: 'post',
            //        data: { "AccountInformation": JSON.stringify(JsonInfo) },
            //        type: "POST",
            //        success: function (result) {
            //            if (result != null) {
            //                var json = $.parseJSON(result);

            //            }

            //        },
            //        error: function () {

            //        }

            //    });

            //    //alert(url);
            //});

            //$('#imagld').click(function () {
            //    // debugger;
            //    var accountId = 1;
            //    var JsonInfo = {};
            //    if ($(this).hasClass("added")) {

            //        // var url = $('#imagfb img').attr('src');
            //        $('.ldImg').remove();
            //        $(this).removeClass("added");
            //        JsonInfo.Account = "LinkedIn";
            //        JsonInfo.Status = 0;
            //    }
            //    else {
            //        var url = $('#imagld img').attr('src');
            //        $('#socialmediapics').append('<img class="ldImg" src=' + url + ' height="25" width="25" />');
            //        $(this).addClass("added");
            //        JsonInfo.Account = "LinkedIn";
            //        JsonInfo.Status = 1;
            //    }

            //    $.ajax({
            //        url: $_UpdateSocialMediaStatus,
            //        type: 'post',
            //        data: { "AccountInformation": JSON.stringify(JsonInfo) },
            //        type: "POST",
            //        success: function (result) {
            //            if (result != null) {
            //                var json = $.parseJSON(result);

            //            }

            //        },
            //        error: function () {

            //        }

            //    });
            //    //alert(url);
            //});

            //$('#imagtw').click(function () {
            //    // debugger;
            //    var JsonInfo = {};
            //    if ($(this).hasClass("added")) {
            //        // var url = $('#imagfb img').attr('src');
            //        $('.twImg').remove();
            //        $(this).removeClass("added");
            //        JsonInfo.Account = "Twitter";
            //        JsonInfo.Status = 0;
            //    }
            //    else {
            //        var url = $('#imagtw img').attr('src');
            //        $('#socialmediapics').append('<img class="twImg" src=' + url + ' height="25" width="25" />');
            //        $(this).addClass("added");
            //        JsonInfo.Account = "Twitter";
            //        JsonInfo.Status = 1;
            //    }

            //    $.ajax({
            //        url: $_UpdateSocialMediaStatus,
            //        type: 'post',
            //        data: { "AccountInformation": JSON.stringify(JsonInfo) },
            //        type: "POST",
            //        success: function (result) {
            //            if (result != null) {
            //                var json = $.parseJSON(result);

            //            }

            //        },
            //        error: function () {

            //        }

            //    });
            //    //alert(url);
            //});

            //$('#imaggp').click(function () {
            //    // debugger;
            //    var JsonInfo = {};
            //    if ($(this).hasClass("added")) {
            //        // var url = $('#imagfb img').attr('src');
            //        $('.gpImg').remove();
            //        $(this).removeClass("added");
            //        JsonInfo.Account = "GooglePlus";
            //        JsonInfo.Status = 0;
            //    }
            //    else {
            //        var url = $('#imaggp img').attr('src');
            //        $('#socialmediapics').append('<img class="gpImg" src=' + url + ' height="25" width="25" />');
            //        $(this).addClass("added");
            //        JsonInfo.Account = "GooglePlus";
            //        JsonInfo.Status = 1;
            //    }

            //    $.ajax({
            //        url: $_UpdateSocialMediaStatus,
            //        type: 'post',
            //        data: { "AccountInformation": JSON.stringify(JsonInfo) },
            //        type: "POST",
            //        success: function (result) {
            //            if (result != null) {
            //                var json = $.parseJSON(result);

            //            }

            //        },
            //        error: function () {

            //        }

            //    });
            //    //alert(url);
            //});

            // Click on Post button
            var list = new Array();

            $('#btnPost').click(function () {
                debugger;              
               list = [];
               var message1 = 'You can,t post more than 140 character in Twitter  !';
               var message2 = 'You can,t post more than 600 character in LinkedIn   !';
               var both = message1.concat(message2);
               var isValid = true;
               var atleastoneischecked = false;
               $('.added').each(function () {
                   if ($(this).hasClass('selected')) {
                       atleastoneischecked = true;
                   }
               });
               if (atleastoneischecked == false) {
                   BootstrapDialog.alert('Please select atleast one account for posting !');
                   //    alert("Please select atleast one account for posting");
                   // $('#btn-show-modal').click();
                   //  $('#success-message').html("Please select atleast one account for posting");
               }

               if ($('#txtMsgContent').val().trim().length > 140 && $("#imagtw").hasClass("selected") && $('#txtMsgContent').val().trim().length > 600 && $("#imagld").hasClass("selected")) {
                   atleastoneischecked = false;                 
                   $("#imagld").removeClass("selected");                             
                   $("#imagtw").removeClass("selected");
                   BootstrapDialog.alert(both);
               }
          
               if ($('#txtMsgContent').val().trim().length > 600 && $("#imagld").hasClass("selected")) {
                   atleastoneischecked = false;
                   $("#imagld").removeClass("selected");
                   var message2 = 'You can,t post more than 600 character in LinkedIn   !';
                   BootstrapDialog.alert(message2);
                   if ($('#txtMsgContent').val().trim().length <= 140 && $("#imagtw").hasClass("selected")) {
                       atleastoneischecked = true;
                   }
               }

               if ($('#txtMsgContent').val().trim().length > 140 && $("#imagtw").hasClass("selected")) {
                   atleastoneischecked = false;
                   var message1 = 'You can,t post more than 140 character in Twitter  !';
                   $("#imagtw").removeClass("selected");
                   BootstrapDialog.alert(message1);
                   if ($('#txtMsgContent').val().trim().length <= 600 && $("#imagld").hasClass("selected")) {
                       atleastoneischecked = true;
                   }
               }

               if ($('#txtMsgContent').val().trim().length == 0) {
                   isValid = false;
                   $('#txtMsgContent').css("border","1px solid red").attr("title",'Please enter content')
                }

                if ($('#txtpostheading').val().length == 0) {
                    isValid = false;
                    $('#txtpostheading').css("border", "1px solid red").attr("title", 'Please enter Heading')
                    
                }
                if ($('#txtpostTitle').val().length == 0) {
                    isValid = false;
                    $('#txtpostTitle').css("border", "1px solid red").attr("title", 'Please enter title')
                }
                if ($('#txtpostLink').val().length == 0) {
                    isValid = false;
                    $('#txtpostLink').css("border", "1px solid red").attr("title", 'Please enter Link')
                }
 
                if (atleastoneischecked == true && isValid ==true) {
                    Dashboard.BindDataMethods.CreateJsonForPostDetails();
                }               
                onsubmit: (function () {
                    debugger;
                    countChar();
                });
            });

            $('.added').click(function () {
                debugger;
                var imgs = $('#wetrrmrk').find('img').attr('src');
                var userId = $("#hdnUserId").val();
                var imgName = $('.dz-image-preview').children().eq(1).children().eq(1).text();
                $.ajax({
                    url: $_WatermarkImg,
                    type: "Post",
                    data: { "img": imgs, "userId": userId, "imgName": imgName },
                    success: function (result) {
                    },
                });
                // Removing items from array
                if ($(this).hasClass('selected')) {
                    var id = $(this).attr('data-id');
                    $(this).removeClass('selected');

                    var i = 0;
                    for (i; i < list.length; i++) {
                        if (list[i] == id) {
                            list.splice(i, 1);
                        }
                    }
                    arrayIds = list;
                }
                else { // Adding items to array
                    var id = $(this).attr('data-id');
                    $(this).addClass('selected');
                    list.push(id);
                    arrayIds = list;
                    // alert(arrayIds);
                }
            });
        },
        keyDown: function () {

        },
        keypress: function () {

        },
        OnChange: function () {
            $('.schedule').change(function () {
                //var date = new Date();
                //var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                var date = new Date();
                date.setDate(date.getDate());
                if ($(this).val() == 2) {
                    debugger
               
                    $("#btnPost").text("Schedule Post");
                    $('.well').show();
                    $('.datepicker').datetimepicker(
                        {
                            minDate: date
                        });
                }
                else {
                    $("#btnPost").text("Post Now");
                    $('.well').hide();
                    // $('.datepicker').hide();
                }
                if ($(this).val() == 3) {
                    $("#btnPost").text("Publishing Post");

                }
             
              
               

            });
        },
       

    },
    AjaxCalls: {
        PostData: function (PostInformation) {
           debugger;
            $('.loadercontainingdiv').show();
            $.ajax({
                url: $_PostDetails,
                type: "POST",
                data: { "PostInformation": JSON.stringify(PostInformation) },
                success: function (result) {
                    FillSceduledPost();
                   // debugger;
                    $('.loadercontainingdiv').hide();
                    $('#txtMsgContent').val("");
                    $('.datepicker').val("");
                    $('#removeImage ').click();
               
                   // arrayIds = [];
                    $('.added').each(function () {
                        if ($(this).hasClass('selected')) {
                            $(this).removeClass('selected');
                        }
                    });
                    if (result != null) {
                        $('#success-message').html("");
                        //debugger;
                        //var json = $.parseJSON(result);
                        //if (result.ErrorModel.Status == true) {
                        //    // alert("Content has been successfully posted.");
                        //    $('#btn-show-modal').click();
                        //    $('#success-message').html("Content has been successfully posted");
                        //}
                        //else {
                        //    //alert("Error occured while posting.");
                        //    $('#btn-show-modal').click();
                        //    $('#success-message').html("Error occured while posting.");
                        //}
                        $('#btn-show-modal').click();
                        $('#success-message').html(result.ErrorModel);
                        $("#watermark").hide();
                        $('#editwatermark').hide();
                        $('#wetrrmrk').hide();                        
                    }
                    else {
                    }
                },
                error: function (result) {
                   // debugger;
                    $('.loadercontainingdiv').hide();
                    //alert("Error occured while posting.");
                    $('#btn-show-modal').click();
                    $('#success-message').html(result.ErrorModel);
                    $("#watermark").hide();
                    $('#editwatermark').hide();
                    $('#wetrrmrk').hide();
                }
            });
        },
    },
    BindDataMethods: {
        CreateJsonForPostDetails: function () {
           debugger;
            var arr = [];
            // var idArray = [];
            //  idArray.push(11);
            var JsonWorkInfo = {};
            // Get Images
            $('.dz-image-preview').each(function () {
                var text = $(this).children().eq(1).children().eq(1).text()
                arr.push(text);
            });
            JsonWorkInfo.TextMessage = $('#txtMsgContent').val();
            JsonWorkInfo.Ids = arrayIds;
            JsonWorkInfo.ImageArray = arr;
            JsonWorkInfo.timeoffset = new Date().getTimezoneOffset();
            JsonWorkInfo.Title = $('#txtpostTitle').val();
            JsonWorkInfo.Heading = $("#txtpostheading").val();
            JsonWorkInfo.Link = $("#txtpostLink").val();
            // Get selected dropdown value
            var postType = $('.schedule option:selected').val();
            JsonWorkInfo.PostType = postType;
            // Get DateTime
            if (postType == 2) {
                var dateTime = $('.datepicker').val();
                JsonWorkInfo.ScheduledTime = dateTime;
            }
            Dashboard.AjaxCalls.PostData(JsonWorkInfo);
        },
    },
    ApplyValidations: {

    },
    BindMethods: {

    },
}


function FillSceduledPost() {
    $.ajax({
        url: $_GetScheduledPost,
        type: "GET",
        success: function (result) {
            $("#scheduledPostSection").html(result);
        }
    });

};