using EmployeeManagement.Models.HumanResources.Response;
using EmployeeManagement.Models.HumanResources.Request;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL.Interface
{
    public interface IHumanResourcesRepository
    {
        Task<IList<DepartmentView>> GetInfoDerpart();
        Task<IList<EmployeeView>> GetEmployeeByDerpartmentId(int id);
        Task<EmployeeInfoView> GetEmployeeInfo(int id);
        Task<int> CreateEmployee(CreateEmployee employee);
        Task<int> UpdateEmployee(UpdateEmployee update);
        Task<int> DeleteEmployee(int id);
        Task<IList<DepartmentModel>> GetDepartments();
        Task<IList<PositionModel>> GetPositions();
        Task<EmployeeGetById> EmployeeGetById(int id);
        Task<IList<EmployeeView>> SearchEmployee(string search,int id);
        
    }
}
