$(document).ready(function () {
    debugger;
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

        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var emailId = $("#emailId").val();

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

        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });   

    var reqUrl = $_BaseUrl + '/Users/api/AdminApi/GetUsersList';

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
                }, {
                    field: 'Email',
                    title: 'Email',
                    type: 'search',
                    sortable: true,
                    formatter: function (value, row, index) {
                        if (row.Email == "") {
                            return ["<b>Authentication Pending</b>"].join('');
                        }
                        else {
                            return [row.Email].join('');
                        }
                    },
                }, {
                    field: 'operate',
                    title: 'Actions',
                    clickToSelect: false,
                    formatter: operateFormatter,
                    events: operateEvents
                }, {
                    field: 'checkbox',
                    title: 'Status',
                    checkbox: false,
                    clickToSelect: true,
                    formatter: operateStatus,
                    events: operateEvents
                }],
        onLoadSuccess: function () {
            Addtitle();
        },
        onPageChange: function () {
            Addtitle();
        },

    });

   
});


$('#addStaffbtn').click(function () {
    debugger;
    $('#AddAdmin')[0].reset();
})

function operateFormatter(value, row, index) {
    return [
        '<button id="delete" class="btn btn-link"  title="Remove">',
            '<i class="fa fa-trash"></i> Delete',
        '</button>'
    ].join('');
}

function operateStatus(value, row, index) {
    if (row.Active == true) {
        return [
           '<button id="activate" class="btn btn-link"  title="deactivate">',
                    '<i class="fa fa-unlock"></i>',
                '</button>'

        ].join('');
    }
    else {
        return [
                   '<button id="deactivate" class="btn btn-link"  title="Activate">',
                            '<i class="fa fa-lock"></i>',
                        '</button>'

        ].join('');
    }

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
                    $.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        //contentType: "application/json; charset=utf-8",
                        url: $_BaseUrl + "/Admin/ManageAdmin/DeleteAdminAccount",
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
                        url: $_BaseUrl + "/Admin/ManageAdmin/UpdateAdminStatusDeactive/" + row.UserId,
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
                        url: $_BaseUrl + "/Admin/ManageAdmin/UpdateAdminStatusActive/" + row.UserId,
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