$(document).ready(function () {
    debugger;

    //$.ajax({
    //    url: $_GetContentFromSocialMedia,
    //    type: 'Get',
    //    data: { },
    //    async: false,
    //    contentType: "application/json; charset=utf-8",
    //    success: function (data) {
    //        debugger;
    //        //$('#postText').html(row.Description);
    //        //if (row.SocialMedia == "Facebook") {
    //        //    $('#postMedia').html("<i class='fa fa-facebook' aria-hidden='true'></i>");
    //        //}
    //        //else if (row.SocialMedia == "LinkedIn") {
    //        //    $('#postMedia').html("<i class='fa fa-linkedin' aria-hidden='true'></i>");
    //        //}
    //        //else if (row.SocialMedia == "Twitter") {
    //        //    $('#postMedia').html("<i class='fa fa-twitter' aria-hidden='true'></i>");
    //        //}
    //        //else if (row.SocialMedia == "GooglePlus") {
    //        //    $('#postMedia').html("<i class='fa fa-google-plus' aria-hidden='true'></i>");
    //        //}

    //        //if (data.response.LikesCount != 0) {
    //        //    var likeName = "";
    //        //    $("#postlikes").html(data.response.LikesCount);
    //        //    $.each(data.response.LikesName, function (i, val) {
    //        //        debugger;
    //        //        likeName = likeName + '<p>' + val + '</p>';
    //        //        //$('#likesName').attr('title', '<p>' + likeName + '</p>');   
    //        //    })
    //        //    $('#likesName').append(likeName);
    //        //}
    //        //else if (data.response.linkedInlikes != 0) {
    //        //    var likeName = "";
    //        //    $("#postlikes").html(data.response.linkedInlikes._total);
    //        //    if (data.response.linkedInlikes.values != undefined) {
    //        //        $.each(data.response.linkedInlikes.values, function (i, val) {
    //        //            debugger;
    //        //            likeName = likeName + '<p>' + val.person.firstName + '</p>';
    //        //            //$('#likesName').attr('title', '<p>' + likeName + '</p>');   
    //        //        })
    //        //    }
    //        //    $('#likesName').append(likeName);

    //        //}
    //        //else {
    //        //    $("#postlikes").html(0);
    //        //}

    //        //if (data.response.fbcomment != undefined) {
    //        //    debugger;
    //        //    $.each(data.response.fbcomment.data, function (i, val) {
    //        //        debugger;
    //        //        $('#postComments').append('<p>' + '<b>' + val.from.name + '</b>' + ':' + val.message + '</p>');
    //        //        //$('#postCommentsNames').append('<p>' + + '</p>');
    //        //    })
    //        //    $('#commentsCount').html(data.response.fbcomment.data.length)
    //        //}
    //        //else if (data.response.linkedincomment != undefined) {
    //        //    $.each(data.response.linkedincomment.values, function (i, val) {
    //        //        debugger;
    //        //        $('#postComments').append('<p>' + '<b>' + val.person.firstName + '</b>' + ':' + val.comment + '</p>');
    //        //    })
    //        //    $('#commentsCount').html(data.response.linkedincomment.values.length)
    //        //    //$.each(data.response.CommentsName, function (i, val) {
    //        //    //    debugger;
    //        //    //    $('#postCommentsNames').append(val);
    //        //    //})
    //        //}
    //        //else {
    //        //    $("#postComments").html();
    //        //    $('#commentsCount').html(0);
    //        //}


    //    },
    //    error: function (xhr, ajaxOptions, thrownError) {

    //    }

    //});




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
        debugger
        var Name = $("#").val();
        var Description = $("#Description").val();
        var Url = $("#Url").val();

        //if (Name != "") {
        //    Param.Name = Name;
        //}
        //else {
        //    Param.Name = "";
        //}
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
        //if (phoneNumber != "") {
        //    Param.phoneNumber = phoneNumber;
        //}
        //else {
        //    Param.phoneNumber = "";
        //}
        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

    Param.currentUserId = $("#currentUserId").val();
    var reqUrl = $_BaseUrl + '/Users/api/HomeApi/GetAllSocialPost';
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
                field: 'Fid',
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
                    sortable: true,
                    events: operateEvents,
                    formatter: function (value, row, index) {
                        var fulltext = row.Name;
                        if (fulltext == null || fulltext==undefined ||fulltext =="") {
                            fulltext = row.Description;
                        }
                        debugger
                        if (fulltext.length >= 30) {
                            var shorttext = fulltext.substring(0, 23);
                            return [shorttext+'...','<button id="read" title="Read More"><i class="fa fa-book"></i></button>'].join('');
                        }
                        else {
                           // return [row.Description].join('');
                            return row.Name + '...' + '<button id="read" title="Read More"><i class="fa fa-book"></i></button>';
                        }
                        return shorttext;
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
                //            debugger;
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
                      field: 'LikesCount',
                      title: 'Likes',
                      type: 'search',
                      sortable: true,
                      formatter: function (value, row, index) {
                          debugger;
                          if (row.LikesNames != null && row.SocialMedia != "Twitter") {
                              debugger
                              var likeName = "";
                              var name = row.LikesNames.split(",");
                              var html = "";
                              html = "<div class='likes-wrap'>"
                              html += "<div id='likesNames' class='like-hover'>"
                              html += "<span class='fa fa-thumbs-up'></span>"
                              html += "<span id='postlikes'> " + row.LikesCount + "  </span>"
                              html += "</div>"
                              $.each(name, function (i, val) {
                                  debugger;
                                  likeName = likeName + '<p>' + val + '</p>';
                              });
                              if (row.LikesCount != 0) {
                                  html += "<div id='showNames' class='onhover-like' >"
                                  html += "<div id='likesName'> " + likeName + " </div>"
                                  html += "</div>"
                                  html += "</div>"
                              }
                              return [html]
                          }
                          else {
                              var html = "";
                              html = "<div class='likes-wrap'>"
                              html += "<div id='likesNames' class='like-hover'>"
                              html += "<span class='fa fa-thumbs-up'></span>"
                              html += "<span id='postlikes'> " + row.LikesCount + "  </span>"
                              html += "</div>"
                              return [html]
                          }
                      }

                  },
                   {
                       field: 'CommentsCount',
                       title: 'Comments',
                       type: 'search',
                       sortable: true,
                       formatter: function (value, row, index) {
                           debugger
                           if (row.CommentsText != null) {
                               debugger
                               var likeName = "";
                               var name = row.CommentsText.split(",");
                               var html = "";
                               html = "<div class='likes-wrap'>"
                               html += "<div id='likesNames' class='like-hover'>"
                               html += "<span class='fa fa-comments'></span>"
                               html += "<span id='postlikes'> " + row.CommentsCount + "  </span>"
                               html += "</div>"
                               $.each(name, function (i, val) {
                                   debugger;
                                   likeName = likeName + '<p>' + val + '</p>';
                               });
                               if (row.CommentsCount != 0) {
                                   html += "<div id='showNames' class='onhover-like'>"
                                   html += "<div id='likesName'> " + likeName + " </div>"
                                   html += "</div>"
                                   html += "</div>"
                               }
                               return [html]
                           }
                       }

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
                    formatter: function (value, row, index) {
                        debugger;
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

                  //  {
                    //    field: 'checkbox',
                    //    title: 'Status',
                    //    checkbox: false,
                    //    clickToSelect: true,
                    //    formatter: operateStatus,
                    //    events: operateEvents
                    //}
                  //  }
        ],
        onLoadSuccess: function () {
            debugger;
            Addtitle();
            $('.btnAdd').hide()
        },
        onPageChange: function () {
            debugger;
            Addtitle();
            $('.btnAdd').hide()
        },

    });
});


