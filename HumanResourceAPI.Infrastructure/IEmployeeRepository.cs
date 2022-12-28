using Entities.Models;
using Entities.RequestFeatures;

namespace HumanResourceAPI.Infrastructure
{
    public interface IEmployeeRepository : IRepositoryBase<Employee, Guid>
    {
        Task<PagedList<Employee>> GetEmployeeAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChange);
    }
}
