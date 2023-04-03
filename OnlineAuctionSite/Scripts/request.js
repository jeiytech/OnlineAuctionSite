//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    $.ajax({
        url: "/Admin/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td' + item.id + '</td>';
                html += '<td>' + item.userid + '</td>';
                html += '<td>' + item.lastdate + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function   
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        Email: $('#Email').val(),
        City: $('#City').val(),
        MobileNo: $('#MobileNo').val(),
        Education: $('#Education').val(),
        //Photo: $('#Photo').val(),
        Hobby: $('#Hobby').val()
    };
    $.ajax({
        url: "/Home/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function Delele(id) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Manage/Delete/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//function to load data first time
loadData();

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ID').val("");
    $('#Name').val("");
    $('#Email').val("");
    $('#City').val("");
    $('#MobileNo').val("");
    $('#Education').val("");
    //$('#Photo').val("");
    $('#Hobby').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#City').css('border-color', 'lightgrey');
    $('#MobileNo').css('border-color', 'lightgrey');
    $('#Education').css('border-color', 'lightgrey');
    //$('#Photo').css('border-color', 'lightgrey');
    $('#Hobby').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;

    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }

    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }

    if ($('#City').val().trim() == "") {
        $('#City').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#City').css('border-color', 'lightgrey');
    }

    if ($('#MobileNo').val().trim() == "") {
        $('#MobileNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MobileNo').css('border-color', 'lightgrey');
    }


    if ($('#Education').val().trim() == "") {
        $('#Education').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Education').css('border-color', 'lightgrey');
    }
    //if ($('#Photo').val().trim() == "") {
    //    $('#Photo').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Photo').css('border-color', 'lightgrey');
    //}

    if ($('#Hobby').val().trim() == "") {
        $('#Hobby').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Hobby').css('border-color', 'lightgrey');
    }
    return isValid;
}