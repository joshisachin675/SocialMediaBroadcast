<!DOCTYPE html>
<html lang="en">

<head>
    <style type="text/css">
        .pac-container {
            background-color: #fff;
            position: absolute!important;
            z-index: 1000;
            border-radius: 2px;
            border-top: 1px solid #d9d9d9;
            font-family: Arial, sans-serif;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            overflow: hidden
        }
        
        .pac-logo:after {
            content: "";
            padding: 1px 1px 1px 0;
            height: 16px;
            text-align: right;
            display: block;
            background-image: url(https://maps.gstatic.com/mapfiles/api-3/images/powered-by-google-on-white3.png);
            background-position: right;
            background-repeat: no-repeat;
            background-size: 120px 14px
        }
        
        .hdpi.pac-logo:after {
            background-image: url(https://maps.gstatic.com/mapfiles/api-3/images/powered-by-google-on-white3_hdpi.png)
        }
        
        .pac-item {
            cursor: default;
            padding: 0 4px;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
            line-height: 30px;
            text-align: left;
            border-top: 1px solid #e6e6e6;
            font-size: 11px;
            color: #999
        }
        
        .pac-item:hover {
            background-color: #fafafa
        }
        
        .pac-item-selected,
        .pac-item-selected:hover {
            background-color: #ebf2fe
        }
        
        .pac-matched {
            font-weight: 700
        }
        
        .pac-item-query {
            font-size: 13px;
            padding-right: 3px;
            color: #000
        }
        
        .pac-icon {
            width: 15px;
            height: 20px;
            margin-right: 7px;
            margin-top: 6px;
            display: inline-block;
            vertical-align: top;
            background-image: url(https://maps.gstatic.com/mapfiles/api-3/images/autocomplete-icons.png);
            background-size: 34px
        }
        
        .hdpi .pac-icon {
            background-image: url(https://maps.gstatic.com/mapfiles/api-3/images/autocomplete-icons_hdpi.png)
        }
        
        .pac-icon-search {
            background-position: -1px -1px
        }
        
        .pac-item-selected .pac-icon-search {
            background-position: -18px -1px
        }
        
        .pac-icon-marker {
            background-position: 1px -161px
        }
        
        .pac-item-selected .pac-icon-marker {
            background-position: -18px -161px
        }
        
        .pac-placeholder {
            color: gray
        }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">

    <!-- Mobile Meta -->
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1">

    <!-- Description and Keywords Meta -->
    <meta name="description" content="Multi purpose landing page template">
    <meta name="keywords" content="landing page, responsive">

    <!-- Page Title -->
    <title>Test</title>


    <!-- Google Fonts -->
    <link href="http://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic,700italic%7cOpen+Sans:300italic,400italic,600italic,700italic,400,300,600,700" rel="stylesheet" type="text/css">

    <!-- Framework and asset stylesheets -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.2.3/css/simple-line-icons.min.css">
    <link href="../Scripts/BootStrapDailog/bootstrap-dialog.min.css" rel="stylesheet" />
    
    <!-- Main stylesheet -->
    <link rel="stylesheet" href="css/homevalue-style.css">

    <!--[if lt IE 9]>
            <link rel="stylesheet" href="/css/ie.css">
            <script src="js/html5.js"></script>
        <![endif]-->
    <style>
        .parallax-1 {
            background-image: url(images/bg.jpg);
        }
        
        .parallax-2 {
            background-image: url(../images/user/parallax2.jpg);
        }
        
        .parallax-3 {
            background-image: url(../images/user/parallax3.jpg);
        }
        
        .parallax-4 {
            background-image: url(../images/user/parallax4.jpg);
        }
        
        .text-highlight {
            color: #CC0000;
        }
        
        .text-inverted {
            background: #CC0000;
        }
        
        .bar-center:after,
        .bar-left:after,
        .bar-right:after,
        .dbar-center:after,
        .dbar-left:after,
        .dbar-right:after {
            border-bottom: 2px solid #CC0000;
        }
        
        .dbar-center:after,
        .dbar-left:after,
        .dbar-right:after {
            border-bottom: 4px double #CC0000;
        }
        
        #footer a:hover {
            color: #CC0000;
        }
        
        .navbar-default .navbar-nav > .active > a,
        .navbar-default .navbar-nav > .active > a:hover,
        .navbar-default .navbar-nav > .active > a:focus {
            color: #CC0000;
        }
        
        .navbar-inverse .navbar-nav > .active > a,
        .navbar-inverse .navbar-nav > .active > a:hover,
        .navbar-inverse .navbar-nav > .active > a:focus {
            background: #CC0000;
        }
        
        .btn-main,
        .btn-main:hover,
        .btn-main:active,
        .btn-main:focus {
            background-color: #CC0000;
        }
        
        .btn-alt,
        .btn-alt:hover,
        .btn-alt:active,
        .btn-alt:focus {
            color: #fff;
            background-color: #37c;
        }
        
        .feature-list .fa {
            background: #CC0000;
        }
        
        .feature-list .icon {
            color: #CC0000;
        }
        
        .link-list-x a:hover,
        .link-list-y a:hover {
            color: #CC0000;
        }
        
        .scroll-to-top a:hover {
            background: #CC0000;
        }
        
        .nav-tabs > li.active > a,
        .nav-tabs > li.active > a:hover,
        .nav-tabs > li.active > a:focus {
            border-top: 2px solid #CC0000;
        }
        
        .tog-title,
        .tog-title:hover,
        .panel-title > a,
        .panel-title > a:hover {
            border-left-color: #CC0000;
        }
        
        .address-details .form-group {
            padding: 15px 0;
        }
        
        .address-details .has-bottom {
            margin-top: 20px;
        }
        
        .step1 #customPrevButton {
            display: none !important;
        }
        
        .step2 #customNextButton,
        .step3 #reg_submit {
            float: right;
        }
        
        #locationField {
            display: flex;
            border: 1px solid rgba(0, 0, 0, .15);
            border-right: none;
        }
        
        .marker {
            color: gray;
            border-radius: 2px 0 0 2px;
            margin-top: 12px;
            z-index: 1000;
        }
        
        .address {
            border-left: none;
            border-radius: 0 2px 2px 0;
            padding-left: 26px;
            margin-left: 0;
        }
    </style>
