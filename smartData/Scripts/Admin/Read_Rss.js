var btnValue = [];
$(document).ready(function () {
    Dashboard.Init();
    var char = 0;

    btnValue.temp = "test";

    //$("#industry").on("change", function (event) {
    //    debugger;
    //    var txt = $("option:selected", $("#industry")).text();
    //    var subtxt = $("option:selected", $("#subIndustry")).text();
    //    if (txt != 'Select Industry Name' && subtxt!='Select SubIndustry') {
    //        $("#selectCategory").modal("hide");
    //    }
    //    else {
    //        BootstrapDialog.alert('Please select industry and subIndustry');

    //    }
    //});

    $('#btnAddRss').click(function () {
        debugger;
        var qs = getQueryStrings();
        var feedID = qs["FeedUrl"];
        var txt = $("option:selected", $("#industry")).text();
        var subtxt = $("option:selected", $("#subIndustry")).text();
        if (btnValue.this.data("imagelink") == "") {
            // btnValue.this.data = $("#txtImageCDN").val();
            btnValue.this.data("imagelink", $("#txtImageCDN").val());

        }
        if ($("#txtTitle").val() != "") {
            var Title = $("#txtTitle").val();
            // btnValue.this.data("itemlinktext", $("#txtTitle").val());
        }
        else {
            var Title = btnValue.this.data("itemlinktext");
        }

        if ($("#userTYpe").val() == 3) {
            if (txt != 'Select Industry Name' && subtxt != 'Select SubIndustry' && $("#txtHeading").val() != "") {

                btnValue.this
                // $("#selectCategory").modal("hide");   // SRohit
                if ($('#hdnUserType').val() == 3) { ////Added by SRohit
                    if ($("#industry option:selected").text() != 'Select Industry Name') {
                        debugger;
                        var Url = btnValue.this.data("itemlink");
                        var Description = btnValue.this.data("itemdescription").substring(0, 300);
                        //var Source = btnValue.this.data("itemlinktext");
                        var Source = Title;
                        var ImageUrl = btnValue.this.data("imagelink");
                        //$('.rss-feed-design').find('table').length ? ImageUrl = $(Source).find("img").attr("src") : ImageUrl = $(Source).attr("src");
                        var CreatedDate = btnValue.this.data("postdate");
                        var CategoryName = $("#industry option:selected").text();
                        var CategoryId = $("#industry option:selected").val();
                        var subIndustryName = $("#subIndustry option:selected").text();
                        var SocialMedia = 'Rss';
                        var arr = [];
                        arr.push(ImageUrl);
                        var arrayIds = [];
                        var arrayTags = [];
                        var heading = $("#txtHeading").val();
                        var OriginalTitle = btnValue.this.data("itemlinktext");


                        var JsonWorkInfo = {};
                        // Get Images
                        //$('.dz-image-preview ').each(function () {
                        //    var text =  btnValue.this.children().eq(1).children().eq(1).text()
                        //    arr.push(text);
                        //});
                        JsonWorkInfo.Heading = heading;
                        JsonWorkInfo.TextMessage = Description;
                        JsonWorkInfo.TextDescription = Source;
                        // JsonWorkInfo.Ids = arrayIds;
                        JsonWorkInfo.ImageArray = arr;
                        JsonWorkInfo.Url = Url;
                        JsonWorkInfo.CreatedDate = CreatedDate;
                        // JsonWorkInfo.TagArray = arrayTags;
                        JsonWorkInfo.CategoryId = CategoryId;
                        JsonWorkInfo.CategoryName = CategoryName;
                        JsonWorkInfo.SubIndustryName = subIndustryName;
                        // JsonWorkInfo.SubIndustryId = state;
                        //JsonWorkInfo.SubIndustryName = stateId;
                        JsonWorkInfo.SocialMedia = SocialMedia;
                        JsonWorkInfo.OriginalTitle = OriginalTitle;
                        JsonWorkInfo.feedID = feedID;
                        Dashboard.AjaxCalls.PostData(JsonWorkInfo, btnValue.this);
                    }
                }
                else {
                    debugger;
                    if ($("#SubindustryForAdmin option:selected").text() != 'Select SubIndustry') {
                        var Url = btnValue.this.data("itemlink");
                        var Description = btnValue.this.data("itemdescription").substring(0, 300);
                        //var Source = btnValue.this.data("itemlinktext");
                        var Source = Title;
                        var ImageUrl = btnValue.this.data("imagelink");
                        //$('.rss-feed-design').find('table').length ? ImageUrl = $(Source).find("img").attr("src") : ImageUrl = $(Source).attr("src");
                        var CreatedDate = btnValue.this.data("postdate");
                        var CategoryName = $("#industry option:selected").text();
                        var CategoryId = $("#industryId").val();
                        var subIndustryName = $("#SubindustryForAdmin option:selected").text();
                        var SocialMedia = 'Rss';
                        var arr = [];
                        arr.push(ImageUrl);
                        var arrayIds = [];
                        var arrayTags = [];
                        var heading = $("#txtHeading").val();
                        var OriginalTitle = btnValue.this.data("itemlinktext");
                        var JsonWorkInfo = {};
                        // Get Images
                        //$('.dz-image-preview ').each(function () {
                        //    var text =  btnValue.this.children().eq(1).children().eq(1).text()
                        //    arr.push(text);
                        //});
                        JsonWorkInfo.Heading = heading;
                        JsonWorkInfo.TextMessage = Description;
                        JsonWorkInfo.TextDescription = Source;
                        // JsonWorkInfo.Ids = arrayIds;
                        JsonWorkInfo.ImageArray = arr;
                        JsonWorkInfo.Url = Url;
                        JsonWorkInfo.CreatedDate = CreatedDate;
                        // JsonWorkInfo.TagArray = arrayTags;
                        JsonWorkInfo.CategoryId = CategoryId;
                        JsonWorkInfo.CategoryName = CategoryName;
                        JsonWorkInfo.SubIndustryName = subIndustryName;
                        // JsonWorkInfo.SubIndustryId = state;
                        //JsonWorkInfo.SubIndustryName = stateId;
                        JsonWorkInfo.SocialMedia = SocialMedia;
                        JsonWorkInfo.OriginalTitle = OriginalTitle;
                        JsonWorkInfo.feedID = feedID;
                        Dashboard.AjaxCalls.PostData(JsonWorkInfo, btnValue.this);

                    }
                    //   Dashboard.BindDataMethods.CreateJsonForPostDetails();
                }

            }

            else {
                if ($("#txtHeading").val() == "") {
                    BootstrapDialog.alert('Please enter heading');
                }
                else {
                    BootstrapDialog.alert('Please select industry and subIndustry');
                }

            }
        }
        else {

            if (subtxt == 'Select SubIndustry') {
                BootstrapDialog.alert('Please select subIndustry');
            }
            else {
                if ($('#hdnUserType').val() == 3) { ////Added by SRohit
                    // $("#selectCategory").modal("hide");
                    if ($("#industry option:selected").text() != 'Select Industry Name') {
                        debugger;
                        var Url = btnValue.this.data("itemlink");
                        var Description = btnValue.this.data("itemdescription").substring(0, 300);
                        //  var Source = btnValue.this.data("itemlinktext");
                        var Source = Title;
                        var ImageUrl = btnValue.this.data("imagelink");
                        //$('.rss-feed-design').find('table').length ? ImageUrl = $(Source).find("img").attr("src") : ImageUrl = $(Source).attr("src");
                        var CreatedDate = btnValue.this.data("postdate");
                        var CategoryName = $("#industry option:selected").text();
                        var CategoryId = $("#industry option:selected").val();
                        var subIndustryName = $("#subIndustry option:selected").text();
                        var SocialMedia = 'Rss';
                        var arr = [];
                        arr.push(ImageUrl);
                        var arrayIds = [];
                        var arrayTags = [];
                        var heading = $("#txtHeading").val();
                        var OriginalTitle = btnValue.this.data("itemlinktext");

                        var JsonWorkInfo = {};
                        // Get Images
                        //$('.dz-image-preview ').each(function () {
                        //    var text =  btnValue.this.children().eq(1).children().eq(1).text()
                        //    arr.push(text);
                        //});
                        JsonWorkInfo.Heading = heading;
                        JsonWorkInfo.TextMessage = Description;
                        JsonWorkInfo.TextDescription = Source;
                        // JsonWorkInfo.Ids = arrayIds;
                        JsonWorkInfo.ImageArray = arr;
                        JsonWorkInfo.Url = Url;
                        JsonWorkInfo.CreatedDate = CreatedDate;
                        // JsonWorkInfo.TagArray = arrayTags;
                        JsonWorkInfo.CategoryId = CategoryId;
                        JsonWorkInfo.CategoryName = CategoryName;
                        JsonWorkInfo.SubIndustryName = subIndustryName;
                        // JsonWorkInfo.SubIndustryId = state;
                        //JsonWorkInfo.SubIndustryName = stateId;
                        JsonWorkInfo.SocialMedia = SocialMedia;
                        JsonWorkInfo.OriginalTitle = OriginalTitle;
                        JsonWorkInfo.feedID = feedID;
                        Dashboard.AjaxCalls.PostData(JsonWorkInfo, btnValue.this);

                    }
                }
                else {
                    debugger;
                    if ($("#SubindustryForAdmin option:selected").text() != 'Select SubIndustry') {
                        var Url = btnValue.this.data("itemlink");
                        var Description = btnValue.this.data("itemdescription").substring(0, 300);
                        //   var Source = btnValue.this.data("itemlinktext");
                        var Source = Title;
                        var ImageUrl = btnValue.this.data("imagelink");
                        //$('.rss-feed-design').find('table').length ? ImageUrl = $(Source).find("img").attr("src") : ImageUrl = $(Source).attr("src");
                        var CreatedDate = btnValue.this.data("postdate");
                        var CategoryName = $("#industry option:selected").text();
                        var CategoryId = $("#industryId").val();
                        var subIndustryName = $("#SubindustryForAdmin option:selected").text();
                        var SocialMedia = 'Rss';
                        var arr = [];
                        arr.push(ImageUrl);
                        var arrayIds = [];
                        var arrayTags = [];
                        var heading = $("#txtHeading").val();
                        var OriginalTitle = btnValue.this.data("itemlinktext");
                        var JsonWorkInfo = {};
                        // Get Images
                        //$('.dz-image-preview ').each(function () {
                        //    var text =  btnValue.this.children().eq(1).children().eq(1).text()
                        //    arr.push(text);
                        //});
                        JsonWorkInfo.Heading = heading;
                        JsonWorkInfo.TextMessage = Description;
                        JsonWorkInfo.TextDescription = Source;
                        // JsonWorkInfo.Ids = arrayIds;
                        JsonWorkInfo.ImageArray = arr;
                        JsonWorkInfo.Url = Url;
                        JsonWorkInfo.CreatedDate = CreatedDate;
                        // JsonWorkInfo.TagArray = arrayTags;
                        JsonWorkInfo.CategoryId = CategoryId;
                        JsonWorkInfo.CategoryName = CategoryName;
                        JsonWorkInfo.SubIndustryName = subIndustryName;
                        // JsonWorkInfo.SubIndustryId = state;
                        //JsonWorkInfo.SubIndustryName = stateId;
                        JsonWorkInfo.SocialMedia = SocialMedia;
                        JsonWorkInfo.OriginalTitle = OriginalTitle;
                        JsonWorkInfo.feedID = feedID;
                        Dashboard.AjaxCalls.PostData(JsonWorkInfo, btnValue.this);

                    }
                    //   Dashboard.BindDataMethods.CreateJsonForPostDetails();
                }

            }

        }
    })
   

});


