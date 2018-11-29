$(document).ready(function () {
    debugger;
    var Param = {};
    var $table = $("#grid");
    var usertype = $("#UserType").val();
    var industryId = $("#IndustryId").val();


    $('#resetBtn').on('click', function () {
        $("#custom-toolbar").find('input').val('');
        $table.bootstrapTable('refresh');
        //$("#resetBtn").attr("disabled", "disabled");
    });

    //$("#custom-toolbar input").on("keyup", function () {
    //    if ($(this).val() != '')
    //        $("#resetBtn").removeAttr("disabled");
    //    else
    //    {

    //    }
    //       // $("#resetBtn").attr("disabled", "disabled");
    //});

    $("#btnFilter").click(function () {

        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var socialMedia = $("#socialMedia").val();
         
        if (firstName != "") {
            Param.firstName = firstName;
        }
        else {
            Param.firstName = "";
        }
        if (lastName != "") {
            Param.lastName = lastName;
        }
        else {
            Param.lastName = "";
        }
        if (socialMedia == "" || socialMedia == "Select Social Media---") {
           // Param.socialMedia = "";
        }
        else {
            Param.socialMedia = socialMedia;
        }
   

        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });


    Param.UserType = usertype;
    Param.IndustryId = industryId;

    var reqUrl = $_BaseUrl + '/Users/api/AdminApi/GetPostList';

    $('#grid').bootstrapTable({
        headers: headers,
        method: 'post',
        url: reqUrl,
        cache: true,
        height: 700,
        classes: 'table table-hover',
        customParams: Param,
        striped: true,
        pageNumber: 1,
        pagination: true,
        pageSize: 10,
        pageList: [5, 10, 20, 30],
        search: false,
        showColumns: false,
        //showRefresh: true,
        sidePagination: 'server',
        minimumCountColumns: 2,
        showHeader: true,
        showFilter: true,
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
            },
                 {
                     field: 'SocialMedia',
                     title: 'Social Media',
                     sortable: true,
                     clickToSelect: false,
                     formatter: function (value, row, index) {
                         if (row.SocialMedia == "Facebook") {
                             return ["<span class='social-icon-tb'><i class='fa fa-facebook'></i></span>"].join('');
                         }
                         else if (row.SocialMedia == "LinkedIn") {
                             return ["<span class='social-icon-tb'><i class='fa fa-linkedin' aria-hidden='true'></i></span>"].join('');
                         }
                         else if (row.SocialMedia == "Twitter") {
                             return ["<span class='social-icon-tb'><i class='fa fa-twitter' aria-hidden='true'></i></span>"].join('');
                         }
                         else if (row.SocialMedia == "Google") {
                             return ["<span class='social-icon-tb'><i class='fa fa-google-plus' aria-hidden='true'></i></span>"].join('');
                         }
                     },
                 },

                 //{
                 //    field: 'SocialMedia , FirstName, LastName, ImageUrl,Description,PostDate',
                 //    title: 'Posts',
                 //    checkbox: false,
                 //    type: 'search',
                 //    sortable: true,
                 //    formatter: function (value, row, index) {
                 //        var desc = row.Description;
                 //        if (desc == null) {
                 //            desc = row.Caption;
                 //        }
                 //        var date = row.PostDate;
                 //        var socialMedia = row.SocialMedia;
                 //        var element = "";
                 //        if (socialMedia == "Facebook") {
                 //            element = "<i class='fa fa-facebook'></i>";
                 //        }
                 //        else if (socialMedia == "Twitter") {
                 //            element = "<i class='fa fa-twitter'></i>";
                 //        }
                 //        else if (socialMedia == "LinkedIn") {
                 //            element = "<i class='fa fa-linkedin'></i>";
                 //        }
                 //        else if (socialMedia == "GooglePlus") {
                 //            element = " <i class='fa fa-google-plus'></i>";
                 //        }

                 //        var date = row.PostDate.split("T");
                 //        var newdate = date[1].split(":");

                 //        if (row.ImageUrl != null) {
                 //            var siteUrl = $_BaseUrl;
                 //            var image = siteUrl;
                 //            var imagePath = row.ImageUrl.trim();
                 //            image = image + imagePath;
                 //            return ["<div class='post-card'><div class='post-title linkedin-bg'>"
                 //                + "<div class='user-detail'><h4>" + row.FirstName + "&nbsp" + row.LastName + "</h4>"
                 //                + "<span class='social-icon'>" + element + "</span>"
                 //               + "<p>Posted On: " + date[0] + " " + newdate[0] + ":" + newdate[1] + "</p></div></div>"
                 //             + "<div class='post-content clearfix'><div class='image-content'><div class='table-box'><div class='table-box-cell height-match'><img src=" + image.trim() + "></div></div></div>"
                 //                + "<div class='text-content'><p>" + desc + "</p></div></div>"]
                 //        }
                 //        else {
                 //            //return ["<div style='float:left'>" + row.SocialMedia + "</div>", "<div>" + row.FirstName + "&nbsp" + row.LastName + "</div>", "<div> No Image </div>", "<div style='float:right'>" + row.Description + "</div>"].join('');
                 //            return ["<div class='post-card'><div class='post-title linkedin-bg'>"
                 //               + "<div class='user-detail'><h4>" + row.FirstName + "&nbsp" + row.LastName + "</h4>"
                 //               + "<span class='social-icon'>" + element + "</span>"
                 //               + "<p>Posted On: " + date[0] + " " + newdate[0] + ":" + newdate[1] + "</p></div></div>"
                 //                + "<div class='post-content'><div class='image-content'><div class='table-box'><div class='table-cell'></div></div></div>"
                 //                + "<div class='text-content no-img-lft'><p>" + desc + "</p></div></div>"]
                 //        }
                 //    }
                 //}
                 {
                     field: 'FirstName',
                     title: 'First Name',
                     checkbox: false,
                     type: 'search',
                     sortable: true,
                 }, {
                     field: 'LastName',
                     title: 'Last Name',
                     checkbox: false,
                     type: 'search',
                     sortable: true,
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
                        if (fulltext == null || fulltext == undefined || fulltext == "") {
                            fulltext = row.Description;
                        }
                        debugger
                        if (fulltext.length >= 30) {
                            var shorttext = fulltext.substring(0, 23);
                            return [shorttext + '...', '<button id="read" title="Read More"><i class="fa fa-book"></i></button>'].join('');
                        }
                        else {
                            // return [row.Description].join('');
                            return row.Name + '...' + '<button id="read" title="Read More"><i class="fa fa-book"></i></button>';
                        }
                        return shorttext;
                    },

                }, {
                     field: 'ImageUrl',
                     title: 'Image',
                     type: 'search',
                     // sortable: true,
                     formatter: function (value, row, index) {
                         var siteUrl = $_BaseUrl;

                         var parsedURL = UrlParser(row.ImageUrl)
                         if (row.ImageUrl != null && row.ImageUrl.indexOf("undefine") < 0) {
                         if (parsedURL.origin == undefined || parsedURL.origin == null) {
                             var url = $_BaseUrl + row.ImageUrl;

                         }
                         else {
                             var url = row.ImageUrl;
                         }
                             return ["<img src='" + url + "' height='40px' width='40px'/>"].join('');
                         }
                         else {
                             return ["<img src='" + url + "/Images/noimage.png" + "' height='40px' width='40px'/>"].join('');
                         }
                     },
                 },
        
                {
                     field: 'PostDate',
                     title: 'Post Date',
                     clickToSelect: false,
                     formatter: function (value, row, index) {
                         debugger
                         //Get local time from UTC
                        // var localTime = moment.utc(row.PostDate).toDate();
                         //localTime = moment(localTime).format('YYYY-MM-DD HH:mm:ss');
                        // localTime = moment(localTime).format('YYYY-MM-DD');
                         // return [localTime].join('');
                         //var localDate = new Date(value);
                         //return localDate.toString().replace(/GMT.*/g, "");

                         var localdate = moment.utc(value).toDate();
                         return localdate.toString().replace(/GMT.*/g, "");

                     },
                     events: operateEvents
                 },
                  {
                      field: 'CreatedByName',
                      title: 'Posted By',
                      checkbox: false,
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
                              return[" User "]
                          }
                          else if (row.RoleType == 2) {
                              return [" Admin "]
                          }
                          else if (row.RoleType == 3) {
                              return [" Super Admin "]
                          }
                      }
                  },

                 //    {
                 //    field: 'operate',
                 //    title: 'Actions',
                 //    clickToSelect: false,
                 //    formatter: operateFormatter,
                 //    events: operateEvents
                 //    },
                 //    {
                 //    field: 'checkbox',
                 //    title: 'Status',
                 //    checkbox: false,
                 //    clickToSelect: true,
                 //    formatter: operateStatus,
                 //    events: operateEvents
                 //}
        ],
        onLoadSuccess: function () {
            Addtitle();
            // $('.btnAdd').hide();
        },
        onPageChange: function () {
            Addtitle();
            // $('.btnAdd').hide();
        },

    });
});

