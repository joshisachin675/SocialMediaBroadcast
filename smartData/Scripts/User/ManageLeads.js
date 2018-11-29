$(document).ready(function () {  

    $('.btnAdd').hide()
    var Param = {};
    var $table = $("#tblmanageLeads");

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

    $("#btnFilterleads").click(function () {
        debugger
      
        $table.bootstrapTable('refresh');
     
    });

    Param.currentUserId = $("#currentUserId").val();
    var reqUrl = $_BaseUrl + '/Users/api/HomeApi/GetLeads';
    // alert(reqUrl);

    // Param.clinicId = $("#clinicId").val();

    $('#tblmanageLeads').bootstrapTable({
        headers: headers,
        method: 'post',
        url: reqUrl,
        cache: true,
        height: 700,
        classes: 'table table-hover',
        customParams: Param,
        // queryParams: Param,
        queryParams: function (p) {
            return {
                Name: $("#name").val(),
                Description: $("#email").val(),
                order: p.order,
                limit: p.limit,
                search:p.search,
                offset: p.offset,
                sort:p.sort

            };
        },
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
                   field: 'HomeValueId',
                   title: '#',
                   checkbox: false,
                   type: 'search',
                   sortable: true,
                   visible: false,
                   switchable: false,
               }, {
                   field: 'FirstName',
                   title: 'Name',
                   checkbox: false,
                   type: 'search',
                   sortable: true,
                   formatter: function (value, row, index) {
                       debugger
                       if (row.FirstName == "" && row.LastName =="") {
                           return "<strong>N/A</strong>";
                       }
                       else {
                           var Fullname = row.FirstName + " " + row.LastName
                           return Fullname;

                       }
                   },

               },
                {
                    field: 'EmailAddress',
                    title: ' Email Address',
                    checkbox: false,
                    type: 'search',
                    sortable: true,
                    formatter: function (value, row, index) {
                        if (value == "") {
                            return "<strong>N/A</strong>";
                        }
                        else {
                          
                            return value;

                        }


                    },

                },
                 {
                     field: 'StreetAddress',
                     title: 'Property Address',
                     checkbox: false,
                     type: 'search',
                     formatter: function (value, row, index) {
                         debugger

                         var data = row.City == "" && row.StreetAddress == "" && row.Unit == "" && row.Province == "" ? row.Address : true;

                         if (data == true) {
                             var StreetAddress = row.StreetAddress == "" || row.StreetAddress == null ? " <strong></strong>" : "" + row.StreetAddress + "";
                             var city = row.City == "" || row.City == null ? " <strong></strong>" : ", " + row.City + "";                          
                             var Unit = row.Unit == "" || row.Unit == null ? " <strong></strong>" : ", " + row.Unit + "";
                             var Province = row.Province == "" || row.Province == null ? ", <strong></strong>" : ", " + row.Province + "";
                             var Data = StreetAddress + Unit + city + Province;
                             return Data;
                         }
                         else {
                             return data;
                         }
                      
                     },


                 },
                  
                     {
                         field: 'PostalCode',
                         title: 'Postal Code',
                         checkbox: false,
                         type: 'search',
                        

                     },
                      {
                          field: 'TimePeriodId',
                          title: 'Time to Act',
                          checkbox: false,
                          type: 'search',
                          formatter: function (value, row, index) {
                              if (value == "0") 
                                  return "Immediately ";
                              
                              else  if (value == "0")                            
                                  return "Immediately ";
                              
                              else if (value == "3")                                
                                  return "3 months";
                                                      
                              else if (value == "6")                                
                                  return "6 months";
                                
                              else if (value == "12") 

                                  return "Just looking to stay informed. ";
                              else if (value == "")

                                  return "None of Above";


},


},
                 {
                     field: 'PhoneNumber',
                     title: ' Phone Number',
                     checkbox: false,
                     type: 'search',
                     // sortable: true,


                 },
                {
                    field: 'IsCompleted',
                    title: 'Status',
                    type: 'search',
                    clickToSelect: false,
                    sortable: true,
                    formatter: function (value, row, index) {

                        if (value == true) {
                            return "Complete";
                        }
                        else {
                            return "Partially Completed"

                        }
                    },
                }, {
                    field: 'DateSubmit',
                    title: 'Date Submit',
                    checkbox: false,
                    type: 'search',
                    sortable: true,
                    formatter: function (value) {
                        debugger

                        var date = new Date(value);
                        var completeDate = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
                        return completeDate;
                    }
                },
                 {
                     field: 'IPAddress',
                     title: 'IP Address',
                     checkbox: false,
                     type: 'search',
                     sortable: true,
                     visible: false,
                 },
                 {
                     field: 'operate',
                     title: 'Actions',
                     clickToSelect: false,
                     formatter: operateFormatter,
                     events: operateEvents
                 },

               //{
               //    field: 'operate',
               //    title: 'Actions',
               //    clickToSelect: false,
               //    formatter: operateFormatter,
               //    events: operateEvents
               //},

],
    onLoadSuccess: function () {
        debugger;
            
        Addtitle();
        $('.btnAdd').hide()
        var $table = $('#tblmanageLeads');
        if ($("#currentUserType").val()==3) {
               

            $table.bootstrapTable('showColumn', 'IPAddress');
        }
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
        '<button id="editLeads" class="btn btn-link"  title="Edit">',
            '<i class="fa fa-edit"></i> Edit',
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
        BootstrapDialog.alert(row.Description);

    },
    'click #editLeads': function (e, value, row, index) {
        list = [];
        debugger
        $("#FirstName").val(row.FirstName);
        $("#LastName").val(row.LastName);
        $("#EmailAddress").val(row.EmailAddress);
        $("#PhoneNumber").val(row.PhoneNumber);
        $("#Address").val(row.Address);
        $("#PostalCode").val(row.PostalCode);
        $("#StreetAddress").val(row.StreetAddress);
        $("#City").val(row.City);
        $("#Unit").val(row.Unit);
        $("#Province").val(row.Province);
        $("#TimePeriodId").val(row.TimePeriodId);
        $("#HomeValueId").val(row.HomeValueId);
        $("#IPAddress").val(row.IPAddress);
        $("#DateSubmit").val(row.DateSubmit); 
        $("#mdlEditLeads").modal("show");
        // Dashboard.BindDataMethods.CreateJsonForRePostDetails(e, value, row, index);
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

var $table = $("#tblmanageLeads");
var chk = false;
function RefreshGrid() {
    document.getElementById("btnFilter").click();
}




