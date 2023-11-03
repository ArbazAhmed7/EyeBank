
// *************************  Raza added Auto Login After Some Time ***************************

//$(function () {
//    $("body").on('click keypress', function () {
//        ResetThisSession();
//    });
//});
//var timeInSecondsAfterSessionOut = 1800; // change this to change session time out (in seconds).
//var secondTick = 0;

//function ResetThisSession() {
//    secondTick = 0;
//}

//function StartThisSessionTimer() {
//    secondTick++;
//    var timeLeft = ((timeInSecondsAfterSessionOut - secondTick) / 60).toFixed(0); // in minutes
//    timeLeft = timeInSecondsAfterSessionOut - secondTick; // override, we have 30 secs only

//    $("#spanTimeLeft").html(timeLeft);

//    if (secondTick > timeInSecondsAfterSessionOut) {
//        clearTimeout(tick);
//        window.location = "/Admin/SignOut";
//        return;
//    }
//    tick = setTimeout("StartThisSessionTimer()", 1000);
//}

//StartThisSessionTimer();

// **********************************************************************************************

//Pass table name without # it set table index or Sr No 
function _reset_table_row_Srno(tbl_name) {
    let i = 1;
    $('#' + tbl_name + ' > tr').each(function (index) {
        var $td = $(this).find('td');
        $td.eq(0).html(i);
        ++i;
    });
}
//}
//var sideBarWidth = document.getElementById("sidebar").offsetWidth;
//var navBodyWidth = document.getElementById("navBody").offsetWidth;
//var getTotalLength;
//var marginLeft;
//$('#hidesidebar').click(function () {
//    /*alert(1); */
//    /*$('#navCol').hide();*/
//    //sideBarWidth 
//    //navBodyWidth 
//    marginLeft = -sideBarWidth;
//    getTotalLength = sideBarWidth + navBodyWidth;
//    /*alert(getTotalLength);*/
//    document.getElementById("sidebar").style.width = "0";
//    document.getElementById("navBody").style.marginLeft = marginLeft.toString() + "px";
//    document.getElementById("navBody").style.width = getTotalLength.toString() + "px"
//    $('#hidesidebar').hide();
//    $('#Showsidebar').show();
//})
//$('#Showsidebar').click(function () {
//    document.getElementById("sidebar").style.width = sideBarWidth.toString() + "px";
//    document.getElementById("navBody").offsetWidth = navBodyWidth.toString() + "px";
//    console.log(getTotalLength - sideBarWidth);
//    document.getElementById("navBody").style.width = (getTotalLength - sideBarWidth - 1).toString() + "px";
//    document.getElementById("navBody").style.marginLeft = "0px";
//    $('#hidesidebar').show();
//    $('#Showsidebar').hide();
//})
//Clear all forms hiddens , text , number , check box.
function _clear_textBox_Numbers()  {
    $("input[type=hidden]").val("");
    $("input[type=text]").val("");
    $("input[type=number]").val(0);
    $("input[type=checkbox]").prop("checked", true);      
}

//#region fn_DateFormate
function _convertDate(val) {
    var date = new Date(val);
    var day = ("0" + date.getDate()).slice(-2);
    var month = ("0" + (date.getMonth() + 1)).slice(-2);
    return newDate = date.getFullYear() + "-" + (month) + "-" + (day);
}
//#endregion

//#region fn_GetPath
function _getPath(date, path) {
    try {
        if (date === "")
            date = new Date();

        var year = date.substring(0, 4);
        var month = ('0' + date.substring(5, 7)).slice(-2);
        var day = date.substring(8, 10);
        var dateFormat = day + '-' + month + '-' + year;
        filesFolderURL = year + '\\' + month + '\\' + dateFormat + path;

    } catch (e) {

    }

    var date = new Date(val);
    var day = ("0" + date.getDate()).slice(-2);
    var month = ("0" + (date.getMonth() + 1)).slice(-2);
    return newDate = date.getFullYear() + "-" + (month) + "-" + (day);
}
//#endregion

//#region validateMobileNos
var _validateMobileNos = (e) => {
    var _mobileNo = e;
    if (!_mobileNo.length === 11) {
        return;
    }
    else {
        var newVal = _mobileNo.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '-').replace(/[a-zA-Z]/, '');
        var isNumber = Number.isInteger(parseInt(newVal));
        if (isNumber) {
            var spanId = $("#" + e + "").parent('div').find('span').attr('id');
            if (newVal.length === 11) {
                var seprator = "-";
                var first = newVal.slice(0, 4);
                var second = newVal.slice(4, 11);
                var mobileNo = first + seprator + second;
                $("#" + e + "").val(mobileNo);
                $("#" + spanId + "").css('display', 'none');
                return true;
            }
            else {
                $("#" + spanId + "").css('display', 'block');
                return false;
            }
        }
    }
    
    
    
}
//#endregion
 
