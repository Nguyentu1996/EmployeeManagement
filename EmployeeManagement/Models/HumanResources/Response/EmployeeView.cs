using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.HumanResources.Response
{
    public class EmployeeView
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool Sex { get; set; }
        //public string IdCode { get; set; }
        public string Gender => Sex ? "Nam" : "Nữ";

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PositionName { get; set; }
    }
}
