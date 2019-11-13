namespace EmployeeManagement.Models.Manager.Response
{
    public class Statistic
    {
        public int EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string FullName { get; set; }
        public int Punctual { get; set; }
        public int Late { get; set; }
        public decimal Unauthorized { get; set; }
        public decimal PaidLeave { get; set; }
        public decimal UnpaidLeave { get; set; }
        public int? DaysLeaveRemaining { get; set; }
    }
}
