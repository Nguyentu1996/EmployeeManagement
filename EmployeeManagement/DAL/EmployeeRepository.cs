using EmployeeManagement.DAL.Interface;
using EmployeeManagement.Models.Employee.Request;
using EmployeeManagement.Models.Employee.Response;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Manager.Request;
using EmployeeManagement.Models.Manager.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementContext _context;

        public EmployeeRepository(EmployeeManagementContext context)
        {
            _context = context;
        }

        public async Task<int> CreateLeaveApp([Bind] LeaveApplicationCreateModel model)
        {
            try
            {
                int id = 0;
                var createLeaveapp = new LeaveApplication
                {
                    EmployeeId = model.EmployeeId,
                    ManagerId = model.ManagerId,
                    Status = model.Status,
                    EndDate = model.EndDate,
                    DaysLeaveRemaining = model.DaysLeaveRemaining,
                    NumberOfAbsent = model.NumberOfAbsent,
                    CommentDate = model.CommentDate,
                    FeedbackDate = model.FeedbackDate,
                    Comment = model.Comment,
                    StartDate = model.StartDate,
                    Feedback = model.Feedback,
                    LeaveCode = model.LeaveCode

                };
                if (_context != null)
                {
                    _context.LeaveApplication.Add(createLeaveapp);
                    await _context.SaveChangesAsync();
                    id = createLeaveapp.Id;
                }

                var sendEmail = EmailService.Send(new SendEmailRequest()
                {
                    Template = "",
                    Body = $"Application for employee permission: <br> " +
                           $"+ FullName: {model.FullName} <br>" +
                           $"+ Department: {model.Department} <br>" +
                           $"+ Start Day Of Leaves: {model.StartDate.ToString("dddd, dd MMMM yyyy")} <br>" +
                           $"+ End Day Of Leaves: {(model.EndDate.HasValue ? model.EndDate.Value.ToString("dddd, dd MMMM yyyy") : "")} <br>" +
                           $"+ Comment: {model.Comment} <br>" +
                           $"+ Date Of Leaves Application: {model.CommentDate.ToString("dddd, dd MMMM yyyy")}",
                    Subject = "Leaves Application",
                    ToEmail = model.Email
                });

                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<LeaveApplicationViewModel> DetailsLeaveApp(int id)
        {
            try
            {
                var detailsLeaveapp = new LeaveApplicationViewModel();
                if (_context != null)
                {
                    detailsLeaveapp = await (from l in _context.LeaveApplication
                                             where l.Id == id
                                             select new LeaveApplicationViewModel
                                             {
                                                 Id = l.Id,
                                                 EmployeeId = l.EmployeeId,
                                                 ManagerId = l.ManagerId,
                                                 Status1 = l.Status == 1 ? "Waiting" : l.Status == 2 ? "Accept" : l.Status == 3 ? "Not Apcept" : "",
                                                 StartDate = l.StartDate.ToString("ddd dd/MM/yyyy"),
                                                 EndDate = l.EndDate.HasValue ? l.EndDate.Value.ToString("ddd dd/MM/yyyy") : " ",
                                                 DaysLeaveRemaining = l.DaysLeaveRemaining,
                                                 NumberOfAbsent = l.NumberOfAbsent,
                                                 CommentDate = l.CommentDate.ToString("ddd dd/MM/yyyy"),
                                                 FeedbackDate = l.FeedbackDate.HasValue ? l.FeedbackDate.Value.ToString("ddd dd/MM/yyyy") : " ",
                                                 Comment = l.Comment,
                                                 Feedback = l.Feedback
                                             }).FirstOrDefaultAsync();

                }
                return detailsLeaveapp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EmployeeViewModel> GetEmployeeById(int id)
        {
            try
            {
                var employee = new EmployeeViewModel();
                if (_context != null)
                {
                    employee = await (from e in _context.Employee
                                      join d in _context.Department on e.DepartmentId equals d.Id
                                      join p in _context.Position on e.PositionId equals p.Id
                                      where e.IsDelete == false && e.IsActive == true
                                       && e.Id == id
                                      select new EmployeeViewModel
                                      {
                                          Id = e.Id,
                                          PositionId = e.PositionId,
                                          Position = p.Name,
                                          Department = d.Name,
                                          IsActive = e.IsActive,
                                          Address = e.Address,
                                          CreateDate = e.CreateDate,
                                          DepartmentId = e.DepartmentId,
                                          Dob = e.Dob,
                                          EditDate = e.EditDate,
                                          Email = e.Email,
                                          FullName = e.FullName,
                                          IdNumber = e.IdNumber,
                                          Image = e.Image,
                                          IsDelete = e.IsDelete,
                                          PhoneNumber = e.PhoneNumber,
                                          Sex = e.Sex,
                                          TaxId = e.TaxId
                                      }).FirstOrDefaultAsync();
                }
                return employee;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LeaveApplicationCreateViewModel> GetLeaveAppOfEmployee(int id)
        {
            try
            {
                var getLeaveApplication = new LeaveApplicationCreateViewModel();
                var departmentId = (from e in _context.Employee
                                    join d in _context.Department on e.DepartmentId equals d.Id
                                    where e.Id == id
                                    select e.DepartmentId).FirstOrDefault();
                var managerId = (from e in _context.Employee
                                 where e.DepartmentId == departmentId && e.PositionId == 1
                                 select e.Id).FirstOrDefault();

                var email = (from e in _context.Employee
                             join p in _context.Position on e.PositionId equals p.Id
                             join d in _context.Department on e.DepartmentId equals d.Id
                             where d.Id == departmentId && p.Id == 1
                             select e.Email).FirstOrDefault();

                var datastatics = await (from s in _context.Statistics
                                         join e in _context.Employee on s.EmployeeId equals e.Id
                                         where e.IsActive == true && e.IsDelete == false && s.EmployeeId == id
                                         select new LeaveApplicationCreateViewModel
                                         {
                                             Late = s.Late,
                                             NumberOfAbsent = (int)(s.PaidLeave + s.UnpaidLeave + s.Unauthorized),
                                             UnpaidLeave = s.UnpaidLeave,
                                             Unauthorized = s.Unauthorized,
                                             DaysLeaveRemaining = s.DaysLeaveRemaining,
                                         }).FirstOrDefaultAsync();


                if (datastatics != null)
                {
                    if (_context != null)
                    {
                        getLeaveApplication = await (from e in _context.Employee
                                                     join d in _context.Department on e.DepartmentId equals d.Id
                                                     where e.IsActive == true && e.IsDelete == false && e.Id == id
                                                     select new LeaveApplicationCreateViewModel
                                                     {
                                                         EmployeeId = e.Id,
                                                         FullName = e.FullName,
                                                         Department = d.Name,

                                                         Late = datastatics.Late,
                                                         NumberOfAbsent = (int)(datastatics.PaidLeave + datastatics.UnpaidLeave + datastatics.Unauthorized),
                                                         PaidLeave = datastatics.PaidLeave,
                                                         UnpaidLeave = datastatics.UnpaidLeave,
                                                         ManagerId = managerId,
                                                         Unauthorized = datastatics.Unauthorized,
                                                         DaysLeaveRemaining = datastatics.DaysLeaveRemaining,
                                                         StartDate = DateTime.Now,
                                                         EndDate = DateTime.Now,
                                                         CommentDate = DateTime.Now,
                                                         Email = email
                                                     }).FirstOrDefaultAsync();
                    }

                }
                else
                {
                    if (_context != null)
                    {
                        getLeaveApplication = await (from e in _context.Employee
                                                     join d in _context.Department on e.DepartmentId equals d.Id
                                                     where e.IsActive == true && e.IsDelete == false && e.Id == id
                                                     select new LeaveApplicationCreateViewModel
                                                     {
                                                         EmployeeId = e.Id,
                                                         FullName = e.FullName,
                                                         Department = d.Name,
                                                         Late = 0,
                                                         NumberOfAbsent = 0,
                                                         PaidLeave = 0,
                                                         UnpaidLeave = 0,
                                                         ManagerId = managerId,
                                                         Unauthorized = 0,
                                                         DaysLeaveRemaining = 0,
                                                         StartDate = DateTime.Now,
                                                         EndDate = DateTime.Now,
                                                         CommentDate = DateTime.Now,
                                                         Email = email
                                                     }).FirstOrDefaultAsync();
                    }
                }
                return getLeaveApplication;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<List<StatisticsViewModel>> GetStatisticsOfEmployee(int id)
        {
            try
            {
                var getStatisticsOfEmployee = new List<StatisticsViewModel>();
                getStatisticsOfEmployee = await (from s in _context.Statistics
                                                 join e in _context.Employee on s.EmployeeId equals e.Id
                                                 where e.IsActive == true && e.IsDelete == false && s.EmployeeId == id
                                                 select new StatisticsViewModel
                                                 {
                                                     EmployeeId = s.EmployeeId,
                                                     Month = s.Month,
                                                     Year = s.Year,
                                                     PaidLeave = s.PaidLeave,
                                                     UnpaidLeave = s.UnpaidLeave,
                                                     Unauthorized = s.Unauthorized,
                                                     Late = s.Late,
                                                     Punctual = s.Punctual,
                                                     DaysLeaveRemaining = s.DaysLeaveRemaining
                                                 }).ToListAsync();

                return getStatisticsOfEmployee;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TimeSheetList>> GetTimeSheetListss( GetTimeSheetList model)
        {
            var getsTimSheet = new List<TimeSheetList>();
            try
            {
                //var departmentId = _context.Employee.FirstOrDefault(e => e.Id == model.EmployeeId && e.IsActive == true && e.IsDelete == false).DepartmentId;
                getsTimSheet = await (from e in _context.Employee
                                      join t in _context.TimeSheet on e.Id equals t.EmployeeId
                                      where e.Id == model.EmployeeId  && e.IsDelete == false
                                      && t.Date.Month == model.Month && t.Date.Year == model.Year
                                      orderby t.Date descending
                                      select new TimeSheetList
                                      {
                                          EmployeeId = e.Id,
                                          ManagerId = t.ManagerId,
                                          FullName = e.FullName,
                                          Status = t.Status == 1 ? "Có Mặt" :
                                                    (t.Status == 2 ? "Trễ" :
                                                    (t.Status == 3 ? "Vắng không phép" :
                                                    (t.Status == 4 ? "Vắng có phép" :
                                                    (t.Status == 5 ? "Vắng không lương" : ""
                                                    )))),
                                          Date = t.Date.ToString("ddd dd/MM/yyyy")
                                      }).ToListAsync();
                return getsTimSheet;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<LeaveApplicationViewModel>> LeaveApllicationOfEmployees(int id)
        {
            try
            {
                var getsLeaveApplication = new List<LeaveApplicationViewModel>();
                getsLeaveApplication = await (from l in _context.LeaveApplication
                                              join e in _context.Employee on l.EmployeeId equals e.Id
                                              where e.IsActive == true && e.IsDelete == false && l.EmployeeId == id
                                              select new LeaveApplicationViewModel
                                              {
                                                  Id = l.Id,
                                                  EmployeeId = l.EmployeeId,
                                                  Owner = e.FullName,
                                                  ManagerId = l.ManagerId,
                                                  Status = l.Status,
                                                  StartDate = l.StartDate.ToString("ddd dd/MM/yyyy"),
                                                  EndDate = l.EndDate.HasValue ? l.EndDate.Value.ToString("ddd dd/MM/yyyy") : " ",
                                                  DaysLeaveRemaining = l.DaysLeaveRemaining,
                                                  NumberOfAbsent = l.NumberOfAbsent,
                                                  CommentDate = l.CommentDate.ToString("ddd dd/MM/yyyy"),
                                                  FeedbackDate = l.FeedbackDate.HasValue ? l.FeedbackDate.Value.ToString("ddd dd/MM/yyyy") : " ",
                                                  Comment = l.Comment,
                                                  Feedback = l.Feedback,
                                                  LeaveCode = l.LeaveCode

                                              }).ToListAsync();
                return getsLeaveApplication;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
