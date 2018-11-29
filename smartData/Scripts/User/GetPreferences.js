//$(document).ready(function () {
//    debugger;
//    $('.btnAdd').hide()
//    var Param = {};
//    var $table = $("#grid");



//    $('#resetBtn').on('click', function () {
//        $("#custom-toolbar").find('input').val('');
//        $table.bootstrapTable('refresh');
//        $("#resetBtn").attr("disabled", "disabled");
//    });

//    $('#AddPreference').on('click', function () {
//        debugger
//        $("#addPreferences").modal("show");
//        $('#Preference', $("#addPreferences")).val("");

//    });

//    $("#custom-toolbar input").on("keyup", function () {
//        if ($(this).val() != '')
//            $("#resetBtn").removeAttr("disabled");
//        else
//            $("#resetBtn").attr("disabled", "disabled");
//    });

//    $("#btnFilter").click(function () {

//        var Preference = $("#Preference").val();


//        if (Preference != "") {
//            Param.Preference = Preference;
//        }
//        else {
//            Param.Preference = "";
//        }


//        $table.bootstrapTable('refresh');
//        Param.clickBtn = false;
//    });

//    Param.currentUserId = $("#currentUserId").val();
//    var reqUrl = $_BaseUrl + '/Users/api/PreferenceAPI/GetAllPreference';
//    // alert(reqUrl);

//    // Param.clinicId = $("#clinicId").val();

//    $('#grid').bootstrapTable({
//        headers: headers,
//        method: 'post',
//        url: reqUrl,
//        cache: true,
//        height: 700,
//        classes: 'table table-hover',
//        customParams: Param,
//        // striped: true,
//        pageNumber: 1,
//        pagination: true,
//        pageSize: 10,
//        pageList: [5, 10, 20, 30],
//        search: false,
//        showColumns: true,
//        showRefresh: true,
//        sidePagination: 'server',
//        minimumCountColumns: 2,
//        showHeader: true,
//        showFilter: false,
//        smartDisplay: true,
//        clickToSelect: true,
//        // rowStyle: rowStyle,
//        toolbar: '#custom-toolbar',
//        columns: [
//            {
//                field: 'PreferenceId',
//                title: '#',
//                checkbox: false,
//                type: 'search',
//                sortable: true,
//                visible: false,
//                switchable: false,
//            }, 
//                {
//                    field: 'Preference',
//                    title: ' Preference',
//                    checkbox: false,
//                    type: 'search',
//                    sortable: true,
//                }, {
//                    field: 'CreatedDate',
//                    title: 'Created Date',

//                    checkbox: false,
//                    type: 'search',
//                    sortable: true,

//                    formatter: function (value) {
//                        debugger
//                        var localDate = new Date(value);
//                        var Date1 = localDate.toDateString('dddd, mmmm dS, yyyy, h:MM:ss TT');

//                        var hour = localDate.getHours();
//                        var minut = localDate.getMinutes();

//                        var completeDate = Date1+"  "+hour + ":" + minut;
//                        return completeDate;
//                    }


//                }, {
//                    field: 'operate',
//                    title: 'Actions',
//                    clickToSelect: false,
//                    formatter: operateFormatter,
//                    events: operateEvents
//                },

//                //{
//                //    field: 'checkbox',
//                //    title: 'Actions',
//                //    checkbox: false,
//                //    clickToSelect: true,
//                //    formatter: operateStatus,
//                //    events: operateEvents
//                //}
//        ],
//        onLoadSuccess: function () {
//           // debugger;
//            Addtitle();
//            $('.btnAdd').hide()
//        },
//        onPageChange: function () {
//        //    debugger;
//            Addtitle();
//            $('.btnAdd').hide()
//        },

//    });
//});



//function operateFormatter(value, row, index) {

//    return [
//        '<button id="edit" class="btn btn-link"  title="Edit">',
//           '<i class="fa fa-edit"></i> Edit',
//       '</button>',
//        '<button id="delete" class="btn btn-link"  title="Remove">',
//            '<i class="fa fa-trash"></i> Delete',
//        '</button>'
//    ].join('');
//}

