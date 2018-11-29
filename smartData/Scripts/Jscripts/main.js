$(function () {
    var $path = $('#path'),
        $newlogo = $('#logoImg');
    $text = $('#text'),
    $textConfig = $('#textConfig'),
    $font = $('#textConfigs'),
    $imageDemo = $('#imageDemo'),
    $withWatermark = $('#withWatermark'),
    obj = {};
    $text.on('input', function () {
        if ($.trim($text.val()) !== '') {
            $path.prop('disabled', true);
            $newlogo.slideUp();
            $textConfig.slideDown();
            $font.slideDown();
        } else {
            $path.prop('disabled', false);
            $textConfig.slideUp();
            $newlogo.slideDown();
        }
    });


    $('#font').fontselect().change(function () {
        debugger;
        // replace + signs with spaces for css
        var font = $(this).val().replace(/\+/g, ' ');

        // split font into family and weight
        font = font.split(':');

        // set family on paragraphs
        //   $('p').css('font-family', font[0]);
    });


    $("#sub").click(function () {
        debugger;
        $("#waterrmark").html("");
        var imdf = $_BaseUrl + '/Images/social-media2.jpg';
        var userId = $("#hdnUserId").val();
        var logo = "";
        if ($('.dz-image-preview').children().eq(1).children().eq(1).text() != "") {
            var img = $('.dz-image-preview').children().eq(1).children().eq(1).text();
            logo = $_BaseUrl + '/Images/WallImages/' + userId + '/LogoImages/' + img;
        }
        else {
            logo = $_BaseUrl + '/Images/logo2.png';
        }
        var imgText = "";
        if ($('#text').val() != "") {
            if ($('#text').val().search(' ™') != -1) {
                imgText = $('#text').val();
            }
            else {
                imgText = $('#text').val() + ' ™';
            }           
        }
        else {
            imgText = "";
        }

        var txtSize = $('#textSize').val();
        var txtColor = $('#textColor').val();
        var gravity = $('#gravity').val();
        var opacity = $('#opacity').val();
        var fontfamily = $("#font").val().replace(/\+/g, ' ');
        if (fontfamily == "") {
            fontfamily = $('.font-select').find('span').html().replace(/\+/g, ' ');
        }
        var error = $('.dz-error-message').find('span').html();
        var fn = 50;
        if (error != "" && error != undefined) {
            BootstrapDialog.show({
                title: 'Information',
                message: "Image size too large",
                buttons: [{
                    label: 'ok',
                    cssClass: 'btn-primary',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }],
            });
        }
       else if (imgText != "") {
            //    watermark([imdf])
            // .image(watermark.text.upperRight(imgText, txtSize + 'px ' + fontfamily, txtColor, opacity, fn))
            //.then(function (img) {
            //    debugger;
            //    // $('#waterrmark').appendChild(img);
            //    document.getElementById('waterrmark').appendChild(img);
            //    //$(img).attr('height', '200px');
            //});

            if (gravity == "ne") {
                watermark([imdf])
             .image(watermark.text.upperRight(imgText, txtSize + 'px ' + fontfamily, txtColor, opacity, fn))
            .then(function (img) {
                document.getElementById('waterrmark').appendChild(img);
                // $(img).attr('height', '200px');
            });
            }
            else if (gravity == "nw") {
                watermark([imdf])
            .image(watermark.text.upperLeft(imgText, txtSize + 'px ' + fontfamily, txtColor, opacity, fn))
           .then(function (img) {
               document.getElementById('waterrmark').appendChild(img);
               //$(img).attr('height', '200px');
           });
            }
            else if (gravity == "sw") {
                watermark([imdf])
           .image(watermark.text.lowerLeft(imgText, txtSize + 'px ' + fontfamily, txtColor, opacity))
          .then(function (img) {
              document.getElementById('waterrmark').appendChild(img);
              //$(img).attr('height', '200px');
          });
            }
            else if (gravity == "se") {
                watermark([imdf])
         .image(watermark.text.lowerRight(imgText, txtSize + 'px ' + fontfamily, txtColor, opacity))
        .then(function (img) {
            document.getElementById('waterrmark').appendChild(img);
            //$(img).attr('height', '200px');
        });
            }
        }
        else if (logo != "") {

            if (gravity == "ne") {
                watermark([imdf, logo])
                                       .image(watermark.image.upperRight(opacity))
                                       .then(function (img) {
                                           debugger;
                                           document.getElementById('waterrmark').appendChild(img);
                                           //$(img).attr('height', '200px');
                                       })
            }
            else if (gravity == "nw") {
                watermark([imdf, logo])
                                       .image(watermark.image.upperLeft(opacity))
                                       .then(function (img) {
                                           document.getElementById('waterrmark').appendChild(img);
                                           //$(img).attr('height', '200px');
                                       })
            }
            else if (gravity == "sw") {
                watermark([imdf, logo])
                                       .image(watermark.image.lowerLeft(opacity))
                                       .then(function (img) {
                                           document.getElementById('waterrmark').appendChild(img);
                                           //$(img).attr('height', '200px');
                                       })
            }
            else if (gravity == "se") {
                watermark([imdf, logo])
                                       .image(watermark.image.lowerRight(opacity))
                                       .then(function (img) {
                                           document.getElementById('waterrmark').appendChild(img);
                                           // $(img).attr('height', '200px');
                                       })
            }
        }
    });


    //$('#watermarkConfig').on('submit', function (e) {
    //    e.preventDefault();
    //    obj = {};
    //    $.each($(this).serializeArray(), function (i, v) {
    //        var val = v.value;
    //        if (/^(textWidth|textSize|opacity|margin|outputWidth|outputHeight)$/.test(v.name) && val !== 'auto') {
    //            val = parseFloat(val);
    //        }
    //        obj[v.name] = val;
    //    });
    //    watermarkCreate();
    //}).on('reset', function () {
    //    $path.prop('disabled', false);
    //    $textConfig.slideUp();
    //});




    $("#saveWaterMark").click(function () {
        debugger;
        //var imgPath = $('#path').val();
        var imgPath = $('.dz-image-preview').children().eq(1).children().eq(1).text();
        var error = $('.dz-error-message').find('span').html();
        var imgText = $('#text').val();
        if (imgText != "") {

            if (imgText.search(' ™') != -1) {
                imgText = imgText;              
            }
            else {
                imgText = $('#text').val() + ' ™';
            }            
        }
        else {
            imgText = "";
        }
        var txtwidth = $('#textWidth').val();
        var txtSize = $('#textSize').val();
        var txtColor = $('#textColor').val();
        var txtBg = $('#textBg').val();
        var fontfamily = $("#font").val().replace(/\+/g, ' ');
        var gravity = $('#gravity').val();
        var opacity = $('#opacity').val();
        var margin = $('#margin').val();
        var outwdth = $('#outputWidth').val();
        var outHeight = $('#outputHeight').val();
        var outType = $('#outputType').val();
        if (error != "" && error != undefined) {
            BootstrapDialog.show({
                title: 'Information',
                message: "Image size too large",
                buttons: [{
                    label: 'ok',
                    cssClass: 'btn-primary',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }],
            });
        }
       else if (imgText.length == 0 && imgPath.length == 0) {
            BootstrapDialog.show({
                title: 'Information',
                message: "Please enter Image Logo or Image Text",
                buttons: [{
                    label: 'ok',
                    cssClass: 'btn-primary',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }],
            });
            $("#path").css('border', '1px solid red');
            $("#text").css('border', '1px solid red');
        }
        else {
            $("#path").css('border', '1px solid #c4c4c4');
            $("#text").css('border', '1px solid #c4c4c4');
            var JsonWorkInfo = {};
            JsonWorkInfo.ImagePathLogo = imgPath;
            JsonWorkInfo.ImageText = imgText;
            JsonWorkInfo.TextWidth = txtwidth;
            JsonWorkInfo.TextSiz = txtSize;
            JsonWorkInfo.Textcolor = txtColor;
            JsonWorkInfo.TextBg = txtBg;
            JsonWorkInfo.Gravity = gravity;
            JsonWorkInfo.Fontfamily = fontfamily;
            JsonWorkInfo.Opacity = opacity;
            JsonWorkInfo.Margin = margin;
            JsonWorkInfo.OutputWidth = outwdth;
            JsonWorkInfo.OutputHeight = outHeight;
            JsonWorkInfo.OutPutType = outType;
            var PostInformation = JsonWorkInfo;
            $.ajax({
                url: $_savewaterMarkImage,
                type: "POST",
                data: { "PostInformation": JSON.stringify(PostInformation) },
                success: function (result) {
                    if (result == "true") {
                        var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
                        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                        BootstrapDialog.show({ title: "<span>Success</span>", type: BootstrapDialog.TYPE_SUCCESS, message: 'Saved Successfully!.' });
                        
                    }
                },

            });
        }
    })


    //function watermarkCreate() {
    //    debugger;
    //    var userId = $("#hdnUserId").val();
    //    var logo = "";
    //    if ($('.dz-image-preview').children().eq(1).children().eq(1).text() != "") {
    //        var img = $('.dz-image-preview').children().eq(1).children().eq(1).text();
    //        logo = $_BaseUrl + '/Images/WallImages/' + userId + '/LogoImages/' + img;
    //    }
    //    else {
    //        logo = $_BaseUrl + '/Images/logo2.png';
    //    }
    //    var imdf = $_BaseUrl + '/Images/social_media.png';
    //    var config = $.extend({}, {
    //        path: logo,
    //        text: '',
    //        textWidth: 130,
    //        textSize: 13,
    //        textColor: 'white',
    //        //textBg: '',
    //        gravity: 'ne', // nw | n | ne | w | e | sw | s | se
    //        opacity: 0.7,
    //        margin: 1,

    //        outputWidth: 'auto',
    //        outputHeight: 'auto',
    //        outputType: 'jpeg', // jpeg | png | webm

    //        done: function (imgURL) {
    //            debugger;
    //            $withWatermark.html('<img id="imageDemo" src="' + imgURL + '">');
    //        },
    //        fail: function (imgURL) {
    //            $withWatermark.html('<span style="color: red;">Lỗi ảnh: ' + imgURL + '</span>');
    //        }
    //    }, obj);
    //    $('<img>', {
    //        src: imdf
    //    }).watermark(config)
    //}

    var resp = null;
    var userId = $("#hdnUserId").val();
    $.ajax({
        url: $_getwaterMarkImageDetails,
        type: "Get",
        data: { "userId": userId },
        success: function (result) {
            debugger;
            resp = result;
            if (resp != null) {
                if (resp.ImagePathLogo != "" && resp.ImagePathLogo!=null) {
                    $("#textConfig").slideUp();
                    $("#textConfigs").slideUp();
                }
                if (resp.ImageText != "") {
                    $("#logoImg").slideUp();
                }
                $('#text').val(resp.ImageText);
                $('#textSize').val(resp.TextSiz);
                $('#textColor').val(resp.Textcolor);
                $('.font-select').find('span').html(resp.Fontfamily);
                $('#gravity').val(resp.Gravity);
                $('#opacity').val(resp.Opacity);
                var logo = $_BaseUrl + '/Images/WallImages/' + userId + '/LogoImages/' + resp.ImagePathLogo;
                if (resp.ImagePathLogo != "")
                {
                    $('.UploadedImage img').remove();
                    $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="' + resp.ImagePathLogo + '" src="' + logo + '"/></div></div>');
                }
                else {
                    $("#uploadImage").css("display", "none");
                }
               
            }

        },
    });



    //watermarkCreate();

});



