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

        var Name = $("#Name").val();
        var Role = $("#Role").val();
        var emailId = $("#Email").val();

        if (Name != "") {
            Param.Name = Name;
        }
        else {
            Param.Name = "";
        }

        if (emailId != "") {
            Param.Email = emailId;
        }
        else {
            Param.Email = "";
        }

        Param.Role = Role;
        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

    var reqUrl = $_BaseUrl + '/Users/api/AdminApi/GetActivityList';
    var name = "";
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
                field: 'ActivityId',
                title: '#',
                checkbox: false,
                type: 'search',
                sortable: true,
                visible: false,
                switchable: false,
            },
                {
                    field: 'Name',
                    title: 'Name',
                    checkbox: false,
                    type: 'search',
                    sortable: true,
                }, {
                    field: 'Email',
                    title: 'Email',
                    type: 'search',
                    sortable: true,
                }, {
                    field: 'Role',
                    title: 'User Type',
                    type: 'search',
                    sortable: true,
                    formatter: function (value, row, index) {
                        if (value == 1) {
                            return ["User"].join('');
                        }
                        else if (value == 2) {
                            return ["Admin"].join('');
                        }

                    },
                }, {
                    field: 'IpAddress',
                    title: 'IP Address',
                    type: 'search',
                    sortable: false,
                }, {
                    field: 'AreaAccessed',
                    title: 'Path',
                    type: 'search',
                    sortable: false,
                }, {
                    field: 'Event',
                    title: 'Event',
                    type: 'search',
                    sortable: false,
                }, {
                    field: 'Message',
                    title: 'Message',
                    type: 'search',
                    sortable: false,
                },
        {
            field: 'TimeStamp',
            title: 'Timestamp',
            type: 'search',
            sortable: true,
            formatter: function (value, row, index) {
                var localDate = new Date(value);
                return localDate.toString().replace(/GMT.*/g, "");

            },
        },
             //{
             //    field: 'CreatedByEmail',
             //    title: 'Done By',
             //    type: 'search',
             //    sortable: false,
             //}
        ],
        queryParams: function (p) {
            p.userType = $("#hdnusertype").val();
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
});

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