var Dashboard = {
    Init: function () {
        Dashboard.PreInit();


    },
    PreInit: function () {
        Dashboard.Events.ClickEvents();

    },
    PageLoad: {

    },
    Events: {
        ClickEvents: function () {

            debugger;
            var categoryName = "";
            $.ajax({
                url: $_UrlGetIndustryById,
                type: "POST",
                data: { "id": $('#industryId').val() },
                success: function (response) {
                    debugger;
                    categoryName = response.IndustryName;
                },
                error: function (result) {

                }
            });

            $('.add_Rss').click(function () {
                debugger;
                $(".loadercontainingdiv").hide()
                btnValue.this = $(this);
                $("#txtTitle").val(btnValue.this.data("itemlinktext"));
                if (btnValue.this.data("imagelink") == "" || btnValue.this.data("imagelink") == undefined) {

                    $(".ImageDiv").show();
                    $("#txtURL").val(btnValue.this.data("itemlink"));

                    $("#fatchData").click();



                }
                else {
                    $(".ImageDiv").hide();

                }
                $("#selectCategory").modal("show");
                var $this = $(this)
                //if ($('#hdnUserType').val() == 3) { ////Added by SRohit
                //    if ($("#industry option:selected").text() != 'Select Industry Name') {
                //        debugger;
                //        var Url = $(this).data("itemlink");
                //        var Description = $(this).data("itemdescription").substring(0, 300);
                //        var Source = $(this).data("itemlinktext");
                //        var ImageUrl = $(this).data("imagelink");
                //        //$('.rss-feed-design').find('table').length ? ImageUrl = $(Source).find("img").attr("src") : ImageUrl = $(Source).attr("src");
                //        var CreatedDate = $(this).data("postdate");
                //        var CategoryName = $("#industry option:selected").text();
                //        var CategoryId = $("#industry option:selected").val();
                //        var subIndustryName = $("#subIndustry option:selected").text();
                //        var SocialMedia = 'Rss';
                //        var arr = [];
                //        arr.push(ImageUrl);
                //        var arrayIds = [];
                //        var arrayTags = [];
                //        var JsonWorkInfo = {};
                //        // Get Images
                //        //$('.dz-image-preview ').each(function () {
                //        //    var text = $(this).children().eq(1).children().eq(1).text()
                //        //    arr.push(text);
                //        //});

                //        JsonWorkInfo.TextMessage = Description;
                //        JsonWorkInfo.TextDescription = Source;
                //        // JsonWorkInfo.Ids = arrayIds;
                //        JsonWorkInfo.ImageArray = arr;
                //        JsonWorkInfo.Url = Url;
                //        JsonWorkInfo.CreatedDate = CreatedDate;
                //        // JsonWorkInfo.TagArray = arrayTags;
                //        JsonWorkInfo.CategoryId = CategoryId;
                //        JsonWorkInfo.CategoryName = CategoryName;
                //        JsonWorkInfo.SubIndustryName = subIndustryName;
                //        // JsonWorkInfo.SubIndustryId = state;
                //        //JsonWorkInfo.SubIndustryName = stateId;
                //        JsonWorkInfo.SocialMedia = SocialMedia;
                //        Dashboard.AjaxCalls.PostData(JsonWorkInfo, $(this));
                //    }
                //}
                //else {
                //    debugger;
                //    if ($("#SubindustryForAdmin option:selected").text() != 'Select SubIndustry') {
                //        var Url = $(this).data("itemlink");
                //        var Description = $(this).data("itemdescription").substring(0, 300);
                //        var Source = $(this).data("itemlinktext");
                //        var ImageUrl = $(this).data("imagelink");
                //        //$('.rss-feed-design').find('table').length ? ImageUrl = $(Source).find("img").attr("src") : ImageUrl = $(Source).attr("src");
                //        var CreatedDate = $(this).data("postdate");
                //        var CategoryName = $("#industry option:selected").text();
                //        var CategoryId = $("#industryId").val();
                //        var subIndustryName = $("#SubindustryForAdmin option:selected").text();
                //        var SocialMedia = 'Rss';
                //        var arr = [];
                //        arr.push(ImageUrl);
                //        var arrayIds = [];
                //        var arrayTags = [];
                //        var JsonWorkInfo = {};
                //        // Get Images
                //        //$('.dz-image-preview ').each(function () {
                //        //    var text = $(this).children().eq(1).children().eq(1).text()
                //        //    arr.push(text);
                //        //});

                //        JsonWorkInfo.TextMessage = Description;
                //        JsonWorkInfo.TextDescription = Source;
                //        // JsonWorkInfo.Ids = arrayIds;
                //        JsonWorkInfo.ImageArray = arr;
                //        JsonWorkInfo.Url = Url;
                //        JsonWorkInfo.CreatedDate = CreatedDate;
                //        // JsonWorkInfo.TagArray = arrayTags;
                //        JsonWorkInfo.CategoryId = CategoryId;
                //        JsonWorkInfo.CategoryName = CategoryName;
                //        JsonWorkInfo.SubIndustryName = subIndustryName;
                //        // JsonWorkInfo.SubIndustryId = state;
                //        //JsonWorkInfo.SubIndustryName = stateId;
                //        JsonWorkInfo.SocialMedia = SocialMedia;
                //        Dashboard.AjaxCalls.PostData(JsonWorkInfo, $(this));
                //    }
                //    //   Dashboard.BindDataMethods.CreateJsonForPostDetails();
                //}


            });

            $('.ignore_Rss').click(function () {
                debugger;
                var $this = $(this)
                var Url = $(this).data("itemlink");
                var Description = $(this).data("itemlinktext");
                //var Source = $(this).data("itemdescription");
                //var ImageUrl;
                //$('.rss-feed-design').find('table').length ? ImageUrl = $(Source).find("img").attr("src") : ImageUrl = $(Source).attr("src");
                var CreatedDate = $(this).data("postdate");
                //var CategoryName = 'Real Estate';
                //var CategoryId = '1';
                //var SocialMedia = 'Rss';
                //var arr = [];
                //arr.push(ImageUrl);
                //var arrayIds = [];
                //var arrayTags = [];
                var JsonWorkInfo = {};
                // Get Images
                //$('.dz-image-preview ').each(function () {
                //    var text = $(this).children().eq(1).children().eq(1).text()
                //    arr.push(text);
                //});

                JsonWorkInfo.TextMessage = Description;

                // JsonWorkInfo.Ids = arrayIds;
                // JsonWorkInfo.ImageArray = arr;
                JsonWorkInfo.Url = Url;
                JsonWorkInfo.CreatedDate = CreatedDate;
                // JsonWorkInfo.TagArray = arrayTags;
                //    JsonWorkInfo.CategoryId = CategoryId;
                //   JsonWorkInfo.CategoryName = CategoryName;
                // JsonWorkInfo.SubIndustryId = state;
                //JsonWorkInfo.SubIndustryName = stateId;
                //    JsonWorkInfo.SocialMedia = SocialMedia;
                Dashboard.AjaxCalls.PostIgnoreData(JsonWorkInfo, $this);
                //   Dashboard.BindDataMethods.CreateJsonForPostDetails();
            });
            $('#selectCategory').on('shown', function () {
                debugger
                if ($("#industry option:selected").text() != 'Select Industry Name') {
                    $('#industry').prop('selectedIndex', 0);
                }

            })

          
        },



    },
    AjaxCalls: {
        PostData: function (PostInformation, button) {
            $('.loadercontainingdiv').show();
            debugger;
            var addbottun = $(button);
            $.ajax({
                url: $_AddRssData,
                type: "POST",
                data: { "PostInformation": JSON.stringify(PostInformation) },
                success: function (data) {

                    $("#industry").val("")
                    $("#subIndustry").val("0")
                    $("#txtHeading").val("");
                    debugger;
                    if (data.status == '1') {
                        $(addbottun).text("Already Added");
                        $(addbottun).addClass("disabled");
                        $(addbottun).prev("a.ignore_Rss").remove();
                        $("#selectCategory").modal("hide");



                        ShowMessages(BootstrapDialog.TYPE_SUCCESS, 'Content added successfully.');
                        $("#selectCategory").modal("hide");
                        $('.loadercontainingdiv').show();
                        $(addbottun).closest("div.rss-item").remove()
                        $("#SubindustryForAdmin").val("");

                    }
                    if (data.status == '2') {
                        ShowMessages(BootstrapDialog.TYPE_DANGER, 'Already Added.');
                        $('.loadercontainingdiv').show();
                        $("#selectCategory").modal("hide");

                    }
                    if (data.status == '3') {
                        ShowMessages(BootstrapDialog.TYPE_DANGER, 'Error occured.');
                        $('.loadercontainingdiv').show();
                        $("#selectCategory").modal("hide");
                    }

                },
                error: function (result) {
                    $("#txtHeading").val("");
                    $("#industry").val("")
                    $("#subIndustry").val("0")
                    $('.loadercontainingdiv').show();
                    ShowMessages(BootstrapDialog.TYPE_DANGER, 'Error occured.');
                    $("#selectCategory").modal("hide");
                }
            });
        },
        PostIgnoreData: function (PostInformation, button) {
            $('.loadercontainingdiv').show();
            debugger;
            var clkdBtn = $(button);
            $.ajax({
                url: $_IgnoreRssData,
                type: "POST",
                data: { "PostInformation": JSON.stringify(PostInformation) },
                success: function (data, button) {

                    debugger;
                    if (data.status == '1') {
                        $(clkdBtn).closest("div.rss-item").remove();
                        ShowMessages(BootstrapDialog.TYPE_SUCCESS, 'Content  Ignored.');

                    }
                    if (data.status == '2') {
                        ShowMessages(BootstrapDialog.TYPE_DANGER, 'Already Ignored .');

                    }
                    if (data.status == '3') {
                        ShowMessages(BootstrapDialog.TYPE_DANGER, 'Error occured while ignored content .');
                    }

                },
                error: function (result) {

                    ShowMessages(BootstrapDialog.TYPE_DANGER, 'Error occured while posting content.');
                }
            });
        },

    },

}


function ShowMessages(type, message) {
    $messageData = $("<span>Information</span>");

    var popTimer = parseInt($("#hdnPopUpTimer").val());       setTimeout("$.each(BootstrapDialog.dialogs, function(id, dialog){dialog.close();});",popTimer);
    BootstrapDialog.configDefaultOptions({ cssClass: 'bstDialg' });
    BootstrapDialog.show({
        title: $messageData,
        type: type,
        message: message,
    });




}

function getQueryStrings() {
    var assoc = {};
    var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
    var queryString = location.search.substring(1);
    var keyValues = queryString.split('&');

    for (var i in keyValues) {
        var key = keyValues[i].split('=');
        if (key.length > 1) {
            assoc[decode(key[0])] = decode(key[1]);
        }
    }

    return assoc;
}






