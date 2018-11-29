
function PostNow(item, contentId, title) {
    ////debugger;

    var d = new Date();
    console.log('calendar 1 eventReceive');
    //makeEventsDraggable();
    // console.log(event);
    //var id = event.title.split('/')[0];
    var evntId = "_fc7";
    //var formDate = $.fullCalendar.formatDate(event.start._d, 'MM-dd-yyyy');
    // var starts = moment(event.start._d).format('YYYY/MM/DD hh:mm:ss tt');
    //var start = moment(event.start._d).format('YYYY/MM/DD');
    //  var tme = timeSchedule;
    // var time = start + " " + tme;
    var offset = d.getTimezoneOffset();
    //var TimeStamp = timeSchedule;
    //var tm = getTwentyFourHourTime(TimeStamp);
    var currentdate = new Date();
    //var hrs = currentdate.getHours();
    //var min = currentdate.getMinutes();
    //var time2 = hrs + ":" + min;
    //var stt = new Date("November 13, 2013 " + tm);
    //stt = stt.getTime();
    //var endt = new Date("November 13, 2013 " + time2);
    //endt = endt.getTime();
    //var datetoPost = start;
    //var today = moment().format('YYYY/MM/DD');
    var unique = [];
    var od = $(item).parents('.fc-event').find('div.text-center').find('input');
    $.each(od, function (i, val) {
        if (val.checked) {
            var text = val.value;
            unique.push(text);
        }
    });
    var arr = [];
    for (var i = 0 ; i < unique.length ; ++i) {
        if (arr.indexOf(unique[i]) == -1)
            arr.push(unique[i]);
    }


    var time = moment(currentdate).format('YYYY/MM/DD hh:mm:ss');

    var newarr = arr.toString();

    ////debugger
  
    $('.loadercontainingdiv').show();
    $.ajax({
        url: $_UrlPostNowSaveCalender,
        type: "Get",
        data: { id: contentId, title: title, date: time, offset: offset, socialarr: newarr, evntId: evntId },
        //async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
            ////debugger;
            $('.loadercontainingdiv').hide();
            BootstrapDialog.alert("Content Posted Successfully");
            $('#calendar1').fullCalendar('refetchEvents');

        }
    });
    $('.loadercontainingdiv').hide();







}


function remove(obj, id) {
    ////debugger;
    var objct = obj;
    BootstrapDialog.show({
        title: 'Confirmation',
        message: 'Are you sure you want to remove this content?',
        buttons: [{
            label: 'No',
            cssClass: 'btn-danger',
            action: function (dialogItself) {
                ////debugger;
                dialogItself.close();
            }
        },
        {
            label: 'Yes',
            cssClass: 'btn-primary',
            action: function (dialogItself) {
                ////debugger;
                $.ajax({
                    url: $_UrlUpdateContentStatusOnCalender,
                    type: "Get",
                    data: { id: id },
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        ////debugger;
                        dialogItself.close();
                        objct.parentElement.parentElement.parentElement.remove();
                    }
                });
            }
        }]
    });
}


