$(document).ready(function () {
   
    $("#ImageDiv").hide();
   
});


   
         
            $("txtURL").click(function () {
                div = document.getElementById("output");
                div.innerHTML = ("");

            })


            $('#txtURL').blur(function () {

                var test = $('#txtURL').val();

                if (test != "") {
                    function isUrlValid(url) {
                        return /^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(url);
                    }

                    var testCases = [
                      test
                    ], div = document.getElementById("output");

                    for (var i = 0; i < testCases.length; i++) {
                        var test = testCases[i];

                        if ($(".invalid").html() != "invalid URL") {
                            div.innerHTML += (isUrlValid(test) ? "<span></span>   " : "<span class='invalid' style='color: red;'>invalid URL</span> ") + "" + "" + "\n";
                        }



                        if (!isUrlValid(test)) {
                            $("#txtURL").css("border", "1px solid red ")

                        }
                        else {
                            $('#txtURL').css("border", "1px solid #cccccc ")
                            div = document.getElementById("output");
                            div.innerHTML = ("");

                        }
                    }


                }
                else {
                    div = document.getElementById("output");
                    div.innerHTML = ("");
                }





            });


            /// to fatch data from URL 
            $("#fatchData").click(function () {
                if ($('#txtURL').val().length == 0) {
                    $("#txtURL").css("border", "1px solid red ")
                }

                $('.loadercontainingdiv').show();
                var test = $('#txtURL').val();

                if (test != "") {
                    function isUrlValid(url) {
                        return /^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(url);
                    }

                    var testCases = [
                      test
                    ], div = document.getElementById("output");

                    for (var i = 0; i < testCases.length; i++) {
                        var test = testCases[i];

                        if ($(".invalid").html() != "invalid URL") {
                            div.innerHTML += (isUrlValid(test) ? "<span></span>   " : "<span class='invalid' style='color: red;'>invalid URL</span> ") + "" + "" + "\n";
                        }



                        if (!isUrlValid(test)) {
                            $("#txtURL").css("border", "1px solid red ")

                        }
                        else {
                            $('#txtURL').css("border", "1px solid #cccccc ")
                            div = document.getElementById("output");
                            div.innerHTML = ("");

                            $('.loadercontainingdiv').show();
                            $.ajax({
                                type: 'POST',
                                url: $_FatchDatafromURL,
                                data: { data: $("#txtURL").val() },
                                success: function (data) {


                                    if (data.length == 0) {
                                        ShowMessage(BootstrapDialog.TYPE_SUCCESS, 'No data found.');
                                    }


                                    ///facebook Detail 
                                    var facebook = {}
                                    $.each(data, function (index, value) {
                                        if (value != undefined) {
                                            if (value.indexOf('og:title') > -1) {
                                                var t1 = value.split('"');
                                                facebook.title = t1[3];
                                            }
                                            if (facebook.title == null || facebook.title == "" || facebook.title == undefined) {
                                                if (value.indexOf('<title>') > -1) {
                                                    var t1 = value.split('title>');
                                                    t1 = t1[1].split('<')
                                                    facebook.title = t1[0];
                                                }
                                            }


                                            if (value.indexOf('og:site_name') > -1) {
                                                var t2 = value.split('"');
                                                facebook.site_name = t2[3];
                                            }

                                            if (value.indexOf('og:url') > -1) {
                                                var t3 = value.split('"');
                                                facebook.url = t3[3];
                                            }

                                            if (value.indexOf('og:image" content=') > -1) {
                                                var t4 = value.split('"');
                                                facebook.image = t4[3];
                                            }

                                            if (value.indexOf('og:tags') > -1) {
                                                var t4 = value.split('"');
                                                facebook.tags = t4[3];
                                            }

                                            if (value.indexOf('og:language') > -1) {
                                                var t4 = value.split('"');
                                                facebook.language = t4[3];
                                            }

                                            if (value.indexOf('og:type') > -1) {
                                                var t4 = value.split('"');
                                                facebook.type = t4[3];
                                            }


                                            if (value.indexOf('og:description') > -1) {
                                                var t4 = value.split('"');
                                                facebook.description = t4[3];
                                            }

                                            if (facebook.description == null || facebook.description == "" || facebook.description == undefined) {
                                                if (value.indexOf('name="description"') > -1) {
                                                    var t4 = value.split('"');
                                                    facebook.description = t4[3];

                                                }
                                            }






                                        }
                                    });
                                    ///facebook Detail 

                                 

                                 


                                    debugger
                                   
                                    bindFacebooktab(facebook);
                                   // $('.loadercontainingdiv').hide();



                                },
                                error: function (data) {
                                    debugger
                                    $('.loadercontainingdiv').hide();
                                }

                            })


                        }
                    }


                }
                else {
                    div = document.getElementById("output");
                    div.innerHTML = ("");
                }





             //   $('.loadercontainingdiv').hide();


            })

            // fatch data from URL


      
   
     
            function bindFacebooktab (facebookData) {
          //  $('.loadercontainingdiv').hide();
            debugger

            //// BInd data for Image txt 
           
            if (facebookData.image != null || facebookData.image != "") {
                $("#txtImageCDN").val(facebookData.image);
                var url = facebookData.image;
                $('.UploadedImage img').remove();
                $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="fromFetchURL" src="' + url + '"/></div></div>');
            }

            //// BInd data for Image txt 



            $('.loadercontainingdiv').hide();
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
            
            }
        }]
    });
}



