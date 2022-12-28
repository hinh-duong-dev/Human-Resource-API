using Entities.Models;
using Entities;
using HumanResourceAPI.Infrastructure;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {         
        }

        public async Task<PagedList<Employee>> GetEmployeeAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChange)
        {
            var employees = await FindAll(trackChange, e => e.Company.Equals(companyId) &&
                     (e.Age >= employeeParameters.MinAge && e.Age <= employeeParameters.MaxAge))
                    .OrderBy(e => e.FirstName)
                    .ToListAsync();

            return PagedList<Employee>
                .ToPagedList(employees, employeeParameters.PageNumber,
                employeeParameters.PageSize);
        }
    }
}
