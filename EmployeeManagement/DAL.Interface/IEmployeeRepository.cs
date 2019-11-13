using EmployeeManagement.Models.Employee.Request;
using EmployeeManagement.Models.Employee.Response;
using EmployeeManagement.Models.Manager.Request;
using EmployeeManagement.Models.Manager.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL.Interface
{
    public interface IEmployeeRepository
    {
        Task<EmployeeViewModel> GetEmployeeById(int id);
        Task<List<LeaveApplicationViewModel>> LeaveApllicationOfEmployees(int id) ;
        Task<LeaveApplicationCreateViewModel> GetLeaveAppOfEmployee(int id);
        Task<LeaveApplicationViewModel> DetailsLeaveApp(int id);
        Task<int> CreateLeaveApp([Bind] LeaveApplicationCreateModel model);
        Task<List<StatisticsViewModel>> GetStatisticsOfEmployee(int id);

        Task<List<TimeSheetList>> GetTimeSheetListss([Bind] GetTimeSheetList model);
    }
}
