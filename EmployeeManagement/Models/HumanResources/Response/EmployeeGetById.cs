using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.HumanResources.Response
{
    public class EmployeeGetById
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public bool Sex { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string DateOfBirth => DOB.ToString("dd/MM/yyyy");

        public string SexStr => Sex ? "Nam" : "Nữ";
        public int Gender => Sex ? 1 : 0;
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
