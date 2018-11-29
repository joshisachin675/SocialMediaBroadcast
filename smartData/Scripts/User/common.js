
setTimeout(function () { if ($.connection && $.connection.hub) $.connection.hub.start(); }, 20000);
window.onload = function () {
    if ($("#HiddenPageCount").val() == 0 || $("#HiddenPageCount").val() < 10) {
        $("#Pagination").hide();
    }
    if ($("#HiddenTotalPage").val() == 0 || $("#HiddenPageCount").val() < 10) {
        $("#Pagination").hide();
    }
}
$(document).ready(function () {
   
    $("[rel=icon]").attr("href", $("#hiddenapplicationlogo").val());

    //$("#DateSelectorFrom").attr(readonly);
    //$("#DateSelectorTo").attr(readonly);
 
        // Run the script on DOM ready:
        $(function () {
            //$('input').customInput();

            $(".closeX").click(function () {
                $(".modal-backdrop").hide();
            });
            //$("#add_number_menu").click(function () {
            //    $(".modal-backdrop").show();
            //});

        });




    //Date: 10/06/2014
    //Summanry: This code prevent &# in text boxes
    $(":input[type='text']").on("blur", function () {

        if ((this.value.indexOf('<', 0) != -1) || (this.value.indexOf('&#', 0) != -1) /*|| (this.value.indexOf("'", 0) != -1)*/) {
            this.value = "";
        }

    });


    //Date: 10/06/2014
    //Summanry: This code prevent &# in text boxes
    $(":input[type='text']").on("keyup", function () {
        if ((this.value.indexOf('<', 0) != -1) || (this.value.indexOf('&#', 0) != -1)/* || (this.value.indexOf("'", 0) != -1)*/) {
            this.value = (this.value).substring(0, (this.value).length - 1);
            return false;
        }

        return true;
    });

    //Date: 10/06/2014
    //Summanry: This is the jquery code that would be used to set all the textboxes with class NumericsOnly as to accept Numerics only
    $(".NumericOnly").on("keypress", function (e) {
        var regex = /^[0-9]+$/;
        var key;
        key = (e.key == undefined) ? String.fromCharCode(e.which) : e.key;
        var regExvalid = false;
        if (!regex.test(key)) {
            if (key == "Right" || key == "Left" || key == "Backspace" || key == "Del" || key == "Tab") {
                regExvalid = true;
            }
        }
        else {
            regExvalid = true;
        }

        return regExvalid;
        //var key;
        //key = e.which ? e.which : e.keyCode;

        //if (key == 8 || (key >= 37 && key <= 40) || key == 46 || (key >= 48 && key <= 57) || key == 9 /*|| key == 35 || key == 36 ||key == 116*/) {
        //    return true;
        //}
        //else {
        //    return false;
        //}
    });
    $(".NumericOnlyWithDecimal").on("keypress", function (e) {
        var regex = /^[0-9]?[.]?$/;
        var key;
        key = e.key;
        var regExvalid = false;
        if (!regex.test(key)) {
            if (key == "Right" || key == "Left" || key == "Backspace" || key == "Del" || key == "Tab") {
                regExvalid = true;
            }
        }
        else {
            regExvalid = true;
        }
        if ($(this).val().indexOf(".", 0) != -1 && key == ".")
            regExvalid = false;
        return regExvalid;
        //var key;
        //key = e.which ? e.which : e.keyCode;

        //if (key == 8 || (key >= 37 && key <= 40) || key == 46 || (key >= 48 && key <= 57) || key == 9 /*|| key == 35 || key == 36 ||key == 116*/) {
        //    return true;
        //}
        //else {
        //    return false;
        //}
    });

    //Date: 10/06/2014
    //Summanry: This is the jquery code to disable paste functionality
    $(".NumericOnly").on('paste', function (e) {
        var el = $(this);
        var text;
        PrevText = $(this).val();

        setTimeout(function () {
            text = $(el).val();
            if (IsNumeric(text))
                $(el).val(text);
            else {
                $(el).val(PrevText);
                alert("Numeric Only");
            }
        }, 100);
    });


    //Date: 10/06/2014
    //Summanry: Validation to accept only alphabets
    $(".AlphabeticOnly").on("keypress", function (e) {
        var key;
        key = e.which ? e.which : e.keyCode;
        if ((key >= 65 && key <= 91) || (key >= 97 && key < 123) || key == 8 || key == 9 || (key >= 37 && key <= 40) || key == 46 || /*key == 35 || key == 36 ||*/key == 116) {
            return true;
        }
        else {
            return false;
        }
    });


    //Date: 10/06/2014
    //Summanry: Validation to accept only alphabets with space
    $(".AlphabaticWithSpace").on("keypress", function (e) {
        var key;
        debugger
        key = e.which ? e.which : e.keyCode;
        if ((key >= 65 && key <= 91) || (key >= 97 && key < 123) || key == 8 || key == 9 || key == 32 || (key >= 37 && key <= 40) || key == 46 || key == 45 || /*key == 35 || key == 36 ||*/key == 116) {
            return true;
        }
        else {
            return false;
        }
    });


    //Date: 10/06/2014
    //Summanry: Validation to accept only alpha-numberics
    $(".AlphaNumericOnly").keypress(function (e) {
        var key;
        key = e.which ? e.which : e.keyCode;
        if ((key >= 65 && key <= 90) || (key >= 97 && key <= 122) || key == 8 || key == 9 || key == 32 || (key >= 37 && key <= 40) || key == 46 || key == 35 || key == 36 || key == 116 || (key >= 48 && key <= 57)) {
            return true;
        }
        else {
            return false;
        }
    });


    //Date: 10/06/2014
    //Summanry: Validation to accept only alpha-numberics with quotation'
    $(".AlphaNumericWithQuestionQuotation").keypress(function (e) {
        var key;
        key = e.which ? e.which : e.keyCode;
        if ((key >= 65 && key <= 90) || (key >= 97 && key <= 122) || key == 8 || key == 9 || key == 33 || key == 63 || key == 32 || (key >= 38 && key <= 39) || key == 46 || key == 35 || key == 36 || key == 116 || (key >= 48 && key <= 57)) {
            return true;
        }
        else {
            return false;
        }
    });

});