</head>

<body class="fixed-nav enable-sticky-nav" cz-shortcut-listen="true">

    <input type="hidden" id="PostId" value="<%=Request.querystring("id")%>" />

    <!-- Main container page -->
    <div id="page" class="site clearfix">
        <img src="images/bg.jpg" alt="background" class="background-image">
        <!-- Top navbar with logo and links -->
        <div class="navbar navbar-inverse navbar-fixed-top nav-sticky" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                    <a class="navbar-brand" href="http://url"><span class="text-highlight"><i class="fa fa-calculator"></i> HOME </span>VALUES</a>
                </div>
                <!-- /.navbar-header -->
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav navbar-right">
                        <li class=""><a href="/">Home 1</a></li>
                        <li class=""><a href="#features">Benefits</a></li>
                        <li class=""><a href="#secondary">Contact Us</a></li>
                    </ul>
                    <!-- /.navbar-nav -->
                </div>
                <!-- /.navbar-collapse -->
            </div>
            <!-- /.container -->
        </div>
        <!-- /.navbar -->



        <section id="hero" class="section section-inverse" data-speed="4" style="background-position: 50% -31.75px;">
            <div class="color-overlay dark">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12 text-center half-bottom">

                            <h1>What Is My Home Worth? Lets Find Out!</h1>

                            <h2 class="text-s">The real estate market is constantly changing. Find out the most current value of your home.<br><span class="text-underline">Do not miss</span> the opportunity to get your <span class="text-underline">FREE evaluation</span>.</h2>

                        </div>
                        <!-- /.col-md-12 -->

                        <div class="col-md-12">
                            <div class="result"></div>
                            <div class="register-form single-row">
                                <form id="register-form" method="post" action="save_homevalues" novalidate="novalidate" role="form" class="form-wizard-container">

                                    <div class="step step1">


                                        <!-- /#step1 -->
                                        <div class="row">
                                            <div class="form-group col-md-10 col-sm-6 has-bottom">

                                                <label for="reg_email" class="sr-only">Your Street/Address</label>


                                                <div id="locationField">
                                                    <span class="pac-icon pac-icon-marker marker"></span><input id="autocomplete" placeholder="Enter your address" type="text" class=" address form-control input-lg valid" autocomplete="off" aria-invalid="false">
                                                </div>
                                                <style>
                                                    input {
                                                        color: #000;
                                                    }
                                                </style>


                                                <script>
                                                    // This example displays an address form, using the autocomplete feature
                                                    // of the Google Places API to help users fill in the information.

                                                    var placeSearch, autocomplete;
                                                    var componentForm = {
                                                        street_number: 'short_name',
                                                        route: 'long_name',
                                                        locality: 'long_name',
                                                        administrative_area_level_1: 'short_name',
                                                        country: 'long_name',
                                                        postal_code: 'short_name'
                                                    };

                                                    function initAutocomplete() {
                                                        // Create the autocomplete object, restricting the search to geographical
                                                        // location types.
                                                        autocomplete = new google.maps.places.Autocomplete(
                                                            /** @type {!HTMLInputElement} */
                                                            (document.getElementById('autocomplete')), {
                                                                types: ['geocode'],
                                                                componentRestrictions: {administrative_area_level_1: "Ontario"},
                                                                componentRestrictions: {country: "ca"}
                                                            });

                                                        // When the user selects an address from the dropdown, populate the address
                                                        // fields in the form.
                                                        autocomplete.addListener('place_changed', fillInAddress);
                                                    }

                                                    // [START region_fillform]
                                                    function fillInAddress() {
                                                        // Get the place details from the autocomplete object.
                                                        var place = autocomplete.getPlace();

                                                        for (var component in componentForm) {
                                                            document.getElementById(component).value = '';
                                                            document.getElementById(component).disabled = false;
                                                        }

                                                        // Get each component of the address from the place details
                                                        // and fill the corresponding field on the form.
                                                        for (var i = 0; i < place.address_components.length; i++) {
                                                            var addressType = place.address_components[i].types[0];
                                                            if (componentForm[addressType]) {
                                                                var val = place.address_components[i][componentForm[addressType]];
                                                                document.getElementById(addressType).value = val;
                                                            }
                                                        }



                                                    }
                                                    // [END region_fillform]

                                                    // [START region_geolocation]
                                                    // Bias the autocomplete object to the user's geographical location,
                                                    // as supplied by the browser's 'navigator.geolocation' object.
                                                    function geolocate() {
                                                        if (navigator.geolocation) {
                                                            navigator.geolocation.getCurrentPosition(function(position) {
                                                                var geolocation = {
                                                                    lat: position.coords.latitude,
                                                                    lng: position.coords.longitude
                                                                };
                                                                var circle = new google.maps.Circle({
                                                                    center: geolocation,
                                                                    radius: position.coords.accuracy
                                                                });
                                                                autocomplete.setBounds(circle.getBounds());
                                                            });
                                                        }
                                                    }
                                                    // [END region_geolocation]
                                                </script>
                                                <script src="https://maps.googleapis.com/maps/api/js?libraries=places&amp;callback=initAutocomplete&key=AIzaSyAKZe--oNXUN1lK3Dtd1t72qNTFaheM_KU" async="" defer=""></script>


                                            </div>

                                            <div class="form-group col-md-2 col-sm-6 has-bottom">

                                              <!--  <a id="customNextButton" class="btn btn-main btn-lg btn-block2" style="display:none">Next <i class="fa fa-chevron-right"></i>
                                                </a>-->
                                                <button type="submit" name="submit" value="Get Started" id="reg_submit" class="btn btn-main btn-lg btn-block valid" style="" aria-invalid="false">NEXT</button>



                                            </div>


                                        </div>
                                        <!-- /.row -->

                                        <!-- /#step1 -->

                                    </div>
                                    <div class="step step2">

                                        <div class="row address-details">


                                            <div class="form-group">
                                                <div class="col-md-10 col-sm-10"><label>Street address</label></div>
                                                <div class="col-md-2 col-sm-2"><label>"Unit / Apt #"</label></div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-10 col-sm-10"> <input class="address form-control" id="route" disabled="true">
                                                </div>
                                                <div class="col-md-2 col-sm-2"><input class="address form-control" id="street_number" disabled="true">
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-4"><label>City</label><input class="address form-control" id="locality" disabled="true">
                                                </div>

                                                <div class="col-md-4"> <label>Province</label>
                                                    <input class="address form-control" id="administrative_area_level_1" disabled="true"></div>

                                                <div class="col-md-4"><label>Postal code</label>
                                                    <input class="address form-control" id="postal_code" disabled="true"></div>
                                            </div>
                                            <p></p>
                                            <div class="form-group">
                                                <div class="col-md-12 col-sm-12 has-bottom">


                                                    <button type="submit" name="submit" value="Get Started" id="reg_submit" class="btn btn-main btn-lg btn-block valid" style="" aria-invalid="false">NEXT</button>

                                                    <br>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="step step3">
                                        <div class="row">
                                            <h3 class="text-m bar-center text-center"><i class="fa fa-check fa-2x" style="color:green"></i> Property Data Found!</h3>
                                            <h5 class="text-center">Where can we send your home value information to?</h5>


                                            <input class="field" name="street_num" id="street_number" type="hidden">
                                            <input class="field" name="street_name" id="route" type="hidden">
                                            <input class="field" name="city" id="locality" type="hidden">
                                            <input class="field" name="region" id="administrative_area_level_1" type="hidden">
                                            <input class="field" name="postalcode" id="postal_code" type="hidden">
                                            <input class="field" name="country" id="country" type="hidden">

                                            <div class="form-group col-md-6 col-sm-6">

                                                <label for="firstname">First Name</label>
                                                <input type="text" id="firstname" name="firstname" class="form-control" placeholder="Your name">

                                            </div>

                                            <div class="form-group col-md-6 col-sm-6">

                                                <label for="firstname">Last Name</label>
                                                <input type="text" id="lastname" name="lastname" class="form-control" placeholder="Your name">

                                            </div>

                                            <div class="form-group col-md-6 col-sm-6">

                                                <label for="e">Email address</label>
                                                <input type="email" id="txtemail" name="e" class="form-control" placeholder="Your email">

                                            </div>



                                            <div class="form-group col-md-6 col-sm-6">

                                                <label for="reg_phone">Phone Number</label>

                                                <input type="text" id="telephone" name="telephone" class="form-control" placeholder="Your phone">

                                            </div>

                                        </div>
                                        <!-- /.row -->

                                        <div class="row">
                                            <div class="form-group col-md-6 col-sm-6">

                                                <label for="move">I'm looking to move in...</label>

                                                <select class="form-control" id="move" name="move"> <option value="">Choose One</option>


                                                <option value="0">Immediately</option>

                                                <option value="3">3 months</option>

                                                <option value="6">6 months</option>

                                                <option value="12">Just looking to stay informed.</option>

                                                <option value="none">None of Above</option>

                                            </select>

                                            </div>

                                        </div>
                                        <!-- /.row -->
                                        <div class="row">
                                            <div class="checkbox">

                                                <label>

                                                               <input type="checkbox" id="reg_check" name="reg_check"> Notify me when my neighbours are selling.
                                            </label>

                                            </div>

                                            <div class="form-group submit-row">
                                                <div class="col-md-12 col-sm-12 has-bottom">

                                                    <button type="submit" name="submit" value="Get Started" id="reg_submit" onclick="SaveData()" class="btn btn-main btn-lg btn-block2" style="display: block;">SIGN UP</button>
                                                    <br><br>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--        <button type="submit">Submit</button>-->
                                </form>
                            </div>

                        </div>
                    </div>
                    <!-- /#step2 -->
                    <!-- /#register-form -->
                </div>
                <!-- /.register-form -->
                <p class="help-block text-center">Your details will not be published or redistributing for any other purpose.</p>
                <p></p>
            </div>
            <!-- /.col-md-12 -->
        </section>
    </div>
    <!-- /.row -->
    <!-- /.container -->
    <!-- /.color-overlay -->
    <!-- /#hero -->
    <!--    <section id="features" class="section feature-list bordered">
        <div class="container">
            <h2 class="text-center bar-center">Benefits of our Home Value Calculator</h2>
            <h3 class="text-s text-center">Be in the know. Keep informed about the activity in your neighbourhood.</h3>
            <div class="feature-container">
                <div class="row">
                    <div class="col-md-4 col-sm-4 ">
                        <i class="icon icon-login"></i>
                        <div class="feature-details">
                            <h3>FREE To Sign Up</h3>
                            <p>Our evaluation service is absolute FREE.</p>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-4">
                        <i class="icon icon-paper-plane"></i>
                        <div class="feature-details">
                            <h3>Instant Notificiation</h3>
                            <p>Get instant notification changes to your homes or neighbourhood.</p>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-4">
                        <i class="icon icon-book-open"></i>
                        <div class="feature-details">
                            <h3>Full Detailed Report</h3>
                            <p>Get full details about your home value.</p>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>-->


    <!--

    <section id="secondary" class="section bordered">
        <div class="container">
            <div class="row">
                <div class="col-md-3">
                    <h3>About Us</h3>
                    <p>In 1977, Stephen, along with his mother Sadie, founded Sadie Moranis Realty. In 1994, the company became affiliated with Prudential Real Estate Affiliates for 18 years and then recently in 2012 joined forces with Sutton Realty Group
                        Services to become Sutton-Sadie Moranis Realty, Brokerage which Stephen independently owns and operates.</p>
                </div>
                <div class="col-md-3">
                    <h3>Contact Us</h3>
                    <p><img src="http://img.briteagent.com.s3.amazonaws.com/websites/387/1451498603.png"></p>
                    <p>Forest Hill Real Estate Signature<br> 1440 Don Mills Road, Suite 101 </p>
                    Direct: (416) 449-2020<br> Phone: (416) 929-4343<br> email: <a href="mailto:info@foresthillsignature.com">info@foresthillsignature.com</a>
                    <p></p>
                </div>
                <div class="col-md-3">
                    <h3>You may also like</h3>
                    <ul class="link-list-y">
                        <li><a href="/search" title="NewsPlus Theme" target="_blank">MLS Search</a></li>
                        <li><a href="/search" title="Xing Theme" target="_blank">New Listings</a></li>
                    </ul>
                </div>
                <div class="col-md-3">
                    <h3>Follow Us</h3>
                    <ul class="ss-social text-xs">

                        <li><a href="https://www.facebook.com/Forest-Hill-Signature-657144744422863" title="facebook" class="facebook"><i class="fa fa-facebook"></i><span class="sr-only">facebook</span></a></li>
                        <li><a href="https://twitter.com/fhillsignature" title="twitter" class="twitter"><i class="fa fa-twitter"></i><span class="sr-only">twitter</span></a></li>
                        <li><a href="https://ca.linkedin.com/in/foresthillsignature" title="linkedin" class="linkedin"><i class="fa fa-linkedin"></i><span class="sr-only">linkedin</span></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </section>-->

    <footer id="footer" class="section footer">
        <div class="container">
            <div class="row">
                <div class="col-sm-6 col-md-6 notes-left">
                    © 2015 Brite Agent. All rights reserved.
                </div>
                <!-- /.notes-left -->
                <div class="col-sm-6 col-md-6 notes-right text-right">

                </div>
                <!-- /.notes-right -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container -->
    </footer>
    <!-- /#footer -->
    <!-- #page -->

    <div class="scroll-to-top" style="display: block;">
        <a href="#page" title="Scroll to top"><i class="fa fa-caret-up"></i><span class="sr-only">Top</span></a>
    </div>
    <!-- /.scroll-to-top -->

    <!-- JavaScript files and plugins -->
    <script src="http://foresthillsignaturecom.briteagent.net/themes/landingpage/4/js/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.11.4/i18n/jquery-ui-i18n.min.js"></script>
    <script src="http://foresthillsignaturecom.briteagent.net/themes/landingpage/4/js/bootstrap.min.js"></script>
    <script src="http://foresthillsignaturecom.briteagent.net/themes/landingpage/4/js/owl.carousel.min.js"></script>
    <script src="http://foresthillsignaturecom.briteagent.net/themes/landingpage/4/js/jquery.validate.min.js"></script>
    <script src="http://foresthillsignaturecom.briteagent.net/themes/landingpage/4/js/viewportchecker.js"></script>
    <script src="http://foresthillsignaturecom.briteagent.net/themes/landingpage/4/js/retina.min.js"></script>
    <script src="http://foresthillsignaturecom.briteagent.net/themes/landingpage/4/js/custom.js"></script>
    <script src="http://foresthillsignaturecom.briteagent.net/themes/landingpage/4/js/jquery.quickWizard.js"></script>
    <script src="../Scripts/BootStrapDailog/bootstrap-dialog.min.js"></script>
    <script>
        $(document).ready(function() {

        



            $('form').quickWizard({
                'breadCrumb': false,
                'element': '.step',
                nextButton: '<a id="customNextButton" onclick="SaveData()" class="btn btn-main btn-lg btn-block2">Next <i class="fa fa-chevron-right"></i></a>',
                //prevButton: ''
                prevButton: ' <a id="customPrevButton" class="btn btn-main btn-lg btn-block2"><i class="fa fa-chevron-left"></i> Previous</a>'
            });


            function evaluate() {
                var item = $(this);
                var relatedItem = $('.street');

                if (item.is(":checked")) {
                    relatedItem.fadeIn();
                } else {
                    relatedItem.fadeOut();
                }
            }

        

            $('input[type="checkbox"]').click(evaluate).each(evaluate);


            

            GetData();
        });
         $('#customNextButton').click(function(){
   
        });


        
        function SaveData()
        {
        debugger;
        var HomeValue={
        Address:$("#autocomplete").val(),
        StreetAddress:$("#route").val(),
        Unit:$("#street_number").val(),
        City:$("#locality").val(),
        Province:$("#administrative_area_level_1").val(),
         PostalCode:$("#postal_code").val(),
        FirstName:$("#firstname").val(),
        LastName:$("#lastname").val(),
        EmailAddress:$("#txtemail").val(),
        PhoneNumber:$("#telephone").val(),
        TimePeriodId:$("#move").val(),
        PostId:$("#PostId").val(),
        Notify:$("#reg_check").is(":checked")


        
        
        }

        var $_BaseUrl = window.location.origin;

          $.ajax({
            cache: false,
            async: true,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: $_BaseUrl + '/Users/api/HomeApi/SaveHomeValue',
            data:JSON.stringify(HomeValue),
            success: function (data) {
               
        BootstrapDialog.alert("Saved successfully");
            },
            error: function (request, error) {
                if (request.statusText == "CustomMessage") {
                    $("#spanError").html(request.responseText)
                }
            },
            headers: {
                'RequestVerificationToken': '@TokenHeaderValue()'
            }
        });



        }

        function GetData()
        {
           var $_BaseUrl = window.location.origin;

          $.ajax({
            cache: false,
            async: false,
            type: "Get",
            contentType: "application/json; charset=utf-8",
            url: $_BaseUrl + '/Users/api/HomeApi/GetHomeValue',
            data:{postId:$("#PostId").val()},
            success: function (data) {
               debugger;
        $('input').attr('disabled',false)
        $("#autocomplete").val(data.Address);
        $("#route").val(data.StreetAddress);
        $("#street_number").val(data.Unit);
        $("#locality").val(data.City);
        $("#administrative_area_level_1").val(data.Province);
        $("#postal_code").val(data.PostalCode);
        $("#firstname").val(data.FirstName);
        $("#lastname").val(data.LastName);
        $("#txtemail").val(data.EmailAddress);
        $("#telephone").val(data.PhoneNumber);
       $("#move").val(data.TimePeriodId);
        $("#PostId").val(data.PostId);
        $("#reg_check").prop('checked',data.Notify);

        //$('#move option[value="'+data.TimePeriod+'"]').attr('selected','selected');
            },
            error: function (request, error) {
                if (request.statusText == "CustomMessage") {
                    $("#spanError").html(request.responseText)
                }
            },
            headers: {
                'RequestVerificationToken': '@TokenHeaderValue()'
            }
        });
        }

    </script>






</body>

</html>