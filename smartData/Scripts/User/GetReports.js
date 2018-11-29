var StartDateForChart = "";
var EndDateForChart = "";

$(document).ready(function () {
    debugger;
    LoadChart(1);
    $("#DateSelector").html(moment().subtract('days', 30).calendar() + " - " + moment().format('MM/DD/YYYY'));
    $("#DateSelectorFrom").datepicker();
    $("#DateSelectorTo").datepicker();

    $(".OpenDatePicker").click(function (e) {
        e.stopPropagation();
        $("#DateSelectorInnerDiv").show();
    });
    $("#DateSelectorFrom").val(moment().subtract('days', 30).calendar());
    $("#DateSelectorTo").val(moment().format('MM/DD/YYYY'));
    $("#DateSelectorInnerDiv").click(function (e) {
        e.stopPropagation();
    });

    $("#UpdateGridByDate", ".OpenDatePicker").click(function () {
        debugger;
        var DateFrom = convertDate($("#DateSelectorFrom").val());
        var DateTo = convertDate($("#DateSelectorTo").val());
        if (DateFrom > DateTo) {
            BootstrapDialog.alert("Date To should be greater than Date From");
            return;
        }
        $("#DateSelector").html($("#DateSelectorFrom").val() + " - " + $("#DateSelectorTo").val());
        if ($("option:selected", $("#drpLikes")).val() == 1) {
            StartDateForChart = $("[id$=DateSelectorFrom]").val();
            EndDateForChart = $("[id$=DateSelectorTo]").val();
            LoadChartForlikes("Likes");
            //StartDateForChart = "";
            //EndDateForChart = "";
            $("#DateSelectorInnerDiv").hide();

        }
        else if ($("option:selected", $("#drpLikes")).val() == 2) {
            StartDateForChart = $("[id$=DateSelectorFrom]").val();
            EndDateForChart = $("[id$=DateSelectorTo]").val();
            LoadChartForlikes("Comments");
            //StartDateForChart = "";
            //EndDateForChart = "";
            $("#DateSelectorInnerDiv").hide();
        }
        else {
            updateChartValues();
        }      
    });

    convertDate = function (date) {
        var splitarray = date.split("/");
        return splitarray[2] + "-" + splitarray[0] + "-" + splitarray[1];
    }


    $("#drpLikes").on("change", function (event) {
        debugger;
        var id = $("option:selected", $("#drpLikes")).val();
        StartDateForChart = $("[id$=DateSelectorFrom]").val();
        EndDateForChart = $("[id$=DateSelectorTo]").val();
        var chartType = "";
        if (id == 1) {
            chartType = "Likes";
        }
        else {
            chartType = "Comments";
        }   
        if (id != 0) {
            LoadChartForlikes(chartType);
            $("#DateSelectorInnerDiv").hide();
        }
        else {
            $(".tp-head-tab").show();
            $("#chart_divNew").hide();
            $("#chart_div").show();
            LoadChart(1);
        }
    });
});

