$(document).ready(function () {
    debugger;
    $('.btnAdd').hide()
    var Param = {};
    var $table = $("#grid");



    $('#resetBtn').on('click', function () {
        $("#custom-toolbar").find('input').val('');
        $table.bootstrapTable('refresh');
        $("#resetBtn").attr("disabled", "disabled");
    });

    $("#custom-toolbar input").on("keyup", function () {
        if ($(this).val() != '')
            $("#resetBtn").removeAttr("disabled");
        else
            $("#resetBtn").attr("disabled", "disabled");
    });

    $("#btnFilter").click(function () {

        var Description = $("#Description").val();
        var Url = $("#Url").val();


        if (Description != "") {
            Param.Description = Description;
        }
        else {
            Param.Description = "";
        }
        if (Url != "") {
            Param.Url = Url;
        }
        else {
            Param.Url = "";
        }

        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

    Param.currentUserId = $("#currentUserId").val();
    var reqUrl = $_BaseUrl + '/Users/api/HomeApi/GetAllSocialFuturePost';
    // alert(reqUrl);

    // Param.clinicId = $("#clinicId").val();

    $('#grid').bootstrapTable({
        headers: headers,
        method: 'post',
        url: reqUrl,
        cache: true,
        height: 700,
        classes: 'table table-hover',
        customParams: Param,
        // striped: true,
        pageNumber: 1,
        pagination: true,
        pageSize: 10,
        pageList: [5, 10, 20, 30],
        search: false,
        showColumns: true,
        showRefresh: true,
        sidePagination: 'server',
        minimumCountColumns: 2,
        showHeader: true,
        showFilter: false,
        smartDisplay: true,
        clickToSelect: true,
        // rowStyle: rowStyle,
        toolbar: '#custom-toolbar',
        columns: [
            {
                field: 'PostId',
                title: '#',
                checkbox: false,
                type: 'search',
                sortable: true,
                visible: false,
                switchable: false,
            }, {
                field: 'Social Media',
                title: 'Social Media',
                clickToSelect: false,
                formatter: function (value, row, index) {
                    //return ["<img src='" + row.Photo + "' height='40px' width='40px'/>"].join('');
                    if (row.SocialMedia == "Facebook") {
                        return ["<i class='fa fa-facebook'></i>"].join('');
                    }
                    else if (row.SocialMedia == "LinkedIn") {
                        return ["<i class='fa fa-linkedin' aria-hidden='true'></i>"].join('');
                    }
                    else if (row.SocialMedia == "Twitter") {
                        return ["<i class='fa fa-twitter' aria-hidden='true'></i>"].join('');
                    }
                    else if (row.SocialMedia == "GooglePlus") {
                        return ["<i class='fa fa-google-plus' aria-hidden='true'></i>"].join('');
                    }
                },
                events: operateEvents
            },
                 {
                     field: 'Description',
                     title: ' Details',
                     checkbox: false,
                     type: 'search',
                     events: operateEvents,
                     formatter: function (value, row, index) {


                         var fulltext = row.Name;
                         if (fulltext == null || fulltext == undefined || fulltext == "") {
                             fulltext = row.Caption;
                         }
                         debugger
                         if (fulltext.length >= 30) {
                             var shorttext = fulltext.substring(0, 24);
                             return [shorttext + '...', '<button id="read" title="Read More"><i class="fa fa-book"></i></button>'].join('');
                         }
                         else {
                             // return [row.Description].join('');
                             return row.Name + '...' + '<button id="read" title="Read More"><i class="fa fa-book"></i></button>';
                         }
                         return shorttext;
                         // return [row.Description].join('');
                     },
                 },
                  //{
                  //    field: 'Url',
                  //    title: 'Url',
                  //    type: 'search',
                  //    clickToSelect: false,
                  //    width: '100',
                  //    events: operateEvents,
                  //    formatter: function (value, row, index) {
                  //        if (row.Url != null) {
                  //            var fulltext = row.Url;
                  //            debugger
                  //            if (fulltext.length >= 30) {
                  //                var shorttext = fulltext.substring(0, 30);
                  //                // return shorttext
                  //                return ["<a href=" + fulltext + " class='text-info' target='_blank'>" + shorttext + "</a>"].join('');
                  //            }
                  //            else {
                  //                return ["<a href=" + fulltext + " class='text-info' target='_blank'>" + fulltext + "</a>"].join('');
                  //            }
                  //            return shorttext;
                  //        }
                  //    },                  
                  //},

                 {
                     field: 'PostDate',
                     title: ' Post Date',

                     checkbox: false,
                     type: 'search',
                     sortable: true,

                     formatter: function (value) {
                         debugger
                         var localDate = new Date(value);
                         var Date1 = localDate.toDateString('dddd, mmmm dS, yyyy, h:MM:ss TT');

                         var hour = localDate.getHours();
                         var minut = localDate.getMinutes();

                         var completeDate = Date1 + "  " + hour + ":" + minut;
                         return completeDate;
                     }
                 },
                {
                    field: 'CreatedByName',
                    title: 'Posted By',
                    type: 'search',
                    sortable: true,

                },
                  {
                      field: 'RoleType',
                      title: 'Role',
                      checkbox: false,
                      type: 'search',
                      formatter: function (value, row, index) {
                          if (row.RoleType == 1) {
                              return [" User "]
                          }
                          else if (row.RoleType == 2) {
                              return [" Admin "]
                          }
                          else if (row.RoleType == 3) {
                              return [" Super Admin "]
                          }
                      }
                  },
 {
     field: 'ImageUrl',
     title: 'Image',
     type: 'search',
     sortable: false,
     formatter: function (value, row, index) {
         // debugger;
         if (row.ImageUrl != null) {

             if (row.ImageUrl.indexOf("www") > -1 || row.ImageUrl.indexOf("http") > -1) {
                 var url = row.ImageUrl;
             }
             else {
                 var url = $_BaseUrl + row.ImageUrl;
             }


         }
         else {
             var url = $_BaseUrl + "/Images/noimage.png";
         }
         return ["<img src='" + url + "' height='40px' width='40px'/>"].join('');
     }
 }, {
     field: 'operate',
     title: 'Actions',
     clickToSelect: false,
     formatter: operateFormatter,
     events: operateEvents
 },

                //{
                //    field: 'checkbox',
                //    title: 'Actions',
                //    checkbox: false,
                //    clickToSelect: true,
                //    formatter: operateStatus,
                //    events: operateEvents
                //}
        ],
        onLoadSuccess: function () {
            // debugger;
            Addtitle();
            $('.btnAdd').hide()
        },
        onPageChange: function () {
            //    debugger;
            Addtitle();
            $('.btnAdd').hide()
        },

    });
});



function operateFormatter(value, row, index) {

    return [
        '<button id="edit" class="btn btn-link"  title="Edit">',
           '<i class="fa fa-pencil-square-o"></i>',
       '</button>',
        '<button id="delete" class="btn btn-link"  title="Remove">',
            '<i class="fa fa-trash"></i>',
        '</button>'
    ].join('');
}

//function operateStatus(value, row, index) {
//    if (row.IsActive == true) {
//       // debugger
//        return [
//               '<button id="edit" class="btn btn-link"  title="Edit">',
//           '<i class="fa fa-trash"></i> Edit',
//       '</button>',
//           //'<button id="activate" class="btn btn-link"  title="deactivate">',
//           //         '<i class="fa fa-trash"></i> Active',
//           //     '</button>'

//        ].join('');
//    }
//    else {
//        return [
//                   '<button id="deactivate" class="btn btn-link"  title="Activate">',
//                            '<i class="fa fa-trash"></i> Deactive',
//                        '</button>'

//        ].join('');
//    }

//}

window.operateEvents = {

    'click #read': function (e, value, row, index) {
        debugger;


        if (row.Url != null) {
            debugger;
            var fulltext = row.Url;
            debugger
            if (fulltext.length >= 30) {
                var shorttext = fulltext.substring(0, 65);
                // return shorttext
                var URL = ["<a href=" + fulltext + " class='text-info' target='_blank'>" + shorttext + "</a>"].join('');
            }
            else {
                var URL = ["<a href=" + fulltext + " class='text-info' target='_blank'>" + fulltext + "</a>"].join('');
            }

        }

        if (row.Caption != null) {

            var haeading = [row.Caption].join('');
        }
        else {
            var haeading = "";
        }

        //BootstrapDialog.alert('<strong>Heading:</strong> '+
        //   haeading + '</br></br><strong>Title:</strong> ' +
        //    row.Name
        //    );



        BootstrapDialog.alert(' <strong>Heading:</strong> ' +
         haeading.trim() + '</br>   </br> <strong>Title:</strong> ' +
          row.Name + '</br>   </br> <strong>Description:</strong> ' +
          row.Description.trim() + '</br>   </br> <strong>Link:</strong> ' +
          URL.trim()
          );


    },


    'click #delete': function (e, value, row, index) {
        // debugger;
        BootstrapDialog.show({
            title: 'Confirmation',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'No',
                cssClass: 'btn-danger',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            },
            {
                label: 'Yes',
                cssClass: 'btn-primary',
                action: function (dialogItself) {
                    //   debugger
                    $.ajax({

                        cache: false,
                        async: true,
                        type: "POST",
                        url: $_BaseUrl + "/Home/DeleteSocialPost/" + row.PostId,
                        success: function (data) {
                            //  debugger
                            if (data.status == true) {
                                RefreshGrid();
                                dialogItself.close();
                            }
                            else {
                                ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.');
                            }
                        },
                        error: function (request, error) {
                            if (request.statusText == "CustomMessage") {
                                $("#spanError").html(request.responseText)
                            }
                        },
                        headers: {
                            'RequestVerificationToken': $("#TokenValue").val()
                        }
                    });
                }
            }]
        });

    },
    'click #edit': function (e, value, row, index) {
           debugger;
        //$('form#dropzoneForm').children().remove();
        // $('.UploadedImage').remove();
        //$('.EditSocialMedia').each(function () {
        //    if ($(this).is(':checked')) {
        //        $(this).prop('checked', false);
        //    }
        //});

        //$('#categoryList').val('');
        //$('#Social_Media').val('');
        $('.label-info').each(function () {
            $(this).remove();
        });

        $('.loadercontainingdiv').show();
        $.ajax({

            cache: false,
            async: true,
            type: "POST",
            url: $_BaseUrl + '/Home/EditContent',
            data: { id: row.PostId },
            success: function (data) {
                debugger;
                $('.loadercontainingdiv').hide();
                $('#PostId').val(row.PostId);
                if (data.result != null) {
                    $("#editContent").modal("show");
                    $('textarea#txtEditMsgContent').html(data.result.Description);
                    $('textarea#EditUrl').html(data.result.Url);

                    $('textarea#txtEditKeywords').html(data.result.Name);
                    $('#categoryList option').each(function () {
                        var value = $(this).val();
                        if (value == data.result.CategoryId) {
                            $(this).attr('selected', true);
                            $('#categoryList').trigger('change');
                        }
                    })


                    //$('#Social_Media option').each(function () {
                    //    var value = $(this).text();
                    //    if (value == data.result.SocialMedia) {
                    //        $(this).attr('selected', true);
                    //    }
                    //});


                    if (data.result.ImageUrl != null) {

                        if (data.result.ImageUrl.indexOf("www") > -1 || data.result.ImageUrl.indexOf("http") > -1) {
                            var url = data.result.ImageUrl;
                        }
                        else {

                            var url = $_BaseUrl + data.result.ImageUrl;
                        }
                        // debugger;
                        //$('form#dropzoneForm').children().remove();
                       
                        // $('.UploadedImage').show();
                        $('.UploadedImage img').remove();
                        $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="" src="' + url + '"/></div></div>');
                    }
                    else {
                        $('.UploadedImage img').remove();
                    }

                    // $('#txtEditKeywords').html(data.result.Tags);
                    //var tags = data.result.Tags;
                    //if (tags != null) {
                    //    var splittedTags = tags.split(',');
                    //    var i;
                    //    for (i = 0; i < splittedTags.length; i++) {
                    //        var text = splittedTags[i];
                    //        $('.bootstrap-tagsinput').prepend('<span class="tag label label-info">' + text + '<span data-role="remove"></span></span>');
                    //    }
                    //}

                }
            },

            error: function (request, error) {
                $('.loadercontainingdiv').hide();
                if (request.statusText == "CustomMessage") {
                    $("#spanError").html(request.responseText)
                }
            },
            headers: {
                'RequestVerificationToken': $("#TokenValue").val()
            }
        });
    },
    //'click #activate': function (e, value, row, index) {
    //    debugger;
    //    var status = false;
    //    BootstrapDialog.show({
    //        title: 'Confirmation',
    //        message: 'Are you sure you want to deactivate this record?',
    //        buttons: [{
    //            label: 'No',
    //            cssClass: 'btn-danger',
    //            action: function (dialogItself) {
    //                dialogItself.close();
    //            }
    //        },
    //        {
    //            label: 'Yes',
    //            cssClass: 'btn-primary',
    //            action: function (dialogItself) {
    //                $.ajax({
    //                    cache: false,
    //                    async: true,
    //                    type: "POST",
    //                    url: $_BaseUrl + "/Home/UpdateSocialStatusDeactive/" + row.Fid,
    //                    success: function (data) {
    //                        debugger
    //                        if (data.status == true) {
    //                            RefreshGrid();
    //                            dialogItself.close();
    //                        }
    //                        else {
    //                            ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.');
    //                        }
    //                    },
    //                    error: function (request, error) {
    //                        if (request.statusText == "CustomMessage") {
    //                            $("#spanError").html(request.responseText)
    //                        }
    //                    },
    //                    headers: {
    //                        'RequestVerificationToken': $("#TokenValue").val()
    //                    }
    //                });
    //            }
    //        }]
    //    });

    //},

    //'click #deactivate': function (e, value, row, index) {
    //    // debugger;
    //    var status = false;
    //    BootstrapDialog.show({
    //        title: 'Confirmation',
    //        message: 'Are you sure you want to activate this record?',
    //        buttons: [{
    //            label: 'No',
    //            cssClass: 'btn-danger',
    //            action: function (dialogItself) {
    //                dialogItself.close();
    //            }
    //        },
    //        {
    //            label: 'Yes',
    //            cssClass: 'btn-primary',
    //            action: function (dialogItself) {
    //                $.ajax({
    //                    cache: false,
    //                    async: true,
    //                    type: "POST",
    //                    url: $_BaseUrl + "/Home/UpdateSocialStatusActive/" + row.Fid,
    //                    success: function (data) {
    //                        debugger
    //                        if (data.status == true) {
    //                            RefreshGrid();
    //                            dialogItself.close();
    //                        }
    //                        else {
    //                            ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.');
    //                        }
    //                    },
    //                    error: function (request, error) {
    //                        if (request.statusText == "CustomMessage") {
    //                            $("#spanError").html(request.responseText)
    //                        }
    //                    },
    //                    headers: {
    //                        'RequestVerificationToken': $("#TokenValue").val()
    //                    }
    //                });
    //            }
    //        }]
    //    });

    //}


};


