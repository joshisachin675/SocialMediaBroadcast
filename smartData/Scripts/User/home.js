
$(document).ready(function () {
    debugger;

    $("#txtpostheading").blur(function () {
        if ($("#txtpostheading").val() != "") {
            $("#txtpostheading").css("border", "1px solid #cccccc").removeAttr("title");
        }
    });

    $("#txtpostTitle").blur(function () {
        if ($("#txtpostTitle").val() != "") {
            $("#txtpostTitle").css("border", "1px solid #cccccc").removeAttr("title");
        }
    });

    $("#txtpostLink").blur(function () {
        if ($("#txtpostLink").val() != "") {
            $("#txtpostLink").css("border", "1px solid #cccccc").removeAttr("title");
        }
    });

    $("#txtMsgContent").blur(function () {
        if ($("#txtMsgContent").val() != "") {
            $("#txtMsgContent").css("border", "1px solid #cccccc").removeAttr("title");
        }
    });


    var isSerach = false;
    $("#categoryname").tagsinput({
        maxTags: 5
    });

        var start = moment().subtract(29, 'days');
        var end = moment();

        //function cb(start, end) {
        //    debugger;
        //    if (start._d != 'Invalid Date') {
        //        $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        //    }
        //    else {

        //        $('#reportrange span').html("");
        //    }
          
        //}

        $('#reportrange').daterangepicker({
            startDate: start,
            endDate: end,
            ranges: {
                'Today': [moment(), moment()],             
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'Total': [moment(''), moment('')],
            }
        });

        //cb(start, end);

        $('#reportrange').on('apply.daterangepicker', function (ev, picker) {
            debugger;
            var startDate = picker.startDate.format('YYYY-MM-DD');
           var endDate = picker.endDate.format('YYYY-MM-DD');         
           $.ajax({
               url: $_UrlGetPostsCounts,
                       type: "Get",
                       data: {"startDate": startDate, "endDate": endDate },
                       success: function (result) {
                           debugger;
                           if (result.postCount != null) {
                               $("#facebookPosts").html(result.postCount.FacebookPosts);
                               $("#TwitterPosts").html(result.postCount.TwitterPosts);
                               $("#linkedInPosts").html(result.postCount.LinkedInPosts);
                           }
                          // alert(result)              
                       },

                   }); 
        });

    //var logo = $_BaseUrl + '/Images/logo2.png';
    //function updateCoords(coords) {
    //    $("#posx").val(coords.x);
    //    $("#posy").val(coords.y);
    //    $("#width").val(coords.width);
    //    $("#height").val(coords.height);
    //    $("#opacity").val(coords.opacity);
    //}
    ////watermarker-image myImage superImage

    //$(".watermarker-image").watermarker({
    //    imagePath: logo,
    //    offsetLeft: 30,
    //    offsetTop: 40,
    //    onChange: updateCoords,
    //    onInitialize: updateCoords,
    //    containerClass: "myContainer container",
    //    watermarkImageClass: "myImage superImage",
    //    watermarkerClass: "js-watermark-1 js-watermark",
    //    data: { id: 1, "class": "superclass", pepe: "pepe" },
    //    onRemove: function () {
    //        if (typeof console !== "undefined" && typeof console.log !== "undefined") {
    //            console.log("Removing...");
    //        }
    //    },
    //    onDestroy: function () {
    //        if (typeof console !== "undefined" && typeof console.log !== "undefined") {
    //            console.log("Destroying...");
    //        }
    //    }
    //});


    //$(document).on("mousemove", function (event) {
    //    $("#mousex").val(event.pageX);
    //    $("#mousey").val(event.pageY);
    //});


    //$("#watermark").click(function () {
    //    debugger;
    //    var img = $(".dz-image").find('img').attr('alt');
    //    var userId = $("#hdnUserId").val();
    //    $.ajax({
    //        url: $_WatermarkImg,
    //        type: "Get",
    //        data: { "img": img, "userId": userId },
    //        success: function (result) {
    //            debugger;
    //            alert(result)              
    //        },

    //    });
    //})


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

    $("#watermark").click(function () {
        debugger;
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
               .image(watermark.text.lowerLeft(resp.ImageText,resp.TextSiz + 'px ' + resp.Fontfamily, resp.Textcolor, resp.Opacity))
              .then(function (img) {
                  debugger;
                  document.getElementById('wetrrmrk').appendChild(img);
                  $(img).attr('height', '200px');
              });
                }
                else if (resp.Gravity == "se") {
                    watermark([imdf])
             .image(watermark.text.lowerRight(resp.ImageText,resp.TextSiz +'px '+ resp.Fontfamily, resp.Textcolor, resp.Opacity))
            .then(function (img) {
                document.getElementById('wetrrmrk').appendChild(img);
                $(img).attr('height', '200px');
            });
                }
            }
        }
        else {
            watermark([imdf])
             .image(watermark.text.upperRight('SMB', '48px Josefin Slab', '#fff', 0.5))
              .then(function (img) {
                  document.getElementById('wetrrmrk').appendChild(img);
                  $(img).attr('height', '200px');
              });
        }
        $('#editwatermark').show();       
    })



    var imdf = $_BaseUrl + '/Images/linkedin.png';
    var logo = $_BaseUrl + '/Images/logo2.png';
    //$("#watermark").click(function () {
    //    if (resp != null) {
    //        debugger;
    //        $(".dz-image").find('img').watermark({
    //            path: resp.ImagePathLogo,
    //            text: resp.ImageText,
    //            textWidth: resp.TextWidth,
    //            textSize: resp.TextSiz,
    //            textColor: resp.Textcolor,
    //            textBg: resp.TextBg,
    //            gravity: resp.Gravity, // nw | n | ne | w | e | sw | s | se
    //            opacity: resp.Opacity,
    //            margin: resp.Margin,
    //            outputWidth: resp.OutputWidth,
    //            outputHeight: resp.OutputHeight,
    //            outputType: resp.OutPutType,
    //        });
    //    }

    //    $("#editwatermark").show();
    //});




    debugger;
    var Param = {};
    Param.categoryname = "";
    Param.UserType = $("#UserType").val();
    Param.IndustryId = $('#IndustryId').val();
    var $table = $("#grid");
    $("#btnFilter").click(function () {
        debugger;
        isSerach = true;
        var categoryname = $("#categoryname").tagsinput('items');
        if (categoryname.length != "") {
            Param.categoryname = categoryname;
        }
        else {
            Param.categoryname = "";
            isSerach = false;

            var types = [BootstrapDialog.TYPE_INFO];

            $.each(types, function (index, type) {
                BootstrapDialog.show({
                    type: type,
                    message: 'Please enter tags to search !',
                });
            });
        }
        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

    debugger;
    var noimg = $_BaseUrl + '/Images/noimage.png';

    var reqUrl = $_BaseUrl + '/Users/api/HomeApi/GetContentListByKeyword';
    $('#grid').bootstrapTable({
        // headers: headers,
        method: 'post',
        url: reqUrl,
        cache: true,
        height: 700,
        classes: 'table table-hover',
        customParams: Param,
        striped: true,
        pageNumber: 1,
        pagination: true,
        pageSize: 5,
        pageList: [5, 10, 20, 30],
        search: false,
        showColumns: false,
        // showRefresh: true,
        sidePagination: 'server',
        minimumCountColumns: 2,
        showHeader: true,
        showFilter: false,
        smartDisplay: true,
        clickToSelect: true,
        // rowStyle: rowStyle,
        toolbar: '#custom-toolbar',
        //queryParams: function (p) {
        //    return { tags: ta }
        //},
        columns: [
            {
                field: 'ContentId',
                title: '#',
                checkbox: false,
                type: 'search',
                sortable: true,
                visible: false,
                switchable: false,
            },
                  {
                      field: 'Description, ImageUrl, SocialMedia',
                      title: 'Content Library',
                      type: 'search',
                      sortable: true,
                      events: operateEvents,
                      formatter: function (value, row, index) {
                          var siteUrl = $_BaseUrl;
                          var btnHtml = ' <button id="postnowcontent" class="btn btn-link"  title="Post">Post Now</button>';
                          if (row.ImageUrl != null) {
                              var fulltext = row.Description;
                              var shorttext = "";
                              debugger;
                              if (fulltext.length >= 30) {
                                  shorttext = fulltext.substring(0, 30);
                                  var html = '  <button id="read" class=""  title="Read More"><i class="fa fa-pencil-square-o text-info"></i> Read More</button>';
                                  shorttext = shorttext + html;
                              }
                              else {
                                  shorttext = row.Description;
                              }

                              if (row.SocialMedia == "Facebook")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + siteUrl + "/" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-facebook'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'>" + btnHtml + "</td></tr></table>"].join('');

                              else if (row.SocialMedia == "LinkedIn")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + siteUrl + "/" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-linkedin'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'>" + btnHtml + "</td></tr></table>"].join('');

                              else if (row.SocialMedia == "Twitter")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + siteUrl + "/" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-twitter'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'>" + btnHtml + "</td></tr></table>"].join('');

                              else if (row.SocialMedia == "Google")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + siteUrl + "/" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-google-plus'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'></td></tr></table>"].join('');
                              else
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" +siteUrl + "/" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-rss'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'></td></tr></table>"].join('');
                          }
                          else {
                              var fulltext = row.Description;
                              var shorttext = "";
                              debugger;
                              if (fulltext.length >= 30) {
                                  shorttext = fulltext.substring(0, 30);
                                  var html = '  <button id="read" class=""  title="Read More"><i class="fa fa-pencil-square-o text-info"></i> Read More</button>';
                                  shorttext = shorttext + html;
                              }
                              else {
                                  shorttext = row.Description;
                              }
                              if (row.SocialMedia == "Facebook")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-facebook'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'>" + btnHtml + "</td>  </tr></table>"].join('');
                              else if (row.SocialMedia == "LinkedIn")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-linkedin'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'>" + btnHtml + "</td></tr></table>"].join('');
                              else if (row.SocialMedia == "Twitter")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-twitter'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'>" + btnHtml + "</td></tr></table>"].join('');
                              else if (row.SocialMedia == "Google")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-google-plus'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'></td></tr></table>"].join('');
                              else
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-rss'></i></td><td width='40%'>" + shorttext + "</td><td width='20%'></td></tr></table>"].join('');
                          }
                      }
                  }
        ],
        onLoadSuccess: function () {
            if (isSerach == true)
                $(".fixed-table-container").removeClass("hidden");

            else
                $(".fixed-table-container").removeClass("hidden");
        },
        onPageChange: function () {

        },
    });

    $('.bootstrap-tagsinput bootstrap-tagsinput-max').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            // alert($("#categoryname").tagsinput('items').length);
            if ($("#categoryname").tagsinput('items').length > 5) {
                alert("You cannot enter more than 5 tags");
            }
        }
        event.stopPropagation();
    });

});



