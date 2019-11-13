var statistic = statistic || {};

statistic.drawDataTable = function () {
    $("#tbStatistic").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "oderMulti": false,
        "ajax": {
            "url": "/Employee/GetsStatistics",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "month",
                "name": "Month",
                "autoWidth": true,
                "title": "Month",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "year",
                "name": "Year",
                "autoWidth": true,
                "title": "Year",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "paidLeave",
                "name": "PaidLeave",
                "autoWidth": true,
                "title": "Paid Leave",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "unpaidLeave",
                "name": "UnpaidLeave",
                "autoWidth": true,
                "title": "Unpaid Leave",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "punctual",
                "name": "Punctual",
                "autoWidth": true,
                "title": "Punctual",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "late",
                "name": "Late",
                "autoWidth": true,
                "title": "Late",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "unauthorized",
                "name": "Unauthorized",
                "autoWidth": true,
                "title": "Unauthorized",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "daysLeaveRemaining",
                "name": "DaysLeaveRemaining",
                "autoWidth": true,
                "title": "Days Leave Remaining",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "id",
                "render": function (data, type, full, meta) {
                    var detail = "<a href='javascript:;' onclick='statistic.showDetailsModal("+full.month+","+full.year+")'><i class='fas fa-calendar-week'></i></a>"
                    return $("<div>").append(detail).html();
                },
                "title": "Actions",
                "orderable": false
            }
        ]
    });
};

statistic.getTimeSheetList = function (month, year) {
    var timeSheetListObj = {};
    timeSheetListObj["Month"] = month;
    timeSheetListObj["Year"] = year;
    $.ajax({
        url: '/employee/detailsStatistic/',
        method: 'POST',
        dataType: 'json',
        data: JSON.stringify(timeSheetListObj),
        contentType: 'application/json',
        success: function (data) {
            $('#tblTimeSheetList').empty();
            if (data.code === 1) {
                var response = data.response;
                $.each(response, function (index, value) {
                    var rows = "<tr>"
                        + "<td>" + value.date + "</td>"
                        + "<td>" + value.status + "</td>"
                        + "</tr>";
                    $('#tblTimeSheetList').append(rows);
                });
            }
        }
    });
};

statistic.showDetailsModal = function (m, y) {
    statistic.getTimeSheetList(m, y)
    $("#TimeSheetList").modal('show');
};

statistic.init = function () {
    statistic.drawDataTable();
};

$(document).ready(function () {
    statistic.init();
});