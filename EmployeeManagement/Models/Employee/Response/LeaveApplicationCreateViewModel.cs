using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Employee.Request
{
    public class LeaveApplicationCreateViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public string Department { get; set; }
        public string Owner { get; set; }
        public int Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public int? DaysLeaveRemaining { get; set; }
        public int? NumberOfAbsent { get; set; }
        public DateTime CommentDate { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public string Comment { get; set; }
        public string Feedback { get; set; }
        public string FullName { get; set; }
        public int LeaveCode { get; set; }
        public int Late { get; set; }
        public decimal Unauthorized { get; set; }
        public string Email { get; set; }
        public decimal PaidLeave { get; set; }
        public decimal UnpaidLeave { get; set; }
    }
}