window.operateEvents = {
    'click #read': function (e, value, row, index) {
        BootstrapDialog.show({
            title: 'Description',
            message: row.Description,
            buttons: [{
                label: 'ok',
                cssClass: 'btn-primary',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }],
        });

    },

    'click #postnowcontent': function (e, value, row, index) {
        debugger;
        Dashboards.BindDataMethods.CreateJsonForRePostDetails(e, value, row, index);
    },

}


$(function () {
    //  Dashboards.Init();

});

var Dashboards = {
    AjaxCalls: {
        PostData: function (PostInformation) {
            debugger;
            $('.loadercontainingdiv').show();
            $.ajax({
                url: $_PostDetails,
                type: "POST",
                data: { "PostInformation": JSON.stringify(PostInformation) },
                success: function (result) {
                    debugger;
                    if (result.ErrorModel == "") {
                        BootstrapDialog.alert("Error posting on facebook");
                    }
                    else {
                        BootstrapDialog.alert(result.ErrorModel);
                    }
                    if (result.ErrorModel == "<br /> Content posted successfully on LinkedIn.") {
                        BootstrapDialog.alert(' Success !');
                    }
                },

            });
        },
    },
    BindDataMethods: {
        CreateJsonForRePostDetails: function (e, value, row, index) {
            debugger;
            var arr = [];
            if (row.ImageUrl != null) {
                var image_name = row.ImageUrl.split('/')[4]
                arr.push("*" + image_name);
            };

            var Ids = [];
            var JsonWorkInfo = {};
            // Get Images
            $('row.ImageUrl').each(function () {
                debugger
                var text = $(this).children().eq(1).children().eq(1).text()
                arr.push(text);
            });
            JsonWorkInfo.TextMessage = row.Description;
            var postUrl = $_BaseUrl + '/Users/api/PostApi/GetSocilaMediaAccountByName';
            var SocialMediaProfileId = 0;
            $.ajax({
                url: postUrl,
                type: 'Get',
                data: { Name: row.SocialMedia },
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger;
                    SocialMediaProfileId = data.Fid;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                }
            });
            Ids.push(SocialMediaProfileId);
            JsonWorkInfo.Ids = Ids;
            debugger;
            if (arr[0] == undefined) {
                JsonWorkInfo.ImageArray = [];
            }
            else {
                JsonWorkInfo.ImageArray = arr;
            }


            // Get selected dropdown value
            var postType = $('.schedule option:selected').val();
            JsonWorkInfo.PostType = 1;
            // Get DateTime
            if (postType == 2) {
                var dateTime = row.dateTime;
                JsonWorkInfo.ScheduledTime = dateTime;
            }
            Dashboard.AjaxCalls.PostData(JsonWorkInfo);
        },
    },

}



