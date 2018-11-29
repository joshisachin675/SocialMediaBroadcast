

$(document).ready(function () {
    $("#tabs").tabs();
    $('.timepicker-default').timepicker();
    var Day = 1;
    $.ajax({
        url: $_UrlGetPublishingTimeByDay,
        type: "Get",
        data: { Day: Day },
        success: function (result) {

            if (result.length == 0) { 
                $("#savedPreferenceData").hide();
                $("#PreferenceData").show();

            }
            else {
                $("#timeDefault").val(result[0].Time);
                $("#hdtimeDefaultSaved").val(result[0].Time);
                
                $("#savedPreferenceData").show();
                $("#PreferenceData").hide();
             //   $("#tabs-Mon").append('<div class="bind-main"><div class="input-append bootstrap-timepicker-component custom-time-pick col-lg-3 col-md-3 col-sm-3 col-xs-12 mrgn-tp"> <input type="text" class="timepicker-default input-small" value =' + result[0].Time + ' id="timeDefault">                                <span class="add-onon" style="margin-left: -40px">                                    <i class="icon-time"></i>  </span></div></div>')
                $.each(result, function (i, val) {
                    //debugger;                       /// $(select).append('<div class="col-sm-6 predefined-time row" data-day-number="' + Day + '" data-day="' + dayName + '" data-time="' + val.Time + '" data-id="15453295"><label class="bg-label">' + dayName + '</label><span class="t-value">' + val.Time + '</span><a class="close remove-predefined-time" title="Remove time"><span id=' + val.PublishingTimeId + ' class="glyphicon glyphicon-remove" onClick="remove(this)" aria-hidden="true"></span></a></div>');
                });
            }

           
        }
    });


    //if ($('#scheduledPostSection').html().trim() == "") {
    //    $('#scheduledPostSection').append('<div id="Nopost"> <p id="Nopost"> <p><img class="norecords" src="/Images/no_document.png"></p>No Posts for Preview</p></div> ').css({ "text-align": "center", "font-style": "italic", "color": "gray" });
    //}
  
     
    $("#btnadd").click(function () {
        //debugger;
        var a = [];
        $(".PreferenceCheck:checked").each(function () {
            a.push($(this).attr("data-SubIndustryId"));
        });

        if (a.length == 0) {
            var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
            BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
            BootstrapDialog.show({ title: "<span>Error</span>", type: BootstrapDialog.TYPE_DANGER, message: 'Select atleast 1 preference.' });
            return false;

        }
        $('.loadercontainingdiv').show();
        var tab = $("#tabs .ui-tabs-active").text().trim();
        var timeoffset = new Date().getTimezoneOffset();
        var TimeStamp = "";
        var Day = "";
        var selector = "";
        var DayName = "";
        var isValid = true;
        if (tab == "Monday") {
            TimeStamp = $('#timeDefault').val();
            Day = 1;
            selector = "#tabs-Mon";
            DayName = "Mon";
            var oldtime = $('#tabs-Mon').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
            if ($('#tabs-Mon').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            if (oldtime == newTime) {
                BootstrapDialog.alert('Time already exists');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
        }
        else if (tab == "Tuesday") {
            TimeStamp = $('#timeDefault').val();
            Day = 2;
            selector = "#tabs-Tue";
            DayName = "Tue";
            var oldtime = $('#tabs-Tue').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
            if ($('#tabs-Tue').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            if (oldtime == newTime) {
                BootstrapDialog.alert('Time already exists');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
        }
        else if (tab == "Wednesday") {
            TimeStamp = $('#timeDefault').val();
            Day = 3;
            selector = "#tabs-Wed";
            DayName = "Wed";
            var oldtime = $('#tabs-Wed').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
            if ($('#tabs-Wed').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            if (oldtime == newTime) {
                BootstrapDialog.alert('Time already exists');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
        }
        else if (tab == "Thursday") {
            TimeStamp = $('#timeDefault').val();
            Day = 4;
            selector = "#tabs-Thr";
            DayName = "Thr";
            var oldtime = $('#tabs-Thr').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
            if ($('#tabs-Thr').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            if (oldtime == newTime) {
                BootstrapDialog.alert('Time already exists');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
        }
        else if (tab == "Friday") {
            TimeStamp = $('#timeDefault').val();
            Day = 5;
            selector = "#tabs-Fri";
            DayName = "Fri";
            var oldtime = $('#tabs-Fri').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
            if ($('#tabs-Fri').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            if (oldtime == newTime) {
                BootstrapDialog.alert('Time already exists');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
        }
        else if (tab == "Saturday") {
            TimeStamp = $('#timeDefault').val();
            Day = 6;
            selector = "#tabs-Sat";
            DayName = "Sat";
            var oldtime = $('#tabs-Sat').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
            if ($('#tabs-Sat').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than two Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            if (oldtime == newTime) {
                BootstrapDialog.alert('Time already exists');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
        }
        else if (tab == "Sunday") {
            TimeStamp = $('#timeDefault').val();
            Day = 7;
            selector = "#tabs-Sun";
            DayName = "Sun";
            var oldtime = $('#tabs-Sun').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
            if ($('#tabs-Sun').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than two Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            if (oldtime == newTime) {
                BootstrapDialog.alert('Time already exists');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
        }
        if (isValid == true) {
            $.ajax({
                url: $_UrlAddPublishingTime,
                type: "Post",
                data: { Time: TimeStamp, Day: Day, offset: timeoffset },
                success: function (result) {
                    var id = result.PublishingTimeId;
                    $(selector).append('<div class=" col-sm-7 predefined-time row" data-day-number="' + Day + '" data-day="' + DayName + '" data-time="' + $('#timeDefault').val() + '" data-id="' + id + '"><label class="bg-label">' + DayName + '</label><span class="t-value">' + $('#timeDefault').val() + '</span><a class="close remove-predefined-time" title="Remove time"><span id=' + id + ' class="glyphicon glyphicon-remove" onClick="remove(this)" aria-hidden="true"></span></a></div>');
                    $('.loadercontainingdiv').hide();
                }
            });
        }      
    });
    ////================================= Update seleted prefereces ==================================

    $("#btnUpdateAutoPreference").click(function () {
        //  var time  = 

      

        $('.loadercontainingdiv').show();
        var tab = $("#tabs .ui-tabs-active").text().trim();
        var timeoffset = new Date().getTimezoneOffset();
        var TimeStamp = "";
        var Day = "";
        var selector = "";
        var DayName = "";
        var isValid = true;
        if (tab == "Monday") {
            TimeStamp = $('#timeDefault').val();
            Day = 1;
            selector = "#tabs-Mon";
            DayName = "Mon";
            var oldtime = $('#tabs-Mon').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();           
        }
        else if (tab == "Tuesday") {
            TimeStamp = $('#timeDefault').val();
            Day = 2;
            selector = "#tabs-Tue";
            DayName = "Tue";
            var oldtime = $('#tabs-Tue').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();           
        }
        else if (tab == "Wednesday") {
            TimeStamp = $('#timeDefault').val();
            Day = 3;
            selector = "#tabs-Wed";
            DayName = "Wed";
            var oldtime = $('#tabs-Wed').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
            
          
        }
        else if (tab == "Thursday") {
            TimeStamp = $('#timeDefault').val();
            Day = 4;
            selector = "#tabs-Thr";
            DayName = "Thr";
            var oldtime = $('#tabs-Thr').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
           
           
        }
        else if (tab == "Friday") {
            TimeStamp = $('#timeDefault').val();
            Day = 5;
            selector = "#tabs-Fri";
            DayName = "Fri";
            var oldtime = $('#tabs-Fri').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
           
           
        }
        else if (tab == "Saturday") {
            TimeStamp = $('#timeDefault').val();
            Day = 6;
            selector = "#tabs-Sat";
            DayName = "Sat";
            var oldtime = $('#tabs-Sat').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
            
           
        }
        else if (tab == "Sunday") {
            TimeStamp = $('#timeDefault').val();
            Day = 7;
            selector = "#tabs-Sun";
            DayName = "Sun";
            var oldtime = $('#tabs-Sun').find('div').find('.t-value').html();
            var newTime = $('#timeDefault').val();
          
           
        }

        JsonData = {}
       
        var a = [];
        $(".SavedPreferenceCheck:checked").each(function () {
            a.push($(this).attr("data-SubIndustryId"));
        });

        if (a.length==0) {
            $('.loadercontainingdiv').hide();
            var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
            BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
            BootstrapDialog.show({ title: "<span>Error</span>", type: BootstrapDialog.TYPE_DANGER, message: 'Please select atleast 1 preference.' });
            return false;
        }
        var b = [];
        $(".SavedlandGenCheck:checked").each(function () {
            b.push($(this).attr("data-pageid"));
        });
        var Time = [];
        Time.push(TimeStamp)
        Time.push(new Date().getTimezoneOffset())

        JsonData.DayID = Day;
        JsonData.Time = Time
        JsonData.SelectedPreferences = a;
        JsonData.PageID = b;
        


        $.ajax({
            url: $_UrlUpdateAutoPreference,
            type: "Get",
            data: { "Data": JSON.stringify(JsonData) },
            success: function (result) {
                //debugger

                $('.loadercontainingdiv').hide();
                $(".SavedPreferenceCheck").each(function () {
                    debugger
                    var subIndId = [];
                    var subIndIdName = [];
                    subIndId.push($(this).attr("data-SubIndustryId"));
                    subIndIdName.push($(this).attr("data-SubindustryName"));
                    if ($(this).is(':checked')) {
                        var JsonWorkInfo = {};
                        JsonWorkInfo.SelectedPreferences = subIndId;
                        JsonWorkInfo.SelectedPreferencesName = subIndIdName;
                        JsonWorkInfo.DayID = Day;
                        JsonWorkInfo.Status = true;
                        $.ajax({
                            url: $_UrlSaveAutoPreferenceDetails,
                            type: "Get",
                            data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                            success: function (result) {
                                // BootstrapDialog.alert("Post is scheduled Successfully");
                            }


                        })
                    }
                    else {
                        var JsonWorkInfo = {};
                        JsonWorkInfo.SelectedPreferences = subIndId;
                        JsonWorkInfo.SelectedPreferencesName = subIndIdName;
                        JsonWorkInfo.DayID = Day;
                        $.ajax({
                            url: $_UrlSaveAutoPreferenceDetails,
                            type: "Get",
                            data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                            success: function (result) {
                                //BootstrapDialog.alert("Post is scheduled Successfully");
                            }


                        })
                    }



                });
                $(".SavedlandGenCheck").each(function () {
                    debugger
                    var PageID = [];
                    PageID.push($(this).attr("data-pageid"));
                    if ($(this).is(':checked')) {
                        var JsonWorkInfo = {};
                        JsonWorkInfo.PageID = PageID;
                        JsonWorkInfo.DayID = Day;
                        JsonWorkInfo.Status = true;
                        $.ajax({
                            url: $_UrlSaveAutoLandPageDetail,
                            type: "Get",
                            data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                            success: function (result) {
                                // BootstrapDialog.alert("Post is scheduled Successfully");
                            }


                        })
                    }
                    else {
                        var JsonWorkInfo = {};
                        JsonWorkInfo.PageID = PageID;
                        JsonWorkInfo.DayID = Day;
                        JsonWorkInfo.Status = false;
                        $.ajax({
                            url: $_UrlSaveAutoLandPageDetail,
                            type: "Get",
                            data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                            success: function (result) {
                                //BootstrapDialog.alert("Post is scheduled Successfully");
                            }


                        })
                    }



                });
                if (result == "True") {
                    refreshTab();
                    var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
                    BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                    BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_SUCESS, message: 'Preferences was updated successfully.' });

                }
            },
            error : function(data)
            {
                refreshTab();
                $('.loadercontainingdiv').hide();
                var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
                BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                BootstrapDialog.show({ title: "<span>Error</span>", type: BootstrapDialog.TYPE_DANGER, message: 'Some error occurred .' });
            }
        });

       

        $('.loadercontainingdiv').hide();
    });
    $("#btnDeleteAutoPreference").click(function () {
        $('.loadercontainingdiv').show();
        var tab = $("#tabs .ui-tabs-active").text().trim();
        var timeoffset = new Date().getTimezoneOffset();
        var TimeStamp = "";
        var Day = "";
        var selector = "";
        var DayName = "";
        var isValid = true;
        if (tab == "Monday") {
            TimeStamp = $('#hdtimeDefaultSaved').val();
            Day = 1;
            selector = "#tabs-Mon";
            DayName = "Mon";
            var oldtime = $('#tabs-Mon').find('div').find('.t-value').html();
            var newTime = $('#hdtimeDefaultSaved').val();
        }
        else if (tab == "Tuesday") {
            TimeStamp = $('#hdtimeDefaultSaved').val();
            Day = 2;
            selector = "#tabs-Tue";
            DayName = "Tue";
            var oldtime = $('#tabs-Tue').find('div').find('.t-value').html();
            var newTime = $('#hdtimeDefaultSaved').val();
        }
        else if (tab == "Wednesday") {
            TimeStamp = $('#hdtimeDefaultSaved').val();
            Day = 3;
            selector = "#tabs-Wed";
            DayName = "Wed";
            var oldtime = $('#tabs-Wed').find('div').find('.t-value').html();
            var newTime = $('#hdtimeDefaultSaved').val();


        }
        else if (tab == "Thursday") {
            TimeStamp = $('#hdtimeDefaultSaved').val();
            Day = 4;
            selector = "#tabs-Thr";
            DayName = "Thr";
            var oldtime = $('#tabs-Thr').find('div').find('.t-value').html();
            var newTime = $('#hdtimeDefaultSaved').val();


        }
        else if (tab == "Friday") {
            TimeStamp = $('#hdtimeDefaultSaved').val();
            Day = 5;
            selector = "#tabs-Fri";
            DayName = "Fri";
            var oldtime = $('#tabs-Fri').find('div').find('.t-value').html();
            var newTime = $('#hdtimeDefaultSaved').val();


        }
        else if (tab == "Saturday") {
            TimeStamp = $('#hdtimeDefaultSaved').val();
            Day = 6;
            selector = "#tabs-Sat";
            DayName = "Sat";
            var oldtime = $('#tabs-Sat').find('div').find('.t-value').html();
            var newTime = $('#hdtimeDefaultSaved').val();


        }
        else if (tab == "Sunday") {
            TimeStamp = $('#hdtimeDefaultSaved').val();
            Day = 7;
            selector = "#tabs-Sun";
            DayName = "Sun";
            var oldtime = $('#tabs-Sun').find('div').find('.t-value').html();
            var newTime = $('#hdtimeDefaultSaved').val();


        }

        JsonData = {}

        var a = [];
        $(".SavedPreferenceCheck:checked").each(function () {
            a.push($(this).attr("data-SubIndustryId"));
        });
        var b = [];
        $(".SavedlandGenCheck:checked").each(function () {
            b.push($(this).attr("data-pageid"));
        });
        var Time = [];
        Time.push(TimeStamp)
        Time.push(new Date().getTimezoneOffset())
        JsonData.DayID = Day;
        JsonData.Time = Time
        JsonData.SelectedPreferences = a;
        JsonData.PageID = b;



        $.ajax({
            url: $_UrlDeleteAutoPreference,
            type: "Get",
            data: { "Data": JSON.stringify(JsonData) },
            success: function (result) {
                //debugger
                $('.loadercontainingdiv').hide();
                if (result == "True") {
                   
                    var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
                    BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                    BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_SUCESS, message: 'Schedule post deleted successfully .' });
                    refreshTab();
                }
            },
            error : function(data)
            {
              
                $('.loadercontainingdiv').hide();
                var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
                BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                BootstrapDialog.show({ title: "<span>Error</span>", type: BootstrapDialog.TYPE_DANGER, message: 'Some error occurred .' });
                refreshTab();
            }
            
        });
        $('.loadercontainingdiv').hide();


    });
    $("#tabs").tabs({
        activate: function (event, ui) {
            //debugger;
            var Day = "";
            var dayName = "";
            var select = ui.newPanel.selector;
            if (select == "#tabs-Mon") {
                Day = 1;
                dayName = "Mon";
                $("#tabs-Mon").html("");
            }
            else if (select == "#tabs-Tue") {
                Day = 2;
                dayName = "Tue";
                $("#tabs-Tue").html("");
            }
            else if (select == "#tabs-Wed") {
                Day = 3;
                dayName = "Wed";
                $("#tabs-Wed").html("");
            }
            else if (select == "#tabs-Thr") {
                Day = 4;
                dayName = "Thr";
                $("#tabs-Thr").html("");
            }
            else if (select == "#tabs-Fri") {
                Day = 5;
                dayName = "Fri";
                $("#tabs-Fri").html("");
            }
            else if (select == "#tabs-Sat") {
                Day = 6;
                dayName = "Sat";
                $("#tabs-Sat").html("");
            }
            else if (select == "#tabs-Sun") {
                Day = 7;
                dayName = "Sun";
                $("#tabs-Sun").html("");
            }


            var tab = $("#tabs .ui-tabs-active").text().trim();
            JsonWorkInfo = {};
            JsonWorkInfo.DayID = Day;
            $.ajax({
                //// bind landing PAges
                url: $_GetLeadManage,
                type: "GET",
                data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                success: function (result) {
                    //debugger
                    $("#ManageLeads").html(result);

                    $.ajax({
                        //// bind ManageAutoPublishing PAges
                        url: $_GetContentPreference,
                        type: "GET",
                        data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                        success: function (result) {
                            //debugger
                            $("#ManageAutoPublishing").html(result);

                            $.ajax({
                                //// bind saved landing PAges
                                url: $_GetSavedLeadManage,
                                type: "GET",
                                data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                                success: function (result) {
                                    //debugger
                                    $("#SavedManageLeads").html(result);
                                    $.ajax({
                                        //// bind saved ManageAutoPublishing PAges
                                        url: $_GetSavedContentPreference,
                                        type: "GET",
                                        data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                                        success: function (result) {
                                            //debugger
                                            $("#SavedManageAutoPublishing").html(result);
                                        }
                                    });
                                }
                            });
                        }
                    });
                }
            });



            $.ajax({
                url: $_UrlGetPublishingTimeByDay,
                type: "Get",
                data: { Day: Day },
                success: function (result) {

                    if (result.length==0) {
                        $("#savedPreferenceData").hide();
                        $("#PreferenceData").show();
                        
                    }
                    else {
                        $("#timeDefault").val(result[0].Time);
                        $("#hdtimeDefaultSaved").val(result[0].Time);
                        $("#savedPreferenceData").show();
                        $("#PreferenceData").hide();
                      //  $(select).append('<div class="bind-main"><div class="input-append bootstrap-timepicker-component custom-time-pick col-lg-3 col-md-3 col-sm-3 col-xs-12 mrgn-tp"> <input type="text" class="timepicker-default input-small" value =' + result[0].Time + ' id="timeDefault">                                <span class="add-onon" style="margin-left: -40px">                                    <i class="icon-time"></i>                                </span>                            </div>                                                      </div>')
                        $.each(result, function (i, val) {
                            //debugger;                       /// $(select).append('<div class="col-sm-6 predefined-time row" data-day-number="' + Day + '" data-day="' + dayName + '" data-time="' + val.Time + '" data-id="15453295"><label class="bg-label">' + dayName + '</label><span class="t-value">' + val.Time + '</span><a class="close remove-predefined-time" title="Remove time"><span id=' + val.PublishingTimeId + ' class="glyphicon glyphicon-remove" onClick="remove(this)" aria-hidden="true"></span></a></div>');
                        });
                    }
                   
                }
            });
        }

    });



    //debugger;
    var Param = {};
    Param.categoryname = "";
    Param.UserType = $("#UserType").val();
    Param.IndustryId = $('#IndustryId').val();
    Param.UserId = $('#UserId').val();
    var $table = $("#grid");
    $("#btnFilter").click(function () {
        //debugger;
        isSerach = true;
        var categoryname = $("#categoryname").tagsinput('items');
        if (categoryname.length != "") {
            Param.categoryname = categoryname;
        }
        else {
            Param.categoryname = "";
            isSerach = false;

            var types = [BootstrapDialog.TYPE_INFO];

            $.each(types, function (index, type) {
                BootstrapDialog.show({
                    type: type,
                    message: 'Please enter tags to search !',
                });
            });
        }
        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

    //debugger;
    var noimg = $_BaseUrl + '/Images/noimage.png';

    var reqUrl = $_BaseUrl + '/Users/api/HomeApi/GetContentListByPreference';
    $('#grid').bootstrapTable({
        // headers: headers,
        method: 'post',
        url: reqUrl,
        cache: true,
        height: 700,
        classes: 'table table-hover',
        customParams: Param,
        striped: true,
        pageNumber: 1,
        pagination: true,
        pageSize: 5,
        pageList: [5, 10, 20, 30],
        search: false,
        showColumns: false,
        // showRefresh: true,
        sidePagination: 'server',
        minimumCountColumns: 2,
        showHeader: true,
        showFilter: false,
        smartDisplay: true,
        clickToSelect: true,
        checkboxHeader: false,
        // rowStyle: rowStyle,
        toolbar: '#custom-toolbar',
        //queryParams: function (p) {
        //    return { tags: ta }
        //},
        columns: [
            {
                field: 'ContentId',
                title: 'Select Content',
                checkbox: true,
                type: 'search',
                sortable: true,
                //visible: true,
                switchable: false,
            },
                  {
                      field: 'Description, ImageUrl, SocialMedia',
                      title: 'Content Library',
                      type: 'search',
                      sortable: true,
                      checkbox: false,
                      clickToSelect: false,
                      events: operateEvents,
                      formatter: function (value, row, index) {
                          var siteUrl = $_BaseUrl; 
                          var btnHtml = '';
                          if (row.ImageUrl != null) {
                              var fulltext = row.Title;
                              var shorttext = "";
                              //debugger;
                              if (fulltext.length >= 30) {
                                  shorttext = fulltext.substring(0, 30);
                                  var html = '  <button id="read" class=""  title="Read More"><i class="fa fa-pencil-square-o text-info"></i> Read More</button>';
                                  shorttext = shorttext + html;
                              }
                              else {
                                  shorttext = row.Title;
                              }
                              if (row.ImageUrl.indexOf("www") > -1 || row.ImageUrl.indexOf("http") > -1) {
                                  if (row.SocialMedia == "Facebook")
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='"  + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-facebook'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');

                                  else if (row.SocialMedia == "LinkedIn")
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='"  + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-linkedin'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');

                                  else if (row.SocialMedia == "Twitter")
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-twitter'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                                  else if (row.SocialMedia == "Rss")                                      
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-rss'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');

                                  else if (row.SocialMedia == "Google")
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='"  + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-google-plus'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                                  else
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-rss'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                              }

                              else {

                                  if (row.SocialMedia == "Facebook")
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + siteUrl + "/" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-facebook'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');

                                  else if (row.SocialMedia == "LinkedIn")
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + siteUrl + "/" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-linkedin'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');

                                  else if (row.SocialMedia == "Twitter")
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + siteUrl + "/" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-twitter'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                                  else if (row.SocialMedia == "Rss")
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-rss'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');

                                  else if (row.SocialMedia == "Google")
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + siteUrl + "/" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-google-plus'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                                  else
                                      return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + row.ImageUrl + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-rss'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');

                              }


                            
                          }
                          else {
                              var fulltext = row.Title;
                              var shorttext = "";
                              //debugger;
                              if (fulltext.length >= 30) {
                                  shorttext = fulltext.substring(0, 30);
                                  var html = '  <button id="read" class=""  title="Read More"><i class="fa fa-pencil-square-o text-info"></i> Read More</button>';
                                  shorttext = shorttext + html;
                              }
                              else {
                                  shorttext = row.Title;
                              }
                              if (row.SocialMedia == "Facebook")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-facebook'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                              else if (row.SocialMedia == "LinkedIn")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-linkedin'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                              else if (row.SocialMedia == "Twitter")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-twitter'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                              else if (row.SocialMedia == "Rss")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-rss'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                              else if (row.SocialMedia == "Google")
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-google-plus'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                              else
                                  return ["<table class='socialmed' border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td width='20%'><img src='" + noimg + "' height='40px' width='40px'/></td><td width='20%'><i class='fa fa-rss'></i></td><td width='40%'>" + shorttext + "</td></tr></table>"].join('');
                          }
                      }
                  }
        ],
        onCheck: function (row, $element) {           
            //debugger;
            //$('#scheduledPostSection').find('#Nopost').remove();
            $('#Nopost').hide();
            var initialUrl = "";
            if (row.ImageUrl != null) {
                if (row.ImageUrl.indexOf("www") > -1 || row.ImageUrl.indexOf("http") > -1) {
                    initialUrl=  row.ImageUrl
                }
                else {
                    initialUrl = $_BaseUrl + row.ImageUrl;
                }

               
            }
            else {
                initialUrl = "";
            }
          
            var logo = "";
            if (row.SocialMedia == "Facebook") {
                logo = '<i class="fa fa-facebook" style="background:#4f77bc;border-radius: 50%;font-size: 13px;color: #fff;  height: 30px; line-height: 30px;text-align: center; vertical-align: middle;width: 30px;"></i>';
            }
            else if (row.SocialMedia == "Twitter")
            {
                logo = '<i class="fa fa-twitter" style="background:#16c2fa;border-radius: 50%;font-size: 13px;color: #fff;  height: 30px; line-height: 30px;text-align: center; vertical-align: middle;width: 30px;"></i>';
            }
            else if (row.SocialMedia == "LinkedIn") {
                logo = '<i class="fa fa-linkedin" style="background:#2587ec;border-radius: 50%;font-size: 13px;color: #fff;  height: 30px; line-height: 30px;text-align: center; vertical-align: middle;width: 30px;" ></i>';
            }
            else if (row.SocialMedia == "Rss") {
                logo = '<i class="fa fa-rss" style="background:#2587ec;border-radius: 50%;font-size: 13px;color: #fff;  height: 30px; line-height: 30px;text-align: center; vertical-align: middle;width: 30px;" ></i>';
            }
            if (initialUrl != "") {
                $("#scheduledPostSection").append('<div class="row newpost" id="post"> <div class="col-md-2" id="logo" style="text-align: center;">' + logo + '</div> <div class="col-md-8"> <p id="Description"  style="font-style: italic; font-size: 13px;  color: gray;"> ' + row.Title.trim() + '</p><p id="GroupId"  style="font-style: italic; font-size: 13px;  color: gray; display:none"> ' + row.GroupId.trim() + '</p> </div> <div class="col-md-2" style="margin-bottom: 10px">  <img src=' + initialUrl + ' height="100" width="100" /> </div> </div>')
            }
            else {
                $("#scheduledPostSection").append('<div class="row newpost" id="post"> <div class="col-md-2" id="logo" style="text-align: center;">' + logo + '</div> <div class="col-md-8"> <p id="Description"  style="font-style: italic; font-size: 13px;  color: gray;"> ' + row.Title.trim() + '</p> <p id="GroupId"  style="font-style: italic; font-size: 13px;  color: gray; display:none"> ' + row.GroupId.trim() + '</p> </div> </div>')
            }
            
        },

        onUncheck: function (row, $element) {
            //debugger;
            $(".newpost").each(function (index, el) {
                //debugger;
                var txt = $.parseHTML($(this).find('#Description').html().trim());
                var newtxt = $.parseHTML(row.Title.trim());
                if (txt[0].data.trim() == newtxt[0].data.trim()) {
                    $(this).remove();
                }
            })
            var d = $("#scheduledPostSection").find("#post")
            if (d.length==0) {
                $('#Nopost').show();
            }
          //  $("#scheduledPostSection").find(row.Description.trim()).remove('div');

        },
        onLoadSuccess: function () {

        },
        onPageChange: function () {

        },
    });


    $("#addpublishingPosts").click(function () {
        //debugger;
        var a = [];
        $(".PreferenceCheck:checked").each(function () {
            a.push($(this).attr("data-SubIndustryId"));
        });

        if (a.length == 0) {
            $('.loadercontainingdiv').hide();
            var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
            BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
            BootstrapDialog.show({ title: "<span>Error</span>", type: BootstrapDialog.TYPE_DANGER, message: 'Please select atleast 1 preference.' });
            return false;

        }
        $('.loadercontainingdiv').show();
        //================================================ Save Preference
     
        //debugger
        var tab = $("#tabs .ui-tabs-active").text().trim();
        var DayID = 0;
        if (tab == "Monday") { DayID = 1; }
        else if (tab == "Tuesday") { DayID = 2 }
        else if (tab == "Wednesday") { DayID = 3; }
        else if (tab == "Thursday") { DayID = 4; }
        else if (tab == "Friday") { DayID = 5; }
        else if (tab == "Saturday") { DayID = 6; }
        else if (tab == "Sunday") { DayID = 7; }
        var subIndId = [];
        var subIndIdName = [];
        $(".PreferenceCheck:checked").each(function () {
            subIndId.length = 0;
            subIndIdName.length = 0;
            subIndId.push($(this).attr("data-SubIndustryId"));
            subIndIdName.push($(this).attr("data-SubindustryName"));
     
            if ($(this).is(':checked')) {
            var JsonWorkInfo = {};
            JsonWorkInfo.SelectedPreferences = subIndId;
            JsonWorkInfo.SelectedPreferencesName = subIndIdName;
            JsonWorkInfo.Status = true;
            JsonWorkInfo.DayID = DayID;
            $.ajax({
                url: $_UrlSaveAutoPreferenceDetails,
                type: "Get",
                data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                success: function (result) {
                    refreshTab();
                    // BootstrapDialog.alert("Post is scheduled Successfully");
                },
                error:function (result)
            {
                    $('.loadercontainingdiv').hide();
            }



            })

        }
        else {
            var JsonWorkInfo = {};
            JsonWorkInfo.SelectedPreferences = subIndId;
            JsonWorkInfo.SelectedPreferencesName = subIndIdName;
            JsonWorkInfo.Status = false;
            $.ajax({
                url: $_UrlSaveAutoPreferenceDetails,
                type: "Get",
                data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                success: function (result) {
                    refreshTab();
                    //BootstrapDialog.alert("Post is scheduled Successfully");
                    $('.loadercontainingdiv').hide();
                }


            })
        }
        });
        //==========================================================Save landing Pages
        $('.loadercontainingdiv').show();
            //debugger
            var tab = $("#tabs .ui-tabs-active").text().trim();
            var DayID = 0;
            if (tab == "Monday") { DayID = 1; }
            else if (tab == "Tuesday") { DayID = 2 }
            else if (tab == "Wednesday") { DayID = 3; }
            else if (tab == "Thursday") { DayID = 4; }
            else if (tab == "Friday") { DayID = 5; }
            else if (tab == "Saturday") { DayID = 6; }
            else if (tab == "Sunday") { DayID = 7; }


            var PageID = [];

            PageID.push($(".landGenCheck").attr("data-pageid"));
            if ($(".landGenCheck").is(':checked')) {
                var JsonWorkInfo = {};
                JsonWorkInfo.PageID = PageID;
                JsonWorkInfo.Status = true;
                JsonWorkInfo.DayID = DayID;
                $.ajax({
                    url: $_UrlSaveAutoLandPageDetail,
                    type: "Get",
                    data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                    success: function (result) {
                      
                        // BootstrapDialog.alert("Post is scheduled Successfully");
                    }


                })
            }
            else {
                var JsonWorkInfo = {};
                JsonWorkInfo.PageID = PageID;
                JsonWorkInfo.Status = false;
                $.ajax({
                    url: $_UrlSaveAutoLandPageDetail,
                    type: "Get",
                    data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                    success: function (result) {
                        //BootstrapDialog.alert("Post is scheduled Successfully");
                    }


                })
            }
            //==========================================================Save landing Pages END
        //// Add Time 
            //debugger;
            $('.loadercontainingdiv').show();
  
        $('.loadercontainingdiv').show();
        var tab = $("#tabs .ui-tabs-active").text().trim();
        var timeoffset = new Date().getTimezoneOffset();
        var TimeStamp = "";
        var Day = "";
        var selector = "";
        var DayName = "";
        var isValid = true;
        if (tab == "Monday") {
            TimeStamp = $('#timeDefaultSaved').val();
            Day = 1;
            selector = "#tabs-Mon";
            DayName = "Mon";
            var oldtime = $('#tabs-Mon').find('div').find('.t-value').html();
            var newTime = $('#timeDefaultSaved').val();
            if ($('#tabs-Mon').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            //if (oldtime == newTime) {
            //    BootstrapDialog.alert('Time already exists');
            //    isValid = false;
            //    $('.loadercontainingdiv').hide();
            //}
        }
        else if (tab == "Tuesday") {
            TimeStamp = $('#timeDefaultSaved').val();
            Day = 2;
            selector = "#tabs-Tue";
            DayName = "Tue";
            var oldtime = $('#tabs-Tue').find('div').find('.t-value').html();
            var newTime = $('#timeDefaultSaved').val();
            if ($('#tabs-Tue').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            //if (oldtime == newTime) {
            //    BootstrapDialog.alert('Time already exists');
            //    isValid = false;
            //    $('.loadercontainingdiv').hide();
            //}
        }
        else if (tab == "Wednesday") {
            TimeStamp = $('#timeDefaultSaved').val();
            Day = 3;
            selector = "#tabs-Wed";
            DayName = "Wed";
            var oldtime = $('#tabs-Wed').find('div').find('.t-value').html();
            var newTime = $('#timeDefaultSaved').val();
            if ($('#tabs-Wed').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            //if (oldtime == newTime) {
            //    BootstrapDialog.alert('Time already exists');
            //    isValid = false;
            //    $('.loadercontainingdiv').hide();
            //}
        }
        else if (tab == "Thursday") {
            TimeStamp = $('#timeDefaultSaved').val();
            Day = 4;
            selector = "#tabs-Thr";
            DayName = "Thr";
            var oldtime = $('#tabs-Thr').find('div').find('.t-value').html();
            var newTime = $('#timeDefaultSaved').val();
            if ($('#tabs-Thr').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            //if (oldtime == newTime) {
            //    BootstrapDialog.alert('Time already exists');
            //    isValid = false;
            //    $('.loadercontainingdiv').hide();
            //}
        }
        else if (tab == "Friday") {
            TimeStamp = $('#timeDefaultSaved').val();
            Day = 5;
            selector = "#tabs-Fri";
            DayName = "Fri";
            var oldtime = $('#tabs-Fri').find('div').find('.t-value').html();
            var newTime = $('#timeDefaultSaved').val();
            if ($('#tabs-Fri').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than three Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            //if (oldtime == newTime) {
            //    BootstrapDialog.alert('Time already exists');
            //    isValid = false;
            //    $('.loadercontainingdiv').hide();
            //}
        }
        else if (tab == "Saturday") {
            TimeStamp = $('#timeDefaultSaved').val();
            Day = 6;
            selector = "#tabs-Sat";
            DayName = "Sat";
            var oldtime = $('#tabs-Sat').find('div').find('.t-value').html();
            var newTime = $('#timeDefaultSaved').val();
            if ($('#tabs-Sat').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than two Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            //if (oldtime == newTime) {
            //    BootstrapDialog.alert('Time already exists');
            //    isValid = false;
            //    $('.loadercontainingdiv').hide();
            //}
        }
        else if (tab == "Sunday") {
            TimeStamp = $('#timeDefaultSaved').val();
            Day = 7;
            selector = "#tabs-Sun";
            DayName = "Sun";
            var oldtime = $('#tabs-Sun').find('div').find('.t-value').html();
            var newTime = $('#timeDefaultSaved').val();
            if ($('#tabs-Sun').find('div').length > 2) {
                BootstrapDialog.alert('Cannot add more than two Times For same day');
                isValid = false;
                $('.loadercontainingdiv').hide();
            }
            //if (oldtime == newTime) {
            //    BootstrapDialog.alert('Time already exists');
            //    isValid = false;
            //    $('.loadercontainingdiv').hide();
            //}
        }
        if (isValid == true) {
            $.ajax({
                url: $_UrlAddPublishingTime,
                type: "Post",
                data: { Time: TimeStamp, Day: Day, offset: timeoffset },
                success: function (result) {
                    var id = result.PublishingTimeId;
                  //  $(selector).append('<div class=" col-sm-7 predefined-time row" data-day-number="' + Day + '" data-day="' + DayName + '" data-time="' + $('#timeDefault').val() + '" data-id="' + id + '"><label class="bg-label">' + DayName + '</label><span class="t-value">' + $('#timeDefault').val() + '</span><a class="close remove-predefined-time" title="Remove time"><span id=' + id + ' class="glyphicon glyphicon-remove" onClick="remove(this)" aria-hidden="true"></span></a></div>');
                    $('.loadercontainingdiv').hide();
                }
            });
        }
        ////Add Time



        var tab = $("#tabs .ui-tabs-active").text().trim();

        if (tab == 'Thursday') {
            tab = 'Thr';
        }
        else if (tab == 'Wednesday') {
            tab = 'Wed';
        }

        else if (tab == 'Tuesday') {
            tab = 'Tue';
        }
        else if (tab == 'Saturday') {
            tab = 'Sat';
        }
        tab = tab.replace('day', '');

        tab = tab.replace('day', '');
        var time1 = $('#timeDefaultSaved').val();
       // var time1 = $('#tabs-' + tab + '').find('div:nth-child(1)').find('.t-value').html();
        var time2 = $('#tabs-' + tab + '').find('div:nth-child(2)').find('.t-value').html();
        var time3 = $('#tabs-' + tab + '').find('div:nth-child(3)').find('.t-value').html();


        if (time1 != undefined) {
            var a = [];
            $(".PreferenceCheck:checked").each(function () {
                a.push($(this).attr("data-SubIndustryId"));
            });
            if (a.length == 0) {
                var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
                BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                BootstrapDialog.show({ title: "<span>Error</span>", type: BootstrapDialog.TYPE_DANGER, message: 'Select atleast 1 preference.' });
                return false;

            }
           
                var Timearr = [];
                Timearr.push(time1);
                if (time2 != undefined) {                   
                        Timearr.push(time2);
                    }                
                if (time3 != undefined) {
                    Timearr.push(time3);
                }
                var JsonWorkInfo = {};
           
                JsonWorkInfo.Time = Timearr;
                // Get selected dropdown value
                JsonWorkInfo.PostType = 2;
                JsonWorkInfo.Day = tab;              
                JsonWorkInfo.SelectedPreferences = a;              
                JsonWorkInfo.timeoffset = new Date().getTimezoneOffset();
                $.ajax({
                    url: $_UrlSavePostDetails,
                    type: "Post",
                    data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                    success: function (result) {                    
                        refreshTab();                      
                        $('.loadercontainingdiv').hide();

                        //var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
                        //BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                        //BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_SUCESS, message: 'Post set successfully for selected preferences.' });         
                       
                      //  BootstrapDialog.alert("Post is scheduled Successfully");
                    }
             

            })
        }
        else {
            $('.loadercontainingdiv').hide();
          //  BootstrapDialog.alert("Please add atleast one time")
        }
    });


});



window.operateEvents = {
    'click #read': function (e, value, row, index) {
        BootstrapDialog.show({
            title: 'Title',
            message: row.Title,
            buttons: [{
                label: 'ok',
                cssClass: 'btn-primary',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }],
        });

    },

    'click #postnowcontent': function (e, value, row, index) {
        //debugger;
        Dashboards.BindDataMethods.CreateJsonForRePostDetails(e, value, row, index);
    },

}


function remove(obj) {
    //debugger;
    var id = obj.attributes[0].value;
    $.ajax({
        url: $_UrlDeletePublishingTime,
        type: "Get",
        data: { id: id },
        success: function (result) {
            if (result == "true") {
                obj.closest('div').remove();
            }
        }
    });
}

function refreshTab ()
{
 
    debugger;
    var current_index = $("#tabs").tabs("option", "active")+1;

    var select = $("#ui-id-" + current_index + "").attr('href');
            var Day = "";
            var dayName = "";
           // var select = ui.newPanel.selector;
            if (select == "#tabs-Mon") {
                Day = 1;
                dayName = "Mon";
                $("#tabs-Mon").html("");
            }
            else if (select == "#tabs-Tue") {
                Day = 2;
                dayName = "Tue";
                $("#tabs-Tue").html("");
            }
            else if (select == "#tabs-Wed") {
                Day = 3;
                dayName = "Wed";
                $("#tabs-Wed").html("");
            }
            else if (select == "#tabs-Thr") {
                Day = 4;
                dayName = "Thr";
                $("#tabs-Thr").html("");
            }
            else if (select == "#tabs-Fri") {
                Day = 5;
                dayName = "Fri";
                $("#tabs-Fri").html("");
            }
            else if (select == "#tabs-Sat") {
                Day = 6;
                dayName = "Sat";
                $("#tabs-Sat").html("");
            }
            else if (select == "#tabs-Sun") {
                Day = 7;
                dayName = "Sun";
                $("#tabs-Sun").html("");
            }


            var tab = $("#tabs .ui-tabs-active").text().trim();
            JsonWorkInfo = {};
            JsonWorkInfo.DayID = Day;
            $.ajax({
                //// bind landing PAges
                url: $_GetLeadManage,
                type: "GET",
                data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                success: function (result) {
                    //debugger
                    $("#ManageLeads").html(result);

                    $.ajax({
                        //// bind ManageAutoPublishing PAges
                        url: $_GetContentPreference,
                        type: "GET",
                        data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                        success: function (result) {
                            //debugger
                            $("#ManageAutoPublishing").html(result);

                            $.ajax({
                                //// bind saved landing PAges
                                url: $_GetSavedLeadManage,
                                type: "GET",
                                data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                                success: function (result) {
                                    //debugger
                                    $("#SavedManageLeads").html(result);
                                    $.ajax({
                                        //// bind saved ManageAutoPublishing PAges
                                        url: $_GetSavedContentPreference,
                                        type: "GET",
                                        data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                                        success: function (result) {
                                            //debugger
                                            $("#SavedManageAutoPublishing").html(result);
                                            $.ajax({
                                                url: $_UrlGetPublishingTimeByDay,
                                                type: "Get",
                                                data: { Day: Day },
                                                success: function (result) {

                                                    if (result.length == 0) {
                                                        $("#savedPreferenceData").hide();
                                                        $("#PreferenceData").show();

                                                    }
                                                    else {
                                                        $("#timeDefault").val(result[0].Time);
                                                        $("#hdtimeDefaultSaved").val(result[0].Time);
                                                        $("#savedPreferenceData").show();
                                                        $("#PreferenceData").hide();
                                                        //  $(select).append('<div class="bind-main"><div class="input-append bootstrap-timepicker-component custom-time-pick col-lg-3 col-md-3 col-sm-3 col-xs-12 mrgn-tp"> <input type="text" class="timepicker-default input-small" value =' + result[0].Time + ' id="timeDefault">                                <span class="add-onon" style="margin-left: -40px">                                    <i class="icon-time"></i>                                </span>                            </div>                                                      </div>')
                                                        $.each(result, function (i, val) {
                                                            //debugger;                       /// $(select).append('<div class="col-sm-6 predefined-time row" data-day-number="' + Day + '" data-day="' + dayName + '" data-time="' + val.Time + '" data-id="15453295"><label class="bg-label">' + dayName + '</label><span class="t-value">' + val.Time + '</span><a class="close remove-predefined-time" title="Remove time"><span id=' + val.PublishingTimeId + ' class="glyphicon glyphicon-remove" onClick="remove(this)" aria-hidden="true"></span></a></div>');
                                                        });
                                                    }

                                                }
                                            });
                                        }
                                    });
                                }
                            });
                        }
                    });
                }
            });



        
        

 
}




