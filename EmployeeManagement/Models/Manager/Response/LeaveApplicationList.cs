using System;

namespace EmployeeManagement.Models.Manager.Response
{
    public class LeaveApplicationList
    {
        public int LeaveApplicationId { get; set; }
        public int ManagerId { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? DaysLeaveRemaining { get; set; }
        public int? NumberOfAbsent { get; set; }
        public string CommentDate { get; set; }
        public string FeedbackDate { get; set; }
        public string Comment { get; set; }
        public string Feedback { get; set; }
        public int LeaveCode { get; set; }
        public string Email { get; set; }
    }
}