////function operateStatus(value, row, index) {
////    if (row.IsActive == true) {
////       // debugger
////        return [
////               '<button id="edit" class="btn btn-link"  title="Edit">',
////           '<i class="fa fa-trash"></i> Edit',
////       '</button>',
////           //'<button id="activate" class="btn btn-link"  title="deactivate">',
////           //         '<i class="fa fa-trash"></i> Active',
////           //     '</button>'

////        ].join('');
////    }
////    else {
////        return [
////                   '<button id="deactivate" class="btn btn-link"  title="Activate">',
////                            '<i class="fa fa-trash"></i> Deactive',
////                        '</button>'

////        ].join('');
////    }

////}

//window.operateEvents = {
//    'click #delete': function (e, value, row, index) {
//       // debugger;
//        BootstrapDialog.show({
//            title: 'Confirmation',
//            message: 'Are you sure you want to delete this record?',
//            buttons: [{
//                label: 'No',
//                cssClass: 'btn-danger',
//                action: function (dialogItself) {
//                    dialogItself.close();
//                }
//            },
//            {
//                label: 'Yes',
//                cssClass: 'btn-primary',
//                action: function (dialogItself) {
//                 //   debugger
//                    $.ajax({

//                        cache: false,
//                        async: true,
//                        type: "POST",
//                        url: $_BaseUrl + "/Preference/DeletePreference/" + row.PreferenceId,
//                        success: function (data) {
//                          //  debugger
//                            if (data.status == true) {
//                                RefreshGrid();
//                                dialogItself.close();
//                            }
//                            else {
//                                ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.');
//                            }
//                        },
//                        error: function (request, error) {
//                            if (request.statusText == "CustomMessage") {
//                                $("#spanError").html(request.responseText)
//                            }
//                        },
//                        headers: {
//                            'RequestVerificationToken': $("#TokenValue").val()
//                        }
//                    });
//                }
//            }]
//        });

//    },
//    'click #edit': function (e, value, row, index) {
//     //   debugger;
//        $('.label-info').each(function () {
//            $(this).remove();
//        });

//        $('.loadercontainingdiv').show();
//        $.ajax({

//            cache: false,
//            async: true,
//            type: "POST",
//            url: $_BaseUrl + '/Preference/EditPreference',
//            data: { id: row.PreferenceId },
//            success: function (data) {
//                debugger;
//                $('.loadercontainingdiv').hide();
//                $('textarea#txtEditMsgContent').val(row.Preference);
//                $('#PreferenceId').val(row.PreferenceId);
//                if (data.result != null) {
//                    $("#EditPreference").modal("show");
//                    $('textarea#txtEditMsgContent').html(data.result.Preference);
//                         }
//            },

//            error: function (request, error) {
//                $('.loadercontainingdiv').hide();
//                if (request.statusText == "CustomMessage") {
//                    $("#spanError").html(request.responseText)
//                }
//            },
//            headers: {
//                'RequestVerificationToken': $("#TokenValue").val()
//            }
//        });
//    },



//};


//function Addtitle() {
//    $('.page-next').attr('Title', 'Next');
//    $('.page-first').attr('Title', 'First');
//    $('.page-last').attr('Title', 'Last');
//    $('.page-pre').attr('Title', 'Previous');
//}

//function AddNew() {
//    $('#editStaff input[type="text"]').val(''); //Empty all fields of the edit form on click of the add staff button
//    $("#editStaff").modal("hide");
//    $("#addStaff").modal("show");
//}

//function rowStyle(row, index) {
//    var classes = ['active', 'success', 'info', 'warning', 'danger'];

//    if (index % 2 === 0) {
//        return {
//            classes: classes[1]
//        };
//    }
//    return {};
//}

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
//                $("#addContent").modal('hide');
//            }
//        }]
//    });
//}


//var $table = $("#grid");
//var chk = false;
//function RefreshGrid() {

//    document.getElementById("btnFilter").click();

//}




