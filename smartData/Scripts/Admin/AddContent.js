var SocialMedianame = "";
var ContentSourceName = "";
$(document).ready(function () {

    $("#tabs").tabs();

    $("#industryfacebook").on("change", function (event) {
        debugger;
        //alert($("option:selected", $("#industry")).val());
        var id = $("option:selected", $("#industryfacebook")).val();
        var state = $("#statefacebook");
        $.ajax({
            url: $_GetStateByCountry,
            type: 'Get',
            data: { IndustryId: id },
            async: false,
            contentType: "application/json; charset=utf-8",
            //data: { IndustryId: $("option:selected", $("#IndustryId")).val() },
            success: function (data) {
                debugger
                if (data != null) {
                    var $html = '<option selected="selected">Select SubIndustry</option>';
                    $(data).each(function (index, item) {
                        $html += '<option value="' + item.id + '" ' + (item.Selected ? 'selected="selected"' : '') + '>' + item.name + '</option>';
                    });
                    $("#statefacebook").html($html);//refresh the plugin to show new values in dropdown                   
                }
            },
        });
    });

    $("#industrylinkedIn").on("change", function (event) {
        debugger;
        //alert($("option:selected", $("#industry")).val());
        var id = $("option:selected", $("#industrylinkedIn")).val();
        var state = $("#statelinkedIn");
        $.ajax({
            url: $_GetStateByCountry,
            type: 'Get',
            data: { IndustryId: id },
            async: false,
            contentType: "application/json; charset=utf-8",
            //data: { IndustryId: $("option:selected", $("#IndustryId")).val() },
            success: function (data) {
                debugger
                if (data != null) {
                    var $html = '<option selected="selected">Select SubIndustry</option>';
                    $(data).each(function (index, item) {
                        $html += '<option value="' + item.id + '" ' + (item.Selected ? 'selected="selected"' : '') + '>' + item.name + '</option>';
                    });
                    $("#statelinkedIn").html($html);//refresh the plugin to show new values in dropdown                   
                }
            },
        });
    });

    $("#industrytwitter").on("change", function (event) {
        debugger;
        //alert($("option:selected", $("#industry")).val());
        var id = $("option:selected", $("#industrytwitter")).val();
        var state = $("#statetwitter");
        $.ajax({
            url: $_GetStateByCountry,
            type: 'Get',
            data: { IndustryId: id },
            async: false,
            contentType: "application/json; charset=utf-8",
            //data: { IndustryId: $("option:selected", $("#IndustryId")).val() },
            success: function (data) {
                debugger
                if (data != null) {
                    var $html = '<option selected="selected">Select SubIndustry</option>';
                    $(data).each(function (index, item) {
                        $html += '<option value="' + item.id + '" ' + (item.Selected ? 'selected="selected"' : '') + '>' + item.name + '</option>';
                    });
                    $("#statetwitter").html($html);//refresh the plugin to show new values in dropdown                   
                }
            },
        });
    });

    Dashboard.Init();
    var char = 0;

    var tab = $("#tabs .ui-tabs-active").text();
    if (tab == "FaceBook") {
        debugger;
        var charfb = 63206;
        $('#charNumfacebook').text("Characters remaining " + charfb);
    }

});





