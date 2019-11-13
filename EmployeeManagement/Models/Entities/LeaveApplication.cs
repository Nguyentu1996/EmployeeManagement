using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models.Entities
{
    public partial class LeaveApplication
    {
        public int Id { get; set; }
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

        public virtual Employee Employee { get; set; }
        public virtual Employee Manager { get; set; }
    }
}
