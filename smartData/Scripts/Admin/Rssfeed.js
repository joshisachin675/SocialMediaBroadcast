
$(document).ready(function () {
    debugger;
    var Param = {};
    Param.UserType = $("#UserType").val();
   
    var $table = $("#grid");
    $("#btnFilter").click(function () {
        debugger;
        if (categoryname.length != "") {
            Param.categoryname = categoryname;
        }
        else {
            Param.categoryname = "";
        }
        $("#grid").show();
        $(".fixed-table-pagination").hide();;
        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });
    $("#txtSearchFeeds").keyup(function () {
        debugger
      
        $table.bootstrapTable('refresh');
    });
    var feedurl = $_BaseUrl + $_ReadRssFeedAction;
    var url = $_BaseUrl + '/Admin/ManageRssFeeds/RssRead?='
    var reqUrl = $_BaseUrl + '/Users/api/AdminApi/GetRssFeed';
    $('#grid').bootstrapTable({
        headers: headers,
        method: 'post',
        url: reqUrl,
        cache: true,
        height: 700,
        classes: 'table table-hover',
        customParams: Param,
        queryParams: function (Param) {
            Param.search = $("#txtSearchFeeds").val();;
            return Param;
        },
        striped: true,
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
        //rowStyle: rowStyle,
        toolbar: '#custom-toolbar',
        //queryParams: function (p) {
        //    return { tags: ta }
        //},
        columns: [
            {
                field: 'FeedId',
                title: '#',
                checkbox: false,
                type: 'search',
                sortable: true,
                visible: false,
                switchable: false,
            },
             {
                 field: 'UserId',
                 title: '#',
                 checkbox: false,
                 type: 'search',
                 sortable: true,
                 visible: false,
                 switchable: false,
             },

                 {
                     field: 'FeedName',
                     title: 'Feed Name',
                     checkbox: false,
                     type: 'search',
                     sortable: true,
                 },
                 {
                     field: 'FeedUrl,FeedId',
                     title: 'Feed Url',
                     checkbox: false,
                     type: 'search',
                     sortable: true,
                     formatter: function (value, row, index) {
                         debugger;
                         var url = '';
                         var ur = row.FeedUrl.trim();
                         var id = row.FeedId;
                         url = $_BaseUrl + '/Admin/ManageRssFeeds/RssRead?FeedUrl=' + id;
                         if ($("#UserType").val() == 3) {
                             return ["<a href=" + url + " class='text-info'>" + row.FeedUrl + "</a>"].join('');
                         }
                         else {
                             if (row.IsApproved == true) {
                                 return ["<a href=" + url + " class='text-info'>" + row.FeedUrl + "</a>"].join('');
                             }
                             else {
                                 return [row.FeedUrl];
                             }
                         }                                           
                     },
                 },
                  {
                      field: 'CreatedBy',
                      title: 'Added By',
                      checkbox: false,
                      type: 'search',
                      sortable: true,
                  },
                  {
                      field: 'UserType',
                      title: 'Role',
                      checkbox: false,
                      type: 'search',
                      sortable: true,
                  },
                       {
                           field: 'DateProcess',
                           title: 'Date Process',
                           checkbox: false,
                           type: 'search',
                           sortable: true,
                           formatter: function (value, row, index) {
                               debugger

                               if (row.DateProcess !=null) {
                                   var localdate = moment.utc(row.DateProcess).toDate();
                                   return localdate.toString().replace(/GMT.*/g, "");
                               }
                              

                           }
                       },
                 {
                     field: 'operate',
                     title: 'Actions',
                     clickToSelect: false,
                     formatter: operateFormatter,
                     events: operateEvents
                 },
                 //{
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
            $('.btnAdd').hide();
        },
        onPageChange: function () {
            Addtitle();
            $('.btnAdd').hide();
        },
    });
})
function operateFormatter(value, row, index) {
    if ($("#UserType").val() == 3) {
        if (row.IsApproved == false) {
            return [
               '<button id="delete" class="delete btn btn-link"  title="Remove">',
                   '<i class="fa fa-trash"></i>',
               '</button>',
               '<button id="Aprrove" class="aprrove btn btn-link"  title="Aprrove">',
                           ' Aprrove',
                       '</button>'
            ].join('');
        }
        else {
            return [
                '<button id="delete" class="delete btn btn-link"  title="Remove">',
                    '<i class="fa fa-trash"></i>',
                '</button>',
                   //'<button id="Aprrove" class="btn btn-link activate-btn"  title="Aprrove">',
                   //        ' Aprrove',
                   //    '</button>'
            ].join('');
        }
    }
    else
    {
        if (row.IsApproved == false) {
            return [
               '<button id="delete" class="delete btn btn-link"  title="Remove">',
                   '<i class="fa fa-trash"></i>',
               '</button>',
               '<button id="Aprrove" class=""  title="Aprrove">',
                           'Pending Approval',
                       '</button>'
            ].join('');
        }
        else {
            return [
                '<button id="delete" class="delete btn btn-link"  title="Remove">',
                    '<i class="fa fa-trash"></i>',
                '</button>',
                   //'<button id="Aprrove" class="btn btn-link activate-btn"  title="Aprrove">',
                   //        ' Aprrove',
                   //    '</button>'
            ].join('');
        }
    }
    

}

