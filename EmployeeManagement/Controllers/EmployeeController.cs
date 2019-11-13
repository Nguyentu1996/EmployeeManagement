using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Interface;
using EmployeeManagement.Models.Employee.Request;
using EmployeeManagement.Models.Employee.Response;
using EmployeeManagement.Models.Manager.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _emprepository;
        private static int employeeId = 0;

        public EmployeeController(IEmployeeRepository emprepository)
        {
            _emprepository = emprepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var employee = await _emprepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        #region Create LeaveApplication
        [HttpPost]
        public async Task<JsonResult> CreateLeaveApp([FromBody] LeaveApplicationCreateModel model)
        {
            var createResult = 0;
            createResult = await _emprepository.CreateLeaveApp(model);
            if (createResult > 0)
            {
                return Json(new { status = 1, message = "Leave Applications has been created successfully" });
            }
            return Json(new { status = 0, message = "Something wrong !" });
        }

        public IActionResult GetLeaveApp(int id)
        {
            var leaveapp = Task.Run(async () => await _emprepository.GetLeaveAppOfEmployee(id)).Result;
            if (leaveapp == null)
            {
                return NotFound();
            }
            return Json(new { response = leaveapp, code = 1 });
        }

        //public 

        [HttpGet]
        public IActionResult DetailsLeaveApp(int id)
        {
            var leaveapp = Task.Run(async () => await _emprepository.DetailsLeaveApp(id)).Result;
            if (leaveapp == null)
            {
                return NotFound();
            }
            return Json(new { response = leaveapp, code = 1 });
        }

        #endregion

        #region LeaveAppOfEmployee
        public IActionResult LeaveApp(int id)
        {
            employeeId = id;
            ViewBag.Id = id;
            return View();
        }

        public JsonResult GetsLeaveApp()
        {
            try
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
                var leaveapps = Task.Run(async () => await _emprepository.LeaveApllicationOfEmployees(employeeId)).Result;
                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    var prop = GeLeaveApptProperty(sortColumn);
                    if (sortColumnDirection == "asc")
                    {
                        leaveapps = leaveapps.OrderBy(prop.GetValue).ToList();
                    }
                    else
                    {
                        leaveapps = leaveapps.OrderByDescending(prop.GetValue).ToList();
                    }
                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    //_employees = (from e in _employees
                    //              where e.EmployeeName.Contains(searchValue) ||
                    //                   e.Skill.Contains(searchValue) ||
                    //                   e.PhoneNumber.Contains(searchValue)
                    //              select e).ToList();
                }
                //total number of rows count
                recordsTotal = leaveapps.Count();
                //Paging
                var data = leaveapps.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PropertyInfo GeLeaveApptProperty(string name)
        {
            var properties = typeof(LeaveApplicationViewModel).GetProperties();
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

        #region Get Statistics
        public IActionResult Statistic(int id)
        {
            employeeId = id;
            ViewBag.Id = id;
            return View();
        }

        public JsonResult GetsStatistics()
        {
            try
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
                var leaveapps = Task.Run(async () => await _emprepository.GetStatisticsOfEmployee(employeeId)).Result;
                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    var prop = GeStatistictProperty(sortColumn);
                    if (sortColumnDirection == "asc")
                    {
                        leaveapps = leaveapps.OrderBy(prop.GetValue).ToList();
                    }
                    else
                    {
                        leaveapps = leaveapps.OrderByDescending(prop.GetValue).ToList();
                    }
                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    //_employees = (from e in _employees
                    //              where e.EmployeeName.Contains(searchValue) ||
                    //                   e.Skill.Contains(searchValue) ||
                    //                   e.PhoneNumber.Contains(searchValue)
                    //              select e).ToList();
                }
                //total number of rows count
                recordsTotal = leaveapps.Count();
                //Paging
                var data = leaveapps.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        private PropertyInfo GeStatistictProperty(string name)
        {
            var properties = typeof(StatisticsViewModel).GetProperties();
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

        [HttpPost]
        public IActionResult DetailsStatistic([FromBody] GetTimeSheetList model)
        {
            model.EmployeeId = employeeId;
            var statistics = Task.Run(async () => await _emprepository.GetTimeSheetListss(model)).Result;
            if (statistics == null)
            {
                return NotFound();
            }
            return Json(new { response = statistics, code = 1 });
        }
        #endregion
    }
}