function operateFormatter(value, row, index) {
    return [
        '<button id="post" class="btn btn-link"  title="Post Now">',
            '<i class="fa fa-edit"></i> RePublish',
        '</button>'
        // '<button id="viewpost" class="btn btn-link"  title="View Post">',
        //    '<i class="icon-eye-open"></i> View',
        //'</button>'
    ].join('');
}

//function operateStatus(value, row, index) {
//    if (row.IsActive == true) {
//        return [
//           '<button id="activate" class="btn btn-link"  title="deactivate">',
//                    '<i class="fa fa-trash"></i> Active',
//                '</button>'

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
        debugger
     
        if (row.Url != null) {
            debugger;
            var fulltext = row.Url;
            debugger
            if (fulltext.length >= 30) {
                var shorttext = fulltext.substring(0, 65);
                // return shorttext
                var URL =  ["<a href=" + fulltext + " class='text-info' target='_blank'>" + shorttext + "</a>"].join('');
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
          row.Name.trim() + '</br>   </br> <strong>Description:</strong> ' +
          row.Description.trim() + '</br>   </br> <strong>Link:</strong> ' +
          URL.trim()
          );

       /// BootstrapDialog.alert(row.Description);

    },
    'click #post': function (e, value, row, index) {
        list = [];
        var message1 = 'You can,t post more than 140 character in Twitter  !';
        var message2 = 'You can,t post more than 600 character in LinkedIn   !';
        var both = message1.concat(message2);
        Dashboard.BindDataMethods.CreateJsonForRePostDetails(e, value, row, index);
    },

    //'click #viewpost': function (e, value, row, index) {
    //    debugger;
    //    $('#postText').html("");
    //    $("#postlikes").html("");
    //    $('#commentsCount').html("");
    //    $('#likesName').html("");
    //    $('#postCommentsNames').html("");
    //    $("#postComments").html("");
    //    var postId = row.PostId;
    //    var socialMedia = row.SocialMedia;

    //    $.ajax({
    //        url: $_GetContentFromSocialMedia,
    //        type: 'Get',
    //        data: { postId: postId, socialMedia: socialMedia },
    //        async: false,
    //        contentType: "application/json; charset=utf-8",
    //        success: function (data) {
    //            debugger;
    //            $('#postText').html(row.Description);
    //            if (row.SocialMedia == "Facebook") {
    //                $('#postMedia').html("<i class='fa fa-facebook' aria-hidden='true'></i>");
    //            }
    //            else if (row.SocialMedia == "LinkedIn") {
    //                $('#postMedia').html("<i class='fa fa-linkedin' aria-hidden='true'></i>");
    //            }
    //            else if (row.SocialMedia == "Twitter") {
    //                $('#postMedia').html("<i class='fa fa-twitter' aria-hidden='true'></i>");
    //            }
    //            else if (row.SocialMedia == "GooglePlus") {
    //                $('#postMedia').html("<i class='fa fa-google-plus' aria-hidden='true'></i>");                    
    //            }        

    //            if (data.response.LikesCount != 0) {
    //                var likeName = "";
    //                $("#postlikes").html(data.response.LikesCount);
    //                $.each(data.response.LikesName, function (i, val) {
    //                    debugger;
    //                    likeName = likeName + '<p>' + val + '</p>';
    //                    //$('#likesName').attr('title', '<p>' + likeName + '</p>');   
    //                })
    //                $('#likesName').append(likeName );
    //            }
    //            else if (data.response.linkedInlikes != 0) {
    //                var likeName = "";
    //                $("#postlikes").html(data.response.linkedInlikes._total);
    //                if (data.response.linkedInlikes.values != undefined) {
    //                    $.each(data.response.linkedInlikes.values, function (i, val) {
    //                        debugger;
    //                        likeName = likeName + '<p>' + val.person.firstName + '</p>';
    //                        //$('#likesName').attr('title', '<p>' + likeName + '</p>');   
    //                    })
    //                }                   
    //                $('#likesName').append(likeName);

    //            }
    //            else {
    //                $("#postlikes").html(0);
    //            }

    //            if (data.response.fbcomment != undefined) {
    //                debugger;
    //                $.each(data.response.fbcomment.data, function (i, val) {
    //                    debugger;
    //                    $('#postComments').append('<p>' +'<b>' + val.from.name+'</b>' +':' + val.message + '</p>');
    //                    //$('#postCommentsNames').append('<p>' + + '</p>');
    //                })            
    //                $('#commentsCount').html(data.response.fbcomment.data.length)
    //            }
    //            else if (data.response.linkedincomment != undefined) {
    //                $.each(data.response.linkedincomment.values, function (i, val) {
    //                    debugger;
    //                    $('#postComments').append('<p>' + '<b>' + val.person.firstName + '</b>' + ':' + val.comment + '</p>');
    //                })
    //                $('#commentsCount').html(data.response.linkedincomment.values.length)
    //                //$.each(data.response.CommentsName, function (i, val) {
    //                //    debugger;
    //                //    $('#postCommentsNames').append(val);
    //                //})
    //            }
    //            else {
    //                $("#postComments").html();
    //                $('#commentsCount').html(0);
    //            }


    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {

    //        }

    //    });


    //    $("#viewPostModal").modal("show");



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

var $table = $("#grid");
var chk = false;
function RefreshGrid() {
    document.getElementById("btnFilter").click();
}


$(function () {
    Dashboard.Init();

});

var Dashboard = {
    Init: function () {
        Dashboard.PreInit();
        var arrayIdsrepost = [];
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
            // Click on Post button
            var list = new Array();

        },

        OnChange: function () {
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



            });
        },


    },
    AjaxCalls: {
        PostData: function (PostInformation) {
            debugger;
          
      
                        debugger;
                        $('.loadercontainingdiv').show();
                        $.ajax({
                            url: $_PostDetails,
                            type: "POST",
                            data: { "PostInformation": JSON.stringify(PostInformation) },
                            success: function (result) {
                                debugger;
                             
                                if (result.ErrorModel == "") {
                                   // BootstrapDialog.alert("Error posting on facebook");
                                    var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
                                    BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                                    BootstrapDialog.show({ title: "<span>Information</span>", message: 'Error posting on facebook.' });

                                }
                                else {
                                    var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
                                    BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                                    BootstrapDialog.show({ title: "<span>Information</span>", message: result.ErrorModel });
                                  
                                }
                                if (result.ErrorModel == "<br /> Content posted successfully on LinkedIn.") {
                                    var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
                                    BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                                    BootstrapDialog.show(' Success !');
                                  
                                }
                            },

                        });
                    
                
            
            //$('.loadercontainingdiv').show();
            //$.ajax({
            //    url: $_PostDetails,
            //    type: "POST",
            //    data: { "PostInformation": JSON.stringify(PostInformation) },
            //    success: function (result) {
            //        debugger;
            //        if (result.ErrorModel == "") {
            //            BootstrapDialog.alert("Error posting on facebook");
            //        }
            //        else {
            //            BootstrapDialog.alert(result.ErrorModel);
            //        }
            //        if (result.ErrorModel == "<br /> Content posted successfully on LinkedIn.") {
            //            BootstrapDialog.alert(' Success !');
            //        }
            //    },

            //});
        },
    },
    BindDataMethods: {
        CreateJsonForRePostDetails: function (e, value, row, index) {
            debugger;
            var arr = [];
            if (row.ImageUrl != null) {
                var image_name = row.ImageUrl;
                arr.push(image_name);
            };

            var Ids = [];
            var JsonWorkInfo = {};
            // Get Images
            $('row.ImageUrl').each(function () {
                debugger
                var text = $(this).children().eq(1).children().eq(1).text();
                var str = "*";
                arr.push(str + text);
            });

            selectedPageList = [];
            JsonWorkInfo.selectedPageList = selectedPageList;
            JsonWorkInfo.TextMessage = row.Description;

            JsonWorkInfo.Heading = row.Caption;
            JsonWorkInfo.Title = row.Name;
            JsonWorkInfo.Link = row.Url;
            JsonWorkInfo.PostMethod = "Republish";
            Ids.push(row.SocialMediaProfileId);

            JsonWorkInfo.Ids = Ids;
            JsonWorkInfo.ImageArray = arr;

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
    ApplyValidations: {

    },
    BindMethods: {

    },
}

