using EmployeeManagement.DAL.Interface;
using EmployeeManagement.Models.Manager.Request;
using EmployeeManagement.Models.Manager.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;

namespace EmployeeManagement.Controllers
{
    public class ManagerController : Controller
    {
        IManagerRepository _managerRepository;
        public ManagerController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        #region Statistics
        public IActionResult Statistics()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TimeSheetList([FromBody] GetTimeSheetList model)
        {
            model.ManagerId = 1;
            
            var responseData = _managerRepository.GetTimeSheetList(model);

            return Json(new { response = responseData, code = 1 });
        }

        [HttpPost]
        public IActionResult EmployeeStatistics([FromBody] GetEmployeeStatistics model)
        {
            model.ManagerId = 1;
            var responseData = _managerRepository.GetEmployeeStatistics(model);

            return Json(new { response = responseData, code = 1 });
        }

        [HttpPost]
        public IActionResult DepartmentStatistics()
        {
            var model = new GetDepartmentStatistics()
            {
                ManagerId = 1
            };
            var responseData = _managerRepository.GetDepartmentStatistics(model);

            return Json(new { response = responseData, code = 1 });
        }
        #endregion

        #region Leave Application
        public IActionResult LeaveApplication()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmployeeLeaveApplication([FromBody] GetEmployeeLeaveApplication model)
        {
            model.ManagerId = 1;
            var responseData = _managerRepository.GetEmployeeLeaveApplication(model);

            return Json(new { response = responseData, code = 1 });
        }

        [HttpPost]
        public IActionResult DepartmentLeaveApplication()
        {
            var model = new GetDepartmentLeaveApplication()
            {
                ManagerId = 1
            };
            var responseData = _managerRepository.GetDepartmentLeaveApplication(model);

            return Json(new { response = responseData, code = 1 });
        }

        //bool UpdateLeaveApplication(UpdateLeaveApplication model);
        #endregion

        #region Information
        [HttpGet]
        public IActionResult Information()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Gets()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();
            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10,20,50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var model = new GetEmployeeList()
            {
                ManagerId = 1
            };
            var responseData = _managerRepository.GetEmployeeList(model);

            //Sorting  
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                var prop = GetProperty(sortColumn);
                if (sortColumnDirection == "asc")
                {
                    responseData = responseData.OrderBy(prop.GetValue).ToList();
                }
                else
                {
                    responseData = responseData.OrderByDescending(prop.GetValue).ToList();
                }
            }

            //Search  
            if (!string.IsNullOrEmpty(searchValue))
            {
                responseData = (from e in responseData
                                where e.FullName.Contains(searchValue) ||
                                        e.PhoneNumber.Contains(searchValue) ||
                                        e.Email.Contains(searchValue)
                                select e).ToList();
            }

            //total number of rows count   
            recordsTotal = responseData.Count();

            //Paging   
            var data = responseData.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data  
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        private PropertyInfo GetProperty(string name)
        {
            var properties = typeof(EmployeeList).GetProperties();
            PropertyInfo prop = null;
            foreach (var item in properties)
            {
                if (item.Name.ToLower().Equals(name.ToLower()))
                {
                    prop = item;
                    break;
                }
            }
            return prop;
        }
        #endregion
    }
}