var ContentSourceName ="";
$(document).ready(function () {
    $('#add_content').click(function () {
        debugger;
        $('#tabTwitter').attr("data-groupid", "")
        $('#tabFacebook').attr("data-groupid", "")
        $('#tabLinkedIn').attr("data-groupid", "")
        $('#txtMsgContent').css('border', '');
        $('.indus').removeClass('errorClass');
        $('textarea').removeClass('errorClass');
        $('textarea').css("border", '1px solid #ccc')
        $('textarea').val('');
        $('.indus').val('');
        $('.subindus').val('');
        $('input[type=text]').val('').css("border", '1px solid #ccc');
        $('#fbdiv').find('.dz-image').find('img').attr('src', '')
        $('#twitdiv').find('.dz-image').find('img').attr('src', '');
        $('#Linkdiv').find('.dz-image').find('img').attr('src', '');
        $(".jumbotron").show()
        $("#txtfbImage").hide();
        $("#txttwitterImage").hide();
        $("#txtimagelink").hide();

        var groupID  =  RandomGenerator();
        $('#tabTwitter').attr("data-groupid", groupID)
        $('#tabFacebook').attr("data-groupid", groupID)
        $('#tabLinkedIn').attr("data-groupid", groupID)

        

    });

    $("#txtheadingfb").blur(function () {
        if ($("#txtheadingfb").val() != "") {
            $("#txtheadingfb").css("border", "1px solid #dfdfdf")
        }
    });
    $("#txttitlefb").blur(function () {
        if ($("#txttitlefb").val() != "") {
            $("#txttitlefb").css("border", "1px solid #dfdfdf")
        }
    });
    $("#txtMsgContentfacebook").blur(function () {
        if ($("#txtMsgContentfacebook").val() != "") {
            $("#txtMsgContentfacebook").css("border", "1px solid #dfdfdf")
        }
    });
    $("#UrlfaceBook").blur(function () {
        if ($("#UrlfaceBook").val() != "") {
            $("#UrlfaceBook").css("border", "1px solid #dfdfdf")
        }
    });


    $("#txtheadingtwittr").blur(function () {
        if ($("#txtheadingtwittr").val() != "") {
            $("#txtheadingtwittr").css("border", "1px solid #dfdfdf")
        }
    });
    $("#txttitletwittr").blur(function () {
        if ($("#txttitletwittr").val() != "") {
            $("#txttitletwittr").css("border", "1px solid #dfdfdf")
        }
    });
    $("#txtMsgContenttwitter").blur(function () {
        if ($("#txtMsgContenttwitter").val() != "") {
            $("#txtMsgContenttwitter").css("border", "1px solid #dfdfdf")
        }
    });
    $("#Urltwitter").blur(function () {
        if ($("#Urltwitter").val() != "") {
            $("#Urltwitter").css("border", "1px solid #dfdfdf")
        }
    });


    $("#txtheadingLnkin").blur(function () {
        if ($("#txtheadingLnkin").val() != "") {
            $("#txtheadingLnkin").css("border", "1px solid #dfdfdf")
        }
    });
    $("#txttitleLnkin").blur(function () {
        if ($("#txttitleLnkin").val() != "") {
            $("#txttitleLnkin").css("border", "1px solid #dfdfdf")
        }
    });
    $("#txtMsgContentLinkedIn").blur(function () {
        if ($("#txtMsgContentLinkedIn").val() != "") {
            $("#txtMsgContentLinkedIn").css("border", "1px solid #dfdfdf")
        }
    });

    $("#UrlLinkedIn").blur(function () {
        if ($("#UrlLinkedIn").val() != "") {
            $("#UrlLinkedIn").css("border", "1px solid #dfdfdf")
        }
    });




    //if ($("#UserType").val() != 3) {
    //    if ($("#IndustryId").val() == 1) {
    //        $("option:selected", $("#industry")).val('1');
    //        $('#industry').val('1');
    //        $('#industryfacebook').val('0');
    //        $('#industrytwitter').val('0');
    //        $('#industrylinkedIn').val('0');
    //        $('#industryeditfacebook').val('1');
    //        $('#industryeditTiwtter').val('1');
    //        $('#industryeditLinkedIn').val('1');
    ////        $("#industry option[value='2']").remove();
    //        $("#industry option[value='3']").remove();
    //        $("#industry option[value='4']").remove();
    //        $("#industryfacebook option[value='2']").remove();
    //        $("#industryfacebook option[value='3']").remove();
    //        $("#industryfacebook option[value='4']").remove();
    //        $("#industrytwitter option[value='2']").remove();
    //        $("#industrytwitter option[value='3']").remove();
    //        $("#industrytwitter option[value='4']").remove();
    //        $("#industrylinkedIn option[value='2']").remove();
    //        $("#industrylinkedIn option[value='3']").remove();
    //        $("#industrylinkedIn option[value='4']").remove(); 
    //        $("#industryeditfacebook option[value='2']").remove();
    //        $("#industryeditfacebook option[value='3']").remove();
    //        $("#industryeditfacebook option[value='4']").remove();
    //        $("#industryeditTiwtter option[value='2']").remove();
    //        $("#industryeditTiwtter option[value='3']").remove();
    //        $("#industryeditTiwtter option[value='4']").remove();
    //        $("#industryeditLinkedIn option[value='2']").remove();
    //        $("#industryeditLinkedIn option[value='3']").remove();
    //        $("#industryeditLinkedIn option[value='4']").remove();
    //    }
    //    else if ($("#IndustryId").val() == 2) {
    //        $('#industry').val('2');
    //        $('#industryfacebook').val('0');
    //        $('#industrytwitter').val('0');
    //        $('#industrylinkedIn').val('0');
    //        $("#industry option[value='1']").remove();
    //        $("#industry option[value='3']").remove();
    //        $("#industry option[value='4']").remove();
    //        $('#industryeditfacebook').val('2');
    //        $('#industryeditTiwtter').val('2');
    //        $('#industryeditLinkedIn').val('2');
    //        $("#industryfacebook option[value='1']").remove();
    //        $("#industryfacebook option[value='3']").remove();
    //        $("#industryfacebook option[value='4']").remove();
    //        $("#industrytwitter option[value='1']").remove();
    //        $("#industrytwitter option[value='3']").remove();
    //        $("#industrytwitter option[value='4']").remove();
    //        $("#industrylinkedIn option[value='1']").remove();
    //        $("#industrylinkedIn option[value='3']").remove();
    //        $("#industrylinkedIn option[value='4']").remove();
    //        $("#industryeditfacebook option[value='1']").remove();
    //        $("#industryeditfacebook option[value='3']").remove();
    //        $("#industryeditfacebook option[value='4']").remove();
    //        $("#industryeditTiwtter option[value='1']").remove();
    //        $("#industryeditTiwtter option[value='3']").remove();
    //        $("#industryeditTiwtter option[value='4']").remove();
    //        $("#industryeditLinkedIn option[value='1']").remove();
    //        $("#industryeditLinkedIn option[value='3']").remove();
    //        $("#industryeditLinkedIn option[value='4']").remove();
    //    }
    //    else if ($("#IndustryId").val() == 3) {
    //        $('#industry').val('3');
    //        $('#industryfacebook').val('0');
    //        $('#industrytwitter').val('0');
    //        $('#industrylinkedIn').val('0');
    //        $('#industryeditfacebook').val('3');
    //        $('#industryeditTiwtter').val('3');
    //        $('#industryeditLinkedIn').val('3');

    //        $("#industry option[value='1']").remove();
    //        $("#industry option[value='2']").remove();
    //        $("#industry option[value='4']").remove();

    //        $("#industryfacebook option[value='1']").remove();
    //        $("#industryfacebook option[value='2']").remove();
    //        $("#industryfacebook option[value='4']").remove();
    //        $("#industrytwitter option[value='1']").remove();
    //        $("#industrytwitter option[value='2']").remove();
    //        $("#industrytwitter option[value='4']").remove();
    //        $("#industrylinkedIn option[value='1']").remove();
    //        $("#industrylinkedIn option[value='2']").remove();
    //        $("#industrylinkedIn option[value='4']").remove();
    //        $("#industryeditfacebook option[value='1']").remove();
    //        $("#industryeditfacebook option[value='2']").remove();
    //        $("#industryeditfacebook option[value='4']").remove();
    //        $("#industryeditTiwtter option[value='1']").remove();
    //        $("#industryeditTiwtter option[value='2']").remove();
    //        $("#industryeditTiwtter option[value='4']").remove();
    //        $("#industryeditLinkedIn option[value='1']").remove();
    //        $("#industryeditLinkedIn option[value='2']").remove();
    //        $("#industryeditLinkedIn option[value='4']").remove();
    //    }
    //    else if ($("#IndustryId").val() == 4) {
    //        $('#industry').val('4');
    //        $('#industryfacebook').val('0');
    //        $('#industrytwitter').val('0');
    //        $('#industrylinkedIn').val('0');
    //        $('#industryeditfacebook').val('4');
    //        $('#industryeditTiwtter').val('4');
    //        $('#industryeditLinkedIn').val('4');
    //        $("#industry option[value='1']").remove();
    //        $("#industry option[value='2']").remove();
    //        $("#industry option[value='3']").remove();
    //        $("#industryfacebook option[value='1']").remove();
    //        $("#industryfacebook option[value='2']").remove();
    //        $("#industryfacebook option[value='3']").remove();
    //        $("#industrytwitter option[value='1']").remove();
    //        $("#industrytwitter option[value='2']").remove();
    //        $("#industrytwitter option[value='3']").remove();
    //        $("#industrylinkedIn option[value='1']").remove();
    //        $("#industrylinkedIn option[value='2']").remove();
    //        $("#industrylinkedIn option[value='3']").remove();
    //        $("#industryeditfacebook option[value='1']").remove();
    //        $("#industryeditfacebook option[value='2']").remove();
    //        $("#industryeditfacebook option[value='3']").remove();
    //        $("#industryeditTiwtter option[value='1']").remove();
    //        $("#industryeditTiwtter option[value='2']").remove();
    //        $("#industryeditTiwtter option[value='3']").remove();
    //        $("#industryeditLinkedIn option[value='1']").remove();
    //        $("#industryeditLinkedIn option[value='2']").remove();
    //        $("#industryeditLinkedIn option[value='3']").remove();
    //    }
    //}

    var Param = {};
    var $table = $("#grid");

    Param.UserType = $("#UserType").val();
    Param.IndustryId = $("#IndustryId").val();
    

    $('#resetBtn').on('click', function () {
        debugger;
        $("#custom-toolbar").find('input').val('');
        $("#custom-toolbar").find('select').val("");
        $("#SocialMedia")[0].selectedIndex = 0;
        // $("#SocialMedia").val("");
        $table.bootstrapTable('refresh');
      
        // $("#resetBtn").attr("disabled", "disabled");
    });

    $("#drpArchive").change(function(){
        debugger
      
        $table.bootstrapTable('refresh');
        Param.clickBtn = false;
    });

    $("#btnFilter").click(function () {
        debugger;
      
        //  $("#SocialMedia").val("");
        var description = $("#Description").val();
        var SocialMedia1 = $("option:selected", $("#SocialMedia")).text();
        var CategoryName1 = $("option:selected", $("#CategoryName")).text();
        var PostedBy1 = $("#PostedBy").val();
        var dateFrom = $('#DateFrom').val();
        var dateTo = $('#DateTo').val();
        if (description != "") {
            Param.Description = description;
        }
        else {
            Param.Description = "";
        }
        if (SocialMedia1 != "" && SocialMedia1 != "Social Media" && SocialMedia1 != "0") {
            Param.SocialMedia = SocialMedia1;
        }
        //else {
        //    Param.SocialMedia = "";
        //}
        if (CategoryName1 != "" && CategoryName1 != "Select Industry") {
            Param.CategoryName = CategoryName1;
        }
        else {
            Param.CategoryName = "";
        }
        if (PostedBy1 != "") {
            Param.PostedBy = PostedBy1;
        }
        else {
            Param.PostedBy = "";
        }
        if (dateFrom != "") {
            Param.DateFrom = dateFrom;
        }
        else {
            Param.DateFrom = "1/1/0001";
        }
        if (dateTo != "") {
            Param.DateTo = dateTo;
        }
        else {
            Param.DateTo = "1/1/0001";
        }


        if (dateFrom != "") {
            if (new Date(dateFrom) <= new Date(dateTo)) {
                $table.bootstrapTable('refresh');
                Param.clickBtn = false;
            }
            else {
                BootstrapDialog.alert("Date To should be greater than Date From");
            }
        }
      
        $table.bootstrapTable('refresh');
        Param.clickBtn = false;

    });

    var reqUrl = $_BaseUrl + '/Users/api/AdminApi/GetContentLists';

    $('#grid').bootstrapTable({
        headers: headers,
        method: 'post',
        url: reqUrl,
        cache: true,
        height: 700,
        classes: 'table table-hover',
        customParams: Param,
        queryParams: function (Param) {               
            Param.isArchive=  $("#drpArchive").val();;
            return Param;
        },
        striped: true,
        pageNumber: 1,
        pagination: true,
        pageSize: 10,
        pageList: [5, 10, 20, 30],
        search: false,
        showColumns: true,
        // showRefresh: true,
        sidePagination: 'server',
        minimumCountColumns: 2,
        showHeader: true,
        showFilter: true,
        smartDisplay: true,
        clickToSelect: true,
        // rowStyle: rowStyle,
        toolbar: '#custom-toolbar',
        columns: [
            {
                field: 'ContentId',
                title: '#',
                checkbox: false,
                type: 'search',
                sortable: true,
                visible: false,
                switchable: false,
               
            },
         
                {
                    field: 'ContentSource',
                    title: 'Source',
                    checkbox: false,
                    type: 'search',
                    width: '20',
                    clickToSelect: true,
                    events: operateEvents,
                    formatter: function (value, row, index){
                        if (row.length==1) {
                            RssIndex = 0
                        }
                        else {
                            RssIndex = row.findIndex(x => x.SocialMedia=="Rss");
                        }
                        if (RssIndex==-1) {
                            RssIndex = 0;
                        }
                        
                        var Source = "";
                        if (row[RssIndex].SocialMedia == "Rss" || row[RssIndex].ContentSource == "Rss") {
                             return ["<span class='social-icon-tb'><i class='fa fa-rss'></i></span>"].join('');
                        }
                    }

                },
                {
                    field: 'SocialMedia',
                    title: 'Social Media',
                    checkbox: false,
                    type: 'search',
                    width: '40',
                    clickToSelect: true,
                    events: operateEvents,
                    formatter: function (value, row, index){
                        if (row.length==1) {
                            RssIndex = 0
                        }
                        else {
                            RssIndex = row.findIndex(x => x.SocialMedia=="Rss");
                        }
                        if (RssIndex==-1) {
                            RssIndex = 0;
                        }
                        
                        var SocialMedia =""
                        $(row).each(function (i, v) {

                            ///
                           
                            if (v.SocialMedia == "Facebook") {
                                SocialMedia +=["<button style='text-decoration:none!important;' data-GroupID ="+ v.GroupId +"  data-key ="+ v.ContentId +" class='readttl btn-info read btn btn-link social-icon-tb' title='Read More'><i class='fa fa-facebook'></i></button>"].join('');
                            }
                            else if (v.SocialMedia == "LinkedIn") {
                                SocialMedia += ["<button style='text-decoration:none!important;'   data-GroupID ="+ v.GroupId +" data-key ="+ v.ContentId +" class='readttl btn-info read btn btn-link social-icon-tb' title='Read More'><i class='fa fa-linkedin' aria-hidden='true'></i></button>"].join('');
                            }
                            else if (v.SocialMedia == "Twitter") {
                                SocialMedia +=["<button style='text-decoration:none!important;'  data-GroupID ="+ v.GroupId +"  data-key ="+ v.ContentId +" class='readttl btn-info read btn btn-link social-icon-tb' title='Read More'><i class='fa fa-twitter' aria-hidden='true'></i></span>"].join('');
                            }
                            else if (v.SocialMedia == "Google") {
                                SocialMedia +=["<span class='social-icon-tb'><i class='fa fa-google-plus' aria-hidden='true'></i></span>"].join('');
                            }
                        });
                        return SocialMedia;
                    }

                },
                 {
                     field: 'Title',
                     title: 'Detail',
                     checkbox: false,
                     type: 'search',
                     width: '250',
                     clickToSelect: true,
                     events: operateEvents,
                     formatter: function (value, row, index){
                         if (row.length==1) {
                             RssIndex = 0
                         }
                         else {
                             RssIndex = row.findIndex(x => x.SocialMedia=="Rss");
                         }
                         if (RssIndex==-1) {
                             RssIndex = 0;
                         }
                         var fulltext = row[RssIndex].Title;
                         var shorttexttitle = row[RssIndex].Title.substring(0, 74);
                      
                         var title = [shorttexttitle + '...', '<button style="text-decoration:none!important;"  data-key ="'+ row[RssIndex].ContentId +'" class="readttl btn-info btn btn-link" title="Read More"><i class="fa fa-book"></i></button>'].join('');
                         var URl = "";
                         if (row[RssIndex].Url != null) {
                             debugger;
                             var fulltext = row[RssIndex].Url;
                             debugger
                             if (fulltext.length >= 30) {
                                 var shorttext = fulltext.substring(0, 30);
                                 // return shorttext
                                 URl = ["<a href=" + fulltext + " class='text-info fa fa-link' target='_blank'></a>"].join('');
                             }
                             else {
                                 URl = ["<a href=" + fulltext + " class='text-info fa fa-link' target='_blank'></a>"].join('');
                             }

                         }

                         return shorttexttitle +'&nbsp'+ URl;
                     }

                 },
                      {
                          field: 'PostedDate',
                          title: 'Posted Date',
                          checkbox: false,
                          type: 'search',
                          width: '80',
                          clickToSelect: true,
                          events: operateEvents,
                          formatter: function (value, row, index){
                              if (row.length==1) {
                                  RssIndex = 0
                              }
                              else {
                                  RssIndex = row.findIndex(x => x.SocialMedia=="Rss");
                              }
                              if (RssIndex==-1) {
                                  RssIndex = 0;
                              }
                              var localdate = moment.utc(row[RssIndex].CreatedDate).toDate();
                              return localdate.toString().replace(/GMT.*/g, "");
                          }

                      },
                       {
                           field: 'PostedBy',
                           title: 'Posted By',
                           checkbox: false,
                           type: 'search',
                           width: '70',
                           clickToSelect: true,
                           events: operateEvents,
                           formatter: function (value, row, index){
                               if (row.length==1) {
                                   RssIndex = 0
                               }
                               else {
                                   RssIndex = row.findIndex(x => x.SocialMedia=="Rss");
                               }
                               if (RssIndex==-1) {
                                   RssIndex = 0;
                               }
                               var Role =""
                               debugger;
                               if (row[0].UserType == 1) {
                                   Role= ["User"].join('');
                               }
                               else if (row[0].UserType == 2) {
                                   Role = ["Admin"].join('');
                               }
                               else if (row[0].UserType == 3) {
                                   Role = ["SuperAdmin"].join('');
                               }
                      return row[RssIndex].CreatedByName +'</br>' +Role ;
                           }

                       },
                         {
                             field: 'Category',
                             title: 'Category',
                             checkbox: false,
                             type: 'search',
                             width: '80',
                             clickToSelect: true,
                             events: operateEvents,
                             formatter: function (value, row, index){
                                 if (row.length==1) {
                                     RssIndex = 0
                                 }
                                 else {
                                     RssIndex = row.findIndex(x => x.SocialMedia=="Rss");
                                 }
                                 if (RssIndex==-1) {
                                     RssIndex = 0;
                                 }
                                 if ($("#UserType").val() == 3) {
                                     var Industry = row[RssIndex].CategoryName;

                                     var SubIndustryName = "";
                                     if (row[RssIndex].SubIndustryName == 'Select SubIndustry') {
                                         SubIndustryName = ""
                                     }
                                     else {
                                         SubIndustryName = row[RssIndex].SubIndustryName;
                                     }


                                   return Industry + '</br>' + SubIndustryName;
                                 }
                                 if ($("#UserType").val() == 2) {
                                     var Industry = row[RssIndex].CategoryName;

                                     var SubIndustryName = "";
                                     if (row[RssIndex].SubIndustryName == 'Select SubIndustry') {
                                         SubIndustryName = ""
                                     }
                                     else {
                                         SubIndustryName = row[RssIndex].SubIndustryName;
                                     }


                                     return Industry + '</br>' ;
                                 }
                             }

                         },
                          {
                              field: 'Image',
                              title: 'Image',
                              checkbox: false,
                              type: 'search',
                              width: '60',
                              clickToSelect: true,
                              events: operateEvents,
                              formatter: function (value, row, index){
                                  if (row.length==1) {
                                      RssIndex = 0
                                  }
                                  else {
                                      RssIndex = row.findIndex(x => x.SocialMedia=="Rss");
                                  }
                                  if (RssIndex==-1) {
                                      RssIndex = 0;
                                  }
                                  var ImageURL = '';                                 
                                  if (row[RssIndex].ImageUrl != null && row[RssIndex].ImageUrl.indexOf("undefine") < 0) {
                                      return ["<img src='" + row[RssIndex].ImageUrl + "' height='40px' width='40px' alt='Smiley face' style ='border-radius: 25px;' />"].join('');
                                  }
                                  else {
                                      return ["<img src='" + ImageURL + "/Images/noimage.png" + "' height='40px' width='40px' style ='border-radius: 25px;'/>"].join('');
                                  }
                              }

                          },
                           {
                               field: 'Action',
                               title: 'Action',
                               checkbox: false,
                               type: 'search',
                               width: '150',
                               clickToSelect: true,
                               events: operateEvents,
                               formatter: function (value, row, index){
                                   if (row.length==1) {
                                       RssIndex = 0
                                   }
                                   else {
                                       RssIndex = row.findIndex(x => x.SocialMedia=="Rss");
                                   }
                                   if (RssIndex==-1) {
                                       RssIndex = 0;
                                   }
                              
                                   var Action = "";
                                   if (row[0].IsActive == true) {
                                       Action  =  [
                                           '<button id="editf"  data-GroupID ='+ row[RssIndex].GroupId +' data-key ="' + row[RssIndex].ContentId + '" class="btn btn-link edit" title="Edit">',
                                           '<i class="fa fa-pencil-square-o"></i>',
                                       '</button>',
                                       '<button id="deleteds" data-GroupID ='+ row[RssIndex].GroupId +' data-key ="' + row[RssIndex].ContentId + '" class="btn btn-link delete" title="Delete">',
                                           '<i class="fa fa-trash"></i>',
                                       '</button>',
                                          '<button id="activatex" data-GroupID ='+ row[RssIndex].GroupId +' data-key ="' + row[RssIndex].ContentId + '" class="btn btn-link activate" title="Deactivate">',
                                                   '<i class="fa fa-unlock"></i>',
                                               '</button>',    

                                       ].join('');
                                   }
                                   else {
                                       Action = [
                                             '<button id="" data-GroupID ='+ row[RssIndex].GroupId +' data-key ="' + row[RssIndex].ContentId + '" class="btn btn-link edit" title="Edit">',
                                           '<i class="fa fa-pencil-square-o"></i>',
                                       '</button>',
                                       '<button id="" data-GroupID ='+ row[RssIndex].GroupId +' data-key ="' + row[RssIndex].ContentId + '" class="btn btn-link delete" title="Delete">',
                                           '<i class="fa fa-trash"></i>',
                                       '</button>',
                                                  '<button id="" data-GroupID ='+ row[RssIndex].GroupId +' data-key ="' + row[RssIndex].ContentId + '" class="btn btn-link deactivate"  title="Activate">',
                                                           '<i class="fa fa-lock"></i>',
                                                       '</button>',
                                       ].join('');
                                   }

                         
                                   if (row[0].Archive==false) {
                                       Action  +=  ['<button id="btnArchive" data-GroupID ='+ row[RssIndex].GroupId +' data-key ="' + row[RssIndex].ContentId + '" class="btn btn-link archive" title="Archive">',
                                                                             '<i class="fa fa-archive "></i>',
                                                                         '</button>',
                                       ].join('');
                                   }   
                                   return Action;
                                  
                               }

                           },




       //{
       //    field: 'operate',
       //    title: 'Actions',
       //    clickToSelect: false,
       //    width: '200',
       //    formatter: operateFormatter,
       //    events: operateEvents
       //},

        ],
        onLoadSuccess: function () {
            Addtitle();
            $('.columns.columns-right.btn-group.pull-right').css("margin-top", '56px');
        },
        onPageChange: function () {
            Addtitle();
            $('.columns.columns-right.btn-group.pull-right').css("margin-top", '56px');
            //$('.columns.columns-right.btn-group.pull-right').hide();
        },
    });

    if ($("#UserType").val() != 3) {
        debugger
        $table.bootstrapTable('hideColumn', 'CategoryName');
    }
});