//Date: 10/06/2014
//Summanry: Common Validation function to check required fields
function CheckRequired(fieldid, message) {
    var formvalidate = true;
    var fieldidval = $(fieldid).val();
    if (fieldidval != undefined)
    {
        fieldidval = fieldidval.trim();
    }
    if (fieldidval == undefined)
    {
        return true;
    }
    if (fieldidval == "" || fieldidval == null) {
        $(fieldid).removeClass("successClass");
        $(fieldid).addClass("errorClass");
        $(fieldid).attr("title", message);
        formvalidate = false;
    }
    else {
        $(fieldid).removeClass("errorClass");
        $(fieldid).addClass("successClass");
        $(fieldid).removeAttr("title");
    }
    return formvalidate;
}


//Date: 10/06/2014
//Summanry: Common Validation function to check Phone number
function ValidatePhone(fieldid, message) {
    var formvalidate = true;

    //  var expr = /^[01]?[- ]?[2-9]\d{2}[- ]?\d{3}[- ]?\d{4}$/;
    var expr = /^\d{4}[- ]?\d{3}[- ]?\d{3}$/;
    var fieldidval = $(fieldid).val();
    if (fieldidval == "" || fieldidval == null) {
        $(fieldid).removeClass("errorClass");
        $(fieldid).removeClass("successClass");
    }
    else {
        if (expr.test(fieldidval)) {
            $(fieldid).removeClass("errorClass");
            $(fieldid).addClass("successClass");
        }
        else {
            $(fieldid).removeClass("successClass");
            $(fieldid).addClass("errorClass");
            $(fieldid).attr("title", message);
            formvalidate = false;
        }
    }
    return formvalidate;
};


function ValidateDestinationPhone(fieldid, message) {

    var formvalidate = true;
    var expr = /^(\+\d{1,2}\s)?\(?\d{3}\)?[ .-]?\d{3}[ .-]\d{4}$/;
    var expr2 = /^\d{10}$/;
    //var exp3 = /^\d{3}\.\d{3}\.\d{4}$/;
    var fieldidval = $(fieldid).val();
    if (fieldidval == "" || fieldidval == null) {
        $(fieldid).removeClass("errorClass");
        $(fieldid).removeClass("successClass");
    }
    else {
        if (expr.test(fieldidval) || expr2.test(fieldidval)) { //|| exp3.test(fieldidval)
            $(fieldid).removeClass("errorClass");
            $(fieldid).addClass("successClass");
        }
        else {
            $(fieldid).removeClass("successClass");
            $(fieldid).addClass("errorClass");
            $(fieldid).attr("title", message);
            formvalidate = false;
        }
    
    }
    return formvalidate;
};



