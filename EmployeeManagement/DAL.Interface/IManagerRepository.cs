using EmployeeManagement.Models.Manager.Request;
using EmployeeManagement.Models.Manager.Response;
using System.Collections.Generic;

namespace EmployeeManagement.DAL.Interface
{
    public interface IManagerRepository
    {

        #region Statistics
        IList<TimeSheetList> GetTimeSheetList(GetTimeSheetList model);
        IList<Statistic> GetEmployeeStatistics(GetEmployeeStatistics model);
        IList<Statistic> GetDepartmentStatistics(GetDepartmentStatistics model);
        #endregion

        #region Leave Application
        LeaveApplicationList GetEmployeeLeaveApplication(GetEmployeeLeaveApplication model);
        IList<LeaveApplicationList> GetDepartmentLeaveApplication(GetDepartmentLeaveApplication model);
        //bool UpdateLeaveApplication(UpdateLeaveApplication model);
        #endregion

        #region Information
        IList<EmployeeList> GetEmployeeList(GetEmployeeList model);
        #endregion
    }
}
