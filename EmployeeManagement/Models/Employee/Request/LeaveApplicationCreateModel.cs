using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Employee.Request
{
    public class LeaveApplicationCreateModel
    {
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DaysLeaveRemaining { get; set; }
        public int? NumberOfAbsent { get; set; }
        public DateTime CommentDate { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public string Comment { get; set; }
        public string Feedback { get; set; }
        public int LeaveCode { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
    }
}
