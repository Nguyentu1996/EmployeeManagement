var manager = manager || {};

manager.getDepartmentStatistics = function () {
    $.ajax({
        url: '/Manager/DepartmentStatistics',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            $('#tblDepartmentStatistics').empty();
            if (data.code === 1) {
                var response = data.response;
                $.each(response, function (index, value) {
                    var rows = "<tr data-id=" + value.employeeId + ">"
                        + "<td>" + value.year + "</td>"
                        + "<td>" + value.month + "</td>"
                        + "<td>" + "<a onclick=\"manager.showModalEmployeeStatistics(this)\" href=\"javascript:void(0);\">" + value.fullName + "</a>" + "</td>"
                        + "<td>" + value.punctual + "</td>"
                        + "<td>" + value.late + "</td>"
                        + "<td>" + value.unauthorized + "</td>"
                        + "<td>" + value.paidLeave + "</td>"
                        + "<td>" + value.unpaidLeave + "</td>"
                        + "<td>" + value.daysLeaveRemaining + "</td>"
                        + "<td>" + "<a onclick=\"manager.showModalTimeSheetList(this)\" href=\"javascript:void(0);\"><i class=\"fas fa-calendar-week\"></i></a>" + "</td>"
                        + "</tr>";
                    $('#tblDepartmentStatistics').append(rows);
                });
            }
        }
    });
};

manager.getTimeSheetList = function (elem) {
    var timeSheetListObj = {};
    timeSheetListObj.EmployeeId = $(elem).closest('tr').data("id");
    timeSheetListObj.Month = $(elem).closest('tr').find('td:nth-child(2)').text();
    timeSheetListObj.Year = $(elem).closest('tr').find('td:nth-child(1)').text();

    $(".modal-title").text($(elem).closest('tr').find('td:nth-child(3)').text());

    $.ajax({
        url: '/Manager/TimeSheetList',
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

manager.getEmployeeStatistics = function (elem) {
    var employeeStatisticsObj = {};
    employeeStatisticsObj.EmployeeId = $(elem).closest('tr').data("id");

    $(".modal-title").text($(elem).closest('tr').find('td:nth-child(3)').text());

    $.ajax({
        url: '/Manager/EmployeeStatistics',
        method: 'POST',
        dataType: 'json',
        data: JSON.stringify(employeeStatisticsObj),
        contentType: 'application/json',
        success: function (data) {
            $('#tblEmployeeStatistics').empty();
            if (data.code === 1) {
                var response = data.response;
                $.each(response, function (index, value) {
                    var rows = "<tr data-id=" + value.employeeId + ">"
                        + "<td>" + value.year + "</td>"
                        + "<td>" + value.month + "</td>"
                        + "<td>" + value.punctual + "</td>"
                        + "<td>" + value.late + "</td>"
                        + "<td>" + value.unauthorized + "</td>"
                        + "<td>" + value.paidLeave + "</td>"
                        + "<td>" + value.unpaidLeave + "</td>"
                        + "<td>" + value.daysLeaveRemaining + "</td>"
                        + "</tr>";
                    $('#tblEmployeeStatistics').append(rows);
                });
            }
        }
    });
};

manager.showModalTimeSheetList = function (elem) {
    manager.getTimeSheetList(elem);
    $("#TimeSheetList").modal({ show: true });
};

manager.showModalEmployeeStatistics = function (elem) {
    manager.getEmployeeStatistics(elem);
    $("#EmployeeStatistics").modal({ show: true });
};

manager.init = function () {
    manager.getDepartmentStatistics();
};

$(document).ready(function () {
    manager.init();
});
