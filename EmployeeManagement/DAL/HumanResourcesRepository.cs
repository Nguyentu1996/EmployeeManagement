using EmployeeManagement.DAL.Interface;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.HumanResources.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models.HumanResources.Request;

namespace EmployeeManagement.DAL
{
    public class HumanResourcesRepository : IHumanResourcesRepository
    {
        EmployeeManagementContext db;
        public HumanResourcesRepository(EmployeeManagementContext context)
        {
            db = context;
        }

        public async Task<IList<EmployeeView>> GetEmployeeByDerpartmentId(int id)
        {
            var list = new List<EmployeeView>();
            try
            {
                list = await (from e in db.Employee
                              join p in db.Position on e.PositionId equals p.Id
                              where e.DepartmentId == id
                              && e.IsDelete == false
                              && e.IsActive == true
                              select new EmployeeView
                              {
                                  Id = e.Id,
                                  FullName = e.FullName,
                                  Sex = e.Sex,
                                  Email = e.Email,
                                  PhoneNumber = e.PhoneNumber,
                                  PositionName = p.Name
                              }).ToListAsync();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }

        public async Task<EmployeeInfoView> GetEmployeeInfo(int id)
        {
            var result = new EmployeeInfoView();
            try
            {
                result = await (from e in db.Employee
                                join p in db.Position on e.PositionId equals p.Id
                                join d in db.Department on e.DepartmentId equals d.Id
                                where e.Id == id
                                select new EmployeeInfoView
                                {
                                    Id = e.Id,
                                    FullName = e.FullName,
                                    Image = e.Image,
                                    Sex = e.Sex,
                                    DOB = e.Dob,
                                    IdNumber = e.IdNumber,
                                    PhoneNumber = e.PhoneNumber,
                                    Email = e.Email,
                                    Address = e.Address,
                                    TaxId = e.TaxId,
                                    CreateDate = e.CreateDate,
                                    EditDate = e.EditDate,
                                    Iswork = e.IsActive,
                                    DepartmentName = d.Name,
                                    PositionName = p.Name
                                }
                               ).FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return result;
        }

        public async Task<IList<DepartmentView>> GetInfoDerpart()
        {
            var list = new List<DepartmentView>();
            try
            {
                if (db != null)
                {
                    list = await (from d in db.Department
                                  where d.IsDelete == false && d.IsActive == true
                                  select new DepartmentView
                                  {
                                      Id = d.Id,
                                      Name = d.Name,
                                      IsActive = d.IsActive,
                                      Quantity = (from e in db.Employee
                                                  where e.DepartmentId == d.Id
                                                  && e.IsActive == true
                                                  && e.IsDelete == false
                                                  select e.Id).Count()

                                  }).ToListAsync();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return list;
        }
        public async Task<int> CreateEmployee(CreateEmployee employee)
        {
            int id = 0;
            var create = new Employee
            {
                FullName = employee.FullName,
                PositionId = employee.PositionId,
                DepartmentId = employee.DepartmentId,
                Sex = employee.Sex!=0,
                Dob = employee.DOB,
                IdNumber = employee.IdNumber,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Address = employee.Address,
                TaxId = employee.TaxId,
                Image = employee.Image,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                IsActive = true,
                IsDelete = false
            };
            try
            {
                if (create.PositionId == 1)
                {
                    int select = await (from e in db.Employee
                                        where e.PositionId == 1 && e.DepartmentId == create.DepartmentId
                                        && e.IsDelete == false && e.IsActive == true
                                        select e.Id).FirstOrDefaultAsync();
                    if (select != 0)
                    {
                        var emp = await (from e in db.Employee
                                         where e.PositionId == 1 && e.DepartmentId == create.DepartmentId
                                         && e.IsDelete == false && e.IsActive == true
                                         select e).FirstOrDefaultAsync();
                        var update = new Employee
                        {
                            Id = emp.Id,
                            PositionId = 3,
                            FullName = emp.FullName,
                            DepartmentId = emp.DepartmentId,
                            Sex = emp.Sex,
                            Dob = emp.Dob,
                            IdNumber = emp.IdNumber,
                            PhoneNumber = emp.PhoneNumber,
                            Email = emp.Email,
                            Address = emp.Address,
                            TaxId = emp.TaxId,
                            Image = emp.Image,
                            EditDate = emp.EditDate,
                            IsActive = true
                        };
                        db.Employee.Update(update);
                        await db.SaveChangesAsync();
                        return id = update.Id;
                    }                  
                }
                db.Employee.Add(create);
                await db.SaveChangesAsync();
                return id = create.Id;
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }
        public async Task<int> UpdateEmployee(UpdateEmployee employee)
        {
            int id = 0;
            var update = new Employee
            {
                Id = employee.Id,
                FullName = employee.FullName,
                PositionId = employee.PositionId,
                DepartmentId = employee.DepartmentId,
                Sex = employee.Sex!=0,
                Dob = employee.DOB,
                IdNumber = employee.IdNumber,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Address = employee.Address,
                TaxId = employee.TaxId,
                Image = employee.Image,
                EditDate = employee.EditDate,
                IsActive = true
            };
            try
            {
                if (update.PositionId == 1)
                {
                    int select = await (from e in db.Employee
                                        where e.PositionId == 1 && e.DepartmentId == update.DepartmentId
                                        && e.IsDelete == false && e.IsActive == true
                                        select e.Id).FirstOrDefaultAsync();
                    if (select != 0)
                    {
                        var emp = await (from e in db.Employee
                                         where e.PositionId == 1 && e.DepartmentId == update.DepartmentId
                                         && e.IsDelete == false && e.IsActive == true
                                         select e).FirstOrDefaultAsync();
                        var updateEmployee = new Employee
                        {
                            Id = emp.Id,
                            PositionId = 3,
                            FullName = emp.FullName,
                            DepartmentId = emp.DepartmentId,
                            Sex = emp.Sex,
                            Dob = emp.Dob,
                            IdNumber = emp.IdNumber,
                            PhoneNumber = emp.PhoneNumber,
                            Email = emp.Email,
                            Address = emp.Address,
                            TaxId = emp.TaxId,
                            Image = emp.Image,
                            EditDate = emp.EditDate,
                            IsActive = true
                        };
                        db.Employee.Update(updateEmployee);
                        await db.SaveChangesAsync();
                        return id = updateEmployee.Id;
                    }
                }
                db.Employee.Update(update);
                await db.SaveChangesAsync();
                return id = update.Id;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public async Task<int> DeleteEmployee(int id)
        {
            try
            {
                var result = (from e in db.Employee
                              where e.Id == id
                              select e).FirstOrDefault();
                result.IsDelete = true;
                result.IsActive = false;
                db.Employee.Update(result);
                await db.SaveChangesAsync();
                return result.Id;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public async Task<IList<DepartmentModel>> GetDepartments()
        {
            var list = new List<DepartmentModel>();
            try
            {
                list = await (from d in db.Department
                              select new DepartmentModel
                              {
                                  Id = d.Id,
                                  NameDepartment = d.Name
                              }).ToListAsync();
                return list;

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public async Task<IList<PositionModel>> GetPositions()
        {
            var list = new List<PositionModel>();
            try
            {
                list = await (from p in db.Position
                              select new PositionModel
                              {
                                  Id = p.Id,
                                  PositionName = p.Name
                              }).ToListAsync();
                return list;

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public async Task<EmployeeGetById> EmployeeGetById(int id)
        {
            var result = new EmployeeGetById();
            try
            {
                result = await (from e in db.Employee
                                where e.Id == id
                                select new EmployeeGetById
                                {
                                    Id = e.Id,
                                    FullName = e.FullName,
                                    Image = e.Image,
                                    Sex = e.Sex,
                                    DOB = (DateTime)e.Dob,
                                    IdNumber = e.IdNumber,
                                    PhoneNumber = e.PhoneNumber,
                                    Email = e.Email,
                                    Address = e.Address,
                                    TaxId = e.TaxId,
                                    CreateDate = e.CreateDate,
                                    EditDate = e.EditDate,
                                    Iswork = e.IsActive,
                                    DepartmentId = e.DepartmentId,
                                    PositionId = e.PositionId
                                }).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
       public async Task<IList<EmployeeView>> SearchEmployee(string search,int departmentId)
        {
            try
            {
                var result = new List<EmployeeView>();
                if(search != null)
                {
                    result = await (from e in db.Employee
                                    join p in db.Position on e.PositionId equals p.Id
                                    where e.DepartmentId == departmentId
                                    && e.IsDelete == false
                                    && e.IsActive == true 
                                    && e.FullName.Contains(search)
                                    || e.Email.Contains(search)
                                    || e.PhoneNumber.Contains(search)
                                   
                                    select new EmployeeView
                                    {
                                        Id = e.Id,
                                        FullName = e.FullName,
                                        Sex = e.Sex,
                                        Email = e.Email,
                                        PhoneNumber = e.PhoneNumber,
                                        PositionName = p.Name
                                    }).ToListAsync();
                }
                else
                {
                    result = await (from e in db.Employee
                                  join p in db.Position on e.PositionId equals p.Id
                                  where e.DepartmentId == departmentId
                                  && e.IsDelete == false
                                  && e.IsActive == true
                                  select new EmployeeView
                                  {
                                      Id = e.Id,
                                      FullName = e.FullName,
                                      Sex = e.Sex,
                                      Email = e.Email,
                                      PhoneNumber = e.PhoneNumber,
                                      PositionName = p.Name
                                  }).ToListAsync();
                }
                return result; 
            }catch(Exception exp)
            {
                throw exp;
            }
        }

    }
}