function LoadChart(str) {
    var $border_color = "#efefef";
    var $grid_color = "#ddd";
    var $default_black = "#666";
    var $default_grey = "#ccc";
    var $primary_color = "#428bca";
    var $go_green = "#93caa3";
    var $jet_blue = "#70aacf";
    var $lemon_yellow = "#ffe38a";
    var $nagpur_orange = "#fc965f";
    var $ruby_red = "#fa9c9b";
    var userId = $('#hdnUserId').val();
    var CompanyId = $("#CompanyId").val();
    var TrackerType = $("#TrackerType").val();
    var SourceName = $("#SourceName").val();
    var data = "";
    var CalendarType = "";
    var DataArray = new Array();
    var chartType = "Day";
    if (str == 1)
        chartType = "Day";
    else if (str == 2)
        chartType = "Week";
    else if (str == 3)
        chartType = "Month";
    else if (str == 4)
        chartType = "Custom";
    else if (str == 5) {
        chartType = "Cust";
        CalendarType = $("#HiddenCalenderType").val();
    }
    var max = 0;
    $.ajax({
        url: $_UrlGetPostChartData + "?UserId=" + userId + "&StartDate=" + StartDateForChart + "&EndDate=" + EndDateForChart + "&ChartType=" + chartType,
        type: 'post',
        dataType: 'json',
        error: function (xhr, status, error) {
        },
        success: function (result) {
            if (result.length == 0) {
                $("#chart_div").html("");
                $("#chart_div").html("<div style='margin-left:114px;text-align: center;margin-top:133px;font-style: italic;color:gray;'>Chart data is not available.</div>");
            }
            else {
                debugger;
                var dataarray = [];
                var max = 0;
                for (var i = 0; i < result.length; i++) {
                    var datalist = [];
                    for (var j = 0; j < result[i].DataList.length; j++) {
                        if (result[i].DataList[j].Count > max) {
                            max = result[i].DataList[j].Count;
                        }
                        datalist.push([result[i].DataList[j].Date, result[i].DataList[j].Count]);
                    }
                    dataarray.push({ "data": datalist, "label": result[i].SourceName, idx: i });
                }

                somePlot = $.plot($("#chart_div"), dataarray,
               {
                   series: {
                       lines: {
                           show: true,
                           lineWidth: 2,
                           fill: true,
                           fillColor: {
                               colors: [{
                                   opacity: 0.0
                               }, {
                                   opacity: 0.2
                               }]
                           }
                       },
                       points: {
                           radius: 5,
                           show: true
                       },
                       grow: {
                           active: true,
                           steps: 50
                       },
                       shadowSize: 2
                   },
                   grid: {
                       hoverable: true,
                       clickable: true,
                       tickColor: "#f0f0f0",
                       borderWidth: 0
                   },
                   colors: ["#94941F", "#1F8685", "#9CC0E4", "#E0BF6F", "#efd71f", "#ef5305", "#f31919", "#bf8fae", "#e6119b", "#a422c3", "#1d0d21", "#5a88de", "#23d2d4", "#23d47c", "#57ad0b"],
                   xaxis: {
                       show: true,
                       mode: "categories",
                       tickLength: 2
                   },
                   yaxis: {
                       min: 0,
                       tickDecimals: 0
                   },
                   zoom: {
                       interactive: true
                   },
                   pan: {
                       interactive: true
                   },
                   panAmount: 50,
                   zoomAmount: 1.0,
                   position: { left: "20px", top: "20px" },
                   tooltip: true,
                   tooltipOpts: {
                       content: gettooltip,
                       defaultTheme: false,
                       shifts: {
                           x: 0,
                           y: 20
                       }
                   },
                   legend: {
                       labelFormatter: function (label, series) {
                           return '<a href="#" onClick="togglePlot(' + series.idx + '); return false;">' + label + '</a>';
                       }
                   }
               }
           );
                if (datalist.length >= 20)
                    $(".flot-x-axis > .tickLabel:odd").hide();
            }
        }

    });
}