function DeleteEvent(obj) {
    ////debugger;
    $(".tooltiptopicevent").hide();
    //var evnId=  obj.parentNode.firstElementChild.firstElementChild.lastElementChild.lastElementChild.innerText;
    var evnId = obj;
    BootstrapDialog.show({
        title: 'Confirmation',
        message: 'Are you sure you want to delete this event?',
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
                ////debugger;
                $.ajax({
                    cache: false,
                    async: true,
                    type: "POST",
                    url: $_UrlDeleteScheduleEvents,
                    data: { id: evnId },
                    success: function (data) {
                        ////debugger;
                        if (data == "true") {
                            dialogItself.close();
                            $('#calendar1').fullCalendar('refetchEvents');
                        }
                        else {
                            //ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.');
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
        }

        ]
    });
}


function EditTime(obj) {
    $(".tooltiptopicevent").hide();
    ////debugger;
    var eventId = $("#id").val();
    var eventdate = $("#date").val();
    var time = obj.innerText;
    var neweventdate = eventdate + " " + time;
    var currentDate = moment().format('YYYY/MM/DD hh:mm:ss');
    var UTCstring = (new Date()).toUTCString();
    $.ajax({
        url: $_UrlGetContentByEventId,
        type: "Get",
        data: { eventid: eventId },
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            ////debugger;
            if (data.social.IsPosted == true) {
                BootstrapDialog.alert("Cannot edit posted events!");
            }
            else {
                var today = moment().format('YYYY/MM/DD');
                if (eventdate < today) {
                    BootstrapDialog.alert("Cannot schedule events on past date");
                }
                else {
                    $("#eventAdd").html('');
                    time = obj.innerText;
                    var ampm = "";
                    $("#eventContent").dialog({ modal: true, title: 'Edit Time', width: 300, id: 'dialog' });
                    if (time.includes('p')) {
                        pm = "PM";
                      time=  time.replace("pm", "PM")
                    }
                    else {
                        //am = "AM";
                      time=  time.replace("am", "AM")
                    }
                   // var newTime = time.substring(0, 6) + " " + ampm;
                    $("#eventContent").find('#timeDefaultevent').val(time);
                }
            }
        }
    });
}

$(document).ready(function () {
    ////debugger;
    //$('.fc-event-container').qtip({
    //    content: "Click to view content",
    //    show: 'mouseover',
    //    hide: 'mouseout'
    //});

    var id = "";
    var date = "";
    var title = "";
    var arr = [];
    var timeSchedule = "";
    $('.timepicker-default').timepicker({
        timeFormat: 'h:mm:ss p'
    });

    function getTwentyFourHourTime(amPmString) {
        ////debugger;
        var d = new Date("1/1/2013 " + amPmString);
        return d.getHours() + ':' + d.getMinutes();
    }

    $("#btnadd").click(function () {
        ////debugger;
        $('.loadercontainingdiv').show();
        var timeoffset = new Date().getTimezoneOffset();
        var TimeStamp = $('#timeDefaultevent').val();
        var tm = getTwentyFourHourTime(TimeStamp);
        var currentdate = new Date();
        var hrs = currentdate.getHours();
        var min = currentdate.getMinutes();
        var time2 = hrs + ":" + min;
        var stt = new Date("November 13, 2013 " + tm);
        stt = stt.getTime();
        var endt = new Date("November 13, 2013 " + time2);
        endt = endt.getTime();
        var res = "";
        var datetoPost = $("#eventContent").find("#date").val();
        var today = moment().format('YYYY/MM/DD');
        if (today == datetoPost) {
            if (stt > endt) {
                ////debugger;
                if ($('#eventAdd').html() == "") {
                    var d = new Date();
                    var offset = d.getTimezoneOffset();
                    var id = $("#eventContent").find("#id").val();
                    var title = $("#eventContent").find("#title").val();
                    var date = $("#eventContent").find("#date").val();
                    var tm = $('#timeDefaultevent').val();
                    var time = date + " " + tm;
                    $.ajax({
                        url: $_UrlUpdateScheduleEvents,
                        type: "Get",
                        data: { id: id, title: title, date: time, offset: offset },
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            $('#eventContent').dialog('close');
                            $('#calendar1').fullCalendar('refetchEvents');
                            ////debugger;
                        }
                    });
                }
                else {
                    $('#eventAdd').find('.predefined-time').each(function () {
                        ////debugger;
                        var old = $(this).find('.t-value').html();
                        var ntime = "";
                        var newtimenew = "";
                        var newTime = $('#timeDefault').val();
                        if (newTime.split(':')[0].length == 1) {
                            ////debugger;
                            ntime = 0 + '' + newTime.split(':')[0];
                            newTime = ntime + ":" + newTime.split(':')[1];
                        }
                        if (old == newTime) {
                            res = "exists";
                        }
                    });
                    if (res == "exists") {
                        BootstrapDialog.alert("Time already exists");
                    }
                    else {
                        ////debugger;
                        var d = new Date();
                        var offset = d.getTimezoneOffset();
                        var id = $("#eventContent").find("#id").val();
                        var title = $("#eventContent").find("#title").val();
                        var date = $("#eventContent").find("#date").val();
                        var tm = $('#timeDefaultevent').val();
                        var time = date + " " + tm;
                        $.ajax({
                            url: $_UrlUpdateScheduleEvents,
                            type: "Get",
                            data: { id: id, title: title, date: time, offset: offset },
                            async: false,
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                $('#eventContent').dialog('close');
                                $('#calendar1').fullCalendar('refetchEvents');
                            }
                        });
                    }
                }
            }
            else {
                BootstrapDialog.alert("Cannot add past time");
            }
        }
        else {
            $('#eventAdd').find('.predefined-time').each(function () {
                ////debugger;
                var old = $(this).find('.t-value').html();
                var ntime = "";
                var newtimenew = "";
                var newTime = $('#timeDefault').val();
                if (newTime.split(':')[0].length == 1) {
                    ////debugger;
                    ntime = 0 + '' + newTime.split(':')[0];
                    newTime = ntime + ":" + newTime.split(':')[1];
                }
                if (old == newTime) {
                    res = "exists";
                }
            });
            if (res == "exists") {
                BootstrapDialog.alert("Time already exists")
            }
            else {
                ////debugger;
                var d = new Date();
                var offset = d.getTimezoneOffset();
                var id = $("#eventContent").find("#id").val();
                var title = $("#eventContent").find("#title").val();
                var date = $("#eventContent").find("#date").val();
                var tm = $('#timeDefaultevent').val();
                var time = date + " " + tm;
                $.ajax({
                    url: $_UrlUpdateScheduleEvents,
                    type: "Get",
                    data: { id: id, title: title, date: time, offset: offset },
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        $('#eventContent').dialog('close');
                        $('#calendar1').fullCalendar('refetchEvents');
                    }
                });
            }
        }
        $('.loadercontainingdiv').hide();
    }
);





    $("#btnPost").click(function () {
        ////debugger;
        var EventId = $("#eventContent").find("#id").val();
        $.ajax({
            url: $_UrlGetContentByEventId,
            type: "Get",
            data: { eventid: EventId },
            success: function (data) {
                ////debugger;
                var arr = [];
                var arrayIds = [];
                if (data.social.IsFacebook == true) {
                    arrayIds.push(0);
                }
                if (data.social.IsTwitter == true) {
                    arrayIds.push(1);
                }
                if (data.social.IsLinkedIn == true) {
                    arrayIds.push(2);
                }
                arr.push(data.status.ImageUrl);
                var JsonWorkInfo = {};
                JsonWorkInfo.TextMessage = data.status.Description;
                JsonWorkInfo.Ids = arrayIds;
                JsonWorkInfo.ImageArray = arr;
                JsonWorkInfo.timeoffset = new Date().getTimezoneOffset();
                JsonWorkInfo.Title = data.status.Title;
                JsonWorkInfo.Heading = data.status.Heading;
                JsonWorkInfo.Link = data.status.Url;
                JsonWorkInfo.PostType = 0;
                $.ajax({
                    url: $_PostDetails,
                    type: "POST",
                    data: { "PostInformation": JSON.stringify(JsonWorkInfo) },
                    success: function (result) {
                        ////debugger;
                        var popTimer = parseInt($("#hdnPopUpTimer").val());
                        setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
                        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                        BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_SUCCESS, message: 'Content Posted Successfully.' });
                        //  BootstrapDialog.alert("Content Posted Successfully");
                    },
                    error: function (result) {
                        var popTimer = parseInt($("#hdnPopUpTimer").val()); setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});", popTimer);
                        BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
                        BootstrapDialog.show({ title: "<span>Information</span>", type: BootstrapDialog.TYPE_SUCCESS, message: 'Some error occured.' });
                        //  BootstrapDialog.alert("Some error occured");
                    }
                });
            }
        });
    });

    // Manage status of dragging event and calendar
    var calEventStatus = [];


    /* Required functions */

    var isEventOverDiv = function (x, y) {

        var external_events = $('#external-events');
        var offset = external_events.offset();
        offset.right = external_events.width() + offset.left;
        offset.bottom = external_events.height() + offset.top;

        // Compare
        if (x >= offset.left
            && y >= offset.top
            && x <= offset.right
            && y <= offset.bottom) { return true; }
        return false;

    }

    function makeEventsDraggable(obj) {
        $('.fc-draggable').each(function () {
            ////debugger;
            // store data so the calendar knows to render an event upon drop
            var i = 0;
            $(this).data('event', {
                title: $.trim($(this).text()),
                image: obj,
                // use the element's text as the event title
                stick: true // maintain when user navigates (see docs on the renderEvent method)
            });

            //if (i == 0 && obj != undefined) {
            //        $(this).find('.fc-content').append('<div><img src=' + obj + ' style="height: 55px;width: 123px;"></div>');
            //}

            // make the event draggable using jQuery UI
            $(this).draggable({

                zIndex: 999,
                revert: true,      // will cause the event to go back to its
                revertDuration: 0  //  original position after the drag
            });

            console.log('makeEventsDraggable');

            // Dirty fix to remove highlighted blue background
            $("td").removeClass("fc-highlight");
            i++;
        });

    }



    /* initialize the external events
    -----------------------------------------------------------------*/

    $('#external-events .fc-event').each(function () {
        ////debugger;
        // store data so the calendar knows to render an event upon drop
        $(this).data('event', {
            title: $.trim($(this).text()),
            // use the element's text as the event title
            stick: true // maintain when user navigates (see docs on the renderEvent method)
        });

        // make the event draggable using jQuery UI
        $(this).draggable({
            zIndex: 999,
            revert: true,      // will cause the event to go back to its
            revertDuration: 0  //  original position after the drag
        });
    });

    var tooltip = $('<div/>').qtip({
        id: 'calendar1',
        prerender: true,
        content: {
            text: ' ',
            title: {
                button: 'Close'
            }
        },
        position: {
            my: 'bottom center',
            at: 'top center',
            target: 'mouse',
            viewport: $('#calendar1'),
            adjust: {
                mouse: false,
                scroll: false
            }
        },
        show: false,
        hide: false,
        style: 'qtip-light'
    }).qtip('api');

    //  var Custom = '<div class="row">                <div class="col-sm-12">                    <i class="fa fa-circle" style="color: rgb(142, 118, 194)" aria-hidden="true"></i> Posted Events                </div>                <div class="col-sm-12">                    <i class="fa fa-circle" style="color: rgb(106, 164, 193)" aria-hidden="true"></i> Scheduled Events                </div>            </div>';

    /* initialize the calendar1
    -----------------------------------------------------------------*/

    $('#calendar1').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: '',

        },


        //eventRender: function (event, element) {
        //    ////debugger;
        //    if (event.title) {
        //        element.find('.fc-event-inner')
        //            .append("<div class='new'>" + event.title) + "</div>";
        //    }
        //   // element.addClass(event.class)
        //},

        //select: function(start, end, jsEvent, view) {
        //   alert(start.format("MM/DD/YYYY hh:mm a") + " to " + end.format("MM/DD/YYYY h\h:mm a") + " in view " + view.name);
        //     },


        //eventRender: function (event, element) {
        //    ////debugger;
        //    element.attr('href', 'javascript:void(0);');
        //    element.click(function() {
        //        ////debugger;

        //        $("#eventContent").dialog({ modal: true, title: 'Schedule Event', width: 300 });




        //    });
        //},

        //eventClick: function (data, event, view) {
        //    ////debugger;
        //    $("#eventAdd").html('');
        //    id = data.id;
        //    date = moment(data.start._d).format('YYYY/MM/DD');
        //    title = data.title;
        //    $("#id").val(id);
        //    $("#title").val(title);
        //    $("#date").val(date);
        //    var d = moment(data.start._d).format('YYYY/MM/DD'); ;
        //    var now = moment().format('YYYY/MM/DD'); ;
        //    if (d < now) {
        //        ////debugger;
        //        BootstrapDialog.alert("Cannot schedule event for past date.");
        //    }
        //    else {
        //        $.ajax({
        //            url: $_UrlEventBydate,
        //            type: "Get",
        //            data: { date: date },
        //            async: false,
        //            contentType: "application/json; charset=utf-8",
        //            success: function (data) {
        //                ////debugger;
        //                $.each(data, function (key, value) {
        //                    ////debugger;
        //                    var timestamp = value.LocalTime;
        //                    var str = moment(timestamp).format("hh:mm");
        //                    var strs = moment(timestamp).format("HH:mm");
        //                    ////debugger;
        //                    var nestr = strs.split(':')[0];
        //                    var ampm = "";
        //                    if (nestr > 12) {
        //                        ampm = "PM";
        //                    }
        //                    else {
        //                        ampm = "AM";
        //                    }
        //                    var tt = str + " " + ampm;
        //                    $("#eventAdd").append('<div class="predefined-time"  data-time="' + tt + '" ><label class="bg-label"></label><span class="t-value">' + tt + '</span><a class="remove-predefined-time"><span id=class="glyphicon glyphicon-remove" onClick="remove(this)" aria-hidden="true"></span></a></div>');
        //                });
        //            }
        //        });
        //        $("#eventContent").dialog({ modal: true, title: 'Schedule Event', width: 300, id: 'dialog' });
        //    }
        //},

        eventMouseover: function (event, jsEvent, view) {
            var contentId = event.id;
            if (contentId == undefined) {
                var id = event.id;
                var date = moment(event.start._d).format('YYYY/MM/DD');
                var title = event.title;
                $("#id").val(id);
                $("#title").val(title);
                $("#date").val(date);
                contentId = event.title.split('/')[0];
                $.ajax({
                    url: $_UrlGetContentByNewId,
                    type: "Get",
                    data: { id: contentId, eventid: event._id },
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var media = "";
                        if (data.social.IsFacebook == true) {
                            media = media + '<span class="socialmediaicon"><i class="fa fa-facebook" aria-hidden="true"></i></span>';
                        }
                        if (data.social.IsTwitter == true) {
                            media = media + '<span class="socialmediaicon"><i class="fa fa-twitter" aria-hidden="true"></i></span>';
                        }
                        if (data.social.IsLinkedIn == true) {
                            media = media + '<span class="socialmediaicon"><i class="fa fa-linkedin" aria-hidden="true"></i></span>';
                        }


                        var url = "";
                        if (data.status.ImageUrl.indexOf("www") > -1 || data.status.ImageUrl.indexOf("http") > -1) {
                            url = data.status.ImageUrl
                        }
                        else {
                            url = $_BaseUrl + data.status.ImageUrl
                        }
                        //$(element).popover({ title: '<h6 style="background: #4f77bc;text-align: center;color: #fff;padding: 5px 0px;"> ' + data.status.Title + ' </h6><div class="social_img"><img src=' + $_BaseUrl + data.status.ImageUrl + ' width="100%" height="140px" ></div><p style="font-size: 12px;padding: 5px 0px;line-height: 20px;"> Description: ' + ': ' + data.status.Description + '</p><span>' + media + '</span>', container: "body", trigger: "hover", placement: "bottom", html: true, });
                        tooltip = '<div class="tooltiptopicevent" style="width:20%;height:auto;background:#fff;position:absolute;z-index:10001;padding:0px;line-height: 200%;top: 242px;left: 667px; opacity: 1.9; border: 1px solid #ccc;"> <h6 style="background: #4f77bc;text-align: center;color: #fff;padding: 5px;"> ' + data.status.Title + ' </h6><div class="social_img"><img src=' + url + ' width="100%" height="140px" ></div><p style="font-size: 12px;padding: 0 5px;line-height: 20px;"> Description: ' + ': ' + data.status.Description + '</p><span>' + media + '</span></br></div>';
                        $("body").append(tooltip);
                    }
                });
            }
            else {
                var id = event.id;
                var date = moment(event.start._d).format('YYYY/MM/DD');
                var title = event.title;
                $("#id").val(id);
                $("#title").val(title);
                $("#date").val(date);
                $.ajax({
                    url: $_UrlGetId,
                    type: "Get",
                    data: { contentId: contentId },
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        var id = result.ContentId;
                        $.ajax({
                            url: $_UrlGetContentById,
                            type: "Get",
                            data: { id: id, eventid: event.id },
                            async: false,
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                //debugger

                                var media = "";
                                if (data.social.IsFacebook == true) {
                                    media = media + '<span class="socialmediaicon"><i class="fa fa-facebook" aria-hidden="true"></i></span>';
                                }
                                if (data.social.IsTwitter == true) {
                                    media = media + '<span class="socialmediaicon"><i class="fa fa-twitter" aria-hidden="true"></i></span>';
                                }
                                if (data.social.IsLinkedIn == true) {
                                    media = media + '<span class="socialmediaicon"><i class="fa fa-linkedin" aria-hidden="true"></i></span>';
                                }
                                var url = "";
                                var parsedURL = UrlParser(data.status.ImageUrl)
                                if (parsedURL.origin == undefined || parsedURL.origin == null) {
                                    url = $_BaseUrl + data.status.ImageUrl

                                }
                                else {
                                    url = data.status.ImageUrl
                                }

                                //$(element).popover({ title: '<h6 style="background: #4f77bc;text-align: center;color: #fff;padding: 5px 0px;"> ' + data.status.Title + ' </h6><div class="social_img"><img src=' + $_BaseUrl + data.status.ImageUrl + ' width="100%" height="140px" ></div><p style="font-size: 12px;padding: 5px 0px;line-height: 20px;"> Description: ' + ': ' + data.status.Description + '</p><span>' + media + '</span>', container: "body", trigger: "hover", placement: "bottom", html: true, });
                                tooltip = '<div class="tooltiptopicevent" style="width:20%;height:auto;background:#fff;position:absolute;z-index:10001;padding:0px;line-height: 200%;top: 242px;left: 667px; opacity: 1.9; border: 1px solid #ccc;"> <h6 style="background: #4f77bc;text-align: center;color: #fff;padding: 5px;"> ' + data.status.Title + ' </h6><div class="social_img"><img src=' + url + ' width="100%" height="140px" ></div><p style="font-size: 12px;padding: 0 5px;line-height: 20px;">' + data.status.Description + '</p><span>' + media + '</span></br></div>';
                                $("body").append(tooltip);
                            }
                        });
                    }
                });
            }

            $(this).mouseover(function (e) {
                $(this).css('z-index', 10000);
                $('.tooltiptopicevent').fadeIn('500');
                $('.tooltiptopicevent').fadeTo('10', 1.9);
            }).mousemove(function (e) {
                $('.tooltiptopicevent').css('top', e.pageY + 10);
                $('.tooltiptopicevent').css('left', e.pageX + 20);
            });
        },





        //eventMouseover: function(event, jsEvent, view) {
        //    $(jsEvent.target).attr('title', "Click to view Event");
        //    //$(jsEvent.target).qtip({
        //    //    overwrite: true,
        //    //    content: "Click to view content",
        //    //    show: 'mouseover',
        //    //    hide: 'mouseout',
        //    //})
        //},

        //eventMouseover:function(data, event, view){
        //    ////debugger;
        //    $("body").append("<div title='Expand Event'></div>");
        //},

        //eventClick: function (data, event, view) {
        //    ////debugger;
        //    var evnId = data.id;
        //    BootstrapDialog.show({
        //        title: 'Confirmation',
        //        message: 'Are you sure you want to delete this event?',
        //        buttons: [{
        //            label: 'No',
        //            cssClass: 'btn-danger',
        //            action: function (dialogItself) {
        //                dialogItself.close();
        //            }
        //        },
        //        {
        //            label: 'Yes',
        //            cssClass: 'btn-primary',
        //            action: function (dialogItself) {
        //                ////debugger;
        //                $.ajax({
        //                    cache: false,
        //                    async: true,
        //                    type: "POST",
        //                    url: $_UrlDeleteScheduleEvents,
        //                    data: { id: data.id },
        //                    success: function (data) {
        //                        ////debugger;
        //                        if (data == "true") {
        //                            dialogItself.close();
        //                            $('#calendar1').fullCalendar('removeEvents', evnId);
        //                        }
        //                        else {
        //                            //ShowMessage(BootstrapDialog.TYPE_DANGER, 'Error occured.');
        //                        }
        //                    },
        //                    error: function (request, error) {
        //                        if (request.statusText == "CustomMessage") {
        //                            $("#spanError").html(request.responseText)
        //                        }
        //                    },
        //                    headers: {
        //                        'RequestVerificationToken': $("#TokenValue").val()
        //                    }
        //                });
        //            }
        //        }]
        //    });
        //},


        eventMouseout: function (data, event, view) {
            $(this).css('z-index', 8);
            $('.tooltiptopicevent').remove();
        },

        dayClick: function () {
            // tooltip.hide()
        },

        editable: true,
        droppable: true, // this allows things to be dropped onto the calendar
        dragRevertDuration: 0,
        disableDragging: false,
        eventLimit: true, // allow "more" link when too many events
        defaultDate: new Date(),
        slotDuration: '00:30:00',
        slotLabelFormat: 'h:mm a',
        slotLabelInterval: 30,
        timezone: 'local',
        events: $_UrlGetcalenderEvents,

        drop: function (date, jsEvent, ui) {
            ////debugger;
            console.log('calendar 1 drop'); console.log(date); console.log(jsEvent); console.log(ui); console.log(this);
            // is the "remove after drop" checkbox checked?
            if ($('#drop-remove').is(':checked')) {
                // if so, remove the element from the "Draggable Events" list
                $(this).remove();
            }

            // if event dropped from another calendar, remove from that calendar
            if (typeof calEventStatus['calendar'] != 'undefined') {
                $(calEventStatus['calendar']).fullCalendar('removeEvents', calEventStatus['event_id']);
                //$(calEventStatus['calendar']).fullCalendar('unselect');
            }
            var img = $(this).find('img').attr('src');
            //makeEventsDraggable(img);
            var od = $(this).find('div.text-center').find('input');
            $.each(od, function (i, val) {
                if (val.checked) {
                    var text = val.value;
                    arr.push(text);
                }
            });
            timeSchedule = $(this).find('div.time').find('#timeDefault').val();
        },

        eventConstraint: {
            start: moment().format('YYYY-MM-DD'),
            end: "2100-01-01" // hard coded goodness unfortunately
        },


        eventReceive: function (event, delta, revertFunc, jsEvent, ui, view) {
             //debugger;
            var d = new Date();
            console.log('calendar 1 eventReceive');
            //makeEventsDraggable();
            // console.log(event);
            var id = event.title.split('/')[0];
            var GroupId = event.title.split('/')[1];
            var evntId = event._id;
            //var formDate = $.fullCalendar.formatDate(event.start._d, 'MM-dd-yyyy');
            var starts = moment(event.start._d).format('YYYY/MM/DD hh:mm:ss tt');
            var start = moment(event.start._d).format('YYYY/MM/DD');
            var tme = timeSchedule;
            var time = start + " " + tme;
            var offset = d.getTimezoneOffset();
            var TimeStamp = timeSchedule;
            var tm = getTwentyFourHourTime(TimeStamp);
            var currentdate = new Date();
            var hrs = currentdate.getHours();
            var min = currentdate.getMinutes();
            var time2 = hrs + ":" + min;
            var stt = new Date("November 13, 2013 " + tm);
            stt = stt.getTime();
            var endt = new Date("November 13, 2013 " + time2);
            endt = endt.getTime();
            var datetoPost = start;
            var today = moment().format('YYYY/MM/DD');

            if (datetoPost == today) {
                ////debugger
                if (stt > endt) {
                    var newarr = arr.toString();
                    $.ajax({
                        url: $_UrlSavecalenderEvents,
                        type: "Get",
                        data: { id: id, GroupId: GroupId, title: event.title, date: time, offset: offset, socialarr: newarr, evntId: evntId },
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            ////debugger;
                            arr = [];
                            $('#calendar1').fullCalendar('refetchEvents');
                            $("#toggerOldNew").change();
                            $("#toggerOldNew").change();

                        }
                    });
                }
                else {
                    BootstrapDialog.alert("Cannot schedule events on past time");
                }
            }
                //alert(formDate);
            else {
                var newarr = arr.toString();
                $.ajax({
                    url: $_UrlSavecalenderEvents,
                    type: "Get",
                    data: { id: id, GroupId: GroupId, title: event.title, date: time, offset: offset, socialarr: newarr, evntId: evntId },
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        ////debugger;
                        arr = [];
                        $("#toggerOldNew").change();
                        $("#toggerOldNew").change();
                        //$('#calendar1').fullCalendar('removeEventSource', event);
                        //$('#calendar1').fullCalendar('addEventSource', event);
                        //$('#calendar1').fullCalendar('rerenderEvents');
                        //$('#calendar1').fullCalendar('refetchEvents');
                        //alert("true");
                        $('#calendar1').fullCalendar('refetchEvents');
                    }
                });
            }
        },

        eventAfterRender: function (event, element, view) {
            debugger;
            if (event.title.includes('/')) {
                element.hide();
            }
            var dataHoje = new Date();
            var daysClass = ['fc-sun', 'fc-mon', 'fc-tue', 'fc-wed', 'fc-thu', 'fc-fri', 'fc-sat'];
            var daysNames = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
            var d = new Date(event.start._i);
            var daysClassnm = daysClass[d.getDay()];
            var daysName = daysNames[d.getDay()];

            if (dataHoje < event.start._d) {
                if (event.contentPageID==2) {
                    element.css('background-color', '#4fa235');
                    $("th.fc-day-header.fc-widget-header." + daysClassnm + "").css('background-color', '#4fa235');
                    $("th.fc-day-header.fc-widget-header." + daysClassnm + "").html('' + daysName + '<span class=""> <i class="icon-time"></i></span>')
                }
                else {
                    element.css('background-color', '#6AA4C1');
                }
               
            }
            else {
                element.css('background-color', '#8E76C2');
            }
        },



        //eventDrop: function (event, delta, revertFunc, jsEvent, ui, view) {
        //    ////debugger;
        //    console.log('calendar 1 eventDrop');
        //    makeEventsDraggable();
        //    //alert("event drop");
        //},

        eventDragStart: function (event, delta, jsEvent, ui, view) {
            ////debugger;
            console.log(event); console.log(jsEvent); console.log(ui); console.log(view);
            // Add dragging event in global var
            calEventStatus['calendar'] = '#calendar1';
            calEventStatus['event_id'] = event._id;
            console.log('calendar 1 eventDragStart');
        },

        eventDrop: function (event, delta, revertFunc) {
            ////debugger;
            // alert(event.title + " was dropped on " + event.start.format());
            ////debugger;
            console.log('calendar 1 eventDragStop');
            var d = new Date();
            console.log('calendar 1 eventReceive');
            //makeEventsDraggable();
            console.log(event);
            var id = event.id;
            var offset = d.getTimezoneOffset();
            // defaultDuration = moment.duration(defaultDuration);
            //var formDate = $.fullCalendar.formatDate(event.start._d, 'MM-dd-yyyy');
            var start = moment(event.start._d).format('YYYY/MM/DD hh:mm');
            var startdate = new Date(event.start._i);
            var str = moment(delta.timeStamp).format("YYYY/MM/DD hh:mm");

            BootstrapDialog.show({
                title: 'Confirmation',
                message: 'You want to copy or move the post?',
                buttons: [{
                    label: 'Copy',
                    cssClass: 'btn-info',
                    action: function (dialogItself) {
                        dialogItself.close();
                        var Ismove = false;
                        $.ajax({
                            url: $_UrlSaveCalenderEventsDrag,
                            type: "Get",
                            data: { id: id, title: event.title, date: start, offset: offset, Ismove: Ismove },
                            async: false,
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                ////debugger;
                                dialogItself.close();
                                $('#calendar1').fullCalendar('refetchEvents');
                            }
                        });
                    }
                },
                {
                    label: 'Move',
                    cssClass: 'btn-primary',
                    action: function (dialogItself) {
                        var Ismove = true;
                        ////debugger;
                        $('.loadercontainingdiv').show();
                        $.ajax({
                            url: $_UrlSaveCalenderEventsDrag,
                            type: "Get",
                            data: { id: id, title: event.title, date: start, offset: offset, Ismove: Ismove },
                            async: false,
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                ////debugger;
                                dialogItself.close();
                                $('#calendar1').fullCalendar('refetchEvents');
                            }
                        });
                        $('.loadercontainingdiv').hide();
                    }
                }]
            })
        },

        //eventRender: function (event, element) {
        //    var contentId = event.id;
        //    $.ajax({
        //        url: $_UrlGetId,
        //        type: "Get",
        //        data: { contentId: contentId },
        //        async: false,
        //        contentType: "application/json; charset=utf-8",
        //        success: function (result) {
        //            var id = result.ContentId;
        //            $.ajax({
        //                url: $_UrlGetContentById,
        //                type: "Get",
        //                data: { id: id, eventid: event.id },
        //                async: false,
        //                contentType: "application/json; charset=utf-8",
        //                success: function (data) {
        //                    var media = "";
        //                        //if (val.IsFacebook == true) {
        //                    if (data.social.IsFacebook == true) {
        //                                media = media + '<span class="socialmediaicon"><i class="fa fa-facebook" aria-hidden="true"></i></span>';
        //                            }
        //                    if (data.social.IsTwitter == true) {
        //                                media = media + '<span class="socialmediaicon"><i class="fa fa-twitter" aria-hidden="true"></i></span>';
        //                            }
        //                    if (data.social.IsLinkedIn == true) {
        //                                media = media + '<span class="socialmediaicon"><i class="fa fa-linkedin" aria-hidden="true"></i></span>';
        //                    }
        //                    $(element).popover({title: '<h6 style="background: #4f77bc;text-align: center;color: #fff;padding: 5px 0px;"> ' + data.status.Title + ' </h6><div class="social_img"><img src=' + $_BaseUrl + data.status.ImageUrl + ' width="100%" height="140px" ></div><p style="font-size: 12px;padding: 5px 0px;line-height: 20px;"> Description: ' + ': ' + data.status.Description + '</p><span>' + media + '</span>', container: "body", trigger: "hover", placement: "bottom", html: true, });
        //                    //tooltip = '<div class="tooltiptopicevent" style="width:20%;height:auto;background:#fff;position:absolute;z-index:10001;padding:0px;line-height: 200%;top: 242px;left: 667px; opacity: 1.9; border: 1px solid #ccc;"> <h6 style="background: #4f77bc;text-align: center;color: #fff;padding: 5px;"> ' + data.status.Title + ' </h6><div class="social_img"><img src=' + $_BaseUrl + data.status.ImageUrl + ' width="100%" height="140px" ></div><p style="font-size: 12px;padding: 0 5px;line-height: 20px;"> Description: ' + ': ' + data.status.Description + '</p><span>' + media + '</span></br></div>';

        //                }
        //            });
        //        }
        //    });
        //    //$('.fc-day-grid-event fc-event fc-start fc-end fc-draggable').append('<div>abc</div>');
        //    //$(element).popover({ title: event.title, content: "abc", container: "body", trigger: "hover", placement: "bottom" });
        //},


        //eventAfterAllRender: function (view)
        //{
        //    ////debugger;
        //    $("#calendar1").find('.fc-event-container').append('<div class="fa fa-times" style="float:right;color:red" title="Delete Event" onclick="DeleteEvent(this)"></div>');
        //},


        //eventDragStop: function (event, delta, revertFunc, jsEvent, ui, view) {

        //    ////debugger;
        //    console.log('calendar 1 eventDragStop');
        //    var d = new Date();
        //    console.log('calendar 1 eventReceive');
        //    //makeEventsDraggable();
        //    console.log(event);
        //    var id = event.title;
        //    var offset = d.getTimezoneOffset();
        //    // defaultDuration = moment.duration(defaultDuration);
        //    //var formDate = $.fullCalendar.formatDate(event.start._d, 'MM-dd-yyyy');
        //    var start = moment(event.start._d).format('YYYY/MM/DD hh:mm');
        //    var startdate = new Date(event.start._i);
        //    var str = moment(delta.timeStamp).format("YYYY/MM/DD hh:mm");
        //    var droppeddate = (new Date(startdate.setDate(startdate.getDate() + delta._days))).setHours(0, 0, 0, 0);

        //    $.ajax({
        //        url: $_UrlSavecalenderEvents,
        //        type: "Get",
        //        data: { id: id, title: event.title, date: start, offset: offset },
        //        async: false,
        //        contentType: "application/json; charset=utf-8",
        //        success: function (data) {
        //            ////debugger;
        //            //alert("true");
        //        }
        //    });
        //},

        //    //if (isEventOverDiv(jsEvent.clientX, jsEvent.clientY)) {
        //    //    ////debugger;
        //    //    $('#calendar1').fullCalendar('removeEvents', event._id);
        //    //    var el = $("<div class='fc-event'>").appendTo('#external-events-listing').text(event.title.split('/')[1]);
        //    //    el.draggable({
        //    //        zIndex: 999,
        //    //        revert: true,
        //    //        revertDuration: 0
        //    //    });
        //    //    el.data('event', { title: event.title,  id: event.id,stick: true });
        //    //}
        //    calEventStatus = []; // Empty
        //   // makeEventsDraggable();
        //},
        //eventResize: function (event, delta, revertFunc, jsEvent, ui, view) {
        //    makeEventsDraggable();
        //},
        //viewRender: function () {
        //    console.log('calendar 1 viewRender');
        //    //makeEventsDraggable();
        //},

    });
    $(".fc-toolbar .fc-right").html('<div class="row">                <div class="col-sm-12 divTextmsg">                    <i class="fa fa-circle" style="color: rgb(142, 118, 194)" aria-hidden="true"></i> Posted Events                </div>                <div class="col-sm-12">                    <i class="fa fa-circle" style="color: rgb(106, 164, 193)" aria-hidden="true"></i> Scheduled Events                </div>            </div>');
    //$('#calendar1').fullCalendar('gotoDate', '2014-05-20');
    //$('#calendar1').fullCalendar('gotoDate', $.fullCalendar.moment());


    /* initialize the calendar2
    -----------------------------------------------------------------*/

    //$('#calendar2').fullCalendar({
    //    header: {
    //        left: 'prev,next today',
    //        center: 'title',
    //        right: ''
    //    },
    //    editable: true,
    //    droppable: true, // this allows things to be dropped onto the calendar
    //    dragRevertDuration: 0,
    //    eventLimit: true, // allow "more" link when too many events
    //    drop: function (date, jsEvent, ui) {
    //        console.log('calendar 2 drop'); console.log(date); console.log(jsEvent); console.log(ui); console.log(this);

    //        // is the "remove after drop" checkbox checked?
    //        if ($('#drop-remove').is(':checked')) {
    //            // if so, remove the element from the "Draggable Events" list
    //            $(this).remove();
    //        }

    //        // if event dropped from another calendar, remove from that calendar
    //        if (typeof calEventStatus['calendar'] != 'undefined') {
    //            $(calEventStatus['calendar']).fullCalendar('removeEvents', calEventStatus['event_id']);
    //        }

    //        makeEventsDraggable();
    //    },
    //    eventReceive: function (event) {
    //        console.log('calendar 2 eventReceive');
    //        makeEventsDraggable();
    //    },
    //    eventDrop: function (event, delta, revertFunc, jsEvent, ui, view) {
    //        console.log('calendar 2 eventDrop');
    //        makeEventsDraggable();
    //    },
    //    eventDragStart: function (event, jsEvent, ui, view) {

    //        // Add dragging event in global var
    //        calEventStatus['calendar'] = '#calendar2';
    //        calEventStatus['event_id'] = event._id;
    //        console.log('calendar 2 eventDragStart');
    //    },
    //    eventDragStop: function (event, jsEvent, ui, view) {
    //        console.log('calendar 2 eventDragStop');

    //        if (isEventOverDiv(jsEvent.clientX, jsEvent.clientY)) {
    //            $('#calendar2').fullCalendar('removeEvents', event._id);
    //            var el = $("<div class='fc-event'>").appendTo('#external-events-listing').text(event.title);
    //            el.draggable({
    //                zIndex: 999,
    //                revert: true,
    //                revertDuration: 0
    //            });
    //            el.data('event', { title: event.title, id: event.id, stick: true });
    //        }

    //        calEventStatus = []; // Empty
    //        makeEventsDraggable();
    //    },
    //    eventResize: function (event, delta, revertFunc, jsEvent, ui, view) {
    //        makeEventsDraggable();
    //    },
    //    viewRender: function () {
    //        console.log('calendar 2 viewRender');
    //        makeEventsDraggable();
    //    },
    //});


    ///* initialize the calendar3
    //-----------------------------------------------------------------*/

    //$('#calendar3').fullCalendar({
    //    header: {
    //        left: 'prev,next today',
    //        center: 'title',
    //        right: ''
    //    },
    //    editable: true,
    //    droppable: true, // this allows things to be dropped onto the calendar
    //    dragRevertDuration: 0,
    //    eventLimit: true, // allow "more" link when too many events
    //    drop: function (date, jsEvent, ui) {
    //        console.log('calendar 3 drop'); console.log(date); console.log(jsEvent); console.log(ui); console.log(this);
    //        // is the "remove after drop" checkbox checked?
    //        if ($('#drop-remove').is(':checked')) {
    //            // if so, remove the element from the "Draggable Events" list
    //            $(this).remove();
    //        }

    //        // if event dropped from another calendar, remove from that calendar
    //        if (typeof calEventStatus['calendar'] != 'undefined') {
    //            $(calEventStatus['calendar']).fullCalendar('removeEvents', calEventStatus['event_id']);
    //        }

    //        makeEventsDraggable();
    //    },
    //    eventReceive: function (event) {
    //        console.log('calendar 3 eventReceive');
    //        makeEventsDraggable();
    //    },
    //    eventDrop: function (event, delta, revertFunc, jsEvent, ui, view) {
    //        console.log('calendar 3 eventDrop');
    //        makeEventsDraggable();
    //    },
    //    eventDragStart: function (event, jsEvent, ui, view) {
    //        console.log(event); console.log(jsEvent); console.log(ui); console.log(view);

    //        // Add dragging event in global var
    //        calEventStatus['calendar'] = '#calendar3';
    //        calEventStatus['event_id'] = event._id;
    //        console.log('calendar 3 eventDragStart');
    //    },
    //    eventDragStop: function (event, jsEvent, ui, view) {
    //        console.log('calendar 3 eventDragStop');

    //        if (isEventOverDiv(jsEvent.clientX, jsEvent.clientY)) {
    //            $('#calendar3').fullCalendar('removeEvents', event._id);
    //            var el = $("<div class='fc-event'>").appendTo('#external-events-listing').text(event.title);
    //            el.draggable({
    //                zIndex: 999,
    //                revert: true,
    //                revertDuration: 0
    //            });
    //            el.data('event', { title: event.title, id: event.id, stick: true });
    //        }

    //        calEventStatus = []; // Empty
    //        makeEventsDraggable();
    //    },
    //    eventResize: function (event, delta, revertFunc, jsEvent, ui, view) {
    //        makeEventsDraggable();
    //    },
    //    viewRender: function () {
    //        console.log('calendar 3 viewRender');
    //        makeEventsDraggable();
    //    },
    //});


});




