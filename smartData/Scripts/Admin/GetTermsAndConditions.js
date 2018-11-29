
$(document).ready(function () {
   

    var Param = {};
    var $table = $("#tblTermsAndConditions");





    ///// Bind Industry 

    var reqUrl = $_BaseUrl + '/Users/api/HomeApi/GetIndustryNameTermsAndCondition';

    var userIds = parseInt($('#hdnUserId').val())
    var userId = {}
    userId.userID = userIds;
    $.ajax({
        type: "POST",
        url: reqUrl,
        data: {},

        //  contentType: "application/json; charset=utf-8",
        // dataType: "json",
        success: function (data) {
            debugger
            $.each(data, function (k, v) {
                var htm = '';
                htm += '<option value = "' + v.IndustryId + '" >' + v.IndustryName + '</option>';
                $('#TermsAndConditonID').append(htm);
               

            });
        },
        failure: function () {
            alert("Failed!");
        }
    });

    ///// Bind Industry 

   
    $("#btnFilter").click(function () {

        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var emailId = $("#emailId").val();
        var role = $('#Roles').val();

        Param.Roles = role;

        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

  

    var reqUrl = $_BaseUrl + '/Users/api/AdminApi/GetTermsandCondition';

    $('#tblTermsAndConditions').bootstrapTable({
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
        showColumns: true,
        // showRefresh: true,
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
                field: 'id',
                title: '#',
                checkbox: false,
                type: 'search',
                sortable: true,
                visible: false,
                switchable: false,
            },
             //{
             //    field: 'TermsConditions',
             //    title: 'Terms and  Conditions',
             //    sortable: true,
             //    clickToSelect: false,
               
                
             //}, 

  {
      field: 'labelandtittle',
      title: 'Title',
      sortable: true,
  //    clickToSelect: false,


  },
             {
                 field: 'cratedDate',
                 title: 'Created Date',
                 sortable: true,                
                 //clickToSelect: false,              
                 formatter: function (value, row, index) {
                  
                     var SaveDateTime = moment.utc(value).local().format("L LT");
                     SaveDateTime = moment(SaveDateTime).format('MM/DD/YYYY')
                     return SaveDateTime
                 }
               
             }, 
           {
               field: 'IndustryName',
               title: 'Industry Name',
               sortable: true,
               clickToSelect: false,
               formatter: function (value, row, index) {


                 
                   if (value==null || value=="") {
                       return "All Industries";
                   }
                   else {
                       return value;
                   }
               }
             
           },
        
           {
                 field: 'operate',
                 title: 'Actions',
                 clickToSelect: false,
                // width: '200',
                 formatter: operateFormatter,
                 events: operateEvents
             },

        ],
        onLoadSuccess: function () {
            Addtitle();
            $('.columns.columns-right.btn-group.pull-right').css("margin-top", '56px');
        },
        onPageChange: function () {
            Addtitle();
            $('.columns.columns-right.btn-group.pull-right').css("margin-top", '56px');
            //$('.columns.columns-right.btn-group.pull-right').hide();
        },
    });

  
});

function operateFormatter(value, row, index) {
    debugger;
    if (row.isActive == false) {
        return [
            '<button id="editterms" class="btn btn-link" title="Edit">',
            '<i class="fa fa-pencil-square-o"></i>',
        '</button>',
        '<button id="delete" class="btn btn-link" title="Activate">',
            '<i class="fa fa-trash"></i>',
        '</button>',
           '<button id="activate" class="btn btn-link" title="Activate">',
                    '<i class="fa fa-lock"></i>',
                '</button>',


        ].join('');
    }
    else {
        return [
              '<button id="editterms" class="btn btn-link" title="Edit">',
            '<i class="fa fa-pencil-square-o"></i>',
        '</button>',
        '<button id="delete" class="btn btn-link" title="Deactivate">',
            '<i class="fa fa-trash"></i>',
        '</button>',
                   '<button id="deactivate" class="btn btn-link" title="Deactivate">',
                            '<i class="fa fa-unlock"></i>',
                        '</button>',
        ].join('');
    }
}

window.operateEvents = {


    'click #editterms':function(e,value,row,index)
    {
 $('.loadercontainingdiv').show();
 var modal = $("#editTermsandCondition");
 $("#TermsAndConditonIDEdit", modal).val(row.id_Industry);
 $('#LabelOrTittleTermsEdit', modal).val(row.labelandtittle)
 $('#TermsAndConditionsEdit').html(tinymce.get('TermsAndConditionsEdit').setContent(row.TermsConditions))


 $('#selectedTEremsandConditionID').val(row.id);
 //tinyMCE.activeEditor.setContent(row.TermsConditions);
        modal.modal("show");
        $('.loadercontainingdiv').hide();
    },

    'click #delete': function (e, value, row, index) {
        debugger;
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
                    debugger;
                    $.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        //contentType: "application/json; charset=utf-8",
                        url: $_BaseUrl + "/Admin/ManageContent/DeleteTermsandCondition",
                        data: { id: row.id },
                        success: function (data) {
                            if (data.status == true) {
                                RefreshGrid();
                                dialogItself.close();
                                ShowMessage(BootstrapDialog.TYPE_DANGER, 'Deleted Successfully.');
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

    'click #activate': function (e, value, row, index) {
        debugger;
        var status = false;
        BootstrapDialog.show({
            title: 'Confirmation',
            message: 'Are you sure you want to Activate this record?',
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
                        url: $_BaseUrl + "/Admin/ManageContent/UpdateTermsandConditionactive/" ,
                        data: { id: row.id ,id_Industry: row.id_Industry},
                        success: function (data) {
                            debugger
                            if (data.status == true) {
                                RefreshGrid();
                                dialogItself.close();
                                ShowMessage(BootstrapDialog.TYPE_DANGER, 'Activated Successfully.');
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

    'click #deactivate': function (e, value, row, index) {
        debugger;
        var status = false;
        BootstrapDialog.show({
            title: 'Confirmation',
            message: 'Are you sure you want to Deactivate this record?',
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
                        url: $_BaseUrl + "/Admin/ManageContent/UpdateTermsandConditionDeactive/" + row.id,
                        success: function (data) {
                            debugger
                            if (data.status == true) {
                                RefreshGrid();
                                $table.bootstrapTable('refresh')
                                ShowMessage(BootstrapDialog.TYPE_DANGER, 'Deactivate Successfully.');
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

    var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
    BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
    BootstrapDialog.show({
        title: $messageData,
        type: type,
        message: message,
    });




}
var $table = $("#tblTermsAndConditions");
var chk = false;

function RefreshGrid() {
    $table.bootstrapTable('refresh');
    document.getElementById("btnFilter").click();
    $table.bootstrapTable('refresh', {
        query: { pageSize: 6 }
    });
}

var charEdit = 0;
;