//function AllowOnlyFour(Id) {
function AllowOnlyFour1(Id) {
    $('#' + Id + '').on('keyup', function () {
        var inputValue = $(this).val();
        console.log(inputValue);
        if (inputValue.length > 4 && inputValue!='') {
            $(this).val(inputValue.slice(0, 5));
        }
    });
}

function AllowOnlyFour2(Id) {
    $('#' + Id + '').on('keypress', function (event) {
        var allowedCharacters = /[0-9\.]/;
        var inputValue = String.fromCharCode(event.which);
        // Check if the entered character is a valid number or decimal point
        if (!allowedCharacters.test(inputValue)) {
            event.preventDefault(); // Prevents the character from being entered
        }

        // Check if there are already two numbers before the decimal point
        var value = $(this).val();
        var decimalIndex = value.indexOf('.');
        if (decimalIndex !== -1 && decimalIndex < value.length - 3) {
            event.preventDefault();
        }
    });
}
function AllowOnlyFour(Id) {
    $('#' + Id + '').on('input', function () {
        var maxLength = 4; // Maximum number of digits allowed
        if ($(this).val().toString().includes('.'))
            maxLength = 5;

        if ($(this).val().length > maxLength) { 
            $(this).val($(this).val().slice(0, maxLength)); // Truncate the input
        }
    });
}
function GetDecimalInput(Id) {
    $('#' + Id + '').on("blur", function () {
        var textboxValue = $(this).val();
        
        var firstNumberIndex = textboxValue.search(/\d/); // Find the index of the first number
        var dotIndex = textboxValue.indexOf('.'); // Find the index of the dot
        //console.log("Dot Index ", dotIndex, " firstNumber Index:  ", firstNumberIndex, " length", textboxValue.length);
        if (textboxValue.length > 1) {
            if (firstNumberIndex >= 0 && dotIndex === -1 && firstNumberIndex < textboxValue.length) {
                var updatedValue = textboxValue.slice(0, firstNumberIndex + 2) + '.' + textboxValue.slice(firstNumberIndex + 2);
                $(this).val(updatedValue);
            }
        }
        else if (textboxValue.length == 1) {
            var updatedValue = textboxValue.slice(0, firstNumberIndex + 1) + '.00';
            $(this).val("0"+updatedValue);
        }
    });

} function AllowNumberOnly(Id) {
    $('#' + Id + '').on('keypress', function (e) {
        var key = String.fromCharCode(e.which);

        // Regular expression to allow only numeric digits
        var regex = /^[0-9.]+$/;

        // Check if the input character matches the regex pattern
        if (!regex.test(key)) {
            e.preventDefault(); // Prevent the input
        }
    });
}
 
function GetDate(Id) {
    //var now = new Date();

    //var options = { day: '2-digit', month: 'short', year: 'numeric' };

    CustomDate(Id);
    //var formattedDate = now.toLocaleDateString('en-GB', options);

   

    //setTimeout(function () {
    //    $('#' + Id + '').datepicker('hide'); $('#' + Id + '').val(formattedDate); DateChange(Id);
    //},
    //    50)    
    var currentDate = new Date();

    // Define the months array for month name conversion
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

    // Format the date as "dd-MMM-yyyy"
    var formattedDate = currentDate.getDate() + '-' + months[currentDate.getMonth()] + '-' + currentDate.getFullYear();
    console.log("data", formattedDate);
    // Set the content of the element with the formatted date
       setTimeout(function () {
           $('#' + Id + '').datepicker('hide'); $('#' + Id + '').val(formattedDate); //DateChange(Id);
               },
        100) 
    
}

function SetDate(Id, value) {

    var options = { day: '2-digit', month: 'short', year: 'numeric' };
    value = new Date(value);
    // Format the date using the options
    CustomDate(Id);
    var formattedDate = value.toLocaleDateString('en-GB', options);

    //console.log(options);

    setTimeout(function () {
        $('#' + Id + '').datepicker('hide'); $('#' + Id + '').val(formattedDate);
    },
        50)

}


function DateChange(Id) {
    //console.log($('#txtEnrollementDate').val().slice(0, 3) + $('#txtEnrollementDate').val().slice($('#txtEnrollementDate').val().length - 8, $('#txtEnrollementDate').val().length));
    setTimeout(function () {
        $('#' + Id + '').val($('#' + Id + '').val().slice(0, 3) + $('#' + Id + '').val().slice($('#' + Id + '').val().length - 8, $('#' + Id + '').val().length));
    }, 500)
}
function CustomDate(Id) {
    $('#' + Id + '').datepicker({
        autoclose: true,
        todayHighlight: true
    });
}
//function _removeSpacialcharacter(obj) {
//    var format = /[`!@#%^&*()_+\-=\[\]{};':"\\|<>\/?~]/;
//    var val = $(obj).val().replace(format, "");
//    if ($(obj).val().match(format)) {
//        $(obj).css({ 'border-color': 'red' });
//        $(obj).attr('title', 'Spacial character are not allowed');
//    } else {
//        $(obj).css({ 'border-color': '' });
//        $(obj).attr('title', '');
//        return $(obj).val(val);
//    }
//}