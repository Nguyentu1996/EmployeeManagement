using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models.Entities
{
    public partial class Position
    {
        public Position()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
