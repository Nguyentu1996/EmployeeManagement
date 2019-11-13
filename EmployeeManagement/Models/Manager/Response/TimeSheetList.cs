namespace EmployeeManagement.Models.Manager.Response
{
    public class TimeSheetList
    {
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
}