//Date: 10/06/2014
//Summanry: Common Validation function to check email
function ValidateEmail(fieldid, message) {
    var formvalidate = true;
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

    var fieldidval = $(fieldid).val();

    if (fieldidval == "" || fieldidval == null) {
        $(fieldid).removeClass("errorClass");
        $(fieldid).removeClass("successClass");
    }
    else {
        if (expr.test(fieldidval)) {
            $(fieldid).removeClass("errorClass");
            $(fieldid).addClass("successClass");
        }
        else {
            $(fieldid).removeClass("successClass");
            $(fieldid).addClass("errorClass");
            $(fieldid).attr("title", message);
            formvalidate = false;
        }
    }
    return formvalidate;
};


//Summanry: Common Validation function to validate web address
function ValidateWebAddress(fieldid, message) {

    var formvalidate = true;
    var ValidationExpression = "^(http(?:s)?\:\/\/[a-zA-Z0-9\-]+(?:\.[a-zA-Z0-9\-]+)*\.[a-zA-Z]{2,6}(?:\/?|(?:\/[\w\-]+)*)(?:\/?|\/\w+\.[a-zA-Z]{2,4}(?:\?[\w]+\=[\w\-]+)?)?(?:\&[\w]+\=[\w\-]+)*)$";

    var fieldidval = $(fieldid).val();
    if (fieldidval == "" || fieldidval == null) {

        $(fieldid).addClass("errorClass");
        $(fieldid).removeClass("successClass");
    }
    else {

        if (ValidationExpression.test(fieldidval)) {
            $(fieldid).removeClass("errorClass");
            $(fieldid).addClass("successClass");
        }
        else {

            $(fieldid).addClass("errorClass");
            $(fieldid).attr("title", message);
            formvalidate = false;
        }
    }
    return formvalidate;
};


//Date: 10/06/2014
//Summanry: Common Validation function for required drop down
function RequiredDropDown(fieldid, selectedText, message) {

    var sText = $('#' + fieldid + ' option:selected').text();
    if (sText == selectedText) {
        $(fieldid).addClass("errorClass");
        $(fieldid).removeClass("successClass");
        $(fieldid).attr("title", message);
        formvalidate = false;
    }
    else {
        $(fieldid).removeClass("errorClass");
        $(fieldid).addClass("successClass");
    }
    return formvalidate;
}


//Date: 10/06/2014
//Summanry: Common validation function for dropdown by SelectedIndex 
function CheckDropDown(fieldid, message) {

    formvalidate = true;
    if (($(fieldid).val() == 'NaN') || (parseInt($(fieldid).val()) == 0)) {
        $(fieldid).addClass("errorClass");
        $(fieldid).removeClass("successClass");
        $(fieldid).attr("title", message);
        formvalidate = false;
    }
    else {
        $(fieldid).removeClass("errorClass");
        $(fieldid).addClass("successClass");
    }
    return formvalidate;
}


//Date: 10/06/2014
//Summanry: Common validation function for repassword
function ValidateRepassword(fieldid, fieldid2, message) {
    var formvalidate = true;

    //some other commom functions to be used
    var fieldidval1 = $(fieldid).val();
    var fieldidval2 = $(fieldid2).val();
    if (fieldidval1 == fieldidval2) {
        $(fieldid).removeClass("errorClass");
        $(fieldid).addClass("successClass");
    }
    else {
        $(fieldid).removeClass("successClass");
        $(fieldid2).removeClass("successClass");

        $(fieldid2).addClass("errorClass");
        $(fieldid).addClass("errorClass");
        $(fieldid).attr("title", message);
        formvalidate = false;
    }
    return formvalidate;
};


//Summanry: Common validation function to check password length in between 6-12
function checkPasswordLength(ctrl, message) {
    var formvalidate = true;
    if ($(ctrl).val().length >= 6 && $(ctrl).val().length <= 12) {
        $(ctrl).removeClass("errorClass");
        $(ctrl).addClass("successClass");

    }
    else {
        $(ctrl).removeClass("successClass");
        $(ctrl).addClass("errorClass");
        $(ctrl).attr("title", message);
        formvalidate = false;
    }
    return formvalidate;
}

function ShowStaus(Message, Type) {
    $('.' + Type).html("<i class='msg-icon'></i>" + Message);
    window.location.hash = '#DivTopcontainer';
    $('.' + Type).fadeIn(500);
    $('.' + Type).fadeOut(7000);
}

//
// Sep 30, 2016
// Remove validation error on input
function removeValidationError(input) {
    if (input) {
        $(input).removeAttr("title").removeClass("errorClass");
    }
}