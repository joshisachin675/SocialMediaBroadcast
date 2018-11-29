

<!DOCTYPE html>
<html class="toolbar-view" xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="favicon.ico">
    <!--<link rel="stylesheet" type="text/css" href="css/style.css" />-->
    <title></title>
    <!--<link href="https://fonts.googleapis.com/css?family=Montserrat:300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">-->
    

    <script src="/Scripts/Jscripts/jquery-2.2.3.js"></script>
    <link href="/Content/css/intro.css" rel="stylesheet" />



</head>
<body class="view" scroll="no">
    <div class="overlay-full overlay-1">
        <div class="wrapper-overlow">
            <div class="block" style="background-color:#285e8e" id="block-modal">
                <p class="title-block">Want To Know How Much Your Home Is Worth?</p>
                <p class="text-block" style="font-size:20px;">Your home might be worth more than you think!</p>
                <div style="width:100%; margin-top:50px;">
                    <p style="width:45%; float:left;"><a class="blue-button" style="background-color:#9f4b4b;" href="#">YES, PLEASE</a></p>
                    <p class="block-new" style="width:45%; float:right; margin-top: 10px;"><a href="" class="link-button">Not Right Now</a></p>
                </div>
            </div>
            <div id="count-block" class="modal-count" style="background-color:#b2c6d6;">
                <div class="top-block">
                    <div class="left-image">
                        <img src="images/bottom-image.png" alt="How to Find a Starter Home in a Hot Housing Market" title="" style="height: 60px; width: 60px; border-radius:30px;" />
                    </div>
                    <div class="title-right">
                        <p class="title-home">How to Find a Starter Home in a Hot Housing Market</p>
                        <p class="location-home" style="color:#fff;">finance.yahoo.com</p>
                    </div>
                </div>
                <div class="line-gray"></div>
                <div class="bottom-block">
                    <p>
                        <span class="text" style="color:#fff;">CONTINUE TO YOUR ARTICLE IN...</span>
                        <span id="count">5</span>
                    </p>
                </div>
            </div>

        </div>
    </div>
    <iframe id="iframe" sandbox="allow-forms allow-popups allow-pointer-lock allow-same-origin allow-scripts"></iframe>

    

    <input type="hidden" id="PostId" value="<%=Request.querystring("data")%>"  />

    <script type="text/javascript">

        Counter();

        function setCookie(name, value, expires, path, domain, secure) {
            var thisCookie = name + "=" + escape(value) +
            ((expires) ? "; expires=" + expires.toGMTString() : "") +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            ((secure) ? "; secure" : "");
            document.cookie = thisCookie;
        }

        function getCookieVal(offset) {
            var endstr = document.cookie.indexOf(";", offset);
            if (endstr == -1)
                endstr = document.cookie.length;
            return unescape(document.cookie.substring(offset, endstr));
        }

        function GetCookie(name) {
            var arg = name + "=";
            var alen = arg.length;
            var clen = document.cookie.length;
            var i = 0;
            while (i < clen) {
                var j = i + alen;
                if (document.cookie.substring(i, j) == arg)
                    return getCookieVal(j);
                i = document.cookie.indexOf(" ", i) + 1;
                if (i == 0) break;
            }
            return null;
        }

        function Counter() {
            var visits = GetCookie("counter");
            $('body').addClass('resize');
            $('.overlay-full').css('display', 'block');
            heightScreen();
            $('#block-modal').animate({ top: "0" }, 3000);
            $('body').addClass('overlay');
            setCookie("counter", visits);
        }

        function heightScreen() {
            heightScreen = $(window).height();
            $('body').css('top', heightScreen);
        }

        function countNumber() {
            countValue--;
            if (countValue > 0) {
                document.getElementById('count').innerHTML = countValue;
                setTimeout("countNumber()", 1000);
            } else {
                animateTop();
            }
        }
        function animateFromBottom() {
            if ((window.innerWidth < 479) || (window.innerHeight <= 330)) {
                var bottomAnimate = 5;
            } else {
                var bottomAnimate = 42;
            }
            $("#count-block").animate({ bottom: bottomAnimate + "px" }, 800, function () {
                document.getElementById('count').innerHTML = countValue;
                countNumber();
            });
        }
        function heightScreen() {
            heightScreen = $(window).height();
            $('body').css('top', heightScreen);
        }
        function animateTop() {
            var iframed = !!$('iframe').length;
            if (!iframed) {

            } else {
                $("body").animate({ top: 0 }, 800);
                $('.overlay-full').animate({ top: "-100%" }, 800);
            }
        }
        function Decrypt(str) {
            debugger
            if (!str) str = "";
            str = (str == "undefined" || str == "null") ? "" : str;
            try {
                var key = 146;
                var pos = 0;
                ostr = '';
                while (pos < str.length) {
                    ostr = ostr + String.fromCharCode(key ^ str.charCodeAt(pos));
                    pos += 1;
                }
                $("#PostId").val(ostr);
                return ostr;
            } catch (ex) {
                return '';
            }
        }
        function GetData() {
            var $_BaseUrl = window.location.origin;

            $.ajax({
               
                type: "POST",            
                url: $_BaseUrl + '/post/GetResultsForAspPAges/',
                data: { postId:$("#PostId").val() },
                success: function (data) {
                    debugger;

                    //$('#iframe').attr('src', data.Url)
                    $('#iframe').attr('src', data.Url);
                   
                 
                
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

        window.onload = function () {
           
            debugger;
            Decrypt($("#PostId").val())
            GetData();
            countValue = $('#count').html();
            if ($('#count').length > 0) {
                document.getElementById('count').innerHTML = countValue;
                countNumber();
            }


            //var timer = setInterval(function () {
            //    window.location.href = "https://finance.yahoo.com/news/starter-home-hot-housing-market-103040164.html";
            //}, 5000);

        }
        $(window).resize(function () {
            if ($('.resize').length > 0) {
                if ($('.overlay').length > 0) {
                    heightScreen = $(window).height();
                    $('body').css('top', heightScreen);
                } else {
                    $('body').css('top', "");
                }
            }
        });
        $(document).on("click touch", ".link-button, #count-block", function (e) {

            e.preventDefault();
            var iframed = !!$('iframe').length;
            if (!iframed) {

            } else {
                $("body").animate({ top: 0 }, 500);
                $('.overlay-full').animate({ top: "-100%" }, 500);
                $('body').removeClass('overlay');
            }
        });
    </script>
</body>
</html>