function convertUTCDateToLocalDate(date) {
    //var localTime  = moment.utc(date).toDate();
    //localTime = moment(localTime).format('YYYY-MM-DD HH:mm:ss');
    //return localTime;
    var newDate = new Date(date);
    newDate.setMinutes(date.getMinutes() - date.getTimezoneOffset());
    return newDate;
}

function convertTime(serverdate) {
    var date = new Date(serverdate);
    // convert to utc time
    var toutc = date.toUTCString();
    //convert to local time
    var locdat = new Date(toutc + " UTC");
    return locdat;
}
function UrlParser(str) {
    "use strict";

    var regx = /^(((([^:\/#\?]+:)?(?:(\/\/)((?:(([^:@\/#\?]+)(?:\:([^:@\/#\?]+))?)@)?(([^:\/#\?\]\[]+|\[[^\/\]@#?]+\])(?:\:([0-9]+))?))?)?)?((\/?(?:[^\/\?#]+\/+)*)([^\?#]*)))?(\?[^#]+)?)(#.*)?/,
        matches = regx.exec(str),
        parser = null;

    if (null !== matches) {
        parser = {
            href: matches[0],
            withoutHash: matches[1],
            url: matches[2],
            origin: matches[3],
            protocol: matches[4],
            protocolseparator: matches[5],
            credhost: matches[6],
            cred: matches[7],
            user: matches[8],
            pass: matches[9],
            host: matches[10],
            hostname: matches[11],
            port: matches[12],
            pathname: matches[13],
            segment1: matches[14],
            segment2: matches[15],
            search: matches[16],
            hash: matches[17]
        };
    }

    return parser;
};



//Covert datetime by GMT offset
//If toUTC is true then return UTC time other wise return local time
function convertLocalDateToUTCDate(date, toUTC) {
    date = new Date(date);
    //Local time converted to UTC
    console.log("Time :" + date);
    var localOffset = date.getTimezoneOffset() * 60000;
    var localTime = date.getTime();
    if (toUTC) {
        date = localTime + localOffset;
    }
    else {
        date = localTime - localOffset;
    }
    date = new Date(date);
    console.log("Converted time" + date);
    return date;
}

$(function () {
    setInterval(function () {
        var divUtc = $('#divUTC');
        var divLocal = $('#divLocal');
        //put UTC time into divUTC
        divUtc.text(moment.utc().format('YYYY-MM-DD HH:mm:ss'));

        //get text from divUTC and conver to local timezone
        var localTime = moment.utc(divUtc.text()).toDate();
        localTime = moment(localTime).format('YYYY-MM-DD HH:mm:ss');
        divLocal.text(localTime);
    }, 1000);
});



