using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models.Entities
{
    public partial class AnnualLeave
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int Year { get; set; }
        public int NumberOfDaysLeave { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