//function operateStatus(value, row, index) {
//    if (row.IsActive == true) {
//        return [
//           '<button id="activate" class="btn btn-link activate-btn"  title="deactivate">',
//                    ' Active',
//                '</button>'

//        ].join('');
//    }
//    else {
//        return [
//                   '<button id="deactivate" class="btn btn-link deactivate-btn"  title="Activate">',
//                            'Deactive',
//                        '</button>'

//        ].join('');
//    }
//}


window.operateEvents = {
    'click .edit': function (e, value, row, index) {
        $('.loadercontainingdiv').show();
        var modal = $("#editCategory");
        $("#CategoryName", modal).val(row.CategoryName);
        $('#CategoryId', modal).val(row.CategoryId);
        modal.modal("show");
        $('.loadercontainingdiv').hide();
    },

    'click .aprrove': function (e, value, row, index) {
        BootstrapDialog.show({
            title: 'Confirmation',
            message: 'Are you sure you want to approve this Feed?',
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
                    $.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        //contentType: "application/json; charset=utf-8",
                        url: $_BaseUrl + "/Admin/ManageRssFeeds/ApproveRssFeed",
                        data: { FeedId: row.FeedId },
                        success: function (data) {
                            if (data.status == true) {
                                $("#grid").bootstrapTable('refresh');
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



    'click .delete': function (e, value, row, index) {
        debugger;
        BootstrapDialog.show({
            title: 'Confirmation',
            message: 'Are you sure you want to delete this Feed?',
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
                    $.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        //contentType: "application/json; charset=utf-8",
                        url: $_BaseUrl + "/Admin/ManageRssFeeds/DeleteRssFeed",
                        data: { FeedId: row.FeedId },
                        success: function (data) {
                            if (data.status == true) {
                                $("#grid").bootstrapTable('refresh');
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

    'click .activate': function (e, value, row, index) {
        debugger;
        var status = false;
        BootstrapDialog.show({
            title: 'Confirmation',
            message: 'Are you sure you want to deactivate this record?',
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
                    $.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        url: $_BaseUrl + "/Admin/ManageCategory/UpdateCategoryStatusDeactive/" + row.CategoryId,
                        success: function (data) {
                            debugger
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

    'click .deactivate': function (e, value, row, index) {
        // debugger;
        var status = false;
        BootstrapDialog.show({
            title: 'Confirmation',
            message: 'Are you sure you want to activate this record?',
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
                    $.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        url: $_BaseUrl + "/Admin/ManageCategory/UpdateCategoryStatusActive/" + row.CategoryId,
                        success: function (data) {
                            debugger
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

    }


};

function Addtitle() {
    $('.page-next').attr('Title', 'Next');
    $('.page-first').attr('Title', 'First');
    $('.page-last').attr('Title', 'Last');
    $('.page-pre').attr('Title', 'Previous');
}
