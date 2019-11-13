using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Employee.Response
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public string FullName { get; set; }
        public bool Sex { get; set; }
        public DateTime? Dob { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxId { get; set; }
        public string Image { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
