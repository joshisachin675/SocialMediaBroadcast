$(document).ready(function () {
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
        debugger;
        var categoryName = $("#category").val();

        if (categoryName != "") {
            Param.category = categoryName;
        }
        else {
            Param.category = "";
        }

        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

    var reqUrl = $_BaseUrl + '/Users/api/AdminApi/GetCategories';
   
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
                field: 'IndustryId',
                title: '#',
                checkbox: false,
                type: 'search',
                sortable: true,
                visible: false,
                switchable: false,
            },
                {
                    field: 'IndustryName',
                    title: 'Industry',
                    checkbox: false,
                    type: 'search',
                    sortable: true,
                },
                {
                    field: 'IndustryShortName',
                    title: 'Short Name',
                    checkbox: false,
                    type: 'search',
                    sortable: true,
                }, {
                    field: 'operate',
                    title: 'Actions',
                    clickToSelect: false,
                    formatter: operateFormatter,
                    events: operateEvents
                }
                    //{
                //    field: 'checkbox',
                //    title: 'Status',
                //    checkbox: false,
                //    clickToSelect: true,
                //   // formatter: operateStatus,
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
});

function operateFormatter(value, row, index) {
    debugger;
    if ($('#hdnUserType').val() == 3)
    {
        return [
       '<button class="edit btn btn-link" title="Edit">',
   '<i class="fa fa-pencil-square-o"></i> Edit',
'</button>',
       '<button id="delete" class="delete btn btn-link"  title="Remove">',
           '<i class="fa fa-trash"></i> Delete',
       '</button>',
       '<button  class="addSubIndustry btn btn-link"  title="Add SubIndustry">',
           '<i class="fa fa-plus"></i> Add SubIndustry',
       '</button>'
        ].join('');
    }
    else {
        if (row.IndustryId == $('#hdnIndustryId').val())
        {
            return [
               '<button  class="addSubIndustry btn btn-link"  title="Add SubIndustry">',
                   '<i class="fa fa-plus"></i> Add SubIndustry',
               '</button>'
            ].join('');
        }
    }
   
}

function operateStatus(value, row, index) {
    if (row.IsActive == true) {
        return [
           '<button id="activate" class="activate btn btn-link"  title="deactivate">',
                    '<i class="fa fa-trash"></i> Active',
                '</button>'

        ].join('');
    }
    else {
        return [
                   '<button id="deactivate" class="deactivate btn btn-link"  title="Activate">',
                            '<i class="fa fa-trash"></i> Deactive',
                        '</button>'

        ].join('');
    }
}




window.operateEvents = {
    'click .edit': function (e, value, row, index) {
        $('.loadercontainingdiv').show();
        var modal = $("#editCategory");
        $("#IndustryName", modal).val(row.IndustryName);
        $('#IndustryId', modal).val(row.IndustryId);
        $('#IndustryShortName', modal).val(row.IndustryShortName);
        modal.modal("show");
        $('.loadercontainingdiv').hide();
    },

    'click .delete': function (e, value, row, index) {
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
                    $.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        //contentType: "application/json; charset=utf-8",
                        url: $_BaseUrl + "/Admin/ManageCategory/DeleteCategory",
                        data: { id: row.IndustryId },
                        success: function (data) {
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

    

    'click .addSubIndustry': function (e, value, row, index) {
        debugger;
        $('#SubIndustryName').val("");
        $('.loadercontainingdiv').show();
        var modal = $("#addSubIndustry");
        $('#IndustryId', modal).val(row.IndustryId);
        modal.modal("show");
       //$('.modal').css('display', 'block');
       $('.loadercontainingdiv').hide();
       
    },


    'click .view': function (e, value, row, index) {
        debugger;
        $('.loadercontainingdiv').show();
        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: $_BaseUrl + "/Admin/ManageCategory/GetSubCategoryList?industryId=" + row.IndustryId,
            success: function (result) {
                debugger;
                $('.loadercontainingdiv').hide();               
                //var modals = $("#viewSubIndustrymodal");
                //modals.modal("show").css('display', 'block');
                $("#viewSubIndustrymodal .modal").html(result)
                $("#viewSubIndustrymodal").modal();
                //$("#viewSubIndustrymodal").html(result);
                //$('.modal').css('display', 'block');

                //if (data.status == true) {
                //    RefreshGrid();
                //    dialogItself.close();
                //}
                //else {
                //    ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.');
                //}
            },
            error: function (request, error) {
                //if (request.statusText == "CustomMessage") {
                //    $("#spanError").html(request.responseText)
                //}
            }            
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
                // $("#addStaff").modal('hide');
            }
        }]
    });
}

//function AddCategorySuccess(data) {
//    $('#addCategory').modal('hide');
//}


var $table = $("#grid");
var chk = false;

function RefreshGrid() {
    document.getElementById("btnFilter").click();
}