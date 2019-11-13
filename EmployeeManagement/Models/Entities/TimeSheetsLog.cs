using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models.Entities
{
    public partial class TimeSheetsLog
    {
        public int Id { get; set; }
        public int TimeSheetsId { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? In1 { get; set; }
        public TimeSpan? Out1 { get; set; }
        public TimeSpan? In2 { get; set; }
        public TimeSpan? Out2 { get; set; }
        public DateTime EditDate { get; set; }

        public virtual TimeSheet TimeSheets { get; set; }
    }
}
