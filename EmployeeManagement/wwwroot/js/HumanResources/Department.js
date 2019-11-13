var department = department || {};
department.loadData = function () {
    $("p").hide();
    $.ajax({
        url: '/HumanResources/ViewDepartment',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            var data = response.data;
            var html = '';
            var template = $('#data-template').html();
            $.each(data, function (i, value) {
                html += Mustache.render(template, {
                    Id: value.id,
                    Name: value.name,
                    IsActive: value.isActive == true ? "<span class=\'label label- success\'> Hoạt Động</span>" : "<span class=\'label label- danger\'>Khóa</span>",
                    Quantity: value.quantity
                });
            });
            $('#tblData').html(html);
            ;
        }
    })
};
department.getDepartment = function () {
    $.ajax({
        url: '/HumanResources/GetDepartment',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == 1) {
                var data = response.data;
                $.each(data, function (i, value) {
                    $('#department').append("<option value=" + value.id + ">" + value.nameDepartment + "</option>");
                });
            }
        }
    });
};
department.getPosition = function () {
    $.ajax({
        url: '/HumanResources/GetPosition',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == 1) {
                var data = response.data;
                $.each(data, function (i, value) {
                    $('#position').append("<option value=" + value.id + ">" + value.positionName + "</option>");
                });
            }
        }
    });
};
department.back = function () {
    $('#tblEmployee').parents('div.dataTables_wrapper').first().hide();
    department.loadData();
    $('#tableDepartment').show();

};

department.showDataTables = function (id) {
    
    $('#tableDepartment').hide();
    $('#tblEmployee').parents('div.dataTables_wrapper').first().show();
    $("p").show();
    dataTableOption = $("#tblEmployee").DataTable({
        "processing": true, // for show progress bar  
        "serverSide": true, // for process server side  
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "destroy": true,
        "ajax": {
            "url": "/HumanResources/ViewEmployee/" + id,
            "type": "POST",
            "datatype": "json"
        },
        "columns": [

            {
                "data": "fullName",
                "name": "FullName",
                "autoWidth": true,
                "title": "Họ Tên",
                "searchable": true,
                "orderable": true
            },
            {
                "data": "gender",
                "name": "gender",
                "autoWidth": true,
                "title": "Giới Tính",
                "searchable": true,
                "orderable": true
            },

            {
                "data": "phoneNumber",
                "name": "PhoneNumber",
                "autoWidth": true,
                "title": "Phone",
                "orderable": true
            },
            {
                "data": "email",
                "name": "Email",
                "autoWidth": true,
                "title": "Email",
                "orderable": true
            },
            {
                "data": "positionName",
                "name": "PositionName",
                "autoWidth": true,
                "title": "Tên Chức Vụ",
                "orderable": true
            },
            {
                "data": "id",
                "render": function (data, type, full, meta) {
                    return '<a class="btn btn-outline-info"  onclick=department.EditEmployee(' + data + ') >Edit</a>' + ' ' + "<a class='btn btn-outline-danger' onclick=department.DeleteEmployee('" + data + "'); >Delete</a>";
                },
                "title": "Thao Tác",
                "orderable": false
            }
        ]

    });
};
department.DeleteEmployee = function (id) {
    $.ajax({
        url: '/HumanResources/Delete/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status === 1) {
                alert('Success');
                department.reloadDatatable();
            } else
                alert('Try again');
        }
    })
};
department.EditEmployee = function (id) {
    department.resetForm();
    $.ajax({
        url: '/HumanResources/EmployeeGetById/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == 1) {
                var data = response.data;
                console.log(data);
                $('#id').val(data.id);
                $('#fullName').val(data.fullName);
                $('#gender').val(data.gender);
                $('#phoneNumber').val(data.phoneNumber);
                $('#datepicker').val(data.dateOfBirth); 
                $('#idNumber').val(data.idNumber);
                $('#email').val(data.email);
                $('#address').val(data.address);
                $('#taxId').val(data.taxId);
                $("#URL").val(data.Url);
                $("#URLPreview").attr("src", data.Url);
                $('#department').val( data.departmentId);
                $('#position').val( data.positionId);
                $('#editOrAdd').find(".modal-title").text("Edit Employee");
                //var $datepicker = $('#datepicker');
                //$datepicker.datepicker();
                //$datepicker.datepicker('setDate', new Date());
                $('#editOrAdd').modal('show');
            }
        }
    })
};
department.URLUploader = function () {
    $('#URLUploader').fileupload({
        dataType: 'json',
        maxFileSize: 5242880,//5MB
        acceptFileTypes: /(\.|\/)(gif|jpg|png)$/i,
        maxNumberOfFiles: 1,
        autoUpload: true,
        disableImageResize: false,
        imageMaxWidth: 1024,
        imageCrop: false,
        done: function (e, data) {
            $.each(data.result.files, function (index, file) {
                $("#URL").val(file.fullpath);
                $("#URLPreview").attr("src", file.fullpath);
                department.showInlineLoading("#URLUploader", false);
            });
        },
        add: function (e, data) {
            window.NotShowLoading = true;
            department.showInlineLoading("#URLUploader", true);
            data.submit();
        }

    })
};
department.showInlineLoading = function (e, isShow) {
    var img = $(e).parents(".upload-section").find(".inline-loading")
    if (isShow)
        img.show();
    else {
        img.hide();
    }
}

department.submit = function () {
    var obj = {
        Id :$('#id').val(),
        FullName:$('#fullName').val(),
        Sex: $('#gender').val(),
        PhoneNumber:$('#phoneNumber').val(),
        DOB: $('#datepicker').val(),
        IdNumber:$('#idNumber').val(),
        Email:$('#email').val(),
        Address: $('#address').val(),
        TaxId: $('#taxId').val(),
        Image: $('#URL').val(),
        DepartmentId: $('#department').val(),
        PositionId: $('#position').val()
    }
    $.ajax({
        url: '/HumanResources/CreateAndUpdateEmployee',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(obj),
        success: function (response) {
            if (response.status === 1) {
                $('#editOrAdd').modal('hide');
                department.resetForm();
                alert("Success");
                department.reloadDatatable();
            } else
                alert("Fail,please try again");

        }
    })

};

department.reloadDatatable = function () {
    dataTableOption.ajax.reload(null, false);
};
department.createEmployee = function () {
    department.resetForm();
    $('#editOrAdd').modal('show');
};
$(function () {
    $("#datepicker").datepicker({
        dateFormat: 'dd/mm/yy'
    });

});
department.resetForm = function () {
    $('#id').val('0');
    $('#fullName').val('');
    $('#gender').val('');
    $('#phoneNumber').val('');
    $('#datepicker').val('');
    $('#idNumber').val('');
    $('#email').val('');
    $('#address').val('');
    $('#taxId').val('');
    $("#URL").val("");
    $("#URLPreview").attr("src", "");
    $('#department').prop('selectedIndex', 0);
    $('#position').prop('selectedIndex', 0);
    $('#editOrAdd').find(".modal-title").text("Create Employee");

};
department.init = function () {
    department.loadData();
    department.getDepartment();
    department.getPosition();
    department.resetForm();
    $('#editOrAdd').modal('hide');
    department.URLUploader();
 
};

$(document).ready(function () {
    department.init();
});
