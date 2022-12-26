using Entities.Models;

namespace HumanResourceAPI.Infrastructure
{
    public interface IEmployeeRepository : IRepositoryBase<Employee, Guid>
    {
        
    }
}
