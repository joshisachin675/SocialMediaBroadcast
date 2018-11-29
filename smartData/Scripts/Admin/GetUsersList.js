$(document).ready(function () {
    debugger;
    var Param = {};
    var $table = $("#grid");
    Param.Roles = 1;
    

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

        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var emailId = $("#emailId").val();
        var role = $('#Roles').val();

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
        if (emailId != "") {
            Param.emailId = emailId;
        }
        else {
            Param.emailId = "";
        }
        Param.Roles = role;

        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

    var reqUrl = $_BaseUrl + '/Users/api/AdminApi/GetEndUsersList';

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
                field: 'UserId',
                title: '#',
                checkbox: false,
                type: 'search',
                sortable: true,
                visible: false,
                switchable: false,
            },
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
                     field: 'IndustryName',
                     title: 'Industry',
                     checkbox: false,
                     type: 'search',
                     sortable: true,
                 }, {
                     field: 'Email',
                     title: 'Email',
                     type: 'search',
                     sortable: true,
                     formatter: function (value, row, index) {
                         debugger;
                         if (row.Email == "") {
                             return ["<b>Authentication Pending</b>"].join('');
                         }
                         else {
                             if ($('#hdnusertype').val() == 2) {
                                 var url = $_BaseUrl + '/Admin/ManageAdmin/LoginAdmin?email=' + row.Email + '&password=' + row.Password + '&industryId=' + row.IndustryId;
                                 return ["<a href=" + url + " class='text-info'>" + row.Email + "</a>"]
                             }
                             else {
                                 return [row.Email].join('');
                             }
                         }
                     },
                 },  {
                     field: 'UserType',
                     title: 'Role',
                     checkbox: false,
                     type: 'search',
                     sortable: true,
                     formatter: function (value, row, index) {
                         if (row.UserType == 1) {
                             return ["User"].join('');
                         }
                         else {
                             return ["Admin"].join('');
                         }
                     },
                 }, {
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
        queryParams: function (p) {
            p.userType = $("#hdnusertype").val();
            p.UserType = $("#hdnusertype").val();
            p.industryId = $('#hdnIndustryId').val();
            return p;
        },
        onLoadSuccess: function () {
            Addtitle();
            $('.btnAdd').hide();
        },
        onPageChange: function () {
            Addtitle();
            $('.btnAdd').hide();
        },

    });

    $('#addStaffbtn').click(function () {
        debugger;
        $('#AddAdmin')[0].reset();
        $('.field-validation-valid').html('')
    })
});


$('#addStaffbtn').click(function () {
    debugger;
    $('#AddAdmin')[0].reset();
    $('.field-validation-valid').html('')
})

function operateFormatter(value, row, index) {
    if (row.Active == true) {
        return [
            '<button id="delete" class="btn btn-link" title="Remove">',
            '<i class="fa fa-trash"></i>',
        '</button>',
           '<button id="activate" class="btn btn-link"  title="Deactivate">',
                    '<i class="fa fa-unlock"></i>',
                '</button>'

        ].join('');
    }
    else {
        return [
            '<button id="delete" class="btn btn-link" title="Delete">',
            '<i class="fa fa-trash"></i>',
        '</button>',
                   '<button id="deactivate" class="btn btn-link" title="Activate">',
                            '<i class="fa fa-lock"></i>',
                        '</button>'

        ].join('');
    }
}

function operateStatus(value, row, index) {
    

}

window.operateEvents = {
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
                        url: $_BaseUrl + "/Admin/ManageUser/DeleteUserAccount",
                        data: { id: row.UserId },
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

    'click #activate': function (e, value, row, index) {
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
                        url: $_BaseUrl + "/Admin/ManageUser/UpdateUserStatusDeactive/" + row.UserId,
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

    'click #deactivate': function (e, value, row, index) {
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
                        url: $_BaseUrl + "/Admin/ManageUser/UpdateUserStatusActive/" + row.UserId,
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

var $table = $("#grid");
var chk = false;

function RefreshGrid() {
    document.getElementById("btnFilter").click();
}