function operateFormatter(value, row, index) {
    debugger;
    if (row.IsActive == true) {
        return [
            '<button id="edit" class="btn btn-link" title="Edit">',
            '<i class="fa fa-pencil-square-o"></i>',
        '</button>',
        '<button id="delete" class="btn btn-link" title="Delete">',
            '<i class="fa fa-trash"></i>',
        '</button>',
           '<button id="activate" class="btn btn-link" title="deactivate">',
                    '<i class="fa fa-unlock"></i>',
                '</button>',


        ].join('');
    }
    else {
        return [
              '<button id="edit" class="btn btn-link" title="Edit">',
            '<i class="fa fa-pencil-square-o"></i>',
        '</button>',
        '<button id="delete" class="btn btn-link" title="Delete">',
            '<i class="fa fa-trash"></i>',
        '</button>',
                   '<button id="deactivate" class="btn btn-link" title="Activate">',
                            '<i class="fa fa-lock"></i>',
                        '</button>',
        ].join('');
    }
}

window.operateEvents = {
    'click .read': function (e, value, row, index) {
        debugger;

        var contentID = $(this).data("key");
        var index = row.findIndex(x => x.ContentId==contentID);

        if (row[index].Url != null) {
            debugger;
            var fulltext = row[index].Url;
            debugger
            if (fulltext.length >= 30) {
                var shorttext = fulltext.substring(0, 30);
                // return shorttext
                var URl = ["<a href=" + fulltext + " class='text-info' target='_blank'>" + shorttext + "</a>"].join('');
            }
            else {
                var URl = ["<a href=" + fulltext + " class='text-info' target='_blank'>" + fulltext + "</a>"].join('');
            }

        }


        if (row[index].Heading != null) {

            var haeading = [row[index].Heading].join('');
        }
        else {
            var haeading = "";
        }

        BootstrapDialog.alert(' <strong>Heading:</strong> ' +
           haeading.trim() + '</br>   </br> <strong>Title:</strong> ' +
            row[index].Title.trim() + '</br>   </br> <strong>Description:</strong> ' +
            row[index].Description.trim() + '</br>   </br> <strong>Link:</strong> ' +
            URl.trim()
            );

    },

    //'click .readttl': function (e, value, row, index) {
    //    debugger;
    //    BootstrapDialog.alert(row.Title);
    //},

    //'click .readHeading': function (e, value, row, index) {
    //    debugger;
    //    BootstrapDialog.alert(row.Heading);
    //},




    'click .delete': function (e, value, row, index) {
        debugger;
        var contentID = $(this).data("key");
        var GroupId  = $(this).data("groupid");
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
                        url: $_BaseUrl + "/Admin/ManageContent/DeleteContent",
                        data: { id: GroupId },
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

    'click .edit': function (e, value, row, index) {
        debugger;
        $('#txtEditMsgContent', $('#editContent')).val('');
        debugger;
        // bind categoryIdEditUrl
        $('#industry option[value=""]').prop("selected", true);
        $("#subIndustry option:selected").text("");       
        $('#Social_Media option[value=""]').prop("selected", true);
        $('#industry', $('#editContent')).removeClass("errorClass").removeAttr("title");
        $('#Social_Media', $('#editContent')).removeClass("errorClass").removeAttr("title");
        $('#txtEditMsgContent', $('#editContent')).removeClass("errorClass").removeAttr("title");
        $('#contentId').val("");
        $('textarea#txtEditMsgContent').val("");
        $('textarea#txtEditMsgContentfacebook').val("");  //.replace(/[^a-z\d\s]+/gi, "")
        $('textarea#txtEditMsgContenttwitter').val("");        
        $('textarea#txtEditMsgContentLinkedIn').val("");
        $('#EditUrl', $('#editContent')).val("");
        $('#EditUrlFacebook', $('#editContentRss')).val("");
        $('#EditUrlTwitter', $('#editContentRss')).val("");
        $('#EditUrlLinkedIn', $('#editContentRss')).val("");
        $('#industryeditfacebook option[value=""]').prop("selected", true);
        $("#stateeditfacebook option:selected").text("");
        $('#industryeditTiwtter option[value=""]').prop("selected", true);
        $("#stateeditTiwtter option:selected").text("");
        $('#industryeditLinkedIn option[value=""]').prop("selected", true);
        $("#stateeditLinkedIn option:selected").text("");
        $("#txtheadingEdit").val("");
        $("#txttitleEdit").val("");
        $("#txtheadingfbEdit").val("");
        $("#txtheadingtwEdit").val("");
        $("#txtheadingliEdit").val("");
        $("#txttitlefbEdit").val("");
        $("#txttitletwEdit").val("");
        $("#txttitleliEdit").val("");
  
        $('.UploadedImagetw img').remove()
        $('.UploadedImagefb img').remove()
        $('.UploadedImageli img').remove()
      

        //if (row.length==1) {
        //    var contentID = $(this).data("key");
        //    var index = row.findIndex(x => x.ContentId==contentID);
        //    bindDetailSingle(row[index])
        //}


        var CheckLinkedIN  = row.findIndex(x => x.SocialMedia=="LinkedIn");       
        var CheckFaceBook  = row.findIndex(x => x.SocialMedia=="Facebook");       
        var CheckTwitter  = row.findIndex(x => x.SocialMedia=="Twitter");
      
        var RssIndex = row.findIndex(x => x.SocialMedia=="Rss"); 

        if (RssIndex==-1) {
            if (CheckLinkedIN>=0) {
                BindLinkedInDetail(row[CheckLinkedIN]);
            }
            
            if (CheckFaceBook>=0) {
                BindFacebookDetail(row[CheckFaceBook]);
            }
           
            if (CheckTwitter>=0) {
                BindTwitterDetail(row[CheckTwitter]);
            }
             
            $("#editContentRss").modal("show");
        }
        if (RssIndex >=0) {
            if (CheckLinkedIN>=0) {
                BindLinkedInDetail(row[CheckLinkedIN]);
            }
            else {
                BindLinkedInDetail(row[RssIndex]);
            }
            if (CheckFaceBook>=0) {
                BindFacebookDetail(row[CheckFaceBook]);
            }
            else {
                BindFacebookDetail(row[RssIndex]);
            }
            if (CheckTwitter>=0) {
                BindTwitterDetail(row[CheckTwitter]);
            }
            else {
                BindTwitterDetail(row[RssIndex]);
            }
            $("#editContentRss").modal("show");
        }
      



    
        // Hide loader
        // $('.loadercontainingdiv').hide();

        //$.ajax({
        //    cache: false,
        //    async: true,
        //    type: "POST",
        //    url: $_BaseUrl + "/Admin/ManageContent/EditContent",
        //    data: { id: row.ContentId },
        //    success: function (data) {
        //        debugger;
        //        $('.loadercontainingdiv').hide();
        //        $('#contentId').val(row.ContentId);
        //        if (data.result != null) {
        //            $("#editContent").modal("show");
        //            $('textarea#txtEditMsgContent').html(data.result.Description);
        //            $('#EditUrl').html(data.result.Url);

        //            $('#categoryList option', $('#editContent')).each(function () {
        //                debugger;
        //                var value = $(this).val();
        //                if (value == data.result.CategoryId && value != "Undefinded") {
        //                    $(this).attr('selected', true);
        //                    // $('#categoryList').trigger('change');
        //                }
        //            })

        //            $('#Social_Media option').each(function () {
        //                var value = $(this).text();
        //                if (value == data.result.SocialMedia) {
        //                    $(this).attr('selected', true);
        //                }
        //            });


        //            if (data.result.ImageUrl != null) {
        //                debugger;
        //                //$('form#dropzoneForm').children().remove();
        //                var url = $_BaseUrl + data.result.ImageUrl;
        //                // $('.UploadedImage').show();
        //                $('.UploadedImage img').remove();
        //                $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="" src="' + url + '"/></div></div>');
        //            }
        //            else {
        //                $('.UploadedImage img').remove();
        //            }
        //        }
        //    },

        //    error: function (request, error) {
        //        $('.loadercontainingdiv').hide();
        //        if (request.statusText == "CustomMessage") {
        //            $("#spanError").html(request.responseText)
        //        }
        //    },
        //    headers: {
        //        'RequestVerificationToken': $("#TokenValue").val()
        //    }
        //});
    },

    'click .activate': function (e, value, row, index) {
        debugger;
        var contentID = $(this).data("key");
        var GroupId  = $(this).data("groupid");
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
                        url: $_BaseUrl + "/Admin/ManageContent/UpdateContentStatusDeactive/" + GroupId,
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

    'click .deactivate': function (e, value, row, index) {

        debugger;
        var contentID = $(this).data("key");
        var GroupId  = $(this).data("groupid");
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
                        url: $_BaseUrl + "/Admin/ManageContent/UpdateContentStatusActive/" + GroupId,
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
    'click .archive': function (e, value, row, index) {

        debugger;
        var contentID = $(this).data("key");
        var GroupId  = $(this).data("groupid");
        var status = false;
        BootstrapDialog.show({
            title: 'Confirmation',
            message: 'Are you sure you want to archive this record?',
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
                        url: $_BaseUrl + "/Admin/ManageContent/ArchiveContentEnable/" + GroupId,
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




function BindLinkedInDetail(linkedInDEtail)
{
    debugger
     
    ContentSourceName=linkedInDEtail.ContentSource;
    $("#txtGroupId").val(linkedInDEtail.GroupId)
    var id = linkedInDEtail.CategoryId;
    var state = $("#state");
    $.ajax({
        url: $_GetStateByCountry,
        type: 'Get',
        data: { IndustryId: id },
        async: false,
        contentType: "application/json; charset=utf-8",           
        success: function (data) {
            debugger
            if (data != null) {
                var $html = '<option selected="selected">Select SubIndustry</option>';
                $(data).each(function (index, item) {
                    $html += '<option value="' + item.id + '" ' + (item.Selected ? 'selected="selected"' : '') + '>' + item.name + '</option>';
                });
                $("#subIndustry").html($html);                
                $("#stateeditLinkedIn").html($html);//refresh the plugin to show new values in dropdown                   
            }
        },
    });

      
    $('#txtEditMsgContent', $('#editContent')).val('');
    debugger;
    // bind categoryIdEditUrl
    $('#industry option[value="' + linkedInDEtail.CategoryId + '"]').prop("selected", true);
    $("#subIndustry option:selected").text(linkedInDEtail.SubIndustryName);        
    // Bind social id
    $('#Social_Media option[value="' + linkedInDEtail.SocialMedia + '"]').prop("selected", true);
    $('#industry', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#Social_Media', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#txtEditMsgContent', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#contentId').val(linkedInDEtail.ContentId);       
    $('textarea#txtEditMsgContentLinkedIn').val($.parseHTML(linkedInDEtail.Description)[0].data);
    $('#EditUrl', $('#editContent')).val(linkedInDEtail.Url);      
    $('#EditUrlLinkedIn', $('#editContentRss')).val(linkedInDEtail.Url);    
    $('#industryeditLinkedIn option[value="' + linkedInDEtail.CategoryId + '"]').prop("selected", true);
    $("#stateeditLinkedIn option:selected").text(linkedInDEtail.SubIndustryName);
    $("#txtheadingEdit").val(linkedInDEtail.Heading);
    $("#txttitleEdit").val(linkedInDEtail.Title);       
    $("#txtheadingliEdit").val(linkedInDEtail.Heading);       
    $("#txttitleliEdit").val(linkedInDEtail.Title);
    ///$("#txtEditimageUrl").val(row.ImageUrl);





    var social = $('#Social_Media option:selected').val();
    var charsleft = $('#txtEditMsgContent', $('#editContent')).val().length;
    var left = 0;
    if (social == "Facebook") {
        left = 63206 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(63206 - charsleft);
        charEdit = left;
    }
    else if (social == "LinkedIn") {
        left = 2000 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(2000 - charsleft);
        charEdit = left;
    }
    else if (social == "Twitter") {
        left = 140 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(140 - charsleft);
        charEdit = left;
    }
    else if (social == "Google") {
        left = 100000 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(100000 - charsleft);
        charEdit = left;
    }

    var charsleft = $('#txtEditMsgContentfacebook', $('#editContentRss')).val().length;
    charEdit = 63206;
    var left = 63206 - charsleft;
    $('#charNumEditRssfacebook').text("Characters remaining ").append(63206 - charsleft);
    charEdit = left;

    // Category List


    // Image 

    if (linkedInDEtail.ImageUrl != null) {
      

        var parsedURL=UrlParser(linkedInDEtail.ImageUrl);
        if (parsedURL.origin==undefined || parsedURL.origin ==null ) {
            var url = $_BaseUrl + linkedInDEtail.ImageUrl;          
            $('.UploadedImageli img').remove();
            $('.UploadedImageli').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="'+linkedInDEtail.ImageUrl+'" src="' + url + '"/></div></div>');
        }
        else {
            $('.UploadedImageli img').remove();
            $('.UploadedImageli').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="'+linkedInDEtail.ImageUrl+'" src="' + linkedInDEtail.ImageUrl + '"/></div></div>');
        }
        // debugger
        //if (linkedInDEtail.ImageUrl.indexOf("www") > -1 || linkedInDEtail.ImageUrl.indexOf("http") > -1) {
        //    $('.UploadedImageli img').remove();
        //    $('.UploadedImageli').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="'+linkedInDEtail.ImageUrl+'" src="' + linkedInDEtail.ImageUrl + '"/></div></div>');
        //}
        //else {
           
        
        //}
          
    
              
    }
    else {
        $('.UploadedImageli img').remove();
    }
    //if (row[index].SocialMedia != 'Rss') {
    //    $("#editContent").modal("show");
    //}
    //else {
    //    $("#editContentRss").modal("show");
    //}
      
    //$("#editContentRss").modal("show");
        

        
   
}
function BindFacebookDetail(facebookDetail)
{
   
    debugger;
      
    ContentSourceName=facebookDetail.ContentSource;
    $("#txtGroupId").val(facebookDetail.GroupId)
    var id = facebookDetail.CategoryId;
    var state = $("#state");
    $.ajax({
        url: $_GetStateByCountry,
        type: 'Get',
        data: { IndustryId: id },
        async: false,
        contentType: "application/json; charset=utf-8",          
        success: function (data) {
            debugger
            if (data != null) {
                var $html = '<option selected="selected">Select SubIndustry</option>';
                $(data).each(function (index, item) {
                    $html += '<option value="' + item.id + '" ' + (item.Selected ? 'selected="selected"' : '') + '>' + item.name + '</option>';
                });
                $("#subIndustry").html($html);
                $("#stateeditfacebook").html($html);
                        
            }
        },
    });

    //Remove everything if alreay selected to prevent two option selected at the same time,
    //It is removeing selected option on previous open thus will select nothing if no categoryId is retrive from row.
    //  $('select option').removeAttr("selected");
    $('#txtEditMsgContent', $('#editContent')).val('');
    debugger;
    // bind categoryIdEditUrl
    $('#industry option[value="' + facebookDetail.CategoryId + '"]').prop("selected", true);
    $("#subIndustry option:selected").text(facebookDetail.SubIndustryName);      
    $('#Social_Media option[value="' + facebookDetail.SocialMedia + '"]').prop("selected", true);
    $('#industry', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#Social_Media', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#txtEditMsgContent', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#contentId').val(facebookDetail.ContentId);
    $('textarea#txtEditMsgContent').val($.parseHTML(facebookDetail.Description)[0].data);
    $('textarea#txtEditMsgContentfacebook').val($.parseHTML(facebookDetail.Description)[0].data);  //.replace(/[^a-z\d\s]+/gi, "")        
    //  $('textarea#txtEditMsgContentLinkedIn').val($.parseHTML(facebookDetail.Description)[0].data);
    $('#EditUrl', $('#editContent')).val(facebookDetail.Url);
    $('#EditUrlFacebook', $('#editContentRss')).val(facebookDetail.Url);      
    $('#industryeditfacebook option[value="' + facebookDetail.CategoryId + '"]').prop("selected", true);
    $("#stateeditfacebook option:selected").text(facebookDetail.SubIndustryName);     
    $("#txtheadingEdit").val(facebookDetail.Heading);
    $("#txttitleEdit").val(facebookDetail.Title);
    $("#txtheadingfbEdit").val(facebookDetail.Heading);
     
    $("#txttitlefbEdit").val(facebookDetail.Title);
     
    ///$("#txtEditimageUrl").val(row.ImageUrl);





    var social = $('#Social_Media option:selected').val();
    var charsleft = $('#txtEditMsgContent', $('#editContent')).val().length;
    var left = 0;
    if (social == "Facebook") {
        left = 63206 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(63206 - charsleft);
        charEdit = left;
    }
    else if (social == "LinkedIn") {
        left = 2000 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(2000 - charsleft);
        charEdit = left;
    }
    else if (social == "Twitter") {
        left = 140 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(140 - charsleft);
        charEdit = left;
    }
    else if (social == "Google") {
        left = 100000 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(100000 - charsleft);
        charEdit = left;
    }

    var charsleft = $('#txtEditMsgContentfacebook', $('#editContentRss')).val().length;
    charEdit = 63206;
    var left = 63206 - charsleft;
    $('#charNumEditRssfacebook').text("Characters remaining ").append(63206 - charsleft);
    charEdit = left;

    // Category List


    // Image 

    //if (facebookDetail.ImageUrl != null) {

    //    debugger

    //    if (facebookDetail.ImageUrl.indexOf("www") > -1 || facebookDetail.ImageUrl.indexOf("http") > -1) {

    //        $('.UploadedImage img').remove();
    //        $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="fromFetchURL" src="' + facebookDetail.ImageUrl + '"/></div></div>');
    //    }
    //    else {

      
    //        var url = $_BaseUrl + facebookDetail.ImageUrl;
          
    //        $('.UploadedImage img').remove();
    //        $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="' + facebookDetail.ImageUrl.split('/')[4] + '" src="' + url + '"/></div></div>');
          
    //    }
    //}
    //else {
    //    $('.UploadedImage img').remove();
    //}

   
    if (facebookDetail.ImageUrl != null) {

        var parsedURL=UrlParser(facebookDetail.ImageUrl);
        if (parsedURL.origin==undefined || parsedURL.origin ==null ) {
            var url = $_BaseUrl + facebookDetail.ImageUrl;          
            $('.UploadedImagefb img').remove();
            $('.UploadedImagefb').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="'+facebookDetail.ImageUrl+'" src="' + url + '"/></div></div>');
        }
        else {
            $('.UploadedImagefb img').remove();
            $('.UploadedImagefb').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="'+facebookDetail.ImageUrl+'" src="' + facebookDetail.ImageUrl + '"/></div></div>');
        }
        
    }
    else {
        $('.UploadedImagefb img').remove();
    }




    //if (row[index].SocialMedia != 'Rss') {
    //    $("#editContent").modal("show");
    //}
    //else {
    //    $("#editContentRss").modal("show");
    //}
      
    //   $("#editContentRss").modal("show");
        

}
function BindTwitterDetail(TwitterDetail)
{
   
    debugger;
    ContentSourceName=TwitterDetail.ContentSource;   
    var id = TwitterDetail.CategoryId;
    var state = $("#state");
    $.ajax({
        url: $_GetStateByCountry,
        type: 'Get',
        data: { IndustryId: id },
        async: false,
        contentType: "application/json; charset=utf-8",
        //data: { IndustryId: $("option:selected", $("#IndustryId")).val() },
        success: function (data) {
            debugger
            if (data != null) {
                var $html = '<option selected="selected">Select SubIndustry</option>';
                $(data).each(function (index, item) {
                    $html += '<option value="' + item.id + '" ' + (item.Selected ? 'selected="selected"' : '') + '>' + item.name + '</option>';
                });
                $("#subIndustry").html($html);                   
                $("#stateeditTiwtter").html($html);
                   
            }
        },
    });

    //Remove everything if alreay selected to prevent two option selected at the same time,
    //It is removeing selected option on previous open thus will select nothing if no categoryId is retrive from row.
    //  $('select option').removeAttr("selected");
    $('#txtEditMsgContent', $('#editContent')).val('');
    debugger;
    // bind categoryIdEditUrl
    $('#industry option[value="' + TwitterDetail.CategoryId + '"]').prop("selected", true);
    $("#subIndustry option:selected").text(TwitterDetail.SubIndustryName);       
    $('#Social_Media option[value="' + TwitterDetail.SocialMedia + '"]').prop("selected", true);
    $('#industry', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#Social_Media', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#txtEditMsgContent', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#contentId').val(TwitterDetail.ContentId);
    $('textarea#txtEditMsgContent').val($.parseHTML(TwitterDetail.Description)[0].data);
    if (TwitterDetail.Description.length > 140) {
        $('textarea#txtEditMsgContenttwitter').val($.parseHTML(TwitterDetail.Description.substring(0, 140))[0].data);
    }
    else {
        $('textarea#txtEditMsgContenttwitter').val($.parseHTML(TwitterDetail.Description)[0].data);
    }

    $('#EditUrl', $('#editContent')).val(TwitterDetail.Url);
     
    $('#EditUrlTwitter', $('#editContentRss')).val(TwitterDetail.Url);
     
    $('#industryeditTiwtter option[value="' + TwitterDetail.CategoryId + '"]').prop("selected", true);
    $("#stateeditTiwtter option:selected").text(TwitterDetail.SubIndustryName);
      
    $("#txtheadingEdit").val(TwitterDetail.Heading);
    $("#txttitleEdit").val(TwitterDetail.Title);
      
    $("#txtheadingtwEdit").val(TwitterDetail.Heading);
       
    $("#txttitletwEdit").val(TwitterDetail.Title);
    $("#txtGroupId").val(TwitterDetail.GroupId)






    var social = $('#Social_Media option:selected').val();
    var charsleft = $('#txtEditMsgContent', $('#editContent')).val().length;
    var left = 0;
    if (social == "Facebook") {
        left = 63206 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(63206 - charsleft);
        charEdit = left;
    }
    else if (social == "LinkedIn") {
        left = 2000 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(2000 - charsleft);
        charEdit = left;
    }
    else if (social == "Twitter") {
        left = 140 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(140 - charsleft);
        charEdit = left;
    }
    else if (social == "Google") {
        left = 100000 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(100000 - charsleft);
        charEdit = left;
    }

    var charsleft = $('#txtEditMsgContentfacebook', $('#editContentRss')).val().length;
    charEdit = 63206;
    var left = 63206 - charsleft;
    $('#charNumEditRssfacebook').text("Characters remaining ").append(63206 - charsleft);
    charEdit = left;

      

    // Image 
    if (TwitterDetail.ImageUrl != null) {

        var parsedURL=UrlParser(TwitterDetail.ImageUrl);
        if (parsedURL.origin==undefined || parsedURL.origin ==null ) {
            var url = $_BaseUrl + TwitterDetail.ImageUrl;          
            $('.UploadedImagetw img').remove();
            $('.UploadedImagetw').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="'+TwitterDetail.ImageUrl+'" src="' + url + '"/></div></div>');
        }
        else {
            $('.UploadedImagetw img').remove();
            $('.UploadedImagetw').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="'+TwitterDetail.ImageUrl+'" src="' + TwitterDetail.ImageUrl + '"/></div></div>');
        }
        
    }
    else {
        $('.UploadedImagetw img').remove();
    }
    //if (TwitterDetail.ImageUrl != null) {
    //    debugger

    //    if (TwitterDetail.ImageUrl.indexOf("www") > -1 || TwitterDetail.ImageUrl.indexOf("http") > -1) {
    //        $('.UploadedImagetw img').remove();
    //        $('.UploadedImagetw').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="'+TwitterDetail.ImageUrl+'" src="' + TwitterDetail.ImageUrl + '"/></div></div>');
    //    }
    //    else
    //    {
    //        var url = $_BaseUrl + TwitterDetail.ImageUrl;
          
    //        $('.UploadedImagetw img').remove();
    //        $('.UploadedImagetw').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="' + TwitterDetail.ImageUrl + '" src="' + url + '"/></div></div>');
        
    //    }

          
     
    //}
    //else {
    //    $('.UploadedImagetw img').remove();
    //}
    //if (row[index].SocialMedia != 'Rss') {
    //    $("#editContent").modal("show");
    //}
    //else {
    //    $("#editContentRss").modal("show");
    //}
      
    //  $("#editContentRss").modal("show");
        

}
function bindDetailSingle (data)
{
    $("#txtGroupId").val(data.GroupId)
    var id = data.CategoryId;
    var state = $("#state");
    $.ajax({
        url: $_GetStateByCountry,
        type: 'Get',
        data: { IndustryId: id },
        async: false,
        contentType: "application/json; charset=utf-8",
        //data: { IndustryId: $("option:selected", $("#IndustryId")).val() },
        success: function (data) {
            debugger
            if (data != null) {
                var $html = '<option selected="selected">Select SubIndustry</option>';
                $(data).each(function (index, item) {
                    $html += '<option value="' + item.id + '" ' + (item.Selected ? 'selected="selected"' : '') + '>' + item.name + '</option>';
                });
                $("#subIndustry").html($html);
                $("#stateeditfacebook").html($html);
                $("#stateeditTiwtter").html($html);
                $("#stateeditLinkedIn").html($html);//refresh the plugin to show new values in dropdown                   
            }
        },
    });

    //Remove everything if alreay selected to prevent two option selected at the same time,
    //It is removeing selected option on previous open thus will select nothing if no categoryId is retrive from row.
    //  $('select option').removeAttr("selected");
    $('#txtEditMsgContent', $('#editContent')).val('');
    debugger;
    // bind categoryIdEditUrl
    $('#industry option[value="' + data.CategoryId + '"]').prop("selected", true);
    $("#subIndustry option:selected").text(data.SubIndustryName);
    // $('#subIndustry').find('option:selected').text(row.SubIndustryName);
    //  $('#subIndustry option[text="' + row.SubIndustryName + '"]').prop("selected", true);
    //$('#industrys option[value="' + row.CategoryName + '"]').prop("selected", true);
    // Bind social id
    $('#Social_Media option[value="' + data.SocialMedia + '"]').prop("selected", true);
    $('#industry', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#Social_Media', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#txtEditMsgContent', $('#editContent')).removeClass("errorClass").removeAttr("title");
    $('#contentId').val(data.ContentId);
    $('textarea#txtEditMsgContent').val($.parseHTML(data.Description)[0].data);
    $('textarea#txtEditMsgContentfacebook').val($.parseHTML(data.Description)[0].data);  //.replace(/[^a-z\d\s]+/gi, "")
    if (data.Description.length > 140) {
        $('textarea#txtEditMsgContenttwitter').val($.parseHTML(data.Description.substring(0, 140))[0].data);
    }
    else {
        $('textarea#txtEditMsgContenttwitter').val($.parseHTML(data.Description)[0].data);
    }
    $('textarea#txtEditMsgContentLinkedIn').val($.parseHTML(data.Description)[0].data);
    $('#EditUrl', $('#editContent')).val(data.Url);
    $('#EditUrlFacebook', $('#editContentRss')).val(data.Url);
    $('#EditUrlTwitter', $('#editContentRss')).val(data.Url);
    $('#EditUrlLinkedIn', $('#editContentRss')).val(data.Url);
    $('#industryeditfacebook option[value="' + data.CategoryId + '"]').prop("selected", true);
    $("#stateeditfacebook option:selected").text(data.SubIndustryName);
    $('#industryeditTiwtter option[value="' + data.CategoryId + '"]').prop("selected", true);
    $("#stateeditTiwtter option:selected").text(data.SubIndustryName);
    $('#industryeditLinkedIn option[value="' + data.CategoryId + '"]').prop("selected", true);
    $("#stateeditLinkedIn option:selected").text(data.SubIndustryName);
    $("#txtheadingEdit").val(data.Heading);
    $("#txttitleEdit").val(data.Title);
    $("#txtheadingfbEdit").val(data.Heading);
    $("#txtheadingtwEdit").val(data.Heading);
    $("#txtheadingliEdit").val(data.Heading);
    $("#txttitlefbEdit").val(data.Title);
    $("#txttitletwEdit").val(data.Title);
    $("#txttitleliEdit").val(data.Title);
  
    ///$("#txtEditimageUrl").val(row.ImageUrl);





    var social = $('#Social_Media option:selected').val();
    var charsleft = $('#txtEditMsgContent', $('#editContent')).val().length;
    var left = 0;
    if (social == "Facebook") {
        left = 63206 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(63206 - charsleft);
        charEdit = left;
    }
    else if (social == "LinkedIn") {
        left = 2000 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(2000 - charsleft);
        charEdit = left;
    }
    else if (social == "Twitter") {
        left = 140 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(140 - charsleft);
        charEdit = left;
    }
    else if (social == "Google") {
        left = 100000 - charsleft;
        $('#charNumEdit').text("Characters remaining ").append(100000 - charsleft);
        charEdit = left;
    }

    var charsleft = $('#txtEditMsgContentfacebook', $('#editContentRss')).val().length;
    charEdit = 63206;
    var left = 63206 - charsleft;
    $('#charNumEditRssfacebook').text("Characters remaining ").append(63206 - charsleft);
    charEdit = left;

    // Category List


    // Image 

    if (data.ImageUrl != null) {

        debugger

        if (data.ImageUrl.indexOf("www") > -1 || data.ImageUrl.indexOf("http") > -1) {

            $('.UploadedImagef img').remove();
            $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="fromFetchURL" src="' + data.ImageUrl + '"/></div></div>');
        }
        else {

            //if (row.SocialMedia != "Rss" && row.ContentSource != 'Rss') {
            //$('form#dropzoneForm').children().remove();
            var url = $_BaseUrl + data.ImageUrl;
            // $('.UploadedImage').show();
            $('.UploadedImage img').remove();
            $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="' + data.ImageUrl.split('/')[4] + '" src="' + url + '"/></div></div>');
            //}
            //else if (row.SocialMedia == "Rss" || row.ContentSource == 'Rss') {
            //    var url = row.ImageUrl;
            //    // $('.UploadedImage').show();
            //    $('.UploadedImage img').remove();
            //    $('.UploadedImage').append('<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail height="108px" width="108px" alt="" src="' + url + '"/></div></div>');

            //}
        }




    }
    else {
        $('.UploadedImage img').remove();
    }
    if (data.SocialMedia != 'Rss') {
        $("#editContent").modal("show");
    }
    else {
        $("#editContentRss").modal("show");
    }
}
function UrlParser (str) {
    "use strict";

    var regx = /^(((([^:\/#\?]+:)?(?:(\/\/)((?:(([^:@\/#\?]+)(?:\:([^:@\/#\?]+))?)@)?(([^:\/#\?\]\[]+|\[[^\/\]@#?]+\])(?:\:([0-9]+))?))?)?)?((\/?(?:[^\/\?#]+\/+)*)([^\?#]*)))?(\?[^#]+)?)(#.*)?/,
        matches = regx.exec(str),
        parser = null;

    if (null !== matches) {
        parser = {
            href              : matches[0],
            withoutHash       : matches[1],
            url               : matches[2],
            origin            : matches[3],
            protocol          : matches[4],
            protocolseparator : matches[5],
            credhost          : matches[6],
            cred              : matches[7],
            user              : matches[8],
            pass              : matches[9],
            host              : matches[10],
            hostname          : matches[11],
            port              : matches[12],
            pathname          : matches[13],
            segment1          : matches[14],
            segment2          : matches[15],
            search            : matches[16],
            hash              : matches[17]
        };
    }

    return parser;
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
    $messageData = $("<span>Description</span>");
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
                $("#addContent").modal('hide');
            }
        }]
    });
}
function RandomGenerator() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 9; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}
var $table = $("#grid");
var chk = false;

function RefreshGrid() {
    document.getElementById("btnFilter").click();
}

var charEdit = 0;
$('#Social_Media', $('#editContent')).on('change', function () {
    debugger;
    var selectedText = $(this).find("option:selected").text();
    if (selectedText == 'Facebook')
        charEdit = 63206;
    else if (selectedText == 'LinkedIn')
        charEdit = 2000;
    else if (selectedText == 'Twitter')
        charEdit = 140;
    else if (selectedText == 'Google+')
        charEdit = 100000;
    //alert($('#txtEditMsgContent').val().length);
    var charremain = $('#txtEditMsgContent').val().length;
    if (charremain > 0) {
        var newchar = parseInt(charEdit) - parseInt(charremain);
        //alert(newchar);
        $('#charNumEdit').text("Characters remaining " + newchar);
        charEdit = newchar;
        //alert(charEdit);
    }
    else {
        $('#charNumEdit').text("Characters remaining " + charEdit);
    }

});

function countCharEdit(val) {
    debugger;
    //alert(charEdit);
    var len = val.value.length;
    if (len >= charEdit) {
        val.value = val.value.substring(0, charEdit);
    } else {
        $('#charNumEdit').text("Characters remaining ").append(charEdit - len);
    }
};