function operateFormatter(value, row, index) {

    return [
       // '<button id="edit" class="btn btn-link"  title="Edit">',
       //    '<i class="fa fa-trash"></i> Edit',
       //'</button>',
        '<button id="delete" class="btn btn-link"  title="Remove">',
            '<i class="fa fa-trash"></i> Delete',
        '</button>'

    ].join('');

}

function operateStatus(value, row, index) {
    if (row.IsActive == true) {
        return [
           '<button id="activate" class="btn btn-link"  title="deactivate">',
                    '<i class="fa fa-trash"></i> Active',
                '</button>'

        ].join('');
    }
    else {
        return [
                   '<button id="deactivate" class="btn btn-link"  title="Activate">',
                            '<i class="fa fa-trash"></i> Deactive',
                        '</button>'

        ].join('');
    }
}

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
        if (row.Name != null) {

            var Name = [row.Name].join('');
        }
        else {
            var Name = "";
        }

        //BootstrapDialog.alert('<strong>Heading:</strong> '+
        //   haeading + '</br></br><strong>Title:</strong> ' +
        //    row.Name
        //    );



        BootstrapDialog.alert(' <strong>Heading:</strong> ' +
         haeading + '</br>   </br> <strong>Title:</strong> ' +
          Name + '</br>   </br> <strong>Description:</strong> ' +
          row.Description + '</br>   </br> <strong>Link:</strong> ' +
          URL
          );

        /// BootstrapDialog.alert(row.Description);

    },

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

var $table = $("#grid");
var chk = false;

function RefreshGrid() {
    document.getElementById("btnFilter").click();
}