$(document).ready(function () {
    var Param = {};
    var $table = $("#grid");
    Param.IndustryId = $("#IndustryId").val();
    Param.UserType = $("#UserType").val();
    Param.currentUserId = $("#currentUserId").val();




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

    //$("#btnFilter").click(function () {
    //    debugger;
    //    var subIndustry = $("#subIndustry").val();

    //    if (subIndustry != "") {
    //        Param.subIndustry = subIndustry;
    //    }
    //    else {
    //        Param.subIndustry = "";
    //    }

    //    $table.bootstrapTable('refresh');
    //    Param.clickBtn = false;
    //});


    $("#btnFilter").click(function () {
        var Preference = $("#Preference").val();
        if (Preference != "") {
            Param.Preference = Preference;
        }
        else {
            Param.Preference = "";
        }
        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

    var reqUrl = $_BaseUrl + '/Users/api/AdminApi/GetAllSubCategoryListWithPrefrence';

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
        pageSize: 20,
        pageList: [5, 10, 20, 30],
        search: false,
        showColumns: true,
        showRefresh: false,
        sidePagination: 'server',
        minimumCountColumns: 2,
        showHeader: true,
        showFilter: false,
        smartDisplay: true,
        clickToSelect: false,
        checkboxHeader: true,
        // rowStyle: rowStyle,
        toolbar: '#custom-toolbar',
        columns: [
            {
                field: 'SubIndustryId',
                title: 'Select Preferences',
                checkbox: true,
                type: 'search',
                sortable: true,
                //visible: false,
                switchable: false,
                formatter: function (value, row, index) {
                    debugger;
                    if (row.Preference == true) {
                        return { checked: true }
                    }
                    else {
                        return { checked: false }
                    }
                },

            },
            {
                field: 'SubIndustryName',
                title: 'Preferences',
                checkbox: false,
                type: 'search',
                sortable: true,
            }
        ],

        onCheck: function (row, $element) {
            debugger;
            AddPrefrence(row);

        },

        onCheckAll: function (row, $element) {
            AddPrefrence(row);
        },

        onUncheck: function (row, $element) {
            DeletePrefrence(row);
        },

        onUncheckAll: function (row, $element) {

            DeletePrefrence(row);



        },

        onLoadSuccess: function () {
            Addtitle();
            $('.btnAdd').hide();
            $('.columns.columns-right.btn-group.pull-right').hide();
        },
        onPageChange: function () {
            Addtitle();
            $('.btnAdd').hide();
            $('.columns.columns-right.btn-group.pull-right').hide();
        },
    });
});


function operateFormatter(value, row, index) {
    return [
        '<button class="edit btn btn-link" title="Edit">',
    '<i class="fa fa-pencil-square-o"></i>',
'</button>',
        '<button id="delete" class="delete btn btn-link"  title="Remove">',
            '<i class="fa fa-trash"></i>',
        '</button>'
    ].join('');
}

function operateStatus(value, row, index) {
    if (row.IsActive == true) {
        return [
           '<button id="activate" class="activate btn btn-link"  title="deactivate">',
                    '<i class="fa fa-unlock"></i>',
                '</button>'

        ].join('');
    }
    else {
        return [
                   '<button id="deactivate" class="deactivate btn btn-link"  title="Activate">',
                            '<i class="fa fa-lock"></i>',
                        '</button>'

        ].join('');
    }
}

window.operateEvents = {
    'click .edit': function (e, value, row, index) {
        $('.loadercontainingdiv').show();
        var modal = $("#editSubIndustry");
        $("#SubIndustryName", modal).val(row.SubIndustryName);
        $('#SubIndustryId', modal).val(row.SubIndustryId);
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
                        url: $_BaseUrl + "/Admin/ManageCategory/DeleteSubIndustry",
                        data: { id: row.SubIndustryId },
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
        $('.loadercontainingdiv').show();
        var modal = $("#addSubIndustry");
        $('#IndustryId', modal).val(row.IndustryId);
        modal.modal("show");
        $('.modal').css('display', 'block');
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

function AddPrefrence(obj) {
    debugger;
    if (obj != undefined) {
        $.ajax({
            url: $_UrladdPrefrences,
            type: 'Get',
            data: { "name": obj.SubIndustryName },
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
            }
        });
    }
    else {
        $.ajax({
            url: $_UrladdPrefrences,
            type: 'Get',
            data: { "name": "All" },
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
            }
        });
    }

}

function DeletePrefrence(obj) {
    debugger;
    if (obj != undefined) {
        $.ajax({
            url: $_UrlDeletePrefrence,
            type: 'Get',
            data: { "name": obj.SubIndustryName },
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
            }
        });
    }
    else {
        $.ajax({
            url: $_UrlDeletePrefrence,
            type: 'Get',
            data: { "name": "All" },
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
            }
        });
    }
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