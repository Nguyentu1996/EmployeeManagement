using EmployeeManagement.DAL.Interface;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Manager.Request;
using EmployeeManagement.Models.Manager.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.DAL
{
    public class ManagerRepository : IManagerRepository
    {
        protected readonly EmployeeManagementContext _dbContext;
        public ManagerRepository(EmployeeManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Statistics
        public IList<TimeSheetList> GetTimeSheetList(GetTimeSheetList model)
        {
            var list = new List<TimeSheetList>();

            try
            {
                int departmentId = _dbContext.Employee.FirstOrDefault(e => e.Id == model.ManagerId && e.IsActive == true && e.IsDelete == false).DepartmentId;

                list = (from e in _dbContext.Employee
                        join t in _dbContext.TimeSheet on e.Id equals t.EmployeeId
                        where e.Id == model.EmployeeId && e.DepartmentId == departmentId && e.IsDelete == false && t.Date.Month == model.Month && t.Date.Year == model.Year
                        orderby t.Date descending
                        select new TimeSheetList
                        {
                            EmployeeId = e.Id,
                            ManagerId = t.ManagerId,
                            FullName = e.FullName,
                            Status = t.Status == 1 ? "Có mặt" :
                                    (t.Status == 2 ? "Trễ" :
                                    (t.Status == 3 ? "Vắng không phép" :
                                    (t.Status == 4 ? "Vắng có phép" :
                                    (t.Status == 5 ? "Vắng không lương" : ""
                                    )))),
                            Date = t.Date.ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public IList<Statistic> GetEmployeeStatistics(GetEmployeeStatistics model)
        {
            var list = new List<Statistic>();

            try
            {
                int DepartmentId = _dbContext.Employee.FirstOrDefault(e => e.Id == model.ManagerId && e.IsActive == true && e.IsDelete == false).DepartmentId;

                list = (from e in _dbContext.Employee
                        join s in _dbContext.Statistics on e.Id equals s.EmployeeId
                        where e.Id == model.EmployeeId && e.DepartmentId == DepartmentId && e.IsDelete == false
                        orderby s.Year descending, s.Month descending
                        select new Statistic
                        {
                            EmployeeId = e.Id,
                            Month = s.Month,
                            Year = s.Year,
                            FullName = e.FullName,
                            Punctual = s.Punctual,
                            Late = s.Late,
                            Unauthorized = s.Unauthorized,
                            PaidLeave = s.PaidLeave,
                            UnpaidLeave = s.UnpaidLeave,
                            DaysLeaveRemaining = s.DaysLeaveRemaining
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public IList<Statistic> GetDepartmentStatistics(GetDepartmentStatistics model)
        {
            var list = new List<Statistic>();

            try
            {
                int DepartmentId = _dbContext.Employee.FirstOrDefault(e => e.Id == model.ManagerId && e.IsActive == true && e.IsDelete == false).DepartmentId;

                list = (from e in _dbContext.Employee
                        join s in _dbContext.Statistics on e.Id equals s.EmployeeId
                        where e.DepartmentId == DepartmentId && e.IsDelete == false
                        orderby s.Year descending, s.Month descending, e.Id ascending
                        select new Statistic
                        {
                            EmployeeId = e.Id,
                            Month = s.Month,
                            Year = s.Year,
                            FullName = e.FullName,
                            Punctual = s.Punctual,
                            Late = s.Late,
                            Unauthorized = s.Unauthorized,
                            PaidLeave = s.PaidLeave,
                            UnpaidLeave = s.UnpaidLeave,
                            DaysLeaveRemaining = s.DaysLeaveRemaining
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }
        #endregion

        #region Leave Application
        public LeaveApplicationList GetEmployeeLeaveApplication(GetEmployeeLeaveApplication model)
        {
            var leaveApplicationList = new LeaveApplicationList();

            try
            {
                int DepartmentId = _dbContext.Employee.FirstOrDefault(e => e.Id == model.ManagerId && e.IsActive == true && e.IsDelete == false).DepartmentId;

                leaveApplicationList = (from e in _dbContext.Employee
                        join l in _dbContext.LeaveApplication on e.Id equals l.EmployeeId
                        where e.DepartmentId == DepartmentId && e.IsDelete == false
                        orderby l.CommentDate descending, l.Id descending
                        select new LeaveApplicationList
                        {
                            LeaveApplicationId = l.Id,
                            FullName = e.FullName,
                            Status = l.Status == 1 ? "Chờ" :
                                    (l.Status == 2 ? "Chấp nhận" :
                                    (l.Status == 3 ? "Từ chối" : ""
                                    )),
                            StartDate = l.StartDate.ToString(),
                            EndDate = l.EndDate.ToString(),
                            DaysLeaveRemaining = l.DaysLeaveRemaining,
                            NumberOfAbsent = l.NumberOfAbsent,
                            CommentDate = l.CommentDate.ToString(),
                            FeedbackDate = l.FeedbackDate == null ? "" : l.FeedbackDate.ToString(),
                            Comment = l.Comment,
                            Feedback = l.Feedback,
                            LeaveCode = l.LeaveCode
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return leaveApplicationList;
        }

        public IList<LeaveApplicationList> GetDepartmentLeaveApplication(GetDepartmentLeaveApplication model)
        {
            var list = new List<LeaveApplicationList>();

            try
            {
                int DepartmentId = _dbContext.Employee.FirstOrDefault(e => e.Id == model.ManagerId && e.IsActive == true && e.IsDelete == false).DepartmentId;

                list = (from e in _dbContext.Employee
                        join l in _dbContext.LeaveApplication on e.Id equals l.EmployeeId
                        where e.DepartmentId == DepartmentId && e.IsDelete == false
                        orderby l.CommentDate descending, l.Id descending
                        select new LeaveApplicationList
                        {
                            LeaveApplicationId = l.Id,
                            FullName = e.FullName,
                            Status = l.Status == 1 ? "Chờ" :
                                    (l.Status == 2 ? "Chấp nhận" :
                                    (l.Status == 3 ? "Từ chối" : ""
                                    )),
                            StartDate = l.StartDate.ToString(),
                            EndDate = l.EndDate.ToString(),
                            DaysLeaveRemaining = l.DaysLeaveRemaining,
                            NumberOfAbsent = l.NumberOfAbsent,
                            CommentDate = l.CommentDate.ToString(),
                            FeedbackDate = l.FeedbackDate == null ? "" : l.FeedbackDate.ToString(),
                            Comment = l.Comment,
                            Feedback = l.Feedback,
                            LeaveCode = l.LeaveCode
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        //public bool UpdateLeaveApplication(UpdateLeaveApplication model)
        //{
        //    var list = new List<TimeSheetList>();

        //    try
        //    {
        //        int DepartmentId = _dbContext.Employee.FirstOrDefault(e => e.Id == model.ManagerId && e.IsActive == true && e.IsDelete == false).DepartmentId;

        //        list = (from e in _dbContext.Employee
        //                join t in _dbContext.TimeSheet on e.Id equals t.EmployeeId
        //                where e.Id == model.EmployeeId && e.DepartmentId == DepartmentId && e.IsDelete == false
        //                orderby t.Date descending
        //                select new TimeSheetList
        //                {
        //                    EmployeeId = e.Id,
        //                    ManagerId = t.ManagerId,
        //                    FullName = e.FullName,
        //                    Status = t.Status == 1 ? "Có mặt" :
        //                            (t.Status == 2 ? "Trễ" :
        //                            (t.Status == 3 ? "Vắng không phép" :
        //                            (t.Status == 4 ? "Vắng có phép" :
        //                            (t.Status == 5 ? "Vắng không lương" : ""
        //                            )))),
        //                    Date = t.Date.ToString()
        //                }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return list;
        //}
        #endregion

        #region Information
        public IList<EmployeeList> GetEmployeeList(GetEmployeeList model)
        {
            var list = new List<EmployeeList>();

            try
            {
                int DepartmentId = _dbContext.Employee.FirstOrDefault(e => e.Id == model.ManagerId && e.IsActive == true && e.IsDelete == false).DepartmentId;

                list = (from e in _dbContext.Employee
                        join p in _dbContext.Position on e.PositionId equals p.Id
                        where e.DepartmentId == DepartmentId && e.IsDelete == false
                        select new EmployeeList
                        {
                            EmployeeId = e.Id,
                            FullName = e.FullName,
                            Sex = e.Sex,
                            Dob = e.Dob.ToString(),
                            PhoneNumber = e.PhoneNumber,
                            Email = e.Email,
                            Image = e.Image,
                            PositionName = p.Name
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }


        #endregion
    }
}
