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

            var testRegex = /^https?:\/\/(?:[a-z\-]+\.)+[a-z]{2,6}(?:\/[^\/#?]+)+\.(?:jpe?g|gif|png)$/;
           
            /// CHange image Link text 
            $("#txtpostImageLink").keyup(function () {
                debugger
                if (testRegex.test($("#txtpostImageLink").val())) {   
                    $("#txtpostImageLink").css("border", "1px solid #cccccc ")                   
                if ($("#txtpostImageLink").val()!="") {
                    $("#dvImageUpload").hide();
                }
                else {
                    $("#dvImageUpload").show();
                }
                }
                else {
                    $("#txtpostImageLink").css("border", "1px solid red ")
                    $("#dvImageUpload").show();
                }
            });
            /// CHange image Link text 




            var list = new Array();

           //// Code for facebook page :SRohit
            /// For sync new Facepage
            var UserID = parseInt($("#hdnUserId").val());
            //$.ajax({
            //    url: $_SyncFacebookPage,
            //    type: 'post',
            //    data: { userId: UserID },
            //    success: function (data) {
            //        debugger
            //        $.each(data, function (k, v) {
            //            var category = v.category;

            //            $.each(v.dataList, function (k, v) {
            //                var htm = '';
            //                htm += ' <optgroup label="' + category + '">        <option value="' + v.PageId + '">' + v.PageName + '</option>        </optgroup>'
            //                $('#FBpageDetail').append(htm);
            //                $('#FBpageDetail').multiselect('rebuild');
            //            });



            //            //htm += '<option value = "' + v.PageId + '" >' + v.PageName + '</option>';



            //        });
            //    },
            //    error: function () {

            //    }

            //})
            ///// For sync new Facepage  
            ////Bind fb page dropdown 
            //var reqUrl = $_BaseUrl + '/Users/api/HomeApi/GetFacebookPageDetail'; 
            //var userIds = parseInt($('#hdnUserId').val())
            //var userId = {}
            //userId.userID = userIds;
            //$.ajax({
            //    type: "POST",
            //    url: reqUrl,
            //    data: userId,
            //    //  contentType: "application/json; charset=utf-8",
            //    // dataType: "json",
            //    success: function (data) {
            //        debugger
            //        $.each(data, function (k, v) {
            //            var category = v.category;
            //            var htm = '';
            //            var option = '';


            //            $.each(v.dataList, function (k, v) {

            //                option += ' <option value="' + v.PageId + '">' + v.PageName + '</option>'

            //            });

            //            htm += ' <optgroup label="' + category + '"> ' + option + '  </optgroup>';
            //            $('#FBpageDetail').append(htm);
            //            $('#FBpageDetail').multiselect('rebuild');
            //            //htm += '<option value = "' + v.PageId + '" >' + v.PageName + '</option>';
            //        });
            //    },
            //    failure: function () {
            //        alert("Failed!");
            //    }
            //});

            //Bind fb page dropdown 
            //// Code for page
            //// Code for facebook page :SRohit
            $('#btnPost').click(function () {
                list = [];
                debugger


                //// code for adding item to array to post on selected social media 

                var imgs = $('#wetrrmrk').find('img').attr('src');

                if (imgs ==undefined) {

                }
                var userId = $("#hdnUserId").val();
                var imgName = $('.dz-image-preview').children().eq(1).children().eq(1).text();
                if ($('#dvImageUpload').css('display') == 'none') {
                    imgName = $("#txtpostImageLink").val();
                }
                $.ajax({
                    url: $_WatermarkImg,
                    type: "Post",
                    data: { "img": imgs, "userId": userId, "imgName": imgName },
                    success: function (result) {
                    },
                });


                $(".added").each(function () {
                    // Removing items from array
                    if ($(this).hasClass('selected')) {
                        var id = $(this).attr('data-id');
                        $(this).addClass('selected');
                        list.push(id);
                        arrayIds = list;
                    }
                    else { // Adding items to array
                     //   var id = $(this).attr('data-id');
                    //    $(this).addClass('selected');
                     //   list.push(id);
                     //   arrayIds = list;
                        // alert(arrayIds);
                    }
                })
                //// code for adding item to array to post on selected social media 



                var FbpageList = $('#FBpageDetail option:selected');
                var selectedPageList = [];
                $(FbpageList).each(function (index, FbpageList) {
                    selectedPageList.push([$(this).val()]);
                });

                var message1 = 'You can,t post more than 140 character in Twitter !';
                var message2 = 'You can,t post more than 600 character in LinkedIn !';
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
                    $('#txtMsgContent').css("border", "1px solid red").attr("title", 'Please enter content')
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
                else {
                    var test = $('#txtpostLink').val();

                    if (test != "") {
                        function isUrlValid(url) {
                            return /^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(url);
                        }

                        var testCases = [
                          test
                        ], div = document.getElementById("output");

                        for (var i = 0; i < testCases.length; i++) {
                            var test = testCases[i];

                          //  div.innerHTML += (isUrlValid(test) ? "<span></span>   " : "<span class='invalid'>invalid URL</span> ") + "\n";
                            if (!isUrlValid(test)) {
                                $('#txtpostLink').css("border", "1px solid red").attr("title", 'Please enter Link')
                                isValid = false;
                            }
                        }


                    }
                    else {
                        div = document.getElementById("output");
                        div.innerHTML = ("");
                    }



                }

                if (atleastoneischecked == true && $('#txtMsgContent').val().length > 0 && isValid==true) {
                    Dashboard.BindDataMethods.CreateJsonForPostDetails();
                }
                onsubmit: (function () {
                    debugger
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


            //For manage Facebook Page/
            /// Sync facebook page detail on click on refresh button  : SRohit
            //$("#SyncFBpage").click(function () {

            //    var UserID = parseInt($("#hdnUserId").val());
            //    $.ajax({
            //        url: $_SyncFacebookPage,
            //        type: 'post',
            //        data: { userId: UserID },
            //        success: function (data) {
            //            debugger
            //            $.each(data, function (k, v) {
            //                var htm = '';
            //                htm += '<option value = "' + v.PageId + '" >' + v.PageName + '</option>';
            //                $('#FBpageDetail').append(htm);
            //                $('#FBpageDetail').multiselect('rebuild');

            //            });
            //        },
            //        error:function()
            //        {

            //        }

            //    })
            //});

            /// Sync facebook page detail on click on refresh button 

            ////Enable and disable facebook select page list ::-- Not done By Client /
            //$("#imagfb").click(function () {
            //    if ($(this).hasClass('selected')) {
            //        $('.multiselect ').removeAttr('disabled'); 
            //    }
            //    else {
            //        $('.multiselect ').attr('disabled', 'disable');
            //        $('#FBpageDetail').multiselect('clearSelection');
            //    }



            //});
            ////Enable and disable facebook select page list ::-- Not done By Client /



            //For manage Facebook Page/
            // Water Mark
            var resp = null;
            var userId = $("#hdnUserId").val();
            $.ajax({
                url: $_getwaterMarkImageDetails,
                type: "Get",
                data: { "userId": userId },
                success: function (result) {
                    debugger;
                    resp = result;
                },
            });
            $("#removeWatermark").click(function () {

                $('#watermark').show();
                $('#wetrrmrk').find('img').remove();
                $('#removeWatermark').hide();
            })
            $("#watermark").click(function () {
                debugger;
                var userId = $("#hdnUserId").val();
                var imdf = $_BaseUrl + '/Images/WallImages/' + userId + '/' + $(".dz-image").find('img').attr('alt');
                var fn = 50;
                if (resp != null) {
                    debugger;
                    if (resp.ImagePathLogo != "" && resp.ImagePathLogo != null) {
                        var logo = $_BaseUrl + '/Images/WallImages/' + userId + '/LogoImages/' + resp.ImagePathLogo;
                        if (resp.Gravity == "ne") {
                            watermark([imdf, logo])
                                                   .image(watermark.image.upperRight(resp.Opacity))
                                                   .then(function (img) {
                                                       debugger;
                                                       document.getElementById('wetrrmrk').appendChild(img);
                                                       $(img).attr('height', '200px');
                                                   })
                        }
                        else if (resp.Gravity == "nw") {
                            watermark([imdf, logo])
                                                   .image(watermark.image.upperLeft(resp.Opacity))
                                                   .then(function (img) {
                                                       document.getElementById('wetrrmrk').appendChild(img);
                                                       $(img).attr('height', '200px');
                                                   })
                        }
                        else if (resp.Gravity == "sw") {
                            watermark([imdf, logo])
                                                   .image(watermark.image.lowerLeft(resp.Opacity))
                                                   .then(function (img) {
                                                       document.getElementById('wetrrmrk').appendChild(img);
                                                       $(img).attr('height', '200px');
                                                   })
                        }
                        else if (resp.Gravity == "se") {
                            watermark([imdf, logo])
                                                   .image(watermark.image.lowerRight(resp.Opacity))
                                                   .then(function (img) {
                                                       document.getElementById('wetrrmrk').appendChild(img);
                                                       $(img).attr('height', '200px');
                                                   })
                        }
                    }
                    else if (resp.ImageText != "" && resp.ImageText != null) {
                        debugger;
                        if (resp.Gravity == "ne") {
                            watermark([imdf])
                         .image(watermark.text.upperRight(resp.ImageText, resp.TextSiz + 'px ' + resp.Fontfamily, resp.Textcolor, resp.Opacity, fn))
                        .then(function (img) {
                            document.getElementById('wetrrmrk').appendChild(img);
                            $(img).attr('height', '200px');
                        });
                        }
                        else if (resp.Gravity == "nw") {
                            watermark([imdf])
                        .image(watermark.text.upperLeft(resp.ImageText, resp.TextSiz + 'px ' + resp.Fontfamily, resp.Textcolor, resp.Opacity, fn))
                       .then(function (img) {
                           debugger;
                           document.getElementById('wetrrmrk').appendChild(img);
                           $(img).attr('height', '200px');
                       });
                        }
                        else if (resp.Gravity == "sw") {
                            watermark([imdf])
                       .image(watermark.text.lowerLeft(resp.ImageText, resp.TextSiz + 'px ' + resp.Fontfamily, resp.Textcolor, resp.Opacity))
                      .then(function (img) {
                          debugger;
                          document.getElementById('wetrrmrk').appendChild(img);
                          $(img).attr('height', '200px');
                      });
                        }
                        else if (resp.Gravity == "se") {
                            watermark([imdf])
                     .image(watermark.text.lowerRight(resp.ImageText, resp.TextSiz + 'px ' + resp.Fontfamily, resp.Textcolor, resp.Opacity))
                    .then(function (img) {
                        document.getElementById('wetrrmrk').appendChild(img);
                        $(img).attr('height', '200px');
                    });
                        }
                    }
                }
                else {
                    //watermark([imdf])
                    // .image(watermark.text.upperRight('SMB', '48px Josefin Slab', '#fff', 0.5))
                    //  .then(function (img) {
                    //      document.getElementById('wetrrmrk').appendChild(img);
                    //      $(img).attr('height', '200px');
                    //  });
                }
                $('#watermark').hide();
                $('#removeWatermark').show();
                $('#editwatermark').show();
            })
        },
        OnChange: function () {
            $("textarea").blur(function (e) {
                debugger
                var $this = $(this);
                if (!$this.val() == "") {
                    $($this).css("border", "1px solid #cccccc ")
                }
                else {
                    $($this).css("border", "1px solid red ")
                }
            })
            $("input").blur(function (e) {
                debugger
                var $this = $(this);
                if (!$this.val()=="") {
                    $($this).css("border", "1px solid #cccccc ")

                }
                else {
                    $($this).css("border", "1px solid red ")
                }
            })

            $('#txtpostLink').click(function () {
                div = document.getElementById("output");
                div.innerHTML = ("");
            });
            $('#txtpostLink').blur(function () {
                debugger
                var test = $('#txtpostLink').val();

                if (test !="") {
                    function isUrlValid(url) {
                        return /^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(url);
                    }

                    var testCases = [
                      test
                    ], div = document.getElementById("output");

                    for (var i = 0; i < testCases.length; i++) {
                        var test = testCases[i];
                        div.innerHTML += (isUrlValid(test) ? "<span></span>   " : "<span class='invalid'>invalid URL</span> ") + "" + "" + "\n";

                        if (!isUrlValid(test)) {
                            $("#txtpostLink").css("border", "1px solid red ")

                        }
                        else {
                            $('#txtpostLink').css("border", "1px solid #cccccc ")
                        }
                    }


                }
                else {
                    div = document.getElementById("output");
                    div.innerHTML = ("");
                }
              
            



            });

        


            $('.schedule').change(function () {
                //var date = new Date();
                //var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                var date = new Date();
                date.setDate(date.getDate());
                if ($(this).val() == 2) {
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
                    // FillSceduledPost();
                    debugger;
                    $('.loadercontainingdiv').hide();
                    $('#txtMsgContent').val("");
                    $("#txtpostheading").val("");
                    $("#txtpostTitle").val("");
                    $("#txtpostLink").val("");
                    $('.datepicker').val("");
                    $('#removeImage').click();
                    //$('#FBpageDetail').multiselect('clearSelection');
                    $("#watermark").hide();
                    $('#editwatermark').hide();
                    $('#wetrrmrk').hide();
                    $('#removeWatermark').click();
                    $('#removeWatermark').hide();
                    $('#txtpostImageLink').val("");
                    $(".drpPostType").val(1)
                    // arrayIds = [];
                    //$('.added').each(function () {
                    //    if ($(this).hasClass('selected')) {
                    //        $(this).removeClass('selected');
                    //    }
                    //});
                    if (result != null) {
                        $('#success-message').html("");
                        //$('#btn-show-modal').click();


                        if (result.ErrorModel.indexOf("Error")>0) {
                            $.confirm({
                                title: 'Error!',
                                content: result.ErrorModel,
                                type: 'red',
                                typeAnimated: true,
                                buttons: {
                                    close: function () {
                                    }
                                }
                            });
                        }
                        else {
                            $.confirm({
                                title: 'Success!',
                                content: result.ErrorModel,
                                type: 'blue',
                                typeAnimated: true,
                                buttons: {
                                    close: function () {
                                    }
                                }
                            });


                        }


                    
                        //$('#success-message').html(result.ErrorModel);
                        $("#watermark").hide();
                        $('#editwatermark').hide();
                        $('#wetrrmrk').hide();
                         $('#removeWatermark').hide();
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
                    $('#removeWatermark').click();

                    $('#removeWatermark').hide();
                }
            });
        },
    },
    BindDataMethods: {
        CreateJsonForPostDetails: function () {
            debugger;
            var arr = [];
            var FbpageList = $('#FBpageDetail option:selected');
            var selectedPageList = [];
            $(FbpageList).each(function (index, FbpageList) {
                selectedPageList.push([$(this).val()]);
            });
            // var idArray = [];
            //  idArray.push(11);
            var JsonWorkInfo = {};
            // Get Images
            if ($('#dvImageUpload').css('display') != 'none') {
                $('.dz-image-preview').each(function () {
                    var text = $(this).children().eq(1).children().eq(1).text()
                    arr.push(text);
                });
            }
            else {
                arr.push( $("#txtpostImageLink").val());  
            }
            JsonWorkInfo.selectedPageList = selectedPageList;
            JsonWorkInfo.TextMessage = $('#txtMsgContent').val();
            JsonWorkInfo.Ids = arrayIds;
            JsonWorkInfo.ImageArray = arr;
            JsonWorkInfo.timeoffset = new Date().getTimezoneOffset();
            JsonWorkInfo.Title = $('#txtpostTitle').val();
            JsonWorkInfo.Heading = $("#txtpostheading").val();
            JsonWorkInfo.Link = $("#txtpostLink").val();
            JsonWorkInfo.PostMethod = "PostNow";
            // Get selected dropdown value
            var postType = $('.schedule option:selected').val();
            var MultipostType = $('.drpPostType option:selected').val();
            JsonWorkInfo.PostType = postType;
            JsonWorkInfo.MultipostType = MultipostType;
            // Get DateTime
            if (postType == 2) {
                var dateTime = $('.datepicker').val();
                JsonWorkInfo.ScheduledTime = dateTime;
            }
         Dashboard.AjaxCalls.PostData(JsonWorkInfo);





        },
    },
}