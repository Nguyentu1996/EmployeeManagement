namespace EmployeeManagement.Models.Manager.Request
{
    public class GetTimeSheetList : BaseRequest
    {
        public int EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
