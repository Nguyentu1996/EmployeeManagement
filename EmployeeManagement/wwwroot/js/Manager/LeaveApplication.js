var manager = manager || {};

manager.getDepartmentLeaveApplication = function () {
    $.ajax({
        url: '/Manager/DepartmentLeaveApplication',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            $('#tblDepartmentLeaveApplication').empty();
            if (data.code === 1) {
                var response = data.response;
                $.each(response, function (index, value) {
                    var rows = "<tr data-id=" + value.leaveApplicationId + ">"
                        + "<td>" + value.fullName + "</td>"
                        + "<td>" + value.status + "</td>"
                        + "<td>" + value.startDate + "</td>"
                        + "<td>" + value.endDate + "</td>"
                        + "<td>" + value.commentDate + "</td>"
                        + "<td>" + value.feedbackDate + "</td>"
                        + "<td>" + "<a onclick=\"manager.showModalEmployeeLeaveApplication(this)\" href=\"javascript:void(0);\"><i class=\"fas fa-calendar-week\"></i></a>" + "</td>"
                        + "</tr>";
                    $('#tblDepartmentLeaveApplication').append(rows);
                });
            }
        }
    });
};

//manager.getEmployeeLeaveApplication = function () {
//    $.ajax({
//        url: '/Manager/EmployeeLeaveApplication',
//        method: 'POST',
//        dataType: 'json',
//        contentType: 'application/json',
//        success: function (data) {
//            $('#tblDepartmentLeaveApplication').empty();
//            if (data.code === 1) {
//                var response = data.response;
//                $.each(response, function (index, value) {
//                    var rows = "<tr data-id=" + value.leaveApplicationId + ">"
//                        + "<td>" + value.fullName + "</td>"
//                        + "<td>" + value.status + "</td>"
//                        + "<td>" + value.startDate + "</td>"
//                        + "<td>" + value.endDate + "</td>"
//                        + "<td>" + value.commentDate + "</td>"
//                        + "<td>" + value.feedbackDate + "</td>"
//                        + "<td>" + "<a onclick=\"manager.showModalTimeSheetList(this)\" href=\"javascript:void(0);\"><i class=\"fas fa-calendar-week\"></i></a>" + "</td>"
//                        + "</tr>";
//                    $('#tblDepartmentLeaveApplication').append(rows);
//                });
//            }
//        }
//    });
//};

manager.showModalEmployeeLeaveApplication = function (elem) {
    //manager.getEmployeeLeaveApplication(elem);
    $("#EmployeeLeaveApplication").modal({ show: true });
};

manager.init = function () {
    manager.getDepartmentLeaveApplication();
};

$(document).ready(function () {
    manager.init();
});