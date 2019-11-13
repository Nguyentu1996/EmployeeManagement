using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            AnnualLeave = new HashSet<AnnualLeave>();
            LeaveApplicationEmployee = new HashSet<LeaveApplication>();
            LeaveApplicationManager = new HashSet<LeaveApplication>();
            Statistics = new HashSet<Statistics>();
            TimeSheetEmployee = new HashSet<TimeSheet>();
            TimeSheetManager = new HashSet<TimeSheet>();
        }

        public int Id { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
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

        public virtual Department Department { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<AnnualLeave> AnnualLeave { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplicationEmployee { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplicationManager { get; set; }
        public virtual ICollection<Statistics> Statistics { get; set; }
        public virtual ICollection<TimeSheet> TimeSheetEmployee { get; set; }
        public virtual ICollection<TimeSheet> TimeSheetManager { get; set; }
    }
}