function LoadChartForlikes(str) {
    debugger;
    var userId = $('#hdnUserId').val();
    $.ajax({
        url: $_UrlGetPostLikesData + "?UserId=" + userId + "&StartDate=" + StartDateForChart + "&EndDate=" + EndDateForChart + "&ChartType=" + str,
        type: 'post',
        dataType: 'json',
        error: function (xhr, status, error) {
        },
        success: function (result) {
            $(".tp-head-tab").hide();
            $("#chart_div").css("display", "none");
            $("#chart_divNew").show();
            $("#chart_divNew").html("");
            if (result.length == 0) {
                $("#chart_divNew").html("<div style='margin-left:114px;text-align: center;margin-top:133px;font-style: italic;color:gray;'>Chart data is not available.</div>");
            }
            else {

                var dataarray = [];
                var datalist = [];
                var tracker_name = "";
                if (result.length > 0) {
                    tracker_name = result[0].TrackerName;
                }

                for (var i = 0; i < result.length; i++) {
                    debugger;
                    datalist.push([result[i].Date, result[i].Count]);

                }
                dataarray.push({ "data": datalist, "label": str, idx: i });
                somePlot = $.plot($("#chart_divNew"), dataarray,
               {
                   series: {
                       lines: {
                           show: true,
                           lineWidth: 2,
                           fill: true,
                           fillColor: {
                               colors: [{
                                   opacity: 0.0
                               }, {
                                   opacity: 0.2
                               }]
                           }
                       },
                       points: {
                           radius: 5,
                           show: true
                       },
                       grow: {
                           active: true,
                           steps: 50
                       },
                       shadowSize: 2
                   },
                   grid: {
                       hoverable: true,
                       clickable: true,
                       tickColor: "#f0f0f0",
                       borderWidth: 0
                   },
                   colors: ["#9CC0E4", "#1F8685", "#9CC0E4", "#E0BF6F", "#efd71f"],
                   xaxis: {
                       show: true,
                       mode: "categories"
                   },
                   yaxis: {
                       min: 0,
                       tickDecimals: 0
                   },
                   tooltip: true,
                   tooltipOpts: {
                       content: gettooltipLikes,
                       defaultTheme: false,
                       shifts: {
                           x: 0,
                           y: 20
                       }
                   },
                   legend: {
                       labelFormatter: function (label, series) {
                           return '<a href="#" onClick="togglePlot(' + '0' + '); return false;">' + label + '</a>';
                       }
                   }
               }
           );
                if (datalist.length >= 20)
                    $(".flot-x-axis > .tickLabel:odd").hide();
            }
        }
    });
}


function gettooltip(label, x, y) {
    debugger
    if (x!=null) {
        var s = x.split(" ");
        s = s.reverse();
        x = s.toString()
        x = x.replace(",", " ");
    }
    y = parseInt(y);
    if (y == 0) {
        return false;

    }
    if (y==1) {
        return " " + y + " Post - " + x ;
    }
    else {
        return " " + y + " Posts - " + x ;
    }
  
}

function gettooltipLikes(label, x, y) {
    return label +" on " + x + " was " + y;
}


var somePlot = null;
togglePlot = function (seriesIdx) {
    var someData = somePlot.getData();
    someData[seriesIdx].lines.show = !someData[seriesIdx].lines.show;
    someData[seriesIdx].points.show = !someData[seriesIdx].points.show;
    somePlot.setData(someData);
    somePlot.draw();
}

function reloadChart(ref) {
    debugger;
    var html = '<div style="margin-left:500px;padding-top:100px;">';
    html += '<img src= />';
    html += '</div>';
    $("#chart_div").html("");
    $("#chart_div").html(html);
    if ($(ref).text() == "Day") {
        LoadChart(1);
        $("#HiidenChartType").val(1);
        $("#HiddenCalendarType").val(1);

    }
    else if ($(ref).text() == "Week") {
        LoadChart(2);
        $("#HiidenChartType").val(2);
        $("#HiddenCalendarType").val(2);
    }
    else if ($(ref).text() == "Month") {
        LoadChart(3);
        $("#HiidenChartType").val(3);
        $("#HiddenCalendarType").val(3);
    }
    $('.tp-head-tab a').removeClass('active');
    $(ref).addClass('active');
}



var updateChartValues = function () {
    debugger;
    ActionTypeForChart = "Calender";
    StartDateForChart = $("[id$=DateSelectorFrom]").val();
    EndDateForChart = $("[id$=DateSelectorTo]").val();
    LoadChart(4);
    ActionTypeValueForChart = 0;
    ActionTypeForChart = "";
    //StartDateForChart = "";
    //EndDateForChart = "";
    $("#DateSelectorInnerDiv").hide();
}

convertDateToDDMMYY = function (date) {
    var splitArray = date.split('/');
    var MonthArray = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return splitArray[1] + "-" + MonthArray[parseInt(splitArray[0]) - 1] + "-" + splitArray[2];
}



