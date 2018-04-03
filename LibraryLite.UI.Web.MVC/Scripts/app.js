'use strict';

function Contains(text_one,text_two){
    if(text_one.indexOf(text_two) != -1)
        return true;
}
$("#search").keyup(function(){
    var searchText =$("#search").val().toLowerCase();
    $("#tableId").each(function () {
        if(!Contains($(this).text().toLowerCase(),searchText)){
            $(this).hide();
        }
        else{
            $(this).show();
        }
    });
});

function getBookLoans(){
    var deleteUrl = '/BookLoan/ReturnBook';
    $.ajax({
        url: '/BookLoan/GetBookLoans',
        verb: 'POST',
        dataType: 'json',
        data: { id: $("#studentId").val() },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            ($('#tableId tbody').empty());
            var items = "";
            $.each(data, function (i, item) {
                var rows = "<tr>"
                + "<td>" + item.FirstName + "</td>"
                + "<td>" + item.LastName + "</td>"
                + "<td>" + item.BookTitle + "</td>"
                + "<td>" + item.DateOfIssue + "</td>"
                + "<td>" + item.DueDate + "</td>"
                + '<td><a href=\"' + deleteUrl +"/"+ item.Id +'"><i class="fa fa-edit"></i>Return book</a></td>'
                + "</tr>"
                $('#tableId tbody').append(rows);
            });
        },
        error: function (ex) {
            var r = ex.responseText;
        }
    });
}

function loadReport() {
    var filter = new Object();

    filter.MonthId = $("#monthId").val();
    filter.Date = $("#date").val();
    filter.Year = $("#year").val();

    $.ajax({
        url: '/Reports/GetRevenueReport',
        verb: 'POST',
        data: filter,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var items = "";
            ($('#tableId tbody').empty());
            $.each(data, function (i, item) {

                var convertJSONDate = function (jsonDate) {
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var dd = date.getDate();
                    var mm = date.getMonth() + 1; //January is 0!

                    var yyyy = date.getFullYear();
                    if (dd < 10) {
                        dd = '0' + dd;
                    }
                    if (mm < 10) {
                        mm = '0' + mm;
                    }
                    var formatedDate = dd + '/' + mm + '/' + yyyy;

                    return formatedDate;
                }

                var rows = "<tr>"
                + "<td>" + item.FirstName + "</td>"
                + "<td>" + item.LastName + "</td>"
                + "<td>" + convertJSONDate(item.DueDate) + "</td>"
                + "<td>" + convertJSONDate(item.DateReturned) + "</td>"
                + "<td class='sum'>" + item.Penalty + "</td>"
                + "</tr>"
                $('#tableId tbody').append(rows);
            });
            var sum = 0;
            $("#tableId tbody .sum").each(function () {
                var value = $(this)[0].innerText;

                if (!isNaN(value) && value.length != 0) {
                    sum += parseFloat(value);
                }
            });
            var row = "<tr>"
                + "<td>" + "" + "</td>"
                + "<td>" + "" + "</td>"
                + "<td>" + "" + "</td>"
                + "<td>" + "" + "</td>"
                + "<td class='sum'>" + sum + "</td>"
                + "</tr>"
            $('#tableId tbody').append(row);
        },
        error: function (ex) {
            var r = ex.responseText;
        }
    });
}