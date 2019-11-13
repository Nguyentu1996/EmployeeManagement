using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EmployeeManagement.DAL;
using EmployeeManagement.DAL.Interface;
using EmployeeManagement.Models.HumanResources.Request;
using EmployeeManagement.Models.HumanResources.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeManagement.Controllers
{
    public class HumanResourcesController : Controller
    {
        private readonly IHumanResourcesRepository _repository;
        private static int departmentId;

        public HumanResourcesController(IHumanResourcesRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SeeEmployee()
        {
            return View();
        }
        public async Task<JsonResult> ViewDepartment()
        {
            IList<DepartmentView> list = new List<DepartmentView>();
            try
            {
              list = (await _repository.GetInfoDerpart());
                
            }catch(Exception exp)
            {
                throw exp;
            }
            return Json(new
            {
                data = list

            });
        }
        public async Task<JsonResult> GetDepartment()
        {
            IList<DepartmentModel> list = new List<DepartmentModel>();
            try
            {
                list = (await _repository.GetDepartments());
            }catch(Exception exp)
            {
                throw exp;
            }
            return Json(new { data = list,status=1 });
        }
        public async Task<JsonResult> GetPosition()
        {
            IList<PositionModel> model = new List<PositionModel>();
            try
            {
                model = (await _repository.GetPositions());
                
            }catch(Exception exp)
            {
                throw exp;
            }
            return Json(new { data = model, status = 1 });
        }
        public async Task<JsonResult> ViewEmployee(int id)
        {
            departmentId = id;
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

                var _employees = await(_repository.GetEmployeeByDerpartmentId(id)); 
                                 

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    var prop = GetProperty(sortColumn);
                    if (sortColumnDirection == "asc")
                    {
                        _employees = _employees.OrderBy(prop.GetValue).ToList();
                    }
                    else
                    {
                        _employees = _employees.OrderByDescending(prop.GetValue).ToList();
                    }
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    _employees = await (_repository.SearchEmployee(searchValue, departmentId));
                    //_employees = (from e in _employees
                    //              where e.EmployeeName.Contains(searchValue) ||
                    //                   e.Skill.Contains(searchValue) ||
                    //                   e.PhoneNumber.Contains(searchValue)
                    //              select e).ToList();
                }

                //total number of rows count   
                recordsTotal = _employees.Count();
                //Paging   
                var data = _employees.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
            //IList<EmployeeView> list = new List<EmployeeView>();
            //try
            //{
            //    list = (await _repository.GetEmployeeByDerpartmentId(id));

            //}
            //catch (Exception exp)
            //{
            //    throw exp;
            //}
            //return Json(new
            //{
            //    data = list

            //});
        }
        private PropertyInfo GetProperty(string name)
        {
            var properties = typeof(EmployeeView).GetProperties();
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
        public async Task<JsonResult> GetEmployeeInfo(int id)
        {
            var result = new EmployeeInfoView();
            try
            {
                result = (await _repository.GetEmployeeInfo(id));
                
            }catch(Exception exp)
            {
                throw exp;
            }
            return Json(new { data = result, status = 1 });
        }
        public async Task<JsonResult> EmployeeGetById(int id)
        {
            var result = new EmployeeGetById();
            try
            {
                result = (await _repository.EmployeeGetById(id));
            }catch(Exception exp)
            {
                throw exp;
            }
            return Json(new { data = result, status = 1 });

        }

        [HttpPost]
        public async Task<JsonResult>CreateAndUpdateEmployee([FromBody]  CreateAndUpdate updateAndCreateEmployee)
        {
            var result =0;
            if (updateAndCreateEmployee.Id != 0)
            {
                try
                {
                    var employee = new UpdateEmployee
                    {
                        Id = updateAndCreateEmployee.Id,
                        FullName = updateAndCreateEmployee.FullName,
                        PositionId = updateAndCreateEmployee.PositionId,
                        DepartmentId = updateAndCreateEmployee.DepartmentId,
                        Sex = updateAndCreateEmployee.Sex,
                        DOB = updateAndCreateEmployee.DOB,
                        IdNumber = updateAndCreateEmployee.IdNumber,
                        PhoneNumber = updateAndCreateEmployee.PhoneNumber,
                        Email = updateAndCreateEmployee.Email,
                        Address = updateAndCreateEmployee.Address,
                        TaxId = updateAndCreateEmployee.TaxId,
                        Image = updateAndCreateEmployee.Image,
                        EditDate=DateTime.Now
                    };
                   result= await _repository.UpdateEmployee(employee);

                }catch(Exception exp)
                {
                    throw exp;
                }
            }
            else
            {
                try
                {
                    var create = new CreateEmployee
                    {
                        FullName = updateAndCreateEmployee.FullName,
                    
                        PositionId = updateAndCreateEmployee.PositionId,
                        DepartmentId = updateAndCreateEmployee.DepartmentId,
                        Sex = updateAndCreateEmployee.Sex,
                        DOB = updateAndCreateEmployee.DOB,
                        IdNumber = updateAndCreateEmployee.IdNumber,
                        PhoneNumber = updateAndCreateEmployee.PhoneNumber,
                        Email = updateAndCreateEmployee.Email,
                        Address = updateAndCreateEmployee.Address,
                        TaxId = updateAndCreateEmployee.TaxId,
                        Image = updateAndCreateEmployee.Image

                    };
                   result= await _repository.CreateEmployee(create);
                    
                }catch(Exception exp)
                {
                    throw exp;
                }
                
            }
            if (result > 0)
            {
                return Json(new { status = 1, message = " successfully." });
            }
            return Json(new { status = 0, message = "Something went wrong, please contact administrator." });
        }
        public async Task<JsonResult>Delete(int id)
        {
            var result = 0;
            try
            {
               result= await _repository.DeleteEmployee(id);
            }catch(Exception exp)
            {
                throw exp;
            }
            if (result > 0)
            {
                return Json(new { status = 1, message = " successfully." });
            }
            return Json(new { status = 0, message = "Something went wrong, please contact administrator." });
        }

    }
}