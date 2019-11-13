using System;

namespace EmployeeManagement.Models.Manager.Response
{
    public class EmployeeList
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public bool Sex { get; set; }
        public string SexStr => Sex ? "Nam" : "Nữ";
        public string Dob { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

    }
}
