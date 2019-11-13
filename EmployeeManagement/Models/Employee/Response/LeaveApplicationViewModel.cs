using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Employee.Response
{
    public class LeaveApplicationViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public string Owner { get; set; }
        public int Status { get; set; }
        public string Status1 { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? DaysLeaveRemaining { get; set; }
        public int? NumberOfAbsent { get; set; }
        public string CommentDate { get; set; }
        public string FeedbackDate { get; set; }
        public string Comment { get; set; }
        public string Feedback { get; set; }
        public int LeaveCode { get; set; }
    }
}
