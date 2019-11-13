var leaveapp = leaveapp || {};

leaveapp.drawDataTable = function () {
    $('#tbLeaveApp').DataTable({
        "processing": true, // thanh tiến trình
        "serverSide": true, // phía máy chủ
        "filter": true, //vô hiệu hóa bộ lọc tìm kiếm
        "orderMulti": false, // vô hiệu hóa nhiều cột cùng lúc
        "ajax": {
            "url": "/Employee/GetsLeaveApp",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "status",
                "name": "Status",
                "autoWidth": true,
                "title": "Status",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "startDate",
                "name": "StartDate",
                "autoWidth": true,
                "title": "StartDate",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "endDate",
                "name": "EndDate",
                "autoWidth": true,
                "title": "EndDate",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "commentDate",
                "name": "CommentDate",
                "autoWidth": true,
                "title": "CommentDate",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "feedbackDate",
                "name": "FeedbackDate",
                "autoWidth": true,
                "title": "FeedbackDate",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "id",
                "render": function (data, type, full, meta) {
                    //return 'a< class="btn btn-info href="# ' + data + '">Details </a>';
                    //return '<a class="btn btn-info" href="/NhanVien/get/' + data + '">Details</a>'
                    return "<a href='javascript:void(0);' onclick='leaveapp.showDetailsModal(" + data + ")' > Details </a> "
                },
                "title": "Actions",
                "orderable": false
            }
        ]
    });
};

leaveapp.showModal = function (id) {
    //$('#addEditLeaveApp').modal('show');
    $.ajax({
        url: '/employee/GetLeaveApp/' + id,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.code === 1) {
                var response = data.response;
                //console.log(response)
                $("#FullName").val(response.fullName);
                $("#NumberOfAbsent").val(response.numberOfAbsent);
                //$("#StartDate").val(response.startDate);
                $("#StartDate").datepicker({});
                $("#Department").val(response.department);
                $("#PaidLeave").val(response.paidLeave);
                $("#EndDate").datepicker();
                $("#DaysLeaveRemaining").val(response.daysLeaveRemaining);
                $("#Comment").val(response.comment);    
                $("#EmployeeId").val(response.employeeId);
                $("#ManagerId").val(response.managerId);
                $("#Email").val(response.email);
                $("#CommentDate").val(response.commentDate);
                $("#UnpaidLeave").val(response.unpaidLeave);

                $('#addEditLeaveApp').modal('show');
            }
        }
    });
};

leaveapp.save = function () {
    var objectLeaveapp = {};
    objectLeaveapp["EmployeeId"] = $("#EmployeeId").val();
    objectLeaveapp["ManagerId"] = $("#ManagerId").val();
    objectLeaveapp["Status"] = 1;
    objectLeaveapp["StartDate"] = $("#StartDate").val();
    objectLeaveapp["EndDate"] = $("#EndDate").val();
    objectLeaveapp["DaysLeaveRemaining"] = $("#DaysLeaveRemaining").val();
    objectLeaveapp["NumberOfAbsent"] = $("#NumberOfAbsent").val();
    objectLeaveapp["CommentDate"] = $("#CommentDate").val();
    objectLeaveapp["Comment"] = $("#Comment").val();
    objectLeaveapp["FeedbackDate"] = new Date();
    objectLeaveapp["Feedback"] = ""
    objectLeaveapp["LeaveCode"] = $("#LeaveCode").val();
    objectLeaveapp["Email"] = $("#Email").val();
    objectLeaveapp["FullName"] = $("#FullName").val();
    objectLeaveapp["Department"] = $("#Department").val();

    $.ajax({
        url: '/employee/CreateLeaveApp',
        method: 'POST',
        data: JSON.stringify(objectLeaveapp),   // convert obj to string json
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                $('#addEditLeaveApp').modal('hide');
                leaveapp.drawDataTable();
            }
        }

    });
}

leaveapp.showDetailsModal = function (id) {
    //$('#detailsModal').modal('show');
    $.ajax({
        url: '/employee/DetailsLeaveApp/' + id,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.code === 1) {
                var response = data.response;
                $("#Status1").val(response.status1);
                $("#detailsModal #StartDate").val(response.startDate);
                $("#detailsModal #EndDate").val(response.endDate);
                $("#detailsModal #DaysLeaveRemaining").val(response.daysLeaveRemaining);
                $("#detailsModal #NumberOfAbsent").val(response.numberOfAbsent);
                $("#detailsModal #CommentDate").val(response.commentDate);
                $("#detailsModal #FeedbackDate").val(response.feedbackDate);
                $("#detailsModal #Comment").val(response.comment);
                $("#detailsModal #Feedback").val(response.feedback);
                

                $('#detailsModal').modal('show');
            }
        }
    });
};

leaveapp.init = function () {
    leaveapp.drawDataTable();
};

$(document).ready(function () {
    leaveapp.init();
});