var Dashboard = {
    Init: function () {
        Dashboard.PreInit();

    },
    PreInit: function () {
        Dashboard.Events.ClickEvents();
        Dashboard.Events.OnChange();
    },
    PageLoad: {

    },
    Events: {
        ClickEvents: function () {
            debugger;
            $('#btnContent').click(function () {
                debugger;
                var arrayKeywords = [];
                var content = "";
                var tab = $("#tabs .ui-tabs-active").text();
                if (tab == "FaceBook") {
                    var content = $('textarea#txtMsgContentfacebook').val().trim();
                    var isValid = true;
                    //if ($('#SocialmediaFacebook', $('#addContent')).val() == 0) {
                    //    isValid = false;
                    //    $('#SocialmediaFacebook', $('#addContent')).addClass("errorClass").attr("title", "Please Select SocialMedia")
                    //}
                    //else {
                    //    isValid = true;
                    //    $('#SocialmediaFacebook', $('#addContent')).removeClass("errorClass").removeAttr("title");
                    //}
                    if (content.length == 0) {
                        isValid = false;
                        $('textarea#txtMsgContentfacebook', $('#addContent')).css("border", 'red 1px solid').attr("title", "Please Enter Text")
                    }
                    //else {
                    //    isValid = true;
                    //    $('textarea#txtMsgContentfacebook', $('#addContent')).removeClass("errorClass").removeAttr("title");
                    //}

                    if ($('#industryfacebook option:selected').text().trim() == "Select Industry") {
                        isValid = false;
                        $('#industryfacebook', $('#addContent')).addClass("errorClass").attr("title", "Please Select Industry")
                    }
                    else {
                        $('#industryfacebook', $('#addContent')).removeClass("errorClass").attr("title", "Please Select Industry")
                    }
                    if ($('#statefacebook  option:selected').text().trim() == "" || $('#statefacebook  option:selected').text().trim() == "Select SubIndustry") {
                        isValid = false;
                        $('#statefacebook ', $('#addContent')).addClass("errorClass").attr()
                    }
                    //else {
                    //    $('#statefacebook ', $('#addContent')).removeClass("errorClass").attr()
                    //}

                    if ($("#txtheadingfb").val() == "") {
                        isValid = false;
                        $("#txtheadingfb").css("border", 'red 1px solid').attr("title", "Please Enter Heading");
                    }

                    if ($("#txttitlefb").val() == "") {
                        isValid = false;
                        $("#txttitlefb").css("border", 'red 1px solid').attr("title", "Please Enter Title");
                    }

                    if ($("#UrlfaceBook").val() == "") {
                        isValid = false;
                        $("#UrlfaceBook").css("border", 'red 1px solid').attr("title", "Please Enter Title");
                    }
                    //else {
                    //    isValid = true;
                    //    $('#industryfacebook', $('#addContent')).removeClass("errorClass").removeAttr("tittle");
                    //}

                }
                else if (tab == "Twitter") {
                    var content = $('textarea#txtMsgContenttwitter').val().trim();
                    var isValid = true;
                    //if ($('#SocialmediaTwitter', $('#addContent')).val() == 0) {
                    //    isValid = false;
                    //    $('#SocialmediaTwitter', $('#addContent')).addClass("errorClass").attr("title", "Please Select SocialMedia")
                    //}
                    //else {
                    //    isValid = true;
                    //    $('#SocialmediaTwitter', $('#addContent')).removeClass("errorClass").removeAttr("title");
                    //}
                    if (content.length == 0) {
                        isValid = false;
                        $('textarea#txtMsgContenttwitter', $('#addContent')).css("border", 'red 1px solid').attr("title", "Please Enter Text")
                    }
                    if ($('#industryfacebook option:selected').text().trim() == "Select Industry") {
                        isValid = false;
                        $('#industryfacebook', $('#addContent')).addClass("errorClass").attr("title", "Please Select Industry")
                    }
                    if ($('#statefacebook  option:selected').text().trim() == "" || $('#statefacebook  option:selected').text().trim() == "Select SubIndustry") {
                        isValid = false;
                        $('#statefacebook ', $('#addContent')).addClass("errorClass").attr("title", "Please Select Sub Industry")
                    }
                    //else {
                    //    isValid = true;
                    //    $('textarea#txtMsgContenttwitter', $('#addContent')).removeClass("errorClass").removeAttr("title");
                    //} 
                    if ($('#industrytwitter option:selected').text().trim() == "Select Industry") {
                        isValid = false;
                        $('#industrytwitter', $('#addContent')).addClass("errorClass").attr("title", "Please Select Industry")
                    }
                    //else {
                    //    isValid = true;
                    //    $('#industrytwitter', $('#addContent')).removeClass("errorClass").removeAttr("tittle");
                    //}
                    if ($("#txtheadingtwittr").val() == "") {
                        isValid = false;
                        $("#txtheadingtwittr").css("border", 'red 1px solid').attr("title", "Please Enter Heading");
                    }

                    if ($("#txttitletwittr").val() == "") {
                        isValid = false;
                        $("#txttitletwittr").css("border", 'red 1px solid').attr("title", "Please Enter Title");
                    }

                    if ($("#Urltwitter").val() == "") {
                        isValid = false;
                        $("#Urltwitter").css("border", 'red 1px solid').attr("title", "Please Enter Title");
                    }

                }
                else if (tab == "LinkedIn") {
                    var content = $('textarea#txtMsgContentLinkedIn').val().trim();
                    var isValid = true;
                    //if ($('#SocialmediaLinkedIN', $('#addContent')).val() == 0) {
                    //    isValid = false;
                    //    $('#SocialmediaLinkedIN', $('#addContent')).addClass("errorClass").attr("title", "Please Select SocialMedia")
                    //}
                    //else {
                    //    isValid = true;
                    //    $('#SocialmediaLinkedIN', $('#addContent')).removeClass("errorClass").removeAttr("title");
                    //}
                    if (content.length == 0) {
                        isValid = false;
                        $('textarea#txtMsgContentLinkedIn', $('#addContent')).css("border", 'red 1px solid').attr("title", "Please Enter Text")
                    }
                    if ($('#industryfacebook option:selected').text().trim() == "Select Industry") {
                        isValid = false;
                        $('#industryfacebook', $('#addContent')).addClass("errorClass").attr("title", "Please Select Industry")
                    }
                    if ($('#statefacebook  option:selected').text().trim() == "" || $('#statefacebook  option:selected').text().trim() == "Select SubIndustry") {
                        isValid = false;
                        $('#statefacebook ', $('#addContent')).addClass("errorClass").attr("title", "Please Select Sub Industry")
                    }
                    //else {
                    //    isValid = true;
                    //    $('textarea#txtMsgContentLinkedIn', $('#addContent')).removeClass("errorClass").removeAttr("title");
                    //} 
                    if ($('#industrylinkedIn option:selected').text().trim() == "Select Industry") {
                        isValid = false;
                        $('#industrylinkedIn', $('#addContent')).addClass("errorClass").attr("title", "Please Select Industry")
                    }
                    //else {
                    //    isValid = true;
                    //    $('#industrylinkedIn', $('#addContent')).removeClass("errorClass").removeAttr("tittle");
                    //}

                    if ($("#txtheadingLnkin").val() == "") {
                        isValid = false;
                        $("#txtheadingLnkin").css("border", 'red 1px solid').attr("title", "Please Enter Heading");
                    }

                    if ($("#txttitleLnkin").val() == "") {
                        isValid = false;
                        $("#txttitleLnkin").css("border", 'red 1px solid').attr("title", "Please Enter Title");
                    }

                    if ($("#UrlLinkedIn").val() == "") {
                        isValid = false;
                        $("#UrlLinkedIn").css("border", 'red 1px solid').attr("title", "Please Enter Title");
                    }

                }


                //if ($('#state', $('#addContent')).val().trim() == 'Select SubIndustry') {
                //    isValid = false;
                //    $('#state', $('#addContent')).addClass("errorClass").attr("title", "Please Select subIndustry")
                //}
                //else {
                //    isValid = true;
                //    $('#state', $('#addContent')).removeClass("errorClass").removeAttr("title");
                //}
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

                //if (content.length == 0 && !isValid) {
                //    BootstrapDialog.show({
                //        message: 'Please write content to post</br> Please enter tags',
                //        buttons: [{
                //            label: 'Ok',
                //            cssClass: 'btn-primary',
                //            action: function (dialogItself) {
                //                dialogItself.close();
                //            }
                //        }]
                //    });
                //}
                //else if (content.length == 0 && !isValid) {
                //    BootstrapDialog.show({
                //        message: 'Please write content to post',
                //        buttons: [{
                //            label: 'Ok',
                //            cssClass: 'btn-primary',
                //            action: function (dialogItself) {
                //                dialogItself.close();
                //            }
                //        }]
                //    });
                //}

                if (content.length > 0 && isValid == true) {
                    Dashboard.BindDataMethods.CreateJsonForPostDetails();
                }

            });

            $('#btnEditContent').click(function () {
                debugger;
                var content = $('textarea#txtEditMsgContent', $('#editContent')).val().trim();
                var isValidIndustry = true;
                var isValidMedia = true;
                var isValid = true;
                if (content.length == 0) {
                    // isValid = false;
                    $('textarea', $('#editContent')).css("border", 'red 1px solid').attr("title", "Please Enter Text")
                }
                else {
                    //isValid = true;
                    $('textarea', $('#editContent')).removeClass("errorClass").removeAttr("title");
                }
                //if ($('#categoryList', $('#editContent')).text() == 'Select Industry') {
                //    isValid = false;

                //    $('#categoryList', $('#editContent')).addClass("errorClass").attr("title", "Please Select Category")
                //}
                //else {
                //    isValid = true;
                //    $('#categoryList', $('#editContent')).removeClass("errorClass").removeAttr("title");
                //}

                if ($('#industry option:selected').text().trim() == "Select Industry") {
                    isValidIndustry = false;
                    $('#industry', $('#editContent')).addClass("errorClass").attr("title", "Please Select Industry")
                }
                //else {
                //    isValidIndustry = true;
                //    $('#industry', $('#editContent')).removeClass("errorClass").removeAttr("tittle");
                //}
                if ($('#Social_Media', $('#editContent')).val() == 0) {
                    isValidMedia = false;
                    $('#Social_Media', $('#editContent')).addClass("errorClass").attr("title", "Please Select SocialMedia")
                }

                if ($("#txtheadingEdit").val() == "") {
                    isValid = false;
                    $('#txtheadingEdit', $('#editContent')).css("border", 'red 1px solid').attr("title", "Please Enter Heading")
                }
                if ($("#txttitleEdit").val() == "") {
                    isValid = false;
                    $('#txttitleEdit', $('#editContent')).css("border", 'red 1px solid').attr("title", "Please Enter Title")
                }
                if ($("#EditUrl").val() == "") {
                    isValid = false;
                    $('#EditUrl', $('#editContent')).css("border", 'red 1px solid').attr("title", "Please Enter link")
                }

                if (content.length > 0 && isValidIndustry == true && isValidMedia == true && isValid == true) {
                    Dashboard.BindDataMethods.CreateJsonForEditPostDetails();
                }

            });


            $('#btnEditRssContent').click(function () {
                debugger;

                debugger

                var tab = $("#tabsRss .ui-tabs-active").text();
                var industry = 0;
                if (tab == "FaceBook") {
                    var content = $('textarea#txtEditMsgContentfacebook').val().trim();
                    industry = $('#industryeditfacebook option:selected').val();
                    var isValid = true;
                    if (content.length == 0) {
                        isValid = false;
                        $('textarea#txtEditMsgContentfacebook', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter Text")
                    }
                    //else {
                    //    isValid = true;
                    //    $('textarea#txtEditMsgContentfacebook', $('#editContentRss')).removeClass("errorClass").removeAttr("title");
                    //}
                    if ($('#industryeditfacebook option:selected').text().trim() == "Select Industry") {
                        isValid = false;
                        $('#industryeditfacebook', $('#editContentRss')).addClass("errorClass").attr("title", "Please Select Industry")
                    }

                    if ($("#txtheadingfbEdit").val() == "") {
                        isValid = false;
                        $('#txtheadingfbEdit', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter Heading")
                    }
                    if ($("#txttitlefbEdit").val() == "") {
                        isValid = false;
                        $('#txttitlefbEdit', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter Title")
                    }
                    if ($("#EditUrlFacebook").val() == "") {
                        isValid = false;
                        $('#EditUrlFacebook', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter link")
                    }


                    //else {
                    //    isValid = true;
                    //    $('#industryeditfacebook', $('#editContentRss')).removeClass("errorClass").removeAttr("tittle");
                    //}

                }
                else if (tab == "Twitter") {
                    var content = $('textarea#txtEditMsgContenttwitter').val().trim();
                    industry = $('#industryeditTiwtter option:selected').val();
                    var isValid = true;

                    if (content.length == 0) {
                        isValid = false;
                        $('textarea#txtEditMsgContenttwitter', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter Text")
                    }
                    //else {
                    //    isValid = true;
                    //    $('textarea#txtEditMsgContenttwitter', $('#editContentRss')).removeClass("errorClass").removeAttr("title");
                    //}
                    if ($('#industryeditTiwtter option:selected').text().trim() == "Select Industry") {
                        isValid = false;
                        $('#industryeditTiwtter', $('#editContentRss')).addClass("errorClass").attr("title", "Please Select Industry")
                    }
                    //else {
                    //    isValid = true;
                    //    $('#industryeditTiwtter', $('#editContentRss')).removeClass("errorClass").removeAttr("tittle");
                    //}


                    if ($("#txtheadingtwEdit").val() == "") {
                        isValid = false;
                        $('#txtheadingtwEdit', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter Heading")
                    }
                    if ($("#txttitletwEdit").val() == "") {
                        isValid = false;
                        $('#txttitletwEdit', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter Title")
                    }
                    if ($("#EditUrlTwitter").val() == "") {
                        isValid = false;
                        $('#EditUrlTwitter', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter link")
                    }

                }
                else if (tab == "LinkedIn") {
                    var content = $('textarea#txtEditMsgContentLinkedIn').val().trim();
                    industry = $('#industryeditLinkedIn option:selected').val();
                    var isValid = true;

                    if (content.length == 0) {
                        isValid = false;
                        $('textarea#txtEditMsgContentLinkedIn', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter Text")
                    }
                    //else {
                    //    isValid = true;
                    //    $('textarea#txtEditMsgContentLinkedIn', $('#editContentRss')).removeClass("errorClass").removeAttr("title");
                    //}
                    if ($('#industryeditLinkedIn option:selected').text().trim() == "Select Industry") {
                        isValid = false;
                        $('#industryeditLinkedIn', $('#editContentRss')).addClass("errorClass").attr("title", "Please Select Industry")
                    }
                    //else {
                    //    isValid = true;
                    //    $('#industryeditLinkedIn', $('#editContentRss')).removeClass("errorClass").removeAttr("tittle");
                    //}

                    if ($("#txtheadingliEdit").val() == "") {
                        isValid = false;
                        $('#txtheadingliEdit', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter Heading")
                    }
                    if ($("#txttitleliEdit").val() == "") {
                        isValid = false;
                        $('#txttitleliEdit', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter Title")
                    }
                    if ($("#EditUrlLinkedIn").val() == "") {
                        isValid = false;
                        $('#EditUrlLinkedIn', $('#editContentRss')).css("border", 'red 1px solid').attr("title", "Please Enter link")
                    }

                }
                //if (content.length > 0 && isValid == true) {
                //    $.ajax({
                //        url: $_UrlCheckDuplicateContent,
                //        type: "POST",
                //        data: { description: content, socialMedia: tab, IndustryId: industry },
                //        success: function (result) {
                //            debugger;
                //            if (result == "exists") {
                //                BootstrapDialog.alert("Content already exists for this Social Media and Industry");
                //                //Dashboard.BindDataMethods.CreateJsonForEditRssPostDetails();
                //            }
                //            else {
                //                Dashboard.BindDataMethods.CreateJsonForEditRssPostDetails();
                //            }
                //        },
                //        error: function (result) {

                //        }
                //    });

                //}
                Dashboard.BindDataMethods.CreateJsonForEditRssPostDetails();


            })
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
                $("#txtfbImage").show();
                $("#txttwitterImage").show();
                $("#txtimagelink").show();
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
                        $('.loadercontainingdiv').hide();
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

                                    ///Twitter Detail 
                                    var Twitter = {}
                                    $.each(data, function (index, value) {
                                        if (value != undefined) {
                                            if (value.indexOf('twitter:card') > -1) {
                                                var t1 = value.split('"');
                                                Twitter.card = t1[3];
                                            }

                                            if (value.indexOf('twitter:text:title') > -1) {
                                                var t2 = value.split('"');
                                                Twitter.title = t2[3];
                                            }

                                            if (Twitter.title == null || Twitter.title == undefined) {
                                                if (value.indexOf('twitter:title') > -1) {
                                                    var t2 = value.split('"');
                                                    Twitter.title = t2[3];
                                                }
                                            }
                                            if (value.indexOf('twitter:title') > -1) {
                                                var t2 = value.split('"');
                                                Twitter.title = t2[3];
                                            }

                                            if (Twitter.title == null || Twitter.title == "" || Twitter.title == undefined) {
                                                if (value.indexOf('<title>') > -1) {
                                                    var t1 = value.split('title>');
                                                    t1 = t1[1].split('<')
                                                    Twitter.title = t1[0];
                                                }
                                            }

                                            if (value.indexOf('og:url') > -1) {
                                                var t3 = value.split('"');
                                                Twitter.url = t3[3];
                                            }

                                            if (value.indexOf('twitter:image') > -1) {
                                                var t4 = value.split('"');
                                                Twitter.image = t4[3];
                                            }

                                            if (value.indexOf('og:site_name') > -1) {
                                                var t4 = value.split('"');
                                                Twitter.site_name = t4[3];
                                            }

                                            if (value.indexOf('og:language') > -1) {
                                                var t4 = value.split('"');
                                                Twitter.language = t4[3];
                                            }

                                            if (value.indexOf('og:type') > -1) {
                                                var t4 = value.split('"');
                                                Twitter.type = t4[3];
                                            }


                                            if (value.indexOf('og:description') > -1) {
                                                var t4 = value.split('"');
                                                Twitter.description = t4[3];
                                            }
                                            if (Twitter.description == null || Twitter.description == "" || Twitter.description == undefined) {
                                                if (value.indexOf('name="description"') > -1) {
                                                    var t4 = value.split('"');
                                                    Twitter.description = t4[3];

                                                }
                                            }




                                        }
                                    });
                                    ///Twitter Detail 


                                    ///LinkedIn Detail 
                                    var LinkedIn = {}
                                    $.each(data, function (index, value) {
                                        if (value != undefined) {
                                            if (value.indexOf('og:title') > -1) {
                                                var t1 = value.split('"');
                                                LinkedIn.title = t1[3];
                                            }

                                            if (LinkedIn.title == null || LinkedIn.title == "" || LinkedIn.title == undefined) {
                                                if (value.indexOf('<title>') > -1) {
                                                    var t1 = value.split('title>');
                                                    t1 = t1[1].split('<')
                                                    LinkedIn.title = t1[0];
                                                }
                                            }
                                            if (value.indexOf('og:site_name') > -1) {
                                                var t2 = value.split('"');
                                                LinkedIn.site_name = t2[3];
                                            }

                                            if (value.indexOf('og:url') > -1) {
                                                var t3 = value.split('"');
                                                LinkedIn.url = t3[3];
                                            }

                                            if (value.indexOf('og:image" content=') > -1) {
                                                var t4 = value.split('"');
                                                LinkedIn.image = t4[3];
                                            }

                                            if (value.indexOf('og:tags') > -1) {
                                                var t4 = value.split('"');
                                                LinkedIn.tags = t4[3];
                                            }

                                            if (value.indexOf('og:language') > -1) {
                                                var t4 = value.split('"');
                                                LinkedIn.language = t4[3];
                                            }

                                            if (value.indexOf('og:type') > -1) {
                                                var t4 = value.split('"');
                                                LinkedIn.type = t4[3];
                                            }


                                            if (value.indexOf('og:description') > -1) {
                                                var t4 = value.split('"');
                                                LinkedIn.description = t4[3];
                                            }

                                            if (LinkedIn.description == null || LinkedIn.description == "" || LinkedIn.description == undefined) {
                                                if (value.indexOf('name="description"') > -1) {
                                                    var t4 = value.split('"');
                                                    LinkedIn.description = t4[3];

                                                }
                                            }


                                        }
                                    });
                                    ///LinkedIn Detail 


                                    debugger
                                    //       $('.loadercontainingdiv').hide();
                                    Dashboard.AjaxCalls.bindFacebooktab(facebook);
                                    Dashboard.AjaxCalls.bindTwittertab(Twitter);
                                    Dashboard.AjaxCalls.bindLinkedintab(LinkedIn);
                                    $('.loadercontainingdiv').hide();



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





                // $('.loadercontainingdiv').hide();


            })

            // fatch data from URL


        },
        keyDown: function () {

        },
        keypress: function () {

        },

        OnChange: function () {
            //$("#SocialmediaFacebook").on('change', function () {
            //    debugger;
            //    var selectedText = $(this).find("option:selected").text();
            //    if (selectedText == 'Facebook') {
            //        char = 63206;
            //        $('#charNumfacebook').text("Characters remaining " + char);
            //    }
            //    else if (selectedText == 'LinkedIn') {
            //        char = 2000;
            //        $('#charNumlinkedIn').text("Characters remaining " + char);
            //    }
            //    else if (selectedText == 'Twitter') {
            //        char = 140;
            //        $('#charNumtwitter').text("Characters remaining " + char);
            //    }
            //    else if (selectedText == 'Google+')
            //        char = 100000;            
            //    $('#txtMsgContent').val('');

            //});


            $("#tabs").tabs({
                activate: function (event, ui) {
                    debugger;
                    var left = 0;
                    var select = ui.newPanel.selector;
                    var charsleft = "";
                    if (select == "#twitter-tab") {
                        char = 140;
                        charsleft = $('#txtMsgContenttwitter', $('#addContent')).val().length;
                        if (charsleft > 0) {
                            $('#charNumtwitter').text("Characters remaining ").append(140 - charsleft);
                        }
                        else {
                            $('#charNumtwitter').text("Characters remaining " + char);
                        }

                        //charsleft = $('#txtMsgContenttwitter', $('#addContent')).val().length;
                        //left = 140 - charsleft;

                        //charEdit = left;
                    }
                    else if (select == "#facebook-tab") {
                        char = 63206;
                        charsleft = $('#txtMsgContentfacebook', $('#addContent')).val().length;
                        if (charsleft > 0) {
                            $('#charNumfacebook').text("Characters remaining ").append(63206 - charsleft);
                        }
                        else {
                            $('#charNumfacebook').text("Characters remaining " + char);
                        }

                        //left = 63206 - charsleft;

                        //charEdit = left;
                    }
                    else if (select == "#LinkedIn-tab") {
                        char = 2000;
                        charsleft = $('#txtMsgContentLinkedIn', $('#addContent')).val().length;
                        if (charsleft > 0) {
                            $('#charNumlinkedIn').text("Characters remaining ").append(2000 - charsleft);
                        }
                        else {
                            $('#charNumlinkedIn').text("Characters remaining " + char);
                        }

                        //left = 2000 - charsleft;

                        //charEdit = left;
                    }
                }
            });

            $("#SocialmediaTwitter").on('change', function () {
                debugger;
                var selectedText = $(this).find("option:selected").text();
                if (selectedText == 'Facebook') {
                    char = 63206;
                    $('#charNumfacebook').text("Characters remaining " + char);
                }
                else if (selectedText == 'LinkedIn') {
                    char = 2000;
                    $('#charNumlinkedIn').text("Characters remaining " + char);
                }
                else if (selectedText == 'Twitter') {
                    char = 140;
                    $('#charNumtwitter').text("Characters remaining " + char);
                }
                else if (selectedText == 'Google+')
                    char = 100000;
                //alert(char);
                //$('#charNumfacebook').text("Characters remaining " + char);
                $('#txtMsgContent').val('');
                //var selectedText = $(this).find("option:selected").text();
                //var selectedValue = $(this).val();
                //alert("Selected Text: " + selectedText + " Value: " + selectedValue);
            });

            $("#SocialmediaLinkedIN").on('change', function () {
                debugger;
                var selectedText = $(this).find("option:selected").text();
                if (selectedText == 'Facebook') {
                    char = 63206;
                    $('#charNumfacebook').text("Characters remaining " + char);
                }
                else if (selectedText == 'LinkedIn') {
                    char = 2000;
                    $('#charNumlinkedIn').text("Characters remaining " + char);
                }
                else if (selectedText == 'Twitter') {
                    char = 140;
                    $('#charNumtwitter').text("Characters remaining " + char);
                }
                else if (selectedText == 'Google+')
                    char = 100000;
                //alert(char);
                //$('#charNumfacebook').text("Characters remaining " + char);
                $('#txtMsgContent').val('');
                //var selectedText = $(this).find("option:selected").text();
                //var selectedValue = $(this).val();
                //alert("Selected Text: " + selectedText + " Value: " + selectedValue);
            });
        }

    },
    AjaxCalls: {
        PostData: function (PostInformation) {
            $('.loadercontainingdiv').show();
            SocialMedianame = PostInformation.SocialMedia;
            debugger;
            $.ajax({
                url: $_PostContentDetails,
                type: "POST",
                data: { "PostInformation": JSON.stringify(PostInformation) },
                success: function (data) {
                    debugger;
                    var GroupID = RandomGenerator();
                    $("#tab" + SocialMedianame + "").attr("data-groupid", GroupID)
                    if (data.status == true) {
                        $('.loadercontainingdiv').hide();
                        ShowMessage(BootstrapDialog.TYPE_SUCCESS, 'Content saved successfully.');
        
                        RefreshGrid();
                        // $("#addContent").modal("hide");
                        // $(".modal-backdrop").remove();
                    }
                    else {
                        $('.loadercontainingdiv').hide();
                        if (data.message == "Error") { ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.'); }
                        RefreshGrid();
                        // $("#addContent").modal("hide");
                        // $(".modal-backdrop").remove();
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
        bindFacebooktab: function (facebookData) {
            $('.loadercontainingdiv').hide();
            debugger


            //// BInd data for facebook 
            $("#txttitlefb").val(facebookData.title);
            $("#txtMsgContentfacebook").val(facebookData.description);
            $("#UrlfaceBook").val(facebookData.url);
            $("#txtheadingfb").val(facebookData.site_name);

            if (facebookData.image != null || facebookData.image != "") {
                $("#txtfbImage").val(facebookData.image);
                var url = facebookData.image;
                $('.UploadedImage img').remove();
                $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="fromFetchURL" src="' + url + '"/></div></div>');
                $(".jumbotron").hide()
            }

            else {
                $('.UploadedImage img').remove();
            }

            if (facebookData.url == undefined || facebookData.url == null) {
                $("#UrlfaceBook").val($("#txtURL").val());
            }
            //// BInd data for facebook  



            $('.loadercontainingdiv').hide();
        },
        bindTwittertab: function (Twitter) {
            $('.loadercontainingdiv').hide();
            debugger

            //// BInd data for Twitter 
            $("#txttitletwittr").val(Twitter.title);
            $("#txtMsgContenttwitter").val(Twitter.description);
            $("#Urltwitter").val(Twitter.url);
            $("#txtheadingtwittr").val(Twitter.site_name);
            //// BInd data for Twitter  

            if (Twitter.image != null || Twitter.image != "") {

                $("#txttwitterImage").val(Twitter.image);
                var url = Twitter.image;

                $('.UploadedImage img').remove();
                $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="fromFetchURL" src="' + url + '"/></div></div>');
                $(".jumbotron").hide()
            }
            else {
                $('.UploadedImage img').remove();
            }
            if (Twitter.url == undefined || Twitter.url == null) {
                $("#Urltwitter").val($("#txtURL").val());
            }

            $('.loadercontainingdiv').hide();
        },
        bindLinkedintab: function (LinkedIn) {
            $('.loadercontainingdiv').hide();
            debugger

            //// BInd data for LinkedIn 
            $("#txttitleLnkin").val(LinkedIn.title);
            $("#txtMsgContentLinkedIn").val(LinkedIn.description);
            $("#UrlLinkedIn").val(LinkedIn.url);
            $("#txtheadingLnkin").val(LinkedIn.site_name);
            //// BInd data for LinkedIn  

            if (LinkedIn.image != null || LinkedIn.image != "") {

                $("#txtimagelink").val(LinkedIn.image);
                var url = LinkedIn.image;

                $('.UploadedImage img').remove();
                $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="fromFetchURL" src="' + url + '"/></div></div>');
                $(".jumbotron").hide()
            }
            else {
                $('.UploadedImage img').remove();
            }
            if (LinkedIn.url == undefined || LinkedIn.url == null) {
                $("#UrlLinkedIn").val($("#txtURL").val());
            }


            $('.loadercontainingdiv').hide();
        }
    },
    BindDataMethods: {
        CreateJsonForPostDetails: function () {
            debugger;
            // var content = tinyMCE.get('txtMsgContent').getContent();
            var tab = $("#tabs .ui-tabs-active").text();
            if (tab == "FaceBook") {
                var content = $('#txtMsgContentfacebook').val();
                var category = $('#industryfacebook option:selected').val();
                var state = $("#statefacebook").val();
                var heading = $("#txtheadingfb").val();
                var title = $("#txttitlefb").val();
                if (state == "Select SubIndustry") {
                    state = 0;
                }
                var stateId = $("#statefacebook option:selected").text();
                var categoryName = $('#industryfacebook option:selected').text();
                var socialMedia = "Facebook";
                var arr = [];
                var arrayIds = [];
                var arrayTags = [];
                var JsonWorkInfo = {};
                // Get Images
                //$('.dz-image-preview ').each(function () {
                //    var text = $(this).children().eq(1).children().eq(1).text()
                //    arr.push(text);
                //});
                var text = $('#fbdiv').find('.dz-image').find('img').attr('alt');
                if (text == undefined) {
                    text = "";
                }
                var newtext = "/Images/WallImages/ContentImages/" + text;
                if (text == "fromFetchURL") {
                    ///newtext = $('#fbdiv').find('.dz-image').find('img').attr('src');
                    newtext = $('#txtfbImage').val();
                }
                if (newtext == null || newtext == "") {
                    //  newtext = $('#txtimagelink').val();
                    newtext = $('#fbdiv').find('.dz-image').find('img').attr('src');
                }
                if (text.indexOf("www") > -1 || text.indexOf("http") > -1) {
                    arr.push(text);
                }
                else {
                    arr.push(newtext);
                }

                var linkurl = $("#UrlfaceBook").val();

                // Get keywords
                $('.label-info').each(function () {
                    var text = $(this).text();
                    arrayTags.push(text);
                });
                var GroupId = $('#tabFacebook').data("groupid")

            }
            else if (tab == "Twitter") {
                var content = $('#txtMsgContenttwitter').val();
                //var category = $('#industrytwitter option:selected').val();
                //var state = $("#statetwitter").val();
                var category = $('#industryfacebook option:selected').val();
                var state = $("#statefacebook").val();
                if (state == "Select SubIndustry") {
                    state = 0;
                }
                var heading = $("#txtheadingtwittr").val();
                var title = $("#txttitletwittr").val();
                //var stateId = $("#statetwitter option:selected").text();
                var stateId = $("#statefacebook option:selected").text();
                var categoryName = $('#industryfacebook option:selected').text();
                //  var categoryName = $('#industrytwitter option:selected').text();
                var socialMedia = "Twitter";
                var arr = [];
                var arrayIds = [];
                var arrayTags = [];
                var JsonWorkInfo = {};
                // Get Images
                //$('.dz-image-preview ').each(function () {
                //    var text = $(this).children().eq(1).children().eq(1).text()
                //    arr.push(text);
                //});
                var text = $('#twitdiv').find('.dz-image').find('img').attr('alt');
                if (text == undefined) {
                    text = "";
                }
                var newtext = "/Images/WallImages/ContentImages/" + text;
                if (text == "fromFetchURL") {
                    newtext = $('#txttwitterImage').val();
                }
                if (newtext == null || newtext == "") {
                    newtext = $('#twitdiv').find('.dz-image').find('img').attr('src');
                }


                if (text.indexOf("www") > -1 || text.indexOf("http") > -1) {
                    arr.push(text);
                }
                else {
                    arr.push(newtext);
                }

                // Get keywords
                $('.label-info').each(function () {
                    var text = $(this).text();
                    arrayTags.push(text);
                });
                var linkurl = $("#Urltwitter").val();

                var GroupId = $('#tabTwitter').data("groupid")
            }
            else if (tab == "LinkedIn") {
                var content = $('#txtMsgContentLinkedIn').val();
                //var category = $('#industrylinkedIn option:selected').val();
                //var state = $("#statelinkedIn").val();

                var category = $('#industryfacebook option:selected').val();
                var state = $("#statefacebook").val();
                var heading = $("#txtheadingLnkin").val();
                var title = $("#txttitleLnkin").val();
                if (state == "Select SubIndustry") {
                    state = 0;
                }
                //var stateId = $("#statelinkedIn option:selected").text();
                //var categoryName = $('#industrylinkedIn option:selected').text();
                var stateId = $("#statefacebook option:selected").text();
                var categoryName = $('#industryfacebook option:selected').text();
                var socialMedia = "LinkedIn";
                var arr = [];
                var arrayIds = [];
                var arrayTags = [];
                var JsonWorkInfo = {};
                // Get Images
                //$('.dz-image-preview ').each(function () {
                //    var text = $(this).children().eq(1).children().eq(1).text()
                //    arr.push(text);
                //});﻿
                var text = $('#Linkdiv').find('.dz-image').find('img').attr('alt');
                if (text == undefined) {
                    text = "";
                }
                var newtext = "/Images/WallImages/ContentImages/" + text;

                if (text == "fromFetchURL") {
                    newtext = $('#txtimagelink').val();

                }
                if (newtext == null || newtext == "") {
                    newtext = $('#Linkdiv').find('.dz-image').find('img').attr('src');
                }

                if (text.indexOf("www") > -1 || text.indexOf("http") > -1) {
                    arr.push(text);
                }
                else {
                    arr.push(newtext);
                }

                // Get keywords
                $('.label-info').each(function () {
                    var text = $(this).text();
                    arrayTags.push(text);
                });
                var linkurl = $("#UrlLinkedIn").val();
                var GroupId = $('#tabLinkedIn').data("groupid")
            }

            JsonWorkInfo.TextMessage = content;
            // JsonWorkInfo.Ids = arrayIds;
            JsonWorkInfo.ImageArray = arr;
            JsonWorkInfo.ContentSource = "";
            JsonWorkInfo.Title = title;
            JsonWorkInfo.Heading = heading;
            JsonWorkInfo.Url = linkurl;
            JsonWorkInfo.TagArray = arrayTags;
            JsonWorkInfo.CategoryId = category;
            JsonWorkInfo.CategoryName = categoryName;
            JsonWorkInfo.SubIndustryId = state;
            JsonWorkInfo.SubIndustryName = stateId;
            JsonWorkInfo.SocialMedia = socialMedia;
            JsonWorkInfo.GroupId = GroupId;
            Dashboard.AjaxCalls.PostData(JsonWorkInfo);
        },

        CreateJsonForEditPostDetails: function () {
            debugger;
            // var editArrayTags = [];
            // var content = tinyMCE.get('txtMsgContent').getContent();
            var content = $('#txtEditMsgContent').val().trim();
            //var category = $('select#categoryList option:selected').val();
            //var socialMedia = $('select#Social_Media option:selected').text();
            $('#industry', $('#editContent')).each(function () {
                debugger;
                if ($(this).attr('selected', true)) {
                    category = $(this).val();
                    categoryName = $("#industry option:selected").text();
                }
            });
            $('#industry', $('#editContent')).each(function () {
                debugger;
                if ($(this).attr('selected', true)) {
                    SubIndustryId = $(this).val();
                    SubIndustryName = $("#subIndustry option:selected").text();
                }
            });

            $('#Social_Media', $('#editContent')).each(function () {
                debugger;
                if ($(this).attr('selected', true)) {
                    socialMedia = $(this).val();
                }
            });



            var arr = [];
            var arrayIds = [];
            var JsonWorkInfo = {};
            // Get Images
            //$('.dz-image-preview').each(function () {
            //    var text = $(this).children().eq(1).children().eq(1).text()
            //    arr.push(text);
            //});
            debugger
            //var text = $('.dz-image').find('img').attr('alt');
            //if (text == "fromFetchURL") {
            //    text = $('.dz-image').find('img').attr('src');
            //}
            //if (text == '') {
            //    text = $('.dz-image').find('img').attr('src');
            //}


            var text = $('.dz-image').find('img').attr('alt');
            var newtext = "/Images/WallImages/ContentImages/" + text;
            if (text == "fromFetchURL") {
                newtext = $('.dz-image').find('img').attr('src');
            }
            if (text == '') {
                newtext = $('.dz-image').find('img').attr('src');
            }




            arr.push(newtext);

            JsonWorkInfo.TextMessage = content;
            // JsonWorkInfo.Ids = arrayIds;
            JsonWorkInfo.Array
            JsonWorkInfo.ImageArray = arr;
            JsonWorkInfo.Url = $('#EditUrl').val();
            JsonWorkInfo.ContentId = $('#contentId').val();
            JsonWorkInfo.CategoryId = category;
            JsonWorkInfo.CategoryName = categoryName;
            JsonWorkInfo.Title = $("#txttitleEdit").val();
            JsonWorkInfo.Heading = $('#txtheadingEdit').val();
            JsonWorkInfo.SubIndustryId = SubIndustryId;
            if (SubIndustryName == 'Select SubIndustry') {
                JsonWorkInfo.SubIndustryName = "";
            }
            else {
                JsonWorkInfo.SubIndustryName = SubIndustryName;
            }
            JsonWorkInfo.ContentSource = "";
            JsonWorkInfo.SocialMedia = socialMedia;
            Dashboard.AjaxCalls.PostEditData(JsonWorkInfo);
        },


        CreateJsonForEditRssPostDetails: function () {
            debugger;
            // var content = tinyMCE.get('txtMsgContent').getContent();
            var tab = $("#tabsRss .ui-tabs-active").text();
            if (tab == "FaceBook") {
                var content = $('#txtEditMsgContentfacebook').val();
                var category = $('#industryeditfacebook option:selected').val();
                var state = $("#stateeditfacebook").val();
                //if (state == "Select SubIndustry" || state=="") {
                state = 1;
                //}
                var stateId = $("#stateeditfacebook option:selected").text();
                var categoryName = $('#industryeditfacebook option:selected').text();
                var socialMedia = "Facebook";

                var JsonWorkInfo = {};
                // Get Images
                var arr = [];
                var arrayIds = [];
                var JsonWorkInfo = {};
                // Get Images
                //$('.dz-image-preview').each(function () {
                //    var text = $(this).children().eq(1).children().eq(1).text()
                //    arr.push(text);
                //});
                var urlLink = $('#EditUrlFacebook').val();
                //var text = $('#fbdiv .dz-image').find('img').attr('alt');
                //if (text == '') {
                var text = "";

                text = $('#fbdiv .dz-image').find('img').attr('src');
                if (text == undefined) {
                    text = "";
                }
                var newtxt = "";
                if (text.length > 1000) {
                    text = $('#fbdiv .dz-image').find('img').attr('alt');
                    newtxt = "/Images/WallImages/ContentImages/" + text;
                }
                else {
                    newtxt = "/" + text.split('/')[4] + "/" + text.split('/')[5] + "/" + text.split('/')[6] + "/" + text.split('/')[7];
                }

                var title = $("#txttitlefbEdit").val();
                var heading = $("#txtheadingfbEdit").val();


                //}
                var parsedURL = UrlParser(text);
                if (parsedURL.origin == undefined || parsedURL.origin == null) {
                    arr.push(newtxt);
                }
                else {
                    arr.push(text);
                }

                // Get keywords
                $('.label-info').each(function () {
                    var text = $(this).text();
                    arrayTags.push(text);
                });

            }
            else if (tab == "Twitter") {
                var content = $('#txtEditMsgContenttwitter').val();
                var category = $('#industryeditTiwtter option:selected').val();
                var state = $("#stateeditTiwtter").val();
                //if (state == "Select SubIndustry" || state == "") {
                state = 1;
                //}


                var title = $("#txttitletwEdit").val();
                var heading = $("#txtheadingtwEdit").val();

                var stateId = $("#stateeditTiwtter option:selected").text();
                var categoryName = $('#industryeditTiwtter option:selected').text();
                var socialMedia = "Twitter";
                var arr = [];
                var arrayIds = [];
                var arrayTags = [];
                var JsonWorkInfo = {};
                // Get Images
                //$('.dz-image-preview ').each(function () {
                //    var text = $(this).children().eq(1).children().eq(1).text()
                //    arr.push(text);
                //});
                //var text = $('#twitdiv .dz-image').find('img').attr('alt');
                //if (text == '') {
                var text = "";
                text = $('#twitdiv .dz-image').find('img').attr('src');
                if (text == undefined) {
                    text = "";
                }
                var newtxt = "";
                if (text.length > 1000) {
                    text = $('#twitdiv .dz-image').find('img').attr('alt');
                    newtxt = "/Images/WallImages/ContentImages/" + text;
                }
                else {
                    newtxt = "/" + text.split('/')[4] + "/" + text.split('/')[5] + "/" + text.split('/')[6] + "/" + text.split('/')[7];
                }
                var parsedURL = UrlParser(text);
                if (parsedURL.origin == undefined || parsedURL.origin == null) {

                    arr.push(newtxt);
                }
                else {
                    arr.push(text);
                }

                // Get keywords
                $('.label-info').each(function () {
                    var text = $(this).text();
                    arrayTags.push(text);
                });
                var urlLink = $('#EditUrlTwitter').val();

            }
            else if (tab == "LinkedIn") {
                var content = $('#txtEditMsgContentLinkedIn').val();
                var category = $('#industryeditLinkedIn option:selected').val();
                var state = $("#stateeditLinkedIn").val();
                //if (state == "Select SubIndustry" || state == "") {
                state = 1;
                //}
                var title = $("#txttitleliEdit").val();
                var heading = $("#txtheadingliEdit").val();

                var stateId = $("#stateeditLinkedIn option:selected").text();
                var categoryName = $('#industryeditLinkedIn option:selected').text();
                var socialMedia = "LinkedIn";
                var arr = [];
                var arrayIds = [];
                var arrayTags = [];
                var JsonWorkInfo = {};
                // Get Images
                //$('.dz-image-preview ').each(function () {
                //    var text = $(this).children().eq(1).children().eq(1).text()
                //    arr.push(text);
                //});

                //var text = $('#Linkdiv .dz-image').find('img').attr('alt');
                //if (text == '') {

                var text = $('#Linkdiv .dz-image').find('img').attr('src');
         
           

                if (text == undefined) {
                    text = "";
                }
                var newtxt = "";
                if (text.length > 1000) {
                    text = $('#Linkdiv .dz-image').find('img').attr('alt');
                    newtxt = "/Images/WallImages/ContentImages/" + text;
                }
                else {
                    newtxt = "/" + text.split('/')[4] + "/" + text.split('/')[5] + "/" + text.split('/')[6] + "/" + text.split('/')[7];
                }



                var parsedURL = UrlParser(text);
                if (parsedURL.origin == undefined || parsedURL.origin == null) {
                  
                    arr.push(newtxt);
                }
                else {
                    arr.push(text);
                }
              
                var urlLink = $('#EditUrlLinkedIn').val();
                // Get keywords
                $('.label-info').each(function () {
                    var text = $(this).text();
                    arrayTags.push(text);
                });
            }

            JsonWorkInfo.TextMessage = content;
            // JsonWorkInfo.Ids = arrayIds;
            JsonWorkInfo.Array
            JsonWorkInfo.ImageArray = arr;
            JsonWorkInfo.Url = urlLink;
            JsonWorkInfo.ContentId = $('#contentId').val();
            JsonWorkInfo.CategoryId = category;
            JsonWorkInfo.CategoryName = categoryName;
            JsonWorkInfo.SubIndustryId = state;
            JsonWorkInfo.Title = title;
            JsonWorkInfo.Heading = heading;

            if (ContentSourceName != "") {
                JsonWorkInfo.ContentSource = "Rss";
            }
            else {
                JsonWorkInfo.ContentSource = "";
            }
            JsonWorkInfo.GroupId = $("#txtGroupId").val()
            if (stateId == 'Select SubIndustry') {
                JsonWorkInfo.SubIndustryName = "";
            }
            else {
                JsonWorkInfo.SubIndustryName = stateId;
            }

            JsonWorkInfo.SocialMedia = socialMedia;
            Dashboard.AjaxCalls.PostData(JsonWorkInfo);
        },



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

function UrlParser(str) {
    "use strict";

    var regx = /^(((([^:\/#\?]+:)?(?:(\/\/)((?:(([^:@\/#\?]+)(?:\:([^:@\/#\?]+))?)@)?(([^:\/#\?\]\[]+|\[[^\/\]@#?]+\])(?:\:([0-9]+))?))?)?)?((\/?(?:[^\/\?#]+\/+)*)([^\?#]*)))?(\?[^#]+)?)(#.*)?/,
        matches = regx.exec(str),
        parser = null;

    if (null !== matches) {
        parser = {
            href: matches[0],
            withoutHash: matches[1],
            url: matches[2],
            origin: matches[3],
            protocol: matches[4],
            protocolseparator: matches[5],
            credhost: matches[6],
            cred: matches[7],
            user: matches[8],
            pass: matches[9],
            host: matches[10],
            hostname: matches[11],
            port: matches[12],
            pathname: matches[13],
            segment1: matches[14],
            segment2: matches[15],
            search: matches[16],
            hash: matches[17]
        };
    }

    return parser;
};
function ShowMessage(type, message) {
    $messageData = $("<span>Information</span>");
    var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);

    BootstrapDialog.configDefaultOptions(
{
    cssClass: 'bstDialg'
}
);
    BootstrapDialog.show({
        title: $messageData,
        type: type,
        message: message,
    });
    //BootstrapDialog.show({
    //    title: $messageData,
    //    type: type,
    //    message: message,
    //    closable: true,
    //    closeByBackdrop: false,
    //    closeByKeyboard: false,
    //    buttons: [{
    //        label: 'Ok',
    //        action: function (dialogItself) {
    //            dialogItself.close();
    //            RefreshGrid();
    //            //$("#addContent").modal('hide');
    //            //$("#editContent").modal('hide');
    //        }
    //    }]
    //});
}


function countCharFacebook(val) {
    debugger;
    var len = val.value.length;
    var char = 63206;
    if (len >= char) {
        val.value = val.value.substring(0, char);
    } else {
        $('#charNumfacebook').text("Characters remaining ").append(char - len);
    }
};

function countCharTwitter(val) {
    debugger;
    var len = val.value.length;
    if (len >= char) {
        val.value = val.value.substring(0, char);
    } else {
        $('#charNumtwitter').text("Characters remaining ").append(char - len);
    }
};

function countCharLinkedIn(val) {
    debugger;
    var len = val.value.length;
    if (len >= char) {
        val.value = val.value.substring(0, char);
    } else {
        $('#charNumlinkedIn').text("Characters remaining ").append(char - len);
    }
};



function RefreshGrid() {

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
function RandomGenerator() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 9; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

