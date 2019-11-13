using System;

namespace EmployeeManagement.Models.Manager.Request
{
    public class UpdateLeaveApplication : BaseRequest
    {
        public int LeaveApplicationId { get; set; }
        public int Status { get; set; }
        public string Feedback { get; set; }
        public string FullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public int LeaveCode { get; set; }
        public string Email { get; set; }
    }
}
