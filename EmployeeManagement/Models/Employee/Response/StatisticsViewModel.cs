using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Employee.Response
{
    public class StatisticsViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Punctual { get; set; }
        public int Late { get; set; }
        public decimal Unauthorized { get; set; }
        public decimal PaidLeave { get; set; }
        public decimal UnpaidLeave { get; set; }
        public int? DaysLeaveRemaining { get; set; }
    }
}