function Addtitle() {
    $('.page-next').attr('Title', 'Next');
    $('.page-first').attr('Title', 'First');
    $('.page-last').attr('Title', 'Last');
    $('.page-pre').attr('Title', 'Previous');
}

function AddNew() {
    $('#editStaff input[type="text"]').val(''); //Empty all fields of the edit form on click of the add staff button
    $("#editStaff").modal("hide");
    $("#addStaff").modal("show");
}

function rowStyle(row, index) {
    var classes = ['active', 'success', 'info', 'warning', 'danger'];

    if (index % 2 === 0) {
        return {
            classes: classes[1]
        };
    }
    return {};
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
            }
        }]
    });
}

//function ShowMessage(type, message) {
//    $messageData = $("<span>Information</span>");
//    BootstrapDialog.show({
//        title: $messageData,
//        type: type,
//        message: message,
//        closable: true,
//        closeByBackdrop: false,
//        closeByKeyboard: false,
//        buttons: [{
//            label: 'Ok',
//            action: function (dialogItself) {
//                dialogItself.close();
//                RefreshGrid();
//                // $("#addStaff").modal('hide');
//            }
//        }]
//    });
//}
var $table = $("#grid");
var chk = false;
function RefreshGrid() {

    document.getElementById("btnFilter").click();

}