using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.HumanResources.Response
{
    public class EmployeeInfoView
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public bool Sex { get; set; }

        public DateTime? DOB { get; set; }
        public string Gender => Sex ? "Nam" : "Nữ";
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string TaxId { get; set; }

        public string Image { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EditDate { get; set; }
        public bool? Iswork { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
    }
}
