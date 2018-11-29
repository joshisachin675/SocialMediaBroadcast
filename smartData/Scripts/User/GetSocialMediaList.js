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
        //var firstName = $("#firstName").val();
        //var lastName = $("#lastName").val();
        //var emailId = $("#emailId").val();
        //// var phoneNumber = $("#phoneNumber").val();
        //if (firstName != "") {
        //    Param.firstName = firstName;
        //}
        //else {
        //    Param.firstName = "";
        //}
        //if (lastName != "") {
        //    Param.lastName = lastName;
        //}
        //else {
        //    Param.lastName = "";
        //}
        //if (emailId != "") {
        //    Param.emailId = emailId;
        //}
        //else {
        //    Param.emailId = "";
        //}
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
    var reqUrl = $_BaseUrl + '/Users/api/HomeApi/GetAllSocialMedia';
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
				width: '100',
                formatter: function (value, row, index) {
                    //return ["<img src='" + row.Photo + "' height='40px' width='40px'/>"].join('');
                    if (row.SocialMedia == "Facebook") {
                        return ["  <a href='' id = 'facebookProfile' target='blank'><i class='fa fa-facebook'></i></a>"].join('');
                    }
                    else if (row.SocialMedia == "LinkedIn") {
                        return [" <a href='' id = 'linkedinProfile' target='blank'></a><i class='fa fa-linkedin' aria-hidden='true'></i></a>"].join('');
                    }
                    else if (row.SocialMedia == "Twitter") {
                        return ["  <a href='' id = 'twitterProfile' target='blank' ><i class='fa fa-twitter' aria-hidden='true'></i></a>"].join('');
                    }
                    else if (row.SocialMedia == "GooglePlus") {
                        return ["<i class='fa fa-google-plus' aria-hidden='true'></i>"].join('');
                    }
                },
                events: operateEvents
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
                        debugger
                        if (row.Email == "") {
                            return ["<b>Authentication Pending</b>"].join('');
                        }
                        else {
                            return [row.Email].join('');
                        }
                    },
                }, {
                    field: 'Photo',
                    title: 'Photo',
                    type: 'search',
					width: '10%',
                    sortable: false,
                    formatter: function (value, row, index) {
                        return ["<img src='" + row.Photo + "' height='40px' width='40px'/>"].join('');
                    }
                }, {
                    field: 'operate',
                    title: 'Actions',
                    clickToSelect: false,
                    formatter: operateFormatter,
                    events: operateEvents
                },
                {
                    field: 'operate',
                    title: 'Set Preference',
                    clickToSelect: false,
                    formatter: operateFormatterPreference,
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
            $('.btnAdd').hide()

        
                            $.ajax({
                                url: $_UrlGetUserById,
                                type: "Get",
                                data: { id: $("#currentUserId").val() },
                                success: function (result) {
                                    debugger
                                    $("#facebookProfile").attr('href', result.FacebookProfile);
                                    $("#linkedinProfile").attr('href', result.LinkedInProfile);
                                    $("#twitterProfile").attr('href', result.TwitterProfile);
                                    },
                            });



             


            
        },
        onPageChange: function () {
            Addtitle();
            $('.btnAdd').hide()
        },

    });
});

function operateFormatter(value, row, index) {
    debugger
    

    return [
        '<button id="delete" class="btn btn-link"  title="Remove">',
            '<i class="fa fa-trash"></i>',
        '</button>'
    ].join('');
}

function operateStatus(value, row, index) {
    if (row.IsActive == true) {
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
function operateFormatterPreference(value , row , index)
{
    if (row.SocialMedia == "Facebook") {
        return [     
      '<button id="SetEditPreference" class="btn btn-link"  title="Set">',
         '<i class="fa fa-edit"></i>',
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
                        url: $_BaseUrl + "/Home/DeleteSocialAccount/" + row.Fid,
                        success: function (data) {
                            debugger
                            if (data.status == true) {                               
                                dialogItself.close();
                                RefreshGrid();
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
    'click #SetEditPreference': function (e, value, row, index)
    {
        $('.loadercontainingdiv').show();
        var modal = $("#setPereference");
        //$("#hdnUserId", modal).val(row.UserId);
        //$('#SubIndustryId', modal).val(row.SubIndustryId);
        modal.modal("show");
        $('.loadercontainingdiv').hide();
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
                        url: $_BaseUrl + "/Home/UpdateSocialStatusDeactive/" + row.Fid,
                        success: function (data) {
                            debugger
                            if (data.status == true) {                                
                                dialogItself.close();
                                RefreshGrid();
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
                        url: $_BaseUrl + "/Home/UpdateSocialStatusActive/" + row.Fid,
                        success: function (data) {
                            debugger
                            if (data.status == true) {                               
                                dialogItself.close();
                                RefreshGrid();
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


var chk = false;
function RefreshGrid() {
    debugger;
    document.getElementById("btnFilter").click();
}