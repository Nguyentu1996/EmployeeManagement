using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.HumanResources.Request
{
    public class CreateEmployee
    {
        public string FullName { get; set; }

        public int Sex { get; set; }

        public DateTime? DOB { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string TaxId { get; set; }

        public string Image { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EditDate { get; set; }
        public bool? Iswork { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
    }
}
