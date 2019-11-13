using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models.Entities
{
    public partial class TimeSheet
    {
        public TimeSheet()
        {
            TimeSheetsLog = new HashSet<TimeSheetsLog>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? In1 { get; set; }
        public TimeSpan? Out1 { get; set; }
        public TimeSpan? In2 { get; set; }
        public TimeSpan? Out2 { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<TimeSheetsLog> TimeSheetsLog { get; set; }
    }
}
