var manager = manager || {};

manager.getInformation = function () {
    $("#tbl").DataTable({
        "processing": true, // for show progress bar  
        "serverSide": true, // for process server side  
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "ajax": {
            "url": "/Manager/Gets",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "fullName",
                "name": "FullName",
                "autoWidth": true,
                "title": "Họ và tên",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "positionName",
                "name": "PositionName",
                "autoWidth": true,
                "title": "Chức vụ",
                "searchable": false,
                "orderable": true
            },
            {
                "data": "sexStr",
                "name": "Sex",
                "autoWidth": true,
                "title": "Giới tính",
                "searchable": false,
                "orderable": true
            },
            {
                "data": "dob",
                "name": "Dob",
                "autoWidth": true,
                "title": "Ngày sinh",
                "searchable": false,
                "orderable": true
            },
            {
                "data": "phoneNumber",
                "name": "PhoneNumber",
                "autoWidth": true,
                "title": "Số điện thoại",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "email",
                "name": "Email",
                "autoWidth": true,
                "title": "Email",
                "searchable": true,
                "orderable": true
            }
        ]
    });
};

manager.init = function () {
    manager.getInformation();
};

$(document).ready(function () {
    manager.init();
});